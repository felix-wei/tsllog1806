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
        <%
            string userName = HttpContext.Current.User.Identity.Name;
            string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();

            string sql_mast = string.Format("SELECT MasterId, Name, Color, IsActive, SortIndex, RoleName,Img FROM MenuMast where (RoleName = '{0}') AND (IsActive = 'True') order by SortIndex", role);
            DataTable tab_mast = ConnectSql.GetTab(sql_mast);
            for (int i = 0; i < tab_mast.Rows.Count; i++)
            {
                // 2 level only
                string masterId = tab_mast.Rows[i]["MasterId"].ToString();
                string masterName = tab_mast.Rows[i]["Name"].ToString();
                string masterColor = tab_mast.Rows[i]["Color"].ToString();
                string masterImg = tab_mast.Rows[i]["Img"].ToString();
                string sql_sub = string.Format("SELECT SequenceId,Name, Color, IsActive, Link, LinkType, SortIndex FROM MenuSub where MasterId='{0}' AND (IsActive = 'True') order by SortIndex", masterId);
                DataTable tab_sub = ConnectSql.GetTab(sql_sub);

                if (tab_sub.Rows.Count > 0)
                {
                    // write parent menu
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < tab_sub.Rows.Count; j++)
                    {
                        string subId = tab_sub.Rows[j]["SequenceId"].ToString();
                        string subName = tab_sub.Rows[j]["Name"].ToString();
                        string subColor = tab_sub.Rows[j]["Color"].ToString();
                        string subLink = tab_sub.Rows[j]["Link"].ToString();
                        string subLinkType = tab_sub.Rows[j]["LinkType"].ToString();
                        if (subName == "-")
                        {
                            sb.AppendFormat("<div class='menuItem'  id='menu-item-{0}' style='border-bottom:solid 1px #CCCCCC;height:4px;margin-bottom:2px;'></div>", subId);
                        }
                        else
                        {
                            sb.AppendFormat("<div class='menuItem'  id='menu-item-{0}'><img class='imgNode' src=''/><a class='cmd' href='#' rel='{3}|{4}|{5}|{6}|{7}'>&nbsp;&nbsp;&nbsp;&nbsp;{1}</a></div>", subId, subName, subColor, subLinkType.ToLower(), subLink, subId, subName, subColor);
                        }
                        // verify by rile1=--
                    }

                    Response.Write(string.Format("<div class='menuGroup' id='menu-{0}'><img class='imgMenu' src='{3}' id='img-{0}-group' />&nbsp;&nbsp;{1}</div>", masterId, masterName, masterColor, masterImg));
                    Response.Write(string.Format("<div id='menu-{0}-tree' class='menuBox' style='display:none;border:solid 1px {1}'>", masterId, masterColor));
                    Response.Write(sb.ToString());
                    Response.Write("</div>");
                    // write content           

                }
            }
        %>
    </div>
    </form>
</body>
</html>
