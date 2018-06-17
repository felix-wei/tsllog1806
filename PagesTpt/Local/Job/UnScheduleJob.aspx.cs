using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Local_UnScheduleJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_Search_Click(null, null);
        }
        OnLoad("");
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {

        string sql = string.Format(@"select Id,JobNo,JobType,BkgDate,BkgTime,TptDate,TptTime,JobProgress,TptType,Driver,VehicleNo,TripCode,Wt,M3,Qty,PickFrm1,DeliveryTo1 from tpt_job where JobProgress!='Canceled' and isnull(BkgDate,'1753-1-1')<'2010-1-1' and StatusCode<>'CNL'");
        DataTable dt = ConnectSql.GetTab(sql);
        grid_Transport.DataSource = dt;
        grid_Transport.DataBind();
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "OK")
        {
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    string id = list[i].Id;
                    string driver = list[i].diver;
                    string vehicle = list[i].vehicle;
                    DateTime bkgDate = SafeValue.SafeDate(list[i].bkgDate, new DateTime(1753, 1, 1));
                    string bkgTime = list[i].bkgTime;
                    string trip = list[i].trip;
                    string sql = string.Format(@"update tpt_job set Driver='{1}',VehicleNo='{2}',BkgDate='{3}',BkgTime='4',TripCode='{5}' where Id='{0}'", id, driver, vehicle, bkgDate, bkgTime,trip);
                    ConnectSql.ExecuteSql(sql);
                }
                catch { }
            }
            e.Result = "Success";
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string diver = "";
        public string vehicle = "";
        public DateTime bkgDate = new DateTime(1753, 1, 1);
        public string bkgTime = "";
        public string Id = "";
        public string trip = "";

        public Record(string _Id,string _DriverCode, string _TowheadCode,DateTime _bkgDate,string _bkgTime,string _trip)
        {
            Id = _Id;
            diver = _DriverCode;
            vehicle = _TowheadCode;
            bkgDate = _bkgDate;
            bkgTime = _bkgTime;
            trip = _trip;
        }

    }
    private void OnLoad(string t)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns[0], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox Id = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "lb_Id") as ASPxTextBox;
            ASPxButtonEdit driver = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "btn_DriverCode") as ASPxButtonEdit;
            ASPxButtonEdit towhead = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["VehicleNo"], "btn_vehicle") as ASPxButtonEdit;
            ASPxDateEdit bkgDate = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["BkgDate"], "txt_BkgDate") as ASPxDateEdit;
            ASPxTextBox bkgTime = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["BkgTime"], "txt_BkgTime") as ASPxTextBox;
            ASPxComboBox trip = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["TripCode"], "cbb_Trip_TripCode") as ASPxComboBox;

            if (Id != null &&isPay != null&&isPay.Checked)
            {
                list.Add(new Record(Id.Text, driver.Text, towhead.Text,bkgDate.Date,bkgTime.Text,SafeValue.SafeString(trip.Value)));
            }
            else if (Id == null)
                break;
        }
    }

    protected void grid_Transport_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

        ASPxButtonEdit driver = this.grid_Transport.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "btn_DriverCode") as ASPxButtonEdit;
        if (driver != null)
        {
            driver.ClientInstanceName = driver.ClientInstanceName + e.VisibleIndex;
            ASPxButtonEdit towhead = this.grid_Transport.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["VehicleNo"], "btn_vehicle") as ASPxButtonEdit;
            towhead.ClientInstanceName = towhead.ClientInstanceName + e.VisibleIndex;
            driver.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupCTM_Driver({0},null,{1},null);", driver.ClientInstanceName, towhead.ClientInstanceName) + "}";
            towhead.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupCTM_MasterData({0},null,'Towhead');", towhead.ClientInstanceName) + "}";
        }
    }
}