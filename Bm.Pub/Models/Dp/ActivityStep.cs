using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 活动参与
    /// </summary>
    [DisplayName("活动参与")]
    public sealed class ActivityStep : IId, IStamp
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
        //MessageNo
        //UserNo
        //Type
        //At
        //LeaveAt
        //Remark
        /// <summary>
        /// 读取或者设置消息编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("消息编号")]
        [StringLength(49)]
        [Required]
        public string MessageNo

        { get; set; }

        /// <summary>
        /// 读取或者设置用户编号
        /// </summary>
        /// <remark></remark>
        [DisplayName("用户编号")]
        [StringLength(49)]
        [Required]
        public string UserNo
        { get; set; }

        /// <summary>
        /// 读取或者设置活动类型
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动类型")]
        [StringLength(49)]
        [Required]
        public string Type
        { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime At { get; set; }

        /// <summary>
        /// 离开时间
        /// </summary>
        public DateTime? LeaveAtAt { get; set; }

        /// <summary>
        /// 读取或者设置活动备注
        /// </summary>
        /// <remark></remark>
        [DisplayName("活动备注")]
        [StringLength(49)]
        public string Remark

        { get; set; }
    }
}
