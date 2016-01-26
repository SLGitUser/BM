using System;
using System.Linq;

namespace Bm.Modules.Helper
{
    public static class ArrayHelper
    {
        #region step
        
        public static int[] Step(this int start, int step, int length)
        {
            var arr = new int[length];
            var cur = start;
            for (var i = 0; i < length; ++i)
            {
                arr[i] = cur;
                start += step;
            }
            return arr;
        }

        public static TModel[] Step<TModel>(this int start, int step, int length, Func<int, TModel> valueFunc)
        {
            return Step(start, step, length).Select(valueFunc).ToArray();
        }

        public static int[] Step(this int start, int length)
        {
            return Step(start, 1, length);
        }

        public static TModel[] Step<TModel>(this int start, int length, Func<int, TModel> valueFunc)
        {
            return Step(start, 1, length).Select(valueFunc).ToArray();
        }

        #endregion

        #region StepTo
        
        public static int[] StepTo(this int start, int end)
        {
            var step = start > end ? -1 : 1;
            var length = Math.Abs(end - start) + 1;
            return Step(start, step, length);
        }

        public static TModel[] StepTo<TModel>(this int start, int end, Func<int, TModel> valueFunc)
        {
            return StepTo(start, end).Select(valueFunc).ToArray();
        }


        #endregion
        
        public static bool IsNullOrEmpty<TDataType>(this TDataType[] array)
        {
            return array == null || !array.Any();
        }

        public static TDataType[] Sub<TDataType>(this TDataType[] value, int start, int length)
        {
            if (value == null) return null;
            if (value.Length == 0 || length <= 0 || value.Length - 1 < start)
                return new TDataType[0];
            var length1 = Math.Min(length, value.Length - start);

            var dataTypeArray = new TDataType[length1];
            Array.Copy(value, start, dataTypeArray, 0, length1);
            return dataTypeArray;
        }

        public static TDataType[] Left<TDataType>(this TDataType[] value, int length)
        {
            if (value == null) return null;
            if (value.Length == 0 || length <= 0) return new TDataType[0];

            var length1 = Math.Min(value.Length, length);
            var dataTypeArray = new TDataType[length1];
            Array.Copy(value, 0, dataTypeArray, 0, length1);
            return dataTypeArray;
        }

        public static TDataType[] TrimEnd<TDataType>(this TDataType[] value, int dropLength)
        {
            if (value == null) return null;
            if (value.Length == 0 || value.Length <= dropLength) return new TDataType[0];

            var length = value.Length - dropLength;
            var dataTypeArray = new TDataType[length];
            Array.Copy(value, 0, dataTypeArray, 0, length);
            return dataTypeArray;
        }

        public static TDataType[] Right<TDataType>(this TDataType[] value, int length)
        {
            if (value == null) return null;
            if (value.Length == 0 || length <= 0) return new TDataType[0];

            var sourceIndex = Math.Max(value.Length, length) - length;
            var length1 = Math.Min(value.Length, length);
            var dataTypeArray = new TDataType[length1];
            Array.Copy(value, sourceIndex, dataTypeArray, 0, length1);
            return dataTypeArray;
        }

        public static TDataType[] TrimStart<TDataType>(this TDataType[] value, int dropLength)
        {
            if (value == null) return null;
            if (value.Length == 0 || dropLength <= 0 || dropLength >= value.Length)
                return new TDataType[0];

            var sourceIndex = dropLength;
            var length = value.Length - dropLength;
            var dataTypeArray = new TDataType[length];
            Array.Copy(value, sourceIndex, dataTypeArray, 0, length);
            return dataTypeArray;
        }
        
        public static TDataType[] Concat<TDataType>(this TDataType[] value, params TDataType[][] args)
        {
            if (value == null) return null;
            var dataTypeArray1 = args.Where(m => m != null).ToArray();
            var dataTypeArray2 = new TDataType[value.Length + dataTypeArray1.Sum(m => m.Length)];

            int num = 0;
            foreach (var dataTypeArray3 in args)
                Array.Copy(dataTypeArray3, 0, dataTypeArray2, num += dataTypeArray3.Length, dataTypeArray3.Length);
            return dataTypeArray2;
        }



    }
}
