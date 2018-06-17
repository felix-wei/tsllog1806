using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;

public partial class ReportFreightSea_Report_Import_UnMathcedRef : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = "select top(100) RefType=(case when RefType='SI' then 'SeaImport' when RefType='SE' then 'SeaExport' when RefType='SCT' then 'SeaCrossTrade' when RefType='AI' then 'AirImport' when RefType='AE' then 'AirExport' when RefType='ACT' then 'AirCrossTrade' when RefType='TPT' then 'LocalHanding' when RefType='IV' then 'Invoice' when RefType='CN' then 'Credit Note' when RefType='DN' then 'Debit Note' when RefType='PL' then 'Payable' when RefType='SC' then 'Supplier CreditNote' when RefType='SD' then 'Supplier DebitNote' when RefType='VO' then 'Voucher' when RefType='CI' then 'Cash Invoice' when RefType='RE' then 'Receipt' when RefType='PC' then 'AR-Refund' when RefType='SR' then 'AP-Refund' when RefType='PS' then 'Payment' end),RefNo,JobNo,Action,CreateBy,CreateDateTime from LogEvent ORDER BY  RefType Desc,RefNo,Action";
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.grid_LogEvent.DataSource = tab;
            this.grid_LogEvent.DataBind();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = "select RefType=(case when RefType='SI' then 'SeaImport' when RefType='SE' then 'SeaExport' when RefType='SCT' then 'SeaCrossTrade' when RefType='AI' then 'AirImport' when RefType='AE' then 'AirExport' when RefType='ACT' then 'AirCrossTrade' when RefType='TPT' then 'LocalHanding' when RefType='IV' then 'Invoice' when RefType='CN' then 'Credit Note' when RefType='DN' then 'Debit Note' when RefType='PL' then 'Payable' when RefType='SC' then 'Supplier CreditNote' when RefType='SD' then 'Supplier DebitNote' when RefType='VO' then 'Voucher' when RefType='CI' then 'Cash Invoice' when RefType='RE' then 'Receipt' when RefType='PC' then 'AR-Refund' when RefType='SR' then 'AP-Refund' when RefType='PS' then 'Payment' end),RefNo,JobNo,Action,CreateBy,CreateDateTime from LogEvent";
        string where = "";
        string refType = SafeValue.SafeString(this.cmb_DocType.Value).Trim();
        

        if (txt_RefNo.Text.Trim() != "")
        {
            where = GetWhere(where, "RefNo='" + txt_RefNo.Text.Trim() + "'");
        }
        else if (txt_JobNo.Text.Trim() != "")
        {
            where = GetWhere(where, "JobNo='" + txt_JobNo.Text.Trim() + "'");
        }
        else if (refType.Length > 0)
        {
            where = GetWhere(where, "RefType='" + refType + "'");
        }
        if (where.Length > 0)
        {
            where = " where " + where;
            sql += where + " ORDER BY  RefType Desc,RefNo,Action";
        }
        else
        {
            sql += " ORDER BY  RefType Desc,RefNo,Action";
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.grid_LogEvent.DataSource = tab;
        this.grid_LogEvent.DataBind();

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}