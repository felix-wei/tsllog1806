using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using C2;

/// <summary>
/// Summary description for Edi_Acc
/// </summary>
public class Edi_Acc
{
	public Edi_Acc()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #region export to xml
    public static XmlDocument ExportArToXml(string docNo)
    {
        string sql_mast = string.Format(@"select *,(select HblNo from dbo.fun_PrintInvoice_GetPart(XaArInvoice.MastRefNo,XAArInvoice.JobRefNo,XaArInvoice.MastType)) HblNo from  [dbo].[XAArInvoice]  WHERE (DocNo = '{0}')", docNo);
        DataTable mast = ConnectSql.GetDataSet(sql_mast).Tables[0];

        if (mast.Rows.Count == 0 && docNo.Length == 0)
        {
            return null;
        }
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"GB2312\"?><Invoices></Invoices>");
        XmlNode xmlNode = xmlDoc.SelectSingleNode("Invoices");

        XmlElement bill = null;
        for (int i = 0; i < mast.Rows.Count; i++)
        {
            bill = xmlDoc.CreateElement("Invoice");
            bill.SetAttribute("DocNo", mast.Rows[i]["DocNo"].ToString());
            bill.SetAttribute("DocType", mast.Rows[i]["DocType"].ToString());
            bill.SetAttribute("DocDate", SafeValue.SafeDate(mast.Rows[i]["DocDate"], DateTime.Today).ToString("yyyy-MM-dd"));
            string partyId = mast.Rows[i]["PartyTo"].ToString();
            string partyName = SafeValue.SafeString(EzshipHelper.GetPartyName(partyId));
            bill.SetAttribute("PartyTo", partyName);
            bill.SetAttribute("PartyFrom", System.Configuration.ConfigurationManager.AppSettings["CompanyName"]);
            bill.SetAttribute("Currency", mast.Rows[i]["CurrencyId"].ToString());
            bill.SetAttribute("ExRate", SafeValue.SafeString(mast.Rows[i]["ExRate"], "0.000"));
            bill.SetAttribute("DocAmt", SafeValue.SafeString(mast.Rows[i]["DocAmt"], "0.000"));
            bill.SetAttribute("LocAmt", SafeValue.SafeString(mast.Rows[i]["LocAmt"], "0.000"));
            bill.SetAttribute("Description", mast.Rows[i]["Description"].ToString());
            //bill.SetAttribute("UserName", mast.Rows[i]["UserId"].ToString());

            string refNo = mast.Rows[i]["MastRefNo"].ToString();
            string mastType = mast.Rows[i]["MastType"].ToString();
            string jobNo = mast.Rows[i]["JobRefNo"].ToString();
            bill.SetAttribute("HblNo", mast.Rows[i]["HblNo"].ToString());




            docNo = mast.Rows[i]["DocNo"].ToString();

            string sql_det = string.Format(@"SELECT ChgDes1, Qty, Price, Unit, Currency, ExRate,Gst, GstAmt, DocAmt, LocAmt,LineLocAmt,ChgCode
FROM XAArInvoiceDet WHERE (DocNo = '{0}') ORDER BY DocLineNo", docNo);
            DataTable tab_det = ConnectSql.GetDataSet(sql_det).Tables[0];
            for (int j = 0; j < tab_det.Rows.Count; j++)
            {
                XmlElement item = xmlDoc.CreateElement("Item");
                string qty = tab_det.Rows[j]["Qty"].ToString();
                string price = tab_det.Rows[j]["Price"].ToString();
                string unit = tab_det.Rows[j]["Unit"].ToString();
                string lineCurrency = tab_det.Rows[j]["Currency"].ToString();
                string lineExRate = tab_det.Rows[j]["ExRate"].ToString();
                decimal lineGst = SafeValue.SafeDecimal(tab_det.Rows[j]["Gst"], 0);
                decimal lineAmt = SafeValue.SafeDecimal(tab_det.Rows[j]["LocAmt"], 0);
                decimal lineGstAmt = SafeValue.SafeDecimal(tab_det.Rows[j]["GstAmt"], 0);
                decimal lineLocAmt = SafeValue.SafeDecimal(tab_det.Rows[j]["LocAmt"], 0);
                decimal lineTotLocAmt = SafeValue.SafeDecimal(tab_det.Rows[j]["LineLocAmt"], 0);
                string lineDes = tab_det.Rows[j]["ChgDes1"].ToString();
                item.SetAttribute("Qty", qty);
                item.SetAttribute("Price", price);
                item.SetAttribute("Unit", unit);
                item.SetAttribute("Currency", lineCurrency);
                item.SetAttribute("ExRate", lineExRate);
                item.SetAttribute("Gst", lineGst.ToString("0.000"));
                item.SetAttribute("GstAmt", lineGstAmt.ToString("0.000"));
                item.SetAttribute("Amt", lineAmt.ToString("0.000"));
                item.SetAttribute("LineAmt", lineLocAmt.ToString("0.000"));
                item.SetAttribute("LocAmt", lineTotLocAmt.ToString("0.000"));
                item.SetAttribute("ChargeCode", tab_det.Rows[j]["ChgCode"].ToString());
                item.InnerText = lineDes;
                bill.AppendChild(item);
            }
            xmlNode.AppendChild(bill);
        }
        return xmlDoc;

    }
    
    #endregion

    #region EDI
    public static string EdiFile(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string docNo = "";
        string docType = "";
        string partyId = "";
        if (true)
        {
            xmlDoc.Load(filePath);

            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Invoices").ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement elem = (XmlElement)xn;
                C2.XAApPayable ap = null;
                string refType = "";
                string runType = "AP-PAYABLE";
                if (ap == null)
                {
                    ap = new C2.XAApPayable();
                    docNo = C2Setup.GetNextNo(refType, runType, DateTime.Now);
                    docType = elem.GetAttribute("DocType");
                    if (docType == "CN")
                        docType = "SC";
                    else if (docType == "DN")
                        docType = "SD";
                    else
                        docType = "PL";
                    string des = "";
                    string supplierBillNo = elem.GetAttribute("DocNo");
                    string hblNo = elem.GetAttribute("HblNo");
                    string term = elem.GetAttribute("Term");
                    if (term.Length == 0)
                        term = "CASH";
                    string currency = elem.GetAttribute("Currency");
                    DateTime supplierBillDate = SafeValue.SafeDate(elem.GetAttribute("DocDate"), DateTime.Today);
                    decimal exRate = SafeValue.SafeDecimal(elem.GetAttribute("ExRate"), 0);
                    decimal docAmt = SafeValue.SafeDecimal(elem.GetAttribute("DocAmt"), 0);
                    decimal locAmt = SafeValue.SafeDecimal(elem.GetAttribute("LocAmt"), 0);

                    ap.DocNo = docNo;
                    ap.DocType = docType;
                    ap.DocDate = DateTime.Today;
                    ap.PartyTo = partyId;
                    ap.SupplierBillDate = supplierBillDate;
                    ap.SupplierBillNo = supplierBillNo;
                    ap.Term = term;
                    ap.ExRate = exRate;
                    ap.CurrencyId = currency;
                    string[] acPeriod = EzshipHelper.GetAccPeriod(DateTime.Today);
                    ap.AcYear = SafeValue.SafeInt(acPeriod[0], DateTime.Today.Year);
                    ap.AcPeriod = SafeValue.SafeInt(acPeriod[1], DateTime.Today.Month);
                    ap.AcCode = getAcCodeByPartyId(partyId, currency);
                    ap.AcSource = "CR";
                    if (docType == "SC")
                        ap.AcSource = "DB";
                    ap.Description = des;
                    ap.DocAmt = docAmt;
                    ap.LocAmt = locAmt;
                    ap.BalanceAmt = docAmt;
                    string[] arr = getRefNoByHbl(hblNo);
                    ap.MastRefNo = arr[0];
                    ap.JobRefNo = arr[1];
                    ap.MastType = arr[2];
                    ap.CancelDate = new DateTime(1900, 1, 1);
                    ap.CancelInd = "N";
                    ap.ChqDate = new DateTime(1900, 1, 1);
                    ap.ChqNo = "";
                    ap.ExportInd = "N";
                    ap.UserId = EzshipHelper.GetUserName();
                    ap.EntryDate = DateTime.Now;

                    Manager.ORManager.StartTracking(ap, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(ap);
                    C2Setup.SetNextNo(refType, runType, ap.DocNo, ap.DocDate);

                    XmlNodeList nodeListJob = elem.ChildNodes;
                    int i = 1;
                    foreach (XmlNode xnJob in nodeListJob)
                    {
                        XmlElement elemDet = (XmlElement)xnJob;
                        string chgCode = elemDet.GetAttribute("ChargeCode");
                        string des1 = elem.InnerText;
                        string currency_det = elem.GetAttribute("Currency");
                        decimal exRate_det = SafeValue.SafeDecimal(elem.GetAttribute("ExRate"), 0);
                        decimal qty = SafeValue.SafeDecimal(elem.GetAttribute("Qty"), 0);
                        decimal price = SafeValue.SafeDecimal(elem.GetAttribute("Price"), 0);
                        string unit = elem.GetAttribute("Unit").Trim();
                        decimal gst = SafeValue.SafeDecimal(elem.GetAttribute("Gst"), 0);
                        decimal gstAmt = SafeValue.SafeDecimal(elem.GetAttribute("GstAmt"), 0);
                        decimal docAmt_det = SafeValue.SafeDecimal(elem.GetAttribute("Amt"), 0);
                        decimal locAmt_det = SafeValue.SafeDecimal(elem.GetAttribute("LocAmt"), 0);
                        decimal lineLocAmt_det = SafeValue.SafeDecimal(elem.GetAttribute("LineAmt"), 0);


                        C2.XAApPayableDet det = new XAApPayableDet();
                        det.AcCode = getAcCodeByChgCode(chgCode);
                        det.AcSource = "DB";
                        if (ap.AcSource == "DB")
                            det.AcSource = "CR";
                        det.ChgCode = chgCode;
                        det.ChgDes1 = des1;
                        det.ChgDes2 = " ";
                        det.ChgDes3 = " ";
                        det.ChgDes4 = " ";
                        det.Currency = currency_det;
                        det.DocAmt = docAmt_det;
                        det.DocId = ap.SequenceId;
                        det.DocLineNo = i;
                        det.DocNo = ap.DocNo;
                        det.DocType = ap.DocType;
                        det.ExRate = exRate_det;
                        det.Gst = gst;
                        det.GstAmt = gstAmt;
                        det.GstType = "Z";
                        if (det.Gst > 0)
                            det.GstType = "S";
                        det.JobRefNo = ap.JobRefNo;
                        det.LineLocAmt = lineLocAmt_det;
                        det.LocAmt = locAmt_det;
                        det.MastRefNo = ap.MastRefNo;
                        det.MastType = ap.MastType;
                        det.SplitType = "WtM3";
                        det.Price = price;
                        det.Qty = qty;
                        det.SplitType = "";
                        det.Unit = unit;
                        if (det.Unit.Length == 0)
                            det.Unit = " ";

                        Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det);
                    }
                }
            }
        }
        return "DocType=" + docType + " , " + "DocNo=" + docNo;
    }
    private static string getAcCodeByPartyId(string partyId, string currency)
    {
        string apCode = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
        string sql = string.Format("select ApCode From XXPartyAcc where ISNULL(Partyid,'')='{0}' and CurrencyId='{1}'", partyId, currency);
        apCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (apCode.Length == 0)
        {
            sql = string.Format("select ApCode From XXPartyAcc where isnull(Partyid,'')='' and CurrencyId='{0}'", currency);
            apCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        }
        return apCode;
    }
    private static string[] getRefNoByHbl(string hblNo)
    {
        string refNo = "";
        string jobNo = "";
        string mastType = "";
        string sql = string.Format(@"select RefNo,JobNo,'SI' from SeaImport where HblNo='{0}'
union select RefNo,JobNo,'SE' from SeaExport where HblNo='{0}'
union select RefNo,JobNo,RefType from air_job where Hawb='{0}'", hblNo);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
        {
            refNo = SafeValue.SafeString(tab.Rows[0][0]);
            jobNo = SafeValue.SafeString(tab.Rows[0][1]);
            mastType = SafeValue.SafeString(tab.Rows[0][2]);
        }
        return new string[] { refNo, jobNo, mastType };
    }
    private static string getAcCodeByChgCode(string chgCode)
    {
        string apCode = "";
        string sql = string.Format("select ApCode from xxchgcode where ChgCodeId='{0}' ", chgCode);
        apCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (apCode.Length == 0)
        {
            apCode = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
        }
        return apCode;
    }
    #endregion
}