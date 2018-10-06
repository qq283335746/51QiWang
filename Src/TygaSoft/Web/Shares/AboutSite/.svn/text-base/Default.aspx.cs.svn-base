using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace LotterySln.Web.Shares.AboutSite
{
    public partial class Default : System.Web.UI.Page
    {
        string nId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            nId = HttpUtility.UrlDecode(Request.QueryString["nId"]);

            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        /// <summary>
        /// 初始化并显示到页面
        /// </summary>
        private void Bind()
        {
            if (!string.IsNullOrEmpty(nId))
            {
                Model.ContentDetail model = WebHelper.ContentDetailDataProxy.GetModel(nId);
                if (model != null)
                {
                    BindContent(model.Title, model.ContentText);

                    Dictionary<string, string> titleDic = WebHelper.ContentDetailDataProxy.GetKeyValueByTypeID(model.ContentTypeID);
                    if (titleDic == null || titleDic.Count == 0)
                    {
                        titleDic = WebHelper.ContentDetailDataProxy.GetKeyValueByType("帮助中心");
                        //绑定帮助标题
                        BindTitle(titleDic);
                    }
                    else
                    {
                        //绑定属于当前的帮助标题
                        BindTitle(titleDic);
                    }
                }
                else 
                {
                    Dictionary<string, string> titleDic = WebHelper.ContentDetailDataProxy.GetKeyValueByType("帮助中心");
                    //绑定帮助标题
                    BindTitle(titleDic);
                }
            }
            else
            {
                Dictionary<string, string> titleDic = WebHelper.ContentDetailDataProxy.GetKeyValueByType("帮助中心");
                //绑定帮助标题
                BindTitle(titleDic);

                Model.ContentDetail cdModel = WebHelper.ContentDetailDataProxy.GetModel(titleDic.First().Key);
                BindContent(cdModel.Title, cdModel.ContentText);
            }
        }

        /// <summary>
        /// 帮助标题
        /// </summary>
        private void BindTitle(Dictionary<string, string> dic)
        {
            if (dic == null || dic.Count == 0) return;

            string titleAppend = "";
            foreach (KeyValuePair<string, string> kvp in dic)
            {
                titleAppend += "<a href=\"?nId=" + HttpUtility.UrlEncode(kvp.Key) + "\" class=\"a\"> " + kvp.Value + "</a>";
            }

            //titleAppend += "</ul>";

            helpNav.InnerHtml = titleAppend;
        }

        /// <summary>
        /// 帮助内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        private void BindContent(string title,string content)
        {
            string cAppend = "<div id=\"currTitle\" class=\"tc\">" + title + "</div>";
            cAppend += "<div class=\"mt10\">" + content + "</div>";
            ltrContent.Text = cAppend;
        }
    }
}