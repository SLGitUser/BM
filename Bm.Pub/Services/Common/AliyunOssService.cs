using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aliyun.OSS;
using Bm.Models.Common;


namespace Bm.Services.Common
{
    public class AliyunOssService
    {
        private static string _accessKeyId;
        private static string _accessKeySecret;
        private static string _endpoint;
        private static string _bucketName;

        private OssClient _client;

        static AliyunOssService()
        {
            _accessKeyId = CommonSetService.GetString("Aliyun.Oss.AccessKeyId");
            _accessKeySecret = CommonSetService.GetString("Aliyun.Oss.AccessKeySecret");
            _endpoint = CommonSetService.GetString("Aliyun.Oss.EndPoint");
            _bucketName = CommonSetService.GetString("Aliyun.Oss.BucketName");
        }

        public AliyunOssService()
        {
            _client = new OssClient(_endpoint, _accessKeyId, _accessKeySecret);
        }

        /// <summary>
        /// 设置存储空间ACL的值
        /// </summary>
        public MessageRecorder<bool> SetBucketAcl(CannedAccessControlList cac)
        {
            var r = new MessageRecorder<bool>();
            try
            {
                _client.SetBucketAcl(_bucketName, cac);
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
                var result = _client.GetBucketAcl(_bucketName);
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
                _client.PutObject(_bucketName, key, file, om);
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
                _client.PutObject(_bucketName, key, ms, om);
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
                _client.DeleteObject(_bucketName, key);
                return r.Info("删除文件成功").SetValue(true);
            }
            catch (Exception ex)
            {
                return r.Error($"删除文件失败. 原因：{ex.Message}");
            }
        }
    }
}
