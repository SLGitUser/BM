using System;
using System.Collections.Generic;
using System.Linq;
using Bm.Models.Common;
using Bm.Modules.Helper;
using Bm.Modules.Orm;

namespace Bm.Services.Common
{
    public class RepoService<TModel>
        where TModel : class
    {
        protected readonly string AccountNo;

        protected readonly DateTime Now = DateTime.Now;

        public RepoService(string accountNo)
        {
            AccountNo = accountNo;
        }

        public virtual IList<TModel> GetAll()
        {
            using (var conn = ConnectionManager.Open())
            {
                return conn.GetAll<TModel>().ToList();
            }
        }

        public virtual TModel GetById(long id)
        {
            using (var conn = ConnectionManager.Open())
            {
                return conn.Get<TModel>(id);
            }
        }

        public virtual IList<TModel> GetByIds(long[] ids)
        {
            using (var conn = ConnectionManager.Open())
            {
                return conn.GetBy<TModel, long>(ids).ToList();
            }
        }

        public virtual MessageRecorder<bool> Update(TModel model)
        {
            var r = new MessageRecorder<bool>();
            using (var conn = ConnectionManager.Open())
            {
                var effectedCount = conn.Update(model);
                return r.SetValue(effectedCount);
            }
        }

        public virtual MessageRecorder<bool> Delete(params TModel[] models)
        {
            var r = new MessageRecorder<bool>();
            if (models.IsNullOrEmpty()) return r.SetValue(true);
            
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var isOk = models.All(m=>conn.Delete(m, trans));
                if (isOk)
                {
                    trans.Commit();
                    return r.SetValue(true);
                }
                trans.Rollback();
                return r;
            }
        }

        public virtual MessageRecorder<bool> Create(TModel model)
        {
            var r = new MessageRecorder<bool>();
            using (var conn = ConnectionManager.Open())
            {
                var effectedCount = conn.Insert(model);
                return r.SetValue(effectedCount > -1);
            }
        }
    }
}
