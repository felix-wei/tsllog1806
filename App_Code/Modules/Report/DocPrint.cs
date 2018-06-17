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
    private static DataTable InitMastDataTable(string name)
    {
        DataTable mast = new DataTable(name);
        mast.Columns.Add("JobNo");
        mast.Columns.Add("JobDate");
        mast.Columns.Add("DateLabel");
        mast.Columns.Add("JobType");
        mast.Columns.Add("Type");
        mast.Columns.Add("Haulier");
        mast.Columns.Add("Attn");
        mast.Columns.Add("PickupFrom");
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
        mast.Columns.Add("EtaTime");
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
        mast.Columns.Add("BookingDate");
        mast.Columns.Add("BookingTime");
        mast.Columns.Add("CompletionDate");

        mast.Columns.Add("TotalQty");
        mast.Columns.Add("TotalQtyOrig");
        mast.Columns.Add("TotalVolume");
        mast.Columns.Add("TotalWeight");
        mast.Columns.Add("TotalPack");

        return mast;
    }
    private static DataTable InitDetDataTable(string name)
    {
        DataTable det = new DataTable(name);
        det.Columns.Add("LineId");
        det.Columns.Add("TotalQty");
        det.Columns.Add("TotalM3");
        det.Columns.Add("No");
        det.Columns.Add("RefNo");
        det.Columns.Add("JobNo");
        det.Columns.Add("ContNo");
        det.Columns.Add("BookingNo");
        det.Columns.Add("StockDate");
        det.Columns.Add("VehicleNo");
        det.Columns.Add("HblNo");
        det.Columns.Add("OpsType");
        det.Columns.Add("Qty");
        det.Columns.Add("Uom");
        det.Columns.Add("QtyOrig");
        det.Columns.Add("WeightOrig");
        det.Columns.Add("VolumeOrig");
        det.Columns.Add("PackType");
        det.Columns.Add("SkuCode");
        det.Columns.Add("BookingItem");
        det.Columns.Add("ActualItem");
        det.Columns.Add("Weight");
        det.Columns.Add("PackQty");
        det.Columns.Add("PackUom");
        det.Columns.Add("BkgSkuQty");
        det.Columns.Add("BkgSkuUnit");
        det.Columns.Add("BkgSkuCode");
        det.Columns.Add("Volume");
        det.Columns.Add("Location");
        det.Columns.Add("Length");
        det.Columns.Add("Width");
        det.Columns.Add("Height");
        det.Columns.Add("Marking1");
        det.Columns.Add("Marking2");
        det.Columns.Add("Remark1");
        det.Columns.Add("Pod");
        det.Columns.Add("LengthPack");
        det.Columns.Add("WidthPack");
        det.Columns.Add("HeightPack");
        det.Columns.Add("BalVolume");
        det.Columns.Add("BalQty");
        det.Columns.Add("BalSKU");
        det.Columns.Add("BalWeight");
        det.Columns.Add("InventoryId");

        return det;
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
        mast.Columns.Add("PickupFrom");
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
        mast.Columns.Add("EtaTime");
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
        det.Columns.Add("FtSub");
        det.Columns.Add("FtCbm");
        det.Columns.Add("FtNom");

        #endregion

        DataRow row = mast.NewRow();
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        row["Attn"] = obj.ClientContact;
        row["Terminal"] = obj.Terminalcode;
        row["Portnet"] = obj.PortnetRef;
        row["PickupFrom"] = obj.PickupFrom;
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
        row["EtaTime"] = obj.EtaTime;
        sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pol, "") + "'";
        row["Pol"] = SafeValue.SafeString(Helper.Sql.One(sql),"");
        sql = "select name from xxparty where partyid='" +obj.CarrierId + "'";
        row["Carrier"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        row["OceanBl"] = obj.CarrierBkgNo;
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
            if (cont.ContainerType != null)
            {
                if (cont.ContainerType.IndexOf("20", 0) == 0)
                    ft20++;
                if (cont.ContainerType.IndexOf("40", 0) == 0)
                    ft40++;
                if (cont.ContainerType.IndexOf("45", 0) == 0)
                    ft45++;
            }
            row1["FtSize"] = ftSize;
            row1["FtType"] = cont.ContainerType;
            row1["FtSub"] = "";//cont.ContainerType;

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
	
	public static DataSet PrintHaulierSub(string orderNo,string jobType, string haulierCode)
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
        mast.Columns.Add("PickupFrom");
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
        mast.Columns.Add("EtaTime");
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
        det.Columns.Add("FtSub");
        det.Columns.Add("FtCbm");
        det.Columns.Add("FtNom");

        #endregion

        DataRow row = mast.NewRow();
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        row["Attn"] = obj.ClientContact;
        row["Terminal"] = obj.Terminalcode;
        row["Portnet"] = obj.PortnetRef;
        row["PickupFrom"] = obj.PickupFrom;
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
        row["EtaTime"] = obj.EtaTime;
        sql = "select name from [XXPort] where code='" + SafeValue.SafeString(obj.Pol, "") + "'";
        row["Pol"] = SafeValue.SafeString(Helper.Sql.One(sql),"");
        sql = "select name from xxparty where partyid='" +obj.CarrierId + "'";
        row["Carrier"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        row["OceanBl"] = obj.CarrierBkgNo;
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
            if (cont.ContainerType != null)
            {
                if (cont.ContainerType.IndexOf("20", 0) == 0)
                    ft20++;
                if (cont.ContainerType.IndexOf("40", 0) == 0)
                    ft40++;
                if (cont.ContainerType.IndexOf("45", 0) == 0)
                    ft45++;
            }
            row1["FtSize"] = ftSize;
            row1["FtType"] = cont.ContainerType;
            row1["FtSub"] = "";//cont.ContainerType;

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
	
    public static DataSet PrintTallySheet(string orderNo, string jobType,string type)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        #region init column
        DataTable mast =  InitMastDataTable("Mast");
        DataTable det = InitDetDataTable("Details");
        #endregion

        #region Data
        string contNo = "";
        string contType = "";

        DataRow row = mast.NewRow();
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        row["JobDate"] = SafeValue.SafeDateStr(obj.JobDate);
        row["Attn"] = obj.Terminalcode;
        row["DeliveryTo"] = obj.DeliveryTo;
        row["CrNo"] = "";
        row["Contact1"] = "";
        row["Contact2"] = "";
        row["Tel1"] = "";
        row["Fax1"] = "";
        if (type == "IN")
        {
            row["Type"] = "INCOMING TALLY SHEET";
            row["DateLabel"] = "Incoming Date";
        }
        if (type == "OUT")
        {
            row["Type"] = "OUTGOMING TALLY SHEET";
            row["DateLabel"] = "Outgoming Date";
        }
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

        row["Etd"] = SafeValue.SafeDateStr(obj.EtdDate);
        row["Eta"] = SafeValue.SafeDateStr(obj.EtaDate);
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

        sql = string.Format(@"select ContainerNo,ContainerType,CompletionDate from Ctm_JobDet1 where JobNo='{0}'", orderNo);
        DataTable tab_cont = ConnectSql_mb.GetDataTable(sql);
        for (int a = 0; a < tab_cont.Rows.Count; a++)
        {
            contNo = SafeValue.SafeString(tab_cont.Rows[a]["ContainerNo"]);
            contType = SafeValue.SafeString(tab_cont.Rows[a]["ContainerType"]);
            row["ContNo"] += contNo + " " + contType + " / ";
            row["CompletionDate"] = SafeValue.SafeDateStr(tab_cont.Rows[a]["CompletionDate"]);
        }

        decimal totalQty = 0;
        decimal totalQtyOrig = 0;
        decimal totalSKUQty = 0;
        decimal totalWeight = 0;
        decimal totalVolume = 0;
        sql = string.Format(@"select tab_h.Id,tab_h.BookingNo,tab_h.ContNo,tab_h.RefNo,tab_h.JobNo,tab_h.StockDate,tab_h.HblNo,tab_h.TripId,
tab_h.Length,tab_h.Width,tab_h.Height,tab_h.Weight,tab_h.BkgSKuCode,tab_h.BkgSkuUnit,tab_h.PackTypeOrig,tab_h.SkuCode,tab_h.BookingItem,tab_h.ActualItem,
tab_h.UomCode,tab_h.PackUom,tab_h.Remark1,tab_h.Marking1,tab_h.Marking2,tab_h.LengthPack,tab_h.WidthPack,tab_h.HeightPack,InventoryId,tab_n.TotalQty as Qty,
tab_h.BkgSkuQty,tab_h.Weight,tab_h.Volume,tab_n.TotalQtyOrig as QtyOrig,tab_h.PackQty,tab_n.TotalVolumeOrig  as VolumeOrig,tab_n.TotalWeightOrig  as WeightOrig,tab_bal.*,tab_n.* from (
select LineId,
SUM(case when CargoType='{1}' then Qty else 0 end) as TotalQty,
SUM(case when CargoType='{1}' then BkgSkuQty else 0 end) as TotalBkgSkuQty,
SUM(case when CargoType='{1}' then Weight else 0 end) as TotalWeight,
SUM(case when CargoType='{1}' then Volume else 0 end) as TotalVolume,
SUM(case when CargoType='{1}' then QtyOrig else 0 end) as TotalQtyOrig,
SUM(case when CargoType='{1}' then PackQty else 0 end) as TotalPackQty,
SUM(case when CargoType='{1}' then VolumeOrig else 0 end) as TotalVolumeOrig
,SUM(case when CargoType='{1}' then WeightOrig else 0 end) as TotalWeightOrig
,SUM(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty
,SUM(case when CargoType='IN' then PackQty else -PackQty end) as BalPack
,SUM(case when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalVolume
,SUM(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalWeight
 from job_house h where JobNo='{0}' and CargoType='{1}' and CargoStatus='C' group by LineId) as tab_n
left join(select LineId,SUM(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty
,SUM(case when CargoType='IN' then PackQty else -QtyOrig end) as BalPack
,SUM(case when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalVolume
,SUM(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalWeight from job_house where CargoStatus='C' group by LineId) as tab_bal
on tab_n.LineId=tab_bal.LineId ", orderNo, type);
        if (type == "IN")
        {
            sql +=string.Format(@"
inner join(select * from job_house where JobNo='{0}' and CargoType='{1}') as tab_h on tab_h.LineId = tab_bal.LineId order by tab_n.LineId", orderNo, type);
        }
        if (type == "OUT")
        {
            sql += string.Format(@"
inner join(select * from job_house) as tab_h on tab_h.Id = tab_bal.LineId order by tab_n.LineId");
        }
        //throw new Exception(sql);
        DataTable dt_det = ConnectSql_mb.GetDataTable(sql);
        
        for (int i = 0; i < dt_det.Rows.Count; i++)
        {
            DataRow rowDet = dt_det.Rows[i];
            DataRow row1  = det.NewRow();
            decimal qty = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["Qty"], 0), 1);
            decimal qtyOrig = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["QtyOrig"], 0), 1);

            //Booking
            row1["Length"] = rowDet["Length"];
            row1["Width"] = rowDet["Width"];
            row1["Height"] = rowDet["Height"];
            row1["LengthPack"] = rowDet["LengthPack"];
            row1["WidthPack"] = rowDet["WidthPack"];
            row1["HeightPack"] = rowDet["HeightPack"];

            row1["Weight"] = rowDet["Weight"];
            row1["Volume"] = rowDet["Volume"];
            row1["WeightOrig"] = rowDet["WeightOrig"];
            row1["VolumeOrig"] = rowDet["VolumeOrig"];
            row1["Qty"] = qty;
            row1["QtyOrig"] = qtyOrig;
            row1["Uom"] = rowDet["UomCode"];
            row1["PackType"] = rowDet["PackTypeOrig"];
            row1["Uom"] = rowDet["UomCode"];
            row1["Uom"] = rowDet["UomCode"];
            row1["BkgSkuQty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["BkgSkuQty"]), 1);
            row1["BkgSkuUnit"] = rowDet["BkgSkuUnit"];
            row1["BkgSkuCode"] = rowDet["BkgSkuCode"];
            row1["BookingItem"] = rowDet["BookingItem"];


            row1["No"] = n + 1;
            row1["LineId"] = rowDet["LineId"];
            row1["ContNo"] = rowDet["ContNo"];
            row1["RefNo"] = rowDet["RefNo"];
            row1["JobNo"] = rowDet["JobNo"];
            row1["StockDate"] = SafeValue.SafeDateStr(rowDet["StockDate"]);
            row1["JobNo"] = rowDet["JobNo"];
            row1["BookingNo"] = rowDet["BookingNo"];
            row1["HblNo"] = rowDet["HblNo"];
            row1["BookingItem"] = rowDet["BookingItem"];
            row1["InventoryId"] = rowDet["InventoryId"];
            row1["Remark1"] = rowDet["Remark1"];
            row1["Marking1"] = rowDet["Marking1"];
            row1["Marking2"] = rowDet["Marking2"];

            

            if (type == "OUT")
            {
                row1["BalQty"] = rowDet["BalQty"];
                row1["BalSKU"] = rowDet["BalPack"];
                row1["BalWeight"] = rowDet["BalWeight"];
                row1["BalVolume"] = rowDet["BalVolume"];
            }
            sql = string.Format(@"select TowheadCode from ctm_jobdet2 where Id={0}", SafeValue.SafeInt(rowDet["TripId"], 0));
            row1["VehicleNo"] = SafeValue.SafeString(Helper.Sql.One(sql), "");

            if (qty > 0 ) {
                det.Rows.Add(row1);
            }
            if (qtyOrig > 0 && qty == 0)
            {

                row1["QtyOrig"] = qtyOrig;
                row1["VolumeOrig"] = SafeValue.SafeDecimal(rowDet["VolumeOrig"], 0);
                row1["WeightOrig"] = rowDet["WeightOrig"];
                row1["PackType"] = rowDet["PackTypeOrig"];
                row1["PackQty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["PackQty"]), 1);
                row1["PackUom"] = rowDet["PackUom"];
                row1["ActualItem"] = rowDet["ActualItem"];
                row1["SkuCode"] = rowDet["SkuCode"];
                row1["LengthPack"] = rowDet["LengthPack"];
                row1["WidthPack"] = rowDet["WidthPack"];
                row1["HeightPack"] = rowDet["HeightPack"];

                det.Rows.Add(row1);
            }
            totalQty += SafeValue.SafeDecimal(rowDet["Qty"], 0);
            totalQtyOrig+= SafeValue.SafeDecimal(rowDet["QtyOrig"], 0);
            totalSKUQty += SafeValue.SafeDecimal(rowDet["PackQty"], 0);
            totalWeight += SafeValue.SafeDecimal(rowDet["WeightOrig"], 0);
            totalVolume += SafeValue.SafeDecimal(rowDet["VolumeOrig"], 0);  
                   
           
        }
        row["TotalQty"] =SafeValue.ChinaRound(totalQty,1);
        row["TotalQtyOrig"] = SafeValue.ChinaRound(totalQtyOrig,1);
        row["TotalPack"] = totalSKUQty;
        row["TotalVolume"] = totalVolume;
        row["TotalWeight"] = totalWeight;

        mast.Rows.Add(row);
        #endregion
      
        set.Tables.Add(mast);
        set.Tables.Add(det);
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

        mast.Columns.Add("DoNo");
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
        mast.Columns.Add("DeliveryRemark");
        mast.Columns.Add("BillingRemark");
        mast.Columns.Add("Remark1");
        mast.Columns.Add("PodType");
        mast.Columns.Add("StartTime");
        mast.Columns.Add("EndTime");
        mast.Columns.Add("ContNo");
        mast.Columns.Add("TrailerRemark");
        mast.Columns.Add("DriverCode");
        mast.Columns.Add("TripN");
        mast.Columns.Add("OT");
        mast.Columns.Add("EpodSignedBy");

        mast.Columns.Add("TowheadCode");
        mast.Columns.Add("RequestVehicleType");
        mast.Columns.Add("RequestTrailerType");
        mast.Columns.Add("FromDate");
        mast.Columns.Add("FromTime");
        mast.Columns.Add("ToDate");
        mast.Columns.Add("ToTime");
        mast.Columns.Add("ToCode");

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
        if (jobType == "do"||jobType=="CRA")
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

        #region Crane Job

        sql = string.Format(@"select TowheadCode,RequestVehicleType,FromDate,ToDate,ToTime,ToCode,DeliveryRemark,BillingRemark,Remark from cmt_jobdet2 where ");
        #endregion
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
            row1["Marking1"] = house.Marking1;
            row1["Marking2"] = house.Marking2;
            total += house.QtyOrig;
            det.Rows.Add(row1);
        }
        row["TotalQty"] = total;

        string sql_trip = string.Format(@"select top 1 Id,DeliveryRemark,BillingRemark,Remark1,PodType,DriverCode,OtHour,epodTrip,epodSignedBy,ContainerNo,RequestTrailerType from ctm_jobdet2 where JobNo='{0}'", orderNo);
        DataTable dt_trip = ConnectSql_mb.GetDataTable(sql_trip);
        if(dt_trip.Rows.Count>0){
            DataRow row_trip=dt_trip.Rows[0];
            row["DeliveryRemark"] = row_trip["DeliveryRemark"].ToString();
            row["BillingRemark"] = row_trip["BillingRemark"].ToString();
            row["Remark1"] = row_trip["Remark1"].ToString();
            row["PodType"] = row_trip["PodType"].ToString();
            
            row["DriverCode"] = row_trip["DriverCode"].ToString();
            row["TripN"] = row_trip["epodTrip"].ToString();
            row["OT"] = row_trip["OtHour"].ToString();
            row["EpodSignedBy"] = row_trip["epodSignedBy"].ToString();
            row["TrailerRemark"] = row_trip["RequestTrailerType"].ToString();
        }

        sql = string.Format(@"select ContainerNo,Remark,ScheduleStartDate,CompletionDate from ctm_jobdet1 where JobNo='{0}'", orderNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow row_cnt = dt.Rows[0];
            row["ContNo"] = row_cnt["ContainerNo"].ToString();
            //row["TrailerRemark"] = row_cnt["Remark"].ToString();
            row["StartTime"] = SafeValue.SafeDateStr(row_cnt["ScheduleStartDate"]);
            row["EndTime"] = SafeValue.SafeDateStr(row_cnt["CompletionDate"]);
        }
        mast.Rows.Add(row);

        set.Tables.Add(mast);
        set.Tables.Add(det);

        return set;
    }
    public static DataSet PrintJobSheet(string orderNo, string jobType,string tripId)
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
        mast.Columns.Add("ClientContact");
        mast.Columns.Add("EmailAddress");
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
        mast.Columns.Add("Remark1");
        mast.Columns.Add("PodType");
        mast.Columns.Add("StartTime");
        mast.Columns.Add("EndTime");
        mast.Columns.Add("ContNo");
        mast.Columns.Add("TrailerRemark");
        mast.Columns.Add("DriverCode");
        mast.Columns.Add("TripN");
        mast.Columns.Add("OT");
        mast.Columns.Add("EpodSignedBy");
        mast.Columns.Add("StampUser");
        mast.Columns.Add("TowheadCode");
        mast.Columns.Add("RequestVehicleType");
        mast.Columns.Add("FromDate");
        mast.Columns.Add("FromTime");
        mast.Columns.Add("ToDate");
        mast.Columns.Add("ToTime");
        mast.Columns.Add("ToCode");
        mast.Columns.Add("DeliveryRemark");
        mast.Columns.Add("BillingRemark");
        mast.Columns.Add("EpodCB1");
        mast.Columns.Add("EpodCB2");
        mast.Columns.Add("EpodCB3");
        mast.Columns.Add("EpodCB4");
        mast.Columns.Add("EpodCB5");
        mast.Columns.Add("EpodCB6");
        mast.Columns.Add("ManPowerNo");
        mast.Columns.Add("ExcludeLunch");

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
        row["ClientContact"] = obj.ClientContact;
        row["EmailAddress"] = obj.EmailAddress;
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
        row["Terminal"]="";
        row["LastDate"]="";
        row["Portnet"]="";
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

        row["StampUser"] = HttpContext.Current.User.Identity.Name;

        #region Crane Job

        ObjectQuery query2 = new ObjectQuery(typeof(C2.CtmJobDet2), "Id=" + tripId + "", "");
        ObjectSet objSet2 = C2.Manager.ORManager.GetObjectSet(query2);
        if (objSet2.Count == 0 || objSet2[0] == null) return set;
        C2.CtmJobDet2 det2 = objSet2[0] as C2.CtmJobDet2;
        row["JobNo"] = det2.TripIndex;
        row["TowheadCode"]=det2.TowheadCode;
        row["RequestVehicleType"]=det2.RequestVehicleType;
        row["FromDate"]=SafeValue.SafeDateStr(det2.FromDate);
        row["FromTime"]=det2.FromTime;
        row["ToDate"]=SafeValue.SafeDateStr(det2.ToDate);
        row["ToTime"]=det2.ToTime;
        row["ToCode"]=det2.ToCode;
        row["DeliveryRemark"]=det2.DeliveryRemark;
        row["BillingRemark"]=det2.BillingRemark;
        row["Remark"] = det2.Remark;
        row["EpodCB1"] = det2.EpodCB1;
        row["EpodCB2"] = det2.EpodCB2;
        row["EpodCB3"] = det2.EpodCB3;
        row["EpodCB4"] = det2.EpodCB4;
        row["EpodCB5"] = det2.EpodCB5;
        row["EpodCB6"] = det2.EpodCB6;
        row["ManPowerNo"] = det2.ManPowerNo;
        if(det2.ExcludeLunch=="Yes")
           row["ExcludeLunch"] = "Exclude Lunch";
        else
           row["ExcludeLunch"] = "Include Lunch";
        #endregion
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
    public static DataSet PrintJobStockTallySheet(string orderNo, string jobType)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");
        mast.Columns.Add("JobNo");
        mast.Columns.Add("Title");

        det.Columns.Add("ContainerNo");
        det.Columns.Add("SealNo");
        det.Columns.Add("ContainerType");
        det.Columns.Add("JobNo");
        det.Columns.Add("Index");
        det.Columns.Add("Description");
        det.Columns.Add("Length");
        det.Columns.Add("Width");
        det.Columns.Add("Height");
        det.Columns.Add("Qty");
        det.Columns.Add("TotalVol");
        det.Columns.Add("PackQty");
        det.Columns.Add("SkuQty");
        det.Columns.Add("TotalWt");
        det.Columns.Add("Weight");
        det.Columns.Add("Remark");
        det.Columns.Add("Volume");
        det.Columns.Add("TotalQty");
        det.Columns.Add("TotalM3");
        det.Columns.Add("TotalSku");

        #endregion

        #region Data
        DataRow row = mast.NewRow();
        row["Title"] = "STOCK TALLY SHEET";


        string cargoType="OUT";
        if (jobType == "IMP" || jobType == "WGR") {
            cargoType = "IN";
        }
        string sql = string.Format(@"select det1.ContainerNo,det1.SealNo,det1.ContainerType,mast.* from job_house mast left join ctm_jobdet1 det1 on mast.JobNo=det1.JobNo and mast.ContId=det1.Id where mast.JobNo='{0}' order by mast.LineId asc", orderNo, cargoType);
        DataTable dt_det = ConnectSql_mb.GetDataTable(sql);
        decimal totalQty = 0;
        decimal totalM3 = 0;
        decimal totalSkuQty = 0;
        int n = 1;
        for (int i = 0; i < dt_det.Rows.Count; i++)
        {
            DataRow rowDet = dt_det.Rows[i];
            
            int id = SafeValue.SafeInt(rowDet["Id"],0);            
            string sql_d = string.Format(@"select * from Dimension where HouseId={0} order by Id asc",id);
            DataTable dt_d=ConnectSql_mb.GetDataTable(sql_d);
            for (int j = 0; j < dt_d.Rows.Count; j++)
            {
                DataRow row_d = dt_d.Rows[j];
                DataRow row1 = det.NewRow();
                decimal qty = SafeValue.SafeDecimal(row_d["Qty"], 0);
                decimal m3 = SafeValue.SafeDecimal(row_d["TotalVol"], 0);
                decimal skuQty = SafeValue.SafeDecimal(row_d["SkuQty"], 0);


                row1["ContainerNo"] = rowDet["ContainerNo"];
                row1["SealNo"] = rowDet["SealNo"];
                row1["ContainerType"] = rowDet["ContainerType"];
                row1["JobNo"] = rowDet["JobNo"];

                row1["Index"] = n;
                row1["Length"] = row_d["Length"];
                row1["Width"] = row_d["Width"];
                row1["Height"] = row_d["Height"];
                row1["Weight"] = C2.Dimension.get_weight(id,skuQty);
                row1["Volume"] = row_d["Volume"];
                row1["TotalWt"] = row_d["TotalWt"];
                row1["Qty"] =SafeValue.SafeInt(qty,0);
                row1["Description"] =row_d["Description"];
                row1["Remark"] = row_d["Remark"];
                row1["PackQty"] = SafeValue.SafeInt(row_d["PackQty"],0);
                row1["SkuQty"] = SafeValue.SafeInt(row_d["SkuQty"],0);
                row1["TotalVol"] = m3;

                det.Rows.Add(row1);
            }
            n++;
        }
        mast.Rows.Add(row);
        #endregion


        set.Tables.Add(mast);
        set.Tables.Add(det);
        return set;
    }
    public static DataSet PrintStockTallySheet(string orderNo, DateTime  from,DateTime to)
    {
        DataSet set = new DataSet();
        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");
        mast.Columns.Add("JobNo");
        mast.Columns.Add("Title");

        det.Columns.Add("ContainerNo");
        det.Columns.Add("SealNo");
        det.Columns.Add("ContainerType");
        det.Columns.Add("JobNo");
        det.Columns.Add("Index");
        det.Columns.Add("Description");
        det.Columns.Add("Length");
        det.Columns.Add("Width");
        det.Columns.Add("Height");
        det.Columns.Add("Qty");
        det.Columns.Add("TotalVol");
        det.Columns.Add("PackQty");
        det.Columns.Add("SkuQty");
        det.Columns.Add("TotalWt");
        det.Columns.Add("Weight");
        det.Columns.Add("Remark");
        det.Columns.Add("Volume");
        det.Columns.Add("TotalQty");
        det.Columns.Add("TotalM3");
        det.Columns.Add("TotalSku");

        #endregion
        ObjectQuery query = null;
        ObjectSet objSet = null;
        C2.CtmJob obj = null;
        if (orderNo.Length > 0)
        {
            DataRow row = mast.NewRow();
            query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
            objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count == 0 || objSet[0] == null) return set;
            obj = objSet[0] as C2.CtmJob;
            #region Data
            
            row["Title"] = "STOCK TALLY SHEET";


            string cargoType = "OUT";
            if (obj.JobType == "IMP" || obj.JobType == "WGR"||obj.JobType=="LI"||obj.JobType=="TPT")
            {
                cargoType = "IN";
            }
            if (orderNo.Length == 0)
            {
                orderNo = obj.JobNo;
            }
            string sql = string.Format(@"select det1.ContainerNo,det1.SealNo,det1.ContainerType,mast.* from job_house mast left join ctm_jobdet1 det1 on mast.JobNo=det1.JobNo and mast.ContNo=det1.ContainerNo where mast.JobNo='{0}' and CargoType='{1}' order by mast.Id asc", orderNo, cargoType);
            DataTable dt_det = ConnectSql_mb.GetDataTable(sql);
            decimal totalQty = 0;
            decimal totalM3 = 0;
            decimal totalSkuQty = 0;
            int n = 1;
            for (int i = 0; i < dt_det.Rows.Count; i++)
            {
                DataRow rowDet = dt_det.Rows[i];

                int id = SafeValue.SafeInt(rowDet["Id"], 0);
                string sql_d = string.Format(@"select * from Dimension where HouseId={0} order by Id asc", id);
                DataTable dt_d = ConnectSql_mb.GetDataTable(sql_d);
                for (int j = 0; j < dt_d.Rows.Count; j++)
                {
                    DataRow row_d = dt_d.Rows[j];
                    DataRow row1 = det.NewRow();
                    decimal qty = SafeValue.SafeDecimal(row_d["Qty"], 0);
                    decimal m3 = SafeValue.SafeDecimal(row_d["TotalVol"], 0);
                    decimal skuQty = SafeValue.SafeDecimal(row_d["SkuQty"], 0);


                    row1["ContainerNo"] = rowDet["ContainerNo"];
                    row1["SealNo"] = rowDet["SealNo"];
                    row1["ContainerType"] = rowDet["ContainerType"];
                    row1["JobNo"] = rowDet["JobNo"];

                    row1["Index"] = n;
                    row1["Length"] = row_d["Length"];
                    row1["Width"] = row_d["Width"];
                    row1["Height"] = row_d["Height"];
                    row1["Weight"] = C2.Dimension.get_weight(id, skuQty);
                    row1["Volume"] = row_d["Volume"];
                    row1["TotalWt"] = row_d["TotalWt"];
                    row1["Qty"] = SafeValue.SafeInt(qty, 0);
                    row1["Description"] = row_d["Description"];
                    row1["Remark"] = row_d["Remark"];
                    row1["PackQty"] = SafeValue.SafeInt(row_d["PackQty"], 0);
                    row1["SkuQty"] = SafeValue.SafeInt(row_d["SkuQty"], 0);
                    row1["TotalVol"] = m3;

                    det.Rows.Add(row1);
                }
                n++;
            }
            mast.Rows.Add(row);
            #endregion
        }
        else
        {
            query = new ObjectQuery(typeof(C2.CtmJob), "JobDate >'" + from + "' and JobDate<'" + to + "'", "");
            objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count >0)
            {
                for (int a = 0; a < objSet.Count; a++)
                {
                    obj = objSet[a] as C2.CtmJob;
                    #region Data
                    DataRow row = mast.NewRow();
                    row["Title"] = "STOCK TALLY SHEET";


                    string cargoType = "OUT";
                    if (obj.JobType == "IMP" || obj.JobType == "WGR")
                    {
                        cargoType = "IN";
                    }
                    if (orderNo.Length == 0)
                    {
                        orderNo = obj.JobNo;
                    }
                    string sql = string.Format(@"select det1.ContainerNo,det1.SealNo,det1.ContainerType,mast.* from job_house mast left join ctm_jobdet1 det1 on mast.JobNo=det1.JobNo and mast.ContNo=det1.ContainerNo where mast.JobNo='{0}' and CargoType='{1}' order by mast.Id asc", orderNo, cargoType);
                    DataTable dt_det = ConnectSql_mb.GetDataTable(sql);
                    decimal totalQty = 0;
                    decimal totalM3 = 0;
                    decimal totalSkuQty = 0;
                    int n = 1;
                    if (dt_det.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_det.Rows.Count; i++)
                        {
                            DataRow rowDet = dt_det.Rows[i];

                            int id = SafeValue.SafeInt(rowDet["Id"], 0);
                            string sql_d = string.Format(@"select * from Dimension where HouseId={0} order by Id asc", id);
                            DataTable dt_d = ConnectSql_mb.GetDataTable(sql_d);
                            for (int j = 0; j < dt_d.Rows.Count; j++)
                            {
                                DataRow row_d = dt_d.Rows[j];
                                DataRow row1 = det.NewRow();
                                decimal qty = SafeValue.SafeDecimal(row_d["Qty"], 0);
                                decimal m3 = SafeValue.SafeDecimal(row_d["TotalVol"], 0);
                                decimal skuQty = SafeValue.SafeDecimal(row_d["SkuQty"], 0);


                                row1["ContainerNo"] = rowDet["ContainerNo"];
                                row1["SealNo"] = rowDet["SealNo"];
                                row1["ContainerType"] = rowDet["ContainerType"];
                                row1["JobNo"] = rowDet["JobNo"];

                                row1["Index"] = n;
                                row1["Length"] = row_d["Length"];
                                row1["Width"] = row_d["Width"];
                                row1["Height"] = row_d["Height"];
                                row1["Weight"] = C2.Dimension.get_weight(id, skuQty);
                                row1["Volume"] = row_d["Volume"];
                                row1["TotalWt"] = row_d["TotalWt"];
                                row1["Qty"] = SafeValue.SafeInt(qty, 0);
                                row1["Description"] = row_d["Description"];
                                row1["Remark"] = row_d["Remark"];
                                row1["PackQty"] = SafeValue.SafeInt(row_d["PackQty"], 0);
                                row1["SkuQty"] = SafeValue.SafeInt(row_d["SkuQty"], 0);
                                row1["TotalVol"] = m3;

                                det.Rows.Add(row1);
                            }
                            n++;
                        }
                        mast.Rows.Add(row);
                    }
                
                    #endregion
                }
            }

        }
        set.Tables.Add(mast);
        set.Tables.Add(det);
        return set;
    }
    public static DataSet PrintStockSummary(string client, DateTime from) {
        DataSet set = new DataSet();
        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");
        mast.Columns.Add("Date");
        mast.Columns.Add("Date1");
        mast.Columns.Add("Company");
        mast.Columns.Add("Address");
        mast.Columns.Add("Tel");
        mast.Columns.Add("CrNo");

        det.Columns.Add("SN");
        det.Columns.Add("JobDate");
        det.Columns.Add("JobType");
        det.Columns.Add("BookingNo");
        det.Columns.Add("Period");
        det.Columns.Add("Att1");
        det.Columns.Add("BayNo");
        det.Columns.Add("Att2");
        det.Columns.Add("Att3");
        det.Columns.Add("Att4");
        det.Columns.Add("Att5");
        det.Columns.Add("Att6");
        det.Columns.Add("Vessel");
		
		det.Columns.Add("InQty");
        det.Columns.Add("StockQty");
        det.Columns.Add("RecQty");
        det.Columns.Add("ShipQty");
        det.Columns.Add("BalQty");
        det.Columns.Add("Remark");

        #endregion
        DataRow row = mast.NewRow();
        row["Date"] = from;
        if (client.Length > 0)
        {
            #region Data
            row["Date"] = from.Day + "th" + " " + from.ToString("MMMM yyyy");
            row["Date1"] =from.ToString("dd-MM-yyyy");
            string company = EzshipHelper.GetPartyName(client);
            row["Company"] = company;
            string sql = string.Format(@"select Address,Tel1,Fax1,CrNo from XXParty where PartyId=@PartyId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@PartyId", client, SqlDbType.NVarChar));
            DataTable dt_p = ConnectSql_mb.GetDataTable(sql, list);
            if (dt_p.Rows.Count > 0)
            {
                DataRow row_p = dt_p.Rows[0];
                string address = row_p["Address"].ToString();
                string tel1 = row_p["Tel1"].ToString();
                string fax1 = row_p["Fax1"].ToString();
                string crNo = row_p["CrNo"].ToString();
                row["Address"] = address;
                row["Tel"] = "Tel:(65)" + tel1 + " Fax:(65)" + fax1;
                row["CrNo"] = "Co Regn No." + crNo;
            }
            mast.Rows.Add(row);

            //            sql = string.Format(@"select dense_rank()over( order by ActualItem,BookingNo) as rowId,*,InQty,OutQty,InQty-ISNULL(OutQty,0) as BalQty from (select tab.ActualItem,tab.BookingNo,tab_in.InQty,tab.RecQty,tab_out.OutQty,job.Vessel,job.PartyId,tab.StockDate,tab.Location,tab.CargoType,tab.Remark1,tab.SkuCode,tab.Commodity from 
            //(select LineId,ActualItem, BookingNo,max(Marking2) as Marking2,Max(Commodity) as Commodity,max(CargoType) CargoType,max(SkuCode) as SkuCode,
            //SUM(case when CargoType='IN' then QtyOrig else 0 end) as RecQty,
            //MAX(JobNo) as JobNo,StockDate,Location,max(Remark1) as Remark1 from job_house
            //where ClientId='{0}' and CargoStatus='C' and CONVERT(date, StockDate,103)<='{1}'  group by ActualItem,BookingNo,Location,StockDate,LineId) as tab
            //inner join CTM_Job job on tab.JobNo=job.JobNo
            //left join (select SUM(case when CargoType='IN' then QtyOrig else 0 end) as InQty,ActualItem,BookingNo from job_house group by ActualItem,BookingNo) as tab_in on tab_in.ActualItem=tab.ActualItem and tab_in.BookingNo=tab.BookingNo
            //left join (select SUM(case when CargoType='OUT' then QtyOrig else 0 end) as OutQty,ActualItem,BookingNo,Location,LineId from job_house group by ActualItem,BookingNo,Location,LineId) as tab_out on tab_out.LineId=tab.LineId
            //) as tab order by rowId,StockDate", client, from);
            sql = string.Format(@"select distinct dense_rank()over( order by ActualItem,BookingNo) as rowId,mast.LineId,mast.StockDate,BookingNo,ActualItem,QtyOrig,Marking2,Remark1,CargoType,Commodity,SkuCode,Location,job.Vessel,job.PartyId,
(select SUM(case when CargoType='IN' then QtyOrig else 0 end) from job_house where ActualItem=mast.ActualItem and BookingNo=mast.BookingNo group by ActualItem,BookingNo) as InQty,
(select SUM(case when CargoType='IN' then QtyOrig else 0 end) from job_house where LineId=mast.LineId group by LineId) as RecQty,
tab_out.OutQty,(select SUM(case when CargoType='IN' then QtyOrig else -QtyOrig end) from job_house where ActualItem=mast.ActualItem and BookingNo=mast.BookingNo group by ActualItem,BookingNo) as BalQty 
from job_house mast inner join CTM_Job job on mast.JobNo=job.JobNo
left join (select SUM(case when CargoType='OUT' then QtyOrig else 0 end) as OutQty,StockDate,LineId from job_house group by LineId,StockDate) as tab_out on tab_out.LineId=mast.LineId and tab_out.StockDate=mast.StockDate
where mast.ClientId='{0}' and CargoStatus='C' and CONVERT(date, mast.StockDate,103)<='{1}' order by rowId,LineId,StockDate asc,QtyOrig desc
", client, from);
            string lastBookingNo = "";
            DataTable dt_det = ConnectSql_mb.GetDataTable(sql);
            int rowId = 0;
            int lineId = 0;
            int lastRowId = 0;
            int lastLineId = 0;
            for (int i = 0; i < dt_det.Rows.Count; i++)
            {
                DataRow rowDet = dt_det.Rows[i];
                string cargoType = SafeValue.SafeString(rowDet["CargoType"]);
                string commodity = SafeValue.SafeString(rowDet["Commodity"]);
                DataRow row1 = det.NewRow();
                string BookingNo = SafeValue.SafeString(rowDet["BookingNo"]);
                rowId = SafeValue.SafeInt(rowDet["rowId"],0);
                lineId = SafeValue.SafeInt(rowDet["LineId"], 0);
                string jobDate = SafeValue.SafeDate(rowDet["StockDate"], DateTime.Today).ToString("dd/MM/yyyy");
               
                
                row1["JobType"] = commodity;
                row1["BookingNo"] = BookingNo;
                string skuCode = SafeValue.SafeString(rowDet["SkuCode"]);
                string itemCode = SafeValue.SafeString(rowDet["ActualItem"]);
                string[] atts = skuCode.Split('-');
                if (itemCode.Length > 0)
                {
                    sql = string.Format(@"select Att1,Att2,Att3,Att4,Att5,Att6 from ref_product where Code='{0}'", itemCode);
                    DataTable dt_att = ConnectSql_mb.GetDataTable(sql);
                    if (dt_att.Rows.Count > 0)
                    {
                        DataRow row_att = dt_att.Rows[0];
                        row1["Att1"] = row_att["Att1"];
                        row1["Att2"] = row_att["Att2"];
                        row1["Att3"] = row_att["Att3"];
                        row1["Att4"] = row_att["Att4"];
                        row1["Att5"] = row_att["Att5"];
                        row1["Att6"] = row_att["Att6"];
                    }
                }
                if (skuCode.Length > 0)
                {
                    sql = string.Format(@"select Att1,Att2,Att3,Att4,Att5,Att6 from ref_product where Code='{0}'", skuCode);
                    DataTable dt_att = ConnectSql_mb.GetDataTable(sql);
                    if (dt_att.Rows.Count > 0)
                    {
                        DataRow row_att = dt_att.Rows[0];
                        row1["Att1"] = row_att["Att1"];
                        row1["Att2"] = row_att["Att2"];
                        row1["Att3"] = row_att["Att3"];
                        row1["Att4"] = row_att["Att4"];
                        row1["Att5"] = row_att["Att5"];
                        row1["Att6"] = row_att["Att6"];
                    }
                }
                string[] s1 = jobDate.Split('/');
                int day = SafeValue.SafeInt(s1[0], 0);
                int month = SafeValue.SafeInt(DateTime.Today.Month, 0);
                int year = SafeValue.SafeInt(DateTime.Today.Year, 0);
                DateTime d1 = new DateTime(year, month, day);
                DateTime d2 = new DateTime(year, month + 1, day);
                row1["Period"] = d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
                row1["BayNo"] = SafeValue.SafeString(rowDet["Location"]);
                row1["Vessel"] = SafeValue.SafeString(rowDet["Vessel"]);
                if (cargoType == "IN" && lastRowId != rowId)
                {
                    if (lastBookingNo != BookingNo&&i>0)
                    {
                        DataRow row4 = det.NewRow();
                        det.Rows.Add(row4);
                    }
                    row1["SN"] = rowId;
                    row1["JobDate"] = jobDate;
                    row1["InQty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["InQty"]), 1);
                    //row1["RecQty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["RecQty"]), 1);
                    row1["BalQty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["BalQty"]), 1);
                    row1["Remark"] = rowDet["Remark1"];
                    det.Rows.Add(row1);

                }
                else if (cargoType == "IN" && lastLineId != lineId)
                {
                    DataRow row2 = det.NewRow();
                    row2["JobDate"] = jobDate;
                    row2["RecQty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["RecQty"]), 1);
                    row2["Remark"] = rowDet["Remark1"];
                    det.Rows.Add(row2);
                }
                else if (cargoType == "OUT")
                {
                    DataRow row3 = det.NewRow();
                    row3["JobDate"] = jobDate;
                    row3["ShipQty"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["OutQty"]), 1);
                    row3["Remark"] = rowDet["Remark1"];
                    det.Rows.Add(row3);
                }
                
                lastBookingNo = BookingNo;
                lastRowId = rowId;
                lastLineId = lineId;
            }
            #endregion
        }
        set.Tables.Add(mast);
        set.Tables.Add(det);
        return set;
    }
    public static DataSet PrintMovementDetail(string orderNo, string sku)
    {
        DataSet ds = new DataSet();
        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");
        mast.Columns.Add("RefNo");
        mast.Columns.Add("ClientId");
        mast.Columns.Add("ClientName");
        mast.Columns.Add("HaulierId");
        mast.Columns.Add("HaulierName");
        mast.Columns.Add("Prefix");
        mast.Columns.Add("RunDate");
        mast.Columns.Add("ClientRefNo");
        mast.Columns.Add("Vessel");
        mast.Columns.Add("Voyage");


        mast.Columns.Add("TotalInQty");
        mast.Columns.Add("TotalInPack");
        mast.Columns.Add("TotalInWeight");
        mast.Columns.Add("TotalInVolume");
        mast.Columns.Add("TotalOutQty");
        mast.Columns.Add("TotalOutPack");
        mast.Columns.Add("TotalOutWeight");
        mast.Columns.Add("TotalOutVolume");
        mast.Columns.Add("TotalHand");
        mast.Columns.Add("TotalPack");
        mast.Columns.Add("TotalWeight");
        mast.Columns.Add("TotalVolume");

        det.Columns.Add("JobDate");
        det.Columns.Add("JobNo");
        det.Columns.Add("ContNo");
        det.Columns.Add("BookingNo");
        det.Columns.Add("HblNo");
        det.Columns.Add("SkuCode");
        det.Columns.Add("Qty");
        det.Columns.Add("UomCode");
        det.Columns.Add("ConsigneeAddress");
        det.Columns.Add("InQty");
        det.Columns.Add("InPack");
        det.Columns.Add("InWeight");
        det.Columns.Add("InVolume");
        det.Columns.Add("OutQty");
        det.Columns.Add("OutPack");
        det.Columns.Add("OutWeight");
        det.Columns.Add("OutVolume");
        det.Columns.Add("HandQty");
        det.Columns.Add("HandPack");
        det.Columns.Add("HandWeight");
        det.Columns.Add("HandVolume");

        #endregion
        string where = "";
        string sql = string.Format(@"select * from(select distinct ROW_NUMBER()over (order by mast.Id) as No,job.JobNo,job.JobDate,mast.CargoType,mast.LineId,
(select top 1 WareHouseCode from CTM_Job j where mast.JobNo=j.JobNo) as WareHouseCode,job.ClientId,job.ClientRefNo,mast.ConsigneeAddress,job.HaulierId,
job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.QtyOrig,mast.ActualItem,
mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,mast.RefNo,
isnull(PackQty,0) as SkuQty,PackUom from job_house mast left join ctm_job job on mast.JobNo=job.JobNo and CargoType in ('IN','OUT')
)as tab ");
        if (sku.Length > 0) {
            where = GetWhere(where,string.Format(@" SkuCode='{0}'",sku));
        }
        if (orderNo.Length > 0)
        {
            where = GetWhere(where, string.Format(@" RefNo='{0}'", orderNo));
        }
        if (where.Length > 0)
        {
            sql += "where" + where + " order by LineId";
        }
        DataRow row = mast.NewRow();
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            string type = SafeValue.SafeString(dt.Rows[0]["CargoType"]);

            if(type=="IN")
                row["RefNo"] = dt.Rows[0]["JobNo"];
            row["ClientId"] = dt.Rows[0]["ClientId"];
            row["ClientName"] = EzshipHelper.GetPartyName(dt.Rows[0]["ClientId"]);
            row["HaulierId"] = dt.Rows[0]["HaulierId"];
            row["HaulierName"] = EzshipHelper.GetPartyName(dt.Rows[0]["HaulierId"]);
            row["RunDate"] = DateTime.Now;
            row["ClientRefNo"] = dt.Rows[0]["ClientRefNo"];
            row["Vessel"] = dt.Rows[0]["Vessel"];
            row["Voyage"] = dt.Rows[0]["Voyage"];
            row["Prefix"] = "[ELL]";

            decimal inQty = 0;
            decimal inPack = 0;
            decimal inWeight = 0;
            decimal inVolume = 0;

            decimal handQty = 0;
            decimal handPack = 0;
            decimal handWeight = 0;
            decimal handVolume = 0;

            decimal outQty = 0;
            decimal outPack = 0;
            decimal outWeight = 0;
            decimal outVolume = 0;

            decimal totalInQty = 0;
            decimal totalInPack = 0;
            decimal totalInWeight = 0;
            decimal totalInVolume = 0;

            decimal totalOutQty = 0;
            decimal totalOutPack = 0;
            decimal totalOutWeight = 0;
            decimal totalOutVolume = 0;

            decimal totalHand = 0;
            decimal totalPack = 0;
            decimal totalWeight = 0;
            decimal totalVolume = 0;

            string lastRefNo = "";
            string lastContNo = "";
            string lastBookingNo = "";
            string lastSkuCode = "";
            string lastHblNo = "";
            string lastItemCode = "";
            int lastLineId = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dt_row = dt.Rows[i];
                DataRow row1 = det.NewRow();
                type = SafeValue.SafeString(dt_row["CargoType"]);
                string bookingNo = SafeValue.SafeString(dt_row["BookingNo"]);
                int lineId = SafeValue.SafeInt(dt_row["LineId"], 0);
                string refNo = SafeValue.SafeString(dt_row["RefNo"]);
                string hblNo = SafeValue.SafeString(dt_row["HblNo"]);
                string skuCode = SafeValue.SafeString(dt.Rows[i]["SkuCode"]);
                string itemCode = SafeValue.SafeString(dt.Rows[i]["ActualItem"]);
                row1["SkuCode"] = dt_row["SkuCode"];
                row1["UomCode"] = dt_row["PackTypeOrig"];
                row1["JobDate"] =SafeValue.SafeDateStr(dt_row["JobDate"]);
                row1["JobNo"] = dt_row["JobNo"];
                row1["ConsigneeAddress"] = dt_row["ConsigneeAddress"];
                if (type == "IN")
                {
                    inQty = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt_row["QtyOrig"]),1);
                    inPack =SafeValue.ChinaRound(SafeValue.SafeDecimal(dt_row["PackQty"]),1);
                    inWeight = SafeValue.SafeDecimal(dt_row["WeightOrig"]);
                    inVolume = SafeValue.SafeDecimal(dt_row["VolumeOrig"]);
                    row1["InQty"] =inQty;
                    row1["InPack"] = inPack;
                    row1["InWeight"] = inWeight;
                    row1["InVolume"] = inVolume;

                    totalInQty += SafeValue.ChinaRound(inQty, 1);
                    totalInPack += SafeValue.ChinaRound(inPack, 1);
                    totalInWeight += SafeValue.SafeDecimal(inWeight);
                    totalInVolume += SafeValue.SafeDecimal(inVolume);
                }
                if (type == "OUT")
                {
                    outQty = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt_row["QtyOrig"]),1);
                    outPack = SafeValue.ChinaRound(SafeValue.SafeDecimal(dt_row["PackQty"]),1);
                    outWeight = SafeValue.SafeDecimal(dt_row["WeightOrig"]);
                    outVolume = SafeValue.SafeDecimal(dt_row["VolumeOrig"]);
                    row1["OutQty"] = outQty;
                    row1["OutPack"] = outPack;
                    row1["OutWeight"] = outWeight;
                    row1["OutVolume"] = outVolume;

                    totalOutQty += SafeValue.ChinaRound(outQty, 1);
                    totalOutPack += SafeValue.ChinaRound(outPack, 1);
                    totalOutWeight += SafeValue.SafeDecimal(outWeight);
                    totalOutVolume += SafeValue.SafeDecimal(outVolume);
                }
                if (bookingNo == lastBookingNo && (skuCode == lastSkuCode || itemCode == lastItemCode) && lineId == lastLineId)
                {
                    if (i == 0)
                    {
                        handQty =SafeValue.ChinaRound(inQty - outQty,1);
                        handPack = SafeValue.ChinaRound(inPack - outPack,1);
                        handWeight = inWeight - outWeight;
                        handVolume = inVolume - outVolume;
                    }
                    else
                    {
                        handQty = SafeValue.ChinaRound(handQty - outQty,1);
                        handPack = SafeValue.ChinaRound(inPack - outPack, 1);
                        handWeight = handWeight - outWeight;
                        handVolume = handVolume - outVolume;
                    }

                    totalHand += totalInQty - totalOutQty;
                    totalPack += totalInPack - totalOutPack;
                    totalWeight += totalInWeight - totalOutWeight;
                    totalVolume += totalInVolume - totalOutVolume;

                }
                else {
                    handQty = SafeValue.ChinaRound(inQty,1);
                    handPack = SafeValue.ChinaRound(inPack, 1);
                    handVolume = inVolume;
                    handWeight = inWeight;
                }
                lastLineId = lineId;

                lastBookingNo = bookingNo;
                lastHblNo = hblNo;
                lastSkuCode = skuCode;
                lastItemCode = itemCode;

                row1["HandQty"] = handQty;
                row1["HandPack"] = handPack;
                row1["HandWeight"] = handWeight;
                row1["HandVolume"] = handVolume;



                det.Rows.Add(row1);
            }

            row["TotalInQty"]=SafeValue.ChinaRound(totalInQty,1);
            row["TotalInPack"] = SafeValue.ChinaRound(totalInPack, 1);
            row["TotalInWeight"] = SafeValue.ChinaRound(totalInWeight, 3);
            row["TotalInVolume"] = SafeValue.ChinaRound(totalInVolume, 3);
            row["TotalOutQty"] = SafeValue.ChinaRound(totalOutQty, 1);
            row["TotalOutPack"] = SafeValue.ChinaRound(totalOutQty, 1);
            row["TotalOutWeight"] = SafeValue.ChinaRound(totalOutWeight,3);
            row["TotalOutVolume"] = SafeValue.ChinaRound(totalOutVolume, 3);
            row["TotalHand"] = SafeValue.ChinaRound(totalHand, 1);
            row["TotalPack"] = SafeValue.ChinaRound(totalPack, 1);
            row["TotalWeight"] = SafeValue.ChinaRound(totalWeight, 3);
            row["TotalVolume"] = SafeValue.ChinaRound(totalVolume, 3);
        }
        mast.Rows.Add(row);

        ds.Tables.Add(mast);
        ds.Tables.Add(det);

        return ds;
    }
    public static DataSet PrintDeliveryTrip(string orderNo, string id, string jobType)
    {
        DataSet set = new DataSet();
        ObjectQuery query = new ObjectQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return set;
        C2.CtmJob obj = objSet[0] as C2.CtmJob;

        ObjectQuery query1 = new ObjectQuery(typeof(C2.CtmJobDet2), "Id=" + id + "", "");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);
        if (objSet1.Count == 0 || objSet1[0] == null) return set;
        C2.CtmJobDet2 det2 = objSet1[0] as C2.CtmJobDet2;


        ObjectQuery query2 = new ObjectQuery(typeof(C2.JobHouse), "TripId='" + id + "'", "");
        ObjectSet objSet2 = C2.Manager.ORManager.GetObjectSet(query2);


        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");

        mast.Columns.Add("DoNo");
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
        mast.Columns.Add("SkuQty");
        mast.Columns.Add("DeliveryRemark");
        mast.Columns.Add("BillingRemark");
        mast.Columns.Add("Remark1");
        mast.Columns.Add("PodType");
        mast.Columns.Add("StartTime");
        mast.Columns.Add("EndTime");
        mast.Columns.Add("ContNo");
        mast.Columns.Add("TrailerRemark");
        mast.Columns.Add("DriverCode");
        mast.Columns.Add("TripN");
        mast.Columns.Add("OT");
        mast.Columns.Add("EpodSignedBy");

        mast.Columns.Add("TowheadCode");
        mast.Columns.Add("RequestVehicleType");
        mast.Columns.Add("RequestTrailerType");
        mast.Columns.Add("FromDate");
        mast.Columns.Add("FromTime");
        mast.Columns.Add("ToDate");
        mast.Columns.Add("ToTime");
        mast.Columns.Add("ToCode");
        mast.Columns.Add("WarehouseRemark");

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
        det.Columns.Add("ActualItem");

        #endregion

        DataRow row = mast.NewRow();
        string sql_haulier = "select Name,CrNo,Contact1,Contact2,Tel1,Fax1 from xxparty where partyid='" + obj.ClientId + "'";
        DataTable tab_hauler = Helper.Sql.List(sql_haulier);
        if (jobType == "gr")
        {
            row["JobType"] = "Goods Receipt Note".ToUpper();
        }
        if (jobType == "do" || jobType == "CRA")
        {
            row["JobType"] = "Delivery Order".ToUpper();
        }
        if (jobType == "tp")
        {
            row["JobType"] = "Transport Order".ToUpper();
        }
        if (jobType == "tr")
        {
            row["JobType"] = "Transfer Instruction".ToUpper();
        }
        row["DoNo"] = det2.ManualDo;
        row["Attn"] = obj.Terminalcode;
        row["DeliveryTo"] = det2.ToCode;
        row["PickupFrom"] = det2.FromCode;
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

        row["JobNo"] = det2.TripIndex;
        row["JobDate"] = det2.BookingDate.ToString("dd/MM/yyyy");
        row["Vessel"] = obj.Vessel;
        row["Voyage"] = obj.Voyage;
        row["Permit"] = obj.PermitNo;
        row["RequestTrailerType"] = det2.RequestTrailerType;
        row["WarehouseRemark"] = det2.WarehouseRemark;
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
        row["TowheadCode"] = det2.TowheadCode;
        #region Crane Job

        sql = string.Format(@"select TowheadCode,RequestVehicleType,FromDate,ToDate,ToTime,ToCode,DeliveryRemark,BillingRemark,Remark from cmt_jobdet2 where ");
        #endregion
        decimal total = 0;
        decimal skuQty = 0;
        for (int i = 0; i < objSet2.Count; i++)
        {
            C2.JobHouse house = objSet2[i] as C2.JobHouse;
            DataRow row1 = det.NewRow();
            row1["No"] = i + 1;
            row1["JobNo"] = orderNo;
            row1["ContNo"] = house.ContNo;
            row1["CargoStatus"] = house.CargoStatus;
            row1["CargoType"] = house.CargoType;
            row1["Location"] = house.Location;
            row1["BookingNo"] = house.BookingNo;
            row1["HblNo"] = house.HblNo;
            row1["Length"] = house.LengthPack;
            row1["Width"] = house.WidthPack;
            row1["Height"] = house.HeightPack;
            row1["Weight"] = house.WeightOrig;
            row1["Volume"] = house.WeightOrig;
            row1["Qty"] = SafeValue.ChinaRound(house.QtyOrig,1);
            row1["PackType"] = house.PackTypeOrig;
            row1["PackQty"] = SafeValue.ChinaRound(house.PackQty,1);
            row1["PackUom"] = house.PackUom;
            row1["Marking1"] = house.Marking1;
            row1["Marking2"] = house.Marking2;
            row1["SkuCode"] = house.SkuCode;
            row1["ActualItem"] = house.BookingItem;
            total += house.QtyOrig;
            skuQty += house.PackQty;
            det.Rows.Add(row1);
        }
        row["TotalQty"] = SafeValue.ChinaRound(total, 1);
        row["SkuQty"] = SafeValue.ChinaRound(skuQty, 1);
        string sql_trip = string.Format(@"select Id,DeliveryRemark,BillingRemark,Remark1,PodType,DriverCode,OtHour,epodTrip,epodSignedBy,ContainerNo from ctm_jobdet2 where Id={0}", id);
        DataTable dt_trip = ConnectSql_mb.GetDataTable(sql_trip);
        if (dt_trip.Rows.Count > 0)
        {
            DataRow row_trip = dt_trip.Rows[0];
            row["DeliveryRemark"] = row_trip["DeliveryRemark"].ToString();
            row["BillingRemark"] = row_trip["BillingRemark"].ToString();
            row["Remark1"] = row_trip["Remark1"].ToString();//Driver Remark
            row["PodType"] = row_trip["PodType"].ToString();

            row["DriverCode"] = row_trip["DriverCode"].ToString();
            row["TripN"] = row_trip["epodTrip"].ToString();
            row["OT"] =SafeValue.ChinaRound(SafeValue.SafeDecimal(row_trip["OtHour"]),1);
            row["EpodSignedBy"] = row_trip["epodSignedBy"].ToString();
        }

        row["ContNo"] = det2.ChessisCode;
        row["TrailerRemark"] = det2.Remark;
        row["StartTime"] =SafeValue.SafeDateStr(det2.FromDate) + "" + det2.FromTime;
        row["EndTime"] = SafeValue.SafeDateStr(det2.ToDate) + "" + det2.ToTime;
         
        mast.Rows.Add(row);

        set.Tables.Add(mast);
        set.Tables.Add(det);

        return set;
    }
    public static DataSet PrintInventoryBalance(string client, DateTime from)
    {
        DataSet set = new DataSet();
        #region init column
        DataTable mast = new DataTable("Mast");
        DataTable det = new DataTable("Det");
        mast.Columns.Add("Date");
        mast.Columns.Add("ReportId");
        mast.Columns.Add("WarehouseId");

        det.Columns.Add("Client");
        det.Columns.Add("LotNo");
        det.Columns.Add("SkuCode");
        det.Columns.Add("PermitNo");
        det.Columns.Add("Marking1");
        det.Columns.Add("Marking2");
        det.Columns.Add("Location");
        det.Columns.Add("SkuQty");
        det.Columns.Add("BalQty");
        det.Columns.Add("Remark");
        det.Columns.Add("OpsType");
        det.Columns.Add("Attribute");
        #endregion
        DataRow row = mast.NewRow();

        #region Data
        row["Date"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        row["ReportId"] = "WMS ";
        
        mast.Rows.Add(row);
        if (client == "NA")
            client = "";
        DataTable dt = C2.JobHouse.getStockBalance("", client, "", "", "", "", "", "", "", "Bonded", "");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow rowDet = dt.Rows[i];
            DataRow row1 = det.NewRow();
            row1["Location"] = rowDet["Location"];
            string skuCode=rowDet["SkuCode"].ToString();
            string hblNo = rowDet["HblNo"].ToString();
            string sql = string.Format(@"select PermitNo from ref_permit where HblNo='{0}'", hblNo);
            string permitNo = ConnectSql_mb.ExecuteScalar(sql);
            if (skuCode.Length > 0)
                skuCode = rowDet["ActualItem"].ToString();
            row1["SkuCode"]=skuCode;
            row1["OpsType"] = rowDet["OpsType"].ToString();
            row1["PermitNo"] = permitNo;
            row1["Marking1"] = rowDet["Marking1"].ToString();
            row1["Marking2"] = rowDet["Marking2"].ToString();
            row1["LotNo"] = rowDet["BookingNo"].ToString();
            row1["BalQty"] = rowDet["BalQty"].ToString();
            row1["SkuQty"] = rowDet["SkuQty"].ToString();
            row1["Client"] = rowDet["PartyName"].ToString();
            row1["Attribute"] = "Length:" + rowDet["LengthPack"]+" Breadth:"+rowDet["WidthPack"]+" Height:"+rowDet["HeightPack"];
            det.Rows.Add(row1);
          
        }

        #endregion

        set.Tables.Add(mast);
        set.Tables.Add(det);
        return set;
    }
    private static string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}