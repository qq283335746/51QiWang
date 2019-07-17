using System;

namespace LotterySln.Model
{
    [Serializable]
    public class LotteryItem
    {
        #region 成员方法

        public object NumberID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ImageUrl { get; set; }
        public decimal FixRatio { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        #endregion
    }
}