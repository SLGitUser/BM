using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Bm.Models.Common;
using Bm.Modules.Helper;

namespace Bm.Services.Common
{
    public sealed class AccessoryService
    {
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="request">HttpRequest</param>
        /// <param name="inputName">获取指定Input控件的文件</param>
        /// <returns>上传结果信息</returns>
        public static MessageRecorder<IList<string>> Upload(
            HttpRequestBase request,
            string inputName = null)
        {
            var mr = new MessageRecorder<IList<string>> { Value = new List<string>() };
            var service = new AliyunOssService();
            for (var i = 0; i < request.Files.Count; i++)
            {
                var upload = request.Files.AllKeys[i];
                //如果指定了Input名称，则寻找对应的Input执行上传操作
                if (Equals(upload, inputName)) continue;

                var file = request.Files[i];
                if (file == null) continue;
                if (file.ContentLength <= 0) continue;

                // Uploaded file
                // Use the following properties to get file's name, size and MIMEType 
                //int fileSize = file.ContentLength;
                //string fileName = file.FileName;
                //string mimeType = file.ContentType;

                var key = Guid.NewGuid().ToString("N");
                var ms = ReadFully(file.InputStream);
                var mr2 = service.PutObject(key, ms);
                mr.Append(mr2);
                mr.Value.Add(key);
            }
            return mr;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static MessageRecorder<bool> Remove(IList<string> keys)
        {
            var mr = new MessageRecorder<bool>();
            if (keys.IsNullOrEmpty()) return mr;

            var service = new AliyunOssService();
            foreach (var key in keys)
            {
                mr.Append(service.DeleteObject(key));
            }
            return mr;
        }

        /// <summary>
        /// 获得访问地址
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetUrl(string key)
        {
            return AliyunOssService.GetUrl(key);
        }

        private static MemoryStream ReadFully(Stream input)
        {
            var buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms;
            }
        }
    }
}
