using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using C2;

namespace Helper
{
    public class Cathay
    {
		public static void X9(int id, string typ, string note)
        {
			X9 x9 = new X9();
			x9.ParentId = id;
			x9.ActionType = typ;
			x9.ActionBy = HttpContext.Current.User.Identity.Name;
			x9.ActionNote = note;
			x9.ActionTime = DateTime.Now;
            C2.Manager.ORManager.StartTracking(x9, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(x9);
        }



        public static string FileLabel(int cnt)
        {
            if (cnt == 0)
                return "<img src=/_lib/icons/silk/page_attach.png>";
            else
                return string.Format("{0} Files", cnt);
        }

        public static string NoteLabel(int cnt)
        {
            if (cnt == 0)
                return "<img src=/_lib/icons/silk/add.png>";
            else
                return string.Format("{0} Notes", cnt);
        }


        public static int FileCount(int id, string typ)
        {
            string sqlTemp = "select Count(*) from X8 where ParentId={0} and RowType='{1}.FILE' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, id, typ);
            int val = Helper.Safe.SafeInt(Sql.One(sql1));
            return val;
        }
        public static int NoteCount(int id, string typ)
        {
            string sqlTemp = "select Count(*) from X3 where ParentId={0} and RowType='{1}.NOTE' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, id, typ);
            int val = Helper.Safe.SafeInt(Sql.One(sql1));
            return val;
        }	


        public static void AlertBunker(int vsl, DateTime dt, decimal op, decimal np, decimal ap, decimal cp, decimal tp, decimal te, decimal df, decimal dfp)
        {
            DataTable tab = Sql.List("select * from X0 where rowtype='TUG' and Id=" + vsl.ToString());
			if(tab.Rows.Count > 0) {
				DataRow dr = tab.Rows[0];
				string _email = Safe.SafeString(dr["PartyName"]);
				string alert_subj = string.Format("Bunker Alert : {0} ({1}) At {2:dd/MM/yyyy}", dr["EntityName"],dr["PartyCode"],dt);
				string alert_note = 
					string.Format(@"To: {0} <br><h2 style='color:red'>Bunker Consumption Alert !!!</h2><br>
						<table border=2>
						<tr><td><b>{1}</b></td><td>{2}</td></tr>
						<tr><td><b>{3}</b></td><td>{4}</td></tr>
						<tr><td><b>{5}</b></td><td>{6}</td></tr>
						<tr><td><b>{7}</b></td><td>{8}</td></tr>
						<tr><td><b>{9}</b></td><td>{10}</td></tr>
						<tr><td><b>{11}</b></td><td>{12}</td></tr>
						<tr><td><b>{13}</b></td><td>{14}</td></tr>
						<tr><td><b>{15}</b></td><td>{16}</td></tr>
						<tr><td><b>{17}</b></td><td>{18}</td></tr>
						<tr><td><b>{19}</b></td><td>{20}</td></tr>
						<tr><td><b>{21}</b></td><td>{22}</td></tr>
						</table>
						<br>
						Send By &copy; Cathay Shipping Tracking System<br><br>",
						dr["PartyName"],
						"Vessel",dr["EntityName"],
						"Sector",dr["PartyCode"],
						"Date",string.Format("{0:dd/MM/yyyy}",dt),
						"Opening",string.Format("{0}",op),
						"New Purchase",string.Format("{0}",np),
						"Adjustment",string.Format("{0}",ap),
						"Closing",string.Format("{0}",cp),
						"Actual Consumption",string.Format("{0}",tp),
						"Theoritcal",string.Format("{0:0.00}",te),
						"Diff (%)",string.Format("<h3 style='color:red'>{0} ({1:0.0%})</h3>",df,dfp),
						"Alert Time",string.Format("{0:dd/MM/yyyy HH:mm} by {1}",DateTime.Now,HttpContext.Current.User.Identity.Name)
						);
				X9(vsl,"BUNKER.ALERT", alert_note);
				string alert_email_subj = string.Format("{0} Bunker Alert",dr["EntityName"]);
				Helper.Email.SendEmail(
					_email,"","admin@cargo.ms",alert_email_subj,alert_note,"");			
			}
        }

		public static decimal GetBunker(DateTime dat, int vsl)
		{
            //string cur = HttpContext.Current.User.Identity.Name;
			string sqlTemp = "select top 1 value4 from x2 where rowtype='{0}' and parentid={1} and date1<='{2:yyyy-MM-dd}' order by date1 desc";
			string sql1 = string.Format(sqlTemp,"BUNKER",vsl,dat);
            decimal val = Helper.Safe.SafeDecimal(Sql.One(sql1));
            return val;
		}	

		public static decimal GetBunkerUse(DateTime dat, int vsl)
		{
            //string cur = HttpContext.Current.User.Identity.Name;
			string sqlTemp = "select top 1 value5 from x2 where rowtype='{0}' and parentid={1} and date1='{2:yyyy-MM-dd}'";
			string sql1 = string.Format(sqlTemp,"BUNKER",vsl,dat);
            decimal val = Helper.Safe.SafeDecimal(Sql.One(sql1));
            return val;
		}	

				public static decimal GetBunkerUseHour(DateTime dat, int vsl, decimal hrs)
		{
            //string cur = HttpContext.Current.User.Identity.Name;
			string sqlTemp = "select top 1 value5 from x2 where rowtype='{0}' and parentid={1} and date1='{2:yyyy-MM-dd}'";
			string sql1 = string.Format(sqlTemp,"BUNKER",vsl,dat);
            decimal val = Helper.Safe.SafeDecimal(Sql.One(sql1));
            return val / hrs;
		}	


    }
}
