namespace Bm.Modules.Orm.Sql
{
    public enum Op
    {
        #region 一元谓词

        /// <summary>
        /// Not
        /// </summary>
        Not,
        /// <summary>
        /// IsNull
        /// </summary>
        Nul,
        /// <summary>
        /// IsNotNull
        /// </summary>
        NotNul,

        #endregion

        #region 二元谓词（比较）

        /// <summary>
        /// Equals
        /// </summary>
        Eq,
        /// <summary>
        /// GreateThan
        /// </summary>
        Gt,
        /// <summary>
        /// GreatThanOrEquals
        /// </summary>
        Ge,
        /// <summary>
        /// LessThan
        /// </summary>
        Lt,
        /// <summary>
        /// LessThanOrEquals
        /// </summary>
        Le,
        /// <summary>
        /// NotEquals
        /// </summary>
        NotEq,

        #endregion

        #region 二元谓词（包含）
        
        /// <summary>
        /// StartWith
        /// </summary>
        Sw,
        /// <summary>
        /// NotStartWith
        /// </summary>
        NotSw,
        /// <summary>
        /// EndWtih
        /// </summary>
        Ew,
        /// <summary>
        /// NotEndWith
        /// </summary>
        NotEw,
        /// <summary>
        /// Contains
        /// </summary>
        Ct,
        /// <summary>
        /// NotContains
        /// </summary>
        NotCt,

        #endregion

        #region 二元谓词（集合）
        
        /// <summary>
        /// In
        /// </summary>
        In,
        /// <summary>
        /// NotIn
        /// </summary>
        NotIn

        #endregion

    }
}
