namespace Bm.Modules.Html
{
    public enum SelectMode
    {
        /// <summary>
        /// 默认，不增加任何字段
        /// </summary>
        Default = 0x0,
        /// <summary>
        /// 增加全部到第一位置，默认为空值
        /// </summary>
        AddAllByString = 0x1,
        /// <summary>
        /// 增加全部选项到第一位置，默认为0
        /// </summary>
        AddAllByNumber = 0x2,
        /// <summary>
        /// 增加无选项到第一位置，默认为空值
        /// </summary>
        AddNoneByString = 0x4,
        /// <summary>
        /// 增加无选项到第一位置，默认为0
        /// </summary>
        AddNoneByNumber = 0x8,
        /// <summary>
        /// 增加空选项到第一位置，默认为空值
        /// </summary>
        AddEmpty = 0x10
    }
}