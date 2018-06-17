using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Tpt_WarehouseList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_search_dateFrom.Date = DateTime.Now;
            txt_search_dateTo.Date = DateTime.Now;
            btn_search_Click(null, null);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {

        string From = txt_search_dateFrom.Date.ToString("yyyyMMdd");
        string To = txt_search_dateTo.Date.AddDays(1).ToString("yyyyMMdd");
        string JobNo = txt_search_jobNo.Text;

        string where = string.Format(@"DatePlan>='{0}' and DatePlan<'{1}'", From, To);
        if (JobNo != null && JobNo.Length > 0)
        {
            where += " and JobNo like '%" + JobNo + "'";
        }
        dsProcess.FilterExpression = where;

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_save2excel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("JobProcess_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), true);
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string temp = e.Parameters;
        string[] ar = temp.Split('_');
        if (ar.Length == 3)
        {
            if (ar[0] == "UpdateInlineStatus")
            {
                contCfsStatusChange(sender, e, SafeValue.SafeInt(ar[1], -1), ar[2]);
            }
        }
    }
    private void contCfsStatusChange(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex, string CfsStatus)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }

        ASPxLabel lb_id = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lb_Id") as ASPxLabel;
        string res = "Save Error";
        if (lb_id != null)
        {

            string sql = string.Format(@"update Job_Process set ProcessStatus=@StatusCode,DateProcess=getdate() 
where Id=@Oid");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@Oid", lb_id.Text, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", CfsStatus, SqlDbType.NVarChar, 100));
            if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            {
                res = "Save successful!";
            }

        }
        e.Result = res;
    }
   
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobProcess));
        }
    }

    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        e.NewValues["HouseId"] = id;
        e.NewValues["Qty"] = 1;
        e.NewValues["ProcessQty1"] = 0;
        e.NewValues["ProcessQty2"] = 0;
        e.NewValues["ProcessQty3"] = 0;
        e.NewValues["JobNo"] = jobNo;
        e.NewValues["DateEntry"] = DateTime.Now;
        e.NewValues["DatePlan"] = DateTime.Now;
        e.NewValues["DateInspect"] = DateTime.Now;
        e.NewValues["DateProcess"] = DateTime.Now;
        e.NewValues["ProcessType"] = "";
        e.NewValues["ProcessStatus"] = "Pending";
        e.NewValues["LotNo"] = "";
        e.NewValues["LocationCode"] = "";
        e.NewValues["Specs1"] = "";
        e.NewValues["Specs2"] = "";
        e.NewValues["Specs3"] = "";
        e.NewValues["Specs4"] = "";
        e.NewValues["Remark1"] = "";
        e.NewValues["Remark2"] = "";
    }

    protected void grid_Transport_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }

    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string id = SafeValue.SafeString(e.NewValues["InventoryId"]);
        string pipeNo = SafeValue.SafeString(e.NewValues["PipeNo"]);
        string heatNo = SafeValue.SafeString(e.NewValues["HeatNo"]);
        bool res = Is_Exis(id, pipeNo, heatNo);
        if (res == true)
            throw new Exception("Can not keyin,Already exist");
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["ProcessQty1"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty1"]);
        e.NewValues["ProcessQty2"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty2"]);
        e.NewValues["ProcessQty3"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty3"]);
        e.NewValues["DateEntry"] = SafeValue.SafeDate(e.NewValues["DateEntry"], DateTime.Now);
        e.NewValues["DatePlan"] = SafeValue.SafeDate(e.NewValues["DatePlan"], DateTime.Now);
        e.NewValues["DateInspect"] = SafeValue.SafeDate(e.NewValues["DateInspect"], DateTime.Now);
        e.NewValues["DateProcess"] = SafeValue.SafeDate(e.NewValues["DateProcess"], DateTime.Now);
        e.NewValues["ProcessType"] = SafeValue.SafeString(e.NewValues["ProcessType"]);
        e.NewValues["ProcessStatus"] = SafeValue.SafeString(e.NewValues["ProcessStatus"]);
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["LocationCode"] = SafeValue.SafeString(e.NewValues["LocationCode"]);
        e.NewValues["Specs1"] = SafeValue.SafeString(e.NewValues["Specs1"]);
        e.NewValues["Specs2"] = SafeValue.SafeString(e.NewValues["Specs2"]);
        e.NewValues["Specs3"] = SafeValue.SafeString(e.NewValues["Specs3"]);
        e.NewValues["Specs4"] = SafeValue.SafeString(e.NewValues["Specs4"]);
        e.NewValues["Remark1"] = SafeValue.SafeString(e.NewValues["Remark1"]);
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]);
        e.NewValues["PipeNo"] = SafeValue.SafeString(e.NewValues["PipeNo"]);
        e.NewValues["HeatNo"] = SafeValue.SafeString(e.NewValues["HeatNo"]);
        e.NewValues["InventoryId"] = SafeValue.SafeString(e.NewValues["InventoryId"]);
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;

        btn_search_Click(null, null);
    }

    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string id = SafeValue.SafeString(e.NewValues["InventoryId"]);
        string pipeNo = SafeValue.SafeString(e.NewValues["PipeNo"]);
        string heatNo = SafeValue.SafeString(e.NewValues["HeatNo"]);
        bool res = Is_Exis(id, pipeNo, heatNo);
        if (res == true)
            throw new Exception("Can not keyin,Already exist");
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["ProcessQty1"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty1"]);
        e.NewValues["ProcessQty2"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty2"]);
        e.NewValues["ProcessQty3"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty3"]);
        e.NewValues["DateEntry"] = SafeValue.SafeDate(e.NewValues["DateEntry"], DateTime.Now);
        e.NewValues["DatePlan"] = SafeValue.SafeDate(e.NewValues["DatePlan"], DateTime.Now);
        e.NewValues["DateInspect"] = SafeValue.SafeDate(e.NewValues["DateInspect"], DateTime.Now);
        e.NewValues["DateProcess"] = SafeValue.SafeDate(e.NewValues["DateProcess"], DateTime.Now);
        e.NewValues["ProcessType"] = SafeValue.SafeString(e.NewValues["ProcessType"]);
        e.NewValues["ProcessStatus"] = SafeValue.SafeString(e.NewValues["ProcessStatus"]);
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["LocationCode"] = SafeValue.SafeString(e.NewValues["LocationCode"]);
        e.NewValues["Specs1"] = SafeValue.SafeString(e.NewValues["Specs1"]);
        e.NewValues["Specs2"] = SafeValue.SafeString(e.NewValues["Specs2"]);
        e.NewValues["Specs3"] = SafeValue.SafeString(e.NewValues["Specs3"]);
        e.NewValues["Specs4"] = SafeValue.SafeString(e.NewValues["Specs4"]);
        e.NewValues["Remark1"] = SafeValue.SafeString(e.NewValues["Remark1"]);
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]);
        e.NewValues["PipeNo"] = SafeValue.SafeString(e.NewValues["PipeNo"]);
        e.NewValues["HeatNo"] = SafeValue.SafeString(e.NewValues["HeatNo"]);
        e.NewValues["InventoryId"] = SafeValue.SafeString(e.NewValues["InventoryId"]);
        e.NewValues["RowCreateTime"] = DateTime.Now;
        e.NewValues["RowCreateUser"] = HttpContext.Current.User.Identity.Name;

        btn_search_Click(null, null);
    }

    private bool Is_Exis(string inventoryId,string pipeNo,string heatNo) {
        bool res = false;
        string sql = string.Format(@"select count(*) from Dimension where HouseId={0} and PipeNo='{1}'", inventoryId,pipeNo);
        int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);
        if (n > 0)
            res = true;
        return res;
    }
}