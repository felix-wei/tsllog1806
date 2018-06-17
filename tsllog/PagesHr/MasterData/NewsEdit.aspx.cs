using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class PagesHr_Job_News : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_Title.Focus();
            if (Request.QueryString["Id"] != null)
            {
                txt_Id.Text = Request.QueryString["Id"];
                string sql = string.Format(@"select Title,Note,ExpireDateTime from Hr_PersonNews where Id='{0}'", SafeValue.SafeInt(txt_Id.Text, 0));
                DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                if (tab.Rows.Count == 1)
                {
                    txt_Title.Text = SafeValue.SafeString(tab.Rows[0]["Title"]);
                    memo_Note.Text = SafeValue.SafeString(tab.Rows[0]["Note"]);
                    date_ExpireDateTime.Value = SafeValue.SafeDate(tab.Rows[0]["ExpireDateTime"], DateTime.Now);
                }
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        string sql = string.Format(@"select Id from Hr_PersonNews where Id='{0}'", SafeValue.SafeInt(txt_Id.Text, 0));
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 1)
        {
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrPersonNews), "Id='" + txt_Id.Text + "'");
            HrPersonNews news = C2.Manager.ORManager.GetObject(query) as HrPersonNews;
            news.Title = txt_Title.Text.Replace("'", "''");
            news.Note = memo_Note.Text.Replace("'", "''");
            news.UpdateBy = user;
            news.UpdateDateTime = DateTime.Now;
            news.ExpireDateTime = SafeValue.SafeDate(date_ExpireDateTime.Value, DateTime.Now);
            C2.Manager.ORManager.StartTracking(news, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(news);
        }
        else
        {
            sql = string.Format(@"INSERT INTO dbo.Hr_PersonNews(Title,Note,CreateBy,CreateDateTime,ExpireDateTime) VALUES('{0}','{1}','{2}','{3}','{4}')", txt_Title.Text.Replace("'", "''"), memo_Note.Text.Replace("'", "''"), user, DateTime.Now, SafeValue.SafeDate(date_ExpireDateTime.Value, DateTime.Now));
            C2.Manager.ORManager.ExecuteCommand(sql);
            sql="select max(Id) from Hr_PersonNews";
            txt_Id.Text = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
        }
    }
}
