using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace LotterySln.DALFactory
{
    public class XmlDataAccess
    {
        private static readonly string[] path = ConfigurationManager.AppSettings["WebDALXmlProvider"].Split(',');

        public static IDAL.IHandlerUserProfile CreateHandlerUserProfile()
        {
            string className = path[0] + ".HandlerUserProfile";
            return (IDAL.IHandlerUserProfile)Assembly.Load(path[1]).CreateInstance(className);
        }

        public static IDAL.IWebSitemap CreateWebSitemap()
        {
            string className = path[0] + ".WebSitemap";
            return (IDAL.IWebSitemap)Assembly.Load(path[1]).CreateInstance(className);
        }

        public static IDAL.IMenuNav CreateMenuNav()
        {
            string className = path[0] + ".MenuNav";
            return (IDAL.IMenuNav)Assembly.Load(path[1]).CreateInstance(className);
        }
    }
}
