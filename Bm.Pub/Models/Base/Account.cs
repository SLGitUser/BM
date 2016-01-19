using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using com.senlang.Sdip.Data;
using com.senlang.Sdip.Util;

namespace Bm.Models.Base
{
    /// <summary>
    /// 账户
    /// </summary>
    [DisplayName("账户")]
    public sealed class Account : IId, IStamp
    {
        #region Implementation of IId

        /// <summary>
        /// 读取或者设置记录序号
        /// </summary>
        /// <value>
        /// 记录序号
        /// </value>
        /// <remarks>
        /// 建议存储时使用无符号类型
        /// </remarks>
        [DisplayName("记录序号")]
        public long Id { get; set; }

        #endregion

        #region Implementation of ICreateStamp

        /// <summary>
        /// 读取或者设置记录创建人
        /// </summary>
        /// <value>
        /// 记录创建人
        /// </value>
        /// <remarks>
        /// 建议使用操作人的账户名
        /// </remarks>
        [DisplayName("记录创建人")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 读取或者设置记录创建时间
        /// </summary>
        /// <value>
        /// 记录创建时间
        /// </value>
        /// <remarks>
        /// 建议使用服务器时间，不要使用默认空值
        /// </remarks>
        [DisplayName("记录创建时间")]
        public DateTime CreatedAt { get; set; }

        #endregion

        #region Implementation of IUpdateStamp

        /// <summary>
        /// 读取或者设置记录更新人
        /// </summary>
        /// <value>
        /// 记录更新人
        /// </value>
        /// <remarks>
        /// 建议使用操作人的账户名，为空时表示记录无更新
        /// </remarks>
        [DisplayName("记录最后修改人")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 读取或者设置记录更新时间
        /// </summary>
        /// <value>
        /// 记录更新时间
        /// </value>
        /// <remarks>
        /// 为空时表示记录无更新
        /// </remarks>
        [DisplayName("记录最后修改时间")]
        public DateTime? UpdatedAt { get; set; }

        #endregion

        /// <summary>
        /// 读取或者设置账户状态
        /// </summary>
        /// <remark></remark>
        [DisplayName("账户状态")]
        public AccountStatus.Type Status { get; set; }

        /// <summary>
        /// 读取或者设置账号
        /// </summary>
        /// <remark></remark>
        [DisplayName("账号")]
        [StringLength(50)]
        [Required]
        public string No { get; set; }

        /// <summary>
        /// 读取或者设置账户名
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("账户名")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 读取或者设置电子邮件
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("电子邮件")]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 读取或者设置手机
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("手机")]
        [StringLength(11)]
        public string Phone { get; set; }

        /// <summary>
        /// 读取或者设置备注
        /// </summary>
        /// <remark>
        /// </remark>
        [DisplayName("备注")]
        [StringLength(50)]
        public string Remark { get; set; }
        
        /// <summary>
        /// 密码，只写
        /// </summary>
        [DisplayName(@"密码")]
        public string Password
        {
            get { return null; }
            set
            {
                if (PasswordSalt == null || PasswordSalt.Length < 32)
                {
                    _passwordSalt = GetSalt();
                }
                PasswordHash = Encrypt(_passwordSalt, value);
            }
        }

        /// <summary>
        /// 密码的加密值
        /// </summary>
        [DisplayName(@"密码密文")]
        [StringLength(32)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 密码加密种子
        /// </summary>
        [DisplayName(@"加密种子")]
        [StringLength(32)]
        public string PasswordSalt
        {
            get { return _passwordSalt; }
            set
            {
                if (value == null || value.Length < 32)
                {
                    throw new Exception("密码种子不能为空，长度要大于等于32.");
                }
                _passwordSalt = value;
            }
        }

        /// <summary>
        /// 读取或者设置是否系统用户
        /// </summary>
        /// <remark></remark>
        [DisplayName("系统用户")]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 读取或者设置皮肤主题
        /// </summary>
        /// <remark></remark>
        [DisplayName("皮肤主题")]
        [StringLength(32)]
        public string ThemeName { get; set; }

        public const int MaxErrLoginCount = 5;

        /// <summary>
        /// 读取或者设置重置密码验证码
        /// </summary>
        /// <remark></remark>
        [DisplayName("重置密码验证码")]
        [StringLength(8)]
        public string ResetFlag { get; set; }


        [DisplayName(@"登录次数")]
        public int LoginCount { get; set; }

        [DisplayName(@"最后登录时间")]
        public DateTime? LastLoginAt { get; set; }

        [DisplayName(@"错误登录次数")]
        public int ErrLoginCount { get; set; }

        [DisplayName(@"最后错误登录时间")]
        public DateTime? ErrLoginAt { get; set; }

        /// <summary>
        /// 获得验证码
        /// </summary>
        /// <returns></returns>
        public static string GetResetFlag()
        {
            var result = string.Empty;
            while (result.Length < 6)
            {
                var str = GetSalt();
                foreach (var x in str)
                {
                    if (x < '0' || x > '9') continue;
                    result += x;
                    if (result.Length == 6) break;
                }
            }
            return result;
        }

        /// <summary>
        /// 认证密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AuthPassword(string password)
        {
            if (string.IsNullOrEmpty(Name)) return false;
            if (string.IsNullOrEmpty(password)) return false;
            if (string.IsNullOrEmpty(PasswordHash)) return false;
            string passwordHash = Encrypt(PasswordSalt, password);
            return PasswordHash.EqualsIgnoreCase(passwordHash);
        }
        
        /// <summary>
        /// 获得随机的唯一密码种子
        /// </summary>
        /// <returns></returns>
        private static string GetSalt()
        {
            return Guid.NewGuid().ToString().Md5Hash();
        }

        private string Encrypt(string salt, string password)
        {
            return string.Concat(Name, "-", salt, "-", password).Md5Hash();
        }
        
        private string _passwordSalt;
    }
}