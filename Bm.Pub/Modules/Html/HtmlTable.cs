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
        /// <typeparam name="TProp">��������</typeparam>
        /// <param name="propExpr">���Ա��ʽ</param>
        /// <param name="formatFunc">��ʽ����</param>
        /// <param name="styleFunc">��ʽ���ʽ����</param>
        /// <param name="headerName">�����ͷ����</param>
        /// <param name="headerStyleFunc">�����ͷ��ʽ</param>
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
        /// <typeparam name="TProp">��������</typeparam>
        /// <param name="propExpr">���Ա��ʽ</param>
        /// <param name="formatFunc">��ʽ����</param>
        /// <param name="styleFunc">��ʽ���ʽ����</param>
        /// <param name="headerName">�����ͷ����</param>
        /// <param name="headerStyleFunc">�����ͷ��ʽ</param>
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
        /// <typeparam name="TProp">��������</typeparam>
        /// <param name="propExpr">���Ա��ʽ</param>
        /// <param name="formatFunc">��ʽ����</param>
        /// <param name="styleFunc">��ʽ���ʽ����</param>
        /// <param name="inputName">�ؼ�����</param>
        /// <param name="headerStyleFunc">�����ͷ��ʽ</param>
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
        /// <typeparam name="TProp">��������</typeparam>
        /// <param name="propExpr">���Ա��ʽ</param>
        /// <param name="valueFunc">����ֵ����</param>
        /// <param name="styleFunc">��ʽ���ʽ����</param>
        /// <param name="headerName">�����ͷ����</param>
        /// <param name="headerStyleFunc">�����ͷ��ʽ</param>
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
            _tbl.HeadEles.Add(new HtmlElement("th").Attr("data-prop", pi.Name).Text(headerName.SetWhenNullOrEmpty("����")));

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
        /// ���ñ��Ԫ��
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
            /// ��ȡ�������ñ�ͷԪ��
            /// </summary>
            /// <remark></remark>
            [DisplayName("��ͷԪ��")]
            public IList<HtmlElement> HeadEles => _headEles ?? (_headEles = new List<HtmlElement>());

            /// <summary>
            /// ͷ��Ԫ��
            /// </summary>
            private IList<HtmlElement> _headEles;

            /// <summary>
            /// ��ȡ�������ñ�βԪ��
            /// </summary>
            /// <remark></remark>
            [DisplayName("��βԪ��")]
            public IList<HtmlElement> FootEles
            {
                get { return _footEles ?? (_footEles = new List<HtmlElement>()); }
                set { _footEles = value; }
            }

            /// <summary>
            /// ��βԪ��
            /// </summary>
            private IList<HtmlElement> _footEles;

            /// <summary>
            /// ��ȡ�������ñ���Ԫ��
            /// </summary>
            /// <remark></remark>
            [DisplayName("����Ԫ��")]
            public IList<HtmlElement>[] BodyEles { get; set; }
        }



    }
}