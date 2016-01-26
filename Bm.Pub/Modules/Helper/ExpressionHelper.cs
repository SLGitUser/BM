using System;
using System.Linq.Expressions;

namespace Bm.Modules.Helper
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// 根据表达式获得属性名
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetMemberName<TEntity, TProp>(this Expression<Func<TEntity, TProp>> expr)
        {
            if (expr?.Body.NodeType != ExpressionType.MemberAccess) return null;
            var memberExp = (MemberExpression)expr.Body;
            return memberExp.Member.Name;
        }

    }
}
