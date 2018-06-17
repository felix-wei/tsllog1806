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

public partial class PagesTpt_Job_ContainerMaster : System.Web.UI.Page
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
            grd.ForceDataRowType(typeof(C2.Container));
        }
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["StatusCode"] = "Use";
    }
    protected string SaveJob()
    {
        try
        {
            ASPxGridView g = this.grid_Transport;


            ASPxTextBox houseId = g.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            int Id = SafeValue.SafeInt(houseId.Text, 0);
            ASPxTextBox parentId = g.FindEditFormTemplateControl("txt_ParentId") as ASPxTextBox;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(Container), "Id='" + Id + "'");
            Container al = C2.Manager.ORManager.GetObject(query) as Container;
            bool isNew = false;
            if (Id == 0)
            {
                isNew = true;
                al = new Container();
            }
            ASPxTextBox containerNo = g.FindEditFormTemplateControl("txt_ContainerNo") as ASPxTextBox;
            ASPxComboBox containerType = g.FindEditFormTemplateControl("cmb_ContainerType") as ASPxComboBox;
            if (containerNo.Text.Trim().Length == 0 || containerType.Text.Trim().Length==0)
            {
                return "ContainerNo and ContainerType are require";
            }
            string checkContainerNO = "select COUNT(*) from Ref_Container where [id]<>" + Id + " and containerNo='" + containerNo.Text+"' ";
            int checkResult = SafeValue.SafeInt(ConnectSql.GetTab(checkContainerNO).Rows[0][0], 0);
            if (checkResult > 0)
            {
                return "This ContainerNo is Existed";
            }
            al.ContainerNo = containerNo.Text;
            ASPxDateEdit commDate = g.FindEditFormTemplateControl("date_CommissionDate") as ASPxDateEdit;
            al.CommDate = commDate.Date;
            ASPxComboBox containerCategory = g.FindEditFormTemplateControl("cmb_ContainerCategory") as ASPxComboBox;
            al.TankCat = containerCategory.Text;
            ASPxTextBox lessor = g.FindEditFormTemplateControl("txt_Lessor") as ASPxTextBox;
            al.Lessor = lessor.Text;
            ASPxDateEdit onHireDateTime = g.FindEditFormTemplateControl("date_OnHireDate") as ASPxDateEdit;
            al.OnHireDateTime = onHireDateTime.Date;
            ASPxDateEdit offHireDateTime = g.FindEditFormTemplateControl("date_OffHireDate") as ASPxDateEdit;
            al.OffHireDateTime = offHireDateTime.Date;
            ASPxDateEdit manuDate = g.FindEditFormTemplateControl("date_Manufacture") as ASPxDateEdit;
            al.ManuDate = manuDate.Date;
            ASPxTextBox manufacturer = g.FindEditFormTemplateControl("txt_Manufacturer") as ASPxTextBox;
            al.Manufacturer = manufacturer.Text;
            ASPxTextBox plateNo = g.FindEditFormTemplateControl("txt_PlateNo") as ASPxTextBox;
            al.PlateNo = plateNo.Text;
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
            ASPxSpinEdit testPress = g.FindEditFormTemplateControl("spin_TestPress") as ASPxSpinEdit;
            al.TestPress = SafeValue.SafeDecimal(testPress.Value, 0);
            ASPxSpinEdit thickness = g.FindEditFormTemplateControl("spin_Thickness") as ASPxSpinEdit;
            al.Thickness = SafeValue.SafeDecimal(thickness.Value, 0);
            ASPxMemo approvals = g.FindEditFormTemplateControl("txt_Approvals") as ASPxMemo;
            al.Approvals = approvals.Text;
            ASPxComboBox inActive = g.FindEditFormTemplateControl("cbb_InActive") as ASPxComboBox;
            al.StatusCode = inActive.Text;


            if (isNew)
            {
                al.CreateBy = EzshipHelper.GetUserName();
                al.CreateDateTime = DateTime.Now;
                //al.StatusCode = "Use";
                C2.Manager.ORManager.StartTracking(al, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(al);


                houseId.Text = al.Id.ToString();
                //this.txt_JobNo.Text = tpt1.JobNo;
                //Session["TptWhere"] = "Id='" + al.Id + "'";
                this.dsTransport.FilterExpression = "";
                //if (this.grid_Transport.GetRow(0) != null)
                //    this.grid_Transport.StartEdit(this.grid_Transport.VisibleRowCount);
            }
            else
            {
                al.UpdateBy = EzshipHelper.GetUserName();
                al.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(al, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(al);
            }
            this.grid_Transport.CancelEdit();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + ex.StackTrace); }
        return "Success";
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
        string s = e.Parameters;
        //throw new Exception("AA");
        try
        {
            if (s == "Save")
            {
                e.Result = "aa";// SaveJob();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ex.StackTrace);
        }
        
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string result = SaveJob();
        if(!result.Equals("Success"))
            throw new Exception(result);
        e.Cancel = true;

    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string result = SaveJob();
        if (!result.Equals("Success"))
            throw new Exception(result);
        e.Cancel = true;
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string ContN = this.txt_ContN.Text.Trim().ToUpper();
        string where = "";
        if (ContN.Length > 0)
        {
            where = string.Format("ContainerNo LIKE '{0}%'", ContN);
        }
        else
        {
            where = "1=1";
        }
        this.dsTransport.FilterExpression = where;
        Session["NameWhere"] = where;
    }
}

#endregion