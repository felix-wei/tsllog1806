using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Daily_KDExport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_searchDate.Date = DateTime.Now;
            if (Request.QueryString["JobType"] != null )
            {
                lb_JobType.Text = Request.QueryString["JobType"].ToString();
            }
            grid_Transport_DataBind();
        }
    }
    private void grid_Transport_DataBind()
    {
        string sql = string.Format(@"select det1.*,job.Vessel,job.Voyage,job.Pol,job.Pod,job.JobType,job.Terminalcode,job.ClientId,HaulierId,dbo.fun_GetPartyName(job.ClientId) as ClientName,dbo.fun_GetPartyName(job.HaulierId) as HaulierName,job.ClientRefNo,job.EtaDate,job.EtaTime
from CTM_JobDet1 as det1 
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where datediff(d,det1.ScheduleDate,'{0}')=0 and job.StatusCode='USE' and job.JobType='{1}' ",date_searchDate.Date,lb_JobType.Text);
        this.grid_Transport.DataSource = ConnectSql.GetTab(sql);
        this.grid_Transport.DataBind();
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            string Id = e.NewValues["Id"].ToString();
            string ContainerNo = SafeValue.SafeString(e.NewValues["ContainerNo"]);
            string SealNo = SafeValue.SafeString(e.NewValues["SealNo"]);
            string ContainerType = SafeValue.SafeString(e.NewValues["ContainerType"]);
            DateTime ScheduleDate = SafeValue.SafeDate(e.NewValues["ScheduleDate"],new DateTime(1753,1,1));
            DateTime RequestDate = SafeValue.SafeDate(e.NewValues["RequestDate"],new DateTime(1753,1,1));
            DateTime CfsInDate = SafeValue.SafeDate(e.NewValues["CfsInDate"],new DateTime(1753,1,1));

            DateTime CfsOutDate = SafeValue.SafeDate(e.NewValues["CfsOutDate"],new DateTime(1753,1,1));
            DateTime YardPickupDate = SafeValue.SafeDate(e.NewValues["YardPickupDate"],new DateTime(1753,1,1));
            DateTime YardReturnDate = SafeValue.SafeDate(e.NewValues["YardReturnDate"],new DateTime(1753,1,1));
            decimal Weight = SafeValue.SafeDecimal(e.NewValues["Weight"],0);

            decimal Volume = SafeValue.SafeDecimal(e.NewValues["Volume"],0);
            int QTY = SafeValue.SafeInt(e.NewValues["QTY"],0);
            string PackageType = SafeValue.SafeString(e.NewValues["PackageType"]);
            string DgClass = SafeValue.SafeString(e.NewValues["DgClass"]);
            string PortnetStatus = SafeValue.SafeString(e.NewValues["PortnetStatus"]);

            string F5Ind = SafeValue.SafeString(e.NewValues["F5Ind"]);
            string UrgentInd = SafeValue.SafeString(e.NewValues["UrgentInd"]);
            string Remark = SafeValue.SafeString(e.NewValues["Remark"]);
            string det1Id = SafeValue.SafeString(e.NewValues["Id"]);
            string contNo_old = SafeValue.SafeString(e.OldValues["ContainerNo"]);
            string sql = string.Format(@"update CTM_JobDet1 set ContainerNo='{0}',SealNo='{8}',ContainerType='{1}',ScheduleDate='{2}',RequestDate='{3}',CfsInDate='{4}',CfsOutDate='{5}',YardPickupDate='{6}',YardReturnDate='{7}',Weight='{9}',Volume='{10}',QTY='{11}',PackageType='{12}',DgClass='{13}',PortnetStatus='{14}',F5Ind='{15}',UrgentInd='{16}',Remark='{17}' where Id={18}", ContainerNo, ContainerType, ScheduleDate, RequestDate, CfsInDate, CfsOutDate, YardPickupDate, YardReturnDate, SealNo, Weight, Volume, QTY, PackageType, DgClass, PortnetStatus, F5Ind, UrgentInd, Remark, Id);
            if (ConnectSql.ExecuteSql(sql)>0&&ContainerNo != contNo_old)
            {
                sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'",det1Id,ContainerNo);
                ConnectSql.ExecuteSql(sql);
            }
        }
        catch { }
        this.grid_Transport.CancelEdit();
        e.Cancel = true;
        grid_Transport_DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        grid_Transport_DataBind();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse(lb_JobType.Text+"_DailySchedule", true);
    }
}