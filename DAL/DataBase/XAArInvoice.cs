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
    
    public partial class XAArInvoice
    {
        public int SequenceId { get; set; }
        public Nullable<int> AcYear { get; set; }
        public Nullable<int> AcPeriod { get; set; }
        public string AcCode { get; set; }
        public string AcSource { get; set; }
        public string DocType { get; set; }
        public string DocNo { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public Nullable<System.DateTime> DocDueDate { get; set; }
        public string PartyTo { get; set; }
        public string MastRefNo { get; set; }
        public string JobRefNo { get; set; }
        public string MastType { get; set; }
        public string CurrencyId { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public string Term { get; set; }
        public string GdDes1 { get; set; }
        public string GdDes2 { get; set; }
        public string GdDes3 { get; set; }
        public string GdDes4 { get; set; }
        public string GdDes5 { get; set; }
        public Nullable<decimal> DocAmt { get; set; }
        public Nullable<decimal> LocAmt { get; set; }
        public Nullable<decimal> BalanceAmt { get; set; }
        public Nullable<decimal> TaxableAmt { get; set; }
        public Nullable<decimal> TaxAmt { get; set; }
        public Nullable<decimal> TaxableAmt1 { get; set; }
        public Nullable<decimal> TaxAmt1 { get; set; }
        public Nullable<decimal> TaxableAmt2 { get; set; }
        public Nullable<decimal> TaxAmt2 { get; set; }
        public Nullable<decimal> NonTaxableAmt { get; set; }
        public Nullable<decimal> NonTaxableAmt1 { get; set; }
        public Nullable<decimal> NonTaxableAmt2 { get; set; }
        public string ExportInd { get; set; }
        public string CancelInd { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string MastClass { get; set; }
        public string Pol { get; set; }
        public string Pod { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public string Pic { get; set; }
        public Nullable<System.DateTime> Eta { get; set; }
        public string RefNo { get; set; }
        public string BkgRefNo { get; set; }
        public string CustRef { get; set; }
        public string Description { get; set; }
        public string SpecialNote { get; set; }
        public string InvType { get; set; }
        public string Contact { get; set; }
        public string ReviseInd { get; set; }
    }
}
