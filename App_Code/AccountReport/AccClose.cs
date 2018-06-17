using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using C2;
using Wilson.ORMapper;

/// <summary>
/// Summary description for AccClose
/// </summary>
public class AccClose
{
	public AccClose()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string ClosePeriod(bool isAddTrans)
    {
        string s = "";
        string sql = "SELECT SequenceId, Year, Period FROM XXAccPeriod WHERE (CloseInd = 'N') ORDER BY Year, Period";
        DataTable tab = Helper.Sql.List(sql);
        if (tab.Rows.Count > 0)
        {
            try
            {
                int year = SafeValue.SafeInt(tab.Rows[0]["Year"], 0);
                int period = SafeValue.SafeInt(tab.Rows[0]["Period"], 1);

               //summary chart of account
                string sql_chart = "SELECT Code, AcDesc, AcType, AcDorc FROM XXChartAcc ORDER BY Code";
                DataTable tab_chart = Helper.Sql.List(sql_chart);
                for (int i = 0; i < tab_chart.Rows.Count; i++)
                {

                    string code = tab_chart.Rows[i]["Code"].ToString();
                    string codeDes = tab_chart.Rows[i]["AcDesc"].ToString();
                    string acType = tab_chart.Rows[i]["AcType"].ToString();//b,p,r only =b ,transfet amt to next year
                    string acDorc = tab_chart.Rows[i]["AcDorc"].ToString();//cr/db
                    decimal dbAmt = 0;
                    decimal crAmt = 0;
                    string sql_sum = "";
                    DataTable tab_current = new DataTable();
   
                
                        sql_sum = string.Format(@"SELECT SUM(CurrencyCrAmt) AS CrAmt,SUM(CurrencyDbAmt) AS DbAmt FROM XAGlEntryDet WHERE (AcYear = '{0}') AND (AcPeriod = '{1}' and AcCode='{2}')", year, period, code);
                        tab_current = Helper.Sql.List(sql_sum);
               
                    if (tab_current.Rows.Count == 1)
                    {
                        dbAmt = SafeValue.SafeDecimal(tab_current.Rows[0]["DbAmt"], 0);
                        crAmt = SafeValue.SafeDecimal(tab_current.Rows[0]["CrAmt"], 0);
                    }
                    decimal transAmt = 0;

                    if (acDorc == "CR")
                    {
                        transAmt = crAmt - dbAmt;
                    }
                    else
                    {
                        transAmt = dbAmt - crAmt;
                    }
                    if (isAddTrans)
                        UpdateAcStatus_1(year, period, code, acType, codeDes, acDorc, transAmt);
                    else
                        UpdateAcStatus(year, period, code, acType, codeDes, acDorc, transAmt);

                }
                //update acc statust
                sql = string.Format("Update XXAccPeriod Set CloseInd = 'Y' where Year='{0}' and Period='{1}'", year, period);
                if (period == 12)
                {
                    //update 3001
                    if (isAddTrans)
                        UpdateAcReturnEarning_1(year);
                    else
                        UpdateAcReturnEarning(year, 12);
                }
                Manager.ORManager.ExecuteCommand(sql);
                s= "Close Success";
            }
            catch (Exception ex)
            {
               s= ex.Message + ex.StackTrace;
            }
        }
        return s;
    }

    public static string OpenPeriod(bool isAddTrans)
    {
        string s = "";
        string sql = "SELECT SequenceId, Year, Period FROM XXAccPeriod WHERE (CloseInd = 'Y') ORDER BY Year desc, Period desc";
        DataTable tab = Helper.Sql.List(sql);
        if (tab.Rows.Count > 0)
        {
            try
            {
                string year = tab.Rows[0]["Year"].ToString();
                string period = tab.Rows[0]["Period"].ToString();
                string firstYear = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["AccountFirstYear"], "2009");

                if (year == firstYear && (period == "13" || period == "12"))
                {
                    s = "Can't open, this is the last period";
                }
                else
                {
                    //update acc statust
                    sql = string.Format("Update XXAccPeriod Set CloseInd = 'N' where Year='{0}' and Period='{1}'", year, period);
                    Manager.ORManager.ExecuteCommand(sql);

                    sql = string.Format("Update XXAccStatus Set  CurrBal=0, CloseBal=0 where Year = '{0}' AND AcPeriod = '{1}'", year, period);
                    Manager.ORManager.ExecuteCommand(sql);
                    if (period == "12")
                    {
                        year = (SafeValue.SafeInt(year, 0) + 1).ToString();
                        if (isAddTrans && period == "12")
                        {
                            string sql1 = string.Format("select GlNo from XXAccPeriod where Year='{0}' and Period='1'", year);
                            string oldGlNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1));
                            sql1 = string.Format("delete from xaglentrydet where glno=(select sequenceid from xaglentry where docNo='{0}' and DocType='GE')", oldGlNo);
                            Manager.ORManager.ExecuteCommand(sql1);
                        }
                        period = "1";
                    }
                    else
                    {
                        period = (SafeValue.SafeInt(period, 0) + 1).ToString();
                    }

                    sql = string.Format("delete XXAccStatus where Year = '{0}' AND AcPeriod = '{1}'", year, period);
                    Manager.ORManager.ExecuteCommand(sql);
                    s= "Open Success";
                }
            }
            catch (Exception ex)
            {
                s= ex.Message;
            }
        }
        return s;
    }
    private static void UpdateAcStatus(int year, int period, string acCode, string acType, string acDesc, string acDorc, decimal currentAmt)
    {
        OPathQuery query = new OPathQuery(typeof(XXAccStatus), string.Format("Year = '{0}' AND AcPeriod = '{1}' and AcCode='{2}'", year, period, acCode));
        XXAccStatus acStatus = Manager.ORManager.GetObject(query) as XXAccStatus;
        if (acStatus == null)
        {
            acStatus = new XXAccStatus();
            acStatus.Year = Convert.ToInt32(year);
            acStatus.AcCode = acCode;
            acStatus.AcDesc = acDesc;
            acStatus.AcDorc = acDorc;
            acStatus.AcPeriod = Convert.ToInt32(period);
            acStatus.CloseBal = currentAmt;
            acStatus.CurrBal = currentAmt;
            acStatus.OpenBal = 0;

            Manager.ORManager.StartTracking(acStatus, InitialState.Inserted);
            Manager.ORManager.PersistChanges(acStatus);
        }
        else
        {
            acStatus.CurrBal = currentAmt;
            acStatus.CloseBal = acStatus.OpenBal + currentAmt;
            Manager.ORManager.StartTracking(acStatus, InitialState.Updated);
            Manager.ORManager.PersistChanges(acStatus);
        }

        if (period == 12)
        {
            year = year + 1;
            period = 1;
        }
        else
            period = period + 1;
        query = new OPathQuery(typeof(XXAccStatus), string.Format("Year = '{0}' AND AcPeriod = '{1}' and AcCode='{2}'", year, period, acCode));
        XXAccStatus acStatus1 = Manager.ORManager.GetObject(query) as XXAccStatus;
        if (acStatus1 == null)
        {
            acStatus1 = new XXAccStatus();
            acStatus1.Year = Convert.ToInt32(year);
            acStatus1.AcCode = acCode;
            acStatus1.AcDesc = acDesc;
            acStatus1.AcDorc = acDorc;
            acStatus1.AcPeriod = Convert.ToInt32(period);
            acStatus1.CloseBal = 0;
            acStatus1.CurrBal = 0;
            if (period == 1)
            {
                if (acType == "B")
                    acStatus1.OpenBal = acStatus.CloseBal;
                else if (acType == "R")
                    acStatus1.OpenBal = acStatus.CloseBal;
                else
                    acStatus1.OpenBal = 0;
            }
            else
            {
                acStatus1.OpenBal = acStatus.CloseBal;
            }

            Manager.ORManager.StartTracking(acStatus1, InitialState.Inserted);
            Manager.ORManager.PersistChanges(acStatus1);
        }
        else
        {
            if (period == 1)
            {
                if (acType == "B")
                    acStatus1.OpenBal = acStatus.CloseBal;
                else if (acType == "R")
                    acStatus1.OpenBal = acStatus.CloseBal;
                else if (acType == "P")
                {
                    string sql = string.Format("Update XXAccStatus Set ");
                }
                else
                    acStatus1.OpenBal = 0;
            }
            else
            {
                acStatus1.OpenBal = acStatus.CloseBal;
            }
            Manager.ORManager.StartTracking(acStatus1, InitialState.Updated);
            Manager.ORManager.PersistChanges(acStatus1);
        }
    }

    private static void UpdateAcReturnEarning(int year, int period)
    {
        string sql = string.Format(@"SELECT ac.Code,ac.AcType, ac.AcSubType, sta.OpenBal, sta.CurrBal, sta.CloseBal
FROM XXChartAcc AS ac INNER JOIN XXAccStatus AS sta ON ac.Code = sta.AcCode
WHERE ((ac.AcType = 'P') or ac.AcType='R') AND (sta.Year = '{0}') AND (sta.AcPeriod = '12')", year);
        decimal amt = 0;
        DataTable tab = Helper.Sql.List(sql);
        if (tab.Rows.Count > 0)
        {
            string code = C2.Manager.ORManager.ExecuteScalar("SELECT Code FROM XXChartAcc WHERE (AcType = 'R')").ToString();
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                DataRow row = tab.Rows[i];
                string subType = row["AcSubType"].ToString();
                decimal closeBal = SafeValue.SafeDecimal(row["CloseBal"], 0);
                if (row["AcType"].ToString() == "R")
                {
                    amt += closeBal;
                    code = row["Code"].ToString();
                }
                else
                {
                    if (subType == "S")
                    {
                        amt += closeBal;
                    }
                    else if (subType == "C")
                    {
                        amt -= closeBal;
                    }
                    else if (subType == "O")
                    {
                        amt -= closeBal;
                    }
                    else
                    {
                        amt -= closeBal;
                    }
                }
            }
            sql = string.Format("update XXAccStatus set OpenBal='{3}' where Year = '{0}' AND AcPeriod = '{1}' and AcCode='{2}'", year + 1, 1, code, amt);

            Manager.ORManager.ExecuteCommand(sql);
        }
    }


    private static void UpdateAcStatus_1(int year, int period, string acCode, string acType, string acDesc, string acDorc, decimal currentAmt)
    {
        OPathQuery query = new OPathQuery(typeof(XXAccStatus), string.Format("Year = '{0}' AND AcPeriod = '{1}' and AcCode='{2}'", year, period, acCode));
        XXAccStatus acStatus = Manager.ORManager.GetObject(query) as XXAccStatus;
        if (acStatus == null)
        {
            acStatus = new XXAccStatus();
            acStatus.Year = Convert.ToInt32(year);
            acStatus.AcCode = acCode;
            acStatus.AcDesc = acDesc;
            acStatus.AcDorc = acDorc;
            acStatus.AcPeriod = Convert.ToInt32(period);
            acStatus.CloseBal = currentAmt;
            acStatus.CurrBal = currentAmt;
            acStatus.OpenBal = 0;

            Manager.ORManager.StartTracking(acStatus, InitialState.Inserted);
            Manager.ORManager.PersistChanges(acStatus);
        }
        else
        {
            acStatus.CurrBal = currentAmt;
            acStatus.CloseBal = acStatus.OpenBal + currentAmt;
            Manager.ORManager.StartTracking(acStatus, InitialState.Updated);
            Manager.ORManager.PersistChanges(acStatus);
        }

        if (period == 12)
        {
            year = year + 1;
            period = 1;
        }
        else
            period = period + 1;
        query = new OPathQuery(typeof(XXAccStatus), string.Format("Year = '{0}' AND AcPeriod = '{1}' and AcCode='{2}'", year, period, acCode));
        XXAccStatus acStatus1 = Manager.ORManager.GetObject(query) as XXAccStatus;
        if (acStatus1 == null)
        {
            acStatus1 = new XXAccStatus();
            acStatus1.Year = Convert.ToInt32(year);
            acStatus1.AcCode = acCode;
            acStatus1.AcDesc = acDesc;
            acStatus1.AcDorc = acDorc;
            acStatus1.AcPeriod = Convert.ToInt32(period);
            acStatus1.CloseBal = 0;
            acStatus1.CurrBal = 0;
            acStatus1.OpenBal = acStatus.CloseBal;

            Manager.ORManager.StartTracking(acStatus1, InitialState.Inserted);
            Manager.ORManager.PersistChanges(acStatus1);
        }
        else
        {
            acStatus1.OpenBal = acStatus.CloseBal;
            Manager.ORManager.StartTracking(acStatus1, InitialState.Updated);
            Manager.ORManager.PersistChanges(acStatus1);
        }
    }
    private static void UpdateAcReturnEarning_1(int year)
    {
        // create a new gl year=new year, period=1, 
        //actype=p, 
		return;
        string currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        C2.XAGlEntry inv = new XAGlEntry();
        string sql1 = string.Format("select GlNo from XXAccPeriod where Year='{0}' and Period='1'", year + 1);
        string oldGlNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1));
        if (oldGlNo.Length > 0)
        {
            OPathQuery query = new OPathQuery(typeof(XAGlEntry), string.Format("DocNo = '{0}' AND DocType = 'GE'", oldGlNo));
            inv = Manager.ORManager.GetObject(query) as XAGlEntry;
        }
        else
        {
            inv.ArApInd = "GL";
            inv.DocNo = C2Setup.GetNextNo("GLENTRY");
            inv.DocType = "GE";
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            inv.CrAmt = 0;
            inv.CurrencyCrAmt = 0;
            inv.CurrencyDbAmt = 0;
            inv.CurrencyId = currency;
            inv.ExRate = 1;
            inv.DbAmt = 0;
            inv.PostInd = "N";
            inv.Remark = "";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.PartyTo = "";
            inv.OtherPartyName = "";
            inv.DocDate = new DateTime(year + 1, SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountFirstMonth"], 1), 1);
            inv.AcYear = year + 1;
            inv.AcPeriod = 1;
            try
            {
                inv.PostDate = DateTime.Now;
                inv.EntryDate = DateTime.Now;
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo("GLENTRY", inv.DocNo);
            }
            catch(Exception ex)
            { throw new Exception(ex.Message + ex.StackTrace); }
        }
		//throw new Exception(inv.DocNo);
        string sql = string.Format(@"SELECT ac.Code,ac.AcDorc,ac.AcType, ac.AcSubType, sta.OpenBal, sta.CurrBal, sta.CloseBal
FROM XXChartAcc AS ac INNER JOIN XXAccStatus AS sta ON ac.Code = sta.AcCode
WHERE ((ac.AcType = 'P') or ac.AcType='R') AND (sta.Year = '{0}') AND (sta.AcPeriod = '1') and sta.OpenBal<>0", year + 1);
        decimal amt = 0;
        DataTable tab = Helper.Sql.List(sql);
        string code = C2.Manager.ORManager.ExecuteScalar("SELECT Code FROM XXChartAcc WHERE (AcType = 'R')").ToString();
        int index = 0;
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            DataRow row = tab.Rows[i];

            string subType = row["AcSubType"].ToString();
            decimal closeBal = SafeValue.SafeDecimal(row["OpenBal"], 0);
            if (row["AcType"].ToString() == "R")
            {
                //amt += closeBal;
                continue;
            }
            else
            {
                if (subType == "S")
                {
                    amt += closeBal;
                }
                else
                {
                    amt -= closeBal;
                }
            }

            string dbCrInd = row["AcDorc"].ToString();
            decimal lineAmt = SafeValue.SafeDecimal(row["OpenBal"], 0);

            if (dbCrInd == "DB")
            {
                dbCrInd = "CR";
            }
            else if (dbCrInd == "CR")
            {
                dbCrInd = "DB";
            }
            else
                continue;
            index++;
            C2.XAGlEntryDet invDet = new XAGlEntryDet();
            invDet.AcCode = row["Code"].ToString();
            invDet.AcPeriod = 1;
            invDet.AcSource = dbCrInd;
            invDet.AcYear = year + 1;
            invDet.ArApInd = inv.ArApInd;
            invDet.CrAmt = 0;
            invDet.CurrencyCrAmt = 0;
            invDet.CurrencyDbAmt = 0;
            invDet.DbAmt = 0;
            if (invDet.AcSource == "DB")
            {
                invDet.CurrencyDbAmt = lineAmt;
                invDet.DbAmt = lineAmt;
            }
            else
            {
                invDet.CrAmt = lineAmt;
                invDet.CurrencyCrAmt = lineAmt;
            }
            invDet.DocNo = inv.DocNo;
            invDet.DocType = inv.DocType;
            invDet.CurrencyId = currency;
            invDet.ExRate = 1;
            invDet.GlLineNo = index;
            invDet.GlNo = inv.SequenceId;
            invDet.Remark = "";
            C2.Manager.ORManager.StartTracking(invDet, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(invDet);
        }
        C2.XAGlEntryDet invDet_r = new XAGlEntryDet();
        invDet_r.AcCode = code;
        invDet_r.AcPeriod = 1;
        invDet_r.AcSource = "CR";
        invDet_r.AcYear = year + 1;
        invDet_r.ArApInd = "GE";
        invDet_r.CrAmt = amt;
        invDet_r.CurrencyCrAmt = amt;
        invDet_r.DbAmt = 0;
        invDet_r.CurrencyDbAmt = 0;
        invDet_r.CurrencyId = currency;
        invDet_r.DocNo = inv.DocNo;
        invDet_r.DocType = inv.DocType;
        invDet_r.ExRate = 1;
        invDet_r.GlLineNo =index + 1;
        invDet_r.GlNo = inv.SequenceId;
        invDet_r.Remark = "";
        C2.Manager.ORManager.StartTracking(invDet_r, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(invDet_r);

        sql = string.Format("Update XXAccPeriod Set GlNo='{1}' where Year='{0}' and Period='1'", year + 1, inv.DocNo);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }


}