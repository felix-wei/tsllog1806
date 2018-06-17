using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class Page_BillRate : BasePage
{
	protected void page_Init(object sender, EventArgs e)
	{
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if(!IsPostBack)
		{
            btn_ClientId.Text = "";
            if (Request.QueryString["clientId"]!=null)
            {
                btn_ClientId.Text = SafeValue.SafeString(Request.QueryString["clientId"]);
                txt_ClientName.Text = EzshipHelper.GetPartyName(btn_ClientId.Text);
                btn_search_Click(null,null);
            }
		}
	}
	protected void btn_search_Click(object sender, EventArgs e)
	{

        string _client = Helper.Safe.SafeString(btn_ClientId.Text);
        string _charge = Helper.Safe.SafeString(txt_CostChgCode.Text);
        string _jobType = Helper.Safe.SafeString(cbb_JobType.Value);
        string _billType = Helper.Safe.SafeString(cbx_Rate.Value);
        string _scope = Helper.Safe.SafeString(cbb_Scope.Value);
        string _billClass = Helper.Safe.SafeString(cbb_BillClass.Value);
		string where = "JobNo='-1' and LineType='RATE'";
		if(_client.Length > 1)
		{
			if(_client!= "ALL")
                where =GetWhere(where, " ClientId='" + _client + "' ");
		}
		if(_charge.Length > 1)
		{
            where = GetWhere(where, " (ChgCode like '%" + _charge + "%' or ChgCodeDes like '%" + _charge + "%')");
		}
        if(_jobType.Length>1){
            where = GetWhere(where, " JobType='" + _jobType + "'");
        }
        if (_billType.Length > 1)
        {
            where = GetWhere(where, " BillType='" + _billType + "'");
        }
        if (_scope.Length > 1)
        {
            where = GetWhere(where, " BillScope='" + _scope + "'");
        }
        if (_billClass.Length > 1)
        {
            where = GetWhere(where, " BillClass='" + _billClass + "'");
        }
		//throw new Exception(where);
		this.ds1.FilterExpression = where;
	}
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
	protected void btn_export_Click(object sender, EventArgs e)
	{
        btn_search_Click(null, null);
        this.gridExport.WriteXlsToResponse("MasterRate");
	}
	protected void btn_add_Click(object sender, EventArgs e)
	{
		this.grid1.AddNewRow();
	}
	#region quotation det
    protected void Grid1_Init(object sender, EventArgs e)
	{
		ASPxGridView grd = sender as ASPxGridView;
		if (grd != null)
			grd.ForceDataRowType(typeof(C2.JobRate));
	}
	protected void Grid1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
	{
        e.NewValues["Price"] = (decimal)0;
        e.NewValues["Cost"] = (decimal)0;
        e.NewValues["Unit"] = "";
        e.NewValues["Remark"] = " ";
        e.NewValues["MinAmt"] = (decimal)0;
        e.NewValues["Qty"] = (decimal)1;
        e.NewValues["CurrencyId"] = "SGD";
        e.NewValues["ExRate"] = 1;
        e.NewValues["JobNo"] = "-1";
        e.NewValues["JobType"] = "";
        e.NewValues["StorageType"] = " ";
        e.NewValues["LineStatus"] = "N";
        e.NewValues["GstType"] = "";
        e.NewValues["LineType"] = "RATE";
        e.NewValues["DailyNo"] = 1;
        //ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        //ASPxComboBox txt_Client = grid.FindEditRowCellTemplateControl(null, "txt_Client") as ASPxComboBox;
        //if (cbx_Client.Text != "ALL")
        //{
        //    txt_Client.Value = cbx_Client.Value;
        //}
	}
	protected void Grid1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
	{
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxButtonEdit txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_Line_ChgCode") as ASPxButtonEdit;
        e.NewValues["JobNo"] = "-1";

        e.NewValues["ChgCode"] = SafeValue.SafeString(txt_ChgCode.Text);
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", SafeValue.SafeString(txt_ChgCode.Value));
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        e.NewValues["LineStatus"] = Helper.Safe.SafeString(e.NewValues["LineStatus"]).Trim();
        e.NewValues["Unit"] = Helper.Safe.SafeString(e.NewValues["Unit"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
        if (Helper.Safe.SafeString(e.NewValues["ContType"]).Trim() != "")
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim().Substring(0, 2);
        }
        else
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        }
        e.NewValues["GstType"] = Helper.Safe.SafeString(e.NewValues["GstType"]).Trim();
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["BillScope"] = Helper.Safe.SafeString(e.NewValues["BillScope"]).Trim();
        e.NewValues["BillType"] = Helper.Safe.SafeString(e.NewValues["BillType"]).Trim();
        e.NewValues["BillClass"] = Helper.Safe.SafeString(e.NewValues["BillClass"]).Trim();
        e.NewValues["RowCreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowCreateTime"] = DateTime.Now;
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
        if (SafeValue.SafeString(e.NewValues["ChgCodeDe"]) == "")
        {
            e.NewValues["ChgCodeDe"] = chgcodeDes;
        }
        e.NewValues["LineType"] = "RATE"; //Helper.Safe.SafeString(e.NewValues["SkuClass"]).Trim();
        e.NewValues["SkuClass"] = Helper.Safe.SafeString(e.NewValues["SkuClass"]).Trim();
        e.NewValues["SkuUnit"] = Helper.Safe.SafeString(e.NewValues["SkuUnit"]).Trim();
        e.NewValues["JobType"] = Helper.Safe.SafeString(e.NewValues["JobType"]).Trim();
        e.NewValues["StorageType"] = Helper.Safe.SafeString(e.NewValues["StorageType"]).Trim();
        e.NewValues["VehicleType"] = Helper.Safe.SafeString(e.NewValues["VehicleType"]).Trim();
        btn_search_Click(null, null);
    }
    protected void Grid1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
        e.NewValues["LineStatus"] = Helper.Safe.SafeString(e.NewValues["LineStatus"]).Trim();
        e.NewValues["Unit"] = Helper.Safe.SafeString(e.NewValues["Unit"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
        if (Helper.Safe.SafeString(e.NewValues["ContType"]).Trim() != "")
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim().Substring(0, 2);
        }
        else
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        }
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["BillScope"] = Helper.Safe.SafeString(e.NewValues["BillScope"]).Trim();
        e.NewValues["BillType"] = Helper.Safe.SafeString(e.NewValues["BillType"]).Trim();
        e.NewValues["BillClass"] = Helper.Safe.SafeString(e.NewValues["BillClass"]).Trim();
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxButtonEdit txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_Line_ChgCode") as ASPxButtonEdit;
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", SafeValue.SafeString(txt_ChgCode.Value));
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (SafeValue.SafeString(e.NewValues["ChgCodeDe"]) == "")
        {
            e.NewValues["ChgCodeDe"] = chgcodeDes;
        }
        e.NewValues["ChgCode"] = SafeValue.SafeString(txt_ChgCode.Value);
        e.NewValues["ChgCodeDe"]=SafeValue.SafeString(e.NewValues["ChgCodeDe"]);
        e.NewValues["SkuClass"] = Helper.Safe.SafeString(e.NewValues["SkuClass"]).Trim();
        e.NewValues["SkuUnit"] = Helper.Safe.SafeString(e.NewValues["SkuUnit"]).Trim();
        e.NewValues["JobType"] = Helper.Safe.SafeString(e.NewValues["JobType"]).Trim();
        e.NewValues["StorageType"] = Helper.Safe.SafeString(e.NewValues["StorageType"]).Trim();
        e.NewValues["VehicleType"] = Helper.Safe.SafeString(e.NewValues["VehicleType"]).Trim();
        e.NewValues["GstType"] = Helper.Safe.SafeString(e.NewValues["GstType"]).Trim();
        btn_search_Click(null, null);
    }
	protected void Grid1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid1_PageIndexChanged(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void grid1_PageSizeChanged(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
    #endregion


}
