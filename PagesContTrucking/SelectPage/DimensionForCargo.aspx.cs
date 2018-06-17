using C2;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_DimensionForCargo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
            string type = SafeValue.SafeString(Request.QueryString["type"]);
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Dimension));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        if (type == "")
            type = "DIMENSION";
        e.NewValues["TypeCode"] = type;
        e.NewValues["HouseId"] = id;
        e.NewValues["Qty"] = 1;
        e.NewValues["Uom"] = "";
        e.NewValues["Weight"] = 0;
        e.NewValues["WeightUnit"] = "";
        e.NewValues["Width"] = 0;
        e.NewValues["WidthUnit"] = "";
        e.NewValues["Length"] = 0;
        e.NewValues["LengthUnit"] = "";
        e.NewValues["Height"] = 0;
        e.NewValues["HeightUnit"] = "";
        e.NewValues["Volume"] = 0;
        e.NewValues["VolumeUnit"] = "";
        e.NewValues["Remark"] = "";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        e.NewValues["HouseId"] = id;
        decimal qty = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        decimal length = SafeValue.SafeDecimal(e.NewValues["Length"]);
        decimal width = SafeValue.SafeDecimal(e.NewValues["Width"]);
        decimal height = SafeValue.SafeDecimal(e.NewValues["Height"]);
        decimal unitQty = SafeValue.SafeDecimal(e.NewValues["PackQty"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Height"] = SafeValue.SafeDecimal(e.NewValues["Height"]);
        e.NewValues["Length"] = SafeValue.SafeDecimal(e.NewValues["Length"]);
        e.NewValues["Width"] = SafeValue.SafeDecimal(e.NewValues["Width"]);
        e.NewValues["Uom"] = SafeValue.SafeString(e.NewValues["Uom"]);
        e.NewValues["WeightUnit"] = SafeValue.SafeString(e.NewValues["WeightUnit"]);
        e.NewValues["LengthUnit"] = SafeValue.SafeString(e.NewValues["LengthUnit"]);
        e.NewValues["WidthUnit"] = SafeValue.SafeString(e.NewValues["WidthUnit"]);
        e.NewValues["HeightUnit"] = SafeValue.SafeString(e.NewValues["HeightUnit"]);
        decimal volume = SafeValue.ChinaRound(length * width * height, 3);
        e.NewValues["Volume"] = volume;

        e.NewValues["VolumeUnit"] = SafeValue.SafeString(e.NewValues["VolumeUnit"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["TotalVol"] = qty * volume;
        decimal totalWt = SafeValue.SafeDecimal(e.NewValues["TotalWt"]);
        e.NewValues["TotalWt"] = SafeValue.SafeDecimal(e.NewValues["TotalWt"]);
        decimal skuQty=qty*unitQty;
        e.NewValues["SkuQty"] = skuQty;

        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        e.NewValues["HouseId"] = id;
        decimal qty = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        decimal length = SafeValue.SafeDecimal(e.NewValues["Length"]);
        decimal width = SafeValue.SafeDecimal(e.NewValues["Width"]);
        decimal height = SafeValue.SafeDecimal(e.NewValues["Height"]);
        decimal unitQty = SafeValue.SafeDecimal(e.NewValues["PackQty"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Height"] = SafeValue.SafeDecimal(e.NewValues["Height"]);
        e.NewValues["Length"] = SafeValue.SafeDecimal(e.NewValues["Length"]);
        e.NewValues["Width"] = SafeValue.SafeDecimal(e.NewValues["Width"]);
        e.NewValues["Uom"] = SafeValue.SafeString(e.NewValues["Uom"]);
        e.NewValues["WeightUnit"] = SafeValue.SafeString(e.NewValues["WeightUnit"]);
        e.NewValues["LengthUnit"] = SafeValue.SafeString(e.NewValues["LengthUnit"]);
        e.NewValues["WidthUnit"] = SafeValue.SafeString(e.NewValues["WidthUnit"]);
        e.NewValues["HeightUnit"] = SafeValue.SafeString(e.NewValues["HeightUnit"]);
        decimal volume = SafeValue.ChinaRound(length * width * height, 3);
        e.NewValues["Volume"] = volume;

        e.NewValues["TotalVol"] = qty * volume;
        decimal totalWt = SafeValue.SafeDecimal(e.NewValues["TotalWt"]);
        e.NewValues["TotalWt"] = totalWt;
        decimal skuQty = qty * unitQty;
        e.NewValues["SkuQty"] = skuQty;

        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    private void Update_Volume(int id) {
        string sql = string.Format(@"select TotalVol,SkuQty,TotalWt from Dimension where HouseId={0}", id);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        decimal totalVolume = 0;
        decimal totalWeight = 0;
        decimal totalSkuQty = 0;
        for (int i = 0; i < dt.Rows.Count;i++ ) {
            DataRow row = dt.Rows[i];
            totalVolume += SafeValue.SafeDecimal(row["TotalVol"]);
            totalWeight += SafeValue.SafeDecimal(row["TotalWt"]);
            totalSkuQty += SafeValue.SafeDecimal(row["SkuQty"]);

        }
        if(totalWeight==0)
            sql = string.Format(@"update job_house set VolumeOrig={1},PackQty={2} where Id={0}", id, totalVolume, totalSkuQty);
        else
            sql = string.Format(@"update job_house set VolumeOrig={1},WeightOrig={2},PackQty={3} where Id={0}", id, totalVolume, totalWeight, totalSkuQty);
        ConnectSql_mb.ExecuteNonQuery(sql);
    }
    protected void grid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        Update_Volume(id);
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        Update_Volume(id);
    }
    protected void grid_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        Update_Volume(id);
    }
    protected void grid_BeforePerformDataSelect(object sender, EventArgs e)
    {
         int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
         dsDimension.FilterExpression = "HouseId=" + id + " and TypeCode='DIMENSION'";
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string para = e.Parameters;
        if (para == "AddLines") {
            int count = SafeValue.SafeInt(txt_Count.Text, 0);
            if (count > 0)
            {
                string userId = HttpContext.Current.User.Identity.Name;
                int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
                string type = SafeValue.SafeString(Request.QueryString["type"]);
                if (type == "")
                    type = "MARUBENI";
                for (int i = 0; i < count; i++)
                {
                    C2.Dimension d = new Dimension();
                    d.HouseId = id;
                    d.HeightUnit = " ";
                    d.Height = 0;
                    d.Length = 0;
                    d.LengthUnit = " ";
                    d.LooseQty = 0;
                    d.PackQty = 0;
                    d.Qty = 0;
                    d.Description = " ";
                    d.Remark = " ";
                    d.Height = 0;
                    d.Height = 0;
                    d.Height = 0;
                    d.Description = " ";
                    d.TypeCode = type;
                    d.RowCreateUser = userId;
                    d.RowCreateTime = DateTime.Now;
                    d.RowUpdateTime = DateTime.Now;
                    d.RowUpdateUser = userId;

                    C2.Manager.ORManager.StartTracking(d, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(d);
                }
                e.Result = "Success";
            }
            else {
                
                e.Result = "Fail!Pls keyin the number";
            }
        }

    }
}