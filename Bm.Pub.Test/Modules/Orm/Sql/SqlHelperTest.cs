using Bm.Modules.Orm.Sql;
using NUnit.Framework;

namespace Bm.Pub.Test.Modules.Orm.Sql
{
    [TestFixture]
    public class SqlHelperTest
    {
        [TestCase(Op.Not, "!{0}")]
        [TestCase(Op.Nul, "{0} IS NULL")]
        [TestCase(Op.NotNul, "{0} IS NOT NULL")]

        [TestCase(Op.Eq, "{0} == {1}")]
        [TestCase(Op.NotEq, "{0} <> {1}")]
        [TestCase(Op.Gt, "{0} > {1}")]
        [TestCase(Op.Ge, "{0} >= {1}")]
        [TestCase(Op.Lt, "{0} < {1}")]
        [TestCase(Op.Le, "{0} <= {1}")]

        [TestCase(Op.Sw, "{0} LIKE '{1}%'")]
        [TestCase(Op.NotSw, "{0} NOT LIKE '{1}%'")]
        [TestCase(Op.Ew, "{0} LIKE '%{1}'")]
        [TestCase(Op.NotEw, "{0} NOT LIKE '%{1}'")]
        [TestCase(Op.Ct, "{0} LIKE '%{1}%'")]
        [TestCase(Op.NotCt, "{0} NOT LIKE '%{1}%'")]

        [TestCase(Op.In, "{0} IN ({1})")]
        [TestCase(Op.NotIn, "{0} NOT IN ({1})")]
        public void OpToStringTest(Op op, string expected)
        {
            Assert.AreEqual(op.AsString(), expected);
        }
    }
}
