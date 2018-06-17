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
using System.Xml;
using System.IO;

public partial class WareHouse_Job_DoIn : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["DoInWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["DoInWhere"] = "DoNo='" + Request.QueryString["no"].ToString() + "' ";
                this.txt_DoNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["DoInWhere"] == null)
                {
                    this.grd_Do.AddNewRow();
                }
            }
            else
                this.dsDo.FilterExpression = "1=0";
        }
        if (Session["DoInWhere"] != null)
        {
            this.dsDo.FilterExpression = Session["DoInWhere"].ToString();
            if (this.grd_Do.GetRow(0) != null)
                this.grd_Do.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region Do
    protected void grd_Do_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhDo));
    }
    protected void grd_Do_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DoNo"] = "";
        e.NewValues["DoType"] = "IN";
        e.NewValues["Priority"] = "IMPORT";
        e.NewValues["DoDate"] =DateTime.Now;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Gst"] = 0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["ExpectedDate"] = DateTime.Today;
        e.NewValues["PartyRefDate"] = DateTime.Today;
        e.NewValues["PoDate"] = DateTime.Today;
        e.NewValues["PartyInvDate"] = DateTime.Today;
        e.NewValues["GateInDate"] = DateTime.Today;
        e.NewValues["GateOutDate"] = DateTime.Today;
        e.NewValues["Eta"] = DateTime.Today;
        e.NewValues["Etd"] = DateTime.Today;
        e.NewValues["EtaDest"] = DateTime.Today;
        e.NewValues["EtaDest"] = DateTime.Today;
        e.NewValues["PermitApprovalDate"] = DateTime.Today;
        e.NewValues["WareHouseId"] = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];

    }
    protected void grd_Do_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grd_Do.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxButtonEdit txt_Do_PartyId =this.grd_Do.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
            ASPxTextBox partyName =this.grd_Do.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
            partyName.Text = EzshipHelper.GetPartyName(this.grd_Do.GetRowValues(this.grd_Do.EditingRowVisibleIndex, new string[] { "PartyId" }));
            string oid = SafeValue.SafeString(this.grd_Do.GetRowValues(this.grd_Do.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                //ASPxDateEdit txt_Date = this.grd_Do.FindEditFormTemplateControl("txt_Date") as ASPxDateEdit;
                //txt_Date.BackColor = ((DevExpress.Web.ASPxEditors.ASPxTextBox)(this.grd_Do.FindEditFormTemplateControl("txt_DoNo"))).BackColor;
                //txt_Date.ReadOnly = true;
                string sql = string.Format("select StatusCode from wh_DO where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grd_Do.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_Void = this.grd_Do.FindEditFormTemplateControl("btn_Void") as ASPxButton;
                if (closeInd == "CLS")
                {
                    btn.Text = "Open Job";
                }
                if (closeInd == "CNL")
                {
                    btn_Void.Text = "Unvoid";
                }
            }

        }
    }
    protected void grd_Do_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        //if (s == "Save")
        //{
           
        //    Save();
        //}
    }
    private string Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_Id =this.grd_Do.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            ASPxDateEdit txt_Date = this.grd_Do.FindEditFormTemplateControl("txt_Date") as ASPxDateEdit;
            string pId = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + pId + "'");
            WhDo whDo = C2.Manager.ORManager.GetObject(query) as WhDo;
            bool action = false;
            string doNo = "";
            if (whDo == null)
            {
                action = true;
                whDo = new WhDo();
                doNo = C2Setup.GetNextNo("", "DOIN", txt_Date.Date);
            }
            ASPxComboBox cmb_Status = this.grd_Do.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            whDo.DoStatus = SafeValue.SafeString(cmb_Status.Value);
            ASPxDateEdit txt_PartyRefDate =this.grd_Do.FindEditFormTemplateControl("txt_PartyRefDate") as ASPxDateEdit;
            whDo.CustomerDate = txt_PartyRefDate.Date;
            ASPxDateEdit txt_ExpectedDate =this.grd_Do.FindEditFormTemplateControl("txt_ExpectedDate") as ASPxDateEdit;
            whDo.ExpectedDate = txt_ExpectedDate.Date;
            ASPxDateEdit txt_PoDate =this.grd_Do.FindEditFormTemplateControl("txt_PoDate") as ASPxDateEdit;
            whDo.PoDate = txt_PoDate.Date;
            ASPxTextBox txt_PoNo =this.grd_Do.FindEditFormTemplateControl("txt_PoNo") as ASPxTextBox;
            whDo.PoNo = txt_PoNo.Text;
            whDo.DoDate =SafeValue.SafeDate(txt_Date.Date,DateTime.Today);
            ASPxMemo txt_Remark1 =this.grd_Do.FindEditFormTemplateControl("txt_Remark1") as ASPxMemo;
            ASPxMemo txt_Remark2 =this.grd_Do.FindEditFormTemplateControl("txt_Remark2") as ASPxMemo;
            whDo.Remark1 = txt_Remark1.Text;
            whDo.Remark2 = txt_Remark2.Text;

            ASPxButtonEdit txt_PartyId =this.grd_Do.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
            whDo.PartyId = txt_PartyId.Text;
            ASPxTextBox txt_PartyName =this.grd_Do.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
            whDo.PartyName = txt_PartyName.Text;
            ASPxMemo txt_PartyAdd =this.grd_Do.FindEditFormTemplateControl("txt_PartyAdd") as ASPxMemo;
            whDo.PartyAdd = txt_PartyAdd.Text;
            ASPxTextBox txt_PostalCode =this.grd_Do.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            whDo.PartyPostalcode = txt_PostalCode.Text;
            ASPxButtonEdit txt_PartyCity =this.grd_Do.FindEditFormTemplateControl("txt_PartyCity") as ASPxButtonEdit;
            whDo.PartyCity = txt_PartyCity.Text;
            ASPxButtonEdit txt_PartyCountry =this.grd_Do.FindEditFormTemplateControl("txt_PartyCountry") as ASPxButtonEdit;
            whDo.PartyCountry = txt_PartyCountry.Text;
            ASPxComboBox cmb_Priority = this.grd_Do.FindEditFormTemplateControl("cmb_Priority") as ASPxComboBox;
            whDo.Priority = SafeValue.SafeString(cmb_Priority.Value);
            ASPxTextBox txt_CustReference =this.grd_Do.FindEditFormTemplateControl("txt_CustReference") as ASPxTextBox;
            whDo.CustomerReference = txt_CustReference.Text;
            ASPxDateEdit txt_PartyInvDate =this.grd_Do.FindEditFormTemplateControl("txt_PartyInvDate") as ASPxDateEdit;
            whDo.PartyInvDate = txt_PartyInvDate.Date;
            ASPxComboBox cb_TptMode =this.grd_Do.FindEditFormTemplateControl("cb_TptMode") as ASPxComboBox;
            whDo.TptMode = SafeValue.SafeString(cb_TptMode.Value);
            ASPxComboBox cb_IncoTerms =this.grd_Do.FindEditFormTemplateControl("cb_IncoTerms") as ASPxComboBox;
            whDo.IncoTerms = SafeValue.SafeString(cb_IncoTerms.Value);
            ASPxTextBox txt_CustomsSealNo =this.grd_Do.FindEditFormTemplateControl("txt_CustomsSealNo") as ASPxTextBox;
            whDo.CustomsSealNo = txt_CustomsSealNo.Text;
            ASPxTextBox txt_PartyInvNo =this.grd_Do.FindEditFormTemplateControl("txt_PartyInvNo") as ASPxTextBox;
            whDo.PartyInvNo = txt_PartyInvNo.Text;
            ASPxTextBox txt_PermitNo =this.grd_Do.FindEditFormTemplateControl("txt_PermitNo") as ASPxTextBox;
            whDo.PermitNo = txt_PermitNo.Text;
            ASPxTextBox txt_TptName =this.grd_Do.FindEditFormTemplateControl("txt_TptName") as ASPxTextBox;
            whDo.TptName = txt_TptName.Text;
            ASPxTextBox txt_Group =this.grd_Do.FindEditFormTemplateControl("txt_Group") as ASPxTextBox;
            whDo.GroupId = txt_Group.Text;
            ASPxDateEdit date_PermitApprovalDate =this.grd_Do.FindEditFormTemplateControl("date_PermitApprovalDate") as ASPxDateEdit;
            whDo.PermitApprovalDate = date_PermitApprovalDate.Date;
            ASPxTextBox txt_JobNo =this.grd_Do.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            whDo.JobNo = txt_JobNo.Text;
            ASPxTextBox txt_IssueNo =this.grd_Do.FindEditFormTemplateControl("txt_IssueNo") as ASPxTextBox;
            whDo.IssueNo = txt_IssueNo.Text;
            ASPxCheckBox ack_PalletizedInd =this.grd_Do.FindEditFormTemplateControl("ack_PalletizedInd") as ASPxCheckBox;
            whDo.PalletizedInd = (ack_PalletizedInd.Checked) ? true : false;
            ASPxDateEdit date_GateInDate =this.grd_Do.FindEditFormTemplateControl("date_GateInDate") as ASPxDateEdit;
            whDo.GateInDate = date_GateInDate.Date;
            ASPxDateEdit date_GateOutDate =this.grd_Do.FindEditFormTemplateControl("date_GateOutDate") as ASPxDateEdit;
            whDo.GateOutDate = date_GateOutDate.Date;
            ASPxCheckBox ack_PopulateInd =this.grd_Do.FindEditFormTemplateControl("ack_PopulateInd") as ASPxCheckBox;
            whDo.PopulateInd = (ack_PopulateInd.Checked) ? true : false;
            ASPxComboBox cmb_EquipmentNo =this.grd_Do.FindEditFormTemplateControl("cmb_EquipmentNo") as ASPxComboBox;
            whDo.EquipNo =SafeValue.SafeString(cmb_EquipmentNo.Value);
            ASPxComboBox cmb_Personnel =this.grd_Do.FindEditFormTemplateControl("cmb_Personnel") as ASPxComboBox;
            whDo.Personnel = SafeValue.SafeString(cmb_Personnel.Value);
            ASPxButtonEdit txt_Quotation =this.grd_Do.FindEditFormTemplateControl("txt_Quotation") as ASPxButtonEdit;
            whDo.Quotation = txt_Quotation.Text;
            ASPxMemo txt_CollectFrom =this.grd_Do.FindEditFormTemplateControl("txt_CollectFrom") as ASPxMemo;
            whDo.CollectFrom = txt_CollectFrom.Text;
            //ASPxMemo txt_DeliveryTo =this.grd_Do.FindEditFormTemplateControl("txt_DeliveryTo") as ASPxMemo;
            //whDo.DeliveryTo = txt_DeliveryTo.Text;
            
            ASPxComboBox PermitBy = grd_Do.FindEditFormTemplateControl("txt_PermitBy") as ASPxComboBox;
            whDo.PermitBy = PermitBy.Text;
            ASPxMemo OtherPermit = grd_Do.FindEditFormTemplateControl("txt_OtherPermit") as ASPxMemo;
            whDo.OtherPermit = OtherPermit.Text;
            ASPxComboBox ModelType = grd_Do.FindEditFormTemplateControl("cmb_ModelType") as ASPxComboBox;
            whDo.ModelType = ModelType.Text;
            ASPxDateEdit PermitExpiryDate = grd_Do.FindEditFormTemplateControl("date_PermitExpiryDate") as ASPxDateEdit;
            whDo.PermitExpiryDate = PermitExpiryDate.Date;
            
            ASPxButtonEdit txt_Contractor = grd_Do.FindEditFormTemplateControl("txt_Contractor") as ASPxButtonEdit;
            whDo.Contractor= txt_Contractor.Text;
            ASPxButtonEdit txt_WareHouseId = grd_Do.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            whDo.WareHouseId= txt_WareHouseId.Text;
            //SHIPMENT
            ASPxTextBox txt_Vessel = pageControl.FindControl("txt_Vessel") as ASPxTextBox;
            whDo.Vessel = txt_Vessel.Text;
            ASPxTextBox txt_Voyage = pageControl.FindControl("txt_Voyage") as ASPxTextBox;
            whDo.Voyage = txt_Voyage.Text;
            ASPxDateEdit date_Etd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            whDo.Etd = date_Etd.Date;
            ASPxButtonEdit txt_Pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
            whDo.Pol = txt_Pol.Text;
            ASPxButtonEdit txt_Pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
            whDo.Pod = txt_Pod.Text;
            ASPxDateEdit date_Eta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
            whDo.Eta = date_Eta.Date;
            ASPxTextBox txt_OceanBl = pageControl.FindControl("txt_OceanBl") as ASPxTextBox;
            whDo.Obl = txt_OceanBl.Text;
            ASPxTextBox txt_HouseBl = pageControl.FindControl("txt_HouseBl") as ASPxTextBox;
            whDo.Hbl = txt_HouseBl.Text;
            ASPxDateEdit date_EtaDest = pageControl.FindControl("date_EtaDest") as ASPxDateEdit;
            whDo.EtaDest = date_EtaDest.Date;
            ASPxTextBox txt_VehicleNo = pageControl.FindControl("txt_VehicleNo") as ASPxTextBox;
            whDo.Vehicle = txt_VehicleNo.Text;
            ASPxTextBox txt_COO = pageControl.FindControl("txt_COO") as ASPxTextBox;
            whDo.Coo= txt_COO.Text;
            ASPxButtonEdit txt_Carrier = pageControl.FindControl("txt_Carrier") as ASPxButtonEdit;
            whDo.Carrier = txt_Carrier.Text;
            ASPxButtonEdit txt_AgentId = pageControl.FindControl("txt_AgentId") as ASPxButtonEdit;
            whDo.AgentId = txt_AgentId.Text;
            ASPxTextBox txt_AgentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            whDo.AgentName = txt_AgentName.Text;
            ASPxTextBox txt_AgentZip = pageControl.FindControl("txt_AgentZip") as ASPxTextBox;
            whDo.AgentZip = txt_AgentZip.Text;
            ASPxMemo txt_AgentAdd = pageControl.FindControl("txt_AgentAdd") as ASPxMemo;
            whDo.AgentAdd = txt_AgentAdd.Text;
            ASPxTextBox txt_AgentTel = pageControl.FindControl("txt_AgentTel") as ASPxTextBox;
            whDo.AgentTel = txt_AgentTel.Text;
            ASPxTextBox txt_AgentContact = pageControl.FindControl("txt_AgentContact") as ASPxTextBox;
            whDo.AgentContact = txt_AgentContact.Text;
            ASPxButtonEdit txt_AgentCountry = pageControl.FindControl("txt_AgentCountry") as ASPxButtonEdit;
            whDo.AgentCountry = txt_AgentCountry.Text;
            ASPxButtonEdit txt_AgentCity = pageControl.FindControl("txt_AgentCity") as ASPxButtonEdit;
            whDo.AgentCity = txt_AgentCity.Text;
            ASPxButtonEdit txt_NotifyId = pageControl.FindControl("txt_NotifyId") as ASPxButtonEdit;
            whDo.NotifyId = txt_NotifyId.Text;
            ASPxTextBox txt_NotifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            whDo.NotifyName = txt_NotifyName.Text;
            ASPxTextBox txt_NotifyZip = pageControl.FindControl("txt_NotifyZip") as ASPxTextBox;
            whDo.NotifyZip = txt_NotifyZip.Text;
            ASPxMemo txt_NotifyAdd = pageControl.FindControl("txt_NotifyAdd") as ASPxMemo;
            whDo.NotifyAdd = txt_NotifyAdd.Text;
            ASPxTextBox txt_NotifyTel = pageControl.FindControl("txt_NotifyTel") as ASPxTextBox;
            whDo.NotifyTel = txt_NotifyTel.Text;
            ASPxTextBox txt_NotifyContact = pageControl.FindControl("txt_NotifyContact") as ASPxTextBox;
            whDo.NotifyContact = txt_NotifyContact.Text;
            ASPxButtonEdit txt_NotifyCountry = pageControl.FindControl("txt_NotifyCountry") as ASPxButtonEdit;
            whDo.NotifyCountry = txt_NotifyCountry.Text;
            ASPxButtonEdit txt_NotifyCity = pageControl.FindControl("txt_NotifyCity") as ASPxButtonEdit;
            whDo.NotifyCity = txt_NotifyCity.Text;
            ASPxButtonEdit txt_ConsigneeId = pageControl.FindControl("txt_ConsigneeId") as ASPxButtonEdit;
            whDo.ConsigneeId = txt_ConsigneeId.Text;
            ASPxTextBox txt_ConsigneeName = pageControl.FindControl("txt_ConsigneeName") as ASPxTextBox;
            whDo.ConsigneeName = txt_ConsigneeName.Text;
            ASPxTextBox txt_ConsigneeZip = pageControl.FindControl("txt_ConsigneeZip") as ASPxTextBox;
            whDo.ConsigneeZip = txt_ConsigneeZip.Text;
            ASPxMemo txt_ConsigneeAdd = pageControl.FindControl("txt_ConsigneeAdd") as ASPxMemo;
            whDo.ConsigneeAdd = txt_ConsigneeAdd.Text;
            ASPxTextBox txt_ConsigneeTel = pageControl.FindControl("txt_ConsigneeTel") as ASPxTextBox;
            whDo.ConsigneeTel = txt_ConsigneeTel.Text;
            ASPxTextBox txt_ConsigneeContact = pageControl.FindControl("txt_ConsigneeContact") as ASPxTextBox;
            whDo.ConsigneeContact = txt_ConsigneeContact.Text;
            ASPxButtonEdit txt_ConsigneeCountry = pageControl.FindControl("txt_ConsigneeCountry") as ASPxButtonEdit;
            whDo.ConsigneeCountry = txt_ConsigneeCountry.Text;
            ASPxButtonEdit txt_ConsigneeCity = pageControl.FindControl("txt_ConsigneeCity") as ASPxButtonEdit;
            whDo.ConsigneeCity = txt_ConsigneeCity.Text;

            ASPxTextBox txt_ContainerYard = pageControl.FindControl("txt_ContainerYard") as ASPxTextBox;
            whDo.ContainerYard= txt_ContainerYard.Text;
            ASPxButtonEdit txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
            string newDoNo = SafeValue.SafeString(txtDoNo.Text);
            if (action)
            {
                whDo.DoType = "IN";
                whDo.StatusCode = "USE";
                whDo.CreateBy = EzshipHelper.GetUserName();
                whDo.CreateDateTime = DateTime.Now;

                string sql = string.Format(@"select Count(*) from Wh_DO where DoNo='{0}'", newDoNo);
                int result=SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
                if (result == 0)
                {
                    if (txtDoNo.ReadOnly == false)
                    {
                        doNo = newDoNo;
                    }
                    else
                    {
                        C2Setup.SetNextNo("", "DOIN", doNo, txt_Date.Date);
                    }
                    whDo.DoNo = doNo;
                    Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(whDo);

                }
                else
                {
                    throw new Exception(string.Format("Had DoNo"));
                    return "";
                }

            }
            else
            {
                whDo.UpdateBy = EzshipHelper.GetUserName();
                whDo.UpdateDateTime = DateTime.Now;
                bool isAddLog = false;
                if (whDo.DoStatus == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select DoStatus from wh_do where DoNo='" + whDo.DoNo + "'")))
                {
                }
                else
                {
                    isAddLog = true;
                }
                Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whDo);
                UpdateDoDet2(whDo.DoNo, whDo.DoType);
                if (isAddLog)
                    EzshipLog.Log(whDo.DoNo, "", "DoIn", whDo.DoStatus);
            }
            Session["DoInWhere"] = "DoNo='" + whDo.DoNo + "'";
            this.dsDo.FilterExpression = Session["DoInWhere"].ToString();
            if (this.grd_Do.GetRow(0) != null)
                this.grd_Do.StartEdit(0);
            return whDo.DoNo;
        }
        catch { }
        return "";
    }
    protected void grd_Do_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox id =this.grd_Do.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxButtonEdit doNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        ASPxButtonEdit txt_WareHouseId = this.grd_Do.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        //string sql = "select Count(*) from Wh_DoDet where DoNo='" + SafeValue.SafeString(doNo.Text) + "' and (JobStatus='Draft' or JobStatus='Waiting')";
       // int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
        string sql = "";
        int cnt = 0;
        if(s=="Save"){
             ASPxButtonEdit txt_PartyId = grd_Do.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
            if (txt_PartyId.Text.Trim() == "")
            {
                e.Result = "Fail!";
                return;
            }
           e.Result = Save();// new one
            return;
        }
        if (s == "CloseJob")
        {
            #region close job

            sql = "select StatusCode from Wh_DO where DoNo='" + doNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update Wh_DO set StatusCode='USE' where DoNo='{0}'", doNo.Text);
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
                //permit no ,permit date
                sql = string.Format("select count(Id) from wh_do where  len(Isnull(PermitNo,''))>0 and PermitApprovalDate>'2010-1-1' and DoNo='{0}'", doNo.Text);
                int res = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (res == 0)
                {
                    e.Result = "Permit";
                    return;
                }
                //lot no
                sql = string.Format("select count(Id) from wh_doDet where  len(Isnull(LotNo,''))=0 and DoNo='{0}'", doNo.Text);
                res = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (res > 0)
                {
                    e.Result = "LotNo";
                    return;
                }
                //sql = string.Format(@"select sum(CostLocAmt) from Wh_Costing where RefNo='{0}' and JobType='IN'", doNo.Text);
                //decimal locAmt = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
                //if (locAmt == 0)
                //{
                //    e.Result = "No Costing";
                //    return;
                //}
                sql = string.Format("update Wh_DO set StatusCode='CLS' where DoNo='{0}'", doNo.Text);
                res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    TransferToCompany(doNo.Text, "IN");
                    sql = string.Format(@"update wh_trans set DoStatus='Received' where DoNo='{0}'",doNo.Text);
                    Manager.ORManager.ExecuteCommand(sql);
                    e.Result = "Success";
                }
                else
                    e.Result = "Fail";

            }
            #endregion
        }

        if (s == "Void")
        {
            #region void master
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='WH' and MastRefNo='{0}'", doNo.Text);
            cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            sql = "select StatusCode from Wh_DO where DoNo='" + doNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update Wh_DO set StatusCode='USE' where DoNo='{0}'", doNo.Text);
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
                sql = string.Format("update Wh_DO set StatusCode='CNL' where DoNo='{0}'", doNo.Text);

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
        if (s == "Receive")
        {
            #region Receive
            sql = string.Format(@"select Id,Qty5,Qty2,Qty3 from Wh_DoDet where DoNo='{0}' and Qty5<>0", doNo.Text);
            DataTable tab = ConnectSql.GetTab(sql);
            if (tab.Rows.Count > 0)
            {
                sql = string.Format(@"select Code from ref_location where WarehouseCode='{0}' and Loclevel='Unit'", txt_WareHouseId.Text);
                string loc =SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql));
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    int detId = SafeValue.SafeInt(tab.Rows[i]["Id"], 0);
                    int qty = SafeValue.SafeInt(tab.Rows[i]["Qty5"], 0);
                    int qty2 = SafeValue.SafeInt(tab.Rows[i]["Qty2"], 0);
                    int qty3 = SafeValue.SafeInt(tab.Rows[i]["Qty3"], 0);
                    sql = string.Format(@"update Wh_DoDet set Qty1=Qty5 where Id={0}", detId);
                    int result = Manager.ORManager.ExecuteCommand(sql);
                    if (result > 0)
                    {
                        C2.Manager.ORManager.ExecuteScalar(string.Format(@"update Wh_DoDet set Qty5=0 where Id={0}", detId));

                        sql = @"Insert Into wh_DoDet2(DoNo,DoType,Product,Des1,Price,Packing,Qty1,Qty2,Qty3,Location,LotNo,BatchNo,CustomsLot,ProcessStatus,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime])";
                        sql += string.Format(@"select DoNo,DoType,ProductCode,Des1,Price,Packing,'{2}' as Qty1,'{3}' as Qty2,'{4}' as Qty3,'{5}' as Location,LotNo,BatchNo,CustomsLot,'Delivered' as ProcessStatus,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,'{1}' as CreateBy,getdate(),'{1}' as UpdateBy,getdate() from Wh_DoDet where Id='{0}'"
                            , detId, EzshipHelper.GetUserName(), qty, qty2, qty3, loc);
                        cnt = C2.Manager.ORManager.ExecuteCommand(sql);
                        
                        e.Result = "Receive";
                    }
                    else
                        e.Result = "Faile";
                }
            }
            else
            {
                e.Result = "No Receive";
            }
            #endregion 

        }
        else if (s == "Return")
        {
            #region Return
            sql = string.Format(@"select StatusCode from Wh_Do where DoNo='{0}'", doNo.Text);
            string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            if (status == "CLS")
            {
                sql = string.Format("select count(*) from Wh_DoDet2 where DoNo='{0}' and DoType='OUT'", doNo.Text);
                 cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                if (cnt == 0)
                {
                    sql = string.Format(@"select * from Wh_DoDet2 where DoNo='{0}' and DoType='IN'", doNo.Text);
                    DataTable tab = ConnectSql.GetTab(sql);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        int detId = SafeValue.SafeInt(tab.Rows[i]["Id"], 0);
                        sql = @"Insert Into Wh_DoDet2(DoNo, DoType,Product,LotNo,Location,Des1,Qty1,Qty2,Qty3,Price,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Packing)";
                        sql += string.Format(@"select DoNo, 'OUT' as DoType,Product,LotNo,Location,Des1,Qty1,Qty2,Qty3,Price,Uom1,Uom2,Uom3,Uom4,QtyPackWhole,QtyWholeLoose,QtyLooseBase,'{1}',getdate(),'{1}',getdate(),Att1,Att2,Att3,Att4,Att5,Att6,Packing 
from Wh_DoDet2 where Id='{0}'", detId, EzshipHelper.GetUserName());
                        ConnectSql.ExecuteSql(sql);

                    }
                }
                sql = string.Format(@"update Wh_Do set StatusCode='RETURN' where DoNo='{0}'", doNo.Text);
                ConnectSql.ExecuteSql(sql);
                e.Result = "Return";
            }
            else
            {
                e.Result = "Fail";
            }
            #endregion
        }
        else if (s == "Costing")
        {
            #region Costing
            string result = "";
            sql = string.Format(@"select sum(Qty1) as Qty from wh_dodet2 where DoNo='{0}' and DoType='IN'", doNo.Text);
            int sumQty = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            String[] chgCode = { "BGFEE", "HANDLE", "PERMIT", "UNSTUFF20", "UNSTUFF40", "ASN", "UNIQUE", "PALLET", "TPT", "FORKLIFT", "STORE", "CUSTOMS", "MISC", "ADHOC" };
            String[] chgCodeDes = { "BG USAGE", "WAREHOUSE HANDLING", "PERMIT", "UNSTUFFING-1×20", "UNSTUFFING-1×40", "ASN", "UNIQUE CREATE", "EMPTY PALLET", "TRANSPORT-PER TRIP", "FORKLIFT", "STORAGE", "CUSTOMS SUPERVISION", "MISC CHARGES", "ADHOC CHARGES" };
            for (int i = 0; i < chgCode.Length; i++)
            {
                string code = SafeValue.SafeString(chgCode[i]);
                string des = SafeValue.SafeString(chgCodeDes[i]);
                sql = string.Format(@"select count(*) from Wh_Costing where RefNo='{0}' and ChgCode='{1}' and JobType='IN'", doNo.Text, code);
                 cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (cnt > 0)
                {
                    result += code + ",";
                }
                else
                {
                    int qty = 1;
                    decimal price = 0;
                    WhCosting cost = new WhCosting();
                    cost.ChgCode = code;
                    if (code == "HANDLE")
                    {
                        price = SafeValue.SafeDecimal(0.2);
                    }
                    if (code == "BGFEE" || code == "HANDLE")
                    {
                        qty = sumQty;
                    }
                    cost.RefNo = doNo.Text;
                    cost.JobType = "IN";
                    cost.ChgCodeDes = des;
                    cost.CostQty = qty;
                    cost.CostPrice = price;
                    cost.CostCurrency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                    cost.CostExRate = 1;
                    cost.CostGst = 0;
                    decimal amt = SafeValue.ChinaRound(sumQty * price, 2);
                    decimal gstAmt = SafeValue.ChinaRound((amt * cost.CostGst), 2);
                    decimal docAmt = amt + gstAmt;
                    decimal locAmt = SafeValue.ChinaRound(docAmt * cost.CostExRate, 2);
                    cost.CostDocAmt = docAmt;
                    cost.CostLocAmt = locAmt;
                    Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(cost);

                }
                e.Result = "Costing";
            }
            if (result.Length > 0)
            {
                e.Result = "Fail!";
            }
            #endregion
        }

    }
    private void TransferToCompany(string orderNo, string orderType)
    {
        string url=SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["WhService_ToCompany"]);
        if (url.Length == 0)
            return;
        stockApi.stock stock = new stockApi.stock();
        stock.Url = url;
        XmlDocument xmlDoc = EDI_Wh.Export_order(orderNo, orderType);
        if (xmlDoc == null)
            return;
        MemoryStream ms = new MemoryStream();
        xmlDoc.Save(ms);
        byte[] bt = ms.GetBuffer();
        stock.CreateDoIn("zhaohui", orderNo, bt);
    }
    private void UpdateDoDet2(string orderNo, string orderType)
    {
        string sql = string.Format(@"update wh_dodet2 set wh_dodet2.PartyId=mast.PartyId,wh_dodet2.DoDate=mast.DoDate from wh_do mast  where mast.DoNo=wh_dodet2.DoNo and mast.Dotype=wh_dodet2.DoType
and wh_dodet2.doNo='{0}'",orderNo);
        ConnectSql.ExecuteSql(sql);
    }
    #endregion

    #region SKU Line
    protected void grid_DoDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhDoDet));
    }
    protected void grid_DoDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxButtonEdit txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        this.dsDoDet.FilterExpression = " DoNo='" + txtDoNo.Text + "'";
    }
    protected void grid_DoDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty1"] = 0;
        e.NewValues["Qty2"] = 0;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Qty4"] = 0;
        e.NewValues["Qty5"] = 0;
        e.NewValues["QtyPackWhole"] = 1;
        e.NewValues["QtyWholeLoose"] = 1;
        e.NewValues["QtyLooseBase"] = 1;
        e.NewValues["Price"] = 0;
    }
    protected void grid_DoDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["DoNo"] = SafeValue.SafeString(txtDoNo.Text);
        if (SafeValue.SafeString(e.NewValues["ProductCode"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        e.NewValues["DoType"] = "IN";

        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["BatchNo"] = SafeValue.SafeString(e.NewValues["BatchNo"]);
        e.NewValues["CustomsLot"] = SafeValue.SafeString(e.NewValues["CustomsLot"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;
        ASPxComboBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxComboBox;
        ASPxComboBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxComboBox;
        ASPxComboBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxComboBox;
        ASPxComboBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxComboBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);
        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;
    }
    protected void grid_DoDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["BatchNo"] = SafeValue.SafeString(e.NewValues["BatchNo"]);
        e.NewValues["CustomsLot"] = SafeValue.SafeString(e.NewValues["CustomsLot"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Uom3"] = SafeValue.SafeString(e.NewValues["Uom3"]);
        e.NewValues["Uom4"] = SafeValue.SafeString(e.NewValues["Uom4"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;
        ASPxComboBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxComboBox;
        ASPxComboBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxComboBox;
        ASPxComboBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxComboBox;
        ASPxComboBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxComboBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        //e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        //e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        //e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        //e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);
        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);
        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;
    }
    protected void grid_DoDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_DoDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    //    int transId = SafeValue.SafeInt(e.NewValues["Id"], 0);
        UpdatePacking();
    }
    protected void grid_DoDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //int transId = SafeValue.SafeInt(e.NewValues["Id"], 0);
        UpdatePacking();
    }
    protected void grid_DoDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        //int transId = SafeValue.SafeInt(e.Values["DoInId"], 0);
        //UpdatePoDetBalQty(transId);
    }
    protected void grid_DoDet_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    protected void grid_DoDet_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        //ASPxGridView grid=sender as ASPxGridView;
        //if(grid.EditingRowVisibleIndex>-1){

        //}
    }

    private void UpdatePacking()
    {
        ASPxButtonEdit txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        string sql = string.Format(@"update Wh_DoDet set Packing=('1'+Uom1
	+case when len(uom2)>0 then 'x'+convert(nvarchar(10),QtyPackWhole)+Uom2 else '' end
	+case when len(uom3)>0 then 'x'+convert(nvarchar(10),QtyWholeLoose)+Uom3 else '' end
	+case when len(uom4)>0 then 'x'+convert(nvarchar(10),QtyLooseBase)+Uom4 else '' end) where DoNo='{0}'", txtDoNo.Text);
        C2.Manager.ORManager.ExecuteScalar(sql);
        sql = string.Format(@"update Wh_DoDet2 set Packing=('1'+Uom1
	+case when len(uom2)>0 then 'x'+convert(nvarchar(10),QtyPackWhole)+Uom2 else '' end
	+case when len(uom3)>0 then 'x'+convert(nvarchar(10),QtyWholeLoose)+Uom3 else '' end
	+case when len(uom4)>0 then 'x'+convert(nvarchar(10),QtyLooseBase)+Uom4 else '' end) where DoNo='{0}'", txtDoNo.Text);
        C2.Manager.ORManager.ExecuteScalar(sql);
        //string sql = string.Format(@"update Wh_TransDet set BalQty=Qty-(select sum(Qty2) from Wh_DoDet where DoInId={0}) where Id='{0}'", transId);
        //C2.Manager.ORManager.ExecuteScalar(sql);
    }

    #endregion
    #region PutAway
    protected void grid_DoDet2_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhDoDet2));
    }
    protected void grid_DoDet2_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxButtonEdit txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        this.dsDoDet2.FilterExpression = " DoNo='" + txtDoNo.Text + "'";
    }
    protected void grid_DoDet2_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxButtonEdit txt_RefNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["Qty"] = 1;
        e.NewValues["DoNo"] = SafeValue.SafeString(txt_RefNo.Text);
        e.NewValues["DoType"] = "IN";
        e.NewValues["Product"] = "";
        e.NewValues["Location"] = "";
        e.NewValues["LotNo"] = "";
        e.NewValues["Des1"] = "";
        e.NewValues["PkgType"] = 0;
        e.NewValues["DoDate"] = DateTime.Today;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["PkgType"] = " ";

    }
    protected void grid_DoDet2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit txt_RefNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        string doNo = SafeValue.SafeString(txt_RefNo.Text);
        e.NewValues["DoNo"] = doNo;
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        e.NewValues["DoType"] = "IN";
        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Today);
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["ProcessStatus"] = SafeValue.SafeString(e.NewValues["ProcessStatus"]);
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["BatchNo"] = SafeValue.SafeString(e.NewValues["BatchNo"]);
        e.NewValues["CustomsLot"] = SafeValue.SafeString(e.NewValues["CustomsLot"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;
        ASPxComboBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxComboBox;
        ASPxComboBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxComboBox;
        ASPxComboBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxComboBox;
        ASPxComboBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxComboBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        //e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        //e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        //e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        //e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);

    }
    protected void grid_DoDet2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);

        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Today);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["ProcessStatus"] = SafeValue.SafeString(e.NewValues["ProcessStatus"]);
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["BatchNo"] = SafeValue.SafeString(e.NewValues["BatchNo"]);
        e.NewValues["CustomsLot"] = SafeValue.SafeString(e.NewValues["CustomsLot"]);
        e.NewValues["Att1"] = SafeValue.SafeString(e.NewValues["Att1"]);
        e.NewValues["Att2"] = SafeValue.SafeString(e.NewValues["Att2"]);
        e.NewValues["Att3"] = SafeValue.SafeString(e.NewValues["Att3"]);
        e.NewValues["Att4"] = SafeValue.SafeString(e.NewValues["Att4"]);
        e.NewValues["Att5"] = SafeValue.SafeString(e.NewValues["Att5"]);
        e.NewValues["Att6"] = SafeValue.SafeString(e.NewValues["Att6"]);
        e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["DoType"] = "IN";
    }
    protected void grid_DoDet2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_DoDet2_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //UpdatePacking();
        //        string sku = SafeValue.SafeString(e.NewValues["Product"]);
        //        string lotNo = SafeValue.SafeString(e.NewValues["LotNo"]);
        //        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        //        string sql = string.Format(@"select wh_DoDet2.Qty1+wh_DoDet2.Qty2+wh_DoDet2.Qty3-(wh_DoDet.Qty1+wh_DoDet.Qty2+wh_DoDet.Qty3) from Wh_DoDet2 left join
        //wh_DoDet on Wh_doDet.DoNo=Wh_DoDet2.DoNo  and Wh_doDet.DoType=Wh_DoDet2.DoType and Wh_doDet.ProductCode=Wh_DoDet2.Product and  Wh_doDet.LotNo=Wh_DoDet2.LotNO
        //where wh_DoDet2.DoNo='{0}' and Wh_DoDet2.DoType='In' and Wh_DoDet2.Product='{1}' and Wh_DoDet2.LotNO='{2}'", refN.Text, sku,lotNo);
        //        if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) != 0)
        //        {
        //            throw new Exception("Issued and Picked is not tally");
        //        }
    }
    protected void grid_DoDet2_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //UpdatePacking();
        //        string sku = SafeValue.SafeString(e.NewValues["Product"]);
        //        string lotNo = SafeValue.SafeString(e.NewValues["LotNo"]);
        //        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        //        string sql = string.Format(@"select wh_DoDet2.Qty1+wh_DoDet2.Qty2+wh_DoDet2.Qty3-(wh_DoDet.Qty1+wh_DoDet.Qty2+wh_DoDet.Qty3) from Wh_DoDet2 left join
        //wh_DoDet on Wh_doDet.DoNo=Wh_DoDet2.DoNo  and Wh_doDet.DoType=Wh_DoDet2.DoType and Wh_doDet.ProductCode=Wh_DoDet2.Product and  Wh_doDet.LotNo=Wh_DoDet2.LotNO
        //where wh_DoDet2.DoNo='{0}' and Wh_DoDet2.DoType='In' and Wh_DoDet2.Product='{1}' and Wh_DoDet2.LotNO='{2}'", refN.Text, sku,lotNo);
        //        if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) != 0)
        //        {
        //            throw new Exception("Issued and Picked is not tally");
        //        }
    }

    #endregion

    #region Container
    protected void grid_DoDet3_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhDoDet3));
    }
    protected void grid_DoDet3_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxButtonEdit txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        this.dsDoDet3.FilterExpression = " DoNo='" + txtDoNo.Text + "'";
    }
    protected void grid_DoDet3_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxButtonEdit txt_RefNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["Qty"] = 1;
        e.NewValues["DoNo"] = SafeValue.SafeString(txt_RefNo.Text);
        e.NewValues["DoType"] = "IN";
        e.NewValues["ContainerNo"] = "";
        e.NewValues["SealNo"] = "";
        e.NewValues["ContainerType"] = "";
        e.NewValues["Weight"] = 0;
        e.NewValues["M3"] = 0;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["PkgType"] = " ";

    }
    protected void grid_DoDet3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit txt_RefNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        string doNo = SafeValue.SafeString(txt_RefNo.Text);

        e.NewValues["DoNo"] = doNo;
        e.NewValues["DoType"] = "IN";

        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"], 0);
        e.NewValues["M3"] = SafeValue.SafeDecimal(e.NewValues["M3"], 0);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);

        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"]);
        e.NewValues["SealNo"] = SafeValue.SafeString(e.NewValues["SealNo"]);
        e.NewValues["PkgType"] = SafeValue.SafeString(e.NewValues["PkgType"]);
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_DoDet3_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"], 0);
        e.NewValues["M3"] = SafeValue.SafeDecimal(e.NewValues["M3"], 0);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"]);
        e.NewValues["SealNo"] = SafeValue.SafeString(e.NewValues["SealNo"]);
        e.NewValues["PkgType"] = SafeValue.SafeString(e.NewValues["PkgType"]);
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_DoDet3_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
            e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_DoDet3_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_DoDet3_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
    }
    protected void grid_DoDet3_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    protected void grid_DoDet3_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid.EditingRowVisibleIndex > -1)
        {
        }
    }
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] keyValues = new object[grid.VisibleRowCount];
        object[] contNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            keyValues[i] = grid.GetRowValues(i, "Id");
            contNs[i] = grid.GetRowValues(i, "ContainerNo");
            contTypes[i] = grid.GetRowValues(i, "ContainerType");
        }
        e.Properties["cpContType"] = contTypes;
        e.Properties["cpContN"] = contNs;
        e.Properties["cpKeyValues"] = keyValues;
    }
    #endregion


    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select DoNo from Wh_DO where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsInvoice.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select DoNo from Wh_DO where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsVoucher.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
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
        ASPxButtonEdit txtRefNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
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
    
    #region log
    protected void grid_Log_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxButtonEdit refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        this.dsLog.FilterExpression = String.Format("RefNo='{0}' and RefType='In'", refN.Text);//
    }
    #endregion



    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select DoNo from wh_do where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "JobType='IN' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhCosting));
        }
    }
    protected void grid_Cost_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CostQty"] = 1;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["CostLocAmt"] = 0;
        e.NewValues["CostGst"] = 0;
        e.NewValues["ChgCode"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["CostCurrency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["CostExRate"] = 1;
    }
    protected void grid_Cost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit doNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobType"] = "IN";
        e.NewValues["RefNo"] = doNo.Text;
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["CostGstType"] = SafeValue.SafeString(e.NewValues["CostGstType"]);
        if (e.NewValues["CostGstType"]=="S")
        {
            e.NewValues["CostGst"] =SafeValue.SafeDecimal(0.7);
        }
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_Cost_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["CostGstType"] = SafeValue.SafeString(e.NewValues["CostGstType"]);
        if (e.NewValues["CostGstType"] == "S")
        {
            e.NewValues["CostGst"] = SafeValue.SafeDecimal(0.7);
        }
        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
    }
    protected void grid_Cost_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
    }
    protected void grid_Cost_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "Import")
        {
            ASPxButtonEdit doNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
            ASPxComboBox impExp = this.grd_Do.FindEditFormTemplateControl("cmb_Priority") as ASPxComboBox;
            ASPxComboBox fclLcl = this.grd_Do.FindEditFormTemplateControl("cmb_ModelType") as ASPxComboBox;
            string where = "";
            if (impExp.Text.ToLower() == "import" && fclLcl.Text.ToLower() == "fcl")
            {
                where = "ImpExpInd='WHI' and FclLclInd='Fcl'";
            }
            else if (impExp.Text.ToLower() == "import" && fclLcl.Text.ToLower() == "lcl")
            {
                where = "ImpExpInd='WHI' and FclLclInd='Lcl'";
            }
            else if (impExp.Text.ToLower() == "export" && fclLcl.Text.ToLower() == "fcl")
            {
                where = "ImpExpInd='WHE' and FclLclInd='Fcl'";
            }
            else if (impExp.Text.ToLower() == "export" && fclLcl.Text.ToLower() == "lcl")
            {
                where = "ImpExpInd='WHE' and FclLclInd='Lcl'";
            }
            if (where.Length > 0)
            {
                string sql = @"insert into wh_costing(RefNo,JobType,ChgCode,ChgCodeDes,remark,CostQty,CostPrice,CostCurrency,CostExRate,Unit,CostGst)"; 
                sql+=string.Format(@"select '{0}','IN',ChgCode,ChgDes,'SKU:'+productcode+';LotNo:'+LotNo
,Case when Qty1*price>=Amt then Qty1 else 1 end as Qty
,Case when Qty1*price>=Amt then Price else Amt end as Price
,Currency,ExRate,Unit,Gst from 
(select productcode,lotno,qty1 from wh_dodet where dono='{0}' and DoType='IN' ) as tab1
cross  join( select ChgCode,chgdes,Currency,exrate,Price,qty,amt,Unit,gsttype,gst from SeaQuoteDet1 where {1}) as tab2
", doNo.Text, where);
                ConnectSql.ExecuteSql(sql);
                sql = string.Format("Update wh_costing set CostDocAmt=convert(decimal(10,2),CostQty*CostPrice)+convert(decimal(10,2),CostQty*CostPrice)*Costgst where RefNo='{0}' and JobType='IN'", doNo.Text);
                ConnectSql.ExecuteSql(sql);
                sql = string.Format("Update wh_costing set CostLocAmt=CostDocAmt*CostExRate where RefNo='{0}' and JobType='IN'", doNo.Text);
                ConnectSql.ExecuteSql(sql);
                e.Result = "Success";
            }
        }
    }
    #endregion

    #region Packing
    protected void Grid_Packing_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select DoNo from wh_do where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsWhPacking.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void Grid_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxButtonEdit refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobNo"] = "";
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["StatusCode"] = "USE";
        //string jobType = EzshipHelper.GetJobType("SI", refN.Text);
        //if (jobType == "FCL")
        //    e.NewValues["MkgType"] = "Cont";
        //else
        //    e.NewValues["MkgType"] = "BL";
    }
    protected void Grid_Packing_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobNo"] = "";
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["StatusCode"] = "USE";
        //string jobType ="OUT";
        //if (jobType == "FCL")
        //    e.NewValues["MkgType"] = "Cont";
        //else
        //    e.NewValues["MkgType"] = "BL";
    }
    protected void Grid_Packing_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void Grid_Packing_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Volume"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["PackageType"] = " ";
        e.NewValues["MkgType"] = "Cont";
        e.NewValues["ContainerType"] = " ";
        e.NewValues["RefStatusCode"] = "USE";
        e.NewValues["StatusCode"] = "USE";
        ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;

    }
    protected void Grid_Packing_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhPacking));
        }
    }
    #endregion
}