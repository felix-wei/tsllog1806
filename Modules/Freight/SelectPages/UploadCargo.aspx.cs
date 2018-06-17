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

public partial class Modules_Freight_SelectPages_UploadCargo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            if (Request.QueryString["sn"] != null)
            {
                lbl_No.Text = SafeValue.SafeString(Request.QueryString["sn"]);
            }
        }
    }
    public void ImportJob(string batch, string file, out string error_text)
    {
        string Oid = lbl_No.Text;
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

        for (int i = 1; true; i++)
        {

            if (empty_i >= 10) { break; }
            DateTime date = DateTime.Today;


            string A = ws.Cells["A" + i].StringValue;
            string B = ws.Cells["B" + i].StringValue;
            string C = ws.Cells["C" + i].StringValue;
            if (A.Trim().ToUpper().Equals("序号") || A.Trim().ToUpper().Equals("此行务须删除"))
            {
                beginImport = true;
                empty_i++;
                //i = i + 1;
                continue;
            }
            try
            {
                SortIndex = SafeValue.SafeInt(ws.Cells["A" + i].StringValue,0);
            }
            catch
            {
                empty_i++;
                continue;
            }

            if (B.Length <= 0&&C.Length<=0)
            {
                empty_i++;
                continue;
            }
            empty_i = 0;

            if (beginImport)
            {
                #region
                
                string D = ws.Cells["D" + i].StringValue;
                string E = ws.Cells["E" + i].StringValue;
                string F = ws.Cells["F" + i].StringValue;
                string G = ws.Cells["G" + i].StringValue;
                string H = ws.Cells["H" + i].StringValue;

                #endregion
                //=====================检测是否存在Bill Number
                string billNo = D;
                string billItem = E;
                string sql_check="";
                DataTable dt_check=null;
                //if (B.Length > 0)
                //{
                //    sql_check = string.Format(@"select Marks1 from XWJobStock where JobDetId={0} and Marks1='{1}'and Marks2!='海运费'", Oid, B);
                //}
                //if(C.Length>0){
                //    sql_check = string.Format(@"select Marks1 from XWJobStock where JobDetId={0} and Marks2='{1}' and Marks2!='海运费'", Oid, C);
                //}

                //dt_check = ConnectSql.GetTab(sql_check);
                //if (dt_check.Rows.Count > 0)
                //{
                //    existDo++;
                //    continue;
                //}
                //else {
                //    sql_check = string.Format(@"select count(*) from XWJobStock where JobDetId={0} and Marks2!='海运费'", Oid);
                //    int n =SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_check),0);
                //    SortIndex = n + 1;

                //}
                try
                {
                    List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    ConnectSql_mb.cmdParameters cpar = null;
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
                    cpar = new ConnectSql_mb.cmdParameters("@F", SafeValue.SafeDecimal(F), SqlDbType.NVarChar);
                    list.Add(cpar);
                    cpar = new ConnectSql_mb.cmdParameters("@G", SafeValue.SafeDecimal(G), SqlDbType.Float);
                    list.Add(cpar);
                    cpar = new ConnectSql_mb.cmdParameters("@OrderId", Oid, SqlDbType.Int, 100);
                    list.Add(cpar);
                    #endregion

                    string sql = string.Format(@"insert into job_stock (SortIndex,Marks1,Marks2,Uom1,Qty2,Price1,Price2, 
OrderId) values (@SortIndex,@B,@C,@D,@E,@F,@G,@OrderId)");
                    ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                    if (re.status)
                    {
                        successJob++;
                        action = true;
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
            string H = ws.Cells["H" + 3].StringValue;
            string I = ws.Cells["I" + 3].StringValue;
            string J = ws.Cells["J" + 3].StringValue;
            string K = ws.Cells["K" + 3].StringValue;
            string L = ws.Cells["L" + 3].StringValue;

            string user = HttpContext.Current.User.Identity.Name;
            string sql1 = string.Format(@"update job_house set Qty=@H,PackTypeOrig=@I,Weight=@J,Volume=@K,Currency=@L where Id=@Id", Oid);
            List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
            list1.Add(new ConnectSql_mb.cmdParameters("@J", SafeValue.SafeInt(H, 0), SqlDbType.Int, 100));
            list1.Add(new ConnectSql_mb.cmdParameters("@K", SafeValue.SafeString(I), SqlDbType.NVarChar, 100));
            list1.Add(new ConnectSql_mb.cmdParameters("@L", SafeValue.SafeDecimal(J), SqlDbType.Decimal, 300));
            list1.Add(new ConnectSql_mb.cmdParameters("@M", SafeValue.SafeDecimal(K), SqlDbType.Decimal, 300));
            list1.Add(new ConnectSql_mb.cmdParameters("@M", SafeValue.SafeDecimal(L), SqlDbType.Decimal, 300));
            list1.Add(new ConnectSql_mb.cmdParameters("@Id", Oid, SqlDbType.Decimal, 300));
            ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
            string sql_check = string.Format(@"select count(*) from job_stock where OrderId={0} and Marks2='海运费'", Oid);
            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_check), 0);
            if (n==0)
            {
                insert_cargo(SafeValue.SafeInt(Oid, 0));
            }
        }
        re_text = string.Format(@"已成功导入 {0} 条记录", successJob);
        re_text += successItem > 0 ? string.Format(@" {0} 记录.", successItem) : ".";
        re_text += existDo > 0 ? string.Format(@" {0} 已存在", existDo) : "";
        re_text += errorDo > 0 ? string.Format(@" {0} 错误", errorDo) : "";
        error_text = re_text;





    }
    public void ImportExcel(string batch, string file, out string error_text)
    {
        string Oid = lbl_No.Text;
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

        for (int i = 3; true; i++)
        {

            if (empty_i >= 10) { break; }
            DateTime date = DateTime.Today;


            string A = ws.Cells["A" + i].StringValue;
            string B = ws.Cells["B" + i].StringValue;
            string C = ws.Cells["C" + i].StringValue;
            if (A.Trim().ToUpper().Equals("序号")||B.Trim().ToUpper().Equals("ENGLISH") )
            {
                beginImport = true;
                empty_i++;
                //i = i + 1;
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

            if (B.Length <= 0 && C.Length <= 0)
            {
                empty_i++;
                continue;
            }
            empty_i = 0;

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
                string sql_check = "";
                DataTable dt_check = null;
                try
                {
                    List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    ConnectSql_mb.cmdParameters cpar = null;
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
                    cpar = new ConnectSql_mb.cmdParameters("@F", SafeValue.SafeDecimal(F), SqlDbType.Decimal);
                    list.Add(cpar);
                    cpar = new ConnectSql_mb.cmdParameters("@G", SafeValue.SafeDecimal(G), SqlDbType.Decimal);
                    list.Add(cpar);
                    cpar = new ConnectSql_mb.cmdParameters("@OrderId", Oid, SqlDbType.Int, 100);
                    list.Add(cpar);
                    #endregion

                    string sql = string.Format(@"insert into job_stock (SortIndex,Marks1,Marks2,Uom1,Qty2,Price1,Price2, 
OrderId) values (@SortIndex,@B,@C,@D,@E,@F,@G,@OrderId)");
                    ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                    if (re.status)
                    {
                        successJob++;
                        action = true;
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
            string B = ws.Cells["B" + 2].StringValue;
            string D = ws.Cells["D" + 2].StringValue;
            string F = ws.Cells["F" + 2].StringValue;
            string H = ws.Cells["H" + 2].StringValue;
            string J = ws.Cells["J" + 2].StringValue;

            string user = HttpContext.Current.User.Identity.Name;
            if (B.Length > 0 && D.Length > 0 && F.Length > 0 && H.Length > 0 && J.Length > 0)
            {
                string sql1 = string.Format(@"update job_house set Qty=@B,PackTypeOrig=@D,Weight=@F,Volume=@H,Currency=@J where Id=@Id", Oid);
                List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
                list1.Add(new ConnectSql_mb.cmdParameters("@B", SafeValue.SafeInt(H, 0), SqlDbType.Int, 100));
                list1.Add(new ConnectSql_mb.cmdParameters("@D", SafeValue.SafeString(D), SqlDbType.NVarChar, 100));
                list1.Add(new ConnectSql_mb.cmdParameters("@F", SafeValue.SafeDecimal(F), SqlDbType.Decimal, 300));
                list1.Add(new ConnectSql_mb.cmdParameters("@H", SafeValue.SafeDecimal(H), SqlDbType.Decimal, 300));
                list1.Add(new ConnectSql_mb.cmdParameters("@J", SafeValue.SafeDecimal(J), SqlDbType.Decimal, 300));
                list1.Add(new ConnectSql_mb.cmdParameters("@Id", Oid, SqlDbType.Decimal, 300));
                ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
                string sql_check = string.Format(@"select count(*) from job_stock where OrderId={0} and Marks2='海运费'", Oid);
                int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_check), 0);
                if (n == 0)
                {
                    insert_cargo(SafeValue.SafeInt(Oid, 0));
                }
            }
        }
        re_text = string.Format(@"已成功导入 {0} 条记录", successJob);
        re_text += successItem > 0 ? string.Format(@" {0} 记录.", successItem) : ".";
        re_text += existDo > 0 ? string.Format(@" {0} 已存在", existDo) : "";
        re_text += errorDo > 0 ? string.Format(@" {0} 错误", errorDo) : "";
        error_text = re_text;





    }
    private void delete_cargo(int id)
    {
        string sql = string.Format(@"delete from job_stock where OrderId={0}",id);
        ConnectSql_mb.ExecuteNonQuery(sql);
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
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        string _name = "";
        string error_text = "";
        try
        {
            if (lbl_No.Text != "")
            {
                string _desc = "";
                UploadedFile[] files = this.file_upload1.UploadedFiles;
                _name = (files[0].FileName ?? "").ToLower().Trim();
                if (_name.Length == 0) return;
                
                ProcessFile(_name, _desc, files[0].FileBytes, out error_text);

                //Auto Payable
                //InsertPl();
                //Auto Payment

                this.lb_txt.Text = error_text.Length > 0 ? error_text : "导入失败，请联系技术员";
                this.lbl_mes.Text = error_text.Length > 0 ? "导入成功" : "导入失败，请联系技术员";
            }
            else {
                this.lb_txt.Text = error_text.Length > 0 ? error_text : "导入失败，请联系技术员";
            }


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
        string path1 = string.Format("~/files/{0}_{1}_{2}/", now.Year,now.Month,now.Day);
        string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        string __name = "CargoList"+"_"+now.Year+now.Month+now.Minute+now.Second;
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
        delete_cargo(SafeValue.SafeInt(lbl_No.Text, 0));
        ImportExcel(batch, path4, out error_text);
        
    }

}