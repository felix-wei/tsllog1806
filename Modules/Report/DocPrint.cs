using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Wilson.ORMapper;
using System.Collections;
using C2;

/// <summary>
/// DocPrint 的摘要说明
/// </summary>
public class DocPrint
{
	public DocPrint()
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
        row["PortLoad"] = SafeValue.SafeString(Helper.Sql.One(sql), obj.Pol);

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
            if (contType.Substring(0,2)=="20")
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
    public static DataSet PrintHaulier(string orderNo,string jobType)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        ObjectQuery query1 = new ObjectQuery(typeof(C2.CtmJobDet1), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);


        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");

        mast.Columns.Add("JobNo");
        mast.Columns.Add("JobType");
        mast.Columns.Add("Haulier");
        mast.Columns.Add("Attn");
        mast.Columns.Add("DeliveryTo");
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

        mast.Columns.Add("Vessel");
        mast.Columns.Add("Voyage");
        mast.Columns.Add("Etd");
        mast.Columns.Add("Eta");
        mast.Columns.Add("Pol");
        mast.Columns.Add("ByWho");
        mast.Columns.Add("ClientRefNo");
        mast.Columns.Add("CustName");
        mast.Columns.Add("AgentName");
        mast.Columns.Add("UserData2");
        mast.Columns.Add("UserData3");
        mast.Columns.Add("UserData4");
        mast.Columns.Add("Outside");
        mast.Columns.Add("Note1");
        mast.Columns.Add("Note2");
        mast.Columns.Add("StampUser");
        mast.Columns.Add("Remark");
        mast.Columns.Add("SpecialInstruction");

        det.Columns.Add("Idx");
        det.Columns.Add("JobNo");
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
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        row["Attn"] = obj.Terminalcode;
        row["DeliveryTo"] = obj.DeliveryTo;
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

        row["JobNo"] = orderNo;
        row["JobType"] = obj.JobType;
        row["Vessel"] = obj.Vessel;
        row["Voyage"] = obj.Voyage;
        row["Permit"] = obj.PermitNo;

        string sql = "select name from xxparty where partyid='" + obj.ClientId + "'";
        row["CustName"] =SafeValue.SafeString(ConnectSql.ExecuteScalar(sql), "");


        row["Etd"] = obj.EtdDate.ToString("dd/MM/yyyy");
        row["Eta"] = obj.EtaDate.ToString("dd/MM/yyyy");
        sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pol, "") + "'";
        row["Pol"] = SafeValue.SafeString(Helper.Sql.One(sql),"");
        sql = "select name from xxparty where partyid='" +obj.CarrierId + "'";
        row["Carrier"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        row["OceanBl"] = obj.CarrierBlNo;
        row["Yard"] =obj.YardRef;
        row["ByWho"] ="";
        row["ClientRefNo"] = obj.ClientRefNo;
        row["UserData2"] = "";
        row["SpecialInstruction"] = obj.SpecialInstruction;
        row["Remark"] = obj.Remark;
        row["StampUser"] = HttpContext.Current.User.Identity.Name;
        int ft20 = 0;
        int ft40 = 0;
        int ft45 = 0;
        

        for (int i = 0; i < objSet1.Count; i++)
        {
            C2.CtmJobDet1 cont = objSet1[i] as C2.CtmJobDet1;
            DataRow row1 = det.NewRow();
            row1["Idx"] = i + 1;
            row1["JobNo"] = orderNo;
            row1["ContNo"] = cont.ContainerNo;
            row1["SealNo"] = cont.SealNo;
            string ftSize = "";
            //int index = cont.ContainerType.IndexOf("FT", 2);
            if (cont.ContainerType.IndexOf("20", 0) == 0)
                ft20++;
            if (cont.ContainerType.IndexOf("40", 0) == 0)
                ft40++;
            if (cont.ContainerType.IndexOf("45", 0) == 0)
                ft45++;
            row1["FtSize"] = ftSize;
            row1["FtType"] = cont.ContainerType;

            row1["FtQty"] = cont.Qty;
            row1["FtKgs"] = cont.Weight.ToString("0");
            row1["FtCbm"] = cont.Volume.ToString("#,##0.00");
            row1["FtTruckIn"] = "TRUCK-IN:" + cont.ScheduleDate.ToString("dd-MMM");
            det.Rows.Add(row1);
        }
        row["Ft20"] = ft20;
        row["Ft40"] = ft40;
        row["Ft45"] = ft45;
        mast.Rows.Add(row);

        set.Tables.Add(mast);
        set.Tables.Add(det);
       
        return set;
    }
    public static DataSet PrintTallySheet(string orderNo, string jobType)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Details");
        mast.Columns.Add("JobNo");
        mast.Columns.Add("JobType");
        mast.Columns.Add("Haulier");
        mast.Columns.Add("Attn");
        mast.Columns.Add("DeliveryTo");
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
        mast.Columns.Add("ContNo");
        mast.Columns.Add("SealNo");
        mast.Columns.Add("Ft20");
        mast.Columns.Add("Ft40");
        mast.Columns.Add("Ft45");
        mast.Columns.Add("FtSize");
        mast.Columns.Add("ContType");
        mast.Columns.Add("CarrierBkgNo");
        mast.Columns.Add("Vessel");
        mast.Columns.Add("Voyage");
        mast.Columns.Add("Etd");
        mast.Columns.Add("Eta");
        mast.Columns.Add("Pol");
        mast.Columns.Add("ByWho");
        mast.Columns.Add("ClientRefNo");
        mast.Columns.Add("CustName");
        mast.Columns.Add("AgentName");
        mast.Columns.Add("UserData2");
        mast.Columns.Add("UserData3");
        mast.Columns.Add("UserData4");
        mast.Columns.Add("Outside");
        mast.Columns.Add("Note1");
        mast.Columns.Add("Note2");
        mast.Columns.Add("StampUser");
        mast.Columns.Add("Remark");
        mast.Columns.Add("SpecialInstruction");

        det.Columns.Add("TotalQty");
        det.Columns.Add("TotalM3");

        det.Columns.Add("No");
        det.Columns.Add("JobNo");
        det.Columns.Add("ContNo");
        det.Columns.Add("BookingNo");
        det.Columns.Add("HblNo");
        det.Columns.Add("OpsType");
        det.Columns.Add("Qty");
        det.Columns.Add("Uom");
        det.Columns.Add("QtyOrig");
        det.Columns.Add("WeightOrig");
        det.Columns.Add("VolumeOrig");
        det.Columns.Add("PackType");
        det.Columns.Add("SkuCode");
        det.Columns.Add("Weight");
        det.Columns.Add("PackQty");
        det.Columns.Add("PackUom");
        det.Columns.Add("Volume");
        det.Columns.Add("Location");
        det.Columns.Add("Length");
        det.Columns.Add("Width");
        det.Columns.Add("Height");
        det.Columns.Add("Marking1");
        det.Columns.Add("Marking2");
        det.Columns.Add("Remark1");
        det.Columns.Add("Pod");
        #endregion

        #region Data
        string contNo = "";
        string contType = "";

        
            DataRow row = mast.NewRow();
            string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
            DataTable tab_hauler = Helper.Sql.List(sql_haulier);
            row["Attn"] = obj.Terminalcode;
            row["DeliveryTo"] = obj.DeliveryTo;
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
            row["CarrierBkgNo"] = obj.CarrierBkgNo;
            row["JobNo"] = orderNo;
            row["JobType"] = obj.JobType;
            row["Vessel"] = obj.Vessel;
            row["Voyage"] = obj.Voyage;
            row["Permit"] = obj.PermitNo;

            string sql = "select name from xxparty where partyid='" + obj.ClientId + "'";
            row["CustName"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql), "");




            row["Etd"] = obj.EtdDate.ToString("dd/MM/yyyy");
            row["Eta"] = obj.EtaDate.ToString("dd/MM/yyyy");
            sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pol, "") + "'";
            row["Pol"] = SafeValue.SafeString(Helper.Sql.One(sql), "");

            sql = "select name from xxparty where partyid='" + obj.CarrierId + "'";
            row["Carrier"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
            row["OceanBl"] = obj.CarrierBlNo;
            row["Yard"] = obj.YardRef;
            row["ByWho"] = "";
            row["ClientRefNo"] = obj.ClientRefNo;
            row["UserData2"] = "";
            row["SpecialInstruction"] = obj.SpecialInstruction;
            row["Remark"] = obj.Remark;
            row["StampUser"] = HttpContext.Current.User.Identity.Name;

            int n = 0;

             sql = string.Format(@"select ContainerNo,ContainerType from Ctm_JobDet1 where JobNo='{0}'", orderNo);
            DataTable tab_cont = ConnectSql_mb.GetDataTable(sql);
            for (int a = 0; a < tab_cont.Rows.Count; a++)
            {
                contNo = SafeValue.SafeString(tab_cont.Rows[a]["ContainerNo"]);
                contType = SafeValue.SafeString(tab_cont.Rows[a]["ContainerType"]);
                row["ContNo"] += contNo + " " + contType+" / ";
            }

            decimal total = 0;
            decimal m3 = 0;
            string lastContNo = "";
            sql = string.Format(@"select *  from job_house where JobNo='{0}' order by Id asc", orderNo);
            DataTable dt_det = ConnectSql_mb.GetDataTable(sql);

            for (int i = 0; i < dt_det.Rows.Count; i++)
            {
                DataRow rowDet = dt_det.Rows[i];
                DataRow row1 = det.NewRow();

                row1["No"] = n + 1;
                row1["ContNo"] = rowDet["ContNo"];
                row1["JobNo"] = orderNo;
                row1["BookingNo"] = rowDet["BookingNo"];
                row1["HblNo"] = rowDet["HblNo"];
                row1["Length"] = rowDet["LengthPack"];
                row1["Width"] = rowDet["WidthPack"];
                row1["Height"] = rowDet["HeightPack"];
                row1["Weight"] = rowDet["Weight"];
                row1["Volume"] = rowDet["Volume"];
                row1["Qty"] = SafeValue.SafeDecimal(rowDet["Qty"], 0);
                row1["Uom"] = SafeValue.SafeDecimal(rowDet["UomCode"], 0);
                row1["QtyOrig"] = SafeValue.SafeDecimal(rowDet["QtyOrig"], 0);
                row1["VolumeOrig"] = SafeValue.SafeDecimal(rowDet["VolumeOrig"], 0);
                row1["WeightOrig"] = rowDet["WeightOrig"];
                row1["PackType"] = rowDet["PackTypeOrig"];
                row1["PackQty"] = rowDet["PackQty"];
                row1["PackUom"] = rowDet["PackUom"];
                sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pod, "") + "'";
                row1["Pod"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
                total += SafeValue.SafeDecimal(rowDet["QtyOrig"], 0);
                m3 += SafeValue.SafeDecimal(rowDet["VolumeOrig"], 0);

                n++;
                det.Rows.Add(row1);
            }
            mast.Rows.Add(row);
        #endregion
      

        set.Tables.Add(mast);
        set.Tables.Add(det);
        //int count = mast.Rows.Count;
        //int m = det.Rows.Count;
        //set.Relations.Add("", mast.Columns["ContNo"], det.Columns["ContNo"]);
        return set;
    }
    public static DataSet PrintDeliveryOrder(string orderNo, string jobType)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        ObjectQuery query1 = new ObjectQuery(typeof(C2.JobHouse), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);


        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");


        mast.Columns.Add("JobNo");
        mast.Columns.Add("JobDate");
        mast.Columns.Add("JobType");
        mast.Columns.Add("Haulier");
        mast.Columns.Add("Attn");
        mast.Columns.Add("DeliveryTo");
        mast.Columns.Add("PickupFrom");
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

        mast.Columns.Add("Vessel");
        mast.Columns.Add("Voyage");
        mast.Columns.Add("Etd");
        mast.Columns.Add("Eta");
        mast.Columns.Add("Pol");
        mast.Columns.Add("Pod");
        mast.Columns.Add("ByWho");
        mast.Columns.Add("ClientRefNo");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("PartyAdd");
        mast.Columns.Add("AgentName");
        mast.Columns.Add("UserData2");
        mast.Columns.Add("UserData3");
        mast.Columns.Add("UserData4");
        mast.Columns.Add("Outside");
        mast.Columns.Add("Note1");
        mast.Columns.Add("Note2");
        mast.Columns.Add("UserId");
        mast.Columns.Add("Remark");
        mast.Columns.Add("SpecialInstruction");
        mast.Columns.Add("WarehouseCode");
        mast.Columns.Add("CrBkgNo");
        mast.Columns.Add("TotalQty");

        det.Columns.Add("No");
        det.Columns.Add("JobNo");
        det.Columns.Add("CargoStatus");
        det.Columns.Add("CargoType");
        det.Columns.Add("ContNo");
        det.Columns.Add("BookingNo");
        det.Columns.Add("HblNo");
        det.Columns.Add("OpsType");
        det.Columns.Add("Qty");
        det.Columns.Add("PackType");
        det.Columns.Add("SkuCode");
        det.Columns.Add("Weight");
        det.Columns.Add("PackQty");
        det.Columns.Add("PackUom");
        det.Columns.Add("Volume");
        det.Columns.Add("Location");
        det.Columns.Add("Length");
        det.Columns.Add("Width");
        det.Columns.Add("Height");
        det.Columns.Add("Marking1");
        det.Columns.Add("Marking2");
        det.Columns.Add("Remark1");
        

        #endregion

        DataRow row = mast.NewRow();
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        if(jobType=="gr"){
            row["JobType"] = "Goods Receipt Note".ToUpper() ;
        } 
        if (jobType == "do")
        {
            row["JobType"] = "Delivery Order".ToUpper();
        }
        if (jobType == "tp")
        {
            row["JobType"] = "Transport Instruction".ToUpper();
        }
        if (jobType == "tr")
        {
            row["JobType"] = "Transfer Instruction".ToUpper();
        }
        row["Attn"] = obj.Terminalcode;
        row["DeliveryTo"] = obj.DeliveryTo;
        row["PickupFrom"] = obj.PickupFrom;
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

        row["JobNo"] = orderNo;
        row["JobDate"] = obj.EtdDate.ToString("dd/MM/yyyy");
        row["Vessel"] = obj.Vessel;
        row["Voyage"] = obj.Voyage;
        row["Permit"] = obj.PermitNo;

        string sql = "select name from xxparty where partyid='" + obj.ClientId + "'";
        row["PartyName"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql), "");
        sql = "select Address from xxparty where partyid='" + obj.ClientId + "'";
        row["PartyAdd"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql), "");

        row["Etd"] = obj.EtdDate.ToString("dd/MM/yyyy");
        row["Eta"] = obj.EtaDate.ToString("dd/MM/yyyy");
        sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pol, "") + "'";
        row["Pol"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pod, "") + "'";
        row["Pod"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        sql = "select name from xxparty where partyid='" + obj.CarrierId + "'";
        row["Carrier"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        row["OceanBl"] = obj.CarrierBlNo;
        row["CrBkgNo"] = obj.CarrierBkgNo;
        row["Yard"] = obj.YardRef;
        row["ByWho"] = "";
        row["ClientRefNo"] = obj.ClientRefNo;
        row["UserData2"] = "";
        row["SpecialInstruction"] = obj.SpecialInstruction;
        row["Remark"] = obj.Remark;
        row["UserId"] = HttpContext.Current.User.Identity.Name;
        decimal total = 0;


        for (int i = 0; i < objSet1.Count; i++)
        {
            C2.JobHouse house = objSet1[i] as C2.JobHouse;
            DataRow row1 = det.NewRow();
            row1["No"] = i + 1;
            row1["JobNo"] = orderNo;
            row1["ContNo"] = house.ContNo;
            row1["CargoStatus"]=house.CargoStatus;
            row1["CargoType"]=house.CargoType;
            row1["Location"] = house.Location;
            row1["BookingNo"] = house.BookingNo;
            row1["HblNo"] = house.HblNo;
            row1["Length"] = house.LengthPack;
            row1["Width"] = house.WidthPack;
            row1["Height"] = house.HeightPack;
            row1["Weight"] = house.WeightOrig;
            row1["Volume"] = house.WeightOrig;
            row1["Qty"] = house.QtyOrig;
            row1["PackType"] = house.PackTypeOrig;
            row1["PackQty"] = house.PackQty;
            row1["PackUom"] = house.PackUom;
            total += house.QtyOrig;
            det.Rows.Add(row1);
        }
        row["TotalQty"] = total;
        mast.Rows.Add(row);

        set.Tables.Add(mast);
        set.Tables.Add(det);

        return set;
    }
    public static DataTable DsReceipt(string recNo)
    {
        DataTable tab = new DataTable();
        #region init column
        tab.Columns.Add("Title");
        tab.Columns.Add("JobType");
        tab.Columns.Add("BarCode");
        tab.Columns.Add("CustCode");
        tab.Columns.Add("Port");

        tab.Columns.Add("RecNo");
        tab.Columns.Add("RecDate");
        tab.Columns.Add("RecTime");
        tab.Columns.Add("RecFrom");
        tab.Columns.Add("Markings");
        tab.Columns.Add("Contents");
        tab.Columns.Add("ContNo");
        tab.Columns.Add("ComplDate");
        tab.Columns.Add("ComplTime");
        tab.Columns.Add("Qty");
        tab.Columns.Add("PackType");
        tab.Columns.Add("M3");
        tab.Columns.Add("Weight");
        tab.Columns.Add("UserId");
        tab.Columns.Add("SpcRemark");
        tab.Columns.Add("InternalExternal");

        tab.Columns.Add("ForkLift");
        tab.Columns.Add("Dn");
        tab.Columns.Add("Tracing");
        tab.Columns.Add("Wh");
        tab.Columns.Add("Admin");
        tab.Columns.Add("Sr");
        tab.Columns.Add("Removal");
        tab.Columns.Add("Ot");
        tab.Columns.Add("LongLength");
        tab.Columns.Add("OverWeight");
        tab.Columns.Add("Sticker");
        tab.Columns.Add("Ugt");
        tab.Columns.Add("Sec");
        tab.Columns.Add("Dg");
        tab.Columns.Add("Misc");
        tab.Columns.Add("Other");
        tab.Columns.Add("Waive");
        tab.Columns.Add("Total");
        tab.Columns.Add("OtherRemarks");
        tab.Columns.Add("VesVoy");

        tab.Columns.Add("BillAmt");

        tab.Columns.Add("DoNo");
        tab.Columns.Add("IsCn");
        tab.Columns.Add("Cn");
        tab.Columns.Add("HblN");
        tab.Columns.Add("JobOrder");
        tab.Columns.Add("ClearRmk");


        tab.Columns.Add("VehicleUser");
        tab.Columns.Add("VehicleTel");
        tab.Columns.Add("VehicleNric");
        tab.Columns.Add("VehicleNo1");
        tab.Columns.Add("VehicleNo2");
        tab.Columns.Add("VehicleNo3");
        tab.Columns.Add("QtyWord");
        tab.Columns.Add("TotQty");
        #endregion

        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + recNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);

        C2.CtmJob job = objSet[0] as C2.CtmJob;

        ObjectQuery query1 = new ObjectQuery(typeof(C2.JobHouse), "JobNo='" + recNo + "'", "");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);

        DataRow row = tab.NewRow();
        if (objSet1.Count > 0 && objSet1[0] != null)
        {
            C2.JobHouse obj = objSet1[0] as C2.JobHouse;

            if (job.JobType == "DO")
                row["Title"] = "DELIVERY NOTE";
            if (job.JobType == "GR")
              row["Title"]="CARGO RECEIPT";
            if (job.JobType == "TP")
                row["Title"] = "CARGO DIRECT RELEASE";
            row["JobType"] = obj.JobType;
            row["BarCode"] = "04-" + job.JobNo + "-00";
            row["RecNo"] = obj.JobNo;
            row["RecDate"] = job.JobDate.ToString("dd/MM/yyyy");
            row["RecTime"] = job.JobDate.ToString("HH:mm");
            row["JobOrder"] = job.JobNo;
            row["RecFrom"] = EzshipHelper.GetPartyName(obj.ClientId);

            row["CustCode"] =obj.ClientId;
            string agCode = "";

          
            //row["Port"] = obj.JobType=="E" ? obj.PortDisc : obj.PortLoad;
            row["Markings"] = obj.Marking1;
            row["Contents"] = obj.Marking2;
            row["ContNo"] = obj.ContNo;
            row["ComplDate"] = job.UpdateDateTime.ToString("dd/MM/yyyy");
            row["ComplTime"] = job.UpdateDateTime.ToString("HH:mm");
            row["HblN"] = obj.HblNo;
            row["Qty"] = obj.Qty.ToString();
            row["PackType"] = obj.UomCode;
            row["M3"] = obj.Volume.ToString("0.000");
            row["Weight"] = obj.Weight.ToString("0.000");
            row["UserId"] = job.CreateBy;

            string value = SafeValue.SafeString("0.00");
            row["ForkLift"] = value;
            row["Dn"] = value;
            row["Tracing"] = value;
            row["Wh"] = value;
            row["Admin"] = value;
            row["Sr"] = value;
            row["Removal"] = value;
            row["Dg"] = value;
            row["Sec"] = value;
            row["LongLength"] = Helper.Safe.SafeDecimal(value).ToString("0.00");
            row["OverWeight"] = Helper.Safe.SafeDecimal(value).ToString("0.00");
            row["Sticker"] = Helper.Safe.SafeDecimal(value).ToString("0.00");
            row["Ot"] = value;
            row["Ugt"] = value;
            row["Misc"] = value;
            row["Other"] = value;
            //if (obj.WaiveAmt == 0)
            //    row["Waive"] = "0.00";
            //else
            //    row["Waive"] = "(" + obj.WaiveAmt.ToString("0.00") + ")";

            row["Total"] = value;
            row["OtherRemarks"] = obj.Remark1;
            row["IsCn"] = "DELIVERY NOTE/OFFICIAL RECEIPT";
            row["Cn"] = "";

            row["VesVoy"] = job.Vessel+" / "+job.Voyage;
            row["DoNo"] = "";
            row["ClearRmk"] = "";
            row["VehicleUser"] = "";
            row["VehicleTel"] = "";
            row["VehicleNric"] ="";
            row["VehicleNo1"] = "";
            row["VehicleNo2"] = "";
            row["VehicleNo3"] = "";
            NumberToMoney num = new NumberToMoney();
            string s = num.NumberToCurrency(Convert.ToDecimal(obj.Qty));
            row["QtyWord"] = s + " " + obj.UomCode;
            row["TotQty"] = obj.Qty;
            row["SpcRemark"] = "";
            row["InternalExternal"] = S.Text("") == "CO-LOAD" ? "Internal" : "External";


        }
        tab.Rows.Add(row);

        return tab;
    }
}