using System.Text;

namespace Bm.Modules
{
    public static class StringHelper
    {
        public static string Underline(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var buff = new StringBuilder();
            var isToLower = true;
            var isAny = false;
            foreach (var c in value)
            {
                if (isToLower && char.IsUpper(c))
                {
                    if(isAny) buff.Append('_');
                    buff.Append(char.ToLower(c));
                    isToLower = false;
                }
                else
                {
                    isToLower = true;
                    buff.Append(c);
                }
                isAny = true;
            }
            return buff.ToString();
        }
    }
}
