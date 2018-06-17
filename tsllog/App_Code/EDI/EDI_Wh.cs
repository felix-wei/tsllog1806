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
public class EDI_Wh
{
    public static XmlDocument Export_order(string orderNo, string orderType)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><Order></Order>");
        XmlNode xmlNode = null;

        string element = "Order";
        string sql_mast = string.Format(@"Select Id,RelaId,DoNo,DoType,DoDate,PoNo,PoDate,PartyName as PartyName,PartyPostalCode as PartyPostal,PartyCity,PartyCountry,PartyAdd as PartyAddress,
CustomerReference as CustomsRef,CustomerDate as RefDate,PoNo,PoDate,PartyInvNo,PartyInvDate,PermitNo,PermitApprovalDate,TptMode,PalletizedInd,EquipNo,Personnel,
CollectFrom as PickFrom,DeliveryTo as DeliveryTo,Remark1,Remark2,Priority,ExpectedDate,DoStatus,
Vessel,Voyage,Obl as OceanBl,Hbl as HouseBl,Pol,Pod,Eta,Etd,EtaDest,Carrier,Vehicle,Coo,
AgentName as Agent,AgentTel,AgentContact,AgentAdd as AgentAddress,AgentZip,AgentCity,AgentCountry,
NotifyName as Notify,NotifyTel,NotifyContact,NotifyAdd as NotifyAddress,NotifyZip,NotifyCity,NotifyCountry,
ConsigneeName Cnee,ConsigneeTel CneeTel,ConsigneeContact CneeContact,ConsigneeAdd CneeAddress,ConsigneeZip as CneeZip,ConsigneeCity as CneeCity,ConsigneeCountry as CneeCountry,PermitBy,OtherPermit,ModelType from Wh_Do  WHERE (DoNo = '{0}' ) and DoType='{1}'", orderNo, orderType);
        string sql_job = string.Format(@"select Id,RelaId,BatchNo,CustomsLot,LotNo,ProductCode Sku,Des1 Des,Qty1 PackQty,Qty2 WholeQty,Qty3 LooseQty,Qty4 Expected,Qty5 Transit,QtyPackWhole,QtyWholeLoose,QtyLooseBase,Uom1 PackUom,Uom2 WholeUom,Uom3 LooseUom,Uom4 BaseUom,Price,Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10 from wh_DoDet WHERE (DoNo = '{0}' ) and DoType='{1}'", orderNo, orderType);
        string sql_job1 = string.Format(@"select Id,RelaId,Product,Location,ProcessStatus,LotNo,BatchNo,CustomsLot,Des1 Des,Qty1 PackQty,Qty2 WholeQty,Qty3 LooseQty,QtyPackWhole,QtyWholeLoose,QtyLooseBase,Uom1 PackUom,Uom2 WholeUom,Uom3 LooseUom,Uom4 BaseUom,Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10 from wh_DoDet2 WHERE (DoNo = '{0}' ) and DoType='{1}'", orderNo, orderType);
        string sql_job2 = string.Format(@"select Id,RelaId,ContainerNo,ContainerType,SealNo,Weight,M3,Qty,PkgType from Wh_DoDet3 WHERE (DoNo = '{0}' ) and DoType='{1}'", orderNo, orderType);
        xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><Order></Order>");
        xmlNode = xmlDoc.SelectSingleNode("Order");
        DataTable mast = ConnectSql.GetDataSet(sql_mast).Tables[0];
        DataTable tab_mark = ConnectSql.GetDataSet(sql_job).Tables[0];
        DataTable tab_job1 = ConnectSql.GetDataSet(sql_job1).Tables[0];
        DataTable tab_job2 = ConnectSql.GetDataSet(sql_job2).Tables[0];
        if (mast.Rows.Count != 1 || tab_mark.Rows.Count == 0)
        {
            return null;
        }
        XmlElement item = null;
        for (int j = 0; j < mast.Rows.Count; j++)
        {
            item = xmlNode as XmlElement;//xmlDoc.CreateElement("" + element + "");
            item.SetAttribute("Id", mast.Rows[j]["Id"].ToString());
            item.SetAttribute("RelaId", mast.Rows[j]["RelaId"].ToString());
            item.SetAttribute("OrderNo", mast.Rows[j]["DoNo"].ToString());
            item.SetAttribute("OrderType", mast.Rows[j]["DoType"].ToString());
            item.SetAttribute("OrderDate", SafeValue.SafeDate(mast.Rows[j]["DoDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("PoNo", mast.Rows[j]["PoNo"].ToString());
            item.SetAttribute("PoDate", SafeValue.SafeDate(mast.Rows[j]["PoDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("Type", mast.Rows[j]["Priority"].ToString());
            item.SetAttribute("ExpectedDate", SafeValue.SafeDate(mast.Rows[j]["ExpectedDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("DoStatus", mast.Rows[j]["DoStatus"].ToString());
            //from company to warehouse
            if (System.Configuration.ConfigurationManager.AppSettings["WhService_ToStock"].Length > 0)
            {
                string partyId = System.Configuration.ConfigurationManager.AppSettings["PartyIdInStock"];
                string partyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                //partyName += "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"];
                //partyName += "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"];
                //partyName += "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
                item.SetAttribute("PartyId", partyId);
                item.SetAttribute("Party", partyName);
                item.SetAttribute("PartyAddress", mast.Rows[j]["PartyName"].ToString() + "\n" + mast.Rows[j]["PartyAddress"].ToString());
            }
            else// from warehouse to company
            {
                item.SetAttribute("PartyId", "");
                item.SetAttribute("Party", "");
                item.SetAttribute("PartyAddress", "");
            }            
            //item.SetAttribute("Party", mast.Rows[j]["PartyName"].ToString());
            //item.SetAttribute("PartyPostal", mast.Rows[j]["PartyPostal"].ToString());
            //item.SetAttribute("PartyCity", mast.Rows[j]["PartyCity"].ToString());
            //item.SetAttribute("PartyCountry", mast.Rows[j]["PartyCountry"].ToString());
            //item.SetAttribute("PartyAddress", mast.Rows[j]["PartyAddress"].ToString());
                



            item.SetAttribute("PickFrom", mast.Rows[j]["PickFrom"].ToString());
            item.SetAttribute("DeliveryTo", mast.Rows[j]["DeliveryTo"].ToString());
            item.SetAttribute("Remark1", mast.Rows[j]["Remark1"].ToString());
            item.SetAttribute("Remark2", mast.Rows[j]["Remark2"].ToString());
            item.SetAttribute("CustomsRef", mast.Rows[j]["CustomsRef"].ToString());
            item.SetAttribute("RefDate", SafeValue.SafeDate(mast.Rows[j]["RefDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("PoNo", mast.Rows[j]["PoNo"].ToString());
            item.SetAttribute("PoDate", SafeValue.SafeDate(mast.Rows[j]["PoDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("PartyInvNo", mast.Rows[j]["PartyInvNo"].ToString());
            item.SetAttribute("PartyInvDate", SafeValue.SafeDate(mast.Rows[j]["PartyInvDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("PermitNo", mast.Rows[j]["PermitNo"].ToString());
            item.SetAttribute("PermitApprovalDate", SafeValue.SafeDate(mast.Rows[j]["PermitApprovalDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("PermitBy", mast.Rows[j]["PermitBy"].ToString());
            item.SetAttribute("OtherPermit", mast.Rows[j]["OtherPermit"].ToString());
            item.SetAttribute("ModelType", mast.Rows[j]["ModelType"].ToString());
            
            item.SetAttribute("TptMode", mast.Rows[j]["TptMode"].ToString());
            item.SetAttribute("PalletizedInd", SafeValue.SafeBool(mast.Rows[j]["PalletizedInd"], true).ToString());
            item.SetAttribute("EquipNo", mast.Rows[j]["EquipNo"].ToString());
            item.SetAttribute("Personnel", mast.Rows[j]["Personnel"].ToString());
            #region shipment info
            item.SetAttribute("Vessel", SafeValue.SafeString(mast.Rows[j]["Vessel"]));
            item.SetAttribute("Voyage", SafeValue.SafeString(mast.Rows[j]["Voyage"]));
            item.SetAttribute("OceanBl", SafeValue.SafeString(mast.Rows[j]["OceanBl"]));
            item.SetAttribute("HouseBl", SafeValue.SafeString(mast.Rows[j]["HouseBl"]));
            item.SetAttribute("Pol", SafeValue.SafeString(mast.Rows[j]["Pol"]));
            item.SetAttribute("Pod", SafeValue.SafeString(mast.Rows[j]["Pod"]));
            item.SetAttribute("Eta", SafeValue.SafeDate(mast.Rows[j]["Eta"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("Etd", SafeValue.SafeDate(mast.Rows[j]["Etd"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("EtaDest", SafeValue.SafeDate(mast.Rows[j]["EtaDest"], DateTime.Today).ToString("yyyy-MM-dd"));
            item.SetAttribute("Carrier", SafeValue.SafeString(mast.Rows[j]["Carrier"]));
            item.SetAttribute("Vehicle", SafeValue.SafeString(mast.Rows[j]["Vehicle"]));
            item.SetAttribute("Coo", SafeValue.SafeString(mast.Rows[j]["Coo"]));
            #endregion
            #region party
            //party-agent
            item.SetAttribute("Agent", SafeValue.SafeString(mast.Rows[j]["Agent"]));
            item.SetAttribute("AgentContact", SafeValue.SafeString(mast.Rows[j]["AgentContact"]));
            item.SetAttribute("AgentTel", SafeValue.SafeString(mast.Rows[j]["AgentTel"]));
            item.SetAttribute("AgentAddress", SafeValue.SafeString(mast.Rows[j]["AgentAddress"]));
            item.SetAttribute("AgentZip", SafeValue.SafeString(mast.Rows[j]["AgentZip"]));
            item.SetAttribute("AgentCity", SafeValue.SafeString(mast.Rows[j]["AgentCity"]));
            item.SetAttribute("AgentCountry", SafeValue.SafeString(mast.Rows[j]["AgentCountry"]));
            //party-notify party
            item.SetAttribute("Notify", SafeValue.SafeString(mast.Rows[j]["Notify"]));
            item.SetAttribute("NotifyContact", SafeValue.SafeString(mast.Rows[j]["NotifyContact"]));
            item.SetAttribute("NotifyTel", SafeValue.SafeString(mast.Rows[j]["NotifyTel"]));
            item.SetAttribute("NotifyAddress", SafeValue.SafeString(mast.Rows[j]["NotifyAddress"]));
            item.SetAttribute("NotifyZip", SafeValue.SafeString(mast.Rows[j]["NotifyZip"]));
            item.SetAttribute("NotifyCity", SafeValue.SafeString(mast.Rows[j]["NotifyCity"]));
            item.SetAttribute("NotifyCountry", SafeValue.SafeString(mast.Rows[j]["NotifyCountry"]));
            //party-consignee
            item.SetAttribute("Cnee", SafeValue.SafeString(mast.Rows[j]["Cnee"]));
            item.SetAttribute("CneeContact", SafeValue.SafeString(mast.Rows[j]["CneeContact"]));
            item.SetAttribute("CneeTel", SafeValue.SafeString(mast.Rows[j]["CneeTel"]));
            item.SetAttribute("CneeAddress", SafeValue.SafeString(mast.Rows[j]["CneeAddress"]));
            item.SetAttribute("CneeZip", SafeValue.SafeString(mast.Rows[j]["CneeZip"]));
            item.SetAttribute("CneeCity", SafeValue.SafeString(mast.Rows[j]["CneeCity"]));
            item.SetAttribute("CneeCountry", SafeValue.SafeString(mast.Rows[j]["CneeCountry"]));
            #endregion
            #region Sku

            for (int k = 0; k < tab_mark.Rows.Count; k++)
            {
                XmlElement cont = xmlDoc.CreateElement("Sku");
                cont.SetAttribute("Id", SafeValue.SafeString(tab_mark.Rows[k]["Id"]));
                cont.SetAttribute("LotNo", SafeValue.SafeString(tab_mark.Rows[k]["LotNo"]));
                cont.SetAttribute("Sku", SafeValue.SafeString(tab_mark.Rows[k]["Sku"]));
                cont.SetAttribute("Des", SafeValue.SafeString(tab_mark.Rows[k]["Des"]));
                cont.SetAttribute("CustomsLot", SafeValue.SafeString(tab_mark.Rows[k]["CustomsLot"]));
                cont.SetAttribute("BatchNo", SafeValue.SafeString(tab_mark.Rows[k]["BatchNo"]));
                cont.SetAttribute("Expected", SafeValue.SafeString(tab_mark.Rows[k]["Expected"]));
                cont.SetAttribute("Transit", SafeValue.SafeString(tab_mark.Rows[k]["Transit"]));
                cont.SetAttribute("PackQty", SafeValue.SafeString(tab_mark.Rows[k]["PackQty"]));
                cont.SetAttribute("WholeQty", SafeValue.SafeString(tab_mark.Rows[k]["WholeQty"]));
                cont.SetAttribute("LooseQty", SafeValue.SafeString(tab_mark.Rows[k]["LooseQty"]));
                cont.SetAttribute("QtyPackWhole", SafeValue.SafeString(tab_mark.Rows[k]["QtyPackWhole"]));
                cont.SetAttribute("QtyWholeLoose", SafeValue.SafeString(tab_mark.Rows[k]["QtyWholeLoose"]));
                cont.SetAttribute("QtyLooseBase", SafeValue.SafeString(tab_mark.Rows[k]["QtyLooseBase"]));
                cont.SetAttribute("PackUom", SafeValue.SafeString(tab_mark.Rows[k]["PackUom"]));
                cont.SetAttribute("WholeUom", SafeValue.SafeString(tab_mark.Rows[k]["WholeUom"]));
                cont.SetAttribute("LooseUom", SafeValue.SafeString(tab_mark.Rows[k]["LooseUom"]));
                cont.SetAttribute("BaseUom", SafeValue.SafeString(tab_mark.Rows[k]["BaseUom"]));
                cont.SetAttribute("Price", SafeValue.SafeString(tab_mark.Rows[k]["Price"]));
                cont.SetAttribute("Att1", SafeValue.SafeString(tab_mark.Rows[k]["Att1"]));
                cont.SetAttribute("Att2", SafeValue.SafeString(tab_mark.Rows[k]["Att2"]));
                cont.SetAttribute("Att3", SafeValue.SafeString(tab_mark.Rows[k]["Att3"]));
                cont.SetAttribute("Att4", SafeValue.SafeString(tab_mark.Rows[k]["Att4"]));
                cont.SetAttribute("Att5", SafeValue.SafeString(tab_mark.Rows[k]["Att5"]));
                cont.SetAttribute("Att6", SafeValue.SafeString(tab_mark.Rows[k]["Att6"]));
                cont.SetAttribute("Att7", SafeValue.SafeString(tab_mark.Rows[k]["Att7"]));
                cont.SetAttribute("Att8", SafeValue.SafeString(tab_mark.Rows[k]["Att8"]));
                cont.SetAttribute("Att9", SafeValue.SafeString(tab_mark.Rows[k]["Att9"]));
                cont.SetAttribute("Att10", SafeValue.SafeString(tab_mark.Rows[k]["Att10"]));
                item.AppendChild(cont);
            }
            #endregion
            #region PutAway
            for (int a = 0; a < tab_job1.Rows.Count; a++)
            {
                XmlElement cont = xmlDoc.CreateElement("PutAway");
                cont.SetAttribute("Id", SafeValue.SafeString(tab_job1.Rows[a]["Id"]));
                cont.SetAttribute("LotNo", SafeValue.SafeString(tab_job1.Rows[a]["LotNo"]));
                cont.SetAttribute("Product", SafeValue.SafeString(tab_job1.Rows[a]["Product"]));
                cont.SetAttribute("Des", SafeValue.SafeString(tab_job1.Rows[a]["Des"]));
                cont.SetAttribute("PackQty", SafeValue.SafeString(tab_job1.Rows[a]["PackQty"]));
                cont.SetAttribute("WholeQty", SafeValue.SafeString(tab_job1.Rows[a]["WholeQty"]));
                cont.SetAttribute("LooseQty", SafeValue.SafeString(tab_job1.Rows[a]["LooseQty"]));
                cont.SetAttribute("QtyPackWhole", SafeValue.SafeString(tab_mark.Rows[a]["QtyPackWhole"]));
                cont.SetAttribute("QtyWholeLoose", SafeValue.SafeString(tab_mark.Rows[a]["QtyWholeLoose"]));
                cont.SetAttribute("QtyLooseBase", SafeValue.SafeString(tab_mark.Rows[a]["QtyLooseBase"]));
                cont.SetAttribute("PackUom", SafeValue.SafeString(tab_mark.Rows[a]["PackUom"]));
                cont.SetAttribute("WholeUom", SafeValue.SafeString(tab_mark.Rows[a]["WholeUom"]));
                cont.SetAttribute("LooseUom", SafeValue.SafeString(tab_mark.Rows[a]["LooseUom"]));
                cont.SetAttribute("BaseUom", SafeValue.SafeString(tab_mark.Rows[a]["BaseUom"]));
                cont.SetAttribute("Location", SafeValue.SafeString(tab_job1.Rows[a]["Location"]));
                cont.SetAttribute("ProcessStatus", SafeValue.SafeString(tab_job1.Rows[a]["ProcessStatus"]));
                cont.SetAttribute("CustomsLot", SafeValue.SafeString(tab_job1.Rows[a]["CustomsLot"]));
                cont.SetAttribute("BatchNo", SafeValue.SafeString(tab_job1.Rows[a]["BatchNo"]));
                cont.SetAttribute("Att1", SafeValue.SafeString(tab_job1.Rows[a]["Att1"]));
                cont.SetAttribute("Att2", SafeValue.SafeString(tab_job1.Rows[a]["Att2"]));
                cont.SetAttribute("Att3", SafeValue.SafeString(tab_job1.Rows[a]["Att3"]));
                cont.SetAttribute("Att4", SafeValue.SafeString(tab_job1.Rows[a]["Att4"]));
                cont.SetAttribute("Att5", SafeValue.SafeString(tab_job1.Rows[a]["Att5"]));
                cont.SetAttribute("Att6", SafeValue.SafeString(tab_job1.Rows[a]["Att6"]));
                cont.SetAttribute("Att7", SafeValue.SafeString(tab_job1.Rows[a]["Att7"]));
                cont.SetAttribute("Att8", SafeValue.SafeString(tab_job1.Rows[a]["Att8"]));
                cont.SetAttribute("Att9", SafeValue.SafeString(tab_job1.Rows[a]["Att9"]));
                cont.SetAttribute("Att10", SafeValue.SafeString(tab_job1.Rows[a]["Att10"]));
                item.AppendChild(cont);
            }
            #endregion
            #region Container
            for (int a = 0; a < tab_job2.Rows.Count; a++)
            {
                XmlElement cont = xmlDoc.CreateElement("Container");
                cont.SetAttribute("Id", SafeValue.SafeString(tab_job2.Rows[a]["Id"]));
                cont.SetAttribute("ContainerNo", SafeValue.SafeString(tab_job2.Rows[a]["ContainerNo"]));
                cont.SetAttribute("ContainerType", SafeValue.SafeString(tab_job2.Rows[a]["ContainerType"]));
                cont.SetAttribute("SealNo", SafeValue.SafeString(tab_job2.Rows[a]["SealNo"]));
                cont.SetAttribute("Weight", SafeValue.SafeString(tab_job2.Rows[a]["Weight"]));
                cont.SetAttribute("M3", SafeValue.SafeString(tab_job2.Rows[a]["M3"]));
                cont.SetAttribute("Qty", SafeValue.SafeString(tab_job2.Rows[a]["Qty"]));
                cont.SetAttribute("PkgType", SafeValue.SafeString(tab_job2.Rows[a]["PkgType"]));
                item.AppendChild(cont);
            }
            #endregion
            //xmlNode.AppendChild(item);
        }

        return xmlDoc;
    }

    public static string Import_Order(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string doNo = "";
        string doType = "";
        if (filePath.Length > 0)
        {
            xmlDoc.Load(filePath);
            if (xmlDoc.SelectSingleNode("Order") != null)
            {
                XmlElement elem = (XmlElement)xmlDoc.SelectSingleNode("Order");
                string id = SafeValue.SafeString(elem.GetAttribute("Id"));
                string relaId = SafeValue.SafeString(elem.GetAttribute("RelaId"));

                bool isNew = false;
                WhDo whDo = null;
                if (id.Length > 0 && relaId.Length == 0)// from company to warehouse
                {
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "RelaId='" + id + "'");
                    whDo = C2.Manager.ORManager.GetObject(query) as WhDo;

                    if (whDo == null)
                    {
                        isNew = true;
                        whDo = new WhDo();
                        whDo.RelaId = id;
                        whDo.DoDate = DateTime.Today;


                        whDo.PartyId = SafeValue.SafeString(elem.GetAttribute("PartyId"));
                        //whDo.PartyId = EzshipHelper.GetPartyId(SafeValue.SafeString(elem.GetAttribute("PartyId")));
                        whDo.PartyName = SafeValue.SafeString(elem.GetAttribute("Party"));
                        whDo.PartyAdd = SafeValue.SafeString(elem.GetAttribute("PartyAddress"));
                    }
                }
                else if (id.Length > 0 && relaId.Length > 0)// from warehouse to company
                {
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + relaId + "'");
                    whDo = C2.Manager.ORManager.GetObject(query) as WhDo;
                    if (whDo == null)
                    {
                        return "";
                        //isNew = true;
                        //whDo = new WhDo();
                        //whDo.RelatId = id;

                    }
                }
                else
                    return "";

                doType = "IN";
                string runType = "DOIN";//SIF/SIL/SIC
                if (SafeValue.SafeString(elem.GetAttribute("OrderType")) == "OUT")
                {
                    doType = "OUT";
                    runType = "DOOUT";
                }
                #region ref
                whDo.PoNo = SafeValue.SafeString(elem.GetAttribute("PoNo"));
                whDo.PoDate = SafeValue.SafeDate(elem.GetAttribute("PoDate"), DateTime.Today);
                //whDo.PartyId = EzshipHelper.GetPartyId(SafeValue.SafeString(elem.GetAttribute("Party")));
                //whDo.PartyName = SafeValue.SafeString(elem.GetAttribute("Party"));
                //whDo.PartyAdd = SafeValue.SafeString(elem.GetAttribute("PartyAddress"));
                //whDo.PartyPostalcode = SafeValue.SafeString(elem.GetAttribute("PartyPostal"));
                //whDo.PartyCity = SafeValue.SafeString(elem.GetAttribute("PartyCity"));
                //whDo.PartyCountry = SafeValue.SafeString(elem.GetAttribute("PartyCountry"));

                whDo.Priority = SafeValue.SafeString(elem.GetAttribute("Type"));
                whDo.ExpectedDate = SafeValue.SafeDate(elem.GetAttribute("ExpectedDate"), DateTime.Today);
                whDo.DoStatus = SafeValue.SafeString(elem.GetAttribute("DoStatus"));
                whDo.Remark1 = SafeValue.SafeString(elem.GetAttribute("Remark1"));
                whDo.Remark2 = SafeValue.SafeString(elem.GetAttribute("Remark2"));
                whDo.CollectFrom = SafeValue.SafeString(elem.GetAttribute("PickFrom"));
                whDo.DeliveryTo = SafeValue.SafeString(elem.GetAttribute("DeliveryTo"));

                //whDo.CustomerReference = SafeValue.SafeString(elem.GetAttribute("CustomsRef"));
                //whDo.CustomerDate= SafeValue.SafeDate(elem.GetAttribute("RefDate"), new DateTime(1753, 1, 1));

                whDo.CustomerReference = SafeValue.SafeString(elem.GetAttribute("OrderNo"));
                whDo.CustomerDate = SafeValue.SafeDate(elem.GetAttribute("OrderDate"), new DateTime(1753, 1, 1));

                whDo.PartyInvNo = SafeValue.SafeString(elem.GetAttribute("PartyInvNo"));
                whDo.PartyInvDate = SafeValue.SafeDate(elem.GetAttribute("PartyInvDate"), new DateTime(1753, 5, 1));
                //permit no
                whDo.PermitNo = SafeValue.SafeString(elem.GetAttribute("PermitNo"));
                whDo.PermitApprovalDate = SafeValue.SafeDate(elem.GetAttribute("PermitApprovalDate"), new DateTime(1753, 5, 1));
                whDo.TptMode = SafeValue.SafeString(elem.GetAttribute("TptMode"));
                whDo.PalletizedInd = SafeValue.SafeBool(elem.GetAttribute("PalletizedInd"), true);
                whDo.EquipNo = SafeValue.SafeString(elem.GetAttribute("EquipNo"));
                whDo.Personnel = SafeValue.SafeString(elem.GetAttribute("Personnel"));
                //shipment
                whDo.Vessel = SafeValue.SafeString(elem.GetAttribute("Vessel"));
                whDo.Voyage = SafeValue.SafeString(elem.GetAttribute("Voyage"));
                whDo.Obl = SafeValue.SafeString(elem.GetAttribute("OceanBl"));
                whDo.Hbl = SafeValue.SafeString(elem.GetAttribute("HouseBl"));
                whDo.Pol = SafeValue.SafeString(elem.GetAttribute("Pol"));
                whDo.Pod = SafeValue.SafeString(elem.GetAttribute("Pod"));
                whDo.Eta = SafeValue.SafeDate(elem.GetAttribute("Eta"), new DateTime(1753, 1, 1));
                whDo.Etd = SafeValue.SafeDate(elem.GetAttribute("Etd"), new DateTime(1753, 1, 1));
                whDo.EtaDest = SafeValue.SafeDate(elem.GetAttribute("EtaDest"), new DateTime(1753, 1, 1));
                whDo.Vehicle = SafeValue.SafeString(elem.GetAttribute("Vehicle"));
                whDo.Coo = SafeValue.SafeString(elem.GetAttribute("Coo"));
                whDo.Carrier = SafeValue.SafeString(elem.GetAttribute("Carrier"));
                //party
                whDo.AgentId = EzshipHelper.GetPartyId(SafeValue.SafeString(elem.GetAttribute("Agent")));
                whDo.AgentName = SafeValue.SafeString(elem.GetAttribute("Agent"));
                whDo.AgentZip = SafeValue.SafeString(elem.GetAttribute("AgentZip"));
                whDo.AgentAdd = SafeValue.SafeString(elem.GetAttribute("AgentAddress"));
                whDo.AgentTel = SafeValue.SafeString(elem.GetAttribute("AgentTel"));
                whDo.AgentContact = SafeValue.SafeString(elem.GetAttribute("AgentContact"));
                whDo.AgentCountry = SafeValue.SafeString(elem.GetAttribute("AgentCountry"));
                whDo.AgentCity = SafeValue.SafeString(elem.GetAttribute("AgentCity"));

                whDo.NotifyId = EzshipHelper.GetPartyId(SafeValue.SafeString(elem.GetAttribute("Notify")));
                whDo.NotifyName = SafeValue.SafeString(elem.GetAttribute("Notify"));
                whDo.NotifyZip = SafeValue.SafeString(elem.GetAttribute("NotifyZip"));
                whDo.NotifyAdd = SafeValue.SafeString(elem.GetAttribute("NotifyAddress"));
                whDo.NotifyTel = SafeValue.SafeString(elem.GetAttribute("NotifyTel"));
                whDo.NotifyContact = SafeValue.SafeString(elem.GetAttribute("NotifyContact"));
                whDo.NotifyCountry = SafeValue.SafeString(elem.GetAttribute("NotifyCountry"));
                whDo.NotifyCity = SafeValue.SafeString(elem.GetAttribute("NotifyCity"));

                whDo.ConsigneeId = EzshipHelper.GetPartyId(SafeValue.SafeString(elem.GetAttribute("Cnee")));
                whDo.ConsigneeName = SafeValue.SafeString(elem.GetAttribute("Cnee"));
                whDo.ConsigneeZip = SafeValue.SafeString(elem.GetAttribute("CneeZip"));
                whDo.ConsigneeAdd = SafeValue.SafeString(elem.GetAttribute("CneeAddress"));
                whDo.ConsigneeTel = SafeValue.SafeString(elem.GetAttribute("CneeTel"));
                whDo.ConsigneeContact = SafeValue.SafeString(elem.GetAttribute("CneeContact"));
                whDo.ConsigneeCountry = SafeValue.SafeString(elem.GetAttribute("CneeCountry"));
                whDo.ConsigneeCity = SafeValue.SafeString(elem.GetAttribute("CneeCity"));
                whDo.PermitBy = SafeValue.SafeString(elem.GetAttribute("PermitBy"));
                whDo.OtherPermit = SafeValue.SafeString(elem.GetAttribute("OtherPermit"));
                whDo.ModelType = SafeValue.SafeString(elem.GetAttribute("ModelType"));

                if (isNew)
                {
                    doNo = C2Setup.GetNextNo("", runType, DateTime.Today);
                    whDo.DoNo = doNo;
                    whDo.DoType = doType;
                    whDo.StatusCode = "USE";
                    whDo.CreateBy = EzshipHelper.GetUserName();
                    whDo.CreateDateTime = DateTime.Today;
                    Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(whDo);
                    C2Setup.SetNextNo("", runType, doNo, whDo.DoDate);
                }
                else
                {
                    doNo = whDo.DoNo;
                    whDo.UpdateBy = EzshipHelper.GetUserName();
                    whDo.UpdateDateTime = DateTime.Now;
                    Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Updated);
                    Manager.ORManager.PersistChanges(whDo);
                }

                #endregion

                #region Delete
                if (!isNew)
                {
                    string sql_del = string.Format(@"delete from Wh_DoDet where DoNo='{0}'", doNo);
                    SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_del), 0);

                    sql_del = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}'", doNo);
                    SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_del), 0);

                    sql_del = string.Format(@"delete from Wh_DoDet3 where DoNo='{0}'", doNo);
                    SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_del), 0);
                }
                #endregion

                #region sku
                XmlNodeList nodeListCont = elem.SelectNodes("Sku");
                foreach (XmlNode xnMkg in nodeListCont)
                {
                    XmlElement elemSku = (XmlElement)xnMkg;
                    if (elemSku.Name == "Sku")
                    {
                        WhDoDet det = new WhDoDet();
                        det.DoNo = doNo;
                        det.DoType = doType;
                        det.LotNo = SafeValue.SafeString(elemSku.GetAttribute("LotNo"));
                        det.BatchNo = SafeValue.SafeString(elemSku.GetAttribute("BatchNo"));
                        det.CustomsLot = SafeValue.SafeString(elemSku.GetAttribute("CustomsLot"));
                        det.ProductCode = SafeValue.SafeString(elemSku.GetAttribute("Sku"));
                        det.Des1 = SafeValue.SafeString(elemSku.GetAttribute("Des"));
                        det.Qty1 = SafeValue.SafeInt(elemSku.GetAttribute("PackQty"), 0);
                        det.Qty2 = SafeValue.SafeInt(elemSku.GetAttribute("WholeQty"), 0);
                        det.Qty3 = SafeValue.SafeInt(elemSku.GetAttribute("LooseQty"), 0);
                        det.Qty4 = SafeValue.SafeInt(elemSku.GetAttribute("Expected"), 0);
                        det.Qty5 = SafeValue.SafeInt(elemSku.GetAttribute("Transit"), 0);
                        det.QtyPackWhole = SafeValue.SafeInt(elemSku.GetAttribute("QtyPackWhole"), 0);
                        det.QtyWholeLoose = SafeValue.SafeInt(elemSku.GetAttribute("QtyWholeLoose"), 0);
                        det.QtyLooseBase = SafeValue.SafeInt(elemSku.GetAttribute("QtyLooseBase"), 0);
                        det.Uom1 = SafeValue.SafeString(elemSku.GetAttribute("PackUom"));
                        det.Uom2 = SafeValue.SafeString(elemSku.GetAttribute("WholeUom"));
                        det.Uom3 = SafeValue.SafeString(elemSku.GetAttribute("LooseUom"));
                        det.Uom4 = SafeValue.SafeString(elemSku.GetAttribute("BaseUom"));
                        det.Att1 = SafeValue.SafeString(elemSku.GetAttribute("Att1"));
                        det.Att2 = SafeValue.SafeString(elemSku.GetAttribute("Att2"));
                        det.Att3 = SafeValue.SafeString(elemSku.GetAttribute("Att3"));
                        det.Att4 = SafeValue.SafeString(elemSku.GetAttribute("Att4"));
                        det.Att5 = SafeValue.SafeString(elemSku.GetAttribute("Att5"));
                        det.Att6 = SafeValue.SafeString(elemSku.GetAttribute("Att6"));
                        det.Att7 = SafeValue.SafeString(elemSku.GetAttribute("Att7"));
                        det.Att8 = SafeValue.SafeString(elemSku.GetAttribute("Att8"));
                        det.Att9 = SafeValue.SafeString(elemSku.GetAttribute("Att9"));
                        det.Att10 = SafeValue.SafeString(elemSku.GetAttribute("Att10"));
                        det.CreateBy = EzshipHelper.GetUserName();
                        det.CreateDateTime = DateTime.Now;
                        det.UpdateBy = EzshipHelper.GetUserName();
                        det.UpdateDateTime = DateTime.Now;

                        Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det);

                    }
                }
                #endregion

                #region PutAway
                XmlNodeList nodeListCont1 = elem.SelectNodes("PutAway");
                foreach (XmlNode xnMkg in nodeListCont1)
                {
                    XmlElement elemPutAway = (XmlElement)xnMkg;
                    if (elemPutAway.Name == "PutAway")
                    {
                        WhDoDet2 det = new WhDoDet2();
                        det.DoNo = doNo;
                        det.DoType = doType;

                        det.LotNo = SafeValue.SafeString(elemPutAway.GetAttribute("LotNo"));
                        det.Product = SafeValue.SafeString(elemPutAway.GetAttribute("Product"));
                        det.Des1 = SafeValue.SafeString(elemPutAway.GetAttribute("Des"));
                        det.BatchNo = SafeValue.SafeString(elemPutAway.GetAttribute("BatchNo"));
                        det.CustomsLot = SafeValue.SafeString(elemPutAway.GetAttribute("CustomsLot"));
                        det.Location = SafeValue.SafeString(elemPutAway.GetAttribute("Location"));
                        det.ProcessStatus = SafeValue.SafeString(elemPutAway.GetAttribute("ProcessStatus"));
                        det.Qty1 = SafeValue.SafeInt(elemPutAway.GetAttribute("PackQty"), 0);
                        det.Qty2 = SafeValue.SafeInt(elemPutAway.GetAttribute("WholeQty"), 0);
                        det.Qty3 = SafeValue.SafeInt(elemPutAway.GetAttribute("LooseQty"), 0);
                        det.QtyPackWhole = SafeValue.SafeInt(elemPutAway.GetAttribute("QtyPackWhole"), 0);
                        det.QtyWholeLoose = SafeValue.SafeInt(elemPutAway.GetAttribute("QtyWholeLoose"), 0);
                        det.QtyLooseBase = SafeValue.SafeInt(elemPutAway.GetAttribute("QtyLooseBase"), 0);
                        det.Uom1 = SafeValue.SafeString(elemPutAway.GetAttribute("PackUom"));
                        det.Uom2 = SafeValue.SafeString(elemPutAway.GetAttribute("WholeUom"));
                        det.Uom3 = SafeValue.SafeString(elemPutAway.GetAttribute("LooseUom"));
                        det.Uom4 = SafeValue.SafeString(elemPutAway.GetAttribute("BaseUom"));
                        det.Att1 = SafeValue.SafeString(elemPutAway.GetAttribute("Att1"));
                        det.Att2 = SafeValue.SafeString(elemPutAway.GetAttribute("Att2"));
                        det.Att3 = SafeValue.SafeString(elemPutAway.GetAttribute("Att3"));
                        det.Att4 = SafeValue.SafeString(elemPutAway.GetAttribute("Att4"));
                        det.Att5 = SafeValue.SafeString(elemPutAway.GetAttribute("Att5"));
                        det.Att6 = SafeValue.SafeString(elemPutAway.GetAttribute("Att6"));
                        det.Att7 = SafeValue.SafeString(elemPutAway.GetAttribute("Att7"));
                        det.Att8 = SafeValue.SafeString(elemPutAway.GetAttribute("Att8"));
                        det.Att9 = SafeValue.SafeString(elemPutAway.GetAttribute("Att9"));
                        det.Att10 = SafeValue.SafeString(elemPutAway.GetAttribute("Att10"));
                        det.CreateBy = EzshipHelper.GetUserName();
                        det.CreateDateTime = DateTime.Now;
                        det.UpdateBy = EzshipHelper.GetUserName();
                        det.UpdateDateTime = DateTime.Now;

                        Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det);
                    }
                }
                #endregion

                #region Container
                XmlNodeList nodeListCont2 = elem.SelectNodes("Container");
                foreach (XmlNode xnMkg in nodeListCont2)
                {
                    XmlElement elemContainer = (XmlElement)xnMkg;
                    if (elemContainer.Name == "Container")
                    {
                        WhDoDet3 det = new WhDoDet3();
                        det.DoNo = doNo;
                        det.DoType = doType;

                        det.ContainerNo = SafeValue.SafeString(elemContainer.GetAttribute("ContainerNo"));
                        det.ContainerType = SafeValue.SafeString(elemContainer.GetAttribute("ContainerType"));
                        det.SealNo = SafeValue.SafeString(elemContainer.GetAttribute("SealNo"));
                        det.Weight = SafeValue.SafeDecimal(elemContainer.GetAttribute("Weight"));
                        det.M3 = SafeValue.SafeDecimal(elemContainer.GetAttribute("M3"));
                        det.Qty = SafeValue.SafeInt(elemContainer.GetAttribute("Qty"), 0);
                        det.PkgType = SafeValue.SafeString(elemContainer.GetAttribute("PkgType"));
                        det.CreateBy = EzshipHelper.GetUserName();
                        det.CreateDateTime = DateTime.Now;
                        det.UpdateBy = EzshipHelper.GetUserName();
                        det.UpdateDateTime = DateTime.Now;
                        Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det);
                    }

                }
                #endregion

            }
        }
        return "Type=" + doType + " , " + "No=" + doNo;
    }

}