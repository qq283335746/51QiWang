using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace LotterySln.Web.ScriptServices
{
    /// <summary>
    /// UsersService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class UsersService : System.Web.Services.WebService
    {
        /// <summary>
        /// 获取投注表格中的项
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetBetList()
        {
            string jsonAppend = "[";
            List<Model.LotteryItem> list = WebHelper.LotteryItemDataProxy.GetList();
            if(list != null)
            {
                int index = -1;
                foreach(Model.LotteryItem model in list)
                {
                    index++;
                    if (index > 0) jsonAppend += ",";
                    jsonAppend += "{\"ItemCode\":\"" + model.ItemCode + "\",\"ItemName\":\"" + model.ItemName + "\",\"CurrRatio\":\" X" + model.FixRatio + "\",\"NId\":\"" + model.NumberID + "\",\"betNum\":\"0\",\"multiple\":\"-\"}";
                }
            }

            jsonAppend += "]";

            return jsonAppend;
        }

        /// <summary>
        /// 获取开奖表格中的项
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetSinglePeriod(string numberId)
        {
            if (string.IsNullOrEmpty(numberId))
            {
                return "[]";
            }

            Guid gId = Guid.Empty;
            Guid.TryParse(numberId, out gId);
            if (gId == Guid.Empty)
            {
                return "[]";
            }

            Model.RunLottery rlModel = WebHelper.RunLotteryDataProxy.GetModel(gId);
            if (rlModel == null)
            {
                return "[]";
            }

            List<Model.UserBetLottery> ublList = WebHelper.UserBetLotteryDataProxy.GetListByRunLotteryID(gId);
            bool isUblList = true;
            if (ublList == null || ublList.Count == 0) isUblList = false;

            List<Model.LotteryItem> list = WebHelper.LotteryItemDataProxy.GetList();

            string jsonAppend = "[";
            
            if (list != null)
            {
                string itemName = list.Find(x => x.ItemCode == rlModel.LotteryNum).ItemName;

                decimal allTotalBetNum = 0;
                decimal allTotalWinNum = 0;

                int index = -1;
                foreach (Model.LotteryItem model in list)
                {
                    decimal currTotalBetNum = 0;
                    decimal currTotalWinNum = 0;
                    if (isUblList)
                    {
                        List<Model.UserBetLottery> currUblList = ublList.FindAll(x => x.ItemAppend.Contains(model.ItemCode));
                        if (currUblList != null && currUblList.Count > 0)
                        {
                            foreach (Model.UserBetLottery currUblModel in currUblList)
                            {
                                if (model.ItemCode == rlModel.LotteryNum)
                                {
                                    currTotalWinNum += currUblModel.WinPointNum;
                                }

                                string[] currItemArr = currUblModel.ItemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                string[] currBetNumArr = currUblModel.BetNumAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                int currItemArrLen = currItemArr.Length;
                                for (int i = 0; i < currItemArrLen; i++)
                                {
                                    if (currItemArr[i] == model.ItemCode)
                                    {
                                        currTotalBetNum += Int32.Parse(currBetNumArr[i]);
                                    }
                                }
                            }
                        }
                    }

                    allTotalBetNum += currTotalBetNum;
                    allTotalWinNum += currTotalWinNum;

                    string sCurrBetNum = currTotalBetNum > 0 ? currTotalBetNum.ToString() : "-";
                    string sCurrWinNum = currTotalWinNum > 0 ? currTotalWinNum.ToString() : "-";

                    index++;
                    if (index > 0) jsonAppend += ",";
                    jsonAppend += "{\"ItemCode\":\"" + model.ItemCode + "\",\"ItemName\":\"" + model.ItemName + "\",\"CurrRatio\":\" X" + model.FixRatio + "\",\"NId\":\"" + model.NumberID + "\",\"BetNum\":\"" + sCurrBetNum + "\",\"WinNum\":\"" + sCurrWinNum + "\",\"allBetNum\":\"allBetNumV\",\"allWinNum\":\"allWinNumV\",\"allBetWinNum\":\"allBetWinNumV\",\"allBetWinRatio\":\"allBetWinRatioV\",\"Period\":\"" + rlModel.Period + "\",\"LotteryNum\":\"" + itemName + "\",\"RunDate\":\"" + rlModel.RunDate.ToString("MM/dd HH:mm") + "\"}";
                }

                string allBetWinRatio = "0";
                if (allTotalWinNum > 0)
                {
                    allBetWinRatio = "1:" + Math.Round(allTotalBetNum / allTotalWinNum, 0) + "";
                }

                jsonAppend = jsonAppend.Replace("allBetNumV", allTotalBetNum.ToString());
                jsonAppend = jsonAppend.Replace("allWinNumV", allTotalWinNum.ToString());
                jsonAppend = jsonAppend.Replace("allBetWinNumV", (allTotalWinNum - allTotalWinNum).ToString());
                jsonAppend = jsonAppend.Replace("allBetWinRatioV", allBetWinRatio);
            }

            jsonAppend += "]";

            return jsonAppend;
        }

        /// <summary>
        /// 投注
        /// </summary>
        /// <param name="itemAppend"></param>
        /// <returns></returns>
        [WebMethod]
        public string BetLottery(string itemAppend)
        {
            if (string.IsNullOrEmpty(itemAppend))
            {
                return "无投注项";
            }

            return "投注成功";
        }
    }
}
