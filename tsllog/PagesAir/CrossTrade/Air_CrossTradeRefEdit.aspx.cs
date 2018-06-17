using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxDataView;
using Wilson.ORMapper;
using C2;
using System.Data;
using System.IO;
using System.Xml;

public partial class PagesAir_CrossTrade_Air_CrossTradeRefEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["CtMastWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["CtMastWhere"] = "RefNo='" + Request.QueryString["no"].ToString() + "'";
                this.txt_RefNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"] != null)
            {
                if (Session["CtMastWhere"] == null)
                {
                    this.grid_ref.AddNewRow();
                }
            }
            else
                this.dsImportRef.FilterExpression = "1=0";
        }
        if (Session["CtMastWhere"] != null)
        {
            this.dsImportRef.FilterExpression = Session["CtMastWhere"].ToString();
            if (this.grid_ref.GetRow(0) != null)
                this.grid_ref.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_ref_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.AirImportRef));
        }
    }
    protected void grid_ref_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

        string userId = HttpContext.Current.User.Identity.Name;
        string currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["RefType"] = "ACT";
        e.NewValues["MAWB"] = "";
        e.NewValues["RefNo"] = "NEW";
        e.NewValues["CarrierBkgNo"] = "";
        e.NewValues["NvoccBlNO"] = "";
        e.NewValues["Piece"] = "0";
        e.NewValues["Unit"] = "0";
        e.NewValues["RefDate"] = DateTime.Today;
        e.NewValues["FlightDate0"] = DateTime.Today;
        e.NewValues["Currency"] = currency;
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["UpdateBy"] = userId;
        e.NewValues["UpdateDateTime"] = DateTime.Today;
        e.NewValues["CloseUser"] = userId;
        e.NewValues["CloseDate"] = DateTime.Today;
    }
    protected void grid_ref_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Photo")
        {
            if (this.dsJobPhoto.FilterExpression == "1=0")
            {
                ASPxGridView grd = sender as ASPxGridView;
                ASPxPageControl pageControl = grd.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                ASPxTextBox refNo = this.grid_ref.FindEditFormTemplateControl("txt_RefN") as ASPxTextBox;
                this.dsJobPhoto.FilterExpression = "JobType='ACT' and RefNo='" + refNo.Text + "'";// 
            }
        }
        if (s == "Save")
        {
            SaveJob();
        }
    }
    private void SaveJob()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txtRefNo = this.grid_ref.FindEditFormTemplateControl("txtRefNo") as ASPxTextBox;
            ASPxTextBox txt_Id = this.grid_ref.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            ASPxDateEdit refDate = this.grid_ref.FindEditFormTemplateControl("date_RefDate") as ASPxDateEdit;
            string refNo = SafeValue.SafeString(txt_RefNo.Text, "");
            int id = SafeValue.SafeInt(txt_Id.Text, 0);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(AirImportRef), "Id=" + id + "");
            AirImportRef airRef = C2.Manager.ORManager.GetObject(query) as AirImportRef;
            bool isNew = false;
            string userId = HttpContext.Current.User.Identity.Name;
            if (airRef == null)
            {
                airRef = new AirImportRef();
                isNew = true;
                refNo = C2Setup.GetNextNo("ACT","AirCrossTrade",refDate.Date);
                AirImport air = new AirImport();
                air.JobNo = C2Setup.GetSubNo(refNo, "ACT");
                air.CloseInd = "N";
                air.CreateBy = userId;
                air.CreateDateTime = DateTime.Now;
                air.RefNo = refNo;
                air.Total = "0";
                air.RefType = "ACT";
                air.StatusCode = "USE";
                C2.Manager.ORManager.StartTracking(air, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(air);
            }
            ASPxButtonEdit txt_AgentId = this.grid_ref.FindEditFormTemplateControl("txt_AgentId") as ASPxButtonEdit;
            airRef.AgentId = txt_AgentId.Text;
            ASPxButtonEdit localCust = this.grid_ref.FindEditFormTemplateControl("txt_LocalCust") as ASPxButtonEdit;
            airRef.LocalCust = localCust.Text;
            ASPxMemo remarks = pageControl.FindControl("txt_Remarks") as ASPxMemo;
            airRef.Remarks = remarks.Text;
            ASPxButtonEdit tbxAirportCode0 = pageControl.FindControl("tbxAirportCode0") as ASPxButtonEdit;
            airRef.AirportCode0 = tbxAirportCode0.Text;
            ASPxTextBox tbxAirportName0 = pageControl.FindControl("tbxAirportName0") as ASPxTextBox;
            airRef.AirportName0 = tbxAirportName0.Text;
            ASPxDateEdit date_FlightDate0 = pageControl.FindControl("date_FlightDate0") as ASPxDateEdit;
            airRef.FlightDate0 = date_FlightDate0.Date;
            ASPxTextBox tbxFlightTime0 = pageControl.FindControl("tbxFlightTime0") as ASPxTextBox;
            airRef.FlightTime0 = tbxFlightTime0.Text;


            airRef.RefDate = refDate.Date;
            ASPxButtonEdit tbxAirportCode1 = pageControl.FindControl("txt_AirportCode1") as ASPxButtonEdit;
            airRef.AirportCode1 = tbxAirportCode1.Text;
            ASPxTextBox tbxAirportName1 = pageControl.FindControl("txt_AirportName1") as ASPxTextBox;
            airRef.AirportName1 = tbxAirportName1.Text;
            ASPxDateEdit spin_FlightDate1 = pageControl.FindControl("spin_FlightDate1") as ASPxDateEdit;
            airRef.FlightDate1 = spin_FlightDate1.Date;
            ASPxTextBox txt_FlightTime1 = pageControl.FindControl("txt_FlightTime1") as ASPxTextBox;
            airRef.FlightTime1 = txt_FlightTime1.Text;



            ASPxTextBox tbxAirlineCode1 = pageControl.FindControl("tbxAirlineCode1") as ASPxTextBox;
            airRef.AirlineCode1 = tbxAirlineCode1.Text;
            ASPxTextBox tbxAirlineName1 = pageControl.FindControl("tbxAirlineName1") as ASPxTextBox;
            airRef.AirlineName1 = tbxAirlineName1.Text;
            ASPxTextBox tbxFlightNo1 = pageControl.FindControl("tbxFlightNo1") as ASPxTextBox;
            airRef.FlightNo1 = tbxFlightNo1.Text;
            ASPxDateEdit tbxAirFlightDate1 = pageControl.FindControl("tbxAirFlightDate1") as ASPxDateEdit;
            airRef.AirFlightDate1 = tbxAirFlightDate1.Date;
            ASPxTextBox tbxAirFlightTime1 = pageControl.FindControl("tbxAirFlightTime1") as ASPxTextBox;
            airRef.AirFlightTime1 = tbxAirFlightTime1.Text;
            ASPxButtonEdit tbxAirLinePortCode1 = pageControl.FindControl("tbxAirLinePortCode1") as ASPxButtonEdit;
            airRef.AirLinePortCode1 = tbxAirLinePortCode1.Text;
            ASPxTextBox tbxAirLinePortName1 = pageControl.FindControl("tbxAirLinePortName1") as ASPxTextBox;
            airRef.AirLinePortName1 = tbxAirLinePortName1.Text;


            ASPxTextBox tbxAirlineCode2 = pageControl.FindControl("tbxAirlineCode2") as ASPxTextBox;
            airRef.AirlineCode2 = tbxAirlineCode2.Text;
            ASPxTextBox tbxAirlineName2 = pageControl.FindControl("tbxAirlineName2") as ASPxTextBox;
            airRef.AirlineName2 = tbxAirlineName2.Text;
            ASPxTextBox tbxFlightNo2 = pageControl.FindControl("tbxFlightNo2") as ASPxTextBox;
            airRef.FlightNo2 = tbxFlightNo2.Text;
            ASPxDateEdit tbxFlightDate2 = pageControl.FindControl("tbxFlightDate2") as ASPxDateEdit;
            airRef.FlightDate2 = tbxFlightDate2.Date;
            ASPxTextBox tbxFlightTime2 = pageControl.FindControl("tbxFlightTime2") as ASPxTextBox;
            airRef.FlightTime2 = tbxFlightTime2.Text;
            ASPxButtonEdit tbxAirportCode2 = pageControl.FindControl("tbxAirportCode2") as ASPxButtonEdit;
            airRef.AirportCode2 = tbxAirportCode2.Text;
            ASPxTextBox AirportName2 = pageControl.FindControl("tbxAirportName2") as ASPxTextBox;
            airRef.AirportName2 = AirportName2.Text;

            ASPxTextBox tbxAirlineCode3 = pageControl.FindControl("tbxAirlineCode3") as ASPxTextBox;
            airRef.AirlineCode3 = tbxAirlineCode3.Text;
            ASPxTextBox tbxAirlineName3 = pageControl.FindControl("tbxAirlineName3") as ASPxTextBox;
            airRef.AirlineName3 = tbxAirlineName3.Text;
            ASPxTextBox tbxFlightNo3 = pageControl.FindControl("tbxFlightNo3") as ASPxTextBox;
            airRef.FlightNo3 = tbxFlightNo3.Text;
            ASPxDateEdit tbxFlightDate3 = pageControl.FindControl("tbxFlightDate3") as ASPxDateEdit;
            airRef.FlightDate3 = tbxFlightDate3.Date;
            ASPxTextBox tbxFlightTime3 = pageControl.FindControl("tbxFlightTime3") as ASPxTextBox;
            airRef.FlightTime3 = tbxFlightTime3.Text;
            ASPxButtonEdit tbxAirportCode3 = pageControl.FindControl("tbxAirportCode3") as ASPxButtonEdit;
            airRef.AirportCode3 = tbxAirportCode3.Text;
            ASPxTextBox tbxAirportName3 = pageControl.FindControl("tbxAirportName3") as ASPxTextBox;
            airRef.AirportName3 = tbxAirportName3.Text;

            ASPxTextBox tbxAirlineCode4 = pageControl.FindControl("tbxAirlineCode4") as ASPxTextBox;
            airRef.AirlineCode4 = tbxAirlineCode4.Text;
            ASPxTextBox tbxAirlineName4 = pageControl.FindControl("tbxAirlineName4") as ASPxTextBox;
            airRef.AirlineName4 = tbxAirlineName4.Text;
            ASPxTextBox tbxFlightNo4 = pageControl.FindControl("tbxFlightNo4") as ASPxTextBox;
            airRef.FlightNo4 = tbxFlightNo4.Text;
            ASPxDateEdit tbxFlightDate4 = pageControl.FindControl("tbxFlightDate4") as ASPxDateEdit;
            airRef.FlightDate4 = tbxFlightDate4.Date;
            ASPxTextBox tbxFlightTime4 = pageControl.FindControl("tbxFlightTime4") as ASPxTextBox;
            airRef.FlightTime4 = tbxFlightTime4.Text;
            ASPxButtonEdit tbxAirportCode4 = pageControl.FindControl("tbxAirportCode4") as ASPxButtonEdit;
            airRef.AirportCode4 = tbxAirportCode4.Text;
            ASPxTextBox AirportName4 = pageControl.FindControl("tbxAirportName4") as ASPxTextBox;
            airRef.AirportName4 = AirportName4.Text;

            ASPxTextBox txtMAWB = this.grid_ref.FindEditFormTemplateControl("txt_MAWB") as ASPxTextBox;
            airRef.Mawb = txtMAWB.Text;

            ASPxButtonEdit txt_CrAgentId = this.grid_ref.FindEditFormTemplateControl("txt_CrAgentId") as ASPxButtonEdit;
            airRef.CarrierAgentId = txt_CrAgentId.Text;
            ASPxTextBox txt_CrBkgRefN = this.grid_ref.FindEditFormTemplateControl("txt_CrBkgRefN") as ASPxTextBox;
            airRef.CarrierBkgNo = txt_CrBkgRefN.Text;
            ASPxButtonEdit txt_NvoccAgentId = pageControl.FindControl("txt_NvoccAgentId") as ASPxButtonEdit;
            airRef.NvoccAgentId = txt_NvoccAgentId.Text;
            ASPxTextBox txt_NvoccBl = pageControl.FindControl("txt_NvoccBl") as ASPxTextBox;
            airRef.NvoccBlNO = txt_NvoccBl.Text;
            ASPxButtonEdit cbx_Currency = pageControl.FindControl("cbx_Currency") as ASPxButtonEdit;
            airRef.CurrencyId = cbx_Currency.Text;
            ASPxSpinEdit spin_CrExRate = pageControl.FindControl("spin_CrExRate") as ASPxSpinEdit;
            airRef.ExRate = SafeValue.SafeDecimal(spin_CrExRate.Value,0);
            ASPxButtonEdit txt_WhId = pageControl.FindControl("txt_WhId") as ASPxButtonEdit;
            airRef.WareHouseId = txt_WhId.Text;
            ASPxMemo tbxShipperName = pageControl.FindControl("tbxShipperName") as ASPxMemo;
            airRef.ShipperName = tbxShipperName.Text;
            ASPxMemo tbxIssuedBy = pageControl.FindControl("tbxIssuedBy") as ASPxMemo;
            airRef.IssuedBy = tbxIssuedBy.Text;
            ASPxMemo tbxConsigneeName = pageControl.FindControl("tbxConsigneeName") as ASPxMemo;
            airRef.ConsigneeName = tbxConsigneeName.Text;
            ASPxMemo tbxCarrierAgent = pageControl.FindControl("tbxCarrierAgent") as ASPxMemo;
            airRef.CarrierAgent = tbxCarrierAgent.Text;
            ASPxMemo tbxAccountInfo = pageControl.FindControl("tbxAccountInfo") as ASPxMemo;
            airRef.AccountInfo = tbxAccountInfo.Text;
            ASPxTextBox tbxAgentIATACode = pageControl.FindControl("tbxAgentIATACode") as ASPxTextBox;
            airRef.AgentIATACode = tbxAgentIATACode.Text;
            ASPxTextBox tbxAgentAccountNo = pageControl.FindControl("tbxAgentAccountNo") as ASPxTextBox;
            airRef.AgentAccountNo = tbxAgentAccountNo.Text;
            ASPxTextBox tbxAirportDeparture = pageControl.FindControl("tbxAirportDeparture") as ASPxTextBox;
            airRef.AirportDeparture = tbxAirportDeparture.Text;
            ASPxTextBox tbxConnDest1 = pageControl.FindControl("tbxConnDest1") as ASPxTextBox;
            airRef.ConnDest1 = tbxConnDest1.Text;
            ASPxTextBox tbxConnCarrier1 = pageControl.FindControl("tbxConnCarrier1") as ASPxTextBox;
            airRef.ConnCarrier1 = tbxConnCarrier1.Text;
            ASPxTextBox tbxConnDest2 = pageControl.FindControl("tbxConnDest2") as ASPxTextBox;
            airRef.ConnDest2 = tbxConnDest2.Text;
            ASPxTextBox tbxConnCarrier2 = pageControl.FindControl("tbxConnCarrier2") as ASPxTextBox;
            airRef.ConnCarrier2 = tbxConnCarrier2.Text;
            ASPxTextBox tbxConnDest3 = pageControl.FindControl("tbxConnDest3") as ASPxTextBox;
            airRef.ConnDest3 = tbxConnDest3.Text;
            ASPxTextBox tbxConnCarrier3 = pageControl.FindControl("tbxConnCarrier3") as ASPxTextBox;
            airRef.ConnCarrier3 = tbxConnCarrier3.Text;
            ASPxTextBox tbxCurrency = pageControl.FindControl("tbxCurrency") as ASPxTextBox;
            airRef.Currency = tbxCurrency.Text;
            ASPxTextBox tbxChgsCode = pageControl.FindControl("tbxChgsCode") as ASPxTextBox;
            airRef.ChgsCode = tbxChgsCode.Text;
            ASPxTextBox tbxPPD1 = pageControl.FindControl("tbxPPD1") as ASPxTextBox;
            airRef.Ppd1 = tbxPPD1.Text;
            ASPxTextBox tbxCOLL1 = pageControl.FindControl("tbxCOLL1") as ASPxTextBox;
            airRef.Coll1 = tbxCOLL1.Text;
            ASPxTextBox tbxPPD2 = pageControl.FindControl("tbxPPD2") as ASPxTextBox;
            airRef.Ppd2 = tbxPPD2.Text;
            ASPxTextBox tbxCOLL2 = pageControl.FindControl("tbxCOLL2") as ASPxTextBox;
            airRef.Coll2 = tbxCOLL2.Text;
            ASPxTextBox tbxCarriageValue = pageControl.FindControl("tbxCarriageValue") as ASPxTextBox;
            airRef.CarriageValue = tbxCarriageValue.Text;
            ASPxTextBox tbxCustomValue = pageControl.FindControl("tbxCustomValue") as ASPxTextBox;
            airRef.CustomValue = tbxCustomValue.Text;
            ASPxTextBox tbxAirportDestination = pageControl.FindControl("tbxAirportDestination") as ASPxTextBox;
            airRef.AirportDestination = tbxAirportDestination.Text;
            ASPxTextBox tbxRequestedFlight = pageControl.FindControl("tbxRequestedFlight") as ASPxTextBox;
            airRef.RequestedFlight = tbxRequestedFlight.Text;
            ASPxTextBox tbxRequestedDate = pageControl.FindControl("tbxRequestedDate") as ASPxTextBox;
            airRef.RequestedDate = tbxRequestedDate.Text;
            ASPxTextBox tbxAmountInsurance = pageControl.FindControl("tbxAmountInsurance") as ASPxTextBox;
            airRef.AmountInsurance = tbxAmountInsurance.Text;
            ASPxMemo tbxHandlingInfo = pageControl.FindControl("tbxHandlingInfo") as ASPxMemo;
            airRef.HandlingInfo = tbxHandlingInfo.Text;
            ASPxTextBox tbxPiece = pageControl.FindControl("tbxPiece") as ASPxTextBox;
            airRef.Piece = tbxPiece.Text;
            ASPxSpinEdit tbxGrossWeight = pageControl.FindControl("tbxGrossWeight") as ASPxSpinEdit;
            airRef.GrossWeight = tbxGrossWeight.Text;
            ASPxSpinEdit tbxUnit = pageControl.FindControl("tbxUnit") as ASPxSpinEdit;
            airRef.Unit = tbxUnit.Text;
            ASPxTextBox tbxRateClass = pageControl.FindControl("tbxRateClass") as ASPxTextBox;
            airRef.RateClass = tbxRateClass.Text;
            ASPxTextBox tbxCommodityItemNo = pageControl.FindControl("tbxCommodityItemNo") as ASPxTextBox;
            airRef.CommodityItemNo = tbxCommodityItemNo.Text;
            ASPxSpinEdit tbxChargeableWeight = pageControl.FindControl("tbxChargeableWeight") as ASPxSpinEdit;
            airRef.ChargeableWeight = tbxChargeableWeight.Text;
            ASPxTextBox tbxRateCharge = pageControl.FindControl("tbxRateCharge") as ASPxTextBox;
            airRef.RateCharge = tbxRateCharge.Text;
            ASPxSpinEdit tbxTotal = pageControl.FindControl("tbxTotal") as ASPxSpinEdit;
            airRef.Total = tbxTotal.Text;
            ASPxMemo tbxGoodsNature = pageControl.FindControl("tbxGoodsNature") as ASPxMemo;
            airRef.GoodsNature = tbxGoodsNature.Text;
            ASPxMemo tbxContentRemark = pageControl.FindControl("tbxContentRemark") as ASPxMemo;
            airRef.ContentRemark = tbxContentRemark.Text;
            ASPxTextBox tbxWeightChargeP = pageControl.FindControl("tbxWeightChargeP") as ASPxTextBox;
            airRef.WeightChargeP = tbxWeightChargeP.Text;
            ASPxTextBox tbxWeightChargeC = pageControl.FindControl("tbxWeightChargeC") as ASPxTextBox;
            airRef.WeightChargeC = tbxWeightChargeC.Text;
            ASPxTextBox tbxValuationChargeP = pageControl.FindControl("tbxValuationChargeP") as ASPxTextBox;
            airRef.ValuationChargeP = tbxValuationChargeP.Text;
            ASPxTextBox tbxValuationChargeC = pageControl.FindControl("tbxValuationChargeC") as ASPxTextBox;
            airRef.ValuationChargeC = tbxValuationChargeC.Text;
            ASPxTextBox tbxTaxP = pageControl.FindControl("tbxTaxP") as ASPxTextBox;
            airRef.TaxP = tbxTaxP.Text;
            ASPxTextBox tbxTaxC = pageControl.FindControl("tbxTaxC") as ASPxTextBox;
            airRef.TaxC = tbxTaxC.Text;
            ASPxTextBox tbxOtherAgentChargeP = pageControl.FindControl("tbxOtherAgentChargeP") as ASPxTextBox;
            airRef.OtherAgentChargeP = tbxOtherAgentChargeP.Text;
            ASPxTextBox tbxOtherAgentChargeC = pageControl.FindControl("tbxOtherAgentChargeC") as ASPxTextBox;
            airRef.OtherAgentChargeC = tbxOtherAgentChargeC.Text;
            ASPxTextBox tbxOtherCarrierChargeP = pageControl.FindControl("tbxOtherCarrierChargeP") as ASPxTextBox;
            airRef.OtherCarrierChargeP = tbxOtherCarrierChargeP.Text;
            ASPxTextBox tbxOtherCarrierChargeC = pageControl.FindControl("tbxOtherCarrierChargeC") as ASPxTextBox;
            airRef.OtherCarrierChargeC = tbxOtherCarrierChargeC.Text;
            ASPxTextBox tbxTotalPrepaid = pageControl.FindControl("tbxTotalPrepaid") as ASPxTextBox;
            airRef.TotalPrepaid = tbxTotalPrepaid.Text;
            ASPxTextBox tbxTotalCollect = pageControl.FindControl("tbxTotalCollect") as ASPxTextBox;
            airRef.TotalCollect = tbxTotalCollect.Text;
            ASPxTextBox tbxCurrencyRate = pageControl.FindControl("tbxCurrencyRate") as ASPxTextBox;
            airRef.CurrencyRate = tbxCurrencyRate.Text;
            ASPxTextBox tbxChargeDestCurrency = pageControl.FindControl("tbxChargeDestCurrency") as ASPxTextBox;
            airRef.ChargeDestCurrency = tbxChargeDestCurrency.Text;

            ASPxTextBox tbxOtherCharge1 = pageControl.FindControl("tbxOtherCharge1") as ASPxTextBox;
            airRef.OtherCharge1 = tbxOtherCharge1.Text;
            ASPxTextBox tbxOtherCharge1Currency = pageControl.FindControl("tbxOtherCharge1Currency") as ASPxTextBox;
            airRef.OtherCharge1Currency = tbxOtherCharge1Currency.Text;
            ASPxSpinEdit tbxOtherCharge1Amount = pageControl.FindControl("tbxOtherCharge1Amount") as ASPxSpinEdit;
            airRef.OtherCharge1Amount = SafeValue.SafeDecimal(tbxOtherCharge1Amount.Text);

            ASPxTextBox tbxOtherCharge2 = pageControl.FindControl("tbxOtherCharge2") as ASPxTextBox;
            airRef.OtherCharge2 = tbxOtherCharge2.Text;
            ASPxTextBox tbxOtherCharge2Currency = pageControl.FindControl("tbxOtherCharge2Currency") as ASPxTextBox;
            airRef.OtherCharge2Currency = tbxOtherCharge2Currency.Text;
            ASPxSpinEdit tbxOtherCharge2Amount = pageControl.FindControl("tbxOtherCharge2Amount") as ASPxSpinEdit;
            airRef.OtherCharge2Amount = SafeValue.SafeDecimal(tbxOtherCharge2Amount.Text);

            ASPxTextBox tbxOtherCharge3 = pageControl.FindControl("tbxOtherCharge3") as ASPxTextBox;
            airRef.OtherCharge3 = tbxOtherCharge3.Text;
            ASPxTextBox tbxOtherCharge3Currency = pageControl.FindControl("tbxOtherCharge3Currency") as ASPxTextBox;
            airRef.OtherCharge3Currency = tbxOtherCharge3Currency.Text;
            ASPxSpinEdit tbxOtherCharge3Amount = pageControl.FindControl("tbxOtherCharge3Amount") as ASPxSpinEdit;
            airRef.OtherCharge3Amount = SafeValue.SafeDecimal(tbxOtherCharge3Amount.Text);

            ASPxTextBox tbxOtherCharge4 = pageControl.FindControl("tbxOtherCharge4") as ASPxTextBox;
            airRef.OtherCharge4 = tbxOtherCharge4.Text;
            ASPxTextBox tbxOtherCharge4Currency = pageControl.FindControl("tbxOtherCharge4Currency") as ASPxTextBox;
            airRef.OtherCharge4Currency = tbxOtherCharge4Currency.Text;
            ASPxSpinEdit tbxOtherCharge4Amount = pageControl.FindControl("tbxOtherCharge4Amount") as ASPxSpinEdit;
            airRef.OtherCharge4Amount = SafeValue.SafeDecimal(tbxOtherCharge4Amount.Text);

            ASPxTextBox tbxOtherCharge5 = pageControl.FindControl("tbxOtherCharge5") as ASPxTextBox;
            airRef.OtherCharge5 = tbxOtherCharge5.Text;
            ASPxTextBox tbxOtherCharge5Currency = pageControl.FindControl("tbxOtherCharge5Currency") as ASPxTextBox;
            airRef.OtherCharge5Currency = tbxOtherCharge5Currency.Text;
            ASPxSpinEdit tbxOtherCharge5Amount = pageControl.FindControl("tbxOtherCharge5Amount") as ASPxSpinEdit;
            airRef.OtherCharge5Amount = SafeValue.SafeDecimal(tbxOtherCharge5Amount.Text);

            ASPxTextBox tbxSignatureShipper = pageControl.FindControl("tbxSignatureShipper") as ASPxTextBox;
            airRef.SignatureShipper = tbxSignatureShipper.Text;
            ASPxTextBox tbxExecuteDate = pageControl.FindControl("tbxExecuteDate") as ASPxTextBox;
            airRef.ExecuteDate = tbxExecuteDate.Text;
            ASPxTextBox tbxExecutePlace = pageControl.FindControl("tbxExecutePlace") as ASPxTextBox;
            airRef.ExecutePlace = tbxExecutePlace.Text;
            ASPxTextBox tbxSignatureIssuing = pageControl.FindControl("tbxSignatureIssuing") as ASPxTextBox;
            airRef.SignatureIssuing = tbxSignatureIssuing.Text;
            if (isNew)
            {
                airRef.CloseInd = "N";
                airRef.CreateBy = userId;
                airRef.CreateDateTime = DateTime.Now;
                airRef.RefNo = refNo.ToString();
                airRef.RefType = "ACT";
                airRef.UpdateBy = userId;
                airRef.UpdateDateTime = DateTime.Now;
                airRef.StatusCode = "USE";
                Manager.ORManager.StartTracking(airRef, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(airRef);
                C2Setup.SetNextNo(airRef.RefType,"AirCrossTrade", refNo,airRef.RefDate);

            }
            else
            {
                airRef.UpdateBy = userId;
                airRef.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(airRef, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(airRef);
            }
            Session["CtMastWhere"] = "RefNo='" + refNo + "'";
            this.dsImportRef.FilterExpression = Session["CtMastWhere"].ToString();
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
            ASPxTextBox txt_AgentName = this.grid_ref.FindEditFormTemplateControl("txt_AgentName") as ASPxTextBox;
            ASPxTextBox txt_CrAgentName = this.grid_ref.FindEditFormTemplateControl("txt_CrAgentName") as ASPxTextBox;
            ASPxTextBox txt_NvoccAgentName = pageControl.FindControl("txt_NvoccAgentName") as ASPxTextBox;
            ASPxTextBox txt_LocalCustName = this.grid_ref.FindEditFormTemplateControl("txt_LocalCustName") as ASPxTextBox;
            ASPxTextBox txt_WhName = pageControl.FindControl("txt_WhName") as ASPxTextBox;
            ASPxDateEdit refDate = this.grid_ref.FindEditFormTemplateControl("date_RefDate") as ASPxDateEdit;
            refDate.ReadOnly = true;
            refDate.BackColor = ((DevExpress.Web.ASPxEditors.ASPxTextBox)(this.grid_ref.FindEditFormTemplateControl("txt_RefN"))).BackColor;
            txt_LocalCustName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "LocalCust" }));
            txt_AgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "AgentId" }));
            txt_CrAgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "CarrierAgentId" }));
            txt_NvoccAgentName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "NvoccAgentId" }));
            txt_WhName.Text = EzshipHelper.GetPartyName(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "WareHouseId" }));
            int oid = SafeValue.SafeInt(this.grid_ref.GetRowValues(this.grid_ref.EditingRowVisibleIndex, new string[] { "Id" }),0);
            if (oid > 0)
            {
                string userId = HttpContext.Current.User.Identity.Name;
                ASPxLabel lab_StatusCode = pageControl.FindControl("lb_JobStatus") as ASPxLabel;
                string sql = string.Format("select StatusCode from air_ref  where Id={0}", oid);
                string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql));
                ASPxButton btn_VoidMaster = this.grid_ref.FindEditFormTemplateControl("btn_VoidMaster") as ASPxButton;
                ASPxButton btn_CloseJob = this.grid_ref.FindEditFormTemplateControl("btn_CloseJob") as ASPxButton;
                if (closeInd == "CLS")
                {
                    btn_CloseJob.Text = "Open Job";
                    lab_StatusCode.Text = "Close";
                }
                else if (closeInd == "CNL")
                {
                    btn_VoidMaster.Text = "Unvoid";
                    lab_StatusCode.Text = "Void";
                }
                else
                {
                    btn_CloseJob.Text = "Close Job";
                    lab_StatusCode.Text = "USE";
                    btn_VoidMaster.Text = "Void";
                }
            }
        }
    }

    protected void grid_ref_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid_ref.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox masterId = this.grid_ref.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        string userId = HttpContext.Current.User.Identity.Name;
        ASPxLabel closeIndStr = this.grid_ref.FindEditFormTemplateControl("lab_CloseInd") as ASPxLabel;
        ASPxTextBox txt_RefN = this.grid_ref.FindEditFormTemplateControl("txt_RefN") as ASPxTextBox;
        ASPxButton btn = this.grid_ref.FindControl("btn_CloseJob") as ASPxButton;
        string sql = string.Format("select StatusCode from air_ref  where Id={0}", SafeValue.SafeInt(masterId.Text, 0));
        string statusCode = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql));
        if (s == "VoidMaster")
        {
            #region void master
            string sql_cnt = string.Format("select count(SequenceId) from XAArInvoiceDet where MastType='ACT' and MastRefNo='{0}' ", txt_RefN.Text);
            int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_cnt), 0);
            if (cnt > 0)
            {
                e.Result = "Billing";
                return;
            }
            if (statusCode == "CNL")
            {
                sql = string.Format("update air_ref set StatusCode='USE' where RefNo='{0}'", txt_RefN.Text);
                string sql1 = string.Format("update air_job set StatusCode='USE' where RefNo='{0}'", txt_RefN.Text);
                int res = Manager.ORManager.ExecuteCommand(sql);
                int res1 = Manager.ORManager.ExecuteCommand(sql1);
                if (res > 0 && res1 > 0)
                {
                    EzshipLog.Log(txt_RefN.Text, "", "ACT", "Unvoid");
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
                string refType = "ACT";
                bool closeByEst = EzshipHelper.GetCloseEstInd(txt_RefN.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update air_ref set StatusCode='CNL' where RefNo='{0}'", txt_RefN.Text);
                    string sql1 = string.Format("update air_job set StatusCode='CNL' where RefNo='{0}'", txt_RefN.Text);
                    int res = Manager.ORManager.ExecuteCommand(sql);
                    int res1 = Manager.ORManager.ExecuteCommand(sql1);
                    if (res > 0 && res1 > 0)
                    {
                        EzshipLog.Log(txt_RefN.Text, "", "ACT", "Void");
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
                    e.Result = "NoMatch";
                }
            }
            #endregion
        }
        if (s == "CloseJob")
        {
            #region close job
            if (statusCode == "CLS")
            {
                sql = string.Format("update air_ref set StatusCode='USE' where RefNo='{0}'", txt_RefN.Text);
                string sql1 = string.Format("update air_job set StatusCode='USE' where RefNo='{0}'", txt_RefN.Text);
                int res = Manager.ORManager.ExecuteCommand(sql);
                int res1 = Manager.ORManager.ExecuteCommand(sql1);
                if (res > 0 && res1 > 0)
                {
                    EzshipLog.Log(txt_RefN.Text, "", "ACT", "Open");
                    e.Result = "Success";
                    //btn.Text = "Close Job";
                    //closeIndStr.Text = "N";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                string refType = "ACT";
                bool closeByEst = EzshipHelper.GetCloseEstInd(txt_RefN.Text, refType);
                if (closeByEst)
                {
                    sql = string.Format("update air_ref set StatusCode='CLS', CloseDate='{0}' ,CloseUser='{1}' where RefNo='{2}'", DateTime.Today.ToString("yyyy-MM-dd"), userId, txt_RefN.Text);
                    string sql1 = string.Format("update air_job set StatusCode='CLS' where RefNo='{0}'", txt_RefN.Text);

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    int res1 = Manager.ORManager.ExecuteCommand(sql1);
                    if (res > 0 && res1 > 0)
                    {
                        EzshipLog.Log(txt_RefN.Text, "", "ACT", "Close");
                        e.Result = "Success";
                        //btn.Text = "Open Job";
                        //closeIndStr.Text = "Y";
                    }
                    else
                    {
                        e.Result = "Fail";
                    }
                }
                else
                {
                    e.Result = "NoMatch";
                }
            }
            #endregion
        }
    }
    #region import job
    protected void grid_Export_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select RefNo from air_ref where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsImport.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }

    #endregion
    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from air_ref where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsInvoice.FilterExpression = "MastType='ACT' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select RefNo from air_ref where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
        this.dsVoucher.FilterExpression = "MastType='ACT' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    #endregion


    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select RefNo from air_ref where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "";
            this.dsCosting.FilterExpression = "JobType='ACT' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
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
        ASPxTextBox refNo = this.grid_ref.FindEditFormTemplateControl("txt_RefN") as ASPxTextBox;
        e.NewValues["JobType"] = "ACT";
        e.NewValues["RefNo"] = refNo.Text;
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxTextBox refNo = this.grid_ref.FindEditFormTemplateControl("txt_RefN") as ASPxTextBox;
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
        ASPxTextBox refNo = this.grid_ref.FindEditFormTemplateControl("txt_RefN") as ASPxTextBox;
        UpdateEstAmt(refNo.Text);
    }
    protected void grid_Cost_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxTextBox refNo = this.grid_ref.FindEditFormTemplateControl("txt_RefN") as ASPxTextBox;
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
        string sql = string.Format("Update air_ref set EstSaleAmt=(select sum(SaleLocAmt) from air_Costing where JobType='ACT' and RefNo=air_ref.RefNo),EstCostAmt=(select sum(CostLocAmt) from air_Costing where JobType='ACT' and RefNo=air_ref.refNo) where RefNo='{0}'", refNo);
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
        ASPxTextBox txtRefNo = this.grid_ref.FindEditFormTemplateControl("txt_RefN") as ASPxTextBox;
        this.dsJobPhoto.FilterExpression = "RefNo='" + SafeValue.SafeString(txtRefNo.Text, "") + "'";
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