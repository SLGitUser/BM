using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Bm.Modules.Helper;

namespace Bm.Modules.Html
{
    public static partial class HtmlHelper
    {
        public static MvcHtmlString InputFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null)
        {

            /*
            <div class="form-group col-sm-12 col-lg-4 col-md-6">
                    <label for="No" class="col-sm-3 control-label">编号</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" id="No" name="No" placeholder="">
                        <p class="help-block">@Html.ValidationMessageFor(m => m.No)</p>
                    </div>
                </div>
            */

            var isRequired = IsRequired(expression);

            var labelDict = new { @class = "control-label col-sm-3" + (isRequired ? " required" : string.Empty) };

            var input1Dict = new Dictionary<string, object>
            {
                {"class", "form-control " + GetCssType(typeof (TValue))}
            };
            if (isRequired)
            {
                input1Dict.Add("required", "required");
            }
            var inputDict = ObjectHelper.CombineKeyValue(input1Dict, htmlAttributes);

            var builder = new StringBuilder();
            builder.Append(@"<div class=""form-group col-sm-12 col-md-6 col-lg-4"">");
            builder.Append(html.LabelFor(expression, labelDict));
            builder.Append(@"<div class=""col-sm-9"">");
            builder.Append(html.EditorFor(expression, new { htmlAttributes = inputDict }));
            builder.Append(html.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
            builder.Append(@"</div>");
            builder.Append(@"</div>");
            return MvcHtmlString.Create(builder.ToString());
        }

        private static bool IsRequired<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var propName = expression.GetMemberName();
            if (string.IsNullOrEmpty(propName)) return false;
            var pi = typeof(TModel).GetProperty(propName);
            return pi?.GetAttribute<RequiredAttribute>() != null;
        }

        /// <summary>
        /// 获得类型的CSS样式
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetCssType(Type type)
        {
            string value;
            return Types.TryGetValue(type, out value) ? value : null;
        }

        private static readonly IDictionary<Type, string> Types = new Dictionary<Type, string>
        {
            { typeof(long),  "data-int"}, { typeof(long?),  "data-int"},
            { typeof(short),  "data-int"}, { typeof(short?),  "data-int"},
            { typeof(int),  "data-int"}, { typeof(int?),  "data-int"},
            { typeof(decimal),  "data-dec"}, { typeof(decimal?),  "data-dec"},
            { typeof(string),  "data-str"},
            { typeof(DateTime),  "data-date"}, { typeof(DateTime?),  "data-date"},
            { typeof(bool),  "data-bool"}, { typeof(bool?),  "data-bool"}
        };
    }
}
