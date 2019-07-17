using System.Web.Caching;

namespace CacheHelper
{
    public interface ICacheDependency
    {
        AggregateCacheDependency GetDependency();
    }
}
