using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

namespace LotterySln.Web.Users
{
    public partial class TicketDetail : System.Web.UI.Page
    {
        string nId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["nId"]))
            {
                nId = HttpUtility.UrlDecode(Request.QueryString["nId"]);
            }

            if (!Page.IsPostBack)
            {
                Bind();

                ////绑定奖品列表
                BindTicket();

                //绑定最新兑奖
                BindLatestTicket();
            }
        }

        private void Bind()
        {
            BLL.ContentDetail cdBll = new BLL.ContentDetail();
            Dictionary<string, string> titleDic = WebHelper.ContentDetailDataProxy.GetKeyValueByType("兑奖帮助");
            if (titleDic != null && titleDic.Count > 0)
            {
                string titleAppend = string.Empty;
                foreach (KeyValuePair<string, string> kvp in titleDic)
                {
                    titleAppend += "<a href=\"/s/t.html?nId=" + kvp.Key + "\" class=\"a\" target=\"_blank\">" + kvp.Value + "</a>";
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
            if (!string.IsNullOrEmpty(nId))
            {
                BLL.PrizeTicket ptBll = new BLL.PrizeTicket();
                Model.PrizeTicket model = ptBll.GetModel(nId);
                List<Model.PrizeTicket> list = new List<Model.PrizeTicket>();
                list.Add(model);

                rpData.DataSource = list;
                rpData.DataBind();
            }
        }

        /// <summary>
        /// 按钮OnCommand事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "lbtnDh":
                    OnSave(e.CommandArgument);
                    break;
                default:
                    break;
            }
        }

        private void OnSave(object nId)
        {
            BLL.PrizeTicket ptBll = new BLL.PrizeTicket();
            Model.PrizeTicket ptModel = ptBll.GetModel(nId.ToString());
            if (ptModel == null)
            {
                WebHelper.MessageBox.Messager(this.Page, rpData.Controls[0], "没有找到相关奖品");
                return;
            }

            decimal pointNum = ptModel.PointNum;
            BLL.UserPoint uBll = new BLL.UserPoint();
            object userId = WebHelper.Common.GetUserId();
            DateTime currTime = DateTime.Now;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                Model.UserPoint uModel = uBll.GetModelByUser(userId);
                if (uModel == null)
                {
                    WebHelper.MessageBox.Messager(this.Page, rpData.Controls[0], "您的棋子数额不足，无法兑换");
                    return;
                }
                decimal userPointNum = uModel.PointNum;
                decimal currPointNum = userPointNum - pointNum;
                if (currPointNum < 0)
                {
                    WebHelper.MessageBox.Messager(this.Page, rpData.Controls[0], "您的棋子数额不足，无法兑换");
                    return;
                }

                Model.UserTicket utModel = new Model.UserTicket();
                utModel.PrizeTicketID = ptModel.NumberID;
                utModel.UserID = userId;
                utModel.Status = 0;
                utModel.LastUpdatedDate = currTime;

                BLL.UserTicket utBll = new BLL.UserTicket();
                if (utBll.Insert(utModel) > 0)
                {
                    uModel.PointNum = currPointNum;
                    uModel.LastUpdatedDate = currTime;

                    if (uBll.Update(uModel) > 0)
                    {
                       //操作成功，提交事务
                        scope.Complete();

                        Response.Redirect("/u/ta.html");
                    }
                    else
                    {
                        WebHelper.MessageBox.Messager(this.Page, rpData.Controls[0], "奖品兑换失败，无法兑换", "系统异常");
                        return;
                    }
                }
                else
                {
                    WebHelper.MessageBox.Messager(this.Page, rpData.Controls[0], "奖品兑换失败，无法兑换","系统异常");
                    return;
                }
            }
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
                    sAppend += "<li>用户 <a href=\"javascript:void(0)\">" + model.UserName + "</a>兑换了奖品<a href=\"/u/y.html?nId="+model.PrizeTicketID+"\" target=\"_blank\">"+model.TicketName+"</a> </li>";
                }

                sAppend += "</ul>";

                ltrLatestTicket.Text = sAppend;
            }
        }
    }
}