using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Base
{
    /// <summary>
    /// 账户登录凭证
    /// </summary>
    [DisplayName("账户登录凭证")]
    public class AccountToken : IId
    {

        #region Implementation of IId

        /// <summary>
        /// 读取或者设置记录序号
        /// </summary>
        /// <value>
        /// 记录序号
        /// </value>
        /// <remarks>
        /// 建议存储时使用无符号类型
        /// </remarks>
        [DisplayName("记录序号")]
        public long Id { get; set; }

        #endregion

        [Required]
        [StringLength(32)]
        [DisplayName(@"系统名称")]
        public string AppName { get; set; }


        [Required]
        [StringLength(32)]
        [DisplayName(@"客户端名称")]
        public string ClientAppName { get; set; }

        [Required]
        [StringLength(32)]
        [DisplayName(@"账户名")]
        public string AccountName { get; set; }

        [Required]
        [StringLength(128)]
        [DisplayName(@"凭证随机码")]
        public string Token { get; set; }

        [Required]
        [DisplayName(@"生成时间")]
        public DateTime GenAt { get; set; }

        [DisplayName(@"访问时间")]
        public DateTime? VisitAt { get; set; }

    }
}
