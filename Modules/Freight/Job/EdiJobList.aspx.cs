using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using C2;
using Wilson.ORMapper;
using System.Drawing;

public partial class Modules_Freight_Job_EdiJobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
            cbo_DocType.Text = "IMP";
        }
    }
    #region search
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string docType = this.cbo_DocType.Text;
        string refNo = this.txt_RefNo.Text;
        string contno = this.txt_ContNo.Text;


        DateTime date1 = this.txt_from.Date; //.ToString("yyyy-MM-dd");
        DateTime date2 = this.txt_end.Date; //.ToString("yyyy-MM-dd");

        string From = date1.Date.ToString("yyyyMMdd");
        string To = date2.Date.ToString("yyyyMMdd");

        string name = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
        string sql_p = string.Format(@"select PartyId from xxparty where Name='{0}'", name);
        string haulierId = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_p));
        //string ClientName = info["ClientName"].ToString();

        List<ConnectEdi.cmdParameters> list = new List<ConnectEdi.cmdParameters>();
        list.Add(new ConnectEdi.cmdParameters("@ClientRefNo", refNo, SqlDbType.NVarChar, 8));
        list.Add(new ConnectEdi.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectEdi.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectEdi.cmdParameters("@JobType", docType, SqlDbType.NVarChar, 8));
        list.Add(new ConnectEdi.cmdParameters("@ContNo", contno, SqlDbType.NVarChar, 8));
        list.Add(new ConnectEdi.cmdParameters("@HaulierId", haulierId, SqlDbType.NVarChar, 8));

        string sql = string.Format(@"select job.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,det1.YardAddress as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.SealNo,det1.ContainerType,job.EtaDate,job.EtaTime,job.EtdDate,
det1.UrgentInd,job.OperatorCode,job.CarrierBkgNo,det1.oogInd,det1.CfsStatus,det1.DischargeCell,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,
(select '<span class='''+det2.Statuscode+'''>'+TripCode+'</span>' from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')) as trips,
datediff(hour,isnull((select top 1 convert(nvarchar(8),det2.FromDate,112)+' '+det2.FromTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and (det2.Statuscode='S' or det2.Statuscode='C') order by det2.FromDate,det2.FromTime),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)),
case when det1.StatusCode='Completed' then isnull((select top 1 convert(nvarchar(8),det2.ToDate,112)+' '+det2.ToTime
from ctm_jobdet2 as det2 where det2.Det1Id=det1.Id and det2.Statuscode='C' order by det2.ToDate desc,det2.ToTime desc),
convert(nvarchar(8),getdate(),112)+' '+convert(nvarchar(5),getdate(),114)) else getdate() end)  as hr,
case job.JobType when 'IMP' then (case det1.StatusCode when 'New' then 'IMP' when 'InTransit' then 'RET' else '' end) 
when 'EXP' then (case det1.StatusCode when 'New' then 'COL' when 'InTransit' then 'EXP' else '' end) else '' end as NextTrip,
isnull((select ','+det2.Statuscode+':'+TripCode from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')),'')+',' as str_trips
from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo");
        string sql_where = " and job.JobStatus='Confirmed'";
        if (refNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.ClientRefNo='" + refNo + "'");
        }
        if (contno.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det1.ContainerNo='" + contno + "'");
        }
        if (docType.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobType='" + docType + "'");
        }
        if (haulierId.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.HaulierId='" + haulierId + "'");
        }
        if (sql_where.Equals(""))
        {
            sql_where = string.Format(@" datediff(d,'{0}',det1.Eta)>=0 and datediff(d,'{1}',det1.Eta)<=0", From, To);

            sql += " where job.JobType in ('IMP','EXP','LOC') and IsTrucking='Yes' and " + sql_where;

        }
        else
        {
            sql += " where  Contractor='YES'" + sql_where;
            sql += " order by job.EtaDate,job.JobNo,det1.Id,det1.StatusCode desc,job.JobDate asc";
        }
        DataTable dt = ConnectEdi.GetDataTable(sql);
        //throw new Exception(sql.ToString());
        int n = dt.Rows.Count;
        this.grd.DataSource = dt;
        this.grd.DataBind();
    }
    #endregion
    protected void grd_Det_BeforePerformDataSelect(object sender, EventArgs e)
    {
    }
    protected void grd_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string trxNo = e.Parameters;
        if (trxNo.Length > 2 && (trxNo.Substring(0, 2) == "BP"))
        {
            string doctype = trxNo.Substring(2, 3);
            string docnos = trxNo.Substring(5);

            string ret = DoBatch(docnos, doctype);
            e.Result = ret;
        }
    }
    public string DoBatch(string docnos, string doctype)
    {
        string[] docs = docnos.Split(new char[] { ',' });
        int done = 0;
        int all = 0;
        for (int i = 0; i < docs.Length; i++)
        {
            string _doc = docs[i].Trim();
            if (_doc != "")
            {
                all++;
                string[] dp = _doc.Split(new char[] { '_' });
                string _ret =C2.Edi_Freight.DoOne(dp[0]);
                if (_ret == "")
                    done++;
            }
        }

        return string.Format("Total UnPosted : {0} / {1}", done, all);
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
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
    public string xmlChangeToHtml(object par, object contId)
    {
        string res = par.ToString();
        res = res.Replace("&lt;", "<");
        res = res.Replace("&gt;", ">");
        if (res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0)
        {
            res = "<span class='X'>Trips</span>";
        }
        return res;
    }
    protected void grd_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;
        string s = SafeValue.SafeString(e.GetValue("JobNo"));
        if (s.Length > 0)
        {
            string clientRefNo = ShowRefNo(s);
            if (clientRefNo.Length > 0)
            {
                e.Row.BackColor = Color.LightBlue;
            }
        }
    }
    public string ShowRefNo(object refNo)
    {
        string sql = string.Format(@"select JobNo from ctm_job where ClientRefNo='{0}'", refNo);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
}