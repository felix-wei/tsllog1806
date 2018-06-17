using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTabControl;

public partial class Z_job_list : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string where2 = "";
        if (txt_SchJobNo.Text.Trim() != "")
            where = " RefNo = '" + txt_SchJobNo.Text.Trim() + "'";
        if (this.date_SchJobDate.Text.Trim().Length == 10)
            where2 = AndWhere(where2, " JobDate='" + this.date_SchJobDate.Text.Substring(6, 4) + "/" + this.date_SchJobDate.Text.Substring(3, 2) + "/" + this.date_SchJobDate.Text.Substring(0, 2) + "'");
        if (this.cmb_SchCustomer.Text.Trim() != "")
        {
            where2 = AndWhere(where2, "CustomerId='" + SafeValue.SafeString(this.cmb_SchCustomer.Value).Trim() + "'");
        }
        if (cbx_SchjobCate.Text.Length > 0)
        {
            where2 = AndWhere(where2, " Note2='" + this.cbx_SchjobCate.Text.Trim() + "'");
        }
        if (cmb_SchStatus.Text.Length > 0)
        {
            where2 = AndWhere(where2, "StatusCode='" + this.cmb_SchStatus.Text.Trim() + "'");
        }
        if (where2.Length > 0)
        {
            where = OrWhere(where, where2);
        }
        if (where.Length > 0)
        {
            ds1.FilterExpression = where;
        }
        else
            ds1.FilterExpression = "1=0";
    }
    private string AndWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    private string OrWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " or (" + s + ")";
        else
            where = s;
        return where;
    }

    protected void grid1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.JobOrder));
    }
    protected void grid1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            C2.JobOrder order = new C2.JobOrder();
            ASPxComboBox txt_JobType = ASPxPopupControl1.FindControl("txt_JobType") as ASPxComboBox;
            ASPxComboBox cbx_jobCate = ASPxPopupControl1.FindControl("cbx_jobCate") as ASPxComboBox;
            ASPxComboBox txtCustomerId = ASPxPopupControl1.FindControl("cmb_Customer") as ASPxComboBox;
            ASPxComboBox txtShipperId = ASPxPopupControl1.FindControl("cmb_Carrier") as ASPxComboBox;
            //ASPxTextBox txtShipperName = ASPxPopupControl1.FindControl("txtShipperName") as ASPxTextBox;
            ASPxMemo txt_Remark = ASPxPopupControl1.FindControl("txt_Remark") as ASPxMemo;
            ASPxSpinEdit spin_Weight = ASPxPopupControl1.FindControl("spin_Weight") as ASPxSpinEdit;
            ASPxSpinEdit spin_Volume = ASPxPopupControl1.FindControl("spin_Volume") as ASPxSpinEdit;
            ASPxSpinEdit spin_Pkgs = ASPxPopupControl1.FindControl("spin_Pkgs") as ASPxSpinEdit;
            ASPxComboBox txt_pkgType = ASPxPopupControl1.FindControl("cmb_PackType") as ASPxComboBox;
            string refType = "SEF";
            string runType = "EXPORTREF";//SEF/SEL/SEC/ SCF/SCL/SCC/ SAE/SAC/SLT
            //if (refType == "SAE")
            //    runType = "AirExport";
            //else if (refType == "SAC")
            //    runType = "AirCrossTrade";
            //else if (refType == "SCF" || refType == "SCL" || refType == "SCC")
            //    runType = "SeaCrossTrade";
            //else if (refType == "SLT")
            //    runType = "LocalTpt";
            order.JobNo = C2Setup.GetNextNo(refType, runType, DateTime.Today);
            order.RefNo = order.JobNo;
            order.Note1 = txt_JobType.Text;
            order.Note2 = cbx_jobCate.Text;
            order.CustomerId = SafeValue.SafeString(txtCustomerId.Value);
            order.ShipperId = SafeValue.SafeString(txtShipperId.Value);
            order.ShipperName = txtShipperId.Text;
            order.Remark = txt_Remark.Text;
            order.JobDate = DateTime.Today;
            C2.Manager.ORManager.StartTracking(order, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(order);
            C2Setup.SetNextNo(refType, runType, order.JobNo, DateTime.Today);

            C2.JobCargo cargo = new C2.JobCargo();
            cargo.RefNo = order.JobNo;
            cargo.Weight = SafeValue.SafeDecimal(spin_Weight.Value, 0);
            cargo.Volume = SafeValue.SafeDecimal(spin_Volume.Value, 0);
            cargo.Qty = SafeValue.SafeInt(spin_Pkgs.Value, 0);
            cargo.PackageType = txt_pkgType.Text;
            cargo.Remark = "";
            C2.Manager.ORManager.StartTracking(cargo, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(cargo);

            EzshipLog.Log(order.JobNo, order.JobNo, "Direct", "Create");

            ASPxPopupControl1.ShowOnPageLoad = false;
            cbx_jobCate.Text = "";
            txtCustomerId.Text = "";
            txtShipperId.Text = "";
            txt_Remark.Text = "";
            spin_Weight.Text = "";
            spin_Volume.Text = "";
            spin_Pkgs.Text = "";
            txt_pkgType.Text = "";
            e.Result = order.SequenceId + "|" + order.JobNo;
            //ClientScriptManager cs = Page.ClientScript;
            //cs.RegisterStartupScript(this.GetType(), "", "<script type=\"text/javascript\">txt_Remark.SetText();cmb_PackType.SetText();spin_Pkgs.SetText();spin_Volume.SetText();spin_Weight.SetText();cmb_Carrier.SetText();cmb_Customer.SetText();cbx_jobCate.SetText();ASPxPopupClientControl.Hide();parent.navTab.openTab(\"" + order.JobNo + "\",\"/Z/job_edit.aspx?id=" + order.SequenceId + "\"" + ",{title:\"" + order.JobNo + "\", fresh:false, external:true});</script>");
        }
    }
}
