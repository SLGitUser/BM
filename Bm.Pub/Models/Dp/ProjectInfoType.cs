using System.Collections.Generic;
using System.ComponentModel;
using Bm.Modules;

namespace Bm.Models.Dp
{
    /// <summary>
    /// 开发项目资料类型
    /// </summary>
    public static class ProjectInfoType
    {
        public enum Type
        {
            [Description("周边")]
            Around = 0x1,
            [Description("简介")]
            Intro = 0x2,
            [Description("地段")]
            Lot = 0x3,
            [Description("配套")]
            Support = 0x4,
            [Description("教育")]
            Education = 0x5,
            [Description("环境")]
            Environmental = 0x6,
            [Description("交通")]
            Traffic = 0x7
        }

        static ProjectInfoType()
        {
            if (Dict == null)
            {
                Dict = Enumerations.GenDict<Type>();
            }
        }

        [Description("状态字典")]
        public static readonly IDictionary<Type, string> Dict;

        public static string Status(Type status)
        {
            return Dict.ContainsKey(status) ? Dict[status] : "无效状态";
        }
        
    }
}