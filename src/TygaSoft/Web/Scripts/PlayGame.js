
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
    delayTime = delayTime - 1000;
    if (delayTime < 1) {
        window.clearInterval(timeItvlObj);
        //setTimeRun();
    }
    else {
        $("#lbDelayTime").text(getTimeByTotalMls(delayTime));
    }
}

function runPoint() {
//    n++;
//    p = Math.round(Math.random() * (9), 0);
//    var hItems = $("#hItems").val().split(",");
//    $("#currBuyItem").text(hItems[p]);
//    if (n > 50) {
//        d = 350;
//        window.clearInterval(itvlObj2);
//        itvlObj2 = window.setInterval(runPoint, d);
//    }
//    if (n > 60) {
//        d = 500;
//        window.clearInterval(itvlObj2);
//        itvlObj2 = window.setInterval(runPoint, d);
//    }
//    if (n > 65) {
//        $("#currBuyItem").text(x);
//        n = 0;
//        defaultFun.Init();
//        window.clearInterval(itvlObj2);
//    }

    $("#currBuyItem").text(x);
    defaultFun.Init();
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