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

public partial class WareHouse_PurchaseOrders_PurchaseOrderEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["POWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["POWhere"] = "PoNo='" + Request.QueryString["no"].ToString() + "'";
                this.txt_PoNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["POWhere"] == null)
                {
                    this.grid_WhPo.AddNewRow();
                }
            }
            else
                this.dsWhPo.FilterExpression = "1=0";
        }
        if (Session["POWhere"] != null)
        {
            this.dsWhPo.FilterExpression = Session["POWhere"].ToString();
            if (this.grid_WhPo.GetRow(0) != null)
                this.grid_WhPo.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txtId = pageControl.FindControl("txt_Id") as ASPxTextBox;
            string id = SafeValue.SafeString(txtId.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhPo), "Id='" + id + "'");
            WhPo whPo = C2.Manager.ORManager.GetObject(query) as WhPo;
            bool isNew = false;
            string poNo = "";
            if (whPo == null)
            {
                whPo = new WhPo();
                isNew = true;
                poNo = C2Setup.GetNextNo("PurchaseOrders");
            }
            ASPxTextBox txt_PartyRefNo = pageControl.FindControl("txt_PartyRefNo") as ASPxTextBox;
            whPo.PartyRefNo = txt_PartyRefNo.Text;
            ASPxButtonEdit txt_PartyId = pageControl.FindControl("txt_PartyId") as ASPxButtonEdit;
            whPo.PartyId = txt_PartyId.Text;
            ASPxDateEdit txt_PoDate = pageControl.FindControl("txt_PoDate") as ASPxDateEdit;
            whPo.PoDate = txt_PoDate.Date;
            ASPxDateEdit txt_PromiseDate = pageControl.FindControl("txt_PromiseDate") as ASPxDateEdit;
            whPo.PromiseDate = txt_PromiseDate.Date;
            ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
            whPo.WarehouseId = txt_WarehouseId.Text;
            ASPxButtonEdit txt_SalesmanId = pageControl.FindControl("txt_SalesmanId") as ASPxButtonEdit;
            whPo.SalesmanId = txt_SalesmanId.Text;
            ASPxButtonEdit txt_Currency = pageControl.FindControl("txt_Currency") as ASPxButtonEdit;
            whPo.Currency = txt_Currency.Text;
            ASPxSpinEdit spin_ExRate = pageControl.FindControl("spin_ExRate") as ASPxSpinEdit;
            whPo.ExRate = SafeValue.SafeDecimal(spin_ExRate.Value, 1);
            ASPxMemo txt_Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
            whPo.Remark = txt_Remark.Text;
            ASPxSpinEdit txt_DocAmt = pageControl.FindControl("spin_DocAmt") as ASPxSpinEdit;
            whPo.DocAmt = SafeValue.SafeDecimal(txt_DocAmt.Value, 1);
            ASPxSpinEdit txt_LocAmt = pageControl.FindControl("spin_LocAmt") as ASPxSpinEdit;
            whPo.LocAmt = SafeValue.SafeDecimal(txt_LocAmt.Value,1)* whPo.ExRate;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                whPo.CreateBy = userId;
                whPo.CreateDateTime = DateTime.Now;
                whPo.PoNo = poNo.ToString();
                whPo.StatusCode = "USE";
                Manager.ORManager.StartTracking(whPo, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whPo);
                C2Setup.SetNextNo("PurchaseOrders", poNo);
            }
            else {
                whPo.UpdateBy = userId;
                whPo.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(whPo, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whPo);
            }
            Session["POWhere"] = "PoNo='" + whPo.PoNo + "'";
            this.dsWhPo.FilterExpression = Session["POWhere"].ToString();
            if (this.grid_WhPo.GetRow(0) != null)
                this.grid_WhPo.StartEdit(0);
        }
        catch { }
    }
    protected void grid_WhPo_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            if (this.dsAttachment.FilterExpression == "1=0")
            {
                ASPxGridView grd = sender as ASPxGridView;
                ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                ASPxTextBox poNo = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
                this.dsAttachment.FilterExpression = "JobType='PO' and RefNo='" + poNo.Text + "'";// 
            }
        }
        else if (s == "Save")
            Save();
    }
    protected void grid_WhPo_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        
        if (this.grid_WhPo.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

            ASPxTextBox partyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox whName = pageControl.FindControl("txt_WhName") as ASPxTextBox;

            partyName.Text = EzshipHelper.GetPartyName(this.grid_WhPo.GetRowValues(this.grid_WhPo.EditingRowVisibleIndex, new string[] { "PartyId" }));
            whName.Text = EzshipHelper.GetWarehouse(this.grid_WhPo.GetRowValues(this.grid_WhPo.EditingRowVisibleIndex, new string[] { "WarehouseId" }));
            string oid = SafeValue.SafeString(this.grid_WhPo.GetRowValues(this.grid_WhPo.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                string sql = string.Format("select StatusCode from wh_PO  where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grid_WhPo.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_Void = this.grid_WhPo.FindEditFormTemplateControl("btn_Void") as ASPxButton;

                if (closeInd == "CLS")
                {
                    btn.Text = "Open Job";
                }
                else { btn.Text = "Close"; }
                 if (closeInd == "CNL")
                {
                    btn_Void.Text = "Unvoid";
                }
                else{btn_Void.Text = "Void";}
            }
        }
    }
    protected void grid_WhPo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PoNo"] = "NEW";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["PoDate"] = DateTime.Now;
        e.NewValues["PromiseDate"] = DateTime.Now;
        e.NewValues["PartyId"] = "";
        e.NewValues["PartyRefNo"] = "";
        e.NewValues["Currency"] = "" ;
        e.NewValues["WarehouseId"] = "";
        e.NewValues["SalesmanId"] = "";
        e.NewValues["ExRate"] = "1.0000";
        e.NewValues["DocAmt"] = "0";
        e.NewValues["LocAmt"] = "0";
        string currency=System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["Currency"] = currency;
        e.NewValues["StatusCode"] = "USE";
    }
    protected void grid_WhPo_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox poNo = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
        string sql = "select Count(*) from wh_PODet where PoNo='" + SafeValue.SafeString(poNo.Text) + "' and (StatusCode='Draft' or StatusCode='Waiting')";
        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (cnt == 0)
        {
            if (s == "CloseJob")
            {
                #region close job
                ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                sql = "select StatusCode from wh_PO where PoNo='" + poNo.Text + "'";
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                if (closeInd == "CLS")
                {
                    sql = string.Format("update wh_PO set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where PoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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

                    sql = string.Format("update wh_PO set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where PoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                        e.Result = "Success";
                    else
                        e.Result = "Fail";

                }
                #endregion
            }
        }
        else
        {
            e.Result = "NotClose";
        }
        if (s == "Void")
        {
            #region void master
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='WH' and MastRefNo='{0}'", poNo.Text);
            cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            sql = "select StatusCode from wh_PO where PoNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update wh_PO set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where PoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                sql = string.Format("update wh_PO set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where PoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
        if (s == "Invoice")
        {
            #region Ap Invoice
            sql = string.Format(@"select count(*) from XAApPayable where MastRefNo='{0}'", poNo.Text);
            int docId = 0;
            cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
            if (cnt == 0)
            {
                ASPxTextBox partyTo = pageControl.FindControl("txt_PartyRefNo") as ASPxTextBox;
                ASPxDateEdit poDate = pageControl.FindControl("txt_PoDate") as ASPxDateEdit;
                string invN = "";
                C2.XAApPayable inv = null;
                bool isNew = false;
                if (invN.Length < 1)// first insert invoice
                {
                    isNew = true;
                    inv = new XAApPayable();
                    inv.SupplierBillDate = SafeValue.SafeDate(poDate.Date, DateTime.Today);
                    inv.DocDate = inv.SupplierBillDate;

                    inv.DocType = "PL";
                    invN = C2Setup.GetNextNo(inv.DocType, "AP-PAYABLE", inv.DocDate);
                    inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
                    inv.DocNo = invN.ToString();
                    inv.Term = "CASH";
                    inv.Description = "";
                    inv.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                    inv.ExRate = 1;
                    inv.AcCode = EzshipHelper.GetAccApCode(inv.PartyTo, inv.CurrencyId);
                    inv.AcSource = "CR";

                    inv.ExportInd = "N";
                    inv.UserId = HttpContext.Current.User.Identity.Name;
                    inv.EntryDate = DateTime.Now;
                    inv.CancelDate = new DateTime(1900, 1, 1);
                    inv.CancelInd = "N";

                    inv.ChqNo = "";
                    inv.ChqDate = new DateTime(1900, 1, 1);

                    inv.MastRefNo = poNo.Text;
                    inv.JobRefNo = "0";
                    inv.MastType = "WH";
                    inv.Eta = DateTime.Now;
                    string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

                    inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
                    inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
                    try
                    {
                        C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(inv);
                        C2Setup.SetNextNo(inv.DocType, "AP-PAYABLE", invN, inv.DocDate);
                        docId = inv.SequenceId;
                        e.Result = "Success";
                    }
                    catch
                    {
                    }

                }
                sql = "select * from wh_PODet where PoNo='" + poNo.Text + "'";
                DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                SavePayableDet(dt,inv.DocNo,docId);
            }
            else
            {
                DataTable tab = C2.Manager.ORManager.GetDataSet(string.Format(@"select Id from wh_PODet where PoNo='{0}'", poNo.Text)).Tables[0];
                int id = 0;
                C2.XAApPayableDet inv = null;
                if (tab.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        id = SafeValue.SafeInt(tab.Rows[i]["Id"],0);
                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(XAApPayableDet), "POlineId='" + id + "'");
                        inv = C2.Manager.ORManager.GetObject(query) as XAApPayableDet;
                        if(inv==null){
                            SavePayableDet(tab,inv.DocNo,docId);
                        }
                    }
                }
                e.Result = "Success";
            }
            #endregion
        }
    }
    private void SavePayableDet(DataTable dt,string doNo,int docId)
    {
        if (dt.Rows.Count > 0)
        {
            XAApPayableDet det = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                det = new XAApPayableDet();
                det.DocNo=doNo;
                det.DocType = "PL";
                det.POlineId = SafeValue.SafeInt(dt.Rows[i]["Id"], 0);
                det.AcSource = "DB";
                det.AcCode = "";
                det.MastType = "WH";
                det.MastRefNo =SafeValue.SafeString(dt.Rows[i]["PoNo"]);
                det.DocId = docId;
                det.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                det.Qty = SafeValue.SafeInt(dt.Rows[i]["Qty"], 0);
                det.Price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
                det.SplitType = "SET";
                det.Gst = SafeValue.SafeInt(dt.Rows[i]["Gst"], 0);
                det.GstType = dt.Rows[i]["GstType"].ToString();
                det.DocAmt = SafeValue.SafeDecimal(dt.Rows[i]["DocAmt"]);
                det.LocAmt = SafeValue.SafeDecimal(dt.Rows[i]["LocAmt"]);
                det.GstAmt = SafeValue.SafeDecimal(dt.Rows[i]["GstAmt"]);
                det.ExRate = SafeValue.SafeDecimal(dt.Rows[i]["ExRate"]);
                try
                {
                    C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(det);
                }
                catch
                {
                }
            }
        }
    }
    protected void grid_WhPo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhPo));
        }
    }

    #region Po det
    protected void grid_PoDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhPODet));
    }
    protected void grid_PoDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_PoNo = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
        this.dsPoDet.FilterExpression = " PoNo='" + txt_PoNo.Text + "'";
    }
    protected void grid_PoDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxButtonEdit docCurr = pageControl.FindControl("txt_Currency") as ASPxButtonEdit;
        e.NewValues["Currency"] = docCurr.Text;
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Gst"] = 0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["BalQty"] = 0;
        e.NewValues["StatusCode"] = "Draft";

    }
    protected void grid_PoDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_PoNo = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
        e.NewValues["PoNo"] = txt_PoNo.Text;
        e.NewValues["LinePNo"] =SafeValue.SafeInt(txt_Id.Text,0);
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
            e.NewValues["ExRate"] = 1.0;
        if (e.NewValues["GstType"].Equals("S"))
            e.NewValues["Gst"] = new decimal(0.07);
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["CreateBy"]=HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["BalQty"] = e.NewValues["Qty"];
    }
    
    protected void grid_PoDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        ASPxComboBox cmb_Status = grid.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
        e.NewValues["StatusCode"] = SafeValue.SafeString(cmb_Status.SelectedItem.Value);
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
        if (e.NewValues["GstType"].Equals("S"))
            e.NewValues["Gst"] = new decimal(0.07);
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeInt(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        int oldbalQty=SafeValue.SafeInt(e.OldValues["BalQty"],0);
        int oldQty=SafeValue.SafeInt(e.OldValues["Qty"],0);
        if (oldbalQty < oldQty)
        {
            e.NewValues["BalQty"] = oldbalQty + (SafeValue.SafeInt(e.NewValues["Qty"], 0) - oldQty);
        }
        else
        {
            e.NewValues["BalQty"] = e.NewValues["Qty"];
        }
    }
    protected void grid_PoDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_PoDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox oidCtr = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_PoDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox oidCtr = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_PoDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox oidCtr = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
        string sql = "delete from wh_POReceiptDet where PoNo='" +SafeValue.SafeString(oidCtr .Text)+ "'";
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    private void UpdateMaster(string poNo)
    {
        decimal docAmt = 0;
        string sql = string.Format("select DocAmt from wh_PODet where PoNo='{0}'", poNo);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            docAmt += SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
        }

        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM wh_PODet AS det INNER JOIN  wh_PO AS mast ON det.PoNo = mast.PoNo
WHERE (det.PoNo = '{0}')", poNo)), 0);

        sql = string.Format("Update wh_PO set DocAmt='{0}',LocAmt=DocAmt*ExRate where PoNo='{1}'", docAmt, poNo);
        C2.Manager.ORManager.ExecuteCommand(sql);

        
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
        ASPxPageControl pageControl = this.grid_WhPo.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtRefNo = pageControl.FindControl("txt_PoNo") as ASPxTextBox;
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

    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select PoNo from wh_PO where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsInvoice.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select PoNo from wh_PO where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsVoucher.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
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
    #region Receipt
    protected void Grid_Receipt_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select PoNo from wh_PO where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsReceipt.FilterExpression = "PoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Receipt_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if(grid!=null){
            grid.ForceDataRowType(typeof(C2.WhPOReceiptDet));
        }
    }
    #endregion
}