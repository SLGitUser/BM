namespace Bm.Services.Common
{
    /// <summary>
    /// �ļ��ϴ�����
    /// </summary>
    public class UploadSettings
    {
        /// <summary>
        ///��ȡ��������the allow ext.
        /// </summary>
        /// <value>
        /// ������չ��
        /// </value>
        public string AllowExts { get; set; }

        /// <summary>
        ///��ȡ��������the length of the allow.
        /// </summary>
        /// <value>
        /// �����ļ�����ֽ���
        /// </value>
        public long MaxLength { get; set; }
    }
}