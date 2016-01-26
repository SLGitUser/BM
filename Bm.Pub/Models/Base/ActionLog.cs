using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Bm.Models.Common;

namespace Bm.Models.Base
{
    /// <summary>
    /// 日志
    /// </summary>
    [DisplayName("日志")]
    public class ActionLog : IId, IStamp
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

        
        [DisplayName("状态")]
        public int Status { get; set; }

        [DisplayName("AppCode")]
        [StringLength(20)]
        public string AppCode { get; set; }

        [DisplayName("IP地址")]
        [StringLength(100)]
        public string Address { get; set; }

        [DisplayName("Url")]
        [StringLength(500)]
        public string Url { get; set; }

        [DisplayName("控制器名称")]
        [StringLength(32)]
        public string Controller { get; set; }

        [DisplayName("控制器名称")]
        [StringLength(32)]
        public string Action { get; set; }

        [DisplayName("是否提交")]
        [StringLength(32)]
        public string Method { get; set; }

        [DisplayName("模型名")]
        [StringLength(50)]
        public string ModelName { get; set; }

        [DisplayName("模型序号")]
        public string ModelId { get; set; }

        [DisplayName("用户序号")]
        public long UserId { get; set; }

        [DisplayName("分组名称")]
        [StringLength(32)]
        public string Category { get; set; }

        [DisplayName("用户账号")]
        [StringLength(32)]
        public string Actor { get; set; }

        [DisplayName("操作功能")]
        [StringLength(512)]
        public string Title { get; set; }

        [DisplayName("执行结果")]
        [StringLength(256)]
        public string Result { get; set; }

        [DisplayName("操作描述")]
        [StringLength(500)]
        public string Description { get; set; }

        public override string ToString()
        {
            var buff = new StringBuilder();
            buff.Append("访问地址：");
            buff.Append(Address);
            buff.Append(Environment.NewLine);
            buff.Append("访问网址：");
            buff.Append(Url);
            buff.Append(Environment.NewLine);
            buff.Append("控制器名称：");
            buff.Append(Controller);
            buff.Append(Environment.NewLine);
            buff.Append("操作名称：");
            buff.Append(Action);
            buff.Append(Environment.NewLine);
            buff.Append("HttpMethod：");
            buff.Append(Method);
            buff.Append(Environment.NewLine);
            buff.Append("模型名称：");
            buff.Append(ModelName);
            buff.Append(Environment.NewLine);
            buff.Append("模型序号：");
            buff.Append(ModelId);
            buff.Append(Environment.NewLine);
            buff.Append("用户账号：");
            buff.Append(Actor);
            buff.Append(Environment.NewLine);
            buff.Append("分组名称：");
            buff.Append(Category);
            buff.Append(Environment.NewLine);
            buff.Append("操作说明：");
            buff.Append(Title);
            buff.Append(Environment.NewLine);
            buff.Append("执行结果：");
            buff.Append(Result);
            buff.Append(Environment.NewLine);
            buff.Append("描述：");
            buff.Append(Description);
            buff.Append(Environment.NewLine);
            return buff.ToString();
        }
    }
}