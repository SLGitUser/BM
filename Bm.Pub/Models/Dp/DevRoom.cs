using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 楼盘户型
    /// </summary>
    [DisplayName("楼盘户型")]
    public sealed class DevRoom : IId, IStamp
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
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }


        /// <summary>
        /// 户型名称
        /// </summary>
        [DisplayName("户型名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 楼盘编号
        /// </summary>
        [DisplayName("楼盘编号")]
        [StringLength(20)]
        [Required]
        public string DpNo { get; set; }

        /// <summary>
        /// 户型名称
        /// </summary>
        [DisplayName("厅室")]
        [StringLength(50)]
        [Required]
        public string Room { get; set; }

        /// <summary>
        /// 参考价
        /// </summary>
        [DisplayName("参考价")] 
        [Required]
        public int?  TotalPrice { get; set; }


        /// <summary>
        /// 面积
        /// </summary>
        [DisplayName("面积")]
        [Required]
        public string Area { get; set; }


        /// <summary>
        /// 位置图片
        /// </summary>
        [DisplayName("位置图片")]
        [Required]
        public string Pic { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [DisplayName("排序号")]
        [StringLength(20)]
        [Required]
        public int? OrderNo { get; set; }


    }
}
