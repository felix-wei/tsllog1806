using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class WareHouse_Job_Quote_Edit : System.Web.UI.Page
{
	public string job_no = "0";
	public string ref_no = "0";
	
    protected void Page_Init(object sender, EventArgs e)
    {
		job_no = Request.QueryString["rev"] ?? "0";
		ref_no = Request.QueryString["no"] ?? "0";
		dsJobCost.FilterExpression = "JobNo='"+job_no+"' and RefNo='"+ref_no+"'";
		
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
	
  
    #region container
    protected void Grid_RefCont_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobCost));
        }
    }
    protected void Grid_RefCont_DataSelect(object sender, EventArgs e)
    {
		job_no = Request.QueryString["rev"] ?? "0";	
		ref_no = Request.QueryString["no"] ?? "0";
        this.dsJobCost.FilterExpression = "ChgCode<>'-' and RefNo='" + ref_no + "' and JobNo='"+job_no+"'";
    }
    protected void Grid_RefCont_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
		e.NewValues["SalePrice"] = 0;
		e.NewValues["Remark"] = "";
		job_no = Request.QueryString["rev"] ?? "0";	
		ref_no = Request.QueryString["no"] ?? "0";
        string typ = ref_no.Substring(2,2); 
		if(typ=="OM") {	
			e.NewValues["ChgCode"] = "OM";
			e.NewValues["ChgCodeDe"] = "Office move charges";
		}
		if(typ=="LM") {	
			e.NewValues["ChgCode"] = "LM";
			e.NewValues["ChgCodeDe"] = "Local move charges";
		}
		if(typ=="SR") {	
			e.NewValues["ChgCode"] = "STO";
			e.NewValues["ChgCodeDe"] = "Storage charges";
		}
		if(typ=="IB") {	
			e.NewValues["ChgCode"] = "IBS";
			e.NewValues["ChgCodeDe"] = "Inbound service charges";
		}
		if(typ=="OB") {	
			e.NewValues["ChgCode"] = "DTD";
			e.NewValues["ChgCodeDe"] = "Door to door charges";
		}
    }
    protected void Grid_RefCont_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
		job_no = Request.QueryString["rev"] ?? "0";	
		ref_no = Request.QueryString["no"] ?? "0";
        e.NewValues["RefNo"] = ref_no;
        e.NewValues["JobNo"] = job_no;
    }
    protected void Grid_RefCont_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDe"] = SafeValue.SafeString(e.NewValues["ChgCodeDe"]);
        e.NewValues["SalePrice"] = SafeValue.SafeDecimal(e.NewValues["SalePrice"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void Grid_RefCont_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string oid = e.Values["SequenceId"].ToString();
        if (oid.Length > 0)
        {
            string sql = string.Format("delete from JobCost where SequenceId='{0}'", oid);
            int res = Manager.ORManager.ExecuteCommand(sql);
            e.Cancel = true;
        }
    }

    #endregion
}