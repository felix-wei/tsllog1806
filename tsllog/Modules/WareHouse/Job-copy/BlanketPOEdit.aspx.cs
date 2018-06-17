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

public partial class WareHouse_Job_BlanketPOEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BPWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["BPWhere"] = "DoNo='" + Request.QueryString["no"].ToString() + "' ";
                this.txt_DoNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["BPWhere"] == null)
                {
                    this.grd_Do.AddNewRow();
                }
            }
            else
                this.dsDo.FilterExpression = "1=0";
        }
        if (Session["BPWhere"] != null)
        {
            this.dsDo.FilterExpression = Session["BPWhere"].ToString();
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
        e.NewValues["DoType"] = "BP";
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
            WhTrans whTrans = C2.Manager.ORManager.GetObject(query) as WhTrans;
            bool action = false;
            string poNo = "";
            if (whTrans == null)
            {
                action = true;
                whTrans = new WhTrans();
                poNo = C2Setup.GetNextNo("", "BlanketPO", txt_Date.Date);
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
            ASPxComboBox cmb_OrderType = grd_Do.FindEditFormTemplateControl("cmb_OrderType") as ASPxComboBox;
            whTrans.OrderType = cmb_OrderType.Text;

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

            ASPxButtonEdit btn_Lc = grd_Do.FindEditFormTemplateControl("btn_Lc") as ASPxButtonEdit;
            whTrans.LcNo = btn_Lc.Text;

            if (action)
            {
                whTrans.DoNo = poNo;
                whTrans.DoType = "BP";
                whTrans.CreateBy = EzshipHelper.GetUserName();
                whTrans.CreateDateTime = DateTime.Now;

                Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whTrans);
                C2Setup.SetNextNo("", "BlanketPO", poNo, txt_Date.Date);
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
            Session["BPWhere"] = "DoNo='" + whTrans.DoNo + "'";
            this.dsDo.FilterExpression = Session["BPWhere"].ToString();
            if (this.grd_Do.GetRow(0) != null)
                this.grd_Do.StartEdit(0);
            return whTrans.DoNo;
        }
        catch { }
        return "";
    }
    
    protected void grd_Do_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grd_Do.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox id = grd_Do.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_DoNo = this.grd_Do.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        ASPxButtonEdit txt_WareHouseId = grd_Do.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        ASPxButtonEdit txt_PartyId = grd_Do.FindEditFormTemplateControl("txt_PartyId") as ASPxButtonEdit;
        ASPxTextBox txt_PartyName = grd_Do.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
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
            string sql = "select StatusCode from Wh_Trans where DoNo='" + txt_DoNo.Text + "'";
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
                        //string poNo = AddPurchase(tab, txt_DoNo.Text, txt_PartyId.Text, txt_PartyName.Text, txt_WareHouseId.Text);
                        //TransferToStock(txt_DoNo.Text, "PO");
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
        else if (s == "CreatePO")
        {
            #region Create PO
            string sql = string.Format(@"select * from Wh_TransDet where DoNo='{0}' and Qty1>0", txt_DoNo.Text);
            DataTable tab = ConnectSql.GetTab(sql);
            if (tab.Rows.Count > 0)
            {
                sql = string.Format(@"select count(*) from (select sum(Qty1) as TotalQty1,mast.DoNo from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoType='BP' group by mast.DoNo) as tab_bp
left join (select sum(Qty1) as TotalQty2,BlanketNo from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoType='PO' group by BlanketNo) as tab_so on tab_so.BlanketNo=tab_bp.DoNo
where TotalQty1-TotalQty2=0 and BlanketNo='{0}'", txt_DoNo.Text);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "The purchase qty has been complete";
                    return;
                }
                string poNo = AddPurchase(tab,txt_DoNo.Text, txt_PartyId.Text, txt_PartyName.Text, txt_WareHouseId.Text);
                e.Result = "Success! " + "Purcharse Order No is " + poNo;

            }
            else
            {
                e.Result = "NO Qty";
            }
            #endregion
        }
    }
    private string AddPurchase(DataTable tab,string doNo,string partyId,string partyName,string wh)
    {
        string poNo = "";
        string pId = "";
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "Id='" + pId + "'");
        WhTrans whTrans = C2.Manager.ORManager.GetObject(query) as WhTrans;

        whTrans = new WhTrans();
        poNo = C2Setup.GetNextNo("", "PurchaseOrders", DateTime.Now);
        whTrans.DoNo = poNo;
        whTrans.DoType = "PO";
        whTrans.PartyId = partyId;
        whTrans.PartyName = partyName;
        whTrans.CreateBy = EzshipHelper.GetUserName();
        whTrans.CreateDateTime = DateTime.Now;
        whTrans.DoDate = DateTime.Now;
        whTrans.ExpectedDate = DateTime.Today.AddDays(14);
        whTrans.Currency = "SGD";
        whTrans.DoStatus = "Draft";
        whTrans.ExRate = SafeValue.SafeDecimal(1.000000);
        whTrans.WareHouseId = wh;
        whTrans.BlanketNo = txt_DoNo.Text;
        Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(whTrans);
        C2Setup.SetNextNo("", "PurchaseOrders", poNo, DateTime.Now);


        for (int i = 0; i < tab.Rows.Count; i++)
        {
            string sku = SafeValue.SafeString(tab.Rows[i]["ProductCode"]);
            string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
            int qty = SafeValue.SafeInt(tab.Rows[i]["Qty1"], 0);
            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"]);
            string location = SafeValue.SafeString(tab.Rows[i]["LocationCode"]);
            string sql = @"Insert Into Wh_TransDet(DoNo,ProductCode,DoType,Qty1,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,LocationCode)";
            sql += string.Format(@"select '{0}'as DoNo, '{1}' as Sku,'PO','{2}' as Qty1,'{3}','{5}',p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase,'{4}' as CreateBy,getdate() as CreateDateTime,
'{4}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,p.Description,p.Att1,'{6}'
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", poNo, sku, 0, price, EzshipHelper.GetUserName(), lotNo, location);
            ConnectSql.ExecuteSql(sql);

        }
        return poNo;
    }
    private void TransferToStock(string orderNo, string orderType)
    {
        string url = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["WhService_ToStock"]);
        if (url.Length == 0)
            return;
        stockApi.stock stock = new stockApi.stock();
        stock.Url = url;
        XmlDocument xmlDoc = EDI_Wh.Export_PoOrder(orderNo, orderType, "Confirmed");
        if (xmlDoc == null)
            return;
        MemoryStream ms = new MemoryStream();
        xmlDoc.Save(ms);
        byte[] bt = ms.GetBuffer();
        stock.CreatePo("zhaohui", orderNo, bt);
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

        e.NewValues["Qty1"] = SafeValue.SafeDecimal(e.NewValues["Qty1"]);

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

        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
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
        e.NewValues["Qty1"] = SafeValue.SafeDecimal(e.NewValues["Qty1"]);
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

        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
;
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

    #region Sub PO/PO Invoice
    protected void grid_SubSo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        string blanketNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1");
        sql = string.Format(@"select mast.DoNo,DoDate,PartyName,TotalQty,DoStatus from Wh_Trans mast left join (select sum(Qty1) as TotalQty,DoNo from Wh_TransDet group by DoNo) as tab on tab.DoNo=mast.DoNo where BlanketNo='{0}' and DoType='PO'", blanketNo);
        DataTable tab = ConnectSql.GetTab(sql);
        grd.DataSource = tab;
    }
    protected void grid_SoInv_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string doNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        sql = string.Format(@"select * from XAArInvoice inv left join Wh_Trans mast on inv.MastRefNo=mast.DoNo and mast.DoType='PO' where BlanketNo='{0}'", doNo);
        DataTable tab = ConnectSql.GetTab(sql);
        grd.DataSource = tab;

    }
    #endregion

    #region GRN
    protected void grid_DoIn_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_Trans where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string doNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        sql = string.Format(@"select * from Wh_Do d left join (select sum(Qty1) as TotalQty,DoNo from Wh_TransDet group by DoNo) as det on d.DoNo=det.DoNo left join Wh_Trans mast on d.PoNo=mast.DoNo and d.DoType='IN' where BlanketNo='{0}'", doNo);
        DataTable tab = ConnectSql.GetTab(sql);
        grd.DataSource = tab;
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