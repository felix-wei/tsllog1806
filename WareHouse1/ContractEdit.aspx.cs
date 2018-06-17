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
using Wilson.ORMapper;

public partial class WareHouse_ContractEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ContractWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["ContractWhere"] = "ContractNo='" + Request.QueryString["no"].ToString() + "' ";
                this.txt_ContractNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["ContractWhere"] == null)
                {
                    this.grid.AddNewRow();
                }
            }
            else
                this.dsWhContract.FilterExpression = "1=0";
        }
        if (Session["ContractWhere"] != null)
        {
            this.dsWhContract.FilterExpression = Session["ContractWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txtId = pageControl.FindControl("txt_Id") as ASPxTextBox;
            string id = SafeValue.SafeString(txtId.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhContract), "Id='" + id + "'");
            WhContract contract = C2.Manager.ORManager.GetObject(query) as WhContract;
            bool isNew = false;
            string contractNo = "";
            if (contract == null)
            {
                contract = new WhContract();
                isNew = true;
                contractNo = C2Setup.GetNextNo("Contract");
            }
            ASPxButtonEdit txt_PartyId = pageControl.FindControl("txt_PartyId") as ASPxButtonEdit;
            contract.PartyId = txt_PartyId.Text;
            ASPxDateEdit txt_Date = pageControl.FindControl("txt_Date") as ASPxDateEdit;
            contract.ContractDate = txt_Date.Date;
            ASPxDateEdit txt_StartDate = pageControl.FindControl("txt_StartDate") as ASPxDateEdit;
            contract.StartDate = txt_StartDate.Date;
            ASPxDateEdit txt_ExpireDate = pageControl.FindControl("txt_ExpireDate") as ASPxDateEdit;
            contract.ExpireDate = txt_ExpireDate.Date;
            ASPxButtonEdit txt_WarehouseId = pageControl.FindControl("txt_WarehouseId") as ASPxButtonEdit;
            contract.WhCode = txt_WarehouseId.Text;
            ASPxButtonEdit txt_SalesmanId = pageControl.FindControl("txt_SalesmanId") as ASPxButtonEdit;

            ASPxMemo txt_Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
            contract.Remark = txt_Remark.Text;
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                contract.CreateBy = userId;
                contract.CreateDateTime = DateTime.Now;
                contract.ContractNo = contractNo.ToString();
                contract.StatusCode = "USE";
                Manager.ORManager.StartTracking(contract, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(contract);
                C2Setup.SetNextNo("Contract", contractNo);
            }
            else
            {
                contract.UpdateBy = userId;
                contract.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(contract, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(contract);
            }
            Session["ContractWhere"] = "ContractNo='" + contract.ContractNo + "'";
            this.dsWhContract.FilterExpression = Session["ContractWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
        catch { }
    }
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
            Save();
    }
    protected void grid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

            ASPxTextBox partyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox whName = pageControl.FindControl("txt_WhName") as ASPxTextBox;
            ASPxDateEdit txt_Date = pageControl.FindControl("txt_Date") as ASPxDateEdit;
            txt_Date.Enabled = false;
            partyName.Text = EzshipHelper.GetPartyName(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
            whName.Text = EzshipHelper.GetWarehouse(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "WhCode" }));
            string oid = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                string sql = string.Format("select StatusCode from wh_Contract  where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grid.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_VoidMaster = this.grid.FindEditFormTemplateControl("btn_Void") as ASPxButton;
                if (closeInd == "CLS")
                {
                    btn.Text = "Open Job";
                }
                if (closeInd == "CNL")
                {
                    btn_VoidMaster.Text = "Unvoid";
                }
                else
                {
                    btn.Text = "Close";
                }
            }
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ContractNo"] = "NEW";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["ContractDate"] = DateTime.Now;
        e.NewValues["StartDate"] = DateTime.Now;
        e.NewValues["PartyId"] = "";
        e.NewValues["WhCode"] = "";
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["ExpireDate"] = DateTime.Today.AddMonths(6);
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox poNo = pageControl.FindControl("txt_RefNo") as ASPxTextBox;
        if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_Contract where ContractNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update wh_Contract set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where ContractNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {

                sql = string.Format("update wh_Contract set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where ContractNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
        if (s == "Void")
        {
            #region void master
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from wh_Contract where ContractNo='" + poNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update wh_Contract set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where ContractNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format("update wh_Contract set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where ContractNo='{0}'", poNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhContract));
        }
    }




    #region Contract det
    protected void grid_det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhContractDet));
    }
    protected void grid_det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_RefNo = pageControl.FindControl("txt_RefNo") as ASPxTextBox;
        this.dsWhContractDet.FilterExpression = " ContractNo='" + txt_RefNo.Text + "'";
    }
    protected void grid_det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_RefNo = pageControl.FindControl("txt_RefNo") as ASPxTextBox;
        e.NewValues["ContractNo"] = txt_RefNo.Text;
        e.NewValues["ProductCode"] = "";

        e.NewValues["IsFixed"] = false;
        e.NewValues["IsYearly"] = false;
        e.NewValues["IsMonthly"] = false;
        e.NewValues["IsWeekly"] = false;
        e.NewValues["IsDaily"] = true;
        e.NewValues["DailyNo"] = 1;
        e.NewValues["HandlingFee"] = 0.00;
        e.NewValues["StorageFee"] = 0.00;
        e.NewValues["Price1"] = 0.00;
        e.NewValues["Price2"] = 0.00;
        e.NewValues["Price3"] = 0.00;
        //ASPxGridView grid = sender as ASPxGridView;
        //ASPxRadioButton rbt_Yearly = grid.FindEditFormTemplateControl("rbt_Yearly") as ASPxRadioButton;
        //ASPxSpinEdit spin_det_Yearly = grid.FindEditFormTemplateControl("spin_det_Yearly") as ASPxSpinEdit;
        //ASPxSpinEdit spin_det_Monthly = grid.FindEditFormTemplateControl("spin_det_Monthly") as ASPxSpinEdit;
        //ASPxSpinEdit spin_det_Weekly = grid.FindEditFormTemplateControl("spin_det_Weekly") as ASPxSpinEdit;
        //ASPxSpinEdit spin_det_Daily = grid.FindEditFormTemplateControl("spin_det_Daily") as ASPxSpinEdit;
        //ASPxSpinEdit spin_det_DailyNo = grid.FindEditFormTemplateControl("spin_det_DailyNo") as ASPxSpinEdit;
        //rbt_Yearly.Checked = true;
        //spin_det_Yearly.ClientEnabled = true;
        //spin_det_Yearly.Value = 1;
        //spin_det_Monthly.Value = 1;
        //spin_det_Weekly.Value = 1;
        //spin_det_Daily.Value = 1;
        //spin_det_DailyNo.Value = 1;

    }
    protected void grid_det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_RefNo = pageControl.FindControl("txt_RefNo") as ASPxTextBox;
        e.NewValues["ContractNo"] = SafeValue.SafeString(txt_RefNo.Text);
        e.NewValues["IsFixed"] = SafeValue.SafeBool(e.NewValues["IsFixed"], false);
        e.NewValues["IsYearly"] = SafeValue.SafeBool(e.NewValues["IsYearly"], false);
        e.NewValues["IsMonthly"] = SafeValue.SafeBool(e.NewValues["IsMonthly"], false);
        e.NewValues["IsWeekly"] = SafeValue.SafeBool(e.NewValues["IsWeekly"], false);
        e.NewValues["IsDaily"] = SafeValue.SafeBool(e.NewValues["IsDaily"], false);
        e.NewValues["ProductCode"] = SafeValue.SafeString(e.NewValues["ProductCode"]);
        e.NewValues["ProductClass"] = SafeValue.SafeString(e.NewValues["ProductClass"]);
        e.NewValues["DailyNo"] = SafeValue.SafeInt(e.NewValues["DailyNo"], 1);
        //e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"], 0);
        //e.NewValues["Price2"] = SafeValue.SafeDecimal(e.NewValues["Price2"], 0);
        //e.NewValues["Price3"] = SafeValue.SafeDecimal(e.NewValues["Price3"], 0);
        e.NewValues["HandlingFee"] = SafeValue.SafeDecimal(e.NewValues["HandlingFee"], 0);
        e.NewValues["StorageFee"] = SafeValue.SafeDecimal(e.NewValues["StorageFee"], 0);

    }

    protected void grid_det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["IsFixed"] = SafeValue.SafeBool(e.NewValues["IsFixed"], false);
        e.NewValues["IsYearly"] = SafeValue.SafeBool(e.NewValues["IsYearly"], false);
        e.NewValues["IsMonthly"] = SafeValue.SafeBool(e.NewValues["IsMonthly"], false);
        e.NewValues["IsWeekly"] = SafeValue.SafeBool(e.NewValues["IsWeekly"], false);
        e.NewValues["IsDaily"] = SafeValue.SafeBool(e.NewValues["IsDaily"], false);
        //e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"], 0);
        //e.NewValues["Price2"] = SafeValue.SafeDecimal(e.NewValues["Price2"], 0);
        //e.NewValues["Price3"] = SafeValue.SafeDecimal(e.NewValues["Price3"], 0);
        e.NewValues["DailyNo"] = SafeValue.SafeInt(e.NewValues["DailyNo"], 1);
        e.NewValues["ProductCode"] = SafeValue.SafeString(e.NewValues["ProductCode"]);
        e.NewValues["ProductClass"] = SafeValue.SafeString(e.NewValues["ProductClass"]);
        e.NewValues["HandlingFee"] = SafeValue.SafeDecimal(e.NewValues["HandlingFee"], 0);
        e.NewValues["StorageFee"] = SafeValue.SafeDecimal(e.NewValues["StorageFee"], 0);
    }
    protected void grid_det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_det_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid.EditingRowVisibleIndex > -1)
        {
            #region
            bool isDaily = SafeValue.SafeBool(grid.GetRowValues(grid.EditingRowVisibleIndex, new string[] { "IsDaily" }), false);
            if(isDaily)
            {
                ASPxSpinEdit dailyNo=grid.FindEditRowCellTemplateControl(null,"spin_det_DailyNo") as ASPxSpinEdit;
                dailyNo.ClientEnabled=false;
            }
            #endregion
        }
    }
    protected void grid_det_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
    }
    #endregion


    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtRefNo = pageControl.FindControl("txt_RefNo") as ASPxTextBox;
        this.dsAttachment.FilterExpression = "RefNo='" + SafeValue.SafeString(txtRefNo.Text, "") + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    #endregion

}