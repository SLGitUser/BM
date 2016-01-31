using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Base
{
    /// <summary>
    /// 角色
    /// </summary>
    [DisplayName("角色")]
    public sealed class Role : IStamp
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
        /// 读取或者设置编码
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("编码")]
        [StringLength(20)]
        public string No { get; set; }

        /// <summary>
        /// 读取或者设置角色名称
        /// </summary>
        [DisplayName("角色名称")]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置角色简称
        /// </summary>
        [DisplayName("角色简称")]
        [StringLength(30)]
        public string ShortName { get; set; }
        
        /// <summary>
        /// 读取或者设置角色描述
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("角色描述")]
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// 读取或者设置上级角色
        /// </summary>
        /// <remark></remark>
        [DisplayName("上级角色")]
        public string ParentNo { get; set; }

        /// <summary>
        /// 读取或者设置排序序号
        /// </summary>
        /// <remark></remark>
        [DisplayName("排序序号")]
        public long OrderId { get; set; }

    }
}