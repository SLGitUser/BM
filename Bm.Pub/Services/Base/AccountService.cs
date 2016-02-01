using System.Collections.Generic;
using System.Linq;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;

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
                        .Where(m => m.AccountNo, Op.Eq, accountNo);
                    model.RoleRefs = conn.Query(roleQuery);
                }
                return model;
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

        public MessageRecorder<Account> Auth(string username, string password)
        {
            var r = new MessageRecorder<Account>();
            var account = GetAll().FirstOrDefault(m => m.No.Equals(username) || m.Email.Equals(username) || m.Phone.Equals(username));
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
            r.Append(Update(account));

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
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Account>()
                    .Where(m => m.No, Op.Eq, model.No)
                    .OrUnless(string.IsNullOrWhiteSpace(model.Email),
                        m => m.Email, Op.Eq, model.Email)
                    .OrUnless(string.IsNullOrWhiteSpace(model.Phone),
                        m => m.Phone, Op.Eq, model.Phone);
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
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Account>()
                    .Where(m => m.No, Op.Eq, model.No)
                    .OrUnless(string.IsNullOrWhiteSpace(model.Email),
                        m => m.Email, Op.Eq, model.Email)
                    .OrUnless(string.IsNullOrWhiteSpace(model.Phone),
                        m => m.Phone, Op.Eq, model.Phone)
                    .And(m => m.Id, Op.NotEq, model.Id);
                if (conn.Exists(query))
                {
                    trans.Rollback();
                    return r.Error("编号或者名称重复");
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
    }
}
