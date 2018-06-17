using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesHr_Report_PrintApplicationForLeave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
    }
    public DataTable GetData()
    {
        string no = "0";
        if (SafeValue.SafeString(Request.QueryString["no"]) != null)
        {
            no = SafeValue.SafeString(Request.QueryString["no"]);
        }
        string sql = string.Format(@"select l.*,p.Name,p.Department,tmp.Days as Entitled,(CONVERT(int, tmp.Days)-CONVERT(int, l.Days)) as BalDays,tmp.Remark as TemRemark from Hr_Leave l left join Hr_Person p on l.ApproveBy=p.Id
left join Hr_LeaveTmp tmp on tmp.Person=l.Person and tmp.LeaveType=l.LeaveType where l.Id={0}", no);
        return ConnectSql.GetTab(sql);
    }
}