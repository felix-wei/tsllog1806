using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;

public partial class WareHouse_Job_Schedule : System.Web.UI.Page
{
    protected void Page_Init()
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            btn_Sch_Click(null,null);
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim();
        string where = string.Format(@"(StatusCode='Pending' or StatusCode='Rescheduled') and (PartyId like '{0}%' or PartyName like '{0}%' or DoctorId like'{0}%')", name);
        this.dsWhSchedule.FilterExpression = where;
    }

    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhSchedule));
        }
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["DoDateTime"] = SafeValue.SafeDate(e.NewValues["DoDateTime"], DateTime.Now);
       // e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "Pending");
        //e.NewValues["SalesId"] = userId;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["DoDateTime"] = SafeValue.SafeDate(e.NewValues["DoDateTime"], DateTime.Now);
        //e.NewValues["SalesId"] = userId;
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
        string Id = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        //string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["DoDateTime"] = DateTime.Now;
        e.NewValues["StatusCode"] = "Pending";
        //e.NewValues["Product"] = "";
        //e.NewValues["SalesId"] = userId;
    }
    protected void btn_Create_Command(object sender, CommandEventArgs e)
    {
        int id = SafeValue.SafeInt(e.CommandArgument, 0);
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhSchedule), "Id='" + id + "'");
        WhSchedule s = C2.Manager.ORManager.GetObject(query) as WhSchedule;
        if(s!=null){
        //    //string value = "Product:" + s.Product + "  LotNo:" + s.LotNo + " Set:" + s.Location + " " + "\\n Qty:"
        //    //    + s.Qty + " Price:" + s.Price + " TotalAmt:" + SafeValue.ChinaRound(s.Qty * s.Price, 0);
        //    //string script = string.Format("<script type='text/javascript' >function Vali(){if(confirm({0})){grid.GetValuesOnCustomCallback(OK,{1});}}</script>", value, SafeValue.SafeString(id));
        //    //Response.Clear();
        //    //Response.Write(script);


          string soNo=  CreateSO(SafeValue.SafeString(id));
          Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script type=\"text/javascript\">parent.navTab.openTab(\"" + soNo + "\",\"/Modules/WareHouse/Job/PoEdit.aspx?no=" + soNo + "\"" + ",{title:\"" + soNo + "\", fresh:false, external:true});</script>");


        }
    }
    protected void btn_Create_Click(object sender, EventArgs e)
    {

    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        string value =SafeValue.SafeString(e.Result);
        if(s=="Save"){
            CreateSO(value);
        }
    }

    public string CreateSO(string id)
    {

        string userId = HttpContext.Current.User.Identity.Name;
        string doNo = "";
        doNo = C2Setup.GetNextNo("", "SaleOrders", DateTime.Now);
        string sql = string.Format(@"insert into Wh_Trans (DoNo,DoDate,PartyId,PartyName,Pic,PartyAdd,Currency,DoType,DoStatus,ExRate,SalesId,CreateBy,CreateDateTime,UpdateBy,UpdateDateTime
,AgentId,AgentName,AgentAdd,AgentContact,NotifyId,NotifyName)
select top 1 '{1}',GETDATE(),wh.PartyId,PartyName,PartyContact,PartyAdd,'SGD','SO','Draft',1,SalesId,'{2}',GETDATE(),'{2}',GETDATE()
,DoctorId,doc.Name,doc.Address,doc.Contact,Patient,pat.Name
from Wh_Schedule as wh
left outer join Ref_PersonInfo as doc on wh.DoctorId=doc.PartyId and doc.Type='Doctor'
left outer join Ref_PersonInfo as pat on wh.Patient=pat.ICNo and pat.Type='Patient'
where wh.Id='{0}'", id, doNo, userId);

        SafeValue.SafeInt(ConnectSql.ExecuteSql(sql),0);
        C2Setup.SetNextNo("", "SaleOrders", doNo, DateTime.Now);

        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhSchedule), "Id='" + id + "'");
        WhSchedule s = C2.Manager.ORManager.GetObject(query) as WhSchedule;
        s.DoNo = doNo;
        Manager.ORManager.StartTracking(s, Wilson.ORMapper.InitialState.Updated);
        Manager.ORManager.PersistChanges(s);

        return doNo;
    }
}