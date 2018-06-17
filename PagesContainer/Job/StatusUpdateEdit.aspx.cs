using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxDataView;

public partial class PagesContainer_Job_StatusUpdateEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = "select aEvent.id from Cont_AssetEvent as aEvent left outer join (select ContainerNo,MAX(eventDateTime) as maxEventDateTime from Cont_AssetEvent group by ContainerNo) as ass on aEvent.ContainerNo=ass.ContainerNo and aEvent.EventDateTime=ass.maxEventDateTime where ass.maxEventDateTime is not null ";

        string where = "";
        if (txt_ContainerNo.Text.Trim() != "")
        {
            where = " and ContainerNo='" + txt_ContainerNo.Text.Trim() + "'";

        }
        string status = this.cb_Status.Text.Trim();
        string statusBack = "";
        switch (status)
        {
            case "boxdischarge":
                statusBack = "'boxload'";
                break;
            case "gateout":
                statusBack = "'boxdischarge'";
                break;
            case "gatein":
                statusBack = "'depotout'";
                break;
            case "boxload":
                statusBack = "'gatein'";
                break;
            default:
                statusBack = "'gatein','depotout','boxload','boxdischarge'";
                break;
        }
        where += " and EventCode in (" + statusBack + ")";

        string from = "";
        string to = "";
        if (this.date_EventFrom.Value != null && this.date_EventTo.Value != null)
        {
            from = this.date_EventFrom.Date.ToString("yyyy-MM-dd");
            to = this.date_EventTo.Date.ToString("yyyy-MM-dd");
        }
        if (from.Length > 0 && to.Length > 0)
        {
            where = GetWhere(where, string.Format(" EventDateTime>='{0}' and EventDateTime<='{1}'", from, to));
        }
        sql += where;
        sql = "id in (" + sql + ")";
        dsTransport.FilterExpression = sql;




        //string where = "";
        //string status = "";
        //if (txt_ContainerNo.Text.Trim() != "")
        //{
        //    where = " and ContainerNo='" + txt_ContainerNo.Text.Trim() + "'";

        //}
        //if (where.Length == 0)
        //{
        //    status = this.cb_Status.Text.Trim();
        //    string from = "";
        //    string to = "";
        //    if (this.date_EventFrom.Value != null && this.date_EventTo.Value != null)
        //    {
        //        from = this.date_EventFrom.Date.ToString("yyyy-MM-dd");
        //        to = this.date_EventTo.Date.ToString("yyyy-MM-dd");
        //    }
        //    if(status.Length>0){
        //        if (status == "boxdischarge")
        //        {
        //            where = GetWhere(where, "EventCode='boxload'");
        //        }
        //        if (status == "gateout")
        //        {
        //            where = GetWhere(where, "EventCode='boxdischarge'");
        //        }
        //        if (status == "gatein")
        //        {
        //            where = GetWhere(where, "EventCode='depotout'");
        //        }
        //        if (status == "boxload")
        //        {
        //            where = GetWhere(where, "EventCode='gatein'");
        //        }
        //    }
        //    if (from.Length > 0 && to.Length > 0)
        //    {
        //        where = GetWhere(where, string.Format(" EventDateTime>='{0}' and EventDateTime<='{1}'", from, to));
        //    }
        //}
        //if (where.Length > 0)
        //{
        //    if(status==""){
        //        where = GetWhere(where, " EventDateTime=(select MAX(EventDateTime) from Cont_AssetEvent) and EventCode in('gatein','gateout','boxload','boxdischarge')");
        //    }
        //    this.dsTransport.FilterExpression = where;
        //}
        //else
        //{
        //    this.dsTransport.FilterExpression = "EventCode in('gatein','gateout','boxload','boxdischarge')";
        //}
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.ContAssetEvent));
        }
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox txt_EventCode = this.grid_Transport.FindEditFormTemplateControl("txt_EventCode") as ASPxTextBox; 
        ASPxComboBox cb_EventCode = this.grid_Transport.FindEditFormTemplateControl("cb_EventCode") as ASPxComboBox;
        string EventCode_now = cb_EventCode.Text;
        e.NewValues["EventCode"] = txt_EventCode.Text;
        if (!EventCode_now.Equals(txt_EventCode.Text))
        {
            ASPxTextBox txt_id = this.grid_Transport.FindEditFormTemplateControl("txt_sequenceId") as ASPxTextBox;
            string sql = "insert into Cont_AssetEvent(CreateDateTime,UpdateDateTime,JobNo,EventCode,EventDateTime,EventPort,EventDepot,Pol,Pod,VehicleNo,ReturnDate,ReceiveDate,State,ReleaseDate,Insturction,ContainerNo,ContainerType) select GETDATE(),GETDATE(),'','" + cb_EventCode.Text + "',GETDATE(),case when EventCode='boxload' then Pod else EventPort end,EventDepot,Pol,Pod,VehicleNo,ReturnDate,ReceiveDate,State,ReleaseDate,Insturction,ContainerNo,ContainerType from Cont_AssetEvent where [id]=" + txt_id.Text;
            ConnectSql.ExecuteSql(sql);
        }

        //ASPxComboBox eventCode = this.grid_Transport.FindEditFormTemplateControl("cb_EventCode") as ASPxComboBox;
        //e.NewValues["EventCode"] = eventCode.Text;
        btn_search_Click(null, null);
        //e.NewValues["EventDateTime"] = DateTime.Now;
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
       
    }
    protected void grid_Transport_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if(this.grid_Transport.EditingRowVisibleIndex>-1)
        {
            ASPxComboBox cb_EventCode = this.grid_Transport.FindEditFormTemplateControl("cb_EventCode") as ASPxComboBox;
            object eventCode = this.grid_Transport.GetRowValues(this.grid_Transport.EditingRowVisibleIndex, new string[] { "EventCode" });

            if (eventCode.ToString() == "gatein")
            {
                cb_EventCode.Items.Add().Value = "boxload";
            }
            if (eventCode.ToString() == "depotout")
            {
                cb_EventCode.Items.Add().Value = "gatein";
            }
            if (eventCode.ToString() == "boxdischarge")
            {
                cb_EventCode.Items.Add().Value = "gateout";
            }
            if (eventCode.ToString() == "boxload")
            {
                cb_EventCode.Items.Add().Value = "boxdischarge";
            }
        }
    }
}