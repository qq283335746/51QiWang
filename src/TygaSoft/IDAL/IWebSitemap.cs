using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotterySln.IDAL
{
    public interface IWebSitemap
    {
        /// <summary>
        /// 获取站点地图数据集
        /// </summary>
        /// <returns></returns>
        List<Model.WebSitemap> GetList();
    }
}
