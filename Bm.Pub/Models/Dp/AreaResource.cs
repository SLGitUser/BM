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
    /// 区域资源
    /// </summary>
    [DisplayName("区域资源")]
    public class AreaResource : IId, IStamp
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
        /// 区域资源编号
        /// </summary>

        [DisplayName("区域资源编号")]
        [StringLength(20)]
        [Required]
        public string CityNo { get; set;}

        /// <summary>
        /// 区域资源类型
        /// </summary>
        [DisplayName("区域资源类型")]
        [StringLength(20)]
        [Required]
        public string Type { get; set; }


        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }

        /// <summary>
        /// 区域资源名称
        /// </summary>
        [DisplayName("区域资源名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// 区域资源名称缩写
        /// </summary>
        [DisplayName("区域资源名称缩写")]
        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [DisplayName("经度")]
        [StringLength(20)]
        [Required]
        public string Longitude { get; set; }


        /// <summary>
        /// 维度
        /// </summary>
        [DisplayName("维度")]
        [StringLength(20)]
        [Required]
        public string Latitude { get; set; }


        /// <summary>
        /// 排序号
        /// </summary>
        [DisplayName("排序号")]
        [StringLength(20)]
        [Required]
        public int? OrderNo { get; set; }

         
    }
}
