using System.Collections.Generic;
using System.ComponentModel;
using Bm.Modules;

namespace Bm.Models.Dp
{
    /// <summary>
    /// ������Ŀ��������
    /// </summary>
    public static class ProjectInfoType
    {
        public enum Type
        {
            [Description("�ܱ�")]
            Around = 0x1,
            [Description("���")]
            Intro = 0x2,
            [Description("�ض�")]
            Lot = 0x3,
            [Description("����")]
            Support = 0x4,
            [Description("����")]
            Education = 0x5,
            [Description("����")]
            Environmental = 0x6,
            [Description("��ͨ")]
            Traffic = 0x7
        }

        static ProjectInfoType()
        {
            if (Dict == null)
            {
                Dict = Enumerations.GenDict<Type>();
            }
        }

        [Description("״̬�ֵ�")]
        public static readonly IDictionary<Type, string> Dict;

        public static string Status(Type status)
        {
            return Dict.ContainsKey(status) ? Dict[status] : "��Ч״̬";
        }
        
    }
}