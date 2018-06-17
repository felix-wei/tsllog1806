using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxDataView;


public partial class PagesContainer_Job_DeportOutEdit : System.Web.UI.Page
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
        where = string.Format("DocType='RO'and isnull(EventCode,'')='' ");
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
        string where = string.Format("DocType='RO' and  isnull(EventCode,'')=''");
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
        public string containerNo = "";
        public string contType = "";
        public Record(string _jobNo, string _jobStatus,string _ContNo,string _contType)
        {
            jobNo = _jobNo;
            jobStatus = _jobStatus;
            containerNo = _ContNo;
            contType = _contType;
        }

    }
    private void OnLoad(string driver)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxLabel jobId = this.grid_Transport.FindRowTemplateControl(i, "txt_jobId") as ASPxLabel;
            ASPxLabel jobStatus = this.grid_Transport.FindRowTemplateControl(i, "lbl_State") as ASPxLabel;
            ASPxTextBox containerNo = this.grid_Transport.FindRowTemplateControl(i, "txt_ContNo") as ASPxTextBox;
            ASPxLabel contType = this.grid_Transport.FindRowTemplateControl(i, "txt_ContType") as ASPxLabel;
            ASPxCheckBox isPay = this.grid_Transport.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (jobId != null && jobStatus != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(jobId.Text, jobStatus.Text,containerNo.Text,contType.Text));
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
            string update_result = "";
            int update_error_rowcount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    if (checkCont_NO_Type(list[i].containerNo, list[i].contType))
                    {
                        update_error_rowcount++;
                        update_result += list[i].containerNo + ",";
                        continue;
                    }

                    string jobNo = list[i].jobNo;
                    string jobStatus = list[i].jobStatus;
                    string sql = string.Format("update Cont_AssetEvent Set EventDepot='{0}',EventCode='depotout',EventDateTime=GETDATE(),ReleaseDate='{1}',State='{2}',ContainerNo='{4}' where Id='{3}'", depot, depotDate, jobStatus, jobNo,list[i].containerNo);

                    C2.Manager.ORManager.ExecuteCommand(sql);

                }
                catch { }
            }
            if (update_error_rowcount > 0)
            {
                update_result = update_error_rowcount + " Rows ContainerNo and Type are not Matching, include:" + update_result.Substring(0, update_result.Length - 1);
                e.Result = update_result;
            }
        }
    }
    private bool checkCont_NO_Type(string NO, string Type)
    {
        if (Type.Length == 0 || NO.Trim().Length == 0)
        {
            return true;
        }
        string sql = "select COUNT(*) from Ref_Container where ContainerNo='" + NO + "' and ContainerType='" + Type + "' ";
        int result = Convert.ToInt32(ConnectSql.GetTab(sql).Rows[0][0]);
        if (result != 1)
        {
            return true;
        }
        return false;
    }
}