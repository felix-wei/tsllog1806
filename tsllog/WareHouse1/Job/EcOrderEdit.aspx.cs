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

public partial class WareHouse_Job_EcOrderEdit : System.Web.UI.Page
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
    private string SaveDoOut(string poNo)
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
        //if (this.grid_Issue.EditingRowVisibleIndex > -1)
        //{
        //    ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        //    //ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
        //    //ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
        //    //agentName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "AgentId" }));
        //    //notifyName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "NotifyId" }));
        //    string oid = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
        //    if (oid.Length > 0)
        //    {
        //        ASPxDateEdit txt_Date = this.grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
        //        txt_Date.BackColor = System.Drawing.Color.FromArgb(250, 240, 240, 240);
        //        txt_Date.ReadOnly = true;
        //        string sql = string.Format("select StatusCode from Wh_Trans where Id='{0}'", oid);
        //        string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
        //        //ASPxButton btn = grid_Issue.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
        //        //ASPxButton btn_Void = grid_Issue.FindEditFormTemplateControl("btn_Void") as ASPxButton;
        //        //if (closeInd == "CLS")
        //        //{
        //        //    btn.Text = "Open Job";
        //        //}
        //        //if (closeInd == "CNL")
        //        //{
        //        //    btn_Void.Text = "Unvoid";
        //        //}

        //        //ASPxCheckBox receipt = pageControl.FindControl("ack_PopulatetoReceipt") as ASPxCheckBox;
        //        //ASPxCheckBox xDock = pageControl.FindControl("ack_PopulatetoXDock") as ASPxCheckBox;
        //        //bool receiptInd = SafeValue.SafeBool(C2.Manager.ORManager.ExecuteScalar(string.Format("select ReceiptInd from Wh_Trans where Id={0}", oid)), false);
        //        //bool xDockInd = SafeValue.SafeBool(C2.Manager.ORManager.ExecuteScalar(string.Format("select XDockInd from Wh_Trans where Id={0}", oid)), false);
        //        //if (receiptInd == true)
        //        //    receipt.Checked = true;
        //        //if (xDockInd == true)
        //        //    xDock.Checked = true;
        //    }
        //}
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
        string s = e.Parameters;
        if (s == "Save")
        {
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
                        ss += string.Format("{0} purchase price is {1} \n", tab.Rows[i]["Sku"], tab.Rows[i]["PoPrice"]);
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
            string sql = "select StatusCode from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update Wh_Trans set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                    sql = string.Format("update Wh_Trans set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(txt_DoNo.Text, "", "OUT", "Void");
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
        else if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update Wh_Trans set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(txt_DoNo.Text, "", "OUT", "Open");
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                bool closeByEst = true;//EzshipHelper.GetCloseEstInd(refN.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update Wh_Trans set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", txt_DoNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(txt_DoNo.Text, "", "OUT", "Close");
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
        if (s == "AddDoOut")
        {
            #region Transfer to Do Out

            string soNo = SafeValue.SafeString(txt_DoNo.Text);
            string sql1 = string.Format("select doNo from wh_do where DoType='Out' and PoNo='{0}'", soNo);
            DataTable tab_do = ConnectSql.GetTab(sql1);
            string where = "(1=0";
            for (int i = 0; i < tab_do.Rows.Count; i++)
            {
                where += string.Format(" or DoNo='{0}' ", tab_do.Rows[i][0]);

            }
            where += ")";


            string sql = string.Format(@" select 
Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {1} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0)
 from Wh_TransDet where DoNo='{0}' and DoType='SO' and Qty1>0", txt_DoNo.Text,where);
            DataTable tab = ConnectSql.GetTab(sql);
            int cnt = 0;
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                cnt += SafeValue.SafeInt(tab.Rows[i][0], 0);
            }
            if (cnt >= 0)
            {
                string doNo = SaveDoOut(soNo);
                sql = @"Insert Into wh_DoDet(DoNo, DoType,ProductCode,Price,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
                sql += string.Format(@" select * from (select '{1}'as DoNo, 'Out' as DoType,ProductCode,Price
,0 as Qty1
,0 as Qty2
,0 as Qty3
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty4
,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where {3} and DoType='Out' and ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty5
,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,'{2}' as CreateBy,getdate() as CreateDateTime,'{2}' as UpdateBy,getdate() as UpdateDateTime,Id as DoInId from Wh_TransDet where DoNo='{0}' and DoType='SO'
 ) as tab_aa where qty4>0 ", txt_DoNo.Text, doNo, EzshipHelper.GetUserName(),where);


                  cnt = C2.Manager.ORManager.ExecuteCommand(sql);
                if (cnt > -1)
                {
                    e.Result = doNo;
                    TransferToStock(doNo, "OUT");
                }
                else
                    e.Result = "Fail";
            }
            else
                e.Result = "No Balance Qty or No Lot No!";

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

        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;

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
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Today;

        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;
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
        this.dsLog.FilterExpression = String.Format("RefNo='{0}' and RefType='SO'", refN.Text);//
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
}