using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Vehicle_Vehicle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["no"] != null)
                txt_Code.Text = SafeValue.SafeString(Request.QueryString["no"]);
        }
        btn_search_Click(null, null);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "1=1";
        if (txt_Code.Text.Length>0)
        {
            where = "VehicleCode='"+txt_Code.Text+"'";
        }
        //string where = "datediff(d,ContractDate,'" + date_search.Date + "')=0";
       
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.RefVehicle));
        }
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["VehicleCode"], "") == "")
        {
            throw new Exception("VehicleCode is request");
        }
        e.NewValues["VehicleType"] = SafeValue.SafeString(e.NewValues["VehicleType"], "");
        e.NewValues["VehicleStatus"] = SafeValue.SafeString(e.NewValues["VehicleStatus"], "");
        e.NewValues["ContractNo"] = SafeValue.SafeString(e.NewValues["ContractNo"], "");
        e.NewValues["ContractType"] = SafeValue.SafeString(e.NewValues["ContractType"], "");
        e.NewValues["ContractDate"] = SafeValue.SafeDate(e.NewValues["ContractDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContractExpiryDate"] = SafeValue.SafeDate(e.NewValues["ContractExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["LicenseNo"] = SafeValue.SafeString(e.NewValues["LicenseNo"], "");
        e.NewValues["LicenseExpiryDate"] = SafeValue.SafeDate(e.NewValues["LicenseExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["SupplierCode"] = SafeValue.SafeString(e.NewValues["SupplierCode"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");

        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], new DateTime(1990, 1, 1));
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], new DateTime(1990, 1, 1));

    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["VehicleCode"], "") == "")
        {
            throw new Exception("VehicleCode is request");
        }
        e.NewValues["VehicleType"] = SafeValue.SafeString(e.NewValues["VehicleType"], "");
        e.NewValues["VehicleStatus"] = SafeValue.SafeString(e.NewValues["VehicleStatus"], "");
        e.NewValues["VeicleSize"] = SafeValue.SafeString(e.NewValues["VeicleSize"], "");
        e.NewValues["VehicleUse"] = SafeValue.SafeString(e.NewValues["VehicleUse"], "");
        e.NewValues["ContractNo"] = SafeValue.SafeString(e.NewValues["ContractNo"], "");
        e.NewValues["ContractType"] = SafeValue.SafeString(e.NewValues["ContractType"], "");
        e.NewValues["ContractDate"] = SafeValue.SafeDate(e.NewValues["ContractDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContractExpiryDate"] = SafeValue.SafeDate(e.NewValues["ContractExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["LicenseNo"] = SafeValue.SafeString(e.NewValues["LicenseNo"], "");
        e.NewValues["LicenseExpiryDate"] = SafeValue.SafeDate(e.NewValues["LicenseExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["SupplierCode"] = SafeValue.SafeString(e.NewValues["SupplierCode"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], new DateTime(1990, 1, 1));
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], new DateTime(1990, 1, 1));
        e.NewValues["InsuranceExpiryDate"] = SafeValue.SafeDate(e.NewValues["InsuranceExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["RoadTaxExpiryDate"] = SafeValue.SafeDate(e.NewValues["RoadTaxExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["CraneLGCertExpiryDate"] = SafeValue.SafeDate(e.NewValues["CraneLGCertExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["CraneLHCertExpiryDate"] = SafeValue.SafeDate(e.NewValues["CraneLHCertExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["VpcExpiryDate"] = SafeValue.SafeDate(e.NewValues["VpcExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["LastInternalInspectionDate"] = SafeValue.SafeDate(e.NewValues["LastInternalInspectionDate"], new DateTime(1753, 1, 1));
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["VehicleStatus"] = "Active";
        e.NewValues["ContractDate"] = DateTime.Now;
        e.NewValues["ContractExpiryDate"] = DateTime.Now.AddYears(1);
        e.NewValues["LicenseExpiryDate"] = DateTime.Now.AddYears(1);

        e.NewValues["Date1"] = DateTime.Now;
        e.NewValues["Date2"] = DateTime.Now;
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Transport_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date1")
        {

            DateTime dtime = SafeValue.SafeDate(e.CellValue, new DateTime(1990, 1, 1));
            DateTime dtime_now = DateTime.Now;
            if (dtime_now.AddDays(5).CompareTo(dtime) > 0)
            {
                e.Cell.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                if (dtime_now.AddDays(30).CompareTo(dtime) > 0)
                {
                    e.Cell.BackColor = System.Drawing.Color.Orange;
                }

            }
        }
        if (e.DataColumn.FieldName == "Date2")
        {

            DateTime dtime = SafeValue.SafeDate(e.CellValue, new DateTime(1990, 1, 1));
            DateTime dtime_now = DateTime.Now;
            if (dtime_now.AddDays(5).CompareTo(dtime) > 0)
            {
                e.Cell.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                if (dtime_now.AddDays(30).CompareTo(dtime) > 0)
                {
                    e.Cell.BackColor = System.Drawing.Color.Orange;
                }

            }
        }
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("PrimeMover_" + DateTime.Now.ToString("yyyyMMdd_HHmmsss"), true);
    }
    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string temp = e.Parameters;
        string[] ar = temp.Split('_');
        if (ar.Length == 2)
        {
            if (ar[0] == "OpenInline")
            {
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_VehicleCode = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_VehicleCode") as ASPxLabel;
                e.Result = lbl_VehicleCode.Text;
            }
        }
    }
}