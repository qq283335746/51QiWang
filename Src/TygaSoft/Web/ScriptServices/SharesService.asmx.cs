using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using CustomProvider;

namespace LotterySln.Web.ScriptServices
{
    /// <summary>
    /// SharesService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class SharesService : System.Web.Services.WebService
    {
        HttpContext context;
        string[] roles;

        [WebMethod]
        public string GetAboutSiteMenu()
        {
            string htmlAppend = string.Empty;
            


            return htmlAppend;
        }

        [WebMethod]
        public string GetMenus(string path)
        {
            string htmlAppend = string.Empty;

            XElement root = XElement.Load(Server.MapPath("~/Web.sitemap"));
            var q = from r in root.Elements().Elements()
                    select r;

            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    string needRole = item.Attribute("roles").Value;
                    if (!string.IsNullOrEmpty(needRole))
                    {
                        if (UserInnRole(needRole))
                        {
                            string selected = "selected:false";
                            string childAppend = GetChildren(item, ref selected, path);
                            htmlAppend += "<div data-options=\"" + selected + "\" title=\"" + item.Attribute("title").Value + "\" style=\"overflow:auto;padding:10px;\">";
                            htmlAppend += childAppend;
                            htmlAppend += "</div>";
                        }
                    }
                    else
                    {
                        string selected = "selected:false";
                        string childAppend = GetChildren(item, ref selected, path);
                        htmlAppend += "<div data-options=\"" + selected + "\" title=\"" + item.Attribute("title").Value + "\" style=\"overflow:auto;padding:10px;\">";
                        htmlAppend += childAppend;
                        htmlAppend += "</div>";
                    }
                }
            }
            return htmlAppend;
        }

        private string GetChildren(XElement xel, ref string selected, string path)
        {
            string htmlAppend = string.Empty;
            var q = from i in xel.Elements()
                    select i;
            if (q.Count() > 0)
            {
                foreach (var item in q)
                {
                    if (UserInnRole(item.Attribute("roles").Value))
                    {
                        if (path.IndexOf(item.Attribute("title").Value) > -1)
                        {
                            selected = "selected:true";
                            htmlAppend += "<a href='" + item.Attribute("url").Value.Replace("~", "") + "' class='hover'>" + item.Attribute("title").Value + "</a>";
                        }
                        else
                        {
                            htmlAppend += "<a href='" + item.Attribute("url").Value.Replace("~", "") + "'>" + item.Attribute("title").Value + "</a>";
                        }
                    }
                }
            }

            return htmlAppend;
        }

        /// <summary>
        /// 当前用户是否具有当前角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private bool UserInnRole(string role)
        {
            if (string.IsNullOrEmpty(role)) return true;

            if (roles == null)
            {
                roles = Roles.GetRolesForUser();
            }

            if (roles != null && roles.Count() > 0)
            {
                string firstRole = roles[0];
                string[] roleArr = role.Split(',');
                return roleArr.Contains(firstRole);
            }

            return false;
        }

        [WebMethod]
        public void OnLayout(int state, string name)
        {
            BLL.MenuNav mnBll = new BLL.MenuNav();
            mnBll.InsertForLayout(name, state.ToString(), GetMenuNavFile());
        }

        [WebMethod]
        public int GetLayoutByName(string name)
        {
            BLL.MenuNav mnBll = new BLL.MenuNav();
            return mnBll.GetLayoutStatus(GetMenuNavFile(), name);
        }

        /// <summary>
        /// 获取当前客户端对应的菜单导航文件
        /// </summary>
        /// <returns></returns>
        private string GetMenuNavFile()
        {
            context = HttpContext.Current;
            CustomProfileCommon profile = new CustomProfileCommon();
            string userName = profile.GetUserName();

            return context.Server.MapPath(string.Format("~/App_Data/SharesData/{0}.xml", userName));
        }

        [WebMethod]
        public string GetRunLotteryItem(int pageIndex, int pageSize)
        {
            int totalCount = 0;
            List<Model.RunLottery> rlList = WebHelper.RunLotteryDataProxy.GetList(pageIndex, pageSize,out totalCount);

            string jsonAppend = "{\"total\":" + totalCount + ",\"rows\":[";

            DateTime currTime = DateTime.Now;

            if (rlList != null && rlList.Count > 0)
            {
                int index = -1;
                foreach (Model.RunLottery model in rlList)
                {
                    index++;

                    int status = model.Status;
                    DateTime runDate = model.RunDate;

                    ////动态增加未开奖的当前期
                    //if (index == 0)
                    //{
                    //    int period = Int32.Parse(dr["Period"].ToString()) + 1;
                    //    for (int i = 4; i > 0; i--)
                    //    {
                    //        int tempPeriod = period + i;
                    //        jsonAppend += "{\"Period\":\"" + tempPeriod + "\",\"ItemName\":\"-\",\"Lud\":\"" + lastUpdateDate.AddMinutes(5 * i) + "\",\"betNum\":\"0\",\"priceNum\":\"100000\",\"winnerNum\":\"0\",\"joinPlay\":\"0\"},";
                    //    }
                    //    jsonAppend += "{\"Period\":\"" + period + "\",\"ItemName\":\"-\",\"Lud\":\"" + lastUpdateDate.AddMinutes(5) + "\",\"betNum\":\"0\",\"priceNum\":\"100000\",\"winnerNum\":\"0\",\"joinPlay\":\"0\"},";
                    //}

                    string itemName = "-";
                    if (status == 1)
                    {
                        itemName = model.ItemName;
                    }
                    if (index > 0) jsonAppend += ",";
                    jsonAppend += "{\"nId\":\"" + HttpUtility.UrlEncode(model.NumberID.ToString()) + "\", \"Period\":\"" + model.Period + "\",\"ItemName\":\"" + itemName + "\",\"RunTime\":\"" + runDate.ToString("MM-dd HH:mm") + "\",\"BetNum\":\"" + model.BetNum + "\",\"PriceNum\":\"" + model.TotalPointNum.ToString() + "\",\"WinnerNum\":\"" + model.WinnerNum.ToString() + "\",\"joinPlay\":\"" + model.Status.ToString() + "\"}";
                }
            }

            jsonAppend += "]}";

            return jsonAppend;
        }

        [WebMethod]
        public string GetLatestLottery()
        {
            BLL.RunLottery bll = new BLL.RunLottery();
            int totalCount = 0;
            DataSet ds = bll.GetRunLotteryItem(1, 6, out totalCount, "", null);
            string jsonAppend = "[";

            if (ds != null && ds.Tables.Count > 0)
            {
                EnumerableRowCollection<DataRow> erc = ds.Tables[0].AsEnumerable();
                DateTime currTime = DateTime.Now;
                DateTime lastUpdatedDate = DateTime.MinValue;
                string itemName = "";

                int totalMilliseconds = 0;
                int remainMilliseconds = 0;
                int betTime = 0;

                DataRow dr = erc.Where(x => int.Parse(x["Status"].ToString()) == 0).OrderBy(x => Int32.Parse(x["Period"].ToString())).First();
                lastUpdatedDate = DateTime.Parse(dr["LastUpdatedDate"].ToString());
                itemName = dr["ItemName"].ToString();

                TimeSpan ts = lastUpdatedDate - currTime;
                totalMilliseconds = Int32.Parse(ts.TotalMilliseconds.ToString("0"));
                remainMilliseconds = totalMilliseconds + 3000;
                betTime = remainMilliseconds - 8000;

                DataRow lastRow = null;
                var q = erc.Where(x => int.Parse(x["Status"].ToString()) == 1).OrderByDescending(x => Int32.Parse(x["Period"].ToString()));
                if (q != null && q.Count() > 0)
                {
                    lastRow = q.First();
                }

                if (lastRow != null)
                {
                    itemName = lastRow["ItemName"].ToString();
                }
                //else
                //{
                //    lastUpdatedDate = DateTime.Parse(dr["LastUpdatedDate"].ToString());
                //    itemName = dr["ItemName"].ToString();

                //    TimeSpan ts = lastUpdatedDate - currTime;
                //    totalMilliseconds = Int32.Parse(ts.TotalMilliseconds.ToString("0"));
                //    remainMilliseconds = totalMilliseconds + 30000;
                //    betTime = remainMilliseconds - 8000;
                //}

                //jsonAppend += "{\"Period\":\"" + dr["Period"].ToString() + "\",\"ItemName\":\"" + lastRow["ItemName"].ToString() + "\",\"Lud\":\"" + remainMilliseconds + "\",\"EndTime\":\"" + currTime.AddMilliseconds(remainMilliseconds) + "\",\"BetTime\":\"" + betTime + "\"}";

                if (totalMilliseconds >= 0 && totalMilliseconds <= 300000)
                {
                    jsonAppend += "{\"Period\":\"" + dr["Period"].ToString() + "\",\"ItemName\":\"" + itemName + "\",\"Lud\":\"" + remainMilliseconds + "\",\"EndTime\":\"" + dr["RunDate"].ToString() + "\",\"BetTime\":\"" + betTime + "\"}";
                }
                else
                {
                    //间隔2分钟
                    jsonAppend += "{\"Period\":\"0\",\"ItemName\":\"准备中\",\"Lud\":\"60000\",\"EndTime\":\"开奖未开始\"}";
                }
            }
            else
            {
                //间隔2分钟
                jsonAppend += "{\"Period\":\"0\",\"ItemName\":\"准备中\",\"Lud\":\"60000\",\"EndTime\":\"开奖未开始\"}";
            }

            jsonAppend += "]";

            return jsonAppend;
        }

        [WebMethod]
        public string GetUserTicketByTop()
        {
            string jsonAppend = "[";

            List<Model.UserTicket> list = WebHelper.UserTicketDataProxy.GetListByTop();
            if (list != null)
            {
                int index = 0;
                foreach (Model.UserTicket utModel in list)
                {
                    string lastTimeDesr = "";
                    TimeSpan ts = DateTime.Now - utModel.LastUpdatedDate;
                    if (ts.TotalMilliseconds > 0)
                    {
                        if (ts.Days > 0)
                        {
                            lastTimeDesr += "" + ts.Days + "天前 ";
                        }
                        if (ts.Hours > 0)
                        {
                            lastTimeDesr += "" + ts.Hours + "小时前 ";
                        }
                        if (ts.Minutes > 0)
                        {
                            lastTimeDesr += "" + ts.Minutes + "分钟前 ";
                        }
                        if (ts.Seconds > 0)
                        {
                            lastTimeDesr += "" + ts.Seconds + "秒前 ";
                        }
                    }
                    if (index > 0) jsonAppend += ",";
                    jsonAppend += "{\"userName\":\"" + utModel.UserName + "\",\"ticketName\":\"" + utModel.TicketName + "\",\"lastTicketTime\":\"" + lastTimeDesr + "\",\"ticketId\":\"" + utModel.PrizeTicketID + "\"}";

                    index++;
                }
            }

            jsonAppend += "]";

            return jsonAppend;
        }
    }
}
