using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 推广合作
    /// </summary>
    [DisplayName("推广合作")]
    public sealed class Promotion : IId, IStamp
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
        /// 读取或者设置项目编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("项目编号")]
        [StringLength(20)]
        [Required]
        public string DpNo { get; set; }
        
        /// <summary>
        /// 读取或者设置名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置有效期起始
        /// </summary>
        /// <remark></remark>
        [DisplayName("有效期起始")]
        public DateTime BeginAt { get; set; }

        /// <summary>
        /// 读取或者设置有效期截止
        /// </summary>
        /// <remark></remark>
        [DisplayName("有效期截止")]
        public DateTime EndAt { get; set; }

        /// <summary>
        /// 读取或者设置佣金
        /// </summary>
        /// <remark></remark>
        [DisplayName("佣金")]
        public int Brokerage { get; set; }

        /// <summary>
        /// 读取或者设置推荐人数
        /// </summary>
        /// <remark></remark>
        [DisplayName("推荐人数")]
        public int TuijianCount { get; set; }

        /// <summary>
        /// 读取或者设置推荐有效人数
        /// </summary>
        /// <remark></remark>
        [DisplayName("推荐有效人数")]
        public int TuijianValidCount { get; set; }

        /// <summary>
        /// 读取或者设置已到访人数
        /// </summary>
        /// <remark></remark>
        [DisplayName("已到访人数")]
        public int DaofangCount { get; set; }

        /// <summary>
        /// 读取或者设置已认筹人数
        /// </summary>
        /// <remark></remark>
        [DisplayName("已认筹人数")]
        public int RenchouCount { get; set; }

        /// <summary>
        /// 读取或者设置已成交人数
        /// </summary>
        /// <remark></remark>
        [DisplayName("已成交人数")]
        public int ChengjiaoCount { get; set; }

        /// <summary>
        /// 读取或者设置统计更新时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("统计更新时间")]
        public DateTime SummaryAt { get; set; }

    }
}
