using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotterySln.Web.Shares
{
    public partial class Default : System.Web.UI.Page
    {
        BLL.ContentDetail bll;

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
            if (bll == null) bll = new BLL.ContentDetail();

            rpData.DataSource = bll.GetDataSet(1, 10, out totalCount, "", null); ;
            rpData.DataBind();
        }
    }
}