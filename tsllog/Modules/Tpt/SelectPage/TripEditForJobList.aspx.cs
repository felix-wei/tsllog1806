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

public partial class Modules_Tpt_SelectPage_TripEditForJobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string tripId = SafeValue.SafeString(Request.QueryString["tripId"]);
        string tripIndex = SafeValue.SafeString(Request.QueryString["tripIndex"]);
        
        txt_JobNo.Text = jobNo;
        lbl_TripId.Text = tripId;
        if (!IsPostBack)
        {
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
            C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;

            if (trip != null)
            {
                Session["CTM_Trip_" + tripId] = "Id='" + tripId + "'";
                this.dsTrip.FilterExpression = Session["CTM_Trip_" + tripId].ToString();
                if (this.gv_tpt_trip.GetRow(0) != null)
                    this.gv_tpt_trip.StartEdit(0);
            }
        }

        if (Session["CTM_Trip_" + tripId] != null)
        {
            this.dsTrip.FilterExpression = Session["CTM_Trip_" + tripId].ToString();
            if (this.gv_tpt_trip.GetRow(0) != null)
                this.gv_tpt_trip.StartEdit(0);
        }
    }
    public string show_driver_signature(object orderNo, object tripId)
    {
        string Signature_Driver = "";
        string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Driver", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Driver = dt.Rows[0]["FilePath"].ToString();
            if (Signature_Driver.Length == 0)
                Signature_Driver = "";
        }

        return Signature_Driver;
    }
    public string show_consignee_signature(object orderNo, object tripId)
    {
        string Signature_Consignee = "";
        string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Consignee", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Consignee = dt.Rows[0]["FilePath"].ToString();
            if (Signature_Consignee.Length == 0)
                Signature_Consignee = "";
        }
        return Signature_Consignee;
    }
    #region Transoprt Trip
    protected void gv_tpt_trip_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet2));
        }
    }
    protected void gv_tpt_trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string action = e.Parameters;

        if (action.Equals("Save"))
        {
            Trip_Update(sender, e, "");
        }
    }
    private void Trip_Update(object sender, ASPxGridViewCustomDataCallbackEventArgs e, string jobType)
    {
        try
        {
            string tripId = SafeValue.SafeString(lbl_TripId.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
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

            //ASPxDropDownEdit dde_Trip_ContNo = gv_tpt_trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
            ASPxButtonEdit btn_ChessisCode = gv_tpt_trip.FindEditFormTemplateControl("btn_ChessisCode") as ASPxButtonEdit;
            ASPxComboBox cbb_zone = gv_tpt_trip.FindEditFormTemplateControl("cbb_zone") as ASPxComboBox;
            ASPxTextBox dde_Trip_ContId = gv_tpt_trip.FindEditFormTemplateControl("dde_Trip_ContId") as ASPxTextBox;
            //ASPxButtonEdit btn_CfsCode = gv_tpt_trip.FindEditFormTemplateControl("btn_CfsCode") as ASPxButtonEdit;
            //ASPxComboBox cbb_Trip_BayCode = gv_tpt_trip.FindEditFormTemplateControl("cbb_Trip_BayCode") as ASPxComboBox;
            //ASPxComboBox cbb_Carpark = gv_tpt_trip.FindEditFormTemplateControl("cbb_Carpark") as ASPxComboBox;
            ASPxComboBox cbb_Trip_TripCode = gv_tpt_trip.FindEditFormTemplateControl("cbb_Trip_TripCode") as ASPxComboBox;
            ASPxButtonEdit btn_ContNo = gv_tpt_trip.FindEditFormTemplateControl("btn_ContNo") as ASPxButtonEdit;
            ASPxButtonEdit btn_DriverCode = gv_tpt_trip.FindEditFormTemplateControl("btn_DriverCode") as ASPxButtonEdit;
            ASPxButtonEdit btn_Attended = gv_tpt_trip.FindEditFormTemplateControl("btn_Attended") as ASPxButtonEdit;
            ASPxButtonEdit btn_TowheadCode = gv_tpt_trip.FindEditFormTemplateControl("btn_TowheadCode") as ASPxButtonEdit;
            //ASPxComboBox cbb_Trip_SubletFlag = gv_tpt_trip.FindEditFormTemplateControl("cbb_Trip_SubletFlag") as ASPxComboBox;
            //ASPxTextBox txt_SubletHauliername = gv_tpt_trip.FindEditFormTemplateControl("txt_SubletHauliername") as ASPxTextBox;
            //ASPxComboBox cbb_StageCode = gv_tpt_trip.FindEditFormTemplateControl("cbb_StageCode") as ASPxComboBox;
            //ASPxComboBox cbb_StageStatus = gv_tpt_trip.FindEditFormTemplateControl("cbb_StageStatus") as ASPxComboBox;
            ASPxComboBox cbb_Trip_StatusCode = gv_tpt_trip.FindEditFormTemplateControl("cbb_Trip_StatusCode") as ASPxComboBox;
            ASPxComboBox cmb_DoubleMounting = gv_tpt_trip.FindEditFormTemplateControl("cmb_DoubleMounting") as ASPxComboBox;
            ASPxDateEdit txt_FromDate = gv_tpt_trip.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
            ASPxTextBox txt_Trip_fromTime = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_fromTime") as ASPxTextBox;
            ASPxDateEdit date_Trip_toDate = gv_tpt_trip.FindEditFormTemplateControl("date_Trip_toDate") as ASPxDateEdit;
            ASPxTextBox txt_Trip_toTime = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_toTime") as ASPxTextBox;
            ASPxMemo txt_Trip_Remark = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_Remark") as ASPxMemo;
            ASPxMemo txt_Trip_FromCode = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_FromCode") as ASPxMemo;
            ASPxMemo txt_Trip_ToCode = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_ToCode") as ASPxMemo;
            //ASPxSpinEdit spin_Incentive2 = gv_tpt_trip.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;

            ASPxMemo txt_driver_remark = gv_tpt_trip.FindEditFormTemplateControl("txt_driver_remark") as ASPxMemo;


            ASPxDateEdit date_BookingDate = gv_tpt_trip.FindEditFormTemplateControl("date_BookingDate") as ASPxDateEdit;
            ASPxTextBox txt_BookingTime = gv_tpt_trip.FindEditFormTemplateControl("txt_BookingTime") as ASPxTextBox;
            ASPxTextBox txt_BookingTime2 = gv_tpt_trip.FindEditFormTemplateControl("txt_BookingTime2") as ASPxTextBox;
            ASPxMemo txt_BookingRemark = gv_tpt_trip.FindEditFormTemplateControl("txt_BookingRemark") as ASPxMemo;
            ASPxMemo txt_Trip_BillingRemark = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_BillingRemark") as ASPxMemo;
            ASPxMemo txt_delivery_remark = gv_tpt_trip.FindEditFormTemplateControl("txt_delivery_remark") as ASPxMemo;
            if (txt_delivery_remark != null)
                trip.DeliveryRemark = txt_delivery_remark.Text;
            ASPxMemo txt_billing_remark = gv_tpt_trip.FindEditFormTemplateControl("txt_billing_remark") as ASPxMemo;
            if (txt_billing_remark != null)
                trip.BillingRemark = txt_billing_remark.Text;
            ASPxMemo txt_Satifaction_Indator = gv_tpt_trip.FindEditFormTemplateControl("txt_Satifaction_Indator") as ASPxMemo;
            if (txt_Satifaction_Indator != null)
                trip.Satisfaction = txt_Satifaction_Indator.Text;
            ASPxComboBox cmb_Escort_Ind = gv_tpt_trip.FindEditFormTemplateControl("cmb_Escort_Ind") as ASPxComboBox;
            ASPxMemo txt_Trip_Escort_Remark = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_Escort_Remark") as ASPxMemo;
            if (cmb_Escort_Ind != null)
                trip.Escort_Ind = SafeValue.SafeString(cmb_Escort_Ind.Value);
            if (txt_Trip_Escort_Remark != null)
                trip.Escort_Remark = SafeValue.SafeString(txt_Trip_Escort_Remark.Text);
            ASPxSpinEdit txt_TotalHour = gv_tpt_trip.FindEditFormTemplateControl("txt_TotalHour") as ASPxSpinEdit;
            ASPxSpinEdit txt_OtHour = gv_tpt_trip.FindEditFormTemplateControl("txt_OtHour") as ASPxSpinEdit;
            ASPxTextBox txt_ByUser = gv_tpt_trip.FindEditFormTemplateControl("txt_ByUser") as ASPxTextBox;
            ASPxTextBox txt_TrailerType = gv_tpt_trip.FindEditFormTemplateControl("txt_TrailerType") as ASPxTextBox;
            ASPxTextBox txt_VehicleType = gv_tpt_trip.FindEditFormTemplateControl("txt_VehicleType") as ASPxTextBox;
            ASPxComboBox cbb_VehicleType = gv_tpt_trip.FindEditFormTemplateControl("cbb_VehicleType") as ASPxComboBox;
            ASPxMemo txt_DeliveryRemark = gv_tpt_trip.FindEditFormTemplateControl("txt_DeliveryRemark") as ASPxMemo;
            if (txt_TrailerType != null)
                trip.RequestTrailerType = txt_TrailerType.Text;
            if (txt_VehicleType != null)
                trip.RequestVehicleType = txt_VehicleType.Text;
            if (cbb_VehicleType != null)
                trip.RequestVehicleType = cbb_VehicleType.Text;
            if (txt_DeliveryRemark != null)
                trip.DeliveryRemark = txt_DeliveryRemark.Text;
            ASPxButtonEdit btn_AgentId = gv_tpt_trip.FindEditFormTemplateControl("btn_AgentId") as ASPxButtonEdit;
            ASPxTextBox txt_AgentName = gv_tpt_trip.FindEditFormTemplateControl("txt_AgentName") as ASPxTextBox;
            if (btn_AgentId != null)
                trip.AgentId = btn_AgentId.Text;
            if (txt_AgentName != null)
                trip.AgentName = txt_AgentName.Text;
            if (btn_ContNo != null)
                trip.ContainerNo = SafeValue.SafeString(btn_ContNo.Text);

            ASPxButtonEdit btn_Trailer = gv_tpt_trip.FindEditFormTemplateControl("btn_Trailer") as ASPxButtonEdit;
            if (btn_Trailer != null)
                trip.ChessisCode = SafeValue.SafeString(btn_Trailer.Text);
            //if (btn_ChessisCode != null)
            //    trip.ChessisCode = SafeValue.SafeString(btn_ChessisCode.Value);
            //if (cbb_zone != null)
            //    trip.ParkingZone = SafeValue.SafeString(cbb_zone.Value);
            if (dde_Trip_ContId != null)
                trip.Det1Id = SafeValue.SafeInt(dde_Trip_ContId.Text, 0);
            //trip.CfsCode = SafeValue.SafeString(btn_CfsCode.Value);
            //trip.BayCode = SafeValue.SafeString(cbb_Trip_BayCode.Value);
            if (btn_DriverCode != null)
            {
                if (btn_DriverCode.Text != "")
                    trip.DriverCode = SafeValue.SafeString(btn_DriverCode.Text);
                else
                    trip.DriverCode = "";
            }
            if (btn_Attended != null)
            {
                if (btn_Attended.Text != "")
                    trip.DriverCode2 = SafeValue.SafeString(btn_Attended.Text);
                else
                    trip.DriverCode2 = "";
            }
            if (btn_TowheadCode != null)
                trip.TowheadCode = SafeValue.SafeString(btn_TowheadCode.Value);
            //trip.SubletFlag = SafeValue.SafeString(cbb_Trip_SubletFlag.Value);
            //trip.SubletHauliername = SafeValue.SafeString(txt_SubletHauliername.Text);
            if (cbb_Trip_TripCode != null)
                trip.TripCode = SafeValue.SafeString(cbb_Trip_TripCode.Value);
            if (cmb_DoubleMounting != null)
                trip.DoubleMounting = SafeValue.SafeString(cmb_DoubleMounting.Value, "No");
            //trip.StageCode = SafeValue.SafeString(cbb_StageCode.Value);
            //trip.Carpark = SafeValue.SafeString(cbb_Carpark.Value);
            //trip.StageStatus = SafeValue.SafeString(cbb_StageStatus.Value);

            if (date_BookingDate != null)
                trip.BookingDate = SafeValue.SafeDate(date_BookingDate.Date, new DateTime(1900, 1, 1));
            if (txt_BookingTime != null)
                trip.BookingTime = SafeValue.SafeString(txt_BookingTime.Text);
            if (txt_BookingTime2 != null)
                trip.BookingTime2 = SafeValue.SafeString(txt_BookingTime2.Text);
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

            //trip.Overtime = SafeValue.SafeSqlString(cbb_Overtime.Value);
            //trip.OverDistance = SafeValue.SafeSqlString(cbb_OverDistance.Value);
            if (txt_driver_remark != null)
                trip.Remark1 = SafeValue.SafeString(txt_driver_remark.Text);
            //if (fromPackingLot != null)
            //   trip.FromParkingLot = SafeValue.SafeString(fromPackingLot.Text);
            //if (toPackingLot != null)
            //   trip.ToParkingLot = SafeValue.SafeString(toPackingLot.Text);
            if (txt_BookingRemark != null)
                trip.BookingRemark = txt_BookingRemark.Text;
            if (txt_Trip_BillingRemark != null)
                trip.BillingRemark = txt_Trip_BillingRemark.Text;
            if (txt_OtHour != null)
                trip.OtHour = SafeValue.SafeDecimal(txt_OtHour.Text);
            if (txt_TotalHour != null)
                trip.TotalHour = SafeValue.SafeDecimal(txt_TotalHour.Text);
            if (txt_ByUser != null)
                trip.ByUser = SafeValue.SafeString(txt_ByUser.Text);

            ASPxDateEdit date_WarehouseScheduleDate = gv_tpt_trip.FindEditFormTemplateControl("date_WarehouseScheduleDate") as ASPxDateEdit;
            if (date_WarehouseScheduleDate != null)
                trip.WarehouseScheduleDate = SafeValue.SafeDate(date_WarehouseScheduleDate.Date, new DateTime(1990, 1, 1));
            ASPxDateEdit date_WarehouseStartDate = gv_tpt_trip.FindEditFormTemplateControl("date_WarehouseStartDate") as ASPxDateEdit;
            if (date_WarehouseStartDate != null)
                trip.WarehouseStartDate = SafeValue.SafeDate(date_WarehouseStartDate.Date, new DateTime(1990, 1, 1));
            ASPxDateEdit date_WarehouseEndDate = gv_tpt_trip.FindEditFormTemplateControl("date_WarehouseEndDate") as ASPxDateEdit;
            if (date_WarehouseEndDate != null)
                trip.WarehouseEndDate = SafeValue.SafeDate(date_WarehouseEndDate.Date, new DateTime(1990, 1, 1));
            ASPxMemo memo_WarehouseRemark = gv_tpt_trip.FindEditFormTemplateControl("memo_WarehouseRemark") as ASPxMemo;
            if (memo_WarehouseRemark != null)
                trip.WarehouseRemark = memo_WarehouseRemark.Text;
            ASPxComboBox cbb_WarehouseStatus = gv_tpt_trip.FindEditFormTemplateControl("cbb_WarehouseStatus") as ASPxComboBox;
            if (cbb_WarehouseStatus != null)
                trip.WarehouseStatus = SafeValue.SafeString(cbb_WarehouseStatus.Value);

            ASPxCheckBox ckb_epodCB1 = gv_tpt_trip.FindEditFormTemplateControl("ckb_epodCB1") as ASPxCheckBox;
            if (ckb_epodCB1 != null)
            {
                if (ckb_epodCB1.Checked)
                    trip.EpodCB1 = "Yes";
                else
                    trip.EpodCB1 = "No";
            }
            ASPxCheckBox ckb_epodCB2 = gv_tpt_trip.FindEditFormTemplateControl("ckb_epodCB2") as ASPxCheckBox;
            if (ckb_epodCB2 != null)
            {
                if (ckb_epodCB2.Checked)
                    trip.EpodCB2 = "Yes";
                else
                    trip.EpodCB2 = "No";
            }
            ASPxCheckBox ckb_epodCB3 = gv_tpt_trip.FindEditFormTemplateControl("ckb_epodCB3") as ASPxCheckBox;
            if (ckb_epodCB3 != null)
            {
                if (ckb_epodCB3.Checked)
                    trip.EpodCB3 = "Yes";
                else
                    trip.EpodCB3 = "No";
            }
            ASPxCheckBox ckb_epodCB4 = gv_tpt_trip.FindEditFormTemplateControl("ckb_epodCB4") as ASPxCheckBox;
            if (ckb_epodCB4 != null)
            {
                if (ckb_epodCB4.Checked)
                    trip.EpodCB4 = "Yes";
                else
                    trip.EpodCB4 = "No";
            }
            ASPxCheckBox ckb_epodCB5 = gv_tpt_trip.FindEditFormTemplateControl("ckb_epodCB5") as ASPxCheckBox;
            if (ckb_epodCB5 != null)
            {
                if (ckb_epodCB5.Checked)
                    trip.EpodCB5 = "Yes";
                else
                    trip.EpodCB5 = "No";
            }
            ASPxCheckBox ckb_epodCB6 = gv_tpt_trip.FindEditFormTemplateControl("ckb_epodCB6") as ASPxCheckBox;
            if (ckb_epodCB6 != null)
            {
                if (ckb_epodCB6.Checked)
                    trip.EpodCB6 = "Yes";
                else
                    trip.EpodCB6 = "No";
            }
            ASPxComboBox cbb_Self_Ind = gv_tpt_trip.FindEditFormTemplateControl("cbb_Self_Ind") as ASPxComboBox;
            if (cbb_Self_Ind != null)
                trip.Self_Ind = SafeValue.SafeString(cbb_Self_Ind.Value, "No");
            ASPxButtonEdit btn_SubCon_Code = gv_tpt_trip.FindEditFormTemplateControl("btn_SubCon_Code") as ASPxButtonEdit;
            if (btn_SubCon_Code != null)
                trip.SubCon_Code = btn_SubCon_Code.Text;
            ASPxComboBox cbb_SubCon_Code = gv_tpt_trip.FindEditFormTemplateControl("cbb_SubCon_Code") as ASPxComboBox;
            if (cbb_SubCon_Code != null)
                trip.SubCon_Ind = SafeValue.SafeString(cbb_SubCon_Code.Value);



            ASPxComboBox cbb_DirectInf = gv_tpt_trip.FindEditFormTemplateControl("cbb_DirectInf") as ASPxComboBox;
            if (cbb_DirectInf != null)
            {
                trip.DirectInf = cbb_DirectInf.Text;
            }




            //========= incentive
            ASPxSpinEdit spin_Charge_ERP = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge_ERP") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge_ParkingFee = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge_ParkingFee") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge9 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge9") as ASPxSpinEdit;
            ASPxSpinEdit spin_Incentive1 = gv_tpt_trip.FindEditFormTemplateControl("spin_Incentive1") as ASPxSpinEdit;
            ASPxSpinEdit spin_Incentive2 = gv_tpt_trip.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;
            ASPxComboBox cbb_Incentive4 = gv_tpt_trip.FindEditFormTemplateControl("cbb_Incentive4") as ASPxComboBox;

            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = HttpContext.Current.User.Identity.Name;

            trip.UpdateUser = HttpContext.Current.User.Identity.Name;
            trip.UpdateTime = DateTime.Now;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);
            //EGL_JobTrip_AfterEndTrip("", dde_Trip_ContId.Text);
            elog.Remark = "Update Trip";

            elog.ActionLevel_isTRIP(trip.Id);
            elog.log();


            Dictionary<string, decimal> d = new Dictionary<string, decimal>();
            d.Add("Trip", SafeValue.SafeDecimal(spin_Incentive1.Text));
            d.Add("OverTime", SafeValue.SafeDecimal(spin_Incentive2.Text));
            d.Add("PSA", SafeValue.SafeDecimal(cbb_Incentive4.Text));

            C2.CtmJobDet2.Incentive_Save(trip.Id, d);

            d = new Dictionary<string, decimal>();
            d.Add("ERP", SafeValue.SafeDecimal(spin_Charge_ERP.Text));
            d.Add("ParkingFee", SafeValue.SafeDecimal(spin_Charge_ParkingFee.Text));
            d.Add("OTHER", SafeValue.SafeDecimal(spin_Charge9.Text));
            C2.CtmJobDet2.Claims_Save(trip.Id, d);

            Session["CTM_Trip_" + tripId] = "Id='" + tripId + "'";
            this.dsTrip.FilterExpression = Session["CTM_Trip_" + tripId].ToString();
            if (this.gv_tpt_trip.GetRow(0) != null)
                this.gv_tpt_trip.StartEdit(0);
        }
        catch { }
        e.Result = "Success";

    }
    #endregion
}