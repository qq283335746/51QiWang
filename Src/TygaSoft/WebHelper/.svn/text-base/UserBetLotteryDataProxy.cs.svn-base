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
using DBUtility;

namespace LotterySln.WebHelper
{
    public class UserBetLotteryDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int timeOut = int.Parse(ConfigurationManager.AppSettings["UserBetLotteryCacheDuration"]);

        /// <summary>
        /// 获取当前开奖ID对应投注数列表
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        public static List<Model.UserBetLottery> GetListByRunLotteryID(object nId)
        {
            Guid gId = Guid.Empty;
            Guid.TryParse(nId.ToString(),out gId);
            object userId = WebHelper.Common.GetUserId();

            SqlParameter parm = new SqlParameter("@RunLotteryID",SqlDbType.UniqueIdentifier);
            parm.Value = gId;

            ParamsHelper parms = new ParamsHelper();
            parms.Add(parm);

            parm = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(userId.ToString());
            parms.Add(parm);

            BLL.UserBetLottery bll = new BLL.UserBetLottery();

            if (!enableCaching)
                return bll.GetList(1, 1000000, "and RunLotteryID = @RunLotteryID and UserID = @UserId ", parms.ToArray());

            string key = "userBetLottery_" + gId.ToString() + "_"+userId.ToString()+"";
            List<Model.UserBetLottery> data = (List<Model.UserBetLottery>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList(1, 1000000, "and RunLotteryID = @RunLotteryID and UserID = @UserId ", parms.ToArray());

                AggregateCacheDependency cd = DependencyFactory.GetUserBetLotteryDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(timeOut), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }
    }
}
