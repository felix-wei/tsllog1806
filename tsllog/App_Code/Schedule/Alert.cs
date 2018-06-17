using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace C2
{
    /// <summary>
    /// Month 的摘要说明
    /// </summary>
    public class Alert
    {
        public Alert()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public void insert_alert() {
            string sql = "";
            string RefNo = "";
            string JobNo = "";
            string JobType = "";
            string DocNo = "";
            string DocType = "";
            string SendType = "";
            string SendMethod = "";
            string SendFrom = "";
            sql = string.Format(@"select * from sys_alert_rule where len(MasterId)=0");
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row=dt.Rows[i];
                string alertSql = SafeValue.SafeString(row["AlertSql"]);
                string alertColumns = SafeValue.SafeString(row["AlertColumns"]);
                string alertSubject="";
                string alertBody="";
                string alertTo = SafeValue.SafeString(row["AlertTo"]);
                string alertBcc = SafeValue.SafeString(row["AlertBcc"]);
                string alertCc = SafeValue.SafeString(row["AlertCc"]);
                string alertMobile = SafeValue.SafeString(row["AlertMobile"]);
                string alertSms = SafeValue.SafeString(row["AlertSms"]);
                string code = SafeValue.SafeString(row["Code"]);
                string masterId = SafeValue.SafeString(row["MasterId"]);
                
                DataTable dt_sql = ConnectSql_mb.GetDataTable(alertSql);
                string[] columns = alertColumns.Split(',');
                DateTime expiryDate = DateTime.Today ;
                string value = "";
                for (int j = 0; j < dt_sql.Rows.Count; j++)
                {
                    alertSubject = SafeValue.SafeString(row["AlertSubject"]);
                    alertBody = SafeValue.SafeString(row["AlertBody"]);
                    DataRow row1 = dt_sql.Rows[j];

                    for (int a = 0; a < columns.Length; a++)
                    {
                        string column=columns[a];
                       
                        if (column.Length > 0)
                        {
                           #region Columns
                            if (column == "RefNo")
                                RefNo = SafeValue.SafeString(row1[column]);
                            else if (column == "JobNo")
                                JobNo = SafeValue.SafeString(row1[column]);
                            else if (column == "JobType")
                                JobType = SafeValue.SafeString(row1[column]);
                            else if (column == "DocNo")
                                DocNo = SafeValue.SafeString(row1[column]);
                            else if (column == "DocType")
                                DocType = SafeValue.SafeString(row1[column]);
                            if (column != null)
                            {
                                value = SafeValue.SafeString(row1[column]);//
                                expiryDate = Helper.Safe.SafeDate(row1[column]);

                                if (value != null)
                                {
                                    alertSubject = alertSubject.Replace("{{"+column+"}}", value);
                                    alertBody = alertBody.Replace("{{" + column + "}}", value);
                                }
                                
                            }
                           #endregion
                        }
                    }
                    string alert_body= line_alert(code,columns,row1,alertBody);
                    DateTime today = DateTime.Today;
                    #region insert into sys_alert
                    if (expiryDate != null && expiryDate > Helper.Safe.SafeDate("1900-01-01"))
                    {
                        TimeSpan span = today - expiryDate;
                        if (span.Days > 0)
                        {
                            create_alert(RefNo, JobNo, JobType, DocNo, DocType, SendType, SendMethod, SendFrom, alertTo, alertSubject, alert_body, today, alertCc, alertBcc);
                        }
                    }
                    else {
                        create_alert(RefNo, JobNo, JobType, DocNo, DocType, SendType, SendMethod, SendFrom, alertTo, alertSubject, alert_body, today, alertCc, alertBcc);
                    }
                    #endregion
                }
            }
        }
        private void create_alert(string RefNo,string JobNo,string JobType,string DocNo,string DocType,string SendType,string SendMethod,
            string SendFrom,string alertTo,string alertSubject,string alertBody,DateTime today,string alertCc,string alertBcc) {
           string sql = string.Format(@"insert into sys_alert(RefNo,JobNo,JobType,DocNo,DocType,SendType,SendMethod,SendFrom,SendTo,Subject,Message,StatusCode,CreateUser,CreateTime) 
values(@RefNo,@JobNo,@JobType,@DocNo,@DocType,@SendType,@SendMethod,@SendFrom,@SendTo,@Subject,@Message,@StatusCode,@CreateUser,@CreateTime) select @@IDENTITY ");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

            list.Add(new ConnectSql_mb.cmdParameters("@RefNo", RefNo, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@DocNo", DocNo, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@DocType", DocType, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@SendType", SendType, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@SendMethod", SendMethod, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@SendFrom", SendFrom, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@SendTo", alertTo, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@Subject", alertSubject, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@Message", alertBody, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", "Pending", SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@CreateUser", "", SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@CreateTime", today, SqlDbType.DateTime));

            ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
            int id = 0;

            if (res.status)
            {
                id = SafeValue.SafeInt(res.context, 0);

                string emailCC = System.Configuration.ConfigurationManager.AppSettings["EmailCc"].ToString();
                string emailBcc = System.Configuration.ConfigurationManager.AppSettings["EmailBcc"].ToString();
                string err = Helper.Email.SendEmail(alertTo, alertCc, alertBcc, alertSubject, alertBody, "");

                if (err.Length > 0)
                {
                    list = new List<ConnectSql_mb.cmdParameters>();
                    sql = string.Format(@"update sys_alert set StatusCode=@StatusCode where Id=@Id");
                    list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", err, SqlDbType.NVarChar));
                    list.Add(new ConnectSql_mb.cmdParameters("@Id", id, SqlDbType.Int));
                    ConnectSql_mb.ExecuteNonQuery(sql, list);
                }
                else
                {
                    list = new List<ConnectSql_mb.cmdParameters>();
                    sql = string.Format(@"update sys_alert set StatusCode=@StatusCode where Id=@Id");
                    list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", "Success", SqlDbType.NVarChar));
                    list.Add(new ConnectSql_mb.cmdParameters("@Id", id, SqlDbType.Int));
                    ConnectSql_mb.ExecuteNonQuery(sql, list);
                }
            }
        
        }
        private string line_alert(string code, string[] columns, DataRow row,string alertBody)
        {
            string alert_Line_Subject = "";
            string alert_Line_Body = "";
            #region Line
            string sql_line = string.Format(@"select * from sys_alert_rule where MasterId='{0}'", code);
            DataTable dt_line = ConnectSql_mb.GetDataTable(sql_line);
            if (dt_line.Rows.Count > 0)
            {
                for (int b = 0; b < dt_line.Rows.Count; b++)
                {
                    DataRow row2 = dt_line.Rows[b];
                    string  alert_line_Sql = SafeValue.SafeString(row2["AlertSql"]);
                    string alert_line_Columns = SafeValue.SafeString(row2["AlertColumns"]);
                    string alert_line_To = SafeValue.SafeString(row2["AlertTo"]);
                    string alert_line_Bcc = SafeValue.SafeString(row2["AlertBcc"]);
                    string alert_line_Cc = SafeValue.SafeString(row2["AlertCc"]);
                    string alert_line_Mobile = SafeValue.SafeString(row2["AlertMobile"]);
                    string alert_line_Sms = SafeValue.SafeString(row2["AlertSms"]);
                    string line_code = SafeValue.SafeString(row2["Code"]);
                    string line_masterId = SafeValue.SafeString(row2["MasterId"]);
                    string subjectPosition = SafeValue.SafeString(row2["SubjectPosition"]);
                    string bodyPosition = SafeValue.SafeString(row2["BodyPosition"]);
                    string[] line_Columns = alert_line_Columns.Split(',');
                    List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    for (int a = 0; a < columns.Length; a++)
                    {
                        string column = columns[a];
                        string value = "";
                        if (alert_line_Sql.Contains(column))
                        {
                            value = SafeValue.SafeString(row[column]);
                            list.Add(new ConnectSql_mb.cmdParameters("@" + column, value, SqlDbType.NVarChar));
                        }
                    }
                    if (alert_line_Sql.Length > 0)
                    {
                        DataTable dt_line_sql = ConnectSql_mb.GetDataTable(alert_line_Sql, list);
                        string value = "";
                        for (int c = 0; c < dt_line_sql.Rows.Count; c++)
                        {
                            DataRow row3 = dt_line_sql.Rows[c];
                            alert_Line_Subject += SafeValue.SafeString(row2["AlertSubject"]) + " ";
                            alert_Line_Body += SafeValue.SafeString(row2["AlertBody"])+" ";
                           
                            for (int a = 0; a < line_Columns.Length; a++)
                            {
                                string column = line_Columns[a];

                                if (column.Length > 0)
                                {
                                    #region Columns

                                    if (column != null)
                                    {
                                        value = SafeValue.SafeString(row3[column]);//

                                        if (value != null)
                                        {
                                            alert_Line_Body = alert_Line_Body.Replace("{{"+column+"}}", value);
                                        }

                                    }
                                    #endregion
                                }
                            }
                        }
                        #region Subject
                        if (alert_Line_Body.Length > 0)
                        {
                            alertBody.Replace("{{Subject}}", alert_Line_Body);
                        }
                        else
                        {
                            alertBody.Replace("{{Subject}}", "");
                        }
                        #endregion

                        #region Body
                        if (alert_Line_Body.Length > 0)
                        {
                           alertBody= alertBody.Replace("{{Body}}", alert_Line_Body);
                        }
                        else
                        {
                           alertBody= alertBody.Replace("{{Body}}", "");
                        }
                        #endregion
                    }
                }
            }
            #endregion
            return alertBody;
        }
    }
}