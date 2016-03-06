using System;
using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;
using System.Collections.Generic;
using System.Linq;
using Bm.Modules.Helper;

namespace Bm.Services.Dp
{
    public sealed class HouseBrokerRefService : RepoService<HouseBrokerRef>
    {
        public HouseBrokerRefService() : base(null)
        {

        }

        public HouseBrokerRefService(string accountNo) : base(accountNo)
        {

        }

        #region 模型操作

        /// <summary>
        /// 页面列表查看
        /// </summary>
        /// <returns></returns>
        public override IList<HouseBrokerRef> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<HouseBrokerRef>()
                    .Desc(m => m.CreatedAt);
                return conn.Query(query);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override MessageRecorder<bool> Create(HouseBrokerRef model)
        {
            var r = new MessageRecorder<bool>();
            model.CreatedAt = Now;
            model.CreatedBy = AccountNo;
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<HouseBrokerRef>()
                    .Where(m => m.BrokerNo, Op.Eq, model.BrokerNo)
                    .And(m => m.ProjectNo, Op.Eq, model.ProjectNo);
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

        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public override MessageRecorder<bool> Update(Project model)
        //{
        //    var r = new MessageRecorder<bool>();
        //    model.CreatedAt = Now;
        //    model.CreatedBy = AccountNo;
        //    string oldKey;
        //    using (var conn = ConnectionManager.Open())
        //    {
        //        var trans = conn.BeginTransaction();
        //        var query = new Criteria<HouseBrokerRef>()
        //            .Where(m => m.No, Op.Eq, model.No)
        //            .Or(m => m.Name, Op.Eq, model.Name)
        //            .And(m => m.Id, Op.NotEq, model.Id);
        //        if (conn.Exists(query))
        //        {
        //            trans.Rollback();
        //            return r.Error("编号或者名称重复");
        //        }
        //        var obj = new Criteria<HouseBrokerRef>()
        //            .Where(m => m.Id, Op.Eq, model.Id)
        //            .Select(m => m.AddrPic);
        //        oldKey = conn.ExecuteScalarEx<string>(obj.ToSelectSql());

        //        var effectedCount = conn.Update(model, trans);
        //        if (!effectedCount)
        //        {
        //            trans.Rollback();
        //            return r.Error("保存失败");
        //        }

        //        foreach (var projectInfo in model.ProjectInfos)
        //        {
        //            projectInfo.UpdatedAt = Now;
        //            projectInfo.UpdatedBy = AccountNo;
        //            projectInfo.DpNo = model.No;
        //        }


        //        var effectedCount2 = conn.Update(model.ProjectInfos, trans);
        //        if (!effectedCount2)
        //        {
        //            trans.Rollback();
        //            return r.Error("保存失败");
        //        }

        //        trans.Commit();
        //    }
        //    if (!Equals(oldKey, model.AddrPic))
        //    {
        //        r.Append(AccessoryService.DeleteObject(oldKey));
        //        r.Append(AccessoryService.ClearExpiration(model.AddrPic));
        //    }
        //    return r.SetValue(!r.HasError);
        //}


        public override MessageRecorder<bool> Delete(params HouseBrokerRef[] models)
        {
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var r = new MessageRecorder<bool>();
                foreach (var model in models)
                {
                    var query = new Criteria<HouseBrokerRef>()
                        .Where(m => m.BrokerNo, Op.Eq, model.BrokerNo)
                        .And(m => m.ProjectNo, Op.Eq, model.ProjectNo);
                    var proInfo = conn.Query(query);
                    var err = conn.Delete(proInfo);
                    if (!err)
                    {
                        trans.Rollback();
                        return r.Error("删除楼盘周边信息失败");
                    }
                    trans.Commit();
                }
                return r;
            }
        }

        /// <summary>
        /// 根据经纪人编号获取所有楼盘信息
        /// </summary>
        /// <param name="brokerNo">经纪人编号</param>
        /// <returns></returns>
        public MessageRecorder<IList<Project>> GetHouseByBrokerNo(string brokerNo)
        {
            var r = new MessageRecorder<IList<Project>>()
            {
                Value = new List<Project>()
            };
            if (string.IsNullOrEmpty(brokerNo)) return r.Error("请指定经纪人");

            using (var conn = ConnectionManager.Open())
            {
                var refQuery = new Criteria<HouseBrokerRef>()
                    .Where(m => m.BrokerNo, Op.Eq, brokerNo)
                    .Asc(m => m.CreatedAt);
                var proNos = conn.Query(refQuery).Select(m => m.ProjectNo).ToList();
                if (proNos.IsNullOrEmpty()) return r;

                var projectList = new Criteria<Project>()
                    .And(m => m.No, Op.In, proNos);
                var list = conn.Query(projectList);
                return r.SetValue(list);
            }
        }

        /// <summary>
        /// 根据楼盘编号获取所有关系经纪人的信息
        /// </summary>
        /// <param name="houseNo">楼盘编号</param>
        /// <returns></returns>
        public MessageRecorder<IList<Broker>> GetBrokerByHouseNo(string houseNo)
        {
            var r = new MessageRecorder<IList<Broker>>();
            using (var conn = ConnectionManager.Open())
            {
                var refQuery = new Criteria<HouseBrokerRef>()
                    .Where(m => m.ProjectNo, Op.Eq, houseNo)
                    .Asc(m => m.CreatedAt);
                var proNos = conn.Query(refQuery).Select(m => m.ProjectNo).ToList();
                var brokerList = new Criteria<Broker>()
                    .And(m => m.No, Op.In, proNos);
                var list = conn.Query(brokerList);
                if (!list.Any())
                {
                    return r.Error("没有任何房源！");
                }
                return r.SetValue(list);
            }
            ;
        }

        #endregion

        /// <summary>
        /// 获取所有经纪人与房源关系信息
        /// </summary>
        /// <returns></returns>
        public MessageRecorder<IList<HouseBrokerRef>> GetAllHouse()
        {
            var r = new MessageRecorder<IList<HouseBrokerRef>>();
            var projectAll = GetAll();

            if (!projectAll.Any())
            {
                return r.Error("没有任何房源！");
            }
            return r.SetValue(projectAll);
        }

        /// <summary>
        /// 切换经纪人收藏楼盘状态（收藏、解除）
        /// </summary>
        /// <returns>返回切换后的状态（True：代表收藏；False：代表解除）</returns>
        public MessageRecorder<bool> ChangeStatus(string brokerNo, string projectNo)
        {
            var r = new MessageRecorder<bool>();
            if (brokerNo.IsNullOrEmpty())
                return r.Error("经纪人编号无效");
            if (projectNo.IsNullOrEmpty())
                return r.Error("楼盘编号无效");
            //根据经纪人编号获取所有楼盘信息
            var house = GetHouseByBrokerNo(brokerNo).Value;
            var yesHouse = house.Where(m => m.No.Equals(projectNo)).ToList();
            var model = new HouseBrokerRef
            {
                ProjectNo = projectNo,
                BrokerNo = brokerNo,
                CreatedAt = DateTime.Now,
                CreatedBy = "root"
            };
            if (!yesHouse.Any())
            {
                if (Create(model).HasError)
                {
                    return r.Error("收藏失败！");
                }
                r.SetValue(true);
            }
            else
            {
                var refQuery = new Criteria<HouseBrokerRef>()
                    .Where(m => m.ProjectNo, Op.Eq, projectNo)
                    .And(m => m.BrokerNo, Op.Eq, brokerNo)
                    .Asc(m => m.CreatedAt);
                var refB = ConnectionManager.Open().Query(refQuery).FirstOrDefault();
                if (Delete(refB).HasError)
                {
                    return r.Error("解除收藏失败！");
                }
                r.SetValue(false);
            }
            return r;
        }

        /// <summary>
        /// 切换经纪人收藏楼盘状态（收藏、解除）
        /// </summary>
        /// <returns>返回切换后的状态（True：代表收藏；False：代表解除）</returns>
        public MessageRecorder<bool> IsCollect(string brokerNo, string projectNo)
        {
            var r = new MessageRecorder<bool>();
            using (var conn = ConnectionManager.Open())
            {
                if (brokerNo.IsNullOrEmpty())
                    return r.Error("经纪人编号无效");
                if (projectNo.IsNullOrEmpty())
                    return r.Error("楼盘编号无效");
                //根据经纪人编号获取所有楼盘信息
                var refQuery = new Criteria<HouseBrokerRef>()
                    .Where(m => m.ProjectNo, Op.Eq, projectNo)
                    .And(m => m.BrokerNo, Op.Eq, brokerNo)
                    .Asc(m => m.CreatedAt);
                r.SetValue(conn.Query(refQuery).Any());
                return r;
            }
        }


    }
}

