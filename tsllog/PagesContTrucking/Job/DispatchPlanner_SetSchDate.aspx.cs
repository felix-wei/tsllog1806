using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner_SetSchDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet1));
        }
    }
    protected void grid_Transport_BeforePerformDataSelect(object sender, EventArgs e)
    {
        this.dsTransport.FilterExpression = " isnull(ScheduleDate,'1753-1-1')='1753-1-1' and (select StatusCode from CTM_Job where CTM_Job.JobNo=CTM_JobDet1.JobNo)<>'CNL' ";
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["F5Ind"] = SafeValue.SafeString(e.NewValues["F5Ind"], "");
        e.NewValues["UrgentInd"] = SafeValue.SafeString(e.NewValues["UrgentInd"], "");
        e.NewValues["PortnetStatus"] = SafeValue.SafeString(e.NewValues["PortnetStatus"], "");
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 1, 1));
        e.NewValues["RequestDate"] = SafeValue.SafeDate(e.NewValues["RequestDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsInDate"] = SafeValue.SafeDate(e.NewValues["CfsInDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsOutDate"] = SafeValue.SafeDate(e.NewValues["CfsOutDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardPickupDate"] = SafeValue.SafeDate(e.NewValues["YardPickupDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardReturnDate"] = SafeValue.SafeDate(e.NewValues["YardReturnDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"], "");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "");
    }
    protected void grid_Transport_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["ContainerNo"]) != SafeValue.SafeString(e.OldValues["ContainerNo"]))
        {
            string sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'", e.Keys["Id"], SafeValue.SafeString(e.NewValues["ContainerNo"]));
            ConnectSql.ExecuteSql(sql);
        }
    }
}