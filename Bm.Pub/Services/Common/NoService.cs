using System.Text.RegularExpressions;
using Bm.Models.Common;

namespace Bm.Services.Common
{
    public class NoService
    {
        /// <summary>
        /// 序号递增
        /// </summary>
        /// <param name="no"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static MessageRecorder<string> Inc(string no, int step = 1)
        {
            var r = new MessageRecorder<string>();
            if (string.IsNullOrWhiteSpace(no))
                return r.Error("编号格式错误");
            r.Value = Regex.Replace(no, @"^(.*)(\d+)$",
                x =>
                {
                    var prefix = x.Groups[1].Value;
                    var noStr = x.Groups[2].Value;
                    int noInt;
                    if (int.TryParse(noStr, out noInt))
                    {
                        return string.Concat(prefix, (noInt + step).ToString().PadLeft(noStr.Length, '0'));
                    }
                    r.Error("编号格式不正确");
                    return noStr;
                });
            return r;
        }
    }
}
