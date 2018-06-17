using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_ProcessForCargo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
            lbl_Id.Text = id.ToString();
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobProcess));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + id + "'");
        C2.JobHouse house = C2.Manager.ORManager.GetObject(job) as C2.JobHouse;
        e.NewValues["HouseId"] = id;
        e.NewValues["Qty"] = 1;
        e.NewValues["ProcessQty1"] = 0;
        e.NewValues["ProcessQty2"] = 0;
        e.NewValues["ProcessQty3"] = 0;
        e.NewValues["JobNo"] = jobNo;
        e.NewValues["DateEntry"] = DateTime.Now;
        e.NewValues["DatePlan"] = DateTime.Now;
        //e.NewValues["DateInspect"] = DateTime.Now;
        //e.NewValues["DateProcess"] = DateTime.Now;
        e.NewValues["ProcessType"] = "";
        e.NewValues["ProcessStatus"] = "Pending";
        e.NewValues["LotNo"] = house.BookingNo;
        e.NewValues["LocationCode"] = "";
        e.NewValues["Specs1"] = "";
        e.NewValues["Specs2"] = "";
        e.NewValues["Specs3"] = "";
        e.NewValues["Specs4"] = "";
        e.NewValues["Remark1"] = "";
        e.NewValues["Remark2"] = "";

    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);

        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        e.NewValues["HouseId"] = id;
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["ProcessQty1"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty1"]);
        e.NewValues["ProcessQty2"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty2"]);
        e.NewValues["ProcessQty3"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty3"]);
        e.NewValues["JobNo"] = jobNo;
        e.NewValues["DateEntry"] =SafeValue.SafeDate(e.NewValues["DateEntry"],DateTime.Now);
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
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]); ;

        e.NewValues["RowCreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowCreateTime"] = DateTime.Now;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        e.NewValues["HouseId"] = id;
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["ProcessQty1"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty1"]);
        e.NewValues["ProcessQty2"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty2"]);
        e.NewValues["ProcessQty3"] = SafeValue.SafeDecimal(e.NewValues["ProcessQty3"]);
        e.NewValues["JobNo"] = jobNo;
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

        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        //Update_Volume(id);
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        //Update_Volume(id);
    }
    protected void grid_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        //Update_Volume(id);
    }
    protected void grid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        dsProcess.FilterExpression = "HouseId=" + id + "";
    }
}