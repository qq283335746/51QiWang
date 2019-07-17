using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CustomProvider;

namespace LotterySln.Web.Shares
{
    public partial class Shares : System.Web.UI.MasterPage
    {
        BLL.MenuNav mnBll;
        string rawUrl;
        string userFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rawUrl = Intelligencia.UrlRewriter.RewriterHttpModule.RawUrl;

                CustomProfileCommon profile = new CustomProfileCommon();
                string userName = profile.GetUserName();

                string fileName = string.Format("~/App_Data/SharesData/{0}.xml", userName);
                string path = Server.MapPath(VirtualPathUtility.GetDirectory(fileName));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                userFile = Server.MapPath(fileName);
                if (!File.Exists(userFile))
                {
                    if (mnBll == null) mnBll = new BLL.MenuNav();
                    mnBll.Create(userFile, "~/Shares/Default.aspx");
                }
                if (mnBll == null) mnBll = new BLL.MenuNav();
                mnBll.RequestSave(Request.AppRelativeCurrentExecutionFilePath, rawUrl, userFile);
            }
        }
    }
}