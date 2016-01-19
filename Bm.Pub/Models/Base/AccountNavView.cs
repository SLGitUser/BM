using System.ComponentModel;

namespace Bm.Models.Base
{
    /// <summary>
    /// 账户与导航关系
    /// </summary>
    [DisplayName("账户与导航关系")]
    public class AccountNavView
    {
        /// <summary>
        /// 读取或者设置账号
        /// </summary>
        /// <remark></remark>
        [DisplayName("账号")]
        public string AccountNo { get; set; }

        /// <summary>
        /// 读取或者设置导航
        /// </summary>
        /// <remark></remark>
        [DisplayName("导航")]
        public long NavId { get; set; }

        /// <summary>
        /// 读取或者设置操作URL
        /// </summary>
        /// <remark></remark>
        [DisplayName("操作URL")]
        public string ActionUrl { get; set; }

        /// <summary>
        /// 读取或者设置操作名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("操作名称")]
        public string ActionName { get; set; }
    }
}
