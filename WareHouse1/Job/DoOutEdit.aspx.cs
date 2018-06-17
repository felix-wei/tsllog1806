using System;
using System.Collections.Generic;
using System.Web;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.Xml;
using System.IO;

public partial class Pages_DoOutEdit : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            Session["Issue"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"] != "0")
            {
                Session["Issue"] = "DoNo='" + Request.QueryString["no"] + "'";
                this.dsIssue.FilterExpression = Session["Issue"].ToString();
                this.txt_SchRefNo.Text = Request.QueryString["no"];
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"] == "0")
            {
                this.grid_Issue.AddNewRow();
            }
            else
                this.dsIssue.FilterExpression = "1=0";

        }
        if (Session["Issue"] != null)
        {
            this.dsIssue.FilterExpression = Session["Issue"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Issue
    protected void grid_Issue_DataSelect(object sender, EventArgs e)
    {
    }
    protected void grid_Issue_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhDo));
        }
    }
    protected void grid_Issue_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DoNo"] = "NEW";
        e.NewValues["Priority"] = "EXPORT";
        e.NewValues["DoDate"] = DateTime.Now;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["WareHouseId"] = "CM";//System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
    }
    protected string Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox txt_Id = this.grid_Issue.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string issueN = "";
            string pId = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + pId + "'");
            WhDo whDo = C2.Manager.ORManager.GetObject(query) as WhDo;
            bool isNew = false;
            string runType = "DOOUT";

            ASPxDateEdit doDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            if (whDo == null)
            {
                whDo = new WhDo();
                isNew = true;
                issueN = C2Setup.GetNextNo("", runType, doDate.Date);
                whDo.DoType = "OUT";
            }

            whDo.DoDate = doDate.Date;
            ASPxComboBox priority = grid_Issue.FindEditFormTemplateControl("cmb_Priority") as ASPxComboBox;
            whDo.Priority = priority.Text;
            ASPxComboBox status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            whDo.DoStatus = status.Text;

            //Main Info
			/*
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


            ASPxComboBox PermitBy = grid_Issue.FindEditFormTemplateControl("txt_PermitBy") as ASPxComboBox;
            whDo.PermitBy = PermitBy.Text;
            ASPxMemo OtherPermit = grid_Issue.FindEditFormTemplateControl("txt_OtherPermit") as ASPxMemo;
            whDo.OtherPermit = OtherPermit.Text;
            ASPxComboBox ModelType = grid_Issue.FindEditFormTemplateControl("cmb_ModelType") as ASPxComboBox;
            whDo.ModelType = ModelType.Text;

            ASPxCheckBox receiptInd = grid_Issue.FindEditFormTemplateControl("ack_PopulatetoReceipt") as ASPxCheckBox;
            whDo.ReceiptInd = (receiptInd.Checked) ? true : false;
            ASPxCheckBox xDockInd = grid_Issue.FindEditFormTemplateControl("ack_PopulatetoXDock") as ASPxCheckBox;
            whDo.XDockInd = (xDockInd.Checked) ? true : false;
            ASPxTextBox receiptNo = grid_Issue.FindEditFormTemplateControl("txt_ReceiptNo") as ASPxTextBox;
            whDo.ReceiptNo = receiptNo.Text;
            ASPxTextBox xDockReference = grid_Issue.FindEditFormTemplateControl("txt_XDockReference") as ASPxTextBox;
            whDo.XDockReference = xDockReference.Text;
            ASPxTextBox poNo = grid_Issue.FindEditFormTemplateControl("txt_PONo") as ASPxTextBox;
            whDo.PoNo = poNo.Text;
            ASPxDateEdit poDate = grid_Issue.FindEditFormTemplateControl("date_PODate") as ASPxDateEdit;
            whDo.PoDate = poDate.Date;
            ASPxTextBox route = grid_Issue.FindEditFormTemplateControl("txt_Route") as ASPxTextBox;
            whDo.Route = route.Text;
            ASPxTextBox group = grid_Issue.FindEditFormTemplateControl("txt_Group") as ASPxTextBox;
            whDo.GroupId = group.Text;
            ASPxTextBox transName = grid_Issue.FindEditFormTemplateControl("txt_TransName") as ASPxTextBox;
            whDo.TptName = transName.Text;
            ASPxTextBox quotation = grid_Issue.FindEditFormTemplateControl("txt_Quotation") as ASPxTextBox;
            whDo.Quotation = quotation.Text;
            ASPxComboBox equipmentNo = grid_Issue.FindEditFormTemplateControl("cmb_EquipmentNo") as ASPxComboBox;
            whDo.EquipNo = equipmentNo.Text;
            ASPxComboBox personnel = grid_Issue.FindEditFormTemplateControl("cmb_Personnel") as ASPxComboBox;
            whDo.Personnel = personnel.Text;
            ASPxTextBox custReference = grid_Issue.FindEditFormTemplateControl("txt_CustReference") as ASPxTextBox;
            whDo.CustomerReference = custReference.Text;
            ASPxDateEdit custDate = grid_Issue.FindEditFormTemplateControl("txt_CustDate") as ASPxDateEdit;
            whDo.CustomerDate = custDate.Date;
            ASPxTextBox customsSealNo = grid_Issue.FindEditFormTemplateControl("txt_CustomsSealNo") as ASPxTextBox;
            whDo.CustomsSealNo = customsSealNo.Text;
            ASPxTextBox permitNo = grid_Issue.FindEditFormTemplateControl("txt_PermitNo") as ASPxTextBox;
            whDo.PermitNo = permitNo.Text;
            ASPxTextBox externalReference = grid_Issue.FindEditFormTemplateControl("txt_ExternalReference") as ASPxTextBox;
            whDo.ExternalReference = externalReference.Text;
            ASPxDateEdit externalDate = grid_Issue.FindEditFormTemplateControl("date_ExternalDate") as ASPxDateEdit;
            whDo.ExternalDate = externalDate.Date;
            ASPxDateEdit expectedDate = grid_Issue.FindEditFormTemplateControl("date_ExceptedDate") as ASPxDateEdit;
            whDo.ExpectedDate = expectedDate.Date;
            ASPxMemo remark1 = grid_Issue.FindEditFormTemplateControl("txt_Remark1") as ASPxMemo;
            whDo.Remark1 = remark1.Text;
            ASPxMemo remark2 = grid_Issue.FindEditFormTemplateControl("txt_Remark2") as ASPxMemo;
            whDo.Remark2 = remark2.Text;
            ASPxMemo remark3 = grid_Issue.FindEditFormTemplateControl("txt_Remark3") as ASPxMemo;
            whDo.Remark3 = remark3.Text;
            ASPxMemo remark4 = grid_Issue.FindEditFormTemplateControl("txt_Remark4") as ASPxMemo;
            whDo.Remark4 = remark4.Text;
            //ASPxMemo collectFrom = grid_Issue.FindEditFormTemplateControl("memo_CollectFrom") as ASPxMemo;
            //whDo.CollectFrom = collectFrom.Text;
            ASPxMemo memo_DeliveryTo = grid_Issue.FindEditFormTemplateControl("memo_DeliveryTo") as ASPxMemo;
            whDo.DeliveryTo = memo_DeliveryTo.Text;
            ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            whDo.WareHouseId = txt_WareHouseId.Text;
            ASPxDateEdit PermitApprovalDate = grid_Issue.FindEditFormTemplateControl("date_PermitApprovalDate") as ASPxDateEdit;
            whDo.PermitApprovalDate = PermitApprovalDate.Date;
            ASPxDateEdit PermitExpiryDate = grid_Issue.FindEditFormTemplateControl("date_PermitExpiryDate") as ASPxDateEdit;
            whDo.PermitExpiryDate = PermitExpiryDate.Date;
            ASPxComboBox TptMode = grid_Issue.FindEditFormTemplateControl("cb_TptMode") as ASPxComboBox;
            whDo.TptMode = SafeValue.SafeString(TptMode.Value);
            ASPxCheckBox PalletizedInd = grid_Issue.FindEditFormTemplateControl("ack_PalletizedInd") as ASPxCheckBox;
            whDo.PalletizedInd = PalletizedInd.Checked;


            ASPxButtonEdit txt_Contractor = grid_Issue.FindEditFormTemplateControl("txt_Contractor") as ASPxButtonEdit;
            whDo.Contractor = txt_Contractor.Text;
           
            //shipment Info
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
            ASPxButtonEdit txt_Carrier = pageControl.FindControl("txt_Carrier") as ASPxButtonEdit;
            whDo.Carrier = txt_Carrier.Text;
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
            ASPxButtonEdit txt_Transport = pageControl.FindControl("txt_Transport") as ASPxButtonEdit;
            whDo.Transport = txt_Transport.Text;
            ASPxTextBox txt_DriveName = pageControl.FindControl("txt_DriveName") as ASPxTextBox;
            whDo.DriverName = txt_DriveName.Text;
            ASPxTextBox txt_DriverIC = pageControl.FindControl("txt_DriverIC") as ASPxTextBox;
            whDo.DriverIC = txt_DriverIC.Text;
            ASPxTextBox txt_DriverTel = pageControl.FindControl("txt_DriverTel") as ASPxTextBox;
            whDo.DriverTel = txt_DriverTel.Text;

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
*/

            ASPxTextBox txt_ContainerYard = pageControl.FindControl("txt_ContainerYard") as ASPxTextBox;
            whDo.ContainerYard = txt_ContainerYard.Text;
            ASPxButtonEdit txtDoNo = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
            string newDoNo = SafeValue.SafeString(txtDoNo.Text);
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                whDo.StatusCode = "USE";
                whDo.CreateBy = userId;
                whDo.CreateDateTime = DateTime.Now;
                whDo.UpdateBy = userId;
                whDo.UpdateDateTime = DateTime.Now;
                string sql = string.Format(@"select Count(*) from Wh_DO where DoNo='{0}'", newDoNo);
                int result = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                if (result == 0)
                {
                    if (txtDoNo.ReadOnly == false)
                    {
                        issueN = newDoNo;
                    }
                    whDo.DoNo = issueN;
                    Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(whDo);
                    C2Setup.SetNextNo("", "DOOUT", issueN, whDo.DoDate);
                }
                else
                {
                    throw new Exception(string.Format("Had DoNo"));
                    return ""; ;
                }

            }
            else
            {
                whDo.UpdateBy = userId;
                whDo.UpdateDateTime = DateTime.Now;
                bool isAddLog = false;
                if (whDo.DoStatus == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select DoStatus from wh_Do where DoNo='" + whDo.DoNo + "'")))
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
                    EzshipLog.Log(whDo.DoNo, "", "DoOut", whDo.DoStatus);
            }
            this.dsIssue.FilterExpression = String.Format("DoNo='{0}'", issueN);
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);
            return whDo.DoNo;
        }
        catch(Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
        return "";
    }
    protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        string refNo = "";
        if (Request.QueryString["no"] != null)
        {
            refNo = Request.QueryString["no"];
        }
        else
        {
            refNo = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "RefNo" }));
        }
        this.dsIssueDet3.FilterExpression = String.Format("DoNo='{0}' and DoType='OUT'", refNo);

        if (this.grid_Issue.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            ASPxTextBox notifyName = pageControl.FindControl("txt_NotifyName") as ASPxTextBox;
            agentName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "AgentId" }));
            notifyName.Text = EzshipHelper.GetPartyName(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "NotifyId" }));
            string oid = SafeValue.SafeString(this.grid_Issue.GetRowValues(this.grid_Issue.EditingRowVisibleIndex, new string[] { "Id" }));
            if (oid.Length > 0)
            {
                ASPxDateEdit txt_Date = this.grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
                txt_Date.BackColor = System.Drawing.Color.FromArgb(250, 240, 240, 240);
                txt_Date.ReadOnly = true;
                string sql = string.Format("select StatusCode from wh_DO where Id='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = grid_Issue.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_Void = grid_Issue.FindEditFormTemplateControl("btn_Void") as ASPxButton;
                if (closeInd == "CLS")
                {
                    btn.Text = "Open Job";
                }
                if (closeInd == "CNL")
                {
                    btn_Void.Text = "Unvoid";
                }
                //ASPxCheckBox receipt = pageControl.FindControl("ack_PopulatetoReceipt") as ASPxCheckBox;
                //ASPxCheckBox xDock = pageControl.FindControl("ack_PopulatetoXDock") as ASPxCheckBox;
                //bool receiptInd = SafeValue.SafeBool(C2.Manager.ORManager.ExecuteScalar(string.Format("select ReceiptInd from Wh_DO where Id={0}", oid)), false);
                //bool xDockInd = SafeValue.SafeBool(C2.Manager.ORManager.ExecuteScalar(string.Format("select XDockInd from Wh_DO where Id={0}", oid)), false);
                //if (receiptInd == true)
                //    receipt.Checked = true;
                //if (xDockInd == true)
                //    xDock.Checked = true;
            }
        }
    }

    protected void grid_Issue_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            if (this.dsIssuePhoto.FilterExpression == "1=0")
            {
                ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
                this.dsIssuePhoto.FilterExpression = String.Format("RefNo='{0}'", refN.Text);// 
            }
        }
        //else if (s == "Save")
        //    Save();
    }

    protected void grid_Issue_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        ASPxButtonEdit txt_WareHouseId = this.grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        string s = e.Parameters;
        if (s == "Save")
        {
            ASPxButtonEdit txt_PartyId = grid_Issue.FindEditFormTemplateControl("txt_ConsigneeCode") as ASPxButtonEdit;
            if (txt_PartyId.Text.Trim() == "")
            {
                e.Result = "Fail! ";
                return;
            }
            e.Result = Save();// new one
            return;
        }
        if (s == "Void")
        {
            #region void
            //billing
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='WH' and MastRefNo='{0}'", refN.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from Wh_DO where DoNo='" + refN.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update Wh_DO set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", refN.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(refN.Text, "", "OUT", "Unvoid");
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format("update Wh_DO set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", refN.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(refN.Text, "", "OUT", "Void");
                    e.Result = "Success";
                }
                else
                    e.Result = "Fail";
            }
            #endregion
        }
        else if (s == "CloseJob")
        {
            #region close job
            string sql = string.Format(@"select ResQty from( select (det.PickQty-det1.PickQty1) as ResQty,mast.DoNo from wh_do mast
 inner join (select sum(Qty1) as PickQty,ProductCode,DoNo from Wh_DoDet group by ProductCode,DoNo ) as det on det.DoNo=mast.DoNo
inner join (select sum(Qty1) as PickQty1,Product,DoNo from Wh_DoDet2 group by Product,DoNo) as det1 on det1.Product=det.ProductCode and det.DoNo=det1.DoNo) as tab
where DoNo='{0}'", refN.Text);
            int cnt=SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (cnt < 0)
            {
                e.Result = "Picked";    
            }
            else
            {
                string wareHouse = SafeValue.SafeString(txt_WareHouseId.Text);
                ASPxLabel closeIndStr = grid_Issue.FindEditFormTemplateControl("lb_JobStatus") as ASPxLabel;
                sql = "select StatusCode from Wh_DO where DoNo='" + refN.Text + "'";
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                if (closeInd == "CLS")
                {
                    sql = string.Format("update Wh_DO set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", refN.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(refN.Text, "", "OUT", "Open");

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
                    sql = string.Format("select count(Id) from wh_do where  len(Isnull(PermitNo,''))>0 and PermitApprovalDate>'2010-1-1' and DoNo='{0}'", refN.Text);
                    int res = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                    if (res == 0)
                    {
                        e.Result = "Permit";
                        return;
                    }
                    sql = string.Format(@"select count(id) from wh_dodet where dono='{0}' and DoType='out'", refN.Text);
                    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                    {
                        e.Result = "NO SKU Line";
                        return;
                    }
                    //lot no
                    //sql = string.Format("select count(Id) from wh_doDet where  len(Isnull(LotNo,''))=0 and DoNo='{0}'", refN.Text);
                    //res = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                    //if (res > 0)
                    //{
                    //    e.Result = "LotNo";
                    //    return;
                    //}
                    sql = string.Format("update Wh_DO set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", refN.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(refN.Text, "", "OUT", "Close");

                        TransferToCompany(refN.Text, "OUT");
                        string name = HttpContext.Current.User.Identity.Name;
                        Helper.Email.AlertProcess("rejo@cargo.ms", refN.Text, "Completed", "",name);
                        e.Result = "Success";
                    }
                    else
                        e.Result = "Fail";
                }
            }
            #endregion
        }
        else if (s == "Pick")
        {
            #region Pick
            string sql = string.Format(@"select LotNo from Wh_DoDet where DoNo='{0}'", refN.Text);
            DataTable tab = ConnectSql.GetTab(sql);
            bool isVal = false;
            for (int i = 0; i < tab.Rows.Count;i++)
            {
               string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
               sql = string.Format(@"Select BalQty from(select (tab_hand.HandQty-isnull(tab_Reserved.ReservedQty,0)) as BalQty,tab_hand.LotNo from 
(select product,LotNo,Packing ,sum(Qty1) as HandQty from wh_dodet2 where DoType='IN'  group by product,LotNo,Packing) as tab_hand
left join (select productCode as Product,LotNo,sum(Qty5) as ReservedQty from wh_doDet where DoType='Out' group by productCode,LotNo) as tab_Reserved on tab_Reserved.product=tab_hand.product and tab_Reserved.LotNo=tab_hand.LotNo)
as tab where tab.LotNo='{0}'  
", lotNo);
                int balQty =SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
                if (balQty<0)
               {
                   e.Result = "No Balance";
                   return;
               }
               else
               {
                   isVal = true;
               }
           }
            if(isVal){
                sql = string.Format(@"update Wh_DoDet set Qty1=Qty5,Qty5=0,JobStatus='Picked' where DoNo='{0}' and Qty5<>0", refN.Text);
                if (ConnectSql.ExecuteSql(sql) > -1)
                    e.Result = "Ship";
                else
                    e.Result = "Fail";
            }
            #endregion
        }
        else if (s == "Ship")
        {
            #region Ship
            string sql = string.Format(@"update Wh_DoDet set JobStatus='Shipped' where DoNo='{0}' and Qty1>0", refN.Text);
            if (ConnectSql.ExecuteSql(sql) > -1)
            {
                EzshipLog.Log(refN.Text, "", "OUT", "Shipped");

                sql = string.Format("update Wh_DO set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", refN.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Manager.ORManager.ExecuteCommand(sql);
                EzshipLog.Log(refN.Text, "", "OUT", "Close");
                //Helper.Email.AlertProcess("99915945@qq.com", refN.Text, "Completed", "");
                string name = HttpContext.Current.User.Identity.Name;
                Helper.Email.AlertProcess("rejo@cargo.ms", refN.Text, "Completed", "",name);
                e.Result = "Ship";
            }
            else
                e.Result = "Fail";
            #endregion
        }
        else if (s == "Return")
        {
            #region Return
            string sql = string.Format(@"select StatusCode from Wh_Do where DoNo='{0}'", refN.Text);
            string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            if (status == "CLS")
            {
                sql = string.Format("select count(*) from Wh_DoDet2 where DoNo='{0}' and DoType='IN'",refN.Text);
                int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
                if(cnt==0){
                    sql = string.Format(@"select * from Wh_DoDet2 where DoNo='{0}' and DoType='OUT'", refN.Text);
                    DataTable tab = ConnectSql.GetTab(sql);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        int id = SafeValue.SafeInt(tab.Rows[i]["Id"], 0);
                        sql = @"Insert Into Wh_DoDet2(DoNo, DoType,Product,LotNo,Location,Des1,Qty1,Qty2,Qty3,Price,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Packing)";
                        sql += string.Format(@"select DoNo, 'IN' as DoType,Product,LotNo,Location,Des1,Qty1,Qty2,Qty3,Price,Uom1,Uom2,Uom3,Uom4,QtyPackWhole,QtyWholeLoose,QtyLooseBase,'{1}',getdate(),'{1}',getdate(),Att1,Att2,Att3,Att4,Att5,Att6,Packing 
from Wh_DoDet2 where Id='{0}'", id, EzshipHelper.GetUserName());
                        ConnectSql.ExecuteSql(sql);

                    }
                }
                sql = string.Format(@"update Wh_Do set StatusCode='RETURN' where DoNo='{0}'", refN.Text);
                ConnectSql.ExecuteSql(sql);
                EzshipLog.Log(refN.Text, "", "OUT", "Return");
                e.Result = "Return";
            }
            else
            {
                e.Result = "Fail";
            }
            #endregion
        }
    }

    private void TransferToCompany(string orderNo, string orderType)
    {
        string url = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["WhService_ToCompany"]);
        if (url.Length == 0)
            return;
        stockApi.stock stock = new stockApi.stock();
        stock.Url = url;
        XmlDocument xmlDoc = null ;//EDI_Wh.Export_order(orderNo, orderType);
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
and wh_dodet2.doNo='{0}'", orderNo);
        ConnectSql.ExecuteSql(sql);
    }
    #endregion

    #region SKULine
    protected void grid_SKULine_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhDoDet));
        }
    }
    protected void grid_SKULine_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_DO where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsIssueDet.FilterExpression = "DoType= 'OUT' and DoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_SKULine_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
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
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["JobStatus"] = "Pending";
    }
    protected void grid_SKULine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("Sku not be null !!!");
            return;
        }
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["DoNo"] = refN.Text;
        e.NewValues["DoType"] = "OUT";
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Qty4"] = SafeValue.SafeInt(e.NewValues["Qty4"], 0);
        e.NewValues["Qty5"] = SafeValue.SafeInt(e.NewValues["Qty5"], 0);
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
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateBy"] = userId;
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
    }
    protected void grid_SKULine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("Sku not be null !!!");
            return;
        }
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Qty4"] = SafeValue.SafeInt(e.NewValues["Qty4"], 0);
        e.NewValues["Qty5"] = SafeValue.SafeInt(e.NewValues["Qty5"], 0);
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
        string status=SafeValue.SafeString(e.NewValues["JobStatus"]);
        string sku=SafeValue.SafeString(e.NewValues["ProductCode"]);
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        string sql=string.Format(@"select count(*) from Wh_DoDet2 where Product='{0}' and DoNo='{1}' and Qty1>0",sku,refN.Text);
        int cnt=SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
        if (cnt > 0 && status == "Cancel")
        {
            throw new Exception("Already Picked,Can't Cancel");
            return;
        }
    }
    protected void grid_SKULine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string product = SafeValue.SafeString(e.Values["ProductCode"]);
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        string doNo = SafeValue.SafeString(refN.Text);
        string sql = string.Format(@"delete from Wh_DoDet2 where Product='{0}' and DoNo='{1}'", product,doNo);
        C2.Manager.ORManager.ExecuteCommand(sql);
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
        
        //ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>Refresh();</script>");

    }
    protected void grid_DoDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //int transId = SafeValue.SafeInt(e.NewValues["DoInId"], 0);
        //UpdatePoDetBalQty(transId);
        UpdatePacking();
    }
    protected void grid_DoDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //int transId = SafeValue.SafeInt(e.NewValues["DoInId"], 0);
        //UpdatePoDetBalQty(transId);
        UpdatePacking();
    }
    protected void grid_DoDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        //int transId = SafeValue.SafeInt(e.Values["DoInId"], 0);
        //UpdatePoDetBalQty(transId);

    }

    private void UpdatePacking()
    {
        ASPxButtonEdit txtDoNo = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
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

    #region PickDetail

    protected void grid_PickDetail_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhDoDet2));
        }

    }
    protected void grid_PickDetail_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_DO where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsIssueDet2.FilterExpression = "(DoType='OUT' or DoType='IN') and DoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_PickDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty"] = 0;
        e.NewValues["Qty1"] = 0;
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Today;
    }
    protected void grid_PickDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["Product"] == null)
        {
            throw new Exception("Sku not be null !!!");
            return;
        }
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["DoNo"] = refN.Text;
        e.NewValues["DoType"] = "OUT";
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);

        e.NewValues["PreQty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["PreQty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["PreQty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["ProcessStatus"] = SafeValue.SafeString(e.NewValues["ProcessStatus"]);
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["BatchNo"] = SafeValue.SafeString(e.NewValues["BatchNo"]);
        e.NewValues["CustomsLot"] = SafeValue.SafeString(e.NewValues["CustomsLot"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Uom3"] = SafeValue.SafeString(e.NewValues["Uom3"]);
        e.NewValues["Uom4"] = SafeValue.SafeString(e.NewValues["Uom4"]);
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
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Today;
    }
    protected void grid_PickDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.NewValues["Product"] == null)
        {
            throw new Exception("Sku not be null !!!");
            return;
        }
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);

        bool isSch = SafeValue.SafeBool(e.NewValues["IsSch"],false);
        if (isSch)
        {
            e.NewValues["PreQty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
            e.NewValues["PreQty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
            e.NewValues["PreQty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        }
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
        //e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        //e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        //e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        //e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Today;
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        if (IsShipped(refN.Text, SafeValue.SafeString(e.NewValues["Product"])))
        {
            throw new Exception("Had Shipped,Can not change !!!");
            return;
        }
    }
    protected void grid_PickDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        string code = SafeValue.SafeString(e.Values["Product"]);
        int qty = SafeValue.SafeInt(e.Values["Qty1"],0);
        if (IsShipped(refN.Text, code))
        {
            throw new Exception("Had Shipped,Can not delete !!!");
            return;
        }
        else
        {
           int result= UpdateMast(refN.Text,code,qty);
           if (result > 0)
           {

               e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
               //string script = string.Format("<script type='text/javascript' >Refresh();</script>");
               //Response.Clear();
               //Response.Write(script);
           }
        }
    }
    protected void grid_PickDetail_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //UpdatePacking();
        //        string sku = SafeValue.SafeString(e.NewValues["Product"]);
        //        string lotNo = SafeValue.SafeString(e.NewValues["LotNo"]);
        //        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        //        string sql = string.Format(@"select wh_DoDet2.Qty1+wh_DoDet2.Qty2+wh_DoDet2.Qty3-(wh_DoDet.Qty1+wh_DoDet.Qty2+wh_DoDet.Qty3) from Wh_DoDet2 left join
        //wh_DoDet on Wh_doDet.DoNo=Wh_DoDet2.DoNo  and Wh_doDet.DoType=Wh_DoDet2.DoType and Wh_doDet.ProductCode=Wh_DoDet2.Product and  Wh_doDet.LotNo=Wh_DoDet2.LotNO
        //where wh_DoDet2.DoNo='{0}' and Wh_DoDet2.DoType='Out' and Wh_DoDet2.Product='{1}' and Wh_DoDet2.LotNO='{2}'", refN.Text, sku,lotNo);
        //        if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) != 0)
        //        {
        //            throw new Exception("Issued and Picked is not tally");
        //        }
    }
    protected void grid_PickDetail_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //UpdatePacking();
        //        string sku = SafeValue.SafeString(e.NewValues["Product"]);
        //        string lotNo = SafeValue.SafeString(e.NewValues["LotNo"]);
        //        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        //        string sql = string.Format(@"select wh_DoDet2.Qty1+wh_DoDet2.Qty2+wh_DoDet2.Qty3-(wh_DoDet.Qty1+wh_DoDet.Qty2+wh_DoDet.Qty3) from Wh_DoDet2 left join
        //        wh_DoDet on Wh_doDet.DoNo=Wh_DoDet2.DoNo  and Wh_doDet.DoType=Wh_DoDet2.DoType and Wh_doDet.ProductCode=Wh_DoDet2.Product and  Wh_doDet.LotNo=Wh_DoDet2.LotNO
        //        where wh_DoDet2.DoNo='{0}' and Wh_DoDet2.DoType='Out' and Wh_DoDet2.Product='{1}' and Wh_DoDet2.LotNO='{2}'", refN.Text, sku, lotNo);
        //        if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) != 0)
        //        {
        //            throw new Exception("Issued and Picked is not tally");
        //        }
    }
    private bool IsShipped(string doNo, string code)
    {
        bool isResult = false;
        string sql = string.Format(@"select JobStatus from wh_dodet where DoNo='{0}' and ProductCode='{1}'", doNo, code);
        string result = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (result == "Shipped")
        {
            isResult = true;
        }
        return isResult;
    }

    private int UpdateMast(string doNo,string sku,int qty)
    {
        int result = 0;
        string sql =string.Format(@"select sum(Qty1) as PQty from wh_dodet2  where DoNo='{0}' and Product='{1}'",doNo,sku);
        int totalQty = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);
        if (totalQty > 0)
        {
            sql = string.Format(@"update wh_dodet set Qty1=Qty4-({0}+Qty5),JobStatus='Pending' where DoNo='{1}' and ProductCode='{2}'", qty, doNo, sku);
            result=SafeValue.SafeInt(ConnectSql.ExecuteSql(sql),0);

            sql = string.Format(@"update wh_dodet set Qty5=Qty4-Qty1,JobStatus='Pending' where DoNo='{1}' and ProductCode='{2}'", qty, doNo, sku);
            result = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
        }
        //else if (totalQty == 0)
        //{
        //    sql = string.Format(@"update wh_dodet set Qty1=0,Qty5=Qty4,JobStatus='Pending' where DoNo='{1}' and ProductCode='{2}'", doNo, sku);
        //    result = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
        //}
        return result;
    }
    #endregion

    #region Billing

    protected void Grid_Invoice_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_DO where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsInvoice.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void Grid_Payable_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_DO where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsVoucher.FilterExpression = "MastType='WH' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    #endregion

    #region Container

    protected void Grid_Cont_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhDoDet3));
        }

    }
    protected void Grid_Cont_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["M3"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
    }
    protected void Grid_Cont_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["DoNo"] = refN.Text;
        e.NewValues["DoType"] = "OUT";
        if (e.NewValues["ContainerNo"] != null)
            e.NewValues["ContainerNo"] = e.NewValues["ContainerNo"].ToString().ToUpper().Replace("'", "");
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Today;

    }
    protected void Grid_Cont_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"]);
        e.NewValues["SealNo"] = SafeValue.SafeString(e.NewValues["SealNo"]);
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["PkgType"] = SafeValue.SafeString(e.NewValues["PkgType"]);
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateBy"] = userId;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void Grid_Cont_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select DoNo from Wh_DO where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsIssueDet3.FilterExpression = "DoType='OUT' and DoNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void Grid_Cont_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
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
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
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
        e.NewValues["FileNote"] = " ";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }



    #endregion


    #region log
    protected void grid_Log_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        this.dsLog.FilterExpression = String.Format("RefNo='{0}' and RefType='Out'", refN.Text);//
    }
    #endregion
    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select DoNo from wh_do where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "JobType='OUT' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
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
        ASPxButtonEdit doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobType"] = "OUT";
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
            string sql = "select DoNo from wh_do where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsWhPacking.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void Grid_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
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
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
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
