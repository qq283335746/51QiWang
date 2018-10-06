using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotterySln.Web.Admin.PrizeTicket
{
    public partial class AddPrizeTicket : System.Web.UI.Page
    {
        string nId;
        BLL.PrizeTicket bll;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["nId"]))
            {
                nId = Request.QueryString["nId"];
            }

            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            if (!string.IsNullOrEmpty(nId))
            {
                if (bll == null) bll = new BLL.PrizeTicket();
                Model.PrizeTicket model = bll.GetModel(nId);
                if (model != null)
                {
                    txtCategory.Value = model.CategoryID.ToString();
                    txtName.Value = model.TicketName;
                    txtPieceNum.Value = model.PointNum.ToString();
                    txtImageUrl.Value = model.ImageUrl;
                    txtPNum.Value = model.PNum;
                    txtStock.Value = model.StockNum.ToString();
                    txtDescr.Value = model.TicketDescr;
                    txtFlow.Value = model.FlowDescr;
                }
            }
        }

        /// <summary>
        /// 按钮OnCommand事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Command(object sender, CommandEventArgs e)
        {
            hBackToN.Value = (Int32.Parse(hBackToN.Value) + 1).ToString();
            string commName = e.CommandName;
            switch (commName)
            {
                case "lbtnsave":
                    OnSave();
                    break;
                default:
                    break;
            }
        }

        private void OnSave()
        {
            Guid categoryId = Guid.Empty;
            string sCategory = txtCategory.Value.Trim();
            if (!string.IsNullOrEmpty(sCategory))
            {
                if (!Guid.TryParse(sCategory, out categoryId))
                {
                    WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "所属分类ID格式不正确，请检查", "错误提醒", "error");
                    return;
                }
            }
            string sName = txtName.Value.Trim();
            if (string.IsNullOrEmpty(sName))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "名称不能为空，请检查", "错误提醒", "error");
                return;
            }
            string sPieceNum = txtPieceNum.Value.Trim();
            if (string.IsNullOrEmpty(sPieceNum))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "棋子数不能为空，请检查", "错误提醒", "error");
                return;
            }
            decimal pointNum = 0;
            if (!decimal.TryParse(sPieceNum, out pointNum))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "棋子数输入格式不正确，请检查", "错误提醒", "error");
                return;
            }

            string sImageUrl = txtImageUrl.Value.Trim();
            if (string.IsNullOrEmpty(sImageUrl))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "图片不能为空，请检查", "错误提醒", "error");
                return;
            }
            string sPNum = txtPNum.Value.Trim();
            string sStockNum = txtStock.Value.Trim();
            int stockNum = 0;
            if(!string.IsNullOrEmpty(sStockNum))
            {
                Int32.TryParse(sStockNum,out stockNum);
            }

            if (bll == null) bll = new BLL.PrizeTicket();
            Model.PrizeTicket model = new Model.PrizeTicket();
            model.CategoryID = categoryId;
            model.TicketName = sName;
            model.PointNum = pointNum;
            model.ImageUrl = sImageUrl;
            model.LastUpdatedDate = DateTime.Now;
            model.PNum = sPNum;
            model.StockNum = stockNum;
            model.TicketDescr = txtDescr.Value.Trim();
            model.FlowDescr = txtFlow.Value.Trim();

            int effectCount = -1;
            if (!string.IsNullOrEmpty(nId))
            {
                model.NumberID = nId;
                effectCount = bll.Update(model);
            }
            else
            {
                effectCount = bll.Insert(model);
            }

            if (effectCount == 110)
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "已存在相同记录", "温馨提醒", "error");
                return;
            }
            if (effectCount > 0)
            {
                WebHelper.MessageBox.MessagerShow(this.Page, lbtnPostBack, "操作成功");
            }
            else
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "操作失败");
            }
        }
    }
}