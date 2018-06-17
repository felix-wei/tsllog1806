using System;
using System.Data;
using System.Collections.Generic;
using System.Data;
/// <summary>
/// Summary description for C2Setup
/// </summary>
public class C2Setup
{
	public C2Setup()
	{
    }
    public static string GetNextNo(string typ)
    {
       return GetNextNo("",typ, DateTime.Today);
    }
    public static string GetMasterId(string type) {
        string sql = "select MasterId from XXsetup1 where Category='" + type + "'";
        string res =SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        return res;
    }
    //reftype 
    public static string GetNextNo(string refType,string typ,DateTime dt)
    {
        typ = GetRunType(refType,typ);
        string masterId = GetMasterId(typ);
        string sql = "select SequenceId,Prefix, Suffix, Cycle, CurrentNo,Formula from XXsetup1 where Category='" + typ + "'";
        if (masterId.Length > 0)
            sql = "select mast.SequenceId,det.Prefix, det.Suffix, mast.Cycle, mast.CurrentNo,mast.Formula from XXsetup1 mast inner join XXSetup1 det on mast.Category=det.MasterId where det.Category='" + typ + "'";
       
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 1)
        {
            string mastId = SafeValue.SafeString(tab.Rows[0]["SequenceId"]);
            
            string prefix = SafeValue.SafeString(tab.Rows[0]["Prefix"]);
            string suffix = SafeValue.SafeString(tab.Rows[0]["Suffix"]);
            string cycle = SafeValue.SafeString(tab.Rows[0]["Cycle"]).ToLower();
            string formula = SafeValue.SafeString(tab.Rows[0]["Formula"], "D");
            string no = "";
            if (cycle == "c")//
            {
                no = SafeValue.SafeString(tab.Rows[0]["CurrentNo"]);
            }
            else if (cycle == "y")
            {
                string sql1 = string.Format("select YearCurrentNo from XXSetup2 where MastId='{0}' and Year='{1}'", mastId, dt.Year);
                no = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1));
            }
            else if (cycle == "m")
            {
                
                string sql1 = string.Format("select Mth{2}CurrentNo from XXSetup2 where MastId='{0}' and Year='{1}'", mastId, dt.Year, dt.ToString("MM"));
                no = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1));
            }
			else if (cycle == "d")
            {
                string sql1 = string.Format("select CurrentNo from XXSetup3 where MastId='{0}' and Year='{1}' and Month='{2}' and Day='{3}'", mastId, dt.Year, dt.ToString("MM"), dt.ToString("dd"));
                no = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1),"0");
            }
            string[] arr = prefix.Split(',');
            string prefixStr = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToLower() == "yy")
                {
                    prefixStr += dt.ToString("yy");
                }
                else if (arr[i].ToLower() == "yyyy")
                {
                    prefixStr += dt.ToString("yyyy");
                }
                else if (arr[i].ToUpper() == "MM")
                {
                    prefixStr += dt.ToString("MM");
                }
                else if (arr[i].ToUpper() == "MMM")
                {
                    prefixStr += dt.ToString("MMM");
                }
                else if (arr[i].ToLower() == "dd")
                {
                    prefixStr += dt.ToString("dd");
                }
                else
                {
                    prefixStr += arr[i];
                }
            }
            arr = suffix.Split(',');
            string suffixStr = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToLower() == "yy")
                {
                    suffixStr += dt.ToString("yy");
                }
                else if (arr[i].ToLower() == "yyyy")
                {
                    suffixStr += dt.ToString("yyyy");
                }
                else if (arr[i].ToUpper() == "MM")
                {
                    suffixStr += dt.ToString("MM");
                }
                else if (arr[i].ToUpper() == "MMM")
                {
                    suffixStr += dt.ToString("MMM");
                }
                else if (arr[i].ToUpper() == "dd")
                {
                    suffixStr += dt.ToString("dd");
                }
                else
                {
                    suffixStr += arr[i];
                }
            }
            return string.Format("{0}{1}{2}", prefixStr, (SafeValue.SafeInt(no, 0) + 1).ToString(formula), suffixStr);
        }
        return "";

    }

    public static void SetNextNo(string typ, string cnt)
    {
        SetNextNo("",typ, cnt, DateTime.Today);
    }
    
    public static void SetNextNo(string refType, string typ, string cnt, DateTime dt)
    {
        typ = GetRunType(refType, typ);
        string masterId = GetMasterId(typ);
        if (masterId.Length > 0)
        {
            SetNextNo(refType, masterId, cnt, dt);
        }
        string sql = "select  SequenceId,Prefix, Suffix, Cycle, CurrentNo,Formula from XXsetup1 where Category='" + typ + "'";
        string sql_update = "";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 1)
        {
            string mastId = SafeValue.SafeString(tab.Rows[0]["SequenceId"]);
            string prefix = SafeValue.SafeString(tab.Rows[0]["Prefix"]);
            string suffix = SafeValue.SafeString(tab.Rows[0]["Suffix"]);
            string cycle = SafeValue.SafeString(tab.Rows[0]["Cycle"]).ToLower();
            string formula = SafeValue.SafeString(tab.Rows[0]["Formula"], "D");
            int no = 0;
            if (cycle == "c")//
            {
                no = SafeValue.SafeInt(tab.Rows[0]["CurrentNo"], 0) + 1;
                sql_update = string.Format("Update XXsetup1 set currentNo='{1}' where SequenceId='{0}'", mastId, no);
                C2.Manager.ORManager.ExecuteCommand(sql_update);
            }
            else if (cycle == "y")
            {
                string sql1 = string.Format("select YearCurrentNo from XXSetup2 where MastId='{0}' and Year='{1}'", mastId, dt.Year);
                no = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql1), 0) + 1;
                sql_update = string.Format("Update XXsetup2 set YearCurrentNo='{2}' where MastId='{0}' and Year='{1}'", mastId, dt.Year, no);
                C2.Manager.ORManager.ExecuteCommand(sql_update);
                sql_update = string.Format("Update XXsetup1 set currentNo='{1}' where SequenceId='{0}'", mastId, no);
                C2.Manager.ORManager.ExecuteCommand(sql_update);
            }
            else if (cycle == "m")
            {
                string sql1 = string.Format("select Mth{2}CurrentNo from XXSetup2 where MastId='{0}' and Year='{1}'", mastId, dt.Year, dt.ToString("MM"));
                no = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql1), 0) + 1;
                sql_update = string.Format("Update XXsetup2 set  Mth{3}CurrentNo='{2}' where MastId='{0}' and Year='{1}'", mastId, dt.Year, no, dt.ToString("MM"));
                C2.Manager.ORManager.ExecuteCommand(sql_update);
                sql_update = string.Format("Update XXsetup1 set currentNo='{1}' where SequenceId='{0}'", mastId, no);
                C2.Manager.ORManager.ExecuteCommand(sql_update);
            }
			else if (cycle == "d")
            {
                string sql1 = string.Format("select CurrentNo from XXSetup3 where MastId='{0}' and Year='{1}' and Month='{2}' and Day='{3}'", mastId, dt.Year, dt.ToString("MM"), dt.ToString("dd"));
				
                no = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql1), 0);

                if (no == 0)
                {
                    no = no + 1;
                    string sql1_insert = string.Format("Insert Into XXSetup3 (MastId, Year, Month, Day, CurrentNo) Values(@MastId, @Year, @Month, @Day, @CurrentNo)");
                    List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@MastId", mastId, SqlDbType.NVarChar));
                    list.Add(new ConnectSql_mb.cmdParameters("@Year", dt.Year, SqlDbType.NVarChar));
                    list.Add(new ConnectSql_mb.cmdParameters("@Month", dt.ToString("MM"), SqlDbType.NVarChar));
                    list.Add(new ConnectSql_mb.cmdParameters("@Day", dt.ToString("dd"), SqlDbType.NVarChar));
                    list.Add(new ConnectSql_mb.cmdParameters("@CurrentNo", no, SqlDbType.NVarChar));
                    ConnectSql_mb.ExecuteNonQuery(sql1_insert, list);
                }
                else {
                    no = no + 1;
                }
                sql_update = string.Format("Update XXSetup3 set  CurrentNo='{2}' where MastId='{0}' and Year='{1}' and Month='{3}' and Day='{4}'", mastId, dt.Year, no, dt.ToString("MM"), dt.ToString("dd"));
                C2.Manager.ORManager.ExecuteCommand(sql_update);

                sql1 = string.Format("select sum(CONVERT(int,CurrentNo)) as CurrentNo  from XXSetup3 where MastId='{0}'", mastId);
                no = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql1), 0);
                sql_update = string.Format("Update XXsetup1 set currentNo='{1}' where SequenceId='{0}'", mastId, no);
                C2.Manager.ORManager.ExecuteCommand(sql_update);
            }
        }
    }

    public static string GetSubNo(string refNo,string jobType)
    {
        string sql_no = "";
        if(jobType=="SI")
        sql_no = "select max(JobNo) from SeaImport where RefNo='" + refNo + "'";
        if (jobType == "SE")
            sql_no = "select max(JobNo) from SeaExport where RefNo='" + refNo + "'";
        if (jobType == "AI")
            sql_no = "select max(JobNo) from air_job where RefNo='" + refNo + "'and RefType='AI'";
        if (jobType == "AE")
            sql_no = "select max(JobNo) from air_job where RefNo='" + refNo + "'and RefType='AE'";
        if (jobType == "ACT")
            sql_no = "select max(JobNo) from air_job where RefNo='" + refNo + "'and RefType='ACT'";
        if (sql_no.Length > 0)
        {
            string ret = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_no), "");
            int newN = 1;
            if (ret != "")
            {
                string part2 = ret.Substring(refNo.Length);
                newN = SafeValue.SafeInt(part2, 0) + 1;
            }
            return string.Format("{0}{1:00}", refNo, newN);
        }
        return "";
    }

    private static string GetRunType(string refType, string typ)
    {
        string sql = "select ActualRuntype from sys_parameter where RefType='"+refType+"' and RunType='"+typ+"'";
        string runType=SafeValue.SafeString( C2.Manager.ORManager.ExecuteScalar(sql));
        if(runType.Length<1){
             runType = typ;
        }
        return runType;
    }
    public static string GetNextSchNo(string refType, string typ, DateTime dt)
    {
        typ = GetRunType(refType, typ);

        string sql = "select  SequenceId,Prefix, Suffix, Cycle, CurrentNo,Formula from XXsetup1 where Category='" + typ + "'";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 1)
        {
            string mastId = SafeValue.SafeString(tab.Rows[0]["SequenceId"]);
            string prefix = SafeValue.SafeString(tab.Rows[0]["Prefix"]);
            string suffix = SafeValue.SafeString(tab.Rows[0]["Suffix"]);
            string cycle = SafeValue.SafeString(tab.Rows[0]["Cycle"]).ToLower();
            string formula = SafeValue.SafeString(tab.Rows[0]["Formula"], "D");
            string no = "";
            if (cycle == "c")//
            {
                no = SafeValue.SafeString(tab.Rows[0]["CurrentNo"]);
            }
            else if (cycle == "y")
            {
                string sql1 = string.Format("select YearCurrentNo from XXSetup2 where MastId='{0}' and Year='{1}'", mastId, dt.Year);
                no = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1));
            }
            else if (cycle == "m")
            {
                string sql1 = string.Format("select Mth{2}CurrentNo from XXSetup2 where MastId='{0}' and Year='{1}'", mastId, dt.Year, dt.ToString("MM"));
                no = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1));
            }
            string[] arr = prefix.Split(',');
            string prefixStr = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToLower() == "yy")
                {
                    prefixStr += dt.ToString("yy");
                }
                else if (arr[i].ToLower() == "yyyy")
                {
                    prefixStr += dt.ToString("yyyy");
                }
                else if (arr[i].ToUpper() == "MM")
                {
                    prefixStr += dt.ToString("MM");
                }
                else if (arr[i].ToUpper() == "MMM")
                {
                    prefixStr += dt.ToString("MMM");
                }
                else
                {
                    prefixStr += arr[i];
                }
            }
            arr = suffix.Split(',');
            string suffixStr = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToLower() == "yy")
                {
                    suffixStr += dt.ToString("yy");
                }
                else if (arr[i].ToLower() == "yyyy")
                {
                    suffixStr += dt.ToString("yyyy");
                }
                else if (arr[i].ToUpper() == "MM")
                {
                    suffixStr += dt.ToString("MM");
                }
                else if (arr[i].ToUpper() == "MMM")
                {
                    suffixStr += dt.ToString("MMM");
                }
                else
                {
                    suffixStr += arr[i];
                }
            }
            return string.Format("{0}{1}{2}", prefixStr, dt.Day,(SafeValue.SafeInt(no, 0) + 1).ToString(formula));
        }
        return "";

    }
}
