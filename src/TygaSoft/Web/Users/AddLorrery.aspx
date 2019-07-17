<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddLorrery.aspx.cs" Inherits="LotterySln.Web.Users.AddLorrery" %>

<%@ Register src="~/WebUserControls/SharesTop.ascx" tagname="SharesTop" tagprefix="uc1" %>
<%@ Register src="~/WebUserControls/Footer.ascx" tagname="Footer" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>竞猜 投注 游戏</title>
    <link href="/Styles/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/plugins/jeasyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/Shares/Nav.js" type="text/javascript"></script>
    <script src="/Scripts/bindT.js" type="text/javascript"></script>
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
            <div class="easyui-panel" style="height:60px; overflow:hidden;background-color:#E0ECFF;">
                <ul>
                    <li class="fl" style=" border-right:1px solid #95B8E7; text-align:center; height:60px; line-height:60px; width:49%;">
                        <a id="abtnPriod" href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-redo',plain:true">第<span class="lbCurrPeriod">0</span>期</a>
                        <span id="lbBetTime"></span>停止竞猜，<span id="lbDelayTime">0</span>开奖    
                    </li>
                    <li class="fl" style="text-align:center; height:60px; line-height:60px; width:50%;">
                        <div style="margin:0 auto; width:200px;">
                            <ul>
                               <li class="fl"><a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true">上一期开奖结果：</a></li>
                               <li class="fl"><a id="currBuyItem" href="javascript:void(0)" class="ci"></a></li>
                           </ul>
                           <span class="clr"></span>
                        </div>
                    </li>
                </ul>
                <span class="clr"></span>
            </div>
            <div style="display:block; margin-top:10px;"></div>
            
            <div class="totalRow">
                <div id="totalBox" class="fl">
                    <ul>
                        <li class="fl">总投注棋子：<span id="totalBetNum">0</span></li>
                        <li class="fr"><a id="abtnConformBet" href="javascript:void(0)" class="conform_btn">确认投注</a></li>
                    </ul>
                </div>
                <div id="abtnConformNext" class="fl">
                    <a href="javascript:void(0)" onclick="abtnClear()">清除</a>
                </div>
                <span class="clr"></span>
            </div>
            <div style="display:block; margin-top:10px;"></div>

            <div class="mtb10">
                第<span id="lbBetPeriod" runat="server">0</span>期投注
            </div>

            <table id="dgBet" class="easyui-datagrid" data-options="fitColumns:true">
            <thead>
                <tr>
                    <th data-options="field:'ItemCode',hidden:true"></th>
                    <th data-options="field:'ItemName',width:100,formatter:formatItem">号码</th>
                    <th data-options="field:'CurrRatio',width:100">赔率</th>
                    <th data-options="field:'betNum',width:100,formatter:formatBet">投注</th>
                    <th data-options="field:'multiple',width:100,formatter:formatMultiple">倍数</th>
                    
                </tr>
            </thead>
            </table>
        </div>
        
    </div>

    <input type="hidden" runat="server" id="hItemAppend" clientidmode="Static" value="" enableviewstate="false" />
    <asp:LinkButton runat="server" ID="lbtnSave" OnCommand="btn_Command" CommandName="lbtnsave" />

    <!--pagemain end-->
    <!--footer begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--footer end-->
    </form>

    <script type="text/javascript">

        var timeItvlObj;
        var itvlObj;
        var itvlObj2;
        var n = 0;
        var d = 100;
        var p;
        var delay = 120000;
        var x = "准备中";
        var delayTime = -1;
        var betTime = -1;
        var isRunPoint = false;

        $(function () {
            setTimeRun();

        })

        function delayTimeFun() {
            if (x == "准备中") {
                window.clearInterval(timeItvlObj);
                return;
            }
            isRunPoint = true;
            betTime = betTime - 1000;
            if (betTime < 1) {
                $("#lbBetTime").text("已");
            }
            else {
                $("#lbBetTime").text("还有" + getTimeByTotalMls(betTime));
            }
            if (delayTime < 1) {
                window.clearInterval(timeItvlObj);
                //setTimeRun();
            }
            else {
                delayTime = delayTime - 1000;
                $("#lbDelayTime").text(getTimeByTotalMls(delayTime));
            }
        }

        function runPoint() {

            $("#currBuyItem").text(x);
            //defaultFun.Init();
        }

        function setTimeRun() {
            $.ajax({
                url: "/ScriptServices/SharesService.asmx/GetLatestLottery",
                type: "post",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                success: function (json) {
                    var jsonData = (new Function("return " + json.d))();
                    $.map(jsonData, function (item) {
                        betTime = item.BetTime;
                        delay = item.Lud;
                        delayTime = item.Lud;
                        x = item.ItemName;
                        $(".lbCurrPeriod").text(item.Period);

                        if (item.ItemName != "准备中" && item.ItemName != $("#currBuyItem").text()) {
                            if (isRunPoint) {
                                runPoint();
                                //itvlObj2 = window.setInterval(runPoint, 100);
                                isRunPoint = false;
                            }
                            else {
                                $("#currBuyItem").text(x);
                            }
                        }

                        if (item.ItemName != "准备中") {

                            $("#lbDelayTime").text(getTimeByTotalMls(item.Lud));
                            $("#lbBetTime").text(getTimeByTotalMls(item.BetTime));
                        }
                        else {
                            $("#currBuyItem").text("准备中");
                            $("#lbDelayTime").text("00:00:00");
                            $("#lbBetTime").text("00:00:00");
                        }

                        window.clearInterval(itvlObj);
                        itvlObj = window.setInterval(setTimeRun, delay);

                        window.clearInterval(timeItvlObj);
                        timeItvlObj = window.setInterval(delayTimeFun, 1000);
                    })
                }
            });
        }

        function getTimeByTotalMls(totalMls) {
            var minute = Math.floor((totalMls / 60000));
            var second = minute * 60000;
            second = (totalMls - second) / 1000;
            second = Math.floor(second);
            if (second < 10) second = "0" + second;
            if (minute < 10) minute = "0" + minute;

            return "00:" + minute + ":" + second + "";
        }
    </script>

    <script type="text/javascript">
        $(function () {

            //确认投注事件
            $("#abtnConformBet").click(function () {
                if (betTime <= 0) {
                    $.messager.alert('温馨提示', '投注时间已到，已禁止投注！', 'info');
                    return false;
                }
                var itemAppend = "";
                var totalBetNum = parseInt($("#totalBetNum").text());
                if (totalBetNum <= 0) {
                    $.messager.alert('温馨提示', '请先投注！', 'info');
                    return false;
                }
                itemAppend += "all|" + totalBetNum + "";
                var dgRows = $("#dgBet").datagrid('getRows');
                $(".txtBet").each(function (index, item) {
                    var currNum = $(item).val();
                    if ((/\d+/.test(currNum))) {
                        itemAppend += "," + dgRows[index].ItemCode + "|" + currNum;
                    }
                })

                $("#hItemAppend").val(itemAppend);

                __doPostBack('lbtnSave', '');

                return false;
            })

            getBetList();

            $(".txtBet").change(function () {
                var betNum = $.trim($(this).val());
                if (betNum.length > 0) {
                    var reg = /(\d)+/;
                    if (!reg.test(betNum)) {
                        $(this).val("");
                        return;
                    }
                    $(this).parent().parent().parent().find("[type=checkbox]").attr("checked", true);
                }
            })
            $(".multiple").click(function () {
                var txtBet = $(".txtBet");
                var sBetNum = $.trim(txtBet.val());
                if (sBetNum.length > 0) {
                    var reg = /(\d)+/;
                    if (!reg.test(sBetNum)) {
                        $.messager.alert('温馨提醒', '投注请输入整数');
                        return false;
                    }

                    var sBetNum = sBetNum * $(this).text();
                    $(this).parent().parent().prev().find("[type=text]").val(sBetNum);
                }

                return false;
            })

            setInterval(setTotalBet, 100);
        })

        function setTotalBet() {
            var txtBetList = $(".datagrid-btable").find("[type=text]");
            var totalBetNum = 0;
            txtBetList.each(function () {
                var sBetNum = $.trim($(this).val());
                var reg = /(\d)+/;
                if (reg.test(sBetNum)) {
                    var betNum = parseInt(sBetNum);
                    totalBetNum = totalBetNum + betNum;
                }
            })

            $("#totalBetNum").text(totalBetNum);
        }

        function abtnInverse() {
            //            var cbList = $("[name=cbItem]");
            //            cbList.filter(':checked').attr("checked", false);

            $("[name=cbItem]").each(function () {
                $(this).attr("checked", !$(this).attr("checked"));
            });

            return false;

            //cbList.filter(not(':checked')).attr("checked", true);
            //            cbList.each(function () {
            //                if ($(this).is(':checked')) {
            //                    $(this).removeAttr("checked");
            //                    console.log("checked");
            //                }
            //                else {
            //                    $(this).attr("checked", "checked");
            //                    console.log("nochecked");
            //                }
            //            })

        }

        function abtnClear() {
            var dg = $(".datagrid-btable");
            dg.find("[type=checkbox]").attr("checked", false);
            dg.find("[type=text]").val("");
            return false;
        }

        function getBetList() {
            $.ajax({
                url: "/ScriptServices/UsersService.asmx/GetBetList",
                type: "post",
                data: "{}",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (json) {
                    var jsonData = (new Function("return " + json.d))();
                    var dg = $("#dgBet");
                    dg.datagrid({
                        data: jsonData
                    })

                }
            });
        }

        function formatCb(value, row, index) {
            return "<input type=\"checkbox\" name=\"cbItem\" value=\"checkbox\">";
        }

        function formatMultiple(value, row, index) {
            return "<a class=\"multiple\" href=\"javascript:void(0)\">.5</a><a class=\"multiple\" href=\"javascript:void(0)\">2</a><a class=\"multiple\" href=\"javascript:void(0)\">10</a>";
        }

        function formatBet(value, row, index) {
            return "<input type=\"text\" class=\"txtBet\" />";
        }

        function formatItem(value, row, index) {
            return "<a href=\"javascript:void(0)\" class=\"ci\">" + value + "</a>";
        }

    </script>
</body>
</html>
