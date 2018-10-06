using System.Configuration;
using System.Web.Caching;
using System.Collections.Generic;

namespace CacheHelper
{
    public static class DependencyFactory
    {
        private static readonly string path = ConfigurationManager.AppSettings["CacheDependencyAssembly"];

        public static AggregateCacheDependency GetUserTicketDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreateUserTicketDependency().GetDependency();
            else
                return null;
        }

        public static AggregateCacheDependency GetContentDetailDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreateContentDetailDependency().GetDependency();
            else
                return null;
        }

        public static AggregateCacheDependency GetContentTypeDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreateContentTypeDependency().GetDependency();
            else
                return null;
        }

        public static AggregateCacheDependency GetPrizeTicketDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreatePrizeTicketDependency().GetDependency();
            else
                return null;
        }

        public static AggregateCacheDependency GetRunLotteryDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreateRunLotteryDependency().GetDependency();
            else
                return null;
        }

        public static AggregateCacheDependency GetLotteryItemDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreateLotteryItemDependency().GetDependency();
            else
                return null;
        }

        public static AggregateCacheDependency GetUserBetLotteryDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreateUserBetLotteryDependency().GetDependency();
            else
                return null;
        }
    }
}
