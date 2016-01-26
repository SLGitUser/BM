using System;

namespace Bm.Modules.Orm.Annotation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class WriteAttribute : Attribute
    {
        public WriteAttribute(bool write)
        {
            Write = write;
        }
        public bool Write { get; }
    }
}