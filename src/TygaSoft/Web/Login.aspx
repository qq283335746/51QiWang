<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LotterySln.Web.Login" %>

<%@ Register src="WebUserControls/Top.ascx" tagname="Top" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>棋王竞猜-登录</title>
    <link href="Styles/Default.css" rel="stylesheet" type="text/css" />
    <link href="Styles/PageMain.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/plugins/jeasyui/themes/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery/jquery-1.10.2.min.js"></script>
    <script src="/Scripts/plugins/jeasyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/plugins/jeasyui/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //动态使登录框垂直居中
            var h = $(window).height();
            h = h - 31;
            $("#pagemain").css("height", "" + h + "px");
        })
    </script>
   
</head>
<body class="loginBody">
    <form id="form1" runat="server">
    <!--header begin-->
    <div id="header">
        <uc1:Top ID="Top1" runat="server" />
    </div>
    <!--header end-->
    <!--pagemain begin-->
    <div id="login">
    <div id="pagemain">
         <div class="w" style="width: 1000px;">
           <div id="loginBoxTop"></div>
           <div id="loginBoxRow">
             <div id="loginBox">
               <div id="loginInfoTop">
                 <div id="righthide"></div>
               </div>
               <span class="clr"></span>
               
               <div id="loginInfo">
                 <ul>
                   <li class="fl">
                       <ul>
                         <li style="height:31px; line-height:31px;">
                             <span class="fl lbUserName"></span>
                             <div class="fl"><input id="txtName" runat="server" maxlength="50" class="easyui-validatebox" data-options="required:true,validType:'length[1,50]'" /></div>
                             <span class="clr"></span>
                         </li>
                         <li style="height:31px; line-height:31px;">
                             <span class="fl lbPsw"></span>
                             <div class="fl"><input type="password" id="txtPsw" runat="server" maxlength="30" class="easyui-validatebox" data-options="required:true,validType:'length[6,30]'" /></div>
                             <span class="clr"></span>
                         </li>
                       </ul>
                   </li>
                   <li class="fl ml10">
                     <a id="abtnLogin" href="javascript:void(0)" class="btnLogin" onclick="onLogin()"></a>
                     <asp:LinkButton runat="server" ID="lbtnPostBack" OnCommand="btn_Command" CommandName="OnLogin" ClientIDMode="Static" />
                   </li>
                  </ul>
                 <span class="clr"></span>
               </div>
             </div>
           </div>
           <div id="loginBoxBottom"></div>
         </div>
      </div>
    </div>
    <!--pagemain end-->
    <!--footer begin-->
    <div id="footer"></div>
    <!--footer eng-->
    </form>

    <script type="text/javascript">
        $(function () {
            $(document).bind("keydown", function (e) {
                var key = e.which;
                if (key == 13) {
                    onLogin();
                }
            })
        })

        function onLogin() {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;
            __doPostBack('lbtnPostBack', '');
        }
    </script>
</body>
</html>
