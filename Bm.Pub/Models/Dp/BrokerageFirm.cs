using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 经纪公司
    /// </summary>
    [DisplayName("经纪公司")]
    public sealed class BrokerageFirm : IId, IStamp
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


        #region 基础信息

        /// <summary>
        /// 读取或者设置名称
        /// </summary>
        /// <remark></remark>
        [DisplayName("名称")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置经纪公司代码
        /// </summary>
        /// <remark></remark>
        [DisplayName("经纪公司代码")]
        [StringLength(50)]
        [Required]
        public string FirmNo { get; set; }

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
        /// 读取或者设置组织机构代码
        /// </summary>
        /// <remark></remark>
        [DisplayName("组织机构代码")]
        [StringLength(20)]
        public string FirmOrgCode { get; set; }

        /// <summary>
        /// 读取或者设置公司地址
        /// </summary>
        /// <remark></remark>
        [DisplayName("公司地址")]
        [StringLength(20)]
        public string Address { get; set; }

        /// <summary>
        /// 读取或者设置公司图片
        /// </summary>
        /// <remark></remark>
        [DisplayName("公司图片")]
        [StringLength(50)]
        public string Pic { get; set; }
        #endregion
        #region 法人代表信息

        /// <summary>
        /// 读取或者设置法人代表姓名
        /// </summary>
        /// <remark></remark>
        [DisplayName("法人代表姓名")]
        [StringLength(50)]
        public string LegalName { get; set; }

        /// <summary>
        /// 读取或者设置法人代表证件类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("法人代表证件类型")]
        [StringLength(49)]
        public string LegalCardType { get; set; }

        /// <summary>
        /// 读取或者设置法人代表证件号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("法人代表证件号码")]
        [StringLength(50)]
        public string LegalCardNo { get; set; }

        /// <summary>
        /// 读取或者设置法人代表手机号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("法人代表手机号码")]
        [StringLength(11)]
        public string LegalMobile { get; set; }

        /// <summary>
        /// 读取或者设置法人代表电子邮箱
        /// </summary>
        /// <remark></remark>
        [DisplayName("法人代表电子邮箱")]
        [StringLength(49)]
        public string LegalEmail { get; set; }

        #endregion
        #region 联系人信息

        /// <summary>
        /// 读取或者设置联系人姓名
        /// </summary>
        /// <remark></remark>
        [DisplayName("联系人姓名")]
        [StringLength(50)]
        public string ContactsName { get; set; }

        /// <summary>
        /// 读取或者设置联系人证件类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("联系人证件类型")]
        [StringLength(49)]
        public string ContactsCardType { get; set; }

        /// <summary>
        /// 读取或者设置联系人证件号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("联系人证件号码")]
        [StringLength(50)]
        public string ContactsCardNo { get; set; }

        /// <summary>
        /// 读取或者设置联系人手机号码
        /// </summary>
        /// <remark></remark>
        [DisplayName("联系人手机号码")]
        [StringLength(11)]
        public string ContactsMobile { get; set; }

        /// <summary>
        /// 读取或者设置联系人电子邮箱
        /// </summary>
        /// <remark></remark>
        [DisplayName("联系人电子邮箱")]
        [StringLength(49)]
        public string ContactsEmail { get; set; }

        #endregion

        #region 时间

        /// <summary>
        /// 读取或者设置注册时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("注册时间")]
        [Required]
        public DateTime RegAt { get; set; }

        /// <summary>
        /// 读取或者设置认证时间
        /// </summary>
        /// <remark></remark>
        [DisplayName("认证时间")]
        public DateTime CheckAt { get; set; }

        #endregion
        #region 金额

        /// <summary>
        /// 读取或者设置待开票额
        /// </summary>
        /// <remark></remark>
        [DisplayName("待开票额")]
        [Required]
        public decimal InvoiceTodoAmount { get; set; }

        /// <summary>
        /// 读取或者设置已开票额
        /// </summary>
        /// <remark></remark>
        [DisplayName("已开票额")]
        [Required]
        public decimal InvoiceDoneAmount { get; set; }

        /// <summary>
        /// 读取或者设置可提现额
        /// </summary>
        /// <remark></remark>
        [DisplayName("可提现额")]
        [Required]
        public decimal CashTodoAmount { get; set; }

        /// <summary>
        /// 读取或者设置申请提现额
        /// </summary>
        /// <remark></remark>
        [DisplayName("申请提现额")]
        [Required]
        public decimal CashDoingAmount { get; set; }

        /// <summary>
        /// 读取或者设置已提现额
        /// </summary>
        /// <remark></remark>
        [DisplayName("已提现额")]
        [Required]
        public decimal CashDoneAmount { get; set; }

        #endregion

    }
}
