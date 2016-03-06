using System;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Dapper;

namespace Bm.Services.Dp
{
    public sealed class PropertyAdvisorAccountService
    {

        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="no"></param>
        /// <param name="projectNo"></param>
        /// <returns></returns>
        public MessageRecorder<bool> Remove(string no, string projectNo)
        {

            var mr = new MessageRecorder<bool>();

            if (string.IsNullOrEmpty(no)) return mr.Error("请指定账号");
            if (string.IsNullOrEmpty(projectNo)) return mr.Error("请指定楼盘编号号");

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();

                var accountQuery = new Criteria<PropertyAdvisor>()
                    .Where(m => m.No, Op.Eq, no)
                    .And(m => m.ProjectNo, Op.Eq, projectNo);
                var account = conn.Get(accountQuery);

                if (account != null)
                {
                    var effectedCount1 = conn.Delete(account, trans);
                    if (!effectedCount1)
                    {
                        trans.Rollback();
                        return mr.Error("删除失败");
                    }
                }

                var query = new Criteria<PropertyAdvisor>()
                    .Where(m => m.No, Op.Eq, no)
                    .Desc(m => m.No)
                    .Limit(1);
                if (!conn.Exists(query))
                {
                    var q1 = new Criteria<AccountRoleRef>()
                        .Where(m => m.AccountNo, Op.Eq, no)
                        .And(m => m.BranchNo, Op.Eq, "420100")
                        .And(m => m.RoleNo, Op.Eq, "PropertyStaff");
                    var r2 = conn.Execute(q1.ToDeleteSql(), null, trans);
                    if (r2 == -1)
                    {
                        trans.Rollback();
                        return mr.Error("账户已经存在，不能重复创建");
                    }
                }

                trans.Commit();

                return mr.SetValue(true);
            }
        }

        
        public MessageRecorder<PropertyAdvisor> Create(
            string name,
            string phone,
            bool isManager,
            string projectNo)
        {
            var mr = new MessageRecorder<PropertyAdvisor>();

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();

                var accountQuery = new Criteria<Account>()
                    .Or(m => m.Phone, Op.Eq, phone)
                    .Or(m => m.Phone2, Op.Eq, phone)
                    .Or(m => m.Phone3, Op.Eq, phone);
                var account = conn.Get(accountQuery);

                if (account == null)
                {
                    account = new Account
                    {
                        No = Guid.NewGuid().ToString("N").ToUpper(),
                        Name = name,
                        Phone = phone,
                        CreatedBy = "SYSTEM",
                        CreatedAt = DateTime.Now,
                        Password = phone + "##",
                        Status = AccountStatus.Type.Normal
                    };
                    var effectedCount = conn.Insert(account, trans);
                    if (effectedCount == -1)
                    {
                        trans.Rollback();
                        return mr.Error("创建账户失败");
                    }
                }

                var q1 = new Criteria<AccountRoleRef>()
                    .Where(m => m.AccountNo, Op.Eq, account.No)
                    .And(m => m.BranchNo, Op.Eq, "420100")
                    .And(m => m.RoleNo, Op.Eq, "PropertyStaff")
                    .Limit(1);
                if (!conn.Exists(q1))
                {
                    var model2 = new AccountRoleRef
                    {
                        AccountNo = account.No,
                        RoleNo = "PropertyStaff",
                        BranchNo = "420100",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "SYSTEM"
                    };
                    var effectedCount1 = conn.Insert(model2, trans);
                    if (effectedCount1 == -1)
                    {
                        trans.Rollback();
                        return mr.Error("账户创建失败，请重试");
                    }
                }

                var query = new Criteria<PropertyAdvisor>()
                    .Where(m => m.No, Op.Eq, account.No)
                    .And(m => m.ProjectNo, Op.Eq, projectNo)
                    .Desc(m => m.No);
                if (conn.Exists(query))
                {
                    trans.Rollback();
                    return mr.Error("账户已经存在，不能重复创建");
                }

                var model = new PropertyAdvisor
                {
                    No = account.No,
                    Name = account.Name,
                    MobileNo = phone,
                    ProjectNo = projectNo,
                    Title = isManager ? "案场经理" : "案场顾问",
                    CreatedBy = "SYSTEM",
                    CreatedAt = DateTime.Now
                };
                var effectedCount2 = conn.Insert(model, trans);
                if (effectedCount2 == -1)
                {
                    trans.Rollback();
                    return mr.Error("账户创建失败，请重试");
                }

                trans.Commit();
                mr.SetValue(model);
            }

            return mr;
        }
    }
}
