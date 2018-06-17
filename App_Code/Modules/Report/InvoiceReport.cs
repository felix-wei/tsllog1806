using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using C2;
using Wilson.ORMapper;

/// <summary>
/// InvoiceReport 的摘要说明
/// </summary>
public class InvoiceReport
{
    private static DataTable InitMastDataTable()
    {
        DataTable tab = new DataTable("Mast");
        tab.Columns.Add("BillType");
        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("Contact");
        tab.Columns.Add("Terms");
        tab.Columns.Add("DocDueDate");
        tab.Columns.Add("SupplierBillNo");
        tab.Columns.Add("CustRefNo");
        tab.Columns.Add("SupplierBillDate");
        tab.Columns.Add("Voyage");
        tab.Columns.Add("Vessel");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Etd");
        tab.Columns.Add("PartyTo");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("PartyAdd");
        tab.Columns.Add("AcCode");
        tab.Columns.Add("ChqNo");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("Currency");
        tab.Columns.Add("DocAmt");
        tab.Columns.Add("LocAmt");
        tab.Columns.Add("Remark");
        tab.Columns.Add("SubGstTotal");
        tab.Columns.Add("SubTotal");
        tab.Columns.Add("TotalAmt");
        tab.Columns.Add("GstTotal");
        tab.Columns.Add("Total");
        tab.Columns.Add("JobNo");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Pol");
        tab.Columns.Add("Pod");
        tab.Columns.Add("HblNo");
        tab.Columns.Add("CrBkgNo");
        tab.Columns.Add("MoneyWords");

        tab.Columns.Add("QuoteNo");
        tab.Columns.Add("QuoteStatus");
        tab.Columns.Add("QuoteDate");
        tab.Columns.Add("QuoteRemark");
        tab.Columns.Add("JobDes");
        tab.Columns.Add("TerminalRemark");

        tab.Columns.Add("CrNo");
        tab.Columns.Add("ZipCode");
        tab.Columns.Add("Contact1");
        tab.Columns.Add("Tel1");
        tab.Columns.Add("Fax1");
        tab.Columns.Add("Email1");
        tab.Columns.Add("ContainerNo");
        tab.Columns.Add("CompanyName");

        tab.Columns.Add("JobDate");
        tab.Columns.Add("PickupFrom");
        tab.Columns.Add("DeliveryTo");

        tab.Columns.Add("NonGstAmt");
        tab.Columns.Add("Gst");
        tab.Columns.Add("AmtStr");

        tab.Columns.Add("DoNo");

        return tab;
    }
    private static DataTable InitDeliveryDataTable()
    {
        DataTable tab = new DataTable("Do");
        tab.Columns.Add("No");
        tab.Columns.Add("VesVoy");
        tab.Columns.Add("ClientContact");
        tab.Columns.Add("ClientRefNo");
        tab.Columns.Add("ScheduleDate");
        tab.Columns.Add("TripIndex");
        tab.Columns.Add("FromCode");
        tab.Columns.Add("JobNo");
        tab.Columns.Add("ToCode");
        tab.Columns.Add("TowheadCode");
        tab.Columns.Add("ContNo");
        tab.Columns.Add("LineAmt");
        tab.Columns.Add("Unit");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("LocAmt");
        tab.Columns.Add("GstAmt");
        tab.Columns.Add("Gst");
        tab.Columns.Add("Rmk");

        return tab;
    }
    private static DataTable InitDetailDataTable()
    {
        DataTable tab = new DataTable("Det");
        tab.Columns.Add("No");
        tab.Columns.Add("VesVoy");
        tab.Columns.Add("DocNo");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("ChgDes1");
        tab.Columns.Add("ChgDes2");
        tab.Columns.Add("ChgDes3");
        tab.Columns.Add("ChgDes4");
        tab.Columns.Add("OtherChgDes");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Price");
        tab.Columns.Add("Amt");
        tab.Columns.Add("LineAmt");
        tab.Columns.Add("Unit");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("LocAmt");
        tab.Columns.Add("GstAmt");
        tab.Columns.Add("Gst");
        tab.Columns.Add("Rmk");
        tab.Columns.Add("Cur");
        return tab;
    }
    private static DataTable InitDetailDataTable1()
    {
        DataTable tab = new DataTable("Detail");
        tab.Columns.Add("No");
        tab.Columns.Add("DocNo");
        tab.Columns.Add("VesVoy");
        tab.Columns.Add("ChgDes1");
        tab.Columns.Add("OtherChgDes");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Price");
        tab.Columns.Add("Amt");
        tab.Columns.Add("LineAmt");
        tab.Columns.Add("Unit");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("LocAmt");
        tab.Columns.Add("GstAmt");
        tab.Columns.Add("Gst");
        tab.Columns.Add("Rmk");

        return tab;
    }
    public static DataSet DsQuotation(string orderNo, string docType)
    {
        DataSet set = new DataSet();
        #region Quotation
        DataTable mast = InitMastDataTable();
        DataRow row = mast.NewRow();


        Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "QuoteNo='" + orderNo + "'");

        C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
        if (ctmJob != null)
        {
            row["Vessel"] = ctmJob.Vessel;
            row["Voyage"] = ctmJob.Voyage;
            row["Eta"] = ctmJob.EtaDate.ToString("dd/MM/yy");
            row["CustRefNo"] = ctmJob.ClientRefNo;
            row["Pol"] = ctmJob.Pol;
            row["Pod"] = ctmJob.Pod;
            row["CrBkgNo"] = ctmJob.CarrierBkgNo;
            row["HblNo"] = ctmJob.CarrierBlNo;
            row["QuoteNo"] = ctmJob.QuoteNo;
            row["QuoteDate"] = ctmJob.QuoteDate.ToString("dd/MM/yy");
            row["JobNo"] = ctmJob.JobNo;
            row["BillType"] = "QUOTATION";
            row["UserId"] = HttpContext.Current.User.Identity.Name;
            row["PartyName"] = EzshipHelper.GetPartyName(ctmJob.ClientId);
            string sql = string.Format(@"select Address,ZipCode,Tel1,Fax1,Email1 from XXParty where PartyId='{0}'", ctmJob.ClientId);
            DataTable tab = ConnectSql_mb.GetDataTable(sql);
            if (tab.Rows.Count > 0)
            {
                string address = tab.Rows[0]["Address"].ToString();
                string zipCode = tab.Rows[0]["ZipCode"].ToString();
                string tel = tab.Rows[0]["Tel1"].ToString();
                string email = tab.Rows[0]["Email1"].ToString();
                row["PartyAdd"] =address;
                if(zipCode.Length>0)
                   row["ZipCode"] = "SINGAPORE:" + zipCode;
            }
            sql = string.Format(@"select Email,Fax,Tel from ref_contact where PartyId='{0}' and Name='{1}'", ctmJob.ClientId, ctmJob.ClientContact);
            tab = ConnectSql_mb.GetDataTable(sql);
            if (tab.Rows.Count > 0)
            {
                string tel = tab.Rows[0]["Tel"].ToString();
                string email = tab.Rows[0]["Email"].ToString();
                string fax = tab.Rows[0]["Fax"].ToString();
                string str="";
                if (tel.Length > 0)
                    str += "T:" + tel;
                if (fax.Length > 0)
                    str += " F:" + fax;
                if (ctmJob.EmailAddress.Length > 0)
                    str=str+ "\n"+"E:" + ctmJob.EmailAddress;
                row["Tel1"] = str;
            }
            row["Contact1"] = ctmJob.ClientContact;
            row["Terms"] = ctmJob.IncoTerm;
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["MoneyWords"] = "";
            row["QuoteRemark"] = ctmJob.QuoteRemark;
            row["JobDes"] = ctmJob.JobDes;
            string terminalRemark="";
            if (ctmJob.TerminalRemark!=null)
            {
                if (ctmJob.TerminalRemark.Contains("<BR>"))
                    terminalRemark = ctmJob.TerminalRemark.Replace("<BR>", "\n");
                else
                {
                    terminalRemark = ctmJob.TerminalRemark;
                }
            }
            row["TerminalRemark"] = terminalRemark;
            row["PickupFrom"] = ctmJob.PickupFrom;
            row["DeliveryTo"] = ctmJob.DeliveryTo;
            row["JobDate"] = R.Date(ctmJob.JobDate);
            decimal subTotal = 0;
            decimal gstTotal = 0;


            DataTable det = InitDetailDataTable();
            sql = string.Format(@"select count(*) from job_rate where JobNo='{0}' and len(LumsumInd)>0", orderNo);
            int count = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
            string sql_n = "";
            string sql_t = "";
            int n = 0;
            if (count > 0)
            {
                //sql = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price,Qty,Unit from job_rate where JobNo='{0}' and LumsumInd='YES' and ChgCode='LUMPSUM'", orderNo);
                //sql_n = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price,Qty,Unit from job_rate where JobNo='{0}' and LumsumInd='YES' and ChgCode<>'LUMPSUM'", orderNo);
                //sql_t = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price,Qty,Unit from job_rate where JobNo='{0}' and (LumsumInd='NO' or LumsumInd is null)", orderNo);
                sql = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price,Qty,Unit,LumsumInd from job_rate where JobNo='{0}' and len(LumsumInd)>0 and (ChgCode='LUMPSUM' OR ChgCode='PACKAGE')  order by  LumsumInd,LineIndex asc", orderNo);
                sql_t = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price,Qty,Unit,LumsumInd,LineIndex from job_rate where JobNo='{0}' and (len(LumsumInd)=0 or LumsumInd is null) order by LineIndex asc", orderNo);
            }
            else {
                sql = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price,Qty,Unit,LumsumInd from job_rate where JobNo='{0}' order by LineIndex asc", orderNo);

            }
            decimal gst = 0;
            DataTable dt = ConnectSql.GetTab(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                #region LUMPSUM / PACKAG
                DataRow row1 = det.NewRow();
                string chgCode = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
                string lumsumInd = SafeValue.SafeString(dt.Rows[i]["LumsumInd"]);
                row1["No"] = n + 1;
                
                row1["ChgDes"] = chgCodeDes;
                sql = string.Format(@"select ChgCodeDes from XXChgCode where chgCodeId='{0}'", chgCode);
                row1["ChgDes1"] = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
                if (chgCode == "LUMPSUM")
                {
                    row1["ChgDes"] = chgCodeDes;
                    row1["ChgDes1"] = "";
					
                }
				decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt.Rows[i]["Qty"]) * SafeValue.SafeDecimal(dt.Rows[i]["Price"], 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * gst), 2);
                row1["ChgCode"] = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                row1["Rmk"] = SafeValue.SafeString(dt.Rows[i]["Remark"]);
                if (chgCode!="PACKAGE"){
                    row1["Qty"] = string.Format("{0:#,##0.0}", SafeValue.SafeDecimal(dt.Rows[i]["Qty"]));
                    row1["Unit"] = SafeValue.SafeString(dt.Rows[i]["Unit"]);
                    row1["Price"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt.Rows[i]["Price"]), 2);
                    row1["Gst"] = SafeValue.SafeInt(gst * 100, 0) + "% ";//+ gstType + "R"
                    row1["GstAmt"] = gstAmt;
                    row1["LocAmt"] = amt + gstAmt;
                    row1["LineAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt.Rows[i]["Qty"]) * SafeValue.SafeDecimal(dt.Rows[i]["Price"]), 2);
                }
                string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeId='{0}'", chgCode);
                DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);

                string gstType = "";
                string chgTypeId = "";
                if (dt_chgCode.Rows.Count > 0)
                {
                    gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                    gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                    chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                }


                subTotal += amt;
                gstTotal += gstAmt;
                det.Rows.Add(row1);
                #endregion
                n++;
                string chgDes = "";
                sql_n = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price,Qty,Unit from job_rate where JobNo='{0}' and len(LumsumInd)>0 and (ChgCode<>'LUMPSUM' and ChgCode<>'PACKAGE')  and  LumsumInd='{1}' order by  LumsumInd,LineIndex asc", orderNo,lumsumInd);

                if (sql_n.Length > 0)
                {
                    sql_n += string.Format(@"");
                    DataTable det_n = ConnectSql.GetTab(sql_n);
                    for (int j = 0; j < det_n.Rows.Count; j++)
                    {
                        DataRow row_1 = det.NewRow();
                        sql = string.Format(@"select ChgCodeDes from XXChgCode where chgCodeId='{0}'", det_n.Rows[j]["ChgCode"]);
                        //row_1["ChgDes"] =SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
                        row_1["ChgDes"] = SafeValue.SafeString(det_n.Rows[j]["ChgCodeDes"]);
                        chgDes = "- " + SafeValue.SafeString(det_n.Rows[j]["ChgCodeDes"]);
                        row_1["ChgDes1"] = chgDes;
                        row_1["Qty"] = string.Format("{0:#,##0.0}",SafeValue.SafeDecimal(det_n.Rows[j]["Qty"]));
                        row_1["Unit"] = SafeValue.SafeString(det_n.Rows[j]["Unit"]);
                        decimal amt_1=0;
						decimal gstAmt_1=0;
						if (chgCode == "PACKAGE")
                        {
						   row_1["Price"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(det_n.Rows[j]["Price"]),2);
                           row_1["Gst"] = SafeValue.SafeInt(gst * 100, 0) + "% ";//+ gstType + "R"
                           amt_1 = SafeValue.ChinaRound(SafeValue.SafeDecimal(det_n.Rows[j]["Qty"]) * SafeValue.SafeDecimal(det_n.Rows[j]["Price"], 0), 2);
                           gstAmt_1 = SafeValue.ChinaRound((amt_1 * gst), 2);
						   row_1["LineAmt"] =amt_1;
                           row_1["GstAmt"] = gstAmt_1;
				        }
                        det.Rows.Add(row_1);
						subTotal += amt_1;
                        gstTotal += gstAmt_1;
                    }
                }
            }

            if (sql_t.Length > 0)
            {
                dt = ConnectSql.GetTab(sql_t);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    #region No LUMPSUM
                    DataRow row1 = det.NewRow();
                    string chgCode = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                    string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
                    row1["No"] = n + 1;
                    sql = string.Format(@"select ChgCodeDes from XXChgCode where chgCodeId='{0}'", chgCode);
                    row1["ChgDes"] = chgCodeDes;//SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
                    row1["ChgDes1"] = chgCodeDes;
                    row1["ChgCode"] = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                    row1["Qty"] = string.Format("{0:#,##0.0}",SafeValue.SafeDecimal(dt.Rows[i]["Qty"]));
                    row1["Rmk"] = SafeValue.SafeString(dt.Rows[i]["Remark"]);
                    row1["Unit"] = SafeValue.SafeString(dt.Rows[i]["Unit"]);
                    row1["LineAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt.Rows[i]["Qty"]) * SafeValue.SafeDecimal(dt.Rows[i]["Price"]), 2);
                    string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeId='{0}'", chgCode);
                    DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);

                    string gstType = "";
                    string chgTypeId = "";
                    if (dt_chgCode.Rows.Count > 0)
                    {
                        gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                        gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                        chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                    }
                    row1["Price"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt.Rows[i]["Price"]), 2);
                    row1["Gst"] = SafeValue.SafeInt(gst * 100, 0) + "% ";//+ gstType + "R"
                    decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt.Rows[i]["Qty"]) * SafeValue.SafeDecimal(dt.Rows[i]["Price"], 0), 2);
                    decimal gstAmt = SafeValue.ChinaRound((amt * gst), 2);
                    row1["Amt"] = amt;
                    row1["GstAmt"] = gstAmt;
                    row1["LocAmt"] = amt + gstAmt;

                    subTotal += amt;
                    gstTotal += gstAmt;
                    det.Rows.Add(row1);
                    #endregion
                    n++;
                }
            }
            gst = SafeValue.SafeDecimal(0.07);
            gstTotal = SafeValue.ChinaRound((subTotal * gst), 2);
            row["SubTotal"] = subTotal;
            row["GstTotal"] = gstTotal;
            row["TotalAmt"] = subTotal + gstTotal;
            mast.Rows.Add(row);

     

            set.Tables.Add(mast);
            set.Tables.Add(det);
        }
        #endregion
        return set;
    }
    public static DataSet DsImpTs(string orderNo, string docType)
    {
        DataSet set = new DataSet();
        if (docType == "IV" || docType == "IV1" || docType == "DN" || docType == "CN")
        {
            ObjectQuery query = new ObjectQuery(typeof(C2.XAArInvoice), "DocNo='" + orderNo + "'", "");
            ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
            C2.XAArInvoice obj = objSet[0] as C2.XAArInvoice;
            #region Invoice

            DataTable mast = InitMastDataTable();

            DataTable delivery = InitDeliveryDataTable();
            DataTable details = InitDetailDataTable();
            DataRow row = mast.NewRow();
            if (obj != null)
            {

                string mastRefNo = obj.MastRefNo;
                string mastType = obj.MastType;

                Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + mastRefNo + "'");

                C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
                if (ctmJob != null)
                {
                    #region Job
                    row["Vessel"] = ctmJob.Vessel;
                    row["Voyage"] = ctmJob.Voyage;
                    row["Eta"] = ctmJob.EtaDate.ToString("dd.MM.yy");
                    row["CustRefNo"] = ctmJob.ClientRefNo;
                    row["Pol"] = ctmJob.Pol;
                    row["Pod"] = ctmJob.Pod;
                    row["CrBkgNo"] = ctmJob.CarrierBkgNo;
                    row["HblNo"] = ctmJob.CarrierBlNo;
                    row["JobDes"] = ctmJob.JobDes;
                    row["CustRefNo"] = ctmJob.ClientRefNo;
                    #endregion
                }
                row["JobNo"] = mastRefNo;
                row["BillType"] = "TAX INVOICE";
				if(docType == "DN")
					row["BillType"] = "TAX DEBIT NOTE";
				if(docType == "CN")
					row["BillType"] = "TAX CREDIT NOTE";
					
				if(S.Text(obj.ReviseInd).Trim() != "")
					row["BillType"] = S.Text(row["BillType"]) + " ("+obj.ReviseInd+")";
				
                row["DocNo"] = obj.DocNo;
                row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
                row["Contact"] = "Account Dept.";//"obj.Contact;
                row["DocDueDate"] = obj.DocDueDate.ToString("dd/MM/yyyy");
                row["UserId"] = HttpContext.Current.User.Identity.Name;
                row["PartyName"] = obj.PartyName;
                row["PartyTo"] = obj.PartyTo;
                string tel1 = "";
                string fax1 = "";
                string email1 = "";
                string sql = string.Format(@"select Address,Contact1,Fax1,Tel1,CrNo,City,ZipCode,Email1 from XXParty where PartyId='{0}'", obj.PartyTo);
                DataTable tab = ConnectSql_mb.GetDataTable(sql);
                if (tab.Rows.Count > 0)
                {
                    string zipCode = tab.Rows[0]["ZipCode"].ToString();
                    string city = tab.Rows[0]["City"].ToString();
                    if (zipCode.Length > 0)
                        zipCode = "\n" + city + " " + zipCode;
                    row["PartyAdd"] = tab.Rows[0]["Address"].ToString() + zipCode;
                    row["CrNo"] = tab.Rows[0]["CrNo"].ToString();
                    tel1 = tab.Rows[0]["Tel1"].ToString();
                    fax1 = tab.Rows[0]["Fax1"].ToString();
                    email1 = tab.Rows[0]["Email1"].ToString();
                    if (fax1.Length > 0)
                        row["Fax1"] = "Fax:" + fax1;
                    if (tel1.Length > 0)
                        row["Tel1"] = "Tel:" + tel1;
                    if (email1.Length > 0)
                        row["Email1"] = "Email:" + email1;
                }
                sql = string.Format(@"select Email,Fax,Tel from ref_contact where PartyId='{0}' and Name='{1}'", obj.PartyTo, obj.Contact);
                DataTable tab_p = ConnectSql_mb.GetDataTable(sql);
                if (tab_p.Rows.Count > 0)
                {
                    string tel = tab_p.Rows[0]["Tel"].ToString();
                    string fax = tab_p.Rows[0]["Fax"].ToString();
                    string email = tab_p.Rows[0]["Email"].ToString();
                    if(fax.Length>0&&fax1.Length==0)
                        row["Fax1"] = "Fax:" + fax;
                    if(tel.Length > 0 && tel1.Length == 0)
                        row["Tel1"] = "Tel:" + tel;
                    if(email.Length>0 && email1.Length == 0)
                        row["Email1"] = "Email:" + email;
                }
                row["Terms"] = obj.Term;
                row["Terms"] = obj.Term;
                row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                row["MoneyWords"] = "";
                //row["Remark"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select Description from XAArInvoice where MastRefNo='{0}'", mastRefNo)));
                row["Remark"] = obj.Description;

                sql = string.Format(@"select top 1 TripIndex from ctm_jobdet2 where JobNo='{0}'  order by Id asc", mastRefNo);
                row["DoNo"] = ConnectSql_mb.ExecuteScalar(sql);
                decimal subTotal = 0;
                decimal subGstTotal = 0;
                decimal subGstTotal2 = 0;

                ObjectQuery query1 = new ObjectQuery(typeof(C2.CtmJobDet1), "JobNo='" + mastRefNo + "'", "");
                ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);



                sql = string.Format(@"select top 1 BookingDate,TripIndex from ctm_jobdet2 where JobNo='{0}'  order by Id asc", mastRefNo);
                DataTable dt_det2 = ConnectSql_mb.GetDataTable(sql);
                //ObjectQuery query2 = new ObjectQuery(typeof(C2.CtmJobDet2), "JobNo='" + mastRefNo + "'  and JobType in ('WDO','CRA','WGR')", "");
                //ObjectSet objSet2 = C2.Manager.ORManager.GetObjectSet(query2);
                for (int i = 0; i < dt_det2.Rows.Count; i++)
                {
                    string contNo = "";
                    if (objSet1.Count > 0)
                    {
                        for (int j = 0; j < objSet1.Count; j++)
                        {
                            CtmJobDet1 det1 = objSet1[j] as CtmJobDet1;
                            if (objSet1.Count-j == 1)
                                contNo += det1.ContainerNo;
                            else
                                contNo += det1.ContainerNo + ",";
                        }
                    }
                    //CtmJobDet2 det2=objSet2[i] as CtmJobDet2;
                    DataRow row_det2 = dt_det2.Rows[i];
                    DataRow row1 = delivery.NewRow();
                    row1["VesVoy"] = ctmJob.Vessel + " / " + ctmJob.Voyage;
                    row1["TripIndex"] = row_det2["TripIndex"];
					row1["ClientRefNo"] = ctmJob.ClientRefNo;

                    row1["JobNo"] = mastRefNo;
                    row1["ScheduleDate"] = SafeValue.SafeDateStr(row_det2["BookingDate"]);
                    if (ctmJob != null)
                    {
                        row1["ClientContact"] = ctmJob.ClientContact;
                        row1["FromCode"] =SafeValue.SafeString(ctmJob.PickupFrom).TrimEnd();
                        row1["ToCode"] = SafeValue.SafeString(ctmJob.DeliveryTo).TrimEnd();
                    }
                    //row1["TowheadCode"] = det2.TowheadCode;
                    if (objSet1.Count > 0)
                    {
                        row1["ScheduleDate"] = string.Format("{0:dd/MM/yyyy}", (objSet1[0] as CtmJobDet1).ScheduleDate);
                    }
                    row1["ContNo"] = contNo;


                    delivery.Rows.Add(row1);

                }
				
				if(dt_det2.Rows.Count == 0)
                {
                     DataRow row1 = delivery.NewRow();
                    if (ctmJob != null)
                    {
						row1["VesVoy"] = ctmJob.Vessel + " / " + ctmJob.Voyage;
						//row1["TripIndex"] = row_det2["TripIndex"];
						row1["ClientRefNo"] = ctmJob.ClientRefNo;
						row1["JobNo"] = ctmJob.JobNo;
						//row1["ScheduleDate"] = SafeValue.SafeDateStr(row_det2["BookingDate"]);
                        row1["ClientContact"] = ctmJob.ClientContact;
                        row1["FromCode"] =SafeValue.SafeString(ctmJob.PickupFrom).TrimEnd();
                        row1["ToCode"] = SafeValue.SafeString(ctmJob.DeliveryTo).TrimEnd();
                    }
                    //row1["TowheadCode"] = det2.TowheadCode;
                    //row1["ContNo"] = contNo;


                    delivery.Rows.Add(row1);

                }

                sql = string.Format(@"select d.ChgCode,sum(d.Qty) as Qty,d.MastType,max(d.DocNo) as DocNo,d.ChgDes1,d.ChgDes4,max(d.ChgDes2) ChgDes2,max(d.ChgDes3) as ChgDes3, (d.Price) as Price, max(d.Gst) Gst,sum(d.GstAmt) GstAmt,max(d.Unit) Unit,
sum(d.LineLocAmt) LineLocAmt, max(d.Currency) Currency,max(d.ExRate) as ExRate,
sum(d.LocAmt) LocAmt,max(d.JobRefNo) as JobRefNo,d.GstType, c.SortIndex from XAArInvoiceDet d
left outer join xxchgcode c on d.chgcode=c.chgcodeid where DocId={0} group by d.ChgCode,c.SortIndex, d.ChgDes1,d.ChgDes2, d.Price, d.ChgDes4,d.MastType,d.GstType 
order by c.SortIndex", obj.SequenceId);
                DataTable det = ConnectSql.GetTab(sql);
                for (int i = 0; i < det.Rows.Count; i++)
                {
                    DataRow row1 = details.NewRow();
                    string cntType = SafeValue.SafeString(det.Rows[i]["JobRefNo"]);
                    string chgCode = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                    string gstType = SafeValue.SafeString(det.Rows[i]["GstType"]);
                    string billingClass = SafeValue.SafeString(det.Rows[i]["MastType"]);
                    string chgDes = SafeValue.SafeString(det.Rows[i]["ChgDes1"]);
                    row1["No"] = i + 1;
                    row1["DocNo"] = SafeValue.SafeString(det.Rows[i]["DocNo"]);
                    row1["ChgDes1"] = SafeValue.SafeString(det.Rows[i]["ChgDes1"]) + "\n" + SafeValue.SafeString(det.Rows[i]["ChgDes2"]);
                    row1["ChgDes4"] = SafeValue.SafeString(det.Rows[i]["ChgDes4"]);
                    row1["ChgCode"] = SafeValue.SafeString(det.Rows[i]["ChgCode"]);

                    row1["Unit"] = SafeValue.SafeString(det.Rows[i]["Unit"]);
                    row1["Currency"] = SafeValue.SafeString(det.Rows[i]["Currency"]);
                    row1["ExRate"] = SafeValue.SafeString(det.Rows[i]["ExRate"]);
                    row1["Price"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Price"]), 2);
                    row1["Gst"] = SafeValue.SafeInt(SafeValue.SafeDecimal(det.Rows[i]["Gst"]) * 100, 0) + "% " + det.Rows[i]["GstType"] + "R";
                    decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Qty"]) * SafeValue.SafeDecimal(det.Rows[i]["Price"], 0)* SafeValue.SafeDecimal(det.Rows[i]["ExRate"], 0), 2);
                    decimal gstAmt = SafeValue.SafeDecimal(det.Rows[i]["GstAmt"],0); //SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(det.Rows[i]["Gst"], 0)), 2);
					
                    row1["Amt"] = amt;
                    row1["LineAmt"] = SafeValue.SafeString(amt);
                    row1["GstAmt"] = SafeValue.SafeString(det.Rows[i]["GstAmt"]);
                    row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);
                    row1["Qty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Qty"]), 2);
                    if (gstType == "S")
                    {
                        row1["Cur"] = "(S)";
                        subGstTotal += amt;
                        subGstTotal2 += gstAmt;
                    }

                    details.Rows.Add(row1);


                    subTotal += amt;

                }
                sql = string.Format(@"select ContainerNo,SealNo,ContainerType from CTM_JobDet1 where JobNo='{0}' ", mastRefNo);
                DataTable cnt = ConnectSql.GetTab(sql);
                for (int i = 0; i < cnt.Rows.Count; i++)
                {

                    if (cnt.Rows.Count - i > 1)
                    {
                        row["ContainerNo"] += SafeValue.SafeString(cnt.Rows[i]["ContainerNo"]) + " / " + SafeValue.SafeString(cnt.Rows[i]["SealNo"])
                            + " / " + SafeValue.SafeString(cnt.Rows[i]["ContainerType"]) + "\n";
                    }
                    else
                    {
                        row["ContainerNo"] += SafeValue.SafeString(cnt.Rows[i]["ContainerNo"]) + " / " + SafeValue.SafeString(cnt.Rows[i]["SealNo"])
                            + " / " + SafeValue.SafeString(cnt.Rows[i]["ContainerType"]);
                    }

                }
                decimal gstTotal = subGstTotal2;//SafeValue.ChinaRound(subGstTotal * SafeValue.SafeDecimal(0.07), 2);
                decimal nonGstAmt = 0;
                row["SubTotal"] = subTotal;
                row["SubGstTotal"] = subGstTotal;
                row["GstTotal"] = gstTotal;
                row["TotalAmt"] = subTotal + gstTotal;
                if (nonGstAmt > 0)
                    row["NonGstAmt"] = SafeValue.SafeDecimal(nonGstAmt);
                else
                    row["NonGstAmt"] = "";
                decimal totalAmt = subTotal + gstTotal;
                string str = "";
                if (totalAmt > 0)
                    str = Helper.NumberToEnglish.NumberToString(SafeValue.SafeDouble(totalAmt));
                row["AmtStr"] = str;
                mast.Rows.Add(row);

            }
            else {
                row["DocNo"] = "0";
                mast.Rows.Add(row);

                DataRow row1 = delivery.NewRow();
                row1["TripIndex"] = "";
                delivery.Rows.Add(row1);

                DataRow row2 = details.NewRow();
                row2["No"] = 0;
                details.Rows.Add(row2);
            }
            set.Tables.Add(mast);
            set.Tables.Add(delivery);
            set.Tables.Add(details);


            #endregion
        }
        if (docType == "PL" || docType == "PL1")
        {
            ObjectQuery query = new ObjectQuery(typeof(C2.XAApPayable), "DocNo='" + orderNo + "'", "");
            ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
            C2.XAApPayable obj = objSet[0] as C2.XAApPayable;
            #region PL
            DataTable mast = InitMastDataTable();
            DataRow row = mast.NewRow();
            string mastRefNo = obj.MastRefNo;
            string mastType = obj.MastType;
            if (mastType == "CTM")
            {

                Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + mastRefNo + "'");

                C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
                row["Vessel"] = ctmJob.Vessel;
                row["Voyage"] = ctmJob.Voyage;
                row["Eta"] = ctmJob.EtaDate.ToString("dd.MM.yy");
            }
            row["DocNo"] = obj.DocNo;
            row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
            row["SupplierBillNo"] = obj.SupplierBillNo;
            row["SupplierBillDate"] = obj.SupplierBillDate.ToString("dd/MM/yyyy");

            row["PartyName"] = obj.PartyName;
            string sql = string.Format(@"select Address from XXParty where PartyId='{0}'", obj.PartyTo);
            row["PartyAdd"] = C2.Manager.ORManager.ExecuteScalar(sql);
            row["Terms"] = obj.Term;

            decimal subTotal = 0;
            decimal gstTotal = 0;

            DataTable delivery = InitDeliveryDataTable();

            DataTable details = InitDetailDataTable();
            sql = string.Format(@"select ChgCode,sum(Qty) as Qty,MastType,max(DocNo) DocNo,max(ChgDes1) as ChgDes1,Price,max(Gst) Gst,max(GstAmt) GstAmt,
max(LocAmt) LocAmt,max(JobRefNo) JobRefNo,max(GstType) GstType from XAApPayableDet where DocId={0} group by ChgCode,MastType,Price order by Gst desc", obj.SequenceId);
            DataTable det = ConnectSql.GetTab(sql);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow row1 = details.NewRow();
                string cntType = SafeValue.SafeString(det.Rows[i]["JobRefNo"]);
                string chgCode = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["DocNo"] = SafeValue.SafeString(det.Rows[i]["DocNo"]);
                row1["ChgDes1"] = SafeValue.SafeString(det.Rows[i]["ChgDes1"]);
                row1["ChgCode"] = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["Price"] = string.Format("{0:#,##0.00}", SafeValue.SafeString(det.Rows[i]["Price"]));
                row1["Gst"] = SafeValue.SafeInt(SafeValue.SafeDecimal(det.Rows[i]["Gst"]) * 100, 0) + "% " + det.Rows[i]["GstType"] + "R";
                decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Qty"]) * SafeValue.SafeDecimal(det.Rows[i]["Price"], 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(det.Rows[i]["Gst"], 0)), 2);
                row1["Amt"] = amt;
                row1["GstAmt"] = SafeValue.SafeString(det.Rows[i]["GstAmt"]);
                row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);
                row1["Qty"] = SafeValue.SafeInt(det.Rows[i]["Qty"], 0);
                details.Rows.Add(row1);


                subTotal += amt;
                gstTotal += gstAmt;

            }

            row["SubTotal"] = subTotal;
            row["GstTotal"] = gstTotal;
            row["Total"] = subTotal + gstTotal;
            mast.Rows.Add(row);

            #endregion

            set.Tables.Add(mast);
            set.Tables.Add(delivery);
            set.Tables.Add(details);
        }
        return set;
    }
    public static DataSet DsImpTsGroup(string orderNo, string docType)
    {
        DataSet set = new DataSet();
        if (docType == "IV" || docType == "IV1")
        {
            ObjectQuery query = new ObjectQuery(typeof(C2.XAArInvoice), "DocNo='" + orderNo + "'", "");
            ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
            C2.XAArInvoice obj = objSet[0] as C2.XAArInvoice;
            #region Invoice

            DataTable mast = InitMastDataTable();

            DataTable delivery = InitDeliveryDataTable();
            DataTable details = InitDetailDataTable();
            DataRow row = mast.NewRow();
            if (obj != null)
            {

                string mastRefNo = obj.MastRefNo;
                string mastType = obj.MastType;

                Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + mastRefNo + "'");

                C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
                if (ctmJob != null)
                {
                    #region Job
                    row["Vessel"] = ctmJob.Vessel;
                    row["Voyage"] = ctmJob.Voyage;
                    row["Eta"] = ctmJob.EtaDate.ToString("dd.MM.yy");
                    row["CustRefNo"] = ctmJob.ClientRefNo;
                    row["Pol"] = ctmJob.Pol;
                    row["Pod"] = ctmJob.Pod;
                    row["CrBkgNo"] = ctmJob.CarrierBkgNo;
                    row["HblNo"] = ctmJob.CarrierBlNo;
                    row["JobDes"] = ctmJob.JobDes;
                    row["CustRefNo"] = ctmJob.ClientRefNo;
                    #endregion
                }
                row["JobNo"] = mastRefNo;
                row["BillType"] = "TAX INVOICE";
                row["DocNo"] = obj.DocNo;
                row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
                row["Contact"] = obj.Contact;
                row["DocDueDate"] = obj.DocDueDate.ToString("dd/MM/yyyy");
                row["UserId"] = HttpContext.Current.User.Identity.Name;
                row["PartyName"] = obj.PartyName;
                row["PartyTo"] = obj.PartyTo;
                string tel1 = "";
                string fax1 = "";
                string email1 = "";
                string sql = string.Format(@"select Address,Contact1,Fax1,Tel1,CrNo,City,ZipCode,Email1 from XXParty where PartyId='{0}'", obj.PartyTo);
                DataTable tab = ConnectSql_mb.GetDataTable(sql);
                if (tab.Rows.Count > 0)
                {
                    string zipCode = tab.Rows[0]["ZipCode"].ToString();
                    string city = tab.Rows[0]["City"].ToString();
                    if (zipCode.Length > 0)
                        zipCode = "\n" + city + " " + zipCode;
                    row["PartyAdd"] = tab.Rows[0]["Address"].ToString() + zipCode;
                    row["CrNo"] = tab.Rows[0]["CrNo"].ToString();
                    tel1 = tab.Rows[0]["Tel1"].ToString();
                    fax1 = tab.Rows[0]["Fax1"].ToString();
                    email1 = tab.Rows[0]["Email1"].ToString();
                    if (fax1.Length > 0)
                        row["Fax1"] = "Fax:" + fax1;
                    if (tel1.Length > 0)
                        row["Tel1"] = "Tel:" + tel1;
                    if (email1.Length > 0)
                        row["Email1"] = "Email:" + email1;
                }
                sql = string.Format(@"select Email,Fax,Tel from ref_contact where PartyId='{0}' and Name='{1}'", obj.PartyTo, obj.Contact);
                DataTable tab_p = ConnectSql_mb.GetDataTable(sql);
                if (tab_p.Rows.Count > 0)
                {
                    string tel = tab_p.Rows[0]["Tel"].ToString();
                    string fax = tab_p.Rows[0]["Fax"].ToString();
                    string email = tab_p.Rows[0]["Email"].ToString();
                    if (fax.Length > 0 && fax1.Length == 0)
                        row["Fax1"] = "Fax:" + fax;
                    if (tel.Length > 0 && tel1.Length == 0)
                        row["Tel1"] = "Tel:" + tel;
                    if (email.Length > 0 && email1.Length == 0)
                        row["Email1"] = "Email:" + email;
                }
                row["Terms"] = obj.Term;
                row["Terms"] = obj.Term;
                row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                row["MoneyWords"] = "";
                row["Remark"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select Description from XAArInvoice where MastRefNo='{0}'", mastRefNo)));

                sql = string.Format(@"select top 1 TripIndex from ctm_jobdet2 where JobNo='{0}'  order by Id asc", mastRefNo);
                row["DoNo"] = ConnectSql_mb.ExecuteScalar(sql);
                decimal subTotal = 0;
                decimal subGstTotal = 0;

                ObjectQuery query1 = new ObjectQuery(typeof(C2.CtmJobDet1), "JobNo='" + mastRefNo + "'", "");
                ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);



                sql = string.Format(@"select top 1 BookingDate,TripIndex from ctm_jobdet2 where JobNo='{0}'  order by Id asc", mastRefNo);
                DataTable dt_det2 = ConnectSql_mb.GetDataTable(sql);
                //ObjectQuery query2 = new ObjectQuery(typeof(C2.CtmJobDet2), "JobNo='" + mastRefNo + "'  and JobType in ('WDO','CRA','WGR')", "");
                //ObjectSet objSet2 = C2.Manager.ORManager.GetObjectSet(query2);
                for (int i = 0; i < dt_det2.Rows.Count; i++)
                {
                    string contNo = "";
                    if (objSet1.Count > 0)
                    {
                        for (int j = 0; j < objSet1.Count; j++)
                        {
                            CtmJobDet1 det1 = objSet1[j] as CtmJobDet1;
                            if (objSet1.Count - j == 1)
                                contNo += det1.ContainerNo;
                            else
                                contNo += det1.ContainerNo + ",";
                        }
                    }
                    //CtmJobDet2 det2=objSet2[i] as CtmJobDet2;
                    DataRow row_det2 = dt_det2.Rows[i];
                    DataRow row1 = delivery.NewRow();
                    row1["VesVoy"] = ctmJob.Vessel + " / " + ctmJob.Voyage;
                    row1["TripIndex"] = row_det2["TripIndex"];
                    row1["JobNo"] = mastRefNo;
                    row1["ScheduleDate"] = SafeValue.SafeDateStr(row_det2["BookingDate"]);
                    if (ctmJob != null)
                    {
                        row1["ClientContact"] = ctmJob.ClientContact;
                        row1["FromCode"] = SafeValue.SafeString(ctmJob.PickupFrom).TrimEnd();
                        row1["ToCode"] = SafeValue.SafeString(ctmJob.DeliveryTo).TrimEnd();
                    }
                    //row1["TowheadCode"] = det2.TowheadCode;
                    row1["ContNo"] = contNo;


                    delivery.Rows.Add(row1);

                }

                sql = string.Format(@"select GroupBy as ChgCode,sum(Qty) as Qty,max(MastType) as MastType,max(DocNo) as DocNo,GroupBy ChgDes1,'' ChgDes4,'' ChgDes2,'' as ChgDes3,SUM(Price) as Price,max(Gst) Gst,sum(GstAmt) GstAmt,max(Unit) Unit,
sum(LineLocAmt) LineLocAmt, max(Currency) Currency,max(ExRate) as ExRate,max(LineIndex) as LineIndex,
sum(LocAmt) LocAmt,max(JobRefNo) as JobRefNo,max(GstType) GstType from XAArInvoiceDet where DocId={0} group by GroupBy order by LineIndex asc,Gst desc", obj.SequenceId);
                DataTable det = ConnectSql.GetTab(sql);
                for (int i = 0; i < det.Rows.Count; i++)
                {
                    DataRow row1 = details.NewRow();
                    string cntType = SafeValue.SafeString(det.Rows[i]["JobRefNo"]);
                    string chgCode = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                    string gstType = SafeValue.SafeString(det.Rows[i]["GstType"]);
                    string billingClass = SafeValue.SafeString(det.Rows[i]["MastType"]);
                    string chgDes = SafeValue.SafeString(det.Rows[i]["ChgDes1"]);
                    row1["No"] = i + 1;
                    row1["DocNo"] = SafeValue.SafeString(det.Rows[i]["DocNo"]);
                    row1["ChgDes1"] = SafeValue.SafeString(det.Rows[i]["ChgDes1"]) + "\n" + SafeValue.SafeString(det.Rows[i]["ChgDes4"]);
                    row1["ChgDes4"] = SafeValue.SafeString(det.Rows[i]["ChgDes4"]);
                    row1["ChgCode"] = SafeValue.SafeString(det.Rows[i]["ChgCode"]);

                    row1["Unit"] = SafeValue.SafeString(det.Rows[i]["Unit"]);
                    row1["Currency"] = SafeValue.SafeString(det.Rows[i]["Currency"]);
                    row1["ExRate"] = SafeValue.SafeString(det.Rows[i]["ExRate"]);
                    row1["Price"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Price"]), 2);
                    row1["Gst"] = SafeValue.SafeInt(SafeValue.SafeDecimal(det.Rows[i]["Gst"]) * 100, 0) + "% " + det.Rows[i]["GstType"] + "R";
                    decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["LocAmt"]),2);
                    //decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Qty"]) * SafeValue.SafeDecimal(det.Rows[i]["Price"], 0), 2);
                    decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(det.Rows[i]["Gst"], 0)), 2);
                    row1["Amt"] = amt;
                    row1["LineAmt"] = amt;
                    row1["GstAmt"] = SafeValue.SafeString(det.Rows[i]["GstAmt"]);
                    row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);
                    row1["Qty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Qty"]), 2);
                    if (gstType == "S")
                    {
                        row1["Cur"] = "(S)";
                        subGstTotal += amt;
                    }

                    details.Rows.Add(row1);


                    subTotal += amt;

                }
                sql = string.Format(@"select ContainerNo,SealNo,ContainerType from CTM_JobDet1 where JobNo='{0}' ", mastRefNo);
                DataTable cnt = ConnectSql.GetTab(sql);
                for (int i = 0; i < cnt.Rows.Count; i++)
                {

                    if (cnt.Rows.Count - i > 1)
                    {
                        row["ContainerNo"] += SafeValue.SafeString(cnt.Rows[i]["ContainerNo"]) + " / " + SafeValue.SafeString(cnt.Rows[i]["SealNo"])
                            + " / " + SafeValue.SafeString(cnt.Rows[i]["ContainerType"]) + "\n";
                    }
                    else
                    {
                        row["ContainerNo"] += SafeValue.SafeString(cnt.Rows[i]["ContainerNo"]) + " / " + SafeValue.SafeString(cnt.Rows[i]["SealNo"])
                            + " / " + SafeValue.SafeString(cnt.Rows[i]["ContainerType"]);
                    }

                }
                decimal gstTotal = SafeValue.ChinaRound(subGstTotal * SafeValue.SafeDecimal(0.07), 2);
                decimal nonGstAmt = 0;
                row["SubTotal"] = subTotal;
                row["SubGstTotal"] = subGstTotal;
                row["GstTotal"] = gstTotal;
                row["TotalAmt"] = subTotal + gstTotal;
                if (nonGstAmt > 0)
                    row["NonGstAmt"] = SafeValue.SafeDecimal(nonGstAmt);
                else
                    row["NonGstAmt"] = "";
                decimal totalAmt = subTotal + gstTotal;
                string str = "";
                if (totalAmt > 0)
                    str = Helper.NumberToEnglish.NumberToString(SafeValue.SafeDouble(totalAmt));
                row["AmtStr"] = str;
                mast.Rows.Add(row);

            }
            else
            {
                row["DocNo"] = "0";
                mast.Rows.Add(row);

                DataRow row1 = delivery.NewRow();
                row1["TripIndex"] = "";
                delivery.Rows.Add(row1);

                DataRow row2 = details.NewRow();
                row2["No"] = 0;
                details.Rows.Add(row2);
            }
            set.Tables.Add(mast);
            set.Tables.Add(delivery);
            set.Tables.Add(details);


            #endregion
        }
        if (docType == "PL" || docType == "PL1")
        {
            ObjectQuery query = new ObjectQuery(typeof(C2.XAApPayable), "DocNo='" + orderNo + "'", "");
            ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
            C2.XAApPayable obj = objSet[0] as C2.XAApPayable;
            #region PL
            DataTable mast = InitMastDataTable();
            DataRow row = mast.NewRow();
            string mastRefNo = obj.MastRefNo;
            string mastType = obj.MastType;
            if (mastType == "CTM")
            {

                Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + mastRefNo + "'");

                C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
                row["Vessel"] = ctmJob.Vessel;
                row["Voyage"] = ctmJob.Voyage;
                row["Eta"] = ctmJob.EtaDate.ToString("dd.MM.yy");
            }
            row["DocNo"] = obj.DocNo;
            row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
            row["SupplierBillNo"] = obj.SupplierBillNo;
            row["SupplierBillDate"] = obj.SupplierBillDate.ToString("dd/MM/yyyy");

            row["PartyName"] = obj.PartyName;
            string sql = string.Format(@"select Address from XXParty where PartyId='{0}'", obj.PartyTo);
            row["PartyAdd"] = C2.Manager.ORManager.ExecuteScalar(sql);
            row["Terms"] = obj.Term;

            decimal subTotal = 0;
            decimal gstTotal = 0;

            DataTable delivery = InitDeliveryDataTable();

            DataTable details = InitDetailDataTable();
            sql = string.Format(@"select ChgCode,sum(Qty) as Qty,MastType,max(DocNo) DocNo,max(ChgDes1) as ChgDes1,Price,max(Gst) Gst,max(GstAmt) GstAmt,
max(LocAmt) LocAmt,max(JobRefNo) JobRefNo,max(GstType) GstType from XAApPayableDet where DocId={0} group by ChgCode,MastType,Price order by Gst desc", obj.SequenceId);
            DataTable det = ConnectSql.GetTab(sql);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow row1 = details.NewRow();
                string cntType = SafeValue.SafeString(det.Rows[i]["JobRefNo"]);
                string chgCode = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["DocNo"] = SafeValue.SafeString(det.Rows[i]["DocNo"]);
                row1["ChgDes1"] = SafeValue.SafeString(det.Rows[i]["ChgDes1"]);
                row1["ChgCode"] = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["Price"] = string.Format("{0:#,##0.00}", SafeValue.SafeString(det.Rows[i]["Price"]));
                row1["Gst"] = SafeValue.SafeInt(SafeValue.SafeDecimal(det.Rows[i]["Gst"]) * 100, 0) + "% " + det.Rows[i]["GstType"] + "R";
                decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(det.Rows[i]["Qty"]) * SafeValue.SafeDecimal(det.Rows[i]["Price"], 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(det.Rows[i]["Gst"], 0)), 2);
                row1["Amt"] = amt;
                row1["GstAmt"] = SafeValue.SafeString(det.Rows[i]["GstAmt"]);
                row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);
                row1["Qty"] = SafeValue.SafeInt(det.Rows[i]["Qty"], 0);
                details.Rows.Add(row1);


                subTotal += amt;
                gstTotal += gstAmt;

            }

            row["SubTotal"] = subTotal;
            row["GstTotal"] = gstTotal;
            row["Total"] = subTotal + gstTotal;
            mast.Rows.Add(row);

            #endregion

            set.Tables.Add(mast);
            set.Tables.Add(delivery);
            set.Tables.Add(details);
        }
        return set;
    }
    public static DataSet DsPayTs(string orderNo)
    {
        ObjectQuery query = new ObjectQuery(typeof(C2.XAApPayment), "DocNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
        C2.XAApPayment obj = objSet[0] as C2.XAApPayment;
        #region Invoice
        DataTable mast = InitMastDataTable();
        DataRow row = mast.NewRow();
        //string mastRefNo = obj.MastRefNo;
        row["DocNo"] = obj.DocNo;
        row["DocType"] = obj.DocType;
        row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
        row["PartyTo"] = obj.PartyTo;
        row["PartyName"] = obj.OtherPartyName;
        row["ExRate"] = obj.ExRate;
        row["Currency"] = obj.CurrencyId;

        row["Remark"] = obj.Remark;

        row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];

        DataTable details = InitDetailDataTable();
        string sql = string.Format(@"select sum(LocAmt) as LocAmt,Remark1 from XAApPaymentDet where PayNo='{0}' group by Remark1", orderNo);
        DataTable det = ConnectSql.GetTab(sql);
        for (int i = 0; i < det.Rows.Count; i++)
        {
            DataRow row1 = details.NewRow();
            row1["Rmk"] = SafeValue.SafeString(det.Rows[i]["Remark1"]);
            row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);

            details.Rows.Add(row1);

        }
        row["LocAmt"] = obj.LocAmt;
        mast.Rows.Add(row);

        #endregion
        DataSet set = new DataSet();
        set.Tables.Add(mast);
        set.Tables.Add(details);
        //set.Relations.Add("", mast.Columns["DocNo"], details.Columns["DocNo"]);

        return set;
    }
}