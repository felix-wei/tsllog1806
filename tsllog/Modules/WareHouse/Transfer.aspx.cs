using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

public partial class WareHouse_Transfer : System.Web.UI.Page
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
        {
            Session["NameWhere"] = null;
            this.dsTransfer.FilterExpression = "1=0";
        }
        if (Session["NameWhere"] != null)
        {
            this.dsTransfer.FilterExpression = Session["NameWhere"].ToString();
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhTransfer));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["RequestPerson"] = " ";
        e.NewValues["RequestDate"] = DateTime.Now;
        e.NewValues["TransferDate"] = DateTime.Now;
        e.NewValues["Pic"] = " ";
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["ConfirmPerson"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["ConfirmDate"] = DateTime.Now;
    }
    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            string oid = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                string sql = string.Format("select StatusCode from wh_Transfer where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grid.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_Void = this.grid.FindEditFormTemplateControl("btn_Void") as ASPxButton;
                if (closeInd == "CLS")
                {
                    btn.Text = "Open Job";
                }
                if (closeInd == "CNL")
                {
                    btn_Void.Text = "Unvoid";
                }
            }

        }
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        AddOrUpdate();
        e.Cancel = true;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        AddOrUpdate();
        e.Cancel = true;
    }
    protected void AddOrUpdate()
    {

        ASPxTextBox txt_pId = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        string pId = SafeValue.SafeString(txt_pId.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTransfer), "Id='" + pId + "'");
        WhTransfer transfer = C2.Manager.ORManager.GetObject(query) as WhTransfer;
        bool action = false;
        string transferNo = "";
        if (transfer == null)
        {
            action = true;
            transfer = new WhTransfer();
            transferNo = C2Setup.GetNextNo("Transfer");
        }

        ASPxTextBox txt_TransferNo =this.grid.FindEditFormTemplateControl("txt_TransferNo") as ASPxTextBox;
        transfer.TransferNo = txt_TransferNo.Text.Trim();
        ASPxTextBox txt_RequestPerson =this.grid.FindEditFormTemplateControl("txt_RequestPerson") as ASPxTextBox;
        transfer.RequestPerson = txt_RequestPerson.Text;
        ASPxDateEdit de_RequestDate =this.grid.FindEditFormTemplateControl("de_RequestDate") as ASPxDateEdit;
        transfer.RequestDate = de_RequestDate.Date;
        ASPxDateEdit de_TransferDate =this.grid.FindEditFormTemplateControl("de_TransferDate") as ASPxDateEdit;
        transfer.TransferDate = de_TransferDate.Date;
        //ASPxComboBox cbo_StatusCode =this.grid.FindEditFormTemplateControl("cbo_StatusCode") as ASPxComboBox;
        //transfer.StatusCode = cbo_StatusCode.Text;
        ASPxTextBox txt_Pic =this.grid.FindEditFormTemplateControl("txt_Pic") as ASPxTextBox;
        transfer.Pic = txt_Pic.Text;
        ASPxTextBox txt_ConfirmPerson =this.grid.FindEditFormTemplateControl("txt_ConfirmPerson") as ASPxTextBox;
        transfer.ConfirmPerson = txt_ConfirmPerson.Text;
        ASPxDateEdit de_ConfirmDate =this.grid.FindEditFormTemplateControl("de_ConfirmDate") as ASPxDateEdit;
        transfer.ConfirmDate = de_ConfirmDate.Date;
        ASPxTextBox txt_UomWhole =this.grid.FindEditFormTemplateControl("txt_UomWhole") as ASPxTextBox;

        ASPxButtonEdit txt_PartyId =this.grid.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
        transfer.PartyId = txt_PartyId.Text;
        ASPxTextBox txt_PartyName =this.grid.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
        transfer.PartyName = txt_PartyName.Text;

        if (action)
        {
            transfer.TransferNo = transferNo;
            transfer.CreateBy = HttpContext.Current.User.Identity.Name;
            transfer.CreateDateTime = DateTime.Now;
            C2Setup.SetNextNo("Transfer", transferNo);
            Manager.ORManager.StartTracking(transfer, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(transfer);
            Session["NameWhere"] = "TransferNo='" + transferNo + "'";
            this.dsTransfer.FilterExpression = Session["NameWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
        else
        {
            transfer.UpdateBy = HttpContext.Current.User.Identity.Name;
            transfer.UpdateDateTime = DateTime.Now;
            Manager.ORManager.StartTracking(transfer, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(transfer);
        }
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxTextBox txt_TransferNo = this.grid.FindEditFormTemplateControl("txt_TransferNo") as ASPxTextBox;
        ASPxButtonEdit txt_PartyId = this.grid.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
        ASPxTextBox txt_PartyName = this.grid.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
        string sql = "";
        string refNo = SafeValue.SafeString(txt_TransferNo.Text);
        if (s == "CloseJob")
        {
            #region close job

            sql = "select StatusCode from wh_Transfer where TransferNo='" + refNo + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update wh_Transfer set StatusCode='USE' where TransferNo='{0}'", refNo);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";
                    string doInId = "";
                    string doOutId = "";
                    sql = string.Format(@"select DoInId,DoOutId from wh_TransferDet where TransferNo='{0}'", refNo);
                    DataTable tab = ConnectSql.GetTab(sql);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        doInId = SafeValue.SafeString(tab.Rows[i]["DoInId"]);
                        doOutId = SafeValue.SafeString(tab.Rows[i]["DoOutId"]);
                        sql = string.Format("delete from Wh_DoDet2 where Id={0}", doInId);
                        C2.Manager.ORManager.ExecuteCommand(sql);
                        sql = string.Format("delete from Wh_DoDet2 where Id={0}", doOutId);
                        C2.Manager.ORManager.ExecuteCommand(sql);
                    }
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format("update wh_Transfer set StatusCode='CLS' where TransferNo='{0}'", refNo);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";

                    sql = string.Format(@"select * from wh_TransferDet where TransferNo='{0}'", refNo);
                    DataTable tab = ConnectSql.GetTab(sql);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        int id = SafeValue.SafeInt(tab.Rows[i]["Id"], 0);
                        string doInId = "";
                        string doOutId = "";
                        string sku = SafeValue.SafeString(tab.Rows[i]["Product"]);
                        string des = SafeValue.SafeString(tab.Rows[i]["Des1"]);
                        string fromLoc = SafeValue.SafeString(tab.Rows[i]["FromLocId"]);
                        string toLoc = SafeValue.SafeString(tab.Rows[i]["ToLocId"]);
                        string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
                        int qty1 = SafeValue.SafeInt(tab.Rows[i]["Qty1"], 0);
                        int qty2 = SafeValue.SafeInt(tab.Rows[i]["Qty2"], 0); ;
                        int qty3 = SafeValue.SafeInt(tab.Rows[i]["Qty3"], 0); ;
                        string uom1 = SafeValue.SafeString(tab.Rows[i]["Uom1"]);
                        string uom2 = SafeValue.SafeString(tab.Rows[i]["Uom2"]);
                        string uom3 = SafeValue.SafeString(tab.Rows[i]["Uom3"]);
                        string uom4 = SafeValue.SafeString(tab.Rows[i]["Uom4"]);
                        int qtyPackWhole = SafeValue.SafeInt(tab.Rows[i]["QtyPackWhole"], 0);
                        int qtyWholeLoose = SafeValue.SafeInt(tab.Rows[i]["QtyWholeLoose"], 0);
                        int qtyLooseBase = SafeValue.SafeInt(tab.Rows[i]["QtyLooseBase"], 0);
                        string att1 = SafeValue.SafeString(tab.Rows[i]["Att1"]);
                        string att2 = SafeValue.SafeString(tab.Rows[i]["Att2"]);
                        string att3 = SafeValue.SafeString(tab.Rows[i]["Att3"]);
                        string att4 = SafeValue.SafeString(tab.Rows[i]["Att4"]);
                        string att5 = SafeValue.SafeString(tab.Rows[i]["Att5"]);
                        string att6 = SafeValue.SafeString(tab.Rows[i]["Att6"]);
                        string packing = SafeValue.SafeString(tab.Rows[i]["Packing"]);
                        string partyId = SafeValue.SafeString(txt_PartyId.Text);
                        string partyName = SafeValue.SafeString(txt_PartyName.Text);
                        ASPxDateEdit de_TransferDate = this.grid.FindEditFormTemplateControl("de_TransferDate") as ASPxDateEdit;
                        DateTime doDate = de_TransferDate.Date;
                        //From Location-----To Location,Do Out 
                        doOutId = AddDoDet2("", refNo, "OUT", fromLoc, sku, lotNo, des, price, qty1, qty2, qty3, uom1, uom2, uom3, uom4, qtyPackWhole, qtyWholeLoose, qtyLooseBase, att1, att2, att3, att4, att5, att6, packing, partyId, partyName, doDate);

                        //From Location-----To Location,Do In
                        doInId = AddDoDet2("", refNo, "IN", toLoc, sku, lotNo, des, price, qty1, qty2, qty3, uom1, uom2, uom3, uom4, qtyPackWhole, qtyWholeLoose, qtyLooseBase, att1, att2, att3, att4, att5, att6, packing, partyId, partyName, doDate);

                        sql = string.Format(@"update wh_TransferDet set DoInId={1},DoOutId={2} where Id={0}", id, doInId, doOutId);
                        C2.Manager.ORManager.ExecuteCommand(sql);
                    }
                }
                else
                    e.Result = "Fail";

            }
            #endregion
        }
        if (s == "Void")
        {
            #region void master
            sql = "select StatusCode from wh_Transfer where TransferNo='" + refNo + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update wh_Transfer set StatusCode='USE' where TransferNo='{0}'", refNo);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format("update wh_Transfer set StatusCode='CNL' where TransferNo='{0}'", refNo);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";
            }
            #endregion
        }
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            AddOrUpdate();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " TransferDate >= '" + dateFrom + "' and TransferDate < '" + dateTo + "'");
        }
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format("TransferNo LIKE '{0}%'", name));
        }
        this.dsTransfer.FilterExpression = where;
        Session["NameWhere"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    #region So det
    protected void grid_det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhTransferDet));
    }
    protected void grid_det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox txt_TransferNo = this.grid.FindEditFormTemplateControl("txt_TransferNo") as ASPxTextBox;
        this.dsTransferDet.FilterExpression = " TransferNo='" + SafeValue.SafeString(txt_TransferNo.Text) + "'";
    }
    protected void grid_det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_id =this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_TransferNo =this.grid.FindEditFormTemplateControl("txt_TransferNo") as ASPxTextBox;
        e.NewValues["LineTNo"] =SafeValue.SafeInt(txt_id.Text,0);
        e.NewValues["TransferNo"] = txt_TransferNo.Text;
        e.NewValues["Qty"] = 0;
        e.NewValues["Price"] = 0.0;
        e.NewValues["Unit"] = 0;
    }
    protected void grid_det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        ASPxTextBox txt_TNo = this.grid.FindEditFormTemplateControl("txt_TransferNo") as ASPxTextBox;
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"]);
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["TransferNo"] = txt_TNo.Text;
        e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;

    }
    protected void grid_det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"]);
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    
       
    }
    protected void grid_det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }


    #endregion
    #region to wh_dodet2
    private string AddDoDet2(string id, string doNo, string doType, string location, string sku, string lotNo, string des,decimal price, int qty1, int qty2, int qty3, string uom1, string uom2, string uom3, string uom4,
int qtyPackWhole, int qtyWholeLoose, int qtyLooseBase, string att1, string att2, string att3, string att4, string att5, string att6, string packing,
string partyId, string partyName, DateTime doDate)
    {
        WhDoDet2 det = new WhDoDet2();

        det.DoNo = "";
        det.DoType = doType;
        det.Product = sku;
        det.Location = location;
        det.DoDate = doDate;// de_TransferDate.Date;
        det.LotNo = lotNo;
        det.Des1 = des;
        det.Price = price;
        //det.PkgType = "";
        det.Qty1 = qty1;
        det.Qty2 = qty2;
        det.Qty3 = qty3;
        det.Uom1 = uom1;
        det.Uom2 = uom2;
        det.Uom3 = uom3;
        det.Uom4 = uom4;
        det.QtyPackWhole = qtyPackWhole;
        det.QtyWholeLoose = qtyWholeLoose;
        det.QtyLooseBase = qtyLooseBase;
        det.IsSch = false;
        det.LastSchQty1 = qty1;
        det.LastSchQty2 = qty2;
        det.LastSchQty3 = qty3;
        det.PreQty1 = qty1;
        det.PreQty2 = qty2;
        det.PreQty3 = qty3;
        det.Att1 = att1;
        det.Att2 = att2;
        det.Att3 = att3;
        det.Att4 = att4;
        det.Att5 = att5;
        det.Att6 = att6;
        det.Packing = packing;
        det.PartyId = partyId;
        det.PartyName = partyName;
        det.CreateBy = EzshipHelper.GetUserName();
        det.CreateDateTime = DateTime.Now;
        det.UpdateBy = EzshipHelper.GetUserName();
        det.UpdateDateTime = DateTime.Now;
        Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(det);
        return SafeValue.SafeString(det.Id);

    } 
    #endregion
}