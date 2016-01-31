using System.Text;
using System.Web.Mvc;

namespace Bm.Modules.Html
{
    public static partial class HtmlHelper
    {
        /// <summary>
        /// 导航
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="title">一级标题</param>
        /// <param name="subTitle">二级标题</param>
        /// <param name="moduleUrl">模块URL</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="moduleIcon">模块ICON</param>
        /// <param name="actionName">操作名称</param>
        /// <returns></returns>
        public static MvcHtmlString Breadcrumb<TModel>(this HtmlHelper<TModel> html,
            string title, string subTitle,
            string moduleUrl, string moduleName, string moduleIcon,
            string actionName)
        {
            /*
    <h1>开发商信息<small></small></h1>
    <ol class="breadcrumb">
        <li><a href="/"><i class="fa fa-dashboard"></i>首页</a></li>
        <li><a href="@Url.Action("Index")"><i class="fa fa-book"></i>开发商</a></li>
        <li class="active">新增开发商信息</li>
    </ol>
            */
            var builder = new StringBuilder();
            builder.Append($"<h1>{title}<small>{subTitle}</small></h1>");
            builder.Append("<ol class=\"breadcrumb\">");
            builder.Append("<li><a href=\"/\"><i class=\"fa fa-dashboard\"></i>首页</a></li>");
            builder.Append($"<li><a href=\"{moduleUrl}\"><i class=\"fa {moduleIcon}\"></i>{moduleName}</a></li>");
            builder.Append($"<li class=\"active\">{actionName}</li>");
            builder.Append("</ol>");
            return MvcHtmlString.Create(builder.ToString());
        }

    }
}
