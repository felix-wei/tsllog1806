using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using C2;

public partial class Page_Upload : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
        if (!IsPostBack && Request.QueryString["Sn"] != null)
        {
            string sn = Request.QueryString["Sn"].ToString();
            this.txt_Sn.Text = sn;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        string _code = Request.QueryString["Sn"] ?? "";
        if (this.txt_Sn.Text.Length > 0)
        {
            string _desc = "";
            string _name = "";
            try
            {
                _desc = txt_Rmk1.Text.Trim();
                UploadedFile[] files1 = this.file_upload1.UploadedFiles;
                _name = (files1[0].FileName ?? "").ToLower().Trim();
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, files1[0].FileBytes);
                }
                UploadedFile[] files2 = this.file_upload2.UploadedFiles;
                _desc = txt_Rmk2.Text.Trim();
                _name = (files2[0].FileName ?? "").ToLower().Trim();
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, files2[0].FileBytes);
                }
                UploadedFile[] files3 = this.file_upload3.UploadedFiles;
                _desc = txt_Rmk3.Text.Trim();
                _name = (files3[0].FileName ?? "").ToLower().Trim();
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, files3[0].FileBytes);
                }
                UploadedFile[] files4 = this.file_upload4.UploadedFiles;
                _desc = txt_Rmk4.Text.Trim();
                _name = (files4[0].FileName ?? "").ToLower().Trim();
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, files4[0].FileBytes);
                }
                UploadedFile[] files5 = this.file_upload5.UploadedFiles;
                _desc = txt_Rmk5.Text.Trim();
                _name = (files5[0].FileName ?? "").ToLower().Trim();
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, files5[0].FileBytes);
                }
                UploadedFile[] files6 = this.file_upload6.UploadedFiles;
                _desc = txt_Rmk6.Text.Trim();
                _name = (files6[0].FileName ?? "").ToLower().Trim();
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, files6[0].FileBytes);
                }
                txt_Rmk1.Text = "";
                txt_Rmk2.Text = "";
                txt_Rmk3.Text = "";
                txt_Rmk4.Text = "";
                txt_Rmk5.Text = "";
                txt_Rmk6.Text = "";
               
            }
            catch (Exception ex) { this.lab.Text = "Upload File fail, pls try agin, error:" + ex.Message; }
        }
    }

    public void ProcessFile(string _code, string _name, string _desc, byte[] _buffer)
    {
        string path = GetFilePath(this.txt_Sn.Text,"","");
        string path1 = MapPath("~/Photos/Wh/" + path);
        string type=Request.QueryString["Type"].ToString();;
        if (!Directory.Exists(path1))
            Directory.CreateDirectory(path1);
        string resFileName = path1 + _name;
        string uploadType = "";
        bool isOk = false;
        if (_name.IndexOf("jpg") != -1 || _name.IndexOf("jpeg") != -1 || _name.IndexOf("png") != -1 || _name.IndexOf("bmp") != -1 || _name.IndexOf("gif") != -1)
        {
            
            uploadType = "Image";
        }
        else
        {
            
            uploadType = "File";
        }
        if (type == "IMG" && uploadType == "Image")
        {
            isOk = true;
        }
        else if (type == "Att" && uploadType != "Image")
        {
            isOk = true;
        }
        if (isOk)
        {
            FileStream fs = new FileStream(resFileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(_buffer);
            bw.Close();
            fs.Close();
            AddFile(uploadType, "wh/" + path + _name, _code, _name, _desc);
            this.lab.Text = "Upload File Success";
        }
        else {
            this.lab.Text = "Upload File fail, Only Upload " + uploadType;
        }
    }

    public void AddFile(string type, string path,string code, string _name, string desc)
    {
        C2.JobAttachment photo = new C2.JobAttachment();
        photo.JobType = Request.QueryString["Type"].ToString();
        photo.FileName = _name;
        photo.RefNo = txt_Sn.Text;
        //photo.conterno = cmb_ContNo.Text;
        photo.FileNote = desc;
        photo.FileType = type;
        photo.FilePath = path;
        Manager.ORManager.StartTracking(photo, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(photo);
    }
    public bool WritePhoto(byte[] bytes, string pathTo)
    {

        try
        {

            Stream stream = new MemoryStream(bytes);

            // thumbnails
            Image img = Image.FromStream(stream);
            //Get the list of available encoders
            System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            //find the encoder with the image/jpeg mime-type
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == "image/jpeg")
                {
                    ici = codec;
                    break;
                }
            }
            //Create a collection of encoder parameters (we only need one in the collection)
            EncoderParameters ep = new EncoderParameters();

            //We'll save images with 25%, 50%, 75% and 100% quality as compared with the original

            //Create an encoder parameter for quality with an appropriate level setting
            ep.Param[0] = new EncoderParameter(Encoder.Quality, 75l);
            //Save the image with a filename that indicates the compression quality used
            img.Save(pathTo, ici, ep);
            //img.Save(pathTo,new System.Drawing.Imaging.ImageCodecInfo());

            //Image img1 = img.GetThumbnailImage(40, 40, null, IntPtr.Zero);
            //img1.Save(string.Format("{0}_thumb.jpg", pathTo.Substring(0, pathTo.IndexOf('.'))));
            //fs.Dispose();
            img.Dispose();
            //img1.Dispose();
        }
        catch
        {
            return false;
        }
        return true;
    }
    public bool WtriteFile(byte[] bytes, string pathTo)
    {
        try
        {
            FileStream fs = new FileStream(pathTo, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        catch
        {
            return false;
        }
        return true;
    }

    private string GetFilePath(string sn, string cont, string doNo)
    {
        string path = sn + "/";
        if (cont.Trim().Length > 0)
            path += cont.Trim() + "/";

        if (doNo.Trim().Length > 0)
        {
            if (doNo.Trim().IndexOf("/") != -1)
                doNo = doNo.Trim().Substring(0, doNo.IndexOf("/"));
            path += doNo.Trim() + "/";
        }
        return path;
    }

}
