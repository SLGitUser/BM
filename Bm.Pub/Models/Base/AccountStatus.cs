using Bm.Modules;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bm.Models.Base
{
    /// <summary>
    /// 账户状态
    /// </summary>
    public sealed class AccountStatus
    {
        public enum Type
        {
            [Description("禁用")]
            Forbidden = 0x0,
            [Description("正常")]
            Normal = 0x1,
            [Description("锁定")]
            Locked = 0x2,
            [Description("未激活")]
            Inactive = 0x4
        }

        static AccountStatus()
        {
            if (Dict == null)
            {
                Dict = Enumerations.GenDict<Type>();
            }
        }

        [Description("状态字典")]
        public static readonly IDictionary<Type, string> Dict;

        public static string Status(Type type)
        {
            return Dict.ContainsKey(type) ? Dict[type] : "无效状态";
        }

        [Description("禁用状态")]
        public static readonly Type[] ForbiddenTypes = { Type.Forbidden };

        [Description("锁定状态")]
        public static readonly Type[] LockedTypes = { Type.Locked };

        [Description("正常状态")]
        public static readonly Type[] NormalTypes = { Type.Normal };

        [Description("未激活状态")]
        public static readonly Type[] InactiveTypes = { Type.Inactive };
        
        [Description("可用状态")]
        public static readonly Type[] AvaliableTypes = { Type.Inactive, Type.Locked, Type.Normal };

        [Description("不可用状态")]
        public static readonly Type[] InavaliableTypes = { Type.Locked, Type.Forbidden };

        //[Description("已登录使用账户的状态")]
        //public static readonly Type[] UsedTypes = { Type.Locked, Type.Normal };
    }
}
