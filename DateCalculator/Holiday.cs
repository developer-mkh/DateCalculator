using System;

namespace DateCalculator.entity
{
    /// <summary>
    /// Holidayテーブルに対するエンティティクラス
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// 休日
        /// </summary>
        public DateTime date { get; set; }
        
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string category { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
    }
}
