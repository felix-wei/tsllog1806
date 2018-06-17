using Aspose.Cells;
using DevExpress.Web.ASPxUploadControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_Language : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            ProcessFile(_name, _desc, files[0].FileBytes);
            this.lb_txt.Text = "Upload File Success";
        }
        catch (Exception ex) { this.lb_txt.Text = "Upload File fail, pls try agin, error:" + ex.Message + ex.StackTrace; }

    }


    public void ProcessFile(string _name, string _desc, byte[] _buffer)
    {
        string path1 = string.Format("~/files/language/");
        string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        if (!Directory.Exists(path3))
            Directory.CreateDirectory(path3);
        string __name = _name.Replace(' ', '_').Replace('\'', '_');
        string path4 = path3 + __name;
        bool isOk = false;
        FileStream fs = new FileStream(path4, FileMode.Create);

        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(_buffer);
        bw.Close();
        fs.Close();
        string batch = DateTime.Now.ToString("yyyyMMddHHmmss");
        ImportJob(batch, path4);
    }



    public void ImportJob(string batch, string file)
    {
        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        wb.Open(file);
        int empty_i = 0;
        for (int i = 2; true; i++)
        {
            Worksheet ws = wb.Worksheets[0];

            string code = ws.Cells["B" + i.ToString()].StringValue.Trim();
            if (empty_i >= 30) { break; }
            if (code.Length <= 0)
            {
                empty_i++;
                continue;
            }
            empty_i = 0;
            //Regex reg = new Regex(@"^[A-Za-z]\w+$");
            //if (!reg.IsMatch(code)) { continue; }
            //if (code.IndexOf(' ') >= 0) { continue; }

            string en = ws.Cells["C" + i.ToString()].StringValue;
            string id = ws.Cells["D" + i.ToString()].StringValue;
            string zh = ws.Cells["E" + i.ToString()].StringValue;

            string sql = string.Format(@"select * from ref_language where code='{0}'", code);
            DataTable dt = ConnectSql.GetTab(sql);
            if (dt.Rows.Count > 0)
            {
                if (en.Length > 0)
                {
                    en = "'" + en + "'";
                }
                else
                {
                    en = "lgg_en";
                }
                if (zh.Length > 0)
                {
                    zh = "'" + zh + "'";
                }
                else
                {
                    zh = "lgg_zh";
                }
                if (id.Length > 0)
                {
                    id = "'" + id + "'";
                }
                else
                {
                    id = "lgg_id";
                }
                sql = string.Format(@"update ref_language set lgg_en={1},lgg_zh={2},lgg_id={3} where Id={0}", dt.Rows[0]["Id"], en, zh, id);
            }
            else
            {
                sql = string.Format(@"insert into ref_language (code,lgg_en,lgg_zh,lgg_id) values('{0}','{1}','{2}','{3}')", code, en, zh, id);
            }
            ConnectSql.ExecuteSql(sql);
        }

    }
    protected void btn_download_Click(object sender, EventArgs e)
    {

        //Regex reg = new Regex(@"^[A-Za-z]\w+$");
        string en = string.Format(@"
var common_language_en = {0}{1};
function common_language_en_init() {0}
    common_language_en = {0}", "{", "}");
        string zh = string.Format(@"
var common_language_zh = {0}{1};
function common_language_zh_init() {0}
    common_language_zh = {0}", "{", "}");
        string id = string.Format(@"
var common_language_id = {0}{1};
function common_language_id_init() {0}
    common_language_id = {0}", "{", "}");

        string sql = string.Format(@"select * from ref_language order by code");
        DataTable dt = ConnectSql.GetTab(sql);
        DataRow dr = null;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dr = dt.Rows[i];
            string code = dr["code"].ToString();
            string lgg_en = dr["lgg_en"].ToString();
            string lgg_zh = dr["lgg_zh"].ToString();
            string lgg_id = dr["lgg_id"].ToString();
            //if (!reg.IsMatch(code)) { continue; }
            if (code.Length <= 0) { continue; }
            if (lgg_en.Length > 0)
            {
                en += string.Format(@"
        '{0}':'{1}',", code, lgg_en);
            }
            if (lgg_zh.Length > 0)
            {
                zh += string.Format(@"
        '{0}':'{1}',", code, lgg_zh);
            }
            if (lgg_id.Length > 0)
            {
                id += string.Format(@"
        '{0}':'{1}',", code, lgg_id);
            }
        }

        en += string.Format(@"
    {1};
{1}", "{", "}");
        zh += string.Format(@"
    {1};
{1}", "{", "}");
        id += string.Format(@"
    {1};
{1}", "{", "}");
        string end = string.Format(@"
common_language_en_init();
common_language_zh_init();
common_language_id_init();");




        string path = Server.MapPath("~/files/language/language.js");

        if (File.Exists(path))
        {
            File.Delete(path);
        }


        StreamWriter sr = File.CreateText(path);
        sr.WriteLine(en);
        sr.WriteLine(zh);
        sr.WriteLine(id);
        sr.WriteLine(end);


        sr.Close();



        //Response.Redirect("/files/language/language.js");


        string fileName = "language.js";//客户端保存的文件名
        string filePath = Server.MapPath("/files/language/language.js");//路径

        FileInfo fileInfo = new FileInfo(filePath);
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.WriteFile(fileInfo.FullName);
        Response.Flush();
        Response.End();
    }
    protected void btn_download_excel_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select * from ref_language order by code");
        DataTable dt = ConnectSql.GetTab(sql);


        Workbook workbook = new Workbook();
        Worksheet worksheet = workbook.Worksheets[0];
        Cells cells = worksheet.Cells;
        Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
        //标题 
        style.HorizontalAlignment = TextAlignmentType.Center;//文字居中  
        style.Font.Name = "Arial";//文字字体 ,宋体
        style.Font.Size = 18;//文字大小  
        style.Font.IsBold = false;//粗体

        cells[0, 0].PutValue("");
        cells[0, 1].PutValue("code");
        cells[0, 2].PutValue("English");
        cells[0, 3].PutValue("Indonesia");
        cells[0, 4].PutValue("Chinese");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int j = i + 2;


            cells[j, 0].PutValue("");
            cells[j, 1].PutValue(dr["code"]);
            cells[j, 2].PutValue(dr["lgg_en"]);
            cells[j, 3].PutValue(dr["lgg_id"]);
            cells[j, 4].PutValue(dr["lgg_zh"]);

        }


        string str_date = DateTime.Now.ToString("yyyyMMddHHmmss");
        string path0 = string.Format("~/files/language/language_{0}.xlsx", str_date);
        string path = HttpContext.Current.Server.MapPath(path0);//POD_RECORD

        //workbook.Save(path);
        System.IO.MemoryStream ms = workbook.SaveToStream();//生成数据流 
        byte[] bt = ms.ToArray();
        workbook.Save(path);
        Response.Redirect(path0.Substring(1));
    }
}