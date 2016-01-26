using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Bm.Modules.Orm
{
    public partial interface ISqlAdapter
    {
        int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert);

        //new methods for issue #336
        void AppendColumnName(StringBuilder sb, string columnName);

        void AppendColumnNameEqualsValue(StringBuilder sb, string columnName);

        int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnSql, string valuesSql, object entityToInsert);
    }
}