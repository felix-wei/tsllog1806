using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using C2;

/// <summary>
/// Collinsmovers 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect : System.Web.Services.WebService
{
	public Connect()
	{
		//如果使用设计的组件，请取消注释以下行 
		//InitializeComponent(); 

		//================================JOject j=new JOject(str);  str不能是json string数组
		//JArray ja = (JArray)JsonConvert.DeserializeObject(str);
		//JObject jo = (JObject)JsonConvert.DeserializeObject(str);
	}

	#region User
	//[WebMethod]
	//public void User_Regist(string info)
	//{
	//    JObject j_info = JObject.Parse(info);
	//    string result = "false";
	//    string sql = string.Format(@"select count(*) from mom_user where user_login='{0}'", j_info["username"]);
	//    if (SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0) > 0)
	//    {
	//        result = "exist";
	//    }
	//    else
	//    {
	//        sql = string.Format(@"insert into mom_user (user_login,user_password,mobile_no) values ('{0}','{1}','{2}')", j_info["username"], j_info["password"], j_info["mobile_No"]);
	//        int i = ConnectSql_mb.ExecuteNonQuery(sql);
	//        if (i == 1)
	//        {
	//            result = "success";
	//        }
	//    }
	//    string json = Common.StringToJson(result);
	//    Common.WriteJsonP(json);
	//}

	[WebMethod]
	public void User_Login(string info)
	{
		JObject j_info = JObject.Parse(info);
		string result = "false";
		bool ok = false;
		string sql = string.Format(@"select * from [User] where name='{0}'", j_info["mobile_no"]);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		if (dt.Rows.Count < 1)
		{
			result = "inexistence";
		}
		else
		{
			Encryption.EncryptClass encrypt = new Encryption.EncryptClass();
			if (SafeValue.SafeString(encrypt.DESEnCode(j_info["mobile_no"].ToString(), j_info["user_password"].ToString()), "") == dt.Rows[0]["Pwd"].ToString())
			{
				ok = true;
			}
			else
			{
				result = "passworderror";
			}
		}

		string json = "";
		if (ok)
		{
			json = Common.DataRowToJson(dt);
		}
		else
		{
			json = Common.StringToJson(result);
		}

		Common.WriteJsonP(ok, json);
	}

	[WebMethod]
	public void User_Login_GetControl(string Role)
	{
		string sql = string.Format(@"select Code,Type From Mobile_Control where RoleName='{0}' and IsActive=1", Role);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	#endregion

    #region haulier Job

    [WebMethod]
	public void FCL_JobView_AddAction(string info, string loc)
	{
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
		l.JobNo = job["JobNo"].ToString();
		l.Remark = job["Action"] + " (" + job["User"] + ")";
		l.Controller = job["User"].ToString();
		l.Lat = jl["Lat"].ToString();
		l.Lng = jl["Lng"].ToString();
		l.Job_Detail_EventLog_Add();
		Common.WriteJsonP(true, Common.StringToJson(""));
	}

    [WebMethod]
    public void FCL_JobView_ChangeStatus(string info, string loc)
    {
        JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"update CTM_JobDet2 set Statuscode=@Statuscode where Id=@Id");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Statuscode", job["Status"], SqlDbType.NVarChar, 10);
        list.Add(cpar);
        if (!ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {
            context = Common.StringToJson("Save Error");
        }
        else
        {
            sql = string.Format(@"select * From CTM_JobDet2 where Id=@Id");
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                string t_status = job["Status"].ToString();
                switch (t_status)
                {
                    case "S":
                        t_status = "Start";
                        break;
                    case "C":
                        t_status = "End";
                        break;
                    default: break;
                }
                DataRow dr = dt.Rows[0];
                C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
                l.JobNo = dr["JobNo"].ToString();
                l.ContainerNo = dr["ContainerNo"].ToString();
                l.Driver = dr["DriverCode"].ToString();
                l.Remark = dr["ContainerNo"] + " trip " + t_status + " (" + job["User"] + ")";
                l.Controller = job["User"].ToString();
                l.Lat = jl["Lat"].ToString();
                l.Lng = jl["Lng"].ToString();
                l.Job_Detail_EventLog_Add();

            }
        }
        Common.WriteJsonP(status, context);
    }

	[WebMethod]
	public void FCL_JobView_GetData(string Id)
	{
		string sql = string.Format(@"select * From CTM_JobDet2 where Id={0}", Id);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		bool status = true;
		string context = Common.DataRowToJson(dt);
		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void FCL_Calendar_GetDataList(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");
		string sql = string.Format(@"select Id,JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Price
from CTM_JobDet2 where DATEDIFF(day,FromDate,@date)=0 or  DATEDIFF(day,ToDate,@date)=0
order by FromTime");

		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@user", job["user"], SqlDbType.NVarChar, 20);
		list.Add(cpar);
		DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
		context = Common.DataTableToJson(dt);
		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Schedule_Get3Weekdays(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");

		string sql = string.Format(@"with tb_today as (
select @date as today,DATEPART(weekday,@date) as wd
),
tb_21days as (
select top 21 ROW_NUMBER()over(order by Id) as Id from sysobjects
),
tb_days as (
select Id,dateadd(day,Id-wd-7,today) as [date] from tb_21days as day21 inner join tb_today as day2 on 1=1
)
select *,DATEPART(day,[date]) as [day],
case DATEDIFF(day,getdate(),[date]) when 0 then 1 else 0 end as isToday,
case DATEDIFF(week,@date,[date]) when 0 then 1 else 0 end as currentWeek 
from tb_days");
		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@date", job["date"], SqlDbType.NVarChar, 20);
		list.Add(cpar);
		DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
		if (dt.Rows.Count > 0)
		{
			context = Common.DataTableToJson(dt);
		}
		else
		{
			status = "0";
			context = Common.StringToJson("Data Error");
		}

		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Job_GetDataList(string search)
	{
		string where = "";
		if (search.Trim().Length == 0)
		{
			where = "and DATEDIFF(day,jobdate,getdate())<10";
		}
		else
		{
			where = "and JobNo like '" + search + "%'";
		}

		string sql = string.Format(@"select top 30 JobNo,JobDate,Vessel,Voyage,Pol,Pod,EtaDate,EtaTime,EtdDate,EtdTime,CarrierId,ClientId,HaulierId
From CTM_Job 
where StatusCode='USE' {0} 
order by JobDate desc", where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Job_New_Save(string info, string user, string loc)
	{
		JObject job = JObject.Parse(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		DateTime date = DateTime.Now;
		string time4 = date.ToString("hhss");
		string jobno = C2Setup.GetNextNo("", "CTM_Job", date);
		string sql = string.Format(@"insert into CTM_Job (JobNo,JobDate,EtaDate,EtdDate,CodDate,StatusCode,CreateBy,CreateDatetime,UpdateBy,UpdateDatetime,EtaTime,EtdTime,JobType) values ('{0}',getdate(),getdate(),getdate(),getdate(),'USE','{1}',getdate(),'{1}',getdate(),'{2}','{2}','{3}')", jobno, user, time4, job["JobType"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo("", "CTM_Job", jobno, date);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jobno;
			l.Remark = "job created by " + user;
			l.Controller = user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string json = Common.StringToJson(jobno);
		Common.WriteJsonP(i, json);
	}

	[WebMethod]
	public void Job_Detail_GetData(string JobNo)
	{
		string sql = string.Format(@"select * from ctm_job where JobNo='{0}'", JobNo);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string info = Common.DataRowToJson(dt);
		//string info = Common.ObjectToJson(dt);

		sql = string.Format(@"select * from CTM_JobDet1 where JobNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string container = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from CTM_JobDet2 where JobNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string trip = Common.DataTableToJson(dt);

		sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i1 = 0; i1 < dt.Rows.Count; i1++)
		{
			dr = dt.Rows[i1];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string activity = Common.DataTableToJson(dt);

		sql = string.Format(@"select  Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where RefNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["FileType"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string attachment = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from CTM_Attachment where RefNo='{0}' and FileType='Signature' and FileNote='PickupSignature'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string PickupSignature = Common.DataRowToJson(dt, true);
		sql = string.Format(@"select * from CTM_Attachment where RefNo='{0}' and FileType='Signature' and FileNote='DeliverySignature'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string DeliverySignature = Common.DataRowToJson(dt, true);
		string Signature = string.Format(@"{0}PickupSignature:{2},DeliverySignature:{3}{1}", "{", "}", PickupSignature, DeliverySignature);

		sql = string.Format(@"select * from CTM_JobCharge where JobNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string charge = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from XAArInvoice where MastRefNo='{0}' and MastType='CTM' order by DocType", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string billing = Common.DataTableToJson(dt);

		sql = string.Format(@"select * From CTM_Job_Stock where JobNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string stock = Common.DataTableToJson(dt);

		string json = string.Format(@"{0}info:{2},container:{3},trip:{4},attachment:{5},signature:{6},charge:{7},billing:{8},stock:{9},activity:{10}{1}", "{", "}", info, container, trip, attachment, Signature, charge, billing, stock, activity);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Job_Detail_Info_Save(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string EtdTime = jo["EtdTime"].ToString().Replace(":", "");
		string EtaTime = jo["EtaTime"].ToString().Replace(":", "");
		string sql = string.Format(@"update ctm_job set JobType='{2}',Pol='{3}',Pod='{4}',Vessel='{5}',Voyage='{6}',EtdDate='{7}',EtdTime='{8}',EtaDate='{9}',EtaTime='{10}',ClientId='{11}',CarrierId='{12}',HaulierId='{13}',PickupFrom='{14}',DeliveryTo='{15}' where Id='{0}' and JobNo='{1}'", jo["Id"], jo["JobNo"], jo["JobType"], jo["Pol"], jo["Pod"], jo["Vessel"], jo["Voyage"], jo["EtdDate"], EtdTime, jo["EtaDate"], EtaTime, jo["ClientId"], jo["CarrierId"], jo["HaulierId"], jo["PickupFrom"], jo["DeliveryTo"]);
		string result = "0";
		if (ConnectSql_mb.ExecuteNonQuery(sql) == 1)
		{
			result = "1";
		}

		string json = Common.StringToJson(result);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Job_Detail_Container_GetNew()
	{
		string sql = string.Format(@"select * from CTM_JobDet1 where Id=-1");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string info = Common.DataRowToJson(dt, true);

		Common.WriteJsonP(info);
	}

	[WebMethod]
	public void Job_Detail_Container_Save(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		JToken Id = jo["Id"];
		string sql = "";
		if (Id == null || Id.ToString() == "")
		{
			sql = string.Format(@"insert into CTM_JobDet1 (JobNo,ContainerNo,ContainerType,SealNo,Weight,Volume,QTY,PackageType,RequestDate,ScheduleDate,CfsInDate,CfsOutDate,YardPickupDate,YardReturnDate,F5Ind,UrgentInd,StatusCode,CdtDate,YardExpiryDate)
values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),getdate())", jo["JobNo"], jo["ContainerNo"], jo["ContainerType"], jo["SealNo"], jo["Weight"], jo["Volume"], jo["QTY"], jo["PackageType"]);
		}
		else
		{
			sql = string.Format(@"update CTM_JobDet1 set ContainerNo='{1}',ContainerType='{2}',SealNo='{3}',Weight='{4}',Volume='{5}',QTY='{6}',PackageType='{7}' where Id='{0}'", Id.ToString(), jo["ContainerNo"], jo["ContainerType"], jo["SealNo"], jo["Weight"], jo["Volume"], jo["QTY"], jo["PackageType"]);
		}

		string result = "0";
		string context = "Error";
		if (ConnectSql_mb.ExecuteNonQuery(sql) == 1)
		{
			result = "1";
			sql = string.Format(@"select * from CTM_JobDet1 where JobNo='{0}'", jo["JobNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			context = Common.DataTableToJson(dt);

			if (Id == null || Id.ToString() == "")
			{
				C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
				l.JobNo = jo["JobNo"].ToString();
				l.ContainerNo = jo["ContainerNo"].ToString();
				l.Remark = "Add new Container " + jo["ContainerNo"] + " by " + user;
				l.Controller = user;
				l.Lat = jl["Lat"].ToString();
				l.Lng = jl["Lng"].ToString();
				l.Job_Detail_EventLog_Add();
			}
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void Job_Detail_Trip_GetNew()
	{
		string sql = string.Format(@"select * from CTM_JobDet2 where Id=-1");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string info = Common.DataRowToJson(dt, true);
		Common.WriteJsonP(info);
	}

	[WebMethod]
	public void Job_Detail_Trip_Save(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		JToken Id = jo["Id"];
		string No = "0";
		string result = "0";
		string sql = "";
		string driver_temp = "";
		if (Id == null || Id.ToString() == "")
		{
			sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,Price) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','B1','N','Pengding','Pengding','{13}')
select Id from CTM_JobDet2 where Id=@@IDENTITY", jo["JobNo"], jo["ContainerNo"], jo["DriverCode"], jo["TowheadCode"], jo["ChessisCode"], jo["FromCode"], jo["FromDate"], jo["FromTime"], jo["ToCode"], jo["ToDate"], jo["ToTime"], jo["Det1Id"], jo["Statuscode"], jo["Price"]);
			No = ConnectSql_mb.ExecuteScalar(sql);
			if (No.Length > 0 && !No.Equals("0"))
			{
				result = "1";
			}
		}
		else
		{
			sql = string.Format(@"select DriverCode from CTM_JobDet2 where Id='{0}'", Id.ToString());
			driver_temp = ConnectSql_mb.ExecuteScalar(sql);
			sql = string.Format(@"update CTM_JobDet2 set ContainerNo='{1}',DriverCode='{2}',TowheadCode='{3}',ChessisCode='{4}',FromCode='{5}',FromDate='{6}',FromTime='{7}',ToCode='{8}',ToDate='{9}',ToTime='{10}',Det1Id='{11}',Statuscode='{12}',Price='{13}' where Id='{0}'", Id.ToString(), jo["ContainerNo"], jo["DriverCode"], jo["TowheadCode"], jo["ChessisCode"], jo["FromCode"], jo["FromDate"], jo["FromTime"], jo["ToCode"], jo["ToDate"], jo["ToTime"], jo["Det1Id"], jo["Statuscode"], jo["Price"]);
			if (ConnectSql_mb.ExecuteNonQuery(sql) == 1)
			{
				result = "1";
				No = Id.ToString();
			}
		}

		string context = "Error";
		if (result.Equals("1"))
		{
			result = No;
			sql = string.Format(@"select * from CTM_JobDet2 where JobNo='{0}'", jo["JobNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			context = Common.DataTableToJson(dt);

			if (Id == null || Id.ToString() == "")
			{
				C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
				l.JobNo = jo["JobNo"].ToString();
				l.ContainerNo = jo["ContainerNo"].ToString();
				l.Trip = jo["JobNo"] + "." + jo["ContainerNo"];
				l.Driver = jo["DriverCode"].ToString();
				l.Towhead = jo["TowheadCode"].ToString();
				l.Trail = jo["ChessisCode"].ToString();
				l.Remark = "Add new Trip " + l.Trip + " by " + user;;
				l.Controller = user;
				l.Lat = jl["Lat"].ToString();
				l.Lng = jl["Lng"].ToString();
				l.Job_Detail_EventLog_Add();
			}
			else
			{
				if (!jo["DriverCode"].ToString().Equals(driver_temp))
				{
					C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
					l.JobNo = jo["JobNo"].ToString();
					l.ContainerNo = jo["ContainerNo"].ToString();
					l.Trip = jo["JobNo"] + "." + jo["ContainerNo"];
					l.Driver = jo["DriverCode"].ToString();
					l.Towhead = jo["TowheadCode"].ToString();
					l.Trail = jo["ChessisCode"].ToString();
					l.Remark = "Trip " + l.Trip + " assign to " + jo["DriverCode"];
					l.Controller = user;
					l.Lat = jl["Lat"].ToString();
					l.Lng = jl["Lng"].ToString();
					l.Job_Detail_EventLog_Add();
				}
			}
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void Job_Detail_Attachment_Add(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"insert into CTM_Attachment (JobType,RefNo,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values('{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}')", jo["JobType"], jo["RefNo"], jo["FileType"], jo["FileName"], jo["FilePath"], jo["CreateBy"], jo["FileNote"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string result = "0";
		string context = "Error";
		if (i == 1)
		{
			result = "1";
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["RefNo"].ToString();
			l.Note1 = jo["FilePath"].ToString();
			l.Note1Type = jo["FileType"].ToString();
			l.Controller = user;
			l.Remark = jo["CreateBy"] + " post a " + jo["FileType"] + " file";
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();

			sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", jo["RefNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			DataRow dr = null;
			for (int i1 = 0; i1 < dt.Rows.Count; i1++)
			{
				dr = dt.Rows[i1];
				if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
				{
					string path = dr["FilePath"].ToString();
					path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
					dr["FilePath"] = path;
				}
			}

			string activity = Common.DataTableToJson(dt);

			sql = string.Format(@"select  Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where RefNo='{0}'", jo["RefNo"]);
			dt = ConnectSql_mb.GetDataTable(sql);
			for (int i1 = 0; i1 < dt.Rows.Count; i1++)
			{
				dr = dt.Rows[i1];
				if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["FileType"].ToString(), 500))
				{
					string path = dr["FilePath"].ToString();
					path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
					dr["FilePath"] = path;
				}
			}

			string attachment = Common.DataTableToJson(dt);

			context = string.Format(@"{0}attachment:{2},activity:{3}{1}", "{", "}", attachment, activity);
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void Job_Detail_Activity_Add_Text(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
		l.JobNo = jo["RefNo"].ToString();
		l.Note1 = jo["FilePath"].ToString();
		l.Note1Type = jo["FileType"].ToString();
		l.Controller = user;
		l.Remark = jo["FileNote"].ToString();
		l.Lat = jl["Lat"].ToString();
		l.Lng = jl["Lng"].ToString();
		l.Job_Detail_EventLog_Add();

		string sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", jo["RefNo"]);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i1 = 0; i1 < dt.Rows.Count; i1++)
		{
			dr = dt.Rows[i1];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Job_Detail_Activity_Add_Music(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);

		string newFileName = DateTime.Now.Ticks.ToString();
		string oldFileName = jo["FileName"].ToString();
		string[] ar_temp = oldFileName.Split('.');
		string sql_part = "";
		if (ar_temp.Length == 2)
		{
			newFileName = newFileName + "." + ar_temp[1];
			string oldFilePath = jo["FilePath"].ToString();
			if (RebuildImage.File_Exist(oldFilePath))
			{
				RebuildImage.File_ReName(oldFilePath, newFileName);
				C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
				l.JobNo = jo["RefNo"].ToString();
				l.Note1 = l.JobNo + "/" + newFileName; //jo["FilePath"].ToString();
				l.Note1Type = jo["FileType"].ToString();
				l.Controller = user;
				l.Remark = jo["CreateBy"] + " post a " + jo["FileType"] + " file";
				l.Lat = jl["Lat"].ToString();
				l.Lng = jl["Lng"].ToString();
				l.Job_Detail_EventLog_Add();
			}
		}

		string sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", jo["RefNo"] + sql_part);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i1 = 0; i1 < dt.Rows.Count; i1++)
		{
			dr = dt.Rows[i1];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Job_Detail_Signature_Add(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"select * from CTM_Attachment where RefNo='{0}' and FileType='Signature' and FileNote='{1}'", jo["RefNo"], jo["FileNote"]);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		if (dt.Rows.Count > 0)
		{
			sql = string.Format(@"update CTM_Attachment set FileName='{2}',FilePath='{3}',CreateBy='{4}',CreateDateTime=getdate() where RefNo='{0}' and FileType='Signature' and FileNote='{1}'", jo["RefNo"], jo["FileNote"], jo["FileName"], jo["FilePath"], jo["CreateBy"]);
		}
		else
		{
			sql = string.Format(@"insert into CTM_Attachment (JobType,RefNo,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values('{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}')", jo["JobType"], jo["RefNo"], jo["FileType"], jo["FileName"], jo["FilePath"], jo["CreateBy"], jo["FileNote"]);
		}

		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string result = "0";
		string context = "Error";
		if (i == 1)
		{
			result = "1";
			sql = string.Format(@"select * from CTM_Attachment where RefNo='{0}' and FileType='Signature' and FileNote='PickupSignature'", jo["RefNo"]);
			dt = ConnectSql_mb.GetDataTable(sql);
			string PickupSignature = Common.DataRowToJson(dt, true);
			sql = string.Format(@"select * from CTM_Attachment where RefNo='{0}' and FileType='Signature' and FileNote='DeliverySignature'", jo["RefNo"]);
			dt = ConnectSql_mb.GetDataTable(sql);
			string DeliverySignature = Common.DataRowToJson(dt, true);
			string Signature = string.Format(@"{0}PickupSignature:{2},DeliverySignature:{3}{1}", "{", "}", PickupSignature, DeliverySignature);
			context = Signature;

			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["RefNo"].ToString();
			l.Remark = jo["FileNote"] + " by " + jo["CreateBy"];
			l.Controller = jo["CreateBy"].ToString();
			l.Note1 = jo["FilePath"].ToString();
			l.Note1Type = jo["FileType"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void Job_Detail_Charge_Add(string JobNo, string user, string loc)
	{
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string status = "0";
		string context = "";
		DataTable dt = null;
		string sql = string.Format(@"select count(*) from CTM_JobCharge where JobNo='{0}'", JobNo);
		int cc = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
		if (cc == 0)
		{
			sql = string.Format(@"insert into CTM_JobCharge (JobNo,ItemName,ItemType,Cost,CreateDateTime) values('{0}','charge1','','0',getdate()),('{0}','charge2','','0',getdate()),('{0}','charge3','','0',getdate())", JobNo);
			int r = ConnectSql_mb.ExecuteNonQuery(sql);
			if (r > 0)
			{
				status = "1";
			}

			sql = string.Format(@"select * from CTM_JobCharge where JobNo='{0}'", JobNo);
			dt = ConnectSql_mb.GetDataTable(sql);
		}
		else
		{
			status = "2";
		}

		if (status.Equals("1") && dt != null)
		{
			context = Common.DataTableToJson(dt);

			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = JobNo;
			l.Controller = user;
			l.Remark = "Add new Charge " + l.Trip + " by " + user;;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Job_Detail_Charge_Save(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string sql = string.Format(@"update CTM_JobCharge set Cost='{1}' where Id='{0}'", jo["Id"], jo["Cost"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string result = "0";
		string context = "Error";
		if (i == 1)
		{
			result = "1";
			sql = string.Format(@"select * from CTM_JobCharge where JobNo='{0}'", jo["JobNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			context = Common.DataTableToJson(dt);
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void Job_Detail_Billing_AutoAdd(string jobno, string user, string loc)
	{
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);

		string sql = string.Format(@"select * from CTM_JobDet1 where JobNo='{0}'", jobno);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string invN = C2Setup.GetNextNo("", "IV", DateTime.Now);
		string acCode = EzshipHelper.GetAccArCode("", "SGD");
		sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo)
values('IV',getdate(),'','{0}',Year(getdate()),Month(getdate()),'CASH',getdate(),'',
'SGD','CTM',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}')
select @@IDENTITY", invN, user, acCode, jobno);
		string docId = ConnectSql_mb.ExecuteScalar(sql);
		for (int i = 0; i < dt.Rows.Count; i++)
		{
		}
	}

	[WebMethod]
	public void Job_Detail_Billing_Add(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string AcCode = EzshipHelper.GetAccArCode("", "SGD");
		DateTime DocDate = DateTime.Now;
		string counterType = "AR-IV";
		if (jo["DocType"].ToString() == "DN")
		{
			counterType = "AR-DN";
		}
		else
		{
			if (jo["DocType"].ToString() == "PVG")
			{
				counterType = "CTM-PVG";
			}
		}

		string invN = C2Setup.GetNextNo(jo["DocType"].ToString(), counterType, DocDate);
		string sql = string.Format(@"insert into XAArInvoice (AcYear,AcPeriod,AcCode,AcSource,DocType,DocNo,DocDate,DocDueDate,MastRefNo,JobRefNo,MastType,CurrencyId,ExRate,Term,DocAmt,LocAmt,BalanceAmt,ExportInd,CancelInd,UserId,EntryDate)
values (YEAR(getdate()),MONTH(getdate()),'{0}','DB','{1}','{2}',GETDATE(),getdate(),'{4}','0','{5}','SGD',1,'CASH',0,0,0,'N','N','{3}',getdate())", AcCode, jo["DocType"], invN, jo["User"], jo["MastRefNo"], jo["MastType"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo("", counterType, invN, DocDate);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["MastRefNo"].ToString();
			l.Note3 = invN;
			l.Remark = (jo["DocType"].ToString() == "PVG" ? "Add new Payment" : "Add new Invoice") + " by " + jo["User"];
			l.Controller = jo["User"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string context = Common.StringToJson(invN);
		Common.WriteJsonP(i, context);
	}

	[WebMethod]
	public void Job_Detail_Billing_Refresh(string No)
	{
		string sql = string.Format(@"select * from XAArInvoice where MastRefNo='{0}' and MastType='CTM' order by DocType", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string billing = Common.DataTableToJson(dt);
		Common.WriteJsonP(billing);
	}

	[WebMethod]
	public void Job_Detail_Activity_Refresh(string No)
	{
		string sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string attachment = Common.DataTableToJson(dt);
		Common.WriteJsonP(attachment);
	}

	[WebMethod]
	public void Job_Detail_Stock_Add(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"insert into CTM_Job_Stock (JobNo,StockStatus,StockQty,PackingQty,Weight,Volume)
values ('{0}','{1}',0,0,0,0)
select Id from CTM_Job_Stock where Id=@@IDENTITY", jo["JobNo"], jo["StockStatus"]);
		string i = ConnectSql_mb.ExecuteScalar(sql);
		if (i.Length > 0)
		{
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["JobNo"].ToString();
			l.Controller = user;
			l.Remark = "Add new stock item by " + user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string json = Common.StringToJson(i);
		Common.WriteJsonP(true, json);
	}

	//[WebMethod]

	public class C_Job_Detail_EventLog
	{
		#region columns
        public int Id { get; set; }
		DateTime CreateDateTime { get; set; }
		public string Controller { get; set; }
		public string JobNo { get; set; }
		public string JobType { get; set; }
		public string ParentJobNo { get; set; }
		public string ParentJobType { get; set; }
		public string ContainerNo { get; set; }
		public string Trip { get; set; }
		public string Driver { get; set; }
		public string Towhead { get; set; }
		public string Trail { get; set; }
		public string Remark { get; set; }
		public string Note1 { get; set; }
		public string Note1Type { get; set; }
		public string Note2 { get; set; }
		public string Note3 { get; set; }
		public string Note4 { get; set; }

		public string Lat { get; set; }
		public string Lng { get; set; }
		public string Platform { get; set; }
		#endregion

        public void Job_Detail_EventLog_Add()
		{
			C_Job_Detail_EventLog l = this;
			string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type) values(getdate(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", l.Controller, l.JobNo, l.ContainerNo, l.Trip, l.Driver, l.Towhead, l.Trail, l.Remark, l.Note1, l.Note2, l.Note3, l.Note4, l.Lat, l.Lng, l.Platform, l.JobType, l.ParentJobNo, l.ParentJobType, l.Note1Type);
			ConnectSql_mb.ExecuteNonQuery(sql);
		}
	}

	#endregion

    #region container schedule
    [WebMethod]
	public void ContainerSchedule_GetList_ByTab(string tab, string user)
	{
		string sql_where = "";
		switch (tab)
		{
			case "Today":
                sql_where = "and datediff(day,FromDate,getdate())=0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "This week":
                sql_where = "and datediff(week,FromDate,getdate())=0 and datediff(day,FromDate,getdate())>0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "Later":
                sql_where = "and datediff(week,FromDate,getdate())>0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "Past":
                sql_where = "and det2.Statuscode='C'";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		string sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and DriverCode='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and ClientId='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select det2.Id,det1.JobNo,det1.ContainerNo,DriverCode,TowheadCode,ChessisCode,det1.Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det1.JobNo=det2.JobNo and det1.ContainerNo=det2.ContainerNo
where datediff(year,getdate(),FromDate)=0 and det1.JobNo is not null {1} {0}
order by FromDate,FromTime", sql_where_2, sql_where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	#endregion

    #region local job

    [WebMethod]
	public void Local_Job_New_Save(string info, string user, string loc)
	{
		JObject job = JObject.Parse(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		DateTime date = DateTime.Now;
		//string time4 = date.ToString("hhss");
		string jobno = C2Setup.GetNextNo(job["JobType"].ToString(), "LocalTpt", date);
		string sql = string.Format(@"insert into TPT_Job (JobNo,JobType,JobDate,Wt,M3,Qty,Eta,Etd,SortIndex,StatusCode,CreateBy,CreateDateTime,TptType,
BkgTime,BkgWt,BkgM3,BkgQty,FeeTpt,FeeLabour,FeeOt,FeeAdmin,FeeReimberse,FeeMisc,FeeTotal,TptTime,BkgDate,TptDate)
values('{0}','{1}',getdate(),0,0,0,getdate(),getdate(),0,'USE','{2}',getdate(),'PUP',
'00:00',0,0,0, 0,0,0,0,0,0,0, '00:00',getdate(),getdate())", jobno, job["JobType"], user);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo(job["JobType"].ToString(), "LocalTpt", jobno, date);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jobno;
			l.Remark = "job create by " + user;
			l.Controller = user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string json = Common.StringToJson(jobno);
		Common.WriteJsonP(i, json);
	}

	[WebMethod]
	public void Local_Job_GetDataList(string search)
	{
		string where = "";
		if (search.Trim().Length == 0)
		{
			where = "and DATEDIFF(day,jobdate,getdate())<10";
		}
		else
		{
			where = "and JobNo like '" + search + "%'";
		}

		string sql = string.Format(@"select JobNo,JobDate,Vessel,Voyage,Pol,Pod,Eta,Etd
From TPT_Job 
where StatusCode='USE' {0} 
order by JobDate desc,Id desc", where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Job_Detail_GetData(string JobNo)
	{
		string sql = string.Format(@"select * from TPT_Job where JobNo='{0}'", JobNo);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string info = Common.DataRowToJson(dt);

		sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string activity = Common.DataTableToJson(dt);

		sql = string.Format(@"select  Id,FileType,FileName,FilePath,FileNote From Tpt_Attachment where RefNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["FileType"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string attachment = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from XAArInvoice where MastRefNo='{0}' and MastType='TPT' order by DocType", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string billing = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from CTM_Job_Stock where JobNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string stock = Common.DataTableToJson(dt);

		sql = string.Format(@"select 'Assigner' as name,CreateBy as value from tpt_job where JobNo='{0}' and isnull(CreateBy,'')<>''
union all
select 'Assigner1' as name,UpdateBy as value from tpt_job where JobNo='{0}' and isnull(UpdateBy,'')<>''
union all
select 'Driver' as name,Driver as value from tpt_job where JobNo='{0}' and isnull(Driver,'')<>''
union all
select 'Customer' as name,Cust as value from tpt_job where JobNo='{0}' and isnull(Cust,'')<>''", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string watch = Common.DataTableToJson(dt);

		string json = string.Format(@"{0}info:{2},attachment:{3},billing:{4},stock:{5},activity:{6},watch:{7}{1}", "{", "}", info, attachment, billing, stock, activity, watch);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Job_Detail_Info_Save(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"select Driver from tpt_job where Id={0} ", jo["Id"]);
		string driver_temp = ConnectSql_mb.ExecuteScalar(sql);

		sql = string.Format(@"update tpt_job set JobType='{1}',Cust='{2}',Pol='{3}',Pod='{4}',Vessel='{5}',Voyage='{6}',Etd='{7}',Eta='{8}',
BkgDate='{9}',BkgTime='{10}',JobRmk='{11}',BkgWt='{12}',BkgM3='{13}',BkgQty='{14}',BkgPkgType='{15}',PickFrm1='{16}',DeliveryTo1='{17}',
TptDate='{18}',TptTime='{19}',JobProgress='{20}',TptType='{21}',TripCode='{22}',Driver='{23}',VehicleNo='{24}',Wt='{25}',M3='{26}',Qty='{27}',PkgType='{28}',
FeeTpt='{29}',FeeLabour='{30}',FeeOt='{31}',FeeAdmin='{32}',FeeReimberse='{33}',FeeMisc='{34}',FeeRemark='{35}'
where Id={0}", jo["Id"], jo["JobType"], jo["Cust"], jo["Pol"], jo["Pod"], jo["Vessel"], jo["Voyage"], jo["Etd"], jo["Eta"],
         jo["BkgDate"], jo["BkgTime"], jo["JobRmk"], jo["BkgWt"], jo["BkgM3"], jo["BkgQty"], jo["BkgPkgType"], jo["PickFrm1"], jo["DeliveryTo1"],
         jo["TptDate"], jo["TptTime"], jo["JobProgress"], jo["TptType"], jo["TripCode"], jo["Driver"], jo["VehicleNo"], jo["Wt"], jo["M3"], jo["Qty"], jo["PkgType"],
         jo["FeeTpt"], jo["FeeLabour"], jo["FeeOt"], jo["FeeAdmin"], jo["FeeReimberse"], jo["FeeMisc"], jo["FeeRemark"]);
		string result = "0";
		try
		{
			if (ConnectSql_mb.ExecuteNonQuery(sql) == 1)
			{
				result = "1";
				if (!jo["Driver"].ToString().Equals(driver_temp))
				{
					C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
					l.JobNo = jo["JobNo"].ToString();
					l.Driver = jo["Driver"].ToString();
					l.Towhead = jo["VehicleNo"].ToString();
					l.Remark = "job assign to " + jo["Driver"];
					l.Controller = user;
					l.Lat = jl["Lat"].ToString();
					l.Lng = jl["Lng"].ToString();
					l.Job_Detail_EventLog_Add();
				}
			}
		}
		catch { }
		string json = Common.StringToJson(result);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Job_Detail_Status_Save(string info, string user, string loc)
	{
		JObject job = JObject.Parse(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"update tpt_job set JobProgress='{1}' where Id='{0}'", job["Id"], job["JobProgress"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = job["JobNo"].ToString();
			l.Remark = "job " + job["JobProgress"] + " by " + user;
			l.Driver = job["Driver"].ToString();
			l.Towhead = job["Towhead"].ToString();
			l.Controller = user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		Common.WriteJsonP(Common.StringToJson(i.ToString()));
	}

	[WebMethod]
	public void Local_Job_Detail_Attachment_Add(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"insert into Tpt_Attachment (JobType,RefNo,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values('{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}')", jo["JobType"], jo["RefNo"], jo["FileType"], jo["FileName"], jo["FilePath"], jo["CreateBy"], jo["FileNote"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string result = "0";
		string context = "Error";
		if (i == 1)
		{
			result = "1";
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["RefNo"].ToString();
			l.Note1 = jo["FilePath"].ToString();
			l.Note1Type = jo["FileType"].ToString();
			l.Controller = user;
			l.Remark = jo["CreateBy"] + " post a " + jo["FileType"] + " file";
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();

			sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", jo["RefNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			DataRow dr = null;
			for (int i1 = 0; i1 < dt.Rows.Count; i1++)
			{
				dr = dt.Rows[i1];
				if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
				{
					string path = dr["FilePath"].ToString();
					path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
					dr["FilePath"] = path;
				}
			}

			string activity = Common.DataTableToJson(dt);

			sql = string.Format(@"select  Id,FileType,FileName,FilePath,FileNote From Tpt_Attachment where RefNo='{0}'", jo["RefNo"]);
			dt = ConnectSql_mb.GetDataTable(sql);
			for (int i1 = 0; i1 < dt.Rows.Count; i1++)
			{
				dr = dt.Rows[i1];
				if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["FileType"].ToString(), 500))
				{
					string path = dr["FilePath"].ToString();
					path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
					dr["FilePath"] = path;
				}
			}

			string attachment = Common.DataTableToJson(dt);

			context = string.Format(@"{0}attachment:{2},activity:{3}{1}", "{", "}", attachment, activity);
			//context = activity;
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void Local_Job_Detail_Activity_Refresh(string No)
	{
		string sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string attachment = Common.DataTableToJson(dt);
		Common.WriteJsonP(attachment);
	}

	[WebMethod]
	public void Local_job_Detail_Billing_Add(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string AcCode = EzshipHelper.GetAccArCode("", "SGD");
		DateTime DocDate = DateTime.Now;
		string counterType = "AR-IV";
		if (jo["DocType"].ToString() == "DN")
		{
			counterType = "AR-DN";
		}
		else
		{
			if (jo["DocType"].ToString() == "PVG")
			{
				counterType = "CTM-PVG";
			}
		}

		string invN = C2Setup.GetNextNo(jo["DocType"].ToString(), counterType, DocDate);
		string sql = string.Format(@"insert into XAArInvoice (AcYear,AcPeriod,AcCode,AcSource,DocType,DocNo,DocDate,DocDueDate,MastRefNo,JobRefNo,MastType,CurrencyId,ExRate,Term,DocAmt,LocAmt,BalanceAmt,ExportInd,CancelInd,UserId,EntryDate)
values (YEAR(getdate()),MONTH(getdate()),'{0}','DB','{1}','{2}',GETDATE(),getdate(),'{4}','0','{5}','SGD',1,'CASH',0,0,0,'N','N','{3}',getdate())", AcCode, jo["DocType"], invN, jo["User"], jo["MastRefNo"], jo["MastType"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo("", counterType, invN, DocDate);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["MastRefNo"].ToString();
			l.Note3 = invN;
			l.Remark = (jo["DocType"].ToString() == "PVG" ? "Add new Payment" : "Add new Invoice") + " by " + jo["User"].ToString();
			l.Controller = jo["User"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string context = Common.StringToJson(invN);
		Common.WriteJsonP(i, context);
	}

	[WebMethod]
	public void Local_job_Detail_Billing_Refresh(string No)
	{
		string sql = string.Format(@"select * from XAArInvoice where MastRefNo='{0}' and MastType='TPT' order by DocType", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string billing = Common.DataTableToJson(dt);
		Common.WriteJsonP(billing);
	}

	[WebMethod]
	public void Local_Schedule_GetList_byUser(string date, string user)
	{
		string sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from tpt_job where DATEDIFF(day,TptDate,'{0}')=0 and Driver='{1}'
order by TptDate", date, user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Schedule_GetList_All(string date, string user)
	{
		string sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from tpt_job where DATEDIFF(day,TptDate,'{0}')=0
order by TptDate", date, user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Schedule1_GetList_ByTab(string tab, string user)
	{
		string sql_where = "";
		switch (tab)
		{
			case "Today":
                sql_where = "and datediff(day,TptDate,getdate())=0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "This week":
                sql_where = "and datediff(week,tptdate,getdate())=0 and datediff(day,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Later":
                sql_where = "and datediff(week,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Past":
                sql_where = "and JobProgress='Delivered'";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		string sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and Driver='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and Cust='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from tpt_job where datediff(year,getdate(),JobDate)=0 {0} {1}
order by TptDate", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Schedule1_GetList_ByNo(string No, string user)
	{
		string sql = string.Format(@"select case when datediff(day,TptDate,getdate())=0  then 'Today' 
when datediff(day,TptDate,getdate())>0 and datediff(week,tptdate,getdate())=0 then 'This week' 
when datediff(week,tptdate,getdate())>0 then 'Later'
else '' end as tab,JobProgress from tpt_job where JobNo='{0}'", No);
		DataTable dt_no = ConnectSql_mb.GetDataTable(sql);
		string tab = "";
		if (dt_no.Rows.Count > 0)
		{
			DataRow dr = dt_no.Rows[0];
			string jp = dr["JobProgress"].ToString();
			string t = dr["tab"].ToString();
			if (jp.Equals("Assigned") || jp.Equals("Confirmed") || jp.Equals("Picked"))
			{
				if (t.Equals("Today") || t.Equals("This week") || t.Equals("Later"))
				{
					tab = t;
				}
			}

			if (jp.Equals("Delivered"))
			{
				tab = "Past";
			}
		}

		string sql_where = "";
		switch (tab)
		{
			case "Today":
                sql_where = "and datediff(day,TptDate,getdate())=0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "This week":
                sql_where = "and datediff(week,tptdate,getdate())=0 and datediff(day,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Later":
                sql_where = "and datediff(week,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Past":
                sql_where = "and JobProgress='Delivered'";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and Driver='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and Cust='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from tpt_job where datediff(year,getdate(),JobDate)=0 {0} {1}
order by TptDate", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Schedule1_GetNoticeData(string user)
	{
		string sql = string.Format(@"select Id,JobNo
from tpt_job where Driver='{0}' and datediff(day,TptDate,getdate())=0 and (JobProgress='Confirmed' or JobProgress='Picked')
order by TptTime", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_Schedule1_Search(string search, string from, string to)
	{
		DateTime dt_from = SafeValue.SafeDate(from, new DateTime());
		DateTime dt_to = SafeValue.SafeDate(to, new DateTime());
		string where = string.Format(@"and jobdate>='{0}' and jobdate<='{1}'", dt_from.ToString("yyyyMMdd"), dt_to.AddDays(1).ToString("yyyyMMdd"));
		if (search.Trim().Length > 0)
		{
			where += " and JobNo like '" + search + "%'";
		}

		string sql = string.Format(@"select JobNo,JobDate,Vessel,Voyage,Pol,Pod,Eta,Etd
From TPT_Job 
where StatusCode='USE' {0} 
order by JobDate desc,Id desc", where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Local_job_Detail_Stock_Add(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"insert into CTM_Job_Stock (JobNo,StockStatus,StockQty,PackingQty,Weight,Volume)
values ('{0}','{1}',0,0,0,0)
select Id from CTM_Job_Stock where Id=@@IDENTITY", jo["JobNo"], jo["StockStatus"]);
		string i = ConnectSql_mb.ExecuteScalar(sql);
		if (i.Length > 0)
		{
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["JobNo"].ToString();
			l.Controller = user;
			l.Remark = "Add new stock item by " + user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string json = Common.StringToJson(i);
		Common.WriteJsonP(true, json);
	}

	#endregion

    #region Schedule
    [WebMethod]
	public void Schedule_GetWeekdays(string date)
	{
		string sql = string.Format(@"with tb_today as (
select '{0}' as today,DATEPART(weekday,'{0}') as wd
),
tb_7days as (
select top 7 ROW_NUMBER()over(order by Id) as Id from sysobjects
),
tb_days as (
select Id,dateadd(day,Id-wd,today) as [date] from tb_7days as day7 inner join tb_today as day2 on 1=1
)
select *,DATEPART(day,[date]) as [day],case DATEDIFF(day,getdate(),[date]) when 0 then 1 else 0 end as isToday from tb_days", date);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Schedule_GetJobList_byDate(string date)
	{
		string sql = string.Format(@"select JobNo,JobDate,EtaDate,EtaTime,JobType from ctm_job where datediff(day,EtaDate,'{0}')=0 order by EtaTime", date);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Schedule_GetTripList_byUser(string date, string user)
	{
		string sql = string.Format(@"select Id,JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime
from CTM_JobDet2 where DATEDIFF(day,FromDate,'{0}')=0 and DriverCode='{1}'
order by FromTime", date, user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Schedule_GetTripList_All(string date, string user)
	{
		string sql = string.Format(@"select Id,JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime
from CTM_JobDet2 where DATEDIFF(day,FromDate,'{0}')=0 
order by FromTime", date, user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Schedule_GetTripData(string No)
	{
		string sql = string.Format(@"select * from CTM_JobDet2 where Id='{0}'", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string trip = Common.DataRowToJson(dt);
		Common.WriteJsonP(trip);
	}

	[WebMethod]
	public void Schedule_Trip_Status_Save(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"update CTM_JobDet2 set StatusCode='{1}',ChessisCode='{2}' where Id='{0}'", jo["Id"], jo["Status"], jo["Trail"]);
		string re = "0";
		if (ConnectSql_mb.ExecuteNonQuery(sql) == 1)
		{
			re = "1";
			string remark = "";
			switch (jo["Status"].ToString())
			{
				case "U":
                    remark = "Use";
				break;
				case "S":
                    remark = "Start";
				break;
				case "C":
                    remark = "Complete";
				break;
			}

			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["JobNo"].ToString();
			l.ContainerNo = jo["ContainerNo"].ToString();
			l.Trip = jo["JobNo"] + "." + jo["ContainerNo"];
			l.Driver = jo["Driver"].ToString();
			l.Towhead = jo["Towhead"].ToString();
			l.Trail = jo["Trail"].ToString();
			remark = "Trip " + l.Trip + " " + remark;
			l.Remark = remark;
			l.Controller = jo["User"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		Common.WriteJsonP(Common.StringToJson(re));
	}

	[WebMethod]
	public void Schedule_GetNoticeData(string user)
	{
		string sql = string.Format(@"select Id,JobNo
from CTM_JobDet2 where DATEDIFF(day,FromDate,getdate())=0 and DriverCode='{0}'
order by FromTime", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Haulier_Schedule1_GetList_ByTab(string tab, string user)
	{
		string sql_where = "";
		switch (tab)
		{
			case "Today":
                sql_where = "and datediff(day,FromDate,getdate())=0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "This week":
                sql_where = "and datediff(week,FromDate,getdate())=0 and datediff(day,FromDate,getdate())>0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "Later":
                sql_where = "and datediff(week,FromDate,getdate())>0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "Past":
                sql_where = "and det2.Statuscode='C'";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		string sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and DriverCode='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and ClientId='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select det2.Id,det2.JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,det2.Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime
from CTM_JobDet2 as det2
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where datediff(year,getdate(),FromDate)=0 {0} {1}
order by FromDate,FromTime", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Haulier_Schedule1_GetList_ByNo(string No, string user)
	{
		string sql = string.Format(@"select case when datediff(day,FromDate,getdate())=0  then 'Today' 
when datediff(day,FromDate,getdate())>0 and datediff(week,FromDate,getdate())=0 then 'This week' 
when datediff(week,FromDate,getdate())>0 then 'Later'
else '' end as tab,Statuscode from CTM_JobDet2 where Id='{0}'", No);
		DataTable dt_no = ConnectSql_mb.GetDataTable(sql);
		string tab = "";
		if (dt_no.Rows.Count > 0)
		{
			DataRow dr = dt_no.Rows[0];
			string jp = dr["Statuscode"].ToString();
			string t = dr["tab"].ToString();
			if (jp.Equals("U") || jp.Equals("S") || jp.Equals("D") || jp.Equals("W") || jp.Equals("P"))
			{
				if (t.Equals("Today") || t.Equals("This week") || t.Equals("Later"))
				{
					tab = t;
				}
			}

			if (jp.Equals("C"))
			{
				tab = "Past";
			}
		}

		string sql_where = "";
		switch (tab)
		{
			case "Today":
                sql_where = "and datediff(day,FromDate,getdate())=0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "This week":
                sql_where = "and datediff(week,FromDate,getdate())=0 and datediff(day,FromDate,getdate())>0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "Later":
                sql_where = "and datediff(week,FromDate,getdate())>0 and (det2.Statuscode='U' or det2.Statuscode='S' or det2.Statuscode='D' or det2.Statuscode='W' or det2.Statuscode='P')";
			break;
			case "Past":
                sql_where = "and det2.Statuscode='C'";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and DriverCode='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and ClientId='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select det2.Id,det2.JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,det2.Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime
from CTM_JobDet2 as det2
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where datediff(year,getdate(),FromDate)=0 {0} {1}
order by FromDate,FromTime", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Haulier_Schedule1_GetNoticeData(string user)
	{
		string sql = string.Format(@"select Id,JobNo
from CTM_JobDet2 
where DriverCode='{0}' and datediff(day,FromDate,getdate())=0 and (Statuscode='U' or Statuscode='S' or Statuscode='D' or Statuscode='W' or Statuscode='P')
order by FromTime", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Haulier_Schedule1_Search(string search, string from, string to)
	{
		DateTime dt_from = SafeValue.SafeDate(from, new DateTime());
		DateTime dt_to = SafeValue.SafeDate(to, new DateTime());
		string where = string.Format(@"and jobdate>='{0}' and jobdate<='{1}'", dt_from.ToString("yyyyMMdd"), dt_to.AddDays(1).ToString("yyyyMMdd"));
		if (search.Trim().Length > 0)
		{
			where += " and JobNo like '" + search + "%'";
		}

		string sql = string.Format(@"select JobNo,JobDate,Vessel,Voyage,Pol,Pod,EtaDate,EtaTime,EtdDate,EtdTime,CarrierId,ClientId,HaulierId
From CTM_Job 
where StatusCode='USE' {0} 
order by JobDate desc", where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Haulier_Daily_GetList_ByTab(string tab, string user)
	{
		string sql_where = "";
		switch (tab)
		{
			case "Undelivered":
                sql_where = "and det2.Statuscode<>'C' and det2.Statuscode<>'X' ";
			break;
			case "Completed":
                sql_where = "and det2.Statuscode='C'";
			break;
			case "Failed":
                sql_where = "and det2.Statuscode='X' ";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		string sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and DriverCode='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and ClientId='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select det2.Id,det2.JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,det2.Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime
from CTM_JobDet2 as det2
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where datediff(day,getdate(),det2.FromDate)=0 {0} {1}
order by FromDate,FromTime", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Haulier_Daily_GetList_ByNo(string No, string user)
	{
		string sql = string.Format(@"select case when datediff(day,FromDate,getdate())=0  then 'Today' 
else '' end as tab,Statuscode from CTM_JobDet2 where Id='{0}'", No);
		DataTable dt_no = ConnectSql_mb.GetDataTable(sql);
		string tab = "";
		if (dt_no.Rows.Count > 0)
		{
			DataRow dr = dt_no.Rows[0];
			string jp = dr["Statuscode"].ToString();
			string t = dr["tab"].ToString();
			if (t.Equals("Today"))
			{
				tab = "Undelivered";
				if (jp.Equals("C"))
				{
					tab = "Completed";
				}

				if (jp.Equals("X"))
				{
					tab = "Failed";
				}
			}
		}

		string sql_where = "";
		switch (tab)
		{
			case "Undelivered":
                sql_where = "and det2.Statuscode<>'C' and det2.Statuscode<>'X' ";
			break;
			case "Completed":
                sql_where = "and det2.Statuscode='C'";
			break;
			case "Failed":
                sql_where = "and det2.Statuscode='X' ";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and DriverCode='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and ClientId='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select det2.Id,det2.JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,det2.Statuscode,StageCode,StageStatus 
,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime
from CTM_JobDet2 as det2
left outer join CTM_Job as job on job.JobNo=det2.JobNo
where datediff(day,getdate(),det2.FromDate)=0 {0} {1}
order by FromDate,FromTime", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	#endregion

    #region Transport job Service Order

    [WebMethod]
	public void TpJob_Job_New_Save(string info, string user, string loc)
	{
		JObject job = JObject.Parse(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		DateTime date = DateTime.Now;
		//string time4 = date.ToString("hhss");
		string jobno = C2Setup.GetNextNo(job["JobType"].ToString(), "TP_Job", date);
		string sql = string.Format(@"insert into TP_ServiceOrder_Job (JobNo,JobType,JobDate,Wt,M3,Qty,Eta,Etd,SortIndex,StatusCode,CreateBy,CreateDateTime,TptType,
BkgTime,BkgWt,BkgM3,BkgQty,FeeTpt,FeeLabour,FeeOt,FeeAdmin,FeeReimberse,FeeMisc,FeeTotal,TptTime,BkgDate,TptDate,Collection_Amount)
values('{0}','{1}',getdate(),0,0,0,getdate(),getdate(),0,'USE','{2}',getdate(),'PUP',
'00:00',0,0,0, 0,0,0,0,0,0,0, '00:00',getdate(),getdate(),0)", jobno, job["JobType"], user);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo(job["JobType"].ToString(), "TP_Job", jobno, date);

			sql = string.Format(@"insert into TP_ServiceOrder_Job_Check (JobNo,DefaultId,Code,[Group],Sort,Flag,CheckDate,CheckTime)
select '{0}',tb1.Id,tb1.Code,tb1.[Group],tb1.Sort,tb1.Flag,getdate(),convert(nvarchar(5),getdate(),114) from TP_ServiceOrder_Job_Check_Default as tb1 
left outer join TP_ServiceOrder_Job_Check as tb2 on tb1.Id=tb2.DefaultId and tb2.JobNo='{0}'
where tb2.Id is null", jobno);
			ConnectSql_mb.ExecuteNonQuery(sql);

			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jobno;
			l.Remark = "job create by " + user;
			l.Controller = user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string json = Common.StringToJson(jobno);
		Common.WriteJsonP(i, json);
	}

	[WebMethod]
	public void TpJob_Job_GetDataList(string search)
	{
		string where = "";
		if (search.Trim().Length == 0)
		{
			where = "and DATEDIFF(day,jobdate,getdate())<10";
		}
		else
		{
			where = "and JobNo like '" + search + "%'";
		}

		string sql = string.Format(@"select JobNo,JobDate,Vessel,Voyage,Pol,Pod,Eta,Etd
From TP_ServiceOrder_Job 
where StatusCode='USE' {0} 
order by JobDate desc,Id desc", where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Job_Detail_GetData(string JobNo)
	{
		//        string sql = string.Format(@"select Id,JobNo,JobType,JobProgress,JobDate,JobRmk,CustRef,Wt,M3,Qty,PkgType,BlRef,BkgRef,Vessel,Voyage,Pol,Pod,
		//Eta,Etd,Driver,Term,PickFrm1,DeliveryTo1,Cust,UserId,VehicleNo,StatusCode,CreateBy,CreateDateTime,UpdateBy,UpdateDateTime,TptType,
		//BkgDate,BkgTime,BkgWt,BkgM3,BkgQty,BkgPkgType,FeeTpt,FeeLabour,FeeOt,FeeAdmin,FeeReimberse,FeeMisc,FeeTotal,FeeRemark,TripCode,TptDate,TptTime,
		//Pickup_Name,Pickup_Contact,Pickup_Address,Pickup_Postcode,Delivery_Name,Delivery_Contact,Delivery_Address,Delivery_Postcode,SaleOrder,PurchaseOrder,Visual_Qty 
		//from TP_ServiceOrder_Job where JobNo='{0}'", JobNo);
		string sql = string.Format(@"select Id,JobNo,JobType,JobProgress,JobDate,JobRmk,CustRef,Wt,M3,Qty,PkgType,BlRef,BkgRef,Vessel,Voyage,Pol,Pod,
Eta,Etd,Driver,Term,PickFrm1,DeliveryTo1,Cust,UserId,VehicleNo,StatusCode,CreateBy,CreateDateTime,UpdateBy,UpdateDateTime,TptType,
BkgDate,BkgTime,BkgWt,BkgM3,BkgQty,BkgPkgType,TripCode,TptDate,TptTime,
Pickup_Name,Pickup_Contact,Pickup_Address,Pickup_Postcode,Delivery_Name,Delivery_Contact,Delivery_Address,Delivery_Postcode,SaleOrder,PurchaseOrder,Visual_Qty,
Unsuccess_Type,Collection_Type,Collection_Amount,Collection_Instruction 
from TP_ServiceOrder_Job where JobNo='{0}'", JobNo);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string info = Common.DataRowToJson(dt);

		sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string activity = Common.DataTableToJson(dt);

		sql = string.Format(@"select  Id,FileType,FileName,FilePath,FileNote From TP_ServiceOrder_Attachment where RefNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["FileType"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string attachment = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from XAArInvoice where MastRefNo='{0}' and MastType='TPT' order by DocType", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string billing = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from CTM_Job_Stock where JobNo='{0}'", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string stock = Common.DataTableToJson(dt);

		sql = string.Format(@"select 'Assigner' as name,CreateBy as value from TP_ServiceOrder_Job where JobNo='{0}' and isnull(CreateBy,'')<>''
union all
select 'Assigner1' as name,UpdateBy as value from TP_ServiceOrder_Job where JobNo='{0}' and isnull(UpdateBy,'')<>''
union all
select 'Driver' as name,Driver as value from TP_ServiceOrder_Job where JobNo='{0}' and isnull(Driver,'')<>''
union all
select 'Customer' as name,Cust as value from TP_ServiceOrder_Job where JobNo='{0}' and isnull(Cust,'')<>''", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string watch = Common.DataTableToJson(dt);

		sql = string.Format(@"select * from TP_ServiceOrder_Job_Check
where JobNo='{0}' 
order by [Group],Sort", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string checklist = Common.DataTableToJson(dt);

		sql = string.Format(@"select * From TP_ServiceOrder_Job_Schedule where JobNo='{0}' order by SDate,STime", JobNo);
		dt = ConnectSql_mb.GetDataTable(sql);
		string schedule = Common.DataTableToJson(dt);

		string json = string.Format(@"{0}info:{2},attachment:{3},billing:{4},stock:{5},activity:{6},watch:{7},check:{8},schedule:{9}{1}", "{", "}", info, attachment, billing, stock, activity, watch, checklist, schedule);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Job_Detail_Info_Save(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"select Driver from TP_ServiceOrder_Job where Id={0} ", jo["Id"]);
		string driver_temp = ConnectSql_mb.ExecuteScalar(sql);

		//        sql = string.Format(@"update TP_ServiceOrder_Job set JobType='{1}',Cust='{2}',Pol='{3}',Pod='{4}',Vessel='{5}',Voyage='{6}',Etd='{7}',Eta='{8}',
		//BkgDate='{9}',BkgTime='{10}',JobRmk='{11}',BkgWt='{12}',BkgM3='{13}',BkgQty='{14}',BkgPkgType='{15}',PickFrm1='{16}',DeliveryTo1='{17}',
		//TptDate='{18}',TptTime='{19}',JobProgress='{20}',TptType='{21}',TripCode='{22}',Driver='{23}',VehicleNo='{24}',Wt='{25}',M3='{26}',Qty='{27}',PkgType='{28}',
		//FeeTpt='{29}',FeeLabour='{30}',FeeOt='{31}',FeeAdmin='{32}',FeeReimberse='{33}',FeeMisc='{34}',FeeRemark='{35}',SaleOrder='{36}',PurchaseOrder='{37}',
		//Pickup_Name='{38}',Pickup_Contact='{39}',Pickup_Address='{40}',Pickup_Postcode='{41}',Delivery_Name='{42}',Delivery_Contact='{43}',Delivery_Address='{44}',Delivery_Postcode='{45}',Visual_Qty='{46}'
		//where Id={0}", jo["Id"], jo["JobType"], jo["Cust"], jo["Pol"], jo["Pod"], jo["Vessel"], jo["Voyage"], jo["Etd"], jo["Eta"],
		//         jo["BkgDate"], jo["BkgTime"], jo["JobRmk"], jo["BkgWt"], jo["BkgM3"], jo["BkgQty"], jo["BkgPkgType"], jo["PickFrm1"], jo["DeliveryTo1"],
		//         jo["TptDate"], jo["TptTime"], jo["JobProgress"], jo["TptType"], jo["TripCode"], jo["Driver"], jo["VehicleNo"], jo["Wt"], jo["M3"], jo["Qty"], jo["PkgType"],
		//         jo["FeeTpt"], jo["FeeLabour"], jo["FeeOt"], jo["FeeAdmin"], jo["FeeReimberse"], jo["FeeMisc"], jo["FeeRemark"], jo["SaleOrder"], jo["PurchaseOrder"],
		//         jo["Pickup_Name"], jo["Pickup_Contact"], jo["Pickup_Address"], jo["Pickup_Postcode"], jo["Delivery_Name"], jo["Delivery_Contact"], jo["Delivery_Address"], jo["Delivery_Postcode"], jo["Visual_Qty"]);
		sql = string.Format(@"update TP_ServiceOrder_Job set JobType='{1}',Cust='{2}',Pol='{3}',Pod='{4}',Vessel='{5}',Voyage='{6}',Etd='{7}',Eta='{8}',
BkgDate='{9}',BkgTime='{10}',JobRmk='{11}',BkgWt='{12}',BkgM3='{13}',BkgQty='{14}',BkgPkgType='{15}',PickFrm1='{16}',DeliveryTo1='{17}',
TptDate='{18}',TptTime='{19}',JobProgress='{20}',TptType='{21}',TripCode='{22}',Driver='{23}',VehicleNo='{24}',Wt='{25}',M3='{26}',Qty='{27}',PkgType='{28}',
SaleOrder='{36}',PurchaseOrder='{37}',
Pickup_Name='{38}',Pickup_Contact='{39}',Pickup_Address='{40}',Pickup_Postcode='{41}',Delivery_Name='{42}',Delivery_Contact='{43}',Delivery_Address='{44}',Delivery_Postcode='{45}',Visual_Qty='{46}',
Unsuccess_Type='{47}',Collection_Type='{48}',Collection_Amount='{49}',Collection_Instruction='{50}',BkgRef='{51}'   
where Id={0}", jo["Id"], jo["JobType"], jo["Cust"], jo["Pol"], jo["Pod"], jo["Vessel"], jo["Voyage"], jo["Etd"], jo["Eta"],
         jo["BkgDate"], jo["BkgTime"], jo["JobRmk"], jo["BkgWt"], jo["BkgM3"], jo["BkgQty"], jo["BkgPkgType"], jo["PickFrm1"], jo["DeliveryTo1"],
         jo["TptDate"], jo["TptTime"], jo["JobProgress"], jo["TptType"], jo["TripCode"], jo["Driver"], jo["VehicleNo"], jo["Wt"], jo["M3"], jo["Qty"], jo["PkgType"],
         "0", "0", "0", "0", "0", "0", "0", jo["SaleOrder"], jo["PurchaseOrder"],
         jo["Pickup_Name"], jo["Pickup_Contact"], jo["Pickup_Address"], jo["Pickup_Postcode"], jo["Delivery_Name"], jo["Delivery_Contact"], jo["Delivery_Address"], jo["Delivery_Postcode"], jo["Visual_Qty"], jo["Unsuccess_Type"], jo["Collection_Type"], SafeValue.SafeDecimal(jo["Collection_Amount"], 0), jo["Collection_Instruction"], jo["BkgRef"]);
		string result = "0";
		try
		{
			if (ConnectSql_mb.ExecuteNonQuery(sql) == 1)
			{
				result = "1";
				if (!jo["Driver"].ToString().Equals(driver_temp))
				{
					C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
					l.JobNo = jo["JobNo"].ToString();
					l.Driver = jo["Driver"].ToString();
					l.Towhead = jo["VehicleNo"].ToString();
					l.Remark = "job assign to " + jo["Driver"];
					l.Controller = user;
					l.Lat = jl["Lat"].ToString();
					l.Lng = jl["Lng"].ToString();
					l.Job_Detail_EventLog_Add();
				}
			}
		}
		catch { }
		string json = Common.StringToJson(result);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Job_Detail_Status_Save(string info, string user, string loc)
	{
		JObject job = JObject.Parse(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"update TP_ServiceOrder_Job set JobProgress='{1}' where Id='{0}'", job["Id"], job["JobProgress"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = job["JobNo"].ToString();
			l.Remark = "job " + job["JobProgress"] + " by " + user;
			l.Driver = job["Driver"].ToString();
			l.Towhead = job["Towhead"].ToString();
			l.Controller = user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		Common.WriteJsonP(Common.StringToJson(i.ToString()));
	}

	[WebMethod]
	public void TpJob_Job_Detail_Attachment_Add(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"insert into TP_ServiceOrder_Attachment (JobType,RefNo,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values('{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}')", jo["JobType"], jo["RefNo"], jo["FileType"], jo["FileName"], jo["FilePath"], jo["CreateBy"], jo["FileNote"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string result = "0";
		string context = "Error";
		if (i == 1)
		{
			result = "1";
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["RefNo"].ToString();
			l.Note1 = jo["FilePath"].ToString();
			l.Note1Type = jo["FileType"].ToString();
			l.Controller = user;
			string filenote = SafeValue.SafeString(jo["FileNote"]);
			l.Remark = jo["CreateBy"] + " post " + (filenote.Length > 0 ? filenote : ("a " + jo["FileType"] + " file"));
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();

			sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", jo["RefNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			DataRow dr = null;
			for (int i1 = 0; i1 < dt.Rows.Count; i1++)
			{
				dr = dt.Rows[i1];
				if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
				{
					string path = dr["FilePath"].ToString();
					path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
					dr["FilePath"] = path;
				}
			}

			string activity = Common.DataTableToJson(dt);

			sql = string.Format(@"select  Id,FileType,FileName,FilePath,FileNote From TP_ServiceOrder_Attachment where RefNo='{0}'", jo["RefNo"]);
			dt = ConnectSql_mb.GetDataTable(sql);
			for (int i1 = 0; i1 < dt.Rows.Count; i1++)
			{
				dr = dt.Rows[i1];
				if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["FileType"].ToString(), 500))
				{
					string path = dr["FilePath"].ToString();
					path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
					dr["FilePath"] = path;
				}
			}

			string attachment = Common.DataTableToJson(dt);

			context = string.Format(@"{0}attachment:{2},activity:{3}{1}", "{", "}", attachment, activity);
			//context = activity;
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void TpJob_Job_Detail_Activity_Refresh(string No)
	{
		string sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where JobNo='{0}' 
order by CreateDateTime desc", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dr = dt.Rows[i];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string attachment = Common.DataTableToJson(dt);
		Common.WriteJsonP(attachment);
	}

	[WebMethod]
	public void TpJob_job_Detail_Billing_Add(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string AcCode = EzshipHelper.GetAccArCode("", "SGD");
		DateTime DocDate = DateTime.Now;
		string counterType = "AR-IV";
		if (jo["DocType"].ToString() == "DN")
		{
			counterType = "AR-DN";
		}
		else
		{
			if (jo["DocType"].ToString() == "PVG")
			{
				counterType = "CTM-PVG";
			}
		}

		string invN = C2Setup.GetNextNo(jo["DocType"].ToString(), counterType, DocDate);
		string sql = string.Format(@"insert into XAArInvoice (AcYear,AcPeriod,AcCode,AcSource,DocType,DocNo,DocDate,DocDueDate,MastRefNo,JobRefNo,MastType,CurrencyId,ExRate,Term,DocAmt,LocAmt,BalanceAmt,ExportInd,CancelInd,UserId,EntryDate)
values (YEAR(getdate()),MONTH(getdate()),'{0}','DB','{1}','{2}',GETDATE(),getdate(),'{4}','0','{5}','SGD',1,'CASH',0,0,0,'N','N','{3}',getdate())", AcCode, jo["DocType"], invN, jo["User"], jo["MastRefNo"], jo["MastType"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo("", counterType, invN, DocDate);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["MastRefNo"].ToString();
			l.Note3 = invN;
			l.Remark = (jo["DocType"].ToString() == "PVG" ? "Add new Payment" : "Add new Invoice") + " by " + jo["User"].ToString();
			l.Controller = jo["User"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string context = Common.StringToJson(invN);
		Common.WriteJsonP(i, context);
	}

	[WebMethod]
	public void TpJob_job_Detail_Billing_Refresh(string No)
	{
		string sql = string.Format(@"select * from XAArInvoice where MastRefNo='{0}' and MastType='TPT' order by DocType", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string billing = Common.DataTableToJson(dt);
		Common.WriteJsonP(billing);
	}

	[WebMethod]
	public void TpJob_job_Detail_Checking_Refresh(string No)
	{
		string sql = string.Format(@"insert into TP_ServiceOrder_Job_Check (JobNo,DefaultId,Code,[Group],Sort,Flag,CheckDate,CheckTime)
select '{0}',tb1.Id,tb1.Code,tb1.[Group],tb1.Sort,tb1.Flag,getdate(),convert(nvarchar(5),getdate(),114) from TP_ServiceOrder_Job_Check_Default as tb1 
left outer join TP_ServiceOrder_Job_Check as tb2 on tb1.Id=tb2.DefaultId and tb2.JobNo='{0}'
where tb2.Id is null", No);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string context = "{}";
		int status = 0;
		if (i > 0)
		{
			status = 1;
			sql = string.Format(@"select * from TP_ServiceOrder_Job_Check
where JobNo='{0}' 
order by [Group],Sort", No);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			context = Common.DataTableToJson(dt);;
		}

		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void TpJob_job_Detail_Checking_Save(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"update TP_ServiceOrder_Job_Check set Flag=@Flag,CheckDate=@CheckDate,CheckTime=@CheckTime,Note1=@Note1 where Id=@Id");
		List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
		ConnectSql_mb.cmdParameters par;
		par = new ConnectSql_mb.cmdParameters("@Flag", SafeValue.SafeBool(jo["Flag"], false), SqlDbType.Bit);
		list.Add(par);
		par = new ConnectSql_mb.cmdParameters("@CheckDate", SafeValue.SafeDate(jo["CheckDate1"], DateTime.Now), SqlDbType.DateTime);
		list.Add(par);
		par = new ConnectSql_mb.cmdParameters("@CheckTime", SafeValue.SafeString(jo["CheckTime"]), SqlDbType.NVarChar, 10);
		list.Add(par);
		par = new ConnectSql_mb.cmdParameters("@Note1", SafeValue.SafeString(jo["Note1"]), SqlDbType.NVarChar, 300);
		list.Add(par);
		par = new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(jo["Id"], 0), SqlDbType.Int);
		list.Add(par);
		ConnectSql_mb.sqlResult sqlRe = ConnectSql_mb.ExecuteNonQuery(sql, list);
		string context = "{}";
		int status = 0;
		if (sqlRe.status && sqlRe.context != "0")
		{
			status = 1;

			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["JobNo"].ToString();
			//l.Note3 = jo["JobNo"].ToString();
			l.Remark = "Change the CheckList:" + jo["Code"] + " by " + user;
			l.Controller = user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();

			sql = string.Format(@"select * from TP_ServiceOrder_Job_Check
where JobNo='{0}' 
order by [Group],Sort", jo["JobNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			context = Common.DataTableToJson(dt);;
		}

		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void TpJob_job_Detail_Schedule_Save(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = "";
		string jo_Id = jo["Id"].ToString();
		List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
		ConnectSql_mb.cmdParameters par;
		if (jo_Id.Equals("0"))
		{
			sql = string.Format(@"insert into TP_ServiceOrder_Job_Schedule (JobNo,SDate,STime,Detail) values(@JobNo,@SDate,@STime,@Detail)");
			par = new ConnectSql_mb.cmdParameters("@JobNo", SafeValue.SafeString(jo["JobNo"]), SqlDbType.NVarChar, 100);
			list.Add(par);
			par = new ConnectSql_mb.cmdParameters("@SDate", SafeValue.SafeDate(jo["SDate1"], DateTime.Now), SqlDbType.DateTime);
			list.Add(par);
			par = new ConnectSql_mb.cmdParameters("@STime", SafeValue.SafeString(jo["STime"], "00:00"), SqlDbType.NVarChar, 10);
			list.Add(par);
			par = new ConnectSql_mb.cmdParameters("@Detail", SafeValue.SafeString(jo["Detail"]), SqlDbType.NVarChar, 300);
			list.Add(par);
		}
		else
		{
			sql = string.Format(@"update TP_ServiceOrder_Job_Schedule set JobNo=@JobNo,SDate=@SDate,STime=@STime,Detail=@Detail where Id=@Id");
			par = new ConnectSql_mb.cmdParameters("@JobNo", SafeValue.SafeString(jo["JobNo"]), SqlDbType.NVarChar, 100);
			list.Add(par);
			//par = new ConnectSql_mb.cmdParameters("@SDateTime", SafeValue.SafeDate(jo["SDate"] + " " + jo["STime"], DateTime.Now), SqlDbType.DateTime);
			//list.Add(par);
			par = new ConnectSql_mb.cmdParameters("@SDate", SafeValue.SafeDate(jo["SDate1"], DateTime.Now), SqlDbType.DateTime);
			list.Add(par);
			par = new ConnectSql_mb.cmdParameters("@STime", SafeValue.SafeString(jo["STime"], "00:00"), SqlDbType.NVarChar, 10);
			list.Add(par);
			par = new ConnectSql_mb.cmdParameters("@Detail", SafeValue.SafeString(jo["Detail"]), SqlDbType.NVarChar, 300);
			list.Add(par);
			par = new ConnectSql_mb.cmdParameters("@Id", SafeValue.SafeInt(jo["Id"], 0), SqlDbType.Int);
			list.Add(par);
		}

		ConnectSql_mb.sqlResult sqlRe = ConnectSql_mb.ExecuteNonQuery(sql, list);
		string context = "{}";
		int status = 0;
		if (sqlRe.status && sqlRe.context != "0")
		{
			status = 1;

			sql = string.Format(@"select * from TP_ServiceOrder_Job_Schedule
where JobNo='{0}' 
order by SDate,STime", jo["JobNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			context = Common.DataTableToJson(dt);;
		}

		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void TpJob_Schedule_GetList_byUser(string date, string user)
	{
		string sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from TP_ServiceOrder_Job where DATEDIFF(day,TptDate,'{0}')=0 and Driver='{1}'
order by TptDate", date, user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Schedule_GetList_All(string date, string user)
	{
		string sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from TP_ServiceOrder_Job where DATEDIFF(day,TptDate,'{0}')=0
order by TptDate", date, user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Schedule1_GetList_ByTab(string tab, string user)
	{
		string sql_where = "";
		switch (tab)
		{
			case "Today":
                sql_where = "and datediff(day,TptDate,getdate())=0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "This week":
                sql_where = "and datediff(week,tptdate,getdate())=0 and datediff(day,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Later":
                sql_where = "and datediff(day,tptdate,getdate())<0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Past":
                sql_where = "and datediff(week,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			//sql_where = "and JobProgress='Delivered'";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		string sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and Driver='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and Cust='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from TP_ServiceOrder_Job where datediff(year,getdate(),JobDate)=0 {0} {1}
order by TptDate", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Schedule1_GetList_ByNo(string No, string user)
	{
		string sql = string.Format(@"select case when datediff(day,TptDate,getdate())=0  then 'Today' 
when datediff(day,TptDate,getdate())>0 and datediff(week,tptdate,getdate())=0 then 'This week' 
when datediff(day,tptdate,getdate())<0 then 'Later' 
when datediff(week,tptdate,getdate())>0 then 'Past'
else '' end as tab,JobProgress from TP_ServiceOrder_Job where JobNo='{0}'", No);
		DataTable dt_no = ConnectSql_mb.GetDataTable(sql);
		string tab = "";
		if (dt_no.Rows.Count > 0)
		{
			DataRow dr = dt_no.Rows[0];
			string jp = dr["JobProgress"].ToString();
			string t = dr["tab"].ToString();
			//if (jp.Equals("Assigned") || jp.Equals("Confirmed") || jp.Equals("Picked"))
			//{
			//    if (t.Equals("Today") || t.Equals("This week") || t.Equals("Later"))
			//    {
			//        tab = t;
			//    }
			//}
			//if (jp.Equals("Delivered"))
			//{
			//    tab = "Past";
			//}
			tab = t;
		}

		string sql_where = "";
		switch (tab)
		{
			case "Today":
                sql_where = "and datediff(day,TptDate,getdate())=0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "This week":
                sql_where = "and datediff(week,tptdate,getdate())=0 and datediff(day,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Later":
                sql_where = "and datediff(day,tptdate,getdate())<0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			break;
			case "Past":
                sql_where = "and datediff(week,tptdate,getdate())>0 and (JobProgress='Assigned' or JobProgress='Confirmed' or JobProgress='Picked')";
			//sql_where = "and JobProgress='Delivered'";
			break;
			default:
                sql_where = "and 1=0";
			break;
		}

		sql = string.Format(@"select Role,CustId from [user] where name='{0}'", user);
		DataTable dt2 = ConnectSql_mb.GetDataTable(sql);
		string sql_where_2 = "";
		if (dt2.Rows.Count > 0)
		{
			string temp_role = dt2.Rows[0][0].ToString();
			if (temp_role == "Driver")
			{
				sql_where_2 = "and Driver='" + user + "'";
			}

			if (temp_role == "Client")
			{
				sql_where_2 = "and Cust='" + dt2.Rows[0][1] + "'";
			}
		}

		sql = string.Format(@"select Id,JobNo,Driver,VehicleNo,Statuscode,TripCode,JobProgress 
,Pol,TptDate,TptTime,Pod,PickFrm1,DeliveryTo1
from TP_ServiceOrder_Job where datediff(year,getdate(),JobDate)=0 {0} {1}
order by TptDate", sql_where, sql_where_2);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		string json = string.Format(@"{0}tab:'{2}',list:{3}{1}", "{", "}", tab, list);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Schedule1_GetNoticeData(string user)
	{
		string sql = string.Format(@"select Id,JobNo
from TP_ServiceOrder_Job where Driver='{0}' and datediff(day,TptDate,getdate())=0 and (JobProgress='Confirmed' or JobProgress='Picked')
order by TptTime", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_Schedule1_Search(string search, string from, string to)
	{
		DateTime dt_from = SafeValue.SafeDate(from, new DateTime());
		DateTime dt_to = SafeValue.SafeDate(to, new DateTime());
		string where = string.Format(@"and jobdate>='{0}' and jobdate<='{1}'", dt_from.ToString("yyyyMMdd"), dt_to.AddDays(1).ToString("yyyyMMdd"));
		if (search.Trim().Length > 0)
		{
			where += " and JobNo like '" + search + "%'";
		}

		string sql = string.Format(@"select JobNo,JobDate,Vessel,Voyage,Pol,Pod,Eta,Etd
From TP_ServiceOrder_Job 
where StatusCode='USE' {0} 
order by JobDate desc,Id desc", where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void TpJob_job_Detail_Stock_Add(string info, string user, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"insert into CTM_Job_Stock (JobNo,StockStatus,StockQty,PackingQty,Weight,Volume)
values ('{0}','{1}',0,0,0,0)
select Id from CTM_Job_Stock where Id=@@IDENTITY", jo["JobNo"], jo["StockStatus"]);
		string i = ConnectSql_mb.ExecuteScalar(sql);
		if (i.Length > 0)
		{
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.JobNo = jo["JobNo"].ToString();
			l.Controller = user;
			l.Remark = "Add new stock item by " + user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string json = Common.StringToJson(i);
		Common.WriteJsonP(true, json);
	}

	[WebMethod]
	public void MasterData_GetData_TpJob_JobType()
	{
		string sql = string.Format(@"select Id,Code from TP_ServiceOrder_MastData where Type='JobType'");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string port = Common.DataTableToJson(dt);
		Common.WriteJsonP(port);
	}

	#endregion
    #region Master Data
    [WebMethod]
	public void MasterData_GetData_Port(string search)
	{
		string sql = string.Format(@"select top 30 Id,Code,Name from XXPort where Code like '{0}%' order by Code", search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string port = Common.DataTableToJson(dt);
		Common.WriteJsonP(port);
	}

	[WebMethod]
	public void MasterData_GetData_Port_All(string loaded)
	{
		string sql = string.Format(@"with tb1 as (
select ROW_NUMBER()over(order by Name)as rowId, Id,Code,Name from XXPort 
where Code is not null
)
select top 1000 Code,Name from tb1 where rowId>{0} order by rowId", loaded);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string port = Common.DataTableToJson(dt);
		sql = string.Format(@"select count(*) from XXPort where code is not null");
		string count = ConnectSql_mb.ExecuteScalar(sql);
		string json = string.Format(@"{0}list:{2},count:'{3}'{1}", "{", "}", port, count);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_Party(string search)
	{
		string sql = string.Format(@"select top 30 SequenceId,PartyId,Name from xxparty where Status='USE' and PartyId like '{0}%' order by PartyId", search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string party = Common.DataTableToJson(dt);
		Common.WriteJsonP(party);
	}

	[WebMethod]
	public void MasterData_GetData_Party_All(string loaded)
	{
		string sql = string.Format(@"with tb1 as (
select ROW_NUMBER()over(order by Name)as rowId,SequenceId,PartyId,Name from xxparty 
where Status='USE' and PartyId is not null
)
select top 1000 SequenceId,PartyId,Name from tb1 where rowId>{0} order by rowId", loaded);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string party = Common.DataTableToJson(dt);
		sql = string.Format(@"select count(*) from xxparty where Status='USE' and PartyId is not null");
		string count = ConnectSql_mb.ExecuteScalar(sql);
		string json = string.Format(@"{0}list:{2},count:'{3}'{1}", "{", "}", party, count);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_Container_All(string loaded)
	{
		string sql = string.Format(@"with tb1 as (
select ROW_NUMBER()over(order by ContainerNo)as rowId,Id,ContainerNo,ContainerType from Ref_Container 
where StatusCode='USE' and ContainerNo is not null
)
select top 1000 ContainerNo,ContainerType from tb1 where rowId>{0} order by rowId", loaded);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string tb = Common.DataTableToJson(dt);
		sql = string.Format(@"select count(*) from Ref_Container where StatusCode='USE' and ContainerNo is not null");
		string count = ConnectSql_mb.ExecuteScalar(sql);
		string json = string.Format(@"{0}list:{2},count:'{3}'{1}", "{", "}", tb, count);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_ContainerType()
	{
		string sql = string.Format(@"select distinct ContainerType from Ref_ContainerType order by ContainerType");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_PackageType()
	{
		string sql = string.Format(@"select distinct Code from XXUom where CodeType='2' order by Code");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_Driver()
	{
		string sql = string.Format(@"select Id,Code,Name,TowheaderCode from ctm_driver where StatusCode='Active' order by Code");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_Towhead()
	{
		string sql = string.Format(@"select Id,Code,Name from CTM_MastData where Type='towhead' order by Code");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_Trail()
	{
		string sql = string.Format(@"select Id,Code,Name from CTM_MastData where Type='chessis' order by Code");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_GetData_Trip()
	{
		string sql = string.Format(@"select Code,Name from CTM_MastData where Type='tripcode' order by Code");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_Invoice_GetData_Term()
	{
		string sql = string.Format(@"select Code,Name from XXTerm");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_Invoice_GetData_Chg_Part(string loaded)
	{
		string sql = string.Format(@"with tb1 as (
select ROW_NUMBER()over(order by SequenceId) as rowId, SequenceId,REPLACE(REPLACE(ChgcodeId,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeId,REPLACE(REPLACE(ChgcodeDes,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeDes,ChgUnit,GstTypeId,GstP,ArCode,ApCode,ImpExpInd from XXChgCode
where ArCode<>''
)
select top 1000 * from tb1 where rowId>{0} order by rowId", loaded);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string list = Common.DataTableToJson(dt);
		sql = string.Format(@"select count(*) from XXChgCode where ArCode<>''");
		string count = ConnectSql_mb.ExecuteScalar(sql);
		string json = string.Format(@"{0}list:{2},count:'{3}'{1}", "{", "}", list, count);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MasterData_Invoice_GetData_GstType()
	{
		string sql = string.Format(@"select * From XXGstType");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	#endregion

    #region Contact
    [WebMethod]
	public void Contact_GetDataList()
	{
		string sql = string.Format(@"select * from [user]");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Contact_GetData_ByType(string type, string no)
	{
		string sql_where = "";
		if (type == "Customer")
		{
			sql_where = " CustId='" + no + "'";
		}
		else
		{
			sql_where = " name='" + no + "'";
		}

		string sql = string.Format(@"select * from [user] where {0}", sql_where);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataRowToJson(dt);
		Common.WriteJsonP(json);
	}

	#endregion

    #region Message chat
    [WebMethod]
	public void Message_Chat_GetUserList(string user)
	{
		string sql = string.Format(@"select SequenceId,Name from [user] where Name<>'{0}'", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Chat_GetNoReadMsgNo(string user)
	{
		string sql = string.Format(@"select item_chat,count(*) as c from Mobile_Chat where listener='{0}' and item_type='c' and item_status='1' group by item_chat", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Chat_AddMsg(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string sql = string.Format(@"insert into Mobile_Chat (item_chat,item_date,item_type,item_status,speaker,listener,msg) values ('{2}',getdate(),'c',1,'{0}','{1}','{3}'),('{2}',getdate(),'c',0,'{0}','{0}','{3}')
select Id from Mobile_Chat where Id=@@IDENTITY", jo["from"], jo["to"], Common.Json2_Replace(jo["chat"].ToString()), jo["text"]);
		string reId = ConnectSql_mb.ExecuteScalar(sql);
		string json = Common.StringToJson(reId);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Chat_ReceiveMsg(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string result = "0";
		string sql = string.Format(@"with dt as (
select top 1 * from Mobile_Chat where Id={0}
)
update Mobile_Chat set item_status=0
where item_date=(select item_date from dt) and speaker='{1}' and listener='{2}' and item_type='c'", jo["Id"], jo["from"], jo["to"]);
		result = ConnectSql_mb.ExecuteNonQuery(sql).ToString();
		string json = Common.StringToJson(result);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Chat_ReceiveMsg_All(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string result = "0";
		string sql = string.Format(@"update Mobile_Chat set item_status=0
where item_chat='{0}' and listener='{1}' and item_status='1'", Common.Json2_Replace(jo["chat"].ToString()), jo["to"]);
		result = ConnectSql_mb.ExecuteNonQuery(sql).ToString();
		string json = Common.StringToJson(result);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Chat_GetHistoryMsg(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string whereNo = "";
		if (jo["No"] != null && !jo["No"].ToString().Equals("0"))
		{
			//whereNo = "and Id<" + jo["No"];
			whereNo = string.Format(@"and Id<(
select min(t2.Id) from 
(select * from Mobile_Chat where Id={0}) as t1
left outer join (select * from Mobile_Chat where item_chat='{1}' and item_type='c') as t2 on t1.item_date=t2.item_date and t1.msg=t2.msg
)", jo["No"], Common.Json2_Replace(jo["chat"].ToString()));
		}

		string sql = string.Format(@"select * from (select top 10 Id,item_date,speaker,msg  from Mobile_Chat where item_chat='{0}' and item_type='c' and listener='{1}' {2} order by Id desc) as temp order by Id", Common.Json2_Replace(jo["chat"].ToString()), jo["to"], whereNo);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	#endregion

    #region Message Group

    [WebMethod]
	public void Message_Group_GetGroupList(string user)
	{
		string sql = string.Format(@"select c.Id,c.group_name from Mobile_ChatGroup_Det as d 
left outer join Mobile_ChatGroup as c on d.group_name=c.Id
where d.username='{0}' and d.Id is not null", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Group_AddMsg(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string sql = string.Format(@"insert into Mobile_Chat (item_chat,item_date,item_type,item_status,speaker,listener,msg) 
select '{1}',getdate(),'g',case username when '{2}' then 0 else 1 end,'{2}',username,'{3}' from Mobile_ChatGroup_Det where group_name='{0}'
select Id from Mobile_Chat where Id=@@IDENTITY", jo["chatId"], jo["chat"], jo["from"], jo["text"]);
		string reId = ConnectSql_mb.ExecuteScalar(sql);
		string json = Common.StringToJson(reId);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Group_ReceiveMsg(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string result = "0";
		string sql = string.Format(@"with dt as (
select top 1 * from Mobile_Chat where Id={0}
)
update Mobile_Chat set item_status=0
where item_date=(select item_date from dt) and speaker='{1}' and listener='{2}' and item_type='g'", jo["Id"], jo["from"], jo["to"]);
		result = ConnectSql_mb.ExecuteNonQuery(sql).ToString();
		string json = Common.StringToJson(result);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Group_GetNoReadMsgNo(string user)
	{
		string sql = string.Format(@"select item_chat,count(*) as c from Mobile_Chat where listener='{0}' and item_type='g' and item_status='1' group by item_chat", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Group_GetHistoryMsg(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string whereNo = "";
		if (jo["No"] != null && !jo["No"].ToString().Equals("0"))
		{
			//whereNo = "and Id<" + jo["No"];
			whereNo = string.Format(@"and Id<(
select min(t2.Id) from 
(select * from Mobile_Chat where Id={0}) as t1
left outer join (select * from Mobile_Chat where item_chat='{1}' and item_type='g') as t2 on t1.item_date=t2.item_date and t1.msg=t2.msg
)", jo["No"], Common.Json2_Replace(jo["chat"].ToString()));
		}

		string sql = string.Format(@"select * from (select top 10 Id,item_date,speaker,msg  from Mobile_Chat where item_chat='{0}' and item_type='g' and listener='{1}' {2} order by Id desc) as temp order by Id", Common.Json2_Replace(jo["chat"].ToString()), jo["to"], whereNo);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	#region Message group setting
    [WebMethod]
	public void Message_Group_Setting_AddGroup(string info)
	{
		string status = "0";
		string context = "''";
		try
		{
			JObject jo = (JObject)JsonConvert.DeserializeObject(info);
			string sql = string.Format(@"insert into Mobile_ChatGroup(group_name,create_date) values('{0}',getdate())
select Id from Mobile_Chat where Id=@@IDENTITY", jo["name"]);
			string Id = ConnectSql_mb.ExecuteScalar(sql);
			sql = string.Format(@"insert into Mobile_ChatGroup_Det (group_name,username,create_date) values('{0}','{1}',getdate())", Id, jo["user"]);
			ConnectSql_mb.ExecuteNonQuery(sql);
			context = Common.StringToJson(Id);
			status = "1";
		}
		catch { }
		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Message_Group_Setting_ExitGroup(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string sql = string.Format(@"delete from Mobile_ChatGroup_Det where group_name='{0}' and username='{1}'", jo["Id"], jo["user"]);
		string re = ConnectSql_mb.ExecuteNonQuery(sql).ToString();
		Common.WriteJsonP(Common.StringToJson(re));
	}

	[WebMethod]
	public void Message_Group_Setting_GetMombers(string No)
	{
		string sql = string.Format(@"select * from Mobile_ChatGroup_Det where group_name='{0}'", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Group_Setting_GetJoinUser(string No)
	{
		string sql = string.Format(@"select t1.Name,case when t2.Id is null then 0 else 1 end as Joined from (
select * from [user]
) as t1 left outer join(
select * from Mobile_ChatGroup_Det where group_name='{0}'
) as t2 on t2.username=t1.Name", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Group_Setting_JoinToMombers(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JArray ja = (JArray)JsonConvert.DeserializeObject(jo["list"].ToString());
		ArrayList al = new ArrayList();
		string sql = "";
		for (int i = 0; i < ja.Count; i++)
		{
			sql = string.Format(@"insert into Mobile_ChatGroup_Det (group_name,username,create_date) values('{0}','{1}',getdate())", jo["Id"], ja[i]["name"]);
			al.Add(sql);
		}

		string re = ConnectSql_mb.ExecuteTrans(al)[0];
		Common.WriteJsonP(Common.StringToJson(re));
	}

	#endregion

    #region Message Group Search
    [WebMethod]
	public void Message_Group_Search_GetGroupList(string user, string search)
	{
		string sql = string.Format(@"select t1.* from Mobile_ChatGroup as t1 
left outer join(
select * from Mobile_ChatGroup_Det where username='{0}'
)  as t2 on t1.Id=t2.group_name
where t2.Id is null and t1.group_name like '{1}%'", user, search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Message_Group_Search_Joinin(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string sql = string.Format(@"insert into Mobile_ChatGroup_Det (group_name,username,create_date) values('{0}','{1}',getdate())", jo["Id"], jo["user"]);
		string re = ConnectSql_mb.ExecuteNonQuery(sql).ToString();
		string json = Common.StringToJson(re);
		Common.WriteJsonP(json);
	}

	#endregion

    #endregion

    #region Location
    [WebMethod]
	public void Location_Upload(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string result = "0";
		if (jo["user"] != null || jo["user"].ToString() != "")
		{
			string sql = string.Format(@"insert into CTM_Location([type],[user],geo_device,geo_latitude,geo_longitude,note1,create_date_time)
values('Location','{0}','{1}','{2}','{3}','{4}',getdate())", jo["user"], jo["deviceId"], jo["lat"], jo["lng"], jo["platform"]);
			result = ConnectSql_mb.ExecuteNonQuery(sql).ToString();
		}

		string json = Common.StringToJson(result);
		Common.WriteJsonP(json);
	}

	#endregion

    #region Map
    [WebMethod]
	public void Map_GetDataList()
	{
		string sql = string.Format(@"with tb1 as(
select ROW_NUMBER()over(PARTITION BY [user] ORDER BY create_date_time desc) as rowId,* from CTM_Location
)
select [user] as u,geo_device as d,geo_latitude as lat,geo_longitude as lng,note1 as p,create_date_time as date from tb1 where rowId=1");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MapDriver_GetCurrentLocation(string user)
	{
		string sql = string.Format(@"select top 1 [user] as u,geo_device as d,geo_latitude as lat,geo_longitude as lng,note1 as p,create_date_time as date 
from CTM_Location as l where l.[user]='{0}' order by create_date_time desc", user);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	#endregion

    #region My Status
    [WebMethod]
	public void MyStatus_GetData(string user)
	{
		string sql = string.Format(@"select * from (select ROW_NUMBER()over(PARTITION BY Note1 order by createdatetime desc,Id desc) as rowNo,* from CTM_JobEventLog 
where Driver='{0}' and datediff(day,createdatetime,getdate())=0 ) as temp where rowNo=1", user);
		C_MyStatus status = new C_MyStatus();
		status.Driver = user;
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			DataRow dr = dt.Rows[i];
			switch (dr["Note1"].ToString())
			{
				case "towhead":
                    if (dr["Note2"].Equals("1")) { status.Towhead = dr["Towhead"].ToString(); }
				break;
				case "trail":
                    if (dr["Note2"].Equals("1")) { status.Trail = dr["Trail"].ToString(); }
				break;
			}
		}

		string json = Common.ClassToJson(status);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void MyStatus_Towhead_Save(string user, string towhead, string old, string loc)
	{
		C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		l.Controller = user;
		l.Driver = user;
		l.Note1 = "towhead";
		l.Note2 = "0";
		l.Towhead = old;
		l.Lat = jl["Lat"].ToString();
		l.Lng = jl["Lng"].ToString();
		if (l.Towhead.Length > 0)
		{
			l.Job_Detail_EventLog_Add();
		}

		l.Note2 = "1";
		l.Towhead = towhead;
		if (l.Towhead.Length > 0)
		{
			l.Job_Detail_EventLog_Add();
		}

		Common.WriteJsonP(Common.StringToJson("1"));
	}

	[WebMethod]
	public void MyStatus_Trail_Save(string user, string trail, string old, string loc)
	{
		C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		l.Controller = user;
		l.Driver = user;
		l.Note1 = "trail";
		l.Note2 = "0";
		l.Trail = old;
		l.Lat = jl["Lat"].ToString();
		l.Lng = jl["Lng"].ToString();
		if (l.Trail.Length > 0)
		{
			l.Job_Detail_EventLog_Add();
		}

		l.Note2 = "1";
		l.Trail = trail;
		if (l.Trail.Length > 0)
		{
			l.Job_Detail_EventLog_Add();
		}

		Common.WriteJsonP(Common.StringToJson("1"));
	}

	class C_MyStatus
	{
		public string Driver { get; set; }
		public string Towhead { get; set; }
		public string Trail { get; set; }
	}

	#endregion

    #region Invoice
    [WebMethod]
	public void Invoice_GetDataList(string search)
	{
		string sql = string.Format(@"select top 30 DocNo,DocType,DocDate,PartyTo,MastRefNo,JobRefNo,Term,DocAmt,LocAmt,BalanceAmt 
from XAArInvoice where DocNo like '{0}%' and (DocType='IV' or DocType='DN') order by SequenceId desc", search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Invoice_New_Save(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string AcCode = EzshipHelper.GetAccArCode("", "SGD");
		DateTime DocDate = DateTime.Now;
		string counterType = "AR-IV";
		if (jo["DocType"].ToString() == "DN")
			counterType = "AR-DN";
		string invN = C2Setup.GetNextNo(jo["DocType"].ToString(), counterType, DocDate);
		string sql = string.Format(@"insert into XAArInvoice (AcYear,AcPeriod,AcCode,AcSource,DocType,DocNo,DocDate,DocDueDate,MastRefNo,JobRefNo,CurrencyId,ExRate,Term,DocAmt,LocAmt,BalanceAmt,ExportInd,CancelInd,UserId,EntryDate)
values (YEAR(getdate()),MONTH(getdate()),'{0}','DB','{1}','{2}',GETDATE(),getdate(),'0','0','SGD',1,'CASH',0,0,0,'N','N','{3}',getdate())", AcCode, jo["DocType"], invN, jo["User"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo("", counterType, invN, DocDate);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.Note3 = invN;
			l.Remark = "Add new Invoice by " + jo["User"];
			l.Controller = jo["User"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string context = Common.StringToJson(invN);
		Common.WriteJsonP(i, context);
	}

	[WebMethod]
	public void Invoice_Detail_GetData(string No)
	{
		string sql = string.Format(@"select * from XAArInvoice where DocNo='{0}'", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string mast = Common.DataRowToJson(dt);

		sql = string.Format(@"select * from XAArInvoiceDet where DocNo='{0}'", No);
		dt = ConnectSql_mb.GetDataTable(sql);
		string det = Common.DataTableToJson(dt);

		//sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng from CTM_JobEventLog where JobNo='{0}' order by CreateDateTime desc", No);
		sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where Note3='{0}' order by CreateDateTime desc", No);
		dt = ConnectSql_mb.GetDataTable(sql);
		DataRow dr = null;
		for (int i1 = 0; i1 < dt.Rows.Count; i1++)
		{
			dr = dt.Rows[i1];
			if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
			{
				string path = dr["FilePath"].ToString();
				path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
				dr["FilePath"] = path;
			}
		}

		string log = Common.DataTableToJson(dt);

		//sql = string.Format(@"select * from XAArInvoice_Attachment where RefNo='{0}'", No);
		//dt = ConnectSql_mb.GetDataTable(sql);
		//string attachment = Common.DataTableToJson(dt);

		//string json = string.Format(@"{0}mast:{2},det:{3},log:{4},attachment:{5}{1}", "{", "}", mast, det, log, attachment);
		string json = string.Format(@"{0}mast:{2},det:{3},log:{4}{1}", "{", "}", mast, det, log);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Invoice_Detail_Mast_Save(string info, string loc, string user)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"update XAArInvoice set PartyTo='{0}',Term='{1}' where SequenceId='{2}'", jo["PartyTo"], jo["Term"], jo["SequenceId"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		//if (i == 1)
		//{
		//    C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
		//    l.Note3 = jo["DocNo"].ToString();
		//    string t_remark = "Invoice";
		//    if (jo["DocType"].ToString() == "PVG")
		//    {
		//        t_remark = "Payment";
		//    }
		//    if (jo["DocType"].ToString() == "QT")
		//    {
		//        t_remark = "Quotation";
		//    }
		//    l.Remark = "Update " + t_remark;
		//    l.Controller = user;
		//    l.Lat = jl["Lat"].ToString();
		//    l.Lng = jl["Lng"].ToString();
		//    l.Job_Detail_EventLog_Add();

		//}
		string json = Common.StringToJson(i.ToString());
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Invoice_Detail_Item_GetNew()
	{
		string sql = string.Format(@"select * from XAArInvoiceDet where SequenceId=-1");
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string info = Common.DataRowToJson(dt, true);

		Common.WriteJsonP(info);
	}

	[WebMethod]
	public void Invoice_Detail_Item_Save(string info, string loc, string user)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = "";
		if (jo["SequenceId"].ToString() == "")
		{
			sql = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,GstType,Qty,
Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt)
values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',
'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", jo["DocId"], jo["DocNo"], jo["DocType"], jo["DocLineNo"], jo["AcCode"], jo["AcSource"], jo["ChgCode"], jo["ChgDes1"], jo["GstType"], jo["Qty"],
  jo["Price"], jo["Unit"], jo["Currency"], jo["ExRate"], jo["Gst"], jo["GstAmt"], jo["DocAmt"], jo["LocAmt"], jo["LineLocAmt"]);
		}
		else
		{
			sql = string.Format(@"update XAArInvoiceDet set AcCode='{1}',ChgCode='{2}',ChgDes1='{3}',GstType='{4}',Qty='{5}'
,Price='{6}',Gst='{7}',GstAmt='{8}',DocAmt='{9}',LocAmt='{10}',LineLocAmt='{11}'
where SequenceId='{0}'", jo["SequenceId"], jo["AcCode"], jo["ChgCode"], jo["ChgDes1"], jo["GstType"], jo["Qty"], jo["Price"], jo["Gst"], jo["GstAmt"], jo["DocAmt"], jo["LocAmt"], jo["LineLocAmt"]);
		}

		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string context = "{}";
		if (i == 1)
		{
			UpdateMaster(jo["DocId"].ToString());

			sql = string.Format(@"select * from XAArInvoice where DocNo='{0}'", jo["DocNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			string mast = Common.DataRowToJson(dt);

			sql = string.Format(@"select * from XAArInvoiceDet where DocNo='{0}'", jo["DocNo"]);
			dt = ConnectSql_mb.GetDataTable(sql);
			string det = Common.DataTableToJson(dt);

			context = string.Format(@"{0}mast:{2},det:{3}{1}", "{", "}", mast, det);

			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.Note3 = jo["DocNo"].ToString();
			string t_remark = "Invoice";
			if (jo["DocType"].ToString() == "PVG")
			{
				t_remark = "Payment";
			}

			if (jo["DocType"].ToString() == "QT")
			{
				t_remark = "Quotation";
			}

			string t_remark1 = jo["SequenceId"].ToString() == "" ? "Add" : "Update";
			l.Remark = t_remark1 + " " + t_remark + " Item by " + user;
			l.Controller = user;
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		Common.WriteJsonP(i, context);
	}

	private void UpdateMaster(string docId)
	{
		//string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
		//C2.Manager.ORManager.ExecuteCommand(sql);
		string sql = "";
		decimal docAmt = 0;
		decimal locAmt = 0;
		sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
		DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
		for (int i = 0; i < tab.Rows.Count; i++)
		{
			if (tab.Rows[i]["AcSource"].ToString() == "CR")
			{
				docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
				locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
			}
			else
			{
				docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
				locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
			}
		}

		decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

		balAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

		sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
		C2.Manager.ORManager.ExecuteCommand(sql);
	}

	[WebMethod]
	public void Invoice_Detail_Attachment_Add(string info, string loc, string user)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string sql = string.Format(@"insert into XAArInvoice_Attachment (JobType,RefNo,FileType,FileName,FilePath,CreateBy,CreateDateTime,FileNote) values('{0}','{1}','{2}','{3}','{4}','{5}',getdate(),'{6}')", jo["JobType"], jo["RefNo"], jo["FileType"], jo["FileName"], jo["FilePath"], jo["CreateBy"], jo["FileNote"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string result = "0";
		string context = "Error";
		if (i == 1)
		{
			result = "1";
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.Note3 = jo["RefNo"].ToString();
			l.Note1 = jo["FilePath"].ToString();
			l.Note1Type = jo["FileType"].ToString();
			l.Controller = user;
			l.Remark = jo["CreateBy"] + " post a " + jo["FileType"] + " file";
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();

			sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where Note3='{0}' order by CreateDateTime desc ", jo["RefNo"]);
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			DataRow dr = null;
			for (int i1 = 0; i1 < dt.Rows.Count; i1++)
			{
				dr = dt.Rows[i1];
				if (RebuildImage.Image_ExistOtherSize(dr["FilePath"].ToString(), dr["type"].ToString(), 500))
				{
					string path = dr["FilePath"].ToString();
					path = path.Substring(0, path.LastIndexOf('/')) + "/" + 500 + path.Substring(path.LastIndexOf('/'));
					dr["FilePath"] = path;
				}
			}

			context = Common.DataTableToJson(dt);
		}
		else
		{
			context = Common.StringToJson(context);
		}

		Common.WriteJsonP(result, context);
	}

	[WebMethod]
	public void Invoice_Detail_HisotryLog_GetList(string No)
	{
		string sql = string.Format(@"select Id,CreateDateTime,Controller,JobNo,Remark,Lat,Lng,Note1Type as type,Note1 as FilePath from CTM_JobEventLog where Note3='{0}' order by CreateDateTime desc", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string log = Common.DataTableToJson(dt);
		Common.WriteJsonP(log);
	}

	#endregion

    #region Quotation
    [WebMethod]
	public void Quotation_GetDataList(string search)
	{
		string sql = string.Format(@"select top 30 DocNo,DocType,DocDate,PartyTo,MastRefNo,JobRefNo,Term,DocAmt,LocAmt,BalanceAmt 
from XAArInvoice where DocNo like '{0}%' and DocType='QT' order by SequenceId desc", search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Quotation_New_Save(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string AcCode = EzshipHelper.GetAccArCode("", "SGD");
		DateTime DocDate = DateTime.Now;
		string counterType = "CTM-QT";
		string invN = C2Setup.GetNextNo(jo["DocType"].ToString(), counterType, DocDate);
		string sql = string.Format(@"insert into XAArInvoice (AcYear,AcPeriod,AcCode,AcSource,DocType,DocNo,DocDate,DocDueDate,MastRefNo,JobRefNo,CurrencyId,ExRate,Term,DocAmt,LocAmt,BalanceAmt,ExportInd,CancelInd,UserId,EntryDate)
values (YEAR(getdate()),MONTH(getdate()),'{0}','DB','{1}','{2}',GETDATE(),getdate(),'0','0','SGD',1,'CASH',0,0,0,'N','N','{3}',getdate())", AcCode, jo["DocType"], invN, jo["User"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo("", counterType, invN, DocDate);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.Note3 = invN;
			l.Remark = "Add new Quotation by " + jo["User"];
			l.Controller = jo["User"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string context = Common.StringToJson(invN);
		Common.WriteJsonP(i, context);
	}

	#endregion

    #region Payment
    [WebMethod]
	public void Payment_GetDataList(string search)
	{
		string sql = string.Format(@"select top 30 DocNo,DocType,DocDate,PartyTo,MastRefNo,JobRefNo,Term,DocAmt,LocAmt,BalanceAmt 
from XAArInvoice where DocNo like '{0}%' and DocType='PVG' order by SequenceId desc", search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Payment_New_Save(string info, string loc)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JObject jl = (JObject)JsonConvert.DeserializeObject(loc);
		string AcCode = EzshipHelper.GetAccArCode("", "SGD");
		DateTime DocDate = DateTime.Now;
		string counterType = "CTM-PVG";
		string invN = C2Setup.GetNextNo(jo["DocType"].ToString(), counterType, DocDate);
		string sql = string.Format(@"insert into XAArInvoice (AcYear,AcPeriod,AcCode,AcSource,DocType,DocNo,DocDate,DocDueDate,MastRefNo,JobRefNo,CurrencyId,ExRate,Term,DocAmt,LocAmt,BalanceAmt,ExportInd,CancelInd,UserId,EntryDate)
values (YEAR(getdate()),MONTH(getdate()),'{0}','DB','{1}','{2}',GETDATE(),getdate(),'0','0','SGD',1,'CASH',0,0,0,'N','N','{3}',getdate())", AcCode, jo["DocType"], invN, jo["User"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		if (i == 1)
		{
			C2Setup.SetNextNo("", counterType, invN, DocDate);
			C_Job_Detail_EventLog l = new C_Job_Detail_EventLog();
			l.Note3 = invN;
			l.Remark = "Add new Payment by " + jo["User"];
			l.Controller = jo["User"].ToString();
			l.Lat = jl["Lat"].ToString();
			l.Lng = jl["Lng"].ToString();
			l.Job_Detail_EventLog_Add();
		}

		string context = Common.StringToJson(invN);
		Common.WriteJsonP(i, context);
	}

	#endregion

    #region Setup
    [WebMethod]
	public void Setup_Driver_GetDateil(string No)
	{
		string sql = string.Format(@"select * from CTM_Driver where Id='{0}'", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataRowToJson(dt, true);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Setup_Driver_Detail_Save(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JToken Id = jo["Id"];
		string sql = "";
		if (Id == null || Id.ToString() == "" || Id.ToString() == "0")
		{
			sql = string.Format(@"insert into CTM_Driver (Code,Name,Tel,ICNo,Remark,Isstaff,TowheaderCode,ServiceLevel,StatusCode)
values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", jo["Code"], jo["Name"], jo["Tel"], jo["ICNo"], jo["Remark"], jo["Isstaff"], jo["TowheaderCode"], jo["ServiceLevel"], jo["StatusCode"]);
		}
		else
		{
			sql = string.Format(@"update CTM_Driver set Name='{0}',Tel='{1}',ICNo='{2}',Remark='{3}',Isstaff='{4}',TowheaderCode='{5}',ServiceLevel='{6}',StatusCode='{7}' where Id='{8}'", jo["Name"], jo["Tel"], jo["ICNo"], jo["Remark"], jo["Isstaff"], jo["TowheaderCode"], jo["ServiceLevel"], jo["StatusCode"], jo["Id"]);
		}

		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string json = Common.StringToJson(i.ToString());
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Setup_Vehicle_GetDateil(string No)
	{
		string sql = string.Format(@"select * from CTM_MastData where Id='{0}'", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataRowToJson(dt, true);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Setup_Vehicle_Detail_Save(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		JToken Id = jo["Id"];
		string sql = "";
		if (Id == null || Id.ToString() == "" || Id.ToString() == "0")
		{
			sql = string.Format(@"insert into CTM_MastData (Code,Name,Type) values ('{0}','{1}','{2}')", jo["Code"], jo["Name"], jo["Type"]);
		}
		else
		{
			sql = string.Format(@"update CTM_MastData set Code='{1}',Name='{2}',Type='{3}' where Id='{0}'", jo["Id"], jo["Code"], jo["Name"], jo["Type"]);
		}

		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		string json = Common.StringToJson(i.ToString());
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Setup_Party_GetDetail(string No)
	{
		string sql = string.Format(@"select * from XXParty where SequenceId='{0}'", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataRowToJson(dt, true);
		Common.WriteJsonP(json);
	}

	#endregion

    #region Activity log
    [WebMethod]
	public void ActivityLog_Save(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
	}

	[WebMethod]
	public void ActivityLog_GetData_ByJobNo(string JobNo, string Type)
	{
		string sql = "";
		if (Type.ToLower() == "lcl")
		{
			sql = string.Format(@"with tb1 as (
select 'Assigner' as name,CreateBy as value,JobNo from tpt_job where JobNo='{0}' and isnull(CreateBy,'')<>''
union all
select 'Assigner' as name,UpdateBy as value,JobNo from tpt_job where JobNo='{0}' and isnull(UpdateBy,'')<>''
union all
select 'Driver' as name,Driver as value,JobNo from tpt_job where JobNo='{0}' and isnull(Driver,'')<>''
union all
select 'Customer' as name,u.Name as value,JobNo 
from tpt_job as job left outer join [user] as u on job.Cust=u.CustId 
where JobNo='{0}' and isnull(u.Name,'')<>''
),
tb2 as (
select CreateDateTime,Controller,Remark,Lat,Lng,JobNo from CTM_JobEventLog where JobNo='{0}'
)
select distinct tb1.name,tb1.value,tb1.JobNo,tb2.CreateDateTime,tb2.Controller,tb2.Remark,tb2.Lat,tb2.Lng 
from tb1 left outer join (select top 1 * from tb2 order by CreateDateTime desc) as tb2 on tb1.JobNo=tb2.JobNo", JobNo);
		}

		if (Type.ToLower() == "fcl")
		{
			sql = string.Format(@"with tb1 as (
select 'Assigner' as name,CreateBy as value,JobNo from CTM_Job where JobNo='{0}' and isnull(CreateBy,'')<>''
union all
select 'Assigner' as name,UpdateBy as value,JobNo from CTM_Job where JobNo='{0}' and isnull(UpdateBy,'')<>''
union all
select 'Driver' as name,Drivercode as value,JobNo from CTM_JobDet2 where JobNo='{0}' and isnull(Drivercode,'')<>''
union all
select 'Customer' as name,u.Name as value,JobNo 
from CTM_Job as job left outer join [user] as u on job.ClientId=u.CustId 
where JobNo='{0}' and isnull(u.Name,'')<>''
),
tb2 as (
select CreateDateTime,Controller,Remark,Lat,Lng,JobNo from CTM_JobEventLog where JobNo='{0}'
)
select distinct tb1.name,tb1.value,tb1.JobNo,tb2.CreateDateTime,tb2.Controller,tb2.Remark,tb2.Lat,tb2.Lng 
from tb1 left outer join (select top 1 * from tb2 order by CreateDateTime desc) as tb2 on tb1.JobNo=tb2.JobNo", JobNo);
		}

		string re_status = "0";
		string context = "";
		if (sql.Length > 0)
		{
			re_status = "1";
			DataTable dt = ConnectSql_mb.GetDataTable(sql);
			context = Common.DataTableToJson(dt);
		}

		Common.WriteJsonP(re_status, context);
	}

	#endregion

    #region Stock Balance/Movement
    [WebMethod]
	public void Stock_GetDetail(string No)
	{
		string sql = string.Format(@"select * from CTM_Job_Stock where Id={0}", No);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataRowToJson_150205(dt, true);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void Stock_Save(string info)
	{
		JObject jo = (JObject)JsonConvert.DeserializeObject(info);
		string sql = string.Format(@"update CTM_Job_Stock set StockStatus='{1}',StockDescription='{2}',StockMarking='{3}',StockQty={4},StockUnit='{5}',
PackingQty={6},PackingUnit='{7}',PackingDimention='{8}',Weight={9},Volume={10} where Id={0}", jo["Id"], jo["StockStatus"], jo["StockDescription"], jo["StockMarking"], jo["StockQty"], jo["StockUnit"], jo["PackingQty"], jo["PackingUnit"], jo["PackingDimention"], jo["Weight"], jo["Volume"]);
		int i = ConnectSql_mb.ExecuteNonQuery(sql);
		Common.WriteJsonP(Common.StringToJson(i.ToString()));
	}

	[WebMethod]
	public void StockBalance_GetList(string search)
	{
		string sql = string.Format(@"select * from CTM_Job_Stock where StockStatus='IN' and JobNo like '{0}%'", search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson_150205(dt);
		Common.WriteJsonP(json);
	}

	[WebMethod]
	public void StockMovement_GetList(string search)
	{
		string sql = string.Format(@"select * from CTM_Job_Stock where StockStatus='OUT' and JobNo like '{0}%'", search);
		DataTable dt = ConnectSql_mb.GetDataTable(sql);
		string json = Common.DataTableToJson_150205(dt);
		Common.WriteJsonP(json);
	}

	#endregion

    #region Vehicle operate
    [WebMethod]
	public void Vehcile_FuelLog_GetList(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");
		string sql = string.Format(@"select * from Vehicle_FuelLog where VehicleNo like @VehicleNo order by Id desc");

		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["search"]+"%", SqlDbType.NVarChar, 100);
		list.Add(cpar);
		DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
		context = Common.DataTableToJson(dt);
		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Vehcile_FuelLog_GetData(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");
		string sql = string.Format(@"select * from Vehicle_FuelLog where Id=@Id");

		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
		list.Add(cpar);
		DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
		context = Common.DataRowToJson(dt, true);
		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Vehcile_FuelLog_Save(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");
		string sql = "";
		if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
		{
			sql = string.Format(@"insert into Vehicle_FuelLog (VehicleNo,CreateDateTime,[Type],Volume,Amount,Note) values(@VehicleNo,getdate(),@Type,@Volume,@Amount,@Note)");
		}
		else
		{
			sql = string.Format(@"update Vehicle_FuelLog set VehicleNo=@VehicleNo,[Type]=@Type,Volume=@Volume,Amount=@Amount,Note=@Note where Id=@Id");
		}

		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["VehicleNo"], SqlDbType.NVarChar, 100);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@Type", job["Type"], SqlDbType.NVarChar, 10);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@Volume", job["Volume"], SqlDbType.Decimal);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@Amount", job["Amount"], SqlDbType.Decimal);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@Note", job["Note"], SqlDbType.NVarChar, 300);
		list.Add(cpar);
		ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
		if (!re.status || re.context.Equals("0"))
		{
			status = "0";
			context = Common.StringToJson("Save Error");
		}

		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Vehcile_Mileage_GetList(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");
		string sql = string.Format(@"select * from Vehicle_Mileage where VehicleNo like @VehicleNo order by Id desc");

		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["search"] + "%", SqlDbType.NVarChar, 100);
		list.Add(cpar);
		DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
		context = Common.DataTableToJson(dt);
		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Vehcile_Mileage_GetData(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");
		string sql = string.Format(@"select * from Vehicle_Mileage where Id=@Id");

		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
		list.Add(cpar);
		DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
		context = Common.DataRowToJson(dt, true);
		Common.WriteJsonP(status, context);
	}

	[WebMethod]
	public void Vehcile_Mileage_Save(string info)
	{
		string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
		JObject job = (JObject)JsonConvert.DeserializeObject(info_);

		List<ConnectSql_mb.cmdParameters> list = null;
		ConnectSql_mb.cmdParameters cpar = null;
		string status = "1";
		string context = Common.StringToJson("");
		string sql = "";
		if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
		{
			sql = string.Format(@"insert into Vehicle_Mileage (VehicleNo,CreateDateTime,Value,Note) values(@VehicleNo,getdate(),@Value,@Note)");
		}
		else
		{
			sql = string.Format(@"update Vehicle_Mileage set VehicleNo=@VehicleNo,Value=@Value,Note=@Note where Id=@Id");
		}

		list = new List<ConnectSql_mb.cmdParameters>();
		cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["VehicleNo"], SqlDbType.NVarChar, 100);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@Value", job["Value"], SqlDbType.Decimal);
		list.Add(cpar);
		cpar = new ConnectSql_mb.cmdParameters("@Note", job["Note"], SqlDbType.NVarChar, 300);
		list.Add(cpar);
		ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
		if (!re.status || re.context.Equals("0"))
		{
			status = "0";
			context = Common.StringToJson("Save Error");
		}

		Common.WriteJsonP(status, context);
	}
	
	
    //==================== IssueReport



    [WebMethod]
    public void Vehcile_IssueReport_GetList(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select * from Vehicle_IssueReport where VehicleNo like @VehicleNo order by Id desc");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["search"] + "%", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataTableToJson(dt);
        Common.WriteJsonP(status, context);

    }
    [WebMethod]
    public void Vehcile_IssueReport_GetData(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = string.Format(@"select * from Vehicle_IssueReport where Id=@Id");

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        list.Add(cpar);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        context = Common.DataRowToJson(dt, true);
        Common.WriteJsonP(status, context);

    }

    [WebMethod]
    public void Vehcile_IssueReport_Save(string info)
    {
        string info_ = HttpUtility.UrlDecode(Server.UrlDecode(info));
        JObject job = (JObject)JsonConvert.DeserializeObject(info_);

        List<ConnectSql_mb.cmdParameters> list = null;
        ConnectSql_mb.cmdParameters cpar = null;
        string status = "1";
        string context = Common.StringToJson("");
        string sql = "";
        if (job["Id"].ToString().Equals("") || job["Id"].ToString().Equals("0"))
        {
            sql = string.Format(@"insert into Vehicle_IssueReport (VehicleNo,CreateDateTime,Description,ActionTaken,Note) values(@VehicleNo,getdate(),@Description,@ActionTaken,@Note)");
        }
        else
        {
            sql = string.Format(@"update Vehicle_IssueReport set VehicleNo=@VehicleNo,Description=@Description,ActionTaken=@ActionTaken,Note=@Note where Id=@Id");
        }

        list = new List<ConnectSql_mb.cmdParameters>();
        cpar = new ConnectSql_mb.cmdParameters("@Id", job["Id"], SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@VehicleNo", job["VehicleNo"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Description", job["Description"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ActionTaken", job["ActionTaken"], SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Note", job["Note"], SqlDbType.NVarChar, 300);
        list.Add(cpar);
        ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (!re.status || re.context.Equals("0"))
        {
            status = "0";
            context = Common.StringToJson("Save Error");
        }
        Common.WriteJsonP(status, context);

    }

    #endregion


    //[WebMethod]
    //public void test()
    //{
    //    //RebuildImage.Rebuild(path, 500);
    //    //string sql = string.Format(@"select * From Menu1");
    //    //DataTable dt = ConnectSql_mb.GetDataTable(sql);
    //    //string json = Common.DataTableToJson(dt);
    //    //Common.WriteJsonP(json);

    //    long num2 = DateTime.Now.Ticks;
    //    string json = Common.StringToJson(num2.ToString());
    //    Common.WriteJsonP(json);
    //}

}
