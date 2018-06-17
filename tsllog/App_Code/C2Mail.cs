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

namespace C2
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
                string subx = string.Format("[{0}/{1}] {2} ", s + 1, sec, subject).Replace("\r", " ").Replace("\n", " ");
                try
                {
                    mail.Subject = subx;
                }
                catch
                {
                    throw new Exception(subx);
                }
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


            Mime mail = new Mime();
            mail.MainEntity.From = new AddressList();
            mail.MainEntity.From.Parse(smtp_user);
            mail.MainEntity.Subject = subject;
            mail.MainEntity.ContentType = MediaType_enum.Multipart_mixed;

            mail.MainEntity.To = new AddressList();
            mail.MainEntity.To.Parse(to);

            mail.MainEntity.Cc = new AddressList();
            mail.MainEntity.Cc.Parse(cc);

            mail.MainEntity.Bcc = new AddressList();
            mail.MainEntity.Bcc.Parse(bcc);

            MimeEntity textEntity = mail.MainEntity.ChildEntities.Add();
            textEntity.ContentType = MediaType_enum.Text_html;//Text_plain;
            textEntity.ContentType_CharSet = "utf-8";
            textEntity.ContentTransferEncoding = ContentTransferEncoding_enum.QuotedPrintable;
            textEntity.DataText = msg;
            try
            {
                for (int i = 0; i < filea.Length - 1; i++)
                {
                    string file = filea[i];
                    if (file.IndexOf("\\") > -1 && File.Exists(file))
                    {
                        string filename = file.Substring(file.LastIndexOf("\\") + 1);

                        MimeEntity attachmentEntity = mail.MainEntity.ChildEntities.Add();
                        attachmentEntity.ContentType = MediaType_enum.Application_octet_stream;
                        attachmentEntity.ContentDisposition = ContentDisposition_enum.Attachment;
                        attachmentEntity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
                        attachmentEntity.ContentDisposition_FileName = filename;
                        attachmentEntity.Data = (byte[])File.ReadAllBytes(file);
                    }
                }
                LumiSoft.Net.SMTP.Client.SmtpClientEx.QuickSendSmartHost(smtp_host, smtp_port, smtp_ssl, "", smtp_user, smtp_pass, mail);
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

            string[] SMTPCONFIG = System.Configuration.ConfigurationManager.AppSettings["POP3"].Split(new char[] { '|' });
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

        public static void ConvertJpg(string filea, string fileb, string logo, string n_job, string n_cont, string vessel, string refLabel, string refno, string remark, string remark1, string remark2, string remark3)
        {
            if (File.Exists(fileb))
                return;

            C2.Exif.ExifTagCollection _exif = new C2.Exif.ExifTagCollection(filea);

            string taken = "";

            foreach (C2.Exif.ExifTag tag in _exif)
            {
                if (tag.FieldName == "DateTimeOriginal")
                {
                    taken = tag.Value;
                    break;
                }
            }

            ImageFormat _formatType = ImageFormat.Jpeg;
            Image image = Image.FromFile(filea);
            Image logoi = Image.FromFile(logo);
            int width = image.Width;
            int height = image.Height;
            SolidBrush transBrush = new SolidBrush(Color.Black);
            SolidBrush transBrush2 = new SolidBrush(Color.FromArgb(245, 184, 0));
            SolidBrush bgBrush = new SolidBrush(Color.White);
            Pen bkPen = new Pen(transBrush, 2);
            Bitmap bmp = new Bitmap(width + 20, height + 200, PixelFormat.Format24bppRgb);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.FillRectangle(bgBrush, 0, 0, width + 20, height + 200);
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gfx.DrawImage(image, new Rectangle(10, 10, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
            gfx.DrawImage(logoi, new Rectangle(width + 10 - 166, height + 158, logoi.Width, logoi.Height), 0, 0, logoi.Width, logoi.Height, GraphicsUnit.Pixel);
            StringFormat strFormatter = new StringFormat();

            gfx.DrawRectangle(bkPen, 3, 3, width + 16, height + 195);

            gfx.DrawLine(bkPen, 10, height + 20, width + 10, height + 20);
            gfx.DrawLine(bkPen, 10, height + 40, width + 10, height + 40);
            gfx.DrawLine(bkPen, 10, height + 60, width + 10, height + 60);
            gfx.DrawLine(bkPen, 10, height + 80, width + 10, height + 80);
            gfx.DrawLine(bkPen, 10, height + 160, width + 10, height + 160);

            gfx.DrawLine(bkPen, 10, height + 20, 10, height + 160);

            gfx.DrawLine(bkPen, 275, height + 20, 275, height + 60);
            gfx.DrawLine(bkPen, 405, height + 20, 405, height + 60);

            gfx.DrawLine(bkPen, 130, height + 20, 130, height + 160);
            gfx.DrawLine(bkPen, width + 10, height + 20, width + 10, height + 160);

            gfx.DrawString("CFS JOB # ", new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(10, height + 21), strFormatter);

            gfx.DrawString("PHOTO TAKEN", new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(280, height + 21), strFormatter);
            gfx.DrawString(refLabel, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(280, height + 41), strFormatter);

            gfx.DrawString("CONTAINER # ", new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(10, height + 41), strFormatter);
            gfx.DrawString("VESSEL # ", new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(10, height + 61), strFormatter);
            gfx.DrawString("REMARKS ", new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(10, height + 81), strFormatter);

            gfx.DrawString(n_job, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(140, height + 21), strFormatter);

            gfx.DrawString(taken, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(410, height + 21), strFormatter);
            gfx.DrawString(refno, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(410, height + 41), strFormatter);

            gfx.DrawString(taken, new Font("Arial Black", 15, FontStyle.Regular), transBrush2, new PointF(400, height - 40), strFormatter);


            gfx.DrawString(n_cont, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(140, height + 41), strFormatter);

            gfx.DrawString(vessel, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(140, height + 61), strFormatter);

            gfx.DrawString(remark, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(140, height + 81), strFormatter);
            gfx.DrawString(remark1, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(140, height + 101), strFormatter);
            gfx.DrawString(remark2, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(140, height + 121), strFormatter);
            gfx.DrawString(remark3, new Font("Arial Black", 10, FontStyle.Regular), transBrush, new PointF(140, height + 141), strFormatter);
            bmp.Save(fileb, _formatType);
        }
        public static void AddWarkWater(string filePath, string waterPhoto, string newPath, string fileName, string waterText, string type)
        {
            if (type == "Image")
            {
                AddWaterLogo(filePath, waterPhoto, newPath);
            }
            if (type == "Text")
            {
                AddWaterText(filePath, newPath, waterText, WaterPositionMode.RightBottom, "Red", 200);
            }
        }
        /// <summary> 
        /// 给图片上水印 
        /// </summary> 
        /// <param name="filePath">原图片地址</param> 
        /// <param name="waterFile">水印图片地址</param> 
        /// <param name="newPath">新水印图片地址</param> 
        /// <param name="fileName">图片文件</param> 
        /// <param name="watertext">水印文本</param> 
        /// <param name="watertext">水印类型（Text or Image）</param> 
        public static void AddWaterLogo(string filePath, string waterPhoto, string newPath)
        {
            //GIF不水印 
            int i = filePath.LastIndexOf(".");
            string ex = filePath.Substring(i, filePath.Length - i);
            if (string.Compare(ex, ".gif", true) == 0)
            {
                return;
            }
            string ModifyImagePath = newPath;//修改的图像路径 
            if (File.Exists(ModifyImagePath))
                File.Delete(ModifyImagePath);
            System.IO.File.Copy(filePath, ModifyImagePath);

            
            int lucencyPercent = 25;
            Image modifyImage = null;
            Image drawedImage = null;
            Bitmap bitmap = null;
            Graphics g = null;
            try
            {
                #region Image
                //建立图形对象 
                modifyImage = Image.FromFile(ModifyImagePath, true);
                drawedImage = Image.FromFile(waterPhoto, true);
                g = Graphics.FromImage(modifyImage);
                //获取要绘制图形坐标 
                int x = modifyImage.Width - drawedImage.Width;
                int y = modifyImage.Height - drawedImage.Height;
                //设置颜色矩阵 
                float[][] matrixItems ={ 
            new float[] {1, 0, 0, 0, 0}, 
            new float[] {0, 1, 0, 0, 0}, 
            new float[] {0, 0, 1, 0, 0}, 
            new float[] {0, 0, 0, (float)lucencyPercent/100f, 0}, 
            new float[] {0, 0, 0, 0, 1}};

                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //绘制阴影图像 
                g.DrawImage(drawedImage, new Rectangle(x, y, drawedImage.Width, drawedImage.Height), 10, 10, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                //保存文件 
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(ModifyImagePath);


                ImageFormat imageType = ImageFormat.Gif;
                switch (fi.Extension.ToLower())
                {
                    case ".jpg": imageType = ImageFormat.Jpeg; break;
                    case ".gif": imageType = ImageFormat.Gif; break;
                    case ".png": imageType = ImageFormat.Png; break;
                    case ".bmp": imageType = ImageFormat.Bmp; break;
                    case ".tif": imageType = ImageFormat.Tiff; break;
                    case ".wmf": imageType = ImageFormat.Wmf; break;
                    case ".ico": imageType = ImageFormat.Icon; break;
                    default: break;
                }
                MemoryStream ms = new MemoryStream();
                modifyImage.Save(ms, imageType);
                byte[] imgData = ms.ToArray();
                modifyImage.Dispose();
                drawedImage.Dispose();
                g.Dispose();
                FileStream fs = null;
                File.Delete(ModifyImagePath);
                fs = new FileStream(ModifyImagePath, FileMode.Create, FileAccess.Write);
                if (fs != null)
                {
                    fs.Write(imgData, 0, imgData.Length);
                    fs.Close();
                }
                #endregion

            }
            finally
            {
                try
                {
                    if (drawedImage != null)
                    {
                        drawedImage.Dispose();
                    }
                    if (bitmap != null)
                    {
                        bitmap.Dispose();
                    }
                    if (modifyImage != null)
                    {
                        modifyImage.Dispose();
                    }
                    if (g != null)
                    {
                        g.Dispose();
                    }
                }
                catch
                {
                }
            }
        }
        public enum WaterPositionMode
        {
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom,
            Center
        }
        /// <summary>
        /// 给图片加水印文字
        /// </summary>
        /// <param name="oldpath">原图文件</param>
        /// <param name="savepath">新图文件</param>
        /// <param name="watertext">水印文字</param>
        /// <param name="position">水印位置</param>
        /// <param name="color">颜色</param>
        /// <param name="alpha">数值</param>
        public static void AddWaterText(string oldPath, string newPath, string waterText, WaterPositionMode position, string color, int alpha)
        {
            Image image = Image.FromFile(oldPath);

            if (File.Exists(newPath))
                File.Delete(newPath);
             
            System.IO.File.Copy(oldPath, newPath);
            int lucencyPercent = 25;
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            Font font = new Font("arial", 12);
            SizeF ziSizeF = new SizeF();
            ziSizeF = graphics.MeasureString(waterText, font);
            float x = 0f;
            float y = 0f;
            switch (position)
            {
                case WaterPositionMode.LeftTop:
                    x = ziSizeF.Width / 2f;
                    y = 8f;
                    break;
                case WaterPositionMode.LeftBottom:
                    x = ziSizeF.Width / 2f;
                    y = image.Height - ziSizeF.Height;
                    break;
                case WaterPositionMode.RightTop:
                    x = image.Width * 1f - ziSizeF.Width / 2f;
                    y = 8f;
                    break;
                case WaterPositionMode.RightBottom:
                    x = image.Width - ziSizeF.Width;
                    y = image.Height - ziSizeF.Height;
                    break;
                case WaterPositionMode.Center:
                    x = image.Width / 2;
                    y = image.Height / 2 - ziSizeF.Height / 2;
                    break;
            }
            try
            {
                StringFormat stringFormat = new StringFormat { Alignment = StringAlignment.Center };
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
                graphics.DrawString(waterText, font, solidBrush, x + 1f, y + 1f, stringFormat);
                SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, ColorTranslator.FromHtml(color)));
                graphics.DrawString(waterText, font, brush, x, y, stringFormat);
                solidBrush.Dispose();
                brush.Dispose();
                bitmap.Save(newPath, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {


            }
            finally
            {
                bitmap.Dispose();
                image.Dispose();
            }

        }
    }


}