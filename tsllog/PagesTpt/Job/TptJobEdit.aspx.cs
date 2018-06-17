using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;
using DevExpress.Web.ASPxTabControl;
using System.Data;

public partial class PagesTpt_Job_WhJobEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string typ = SafeValue.SafeString(Request.QueryString["typ"], "Trucking");
            this.txt_type.Text = typ;
            Session["TptJob_" + this.txt_type.Text] = null;
            if (Request.QueryString["no"] != null)
            {
                string no = Request.QueryString["no"].ToString();

                if (no == "0")
                {
                    this.grid_Transport.AddNewRow();
                }
                else if (no.Length > 0)
                {
                    this.txt_TptNo.Text = no;
                    Session["TptJob_" + this.txt_type.Text] = string.Format("JobNo ='{0}' and JobType='{1}'", no, typ);
                }
            }
        }
        if (Session["TptJob_" + this.txt_type.Text] != null)
        {
            this.dsTransport.FilterExpression = Session["TptJob_" + this.txt_type.Text].ToString();
            if (this.grid_Transport.GetRow(0) != null)
                this.grid_Transport.StartEdit(0);
        }
    }
    #region Job Info
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.TptJob));
        }
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["JobNo"] = "NEW";
        e.NewValues["JobDate"] = DateTime.Today;
        e.NewValues["Qty"] = 0;
        e.NewValues["Wt"] = 0;
        e.NewValues["M3"] = 0;
        e.NewValues["PkgType"] = " ";
        e.NewValues["SortIndex"] = 0;

        e.NewValues["JobProgress"] = "Booked";
        e.NewValues["JobLevel"] = "NORMAL";
        e.NewValues["Eta"] = DateTime.Today;
        e.NewValues["Etd"] = DateTime.Today;
        e.NewValues["Term"] = "Cash";

        e.NewValues["JobCode"] = "";
        e.NewValues["Driver"] = "";
        e.NewValues["VehicleType"] = "Lorry";
        e.NewValues["Cust"] = "";
        e.NewValues["JobType"] = this.txt_type.Text;
        e.NewValues["StatusCode"] = "USE";
    }
    protected void SaveJob()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox idCtr = pageControl.FindControl("txt_Id") as ASPxTextBox;
            ASPxTextBox jobNoCtr = pageControl.FindControl("txt_JobNo") as ASPxTextBox;

            string jobNo = jobNoCtr.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.TptJob), "Id='" + idCtr.Text + "'");
            C2.TptJob tj = C2.Manager.ORManager.GetObject(query) as C2.TptJob;
            bool isNew = false;
            string runType = "LocalTpt";
            ASPxDateEdit jobDate = pageControl.FindControl("date_JobDate") as ASPxDateEdit;
            if (tj == null)
            {
                tj = new C2.TptJob();
                isNew = true;
                tj.JobType = this.txt_type.Text;
                jobNo = C2Setup.GetNextNo(tj.JobType, runType, jobDate.Date);
                tj.JobNo = jobNo;
                tj.UserId = HttpContext.Current.User.Identity.Name;
                tj.EntryDate = DateTime.Now;
            }
            tj.JobDate = jobDate.Date;
            ASPxButtonEdit cust = pageControl.FindControl("btn_Cust") as ASPxButtonEdit;
            tj.Cust = cust.Text;
            ASPxTextBox bkgRef = pageControl.FindControl("txt_BkgRef") as ASPxTextBox;
            tj.BkgRef = bkgRef.Text;

            ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
            tj.Vessel = ves.Text;
            ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
            tj.Voyage = voy.Text;
            ASPxButtonEdit pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
            tj.Pol = pol.Text;
            ASPxButtonEdit pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
            tj.Pod = pod.Text;
            ASPxDateEdit eta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
            tj.Eta = eta.Date;
            ASPxDateEdit etd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            tj.Etd = etd.Date;
            ASPxTextBox blRef = pageControl.FindControl("txt_BlRef") as ASPxTextBox;
            tj.BlRef = blRef.Text;

            ASPxSpinEdit qty = pageControl.FindControl("spin_Qty") as ASPxSpinEdit;
            tj.Qty = SafeValue.SafeInt(qty.Value, 0);
            ASPxButtonEdit pkgType = pageControl.FindControl("txt_PackType") as ASPxButtonEdit;
            tj.PkgType = pkgType.Text;
            ASPxSpinEdit wt = pageControl.FindControl("spin_Wt") as ASPxSpinEdit;
            tj.Wt = SafeValue.SafeDecimal(wt.Value, 0);
            ASPxSpinEdit m3 = pageControl.FindControl("spin_M3") as ASPxSpinEdit;
            tj.M3 = SafeValue.SafeDecimal(m3.Value, 0);

            ASPxMemo pickFrm1 = pageControl.FindControl("txt_PickupFrm1") as ASPxMemo;
            tj.PickFrm1 = pickFrm1.Text;
            ASPxMemo deliveryTo1 = pageControl.FindControl("txt_DeliveryTo1") as ASPxMemo;
            tj.DeliveryTo1 = deliveryTo1.Text;
            ASPxMemo jobRmk = pageControl.FindControl("txt_JobRmk") as ASPxMemo;
            tj.JobRmk = jobRmk.Text;
            ASPxMemo mkg = pageControl.FindControl("txt_cargoMkg") as ASPxMemo;
            tj.CargoMkg = mkg.Text;
            ASPxMemo des = pageControl.FindControl("txt_cargoDes") as ASPxMemo;
            tj.CargoDesc = des.Text;

            if (isNew)
            {
                tj.CreateBy = EzshipHelper.GetUserName();
                tj.CreateDateTime = DateTime.Now;
                tj.StatusCode = "USE";
                C2.Manager.ORManager.StartTracking(tj, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(tj);
            }
            else
            {
                tj.UpdateBy = EzshipHelper.GetUserName();
                tj.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(tj, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(tj);
            }
            if (isNew)
            {
                jobNoCtr.Text = jobNo;
                C2Setup.SetNextNo(tj.JobType, runType, jobNo, tj.JobDate);
                string where = "JobNo='" + jobNo + "'";
                Session["TptJob_" + this.txt_type.Text] = where;
                this.dsTransport.FilterExpression = where;
                if (this.grid_Transport.GetRow(0) != null)
                    this.grid_Transport.StartEdit(0);
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }


    protected void grid_Transport_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        try
        {
            if (this.grid_Transport.EditingRowVisibleIndex > -1)
            {
                ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                ASPxTextBox custName = pageControl.FindControl("txt_Cust") as ASPxTextBox;
                custName.Text = EzshipHelper.GetPartyName(this.grid_Transport.GetRowValues(this.grid_Transport.EditingRowVisibleIndex, new string[] { "Cust" }));
                string oid = SafeValue.SafeString(this.grid_Transport.GetRowValues(this.grid_Transport.EditingRowVisibleIndex, new string[] { "Id" }));
                if (oid.Length > 0)
                {
                    string userId = HttpContext.Current.User.Identity.Name;
                    string sql = string.Format("select StatusCode from Tpt_Job  where Id='{0}'", oid);
                    string refStatusCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
                    ASPxButton btn = this.grid_Transport.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                    ASPxButton btn_VoidMaster = this.grid_Transport.FindEditFormTemplateControl("btn_VoidMaster") as ASPxButton;
                    ASPxLabel jobStatusStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                    if (refStatusCode == "CNL")
                    {
                        btn_VoidMaster.Text = "Unvoid";
                        jobStatusStr.Text = "Void";
                    }
                    else if (refStatusCode == "CLS")
                    {
                        btn.Text = "Open Job";
                        jobStatusStr.Text = "Close";
                    }
                    else
                    {
                        btn_VoidMaster.Text = "Void";
                        jobStatusStr.Text = "USE";
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ex.StackTrace);
        }
    }
    protected void grid_Transport_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        try
        {
            if (s == "Photo")
            {
                if (this.dsJobPhoto.FilterExpression == "1=0")
                {
                    ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                    ASPxTextBox refNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
                    this.dsJobPhoto.FilterExpression = string.Format("JobType='Tpt' and RefNo='{0}'", refNo.Text);
                }
            }
            if (s == "Save")
                SaveJob();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ex.StackTrace);
        }
    }


    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox masterId = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        string userId = HttpContext.Current.User.Identity.Name;
        if (s == "VoidJob")
        {
            #region void master
            //billing
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='TPT' and MastRefNo='{0}'", jobNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from TPT_Job where JobNo='" + jobNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update TPT_Job set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(jobNo.Text, "", "TPT", "Unvoid");
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                bool closeByEst = EzshipHelper.GetCloseEstInd(jobNo.Text, "TPT");
                if (closeByEst)
                {
                    sql = string.Format("update TPT_Job set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(jobNo.Text, "", "TPT", "Void");
                        e.Result = "Success";
                    }
                    else
                        e.Result = "Fail";
                }
                else
                    e.Result = "NoMatch";
            }
            #endregion
        }
        if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = pageControl.FindControl("lab_CloseInd") as ASPxLabel;
            ASPxButton btn = pageControl.FindControl("btn_CloseJob") as ASPxButton;
            string sql = "select StatusCode from Tpt_Job where id='" + masterId.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// closeIndStr.Text;
            if (closeInd == "CLS")
            {
                sql = string.Format("update Tpt_Job set StatusCode='USE' where Id='{0}'", masterId.Text);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(jobNo.Text, "", "TPT", "Open");
                    e.Result = "Success";
                    //btn.Text = "Close Job";
                    //closeIndStr.Text = "N";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format("update Tpt_Job set StatusCode='CLS' where Id='{2}'", DateTime.Today.ToString("yyyy-MM-dd"), userId, masterId.Text);

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(jobNo.Text, "", "TPT", "Close");
                    e.Result = "Success";
                    //btn.Text = "Open Job";
                    //closeIndStr.Text = "Y";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            #endregion
        }
    }

    #endregion
    #region Billing
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from Tpt_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        string refNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));

        this.dsInvoice.FilterExpression = string.Format("MastRefNo='{0}' and MastType='TPT'", refNo);
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from Tpt_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        string refNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
        this.dsVoucher.FilterExpression = string.Format("MastRefNo='{0}' and MastType='TPT'", refNo);
    }

    #endregion
    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select JobNo from Tpt_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.TptCosting));
        }
    }
    protected void grid_Cost_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SaleQty"] = 1;
        e.NewValues["CostQty"] = 1;
        e.NewValues["SalePrice"] = 0;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["SaleLocAmt"] = 0;
        e.NewValues["CostLocAmt"] = 0;
        e.NewValues["VendorId"] = " ";
        e.NewValues["ChgCode"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["SaleCurrency"] = "SGD";
        e.NewValues["SaleExRate"] = 1;
        e.NewValues["CostCurrency"] = "SGD";
        e.NewValues["CostExRate"] = 1;
        e.NewValues["JobNo"] = "0";
        e.NewValues["SplitType"] = "Set";
    }
    protected void grid_Cost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobType"] = "Tpt";
        e.NewValues["RefNo"] = refNo.Text;
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_Cost_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
    }
    protected void grid_Cost_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox vendorName = grd.FindEditFormTemplateControl("txt_CostVendorName") as ASPxTextBox;
        vendorName.Text = EzshipHelper.GetPartyName(grd.GetRowValues(grd.EditingRowVisibleIndex, new string[] { "VendorId" }));
    }
    #endregion

    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.TptAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtRefNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        this.dsJobPhoto.FilterExpression = "RefNo='" + SafeValue.SafeString(txtRefNo.Text, "") + "'";
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
    }

    #endregion
}
    
