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
using System.Web.Mvc;

namespace Bm.Services.Dp
{
    public sealed class BrokerageFirmService : RepoService<BrokerageFirm>
    {
        public BrokerageFirmService(string accountNo) : base(accountNo)
        {

        }
        #region 模型操作

        /// <summary>
        /// 页面列表查看
        /// </summary>
        /// <returns></returns>
        public override IList<BrokerageFirm> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<BrokerageFirm>()
                    .And(m => m.Id, Op.Gt, 0)
                    .And(m => m.Id, Op.NotIn, new long[] { 0, -1 })
                    .Desc(m => m.FirmNo);
                return conn.Query(query);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Create(BrokerageFirm model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<BrokerageFirm>()
                    .And(m => m.FirmNo, Op.Gt, model.FirmNo)
                    .And(m => m.Name, Op.Gt, model.Name)
                    .Desc(m => m.FirmNo);
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
        public override MessageRecorder<bool> Update(BrokerageFirm model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;

            using (var conn=ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction(); 
                var query = new Criteria<BrokerageFirm>()
                    .Where(m => m.FirmNo, Op.Eq, model.FirmNo)
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
        #endregion
       
    }
}

