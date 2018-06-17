using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Daily_UnscheduledContainers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_EtaFrom.Date = DateTime.Now.AddDays(-7);
            search_EtaTo.Date = DateTime.Now.AddDays(3);
            btn_search_Click(null, null);
        }
        OnLoad("");
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (search_EtaFrom.Date < new DateTime(1900, 1, 1))
        {
            search_EtaFrom.Date = DateTime.Now.AddDays(-7);
        }
        if (search_EtaTo.Date < new DateTime(1900, 1, 1))
        {
            search_EtaTo.Date = DateTime.Now.AddDays(3);
        }
        string sql = string.Format(@"select det1.Id,det1.JobNo,det1.ContainerNo,det1.ContainerType,det1.SealNo,det1.Weight,det1.Volume,det1.QTY,det1.PackageType,job.EtaDate,det1.ScheduleDate
from ctm_jobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where datediff(d,det1.ScheduleDate,'1900-1-1')>0 and job.StatusCode='USE' and DATEDIFF(d,job.EtaDate,'{0}')<=0 and DATEDIFF(d,job.Etadate,'{1}')>=0", search_EtaFrom.Date, search_EtaTo.Date);
        grid_Transport.DataSource = ConnectSql.GetTab(sql);
        grid_Transport.DataBind();
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string det1Id;
        public DateTime SchDate;
        public Record(string _det1Id,DateTime _SchDate)
        {
            det1Id = _det1Id;
            SchDate = SafeValue.SafeDate(_SchDate, new DateTime(1753, 1, 1));
        }

    }
    private void OnLoad(string driver)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxLabel det1Id = this.grid_Transport.FindRowTemplateControl(i, "txt_det1Id") as ASPxLabel;
            ASPxDateEdit SchDate = this.grid_Transport.FindRowTemplateControl(i, "date_SchDate") as ASPxDateEdit;
            ASPxCheckBox isPay = this.grid_Transport.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (det1Id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(det1Id.Text,SchDate.Date));
            }
        }
    }

    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "OK")
        {
            int update_error_rows = 0;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    string sql = string.Format(@"update ctm_jobDet1 set ScheduleDate='{0}' where Id='{1}'", list[i].SchDate, list[i].det1Id);
                    int result = ConnectSql.ExecuteSql(sql);
                    if (result != 1)
                    {
                        update_error_rows++;
                    }
                }
                catch { }
            }
            btn_search_Click(null, null);
        }
    }
}