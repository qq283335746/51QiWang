using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Services;
using CustomProvider;
using System.Web.Security;

namespace LotterySln.Web.ScriptServices
{
    /// <summary>
    /// AdminService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class AdminService : System.Web.Services.WebService
    {
        HttpContext context;
        string[] roles;
        BLL.ContentType ctBll;

        #region 菜单导航

        [WebMethod]
        public string GetMenus(string path)
        {
            string htmlAppend = string.Empty;

            XElement root = XElement.Load(Server.MapPath("~/Web.sitemap"));
            var q = from r in root.Elements().Elements()
                    select r;

            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    if (UserInnRole(item.Attribute("roles").Value))
                    {
                        string selected = "selected:false";
                        string childAppend = GetChildren(item, ref selected, path);
                        htmlAppend += "<div data-options=\"" + selected + "\" title=\"" + item.Attribute("title").Value + "\" style=\"overflow:auto;padding:10px;\">";
                        htmlAppend += childAppend;
                        htmlAppend += "</div>";
                    }
                }
            }
            return htmlAppend;
        }

        private string GetChildren(XElement xel, ref string selected, string path)
        {
            string htmlAppend = string.Empty;
            var q = from i in xel.Elements()
                    select i;
            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    if (UserInnRole(item.Attribute("roles").Value))
                    {
                        if (path.IndexOf(item.Attribute("title").Value) > -1)
                        {
                            selected = "selected:true";
                            htmlAppend += "<a href='" + item.Attribute("url").Value.Replace("~", "") + "' class='hover'>" + item.Attribute("title").Value + "</a>";
                        }
                        else
                        {
                            htmlAppend += "<a href='" + item.Attribute("url").Value.Replace("~", "") + "'>" + item.Attribute("title").Value + "</a>";
                        }
                    }
                }
            }

            return htmlAppend;
        }

        /// <summary>
        /// 当前用户是否具有当前角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private bool UserInnRole(string role)
        {
            //if (string.IsNullOrEmpty(role)) return true;

            if (roles == null)
            {
                roles = Roles.GetRolesForUser();
            }

            if (roles == null || roles.Length == 0) return false;

            string firstRole = roles[0];
            string[] roleArr = role.Split(',');
            return roleArr.Contains(firstRole);
        }

        [WebMethod]
        public void OnLayout(int state, string name)
        {
            BLL.MenuNav mnBll = new BLL.MenuNav();
            mnBll.InsertForLayout(name, state.ToString(), GetMenuNavFile());
        }

        [WebMethod]
        public int GetLayoutByName(string name)
        {
            BLL.MenuNav mnBll = new BLL.MenuNav();
            return mnBll.GetLayoutStatus(GetMenuNavFile(), name);
        }

        [WebMethod]
        public List<Model.MenuNav> GetTabs()
        {
            BLL.MenuNav mnBll = new BLL.MenuNav();
            CustomProfileCommon profile = new CustomProfileCommon();
            string fileName = string.Format("~/App_Data/AdminData/{0}.xml", profile.GetUserName());
            return mnBll.GetList(Server.MapPath(fileName));
        }

        [WebMethod]
        public string TabsClose(string url)
        {
            BLL.MenuNav mnBll = new BLL.MenuNav();
            mnBll.Delete(url, GetMenuNavFile());

            List<Model.MenuNav> list = GetTabs();
            if (list != null && list.Count > 0)
            {
                Model.MenuNav model = list.Find(m => m.UpdateDate == list.Max(mx => mx.UpdateDate));
                if (model != null)
                {
                    return model.Url;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取当前客户端对应的菜单导航文件
        /// </summary>
        /// <returns></returns>
        private string GetMenuNavFile()
        {
            context = HttpContext.Current;
            CustomProfileCommon profile = new CustomProfileCommon();
            string userName = profile.GetUserName();

            return context.Server.MapPath(string.Format("~/App_Data/AdminData/{0}.xml", userName));
        }

        #endregion 

        [WebMethod]
        public string GetTreeJson()
        {
            if (ctBll == null) ctBll = new BLL.ContentType();
            return ctBll.GetTreeJson();
        }

        [WebMethod]
        public List<Model.ContentType> GetContentType()
        {
            if (ctBll == null) ctBll = new BLL.ContentType();
            int totalCount = 0;
            return ctBll.GetList(1,10000,out totalCount, "",null);
        }

        [WebMethod]
        public string GetContentTypeJson()
        {
            if (ctBll == null) ctBll = new BLL.ContentType();
            return ctBll.GetTreeJson();
        }

        [WebMethod]
        public string InsertContentType(string name,string parentId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "类型名称不能为空";
            }
            if (string.IsNullOrEmpty(parentId))
            {
                return "所属名称对应值不正确，请检查";
            }
            Guid pId = Guid.Empty;
            if (!Guid.TryParse(parentId, out pId))
            {
                return "所属名称对应值格式不正确";
            }

            Model.ContentType model = new Model.ContentType();
            model.TypeName = name;
            model.ParentID = pId;
            model.SameName = "All";
            model.LastUpdatedDate = DateTime.Now;

            if (ctBll == null) ctBll = new BLL.ContentType();
            int effect = ctBll.Insert(model);
            if (effect == 110) return "已存在相同记录";
            if (effect > 0) return "操作成功";
            else return "操作失败";
        }

        [WebMethod]
        public string UpdateContentType(string nId, string name, string parentId)
        {
            if (string.IsNullOrEmpty(nId))
            {
                return "没有找到要编辑的数据，请检查";
            }
            Guid gId = Guid.Empty;
            if (!Guid.TryParse(nId, out gId))
            {
                return "当前编辑行的主键ID不合法，请检查";
            }
            if (string.IsNullOrEmpty(name))
            {
                return "类型名称不能为空";
            }
            if (string.IsNullOrEmpty(parentId))
            {
                return "所属名称对应值不正确，请检查";
            }
            Guid pId = Guid.Empty;
            if (!Guid.TryParse(parentId, out pId))
            {
                return "所属名称对应值格式不正确";
            }

            Model.ContentType model = new Model.ContentType();
            model.NumberID = nId;
            model.TypeName = name;
            model.ParentID = pId;
            model.SameName = "All";
            model.LastUpdatedDate = DateTime.Now;

            if (ctBll == null) ctBll = new BLL.ContentType();
            int effect = ctBll.Update(model);
            if (effect == 110) return "已存在相同记录";
            if (effect > 0) return "操作成功";
            else return "操作失败";
        }

        [WebMethod]
        public string DelContentType(string nId)
        {
            if (string.IsNullOrEmpty(nId))
            {
                return "没有找到要删除的数据，请检查";
            }
            Guid gId = Guid.Empty;
            if (!Guid.TryParse(nId, out gId))
            {
                return "当前行的主键ID不合法，请检查";
            }

            if (ctBll == null) ctBll = new BLL.ContentType();
            int effect = ctBll.Delete(nId);
            if (effect > 0) return "操作成功";
            else return "操作失败";
        }

        [WebMethod]
        public string InsertCategory(string name, string parentId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "类型名称不能为空";
            }
            if (string.IsNullOrEmpty(parentId))
            {
                return "所属名称对应值不正确，请检查";
            }
            Guid pId = Guid.Empty;
            if (!Guid.TryParse(parentId, out pId))
            {
                return "所属名称对应值格式不正确";
            }

            Model.Category model = new Model.Category();
            model.CategoryName = name;
            model.ParentID = pId;
            model.Title = "All";
            model.Sort = 0;
            model.Remark = string.Empty;
            model.LastUpdatedDate = DateTime.Now;

            BLL.Category cBll = new BLL.Category();
            int effect = cBll.Insert(model);
            if (effect == 110) return "已存在相同记录";
            if (effect > 0) return "操作成功";
            else return "操作失败";
        }

        [WebMethod]
        public string UpdateCategory(string nId, string name, string parentId)
        {
            if (string.IsNullOrEmpty(nId))
            {
                return "没有找到要编辑的数据，请检查";
            }
            Guid gId = Guid.Empty;
            if (!Guid.TryParse(nId, out gId))
            {
                return "当前编辑行的主键ID不合法，请检查";
            }
            if (string.IsNullOrEmpty(name))
            {
                return "类型名称不能为空";
            }
            if (string.IsNullOrEmpty(parentId))
            {
                return "所属名称对应值不正确，请检查";
            }
            Guid pId = Guid.Empty;
            if (!Guid.TryParse(parentId, out pId))
            {
                return "所属名称对应值格式不正确";
            }

            Model.Category model = new Model.Category();
            model.NumberID = nId;
            model.CategoryName = name;
            model.ParentID = pId;
            model.Title = "All";
            model.Sort = 0;
            model.Remark = string.Empty;
            model.LastUpdatedDate = DateTime.Now;

            BLL.Category cBll = new BLL.Category();
            int effect = cBll.Update(model);
            if (effect == 110) return "已存在相同记录";
            if (effect > 0) return "操作成功";
            else return "操作失败";
        }

        [WebMethod]
        public string DelCategory(string nId)
        {
            if (string.IsNullOrEmpty(nId))
            {
                return "没有找到要删除的数据，请检查";
            }
            Guid gId = Guid.Empty;
            if (!Guid.TryParse(nId, out gId))
            {
                return "当前行的主键ID不合法，请检查";
            }

            BLL.Category cBll = new BLL.Category();
            int effect = cBll.Delete(nId);
            if (effect > 0) return "操作成功";
            else return "操作失败";
        }

        [WebMethod]
        public string GetCategoryJson()
        {
            BLL.Category cBll = new BLL.Category();
            return cBll.GetTreeJson();
        }
    }
}
