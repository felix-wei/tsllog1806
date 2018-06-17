﻿using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Tpt_PendingContainerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string ContNo = txt_search_ContNo.Text;
        //bool ContStauts_N = cb_ContStatus1.Checked;
        //bool ContStauts_Int = cb_ContStatus2.Checked;
        //bool ContStauts_C = cb_ContStatus3.Checked;
        //bool ContStatus_UnC = cb_ContStatus4.Checked;
        string JobNo = txt_search_jobNo.Text;
        string JobType = search_JobType.Text;
        //string Vessel = txt_Vessel.Text;
        string Client = btn_ClientId.Text;
        //string NextTrip = SafeValue.SafeString(search_NextTrip.Value);
        string WhStatus = SafeValue.SafeString(cbb_warehouseStatus.Text);
        string Warehouse = SafeValue.SafeString(txt_WareHouseId.Text);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@WhStatus", WhStatus, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@NextTrip", NextTrip, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Warehouse", Warehouse, SqlDbType.NVarChar, 100));


        string sql_where = "";
        if (ContNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det1.ContainerNo like @ContNo");
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        if (sql_where.Equals(""))
        {
            sql_where = GetWhere(sql_where, "isnull(Stuff_Ind,'Yes')='Yes' ");
            if (JobType.Length > 0 && !JobType.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "job.JobType=@JobType");
            }
            else
            {
                sql_where = GetWhere(sql_where, "job.JobType in ('IMP','EXP','WGR','WDO','TPT')");
            }
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }
            if (WhStatus.Length > 0 && !WhStatus.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "det1.CfsStatus=@WhStatus");
            }
            else
            {
                sql_where = GetWhere(sql_where, "det1.CfsStatus in ('Pending')");
            }
        }
        if (Warehouse.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.WareHouseCode=@Warehouse");
        }
        else
        {
            sql_where = GetWhere(sql_where, "job.WareHouseCode<>''");
        }

        string sql = string.Format(@"select *,
(select top 1 FromDate from CTM_JobDet2 where t.Id=Det1Id and TripCode=(case t.JobType when 'WGR' then 'LOC' when 'WDO' then 'SHF' end)) as DateIn,
(select top 1 ToDate from CTM_JobDet2 where t.Id=Det1Id and TripCode=(case t.JobType when 'WGR' then 'SHF' when 'WDO' then 'LOC' end)) as DateOut
from (
select det1.Id,det1.ContainerNo,det1.ContainerType,det1.SealNo,det1.JobNo,job.JobType,job.ClientId,det1.CfsStatus,det1.StatusCode,det1.ScheduleDate,
getdate() as ScheduleStartDate,ScheduleStartTime,
case when YEAR(CompletionDate)<2010 then '' else convert(nvarchar(10),CompletionDate,103)+' '+CompletionTime end as CompletionDate
from CTM_JobDet1 as det1
left outer join ctm_job as job on det1.JobNo=job.JobNo
where {0}
) as t", sql_where);
        sql += " order by ClientId,ContainerNo,JobNo";

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        //throw new Exception(sql.ToString());
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();


    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_save2excel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("PendingSchedule_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), true);
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string temp = e.Parameters;
        string[] ar = temp.Split('_');
        if (ar.Length == 3)
        {
            if (ar[0] == "UpdateInlineStatus")
            {
                contCfsStatusChange(sender, e, SafeValue.SafeInt(ar[1], -1), ar[2]);
            }
        }
    }
    private void contCfsStatusChange(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex, string CfsStatus)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }

        ASPxLabel lb_id = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lb_Id") as ASPxLabel;
        ASPxLabel lbl_JobNo = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_JobNo") as ASPxLabel;
        ASPxDateEdit txt_search_dateFrom = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_search_dateFrom") as ASPxDateEdit;

        ASPxTextBox date_ScheduleStartTime = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "date_ScheduleStartTime") as ASPxTextBox;
        string res = "Save Error";
        string StatusCode = "";
        if (lb_id != null)
        {
            if (txt_search_dateFrom.Date> new DateTime(1900, 1, 1))
            {
                string sql0 = ",ScheduleStartDate=@ScheduleStartDate,ScheduleStartTime=@ScheduleStartTime";
                //if (CfsStatus == "Scheduled")
                //StatusCode == "";

                string sql = string.Format(@"update CTM_JobDet1 set CfsStatus=@ContStatus{0} 
where Id=@Oid", sql0);
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@Oid", lb_id.Text, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@ContStatus", CfsStatus, SqlDbType.NVarChar));
                //list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", StatusCode, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@ScheduleStartDate", txt_search_dateFrom.Date.ToString("yyyy-MM-dd"), SqlDbType.DateTime));
                list.Add(new ConnectSql_mb.cmdParameters("@ScheduleStartTime", date_ScheduleStartTime.Text, SqlDbType.NVarChar));

                if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
                {
                    res = "Save successful!";
                    //C2.CtmJobDet1.contWarehouseStatusChanged(SafeValue.SafeInt(lb_id.Text, 0));
                }
            }
            else {
                res = "Pls select the Schedule Date";
            }
            
        }
        e.Result = res;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "Start" || status == "Arrival"
            || status == "LCL-MT" || status == "LCL-LD"
            || status == "Depart" || status == "Delivered" || status == "Returned")
        {
            color = "green";
        }
        if (status == "Import" || status == "Collection"
    || status == "Customer-MT" || status == "Customer-LD"
    || status == "WHS-LD" || status == "WHS-MT"
    || status == "Return" || status == "Export")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }
        return color;
    }
}