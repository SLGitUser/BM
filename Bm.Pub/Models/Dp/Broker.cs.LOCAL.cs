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
    /// 经纪人
    /// </summary>
    [DisplayName("经纪人")]
    public sealed  class Broker : IId, IStamp
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
        /// 姓名
        /// </summary>

        [DisplayName("姓名")]
        [StringLength(20)]
        [Required]
        public string 姓名 { get; set; }


        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        [StringLength(5)] 
        public string Gender { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        [DisplayName("手机号")]
        [StringLength(12)]
        [Required]
        public string Mobile { get; set; }


        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("手机号")]
        [StringLength(50)] 
        public string Email { get; set; }


        /// <summary>
        /// 地区
        /// </summary>
        [DisplayName("地区")]
        [StringLength(20)]
        [Required]
        public string City { get; set; } 

        /// <summary>
        /// 地区代码
        /// </summary>
        [DisplayName("地区代码")]
        [StringLength(20)]
        [Required]
        public string CityNo { get; set; }


        /// <summary>
        /// 经纪公司
        /// </summary>
        [DisplayName("经纪公司")]
        [StringLength(50)] 
        public string Firm { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [DisplayName("经纪公司代码")]
        [StringLength(50)] 
        public string FirmNo { get; set; }


        /// <summary>
        /// 个人简介
        /// </summary>
        [DisplayName("个人简介")]
        [StringLength(200)] 
        public string Intro { get; set; }


        /// <summary>
        /// 个人简介
        /// </summary>
        [DisplayName("个人简介")] 
        [Required]
        public DateTime? RegAt { get; set; }




        /// <summary>
        /// 推荐人
        /// </summary>
        [DisplayName("个人简介")]

        [StringLength(50)]
        public string Referral { get; set; }




        /// <summary>
       /// 头像
        /// </summary>
        [DisplayName("头像")]
        [Required]
        public string Pic { get; set; }
    }
}
