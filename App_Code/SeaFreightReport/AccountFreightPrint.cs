using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using C2;

/// <summary>
/// Summary description for AccountPrint
/// </summary>
public class AccountFreightPrint
{
    public AccountFreightPrint()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet PrintInvoice(string invN, string docType)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintInvoice '{0}','{1}','{2}','{3}','{4}'", invN, "", docType, System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"], System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            for (int i = 5; Detail.Rows.Count < i; )
            {
                Detail.Rows.Add(Detail.NewRow());
            }
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["InvoiceN"], Detail.Columns["InvoiceN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;


//        DataSet set = new DataSet();

//        decimal gstAmt = 0;
//        decimal amt = 0;//
//        string cust_id = "2001";// 
//        string address = "";


//        DataTable tab_mast = new DataTable("Mast");
//        DataTable tab_det = new DataTable("Detail");
//        #region init column
//        tab_mast.Columns.Add("BillType");
//        tab_mast.Columns.Add("BillTypeStr");
//        tab_mast.Columns.Add("BarCode");
//        tab_mast.Columns.Add("InvoiceN");
//        tab_mast.Columns.Add("InvoiceType");
//        tab_mast.Columns.Add("InvDate");
//        tab_mast.Columns.Add("Name");
//        tab_mast.Columns.Add("Address");
//        tab_mast.Columns.Add("CustId");
//        tab_mast.Columns.Add("Terms");
//        tab_mast.Columns.Add("VesVoy");
//        tab_mast.Columns.Add("Eta");
//        tab_mast.Columns.Add("Etd");
//        tab_mast.Columns.Add("RefNo");
//        tab_mast.Columns.Add("JobNo");
//        tab_mast.Columns.Add("OblNo");
//        tab_mast.Columns.Add("HblNo");
//        tab_mast.Columns.Add("Pol");
//        tab_mast.Columns.Add("Pod");
//        tab_mast.Columns.Add("Des");
//        tab_mast.Columns.Add("Qty");
//        tab_mast.Columns.Add("PkgType");
//        tab_mast.Columns.Add("Wt");
//        tab_mast.Columns.Add("M3");
//        tab_mast.Columns.Add("MoneyWords");
//        tab_mast.Columns.Add("Amt");
//        tab_mast.Columns.Add("TaxAmt");
//        tab_mast.Columns.Add("TotAmt");
//        tab_mast.Columns.Add("UserId");
//        tab_mast.Columns.Add("CompanyName");
//        tab_mast.Columns.Add("CompanyInvHeader");
//        tab_mast.Columns.Add("Currency");
//        tab_mast.Columns.Add("ExRate");

//        tab_mast.Columns.Add("Shipper");
//        tab_mast.Columns.Add("Cng");
//        tab_mast.Columns.Add("ShipTo");

//        tab_det.Columns.Add("InvoiceN");
//        tab_det.Columns.Add("LineN");
//        tab_det.Columns.Add("Des");
//        tab_det.Columns.Add("Currency");
//        tab_det.Columns.Add("Price");
//        tab_det.Columns.Add("Qty");
//        tab_det.Columns.Add("Unit");
//        tab_det.Columns.Add("Rate");
//        tab_det.Columns.Add("GstType");
//        tab_det.Columns.Add("GstAmt");
//        tab_det.Columns.Add("LineAmt");
//        #endregion
//        string sql = string.Format(@"SELECT  DocType, DocNo, DocDate, PartyTo, MastRefNo, JobRefNo, MastType, CurrencyId, ExRate, Term, UserId, EntryDate, Description
// FROM  XAArInvoice WHERE (DocNo = '{0}') and (DocType='{1}') ", invN, docType);
//        string sql1 = string.Format(@"SELECT DocLineNo,ChgCode, ChgDes1, ChgDes2, ChgDes3, ChgDes4, GstType, Qty, Price, Unit, Currency, ExRate, Gst, GstAmt, DocAmt, LocAmt
//FROM XAArInvoiceDet WHERE (DocNo = '{0}') and (DocType='{1}') order by DocLineNo", invN, docType);

//        DataTable master = ConnectSql.GetTab(sql);
//        DataTable det = ConnectSql.GetTab(sql1);
//        for (int i = 0; i < det.Rows.Count; i++)
//        {
//            DataRow oldRow = det.Rows[i];
//            DataRow newRow = tab_det.NewRow();

//            newRow["InvoiceN"] = invN;
//            newRow["LineN"] = oldRow["DocLineNo"];
//            string des = "";
//            if (SafeValue.SafeString(oldRow["ChgDes1"], "").Length > 0)
//            {
//                des += SafeValue.SafeString(oldRow["ChgDes1"], "");
//            }
//            if (SafeValue.SafeString(oldRow["ChgDes2"], "").Length > 0)
//            {
//                des += "\n" + SafeValue.SafeString(oldRow["ChgDes2"], "");
//            }
//            if (SafeValue.SafeString(oldRow["ChgDes3"], "").Length > 0)
//            {
//                des += "\n" + SafeValue.SafeString(oldRow["ChgDes3"], "");
//            }
//            if (SafeValue.SafeString(oldRow["ChgDes4"], "").Length > 0)
//            {
//                des += "\n" + SafeValue.SafeString(oldRow["ChgDes4"], "");
//            }
//            newRow["Des"] = des;
//            newRow["Currency"] = oldRow["Currency"];
//            newRow["Qty"] = SafeValue.SafeDecimal(oldRow["QTY"], 0).ToString("0.000");
//            newRow["Price"] = SafeValue.SafeDecimal(oldRow["PRICE"], 0).ToString("0.000");
//            newRow["GstType"] = oldRow["GstType"];
//            newRow["Unit"] = oldRow["UNIT"];
//            newRow["Rate"] = SafeValue.SafeDecimal(oldRow["ExRate"], 0).ToString("0.0000");
//            if (SafeValue.SafeDecimal(oldRow["GstAmt"], 0) == 0)
//            {
//                newRow["GstAmt"] = "";
//            }
//            else
//            {
//                newRow["GstAmt"] = SafeValue.SafeDecimal(oldRow["GstAmt"], 0).ToString("0.00");
//            }
//            newRow["LineAmt"] = (SafeValue.SafeDecimal(oldRow["LocAmt"], 0) - SafeValue.SafeDecimal(oldRow["GstAmt"], 0)).ToString("0.00");
//            tab_det.Rows.Add(newRow);
//            amt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["LocAmt"], 0) - SafeValue.SafeDecimal(oldRow["GstAmt"], 0), 2);
//            gstAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["GstAmt"], 0), 2);
//        }
//        for (int i = 0; i < 5 - det.Rows.Count; i++)
//        {
//            DataRow newRow = tab_det.NewRow();
//            tab_det.Rows.Add(newRow);

//        }
//        if (master.Rows.Count == 1)
//        {
//            DataRow row_Source = master.Rows[0];
//            DataRow row = tab_mast.NewRow();
//            row["BillType"] = "TAX INVOICE";
//            row["BarCode"] = docType + "-" + invN;
//            if (docType == "CN")
//            {
//                row["BillType"] = "CREDIT NOTE";
//                row["BillTypeStr"] = "CREDIT NOTE";
//            }
//            else if (docType == "DN")
//            {
//                row["BillType"] = "DEBIT NOTE";
//                row["BillTypeStr"] = "DEBIT NOTE";
//            }

//            row["InvoiceN"] = invN;
//            row["InvoiceType"] = docType;
//            row["InvDate"] = SafeValue.SafeDate(row_Source["DocDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

//            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
//            string sql_cust = "select Name,Address,Tel1,Fax1,SalesmanId from XXParty where PartyId = '" + cust_id + "'";
//            string salesman = "";
//            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
//            if (tab_cust.Rows.Count > 0)
//            {
//                // cust_id = tab_cust.Rows[0][0].ToString();
//                address = tab_cust.Rows[0]["Address"].ToString().Trim();
//                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
//                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();
//                salesman = SafeValue.SafeString(tab_cust.Rows[0]["SalesmanId"], "");

//                if (tel.Length > 0)
//                {
//                    address += "\nTel:" + tel;
//                }
//                if (fax.Length > 0)
//                {
//                    address += " Fax:" + fax;
//                }
//                row["Name"] = tab_cust.Rows[0]["Name"].ToString() + "[" + cust_id + "]";
//                row["Address"] = address;
//            }

//            row["CustId"] = cust_id;
//            row["Terms"] = row_Source["Term"];
//            row["Des"] = row_Source["Description"];

//            string mastNo = row_Source["MastRefNo"].ToString();
//            string jobNo = row_Source["JobRefNo"].ToString();
//            string jobType = row_Source["MastType"].ToString();
//            row["RefNo"] = mastNo;
//            row["JobNo"] = jobNo;
//            string sql_ref = "";
//            string sql_job = "";
//            string sql_mkg = "";
//            if (jobType == "SI")
//            {
//                sql_ref = string.Format(@"select OblNo,Pol,Pod,Eta,Etd,Vessel,Voyage,Weight,Volume,Qty,Packagetype,SShipperRemark,SConsigneeRemark From SeaImportRef  where RefNo='{0}' ", mastNo, jobNo);
//                sql_job = string.Format(@"select job.HblNo,job.Weight,job.Volume,job.Qty,job.Packagetype,job.SShipperRemark,job.SConsigneeRemark
//from SeaImport job where Job.RefNo='{0}' and job.JobNo='{1}'", mastNo, jobNo);
//                sql_mkg = string.Format("SELECT top(1) Description FROM SeaImportMkg where RefNo='{0}' and JobNo='{1}' order by SequenceId", mastNo, jobNo);
//            }
//            else if (jobType == "SE")
//            {
//                sql_ref = string.Format(@"select OblNo,Pol,Pod,Eta,Etd,Vessel,Voyage,Weight,Volume,Qty,Packagetype,SShipperRemark,SConsigneeRemark From SeaExportRef  where RefNo='{0}' ", mastNo, jobNo);
//                sql_job = string.Format(@"select job.HblNo,job.Weight,job.Volume,job.Qty,job.Packagetype,job.SShipperRemark,job.SConsigneeRemark
//from SeaExport job where Job.RefNo='{0}' and job.JobNo='{1}'", mastNo, jobNo);
//                sql_mkg = string.Format("SELECT top(1) Description FROM SeaExportMkg where RefNo='{0}' and JobNo='{1}' order by SequenceId", mastNo, jobNo);
//            }
//            else if (jobType == "AI")
//            {
//                sql_ref = string.Format("Select AirportCode0 as Vessel, AirportName0 as Voyage,(FlightDate0+' '+FlightTime0) as  Eta, MAWB, NvoccBlNO as Pol,CarrierBkgNo as Pod, Currency, CurrencyRate from air_ref where RefNo='{0}'", mastNo);
//                sql_job = string.Format("SELECT HAWB, Total, Unit, GrossWeight,TotalPrepaid, TotalCollect FROM air_job WHERE (RefNo = '{0}') AND (JobNo = '{1}')", mastNo, jobNo);
//                sql_mkg = string.Format("SELECT Remark FROM air_costing where RefNo='{0}' and JobNo='{1}'", mastNo, jobNo);
//            }
//            else if (jobType == "AE")
//            {
//                sql_ref = string.Format("Select AirportCode0 as Vessel, AirportName0 as Voyage,(FlightDate0+' '+FlightTime0) as  Eta, MAWB,NvoccBlNO as Pol,CarrierBkgNo as Pod, Currency, CurrencyRate from air_ref where RefNo='{0}'", mastNo);
//                sql_job = string.Format("SELECT HAWB, Total, Unit, GrossWeight, TotalPrepaid, TotalCollect FROM air_job WHERE (RefNo = '{0}') AND (JobNo = '{1}')", mastNo, jobNo);
//                sql_mkg = string.Format("SELECT Remark FROM air_costing where RefNo='{0}' and JobNo='{1}'", mastNo, jobNo);
//            }

//            DataTable tab_ref = Manager.ORManager.GetDataSet(sql_ref).Tables[0];
//            if (tab_ref.Rows.Count > 0)
//            {
//                DataRow row_job = tab_ref.Rows[0];
//                row["OblNo"] = SafeValue.SafeString(row_job["OblNo"], "");
//                row["VesVoy"] = SafeValue.SafeString(row_job["Vessel"], "") + " " + SafeValue.SafeString(row_job["Voyage"], "");
//                row["Eta"] = SafeValue.SafeDateStr(row_job["Eta"]);
//                row["Etd"] = SafeValue.SafeDateStr(row_job["Eta"]);
//                row["Pol"] = EzshipHelper.GetPortName(row_job["Pol"]);
//                row["Pod"] = EzshipHelper.GetPortName(row_job["Pod"]);
//                row["Qty"] = row_job["Qty"];
//                row["PkgType"] = row_job["PackageType"];
//                row["Wt"] = SafeValue.SafeDecimal(row_job["Weight"], 0).ToString("0.000");
//                row["M3"] = SafeValue.SafeDecimal(row_job["Volume"], 0).ToString("0.000");
//                row["Shipper"] = row_job["SShipperRemark"];
//                row["Cng"] = row_job["SConsigneeRemark"];
//                //row["ShipTo"] = row_job["PackageType"];
//            }
//            DataTable tab_job = Manager.ORManager.GetDataSet(sql_job).Tables[0];
//            if (tab_job.Rows.Count > 0)
//            {
//                DataRow row_job = tab_job.Rows[0];
//                row["Qty"] = row_job["Qty"];
//                row["PkgType"] = row_job["PackageType"];
//                row["Wt"] = SafeValue.SafeDecimal(row_job["Weight"], 0).ToString("0.000");
//                row["M3"] = SafeValue.SafeDecimal(row_job["Volume"], 0).ToString("0.000");
//                row["HblNo"] = SafeValue.SafeString(row_job["HblNo"], "");
//                row["Shipper"] = row_job["SShipperRemark"];
//                row["Cng"] = row_job["SConsigneeRemark"];
//                //row["ShipTo"] = row_job["PackageType"];

//            }
//            row["Des"] = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_mkg), "").Replace("\n", "  ");

//            row["Amt"] = SafeValue.ChinaRound(amt, 2).ToString("0.00");
//            row["TaxAmt"] = SafeValue.ChinaRound(gstAmt, 2).ToString("0.00");
//            row["TotAmt"] = SafeValue.ChinaRound(amt + gstAmt, 2).ToString("0.00");
//            string currency = row_Source["CurrencyId"].ToString().ToUpper();
//            decimal exRate = SafeValue.SafeDecimal(row_Source["ExRate"], 0);
//            row["MoneyWords"] = "TOTAL SINGAPORE DOLLARS: " + (new NumberToMoney()).NumberToCurrency(SafeValue.ChinaRound(amt + gstAmt, 2)).ToUpper() + " ONLY";
//            if (currency == "USD")
//                row["MoneyWords"] = "TOTAL US DOLLARS: " + (new NumberToMoney()).NumberToCurrency(SafeValue.ChinaRound(amt + gstAmt, 2)).ToUpper() + " ONLY";
//            row["Currency"] = currency;
//            row["ExRate"] = exRate.ToString("0.000");
//            row["UserId"] = row_Source["UserId"];
//            if (salesman.Length > 2)
//            {
//                row["UserId"] = SafeValue.SafeString(row_Source["UserId"], "") + " / " + salesman;
//            }
//            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"];
//            tab_mast.Rows.Add(row);
//        }

//        set.Tables.Add(tab_mast);
//        set.Tables.Add(tab_det);
//        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
//        set.Relations.Add(r);

//        return set;
    }

    public static DataSet PrintVoucher(string invN)
    {

        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintVoucher '{0}','{1}','{2}','{3}','{4}'", invN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"], System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            for (int i = 5; Detail.Rows.Count < i; )
            {
                Detail.Rows.Add(Detail.NewRow());
            }
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation rr = new DataRelation("", Mast.Columns["InvoiceN"], Detail.Columns["InvoiceN"]);
            ds.Relations.Add(rr);
        }
        catch (Exception ex) { }
        return ds;

//        DataSet set = new DataSet();
//        decimal gstAmt = 0;
//        decimal amt = 0;

//        DataTable tab_mast = new DataTable("Mast");
//        tab_mast.Columns.Add("InvoiceN");
//        tab_mast.Columns.Add("InvDate");
//        tab_mast.Columns.Add("RefNo");
//        tab_mast.Columns.Add("MastType");
//        tab_mast.Columns.Add("JobType");
//        tab_mast.Columns.Add("Name");
//        tab_mast.Columns.Add("Cust");
//        tab_mast.Columns.Add("VesVoy");
//        tab_mast.Columns.Add("Pol");
//        tab_mast.Columns.Add("Pod");
//        tab_mast.Columns.Add("Eta");
//        tab_mast.Columns.Add("Obl");
//        tab_mast.Columns.Add("Des");
//        tab_mast.Columns.Add("Currency");
//        tab_mast.Columns.Add("TotAmt");
//        tab_mast.Columns.Add("UserId");
//        DataTable tab_det = new DataTable("Detail");
//        tab_det.Columns.Add("InvoiceN");
//        tab_det.Columns.Add("LineN");
//        tab_det.Columns.Add("Des");
//        tab_det.Columns.Add("Currency");
//        tab_det.Columns.Add("Price");
//        tab_det.Columns.Add("Qty");
//        tab_det.Columns.Add("Unit");
//        tab_det.Columns.Add("Rate");
//        tab_det.Columns.Add("GstAmt");
//        tab_det.Columns.Add("LineAmt");
//        string sql_mast = string.Format(@"SELECT DocType, DocNo, DocDate, PartyTo, MastRefNo, JobRefNo, MastType, CurrencyId, ExRate, Term, Description,UserId
//FROM XAApPayable WHERE (DocNo = '{0}') AND (DocType = 'VO')", invN);
//        string sql_det = string.Format(@"SELECT     ChgCode, ChgDes1, ChgDes2, ChgDes3, ChgDes4, GstType, Qty, Price, Unit, Currency, ExRate, Gst, GstAmt, DocAmt, LocAmt
//FROM XAApPayableDet WHERE (DocNo = '{0}') AND (DocType = 'VO')", invN);
//        DataTable mast = Manager.ORManager.GetDataSet(sql_mast).Tables[0];
//        if (mast.Rows.Count == 0) return set;
//        DataTable det = Manager.ORManager.GetDataSet(sql_det).Tables[0];
//        int index = 0;
//        for (int i = 0; i < det.Rows.Count; i++)
//        {
//            DataRow oldRow = det.Rows[i];
//            DataRow newRow = tab_det.NewRow();
//            index++;
//            newRow["InvoiceN"] = invN;
//            newRow["LineN"] = index.ToString();
//            string des = "";
//            if (SafeValue.SafeString(oldRow["ChgDes1"], "").Length > 0)
//            {
//                des += SafeValue.SafeString(oldRow["ChgDes1"], "");
//            }
//            //if (SafeValue.SafeString(oldRow["ChgDes2"], "").Length > 0)
//            //{
//            //    des += "\n" + SafeValue.SafeString(oldRow["ChgDes2"], "");
//            //}
//            //if (SafeValue.SafeString(oldRow["ChgDes3"], "").Length > 0)
//            //{
//            //    des += "\n" + SafeValue.SafeString(oldRow["ChgDes3"], "");
//            //}
//            //if (SafeValue.SafeString(oldRow["ChgDes4"], "").Length > 0)
//            //{
//            //    des += "\n" + SafeValue.SafeString(oldRow["ChgDes4"], "");
//            //}
//            newRow["Des"] = des;
//            newRow["Qty"] = SafeValue.SafeDecimal(oldRow["QTY"], 0).ToString("0.000");
//            newRow["Price"] = SafeValue.SafeDecimal(oldRow["PRICE"], 0).ToString("0.000");
//            newRow["Unit"] = oldRow["UNIT"];
//            newRow["Currency"] = oldRow["Currency"];
//            newRow["Rate"] = SafeValue.SafeDecimal(oldRow["ExRate"], 1).ToString("0.0000");
//            if (SafeValue.SafeDecimal(oldRow["GstAmt"], 0) == 0)
//            {
//                newRow["GstAmt"] = "";
//            }
//            else
//            {
//                newRow["GstAmt"] = SafeValue.SafeDecimal(oldRow["GstAmt"], 0).ToString("0.00");
//            }
//            newRow["LineAmt"] = SafeValue.SafeDecimal(oldRow["LocAmt"], 0).ToString("0.00");
//            tab_det.Rows.Add(newRow);
//            amt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["LocAmt"], 0) - SafeValue.SafeDecimal(oldRow["GstAmt"], 0), 2);
//            gstAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["GstAmt"], 0), 2);
//        }




//        DataRow row = tab_mast.NewRow();

//        row["InvoiceN"] = invN;
//        row["InvDate"] = string.Format("{0:dd/MM/yyyy}",mast.Rows[0]["DocDate"]);
//        row["Name"] = "";
//        string partyTo = SafeValue.SafeString(mast.Rows[0]["PartyTo"], "");
//        row["Name"] = EzshipHelper.GetPartyName(partyTo);


//        row["Currency"] = mast.Rows[0]["CurrencyId"];

//        row["Des"] = mast.Rows[0]["Description"];
//        string mastNo = mast.Rows[0]["MastRefNo"].ToString();
//        string jobNo = mast.Rows[0]["JobRefNo"].ToString();
//        string jobType = mast.Rows[0]["MastType"].ToString();
//        row["RefNo"] = jobNo;
//        string sql_job = "";
//        string sql_cust = "";
//        if (jobType == "SI")
//        {
//            sql_job = string.Format("Select Vessel, Voyage, Eta,OblNo, Pol, Pod,JobType from SeaImportRef where RefNo='{0}'", mastNo);
//            sql_cust = string.Format("select dbo.fun_GetPartyName(customerId) from SeaImport where jobno='{0}'", jobNo);
//        }
//        else if (jobType == "SE"||jobType=="SOT")
//        {
//            sql_job = string.Format("Select Vessel, Voyage, Eta,OblNo, Pol, Pod,JobType from SeaExportRef where RefNo='{0}'", mastNo);
//            sql_cust = string.Format("select dbo.fun_GetPartyName(customerId) from SeaExport where jobno='{0}'", jobNo);
//        }
//        else if (jobType.Length>1&&jobType.Substring(0,1) == "A")
//         {
//           sql_job = string.Format("Select AirlineCode1 as Vessel,AirlineName1 as Voyage,(FlightDate0+''+FlightTime0) as Eta,NvoccBlNO as OblNo,AirportName0 as Pol,AirportName1 as Pod, RefType as JobType from air_ref where RefNo='{0}'", mastNo);
//            sql_cust = string.Format("select dbo.fun_GetPartyName(customerId) from air_job where jobno='{0}'", jobNo);
//        }
//        DataTable tab_job = Manager.ORManager.GetDataSet(sql_job).Tables[0];
//        if (tab_job.Rows.Count > 0)
//        {
//            DataRow row_job = tab_job.Rows[0];
//            row["VesVoy"] = SafeValue.SafeString(row_job["Vessel"], "") + " " + SafeValue.SafeString(row_job["Voyage"], "");
//            row["Eta"] = SafeValue.SafeDate(row_job["Eta"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
//            row["Pol"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_job["Pol"] + "'"), row_job["Pol"].ToString());
//            row["Pod"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_job["Pod"] + "'"), row_job["Pod"].ToString());
//            row["Obl"] = row_job["OblNo"];
//            row["JobType"] = row_job["JobType"];
//        }
//        if (sql_cust.Length > 0)
//            row["Cust"] = C2.Manager.ORManager.ExecuteScalar(sql_cust);
//        if (jobType == "SE")
//            row["MastType"] = "Export";
//        else if (jobType == "SI")
//            row["MastType"] = "Import";
//        else if (jobType == "AI")
//            row["MastType"] = "Import";
//        else if (jobType == "AE")
//            row["MastType"] = "Export";
//        else if (jobType == "ACT"||jobType=="SCT")
//            row["MastType"] = "CrossTrade";
//        else
//            row["MastType"] = "Other";


//        row["TotAmt"] = SafeValue.ChinaRound(amt + gstAmt, 2).ToString("0.00");
//        row["UserId"] = mast.Rows[0]["UserId"];
//        tab_mast.Rows.Add(row);


//        set.Tables.Add(tab_mast);
//        set.Tables.Add(tab_det);
//        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
//        set.Relations.Add(r);
//        return set;
    }

    public static DataSet PrintPayable(string invN)
    {
        
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintPayable '{0}','{1}','{2}','{3}','{4}'", invN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"], System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            for (int i = 5; Detail.Rows.Count < i; )
            {
                Detail.Rows.Add(Detail.NewRow());
            }
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation rr = new DataRelation("", Mast.Columns["InvoiceN"], Detail.Columns["InvoiceN"]);
            ds.Relations.Add(rr);
        }
        catch (Exception ex) { }
        return ds;
//        DataSet set = new DataSet();
//        decimal gstAmt = 0;
//        decimal amt = 0;

//        DataTable tab_mast = new DataTable("Mast");
//        tab_mast.Columns.Add("InvoiceN");
//        tab_mast.Columns.Add("InvDate");
//        tab_mast.Columns.Add("RefNo");
//        tab_mast.Columns.Add("MastType");
//        tab_mast.Columns.Add("JobType");
//        tab_mast.Columns.Add("Name");
//        tab_mast.Columns.Add("VesVoy");
//        tab_mast.Columns.Add("Pol");
//        tab_mast.Columns.Add("Pod");
//        tab_mast.Columns.Add("Eta");
//        tab_mast.Columns.Add("Obl");
//        tab_mast.Columns.Add("Des");
//        tab_mast.Columns.Add("Currency");
//        tab_mast.Columns.Add("TotAmt");
//        tab_mast.Columns.Add("UserId");
//        DataTable tab_det = new DataTable("Detail");
//        tab_det.Columns.Add("InvoiceN");
//        tab_det.Columns.Add("LineN");
//        tab_det.Columns.Add("Des");
//        tab_det.Columns.Add("Currency");
//        tab_det.Columns.Add("Price");
//        tab_det.Columns.Add("Qty");
//        tab_det.Columns.Add("Unit");
//        tab_det.Columns.Add("Rate");
//        tab_det.Columns.Add("GstAmt");
//        tab_det.Columns.Add("LineAmt");
//        string sql_mast = string.Format(@"SELECT DocType, DocNo, DocDate, PartyTo, MastRefNo, JobRefNo, MastType, CurrencyId, ExRate, Term, Description,UserId
//FROM XAApPayable WHERE (DocNo = '{0}') AND (DocType = 'PL')", invN);
//        string sql_det = string.Format(@"SELECT     ChgCode, ChgDes1, ChgDes2, ChgDes3, ChgDes4, GstType, Qty, Price, Unit, Currency, ExRate, Gst, GstAmt, DocAmt, LocAmt
//FROM XAApPayableDet WHERE (DocNo = '{0}') AND (DocType = 'PL')", invN);
//        DataTable mast = Manager.ORManager.GetDataSet(sql_mast).Tables[0];
//        if (mast.Rows.Count == 0) return set;
//        DataTable det = Manager.ORManager.GetDataSet(sql_det).Tables[0];
//        int index = 0;
//        for (int i = 0; i < det.Rows.Count; i++)
//        {
//            DataRow oldRow = det.Rows[i];
//            DataRow newRow = tab_det.NewRow();
//            index++;
//            newRow["InvoiceN"] = invN;
//            newRow["LineN"] = index.ToString();
//            string des = "";
//            if (SafeValue.SafeString(oldRow["ChgDes1"], "").Length > 0)
//            {
//                des += SafeValue.SafeString(oldRow["ChgDes1"], "");
//            }
//            //if (SafeValue.SafeString(oldRow["ChgDes2"], "").Length > 0)
//            //{
//            //    des += "\n" + SafeValue.SafeString(oldRow["ChgDes2"], "");
//            //}
//            //if (SafeValue.SafeString(oldRow["ChgDes3"], "").Length > 0)
//            //{
//            //    des += "\n" + SafeValue.SafeString(oldRow["ChgDes3"], "");
//            //}
//            //if (SafeValue.SafeString(oldRow["ChgDes4"], "").Length > 0)
//            //{
//            //    des += "\n" + SafeValue.SafeString(oldRow["ChgDes4"], "");
//            //}
//            newRow["Des"] = des;
//            newRow["Qty"] = SafeValue.SafeDecimal(oldRow["QTY"], 0).ToString("0.000");
//            newRow["Price"] = SafeValue.SafeDecimal(oldRow["PRICE"], 0).ToString("0.000");
//            newRow["Unit"] = oldRow["UNIT"];
//            newRow["Currency"] = oldRow["Currency"];
//            newRow["Rate"] = SafeValue.SafeDecimal(oldRow["ExRate"], 1).ToString("0.0000");
//            if (SafeValue.SafeDecimal(oldRow["GstAmt"], 0) == 0)
//            {
//                newRow["GstAmt"] = "";
//            }
//            else
//            {
//                newRow["GstAmt"] = SafeValue.SafeDecimal(oldRow["GstAmt"], 0).ToString("0.00");
//            }
//            newRow["LineAmt"] = SafeValue.SafeDecimal(oldRow["LocAmt"], 0).ToString("0.00");
//            tab_det.Rows.Add(newRow);
//            amt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["LocAmt"], 0) - SafeValue.SafeDecimal(oldRow["GstAmt"], 0), 2);
//            gstAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["GstAmt"], 0), 2);
        //}




        //DataRow row = tab_mast.NewRow();

        //row["InvoiceN"] = invN;
        //row["InvDate"] = SafeValue.SafeString(mast.Rows[0]["DocDate"], "");
        //row["Name"] = "";
        //string partyTo = SafeValue.SafeString(mast.Rows[0]["PartyTo"], "");
        //row["Name"] = EzshipHelper.GetPartyName(partyTo);


        //row["Currency"] = mast.Rows[0]["CurrencyId"];

        //row["Des"] = mast.Rows[0]["Description"];
        //string mastNo = mast.Rows[0]["MastRefNo"].ToString();
        //string jobNo = mast.Rows[0]["JobRefNo"].ToString();
        //string jobType = mast.Rows[0]["MastType"].ToString();
        //row["RefNo"] = jobType + "/" + mastNo + "/" + jobNo;
        //string sql_job = "";
        //if (jobType == "SI")
        //{
        //    sql_job = string.Format("Select Vessel, Voyage, Eta,OblNo, Pol, Pod,JobType from SeaImportRef where RefNo='{0}'", mastNo);
        //}
        //else if (jobType == "SE")
        //    sql_job = string.Format("Select Vessel, Voyage, Eta,OblNo, Pol, Pod,JobType from SeaExportRef where RefNo='{0}'", mastNo);
        //else if (jobType == "AI")
        //    sql_job = string.Format("Select AirlineCode1 as Vessel,AirlineName1 as Voyage,(FlightDate0+''+FlightTime0) as Eta,NvoccBlNO as OblNo,AirportName0 as Pol,AirportName1 as Pod, RefType as JobType from air_ref where RefNo='{0}'", mastNo);
        //else if (jobType == "AE")
        //    sql_job = string.Format("Select AirlineCode1 as Vessel,AirlineName1 as Voyage,(FlightDate0+''+FlightTime0) as Eta,NvoccBlNO as OblNo,AirportName0 as Pol,AirportName1 as Pod, RefType as JobType from air_ref where RefNo='{0}'", mastNo);
        //DataTable tab_job = Manager.ORManager.GetDataSet(sql_job).Tables[0];
        //if (tab_job.Rows.Count > 0)
        //{
        //    DataRow row_job = tab_job.Rows[0];
        //    row["VesVoy"] = SafeValue.SafeString(row_job["Vessel"], "") + " " + SafeValue.SafeString(row_job["Voyage"], "");
        //    row["Eta"] = SafeValue.SafeDate(row_job["Eta"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
        //    row["Pol"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_job["Pol"] + "'"), row_job["Pol"].ToString());
        //    row["Pod"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_job["Pod"] + "'"), row_job["Pod"].ToString());
        //    row["Obl"] = row_job["OblNo"];
        //    row["JobType"] = row_job["JobType"];
        //}
        //if (jobType == "SE")
        //    row["MastType"] = "Export";
        //else if (jobType == "SI")
        //    row["MastType"] = "Import";
        //else if (jobType == "AI")
        //    row["MastType"] = "Import";
        //else if (jobType == "AE")
        //    row["MastType"] = "Export";


        //row["TotAmt"] = SafeValue.ChinaRound(amt + gstAmt, 2).ToString("0.00");
        //row["UserId"] = mast.Rows[0]["UserId"];
        //tab_mast.Rows.Add(row);


        //set.Tables.Add(tab_mast);
        //set.Tables.Add(tab_det);
        //DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
        //set.Relations.Add(r);
        //return set;
    }

}
