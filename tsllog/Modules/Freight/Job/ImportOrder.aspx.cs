using Aspose.Cells;
using DevExpress.Web;
using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxUploadControl;


public partial class Modules_Freight_Job_ImportOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void ImportJob(string batch, string file, out string error_text)
    {
        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        if (file.ToLower().EndsWith(".csv"))
        {
            wb.Open(file, FileFormatType.CSV);
        }
        else
        {
            wb.Open(file);
        }
        int empty_i = 0;
        string re_text = "";
        Worksheet ws = wb.Worksheets[0];

        int existDo = 0;
        int successJob = 0;
        int successItem = 0;
        int errorDo = 0;


        //=================================== version 1
        bool beginImport = false;
        //int existDo = 0;
        //int successJob = 0;
        int SortIndex = 0;
        bool action = false;

        string bkgRefNo = ws.Cells["C" + 2].StringValue;
        string jobOrder = ws.Cells["F" + 3].StringValue;
        string conNo = ws.Cells["J" + 2].StringValue;
        string shipper = ws.Cells["C" + 3].StringValue;
        string consignee = ws.Cells["C" + 4].StringValue;
        string responsible = ws.Cells["L" +4].StringValue;
        string ic = ws.Cells["D" + 5].StringValue;
        string uen = ws.Cells["I" + 5].StringValue;
        string email1 = ws.Cells["D" + 6].StringValue;
        string email2 = ws.Cells["H" + 6].StringValue;
        string tel1 = ws.Cells["D" + 7].StringValue;
        string tel2 = ws.Cells["F" + 7].StringValue;
        string mobile1 = ws.Cells["H" + 7].StringValue;
        string mobile2 = ws.Cells["L" + 7].StringValue;
        string address = ws.Cells["C" + 8].StringValue;
        string receiver = ws.Cells["C" + 10].StringValue;
        string contact = ws.Cells["C" + 11].StringValue;
        string delivery = ws.Cells["C" + 12].StringValue;
        string payable = ws.Cells["C" + 13].StringValue;
        string prepaidInd = ws.Cells["D" + 13].StringValue;
        decimal collectAmount1 =SafeValue.SafeDecimal(ws.Cells["H" + 13].StringValue);
        decimal collectAmount2 = SafeValue.SafeDecimal(ws.Cells["M" + 13].StringValue);
        string dutyPayment = ws.Cells["C" + 14].StringValue;
        string incoterm = ws.Cells["C" + 15].StringValue;
        decimal miscFee = SafeValue.SafeDecimal(ws.Cells["E" + 15].StringValue);
        string remark = ws.Cells["C" + 16].StringValue;
        string fee1CurrId = ws.Cells["M" + 19].StringValue;
        string totalQty = ws.Cells["I" + 19].StringValue;
        string packType = ws.Cells["J" + 19].StringValue;
        string weight = ws.Cells["K" + 19].StringValue;
        string m3 = ws.Cells["L" + 19].StringValue;



        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        #region list add
        cpar = new ConnectSql_mb.cmdParameters("@BookingNo", bkgRefNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@JobNo", 0, SqlDbType.Int);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContNo", conNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ShipperInfo", shipper, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeInfo", consignee, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        if (responsible == "个人")
        {
            cpar = new ConnectSql_mb.cmdParameters("@Responsible", "PERSON", SqlDbType.NVarChar, 100);
            list.Add(cpar);
        }
        else {
            cpar = new ConnectSql_mb.cmdParameters("@Responsible", "COMPANY", SqlDbType.NVarChar, 100);
            list.Add(cpar);
        }
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeRemark", ic, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeEmail", uen, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Mobile1", mobile1, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Mobile2", mobile2, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Desc1", address, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ClientId", receiver, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ClientEmail", contact, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Remark1", delivery, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        string prepaid = "NO";
        if (prepaidInd.Equals("√"))
        {
            prepaid = "YES";
        }
        cpar = new ConnectSql_mb.cmdParameters("@PrepaidInd", prepaid, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CollectAmount1", collectAmount1, SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CollectAmount2", collectAmount2, SqlDbType.Decimal, 100);
        list.Add(cpar);
        string payment="";
        if(dutyPayment.Equals("税中国付/DUTY PAID")){
             payment="DUTY PAID";
        }
        else{
          payment="DUTY UNPAID";
        }
        cpar = new ConnectSql_mb.cmdParameters("@DutyPayment", payment, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Incoterm", incoterm, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Ocean_Freight", miscFee, SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Remark2", remark, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Currency", fee1CurrId, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Qty", SafeValue.SafeInt(totalQty,0), SqlDbType.Int, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@PackTypeOrig", packType, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Weight", SafeValue.SafeDecimal(weight, 0), SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Volume", SafeValue.SafeDecimal(m3, 0), SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@JobType", "E", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        #endregion

        string sql = string.Format(@"insert into job_house (BookingNo,JobNo,JobType,ContNo,CargoStatus,ShipperInfo,ConsigneeInfo,Responsible,ConsigneeRemark, 
ConsigneeEmail,Email1,Email2,Tel1,Tel2,Mobile1,Mobile2,Desc1,ClientId,ClientEmail,Remark1,Prepaid_Ind,Collect_Amount1,Collect_Amount2,Duty_Payment,Incoterm,Ocean_Freight,Remark2,Currency,Qty,PackTypeOrig,Weight,Volume) 
values (@BookingNo,@JobNo,@JobType,@ContNo,'ORDER',@ShipperInfo,@ConsigneeInfo,@Responsible,@ConsigneeRemark,@ConsigneeEmail,@Email1,@Email2,@Tel1,@Tel2,@Mobile1,@Mobile2,@Desc1,@ClientId,@ClientEmail,@Remark1,@PrepaidInd,@CollectAmount1,@CollectAmount2,@DutyPayment,@Incoterm,@Ocean_Freight,@Remark2,@Currency,@Qty,@PackTypeOrig,@Weight,@Volume)");
        ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteScalar(sql, list);
        string sql_id = string.Format("select top 1 Id from job_house order by Id desc");
        string Oid = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_id));
        insert_cargo(SafeValue.SafeInt(Oid,0));
        if (Oid.Length > 0)
        {
            action = true;
            for (int i = 17; true; i++)
            {

                if (empty_i >= 10) { break; }
                DateTime date = DateTime.Today;


                string A = ws.Cells["A" + i].StringValue;
                string C = ws.Cells["C" + i].StringValue;
                if (A.Trim().ToUpper().Equals("序号"))
                {
                    beginImport = true;
                    empty_i++;
                    i = i + 1;
                    continue;
                }
                try
                {
                    SortIndex = SafeValue.SafeInt(ws.Cells["A" + i].StringValue, 0);
                }
                catch
                {
                    empty_i++;
                    continue;
                }
                if (C.Length <= 0)
                {
                    empty_i++;
                    continue;
                }
                //empty_i = 0;

                if (beginImport)
                {
                    #region

                    
                    string D = ws.Cells["D" + i].StringValue;
                    string E = ws.Cells["E" + i].StringValue;
                    string F = ws.Cells["F" + i].StringValue;
                    string G = ws.Cells["G" + i].StringValue;
                    string H = ws.Cells["H" + i].StringValue;
                    string I = ws.Cells["I" + i].StringValue;
                    #endregion
                    //=====================检测是否存在Bill Number
                    string billNo = D;
                    string billItem = E;
                    string sql_check = string.Format(@"select Marks1 from job_stock where OrderId={0} and SortIndex={1} and Marks1='{2}'", Oid, SortIndex, C);
                    DataTable dt_check = ConnectSql.GetTab(sql_check);
                    if (dt_check.Rows.Count > 0)
                    {
                        existDo++;
                        continue;
                    }
                    else
                    {
                        sql_check = string.Format(@"select count(*) from job_stock where OrderId={0}", Oid);
                        int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_check), 0);
                        SortIndex = n + 1;
                    }
                    try
                    {
                        list = new List<ConnectSql_mb.cmdParameters>();
                        #region list add
                        cpar = new ConnectSql_mb.cmdParameters("@SortIndex", SortIndex, SqlDbType.Int, 100);
                        list.Add(cpar);
                        cpar = new ConnectSql_mb.cmdParameters("@C", C, SqlDbType.NVarChar, 100);
                        list.Add(cpar);
                        cpar = new ConnectSql_mb.cmdParameters("@D", D, SqlDbType.NVarChar, 100);
                        list.Add(cpar);
                        cpar = new ConnectSql_mb.cmdParameters("@E", SafeValue.SafeInt(F, 0), SqlDbType.NVarChar, 100);
                        list.Add(cpar);
                        cpar = new ConnectSql_mb.cmdParameters("@F", SafeValue.SafeInt(F, 0), SqlDbType.Int);
                        list.Add(cpar);
                        cpar = new ConnectSql_mb.cmdParameters("@G", SafeValue.SafeDecimal(G), SqlDbType.NVarChar);
                        list.Add(cpar);
                        cpar = new ConnectSql_mb.cmdParameters("@H", SafeValue.SafeDecimal(H), SqlDbType.Float);
                        list.Add(cpar);
                        cpar = new ConnectSql_mb.cmdParameters("@OrderId", Oid, SqlDbType.Int, 100);
                        list.Add(cpar);
                        #endregion

                        sql = string.Format(@"insert into job_stock (SortIndex,Marks1,Marks2,Uom1,Qty2,Price1,Price2, 
OrderId) values (@SortIndex,@C,@D,@E,@F,@G,@H,@OrderId)");
                        re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                        if (re.status)
                        {
                            successJob++;
                            //action = true;
                        }
                        else
                        {
                            errorDo++;
                            //throw new Exception(re.context);
                        }

                    }
                    catch (Exception ex)
                    {
                        errorDo++;
                        //throw new Exception(ex.ToString());
                    }

                }
                else
                {
                }

            }
            if (action)
            {
                VilaParty(consignee, uen, ic, email1, email2, tel1, tel2, mobile1, mobile2, address, responsible);
            }
        }

        re_text = string.Format(@"添加了 {0} 条货物", successJob);
        re_text += successItem > 0 ? string.Format(@" {0} 条记录.", successItem) : ".";
        re_text += existDo > 0 ? string.Format(@" {0} 条已存在", existDo) : "";
        re_text += errorDo > 0 ? string.Format(@" {0} 条错误", errorDo) : "";
        error_text = re_text;





    }
    public void ImportExcel(string batch, string file, out string error_text)
    {
        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        if (file.ToLower().EndsWith(".csv"))
        {
            wb.Open(file, FileFormatType.CSV);
        }
        else
        {
            wb.Open(file);
        }
        int empty_i = 0;
        string re_text = "";
        Worksheet ws = wb.Worksheets[0];

        int existDo = 0;
        int successJob = 0;
        int successItem = 0;
        int errorDo = 0;


        //=================================== version 1
        bool beginImport = false;
        //int existDo = 0;
        //int successJob = 0;
        int SortIndex = 0;
        bool action = false;

        //Job Info
        string client = ws.Cells["B" + 2].StringValue;
        string vessel = ws.Cells["B" + 3].StringValue;
        string voyage = ws.Cells["D" + 3].StringValue;
        string pol = ws.Cells["B" + 4].StringValue;
        string pod = ws.Cells["D" + 4].StringValue;
        string warehouse = ws.Cells["H" + 2].StringValue;
        string isWarehouse = ws.Cells["J" + 2].StringValue;

        DateTime now = DateTime.Now;
        string jobNo = "";
        C2.CtmJob job = new C2.CtmJob();
        jobNo = C2Setup.GetNextNo("", "CTM_Job_WGR", DateTime.Now);
        job.JobNo = jobNo;
        job.JobType = "WGR";
        job.JobDate = DateTime.Now;
        job.ClientId = EzshipHelper.GetPartyId(client);
        job.DeliveryTo = "";
        job.StatusCode = "USE";
        job.QuoteNo = jobNo;
        job.QuoteStatus = "None";
        job.JobStatus = "Confirmed";
        job.Vessel = vessel;
        job.Voyage = voyage;
        job.Pol = pol;
        job.Pod = pod;
        job.WareHouseCode = warehouse;
        job.IsWarehouse = isWarehouse;
        C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(job);
        C2Setup.SetNextNo("", "CTM_Job_WGR", jobNo, now);

        auto_add_trailer_trip(jobNo, "WGR");

        //Order Info

        string bkgRefNo = ws.Cells["B" + 7].StringValue;
        string hblNo = ws.Cells["F" + 7].StringValue;
        string conNo = ws.Cells["I" + 7].StringValue;
        string shipper = ws.Cells["B" + 8].StringValue;
        string consignee = ws.Cells["B" + 9].StringValue;
        string ic_uen = ws.Cells["H" + 9].StringValue;
        string responsible = ws.Cells["K" + 9].StringValue;
        string email1 = ws.Cells["C" + 10].StringValue;
        string email2 = ws.Cells["H" + 10].StringValue;
        string tel = ws.Cells["C" + 11].StringValue;
        string postCode = ws.Cells["E" + 11].StringValue;
        string mobile = ws.Cells["H" + 11].StringValue;
        string address = ws.Cells["B" + 12].StringValue;
        string receiver = ws.Cells["B" + 14].StringValue;
        string contact = ws.Cells["B" + 15].StringValue;
        string delivery = ws.Cells["B" + 16].StringValue;
        
        string prepaidInd = ws.Cells["C" + 18].StringValue;
        decimal collectAmount1 = SafeValue.SafeDecimal(ws.Cells["H" + 18].StringValue);
        decimal collectAmount2 = SafeValue.SafeDecimal(ws.Cells["K" + 18].StringValue);
        string dutyPayment = ws.Cells["B" + 19].StringValue;
        string incoterm = ws.Cells["B" + 20].StringValue;
        decimal ocean_freight = SafeValue.SafeDecimal(ws.Cells["H" + 20].StringValue);
        string remark = ws.Cells["B" + 21].StringValue;

        string fee1CurrId = ws.Cells["H" + 24].StringValue;
        string totalQty = ws.Cells["B" + 24].StringValue;
        string packType = ws.Cells["D" + 2].StringValue;
        string weight = ws.Cells["F" + 24].StringValue;
        string m3 = ws.Cells["J" + 24].StringValue;



        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        ConnectSql_mb.cmdParameters cpar = null;
        #region list add
        cpar = new ConnectSql_mb.cmdParameters("@BookingNo", bkgRefNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@HblNo", hblNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@RefNo", jobNo, SqlDbType.NVarChar);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ContNo", conNo, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ShipperInfo",EzshipHelper.GetPartyId(shipper), SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeInfo", consignee, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        if (responsible == "个人")
        {
            cpar = new ConnectSql_mb.cmdParameters("@Responsible", "PERSON", SqlDbType.NVarChar, 100);
            list.Add(cpar);
        }
        else
        {
            cpar = new ConnectSql_mb.cmdParameters("@Responsible", "COMPANY", SqlDbType.NVarChar, 100);
            list.Add(cpar);
        }
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeRemark", ic_uen, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        if (email2.Length > 0) {
            email1 += "," + email2;
        }
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeEmail", email1, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeTel", tel, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeZip", postCode, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Mobile1", mobile, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ConsigneeAddress", address, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ClientId", receiver, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ClientEmail", contact, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@ClientAddress", delivery, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        string prepaid = "NO";
        if (prepaidInd.Equals("√"))
        {
            prepaid = "YES";
        }
        cpar = new ConnectSql_mb.cmdParameters("@PrepaidInd", prepaid, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CollectAmount1", collectAmount1, SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@CollectAmount2", collectAmount2, SqlDbType.Decimal, 100);
        list.Add(cpar);
        string payment = "";
        if (dutyPayment.Equals("税中国付/DUTY PAID"))
        {
            payment = "DUTY PAID";
        }
        else
        {
            payment = "DUTY UNPAID";
        }
        cpar = new ConnectSql_mb.cmdParameters("@DutyPayment", payment, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Incoterm", incoterm, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Ocean_Freight", ocean_freight, SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Remark1", remark, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Currency", fee1CurrId, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Qty", SafeValue.SafeDecimal(totalQty, 0), SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@PackTypeOrig", packType, SqlDbType.NVarChar, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Weight", SafeValue.SafeDecimal(weight, 0), SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@Volume", SafeValue.SafeDecimal(m3, 0), SqlDbType.Decimal, 100);
        list.Add(cpar);
        cpar = new ConnectSql_mb.cmdParameters("@JobType", "WGR", SqlDbType.NVarChar, 100);
        list.Add(cpar);
        #endregion

        string sql = string.Format(@"insert into job_house (BookingNo,HblNo,JobNo,RefNo,JobType,ContNo,CargoStatus,ShipperInfo,ConsigneeInfo,Responsible,ConsigneeRemark, 
ConsigneeEmail,ConsigneeTel,Mobile1,ConsigneeAddress,ClientId,ClientEmail,ClientAddress,Remark1,Prepaid_Ind,Collect_Amount1,Collect_Amount2,Duty_Payment,Incoterm,Ocean_Freight,Currency,Qty,PackTypeOrig,Weight,Volume,CargoType) 
values (@BookingNo,@HblNo,@JobNo,@RefNo,@JobType,@ContNo,'P',@ShipperInfo,@ConsigneeInfo,@Responsible,@ConsigneeRemark,@ConsigneeEmail,@ConsigneeTel,@Mobile1,@ConsigneeAddress,@ClientId,@ClientEmail,@ClientAddress,@Remark1,@PrepaidInd,@CollectAmount1,
@CollectAmount2,@DutyPayment,@Incoterm,@Ocean_Freight,@Currency,@Qty,@PackTypeOrig,@Weight,@Volume,'IN') select @@identity");
        ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteScalar(sql, list);
        if (re.status)
        {
            string id = SafeValue.SafeString(re.context);

            sql = string.Format(@"update job_house set LineId={0} where Id={0}", id);
            ConnectSql_mb.ExecuteNonQuery(sql);

            //insert_cargo(SafeValue.SafeInt(id, 0));
            if (id.Length > 0)
            {
                action = true;
                for (int i = 25; true; i++)
                {

                    if (empty_i >= 10) { break; }
                    DateTime date = DateTime.Today;


                    string A = ws.Cells["A" + i].StringValue;
                    string B = ws.Cells["B" + i].StringValue;
                    string C = ws.Cells["C" + i].StringValue;
                    if (A.Trim().ToUpper().Equals("序号"))
                    {
                        beginImport = true;
                        empty_i++;
                        i = i + 1;
                        continue;
                    }
                    try
                    {
                        SortIndex = SafeValue.SafeInt(ws.Cells["A" + i].StringValue, 0);
                    }
                    catch
                    {
                        empty_i++;
                        continue;
                    }
                    if (C.Length <= 0 && B.Length <= 0)
                    {
                        empty_i++;
                        continue;
                    }
                    //empty_i = 0;

                    if (beginImport)
                    {
                        #region



                        string D = ws.Cells["D" + i].StringValue;
                        string E = ws.Cells["E" + i].StringValue;
                        string F = ws.Cells["F" + i].StringValue;
                        string G = ws.Cells["G" + i].StringValue;
                        #endregion
                        //=====================检测是否存在Bill Number
                        string billNo = D;
                        string billItem = E;
                        string sql_check = string.Format(@"select Marks1 from job_stock where OrderId={0} and SortIndex={1} and Marks1='{2}'", id, SortIndex, C);
                        DataTable dt_check = ConnectSql.GetTab(sql_check);
                        if (dt_check.Rows.Count > 0)
                        {
                            existDo++;
                            continue;
                        }
                        else
                        {
                            sql_check = string.Format(@"select count(*) from job_stock where OrderId={0}", id);
                            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_check), 0);
                            SortIndex = n + 1;
                        }
                        try
                        {
                            list = new List<ConnectSql_mb.cmdParameters>();
                            #region list add
                            cpar = new ConnectSql_mb.cmdParameters("@SortIndex", SortIndex, SqlDbType.Int, 100);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@B", B, SqlDbType.NVarChar, 100);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@C", C, SqlDbType.NVarChar, 100);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@D", D, SqlDbType.NVarChar, 100);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@E", SafeValue.SafeInt(E, 0), SqlDbType.Int);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@F", SafeValue.SafeDecimal(F, 0), SqlDbType.Decimal);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@G", SafeValue.SafeDecimal(G), SqlDbType.Decimal);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@OrderId", id, SqlDbType.Int, 100);
                            list.Add(cpar);
                            #endregion

                            sql = string.Format(@"insert into job_stock (SortIndex,Marks1,Marks2,Uom1,Qty2,Price1,Price2, 
OrderId) values (@SortIndex,@B,@C,@D,@E,@F,@G,@OrderId)");
                            re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                            if (re.status)
                            {
                                successJob++;
                                //action = true;
                            }
                            else
                            {
                                errorDo++;
                                //throw new Exception(re.context);
                            }

                        }
                        catch (Exception ex)
                        {
                            errorDo++;
                            //throw new Exception(ex.ToString());
                        }

                    }
                    else
                    {
                    }

                }
                if (action)
                {
                    //VilaParty(consignee, uen, ic, email1, email2, tel1, tel2, mobile1, mobile2, address, responsible);
                }
            }
        }
        re_text = string.Format(@"添加了 {0} 条货物", successJob);
        re_text += successItem > 0 ? string.Format(@" {0} 条记录.", successItem) : ".";
        re_text += existDo > 0 ? string.Format(@" {0} 条已存在", existDo) : "";
        re_text += errorDo > 0 ? string.Format(@" {0} 条错误", errorDo) : "";
        error_text = re_text;





    }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        string _name = "";
        string error_text = "";
        try
        {

            string _desc = "";
            UploadedFile[] files = this.file_upload1.UploadedFiles;
            _name = (files[0].FileName ?? "").ToLower().Trim();
            if (_name.Length == 0) return;

            ProcessFile(_name, _desc, files[0].FileBytes, out error_text);

            //Auto Payable
            //InsertPl();
            //Auto Payment

            this.lb_txt.Text = error_text.Length > 0 ? error_text : "Upload Error";
        }
        catch (Exception ex)
        {
            this.lb_txt.Text = "Upload File fail, pls try agin, error:" + ex.Message;

            string user = HttpContext.Current.User.Identity.Name;
            string sql1 = string.Format(@"insert into CTM_JobEventLog (CreateDatetime,Controller,JobType,Remark) values (getdate(),@Controller,@JobType,@Remark)");
            List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
            list1.Add(new ConnectSql_mb.cmdParameters("@Controller", user, SqlDbType.NVarChar, 100));
            list1.Add(new ConnectSql_mb.cmdParameters("@JobType", "Import Cargo List", SqlDbType.NVarChar, 100));
            list1.Add(new ConnectSql_mb.cmdParameters("@Remark", this.lb_txt.Text, SqlDbType.NVarChar, 300));
            ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
        }

    }
    public void ProcessFile(string _name, string _desc, byte[] _buffer, out string error_text)
    {
        DateTime now = DateTime.Now;
        string path1 = string.Format("~/files/Order/{0}_{1}_{2}/", now.Year, now.Month, now.Day);
        string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        string __name = "OrderList" + "_" + now.Year + now.Month + now.Minute + now.Second;
        if (!Directory.Exists(path3))
            Directory.CreateDirectory(path3);
        //string __name = _name.Replace(' ', '_').Replace('\'', '_');
        string path4 = path3 + __name;
        bool isOk = false;
        FileStream fs = new FileStream(path4, FileMode.Create);

        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(_buffer);
        bw.Close();
        fs.Close();
        string batch = DateTime.Now.ToString("yyyyMMddHHmmss");


        //ImportJob(batch, path4, out error_text);
        ImportExcel(batch, path4, out error_text);
    }
    private void insert_cargo(int id)
    {

        string sql = string.Format(@"insert into job_stock(OrderId,SortIndex,Marks1,Marks2,Qty2,Price1) values(@OrderId,100,@Marks1,@Marks2,1,380) ");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@OrderId", id, SqlDbType.Int, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Marks1", "OCEAN FREIGHT", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Marks2", "海运费", SqlDbType.NVarChar, 100));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status)
        {

        }
    }
    #region New Party
    protected bool VilaParty(string code, string uen, string ic, string email1, string email2, string tel1, string tel2, string mobile1, string mobile2, string address,string responsible)
    {
        
        bool action = false;
        string sql = string.Format(@"select top 1 SequenceId from XXParty order by SequenceId desc");
        int sequenceId = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (responsible == "个人")
            sql = string.Format(@"select count(*) from XXParty where Name like N'%{0}%' and CrNo='{1}' and Tel1='{2}'", code, ic, tel1);
        else
            sql = string.Format(@"select count(*) from XXParty where Name like N'%{0}%' and CrNo='{1}' and Tel1='{2}'", code, uen, tel1);
        int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (n > 0)
        {
            action = true;
        }
        else
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = null;
            sequenceId = sequenceId + 1;
            #region list add
            if (code.Length > 4)
            {
                if (responsible == "个人")
                {
                    cpar = new ConnectSql_mb.cmdParameters("@PartyId", code + "_" + sequenceId, SqlDbType.NVarChar, 100);
                    list.Add(cpar);
                }
                else
                {
                    cpar = new ConnectSql_mb.cmdParameters("@PartyId", code.Substring(0, 6) + "_" + sequenceId, SqlDbType.NVarChar, 100);
                    list.Add(cpar);
                }
            }
            else
            {
                cpar = new ConnectSql_mb.cmdParameters("@PartyId", code + DateTime.Today.Second, SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }

            cpar = new ConnectSql_mb.cmdParameters("@Code", code, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Name", code, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Status", "USE", SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Address", address, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Fax1", mobile1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Fax2", mobile2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            if (ic.Length > 0)
            {
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", ic, SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }
            if (uen.Length > 0)
            {
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", uen, SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }
            sql = string.Format(@"insert into XXParty(PartyId,Code,Name,IsCustomer,Status,Address,Tel1,Tel2,Fax1,Fax2,Email1,Email2,CrNo) values(@PartyId,@Code,@Name,1,@Status,@Address,@Tel1,@Tel2,@Fax1,@Fax2,@Email1,@Email2,@CrNo)");
            ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (re.status)
            {
                action = true;

            }
            else
            {
            }
            #endregion
        }
        return action;
    }
    protected bool UpdateParty(string name, string uen, string ic, string email1, string email2, string tel1, string tel2, string mobile1, string mobile2, string address, string responsible)
    {
        string sql = string.Format(@"select count(*) from XXParty where Name like N'%{0}%' and (CrNo='{1}' or CrNo='{2}')", name, uen, ic);
        int count = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        bool action = false;
        if (count > 0)
        {
            #region  Update Party
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = null;
            string code = "";
            if (name.Length > 4)
            {
                if (responsible != "个人")
                {
                    code = name.Substring(0, 4) + DateTime.Today.Second;
                }
                else
                {
                    code = name;
                }
            }
            else
            {
                code = name;
            }
            if (ic.Length > 0)
            {
                #region
                cpar = new ConnectSql_mb.cmdParameters("@Code", code, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Name", name, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Address", address, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax1", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax2", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", ic, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Value", "like '%" + name + "%'", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                sql = string.Format(@"update XXParty set Code=@Code,Name=@Name,Address=@Address,Tel1=@Tel1,Tel2=@Tel2,Fax1=@Fax1,Fax2=@Fax2,Email1=@Email1,Email2=@Email2,CrNo=@CrNo where Name=@Value or CrNo=@CrNo");
                ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (re.status)
                {
                    action = true;
                }
                #endregion
            }
            if (uen.Length > 0)
            {
                #region
                cpar = new ConnectSql_mb.cmdParameters("@Code", code, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Name", name, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Address", address, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax1", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax2", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", uen, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Value", "like '%" + name + "%'", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                sql = string.Format(@"update XXParty set Code=@Code,Name=@Name,Address=@Address,Tel1=@Tel1,Tel2=@Tel2,Fax1=@Fax1,Fax2=@Fax2,Email1=@Email1,Email2=@Email2,CrNo=@CrNo where Name=@Value or CrNo=@CrNo");
                ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (re.status)
                {
                    action = true;
                }
                #endregion
            }
            #endregion
        }
        return action;
    }
    #endregion
    public void DownloadFile()
    {
        //string fileRpath = MapPath("~/Modules/Freight/template/template.zip");
        string fileRpath = MapPath("~/Modules/Freight/template/excel.zip");
        Response.ClearHeaders();
        Response.Clear();
        Response.Expires = 0;
        Response.Buffer = true;
        Response.AddHeader("Accept-Language", "zh-tw");
        string name = System.IO.Path.GetFileName(fileRpath);
        System.IO.FileStream files = new FileStream(fileRpath, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] byteFile = null;
        if (files.Length == 0)
        {
            byteFile = new byte[1];
        }
        else
        {
            byteFile = new byte[files.Length];
        }
        files.Read(byteFile, 0, (int)byteFile.Length);
        files.Close();

        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
        Response.ContentType = "application/octet-stream;charset=gbk";
        Response.BinaryWrite(byteFile);
        Response.End();

    }
    protected void btn_download_Click(object sender, EventArgs e)
    {
        DownloadFile();
    }
    private void auto_add_trailer_trip(string jobNo, string jobType)
    {
        if (jobType == "WGR" || jobType == "WDO" || jobType == "TPT")
        {

            C2.CtmJobDet1 det1 = new C2.CtmJobDet1();
            det1.JobNo = jobNo;
            det1.ScheduleDate = DateTime.Today;
            det1.StatusCode = "New";
            det1.CfsStatus = "Pending";
            det1.ScheduleStartDate = DateTime.Today;
            det1.ContainerNo = "";

            C2.Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det1);

            string tripType = "";
            if (jobType == "WGR")
                tripType = "LOC";
            if (jobType == "WDO")
                tripType = "SHF";

            //C2.CtmJobDet2 det2 = new C2.CtmJobDet2();
            //det2.Det1Id = det1.Id;
            //det2.TripCode = tripType;
            //det2.ContainerNo = det1.ContainerNo;
            //det2.JobNo = jobNo;
            //C2.Manager.ORManager.StartTracking(det2, Wilson.ORMapper.InitialState.Inserted);
            //C2.Manager.ORManager.PersistChanges(det2);
        }
    }
}