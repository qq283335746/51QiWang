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
    public class PrizeTicketDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int userTicketTimeout = int.Parse(ConfigurationManager.AppSettings["UserTicketCacheDuration"]);

        /// <summary>
        /// 获取当前区间数据行的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Model.PrizeTicket> GetList(int pageIndex,int pageSize)
        {
            BLL.PrizeTicket bll = new BLL.PrizeTicket();

            if (!enableCaching)
                return bll.GetList(pageIndex, pageSize, "", null);

            string key = "prizeTicket_"+pageIndex+"_"+pageSize+"";
            List<Model.PrizeTicket> data = (List<Model.PrizeTicket>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList(pageIndex, pageSize, "", null);

                AggregateCacheDependency cd = DependencyFactory.GetUserTicketDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(userTicketTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }
    }
}
