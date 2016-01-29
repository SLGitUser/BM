using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bm.Services.Dp
{
    public sealed class ProjectService : RepoService<Project>
    {
        public ProjectService(string accountNo) : base(accountNo)
        {

        }
        /// <summary>
        /// 页面列表查看
        /// </summary>
        /// <returns></returns>
        public override IList<Project> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Project>()
                    .And(m => m.Id, Op.Gt, 0)
                    .And(m => m.Id, Op.NotIn, new long[] { 0, -1 })
                    .Desc(m => m.No);
                return conn.Query(query);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Create(Project model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Project>()
                    .And(m => m.No, Op.Gt, model.No)
                    .And(m => m.Name, Op.Gt, model.Name)
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
                trans.Commit();
                return r.SetValue(true);
            }

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Update(Project model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;

            using (var conn=ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction(); 
                var query = new Criteria<Project>()
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

