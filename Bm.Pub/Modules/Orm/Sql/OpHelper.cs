using System.Collections.Generic;

namespace Bm.Modules.Orm.Sql
{
    public static class OpHelper
    {
        private static readonly IDictionary<Op, string> Dict = new Dictionary<Op, string>
        {
            { Op.Not, "!{0}" },

            { Op.Nul, "{0} IS NULL" },
            { Op.NotNul, "{0} IS NOT NULL" },

            { Op.Eq, "{0} = {1}" },
            { Op.NotEq, "{0} <> {1}" },
            { Op.Gt, "{0} > {1}" },
            { Op.Ge, "{0} >= {1}" },
            { Op.Lt, "{0} < {1}" },
            { Op.Le, "{0} <= {1}" },

            { Op.Sw, "{0} LIKE '{1}%'" },
            { Op.NotSw, "{0} NOT LIKE '{1}%'" },
            { Op.Ew, "{0} LIKE '%{1}'" },
            { Op.NotEw, "{0} NOT LIKE '%{1}'" },
            { Op.Ct, "{0} LIKE '%{1}%'" },
            { Op.NotCt, "{0} NOT LIKE '%{1}%'" },

            { Op.In, "{0} IN ({1})" },
            { Op.NotIn, "{0} NOT IN ({1})" }
        }; 

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static string AsString(this Op op)
        {
            string value;
            return Dict.TryGetValue(op, out value) ? value : null;
        }

    }
}
