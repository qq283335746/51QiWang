<%@ Page Title="帮助中心 棋王竞猜娱乐 棋王竞猜首选网站" Language="C#" MasterPageFile="~/Shares/Shares.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LotterySln.Web.Shares.AboutSite.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="easyuiLayou" class="easyui-layout" style="height:780px;">
    <div data-options="region:'west',title:'菜单导航',split:true" style="width:230px; padding:5px;">
        <div id="helpNav" runat="server" clientidmode="Static">
        </div>
        <span class="clr"></span>
    </div>
    <div data-options="region:'center',title:''" style="padding:5px;">
        <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
    </div>
</div>

<script type="text/javascript">
    $(function () {
        var currTitle = $.trim($("#currTitle").text());
        $("#helpNav a").filter(":contains('" + currTitle + "')").addClass("cr");
    })
</script>

</asp:Content>
