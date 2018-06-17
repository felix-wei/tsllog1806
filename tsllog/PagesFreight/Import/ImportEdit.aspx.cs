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

public partial class Pages_ImportEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = "Import";
        if (!IsPostBack)
        {
            Session["ImpWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["ImpWhere"] = "JobNo='" + Request.QueryString["no"].ToString() + "'";
                this.dsJob.FilterExpression = Session["ImpWhere"].ToString();
                this.txt_RefNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                this.grid_Export.AddNewRow();
            }
            else
                this.dsJob.FilterExpression = "1=0";

        }
        if (Session["ImpWhere"] != null)
        {
            this.dsJob.FilterExpression = Session["ImpWhere"].ToString();
            if (this.grid_Export.GetRow(0) != null)
                this.grid_Export.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (txt_RefNo.Text.Trim() != "")
        {
            where = " JobNo='" + SafeValue.SafeInt(txt_RefNo.Text.Trim(), 0) + "'";

            Session["ImpMastWhere"] = where;
            this.dsJob.FilterExpression = where;
            if (this.grid_Export.GetRow(0) != null)
                this.grid_Export.StartEdit(0);
        }
    }

    #region Import
    protected void grid_Export_DataSelect(object sender, EventArgs e)
    {
    }
    protected void grid_Export_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaImport));
        }
    }
    protected void grid_Export_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = EzshipHelper.GetUserName();
        string port = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        string currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["UpdateBy"] = userId;
        e.NewValues["UpdateDateTime"] = DateTime.Today;
        e.NewValues["DeliveryDate"] = DateTime.Today;

        e.NewValues["RefNo"] = Request.QueryString["masterNo"].ToString();

        e.NewValues["CustId"] = "";
        e.NewValues["CustName"] = "";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["ExpressBl"] = "N";
        e.NewValues["TruckingInd"] = "N";
        e.NewValues["DoReadyInd"] = "N";
        e.NewValues["FrCollectInd"] = "N";
        e.NewValues["CollectCurrency"] = currency;
        e.NewValues["CollectExRate"] = 1.0;
        e.NewValues["CollectAmount"] = 0;

        e.NewValues["TsInd"] = "N";
        e.NewValues["TsAgtRate"] = 0;
        e.NewValues["TsTotAgtRate"] = 0;
        e.NewValues["TsTotImpRate"] = 0;
        e.NewValues["TsImpRate"] = 0;


        e.NewValues["RateForklift"] = 0;
        e.NewValues["RateProcess"] = 0;
        e.NewValues["RateTracing"] = 0;
        e.NewValues["RateWarehouse"] = 0;
        e.NewValues["RateAdmin"] = 0;
        e.NewValues["FlagNomination"] = "Y";
        e.NewValues["ValueCurrency"] = " ";
        e.NewValues["ValueExRate"] = 0;
        e.NewValues["ValueAmt"] = 0;
        e.NewValues["GstPaidAmt"] = 0;
        e.NewValues["StatusCode"] = "USE";

        e.NewValues["Dept"] = "LCL";
        string sql = string.Format("select JobType from SeaImportRef where RefNo='{0}'", e.NewValues["RefNo"]);
        string jobType = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (jobType == "FCL")
            e.NewValues["Dept"] = "CNT";

    }
    private void SaveJob()
    {
        try
        {
            string userId = HttpContext.Current.User.Identity.Name;
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            //ASPxTextBox houseId = pageControl.FindControl("txtHouseId") as ASPxTextBox;
            ASPxTextBox expNoCtr = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
            string expN = expNoCtr.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(SeaImport), "JobNo='" + expN + "'");
            SeaImport exp = C2.Manager.ORManager.GetObject(query) as SeaImport;
            bool isNew = false;
            if (exp == null)
            {
                isNew = true;
                expN = C2Setup.GetSubNo(Request.QueryString["masterNo"].ToString(), "SI");
                exp = new SeaImport();
                exp.JobNo = expN.ToString();
                exp.CreateBy = userId;
                exp.CreateDateTime = DateTime.Now;
                exp.PackageType = "";
                exp.Weight = 0;
                exp.Volume = 0;
                exp.Qty = 0;

                exp.RefNo = Request.QueryString["masterNo"].ToString();
            }
            ASPxTextBox hblN = this.grid_Export.FindEditFormTemplateControl("txtHouseBl") as ASPxTextBox;
            exp.HblNo = hblN.Text;

            ASPxButtonEdit custID = pageControl.FindControl("txtCustId") as ASPxButtonEdit;
            exp.CustomerId = custID.Text;

            ASPxTextBox custName = pageControl.FindControl("txtCust") as ASPxTextBox;
            exp.CustomerName = custName.Text;

            ASPxButtonEdit forwardingId = pageControl.FindControl("txtForwardingId") as ASPxButtonEdit;
            exp.ForwardingId = forwardingId.Text;


            ASPxTextBox doTo = pageControl.FindControl("txtRelease") as ASPxTextBox;
            exp.DoRealeaseTo = doTo.Text;

            ASPxComboBox trucking = pageControl.FindControl("cmb_Hbl_Trucking") as ASPxComboBox;
            exp.TruckingInd = trucking.Text;

            ASPxComboBox expressBl = pageControl.FindControl("cmb_Hbl_ExpressBl") as ASPxComboBox;
            exp.ExpressBl = expressBl.Text;

            ASPxDateEdit deliveryDate = pageControl.FindControl("txtDeliveryDate") as ASPxDateEdit;
            exp.DeliveryDate = deliveryDate.Date;

            ASPxComboBox doReady = pageControl.FindControl("txtDoReady") as ASPxComboBox;
            exp.DoReadyInd = doReady.Text;
            ASPxMemo rmk = pageControl.FindControl("txt_Remark") as ASPxMemo;
            exp.Remark = rmk.Text;
            //fr collect
            ASPxComboBox frCollect = pageControl.FindControl("cmb_FrCollect") as ASPxComboBox;
            exp.FrCollectInd = frCollect.Text;
            ASPxButtonEdit currency = pageControl.FindControl("txtCurrency") as ASPxButtonEdit;
            exp.CollectCurrency = currency.Text;
            ASPxSpinEdit exRate = pageControl.FindControl("txtAmtExRate") as ASPxSpinEdit;
            exp.CollectExRate = SafeValue.SafeDecimal(exRate.Value, 0);
            ASPxSpinEdit collectAmt = pageControl.FindControl("txtAmt") as ASPxSpinEdit;
            exp.CollectAmount = SafeValue.SafeDecimal(collectAmt.Value, 0);
            ASPxButtonEdit btn_ValueCurrency = pageControl.FindControl("btn_ValueCurrency") as ASPxButtonEdit;
            exp.ValueCurrency = btn_ValueCurrency.Text;
            ASPxSpinEdit spin_ValueExRate = pageControl.FindControl("spin_ValueExRate") as ASPxSpinEdit;
            exp.ValueExRate = SafeValue.SafeDecimal(spin_ValueExRate.Value, 0);
            ASPxSpinEdit spin_ValueAmt = pageControl.FindControl("spin_ValueAmt") as ASPxSpinEdit;
            exp.ValueAmt = SafeValue.SafeDecimal(spin_ValueAmt.Value, 0);


            ASPxSpinEdit rateForklift = pageControl.FindControl("txtRateForklift") as ASPxSpinEdit;
            exp.RateForklift = SafeValue.SafeDecimal(rateForklift.Value, 0);

            ASPxSpinEdit rateProcess = pageControl.FindControl("txtRateProcess") as ASPxSpinEdit;
            exp.RateProcess = SafeValue.SafeDecimal(rateProcess.Value, 0);

            ASPxSpinEdit rateTracing = pageControl.FindControl("txtRateTracing") as ASPxSpinEdit;
            exp.RateTracing = SafeValue.SafeDecimal(rateTracing.Value, 0);

            ASPxSpinEdit rateWarehouse = pageControl.FindControl("txtRateWarehouse") as ASPxSpinEdit;
            exp.RateWarehouse = SafeValue.SafeDecimal(rateWarehouse.Value, 0);

            ASPxSpinEdit rateAdmin = pageControl.FindControl("txtRateAdmin") as ASPxSpinEdit;
            exp.RateAdmin = SafeValue.SafeDecimal(rateAdmin.Value, 0);



            ASPxComboBox flagNomination = pageControl.FindControl("txtFlagNomination") as ASPxComboBox;
            exp.FlagNomination = flagNomination.Text;


            ASPxMemo permitN = pageControl.FindControl("txt_Permit") as ASPxMemo;
            exp.PermitRmk = permitN.Text;

            //ts
            ASPxComboBox tsInd = pageControl.FindControl("cmb_Hbl_Tranship") as ASPxComboBox;
            exp.TsInd = tsInd.Text;
            //ASPxTextBox bkgRef = pageControl.FindControl("txtBkgNo") as ASPxTextBox;
            //exp.TsBkgNo = bkgRef.Text;
            //ASPxTextBox schNo = pageControl.FindControl("txtSchNo") as ASPxTextBox;
            //exp.TsSchNo = schNo.Text;
            //ASPxTextBox expRef = pageControl.FindControl("txtExpRefNo") as ASPxTextBox;
            //exp.TsRefNo = expRef.Text;
            //ASPxTextBox expNo = pageControl.FindControl("txtExpNo") as ASPxTextBox;
            //exp.TsExportNo = expNo.Text;

            ASPxButtonEdit tPod = pageControl.FindControl("txtPod") as ASPxButtonEdit;
            exp.TsPod = tPod.Text;
            ASPxButtonEdit tFin = pageControl.FindControl("txtFinalDest") as ASPxButtonEdit;
            exp.TsPortFinName = tFin.Text;
            ASPxTextBox tRemark = pageControl.FindControl("txt_TRemark") as ASPxTextBox;
            exp.TsRemark = tRemark.Text;
            ASPxDateEdit tEtd = pageControl.FindControl("date_EtdSin") as ASPxDateEdit;
            exp.TsEtd = tEtd.Date;
            ASPxTextBox tVes = pageControl.FindControl("txt_TVes") as ASPxTextBox;
            exp.TsVessel = tVes.Text;
            ASPxTextBox tVoy = pageControl.FindControl("txt_TVoy") as ASPxTextBox;
            exp.TsVoyage = tVoy.Text;
            ASPxDateEdit tEta = pageControl.FindControl("date_EtaDest") as ASPxDateEdit;
            exp.TsEta = tEta.Date;
            ASPxButtonEdit tAgt = pageControl.FindControl("txt_TAgentId") as ASPxButtonEdit;
            exp.TsAgentId = tAgt.Text;
            ASPxTextBox tColoader = pageControl.FindControl("txt_Coloader") as ASPxTextBox;
            exp.TsColoader = tColoader.Text;
            ASPxSpinEdit agRate = pageControl.FindControl("spin_AgtRate") as ASPxSpinEdit;
            exp.TsAgtRate = SafeValue.SafeDecimal(agRate.Value, 0);
            ASPxTextBox totAgRate = pageControl.FindControl("spin_TotAgtRate") as ASPxTextBox;
            exp.TsTotAgtRate = SafeValue.SafeDecimal(totAgRate.Value, 0);
            ASPxSpinEdit imRate = pageControl.FindControl("spin_ImpRate") as ASPxSpinEdit;
            exp.TsImpRate = SafeValue.SafeDecimal(imRate.Value, 0);
            ASPxTextBox totImRate = pageControl.FindControl("spin_TotImpRate") as ASPxTextBox;
            exp.TsTotImpRate = SafeValue.SafeDecimal(totImRate.Value, 0);

            ASPxMemo shipperRemark = pageControl.FindControl("txt_Hbl_Shipper2") as ASPxMemo;
            exp.SShipperRemark = shipperRemark.Text;
            ASPxMemo con2 = pageControl.FindControl("txt_Hbl_Consigee2") as ASPxMemo;
            exp.SConsigneeRemark = con2.Text;
            ASPxMemo party2 = pageControl.FindControl("txt_Hbl_Party2") as ASPxMemo;
            exp.SNotifyPartyRemark = party2.Text;
            ASPxMemo agt2 = pageControl.FindControl("txt_Hbl_Agt2") as ASPxMemo;
            exp.SAgentRemark = agt2.Text;

            ASPxTextBox haulier = pageControl.FindControl("txt_Ref_H_Hauler") as ASPxTextBox;
            exp.HaulierName = haulier.Text;
            ASPxTextBox crno = pageControl.FindControl("txt_Ref_H_CrNo") as ASPxTextBox;
            exp.HaulierCrNo = crno.Text;
            ASPxTextBox fax = pageControl.FindControl("txt_Ref_H_Fax") as ASPxTextBox;
            exp.HaulierFax = fax.Text;
            ASPxTextBox attention = pageControl.FindControl("txt_Ref_H_Attention") as ASPxTextBox;
            exp.HaulierAttention = attention.Text;

            ASPxMemo collectFrm1 = pageControl.FindControl("txt_Ref_H_CltFrm") as ASPxMemo;
            exp.HaulierCollect = collectFrm1.Text;

            ASPxMemo truckTo1 = pageControl.FindControl("txt_Ref_H_TruckTo") as ASPxMemo;
            exp.HaulierTruck = truckTo1.Text;
            ASPxDateEdit collectDate = pageControl.FindControl("date_Ref_H_CltDate") as ASPxDateEdit;
            exp.HaulierCollectDate = collectDate.Date;
            ASPxTextBox collectTime = pageControl.FindControl("date_Ref_H_CltTime") as ASPxTextBox;
            exp.HaulierCollectTime = collectTime.Text;
            ASPxMemo haulerRmk = pageControl.FindControl("txt_Ref_H_Rmk1") as ASPxMemo;
            exp.HaulierRemark = haulerRmk.Text;

            ASPxTextBox txt_DriverName = pageControl.FindControl("txt_DriverName") as ASPxTextBox;
            exp.DriverName = txt_DriverName.Text;
            ASPxTextBox txt_DriverMobile = pageControl.FindControl("txt_DriverMobile") as ASPxTextBox;
            exp.DriverMobile = txt_DriverMobile.Text;
            ASPxTextBox txt_DriverLicense = pageControl.FindControl("txt_DriverLicense") as ASPxTextBox;
            exp.DriverLicense = txt_DriverLicense.Text;
            ASPxTextBox txt_VehicleNo = pageControl.FindControl("txt_VehicleNo") as ASPxTextBox;
            exp.VehicleNo = txt_VehicleNo.Text;
            ASPxTextBox txt_VehicleType = pageControl.FindControl("txt_VehicleType") as ASPxTextBox;
            exp.VehicleType = txt_VehicleType.Text;
            ASPxTextBox me_DriverRemark = pageControl.FindControl("me_DriverRemark") as ASPxTextBox;
            exp.DriverRemark = me_DriverRemark.Text;

            ASPxDateEdit h_deliveryDate = pageControl.FindControl("date_Ref_H_DlvDate") as ASPxDateEdit;
            exp.HaulierDeliveryDate = h_deliveryDate.Date;
            ASPxTextBox h_deliveryTime = pageControl.FindControl("date_Ref_H_DlvTime") as ASPxTextBox;
            exp.HaulierDeliveryTime = h_deliveryTime.Text;
            ASPxTextBox sendTo = pageControl.FindControl("txt_H_SendTo") as ASPxTextBox;
            exp.HaulierSendTo = sendTo.Text;
            ASPxTextBox stuffBy = pageControl.FindControl("txt_H_StuffBy") as ASPxTextBox;
            exp.HaulierStuffBy = stuffBy.Text;
            ASPxTextBox coload = pageControl.FindControl("txt_H_Coload") as ASPxTextBox;
            exp.HaulierCoload = coload.Text;
            ASPxTextBox person = pageControl.FindControl("txt_H_Person") as ASPxTextBox;
            exp.HaulierPerson = person.Text;
            ASPxTextBox personTel = pageControl.FindControl("txt_H_PersonTel") as ASPxTextBox;
            exp.HaulierPersonTel = personTel.Text;


            ASPxButtonEdit cltFrmId = pageControl.FindControl("txtCltFrmId") as ASPxButtonEdit;
            exp.CltFrmId = cltFrmId.Text;
            ASPxButtonEdit deliveryToId = pageControl.FindControl("txtDeliveryToId") as ASPxButtonEdit;
            exp.DeliveryToId = deliveryToId.Text;

            ASPxButtonEdit txtPODBy = pageControl.FindControl("txtPODBy") as ASPxButtonEdit;
            exp.PODBy = txtPODBy.Text;
            ASPxDateEdit date_PodTime = pageControl.FindControl("date_PodTime") as ASPxDateEdit;
            exp.PODTime = date_PodTime.Date;
            ASPxMemo me_Remark = pageControl.FindControl("me_Remark") as ASPxMemo;
            exp.PODRemark = me_Remark.Text;
            exp.PODUpdateUser = EzshipHelper.GetUserName();
            exp.PODUpdateTime = DateTime.Now;


            //DG Cargo
            ASPxTextBox dgCategory = pageControl.FindControl("txt_DgCategory") as ASPxTextBox;
            exp.DgCategory = dgCategory.Text;
            ASPxTextBox dgImdgClass = pageControl.FindControl("txt_DgImdgClass") as ASPxTextBox;
            exp.DgImdgClass = dgImdgClass.Text;
            ASPxTextBox dgUnNo = pageControl.FindControl("txt_DgUnNo") as ASPxTextBox;
            exp.DgUnNo = dgUnNo.Text;
            ASPxMemo dgShippingName = pageControl.FindControl("memo_DgShippingName") as ASPxMemo;
            exp.DgShippingName = dgShippingName.Text;
            ASPxMemo dgTecnicalName = pageControl.FindControl("memo_DgTecnicalName") as ASPxMemo;
            exp.DgTecnicalName = dgTecnicalName.Text;
            ASPxTextBox dgMfagNo1 = pageControl.FindControl("txt_DgMfagNo1") as ASPxTextBox;
            exp.DgMfagNo1 = dgMfagNo1.Text;
            ASPxTextBox dgMfagNo2 = pageControl.FindControl("txt_DgMfagNo2") as ASPxTextBox;
            exp.DgMfagNo2 = dgMfagNo2.Text;
            ASPxTextBox dgEmsFire = pageControl.FindControl("txt_DgEmsFire") as ASPxTextBox;
            exp.DgEmsFire = dgEmsFire.Text;
            ASPxTextBox dgEmsSpill = pageControl.FindControl("txt_DgEmsSpill") as ASPxTextBox;
            exp.DgEmsSpill = dgEmsSpill.Text;
            ASPxCheckBox dgLimitedQtyInd = pageControl.FindControl("ack_DgLimitedQtyInd") as ASPxCheckBox;
            exp.DgLimitedQtyInd = (dgLimitedQtyInd.Checked) ? "Y" : "N";
            ASPxCheckBox dgExemptedQtyInd = pageControl.FindControl("ack_DgExemptedQtyInd") as ASPxCheckBox;
            exp.DgExemptedQtyInd = (dgExemptedQtyInd.Checked) ? "Y" : "N";
            ASPxTextBox dgNetWeight = pageControl.FindControl("txt_DgNetWeight") as ASPxTextBox;
            exp.DgNetWeight = dgNetWeight.Text;
            ASPxTextBox dgFlashPoint = pageControl.FindControl("txt_DgFlashPoint") as ASPxTextBox;
            exp.DgFlashPoint = dgFlashPoint.Text;
            ASPxTextBox dgRadio = pageControl.FindControl("txt_DgRadio") as ASPxTextBox;
            exp.DgRadio = dgRadio.Text;
            ASPxTextBox dgPageNo = pageControl.FindControl("txt_DgPageNo") as ASPxTextBox;
            exp.DgPageNo = dgPageNo.Text;
            ASPxTextBox dgPackingGroup = pageControl.FindControl("txt_DgPackingGroup") as ASPxTextBox;
            exp.DgPackingGroup = dgPackingGroup.Text;
            ASPxTextBox dgPackingTypeCode = pageControl.FindControl("txt_DgPackingTypeCode") as ASPxTextBox;
            exp.DgPackingTypeCode = dgPackingTypeCode.Text;
            ASPxTextBox dgTransportCode = pageControl.FindControl("txt_DgTransportCode") as ASPxTextBox;
            exp.DgTransportCode = dgTransportCode.Text;
            //certificate


            ASPxTextBox dept = this.grid_Export.FindEditFormTemplateControl("txt_Dept") as ASPxTextBox;
            exp.Dept = dept.Text;

            exp.UpdateBy = userId;
            exp.UpdateDateTime = DateTime.Now;
            if (isNew)
            {
                exp.StatusCode = "USE";
                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(exp);

                expNoCtr.Text = expN.ToString();
                this.txt_RefNo.Text = expN.ToString();
                Session["ImpWhere"] = "JobNo='" + exp.JobNo + "'";
                this.dsJob.FilterExpression = Session["ImpWhere"].ToString();
                if (this.grid_Export.GetRow(0) != null)
                    this.grid_Export.StartEdit(0);
            }
            else
            {
                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(exp);
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
    }
    protected void grid_Export_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        string refNo = "";
        if (Request.QueryString["masterNo"] != null)
        {
            refNo = Request.QueryString["masterNo"].ToString();
        }
        else
        {
            refNo = SafeValue.SafeString(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "RefNo" }));
        }
        this.dsRefCont.FilterExpression = "MkgType='Cont' and RefNo='" + refNo + "'";
        ASPxTextBox refType = this.grid_Export.FindEditFormTemplateControl("lab_RefType") as ASPxTextBox;
        refType.Text = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select RefType from SeaImportRef where RefNo='{0}'", refNo)));

        if (this.grid_Export.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox forwardName = pageControl.FindControl("txtForwadingName") as ASPxTextBox;
            forwardName.Text = EzshipHelper.GetPartyName(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "ForwardingId" }));
            ASPxTextBox tAgtName = pageControl.FindControl("txt_TAgentName") as ASPxTextBox;
            tAgtName.Text = EzshipHelper.GetPartyName(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "TsAgentId" }));

            ASPxTextBox cltFrmName = pageControl.FindControl("txtCltFrmName") as ASPxTextBox;
            cltFrmName.Text = EzshipHelper.GetPartyName(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "CltFrmId" }));
            ASPxTextBox delivertyToName = pageControl.FindControl("txtDeliveryToName") as ASPxTextBox;
            delivertyToName.Text = EzshipHelper.GetPartyName(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "DeliveryToId" }));
            string oid = SafeValue.SafeString(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            if (oid.Length > 0)
            {
                ASPxTextBox jobNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
                string sql = "select StatusCode from SeaImport where SequenceId=" + oid + "";
                string jobStatus = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                ASPxButton btn_VoidHouse = this.grid_Export.FindEditFormTemplateControl("btn_VoidHouse") as ASPxButton;
                ASPxLabel jobStatus_lab = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                if (jobStatus == "CNL")
                {
                    btn_VoidHouse.Text = "Unvoid";
                    jobStatus_lab.Text = "Void";
                }
                else
                {
                    btn_VoidHouse.Text = "Void";
                    jobStatus_lab.Text = "USE";
                }
                ASPxCheckBox dgLimitedQty = pageControl.FindControl("ack_DgLimitedQtyInd") as ASPxCheckBox;
                ASPxCheckBox dgExemptedQty = pageControl.FindControl("ack_DgExemptedQtyInd") as ASPxCheckBox;
                string dgLimitedQtyInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select DgLimitedQtyInd from SeaImport where SequenceId={0}", oid)));
                string dgExemptedQtyInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select DgExemptedQtyInd from SeaImport where SequenceId={0}", oid)));
                if (dgLimitedQtyInd == "Y")
                    dgLimitedQty.Checked = true;
                if (dgExemptedQtyInd == "Y")
                    dgExemptedQty.Checked = true;
            }
        }
    }

    protected void grid_Export_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            if (this.dsJobPhoto.FilterExpression == "1=0")
            {
                ASPxGridView grd = sender as ASPxGridView;
                ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
                ASPxTextBox HNo = pageControl.FindControl("txtHouseId") as ASPxTextBox;
                //this.dsJobPhoto.FilterExpression = "JobClass='I' and RefNo='" + refNo.Text + "'";
                string sql = "select RefNo,JobNo from SeaImport where sequenceId='" + HNo.Text + "'";
                DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                if (tab.Rows.Count > 0)
                {
                    string refNo = tab.Rows[0]["RefNo"].ToString();
                    string jobNo = tab.Rows[0]["JobNo"].ToString();
                    this.dsJobPhoto.FilterExpression = string.Format("JobClass='I' and RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                }
            }
        }
        else if (s == "Save")
        {
            SaveJob();
        }
    }

    protected void grid_Export_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "CancelBkg")
        {
            #region cancel booking
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;

            ASPxTextBox jobNoCtr = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
            ASPxTextBox bkgRef = pageControl.FindControl("txtBkgNo") as ASPxTextBox;

            string sql = string.Format("Update SeaImport Set TsVessel='', TsVoyage='', TsEtd='1900-01-01', TsEta='1900-01-01',TsSchNo='', TsBkgNo='', tsBkgId='',TsJobNo='',TsRefNo='' where JobNo='{0}'",
                jobNoCtr.Text);
            int res = Manager.ORManager.ExecuteCommand(sql);
            string user = HttpContext.Current.User.Identity.Name;
            sql = string.Format("Update SeaExport set StatusCode='CNL' where BkgRefNo='{0}'", bkgRef.Text);
            res = Manager.ORManager.ExecuteCommand(sql);
            if (res > 0)
            {
                e.Result = "Success";
            }
            else
            {
                e.Result = "Fail";
            }

            #endregion

        }
        else if (s == "ChgRef")
        {
            #region change ref
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox jobNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
            ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
            if (refN.Text.Length > 0)
            {
                string sql = string.Format("Update SeaImport set RefNo='{0}' where JobNo='{1}'", refN.Text, jobNo.Text);
                int res = Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update SeaImportMkg set RefNo='{0}' where JobNo='{1}'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XaArInvoice set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SI'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XaArInvoicedet set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SI'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XAApPayable set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SI'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XAApPayabledet set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SI'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update SeaAttachment set RefNo='{0}' where JobNo='{1}' AND JOBCLASS='I'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update SeaDn set RefNo='{0}' where JobNo='{1}' AND RefType='SI'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update seacertificate set RefNo='{0}' where JobNo='{1}' AND RefType='SI'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update seacertificatedet set RefNo='{0}' where JobNo='{1}' AND RefType='SI'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            #endregion
        }
        else if (s == "Booking")
        {
            #region create booking
            ASPxGridView grd = sender as ASPxGridView;
            ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;


            ASPxTextBox refNCtr = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxTextBox;
            ASPxTextBox jobNoCtr = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
            ASPxTextBox schNo = pageControl.FindControl("txtSchNo") as ASPxTextBox;
            ASPxTextBox bkgRef = pageControl.FindControl("txtBkgNo") as ASPxTextBox;
            ASPxTextBox bkgId = pageControl.FindControl("txtBkgId") as ASPxTextBox;

            ASPxButtonEdit tPod = pageControl.FindControl("txtPod") as ASPxButtonEdit;
            ASPxButtonEdit tFin = pageControl.FindControl("txtFinalDest") as ASPxButtonEdit;
            ASPxTextBox tRemark = pageControl.FindControl("txt_TRemark") as ASPxTextBox;
            ASPxDateEdit tEtd = pageControl.FindControl("date_EtdSin") as ASPxDateEdit;
            ASPxTextBox tVes = pageControl.FindControl("txt_TVes") as ASPxTextBox;
            ASPxTextBox tVoy = pageControl.FindControl("txt_TVoy") as ASPxTextBox;
            ASPxDateEdit tEta = pageControl.FindControl("date_EtaDest") as ASPxDateEdit;


            try
            {
                string user = HttpContext.Current.User.Identity.Name;
                string pod = tPod.Text;
                string pol = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["LocalPort"], "SGSIN");
                string polName = EzshipHelper.GetPortName(pol, "SINGAPORE");
                string podName = EzshipHelper.GetPortName(pod, "SINGAPORE");
                SeaExport exp = new SeaExport();
                #region export info

                exp.AsAgent = "N";

                string bkgNo = C2Setup.GetNextNo("ExportBooking");
                string bkgNPrefix = "SIN";
                if (pod.Length == 5)
                    bkgNPrefix += pod.Substring(2);
                exp.BkgRefNo = bkgNPrefix + bkgNo;
                exp.CollectFrom = "";
                exp.CreateBy = EzshipHelper.GetUserName();
                exp.CreateDateTime = DateTime.Now;
                exp.CustomerId = "";
                exp.ExpressBl = "N";

                exp.FrtTerm = "FP";
                exp.HaulierAttention = "";
                exp.HaulierCollect = "";
                exp.HaulierCrNo = "";
                exp.HaulierName = "";
                exp.HaulierRemark = "";
                exp.HaulierTruck = "";
                exp.HblNo = "";
                exp.ImpCharge = 0;
                exp.JobNo = C2Setup.GetSubNo(schNo.Text, "SE");
                exp.Marking = "";
                exp.PermitRmk = "";
                exp.PlaceDeliveryId = pod;
                exp.PlaceDeliveryName = podName;
                exp.PlaceDeliveryTerm = "CFS";
                exp.PlaceDischargeName = podName;
                exp.PlaceLoadingName = polName;
                exp.PlaceReceiveId = pol;
                exp.PlaceReceiveName = polName;
                exp.PlaceReceiveTerm = "CFS";
                exp.Pod = pod;
                exp.Pol = pol;
                exp.PreCarriage = "";
                exp.RefNo = schNo.Text;
                exp.Remark = "";
                exp.SAgentRemark = "";
                exp.SConsigneeRemark = "";
                exp.ShipLoadInd = "N";
                exp.ShipOnBoardDate = DateTime.Today;
                exp.ShipOnBoardInd = "N";
                exp.ShipperContact = "";
                exp.ShipperEmail = "";
                exp.ShipperFax = "";
                exp.ShipperId = "";
                exp.ShipperName = "";
                exp.ShipperTel = "";
                exp.SNotifyPartyRemark = "";
                exp.SShipperRemark = "";
                exp.StatusCode = "USE";
                exp.SurrenderBl = "N";
                exp.TsInd = "Y";
                exp.TsJobNo = jobNoCtr.Text;
                exp.UpdateBy = EzshipHelper.GetUserName();
                exp.UpdateDateTime = DateTime.Now;




                string finDest = pod;
                decimal wt = 0;
                decimal m3 = 0;
                int qty = 0;
                string pkgType = "";

                string sql = string.Format(@"SELECT TsPod, TsPortFinName, TsVessel, TsVoyage, TsColoader, TsEtd, TsEta, TsAgentId, TsRemark, TsAgtRate, TsTotAgtRate, TsImpRate, 
                      TsTotImpRate,Weight,Volume,Qty,PackageType FROM SeaImport where JobNo='{0}'", jobNoCtr.Text);
                DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                if (tab.Rows.Count > 0)
                {
                    DataRow row = tab.Rows[0];
                    pod = row["TsPod"].ToString();
                    finDest = row["TsPortFinName"].ToString();
                    wt = SafeValue.SafeDecimal(row["Weight"], 0);
                    m3 = SafeValue.SafeDecimal(row["Volume"], 0);
                    qty = SafeValue.SafeInt(row["Qty"], 0);
                    pkgType = row["PackageType"].ToString();
                }
                exp.FinDest = finDest;



                exp.Weight = wt;
                exp.Volume = m3;
                exp.Qty = qty;
                exp.PackageType = pkgType;
                #endregion

                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(exp);
                C2Setup.SetNextNo("ExportBooking", bkgNo);
                //create bkg
                #region booking and marking
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(SeaImportMkg), "JobNo='" + exp.TsJobNo + "'");
                ObjectSet set_impMkgs = C2.Manager.ORManager.GetObjectSet(query);
                for (int m = 0; m < set_impMkgs.Count; m++)
                {
                    C2.SeaImportMkg impMkg = set_impMkgs[m] as C2.SeaImportMkg;
                    C2.SeaExportMkg mkg_bkg = new SeaExportMkg();
                    mkg_bkg.ContainerNo = "";
                    mkg_bkg.ContainerType = "";
                    mkg_bkg.Description = impMkg.Description;
                    mkg_bkg.CreateDateTime = DateTime.Now;
                    mkg_bkg.JobNo = exp.JobNo;
                    mkg_bkg.Marking = impMkg.Marking;
                    mkg_bkg.MkgType = "BKG";
                    mkg_bkg.PackageType = "";
                    mkg_bkg.Qty = impMkg.Qty;
                    mkg_bkg.RefNo = exp.RefNo;
                    mkg_bkg.Remark = impMkg.Remark;
                    mkg_bkg.SealNo = "";
                    mkg_bkg.CreateBy = EzshipHelper.GetUserName();
                    mkg_bkg.Volume = impMkg.Volume;
                    mkg_bkg.Weight = impMkg.Weight;
                    C2.Manager.ORManager.StartTracking(mkg_bkg, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(mkg_bkg);

                    C2.SeaExportMkg mkg = new SeaExportMkg();
                    mkg.ContainerNo = "";
                    mkg.ContainerType = "";
                    mkg.Description = impMkg.Description;
                    mkg.CreateDateTime = DateTime.Now;
                    mkg.JobNo = exp.JobNo;
                    mkg.Marking = impMkg.Marking;
                    mkg.MkgType = "BL";
                    mkg.PackageType = "";
                    mkg.Qty = impMkg.Qty;
                    mkg.RefNo = exp.RefNo;
                    mkg.Remark = impMkg.Remark;
                    mkg.SealNo = "";
                    mkg.CreateBy = EzshipHelper.GetUserName();
                    mkg.Volume = impMkg.Volume;
                    mkg.Weight = impMkg.Weight;
                    C2.Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(mkg);
                }

                #endregion

                //update import info
                sql = string.Format("select Vessel, Voyage, Eta, Etd, EtaDest from SeaExportRef where RefNo='{0}'", schNo.Text);
                DataTable tab1 = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                if (tab1.Rows.Count > 0)
                {
                    DataRow row = tab1.Rows[0];
                    string ves = row["Vessel"].ToString();
                    string voy = row["Voyage"].ToString();
                    DateTime eta = SafeValue.SafeDate(row["Eta"], new DateTime(1900, 1, 1));
                    DateTime etd = SafeValue.SafeDate(row["Etd"], new DateTime(1900, 1, 1));
                    // DateTime etaDest = SafeValue.SafeDate(row["EtaDest"], new DateTime(1900, 1, 1));
                    sql = string.Format("Update SeaImport Set TsVessel='{0}', TsVoyage='{1}', TsEtd='{2}', TsEta='{3}',TsSchNo='{4}', TsBkgNo='{5}', TsPortFinName='{6}',tsBkgId='{7}',TsJobNo='{9}',TsRefNo='{10}' where JobNo='{8}'",
                        ves, voy, etd, eta, schNo.Text, exp.BkgRefNo, finDest, exp.SequenceId, jobNoCtr.Text, exp.JobNo, exp.RefNo);
                    int res = C2.Manager.ORManager.ExecuteCommand(sql);

                }
                e.Result = "Booking Success";
            }
            catch { e.Result = "Booking Fail,pls try again!"; }
            #endregion
        }
        if (s == "VoidHouse")
        {
            #region cancel jbo
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox jobNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
            ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
            //billing
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='SI' and MastRefNo='{0}' and JobRefNo='{1}'", refN.Text, jobNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            string sql = "select StatusCode from SeaImport where JobNo='" + jobNo.Text + "'";
            s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
            if (s == "CNL")
            {

                sql = string.Format("update SeaImport set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    EzshipLog.Log(refN.Text, jobNo.Text, "SI", "Unvoid");
                else
                    e.Result = "Fail";


                UpdateMast(jobNo.Text);

                sql = string.Format("update SeaImportMkg set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                res = Manager.ORManager.ExecuteCommand(sql);
                e.Result = "Success";
            }
            else
            {
                sql = string.Format("update SeaImport set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                    EzshipLog.Log(refN.Text, jobNo.Text, "SI", "Void");
                else
                    e.Result = "Fail";

                sql = string.Format("update SeaImportMkg set StatusCode='CNL',UpdateBy='{1}',UpdateDateTime='{2}' where JobNo='{0}'", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                res = Manager.ORManager.ExecuteCommand(sql);
                if (res < 0)
                    e.Result = "Fail";

                UpdateMast(jobNo.Text);
                e.Result = "Success";
            }
            #endregion
        }
    }

    #endregion

    #region Billing
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo,JobNo from SeaImport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            string refNo = tab.Rows[0]["RefNo"].ToString();
            string jobNo = tab.Rows[0]["JobNo"].ToString();
            this.dsInvoice.FilterExpression = string.Format("MastType='SI' and MastRefNo='{0}' and JobRefNo='{1}'", refNo, jobNo);
        }
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {

        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo,JobNo from SeaImport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            string refNo = tab.Rows[0]["RefNo"].ToString();
            string jobNo = tab.Rows[0]["JobNo"].ToString();
            this.dsVoucher.FilterExpression = string.Format("MastType='SI' and MastRefNo='{0}' and JobRefNo='{1}'", refNo, jobNo);
        }
    }
    #endregion

    #region marking
    protected void grid_Mkgs_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaImport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsMarking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_Mkgs_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaImportMkg));
        }
    }
    protected void grid_Mkgs_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Volume"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["PackageType"] = " ";
        e.NewValues["MkgType"] = "Cont";
        e.NewValues["ContainerType"] = " ";
        e.NewValues["RefStatusCode"] = "USE";
        e.NewValues["StatusCode"] = "USE";
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
        string sql = string.Format("select mast.JobType,det.ContainerNo,det.SealNo,det.ContainerType from SeaImportMkg det inner join SeaImportRef mast on mast.RefNo=det.RefNo where det.MkgType='CONT' and det.RefNo='{0}'", refN.Text);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
        {
            if (SafeValue.SafeString(tab.Rows[0]["JobType"]) == "FCL")
            {
            }
            else
            {
                e.NewValues["ContainerNo"] = SafeValue.SafeString(tab.Rows[0]["ContainerNo"]);
                e.NewValues["SealNo"] = SafeValue.SafeString(tab.Rows[0]["SealNo"]);
                e.NewValues["ContainerType"] = SafeValue.SafeString(tab.Rows[0]["ContainerType"]);
            }
        }
    }
    protected void grid_Mkgs_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["StatusCode"] = "USE";
        string jobType = EzshipHelper.GetJobType("SI", refN.Text);
        if (jobType == "FCL")
            e.NewValues["MkgType"] = "Cont";
        else
            e.NewValues["MkgType"] = "BL";

    }
    protected void grid_Mkgs_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grid_Mkgs_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_Mkgs_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
        string sql = string.Format("select JobType from SeaImportRef where RefNo='{0}'", refN.Text);
        string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "FCL");
        if (jobType.ToUpper() != "FCL")
        {
            ASPxDropDownEdit contNo = (sender as ASPxGridView).FindEditFormTemplateControl("DropDownEdit") as ASPxDropDownEdit;
            ASPxTextBox sealNo = (sender as ASPxGridView).FindEditFormTemplateControl("txt_sealN") as ASPxTextBox;
            ASPxButtonEdit contType = (sender as ASPxGridView).FindEditFormTemplateControl("txt_contType") as ASPxButtonEdit;
            contNo.ReadOnly = true;
            contNo.BackColor = System.Drawing.Color.FromArgb(250, 240, 240, 240);
            sealNo.ReadOnly = true;
            sealNo.BackColor = System.Drawing.Color.FromArgb(250, 240, 240, 240);
            contType.ReadOnly = true;
            contType.BackColor = System.Drawing.Color.FromArgb(250, 240, 240, 240);
            contType.Buttons.Clear();
        }
    }
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] keyValues = new object[grid.VisibleRowCount];
        object[] contNs = new object[grid.VisibleRowCount];
        object[] sealNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            keyValues[i] = grid.GetRowValues(i, "SequenceId");
            contNs[i] = grid.GetRowValues(i, "ContainerNo");
            sealNs[i] = grid.GetRowValues(i, "SealNo");
            contTypes[i] = grid.GetRowValues(i, "ContainerType");
        }
        e.Properties["cpContType"] = contTypes;
        e.Properties["cpSealN"] = sealNs;
        e.Properties["cpContN"] = contNs;
        e.Properties["cpKeyValues"] = keyValues;
    }
    protected void grid_Mkgs_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        UpdateMast(e.NewValues["JobNo"].ToString());
    }
    protected void grid_Mkgs_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        UpdateMast(e.NewValues["JobNo"].ToString());
    }
    protected void grid_Mkgs_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        UpdateMast(e.Values["JobNo"].ToString());
    }

    private void UpdateMast(string expN)
    {
        string sql = "select mast.jobtype,mast.RefNo from SeaImport job inner join SeaImportRef mast on job.RefNo=mast.Refno  where job.JobNo='" + expN + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 1)
        {
            string jobType = SafeValue.SafeString(tab.Rows[0][0]);
            if (jobType == "FCL")
                jobType = "CONT";
            else
                jobType = "BL";
            string refNo = SafeValue.SafeString(tab.Rows[0][1]);

            sql = string.Format(@"update SeaImport set 
Volume=(select SUM(Volume) from SeaImportMkg  where JobNo='{0}' and MkgType='{1}') 
,Weight=(select SUM(Weight) from SeaImportMkg  where JobNo='{0}' and MkgType='{1}') 
,Qty=(select SUM(Qty) from SeaImportMkg  where JobNo='{0}' and MkgType='{1}') 
,PackageType= case when (select count(*) from (select PackageType from SeaImportMkg  where JobNo='{0}' and MkgType='{1}' group by PackageType) as aa)>1 then 'Packages' else (select top 1 PackageType from SeaImportMkg  where JobNo='{0}' and MkgType='{1}') end
where JobNo='{0}'", expN, jobType);
            //update house
            C2.Manager.ORManager.ExecuteCommand(sql);
            //update master
            sql = string.Format(@"update SeaImportRef set 
Volume=(select SUM(SeaImportMkg.Volume) from SeaImportMkg inner join SeaImport on SeaImport.RefNo=SeaImportMkg.RefNo and SeaImport.JobNo=SeaImportMkg.JobNo where SeaImport.StatusCode='USE' and SeaImportMkg.RefNo='{0}' and SeaImportMkg.MkgType='{1}' )
,Weight=(select SUM(SeaImportMkg.Weight) from SeaImportMkg inner join SeaImport on SeaImport.RefNo=SeaImportMkg.RefNo and SeaImport.JobNo=SeaImportMkg.JobNo where SeaImport.StatusCode='USE' and SeaImportMkg.RefNo='{0}' and SeaImportMkg.MkgType='{1}' )
,Qty=(select SUM(SeaImportMkg.Qty) from SeaImportMkg inner join SeaImport on SeaImport.RefNo=SeaImportMkg.RefNo and SeaImport.JobNo=SeaImportMkg.JobNo where SeaImport.StatusCode='USE' and SeaImportMkg.RefNo='{0}' and SeaImportMkg.MkgType='{1}' )
,PackageType= case when (select count(*) from (select SeaImportMkg.PackageType from SeaImportMkg inner join SeaImport on SeaImport.RefNo=SeaImportMkg.RefNo and SeaImport.JobNo=SeaImportMkg.JobNo where SeaImport.StatusCode='USE' and SeaImportMkg.RefNo='{0}' and SeaImportMkg.MkgType='{1}'  group by SeaImportMkg.PackageType ) as aa)>1 then 'Packages' else ( select top 1 SeaImportMkg.PackageType from SeaImportMkg inner join SeaImport on SeaImport.RefNo=SeaImportMkg.RefNo and SeaImport.JobNo=SeaImportMkg.JobNo where SeaImport.StatusCode='USE' and SeaImportMkg.RefNo='{0}' and SeaImportMkg.MkgType='{1}') end 

 where RefNo='{0}'", refNo, jobType);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
    }
    #endregion


    #region delivery note
    protected void grid_Dn_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaImport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsDn.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_Dn_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaDn));
        }
    }
    protected void grid_Dn_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Volume"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["PackageType"] = " ";
    }
    protected void grid_Dn_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["RefType"] = "SI";
        e.NewValues["PackageType"] = SafeValue.SafeString(e.NewValues["PackageType"]);
    }
    protected void grid_Dn_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["PackageType"] = SafeValue.SafeString(e.NewValues["PackageType"]);
    }
    protected void grid_Dn_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_Dn_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
        string sql = string.Format(@"insert into SeaDn(RefNo,JobNo,RefType,Address,Marking,Description,Weight,Volume,Qty,PackageType)
select refno,jobno,'SI',(Select HaulierTruck from SeaImport where JobNo=SeaImportMkg.JobNo),Marking,Description,Weight,Volume,Qty,
(Case when (select JobType from SeaImportRef where RefNo=SeaImportMkg.RefNo)='FCL' then ContainerType else PackageType end )from seaimportmkg 
where refno='{0}' and JobNo='{1}'", refN.Text, impN.Text);
        if (ConnectSql.ExecuteSql(sql) > -1)
            e.Result = "Success";
        else
            e.Result = "Fail";
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
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox txtHouseNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        this.dsJobPhoto.FilterExpression = "JobNo='" + SafeValue.SafeString(txtHouseNo.Text, "") + "'";
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
    #region Certificate
    protected void gird_Certificate_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.SeaCertificate));
        }
    }
    protected void gird_Certificate_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PackageType"] = " ";
        e.NewValues["Qty"] = 0;
        e.NewValues["Amt"] = 0;
        e.NewValues["Description"] = " ";
        e.NewValues["Code"] = " ";
    }
    protected void gird_Certificate_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["RefType"] = "SI";
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
    }
    protected void gird_Certificate_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
    }
    protected void gird_Certificate_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gird_Certificate_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaImport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsCertificate.FilterExpression = "RefType='SI' and JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    #endregion

    #region full house job
    protected void grid_Export1_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from SeaImport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsFullJob.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";

    }
    #endregion

}
