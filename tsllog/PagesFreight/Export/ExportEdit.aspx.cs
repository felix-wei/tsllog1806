using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;
using DevExpress.Web.ASPxTabControl;
using System.Data;
using System.IO;
using System.Xml;
using Wilson.ORMapper;

public partial class Pages_ExportEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ExpWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["ExpWhere"] = "JobNo='" + Request.QueryString["no"].ToString() + "'";
                this.dsExport.FilterExpression = Session["ExpWhere"].ToString();
                this.txt_JobNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["bkgNo"] != null && SafeValue.SafeString(Request.QueryString["bkgNo"]).Length > 0)
            {
                Session["ExpWhere"] = "BkgRefNo='" + Request.QueryString["bkgNo"].ToString() + "'";
                this.dsExport.FilterExpression = Session["ExpWhere"].ToString();
                this.txt_BkgRefNo.Text = Request.QueryString["bkgNo"].ToString();
            }
            else if (Request.QueryString["masterNo"] != null && Request.QueryString["no"].ToString() == "0")
            {
                this.grid_Export.AddNewRow();
            }
            else
                this.dsExport.FilterExpression = "1=0";
        }
        if (Session["ExpWhere"] != null)
        {
            this.dsExport.FilterExpression = Session["ExpWhere"].ToString();
            if (this.grid_Export.GetRow(0) != null)
                this.grid_Export.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Export
    protected void grid_Export_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaExport));
        }
    }
    protected void grid_Export_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["EntryDate"] = DateTime.Today.ToString("dd/MM/yyyy");

        string refN = Request.QueryString["masterNo"].ToString();
        string sql_pod = string.Format("select Pod from SeaExportRef where RefNo='{0}'", refN);
        string pod = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql_pod), "SGSIN");
        string pol = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["LocalPort"], "SGSIN");
        string currency = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["Currency"], "SGD");
        string polName = EzshipHelper.GetPortName(pol, "SINGAPORE");
        string podName = EzshipHelper.GetPortName(pod, "SINGAPORE");

        e.NewValues["FinDest"] = pod;
        e.NewValues["AsAgent"] = "N";
        e.NewValues["TsInd"] = "N";
        e.NewValues["TsJobNo"] = "";
        //string expN = C2Setup.GetNextNo("ExportBooking");
        //string bkgN =SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["BookingPrefix"]);
        ////if (pol.Length == 5)
        ////    bkgN += pol.Substring(2);
        //if (pod.Length == 5)
        //    bkgN += pod.Substring(2);
        //e.NewValues["BkgRefNo"] = bkgN+expN;

        //string bkgN1 =SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["BookingPrefix"]);
        //if (pod.Length == 5)
        //    bkgN1 += "/"+pod.Substring(2);
        //e.NewValues["HblNo"] = bkgN1+"/"+expN;
        e.NewValues["CancelInd"] = "N";


        e.NewValues["RefNo"] = refN;
        e.NewValues["FrtTerm"] = "FP";
        e.NewValues["Pol"] = pol;
        e.NewValues["PlaceLoadingName"] = polName;
        e.NewValues["Pod"] = pod;
        e.NewValues["PlaceDischargeName"] = podName;

        e.NewValues["PlaceDeliveryId"] = pod;
        e.NewValues["PlaceDeliveryName"] = podName;
        e.NewValues["PlaceReceiveId"] = pol;
        e.NewValues["PlaceReceiveName"] = polName;
        e.NewValues["PlaceDeliveryTerm"] = "CFS";
        e.NewValues["PlaceReceiveTerm"] = "CFS";

        e.NewValues["ShipOnBoardInd"] = "N";
        e.NewValues["ShipOnBoardDate"] = DateTime.Today;
        e.NewValues["ExpressBl"] = "N";
        e.NewValues["ShipLoadInd"] = "N";
        e.NewValues["SurrenderBl"] = "N";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["CfsCharge"] = 0;
        e.NewValues["Rebate"] = 0;
        e.NewValues["ForwardingRate"] = 0;
        e.NewValues["PODTime"] = DateTime.Today;
        e.NewValues["PODUpdateUser"] = userId;
        e.NewValues["PODUpdateTime"] = DateTime.Today;
        e.NewValues["PODRemark"] = "";
        e.NewValues["ValueCurrency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ValueExRate"] = 1;
        e.NewValues["ValueAmt"] = 0;
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["Dept"] = "OUT";
    }

    protected void SaveJob()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;

            ASPxTextBox houseId = pageControl.FindControl("txtHouseId") as ASPxTextBox;
            int SequenceId = SafeValue.SafeInt(houseId.Text, 0);
            ASPxTextBox expNoCtr = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(SeaExport), "SequenceId='" + SequenceId + "'");
            SeaExport exp = C2.Manager.ORManager.GetObject(query) as SeaExport;
            bool isNew = false;
            if (SequenceId == 0)
            {
                string refN = Request.QueryString["masterNo"].ToString();
                string userId = HttpContext.Current.User.Identity.Name;
                isNew = true;
                exp = new SeaExport();
                exp.JobNo = C2Setup.GetSubNo(refN, "SE");
                exp.PackageType = "";
                exp.Weight = 0;
                exp.Volume = 0;
                exp.Qty = 0;
                exp.RefNo = refN;

                string sql_pod = string.Format("select Pod from SeaExportRef where RefNo='{0}'", refN);
                string pod1 = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql_pod), "SGSIN");

                string expN = C2Setup.GetNextNo("ExportBooking");
                string bkgN = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["BookingPrefix"]);
                //if (pol.Length == 5)
                //    bkgN += pol.Substring(2);
                if (pod1.Length == 5)
                    bkgN += pod1.Substring(2);
                exp.BkgRefNo = bkgN + expN;

                string bkgN1 = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["BookingPrefix"]);
                if (pod1.Length == 5)
                    bkgN1 += "/" + pod1.Substring(2);
                exp.HblNo = bkgN1 + "/" + expN;


            }
            else
            {
                ASPxTextBox hbl = this.grid_Export.FindEditFormTemplateControl("txtHouseBl") as ASPxTextBox;
                exp.HblNo = hbl.Text;
                ASPxTextBox bkgNo = this.grid_Export.FindEditFormTemplateControl("txtBkgNo") as ASPxTextBox;
                exp.BkgRefNo = bkgNo.Text;
            }
            ASPxButtonEdit custID = pageControl.FindControl("txtCustomerId") as ASPxButtonEdit;
            exp.CustomerId = custID.Text;

            ASPxButtonEdit btn_ValueCurrency = pageControl.FindControl("btn_ValueCurrency") as ASPxButtonEdit;
            exp.ValueCurrency = btn_ValueCurrency.Text;
            ASPxSpinEdit spin_ValueExRate = pageControl.FindControl("spin_ValueExRate") as ASPxSpinEdit;
            exp.ValueExRate = SafeValue.SafeDecimal(spin_ValueExRate.Value, 0);
            ASPxSpinEdit spin_ValueAmt = pageControl.FindControl("spin_ValueAmt") as ASPxSpinEdit;
            exp.ValueAmt = SafeValue.SafeDecimal(spin_ValueAmt.Value, 0);
            ASPxButtonEdit pol = pageControl.FindControl("txt_Hbl_Pol") as ASPxButtonEdit;
            exp.Pol = pol.Text;
            ASPxTextBox polName = pageControl.FindControl("txt_pol_name") as ASPxTextBox;
            exp.PlaceLoadingName = polName.Text;
            ASPxButtonEdit pod = pageControl.FindControl("txt_Hbl_Pod") as ASPxButtonEdit;
            exp.Pod = pod.Text;
            ASPxTextBox podName = pageControl.FindControl("txt_pod_name") as ASPxTextBox;
            exp.PlaceDischargeName = podName.Text;
            ASPxTextBox preCarriage = pageControl.FindControl("txt_Hbl_PreCarriage") as ASPxTextBox;
            exp.PreCarriage = preCarriage.Text;
            ASPxSpinEdit impChg = pageControl.FindControl("spin_Hbl_ImpChg") as ASPxSpinEdit;
            exp.ImpCharge = SafeValue.SafeDecimal(impChg.Value, 0);


            ASPxButtonEdit portDelId = pageControl.FindControl("txt_Hbl_DelId") as ASPxButtonEdit;
            exp.PlaceDeliveryId = portDelId.Text;
            ASPxTextBox portDelName = pageControl.FindControl("txt_Hbl_DelName") as ASPxTextBox;
            exp.PlaceDeliveryName = portDelName.Text;
            ASPxButtonEdit portRecId = pageControl.FindControl("txt_Hbl_RecId") as ASPxButtonEdit;
            exp.PlaceReceiveId = portRecId.Text;
            ASPxTextBox portRecName = pageControl.FindControl("txt_Hbl_RecName") as ASPxTextBox;
            exp.PlaceReceiveName = portRecName.Text;
            ASPxButtonEdit portDelTerm = pageControl.FindControl("txt_Hbl_DelTerm") as ASPxButtonEdit;
            exp.PlaceDeliveryTerm = portDelTerm.Text;
            ASPxButtonEdit portRecTerm = pageControl.FindControl("txt_Hbl_RecTerm") as ASPxButtonEdit;
            exp.PlaceReceiveTerm = portRecTerm.Text;

            ASPxComboBox shipOnBrd = pageControl.FindControl("cmb_Hbl_ShipOnBrd") as ASPxComboBox;
            exp.ShipOnBoardInd = shipOnBrd.Text;
            ASPxDateEdit shipOnBrdD = pageControl.FindControl("date_Hbl_ShipOnBrdDate") as ASPxDateEdit;
            exp.ShipOnBoardDate = shipOnBrdD.Date;
            ASPxComboBox expressBl = pageControl.FindControl("cmb_Hbl_ExpressBl") as ASPxComboBox;
            exp.ExpressBl = expressBl.Text;
            ASPxComboBox surrenderBl = pageControl.FindControl("cmb_SurrenderBl") as ASPxComboBox;
            exp.SurrenderBl = surrenderBl.Text;
            ASPxComboBox frtTerm = pageControl.FindControl("cmb_FrtTerm") as ASPxComboBox;
            exp.FrtTerm = SafeValue.SafeString(frtTerm.Value);

            ASPxMemo shipper = pageControl.FindControl("txt_Shipper2") as ASPxMemo;
            exp.SShipperRemark = shipper.Text;
            ASPxMemo con = pageControl.FindControl("txt_Consigee2") as ASPxMemo;
            exp.SConsigneeRemark = con.Text;
            ASPxMemo party = pageControl.FindControl("txt_Party2") as ASPxMemo;
            exp.SNotifyPartyRemark = party.Text; ;
            ASPxMemo agt = pageControl.FindControl("txt_Agt2") as ASPxMemo;
            exp.SAgentRemark = agt.Text;

            //haulier info
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
            //ASPxSpinEdit haulierQty = pageControl.FindControl("spin_HaulierPkgs") as ASPxSpinEdit;
            //ASPxTextBox haulierPkgType = pageControl.FindControl("txt_HaulierPkgsType") as ASPxTextBox;
            //ASPxSpinEdit haulierWt = pageControl.FindControl("spin_HaulierWt") as ASPxSpinEdit;
            //ASPxSpinEdit haulierM3 = pageControl.FindControl("spin_HaulierM3") as ASPxSpinEdit;
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









            ASPxButtonEdit finalDest = pageControl.FindControl("txtFinalDest") as ASPxButtonEdit;
            exp.FinDest = finalDest.Text;
            ASPxButtonEdit shipperId = pageControl.FindControl("txtShipperId") as ASPxButtonEdit;
            exp.ShipperId = shipperId.Text;
            ASPxTextBox shipperName = pageControl.FindControl("txtShipperName") as ASPxTextBox;
            exp.ShipperName = shipperName.Text;
            ASPxTextBox txtContact = pageControl.FindControl("txtContact") as ASPxTextBox;
            exp.ShipperContact = txtContact.Text;
            ASPxTextBox shipperFax = pageControl.FindControl("txtFax") as ASPxTextBox;
            exp.ShipperFax = shipperFax.Text;
            ASPxTextBox shipperTel = pageControl.FindControl("txtTel") as ASPxTextBox;
            exp.ShipperTel = shipperTel.Text;
            ASPxTextBox shipperEmail = pageControl.FindControl("txtEmail") as ASPxTextBox;
            exp.ShipperEmail = shipperEmail.Text;
            ASPxComboBox cbx_AsAgent = pageControl.FindControl("cbx_AsAgent") as ASPxComboBox;
            exp.AsAgent = cbx_AsAgent.Text;
            ASPxTextBox poNo = pageControl.FindControl("txt_PoNO") as ASPxTextBox;
            exp.PoNo = poNo.Text;
            ASPxMemo remark = pageControl.FindControl("txtRemarks1") as ASPxMemo;
            exp.Remark = remark.Text;
            ASPxMemo marking = pageControl.FindControl("txtMarking1") as ASPxMemo;
            exp.Marking = marking.Text;
            ASPxMemo collection = pageControl.FindControl("txtCollection1") as ASPxMemo;
            exp.CollectFrom = collection.Text;

            //exp.HaulierQty = SafeValue.SafeInt(haulierQty.Value, 0);
            //if (exp.HaulierQty == 0)
            //    exp.HaulierQty = exp.Qty;
            //exp.HaulierPkgType = haulierPkgType.Text;
            //if (SafeValue.SafeString(exp.HaulierPkgType, "").Length == 0)
            //    exp.HaulierPkgType = exp.PackageType;
            //exp.HaulierM3 = SafeValue.SafeDecimal(haulierM3.Value, 0);
            //if (exp.HaulierM3 == 0)
            //    exp.HaulierM3 = exp.Volume;
            //exp.HaulierWt = SafeValue.SafeDecimal(haulierWt.Value, 0);
            //if (exp.HaulierWt == 0)
            //    exp.HaulierWt = exp.Weight;

            ASPxMemo rmk = pageControl.FindControl("txt_Hbl_Permit") as ASPxMemo;
            exp.PermitRmk = rmk.Text;
            ASPxButtonEdit quoteNo = pageControl.FindControl("txtQuoteNo") as ASPxButtonEdit;
            exp.QuoteNo = quoteNo.Text;


            ASPxButtonEdit txtPODBy = pageControl.FindControl("txtPODBy") as ASPxButtonEdit;
            exp.PODBy = txtPODBy.Text;
            ASPxDateEdit date_PodTime = pageControl.FindControl("date_PodTime") as ASPxDateEdit;
            exp.PODTime = date_PodTime.Date;
            ASPxMemo me_Remark = pageControl.FindControl("me_Remark") as ASPxMemo;
            exp.PODRemark = me_Remark.Text;
            exp.PODUpdateUser = EzshipHelper.GetUserName();
            exp.PODUpdateTime = DateTime.Now;
            ASPxTextBox dept = this.grid_Export.FindEditFormTemplateControl("txt_Dept") as ASPxTextBox;
            exp.Dept = dept.Text;


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


            if (isNew)
            {
                exp.CreateBy = EzshipHelper.GetUserName();
                exp.CreateDateTime = DateTime.Now;
                exp.UpdateBy = EzshipHelper.GetUserName();
                exp.UpdateDateTime = DateTime.Now;
                exp.StatusCode = "USE";
                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(exp);


                string expN = C2Setup.GetNextNo("ExportBooking");
                string bkgN = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["BookingPrefix"]);
                if (pod.Text.Length == 5)
                    bkgN += pod.Text.Substring(2);
                if (bkgN + expN == exp.BkgRefNo)
                {
                    C2Setup.SetNextNo("ExportBooking", expN);
                }
                houseId.Text = exp.SequenceId.ToString();
                expNoCtr.Text = exp.JobNo;
                this.txt_JobNo.Text = exp.JobNo;

                UpdatePoNo(exp.RefNo);
                Session["ExpWhere"] = "SequenceId='" + exp.SequenceId + "'";
                this.dsExport.FilterExpression = Session["ExpWhere"].ToString();
                if (this.grid_Export.GetRow(0) != null)
                    this.grid_Export.StartEdit(0);
            }
            else
            {
                exp.UpdateBy = EzshipHelper.GetUserName();
                exp.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(exp);
                UpdatePoNo(exp.RefNo);
            }
        }
        catch
        { }
    }

    private void UpdatePoNo(string refNo)
    {
        string sql_sum = "SELECT PoNo FROM SeaExport where RefNo='" + refNo + "'";

        DataTable tab = Manager.ORManager.GetDataSet(sql_sum).Tables[0];
        string poNos = "";
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (SafeValue.SafeString(tab.Rows[i]["PoNo"]).Length > 0)
                poNos += SafeValue.SafeString(tab.Rows[i]["PoNo"]) + ";";
        }
        sql_sum = string.Format("Update SeaExportRef set PoNo='{1}' where RefNo='{0}'", refNo, poNos);
        C2.Manager.ORManager.ExecuteCommand(sql_sum);
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
        this.dsRefCont.FilterExpression = "RefNo='" + refNo + "' and MkgType='Cont'";
        ASPxTextBox refType = this.grid_Export.FindEditFormTemplateControl("lab_RefType") as ASPxTextBox;
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        refType.Text = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select RefType from SeaExportRef where RefNo='{0}'", refNo)));
        string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select JobType from SeaExportRef where RefNo='{0}'", refNo)));
        if (jobType.ToUpper() == "FCL")
        {
            ASPxButton cltFrom = pageControl.FindControl("ASPxButton11") as ASPxButton;
            cltFrom.Text = "Collect Yard";
        }
        if (this.grid_Export.EditingRowVisibleIndex > -1)
        {

            ASPxTextBox custName = pageControl.FindControl("txtCustomer") as ASPxTextBox;
            custName.Text = EzshipHelper.GetPartyName(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "CustomerId" }));

            string oid = SafeValue.SafeString(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            if (oid.Length > 0)
            {
                ASPxLabel lbl_StatusCode = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                string sql = "select StatusCode from SeaExport where SequenceId=" + oid + "";
                string jobStatus = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                ASPxButton btn_Void = this.grid_Export.FindEditFormTemplateControl("btn_CancelBkg") as ASPxButton;
                if (jobStatus == "CNL")
                {
                    lbl_StatusCode.Text = "Void";
                    btn_Void.Text = "UnVoid";
                }
                else
                {
                    lbl_StatusCode.Text = "USE";
                    btn_Void.Text = "Void";
                }

                ASPxCheckBox dgLimitedQty = pageControl.FindControl("ack_DgLimitedQtyInd") as ASPxCheckBox;
                ASPxCheckBox dgExemptedQty = pageControl.FindControl("ack_DgExemptedQtyInd") as ASPxCheckBox;
                string dgLimitedQtyInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select DgLimitedQtyInd from SeaExport where SequenceId={0}", oid)));
                string dgExemptedQtyInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select DgExemptedQtyInd from SeaExport where SequenceId={0}", oid)));
                if (dgLimitedQtyInd == "Y")
                    dgLimitedQty.Checked = true;
                if (dgExemptedQtyInd == "Y")
                    dgExemptedQty.Checked = true;
            }

        }
    }

    protected void grid_Export_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox jobNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        if (s == "ChgRef")
        {
            #region changer refno
            if (refN.Text.Length > 0)
            {
                string sql = string.Format("Update SeaExport set RefNo='{0}' where JobNo='{1}'", refN.Text, jobNo.Text);
                int res = Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update SeaExportMkg set RefNo='{0}' where JobNo='{1}'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XaArInvoice set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SE'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XaArInvoiceDet set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SE'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XAApPayable set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SE'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update XAApPayableDet set MastRefNo='{0}' where JobRefNo='{1}' AND MASTTYPE='SE'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update SeaAttachment set RefNo='{0}' where JobNo='{1}' AND JOBCLASS='E'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                //export detail
                sql = string.Format("Update SeaExportDetail set RefNo='{0}' where JobNo='{1}' ", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                //certificate
                sql = string.Format("Update seacertificate set RefNo='{0}' where JobNo='{1}' AND RefType='SE'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                sql = string.Format("Update seacertificatedet set RefNo='{0}' where JobNo='{1}' AND RefType='SE'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);

                //certificate
                sql = string.Format("Update seacommercial set RefNo='{0}' where JobNo='{1}' AND RefType='SE'", refN.Text, jobNo.Text);
                Manager.ORManager.ExecuteCommand(sql);
                //packing
                sql = string.Format("Update seaPacking set RefNo='{0}' where JobNo='{1}' AND RefType='SE'", refN.Text, jobNo.Text);
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
        else if (s == "CancelBkg")
        {
            //billing
            #region cancel jbo
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='SE' and MastRefNo='{0}' and JobRefNo='{1}'", refN.Text, jobNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            string sql = "select StatusCode from SeaExport where JobNo='" + jobNo.Text + "'";
            s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
            if (s == "CNL")
            {

                sql = sql = string.Format("Update SeaExport set StatusCode='USE' ,UpdateBy='{2}',UpdateDateTime='{3}' where RefNo='{0}' and JobNo='{1}'", refN.Text, jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    sql = string.Format("select RefType from SeaExportRef ref inner join SeaExport job on ref.RefNo=job.RefNo where JobNo='{0}'", jobNo.Text);
                    string refType = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql));
                    if (refType.Length > 2)
                        EzshipLog.Log(refN.Text, jobNo.Text, refType.Substring(0, 2) == "SE" ? "SE" : "SCT", "Unvoid");
                    else
                        EzshipLog.Log(refN.Text, jobNo.Text, refType, "Unvoid");

                    sql = string.Format("Update SeaExportMkg set StatusCode='USE' ,UpdateBy='{2}',UpdateDateTime='{3}' where RefNo='{0}' and JobNo='{1}'", refN.Text, jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now);
                    res = Manager.ORManager.ExecuteCommand(sql);
                    if (res < 0)
                        e.Result = "Fail";

                    UpdateMast(jobNo.Text);
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format("Update SeaExport set StatusCode='CNL' ,UpdateBy='{2}',UpdateDateTime='{3}' where RefNo='{0}' and JobNo='{1}'", refN.Text, jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now); int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    sql = string.Format("select RefType from SeaExportRef ref inner join SeaExport job on ref.RefNo=job.RefNo where JobNo='{0}'", jobNo.Text);
                    string refType = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql));
                    if (refType != null)
                        EzshipLog.Log(refN.Text, jobNo.Text, refType.Substring(0, 2) == "SE" ? "SE" : "SCT", "Void");

                    sql = string.Format("Update SeaExportMkg set StatusCode='CNL' ,UpdateBy='{2}',UpdateDateTime='{3}' where RefNo='{0}' and JobNo='{1}'", refN.Text, jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now);
                    res = Manager.ORManager.ExecuteCommand(sql);
                    if (res < 0)
                        e.Result = "Fail";

                    UpdateMast(jobNo.Text);
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            #endregion
        }
        else if (s == "Transfer")
        {
            //billing
            #region Transfer jbo
            ASPxTextBox refType = this.grid_Export.FindEditFormTemplateControl("lab_RefType") as ASPxTextBox;
            string sql = "";
            if (refType.Text == "SEF" || refType.Text == "SCF")
            {
                sql = string.Format(@"Insert into SeaExportMkg (RefNo,JobNo,GrossWt,NetWt,Weight,Volume,Qty,PackageType,ContainerType,MkgType,CreateBy,CreateDateTime,UpdateBy,UpdateDateTime)
select RefNo,JobNo,GrossWt,NetWt,Weight,Volume,Qty,'',PackageType,'CONT','{1}','{2}','{1}','{2}' from SeaExportMkg where jobno='{0}' and mkgtype='BKG'
", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                C2.Manager.ORManager.ExecuteCommand(sql);
                UpdateMast(jobNo.Text);
                e.Result = "Success";
            }
            else
            {
                sql = string.Format(@"Insert into SeaExportMkg (RefNo,JobNo,GrossWt,NetWt,Weight,Volume,Qty,PackageType,ContainerType,MkgType,CreateBy,CreateDateTime,UpdateBy,UpdateDateTime)
select RefNo,JobNo,GrossWt,NetWt,Weight,Volume,Qty,PackageType,ContainerType,'BL','{1}','{2}','{1}','{2}' from SeaExportMkg where jobno='{0}' and mkgtype='BKG'
", jobNo.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                C2.Manager.ORManager.ExecuteCommand(sql);
                UpdateMast(jobNo.Text);
                e.Result = "Success";
            }

            #endregion
        }
    }
    protected void grid_Export_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            //if (this.dsJobPhoto.FilterExpression == "1=0")
            //{
            //    ASPxGridView grd = sender as ASPxGridView;
            //    ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            //    ASPxTextBox HNo = pageControl.FindControl("txtHouseId") as ASPxTextBox;
            //    //this.dsJobPhoto.FilterExpression = "JobClass='E' and RefNo='" + HNo.Text + "'";// 
            //    string sql = "select RefNo,JobNo from SeaExport where sequenceId='" + HNo.Text + "'";
            //    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            //    if (tab.Rows.Count > 0)
            //    {
            //        string refNo = tab.Rows[0]["RefNo"].ToString();
            //        string jobNo = tab.Rows[0]["JobNo"].ToString();
            //        this.dsJobPhoto.FilterExpression = string.Format("JobClass='E' and RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
            //    }
            //}
        }
        else if (s == "Save")
            SaveJob();

    }

    #endregion

    #region Billing
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo,JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            string refNo = tab.Rows[0]["RefNo"].ToString();
            string jobNo = tab.Rows[0]["JobNo"].ToString();
            this.dsInvoice.FilterExpression = string.Format("MastType='SE' and MastRefNo='{0}' and JobRefNo='{1}'", refNo, jobNo);
        }
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo,JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            string refNo = tab.Rows[0]["RefNo"].ToString();
            string jobNo = tab.Rows[0]["JobNo"].ToString();
            this.dsVoucher.FilterExpression = string.Format("MastType='SE' and MastRefNo='{0}' and JobRefNo='{1}'", refNo, jobNo);
        }
    }

    #endregion

    #region marking
    protected void grid_Mkgs_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string jobNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        string sql_jobType = "select mast.jobtype from seaexport job inner join SeaExportRef mast on job.RefNo=mast.Refno  where job.sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_jobType), "");
        if (jobType == "FCL")
            this.dsMarking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and MkgType='Cont'";
        else
            this.dsMarking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and MkgType='Bl'";

    }
    protected void grid_Mkgs_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaExportMkg));
        }
    }
    protected void grid_Mkgs_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        string sql = string.Format("select JobType from SeaExportRef where RefNo='{0}'", refN.Text);
        string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "FCL");
        if (jobType.ToUpper() != "FCL")
        {
            ASPxDropDownEdit contNo = (sender as ASPxGridView).FindEditFormTemplateControl("DropDownEdit2") as ASPxDropDownEdit;
            ASPxTextBox sealNo = (sender as ASPxGridView).FindEditFormTemplateControl("txt_sealN2") as ASPxTextBox;
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
    protected void grid_Mkgs_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Volume"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["GrossWt"] = 0;
        e.NewValues["NetWt"] = 0;
        e.NewValues["PackageType"] = " ";
        e.NewValues["ContainerType"] = " ";
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        string sql = string.Format("select mast.JobType,det.ContainerNo,det.SealNo,det.ContainerType from SeaExportMkg det inner join SeaExportRef mast on mast.RefNo=det.RefNo where det.MkgType='CONT' and det.RefNo='{0}'", refN.Text);
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
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;

        e.NewValues["StatusCode"] = "USE";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        string jobType = EzshipHelper.GetJobType("SE", refN.Text);
        if (jobType == "FCL")
            e.NewValues["MkgType"] = "CONT";
        else
            e.NewValues["MkgType"] = "BL";

        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["PackageType"] = SafeValue.SafeString(e.NewValues["PackageType"]);
    }
    protected void grid_Mkgs_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["PackageType"] = SafeValue.SafeString(e.NewValues["PackageType"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Mkgs_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
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
        e.Properties["cpSealN"] = sealNs;
        e.Properties["cpContN"] = contNs;
        e.Properties["cpContType"] = contTypes;
        e.Properties["cpKeyValues"] = keyValues;
    }
    protected void grid_Mkgs_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        UpdateMast(e.NewValues["JobNo"].ToString());
    }
    protected void grid_Mkgs_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        UpdateMast(impN.Text);
    }
    protected void grid_Mkgs_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        UpdateMast(e.Values["JobNo"].ToString());
    }

    private void UpdateMast(string expN)
    {
        string sql = "select mast.jobtype,mast.RefNo from seaexport job inner join SeaExportRef mast on job.RefNo=mast.Refno  where job.JobNo='" + expN + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 1)
        {

            string jobType = SafeValue.SafeString(tab.Rows[0][0]);
            if (jobType == "FCL")
                jobType = "CONT";
            else
                jobType = "BL";
            string refNo = SafeValue.SafeString(tab.Rows[0][1]);

            sql = string.Format(@"update SeaExport set 
Volume=(select SUM(Volume) from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') 
,Weight=(select SUM(Weight) from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') 
,Qty=(select SUM(Qty) from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') 
,PackageType= case when (select count(*) from (select PackageType from SeaExportMkg  where JobNo='{0}' and MkgType='{1}' group by PackageType) as aa)>1 then 'Packages' else (select top 1 PackageType from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') end
where JobNo='{0}'", expN, jobType);
            //update house
            C2.Manager.ORManager.ExecuteCommand(sql);
            //update master
            sql = string.Format(@"update SeaExportRef set 
Volume=(select SUM(SeaExportMkg.Volume) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}' )
,Weight=(select SUM(SeaExportMkg.Weight) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}' )
,Qty=(select SUM(SeaExportMkg.Qty) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}' )
,PackageType= case when (select count(*) from (select SeaExportMkg.PackageType from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}'  group by SeaExportMkg.PackageType ) as aa)>1 then 'Packages' else ( select top 1 SeaExportMkg.PackageType from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}') end 

,BkgVolume=(select SUM(SeaExportMkg.Volume) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' )
,BkgWeight=(select SUM(SeaExportMkg.Weight) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' )
,BkgQty=(select SUM(SeaExportMkg.Qty) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' )
,BkgPackageType=case when (select count(*) from (select SeaExportMkg.PackageType from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' group by SeaExportMkg.PackageType) as aa)>1 then 'Packages' else (select top 1 SeaExportMkg.PackageType from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExport.StatusCode='USE' and SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' ) end
 where RefNo='{0}'", refNo, jobType);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
    }
    #endregion


    #region Detail
    protected void grid_ExpDet_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsExpDetail.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    protected void grid_ExpDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaExportDetail));
        }
    }
    protected void grid_ExpDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty"] = 0;
        e.NewValues["Price"] = 0;
        e.NewValues["Amount"] = 0;
        e.NewValues["PrintInd"] = false;
        e.NewValues["WtEntryInd"] = "N";
        e.NewValues["Currency"] = "SGD";
        e.NewValues["PrintTerm"] = "PREPAID";

    }
    protected void grid_ExpDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;

        e.NewValues["Amount"] = SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0);
    }
    protected void grid_ExpDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Amount"] = SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0);
    }
    protected void grid_ExpDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    #endregion


    #region Bl Info Marking
    protected void grid_mkgsBl_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsBlMarking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and MkgType='BKG' and StatusCode='USE'";
    }
    protected void grid_mkgsBl_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaExportMkg));
        }
    }
    protected void grid_mkgsBl_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Volume"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["PackageType"] = " ";
        e.NewValues["MkgType"] = "Bkg";
        e.NewValues["GrossWt"] = 0;
        e.NewValues["NetWt"] = 0;
        e.NewValues["StatusCode"] = "USE";
    }
    protected void grid_mkgsBl_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_mkgsBl_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;

        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["MkgType"] = "Bkg";
    }
    protected void grid_mkgsBl_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
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
        ASPxTextBox HNo = pageControl.FindControl("txtHouseId") as ASPxTextBox;
        //this.dsJobPhoto.FilterExpression = "JobClass='E' and RefNo='" + HNo.Text + "'";// 
        string sql = "select RefNo,JobNo from SeaExport where sequenceId='" + HNo.Text + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            string refNo = tab.Rows[0]["RefNo"].ToString();
            string jobNo = tab.Rows[0]["JobNo"].ToString();
            this.dsJobPhoto.FilterExpression = string.Format("JobClass='E' and RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
        }
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
    }



    #endregion

    #region Sea Commercial
    protected void grid_Commercial_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.SeaCommercial));
        }
    }
    protected void grid_Commercial_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Code"] = "";
        e.NewValues["Price"] = 0;
        e.NewValues["Amount"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["RefType"] = "SE";
        e.NewValues["Description"] = "";
        e.NewValues["LineCNo"] = 0;
        // e.NewValues["StatusCode"] = "USE";
    }
    protected void grid_Commercial_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["RefType"] = "SE";
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["Amount"] = SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0);

        string sql = string.Format("select max(LineCNo) from SeaCommercial where (RefNo='" + refN.Text + "' and JobNo='" + impN.Text + "' and RefType='SE')");
        int lineN = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        e.NewValues["LineCNo"] = ++lineN;
    }
    protected void grid_Commercial_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Amount"] = SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0);
    }
    protected void grid_Commercial_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Commercial_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsCommercial.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    #endregion


    #region Sea Packing
    protected void gird_Packing_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.SeaPacking));
        }
    }
    protected void gird_Packing_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PackageType"] = "";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["RefType"] = "SE";
        e.NewValues["Description"] = "";
        e.NewValues["Marking"] = "";
        e.NewValues["Dimension"] = "";
        e.NewValues["LinePNo"] = 0;
        e.NewValues["PackingType"] = "SP";
    }
    protected void gird_Packing_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["RefType"] = "SE";
        e.NewValues["PackingType"] = "SP";
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
    }
    protected void gird_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
    }
    protected void gird_Packing_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gird_Packing_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsPacking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and PackingType='SP'";
    }
    #endregion

    #region Shipping Request
    protected void grid_shipping_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.SeaPacking));
        }
    }
    protected void grid_shipping_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PackageType"] = "";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["RefType"] = "SE";
        e.NewValues["Description"] = "";
        e.NewValues["Marking"] = "";
        e.NewValues["Dimension"] = "";
        e.NewValues["LinePNo"] = 0;
        e.NewValues["PackingType"] = "SR";
    }
    protected void grid_shipping_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox impN = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["RefType"] = "SE";
        e.NewValues["PackingType"] = "SR";
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
    }
    protected void grid_shipping_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grid_shipping_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_shipping_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsPacking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and PackingType='SR'";
    }
    protected void grid_shipping_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox jobNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;

        string sql = string.Format("select marking,description as Des,qty,packagetype,weight,volume from SeaExportMkg where JobNo='{0}' and MkgType='CONT'", jobNo.Text);
        DataTable tab = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            C2.SeaPacking pack = new SeaPacking();
            pack.Description = SafeValue.SafeString(tab.Rows[i]["Des"]);
            pack.Dimension = "";
            pack.JobNo = jobNo.Text;
            pack.Marking = SafeValue.SafeString(tab.Rows[i]["marking"]);
            pack.PackageType = SafeValue.SafeString(tab.Rows[i]["packagetype"]);
            pack.Qty = SafeValue.SafeInt(tab.Rows[i]["qty"], 0);
            pack.RefNo = refN.Text;
            pack.RefType = "SE";
            pack.Volume = SafeValue.SafeDecimal(tab.Rows[i]["volume"], 0);
            pack.Weight = SafeValue.SafeDecimal(tab.Rows[i]["weight"], 0);
            pack.PackingType = "SR";
            C2.Manager.ORManager.StartTracking(pack, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(pack);
        }
        e.Result = "Success";
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
        ASPxButtonEdit refN = pageControl.FindControl("txt_Hbl_RefN") as ASPxButtonEdit;
        e.NewValues["JobNo"] = impN.Text;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["RefType"] = "SE";
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
        string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsCertificate.FilterExpression = "RefType='SE' and JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    #endregion


    #region full house job
    protected void grid_Export1_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsFullJob.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }
    #endregion

}
