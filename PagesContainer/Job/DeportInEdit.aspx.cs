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
using DevExpress.Web.ASPxDataView;

public partial class PagesContainer_Job_DepotInList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (EzshipHelper.GetUserName().ToUpper() == "ADMIN")
            {
                btn_Port.Enabled = true;
                btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
            }
            else
            {
                btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
            }
            DataBind("");
        }
        OnLoad("");
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        //string Name = EzshipHelper.GetUserName();
        //string Port = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select Port from dbo.[User] where name='{0}'", Name)));
        string Port = btn_Port.Text.Trim();
        where = string.Format("DocType='SO' and (EventCode='gateout' or isnull(EventCode,'')='')", Port);
        if (Port.Length > 0)
        {
            where += " and EventPort='" + Port + "'";
        }
        if (where.Length > 0)
        {
            this.dsTransport.FilterExpression = where;
        }
        else
        {
            this.dsTransport.FilterExpression = "1=1";
        }
    }

    protected void DataBind(string filter)
    {
        this.date_DepotDate.Value = DateTime.Now;
        //string Name = EzshipHelper.GetUserName();
        //string Port = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        //string Port = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select Port from dbo.[User] where name='{0}'", Name)));
        string Port = btn_Port.Text.Trim();
        string where = string.Format("DocType='SO' and (EventCode='gateout' or isnull(EventCode,'')='')");
        if (Port.Length > 0)
        {
            where += " and EventPort='" + Port + "'";
        }
        if (where.Length > 0)
         {
            this.dsTransport.FilterExpression = where;
        }
        else
        {
            this.dsTransport.FilterExpression = "1=1";
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string jobNo = "";
        public string jobStatus = "";
        public Record(string _jobNo, string _jobStatus)
        {
            jobNo = _jobNo;
            jobStatus = _jobStatus;
        }

    }
    private void OnLoad(string driver)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxLabel jobId = this.grid_Transport.FindRowTemplateControl(i, "txt_jobId") as ASPxLabel;
            //ASPxLabel jobStatus = this.grid_Transport.FindRowTemplateControl(i, "lbl_State") as ASPxLabel;
            ASPxComboBox jobStatus = this.grid_Transport.FindRowTemplateControl(i, "cbb_state") as ASPxComboBox;
            ASPxCheckBox isPay = this.grid_Transport.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (jobId != null && jobStatus != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(jobId.Text, jobStatus.Text));
            }
        }
    }
    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "OK")
        {
            ASPxGridView g = this.grid_Transport;
            string depot = this.cmb_Depot.Text;
            DateTime depotDate = this.date_DepotDate.Date;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    string jobNo = list[i].jobNo;
                    string jobStatus = list[i].jobStatus;
                    string sql = string.Format("update Cont_AssetEvent Set EventDepot='{0}',EventCode='depotin',EventDateTime=GETDATE(),ReceiveDate='{1}',State='{2}' where Id='{3}'", depot, depotDate, jobStatus, jobNo);

                    C2.Manager.ORManager.ExecuteCommand(sql);

                }
                catch { }
            }
        }
    }
}