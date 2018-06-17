using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_WareHouse_Job_ContainerMonitor : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            btn_search_Click(null, null);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = string.Format(@"select d.*,dbo.fun_GetPartyName(mast.PartyId) as PartyName,mast.PartyId,mast.CustomerReference,mast.CustomerDate 
        ,mast.DoDate,mast.DoType from Wh_DoDet3 d inner join Wh_do mast on d.DoNo=mast.DoNo");
        string dateFrom = "";
        string dateTo = "";
        string dateRef = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (date_RefDate.Value != null)
        {
            dateRef = date_RefDate.Date.ToString("yyyy-MM-dd");
        }
        //else if (this.txt_CustId.Text.Length > 0)
        //{
        //    where = GetWhere(where, "mast.PartyId='" + this.txt_CustId.Text.Trim() + "'");
        //}
        else if (this.txt_CustomerRef.Text.Length > 0)
        {
            where = GetWhere(where, "CustomerReference='" + this.txt_CustomerRef.Text.Trim() + "'");
        }
        else if (dateRef.Length > 0)
        {
            where = GetWhere(where, "CustomerDate='" + dateRef + "'");
        }
        else if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " JobStart >= '" + dateFrom + "' and JobStart < '" + dateTo + "'");
        }

        if (where.Length > 0)
        {
            sql += " where " + where ;
        }
        DataTable tab = ConnectSql.GetTab(sql);
        dsIssueDet3.FilterExpression=where;
        dsIssueDet3.DataBind();
        
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    public string VilaStatus(string status)
    {
        string strStatus = "";
        if (status == "USE")
        {
            strStatus = "NEW";
        }
        if (status == "CLS")
        {
            strStatus = "COMPLATED";
        }
        if (status == "CNL")
        {
            strStatus = "CANCEL";
        }
        return strStatus;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "InTransit")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }
        return color;
    }
    protected void grid_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
    {

    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ContainerStatus"] = SafeValue.SafeString(e.NewValues["ContainerStatus"]);
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["M3"] = SafeValue.SafeDecimal(e.NewValues["M3"]);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"],0);
        e.NewValues["JobStart"] = SafeValue.SafeDate(e.NewValues["JobStart"], DateTime.Now);
        e.NewValues["JobEnd"] = SafeValue.SafeDate(e.NewValues["JobEnd"], DateTime.Now);
        e.NewValues["SealNo"] = SafeValue.SafeString(e.NewValues["SealNo"]);
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["PkgType"] = SafeValue.SafeString(e.NewValues["PkgType"]);
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateBy"] = userId;
        e.NewValues["UpdateDateTime"] = DateTime.Now;

        btn_search_Click(null,null);
    }
}