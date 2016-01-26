using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Base
{
    /// <summary>
    /// 功能导航
    /// </summary>
    [DisplayName("功能导航")]
    public sealed class Nav : IId, ICreateStamp
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
        /// 读取或者设置是否分类
        /// </summary>
        /// <remark></remark>
        [DisplayName("是否分类")]
        public bool IsCategory { get; set; }

        /// <summary>
        /// 读取或者设置名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置简称
        /// </summary>
        /// <remark></remark>
        [DisplayName("简称")]
        [StringLength(20)]
        public string ShortName { get; set; }

        /// <summary>
        /// 读取或者设置资源定位符
        /// </summary>
        /// <remark></remark>
        [DisplayName("资源定位符")]
        public string Uri { get; set; }
    }
}