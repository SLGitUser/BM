using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 工作任务
    /// </summary>
    [DisplayName("工作任务")]
    public sealed class Task : IId, IStamp
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
        public string CustomerNo { get; set; }

        /// <summary>
        /// 读取或者设置经纪人编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("经纪人编号")]
        [StringLength(49)]
        [Required]
        public string BrokerNo { get; set; }
        /// <summary>
        /// 读取或者设置项目编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("项目编号")]
        [StringLength(49)]
        [Required]
        public string ProjectNo { get; set; }
        /// <summary>
        /// 读取或者设置地区代码
        /// </summary>
        /// <remark></remark>
        [DisplayName("地区代码")]
        [StringLength(20)]
        [Required]
        public string CityNo { get; set; }

        /// <summary>
        /// 读取或者设置客户姓名
        /// </summary>
        /// <remark></remark>
        [DisplayName("客户姓名")]
        [StringLength(49)]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置客户性别
        /// </summary>
        /// <remark></remark>
        [DisplayName("客户性别")]
        [StringLength(5)]
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
        [StringLength(49)]
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
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 读取或者设置活动编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动编号")]
        [StringLength(49)]
        [Required]
        public string No { get; set; }
        /// <summary>
        /// 读取或者设置活动类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动类型")]
        [StringLength(49)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 读取或者设置发生日期
        /// </summary>
        /// <remark></remark>
        [DisplayName("发生日期")]
        [Required]
        public DateTime At { get; set; }

        /// <summary>
        /// 读取或者设置活动状态
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动状态")]
        [StringLength(49)]
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// 读取或者设置响应截止日
        /// </summary>
        /// <remark></remark>
        [DisplayName("响应截止日")]
        public DateTime Deadline { get; set; }

        /// <summary>
        /// 读取或者设置活动备注
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动备注")]
        [StringLength(49)]
        public string Remark { get; set; }

        /// <summary>
        /// 读取或者设置活动图片
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动图片")]
        [StringLength(49)]
        public string Pic { get; set; }
        /// <summary>
        /// 读取或者设置活动结果
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动结果")]
        [StringLength(49)]
        public string Result { get; set; }
        /// <summary>
        /// 读取或者设置置业顾问编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("置业顾问编号")]
        [StringLength(49)]
        public string ConsultantNo { get; set; }
        /// <summary>
        /// 读取或者设置置业顾问项目
        /// </summary>
        /// <remark></remark>
        [DisplayName("置业顾问项目")]
        [StringLength(49)]
        public string ConsultantName { get; set; }
        /// <summary>
        /// 读取或者设置置业顾问备注
        /// </summary>
        /// <remark></remark>
        [DisplayName("置业顾问备注")]
        [StringLength(49)]
        public string ConsultantRemark { get; set; }

        /// <summary>
        /// 读取或者设置流动保护过期日
        /// </summary>
        /// <remark></remark>
        [DisplayName("流动保护过期日")]
        public DateTime ExpiredAt { get; set; }
    }
}
