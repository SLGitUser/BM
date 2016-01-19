using System.ComponentModel;

namespace Bm.Models.Base
{
    /// <summary>
    /// 账户与角色关系视图
    /// </summary>
    [DisplayName("账户与角色关系视图")]
    public sealed class AccountRoleRefView : AccountRoleRef
    {
        /// <summary>
        /// 读取或者设置账户名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("账户名称")]
        public string AccountName { get; set; }

        /// <summary>
        /// 读取或者设置角色名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("角色名称")]
        public string RoleName { get; set; }
    }
}