<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketDetail.aspx.cs" Inherits="LotterySln.Web.Users.TicketDetail" %>

<%@ Register src="~/WebUserControls/SharesTop.ascx" tagname="SharesTop" tagprefix="uc1" %>
<%@ Register src="~/WebUserControls/Footer.ascx" tagname="Footer" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>奖品详情</title>
    <link href="/Styles/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/plugins/jeasyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/Shares/Nav.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--header begin-->
    <div id="header">
        
        <uc1:SharesTop ID="SharesTop1" runat="server" />
        
    </div>
    <!--header end-->
    <!--pagemain begin-->
    <div id="pagemain">
        <div class="w">
            <div class="sl">
                <div class="easyui-panel" title="奖品分类" style="height:100px; padding:5px;">
                    <span>虚拟奖品</span>
                    <span class="cr">></span>
                    <span>手机充值</span>
                </div>
                <div style="display:block; margin-top:10px;"></div>
                <div class="easyui-panel" title="兑奖帮助" style="height:300px; padding:5px;">
                    <asp:Literal runat="server" ID="ltrTicketHelp"></asp:Literal>
                    <span class="clr"></span>
                </div>
                <div style="display:block; margin-top:10px;"></div>
                <div class="easyui-panel" title="最新兑奖" style="height:300px; padding:5px;">
                    <asp:Literal runat="server" ID="ltrLatestTicket"></asp:Literal>
                </div>
                <div style="display:block; margin-top:10px;"></div>
            </div>
            <div class="sr">
                <asp:Repeater runat="server" ID="rpData">
                    <ItemTemplate>

                    <ul>
                        <li class="v_p_img">
                            <img src='<%#Eval("TicketName").ToString().Contains("联通") ? "/Images/mZC0106.jpg" : "/Images/mZC0104.jpg"%>' alt="" />
                        </li>
                        <li class="fl">
                            <ul id="v_p_descr">
                                <li><span class="title"><%#Eval("TicketName")%> </span></li>
                                <li>奖品编号：<%#Eval("PNum")%> </li>
                                <li>兑换价：<%#decimal.Parse(Eval("PointNum").ToString()).ToString("N")%></li>
                                <li>今日剩余数量：<%#Eval("StockNum")%></li>
                                <li class="end">
                                    <asp:LinkButton runat="server" ID="lbtnDh" OnCommand="btn_Command" CommandName="lbtnDh" CommandArgument='<%#Eval("NumberID") %>' CssClass="abtnDh"></asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                    </ul>
                    <span class="clr"></span>
                    <div style="display:block; margin-top:10px;"></div>
                    <div id="pTab" class="easyui-tabs" style="height:508px;">
                        <div title="奖品介绍" style="padding:10px;">
                            <%#Eval("TicketDescr")%>
                        </div>
                        <div title="兑奖流程" style="overflow:auto;padding:10px;">
                            <%#Eval("FlowDescr")%>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
            </div>
            <span class="clr"></span>
            
        </div>
        
    </div>
    <!--pagemain end-->
    <!--footer begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--footer end-->
    </form>
</body>
</html>
