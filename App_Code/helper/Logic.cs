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
    public class Logic
    {
       

        public static string GetEmployeeName(string code)
        {

            string sql = string.Format("select EntityName from x0 where rowtype='STAFF' and EntityNo='{0}' ", code);
            string ret = Helper.Safe.SafeString(Helper.Sql.One(sql));
            return ret;
        }

        public static int GetPostCount(string site, string post)
        {

            string sql = string.Format("select count(*) from x2 where rowtype='POST' and rowstatus='RECORD' and ParentId={0} and ItemName='{1}' ", site, post);
            int ret = Helper.Safe.SafeInt(Helper.Sql.One(sql));
            return ret;
        }

        public static string GetEmployeeWp(string code)
        {

            string sql = string.Format("select Note10 from x0 where rowtype='STAFF' and EntityNo='{0}' ", code);
            string ret = Helper.Safe.SafeString(Helper.Sql.One(sql));
            return ret;
        }


        public static int EmployeePostCount(string site, string type, DateTime d1, DateTime d2)
        {

            string sql = string.Format("select count(*) from x2 where rowtype='POST' and  itemname='{3}' and parentid='{0}' ", site, d1, d2, type);
            if (type == "")
                sql = string.Format("select count(*) from x2 where rowtype='POST'  and parentid='{0}' ", site, d1, d2, type);

            //throw new Exception(sql);
            int dd = Helper.Safe.SafeInt(Helper.Sql.One(sql));
            return dd;
        }

        public static int EmployeeCount(string site, string type, DateTime d1, DateTime d2)
        {
            string sql = string.Format("select count(*) from x2 where rowtype='{3}' and parentid='{0}'  and ScheduleDate>='{1:yyyy-MM-dd}' and ScheduleDate<='{2:yyyy-MM-dd}' ", site, d1, d2, type);
            //throw new Exception(sql);
            int dd = Helper.Safe.SafeInt(Helper.Sql.One(sql));
            return dd;
        }
        public static int VacancyCount(string site, string type, DateTime d1, DateTime d2)
        {
            string sql = string.Format(@"select count(*) from x2 where rowtype='DUTY' and parentid='{0}'  and ScheduleDate>='{1:yyyy-MM-dd}' and ScheduleDate<='{2:yyyy-MM-dd}' and 
(Note1='V' OR
Note2='V' OR
Note3='V' OR
Note4='V' OR
Note5='V' OR
Note6='V' OR
Note7='V' OR
Note8='V' OR
Note9='V' OR
Note10='V' OR
Note11='V' OR
Note12='V' OR
Note13='V' OR
Note14='V' OR
Note15='V' OR
Note16='V' OR
Note17='V' OR
Note18='V' OR
Note19='V' OR
Note20='V' OR
User1='V' OR
User2='V' OR
User3='V' OR
User4='V' OR
User5='V' OR
User6='V' OR
User7='V' OR
User8='V' OR
User9='V' OR
User10='V' OR
User11='V' )
            ", site, d1, d2, type);
            //throw new Exception(sql);
            int dd = Helper.Safe.SafeInt(Helper.Sql.One(sql));
            return dd;
        }


        public static void AlertIncident(int par, string usr, DateTime dat, string loc, string tim)
        {
            string alert_note = string.Format(@"<h2 style='color:red'>Incident Alert !!!</h2>{0}<br>
						<table border=2>
						<tr><td><b>{1}</b></td><td>{2}</td></tr>
						<tr><td><b>{3}</b></td><td>{4}</td></tr>
						<tr><td><b>{5}</b></td><td>{6}</td></tr>
						<tr><td><b>{7}</b></td><td>{8}</td></tr>
						</table>
						<br>
						Send By &copy; LCC Management System<br><br>",
                         " ",
                         "Name", usr,
                         "Date", string.Format("{0:dd/MM/yyyy}", dat),
                         "Time", string.Format("{0}", tim),
                         "Location", string.Format("{0}", loc)

                         );
            //X9(par, "INCIDENT.ALERT", alert_note);
            Helper.Email.SendEmail("admin@cargo.ms", "", "admin@cargo.ms", "Incident Alert", alert_note, "");
        }


        public static void AlertQshe(int par, string qshe, string cat, string usr)
        {
            string alert_note = string.Format(@"<h2 style='color:red'>Qshe Attachment File Pending Approval !!!</h2>{0}<br>
						<table border=2>
						<tr><td><b>{1}</b></td><td>{2}</td></tr>
						<tr><td><b>{3}</b></td><td>{4}</td></tr>
						<tr><td><b>{5}</b></td><td>{6}</td></tr>
						<tr><td><b>{7}</b></td><td>{8}</td></tr>
						</table>
						<br>
						Send By &copy; LCC Management System<br><br>",
                         " ",
                         "Name", usr,
                         "Date", string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now),
                         "QShe Section", string.Format("{0}", qshe),
                         "Category", string.Format("{0}", cat)

                         );
            //X9(par, "QSHE.ALERT", alert_note);
            Helper.Email.SendEmail("admin@cargo.ms", "", "admin@cargo.ms", "QSHE Alert", alert_note, "");
        }


        public static void AlertBoat(int par, string usr, DateTime dat, string loc, string tim)
        {
            string alert_note = string.Format(@"<h2 style='color:red'>Boat Part Alert !!!</h2>{0}<br>
						<table border=2>
						<tr><td><b>{1}</b></td><td>{2}</td></tr>
						<tr><td><b>{3}</b></td><td>{4}</td></tr>
						<tr><td><b>{5}</b></td><td>{6}</td></tr>
						<tr><td><b>{7}</b></td><td>{8}</td></tr>
						</table>
						<br>
						Send By &copy; LCC Management System<br><br>",
                         " ",
                         "Name", usr,
                         "Date", string.Format("{0:dd/MM/yyyy}", dat),
                         "Time", string.Format("{0}", tim),
                         "Location", string.Format("{0}", loc)

                         );
            //X9(par, "BOATPART.ALERT", alert_note);
            Helper.Email.SendEmail("admin@cargo.ms", "", "admin@cargo.ms", "Boat Part Alert", alert_note, "");
        }

        public static void AlertPpe(int par, string usr, DateTime dat, string loc, string tim)
        {
            string alert_note = string.Format(@"<h2 style='color:red'>PPE Alert !!!</h2>{0}<br>
						<table border=2>
						<tr><td><b>{1}</b></td><td>{2}</td></tr>
						<tr><td><b>{3}</b></td><td>{4}</td></tr>
						<tr><td><b>{5}</b></td><td>{6}</td></tr>
						<tr><td><b>{7}</b></td><td>{8}</td></tr>
						</table>
						<br>
						Send By &copy; LCC Management System<br><br>",
                         " ",
                         "Name", usr,
                         "Date", string.Format("{0:dd/MM/yyyy}", dat),
                         "Time", string.Format("{0}", tim),
                         "Location", string.Format("{0}", loc)

                         );
            //X9(par, "PPE.ALERT", alert_note);
            Helper.Email.SendEmail("admin@cargo.ms", "", "admin@cargo.ms", "PPE Alert", alert_note, "");
        }

        public static void AlertUpload(int par, string usr, DateTime dat, string loc, string tim)
        {
            string alert_note = string.Format(@"<h2 style='color:red'>Incident Upload Alert !!!</h2>{0}<br>
						<table border=2>
						<tr><td><b>{1}</b></td><td>{2}</td></tr>
						<tr><td><b>{3}</b></td><td>{4}</td></tr>
						<tr><td><b>{5}</b></td><td>{6}</td></tr>
						<tr><td><b>{7}</b></td><td>{8}</td></tr>
						</table>
						<br>
						Send By &copy; LCC Management System<br><br>",
                         " ",
                         "Name", usr,
                         "Date", string.Format("{0:dd/MM/yyyy}", dat),
                         "Time", string.Format("{0}", tim),
                         "Location", string.Format("{0}", loc)

                         );
            //X9(par, "UPLOAD.ALERT", alert_note);
            Helper.Email.SendEmail("admin@cargo.ms", "", "admin@cargo.ms", "Upload Alert", alert_note, "");
        }

        public static void AlertOp(int par, string usr, DateTime dat, string loc, string tim)
        {
            string alert_note = string.Format(@"<h2 style='color:red'>Operation List Alert !!!</h2>{0}<br>
						<table border=2>
						<tr><td><b>{1}</b></td><td>{2}</td></tr>
						<tr><td><b>{3}</b></td><td>{4}</td></tr>
						<tr><td><b>{5}</b></td><td>{6}</td></tr>
						<tr><td><b>{7}</b></td><td>{8}</td></tr>
						</table>
						<br>
						Send By &copy; LCC Management System<br><br>",
                         " ",
                         "Name", usr,
                         "Date", string.Format("{0:dd/MM/yyyy}", dat),
                         "Time", string.Format("{0}", tim),
                         "Location", string.Format("{0}", loc)

                         );
            //X9(par, "OPLIST.ALERT", alert_note);
            Helper.Email.SendEmail("admin@cargo.ms", "", "admin@cargo.ms", "OP LIST Alert", alert_note, "");
        }

        public static void AlertIcNo(DataTable dt)
        {
            string alert_note = "";
            string alert_body = "";
            string alert_header = string.Format(@"<h2 style='color:red'>WorkPass Expiry List Alert !!!</h2><br>
                        <table border=1>
					     	<tr><td><b>Name</b></td><td>Ic No</td><td>WorkPass Expiry</td></tr>
						");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                alert_body+= string.Format(@"<tr><td><b>{0}</b></td><td>{1}</td><td>{2}</td></tr>
						", dr["Name"],dr["IcNo"],Helper.Safe.SafeDateStr(dr["Date2"]));
            }
            string alert_footer =string.Format(@"</table><br>Send By &copy; TSL LOGISTICS System<br><br>");

            alert_note = alert_header + alert_body + alert_footer;

            string emailTo = System.Configuration.ConfigurationManager.AppSettings["EmailTo"];
            string emailCc = System.Configuration.ConfigurationManager.AppSettings["EmailCc"];
            string emailBcc = System.Configuration.ConfigurationManager.AppSettings["EmailBcc"];
            string sql = string.Format("select Email from [dbo].[User] where Role='HRManager'");
            dt = ConnectSql.GetTab(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    emailTo = Safe.SafeString(dr["Email"]);
                    Helper.Email.SendEmail(emailTo, emailCc, emailBcc, "Empolyee WorkPass Expiry Alert", alert_note, "");
                }
            }
            else {
                Helper.Email.SendEmail(emailTo, emailCc, emailBcc, "Empolyee WorkPass Expiry Alert", alert_note, "");
            }
           

        }
        public static void AlertVehicle(DataTable dt)
        {
            string alert_note = "";
            string alert_body = "";
            string alert_header = string.Format(@"<h2 style='color:red'>Vehicle Expiry List Alert !!!</h2><br>
                        <table border=1>
					     	<tr><td><b>Code</b></td><td>Type</td><td>Model</td><td>Expiry Date</td></tr>
						");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                alert_body += string.Format(@"<tr><td><b>{0}</b></td><td>{1}</td><td>{2}</td><td>{3}</td></tr>
						", dr["VehicleCode"], dr["VehicleType"],dr["ContractNo"], Helper.Safe.SafeDateStr(dr["VpcExpiryDate"]));
            }
            string alert_footer = string.Format(@"</table><br>Send By &copy; TSL LOGISTICS System<br><br>");

            alert_note = alert_header + alert_body + alert_footer;
            string emailTo = System.Configuration.ConfigurationManager.AppSettings["EmailTo"];
            string emailCc = System.Configuration.ConfigurationManager.AppSettings["EmailCc"];
            string emailBcc = System.Configuration.ConfigurationManager.AppSettings["EmailBcc"];
            string sql = string.Format("select Email from [dbo].[User] where Role='HRManager'");
            dt = ConnectSql.GetTab(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    emailTo = Safe.SafeString(dr["Email"]);
                    Helper.Email.SendEmail(emailTo, emailCc, emailBcc, "Vehicle Expiry Alert", alert_note, "");
                }
            }
            else
            {
                Helper.Email.SendEmail(emailTo, emailCc, emailBcc, "Vehicle Expiry Alert", alert_note, "");
            }

        }
        public static void AlertBunker(int vsl, DateTime dt, decimal op, decimal np, decimal ap, decimal cp, decimal tp, decimal te, decimal df, decimal dfp)
        {
            DataTable tab = Sql.List("select * from X0 where rowtype='TUG' and Id=" + vsl.ToString());
            if (tab.Rows.Count > 0)
            {
                DataRow dr = tab.Rows[0];
                string _email = Safe.SafeString(dr["PartyName"]);
                string alert_subj = string.Format("Bunker Alert : {0} ({1}) At {2:dd/MM/yyyy}", dr["EntityName"], dr["PartyCode"], dt);
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
                        "Vessel", dr["EntityName"],
                        "Sector", dr["PartyCode"],
                        "Date", string.Format("{0:dd/MM/yyyy}", dt),
                        "Opening", string.Format("{0}", op),
                        "New Purchase", string.Format("{0}", np),
                        "Adjustment", string.Format("{0}", ap),
                        "Closing", string.Format("{0}", cp),
                        "Actual Consumption", string.Format("{0}", tp),
                        "Theoritcal", string.Format("{0:0.00}", te),
                        "Diff (%)", string.Format("<h3 style='color:red'>{0} ({1:0.0%})</h3>", df, dfp),
                        "Alert Time", string.Format("{0:dd/MM/yyyy HH:mm} by {1}", DateTime.Now, HttpContext.Current.User.Identity.Name)
                        );
                //X9(vsl, "BUNKER.ALERT", alert_note);
                Helper.Email.SendEmail(
                    _email, "", "admin@cargo.ms", "Bunker Alert", alert_note, "");
            }
        }

        public static int PpeBalance(string site, string ppe, string qty)
        {
            return PpeCount(site, ppe, "I", qty) - PpeCount(site, ppe, "O", qty) - PpeCount(site, ppe, "E", qty);
        }


        public static int Balance(string table, string row, string qty, string id)
        {
            string sqlTemp = "select sum({0}) from {1} where RowType='{2}' and Id<'{3}' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, qty, table, row, id);
            // throw new Exception(sql1);
            int val = Helper.Safe.SafeInt(Sql.One(sql1));
            return val;
        }


        public static string PpeName(string code)
        {
            string sqlTemp = "select EntityName from X0 where RowType='SPPE' and EntityNo='{0}' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, code);
            string val = Helper.Safe.SafeString(Sql.One(sql1));
            return val;
        }

        public static string StoreName(string code)
        {
            string sqlTemp = "select EntityName from X0 where RowType='STORE' and EntityNo='{0}' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, code);
            string val = Helper.Safe.SafeString(Sql.One(sql1));
            return val;
        }

        public static string StaffCode(string id)
        {
            string sqlTemp = "select EntityNo from X0 where RowType='STAFF' and Id='{0}' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, id);
            string val = Helper.Safe.SafeString(Sql.One(sql1));
            return val;
        }

        public static string StaffName(string id)
        {
            string sqlTemp = "select EntityName from X0 where RowType='STAFF' and Id='{0}' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, id);
            string val = Helper.Safe.SafeString(Sql.One(sql1));
            return val;
        }


        public static int PpeCount(string site, string ppe, string typ, string qty)
        {
            string _qty = qty == "" ? "Qty1" : qty;
            string sqlTemp = "select Sum({3}) from X2 where itemcode='{0}' and itemgroup='{1}' and itemtype='{2}' and RowType='PPEX' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, ppe, site, typ, _qty);
            int val = Helper.Safe.SafeInt(Sql.One(sql1));
            return val;

        }

        public static int PpeBalanceFast(string site, string ppe, string qty)
        {
            string sqlTemp = "select Sum({2}) as Qty1, ItemType from X2 where itemcode='{0}' and itemgroup='{1}' and RowType='PPEX' and RowStatus='RECORD' group by itemtype order by itemtype";
            string sql1 = string.Format(sqlTemp, ppe, site, qty);
            DataTable dt = Sql.List(sql1);
            int val = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (dr["ItemType"].ToString() == "I")
                    val += Safe.SafeInt(dr["Qty1"]);
                else
                    val -= Safe.SafeInt(dr["Qty1"]);
            }
            return val;
        }

        public static int PpeIssue(string ppe, string site)
        {
            string sqlTemp = "select Sum(qty1) as Qty1 from X2 where itemcode='{0}' and Note4='{1}' and (ItemType='O' or ItemType='E') and RowType='PPEX' and RowStatus='RECORD' ";
            string sql1 = string.Format(sqlTemp, ppe, site);
            int val = Safe.SafeInt(Sql.One(sql1));
            return val;
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

        public static int FileCountType(int id, string typ, string cat)
        {
            string sqlTemp = "select Count(*) from X8 where ParentId={0} and RowType='{1}' and Status1='{2}' and RowStatus='RECORD'";
            string sql1 = string.Format(sqlTemp, id, typ, cat);
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


        public static decimal GetBunker(DateTime dat, int vsl)
        {
            //string cur = HttpContext.Current.User.Identity.Name;
            string sqlTemp = "select top 1 value4 from x2 where rowtype='{0}' and parentid={1} and date1='{2:yyyy-MM-dd}'";
            string sql1 = string.Format(sqlTemp, "BUNKER", vsl, dat);
            decimal val = Helper.Safe.SafeDecimal(Sql.One(sql1));
            return val;
        }

        public static decimal GetBunkerUse(DateTime dat, int vsl)
        {
            //string cur = HttpContext.Current.User.Identity.Name;
            string sqlTemp = "select top 1 value5 from x2 where rowtype='{0}' and parentid={1} and date1='{2:yyyy-MM-dd}'";
            string sql1 = string.Format(sqlTemp, "BUNKER", vsl, dat);
            decimal val = Helper.Safe.SafeDecimal(Sql.One(sql1));
            return val;
        }

        public static decimal GetBunkerUseHour(DateTime dat, int vsl, decimal hrs)
        {
            //string cur = HttpContext.Current.User.Identity.Name;
            string sqlTemp = "select top 1 value5 from x2 where rowtype='{0}' and parentid={1} and date1='{2:yyyy-MM-dd}'";
            string sql1 = string.Format(sqlTemp, "BUNKER", vsl, dat);
            decimal val = Helper.Safe.SafeDecimal(Sql.One(sql1));
            return val / hrs;
        }
        public static bool GetEditRight(string access)
        {
            //Admin:0
            //Management:1
            //General:2
            //Office(OPS Admin):3
            //HR:4
            //Accounts:5
            //Operations:6
            //Logistics:7
            //Safety:8
            //Meeting Minutes:13
            //QSHE:14
            string sql = string.Format("select {0} from [User] where Name='{1}'", "access" + access, HttpContext.Current.User.Identity.Name.ToUpper());
            //throw new Exception(sql);
            string dd = Helper.Safe.SafeString(Helper.Sql.One(sql));
            if (dd == "Edit")
                return true;
            else
                return false;
        }


    }
}
