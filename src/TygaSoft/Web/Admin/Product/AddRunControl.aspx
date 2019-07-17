<%@ Page Title="开奖控制" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddRunControl.aspx.cs" Inherits="LotterySln.Web.Admin.Product.AddRunControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar">
   <a href="javasript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-tip',plain:true"></a>您可以对未来5期的开奖进行控制
</div>

<asp:Repeater ID="rpData" runat="server">
    <HeaderTemplate>
        <table id="dgLItem" class="easyui-datagrid" title="开奖控制" data-options="rownumbers:true,singleSelect:true,toolbar:'#toolbar'" style="height:auto;">
        <thead>
            <tr>
                <th data-options="field:'f0',hidden:true">期数</th>
                <th data-options="field:'f1'">期数</th>
                <th data-options="field:'f2'">开奖结果</th>
                <th data-options="field:'f3'">开奖时间</th>
                <th data-options="field:'f4'">开奖状态</th>
                <th data-options="field:'f5'">已投注数</th>
                <th data-options="field:'f6'">棋子总数</th>
            </tr>
        </thead>
        <tbody>
    </HeaderTemplate>
    <ItemTemplate>
            <tr>
                <td><%#Eval("NumberID")%></td>
                <td>第<%#Eval("Period")%>期</td>
                <td> 
                    <input type="text" runat="server" id='txtLotteryNum' value='<%#Eval("LotteryNum")%>' readonly="readonly" onclick="onDlg(this)" />
                    <input type="hidden" runat="server" id="hNid" value='<%#Eval("NumberID")%>' />
                </td>
                <td><%#Eval("RunDate")%></td>
                <td><%#Eval("Status").ToString() == "1" ? "已开奖":"未开奖"%></td>
                <td><%#Eval("BetNum")%></td>
                <td><%#Eval("TotalPointNum")%></td>
            </tr>
    </ItemTemplate>
    <FooterTemplate></tbody></table>
        <%#rpData.Items.Count == 0 ? "<div class='tc m10'>暂无数据记录！</div>" : "" %>
    </FooterTemplate>
</asp:Repeater>

<div id="dlgAdd" class="easyui-dialog" title="请选择开奖元素" style="width:400px;height:200px; padding:10px;" data-options="closed:true,buttons:[{
				text:'确定',
				handler:function(){onDlgSave();}
			},{
				text:'取消',
				handler:function(){$('#dlgAdd').window('close');}
			}]"">
    <div id="dlgContent" runat="server">
        
    </div>
    
    <input type="hidden" id="hClientId" value="" />
</div>

<asp:LinkButton runat="server" ID="lbtnPostBack" OnCommand="btn_Command" CommandName="lbtnSave"/>

<script type="text/javascript" src="/Scripts/Jeasyui.js"></script>

<script type="text/javascript">
    //保存事件
    function OnSave() {

        __doPostBack('ctl00$cphMain$lbtnPostBack', '');
    }

    function onDlg(h) {
        $("#hClientId").val($(h).attr("id"));
        $('#dlgAdd').window('open');
    }

    function onDlgSave() {
        var currClientId = $("#hClientId").val();
        $(document.getElementById(currClientId)).val($("#cbbLItem").combobox('getValue'));
        $('#dlgAdd').window('close');
    }
</script>


</asp:Content>
