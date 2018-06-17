using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetEzshipMenu();
        }
    }


    public void SetEzshipMenu()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(RenderEzshipChild1());
        this.menu.InnerHtml = sb.ToString();
    }
    string RenderEzshipChild1()
    {
        StringBuilder sb = new StringBuilder();
        string userName = EzshipHelper.GetUserName();
        string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
        string sql_mast = string.Format("SELECT MasterId, Name, IsActive, SortIndex, RoleName FROM Menu1 where (RoleName = '{0}') AND (IsActive = 'True') order by SortIndex", role);
        DataTable tab_mast = ConnectSql.GetTab(sql_mast);
        for (int i = 0; i < tab_mast.Rows.Count; i++)
        {
            // 2 level only
            string masterId = tab_mast.Rows[i]["MasterId"].ToString();
            string masterName = tab_mast.Rows[i]["Name"].ToString();

            sb.Append("<div class=\"accordionHeader\"><h2>" + masterName + "</h2>");
            sb.Append("</div>");
            sb.Append("<div class=\"accordionContent\" style=\"overflow:auto;\">");
            sb.Append("<ul class=\"tree treeFolder\">");

            sb.Append(RenderEzshipChild2(masterId));

            sb.Append("</ul></div>");
        }
        return sb.ToString();
    }

    string RenderEzshipChild2(string masterId)
    {
        StringBuilder sb = new StringBuilder();
        string sql_sub = string.Format("SELECT  SequenceId,SubId,Name, IsActive, Link, SortIndex FROM Menu2 where MasterId='{0}' AND (IsActive = 'True') order by SortIndex", masterId);
        DataTable tab_sub = ConnectSql.GetTab(sql_sub);
        for (int j = 0; j < tab_sub.Rows.Count; j++)
        {
            string oid = tab_sub.Rows[j]["SequenceId"].ToString();
            string subId = SafeValue.SafeString(tab_sub.Rows[j]["SubId"]);
            string subName = tab_sub.Rows[j]["Name"].ToString();
            string subLink = SafeValue.SafeString(tab_sub.Rows[j]["Link"]);
            sb.Append("<li>");
            int cnt = 0;
            if (subId.Length > 0)
            {
                string sql_sub3 = string.Format("SELECT  count(SequenceId) cnt FROM Menu3 where MasterId='{0}' AND (IsActive = 'True') ", subId);
                cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_sub3), 0);
            }
            if (cnt == 0)
            {
                sb.Append("<a href='" + subLink + "' target=\"navTab\" external=\"true\" rel=\"2f" + oid + "\">" + subName + "");
                sb.Append("</a>");
            }
            else
            {
                sb.Append("<a href=# rel=\"f" + oid + "\">" + subName + "");
                sb.Append("</a>");

                sb.Append("<ul>");
                sb.Append(RenderEzshipChild3(subId));
                sb.Append("</ul>");
            }

            sb.Append("</li>");

        }
        return sb.ToString();
    }

    string RenderEzshipChild3(string masterId)
    {
        StringBuilder sb = new StringBuilder();
        string sql_sub = string.Format("SELECT  SequenceId,Name, IsActive, Link, SortIndex FROM Menu3 where MasterId='{0}' AND (IsActive = 'True') order by SortIndex", masterId);
        DataTable tab_sub = ConnectSql.GetTab(sql_sub);
        for (int j = 0; j < tab_sub.Rows.Count; j++)
        {
            string subId = tab_sub.Rows[j]["SequenceId"].ToString();
            string subName = tab_sub.Rows[j]["Name"].ToString();
            string subLink = tab_sub.Rows[j]["Link"].ToString();

            sb.Append("<li>");
            sb.Append("<a href='" + subLink + "' target=\"navTab\" external=\"true\" rel=\"3f" + subId + "\">" + subName + "");
            sb.Append("</a>");
            sb.Append("</li>");
        }
        return sb.ToString();
    }
}
