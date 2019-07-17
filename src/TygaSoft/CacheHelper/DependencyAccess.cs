using System.Reflection;
using System.Configuration;

namespace CacheHelper
{
    public static class DependencyAccess
    {
        private static ICacheDependency LoadInstance(string className)
        {
            string[] paths = ConfigurationManager.AppSettings["CacheDependencyAssembly"].Split(',');
            string fullyQualifiedClass = paths[0] + "." + className;

            return (ICacheDependency)Assembly.Load(paths[1]).CreateInstance(fullyQualifiedClass);
        }

        public static ICacheDependency CreateUserTicketDependency()
        {
            return LoadInstance("UserTicket");
        }

        public static ICacheDependency CreateContentDetailDependency()
        {
            return LoadInstance("ContentDetail");
        }

        public static ICacheDependency CreateContentTypeDependency()
        {
            return LoadInstance("ContentType");
        }

        public static ICacheDependency CreateCategoryDependency()
        {
            return LoadInstance("Category");
        }

        public static ICacheDependency CreatePrizeTicketDependency()
        {
            return LoadInstance("PrizeTicket");
        }

        public static ICacheDependency CreateRunLotteryDependency()
        {
            return LoadInstance("RunLottery");
        }

        public static ICacheDependency CreateLotteryItemDependency()
        {
            return LoadInstance("LotteryItem");
        }

        public static ICacheDependency CreateUserBetLotteryDependency()
        {
            return LoadInstance("UserBetLottery");
        }
    }
}
