<%@ Page Title="添加/编辑奖品" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddPrizeTicket.aspx.cs" Inherits="LotterySln.Web.Admin.PrizeTicket.AddPrizeTicket" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <link href="/Scripts/plugins/kindeditor/themes/default/default.css" rel="stylesheet" type="text/css" />
  <link href="/Scripts/plugins/kindeditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" /> 
  <script type="text/javascript" src="/Scripts/plugins/kindeditor/kindeditor.js"></script>
  <script type="text/javascript" src="/Scripts/plugins/kindeditor/lang/zh_CN.js"></script>
  <script type="text/javascript" src="/Scripts/plugins/kindeditor/plugins/code/prettify.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息">
    <div class="row mt10">
      <span class="fl rl"><b class="cr">*</b>所属分类：</span>
      <div class="fl">
          <input id="txtCategory" runat="server" class="easyui-combotree" style="width:200px;" />
      </div>
      <div class="clr"></div>
   </div>
   <div class="row mt10">
      <span class="fl rl"><b class="cr">*</b>名称：</span>
      <div class="fl">
          <input type="text" runat="server" id="txtName" class="easyui-validatebox txt" data-options="required:true,missingMessage:'必填项'" />
      </div>
      <div class="clr"></div>
   </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>棋子数：</span>
        <div class="fl">
            <input type="text" runat="server" id="txtPieceNum" class="easyui-validatebox txt"  data-options="required:true,missingMessage:'必填项'" />
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
    <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>奖品编号：</span>
        <div class="fl">
            <input type="text" runat="server" id="txtPNum" class="easyui-validatebox txt"  data-options="required:true,missingMessage:'必填项'" />
        </div>
        <span class="clr"></span>
   </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>奖品库存：</span>
        <div class="fl">
            <input type="text" runat="server" id="txtStock" class="easyui-validatebox txt"  data-options="required:true,missingMessage:'必填项',validType:'number'" />
        </div>
        <span class="clr"></span>
   </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>奖品介绍：</span>
        <div class="fl">
            <textarea id="txtDescr" runat="server" clientidmode="Static" cols="100" rows="8" style="width:800px;height:400px;visibility:hidden;"></textarea>
        </div>
        <span class="clr"></span>
   </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>兑奖流程：</span>
        <div class="fl">
            <textarea id="txtFlow" runat="server" clientidmode="Static" cols="100" rows="8" style="width:800px;height:400px;visibility:hidden;"></textarea>
        </div>
        <span class="clr"></span>
   </div>
</div>

<asp:LinkButton runat="server" ID="lbtnPostBack" OnCommand="btn_Command" CommandName="lbtnsave" />
<input type="hidden" runat="server" id="hBackToN" value="1" />

<script type="text/javascript" src="/Scripts/Jeasyui.js"></script>
<script type="text/javascript">
    var editor1;
    var editor2;
    KindEditor.ready(function (K) {
        editor1 = K.create('#txtDescr', {
            cssPath: '/Scripts/plugins/kindeditor/plugins/code/prettify.css',
            uploadJson: '/Handlers/KindeditorFilesUpload.ashx',
            fileManagerJson: '/Handlers/KindeditorFiles.ashx',
            allowFileManager: true,
            afterCreate: function () {
                var self = this;
                K.ctrl(document, 13, function () {
                });
                K.ctrl(self.edit.doc, 13, function () {
                });
            }
        });
        editor2 = K.create('#txtFlow', {
            cssPath: '/Scripts/plugins/kindeditor/plugins/code/prettify.css',
            uploadJson: '/Handlers/KindeditorFilesUpload.ashx',
            fileManagerJson: '/Handlers/KindeditorFiles.ashx',
            allowFileManager: true,
            afterCreate: function () {
                var self = this;
                K.ctrl(document, 13, function () {
                });
                K.ctrl(self.edit.doc, 13, function () {
                });
            }
        });
        prettyPrint();

    });
    $(function () {

        //所属分类
        $.ajax({
            url: "/ScriptServices/AdminService.asmx/GetCategoryJson",
            type: "post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var jsonData = (new Function("", "return " + data.d))();
                $('[id$=txtCategory]').combotree('loadData', jsonData);
            }
        });
    })

    function historyGo() {
        var n = parseInt($("[id$=hBackToN]").val());
        history.go(-n);
    }
    function OnSave() {
        var isValid = $('#form1').form('validate');
        if (!isValid) return false;

        $("[id$=txtDescr]").val(editor1.html());
        $("[id$=txtFlow]").val(editor2.html());

        __doPostBack('ctl00$cphMain$lbtnPostBack', '');
    }
</script>

</asp:Content>
