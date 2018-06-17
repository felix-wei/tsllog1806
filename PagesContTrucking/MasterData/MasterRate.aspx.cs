using Aspose.Cells;
using DevExpress.Web.ASPxUploadControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using C2;
using DevExpress.Web.ASPxGridView;

public partial class PagesContTrucking_MasterData_MasterRate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_from.Date = DateTime.Today.AddDays(-1);
            search_to.Date = DateTime.Today;
        }
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
        else if (file.ToLower().EndsWith(".xls"))
        {
            wb.Open(file, FileFormatType.Default);
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



        for (int i = 1; true; i++)
        {

            if (empty_i >= 10) { break; }
            DateTime date = DateTime.Today;


            string A = ws.Cells["A" + i].StringValue;
            if (A.Trim().ToUpper().Equals("COMPANY"))
            {
                beginImport = true;
                empty_i++;
                continue;
            }
            try
            {
                A = ws.Cells["A" + i].StringValue;
            }
            catch
            {
                empty_i++;
                continue;
            }

            if (A.Length <= 0)
            {
                empty_i++;
                continue;
            }
            empty_i = 0;

            if (beginImport)
            {

                string[] cellsList = {"B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","AA","AB","AC","AD","AE","AF",
                                         "AG","AH","AI","AJ","AK","AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU" };
                for (int j = 0; j < cellsList.Length; j++)
                {
                    string code = "";
                    string value = ws.Cells[cellsList[j] + i].StringValue;
                    string header = ws.Cells[cellsList[j] + 1].StringValue;
                    if(value.Length>0){
                        try
                        {
                            
                            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                            //ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@A", SafeValue.SafeDate(A, new DateTime(1900, 1, 1)), SqlDbType.DateTime);
                            ConnectSql_mb.cmdParameters cpar = null;
                            #region list add
                            cpar = new ConnectSql_mb.cmdParameters("@Customer", A, SqlDbType.NVarChar, 100);
                            list.Add(cpar);
                            if (header.Length < 4)
                            {
                                code = (header.Trim().Replace("'", ""));
                                cpar = new ConnectSql_mb.cmdParameters("@Code",code, SqlDbType.NVarChar, 100);
                            }
                            else {
                                code=(header.Substring(0, 4).Trim().Replace("'", ""));
                                cpar = new ConnectSql_mb.cmdParameters("@Code",code , SqlDbType.NVarChar, 100);
                            }
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@Description", header, SqlDbType.NVarChar, 100);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@Price", value, SqlDbType.Decimal, 100);
                            list.Add(cpar);
                            cpar = new ConnectSql_mb.cmdParameters("@LoadedTime", DateTime.Today.ToString("yyyy-MM-dd"), SqlDbType.DateTime);
                            list.Add(cpar);
                            #endregion

                            string sql_check = string.Format(@"select CustomerName from [dbo].[MastertRate]  where [CustomerName]='{0}' and [Code]='{1}'", A, code);
                            DataTable dt_check = ConnectSql.GetTab(sql_check);
                            if (dt_check.Rows.Count > 0)
                            {
                                existDo++;
                                continue;
                            }
                            else
                            {
                                string sql = string.Format(@"insert into [dbo].[MastertRate] ([CustomerName],[Code],[Description],[Price],LoadedTime) 
values (@Customer,@Code,@Description,@Price,@LoadedTime)");
                                ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                                if (re.status)
                                {
                                    successJob++;
                                }
                                else
                                {
                                    errorDo++;
                                    //throw new Exception(re.context);
                                }
                            }
                           

                        }
                        catch (Exception ex)
                        {
                            errorDo++;
                            //throw new Exception(ex.ToString());
                        }
                    }
                }
                //=====================检测是否存在Bill Number


            }
            else
            {
            }

        }

        re_text = string.Format(@"uploaded {0} rate", successJob);
        re_text += successItem > 0 ? string.Format(@" {0} items.", successItem) : ".";
        re_text += existDo > 0 ? string.Format(@" {0} existed", existDo) : "";
        //re_text += errorDo > 0 ? string.Format(@" {0} error", errorDo) : "";
        error_text = re_text;
        string user = HttpContext.Current.User.Identity.Name;
        //string sql1 = string.Format(@"insert into CTM_JobEventLog (CreateDatetime,Controller,JobType,Remark) values (getdate(),@Controller,@JobType,@Remark)");
        //List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
        //list1.Add(new ConnectSql_mb.cmdParameters("@Controller", user, SqlDbType.NVarChar, 100));
        //list1.Add(new ConnectSql_mb.cmdParameters("@JobType", "PSA", SqlDbType.NVarChar, 100));
        //list1.Add(new ConnectSql_mb.cmdParameters("@Remark", re_text, SqlDbType.NVarChar, 300));
        //ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);




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
                if (f.Name.EndsWith(".csv"))
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
        string where = "";
        //string sql = string.Format(@"select * From [dbo].[MastertRate] where 1=1");
        //if (search_from.Date != null)
        //{
        //    where = " LoadedTime>='" + search_from.Date.ToString("yyyy-MM-dd") + "'";
        //}
        //if (search_to.Date != null)
        //{
        //    sql += " and LoadedTime<'" + search_to.Date.AddDays(1).ToString("yyyy-MM-dd") + "'";
        //}
        //DataTable dt = ConnectSql.GetTab(sql);
        if(txt_search_Code.Text.Trim()!=""){
            dsMasterRate.FilterExpression = string.Format(@"CustomerName like '%{0}%'", txt_search_Code.Text.Trim());
        }
        //
    }

    private string reGetDateFormat(string par, string file)
    {
        string res = "19000101";

        if (file.ToLower().EndsWith(".csv"))
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
                    res += " " + ar[1];
                }
            }
        }
        else
        {
            res = SafeValue.SafeDate(par, new DateTime(1900, 1, 1)).ToString("yyyyMMdd HH:mm:ss");
        }

        return res;
    }

    protected void gv_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.MastertRate));
        }
    }
    protected void gv_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["Price"] = 1;
        e.NewValues["CustomerId"] = "";
        e.NewValues["CustomerName"] = "";
        e.NewValues["Code"] = "";
        e.NewValues["Description"] = "";
        e.NewValues["LoadedTime"] = DateTime.Now;
    }
    protected void gv_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"]);
        e.NewValues["CreateDateTime"] = SafeValue.SafeDate(e.NewValues["CreateDateTime"], DateTime.Today);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"]);
        e.NewValues["CustomerId"] = SafeValue.SafeString(e.NewValues["CustomerId"]);
        e.NewValues["CustomerName"] = SafeValue.SafeString(e.NewValues["CustomerName"]);
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["LoadedTime"] = SafeValue.SafeDate(e.NewValues["LoadedTime"], DateTime.Today);
    }
    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"]);
        e.NewValues["UpdateDateTime"] = SafeValue.SafeDate(e.NewValues["CreateDateTime"], DateTime.Today);
        e.NewValues["CustomerId"] = SafeValue.SafeString(e.NewValues["CustomerId"]);
        e.NewValues["CustomerName"] = SafeValue.SafeString(e.NewValues["CustomerName"]);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"]);
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
    }
    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}