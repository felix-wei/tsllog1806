//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTM_JobDet2
    {
        public int Id { get; set; }
        public string JobNo { get; set; }
        public string ContainerNo { get; set; }
        public string DriverCode { get; set; }
        public string TowheadCode { get; set; }
        public string ChessisCode { get; set; }
        public string Remark { get; set; }
        public string FromCode { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public string FromTime { get; set; }
        public string ToCode { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string ToTime { get; set; }
        public string CfsCode { get; set; }
        public string BayCode { get; set; }
        public string SubletFlag { get; set; }
        public string SubletHauliername { get; set; }
        public string Statuscode { get; set; }
        public string TripCode { get; set; }
        public Nullable<int> Det1Id { get; set; }
        public string StageCode { get; set; }
        public string StageStatus { get; set; }
        public string LoadCode { get; set; }
        public string Overtime { get; set; }
        public string OverDistance { get; set; }
        public string Carpark { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string FromParkingLot { get; set; }
        public string ToParkingLot { get; set; }
        public Nullable<decimal> OverTimePrice { get; set; }
        public Nullable<int> FromParkingLotType { get; set; }
        public Nullable<int> ToParkingLotType { get; set; }
        public string ParkingZone { get; set; }
        public string TrailerParkingLot { get; set; }
        public string TrailerAddress { get; set; }
        public string DoubleMounting { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public string BookingTime { get; set; }
        public Nullable<decimal> TotalHour { get; set; }
        public string BookingTime2 { get; set; }
        public string BookingRemark { get; set; }
        public Nullable<decimal> OtHour { get; set; }
        public string ByUser { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string BillingRemark { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string PackageType { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public string DeliveryRemark { get; set; }
        public string Satisfaction { get; set; }
        public string PodType { get; set; }
        public Nullable<int> epodTrip { get; set; }
        public string epodSignedBy { get; set; }
        public string Escort_Ind { get; set; }
        public string Escort_Remark { get; set; }
        public string SubCon_Code { get; set; }
        public string SubCon_Ind { get; set; }
        public string Self_Ind { get; set; }
        public string CheckType { get; set; }
        public string CheckRemark { get; set; }
        public string JobType { get; set; }
        public string RequestVehicleType { get; set; }
        public string RequestTrailerType { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string epodCB1 { get; set; }
        public string epodCB2 { get; set; }
        public string epodCB3 { get; set; }
        public string epodCB4 { get; set; }
        public string epodCB5 { get; set; }
        public string epodCB6 { get; set; }
        public string WarehouseStatus { get; set; }
        public string WarehouseRemark { get; set; }
        public Nullable<System.DateTime> WarehouseScheduleDate { get; set; }
        public Nullable<System.DateTime> WarehouseStartDate { get; set; }
        public Nullable<System.DateTime> WarehouseEndDate { get; set; }
        public string TripIndex { get; set; }
        public string PermitNo { get; set; }
        public string PermitBy { get; set; }
        public Nullable<System.DateTime> PermitDate { get; set; }
        public string PermitRemark { get; set; }
        public string IncoTerm { get; set; }
        public string PartyInvNo { get; set; }
        public Nullable<decimal> GstAmt { get; set; }
        public string PaymentStatus { get; set; }
        public Nullable<int> ManPowerNo { get; set; }
        public string ExcludeLunch { get; set; }
        public string ReturnType { get; set; }
        public Nullable<System.DateTime> ReturnLastDate { get; set; }
        public string CargoVerify { get; set; }
        public string DeliveryResult { get; set; }
        public string epodHardCopy { get; set; }
        public string direct_inf { get; set; }
        public string TripStatus { get; set; }
        public string ManualDo { get; set; }
        public string DriverCode2 { get; set; }
        public string ServiceType { get; set; }
        public string ClientRefNo { get; set; }
        public string DriverCode3 { get; set; }
        public Nullable<int> TripIndex1 { get; set; }
        public string DriverCode11 { get; set; }
        public string DriverCode12 { get; set; }
    }
}
