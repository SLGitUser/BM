using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;
using Bm.Services.Common;

namespace Bm.Models.Base
{
    /// <summary>
    /// 短信验证码
    /// </summary>
    [DisplayName("验证码")]
    public sealed class VerificationCode : IId, ICreateStamp
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

        #region Implementation of ICreateStamp

        /// <summary>
        /// 读取或者设置记录创建人
        /// </summary>
        /// <value>
        /// 记录创建人
        /// </value>
        /// <remarks>
        /// 建议使用操作人的账户名
        /// </remarks>
        [DisplayName("记录创建人")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 读取或者设置记录创建时间
        /// </summary>
        /// <value>
        /// 记录创建时间
        /// </value>
        /// <remarks>
        /// 建议使用服务器时间，不要使用默认空值
        /// </remarks>
        [DisplayName("记录创建时间")]
        public DateTime CreatedAt { get; set; }

        #endregion

        /// <summary>
        /// 读取或者设置类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("类型")]
        [StringLength(20)]
        public AlidayuService.CodeType CodeType { get; set; }

        /// <summary>
        /// 读取或者设置账户号
        /// </summary>
        /// <remark></remark>
        [DisplayName("账户号")]
        [StringLength(32)]
        public string AccountNo { get; set; }

        /// <summary>
        /// 读取或者设置手机号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("手机号码")]
        [StringLength(11)]
        public string PhoneNo { get; set; }

        /// <summary>
        /// 读取或者设置随机编码
        /// </summary>
        /// <remark></remark>
        [DisplayName("随机编码")]
        [StringLength(32)]
        public string Uuid { get; set; }
        
        /// <summary>
        /// 读取或者设置代码
        /// </summary>
        /// <remark></remark>
        [DisplayName("代码")]
        [StringLength(5)]
        public string Code { get; set; }

        /// <summary>
        /// 读取或者设置阿里流水号
        /// </summary>
        /// <remark></remark>
        [DisplayName("阿里流水号")]
        [StringLength(50)]
        public string AliOrderNo { get; set; }
        
        /// <summary>
        /// 读取或者设置过期时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("过期时间")]
        public DateTime ExpiredAt { get; set; }

        /// <summary>
        /// 读取或者设置验证时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("验证时间")]
        public DateTime? VerifyAt { get; set; }
    }
}
