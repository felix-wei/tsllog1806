//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTM_Job
    {
        public int Id { get; set; }
        public string JobNo { get; set; }
        public Nullable<System.DateTime> JobDate { get; set; }
        public string PartyId { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public string Pol { get; set; }
        public string Pod { get; set; }
        public Nullable<System.DateTime> EtaDate { get; set; }
        public string EtaTime { get; set; }
        public Nullable<System.DateTime> EtdDate { get; set; }
        public string EtdTime { get; set; }
        public string CarrierId { get; set; }
        public string CarrierBlNo { get; set; }
        public string CarrierBkgNo { get; set; }
        public string PickupFrom { get; set; }
        public string DeliveryTo { get; set; }
        public string Remark { get; set; }
        public string SpecialInstruction { get; set; }
        public string StatusCode { get; set; }
        public string Terminalcode { get; set; }
        public string JobType { get; set; }
        public string ClientId { get; set; }
        public string ClientRefNo { get; set; }
        public string HaulierId { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public Nullable<System.DateTime> CodDate { get; set; }
        public string CodTime { get; set; }
        public string WarehouseAddress { get; set; }
        public string PermitNo { get; set; }
        public string YardRef { get; set; }
        public string PortnetRef { get; set; }
        public string OperatorCode { get; set; }
        public string IsTrucking { get; set; }
        public string IsWarehouse { get; set; }
        public string IsFreight { get; set; }
        public string IsLocal { get; set; }
        public string IsAdhoc { get; set; }
        public string IsOthers { get; set; }
        public Nullable<System.DateTime> PermitExpiry { get; set; }
        public string EmailAddress { get; set; }
        public string Contractor { get; set; }
        public string SubClientId { get; set; }
        public string WareHouseCode { get; set; }
        public string ClientContact { get; set; }
        public string JobStatus { get; set; }
        public string QuoteNo { get; set; }
        public string QuoteStatus { get; set; }
        public string QuoteRemark { get; set; }
        public Nullable<System.DateTime> QuoteDate { get; set; }
        public string WhPermitNo { get; set; }
        public Nullable<System.DateTime> PermitDate { get; set; }
        public string PermitBy { get; set; }
        public string PartyInvNo { get; set; }
        public string IncoTerms { get; set; }
        public Nullable<decimal> GstAmt { get; set; }
        public string PaymentRemark { get; set; }
        public string PaymentStatus { get; set; }
        public string JobDes { get; set; }
        public string TerminalRemark { get; set; }
        public string AdditionalRemark { get; set; }
        public string LumSumRemark { get; set; }
        public string InternalRemark { get; set; }
        public string BillingType { get; set; }
        public string RefNo { get; set; }
        public string BillType { get; set; }
        public string TallyDone { get; set; }
        public string IssuedBy { get; set; }
        public string AgentId { get; set; }
        public string OrderType { get; set; }
        public string DepotCode { get; set; }
        public string MasterJobNo { get; set; }
        public string Escort_Ind { get; set; }
        public string Escort_Remark { get; set; }
        public string Fumigation_Ind { get; set; }
        public string FumigationRemark { get; set; }
        public string FumigationStatus { get; set; }
        public string Stamping_Ind { get; set; }
        public string StampingRemark { get; set; }
        public string StampingStatus { get; set; }
        public string BillingRefNo { get; set; }
        public string ShowInvoice_Ind { get; set; }
        public Nullable<System.DateTime> ReturnLastDate { get; set; }
        public string DepartmentId { get; set; }
        public string ReleaseToHaulierRemark { get; set; }
        public string JobNo2 { get; set; }
    }
}
