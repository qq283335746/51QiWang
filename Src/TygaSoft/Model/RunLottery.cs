using System;

namespace LotterySln.Model
{
    [Serializable]
    public class RunLottery
    {
        #region 成员方法

        public object NumberID { get; set; }
        public int Period { get; set; }
        public string LotteryNum { get; set; }
        public DateTime RunDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public short Status { get; set; }
        public decimal BetNum { get; set; }
        public decimal TotalPointNum { get; set; }
        public int WinnerNum { get; set; }
        public decimal WinPointNum { get; set; }

        //以下字段数据表中不存在，属业务需要
        public string ItemName { get; set; }

        #endregion
    }
}
