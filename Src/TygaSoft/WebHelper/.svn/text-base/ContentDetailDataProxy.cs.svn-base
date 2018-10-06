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
    public class ContentDetailDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int contentDetailTimeout = int.Parse(ConfigurationManager.AppSettings["ContentCacheDuration"]);

        /// <summary>
        /// 获取前第15行数据
        /// </summary>
        /// <returns></returns>
        public static IList<Model.ContentDetail> GetListByNotice()
        {
            BLL.ContentDetail bll = new BLL.ContentDetail();

            if (!enableCaching)
                return bll.GetList(1, 15, "and ct.TypeName = '开奖公告'", null);

            string key = "contentDetail_Notice";
            IList<Model.ContentDetail> data = (IList<Model.ContentDetail>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList(1, 15, "and ct.TypeName = '开奖公告'", null);

                AggregateCacheDependency cd = DependencyFactory.GetContentDetailDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(contentDetailTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        /// <summary>
        /// 获取属于当前类型名称的内容标题和ID
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeyValueByType(string typeName)
        {
            BLL.ContentDetail bll = new BLL.ContentDetail();

            if (!enableCaching)
                return bll.GetKeyValueByType(typeName);

            string key = "contentDetail_"+typeName+"";
            Dictionary<string, string> data = (Dictionary<string, string>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetKeyValueByType(typeName);

                AggregateCacheDependency cd = DependencyFactory.GetContentDetailDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(contentDetailTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        /// <summary>
        /// 获取属于当前类型ID的内容标题和ID
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeyValueByTypeID(object nId)
        {
            BLL.ContentDetail bll = new BLL.ContentDetail();

            if (!enableCaching)
                return bll.GetKeyValueByTypeID(nId);

            string key = "contentDetail_" + nId.ToString() + "";
            Dictionary<string, string> data = (Dictionary<string, string>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetKeyValueByTypeID(nId);

                AggregateCacheDependency cd = DependencyFactory.GetContentDetailDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(contentDetailTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        /// <summary>
        /// 获取当前ID对应数据
        /// </summary>
        /// <param name="nId"></param>
        /// <returns></returns>
        public static Model.ContentDetail GetModel(object nId)
        {
            BLL.ContentDetail bll = new BLL.ContentDetail();

            if (!enableCaching)
                return bll.GetModel(nId);

            string key = "contentDetail_" + nId.ToString() + "";
            Model.ContentDetail data = (Model.ContentDetail)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetModel(nId);

                AggregateCacheDependency cd = DependencyFactory.GetContentDetailDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(contentDetailTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }
    }
}
