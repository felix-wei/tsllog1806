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
public class EventDAO
{
	//change the connection string as per your database connection.
    private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=db_lcc_live;uid=sa;pwd=1234";

	//this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {
       
        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SELECT id, ItemName, ItemCode, scheduleDate, expiryDate FROM X3 where RowType='BOOKING' and RowStatus='RECORD' and  scheduleDate>=@start AND expiryDate<=@end", con);
        cmd.Parameters.AddWithValue("@start", start);
        cmd.Parameters.AddWithValue("@end", end);
        
        using (con)
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = (int)reader["Id"];
                cevent.title = (string)reader["ItemCode"];
                cevent.description = (string)reader["ItemName"];
                cevent.start = (DateTime)reader["ScheduleDate"];
                cevent.end = (DateTime)reader["ExpiryDate"];
                events.Add(cevent);
            }
        }
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains a extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }

	//this method updates the event title and description
    public static void updateEvent(int id, String title, String description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE X3 SET ItemCode=@title, ItemName=@description WHERE Id=@event_id", con);
        cmd.Parameters.AddWithValue("@title", title);
        cmd.Parameters.AddWithValue("@description", description);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }


    }

	//this method updates the event start and end time
    public static void updateEventTime(int id, DateTime start, DateTime end)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE X3 SET scheduledate=@event_start, expirydate=@event_end WHERE id=@event_id", con);
        cmd.Parameters.AddWithValue("@event_start", start);
        cmd.Parameters.AddWithValue("@event_end", end);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

	//this mehtod deletes event with the id passed in.
    public static void deleteEvent(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE X3 SET RowStatus='DELETE' WHERE (id = @event_id)", con);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

	//this method adds events to the database
    public static int addEvent(CalendarEvent cevent)
    {
        //add event to the database and return the primary key of the added event row

        string cnt_sql_temp = "select count(*) from x3 where RowType='BOOKING' and ((ScheduleDate<='{0:yyyy-MM-dd HH:mm}' AND ExpiryDate>='{0:yyyy-MM-dd HH:mm}') OR (ScheduleDate<='{1:yyyy-MM-dd HH:mm}' AND ExpiryDate>='{1:yyyy-MM-dd HH:mm}'))";
        string cnt_sql = string.Format(cnt_sql_temp,cevent.start, cevent.end);
        throw new Exception(cnt_sql);
        int cnt = Helper.Safe.SafeInt(Helper.Sql.One(cnt_sql));
        if (cnt > 0)
            return -1;

        //insert
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO X3(RowType,RowStatus,ItemCode, ItemName, ScheduleDate, ExpiryDate) VALUES('BOOKING','RECORD',@title, @description, @event_start, @event_end)", con);
        cmd.Parameters.AddWithValue("@title", cevent.title);
        cmd.Parameters.AddWithValue("@description", cevent.description);
        cmd.Parameters.AddWithValue("@event_start", cevent.start);
        cmd.Parameters.AddWithValue("@event_end", cevent.end);

        int key = 0;
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();

            //get primary key of inserted row
            cmd = new SqlCommand("SELECT max(id) FROM X3 where RowType='BOOKING' and RowStatus='RECORD' and ItemCode=@title AND ItemName=@description AND scheduleDate=@event_start AND expiryDate=@event_end", con);
            cmd.Parameters.AddWithValue("@title", cevent.title);
            cmd.Parameters.AddWithValue("@description", cevent.description);
            cmd.Parameters.AddWithValue("@event_start", cevent.start);
            cmd.Parameters.AddWithValue("@event_end", cevent.end);

            key = (int)cmd.ExecuteScalar();
        }

        return key;

    }


    
}
