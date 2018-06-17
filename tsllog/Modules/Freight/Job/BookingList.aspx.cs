using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wilson.ORMapper;

public partial class Modules_Freight_Job_BookingList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            txt_search_dateFrom.Date = DateTime.Now;
            txt_search_dateTo.Date = DateTime.Now.AddDays(7);
        }
    }   
    public string FilePath(int id)
    {
        string sql = string.Format("select top 1 FilePath from CTM_Attachment where JobNo='{0}'", id);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    public string Status(object s) { 
        string status=SafeValue.SafeString(s);
        if (status == "P")
            status = "Pending";
        if (status == "C")
            status = "Completed";
        return status;
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("Uploadline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                ASPxTextBox txt_ContNo = grid.FindRowCellTemplateControl(rowIndex, null, "txt_ContNo") as ASPxTextBox;
                e.Result = txt_Id.Text + "_" + txt_ContNo.Text;
                #endregion
            }
            if (ar[0].Equals("UploadCargoline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                ASPxTextBox txt_ContNo = grid.FindRowCellTemplateControl(rowIndex, null, "txt_ContNo") as ASPxTextBox;
                e.Result = txt_Id.Text;
                #endregion
            }
            if (ar[0].Equals("CargoListline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                e.Result = txt_Id.Text;
                #endregion
            }
            if (ar[0].Equals("Confirmedline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_JobId = grid.FindRowCellTemplateControl(rowIndex, null, "lbl_JobId") as ASPxLabel;
                if (lbl_JobId != null)
                {
                    string sql = string.Format(@"update ctm_job set JobStatus='{0}' where Id={1}", "Confirmed", SafeValue.SafeInt(lbl_JobId.Text, 0));
                    int n = ConnectSql_mb.ExecuteNonQuery(sql);
                    if (n > 0)
                    {
                        e.Result = "Action Success!";
                    }
                }
                #endregion
            }
        }
        if (pars.Length >= 2 && (pars.Substring(0, 2) == "BR"))
        {
            #region Receive
            string docnos = pars.Substring(2);
            string[] docs = docnos.Split(new char[] { ',' });
            int done = 0;
            int all = 0;
            string result = "";
            if (docs.Length > 1)
            {
                for (int i = 0; i < docs.Length; i++)
                {

                    string id = docs[i].Trim();
                    if (id != "")
                    {
                        all++;
                        string sql = string.Format(@"update job_house set CargoStatus='C',QtyOrig=Qty,WeightOrig=Weight,VolumeOrig=Volume where Id=@Id");
                        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                        list.Add(new ConnectSql_mb.cmdParameters("@Id", id, SqlDbType.Int));
                        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
                        if (res.status)
                            done++;
                    }
                }
                result = string.Format("Total received : {0} / {1}", done, all);
            }
            else
            {
                result = string.Format("Action Error ! Pls select at least one");
            }
            e.Result = result;
            #endregion
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = " CargoType='IN' and h.JobType='WGR' ";
        string from = "";
        string to = "";
        string sql = string.Format(@"select h.*,job.Vessel,job.Voyage,job.JobDate,job.JobStatus,job.Id as JobId from job_house h inner join ctm_job job on h.JobNo=job.JobNo");
        if(txt_search_jobNo.Text!=""){
            where = GetWhere(where, string.Format(@" JobNo='{0}'", txt_search_jobNo.Text));
        }
        else if (txt_search_BookingNo.Text != "")
        {
            where = GetWhere(where, string.Format(@" BookingNo='{0}'", txt_search_BookingNo.Text));
        }
        else if (txt_HblNo.Text != "")
        {
            where = GetWhere(where, string.Format(@" HblNo='{0}'", txt_HblNo.Text));
        }
        else {
            if (txt_search_dateFrom.Date != null && txt_search_dateTo.Date != null)
            {
                from = txt_search_dateFrom.Date.ToString("yyyyMMdd");
                to = txt_search_dateTo.Date.ToString("yyyyMMdd");
            }
            if (cbb_Type.Value != null)
            {
                where = GetWhere(where, string.Format(@" CargoStatus='{0}'", cbb_Type.Value));
            }
            if (txt_Product.Text != "")
            {
                where = GetWhere(where, string.Format(@" SkuCode='{0}'", txt_Product.Text));
            }
            if (txt_Pol.Text != "")
            {
                where = GetWhere(where, string.Format(@" job.Pol='{0}'", txt_Pol.Text));
            }
            if (txt_Pod.Text != "")
            {
                where = GetWhere(where, string.Format(@" job.Pod='{0}'", txt_Pod.Text));
            }
            if (txt_Vessel.Text != "") {
                where= GetWhere(where, string.Format(@" job.Vessel='{0}'", txt_Vessel.Text));
            }
            if(from.Length>0&&to.Length>0){
                where = GetWhere(where, string.Format(@" '{0}'<=JobDate and JobDate<='{1}'", from, to));
            }
        }
        if (where.Length > 0)
        {
            sql += " where " + where;
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        grid.DataSource = dt;
        grid.DataBind();
        //dsWh.FilterExpression = where;
    }
    private string GetWhere(string where,string s) {
        if (where.Length > 0)
        {
            where += " and" + s;
        }
        else {
            where = s;
        }
        return where;
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.JobHouse));
        }
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        btn_search_Click(null,null);
    }
}