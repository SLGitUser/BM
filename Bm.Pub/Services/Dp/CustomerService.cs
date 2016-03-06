using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bm.Services.Common;

namespace Bm.Services.Dp
{
   public sealed class CustomerService : RepoService<Customer>
    {
        public CustomerService() : base(null)
        {

        }

        public CustomerService(string accountNo) : base(accountNo)
        {

        }
        public override Customer GetById(long id)
        {
            using (var conn = ConnectionManager.Open())
            {
                var model = conn.Get<Customer>(id);

                if (model != null)
                {
                    var query = new Criteria<Customer>()
                        .Where(m => m.No, Op.Eq, model.No)
                        .Asc(m => m.Id);
                };
                return model;
            }
        }
        public MessageRecorder<IList<Customer>> GetAllCustomer()
        {
            var r = new MessageRecorder<IList<Customer>>();
            var customerAll = GetAll();

            if (!customerAll.Any())
            {
                return r.Error("没有客户信息");
            }
            return r.SetValue(customerAll);
        }
        public override MessageRecorder<bool> Create(Customer model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Customer>()
                    .Where(m => m.No, Op.Eq, model.No)
                    .Or(m => m.Name, Op.Eq, model.Name)
                    .Desc(m => m.No);
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
                //var r2 = conn.Insert(model.Customers);
                //var count = model.Customers.Count;
                //if (r2 != count)
                //{
                //    trans.Rollback();
                //    return r.Error("保存失败");
                //} 
                trans.Commit();
            }
            return r.SetValue(!r.HasError); 
        }
    }
    }
