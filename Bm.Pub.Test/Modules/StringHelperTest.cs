using Bm.Modules.Helper;
using NUnit.Framework;

namespace Bm.Pub.Test.Modules
{
    [TestFixture]
    public class StringHelperTest
    {
        [TestCase("Id", "id")]
        [TestCase("CreatedAt", "created_at")]
        [TestCase("CreatedBy", "created_by")]
        [TestCase("OnePropertyOfOneThing", "one_property_of_one_thing")]
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        [TestCase(null, null)]
        public void UnderlineTest(string value, string expected)
        {
            Assert.AreEqual(value.Underline(), expected);
        }
        
        [TestCase("Id", "id")]
        [TestCase("id", "id")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void EqualsIgnoreCaseTrueTest(string value, string expected)
        {
            Assert.IsTrue(value.EqualsIgnoreCase(expected));
        }
        
        [TestCase("id", "idx")]
        [TestCase("", null)]
        [TestCase(null, "")]
        public void EqualsIgnoreCaseFalseTest(string value, string expected)
        {
            Assert.IsFalse(value.EqualsIgnoreCase(expected));
        }
    }
}
