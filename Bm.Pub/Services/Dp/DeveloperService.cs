using System.Collections.Generic;
using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;

namespace Bm.Services.Dp
{
    public sealed class DeveloperService : RepoService<Developer>
    {

        public DeveloperService(string accountNo) : base(accountNo)
        {
        }

        public override IList<Developer> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Developer>()
                    .And(m => m.Id, Op.Gt, 0)
                    .And(m => m.Id, Op.NotIn, new long[] { 0, -1 })
                    .Desc(m => m.No);
                return conn.Query(query);
            }
        }

        public override MessageRecorder<bool> Create(Developer model)
        {
            var r = new MessageRecorder<bool>();

            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();

                var query = new Criteria<Developer>()
                    .And(m => m.No, Op.Eq, model.No)
                    .And(m => m.Name, Op.Eq, model.Name);
                if (conn.Exists(query))
                {
                    trans.Rollback();
                    return r.Error("编号或者名称重复");
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


        public override MessageRecorder<bool> Update(Developer model)
        {
            var r = new MessageRecorder<bool>();

            model.UpdatedAt = Now;
            model.UpdatedBy = AccountNo;

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();

                var query = new Criteria<Developer>()
                    .Where(m => m.No, Op.Eq, model.No)
                    .Or(m => m.Name, Op.Eq, model.Name)
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
