using System;

namespace LotterySln.Model
{
    [Serializable]
    public class UserBetLottery
    {
        #region 成员方法

        public object NumberID { get; set; }
        public object UserID { get; set; }
        public object RunLotteryID { get; set; }
        public decimal TotalPointNum { get; set; }
        public string ItemAppend { get; set; }
        public string BetNumAppend { get; set; }
        public decimal WinPointNum { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        #endregion
    }
}