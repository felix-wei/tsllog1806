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

public partial class Modules_Hr_SelectPage_QuoteList : System.Web.UI.Page
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
        if (Request.QueryString["mId"] != null && Request.QueryString["pId"] != null)
        {
            int mastId = SafeValue.SafeInt(Request.QueryString["mId"], 0);
            int personId = SafeValue.SafeInt(Request.QueryString["pId"],-1);
            string sql = "";
            if (mastId > 0)
            {
                sql = string.Format("select Id from Hr_Payroll where Id='{0}'", mastId);

                DataTable tab = ConnectSql.GetTab(sql);
                if (tab.Rows.Count == 1)
                {
                    sql = string.Format(@"SELECT Id,PayItem, Amt, Remark,'' as SignNo 
FROM Hr_Quote
where Person=0 or Person='{0}'",personId);
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
        if (Request.QueryString["mId"] != null)
        {
            int mastId = SafeValue.SafeInt(Request.QueryString["mId"], 0);

            
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    int detId = list[i].detId;
                    string des = list[i].des;
                    decimal amt = list[i].amt;

                    string sql = string.Format("SELECT PayItem FROM Hr_Quote where Id='{0}'", detId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];

                    if (tab.Rows.Count == 1)
                    {
                        HrPayrollDet rollDet = new HrPayrollDet();

                        string chgCode = SafeValue.SafeString(tab.Rows[0]["PayItem"], "");

                        rollDet.PayrollId = mastId;
                        rollDet.ChgCode = chgCode;
                        rollDet.Description = des;
                        rollDet.Amt = amt;
                        C2.Manager.ORManager.StartTracking(rollDet, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(rollDet);
                    }
                    UpdateMaster(mastId);
                }
                catch { }
            }
        }
        else
        {
            e.Result = "Error, Pls refresh your Interview and Recruitment";
        }
    }
    private void UpdateMaster(int mastId)
    {
        string sql = string.Format("Update Hr_Payroll set Amt=(select sum(Amt) from Hr_PayrollDet where PayrollId='{0}') where Id='{0}'", mastId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public int detId = 0;
        public bool isPay = false;
        public string des = "";
        public decimal amt = 0;
        public Record(int _detId, string _des ,decimal _amt)
        {
            detId = _detId;
            des = _des;
            amt = _amt;
        }

    }
    public decimal totPayAmt = 0;
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 100;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox detId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_detId") as ASPxTextBox;
            ASPxTextBox des = this.ASPxGridView1.FindRowTemplateControl(i, "txt_Des") as ASPxTextBox;
            ASPxSpinEdit amt = this.ASPxGridView1.FindRowTemplateControl(i, "spin_Amt") as ASPxSpinEdit;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (detId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(detId.Text, 0), SafeValue.SafeString(des.Value, ""), SafeValue.SafeDecimal(amt.Value, 0)
                    ));
            }
        }
    }
}
