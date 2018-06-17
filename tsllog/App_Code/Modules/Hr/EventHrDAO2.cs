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
public class EventHrDAO2
{
	//change the connection string as per your database connection.
    private static string cnType = System.Configuration.ConfigurationManager.AppSettings["DEPLOY"];
    private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[cnType].ConnectionString.ToString();
	//this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start1, DateTime end1)
    {
       DateTime start = start1;
	if(start.Day != 1)
		start = new DateTime(start1.Year, start1.Month, 1).AddMonths(1);
	DateTime end = start.AddMonths(1).AddDays(-1);

        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);
        string _sql = @"
SELECT l.*,p.Name FROM Hr_Leave l
inner join Hr_Person p on p.Id=l.Person
WHERE  l.Date1>='{0:yyyy-MM-dd}' 
 and l.Date2<='{1:yyyy-MM-dd}'  
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
                    //cevent.title = string.Format("<a href='ShowJob(\"{0}\")'>{0}</a>:{1}", dr["RefNo"], dr["VolumneRmk"]);
                    cevent.title = string.Format("{1}:{2}:{0} / {3}", dr["Name"], dr["Date1"],dr["Date2"], S.Text(dr["Remark"]).Replace("'", ""));
                    cevent.description = ""; // string.Format("{0}\r\n{1}\r\n{2}",dr["VolumneRmk"],dr["Contact"],dr["DestinationAdd"]);
                    cevent.start = Helper.Safe.SafeDate(dr["Date1"]);
                    cevent.end = Helper.Safe.SafeDate(dr["Date2"]);
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
