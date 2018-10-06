using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotterySln.Web.WebUserControls
{
    public partial class SharesTop : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();

                //滚动公告
                BindNotice();
                //动态
                BindUserTicketByTop();
            }
        }

        private void Bind()
        {
            decimal pointNum = 0;
            BLL.UserPoint uBll = new BLL.UserPoint();
            Model.UserPoint uModel = uBll.GetModelByUser(WebHelper.Common.GetUserId());
            if (uModel != null)
            {
                pointNum = uModel.PointNum;
            }
            Label lbMyPoint = lvUser.FindControl("lbMyPoint") as Label;
            if (lbMyPoint != null)
            {
                lbMyPoint.Text = pointNum.ToString("N");
            } 
        }

        /// <summary>
        /// 绑定最新获奖动态
        /// </summary>
        private void BindUserTicketByTop()
        {
            string mqAppend = string.Empty;

            List<Model.UserTicket> list = WebHelper.UserTicketDataProxy.GetListByTop();
            if (list != null)
            {
                foreach (Model.UserTicket utModel in list)
                {
                    string lastTimeDesr = "";
                    TimeSpan ts = DateTime.Now - utModel.LastUpdatedDate;
                    if (ts.TotalMilliseconds > 0)
                    {
                        if (ts.Days > 0)
                        {
                            lastTimeDesr += "" + ts.Days + "天";
                        }
                        if (ts.Hours > 0)
                        {
                            lastTimeDesr += "" + ts.Hours + "小时";
                        }
                        if (ts.Minutes > 0)
                        {
                            lastTimeDesr += "" + ts.Minutes + "分钟";
                        }
                        if (ts.Seconds > 0)
                        {
                            lastTimeDesr += "" + ts.Seconds + "秒";
                        }

                        lastTimeDesr += "前";
                    }

                    var title = "棋友<a href=\"javascript:void(0)\" target=\"_blank\">" + utModel.UserName + "</a>兑换了<a href=\"/u/y.html?nId=" + utModel.PrizeTicketID + "\" target=\"_blank\">" + utModel.TicketName + "</a>";
                    mqAppend += "<span>【奖品】</span>" + title + " " + lastTimeDesr + "&nbsp;&nbsp;&nbsp;";
                }
            }

            ltrMq.Text = "<div id=\"mqBox\"><div id=\"mqIn\"><div id=\"mqA\">" + mqAppend + "</div><div id=\"mqB\">" + mqAppend + "</div></div></div>";
        }

        /// <summary>
        /// 绑定公告
        /// </summary>
        private void BindNotice()
        {
            string sAppend = "<ul id=\"aScroll\">";

            IList<Model.ContentDetail> list = WebHelper.ContentDetailDataProxy.GetListByNotice();
            if (list != null)
            {
                foreach (Model.ContentDetail udModel in list)
                {
                    sAppend += "<li>" + udModel.Title + "</li>";
                }
            }

            sAppend += "</ul>";

            ltrNotice.Text = sAppend;
        }
    }
}