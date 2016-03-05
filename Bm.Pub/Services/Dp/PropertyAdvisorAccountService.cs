using System;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;

namespace Bm.Services.Dp
{
    public sealed class PropertyAdvisorAccountService
    {

        public MessageRecorder<PropertyAdvisor> Create(string phone, string password, string ownerNo)
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
                        Name = "未来经纪大师",
                        Phone = phone,
                        CreatedBy = "SYSTEM",
                        CreatedAt = DateTime.Now,
                        Password = password,
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
                    .And(m=>m.BranchNo, Op.Eq, "420100")
                    .And(m => m.RoleNo, Op.Eq, "PropertyAdvisor")
                    .Limit(1);
                if (conn.Exists(q1))
                {
                    trans.Rollback();
                    return mr.Error("账户已经存在，不能重复创建");
                }

                var query = new Criteria<Broker>()
                    .Where(m => m.No, Op.Eq, account.No)
                    .Desc(m => m.No);
                if (conn.Exists(query))
                {
                    trans.Rollback();
                    return mr.Error("账户已经存在，不能重复创建");
                }

                var model2 = new AccountRoleRef
                {
                    AccountNo = account.No,
                    RoleNo = "PropertyAdvisor",
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

                var model = new PropertyAdvisor
                {
                    No = account.No,
                    Name = account.Name,
                    Mobile1 = phone,
                    OwnerNo = "420100",
                    RegAt = DateTime.Now,
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
