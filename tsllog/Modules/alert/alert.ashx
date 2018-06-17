<%@ WebHandler Language="C#" Class="Alert" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Data;
using Wilson.ORMapper;
using System.Net;


public class Alert : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string _type = context.Request.QueryString["type"] ?? "D";

        //throw new Exception(string.Format("{0:yyyy-MM-dd}/{1:yyyy-MM-dd}/{2:yyyy-MM-dd}/{3:yyyy-MM-dd}",DateTime.Today.AddDays(30),DateTime.Today.AddDays(60),DateTime.Today.AddDays(90),DateTime.Today.AddDays(180)));

        string days1 = "";
        string days2 = "";

        for(int d=270; d>30; d-=7)
        {
            days1 += string.Format(",'{0:yyyy-MM-dd}'",DateTime.Today.AddDays(d));
        }
        for(int d=30; d>-30; d--)
        {
            days1 += string.Format(",'{0:yyyy-MM-dd}'",DateTime.Today.AddDays(d));
        }

        //for(int d=90; d>30; d-=7)
        //{
        //    days2 += string.Format(",'{0:yyyy-MM-dd}'",DateTime.Today.AddDays(d));
        //}
        for(int d=30; d>-30; d--)
        {
            days2 += string.Format(",'{0:yyyy-MM-dd}'",DateTime.Today.AddDays(d));
        }

        //string _pp = string.Format("select Id, ParentId, EntityName,EntityType, Note3, Date3, DateDiff(day,'{0:yyyy-MM-dd}', date3) as Days from X0 where RowStatus='USE' and RowType='CREW' and Date3 in ({1}) Order By Date3 Desc ", DateTime.Today, days1.Substring(1));
        //string _wp = string.Format("select Id, ParentId, EntityName,EntityType, Note7, Date4, DateDiff(day,'{0:yyyy-MM-dd}', date4) as Days from X0 where RowStatus='USE' and RowType='CREW' and Date4 in ({1}) Order By Date4 Desc ", DateTime.Today, days2.Substring(1));
        //string _cert = string.Format("select Id, ParentId,ItemName,ItemType, User1,ExpiryDate, DateDiff(day,'{0:yyyy-MM-dd}', ExpiryDate) as Days from X2 where RowStatus='USE' and RowType='CERT' and Status2='RENEW' and ExpiryDate in ({1}) Order By ExpiryDate Desc ", DateTime.Today, days2.Substring(1));
        //string _wip = string.Format("select Id, ParentId,ItemName,ItemType, User1,ExpiryDate,Date8,DateDiff(day,'{0:yyyy-MM-dd}', ExpiryDate) as Days from X2 where RowStatus='USE' and RowType='CERT' and Status2='INPROCESS' and Date8 < '{0:yyyy-MM-dd}' Order By ExpiryDate Desc ", DateTime.Today, days2.Substring(1));

        string _hr = string.Format(@"select Name,IcNo,Date2 from hr_Person where Date2 in ({0})",days2.Substring(1));

        DataTable dt =  new DataTable();
        dt = Helper.Sql.List(_hr);
        if(dt.Rows.Count>0)
        {
            Helper.Logic.AlertIcNo(dt);
        }


        string _vehicle = string.Format(@"select VehicleCode,VehicleType,ContractNo,VpcExpiryDate from Ref_Vehicle where VehicleStatus='Active' and VpcExpiryDate in ({0})",days2.Substring(1));

        dt =  new DataTable();
        dt = Helper.Sql.List(_vehicle);
        if(dt.Rows.Count>0)
        {
            Helper.Logic.AlertVehicle(dt);
        }

        //dt = Helper.Sql.List(_wip);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DataRow dr = dt.Rows[i];
        //    Helper.Logic.AlertWip( Helper.Safe.SafeInt(dr["Days"]),
        //     Helper.Safe.SafeInt(dr["Id"]),
        //     Helper.Safe.SafeInt(dr["ParentId"]),
        //     Helper.Safe.SafeString(dr["ItemName"]),
        //     Helper.Safe.SafeString(dr["ItemType"]),
        //     Helper.Safe.SafeString(dr["User1"]),
        //     string.Format("{1:dd/MM/yyyy}/EXPIRY:{0:dd/MM/yyyy}",dr["ExpiryDate"],dr["Date8"])
        //    );
        //}

        //return;

        //dt = Helper.Sql.List(_cert);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DataRow dr = dt.Rows[i];
        //    Helper.Logic.AlertCert( Helper.Safe.SafeInt(dr["Days"]),
        //     Helper.Safe.SafeInt(dr["Id"]),
        //     Helper.Safe.SafeInt(dr["ParentId"]),
        //     Helper.Safe.SafeString(dr["ItemName"]),
        //     Helper.Safe.SafeString(dr["ItemType"]),
        //     Helper.Safe.SafeString(dr["User1"]),
        //     string.Format("{0:dd/MM/yyyy}",dr["ExpiryDate"])
        //    );
        //}



        //dt = Helper.Sql.List(_pp);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DataRow dr = dt.Rows[i];
        //    Helper.Logic.AlertPassport( Helper.Safe.SafeInt(dr["Days"]),
        //     Helper.Safe.SafeInt(dr["Id"]),
        //     Helper.Safe.SafeInt(dr["ParentId"]),
        //     Helper.Safe.SafeString(dr["EntityName"]),
        //     Helper.Safe.SafeString(dr["EntityType"]),
        //     Helper.Safe.SafeString(dr["Note3"]),
        //     string.Format("{0:dd/MM/yyyy}",dr["Date3"])
        //    );
        //}

        //       dt = Helper.Sql.List(_wp);
        //     for (int i = 0; i < dt.Rows.Count; i++)
        //     {
        //DataRow dr = dt.Rows[i];
        //        Helper.Logic.AlertWorkPermit( Helper.Safe.SafeInt(dr["Days"]),
        //   Helper.Safe.SafeInt(dr["Id"]),
        //   Helper.Safe.SafeInt(dr["ParentId"]),
        //Helper.Safe.SafeString(dr["EntityName"]),
        //Helper.Safe.SafeString(dr["EntityType"]),
        //Helper.Safe.SafeString(dr["Note7"]),
        //string.Format("{0:dd/MM/yyyy}",dr["Date4"])
        //  );
        //     }


        StringBuilder sb = new StringBuilder();
        try
        {
            System.Threading.Thread.Sleep(10000);

            int year = DateTime.Now.Year;
            int hour = DateTime.Now.Hour;

        }
        catch (Exception ex)
        {
            sb.Append(ex.Message);
            sb.Append(ex.StackTrace);
        }
        context.Response.Write(sb.ToString());
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



}