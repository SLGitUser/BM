using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using com.senlang.Sdip.Data;

namespace Bm.Models.Base
{
    /// <summary>
    /// 字典
    /// </summary>
    [DisplayName("字典")]
    public sealed class Dict : IId, IStamp
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


        [DisplayName("标准名")]
        [Required]
        [StringLength(50)]
        public string StandardNo { get; set; }

        [DisplayName("字典名")]
        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [DisplayName("数值键名")]
        [Required]
        [StringLength(100)]
        public string StateCode { get; set; }

        [DisplayName("字符键名")]
        [Required]
        [StringLength(100)]
        public string NoCode { get; set; }

        [DisplayName("键值")]
        [Required]
        [StringLength(100)]
        public string Value { get; set; }

        [DisplayName("扩展值")]
        [Required]
        public string Extra { get; set; }

        [DisplayName("排序序号")]
        [Required]
        public int OrderId { get; set; }
    }
}
