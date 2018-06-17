using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

public partial class SelectPage_PhotoEdit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sn = "";
        string id = "";
        if (Request.QueryString["sn"] != null&&Request.QueryString["id"] != null)
        {
            sn = Request.QueryString["sn"].ToString();
            id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                this.dsCont.FilterExpression = "JobNo='" + sn + "'";
                this.dsContTrip.FilterExpression = "JobNo='" + sn + "'";
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmAttachment), "Id='" + id + "'");
                C2.CtmAttachment obj = C2.Manager.ORManager.GetObject(query) as C2.CtmAttachment;
                DataTable tab = new DataTable();
                tab.Columns.Add("RefNo");
                tab.Columns.Add("ContainerNo");
                tab.Columns.Add("TripId");
                tab.Columns.Add("FileName");
                tab.Columns.Add("FileNote");

                tab.Columns.Add("FilePath");
                tab.Columns.Add("ImgPath");
                string path = obj.FilePath;
                string filePath = obj.FilePath;
                string imgPath = obj.ImgPath;
                string uploadType =obj.FileType;
                string url = Request.Url.ToString().Replace("http://", "");
                //if (url.IndexOf("192.168") == -1)
                    //imgPath = "http://" + url.Substring(0, url.IndexOf("/")) + imgPath.ToLower().Replace(".jpg", "_thumb.jpg");
                if (uploadType.ToLower() != "image")
                {
                    imgPath = "http://" + url.Substring(0, url.IndexOf("/")) + "/Images/LOGO.jpg";
                }
                //filePath = "http://" + url.Substring(0, url.IndexOf("/")) + filePath;
                DataRow row = tab.NewRow();
                row["RefNo"] = sn;
                row["ContainerNo"] = obj.ContainerNo;
                row["TripId"] = obj.TripId;
                row["FileName"] = obj.FileName;
                row["FileNote"] = obj.FileNote;
                row["ImgPath"] = imgPath;
                row["FilePath"] = filePath;
                tab.Rows.Add(row);

                this.grid.DataSource = tab;
                this.grid.DataBind();
            }
            OnLoad(sn);
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string sn = "";
        public string contNo = "";
        public string doNo = "";
        public string fileName = "";
        public string rmk = "";
        public bool showCust = false;
        public int photoIndex = 0;
        public string spcRmk1="";
        public string spcRmk2="";
        public string spcRmk3 = "";
        public Record(string _sn, string _contNo, string _doNo, string _fileName, string _rmk)
        {
            sn = _sn;
            contNo = _contNo;
            doNo = _doNo;
            fileName = _fileName;
            rmk = _rmk;
        }

    }
    private void OnLoad(string sn)
    {
        int start = grid.PageIndex * grid.SettingsPager.PageSize;
        int end = (grid.PageIndex + 1) * grid.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxComboBox cmb_ContNo = this.grid.FindRowTemplateControl(i, "cmb_ContNo") as ASPxComboBox;
            if (cmb_ContNo != null)
            {
                ASPxComboBox cmb_DoNo = this.grid.FindRowTemplateControl(i, "cmb_Trip") as ASPxComboBox;
                ASPxTextBox txt_FileName = this.grid.FindRowTemplateControl(i, "txt_FileName") as ASPxTextBox;
                ASPxTextBox txt_Rmk = this.grid.FindRowTemplateControl(i, "txt_Rmk") as ASPxTextBox;
                list.Add(new Record(sn,
                    cmb_ContNo.Text,
                    cmb_DoNo.SelectedItem == null ? "" : string.Format("{0}",cmb_DoNo.SelectedItem.Value),
                txt_FileName.Text,
                txt_Rmk.Text
                    ));
            }
        }
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        UploadFile();
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "post")
        {

            e.Result = UploadFile();
        }
    }
    private string UploadFile()
    {
        string s = "Save Successful!";
        try
        {
            int id =SafeValue.SafeInt(Request.QueryString["id"],0);
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    string sn = list[i].sn;
                    string contNo = list[i].contNo;
                    string doNo = list[i].doNo;
                    string fileName = list[i].fileName;
                    string rmk = list[i].rmk;


                    //string path = photo.MoveFile(sn, contNo, doNo, fileName);

                    C2.CtmAttachment job = C2.Manager.ORManager.GetObject<C2.CtmAttachment>(id);
                    //job.OrderNo = sn;
                    job.ContainerNo = contNo;
                    job.TripId = SafeValue.SafeInt(doNo, 0);
                    //job.Name = fileName;
                    //job.Path = path;
                    //if (fileName.Substring(index + 1).ToLower() == "pdf")
                    //    job.UploadType = "File";
                    //else
                    //    job.UploadType = "image";
                    job.FileNote = rmk;

                    C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(job);
                    
                }
                catch (Exception ex)
                {
                }

            }
        }
        catch (Exception ex) { s = "Save Fail!" + ex.Message; }
        return s;
    }
}
