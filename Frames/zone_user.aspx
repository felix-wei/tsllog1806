<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="zone">

  <div class="menuGroup" id="menu-User">
    <table width=180 cellpadding=0 cellspacing=0 border=0>
    <tr>
    <td align="left">
        Welcome, <%= HttpContext.Current.User.Identity.Name %>
    </td>
    <td align="right">
    <a href="#" class="cmd" rel="exit|/frames/signout.ashx||Do you want to exit the system ?|">Log Out</a>
    </td>
    </tr>
    </table>
    </div>
    <div id="menu-User-tree" class="menuBox" style="border:solid 1px #cccccc;" >
    <table cellpadding="2">
    <tr>
    <td align="">
    <img src='../images/users/<% string path=Page.Server.MapPath(@"~\images\users\"+HttpContext.Current.User.Identity.Name+".jpg");
        if(File.Exists(path))
        {
            Response.Write(HttpContext.Current.User.Identity.Name);
        }else
        {
            Response.Write("work");
        }
        %>.jpg' width="60" />
    </td>
    <td width="10">
    </td>
    <td>
    <a href="#" class="cmd" rel="link|about:blank|calendar|Calendar|#cccccc">Calendar</a>
    <br />
    <a href="#" class="cmd" rel="link|about:blank|alert|Alert|#cccccc">Alerts(2)</a>
    <br />
    <a href="#" class="cmd" rel="external|http://www.portnet.com|webmail|Portnet|#cccccc">Portnet</a>
    <br />
    <%--<a href="#" class="cmd" rel="link|#|tracking|E-Tracking|#EEEEEE">e-Tracking</a>--%>
    <br />
    <%--<a href="#" class="cmd" rel="link|#|website|Website|#EEEEEE">Website</a>--%>
    <br />
    </td>
    </tr>
    </table>
    </div>

    
    </div>
    </form>
</body>
</html>
