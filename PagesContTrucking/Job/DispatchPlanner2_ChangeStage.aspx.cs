using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner2_ChangeStage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ContNo = SafeValue.SafeString(Request.QueryString["ContNo"]);
            string JobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
            string Det2Id = SafeValue.SafeString(Request.QueryString["Det2Id"]);
            txt_ContNo.Text = ContNo;
            txt_JobNo.Text = JobNo;
            txt_Det2Id.Text = Det2Id;
            search_data();
        }

    }
    private void search_data()
    {
        string sql = string.Format(@"select case StageCode when 'Pending' then 1 when 'Port' then 2 when 'Park1' then 3 when 'Warehouse' then 4 when 'Park2' then 5 when 'Yard' then 6 when 'Completed' then 7 else 0 end as StageIndex 
,JobNo,ContainerNo,StageCode,ChessisCode,Convert(nvarchar,FromDate,111) as FromDate,FromTime,Remark,DriverCode,TowheadCode,Id,StageStatus,LoadCode
from CTM_JobDet2 where JobNo='{0}' and ContainerNo='{1}' order by FromDate desc,FromTime desc", txt_JobNo.Text, txt_ContNo.Text);
        DataTable dt = ConnectSql.GetTab(sql);
        this.gv.DataSource = dt;
        this.gv.DataBind();

        string driverCode = "";
        string Trailer = "";
        string Towhead = "";
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            driverCode = dr["DriverCode"].ToString();
            Trailer = dr["ChessisCode"].ToString();
            Towhead = dr["TowheadCode"].ToString();
        }
        btn_DriverCode.Text = driverCode;
        txt_TowheadCode.Text = Towhead;
        btn_ChessisCode.Text = Trailer;
    }
    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string sql = string.Format(@"delete from CTM_JobDet2 where Id='{0}'", SafeValue.SafeString(e.Values["Id"]));
        ConnectSql.ExecuteSql(sql);
        e.Cancel = true;
        search_data();
    }
    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox txt_Id = gv.FindEditRowCellTemplateControl(null, "txt_Id") as ASPxTextBox;
        string driverCode = SafeValue.SafeString(e.NewValues["DriverCode"]);
        string stage = SafeValue.SafeString(e.NewValues["StageCode"]);
        string date = SafeValue.SafeString(e.NewValues["FromDate"]);
        string remark = SafeValue.SafeString(e.NewValues["Remark"]);
        string status = SafeValue.SafeString(e.NewValues["StageStatus"]);
        string time = SafeValue.SafeString(e.NewValues["FromTime"]);
        string load = SafeValue.SafeString(e.NewValues["LoadCode"], " ");
        string Id = txt_Id.Text;
        string sql = string.Format(@"update CTM_JobDet2 set StageCode='{0}',FromDate='{1}',Remark='{2}',StageStatus='{3}',FromTime='{4}',LoadCode='{6}' where Id='{5}'", stage, date, remark, status, time, Id, load);
        ConnectSql.ExecuteSql(sql);
        e.Cancel = true;
        search_data();
        this.gv.StartEdit(-1);
    }
    protected void btn_submit_AddNewStageRow_Click(object sender, EventArgs e)
    {
        if (btn_DriverCode.Text == "")
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>addNewStageRow_result('-1');</script>");
            return;
        }
        string sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,Remark,FromCode,FromDate,FromTime,CfsCode,BayCode,SubletHauliername,SubletFlag,Statuscode,TripCode,Det1Id,StageCode,StageStatus,LoadCode,ToDate,ToTime)
(select JobNo,ContainerNo,'{1}','{2}','{3}','{4}','{5}','{6}','{7}',CfsCode,BayCode,SubletHauliername,SubletFlag,'U',TripCode,Det1Id,'{8}','{9}','{10}','{11}','{12}' from CTM_JobDet2 where Id='{0}' )", txt_Det2Id.Text, btn_DriverCode.Text, txt_TowheadCode.Text, btn_ChessisCode.Text, txt_Trip_Remark.Text, txt_Trip_FromCode.Text, txt_FromDate.Date.ToString("yyyy/MM/dd"), txt_FromTime.Text, cbb_StageCode.Value, cbb_StageStatus.Value, cbb_LoadCode.Value, txt_ToDate.Date.ToString("yyyy/MM/dd"), txt_ToTime.Text);
        int result = ConnectSql.ExecuteSql(sql);
        if (result == 1)
        {
            search_data();
        }
        ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>addNewStageRow_result('" + result + "');</script>");

    }
}