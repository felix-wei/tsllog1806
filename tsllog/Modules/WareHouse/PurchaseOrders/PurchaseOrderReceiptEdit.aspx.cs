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
using DevExpress.Web.ASPxDataView;
using Wilson.ORMapper;

public partial class WareHouse_PurchaseOrders_PurchaseOrderReceiptEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["PORWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["PORWhere"] = "ReceiptNo='" + Request.QueryString["no"].ToString() + "'";
                this.txt_PoNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["PORWhere"] == null)
                {
                    this.grid_WhPoR.AddNewRow();
                }
            }
            else
                this.dsWhPOReceipt.FilterExpression = "1=0";
        }
        if (Session["PORWhere"] != null)
        {
            this.dsWhPOReceipt.FilterExpression = Session["PORWhere"].ToString();
            if (this.grid_WhPoR.GetRow(0) != null)
                this.grid_WhPoR.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txtId = pageControl.FindControl("txt_Id") as ASPxTextBox;
            string id = SafeValue.SafeString(txtId.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhPOReceipt), "Id='" + id + "'");
            WhPOReceipt whPOR = C2.Manager.ORManager.GetObject(query) as WhPOReceipt;
            bool isNew = false;
            string porNo = "";
            if (whPOR == null)
            {
                whPOR = new WhPOReceipt();
                isNew = true;
                porNo = C2Setup.GetNextNo("PurchaseOrdersReceipt");
            }
            ASPxTextBox txt_PartyRefNo = pageControl.FindControl("txt_PartyRefNo") as ASPxTextBox;
            whPOR.PartyRefNo = txt_PartyRefNo.Text;
            ASPxButtonEdit txt_PartyId = pageControl.FindControl("txt_PartyId") as ASPxButtonEdit;
            whPOR.PartyId = txt_PartyId.Text;
            ASPxDateEdit de_ReceiptDate = pageControl.FindControl("de_ReceiptDate") as ASPxDateEdit;
            whPOR.ReceiptDate = de_ReceiptDate.Date;
            ASPxComboBox cbo_ReceiptType = pageControl.FindControl("cbo_ReceiptType") as ASPxComboBox;
            whPOR.ReceiptType =SafeValue.SafeString(cbo_ReceiptType.Value);
            ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
            whPOR.WarehouseId = txt_WarehouseId.Text;
            ASPxButtonEdit txt_SalesmanId = pageControl.FindControl("txt_SalesmanId") as ASPxButtonEdit;
            whPOR.SalesmanId = txt_SalesmanId.Text;
            ASPxButtonEdit txt_Currency = pageControl.FindControl("txt_Currency") as ASPxButtonEdit;
            whPOR.Currency = txt_Currency.Text;
            ASPxSpinEdit spin_ExRate = pageControl.FindControl("spin_ExRate") as ASPxSpinEdit;
            whPOR.ExRate = SafeValue.SafeDecimal(spin_ExRate.Value, 1);
            ASPxMemo txt_Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
            whPOR.Remark = txt_Remark.Text;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                whPOR.CreateBy = userId;
                whPOR.CreateDateTime = DateTime.Now;
                whPOR.ReceiptNo= porNo.ToString();
                whPOR.StatusCode = "USE";
                Manager.ORManager.StartTracking(whPOR, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whPOR);
                C2Setup.SetNextNo("PurchaseOrdersReceipt", porNo);
            }
            else
            {
                whPOR.UpdateBy = userId;
                whPOR.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(whPOR, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whPOR);
            }
            Session["PORWhere"] = "ReceiptNo='" + whPOR.ReceiptNo + "'";
            this.dsWhPOReceipt.FilterExpression = Session["PORWhere"].ToString();
            if (this.grid_WhPoR.GetRow(0) != null)
                this.grid_WhPoR.StartEdit(0);
        }
        catch { }
    }
    protected void grid_WhPoR_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            if (this.dsAttachment.FilterExpression == "1=0")
            {
                ASPxGridView grd = sender as ASPxGridView;
                ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                ASPxTextBox poNo = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
                this.dsAttachment.FilterExpression = "JobType='POR' and RefNo='" + poNo.Text + "'";// 
            }
        }
        else if (s == "Save")
            Save();
    }
    protected void grid_WhPoR_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_WhPoR.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

            ASPxTextBox partyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox whName = pageControl.FindControl("txt_WhName") as ASPxTextBox;
            ASPxTextBox salesManName = pageControl.FindControl("txt_SalesManName") as ASPxTextBox;

            partyName.Text = EzshipHelper.GetPartyName(this.grid_WhPoR.GetRowValues(this.grid_WhPoR.EditingRowVisibleIndex, new string[] { "PartyId" }));
            whName.Text = EzshipHelper.GetWarehouse(this.grid_WhPoR.GetRowValues(this.grid_WhPoR.EditingRowVisibleIndex, new string[] { "WarehouseId" }));
            string oid = SafeValue.SafeString(this.grid_WhPoR.GetRowValues(this.grid_WhPoR.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                ASPxLabel jobStatusStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                string sql = string.Format("select StatusCode from wh_POReceipt  where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grid_WhPoR.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_VoidMaster = this.grid_WhPoR.FindEditFormTemplateControl("btn_VoidMaster") as ASPxButton;
                if (closeInd == "CNL")
                {
                    btn_VoidMaster.Text = "Unvoid";
                    jobStatusStr.Text = "Void";
                }
                if (closeInd == "CLS")
                {
                    btn.Text = "Open Job";
                    jobStatusStr.Text = "Close";
                }
                else
                {
                    jobStatusStr.Text = "USE";
                }
            }
        }
    }
    protected void grid_WhPoR_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PoNo"] = "NEW";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["PoDate"] = DateTime.Now;
        e.NewValues["PromiseDate"] = DateTime.Now;
        e.NewValues["PartyId"] = "";
        e.NewValues["PartyRefNo"] = "";
        string currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["Currency"] = currency;
        e.NewValues["WarehouseId"] = "";
        e.NewValues["SalesmanId"] = "";
        e.NewValues["ExRate"] = "1.000000";
        e.NewValues["DocAmt"] = "0";
        e.NewValues["LocAmt"] = "0";
        e.NewValues["ReceiptType"] = "Receipt";
        e.NewValues["ReceiptDate"] = DateTime.Now;

    }
    protected void grid_WhPoR_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox poNo = pageControl.FindControl("txt_ReceiptNo") as ASPxTextBox;
        if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_POReceipt where ReceiptNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update wh_POReceipt set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where ReceiptNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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

                sql = string.Format("update wh_POReceipt set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where ReceiptNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
        if (s == "VoidMaster")
        {
            #region void master
            //billing
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='POR' and MastRefNo='{0}'", poNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            sql_cnt = string.Format("select p.BalQty from wh_PODet p inner join wh_POReceiptDet det on  p.PoNo=det.PoNo and p.Product=det.Product where det.ReceiptNo='{0}'",poNo.Text);
            cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if(cnt>0){
                e.Result = "BalQty";
                return;
            }
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_POReceipt where ReceiptNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update wh_POReceipt set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where ReceiptNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                sql = string.Format("update wh_POReceipt set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where ReceiptNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
    }
    protected void grid_WhPoR_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhPOReceipt));
        }
    }

    #region POR det
    protected void grid_PORDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhPOReceiptDet));
    }
    protected void grid_PORDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_PoNo = pageControl.FindControl("txt_ReceiptNo") as ASPxTextBox;
        this.dsWhPOReceiptDet.FilterExpression = " ReceiptNo='" + txt_PoNo.Text + "'";
    }
    protected void grid_PORDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxButtonEdit docCurr = pageControl.FindControl("txt_Currency") as ASPxButtonEdit;
        ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
        e.NewValues["WareHouseId"] = SafeValue.SafeString(txt_WarehouseId.Text);
        e.NewValues["Currency"] = docCurr.Text;
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Gst"] = 0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["LocCode"] = " ";
        e.NewValues["LinePNo"] = 0;

    }
    protected void grid_PORDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_PoNo = pageControl.FindControl("txt_ReceiptNo") as ASPxTextBox;
        ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
        e.NewValues["WareHouseId"] = SafeValue.SafeString(txt_WarehouseId.Text);
        e.NewValues["LinePNo"] = SafeValue.SafeInt(txt_Id.Text, 0);
        e.NewValues["ReceiptNo"] = txt_PoNo.Text;
        string product=SafeValue.SafeString(e.NewValues["Product"], "");
        if (product.Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1.0;

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_PORDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
        e.NewValues["WareHouseId"] = SafeValue.SafeString(txt_WarehouseId.Text);
    }
    protected void grid_PORDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int lineNo = SafeValue.SafeInt(e.Values["Id"], 0);
        string poNo = SafeValue.SafeString(e.Values["PoNo"]);
        string product = SafeValue.SafeString(e.Values["Product"]);
        UpdateMaster(lineNo, 0, poNo, product);
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_PORDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        int lineNo = SafeValue.SafeInt(e.NewValues["Id"], 0);
        int qty = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        string poNo = SafeValue.SafeString(e.NewValues["PoNo"]);
        string product = SafeValue.SafeString(e.NewValues["Product"]);
        UpdateMaster(lineNo, qty, poNo, product);
    }
    protected void grid_PORDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        int lineNo = SafeValue.SafeInt(e.NewValues["Id"], 0);
        int qty = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        string poNo = SafeValue.SafeString(e.NewValues["PoNo"]);
        string product = SafeValue.SafeString(e.NewValues["Product"]);
        UpdateMaster(lineNo, qty, poNo, product);
    }
    protected void grid_PORDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
    }
    private void UpdateMaster(int lineNo,int qty,string poNo,string product)
    {
        decimal docAmt = 0;
        string sql = string.Format("select DocAmt from wh_POReceiptDet where Id={0}", lineNo);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            docAmt += SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
        }

        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM wh_POReceiptDet AS det INNER JOIN  wh_POReceipt AS mast ON det.ReceiptNo = mast.ReceiptNo
WHERE (det.Id = {0})", lineNo)), 0);

        sql = string.Format("Update wh_POReceiptDet set DocAmt='{0}',LocAmt=DocAmt*ExRate where Id={1}", docAmt, lineNo);
        C2.Manager.ORManager.ExecuteCommand(sql);

        sql = string.Format("update wh_POReceiptDet set LineLocAmt=locAmt* (select ExRate from wh_POReceipt where ReceiptNo=wh_POReceiptDet.ReceiptNo) where Id='{0}'", lineNo);
        C2.Manager.ORManager.ExecuteCommand(sql);

        sql = string.Format("update wh_PODet set BalQty=Qty-{0} where PoNo='{1}' and Product='{2}'",qty,poNo,product);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion

    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select ReceiptNo from wh_POReceipt where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsInvoice.FilterExpression = "MastType='POR' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select ReceiptNo from wh_POReceipt where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsVoucher.FilterExpression = "MastType='POR' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    #endregion
    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPoR.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtRefNo = pageControl.FindControl("txt_ReceiptNo") as ASPxTextBox;
        this.dsAttachment.FilterExpression = "RefNo='" + SafeValue.SafeString(txtRefNo.Text, "") + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }



    #endregion

    protected void gridGstType_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] keyValues = new object[grid.VisibleRowCount];
        object[] code = new object[grid.VisibleRowCount];
        object[] gstValue = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            keyValues[i] = grid.GetRowValues(i, "SequenceId");
            code[i] = grid.GetRowValues(i, "Code");
            gstValue[i] = grid.GetRowValues(i, "GstValue");
        }
        e.Properties["cpId"] = keyValues;
        e.Properties["cpCode"] = code;
        e.Properties["cpGstValue"] = gstValue;
    }
}