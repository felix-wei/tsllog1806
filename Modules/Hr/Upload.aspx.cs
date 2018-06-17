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
using Aspose.Cells;

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
            string type = "Employee";
            if (Request.QueryString["Type"] != null)
            {
                type = Request.QueryString["Type"].ToString();
            }
            string jobNo = SafeValue.SafeString(Request.QueryString["jobNo"]);
            if (sn == "0")
            {
                this.txt_Sn.Text = jobNo;
            }
            else
            {
                this.txt_Sn.Text = sn;
            }
            this.cmb_DoNo.Text = jobNo;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        string _type = Request.QueryString["Type"] ?? "";
        string _code = Request.QueryString["Sn"] ?? "";
        if (this.txt_Sn.Text.Length > 0)
        {
            string _desc = "";
            string _name = "";
            string _category = "";
            try
            {
                _desc = txt_Rmk1.Text.Trim();
                UploadedFile[] files1 = this.file_upload1.UploadedFiles;
                _name = (files1[0].FileName.Replace('&','-') ?? "").ToLower().Trim();
                _category =SafeValue.SafeString(cmb_Category1.Value);
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, _category, files1[0].FileBytes);
                }
                UploadedFile[] files2 = this.file_upload2.UploadedFiles;
                _desc = txt_Rmk2.Text.Trim();
                _name = (files2[0].FileName.Replace('&', '-') ?? "").ToLower().Trim();
                _category = SafeValue.SafeString(cmb_Category2.Value);
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, _category, files2[0].FileBytes);
                }
                UploadedFile[] files3 = this.file_upload3.UploadedFiles;
                _desc = txt_Rmk3.Text.Trim();
                _name = (files3[0].FileName.Replace('&', '-').Trim() ?? "").ToLower().Trim();
                _category = SafeValue.SafeString(cmb_Category3.Value);
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, _category, files3[0].FileBytes);
                }
                UploadedFile[] files4 = this.file_upload4.UploadedFiles;
                _desc = txt_Rmk4.Text.Trim();
                _name = (files4[0].FileName.Replace('&', '-') ?? "").ToLower().Trim();
                _category = SafeValue.SafeString(cmb_Category4.Value);
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, _category, files4[0].FileBytes);
                }
                UploadedFile[] files5 = this.file_upload5.UploadedFiles;
                _desc = txt_Rmk5.Text.Trim();
                _name = (files5[0].FileName.Replace('&', '-') ?? "").ToLower().Trim();
                _category = SafeValue.SafeString(cmb_Category5.Value);
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, _category, files5[0].FileBytes);
                }
                UploadedFile[] files6 = this.file_upload6.UploadedFiles;
                _desc = txt_Rmk6.Text.Trim();
                _name = (files6[0].FileName.Replace('&', '-') ?? "").ToLower().Trim();
                _category = SafeValue.SafeString(cmb_Category6.Value);
                if (_name.Length > 0)
                {
                    ProcessFile(_code, _name, _desc, _category, files6[0].FileBytes);
                }
                txt_Rmk1.Text = "";
                txt_Rmk2.Text = "";
                txt_Rmk3.Text = "";
                txt_Rmk4.Text = "";
                txt_Rmk5.Text = "";
                txt_Rmk6.Text = "";
                this.lab.Text = "Upload File Success";
            }
            catch (Exception ex) { this.lab.Text = "Upload File fail, pls try agin, error:" + ex.Message; }
        }
    }

    public void ProcessFile(string _code, string _name, string _desc,string _category, byte[] _buffer)
    {
        string path = GetFilePath(this.txt_Sn.Text, this.cmb_ContNo.Text, this.cmb_DoNo.Text);
        string path1 = MapPath("~/Photos/" + path);
        if (!Directory.Exists(path1))
            Directory.CreateDirectory(path1);
        string resFileName = path1 + _name;
        string uploadType = "";
        if (_name.IndexOf("jpg") != -1 || _name.IndexOf("jpeg") != -1 || _name.IndexOf("png") != -1 || _name.IndexOf("bmp") != -1 || _name.IndexOf("gif") != -1)
            uploadType = "Image";
        else
            uploadType = "Report";
        bool isOk = false;
        FileStream fs = new FileStream(resFileName, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(_buffer);
        bw.Close();
        fs.Close();

        AddFile(uploadType, _code, _name, _desc, _category);
    }
    
    public void AddFile(string type, string code, string _name, string desc,string _category)
    {
        C2.HrAttachment photo = new C2.HrAttachment();

        photo.FileName = _name;
        //photo.conterno = cmb_ContNo.Text;
        photo.Person =SafeValue.SafeInt(code,0);
        photo.FileNote = desc;
        photo.FileType = type;
        photo.FilePath = GetFilePath(this.txt_Sn.Text, this.cmb_ContNo.Text, this.cmb_DoNo.Text) + _name;
        photo.Category = _category;
        photo.CreateBy= HttpContext.Current.User.Identity.Name;
        photo.CreateDateTime = DateTime.Now.ToLocalTime();
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
