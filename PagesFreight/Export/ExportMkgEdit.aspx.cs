using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;
using DevExpress.Web.ASPxTabControl;
using System.Data;

public partial class PagesFreight_Export_ExportMkgEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string where = "";
            string sql = string.Format("SELECT ref.RefNo FROM SeaExportRef AS ref INNER JOIN SeaExportMkg AS job ON job.RefNo = ref.RefNo WHERE JobType='FCL'");
            DataTable tab = Manager.ORManager.GetDataSet(sql).Tables[0];
            string where1 = "( 1=0";

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                //where1 += string.Format(" and JobType='FCL'");
                where1 += " or refNo='" + tab.Rows[i][0] + "'";
            }
            where1 += ")";
            where = where1;
            this.dsMarking.FilterExpression = where;
            Session["TptWhere"] = where;
        }
        if (Session["TptWhere"] != null)
        {
            this.dsMarking.FilterExpression = Session["TptWhere"].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Mkg Info
    protected void grid_Marking_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaExportMkg));
        }
    }
    protected void grid_Marking_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void SaveJob()
    {
        try
        {
            ASPxGridView g = this.grid_Marking;


            ASPxTextBox houseId = g.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            int Id = SafeValue.SafeInt(houseId.Text, 0);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(SeaExportMkg), "SequenceId='" + Id + "'");
            SeaExportMkg mkg = C2.Manager.ORManager.GetObject(query) as SeaExportMkg;
            bool isNew = false;
            if (Id == 0)
            {
                isNew = true;
                mkg = new SeaExportMkg();
            }
            ASPxDateEdit polEta = g.FindEditFormTemplateControl("date_PolEta") as ASPxDateEdit;
            mkg.PolEta = polEta.Date;
            ASPxDateEdit polClearDate = g.FindEditFormTemplateControl("date_PolClearDate") as ASPxDateEdit;
            mkg.PolClearDate = polClearDate.Date;
            ASPxDateEdit polReturnDate = g.FindEditFormTemplateControl("date_PolReturnDate") as ASPxDateEdit;
            mkg.PolReturnDate = polReturnDate.Date;
            ASPxDateEdit podEta = g.FindEditFormTemplateControl("date_PodEta") as ASPxDateEdit;
            mkg.PodEta = podEta.Date;
            ASPxDateEdit podClearDate = g.FindEditFormTemplateControl("date_PodClearDate") as ASPxDateEdit;
            mkg.PodClearDate = podClearDate.Date;
            ASPxDateEdit podReturnDate = g.FindEditFormTemplateControl("date_PodReturnDate") as ASPxDateEdit;
            mkg.PodReturnDate = podReturnDate.Date;
            ASPxMemo polRemark = g.FindEditFormTemplateControl("memo_PolRemark") as ASPxMemo;
            mkg.PolRemark = polRemark.Text;
            ASPxMemo podRemark = g.FindEditFormTemplateControl("memo_PodRemark") as ASPxMemo;
            mkg.PodRemark = podRemark.Text;



            if (isNew)
            {
                C2.Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(mkg);
                houseId.Text = mkg.SequenceId.ToString();
                //this.txt_JobNo.Text = tpt1.JobNo;
                Session["TptWhere"] = "SequenceId='" + mkg.SequenceId + "'";
                this.dsMarking.FilterExpression = Session["TptWhere"].ToString();
                if (this.grid_Marking.GetRow(0) != null)
                    this.grid_Marking.StartEdit(0);
            }
            else
            {
                C2.Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(mkg);
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + ex.StackTrace); }
    }


    protected void grid_Marking_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Marking.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox agentId = this.grid_Marking.FindEditFormTemplateControl("txt_AgentId") as ASPxTextBox;
            ASPxTextBox agentName = this.grid_Marking.FindEditFormTemplateControl("txt_AgentName") as ASPxTextBox;
            agentName.Text = EzshipHelper.GetPartyName(this.grid_Marking.GetRowValues(this.grid_Marking.EditingRowVisibleIndex, new string[] { "AgentId" }));
        }
    }
    protected void grid_Marking_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        //throw new Exception("AA");
        try
        {
            if (s == "Save")
                SaveJob();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ex.StackTrace);
        }
    }


    protected void grid_Marking_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    protected void grid_Marking_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_Marking_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        SaveJob();
        e.Cancel = true;
    }
    protected void grid_Marking_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        SaveJob();
        e.Cancel = true;
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string RefN = this.txt_RefN.Text.Trim().ToUpper();
        string ContN = this.txt_ContN.Text.Trim().ToUpper();
        string where = "";
        if (RefN.Length > 0)
        {
            where = string.Format("job.RefNo LIKE '{0}%'", RefN);
        }
        else if (ContN.Length > 0)
        {
            where = string.Format("ContainerNo LIKE '{0}%'", ContN);
        }
        else
        {
            where = "1=1";
        }
        string sql = string.Format("SELECT ref.RefNo FROM SeaExportRef AS ref INNER JOIN SeaExportMkg AS job ON job.RefNo = ref.RefNo WHERE JobType='FCL' and {0}", where);
        DataTable tab = Manager.ORManager.GetDataSet(sql).Tables[0];
        string where1 = "( 1=0";

        for (int i = 0; i < tab.Rows.Count; i++)
        {
            //where1 += string.Format(" and JobType='FCL'");
            where1 += " or refNo='"+tab.Rows[i][0]+"'";
        }
        where1 += ")";
        where = where1;
        this.dsMarking.FilterExpression = where;
        Session["NameWhere"] = where;
    }
}

#endregion