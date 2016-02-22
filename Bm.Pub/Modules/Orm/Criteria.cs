using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Bm.Models.Base;
using Bm.Modules.Helper;
using Bm.Modules.Orm.Sql;

namespace Bm.Modules.Orm
{
    public sealed class Criteria<TModel>
    {
        private readonly IList<string> _selectFields = new List<string>();

        private readonly IList<string> _whereSqls = new List<string>();

        private readonly IList<string> _orders = new List<string>();

        public Criteria<TModel> And(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return this;

            var lastSql = _whereSqls.Any()
                ? $"({_whereSqls.Last()}) AND ({sql})"
                : sql;
            _whereSqls.Add(lastSql);
            return this;
        }

        public Criteria<TModel> Or(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return this;

            var lastSql = _whereSqls.Any()
                ? $"({_whereSqls.Last()}) OR ({sql})"
                : sql;
            _whereSqls.Add(lastSql);
            return this;
        }

        private string MakeSql<TProp>(Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            if (propExpr == null) throw new ArgumentNullException(nameof(propExpr));

            if (value == null) throw new ArgumentNullException(nameof(value));

            var prop = "`" + propExpr.GetMemberName() + "`";
            if (string.IsNullOrEmpty(prop)) throw new ArgumentNullException(nameof(propExpr));

            var tpl = op.AsString();
            var v = tpl.Contains("'") ? value.ToString() : WrapValue(typeof(TProp), value);
            return string.Format(tpl, prop, v);
        }
        
        private string MakeInSql<TProp>(Expression<Func<TModel, TProp>> propExpr, Op op, IList<TProp> values)
        {
            if (propExpr == null) throw new ArgumentNullException(nameof(propExpr));

            if (values == null) throw new ArgumentNullException(nameof(values));

            var prop = propExpr.GetMemberName();
            if (string.IsNullOrEmpty(prop)) throw new ArgumentNullException(nameof(propExpr));

            var type = typeof(TProp);
            var list = values.Select(value => WrapValue(type, value));
            var v = string.Join(",", list);
            var tpl = op.AsString();
            return string.Format(tpl, prop, v);
        }

        private static string WrapValue(Type type, object obj)
        {
            //if (type.IsNumericType()) return obj.ToString();
            if (type == typeof(string))
                return Equals(obj, null) ? "NULL" : string.Concat("'", obj, "'");
            if (type == typeof(DateTime))
                return string.Concat("'", ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss"), "'");
            if (type == typeof(DateTime))
                return Equals(obj, null) ? "NULL" : string.Concat("'", ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss"), "'");
            return obj.ToString();
        }

        private void GetFromSql(StringBuilder buff)
        {
            buff.Append(" FROM ");
            buff.Append(MapHelper.GetTableName<TModel>());
            if (_whereSqls.Any())
            {
                buff.Append(" WHERE ");
                buff.Append(_whereSqls.Last());
            }
            if (_orders.Any())
            {
                buff.Append(" ORDER BY ");
                buff.Append(string.Join(", ", _orders));
            }

            #region MySQL

            if (_count.HasValue)
            {
                buff.Append(" LIMIT ");
                if (_offset.HasValue)
                {
                    buff.Append(_offset.Value);
                    buff.Append(", ");
                }
                buff.Append(_count.Value);
            }

            #endregion
        }

        public string WhereSql => _whereSqls.Last();

        public Criteria<TModel> ClearCondition()
        {
            _whereSqls.Clear();
            return this;
        }

        public Criteria<TModel> ClearCondition(int i)
        {
            while (i-- > 0 && _whereSqls.Any())
            {
                _whereSqls.RemoveAt(_whereSqls.Count - 1);
            }
            return this;
        }

        public Criteria<TModel> And(Criteria<TModel> q)
        {
            return And(q.WhereSql);
        }

        public Criteria<TModel> Or(Criteria<TModel> q)
        {
            return Or(q.WhereSql);
        }

        public Criteria<TModel> Select<TProp>(Expression<Func<TModel, TProp>> propExpr)
        {
            var prop = propExpr.GetMemberName();
            if (!string.IsNullOrEmpty(prop))
                _selectFields.Add(prop);
            return this;
        }

        public Criteria<TModel> SelectAll()
        {
            _selectFields.Clear();
            return this;
        }


        public Criteria<TModel> Where<TProp>(Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            if (op == Op.In || op == Op.NotIn)
                throw new ArgumentException("invalid op", nameof(op));

            _whereSqls.Clear();
            var sql = MakeSql(propExpr, op, value);
            And(sql);
            return this;
        }

        public Criteria<TModel> And<TProp>(Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            if (op == Op.In || op == Op.NotIn)
                throw new ArgumentException("invalid op", nameof(op));

            var sql = MakeSql(propExpr, op, value);
            return And(sql);
        }

        public Criteria<TModel> Or<TProp>(Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            if (op == Op.In || op == Op.NotIn)
                throw new ArgumentException("invalid op", nameof(op));

            var sql = MakeSql(propExpr, op, value);
            return Or(sql);
        }

        public Criteria<TModel> And<TProp>(Expression<Func<TModel, TProp>> propExpr, Op op, IList<TProp> values)
        {
            if (op != Op.In && op != Op.NotIn)
                throw new ArgumentException("invalid op", nameof(op));

            var sql = MakeInSql(propExpr, op, values);
            return And(sql);
        }

        public Criteria<TModel> Or<TProp>(Expression<Func<TModel, TProp>> propExpr, Op op, IList<TProp> values)
        {
            if (op != Op.In && op != Op.NotIn)
                throw new ArgumentException("invalid op", nameof(op));

            var sql = MakeInSql(propExpr, op, values);
            return Or(sql);
        }

        #region If or Unless

        public Criteria<TModel> AndIf<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            return condition ? And(propExpr, op, value) : this;
        }

        public Criteria<TModel> OrIf<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            return condition ? Or(propExpr, op, value) : this;
        }

        public Criteria<TModel> AndIf<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, IList<TProp> values)
        {
            return condition ? And(propExpr, op, values) : this;
        }

        public Criteria<TModel> OrIf<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, IList<TProp> values)
        {
            return condition ? Or(propExpr, op, values) : this;
        }

        public Criteria<TModel> AndUnless<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            return condition ? this : And(propExpr, op, value);
        }

        public Criteria<TModel> OrUnless<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, TProp value)
        {
            return condition ? this : Or(propExpr, op, value);
        }

        public Criteria<TModel> AndUnless<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, IList<TProp> values)
        {
            return condition ? this : And(propExpr, op, values);
        }

        public Criteria<TModel> OrUnless<TProp>(bool condition, Expression<Func<TModel, TProp>> propExpr, Op op, IList<TProp> values)
        {
            return condition ? this : Or(propExpr, op, values);
        }

        #endregion

        #region 排序

        public Criteria<TModel> Asc<TProp>(Expression<Func<TModel, TProp>> propExpr)
        {
            return OrderBy(propExpr, true);
        }

        public Criteria<TModel> Desc<TProp>(Expression<Func<TModel, TProp>> propExpr)
        {
            return OrderBy(propExpr, false);
        }

        public Criteria<TModel> OrderBy<TProp>(Expression<Func<TModel, TProp>> propExpr, bool isAsc)
        {
            var prop = propExpr.GetMemberName();
            if (string.IsNullOrEmpty(prop))
                throw new ArgumentException("表达式不正确", nameof(propExpr));

            _orders.Add(string.Concat(prop, isAsc ? " ASC" : " DESC"));
            return this;
        }

        public Criteria<TModel> ClearOrder()
        {
            _orders.Clear();
            return this;
        }

        #endregion

        #region 分页数量

        public Criteria<TModel> Limit(int offset, int count)
        {
            _offset = offset;
            _count = count;
            return this;
        }

        public Criteria<TModel> Limit(int count)
        {
            _offset = null;
            _count = count;
            return this;
        }

        public Criteria<TModel> Page(int pagesize, int pageno)
        {
            if (pageno <= 0 || pagesize <= 0)
            {
                _offset = null;
                _count = null;
            }
            else
            {
                _offset = (pageno - 1) * pagesize;
                _count = pagesize;
            }
            return this;
        }

        private int? _offset;
        private int? _count;

        #endregion

        public Criteria<TModel> Clear()
        {
            SelectAll();
            ClearCondition();
            ClearOrder();
            Page(0, 0);
            return this;
        }

        public string ToSelectSql()
        {
            var buff = new StringBuilder();
            buff.Append("SELECT ");
            buff.Append(_selectFields.Any() ? string.Join(", ", _selectFields) : "*");
            GetFromSql(buff);
            return buff.ToString();
        }

        public string ToCountSql()
        {
            var buff = new StringBuilder();
            buff.Append("SELECT COUNT(*) AS count");
            GetFromSql(buff);
            return buff.ToString();
        }

        public string ToDeleteSql()
        {
            var buff = new StringBuilder();
            buff.Append("DELETE");
            GetFromSql(buff);

            return buff.ToString();
        }

    }
}
