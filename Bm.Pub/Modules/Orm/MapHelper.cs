namespace Bm.Modules.Orm
{
    /// <summary>
    /// 数据库映射辅助类
    /// </summary>
    public class MapHelper
    {
        public static string GetTableName<T>()
        {
            return NamingHelper.MapClassToTable(typeof (T).FullName);
        }
    }
}
