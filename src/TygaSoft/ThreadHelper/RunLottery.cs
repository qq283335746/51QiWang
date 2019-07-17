using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotterySln.BLL;
using ThreadHelper;

namespace LotterySln.ThreadHelper
{
    public class RunLottery : BaseThread
    {
        Model.RunLottery model;
        BLL.RunLottery bll;

        public RunLottery(Model.RunLottery model) 
        {
            this.model = model;
        }

        public override void ThreadWork()
        {
            int totalCount = 0;
            BLL.UserBetLottery ublBll = new UserBetLottery();
            List<Model.UserBetLottery> ublList = ublBll.GetList(1, 10000000, out totalCount, "and RunLotteryID = '" + model.NumberID + "' ", null);
            if (ublList != null && ublList.Count > 0)
            {
 
            }
        }
    }
}
