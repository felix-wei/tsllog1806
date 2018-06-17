using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Modules_Tpt_TptJobListBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
            txt_search_dateTo.Date = DateTime.Now;
            if (SafeValue.SafeString(Request.QueryString["n"]) == "0")
                cbb_BillingStatusCode.Text = "Unbilled";
            else
                cbb_BillingStatusCode.Text = "Billed";

            //btn_search_Click(null, null);
            if (Request.QueryString["type"] != null)
            {
                lbl_type.Text = SafeValue.SafeString(Request.QueryString["type"]);
            }
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = string.Format(@"select * from(select 
job.Id,job.JobNo,job.StatusCode as JobStatus,
job.JobDate,job.YardRef as Depot,
job.ClientRefNo,job.ClientId,
job.PermitNo,job.Remark,job.SpecialInstruction,job.EtaDate,job.EtaTime,job.EtdDate,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,inv.DocNo,BillingType,
(select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo ) as Cnt,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,tab_h.Weight,tab_h.Volume,tab_h.ContNo,tab_h.SkuCode,tab_h.QtyOrig,tab_h.BookingNo,tab_h.HblNo,tab_h.Marking1,tab_h.Marking2
from CTM_Job as job
left join(select SkuCode,max(QtyOrig) QtyOrig,max(ContNo) ContNo,BookingNo,h.JobNo,HblNo,max(DoNo) DoNo,max(Marking1) Marking1,max(Marking2) Marking2,max(Weight) Weight,max(Volume) Volume from job_house h inner join  CTM_Job mast on mast.JobNo=h.JobNo group by h.JobNo,HblNo,BookingNo,SkuCode) as tab_h on tab_h.JobNo=job.JobNo
left join(select DocNo,MastRefNo,MastType,LocAmt from XAArInvoice) as inv on inv.MastRefNo=job.JobNo
left join(select isnull(sum(LocAmt),0) as Amt,MastRefNo from XAApPaymentDet group by MastRefNo) as pay on pay.MastRefNo=job.JobNo
) as tab");
        if (txt_search_jobNo.Text.Trim() != "")
        {
            where = GetWhere(where, string.Format(@"JobNo like '%{0}%'", txt_search_jobNo.Text.Replace("'", "").Trim()));
        }
        else
        {
            if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
            {
                where = GetWhere(where, " datediff(d,'" + txt_search_dateFrom.Date + "',EtaDate)>=0");
            }
            if (txt_search_dateTo.Date > new DateTime(1900, 1, 1))
            {
                where = GetWhere(where, " datediff(d,'" + txt_search_dateTo.Date + "',EtaDate)<=0");
            }
            if (btn_ClientId.Text.Length > 0)
            {
                where = GetWhere(where, " clientId='" + btn_ClientId.Text.Replace("'", "") + "'");
            }
            if (Request.QueryString["type"] != null)
            {
                where = GetWhere(where, " JobType='" + lbl_type.Text + "' ");
            }
            else {
                where = GetWhere(where, " JobType in ('GR','DO','TP') ");
            }
        }

        if (where.Length > 0)
        {
            string bst = cbb_BillingStatusCode.Text;

            if (Request.QueryString["n"] != null && bst == "Unbilled")
            {
                where = GetWhere(where, " Cnt=0 ");
            }
            else if (Request.QueryString["n"] != null && bst == "Billed")
            {
                where = GetWhere(where, " Cnt>0 ");
            }
           
            else
            {
                where = GetWhere(where, " (Cnt>=0) ");
            }
            sql += " where " + where + " and BillingType!='None' order by EtaDate,JobNo,Id desc, JobDate asc";
        }

        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();


    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    public string VilaStatus(string status)
    {
        string strStatus = "";
        if (status == "USE")
        {
            strStatus = "NEW";
        }
        if (status == "CLS")
        {
            strStatus = "COMPLATED";
        }
        if (status == "CNL")
        {
            strStatus = "CANCEL";
        }
        return strStatus;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "InTransit")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }
        return color;
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("CreateInvline"))
            {
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_JobNo = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_JobNo") as ASPxLabel;
                ASPxLabel lbl_ClientId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_ClientId") as ASPxLabel;
                TextBox txt_cntId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_cntId") as TextBox;
                ASPxLabel lbl_JobType = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_JobType") as ASPxLabel;

                e.Result = lbl_JobNo.Text + "_" + lbl_JobType.Text + "_" + lbl_ClientId.Text;
            }
        }
    }
}