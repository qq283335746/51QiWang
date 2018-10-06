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
    public class UserTicketDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int userTicketTimeout = int.Parse(ConfigurationManager.AppSettings["UserTicketCacheDuration"]);

        /// <summary>
        /// 获取前第15行数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.UserTicket> GetListByTop()
        {
            BLL.UserTicket bll = new BLL.UserTicket();

            if (!enableCaching)
                return bll.GetList(1,15,"",null);

            string key = "userTicket_top";
            List<Model.UserTicket> data = (List<Model.UserTicket>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList(1, 15, "", null);

                AggregateCacheDependency cd = DependencyFactory.GetUserTicketDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(userTicketTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }
    }
}
