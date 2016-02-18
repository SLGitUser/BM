using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aliyun.OSS;
using Bm.Models.Common;


namespace Bm.Services.Common
{
    internal class AliyunOssService
    {
        private static readonly string AccessKeyId;
        private static readonly string AccessKeySecret;
        private static readonly string Endpoint;
        private static readonly string BucketName;

        private readonly OssClient _client;

        static AliyunOssService()
        {
            AccessKeyId = CommonSetService.GetString("Aliyun.Oss.AccessKeyId");
            AccessKeySecret = CommonSetService.GetString("Aliyun.Oss.AccessKeySecret");
            Endpoint = CommonSetService.GetString("Aliyun.Oss.EndPoint");
            BucketName = CommonSetService.GetString("Aliyun.Oss.BucketName");
        }

        /// <summary>
        /// 获得资源访问地址
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetUrl(string key)
        {
            return string.Concat(Endpoint, "/", BucketName, "/", key);
        }

        public AliyunOssService()
        {
            _client = new OssClient(Endpoint, AccessKeyId, AccessKeySecret);
        }

        /// <summary>
        /// 设置存储空间ACL的值
        /// </summary>
        public MessageRecorder<bool> SetBucketAcl(CannedAccessControlList cac)
        {
            var r = new MessageRecorder<bool>();
            try
            {
                _client.SetBucketAcl(BucketName, cac);
                return r.Info("设置存储空间权限成功").SetValue(true);
            }
            catch (Exception ex)
            {
                return r.Error($"设置存储空间权限失败. 原因：{ex.Message}");
            }
        }


        /// <summary>
        /// 获取存储空间ACL的值
        /// </summary>
        public MessageRecorder<IList<Permission>> GetBucketAcl()
        {
            var r = new MessageRecorder<IList<Permission>>();
            try
            {
                var result = _client.GetBucketAcl(BucketName);
                r.Value = result.Grants.Select(m => m.Permission).ToList();
                return r;
            }
            catch (Exception ex)
            {
                return r.Error($"获取存储空间权限失败. 原因：{ex.Message}");
            }
        }


        /// <summary>
        /// 上传一个新文件
        /// </summary>
        public MessageRecorder<bool> PutObject(string key, string file, ObjectMetadata om = null)
        {
            var r = new MessageRecorder<bool>();
            try
            {
                _client.PutObject(BucketName, key, file, om);
                return r.Info("上传文件成功").SetValue(true);
            }
            catch (Exception ex)
            {
                return r.Error($"上传文件失败. 原因：{ex.Message}");
            }
        }

        /// <summary>
        /// 上传一个新文件
        /// </summary>
        public MessageRecorder<bool> PutObject(string key, MemoryStream ms, ObjectMetadata om = null)
        {
            var r = new MessageRecorder<bool>();
            try
            {
                _client.PutObject(BucketName, key, ms, om);
                return r.Info("上传文件成功").SetValue(true);
            }
            catch (Exception ex)
            {
                return r.Error($"上传文件失败. 原因：{ex.Message}");
            }
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        public MessageRecorder<bool> DeleteObject(string key)
        {
            var r = new MessageRecorder<bool>();
            try
            {
                _client.DeleteObject(BucketName, key);
                return r.Info("删除文件成功").SetValue(true);
            }
            catch (Exception ex)
            {
                return r.Error($"删除文件失败. 原因：{ex.Message}");
            }
        }
    }
}
