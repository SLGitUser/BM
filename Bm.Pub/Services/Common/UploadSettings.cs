namespace Bm.Services.Common
{
    /// <summary>
    /// 文件上传限制
    /// </summary>
    public class UploadSettings
    {
        /// <summary>
        ///读取或者设置the allow ext.
        /// </summary>
        /// <value>
        /// 允许扩展名
        /// </value>
        public string AllowExts { get; set; }

        /// <summary>
        ///读取或者设置the length of the allow.
        /// </summary>
        /// <value>
        /// 允许文件最大字节数
        /// </value>
        public long MaxLength { get; set; }
    }
}