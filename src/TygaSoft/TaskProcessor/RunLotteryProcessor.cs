using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Transactions;
using System.Threading;
using LotterySln.BLL;

namespace LotterySln.TaskProcessor
{
    public class RunLotteryProcessor
    {
        public static void TaskStart()
        {
            Thread workThread = new Thread(new ThreadStart(WorkProcess));

            // Make this a background thread, so it will terminate when the main thread/process is de-activated
            workThread.IsBackground = true;
            workThread.SetApartmentState(ApartmentState.STA);

            // Start the Work
            workThread.Start();
        }

        public static void WorkProcess()
        {
            Console.WriteLine("开奖程序已开启 \r\n");
            Random rnd = new Random();

            int[] minItems = { 1, 2, 3 };
            int[] allItems = { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] items;
            //string[] names = { "兵", "炮", "傌", "俥", "相", "仕", "帥", "棋王" };

            int timeNum = 0;
            int totalCount = 0;

            try
            {
                LotteryItem liBll = new LotteryItem();
                List<Model.LotteryItem> liList = liBll.GetList(1, 1000, out totalCount, "", null);
                if (liList == null || liList.Count == 0)
                {
                    Console.WriteLine("系统需要预先设置开奖元素，已终止执行！");
                    Console.ReadLine();
                    return;
                }

                LotterySln.BLL.RunLottery rlBll = new LotterySln.BLL.RunLottery();
                LotterySln.BLL.UserBetLottery ublBll = new LotterySln.BLL.UserBetLottery();
                LotterySln.BLL.UserPoint uBll = new LotterySln.BLL.UserPoint();

                while (true)
                {
                    timeNum++;

                    if (timeNum > 10) timeNum = 0;
                    if (timeNum < 8) items = minItems;
                    else items = allItems;
                    //处于最大的预备时间
                    DateTime maxCurrDate = DateTime.Now;
                    //上一期期数
                    int lastPeriod = 0;
                    //上一期开奖时间
                    DateTime lastRunTime = DateTime.MinValue;
                    //上一次执行时间
                    DateTime currRunTime = DateTime.MinValue;
                    //上期已开奖的数字
                    int lastNum = -1;
                    //当前期ID
                    object numberId = null;
                    DateTime currTime = DateTime.Now;
                    if (currTime < DateTime.Parse(currTime.ToString("yyyy-MM-dd")).AddHours(9))
                    {
                        currTime = DateTime.Parse(currTime.ToString("yyyy-MM-dd")).AddHours(9);
                    }

                    List<Model.RunLottery> rlList = rlBll.GetList(1, 6, out totalCount, "", null);
                    if (rlList == null || rlList.Count == 0)
                    {
                        //每期都要保证有5期是预设奖期，进行投注
                        for (int i = 1; i <= 5; i++)
                        {
                            double currMin = i * 5;
                            Model.RunLottery rlModel = new Model.RunLottery();
                            rlModel.LotteryNum = string.Empty;
                            rlModel.RunDate = currTime.AddMinutes(currMin);
                            rlModel.Status = 0;
                            rlModel.LastUpdatedDate = currTime.AddMinutes(currMin);

                            rlBll.Insert(rlModel);
                        }

                        //睡眠一分钟
                        Thread.Sleep(30000);
                    }
                    else
                    {
                        #region 程序非第一次运行

                        //获取当前即将开奖的期
                        Model.RunLottery currModel = null;

                        //将获取上一期开奖时间和开奖数字
                        List<Model.RunLottery> lastList = rlList.FindAll(x => x.Status == 1);
                        if (lastList != null && lastList.Count > 0)
                        {
                            Model.RunLottery lastModel = lastList.OrderByDescending(x => x.Period).First();
                            if (lastModel != null)
                            {
                                lastRunTime = lastModel.RunDate;
                                lastNum = Int32.Parse(lastModel.LotteryNum);
                                lastPeriod = lastModel.Period;
                            }
                        }

                        List<Model.RunLottery> currReadyList = rlList.FindAll(x => x.Status == 0);
                        if (currReadyList != null && currReadyList.Count > 0)
                        {
                            //获取当前最大开奖时间
                            currModel = currReadyList.OrderByDescending(x => x.Period).First();
                            maxCurrDate = currModel.RunDate;

                            //获取当前即将开奖的期
                            currModel = currReadyList.OrderBy(x => x.Period).First();
                            if (currModel != null)
                            {
                                numberId = currModel.NumberID;
                                currRunTime = currModel.RunDate;
                            }
                        }

                        //如果已控制了开奖结果，则直接开奖，否则系统随机开奖
                        if (currModel != null && !string.IsNullOrEmpty(currModel.LotteryNum))
                        {
                            #region 控制开奖

                            int currNum = Int32.Parse(currModel.LotteryNum);

                            //对应赔率
                            decimal fixRatio = liList.Find(x => x.ItemCode == currNum.ToString()).FixRatio;

                            TimeSpan spaceTs = currRunTime.AddMilliseconds(-8000) - DateTime.Now;
                            if (spaceTs.TotalMilliseconds > 0)
                            {
                                Thread.Sleep(spaceTs);
                            }

                            int betNum = 0;              //已投注数
                            decimal totalPointNum = 0;   //棋子总数
                            int winnerNum = 0;           //中奖人数
                            decimal winPointNum = 0;     //中奖支出棋子总数

                            //提前处理投注等问题
                            List<Model.UserBetLottery> ublList = ublBll.GetList(1, 10000000, out totalCount, "and RunLotteryID = '" + numberId + "' ", null);
                            if (ublList != null && ublList.Count > 0)
                            {
                                betNum = ublList.Count;
                                totalPointNum = ublList.Sum(x => x.TotalPointNum);

                                foreach (Model.UserBetLottery ublModel in ublList)
                                {
                                    //判断是否中奖
                                    if (ublModel.ItemAppend.IndexOf(currNum.ToString()) > -1)
                                    {
                                        decimal dWinPointNum = 0;

                                        string[] currItemArr = ublModel.ItemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        string[] currBetNumArr = ublModel.BetNumAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        int currItemArrLen = currItemArr.Length;
                                        for (int i = 0; i < currItemArrLen; i++)
                                        {
                                            if (currItemArr[i] == currNum.ToString())
                                            {
                                                dWinPointNum = fixRatio * int.Parse(currBetNumArr[i]);
                                            }
                                        }

                                        //中奖支出棋子
                                        winPointNum = winPointNum + dWinPointNum;
                                        ublModel.WinPointNum = dWinPointNum;

                                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                                        {
                                            ublBll.Update(ublModel);

                                            Model.UserPoint uModel = uBll.GetModelByUser(ublModel.UserID);
                                            if (uModel != null)
                                            {
                                                uModel.PointNum = uModel.PointNum + dWinPointNum;
                                                uBll.Update(uModel);
                                            }

                                            scope.Complete();
                                        }
                                    }
                                }

                                //计算中奖人数
                                winnerNum = ublList.Where(x => x.WinPointNum > 0).GroupBy(x => x.UserID).Count();
                            }

                            spaceTs = currRunTime.AddMilliseconds(-1000) - DateTime.Now;
                            if (spaceTs.TotalMilliseconds > 0)
                            {
                                Thread.Sleep(spaceTs);
                            }

                            currModel.Status = 1;
                            currModel.LastUpdatedDate = DateTime.Now;
                            currModel.BetNum = betNum;
                            currModel.TotalPointNum = totalPointNum;
                            currModel.WinnerNum = winnerNum;
                            currModel.WinPointNum = winPointNum;

                            rlBll.Update(currModel);

                            //每天最多开奖结束时间为第二天14:00
                            int h = maxCurrDate.Hour;
                            if (h > 1 && h <= 2)
                            {
                                maxCurrDate = DateTime.Parse(maxCurrDate.ToString("yyyy-MM-dd")).AddHours(9);
                            }
                            else
                            {
                                maxCurrDate = maxCurrDate.AddMinutes(5);
                            }

                            Model.RunLottery rlModel = new Model.RunLottery();
                            rlModel.LotteryNum = string.Empty;
                            rlModel.RunDate = maxCurrDate;
                            rlModel.Status = 0;
                            rlModel.LastUpdatedDate = maxCurrDate;

                            rlBll.Insert(rlModel);

                            Console.WriteLine("第{0}期  开奖结果 {1}  开奖时间 {2} 系统时间 {3}", currModel.Period, currNum, currModel.RunDate, currModel.LastUpdatedDate);

                            #endregion
                        }
                        else
                        {
                            #region 系统随机开奖

                            //保证开奖，避免当前期开的奖与上一期的相同
                            while (true)
                            {
                                int currNum = rnd.Next(1, items.Length);

                                //对应赔率
                                decimal fixRatio = liList.Find(x => x.ItemCode == currNum.ToString()).FixRatio;

                                //尽量避免当前期开的奖与上一期的相同
                                if (currNum != lastNum)
                                {
                                    TimeSpan spaceTs = currRunTime.AddMilliseconds(-8000) - DateTime.Now;
                                    if (spaceTs.TotalMilliseconds > 0)
                                    {
                                        Thread.Sleep(spaceTs);
                                    }

                                    int betNum = 0;              //已投注数
                                    decimal totalPointNum = 0;   //棋子总数
                                    int winnerNum = 0;           //中奖人数
                                    decimal winPointNum = 0;     //中奖支出棋子总数

                                    //提前处理投注等问题
                                    List<Model.UserBetLottery> ublList = ublBll.GetList(1, 10000000, out totalCount, "and RunLotteryID = '" + numberId + "' ", null);
                                    if (ublList != null && ublList.Count > 0)
                                    {
                                        betNum = ublList.Count;
                                        totalPointNum = ublList.Sum(x => x.TotalPointNum);

                                        foreach (LotterySln.Model.UserBetLottery ublModel in ublList)
                                        {
                                            //判断是否中奖
                                            if (ublModel.ItemAppend.IndexOf(currNum.ToString()) > -1)
                                            {
                                                decimal dWinPointNum = 0;

                                                string[] currItemArr = ublModel.ItemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                                string[] currBetNumArr = ublModel.BetNumAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                                int currItemArrLen = currItemArr.Length;
                                                for (int i = 0; i < currItemArrLen; i++)
                                                {
                                                    if (currItemArr[i] == currNum.ToString())
                                                    {
                                                        dWinPointNum = fixRatio * int.Parse(currBetNumArr[i]);
                                                    }
                                                }

                                                //中奖支出棋子
                                                winPointNum = winPointNum + dWinPointNum;
                                                ublModel.WinPointNum = dWinPointNum;

                                                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                                                {
                                                    ublBll.Update(ublModel);

                                                    Model.UserPoint uModel = uBll.GetModelByUser(ublModel.UserID);
                                                    if (uModel != null)
                                                    {
                                                        uModel.PointNum = uModel.PointNum + dWinPointNum;
                                                        uBll.Update(uModel);
                                                    }

                                                    scope.Complete();
                                                }
                                            }
                                        }

                                        //计算中奖人数
                                        winnerNum = ublList.Where(x => x.WinPointNum > 0).GroupBy(x => x.UserID).Count();
                                    }

                                    spaceTs = currRunTime.AddMilliseconds(-1000) - DateTime.Now;
                                    if (spaceTs.TotalMilliseconds > 0)
                                    {
                                        Thread.Sleep(spaceTs);
                                    }

                                    currModel.LotteryNum = currNum.ToString();
                                    currModel.Status = 1;
                                    currModel.LastUpdatedDate = DateTime.Now;
                                    currModel.BetNum = betNum;
                                    currModel.TotalPointNum = totalPointNum;
                                    currModel.WinnerNum = winnerNum;
                                    currModel.WinPointNum = winPointNum;

                                    rlBll.Update(currModel);

                                    //每天最多开奖结束时间为第二天14:00
                                    int h = maxCurrDate.Hour;
                                    if (h > 1 && h <= 2)
                                    {
                                        maxCurrDate = DateTime.Parse(maxCurrDate.ToString("yyyy-MM-dd")).AddHours(9);
                                    }
                                    else
                                    {
                                        maxCurrDate = maxCurrDate.AddMinutes(5);
                                    }

                                    Model.RunLottery rlModel = new Model.RunLottery();
                                    rlModel.LotteryNum = string.Empty;
                                    rlModel.RunDate = maxCurrDate;
                                    rlModel.Status = 0;
                                    rlModel.LastUpdatedDate = maxCurrDate;

                                    rlBll.Insert(rlModel);

                                    Console.WriteLine("第{0}期  开奖结果 {1}  开奖时间 {2} 系统时间 {3}", currModel.Period, currNum, currModel.RunDate, currModel.LastUpdatedDate);

                                    break;
                                }                               
                            }

                            #endregion
                        }

                        //每次开奖完后睡眠4分钟后继续循环,以便控制当前期开奖
                        Thread.Sleep(360000);

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序发生了异常，原因：{0}", ex.Message);
                Console.ReadLine();
            }
        }
    }
}
