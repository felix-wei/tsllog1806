using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxDataView;
using DevExpress.Web.ASPxClasses;
using System.IO;
using System.Xml;
using Wilson.ORMapper;

public partial class Pages_ExportRefEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string refType = SafeValue.SafeString(Request.QueryString["refType"]);
        if (refType.Length > 0)
        {
            this.txt_RefType.Text = refType;
            this.txt_MasterNo.Text = masterNo;
        }
        if (!IsPostBack)
        {
            Session["ExportMast_" + this.txt_RefType.Text] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["ExportMast_" + this.txt_RefType.Text] = "RefNo='" + Request.QueryString["no"].ToString() + "' and RefType='" + refType + "'";
                this.txt_RefNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["ExportMast_" + this.txt_RefType.Text] == null)
                {
                    this.grid_ref.AddNewRow();
                }
            }
            else
                this.dsExportRef.FilterExpression = "1=0";
        }
        if (Session["ExportMast_" + this.txt_RefType.Text] != null)
        {
            this.dsExportRef.FilterExpression = Session["ExportMast_" + this.txt_RefType.Text].ToString();
            if (this.grid_ref.GetRow(0) != null)
                this.grid_ref.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region export ref
    protected void grid_ref_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaExportRef));
        }
    }
    protected void grid_ref_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        
        e.NewValues["UserId"] = userId;
        e.NewValues["EntryDate"] = DateTime.Today;
        e.NewValues["RefNo"] = "0";
        e.NewValues["ExpressBl"] = "N";
        e.NewValues["SchFaxInd"] = "N";
        e.NewValues["PoNo"] = this.txt_MasterNo.Text;
        e.NewValues["RefType"] = this.txt_RefType.Text;
        e.NewValues["JobType"] = "LCL";
        if (this.txt_RefType.Text == "SEF")
            e.NewValues["JobType"] = "FCL";
        else if (this.txt_RefType.Text == "SEL")
            e.NewValues["JobType"] = "LCL";
        else if (this.txt_RefType.Text == "SEC")
            e.NewValues["JobType"] = "CONSOL";
        else if (this.txt_RefType.Text == "SCF")
            e.NewValues["JobType"] = "FCL";
        else if (this.txt_RefType.Text == "SCL")
            e.NewValues["JobType"] = "LCL";
        else if (this.txt_RefType.Text == "SCC")
            e.NewValues["JobType"] = "CONSOL";
        else if (this.txt_RefType.Text == "SAE")
            e.NewValues["JobType"] = "";
        else if (this.txt_RefType.Text == "SAC")
            e.NewValues["JobType"] = "";
        else if (this.txt_RefType.Text == "SLT")
            e.NewValues["JobType"] = "";
        e.NewValues["RefDate"] = DateTime.Today;
        e.NewValues["Eta"] = DateTime.Today;
        e.NewValues["EtaDest"] = DateTime.Today;
        e.NewValues["Etd"] = DateTime.Today;
        e.NewValues["Pol"] = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        e.NewValues["Pod"] = "";
        e.NewValues["OblTerm"] = "FP";
        e.NewValues["StuffDate"] = DateTime.Today;
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1;
        e.NewValues["Volume"] = "0";
        e.NewValues["Weight"] = "0";
        e.NewValues["Qty"] = "0";
        e.NewValues["TransitDay"] = "0";
    }

    protected void Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox oidCtr = pageControl.FindControl("txtSequenceId") as ASPxTextBox;
            int oid = SafeValue.SafeInt(oidCtr.Text, 0);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(SeaExportRef), "SequenceId='" + oid + "'");
            SeaExportRef expRef = C2.Manager.ORManager.GetObject(query) as SeaExportRef;
            ASPxDateEdit refDate = pageControl.FindControl("date_RefDate") as ASPxDateEdit;
            string refType = this.txt_RefType.Text;
            string runType = "EXPORTREF";//SEF/SEL/SEC/ SCF/SCL/SCC/ SAE/SAC/SLT
            if (refType == "SAE")
                runType = "AirExport";
            else if (refType == "SAC")
                runType = "AirCrossTrade";
            else if (refType == "SCF"||refType=="SCL"||refType=="SCC")
                runType = "SeaCrossTrade";
            else if (refType == "SLT")
                runType = "LocalTpt";
            bool isNew = false;
            if (expRef == null)
            {
                expRef = new SeaExportRef();
                isNew = true;
                expRef.RefType = refType;

                expRef.RefNo = C2Setup.GetNextNo(refType, runType, refDate.Date);
                expRef.RefDate = refDate.Date;
            }
            ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
            expRef.Vessel = ves.Text;
            ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
            expRef.Voyage = voy.Text;

            ASPxButtonEdit pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
            expRef.Pol = pol.Text;
            ASPxButtonEdit pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
            expRef.Pod = pod.Text;
            ASPxButtonEdit portFin = pageControl.FindControl("txt_PortFinName") as ASPxButtonEdit;
            expRef.FinDest = portFin.Text;
            ASPxTextBox jobType = pageControl.FindControl("cbx_JobType") as ASPxTextBox;
            expRef.JobType = jobType.Text;

            ASPxDateEdit eta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
            expRef.Eta = eta.Date;
            ASPxDateEdit etd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            expRef.Etd = etd.Date;
            ASPxDateEdit etaDest = pageControl.FindControl("date_EtaDest") as ASPxDateEdit;
            expRef.EtaDest = etaDest.Date;


            ASPxButtonEdit crAgentId = pageControl.FindControl("txt_CrAgentId") as ASPxButtonEdit;
            expRef.CrAgentId = crAgentId.Text;
            ASPxTextBox crBkgRefN = pageControl.FindControl("txt_CrBkgRefN") as ASPxTextBox;
            expRef.CrBkgNo = crBkgRefN.Text;

            ASPxButtonEdit whId = pageControl.FindControl("txt_WhId") as ASPxButtonEdit;
            expRef.WarehouseId = whId.Text;
            ASPxDateEdit stuffD = pageControl.FindControl("date_Stuff") as ASPxDateEdit;
            expRef.StuffDate = stuffD.Date;

            ASPxButtonEdit nvoccAgentId = pageControl.FindControl("txt_NvoccAgentId") as ASPxButtonEdit;
            expRef.NvoccAgentId = nvoccAgentId.Text;
            ASPxTextBox nvoccBl = pageControl.FindControl("txt_NvoccBl") as ASPxTextBox;
            expRef.NvoccBl = nvoccBl.Text;
            ASPxSpinEdit transitDay = pageControl.FindControl("spin_Ref_TransitDay") as ASPxSpinEdit;
            expRef.TransitDay = SafeValue.SafeInt(transitDay.Value, 0);
            ASPxButtonEdit localCust = pageControl.FindControl("txt_LocalCust") as ASPxButtonEdit;
            expRef.LocalCust = localCust.Text;

            ASPxButtonEdit agentId = pageControl.FindControl("txt_AgentId") as ASPxButtonEdit;
            expRef.AgentId = agentId.Text;
            ASPxComboBox schFax = pageControl.FindControl("cbx_SchFaxInd") as ASPxComboBox;
            expRef.SchFaxInd = schFax.Text;

            ASPxTextBox oceanBl = pageControl.FindControl("txt_OceanBl") as ASPxTextBox;
            expRef.OblNo = oceanBl.Text;
            ASPxComboBox term = pageControl.FindControl("cbx_Term") as ASPxComboBox;
            expRef.OblTerm = SafeValue.SafeString(term.Value);
            ASPxComboBox express = pageControl.FindControl("cbx_ExpressBl") as ASPxComboBox;
            expRef.ExpressBl = express.Text;

            ASPxButtonEdit currency = pageControl.FindControl("cbx_Currency") as ASPxButtonEdit;
            ASPxSpinEdit crExRate = pageControl.FindControl("spin_CrExRate") as ASPxSpinEdit;
            expRef.CurrencyId = currency.Text;
            expRef.ExRate = crExRate.Number;

            ASPxMemo shipper = pageControl.FindControl("txt_Shipper2") as ASPxMemo;
            expRef.SShipperRemark = shipper.Text;
            ASPxMemo con = pageControl.FindControl("txt_Consigee2") as ASPxMemo;
            expRef.SConsigneeRemark = con.Text;
            ASPxMemo party = pageControl.FindControl("txt_Party2") as ASPxMemo;
            expRef.SNotifyPartyRemark = party.Text; ;
            ASPxMemo agt = pageControl.FindControl("txt_Agt2") as ASPxMemo;
            expRef.SAgentRemark = agt.Text;


            //haulier info
            ASPxSpinEdit haulierQty = pageControl.FindControl("spin_HaulierPkgs") as ASPxSpinEdit;
            ASPxTextBox haulierPkgType = pageControl.FindControl("txt_HaulierPkgsType") as ASPxTextBox;
            ASPxSpinEdit haulierWt = pageControl.FindControl("spin_HaulierWt") as ASPxSpinEdit;
            ASPxSpinEdit haulierM3 = pageControl.FindControl("spin_HaulierM3") as ASPxSpinEdit;
            expRef.HaulierQty = SafeValue.SafeInt(haulierQty.Value, 0);


            ASPxTextBox haulier = pageControl.FindControl("txt_Ref_H_Hauler") as ASPxTextBox;
            expRef.HaulierName = haulier.Text;
            ASPxTextBox crno = pageControl.FindControl("txt_Ref_H_CrNo") as ASPxTextBox;
            expRef.HaulierCrNo = crno.Text;
            ASPxTextBox fax = pageControl.FindControl("txt_Ref_H_Fax") as ASPxTextBox;
            expRef.HaulierFax = fax.Text;
            ASPxTextBox attention = pageControl.FindControl("txt_Ref_H_Attention") as ASPxTextBox;
            expRef.HaulierAttention = attention.Text;

            ASPxMemo collectFrm1 = pageControl.FindControl("txt_Ref_H_CltFrm") as ASPxMemo;
            expRef.HaulierCollect = collectFrm1.Text;

            ASPxMemo truckTo1 = pageControl.FindControl("txt_Ref_H_TruckTo") as ASPxMemo;
            expRef.HaulierTruck = truckTo1.Text;
            ASPxDateEdit collectDate = pageControl.FindControl("date_Ref_H_CltDate") as ASPxDateEdit;
            expRef.HaulierCollectDate = collectDate.Date;
            ASPxTextBox collectTime = pageControl.FindControl("date_Ref_H_CltTime") as ASPxTextBox;
            expRef.HaulierCollectTime = collectTime.Text;

            ASPxMemo haulerRmk = pageControl.FindControl("txt_Ref_H_Rmk1") as ASPxMemo;
            expRef.HaulierRemark = haulerRmk.Text;
            ASPxTextBox txt_DriverName = pageControl.FindControl("txt_DriverName") as ASPxTextBox;
            expRef.DriverName = txt_DriverName.Text;
            ASPxTextBox txt_DriverMobile = pageControl.FindControl("txt_DriverMobile") as ASPxTextBox;
            expRef.DriverMobile = txt_DriverMobile.Text;
            ASPxTextBox txt_DriverLicense = pageControl.FindControl("txt_DriverLicense") as ASPxTextBox;
            expRef.DriverLicense = txt_DriverLicense.Text;
            ASPxTextBox txt_VehicleNo = pageControl.FindControl("txt_VehicleNo") as ASPxTextBox;
            expRef.VehicleNo = txt_VehicleNo.Text;
            ASPxTextBox txt_VehicleType = pageControl.FindControl("txt_VehicleType") as ASPxTextBox;
            expRef.VehicleType = txt_VehicleType.Text;
            ASPxTextBox me_DriverRemark = pageControl.FindControl("me_DriverRemark") as ASPxTextBox;
            expRef.DriverRemark = me_DriverRemark.Text;

            ASPxDateEdit h_deliveryDate = pageControl.FindControl("date_Ref_H_DlvDate") as ASPxDateEdit;
            expRef.HaulierDeliveryDate = h_deliveryDate.Date;
            ASPxTextBox h_deliveryTime = pageControl.FindControl("date_Ref_H_DlvTime") as ASPxTextBox;
            expRef.HaulierDeliveryTime = h_deliveryTime.Text;
            ASPxTextBox sendTo = pageControl.FindControl("txt_H_SendTo") as ASPxTextBox;
            expRef.HaulierSendTo = sendTo.Text;
            ASPxTextBox stuffBy = pageControl.FindControl("txt_H_StuffBy") as ASPxTextBox;
            expRef.HaulierStuffBy = stuffBy.Text;
            ASPxTextBox coload = pageControl.FindControl("txt_H_Coload") as ASPxTextBox;
            expRef.HaulierCoload = coload.Text;
            ASPxTextBox person = pageControl.FindControl("txt_H_Person") as ASPxTextBox;
            expRef.HaulierPerson = person.Text;
            ASPxTextBox personTel = pageControl.FindControl("txt_H_PersonTel") as ASPxTextBox;
            expRef.HaulierPersonTel = personTel.Text;
            ASPxMemo permit = pageControl.FindControl("txt_Hbl_Permit") as ASPxMemo;
            expRef.PermitRmk = permit.Text;
            if (expRef.HaulierQty == 0)
                expRef.HaulierQty = expRef.Qty;
            expRef.HaulierPkgType = haulierPkgType.Text;
            if (SafeValue.SafeString(expRef.HaulierPkgType, "").Length == 0)
                expRef.HaulierPkgType = expRef.PackageType;
            expRef.HaulierM3 = SafeValue.SafeDecimal(haulierM3.Value, 0);
            if (expRef.HaulierM3 == 0)
                expRef.HaulierM3 = expRef.Volume;
            expRef.HaulierWt = SafeValue.SafeDecimal(haulierWt.Value, 0);
            if (expRef.HaulierWt == 0)
                expRef.HaulierWt = expRef.Weight;

            ASPxMemo rmk = pageControl.FindControl("txt_Rmk") as ASPxMemo;
            expRef.Remark = rmk.Text;
            ASPxTextBox portnetNo = pageControl.FindControl("txt_PortnetNo") as ASPxTextBox;
            expRef.PortnetNo = portnetNo.Text;
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                expRef.Pol = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["LocalPort"], "SGSIN");

                expRef.CreateBy= userId;
                expRef.CreateDateTime = DateTime.Now;
                expRef.UpdateBy = userId;
                expRef.UpdateDateTime = DateTime.Now;
                expRef.StatusCode = "USE";
                expRef.IsRef = true;
                expRef.IsSch = false;
                expRef.PoNo = this.txt_MasterNo.Text;
                Manager.ORManager.StartTracking(expRef, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(expRef);
                C2Setup.SetNextNo(refType, runType, expRef.RefNo, refDate.Date);
            }
            else
            {
                expRef.UpdateBy = userId;
                expRef.UpdateDateTime=DateTime.Now;
                Manager.ORManager.StartTracking(expRef, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(expRef);
                jobType.Enabled = false;
            }
            if (isNew)
            {
                Session["ExportMast_" + this.txt_RefType.Text] = "SequenceId=" + expRef.SequenceId;
                this.dsExportRef.FilterExpression = Session["ExportMast_" + this.txt_RefType.Text].ToString();
                if (this.grid_ref.GetRow(0) != null)
                    this.grid_ref.StartEdit(0);
            }
        }
        catch { }
    }
    protected void grid_ref_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {

        if (this.grid_ref.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxButtonEdit whId = pageControl.FindControl("txt_WhId") as ASPxButtonEdit;
            ASPxButtonEdit crAgentId = pageControl.FindControl("txt_CrAgentId") as ASPxButtonEdit;
            ASPxButtonEdit nvoccAgentId = pageControl.FindControl("txt_NvoccAgentId") as ASPxButtonEdit;
            ASPxButtonEdit agentId = pageControl.FindControl("txt_AgentId") as ASPxButtonEdit;

            ASPxTextBox whName = pageControl.FindControl("txt_WhName") as ASPxTextBox;
            ASPxTextBox crAgentName = pageControl.FindControl("txt_CrAgentName") as ASPxTextBox;
            ASPxTextBox nvoccAgentName = pageControl.FindControl("txt_NvoccAgentName") as ASPxTextBox;
            ASPxTextBox agentName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            ASPxTextBox localCustName = pageControl.FindControl("txt_LocalCustName") as ASPxTextBox;



            whName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "WarehouseId" }));
            crAgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "CrAgentId" }));
            nvoccAgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "NvoccAgentId" }));
            agentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "AgentId" }));
            localCustName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "LocalCust" }));
            string oid = SafeValue.SafeString(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            if (this.txt_RefType.Text.ToLower() == "sec")
            {
                for (int i = 0; i < pageControl.TabPages.Count; i++)
                {
                    if (pageControl.TabPages[i].Text == "Booking")
                    {
                        pageControl.TabPages[i].ClientVisible = true;
                        break;
                    }
                }
            }


            if (oid.Length > 0)
            {
                ASPxDateEdit date_RefDate = pageControl.FindControl("date_RefDate") as ASPxDateEdit;
                date_RefDate.BackColor = ((DevExpress.Web.ASPxEditors.ASPxTextBox)(pageControl.FindControl("cbx_JobType"))).BackColor;
                date_RefDate.ReadOnly = true;

                string userId = HttpContext.Current.User.Identity.Name;
                ASPxLabel jobStatusStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                string sql = string.Format("select StatusCode from SeaExportRef  where SequenceId='{0}'", oid);
                string statusCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// closeIndStr.Text;
                ASPxButton btn = this.grid_ref.FindEditFormTemplateControl("btn_RefCloseJob") as ASPxButton;
                ASPxButton btn_VoidMaster = this.grid_ref.FindEditFormTemplateControl("btn_VoidMaster") as ASPxButton;
                if (statusCode == "CNL")
                {
                    btn_VoidMaster.Text = "Unvoid";
                    jobStatusStr.Text = "Void";

                }
                else if (statusCode == "USE")
                {
                    btn.Text = "Close Job";
                    btn_VoidMaster.Text = "Void";
                    jobStatusStr.Text = "USE";
                }
                else if (statusCode == "CLS")
                {
                    btn.Text = "Open Job";
                    jobStatusStr.Text = "Closed";
                }
            }
        }
    }
    protected void grid_ref_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string refN = e.Parameters;
        ASPxGridView grd = sender as ASPxGridView;
        if (refN == "Photo")
        {
            if (this.dsJobPhoto.FilterExpression == "1=0")
            {

                ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
                this.dsJobPhoto.FilterExpression = "JobClass='E' and RefNo='" + refNo.Text + "'";// 
            }
        }
        else if (refN == "Save")
        {
            Save();
        }
        else if (refN == "Delete")
        {

            // ASPxCheckBox cb_de =(ASPxCheckBox)grid_ref.FindControl

            //if (cb_Delete.Checked == true)
            // {
            //    string sql = "delete from SeaAttachment where SequenceId="+SafeValue.SafeInt(txt_ID.Text,0);
            // }
        }
    }
    protected void grid_ref_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        ASPxTextBox masterId = pageControl.FindControl("txtSequenceId") as ASPxTextBox;
        string userId = HttpContext.Current.User.Identity.Name;
        if (s == "VoidMaster")
        {
            #region void master
            //billing
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='SE' and MastRefNo='{0}'", refNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select statusCode from SeaExportRef where RefNo='" + refNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update SeaExportRef set statusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    string refType = this.txt_RefType.Text;
                    if (refType.Length>2)
                        EzshipLog.Log(refNo.Text, "", refType.Substring(0, 2) == "SE" ? "SE" : "SCT", "Unvoid");
                    else
                        EzshipLog.Log(refNo.Text, "", refType, "Unvoid");

                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                string refType = this.txt_RefType.Text;
                bool closeByEst = EzshipHelper.GetCloseEstInd(refNo.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update SeaExportRef set statusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        if (refType.Length > 2)
                            EzshipLog.Log(refNo.Text, "", refType.Substring(0, 2) == "SE" ? "SE" : "SCT", "Unvoid");
                        else
                            EzshipLog.Log(refNo.Text, "", refType, "Unvoid");
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
        if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = pageControl.FindControl("lab_CloseInd") as ASPxLabel;
            ASPxButton btn = pageControl.FindControl("btn_RefCloseJob") as ASPxButton;
            string sql = string.Format("select statusCode from SeaExportRef  where SequenceId='{0}'", masterId.Text);
            string statusCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (statusCode == "CLS")
            {
                sql = string.Format("update SeaExportRef set statusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    string refType = this.txt_RefType.Text;
                    if (refType != null)
                        EzshipLog.Log(refNo.Text, "", refType.Substring(0, 2) == "SE" ? "SE" : "SCT", "Open");
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                string refType = this.txt_RefType.Text;
                bool closeByEst = EzshipHelper.GetCloseEstInd(refNo.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update SeaExportRef set statusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        if (refType != null)
                            EzshipLog.Log(refNo.Text, "", refType.Substring(0, 2) == "SE" ? "SE" : "SCT", "Close");
                        e.Result = "Success";
                    }
                    else
                    {
                        e.Result = "Fail";
                    }
                }
                else
                    e.Result = "NoMatch";
            }
            #endregion
        }
  }
    #endregion
    #region container
    protected void Grid_RefCont_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaExportMkg));
        }
    }
    protected void Grid_RefCont_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select RefNo from SeaExportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsExpRefCont.FilterExpression = "StatusCode='USE' and MkgType='Cont' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void Grid_RefCont_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["ContainerType"] = "20GP";
        e.NewValues["MkgType"] = "Cont";
    }
    protected void Grid_RefCont_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;

        e.NewValues["RefNo"] = refNo.Text;
        e.NewValues["JobNo"] = "0";
        if (e.NewValues["ContainerNo"]!=null)
            e.NewValues["ContainerNo"] = e.NewValues["ContainerNo"].ToString().ToUpper().Replace("'", "");
        e.NewValues["MkgType"] = "Cont";
        e.NewValues["StatusCode"] = "USE";
    }
    protected void Grid_RefCont_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"]);
        e.NewValues["SealNo"] = SafeValue.SafeString(e.NewValues["SealNo"]);
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        if(e.NewValues["ContainerNo"]!=null)
            e.NewValues["ContainerNo"] = e.NewValues["ContainerNo"].ToString().ToUpper().Replace("'", "");
        e.NewValues["MkgType"] = "Cont";
    }
    protected void Grid_RefCont_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string oid = e.Values["SequenceId"].ToString();
        if (oid.Length > 0)
        {
            string sql = string.Format("delete from SeaExportMkg where SequenceId='{0}'", oid);
            int res = Manager.ORManager.ExecuteCommand(sql);
            e.Cancel = true;
        }
    }

    #endregion

    #region Export
    protected void grid_Export_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select RefNo from SeaExportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsExport.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }


    #endregion

    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from SeaExportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        this.dsInvoice.FilterExpression = "MastType='SE' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from SeaExportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        this.dsVoucher.FilterExpression = "MastType='SE' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
    }
    #endregion

    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select RefNo from SeaExportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "JobType='SE' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaCosting));
        }
    }
    protected void grid_Cost_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SaleQty"] = 1;
        e.NewValues["CostQty"] = 1;
        e.NewValues["SalePrice"] = 0;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["SaleLocAmt"] = 0;
        e.NewValues["CostLocAmt"] = 0;
        e.NewValues["VendorId"] = " ";
        e.NewValues["ChgCode"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["SaleCurrency"] = "SGD";
        e.NewValues["SaleExRate"] = 1;
        e.NewValues["CostCurrency"] = "SGD";
        e.NewValues["CostExRate"] = 1;
        e.NewValues["JobNo"] = "0";
        e.NewValues["SplitType"] = "Set";
        e.NewValues["PayInd"] = "N";
        e.NewValues["VerifryInd"] = "N";
        e.NewValues["DocNo"] = " ";
        e.NewValues["Salesman"] = "NA";
    }
    protected void grid_Cost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox sequenceID = pageControl.FindControl("txt_SequenceId") as ASPxTextBox;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        e.NewValues["JobType"] = "SE";
        e.NewValues["RefNo"] = refNo.Text;
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        UpdateEstAmt(refNo.Text);
    }
    protected void grid_Cost_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        UpdateEstAmt(refNo.Text);
    }
    protected void grid_Cost_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox vendorName = grd.FindEditFormTemplateControl("txt_CostVendorName") as ASPxTextBox;
        vendorName.Text = EzshipHelper.GetPartyName(grd.GetRowValues(grd.EditingRowVisibleIndex, new string[] { "VendorId" }));
    }
    private void UpdateEstAmt(string refNo)
    {
        string sql = string.Format("Update SeaExportRef set EstSaleAmt=(select sum(SaleLocAmt) from SeaCosting where JobType='SE' and RefNo=SeaExportRef.refNo),EstCostAmt=(select sum(CostLocAmt) from SeaCosting where JobType='SE' and RefNo=SeaExportRef.refNo) where RefNo='{0}'", refNo);
        ConnectSql.ExecuteSql(sql);
    }
    #endregion


    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtRefNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        this.dsJobPhoto.FilterExpression = "RefNo='" + SafeValue.SafeString(txtRefNo.Text, "") + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
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


    #region cosol booking show only jobtype=consol
    protected void grid_bkg_Export_DataSelect(object sender, EventArgs e)
    {
        if (this.txt_RefType.Text.ToLower() == "sec")
        {
            ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
            if (grd.GetMasterRowKeyValue() != null)
            {
                string sql = "select RefNo from SeaExportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
                this.dsExportBkg.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
            }
        }
    }

    protected void grid_bkg_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd.EditingRowVisibleIndex > -1)
        {
            string houseId=SafeValue.SafeString(grd.GetRowValues(grd.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            string sql = string.Format("select top(1) mkg.Sequenceid,Mkg.Qty,Mkg.PackageType,Mkg.Weight,mkg.Volume,mkg.Marking,mkg.Description from seaexportmkg mkg inner join seaexport job on mkg.RefNo=job.RefNo and mkg.JobNo=job.JobNo where job.SequenceId='{0}'", houseId);
            DataTable tab = ConnectSql.GetTab(sql);
            if (tab.Rows.Count == 1)
            {
                ASPxTextBox mkgId = grd.FindEditFormTemplateControl("txtMkgId") as ASPxTextBox;
                ASPxSpinEdit wt = grd.FindEditFormTemplateControl("spin_wt2") as ASPxSpinEdit;
                ASPxSpinEdit m3 = grd.FindEditFormTemplateControl("spin_m4") as ASPxSpinEdit;
                ASPxSpinEdit qty = grd.FindEditFormTemplateControl("spin_pkg2") as ASPxSpinEdit;
                ASPxButtonEdit pkgType = grd.FindEditFormTemplateControl("txt_pkgType2") as ASPxButtonEdit;
                ASPxMemo mkg = grd.FindEditFormTemplateControl("txt_mkg2") as ASPxMemo;
                ASPxMemo des = grd.FindEditFormTemplateControl("txt_des2") as ASPxMemo;
                mkgId.Text = SafeValue.SafeString(tab.Rows[0]["SequenceId"]);
                wt.Value = SafeValue.SafeDecimal(tab.Rows[0]["Weight"], 0);
                m3.Value = SafeValue.SafeDecimal(tab.Rows[0]["Volume"], 0);
                qty.Value = SafeValue.SafeInt(tab.Rows[0]["Qty"], 0);
                pkgType.Text = SafeValue.SafeString(tab.Rows[0]["PackageType"]);
                mkg.Text = SafeValue.SafeString(tab.Rows[0]["Marking"]);
                des.Text = SafeValue.SafeString(tab.Rows[0]["Description"]);
            }
        }
    }

    protected void grid_bkg_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        e.NewValues["JobNo"] = C2Setup.GetSubNo(refNo.Text, "SE");
        e.NewValues["RefNo"] = refNo.Text;

        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["StatusCode"] = "USE";

        string sql_pod = string.Format("select Pod from SeaExportRef where RefNo='{0}'", refNo.Text);
        string pod1 = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql_pod), "SGSIN");
        string expN = C2Setup.GetNextNo("ExportBooking");
        string bkgN = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["BookingPrefix"]);
        //if (pol.Length == 5)
        //    bkgN += pol.Substring(2);
        if (pod1.Length == 5)
            bkgN += pod1.Substring(2);
        e.NewValues["BkgRefNo"] = bkgN + expN;

        string bkgN1 = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["BookingPrefix"]);
        if (pod1.Length == 5)
            bkgN1 += "/" + pod1.Substring(2);
        e.NewValues["HblNo"] = bkgN1 + "/" + expN;
    }
    protected void grid_bkg_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        //ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxTextBox mkgId = grd.FindEditFormTemplateControl("txtMkgId") as ASPxTextBox;
        ASPxSpinEdit wt = grd.FindEditFormTemplateControl("spin_wt2") as ASPxSpinEdit;
        ASPxSpinEdit m3 = grd.FindEditFormTemplateControl("spin_m4") as ASPxSpinEdit;
        ASPxSpinEdit qty = grd.FindEditFormTemplateControl("spin_pkg2") as ASPxSpinEdit;
        ASPxButtonEdit pkgType = grd.FindEditFormTemplateControl("txt_pkgType2") as ASPxButtonEdit;
        ASPxMemo mkg = grd.FindEditFormTemplateControl("txt_mkg2") as ASPxMemo;
        ASPxMemo des = grd.FindEditFormTemplateControl("txt_des2") as ASPxMemo;


        C2Setup.SetNextNo("ExportBooking", SafeValue.SafeString(e.NewValues["BkgRefNo"]));
        C2.SeaExportMkg expMkg = new C2.SeaExportMkg();

        expMkg.RefNo = refNo.Text;
        expMkg.JobNo = SafeValue.SafeString(e.NewValues["JobNo"]);
        expMkg.Qty = SafeValue.SafeInt(qty.Value, 0);
        expMkg.PackageType = pkgType.Text;
        expMkg.Volume = SafeValue.SafeDecimal(m3.Value, 0);
        expMkg.Weight = SafeValue.SafeDecimal(wt.Value, 0);
        expMkg.Description = des.Text;
        expMkg.Marking = mkg.Text;


        expMkg.MkgType = "BL";
        expMkg.ContainerNo = "";
        expMkg.SealNo = "";
        expMkg.ContainerType = "";
        expMkg.CreateBy = EzshipHelper.GetUserName();
        expMkg.CreateDateTime = DateTime.Now;
        expMkg.GrossWt = 0;
        expMkg.NetWt = 0;
        expMkg.UpdateBy = EzshipHelper.GetUserName();
        expMkg.UpdateDateTime = DateTime.Now;
        
        C2.Manager.ORManager.StartTracking(expMkg,Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(expMkg);
    }
    protected void grid_bkg_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox mkgId = grd.FindEditFormTemplateControl("txtMkgId") as ASPxTextBox;
        ASPxSpinEdit wt = grd.FindEditFormTemplateControl("spin_wt2") as ASPxSpinEdit;
        ASPxSpinEdit m3 = grd.FindEditFormTemplateControl("spin_m4") as ASPxSpinEdit;
        ASPxSpinEdit qty = grd.FindEditFormTemplateControl("spin_pkg2") as ASPxSpinEdit;
        ASPxButtonEdit pkgType = grd.FindEditFormTemplateControl("txt_pkgType2") as ASPxButtonEdit;
        ASPxMemo mkg = grd.FindEditFormTemplateControl("txt_mkg2") as ASPxMemo;
        ASPxMemo des = grd.FindEditFormTemplateControl("txt_des2") as ASPxMemo;

        string sql = string.Format("update SeaExportMkg set Qty='{1}',PackageType='{2}',Weight='{3}',Volume='{4}',Marking='{5}',Description='{6}'  where SequenceId='{0}'"
            , mkgId.Text,qty.Value,pkgType.Text,wt.Value,m3.Value,mkg.Text,des.Text);

        ConnectSql.ExecuteSql(sql);

    }
    protected void grid_bkg_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }

    #endregion
}
