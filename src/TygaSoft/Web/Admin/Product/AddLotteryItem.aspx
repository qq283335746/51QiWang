<%@ Page Title="新建/编辑开奖元素" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddLotteryItem.aspx.cs" Inherits="LotterySln.Web.Admin.Product.AddLotteryItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息">
   <div class="row mt10">
      <span class="fl rl"><b class="cr">*</b>名称：</span>
      <div class="fl">
          <input type="text" runat="server" id="txtName" class="easyui-validatebox txt" data-options="required:true,missingMessage:'必填项'" />
      </div>
      <div class="clr"></div>
   </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>代号：</span>
        <div class="fl">
            <input type="text" runat="server" id="txtCode" class="easyui-validatebox txt"  data-options="required:true,missingMessage:'必填项'" />
        </div>
        <div class="clr"></div>
    </div>
    <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>赔率：</span>
        <div class="fl">
            <input type="text" runat="server" id="txtRatio" class="easyui-validatebox txt"  data-options="required:true,missingMessage:'必填项'" />
        </div>
        <div class="clr"></div>
    </div>
   <div class="row mtb10">
        <span class="fl rl">图片：</span>
        <div class="fl">
            <input type="text" runat="server" id="txtImageUrl" class="easyui-validatebox txt"  data-options="required:true,missingMessage:'必填项'" />
        </div>
        <div class="clr"></div>
    </div>
</div>

<asp:LinkButton runat="server" ID="lbtnPostBack" OnCommand="btn_Command" CommandName="lbtnsave" />
<input type="hidden" runat="server" id="hBackToN" value="1" />
<script type="text/javascript" src="/Scripts/Jeasyui.js"></script>
<script type="text/javascript">

    $(function () {
        
    })

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
