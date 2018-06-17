using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class Z_pay_list : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsArReceipt.FilterExpression = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
            this.cbo_PostInd.Text = "All";
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        //this.ASPxGridView1.FindRowTemplateControl(0, "txt").;

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
        else if (this.txt_ChqNo.Text.Trim().Length > 0)
        {
            if (where.Length > 0)
                where += " and ChqNo='" + this.txt_ChqNo.Text + "'";
            else
                where = " ChqNo='" + this.txt_ChqNo.Text + "'";
        }

        if (this.txt_InvNo.Text.Trim().Length > 0)
        {
            string sql = "select  repId from pay_line where DocType='IV' and docno='" + this.txt_InvNo.Text.Trim() + "'";
            DataTable tab = ConnectSql.GetTab(sql);
            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (i == 0)
                        where = "( Id='" + SafeValue.SafeInt(tab.Rows[i][0], 0) + "'";
                    else

                        where += " or Id='" + SafeValue.SafeInt(tab.Rows[i][0], 0) + "'";

                }
                where += ")";
            }
            else
            {
                where = "";
            }
        }
        if (where.Length > 0)
        {
            if (this.cbo_PostInd.Text == "Y")
                where += " and ExportInd='Y'";
            else if (this.cbo_PostInd.Text == "N")
                where += " and ExportInd!='Y'";
            this.dsArReceipt.FilterExpression = where;// +" and DocType='RE'";
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("ARRE", true);
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            C2.pay_doc inv = new C2.pay_doc();
            ASPxComboBox txtCustomerId = ASPxPopupControl1.FindControl("cmb_Customer") as ASPxComboBox;
            ASPxDateEdit txtDocDt = ASPxPopupControl1.FindControl("txt_DocDt") as ASPxDateEdit;
            ASPxTextBox txtAcDorc = ASPxPopupControl1.FindControl("txt_AcDorc") as ASPxTextBox;
            ASPxComboBox cboDocType = ASPxPopupControl1.FindControl("cbo_DocType") as ASPxComboBox;
            ASPxComboBox cboDocType1 = ASPxPopupControl1.FindControl("cbo_DocType1") as ASPxComboBox;
            ASPxTextBox txtOtherPartyName = ASPxPopupControl1.FindControl("txt_OtherPartyName") as ASPxTextBox;

            inv.PartyTo = SafeValue.SafeString(txtCustomerId.Value, "");
            //inv.DocNo = invN;
            inv.DocType = cboDocType.Text;
            inv.DocType1 = cboDocType1.Text;
            inv.DocDate = txtDocDt.Date;
            inv.ChqDate = txtDocDt.Date;
            inv.AcCode = System.Configuration.ConfigurationManager.AppSettings["DefaultBankCode"];
            inv.DocCurrency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
            inv.DocExRate = new decimal(1.0);
            inv.ExportInd = "N";

            if (inv.DocType1.ToLower() == "refund")
                inv.AcSource = "CR";
            else
                inv.AcSource = "DB";

            inv.DocAmt = 0;
            inv.LocAmt = 0;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            inv.BankRec = "N";
            inv.BankDate = new DateTime(1900, 1, 1);
            inv.OtherPartyName = txtOtherPartyName.Text;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(txtDocDt.Date);
            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], txtDocDt.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], txtDocDt.Date.Month);
            try
            {
                inv.GenerateInd = "N";
                inv.CreateBy = HttpContext.Current.User.Identity.Name;
                inv.CreateDateTime = DateTime.Now;
                inv.UpdateBy = HttpContext.Current.User.Identity.Name;
                inv.UpdateDateTime = DateTime.Now;
                inv.PostBy = "";
                inv.PostDateTime = new DateTime(1900, 1, 1);
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                inv.DocNo = inv.Id.ToString();
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(inv);
                //C2Setup.SetNextNo("AR-RECEIPT", invN);
            }
            catch
            {
            }
            e.Result = inv.DocNo;
        }
    }
}
