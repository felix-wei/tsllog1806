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

public partial class Modules_Tpt_ELCShifting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }

    }
    #region Trip
    public string change_StatusShortCode_ToCode(object par)
    {
        string res = SafeValue.SafeString(par);
        switch (res)
        {
            case "P":
                res = "Pending";
                break;
            case "S":
                res = "Start";
                break;
            case "C":
                res = "Completed";
                break;
            case "X":
                res = "Cancel";
                break;
        }
        return res;
    }
    protected void grid_Trip_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet2));
        }
    }

    protected void grid_Trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsTrip.FilterExpression = "JobNo='0'";
    }

    protected void grid_Trip_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Statuscode"] = "P";
        e.NewValues["FromDate"] = DateTime.Now;
        e.NewValues["ToDate"] = DateTime.Now;
        e.NewValues["SubletFlag"] = "N";
        e.NewValues["BayCode"] = "B1";
        e.NewValues["StageCode"] = "Pending";
        e.NewValues["StageStatus"] = "";
        e.NewValues["FromParkingLot"] = "";
        e.NewValues["FromCode"] = "";
        e.NewValues["ToCode"] = "";
        e.NewValues["Overtime"] = "Normal";
        e.NewValues["OverDistance"] = "Y";
        e.NewValues["JobNo"] = "0";
    }

    protected void grid_Trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
    }

    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] contIds = new object[grid.VisibleRowCount];
        object[] contNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            contIds[i] = grid.GetRowValues(i, "Id");
            contNs[i] = grid.GetRowValues(i, "ContainerNo");
            contTypes[i] = grid.GetRowValues(i, "ContainerType");
        }

        e.Properties["cpContId"] = contIds;
        e.Properties["cpContN"] = contNs;
        e.Properties["cpContType"] = contTypes;
    }

    private void check_Trip_Status(string id, string driverCode, string status)
    {
        if (driverCode.Trim().Length == 0)
        {
            return;
        }

        if (status == "S" || status == "P")
        {
            string sql = string.Format(@"select COUNT(*) from CTM_JobDet2 where DriverCode='{0}' and Statuscode='{2}' and Id<>'{1}'", driverCode, id, status);
            int result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
            if (result > 0)
            {
                throw new Exception("Status:'" + status + "' is existing for " + driverCode);
            }
        }
    }

    protected void grid_Trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "Update")
        {
            Trip_Update(sender, e);
        }
        else
        {
            string temp = e.Parameters;
            string[] ar = temp.Split('_');
            if (ar.Length == 2)
            {
                if (ar[0] == "Delete")
                {
                    Trip_Delete(sender, e, ar[1]);
                }
            }
        }
    }

    private void Trip_Delete(object sender, ASPxGridViewCustomDataCallbackEventArgs e, string tripId)
    {
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = HttpContext.Current.User.Identity.Name;
        elog.ActionLevel_isTRIP(SafeValue.SafeInt(tripId, 0));
        elog.Remark = "Trip delete";
        elog.log();
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        C2.Manager.ORManager.ExecuteDelete(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        //C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
        //C2.Manager.ORManager.PersistChanges(trip);

        //EGL_JobTrip_AfterEndTrip("", SafeValue.SafeString(trip.Det1Id, "0"));

        string sql = string.Format(@"delete from job_cost where TripNo='{0}'", tripId);
        C2.Manager.ORManager.ExecuteScalar(sql);


        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;
        e.Result = re;
    }
    private void Trip_Update(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxLabel lb_tripId = grd.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
        string tripId = SafeValue.SafeString(lb_tripId.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'"); ;
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        string Driver_old = "";

        bool isNew = false;
        if (trip == null)
        {
            isNew = true;
            trip = new C2.CtmJobDet2();
        }
        else
        {
            Driver_old = trip.DriverCode;
        }

        ASPxDropDownEdit dde_Trip_ContNo = grd.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxTextBox dde_Trip_ContId = grd.FindEditFormTemplateControl("dde_Trip_ContId") as ASPxTextBox;
        //ASPxButtonEdit btn_CfsCode = grd.FindEditFormTemplateControl("btn_CfsCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_BayCode = grd.FindEditFormTemplateControl("cbb_Trip_BayCode") as ASPxComboBox;
        //ASPxComboBox cbb_Carpark = grd.FindEditFormTemplateControl("cbb_Carpark") as ASPxComboBox;
        ASPxComboBox cbb_Trip_TripCode = grd.FindEditFormTemplateControl("cbb_Trip_TripCode") as ASPxComboBox;

        ASPxButtonEdit btn_DriverCode = grd.FindEditFormTemplateControl("btn_DriverCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_TowheadCode = grd.FindEditFormTemplateControl("btn_TowheadCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_ChessisCode = grd.FindEditFormTemplateControl("btn_ChessisCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_SubletFlag = grd.FindEditFormTemplateControl("cbb_Trip_SubletFlag") as ASPxComboBox;
        //ASPxTextBox txt_SubletHauliername = grd.FindEditFormTemplateControl("txt_SubletHauliername") as ASPxTextBox;
        //ASPxComboBox cbb_StageCode = grd.FindEditFormTemplateControl("cbb_StageCode") as ASPxComboBox;
        //ASPxComboBox cbb_StageStatus = grd.FindEditFormTemplateControl("cbb_StageStatus") as ASPxComboBox;
        ASPxComboBox cbb_Trip_StatusCode = grd.FindEditFormTemplateControl("cbb_Trip_StatusCode") as ASPxComboBox;
        ASPxComboBox cmb_DoubleMounting = grd.FindEditFormTemplateControl("cmb_DoubleMounting") as ASPxComboBox;
        ASPxDateEdit txt_FromDate = grd.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_fromTime = grd.FindEditFormTemplateControl("txt_Trip_fromTime") as ASPxTextBox;
        ASPxDateEdit date_Trip_toDate = grd.FindEditFormTemplateControl("date_Trip_toDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_toTime = grd.FindEditFormTemplateControl("txt_Trip_toTime") as ASPxTextBox;
        ASPxMemo txt_Trip_Remark = grd.FindEditFormTemplateControl("txt_Trip_Remark") as ASPxMemo;
        ASPxMemo txt_Trip_FromCode = grd.FindEditFormTemplateControl("txt_Trip_FromCode") as ASPxMemo;
        ASPxMemo txt_Trip_ToCode = grd.FindEditFormTemplateControl("txt_Trip_ToCode") as ASPxMemo;
        //ASPxSpinEdit spin_Price = grd.FindEditFormTemplateControl("spin_Price") as ASPxSpinEdit;
        ASPxComboBox cbb_zone = grd.FindEditFormTemplateControl("cbb_zone") as ASPxComboBox;
        //ASPxSpinEdit spin_Incentive1 = grd.FindEditFormTemplateControl("spin_Incentive1") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Incentive2 = grd.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Incentive3 = grd.FindEditFormTemplateControl("spin_Incentive3") as ASPxSpinEdit;
        //ASPxComboBox cbb_Incentive4 = grd.FindEditFormTemplateControl("cbb_Incentive4") as ASPxComboBox;

        //ASPxComboBox cbb_Overtime = grd.FindEditFormTemplateControl("cbb_Overtime") as ASPxComboBox;
        ASPxMemo txt_driver_remark = grd.FindEditFormTemplateControl("txt_driver_remark") as ASPxMemo;
        //ASPxComboBox cbb_OverDistance = grd.FindEditFormTemplateControl("cbb_OverDistance") as ASPxComboBox;

        ASPxTextBox fromPackingLot = grd.FindEditFormTemplateControl("txt_FromPL") as ASPxTextBox;
        ASPxTextBox toPackingLot = grd.FindEditFormTemplateControl("txt_ToPL") as ASPxTextBox;
        //check_Trip_Status("0", trip.DriverCode,trip.Statuscode);

        //ASPxSpinEdit spin_Charge1 = grd.FindEditFormTemplateControl("spin_Charge1") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge2 = grd.FindEditFormTemplateControl("spin_Charge2") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge3 = grd.FindEditFormTemplateControl("spin_Charge3") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge4 = grd.FindEditFormTemplateControl("spin_Charge4") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge5 = grd.FindEditFormTemplateControl("spin_Charge5") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge6 = grd.FindEditFormTemplateControl("spin_Charge6") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge7 = grd.FindEditFormTemplateControl("spin_Charge7") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge8 = grd.FindEditFormTemplateControl("spin_Charge8") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge9 = grd.FindEditFormTemplateControl("spin_Charge9") as ASPxSpinEdit;
        //ASPxSpinEdit spin_Charge10 = grd.FindEditFormTemplateControl("spin_Charge10") as ASPxSpinEdit;
        ASPxSpinEdit txt_trip_qty = grd.FindEditFormTemplateControl("txt_trip_qty") as ASPxSpinEdit;
        ASPxButtonEdit txt_Trip_PkgsType = grd.FindEditFormTemplateControl("txt_Trip_PkgsType") as ASPxButtonEdit;
        ASPxSpinEdit txt_trip_Wt = grd.FindEditFormTemplateControl("txt_trip_Wt") as ASPxSpinEdit;
        ASPxSpinEdit txt_trip_M3 = grd.FindEditFormTemplateControl("txt_trip_M3") as ASPxSpinEdit;

        if (dde_Trip_ContNo != null)
            trip.ContainerNo = SafeValue.SafeString(dde_Trip_ContNo.Value);
        if (dde_Trip_ContId != null)
            trip.Det1Id = SafeValue.SafeInt(dde_Trip_ContId.Text, 0);
        //trip.CfsCode = SafeValue.SafeString(btn_CfsCode.Value);
        //trip.BayCode = SafeValue.SafeString(cbb_Trip_BayCode.Value);
        if (btn_DriverCode != null)
            trip.DriverCode = SafeValue.SafeString(btn_DriverCode.Value);
        if (btn_TowheadCode != null)
            trip.TowheadCode = SafeValue.SafeString(btn_TowheadCode.Value);
        if (btn_ChessisCode != null)
            trip.ChessisCode = SafeValue.SafeString(btn_ChessisCode.Value);
        //trip.SubletFlag = SafeValue.SafeString(cbb_Trip_SubletFlag.Value);
        //trip.SubletHauliername = SafeValue.SafeString(txt_SubletHauliername.Text);
        if (cbb_Trip_TripCode != null)
            trip.TripCode = SafeValue.SafeString(cbb_Trip_TripCode.Value);
        if (cmb_DoubleMounting != null)
            trip.DoubleMounting = SafeValue.SafeString(cmb_DoubleMounting.Value, "No");
        //trip.StageCode = SafeValue.SafeString(cbb_StageCode.Value);
        //trip.Carpark = SafeValue.SafeString(cbb_Carpark.Value);
        //trip.StageStatus = SafeValue.SafeString(cbb_StageStatus.Value);
        if (cbb_Trip_StatusCode != null)
            trip.Statuscode = SafeValue.SafeString(cbb_Trip_StatusCode.Value);
        if (txt_FromDate != null)
            trip.FromDate = SafeValue.SafeDate(txt_FromDate.Date, new DateTime(1990, 1, 1));
        if (txt_Trip_fromTime != null)
            trip.FromTime = SafeValue.SafeString(txt_Trip_fromTime.Text);
        if (date_Trip_toDate != null)
            trip.ToDate = SafeValue.SafeDate(date_Trip_toDate.Date, new DateTime(1990, 1, 1));
        if (txt_Trip_toTime != null)
            trip.ToTime = SafeValue.SafeString(txt_Trip_toTime.Text);
        if (txt_Trip_Remark != null)
            trip.Remark = SafeValue.SafeString(txt_Trip_Remark.Text);
        if (txt_Trip_FromCode != null)
            trip.FromCode = SafeValue.SafeString(txt_Trip_FromCode.Text);
        if (txt_Trip_ToCode != null)
            trip.ToCode = SafeValue.SafeString(txt_Trip_ToCode.Text);
        //trip.Incentive1 = SafeValue.SafeDecimal(spin_Price.Text);
        if (cbb_zone != null)
            trip.ParkingZone = SafeValue.SafeString(cbb_zone.Value);
        //trip.Incentive1 = SafeValue.SafeDecimal(spin_Incentive1.Text);
        //trip.Incentive2 = SafeValue.SafeDecimal(spin_Incentive2.Text);
        //trip.Incentive3 = SafeValue.SafeDecimal(spin_Incentive3.Text);
        //trip.Incentive4 = SafeValue.SafeDecimal(cbb_Incentive4.Value);

        //trip.Overtime = SafeValue.SafeSqlString(cbb_Overtime.Value);
        //trip.OverDistance = SafeValue.SafeSqlString(cbb_OverDistance.Value);
        if (txt_driver_remark != null)
            trip.Remark1 = SafeValue.SafeString(txt_driver_remark.Text);
        if (fromPackingLot != null)
            trip.FromParkingLot = SafeValue.SafeString(fromPackingLot.Text);
        if (toPackingLot != null)
            trip.ToParkingLot = SafeValue.SafeString(toPackingLot.Text);

        //trip.Charge1 = SafeValue.SafeDecimal(spin_Charge1.Text);
        //trip.Charge2 = SafeValue.SafeDecimal(spin_Charge2.Text);
        //trip.Charge3 = SafeValue.SafeDecimal(spin_Charge3.Text);
        //trip.Charge4 = SafeValue.SafeDecimal(spin_Charge4.Text);
        //trip.Charge5 = SafeValue.SafeDecimal(spin_Charge5.Text);
        //trip.Charge6 = SafeValue.SafeDecimal(spin_Charge6.Text);
        //trip.Charge7 = SafeValue.SafeDecimal(spin_Charge7.Text);
        //trip.Charge8 = SafeValue.SafeDecimal(spin_Charge8.Text);
        //trip.Charge9 = SafeValue.SafeDecimal(spin_Charge9.Text);
        //trip.Charge10 = SafeValue.SafeDecimal(spin_Charge10.Text);


        if (txt_trip_qty != null)
            trip.Qty = SafeValue.SafeInt(txt_trip_qty.Text, 0);
        if (txt_Trip_PkgsType != null)
            trip.PackageType = SafeValue.SafeString(txt_Trip_PkgsType.Text);
        if (txt_trip_Wt != null)
            trip.Weight = SafeValue.SafeDecimal(txt_trip_Wt.Text);
        if (txt_trip_M3 != null)
            trip.Volume = SafeValue.SafeDecimal(txt_trip_M3.Text);


        if (isNew)
        {
            trip.JobNo = "0";
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(trip);
            Event_Log("", "TRIP", 1, SafeValue.SafeInt(trip.Id, 0), "");
        }
        else
        {
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);
            Event_Log("", "TRIP", 3, SafeValue.SafeInt(trip.Id, 0), "");

            //EGL_JobTrip_AfterEndTrip("", dde_Trip_ContId.Text);
        }
        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;

        if (!trip.DriverCode.Equals(Driver_old))
        {
            re += "," + Driver_old;
        }
        Session["NameTrip_" + trip.Id] = "Id=" + trip.Id + "";
        this.dsTrip.FilterExpression = Session["NameTrip_" + trip.Id].ToString();
        if (this.grid_Trip.GetRow(0) != null)
            this.grid_Trip.CancelEdit();
        e.Result = re;

    }
    private void Event_Log(string jobNo, string level, int c, int id, string status)
    {

        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        if (level == "JOB")
        {
            elog.ActionLevel_isJOB(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Job, c, status);
        }
        if (level == "QUOTATION")
        {
            elog.ActionLevel_isJOB(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Quotation, c, status);
        }
        if (level == "CONT")
        {
            elog.ActionLevel_isCONT(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Container, c, status);
        }
        if (level == "TRIP")
        {
            elog.ActionLevel_isTRIP(id);
            elog.setActionLevel(id, CtmJobEventLogRemark.Level.Trip, c, status);
        }
        elog.log();


    }
    #endregion
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = " JobNo='0'";
        if (cbb_Trip_TripCode.Value != null)
        {
            where += " and TripCode='" + SafeValue.SafeString(cbb_Trip_TripCode.Value) + "'";
        }
        if (btn_DriverCode.Text != null)
        {
            where += " and DriverCode='" + SafeValue.SafeString(btn_DriverCode.Text) + "'";
        }
        dsTrip.FilterExpression = where;
    }
}