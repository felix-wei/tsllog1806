using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EzshipHelper
/// </summary>
public class EzshipHelper
{
    static public string GetPartyCode(object partyId)
    {

        if (SafeValue.SafeString(partyId, "").Length > 0)
        {
            string sql = "select Code from XXParty where PartyId='" + partyId + "'";
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }
    static public string GetPartyName(object partyId)
    {

        if (SafeValue.SafeString(partyId, "").Length > 0)
        {
            string sql = "select name from XXParty where PartyId='" + partyId + "'";
            return SafeValue.SafeSqlString(C2.Manager.ORManager.ExecuteScalar(sql));
        }
        return "";
    }
    static public string GetPortName(object portCode, object exValue)
    {

        string name = GetPortName(portCode);
        if (name.Length == 0)
            return SafeValue.SafeString(exValue);
        return name;
    }
    static public string GetPortName(object portCode)
    {

        if (SafeValue.SafeString(portCode, "").Length > 0)
        {
            string sql = "select name from XXPort where Code='" + portCode + "'";
            return SafeValue.SafeSqlString(C2.Manager.ORManager.ExecuteScalar(sql));
        }
        return "";
    }
    static public string GetUserName()
    {
        return HttpContext.Current.User.Identity.Name;
    }
    static public string GetUseRole()
    {
        string userName = HttpContext.Current.User.Identity.Name;
        string sql = "select Role from [user]where Name='" + userName + "'";
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
    }
    static public string GetTerm(string partyTo)
    {
        string sql1 = "select TermId from XXParty where PartyId='" + partyTo + "'";
        string term = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1), "CASH");
        return term;
    }
    static public string GetJobType(string type, string refNo)
    {
        string sql = "";
        if (type == "SI")
        {
            sql = "select JobType from SeaImportRef where RefNo='" + refNo + "'";
        }
        else
        {
            sql = "select JobType from SeaExportRef where RefNo='" + refNo + "'";
        }
        string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
        return jobType;
    }
    /// <summary>
    /// 是否可以关闭ref
    /// </summary>
    /// <param name="refType"></param>
    /// <param name="refNo"></param>
    /// <returns></returns>
    static public bool GetCloseEstInd(string refNo, string refType)
    {
        string runType = "";
        if (refType.Length > 0)
        {
            if (refType == "SIF" || refType == "SIL" || refType == "SIC")//SEA IMPORT FCL/LCL/CONSOLE
                runType = "ImportRef";
            else if (refType == "SEF" || refType == "SEL" || refType == "SEC")//SEA EXPORT FCL/LCL/CONSOLE
                runType = "EXPORTREF";
            else if (refType == "SCF" || refType == "SCL" || refType == "SCC")//SEA CrossTrade FCL/LCL/CONSOLE
                runType = "SeaCrossTrade";
            else if (refType == "SAI" || refType == "AI")//air Import
                runType = "AirImport";
            else if (refType == "SAE" || refType == "AE")//AIR EXPORT
                runType = "AirExport";
            else if (refType == "SAC" || refType == "ACT")//AIR IMPORT
                runType = "AirCrossTrade";//AIR CROSS TRADE
            else
                runType = "LocalTpt";
        }
        string sql = string.Format("Select CloseByEst from sys_parameter where RunType='{0}' and RefType='{1}'", runType, refType);
        //得到是否要根据est amount 判断关闭ref
        string closeByEst = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        if (closeByEst.ToUpper() == "N")//不根据est 判断
        {
            return true;
        }
        else
        {
            string sql_count1 = string.Format(@"select count(*) from XaArInvoiceDet where MastRefNo='{0}' and (MastType='SI'or MastType='SE') and DocType='CN'", refNo);
            string sql_count2 = string.Format(@"select count(*) from XaApPayableDet where MastRefNo='{0}' and (MastType='SI'or MastType='SE') and DocType='SC'", refNo);
           int count1 = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_count1), 0);
           int count2 = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_count2), 0);
           if (count1 > 0 && count2>0)
           {
               if (refType == "SIF" || refType == "SIL" || refType == "SIC")//SEA IMPORT FCL/LCL/CONSOLE
                   sql = string.Format(@"select count(refNo) from SeaImportRef where RefNo='{0}'
and EstSaleAmt=(select sum(case when DocType='CN' then -lineLocAmt else lineLocAmt end) from XaArInvoiceDet where MastRefNo='{0}' and MastType='SI')
and EstCostAmt=(select sum(case when DocType='SC' then -lineLocAmt else lineLocAmt end) from XaApPayableDet where MastRefNo='{0}' and MastType='SI')
", refNo);

               else if (refType == "SEF" || refType == "SEL" || refType == "SEC")//SEA EXPORT FCL/LCL/CONSOLE
                   sql = string.Format(@"select count(refNo) from SeaExportRef where RefNo='{0}'
and EstSaleAmt=(select sum(case when DocType='CN' then -lineLocAmt else lineLocAmt end) from XaArInvoiceDet where MastRefNo='{0}' and MastType='SE')
and EstCostAmt=(select sum(case when DocType='SC' then -lineLocAmt else lineLocAmt end) from XaApPayableDet where MastRefNo='{0}' and MastType='SE')
", refNo);
               else if (refType == "SCF" || refType == "SCL" || refType == "SCC")//SEA CrossTrade FCL/LCL/CONSOLE
                   sql = string.Format(@"select count(refNo) from SeaExportRef where RefNo='{0}'
and EstSaleAmt=(select sum(case when DocType='CN' then -lineLocAmt else lineLocAmt end) from XaArInvoiceDet where MastRefNo='{0}' and MastType='SE')
and EstCostAmt=(select sum(case when DocType='SC' then -lineLocAmt else lineLocAmt end) from XaApPayableDet where MastRefNo='{0}' and MastType='SE')
", refNo);
           }
            else if (refType == "SAI" || refType == "AI")//air Import
                sql = "";
            else if (refType == "SAE" || refType == "AE")//AIR EXPORT
                sql = "";
            else if (refType == "SAC" || refType == "ACT")//AIR IMPORT
                sql = "";//AIR CROSS TRADE
            else
                sql = "";
           
            if (sql.Length > 0 && SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) > 0)
            {
                return true;
            }
            else if (sql.Length == 0)
                return true;
            else
                return false;

        }
        return true;
    }
    #region account
    static public string GetCloseIndByOid(string type, string refNo)
    {
        string sql = "";
        if (type == "IV" || type == "CN" || type == "DN" || type == "CI")
        {
            sql = "Select ExportInd from XAArInvoice where Sequenceid='" + refNo + "'";
        }
        else if (type == "PL" || type == "SC" || type == "SD" || type == "VO")
        {
            sql = "Select ExportInd from XAApPayable where Sequenceid='" + refNo + "'";
        }
        else if (type == "RE" || type == "PC")
        {
            sql = "Select ExportInd from XAArReceipt where Sequenceid='" + refNo + "'";
        }
        else if (type == "PS" || type == "SR")
        {
            sql = "Select ExportInd from XAApPayment where Sequenceid='" + refNo + "'";
        }
        string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        return closeInd;
    }
    static public string[] GetAccPeriod(DateTime d)
    {
        string acPeriod = d.Month.ToString();
        string acYear = d.Year.ToString();

        string sql = "SELECT Year, Period FROM XXAccPeriod WHERE StartDate<='" + d.ToString("yyyy-MM-dd") + "' and EndDate>='" + d.ToString("yyyy-MM-dd") + "'";
        DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            acYear = dt.Rows[0][0].ToString();
            acPeriod = dt.Rows[0][1].ToString();
        }

        string[] period = { acPeriod, acYear };
        return period;
    }
    static public string GetAccArCode(string partyId, string currency)
    {
        string sql_Accode = "";
        string acCode = "";
        sql_Accode = string.Format("select ArCode from XXPartyAcc where PartyId='{0}' and CurrencyId='{1}'", partyId, currency);
        acCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_Accode));
        if (acCode == "")
        {
            sql_Accode = string.Format("select ArCode from XXPartyAcc where (PartyId='' OR PartyId is null) and CurrencyId='{0}'", currency);
            acCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_Accode));
        }
        acCode = acCode.Length > 0 ? acCode : System.Configuration.ConfigurationManager.AppSettings["LocalArCode"];

        return "10";
    }
    static public string GetAcCodeDes(object acCode)
    {
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select AcDesc from XXChartAcc where Code='{0}'", acCode)));
    }
    static public string GetAccApCode(string partyId, string currency)
    {
        string sql_Accode = "";
        string acCode = "";
        sql_Accode = string.Format("select ApCode from XXPartyAcc where PartyId='{0}' and CurrencyId='{1}'", partyId, currency);
        acCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_Accode));
        if (acCode == "")
        {
            sql_Accode = string.Format("select ApCode from XXPartyAcc where (PartyId='' OR PartyId is null) and CurrencyId='{0}'", currency);
            acCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_Accode));
        }
        acCode = acCode.Length > 0 ? acCode : System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
        return "20";
    }
    static public string GetPeroidCloseInd(string acYear, string acPeriod)
    {
        string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
        string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1), "N");
        return closeInd;
    }

    static public string GetListSalePrice(string partyId,string product)
    {
        string sql = string.Format(@"select Price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and mast.DoType='SQ' where mast.PartyId='{0}' and ProductCode='{1}'", partyId, product);
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
    }
    static public string GetListBuyPrice(string partyId, string product)
    {
        string sql = string.Format(@"select Price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and mast.DoType='PQ' where mast.PartyId='{0}' and ProductCode='{1}'", partyId, product);
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
    }
    static public string GetLastSalePrice(string partyId, string product)
    {
        string sql = string.Format(@"select top(1) price from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.PartyId='{0}' and ProductCode='{1}' and mast.DoType='SO'   order by mast.DoDate,det.Price Desc", partyId, product);
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
    }
    static public string GetLastBuyPrice(string partyId, string product)
    {
        string sql = string.Format(@"select top(1) price from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.PartyId='{0}' and ProductCode='{1}' and mast.DoType='PO'   order by mast.DoDate,det.Price Desc", partyId, product);
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
    }
    static public string GetPartyIdByDoNo(string doNo)
    {
        string sql = string.Format(@"select PartyId from wh_trans where DoNo='{0}'", doNo);
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
    }
    #endregion


    static public string GetWarehouse(object code)
    {

        if (SafeValue.SafeString(code, "").Length > 0)
        {
            string sql = "select Name from ref_warehouse where code='" + code + "'";
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }
    static public string GetSaleName(object code)
    {

        if (SafeValue.SafeString(code, "").Length > 0)
        {
            string sql = "select Name from XXSalesman where Code='" + code + "'";
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }
    static public string GetLocationName(object id)
    {

        if (SafeValue.SafeString(id, "").Length > 0)
        {
            string sql = "select Name from ref_location where id=" + id + "";
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }

    static public string GetPartyId(object partyName)
    {
        if (SafeValue.SafeString(partyName, "").Length > 0)
        {
            string value = "";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select PartyId from XXParty where Name=@name", conn);
            SqlParameter para = new SqlParameter("@name", SqlDbType.VarChar, 1000);//创建一个名为@p_user,类型为varchar,长度为20的参数。
            para.Value = partyName;    //给para赋值p.puSER。
            cmd.Parameters.Add(para);    //给cmd命令添加参数。
            value = SafeValue.SafeSqlString(cmd.ExecuteScalar());
            conn.Close();
            return value;
        }
        return "";
    }

    static public string GetPortCode(object portName)
    {
        if (SafeValue.SafeString(portName, "").Length > 0)
        {
            string sql = "select Code from XXPort where Name='" + portName + "'";
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }
    static public string GetPortAirCode(object portName)
    {
        if (SafeValue.SafeString(portName, "").Length > 0)
        {
            string sql = "select AirCode from XXPort where Name='" + portName + "'";
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }


    static public string GetDoNoByLotNo(object lotNo)
    {
        if (SafeValue.SafeString(lotNo, "").Length > 0)
        {
            string sql = string.Format("select DoNo from Wh_DoDet where LotNo='{0}' and DoType='IN'", lotNo);
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }
    static public string GetDoDateByDoNo(object doNo)
    {
        if (SafeValue.SafeString(doNo, "").Length > 0)
        {
            string sql = string.Format("select DoDate from Wh_Do where DoNo='{0}'", doNo);
            return SafeValue.SafeDateStr(C2.Manager.ORManager.ExecuteScalar(sql));
        }
        return "";
    }
    static public string GetTermCode(string id)
    {
        string term = "CASH";

        if (id.Length > 0 && SafeValue.SafeInt(id, 0) > 0)
        {
            string sql1 = "select Code from XXTerm where SequenceId=" + id + "";
            term = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1), "");
            if (term.Length == 0)
                term = "CASH";
        }
        return term;
    }
}