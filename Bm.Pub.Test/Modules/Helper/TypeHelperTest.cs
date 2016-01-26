using System;
using Bm.Modules.Helper;
using NUnit.Framework;

namespace Bm.Pub.Test.Modules.Helper
{
    [TestFixture]
    public class TypeHelperTest
    {



        #region Type
        
        [TestCase(typeof(int))]
        [TestCase(typeof(uint))]
        [TestCase(typeof(int?))]
        [TestCase(typeof(long))]
        [TestCase(typeof(ulong))]
        [TestCase(typeof(long?))]
        [TestCase(typeof(short))]
        [TestCase(typeof(ushort))]
        [TestCase(typeof(short?))]
        [TestCase(typeof(byte))]
        [TestCase(typeof(sbyte))]
        [TestCase(typeof(byte?))]
        [TestCase(typeof(decimal))]
        [TestCase(typeof(decimal?))]
        [TestCase(typeof(float))]
        [TestCase(typeof(float?))]
        [TestCase(typeof(double))]
        [TestCase(typeof(double?))]
        public void IsNumericTypeTest(Type type)
        {
            Assert.IsTrue(type.IsNumeric());
        }

        [TestCase(typeof(DateTime))]
        [TestCase(typeof(DateTime?))]
        [TestCase(typeof(bool))]
        [TestCase(typeof(bool?))]
        [TestCase(typeof(string))]
        public void IsNumericTypeNotTest(Type type)
        {
            Assert.IsFalse(type.IsNumeric());
        }

        [TestCase(typeof(DateTime))]
        [TestCase(typeof(DateTime?))]
        public void IsDateTimeTest(Type type)
        {
            Assert.IsTrue(type.IsDateTime());
        }

        [TestCase(typeof(bool))]
        [TestCase(typeof(bool?))]
        public void IsBoolTest(Type type)
        {
            Assert.IsTrue(type.IsBool());
        }

        [TestCase(typeof(DateTime))]
        [TestCase(typeof(DateTime?))]
        [TestCase(typeof(string))]
        public void IsStringValueTest(Type type)
        {
            Assert.IsTrue(type.IsStringValue());
        }

        #endregion
    }
}