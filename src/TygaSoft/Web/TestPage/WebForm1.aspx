<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LotterySln.Web.TestPage.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <span id="lbPoint" style="font-size:100px;"></span>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
        <script type="text/javascript">
            var itvlObj;
            var n = 0;
            var d = 100;
            var p;

            $(function () {
                itvlObj = window.setInterval(setPoint, 60000);

            })

            function beginPoint() {
                itvlObj = window.setInterval(setPoint, 60000);
            }

            function setPoint() {
                n++;
                p = Math.round(Math.random() * (9), 0);
                $("#lbPoint").text(p);
                if (n > 50) {
                    //                var r = Math.random();
                    d = 350;
                    window.clearInterval(intervalObj);
                    itvlObj = window.setInterval(setPoint, d);
                }
                if (n > 60) {
                    d = 500;
                    window.clearInterval(intervalObj);
                    itvlObj = window.setInterval(setPoint, d);
                }
                if (n > 65) {
                    //window.clearInterval(intervalObj);
                    //intervalObj = window.setInterval(setPoint, 1000);
                    console.log("n444---" + n);
                }
            }
    </script>
</body>
</html>
