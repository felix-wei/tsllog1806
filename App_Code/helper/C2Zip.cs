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
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;

namespace C2
{
    public class Zip
    {
        public static void FastZip(string folder, string pattern, string dest)
        {
            //FastZip fz = new FastZip();
            //string[] files = Directory.GetFiles(folder,"*.jpg",SearchOption.TopDirectoryOnly);

            //foreach (string file in files){

            //    string fileLocation = file;
            //    fz.CreateZip("C:\\helloWorld.zip", fileLocation,true,"\\.jpg$");
            //}
        }

        public static void ZipFiles(string folder, string pattern, string dest)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] files = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);

            byte[] buffer = new byte[4096];

            // the path on the server where the temp file will be created!
            string tempFileName = dest; //Server.MapPath(@"TempFiles/" + Guid.NewGuid().ToString() + ".zip"); 

            ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(tempFileName));
       //     zipOutputStream.
            string filePath = String.Empty;
            string fileName = String.Empty;
            int readBytes = 0;

            foreach (FileInfo f in files)
            {
                ZipEntry zipEntry = new ZipEntry(f.Name);
                zipOutputStream.PutNextEntry(zipEntry);

                using (FileStream fs = File.OpenRead(f.FullName))
                {
                    do
                    {
                        readBytes = fs.Read(buffer, 0, buffer.Length);
                        zipOutputStream.Write(buffer, 0, readBytes);

                    } while (readBytes > 0);
                }
            }

            zipOutputStream.Finish();
            zipOutputStream.Close();

            // delete the temp file 
            //if(File.Exists(tempFileName))
            //    File.Delete(tempFileName);
        }

        public static void ZipFilesString(string files, string dest)
        {
            string[] filesa = files.Split(new char[] { ',' });


            byte[] buffer = new byte[4096];

            // the path on the server where the temp file will be created!
            string tempFileName = dest; //Server.MapPath(@"TempFiles/" + Guid.NewGuid().ToString() + ".zip"); 

            ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(tempFileName));
            //     zipOutputStream.
            string filePath = String.Empty;
            string fileName = String.Empty;
            int readBytes = 0;
            for (int i = 0; i < filesa.Length - 1; i++)
            {
                string[] fp = filesa[i].Split(new char[] {'\\','/'} );
                ZipEntry zipEntry = new ZipEntry(fp[fp.Length -1]);
                zipOutputStream.PutNextEntry(zipEntry);
                using (FileStream fs = File.OpenRead(filesa[i]))
                {
                    do
                    {
                        readBytes = fs.Read(buffer, 0, buffer.Length);
                        zipOutputStream.Write(buffer, 0, readBytes);

                    } while (readBytes > 0);
                }
            }

            zipOutputStream.Finish();
            zipOutputStream.Close();

            // delete the temp file 
            //if(File.Exists(tempFileName))
            //    File.Delete(tempFileName);
        }

    
    }

}