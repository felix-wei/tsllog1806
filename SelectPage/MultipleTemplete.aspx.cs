using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectPage_MultipleTemplete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            btn_Sch_Click(null, null);
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sql = @"select * from Ref_Contract order by CreateDateTime desc ";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["no"] != null)
        {
            string refNo = SafeValue.SafeString(Request.QueryString["no"].ToString());
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string title = list[i].title;
                    string content = list[i].content;
                    string sql = string.Format(@"insert into Ref_Contract(Title,CenterContent,CreateBy,CreateDateTime,RefNo) 
                     values('{0}','{1}','{2}',getdate(),'{3}')",title,content,EzshipHelper.GetUserName(),refNo);
                    ConnectSql.ExecuteSql(sql);
                }
                e.Result = "Success";
            }
            catch { }

        }
        else
        {
            e.Result = "Please keyin select contract ";
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string title = "";
        public string content = "";
        public Record(string _title, string _content)
        {
            title=_title;
            content=_content;
        }

    }
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxLabel title = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Title"], "lbl_Title") as ASPxLabel;
            ASPxLabel content = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["CenterContent"], "lbl_CenterContent") as ASPxLabel;
            if (title != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(title.Text, content.Text
                    ));
            }
            else if (title == null)
                break; ;
        }
    }
}