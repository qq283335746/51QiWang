using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotterySln.Web.Users
{
    public partial class ListPrizeTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();

                //绑定奖品列表
                BindTicket();

                //绑定最新兑奖
                BindLatestTicket();
            }
        }

        private void Bind()
        {
            Dictionary<string,string> titleDic = WebHelper.ContentDetailDataProxy.GetKeyValueByType("兑奖帮助");
            if (titleDic != null && titleDic.Count > 0)
            {
                string titleAppend = string.Empty;
                foreach (KeyValuePair<string, string> kvp in titleDic)
                {
                    titleAppend += "<a href=\"/s/t.html?nId="+kvp.Key+"\" class=\"a\" target=\"_blank\">"+kvp.Value+"</a>";
                }

                titleAppend += "<span class=\"clr\"></span>";

                ltrTicketHelp.Text = titleAppend;
            }
        }

        /// <summary>
        /// 绑定奖品列表
        /// </summary>
        private void BindTicket()
        {
            rpData.DataSource = WebHelper.PrizeTicketDataProxy.GetList(1, 50);
            rpData.DataBind();
        }

        /// <summary>
        /// 绑定最新兑奖
        /// </summary>
        private void BindLatestTicket()
        {
            List<Model.UserTicket> list = WebHelper.UserTicketDataProxy.GetListByTop();
            if (list != null && list.Count > 0)
            {
                string sAppend = "<ul class=\"v_ul\">";
                foreach (Model.UserTicket model in list)
                {
                    sAppend += "<li>用户 <a href=\"javascript:void(0)\">" + model.UserName + "</a>兑换了奖品<a href=\"/u/y.html?nId=" + model.PrizeTicketID + "\" target=\"_blank\">" + model.TicketName + "</a> </li>";
                }

                sAppend += "</ul>";

                ltrLatestTicket.Text = sAppend;
            }
        }
    }
}