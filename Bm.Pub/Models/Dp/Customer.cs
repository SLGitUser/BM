using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;
using System.Collections.Generic;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 客户关系编号
    /// </summary>
    [DisplayName("客户关系编号")]
    public sealed class Customer : IId, IStamp
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
        public string No { get; set; }

        /// <summary>
        /// 读取或者设置地区代码
        /// </summary>
        /// <remark></remark>
        [DisplayName("地区代码")]
        [StringLength(50)]
        [Required]
        public string CityNo { get; set; }

        /// <summary>
        /// 读取或者设置姓名
        /// </summary>
        /// <remark></remark>
        [DisplayName("姓名")]
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置姓名拼音
        /// </summary>
        /// <remark></remark>
        [DisplayName("姓名拼音")]
        [StringLength(40)]
        [Required]
        public string Pinyin { get; set; }

        /// <summary>
        /// 读取或者设置性别
        /// </summary>
        /// <remark></remark>
        [DisplayName("性别")]
        [StringLength(5)]
        [Required]
        public string Gender { get; set; }

        /// <summary>
        /// 读取或者设置证件类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("证件类型")]
        [StringLength(49)]
        public string CardType { get; set; }

        /// <summary>
        /// 读取或者设置证件号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("证件号码")]
        [StringLength(30)]
        public string CardNo { get; set; }

        /// <summary>
        /// 读取或者设置手机号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("手机号码")]
        [StringLength(12)]
        public string Mobile { get; set; }

        /// <summary>
        /// 读取或者设置电子邮箱
        /// </summary>
        /// <remark></remark>
        [DisplayName("电子邮箱")]
        [StringLength(49)]
        public string Email { get; set; }

        /// <summary>
        /// 读取或者设置头像
        /// </summary>
        /// <remark></remark>
        [DisplayName("头像")]
        [StringLength(50)]
        public string Pic { get; set; }

        /// <summary>
        /// 读取或者设置经纪人编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("经纪人编号")]
        [StringLength(49)]
        [Required]
        public string BrokerNo { get; set; }

        /// <summary>
        /// 读取或者设置注册日期
        /// </summary>
        /// <remark></remark>
        [DisplayName("注册日期")]
        [Required]
        public DateTime RegAt { get; set; }

        /// <summary>
        /// 读取或者设置保护过期日期
        /// </summary>
        /// <remark></remark>
        [DisplayName("保护过期日期")]
        [Required]
        public DateTime ExpiredAt { get; set; }

        /// <summary>
        /// 读取或者设置客户等级
        /// </summary>
        /// <remark></remark>
        [DisplayName("客户等级")]
        [StringLength(1)]
        [Required]
        public string Level { get; set; }

        /// <summary>
        /// 读取或者设置客户备注
        /// </summary>
        /// <remark></remark>
        [DisplayName("客户备注")]
        [StringLength(50)]
        public string Remark
        { get; set; }
        public IList<Customer> Customers
        {
            get { return Customers; }
            set { Customers = value; }
        }

    }
}
