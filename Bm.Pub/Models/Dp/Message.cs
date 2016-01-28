using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 信息
    /// </summary>
    [DisplayName("信息")]
    public sealed class Message : IId, IStamp
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
        /// 读取或者设置用户编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("用户编号")]
        [StringLength(49)]
        [Required]
        public string UserNo { get; set; }

        /// <summary>
        /// 读取或者设置消息类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("消息类型")]
        [StringLength(49)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 读取或者设置时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("时间")]
        [Required]
        public DateTime At { get; set; }

        /// <summary>
        /// 读取或者设置标题
        /// </summary>
        /// <remark></remark>
        [DisplayName("标题")]
        [StringLength(49)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 读取或者设置内容
        /// </summary>
        /// <remark></remark>
        [DisplayName("内容")]
        [StringLength(500)]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 读取或者设置是否阅读
        /// </summary>
        /// <remark></remark>
        [DisplayName("是否阅读")]
        [Required]
        public bool IsRead { get; set; }

        /// <summary>
        /// 读取或者设置阅读时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("阅读时间")]
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// 读取或者设置是否删除
        /// </summary>
        /// <remark></remark>
        [DisplayName("是否删除")]
        [Required]
        public bool IsDel { get; set; }
    }
}
