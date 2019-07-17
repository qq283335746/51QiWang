<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LotterySln.Web.Default" %>

<%@ Register src="~/WebUserControls/SharesTop.ascx" tagname="SharesTop" tagprefix="uc1" %>
<%@ Register src="~/WebUserControls/Footer.ascx" tagname="Footer" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页-棋王竞猜娱乐</title>
    <link rel="shortcut" href="favicon.ico"/>
    <link href="/Styles/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/plugins/jeasyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/Shares/Nav.js" type="text/javascript"></script>
    <script src="/Scripts/Shares/Default.js" type="text/javascript"></script>
    <script src="/Scripts/PlayGame.js" type="text/javascript"></script>
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
                        <span id="lbBetTime">00:00:00</span>停止竞猜，<span id="lbDelayTime">00:00:00</span>开奖    
                    </li>
                    <li class="fl" style="text-align:center; height:60px; line-height:60px; width:50%;">
                        <div style="margin:0 auto; width:200px;">
                            <ul>
                               <li class="fl"><a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true">上一期开奖结果：</a></li>
                               <li class="fl"><a id="currBuyItem" href="javascript:void(0)" class="ci">准备中</a></li>
                           </ul>
                           <span class="clr"></span>
                        </div>
                    </li>
                </ul>
                <span class="clr"></span>
            </div>
            <div style="display:block; margin-top:10px;"></div>
            <table id="dgPlay" class="easyui-datagrid" title="" data-options="fitColumns:true,singleSelect:true,pagination:true">
            <thead>
                <tr>
                    <th data-options="field:'nId',hidden:true"></th>
                    <th data-options="field:'Period',width:100">期号</th>
                    <th data-options="field:'RunTime',width:100">开奖时间</th>
                    <th data-options="field:'ItemName',width:100,formatter:defaultFun.formatItem">开奖结果</th>
                    <th data-options="field:'BetNum',width:100">已投注数</th>
                    <th data-options="field:'PriceNum',width:100">棋子总数</th>
                    <th data-options="field:'WinnerNum',width:100">中奖人数</th>
                    <th data-options="field:'joinPlay',width:100,formatter:defaultFun.joinPlayFormat">参与</th>
                </tr>
            </thead>
        </table>
        </div>
        
    </div>
    <!--pagemain end-->
    <!--footer begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--footer end-->
    </form>
</body>
</html>
