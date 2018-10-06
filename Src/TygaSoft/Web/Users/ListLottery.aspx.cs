using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotterySln.Web.Users
{
    public partial class ListLottery : System.Web.UI.Page
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
                hNId.Value = nId;

                Bind();
            }
        }

        private void Bind()
        {
 
        }
    }
}