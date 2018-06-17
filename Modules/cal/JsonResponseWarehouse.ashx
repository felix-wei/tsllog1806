<%@ WebHandler Language="C#" Class="JsonResponseWarehouse" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Data;

public class JsonResponseWarehouse : IHttpHandler, IRequiresSessionState 
{


    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        DateTime start = new DateTime(1970, 1, 1);
        DateTime end = new DateTime(1970, 1, 1);

        start = start.AddSeconds(double.Parse(context.Request.QueryString["start"]));
        end = end.AddSeconds(double.Parse(context.Request.QueryString["end"]));
        
        
        String result = String.Empty;

        result += "[";

        List<int> idList = new List<int>();
		
		
		
        foreach (CalendarEvent cevent in getEvents(start, end))
        {
            result += convertCalendarEventIntoString(cevent);
            idList.Add(cevent.id);
        }

        if (result.EndsWith(","))
        {
            result = result.Substring(0, result.Length - 1);
        }

        result += "]";
        //store list of event ids in Session, so that it can be accessed in web methods
        context.Session["idList"] = idList;

        context.Response.Write(result);
    }

 public List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
       
        List<CalendarEvent> events = new List<CalendarEvent>();
    
        string _sql = @"
SELECT  i.Id,i.JobNo,i.DoType,i.Date1,j.JobType
FROM         JobInventory i, JobInfo j
WHERE  i.JobNo=j.JobNo and  (i.Status1 <> 'Cancel') and i.DoType in ('WI','WO') and i.Date1>='{0:yyyy-MM-dd} 00:00' and i.Date1<='{1:yyyy-MM-dd} 23:59'

";
        string __sql = string.Format(_sql, start, end);
        //throw new Exception(__sql);
        //SqlCommand cmd = new SqlCommand(__sql);

        DataTable dt = Helper.Sql.List(__sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
                    CalendarEvent cevent = new CalendarEvent();
                    cevent.id = Helper.Safe.SafeInt(dr["Id"]);
                    //cevent.title = string.Format("<a href='ShowJob(\"{0}\")'>{0}</a>:{1}/{2}", dr["JobNo"], dr["DoType"], dr["JobType"]);
                    cevent.title = string.Format("{0}/{1}/{2}", dr["JobNo"], dr["JobType"],dr["DoType"]);
                    cevent.description = ""; // string.Format("{0}\r\n{1}\r\n{2}",dr["VolumneRmk"],dr["Contact"],dr["DestinationAdd"]);
                    cevent.start = Helper.Safe.SafeDate(dr["Date1"]);
                    cevent.end = Helper.Safe.SafeDate(dr["Date1"]);
                    events.Add(cevent);
         
        }
        return events;
    }	
	
    private String convertCalendarEventIntoString(CalendarEvent cevent)
    {
        String allDay = "true";
        if (ConvertToTimestamp(cevent.start).ToString().Equals(ConvertToTimestamp(cevent.end).ToString()))
        {

            if (cevent.start.Hour == 0 && cevent.start.Minute == 0 && cevent.start.Second == 0)
            {
                allDay = "true";
            }
            else
            {
                allDay = "false";
            }
        }
        else
        {
            if (cevent.start.Hour == 0 && cevent.start.Minute == 0 && cevent.start.Second == 0
                && cevent.end.Hour == 0 && cevent.end.Minute == 0 && cevent.end.Second == 0)
            {
                allDay = "true";
            }
            else
            {
                allDay = "false";
            }
        }
        return    "{" +
                  "id: '" + cevent.id + "'," +
                  "title: '" + HttpContext.Current.Server.HtmlEncode(cevent.title) + "'," +
                  "start:  " + ConvertToTimestamp(cevent.start).ToString() + "," +
                  "end: " + ConvertToTimestamp(cevent.end).ToString() + "," +
                  "allDay:" + allDay + "," +
                  "className:'" + getColor(cevent.title) + "'," +
                  "description: '" + HttpContext.Current.Server.HtmlEncode(cevent.description) + "'" +
                  "},";
    }

	public string getColor(string title)
		{
			string ret = "OT";
			if(title.Length < 4)
				return ret;
			string f2 = title.Substring(2,2);
			return f2;
			if(f2 == "OM")
				ret = "lightgreen";
			if(f2 == "IB")
				ret = "yellow";
			if(f2 == "OB")
				ret = "orange";
			if(f2 == "AR")
				ret = "#123456";
			if(f2 == "SR")
				ret = "#cccccc";
			return ret;
		}
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private long ConvertToTimestamp(DateTime value)
    {


        long epoch = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        return epoch;

    }

}