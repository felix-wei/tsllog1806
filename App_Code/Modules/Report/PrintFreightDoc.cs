using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Wilson.ORMapper;
using C2;


/// <summary>
/// PrintFreightDoc 的摘要说明
/// </summary>
public class PrintFreightDoc
{
    public PrintFreightDoc()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static DataSet PrintImpHaulier(string orderNo)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        ObjectQuery query1 = new ObjectQuery(typeof(C2.CtmJobDet1), "JobNo='" + orderNo + "'", "ContainerNo");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);

        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");

        mast.Columns.Add("JobOrderNo");
        mast.Columns.Add("Haulier");
        mast.Columns.Add("OceanBl");
        mast.Columns.Add("Yard");
        mast.Columns.Add("Carrier");
        mast.Columns.Add("CrNo");
        mast.Columns.Add("Contact1");
        mast.Columns.Add("Contact2");
        mast.Columns.Add("Tel1");
        mast.Columns.Add("Fax1");
        mast.Columns.Add("Terminal");
        mast.Columns.Add("Permit");
        mast.Columns.Add("LastDate");
        mast.Columns.Add("Portnet");
        mast.Columns.Add("Ft20");
        mast.Columns.Add("Ft40");
        mast.Columns.Add("Ft45");
        mast.Columns.Add("DeliveryTo");
        mast.Columns.Add("VesselNo");
        mast.Columns.Add("VoyNo");
        mast.Columns.Add("Etd");
        mast.Columns.Add("Eta");
        mast.Columns.Add("PortLoad");
        mast.Columns.Add("ByWho");
        mast.Columns.Add("ImpRefNo");
        mast.Columns.Add("CustName");
        mast.Columns.Add("AgentName");
        mast.Columns.Add("UserData2");
        mast.Columns.Add("UserData3");
        mast.Columns.Add("UserData4");
        mast.Columns.Add("Outside");
        mast.Columns.Add("Note1");
        mast.Columns.Add("Note2");
        mast.Columns.Add("StampUser");

        det.Columns.Add("Idx");
        det.Columns.Add("JobOrderNo");
        det.Columns.Add("ContNo");
        det.Columns.Add("SealNo");
        det.Columns.Add("FtSize");
        det.Columns.Add("FtRemark");
        det.Columns.Add("FtType");
        det.Columns.Add("FtKgs");
        det.Columns.Add("FtQty");
        det.Columns.Add("FtPack");
        det.Columns.Add("FtTruckIn");
        det.Columns.Add("FtCbm");
        det.Columns.Add("FtNom");

        #endregion

        DataRow row = mast.NewRow();
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.HaulierId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        row["Haulier"] = "";
        row["CrNo"] = "";
        row["Contact1"] = "";
        row["Contact2"] = "";
        row["Tel1"] = "";
        row["Fax1"] = "";
        if (tab_hauler.Rows.Count > 0)
        {
            row["Haulier"] = tab_hauler.Rows[0]["Name"];
            row["CrNo"] = tab_hauler.Rows[0]["CrNo"].ToString();
            row["Contact1"] = tab_hauler.Rows[0]["Contact1"].ToString();
            row["Contact2"] = tab_hauler.Rows[0]["Contact2"].ToString();
            row["Tel1"] = tab_hauler.Rows[0]["Tel1"].ToString();
            row["Fax1"] = tab_hauler.Rows[0]["Fax1"].ToString();
        }

        row["JobOrderNo"] = orderNo;
        row["VesselNo"] = obj.Vessel;
        row["VoyNo"] = obj.Voyage;
        row["DeliveryTo"] = Helper.Sql.One(string.Format(@"select Address from xxparty where PartyId='{0}'", obj.ClientId));
        row["Ft20"] = 0;
        row["Ft40"] = 0;
        row["Ft45"] = 0;

        string sql = "select name from xxparty where partyid='" + SafeValue.SafeInt(obj.ClientId, 0) + "'";
        row["CustName"] = SafeValue.SafeString(Helper.Sql.One(sql), "");

        sql = "select name from xxparty where partyid='" + SafeValue.SafeInt(obj.CarrierId, 0) + "'";
        row["AgentName"] = SafeValue.SafeString(Helper.Sql.One(sql), "");

        row["Etd"] = obj.EtdDate.ToString("dd/MM/yyyy");
        row["Eta"] = obj.EtaDate.ToString("dd/MM/yyyy");
        sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pol, "") + "'";
        //row["PortLoad"] = SafeValue.SafeString(Helper.Sql.One(sql), obj.Pol);

        row["Carrier"] = obj.CarrierId;
        row["OceanBl"] = "";
        row["Yard"] = obj.YardRef;
        row["ByWho"] = obj.CreateBy;
        row["ImpRefNo"] = obj.ClientRefNo;
        row["UserData2"] = "";
        row["Note1"] = obj.SpecialInstruction;
        row["StampUser"] = obj.CreateBy;

        mast.Rows.Add(row);

        for (int i = 0; i < objSet1.Count; i++)
        {
            C2.CtmJobDet1 cont = objSet1[i] as C2.CtmJobDet1;
            DataRow row1 = det.NewRow();
            string contType = cont.ContainerType;
            row1["JobOrderNo"] = orderNo;
            row1["ContNo"] = cont.ContainerNo;
            row1["SealNo"] = cont.SealNo;
            string ftSize = "";
            if (contType.Substring(0, 2) == "20")
                ftSize = "20";
            if (contType.Substring(0, 2) == "40")
                ftSize += "40";
            if (contType.Substring(0, 2) == "45")
                ftSize += "45";
            row1["FtSize"] = ftSize;
            row1["FtType"] = "";

            row1["FtQty"] = cont.Qty;
            row1["FtKgs"] = cont.Weight.ToString("0");
            row1["FtCbm"] = cont.Volume.ToString("#,##0.00");
            row1["FtTruckIn"] = "TRUCK-IN:" + cont.ScheduleDate.ToString("dd-MMM");
            det.Rows.Add(row1);
        }
        set.Tables.Add(mast);
        set.Tables.Add(det);
        set.Relations.Add("", mast.Columns["JobOrderNo"], det.Columns["JobOrderNo"]);
        return set;
    }
    #region print tallsheet -import

    public static DataSet DsImpTs_Pre(string orderNo, string userName)
    {
        DataTable mast = InitTsMastDataTable();
        DataTable details = InitTsDetailDataTable();

        string sql_Order = string.Format("select Vessel,Voyage,CarrierBkgNo,EtaDate,ClientContact,ClientRefNo,Pol,PartyId,AdditionalRemark,CreateBy,IssuedBy from ctm_job where JobNo='{0}'", orderNo);
        DataTable tab_Order = Helper.Sql.List(sql_Order);
        if (tab_Order.Rows.Count == 1)
        {
            DataRow row_Order = tab_Order.Rows[0];
            #region tally sheet
            DataRow row = mast.NewRow();
            row["JobOrderNo"] = orderNo;
            row["VesselNo"] = row_Order["Vessel"];
            row["BookingNo"] = row_Order["CarrierBkgNo"];
            row["VoyNo"] = row_Order["Voyage"];


            row["Eta"] = SafeValue.SafeDateStr(row_Order["EtaDate"]);
            row["ByWho"] = row_Order["ClientContact"];
            row["ImpRefNo"] = row_Order["ClientRefNo"];
            string sql = "select name from XXPort where Code='" + SafeValue.SafeString(row_Order["Pol"], "") + "'";
            row["PortLoad"] = SafeValue.SafeString(row_Order["Pol"], ""); //SafeValue.SafeString(Helper.Sql.One(sql), "");
            sql = "select name from xxparty where partyid='" + row_Order["PartyId"] + "'";
            row["CustName"] = row_Order["PartyId"]; //SafeValue.SafeString(Helper.Sql.One(sql), "");
            row["Condition"] = row_Order["AdditionalRemark"];
            row["UserData1"] = row_Order["CreateBy"];
            row["IssueBy"] = row_Order["IssuedBy"];
            row["UserName"] = userName;
            mast.Rows.Add(row);

            string sql_JobDet = string.Format(@"select DoNo,CargoType,ContNo,SealNo,IsDg,BookingNo,HblNo,Marking2
,Qty,UomCode,PackTypeOrig,Volume,Weight,QtyOrig,VolumeOrig,WeightOrig, POD
,Remark1,DgClass,Desc2 from job_house where JobNo='{0}' order by ContNo,LineId,IsDg,DoNo", orderNo);
            DataTable tab_Det = Helper.Sql.List(sql_JobDet);
            int g0 = 0;
            int g1 = 0;
            int g2 = 0;
            int p = 0;
            string lastContNo = "";

            bool lastDg = false;
            for (int i = 0; i < tab_Det.Rows.Count; i++)
            {
                DataRow row_Det = tab_Det.Rows[i];
                DataRow row1 = details.NewRow();
                string dnNo = row_Det["DoNo"].ToString();
                string jobType = "";
                string contNo = row_Det["ContNo"].ToString();
                string sealNo = row_Det["SealNo"].ToString();
                bool isDg = SafeValue.SafeBool(row_Det["IsDg"], false);
                string dgClass = row_Det["DgClass"].ToString();
                string expBkgN = row_Det["BookingNo"].ToString();
                string hblN = row_Det["HblNo"].ToString();
                string mkg = row_Det["Marking2"].ToString();
                string rmk = row_Det["Remark1"].ToString();
                string desc2 = row_Det["Desc2"].ToString();
                string pod = "";

                //int tally1 = SafeValue.SafeInt(row_Det["Tally1"], 0);
                //int tally2 = SafeValue.SafeInt(row_Det["Tally2"], 0);
                //int tally3 = SafeValue.SafeInt(row_Det["Tally3"], 0);
                //int tally4 = SafeValue.SafeInt(row_Det["Tally4"], 0);
                //int tally21 = SafeValue.SafeInt(row_Det["Tally21"], 0);
                //int tally22 = SafeValue.SafeInt(row_Det["Tally22"], 0);
                //int tally23 = SafeValue.SafeInt(row_Det["Tally23"], 0);
                //int tally24 = SafeValue.SafeInt(row_Det["Tally24"], 0);
                //int tally31 = SafeValue.SafeInt(row_Det["Tally31"], 0);
                //int tally32 = SafeValue.SafeInt(row_Det["Tally32"], 0);
                //int tally33 = SafeValue.SafeInt(row_Det["Tally33"], 0);
                //int tally34 = SafeValue.SafeInt(row_Det["Tally34"], 0);
                //int tally41 = SafeValue.SafeInt(row_Det["Tally41"], 0);
                //int tally42 = SafeValue.SafeInt(row_Det["Tally42"], 0);
                //int tally43 = SafeValue.SafeInt(row_Det["Tally43"], 0);
                //int tally44 = SafeValue.SafeInt(row_Det["Tally44"], 0);
                //int tally51 = SafeValue.SafeInt(row_Det["Tally51"], 0);
                //int tally52 = SafeValue.SafeInt(row_Det["Tally52"], 0);
                //int tally53 = SafeValue.SafeInt(row_Det["Tally53"], 0);
                //int tally54 = SafeValue.SafeInt(row_Det["Tally54"], 0);
                //int tally61 = SafeValue.SafeInt(row_Det["Tally61"], 0);
                //int tally62 = SafeValue.SafeInt(row_Det["Tally62"], 0);
                //int tally63 = SafeValue.SafeInt(row_Det["Tally63"], 0);
                //int tally64 = SafeValue.SafeInt(row_Det["Tally64"], 0);

                int qty = SafeValue.SafeInt(row_Det["Qty"], 0);
                decimal wt = SafeValue.SafeDecimal(row_Det["Weight"], 0);
                decimal m3 = SafeValue.SafeDecimal(row_Det["Volume"], 0);
                string pkgType = SafeValue.SafeString(row_Det["UomCode"], "");
                string pkgType2 = SafeValue.SafeString(row_Det["PackTypeOrig"], "");
                int qtyOrig = SafeValue.SafeInt(row_Det["QtyOrig"], 0);
                decimal wtOrig = SafeValue.SafeDecimal(row_Det["WeightOrig"], 0);
                decimal m3Orig = SafeValue.SafeDecimal(row_Det["VolumeOrig"], 0);

                if ((contNo == lastContNo & isDg == lastDg) || lastContNo == "")
                {
                    g2++;
                }
                else
                {
                    g0++;
                    g1 = 1;
                    g2 = 1;
                    p = 0;
                }
                lastContNo = contNo;
                lastDg = isDg;
                row1["JobOrderNo"] = orderNo;
                p++;
                row1["LineN"] = p;

                row1["PrintGroup"] = string.Format("C{0:00}{1:00}", g0, g1);
                row1["PrintSesseion"] = string.Format("C{0:00}", g0);
                if (g2 % 6 == 0)
                {
                    g1++;
                }
                row1["ContNo"] = contNo;
                row1["SealNo"] = sealNo;
                //string sql_cont = string.Format("select ContNo,SealNo,Ft20,Ft40,Ft45,FtType,scheduleDate,ScheduleTime,ActualDate,ActualTime,TallyClerk,FlDriver from xwjobcont where JobOrder='{0}' and ContNo='{1}'", orderNo, contNo);
                //DataTable tab_cont = Helper.Sql.List(sql_cont);
                //if (tab_cont.Rows.Count > 0)
                //{
                //    row1["SealNo"] = SafeValue.SafeString(tab_cont.Rows[0]["SealNo"], "");
                //    row1["FtType"] = SafeValue.SafeString(tab_cont.Rows[0]["FtType"], "");
                //    row1["SchDate"] = SafeValue.SafeDateStr(tab_cont.Rows[0]["ScheduleDate"]);
                //    row1["ComplDate"] = SafeValue.SafeDateStr(tab_cont.Rows[0]["ActualDate"]);
                //    string time = SafeValue.SafeString(tab_cont.Rows[0]["ScheduleTime"], "");
                //    string time1 = SafeValue.SafeString(tab_cont.Rows[0]["ActualTime"], "");
                //    if (time.Trim().Length > 0)
                //    {
                //        row1["SchTime"] = time;
                //    }
                //    if (time1.Trim().Length > 0)
                //        row1["ComplTime"] = time1;
                //    string size = "";
                //    if (SafeValue.SafeInt(tab_cont.Rows[0]["Ft20"], 0) > 0)
                //        size = "20";
                //    else if (SafeValue.SafeInt(tab_cont.Rows[0]["Ft40"], 0) > 0)
                //        size = "40";
                //    else if (SafeValue.SafeInt(tab_cont.Rows[0]["Ft45"], 0) > 0)
                //        size = "45";
                //    row1["Size"] = size;
                //    row1["TallyClerk"] = SafeValue.SafeString(tab_cont.Rows[0]["TallyClerk"], "");
                //    row1["Driver"] = SafeValue.SafeString(tab_cont.Rows[0]["FlDriver"], "");
                //}
                row1["DnNO"] = dnNo.Trim();
                row1["ExpBkgN"] = expBkgN;
                row1["HblN"] = hblN;
                row1["TotMarking"] = mkg;

                //row1["Tally1"] = tally1.ToString("#");
                //row1["Tally2"] = tally2.ToString("#");
                //row1["Tally3"] = tally3.ToString("#");
                //row1["Tally4"] = tally4.ToString("#");
                //row1["Tally21"] = tally21.ToString("#");
                //row1["Tally22"] = tally22.ToString("#");
                //row1["Tally23"] = tally23.ToString("#");
                //row1["Tally24"] = tally24.ToString("#");
                //row1["Tally31"] = tally31.ToString("#");
                //row1["Tally32"] = tally32.ToString("#");
                //row1["Tally33"] = tally33.ToString("#");
                //row1["Tally34"] = tally34.ToString("#");
                //row1["Tally41"] = tally41.ToString("#");
                //row1["Tally42"] = tally42.ToString("#");
                //row1["Tally43"] = tally43.ToString("#");
                //row1["Tally44"] = tally44.ToString("#");
                //row1["Tally51"] = tally51.ToString("#");
                //row1["Tally52"] = tally52.ToString("#");
                //row1["Tally53"] = tally53.ToString("#");
                //row1["Tally54"] = tally54.ToString("#");
                //row1["Tally61"] = tally61.ToString("#");
                //row1["Tally62"] = tally62.ToString("#");
                //row1["Tally63"] = tally63.ToString("#");
                //row1["Tally64"] = tally64.ToString("#");
                row1["TotQty"] = qty.ToString("#");
                row1["PackType"] = pkgType;
                row1["PackTypeOrig"] = pkgType2;
                row1["M3"] = m3.ToString("#,##0.000");
                row1["Weight"] = wt.ToString("#,##0.000");


                row1["OrigQty"] = qtyOrig.ToString("#");
                row1["OrigM3"] = m3Orig.ToString("#,##0.000");
                row1["OrigWeight"] = wtOrig.ToString("#,##0.000");
                if (qtyOrig != qty)
                    row1["DiffQty"] = qty - qtyOrig;
                if (m3Orig != m3)
                    row1["DiffM3"] = m3 - m3Orig;
                if (wtOrig != wt)
                    row1["DiffWeight"] = wt - wtOrig;

                row1["TotRemark"] = rmk;
                row1["LocalRemark"] = "";


                if (isDg)
                {
                    if (dgClass.Trim().Length < 1)
                    {
                        row1["DgClass"] = "DG Cargo";
                    }
                    else
                    {
                        row1["DgClass"] = "DG Class:\n" + dgClass;
                    }
                }
                //row1["TotDesc"] = desc2.Trim();
                row1["POD"] = pod;
                if (jobType.ToUpper() == "T")
                {
                    if (desc2.Trim().Length > 23)
                        row1["TotDesc"] = desc2.Trim().Substring(desc2.Trim().Length - 23).Trim().Replace("\n", "") + "\n" + rmk.Trim();
                    else
                        row1["TotDesc"] = desc2.Trim() + "\n" + rmk.Trim();

                    // row1["TotDesc"] += "\n\nM3:" + SafeValue.SafeDecimal(row_Det["M3"], 0).ToString("0.000");

                }
                else
                {
                    row1["TotDesc"] = rmk;

                }
                details.Rows.Add(row1);
            }
            #endregion
        }
        DataSet set = new DataSet();
        set.Tables.Add(mast);
        set.Tables.Add(details);
        set.Relations.Add("", mast.Columns["JobOrderNo"], details.Columns["JobOrderNo"]);

        return set;
    }
    private static DataTable InitTsMastDataTable()
    {
        DataTable tab = new DataTable("Mast");
        tab.Columns.Add("JobOrderNo");
        tab.Columns.Add("VesselNo");
        tab.Columns.Add("VoyNo");
        tab.Columns.Add("BookingNo");
        tab.Columns.Add("Carrier");
        tab.Columns.Add("Eta");
        tab.Columns.Add("ByWho");
        tab.Columns.Add("ImpRefNo");
        tab.Columns.Add("PortLoad");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Condition");
        tab.Columns.Add("TallyDone");
        tab.Columns.Add("UserData1");
        tab.Columns.Add("IssueBy");
        tab.Columns.Add("UserName");
        return tab;
    }
    private static DataTable InitTsDetailDataTable()
    {
        DataTable tab = new DataTable("Details");
        tab.Columns.Add("JobOrderNo");
        tab.Columns.Add("LineN", typeof(Int32));
        tab.Columns.Add("PrintSesseion");
        tab.Columns.Add("PrintGroup");
        tab.Columns.Add("ContNo");
        tab.Columns.Add("SealNo");
        tab.Columns.Add("SchDate");
        tab.Columns.Add("SchTime");
        tab.Columns.Add("ComplDate");
        tab.Columns.Add("ComplTime");
        tab.Columns.Add("Size");
        tab.Columns.Add("FtType");
        tab.Columns.Add("DnNO");
        tab.Columns.Add("ExpBkgN");
        tab.Columns.Add("POD");
        tab.Columns.Add("HblN");
        tab.Columns.Add("TotMarking");
        tab.Columns.Add("Tally1");
        tab.Columns.Add("Tally2");
        tab.Columns.Add("Tally3");
        tab.Columns.Add("Tally4");
        tab.Columns.Add("Tally21");
        tab.Columns.Add("Tally22");
        tab.Columns.Add("Tally23");
        tab.Columns.Add("Tally24");
        tab.Columns.Add("Tally31");
        tab.Columns.Add("Tally32");
        tab.Columns.Add("Tally33");
        tab.Columns.Add("Tally34");
        tab.Columns.Add("Tally41");
        tab.Columns.Add("Tally42");
        tab.Columns.Add("Tally43");
        tab.Columns.Add("Tally44");
        tab.Columns.Add("Tally51");
        tab.Columns.Add("Tally52");
        tab.Columns.Add("Tally53");
        tab.Columns.Add("Tally54");
        tab.Columns.Add("Tally61");
        tab.Columns.Add("Tally62");
        tab.Columns.Add("Tally63");
        tab.Columns.Add("Tally64");
        tab.Columns.Add("TotQty");
        tab.Columns.Add("PackType");
        tab.Columns.Add("M3");
        tab.Columns.Add("Weight");
        tab.Columns.Add("PackTypeOrig");
        tab.Columns.Add("OrigQty");
        tab.Columns.Add("OrigM3");
        tab.Columns.Add("OrigWeight");
        tab.Columns.Add("DiffQty");
        tab.Columns.Add("DiffM3");
        tab.Columns.Add("DiffWeight");
        tab.Columns.Add("TotRemark");
        tab.Columns.Add("TsRemarkStr");
        tab.Columns.Add("DgClass");
        tab.Columns.Add("LocalRemark");
        tab.Columns.Add("CargoStatus");
        tab.Columns.Add("LandStatus");


        tab.Columns.Add("Shipper");
        tab.Columns.Add("Port");
        tab.Columns.Add("DoNoBarcode");
        tab.Columns.Add("TotDesc");


        tab.Columns.Add("TallyClerk");
        tab.Columns.Add("Driver");

        return tab;
    }
    #endregion
    #region haulier imp&exp
    public static DataTable PrintAuthLetter(string orderNo)
    {
        #region init column
        DataTable mast = new DataTable("Mast");

        mast.Columns.Add("RefN");
        mast.Columns.Add("NowD");
        mast.Columns.Add("Cust");
        mast.Columns.Add("Vend1");
        mast.Columns.Add("BlNo");
        mast.Columns.Add("Sn");
        mast.Columns.Add("Eta");
        mast.Columns.Add("VesVoy");
        mast.Columns.Add("ContNo");
        mast.Columns.Add("FtSize");
        mast.Columns.Add("Port");
        mast.Columns.Add("Ms");
        mast.Columns.Add("Cr");
        mast.Columns.Add("Qty");
        mast.Columns.Add("Wt");
        mast.Columns.Add("M3");
        mast.Columns.Add("SealNo");
        mast.Columns.Add("CompanyName");
        mast.Columns.Add("User");
        mast.Columns.Add("Rmk");
        mast.Columns.Add("Pack");
        #endregion
        string sql = string.Format("SELECT JobOrderNo,JobDate,CustCode,Carrier, Eta, VesselNo, VoyNo, Pol,BookingNo,Pod,PermitNo,PartyCode,Haulier FROM XWJobOrder WHERE (JobOrderNo = '{0}')", orderNo);
        DataTable tabOrder = Helper.Sql.List(sql);
        if (tabOrder.Rows.Count == 1)
        {
            string custCode = tabOrder.Rows[0]["CustCode"].ToString();
            string sql1 = "select name from xxparty where partyid='" + custCode + "'";
            string custName = SafeValue.SafeString(Helper.Sql.One(sql1), custCode);
            DateTime jobDate = SafeValue.SafeDate(tabOrder.Rows[0]["JobDate"], DateTime.Today);
            string ves = tabOrder.Rows[0]["VesselNo"].ToString();
            string voy = tabOrder.Rows[0]["VoyNo"].ToString();
            string pol = tabOrder.Rows[0]["Pol"].ToString();
            string bl = tabOrder.Rows[0]["BookingNo"].ToString();
            string pod = tabOrder.Rows[0]["Pod"].ToString();
            string carrier = tabOrder.Rows[0]["Carrier"].ToString();
            string permitNo = tabOrder.Rows[0]["PermitNo"].ToString();
            string partyCode = tabOrder.Rows[0]["PartyCode"].ToString();
            string haulier = tabOrder.Rows[0]["Haulier"].ToString();
            sql1 = "select name from [XXPort] where code='" + SafeValue.SafeString(pol, "") + "'";
            string port = "";

            if (pol == "NA")
                port = "NA";
            else
                port = SafeValue.SafeString(Helper.Sql.One(sql1), pol) + " / " + SafeValue.SafeString(Helper.Sql.One(sql1), pod);
            string eta = SafeValue.SafeDateStr(tabOrder.Rows[0]["Eta"]);
            sql1 = string.Format(@"select Name,CrNo from xxParty where PartyId='{0}'", haulier);
            DataTable dt_p = Helper.Sql.List(sql1);
            string ms = "";
            string cr = "";
            if (dt_p.Rows.Count > 0)
            {
                ms = SafeValue.SafeString(dt_p.Rows[0]["Name"]);
                cr = SafeValue.SafeString(dt_p.Rows[0]["CrNo"]);
            }
            string contNo = "";
            string sealNo = "";
            int ft20 = 0;
            int ft40 = 0;
            int ft45 = 0;
            int qty = 0;
            decimal wt = 0;
            decimal m3 = 0;
            string ftSize = "";
            string ftType = "";
            string rmk = "";
            string sqlCont = string.Format("SELECT ContNo, SealNo, Ft20, Ft40, Ft45, FtType,Remark FROM XWJobCont WHERE (JobOrder = '{0}')", orderNo);
            DataTable tabCont = Helper.Sql.List(sqlCont);
            if (tabCont.Rows.Count > 0)
            {
                for (int i = 0; i < tabCont.Rows.Count; i++)
                {
                    DataRow row1 = tabCont.Rows[i];
                    contNo = row1["ContNo"].ToString();
                    sealNo = row1["SealNo"].ToString();

                    ft20 = SafeValue.SafeInt(row1["Ft20"], 0);
                    ft40 = SafeValue.SafeInt(row1["Ft40"], 0);
                    ft45 = SafeValue.SafeInt(row1["Ft45"], 0);

                    if (ft20 > 0)
                        ftSize = "20";
                    else if (ft40 > 0)
                        ftSize += "40";
                    else if (ft45 > 0)
                        ftSize += "45";
                    ftType = row1["FtType"].ToString();

                    rmk = row1["Remark"].ToString();
                    string sql_sum = string.Format("SELECT  SUM(QtyOrig) AS Qty, SUM(WeightOrig) AS Wt, SUM(M3Orig) AS M3 FROM  XWJobDet WHERE (JobOrder = '{0}') AND (ContNo = '{1}')", orderNo, contNo);

                    DataTable tab_sum = Helper.Sql.List(sql_sum);
                    if (tab_sum.Rows.Count == 1)
                    {
                        qty = SafeValue.SafeInt(tab_sum.Rows[0]["Qty"], 0);
                        wt = SafeValue.SafeDecimal(tab_sum.Rows[0]["Wt"], 0);
                        m3 = SafeValue.SafeDecimal(tab_sum.Rows[0]["M3"], 0);
                    }
                }

            }
            DataRow row = mast.NewRow();
            row["Cust"] = custCode; //custName;
            row["Vend1"] = carrier;
            row["RefN"] = orderNo;
            row["NowD"] = jobDate.ToString("dd/MM/yyyy");
            row["Eta"] = eta;
            row["VesVoy"] = ves + "/" + voy;
            row["ContNo"] = contNo;
            row["FtSize"] = ftSize + ftType;
            row["Port"] = port;
            row["Qty"] = qty;
            row["Wt"] = wt.ToString("0.000");
            row["M3"] = m3.ToString("0.000");
            row["SealNo"] = sealNo;
            row["Rmk"] = rmk;
            row["BlNo"] = bl;
            row["Ms"] = ms;
            row["Cr"] = cr;
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["Pack"] = "";
            row["User"] = HttpContext.Current.User.Identity.Name;
            mast.Rows.Add(row);
        }
        return mast;
    }
    #endregion
}
