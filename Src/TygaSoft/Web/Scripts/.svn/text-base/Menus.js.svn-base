
var MenusFun = {
    Init: function () {
        MenusFun.InitAccordion();
        MenusFun.InitLayout();
//        MenusFun.InitTabs();
    },

    //左侧（或右侧）
    InitAccordion: function () {
        $.ajax({
            url: "/ScriptServices/SharesService.asmx/GetMenus",
            type: "post",
            data: "{path:'" + $("#SitePaths").text() + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (html) {
                $("#menuNav").html(html.d);
                $("#menuNav").accordion({
                    fit: true,
                    border: false
                });
            }
        });
    },

    InitTabs: function () {
        $.ajax({
            url: "/ScriptServices/SharesService.asmx/GetTabs",
            type: "post",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                $.map(json.d, function (item) {
                    $('#tt').tabs('add', {
                        title: item.Title + "<input type=\"hidden\" value=\"" + item.Url + "\" />",
                        closable: item.Title != "我的桌面",
                        selected: item.Selected
                    });
                })

                var tt = $("#tt");
                var tabsInner = tt.find("[class=tabs-inner]");
                tabsInner.click(function () {
                    var hTabsV = $(this).find("[type=hidden]").val();
                    if (hTabsV.length > 0) {
                        window.location = hTabsV;
                    }
                })
                var tabsClose = tt.find("[class=tabs-close]");
                tabsClose.click(function () {
                    var hTabsV = $(this).prev().find("[type=hidden]").val();
                    MenusFun.OnTabsClose(hTabsV);
                })
            }
        });
    },

    OnTabsClose: function (h) {
        $.ajax({
            url: "/ScriptServices/SharesService.asmx/TabsClose",
            type: "post",
            data: "{url:'" + h + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (s) {
                window.location = s.d;
            }
        });
    },

    //缩小或展开 读取
    InitLayout: function () {
        $('#easyuiLayou').layout('panel', 'west').panel({
            onCollapse: function () {
                MenusFun.OnLayout("1", "west");
            },
            onExpand: function () {
                MenusFun.OnLayout("0", "west");
            }
        });
        $.ajax({
            url: "/ScriptServices/SharesService.asmx/GetLayoutByName",
            type: "post",
            data: "{name:'west'}",
            contentType: "application/json; charset=utf-8",
            success: function (html) {
                if (html.d == 1) {
                    $('#easyuiLayou').layout('collapse', 'west');
                }
            }
        });
    },

    //缩小或展开 写入
    OnLayout: function (h, name) {
        $.ajax({
            url: "/ScriptServices/SharesService.asmx/OnLayout",
            type: "post",
            data: "{state:" + h + ",name:'" + name + "'}",
            contentType: "application/json; charset=utf-8",
            success: function () {

            }
        });
    }
}

var AdminMenus = {
    Init: function () {
        AdminMenus.InitAccordion();
        AdminMenus.InitLayout();
        AdminMenus.InitTabs();
    },

    //左侧（或右侧）
    InitAccordion: function () {
        $.ajax({
            url: "/ScriptServices/AdminService.asmx/GetMenus",
            type: "post",
            data: "{path:'" + $("#SitePaths").text() + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (html) {
                $("#menuNav").html(html.d);
                $("#menuNav").accordion({
                    fit: true,
                    border: false
                });
            }
        });
    },

    InitTabs: function () {
        $.ajax({
            url: "/ScriptServices/AdminService.asmx/GetTabs",
            type: "post",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                $.map(json.d, function (item) {
                    $('#tt').tabs('add', {
                        title: item.Title + "<input type=\"hidden\" value=\"" + item.Url + "\" />",
                        closable: item.Title != "我的桌面",
                        selected: item.Selected
                    });
                })

                var tt = $("#tt");
                var tabsInner = tt.find("[class=tabs-inner]");
                tabsInner.click(function () {
                    var hTabsV = $(this).find("[type=hidden]").val();
                    if (hTabsV.length > 0) {
                        window.location = hTabsV;
                    }
                })
                var tabsClose = tt.find("[class=tabs-close]");
                tabsClose.click(function () {
                    var hTabsV = $(this).prev().find("[type=hidden]").val();
                    AdminMenus.OnTabsClose(hTabsV);
                })
            }
        });
    },

    OnTabsClose: function (h) {
        $.ajax({
            url: "/ScriptServices/AdminService.asmx/TabsClose",
            type: "post",
            data: "{url:'" + h + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (s) {
                window.location = s.d;
            }
        });
    },

    //缩小或展开 读取
    InitLayout: function () {
        $('#body').layout('panel', 'east').panel({
            onCollapse: function () {
                AdminMenus.OnLayout("1", "east");
            },
            onExpand: function () {
                AdminMenus.OnLayout("0", "east");
            }
        });
        $.ajax({
            url: "/ScriptServices/AdminService.asmx/GetLayoutByName",
            type: "post",
            data: "{name:'east'}",
            contentType: "application/json; charset=utf-8",
            success: function (html) {
                if (html.d == 1) {
                    $('#body').layout('collapse', 'east');
                }
            }
        });
    },

    //缩小或展开 写入
    OnLayout: function (h, name) {
        $.ajax({
            url: "/ScriptServices/AdminService.asmx/OnLayout",
            type: "post",
            data: "{state:" + h + ",name:'" + name + "'}",
            contentType: "application/json; charset=utf-8",
            success: function () {

            }
        });
    }
}