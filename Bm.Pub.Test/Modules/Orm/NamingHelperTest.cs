using Bm.Modules;
using Bm.Modules.Orm;
using NUnit.Framework;

namespace Bm.Pub.Test.Modules.Orm
{
    [TestFixture]
    public class NamingHelperTest
    {
        [TestCase("Id", "id")]
        [TestCase("CreatedAt", "created_at")]
        [TestCase("CreatedBy", "created_by")]
        [TestCase("OnePropertyOfOneThing", "one_property_of_one_thing")]
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        [TestCase(null, null)]
        public void MapPascalToUnderlineTest(string value, string expected)
        {
            Assert.AreEqual(NamingHelper.MapPascalToUnderline(value), expected);
        }
        
        [TestCase("id", "Id")]
        [TestCase("created_at", "CreatedAt")]
        [TestCase("created_by", "CreatedBy")]
        [TestCase("one_property_of_one_thing", "OnePropertyOfOneThing")]
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        [TestCase(null, null)]
        public void MapUnderlineToPascalTest(string value, string expected)
        {
            Assert.AreEqual(NamingHelper.MapUnderlineToPascal(value), expected);
        }

        [TestCase("id", "Id")]
        [TestCase("created_at", "CreatedAt")]
        [TestCase("created_by", "CreatedBy")]
        [TestCase("one_property_of_one_thing", "OnePropertyOfOneThing")]
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        [TestCase(null, null)]
        public void MapFieldToPropertyTest(string value, string expected)
        {
            Assert.AreEqual(NamingHelper.MapFieldToProperty(value), expected);
        }

        [TestCase("Book", "book")]
        [TestCase("BookRef", "book_ref")]
        [TestCase("BookOwnerRef", "book_owner_ref")]
        [TestCase("Bm.Models.Biz.Book", "biz_book")]
        [TestCase("Bm.Models.Biz.BookOwnerRef", "biz_book_owner_ref")]
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        [TestCase(null, null)]
        public void MapClassToTableTest(string value, string expected)
        {
            Assert.AreEqual(NamingHelper.MapClassToTable(value), expected);
        }
    }
}
