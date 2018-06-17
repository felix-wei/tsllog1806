using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Data;
using C2;
using DevExpress.Web.ASPxUploadControl;

/// <summary>
/// EDI_Sea 的摘要说明
/// </summary>
public class EDI_Sea
{
    public EDI_Sea()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static XmlDocument Export_Sea(string refNo, string refType)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><SeaImportRef></SeaImportRef>");
        XmlNode xmlNode = null;

        string sql_mast = "";
        string element = "";
        string sql_job = "";
        string sql_Cont = "";
        if (refType == "SI")
        {
            sql_mast = string.Format(@"select * from [dbo].[SeaImportRef]  WHERE (RefNo = '{0}' )", refNo);
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><SeaImportRef></SeaImportRef>");
            xmlNode = xmlDoc.SelectSingleNode("SeaImportRef");
            element = "SeaImportRef";
            sql_job = string.Format(@"SELECT * FROM SeaImport WHERE (RefNo = '{0}') ORDER BY JobNo", refNo);
        }
        else
        {
            sql_mast = string.Format(@"select * from [dbo].[SeaExportRef]  WHERE (RefNo = '{0}' )", refNo);
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><SeaExportRef></SeaExportRef>");
            xmlNode = xmlDoc.SelectSingleNode("SeaExportRef");
            element = "SeaExportRef";
            sql_job = string.Format(@"SELECT * FROM SeaExport WHERE (RefNo = '{0}') ORDER BY JobNo", refNo);
        }

        DataTable mast = ConnectSql.GetDataSet(sql_mast).Tables[0];


        if (mast.Rows.Count == 0 && refNo.Length == 0)
        {
            return null;
        }
        XmlElement bill = null;
        for (int i = 0; i < mast.Rows.Count; i++)
        {
            #region Import
            bill = xmlDoc.CreateElement("" + element + "");
            bill.SetAttribute("RefNo", mast.Rows[i]["RefNo"].ToString());
            bill.SetAttribute("JobType", mast.Rows[i]["JobType"].ToString());
            bill.SetAttribute("RefDate", SafeValue.SafeDate(mast.Rows[i]["RefDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            bill.SetAttribute("PolCode", mast.Rows[i]["Pol"].ToString());
            bill.SetAttribute("Pol", EzshipHelper.GetPortName(mast.Rows[i]["Pol"].ToString()));
            bill.SetAttribute("PodCode", mast.Rows[i]["Pod"].ToString());
            bill.SetAttribute("Pod", EzshipHelper.GetPortName(mast.Rows[i]["Pod"].ToString()));
            string vesVoy = mast.Rows[i]["Vessel"].ToString() + "/" + mast.Rows[i]["Voyage"].ToString();
            bill.SetAttribute("VesVoy", vesVoy);
            bill.SetAttribute("Eta", SafeValue.SafeDate(mast.Rows[i]["Eta"], DateTime.Today).ToString("yyyy-MM-dd"));
            bill.SetAttribute("Etd", SafeValue.SafeDate(mast.Rows[i]["Etd"], DateTime.Today).ToString("yyyy-MM-dd"));
            bill.SetAttribute("OblNo", mast.Rows[i]["OblNo"].ToString());
            bill.SetAttribute("CrBkgNo", mast.Rows[i]["CrBkgNo"].ToString());
            bill.SetAttribute("NvoccBl", mast.Rows[i]["NvoccBl"].ToString());
            string Agent = EzshipHelper.GetPartyName(mast.Rows[i]["AgentId"].ToString());
            bill.SetAttribute("Agent", EzshipHelper.GetPartyName(mast.Rows[i]["AgentId"].ToString()));
            bill.SetAttribute("CrAgent", EzshipHelper.GetPartyName(mast.Rows[i]["CrAgentId"].ToString()));
            bill.SetAttribute("Warehouse", EzshipHelper.GetPartyName(mast.Rows[i]["WarehouseId"].ToString()));
            bill.SetAttribute("NvoccAgent", EzshipHelper.GetPartyName(mast.Rows[i]["NvoccAgentId"].ToString()));
            if (refType == "SI")
            {
                sql_Cont = string.Format(@"select * from SeaImportMkg where RefNo='{0}' and MkgType='Cont'", mast.Rows[i]["RefNo"].ToString());

                bill.SetAttribute("Permit", mast.Rows[i]["PermitRemark"].ToString());
                bill.SetAttribute("ForwardAgent", EzshipHelper.GetPartyName(mast.Rows[i]["ForwardAgentId"].ToString()));
                bill.SetAttribute("Transport", EzshipHelper.GetPartyName(mast.Rows[i]["TransportId"].ToString()));

            }
            else
            {
                sql_Cont = string.Format(@"select * from SeaExportMkg where RefNo='{0}' and MkgType='Cont'", mast.Rows[i]["RefNo"].ToString());
                bill.SetAttribute("FinDest", mast.Rows[i]["FinDest"].ToString());
                bill.SetAttribute("OblTerm", mast.Rows[i]["OblTerm"].ToString());
                bill.SetAttribute("EtaDest", SafeValue.SafeDate(mast.Rows[i]["EtaDest"], DateTime.Today).ToString("yyyy-MM-dd"));
                bill.SetAttribute("ExpressBl", mast.Rows[i]["ExpressBl"].ToString());
                bill.SetAttribute("TransitDay", mast.Rows[i]["TransitDay"].ToString());
                bill.SetAttribute("StuffDate", SafeValue.SafeDate(mast.Rows[i]["StuffDate"], DateTime.Today).ToString("yyyy-MM-dd"));
                bill.SetAttribute("SchFaxInd", mast.Rows[i]["SchFaxInd"].ToString());
                bill.SetAttribute("PortnetNo", mast.Rows[i]["PortnetNo"].ToString());
                bill.SetAttribute("SchFaxInd", mast.Rows[i]["SchFaxInd"].ToString());
                bill.SetAttribute("PoNo", mast.Rows[i]["PoNo"].ToString());
            }
            bill.SetAttribute("LocalCustName", EzshipHelper.GetPartyName(mast.Rows[i]["LocalCust"].ToString()));
            bill.SetAttribute("Wt", mast.Rows[i]["Weight"].ToString());
            bill.SetAttribute("M3", mast.Rows[i]["Volume"].ToString());
            bill.SetAttribute("Qty", mast.Rows[i]["Qty"].ToString());
            bill.SetAttribute("PackageType", mast.Rows[i]["PackageType"].ToString());
            bill.SetAttribute("RefCurrency", mast.Rows[i]["CurrencyId"].ToString());
            bill.SetAttribute("ExRate", mast.Rows[i]["ExRate"].ToString());
            bill.SetAttribute("StatusCode", mast.Rows[i]["StatusCode"].ToString());
            bill.SetAttribute("SShipperRemark", mast.Rows[i]["SShipperRemark"].ToString());
            bill.SetAttribute("SAgentRemark", mast.Rows[i]["SAgentRemark"].ToString());
            bill.SetAttribute("SConsigneeRemark", mast.Rows[i]["SConsigneeRemark"].ToString());
            bill.SetAttribute("SNotifyPartyRemark", mast.Rows[i]["SNotifyPartyRemark"].ToString());
            bill.SetAttribute("HaulierName", mast.Rows[i]["HaulierName"].ToString());
            bill.SetAttribute("HaulierCrNo", mast.Rows[i]["HaulierCrNo"].ToString());
            bill.SetAttribute("HaulierFax", mast.Rows[i]["HaulierFax"].ToString());
            bill.SetAttribute("DriverName", mast.Rows[i]["DriverName"].ToString());
            bill.SetAttribute("DriverMobile", mast.Rows[i]["DriverMobile"].ToString());
            bill.SetAttribute("DriverLicense", mast.Rows[i]["DriverLicense"].ToString());
            bill.SetAttribute("VehicleNo", mast.Rows[i]["VehicleNo"].ToString());
            bill.SetAttribute("VehicleType", mast.Rows[i]["VehicleType"].ToString());
            bill.SetAttribute("DriverRemark", mast.Rows[i]["DriverRemark"].ToString());
            bill.SetAttribute("HaulierAttention", mast.Rows[i]["HaulierAttention"].ToString());
            bill.SetAttribute("HaulierCollect", mast.Rows[i]["HaulierCollect"].ToString());
            bill.SetAttribute("HaulierCollectDate", SafeValue.SafeDate(mast.Rows[i]["HaulierCollectDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            bill.SetAttribute("HaulierCollectTime", mast.Rows[i]["HaulierCollectTime"].ToString());
            bill.SetAttribute("HaulierDeliveryDate", SafeValue.SafeDate(mast.Rows[i]["HaulierDeliveryDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            bill.SetAttribute("HaulierDeliveryTime", mast.Rows[i]["HaulierDeliveryTime"].ToString());
            bill.SetAttribute("HaulierRemark", mast.Rows[i]["HaulierRemark"].ToString());
            bill.SetAttribute("HaulierTruck", mast.Rows[i]["HaulierTruck"].ToString());
            bill.SetAttribute("HaulierSendTo", mast.Rows[i]["HaulierSendTo"].ToString());
            bill.SetAttribute("HaulierStuffBy", mast.Rows[i]["HaulierStuffBy"].ToString());
            bill.SetAttribute("HaulierCoload", mast.Rows[i]["HaulierCoload"].ToString());
            bill.SetAttribute("HaulierPerson", mast.Rows[i]["HaulierPerson"].ToString());
            bill.SetAttribute("HaulierPersonTel", mast.Rows[i]["HaulierPersonTel"].ToString());
            bill.SetAttribute("Remark", mast.Rows[i]["Remark"].ToString());
            bill.SetAttribute("PermitRmk", mast.Rows[i]["PermitRmk"].ToString());

            bill.SetAttribute("RefType", mast.Rows[i]["RefType"].ToString());
            bill.SetAttribute("StatusCode", mast.Rows[i]["StatusCode"].ToString());


            DataTable tab_Cont = ConnectSql.GetDataSet(sql_Cont).Tables[0];

            for (int k = 0; k < tab_Cont.Rows.Count; k++)
            {
                XmlElement container = xmlDoc.CreateElement("Container");

                string mkgType = tab_Cont.Rows[k]["MkgType"].ToString();
                container.SetAttribute("MkgType", mkgType);
                container.SetAttribute("ContainerNo", tab_Cont.Rows[k]["ContainerNo"].ToString());
                container.SetAttribute("SealNo", tab_Cont.Rows[k]["SealNo"].ToString());
                container.SetAttribute("ContainerType", tab_Cont.Rows[k]["ContainerType"].ToString());
                container.SetAttribute("StatusCode", tab_Cont.Rows[k]["StatusCode"].ToString());
                bill.AppendChild(container);
            }

            #endregion

            #region SeaImport/SeaExport

            DataTable tab_job = ConnectSql.GetDataSet(sql_job).Tables[0];
            for (int j = 0; j < tab_job.Rows.Count; j++)
            {
                XmlElement item = xmlDoc.CreateElement("House");
                item.SetAttribute("JobNo", tab_job.Rows[j]["JobNo"].ToString());
                item.SetAttribute("HblNo", tab_job.Rows[j]["HblNo"].ToString());
                item.SetAttribute("Remark", tab_job.Rows[j]["Remark"].ToString());
                item.SetAttribute("CustomerName", EzshipHelper.GetPartyName(tab_job.Rows[j]["CustomerId"].ToString()));
                if (refType == "SI")
                {
                    item.SetAttribute("Weight", tab_job.Rows[j]["Weight"].ToString());
                    item.SetAttribute("Volume", tab_job.Rows[j]["Volume"].ToString());
                    item.SetAttribute("Qty", tab_job.Rows[j]["Qty"].ToString());
                    item.SetAttribute("PackageType", tab_job.Rows[j]["PackageType"].ToString());
                    item.SetAttribute("ExpressBl", tab_job.Rows[j]["ExpressBl"].ToString());
                    item.SetAttribute("Forwarding", EzshipHelper.GetPartyName(tab_job.Rows[j]["ForwardingId"].ToString()));
                    item.SetAttribute("CltFrmFrom", EzshipHelper.GetPartyName(tab_job.Rows[j]["CltFrmId"].ToString()));
                    item.SetAttribute("DeliveryTo", EzshipHelper.GetPartyName(tab_job.Rows[j]["DeliveryToId"].ToString()));
                    item.SetAttribute("DeliveryDate", SafeValue.SafeDate(tab_job.Rows[j]["DeliveryDate"], DateTime.Today).ToString("yyyy-MM-dd"));
                    item.SetAttribute("DoRealeaseTo", tab_job.Rows[j]["DoRealeaseTo"].ToString());
                    item.SetAttribute("TruckingInd", tab_job.Rows[j]["TruckingInd"].ToString());
                    item.SetAttribute("DoReadyInd", tab_job.Rows[j]["DoReadyInd"].ToString());
                    item.SetAttribute("FrCollectInd", tab_job.Rows[j]["FrCollectInd"].ToString());
                    item.SetAttribute("CollectCurrency", tab_job.Rows[j]["CollectCurrency"].ToString());
                    item.SetAttribute("CollectExRate", tab_job.Rows[j]["CollectExRate"].ToString());
                    item.SetAttribute("CollectAmount", tab_job.Rows[j]["CollectAmount"].ToString());
                    item.SetAttribute("ValueCurrency", tab_job.Rows[j]["ValueCurrency"].ToString());
                    item.SetAttribute("ValueExRate", tab_job.Rows[j]["ValueExRate"].ToString());
                    item.SetAttribute("ValueAmt", tab_job.Rows[j]["ValueAmt"].ToString());

                    item.SetAttribute("RateForklift", tab_job.Rows[j]["rateForklift"].ToString());
                    item.SetAttribute("RateProcess", tab_job.Rows[j]["rateProcess"].ToString());
                    item.SetAttribute("RateTracing", tab_job.Rows[j]["rateTracing"].ToString());
                    item.SetAttribute("RateWarehouse", tab_job.Rows[j]["rateWarehouse"].ToString());
                    item.SetAttribute("RateAdmin", tab_job.Rows[j]["rateAdmin"].ToString());
                    item.SetAttribute("FlagNomination", tab_job.Rows[j]["flagNomination"].ToString());
                    item.SetAttribute("TsInd", tab_job.Rows[j]["TsInd"].ToString());
                    item.SetAttribute("TsPod", tab_job.Rows[j]["TsPod"].ToString());
                    item.SetAttribute("TsPortFinName", tab_job.Rows[j]["TsPortFinName"].ToString());
                    item.SetAttribute("TsAgent", EzshipHelper.GetPartyName(tab_job.Rows[j]["TsAgentId"].ToString()));
                    item.SetAttribute("BkgNo", tab_job.Rows[j]["TsBkgNo"].ToString());
                    item.SetAttribute("TsVessel", tab_job.Rows[j]["TsVessel"].ToString());
                    item.SetAttribute("TsVoyage", tab_job.Rows[j]["TsVoyage"].ToString());
                    item.SetAttribute("TsEtd", SafeValue.SafeDate(tab_job.Rows[j]["TsEtd"], DateTime.Today).ToString("yyyy-MM-dd"));
                    item.SetAttribute("TsEta", SafeValue.SafeDate(tab_job.Rows[j]["TsEta"], DateTime.Today).ToString("yyyy-MM-dd"));
                    item.SetAttribute("TsColoader", tab_job.Rows[j]["TsColoader"].ToString());
                    item.SetAttribute("TsRemark", tab_job.Rows[j]["TsRemark"].ToString());
                    item.SetAttribute("TsAgtRate", tab_job.Rows[j]["TsAgtRate"].ToString());
                    item.SetAttribute("TsTotAgtRate", tab_job.Rows[j]["TsTotAgtRate"].ToString());
                    item.SetAttribute("TsImpRate", tab_job.Rows[j]["TsImpRate"].ToString());
                    item.SetAttribute("TsTotImpRate", tab_job.Rows[j]["TsTotImpRate"].ToString());
                    item.SetAttribute("PermitRmk", tab_job.Rows[j]["PermitRmk"].ToString());
                    item.SetAttribute("Shipper", tab_job.Rows[j]["SShipperRemark"].ToString());
                    item.SetAttribute("Consignee", tab_job.Rows[j]["SConsigneeRemark"].ToString());
                    item.SetAttribute("SNotifyPartyRemark", tab_job.Rows[j]["SNotifyPartyRemark"].ToString());
                    item.SetAttribute("SAgentRemark", tab_job.Rows[j]["SAgentRemark"].ToString());

                    item.SetAttribute("PODBy", tab_job.Rows[j]["PODBy"].ToString());
                    item.SetAttribute("PODTime", tab_job.Rows[j]["PODTime"].ToString());
                    item.SetAttribute("PODRemark", tab_job.Rows[j]["PODRemark"].ToString());

                    item.SetAttribute("HaulierName", tab_job.Rows[j]["HaulierName"].ToString());
                    item.SetAttribute("HaulierCrNo", tab_job.Rows[j]["HaulierCrNo"].ToString());
                    item.SetAttribute("HaulierFax", tab_job.Rows[j]["HaulierFax"].ToString());
                    item.SetAttribute("HaulierAttention", tab_job.Rows[j]["HaulierAttention"].ToString());
                    item.SetAttribute("DriverName", tab_job.Rows[j]["DriverName"].ToString());
                    item.SetAttribute("DriverMobile", tab_job.Rows[j]["DriverMobile"].ToString());
                    item.SetAttribute("DriverLicense", tab_job.Rows[j]["DriverLicense"].ToString());
                    item.SetAttribute("DriverRemark", tab_job.Rows[j]["DriverRemark"].ToString());
                    item.SetAttribute("VehicleNo", tab_job.Rows[j]["VehicleNo"].ToString());
                    item.SetAttribute("VehicleType", tab_job.Rows[j]["VehicleType"].ToString());
                    item.SetAttribute("HaulierCollect", tab_job.Rows[j]["HaulierCollect"].ToString());
                    item.SetAttribute("HaulierTruck", tab_job.Rows[j]["HaulierTruck"].ToString());
                    item.SetAttribute("HaulierCollectDate", SafeValue.SafeDate(tab_job.Rows[j]["HaulierCollectDate"], DateTime.Today).ToString("yyyy-MM-dd"));
                    item.SetAttribute("HaulierCollectTime", tab_job.Rows[j]["HaulierCollectTime"].ToString());
                    item.SetAttribute("HaulierDeliveryDate", SafeValue.SafeDate(tab_job.Rows[j]["HaulierDeliveryDate"], DateTime.Today).ToString("yyyy-MM-dd"));
                    item.SetAttribute("HaulierDeliveryTime", tab_job.Rows[j]["HaulierDeliveryTime"].ToString());
                    item.SetAttribute("HaulierRemark", tab_job.Rows[j]["HaulierRemark"].ToString());
                    item.SetAttribute("HaulierSendTo", tab_job.Rows[j]["HaulierSendTo"].ToString());
                    item.SetAttribute("HaulierStuffBy", tab_job.Rows[j]["HaulierStuffBy"].ToString());
                    item.SetAttribute("HaulierCoload", tab_job.Rows[j]["HaulierCoload"].ToString());
                    item.SetAttribute("HaulierPerson", tab_job.Rows[j]["HaulierPerson"].ToString());
                    item.SetAttribute("HaulierPersonTel", tab_job.Rows[j]["HaulierPersonTel"].ToString());
                    item.SetAttribute("Department", tab_job.Rows[j]["Dept"].ToString());

                    item.SetAttribute("DgImdgClass", tab_job.Rows[j]["DgImdgClass"].ToString());
                    item.SetAttribute("DgUnNo", tab_job.Rows[j]["DgUnNo"].ToString());
                    item.SetAttribute("DgShippingName", tab_job.Rows[j]["DgShippingName"].ToString());
                    item.SetAttribute("DgTecnicalName", tab_job.Rows[j]["DgTecnicalName"].ToString());
                    item.SetAttribute("DgMfagNo1", tab_job.Rows[j]["DgMfagNo1"].ToString());
                    item.SetAttribute("DgMfagNo2", tab_job.Rows[j]["DgMfagNo2"].ToString());
                    item.SetAttribute("DgEmsFire", tab_job.Rows[j]["DgEmsFire"].ToString());
                    item.SetAttribute("DgEmsSpill", tab_job.Rows[j]["DgEmsSpill"].ToString());
                    item.SetAttribute("DgNetWeight", tab_job.Rows[j]["DgNetWeight"].ToString());
                    item.SetAttribute("DgFlashPoint", tab_job.Rows[j]["DgFlashPoint"].ToString());
                    item.SetAttribute("DgRadio", tab_job.Rows[j]["DgRadio"].ToString());
                    item.SetAttribute("DgPageNo", tab_job.Rows[j]["DgPageNo"].ToString());
                    item.SetAttribute("DgPackingGroup", tab_job.Rows[j]["DgPackingGroup"].ToString());
                    item.SetAttribute("DgPackingTypeCode", tab_job.Rows[j]["DgPackingTypeCode"].ToString());
                    item.SetAttribute("DgTransportCode", tab_job.Rows[j]["DgTransportCode"].ToString());
                    item.SetAttribute("DgCategory", tab_job.Rows[j]["DgCategory"].ToString());
                    item.SetAttribute("DgLimitedQtyInd", tab_job.Rows[j]["DgLimitedQtyInd"].ToString());
                    item.SetAttribute("DgExemptedQtyInd", tab_job.Rows[j]["DgExemptedQtyInd"].ToString());

                    #region SeaImportMkg
                    string sql_mark = string.Format(@"select * from SeaImportMkg where JobNo='{0}'", tab_job.Rows[j]["JobNo"].ToString());
                    DataTable tab_mark = ConnectSql.GetDataSet(sql_mark).Tables[0];

                    for (int k = 0; k < tab_mark.Rows.Count; k++)
                    {
                        XmlElement cont = xmlDoc.CreateElement("Booking");
                        cont.SetAttribute("Weight", tab_mark.Rows[k]["Weight"].ToString());
                        cont.SetAttribute("Volume", tab_mark.Rows[k]["Volume"].ToString());
                        cont.SetAttribute("Qty", tab_mark.Rows[k]["Qty"].ToString());
                        cont.SetAttribute("PackageType", tab_mark.Rows[k]["PackageType"].ToString());
                        cont.SetAttribute("Marking", tab_mark.Rows[k]["Marking"].ToString());
                        cont.SetAttribute("Description", tab_mark.Rows[k]["Description"].ToString());
                        cont.SetAttribute("StatusCode", tab_mark.Rows[k]["StatusCode"].ToString());
                        string mkgType = tab_mark.Rows[k]["MkgType"].ToString();
                        cont.SetAttribute("MkgType", mkgType);
                        string cNo = tab_mark.Rows[k]["ContainerNo"].ToString();
                        string sNo = tab_mark.Rows[k]["SealNo"].ToString();
                        string cType = tab_mark.Rows[k]["ContainerType"].ToString();
                        if (mkgType == "Cont" || mkgType == "BL")
                        {
                            cont.SetAttribute("ContainerNo", tab_mark.Rows[k]["ContainerNo"].ToString());
                            cont.SetAttribute("SealNo", tab_mark.Rows[k]["SealNo"].ToString());
                            cont.SetAttribute("ContainerType", tab_mark.Rows[k]["ContainerType"].ToString());
                        }
                        item.AppendChild(cont);
                    }
                    #endregion
                }
                else
                {
                    item.SetAttribute("BkgRefNo", tab_job.Rows[j]["BkgRefNo"].ToString());

                    item.SetAttribute("ShipperId", tab_job.Rows[j]["ShipperId"].ToString());
                    item.SetAttribute("ShipperName", tab_job.Rows[j]["ShipperName"].ToString());
                    item.SetAttribute("ShipperContact", tab_job.Rows[j]["ShipperContact"].ToString());
                    item.SetAttribute("ShipperTel", tab_job.Rows[j]["ShipperTel"].ToString());
                    item.SetAttribute("ShipperFax", tab_job.Rows[j]["ShipperFax"].ToString());
                    item.SetAttribute("ShipperEmail", tab_job.Rows[j]["ShipperEmail"].ToString());
                    item.SetAttribute("AsAgent", tab_job.Rows[j]["AsAgent"].ToString());
                    item.SetAttribute("QuoteNo", tab_job.Rows[j]["QuoteNo"].ToString());
                    item.SetAttribute("PoNo", tab_job.Rows[j]["PoNo"].ToString());
                    item.SetAttribute("CollectFrom", tab_job.Rows[j]["CollectFrom"].ToString());
                    item.SetAttribute("FinDest", tab_job.Rows[j]["FinDest"].ToString());
                    item.SetAttribute("Marking", SafeValue.SafeString(tab_job.Rows[j]["Marking"]));
                    item.SetAttribute("Pol", tab_job.Rows[j]["Pol"].ToString());
                    item.SetAttribute("Pod", tab_job.Rows[j]["Pod"].ToString());
                    item.SetAttribute("PlaceLoadingName", tab_job.Rows[j]["PlaceLoadingName"].ToString());
                    item.SetAttribute("PlaceDischargeName", tab_job.Rows[j]["PlaceDischargeName"].ToString());
                    item.SetAttribute("PreCarriage", tab_job.Rows[j]["PreCarriage"].ToString());
                    item.SetAttribute("ShipOnBoardInd", tab_job.Rows[j]["ShipOnBoardInd"].ToString());
                    item.SetAttribute("ShipOnBoardDate", SafeValue.SafeDate(tab_job.Rows[j]["ShipOnBoardDate"], DateTime.Now).ToString());
                    item.SetAttribute("FrtTerm", tab_job.Rows[j]["FrtTerm"].ToString());
                    item.SetAttribute("PlaceDeliveryId", tab_job.Rows[j]["PlaceDeliveryId"].ToString());
                    item.SetAttribute("PlaceDeliveryName", tab_job.Rows[j]["PlaceDeliveryname"].ToString());
                    item.SetAttribute("PlaceReceiveId", tab_job.Rows[j]["PlaceReceiveId"].ToString());
                    item.SetAttribute("PlaceReceiveName", tab_job.Rows[j]["PlaceReceiveName"].ToString());
                    item.SetAttribute("PlaceDeliveryTerm", tab_job.Rows[j]["PlaceDeliveryTerm"].ToString());
                    item.SetAttribute("PlaceReceiveTerm", tab_job.Rows[j]["PlaceReceiveTerm"].ToString());
                    item.SetAttribute("ValueCurrency", tab_job.Rows[j]["ValueCurrency"].ToString());
                    item.SetAttribute("ValueExRate", tab_job.Rows[j]["ValueExRate"].ToString());
                    item.SetAttribute("ValueAmt", tab_job.Rows[j]["ValueAmt"].ToString());
                    item.SetAttribute("ExpressBl", tab_job.Rows[j]["ExpressBl"].ToString());
                    item.SetAttribute("Weight", tab_job.Rows[j]["Weight"].ToString());
                    item.SetAttribute("ExRate", tab_job.Rows[j]["Qty"].ToString());
                    item.SetAttribute("Volume", tab_job.Rows[j]["Volume"].ToString());
                    item.SetAttribute("Qty", tab_job.Rows[j]["Qty"].ToString());
                    item.SetAttribute("PackageType", tab_job.Rows[j]["PackageType"].ToString());
                    item.SetAttribute("PODBy", tab_job.Rows[j]["PODBy"].ToString());
                    item.SetAttribute("PODTime", tab_job.Rows[j]["PODTime"].ToString());
                    item.SetAttribute("PODRemark", tab_job.Rows[j]["PODRemark"].ToString());
                    item.SetAttribute("SShipperRemark", tab_job.Rows[j]["SShipperRemark"].ToString());
                    item.SetAttribute("SNotifyPartyRemark", tab_job.Rows[j]["SNotifyPartyRemark"].ToString());
                    item.SetAttribute("SAgentRemark", tab_job.Rows[j]["SAgentRemark"].ToString());
                    item.SetAttribute("SConsigneeRemark", tab_job.Rows[j]["SConsigneeRemark"].ToString());
                    item.SetAttribute("PermitRmk", tab_job.Rows[j]["PermitRmk"].ToString());
                    item.SetAttribute("HaulierName", tab_job.Rows[j]["HaulierName"].ToString());
                    item.SetAttribute("HaulierCrNo", tab_job.Rows[j]["HaulierCrNo"].ToString());
                    item.SetAttribute("HaulierFax", tab_job.Rows[j]["HaulierFax"].ToString());
                    item.SetAttribute("HaulierAttention", tab_job.Rows[j]["HaulierAttention"].ToString());
                    item.SetAttribute("DriverName", tab_job.Rows[j]["DriverName"].ToString());
                    item.SetAttribute("DriverMobile", tab_job.Rows[j]["DriverMobile"].ToString());
                    item.SetAttribute("DriverLicense", tab_job.Rows[j]["DriverLicense"].ToString());
                    item.SetAttribute("DriverRemark", tab_job.Rows[j]["DriverRemark"].ToString());
                    item.SetAttribute("VehicleNo", tab_job.Rows[j]["VehicleNo"].ToString());
                    item.SetAttribute("VehicleType", tab_job.Rows[j]["VehicleType"].ToString());
                    item.SetAttribute("HaulierCollect", tab_job.Rows[j]["HaulierCollect"].ToString());
                    item.SetAttribute("HaulierTruck", tab_job.Rows[j]["HaulierTruck"].ToString());
                    item.SetAttribute("HaulierCollectDate", SafeValue.SafeDate(tab_job.Rows[j]["HaulierCollectDate"], DateTime.Today).ToString("yyyy-MM-dd"));
                    item.SetAttribute("HaulierCollectTime", tab_job.Rows[j]["HaulierCollectTime"].ToString());
                    item.SetAttribute("HaulierDeliveryDate", SafeValue.SafeDate(tab_job.Rows[j]["HaulierDeliveryDate"], DateTime.Today).ToString("yyyy-MM-dd"));
                    item.SetAttribute("HaulierDeliveryTime", tab_job.Rows[j]["HaulierDeliveryTime"].ToString());
                    item.SetAttribute("HaulierRemark", tab_job.Rows[j]["HaulierRemark"].ToString());
                    item.SetAttribute("HaulierSendTo", tab_job.Rows[j]["HaulierSendTo"].ToString());
                    item.SetAttribute("HaulierStuffBy", tab_job.Rows[j]["HaulierStuffBy"].ToString());
                    item.SetAttribute("HaulierCoload", tab_job.Rows[j]["HaulierCoload"].ToString());
                    item.SetAttribute("HaulierPerson", tab_job.Rows[j]["HaulierPerson"].ToString());
                    item.SetAttribute("HaulierPersonTel", tab_job.Rows[j]["HaulierPersonTel"].ToString());
                    item.SetAttribute("Department", tab_job.Rows[j]["Dept"].ToString());
                    item.SetAttribute("StatusCode", tab_job.Rows[j]["StatusCode"].ToString());
                    item.SetAttribute("SurrenderBl", tab_job.Rows[i]["SurrenderBl"].ToString());

                    item.SetAttribute("DgImdgClass", tab_job.Rows[j]["DgImdgClass"].ToString());
                    item.SetAttribute("DgUnNo", tab_job.Rows[j]["DgUnNo"].ToString());
                    item.SetAttribute("DgShippingName", tab_job.Rows[j]["DgShippingName"].ToString());
                    item.SetAttribute("DgTecnicalName", tab_job.Rows[j]["DgTecnicalName"].ToString());
                    item.SetAttribute("DgMfagNo1", tab_job.Rows[j]["DgMfagNo1"].ToString());
                    item.SetAttribute("DgMfagNo2", tab_job.Rows[j]["DgMfagNo2"].ToString());
                    item.SetAttribute("DgEmsFire", tab_job.Rows[j]["DgEmsFire"].ToString());
                    item.SetAttribute("DgEmsSpill", tab_job.Rows[j]["DgEmsSpill"].ToString());
                    item.SetAttribute("DgNetWeight", tab_job.Rows[j]["DgNetWeight"].ToString());
                    item.SetAttribute("DgFlashPoint", tab_job.Rows[j]["DgFlashPoint"].ToString());
                    item.SetAttribute("DgRadio", tab_job.Rows[j]["DgRadio"].ToString());
                    item.SetAttribute("DgPageNo", tab_job.Rows[j]["DgPageNo"].ToString());
                    item.SetAttribute("DgPackingGroup", tab_job.Rows[j]["DgPackingGroup"].ToString());
                    item.SetAttribute("DgPackingTypeCode", tab_job.Rows[j]["DgPackingTypeCode"].ToString());
                    item.SetAttribute("DgTransportCode", tab_job.Rows[j]["DgTransportCode"].ToString());
                    item.SetAttribute("DgCategory", tab_job.Rows[j]["DgCategory"].ToString());
                    item.SetAttribute("DgLimitedQtyInd", tab_job.Rows[j]["DgLimitedQtyInd"].ToString());
                    item.SetAttribute("DgExemptedQtyInd", tab_job.Rows[j]["DgExemptedQtyInd"].ToString());


                    #region SeaExportMkg
                    string sql_mark = string.Format(@"select * from SeaExportMkg where JobNo='{0}'", tab_job.Rows[j]["JobNo"].ToString());
                    DataTable tab_mark = ConnectSql.GetDataSet(sql_mark).Tables[0];

                    for (int k = 0; k < tab_mark.Rows.Count; k++)
                    {
                        XmlElement cont = xmlDoc.CreateElement("Booking");
                        cont.SetAttribute("Weight", tab_mark.Rows[k]["Weight"].ToString());
                        cont.SetAttribute("Volume", tab_mark.Rows[k]["Volume"].ToString());
                        cont.SetAttribute("Qty", tab_mark.Rows[k]["Qty"].ToString());
                        cont.SetAttribute("PackageType", tab_mark.Rows[k]["PackageType"].ToString());
                        string mkgType = tab_mark.Rows[k]["MkgType"].ToString();
                        cont.SetAttribute("MkgType", mkgType);
                        cont.SetAttribute("GrossWt", tab_mark.Rows[k]["GrossWt"].ToString());
                        cont.SetAttribute("NetWt", tab_mark.Rows[k]["NetWt"].ToString());
                        string cNo = tab_mark.Rows[k]["ContainerNo"].ToString();
                        string sNo = tab_mark.Rows[k]["SealNo"].ToString();
                        string cType = tab_mark.Rows[k]["ContainerType"].ToString();
                        cont.SetAttribute("ContainerNo", tab_mark.Rows[k]["ContainerNo"].ToString());
                        cont.SetAttribute("SealNo", tab_mark.Rows[k]["SealNo"].ToString());
                        cont.SetAttribute("ContainerType", tab_mark.Rows[k]["ContainerType"].ToString());
                        cont.SetAttribute("Marking", tab_mark.Rows[k]["Marking"].ToString());
                        cont.SetAttribute("Description", tab_mark.Rows[k]["Description"].ToString());
                        cont.SetAttribute("StatusCode", tab_mark.Rows[k]["StatusCode"].ToString());
                        item.AppendChild(cont);
                    }
                    #endregion
                }
                bill.AppendChild(item);
            }
            #endregion
            xmlNode.AppendChild(bill);
        }

        return xmlDoc;
    }

    public static string Import_SeaImportFromImport(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string refN = "";
        string jobType = "";
        if (filePath.Length > 0)
        {

            xmlDoc.Load(filePath);
            if (xmlDoc.SelectSingleNode("SeaImportRef") != null)
            {
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("SeaImportRef").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement elem = (XmlElement)xn;
                    SeaImportRef impRef = null;
                    string refType = "";
                    string runType = "ImportRef";//SIF/SIL/SIC

                    if (impRef == null)
                    {
                        #region ref
                        impRef = new SeaImportRef();
                        refType = elem.GetAttribute("RefType");
                        refN = C2Setup.GetNextNo(refType, runType, DateTime.Now);
                        impRef.RefNo = refN;
                        impRef.JobType = elem.GetAttribute("JobType");
                        impRef.RefType = refType;
                        jobType = impRef.JobType;
                        impRef.RefDate = SafeValue.SafeDate(elem.GetAttribute("RefDate"), DateTime.Now);
                        impRef.Pol = EzshipHelper.GetPortCode(elem.GetAttribute("Pol"));
                        impRef.Pod = EzshipHelper.GetPortCode(elem.GetAttribute("Pod"));
                        impRef.AgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Agent"));
                        string vesVoy = elem.GetAttribute("VesVoy");
                        string[] arr = vesVoy.Split('/');
                        impRef.Vessel = arr[0];
                        impRef.Voyage = arr[1];
                        impRef.Eta = SafeValue.SafeDate(elem.GetAttribute("Eta"), DateTime.Now);
                        impRef.Etd = SafeValue.SafeDate(elem.GetAttribute("Etd"), DateTime.Now);
                        impRef.OblNo = elem.GetAttribute("OblNo");
                        impRef.CrBkgNo = elem.GetAttribute("CrBkgNo");
                        impRef.PermitRemark = elem.GetAttribute("Permit");
                        impRef.CrAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("CrAgent"));
                        impRef.NvoccAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("NvoccAgent"));
                        impRef.NvoccBl = elem.GetAttribute("NvoccBl");

                        impRef.WarehouseId = EzshipHelper.GetPartyId(elem.GetAttribute("Warehouse"));
                        impRef.ForwardAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("ForwardAgent"));
                        impRef.TransportId = EzshipHelper.GetPartyId(elem.GetAttribute("Transport"));
                        impRef.LocalCust = EzshipHelper.GetPartyId(elem.GetAttribute("LocalCustName"));
                        impRef.Weight = SafeValue.SafeDecimal(elem.GetAttribute("Wt"));
                        impRef.Volume = SafeValue.SafeDecimal(elem.GetAttribute("M3"));
                        impRef.Qty = SafeValue.SafeInt(elem.GetAttribute("Qty"), 0);
                        impRef.PackageType = elem.GetAttribute("PackageType");
                        impRef.CurrencyId = elem.GetAttribute("RefCurrency");
                        impRef.SShipperRemark = elem.GetAttribute("SShipperRemark");
                        impRef.SAgentRemark = elem.GetAttribute("SAgentRemark");
                        impRef.SConsigneeRemark = elem.GetAttribute("SConsigneeRemark");
                        impRef.SNotifyPartyRemark = elem.GetAttribute("SNotifyPartyRemark");
                        impRef.HaulierName = elem.GetAttribute("HaulierName");
                        impRef.HaulierCrNo = elem.GetAttribute("HaulierCrNo");
                        impRef.HaulierFax = elem.GetAttribute("HaulierFax");
                        impRef.HaulierAttention = elem.GetAttribute("HaulierAttention");
                        impRef.HaulierCollect = elem.GetAttribute("HaulierCollect");
                        impRef.HaulierCollectDate = SafeValue.SafeDate(elem.GetAttribute("HaulierCollectDate"), DateTime.Now);
                        impRef.HaulierCollectTime = elem.GetAttribute("HaulierCollectTime");
                        impRef.HaulierDeliveryDate = SafeValue.SafeDate(elem.GetAttribute("HaulierDeliveryDate"), DateTime.Now);
                        impRef.HaulierDeliveryTime = elem.GetAttribute("HaulierDeliveryTime");
                        impRef.HaulierRemark = elem.GetAttribute("HaulierRemark");
                        impRef.HaulierTruck = elem.GetAttribute("HaulierTruck");
                        impRef.HaulierFax = elem.GetAttribute("HaulierFax");
                        impRef.DriverName = elem.GetAttribute("DriverName");
                        impRef.DriverMobile = elem.GetAttribute("DriverMobile");
                        impRef.DriverLicense = elem.GetAttribute("DriverLicense");
                        impRef.VehicleNo = elem.GetAttribute("VehicleNo");
                        impRef.VehicleType = elem.GetAttribute("VehicleType");
                        impRef.DriverRemark = elem.GetAttribute("DriverRemark");
                        impRef.HaulierSendTo = elem.GetAttribute("HaulierSendTo");
                        impRef.HaulierStuffBy = elem.GetAttribute("HaulierStuffBy");
                        impRef.HaulierPerson = elem.GetAttribute("HaulierPerson");
                        impRef.HaulierPersonTel = elem.GetAttribute("HaulierPersonTel");
                        impRef.HaulierCoload = elem.GetAttribute("HaulierCoload");
                        impRef.Remark = elem.GetAttribute("Remark");
                        impRef.StatusCode = elem.GetAttribute("StatusCode");
                        impRef.CreateBy = EzshipHelper.GetUserName();
                        impRef.CreateDateTime = DateTime.Now;
                        impRef.CreateBy = EzshipHelper.GetUserName();
                        impRef.UpdateDateTime = DateTime.Now;
                        C2Setup.SetNextNo(refType, "ImportRef", refN, impRef.RefDate);
                        Manager.ORManager.StartTracking(impRef, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(impRef);

                        #endregion

                        XmlNodeList nodeListCont = xn.SelectNodes("Container");
                        #region Container
                        foreach (XmlNode xnMkg in nodeListCont)
                        {
                            XmlElement elemMark = (XmlElement)xnMkg;
                            if (elemMark.LocalName == "Container")
                            {
                                if (impRef.RefType == "SIL")
                                {
                                    C2.SeaImportMkg mkg = new SeaImportMkg();
                                    mkg.JobNo = "";
                                    mkg.RefNo = refN;
                                    string mkgType = elemMark.GetAttribute("MkgType");
                                    mkg.MkgType = mkgType;
                                    mkg.ContainerNo = elemMark.GetAttribute("ContainerNo");
                                    mkg.SealNo = elemMark.GetAttribute("SealNo");
                                    mkg.ContainerType = elemMark.GetAttribute("ContainerType");
                                    Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                                    Manager.ORManager.PersistChanges(mkg);
                                }
                            }
                        }
                        #endregion

                        #region job
                        XmlNode nodeListHouse = xmlDoc.SelectSingleNode("SeaImportRef").LastChild;
                        foreach (XmlNode xnCont in nodeListHouse)
                        {
                            C2.SeaImport imp = new SeaImport();
                            XmlElement elemCont = (XmlElement)xnCont;
                            #region House
                            if (elemCont.LocalName == "House")
                            {

                                string impN = C2Setup.GetSubNo(refN, "SI");
                                imp.JobNo = impN;
                                imp.RefNo = refN;
                                imp.HblNo = elemCont.GetAttribute("HblNo");
                                imp.CustomerId = EzshipHelper.GetPartyId(elemCont.GetAttribute("CustomerName"));
                                imp.CustomerName = elemCont.GetAttribute("CustomerName");
                                imp.Weight = SafeValue.SafeDecimal(elemCont.GetAttribute("Weight"));
                                imp.Volume = SafeValue.SafeDecimal(elemCont.GetAttribute("Volume"));
                                imp.Qty = SafeValue.SafeInt(elemCont.GetAttribute("Qty"), 0);
                                imp.PackageType = elemCont.GetAttribute("PackageType");
                                imp.ExpressBl = elemCont.GetAttribute("ExpressBl");
                                imp.ForwardingId = EzshipHelper.GetPartyId(elemCont.GetAttribute("Forwarding"));
                                imp.CltFrmId = EzshipHelper.GetPartyId(elemCont.GetAttribute("CltFrmFrom"));
                                imp.DeliveryToId = EzshipHelper.GetPartyId(elemCont.GetAttribute("DeliveryTo"));
                                imp.DoRealeaseTo = elemCont.GetAttribute("DoRealeaseTo");
                                imp.DeliveryDate = SafeValue.SafeDate(elemCont.GetAttribute("DeliveryDate"), DateTime.Now);
                                imp.DoReadyInd = elemCont.GetAttribute("DoReadyInd");
                                imp.TruckingInd = elemCont.GetAttribute("TruckingInd");
                                imp.FrCollectInd = elemCont.GetAttribute("FrCollectInd");
                                imp.CollectExRate = SafeValue.SafeDecimal(elemCont.GetAttribute("CollectExRate"));
                                imp.CollectCurrency = elemCont.GetAttribute("CollectCurrency");
                                imp.CollectAmount = SafeValue.SafeDecimal(elemCont.GetAttribute("CollectAmount"));
                                imp.ValueCurrency = elemCont.GetAttribute("ValueCurrency");
                                imp.ValueExRate = SafeValue.SafeDecimal(elemCont.GetAttribute("ValueExRate"));
                                imp.ValueAmt = SafeValue.SafeDecimal(elemCont.GetAttribute("ValueAmt"));
                                imp.Remark = elemCont.GetAttribute("Remark");
                                imp.RateForklift = SafeValue.SafeDecimal(elemCont.GetAttribute("RateForklift"));
                                imp.RateProcess = SafeValue.SafeDecimal(elemCont.GetAttribute("RateProcess"));
                                imp.RateTracing = SafeValue.SafeDecimal(elemCont.GetAttribute("RateTracing"));
                                imp.RateWarehouse = SafeValue.SafeDecimal(elemCont.GetAttribute("RateWarehouse"));
                                imp.RateAdmin = SafeValue.SafeDecimal(elemCont.GetAttribute("RateAdmin"));
                                imp.FlagNomination = elemCont.GetAttribute("FlagNomination");
                                imp.TsInd = elemCont.GetAttribute("TsInd");
                                imp.TsPod = elemCont.GetAttribute("TsPod"); ;
                                imp.TsPortFinName = elemCont.GetAttribute("TsPortFinName");
                                imp.TsAgentId = EzshipHelper.GetPartyId(elemCont.GetAttribute("TsAgent"));
                                imp.TsBkgNo = elemCont.GetAttribute("BkgNo");
                                imp.TsVessel = elemCont.GetAttribute("TsVessel");
                                imp.TsVoyage = elemCont.GetAttribute("TsVoyage");
                                imp.TsEtd = SafeValue.SafeDate(elemCont.GetAttribute("TsEtd"), DateTime.Now);
                                imp.TsEta = SafeValue.SafeDate(elemCont.GetAttribute("TsEta"), DateTime.Now);
                                imp.TsColoader = elemCont.GetAttribute("TsColoader");
                                imp.TsRemark = elemCont.GetAttribute("TsRemark");
                                imp.TsAgtRate = SafeValue.SafeDecimal(elemCont.GetAttribute("TsAgtRate"));
                                imp.TsImpRate = SafeValue.SafeDecimal(elemCont.GetAttribute("TsImpRate"));
                                imp.PermitRmk = elemCont.GetAttribute("PermitRmk");
                                imp.SShipperRemark = elemCont.GetAttribute("Shipper");
                                imp.SConsigneeRemark = elemCont.GetAttribute("Consignee");
                                imp.SNotifyPartyRemark = elemCont.GetAttribute("SNotifyPartyRemark");
                                imp.SAgentRemark = elemCont.GetAttribute("SAgentRemark");
                                imp.PODBy = elemCont.GetAttribute("PODBy");
                                imp.PODTime = SafeValue.SafeDate(elemCont.GetAttribute("PODTime"), DateTime.Now);
                                imp.PODRemark = elemCont.GetAttribute("PODRemark");
                                imp.PODUpdateUser = EzshipHelper.GetUserName();
                                imp.PODUpdateTime = DateTime.Now;

                                imp.DgImdgClass = elemCont.GetAttribute("DgImdgClass");
                                imp.DgUnNo = elemCont.GetAttribute("DgUnNo");
                                imp.DgShippingName = elemCont.GetAttribute("DgShippingName");
                                imp.DgTecnicalName = elemCont.GetAttribute("DgTecnicalName");
                                imp.DgMfagNo1 = elemCont.GetAttribute("DgMfagNo1");
                                imp.DgMfagNo2 = elemCont.GetAttribute("DgMfagNo2");
                                imp.DgEmsFire = elemCont.GetAttribute("DgEmsFire");
                                imp.DgEmsSpill = elemCont.GetAttribute("DgEmsSpill");
                                imp.DgNetWeight = elemCont.GetAttribute("DgNetWeight");
                                imp.DgFlashPoint = elemCont.GetAttribute("DgFlashPoint");
                                imp.DgRadio = elemCont.GetAttribute("DgRadio");
                                imp.DgPageNo = elemCont.GetAttribute("DgPageNo");
                                imp.DgPackingGroup = elemCont.GetAttribute("DgPackingGroup");
                                imp.DgTransportCode = elemCont.GetAttribute("DgTransportCode");
                                imp.DgPackingTypeCode = elemCont.GetAttribute("DgPackingTypeCode");
                                imp.DgCategory = elemCont.GetAttribute("DgCategory");
                                imp.DgLimitedQtyInd = elemCont.GetAttribute("DgLimitedQtyInd");
                                imp.DgExemptedQtyInd = elemCont.GetAttribute("DgExemptedQtyInd");


                                imp.HaulierName = elemCont.GetAttribute("HaulierName");
                                imp.HaulierCrNo = elemCont.GetAttribute("HaulierCrNo");
                                imp.HaulierFax = elemCont.GetAttribute("HaulierFax");
                                imp.HaulierAttention = elemCont.GetAttribute("HaulierAttention");
                                imp.DriverName = elemCont.GetAttribute("DriverName");
                                imp.DriverMobile = elemCont.GetAttribute("DriverMobile");
                                imp.DriverLicense = elemCont.GetAttribute("DriverLicense");
                                imp.DriverRemark = elemCont.GetAttribute("DriverRemark");
                                imp.VehicleNo = elemCont.GetAttribute("VehicleNo");
                                imp.VehicleType = elemCont.GetAttribute("VehicleType");
                                imp.HaulierCollect = elemCont.GetAttribute("HaulierCollect");
                                imp.HaulierTruck = elemCont.GetAttribute("HaulierTruck");
                                imp.HaulierRemark = elemCont.GetAttribute("HaulierRemark");
                                imp.HaulierCollectDate = SafeValue.SafeDate(elemCont.GetAttribute("HaulierCollectDate"), DateTime.Now);
                                imp.HaulierDeliveryDate = SafeValue.SafeDate(elemCont.GetAttribute("HaulierDeliveryDate"), DateTime.Now);
                                imp.HaulierCollectTime = elemCont.GetAttribute("HaulierCollectTime");
                                imp.HaulierDeliveryTime = elemCont.GetAttribute("HaulierDeliveryTime");
                                imp.HaulierSendTo = elemCont.GetAttribute("HaulierSendTo");
                                imp.HaulierStuffBy = elemCont.GetAttribute("HaulierStuffBy");
                                imp.HaulierCoload = elemCont.GetAttribute("HaulierCoload");
                                imp.HaulierPerson = elemCont.GetAttribute("HaulierPerson");
                                imp.HaulierPersonTel = elemCont.GetAttribute("HaulierPersonTel");
                                imp.Dept = elemCont.GetAttribute("Department");
                                imp.HaulierCoload = elemCont.GetAttribute("HaulierCoload");



                                imp.CreateBy = EzshipHelper.GetUserName();
                                imp.CreateDateTime = DateTime.Now;
                                imp.StatusCode = elemCont.GetAttribute("StatusCode");
                                Manager.ORManager.StartTracking(imp, Wilson.ORMapper.InitialState.Inserted);
                                Manager.ORManager.PersistChanges(imp);

                                XmlNodeList nodeListMkg = xnCont.SelectNodes("Booking");
                                #region Marking
                                foreach (XmlNode xnMkg in nodeListMkg)
                                {
                                    XmlElement elemMark = (XmlElement)xnMkg;
                                    if (elemMark.LocalName == "Booking")
                                    {
                                        C2.SeaImportMkg mkg = new SeaImportMkg();
                                        mkg.JobNo = imp.JobNo;
                                        mkg.RefNo = refN;
                                        mkg.Weight = SafeValue.SafeDecimal(elemMark.GetAttribute("Weight"));
                                        mkg.Volume = SafeValue.SafeDecimal(elemMark.GetAttribute("Volume"));
                                        mkg.Qty = SafeValue.SafeInt(elemMark.GetAttribute("Qty"), 0);
                                        mkg.PackageType = elemMark.GetAttribute("PackageType");
                                        string mkgType = elemMark.GetAttribute("MkgType");
                                        mkg.MkgType = mkgType;
                                        mkg.ContainerNo = elemMark.GetAttribute("ContainerNo");
                                        mkg.SealNo = elemMark.GetAttribute("SealNo");
                                        mkg.ContainerType = elemMark.GetAttribute("ContainerType");
                                        mkg.Marking = elemMark.GetAttribute("Marking");
                                        mkg.Description = elemMark.GetAttribute("Description");
                                        mkg.StatusCode = elemMark.GetAttribute("StatusCode");
                                        Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                                        Manager.ORManager.PersistChanges(mkg);
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }

                }

            }
        }
        return "JobType=" + jobType + " , " + "RefNo=" + refN;
    }

    public static string Import_SeaImportFromExport(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string refN = "";
        string jobType = "";
        string value = "Error";
        if (filePath.Length > 0)
        {
            xmlDoc.Load(filePath);
            if (xmlDoc.SelectSingleNode("SeaExportRef") != null)
            {
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("SeaExportRef").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement elem = (XmlElement)xn;
                    SeaImportRef impRef = null;
                    string refType = "";
                    string runType = "ImportRef";//SIF/SIL/SIC/SAI

                    if (impRef == null)
                    {
                        #region ref
                        impRef = new SeaImportRef();
                        refType = elem.GetAttribute("RefType");//SEF/SEL/SEC/
                        if (refType == "SEF")
                        {
                            refType = "SIF";
                        }
                        if (refType == "SEL")
                        {
                            refType = "SIL";
                        }
                        if (refType == "SEC")
                        {
                            refType = "SIC";
                        }
                        refN = C2Setup.GetNextNo(refType, runType, DateTime.Now);
                        impRef.RefNo = refN;
                        impRef.JobType = elem.GetAttribute("JobType");
                        impRef.RefType = refType;
                        jobType = impRef.JobType;
                        impRef.RefDate = SafeValue.SafeDate(elem.GetAttribute("RefDate"), DateTime.Now);
                        impRef.Pol = EzshipHelper.GetPortCode(elem.GetAttribute("Pol"));
                        impRef.Pod = EzshipHelper.GetPortCode(elem.GetAttribute("Pod"));
                        impRef.AgentId = "";
                        string vesVoy = elem.GetAttribute("VesVoy");
                        string[] arr = vesVoy.Split('/');
                        impRef.Vessel = arr[0];
                        impRef.Voyage = arr[1];
                        impRef.Eta = SafeValue.SafeDate(elem.GetAttribute("Eta"), DateTime.Now);
                        impRef.Etd = SafeValue.SafeDate(elem.GetAttribute("Etd"), DateTime.Now);
                        impRef.OblNo = elem.GetAttribute("OblNo");
                        impRef.CrBkgNo = elem.GetAttribute("CrBkgNo");
                        impRef.PermitRemark = elem.GetAttribute("Permit");
                        impRef.CrAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("CrAgent"));
                        impRef.NvoccAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("NvoccAgent"));
                        impRef.NvoccBl = elem.GetAttribute("NvoccBl");

                        impRef.WarehouseId = EzshipHelper.GetPartyId(elem.GetAttribute("Warehouse"));
                        impRef.ForwardAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("ForwardAgent"));
                        //impRef.TransportId = EzshipHelper.GetPartyId(elem.GetAttribute("Transport"));
                        impRef.LocalCust = EzshipHelper.GetPartyId(elem.GetAttribute("LocalCustName"));
                        impRef.Weight = SafeValue.SafeDecimal(elem.GetAttribute("Wt"));
                        impRef.Volume = SafeValue.SafeDecimal(elem.GetAttribute("M3"));
                        impRef.Qty = SafeValue.SafeInt(elem.GetAttribute("Qty"), 0);
                        impRef.PackageType = elem.GetAttribute("PackageType");
                        impRef.CurrencyId = elem.GetAttribute("RefCurrency");
                        impRef.ExRate = 0;
                        impRef.SShipperRemark = elem.GetAttribute("SShipperRemark");
                        impRef.SAgentRemark = elem.GetAttribute("SAgentRemark");
                        impRef.SConsigneeRemark = elem.GetAttribute("SConsigneeRemark");
                        impRef.SNotifyPartyRemark = elem.GetAttribute("SNotifyPartyRemark");
                        impRef.Remark = elem.GetAttribute("Remark");
                        impRef.PermitRemark = elem.GetAttribute("PermitRmk");
                        impRef.StatusCode = elem.GetAttribute("StatusCode");
                        impRef.CreateBy = EzshipHelper.GetUserName();
                        impRef.CreateDateTime = DateTime.Now;
                        C2Setup.SetNextNo(refType, "ImportRef", refN, impRef.RefDate);
                        Manager.ORManager.StartTracking(impRef, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(impRef);
                        #endregion

                        XmlNodeList nodeListCont = xn.SelectNodes("Container");
                        #region Container
                        foreach (XmlNode xnMkg in nodeListCont)
                        {
                            XmlElement elemMark = (XmlElement)xnMkg;
                            if (elemMark.LocalName == "Container")
                            {
                                if (impRef.RefType == "SIL")
                                {
                                    C2.SeaImportMkg mkg = new SeaImportMkg();
                                    mkg.JobNo = "";
                                    mkg.RefNo = refN;
                                    string mkgType = elemMark.GetAttribute("MkgType");
                                    mkg.MkgType = mkgType;
                                    mkg.ContainerNo = elemMark.GetAttribute("ContainerNo");
                                    mkg.SealNo = elemMark.GetAttribute("SealNo");
                                    mkg.ContainerType = elemMark.GetAttribute("ContainerType");
                                    mkg.StatusCode = elemMark.GetAttribute("StatusCode");
                                    Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                                    Manager.ORManager.PersistChanges(mkg);
                                }
                            }
                        }
                        #endregion

                        XmlNode nodeListHouse = xmlDoc.SelectSingleNode("SeaExportRef").LastChild;
                        foreach (XmlNode xnCont in nodeListHouse)
                        {
                            XmlElement elemCont = (XmlElement)xnCont;
                            C2.SeaImport imp = new SeaImport();
                            #region House
                            if (elemCont.LocalName == "House")
                            {
                                string impN = C2Setup.GetSubNo(refN, "SI");
                                imp.JobNo = impN;
                                imp.RefNo = refN;
                                imp.HblNo = elemCont.GetAttribute("HblNo");
                                imp.CustomerId = EzshipHelper.GetPartyId(elemCont.GetAttribute("CustomerName"));
                                imp.CustomerName = elemCont.GetAttribute("CustomerName");
                                imp.Weight = SafeValue.SafeDecimal(elemCont.GetAttribute("Weight"));
                                imp.Volume = SafeValue.SafeDecimal(elemCont.GetAttribute("Volume"));
                                imp.Qty = SafeValue.SafeInt(elemCont.GetAttribute("Qty"), 0);
                                imp.PackageType = elemCont.GetAttribute("PackageType");
                                imp.ExpressBl = elemCont.GetAttribute("ExpressBl");
                                imp.ForwardingId = EzshipHelper.GetPartyId(elemCont.GetAttribute("Forwarding"));
                                imp.CltFrmId = EzshipHelper.GetPartyId(elemCont.GetAttribute("CltFrmFrom"));
                                imp.DeliveryToId = EzshipHelper.GetPartyId(elemCont.GetAttribute("DeliveryTo"));
                                imp.DoRealeaseTo = elemCont.GetAttribute("DoRealeaseTo");
                                imp.DeliveryDate = SafeValue.SafeDate(elemCont.GetAttribute("DeliveryDate"), DateTime.Now);
                                imp.DoReadyInd = elemCont.GetAttribute("DoReadyInd");
                                imp.TruckingInd = elemCont.GetAttribute("TruckingInd");
                                imp.FrCollectInd = elemCont.GetAttribute("FrCollectInd");
                                imp.CollectExRate = SafeValue.SafeDecimal(elemCont.GetAttribute("CollectExRate"));
                                imp.CollectCurrency = elemCont.GetAttribute("CollectCurrency");
                                imp.CollectAmount = SafeValue.SafeDecimal(elemCont.GetAttribute("CollectAmount"));
                                imp.ValueCurrency = elemCont.GetAttribute("ValueCurrency");
                                imp.ValueExRate = SafeValue.SafeDecimal(elemCont.GetAttribute("ValueExRate"));
                                imp.ValueAmt = SafeValue.SafeDecimal(elemCont.GetAttribute("ValueAmt"));
                                imp.Remark = elemCont.GetAttribute("Remark");
                                imp.RateForklift = SafeValue.SafeDecimal(elemCont.GetAttribute("RateForklift"));
                                imp.RateProcess = SafeValue.SafeDecimal(elemCont.GetAttribute("RateProcess"));
                                imp.RateTracing = SafeValue.SafeDecimal(elemCont.GetAttribute("RateTracing"));
                                imp.RateWarehouse = SafeValue.SafeDecimal(elemCont.GetAttribute("RateWarehouse"));
                                imp.RateAdmin = SafeValue.SafeDecimal(elemCont.GetAttribute("RateAdmin"));
                                imp.FlagNomination = elemCont.GetAttribute("FlagNomination");
                                imp.TsInd = elemCont.GetAttribute("TsInd");
                                imp.TsPod = elemCont.GetAttribute("TsPod"); ;
                                imp.TsPortFinName = elemCont.GetAttribute("TsPortFinName");
                                imp.TsAgentId = EzshipHelper.GetPartyId(elemCont.GetAttribute("TsAgent"));
                                imp.TsBkgNo = elemCont.GetAttribute("BkgNo");
                                imp.TsVessel = elemCont.GetAttribute("TsVessel");
                                imp.TsVoyage = elemCont.GetAttribute("TsVoyage");
                                imp.TsEtd = SafeValue.SafeDate(elemCont.GetAttribute("TsEtd"), DateTime.Now);
                                imp.TsEta = SafeValue.SafeDate(elemCont.GetAttribute("TsEta"), DateTime.Now);
                                imp.TsColoader = elemCont.GetAttribute("TsColoader");
                                imp.TsRemark = elemCont.GetAttribute("TsRemark");
                                imp.TsAgtRate = SafeValue.SafeDecimal(elemCont.GetAttribute("TsAgtRate"));
                                imp.TsImpRate = SafeValue.SafeDecimal(elemCont.GetAttribute("TsImpRate"));
                                imp.PermitRmk = elemCont.GetAttribute("PermitRmk");
                                imp.SShipperRemark = elemCont.GetAttribute("SShipperRemark");
                                imp.SConsigneeRemark = elemCont.GetAttribute("SConsigneeRemark");
                                imp.SNotifyPartyRemark = elemCont.GetAttribute("SNotifyPartyRemark");
                                imp.SAgentRemark = elemCont.GetAttribute("SAgentRemark");
                                imp.Dept = elemCont.GetAttribute("Department");
                                imp.Remark = elemCont.GetAttribute("Remark");
                                imp.CreateBy = EzshipHelper.GetUserName();
                                imp.CreateDateTime = DateTime.Now;
                                imp.StatusCode = elemCont.GetAttribute("StatusCode");
                                Manager.ORManager.StartTracking(imp, Wilson.ORMapper.InitialState.Inserted);
                                Manager.ORManager.PersistChanges(imp);
                            }
                            #endregion

                            XmlNodeList nodeListMkg = xnCont.SelectNodes("Booking");

                            #region Marking
                            foreach (XmlNode xnMkg in nodeListMkg)
                            {
                                XmlElement elemMark = (XmlElement)xnMkg;
                                if (elemMark.LocalName == "Booking")
                                {
                                    C2.SeaImportMkg mkg = new SeaImportMkg();
                                    mkg.JobNo = imp.JobNo;
                                    mkg.RefNo = refN;
                                    mkg.Weight = SafeValue.SafeDecimal(elemMark.GetAttribute("Weight"));
                                    mkg.Volume = SafeValue.SafeDecimal(elemMark.GetAttribute("Volume"));
                                    mkg.Qty = SafeValue.SafeInt(elemMark.GetAttribute("Qty"), 0);
                                    mkg.PackageType = elemMark.GetAttribute("PackageType");
                                    string mkgType = elemMark.GetAttribute("MkgType");
                                    mkg.MkgType = mkgType;
                                    mkg.ContainerNo = elemMark.GetAttribute("ContainerNo");
                                    mkg.SealNo = elemMark.GetAttribute("SealNo");
                                    mkg.ContainerType = elemMark.GetAttribute("ContainerType");
                                    mkg.Marking = elemMark.GetAttribute("Marking");
                                    mkg.Description = elemMark.GetAttribute("Description");
                                    mkg.StatusCode = elemMark.GetAttribute("StatusCode");
                                    Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                                    Manager.ORManager.PersistChanges(mkg);
                                }
                            }
                            #endregion
                        }
                    }
                    value = "JobType=" + jobType + " , " + "RefNo=" + refN;
                }
            }

        }
        return value;
    }

    public static string Import_SeaExportFromExport(string filePath)
    {

        XmlDocument xmlDoc = new XmlDocument();
        string jobType = "";
        string refN = "";
        string refType = "";
        string value = "Error";
        if (filePath.Length > 0)
        {
            xmlDoc.Load(filePath);
            if (xmlDoc.SelectSingleNode("SeaExportRef") != null)
            {
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("SeaExportRef").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement elem = (XmlElement)xn;

                    SeaExportRef expRef = null;
                    if (expRef == null)
                    {
                        #region ref
                        expRef = new SeaExportRef();
                        refType = elem.GetAttribute("RefType");
                        string runType = "EXPORTREF";//SEF/SEL/SEC/ SCF/SCL/SCC/ SAE/SAC/SLT
                        if (refType == "SCF" || refType == "SCL" || refType == "SCC")
                            runType = "SeaCrossTrade";
                        refN = C2Setup.GetNextNo(refType, runType, DateTime.Now);
                        expRef.RefNo = refN;
                        expRef.JobType = elem.GetAttribute("JobType");
                        jobType = expRef.JobType;
                        expRef.RefDate = DateTime.Now;
                        expRef.Pol = EzshipHelper.GetPortCode(elem.GetAttribute("Pol"));
                        expRef.Pod = EzshipHelper.GetPortCode(elem.GetAttribute("Pod"));
                        expRef.AgentId = EzshipHelper.GetPartyId(elem.GetAttribute("Agent"));
                        string vesVoy = elem.GetAttribute("VesVoy");

                        string[] arr = vesVoy.Split('/');
                        if (arr.Length > 1)
                        {
                            expRef.Vessel = arr[0];
                            expRef.Voyage = arr[1];
                        }
                        expRef.Eta = SafeValue.SafeDate(elem.GetAttribute("Eta"), DateTime.Now);
                        expRef.Etd = SafeValue.SafeDate(elem.GetAttribute("Etd"), DateTime.Now);
                        expRef.FinDest = elem.GetAttribute("FinDest");
                        expRef.OblTerm = elem.GetAttribute("OblTerm");
                        expRef.ExpressBl = elem.GetAttribute("ExpressBl");
                        expRef.EtaDest = SafeValue.SafeDate(elem.GetAttribute("EtaDest"), DateTime.Now);
                        expRef.TransitDay = SafeValue.SafeInt(elem.GetAttribute("TransitDay"), 0);
                        expRef.StuffDate = SafeValue.SafeDate(elem.GetAttribute("StuffDate"), DateTime.Now);
                        expRef.SchFaxInd = elem.GetAttribute("SchFaxInd");
                        expRef.PortnetNo = elem.GetAttribute("PortnetNo");
                        expRef.SchFaxInd = elem.GetAttribute("SchFaxInd");
                        expRef.OblNo = elem.GetAttribute("OblNo");
                        expRef.CrBkgNo = elem.GetAttribute("CrBkgNo");
                        expRef.CrAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("CrAgent"));
                        expRef.NvoccAgentId = EzshipHelper.GetPartyId(elem.GetAttribute("NvoccAgent"));
                        expRef.NvoccBl = elem.GetAttribute("NvoccBl");
                        expRef.WarehouseId = EzshipHelper.GetPartyId(elem.GetAttribute("Warehouse"));
                        expRef.LocalCust = EzshipHelper.GetPartyId(elem.GetAttribute("LocalCustName"));
                        expRef.Weight = SafeValue.SafeDecimal(elem.GetAttribute("Wt"));
                        expRef.Volume = SafeValue.SafeDecimal(elem.GetAttribute("M3"));
                        expRef.Qty = SafeValue.SafeInt(elem.GetAttribute("Qty"), 0);
                        expRef.PackageType = elem.GetAttribute("PackageType");
                        expRef.ExRate = SafeValue.SafeDecimal(elem.GetAttribute("ExRate"));
                        expRef.CurrencyId = elem.GetAttribute("RefCurrency");
                        expRef.SShipperRemark = elem.GetAttribute("SShipperRemark");
                        expRef.SAgentRemark = elem.GetAttribute("SAgentRemark");
                        expRef.SConsigneeRemark = elem.GetAttribute("SConsigneeRemark");
                        expRef.SNotifyPartyRemark = elem.GetAttribute("SNotifyPartyRemark");
                        expRef.HaulierName = elem.GetAttribute("HaulierName");
                        expRef.HaulierCrNo = elem.GetAttribute("HaulierCrNo");
                        expRef.HaulierAttention = elem.GetAttribute("HaulierAttention");
                        expRef.HaulierCollect = elem.GetAttribute("HaulierCollect");
                        expRef.HaulierCollectDate = SafeValue.SafeDate(elem.GetAttribute("HaulierCollectDate"), DateTime.Now);
                        expRef.HaulierCollectTime = elem.GetAttribute("HaulierCollectTime");
                        expRef.HaulierDeliveryDate = SafeValue.SafeDate(elem.GetAttribute("HaulierDeliveryDate"), DateTime.Now);
                        expRef.HaulierDeliveryTime = elem.GetAttribute("HaulierDeliveryTime");
                        expRef.HaulierRemark = elem.GetAttribute("HaulierRemark");
                        expRef.HaulierTruck = elem.GetAttribute("HaulierTruck");
                        expRef.HaulierFax = elem.GetAttribute("HaulierFax");
                        expRef.DriverName = elem.GetAttribute("DriverName");
                        expRef.DriverMobile = elem.GetAttribute("DriverMobile");
                        expRef.DriverLicense = elem.GetAttribute("DriverLicense");
                        expRef.VehicleNo = elem.GetAttribute("VehicleNo");
                        expRef.VehicleType = elem.GetAttribute("VehicleType");
                        expRef.DriverRemark = elem.GetAttribute("DriverRemark");
                        expRef.HaulierSendTo = elem.GetAttribute("HaulierSendTo");
                        expRef.HaulierStuffBy = elem.GetAttribute("HaulierStuffBy");
                        expRef.HaulierPerson = elem.GetAttribute("HaulierPerson");
                        expRef.HaulierPersonTel = elem.GetAttribute("HaulierPersonTel");
                        expRef.HaulierCoload = elem.GetAttribute("HaulierCoload");

                        expRef.PoNo = elem.GetAttribute("PoNo");
                        expRef.Remark = elem.GetAttribute("Remark");
                        expRef.PermitRmk = elem.GetAttribute("PermitRmk");
                        expRef.StatusCode = elem.GetAttribute("StatusCode");
                        expRef.RefType = refType;
                        expRef.CreateBy = EzshipHelper.GetUserName();
                        expRef.CreateDateTime = DateTime.Now;
                        expRef.CreateBy = EzshipHelper.GetUserName();
                        expRef.UpdateDateTime = DateTime.Now;
                        C2Setup.SetNextNo(refType, runType, expRef.RefNo, DateTime.Now);
                        Manager.ORManager.StartTracking(expRef, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(expRef);
                        #endregion

                        XmlNodeList nodeListCont = xn.SelectNodes("Container");
                        #region Container
                        foreach (XmlNode xnMkg in nodeListCont)
                        {
                            XmlElement elemMark = (XmlElement)xnMkg;
                            if (elemMark.LocalName == "Container")
                            {
                                if (expRef.RefType == "SEL")
                                {
                                    C2.SeaExportMkg mkg = new SeaExportMkg();
                                    mkg.JobNo = "";
                                    mkg.RefNo = refN;

                                    string mkgType = elemMark.GetAttribute("MkgType");
                                    mkg.MkgType = mkgType;
                                    mkg.ContainerNo = elemMark.GetAttribute("ContainerNo");
                                    mkg.SealNo = elemMark.GetAttribute("SealNo");
                                    mkg.ContainerType = elemMark.GetAttribute("ContainerType");
                                    mkg.StatusCode = elemMark.GetAttribute("StatusCode");
                                    Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                                    Manager.ORManager.PersistChanges(mkg);
                                }
                            }
                        }
                        #endregion

                        XmlNode nodeListHose = xmlDoc.SelectSingleNode("SeaExportRef").LastChild;
                        foreach (XmlNode xnCont in nodeListHose)
                        {
                            C2.SeaExport exp = new SeaExport();
                            XmlElement elemCont = (XmlElement)xnCont;

                            if (elemCont.LocalName == "House")
                            {
                                #region job
                                exp.JobNo = C2Setup.GetSubNo(refN, "SE");
                                exp.RefNo = refN;
                                exp.BkgRefNo = elemCont.GetAttribute("BkgRefNo");
                                exp.HblNo = elemCont.GetAttribute("HblNo");
                                exp.CustomerId = EzshipHelper.GetPartyId(elemCont.GetAttribute("CustomerName"));
                                exp.Weight = SafeValue.SafeDecimal(elemCont.GetAttribute("Weight"));
                                exp.Volume = SafeValue.SafeDecimal(elemCont.GetAttribute("Volume"));
                                exp.Qty = SafeValue.SafeInt(elemCont.GetAttribute("Qty"), 0);
                                exp.PackageType = elemCont.GetAttribute("PackageType");
                                exp.ExpressBl = elemCont.GetAttribute("ExpressBl");
                                exp.PoNo = elemCont.GetAttribute("PoNo");
                                exp.FinDest = elemCont.GetAttribute("FinDest");
                                exp.ShipperId = elemCont.GetAttribute("ShipperId");
                                exp.ShipperContact = elemCont.GetAttribute("ShipperContact");
                                exp.ShipperName = elemCont.GetAttribute("ShipperName");
                                exp.ShipperTel = elemCont.GetAttribute("ShipperTel");
                                exp.ShipperFax = elemCont.GetAttribute("ShipperFax");
                                exp.ShipperEmail = elemCont.GetAttribute("ShipperEmail");
                                exp.AsAgent = elemCont.GetAttribute("AsAgent");
                                exp.QuoteNo = elemCont.GetAttribute("QuoteNo");
                                exp.PoNo = elemCont.GetAttribute("PoNo");
                                exp.ValueCurrency = elemCont.GetAttribute("ValueCurrency");
                                exp.ValueExRate = SafeValue.SafeDecimal(elemCont.GetAttribute("ValueExRate"));
                                exp.ValueAmt = SafeValue.SafeDecimal(elemCont.GetAttribute("ValueAmt"));
                                exp.Remark = elemCont.GetAttribute("Remark");
                                exp.TsInd = elemCont.GetAttribute("TsInd");
                                exp.CollectFrom = elemCont.GetAttribute("CollectFrom");
                                exp.Marking = elemCont.GetAttribute("Marking");
                                exp.Pol = elemCont.GetAttribute("Pol");
                                exp.Pod = elemCont.GetAttribute("Pod");
                                exp.PlaceLoadingName = elemCont.GetAttribute("PlaceLoadingName");
                                exp.PlaceDischargeName = elemCont.GetAttribute("PlaceDischargeName");
                                exp.PreCarriage = elemCont.GetAttribute("PreCarriage");
                                exp.ShipOnBoardInd = elemCont.GetAttribute("ShipOnBoardInd");
                                exp.ShipOnBoardDate = SafeValue.SafeDate(elem.GetAttribute("ShipOnBoardDate"), DateTime.Now);
                                exp.FrtTerm = elemCont.GetAttribute("FrtTerm");
                                exp.PlaceDeliveryId = elemCont.GetAttribute("PlaceDeliveryId");
                                exp.PlaceDeliveryName = elemCont.GetAttribute("PlaceDeliveryName");
                                exp.PlaceReceiveId = elemCont.GetAttribute("PlaceReceiveId");
                                exp.PlaceReceiveName = elemCont.GetAttribute("PlaceReceiveName");
                                exp.PlaceDeliveryTerm = elemCont.GetAttribute("PlaceDeliveryTerm");
                                exp.PlaceReceiveTerm = elemCont.GetAttribute("PlaceReceiveTerm");
                                exp.ExpressBl = elemCont.GetAttribute("ExpressBl");
                                exp.PackageType = elemCont.GetAttribute("PackageType");
                                exp.SShipperRemark = elemCont.GetAttribute("SShipperRemark");
                                exp.SConsigneeRemark = elemCont.GetAttribute("SConsigneeRemark");
                                exp.SNotifyPartyRemark = elemCont.GetAttribute("SNotifyPartyRemark");
                                exp.SAgentRemark = elemCont.GetAttribute("SAgentRemark");
                                exp.SurrenderBl = elemCont.GetAttribute("SurrenderBl");
                                exp.PODBy = elemCont.GetAttribute("PODBy");
                                exp.PODTime = SafeValue.SafeDate(elemCont.GetAttribute("PODTime"), DateTime.Now);
                                exp.PODRemark = elemCont.GetAttribute("PODRemark");
                                exp.PODUpdateUser = EzshipHelper.GetUserName();
                                exp.PODUpdateTime = DateTime.Now;
                                exp.HaulierName = elemCont.GetAttribute("HaulierName");
                                exp.HaulierCrNo = elemCont.GetAttribute("HaulierCrNo");
                                exp.HaulierFax = elemCont.GetAttribute("HaulierFax");
                                exp.HaulierAttention = elemCont.GetAttribute("HaulierAttention");
                                exp.DriverName = elemCont.GetAttribute("DriverName");
                                exp.DriverMobile = elemCont.GetAttribute("DriverMobile");
                                exp.DriverLicense = elemCont.GetAttribute("DriverLicense");
                                exp.DriverRemark = elemCont.GetAttribute("DriverRemark");
                                exp.VehicleNo = elemCont.GetAttribute("VehicleNo");
                                exp.VehicleType = elemCont.GetAttribute("VehicleType");
                                exp.HaulierCollect = elemCont.GetAttribute("HaulierCollect");
                                exp.HaulierTruck = elemCont.GetAttribute("HaulierTruck");
                                exp.HaulierRemark = elemCont.GetAttribute("HaulierRemark");
                                exp.HaulierCollectDate = SafeValue.SafeDate(elem.GetAttribute("HaulierCollectDate"), DateTime.Now);
                                exp.HaulierCollectTime = elemCont.GetAttribute("HaulierCollectTime");
                                exp.HaulierDeliveryTime = elemCont.GetAttribute("HaulierDeliveryTime");
                                exp.HaulierDeliveryDate = SafeValue.SafeDate(elem.GetAttribute("HaulierDeliveryDate"), DateTime.Now);
                                exp.HaulierSendTo = elemCont.GetAttribute("HaulierSendTo");
                                exp.HaulierStuffBy = elemCont.GetAttribute("HaulierStuffBy");
                                exp.HaulierCoload = elemCont.GetAttribute("HaulierCoload");
                                exp.HaulierPerson = elemCont.GetAttribute("HaulierPerson");
                                exp.HaulierPersonTel = elemCont.GetAttribute("HaulierPersonTel");
                                exp.PermitRmk = elemCont.GetAttribute("PermitRmk");
                                exp.Dept = elemCont.GetAttribute("Department");

                                exp.DgImdgClass = elemCont.GetAttribute("DgImdgClass");
                                exp.DgUnNo = elemCont.GetAttribute("DgUnNo");
                                exp.DgShippingName = elemCont.GetAttribute("DgShippingName");
                                exp.DgTecnicalName = elemCont.GetAttribute("DgTecnicalName");
                                exp.DgMfagNo1 = elemCont.GetAttribute("DgMfagNo1");
                                exp.DgMfagNo2 = elemCont.GetAttribute("DgMfagNo2");
                                exp.DgEmsFire = elemCont.GetAttribute("DgEmsFire");
                                exp.DgEmsSpill = elemCont.GetAttribute("DgEmsSpill");
                                exp.DgNetWeight = elemCont.GetAttribute("DgNetWeight");
                                exp.DgFlashPoint = elemCont.GetAttribute("DgFlashPoint");
                                exp.DgRadio = elemCont.GetAttribute("DgRadio");
                                exp.DgPageNo = elemCont.GetAttribute("DgPageNo");
                                exp.DgPackingGroup = elemCont.GetAttribute("DgPackingGroup");
                                exp.DgTransportCode = elemCont.GetAttribute("DgTransportCode");
                                exp.DgPackingTypeCode = elemCont.GetAttribute("DgPackingTypeCode");
                                exp.DgCategory = elemCont.GetAttribute("DgCategory");
                                exp.DgLimitedQtyInd = elemCont.GetAttribute("DgLimitedQtyInd");
                                exp.DgExemptedQtyInd = elemCont.GetAttribute("DgExemptedQtyInd");

                                exp.StatusCode = elemCont.GetAttribute("StatusCode");
                                Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Inserted);
                                Manager.ORManager.PersistChanges(exp);

                                #endregion

                                #region Booking
                                XmlNodeList nodeListMkg = xnCont.SelectNodes("Booking");
                                foreach (XmlNode xnMkg in nodeListMkg)
                                {
                                    XmlElement elemMark = (XmlElement)xnMkg;
                                    if (elemMark.LocalName == "Booking")
                                    {
                                        C2.SeaExportMkg mkg = new SeaExportMkg();
                                        mkg.JobNo = exp.JobNo;
                                        mkg.RefNo = refN;
                                        mkg.Weight = SafeValue.SafeDecimal(elemMark.GetAttribute("Weight"));
                                        mkg.Volume = SafeValue.SafeDecimal(elemMark.GetAttribute("Volume"));
                                        mkg.Qty = SafeValue.SafeInt(elemMark.GetAttribute("Qty"), 0);
                                        mkg.PackageType = elemMark.GetAttribute("PackageType");
                                        string mkgType = elemMark.GetAttribute("MkgType");
                                        mkg.MkgType = mkgType;
                                        mkg.ContainerNo = elemMark.GetAttribute("ContainerNo");
                                        mkg.SealNo = elemMark.GetAttribute("SealNo");
                                        mkg.ContainerType = elemMark.GetAttribute("ContainerType");
                                        mkg.GrossWt = SafeValue.SafeDecimal(elemMark.GetAttribute("GrossWt"));
                                        mkg.NetWt = SafeValue.SafeDecimal(elemMark.GetAttribute("NetWt"));
                                        mkg.Marking = elemMark.GetAttribute("Marking");
                                        mkg.Description = elemMark.GetAttribute("Description");
                                        mkg.StatusCode = elemMark.GetAttribute("StatusCode");
                                        Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
                                        Manager.ORManager.PersistChanges(mkg);
                                    }
                                }
                                #endregion
                            }

                        }
                    }
                }
            }
            value = "JobType=" + jobType + " , " + "RefNo=" + refN;
        }

        return value;
    }

}