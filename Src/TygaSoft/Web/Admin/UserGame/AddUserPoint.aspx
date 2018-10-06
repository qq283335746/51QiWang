<%@ Page Title="新建/编辑用户棋子" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddUserPoint.aspx.cs" Inherits="LotterySln.Web.Admin.UserGame.AddUserPoint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script src="/Scripts/JeasyuiExtend.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息">
   <div class="row mt10">
      <span class="fl rl"><b class="cr">*</b>用户：</span>
      <div class="fl">
          <input class="easyui-combobox" id="cbbUser" runat="server" clientidmode="Static" data-options="valueField:'id',textField:'name'" />
      </div>
      <div class="clr"></div>
   </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>棋子数：</span>
        <div class="fl">
            <input class="easyui-validatebox txt" type="text" id="txtPoint" runat="server" data-options="required:true,missingMessage:'必填项',validType:'number'" />
        </div>
        <div class="clr"></div>
    </div>
</div>

<asp:LinkButton runat="server" ID="lbtnPostBack" OnCommand="btn_Command" CommandName="lbtnsave" />
<input type="hidden" runat="server" id="hBackToN" value="1" />
<script type="text/javascript" src="/Scripts/Jeasyui.js"></script>
<script type="text/javascript">

    function historyGo() {
        var n = parseInt($("[id$=hBackToN]").val());
        history.go(-n);
    }
    function OnSave() {
        var isValid = $('#form1').form('validate');
        if (!isValid) return false;
        __doPostBack('ctl00$cphMain$lbtnPostBack', '');
    }
</script>

</asp:Content>
