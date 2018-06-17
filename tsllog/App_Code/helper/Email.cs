using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using Wilson.ORMapper;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Text;
using LumiSoft.Net.Mime;


namespace Helper
{
    public class Email
    {
        public static int batchSize = 10;

        public static string SendEmailSplit(string to, string cc, string bcc, string subject, string msg, string files)
        {
            string err = "";
            string[] filea = files.Split(new char[] { ',' });

            string[] SMTPCONFIG = System.Configuration.ConfigurationManager.AppSettings["SMTP"].Split(new char[] { '|' });
            string smtp_host = SMTPCONFIG[0];
            int smtp_port = int.Parse(SMTPCONFIG[1]);
            bool smtp_ssl = SMTPCONFIG[2] == "SSL";
            string smtp_user = SMTPCONFIG[3];
            string smtp_pass = SMTPCONFIG[4];
            string smtp_name = SMTPCONFIG[5];


            int total = filea.Length - 1;
            int idx = 0;
            int idxTotal = batchSize;

            int sec = (int)(total / batchSize);
            int spare = total % batchSize;
            if (spare > 0)
                sec = sec + 1;


            for (int s = 0; s < sec; s++)
            {

                MailMessage mail = new MailMessage();

                mail.From = new System.Net.Mail.MailAddress(smtp_user, smtp_name);
                SmtpClient smtp = new SmtpClient();
                smtp.Port = smtp_port; // 925;   // [1] You can try with 465 also, I always used 587 and got success
                smtp.EnableSsl = smtp_ssl;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
                smtp.UseDefaultCredentials = false; // [3] Changed this
                smtp.Credentials = new NetworkCredential(smtp_user, smtp_pass);  // [4] Added this. Note, first parameter is NOT string.
                smtp.Host = smtp_host;

                //recipient address
                string[] toa = to.Split(new char[] { ',', ';', '/' });
                for (int i = 0; i < toa.Length; i++)
                {
                    string eaddr = toa[i].Trim();
                    if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                        mail.To.Add(new MailAddress(eaddr));
                }
                string[] cca = cc.Split(new char[] { ',', ';', '/' });
                for (int i = 0; i < cca.Length; i++)
                {
                    string eaddr = cca[i].Trim();
                    if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                        mail.CC.Add(new MailAddress(eaddr));
                }
                string[] bcca = bcc.Split(new char[] { ',', ';', '/' });
                for (int i = 0; i < bcca.Length; i++)
                {
                    string eaddr = bcca[i].Trim();
                    if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                        mail.Bcc.Add(new MailAddress(eaddr));
                }
                mail.Subject = string.Format("[{0}/{1}] ", s + 1, sec) + subject;
                mail.Body = (s == 0) ? msg : "Attachments Only.";
                //Formatted mail body
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                mail.IsBodyHtml = false;
                try
                {
                    for (int i = idx; i < idx + idxTotal; i++)
                    {
                        string file = filea[i];

                        //string file = fileaa.ToLower().Replace(".jpg", "a.jpg");
                        Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                        string[] part = file.Split(new char[] { '/', '\\' });
                        data.Name = part[part.Length - 1];
                        // Add time stamp information for the file.
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(file);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                        // Add the file attachment to this e-mail message.
                        mail.Attachments.Add(data);
                    }
                    idx += batchSize;
                    if (total - idx < batchSize)
                        idxTotal = total - idx;

                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    err += ex.Message + "\r\n";
                }
            }
            return err;
        }

        public static string SendEmail(string to, string cc, string bcc, string subject, string msg, string files)
        {
            string err = "";
            string[] filea = files.Split(new char[] { ',' });

            //if (filea.Length > batchSize)
            //{
            //    return SendEmailSplit(to, cc, bcc, subject, msg, files);
            //}
            string[] SMTPCONFIG = System.Configuration.ConfigurationManager.AppSettings["SMTP"].Split(new char[] { '|' });
            string smtp_host = SMTPCONFIG[0];
            int smtp_port = int.Parse(SMTPCONFIG[1]);
            bool smtp_ssl = SMTPCONFIG[2] == "SSL";
            string smtp_user = SMTPCONFIG[3];
            string smtp_pass = SMTPCONFIG[4];
            string smtp_name = SMTPCONFIG[5];


            Mime mail = new Mime();
            mail.MainEntity.From = new AddressList();
            mail.MainEntity.From.Parse(smtp_name + "<" + smtp_user + ">");
            mail.MainEntity.Subject = subject;
            mail.MainEntity.ContentType = MediaType_enum.Multipart_mixed;

            mail.MainEntity.To = new AddressList();
            string[] toa = to.Split(new char[] { ',', ';', '/' });
            for (int i = 0; i < toa.Length; i++)
            {
                string eaddr = toa[i].Trim();
                if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                    mail.MainEntity.To.Parse(eaddr);
            }
            mail.MainEntity.Cc = new AddressList();
            string[] cca = cc.Split(new char[] { ',', ';', '/' });
            for (int i = 0; i < cca.Length; i++)
            {
                string eaddr = cca[i].Trim();
                if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                    mail.MainEntity.Cc.Parse(eaddr);
            }
            mail.MainEntity.Bcc = new AddressList();
            string[] bcca = bcc.Split(new char[] { ',', ';', '/' });
            for (int i = 0; i < bcca.Length; i++)
            {
                string eaddr = bcca[i].Trim();
                if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                    mail.MainEntity.Bcc.Parse(eaddr);
            }
            MimeEntity textEntity = mail.MainEntity.ChildEntities.Add();
            textEntity.ContentType = MediaType_enum.Text_html;//Text_plain;
            textEntity.ContentType_CharSet = "utf-8";
            textEntity.ContentTransferEncoding = ContentTransferEncoding_enum.QuotedPrintable;
            textEntity.DataText = msg;
            try
            {
                for (int i = 0; i < filea.Length; i++)
                {
                    string file = filea[i];
                    string parthFile = HttpContext.Current.Server.MapPath(file);
                    if (File.Exists(parthFile))
                    {
                        string filename = file.Substring(file.LastIndexOf("\\") + 1);
                        MimeEntity attachmentEntity = mail.MainEntity.ChildEntities.Add();
                        attachmentEntity.ContentType = MediaType_enum.Application_octet_stream;
                        attachmentEntity.ContentDisposition = ContentDisposition_enum.Attachment;
                        attachmentEntity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
                        attachmentEntity.ContentDisposition_FileName = filename;
                        attachmentEntity.Data = (byte[])File.ReadAllBytes(parthFile);
                    }
                }
                LumiSoft.Net.SMTP.Client.SmtpClientEx.QuickSendSmartHost(smtp_host,smtp_port,smtp_ssl,"",smtp_user,smtp_pass, mail);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }
        public static string SendEmailSmtp(string to, string cc, string bcc, string subject, string msg, string files)
        {
            string err = "";
            string[] filea = files.Split(new char[] { ',' });

            if (filea.Length > batchSize)
            {
                return SendEmailSplit(to, cc, bcc, subject, msg, files);
            }

            string[] SMTPCONFIG = System.Configuration.ConfigurationManager.AppSettings["SMTP"].Split(new char[] { '|' });
            string smtp_host = SMTPCONFIG[0];
            int smtp_port = int.Parse(SMTPCONFIG[1]);
            bool smtp_ssl = SMTPCONFIG[2] == "SSL";
            string smtp_user = SMTPCONFIG[3];
            string smtp_pass = SMTPCONFIG[4];
            string smtp_name = SMTPCONFIG[5];


            MailMessage mail = new MailMessage();

            mail.From = new System.Net.Mail.MailAddress(smtp_user, smtp_name);
            SmtpClient smtp = new SmtpClient();
            smtp.Port = smtp_port; // 925;   // [1] You can try with 465 also, I always used 587 and got success
            smtp.EnableSsl = smtp_ssl;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
            smtp.UseDefaultCredentials = false; // [3] Changed this
            smtp.Credentials = new NetworkCredential(smtp_user, smtp_pass);  // [4] Added this. Note, first parameter is NOT string.
            smtp.Host = smtp_host;

            //recipient address
            string[] toa = to.Split(new char[] { ',', ';', '/' });
            for (int i = 0; i < toa.Length; i++)
            {
                string eaddr = toa[i].Trim();
                if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                    mail.To.Add(new MailAddress(eaddr));
            }
            string[] cca = cc.Split(new char[] { ',', ';', '/' });
            for (int i = 0; i < cca.Length; i++)
            {
                string eaddr = cca[i].Trim();
                if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                    mail.CC.Add(new MailAddress(eaddr));
            }
            string[] bcca = bcc.Split(new char[] { ',', ';', '/' });
            for (int i = 0; i < bcca.Length; i++)
            {
                string eaddr = bcca[i].Trim();
                if (eaddr.Length > 8 && eaddr.IndexOf("@") > 2)
                    mail.Bcc.Add(new MailAddress(eaddr));
            }
            mail.Subject = subject;
            mail.Body = msg;
            //Formatted mail body
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            mail.IsBodyHtml = false;
            try
            {
                for (int i = 0; i < filea.Length - 1; i++)
                {
                    string file = filea[i];

                    //string file = fileaa.ToLower().Replace(".jpg", "a.jpg");
                    Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                    string[] part = file.Split(new char[] { '/', '\\' });
                    data.Name = part[part.Length - 1];
                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(file);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                    // Add the file attachment to this e-mail message.
                    mail.Attachments.Add(data);
                }

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }

   
     

        public static void AlertProcess(string email, string orderNo, string status, string body,string name)
        {
				string _email = email;
				string alert_subj = string.Format("Order Alert : {0}/({1})", orderNo, status);
				string alert_note = 
					string.Format(@"<h2 style='color:red'>Order Alert !!!</h2>
						<br>
						<table border=2>
						<tr><td><b>Order No</b></td><td>{0}</td></tr>
						<tr><td><b>Status</b></td><td>{1}</td></tr>
						<tr><td><b>Alert</b></td><td>{3}</td></tr>
	 
						</table>
						<br>
						{2}
						<br><br>
						Send By &copy; CargoERP.com<br><br>", orderNo, status, body, string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now)
						);
				//X9(vsl,"PASSPORT.ALERT", alert_note);
                string fromEmail = GetEmailByName(name);
                Helper.Email.SendEmail(
                       _email, "", fromEmail, alert_subj, alert_note, "");
               // Helper.Email.SendEmail(
                 //           _email, "", "ymyg1985318@163.com", alert_subj, alert_note, "");
			
        }
        private static string GetEmailByName(string name)
        {
            string sql = string.Format(@"select Email from [dbo].[User] where Name='{0}'", name);
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
        }

    }


}