using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SelectPage_SelectAllRefNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_RefNo.Focus();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string refNo = this.txt_RefNo.Text.Trim().ToUpper();
        string mastType = "";
        if (this.cb_Type.Value!=null)
        {
            mastType=this.cb_Type.Value.ToString();
        }
        string sql = "";
        if (mastType == "SI")
        {
            sql = @"SELECT SequenceId as Id, RefNo as MastRefNo,'SI' as MastType,Eta,Etd,(select Name from XXParty where PartyId=AgentId) as Agent,Pol,Pod  FROM SeaImportRef ";
            if (refNo.Length > 0)
            {
                sql += string.Format(" where RefNo Like '{0}%' and StatusCode='USE' order by SequenceId desc", refNo);
            }
        }
        if (mastType == "SE")
        {
            sql = @"SELECT SequenceId as Id, RefNo as MastRefNo,'SE' as MastType,Eta,Etd,(select Name from XXParty where PartyId=AgentId) as Agent,Pol,Pod  FROM SeaExportRef ";//SCF/SCL/SCC
            if (refNo.Length > 0)
            {
                sql += string.Format("where RefNo Like '{0}%' and StatusCode='USE' order by SequenceId desc", refNo);
            }
        }
        if (mastType == "AI")
        {
            sql = @"SELECT Id, RefNo as MastRefNo,'AI' as MastType,AirportCode0 as Pol,AirportCode1 as Pod,FlightDate0 as Etd,FlightDate1 as Eta,(select Name from XXParty where PartyId=AgentId) as Agent FROM air_ref where RefType='AI'";
            if (refNo.Length > 0)
            {
                sql += string.Format("where RefNo Like '{0}%' and StatusCode='USE' order by Id desc", refNo);
            }
        }
        if (mastType == "AE")
        {
            sql = @"SELECT Id, RefNo as MastRefNo,'AE' as MastType,AirportCode0 as Pol,AirportCode1 as Pod,FlightDate0 as Etd,FlightDate1 as Eta,(select Name from XXParty where PartyId=AgentId) as Agent FROM air_ref where RefType='AE'";
            if (refNo.Length > 0)
            {
                sql += string.Format("where RefNo Like '{0}%' and StatusCode='USE' order by Id desc", refNo);
            }
        }
        if (mastType == "ACT")
        {
            sql = @"SELECT Id, RefNo as MastRefNo,'ACT' as MastType,AirportCode0 as Pol,AirportCode1 as Pod,FlightDate0 as Etd,FlightDate1 as Eta,(select Name from XXParty where PartyId=AgentId) as Agent FROM air_ref where RefType='ACT'";
            if (refNo.Length > 0)
            {
                sql += string.Format("where RefNo Like '{0}%' and StatusCode='USE' order by Id desc", refNo);
            }
        }

        if(sql.Length>0)
        {
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.ASPxGridView1.DataSource = tab;
            this.ASPxGridView1.DataBind();
        }


    }
}