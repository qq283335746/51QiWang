using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotterySln.Web.Admin.Product
{
    public partial class AddLotteryItem : System.Web.UI.Page
    {
        string nId;
        BLL.LotteryItem bll;

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
                if (bll == null) bll = new BLL.LotteryItem();
                Model.LotteryItem model = bll.GetModel(nId);
                if (model != null)
                {
                    txtName.Value = model.ItemName;
                    txtCode.Value = model.ItemCode;
                    txtImageUrl.Value = model.ImageUrl;
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
            string sName = txtName.Value.Trim();
            if (string.IsNullOrEmpty(sName))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "名称不能为空，请检查", "错误提醒", "error");
                return;
            }
            string sCode = txtCode.Value.Trim();
            if (string.IsNullOrEmpty(sCode))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "代号不能为空，请检查", "错误提醒", "error");
                return;
            }
            string sRatio = txtRatio.Value.Trim();
            if (string.IsNullOrEmpty(sRatio))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "赔率不能为空，请检查", "错误提醒", "error");
                return;
            }
            decimal ratio = 0;
            if (!decimal.TryParse(sRatio, out ratio))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "赔率正确格式为整数，请检查", "错误提醒", "error");
                return;
            }
            string sImageUrl = txtImageUrl.Value.Trim();
            if (string.IsNullOrEmpty(sImageUrl))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "图片不能为空，请检查", "错误提醒", "error");
                return;
            }

            if (bll == null) bll = new BLL.LotteryItem();
            Model.LotteryItem model = new Model.LotteryItem();
            model.ItemName = sName;
            model.ItemCode = sCode;
            model.ImageUrl = sImageUrl;
            model.FixRatio = ratio;
            model.LastUpdatedDate = DateTime.Now;

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