using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_Chessis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "type='chessis' ";
        if (txt_search_Code.Text.Trim().Length > 0)
        {
            where += " and Code like '%" + txt_search_Code.Text.Trim() + "%' ";
        }
        if (txt_search_Name.Text.Trim().Length > 0)
        {
            where += " and Name like '%" + txt_search_Name.Text.Trim() + "%' ";
        }
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox Code = this.grid_Transport.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
        check_save(Code.Text);
        e.NewValues["Type"] = "chessis";
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], new DateTime(1990, 1, 1));
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], new DateTime(1990, 1, 1));
        e.NewValues["Date3"] = SafeValue.SafeDate(e.NewValues["Date3"], new DateTime(1990, 1, 1));
        e.NewValues["Date4"] = SafeValue.SafeDate(e.NewValues["Date4"], new DateTime(1990, 1, 1));
        e.NewValues["Date5"] = SafeValue.SafeDate(e.NewValues["Date5"], new DateTime(1990, 1, 1));
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    private void check_save(string Code)
    {
        if (Code.Trim().Length == 0)
        {
            throw new Exception("Code is request");
        }
    }

    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox Code = this.grid_Transport.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
        check_save(Code.Text);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], new DateTime(1990, 1, 1));
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], new DateTime(1990, 1, 1));
        e.NewValues["Date3"] = SafeValue.SafeDate(e.NewValues["Date3"], new DateTime(1990, 1, 1));
        e.NewValues["Date4"] = SafeValue.SafeDate(e.NewValues["Date4"], new DateTime(1990, 1, 1));
        e.NewValues["Date5"] = SafeValue.SafeDate(e.NewValues["Date5"], new DateTime(1990, 1, 1));
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Date1"] = DateTime.Now;
        e.NewValues["Date2"] = DateTime.Now;
        e.NewValues["Date3"] = DateTime.Now;
        e.NewValues["Date4"] = DateTime.Now;
        e.NewValues["Date5"] = DateTime.Now;
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.CtmMastData));
        }
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
        gridExport.WriteXlsToResponse("Trailer_" + DateTime.Now.ToString("yyyyMMdd_HHmmsss"), true);
    }

    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters.Equals("save"))
        {
            gridView_save(sender, e);
        }
        if (e.Parameters.Equals("borrow"))
        {
            ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
            ASPxLabel txt_Id = grd.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
            C2.CtmMastData data = C2.Manager.ORManager.GetObject<C2.CtmMastData>(SafeValue.SafeInt(txt_Id.Text, 0));
            if (data != null)
            {
                C2.CtmMastDataLog.addBorrow(data);
                e.Result = "success";
            }
        }
        if (e.Parameters.Equals("return"))
        {
            ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
            ASPxLabel txt_Id = grd.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
            C2.CtmMastData data = C2.Manager.ORManager.GetObject<C2.CtmMastData>(SafeValue.SafeInt(txt_Id.Text, 0));
            if (data != null)
            {
                C2.CtmMastDataLog.addReturn(data);
                e.Result = "success";
            }
        }
    }
    private void gridView_save(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxLabel txt_Id = grd.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxTextBox txt_Code = grd.FindEditFormTemplateControl("txt_Code") as ASPxTextBox;
        ASPxTextBox txt_Size = grd.FindEditFormTemplateControl("txt_Size") as ASPxTextBox;
        ASPxComboBox cbb_Status = grd.FindEditFormTemplateControl("cbb_Status") as ASPxComboBox;
        ASPxComboBox cbb_self = grd.FindEditFormTemplateControl("cbb_self") as ASPxComboBox;
        ASPxTextBox txt_finance = grd.FindEditFormTemplateControl("txt_finance") as ASPxTextBox;
        ASPxMemo txt_Remark = grd.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
        ASPxDateEdit ASPxDateEdit1 = grd.FindEditFormTemplateControl("ASPxDateEdit1") as ASPxDateEdit;
        ASPxDateEdit ASPxDateEdit2 = grd.FindEditFormTemplateControl("ASPxDateEdit2") as ASPxDateEdit;
        ASPxDateEdit ASPxDateEdit3 = grd.FindEditFormTemplateControl("ASPxDateEdit3") as ASPxDateEdit;
        ASPxDateEdit ASPxDateEdit4 = grd.FindEditFormTemplateControl("ASPxDateEdit4") as ASPxDateEdit;
        ASPxDateEdit ASPxDateEdit5 = grd.FindEditFormTemplateControl("ASPxDateEdit5") as ASPxDateEdit;
        C2.CtmMastData data = C2.Manager.ORManager.GetObject<C2.CtmMastData>(SafeValue.SafeInt(txt_Id.Text, 0));
        bool isNew = false;
        if (data == null)
        {
            data = new C2.CtmMastData();
            data.Type = "chessis";
            isNew = true;
        }
        data.Code = txt_Code.Text;
        data.Name = data.Code;
        data.Remark = txt_Size.Text;
        data.Type1 = (SafeValue.SafeString(cbb_Status.Text) == "InActive" ? "InActive" : "Active");
        data.Note2 = (SafeValue.SafeString(cbb_self.Text) == "N" ? "N" : "Y");
        data.Note3 = txt_finance.Text;
        data.Note1 = txt_Remark.Text;
        data.Date1 = SafeValue.SafeDate(ASPxDateEdit1.Date, DateTime.Now);
        data.Date2 = SafeValue.SafeDate(ASPxDateEdit2.Date, DateTime.Now);
        data.Date3 = SafeValue.SafeDate(ASPxDateEdit3.Date, DateTime.Now);
        data.Date4 = SafeValue.SafeDate(ASPxDateEdit4.Date, DateTime.Now);
        data.Date5 = SafeValue.SafeDate(ASPxDateEdit5.Date, DateTime.Now);
        if (isNew)
        {
            C2.Manager.ORManager.StartTracking(data, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(data);
        }else
        {
            C2.Manager.ORManager.StartTracking(data, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(data);
        }
        e.Result = "success";
    }

    protected void grid_Transport_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxLabel txt_Id = grid_Transport.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        dslog.FilterExpression = "mastId=" + SafeValue.SafeInt(txt_Id.Text, 0);
    }
}