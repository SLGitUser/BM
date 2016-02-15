using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Common;

namespace Bm.Models.Base
{
    [Serializable]
    public class UploadFile : IId, IStamp
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
        /// 读取或者设置资源类型
        /// </summary>
        [DisplayName(@"资源类型")]
        [StringLength(50)]
        public string ResType { get; set; }

        /// <summary>
        /// 读取或者设置资源编号
        /// </summary>
        [DisplayName(@"资源编号")]
        [StringLength(36)]
        public string ResNo { get; set; }

        /// <summary>
        /// 读取或者设置附件编号
        /// </summary>
        [DisplayName(@"UUID")]
        [StringLength(36)]
        public string No { get; set; }

        /// <summary>
        /// 读取或者设置文件路径
        /// </summary>
        [DisplayName(@"文件路径")]
        [Required]
        [StringLength(200)]
        public string Path { get; set; }

        /// <summary>
        /// 读取或者设置是否显示
        /// </summary>
        [DisplayName(@"是否显示")]
        public bool IsShow { get; set; }

        /// <summary>
        /// 读取或者设置MIME信息
        /// </summary>
        [DisplayName(@"MIME信息")]
        [StringLength(50)]
        public string Mime { get; set; }

        /// <summary>
        /// 读取或者设置文件名
        /// </summary>
        [DisplayName(@"文件名")]
        [StringLength(80)]
        public string Name { get; set; }
        
        /// <summary>
        /// 读取或者设置文件大小
        /// </summary>
        /// <remark></remark>
        [DisplayName("文件字节数")]
        public int Size { get; set; }

        [DisplayName("文件大小")]
        public string SizeDesc()
        {
            decimal size = Size;
            if (size < 1024) return string.Concat(size, " Bytes");
            if (size < 1048576) return $"{size / 1024:F2} KiB";
            if (size < 1073741824) return $"{size / 1048576:F2} MiB";
            return $"{size / 1073741824:F2} GiB";
        }

    }

}