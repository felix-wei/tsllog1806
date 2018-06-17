using Aspose.Cells;
using DevExpress.Web.ASPxUploadControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using C2;
using System.IO.Compression;

public partial class PSA_Import_PSA_GIRO : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			//InsertPl();
			search_from.Date = DateTime.Today.AddDays(-1);
			search_to.Date = DateTime.Today;
		}
	}

	public void ImportJob(string batch, string file, out string error_text)
	{
		//throw new Exception("importjob");
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense(MapPath("~/Aspose.lic"));
		//Workbook wb = new Workbook();
        byte[] byData = new byte[100];
        char[] charData = new char[1000];
        List<string> list = new List<string>();
		if (file.ToLower().EndsWith(".txt"))
		{
            using (System.IO.StreamReader sr = new System.IO.StreamReader(file, true))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    list.Add(str);
                }
            }
		}

		int empty_i = 0;
		string re_text = "";
		//Worksheet ws = wb.Worksheets[0];

		int existDo = 0;
		int successJob = 0;
		int successItem = 0;
		int errorDo = 0;
        string notInside="";
        int insideDo = 0;
        string sql = "";
		//throw new Exception("here");
		//=================================== version 1
        bool beginImport = false;
		int _row = 0;
        DateTime payDate = DateTime.Today;
		string value = "";
        string cur = "SGD";
        int docId=0;
        int index = 0;

		for (int i = 0;i<list.Count; i++)
		{
			_row++;
			if (empty_i >= 10) { break; }
			DateTime date = DateTime.Today;

            string line = list[i];
            string[] ar = line.Split('~');
            if(i==0){
              ar = line.Split(' ');
              payDate = SafeValue.SafeDate(ar[7],date);
              docId= InsertPay(payDate,cur);
              
            }
            if(i==list.Count-1){
                ar = line.Split('~');

                decimal locAmt=SafeValue.SafeDecimal(ar[4]);
                sql = string.Format(@"update XAApPayment set DocAmt={1}, LocAmt={1} where SequenceId={0}",docId,locAmt);
                ConnectSql_mb.ExecuteNonQuery(sql);
                continue;
            }
            if(i==4){
                ar=line.Split(':');
                cur = ar[1];
            }
            if (ar[0] == "Invoice No.")
			{
				beginImport = true;
				empty_i++;
				continue;
                
			}

			//throw new Exception(ws.Cells["A" + i].Type.ToString());
			try
			{
                value = list[i];
			}
			catch
			{
				empty_i++;
				continue;
			}

            if (line.Length<= 0)
			{
				empty_i++;
				continue;
			}

			empty_i = 0;

			if (beginImport)
			{
                index++;
                string invNo =SafeValue.SafeString(ar[0]);
                DateTime invDate = SafeValue.SafeDate(ar[1],date);
                //string docType = SafeValue.SafeString(ar[2]);
                decimal amt = SafeValue.SafeDecimal(ar[4]);
				//=====================检测是否存在Bill Number
                string billDate=invDate.ToString("yyyy-MM-dd");
                string sql_check = string.Format(@"select count(*) from XAApPaymentDet where Remark1='{0}'", invNo);
                int check_n =SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_check),0);
                if (check_n > 0)
                {
                    existDo++;
                    continue;
                }
                else
                {
                    try
                    {
                        sql_check = string.Format(@"select SequenceId,DocNo,DocType,CurrencyId,DocDate from XAApPayable where SupplierBillNo='{0}' and SupplierBillDate='{1}'", invNo, billDate);
                        DataTable dt_check = ConnectSql.GetTab(sql_check);
                        if (dt_check.Rows.Count > 0)
                        {
                            string docNo = SafeValue.SafeString(dt_check.Rows[0]["DocNo"]);
                            string docType = SafeValue.SafeString(dt_check.Rows[0]["DocType"]);
                            DateTime docDate = SafeValue.SafeDate(dt_check.Rows[0]["DocDate"], DateTime.Today);
                            InsertPay_Det(docId, docNo, docType, index, invNo, "", "", 1, amt, cur, 1, 0, "", "", "", "", invDate);
                            successJob++;
                        }
                        else
                        {
                            insideDo++;
                            if (insideDo == 1)
                            {
                                notInside += invNo;
                            }
                            else {
                                notInside += invNo + ",";
                            }
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        //errorDo++;
                        throw new Exception(_row.ToString() + "##" + ex.Message);
                    }
                }
			}
			else
			{
			}
		}

        sql = string.Format(@"select DocNo,DocDate,LocAmt from XAApPayment where SequenceId={0}",docId);
        DataTable dt_pay = ConnectSql_mb.GetDataTable(sql);
        string billNo = "";
        decimal totalAmt = 0;
        DateTime docDate1 = DateTime.Today;
        if(dt_pay.Rows.Count>0){
            billNo = SafeValue_mb.SafeString(dt_pay.Rows[0]["DocNo"]);
            totalAmt = SafeValue.SafeDecimal(dt_pay.Rows[0]["LocAmt"]);
            docDate1 = SafeValue.SafeDate(dt_pay.Rows[0]["DocDate"], DateTime.Today);
        }
		re_text = string.Format(@"uploaded {0} lines", successJob);
		re_text += successItem > 0 ? string.Format(@" {0} items.", successItem) : ".";
		re_text += existDo > 0 ? string.Format(@" {0} existed", existDo) : "";
        re_text += errorDo > 0 ? string.Format(@" {0} error", errorDo) : "";
        re_text += insideDo>0 ? string.Format(@" ,{0} not inside the payable", "Invoice:【" + notInside + "】") : "";
		error_text = re_text;
		string user = HttpContext.Current.User.Identity.Name;
        string sql1 = string.Format(@"insert into CTM_JobEventLog (CreateDatetime,Controller,JobType,Remark,Note1,Note2,Value1) values (getdate(),@Controller,@JobType,@Remark,@Note1,@Note2,@Value1)");
		List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
		list1.Add(new ConnectSql_mb.cmdParameters("@Controller", user, SqlDbType.NVarChar, 100));
		list1.Add(new ConnectSql_mb.cmdParameters("@JobType", "GIRO", SqlDbType.NVarChar, 100));
		list1.Add(new ConnectSql_mb.cmdParameters("@Remark", re_text, SqlDbType.NVarChar, 300));
        list1.Add(new ConnectSql_mb.cmdParameters("@Note1", billNo, SqlDbType.NVarChar, 300));
        list1.Add(new ConnectSql_mb.cmdParameters("@Note2", docDate1.ToString("yyyy-MM-dd"), SqlDbType.NVarChar, 300));
        list1.Add(new ConnectSql_mb.cmdParameters("@Value1", totalAmt, SqlDbType.NVarChar, 300));
		ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
	}

	protected void btn_upload_Click(object sender, EventArgs e)
	{
		string _name = "";
		try
		{
			string _desc = "";
			UploadedFile[] files = this.file_upload1.UploadedFiles;
			_name = (files[0].FileName ?? "").ToLower().Trim();
			if (_name.Length == 0) return;
			string error_text = "";
			ProcessFile(_type, _id, _name, _desc, files[0].FileBytes, out error_text);

			//InsertPay();
			//Auto Payment
			this.lb_txt.Text = error_text.Length > 0 ? error_text : "Upload Error";
		}
		catch (Exception ex)
		{
			this.lb_txt.Text = "Upload File fail, pls try agin, error:" + ex.Message + ex.StackTrace;

			string user = HttpContext.Current.User.Identity.Name;
			string sql1 = string.Format(@"insert into CTM_JobEventLog (CreateDatetime,Controller,JobType,Remark) values (getdate(),@Controller,@JobType,@Remark)");
			List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
			list1.Add(new ConnectSql_mb.cmdParameters("@Controller", user, SqlDbType.NVarChar, 100));
			list1.Add(new ConnectSql_mb.cmdParameters("@JobType", "PSA", SqlDbType.NVarChar, 100));
			list1.Add(new ConnectSql_mb.cmdParameters("@Remark", this.lb_txt.Text, SqlDbType.NVarChar, 300));
			ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
		}
	}

	public string _id = "0";
	public string _site = "ALL";
	public string _type = "PSA_ZipFile";
	public string _page = "AttachEdit";
	public string _where = "Where";
	public string _key = "id";
	public string _query = "RowType=''";
	public string _code = "code";
	public string _status = "LIST";
	public string _table = "X2";
	public string _cat = "";
	public void ProcessFile(string _type, string _code, string _name, string _desc, byte[] _buffer, out string error_text)
	{
		string path1 = string.Format("~/files/{0}_{1}/", _type, _code);
		string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        string __name = _name.Replace(' ', '_').Replace('\'', '_');
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
		//AddFile(_type, _code, pathx, _desc, __name, _buffer.Length);
		string batch = DateTime.Now.ToString("yyyyMMddHHmmss");

		if (path4.ToLower().EndsWith(".zip"))
		{
			string folder_dir = path4.Substring(0, path4.Length - 4);
			ZipFile.ExtractToDirectory(path4, folder_dir);
			DirectoryInfo dir = new System.IO.DirectoryInfo(folder_dir);
			string target_file = "";
			foreach (FileInfo f in dir.GetFiles())
			{
				string _fname = f.Name.ToLower();
                if (_fname.EndsWith(".csv") || _fname.EndsWith(".xls") || _fname.EndsWith(".xlsx"))
                {
                    target_file = f.FullName;
                    break;
                }
            }
            error_text = "";
            if (target_file != "")
            {
                ImportJob(batch, target_file, out error_text);
            }
            dir.Delete(true);
        }
        else
        {
            ImportJob(batch, path4, out error_text);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select * From psa_bill where 1=1");
        if (search_from.Date != null)
        {
            sql += " and F1>='" + search_from.Date.ToString("yyyyMMdd") + "'";
        }
        if (search_to.Date != null)
        {
            sql += " and F1<'" + search_to.Date.AddDays(1).ToString("yyyyMMdd") + "'";
        }
        DataTable dt = ConnectSql.GetTab(sql);
        gv.DataSource = dt;
        gv.DataBind();
    }

    private string reGetDateFormat(string par, string file)
    {
        string res = "19000101";

        if (file.ToLower().EndsWith(".csv") || file.ToLower().EndsWith(".xls"))
        {
            string[] ar = par.Split(' ');
            if (ar[0].Length > 0)
            {

                string[] ar_date = ar[0].Split('/');
                if (ar_date.Length < 3)
                {
                    return res;
                }
                string ar_date_year = ar_date[2];

                if (ar_date_year.Length == 2)
                {
                    ar_date_year = DateTime.Now.ToString("yyyy").Substring(0, 2) + ar_date_year;
                }
                DateTime dt_t = new DateTime(SafeValue.SafeInt(ar_date_year, 1900), SafeValue.SafeInt(ar_date[1], 1), SafeValue.SafeInt(ar_date[0], 1));
               res = dt_t.ToString("yyyyMMdd");
                if (ar.Length > 1)
                {
                    //res += " " + ar[1];
                }
            }
        }
        else
        {
            res = SafeValue.SafeDate(par, new DateTime(1900, 1, 1)).ToString("yyyyMMdd HH:mm:ss");
        }
		//throw new Exception("[" + par + "###" + res + "]");
        return res;
    }
    private int InsertPay(DateTime docDate, string cur)
    {

        string counterType = "";
        int invId = 0;
        #region invoice mast
        counterType = "AP-PAYMENT-Job";
        C2.XAApPayment inv = new C2.XAApPayment();


        inv.DocDate = docDate;
        string  invN = C2Setup.GetNextNo("", counterType, inv.DocDate);
        inv.PartyTo = "PSAC";
        inv.DocType = "PS";
        inv.DocType1 = "Job";
        inv.DocNo = invN.ToString();
        string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

        inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
        inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
        inv.ChqNo = "GIRO";
        //

        inv.Remark = "";
        inv.CurrencyId = cur;
        inv.ExRate = 1;
        inv.AcCode = "120401";
        inv.AcSource = "CR";
        inv.MastRefNo = "";
        inv.JobRefNo = "";

        inv.ExportInd = "N";
        inv.CreateBy = HttpContext.Current.User.Identity.Name;
        inv.CreateDateTime = DateTime.Now;
        inv.CancelDate = new DateTime(1900, 1, 1);
        inv.CancelInd = "N";
        try
        {

            C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(inv);

            invId = inv.SequenceId;

            invN = C2Setup.GetNextNo("AP-PAYMENT-" + inv.DocType1);
            string sql = string.Format("Update XAApPayment set DocNo='{1}',GenerateInd='Y' where SequenceId='{0}'", invId, invN);
            if (ConnectSql.ExecuteSql(sql) > -1)
                C2Setup.SetNextNo("AP-PAYMENT-" + inv.DocType1, invN);

        }
        catch
        {
        }
        #endregion
        return invId;
    }
    private void InsertPay_Det(int docId, string docNo, string docType, int index, string code, string refNo, string des, decimal qty, decimal price, string cur, decimal exRate, decimal gst, string billType, string billNo, string mastType, string cntNo,DateTime docDate)
    {
        try
        {
            C2.XAApPaymentDet det = new C2.XAApPaymentDet();
            det.PayNo = docId.ToString();
            det.PayId = docId;
            det.DocId = 0;
            det.PayLineNo = index;
            det.DocNo = docNo;
            det.DocDate = docDate;
            det.DocType = docType;
            det.AcCode = "20";
            det.AcSource = "DB";
            det.MastRefNo = refNo;
            det.JobRefNo = "";


            det.Remark1 = code;
            det.Remark2 = des;
            det.Remark3 = cntNo;
            det.DocAmt = price;
            det.Currency = cur;
            det.ExRate = 1;

            if (det.ExRate == 0)
                det.ExRate = 1;
            decimal locAmt = SafeValue.ChinaRound(price * det.ExRate, 2);
            det.DocAmt = price;
            det.LocAmt = locAmt;
            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det);
        }
        catch
        {
        }
    }
	
     private void UpdateApPayment(int docId)
    {
        string sql = "select SUM(docAmt) from XAApPaymentDet where PayId ='" + docId + "' and DocType='PS'";
        decimal docAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        string sql_invoice = string.Format("update XAApPayment set DocAmt={1},LocAmt={2} where SequenceId={0}", docId, docAmt, docAmt);
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }


    private void feeUpdate(string tariffCode, string tariffDes, string Cont, string Voyage, Decimal amount)
    {
        string fee_code = "";
        switch (tariffCode)
        {
            case "8928":
                fee_code = "Fee23";
                break;
            case "4546":
                fee_code = "Fee21";
                break;
            case "5314":
                fee_code = "Fee31";
                break;
            case "5311":
                fee_code = "Fee31";
                break;
            case "4361":
                fee_code = "Fee32";
                break;
            case "9502":
                fee_code = "Fee33";
                break;
        }
        if (!fee_code.Equals(""))
        {
            Cont = Cont.Replace(" ", "");
            string sql = string.Format(@"select distinct job.JobNo from ctm_job as job
left outer join ctm_jobdet1 as det1 on job.jobno=det1.jobno
where det1.ContainerNo=@ContainerNo and job.Voyage=@Voyage");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", Cont, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@Voyage", Voyage, SqlDbType.NVarChar, 100));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                string jobNo = dt.Rows[0]["JobNo"].ToString();
                sql = string.Format(@"update ctm_jobdet1 set {0}=@fee where ContainerNo=@ContainerNo and JobNo=@JobNo", fee_code);
                list.Add(new ConnectSql_mb.cmdParameters("@fee", amount, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar, 100));
                if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
                {
                    string user = HttpContext.Current.User.Identity.Name;
                    C_Job_Detail_EventLog c = new C_Job_Detail_EventLog();
                    c.Controller = user;
                    c.Remark = tariffDes + ": " + amount;
                    c.JobNo = jobNo;
                    c.ContainerNo = Cont;
                    c.Job_Detail_EventLog_Add();
                }
            }

        }
    }

    private class C_Job_Detail_EventLog
    {
        #region columns
        public int Id { get; set; }
        DateTime CreateDateTime { get; set; }
        public string Controller { get; set; }
        public string JobNo { get; set; }
        public string JobType { get; set; }
        public string ParentJobNo { get; set; }
        public string ParentJobType { get; set; }
        public string ContainerNo { get; set; }
        public string Trip { get; set; }
        public string Driver { get; set; }
        public string Towhead { get; set; }
        public string Trail { get; set; }
        public string Remark { get; set; }
        public string Note1 { get; set; }
        public string Note1Type { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Platform { get; set; }
        #endregion

        public void Job_Detail_EventLog_Add()
        {
            C_Job_Detail_EventLog l = this;
            //string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type) values(getdate(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", l.Controller, l.JobNo, l.ContainerNo, l.Trip, l.Driver, l.Towhead, l.Trail, l.Remark, l.Note1, l.Note2, l.Note3, l.Note4, l.Lat, l.Lng, l.Platform, l.JobType, l.ParentJobNo, l.ParentJobType, l.Note1Type);
            //ConnectSql_mb.ExecuteNonQuery(sql);

            string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type) values(getdate(),@Controller,@JobNo,@ContainerNo,@Trip,@Driver,@Towhead,@Trail,@Remark,@Note1,@Note2,@Note3,@Note4,@Lat,@Lng,@Platform,@JobType,@ParentJobNo,@ParentJobType,@Note1Type)");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@Controller", l.Controller, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@JobNo", l.JobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", l.ContainerNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Trip", l.Trip, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Driver", l.Driver, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Towhead", l.Towhead, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Trail", l.Trail, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Remark", l.Remark, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note1", l.Note1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note2", l.Note2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note3", l.Note3, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note4", l.Note4, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Lat", l.Lat, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Lng", l.Lng, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Platform", l.Platform, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@JobType", l.JobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ParentJobNo", l.ParentJobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ParentJobType", l.ParentJobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note1Type", l.Note1Type, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            ConnectSql_mb.ExecuteNonQuery(sql, list);
        }
    }

}