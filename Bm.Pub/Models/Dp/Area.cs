using Bm.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 区域
    /// </summary>
    [DisplayName("区域")]
    public sealed class Area : IId, IStamp
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
        /// 区域类型缩写
        /// </summary>
        /// <remark></remark>
        [DisplayName("类型缩写")]
        [StringLength(20)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 区域类型编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("区域编号")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }

        /// <summary>
        /// 区域类型名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("区域名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 区域类型名称简写
        /// </summary>
        /// <remark></remark>
        [DisplayName("区域名称简写")]
        [StringLength(50)]
        public string ShortName { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        /// <remark></remark>
        [DisplayName("级别")]
        public int? Live { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("排序编号")]
        [Required]
        public int? OrderNo { get; set; }

          }
}
