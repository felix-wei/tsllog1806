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
            string typ = SafeValue.SafeString(Request.QueryString["typ"], "");
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
                    Session["TptJob_" + this.txt_type.Text] = string.Format("JobNo ='{0}' ", no, typ);
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
        e.NewValues["BkgQty"] = 0;
        e.NewValues["BkgWt"] = 0;
        e.NewValues["BkgM3"] = 0;

        e.NewValues["JobProgress"] = "";
        e.NewValues["Eta"] = DateTime.Today;
        e.NewValues["Etd"] = DateTime.Today;

        e.NewValues["JobType"] = this.txt_type.Text;
        if (this.txt_type.Text.Length < 2)
            e.NewValues["JobType"] = "IMP";
        e.NewValues["StatusCode"] = "USE";



        e.NewValues["FeeTpt"] = 0;
        e.NewValues["FeeAdmin"] = 0;
        e.NewValues["FeeOt"] = 0;
        e.NewValues["FeeMisc"] = 0;
        e.NewValues["FeeLabour"] = 0;
        e.NewValues["FeeReimberse"] = 0;
    }
    protected string SaveJob()
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

            bool isAddTripLog = false;
            string runType = "LocalTpt";
            ASPxDateEdit jobDate = pageControl.FindControl("date_JobDate") as ASPxDateEdit;
            if (tj == null)
            {
                tj = new C2.TptJob();
                isNew = true;
                tj.JobType = this.txt_type.Text;
                jobNo = C2Setup.GetNextNo(tj.JobType, runType, jobDate.Date);
                tj.JobNo = jobNo;
            }
            tj.JobDate = jobDate.Date;
            ASPxComboBox jobType = pageControl.FindControl("cmb_JobType") as ASPxComboBox;
            tj.JobType = jobType.Text;
            ASPxButtonEdit cust = pageControl.FindControl("btn_Cust") as ASPxButtonEdit;
            tj.Cust = cust.Text;
            ASPxTextBox bkgRef = pageControl.FindControl("txt_BkgRef") as ASPxTextBox;
            tj.BkgRef = bkgRef.Text;
            ASPxTextBox pic = pageControl.FindControl("txt_CustPic") as ASPxTextBox;
            tj.CustPic = pic.Text;
            ASPxTextBox email = pageControl.FindControl("txt_CustEmail") as ASPxTextBox;
            tj.CustEmail = email.Text;
            ASPxTextBox docNo = pageControl.FindControl("txt_CustDocNo") as ASPxTextBox;
            tj.CustDocNo = docNo.Text;
            ASPxComboBox custDocType = pageControl.FindControl("txt_CustDocType") as ASPxComboBox;
            tj.CustDocType = custDocType.Text;

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



            ASPxDateEdit bkgDate = pageControl.FindControl("date_BkgDate") as ASPxDateEdit;
            tj.BkgDate = SafeValue.SafeDate(bkgDate.Date, new DateTime(1753, 1, 1));
            ASPxTextBox bkgTime = pageControl.FindControl("txt_BkgTime") as ASPxTextBox;
            tj.BkgTime = bkgTime.Text;
            ASPxTextBox jobRmk = pageControl.FindControl("txt_JobRmk") as ASPxTextBox;
            tj.JobRmk = jobRmk.Text;

            ASPxSpinEdit bkgqty = pageControl.FindControl("spin_BkgQty") as ASPxSpinEdit;
            tj.BkgQty = SafeValue.SafeInt(bkgqty.Value, 0);
            ASPxButtonEdit bkgpkgType = pageControl.FindControl("txt_BkgPackType") as ASPxButtonEdit;
            tj.BkgPkgType = bkgpkgType.Text;
            ASPxSpinEdit bkgwt = pageControl.FindControl("spin_BkgWt") as ASPxSpinEdit;
            tj.BkgWt = SafeValue.SafeDecimal(bkgwt.Value, 0);
            ASPxSpinEdit bkgm3 = pageControl.FindControl("spin_BkgM3") as ASPxSpinEdit;
            tj.BkgM3 = SafeValue.SafeDecimal(bkgm3.Value, 0);

            ASPxMemo pickFrm1 = pageControl.FindControl("txt_PickupFrm1") as ASPxMemo;
            tj.PickFrm1 = pickFrm1.Text;
            ASPxMemo deliveryTo1 = pageControl.FindControl("txt_DeliveryTo1") as ASPxMemo;
            tj.DeliveryTo1 = deliveryTo1.Text;
            ASPxMemo mkg = pageControl.FindControl("txt_cargoMkg") as ASPxMemo;
            tj.CargoMkg = mkg.Text;
            ASPxMemo des = pageControl.FindControl("txt_cargoDes") as ASPxMemo;
            tj.CargoDesc = des.Text;


            ASPxDateEdit tptDate = pageControl.FindControl("date_TptDate") as ASPxDateEdit;
            tj.TptDate = SafeValue.SafeDate(tptDate.Date, new DateTime(1753, 1, 1));
            ASPxTextBox tptTime = pageControl.FindControl("txt_TptTime") as ASPxTextBox;
            tj.TptTime = tptTime.Text;
            ASPxComboBox JobProgress = pageControl.FindControl("cmb_JobStatus") as ASPxComboBox;
            if (!isNew && tj.JobProgress != JobProgress.Text)
            {
                isAddTripLog = true;
            }
            tj.JobProgress = JobProgress.Text;
            ASPxComboBox tptType = pageControl.FindControl("cmb_TptType") as ASPxComboBox;
            tj.TptType = tptType.Text;
            ASPxComboBox tipCode = pageControl.FindControl("cbb_Trip_TripCode") as ASPxComboBox;
            tj.TripCode = tipCode.Text;

            ASPxButtonEdit driver = pageControl.FindControl("btn_DriverCode") as ASPxButtonEdit;
            tj.Driver = driver.Text;
            ASPxButtonEdit vehicle = pageControl.FindControl("btn_vehicle") as ASPxButtonEdit;
            tj.VehicleNo = vehicle.Text;

            ASPxSpinEdit qty = pageControl.FindControl("spin_Qty") as ASPxSpinEdit;
            tj.Qty = SafeValue.SafeInt(qty.Value, 0);
            ASPxButtonEdit pkgType = pageControl.FindControl("txt_PackType") as ASPxButtonEdit;
            tj.PkgType = pkgType.Text;
            ASPxSpinEdit wt = pageControl.FindControl("spin_Wt") as ASPxSpinEdit;
            tj.Wt = SafeValue.SafeDecimal(wt.Value, 0);
            ASPxSpinEdit m3 = pageControl.FindControl("spin_M3") as ASPxSpinEdit;
            tj.M3 = SafeValue.SafeDecimal(m3.Value, 0);


            ASPxSpinEdit FeeTpt = pageControl.FindControl("spin_FeeTpt") as ASPxSpinEdit;
            tj.FeeTpt = SafeValue.SafeDecimal(FeeTpt.Value, 0);
            ASPxSpinEdit FeeLabour = pageControl.FindControl("spin_FeeLabour") as ASPxSpinEdit;
            tj.FeeLabour = SafeValue.SafeDecimal(FeeLabour.Value, 0);
            ASPxSpinEdit FeeOt = pageControl.FindControl("spin_FeeOt") as ASPxSpinEdit;
            tj.FeeOt = SafeValue.SafeDecimal(FeeOt.Value, 0);
            ASPxSpinEdit FeeAdmin = pageControl.FindControl("spin_FeeAdmin") as ASPxSpinEdit;
            tj.FeeAdmin = SafeValue.SafeDecimal(FeeAdmin.Value, 0);
            ASPxSpinEdit FeeReimberse = pageControl.FindControl("spin_FeeReimberse") as ASPxSpinEdit;
            tj.FeeReimberse = SafeValue.SafeDecimal(FeeReimberse.Value, 0);
            ASPxSpinEdit FeeMisc = pageControl.FindControl("spin_FeeMisc") as ASPxSpinEdit;
            tj.FeeMisc = SafeValue.SafeDecimal(FeeMisc.Value, 0);

            tj.FeeTotal = tj.FeeTpt + tj.FeeLabour + tj.FeeOt + tj.FeeAdmin + tj.FeeReimberse + tj.FeeMisc;
            ASPxTextBox FeeRemark = pageControl.FindControl("txt_FeeRemark") as ASPxTextBox;
            tj.FeeRemark = FeeRemark.Text;


            if (isNew)
            {
                tj.CreateBy = EzshipHelper.GetUserName();
                tj.CreateDateTime = DateTime.Now;
                tj.StatusCode = "USE";
                C2.Manager.ORManager.StartTracking(tj, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(tj);
                if (tj.JobProgress == "Assigned")
                {
                    InsertTripLog(tj);
                }
            }
            else
            {
                tj.UpdateBy = EzshipHelper.GetUserName();
                tj.UpdateDateTime = DateTime.Now;

                C2.Manager.ORManager.StartTracking(tj, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(tj);
                if (isAddTripLog)
                    InsertTripLog(tj);


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
            string re = HttpContext.Current.User.Identity.Name + "," + tj.Driver + "," + tj.JobNo;
            return re;
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }

    }
    private void InsertTripLog(TptJob job)
    {
        C2.CtmTripLog tripLog = new CtmTripLog();
        tripLog.Driver = job.Driver;
        tripLog.JobNo = job.JobNo;
        tripLog.JobType = "Local";
        tripLog.LogDate = DateTime.Today;
        tripLog.LogTime = DateTime.Now.ToString("HH:mm");
        tripLog.Remark = "";
        tripLog.Status = job.JobProgress;
        tripLog.TripId = job.Id;
        tripLog.CreateBy = EzshipHelper.GetUserName();
        tripLog.CreateDateTime = DateTime.Now;
        tripLog.UpdateBy = EzshipHelper.GetUserName();
        tripLog.UpdateDateTime = DateTime.Now;

        C2.Manager.ORManager.StartTracking(tripLog, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(tripLog);
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
        if (s == "Save")
        {
            e.Result = SaveJob();
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


    #region Trip Log
    protected void grid_TripLog_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmTripLog));
        }
    }
    protected void grid_TripLog_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from tpt_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsTripLog.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and JobType='Local'";
    }
    protected void grid_TripLog_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["LogDate"] = DateTime.Now;
        e.NewValues["LogTime"] = DateTime.Now.ToString("HH:mm");
        e.NewValues["Status"] = "";
    }
    protected void grid_TripLog_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"], "");
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["JobType"] = "Local";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        e.NewValues["TripId"] = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        string sql = "select JobNo from tpt_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        e.NewValues["JobNo"] = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
    }
    protected void grid_TripLog_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"], "");
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_TripLog_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }


    #endregion



    #region  Stock

    protected void gv_stock_Init(object sender, EventArgs e)
    {

        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobStock));
        }
    }
    protected void gv_stock_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from TPT_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsStock.FilterExpression = "JobNo='" + JobNo + "'";

    }
    protected void gv_stock_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["StockStatus"] = "IN";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["StockQty"] = 0;
        e.NewValues["PackingQty"] = 0;
    }
    protected void gv_stock_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;

    }
    protected void gv_stock_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gv_stock_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["StockDescription"] = SafeValue.SafeString(e.NewValues["StockDescription"]);
        e.NewValues["StockMarking"] = SafeValue.SafeString(e.NewValues["StockMarking"]);
        e.NewValues["StockUnit"] = SafeValue.SafeString(e.NewValues["StockUnit"]);
        e.NewValues["PackingUnit"] = SafeValue.SafeString(e.NewValues["PackingUnit"]);
        e.NewValues["PackingDimention"] = SafeValue.SafeString(e.NewValues["PackingDimention"]);
    }

    #endregion

    #region Activity
    protected void gv_activity_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from TPT_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsActivity.FilterExpression = "JobNo='" + JobNo + "'";
    }
    #endregion
}

