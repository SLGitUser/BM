using System;
using System.Collections.Generic;
using System.Linq;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Modules.Helper;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;
using Dapper;

namespace Bm.Services.Base
{
    public sealed class AccountService : RepoService<Account>
    {
        public AccountService() : base(null)
        {

        }

        public AccountService(string accountNo) : base(accountNo)
        {

        }

        public string GetBranchNo()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<AccountRoleRef>()
                    .Where(m => m.AccountNo, Op.Eq, AccountNo)
                    .Limit(1);
                var model = conn.Query(query).FirstOrDefault();
                return model?.BranchNo;
            }
        }


        public Account GetAccount(string accountNo)
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Account>()
                    .Where(m => m.No, Op.Eq, accountNo)
                    .Limit(1);
                var model = conn.Query(query).FirstOrDefault();
                if (model != null)
                {
                    var roleQuery = new Criteria<AccountRoleRef>()
                        .Where(m => m.AccountNo, Op.Eq, model.No);
                    model.RoleRefs = conn.Query(roleQuery);
                }
                return model;
            }

        }


        public MessageRecorder<Account> GetAccountByMobileNo(string mobileNo)
        {
            var r = new MessageRecorder<Account>();
            if (string.IsNullOrEmpty(mobileNo))
            {
                return r.Error("请输入手机号码");
            }
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Account>()
                    .Or(m => m.Phone, Op.Eq, mobileNo)
                    .Or(m => m.Phone2, Op.Eq, mobileNo)
                    .Or(m => m.Phone3, Op.Eq, mobileNo)
                    .Limit(1);
                var model = conn.Query(query).FirstOrDefault();
                if (model != null)
                {
                    var roleQuery = new Criteria<AccountRoleRef>()
                        .Where(m => m.AccountNo, Op.Eq, model.No);
                    model.RoleRefs = conn.Query(roleQuery);
                }
                return r.SetValue(model);
            }

        }


        /// <summary>
        /// 页面列表查看
        /// </summary>
        /// <returns></returns>
        public override IList<Account> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Account>()
                    .Desc(m => m.No);
                return conn.Query(query);
            }
        }

        /// <summary>
        /// 检查账户是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public MessageRecorder<bool> IsExists(string username)
        {
            var r = new MessageRecorder<bool>();
            if (string.IsNullOrEmpty(username))
            {
                return r.Error("请输入账户名");
            }
            var account = GetAll().FirstOrDefault(m => m.ValidNos.Contains(username));
            return r.SetValue(account != null);
        }

        public MessageRecorder<Account> Auth(string username, string password)
        {
            var r = new MessageRecorder<Account>();
            if (string.IsNullOrEmpty(username))
            {
                return r.Error("请输入账户名");
            }
            if (string.IsNullOrEmpty(password))
            {
                return r.Error("请输入密码");
            }
            var account = GetAll().FirstOrDefault(m => m.ValidNos.Contains(username));
            if (account == null)
            {
                return r.Error("账户名或者密码不匹配");
            }

            if (!account.AuthPassword(password))
            {
                account.ErrLoginCount++;
                account.ErrLoginAt = Now;
                r.Append(Update(account));
                return r.Error("密码不正确");
            }

            account.LastLoginAt = Now;
            account.ErrLoginCount = 0;
            r.Append(UpdateLoginInfo(account));

            return r.SetValue(account);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Create(Account model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;
            if (model.ValidNos.Any() && model.ValidNos.Count != model.ValidNos.Distinct().Count())
            {
                return r.Error("电话或者邮件地址重复");
            }

            var validNos = model.ValidNos;
            if (validNos.IsNullOrEmpty()) return r.Error("没有有效账户标识");

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Account>()
                    .Or(m => m.No, Op.In, validNos)
                    .Or(m => m.Email, Op.In, validNos)
                    .Or(m => m.Phone, Op.In, validNos)
                    .Or(m => m.Phone2, Op.In, validNos)
                    .Or(m => m.Phone3, Op.In, validNos);
                if (conn.Exists(query))
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }
                var effectedCount = conn.Insert(model, trans);
                if (effectedCount == -1)
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }
                var accountRoleRefQuery = new Criteria<AccountRoleRef>()
                    .Where(m=>m.AccountNo, Op.Eq, AccountNo);
                var accountRoleRef = conn.Get(accountRoleRefQuery);
                if (string.IsNullOrEmpty(accountRoleRef?.BranchNo))
                {
                    trans.Rollback();
                    return r.Error("没有找到分支号，保存失败");
                }
                var arr = new AccountRoleRef
                {
                    AccountNo = model.No,
                    BranchNo = accountRoleRef.BranchNo,
                    RoleNo = "BranchStaff",
                    CreatedAt = DateTime.Now,
                    CreatedBy = AccountNo
                };
                var effectedCount1 = conn.Insert(arr, trans);
                if (effectedCount1 == -1)
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }
                trans.Commit();
                return r.SetValue(true);
            }
        }

        public MessageRecorder<bool> UpdateLoginInfo(Account model)
        {
            var r = new MessageRecorder<bool>();
            model.UpdatedAt = Now;
            model.UpdatedBy = AccountNo;

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var sql = @"UPDATE `base_account` SET "
                + " `LastLoginAt` = @LastLoginAt, `ErrLoginCount` = @ErrLoginCount, `UpdatedAt` = @UpdatedAt, `UpdatedBy` = @UpdatedBy"
                + " WHERE `no`= @No";
                var effectedCount = conn.Execute(sql, model, trans);
                if (effectedCount <= 0 )
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }
                trans.Commit();
                return r.SetValue(true);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Update(Account model)
        {
            var r = new MessageRecorder<bool>();
            model.UpdatedAt = Now;
            model.UpdatedBy = AccountNo;

            var validNos = model.ValidNos;
            if (validNos.IsNullOrEmpty()) return r.Error("没有有效账户标识");
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Account>()
                    .Or(m => m.No, Op.In, validNos)
                    .Or(m => m.Email, Op.In, validNos)
                    .Or(m => m.Phone, Op.In, validNos)
                    .Or(m => m.Phone2, Op.In, validNos)
                    .Or(m => m.Phone3, Op.In, validNos)
                    .And(m => m.Id, Op.NotEq, model.Id);
                if (conn.Exists(query))
                {
                    trans.Rollback();
                    return r.Error("编号或者名称重复");
                }

                // 在不修改密码的情况下，赋予原来的密码
                if (string.IsNullOrEmpty(model.PasswordHash))
                {
                    query.ClearCondition().And(m => m.Id, Op.Eq, model.Id);
                    var oriModel = conn.Query(query).FirstOrDefault();
                    if (oriModel == null)
                    {
                        return r.Error("账户不存在");
                    }
                    model.PasswordHash = oriModel.PasswordHash;
                    model.PasswordSalt = oriModel.PasswordSalt;
                }

                var effectedCount = conn.Update(model, trans);
                if (!effectedCount)
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }
                trans.Commit();
                return r.SetValue(true);
            }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public MessageRecorder<bool> UpdatePassword(string username, string password)
        {
            var r = new MessageRecorder<bool>();
            if (string.IsNullOrEmpty(username)) return r.Error("请设置用户名");
            if (string.IsNullOrEmpty(password)) return r.Error("请设置密码");
            
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Account>()
                    .Or(m => m.No, Op.Eq, username)
                    .Or(m => m.Email, Op.Eq, username)
                    .Or(m => m.Phone, Op.Eq, username)
                    .Or(m => m.Phone2, Op.Eq, username)
                    .Or(m => m.Phone3, Op.Eq, username);
                var model = conn.Get(query);
                if(model == null)
                {
                    trans.Rollback();
                    return r.Error("账户不存在");
                }
                
                model.Password = password;

                var effectedCount = conn.Update(model, trans);
                if (!effectedCount)
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }
                trans.Commit();
                return r.SetValue(true);
            }
        }
    }
}
