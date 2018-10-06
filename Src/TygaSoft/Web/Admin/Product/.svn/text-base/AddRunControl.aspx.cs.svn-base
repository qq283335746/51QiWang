using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace LotterySln.Web.Admin.Product
{
    public partial class AddRunControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //数据绑定
                Bind();
            }
        }

        private void Bind()
        {
            int totalCount = 0;

            string liAppend = "开奖：<select id=\"cbbLItem\" class=\"easyui-combobox\" style=\"width:200px;\">";
            BLL.LotteryItem liBll = new BLL.LotteryItem();
            List<Model.LotteryItem> liList = liBll.GetList(1, 1000, out totalCount, "", null);
            if (liList != null)
            {
                
                foreach (Model.LotteryItem model in liList)
                {
                    liAppend += "<option value=\""+model.ItemCode+"\">"+model.ItemName+"</option>";
                }
            }

            liAppend += "</select>";

            dlgContent.InnerHtml = liAppend;

            BLL.RunLottery rlBll = new BLL.RunLottery();

            rpData.DataSource = rlBll.GetDataSet(1, 5, out totalCount, "", null); ;
            rpData.DataBind();
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
                case "lbtnSave":
                    OnSave();
                    break;
                default:
                    break;
            }
        }

        private void OnSave()
        {
            BLL.RunLottery rlBll = new BLL.RunLottery();
            bool isRight = false;

            //获取所有行
            RepeaterItemCollection ric = rpData.Items;

            foreach (RepeaterItem item in ric)
            {
                //找到CheckBox
                HtmlInputText txt = item.FindControl("txtLotteryNum") as HtmlInputText;
                
                if (txt != null)
                {
                    if (!string.IsNullOrEmpty(txt.Value.Trim()))
                    {
                        HtmlInputHidden hNid = item.FindControl("hNid") as HtmlInputHidden;
                        if (hNid != null)
                        {
                            if (!string.IsNullOrEmpty(hNid.Value.Trim()))
                            {
                                Model.RunLottery rlModel = rlBll.GetModel(hNid.Value.Trim());
                                if (rlModel != null)
                                {
                                    rlModel.LotteryNum = txt.Value.Trim();

                                    if (rlBll.Update(rlModel) > 0)
                                    {
                                        if (!isRight) isRight = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (isRight)
            {
                WebHelper.MessageBox.MessagerShow(this.Page, lbtnPostBack, "操作成功");
            }
            else
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "操作失败","系统提示");
            }
        }

    }
}