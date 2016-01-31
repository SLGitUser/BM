using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Bm.Modules.Helper;

namespace Bm.Modules.Html
{
    public class HtmlTable<TModel>
    {
        private readonly IList<TModel> _models;

        private readonly ContentTable _tbl;

        private readonly string _tblClass;

        public HtmlTable(List<TModel> models, string tblClass)
        {
            _models = models;
            _tblClass = tblClass;
            _tbl = new ContentTable(models.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProp">属性类型</typeparam>
        /// <param name="propExpr">属性表达式</param>
        /// <param name="formatFunc">格式函数</param>
        /// <param name="styleFunc">样式表达式函数</param>
        /// <param name="headerName">表格列头名称</param>
        /// <param name="headerStyleFunc">表格列头样式</param>
        /// <returns></returns>
        public HtmlTable<TModel> Add<TProp>(Expression<Func<TModel, TProp>> propExpr,
            Func<TProp, string> formatFunc = null,
            Func<TProp, string> styleFunc = null,
            string headerName = null,
            Func<TProp, string> headerStyleFunc = null)
        {
            var propName = propExpr.GetMemberName();
            if (string.IsNullOrEmpty(propName)) return this;

            var pi = typeof(TModel).GetProperty(propName);
            var displayNameAttr = pi.GetAttribute<DisplayNameAttribute>();
            var displayName = displayNameAttr?.DisplayName.SetWhenNullOrEmpty(pi.Name);
            _tbl.HeadEles.Add(new HtmlElement("th").Attr("data-prop", pi.Name).Text(displayName));

            var propFunc = propExpr.Compile();
            var fmtFunc = formatFunc ?? (m => m?.ToString());
            for (var i = 0; i < _models.Count; i++)
            {
                var propValue = propFunc(_models[i]);
                var displayValue = fmtFunc(propValue);
                var style = styleFunc?.Invoke(propValue);
                var ele = new HtmlElement("td").Text(displayValue)
                    .AttrUnlessNullOrEmpty("class", style);
                _tbl.BodyEles[i].Add(ele);
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProp">属性类型</typeparam>
        /// <param name="propExpr">属性表达式</param>
        /// <param name="formatFunc">格式函数</param>
        /// <param name="styleFunc">样式表达式函数</param>
        /// <param name="headerName">表格列头名称</param>
        /// <param name="headerStyleFunc">表格列头样式</param>
        /// <returns></returns>
        public HtmlTable<TModel> AddEx<TProp>(Expression<Func<TModel, TProp>> propExpr,
            Func<TModel, string> formatFunc = null,
            Func<TModel, string> styleFunc = null,
            string headerName = null,
            Func<TProp, string> headerStyleFunc = null)
        {
            var propName = propExpr.GetMemberName();
            if (string.IsNullOrEmpty(propName)) return this;

            var pi = typeof(TModel).GetProperty(propName);
            var displayNameAttr = pi.GetAttribute<DisplayNameAttribute>();
            var displayName = displayNameAttr?.DisplayName.SetWhenNullOrEmpty(pi.Name);
            _tbl.HeadEles.Add(new HtmlElement("th").Attr("data-prop", pi.Name).Text(displayName));

            var fmtFunc = formatFunc ?? (m => m.ToString());
            for (var i = 0; i < _models.Count; i++)
            {
                var displayValue = fmtFunc(_models[i]);
                var style = styleFunc?.Invoke(_models[i]);
                var ele = new HtmlElement("td").Text(displayValue)
                    .AttrUnlessNullOrEmpty("class", style);
                _tbl.BodyEles[i].Add(ele);
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProp">属性类型</typeparam>
        /// <param name="propExpr">属性表达式</param>
        /// <param name="formatFunc">格式函数</param>
        /// <param name="styleFunc">样式表达式函数</param>
        /// <param name="inputName">控件名称</param>
        /// <param name="headerStyleFunc">表格列头样式</param>
        /// <returns></returns>
        public HtmlTable<TModel> AddCheckBox<TProp>(Expression<Func<TModel, TProp>> propExpr,
            Func<TModel, string> formatFunc = null,
            Func<TModel, string> styleFunc = null,
            string inputName = null,
            Func<TProp, string> headerStyleFunc = null)
        {
            var propName = propExpr.GetMemberName();
            if (string.IsNullOrEmpty(propName)) return this;

            var iName = inputName.SetWhenNullOrEmpty("ids");

            var pi = typeof(TModel).GetProperty(propName);
            _tbl.HeadEles.Add(new HtmlElement("th").Attr("data-prop", pi.Name).Append(
                new HtmlElement("input").Attr("type", "checkbox").Attr("onclick", "switchCheckAll(this, '" + iName + "')")));

            var propFunc = propExpr.Compile();
            var fmtFunc = formatFunc ?? (m => propFunc(m).ToString());
            for (var i = 0; i < _models.Count; i++)
            {
                var displayValue = fmtFunc(_models[i]);
                var style = styleFunc?.Invoke(_models[i]);
                var ele = new HtmlElement("td")
                    .AttrUnlessNullOrEmpty("class", style)
                    .Append(new HtmlElement("input").Attr("type", "checkbox").Attr("name", iName).Attr("value", displayValue));
                _tbl.BodyEles[i].Add(ele);
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProp">属性类型</typeparam>
        /// <param name="propExpr">属性表达式</param>
        /// <param name="valueFunc">属性值函数</param>
        /// <param name="styleFunc">样式表达式函数</param>
        /// <param name="headerName">表格列头名称</param>
        /// <param name="headerStyleFunc">表格列头样式</param>
        /// <returns></returns>
        public HtmlTable<TModel> AddAction<TProp>(Expression<Func<TModel, TProp>> propExpr,
            Func<TModel, string> valueFunc = null,
            Func<TModel, string> styleFunc = null,
            string headerName = null,
            Func<TProp, string> headerStyleFunc = null)
        {
            var propName = propExpr.GetMemberName();
            if (string.IsNullOrEmpty(propName)) return this;

            var pi = typeof(TModel).GetProperty(propName);
            _tbl.HeadEles.Add(new HtmlElement("th").Attr("data-prop", pi.Name).Text(headerName.SetWhenNullOrEmpty("操作")));

            var fmtFunc = valueFunc ?? (m => m.ToString());
            for (var i = 0; i < _models.Count; i++)
            {
                var displayValue = fmtFunc(_models[i]);
                var style = styleFunc?.Invoke(_models[i]);
                var ele = new HtmlElement("td")
                    .AttrUnlessNullOrEmpty("class", style)
                    .Html(displayValue);
                _tbl.BodyEles[i].Add(ele);
            }
            return this;
        }

        private HtmlElement _tblElement;

        /// <summary>
        /// 设置表格元素
        /// </summary>
        /// <param name="tblElement"></param>
        /// <returns></returns>
        public HtmlTable<TModel> Table(HtmlElement tblElement)
        {
            _tblElement = tblElement;
            return this;
        }

        public override string ToString()
        {
            var tbl = _tblElement ?? new HtmlElement("table")
                .AttrUnlessNullOrEmpty("class", _tblClass);

            var thead = new HtmlElement("thead").Append(new HtmlElement("tr").Attr("role", "row").Append(_tbl.HeadEles));

            var tbody = new HtmlElement("tbody");
            var i = 1;
            foreach (var bodyEle in _tbl.BodyEles)
            {
                tbody.Append(new HtmlElement("tr").Attr("role", "row").Attr("class", i == 1 ? "even" : "odd").Append(bodyEle));
                i = 1 - i;
            }

            tbl.Append(thead).Append(tbody);

            return tbl.ToString();
        }

        private class ContentTable
        {
            public ContentTable(int bodyCount)
            {
                BodyEles = new IList<HtmlElement>[bodyCount];
                for (var i = 0; i < BodyEles.Length; i++)
                {
                    BodyEles[i] = new List<HtmlElement>();
                }
            }

            /// <summary>
            /// 读取或者设置表头元素
            /// </summary>
            /// <remark></remark>
            [DisplayName("表头元素")]
            public IList<HtmlElement> HeadEles => _headEles ?? (_headEles = new List<HtmlElement>());

            /// <summary>
            /// 头部元素
            /// </summary>
            private IList<HtmlElement> _headEles;

            /// <summary>
            /// 读取或者设置表尾元素
            /// </summary>
            /// <remark></remark>
            [DisplayName("表尾元素")]
            public IList<HtmlElement> FootEles
            {
                get { return _footEles ?? (_footEles = new List<HtmlElement>()); }
                set { _footEles = value; }
            }

            /// <summary>
            /// 表尾元素
            /// </summary>
            private IList<HtmlElement> _footEles;

            /// <summary>
            /// 读取或者设置表体元素
            /// </summary>
            /// <remark></remark>
            [DisplayName("表体元素")]
            public IList<HtmlElement>[] BodyEles { get; set; }
        }



    }
}