using System.Web.Http;
using Bm.Modules.Orm;

namespace Bm.Controllers
{
    public class NoController : ApiController
    {
        // GET: api/No/5
        public IHttpActionResult Get(string tbl = null, string no = null, string name = null, string noVal = null)
        {
            if (string.IsNullOrEmpty(tbl)
                || string.IsNullOrEmpty(no)
                || string.IsNullOrEmpty(noVal)
                || string.IsNullOrEmpty(name)) return Ok(string.Empty);

            string sql = $"SELECT `{@name}` FROM `{@tbl}` WHERE `{@no}` = '{@noVal}'";
            var result = ConnectionManager.ExecuteScalar<string>(sql);
            return Ok(result);
        }
    }
}
