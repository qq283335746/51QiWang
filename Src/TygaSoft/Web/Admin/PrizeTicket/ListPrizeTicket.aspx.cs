using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DBUtility;

namespace LotterySln.Web.Admin.PrizeTicket
{
    public partial class ListPrizeTicket : System.Web.UI.Page
    {
        string sqlWhere;
        ParamsHelper parms;
        BLL.PrizeTicket bll;

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
            //查询条件
            GetSearchItem();

            int totalCount = 0;
            if (bll == null) bll = new BLL.PrizeTicket();

            rpData.DataSource = bll.GetDataSet(AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalCount, sqlWhere, parms == null ? null : parms.ToArray()); ;
            rpData.DataBind();
            AspNetPager1.RecordCount = totalCount;
        }

        /// <summary>
        /// 获取列表查询条件项,并构建查询参数集
        /// </summary>
        private void GetSearchItem()
        {
            if (parms == null) parms = new ParamsHelper();

            string sName = txtName.Value.Trim();
            if (!string.IsNullOrEmpty(sName))
            {
                sqlWhere += "and TicketName like @TicketName ";
                SqlParameter parm = new SqlParameter("@TicketName", SqlDbType.NVarChar, 50);
                parm.Value = "%" + sName + "%";
                parms.Add(parm);
            }
            string sCategoryID = txtCategory.Value.Trim();
            if (!string.IsNullOrEmpty(sCategoryID))
            {
                sqlWhere += "and CategoryID = @CategoryID ";
                SqlParameter parm = new SqlParameter("@CategoryID", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(sCategoryID);
                parms.Add(parm);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }

        /// <summary>
        /// 按钮OnCommand事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Command(object sender, CommandEventArgs e)
        {
            string commName = hOp.Value.Trim();
            switch (commName)
            {
                case "reload":
                    Bind();
                    break;
                case "del":
                    OnDelete();
                    break;
                case "search":
                    OnSearch();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        private void OnSearch()
        {
            Bind();
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        private void OnDelete()
        {
            string itemsAppend = hV.Value.Trim();
            if (string.IsNullOrEmpty(itemsAppend))
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "请至少勾选一行进行操作", "错误提醒", "error");
                return;
            }

            if (bll == null) bll = new BLL.PrizeTicket();
            string[] itemsAppendArr = itemsAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = itemsAppendArr.ToList<string>();
            if (bll.DeleteBatch(list))
            {
                WebHelper.MessageBox.MessagerShow(this.Page, lbtnPostBack, "操作成功");
                Bind();
            }
            else
            {
                WebHelper.MessageBox.Messager(this.Page, lbtnPostBack, "操作失败", "系统提示");
            }
        }
    }
}