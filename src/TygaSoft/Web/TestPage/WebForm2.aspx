<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="LotterySln.Web.TestPage.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            <div id="easyuiLayou" class="easyui-layout" style="height:780px; width:99.999%;">
                <div data-options="region:'north',title:'',split:true" style="height:105px; padding:5px; background-color:#E0ECFF;">
                    <div id="runBox">
                        <div class="easyui-panel" style="background-color:#E0ECFF; padding:5px">
                            <ul class="fl" style="width:260px;">
                                <li><h3>棋王游戏机</h3> </li>
                                <li>第<span class="lbCurrPeriod"></span>期</li>
                                <li>本期销售截止时间： <span id="lbEndTime"></span></li>
                                <li>剩余时间：<span id="lbDelayTime"></span></li>
                            </ul>
                            <ul class="fl">
                                <li class="fl"><h3>棋王游戏机</h3></li>
                                <li class="fl ml10">第<span class="lbCurrPeriod"></span>期 </li>
                                <li style="text-align:center;">
                                   <span id="currBuyItem" style="font-size:48px; height:48px; line-height:48px;"></span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div data-options="region:'west',title:'功能菜单',split:true" style="width:230px;">
                    <div id="menuNav"></div>
                </div>
                <div data-options="region:'center',title:''" style="padding:5px;">
                    <asp:ContentPlaceHolder ID="cphMain" runat="server" />
                </div>
                <div data-options="region:'south',title:'',split:true" style="height:30px; background-color:#E0ECFF; padding-top:5px;">
                    <div class="tc">Copyright ©  2013-2014 天涯孤岸版权所有</div>
                </div>
            </div>
        </div>
    </div>
    <!--pagemain end-->
    <!--footer begin-->
    <div id="footer"> 
        
    </div>
    <!--footer end-->

        <input id="hItems" type="hidden" value="兵,炮,傌,俥,相,仕,帥,棋王" />
    </form>
    <script type="text/javascript">
        $(function () {
            MenusFun.Init();
        })
    </script>
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
            if (delayTime < 1) {
                window.clearInterval(timeItvlObj);
                //setTimeRun();
            }
            else {
                delayTime = delayTime - 1000;
                var minute = Math.floor((delayTime / 60000));
                var second = minute * 60000;
                second = (delayTime - second) / 1000;
                second = Math.floor(second);
                if (second < 10) second = "0" + second;
                $("#lbDelayTime").text("00:0" + minute + ":" + second + "");
            }
        }

        function runPoint() {
            n++;
            p = Math.round(Math.random() * (9), 0);
            var hItems = $("#hItems").val().split(",");
            $("#currBuyItem").text(hItems[p]);
            if (n > 50) {
                d = 350;
                window.clearInterval(itvlObj2);
                itvlObj2 = window.setInterval(runPoint, d);
            }
            if (n > 60) {
                d = 500;
                window.clearInterval(itvlObj2);
                itvlObj2 = window.setInterval(runPoint, d);
            }
            if (n > 65) {
                $("#currBuyItem").text(x);
                n = 0;
                defaultFun.Init();
                console.log("n444---" + n);
                window.clearInterval(itvlObj2);
            }
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
                        delay = item.Lud;
                        delayTime = item.Lud;
                        x = item.ItemName;

                        $(".lbCurrPeriod").text(item.Period);
                        $("#lbEndTime").text(item.EndTime);

                        if (item.ItemName != "准备中" && item.ItemName != $("#currBuyItem").text()) {
                            if (isRunPoint) {
                                itvlObj2 = window.setInterval(runPoint, 100);
                                isRunPoint = false;
                            }
                            else {
                                $("#currBuyItem").text(x);
                            }
                        }

                        if (item.ItemName != "准备中") {
                            var minute = Math.floor((item.Lud / 60000));
                            var second = Math.floor(item.Lud % 60);
                            if (second < 10) second = "0" + second;
                            $("#lbDelayTime").text("00:0" + minute + ":" + second + "");
                        }
                        else {
                            $("#currBuyItem").text("准备中");
                            $("#lbDelayTime").text("00:00:00");
                        }

                        window.clearInterval(itvlObj);
                        itvlObj = window.setInterval(setTimeRun, delay);

                        window.clearInterval(timeItvlObj);
                        timeItvlObj = window.setInterval(delayTimeFun, 1000);
                    })
                }
            });
        }
    </script>
</body>
</html>
