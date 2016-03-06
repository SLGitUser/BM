using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebSockets;
using Bm.Modules.Helper;

namespace Bm.Services.Dp
{
    public sealed class ProjectService : RepoService<Project>
    {
        public ProjectService() : base(null)
        {

        }

        public ProjectService(string accountNo) : base(accountNo)
        {

        }
        #region 模型操作

        /// <summary>
        /// 页面列表查看
        /// </summary>
        /// <returns></returns>
        public override IList<Project> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Project>()
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
                var r2 = conn.Insert(model.ProjectInfos);
                var count = model.ProjectInfos.Count;
                if (r2 != count)
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }

                trans.Commit();
            }
            r.Append(AccessoryService.ClearExpiration(model.AddrPic));
            return r.SetValue(!r.HasError);

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
            string oldKey;
            using (var conn = ConnectionManager.Open())
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
                var obj = new Criteria<Project>()
                    .Where(m => m.Id, Op.Eq, model.Id)
                    .Select(m => m.AddrPic);
                oldKey = conn.ExecuteScalarEx<string>(obj.ToSelectSql());

                var effectedCount = conn.Update(model, trans);
                if (!effectedCount)
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }

                foreach (var projectInfo in model.ProjectInfos)
                {
                    projectInfo.UpdatedAt = Now;
                    projectInfo.UpdatedBy = AccountNo;
                    projectInfo.DpNo = model.No;
                }


                var effectedCount2 = conn.Update(model.ProjectInfos, trans);
                if (!effectedCount2)
                {
                    trans.Rollback();
                    return r.Error("保存失败");
                }

                trans.Commit();
            }
            if (!Equals(oldKey, model.AddrPic))
            {
                r.Append(AccessoryService.DeleteObject(oldKey));
                r.Append(AccessoryService.ClearExpiration(model.AddrPic));
            }
            return r.SetValue(!r.HasError);
        }


        public override MessageRecorder<bool> Delete(params Project[] models)
        {
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var r = base.Delete(models);
                foreach (var model in models)
                {
                    var query = new Criteria<ProjectInfo>()
                        .Where(m => m.DpNo, Op.Eq, model.No)
                        .Asc(m => m.Id);
                    var proInfo = conn.Query(query);
                    var err = conn.Delete(proInfo);
                    if (!err)
                    {
                        trans.Rollback();
                        return r.Error("删除楼盘周边信息失败");
                    }
                    trans.Commit();
                    r.Append(AccessoryService.DeleteObject(model.AddrPic));
                }
                return r;
            }
        }


        public override Project GetById(long id)
        {
            using (var conn = ConnectionManager.Open())
            {
                var model = conn.Get<Project>(id);

                if (model != null)
                {
                    var query = new Criteria<ProjectInfo>()
                        .Where(m => m.DpNo, Op.Eq, model.No)
                        .Asc(m => m.Id)
                        //.Limit(6)
                        ;
                    model.ProjectInfos = conn.Query(query);
                };
                return model;
            }
        }

        public Project GetByNo(string no)
        {
            using (var conn = ConnectionManager.Open())
            {
                var pro = new Criteria<Project>()
                    .Where(m=>m.No,Op.Eq, no);
                var model = conn.Query(pro).FirstOrDefault();

                if (model != null)
                {
                    var query = new Criteria<ProjectInfo>()
                        .Where(m => m.DpNo, Op.Eq, model.No)
                        .Asc(m => m.Id)
                        //.Limit(6)
                        ;
                    model.ProjectInfos = conn.Query(query);
                };
                return model;
            }
        }
        #endregion
        public MessageRecorder<IList<Project>> GetAllHouse()
        {
            var r = new MessageRecorder<IList<Project>>();
            var projectAll = GetAll();

            if (!projectAll.Any())
            {
                return r.Error("没有任何房源！");
            }
            return r.SetValue(projectAll);
        }
    }
}

