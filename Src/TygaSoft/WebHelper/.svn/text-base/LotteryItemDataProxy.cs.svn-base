using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using CacheHelper;

namespace LotterySln.WebHelper
{
    public class LotteryItemDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int timeOut = int.Parse(ConfigurationManager.AppSettings["LotteryItemCacheDuration"]);

        /// <summary>
        /// 获取当前区间数据行的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Model.LotteryItem> GetList()
        {
            BLL.LotteryItem bll = new BLL.LotteryItem();

            if (!enableCaching)
                return bll.GetList(1, 100, "", null);

            string key = "lotteryItem_All";
            List<Model.LotteryItem> data = (List<Model.LotteryItem>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList(1, 100, "", null);

                AggregateCacheDependency cd = DependencyFactory.GetLotteryItemDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(timeOut), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }
    }
}
