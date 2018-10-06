using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotterySln.Web.Admin.UserGame
{
    public partial class AddUserPoint : System.Web.UI.Page
    {
        string nId;
        BLL.UserPoint bll;

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
                if (bll == null) bll = new BLL.UserPoint();
                Model.UserPoint model = bll.GetModel(nId);
                if (model != null)
                {
                    cbbUser.Value = model.UserID.ToString();
                    txtPoint.Value = model.PointNum.ToString();
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
            string sUser = cbbUser.Value.Trim();
            if (string.IsNullOrEmpty(sUser))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "用户为必选项，请检查", "错误提醒", "error");
                return;
            }
            string sPoint = txtPoint.Value.Trim();
            if (string.IsNullOrEmpty(sPoint))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "棋子数不能为空，请检查", "错误提醒", "error");
                return;
            }

            decimal pointNum = 0;
            if (!decimal.TryParse(sPoint, out pointNum))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "棋子数输入格式不正确，请检查", "错误提醒", "error");
                return;
            }

            if (bll == null) bll = new BLL.UserPoint();
            Model.UserPoint model = new Model.UserPoint();
            model.UserID = sUser;
            model.PointNum = pointNum;
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