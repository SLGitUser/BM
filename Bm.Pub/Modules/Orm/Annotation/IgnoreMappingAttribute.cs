using System;

namespace Bm.Modules.Orm.Annotation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreMappingAttribute : Attribute
    {
    }
}