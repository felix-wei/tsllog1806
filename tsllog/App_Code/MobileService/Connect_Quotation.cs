using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Connect_Quotation
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Connect_Quotation : System.Web.Services.WebService
{

    public Connect_Quotation()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod]
    public void Quotation_List_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        string QuoteNo = SafeValue.SafeString(job["no"]);
        string dateFrom = SafeValue.SafeString(job["from"]);
        string dateTo = SafeValue.SafeString(job["to"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        string sql = string.Format(@"select job.Id,job.JobNo,job.StatusCode,job.JobStatus,job.JobDate,job.YardRef as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,job.EtaDate,job.EtdDate,job.QuoteNo,job.QuoteStatus,job.QuoteDate,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,
(select top 1 Code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,
(select count(*) from XAArInvoice where MastRefNo=job.JobNo) as billed
from CTM_Job as job
where job.QuoteNo like @QuoteNo and job.QuoteDate>=@dateFrom and job.QuoteDate<@dateTo and job.QuoteNo<>'' and job.QuoteStatus<>'None' 
order by job.QuoteDate asc,job.QuoteNo desc");
        list.Add(new ConnectSql_mb.cmdParameters("@dateFrom", dateFrom, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@dateTo", dateTo, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteNo", "%" + QuoteNo + "%", SqlDbType.NVarChar, 100));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void Quotation_View_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int jobId = SafeValue.SafeInt(job["no"], 0);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@jobId", jobId, SqlDbType.Int));

        string sql = string.Format(@"select * from ctm_job where Id=@jobId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataRowToJson(dt);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void Quotation_View_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int jobId = SafeValue.SafeInt(job["Id"], 0);
        string QuoteStatus = SafeValue.SafeString(job["QuoteStatus"]);
        string ClientId = SafeValue.SafeString(job["ClientId"]);
        string EmailAddress = SafeValue.SafeString(job["EmailAddress"]);
        string ClientRefNo = SafeValue.SafeString(job["ClientRefNo"]);
        string JobDes = SafeValue.SafeString(job["JobDes"]);
        string QuoteRemark = SafeValue.SafeString(job["QuoteRemark"]);
        string TerminalRemark = SafeValue.SafeString(job["TerminalRemark"]);
        string InternalRemark = SafeValue.SafeString(job["InternalRemark"]);
        string AdditionalRemark = SafeValue.SafeString(job["AdditionalRemark"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@jobId", jobId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteStatus", QuoteStatus, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@EmailAddress", EmailAddress, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientRefNo", ClientRefNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobDes", JobDes, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@QuoteRemark", QuoteRemark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@TerminalRemark", TerminalRemark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@InternalRemark", InternalRemark, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@AdditionalRemark", AdditionalRemark, SqlDbType.NVarChar, 300));

        string sql = string.Format(@"update ctm_job set QuoteStatus=@QuoteStatus,ClientId=@ClientId,EmailAddress=@EmailAddress,ClientRefNo=@ClientRefNo,
JobDes=@JobDes,QuoteRemark=@QuoteRemark,TerminalRemark=@TerminalRemark,InternalRemark=@InternalRemark,AdditionalRemark=@AdditionalRemark  
where Id=@jobId");
        bool status=ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        Common.WriteJsonP(status, Common.StringToJson(""));
    }


    [WebMethod]
    public void Quotation_RateList_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int jobId = SafeValue.SafeInt(job["no"], 0);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@jobId", jobId, SqlDbType.Int));

        string sql = string.Format(@"select r.Id,r.ChgCode,r.Qty,r.Price,r.Unit,r.Remark from job_rate as r
left outer join ctm_job as job on job.QuoteNo=r.JobNo
where job.Id=@jobId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataTableToJson(dt);
        Common.WriteJsonP(true, context);
    }

    [WebMethod]
    public void Quotation_RateView_GetData(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int rateId = SafeValue.SafeInt(job["no"], 0);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@rateId", rateId, SqlDbType.Int));

        string sql = string.Format(@"select * from job_rate
where Id=@rateId");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string context = Common.DataRowToJson(dt);
        Common.WriteJsonP(true, context);
    }


    [WebMethod]
    public void Quotation_RateView_Save(string info)
    {
        JObject job = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Server.UrlDecode(info)));

        int rateId = SafeValue.SafeInt(job["Id"], 0);
        string ChgCode = SafeValue.SafeString(job["ChgCode"]);
        string ChgCodeDes = SafeValue.SafeString(job["ChgCodeDes"]);
        int Qty = SafeValue.SafeInt(job["Qty"], 0);
        decimal Price = SafeValue.SafeDecimal(job["Price"],0);
        string Unit = SafeValue.SafeString(job["Unit"]);
        string Remark = SafeValue.SafeString(job["Remark"]);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@rateId", rateId, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", ChgCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChgCodeDes", ChgCodeDes, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Qty", Qty, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@Price", Price, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Unit", Unit, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", Remark, SqlDbType.NVarChar, 300));

        string sql = string.Format(@"update job_rate set ChgCode=@ChgCode,ChgCodeDes=@ChgCodeDes,Qty=@Qty,Price=@Price,Unit=@Unit,Remark=@Remark
where Id=@rateId");
        bool status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        Common.WriteJsonP(status, Common.StringToJson(""));
    }

}
