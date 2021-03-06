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
    
    public partial class AirImport
    {
        public int SequenceId { get; set; }
        public string RefNo { get; set; }
        public string ContainerNo { get; set; }
        public string JobNo { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string HblNo { get; set; }
        public string CustomerId { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public Nullable<int> Qty { get; set; }
        public string PackageType { get; set; }
        public string ExpressBl { get; set; }
        public string TruckingInd { get; set; }
        public string Shipper { get; set; }
        public string Consignee { get; set; }
        public string DoRealeaseTo { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string DoReadyInd { get; set; }
        public string ForwardingId { get; set; }
        public string FrCollectInd { get; set; }
        public Nullable<decimal> CollectAmount { get; set; }
        public string CollectCurrency { get; set; }
        public Nullable<decimal> CollectExRate { get; set; }
        public string TsInd { get; set; }
        public string TsSchNo { get; set; }
        public string TsBkgId { get; set; }
        public string TsBkgNo { get; set; }
        public string TsRefNo { get; set; }
        public string TsJobNo { get; set; }
        public string TsPod { get; set; }
        public string TsPortFinName { get; set; }
        public string TsVessel { get; set; }
        public string TsVoyage { get; set; }
        public string TsColoader { get; set; }
        public Nullable<System.DateTime> TsEtd { get; set; }
        public Nullable<System.DateTime> TsEta { get; set; }
        public string TsAgentId { get; set; }
        public string TsRemark { get; set; }
        public Nullable<decimal> TsAgtRate { get; set; }
        public Nullable<decimal> TsTotAgtRate { get; set; }
        public Nullable<decimal> TsImpRate { get; set; }
        public Nullable<decimal> TsTotImpRate { get; set; }
        public string PermitRmk { get; set; }
        public string SShipperRemark { get; set; }
        public string SAgentRemark { get; set; }
        public string SConsigneeRemark { get; set; }
        public string SNotifyPartyRemark { get; set; }
    }
}
