<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketSuccess.aspx.cs" Inherits="LotterySln.Web.Users.TicketSuccess" %>

<%@ Register src="~/WebUserControls/SharesTop.ascx" tagname="SharesTop" tagprefix="uc1" %>
<%@ Register src="~/WebUserControls/Footer.ascx" tagname="Footer" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>奖品兑换 成功</title>
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
                <div style="background-color:#E0ECFF; height:100px; line-height:100px; padding:20px;">
                    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-tip',plain:true">兑换成功，我们将尽快处理。</a>
                </div>
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
