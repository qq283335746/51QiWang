$(function () {
    defaultFun.Init();

    $("#buyItems li:first a").click(function () {
        $(this).addClass("buyItem").siblings().removeClass("buyItem");
        return false;
    })
})

var defaultFun = {
    Init: function () {
        defaultFun.Grid(1, 20);
    },
    Grid: function (pageIndex, pageSize) {
        $.ajax({
            url: "/ScriptServices/SharesService.asmx/GetRunLotteryItem",
            type: "post",
            data: "{pageIndex:" + pageIndex + ",pageSize:" + pageSize + "}",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                var jsonData = (new Function("return " + json.d))();
                var dg = $("#dgPlay");
                dg.datagrid({
                    data: jsonData,
                    pageNumber: pageIndex,
                    pageSize: pageSize
                })

            }
        });

        var p = $('#dgPlay').datagrid('getPager');
        $(p).pagination({
            pageSize: pageSize,  //每页显示的记录条数，默认为10  
            //            pageList: [5,10,15],//可以设置每页记录条数的列表  
            beforePageText: '第', //页数文本框前显示的汉字  
            afterPageText: '页    共 {pages} 页',
            displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
            /*onBeforeRefresh:function(){ 
            $(this).pagination('loading'); 
            alert('before refresh'); 
            $(this).pagination('loaded'); 
            }*/
            onSelectPage: function (pageNumber, pageSize) {
                //showRefresh: false,
                defaultFun.Grid(pageNumber, pageSize);
            }
        });
    },
    joinPlayFormat: function (value, row, index) {
        if (value == 1) {
            return "<a href=\"/u/a.html?nId=" + row.nId + "\" target=\"_blank\">已开奖</a>";
        }
        else {
            return "<a href=\"/u/g.html?nId=" + row.nId + "\" class=\"cr\" target=\"_blank\">投注</a>";
        }
    },
    formatItem: function (value, row, index) {
        if (value != "-") {
            return "<a href=\"javascript:void(0)\" class=\"ci\">" + value + "</a>";
        }
        else return "-";
    }
}