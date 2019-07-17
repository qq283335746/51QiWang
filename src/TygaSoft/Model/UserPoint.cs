using System;

namespace LotterySln.Model
{
    [Serializable]
    public class UserPoint
    {
        #region 成员方法

        public object NumberID { get; set; }
        public object UserID { get; set; }
        public decimal PointNum { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        #endregion
    }
}