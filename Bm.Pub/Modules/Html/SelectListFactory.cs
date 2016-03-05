using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Bm.Models.Dp;
using Bm.Modules.Helper;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;

namespace Bm.Modules.Html
{
    public sealed class SelectListFactory
    {

        #region private

        /// <summary>
        /// 转换为选择列表
        /// </summary>
        /// <param name="items">实例集合</param>
        /// <param name="selectMode">选择模式</param>
        /// <param name="selectedValue">默认选中值</param>
        /// <param name="extOptionText">额外选项文本</param>
        /// <returns></returns>
        private static SelectList ToSelectList(IEnumerable<KeyValuePair<string, string>> items,
            SelectMode selectMode, object selectedValue, string extOptionText)
        {
            var list = items.ToList();
            switch (selectMode)
            {
                case SelectMode.Default:
                    break;
                case SelectMode.AddAllByString:
                    list.Insert(0, new KeyValuePair<string, string>("", extOptionText ?? "全部"));
                    break;
                case SelectMode.AddAllByNumber:
                    list.Insert(0, new KeyValuePair<string, string>("0", extOptionText ?? "全部"));
                    break;
                case SelectMode.AddNoneByString:
                    list.Insert(0, new KeyValuePair<string, string>("", extOptionText ?? "无"));
                    break;
                case SelectMode.AddNoneByNumber:
                    list.Insert(0, new KeyValuePair<string, string>("0", extOptionText ?? "无"));
                    break;
                case SelectMode.AddEmpty:
                    list.Insert(0, new KeyValuePair<string, string>("", extOptionText ?? string.Empty));
                    break;
            }
            return new SelectList(list, "Key", "Value", selectedValue);
        }

        #endregion

        #region 枚举

        /// <summary>
        /// 选择列表
        /// </summary>
        /// <typeparam name="TModel">模型类型</typeparam>
        /// <param name="models">实例集合</param>
        /// <param name="valueFunc">Value函数</param>
        /// <param name="textFunc">Text函数</param>
        /// <param name="selectMode">选择模式</param>
        /// <param name="selectedValue">选中值</param>
        /// <param name="extOptionText">额外选项的文本</param>
        /// <returns></returns>
        public static SelectList SelectList<TModel>(IEnumerable<TModel> models,
            Func<TModel, string> valueFunc, Func<TModel, string> textFunc,
            SelectMode selectMode = SelectMode.Default, object selectedValue = null, string extOptionText = null)
        {
            var items = from TModel model in models
                        where model != null
                        select new KeyValuePair<string, string>(valueFunc(model), textFunc(model));
            return ToSelectList(items, selectMode, selectedValue, extOptionText);
        }

        /// <summary>
        /// 选择列表
        /// </summary>
        /// <param name="models">实例集合</param>
        /// <param name="selectMode">选择模式</param>
        /// <param name="selectedValue">选中值</param>
        /// <param name="extOptionText">额外选项的文本</param>
        /// <returns></returns>
        public static SelectList SelectList(string[] models,
            SelectMode selectMode = SelectMode.Default, string selectedValue = null, string extOptionText = null)
        {
            var items = models.Select(m => new KeyValuePair<string, string>(m, m));
            return ToSelectList(items, selectMode, selectedValue, extOptionText);
        }


        #endregion

        #region 枚举类型

        /// <summary>
        /// 自动将枚举类型转换为下拉列表，标签为描述文字，值为枚举字符串
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="models">实例集合</param>
        /// <param name="selectMode">选择模式</param>
        /// <param name="selectedValue">选中值</param>
        /// <param name="extOptionText">额外选项的文本</param>
        /// <returns></returns>
        public static SelectList SelectStringList<TEnum>(TEnum[] models = null,
            SelectMode selectMode = SelectMode.Default, string selectedValue = null, string extOptionText = null)
            where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new Exception($"{enumType.FullName}不是枚举类型，不能生成选择列表");

            var items = from TEnum enumValue in models ?? enumType.GetEnumValues()
                        let fi = enumType.GetField(enumValue.ToString())
                        let de = fi.GetAttribute<DescriptionAttribute>()?.Description ?? enumValue.ToString()
                        select new KeyValuePair<string, string>(de, enumValue.ToString());
            return ToSelectList(items, selectMode, selectedValue, extOptionText);
        }

        /// <summary>
        /// 自动将枚举类型转换为下拉列表，标签为描述文字，值为枚举字符串
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="models">实例集合</param>
        /// <param name="selectMode">选择模式</param>
        /// <param name="selectedValue">选中值</param>
        /// <param name="extOptionText">额外选项的文本</param>
        /// <returns></returns>
        public static SelectList SelectNumberList<TEnum>(TEnum[] models = null,
            SelectMode selectMode = SelectMode.Default, string selectedValue = null, string extOptionText = null)
            where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new Exception($"{enumType.FullName}不是枚举类型，不能生成选择列表");

            var items = from TEnum enumValue in models ?? enumType.GetEnumValues()
                        let fi = enumType.GetField(enumValue.ToString())
                        let de = fi.GetAttribute<DescriptionAttribute>()?.Description ?? enumValue.ToString()
                        select new KeyValuePair<string, string>(de, ((int)(ValueType)enumValue).ToString());
            return ToSelectList(items, selectMode, selectedValue, extOptionText);
        }

        #endregion

        #region 页面需求

        /// <summary>
        /// [是否]下拉框
        /// </summary>
        /// <param name="selectMode"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static SelectList YesOrNo(SelectMode selectMode = SelectMode.Default, string selectedValue = null)
        {
            return SelectList(new[] { "是", "否" }, selectMode, selectedValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectMode"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static SelectList NoOrYes(SelectMode selectMode = SelectMode.Default, string selectedValue = null)
        {
            return SelectList(new[] { "否", "是" }, selectMode, selectedValue);
        }

        /// <summary>
        /// 性别下拉框
        /// </summary>
        /// <param name="selectMode"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static SelectList SexSelectList(SelectMode selectMode = SelectMode.Default, string selectedValue = null)
        {
            return SelectList(new[] { "男", "女" }, selectMode, selectedValue);
        }

        /// <summary>
        /// 查询所有开发商编号
        /// </summary>
        /// <returns></returns>
        public static SelectList GetDeveloperAllNo(SelectMode selectMode = SelectMode.Default, object selectedValue = null)
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<Developer>()
                    .Desc(m => m.No);
                var models = conn.Query(query)
                    .Select(m => new
                    {
                        m.No,
                        Text = $"[{m.No}]{m.Name}"
                    }).ToList();
                return SelectList(models, m => m.No, m => m.Text, selectMode, selectedValue);
            }
        }
        /// <summary>
        /// 获取所有经纪人编号
        /// </summary>
        /// <returns></returns>
        public static SelectList BrokerageAllNo(SelectMode selectMode = SelectMode.Default, object selectedValue = null)
        {
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<BrokerageFirm>()
                    .And(m => m.Id, Op.Gt, 0)
                    .And(m => m.Id, Op.NotIn, new long[] { 0, -1 })
                    .Desc(m => m.FirmNo);
                var models = conn.Query(query)
                    .Select(m => new
                    {
                        m.FirmNo,
                        Text = $"[{m.FirmNo}]{m.Name}"
                    }).ToList();
                return SelectList(models, m => m.FirmNo, m => m.Text, selectMode, selectedValue);
            }

        }

        /// <summary>
        /// 楼盘类型
        /// </summary>
        /// <returns></returns>
        public static SelectList GetProjectType(SelectMode selectMode = SelectMode.Default, object selectedValue = null)
        {
            //TODO 针对不同的地区读取不同的楼盘类型
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<AreaTag>()
                    .Where(m => m.Type, Op.Eq, "T")
                    .Desc(m => m.No);
                var models = conn.Query(query);
                return SelectList(models, m => m.Name, m => m.Name, selectMode, selectedValue);
            }
        }
        #endregion

    }
}

