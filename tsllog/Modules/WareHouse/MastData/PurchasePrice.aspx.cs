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

public partial class WareHouse_MastData_PurchasePrice : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
          
        }


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["PriceWhere"] = null;
            this.dsPrice.FilterExpression = "DoType='PQ'";
        }
        if (Session["PriceWhere"] != null)
        {
            this.dsPrice.FilterExpression = Session["PriceWhere"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_SupplierName.Text.Trim().ToUpper();
        string where = " DoType='PQ'";
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format("PartyName LIKE '{0}%'", name.Replace("'", "''")));
        }
        this.dsPrice.FilterExpression = where;
        Session["PriceWhere"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    #region PuchasePrice
    protected void grid_Price_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhTrans));
        }
    }
    protected void grid_Price_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["DoDate"] = DateTime.Now;
        e.NewValues["DoStatus"] = "USE";
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["ExpectedDate"] = DateTime.Today.AddDays(30);
        e.NewValues["WareHouseId"] = "SBL";
    }
    protected void SavePrice()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_Price.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox txt_Id = grid_Price.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string pId = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "Id='" + pId + "'");
            WhTrans whTrans = C2.Manager.ORManager.GetObject(query) as WhTrans;
            ASPxDateEdit issueDate = grid_Price.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            bool isNew = false;
            //const string runType = "DOOUT";
            string issueN = "";
            if (whTrans == null)
            {
                whTrans = new WhTrans();
                isNew = true;
                issueN = C2Setup.GetNextNo("", "PuchasePrice", issueDate.Date);
                whTrans.DoDate = issueDate.Date;
            }

            ASPxDateEdit doDate = grid_Price.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            whTrans.DoDate = doDate.Date;
            ASPxDateEdit txt_ExpectedDate = this.grid_Price.FindEditFormTemplateControl("txt_ExpectedDate") as ASPxDateEdit;
            whTrans.ExpectedDate = txt_ExpectedDate.Date;

            ASPxComboBox doStatus = grid_Price.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            whTrans.DoStatus = SafeValue.SafeString(doStatus.Value);
            //Main Info
            ASPxButtonEdit txt_PartyId = grid_Price.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
            whTrans.PartyId = txt_PartyId.Text;
           
            ASPxTextBox txt_PartyName = grid_Price.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
            whTrans.PartyName = txt_PartyName.Text;
            if (txt_PartyName.Text == "")
            {
                throw new Exception("Supplier not be null!!!");
                return;
            }
            ASPxMemo remark = grid_Price.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            whTrans.Remark = remark.Text;
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                whTrans.StatusCode = "USE";
                whTrans.CreateBy = userId;
                whTrans.CreateDateTime = DateTime.Now;
                whTrans.UpdateBy = userId;
                whTrans.UpdateDateTime = DateTime.Now;
                whTrans.DoType = "PQ";
                whTrans.DoNo = issueN;
                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whTrans);
                C2Setup.SetNextNo("", "PuchasePrice", issueN, issueDate.Date);
                
            }
            else
            {
                whTrans.UpdateBy = userId;
                whTrans.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whTrans);

            }
            Session["PriceWhere"] = "DoNo='" + whTrans.DoNo + "'";
            this.dsPrice.FilterExpression = Session["PriceWhere"].ToString();
            if (this.grid_Price.GetRow(0) != null)
                this.grid_Price.StartEdit(0);

        }
        catch { }
    }

    protected void grid_Price_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Price.EditingRowVisibleIndex > -1)
        {
            string oid = SafeValue.SafeString(this.grid_Price.GetRowValues(this.grid_Price.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
               
            }
        }
    }
    protected void grid_Price_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            SavePrice();
        }
    }
    protected void grid_Price_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxTextBox txt_DoNo = grid_Price.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        string s = e.Parameters;
        if (s == "Save")
        {
            #region Save
            SavePrice();
            e.Result = "Save";
            #endregion
        }

    }
    protected void grid_Price_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        SavePrice();
        e.Cancel = true;
    }
    protected void grid_Price_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        SavePrice();
        e.Cancel = true;
    }
    protected void grid_Price_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    
   
}