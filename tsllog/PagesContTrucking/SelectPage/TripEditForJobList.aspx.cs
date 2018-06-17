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

public partial class PagesContTrucking_SelectPage_TripEditForJobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string tripId = SafeValue.SafeString(Request.QueryString["tripId"]);
        string contId = SafeValue.SafeString(Request.QueryString["contId"]);
        string sql = string.Format(@"select TripStatus from ctm_jobdet2 where Id={0}", tripId);
        string tripStatus = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql), "");
        if (tripStatus == "LOACKED"|| tripStatus == "PAID")
        {
            btn_job_save.Enabled = false;
        }
        lbl_JobNo.Text = jobNo;
        lbl_TripId.Text = tripId;
        lb_ContId.Text = contId;
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
            if (jobNo.Length > 0)
            {
                DataTable dt = ConnectSql.GetTab(string.Format(@"select job.Id,PickupFrom,DeliveryTo,YardRef,job.SpecialInstruction,job.Remark,job.PermitNo,
det1.SealNo,det1.ContainerNo,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,det1.PortnetStatus,det1.UrgentInd
from ctm_jobdet1 as det1
left outer join ctm_job as job on job.JobNo=det1.JobNo where det1.Id={0}", contId));
                if (dt.Rows.Count > 0)
                {
                    txt_sealno.Text = SafeValue.SafeString(dt.Rows[0]["SealNo"]);
                    txt_ContNo.Text = SafeValue.SafeString(dt.Rows[0]["ContainerNo"]);

                    txt_from.Text = SafeValue.SafeString(dt.Rows[0]["PickupFrom"]);
                    txt_to.Text = SafeValue.SafeString(dt.Rows[0]["DeliveryTo"]);
                    txt_depot.Text = SafeValue.SafeString(dt.Rows[0]["YardRef"]);
                    txt_instruction.Text = SafeValue.SafeString(dt.Rows[0]["SpecialInstruction"]);
                    txt_Remark.Text = SafeValue.SafeString(dt.Rows[0]["Remark"]);
                    txt_PermitNo.Text = SafeValue.SafeString(dt.Rows[0]["PermitNo"]);

                    cbb_EmailInd.Value = SafeValue.SafeString(dt.Rows[0]["EmailInd"]);
                    cbb_F5Ind.Value = SafeValue.SafeString(dt.Rows[0]["F5Ind"]);
                    cbb_PortnetStatus.Value = SafeValue.SafeString(dt.Rows[0]["PortnetStatus"]);
                    cbb_UrgentInd.Value = SafeValue.SafeString(dt.Rows[0]["UrgentInd"]);
                    cbb_warehouse_status.Value = SafeValue.SafeString(dt.Rows[0]["WarehouseStatus"]);

                }
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
    private void Job_Save(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string sql = string.Format(@"update ctm_jobdet1 set SealNo=@SealNo,ContainerNo=@ContainerNo,
EmailInd=@EmailInd,F5Ind=@F5Ind,PortnetStatus=@PortnetStatus,UrgentInd=@UrgentInd,WarehouseStatus=@WarehouseStatus
where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", lb_ContId.Text, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@SealNo", txt_sealno.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", txt_ContNo.Text, SqlDbType.NVarChar, 100));

        list.Add(new ConnectSql_mb.cmdParameters("@EmailInd", SafeValue.SafeString(cbb_EmailInd.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@F5Ind", SafeValue.SafeString(cbb_F5Ind.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@PortnetStatus", SafeValue.SafeString(cbb_PortnetStatus.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@UrgentInd", SafeValue.SafeString(cbb_UrgentInd.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@WarehouseStatus", SafeValue.SafeString(cbb_warehouse_status.Value), SqlDbType.NVarChar, 100));

        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status)
        {
            sql = string.Format(@"update ctm_job set PickupFrom=@PickupFrom,DeliveryTo=@DeliveryTo,YardRef=@YardRef,SpecialInstruction=@SpecialInstruction,Remark=@Remark,PermitNo=@PermitNo where JobNo=@JobNo");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", lbl_JobNo.Text, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@PickupFrom", txt_from.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@DeliveryTo", txt_to.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@YardRef", txt_depot.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@SpecialInstruction", txt_instruction.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@Remark", txt_Remark.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@PermitNo", txt_PermitNo.Text, SqlDbType.NVarChar, 100));
            res = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (res.status)
            {
                sql = string.Format(@"update ctm_jobdet2 set ContainerNo=@ContainerNo where Det1Id=@Det1Id");
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", lb_ContId.Text, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", txt_ContNo.Text, SqlDbType.NVarChar, 100));
                res = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (res.status)
                {
                    update_cargo(lb_ContId.Text, txt_ContNo.Text);
                    e.Result = "Success";
                }
            }
        }
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
            Job_Save(sender, e);
        }
        if (action.Equals("Complete"))
        {
            string tripId = SafeValue.SafeString(lbl_TripId.Text, "");

            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
            C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
            trip.Statuscode = "C";
            trip.FromDate = trip.BookingDate;
            trip.FromTime = trip.BookingTime;
            trip.ToDate = trip.BookingDate;
            trip.ToTime = trip.BookingTime;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);

            C2.CtmJobDet2.tripStatusChanged(trip.Id);

            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = HttpContext.Current.User.Identity.Name;
            elog.Remark = "Update Trip";
            elog.ActionLevel_isTRIP(trip.Id);
            elog.log();
            e.Result = "Success";
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
            ASPxButtonEdit btn_TowheadCode = gv_tpt_trip.FindEditFormTemplateControl("btn_TowheadCode") as ASPxButtonEdit;
           
            ASPxTextBox fromPackingLot = gv_tpt_trip.FindEditFormTemplateControl("txt_FromPL") as ASPxTextBox;
            ASPxTextBox toPackingLot = gv_tpt_trip.FindEditFormTemplateControl("txt_ToPL") as ASPxTextBox;
            ASPxComboBox cbb_Trip_StatusCode = gv_tpt_trip.FindEditFormTemplateControl("cbb_Trip_StatusCode") as ASPxComboBox;
            ASPxComboBox cmb_DoubleMounting = gv_tpt_trip.FindEditFormTemplateControl("cmb_DoubleMounting") as ASPxComboBox;
            ASPxDateEdit txt_FromDate = gv_tpt_trip.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
            ASPxTextBox txt_Trip_fromTime = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_fromTime") as ASPxTextBox;
            ASPxDateEdit date_Trip_toDate = gv_tpt_trip.FindEditFormTemplateControl("date_Trip_toDate") as ASPxDateEdit;
            ASPxTextBox txt_Trip_toTime = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_toTime") as ASPxTextBox;
            ASPxMemo txt_Trip_Remark = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_Remark") as ASPxMemo;
            ASPxMemo txt_Trip_FromCode = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_FromCode") as ASPxMemo;
            ASPxMemo txt_Trip_ToCode = gv_tpt_trip.FindEditFormTemplateControl("txt_Trip_ToCode") as ASPxMemo;

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
            if (btn_ChessisCode != null)
                trip.ChessisCode = SafeValue.SafeString(btn_ChessisCode.Value);
            if (cbb_zone != null)
                trip.ParkingZone = SafeValue.SafeString(cbb_zone.Value);
            if (dde_Trip_ContId != null)
                trip.Det1Id = SafeValue.SafeInt(dde_Trip_ContId.Text, 0);
            if (btn_DriverCode != null)
            {
                if (btn_DriverCode.Text != "")
                    trip.DriverCode = SafeValue.SafeString(btn_DriverCode.Text);
                else
                    trip.DriverCode = "";
            }
            if (btn_TowheadCode != null)
                trip.TowheadCode = SafeValue.SafeString(btn_TowheadCode.Value);
            if (cbb_Trip_TripCode != null)
                trip.TripCode = SafeValue.SafeString(cbb_Trip_TripCode.Value);
            if (cmb_DoubleMounting != null)
                trip.DoubleMounting = SafeValue.SafeString(cmb_DoubleMounting.Value, "No");
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
            if (fromPackingLot != null)
                trip.FromParkingLot = fromPackingLot.Text;
            if (toPackingLot != null)
                trip.ToParkingLot = toPackingLot.Text;
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

            ASPxSpinEdit spin_Charge1 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge1") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge2 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge2") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge3 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge3") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge4 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge4") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge5 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge5") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge6 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge6") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge7 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge7") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge8 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge8") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge10 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge10") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge_ERP = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge_ERP") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge_ParkingFee = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge_ParkingFee") as ASPxSpinEdit;
            ASPxSpinEdit spin_Charge9 = gv_tpt_trip.FindEditFormTemplateControl("spin_Charge9") as ASPxSpinEdit;
            Dictionary<string, decimal> d = new Dictionary<string, decimal>();
            if (spin_Charge_ERP != null)
                d.Add("ERP", SafeValue.SafeDecimal(spin_Charge_ERP.Text));
            if (spin_Charge_ParkingFee != null)
                d.Add("ParkingFee", SafeValue.SafeDecimal(spin_Charge_ParkingFee.Text));
            if (spin_Charge1 != null)
                //d.Add("EXPENSE", SafeValue.SafeDecimal(spin_Charge1.Text));
                d.Add("DHC", SafeValue.SafeDecimal(spin_Charge1.Text));
            if (spin_Charge2 != null)
                d.Add("WEIGHING", SafeValue.SafeDecimal(spin_Charge2.Text));
            if (spin_Charge3 != null)
                d.Add("WASHING", SafeValue.SafeDecimal(spin_Charge3.Text));
            if (spin_Charge4 != null)
                d.Add("REPAIR", SafeValue.SafeDecimal(spin_Charge4.Text));
            if (spin_Charge5 != null)
                d.Add("DETENTION", SafeValue.SafeDecimal(spin_Charge5.Text));
            if (spin_Charge6 != null)
                d.Add("DEMURRAGE", SafeValue.SafeDecimal(spin_Charge6.Text));
            if (spin_Charge7 != null)
                d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(spin_Charge7.Text));
            if (spin_Charge8 != null)
                d.Add("C_SHIPMENT", SafeValue.SafeDecimal(spin_Charge8.Text));
            if (spin_Charge9 != null)
                d.Add("OTHER", SafeValue.SafeDecimal(spin_Charge9.Text));
            if (spin_Charge10 != null)
                d.Add("EMF", SafeValue.SafeDecimal(spin_Charge10.Text));
            C2.CtmJobDet2.Claims_Save(trip.Id, d);
			
		ASPxSpinEdit spin_Incentive1 = gv_tpt_trip.FindEditFormTemplateControl("spin_Incentive1") as ASPxSpinEdit;
        ASPxSpinEdit spin_Incentive2 = gv_tpt_trip.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;
        ASPxSpinEdit spin_Incentive3 = gv_tpt_trip.FindEditFormTemplateControl("spin_Incentive3") as ASPxSpinEdit;
        ASPxComboBox cbb_Incentive4 = gv_tpt_trip.FindEditFormTemplateControl("cbb_Incentive4") as ASPxComboBox;
        ASPxSpinEdit spin_Incentive4 = gv_tpt_trip.FindEditFormTemplateControl("spin_Incentive4") as ASPxSpinEdit;
			
			 d = new Dictionary<string, decimal>();
        if (spin_Incentive1 != null)
            d.Add("Trip", SafeValue.SafeDecimal(spin_Incentive1.Text));
        if (spin_Incentive2 != null)
            d.Add("OverTime", SafeValue.SafeDecimal(spin_Incentive2.Text));
        if (spin_Incentive3 != null)
            d.Add("Standby", SafeValue.SafeDecimal(spin_Incentive3.Text));
        if (cbb_Incentive4 != null)
            d.Add("PSA", SafeValue.SafeDecimal(cbb_Incentive4.Text));
        if (spin_Incentive4 != null)
            d.Add("ALLOWANCE", SafeValue.SafeDecimal(spin_Incentive4.Text));
        C2.CtmJobDet2.Incentive_Save(trip.Id, d);
			

            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = HttpContext.Current.User.Identity.Name;

            trip.UpdateUser = HttpContext.Current.User.Identity.Name;
            trip.UpdateTime = DateTime.Now;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);

            C2.CtmJobDet2.tripStatusChanged(trip.Id);


            //EGL_JobTrip_AfterEndTrip("", dde_Trip_ContId.Text);
            elog.Remark = "Update Trip";

            elog.ActionLevel_isTRIP(trip.Id);
            elog.log();

            Session["CTM_Trip_" + tripId] = "Id='" + tripId + "'";
            this.dsTrip.FilterExpression = Session["CTM_Trip_" + tripId].ToString();
            if (this.gv_tpt_trip.GetRow(0) != null)
                this.gv_tpt_trip.StartEdit(0);
        }
        catch { }
        e.Result = "Success";

    }
    private void update_ContStatus_trip(int contId, string type, string statucCode)
    {
        string JobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string isWarehouse = SafeValue.SafeString(Request.QueryString["isWarehouse"]);
        string sql = "";
        string status = "";
        if (isWarehouse == "Yes")
        {
            if (type == "IMP")
                status = "WHS-LD";
            if (type == "EXP")
                status = "WHS-MT";
            if (type == "RET")
                status = "Return";
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        if (isWarehouse == "No")
        {
            if (type == "IMP")
                status = "Customer-LD";
            if (type == "EXP")
                status = "Customer-MT";
            if (type == "RET")
                status = "Return";
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isCONT(contId);
        elog.setActionLevel(contId, CtmJobEventLogRemark.Level.Container, 4, status);
        elog.log();
    }
    private void update_cargo(string contId, string contNo)
    {
        string sql = string.Format(@"update job_house set ContNo=@ContNo where ContId=@ContId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", contNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@ContId", SafeValue.SafeInt(contId, 0), SqlDbType.Int));
        ConnectSql_mb.ExecuteNonQuery(sql, list);
    }
    #endregion
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
}