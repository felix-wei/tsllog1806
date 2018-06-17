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

public partial class Account_GlEntryEdit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            Session["GlEntryWhere"] = null;
            this.txt_DocType.Text = "IV";
            if (Request.QueryString["no"] != null && Request.QueryString["type"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                this.txt_DocType.Text = Request.QueryString["type"].ToString();
                Session["GlEntryWhere"] = "DocNo='" + Request.QueryString["no"] + "' and DocType='" + Request.QueryString["type"] + "'";
                string billNo = SafeValue.SafeString(Request.QueryString["billNo"], "");
                if (billNo.Length > 0 && (this.txt_DocType.Text == "PL" || this.txt_DocType.Text == "SC" || this.txt_DocType.Text == "SD" || this.txt_DocType.Text == "VO"))
                {
                    Session["GlEntryWhere"] = "SupplierBillNo='" +billNo + "' and DocType='" + Request.QueryString["type"] + "'";
                }
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["GlEntryWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsGlEntry.FilterExpression = "1=0";
        }
        if (Session["GlEntryWhere"] != null)
        {
            this.dsGlEntry.FilterExpression = Session["GlEntryWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
 
    protected void grid_InvDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsGlEntryDet.FilterExpression = "GlNo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }

    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string filter = e.Parameters;

        #region unPost
        if (filter == "P")
        {
            ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
            string glId = oidCtr.Text;
            string sql = @"SELECT DocNo,DocType from XAGlEntry";
            sql += " WHERE SequenceId='" +oidCtr.Text + "'";
            DataTable dt = Helper.Sql.List(sql);
            if (dt.Rows.Count > 0)
            {
                string docNo = dt.Rows[0]["DocNo"].ToString();
                string docType = dt.Rows[0]["DocType"].ToString();
                int res = 0;
                if (docType == "IV" || docType == "DN" || docType == "CN")
                {
                    sql = "select Sequenceid from XAArInvoice where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                    int invId = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
                    if (invId > 0)
                    {
                        //get payment list from receipt
                        sql = string.Format(@"SELECT RepId FROM XAArReceiptDet WHERE (DocId = '{0}' and DocType='{1}')", invId, docType);
                        DataTable tab_ar = Helper.Sql.List(sql);
                        for (int m = 0; m < tab_ar.Rows.Count; m++)
                        {
                            string recId = tab_ar.Rows[m]["RepId"].ToString();
                            if (SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select ExportInd from XAArReceipt where SequenceId='{0}'", recId)), "N") == "Y")
                            {
                                //have post, first unpost payment
                                string recNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select DocNo from XAArReceipt where SequenceId='{0}'", recId)), "");
                                e.Result = "This Invoice have been payed by Receipt("+recNo+"),can't unpost!";
                                return;
                            }
                        }

                        //get payment list from payment
                        sql = string.Format(@"SELECT PayId FROM XAApPaymentDet WHERE (DocId = '{0}' and DocType='{1}')", invId, docType);
                        DataTable tab = Helper.Sql.List(sql);
                        for (int p = 0; p < tab.Rows.Count; p++)
                        {
                            string payId = tab.Rows[p]["PayId"].ToString();
                            if (SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select ExportInd from XAApPayment where SequenceId='{0}'", payId)), "N") == "Y")
                            {
                                //have post, first unpost payment
                                string payNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select DocNo from XAApPayment where SequenceId='{0}'", payId)), "");
                                e.Result = "This Invoice  have been pay by Payment("+payNo+"),can't unpost!!!";
                                return;
                            }
                        }
                        sql = "update XAArInvoice set ExportInd='N' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                    }
                }
                else if (docType == "RE" || docType == "PC")
                {
                    sql = "update XAArReceipt set ExportInd='N' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                }
                else if (docType == "PS" || docType == "SR")
                {
                    sql = "update XAApPayment set ExportInd='N' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                }

                else if (docType == "PL" || docType == "SC" || docType == "SD")
                {
                    sql = "select SequenceId from XAApPayable where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                    int invId = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
                    if (invId > 0)
                    {
                        //get payment list from receipt
                        sql = string.Format(@"SELECT RepId FROM XAArReceiptDet WHERE (DocId = '{0}' and DocType='{1}')", invId, docType);
                        DataTable tab_ar = Helper.Sql.List(sql);
                        for (int m = 0; m < tab_ar.Rows.Count; m++)
                        {
                            string recId = tab_ar.Rows[m]["RepId"].ToString();
                            if (SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select ExportInd from XAArReceipt where SequenceId='{0}'", recId)), "N") == "Y")
                            {
                                //have post, first unpost payment
                                string recNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select DocNo from XAArReceipt where SequenceId='{0}'", recId)), "");
                                e.Result = "This Supply Invoice have been payed by Receipt(" + recNo + "),can't unpost!";
                                return;
                            }
                        }

                        //get payment list from payment
                        sql = string.Format(@"SELECT PayId FROM XAApPaymentDet WHERE (DocId = '{0}' and DocType='{1}')", invId, docType);
                        DataTable tab = Helper.Sql.List(sql);
                        for (int p = 0; p < tab.Rows.Count; p++)
                        {
                            string payId = tab.Rows[p]["PayId"].ToString();
                            if (SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select ExportInd from XAApPayment where SequenceId='{0}'", payId)), "N") == "Y")
                            {
                                //have post, first unpost payment
                                string payNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("select DocNo from XAApPayment where SequenceId='{0}'", payId)), "");
                                e.Result = "This Supply Invoice  have been pay by Payment(" + payNo + "),can't unpost!!!";
                                return;
                            }
                        }
                        sql = "update XAApPayable set ExportInd='N' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                    }
                }
                else if (docType == "VO")
                {
                    sql = "update XAApPayable set ExportInd='N' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                }
                if (sql.Length > 0)
                {
                    res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        string sql_delete = "delete from XAGlEntryDet where GlNo= '" + oidCtr.Text + "'";
                        res = Manager.ORManager.ExecuteCommand(sql_delete);

                        sql_delete = "delete from XAGlEntry where SequenceId= '" + oidCtr.Text + "'";
                        res = Manager.ORManager.ExecuteCommand(sql_delete);
                        if (res < 1)
                        {
                            if (docType == "IV" || docType == "DN" || docType == "CN")
                            {
                                sql = "update XAArInvoice set ExportInd='Y' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                            }
                            else if (docType == "RE" || docType == "PC")
                            {
                                sql = "update XAArReceipt set ExportInd='Y' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                            }
                            else if (docType == "PS")
                            {
                                sql = "update XAApPayment set ExportInd='Y' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                            }

                            else if (docType == "VO" || docType == "PL" || docType == "SC" || docType == "SD")
                            {
                                sql = "update XAApPayable set ExportInd='Y' where DocNo='" + docNo + "' AND DocType='" + docType + "'";
                            }
                            res = Manager.ORManager.ExecuteCommand(sql);
                            e.Result = "UnPost Fail,pls try again!";
                        }
                        else
                            e.Result = "UnPost Success";
                    }
                }
            }

        }
        #endregion
    }

}
