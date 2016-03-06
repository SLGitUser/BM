using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Modules.Orm.Annotation;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 置业顾问
    /// </summary>
    [DisplayName("置业顾问")]
    public sealed class PropertyAdvisor : IId, IStamp
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

        #region Implementation of IUpdateStamp

        /// <summary>
        /// 读取或者设置记录更新人
        /// </summary>
        /// <value>
        /// 记录更新人
        /// </value>
        /// <remarks>
        /// 建议使用操作人的账户名，为空时表示记录无更新
        /// </remarks>
        [DisplayName("记录最后修改人")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 读取或者设置记录更新时间
        /// </summary>
        /// <value>
        /// 记录更新时间
        /// </value>
        /// <remarks>
        /// 为空时表示记录无更新
        /// </remarks>
        [DisplayName("记录最后修改时间")]
        public DateTime? UpdatedAt { get; set; }

        #endregion
        
        /// <summary>
        /// 读取或者设置名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("编号")]
        [StringLength(36)]
        public string No { get; set; }

        /// <summary>
        /// 读取或者设置手机号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("手机号码")]
        [StringLength(11)]
        [Required]
        public string MobileNo { get; set; }

        /// <summary>
        /// 读取或者设置职位
        /// </summary>
        /// <remark></remark>
        [DisplayName("职位")]
        [StringLength(20)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 读取或者设置楼盘编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("楼盘")]
        [StringLength(50)]
        [Required]
        public string ProjectNo { get; set; }

        /// <summary>
        /// 账户信息
        /// </summary>
        [IgnoreMapping]
        public Account Account { get; set; }

    }
}
