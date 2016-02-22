using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Aliyun.OSS;
using Aliyun.OSS.Model;
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
            return string.Concat(Endpoint.Replace(@"//", @"//" + BucketName + "."), "/", key);
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
        public MessageRecorder<bool> PutObject(string key, Stream ms, ObjectMetadata om = null)
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

        /// <summary>
        /// 获取元数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MessageRecorder<ObjectMetadata> GetObjectMetadata(string key)
        {
            var r = new MessageRecorder<ObjectMetadata>();
            try
            {
                r.Value = _client.GetObjectMetadata(BucketName, key);
                return r.Info("获取文件元数据成功");
            }
            catch (Exception ex)
            {
                return r.Error($"获取文件元数据失败. 原因：{ex.Message}");
            }
        }

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public MessageRecorder<bool> ModifyObjectMeta(string key,
            Action<ObjectMetadata> action)
        {
            var r = new MessageRecorder<bool>();
            try
            {
                var meta = _client.GetObjectMetadata(BucketName, key);
                var newMeta = new ObjectMetadata { ContentLength = meta.ContentLength };
                if (!string.IsNullOrEmpty(meta.ContentType))
                    newMeta.ContentType = meta.ContentType;
                if (!string.IsNullOrEmpty(meta.CacheControl))
                    newMeta.CacheControl = meta.CacheControl;
                if (!string.IsNullOrEmpty(meta.ContentDisposition))
                    newMeta.ContentDisposition = meta.ContentDisposition;
                if (!string.IsNullOrEmpty(meta.ContentEncoding))
                    newMeta.ContentEncoding = meta.ContentEncoding;
                //TODO 等待修复取值错误 
                //newMeta.ExpirationTime = meta.ExpirationTime;
                action?.Invoke(newMeta);
                _client.ModifyObjectMeta(BucketName, key, newMeta);
                return r.Info("修改文件元数据成功").SetValue(true);
            }
            catch (Exception ex)
            {
                return r.Error($"修改文件元数据失败. 原因：{ex.Message}");
            }
        }
    }
}
