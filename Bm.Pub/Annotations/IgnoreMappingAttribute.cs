using System;

namespace Bm.Annotations
{
    /// <summary>
    /// 是否忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreMappingAttribute : Attribute
    {
    }
}