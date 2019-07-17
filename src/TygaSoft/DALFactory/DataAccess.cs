using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace LotterySln.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string[] paths = ConfigurationManager.AppSettings["WebDALMsSqlProvider"].Split(',');

        public static IDAL.ICategory CreateCategory()
        {
            string className = paths[0] + ".Category";
            return (IDAL.ICategory)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.IRunLottery CreateRunLottery()
        {
            string className = paths[0] + ".RunLottery";
            return (IDAL.IRunLottery)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.IContentType CreateContentType()
        {
            string className = paths[0] + ".ContentType";
            return (IDAL.IContentType)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.IContentDetail CreateContentDetail()
        {
            string className = paths[0] + ".ContentDetail";
            return (IDAL.IContentDetail)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.ILotteryItem CreateLotteryItem()
        {
            string className = paths[0] + ".LotteryItem";
            return (IDAL.ILotteryItem)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.IPrizeTicket CreatePrizeTicket()
        {
            string className = paths[0] + ".PrizeTicket";
            return (IDAL.IPrizeTicket)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.IUserPoint CreateUserPoint()
        {
            string className = paths[0] + ".UserPoint";
            return (IDAL.IUserPoint)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.IUserTicket CreateUserTicket()
        {
            string className = paths[0] + ".UserTicket";
            return (IDAL.IUserTicket)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IDAL.IUserBetLottery CreateUserBetLottery()
        {
            string className = paths[0] + ".UserBetLottery";
            return (IDAL.IUserBetLottery)Assembly.Load(paths[1]).CreateInstance(className);
        }
    }
}
