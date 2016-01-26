using System;
using System.Globalization;
using System.Linq.Expressions;
using Bm.Modules.Helper;
using NUnit.Framework;

namespace Bm.Pub.Test.Modules.Helper
{
    [TestFixture]
    public class ExpressionHelperTest
    {
        public class Book
        {
            public string Name { get; set; }

            public decimal Price { get; set; }

            public int Counter;
        }

        [Test]
        public void MemberNameTest()
        {
            {
                Expression<Func<Book, string>> expr = m => m.Name;
                Assert.AreEqual(expr.GetMemberName(), "Name");
            }
            {
                Expression<Func<Book, decimal>> expr = m => m.Price;
                Assert.AreEqual(expr.GetMemberName(), "Price");
            }
            {
                Expression<Func<Book, int>> expr = m => m.Counter;
                Assert.AreEqual(expr.GetMemberName(), "Counter");
            }
            {
                Expression<Func<Book, string>> expr = null;
                Assert.IsNull(expr.GetMemberName());
            }
            {
                Expression<Func<Book, string>> expr = m => m.Price.ToString(CultureInfo.InvariantCulture);
                Assert.IsNull(expr.GetMemberName());
            }
        }
    }
}
