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
            SelectList selectList,
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
            builder.Append(html.DropDownListFor(expression, selectList, inputDict));
            builder.Append(html.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
            builder.Append(@"</div>");
            builder.Append(@"</div>");
            return MvcHtmlString.Create(builder.ToString());
        }

    }
}
