using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Modules.Helper;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using log4net;

namespace Bm.Services.Common
{


    /// <summary>
    /// 附件上传服务
    /// </summary>
    public class UploadService : RepoService<UploadFile>
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UploadService(string accountNo) : base(accountNo)
        {
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<UploadFile> Find<TModel>(TModel model)
            where TModel : INo
        {
            if (model == null) return new List<UploadFile>();
            var resType = typeof(TModel).Name;
            var resNo = model.No;
            return ConnectionManager.ExecuteResult(
                conn =>
                {
                    var query = new Criteria<UploadFile>()
                        .Where(m => m.ResType, Op.Eq, resType)
                        .And(m => m.ResNo, Op.Eq, resNo);
                    return conn.Query(query);
                });
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <typeparam name="TResourceType">资源类型</typeparam>
        /// <param name="request">HttpRequest</param>
        /// <param name="settings">指定的上传设置</param>
        /// <param name="inputName">获取指定Input控件的文件</param>
        /// <returns>上传结果信息</returns>
        public UploadResult DoUpload<TResourceType>(HttpRequestBase request, 
            UploadSettings settings = null, string inputName = null)
        {
            return DoUpload(request, typeof(TResourceType).Name, settings, inputName);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="request">HttpRequest</param>
        /// <param name="type">上传附件类型</param>
        /// <param name="settings">指定的上传设置</param>
        /// <param name="inputName">获取指定Input控件的文件</param>
        /// <returns>
        /// 上传结果信息
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// request;请求失效
        /// or
        /// request;指定的用户不存在
        /// </exception>
        /// <exception cref="System.Data.DataException">数据库写入失败</exception>
        public UploadResult DoUpload(HttpRequestBase request, string type,
            UploadSettings settings = null, string inputName = null)
        {
            var result = new UploadResult { Errors = new List<UploadError>() };

            if (!string.IsNullOrEmpty(inputName) && !request.Files.AllKeys.Contains(inputName))
            {
                result.Errors.Add(new UploadError
                {
                    ErrorType = UploadErrorType.指定的文件不存在,
                    InputName = inputName
                });
                return result;
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "请求失效");
            }

            result.Uploads = new List<UploadFile>();

            //如果没有指定设置文件，则获取默认的设置项
            if (settings == null)
            {
                settings = GetSettings();
            }

            for (var i = 0; i < request.Files.Count; i++)
            {
                var upload = request.Files.AllKeys[i];
                //如果指定了Input名称，则寻找对应的Input执行上传操作
                if (Equals(upload, inputName)) continue;

                var file = request.Files[i];
                if (file == null) continue;
                if (file.ContentLength <= 0) continue;

                //最大文件长度控制
                if (settings.MaxLength > 0 && file.ContentLength > settings.MaxLength)
                {
                    result.Errors.Add(new UploadError
                    {
                        ErrorType = UploadErrorType.文件过大,
                        FileId = i,
                        FileName = file.FileName,
                        InputName = upload
                    });
                    continue;
                }

                //按文件扩展名过滤
                if (settings.AllowExts != "*")
                {

                    var fileExtName = Path.GetExtension(file.FileName);

                    //找不到文件的时候
                    if (fileExtName == null)
                    {
                        result.Errors.Add(new UploadError
                        {
                            ErrorType = UploadErrorType.指定的文件不存在,
                            FileId = i,
                            FileName = file.FileName,
                            InputName = upload
                        });
                        continue;
                    }

                    //允许的扩展名列表
                    var allowExts = settings.AllowExts.Split(',');
                    if (!allowExts.Contains(fileExtName))
                    {
                        result.Errors.Add(new UploadError
                        {
                            ErrorType = UploadErrorType.扩展名未在允许列表,
                            FileId = i,
                            FileName = file.FileName,
                            InputName = upload
                        });
                        continue;
                    }
                }

                //文件上传路径
                var path = ConfigurationManager.AppSettings["UploadPath"] ?? AppDomain.CurrentDomain.BaseDirectory + "uploads/";

                path = Path.Combine(path, DateTime.Now.ToString("yyyyMM"));

                //如果没找到路径就自己建个
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = Path.GetFileName(file.FileName);
                var mimeType = file.ContentType;
                var fileTruename = filename;

                //用guid作为其储存文件名
                filename = Guid.NewGuid().ToString();
                try
                {
                    file.SaveAs(Path.Combine(path, filename));
                }
                catch (IOException)//文件IO错误，绝大部分是因为空间不足，另一部分是因为服务器权限。
                {
                    result.Errors.Add(new UploadError
                    {
                        ErrorType = UploadErrorType.服务器磁盘空间不足,
                        FileId = i,
                        FileName = file.FileName,
                        InputName = upload
                    });
                    continue;
                }
                catch (Exception)
                {
                    result.Errors.Add(new UploadError
                    {
                        ErrorType = UploadErrorType.未知错误,
                        FileId = i,
                        FileName = file.FileName,
                        InputName = upload
                    });
                    continue;
                }

                var u = new UploadFile
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = AccountNo,
                    Mime = mimeType,
                    Name = fileTruename,
                    ResType = type,
                    No = filename,
                    Path = Path.Combine(path, filename),
                    Size = file.ContentLength
                };
                result.Uploads.Add(u);
            }

            //如果没有成功成功上传的文件或成功写入数据库，则正常返回
            if (!result.Uploads.Any() || result.Uploads.All(m=>!Create(m).HasError))
            {
                return result;
            }

            //数据库写入失败执行还原操作
            foreach (var upload in result.Uploads)
            {
                File.Delete(upload.Path);
            }
            result.Uploads.Clear();
            result.Uploads = null;
            result.Errors.Clear();
            throw new DataException("数据库写入失败");
        }


        /// <summary>
        /// Downloads the specified identifier.
        /// </summary>
        /// <param name="u"></param>
        /// <returns>ActionResult.</returns>
        public ActionResult Download(UploadFile u)
        {
            return new FilePathResult(u.Path, u.Mime);
        }


        public UploadFile GetByUuid(string uuid)
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<UploadFile>()
                    .Where(m=>m.No, Op.Eq, uuid)
                    .Limit(1);
                return conn.Query(query).FirstOrDefault();
            }
        }

        public ActionResult Download(string uuid)
        {
            var u = GetByUuid(uuid);
            return Download(u);
        }


        /// <summary>
        /// Deletes the specified upload.
        /// </summary>
        /// <param name="conn">The session.</param>
        /// <param name="upload">The upload.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Delete(IDbConnection conn, UploadFile upload)
        {
            if (conn == null)
            {
                return false;
            }
            if (upload == null)
            {
                return true;
            }
            if (!conn.Delete(upload))
            {
                return false;
            }
            try
            {
                File.Delete(upload.Path);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes the specified uploads.
        /// </summary>
        /// <param name="uploads">The uploads.</param>
        /// <param name="conn">The session.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Delete(IDbConnection conn, IList<UploadFile> uploads)
        {
            if (conn == null)
            {
                return false;
            }
            if (!uploads.Any())
            {
                return true;
            }
            if (!conn.Delete(uploads))
            {
                return false;
            }
            foreach (var upload in uploads)
            {
                try
                {
                    File.Delete(upload.Path);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    Log.Error(e.StackTrace);
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 获取上传设置
        /// </summary>
        /// <param name="resType">上传类型，为null则获取全局设置</param>
        /// <returns>
        /// 与类型对应的上传设置
        /// </returns>
        public static UploadSettings GetSettings(string resType = null)
        {
            var exts = string.IsNullOrEmpty(resType)
                ? CommonSetService.GetString("Upload.Global.Exts")
                : CommonSetService.GetString($"Upload.{resType}.Exts")
                    .SetWhenNullOrEmpty(CommonSetService.GetString("Upload.Global.Exts"));
            var maxLen = string.IsNullOrEmpty(resType)
                ? CommonSetService.GetString("Upload.Global.MaxLength")
                : CommonSetService.GetString($"Upload.{resType}.MaxLength")
                    .SetWhenNullOrEmpty(CommonSetService.GetString("Upload.Global.MaxLength"));
            return new UploadSettings
            {
                AllowExts = exts,
                MaxLength = maxLen.TryToLong().GetValueOrDefault(0)
            };
        }


        ///// <summary>
        ///// Gets the default ext settings.
        ///// </summary>
        ///// <returns></returns>
        //private static Setting GetDefaultExtSettings()
        //{
        //    var setting = SessionHelper.Load<Setting>(m => m.Category.Equals("上传设置_全局") && m.Name.Equals("可用扩展名"));

        //    if (setting != null)
        //    {
        //        return setting;
        //    }

        //    setting = new Setting
        //    {
        //        Category = "上传设置_全局",
        //        Code = Setting.UploadSettingOfExt,
        //        Name = "可用扩展名",
        //        Value = "*",//格式为英文逗号连接并带点"."，如“.jpg,.png,.mpeg,.mp3”,星号表示不限制
        //        Description = "全局上传设置-可用扩展名",
        //        CreatedBy = "SYSTEM",
        //        CreatedAt = DateTime.Now
        //    };
        //    SessionHelper.Create(setting);
        //    return setting;
        //}

        ///// <summary>
        ///// Gets the default length settings.
        ///// </summary>
        ///// <returns></returns>
        //private static Setting GetDefaultLengthSettings()
        //{
        //    var setting = SessionHelper.Load<Setting>(m => m.Category.Equals("上传设置_全局") && m.Name.Equals("允许单文件最大字节"));

        //    if (setting != null)
        //    {
        //        return setting;
        //    }
        //    setting = new Setting
        //    {
        //        Category = "上传设置_全局",
        //        Code = Setting.UploadSettingOfLength,
        //        Name = "允许单文件最大字节",
        //        Value = string.Format("{0}", 1024 * 1024 * 5), //5MB
        //        Description = "全局上传设置-允许单文件最大字节（默认5MB，0为不做限制）",
        //        CreatedBy = "SYSTEM",
        //        CreatedAt = DateTime.Now
        //    };
        //    SessionHelper.Create(setting);
        //    return setting;
        //}

    }


    /// <summary>
    /// 上传结果
    /// </summary>
    public class UploadResult
    {
        /// <summary>
        ///读取或者设置上传的文件结果
        /// </summary>
        /// <value>
        /// The uploads.
        /// </value>
        public IList<UploadFile> Uploads { get; set; }

        /// <summary>
        /// 读取或者设置上传的错误信息
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IList<UploadError> Errors { get; set; }

        /// <summary>
        /// 读取是否有错误
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has error]; otherwise, <c>false</c>.
        /// </value>
        public bool HasError
        {
            get { return Errors != null && Errors.Any(); }
        }

        /// <summary>
        /// 读取是否有上传文件
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has upload]; otherwise, <c>false</c>.
        /// </value>
        public bool HasUpload
        {
            get { return Uploads != null && Uploads.Any(); }
        }

    }

    /// <summary>
    /// 上传错误
    /// </summary>
    public class UploadError
    {

        /// <summary>
        ///读取或者设置出错文件序号.
        /// </summary>
        /// <value>
        /// 出错文件序号
        /// </value>
        public long FileId { get; set; }


        /// <summary>
        ///读取或者设置出错文件名称.
        /// </summary>
        /// <value>
        /// 出错文件名称
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        ///读取或者设置出错控件名.
        /// </summary>
        /// <value>
        /// 出错控件名
        /// </value>
        public string InputName { get; set; }


        /// <summary>
        ///读取或者设置错误类型.
        /// </summary>
        /// <value>
        /// 错误类型
        /// </value>
        public UploadErrorType ErrorType { get; set; }
    }


    /// <summary>
    /// 上传错误类型
    /// </summary>
    public enum UploadErrorType
    {
        /// <summary>
        /// 扩展名未在上传设置的允许列表
        /// </summary>
        扩展名未在允许列表,
        /// <summary>
        /// 文件过大，超出上传设置的限制
        /// </summary>
        文件过大,
        /// <summary>
        /// 服务器磁盘空间不足，不能保存文件
        /// </summary>
        服务器磁盘空间不足,
        /// <summary>
        /// 指定的文件不存在
        /// </summary>
        指定的文件不存在,
        /// <summary>
        /// 未知错误
        /// </summary>
        未知错误
    }
}