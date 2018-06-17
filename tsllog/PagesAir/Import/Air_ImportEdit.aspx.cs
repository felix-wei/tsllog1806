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
using System.Data.SqlClient;
using DevExpress.Web.ASPxDataView;
using Wilson.ORMapper;
using System.IO;
using System.Xml;

public partial class PagesAir_Import_Air_ImportEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = "Import";
        if (!IsPostBack)
        {
            Session["AirImpWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["AirImpWhere"] = "JobNo='" + Request.QueryString["no"].ToString() + "'";
                this.dsJob.FilterExpression = Session["AirImpWhere"].ToString();
                this.txt_RefNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                this.grid_Export.AddNewRow();
            }
            else
                this.dsJob.FilterExpression = "1=0";

        }
        if (Session["AirImpWhere"] != null)
        {
            this.dsJob.FilterExpression = Session["AirImpWhere"].ToString();
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
            where = " JobNo='" + txt_RefNo.Text.Trim() + "'";

            Session["ImpMastWhere"] = where;
            this.dsJob.FilterExpression = where;
            if (this.grid_Export.GetRow(0) != null)
                this.grid_Export.StartEdit(0);
        }
    }
    #region JOB

    protected void grid_Export_DataSelect(object sender, EventArgs e)
    {

    }
    protected void grid_Export_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.AirImport));
        }
    }
    protected void grid_Export_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        string port = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        string currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["UserId"] = userId;
        e.NewValues["EntryDate"] = DateTime.Today;
        e.NewValues["DeliveryDate"] = DateTime.Today;

        e.NewValues["RefNo"] = Request.QueryString["masterNo"].ToString();

        e.NewValues["CustId"] = "";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["ExpressBl"] = "N";
        e.NewValues["TruckingInd"] = "N";
        e.NewValues["DoReadyInd"] = "N";
        e.NewValues["DoType"] = "D/O";
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

        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["PackageType"] = "";



    }
    protected void grid_Export_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Export.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox custName = this.grid_Export.FindEditFormTemplateControl("txtCust") as ASPxTextBox;
            custName.Text = EzshipHelper.GetPartyName(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "CustomerId" }));
            int oid = SafeValue.SafeInt(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "Id" }), 0);
            if (oid > 0)
            {
                string sql = string.Format("select StatusCode from air_job  where Id={0}", oid);
                ASPxButton btn_VoidHawb = this.grid_Export.FindEditFormTemplateControl("btn_VoidHawb") as ASPxButton;
                string jobStatus = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql));
                if (jobStatus == "CNL")
                {
                    btn_VoidHawb.Text = "Unvoid";

                }
                else
                {
                    btn_VoidHawb.Text = "Void";
                }
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
                ASPxTextBox HNo = this.grid_Export.FindEditFormTemplateControl("txtHouseId") as ASPxTextBox;
                //this.dsJobPhoto.FilterExpression = "JobClass='I' and RefNo='" + refNo.Text + "'";
                string sql = "select RefNo,JobNo from air_job where Id='" + HNo.Text + "'";
                DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                if (tab.Rows.Count > 0)
                {
                    string refNo = tab.Rows[0]["RefNo"].ToString();
                    string jobNo = tab.Rows[0]["JobNo"].ToString();
                    this.dsJobPhoto.FilterExpression = string.Format("JobType='AI' and RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                }
            }
        }
        if (s == "Save")
        {
            SaveJob();
        }
    }
    protected void grid_Export_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxTextBox txtHouseNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        ASPxTextBox txtRefNo = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxTextBox;
        int oid = SafeValue.SafeInt(this.grid_Export.GetRowValues(this.grid_Export.EditingRowVisibleIndex, new string[] { "Id" }), 0);
        string sql = "select StatusCode from air_job where Id=" + oid + "";
        string jobStatus = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql));
        if (s == "VoidHouse")
        {
            #region VoidHouse
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='AI' and MastRefNo='{0}' and JobRefNo='{1}' ", txtRefNo.Text, txtHouseNo.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            if (jobStatus == "CNL")
            {
                sql = string.Format("update air_job set StatusCode='USE' where Id={0}", oid);
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    EzshipLog.Log(txtRefNo.Text, txtHouseNo.Text, "AI", "Unvoid");
                    UpdateMast(txtRefNo.Text);
                    e.Result = "Success";
                    //btn.Text = "Close Job";
                    //closeIndStr.Text = "C";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                string refType = "AI";
                bool closeByEst = EzshipHelper.GetCloseEstInd(txtHouseNo.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update air_job set StatusCode='CNL' where Id={0}", oid);
                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        EzshipLog.Log(txtRefNo.Text, txtHouseNo.Text, "AI", "Void");
                        UpdateMast(txtRefNo.Text);
                        e.Result = "Success";
                        //btn.Text = "Close Job";
                        //closeIndStr.Text = "C";
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


    private void SaveJob()
    {
        try
        {
            string userId = HttpContext.Current.User.Identity.Name;
            ASPxPageControl pageControl = this.grid_Export.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            //ASPxTextBox houseId = pageControl.FindControl("txtHouseId") as ASPxTextBox;
            ASPxTextBox expNoCtr = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
            int expN = 0; //SafeValue.SafeInt(expNoCtr.Text,0);
            string expNs = expNoCtr.Text.Trim();
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(AirImport), "JobNo='" + expNs + "'");
            AirImport exp = C2.Manager.ORManager.GetObject(query) as AirImport;
            bool isNew = false;
            if (exp == null)
            {
                isNew = true;
                expN = 0;
                exp = new AirImport();
                exp.RefNo = Request.QueryString["masterNo"].ToString();
                exp.JobNo = C2Setup.GetSubNo(exp.RefNo, "AI");
                exp.CreateBy = userId;
                exp.CreateDateTime = DateTime.Now;
                exp.Total = "0";
                exp.RefType = "AI";
                exp.StatusCode = "USE";

            }
            ASPxSpinEdit spin_Weight = this.grid_Export.FindEditFormTemplateControl("spin_Weight") as ASPxSpinEdit;
            exp.Weight = SafeValue.SafeDecimal(spin_Weight.Text);
            ASPxSpinEdit spin_Volume = this.grid_Export.FindEditFormTemplateControl("spin_Volume") as ASPxSpinEdit;
            exp.Volume = SafeValue.SafeDecimal(spin_Volume.Text);
            ASPxSpinEdit spin_Qty = this.grid_Export.FindEditFormTemplateControl("spin_Qty") as ASPxSpinEdit;
            exp.Qty = SafeValue.SafeInt(spin_Qty.Text, 0);
            ASPxButtonEdit txt_PackageType = this.grid_Export.FindEditFormTemplateControl("txt_PackageType") as ASPxButtonEdit;
            exp.PackageType = txt_PackageType.Text;
            ASPxComboBox txt_TsInd = this.grid_Export.FindEditFormTemplateControl("txt_TsInd") as ASPxComboBox;
            exp.TsInd = txt_TsInd.Text;
            ASPxMemo remark = this.grid_Export.FindEditFormTemplateControl("Memo_Remark") as ASPxMemo;
            exp.Remark = remark.Text;
            ASPxMemo marking = this.grid_Export.FindEditFormTemplateControl("Memo_Marking") as ASPxMemo;
            exp.Marking = marking.Text;
            ASPxMemo memo_Des = this.grid_Export.FindEditFormTemplateControl("Memo_Des") as ASPxMemo;
            exp.Description = memo_Des.Text;

            ASPxButtonEdit txtCustId = this.grid_Export.FindEditFormTemplateControl("txtCustId") as ASPxButtonEdit;
            exp.CustomerId = txtCustId.Text;
            ASPxTextBox txtHouseBl = this.grid_Export.FindEditFormTemplateControl("txtHouseBl") as ASPxTextBox;
            exp.Hawb = txtHouseBl.Text;
            ASPxMemo tbxShipperName = pageControl.FindControl("tbxShipperName") as ASPxMemo;
            exp.ShipperName = tbxShipperName.Text;
            ASPxMemo tbxIssuedBy = pageControl.FindControl("tbxIssuedBy") as ASPxMemo;
            exp.IssuedBy = tbxIssuedBy.Text;
            ASPxMemo tbxConsigneeName = pageControl.FindControl("tbxConsigneeName") as ASPxMemo;
            exp.ConsigneeName = tbxConsigneeName.Text;
            ASPxMemo tbxCarrierAgent = pageControl.FindControl("tbxCarrierAgent") as ASPxMemo;
            exp.CarrierAgent = tbxCarrierAgent.Text;
            ASPxMemo tbxAccountInfo = pageControl.FindControl("tbxAccountInfo") as ASPxMemo;
            exp.AccountInfo = tbxAccountInfo.Text;
            ASPxTextBox tbxAgentIATACode = pageControl.FindControl("tbxAgentIATACode") as ASPxTextBox;
            exp.AgentIATACode = tbxAgentIATACode.Text;
            ASPxTextBox tbxAgentAccountNo = pageControl.FindControl("tbxAgentAccountNo") as ASPxTextBox;
            exp.AgentAccountNo = tbxAgentAccountNo.Text;
            ASPxTextBox tbxAirportDeparture = pageControl.FindControl("tbxAirportDeparture") as ASPxTextBox;
            exp.AirportDeparture = tbxAirportDeparture.Text;
            ASPxTextBox tbxConnDest1 = pageControl.FindControl("tbxConnDest1") as ASPxTextBox;
            exp.ConnDest1 = tbxConnDest1.Text;
            ASPxTextBox tbxConnCarrier1 = pageControl.FindControl("tbxConnCarrier1") as ASPxTextBox;
            exp.ConnCarrier1 = tbxConnCarrier1.Text;
            ASPxTextBox tbxConnDest2 = pageControl.FindControl("tbxConnDest2") as ASPxTextBox;
            exp.ConnDest2 = tbxConnDest2.Text;
            ASPxTextBox tbxConnCarrier2 = pageControl.FindControl("tbxConnCarrier2") as ASPxTextBox;
            exp.ConnCarrier2 = tbxConnCarrier2.Text;
            ASPxTextBox tbxConnDest3 = pageControl.FindControl("tbxConnDest3") as ASPxTextBox;
            exp.ConnDest3 = tbxConnDest3.Text;
            ASPxTextBox tbxConnCarrier3 = pageControl.FindControl("tbxConnCarrier3") as ASPxTextBox;
            exp.ConnCarrier3 = tbxConnCarrier3.Text;
            ASPxTextBox tbxCurrency = pageControl.FindControl("tbxCurrency") as ASPxTextBox;
            exp.Currency = tbxCurrency.Text;
            ASPxTextBox tbxChgsCode = pageControl.FindControl("tbxChgsCode") as ASPxTextBox;
            exp.ChgsCode = tbxChgsCode.Text;
            ASPxTextBox tbxPPD1 = pageControl.FindControl("tbxPPD1") as ASPxTextBox;
            exp.Ppd1 = tbxPPD1.Text;
            ASPxTextBox tbxCOLL1 = pageControl.FindControl("tbxCOLL1") as ASPxTextBox;
            exp.Coll1 = tbxCOLL1.Text;
            ASPxTextBox tbxPPD2 = pageControl.FindControl("tbxPPD2") as ASPxTextBox;
            exp.Ppd2 = tbxPPD2.Text;
            ASPxTextBox tbxCOLL2 = pageControl.FindControl("tbxCOLL2") as ASPxTextBox;
            exp.Coll2 = tbxCOLL2.Text;
            ASPxTextBox tbxCarriageValue = pageControl.FindControl("tbxCarriageValue") as ASPxTextBox;
            exp.CarriageValue = tbxCarriageValue.Text;
            ASPxTextBox tbxCustomValue = pageControl.FindControl("tbxCustomValue") as ASPxTextBox;
            exp.CustomValue = tbxCustomValue.Text;
            ASPxTextBox tbxAirportDestination = pageControl.FindControl("tbxAirportDestination") as ASPxTextBox;
            exp.AirportDestination = tbxAirportDestination.Text;
            ASPxTextBox tbxRequestedFlight = pageControl.FindControl("tbxRequestedFlight") as ASPxTextBox;
            exp.RequestedFlight = tbxRequestedFlight.Text;
            ASPxTextBox tbxRequestedDate = pageControl.FindControl("tbxRequestedDate") as ASPxTextBox;
            exp.RequestedDate = tbxRequestedDate.Text;
            ASPxTextBox tbxAmountInsurance = pageControl.FindControl("tbxAmountInsurance") as ASPxTextBox;
            exp.AmountInsurance = tbxAmountInsurance.Text;
            ASPxMemo tbxHandlingInfo = pageControl.FindControl("tbxHandlingInfo") as ASPxMemo;
            exp.HandlingInfo = tbxHandlingInfo.Text;
            ASPxTextBox tbxPiece = pageControl.FindControl("tbxPiece") as ASPxTextBox;
            exp.Piece = tbxPiece.Text;
            ASPxSpinEdit tbxGrossWeight = pageControl.FindControl("tbxGrossWeight") as ASPxSpinEdit;
            exp.GrossWeight = tbxGrossWeight.Text;
            ASPxSpinEdit tbxUnit = pageControl.FindControl("tbxUnit") as ASPxSpinEdit;
            exp.Unit = tbxUnit.Text;
            ASPxTextBox tbxRateClass = pageControl.FindControl("tbxRateClass") as ASPxTextBox;
            exp.RateClass = tbxRateClass.Text;
            ASPxTextBox tbxCommodityItemNo = pageControl.FindControl("tbxCommodityItemNo") as ASPxTextBox;
            exp.CommodityItemNo = tbxCommodityItemNo.Text;
            ASPxSpinEdit tbxChargeableWeight = pageControl.FindControl("tbxChargeableWeight") as ASPxSpinEdit;
            exp.ChargeableWeight = tbxChargeableWeight.Text;
            ASPxTextBox tbxRateCharge = pageControl.FindControl("tbxRateCharge") as ASPxTextBox;
            exp.RateCharge = tbxRateCharge.Text;
            ASPxSpinEdit tbxTotal = pageControl.FindControl("tbxTotal") as ASPxSpinEdit;
            exp.Total = tbxTotal.Text;
            ASPxMemo tbxGoodsNature = pageControl.FindControl("tbxGoodsNature") as ASPxMemo;
            exp.GoodsNature = tbxGoodsNature.Text;
            ASPxMemo tbxContentRemark = pageControl.FindControl("tbxContentRemark") as ASPxMemo;
            exp.ContentRemark = tbxContentRemark.Text;
            ASPxTextBox tbxWeightChargeP = pageControl.FindControl("tbxWeightChargeP") as ASPxTextBox;
            exp.WeightChargeP = tbxWeightChargeP.Text;
            ASPxTextBox tbxWeightChargeC = pageControl.FindControl("tbxWeightChargeC") as ASPxTextBox;
            exp.WeightChargeC = tbxWeightChargeC.Text;
            ASPxTextBox tbxValuationChargeP = pageControl.FindControl("tbxValuationChargeP") as ASPxTextBox;
            exp.ValuationChargeP = tbxValuationChargeP.Text;
            ASPxTextBox tbxValuationChargeC = pageControl.FindControl("tbxValuationChargeC") as ASPxTextBox;
            exp.ValuationChargeC = tbxValuationChargeC.Text;
            ASPxTextBox tbxTaxP = pageControl.FindControl("tbxTaxP") as ASPxTextBox;
            exp.TaxP = tbxTaxP.Text;
            ASPxTextBox tbxTaxC = pageControl.FindControl("tbxTaxC") as ASPxTextBox;
            exp.TaxC = tbxTaxC.Text;
            ASPxTextBox tbxOtherAgentChargeP = pageControl.FindControl("tbxOtherAgentChargeP") as ASPxTextBox;
            exp.OtherAgentChargeP = tbxOtherAgentChargeP.Text;
            ASPxTextBox tbxOtherAgentChargeC = pageControl.FindControl("tbxOtherAgentChargeC") as ASPxTextBox;
            exp.OtherAgentChargeC = tbxOtherAgentChargeC.Text;
            ASPxTextBox tbxOtherCarrierChargeP = pageControl.FindControl("tbxOtherCarrierChargeP") as ASPxTextBox;
            exp.OtherCarrierChargeP = tbxOtherCarrierChargeP.Text;
            ASPxTextBox tbxOtherCarrierChargeC = pageControl.FindControl("tbxOtherCarrierChargeC") as ASPxTextBox;
            exp.OtherCarrierChargeC = tbxOtherCarrierChargeC.Text;
            ASPxTextBox tbxTotalPrepaid = pageControl.FindControl("tbxTotalPrepaid") as ASPxTextBox;
            exp.TotalPrepaid = tbxTotalPrepaid.Text;
            ASPxTextBox tbxTotalCollect = pageControl.FindControl("tbxTotalCollect") as ASPxTextBox;
            exp.TotalCollect = tbxTotalCollect.Text;
            ASPxTextBox tbxCurrencyRate = pageControl.FindControl("tbxCurrencyRate") as ASPxTextBox;
            exp.CurrencyRate = tbxCurrencyRate.Text;
            ASPxTextBox tbxChargeDestCurrency = pageControl.FindControl("tbxChargeDestCurrency") as ASPxTextBox;
            exp.ChargeDestCurrency = tbxChargeDestCurrency.Text;

            ASPxTextBox tbxOtherCharge1 = pageControl.FindControl("tbxOtherCharge1") as ASPxTextBox;
            exp.OtherCharge1 = tbxOtherCharge1.Text;
            ASPxTextBox tbxOtherCharge1Currency = pageControl.FindControl("tbxOtherCharge1Currency") as ASPxTextBox;
            exp.OtherCharge1Currency = tbxOtherCharge1Currency.Text;
            ASPxSpinEdit tbxOtherCharge1Amount = pageControl.FindControl("tbxOtherCharge1Amount") as ASPxSpinEdit;
            exp.OtherCharge1Amount = SafeValue.SafeDecimal(tbxOtherCharge1Amount.Text);

            ASPxTextBox tbxOtherCharge2 = pageControl.FindControl("tbxOtherCharge2") as ASPxTextBox;
            exp.OtherCharge2 = tbxOtherCharge2.Text;
            ASPxTextBox tbxOtherCharge2Currency = pageControl.FindControl("tbxOtherCharge2Currency") as ASPxTextBox;
            exp.OtherCharge2Currency = tbxOtherCharge2Currency.Text;
            ASPxSpinEdit tbxOtherCharge2Amount = pageControl.FindControl("tbxOtherCharge2Amount") as ASPxSpinEdit;
            exp.OtherCharge2Amount = SafeValue.SafeDecimal(tbxOtherCharge2Amount.Text);

            ASPxTextBox tbxOtherCharge3 = pageControl.FindControl("tbxOtherCharge3") as ASPxTextBox;
            exp.OtherCharge3 = tbxOtherCharge3.Text;
            ASPxTextBox tbxOtherCharge3Currency = pageControl.FindControl("tbxOtherCharge3Currency") as ASPxTextBox;
            exp.OtherCharge3Currency = tbxOtherCharge3Currency.Text;
            ASPxSpinEdit tbxOtherCharge3Amount = pageControl.FindControl("tbxOtherCharge3Amount") as ASPxSpinEdit;
            exp.OtherCharge3Amount = SafeValue.SafeDecimal(tbxOtherCharge3Amount.Text);

            ASPxTextBox tbxOtherCharge4 = pageControl.FindControl("tbxOtherCharge4") as ASPxTextBox;
            exp.OtherCharge4 = tbxOtherCharge4.Text;
            ASPxTextBox tbxOtherCharge4Currency = pageControl.FindControl("tbxOtherCharge4Currency") as ASPxTextBox;
            exp.OtherCharge4Currency = tbxOtherCharge4Currency.Text;
            ASPxSpinEdit tbxOtherCharge4Amount = pageControl.FindControl("tbxOtherCharge4Amount") as ASPxSpinEdit;
            exp.OtherCharge4Amount = SafeValue.SafeDecimal(tbxOtherCharge4Amount.Text);

            ASPxTextBox tbxOtherCharge5 = pageControl.FindControl("tbxOtherCharge5") as ASPxTextBox;
            exp.OtherCharge5 = tbxOtherCharge5.Text;
            ASPxTextBox tbxOtherCharge5Currency = pageControl.FindControl("tbxOtherCharge5Currency") as ASPxTextBox;
            exp.OtherCharge5Currency = tbxOtherCharge5Currency.Text;
            ASPxSpinEdit tbxOtherCharge5Amount = pageControl.FindControl("tbxOtherCharge5Amount") as ASPxSpinEdit;
            exp.OtherCharge5Amount = SafeValue.SafeDecimal(tbxOtherCharge5Amount.Text);

            ASPxTextBox tbxSignatureShipper = pageControl.FindControl("tbxSignatureShipper") as ASPxTextBox;
            exp.SignatureShipper = tbxSignatureShipper.Text;
            ASPxTextBox tbxExecuteDate = pageControl.FindControl("tbxExecuteDate") as ASPxTextBox;
            exp.ExecuteDate = tbxExecuteDate.Text;
            ASPxTextBox tbxExecutePlace = pageControl.FindControl("tbxExecutePlace") as ASPxTextBox;
            exp.ExecutePlace = tbxExecutePlace.Text;
            ASPxTextBox tbxSignatureIssuing = pageControl.FindControl("tbxSignatureIssuing") as ASPxTextBox;
            exp.SignatureIssuing = tbxSignatureIssuing.Text;

            ASPxButtonEdit haulier = pageControl.FindControl("txt_Ref_H_Haulier") as ASPxButtonEdit;
            exp.HaulierName = haulier.Text;
            ASPxTextBox crno = pageControl.FindControl("txt_Ref_H_CrNo") as ASPxTextBox;
            exp.HaulierCrNo = crno.Text;
            ASPxTextBox attention = pageControl.FindControl("txt_Ref_H_Attention") as ASPxTextBox;
            exp.HaulierAttention = attention.Text;

            ASPxMemo collectFrm1 = pageControl.FindControl("txt_Ref_H_CltFrm") as ASPxMemo;
            exp.HaulierCollect = collectFrm1.Text;

            ASPxMemo truckTo1 = pageControl.FindControl("txt_Ref_H_TruckTo") as ASPxMemo;
            exp.HaulierTruck = truckTo1.Text;
            ASPxDateEdit collectDate = pageControl.FindControl("date_Ref_H_CltDate") as ASPxDateEdit;
            exp.HaulierCollectDate = collectDate.Date;
            ASPxMemo haulierRmk = pageControl.FindControl("txt_Ref_H_Rmk1") as ASPxMemo;
            exp.HaulierRemark = haulierRmk.Text;
            ASPxMemo haulierPermit = pageControl.FindControl("txt_Hbl_Permit") as ASPxMemo;
            exp.PermitRmk = haulierPermit.Text;

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
            ASPxMemo me_DriverRemark = pageControl.FindControl("me_DriverRemark") as ASPxMemo;
            exp.DriverRemark = me_DriverRemark.Text;
            ASPxDateEdit deliveryDate = this.grid_Export.FindEditFormTemplateControl("date_DeliveryDate") as ASPxDateEdit;
            exp.DeliveryDate = deliveryDate.Date;

            ASPxComboBox doReady = this.grid_Export.FindEditFormTemplateControl("txtDoReady") as ASPxComboBox;
            exp.DoReadyInd = doReady.Text;

            ASPxButtonEdit txtPODBy = pageControl.FindControl("txtPODBy") as ASPxButtonEdit;
            exp.PODBy = txtPODBy.Text;
            ASPxDateEdit date_PodTime = pageControl.FindControl("date_PodTime") as ASPxDateEdit;
            exp.PODTime = date_PodTime.Date;
            ASPxMemo me_Remark = pageControl.FindControl("me_Remark") as ASPxMemo;
            exp.PODRemark = me_Remark.Text;
            exp.PODUpdateUser = EzshipHelper.GetUserName();
            exp.PODUpdateTime = DateTime.Now;

            ASPxTextBox txt_TsBkgRef = this.grid_Export.FindEditFormTemplateControl("txt_TsBkgRef") as ASPxTextBox;
            exp.TsBkgRef = txt_TsBkgRef.Text;
            ASPxTextBox txt_TsBkgUser = this.grid_Export.FindEditFormTemplateControl("txt_TsBkgUser") as ASPxTextBox;
            exp.TsBkgUser = txt_TsBkgUser.Text;
            ASPxDateEdit date_TsBkgTime = this.grid_Export.FindEditFormTemplateControl("date_TsBkgTime") as ASPxDateEdit;
            exp.TsBkgTime = date_TsBkgTime.Date;

            if (isNew)
            {
                exp.CloseInd = "N";
                exp.CreateBy = userId;
                exp.CreateDateTime = DateTime.Now;
                exp.RefNo = Request.QueryString["masterNo"].ToString();
                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(exp);
            }
            else
            {
                exp.UpdateBy = userId;
                exp.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(exp);
               
            }
            UpdateMast(exp.RefNo);
            Session["AirImpWhere"] = "JobNo='" + exp.JobNo + "'";
            this.dsJob.FilterExpression = Session["AirImpWhere"].ToString();
            if (this.grid_Export.GetRow(0) != null)
                this.grid_Export.StartEdit(0);
        }
        catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
    } 
    #endregion
    private void UpdateMast(string refNo)
    {
        string sql = string.Format(@"update air_ref set Volume=(select SUM(Volume) from air_job where StatusCode='USE' and RefNo='{0}'),Weight=(select SUM(Weight) from air_job where StatusCode='USE' and RefNo='{0}'),Qty=(select SUM(Qty) from air_job where StatusCode='USE' and RefNo='{0}'),PackageType=(select MAX(PackageType) from air_job where StatusCode='USE' and RefNo='{0}') where RefNo='{0}'", refNo);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }

    #region bill  
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo,JobNo from air_job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            string refNo = tab.Rows[0]["RefNo"].ToString();
            string jobNo = tab.Rows[0]["JobNo"].ToString();
            this.dsInvoice.FilterExpression = string.Format("MastType='AI' and MastRefNo='{0}' and JobRefNo='{1}'",refNo,jobNo);
        }
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo,JobNo from air_job where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            string refNo = tab.Rows[0]["RefNo"].ToString();
            string jobNo = tab.Rows[0]["JobNo"].ToString();
            this.dsVoucher.FilterExpression = string.Format("MastType='AI' and MastRefNo='{0}' and JobRefNo='{1}'", refNo, jobNo);
        }
    }
    #endregion


    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select JobNo from air_job where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
            this.dsCosting.FilterExpression = "JobType='AI' and JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.AirCosting));
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
        ASPxTextBox refNo = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxTextBox;
        ASPxTextBox houseNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        e.NewValues["JobType"] = "AI";
        e.NewValues["RefNo"] = refNo.Text;
        e.NewValues["JobNo"] = houseNo.Text;
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxTextBox refNo = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxTextBox;
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
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxTextBox refNo = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxTextBox;
        UpdateEstAmt(refNo.Text);
    }
    protected void grid_Cost_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxTextBox refNo = this.grid_Export.FindEditFormTemplateControl("txtRefNo") as ASPxTextBox;
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
        string sql = string.Format("Update air_ref set EstSaleAmt=(select sum(SaleLocAmt) from air_Costing where JobType='AI' and RefNo=air_ref.RefNo),EstCostAmt=(select sum(CostLocAmt) from air_Costing where JobType='AI' and RefNo=air_ref.refNo) where RefNo='{0}'", refNo);
        ConnectSql.ExecuteSql(sql);
    }
    #endregion
    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.AirAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox txtRefNo = this.grid_Export.FindEditFormTemplateControl("txtHouseNo") as ASPxTextBox;
        this.dsJobPhoto.FilterExpression = "JobNo='" + SafeValue.SafeString(txtRefNo.Text, "") + "'";
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
    }

    #endregion


}