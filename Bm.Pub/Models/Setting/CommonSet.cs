using System.ComponentModel.DataAnnotations;

namespace Bm.Models.Setting
{
    /// <summary>
    /// 通用设置信息
    /// </summary>
    public sealed class CommonSet
    {
        /// <summary>
        /// 读取或者设置应用程序
        /// </summary>
        [Required]
        [StringLength(50)]
        public string App { get; set; }

        /// <summary>
        /// 读取或者设置分组
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        /// <summary>
        /// 读取或者设置键
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        /// <summary>
        /// 读取或者设置描述
        /// </summary>
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        /// 读取或者设置值
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        /// <summary>
        /// 读取或者设置数据类型
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
