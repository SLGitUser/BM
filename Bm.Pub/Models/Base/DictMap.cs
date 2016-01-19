using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using com.senlang.Sdip.Data;

namespace Bm.Models.Base
{
    /// <summary>
    /// 字典应用规则
    /// </summary>
    [DisplayName("字典应用规则")]
    public sealed class DictMap :IId, IStamp
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
        /// 读取或者设置资源名称
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("资源名称")]
        [StringLength(100)]
        [Required]
        public string TypeName { get; set; }

        /// <summary>
        /// 读取或者设置属性名称
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("属性名称")]
        [StringLength(50)]
        [Required]
        public string PropName { get; set; }

        /// <summary>
        /// 读取或者设置字典名称
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("字典名称")]
        [StringLength(50)]
        [Required]
        public string DictName { get; set; }

        /// <summary>
        /// 读取或者设置取值方式
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("取值方式")]
        [StringLength(20)]
        [Required]
        public string DictValue { get; set; }
    }
}
