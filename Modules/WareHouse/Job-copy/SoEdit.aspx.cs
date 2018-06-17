﻿using System;
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
using System.Xml;
using System.IO;

public partial class WareHouse_Job_SoEdit : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            Session["SoWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"] != "0")
            {
                Session["SoWhere"] = "DoNo='" + Request.QueryString["no"] + "'";
                this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
                this.txt_SchRefNo.Text = Request.QueryString["no"];
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"] == "0")
            {
                this.grid_Issue.AddNewRow();
            }
            else
                this.dsIssue.FilterExpression = "1=0";

        }
        if (Session["SoWhere"] != null)
        {
            this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region SO
    protected void grid_Issue_DataSelect(object sender, EventArgs e)
    {
    }
    protected void grid_Issue_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhTrans));
        }
    }
    protected void grid_Issue_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["DoNo"] = "NEW";
        e.NewValues["DoDate"] = DateTime.Now;
        e.NewValues["DoStatus"] = "Draft";
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["ExpectedDate"] = DateTime.Today.AddDays(14);
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.000000;
		e.NewValues["PayTerm"] = "CASH";
		e.NewValues["IncoTerm"] = "EXW";
        e.NewValues["WareHouseId"] = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
    }
    protected string SaveSo()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            ASPxTextBox txt_Id = grid_Issue.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string pId = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTrans),  "Id='" + pId + "'");
            WhTrans  whTrans = C2.Manager.ORManager.GetObject(query) as WhTrans;
            ASPxDateEdit issueDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            bool isNew = false;
            //const string runType = "DOOUT";
            string issueN = "";
            if (whTrans == null)
            {
                whTrans = new WhTrans();
                isNew = true;
                issueN = C2Setup.GetNextNo("", "SaleOrders", issueDate.Date);
                whTrans.DoDate = issueDate.Date;
            }

            ASPxDateEdit doDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            whTrans.DoDate = doDate.Date;
            ASPxDateEdit txt_ExpectedDate = this.grid_Issue.FindEditFormTemplateControl("txt_ExpectedDate") as ASPxDateEdit;
            whTrans.ExpectedDate = txt_ExpectedDate.Date;

            ASPxComboBox doStatus = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            whTrans.DoStatus = SafeValue.SafeString(doStatus.Value);
            //ASPxButtonEdit btn_LCNo = grid_Issue.FindEditFormTemplateControl("btn_LCNo") as ASPxButtonEdit;
            //whTrans.LcNo = btn_LCNo.Text;

            //Main Info
            ASPxButtonEdit ConsigneeCode = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeCode") as ASPxButtonEdit;
            whTrans.PartyId = ConsigneeCode.Text;
            ASPxTextBox ConsigneeName = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeName") as ASPxTextBox;
            whTrans.PartyName = ConsigneeName.Text;
            ASPxMemo consigneeAdd = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
            whTrans.PartyAdd = consigneeAdd.Text;
            //ASPxButtonEdit consigneeCountry = grid_Issue.FindEditFormTemplateControl("txt_Country") as ASPxButtonEdit;
            //whTrans.PartyCountry = consigneeCountry.Text;
            //ASPxButtonEdit consigneeCity = grid_Issue.FindEditFormTemplateControl("txt_City") as ASPxButtonEdit;
            //whTrans.PartyCity = consigneeCity.Text;
            //ASPxTextBox consigneeZip = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            //whTrans.PartyPostalcode = consigneeZip.Text;
            ASPxTextBox pic = grid_Issue.FindEditFormTemplateControl("txt_Pic") as ASPxTextBox;
            whTrans.Pic= pic.Text;
            ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            whTrans.Remark = remark.Text;
            ASPxButtonEdit currency = grid_Issue.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
            whTrans.Currency = currency.Text;
            ASPxSpinEdit exRate = grid_Issue.FindEditFormTemplateControl("spin_ExRate") as ASPxSpinEdit;
            whTrans.ExRate = SafeValue.SafeDecimal(exRate.Value,1);
            ASPxComboBox payTerm = grid_Issue.FindEditFormTemplateControl("cmb_PayTerm") as ASPxComboBox;
            whTrans.PayTerm = payTerm.Text;
            ASPxComboBox incoTerm = grid_Issue.FindEditFormTemplateControl("cmb_IncoTerm") as ASPxComboBox;
            whTrans.IncoTerm = incoTerm.Text;







            //Issue Info
            ASPxTextBox vessel = pageControl.FindControl("txt_Vessel") as ASPxTextBox;
            whTrans.Vessel = vessel.Text;
            ASPxTextBox voyage = pageControl.FindControl("txt_Voyage") as ASPxTextBox;
            whTrans.Voyage = voyage.Text;
            ASPxTextBox hbl = pageControl.FindControl("txt_HBL") as ASPxTextBox;
            whTrans.Hbl = hbl.Text;
            ASPxTextBox obl = pageControl.FindControl("txt_OBL") as ASPxTextBox;
            whTrans.Obl = obl.Text;
            ASPxDateEdit etaDest = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            whTrans.EtaDest = etaDest.Date;
            ASPxTextBox carrier = pageControl.FindControl("txt_DriveName") as ASPxTextBox;
            whTrans.Carrier = carrier.Text;
            ASPxTextBox vehicle = pageControl.FindControl("txt_Vehno") as ASPxTextBox;
            whTrans.Vehicle = vehicle.Text;
            ASPxButtonEdit pol = pageControl.FindControl("txt_POL") as ASPxButtonEdit;
            whTrans.Pol = pol.Text;
            ASPxButtonEdit pod = pageControl.FindControl("txt_POD") as ASPxButtonEdit;
            whTrans.Pod = pod.Text;
            ASPxDateEdit etdPol = pageControl.FindControl("txt_EtdPol") as ASPxDateEdit;
            whTrans.Etd = etdPol.Date;
            ASPxDateEdit etaPod = pageControl.FindControl("txt_EtaPod") as ASPxDateEdit;
            whTrans.Eta = etaPod.Date;
            ASPxMemo txt_CollectFrom = grid_Issue.FindEditFormTemplateControl("txt_CollectFrom") as ASPxMemo;
            whTrans.CollectFrom = txt_CollectFrom.Text;
            ASPxMemo txt_DeliveryTo = grid_Issue.FindEditFormTemplateControl("txt_DeliveryTo") as ASPxMemo;
            whTrans.DeliveryTo = txt_DeliveryTo.Text;
            ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            whTrans.WareHouseId = txt_WareHouseId.Text;
            ASPxButtonEdit txt_SupplierId = grid_Issue.FindEditFormTemplateControl("txt_SupplierId") as ASPxButtonEdit;
            whTrans.SupplierId=txt_SupplierId.Text;
            ASPxTextBox txt_SupplierName = grid_Issue.FindEditFormTemplateControl("txt_SupplierName") as ASPxTextBox;
            whTrans.SupplierName = txt_SupplierName.Text;
            ASPxTextBox txt_MasterDocNo = grid_Issue.FindEditFormTemplateControl("txt_MasterDocNo") as ASPxTextBox;
            whTrans.MasterDocNo = txt_MasterDocNo.Text;

            //Party
            ASPxButtonEdit agentCode = pageControl.FindControl("txt_AgentCode") as ASPxButtonEdit;
            whTrans.AgentId = agentCode.Text;
            ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            whTrans.AgentName = agentName.Text;
            ASPxMemo agentAdd = pageControl.FindControl("memo_AgentAddress") as ASPxMemo;
            whTrans.AgentAdd = agentAdd.Text;
            ASPxTextBox agentPostalCode = pageControl.FindControl("txt_AgentPostalCode") as ASPxTextBox;
            whTrans.AgentZip = agentPostalCode.Text;
            ASPxButtonEdit agentCountry = pageControl.FindControl("txt_AgentCountry") as ASPxButtonEdit;
            whTrans.AgentCountry = agentCountry.Text;
            ASPxButtonEdit agentCity = pageControl.FindControl("txt_AgentCity") as ASPxButtonEdit;
            whTrans.AgentCity = agentCity.Text;
            ASPxTextBox agentTel = pageControl.FindControl("txt_AgentTelexFax") as ASPxTextBox;
            whTrans.AgentTel = agentTel.Text;
            ASPxTextBox agentContact = pageControl.FindControl("txt_AgentContact") as ASPxTextBox;
            whTrans.AgentContact = agentContact.Text;

            ASPxButtonEdit notifyCode = pageControl.FindControl("txt_NotifyCode") as ASPxButtonEdit;
            whTrans.NotifyId = notifyCode.Text;
            ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            whTrans.NotifyName = notifyName.Text;
            ASPxMemo notifyAdd = pageControl.FindControl("memo_NotifyAddress") as ASPxMemo;
            whTrans.NotifyAdd = notifyAdd.Text;
            ASPxTextBox notifyPostalCode = pageControl.FindControl("txt_NotifyPostalCode") as ASPxTextBox;
            whTrans.NotifyZip = notifyPostalCode.Text;
            ASPxButtonEdit notifyCountry = pageControl.FindControl("txt_NotifyCountry") as ASPxButtonEdit;
            whTrans.NotifyCountry = notifyCountry.Text;
            ASPxButtonEdit notifyCity = pageControl.FindControl("txt_NotifyCity") as ASPxButtonEdit;
            whTrans.NotifyCity = notifyCity.Text;
            ASPxTextBox notifyTel = pageControl.FindControl("txt_NotifyTelexFax") as ASPxTextBox;
            whTrans.NotifyTel = notifyTel.Text;
            ASPxTextBox notifyContact = pageControl.FindControl("txt_NotifyContact") as ASPxTextBox;
            whTrans.NotifyContact = notifyContact.Text;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                whTrans.StatusCode = "USE";
                whTrans.CreateBy = userId;
                whTrans.CreateDateTime = DateTime.Now;
                whTrans.UpdateBy = userId;
                whTrans.UpdateDateTime = DateTime.Now;
                whTrans.DoNo = issueN;
                whTrans.DoType = "SO";

                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whTrans);
                C2Setup.SetNextNo("", "SaleOrders", issueN, issueDate.Date);
            }
            else
            {
                whTrans.UpdateBy = userId;
                whTrans.UpdateDateTime = DateTime.Now;
                bool isAddLog=false;
                if(whTrans.DoStatus==SafeValue.SafeString(ConnectSql.ExecuteScalar("Select DoStatus from wh_trans where DoNo='"+whTrans.DoNo+"'")))
                {
                }else
                {
                    isAddLog=true;
                }
                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whTrans);
                if (isAddLog)
                    EzshipLog.Log(whTrans.DoNo, "", "SO", whTrans.DoStatus);
            }
            Session["SoWhere"] = "DoNo='" + whTrans.DoNo + "'";
            this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);
           
            return whTrans.DoNo;
        }
        catch { }
        return "";
    }
    private string SaveDoOut(string poNo,string wh)
    {
        string issueN = "";
        try
        {
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            ASPxTextBox txt_Id = grid_Issue.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string pId = "";
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + pId + "'");
            WhDo whDo = C2.Manager.ORManager.GetObject(query) as WhDo;
            ASPxDateEdit issueDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            bool isNew = false;

            if (whDo == null)
            {
                whDo = new WhDo();
                isNew = true;
                whDo.DoDate = DateTime.Today;
                issueN = C2Setup.GetNextNo("", "DOOUT", whDo.DoDate);
            }

            //Main Info
            ASPxButtonEdit ConsigneeCode = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeCode") as ASPxButtonEdit;
            whDo.PartyId = ConsigneeCode.Text;
            ASPxTextBox ConsigneeName = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeName") as ASPxTextBox;
            whDo.PartyName = ConsigneeName.Text;
            ASPxButtonEdit consigneeCountry = grid_Issue.FindEditFormTemplateControl("txt_Country") as ASPxButtonEdit;
            whDo.PartyCountry = consigneeCountry.Text;
            ASPxButtonEdit consigneeCity = grid_Issue.FindEditFormTemplateControl("txt_City") as ASPxButtonEdit;
            whDo.PartyCity = consigneeCity.Text;
            ASPxMemo consigneeAdd = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
            whDo.PartyAdd = consigneeAdd.Text;
            ASPxTextBox consigneeZip = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            whDo.PartyPostalcode = consigneeZip.Text;
            ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            whDo.Remark = remark.Text;
            whDo.Priority = "EXPORT";

            //Issue Info
            ASPxTextBox vessel = pageControl.FindControl("txt_Vessel") as ASPxTextBox;
            whDo.Vessel = vessel.Text;
            ASPxTextBox voyage = pageControl.FindControl("txt_Voyage") as ASPxTextBox;
            whDo.Voyage = voyage.Text;
            ASPxTextBox hbl = pageControl.FindControl("txt_HBL") as ASPxTextBox;
            whDo.Hbl = hbl.Text;
            ASPxTextBox obl = pageControl.FindControl("txt_OBL") as ASPxTextBox;
            whDo.Obl = obl.Text;
            ASPxDateEdit etaDest = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            whDo.EtaDest = etaDest.Date;
            ASPxTextBox carrier = pageControl.FindControl("txt_DriveName") as ASPxTextBox;
            whDo.Carrier = carrier.Text;
            ASPxTextBox vehicle = pageControl.FindControl("txt_Vehno") as ASPxTextBox;
            whDo.Vehicle = vehicle.Text;
            ASPxButtonEdit pol = pageControl.FindControl("txt_POL") as ASPxButtonEdit;
            whDo.Pol = pol.Text;
            ASPxButtonEdit pod = pageControl.FindControl("txt_POD") as ASPxButtonEdit;
            whDo.Pod = pod.Text;
            ASPxDateEdit etdPol = pageControl.FindControl("txt_EtdPol") as ASPxDateEdit;
            whDo.Etd = etdPol.Date;
            ASPxDateEdit etaPod = pageControl.FindControl("txt_EtaPod") as ASPxDateEdit;
            whDo.Eta = etaPod.Date;

            //Party
            ASPxButtonEdit agentCode = pageControl.FindControl("txt_AgentCode") as ASPxButtonEdit;
            whDo.AgentId = agentCode.Text;
            ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            whDo.AgentName = agentName.Text;
            ASPxMemo agentAdd = pageControl.FindControl("memo_AgentAddress") as ASPxMemo;
            whDo.AgentAdd = agentAdd.Text;
            ASPxTextBox agentPostalCode = pageControl.FindControl("txt_AgentPostalCode") as ASPxTextBox;
            whDo.AgentZip = agentPostalCode.Text;
            ASPxButtonEdit agentCountry = pageControl.FindControl("txt_AgentCountry") as ASPxButtonEdit;
            whDo.AgentCountry = agentCountry.Text;
            ASPxButtonEdit agentCity = pageControl.FindControl("txt_AgentCity") as ASPxButtonEdit;
            whDo.AgentCity = agentCity.Text;
            ASPxTextBox agentTel = pageControl.FindControl("txt_AgentTelexFax") as ASPxTextBox;
            whDo.AgentTel = agentTel.Text;
            ASPxTextBox agentContact = pageControl.FindControl("txt_AgentContact") as ASPxTextBox;
            whDo.AgentContact = agentContact.Text;

            ASPxButtonEdit notifyCode = pageControl.FindControl("txt_NotifyCode") as ASPxButtonEdit;
            whDo.NotifyId = notifyCode.Text;
            ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            whDo.NotifyName = notifyName.Text;
            ASPxMemo notifyAdd = pageControl.FindControl("memo_NotifyAddress") as ASPxMemo;
            whDo.NotifyAdd = notifyAdd.Text;
            ASPxTextBox notifyPostalCode = pageControl.FindControl("txt_NotifyPostalCode") as ASPxTextBox;
            whDo.NotifyZip = notifyPostalCode.Text;
            ASPxButtonEdit notifyCountry = pageControl.FindControl("txt_NotifyCountry") as ASPxButtonEdit;
            whDo.NotifyCountry = notifyCountry.Text;
            ASPxButtonEdit notifyCity = pageControl.FindControl("txt_NotifyCity") as ASPxButtonEdit;
            whDo.NotifyCity = notifyCity.Text;
            ASPxTextBox notifyTel = pageControl.FindControl("txt_NotifyTelexFax") as ASPxTextBox;
            whDo.NotifyTel = notifyTel.Text;
            ASPxTextBox notifyContact = pageControl.FindControl("txt_NotifyContact") as ASPxTextBox;
            whDo.NotifyContact = notifyContact.Text;
            ASPxMemo txt_CollectFrom = grid_Issue.FindEditFormTemplateControl("txt_CollectFrom") as ASPxMemo;
            whDo.CollectFrom = txt_CollectFrom.Text;
            ASPxMemo txt_DeliveryTo = grid_Issue.FindEditFormTemplateControl("txt_DeliveryTo") as ASPxMemo;
            ASPxComboBox cmb_Status = this.grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            whDo.DeliveryTo = txt_DeliveryTo.Text;
            whDo.WareHouseId = wh;
            whDo.DoStatus = cmb_Status.Text;
            whDo.PoNo=DoNo.Text;
            whDo.PoDate = issueDate.Date;
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                whDo.StatusCode = "USE";
                whDo.CreateBy = userId;
                whDo.CreateDateTime = DateTime.Now;
                whDo.DoNo = issueN;
                whDo.DoType = "OUT";
                Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whDo);
                C2Setup.SetNextNo("", "DOOUT", issueN, whDo.DoDate);
            }
            else
            {
                whDo.UpdateBy = userId;
                whDo.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(whDo);
            }
        }
        catch { }
        return issueN;
    }
    protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Issue.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxComboBox cmb_Status = this.grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            agentName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "AgentId" }));
            notifyName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "NotifyId" }));
            string oid = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                string sql = string.Format("select DoStatus from Wh_Trans where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = grid_Issue.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_Void = grid_Issue.FindEditFormTemplateControl("btn_Void") as ASPxButton;
                if (closeInd == "Closed")
                {
                    btn.Text = "Open Job";
                }
                if (closeInd == "Canceled")
                {
                    btn_Void.Text = "Unvoid";
                }


         }
        }
    }
    protected void grid_Issue_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //string s = e.Parameters;
        //if (s == "Save")
        //{
        //    SaveSo();
        //}
    }
    protected void grid_Issue_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        ASPxButtonEdit txt_ConsigneeCode = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeCode") as ASPxButtonEdit;
        string s = e.Parameters;
        if (s == "Save")
        {
            #region Save
            if(txt_ConsigneeCode.Text.Trim()==""){
                e.Result = "Fail";
                return;
            }
            ASPxComboBox doStatus = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            if (doStatus.Text == "Confirmed")
            {
                //check purchase price and sell price
                string sql = string.Format(@"select count(id) from wh_transdet 
 where dono='{0}' and DoType='so'", txt_DoNo.Text);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0) == 0)
                {
                    e.Result = "NO SKU Line";
                    return;
                }
                sql = string.Format(@"select (case when Price=0 then 0 else 1 end) Result from Wh_TransDet where dono='{0}' and DoType='so'", txt_DoNo.Text);
                DataTable tab = ConnectSql.GetTab(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (SafeValue.SafeInt(tab.Rows[i]["Result"], 0) == 0)
                    {
                        e.Result = "No Price";
                        return;
                    }
                }
                sql = string.Format(@"select * from (select so.ProductCode as Sku,so.price SOPrice,isnull(po.price,0) POPrice from wh_transdet so
left join wh_transdet po on po.DoType='PO' and po.ProductCode=so.ProductCode and 
po.LotNo=(Case when len(so.LotNo)>0 then so.LotNo else po.LotNo end)
 where so.dono='{0}' and so.DoType='so') as aa where SoPrice<poprice", txt_DoNo.Text);
                DataTable tab1 = ConnectSql.GetTab(sql);
                if (tab1.Rows.Count > 0)
                {
                    string ss = "Fail.\n";

                    for (int i = 0; i < tab1.Rows.Count; i++)
                    {
                        ss += string.Format("{0} purchase price is {1} \n", tab1.Rows[i]["Sku"], tab1.Rows[i]["PoPrice"]);
                    }
                    ss += "So can't confirm it.";
                    e.Result = ss;

                    return;
                }
            }
            if (txt_DoNo.Text.Length > 4)
            {
                SaveSo();
                e.Result = "";//update old one
            }
            else
                e.Result = SaveSo();// new one
            #endregion
        }
        else if (s == "Close")
        {
            #region Close 
            ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
            string sql = "select DoStatus from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "Closed")
            {

                sql = string.Format("update Wh_Trans set DoStatus='Confirmed',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(txt_DoNo.Text, "", "SO", "Open Job");
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format(@"select count(id) from wh_transdet 
 where dono='{0}' and DoType='so'", txt_DoNo.Text);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                {
                    e.Result = "NO SKU Line";
                    return;
                }
                sql = string.Format(@"select (case when Price=0 then 0 else 1 end) Result from Wh_TransDet where dono='{0}' and DoType='so'", txt_DoNo.Text);
                DataTable tab = ConnectSql.GetTab(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (SafeValue.SafeInt(tab.Rows[i]["Result"], 0) == 0)
                    {
                        e.Result = "No Price";
                        return;
                    }
                }
                bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refN.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update Wh_Trans set DoStatus='Closed',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(txt_DoNo.Text, "", "SO", "Closed");
                        
                        e.Result = "Success";
                    }
                    else
                        e.Result = "Fail";
                }
                else
                    e.Result = "NoMatch";

            }
            #endregion
        }
        if (s == "Void")
        {
            #region void
            //billing
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='WH' and MastRefNo='{0}'", txt_DoNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
            string sql = "select DoStatus from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "Canceled")
            {
                sql = string.Format("update Wh_Trans set DoStatus='Draft',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(txt_DoNo.Text, "", "OUT", "Unvoid");
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refNo.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update Wh_Trans set DoStatus='Canceled',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(txt_DoNo.Text, "", "OUT", "Canceled");
                        e.Result = "Success";
                    }
                    else
                        e.Result = "Fail";
                }
                else
                    e.Result = "NoMatch";
            }
            #endregion
        }
        else if (s == "Confirm")
        {
            #region Confirm
            if (txt_ConsigneeCode.Text.Trim() == "")
            {
                e.Result = "No Customer";
                return;
            }
            ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
            string soNo = SafeValue.SafeString(txt_DoNo.Text);
            string sql = "select DoStatus from Wh_Trans where DoNo='" + soNo + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "Draft")
            {
                sql = string.Format(@"select count(id) from wh_transdet 
 where dono='{0}' and DoType='so'", soNo);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                {
                    e.Result = "NO SKU Line";
                    return;
                }
                bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refN.Text, refType);
                if (closeByEst)
                {                  
                        string wareHouse = SafeValue.SafeString(txt_WareHouseId.Text);
                        string sql1 = string.Format("select doNo from wh_do where DoType='Out' and PoNo='{0}'", soNo);
                        DataTable tab_do = ConnectSql.GetTab(sql1);
                        string where = "(1=0";
                        for (int i = 0; i < tab_do.Rows.Count; i++)
                        {
                            where += string.Format(" or DoNo='{0}' ", tab_do.Rows[i][0]);

                        }
                        where += ")";
                        int cnt = 0;
                        sql = string.Format("select count(*) from wh_transDet where DoNo='{0}' and DoType='SO' and isnull(LotNo,'')='' ", soNo);
                        cnt = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
                        if (cnt > 0)
                        {
                            e.Result = "No Balance Qty or No Lot No!";
                            return;
                        }
                        sql = string.Format(@"select (case when Price=0 then 0 else 1 end) Result from Wh_TransDet where dono='{0}' and DoType='so'", soNo);
                        DataTable tab_price = ConnectSql.GetTab(sql);
                        for (int i = 0; i < tab_price.Rows.Count; i++)
                        {
                            if (SafeValue.SafeInt(tab_price.Rows[i]["Result"], 0) == 0)
                            {
                                e.Result = "No Price";
                                return;
                            }
                        }
                        sql = string.Format(@" select LotNo,
Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {1} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0)
 from Wh_TransDet where DoNo='{0}' and DoType='SO' and Qty1>0", soNo, where);
                        DataTable tab = ConnectSql.GetTab(sql);
                        for (int i = 0; i < tab.Rows.Count; i++)
                        {

                            cnt += SafeValue.SafeInt(tab.Rows[i][1], 0);
                        }
                        if (cnt >= 0)
                        {
                            sql = string.Format("update Wh_Trans set DoStatus='Confirmed',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", soNo, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                            int res = Manager.ORManager.ExecuteCommand(sql);
                            if (res > 0)
                            {
                                EzshipLog.Log(txt_DoNo.Text, "", "SO", "Confirmed");
                                sql = string.Format(@"select LocationCode from Wh_TransDet where DoNo='{0}' group by LocationCode", soNo);
                                DataTable tab_wh = ConnectSql.GetTab(sql);
                                for (int i = 0; i < tab_wh.Rows.Count; i++)
                                {

                                   string wh = SafeValue.SafeString(tab_wh.Rows[i]["LocationCode"]);
                                    string doNo = SaveDoOut(soNo, wareHouse);
                                    sql = @"Insert Into wh_DoDet(JobStatus,DoNo, DoType,ProductCode,ExpiredDate,Price,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
                                    sql += string.Format(@" select * from (select 'Pending' as JobStatus,'{1}'as DoNo, 'Out' as DoType,ProductCode,ExpiredDate,Price
,0 as Qty1
,0 as Qty2
,0 as Qty3
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty4
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty5
,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,'{2}' as CreateBy,getdate() as CreateDateTime,'{2}' as UpdateBy,getdate() as UpdateDateTime,Id as DoInId from Wh_TransDet where DoNo='{0}' and DoType='SO' and LocationCode='{4}'
 ) as tab_aa where qty4>0  ", soNo, doNo, EzshipHelper.GetUserName(), where, wh);


                                    cnt = C2.Manager.ORManager.ExecuteCommand(sql);
                                    if (cnt > -1)
                                    {

                                        //if (wareHouse == "SBL")
                                        // {
                                        // sql = string.Format(@"select StockType from ref_warehouse where Code='{0}'", wareHouse);
                                        // string wh = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
                                        //if (wh == "EDI")
                                        //{
                                        TransferToStock(doNo, "OUT");
                                        EzshipLog.Log(soNo, "", "SO", "Create Do");
                                        //}
                                        // }

                                        e.Result += doNo + " ";
                                        string _body1 = string.Format(@"<br><table border=2><tr><td>Client</td><td>{0}</td><td>DO No</td><td>{1}</td>
<td>SO REF</td><td>{2}</td>
<td>IN CHARGE</td><td>{3}</td>
</tr></table>",
txt_ConsigneeCode.Text,
doNo,
soNo,
EzshipHelper.GetUserName());
                                        sql = string.Format(@"select * from Wh_TransDet where DoNo='{0}'",soNo);
                                        DataTable tab_sku = ConnectSql.GetTab(sql);
                                        string _body2 ="<table border=2>";
                                        _body2 += string.Format(@"<tr><td>#</td><td>SKU</td><td>PRODUCT</td><td>ML</td><td>ACL%</td><td>CO</td><td>NRF/REF</td><td>GBX</td><td>DECODED</td><td>QTY</td></tr>");
                                        for (int j = 0; j < tab_sku.Rows.Count; j++)
                                        {
                                            int id = SafeValue.SafeInt(tab_sku.Rows[j]["Id"], 0);
                                            string sku = SafeValue.SafeString(tab_sku.Rows[i]["ProductCode"]);
                                            string des = SafeValue.SafeString(tab_sku.Rows[i]["Des1"]);
                                            string att1 = SafeValue.SafeString(tab_sku.Rows[i]["Att1"]);
                                            string att2 = SafeValue.SafeString(tab_sku.Rows[i]["Att2"]);
                                            string att3 = SafeValue.SafeString(tab_sku.Rows[i]["Att3"]);
                                            string att4 = SafeValue.SafeString(tab_sku.Rows[i]["Att4"]);
                                            string att5 = SafeValue.SafeString(tab_sku.Rows[i]["Att5"]);
                                            string att6 = SafeValue.SafeString(tab_sku.Rows[i]["Att6"]);
                                            int qty = SafeValue.SafeInt(tab_sku.Rows[j]["Qty1"], 0);
                                            _body2 += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td></tr>",
                                             id, sku, des, att1, att2, att3, att4, att5, att6, qty);
                                        }

                                        _body2 += "</table>";

                                        string _body = _body1 + _body2;
                                        string name = HttpContext.Current.User.Identity.Name;
                                        //Helper.Email.AlertProcess("99915945@qq.com", doNo, "New DO", _body,name);
                                        
                                        Helper.Email.AlertProcess("rejo@cargo.ms", doNo, "New DO", _body,name);

                                    }
                                    else
                                        e.Result = "Fail";
                                }
                            }
                            else
                                e.Result = "Fail";
                        //e.Result = "Success";

                        }
                        else
                            e.Result = "No Balance Qty or No Lot No!";
                }
                else
                    e.Result = "NoMatch";
            }
            #endregion
        }
        if (s == "AddDoOut")
        {
           #region Transfer to Do Out

//            string soNo = SafeValue.SafeString(txt_DoNo.Text);
//            string wareHouse = SafeValue.SafeString(txt_WareHouseId.Text);
//            string sql1 = string.Format("select doNo from wh_do where DoType='Out' and PoNo='{0}'", soNo);
//            DataTable tab_do = ConnectSql.GetTab(sql1);
//            string where = "(1=0";
//            for (int i = 0; i < tab_do.Rows.Count; i++)
//            {
//                where += string.Format(" or DoNo='{0}' ", tab_do.Rows[i][0]);

//            }
//            where += ")";

//            int cnt = 0;
//            string sql = string.Format("select count(*) from wh_transDet where DoNo='{0}' and DoType='SO' and isnull(LotNo,'')='' ", txt_DoNo.Text);
//            cnt = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
//            if (cnt > 0)
//            {
//                e.Result = "No Balance Qty or No Lot No!";
//                return;
//            }
//            sql = string.Format(@" select LotNo,
//Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {1} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0)
// from Wh_TransDet where DoNo='{0}' and DoType='SO' and Qty1>0", txt_DoNo.Text,where);
//            DataTable tab = ConnectSql.GetTab(sql);
//            for (int i = 0; i < tab.Rows.Count; i++)
//            {

//                cnt += SafeValue.SafeInt(tab.Rows[i][1], 0);
//            }
//            if (cnt >= 0)
//            {
//                sql = string.Format(@"select LocationCode from Wh_TransDet where DoNo='{0}' group by LocationCode",txt_DoNo.Text);
//                DataTable tab_wh=ConnectSql.GetTab(sql);
//                for (int i = 0; i < tab_wh.Rows.Count;i++ )
//                {

//                    wareHouse = SafeValue.SafeString(tab_wh.Rows[i]["LocationCode"]);
//                    string doNo= SaveDoOut(soNo, wareHouse);
//                    sql = @"Insert Into wh_DoDet(JobStatus,DoNo, DoType,ProductCode,ExpiredDate,Price,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
//                    sql += string.Format(@" select * from (select 'Pending' as JobStatus,'{1}'as DoNo, 'Out' as DoType,ProductCode,ExpiredDate,Price
//,0 as Qty1
//,0 as Qty2
//,0 as Qty3
//,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty4
//,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty5
//,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,'{2}' as CreateBy,getdate() as CreateDateTime,'{2}' as UpdateBy,getdate() as UpdateDateTime,Id as DoInId from Wh_TransDet where DoNo='{0}' and DoType='SO' and LocationCode='{4}'
// ) as tab_aa where qty4>0  ", txt_DoNo.Text, doNo, EzshipHelper.GetUserName(), where,wareHouse);


//                    cnt = C2.Manager.ORManager.ExecuteCommand(sql);
//                    if (cnt > -1)
//                    {

//                        //if (wareHouse == "SBL")
//                       // {
//                           // sql = string.Format(@"select StockType from ref_warehouse where Code='{0}'", wareHouse);
//                           // string wh = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
//                            //if (wh == "EDI")
//                            //{
//                            TransferToStock(doNo, "OUT");
//                            EzshipLog.Log(txt_DoNo.Text, "", "SO", "Create Do");
//                            //}
//                       // }
//                        e.Result += doNo+" ";
//                    }
//                    else
//                        e.Result = "Fail";
//                }
//            }
//            else
//                e.Result = "No Balance Qty or No Lot No!";

        #endregion
        }
        if (s == "CreatePO")
        {
            #region Create PO
            string sql = string.Format(@"select Count(*) from Wh_TransDet where DoNo='{0}' and DoType='SO' and ISNULL(LotNo,'')=''", txt_DoNo.Text);
            int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (cnt > 0)
            {
                string pId = "";
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "Id='" + pId + "'");
                WhTrans whTrans = C2.Manager.ORManager.GetObject(query) as WhTrans;
                ASPxDateEdit txt_Date = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
                ASPxButtonEdit txt_SupplierId = grid_Issue.FindEditFormTemplateControl("txt_SupplierId") as ASPxButtonEdit;
                ASPxTextBox txt_SupplierName = grid_Issue.FindEditFormTemplateControl("txt_SupplierName") as ASPxTextBox;
                whTrans = new WhTrans();
                string poNo = C2Setup.GetNextNo("", "PurchaseOrders", txt_Date.Date);
                whTrans.DoNo = poNo;
                whTrans.DoType = "PO";
                whTrans.PartyId = SafeValue.SafeString(txt_SupplierId.Text);
                whTrans.PartyName = SafeValue.SafeString(txt_SupplierName.Text);
                whTrans.CreateBy = EzshipHelper.GetUserName();
                whTrans.CreateDateTime = DateTime.Now;
                whTrans.DoDate = txt_Date.Date;
                whTrans.ExpectedDate = DateTime.Today.AddDays(14);
                whTrans.Currency = "SGD";
                whTrans.DoStatus = "Draft";
                whTrans.ExRate =SafeValue.SafeDecimal(1.000000);
                whTrans.WareHouseId = txt_WareHouseId.Text;
                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whTrans);
                C2Setup.SetNextNo("", "PurchaseOrders", poNo, txt_Date.Date);
                sql = string.Format(@"select * from Wh_TransDet where DoNo='{0}' and DoType='SO' and ISNULL(LotNo,'')=''", txt_DoNo.Text);
                DataTable tab = ConnectSql.GetTab(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    string doNo = SafeValue.SafeString(txt_DoNo.Text);
                    string sku = SafeValue.SafeString(tab.Rows[i]["ProductCode"]);
                    string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
                    int qty = SafeValue.SafeInt(tab.Rows[i]["Qty1"], 0);
                    decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"]);
                    sql = @"Insert Into Wh_PoRequest(SoNo,Product,Qty1,Qty2,Qty3,Price,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,PoNo)";
                    sql += string.Format(@"select '{0}'as DoNo, '{1}' as Sku,'{2}' as Qty1,0 as Qty2,0 as Qty3,'{3}',p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase,'{4}' as CreateBy,getdate() as CreateDateTime,
'{4}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,p.Description,p.Att1,'{5}'
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", doNo, sku, qty, price, EzshipHelper.GetUserName(), poNo);
                    cnt = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
                    if (cnt > 0)
                    {
                        sql = @"Insert Into Wh_TransDet(DoNo,ProductCode,DoType,Qty1,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing)";
                        sql += string.Format(@"select '{0}'as DoNo, '{1}' as Sku,'PO','{2}' as Qty1,'{3}','{5}',p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase,'{4}' as CreateBy,getdate() as CreateDateTime,
'{4}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,p.Description,p.Att1 
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", poNo, sku, qty, price, EzshipHelper.GetUserName(),lotNo);
                        ConnectSql.ExecuteSql(sql);
                    }
                }
                e.Result = "Success";
            }
            else
            {
                e.Result = "Fail";
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
        stock.CreateDoOut("zhaohui", orderNo, bt);
    }
    private void UpdateSoDetBalQty(string soNo)
    {
        string sql = string.Format(@"update Wh_TransDet set BalQty=Qty-isnull((select sum(Qty) from Wh_DoDet where DoInId=Wh_TransDet.Id),0) where  dono='{0}' and dotype='so'", soNo);
        C2.Manager.ORManager.ExecuteScalar(sql);
    }
    #endregion

    #region SKULine
    protected void grid_SKULine_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhTransDet));
        }
    }
    protected void grid_SKULine_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsIssueDet.FilterExpression = "DoType= 'SO' and DoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_SKULine_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
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
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        e.NewValues["LocationCode"] = SafeValue.SafeString(txt_WareHouseId.Text);
    }
    protected void grid_SKULine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("Sku not be null !!!");
            return;
        }
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["DoNo"] = refN.Text;
        e.NewValues["DoType"] = "SO";

        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);

        e.NewValues["LocationCode"] = SafeValue.SafeString(e.NewValues["LocationCode"]);
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
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Today;
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
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;

        #region vali Qty
        //vali qty1>balqty,auto change qty1=balqty, then add another sku line,it's qty1=oldQty1-balqty

        string product = SafeValue.SafeString(e.NewValues["ProductCode"]);
        string lotNo = SafeValue.SafeString(e.NewValues["LotNo"]);
        int qty = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        string des = SafeValue.SafeString(e.NewValues["Des1"]);
        string uom1 = SafeValue.SafeString(e.NewValues["Uom1"]);
        string uom2 = SafeValue.SafeString(e.NewValues["Uom2"]);
        string uom3 = SafeValue.SafeString(e.NewValues["Uom3"]);
        int pkg = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        int unit = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        int stk = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);
        string att1 = SafeValue.SafeString(e.NewValues["Att1"]);
        string att2 = SafeValue.SafeString(e.NewValues["Att2"]);
        string att3 = SafeValue.SafeString(e.NewValues["Att3"]);
        string att4 = SafeValue.SafeString(e.NewValues["Att4"]);
        string att5 = SafeValue.SafeString(e.NewValues["Att5"]);
        string att6 = SafeValue.SafeString(e.NewValues["Att6"]);
        decimal price = SafeValue.SafeDecimal(e.NewValues["Price"]);
        string wh = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format(@"select TOP(1) code from ref_warehouse where code!='{0}'",txt_WareHouseId.Text)));
       // e.NewValues["Qty1"] = AddNewSkuLine(qty, refN.Text, product, lotNo, des, uom1, pkg, unit, att1, att2, att3, att4, att5, att6, stk, uom2, uom3, price, packing, wh);
        #endregion

    }
    protected void grid_SKULine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("SKU not be null !!!");
            return;
        }
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);
        e.NewValues["LocationCode"] = SafeValue.SafeString(e.NewValues["LocationCode"]);

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
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Today;
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
        #region vali Qty
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;

        //vali qty1>balqty,auto change qty1=balqty, then add another sku line,it's qty1=oldQty1-balqty

        string product = SafeValue.SafeString(e.NewValues["ProductCode"]);
        string lotNo = SafeValue.SafeString(e.NewValues["LotNo"]);
        int qty = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        string des = SafeValue.SafeString(e.NewValues["Des1"]);
        string uom1 = SafeValue.SafeString(e.NewValues["Uom1"]);
        string uom2 = SafeValue.SafeString(e.NewValues["Uom2"]);
        string uom3 = SafeValue.SafeString(e.NewValues["Uom3"]);
        int pkg = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        int unit = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        int stk = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);
        string att1 = SafeValue.SafeString(e.NewValues["Att1"]);
        string att2 = SafeValue.SafeString(e.NewValues["Att2"]);
        string att3 = SafeValue.SafeString(e.NewValues["Att3"]);
        string att4 = SafeValue.SafeString(e.NewValues["Att4"]);
        string att5 = SafeValue.SafeString(e.NewValues["Att5"]);
        string att6 = SafeValue.SafeString(e.NewValues["Att6"]);
        decimal price = SafeValue.SafeDecimal(e.NewValues["Price"]);
        string wh = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format(@"select TOP(1) code from ref_warehouse where code!='{0}'", txt_WareHouseId.Text)));
        //e.NewValues["Qty1"] = AddNewSkuLine(qty, refN.Text, product, lotNo, des, uom1, pkg, unit, att1, att2, att3, att4, att5, att6, stk, uom2, uom3, price, packing, wh);
        #endregion

    }
    protected void grid_SKULine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_SKULine_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        //int transId = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select max(id) from wh_transDet where DoNo='{0}' and DoType='SO'",refN.Text)), 0);
        //UpdatePoDetBalQty(transId);
    }
    protected void grid_SKULine_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //int transId = SafeValue.SafeInt(e.Keys["Id"], 0);
        //UpdatePoDetBalQty(transId);
    }
    private void UpdatePoDetBalQty(int transId)
    {
        string sql = string.Format(@"update Wh_TransDet set BalQty=Qty-isnull((select sum(Qty) from Wh_DoDet where DoInId={0}),0) where Id='{0}'", transId);
        C2.Manager.ORManager.ExecuteScalar(sql);
    }
    private int AddNewSkuLine(int qty,string doNo,string product,string lotNo,string des,string uom1,int pkg,int unit,string att1
        ,string att2,string att3,string att4,string att5,string att6,int stk,string uom2,string uom3,decimal price,string packing,string wh)
    {
        string sql = string.Format(@"select tab_hand.HandQty-isnull(tab_Reserved.ReservedQty,0) as BalQty
from (select product,LotNo,Packing ,sum(isnull(Case when det.DoType='In' then Qty1 else -Qty1 end,0)) as HandQty from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode!='CNL' group by product,LotNo,Packing) as tab_hand
left join (select productCode as Product,LotNo,sum(Qty5) as ReservedQty from wh_doDet det inner join  wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' group by productCode,LotNo) as tab_Reserved on tab_Reserved.product=tab_hand.product and tab_Reserved.LotNo=tab_hand.LotNo 
where tab_hand.Product='{0}'", product, lotNo);
        int balQty = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (qty > balQty&&balQty!=0)
        {
            sql = @"Insert Into wh_TransDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Qty4,Qty5,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,DocAmt,LocationCode)";
           sql += string.Format(@"select '{0}'as DoNo, 'SO' as DoType,'{1}' as Sku,'{2}' as Qty1,0 as Qty2,0 as Qty3,0 as Qty4,0 as Qty5,'{3}' as Price,'{4}' as LotNo
,'{5}' as Uom1,'{6}' as Uom2,'{7}' as Uom3,'' as Uom4
,'{8}' as QtyPackWhole,'{9}' as QtyWholeLoose,'{10}' as QtyLooseBase
,'{11}' as CreateBy,getdate() as CreateDateTime,'{11}' as UpdateBy,getdate() as UpdateDateTime
,'{12}' as att1,'{13}' as att2,'{14}' as att3,'{15}' as att4,'{16}' as att5,'{17}' as att6,'{18}' as Des1,'{19}' as Packing,({2}*{3}) as DocAmt,'{20}' as LocationCode
from (select '{1}' as Sku) as tab", doNo, product, (qty-balQty), price, lotNo, uom1, uom2, uom3, pkg, unit, stk, EzshipHelper.GetUserName(), att1, att2, att3, att4, att5, att6, des, packing,wh);
           ConnectSql.ExecuteSql(sql);
        }
        else
        {
            balQty = qty;
        }
        return balQty;
    }
    #endregion

    #region Billing
    protected void Grid_Invoice_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsInvoice.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void Grid_Payable_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsVoucher.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
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
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsIssuePhoto.FilterExpression = "RefNo='" + refN.Text + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }



    #endregion
    #region log
    protected void grid_Log_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsLog.FilterExpression = String.Format("RefNo='{0}'", refN.Text);//
    }
    #endregion
    #region DO out
    protected void grid_DoIn_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsDoIn.FilterExpression = "DoType='Out' and PoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    #endregion
    #region po request
    protected void grid_PoRequest_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhPORequest));
        }
    }
    protected void grid_PoRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsPoRequest.FilterExpression = "SoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_PoRequest_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty1"] = 1;
        e.NewValues["Qty2"] = 0;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Qty4"] = 0;
        e.NewValues["Qty5"] = 0;
        e.NewValues["QtyPackWhole"] = 1;
        e.NewValues["QtyWholeLoose"] = 1;
        e.NewValues["QtyLooseBase"] = 1;
        e.NewValues["Price"] = 0;
    }
    protected void grid_PoRequest_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["SoNo"] = refN.Text;


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
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_PoRequest_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
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
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            if (e.NewValues[i] == null)
            {
                e.NewValues[i] = "";
            }
        }
    }
    protected void grid_PoRequest_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_PoRequest_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {

    }
    protected void grid_PoRequest_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
    } 
    #endregion
    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
        ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        string doNo = SafeValue.SafeString(txt_DoNo.Text);
        string sql = string.Format(@"select DoStatus from Wh_Trans where DoNo='{0}'",doNo);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        //sql = string.Format(@"select StatusCode from wh_do where PoNo='{0}'", doNo);
        //string doStatus = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
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
        //if (status == "CLS")
        //{
        //    sql = string.Format(@"update Wh_Trans set DoStatus='Delivered' where DoNo='{0}'",doNo);
        //    C2.Manager.ORManager.ExecuteCommand(sql);
        //    cmb_Status.Text = "Delivered";
        //}
    }
    protected void grid_Payment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        string sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}'",refN.Text);
        string docNo =SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
        sql = string.Format(@"select RepId from XAArReceiptdet where DocNo='{0}'", docNo);
        int repNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
        this.dsReceipt.FilterExpression = String.Format("SequenceId={0} or SoNo='{1}'", repNo, refN.Text);//
    }
    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select DoNo from wh_trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "JobType='SO' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
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
        ASPxTextBox doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["JobType"] = "SO";
        e.NewValues["RefNo"] = doNo.Text;
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
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
            ASPxButtonEdit doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
            ASPxComboBox impExp = this.grid_Issue.FindEditFormTemplateControl("cmb_Priority") as ASPxComboBox;
            ASPxComboBox fclLcl = this.grid_Issue.FindEditFormTemplateControl("cmb_ModelType") as ASPxComboBox;
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
                sql += string.Format(@"select '{0}','OUT',ChgCode,ChgDes,'SKU:'+productcode+';LotNo:'+LotNo
,Case when Qty1*price>=Amt then Qty1 else 1 end as Qty
,Case when Qty1*price>=Amt then Price else Amt end as Price
,Currency,ExRate,Unit,Gst from 
(select productcode,lotno,qty1 from wh_dodet where dono='{0}' and DoType='OUT' ) as tab1
cross  join( select ChgCode,chgdes,Currency,exrate,Price,qty,amt,Unit,gsttype,gst from SeaQuoteDet1 where {1}) as tab2
", doNo.Text, where);
                ConnectSql.ExecuteSql(sql);
                sql = string.Format("Update wh_costing set CostDocAmt=convert(decimal(10,2),CostQty*CostPrice)+convert(decimal(10,2),CostQty*CostPrice)*Costgst where RefNo='{0}' and JobType='OUT'", doNo.Text);
                ConnectSql.ExecuteSql(sql);
                sql = string.Format("Update wh_costing set CostLocAmt=CostDocAmt*CostExRate where RefNo='{0}' and JobType='OUT'", doNo.Text);
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
            string sql = "select DoNo from wh_trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsWhPacking.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void Grid_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
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
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
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
        ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;

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