using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotterySln.IDAL;

namespace LotterySln.BLL
{
    public class WebSitemap
    {
        private static readonly IWebSitemap dal = DALFactory.XmlDataAccess.CreateWebSitemap();

        /// <summary>
        /// 获取站点地图数据集
        /// </summary>
        /// <returns></returns>
        public List<Model.WebSitemap> GetList()
        {
            return dal.GetList();
        }
    }
}
