using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bm.Modules.Helper;

namespace Bm.Modules
{
    public class GeneralComparer<T> : IEqualityComparer<T>
    {
        private readonly IList<Func<T, object>> _getPropertyValueFunc = new List<Func<T, object>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public GeneralComparer(params Expression<Func<T, object>>[] e)
        {
            foreach (var expression in e)
            {
                _getPropertyValueFunc.Add(expression.Compile());
            }
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            var ret = true;
            foreach (var func in _getPropertyValueFunc)
            {
                var xValue = func(x);
                var yValue = func(y);

                if (xValue == null)
                {
                    ret &= yValue == null;
                    continue;
                }

                if (xValue.GetType().GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var xList = ((IEnumerable)xValue).GetEnumerator();
                    var yList = ((IEnumerable)yValue).GetEnumerator();
                    try
                    {
                        while (xList.MoveNext())
                        {
                            yList.MoveNext();
                            ret &= xList.Current.Equals(yList.Current);
                        }
                        if (yList.MoveNext())
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    ret &= xValue.Equals(yValue);
                }
            }
            return ret;
        }

        public int GetHashCode(T obj)
        {
            var p = _getPropertyValueFunc
                .Select(func => func(obj))
                .Where(propertyValue => propertyValue != null)
                .Aggregate("", (current, propertyValue) => current + propertyValue.ToString());
            return p.GetHashCode();
        }

        #endregion
    }

    public static class DistinctHelper
    {
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> t, params Expression<Func<T, object>>[] e)
        {
            if (e.IsNullOrEmpty())
            {
                return Enumerable.Distinct(t);
            }
            return t.Distinct(new GeneralComparer<T>(e));
        }
    }
}