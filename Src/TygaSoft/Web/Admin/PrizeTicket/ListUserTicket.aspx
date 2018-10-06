<%@ Page Title="用户兑奖列表" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListUserTicket.aspx.cs" Inherits="LotterySln.Web.Admin.PrizeTicket.ListUserTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar">
  用户：<input type="text" runat="server" id="txtName" maxlength="256" class="txt" />
  <a id="abtnSearch" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'">查 询</a>
</div>

<asp:Repeater ID="rpData" runat="server">
    <HeaderTemplate>
        <table id="bindT" class="easyui-datagrid" title="用户兑奖列表" data-options="rownumbers:true,toolbar:'#toolbar'" style="height:auto;">
        <thead>
            <tr>
                <th data-options="field:'f0',checkbox:true"></th>
                <th data-options="field:'f7',hidden:true"></th>
                <th data-options="field:'f2',sortable:true">用户</th>
                <th data-options="field:'f3',sortable:true">奖品</th>
                <th data-options="field:'f4',sortable:true">兑换日期</th>
                <th data-options="field:'f5',sortable:true">处理状态</th>
            </tr>
        </thead>
        <tbody>
    </HeaderTemplate>
    <ItemTemplate>
            <tr>
                <td><%#Eval("NumberID")%></td>
                <td><%#Eval("Status")%></td>
                <td><%#Eval("UserName")%></td>
                <td><%#Eval("TicketName")%></td>
                <td><%#Eval("LastUpdatedDate")%></td>
                <td><%#Eval("Status").ToString() == "1" ? "已处理":"未处理"%></td>
            </tr>
    </ItemTemplate>
    <FooterTemplate></tbody></table>
        <%#rpData.Items.Count == 0 ? "<div class='tc m10'>暂无数据记录！</div>" : "" %>
    </FooterTemplate>
</asp:Repeater>
      
<asp:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="false" CssClass="pages mt10" CurrentPageButtonClass="cpb"
    ShowPageIndexBox="Never" PageSize="50" OnPageChanged="AspNetPager1_PageChanged"
    EnableTheming="true" FirstPageText="首页" LastPageText="末页" NextPageText="下一页" PrevPageText="上一页" 
    ShowCustomInfoSection="Never" CustomInfoHTML="当前第%CurrentPageIndex%页/共%PageCount%页，每页显示%PageSize%条">
</asp:AspNetPager>

<asp:LinkButton runat="server" ID="lbtnPostBack" OnCommand="btn_Command"/>
<input type="hidden" runat="server" id="hOp" value="" />
<input type="hidden" runat="server" id="hV" value="" />

<script src="/Scripts/Jeasyui.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {

        $("#abtnSearch").click(function () {
            $("[id$=hOp]").val("search");
            __doPostBack('ctl00$cphMain$lbtnPostBack', '');
        })

        $("#abtnDel").click(function () {
            var cbl = $('#bindT').datagrid("getSelections");
            var cblLen = cbl.length;
            if (cblLen == 0) {
                $.messager.alert('错误提醒', '请至少选择一行数据再进行操作', 'error');
                return false;
            }
            $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
                if (r) {
                    var itemsAppend = "";
                    for (var i = 0; i < cbl.length; i++) {
                        itemsAppend += cbl[i].f0 + ",";
                    }

                    $("[id$=hV]").val(itemsAppend);
                    $("[id$=hOp]").val("del");
                    __doPostBack('ctl00$cphMain$lbtnPostBack', '');
                }
            });
        })
    })

    function OnSave() {
        var cbl = $('#bindT').datagrid("getSelections");
        var cblLen = cbl.length;
        if (cblLen == 0) {
            $.messager.alert('错误提醒', '请至少选择一行数据再进行操作', 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要处理当前兑奖吗？', function (r) {
            if (r) {
                var itemsAppend = "";
                for (var i = 0; i < cbl.length; i++) {
                    if (cbl[i].f7 == "1") {
                        $.messager.alert('错误提醒', '选择行中包含已兑奖，不能选择已兑奖的行进行操作', 'error');
                        return false;
                    }
                    if (i > 0) {
                        itemsAppend += ",";
                    }
                    itemsAppend += cbl[i].f0;
                }

                $("[id$=hV]").val(itemsAppend);
                $("[id$=hOp]").val("saveStatus");
                __doPostBack('ctl00$cphMain$lbtnPostBack', '');
            }
        });
    }
</script>

</asp:Content>
