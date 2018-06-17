using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;

public partial class PagesMaster_BankList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.txt_from.Date = DateTime.Today.AddDays(-30);
            //this.txt_end.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = string.Format(@"select SequenceId,PartyId,Code,WarningAmt,(WarningAmt-isnull(TotalLC,0)) as RemainingLimit from XXParty p left join (select BankCode,sum(LcAmount) as TotalLC from  Ref_LC  where StatusCode='Buy' group by BankCode) as l on p.PartyId=l.BankCode where GroupId='BANK'");
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
       

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_Active_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select PartyId from XXParty where SequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string bankCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        sql = string.Format(@"select lc.*,(WarningAmt-LcAmount-isnull(tab_lc.TotalLc,0)) as  RemainingLimit from Ref_LC lc 
left join (select sum(LcAmount) as TotalLc,max(LcAppDate) LcAppDate,max(LcNo) as LcNo from Ref_LC where BankCode='UOM' and StatusCode='Buy' group by BankCode,LcNo) as tab_lc 
on tab_lc.LcAppDate<lc.LcAppDate
inner join XXParty p on lc.BankCode=p.PartyId where lc.StatusCode='Buy' and BankCode='{0}' order by LcAppDate asc", bankCode);
        DataTable tab = ConnectSql.GetTab(sql);
        grd.DataSource = tab;
    }
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox txt_Code = this.grid.FindEditRowCellTemplateControl(null, "txt_Code") as ASPxTextBox;
            ASPxTextBox txt_RemainingLimit = this.grid.FindEditRowCellTemplateControl(null, "txt_RemainingLimit") as ASPxTextBox;
            if (txt_Code != null && txt_RemainingLimit != null)
            {
                txt_Code.ReadOnly = true;
                txt_Code.Border.BorderWidth = 0;

                txt_RemainingLimit.ReadOnly = true;
                txt_RemainingLimit.Border.BorderWidth = 0;
            }
        }
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string code =SafeValue.SafeString(e.NewValues["Code"]);
        decimal warningAmt = SafeValue.SafeDecimal(e.NewValues["WarningAmt"],0);
        string sql = string.Format(@"update XXParty set WarningAmt={0} where Code='{1}'",warningAmt,code);
        SafeValue.SafeInt(ConnectSql.ExecuteSql(sql),0);
    }
}