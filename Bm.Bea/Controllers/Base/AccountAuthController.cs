using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Bm.Extensions;
using Bm.Services.Base;

namespace Bm.Controllers.Base
{
    
    public class AccountAuthController : BaseApiController
    {
        /// <summary>
        /// 验证账户登录是否正确
        /// </summary>
        /// <returns></returns>
        [Route("api/base_account_auth")]
        public IHttpActionResult Get()
        {
            var m = GetQueryString("m");
            var p = GetQueryString("p");
            var service = new AccountService();
            var r = service.Auth(m, p);
            if (r.HasError) return Ok(r);

            var a = r.Value?.No;
            if (string.IsNullOrEmpty(a)) return Ok(r.Error("账号为空"));

            var account = service.GetAccount(a);
            if (account == null) return Ok(r.Error("账户为空"));

            var roleNo = account.RoleRefs.Select(n => n.RoleNo).Distinct().FirstOrDefault();
            if (string.IsNullOrEmpty(roleNo)) return Ok(r.Error("角色为空"));

            if (!RoleDirMap.ContainsKey(roleNo)) return Ok(r.Error("当前角色不支持认证"));
            
            return Ok(r, n => new
            {
                No = n?.No ?? "",
                Name = n?.Name ?? "",
                Type = RoleDirMap[roleNo]
            });
        }

        private static readonly IDictionary<string, string> RoleDirMap = new Dictionary<string, string>
        {
            { "Broker", "_br"},
            { "PropertyAdvisor", "_pa"},
            { "PropertyManager", "_pm"},
        }; 
    }
}
