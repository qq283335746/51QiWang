using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Data;
using System.Data.SqlClient;
using CacheHelper;

namespace LotterySln.WebHelper
{
    public class RunLotteryDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int timeOut = int.Parse(ConfigurationManager.AppSettings["RunLotteryCacheDuration"]);

        /// <summary>
        /// 获取当前ID对应数据
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        public static Model.RunLottery GetModel(object nId)
        {
            BLL.RunLottery bll = new BLL.RunLottery();

            if (!enableCaching)
                return bll.GetModel(nId.ToString());

            string key = "runLottery_" + nId.ToString() + "";
            Model.RunLottery data = (Model.RunLottery)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetModel(nId.ToString());

                AggregateCacheDependency cd = DependencyFactory.GetRunLotteryDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(timeOut), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        /// <summary>
        /// 获取当前开奖ID对应投注数列表
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        public static List<Model.RunLottery> GetList(int pageIndex, int pageSize, out int totalCount)
        {
            BLL.RunLottery bll = new BLL.RunLottery();
            totalCount = 0;

            if (!enableCaching)
                return bll.GetList(pageIndex, pageSize, out totalCount, "", null);

            string key = "runLottery_" + pageIndex + "_"+pageSize+"";
            string keyCount = "runLotteryCount_" + pageIndex + "_" + pageSize + "";
            List<Model.RunLottery> data = (List<Model.RunLottery>)HttpRuntime.Cache[key];
            if (HttpRuntime.Cache[keyCount] != null)
            {
                totalCount = (Int32)HttpRuntime.Cache[keyCount];
            }

            if (data == null)
            {
                data = bll.GetList(pageIndex, pageSize, out totalCount, "", null);

                if (pageIndex > 1)
                {
                    DateTime currTime = DateTime.Now;

                    AggregateCacheDependency cd = DependencyFactory.GetRunLotteryDependency();
                    HttpRuntime.Cache.Add(key, data, cd, currTime.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.High, null);

                    HttpRuntime.Cache.Add(keyCount, totalCount, null, currTime.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                }
            }

            return data;
        }

    }
}
