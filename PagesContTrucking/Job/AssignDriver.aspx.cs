using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_AssignDriver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            date_SearchFromDate.Date = DateTime.Now.AddDays(-15);
            date_SearchToDate.Date = DateTime.Now.AddDays(8);
            btn_Search_Click(null, null);

        }
        OnLoad("");
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string JobNo = txt_SearchJobNo.Text.Trim();
        string ContNo = txt_SearchContainer.Text.Trim();
        //string where = "JobNo like '%" + JobNo + "%' and ContainerNo like '%" + ContNo + "%' and isnull(DriverCode,'')='' and isnull(StatusCode,'USE')='USE'";
        //dsTransport.FilterExpression = where;
        string sql = string.Format(@"select det2.*,det1.ScheduleDate from CTM_JobDet2 as det2 
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
where det2.JobNo like '%{0}%' and det2.ContainerNo like '%{1}%' and det1.StatusCode<>'Completed' and det1.StatusCode<>'Canceled' and det2.Statuscode<>'Completed' and det2.Statuscode<>'Cancel' and job.StatusCode<>'CNL'", JobNo, ContNo); 
        if (date_SearchFromDate.Date > new DateTime(1900, 1, 1))
        {
            sql += " and DATEDIFF(d,'" + date_SearchFromDate.Date + "',ScheduleDate)>=0";
        }
        if (date_SearchToDate.Date > new DateTime(1900, 1, 1))
        {
            sql += " and DATEDIFF(d,'" + date_SearchToDate.Date + "',ScheduleDate)<=0";
        }
        if (cbo_View.Value.ToString().Equals("Unassigned"))
        {
            sql += " and isnull(det2.DriverCode,'')=''";
        }
        if (cbo_View.Value.ToString().Equals("Assigned"))
        {
            sql += " and isnull(det2.DriverCode,'')<>''";
        }
        DataTable dt = ConnectSql.GetTab(sql);
        grid_Transport.DataSource = dt;
        grid_Transport.DataBind();

    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "OK")
        {
            //string DriverCode = btn_DriveCode.Text.Trim();
            //if (DriverCode.Length == 0)
            //{
            //    e.Result = "Driver Code is request";
            //    return;
            //}
            int update_error_rowcount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    if (check_Trip_Status(list[i].Id, list[i].DriverCode, list[i].Statuscode))
                    {
                        update_error_rowcount++;
                        continue;
                    }
                    string sql = string.Format(@"update CTM_JobDet2 set DriverCode='{0}',TowheadCode='{2}',CfsCode='{3}',ChessisCode='{4}',SubletFlag='{5}',SubletHauliername='{6}',BayCode='{7}' where Id='{1}'", list[i].DriverCode, list[i].jobNo, list[i].TowheadCode, list[i].CfsCode, list[i].ChessisCode, list[i].subletFlag, list[i].subletHaulierName, list[i].BayCode);
                    ConnectSql.ExecuteSql(sql);
                }
                catch { }
            }
            if (update_error_rowcount > 0)
            {
                e.Result = update_error_rowcount + " Rows Update is Error";
            }
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string jobNo = "";
        public string DriverCode = "";
        public string TowheadCode = "";
        public string CfsCode = "";
        public string ChessisCode = "";
        public string subletFlag = "";
        public string subletHaulierName = "";
        public string BayCode = "";
        public string Statuscode = "U";
        public string Id = "";

        public Record(string _jobNo, string _DriverCode, string _TowheadCode, string _CfsCode, string _ChessisCode, string _subletFlag, string _subletHaulierName, string _BayCode, string _status, string _Id)
        {
            jobNo = _jobNo;
            DriverCode = _DriverCode;
            TowheadCode = _TowheadCode;
            CfsCode = _CfsCode;
            ChessisCode = _ChessisCode;
            subletFlag = _subletFlag;
            subletHaulierName = _subletHaulierName;
            BayCode = _BayCode;
            Statuscode = _status == "" ? "U" : _status;
            Id = _Id;
        }

    }
    private void OnLoad(string driver)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxLabel jobId = this.grid_Transport.FindRowTemplateControl(i, "txt_jobId") as ASPxLabel;
            ASPxCheckBox isPay = this.grid_Transport.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            //ASPxComboBox Towhead = this.grid_Transport.FindRowTemplateControl(i, "cbb_TowheadCode") as ASPxComboBox;
            ASPxButtonEdit DriverCode = this.grid_Transport.FindRowTemplateControl(i, "btn_DriveCode") as ASPxButtonEdit;
            ASPxTextBox Towhead = this.grid_Transport.FindRowTemplateControl(i, "txt_TowheadCode") as ASPxTextBox;
            ASPxComboBox Cfs = this.grid_Transport.FindRowTemplateControl(i, "cbb_CfsCode") as ASPxComboBox;
            ASPxComboBox Chessis = this.grid_Transport.FindRowTemplateControl(i, "cbb_Chessis") as ASPxComboBox;
            ASPxComboBox subletFlag = this.grid_Transport.FindRowTemplateControl(i, "cbb_SubletFlag") as ASPxComboBox;
            ASPxTextBox subletHaulierName = this.grid_Transport.FindRowTemplateControl(i, "txt_SubletHauliername") as ASPxTextBox;
            ASPxComboBox Bay = this.grid_Transport.FindRowTemplateControl(i, "cbb_Trip_BayCode") as ASPxComboBox;
            ASPxComboBox status = this.grid_Transport.FindRowTemplateControl(i, "cbb_Trip_StatusCode") as ASPxComboBox;
            ASPxLabel Id = this.grid_Transport.FindRowTemplateControl(i, "lb_Id") as ASPxLabel;

            if (jobId != null && DriverCode != null & Towhead != null && Cfs != null && Chessis != null && subletFlag != null && subletHaulierName != null && Bay != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(jobId.Text, DriverCode.Text, Towhead.Text, Cfs.Text, Chessis.Text, subletFlag.Text, subletHaulierName.Text, Bay.Text, status.Value.ToString(), Id.Text));
            }
        }
    }

    private bool check_Trip_Status(string id, string driverCode, string status)
    {
        //if (driverCode.Trim().Length == 0)
        //{
        //    return true;
        //}
        if (status == "S" || status == "P")
        {
            string sql = string.Format(@"select COUNT(*) from CTM_JobDet2 where DriverCode='{0}' and Statuscode='{2}' and Id<>'{1}'", driverCode, id, status);
            int result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
            if (result > 0)
            {
                return true;
            }
        }
        return false;
    }
    protected void grid_Transport_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;
        ASPxButtonEdit driver = this.grid_Transport.FindRowTemplateControl(e.VisibleIndex, "btn_DriveCode") as ASPxButtonEdit;
        if (driver != null)
        {
            driver.ClientInstanceName = driver.ClientInstanceName + e.VisibleIndex;
            ASPxTextBox towhead = this.grid_Transport.FindRowTemplateControl(e.VisibleIndex, "txt_TowheadCode") as ASPxTextBox;
            towhead.ClientInstanceName = towhead.ClientInstanceName + e.VisibleIndex;
            ASPxTextBox ScheduleDate = this.grid_Transport.FindRowTemplateControl(e.VisibleIndex, "txt_ScheduleDate") as ASPxTextBox;
            driver.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupCTM_DriverLog({0},null,{1},'{2}');", driver.ClientInstanceName, towhead.ClientInstanceName, ScheduleDate.Text) + "}";
        }
    }
}