using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SelectPage_SelectJobNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_RefNo.Focus();
            string mastRefNo = Request.QueryString["MastRefNo"].ToString();
            this.txt_RefNo.Text = mastRefNo;
            btn_Sch_Click(null, null);
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string refNo = this.txt_RefNo.Text.Trim().ToUpper();
        string mastType = Request.QueryString["MastType"].ToString();
        string sql = "";
        if (mastType == "SI" )
        {
            sql = @"SELECT SequenceId as Id,RefNo as MastRefNo,JobNo as JobRefNo,'SI'as MastType,HblNo,(select Name from XXParty where PartyId=CustomerId) as Customer,Weight,Volume FROM SeaImport where";
            if (refNo.Length > 0)
            {
                sql += " RefNo='" + refNo + "' and StatusCode='USE' ";
            }
            else
            {
                sql += " 1=0";
            }
        }
        if (mastType == "SE")
        {
            sql = @"SELECT SequenceId as Id,RefNo as MastRefNo,JobNo as JobRefNo,'SE'as MastType,HblNo,(select Name from XXParty where PartyId=CustomerId) as Customer,Weight,Volume FROM SeaExport where";
            if (refNo.Length > 0)
            {
                sql += " RefNo='" + refNo + "'  and StatusCode='USE'  ";
            }
            else
            {
                sql += " 1=0";
            }
        }
        if (mastType == "AI")
        {
            sql = @"SELECT Id,RefNo as MastRefNo,JobNo as JobRefNo,'AI'as MastType,HAWB as HblNo,(select Name from XXParty where PartyId=CustomerId) as Customer,Weight,Volume FROM air_job where";

            if (refNo.Length > 0)
            {
                sql += " RefNo='" + refNo + "'  and StatusCode='USE' ";
            }
            else {
                sql += " 1=0";
            }
        }
        if (mastType == "AE")
        {
            sql = @"SELECT Id,RefNo as MastRefNo,JobNo as JobRefNo,'AE'as MastType,HAWB as HblNo,(select Name from XXParty where PartyId=CustomerId) as Customer,Weight,Volume FROM air_job where";

            if (refNo.Length > 0)
            {
                sql += " RefNo='" + refNo + "'  and StatusCode='USE' ";
            }
            else
            {
                sql += " 1=0";
            }
        }
        if (mastType == "ACT")
        {
            sql = @"SELECT Id as Id,RefNo as MastRefNo,JobNo as JobRefNo,'ACT'as MastType,HAWB as HblNo,(select Name from XXParty where PartyId=CustomerId) as Customer,Weight,Volume FROM air_job where";

            if (refNo.Length > 0)
            {
                sql += " RefNo='" + refNo + "'  and StatusCode='USE' ";
            }
            else
            {
                sql += " 1=0";
            }
        }
        if (sql.Length > 0)
        {

            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.ASPxGridView1.DataSource = tab;
            this.ASPxGridView1.DataBind();
        }

    }
}