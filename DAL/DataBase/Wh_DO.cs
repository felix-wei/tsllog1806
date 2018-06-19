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
    
    public partial class Wh_DO
    {
        public int Id { get; set; }
        public string DoNo { get; set; }
        public string DoType { get; set; }
        public Nullable<System.DateTime> DoDate { get; set; }
        public string PartyId { get; set; }
        public string WareHouseId { get; set; }
        public string Remark { get; set; }
        public string ContractNo { get; set; }
        public Nullable<int> FirstQty { get; set; }
        public Nullable<int> BalQty { get; set; }
        public string StatusCode { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public string PartyName { get; set; }
        public string PartyAdd { get; set; }
        public string PartyPostalcode { get; set; }
        public string PartyCity { get; set; }
        public string PartyCountry { get; set; }
        public Nullable<System.DateTime> partyRefDate { get; set; }
        public string CustomsSealNo { get; set; }
        public string TptMode { get; set; }
        public Nullable<System.DateTime> ExpectedDate { get; set; }
        public Nullable<bool> PalletizedInd { get; set; }
        public Nullable<System.DateTime> PoDate { get; set; }
        public string PartyInvNo { get; set; }
        public Nullable<System.DateTime> PartyInvDate { get; set; }
        public string IncoTerms { get; set; }
        public string PermitNo { get; set; }
        public Nullable<System.DateTime> PermitApprovalDate { get; set; }
        public string TptName { get; set; }
        public Nullable<bool> PopulateInd { get; set; }
        public string GroupId { get; set; }
        public Nullable<System.DateTime> GateInDate { get; set; }
        public Nullable<System.DateTime> GateOutDate { get; set; }
        public string IssueNo { get; set; }
        public string JobNo { get; set; }
        public string Quotation { get; set; }
        public string EquipNo { get; set; }
        public string Personnel { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string CollectFrom { get; set; }
        public string DeliveryTo { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public Nullable<System.DateTime> Eta { get; set; }
        public Nullable<System.DateTime> Etd { get; set; }
        public Nullable<System.DateTime> EtaDest { get; set; }
        public string Pol { get; set; }
        public string Pod { get; set; }
        public string Obl { get; set; }
        public string Hbl { get; set; }
        public string Vehicle { get; set; }
        public string Coo { get; set; }
        public string Carrier { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentAdd { get; set; }
        public string AgentZip { get; set; }
        public string AgentCity { get; set; }
        public string AgentCountry { get; set; }
        public string AgentTel { get; set; }
        public string AgentContact { get; set; }
        public string NotifyId { get; set; }
        public string NotifyName { get; set; }
        public string NotifyAdd { get; set; }
        public string NotifyZip { get; set; }
        public string NotifyCity { get; set; }
        public string NotifyCountry { get; set; }
        public string NotifyTel { get; set; }
        public string NotifyContact { get; set; }
        public string ConsigneeId { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAdd { get; set; }
        public string ConsigneeZip { get; set; }
        public string ConsigneeCity { get; set; }
        public string ConsigneeCountry { get; set; }
        public string ConsigneeTel { get; set; }
        public string ConsigneeContact { get; set; }
        public string Remark3 { get; set; }
        public string Remark4 { get; set; }
        public string PoNo { get; set; }
        public string Priority { get; set; }
        public string XDockReference { get; set; }
        public string ReceiptNo { get; set; }
        public string Route { get; set; }
        public Nullable<System.DateTime> CustomerDate { get; set; }
        public string CustomerReference { get; set; }
        public string ExternalReference { get; set; }
        public Nullable<System.DateTime> ExternalDate { get; set; }
        public Nullable<bool> ReceiptInd { get; set; }
        public Nullable<bool> XDockInd { get; set; }
        public string DoStatus { get; set; }
        public string RelaId { get; set; }
        public string ModelType { get; set; }
        public string PermitBy { get; set; }
        public string OtherPermit { get; set; }
        public string ContainerYard { get; set; }
        public string Contractor { get; set; }
        public string DriverName { get; set; }
        public string DriverIC { get; set; }
        public string DriverTel { get; set; }
        public string Transport { get; set; }
        public Nullable<System.DateTime> PermitExpiryDate { get; set; }
        public string TransportName { get; set; }
        public Nullable<decimal> TransportFree { get; set; }
        public Nullable<decimal> TransportFee { get; set; }
        public string ContainerNo { get; set; }
        public string UseTransport { get; set; }
        public string TransportStatus { get; set; }
        public Nullable<System.DateTime> TransportStart { get; set; }
        public Nullable<System.DateTime> TransportEnd { get; set; }
        public string UseFreight { get; set; }
        public string FreightStatus { get; set; }
        public string VehicleNo { get; set; }
        public string Remarks { get; set; }
        public string TptJobNo { get; set; }
        public string IsBonded { get; set; }
        public string LotCode { get; set; }
        public string LicenseNo { get; set; }
        public Nullable<System.DateTime> LicenseExpiry { get; set; }
        public string LicenseRemark { get; set; }
        public Nullable<System.DateTime> OperationStart { get; set; }
    }
}