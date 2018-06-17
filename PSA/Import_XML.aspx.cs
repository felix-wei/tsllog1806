using DevExpress.Web.ASPxUploadControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class PSA_Import_XML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_from.Date = DateTime.Today.AddDays(-1);
            search_to.Date = DateTime.Today;
        }

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select Note1,Note2,Note3,Note4,Note5 From x1 where 1=1");
        DataTable dt = ConnectSql.GetTab(sql);
        gv.DataSource = dt;
        gv.DataBind();
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
        catch (Exception ex) { this.lb_txt.Text = "Upload File fail, pls try agin, error:" + ex.Message + ex.StackTrace; }

    }


    public string _id = "0";
    public string _site = "ALL";
    public string _type = "ATTACH";
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
        //AddFile(_type, _code, pathx, _desc, __name, _buffer.Length);
        string batch = DateTime.Now.ToString("yyyyMMddHHmmss");
        ImportJob(batch, path4, out error_text);
    }


    public void ImportJob(string batch, string file, out string error_text)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(file);

        string re_text = "";

        int existDo = 0;
        int successJob = 0;
        int successItem = 0;
        int errorDo = 0;

        XmlNode xml_root = xml.SelectSingleNode("pro2_warehouse_inbound");
        if (xml_root != null)
        {
            XmlNode xml_job = xml_root.SelectSingleNode("job");
            if (xml_job != null)
            {
                XmlNodeList xml_bls = xml_job.SelectNodes("bl");
                foreach (XmlNode xml_t in xml_bls)
                {
                    XmlNodeList xml_bl = xml_t.ChildNodes;
                    List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    int i = 1;
                    string par0 = "";
                    string par1 = "";
                    for (; i <= xml_bl.Count; i++)
                    {
                        XmlNode xml_t1 = xml_bl[i-1];
                        //if (xml_t1.HasChildNodes)
                        //{
                        //    continue;
                        //}
                        ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@Note" + i, xml_t1.InnerXml, SqlDbType.NVarChar, 100);
                        list.Add(cpar);
                        par0 += par0.Length > 0 ? "," + "Note" + i : "Note" + i;
                        par1 += par1.Length > 0 ? "," + "@Note" + i : "@Note" + i;
                    }
                    string sql = string.Format(@"insert into X1 ({0}) values ({1})", par0, par1);
                    ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                    if (re.status)
                    {
                        successJob++;
                    }
                    else
                    {
                        throw new Exception(re.context);
                        errorDo++;
                    }

                }
            }
        }

        re_text = string.Format(@"uploaded {0} jobs", successJob);
        re_text += successItem > 0 ? string.Format(@" {0} items.", successItem) : ".";
        re_text += existDo > 0 ? string.Format(@" {0} existed", existDo) : "";
        re_text += errorDo > 0 ? string.Format(@" {0} error", errorDo) : "";
        error_text = re_text;
    }
}