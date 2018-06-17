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

public partial class WareHouse_SalesOrders_SalesOrderReleaseEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SORWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["SORWhere"] = "ReleaseNo='" + Request.QueryString["no"].ToString() + "'";
                this.txt_PoNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["SORWhere"] == null)
                {
                    this.grid.AddNewRow();
                }
            }
            else
                this.dsWhSORelease.FilterExpression = "1=0";
        }
        if (Session["SORWhere"] != null)
        {
            this.dsWhSORelease.FilterExpression = Session["SORWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txtId = pageControl.FindControl("txt_Id") as ASPxTextBox;
            string id = SafeValue.SafeString(txtId.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhSORelease), "Id='" + id + "'");
            WhSORelease obj = C2.Manager.ORManager.GetObject(query) as WhSORelease;
            bool isNew = false;
            string soNo = "";
            if (obj == null)
            {
                obj = new WhSORelease();
                isNew = true;
                soNo = C2Setup.GetNextNo("SaleOrdersRelease");
            }
            ASPxTextBox txt_PartyRefNo = pageControl.FindControl("txt_PartyRefNo") as ASPxTextBox;
            obj.PartyRefNo = txt_PartyRefNo.Text;
            ASPxButtonEdit txt_PartyId = pageControl.FindControl("txt_PartyId") as ASPxButtonEdit;
            obj.PartyId = txt_PartyId.Text;
            ASPxDateEdit de_ReleaseDate = pageControl.FindControl("de_ReleaseDate") as ASPxDateEdit;
            obj.ReleaseDate = de_ReleaseDate.Date;
            ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
            obj.WarehouseId = txt_WarehouseId.Text;
            ASPxButtonEdit txt_SalesmanId = pageControl.FindControl("txt_SalesmanId") as ASPxButtonEdit;
            obj.SalesmanId = txt_SalesmanId.Text;
            ASPxButtonEdit txt_Currency = pageControl.FindControl("txt_Currency") as ASPxButtonEdit;
            obj.Currency = txt_Currency.Text;
            ASPxSpinEdit spin_ExRate = pageControl.FindControl("spin_ExRate") as ASPxSpinEdit;
            obj.ExRate = SafeValue.SafeDecimal(spin_ExRate.Value, 1);
            ASPxMemo txt_Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
            obj.Remark = txt_Remark.Text;


            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                obj.CreateBy = userId;
                obj.CreateDateTime = DateTime.Now;
                obj.ReleaseNo= soNo.ToString();
                obj.StatusCode = "USE";
                Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(obj);
                C2Setup.SetNextNo("SaleOrdersRelease", soNo);
            }
            else
            {
                obj.UpdateBy = userId;
                obj.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(obj);
            }
            Session["SORWhere"] = "ReleaseNo='" + obj.ReleaseNo + "'";
            this.dsWhSORelease.FilterExpression = Session["SORWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
        catch { }
    }
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            if (this.dsAttachment.FilterExpression == "1=0")
            {
                ASPxGridView grd = sender as ASPxGridView;
                ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                ASPxTextBox poNo = pageControl.FindControl("txt_RoNo") as ASPxTextBox;
                this.dsAttachment.FilterExpression = "JobType='SOR' and RefNo='" + poNo.Text + "'";// 
            }
        }
        else if (s == "Save")
            Save();
    }
    protected void grid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

            ASPxTextBox partyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox whName = pageControl.FindControl("txt_WhName") as ASPxTextBox;
            partyName.Text = EzshipHelper.GetPartyName(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
            whName.Text = EzshipHelper.GetWarehouse(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "WarehouseId" }));
            string oid = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                ASPxLabel jobStatusStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                string sql = string.Format("select StatusCode from wh_SORelease  where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grid.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_VoidMaster = this.grid.FindEditFormTemplateControl("btn_VoidMaster") as ASPxButton;
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
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SoNo"] = "NEW";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["ReleaseDate"] = DateTime.Now;
        e.NewValues["PartyId"] = "";
        e.NewValues["PartyRefNo"] = "";
        e.NewValues["Currency"] = "SGD";
        e.NewValues["WarehouseId"] = "";
        e.NewValues["SalesmanId"] = "";
        e.NewValues["ExRate"] = "1.000000";
        e.NewValues["DocAmt"] = "0";
        e.NewValues["LocAmt"] = "0";
        e.NewValues["StatusCode"] = "USE";
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox poNo = pageControl.FindControl("txt_RoNo") as ASPxTextBox;
        if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_SORelease where ReleaseNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update wh_SORelease set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where ReleaseNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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

                sql = string.Format("update wh_SORelease set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where ReleaseNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='SOR' and MastRefNo='{0}'", poNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            sql_cnt = string.Format("select s.BalQty from wh_SoDet s inner join wh_SoReleaseDet det on  s.SoNo=det.SoNo and s.Product=det.Product where det.ReleaseNo='{0}'", poNo.Text);
            cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "BalQty";
                return;
            }
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_SORelease where ReleaseNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update wh_SORelease set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where ReleaseNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                sql = string.Format("update wh_SORelease set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where ReleaseNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhSORelease));
        }
    }

    #region SOR det
    protected void grid_det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhSOReleaseDet));
    }
    protected void grid_det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_PoNo = pageControl.FindControl("txt_RoNo") as ASPxTextBox;
        this.dsWhSOReleaseDet.FilterExpression = " ReleaseNo='" + txt_PoNo.Text + "'";
    }
    protected void grid_det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
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
        e.NewValues["BalQty"] = 1;
    }
    protected void grid_det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_RoNo = pageControl.FindControl("txt_RoNo") as ASPxTextBox;
        e.NewValues["ReleaseNo"] = txt_RoNo.Text;
        e.NewValues["LineSNo"] = SafeValue.SafeInt(txt_Id.Text, 0);
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        if (!SafeValue.SafeString(e.NewValues["Currency"],"").Equals("SGD")) //if (!e.NewValues["Currency"].Equals("SGD"))
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
        int qty = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        int balQty = GetBalQty(grd);

        if (qty > balQty)
        {
            e.Cancel = true;
            throw new Exception("Pls write Qty again");
        }

    }
    protected void grid_det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the charge code");
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
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        int qty = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        int balQty = GetBalQty(grd);

        if (qty > balQty)
        {
            e.Cancel = true;
            throw new Exception("Pls write Qty again");
        }
        
    }
    protected void grid_det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int id = SafeValue.SafeInt(e.Values["Id"], 0);
        string poNo = SafeValue.SafeString(e.Values["SoNo"]);
        string product = SafeValue.SafeString(e.Values["Product"]);
        UpdateMaster(id, 0, poNo, product);
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_det_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        int id = SafeValue.SafeInt(e.NewValues["Id"], 0);
        string poNo = SafeValue.SafeString(e.NewValues["SoNo"]);
        string product = SafeValue.SafeString(e.NewValues["Product"]);
        UpdateMaster(id, 0, poNo, product);
    }
    protected void grid_det_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        int id = SafeValue.SafeInt(e.NewValues["Id"], 0);
        string poNo = SafeValue.SafeString(e.NewValues["SoNo"]);
        string product = SafeValue.SafeString(e.NewValues["Product"]);
        UpdateMaster(id, 0, poNo, product);
    }
    protected void grid_det_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        
    }
    private void UpdateMaster(int detId, int qty, string poNo, string product)
    {
        decimal docAmt = 0;
        decimal locAmt = 0;
        string sql = string.Format("select DocAmt from wh_SoReleaseDet where Id={0}", detId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            docAmt += SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
        }

        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM wh_SoReleaseDet AS det INNER JOIN  wh_SORelease AS mast ON det.ReleaseNo = mast.ReleaseNo
WHERE (det.Id = {0})", detId)), 0);

        sql = string.Format("Update wh_SoReleaseDet set DocAmt='{0}',LocAmt=DocAmt*ExRate where Id='{1}'", docAmt, locAmt, detId);
        C2.Manager.ORManager.ExecuteCommand(sql);

         sql = string.Format("update wh_SoReleaseDet set LineLocAmt=locAmt* (select ExRate from wh_SORelease where ReleaseNo=wh_SoReleaseDet.ReleaseNo) where Id={0}", detId);
        C2.Manager.ORManager.ExecuteCommand(sql);

        sql = string.Format("update wh_SoDet set BalQty=Qty-{0} where SoNo='{1}' and Product='{2}'", qty, poNo, product);
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
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtRefNo = pageControl.FindControl("txt_RoNo") as ASPxTextBox;
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
        string sql = "select ReleaseNo from wh_SORelease where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsInvoice.FilterExpression = "MastType='SOR' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select ReleaseNo from wh_SORelease where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsVoucher.FilterExpression = "MastType='SOR' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
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

    private int GetBalQty(ASPxGridView grd)
    {
        ASPxButtonEdit txt_det_Product = grd.FindEditFormTemplateControl("txt_det_Product") as ASPxButtonEdit;
        ASPxSpinEdit spin_det_Price = grd.FindEditFormTemplateControl("spin_det_Price") as ASPxSpinEdit;
        string product = SafeValue.SafeString(txt_det_Product.Text);
        decimal price = SafeValue.SafeDecimal(spin_det_Price.Value,0);
        string sql = string.Format("select (select SUM(Qty) from wh_POReceiptDet where Price={1} and Product='{0}')-isnull((select sum(Qty) from wh_SoReleaseDet where  Price={1} and Product='{0}'),0)", product, price);
        return SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
    }
}