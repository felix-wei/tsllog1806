using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_Driver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsTransport.FilterExpression = "1=1";
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "1=1";
        if (txt_search_Code.Text.Trim().Length > 0)
        {
            where += " and Code like '%" + txt_search_Code.Text.Trim() + "%' ";
        }
        if (txt_search_Name.Text.Trim().Length > 0)
        {
            where += " and Name like '%" + txt_search_Name.Text.Trim() + "%' ";
        }
        if (cbb_TeamNo.Text.Trim().Length > 0)
        {
            where += " and TeamNo='" + cbb_TeamNo.Text.Trim() + "'";
        }
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Transport_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {

    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmDriver));
        }
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox Code = this.grid_Transport.FindEditFormTemplateControl("txt_Code") as ASPxTextBox;
        //e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        check_save(Code.Text);
        e.NewValues["Isstaff"] = SafeValue.SafeString(e.NewValues["Isstaff"], "N");
        e.NewValues["ServiceLevel"] = SafeValue.SafeString(e.NewValues["ServiceLevel"], "Level1");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "Active");
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);
        e.NewValues["Tel"] = SafeValue.SafeString(e.NewValues["Tel"]);
        e.NewValues["ICNo"] = SafeValue.SafeString(e.NewValues["ICNo"]);
        e.NewValues["TowheaderCode"] = SafeValue.SafeString(e.NewValues["TowheaderCode"]);
        e.NewValues["LicenseNo"] = SafeValue.SafeString(e.NewValues["LicenseNo"]);
        e.NewValues["LicenseExpiry"] = SafeValue.SafeDate(e.NewValues["LicenseExpiry"],DateTime.Today);
        e.NewValues["BankAccount"] = SafeValue.SafeString(e.NewValues["BankAccount"]);
        e.NewValues["TeamNo"] = SafeValue.SafeString(e.NewValues["TeamNo"]);
        e.NewValues["SalaryBasic"] = SafeValue.SafeDecimal(e.NewValues["SalaryBasic"]);
        e.NewValues["SalaryAllowance"] = SafeValue.SafeDecimal(e.NewValues["SalaryAllowance"]);
        e.NewValues["SubContract_Ind"] = SafeValue.SafeString(e.NewValues["SubContract_Ind"]);
        e.NewValues["SalaryRemark"] = SafeValue.SafeString(e.NewValues["SalaryRemark"]);

        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void grid_Transport_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Isstaff"] = "Y";
        e.NewValues["ServiceLevel"] = "Level1";
        e.NewValues["StatusCode"] = "Active";
        e.NewValues["LicenseExpiry"] = DateTime.Now;
        e.NewValues["TeamNo"] = "A";
    }
    protected void grid_Transport_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox Code = this.grid_Transport.FindEditFormTemplateControl("txt_Code") as ASPxTextBox;
        //e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        check_save(Code.Text);
        e.NewValues["Isstaff"] = SafeValue.SafeString(e.NewValues["Isstaff"], "N");
        e.NewValues["ServiceLevel"] = SafeValue.SafeString(e.NewValues["ServiceLevel"], "Level1");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "Active");
    }

    private void check_save(string Code)
    {
        if (Code.Trim().Length == 0)
        {
            throw new Exception("Code is request");
        }
    }
    protected void grid_Transport_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        btn_search_Click(null, null);
    }

    protected void grid_Transport_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        //{
        //    DateTime dtime = SafeValue.SafeDate(this.grid_Transport.GetRowValues(e.VisibleIndex, "LicenseExpiry"), new DateTime(1990, 1, 1));
        //    DateTime dtime_now = DateTime.Now;
        //    if (dtime_now.AddDays(5).CompareTo(dtime) > 0)
        //    {
        //        e.Row.BackColor = System.Drawing.Color.Red;
        //    }
        //    else
        //    {
        //        if (dtime_now.AddDays(30).CompareTo(dtime) > 0)
        //        {
        //            e.Row.BackColor = System.Drawing.Color.Orange;
        //        }

        //    }
        //}
    }
    protected void grid_Transport_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "LicenseExpiry")
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
        gridExport.WriteXlsToResponse("Driver_" +DateTime.Now.ToString("yyyyMMdd_HHmmsss"), true);
    }
}