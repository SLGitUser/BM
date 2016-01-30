using System.Collections.Generic;
using System.Linq;

namespace Bm.Modules.Html
{
    public static class HtmlTableHelper
    {
        public static HtmlTable<TModel> ToHtmlTable<TModel>(this TModel[] models, string tblClass = null)
        {
            return ToHtmlTable(models.ToList(), tblClass);
        }

        public static HtmlTable<TModel> ToHtmlTable<TModel>(this ICollection<TModel> models, string tblClass = null)
        {
            return ToHtmlTable(models.ToList(), tblClass);
        }

        public static HtmlTable<TModel> ToHtmlTable<TModel>(this IList<TModel> models, string tblClass = null)
        {
            return ToHtmlTable(models.ToList(), tblClass);
        }

        public static HtmlTable<TModel> ToHtmlTable<TModel>(this List<TModel> models, string tblClass = null)
        {
            return new HtmlTable<TModel>(models, tblClass);
        }
    }
}
