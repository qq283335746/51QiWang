<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListLottery.aspx.cs" Inherits="LotterySln.Web.Users.ListLottery" %>

<%@ Register src="~/WebUserControls/SharesTop.ascx" tagname="SharesTop" tagprefix="uc1" %>
<%@ Register src="~/WebUserControls/Footer.ascx" tagname="Footer" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>开奖 我的投注 明细</title>
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
            
            <div class="easyui-panel" style="height:100px; overflow:hidden;background-color:#E0ECFF; padding:10px;">
                <ul>
                    <li class="fl" style="width:25%; height:50px; line-height:50px;">
                        期号：<span class="cr" id="lbPeriod">0</span> 
                    </li>
                    <li class="fl" style="width:25%; height:50px; line-height:50px;">开奖状态：已开奖</li>
                    <li class="fl" style="width:25%; height:50px; line-height:50px;">开奖时间：<span id="lbRunDate">0</span> </li>
                    <li class="fl" style="width:25%; height:50px; line-height:50px;">
                        <span class="fl"> 开奖结果：</span><a href="javascript:void(0)" class="fl ci" id="lbLotteryNum">未知</a>
                    </li>
                </ul>
                <ul>
                    <li class="fl" style="width:25%; height:33px; line-height:33px;">我的投注额：<span id="lbAllBetNum">0</span>
                        <img src="/Images/egg.gif" alt="" />
                    </li>
                    <li class="fl" style="width:25%; height:33px; line-height:33px;">获得棋子：<span id="lbAllWinNum">0</span> <img src="/Images/egg.gif" alt="" /></li>
                    <li class="fl" style="width:25%; height:33px; line-height:33px;">我的盈亏：<span id="lbAllBetWinNum">0</span> <img src="/Images/egg.gif" alt="" /></li>
                    <li class="fl" style="width:25%; height:33px; line-height:33px;">盈亏比例：<span id="lbAllBetWinRatio">0</span></li>
                </ul>
                <span class="clr"></span>
            </div>

            <div class="mtb10">
                第<span id="lbPeriodD">0</span>期明细
            </div>
            <div style="display:block; margin-top:10px;"></div>
            <table id="dgLottery" class="easyui-datagrid" data-options="fitColumns:true,singleSelect:true">
            <thead>
                <tr>
                    <th data-options="field:'NId',hidden:true"></th>
                    <th data-options="field:'allBetNum',hidden:true"></th>
                    <th data-options="field:'allWinNum',hidden:true"></th>
                    <th data-options="field:'allBetWinNum',hidden:true"></th>
                    <th data-options="field:'allBetWinRatio',hidden:true"></th>
                    <th data-options="field:'Period',hidden:true"></th>
                    <th data-options="field:'LotteryNum',hidden:true"></th>
                    <th data-options="field:'RunDate',hidden:true"></th>
                    <th data-options="field:'ItemName',width:100,align:'center',halign:'center',formatter:formatItem">号码</th>
                    <th data-options="field:'CurrRatio',align:'center',halign:'center',width:100">赔率</th>
                    <th data-options="field:'BetNum',align:'center',halign:'center',width:100">我的投注额</th>
                    <th data-options="field:'WinNum',align:'center',halign:'center',width:100">获得棋子</th>
                </tr>
            </thead>
            </table>

            <input type="hidden" id="hNId" runat="server" clientidmode="Static" />

        </div>
        
    </div>
    <!--pagemain end-->
    <!--footer begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--footer end-->
    </form>

    <script type="text/javascript">
        $(function () {
            getLotteryList();

        })

        function getLotteryList() {
            $.ajax({
                url: "/ScriptServices/UsersService.asmx/GetSinglePeriod",
                type: "post",
                data: "{numberId:'" + $("#hNId").val() + "'}",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (json) {
                    var jsonData = (new Function("return " + json.d))();
                    var dg = $("#dgLottery");
                    dg.datagrid({
                        data: jsonData,
                        onLoadSuccess: function (data) {
                            var rows = $(this).datagrid('getRows');
                            if (rows.length > 0) {
                                var row = rows[0];
                                $('#lbLotteryNum').text(row.LotteryNum);
                                $('#lbPeriod').text(row.Period);
                                $('#lbRunDate').text(row.RunDate);
                                $('#lbAllBetNum').text(row.allBetNum);
                                $('#lbAllWinNum').text(row.allWinNum);
                                $('#lbAllBetWinNum').text(row.allBetWinNum);
                                $('#lbAllBetWinRatio').text(row.allBetWinRatio);
                                $('#lbPeriodD').text(row.Period);
                            }
                        }
                    })

                }
            });
        }

        function formatItem(value, row, index) {
            return "<a href=\"javascript:void(0)\" class=\"ci\">" + value + "</a>";
        }

    </script>
</body>
</html>
