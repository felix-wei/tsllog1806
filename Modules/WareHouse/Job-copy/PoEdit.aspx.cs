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

public partial class WareHouse_Job_PoEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["PoWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["PoWhere"] = "DoNo='" + Request.QueryString["no"].ToString() + "' ";
                this.txt_DoNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["PoWhere"] == null)
                {
                    this.grd_Do.AddNewRow();
                }
            }
            else
                this.dsDo.FilterExpression = "1=0";
        }
        if (Session["PoWhere"] != null)
        {
            this.dsDo.FilterExpression = Session["PoWhere"].ToString();
            if (this.grd_Do.GetRow(0) != null)
                this.grd_Do.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region Po
    protected void grd_Do_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhTrans));
    }
    protected void grd_Do_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DoNo"] = "NEW";
        e.NewValues["DoType"] ="PO";
        e.NewValues["DoDate"] = DateTime.Now;
        e.NewValues["DoStatus"] = "Draft";
        e.NewValues["ExpectedDate"] = DateTime.Today.AddDays(14);
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["WareHouseId"] = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
        e.NewValues["ExRate"] = 1.000000;
    }
    protected void grd_Do_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grd_Do.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            //ASPxTextBox partyName = grd_Do.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox txt_DoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            //partyName.Text = EzshipHelper.GetPartyName(this.grd_Do.GetRowValues(this.grd_Do.EditingRowVisibleIndex, new string[] { "PartyId" }));
            string oid = SafeValue.SafeString(this.grd_Do.GetRowValues(this.grd_Do.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                ASPxDateEdit txt_Date = this.grd_Do.FindEditFormTemplateControl("txt_Date") as ASPxDateEdit;
                txt_Date.BackColor = ((DevExpress.Web.ASPxEditors.ASPxTextBox)(this.grd_Do.FindEditFormTemplateControl("txt_DoNo"))).BackColor;
                txt_Date.ReadOnly = true;
                string sql = string.Format("select DoStatus from wh_Trans where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grd_Do.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                //ASPxButton btn_Void = this.grd_Do.FindEditFormTemplateControl("btn_Void") as ASPxButton;
                if (closeInd == "Closed")
                {
                    btn.Text = "Open Job";
                }
                //if (closeInd == "CNL")
                // {
                //     btn_Void.Text = "Unvoid";
                // }
            }

        }
    }
    protected void grd_Do_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //string s = e.Parameters;
        //if (s == "Save")
        //{
        //    SavePo();

        //}
    }
    private string SavePo()
    {
        try
        {
            ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;         
            ASPxDateEdit txt_Date = this.grd_Do.FindEditFormTemplateControl("txt_Date") as ASPxDateEdit;
            ASPxTextBox txt_Id = grd_Do.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string pId = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "Id='" + pId + "'");
            WhTrans whTrans =  C2.Manager.ORManager.GetObject(query) as  WhTrans;
            bool action = false;
            string poNo = "";
            if (whTrans == null)
            {
                action = true;
                whTrans = new  WhTrans();
                poNo = C2Setup.GetNextNo("", "PurchaseOrders", txt_Date.Date);
            }
            ASPxComboBox cmb_Status = this.grd_Do.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            whTrans.DoStatus = SafeValue.SafeString(cmb_Status.Value);
            ASPxButtonEdit txt_PartyId = grd_Do.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
            whTrans.PartyId = SafeValue.SafeString(txt_PartyId.Text);
            whTrans.DoDate = SafeValue.SafeDate(txt_Date.Date, DateTime.Today);
            ASPxDateEdit txt_ExpectedDate = this.grd_Do.FindEditFormTemplateControl("txt_ExpectedDate") as ASPxDateEdit;
            whTrans.ExpectedDate = txt_ExpectedDate.Date;


            ASPxTextBox txt_PartyName = grd_Do.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
            whTrans.PartyName = txt_PartyName.Text;
            ASPxMemo txt_PartyAdd = grd_Do.FindEditFormTemplateControl("txt_PartyAdd") as ASPxMemo;
            whTrans.PartyAdd = txt_PartyAdd.Text;
            //ASPxTextBox txt_PostalCode = grd_Do.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            //whTrans.PartyPostalcode = txt_PostalCode.Text;
            //ASPxButtonEdit txt_PartyCity = grd_Do.FindEditFormTemplateControl("txt_PartyCity") as ASPxButtonEdit;
            //whTrans.PartyCity = txt_PartyCity.Text;
            //ASPxButtonEdit txt_PartyCountry = grd_Do.FindEditFormTemplateControl("txt_PartyCountry") as ASPxButtonEdit;
            //whTrans.PartyCountry = txt_PartyCountry.Text;
            ASPxMemo txt_Remark1 = grd_Do.FindEditFormTemplateControl("txt_Remark1") as ASPxMemo;
            whTrans.Remark1 = txt_Remark1.Text;
            ASPxMemo txt_Remark2 = grd_Do.FindEditFormTemplateControl("txt_Remark2") as ASPxMemo;
            whTrans.Remark2 = txt_Remark2.Text;
            ASPxMemo txt_CollectFrom = grd_Do.FindEditFormTemplateControl("txt_CollectFrom") as ASPxMemo;
            whTrans.CollectFrom = txt_CollectFrom.Text;
            ASPxMemo txt_DeliveryTo = grd_Do.FindEditFormTemplateControl("txt_DeliveryTo") as ASPxMemo;
            whTrans.DeliveryTo = txt_DeliveryTo.Text;
            ASPxButtonEdit txt_WareHouseId = grd_Do.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            whTrans.WareHouseId = txt_WareHouseId.Text;

            ASPxTextBox pic = grd_Do.FindEditFormTemplateControl("txt_Pic") as ASPxTextBox;
            whTrans.Pic = pic.Text;
            ASPxButtonEdit currency = grd_Do.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
            whTrans.Currency = currency.Text;
            ASPxSpinEdit exRate = grd_Do.FindEditFormTemplateControl("spin_ExRate") as ASPxSpinEdit;
            whTrans.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            ASPxComboBox payTerm = grd_Do.FindEditFormTemplateControl("cmb_PayTerm") as ASPxComboBox;
            whTrans.PayTerm = payTerm.Text;
            ASPxComboBox incoTerm = grd_Do.FindEditFormTemplateControl("cmb_IncoTerm") as ASPxComboBox;
            whTrans.IncoTerm = incoTerm.Text;
            ASPxTextBox txt_MasterDocNo = grd_Do.FindEditFormTemplateControl("txt_MasterDocNo") as ASPxTextBox;
            whTrans.MasterDocNo = txt_MasterDocNo.Text;

            //shipment info
            ASPxTextBox txt_Vessel = pageControl.FindControl("txt_Vessel") as ASPxTextBox;
            whTrans.Vessel = txt_Vessel.Text;
            ASPxTextBox txt_Voyage = pageControl.FindControl("txt_Voyage") as ASPxTextBox;
            whTrans.Voyage = txt_Voyage.Text;
            ASPxDateEdit date_Etd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            whTrans.Etd = date_Etd.Date;
            ASPxButtonEdit txt_Pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
            whTrans.Pol = txt_Pol.Text;
            ASPxButtonEdit txt_Pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
            whTrans.Pod = txt_Pod.Text;
            ASPxDateEdit date_Eta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
            whTrans.Eta = date_Eta.Date;
            ASPxTextBox txt_OceanBl = pageControl.FindControl("txt_OceanBl") as ASPxTextBox;
            whTrans.Obl = txt_OceanBl.Text;
            ASPxTextBox txt_HouseBl = pageControl.FindControl("txt_HouseBl") as ASPxTextBox;
            whTrans.Hbl = txt_HouseBl.Text;
            ASPxDateEdit date_EtaDest = pageControl.FindControl("date_EtaDest") as ASPxDateEdit;
            whTrans.EtaDest = date_EtaDest.Date;
            ASPxTextBox txt_VehicleNo = pageControl.FindControl("txt_VehicleNo") as ASPxTextBox;
            whTrans.Vehicle = txt_VehicleNo.Text;
            ASPxTextBox txt_COO = pageControl.FindControl("txt_COO") as ASPxTextBox;
            whTrans.Coo = txt_COO.Text;
            ASPxButtonEdit txt_Carrier = pageControl.FindControl("txt_Carrier") as ASPxButtonEdit;
            whTrans.Carrier = txt_Carrier.Text;
            //party
            ASPxButtonEdit txt_AgentId = pageControl.FindControl("txt_AgentId") as ASPxButtonEdit;
            whTrans.AgentId = txt_AgentId.Text;
            ASPxTextBox txt_AgentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            whTrans.AgentName = txt_AgentName.Text;
            ASPxTextBox txt_AgentZip = pageControl.FindControl("txt_AgentZip") as ASPxTextBox;
            whTrans.AgentZip = txt_AgentZip.Text;
            ASPxMemo txt_AgentAdd = pageControl.FindControl("txt_AgentAdd") as ASPxMemo;
            whTrans.AgentAdd = txt_AgentAdd.Text;
            ASPxTextBox txt_AgentTel = pageControl.FindControl("txt_AgentTel") as ASPxTextBox;
            whTrans.AgentTel = txt_AgentTel.Text;
            ASPxTextBox txt_AgentContact = pageControl.FindControl("txt_AgentContact") as ASPxTextBox;
            whTrans.AgentContact = txt_AgentContact.Text;
            ASPxButtonEdit txt_AgentCountry = pageControl.FindControl("txt_AgentCountry") as ASPxButtonEdit;
            whTrans.AgentCountry = txt_AgentCountry.Text;
            ASPxButtonEdit txt_AgentCity = pageControl.FindControl("txt_AgentCity") as ASPxButtonEdit;
            whTrans.AgentCity = txt_AgentCity.Text;
            ASPxButtonEdit txt_NotifyId = pageControl.FindControl("txt_NotifyId") as ASPxButtonEdit;
            whTrans.NotifyId = txt_NotifyId.Text;
            ASPxTextBox txt_NotifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            whTrans.NotifyName = txt_NotifyName.Text;
            ASPxTextBox txt_NotifyZip = pageControl.FindControl("txt_NotifyZip") as ASPxTextBox;
            whTrans.NotifyZip = txt_NotifyZip.Text;
            ASPxMemo txt_NotifyAdd = pageControl.FindControl("txt_NotifyAdd") as ASPxMemo;
            whTrans.NotifyAdd = txt_NotifyAdd.Text;
            ASPxTextBox txt_NotifyTel = pageControl.FindControl("txt_NotifyTel") as ASPxTextBox;
            whTrans.NotifyTel = txt_NotifyTel.Text;
            ASPxTextBox txt_NotifyContact = pageControl.FindControl("txt_NotifyContact") as ASPxTextBox;
            whTrans.NotifyContact = txt_NotifyContact.Text;
            ASPxButtonEdit txt_NotifyCountry = pageControl.FindControl("txt_NotifyCountry") as ASPxButtonEdit;
            whTrans.NotifyCountry = txt_NotifyCountry.Text;
            ASPxButtonEdit txt_NotifyCity = pageControl.FindControl("txt_NotifyCity") as ASPxButtonEdit;
            whTrans.NotifyCity = txt_NotifyCity.Text;
            ASPxButtonEdit txt_ConsigneeId = pageControl.FindControl("txt_ConsigneeId") as ASPxButtonEdit;
            whTrans.ConsigneeId = txt_ConsigneeId.Text;
            ASPxTextBox txt_ConsigneeName = pageControl.FindControl("txt_ConsigneeName") as ASPxTextBox;
            whTrans.ConsigneeName = txt_ConsigneeName.Text;
            ASPxTextBox txt_ConsigneeZip = pageControl.FindControl("txt_ConsigneeZip") as ASPxTextBox;
            whTrans.ConsigneeZip = txt_ConsigneeZip.Text;
            ASPxMemo txt_ConsigneeAdd = pageControl.FindControl("txt_ConsigneeAdd") as ASPxMemo;
            whTrans.ConsigneeAdd = txt_ConsigneeAdd.Text;
            ASPxTextBox txt_ConsigneeTel = pageControl.FindControl("txt_ConsigneeTel") as ASPxTextBox;
            whTrans.ConsigneeTel = txt_ConsigneeTel.Text;
            ASPxTextBox txt_ConsigneeContact = pageControl.FindControl("txt_ConsigneeContact") as ASPxTextBox;
            whTrans.ConsigneeContact = txt_ConsigneeContact.Text;
            ASPxButtonEdit txt_ConsigneeCountry = pageControl.FindControl("txt_ConsigneeCountry") as ASPxButtonEdit;
            whTrans.ConsigneeCountry = txt_ConsigneeCountry.Text;
            ASPxButtonEdit txt_ConsigneeCity = pageControl.FindControl("txt_ConsigneeCity") as ASPxButtonEdit;
            whTrans.ConsigneeCity = txt_ConsigneeCity.Text;

            if (action)
            {
                whTrans.DoNo = poNo;
                whTrans.DoType = "PO";
                whTrans.CreateBy = EzshipHelper.GetUserName();
                whTrans.CreateDateTime = DateTime.Now;

                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whTrans);
                C2Setup.SetNextNo("", "PurchaseOrders", poNo, txt_Date.Date);
            }
            else
            {
                whTrans.UpdateBy = EzshipHelper.GetUserName();
                whTrans.UpdateDateTime = DateTime.Now;
                bool isAddLog = false;
                if (whTrans.DoStatus == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select DoStatus from wh_trans where DoNo='" + whTrans.DoNo + "'")))
                {
                }
                else
                {
                    isAddLog = true;
                }
                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whTrans);
                if (isAddLog)
                    EzshipLog.Log(whTrans.DoNo, "", "PO", whTrans.DoStatus);
            }
            Session["PoWhere"] = "DoNo='" + whTrans.DoNo + "'";
            this.dsDo.FilterExpression = Session["PoWhere"].ToString();
            if (this.grd_Do.GetRow(0) != null)
                this.grd_Do.StartEdit(0);
            return whTrans.DoNo;
        }
        catch { }
        return "";
    }
    private string SaveDoIn(string poNo)
    {
        string doNo = "";
        try
        {
            ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxDateEdit txt_Date = this.grd_Do.FindEditFormTemplateControl("txt_Date") as ASPxDateEdit;
            //string pId = "";
            //Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + pId + "'");
            WhDo whDo = null;// C2.Manager.ORManager.GetObject(query) as WhDo;
            bool action = false;
            if (whDo == null)
            {
                action = true;
                whDo = new WhDo();
                doNo = C2Setup.GetNextNo("", "DOIN", DateTime.Today);
            }
            
            whDo.DoDate = DateTime.Today;
            ASPxDateEdit txt_ExpectedDate = this.grd_Do.FindEditFormTemplateControl("txt_ExpectedDate") as ASPxDateEdit;
            whDo.ExpectedDate = txt_ExpectedDate.Date;
            whDo.Priority = "IMPORT";
            ASPxButtonEdit txt_PartyId = grd_Do.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
            whDo.PartyId = SafeValue.SafeString(txt_PartyId.Text);
            ASPxTextBox txt_PartyName = grd_Do.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
            whDo.PartyName = txt_PartyName.Text;
            ASPxMemo txt_PartyAdd = grd_Do.FindEditFormTemplateControl("txt_PartyAdd") as ASPxMemo;
            whDo.PartyAdd = txt_PartyAdd.Text;
            ASPxTextBox txt_PostalCode = grd_Do.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            whDo.PartyPostalcode = txt_PostalCode.Text;
            ASPxButtonEdit txt_PartyCity = grd_Do.FindEditFormTemplateControl("txt_PartyCity") as ASPxButtonEdit;
            whDo.PartyCity = txt_PartyCity.Text;
            ASPxButtonEdit txt_PartyCountry = grd_Do.FindEditFormTemplateControl("txt_PartyCountry") as ASPxButtonEdit;
            whDo.PartyCountry = txt_PartyCountry.Text;
            //shipment
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
            whDo.Coo = txt_COO.Text;
            ASPxButtonEdit txt_Carrier = pageControl.FindControl("txt_Carrier") as ASPxButtonEdit;
            whDo.Carrier = txt_Carrier.Text;
            //party
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

            ASPxMemo txt_Remark1 = grd_Do.FindEditFormTemplateControl("txt_Remark1") as ASPxMemo;
            whDo.Remark1 = txt_Remark1.Text;
            ASPxMemo txt_Remark2 = grd_Do.FindEditFormTemplateControl("txt_Remark2") as ASPxMemo;
            whDo.Remark2 = txt_Remark2.Text;
            ASPxMemo txt_CollectFrom = grd_Do.FindEditFormTemplateControl("txt_CollectFrom") as ASPxMemo;
            whDo.CollectFrom = txt_CollectFrom.Text;
            ASPxMemo txt_DeliveryTo = grd_Do.FindEditFormTemplateControl("txt_DeliveryTo") as ASPxMemo;
            whDo.DeliveryTo = txt_DeliveryTo.Text;
            ASPxButtonEdit txt_WareHouseId = grd_Do.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            whDo.WareHouseId = txt_WareHouseId.Text;

            if (action)
            {
                whDo.DoNo = doNo;
                whDo.DoType = "IN";
                whDo.StatusCode = "USE";
                whDo.CreateBy = EzshipHelper.GetUserName();
                whDo.CreateDateTime = DateTime.Today;
                whDo.PoNo = poNo;
                whDo.PoDate = txt_Date.Date;
                Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whDo);
                C2Setup.SetNextNo("", "DOIN", doNo, DateTime.Today);
            }
            else
            {
                whDo.UpdateBy = EzshipHelper.GetUserName();
                whDo.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whDo);
            }
        }
        catch { }
        return doNo;
    }
    protected void grd_Do_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters; 
        ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox id = grd_Do.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_DoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        ASPxButtonEdit txt_WareHouseId = grd_Do.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        ASPxButtonEdit txt_PartyId = grd_Do.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
        if (s == "Save")
        {
            #region Save
            if (txt_PartyId.Text.Trim() == "")
            {
                e.Result = "Fail";
                return;
            }
            ASPxComboBox doStatus = grd_Do.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            if (doStatus.Text == "Confirmed")
            {
                //check purchase price and sell price
                string sql1 = string.Format(@"select count(id) from wh_transdet 
 where dono='{0}' and DoType='po'", txt_DoNo.Text);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql1), 0) == 0)
                {
                    e.Result = "NO SKU";
                    return;
                }
                sql1 = string.Format(@"select (case when Price=0 then 0 else 1 end) Result from Wh_TransDet where dono='{0}' and DoType='po'", txt_DoNo.Text);
                DataTable tab = ConnectSql.GetTab(sql1);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (SafeValue.SafeInt(tab.Rows[i]["Result"], 0) == 0)
                    {
                        e.Result = "No Price";
                        return;
                    }
                }
            }
            if (txt_DoNo.Text.Length > 4)
            {
                SavePo();
                e.Result = "";//update old one
            }
            else
                e.Result = SavePo();// new one
            return;
            #endregion
        }
        //string sql = "select Count(*) from Wh_TransDet where DoNo='" + SafeValue.SafeString(txt_DoNo.Text) + "' and (JobStatus='Draft' or JobStatus='Waiting')";
       // int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
       //if (cnt == 0)
       // {
        if (s == "CloseJob")
        {
            #region close job

           string sql = "select DoStatus from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "Closed")
            {
                sql = string.Format("update Wh_Trans set DoStatus='Draft' where DoNo='{0}'", txt_DoNo.Text);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(txt_DoNo.Text, "", "PO", "Open Job");
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {

                sql = string.Format("update Wh_Trans set DoStatus='Closed' where DoNo='{0}'", txt_DoNo.Text);

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(txt_DoNo.Text, "", "PO", "Closed Job");
                    e.Result = "Success";
                }
                else
                    e.Result = "Fail";

            }
            #endregion
        }
        //}
        //else
        //{
        //    e.Result = "NotClose";
        //}
        if (s == "Void")
        {
            #region void master
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='WH' and MastRefNo='{0}'", txt_DoNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
           string  sql = "select StatusCode from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update Wh_Trans set StatusCode='USE' where DoNo='{0}'", txt_DoNo.Text);
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
                sql = string.Format("update Wh_Trans set StatusCode='CNL' where DoNo='{0}'", txt_DoNo.Text);

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    e.Result = "Success";
                else
                    e.Result = "Fail";

            }
            #endregion
        }
        if (s == "AddDoIn")
        {
            #region Transfer to Do In
            string sql = string.Format(@" select count(id) from Wh_TransDet where DoNo='{0}' and DoType='PO'", txt_DoNo.Text);
            DataTable tab = ConnectSql.GetTab(sql);
           int cnt = 0;
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                cnt += SafeValue.SafeInt(tab.Rows[i][0],0);
            }
            if (cnt > 0)
            {
                string poNo = SafeValue.SafeString(txt_DoNo.Text);
                string wareHouse = SafeValue.SafeString(txt_WareHouseId.Text);
                string doNo = SaveDoIn(poNo);
                sql = @"Insert Into wh_DoDet(DoNo, DoType,ProductCode,ExpiredDate,Price,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
                sql += string.Format(@" select * from (select '{1}'as DoNo, 'In' as DoType,ProductCode,ExpiredDate,Price
,0 as Qty1,0 as Qty2,0 as Qty3
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where DoType='In' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty4
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where DoType='In' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty5
,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,'ADMIN' as CreateBy,getdate() as CreateDateTime,'ADMIN' as UpdateBy,getdate() as UpdateDateTime,Id as DoInId from Wh_TransDet where DoNo='{0}' and DoType='PO'
 ) as tab_aa where qty4>0", txt_DoNo.Text, doNo, EzshipHelper.GetUserName());

                cnt = C2.Manager.ORManager.ExecuteCommand(sql);
                if (cnt > -1)
                {
                    e.Result = doNo;
                    //transfe doin to warehouse
                    //if (wareHouse == "SBL")
                    //{
                     //   sql =string.Format(@"select StockType from ref_warehouse where Code='{0}'",wareHouse);
                     //   string wh=SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
                      //  if (wh == "EDI")
                      //  {
                            TransferToStock(doNo, "IN");
                       // }
                    //}
                }
                else
                    e.Result = "Fail";
            }
            else
                e.Result = "NO SKU Line";
            #endregion
        }
        else if (s == "Confirm")
        {
            #region Confirm
            if (txt_PartyId.Text.Trim() == "")
            {
                e.Result = "Fail";
                return;
            }
            string sql = "select StatusCode from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "Draft");// 
            if (closeInd == "Draft")
            {
                bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refN.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update Wh_Trans set DoStatus='Confirmed',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(txt_DoNo.Text, "", "PO", "Confirmed");
                        e.Result = "Success";
                    }
                    else
                        e.Result = "Fail";
                }
                else
                    e.Result = "NoMatch";
            }
            else
            {
               

            }
            #endregion
        }
    }
    private void TransferToStock(string orderNo, string orderType)
    {
        string url = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["WhService_ToStock"]);
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
        stock.CreateDoIn("zhaohui",orderNo, bt);
    }
    #endregion

    #region SKU Line
    protected void grid_DoDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhTransDet));
    }
    protected void grid_DoDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsDoDet.FilterExpression = " DoNo='" + txtDoNo.Text + "'";
    }
    protected void grid_DoDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty"] = 0;
        e.NewValues["Qty1"] = 1;
        e.NewValues["Qty2"] = 0;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Qty4"] = 0;
        e.NewValues["Qty5"] = 0;
        e.NewValues["QtyPackWhole"] = 1;
        e.NewValues["QtyWholeLoose"] = 1;
        e.NewValues["QtyLooseBase"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["Gst"] = 0;
        e.NewValues["ExRate"] = 1;
    }
    protected void grid_DoDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox txtDoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["DoNo"] = SafeValue.SafeString(txtDoNo.Text);
        if (SafeValue.SafeString(e.NewValues["ProductCode"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        e.NewValues["DoType"] = "PO";

        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
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

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["Currency"] = SafeValue.SafeString(e.NewValues["Currency"]);
        e.NewValues["ExRate"] = SafeValue.SafeDecimal(e.NewValues["ExRate"], 0);
        e.NewValues["Gst"] = SafeValue.SafeDecimal(e.NewValues["Gst"], 0);
        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);

        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;
        e.NewValues["DocAmt"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty1"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0));
    }
    protected void grid_DoDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
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

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["Currency"] = SafeValue.SafeString(e.NewValues["Currency"]);
        e.NewValues["ExRate"] = SafeValue.SafeDecimal(e.NewValues["ExRate"], 0);
        e.NewValues["Gst"] = SafeValue.SafeDecimal(e.NewValues["Gst"], 0);

        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);
        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;
        e.NewValues["DocAmt"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty1"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0));
    }
    protected void grid_DoDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_DoDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //int id = SafeValue.SafeInt(e.NewValues["Id"], 0);
        //UpdateQty(id);
    }
    protected void grid_DoDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //int id = SafeValue.SafeInt(e.NewValues["Id"], 0);
        //UpdateQty(id);
    }
//    private void UpdateQty(int id)
//    {
//        string sql = string.Format(@"update Wh_TransDet set Qty=(Qty1*(CASE WHEN ISNULL(QtyPackWhole,0)=0 THEN 1 ELSE QtyPackWhole end)*(CASE WHEN ISNULL(QtyWholeLoose,0)=0 THEN 1 ELSE QtyWholeLoose end)*(CASE WHEN ISNULL(QtyLooseBase,0)=0 THEN 1 ELSE QtyLooseBase end)
//	+Qty2*(CASE WHEN ISNULL(QtyWholeLoose,0)=0 THEN 1 ELSE QtyWholeLoose end)*(CASE WHEN ISNULL(QtyLooseBase,0)=0 THEN 1 ELSE QtyLooseBase end)
//	+Qty3)  where id='{0}'", id);
//        C2.Manager.ORManager.ExecuteScalar(sql);
//    }
    #endregion

    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsInvoice.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
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
        ASPxTextBox txtRefNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
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

    #region DO IN
    protected void grid_DoIn_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsDoIn.FilterExpression = "DoType='IN' and PoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    #endregion

    #region log
    protected void grid_Log_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsLog.FilterExpression = String.Format("RefNo='{0}' and RefType='PO'", refN.Text);//
    }
    #endregion
    protected void grid_Payment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        string sql = string.Format(@"select DocNo from XAApPayable where MastRefNo='{0}'", refN.Text);
        string docNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
        sql = string.Format(@"select PayId from XAApPaymentDet where DocNo='{0}'", docNo);
        int repNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        this.dsPayment.FilterExpression = String.Format("PoNo='{0}' or SequenceId={1}", refN.Text, repNo);
    }
    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_Status = grd_Do.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
        ASPxTextBox txt_DoNo = grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        string doNo = SafeValue.SafeString(txt_DoNo.Text);
        string sql = string.Format(@"select DoStatus from Wh_Trans where DoNo='{0}'", doNo);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        if (status == "Confirmed")
        {
            cmb_Status.Text = "Confirmed";
        }
        if (status == "Draft")
        {
            cmb_Status.Text = "Draft";
        }
        if (status == "Closed")
        {
            cmb_Status.Text = "Closed";
        }
        if (status == "Canceled")
        {
            cmb_Status.Text = "Canceled";
        }
    }

    #region Packing
    protected void Grid_Packing_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select DoNo from wh_trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsWhPacking.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void Grid_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
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
        ASPxTextBox refN = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
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