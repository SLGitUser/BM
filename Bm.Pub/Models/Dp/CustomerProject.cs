using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 客户关注楼盘
    /// </summary>
    [DisplayName("客户关注楼盘")]
    [Table("dp_developer")]
    public sealed class CustomerProject : IId, IStamp
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
        /// 读取或者设置客户关系编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("客户关系编号")]
        [StringLength(49)]
        [Required]
        public string CustomerNo { get; set; }

        /// <summary>
        /// 读取或者设置项目编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("项目编号")]
        [StringLength(49)]
        [Required]
        public string ProjectNo { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [DisplayName("序号")]
        [Required]
        public int OrderNo { get; set; }

        /// <summary>
        /// 读取或者设置时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("时间")]
        [Required]
        public DateTime At { get; set; }
    }
}
