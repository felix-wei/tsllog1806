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

public partial class Pages_ImportRefList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_RefNo.Text = "";//"51856";// "55788";
            this.txt_from.Date = DateTime.Today.AddDays(-15);
            this.txt_end.Date = DateTime.Today.AddDays(8);

            string refType = SafeValue.SafeString(Request.QueryString["refType"]).ToUpper();
            if (refType.Length > 0)
                this.lab_RefType.Text = refType;
            btn_search_Click(null, null);
            this.grid_ref.ExpandAll();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string refType = this.lab_RefType.Text;
        string sql = @"select job.SequenceId,ref.RefNo,ref.OblNo as OBL,ref.Vessel,ref.Pol,convert(nvarchar(10),ref.Eta,103) Eta,Convert(nvarchar(10),ref.Etd,103) Etd,job.JobNo,job.HblNo as HBL,(select Name from dbo.XXParty where PartyId=job.CustomerId) as CustomerName,job.Qty,job.Weight,job.volume,ref.StatusCode StatusCode,job.StatusCode as JobStatusCode from SeaImportRef ref left join SeaImport job on ref.RefNo=job.RefNo";
        
        if (txt_RefNo.Text.Trim() != "")
            where =" ref.RefNo='" + txt_RefNo.Text.Trim() + "'";
        else if (this.txt_Obl.Text.Trim().Length > 0)
            where = " ref.OblNo='" + this.txt_Obl.Text.Trim() + "'";
        else if(this.txt_HblN.Text.Trim()!="")
        {
            where ="job.HblNo='" + this.txt_HblN.Text.Trim() + "'";
        }
        else if (txt_Cont.Text.Length > 0)
        {
            string sql_Con = string.Format("select distinct RefNo from SeaImportMkg where rTRIM(ContainerNo)='{0}'", txt_Cont.Text.Trim());
            DataTable tab = ConnectSql.GetTab(sql_Con);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (i == 0)
                    where = "(ref.RefNo='" + tab.Rows[i][0] + "'";
                else
                    where = " or ref.RefNo='" + tab.Rows[i][0] + "'";
                if (i == tab.Rows.Count - 1)
                    where += ")";
            }
        }
        else
        {
            string ves = this.txt_Ves.Text.Trim();
            string voy = this.txt_Voy.Text.Trim();
            string agtId = this.txt_AgtId.Text.Trim();
            string dateFrom = "";
            string pod = this.txt_SchPod.Text.Trim();
            string dateTo = "";

            if (txt_CustId.Text.Trim() != "")
            {
                where = GetWhere(where, "job.CustomerId='" + txt_CustId.Text.Trim() + "'");
            }
            if (txt_from.Value != null && txt_end.Value != null)
            {
                dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
                dateTo = txt_end.Date.ToString("yyyy-MM-dd");
            }
            if (ves.Length > 0)
                where = GetWhere(where, " ref.Vessel='" + ves + "'");
            if (voy.Length > 0)
                where = GetWhere(where, " ref.Voyage='" + voy + "'");
            if (agtId.Length > 0)
                where = GetWhere(where, " ref.AgentId='" + agtId + "'");
            if (dateFrom.Length > 0 && dateTo.Length > 0)
                where = GetWhere(where, string.Format(" ref.Eta >= '{0}' and ref.Eta <= '{1}'", dateFrom, dateTo));
            if (pod.Length > 0)
                where = GetWhere(where, " ref.Pol='" + pod + "'");
        }

        if (refType.Length > 0)
        {
            where = GetWhere(where, "ref.RefType='" + refType + "'");
        }
        if (where.Length > 0)
        {
            where = " where " + where;
            sql +=where+ " ORDER BY  ref.RefNo Desc,job.JobNo";
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.grid_ref.DataSource= tab;
            this.grid_ref.DataBind();
        }       
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("SeaImportRef", true);
    }

    protected void grid_ref_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            string closeInd = SafeValue.SafeString(this.grid_ref.GetRowValues(e.VisibleIndex, "StatusCode"));
            if (closeInd == "CLS")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            } else if (closeInd == "CNL")
            {
                e.Row.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                DateTime eta = SafeValue.SafeDate(this.grid_ref.GetRowValues(0,"Eta"),new DateTime(1900,1,1));
                if ((DateTime.Today.Subtract(eta)).Days > 30)
                    e.Row.BackColor = System.Drawing.Color.LightPink;

            }
        }

    }
}
