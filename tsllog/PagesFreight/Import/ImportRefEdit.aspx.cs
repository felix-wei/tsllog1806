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
using Wilson.ORMapper;
using System.IO;
using System.Xml;

public partial class Pages_ImportRefEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string refType = SafeValue.SafeString(Request.QueryString["refType"]);
        string masterNo = SafeValue.SafeString(Request.QueryString["MasterNo"]);
        if (refType.Length > 0)
        {
            this.txt_RefType.Text = refType;
            this.txt_MasterNo.Text = masterNo;
        }
        if (!IsPostBack)
        {
            Session["ImpMast_" + this.txt_RefType.Text] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["ImpMast_" + this.txt_RefType.Text] = "RefNo='" + Request.QueryString["no"].ToString() + "' and RefType='" + refType + "'";
                this.txt_RefNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["ImpMast_" + this.txt_RefType.Text] == null)
                {
                    this.grid_ref.AddNewRow();
                }
            }
            else
                this.dsImportRef.FilterExpression = "1=0";
        }
        if (Session["ImpMast_" + this.txt_RefType.Text] != null)
        {
            this.dsImportRef.FilterExpression = Session["ImpMast_" + this.txt_RefType.Text].ToString();
            if (this.grid_ref.GetRow(0) != null)
                this.grid_ref.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //
    #region import ref
    protected void grid_ref_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaImportRef));
        }
    }
    protected void grid_ref_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        string port = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        string currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["RefNo"] = "NEW";
        e.NewValues["AgentId"] = "";
        e.NewValues["JobType"] = "LCL";
        e.NewValues["PoNo"] = this.txt_MasterNo.Text;
        e.NewValues["RefType"] = this.txt_RefType.Text;
        if (this.txt_RefType.Text == "SIF")
            e.NewValues["JobType"] = "FCL";
        else if (this.txt_RefType.Text == "SIL")
            e.NewValues["JobType"] = "LCL";
        else if (this.txt_RefType.Text == "SIC")
            e.NewValues["JobType"] = "CONSOL";
        else if (this.txt_RefType.Text == "SAI")
            e.NewValues["JobType"] = "";
        e.NewValues["RefDate"] = DateTime.Today;
        e.NewValues["Eta"] = DateTime.Today;
        e.NewValues["Etd"] = DateTime.Today;
        e.NewValues["Vessel"] = "";
        e.NewValues["Voyage"] = "";
        e.NewValues["Pol"] = "";
        e.NewValues["Pod"] = port;

        e.NewValues["CurrencyId"] = currency;
        e.NewValues["ExRate"] = "1";
        e.NewValues["CrBkgNo"] = "";
        e.NewValues["WarehouseId"] = "";
        e.NewValues["CrAgentId"] = "";
        e.NewValues["NvoccAgentId"] = "";
        e.NewValues["BkgForwardAgent"] = "";
        e.NewValues["Volume"] = "0";
        e.NewValues["Weight"] = "0";
        e.NewValues["Qty"] = "0";
        e.NewValues["PackageType"] = "";
        e.NewValues["HaulierM3"] = "0";
        e.NewValues["HaulierWt"] = "0";
        e.NewValues["HaulierQty"] = "0";
        e.NewValues["LocalCust"] = "";
    }

    protected void Save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
            string refN = SafeValue.SafeString(refNo.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(SeaImportRef), "RefNo='" + refN + "'");
            SeaImportRef expRef = C2.Manager.ORManager.GetObject(query) as SeaImportRef;
            ASPxDateEdit refDate = pageControl.FindControl("date_RefDate") as ASPxDateEdit;
            bool isNew = false;
            string refType = this.txt_RefType.Text;
            string runType = "ImportRef";//SIF/SIL/SIC/SAI
            if (refType == "SAI")
                runType = "AirImport";

            if (expRef == null)
            {
                expRef = new SeaImportRef();
                isNew = true;
                refN = C2Setup.GetNextNo(refType, runType, refDate.Date);
                expRef.RefDate = refDate.Date;
            }
            ASPxTextBox jobType = pageControl.FindControl("cbx_JobType") as ASPxTextBox;
            expRef.JobType = jobType.Text;
            expRef.RefType = this.txt_RefType.Text;
            ASPxButtonEdit agentId = pageControl.FindControl("txt_AgentId") as ASPxButtonEdit;
            expRef.AgentId = agentId.Text;
            ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
            expRef.Vessel = ves.Text;
            ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
            expRef.Voyage = voy.Text;
            ASPxDateEdit eta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
            expRef.Eta = eta.Date;
            ASPxDateEdit etd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            expRef.Etd = etd.Date;
            ASPxTextBox oceanBl = pageControl.FindControl("txt_OceanBl") as ASPxTextBox;
            expRef.OblNo = oceanBl.Text;

            ASPxButtonEdit pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
            expRef.Pol = pol.Text;
            ASPxButtonEdit pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
            expRef.Pod = pod.Text;
            ASPxButtonEdit currency = pageControl.FindControl("cbx_Currency") as ASPxButtonEdit;
            ASPxSpinEdit crExRate = pageControl.FindControl("spin_CrExRate") as ASPxSpinEdit;
            expRef.CurrencyId = currency.Text;
            expRef.ExRate = SafeValue.SafeDecimal(crExRate.Value, 1);

            ASPxTextBox crBkgRefN = pageControl.FindControl("txt_CrBkgRefN") as ASPxTextBox;
            expRef.CrBkgNo = crBkgRefN.Text;
            ASPxButtonEdit whId = pageControl.FindControl("txt_WhId") as ASPxButtonEdit;
            ASPxButtonEdit crAgentId = pageControl.FindControl("txt_CrAgentId") as ASPxButtonEdit;
            ASPxButtonEdit nvoccAgentId = pageControl.FindControl("txt_NvoccAgentId") as ASPxButtonEdit;
            ASPxButtonEdit localCust = pageControl.FindControl("txt_LocalCust") as ASPxButtonEdit;
            expRef.LocalCust = localCust.Text;
            expRef.WarehouseId = whId.Text;
            expRef.CrAgentId = crAgentId.Text;
            expRef.NvoccAgentId = nvoccAgentId.Text;
            ASPxTextBox nvoccbl = pageControl.FindControl("txt_NvoccBl") as ASPxTextBox;
            expRef.NvoccBl = nvoccbl.Text;


            ASPxMemo shipper = pageControl.FindControl("txt_Shipper2") as ASPxMemo;
            expRef.SShipperRemark = shipper.Text;
            ASPxMemo con = pageControl.FindControl("txt_Consigee2") as ASPxMemo;
            expRef.SConsigneeRemark = con.Text;
            ASPxMemo party = pageControl.FindControl("txt_Party2") as ASPxMemo;
            expRef.SNotifyPartyRemark = party.Text; ;
            ASPxMemo agt = pageControl.FindControl("txt_Agt2") as ASPxMemo;
            expRef.SAgentRemark = agt.Text;

            ASPxButtonEdit forwardAgtId = pageControl.FindControl("txt_ForwardAgentId") as ASPxButtonEdit;
            expRef.ForwardAgentId = forwardAgtId.Text;
            ASPxButtonEdit tptId = pageControl.FindControl("txt_TptId") as ASPxButtonEdit;
            expRef.TransportId = tptId.Text;

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


            ASPxMemo permit = pageControl.FindControl("txt_Ref_Permit") as ASPxMemo;
            expRef.PermitRemark = permit.Text;

            ASPxMemo rmk = pageControl.FindControl("txt_Remark") as ASPxMemo;
            expRef.Remark = rmk.Text;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {

                expRef.StatusCode = "USE";
                expRef.CreateBy = userId;
                expRef.CreateDateTime = DateTime.Now;
                expRef.RefNo = refN.ToString();
                expRef.PoNo = this.txt_MasterNo.Text;
                Manager.ORManager.StartTracking(expRef, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(expRef);
            }
            else
            {
                expRef.UpdateBy = userId;
                expRef.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(expRef, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(expRef);

            }
            if (isNew)
            {
                refNo.Text = refN.ToString();
                C2Setup.SetNextNo(refType, "ImportRef", refN, refDate.Date);
            }
            Session["ImpMast_"+this.txt_RefType.Text] = "RefNo='" + refN + "'";
            this.dsImportRef.FilterExpression = Session["ImpMast_"+this.txt_RefType.Text].ToString();
            if (this.grid_ref.GetRow(0) != null)
                this.grid_ref.StartEdit(0);

        }
        catch { }
    }
    protected void grid_ref_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_ref.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            //ASPxTextBox agtId = pageControl.FindControl("txt_AgentId") as ASPxTextBox;
            //ASPxTextBox whId = pageControl.FindControl("txt_WhId") as ASPxTextBox;
            //ASPxTextBox crAgentId = pageControl.FindControl("txt_CrAgentId") as ASPxTextBox;
            ASPxTextBox nvoccAgentId = pageControl.FindControl("txt_NvoccAgentId") as ASPxTextBox;
            ASPxTextBox agtName = pageControl.FindControl("txt_AgentName") as ASPxTextBox;
            ASPxTextBox whName = pageControl.FindControl("txt_WhName") as ASPxTextBox;
            ASPxTextBox crAgentName = pageControl.FindControl("txt_CrAgentName") as ASPxTextBox;
            ASPxTextBox nvoccAgentName = pageControl.FindControl("txt_NvoccAgentName") as ASPxTextBox;
            //ASPxTextBox forwardAgentId = pageControl.FindControl("txt_ForwardAgentId") as ASPxTextBox;
            ASPxTextBox forwardAgentName = pageControl.FindControl("txt_ForwardAgentName") as ASPxTextBox;
            ASPxTextBox localCustName = pageControl.FindControl("txt_LocalCustName") as ASPxTextBox;

            ASPxTextBox tptId = pageControl.FindControl("txt_TptId") as ASPxTextBox;
            ASPxTextBox tptName = pageControl.FindControl("txt_TptName") as ASPxTextBox;

            agtName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "AgentId" }));
            whName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "WarehouseId" }));
            crAgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "CrAgentId" }));
            nvoccAgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "NvoccAgentId" }));
            forwardAgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "ForwardAgentId" }));
            tptName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "TransportId" }));
            localCustName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "LocalCust" }));

            string oid = SafeValue.SafeString(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            if (oid.Length > 0)
            {
                ASPxDateEdit date_RefDate = pageControl.FindControl("date_RefDate") as ASPxDateEdit;
                date_RefDate.BackColor = ((DevExpress.Web.ASPxEditors.ASPxTextBox)(pageControl.FindControl("cbx_JobType"))).BackColor;
                date_RefDate.ReadOnly = true;
                string userId = HttpContext.Current.User.Identity.Name;
                ASPxLabel jobStatusStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                string sql = string.Format("select StatusCode from SeaImportRef  where SequenceId='{0}'", oid);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                ASPxButton btn = this.grid_ref.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                ASPxButton btn_VoidMaster = this.grid_ref.FindEditFormTemplateControl("btn_VoidMaster") as ASPxButton;
                if (closeInd == "CNL")
                {
                    btn_VoidMaster.Text = "Unvoid";
                    jobStatusStr.Text = "Void";
                }else if (closeInd == "CLS")
                {
                    btn.Text = "Open Job";
                    jobStatusStr.Text = "Close";
                }
                else
                {
                    btn_VoidMaster.Text = "Void";
                    jobStatusStr.Text = "USE";
                }
            }
        }
    }
    protected void grid_ref_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            if (this.dsJobPhoto.FilterExpression == "1=0")
            {
                ASPxGridView grd = sender as ASPxGridView;
                ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
                this.dsJobPhoto.FilterExpression = "JobClass='I' and RefNo='" + refNo.Text + "'";// 
            }
        }
        else if (s == "Save")
            Save();
    }

    protected void grid_ref_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;
        string userId = HttpContext.Current.User.Identity.Name;
        if (s == "VoidMaster")
        {
            #region void master
            //billing
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='SI' and MastRefNo='{0}'", refNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from SeaImportRef where RefNo='" + refNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");// closeIndStr.Text;
            if (closeInd == "CNL")
            {
                sql = string.Format("update SeaImportRef set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(refNo.Text, "", "SI", "Unvoid");
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
                    sql = string.Format("update SeaImportRef set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(refNo.Text, "", "SI", "Void");
                        e.Result = "Success";
                    }
                    else
                        e.Result = "Fail";
                }
                else
                    e.Result = "NoMatch";
            }
            #endregion
        }else if (s == "CloseJob")
        {
            #region close job
            ASPxLabel closeIndStr = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
            string sql = "select StatusCode from SeaImportRef where RefNo='" + refNo.Text + "'";
            string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
            if (closeInd == "CLS")
            {
                sql = string.Format("update SeaImportRef set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); 
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(refNo.Text, "", "SI", "Open");
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
                    sql = string.Format("update SeaImportRef set StatusCode='CLS',UpdateBy='{1}',UpdateDateTime='{2}' where RefNo='{0}'", refNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(refNo.Text, "", "SI", "Close");
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
    }
    #endregion

    #region container
    protected void Grid_RefCont_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaImportMkg));
        }
    }
    protected void Grid_RefCont_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select RefNo from SeaImportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsImpRefCont.FilterExpression = "StatusCode='USE' and MkgType='Cont' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void Grid_RefCont_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ContainerType"] = "20GP";
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
        if (e.NewValues["ContainerNo"] != null)
            e.NewValues["ContainerNo"] = e.NewValues["ContainerNo"].ToString().ToUpper().Replace("'", "");
    }
    protected void Grid_RefCont_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string lineN = e.Values["SequenceId"].ToString();
        if (lineN.Length > 0)
        {
            string sql = string.Format("delete from SeaImportMkg where SequenceId='{0}'", lineN);
            int res = Manager.ORManager.ExecuteCommand(sql);
            e.Cancel = true;
        }
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

    #region import
    protected void grid_Export_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select RefNo from SeaImportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsImport.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }

    #endregion


    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from SeaImportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsInvoice.FilterExpression = "MastType='SI' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from SeaImportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsVoucher.FilterExpression = "MastType='SI' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    #endregion

    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select RefNo from SeaImportRef where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "JobType='SI' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
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
        e.NewValues["SplitType"] = "WtM3";
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
        e.NewValues["JobType"] = "SI";
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
        string sql = string.Format("Update SeaImportRef set EstSaleAmt=(select sum(SaleLocAmt) from SeaCosting where JobType='SI' and RefNo=SeaImportRef.refNo),EstCostAmt=(select sum(CostLocAmt) from SeaCosting where JobType='SI' and RefNo=SeaImportRef.refNo) where RefNo='{0}'",refNo);
        ConnectSql.ExecuteSql(sql);
    }
    #endregion


}
