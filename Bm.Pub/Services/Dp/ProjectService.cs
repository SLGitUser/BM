﻿using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using Bm.Services.Common;
using System.Collections.Generic;

namespace Bm.Services.Dp
{
    public sealed class ProjectService : RepoService<Project>
    {
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
            var r = base.Delete(models);
            foreach (var model in models)
            {
                r.Append(AccessoryService.DeleteObject(model.AddrPic));
            }
            return r;
        }
        #endregion
    }
}

