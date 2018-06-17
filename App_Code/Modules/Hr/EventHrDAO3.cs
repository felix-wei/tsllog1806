using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
public class EventHrDAO3
{
	//change the connection string as per your database connection.
    private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=db_lcc_live;uid=sa;pwd=1234";

	//this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
       
        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);
        string _sql = @"
SELECT  Id,QuotationNo,VolumneRmk,DateTime2,Contact,DestinationAdd,Value2
FROM         JobInfo
WHERE     (WorkStatus <> 'Cancel' AND WorkStatus<>'Unsuccess') and Value2<>'NA' and DateTime2>='{0:yyyy-MM-dd} 00:00' and DateTime2<='{1:yyyy-MM-dd} 23:59'

";
        string _sql2 = @"
SELECT Id, TaskName,TaskType,PartyId, DueDate
FROM        Tasks
WHERE     DueDate>='{0:yyyy-MM-dd} 00:00' and DueDate<='{1:yyyy-MM-dd} 23:59'

";
        string __sql = string.Format(_sql, start, end);
        string __sql2 = string.Format(_sql2, start, end);
        //throw new Exception(__sql);
        //SqlCommand cmd = new SqlCommand(__sql);

        DataTable dt = Helper.Sql.List(__sql);
        DataTable dt2 = Helper.Sql.List(__sql2);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
                    CalendarEvent cevent = new CalendarEvent();
                    cevent.id = Helper.Safe.SafeInt(dr["Id"]);
                    //cevent.title = string.Format("<a href='ShowJob(\"{0}\")'>{0}</a>:{1}", dr["RefNo"], dr["VolumneRmk"]);
                    cevent.title = string.Format("{0}/{1}", dr["QuotationNo"], dr["Value2"]);
                    cevent.description = ""; // string.Format("{0}\r\n{1}\r\n{2}",dr["VolumneRmk"],dr["Contact"],dr["DestinationAdd"]);
                    cevent.start = Helper.Safe.SafeDate(dr["DateTime2"]);
                    cevent.end = Helper.Safe.SafeDate(dr["DateTime2"]);
                    events.Add(cevent);
         
        }
        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            DataRow dr = dt2.Rows[i];
                    CalendarEvent cevent = new CalendarEvent();
                    cevent.id = Helper.Safe.SafeInt(dr["Id"]);
                    //cevent.title = string.Format("<a href='ShowJob(\"{0}\")'>{0}</a>:{1}", dr["RefNo"], dr["VolumneRmk"]);
                    cevent.title = string.Format("TASK:{0}/{1}", dr["TaskName"], dr["PartyId"]);
                    cevent.description = ""; // string.Format("{0}\r\n{1}\r\n{2}",dr["VolumneRmk"],dr["Contact"],dr["DestinationAdd"]);
                    cevent.start = Helper.Safe.SafeDate(dr["DueDate"]);
                    cevent.end = Helper.Safe.SafeDate(dr["DueDate"]);
                    events.Add(cevent);
         
        }
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains a extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }

	

    
}
