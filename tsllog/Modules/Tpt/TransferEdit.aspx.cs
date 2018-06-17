using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Tpt_TransferEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["CTM_Job"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                txt_search_JobNo.Text = Request.QueryString["no"].ToString();
                string sql = string.Format(@"select count(*) from ctm_job where JobNo='{0}'", Request.QueryString["no"]);
                int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    Session["CTM_Job_" + txt_search_JobNo.Text] = " JobNo='" + txt_search_JobNo.Text + "'";
                    this.dsJob.FilterExpression = " JobNo='" + txt_search_JobNo.Text + "'";
                    if (this.grid_job.GetRow(0) != null)
                        this.grid_job.StartEdit(0);
                }
                else
                {
                    Session["CTM_Job_" + txt_search_JobNo.Text] = " QuoteNo='" + txt_search_JobNo.Text + "'";
                    this.dsJob.FilterExpression = " QuoteNo='" + txt_search_JobNo.Text + "'";
                    if (this.grid_job.GetRow(0) != null)
                        this.grid_job.StartEdit(0);
                }
            }
            else
            {
                this.grid_job.AddNewRow();
            }
        }

        if (Session["CTM_Job_" + txt_search_JobNo.Text] != null)
        {
            this.dsJob.FilterExpression = Session["CTM_Job_" + txt_search_JobNo.Text].ToString();
            if (this.grid_job.GetRow(0) != null)
                this.grid_job.StartEdit(0);
        }
    }

    #region Job
    protected void grid_job_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJob));
        }
    }
    protected void grid_job_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["JobDate"] = DateTime.Now;
        e.NewValues["EtaDate"] = DateTime.Now;
        e.NewValues["EtdDate"] = DateTime.Now;
        e.NewValues["CodDate"] = DateTime.Now;
        e.NewValues["JobType"] = "";
        e.NewValues["IsTrucking"] = "Yes";
        e.NewValues["Pod"] = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
    }
    protected void grid_job_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Void")
        {
            e.Result = job_void();
        }

        if (s == "Save")
        {
            e.Result = job_save();
        }

        if (s == "Close")
        {
            e.Result = job_close();
        }
    }
    private string job_close()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;

        string sql = "update CTM_Job set StatusCode=case when StatusCode='CLS' then 'USE' else 'CLS' end where Id=" + Id.Text;
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            sql = string.Format(@"select StatusCode from CTM_Job where Id={0}", Id.Text);
            string status = ConnectSql.ExecuteScalar(sql).ToString();
            CtmJobEventLog log = new CtmJobEventLog();
            log.CreateDateTime = DateTime.Now;
            log.Controller = HttpContext.Current.User.Identity.Name;
            log.Platform_isWeb();
            log.JobNo = txt_JobNo.Text;
            log.JobStatus = status;
            log.Remark ="Transfer Job Close";
            log.log();

            return "";
        }

        return "error";
    }
    private string job_void()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        string sql = "update CTM_Job set StatusCode=case when StatusCode='CNL' then 'USE' else 'CNL' end where Id=" + Id.Text;
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            string userId = HttpContext.Current.User.Identity.Name;
            int jobId = SafeValue.SafeInt(Id.Text, 0);
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            elog.ActionLevel_isJOB(jobId);
            elog.Remark = "Job Void";
            elog.log();
            return "";
        }

        return "error";
    }
    private string job_save()
    {
        string res = "";
        try
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            string jobNo = SafeValue.SafeString(txt_JobNo.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + jobNo + "'");
            C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;

            ASPxDateEdit jobDate = this.grid_job.FindEditFormTemplateControl("txt_JobDate") as ASPxDateEdit;
            bool isNew = false;
            if (ctmJob == null)
            {
                isNew = true;
                ctmJob = new C2.CtmJob();
                ctmJob.JobNo = C2Setup.GetNextNo("", "CTM_Job", jobDate.Date);
            }
            ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
            ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
            ASPxTextBox txt_ClientRefNo = this.grid_job.FindEditFormTemplateControl("txt_ClientRefNo") as ASPxTextBox;

            ASPxTextBox txt_notifiEmail = this.grid_job.FindEditFormTemplateControl("txt_notifiEmail") as ASPxTextBox;

            ASPxButtonEdit btn_SubClientId = this.grid_job.FindEditFormTemplateControl("btn_SubClientId") as ASPxButtonEdit;
            ASPxButtonEdit txt_WareHouseId = this.grid_job.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            ASPxTextBox txt_ClientContact = this.grid_job.FindEditFormTemplateControl("txt_ClientContact") as ASPxTextBox;

            ctmJob.ClientContact = SafeValue.SafeString(txt_ClientContact.Text);
            ctmJob.JobDate = SafeValue.SafeDate(jobDate.Date, new DateTime(1753, 1, 1));         
            ctmJob.JobType = cbb_JobType.Text;
            ctmJob.ClientId = btn_ClientId.Text;
            ctmJob.ClientRefNo = txt_ClientRefNo.Text;
            ctmJob.EmailAddress = txt_notifiEmail.Text;
            ctmJob.SubClientId = btn_SubClientId.Text;

            if (txt_WareHouseId != null)
                ctmJob.WareHouseCode = txt_WareHouseId.Text;
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                ctmJob.StatusCode = "USE";

                ctmJob.CreateBy = userId;
                ctmJob.CreateDateTime = DateTime.Now;
                ctmJob.UpdateBy = userId;
                ctmJob.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(ctmJob, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(ctmJob);
            }
            else
            {
                ctmJob.UpdateBy = userId;
                ctmJob.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(ctmJob, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(ctmJob);

                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isWeb();
                elog.Controller = userId;
                elog.ActionLevel_isJOB(ctmJob.Id);
                elog.Remark = "Job Update";
                elog.log();
            }

            if (isNew)
            {
                txt_JobNo.Text = ctmJob.JobNo;
                //txt_search_JobNo.Text = txt_JobNo.Text;
                C2Setup.SetNextNo("", "CTM_Job", ctmJob.JobNo, jobDate.Date);
            }

            //res = Job_Check_JobLevel(ctmJob.JobNo);

            Session["CTM_Job_" + txt_search_JobNo.Text] = "JobNo='" + ctmJob.JobNo + "'";
            this.dsJob.FilterExpression = Session["CTM_Job_" + txt_search_JobNo.Text].ToString();
            if (this.grid_job.GetRow(0) != null)
                this.grid_job.StartEdit(0);
        }
        catch { }

        return res;
    }
    protected void grid_job_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "save")
        {
            job_save();
        }
    }
    protected void grid_job_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_job.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;;
            ASPxTextBox txt_ClientName = this.grid_job.FindEditFormTemplateControl("txt_ClientName") as ASPxTextBox;
            txt_ClientName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "ClientId" }));

            ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
            ASPxButton btn_Confirm = this.grid_job.FindEditFormTemplateControl("btn_Confirm") as ASPxButton;
            string StatusCode = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "StatusCode" }), "USE");
            switch (StatusCode)
            {
                case "CLS":
                    //ASPxButton btnCLS = this.grid_job.FindEditFormTemplateControl("btn_JobClose") as ASPxButton;
                    ASPxButton btnCLS = this.grid_job.FindEditFormTemplateControl("btn_JobClose") as ASPxButton;
                    btnCLS.Text = "Open";
                    break;
                case "CNL":
                    ASPxButton btnCNL = this.grid_job.FindEditFormTemplateControl("btn_JobVoid") as ASPxButton;
                    btnCNL.Text = "UnVoid";
                    
                    break;
                default:
                    break;
            }

            EzshipHelper_Authority.Bind_Authority(this.grid_job);
            EzshipHelper_Authority.Bind_Authority(pageControl);
            
        }
    }
    private void Event_Log(string jobNo, string status, string note)
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        string userId = HttpContext.Current.User.Identity.Name;
        int jobId = SafeValue.SafeInt(Id.Text, 0);
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isJOB(jobId);
        elog.Remark = note;
        elog.log();
    }
    protected void cmb_JobStatus_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_JobStatus = this.grid_job.FindEditFormTemplateControl("cmb_JobStatus") as ASPxComboBox;
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        string sql = string.Format(@"select JobStatus from ctm_job where Id='{0}'", Id.Text);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        if (status == "Quoted")
        {
            cmb_JobStatus.Text = "Quoted";
        }
        if (status == "Confirmed")
        {
            cmb_JobStatus.Text = "Confirmed";
        }
        if (status == "Completed")
        {
            cmb_JobStatus.Text = "Completed";
        }
        if (status == "Voided")
        {
            cmb_JobStatus.Text = "Voided";
        }
    }
    #endregion

    #region Cargo
    protected void grid_wh_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsWh.FilterExpression = "JobNo='" + JobNo + "' and CargoType='IN'";
    }
    protected void grid_wh_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.JobHouse));
        }
    }
    protected void grid_wh_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        if (cbb_JobType.Text == "IMP")
        {
            e.NewValues["CargoType"] = "IN";
        }
        e.NewValues["Qty"] = 1;
        e.NewValues["ContNo"] = "";
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["JobNo"] = txt_JobNo.Text;
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);
        e.NewValues["OpsType"] = " ";
        e.NewValues["UomCode"] = " ";
        e.NewValues["PackTypeOrig"] = " ";
        e.NewValues["LandStatus"] = " ";
        e.NewValues["DgClass"] = " ";
        e.NewValues["CargoStatus"] = "P";
        e.NewValues["PackUom"] = " ";
        e.NewValues["LandStatus"] = "Normal";
        e.NewValues["DgClass"] = "Normal";
        e.NewValues["CargoStatus"] = " ";
        e.NewValues["DamagedStatus"] = "Normal";
    }
    protected void grid_wh_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        if (cbb_JobType.Text == "GR")
        {
            e.NewValues["CargoType"] = "IN";
            e.NewValues["RefNo"] = txt_JobNo.Text;
        }
        if (cbb_JobType.Text == "DO")
        {
            e.NewValues["CargoType"] = "OUT";
        }
        if (SafeValue.SafeString(e.NewValues["BookingNo"]) == "")
        {
            throw new Exception("Pls enter the Lot No");
        }
        if (SafeValue.SafeInt(e.NewValues["QtyOrig"], 0) <= 0)
        {
            throw new Exception("Pls enter the Actual's Qty");
        }
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);

        e.NewValues["JobNo"] = txt_JobNo.Text;
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        if (SafeValue.SafeDecimal(e.NewValues["LengthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["WidthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["HeightPack"]) == 0)
        {
            e.NewValues["VolumeOrig"] = SafeValue.SafeDecimal(e.NewValues["VolumeOrig"]);
        }
        else
        {
            e.NewValues["VolumeOrig"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["LengthPack"]) * SafeValue.SafeDecimal(e.NewValues["WidthPack"]) * SafeValue.SafeDecimal(e.NewValues["HeightPack"]), 3);
        }
        e.NewValues["LandStatus"] = SafeValue.SafeString(e.NewValues["LandStatus"]);
        e.NewValues["DgClass"] = SafeValue.SafeString(e.NewValues["DgClass"]);
        e.NewValues["DamagedStatus"] = SafeValue.SafeString(e.NewValues["DamagedStatus"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["HblNo"] = SafeValue.SafeString(e.NewValues["HblNo"]);
        e.NewValues["BookingNo"] = SafeValue.SafeString(e.NewValues["BookingNo"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["OpsType"] = SafeValue.SafeString(e.NewValues["OpsType"]);
        e.NewValues["UomCode"] = SafeValue.SafeString(e.NewValues["UomCode"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["SkuCode"] = SafeValue.SafeString(e.NewValues["SkuCode"]);
        e.NewValues["QtyOrig"] = SafeValue.SafeDecimal(e.NewValues["QtyOrig"]);
        e.NewValues["PackTypeOrig"] = SafeValue.SafeString(e.NewValues["PackTypeOrig"]);
        e.NewValues["WeightOrig"] = SafeValue.SafeDecimal(e.NewValues["WeightOrig"]);
        e.NewValues["PackQty"] = SafeValue.SafeDecimal(e.NewValues["PackQty"]);
    }
    protected void grid_wh_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        if (cbb_JobType.Text == "GR")
        {
            e.NewValues["CargoType"] = "IN";
        }
        if (cbb_JobType.Text == "DO")
        {
            e.NewValues["CargoType"] = "OUT";
        }
        if (SafeValue.SafeString(e.NewValues["BookingNo"]) == "")
        {
            throw new Exception("Pls enter the Lot No");
        }
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);
        ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        e.NewValues["ClientId"] = btn_ClientId.Text;
        if (SafeValue.SafeDecimal(e.NewValues["LengthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["WidthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["HeightPack"]) == 0)
        {
            e.NewValues["VolumeOrig"] = SafeValue.SafeDecimal(e.NewValues["VolumeOrig"]);
        }
        else
        {
            e.NewValues["VolumeOrig"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["LengthPack"]) * SafeValue.SafeDecimal(e.NewValues["WidthPack"]) * SafeValue.SafeDecimal(e.NewValues["HeightPack"]), 3);
        }
        e.NewValues["RefNo"] = SafeValue.SafeString(e.NewValues["RefNo"]);
        e.NewValues["LandStatus"] = SafeValue.SafeString(e.NewValues["LandStatus"]);
        e.NewValues["DgClass"] = SafeValue.SafeString(e.NewValues["DgClass"]);
        e.NewValues["DamagedStatus"] = SafeValue.SafeString(e.NewValues["DamagedStatus"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["HblNo"] = SafeValue.SafeString(e.NewValues["HblNo"]);
        e.NewValues["BookingNo"] = SafeValue.SafeString(e.NewValues["BookingNo"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["OpsType"] = SafeValue.SafeString(e.NewValues["OpsType"]);
        e.NewValues["UomCode"] = SafeValue.SafeString(e.NewValues["UomCode"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["SkuCode"] = SafeValue.SafeString(e.NewValues["SkuCode"]);
        e.NewValues["QtyOrig"] = SafeValue.SafeDecimal(e.NewValues["QtyOrig"]);
        e.NewValues["PackTypeOrig"] = SafeValue.SafeString(e.NewValues["PackTypeOrig"]);
        e.NewValues["WeightOrig"] = SafeValue.SafeDecimal(e.NewValues["WeightOrig"]);
        e.NewValues["PackQty"] = SafeValue.SafeDecimal(e.NewValues["PackQty"]);
    }
    protected void grid_wh_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int id = SafeValue.SafeInt(e.Values["Id"], 0);
        string sql = string.Format(@"select LineId from job_house where Id={0}",id);
        int lineId = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
        delete_cargo_out(lineId);
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    public string FilePath(int id)
    {
        string sql = string.Format("select top 1 FilePath from CTM_Attachment where JobNo='{0}'", id);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceQty(object client, object code, object bookingNo, object refNo, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,ClientId,SkuCode,BookingNo,Location from job_house  group by ClientId,SkuCode,BookingNo,Location) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.RefNo='{3}' and mast.Location='{4}'", client, code, bookingNo, SafeValue.SafeString(refNo), loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceSkuQty(object client, object code, object bookingNo, object refNo, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then PackQty else -PackQty end) as BalQty,ClientId,SkuCode,BookingNo,Location from job_house  group by ClientId,SkuCode,BookingNo,Location) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.RefNo='{3}' and mast.Location='{4}'", client, code, bookingNo, refNo, loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceWeight(object client, object code, object bookingNo, object refNo, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalQty,ClientId,SkuCode,BookingNo,Location from job_house  group by ClientId,SkuCode,BookingNo,Location) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.RefNo='{3}' and mast.Location='{4}'", client, code, bookingNo, refNo, loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceVolume(object client, object code, object bookingNo, object refNo, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalQty,ClientId,SkuCode,BookingNo,Location from job_house  group by ClientId,SkuCode,BookingNo,Location) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.RefNo='{3}' and mast.Location='{4}'", client, code, bookingNo, refNo, loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    protected void grid_wh_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("Uploadline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                ASPxTextBox txt_ContNo = grid.FindRowCellTemplateControl(rowIndex, null, "txt_ContNo") as ASPxTextBox;
                e.Result = txt_Id.Text + "_" + txt_ContNo.Text;
            }
        }
    }
    protected void grid_wh_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
    }
    private void delete_cargo_out(int lineId) {
        string sql = string.Format(@"delete from job_house where Id={0}",lineId);
        ConnectSql_mb.ExecuteNonQuery(sql);
    }
    public string from_loc(int lineId) {
        string res = "";
        string sql = string.Format(@"select Location from job_house where Id={0}",lineId);
        return ConnectSql_mb.ExecuteScalar(sql) ;
    }
    #endregion



}