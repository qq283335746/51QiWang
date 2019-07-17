using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

namespace LotterySln.Web.Users
{
    public partial class AddLorrery : System.Web.UI.Page
    {
        string nId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["nId"]))
            {
                nId = HttpUtility.UrlDecode(Request.QueryString["nId"]);
            }
            else
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnSave, "没有选中任何开奖期数进行投注", "错误提示");
                return;
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
                BLL.RunLottery rlBll = new BLL.RunLottery();
                Model.RunLottery model = rlBll.GetModel(nId);
                if (model != null)
                {
                    lbBetPeriod.InnerText = model.Period.ToString();
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
            switch (e.CommandName)
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
            string sItemAppend = hItemAppend.Value.Trim();

            if (string.IsNullOrEmpty(sItemAppend))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnSave, "投注项不能为空","错误提示");
                return;
            }

            string[] items = sItemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length < 1)
            {
                return;
            }

            BLL.RunLottery rlBll = new BLL.RunLottery();

            Model.RunLottery rlModel = rlBll.GetModel(nId);
            if (rlModel == null)
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnSave, "不存在的开奖期，非法操作", "错误提示");
                return;
            }

            DateTime runDate = rlModel.RunDate;
            TimeSpan ts = runDate - DateTime.Now;
            if ((ts.TotalMilliseconds - 8000) < 0)
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnSave, "当前第" + rlModel.Period + "期，已停止投注", "温馨提示");
                return;
            }

            string[] itemArr = items[0].Split(new char[]{'|'},StringSplitOptions.RemoveEmptyEntries);

            int totalBetNum = Int32.Parse(itemArr[1]);
            if (totalBetNum <= 0)
            {
                return;
            }

            object userId = WebHelper.Common.GetUserId();
            //获取当前用户的棋子数
            BLL.UserPoint uBll = new BLL.UserPoint();
            Model.UserPoint uModel = uBll.GetModelByUser(userId);
            if (uModel == null)
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnSave, "您的棋子数不足，不能进行投注！", "系统提示");
                return;
            }

            if ((uModel.PointNum - totalBetNum) < 0)
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnSave, "您的棋子数不足，不能进行投注！", "系统提示");
                return;
            }

            string itemAppend = string.Empty;
            string betNumAppend = string.Empty;
            int index = 0;
            foreach (string s in items)
            {
                index++;
                if (index > 1)
                {
                    string[] nvArr = s.Split('|');
                    itemAppend += nvArr[0] + ",";
                    betNumAppend += nvArr[1] + ",";
                }
            }

            BLL.Order ublBll = new BLL.Order();
            Model.UserBetLottery ublModel = new Model.UserBetLottery();
            ublModel.LastUpdatedDate = DateTime.Now;
            ublModel.UserID = userId;
            ublModel.RunLotteryID = rlModel.NumberID;
            ublModel.TotalPointNum = totalBetNum;
            ublModel.ItemAppend = itemAppend.Trim(',');
            ublModel.BetNumAppend = betNumAppend.Trim(',');
            ublModel.WinPointNum = 0;

            int effectCount = -1;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                uModel.PointNum = uModel.PointNum - totalBetNum;
                if (uBll.Update(uModel) > 0)
                {
                    ublBll.Insert(ublModel);
                    effectCount = 1;
                }

                scope.Complete();
            }

            if (effectCount > 0)
            {
                WebHelper.MessageBox.Show(this.Page, lbtnSave, "当前第" + rlModel.Period + "期，投注成功！",Request.Url.ToString());
            }
            else
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnSave, "当前第" + rlModel.Period + "期，投注失败，请检查", "系统提示");
            }
        }
    }
}