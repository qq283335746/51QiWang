<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SharesTop.ascx.cs" Inherits="LotterySln.Web.WebUserControls.SharesTop" %>
<div class="vt">
    <div class="w">
        <ul class="fl lh">
            <li>
                <a href="/">首 页</a>
            </li>
        </ul>
        <ul class="fr lh">
            <li> 
            <asp:LoginView ID="lvUser" runat="server">
                <AnonymousTemplate> 
                    <a href="/y.html" class="alkc">[注册]</a>
                    <a href="/t.html" class="alkc">[登录]</a>
                </AnonymousTemplate>
                <LoggedInTemplate>
                    <asp:LoginName ID="lnUser" runat="server" FormatString="您好，{0}" />
                    <asp:LoginStatus ID="lsUser" runat="server" LogoutText="[退出]" CssClass="alkc ml10" />
                    |  棋子：<asp:Label ID="lbMyPoint" runat="server"></asp:Label>
                </LoggedInTemplate>
            </asp:LoginView> 
             </li>
        </ul>
        <span class="clr"></span>
    </div>
</div>
<div class="w">
  <div id="logoBox"> 
      棋王竞猜娱乐
  </div>
</div>

<div style="display:none;"><asp:SiteMapPath ID="SitePaths" runat="server" ClientIDMode="Static" /></div>
<div id="nav" class="nav mb10">
  <div class="w">
    <a href="/" class="curr">首 页</a>
    <a href="/u/t.html">兑奖中心</a>
    <a href="/s/t.html">帮助中心</a>
  </div>
</div>
<div class="w">
    <div class="easyui-panel" style="height:35px; overflow:hidden;">
      <ul>
        <li class="fl" style=" height:30px; line-height:30px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">【公告】</a>
        </li>
        <li class="fl" style=" height:30px; line-height:30px; width:384px;">
            <asp:Literal runat="server" ID="ltrNotice"></asp:Literal>
        </li>
        <li class="fl mr10" style=" height:30px; line-height:30px;"><a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">动态</a></li>
        <li class="fl" style=" height:30px; line-height:30px;">
            <asp:Literal runat="server" ID="ltrMq"></asp:Literal>
        </li>
      </ul>
      <span class="clr"></span>
    </div>
    <div style="display:block; margin-top:10px;"></div>
</div>

<script type="text/javascript" src="/Scripts/scrollHelper.js"></script>

<script type="text/javascript">
    $(function () {
        shareNav.Init();

        $("#aScroll").topScroll({ line: 1, speed: 2000, timer: 5000 });
        $("#mqBox").leftScroll({ speed: 40, timer: 100 });
    })
</script>