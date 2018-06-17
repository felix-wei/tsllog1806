using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using C2;
using Wilson.ORMapper;
/// <summary>
/// WareHouseReport 的摘要说明
/// </summary>
public class WareHouseReport
{
    private static DataTable InitMastDataTable()
    {
        DataTable tab = new DataTable("Mast");

        //Do
        tab.Columns.Add("DoNo");
        tab.Columns.Add("InvoiceNo");
        tab.Columns.Add("GINNo");
        tab.Columns.Add("DoDate");
        tab.Columns.Add("Vessel");
        tab.Columns.Add("Voyage");
        tab.Columns.Add("CompanyName");
        tab.Columns.Add("CompanyAddress1");
        tab.Columns.Add("DeliveryTo");
        tab.Columns.Add("CollectFrom");
        tab.Columns.Add("Carrier");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Etd");
        tab.Columns.Add("EtaDest");
        tab.Columns.Add("UserId");
        tab.Columns.Add("PartyId");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("PartyAdd");
        tab.Columns.Add("Haulier");
        tab.Columns.Add("SealNo");
        tab.Columns.Add("PoNo");
        tab.Columns.Add("Pol");
        tab.Columns.Add("Pod");
        tab.Columns.Add("Name");
        tab.Columns.Add("Obl");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("CreatedBy");
        tab.Columns.Add("ContainerNo");
        tab.Columns.Add("CustRef");
        tab.Columns.Add("MelsOrderNo");
        tab.Columns.Add("Payment");
        tab.Columns.Add("PartyTel");
        tab.Columns.Add("PartyFax");
        tab.Columns.Add("PartyContact");
        tab.Columns.Add("ShippingMark");
        tab.Columns.Add("PartyInvNo");
        tab.Columns.Add("PermitNo");
        tab.Columns.Add("Tel");
        tab.Columns.Add("Fax");
        tab.Columns.Add("Attn");
        tab.Columns.Add("Coloader");
        tab.Columns.Add("Volume");
        tab.Columns.Add("GrossWeight");
        tab.Columns.Add("NettWeight");
        tab.Columns.Add("Remark");
        tab.Columns.Add("TotalQty");
        tab.Columns.Add("TotalPkg");
        tab.Columns.Add("TotalPcs");
        tab.Columns.Add("TotalWeight");
        tab.Columns.Add("TotalVolume");
        tab.Columns.Add("Cnt1");
        tab.Columns.Add("Cnt2");
        tab.Columns.Add("Cnt3");

        return tab;
    }
    private static DataTable InitDetailDataTable()
    {
        DataTable tab = new DataTable("Det");
        tab.Columns.Add("DoNo");
        tab.Columns.Add("No", typeof(Int32));
        tab.Columns.Add("Sku");
        tab.Columns.Add("LotNo");
        tab.Columns.Add("PkgType");
        tab.Columns.Add("Des");
        tab.Columns.Add("Uom");
        tab.Columns.Add("Plt");
        tab.Columns.Add("Pkg");
        tab.Columns.Add("Pcs");
        tab.Columns.Add("Weight");
        tab.Columns.Add("Height");
        tab.Columns.Add("Volume");
        tab.Columns.Add("WarehouseId");
        tab.Columns.Add("Location");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Qty1");
        tab.Columns.Add("Qty2");
        tab.Columns.Add("Qty3");
        tab.Columns.Add("Remark");
        tab.Columns.Add("PalletNo");
        tab.Columns.Add("Att1");
        tab.Columns.Add("Att2");
        tab.Columns.Add("Att3");
        tab.Columns.Add("Att4");
        tab.Columns.Add("Att5");
        tab.Columns.Add("Att6");
        tab.Columns.Add("ContainerNo");
        return tab;
    }

    public static DataSet DsImpTs(string orderNo, string userName)
    {
        ObjectQuery query = new ObjectQuery(typeof(C2.WhDo), "DoNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
        C2.WhDo obj = objSet[0] as C2.WhDo;
        string sql="";
        #region tally sheet
        DataTable mast = InitMastDataTable();
        DataRow row = mast.NewRow();
        row["DoNo"] = obj.DoNo;
        sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}'", obj.DoNo);
        row["InvoiceNo"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        row["DoDate"] = obj.DoDate.ToString("dd/MM/yyyy");
        row["GINNo"]=obj.CustomerReference;
        row["Vessel"] = obj.Vessel;
        row["Voyage"] = obj.Voyage;
        row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
        row["Name"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
        row["CompanyAddress1"] = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"];
        row["DeliveryTo"] = obj.DeliveryTo;
        row["CollectFrom"] = obj.CollectFrom;
        row["Carrier"] = obj.Carrier;
        row["Eta"] = obj.Eta.ToString("dd/MM/yyyy");
        row["Etd"] = obj.Eta.ToString("dd/MM/yyyy");
        row["EtaDest"] = obj.EtaDest.ToString("dd/MM/yyyy");
        row["UserId"] = userName;
        row["PartyId"] = obj.PartyId;
        row["PartyName"] = obj.PartyName;
        row["PartyAdd"] = obj.PartyAdd;
        row["Haulier"] = obj.DriverName;
        row["SealNo"] = obj.CustomsSealNo;
        row["PoNo"] = obj.PoNo;
        sql = "select name from XXPort where Code='" + SafeValue.SafeString(obj.Pol, "") + "'";
        row["Pol"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        sql = "select name from XXPort where Code='" + SafeValue.SafeString(obj.Pod, "") + "'";
        row["Pod"] = SafeValue.SafeString(Helper.Sql.One(sql), "");
        
        row["Obl"] = obj.Obl;
        row["Hbl"] = obj.Hbl;
        row["CreatedBy"] = obj.CreateBy;
        row["CustRef"] = obj.CustomerReference;
        row["ContainerNo"] = obj.ContainerNo;
        row["SealNo"] = obj.CustomsSealNo;
        row["PartyInvNo"] = obj.PartyInvNo;
        row["PermitNo"] = obj.PermitNo;
        row["Tel"] = System.Configuration.ConfigurationManager.AppSettings["CompanyTel"];
        row["Fax"] = System.Configuration.ConfigurationManager.AppSettings["CompanyFax"];
        row["Attn"] = System.Configuration.ConfigurationManager.AppSettings["CompanyAttn"];
        row["Coloader"] = "";
        row["Volume"] = "";
        row["GrossWeight"] = "";
        row["NettWeight"] = "";
        row["Remark"] = obj.Remark1;
        row["Payment"] = "";
        sql = string.Format(@"select Tel1,Fax1,Contact1 from XXParty where PartyId='{0}'", obj.PartyId);
        DataTable tab = ConnectSql.GetTab(sql);
        row["PartyTel"] = SafeValue.SafeString(tab.Rows[0]["Tel1"]);
        row["PartyFax"] = SafeValue.SafeString(tab.Rows[0]["Fax1"]);
        row["PartyContact"] = SafeValue.SafeString(tab.Rows[0]["Contact1"]);
        row["Payment"] = "";
        row["Payment"] = "";
        row["SealNo"] = obj.CustomsSealNo;
        
        sql="select sum(Qty1) from wh_dodet2 where DoNo='"+orderNo+"' group by DoNo";
        row["TotalQty"] = Helper.Sql.One(sql);
        sql = "select sum(QtyPackWhole) from wh_dodet2 where DoNo='" + orderNo + "' group by DoNo";
        row["TotalPkg"] = Helper.Sql.One(sql);
        sql = "select sum(QtyWholeLoose) from wh_dodet2 where DoNo='" + orderNo + "' group by DoNo";
        row["TotalPcs"] = Helper.Sql.One(sql);
        sql = "select sum(NettWeight) from wh_dodet2 where DoNo='" + orderNo + "' group by DoNo";
        row["TotalWeight"] = Helper.Sql.One(sql);

        DataTable details = InitDetailDataTable();
        ObjectQuery query1 = new ObjectQuery(typeof(C2.WhDoDet2), "DoNo='" + orderNo + "'", "");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);
        decimal totalVolume = 0;
        string ginNo = "";
        for (int i = 0; i < objSet1.Count; i++)
        {
            DataRow row1 = details.NewRow();
            C2.WhDoDet2 det = objSet1[i] as C2.WhDoDet2;

            row1["DoNo"] = obj.DoNo;
            row1["No"] = i + 1;
            row1["ContainerNo"] = det.ContainerNo;
            row1["Sku"] = det.Product;
            row1["Qty"] = det.Qty1;
            row1["Qty1"] = det.Qty1;
            row1["Qty2"] = det.Qty2;
            row1["Qty3"] = det.Qty3;
            row1["Plt"] = det.QtyPackWhole;
            row1["Pkg"] = det.QtyWholeLoose;
            row1["Pcs"] = det.Qty1;
            row1["Des"] = det.Des1;
            row1["LotNo"] = det.LotNo;
            row1["WarehouseId"] = obj.WareHouseId;
            row1["Location"] = det.Location;
            row1["Uom"] = det.Uom1;
            row1["Weight"] = det.NettWeight;
            details.Rows.Add(row1);
            if(det.DoType=="OUT"){
                sql = string.Format(@"select det.DoNo from wh_dodet2 det inner join wh_dodet2 out on det.Id=out.RelaId where out.Id={0}",det.Id);
                ginNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
            }

            sql = "select sum(LengthPacking*WidthPacking) from ref_product where Code='" + det.Product + "'";
            decimal volume = Helper.Safe.SafeDecimal(Helper.Sql.One(sql));
            totalVolume += volume;
        }

        ObjectQuery query2 = new ObjectQuery(typeof(C2.WhDoDet3), "DoNo='" + orderNo + "'", "");
        ObjectSet objSet2 = C2.Manager.ORManager.GetObjectSet(query2);
        int cnt1=0;
        int cnt2 = 0;
        int cnt3 = 0;
        for (int i = 0; i < objSet2.Count;i++ )
        {
            C2.WhDoDet3 det = objSet2[i] as C2.WhDoDet3;
            if(det.ContainerType=="20FT"){
                cnt1++;
            }
            if (det.ContainerType == "20FT")
            {
                cnt2++;
            }
            if (det.ContainerType == "20FT")
            {
                cnt3++;
            }
        }
        row["TotalVolume"] = Helper.Safe.AccountNz(totalVolume);
        row["GINNo"] = ginNo;
        row["Cnt1"] = cnt1;
        row["Cnt2"] = cnt2;
        row["Cnt3"] = cnt3;
        mast.Rows.Add(row);

        #endregion
        DataSet set = new DataSet();
        set.Tables.Add(mast);
        set.Tables.Add(details);
        set.Relations.Add("", mast.Columns["DoNo"], details.Columns["DoNo"]);

        return set;
    }

    private static DataTable InitTransferMastDataTable() {
        DataTable tab = new DataTable("Mast");
        tab.Columns.Add("TransferNo");
        tab.Columns.Add("PartyId");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("RequestPerson");
        tab.Columns.Add("Pic");
        tab.Columns.Add("TransferDate");
        tab.Columns.Add("CreateBy");

        return tab;
    }
    private static DataTable InitTransferDetailDataTable()
    {
        DataTable tab = new DataTable("Det");
        tab.Columns.Add("TransferNo");
        tab.Columns.Add("No", typeof(Int32));
        tab.Columns.Add("Sku");
        tab.Columns.Add("Description");
        tab.Columns.Add("LotNo");
        tab.Columns.Add("FromWh");
        tab.Columns.Add("FromLoc");
        tab.Columns.Add("BrandName");
        tab.Columns.Add("ToWh");
        tab.Columns.Add("ToLoc");
        tab.Columns.Add("Qty1");
        tab.Columns.Add("Qty2");
        tab.Columns.Add("Qty3");
        tab.Columns.Add("Weight");
        tab.Columns.Add("Space");
        tab.Columns.Add("Volume");
        return tab;
    }
    public static DataSet DsTransferTs(string orderNo)
    {
        ObjectQuery query = new ObjectQuery(typeof(C2.WhTransfer), "TransferNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
        C2.WhTransfer obj = objSet[0] as C2.WhTransfer;
        #region Mast
        DataTable mast = InitTransferMastDataTable();
        DataRow row = mast.NewRow();
        row["TransferNo"] = obj.TransferNo;
        row["PartyId"] = obj.PartyId;
        row["PartyId"] = obj.PartyName;
        row["RequestPerson"] = obj.RequestPerson;
        row["Pic"] = obj.Pic;
        row["TransferDate"] = obj.TransferDate.ToString("dd/MM/yyyy");
        row["CreateBy"] =obj.CreateBy;
        mast.Rows.Add(row);

        DataTable details = InitTransferDetailDataTable();
        ObjectQuery query1 = new ObjectQuery(typeof(C2.WhTransferDet), "TransferNo='" + orderNo + "'", "");
        ObjectSet objSet1 = C2.Manager.ORManager.GetObjectSet(query1);
        if (objSet1.Count > 0)
        {


            for (int i = 0; i < objSet1.Count; i++)
            {
                DataRow row1 = details.NewRow();
                C2.WhTransferDet det = objSet1[i] as C2.WhTransferDet;

                row1["TransferNo"] = obj.TransferNo;
                row1["No"] = i + 1;
                row1["Sku"] = det.Product;
                row1["LotNo"] = det.LotNo;
                row1["Qty1"] = det.Qty1;
                row1["Qty2"] = det.Qty2;
                row1["Qty3"] = det.Qty3;
                row1["FromWh"] = det.FromWarehouseId;
                row1["FromLoc"] = det.FromLocId;
                row1["ToWh"] = det.ToWarehouseId;
                row1["ToLoc"] = det.ToLocId;
                string sql = "select BrandName,VolumePacking from ref_product where Code='" + det.Product + "'";
                DataTable tab = ConnectSql.GetTab(sql);
                if(tab.Rows.Count>0){
                    row1["BrandName"] = tab.Rows[0]["BrandName"];
                    row1["Volume"] = tab.Rows[0]["VolumePacking"];
                }

                row1["Space"] = "";
                row1["Weight"] = det.Weight;
                details.Rows.Add(row1);
            }
        }
        else {
            DataRow row1 = details.NewRow();

            row1["TransferNo"] = obj.TransferNo;
            row1["No"] = 0;
            row1["Sku"] = "";
            row1["LotNo"] = "";
            row1["Qty1"] = "";
            row1["Qty2"] = "";
            row1["Qty3"] = "";
            row1["FromWh"] = "";
            row1["FromLoc"] = "";
            row1["ToWh"] = "";
            row1["ToLoc"] = "";
            row1["BrandName"] ="";
            row1["Volume"] = "";
            row1["Space"] = "";
            row1["Weight"] ="";
            details.Rows.Add(row1);
        }
        

        #endregion
        DataSet set = new DataSet();
        set.Tables.Add(mast);
        set.Tables.Add(details);

        return set;
    }

}