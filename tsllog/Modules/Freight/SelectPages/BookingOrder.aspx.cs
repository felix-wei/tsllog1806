using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Freight_SelectPages_BookingOrder : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["sn"].ToString());
        dsJobCont.FilterExpression = "JobNo='" + jobNo + "'";
        string sealNo = SafeValue.SafeString(Helper.Sql.One("select SealNo from ctm_jobdet1 where JobNo='"+jobNo+"'"));
        lbl_SealNo.Text = sealNo;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_No.Focus();
            btn_Sch_Click(null, null);
            string jobNo = SafeValue.SafeString(Request.QueryString["sn"].ToString());
            dsJobCont.FilterExpression = "JobNo='"+jobNo+"'";
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string where = "";
        if(txt_No.Text.Trim()!=""){
            string bkgNo = txt_No.Text.Replace("'", "").Trim();
            where = GetWhere(where, " BookingNo like '%" + bkgNo + "%'");
        }
        if(txt_Name.Text.Trim()!=""){
            string name = txt_Name.Text.Replace("'", "").Trim();
            where = GetWhere(where, " (ShipperInfo like '" + name + "%') or (ConsigneeInfo like '" + name + "%') or (ClientId like '" + name + "%')");
        }
        if (where.Length > 0)
        {
            where = GetWhere(where, " JobNo='0'");

        }
        else {
            where = "JobNo='0'";
        }
        string sql = string.Format(@"select Id,ClientId,ClientEmail,Remark1,ConsigneeInfo,ConsigneeRemark,ConsigneeEmail,Prepaid_Ind,ContNo,Marking1,CargoStatus,BookingNo,ShipperInfo from job_house where {0}", where);
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid.DataSource = dt;
        this.grid.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string id = "";
        public Record(string _id)
        {
            id = _id;
        }

    }
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxLabel id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "lbl_Id") as ASPxLabel;
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(id.Text));

            }
            else if (id == null)
                break; ;
        }
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["sn"] != null)
        {
            string jobNo = SafeValue.SafeString(Request.QueryString["sn"].ToString());
            string result = "";
           try
           {
               if (list.Count > 0)
               {
                   for (int i = 0; i < list.Count; i++)
                   {
                       string id = list[i].id;
                       if (SafeValue.SafeString(cmb_ContNo.Value) != "")
                       {
                           string sql = "";

                           sql = string.Format(@"update job_house set JobNo='{1}',ContNo='{2}',SealNo='{3}',JobType='I' where Id='{0}'", id, jobNo, SafeValue.SafeString(cmb_ContNo.Value), SafeValue.SafeString(lbl_SealNo.Text));

                           ConnectSql.ExecuteSql(sql);
                           result = "Success";
                       }
                       else
                       {
                           result = "Please Select Container";
                       }

                   }
               }
               else {
                   result = "Please Select Booking Order";
               }
                e.Result = result;
            }
            catch { }
        }
    }
}