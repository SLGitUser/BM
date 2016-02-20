using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 置业顾问
    /// </summary>
    [DisplayName("置业顾问")]
    public sealed class PropertyAdvisor : IId, IStamp
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
        /// 读取或者设置名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("编号")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }

        /// <summary>
        /// 读取或者设置性别
        /// </summary>
        /// <remark></remark>
        [DisplayName("性别")]
        [StringLength(5)]
        public string Gender { get; set; }

        /// <summary>
        /// 读取或者设置手机号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("手机号码")]
        [StringLength(12)]
        [Required]
        public string Mobile { get; set; }

        /// <summary>
        /// 读取或者设置电子邮箱
        /// </summary>
        /// <remark></remark>
        [DisplayName("电子邮箱")]
        [StringLength(49)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 读取或者设置地区
        /// </summary>
        /// <remark></remark>
        [DisplayName("地区")]
        [StringLength(20)]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// 读取或者设置地区代码
        /// </summary>
        /// <remark></remark>
        [DisplayName("地区代码")]
        [StringLength(20)]
        [Required]
        public string CityNo { get; set; }

        /// <summary>
        /// 读取或者设置经纪公司
        /// </summary>
        /// <remark></remark>
        [DisplayName("经纪公司")]
        [StringLength(50)]
        public string Firm { get; set; }

        /// <summary>
        /// 读取或者设置经纪公司代码
        /// </summary>
        /// <remark></remark>
        [DisplayName("经纪公司代码")]
        [StringLength(50)]
        public string FirmNo { get; set; }

        /// <summary>
        /// 读取或者设置个人简介
        /// </summary>
        /// <remark></remark>
        [DisplayName("个人简介")]
        [StringLength(200)]
        public string Intro { get; set; }

        /// <summary>
        /// 读取或者设置注册时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("注册时间")]
        [StringLength(50)]
        [Required]
        public DateTime RegAt { get; set; }

        /// <summary>
        /// 读取或者设置推荐人
        /// </summary>
        /// <remark></remark>
        [DisplayName("推荐人")]
        [StringLength(50)]
        public string Referral { get; set; }

        /// <summary>
        /// 读取或者设置头像
        /// </summary>
        /// <remark></remark>
        [DisplayName("头像")]
        [StringLength(50)]
        [Required]
        public string Pic { get; set; }

    }
}
