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

public partial class WareHouse_SalesOrders_SalesOrderEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SOWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["SOWhere"] = "SoNo='" + Request.QueryString["no"].ToString() + "'";
                this.txt_SoNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["SOWhere"] == null)
                {
                    this.grid.AddNewRow();
                }
            }
            else
                this.dsWhSo.FilterExpression = "1=0";
        }
        if (Session["SOWhere"] != null)
        {
            this.dsWhSo.FilterExpression = Session["SOWhere"].ToString();
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
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhSo), "Id='" + id + "'");
            WhSo whSo = C2.Manager.ORManager.GetObject(query) as WhSo;
            bool isNew = false;
            string soNo = "";
            if (whSo == null)
            {
                whSo = new WhSo();
                isNew = true;
                soNo = C2Setup.GetNextNo("SaleOrders");
                txtId.Text =SafeValue.SafeString(whSo.Id);
            }
            ASPxTextBox txt_PartyRefNo = pageControl.FindControl("txt_PartyRefNo") as ASPxTextBox;
            whSo.PartyRefNo = txt_PartyRefNo.Text;
            ASPxButtonEdit txt_PartyId = pageControl.FindControl("txt_PartyId") as ASPxButtonEdit;
            whSo.PartyId = txt_PartyId.Text;
            ASPxDateEdit txt_SoDate = pageControl.FindControl("txt_SoDate") as ASPxDateEdit;
            whSo.SoDate = txt_SoDate.Date;
            ASPxDateEdit txt_RequestDate = pageControl.FindControl("txt_RequestDate") as ASPxDateEdit;
            whSo.RequestDate = txt_RequestDate.Date;
            ASPxButtonEdit txt_SalesmanId = pageControl.FindControl("txt_SalesmanId") as ASPxButtonEdit;
            whSo.SalesmanId = txt_SalesmanId.Text;
            ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
            whSo.WarehouseId = txt_WarehouseId.Text;
            ASPxButtonEdit txt_Currency = pageControl.FindControl("txt_Currency") as ASPxButtonEdit;
            whSo.Currency = txt_Currency.Text;
            ASPxSpinEdit spin_ExRate = pageControl.FindControl("spin_ExRate") as ASPxSpinEdit;
            whSo.ExRate = SafeValue.SafeDecimal(spin_ExRate.Value, 1);
            ASPxMemo txt_Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
            whSo.Remark = txt_Remark.Text;

            ASPxSpinEdit txt_DocAmt = pageControl.FindControl("spin_DocAmt") as ASPxSpinEdit;
            whSo.DocAmt = SafeValue.SafeDecimal(txt_DocAmt.Value, 1);
            ASPxSpinEdit txt_LocAmt = pageControl.FindControl("spin_LocAmt") as ASPxSpinEdit;
            whSo.LocAmt = SafeValue.SafeDecimal(txt_DocAmt.Value, 1) * whSo.ExRate;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                whSo.CreateBy = userId;
                whSo.CreateDateTime = DateTime.Now;
                whSo.SoNo = soNo.ToString(); 
                whSo.StatusCode = "USE";
                Manager.ORManager.StartTracking(whSo, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whSo);
                C2Setup.SetNextNo("SaleOrders", soNo);
            }
            else
            {
                whSo.UpdateBy = userId;
                whSo.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(whSo, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whSo);
            }
            Session["SOWhere"] = "SoNo='" + whSo.SoNo + "'";
            this.dsWhSo.FilterExpression = Session["SOWhere"].ToString();
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
                ASPxTextBox soNo = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
                this.dsAttachment.FilterExpression = "JobType='SO' and RefNo='" + soNo.Text + "'";// 
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
            ASPxTextBox WarehouseName = pageControl.FindControl("txt_WhName") as ASPxTextBox;

            partyName.Text = EzshipHelper.GetPartyName(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
            WarehouseName.Text = EzshipHelper.GetWarehouse(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "WarehouseId" }));
            string oid = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                ASPxLabel jobStatusStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                string sql = string.Format("select StatusCode from wh_So  where Id='{0}'", oid);
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
        e.NewValues["SoDate"] = DateTime.Now;
        e.NewValues["RequestDate"] = DateTime.Now;
        e.NewValues["PartyId"] = " ";
        e.NewValues["PartyOrderNo"] = " ";
        e.NewValues["PartyRefNo"] = " ";
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
        ASPxTextBox poNo = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
        if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_So where SoNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update wh_So set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where SoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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

                sql = string.Format("update wh_So set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where SoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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
            string sql_cnt = string.Format("select det.BalQty from wh_SoDet det inner join wh_So s on det.SoNo=s.SoNo  where s.SoNo='{0}'", poNo.Text);
            DataTable dt = C2.Manager.ORManager.GetDataSet(sql_cnt).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int cnt = SafeValue.SafeInt(dt.Rows[i]["BalQty"], 0);
                if (cnt > 0)
                {
                    e.Result = "BalQty";
                    return;
                }
            }
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_So where SoNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update wh_So set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where SoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                sql = string.Format("update wh_So set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where SoNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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
            grd.ForceDataRowType(typeof(C2.WhSo));
        }
    }

    #region So det
    protected void grid_det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhSoDet));
    }
    protected void grid_det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_PoNo = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
        this.dsSoDet.FilterExpression = " SoNo='" + txt_PoNo.Text + "'";
    }
    protected void grid_det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Currency"] = "SGD";
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
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_SoNo = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
        e.NewValues["SoNo"] = txt_SoNo.Text;
        e.NewValues["LineSNo"] = SafeValue.SafeInt(txt_Id.Text, 0);
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
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        int qty = SafeValue.SafeInt(e.NewValues["Qty"], 0);

        e.NewValues["Qty"] = qty;
        e.NewValues["BalQty"] = qty;

    }
    protected void grid_det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
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
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        int oldbalQty = SafeValue.SafeInt(e.OldValues["BalQty"], 0);
        int oldQty = SafeValue.SafeInt(e.OldValues["Qty"], 0);

        if (oldbalQty < oldQty)
        {
            e.NewValues["BalQty"] = oldbalQty + (SafeValue.SafeInt(e.NewValues["Qty"], 0) - oldQty);
        }
        else
        {
            e.NewValues["BalQty"] = e.NewValues["Qty"];
        }
    }
    protected void grid_det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_det_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox oidCtr = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_det_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox oidCtr = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_det_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox oidCtr = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
        string sql = "delete from wh_SoReleaseDet where SoNo='" + SafeValue.SafeString(oidCtr.Text) + "'";
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    private void UpdateMaster(string lineNo)
    {
        decimal docAmt = 0;
        string sql = string.Format("select DocAmt from wh_SoDet where SoNo='{0}'", lineNo);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            docAmt += SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
        }

        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM wh_SoDet AS det INNER JOIN  wh_So AS mast ON det.SoNo = mast.SoNo
WHERE (det.SoNo ='{0}')", lineNo)), 0);

        sql = string.Format("Update wh_So set DocAmt='{0}',LocAmt=DocAmt*ExRate where SoNo='{1}'", docAmt, lineNo);
        C2.Manager.ORManager.ExecuteCommand(sql);

        //sql = string.Format("update wh_SoDet set LineLocAmt=locAmt* (select ExRate from wh_So where SoNo=wh_SoDet.SoNo) where SoNo='{0}'", lineNo);
        //C2.Manager.ORManager.ExecuteCommand(sql);
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
        ASPxTextBox txtRefNo = pageControl.FindControl("txt_SoNo") as ASPxTextBox;
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