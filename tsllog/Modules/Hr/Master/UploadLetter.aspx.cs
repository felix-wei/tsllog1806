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
        if (!IsPostBack && Request.QueryString["no"] != null)
        {
            string sn = Request.QueryString["no"].ToString();
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
        string _code = Request.QueryString["no"] ?? "";
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

                txt_Rmk1.Text = "";
                this.lab.Text = "Upload File Success";
            }
            catch (Exception ex) { this.lab.Text = "Upload File fail, pls try agin, error:" + ex.Message; }
        }
    }

    public void ProcessFile(string _code, string _name, string _desc, byte[] _buffer)
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
            uploadType = _name.Substring(_name.LastIndexOf(".")+1);
        bool isOk = false;
        FileStream fs = new FileStream(resFileName, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(_buffer);
        bw.Close();
        fs.Close();
        AddFile(uploadType, _code, _name, _desc);
    }

    public void AddFile(string type, string code, string _name, string desc )
    {
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(CtmAttachment), "Id='" + code + "'");
        CtmAttachment photo = C2.Manager.ORManager.GetObject(query) as CtmAttachment;
        photo.FileName = _name;
        photo.RefNo = code;
        //photo.conterno = cmb_ContNo.Text;
        photo.JobNo = "";
        photo.FileNote = desc;
        photo.FileType = type;
        photo.FilePath = GetFilePath(this.txt_Sn.Text, this.cmb_ContNo.Text, this.cmb_DoNo.Text) + _name;
        photo.UpdateBy= HttpContext.Current.User.Identity.Name;
        photo.UpdateDateTime = DateTime.Now.ToLocalTime();
        Manager.ORManager.StartTracking(photo, Wilson.ORMapper.InitialState.Updated);
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
