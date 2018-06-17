using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Z_rate_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsArInvoice.FilterExpression = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string dateFrom = "";
        string dateEnd = "";
        string where = "";
        if (txt_refNo.Text.Trim() != "")
            where = "DocNo='" + txt_refNo.Text.Trim() + "'";
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
            where = "DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }
        if (this.cmb_PartyTo.Value != null)
        {
            if (where.Length > 0)
                where += " and PartyTo='" + this.cmb_PartyTo.Value + "'";
            else
                where = " PartyTo='" + this.cmb_PartyTo.Value + "'";
        }
        if (where.Length > 0)
        {
            where += " AND DocType='SQU' and  (MastType is not null)";
            this.dsArInvoice.FilterExpression = where;
        }
    }
    protected void cmb_Customer_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb = sender as ASPxComboBox;

        object[] name = new object[cmb.Items.Count];
        object[] term = new object[cmb.Items.Count];
        string sql = "select p.Name,t.Code from XXParty p left outer join XXTerm t on p.TermId=t.Code";
        DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < cmb.Items.Count; i++)
        {
            name[i] = cmb.Items[i];
            for (int r = 0; r < dt.Rows.Count; r++)
                if (name[i].ToString() == dt.Rows[r]["Name"].ToString())
                    term[i] = dt.Rows[i]["Code"];
        }
        e.Properties["cpTerm"] = term;
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            C2.rate_doc inv = new C2.rate_doc();
            ASPxComboBox txtCustomerId = ASPxPopupControl1.FindControl("cmb_Customer") as ASPxComboBox;
            ASPxDateEdit txtDocDt = ASPxPopupControl1.FindControl("txt_DocDt") as ASPxDateEdit;
            ASPxComboBox txtTermId = ASPxPopupControl1.FindControl("txt_TermId") as ASPxComboBox;

            string counterType = "AR-SQU";

            inv.DocType = "SQU";
            inv.DocDate = txtDocDt.Date;
            string invN = C2Setup.GetNextNo(inv.DocType, counterType, inv.DocDate);
            inv.DocNo = invN;
            inv.PartyTo = SafeValue.SafeString(txtCustomerId.Value, "");
            string[] currentPeriod = EzshipHelper.GetAccPeriod(txtDocDt.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], txtDocDt.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], txtDocDt.Date.Month);
            inv.Term = txtTermId.Text;
            //
            int dueDay = SafeValue.SafeInt(txtTermId.Text.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);

            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId); ;
            inv.AcSource = "DB";

            inv.MastRefNo = "0";
            inv.JobRefNo = "0";
            inv.MastType = "";
            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
            }
            catch
            {
            }
            e.Result = invN;
        }
    }
}