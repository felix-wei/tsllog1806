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

public partial class Modules_Hr_SelectPage_CandidateList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        OnLoad();
    }
    private void BindData()
    {
        if (Request.QueryString["id"] != null && Request.QueryString["typ"] != null)
        {
            int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
            string ItvRec = SafeValue.SafeString(Request.QueryString["typ"]).ToUpper();
            string sql = "";
            if (ItvRec.ToLower() == "recruitment" && id > 0)
            {
                sql = string.Format("select Id from Hr_Recruitment where Id='{0}'", id);
            }
            else if (ItvRec.ToLower() == "interview" && id > 0)
            {
                sql = string.Format("select Id  from Hr_Interview where Id='{0}'", id);
            }
            if (sql.Length > 0)
            {
                DataTable tab = ConnectSql.GetTab(sql);
                if (tab.Rows.Count == 1)
                {
                    sql = "select Id,Name,BirthDay,IcNo,Gender,Country,Race,Religion,Married,Email,Telephone,Address,Remark,Remark1,Remark2,Remark3,Remark4 from Hr_Person where Status='CANDIDATE'";
                    DataTable tab1 = ConnectSql.GetTab(sql);
                    this.ASPxGridView1.DataSource = tab1;
                    this.ASPxGridView1.DataBind();
                }
            }
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["typ"] != null)
        {
            string ItvRec = Request.QueryString["typ"].ToString();
            int mastId = SafeValue.SafeInt(Request.QueryString["id"], 0);

            
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    int detId = list[i].detId;
                    
                    string sql = "";

                    sql = string.Format(@"select Id from Hr_Person where Id='{0}'", detId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrPerson), "Id='" + detId + "'");
                        HrPerson person = C2.Manager.ORManager.GetObject(query) as HrPerson;
                        string newItvRecId = "<" + mastId + ">";

                        if (ItvRec.ToLower() == "recruitment" && !person.RecruitId.Contains(newItvRecId))
                            person.RecruitId = person.RecruitId + newItvRecId;
                        if (ItvRec.ToLower() == "interview" && !person.InterviewId.Contains(newItvRecId))
                            person.InterviewId = person.InterviewId + newItvRecId;
                        C2.Manager.ORManager.StartTracking(person, Wilson.ORMapper.InitialState.Updated);
                        C2.Manager.ORManager.PersistChanges(person);
                    }
                }
                catch { }
            }
        }
        else
        {
            e.Result = "Error, Pls refresh your Interview and Recruitment";
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public int detId = 0;
        public bool isPay = false;
        public Record(int _detId)
        {
            detId = _detId;
        }

    }
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 100;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox detId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_detId") as ASPxTextBox;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (detId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(detId.Text, 0)
                    ));
            }
        }
    }
}
