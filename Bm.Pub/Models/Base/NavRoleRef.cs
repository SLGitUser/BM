using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Base
{
    /// <summary>
    /// 功能导航与角色关系
    /// </summary>
    [DisplayName("功能导航与角色关系")]
    public sealed class NavRoleRef: IId, ICreateStamp
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

        /// <summary>
        /// 读取或者设置角色编号
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("角色编号")]
        [StringLength(20)]
        public string RoleNo { get; set; }

        /// <summary>
        /// 读取或者设置导航
        /// </summary>
        /// <remark></remark>
        [DisplayName("导航")]
        public long NavId { get; set; }
    }
}