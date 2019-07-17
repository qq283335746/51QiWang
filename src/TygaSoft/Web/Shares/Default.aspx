<%@ Page Title="开奖-首页" Language="C#" MasterPageFile="~/Shares/Shares.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LotterySln.Web.Shares.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/Shares/Default.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-layout" style="height:633px;">
    <div data-options="region:'east',title:'',split:true,fit:true" style="width:300px;">
        <asp:Repeater ID="rpData" runat="server">
        <HeaderTemplate>
            <table id="bindT" class="easyui-datagrid" title="开奖公告" data-options="rownumbers:false,showHeader:false,fitColumns:true" style="height:auto;">
            <thead>
                <tr>
                    <th data-options="field:'f0'">标题</th>
                    <th data-options="field:'f1'">时间</th>
                </tr>
            </thead>
            <tbody>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td><a title='<%#Eval("Title") %>' href='AddContent.aspx?nId=<%#Eval("NumberID")%>'><%#Eval("Title").ToString().Length > 17 ? Eval("Title").ToString().Substring(1, 17) + "..." : Eval("Title").ToString()%></a> 
                    </td>
                    <td><%#DateTime.Parse(Eval("LastUpdatedDate").ToString()).ToString("yyyy-MM-dd")%></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate></tbody></table>
            <%#rpData.Items.Count == 0 ? "<div class='tc m10'>暂无开奖公告！</div>" : "" %>
        </FooterTemplate>
    </asp:Repeater>
        <div style="display:block; margin-top:5px;"></div>
        <table id="dgDrawingHistory" class="easyui-datagrid" title="开奖历史" data-options="fitColumns:true,singleSelect:true">
            <thead>
                <tr>
                    <th data-options="field:'Period'">期数</th>
                    <th data-options="field:'ItemName'">开奖号码</th>
                    <th data-options="field:'Lud'">开奖时间</th>
                </tr>
            </thead>
        </table>
    </div>
    <div data-options="region:'center',title:'',border:false">
        <div id="buyItems" class="easyui-panel" title="请选中一个进行投注" style="height:80px; padding:15px 10px 10px 10px;">
        <ul>
           <li class="fl">
               <a href="javascript:void(0);" class="easyui-linkbutton">兵</a>
               <a href="javascript:void(0);" class="easyui-linkbutton">炮</a>
               <a href="javascript:void(0);" class="easyui-linkbutton">傌</a>
               <a href="javascript:void(0);" class="easyui-linkbutton">俥</a>
               <a href="javascript:void(0);" class="easyui-linkbutton">相</a>
               <a href="javascript:void(0);" class="easyui-linkbutton">仕</a>
               <a href="javascript:void(0);" class="easyui-linkbutton">帥</a>
               <a href="javascript:void(0);" class="easyui-linkbutton">棋王</a>
           </li>
           <li class="fl" style="margin-left:100px;">
               <a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-add'">投注</a>
           </li>
        </ul> 
    </div>
    <div style="display:block; margin-top:5px;"></div>
    <div id="inBuyItems" class="easyui-panel" title="已投注项" style="height:300px;padding:5px;">
    
    </div>
    </div>
</div>

</asp:Content>
