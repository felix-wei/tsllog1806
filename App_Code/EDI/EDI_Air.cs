using C2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// EDI_Air 的摘要说明
/// </summary>
public class EDI_Air
{
	public EDI_Air()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static XmlDocument Export_Air(string refNo, string refType)
    {
        XmlDocument xmlDoc = new XmlDocument();

        XmlNode xmlNode = null;
        string sql_mast = string.Format(@"select * from [dbo].[air_ref] ref  WHERE (RefNo = '{0}' )", refNo);
        DataTable mast = ConnectSql.GetDataSet(sql_mast).Tables[0];
        string element = "";
        if (refType == "AI")
        {
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><AirImportRef></AirImportRef>");
            xmlNode = xmlDoc.SelectSingleNode("AirImportRef");
            element = "AirImport";
        }
        if (refType == "AE")
        {
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><AirExportRef></AirExportRef>");
            xmlNode = xmlDoc.SelectSingleNode("AirExportRef");
            element = "AirExport";
        }
        if (refType == "ACT")
        {
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><AirCrossTradeRef></AirCrossTradeRef>");
            xmlNode = xmlDoc.SelectSingleNode("AirCrossTradeRef");
            element = "AirCrossTrade";
        }
        XmlElement bill = null;
        for (int i = 0; i < mast.Rows.Count; i++)
        {
            #region ref
            bill = xmlDoc.CreateElement("" + element + "");
            bill.SetAttribute("RefNo", mast.Rows[i]["RefNo"].ToString());
            bill.SetAttribute("RefType", mast.Rows[i]["RefType"].ToString());
            bill.SetAttribute("MAWB", mast.Rows[i]["MAWB"].ToString());
            bill.SetAttribute("RefDate", SafeValue.SafeDate(mast.Rows[i]["RefDate"], DateTime.Now).ToString("yyyy-MM-dd"));
            bill.SetAttribute("CarrierBkgNo", mast.Rows[i]["CarrierBkgNo"].ToString());
            bill.SetAttribute("Agent", EzshipHelper.GetPartyName(mast.Rows[i]["AgentId"].ToString()));
            bill.SetAttribute("Customer", EzshipHelper.GetPartyName(mast.Rows[i]["LocalCust"].ToString()));
            bill.SetAttribute("Airline", EzshipHelper.GetPartyName(mast.Rows[i]["CarrierAgentId"].ToString()));
            bill.SetAttribute("DeparturePort", mast.Rows[i]["AirportName0"].ToString());
            bill.SetAttribute("ArrivePort", mast.Rows[i]["AirportName1"].ToString());
            bill.SetAttribute("DepartureDate", SafeValue.SafeDate(mast.Rows[i]["FlightDate0"], DateTime.Now).ToString("yyyy-MM-dd"));
            bill.SetAttribute("DepartureTime", mast.Rows[i]["FlightTime0"].ToString());
            bill.SetAttribute("ArriveDate", SafeValue.SafeDate(mast.Rows[i]["FlightDate1"], DateTime.Now).ToString("yyyy-MM-dd"));
            bill.SetAttribute("ArriveTime", mast.Rows[i]["FlightTime1"].ToString());
            bill.SetAttribute("AirlineCode1", mast.Rows[i]["AirlineCode1"].ToString());
            bill.SetAttribute("AirlineName1", mast.Rows[i]["AirlineName1"].ToString());
            bill.SetAttribute("FlightNo1", mast.Rows[i]["FlightNo1"].ToString());
            bill.SetAttribute("AirFlightDate1", SafeValue.SafeDate(mast.Rows[i]["AirFlightDate1"], DateTime.Now).ToString("yyyy-MM-dd"));
            bill.SetAttribute("AirFlightTime1", mast.Rows[i]["AirFlightTime1"].ToString());
            bill.SetAttribute("AirLinePortCode1", mast.Rows[i]["AirLinePortCode1"].ToString());
            bill.SetAttribute("AirLinePortName1", mast.Rows[i]["AirLinePortName1"].ToString());

            bill.SetAttribute("AirlineCode2", mast.Rows[i]["AirlineCode2"].ToString());
            bill.SetAttribute("AirlineName2", mast.Rows[i]["AirlineName2"].ToString());
            bill.SetAttribute("FlightNo2", mast.Rows[i]["FlightNo2"].ToString());
            bill.SetAttribute("FlightDate2", SafeValue.SafeDate(mast.Rows[i]["FlightDate2"], DateTime.Now).ToString("yyyy-MM-dd"));
            bill.SetAttribute("FlightTime2", mast.Rows[i]["FlightTime2"].ToString());
            bill.SetAttribute("AirportName2", mast.Rows[i]["AirportName2"].ToString());

            bill.SetAttribute("AirlineCode3", mast.Rows[i]["AirlineCode3"].ToString());
            bill.SetAttribute("AirlineName3", mast.Rows[i]["AirlineName3"].ToString());
            bill.SetAttribute("FlightNo3", mast.Rows[i]["FlightNo3"].ToString());
            bill.SetAttribute("FlightDate3", SafeValue.SafeDate(mast.Rows[i]["FlightDate3"], DateTime.Now).ToString("yyyy-MM-dd"));
            bill.SetAttribute("FlightTime3", mast.Rows[i]["FlightTime3"].ToString());
            bill.SetAttribute("AirportName3", mast.Rows[i]["AirportName3"].ToString());

            bill.SetAttribute("AirlineCode4", mast.Rows[i]["AirlineCode4"].ToString());
            bill.SetAttribute("AirlineName4", mast.Rows[i]["AirlineName4"].ToString());
            bill.SetAttribute("FlightNo4", mast.Rows[i]["FlightNo4"].ToString());
            bill.SetAttribute("FlightDate4", SafeValue.SafeDate(mast.Rows[i]["FlightDate4"], DateTime.Now).ToString("yyyy-MM-dd"));
            bill.SetAttribute("FlightTime4", mast.Rows[i]["FlightTime4"].ToString());
            bill.SetAttribute("AirportName4", mast.Rows[i]["AirportName4"].ToString());

            bill.SetAttribute("Consolidator", EzshipHelper.GetPartyName(mast.Rows[i]["NvoccAgentId"].ToString()));
            bill.SetAttribute("ConsolRef", mast.Rows[i]["NvoccBlNO"].ToString());
            bill.SetAttribute("Warehouse", EzshipHelper.GetPartyName(mast.Rows[i]["WareHouseId"].ToString()));
            bill.SetAttribute("Weight", mast.Rows[i]["Weight"].ToString());
            bill.SetAttribute("Volume", mast.Rows[i]["Volume"].ToString());
            bill.SetAttribute("PackageType", mast.Rows[i]["PackageType"].ToString());
            bill.SetAttribute("Qty", mast.Rows[i]["Qty"].ToString());
            bill.SetAttribute("Remarks", mast.Rows[i]["Remarks"].ToString());
            bill.SetAttribute("ShipperName", mast.Rows[i]["ShipperName"].ToString());
            bill.SetAttribute("IssuedBy", mast.Rows[i]["IssuedBy"].ToString());
            bill.SetAttribute("ConsigneeName", mast.Rows[i]["ConsigneeName"].ToString());
            bill.SetAttribute("AccountInfo", mast.Rows[i]["AccountInfo"].ToString());
            bill.SetAttribute("CarrierAgent", mast.Rows[i]["CarrierAgent"].ToString());
            bill.SetAttribute("AgentIATACode", mast.Rows[i]["AgentIATACode"].ToString());
            bill.SetAttribute("AgentAccountNo", mast.Rows[i]["AgentAccountNo"].ToString());
            bill.SetAttribute("AirportDeparture", mast.Rows[i]["AirportDeparture"].ToString());
            bill.SetAttribute("ConnDest1", mast.Rows[i]["ConnDest1"].ToString());
            bill.SetAttribute("ConnCarrier1", mast.Rows[i]["ConnCarrier1"].ToString());
            bill.SetAttribute("ConnDest2", mast.Rows[i]["ConnDest2"].ToString());
            bill.SetAttribute("ConnCarrier2", mast.Rows[i]["ConnCarrier2"].ToString());
            bill.SetAttribute("ConnDest3", mast.Rows[i]["ConnDest3"].ToString());
            bill.SetAttribute("ConnCarrier3", mast.Rows[i]["ConnCarrier3"].ToString());
            bill.SetAttribute("RequestedFlight", mast.Rows[i]["RequestedFlight"].ToString());
            bill.SetAttribute("RequestedDate", mast.Rows[i]["RequestedDate"].ToString());
            bill.SetAttribute("Currency", mast.Rows[i]["Currency"].ToString());
            bill.SetAttribute("ChgsCode", mast.Rows[i]["ChgsCode"].ToString());
            bill.SetAttribute("PPD1", mast.Rows[i]["PPD1"].ToString());
            bill.SetAttribute("COLL1", mast.Rows[i]["COLL1"].ToString());
            bill.SetAttribute("PPD2", mast.Rows[i]["PPD2"].ToString());
            bill.SetAttribute("COLL2", mast.Rows[i]["COLL2"].ToString());
            bill.SetAttribute("CarriageValue", mast.Rows[i]["CarriageValue"].ToString());
            bill.SetAttribute("CustomValue", mast.Rows[i]["CustomValue"].ToString());
            bill.SetAttribute("AmountInsurance", mast.Rows[i]["AmountInsurance"].ToString());
            bill.SetAttribute("HandlingInfo", mast.Rows[i]["HandlingInfo"].ToString());
            bill.SetAttribute("Piece", mast.Rows[i]["Piece"].ToString());
            bill.SetAttribute("GrossWeight", mast.Rows[i]["GrossWeight"].ToString());
            bill.SetAttribute("Unit", mast.Rows[i]["Unit"].ToString());
            bill.SetAttribute("RateClass", mast.Rows[i]["RateClass"].ToString());
            bill.SetAttribute("CommodityItemNo", mast.Rows[i]["CommodityItemNo"].ToString());
            bill.SetAttribute("ChargeableWeight", mast.Rows[i]["ChargeableWeight"].ToString());
            bill.SetAttribute("RateCharge", mast.Rows[i]["RateCharge"].ToString());
            bill.SetAttribute("Total", mast.Rows[i]["Total"].ToString());
            bill.SetAttribute("GoodsNature", mast.Rows[i]["GoodsNature"].ToString());
            bill.SetAttribute("ContentRemark", mast.Rows[i]["ContentRemark"].ToString());
            bill.SetAttribute("WeightChargeP", mast.Rows[i]["WeightChargeP"].ToString());
            bill.SetAttribute("ValuationChargeP", mast.Rows[i]["ValuationChargeP"].ToString());
            bill.SetAttribute("WeightChargeC", mast.Rows[i]["WeightChargeC"].ToString());
            bill.SetAttribute("ValuationChargeC", mast.Rows[i]["ValuationChargeC"].ToString());
            bill.SetAttribute("TaxP", mast.Rows[i]["TaxP"].ToString());
            bill.SetAttribute("TaxC", mast.Rows[i]["TaxC"].ToString());
            bill.SetAttribute("OtherAgentChargeP", mast.Rows[i]["OtherAgentChargeP"].ToString());
            bill.SetAttribute("OtherAgentChargeC", mast.Rows[i]["OtherAgentChargeC"].ToString());
            bill.SetAttribute("OtherCarrierChargeP", mast.Rows[i]["OtherCarrierChargeP"].ToString());
            bill.SetAttribute("OtherCarrierChargeC", mast.Rows[i]["OtherCarrierChargeC"].ToString());
            bill.SetAttribute("TotalPrepaid", mast.Rows[i]["TotalPrepaid"].ToString());
            bill.SetAttribute("TotalCollect", mast.Rows[i]["TotalCollect"].ToString());
            bill.SetAttribute("CurrencyRate", mast.Rows[i]["CurrencyRate"].ToString());
            bill.SetAttribute("ChargeDestCurrency", mast.Rows[i]["ChargeDestCurrency"].ToString());
            bill.SetAttribute("OtherCharge1", mast.Rows[i]["OtherCharge1"].ToString());
            bill.SetAttribute("OtherCharge2", mast.Rows[i]["OtherCharge2"].ToString());
            bill.SetAttribute("OtherCharge3", mast.Rows[i]["OtherCharge3"].ToString());
            bill.SetAttribute("SignatureShipper", mast.Rows[i]["SignatureShipper"].ToString());
            bill.SetAttribute("ExecuteDate", mast.Rows[i]["ExecuteDate"].ToString());
            bill.SetAttribute("ExecutePlace", mast.Rows[i]["ExecutePlace"].ToString());
            bill.SetAttribute("SignatureIssuing", mast.Rows[i]["SignatureIssuing"].ToString());
            bill.SetAttribute("AirportDestination", mast.Rows[i]["AirportDestination"].ToString());
            bill.SetAttribute("OtherCharge4", mast.Rows[i]["OtherCharge4"].ToString());
            bill.SetAttribute("OtherCharge5", mast.Rows[i]["OtherCharge5"].ToString());
            bill.SetAttribute("OtherCharge1Currency", mast.Rows[i]["OtherCharge1Currency"].ToString());
            bill.SetAttribute("OtherCharge2Currency", mast.Rows[i]["OtherCharge2Currency"].ToString());
            bill.SetAttribute("OtherCharge3Currency", mast.Rows[i]["OtherCharge3Currency"].ToString());
            bill.SetAttribute("OtherCharge4Currency", mast.Rows[i]["OtherCharge4Currency"].ToString());
            bill.SetAttribute("OtherCharge5Currency", mast.Rows[i]["OtherCharge5Currency"].ToString());
            bill.SetAttribute("OtherCharge1Amount", mast.Rows[i]["OtherCharge1Amount"].ToString());
            bill.SetAttribute("OtherCharge2Amount", mast.Rows[i]["OtherCharge2Amount"].ToString());
            bill.SetAttribute("OtherCharge3Amount", mast.Rows[i]["OtherCharge3Amount"].ToString());
            bill.SetAttribute("OtherCharge4Amount", mast.Rows[i]["OtherCharge4Amount"].ToString());
            bill.SetAttribute("OtherCharge5Amount", mast.Rows[i]["OtherCharge5Amount"].ToString());
            bill.SetAttribute("CloseInd", mast.Rows[i]["CloseInd"].ToString());
            bill.SetAttribute("remark", mast.Rows[i]["remark"].ToString());
            bill.SetAttribute("EstSaleAmt", mast.Rows[i]["EstSaleAmt"].ToString());
            bill.SetAttribute("EstCostAmt", mast.Rows[i]["EstCostAmt"].ToString());
            bill.SetAttribute("IssuedBy", mast.Rows[i]["IssuedBy"].ToString());
            bill.SetAttribute("StatusCode", mast.Rows[i]["StatusCode"].ToString());
            #endregion

            #region job
            string sql_job = string.Format(@"SELECT * FROM air_job WHERE (RefNo = '{0}') ", refNo);
            DataTable tab_job = ConnectSql.GetDataSet(sql_job).Tables[0];
            for (int j = 0; j < tab_job.Rows.Count; j++)
            {
                XmlElement hawb = xmlDoc.CreateElement("HAWB");
                hawb.SetAttribute("JobNo", tab_job.Rows[j]["JobNo"].ToString());
                hawb.SetAttribute("HAWB", tab_job.Rows[j]["HAWB"].ToString());
                string customerName = EzshipHelper.GetPartyName(tab_job.Rows[j]["CustomerId"].ToString());
                hawb.SetAttribute("Customer", customerName);
                hawb.SetAttribute("TsBkgRef", tab_job.Rows[j]["TsBkgRef"].ToString());
                hawb.SetAttribute("TsBkgUser", tab_job.Rows[j]["TsBkgUser"].ToString());
                hawb.SetAttribute("TsBkgTime", tab_job.Rows[j]["TsBkgTime"].ToString());
                hawb.SetAttribute("DeliveryDate", tab_job.Rows[j]["DeliveryDate"].ToString());
                hawb.SetAttribute("Weight", tab_job.Rows[j]["Weight"].ToString());
                hawb.SetAttribute("Volume", tab_job.Rows[j]["Volume"].ToString());
                hawb.SetAttribute("TsInd", tab_job.Rows[j]["TsInd"].ToString());
                hawb.SetAttribute("DoReadyInd", tab_job.Rows[j]["DoReadyInd"].ToString());
                hawb.SetAttribute("PackageType", tab_job.Rows[j]["PackageType"].ToString());
                hawb.SetAttribute("Qty", tab_job.Rows[j]["Qty"].ToString());
                hawb.SetAttribute("Remark", tab_job.Rows[j]["Remark"].ToString());
                hawb.SetAttribute("Marking", tab_job.Rows[j]["Marking"].ToString());
                hawb.SetAttribute("ShipperName", tab_job.Rows[j]["ShipperName"].ToString());
                hawb.SetAttribute("IssuedBy", tab_job.Rows[j]["IssuedBy"].ToString());
                hawb.SetAttribute("ConsigneeName", tab_job.Rows[j]["ConsigneeName"].ToString());
                hawb.SetAttribute("AccountInfo", tab_job.Rows[j]["AccountInfo"].ToString());
                hawb.SetAttribute("CarrierAgent", tab_job.Rows[j]["CarrierAgent"].ToString());
                hawb.SetAttribute("AgentIATACode", tab_job.Rows[j]["AgentIATACode"].ToString());
                hawb.SetAttribute("AgentAccountNo", tab_job.Rows[j]["AgentAccountNo"].ToString());
                hawb.SetAttribute("AirportDeparture", tab_job.Rows[j]["AirportDeparture"].ToString());
                hawb.SetAttribute("ConnDest1", tab_job.Rows[j]["ConnDest1"].ToString());
                hawb.SetAttribute("ConnCarrier1", tab_job.Rows[j]["ConnCarrier1"].ToString());
                hawb.SetAttribute("ConnDest2", tab_job.Rows[j]["ConnDest2"].ToString());
                hawb.SetAttribute("ConnCarrier2", tab_job.Rows[j]["ConnCarrier2"].ToString());
                hawb.SetAttribute("ConnDest3", tab_job.Rows[j]["ConnDest3"].ToString());
                hawb.SetAttribute("ConnCarrier3", tab_job.Rows[j]["ConnCarrier3"].ToString());
                hawb.SetAttribute("RequestedFlight", tab_job.Rows[j]["RequestedFlight"].ToString());
                hawb.SetAttribute("RequestedDate", tab_job.Rows[j]["RequestedDate"].ToString());
                hawb.SetAttribute("Currency", tab_job.Rows[j]["Currency"].ToString());
                hawb.SetAttribute("ChgsCode", tab_job.Rows[j]["ChgsCode"].ToString());
                hawb.SetAttribute("PPD1", tab_job.Rows[j]["PPD1"].ToString());
                hawb.SetAttribute("COLL1", tab_job.Rows[j]["COLL1"].ToString());
                hawb.SetAttribute("PPD2", tab_job.Rows[j]["PPD2"].ToString());
                hawb.SetAttribute("COLL2", tab_job.Rows[j]["COLL2"].ToString());
                hawb.SetAttribute("CarriageValue", tab_job.Rows[j]["CarriageValue"].ToString());
                hawb.SetAttribute("CustomValue", tab_job.Rows[j]["CustomValue"].ToString());
                hawb.SetAttribute("AmountInsurance", tab_job.Rows[j]["AmountInsurance"].ToString());
                hawb.SetAttribute("HandlingInfo", tab_job.Rows[j]["HandlingInfo"].ToString());
                hawb.SetAttribute("Piece", tab_job.Rows[j]["Piece"].ToString());
                hawb.SetAttribute("GrossWeight", tab_job.Rows[j]["GrossWeight"].ToString());
                hawb.SetAttribute("Unit", tab_job.Rows[j]["Unit"].ToString());
                hawb.SetAttribute("RateClass", tab_job.Rows[j]["RateClass"].ToString());
                hawb.SetAttribute("CommodityItemNo", tab_job.Rows[j]["CommodityItemNo"].ToString());
                hawb.SetAttribute("ChargeableWeight", tab_job.Rows[j]["ChargeableWeight"].ToString());
                hawb.SetAttribute("RateCharge", tab_job.Rows[j]["RateCharge"].ToString());
                hawb.SetAttribute("Total", tab_job.Rows[j]["Total"].ToString());
                hawb.SetAttribute("GoodsNature", tab_job.Rows[j]["GoodsNature"].ToString());
                hawb.SetAttribute("ContentRemark", tab_job.Rows[j]["ContentRemark"].ToString());
                hawb.SetAttribute("WeightChargeP", tab_job.Rows[j]["WeightChargeP"].ToString());
                hawb.SetAttribute("ValuationChargeP", tab_job.Rows[j]["ValuationChargeP"].ToString());
                hawb.SetAttribute("WeightChargeC", tab_job.Rows[j]["WeightChargeC"].ToString());
                hawb.SetAttribute("ValuationChargeC", tab_job.Rows[j]["ValuationChargeC"].ToString());
                hawb.SetAttribute("TaxP", tab_job.Rows[j]["TaxP"].ToString());
                hawb.SetAttribute("TaxC", tab_job.Rows[j]["TaxC"].ToString());
                hawb.SetAttribute("OtherAgentChargeP", tab_job.Rows[j]["OtherAgentChargeP"].ToString());
                hawb.SetAttribute("OtherAgentChargeC", tab_job.Rows[j]["OtherAgentChargeC"].ToString());
                hawb.SetAttribute("OtherCarrierChargeP", tab_job.Rows[j]["OtherCarrierChargeP"].ToString());
                hawb.SetAttribute("OtherCarrierChargeC", tab_job.Rows[j]["OtherCarrierChargeC"].ToString());
                hawb.SetAttribute("TotalPrepaid", tab_job.Rows[j]["TotalPrepaid"].ToString());
                hawb.SetAttribute("TotalCollect", tab_job.Rows[j]["TotalCollect"].ToString());
                hawb.SetAttribute("CurrencyRate", tab_job.Rows[j]["CurrencyRate"].ToString());
                hawb.SetAttribute("ChargeDestCurrency", tab_job.Rows[j]["ChargeDestCurrency"].ToString());
                hawb.SetAttribute("OtherCharge1", tab_job.Rows[j]["OtherCharge1"].ToString());
                hawb.SetAttribute("OtherCharge2", tab_job.Rows[j]["OtherCharge2"].ToString());
                hawb.SetAttribute("OtherCharge3", tab_job.Rows[j]["OtherCharge3"].ToString());
                hawb.SetAttribute("SignatureShipper", tab_job.Rows[j]["SignatureShipper"].ToString());
                hawb.SetAttribute("ExecuteDate", tab_job.Rows[j]["ExecuteDate"].ToString());
                hawb.SetAttribute("ExecutePlace", tab_job.Rows[j]["ExecutePlace"].ToString());
                hawb.SetAttribute("SignatureIssuing", tab_job.Rows[j]["SignatureIssuing"].ToString());
                hawb.SetAttribute("AirportDestination", tab_job.Rows[j]["AirportDestination"].ToString());
                hawb.SetAttribute("OtherCharge4", tab_job.Rows[j]["OtherCharge4"].ToString());
                hawb.SetAttribute("OtherCharge5", tab_job.Rows[j]["OtherCharge5"].ToString());
                hawb.SetAttribute("OtherCharge1Currency", tab_job.Rows[j]["OtherCharge1Currency"].ToString());
                hawb.SetAttribute("OtherCharge2Currency", tab_job.Rows[j]["OtherCharge2Currency"].ToString());
                hawb.SetAttribute("OtherCharge3Currency", tab_job.Rows[j]["OtherCharge3Currency"].ToString());
                hawb.SetAttribute("OtherCharge4Currency", tab_job.Rows[j]["OtherCharge4Currency"].ToString());
                hawb.SetAttribute("OtherCharge5Currency", tab_job.Rows[j]["OtherCharge5Currency"].ToString());
                hawb.SetAttribute("OtherCharge1Amount", tab_job.Rows[j]["OtherCharge1Amount"].ToString());
                hawb.SetAttribute("OtherCharge2Amount", tab_job.Rows[j]["OtherCharge2Amount"].ToString());
                hawb.SetAttribute("OtherCharge3Amount", tab_job.Rows[j]["OtherCharge3Amount"].ToString());
                hawb.SetAttribute("OtherCharge4Amount", tab_job.Rows[j]["OtherCharge4Amount"].ToString());
                hawb.SetAttribute("OtherCharge5Amount", tab_job.Rows[j]["OtherCharge5Amount"].ToString());
                hawb.SetAttribute("PermitRmk", tab_job.Rows[j]["PermitRmk"].ToString());
                hawb.SetAttribute("Transporter", tab_job.Rows[j]["HaulierName"].ToString());
                hawb.SetAttribute("CrNo", tab_job.Rows[j]["HaulierCrNo"].ToString());
                hawb.SetAttribute("Attention", tab_job.Rows[j]["HaulierAttention"].ToString());
                hawb.SetAttribute("DriverName", tab_job.Rows[j]["DriverName"].ToString());
                hawb.SetAttribute("DriverMobile", tab_job.Rows[j]["DriverMobile"].ToString());
                hawb.SetAttribute("DriverLicense", tab_job.Rows[j]["DriverLicense"].ToString());
                hawb.SetAttribute("CollectDate", tab_job.Rows[j]["HaulierCollectDate"].ToString());
                hawb.SetAttribute("VehicleNo", tab_job.Rows[j]["VehicleNo"].ToString());
                hawb.SetAttribute("VehicleType", tab_job.Rows[j]["VehicleType"].ToString());
                hawb.SetAttribute("DriverRemark", tab_job.Rows[j]["DriverRemark"].ToString());
                hawb.SetAttribute("CollectFrom", tab_job.Rows[j]["HaulierCollect"].ToString());
                hawb.SetAttribute("TruckTo", tab_job.Rows[j]["HaulierTruck"].ToString());
                hawb.SetAttribute("Instruction", tab_job.Rows[j]["HaulierRemark"].ToString());
                hawb.SetAttribute("PODBy", tab_job.Rows[j]["PODBy"].ToString());
                hawb.SetAttribute("PODTime", tab_job.Rows[j]["PODTime"].ToString());
                hawb.SetAttribute("PODRemark", tab_job.Rows[j]["PODRemark"].ToString());
                hawb.SetAttribute("Description", tab_job.Rows[j]["Description"].ToString());
                hawb.SetAttribute("StatusCode", tab_job.Rows[j]["StatusCode"].ToString());
                bill.AppendChild(hawb);
            }
            #endregion
            xmlNode.AppendChild(bill);
        }
        return xmlDoc;
    }

    public static string Import_AirImportFromImport(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        string refNo = "";
        string refType = "";
        string value = "Error";
        if (filePath.Length>0)
        {
            xmlDoc.Load(filePath);
            XmlNodeList nodeList = null;
            XmlNode nodeListJob = null;

            if (xmlDoc.SelectSingleNode("AirImportRef") != null)
            {
                nodeList = xmlDoc.SelectSingleNode("AirImportRef").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    #region
                    XmlElement elem = (XmlElement)xn;
                    AirImportRef airRef = new AirImportRef();
                    refType = elem.GetAttribute("RefType");
                    refNo = C2Setup.GetNextNo(refType, "AirImport", DateTime.Now);

                    airRef.RefNo = refNo;
                    airRef.RefType = refType;
                    airRef.Mawb = elem.GetAttribute("MAWB");
                    airRef.RefDate = SafeValue.SafeDate(elem.GetAttribute("RefDate"), DateTime.Now);
                    airRef.Weight = SafeValue.SafeDecimal(elem.GetAttribute("Weight"));
                    airRef.Volume = SafeValue.SafeDecimal(elem.GetAttribute("Volume"));
                    airRef.Qty = SafeValue.SafeInt(elem.GetAttribute("Qty"), 0);
                    airRef.PackageType = elem.GetAttribute("PackageType");
                    //if(){}
                    airRef.CarrierBkgNo = elem.GetAttribute("CarrierBkgNo");
                    airRef.AgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Agent"));
                    airRef.LocalCust = EzshipHelper.GetPartyId(elem.GetAttribute("Customer"));
                    airRef.CarrierAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Airline"));
                    airRef.AirportCode0 = EzshipHelper.GetPortAirCode(elem.GetAttribute("DeparturePort"));
                    airRef.AirportCode1 = EzshipHelper.GetPortAirCode(elem.GetAttribute("ArrivePort"));
                    airRef.AirportName0 = elem.GetAttribute("DeparturePort");
                    airRef.AirportName1 = elem.GetAttribute("ArrivePort");
                    airRef.FlightDate0 = SafeValue.SafeDate(elem.GetAttribute("DepartureDate"), DateTime.Now);
                    airRef.FlightTime0 = elem.GetAttribute("DepartureTime");
                    airRef.FlightDate1 = SafeValue.SafeDate(elem.GetAttribute("ArriveDate"), DateTime.Now);
                    airRef.FlightTime1 = elem.GetAttribute("ArriveTime");

                    airRef.AirlineCode1 = elem.GetAttribute("AirlineCode1");
                    airRef.AirlineName1 = elem.GetAttribute("AirlineName1");
                    airRef.FlightNo1 = elem.GetAttribute("FlightNo1");
                    airRef.AirFlightDate1 = SafeValue.SafeDate(elem.GetAttribute("AirFlightDate1"), DateTime.Now);
                    airRef.AirFlightTime1 = elem.GetAttribute("AirFlightTime1");
                    airRef.AirLinePortCode1 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirLinePortName1"));
                    airRef.AirLinePortName1 = elem.GetAttribute("AirLinePortName1");

                    airRef.AirlineCode2 = elem.GetAttribute("AirlineCode2");
                    airRef.AirlineName2 = elem.GetAttribute("AirlineName2");
                    airRef.FlightNo2 = elem.GetAttribute("FlightNo2");
                    airRef.FlightDate2 = SafeValue.SafeDate(elem.GetAttribute("FlightDate2"), DateTime.Now);
                    airRef.FlightTime2 = elem.GetAttribute("FlightTime2");
                    airRef.AirportCode2 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName2"));
                    airRef.AirportName2 = elem.GetAttribute("AirportName2");

                    airRef.AirlineCode3 = elem.GetAttribute("AirlineCode3");
                    airRef.AirlineName3 = elem.GetAttribute("AirlineName3");
                    airRef.FlightNo3 = elem.GetAttribute("FlightNo3");
                    airRef.FlightDate3 = SafeValue.SafeDate(elem.GetAttribute("FlightDate3"), DateTime.Now);
                    airRef.FlightTime3 = elem.GetAttribute("FlightTime3");
                    airRef.AirportCode3 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName3"));
                    airRef.AirportName3 = elem.GetAttribute("AirportName3");

                    airRef.AirlineCode4 = elem.GetAttribute("AirlineCode4");
                    airRef.AirlineName4 = elem.GetAttribute("AirlineName4");
                    airRef.FlightNo4 = elem.GetAttribute("FlightNo4");
                    airRef.FlightDate4 = SafeValue.SafeDate(elem.GetAttribute("FlightDate4"), DateTime.Now);
                    airRef.FlightTime4 = elem.GetAttribute("FlightTime4");
                    airRef.AirportCode4 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName4"));
                    airRef.AirportName4 = elem.GetAttribute("AirportName4");

                    airRef.NvoccAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Consolidator"));
                    airRef.NvoccBlNO = elem.GetAttribute("ConsolRef");
                    airRef.WareHouseId = EzshipHelper.GetPartyId(elem.GetAttribute("Warehouse"));
                    airRef.Remarks = elem.GetAttribute("Remarks");
                    airRef.ShipperName = elem.GetAttribute("ShipperName");
                    airRef.IssuedBy = elem.GetAttribute("IssuedBy");
                    airRef.ConsigneeName = elem.GetAttribute("ConsigneeName");
                    airRef.AccountInfo = elem.GetAttribute("AccountInfo");
                    airRef.CarrierAgent = elem.GetAttribute("CarrierAgent");
                    airRef.AgentIATACode = elem.GetAttribute("AgentIATACode");
                    airRef.AgentAccountNo = elem.GetAttribute("AgentAccountNo");
                    airRef.AirportDeparture = elem.GetAttribute("AirportDeparture");
                    airRef.ConnDest1 = elem.GetAttribute("ConnDest1");
                    airRef.ConnCarrier1 = elem.GetAttribute("ConnCarrier1");
                    airRef.ConnDest2 = elem.GetAttribute("ConnDest2");
                    airRef.ConnCarrier2 = elem.GetAttribute("ConnCarrier2");
                    airRef.ConnDest3 = elem.GetAttribute("ConnDest3");
                    airRef.ConnCarrier3 = elem.GetAttribute("ConnCarrier3");
                    airRef.RequestedFlight = elem.GetAttribute("RequestedFlight");
                    airRef.RequestedDate = elem.GetAttribute("RequestedDate");
                    airRef.AirportDestination = elem.GetAttribute("AirportDestination");
                    airRef.Currency = elem.GetAttribute("Currency");
                    airRef.ChgsCode = elem.GetAttribute("ChgsCode");
                    airRef.Ppd1 = elem.GetAttribute("PPD1");
                    airRef.Coll1 = elem.GetAttribute("COLL1");
                    airRef.Ppd2 = elem.GetAttribute("PPD2");
                    airRef.Coll2 = elem.GetAttribute("COLL2");
                    airRef.CarriageValue = elem.GetAttribute("CarriageValue");
                    airRef.CustomValue = elem.GetAttribute("CustomValue");
                    airRef.AmountInsurance = elem.GetAttribute("AmountInsurance");
                    airRef.HandlingInfo = elem.GetAttribute("HandlingInfo");
                    airRef.Piece = elem.GetAttribute("Piece");
                    airRef.GrossWeight = elem.GetAttribute("GrossWeight");
                    airRef.Unit = elem.GetAttribute("Unit");
                    airRef.RateClass = elem.GetAttribute("RateClass");
                    airRef.CommodityItemNo = elem.GetAttribute("CommodityItemNo");
                    airRef.ChargeableWeight = elem.GetAttribute("ChargeableWeight");
                    airRef.RateCharge = elem.GetAttribute("RateCharge");
                    airRef.Total = elem.GetAttribute("Total");
                    airRef.GoodsNature = elem.GetAttribute("GoodsNature");
                    airRef.ContentRemark = elem.GetAttribute("ContentRemark");
                    airRef.WeightChargeP = elem.GetAttribute("WeightChargeP");
                    airRef.ValuationChargeP = elem.GetAttribute("ValuationChargeP");
                    airRef.WeightChargeC = elem.GetAttribute("WeightChargeC");
                    airRef.ValuationChargeC = elem.GetAttribute("ValuationChargeC");
                    airRef.TaxP = elem.GetAttribute("TaxP");
                    airRef.TaxC = elem.GetAttribute("TaxC");
                    airRef.OtherAgentChargeP = elem.GetAttribute("OtherAgentChargeP");
                    airRef.OtherAgentChargeC = elem.GetAttribute("OtherAgentChargeC");
                    airRef.OtherCarrierChargeP = elem.GetAttribute("OtherCarrierChargeP");
                    airRef.OtherCarrierChargeC = elem.GetAttribute("OtherCarrierChargeC");
                    airRef.TotalPrepaid = elem.GetAttribute("TotalPrepaid");
                    airRef.TotalCollect = elem.GetAttribute("TotalCollect");
                    airRef.CurrencyRate = elem.GetAttribute("CurrencyRate");
                    airRef.ChargeDestCurrency = elem.GetAttribute("ChargeDestCurrency");
                    airRef.OtherCharge1 = elem.GetAttribute("OtherCharge1");
                    airRef.OtherCharge2 = elem.GetAttribute("OtherCharge2");
                    airRef.OtherCharge3 = elem.GetAttribute("OtherCharge3");
                    airRef.SignatureShipper = elem.GetAttribute("SignatureShipper");
                    airRef.ExecuteDate = elem.GetAttribute("ExecuteDate");
                    airRef.ExecutePlace = elem.GetAttribute("ExecutePlace");
                    airRef.SignatureIssuing = elem.GetAttribute("SignatureIssuing");
                    airRef.OtherCharge4 = elem.GetAttribute("OtherCharge4");
                    airRef.OtherCharge5 = elem.GetAttribute("OtherCharge5");
                    airRef.OtherCharge1Currency = elem.GetAttribute("OtherCharge1Currency");
                    airRef.OtherCharge2Currency = elem.GetAttribute("OtherCharge2Currency");
                    airRef.OtherCharge3Currency = elem.GetAttribute("OtherCharge3Currency");
                    airRef.OtherCharge4Currency = elem.GetAttribute("OtherCharge4Currency");
                    airRef.OtherCharge5Currency = elem.GetAttribute("OtherCharge5Currency");
                    airRef.OtherCharge1Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge1Amount"));
                    airRef.OtherCharge2Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge2Amount"));
                    airRef.OtherCharge3Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge3Amount"));
                    airRef.OtherCharge4Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge4Amount"));
                    airRef.OtherCharge5Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge5Amount"));
                    airRef.CloseInd = elem.GetAttribute("CloseInd");
                    airRef.CreateBy = EzshipHelper.GetUserName();
                    airRef.CreateDateTime = DateTime.Now;
                    airRef.RefNo = refNo.ToString();
                    airRef.RefType = refType;
                    airRef.UpdateBy = EzshipHelper.GetUserName();
                    airRef.UpdateDateTime = DateTime.Now;
                    airRef.StatusCode = elem.GetAttribute("StatusCode");
                    Manager.ORManager.StartTracking(airRef, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(airRef);
                    C2Setup.SetNextNo(refType, "AirImport", refNo, DateTime.Now);
                    #endregion
                    nodeListJob = xmlDoc.SelectSingleNode("AirImportRef").LastChild;
                    #region job
                    foreach (XmlNode xnJob in nodeListJob)
                    {
                        XmlElement elemJob = (XmlElement)xnJob;
                        C2.AirImport imp = new AirImport();
                        imp.JobNo = C2Setup.GetSubNo(refNo, refType);
                        if (elemJob.LocalName == "HAWB")
                        {
                            imp.Total = "0";
                            imp.RefType = refType;
                            imp.Hawb = elemJob.GetAttribute("HAWB");
                            imp.CustomerId = EzshipHelper.GetPartyId(elemJob.GetAttribute("Customer"));
                            imp.TsBkgRef = elemJob.GetAttribute("TsBkgRef");
                            imp.TsBkgUser = elemJob.GetAttribute("TsBkgUser");
                            imp.TsBkgTime = SafeValue.SafeDate(elemJob.GetAttribute("TsBkgTime"), DateTime.Now);
                            imp.DeliveryDate = SafeValue.SafeDate(elemJob.GetAttribute("DeliveryDate"), DateTime.Now);
                            imp.Weight = SafeValue.SafeDecimal(elemJob.GetAttribute("Weight"));
                            imp.Volume = SafeValue.SafeDecimal(elemJob.GetAttribute("Volume"));
                            imp.TsInd = elemJob.GetAttribute("TsInd");
                            imp.DoReadyInd = elemJob.GetAttribute("DoReadyInd");
                            imp.PackageType = elemJob.GetAttribute("PackageType");
                            imp.Qty = SafeValue.SafeInt(elemJob.GetAttribute("Qty"), 0);
                            imp.Remark = elemJob.GetAttribute("Remark");
                            imp.Marking = elemJob.GetAttribute("Marking");
                            imp.ShipperName = elemJob.GetAttribute("ShipperName");
                            imp.IssuedBy = elemJob.GetAttribute("IssuedBy");
                            imp.ConsigneeName = elemJob.GetAttribute("ConsigneeName");
                            imp.AccountInfo = elemJob.GetAttribute("AccountInfo");
                            imp.CarrierAgent = elemJob.GetAttribute("CarrierAgent");
                            imp.AgentIATACode = elemJob.GetAttribute("AgentIATACode");
                            imp.AgentAccountNo = elemJob.GetAttribute("AgentAccountNo");
                            imp.AirportDeparture = elemJob.GetAttribute("AirportDeparture");
                            imp.ConnDest1 = elemJob.GetAttribute("ConnDest1");
                            imp.ConnCarrier1 = elemJob.GetAttribute("ConnCarrier1");
                            imp.ConnDest2 = elemJob.GetAttribute("ConnDest2");
                            imp.ConnCarrier2 = elemJob.GetAttribute("ConnCarrier2");
                            imp.ConnDest3 = elemJob.GetAttribute("ConnDest3");
                            imp.ConnCarrier3 = elemJob.GetAttribute("ConnCarrier3");
                            imp.RequestedFlight = elemJob.GetAttribute("RequestedFlight");
                            imp.RequestedDate = elemJob.GetAttribute("RequestedDate");
                            imp.Currency = elemJob.GetAttribute("Currency");
                            imp.ChgsCode = elemJob.GetAttribute("CarriageValue");
                            imp.Ppd1 = elemJob.GetAttribute("PPD1");
                            imp.Coll1 = elemJob.GetAttribute("COLL1");
                            imp.Ppd2 = elemJob.GetAttribute("PPD2");
                            imp.Coll2 = elemJob.GetAttribute("COLL2");
                            imp.CarriageValue = elemJob.GetAttribute("CarriageValue");
                            imp.CustomValue = elemJob.GetAttribute("CustomValue");
                            imp.AmountInsurance = elemJob.GetAttribute("AmountInsurance");
                            imp.HandlingInfo = elemJob.GetAttribute("HandlingInfo");
                            imp.Piece = elemJob.GetAttribute("Piece");
                            imp.GrossWeight = elemJob.GetAttribute("GrossWeight");
                            imp.Unit = elemJob.GetAttribute("Unit");
                            imp.RateClass = elemJob.GetAttribute("RateClass");
                            imp.CommodityItemNo = elemJob.GetAttribute("CommodityItemNo");
                            imp.ChargeableWeight = elemJob.GetAttribute("ChargeableWeight");
                            imp.RateCharge = elemJob.GetAttribute("RateCharge");
                            imp.Total = elemJob.GetAttribute("Total");
                            imp.GoodsNature = elemJob.GetAttribute("GoodsNature");
                            imp.ContentRemark = elemJob.GetAttribute("ContentRemark");
                            imp.WeightChargeP = elemJob.GetAttribute("WeightChargeP");
                            imp.ValuationChargeP = elemJob.GetAttribute("ValuationChargeP");
                            imp.WeightChargeC = elemJob.GetAttribute("WeightChargeC");
                            imp.ValuationChargeC = elemJob.GetAttribute("ValuationChargeC");
                            imp.TaxP = elemJob.GetAttribute("TaxP");
                            imp.TaxC = elemJob.GetAttribute("TaxC");
                            imp.OtherAgentChargeP = elemJob.GetAttribute("OtherAgentChargeP");
                            imp.OtherAgentChargeC = elemJob.GetAttribute("OtherAgentChargeC");
                            imp.OtherCarrierChargeP = elemJob.GetAttribute("OtherCarrierChargeP");
                            imp.OtherCarrierChargeC = elemJob.GetAttribute("OtherCarrierChargeC");
                            imp.TotalPrepaid = elemJob.GetAttribute("TotalPrepaid");
                            imp.TotalCollect = elemJob.GetAttribute("TotalCollect");
                            imp.CurrencyRate = elemJob.GetAttribute("CurrencyRate");
                            imp.ChargeDestCurrency = elemJob.GetAttribute("ChargeDestCurrency");
                            imp.OtherCharge1 = elemJob.GetAttribute("OtherCharge1");
                            imp.OtherCharge2 = elemJob.GetAttribute("OtherCharge2");
                            imp.OtherCharge3 = elemJob.GetAttribute("OtherCharge3");
                            imp.SignatureShipper = elemJob.GetAttribute("SignatureShipper");
                            imp.ExecuteDate = elemJob.GetAttribute("ExecuteDate");
                            imp.ExecutePlace = elemJob.GetAttribute("ExecutePlace");
                            imp.SignatureIssuing = elemJob.GetAttribute("SignatureIssuing");
                            imp.AirportDestination = elemJob.GetAttribute("AirportDestination");
                            imp.OtherCharge4 = elemJob.GetAttribute("OtherCharge4");
                            imp.OtherCharge5 = elemJob.GetAttribute("OtherCharge5");
                            imp.OtherCharge1Currency = elemJob.GetAttribute("OtherCharge1Currency");
                            imp.OtherCharge2Currency = elemJob.GetAttribute("OtherCharge2Currency");
                            imp.OtherCharge3Currency = elemJob.GetAttribute("OtherCharge3Currency");
                            imp.OtherCharge4Currency = elemJob.GetAttribute("OtherCharge4Currency");
                            imp.OtherCharge5Currency = elemJob.GetAttribute("OtherCharge5Currency");
                            imp.OtherCharge1Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge1Amount"));
                            imp.OtherCharge2Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge2Amount"));
                            imp.OtherCharge3Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge3Amount"));
                            imp.OtherCharge4Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge4Amount"));
                            imp.OtherCharge5Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge5Amount"));
                            imp.CloseInd = elemJob.GetAttribute("CloseInd");
                            imp.PermitRmk = elemJob.GetAttribute("PermitRmk");
                            imp.HaulierName = elemJob.GetAttribute("Transporter");
                            imp.HaulierCrNo = elemJob.GetAttribute("CrNo");
                            imp.HaulierAttention = elemJob.GetAttribute("Attention");
                            imp.DriverName = elemJob.GetAttribute("DriverName");
                            imp.DriverMobile = elemJob.GetAttribute("DriverMobile");
                            imp.DriverLicense = elemJob.GetAttribute("DriverLicense");
                            imp.HaulierCollectDate = SafeValue.SafeDate(elemJob.GetAttribute("CollectDate"), DateTime.Now);
                            imp.VehicleNo = elemJob.GetAttribute("VehicleNo");
                            imp.VehicleType = elemJob.GetAttribute("VehicleType");
                            imp.DriverRemark = elemJob.GetAttribute("DriverRemark");
                            imp.HaulierCollect = elemJob.GetAttribute("CollectFrom");
                            imp.HaulierTruck = elemJob.GetAttribute("TruckTo");
                            imp.HaulierRemark = elemJob.GetAttribute("Instruction");
                            imp.VehicleNo = elemJob.GetAttribute("VehicleNo");

                            imp.StatusCode = elemJob.GetAttribute("StatusCode");
                            imp.Description = elemJob.GetAttribute("Description");
                            imp.CreateBy = EzshipHelper.GetUserName();
                            imp.CreateDateTime = DateTime.Now;
                            imp.RefNo = refNo;
                            C2.Manager.ORManager.StartTracking(imp, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(imp);
                        }
                    }
                    #endregion
                }
                value = "RefType=" + refType + " , " + "RefNo=" + refNo;
            }
        }
        return value;
    }
    public static string Import_AirImportFromExport(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string refNo = "";
        string refType = "";
        string value = "Error";
        if (filePath.Length>0)
        {
            xmlDoc.Load(filePath);
            XmlNodeList nodeList =null;
            XmlNode nodeListJob = null;
                
                if (xmlDoc.SelectSingleNode("AirExportRef")!=null)
                {
                    nodeList = xmlDoc.SelectSingleNode("AirExportRef").ChildNodes;
                    foreach (XmlNode xn in nodeList)
                    {
                        #region ref
                        XmlElement elem = (XmlElement)xn;
                        AirImportRef airRef = new AirImportRef();
                        refType = elem.GetAttribute("RefType");
                        if (refType == "AE")
                        {
                            refType = "AI";
                        }
                        refNo = C2Setup.GetNextNo(refType, "AirImport", DateTime.Now);

                        airRef.RefNo = refNo;
                        airRef.RefType = refType;
                        airRef.Mawb = elem.GetAttribute("MAWB");
                        airRef.RefDate = SafeValue.SafeDate(elem.GetAttribute("RefDate"), DateTime.Now);
                        airRef.Weight = SafeValue.SafeDecimal(elem.GetAttribute("Weight"));
                        airRef.Volume = SafeValue.SafeDecimal(elem.GetAttribute("Volume"));
                        airRef.Qty = SafeValue.SafeInt(elem.GetAttribute("Qty"), 0);
                        airRef.PackageType = elem.GetAttribute("PackageType");
                        //if(){}
                        airRef.CarrierBkgNo = elem.GetAttribute("CarrierBkgNo");
                        airRef.AgentId = "";
                        airRef.LocalCust = EzshipHelper.GetPartyId(elem.GetAttribute("Customer"));
                        airRef.CarrierAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Airline"));
                        airRef.AirportCode0 = EzshipHelper.GetPortAirCode(elem.GetAttribute("DeparturePort"));
                        airRef.AirportCode1 = EzshipHelper.GetPortAirCode(elem.GetAttribute("ArrivePort"));
                        airRef.AirportName0 = elem.GetAttribute("DeparturePort");
                        airRef.AirportName1 = elem.GetAttribute("ArrivePort");
                        airRef.FlightDate0 = SafeValue.SafeDate(elem.GetAttribute("DepartureDate"), DateTime.Now);
                        airRef.FlightTime0 = elem.GetAttribute("DepartureTime");
                        airRef.FlightDate1 = SafeValue.SafeDate(elem.GetAttribute("ArriveDate"), DateTime.Now);
                        airRef.FlightTime1 = elem.GetAttribute("ArriveTime");

                        airRef.AirlineCode1 = elem.GetAttribute("AirlineCode1");
                        airRef.AirlineName1 = elem.GetAttribute("AirlineName1");
                        airRef.FlightNo1 = elem.GetAttribute("FlightNo1");
                        airRef.AirFlightDate1 = SafeValue.SafeDate(elem.GetAttribute("AirFlightDate1"), DateTime.Now);
                        airRef.AirFlightTime1 = elem.GetAttribute("AirFlightTime1");
                        airRef.AirLinePortCode1 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirLinePortName1"));
                        airRef.AirLinePortName1 = elem.GetAttribute("AirLinePortName1");

                        airRef.AirlineCode2 = elem.GetAttribute("AirlineCode2");
                        airRef.AirlineName2 = elem.GetAttribute("AirlineName2");
                        airRef.FlightNo2 = elem.GetAttribute("FlightNo2");
                        airRef.FlightDate2 = SafeValue.SafeDate(elem.GetAttribute("FlightDate2"), DateTime.Now);
                        airRef.FlightTime2 = elem.GetAttribute("FlightTime2");
                        airRef.AirportCode2 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName2"));
                        airRef.AirportName2 = elem.GetAttribute("AirportName2");

                        airRef.AirlineCode3 = elem.GetAttribute("AirlineCode3");
                        airRef.AirlineName3 = elem.GetAttribute("AirlineName3");
                        airRef.FlightNo3 = elem.GetAttribute("FlightNo3");
                        airRef.FlightDate3 = SafeValue.SafeDate(elem.GetAttribute("FlightDate3"), DateTime.Now);
                        airRef.FlightTime3 = elem.GetAttribute("FlightTime3");
                        airRef.AirportCode3 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName3"));
                        airRef.AirportName3 = elem.GetAttribute("AirportName3");

                        airRef.AirlineCode4 = elem.GetAttribute("AirlineCode4");
                        airRef.AirlineName4 = elem.GetAttribute("AirlineName4");
                        airRef.FlightNo4 = elem.GetAttribute("FlightNo4");
                        airRef.FlightDate4 = SafeValue.SafeDate(elem.GetAttribute("FlightDate4"), DateTime.Now);
                        airRef.FlightTime4 = elem.GetAttribute("FlightTime4");
                        airRef.AirportCode4 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName4"));
                        airRef.AirportName4 = elem.GetAttribute("AirportName4");

                        airRef.NvoccAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Consolidator"));
                        airRef.NvoccBlNO = elem.GetAttribute("ConsolRef");
                        airRef.WareHouseId = EzshipHelper.GetPartyId(elem.GetAttribute("Warehouse"));
                        airRef.Remarks = elem.GetAttribute("Remarks");
                        airRef.ShipperName = elem.GetAttribute("ShipperName");
                        airRef.IssuedBy = elem.GetAttribute("IssuedBy");
                        airRef.ConsigneeName = elem.GetAttribute("ConsigneeName");
                        airRef.AccountInfo = elem.GetAttribute("AccountInfo");
                        airRef.CarrierAgent = elem.GetAttribute("CarrierAgent");
                        airRef.AgentIATACode = elem.GetAttribute("AgentIATACode");
                        airRef.AgentAccountNo = elem.GetAttribute("AgentAccountNo");
                        airRef.AirportDeparture = elem.GetAttribute("AirportDeparture");
                        airRef.ConnDest1 = elem.GetAttribute("ConnDest1");
                        airRef.ConnCarrier1 = elem.GetAttribute("ConnCarrier1");
                        airRef.ConnDest2 = elem.GetAttribute("ConnDest2");
                        airRef.ConnCarrier2 = elem.GetAttribute("ConnCarrier2");
                        airRef.ConnDest3 = elem.GetAttribute("ConnDest3");
                        airRef.ConnCarrier3 = elem.GetAttribute("ConnCarrier3");
                        airRef.RequestedFlight = elem.GetAttribute("RequestedFlight");
                        airRef.RequestedDate = elem.GetAttribute("RequestedDate");
                        airRef.AirportDestination = elem.GetAttribute("AirportDestination");
                        airRef.Currency = elem.GetAttribute("Currency");
                        airRef.ChgsCode = elem.GetAttribute("ChgsCode");
                        airRef.Ppd1 = elem.GetAttribute("PPD1");
                        airRef.Coll1 = elem.GetAttribute("COLL1");
                        airRef.Ppd2 = elem.GetAttribute("PPD2");
                        airRef.Coll2 = elem.GetAttribute("COLL2");
                        airRef.CarriageValue = elem.GetAttribute("CarriageValue");
                        airRef.CustomValue = elem.GetAttribute("CustomValue");
                        airRef.AmountInsurance = elem.GetAttribute("AmountInsurance");
                        airRef.HandlingInfo = elem.GetAttribute("HandlingInfo");
                        airRef.Piece = elem.GetAttribute("Piece");
                        airRef.GrossWeight = elem.GetAttribute("GrossWeight");
                        airRef.Unit = elem.GetAttribute("Unit");
                        airRef.RateClass = elem.GetAttribute("RateClass");
                        airRef.CommodityItemNo = elem.GetAttribute("CommodityItemNo");
                        airRef.ChargeableWeight = elem.GetAttribute("ChargeableWeight");
                        airRef.RateCharge = elem.GetAttribute("RateCharge");
                        airRef.Total = elem.GetAttribute("Total");
                        airRef.GoodsNature = elem.GetAttribute("GoodsNature");
                        airRef.ContentRemark = elem.GetAttribute("ContentRemark");
                        airRef.WeightChargeP = elem.GetAttribute("WeightChargeP");
                        airRef.ValuationChargeP = elem.GetAttribute("ValuationChargeP");
                        airRef.WeightChargeC = elem.GetAttribute("WeightChargeC");
                        airRef.ValuationChargeC = elem.GetAttribute("ValuationChargeC");
                        airRef.TaxP = elem.GetAttribute("TaxP");
                        airRef.TaxC = elem.GetAttribute("TaxC");
                        airRef.OtherAgentChargeP = elem.GetAttribute("OtherAgentChargeP");
                        airRef.OtherAgentChargeC = elem.GetAttribute("OtherAgentChargeC");
                        airRef.OtherCarrierChargeP = elem.GetAttribute("OtherCarrierChargeP");
                        airRef.OtherCarrierChargeC = elem.GetAttribute("OtherCarrierChargeC");
                        airRef.TotalPrepaid = elem.GetAttribute("TotalPrepaid");
                        airRef.TotalCollect = elem.GetAttribute("TotalCollect");
                        airRef.CurrencyRate = elem.GetAttribute("CurrencyRate");
                        airRef.ChargeDestCurrency = elem.GetAttribute("ChargeDestCurrency");
                        airRef.OtherCharge1 = elem.GetAttribute("OtherCharge1");
                        airRef.OtherCharge2 = elem.GetAttribute("OtherCharge2");
                        airRef.OtherCharge3 = elem.GetAttribute("OtherCharge3");
                        airRef.SignatureShipper = elem.GetAttribute("SignatureShipper");
                        airRef.ExecuteDate = elem.GetAttribute("ExecuteDate");
                        airRef.ExecutePlace = elem.GetAttribute("ExecutePlace");
                        airRef.SignatureIssuing = elem.GetAttribute("SignatureIssuing");
                        airRef.OtherCharge4 = elem.GetAttribute("OtherCharge4");
                        airRef.OtherCharge5 = elem.GetAttribute("OtherCharge5");
                        airRef.OtherCharge1Currency = elem.GetAttribute("OtherCharge1Currency");
                        airRef.OtherCharge2Currency = elem.GetAttribute("OtherCharge2Currency");
                        airRef.OtherCharge3Currency = elem.GetAttribute("OtherCharge3Currency");
                        airRef.OtherCharge4Currency = elem.GetAttribute("OtherCharge4Currency");
                        airRef.OtherCharge5Currency = elem.GetAttribute("OtherCharge5Currency");
                        airRef.OtherCharge1Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge1Amount"));
                        airRef.OtherCharge2Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge2Amount"));
                        airRef.OtherCharge3Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge3Amount"));
                        airRef.OtherCharge4Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge4Amount"));
                        airRef.OtherCharge5Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge5Amount"));
                        airRef.CloseInd = elem.GetAttribute("CloseInd");
                        airRef.CreateBy = EzshipHelper.GetUserName();
                        airRef.CreateDateTime = DateTime.Now;
                        airRef.RefNo = refNo.ToString();
                        airRef.RefType = refType;
                        airRef.UpdateBy = EzshipHelper.GetUserName();
                        airRef.UpdateDateTime = DateTime.Now;
                        airRef.StatusCode = elem.GetAttribute("StatusCode");
                        Manager.ORManager.StartTracking(airRef, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(airRef);
                        C2Setup.SetNextNo(refType, "AirImport", refNo, DateTime.Now);
                        #endregion
                        nodeListJob = xmlDoc.SelectSingleNode("AirExportRef").LastChild;
                        foreach (XmlNode xnJob in nodeListJob)
                        {
                            #region job
                            XmlElement elemJob = (XmlElement)xnJob;
                            C2.AirImport imp = new AirImport();
                            if (elemJob.LocalName == "HAWB")
                            {
                                imp.JobNo = C2Setup.GetSubNo(refNo, refType);

                                imp.Total = "0";
                                imp.RefType = refType;
                                imp.Hawb = elemJob.GetAttribute("HAWB");
                                imp.CustomerId = EzshipHelper.GetPartyId(elemJob.GetAttribute("Customer"));
                                imp.TsBkgRef = elemJob.GetAttribute("TsBkgRef");
                                imp.TsBkgUser = elemJob.GetAttribute("TsBkgUser");
                                imp.TsBkgTime = SafeValue.SafeDate(elemJob.GetAttribute("TsBkgTime"), DateTime.Now);
                                imp.DeliveryDate = SafeValue.SafeDate(elemJob.GetAttribute("DeliveryDate"), DateTime.Now);
                                imp.Weight = SafeValue.SafeDecimal(elemJob.GetAttribute("Weight"));
                                imp.Volume = SafeValue.SafeDecimal(elemJob.GetAttribute("Volume"));
                                imp.TsInd = elemJob.GetAttribute("TsInd");
                                imp.DoReadyInd = elemJob.GetAttribute("DoReadyInd");
                                imp.PackageType = elemJob.GetAttribute("PackageType");
                                imp.Qty = SafeValue.SafeInt(elemJob.GetAttribute("Qty"), 0);
                                imp.Remark = elemJob.GetAttribute("Remark");
                                imp.Marking = elemJob.GetAttribute("Marking");
                                imp.ShipperName = elemJob.GetAttribute("ShipperName");
                                imp.IssuedBy = elemJob.GetAttribute("IssuedBy");
                                imp.ConsigneeName = elemJob.GetAttribute("ConsigneeName");
                                imp.AccountInfo = elemJob.GetAttribute("AccountInfo");
                                imp.CarrierAgent = elemJob.GetAttribute("CarrierAgent");
                                imp.AgentIATACode = elemJob.GetAttribute("AgentIATACode");
                                imp.AgentAccountNo = elemJob.GetAttribute("AgentAccountNo");
                                imp.AirportDeparture = elemJob.GetAttribute("AirportDeparture");
                                imp.ConnDest1 = elemJob.GetAttribute("ConnDest1");
                                imp.ConnCarrier1 = elemJob.GetAttribute("ConnCarrier1");
                                imp.ConnDest2 = elemJob.GetAttribute("ConnDest2");
                                imp.ConnCarrier2 = elemJob.GetAttribute("ConnCarrier2");
                                imp.ConnDest3 = elemJob.GetAttribute("ConnDest3");
                                imp.ConnCarrier3 = elemJob.GetAttribute("ConnCarrier3");
                                imp.RequestedFlight = elemJob.GetAttribute("RequestedFlight");
                                imp.RequestedDate = elemJob.GetAttribute("RequestedDate");
                                imp.Currency = elemJob.GetAttribute("Currency");
                                imp.ChgsCode = elemJob.GetAttribute("CarriageValue");
                                imp.Ppd1 = elemJob.GetAttribute("PPD1");
                                imp.Coll1 = elemJob.GetAttribute("COLL1");
                                imp.Ppd2 = elemJob.GetAttribute("PPD2");
                                imp.Coll2 = elemJob.GetAttribute("COLL2");
                                imp.CarriageValue = elemJob.GetAttribute("CarriageValue");
                                imp.CustomValue = elemJob.GetAttribute("CustomValue");
                                imp.AmountInsurance = elemJob.GetAttribute("AmountInsurance");
                                imp.HandlingInfo = elemJob.GetAttribute("HandlingInfo");
                                imp.Piece = elemJob.GetAttribute("Piece");
                                imp.GrossWeight = elemJob.GetAttribute("GrossWeight");
                                imp.Unit = elemJob.GetAttribute("Unit");
                                imp.RateClass = elemJob.GetAttribute("RateClass");
                                imp.CommodityItemNo = elemJob.GetAttribute("CommodityItemNo");
                                imp.ChargeableWeight = elemJob.GetAttribute("ChargeableWeight");
                                imp.RateCharge = elemJob.GetAttribute("RateCharge");
                                imp.Total = elemJob.GetAttribute("Total");
                                imp.GoodsNature = elemJob.GetAttribute("GoodsNature");
                                imp.ContentRemark = elemJob.GetAttribute("ContentRemark");
                                imp.WeightChargeP = elemJob.GetAttribute("WeightChargeP");
                                imp.ValuationChargeP = elemJob.GetAttribute("ValuationChargeP");
                                imp.WeightChargeC = elemJob.GetAttribute("WeightChargeC");
                                imp.ValuationChargeC = elemJob.GetAttribute("ValuationChargeC");
                                imp.TaxP = elemJob.GetAttribute("TaxP");
                                imp.TaxC = elemJob.GetAttribute("TaxC");
                                imp.OtherAgentChargeP = elemJob.GetAttribute("OtherAgentChargeP");
                                imp.OtherAgentChargeC = elemJob.GetAttribute("OtherAgentChargeC");
                                imp.OtherCarrierChargeP = elemJob.GetAttribute("OtherCarrierChargeP");
                                imp.OtherCarrierChargeC = elemJob.GetAttribute("OtherCarrierChargeC");
                                imp.TotalPrepaid = elemJob.GetAttribute("TotalPrepaid");
                                imp.TotalCollect = elemJob.GetAttribute("TotalCollect");
                                imp.CurrencyRate = elemJob.GetAttribute("CurrencyRate");
                                imp.ChargeDestCurrency = elemJob.GetAttribute("ChargeDestCurrency");
                                imp.OtherCharge1 = elemJob.GetAttribute("OtherCharge1");
                                imp.OtherCharge2 = elemJob.GetAttribute("OtherCharge2");
                                imp.OtherCharge3 = elemJob.GetAttribute("OtherCharge3");
                                imp.SignatureShipper = elemJob.GetAttribute("SignatureShipper");
                                imp.ExecuteDate = elemJob.GetAttribute("ExecuteDate");
                                imp.ExecutePlace = elemJob.GetAttribute("ExecutePlace");
                                imp.SignatureIssuing = elemJob.GetAttribute("SignatureIssuing");
                                imp.AirportDestination = elemJob.GetAttribute("AirportDestination");
                                imp.OtherCharge4 = elemJob.GetAttribute("OtherCharge4");
                                imp.OtherCharge5 = elemJob.GetAttribute("OtherCharge5");
                                imp.OtherCharge1Currency = elemJob.GetAttribute("OtherCharge1Currency");
                                imp.OtherCharge2Currency = elemJob.GetAttribute("OtherCharge2Currency");
                                imp.OtherCharge3Currency = elemJob.GetAttribute("OtherCharge3Currency");
                                imp.OtherCharge4Currency = elemJob.GetAttribute("OtherCharge4Currency");
                                imp.OtherCharge5Currency = elemJob.GetAttribute("OtherCharge5Currency");
                                imp.OtherCharge1Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge1Amount"));
                                imp.OtherCharge2Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge2Amount"));
                                imp.OtherCharge3Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge3Amount"));
                                imp.OtherCharge4Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge4Amount"));
                                imp.OtherCharge5Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge5Amount"));
                                imp.CloseInd = elemJob.GetAttribute("CloseInd");
                                imp.PermitRmk = elemJob.GetAttribute("PermitRmk");
                                imp.HaulierName = elemJob.GetAttribute("Transporter");
                                imp.HaulierCrNo = elemJob.GetAttribute("CrNo");
                                imp.HaulierAttention = elemJob.GetAttribute("Attention");
                                imp.DriverName = elemJob.GetAttribute("DriverName");
                                imp.DriverMobile = elemJob.GetAttribute("DriverMobile");
                                imp.DriverLicense = elemJob.GetAttribute("DriverLicense");
                                imp.HaulierCollectDate = SafeValue.SafeDate(elemJob.GetAttribute("CollectDate"), DateTime.Now);
                                imp.VehicleNo = elemJob.GetAttribute("VehicleNo");
                                imp.VehicleType = elemJob.GetAttribute("VehicleType");
                                imp.DriverRemark = elemJob.GetAttribute("DriverRemark");
                                imp.HaulierCollect = elemJob.GetAttribute("CollectFrom");
                                imp.HaulierTruck = elemJob.GetAttribute("TruckTo");
                                imp.HaulierRemark = elemJob.GetAttribute("Instruction");
                                imp.VehicleNo = elemJob.GetAttribute("VehicleNo");
                                imp.Description = elemJob.GetAttribute("Description");
                                imp.StatusCode = elemJob.GetAttribute("StatusCode");
                                imp.CreateBy = EzshipHelper.GetUserName();
                                imp.CreateDateTime = DateTime.Now;
                                imp.RefNo = refNo;
                                C2.Manager.ORManager.StartTracking(imp, Wilson.ORMapper.InitialState.Inserted);
                                C2.Manager.ORManager.PersistChanges(imp);
                            }
                            #endregion
                        }
                    }
                    value = "RefType=" + refType + " , " + "RefNo=" + refNo;
            }
        }
        return value;
    }
    public static string Import_AirExportFromExport(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string refNo = "";
        string type = "";
        string refType = "";
        string value = "Error";
        if (filePath.Length>0)
        {
            xmlDoc.Load(filePath);
            XmlNodeList nodeList = null;
            XmlNode nodeListJob = null;
            if (xmlDoc.SelectSingleNode("AirExportRef") != null)
            {
                nodeList = xmlDoc.SelectSingleNode("AirExportRef").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    #region ref
                    XmlElement elem = (XmlElement)xn;
                    AirImportRef airRef = new AirImportRef();
                    refType = elem.GetAttribute("RefType");
                    if (refType == "AE")
                    {
                        type = "AirExport";
                        refNo = C2Setup.GetNextNo(refType, type, DateTime.Now);
                    }
                    airRef.RefNo = refNo;
                    airRef.RefType = refType;
                    airRef.Mawb = elem.GetAttribute("MAWB");
                    airRef.RefDate = SafeValue.SafeDate(elem.GetAttribute("RefDate"), DateTime.Now);
                    airRef.CarrierBkgNo = elem.GetAttribute("CarrierBkgNo");
                    airRef.AgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Agent"));
                    airRef.LocalCust = EzshipHelper.GetPartyId(elem.GetAttribute("Customer"));
                    airRef.Weight = SafeValue.SafeDecimal(elem.GetAttribute("Weight"));
                    airRef.Volume = SafeValue.SafeDecimal(elem.GetAttribute("Volume"));
                    airRef.Qty = SafeValue.SafeInt(elem.GetAttribute("Qty"), 0);
                    airRef.PackageType = elem.GetAttribute("PackageType");
                    airRef.CarrierAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Airline"));
                    airRef.AirportCode0 = EzshipHelper.GetPortAirCode(elem.GetAttribute("DeparturePort"));
                    airRef.AirportCode1 = EzshipHelper.GetPortAirCode(elem.GetAttribute("ArrivePort"));
                    airRef.AirportName0 = elem.GetAttribute("DeparturePort");
                    airRef.AirportName1 = elem.GetAttribute("ArrivePort");
                    airRef.FlightDate0 = SafeValue.SafeDate(elem.GetAttribute("DepartureDate"), DateTime.Now);
                    airRef.FlightTime0 = elem.GetAttribute("DepartureTime");
                    airRef.FlightDate1 = SafeValue.SafeDate(elem.GetAttribute("ArriveDate"), DateTime.Now);
                    airRef.FlightTime1 = elem.GetAttribute("ArriveTime");

                    airRef.AirlineCode1 = elem.GetAttribute("AirlineCode1");
                    airRef.AirlineName1 = elem.GetAttribute("AirlineName1");
                    airRef.FlightNo1 = elem.GetAttribute("FlightNo1");
                    airRef.AirFlightDate1 = SafeValue.SafeDate(elem.GetAttribute("AirFlightDate1"), DateTime.Now);
                    airRef.AirFlightTime1 = elem.GetAttribute("AirFlightTime1");
                    airRef.AirLinePortCode1 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirLinePortName1"));
                    airRef.AirLinePortName1 = elem.GetAttribute("AirLinePortName1");

                    airRef.AirlineCode2 = elem.GetAttribute("AirlineCode2");
                    airRef.AirlineName2 = elem.GetAttribute("AirlineName2");
                    airRef.FlightNo2 = elem.GetAttribute("FlightNo2");
                    airRef.FlightDate2 = SafeValue.SafeDate(elem.GetAttribute("FlightDate2"), DateTime.Now);
                    airRef.FlightTime2 = elem.GetAttribute("FlightTime2");
                    airRef.AirportCode2 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName2"));
                    airRef.AirportName2 = elem.GetAttribute("AirportName2");

                    airRef.AirlineCode3 = elem.GetAttribute("AirlineCode3");
                    airRef.AirlineName3 = elem.GetAttribute("AirlineName3");
                    airRef.FlightNo3 = elem.GetAttribute("FlightNo3");
                    airRef.FlightDate3 = SafeValue.SafeDate(elem.GetAttribute("FlightDate3"), DateTime.Now);
                    airRef.FlightTime3 = elem.GetAttribute("FlightTime3");
                    airRef.AirportCode3 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName3"));
                    airRef.AirportName3 = elem.GetAttribute("AirportName3");

                    airRef.AirlineCode4 = elem.GetAttribute("AirlineCode4");
                    airRef.AirlineName4 = elem.GetAttribute("AirlineName4");
                    airRef.FlightNo4 = elem.GetAttribute("FlightNo4");
                    airRef.FlightDate4 = SafeValue.SafeDate(elem.GetAttribute("FlightDate4"), DateTime.Now);
                    airRef.FlightTime4 = elem.GetAttribute("FlightTime4");
                    airRef.AirportCode4 = EzshipHelper.GetPortAirCode(elem.GetAttribute("AirportName4"));
                    airRef.AirportName4 = elem.GetAttribute("AirportName4");

                    airRef.NvoccAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Consolidator"));
                    airRef.NvoccBlNO = elem.GetAttribute("ConsolRef");
                    airRef.WareHouseId = EzshipHelper.GetPartyId(elem.GetAttribute("Warehouse"));
                    airRef.Remarks = elem.GetAttribute("Remarks");
                    airRef.ShipperName = elem.GetAttribute("ShipperName");
                    airRef.IssuedBy = elem.GetAttribute("IssuedBy");
                    airRef.ConsigneeName = elem.GetAttribute("ConsigneeName");
                    airRef.AccountInfo = elem.GetAttribute("AccountInfo");
                    airRef.CarrierAgent = elem.GetAttribute("CarrierAgent");
                    airRef.AgentIATACode = elem.GetAttribute("AgentIATACode");
                    airRef.AgentAccountNo = elem.GetAttribute("AgentAccountNo");
                    airRef.AirportDeparture = elem.GetAttribute("AirportDeparture");
                    airRef.ConnDest1 = elem.GetAttribute("ConnDest1");
                    airRef.ConnCarrier1 = elem.GetAttribute("ConnCarrier1");
                    airRef.ConnDest2 = elem.GetAttribute("ConnDest2");
                    airRef.ConnCarrier2 = elem.GetAttribute("ConnCarrier2");
                    airRef.ConnDest3 = elem.GetAttribute("ConnDest3");
                    airRef.ConnCarrier3 = elem.GetAttribute("ConnCarrier3");
                    airRef.RequestedFlight = elem.GetAttribute("RequestedFlight");
                    airRef.RequestedDate = elem.GetAttribute("RequestedDate");
                    airRef.Currency = elem.GetAttribute("Currency");
                    airRef.ChgsCode = elem.GetAttribute("ChgsCode");
                    airRef.Ppd1 = elem.GetAttribute("PPD1");
                    airRef.Coll1 = elem.GetAttribute("COLL1");
                    airRef.Ppd2 = elem.GetAttribute("PPD2");
                    airRef.Coll2 = elem.GetAttribute("COLL2");
                    airRef.CarriageValue = elem.GetAttribute("CarriageValue");
                    airRef.CustomValue = elem.GetAttribute("CustomValue");
                    airRef.AmountInsurance = elem.GetAttribute("AmountInsurance");
                    airRef.HandlingInfo = elem.GetAttribute("HandlingInfo");
                    airRef.Piece = elem.GetAttribute("Piece");
                    airRef.GrossWeight = elem.GetAttribute("GrossWeight");
                    airRef.Unit = elem.GetAttribute("Unit");
                    airRef.RateClass = elem.GetAttribute("RateClass");
                    airRef.CommodityItemNo = elem.GetAttribute("CommodityItemNo");
                    airRef.ChargeableWeight = elem.GetAttribute("ChargeableWeight");
                    airRef.RateCharge = elem.GetAttribute("RateCharge");
                    airRef.Total = elem.GetAttribute("Total");
                    airRef.GoodsNature = elem.GetAttribute("GoodsNature");
                    airRef.ContentRemark = elem.GetAttribute("ContentRemark");
                    airRef.WeightChargeP = elem.GetAttribute("WeightChargeP");
                    airRef.ValuationChargeP = elem.GetAttribute("ValuationChargeP");
                    airRef.WeightChargeC = elem.GetAttribute("WeightChargeC");
                    airRef.ValuationChargeC = elem.GetAttribute("ValuationChargeC");
                    airRef.TaxP = elem.GetAttribute("TaxP");
                    airRef.TaxC = elem.GetAttribute("TaxC");
                    airRef.OtherAgentChargeP = elem.GetAttribute("OtherAgentChargeP");
                    airRef.OtherAgentChargeC = elem.GetAttribute("OtherAgentChargeC");
                    airRef.OtherCarrierChargeP = elem.GetAttribute("OtherCarrierChargeP");
                    airRef.OtherCarrierChargeC = elem.GetAttribute("OtherCarrierChargeC");
                    airRef.TotalPrepaid = elem.GetAttribute("TotalPrepaid");
                    airRef.TotalCollect = elem.GetAttribute("TotalCollect");
                    airRef.CurrencyRate = elem.GetAttribute("CurrencyRate");
                    airRef.ChargeDestCurrency = elem.GetAttribute("ChargeDestCurrency");
                    airRef.OtherCharge1 = elem.GetAttribute("OtherCharge1");
                    airRef.OtherCharge2 = elem.GetAttribute("OtherCharge2");
                    airRef.OtherCharge3 = elem.GetAttribute("OtherCharge3");
                    airRef.SignatureShipper = elem.GetAttribute("SignatureShipper");
                    airRef.ExecuteDate = elem.GetAttribute("ExecuteDate");
                    airRef.ExecutePlace = elem.GetAttribute("ExecutePlace");
                    airRef.SignatureIssuing = elem.GetAttribute("SignatureIssuing");
                    airRef.AirportDestination = elem.GetAttribute("AirportDestination");
                    airRef.OtherCharge4 = elem.GetAttribute("OtherCharge4");
                    airRef.OtherCharge5 = elem.GetAttribute("OtherCharge5");
                    airRef.OtherCharge1Currency = elem.GetAttribute("OtherCharge1Currency");
                    airRef.OtherCharge2Currency = elem.GetAttribute("OtherCharge2Currency");
                    airRef.OtherCharge3Currency = elem.GetAttribute("OtherCharge3Currency");
                    airRef.OtherCharge4Currency = elem.GetAttribute("OtherCharge4Currency");
                    airRef.OtherCharge5Currency = elem.GetAttribute("OtherCharge5Currency");
                    airRef.OtherCharge1Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge1Amount"));
                    airRef.OtherCharge2Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge2Amount"));
                    airRef.OtherCharge3Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge3Amount"));
                    airRef.OtherCharge4Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge4Amount"));
                    airRef.OtherCharge5Amount = SafeValue.SafeDecimal(elem.GetAttribute("OtherCharge5Amount"));
                    airRef.CreateBy = EzshipHelper.GetUserName();
                    airRef.CreateDateTime = DateTime.Now;
                    airRef.RefNo = refNo.ToString();
                    airRef.RefType = refType;
                    airRef.UpdateBy = EzshipHelper.GetUserName();
                    airRef.UpdateDateTime = DateTime.Now;
                    airRef.StatusCode = elem.GetAttribute("StatusCode");
                    Manager.ORManager.StartTracking(airRef, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(airRef);
                    C2Setup.SetNextNo(refType, type, refNo, DateTime.Now);
                    #endregion

                    nodeListJob = xmlDoc.SelectSingleNode("AirExportRef").LastChild;
                    foreach (XmlNode xnJob in nodeListJob)
                    {
                        #region job
                        XmlElement elemJob = (XmlElement)xnJob;
                        C2.AirImport imp = new AirImport();
                        if (elemJob.LocalName == "HAWB")
                        {
                            imp.JobNo = C2Setup.GetSubNo(refNo, refType);

                            imp.Total = "0";
                            imp.RefType = refType;
                            imp.Hawb = elemJob.GetAttribute("HAWB");
                            imp.CustomerId = EzshipHelper.GetPartyId(elemJob.GetAttribute("Customer"));
                            imp.TsBkgRef = elemJob.GetAttribute("TsBkgRef");
                            imp.TsBkgUser = elemJob.GetAttribute("TsBkgUser");
                            imp.TsBkgTime = SafeValue.SafeDate(elemJob.GetAttribute("TsBkgTime"), DateTime.Now);
                            imp.DeliveryDate = SafeValue.SafeDate(elemJob.GetAttribute("DeliveryDate"), DateTime.Now);
                            imp.Weight = SafeValue.SafeDecimal(elemJob.GetAttribute("Weight"));
                            imp.Volume = SafeValue.SafeDecimal(elemJob.GetAttribute("Volume"));
                            imp.TsInd = elemJob.GetAttribute("TsInd");
                            imp.DoReadyInd = elemJob.GetAttribute("DoReadyInd");
                            imp.PackageType = elemJob.GetAttribute("PackageType");
                            imp.Qty = SafeValue.SafeInt(elemJob.GetAttribute("Qty"), 0);
                            imp.Remark = elemJob.GetAttribute("Remark");
                            imp.Marking = elemJob.GetAttribute("Marking");
                            imp.ShipperName = elemJob.GetAttribute("ShipperName");
                            imp.IssuedBy = elemJob.GetAttribute("IssuedBy");
                            imp.ConsigneeName = elemJob.GetAttribute("ConsigneeName");
                            imp.AccountInfo = elemJob.GetAttribute("AccountInfo");
                            imp.CarrierAgent = elemJob.GetAttribute("CarrierAgent");
                            imp.AgentIATACode = elemJob.GetAttribute("AgentIATACode");
                            imp.AgentAccountNo = elemJob.GetAttribute("AgentAccountNo");
                            imp.AirportDeparture = elemJob.GetAttribute("AirportDeparture");
                            imp.ConnDest1 = elemJob.GetAttribute("ConnDest1");
                            imp.ConnCarrier1 = elemJob.GetAttribute("ConnCarrier1");
                            imp.ConnDest2 = elemJob.GetAttribute("ConnDest2");
                            imp.ConnCarrier2 = elemJob.GetAttribute("ConnCarrier2");
                            imp.ConnDest3 = elemJob.GetAttribute("ConnDest3");
                            imp.ConnCarrier3 = elemJob.GetAttribute("ConnCarrier3");
                            imp.RequestedFlight = elemJob.GetAttribute("RequestedFlight");
                            imp.RequestedDate = elemJob.GetAttribute("RequestedDate");
                            imp.Currency = elemJob.GetAttribute("Currency");
                            imp.ChgsCode = elemJob.GetAttribute("CarriageValue");
                            imp.Ppd1 = elemJob.GetAttribute("PPD1");
                            imp.Coll1 = elemJob.GetAttribute("COLL1");
                            imp.Ppd2 = elemJob.GetAttribute("PPD2");
                            imp.Coll2 = elemJob.GetAttribute("COLL2");
                            imp.CarriageValue = elemJob.GetAttribute("CarriageValue");
                            imp.CustomValue = elemJob.GetAttribute("CustomValue");
                            imp.AmountInsurance = elemJob.GetAttribute("AmountInsurance");
                            imp.HandlingInfo = elemJob.GetAttribute("HandlingInfo");
                            imp.Piece = elemJob.GetAttribute("Piece");
                            imp.GrossWeight = elemJob.GetAttribute("GrossWeight");
                            imp.Unit = elemJob.GetAttribute("Unit");
                            imp.RateClass = elemJob.GetAttribute("RateClass");
                            imp.CommodityItemNo = elemJob.GetAttribute("CommodityItemNo");
                            imp.ChargeableWeight = elemJob.GetAttribute("ChargeableWeight");
                            imp.RateCharge = elemJob.GetAttribute("RateCharge");
                            imp.Total = elemJob.GetAttribute("Total");
                            imp.GoodsNature = elemJob.GetAttribute("GoodsNature");
                            imp.ContentRemark = elemJob.GetAttribute("ContentRemark");
                            imp.WeightChargeP = elemJob.GetAttribute("WeightChargeP");
                            imp.ValuationChargeP = elemJob.GetAttribute("ValuationChargeP");
                            imp.WeightChargeC = elemJob.GetAttribute("WeightChargeC");
                            imp.ValuationChargeC = elemJob.GetAttribute("ValuationChargeC");
                            imp.TaxP = elemJob.GetAttribute("TaxP");
                            imp.TaxC = elemJob.GetAttribute("TaxC");
                            imp.OtherAgentChargeP = elemJob.GetAttribute("OtherAgentChargeP");
                            imp.OtherAgentChargeC = elemJob.GetAttribute("OtherAgentChargeC");
                            imp.OtherCarrierChargeP = elemJob.GetAttribute("OtherCarrierChargeP");
                            imp.OtherCarrierChargeC = elemJob.GetAttribute("OtherCarrierChargeC");
                            imp.TotalPrepaid = elemJob.GetAttribute("TotalPrepaid");
                            imp.TotalCollect = elemJob.GetAttribute("TotalCollect");
                            imp.CurrencyRate = elemJob.GetAttribute("CurrencyRate");
                            imp.ChargeDestCurrency = elemJob.GetAttribute("ChargeDestCurrency");
                            imp.OtherCharge1 = elemJob.GetAttribute("OtherCharge1");
                            imp.OtherCharge2 = elemJob.GetAttribute("OtherCharge2");
                            imp.OtherCharge3 = elemJob.GetAttribute("OtherCharge3");
                            imp.SignatureShipper = elemJob.GetAttribute("SignatureShipper");
                            imp.ExecuteDate = elemJob.GetAttribute("ExecuteDate");
                            imp.ExecutePlace = elemJob.GetAttribute("ExecutePlace");
                            imp.SignatureIssuing = elemJob.GetAttribute("SignatureIssuing");
                            imp.AirportDestination = elemJob.GetAttribute("AirportDestination");
                            imp.OtherCharge4 = elemJob.GetAttribute("OtherCharge4");
                            imp.OtherCharge5 = elemJob.GetAttribute("OtherCharge5");
                            imp.OtherCharge1Currency = elemJob.GetAttribute("OtherCharge1Currency");
                            imp.OtherCharge2Currency = elemJob.GetAttribute("OtherCharge2Currency");
                            imp.OtherCharge3Currency = elemJob.GetAttribute("OtherCharge3Currency");
                            imp.OtherCharge4Currency = elemJob.GetAttribute("OtherCharge4Currency");
                            imp.OtherCharge5Currency = elemJob.GetAttribute("OtherCharge5Currency");
                            imp.OtherCharge1Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge1Amount"));
                            imp.OtherCharge2Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge2Amount"));
                            imp.OtherCharge3Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge3Amount"));
                            imp.OtherCharge4Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge4Amount"));
                            imp.OtherCharge5Amount = SafeValue.SafeDecimal(elemJob.GetAttribute("OtherCharge5Amount"));
                            imp.CloseInd = elemJob.GetAttribute("CloseInd");
                            imp.PermitRmk = elemJob.GetAttribute("PermitRmk");
                            imp.HaulierName = elemJob.GetAttribute("Transporter");
                            imp.HaulierCrNo = elemJob.GetAttribute("CrNo");
                            imp.HaulierAttention = elemJob.GetAttribute("Attention");
                            imp.DriverName = elemJob.GetAttribute("DriverName");
                            imp.DriverMobile = elemJob.GetAttribute("DriverMobile");
                            imp.DriverLicense = elemJob.GetAttribute("DriverLicense");
                            imp.HaulierCollectDate = SafeValue.SafeDate(elemJob.GetAttribute("CollectDate"), DateTime.Now);
                            imp.VehicleNo = elemJob.GetAttribute("VehicleNo");
                            imp.VehicleType = elemJob.GetAttribute("VehicleType");
                            imp.DriverRemark = elemJob.GetAttribute("DriverRemark");
                            imp.HaulierCollect = elemJob.GetAttribute("CollectFrom");
                            imp.HaulierTruck = elemJob.GetAttribute("TruckTo");
                            imp.HaulierRemark = elemJob.GetAttribute("Instruction");
                            imp.VehicleNo = elemJob.GetAttribute("VehicleNo");
                            imp.Description = elemJob.GetAttribute("Description");
                            imp.StatusCode = elemJob.GetAttribute("StatusCode");
                            imp.CreateBy = EzshipHelper.GetUserName();
                            imp.CreateDateTime = DateTime.Now;
                            imp.RefNo = refNo;
                            C2.Manager.ORManager.StartTracking(imp, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(imp);
                        }
                        #endregion
                    }
                }
                value = "RefType=" + refType + " , " + "RefNo=" + refNo;
            }

        }
        return value;
    }
}