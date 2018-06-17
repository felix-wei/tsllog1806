using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class PagesFreight_UploadAccountPick : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_No.Text= Request.QueryString["Job"].ToString();
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string job = this.txt_No.Text.Trim();
            DataTable tab = D.List("select Id, FileName, FilePath from ctm_attachment where refno='"+job+"'") 
            this.ASPxGridView1.DataSource = tab;
            this.ASPxGridView1.DataBind();

        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["Job"] != null && Request.QueryString["Sn"] != null )
        {
            string job = Request.QueryString["Job"].ToString();
            string inv = Request.QueryString["Sn"].ToString();

            
                for (int m = 0; m < list.Count; m++)
                {
                    
                        int id = list[m].docId;

                        string sql = string.Format("select * from Ctm_Attachment where Id='{0}'", id);
						DataTable dt = D.List(sql);
						DataRow dr = dt.Rows[0];

						C2.SeaAttachment photo = new C2.SeaAttachment();
						photo.JobClass = "IV"; //Request.QueryString["Type"].ToString();
						photo.FileName = S.Text(dr["FileName"]);
						photo.RefNo = inv;
						photo.JobNo = job;
						photo.FileNote = "";
						photo.FileType = S.Text(dr["FileType"]);
						photo.FilePath = S.Text(dr["FilePath"]);
						photo.CreateBy= HttpContext.Current.User.Identity.Name;
						photo.CreateDateTime = DateTime.Now.ToLocalTime();
						Manager.ORManager.StartTracking(photo, Wilson.ORMapper.InitialState.Inserted);
						Manager.ORManager.PersistChanges(photo);						
						
                        
                }
                 
            
        }
        else
        {
            e.Result = "Error, Pls refresh your invoice";
        }
    }
     
	 public string GetPath(string name)
	 {
		if(name.ToLower().IndexOf(".jpg") >= 0)
		{
			return "/photos/" + name;
		}
		return "/custom/logo.jpg";
	 }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public int docId = 0;
        public bool isPay = false;
        public Record(int _docId)
        {
            docId = _docId;
        }

    }
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 100;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(docId.Text, 0)
                    ));
            }
        }
    }
}
