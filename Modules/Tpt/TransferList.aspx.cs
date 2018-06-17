using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Tpt_TransferList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["type"] != null)
            {
                search_JobType.Value = SafeValue.SafeString(Request.QueryString["type"]);
                cbb_new_jobtype.Value = SafeValue.SafeString(Request.QueryString["type"]);
                lbl_type.Text = SafeValue.SafeString(Request.QueryString["type"]);
            }
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
            txt_search_dateTo.Date = DateTime.Now;
            btn_search_Click(null, null);


            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string From = txt_search_dateFrom.Date.ToString("yyyyMMdd");
        string To = txt_search_dateTo.Date.ToString("yyyyMMdd");
        //string ContNo = txt_search_ContNo.Text;
        string JobNo = txt_search_jobNo.Text;
        string JobType = search_JobType.Text;
        string Vessel = txt_Vessel.Text;
        string Client = btn_ClientId.Text;
        string hblNo = txt_HblNo.Text.Trim();
        //string ClientName = info["ClientName"].ToString();

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo + "%", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@HblNo", hblNo, SqlDbType.NVarChar, 100));

        string sql = string.Format(@"select job.Id,job.JobNo,job.StatusCode as JobStatus,job.JobDate,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,job.EtaDate,job.EtaTime,job.EtdDate,job.OperatorCode,job.CarrierBkgNo,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,h.HblNo,WarehouseCode,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType
from CTM_Job as job left join (select top 1 * from  job_house) h on h.JobNo=job.JobNo");
        string sql_where = "";
        if (hblNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "h.HblNo=@HblNo");
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        if (sql_where.Equals(""))
        {
            if (Vessel.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.Vessel=@Vessel");
            }
            if (JobType.Length > 0 && !JobType.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "job.JobType=@JobType");
            }
            else
            {
                sql_where = GetWhere(sql_where, "job.JobType=@JobType");
            }
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }

            if (sql_where.Length > 0)
            {
                sql_where = "and " + sql_where;
            }
            sql += " where job.JobType in ('FRT') " + sql_where;

        }
        else
        {
            sql += " where " + sql_where;
            sql += " order by job.EtaDate,job.JobNo desc, job.JobDate asc";
        }
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
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        e.Result = "Fail!";
        Job_New_Save(e);
    }
    private void Job_New_Save(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        DateTime date = DateTime.Now;
        string time4 = date.ToString("HHmm");

        string jobType = SafeValue.SafeString(cbb_new_jobtype.Value, "IMP");
        string jobStatus = SafeValue.SafeString(cbb_new_jobstatus.Text, "Quoted");

        string jobType1 = jobType;

        string jobno = "";
        string user = HttpContext.Current.User.Identity.Name;
        string quoteNo = "";
        string billingType = "None";
        if (jobType1 == "TP")
        {
            billingType = "Job";
        }
        if (jobStatus == "Quoted")
        {
            quoteNo = C2Setup.GetNextNo("", "CTM_Job_" + jobStatus, txt_new_JobDate.Date);
        }
        else
        {
            jobno = C2Setup.GetNextNo("", "CTM_Job_" + jobType1, txt_new_JobDate.Date);
            quoteNo = jobno;
        }
        string wh = System.Configuration.ConfigurationManager.AppSettings["Warehosue"];
        string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType,ClientId,YardRef,PickupFrom,DeliveryTo,Remark,WarehouseAddress,JobStatus,QuoteNo,QuoteStatus,QuoteDate,WareHouseCode,BillingType) values ('{0}','{4}',getdate(),getdate(),getdate(),'USE','{1}',getdate(),'{1}',getdate(),'{2}','{2}','{3}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','Pending',getdate(),'{13}','{14}')", jobno, user, time4, cbb_new_jobtype.Value, txt_new_JobDate.Date, btn_new_ClientId.Text, "", txt_FromAddress.Text, txt_ToAddress.Text, txt_new_remark.Text, "", jobStatus, quoteNo, wh, billingType);
        int i = ConnectSql_mb.ExecuteNonQuery(sql);
        if (i == 1)
        {
            if (jobStatus == "Quoted")
            {
                C2Setup.SetNextNo("", "CTM_Job_" + jobStatus, quoteNo, txt_new_JobDate.Date);
                //GetJobRate(quoteNo, btn_new_ClientId.Text, SafeValue.SafeString(cbb_new_jobtype.Value));
                e.Result = quoteNo;
            }
            else
            {
                C2Setup.SetNextNo("", "CTM_Job_" + jobType1, jobno, txt_new_JobDate.Date);
                e.Result = jobno;
            }

        }
    }
    protected void grid_Transport_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        //if (e.DataColumn.Caption == "Job Status")
        //{
        //    string status = SafeValue.SafeString(this.grid_Transport.GetRowValues(e.VisibleIndex, "JobStatus"));
        //    if (status == "USE")
        //    {
        //        e.Cell.BackColor = System.Drawing.Color.LightGreen;
        //    }
        //    if (status == "CLS")
        //    {
        //        e.Cell.BackColor = System.Drawing.Color.Orange;
        //    }
        //    if (status == "CNL")
        //    {
        //        e.Cell.BackColor = System.Drawing.Color.Red;
        //    }
        //}
    }
    public string VilaStatus(string status)
    {
        string strStatus = "";
        if (status == "USE")
        {
            strStatus = "NEW";
        }
        if (status == "CLS")
        {
            strStatus = "COMPLATED";
        }
        if (status == "CNL")
        {
            strStatus = "CANCEL";
        }
        return strStatus;
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
        if (status == "InTransit")
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