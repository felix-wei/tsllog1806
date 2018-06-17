using DevExpress.Web;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Modules_Freight_Job_OrderList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_Sch_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void grd_Det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobHouse));
        }
    }
    public string ShowStatus(object res)
    {
        string str = "";
        if (SafeValue.SafeString(res) == "USE")
        {
            str = "待确认";
        }
        if (SafeValue.SafeString(res) == "ORDER")
        {
            str = "已下单";
        }
        if (SafeValue.SafeString(res) == "IN")
        {
            str = "已入库";
        }
        if (SafeValue.SafeString(res) == "PICKED")
        {
            str = "已排仓";
        }
        if (SafeValue.SafeString(res) == "OUT")
        {
            str = "已出库";
        }
        if (SafeValue.SafeString(res) == "SHIPMENT")
        {
            str = "已装船";
        }
        if (SafeValue.SafeString(res) == "DEPARTURE")
        {
            str = "已出港";
        }
        if (SafeValue.SafeString(res) == "ARRIVED")
        {
            str = "已抵港";
        }
        if (SafeValue.SafeString(res) == "GR")
        {
            str = "已入库";
        }
        if (SafeValue.SafeString(res) == "DO")
        {
            str = "已出库";
        }
        if (SafeValue.SafeString(res) == "SEND")
        {
            str = "派送中";
        }
        if (SafeValue.SafeString(res) == "COMPLETED")
        {
            str = "派送完成";
        }
        if (SafeValue.SafeString(res) == "CANCEL")
        {
            str = "取消";
        }
        return str;
    }
    protected void cmb_Type_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (cmb_Type.Value != null)
        {
            if (SafeValue.SafeString(cmb_Type.Value) != "All")
            {
                dsJobDet.FilterExpression = "CargoStatus='" + SafeValue.SafeString(cmb_Type.Value) + "'";
            }
            else {
                dsJobDet.FilterExpression = "1=1";
            }

        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string role = EzshipHelper.GetUseRole();
        string name = EzshipHelper.GetUserName();
        string sql = string.Format(@"select Id,BookingNo,ClientId,Remark1,ClientEmail,ConsigneeInfo,ConsigneeRemark,ConsigneeEmail,Email1,Tel1,Mobile1,Qty,Weight,Volume,CargoStatus from job_house ");
        string where = " JobType in ('I','E')";
        if (SafeValue.SafeString(cmb_Type.Value) != "All")
        {
            where =GetWhere(where," CargoStatus='"+SafeValue.SafeString(cmb_Type.Value)+"'");
        }
        if(txt_BkgRefNo.Text!=""){
            where = GetWhere(where, " BookingNo like'" + SafeValue.SafeString(txt_BkgRefNo.Text) + "%'");
        }
        if(txt_Tel.Text!=""){
            where = GetWhere(where, " (Tel1 like '%" + SafeValue.SafeString(txt_Tel.Text) + "%' or Tel2 like '%" + SafeValue.SafeString(txt_Tel.Text) + "%' or Mobile1 like '%" + SafeValue.SafeString(txt_Tel.Text) + "%' or Mobile2 like '%" + SafeValue.SafeString(txt_Tel.Text) + "%')");
        }
        if (role == "Client")
        {
            where=GetWhere(where,string.Format(" UserId='{0}'",name));
        }
        if (where.Length > 0)
        {
            sql += " where " + where;
        }
        dsJobDet.FilterExpression = where;
        
        //this.grd_Det.DataSource = ConnectSql.GetTab(sql);
        //this.grd_Det.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        string str = "";
        if (where.Length > 0)
        {
            str =where+ " and " + s;
        }
        else {
            str = s;
        }
        return str;
    }
    public void DownloadFile()
    {
        string fileRpath = MapPath("~/Modules/Freight/template/template.zip");
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
    public void DownloadFile1()
    {
        string fileRpath = MapPath("~/excel/packinglistsample.xlsx");
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
        //DownloadFile1();
    }
    protected void grd_Det_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("UpdateInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                updateStatus(grid, id, e, SafeValue.SafeInt(ar[1], -1));
            }
        }
    }
    private void updateStatus(ASPxGridView grid, string Id, ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        try
        {
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + Id + "'");
            C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
            if (jo.CargoStatus == "USE")
            {
                jo.CargoStatus = "ORDER";
            }
            if (jo.CargoStatus == "ORDER")
            {
                jo.CargoStatus = "IN";
            }
            else if (jo.CargoStatus == "IN")
            {
                jo.CargoStatus = "PICKED";
            }
            else if (jo.CargoStatus == "PICKED")
            {
                jo.CargoStatus = "OUT";
            }
            else if (jo.CargoStatus == "OUT")
            {
                jo.CargoStatus = "SHIPMENT";
            }
            else if (jo.CargoStatus == "SHIPMENT")
            {
                jo.CargoStatus = "DEPARTURE";
            }
            string role = EzshipHelper.GetUseRole();
            if (role == "Admin") {
                if (jo.CargoStatus == "DEPARTURE")
                {
                    jo.CargoStatus = "GR";
                }
                if (jo.CargoStatus == "GR")
                {
                    jo.CargoStatus = "DO";
                }
                else if (jo.CargoStatus == "DO")
                {
                    jo.CargoStatus = "SEND";
                }
                else if (jo.CargoStatus == "PICKED")
                {
                    jo.CargoStatus = "OUT";
                }
                else if (jo.CargoStatus == "SEND")
                {
                    jo.CargoStatus = "COMPLETED";
                }
            
            }
            C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(jo);
            L.Audit(jo.CargoStatus, "DJ", jo.Id.ToString(), "", "", "");
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }
    }
    protected void btn_Ops_Init(object sender, EventArgs e)
    {
        ASPxButton btn = sender as ASPxButton;
        GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;

        string oid = SafeValue.SafeString(this.grd_Det.GetRowValues(container.VisibleIndex, "Id"));
        string sql = "";
        string status = "";
        sql = string.Format(@"select CargoStatus from job_house where Id='{0}'", oid);
        status = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (status != "")
        {
            if (status == "USE")
            {
                btn.Text = "下单";
            }
            else if (status == "ORDER")
            {
                btn.Text = "入库";
            }
            else if (status == "IN")
            {
                btn.Text = "排库";
            }
            else if (status == "PICKED")
            {
                btn.Text = "出库";
            }
            else if (status == "OUT")
            {
                btn.Text = "装船";
            }
            else if (status == "SHIPMENT")
            {
                btn.Text = "出港";
            }

            string role = EzshipHelper.GetUseRole();
            if(role=="Admin"){
                if (status == "DEPARTURE")
                {
                    btn.Text = "入 库";
                }
                else if (status == "GR")
                {
                    btn.Text = "出 库";
                }
                else if (status == "DO")
                {
                    btn.Text = "派 送";
                }
                else if (status == "SEND")
                {
                    btn.Text = "派送完成";
                }
                else if (status == "COMPLETED")
                {
                    btn.Visible = false;
                }
            }
            else if (status == "DEPARTURE")
            {
                btn.Visible = false;
            }
            btn.ClientInstanceName = String.Format("btn_Ops{0}", container.VisibleIndex);
        }
        else {
            btn.Text = "下单";
            btn.ClientInstanceName = String.Format("btn_Ops{0}", container.VisibleIndex);
        }
    }
    protected void grd_Det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        L.Audit("D", "DJ", e.Values["Id"].ToString(), "", "", "");
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}