using System;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;

namespace Bm.Services.Dp
{
    public sealed class BrokerAccountService
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="cityCode"></param>
        /// <param name="name"></param>
        /// <param name="firmNo"></param>
        /// <param name="referral"></param>
        /// <returns></returns>
        public MessageRecorder<Broker> Create(
            string phone, 
            string password,
            string cityCode = null,
            string name = null,
            string firmNo = null,
            string referral = null)
        {
            var mr= new MessageRecorder<Broker>();
            
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
                        Name = name ?? "未来经纪大师",
                        Phone = phone,
                        CreatedBy = "SYSTEM",
                        CreatedAt = DateTime.Now,
                        Password = password,
                        Status = AccountStatus.Type.Normal,
                        Referral = referral
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
                    .And(m => m.RoleNo, Op.Eq, "Broker")
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
                    RoleNo = "Broker",
                    BranchNo = cityCode ?? "420100",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "SYSTEM"
                };
                var effectedCount1 = conn.Insert(model2, trans);
                if (effectedCount1 == -1)
                {
                    trans.Rollback();
                    return mr.Error("账户创建失败，请重试");
                }

                var model = new Broker
                {
                    No = account.No,
                    Name = account.Name,
                    Mobile = phone,
                    CityNo = cityCode ?? "420100",
                    FirmNo = firmNo,
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
