using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bm.Modules.Orm
{
    public static class ExpressionHelper
    {
        // Visitorで舐めてx => x.Hoge == xxという形式のExpression Treeから値と演算子のペアを取り出す
        public static PredicatePair[] GetPredicatePairs<T>(Expression<Func<T, bool>> predicate)
        {
            return PredicateExtractVisitor.VisitAndGetPairs(predicate);
        }

        class PredicateExtractVisitor : ExpressionVisitor
        {
            readonly ParameterExpression parameterExpression; // x => ...のxなのかを比較判定するため保持
            List<PredicatePair> result = new List<PredicatePair>(); // 抽出結果保持

            public static PredicatePair[] VisitAndGetPairs<T>(Expression<Func<T, bool>> predicate)
            {
                var visitor = new PredicateExtractVisitor(predicate.Parameters[0]); // x => ... の"x"
                visitor.Visit(predicate);
                return visitor.result.ToArray();
            }

            public PredicateExtractVisitor(ParameterExpression parameterExpression)
            {
                this.parameterExpression = parameterExpression;
            }

            // Visitぐるぐるの入り口
            protected override Expression VisitBinary(BinaryExpression node)
            {
                // && と || はスルー、 <, <=, >, >=, !=, == なら左右の解析
                PredicatePair pair;
                switch (node.NodeType)
                {
                    case ExpressionType.AndAlso:
                        pair = null;
                        break;
                    case ExpressionType.OrElse:
                        pair = null;
                        break;
                    case ExpressionType.LessThan:
                        pair = ExtractBinary(node, PredicateOperator.LessThan);
                        break;
                    case ExpressionType.LessThanOrEqual:
                        pair = ExtractBinary(node, PredicateOperator.LessThanOrEqual);
                        break;
                    case ExpressionType.GreaterThan:
                        pair = ExtractBinary(node, PredicateOperator.GreaterThan);
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        pair = ExtractBinary(node, PredicateOperator.GreaterThanOrEqual);
                        break;
                    case ExpressionType.Equal:
                        pair = ExtractBinary(node, PredicateOperator.Equal);
                        break;
                    case ExpressionType.NotEqual:
                        pair = ExtractBinary(node, PredicateOperator.NotEqual);
                        break;
                    case ExpressionType.Call:
                        pair = ExtractBinary(node, PredicateOperator.NotEqual);
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                if (pair != null) result.Add(pair);

                return base.VisitBinary(node);
            }

            // 左右ノードから抽出
            PredicatePair ExtractBinary(BinaryExpression node, PredicateOperator predicateOperator)
            {
                // x.hoge == xx形式なら左がメンバ名
                var memberName = ExtractMemberName(node.Left);
                if (memberName != null)
                {
                    var value = GetValue(node.Right);
                    return new PredicatePair(memberName, value, predicateOperator);
                }
                // xx == x.hoge形式なら右がメンバ名
                memberName = ExtractMemberName(node.Right);
                if (memberName != null)
                {
                    var value = GetValue(node.Left);
                    return new PredicatePair(memberName, value, predicateOperator.Flip()); // >, >= と <, <= を統一して扱うため演算子は左右反転
                }

                throw new InvalidOperationException();
            }

            string ExtractMemberName(Expression expression)
            {
                var member = expression as MemberExpression;

                // ストレートにMemberExpressionじゃないとUnaryExpressionの可能性あり
                if (member == null)
                {
                    var unary = (expression as UnaryExpression);
                    if (unary != null && unary.NodeType == ExpressionType.Convert)
                    {
                        member = unary.Operand as MemberExpression;
                    }
                }

                // x => xのxと一致してるかチェック
                if (member != null && member.Expression == parameterExpression)
                {
                    var memberName = member.Member.Name;
                    return memberName;
                }

                return null;
            }

            // 式から値取り出すほげもげ色々、階層が深いと面倒なのね対応
            static object GetValue(Expression expression)
            {
                if (expression is ConstantExpression) return ((ConstantExpression)expression).Value;
                if (expression is NewExpression)
                {
                    var expr = (NewExpression)expression;
                    var parameters = expr.Arguments.Select(x => GetValue(x)).ToArray();
                    return expr.Constructor.Invoke(parameters); // newしてるけどアクセサ生成で高速云々
                }

                var memberNames = new List<string>();
                while (!(expression is ConstantExpression))
                {
                    if ((expression is UnaryExpression) && (expression.NodeType == ExpressionType.Convert))
                    {
                        expression = ((UnaryExpression)expression).Operand;
                        continue;
                    }

                    var memberExpression = (MemberExpression)expression;
                    memberNames.Add(memberExpression.Member.Name);
                    expression = memberExpression.Expression;
                }

                var value = ((ConstantExpression)expression).Value;

                for (int i = memberNames.Count - 1; i >= 0; i--)
                {
                    var memberName = memberNames[i];
                    // とりまリフレクションだけど、ここはアクセサを生成してキャッシュして高速可しよー
                    dynamic info = value.GetType().GetMember(memberName)[0];
                    value = info.GetValue(value);
                }

                return value;
            }

        }
    }

    // ExpressionTypeだと範囲広すぎなので縮めたものを
    public enum PredicateOperator
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }

    // x.Hoge == 10 みたいなのの左と右のペアを保持
    public class PredicatePair
    {
        public PredicateOperator Operator { get; private set; }
        public string MemberName { get; private set; }
        public object Value { get; private set; }

        public PredicatePair(string name, object value, PredicateOperator predicateOperator)
        {
            this.MemberName = name;
            this.Value = value;
            this.Operator = predicateOperator;
        }
    }

    public static class PredicatePairsExtensions
    {
        // SQL文作るー、のでValueのほうは無視気味。
        public static string ToSqlString(this PredicatePair[] pairs, string parameterPrefix)
        {
            var sb = new StringBuilder();
            var isFirst = true;
            foreach (var pair in pairs)
            {
                if (isFirst) isFirst = false;
                else sb.Append(" && "); // 今は&&連結だけ。||対応は面倒なのよ。。。

                sb.Append(pair.MemberName);
                switch (pair.Operator)
                {
                    case PredicateOperator.Equal:
                        if (pair.Value == null)
                        {
                            sb.Append(" is null ");
                            continue;
                        }
                        sb.Append(" = ").Append(parameterPrefix + pair.MemberName);
                        break;
                    case PredicateOperator.NotEqual:
                        if (pair.Value == null)
                        {
                            sb.Append(" is not null ");
                            continue;
                        }
                        sb.Append(" <> ").Append(parameterPrefix + pair.MemberName);
                        break;
                    case PredicateOperator.LessThan:
                        if (pair.Value == null) throw new InvalidOperationException();
                        sb.Append(" < ").Append(parameterPrefix + pair.MemberName);
                        break;
                    case PredicateOperator.LessThanOrEqual:
                        if (pair.Value == null) throw new InvalidOperationException();
                        sb.Append(" <= ").Append(parameterPrefix + pair.MemberName);
                        break;
                    case PredicateOperator.GreaterThan:
                        if (pair.Value == null) throw new InvalidOperationException();
                        sb.Append(" > ").Append(parameterPrefix + pair.MemberName);
                        break;
                    case PredicateOperator.GreaterThanOrEqual:
                        if (pair.Value == null) throw new InvalidOperationException();
                        sb.Append(" >= ").Append(parameterPrefix + pair.MemberName);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return sb.ToString();
        }
    }

    public static class PredicateOperatorExtensions
    {
        // 演算子を反転させる、 <= と >= の違いを吸収するため
        public static PredicateOperator Flip(this PredicateOperator predicateOperator)
        {
            switch (predicateOperator)
            {
                case PredicateOperator.LessThan:
                    return PredicateOperator.GreaterThan;
                case PredicateOperator.LessThanOrEqual:
                    return PredicateOperator.GreaterThanOrEqual;
                case PredicateOperator.GreaterThan:
                    return PredicateOperator.LessThan;
                case PredicateOperator.GreaterThanOrEqual:
                    return PredicateOperator.LessThanOrEqual;
                default:
                    return predicateOperator;
            }
        }
    }
}
