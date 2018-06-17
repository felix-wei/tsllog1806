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

public partial class PagesTpt_Job_ContainerType : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Session["TptWhere"] = null;
        //}
        //if (Session["TptWhere"] != null)
        //{
        //    this.dsTransport.FilterExpression = Session["TptWhere"].ToString();
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Job Info
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Container_Type));
        }
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Material"] = " ";
        e.NewValues["ExternalCoat"] = " ";
        e.NewValues["Stacking"] = " ";
    }
    protected void SaveJob()
    {
        try
        {
            ASPxGridView g = this.grid_Transport;


            ASPxTextBox houseId = g.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            int Id = SafeValue.SafeInt(houseId.Text, 0);
            ASPxTextBox parentId = g.FindEditFormTemplateControl("txt_ParentId") as ASPxTextBox;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(Container_Type), "Id='" + Id + "'");
            Container_Type al = C2.Manager.ORManager.GetObject(query) as Container_Type;
            bool isNew = false;
            if (Id == 0)
            {
                isNew = true;
                al = new Container_Type();
            }
            ASPxTextBox containerType = g.FindEditFormTemplateControl("txt_ContainerType") as ASPxTextBox;
            if (checkExist(Id.ToString(), containerType.Text))
            {
                throw new Exception("This Code is exited");
            }
            al.ContainerType = containerType.Text;
            ASPxSpinEdit externalLength = g.FindEditFormTemplateControl("spin_ExternalLength") as ASPxSpinEdit;
            al.ExternalLength = SafeValue.SafeDecimal(externalLength.Value, 0);
            ASPxSpinEdit externalWidth = g.FindEditFormTemplateControl("spin_ExternalWidth") as ASPxSpinEdit;
            al.ExternalBreadth = SafeValue.SafeDecimal(externalWidth.Value, 0);
            ASPxSpinEdit externalHeight = g.FindEditFormTemplateControl("spin_ExternalHeight") as ASPxSpinEdit;
            al.ExternalHeight = SafeValue.SafeDecimal(externalHeight.Value, 0);
            ASPxSpinEdit internalLength = g.FindEditFormTemplateControl("spin_InternalLength") as ASPxSpinEdit;
            al.InternalLength = SafeValue.SafeDecimal(internalLength.Value, 0);
            ASPxSpinEdit internalBreadth = g.FindEditFormTemplateControl("spin_InternalBreadth") as ASPxSpinEdit;
            al.InternalBreadth = SafeValue.SafeDecimal(internalBreadth.Value, 0);
            ASPxSpinEdit internalHeight = g.FindEditFormTemplateControl("spin_InternalHeight") as ASPxSpinEdit;
            al.InternalHeight = SafeValue.SafeDecimal(internalHeight.Value, 0);
            ASPxTextBox material = g.FindEditFormTemplateControl("txt_Material") as ASPxTextBox;
            al.Material = material.Text;
            ASPxTextBox externalCoat = g.FindEditFormTemplateControl("txt_ExternalCoat") as ASPxTextBox;
            al.ExternalCoat = externalCoat.Text;
            ASPxSpinEdit capacity = g.FindEditFormTemplateControl("spin_Capacity") as ASPxSpinEdit;
            al.Capacity = SafeValue.SafeDecimal(capacity.Value, 0);
            ASPxSpinEdit maxGrossWeight = g.FindEditFormTemplateControl("spin_MaxGrossWeight") as ASPxSpinEdit;
            al.MaxGrossWeight = SafeValue.SafeDecimal(maxGrossWeight.Value, 0);
            ASPxSpinEdit tareWeight = g.FindEditFormTemplateControl("spin_TareWeight") as ASPxSpinEdit;
            al.TareWeight = SafeValue.SafeDecimal(tareWeight.Value, 0);
            ASPxSpinEdit maxPayload = g.FindEditFormTemplateControl("spin_MaxPayload") as ASPxSpinEdit;
            al.MaxPayload = SafeValue.SafeDecimal(maxPayload.Value, 0);
            ASPxTextBox stacking = g.FindEditFormTemplateControl("txt_Stacking") as ASPxTextBox;
            al.Stacking = stacking.Text;
            ASPxMemo approvals = g.FindEditFormTemplateControl("txt_Approvals") as ASPxMemo;
            al.Approvals = approvals.Text;



            if (isNew)
            {
                al.CreateBy = EzshipHelper.GetUserName();
                al.CreateDateTime = DateTime.Now;
                al.StatusCode = "Use";
                C2.Manager.ORManager.StartTracking(al, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(al);

                houseId.Text = al.Id.ToString();
                //this.txt_JobNo.Text = tpt1.JobNo;
                //Session["TptWhere"] = "Id='" + al.Id + "'";
                this.dsTransport.FilterExpression = "";// Session["TptWhere"].ToString();
                if (this.grid_Transport.GetRow(0) != null)
                    this.grid_Transport.StartEdit(this.grid_Transport.VisibleRowCount );
            }
            else
            {
                al.UpdateBy = EzshipHelper.GetUserName();
                al.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(al, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(al);
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }


    protected void grid_Transport_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
    }
    protected void grid_Transport_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
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


    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        SaveJob();
        e.Cancel = true;
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        SaveJob();
        e.Cancel = true;
    }
    private bool checkExist(string id, string code)
    {
        string sql = "select COUNT(*) from Ref_ContainerType where ContainerType='" + code + "' and Id<>" + id;
        int result = Convert.ToInt32(ConnectSql.GetTab(sql).Rows[0][0]);
        if (result > 0)
        {
            return true;
        }
        return false;
    }
}

    #endregion