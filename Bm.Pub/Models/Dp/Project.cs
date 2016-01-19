using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using com.senlang.Sdip.Data;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 开发项目
    /// </summary>
    [DisplayName("开发项目")]
    public sealed class Project: IId, IStamp
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
        /// 读取或者设置开发商编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("开发商编号")]
        [StringLength(20)]
        [Required]
        public string OwnerNo { get; set; }
        
        /// <summary>
        /// 读取或者设置编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("编号")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }
        
        /// <summary>
        /// 读取或者设置名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置板块
        /// </summary>
        /// <remark></remark>
        [DisplayName("板块")]
        [StringLength(50)]
        public string BizGrp { get; set; }

        /// <summary>
        /// 读取或者设置类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("类型")]
        [StringLength(50)]
        public string BldType { get; set; }

        /// <summary>
        /// 读取或者设置销售状态
        /// </summary>
        /// <remark></remark>
        [DisplayName("销售状态")]
        [StringLength(20)]
        [Required]
        public string SaleStatus { get; set; }

        /// <summary>
        /// 读取或者设置均价
        /// </summary>
        /// <remark></remark>
        [DisplayName("均价")]
        public int AvgPrice { get; set; }


        /// <summary>
        /// 读取或者设置咨询电话
        /// </summary>
        /// <remark></remark>
        [DisplayName("咨询电话")]
        [StringLength(50)]
        public string SalesTel { get; set; }

        /// <summary>
        /// 读取或者设置地址
        /// </summary>
        /// <remark></remark>
        [DisplayName("地址")]
        [StringLength(50)]
        public string Address { get; set; }

        /// <summary>
        /// 读取或者设置经度
        /// </summary>
        /// <remark></remark>
        [DisplayName("经度")]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// 读取或者设置纬度
        /// </summary>
        /// <remark></remark>
        [DisplayName("纬度")]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// 读取或者设置位置图片
        /// </summary>
        /// <remark></remark>
        [DisplayName("位置图片")]
        [StringLength(50)]
        public string AddrPic { get; set; }

        /// <summary>
        /// 读取或者设置推客认证类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("推客认证类型")]
        public string AuthType { get; set; }
    }
}
