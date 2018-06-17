using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_SelectSubJob : System.Web.UI.Page
{
    public int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            btn_search_Click(null,null);
        }
        count = this.grid.VisibleRowCount;
        OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select Id,JobNo,JobType,IsTrucking,IsWarehouse,IsLocal,IsAdhoc,JobStatus,PickupFrom,DeliveryTo,Vessel,Voyage,Terminalcode,ClientRefNo,EtaDate
,PermitNo,JobDate,Remark,(select top 1 Name from XXParty where PartyId=ClientId) as client,
(select top 1 code from XXParty where PartyId=HaulierId) as Haulier from CTM_Job where JobStatus='Confirmed' and JobNo<>'{0}' and isnull(BillingRefNo,'')='' ", no);
        if (txt_search_jobNo.Text.Trim() != "")
        {
            sql += string.Format(" and JobNo like '%{0}%'", txt_search_jobNo.Text.Trim());
            if (search_JobType.Text != "All")
            {
                sql += " and JobType='" + search_JobType.Text.Replace("'", "") + "'";
            }
        }
        if (search_JobType.Text != "All")
        {
            sql += " and JobType='" + search_JobType.Text.Replace("'", "") + "'";
        }
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid.DataSource = dt;
        this.grid.DataBind();
        
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;

        public Record(int _id)
        {
            id = _id;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = count;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "txt_Id") as ASPxTextBox;
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0)));

            }
            else if (id == null)
                break;
        }
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        btn_search_Click(null,null);
    }
    protected void grid_PageSizeChanged(object sender, EventArgs e)
    {
       btn_search_Click(null,null);
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string par = e.Parameters;
        if (par == "Save")
        {
            if (list.Count > 0) {
                for (int i = 0; i < list.Count; i++) {
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    int id = list[i].id;
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "Id='" + id + "'");
                    C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;

                    ctmJob.BillingRefNo = no;
                    ctmJob.ShowInvoice_Ind = SafeValue.SafeString(cmb_IsShow.Value);
                    C2.Manager.ORManager.StartTracking(ctmJob, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(ctmJob);
                }
                e.Result = "Action Success!";
            }
        }
    }
}