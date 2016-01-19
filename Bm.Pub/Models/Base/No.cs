using System;
using System.ComponentModel;
using com.senlang.Sdip.Data;

namespace Bm.Models.Base
{
    /// <summary>
    /// 编号规则
    /// </summary>
    [DisplayName("编号规则")]
    public sealed class No : IId, IStamp, IOrg
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
        /// 读取或者设置部门
        /// </summary>
        /// <remark></remark>
        [DisplayName("部门")]
        public long OrgId { get; set; }

        /// <summary>
        /// 状态，目前没有使用
        /// </summary>
        [DisplayName("状态")]
        public int Status { get; set; }

        [DisplayName(@"年度")]
        public int Year { get; set; }

        [DisplayName(@"月份")]
        public int Month { get; set; }

        [DisplayName(@"资源描述")]
        public string Description { get; set; }

        [DisplayName(@"资源名称")]
        public string Resource { get; set; }

        [DisplayName(@"编号模板")]
        public string Expr { get; set; }

        [DisplayName(@"前缀")]
        public string Prefix { get; set; }

        [DisplayName(@"时间格式")]
        public string TimeFormat { get; set; }

        [DisplayName(@"编号序号")]
        public long DigitalNo { get; set; }

        [DisplayName(@"编号位长")]
        public int NoLength { get; set; }

        [DisplayName(@"最新编号")]
        public String FullNo { get; set; }

    }
}
