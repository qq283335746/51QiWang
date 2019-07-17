<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="LotterySln.Web.TestPage.WebForm3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #mqIn{ width:400px; height:30px; overflow:hidden; background-color:#000; color:#FFF;}
        #mqIn{ width:400px; height:30px; overflow:hidden; background-color:#000; color:#FFF;}
        #mqIn{ width:400px; height:30px; overflow:hidden; background-color:#000; color:#FFF;}
  div.demo {
    background: #ccc none repeat scroll 0 0;
    border: 3px solid #666;
    margin: 5px;
    padding: 5px;
    position: relative;
    width: 200px;
    height: 100px;
    overflow: auto;
  }
  p {
    margin: 10px;
    padding: 5px;
    border: 2px solid #666;
    width: 1000px;
    height: 1000px;
  }
  </style>
    <script type="text/javascript" src="../Scripts/jquery/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
<div id="mqBox">
    <div id="mqIn">
        <div id="mqA">我们唱着东方红，当家做主站起来，我们唱着春天的故事，改革开放富起来。
        既往开来的领路人，带领我们走进那新时代，的深深地发送到放松放松的方式的方式。实得分实得分是的沃尔沃热温热我惹我
        水电费是否玩儿玩儿玩儿的广泛的广泛地，sdgsdfssdlkjkljk。sdfsdfsfs松岛枫松岛枫，额外热无非是电饭锅电饭锅发到。
        二个梵蒂冈和规范化。规划局后即可、的说法是的方式。
        </div>
    </div>
</div>
 
<script type="text/javascript">
    var step = 0;
    $(function () {
        var itv = null;
        setInterval(mql, 100);
    })
    function mql() {
        step = step + 2;
        if(step)
        $("div.demo").scrollLeft(300);
    }
</script>
    </form>
</body>
</html>
