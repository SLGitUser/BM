using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bm.Services.Dp
{
    public sealed class BrokerService: RepoService<Broker>
    {
        public BrokerService() : base(null)
        {

        }

        public BrokerService(string accountNo) : base(accountNo)
        {

        }
        #region 模型操作

        /// <summary>
        /// 页面列表查看
        /// </summary>
        /// <returns></returns>
        public override IList<Broker> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Broker>()
                    .Desc(m => m.No);
                return conn.Query(query);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Create(Broker model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Broker>()
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
                trans.Commit();
            }
            return r.SetValue(!r.HasError);

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Update(Broker model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<Broker>()
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
            }
            return r.SetValue(true);
        }

        #endregion
        #region API访问
        /// <summary>
        /// 根据经纪人编号查询所属公司信息
        /// </summary>
        /// <returns></returns>
        public MessageRecorder<BrokerageFirm> GetByNo(string brokerNo)
        {
            var r = new MessageRecorder<BrokerageFirm>();
            if (string.IsNullOrEmpty(brokerNo)) return r.Error("经纪人编号无效！");
            using (var conn = ConnectionManager.Open())
            {
                var brokerQuery = new Criteria<Broker>()
                    .Where(m => m.No, Op.Eq, brokerNo)
                    .Desc(m => m.No);
                var broker = conn.Get(brokerQuery);
                if (broker == null) return r.Error("程序错误，请重新登录后重试！");
                var firmQuery = new Criteria<BrokerageFirm>()
                    .Where(m => m.FirmNo, Op.Eq, broker.FirmNo)
                    .Desc(m => m.FirmNo);
                var firm = conn.Get(firmQuery);
                if (firm==null)
                {
                    r.SetValue(new BrokerageFirm());
                }
                r.SetValue(firm);
                return r;
            }
        }

        #endregion
        #region SelectHelper

        public static SelectList GetBldType()
        {
            //using (var conn = ConnectionManager.Open())
            //{
            //var query = new Criteria<BldType>()
            //    .Desc(m => m.No);
            //return conn.Query(query);
            //}
            var list = new[] { new { Text = "类型1", Value = "类型1" }, new { Text = "类型2", Value = "类型2" } };
            return new SelectList(list, "Text", "Value");
        }
        #endregion
    }
}
