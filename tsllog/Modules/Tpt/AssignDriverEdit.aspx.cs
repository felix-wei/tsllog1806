using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_AssignDriverEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            this.txt_end.Date = DateTime.Today;
            btn_Search_Click(null, null);
        }
        else
        {
            OnLoad("");
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
            where = string.Format(" and det2.FromDate >= '{0}' and det2.FromDate <'{1}'", dateFrom, dateTo);

        if (search_DriverCode.Text.Length > 0)
        {
            where += " and det2.DriverCode='" + search_DriverCode.Text + "'";
        }
        if (search_JobProgress.Text.Length > 0)
        {
            where += " and det2.Statuscode='" + SafeValue.SafeString(search_JobProgress.Value) + "'";
        }

        //string sql = string.Format(@"select Id,JobNo,JobType,BkgDate,BkgTime,JobProgress,TptDate,TptTime,TptType,Driver,VehicleNo,TripCode,Wt,M3,Qty,PickFrm1,DeliveryTo1 from tpt_job where JobProgress='' and isnull(driver,'')='' and StatusCode<>'CNL' {0} order by delivery_postcode", where);
        string sql = string.Format(@"select det2.Id,det2.JobNo,ClientRefNo as BkgRef,'' as SaleOrder,job.WarehouseAddress as Cust,isnull(det2.Statuscode,'') as JobProgress,
det2.FromDate as TptDate,det2.FromTime as TptTime,det2.DriverCode as Driver,0 as DeliveryIndex,
job.ClientId as Delivery_Name,ClientContact as Delivery_Contact,det2.ToCode as Delivery_Address,det2.Remark as CargoMkg,'' as Delivery_Timeslot,
'' as Delivery_PostCode,Qty
from ctm_jobdet2 as det2
left outer join ctm_job as job on det2.JobNo=job.JobNo 
where job.StatusCode<>'CNL' and job.JobType in ('WDO','WGR','TPT') {0}
order by TptDate,JobProgress", where);
        DataTable dt = ConnectSql.GetTab(sql);
        grid_Transport.DataSource = dt;
        grid_Transport.DataBind();
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "OK")
        {
            int update_error_rowcount = 0;
            string user = HttpContext.Current.User.Identity.Name;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    Record r = list[i];
                    List<ConnectSql_mb.cmdParameters> par_list = new List<ConnectSql_mb.cmdParameters>();
                    par_list.Add(new ConnectSql_mb.cmdParameters("@Id", r.Id, SqlDbType.Int));
                    par_list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", r.driver, SqlDbType.NVarChar, 100));
                    par_list.Add(new ConnectSql_mb.cmdParameters("@TptDate", r.TptDate, SqlDbType.NVarChar, 10));
                    par_list.Add(new ConnectSql_mb.cmdParameters("@TptTime", r.TptTime, SqlDbType.NVarChar, 10));
                    par_list.Add(new ConnectSql_mb.cmdParameters("@Statuscode", r.progress_new, SqlDbType.NVarChar, 10));
                    string sql_where = "";
                    if (r.progress_new=="P"||r.progress_new=="S")
                    {
                        sql_where = ",FromDate=@TptDate,FromTime=@TptTime";
                    }
                    if(r.progress_new == "C")
                    {
                        sql_where = ",ToDate=@TptDate,ToTime=@TptTime";
                    }
                    string sql = string.Format(@"update CTM_JobDet2 set DriverCode=@DriverCode,Statuscode=@Statuscode{0} where Id=@Id",sql_where);
                    // string.Format(@"update tpt_job set Driver='{1}',VehicleNo='{2}' where Id='{0}'", id, driver, progress);
                    //if (r.progress.Length == 0 && r.driver.Length > 0 && r.progress_new.Length == 0)
                    //{
                    //    sql = string.Format(@"update tpt_job set Driver='{1}',JobProgress='Confirmed',DeliveryIndex='{2}',TptTime='{3}',TptDate='{4}' where Id='{0}'", r.Id, r.driver, r.index, r.TptTime, r.TptDate);
                    //}
                    //else
                    //{
                    //    sql = string.Format(@"update tpt_job set Driver='{1}',DeliveryIndex='{2}',TptTime='{3}',JobProgress='{4}',TptDate='{5}' where Id='{0}'", r.Id, r.driver, r.index, r.TptTime, r.progress_new, r.TptDate);
                    //}
                    ConnectSql_mb.ExecuteNonQuery(sql, par_list);
                    C2.CtmJobDet2.tripStatusChanged(SafeValue.SafeInt(r.Id, 0));
                    C2.CtmJobEventLog lg = new C2.CtmJobEventLog();
                    lg.Platform_isWeb();
                    lg.Controller = user;
                    lg.setActionLevel(SafeValue.SafeInt(r.Id, 0), CtmJobEventLogRemark.Level.Trip, 4, ":" + r.progress_new);
                    lg.log();
                }
                catch
                {
                    update_error_rowcount++;
                }
            }
            if (update_error_rowcount > 0)
            {
                e.Result = update_error_rowcount + " Rows Save Error";
            }
            else
            {
                btn_Search_Click(null, null);
            }
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string driver { get; set; }
        public string Id { get; set; }
        public string progress { get; set; }
        public string progress_new { get; set; }
        public int index { get; set; }
        public string TptTime { get; set; }
        public string TptDate { get; set; }


        public Record(string _Id, string _DriverCode, string _progress, int _index)
        {
            Id = _Id;
            driver = _DriverCode;
            progress = _progress;
            index = _index;
        }

    }
    private void OnLoad(string t)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns[0], "ack_IsPay") as ASPxCheckBox;
            //ASPxButtonEdit driver = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "btn_DriverCode") as ASPxButtonEdit;
            ASPxComboBox driver = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "btn_DriverCode") as ASPxComboBox;
            //ASPxButtonEdit towhead = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["VehicleNo"], "btn_vehicle") as ASPxButtonEdit;
            ASPxTextBox Id = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "lb_Id") as ASPxTextBox;
            ASPxTextBox lb_progress = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "lb_progress") as ASPxTextBox;
            ASPxComboBox cbb_JobProgress = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["JobProgress"], "cbb_JobProgress") as ASPxComboBox;
            //ASPxTextEdit txt_DeliveryIndex = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["DeliveryIndex"], "txt_DeliveryIndex") as ASPxTextEdit;
            ASPxTextBox txt_TptTime = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["TptTime"], "txt_TptTime") as ASPxTextBox;
            ASPxDateEdit date_TptDate = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["TptDate"], "date_TptDate") as ASPxDateEdit;

            if (Id != null && driver != null & lb_progress != null && isPay != null && isPay.Checked)
            {
                Record r = new Record(Id.Text, SafeValue.SafeString(driver.Value), lb_progress.Text, 0);
                r.TptTime = SafeValue.SafeString(txt_TptTime.Text, "00:00");
                r.TptDate = SafeValue.SafeDate(date_TptDate.Date, new DateTime(1900, 1, 1)).ToString("yyyyMMdd");
                r.progress_new = SafeValue.SafeString(cbb_JobProgress.Value);
                list.Add(r);
            }
            else
            {
                if (Id == null)
                {
                    break;
                }
            }
        }
    }

    protected void grid_Transport_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

        //ASPxButtonEdit driver = this.grid_Transport.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "btn_DriverCode") as ASPxButtonEdit;
        //if (driver != null)
        //{
        //    driver.ClientInstanceName = driver.ClientInstanceName + e.VisibleIndex;
        //    ASPxButtonEdit towhead = this.grid_Transport.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["VehicleNo"], "btn_vehicle") as ASPxButtonEdit;
        //    towhead.ClientInstanceName = towhead.ClientInstanceName + e.VisibleIndex;
        //    driver.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupCTM_Driver({0},null,{1},null);", driver.ClientInstanceName, towhead.ClientInstanceName) + "}";
        //    towhead.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupCTM_MasterData({0},null,'Towhead');", towhead.ClientInstanceName) + "}";
        //}
    }
}