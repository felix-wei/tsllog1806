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
    
    public partial class jnl_doc
    {
        public int Id { get; set; }
        public Nullable<int> AcYear { get; set; }
        public Nullable<int> AcPeriod { get; set; }
        public string DocType { get; set; }
        public string DocNo { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string DocTerm { get; set; }
        public string DocCurrency { get; set; }
        public Nullable<decimal> DocExRate { get; set; }
        public string PartyTo { get; set; }
        public string OtherPartyName { get; set; }
        public Nullable<decimal> DocAmt { get; set; }
        public Nullable<decimal> LocAmt { get; set; }
        public string AcCode { get; set; }
        public string AcSource { get; set; }
        public string ChqNo { get; set; }
        public Nullable<System.DateTime> ChqDate { get; set; }
        public string CloseInd { get; set; }
        public string ExportInd { get; set; }
        public string Term { get; set; }
        public string BankRec { get; set; }
        public string BankDate { get; set; }
        public string BankName { get; set; }
        public string MatchInd { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string CancelInd { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string BankRec1 { get; set; }
        public Nullable<System.DateTime> BankDate1 { get; set; }
        public string Remark { get; set; }
        public string DocType1 { get; set; }
        public string Pic { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public Nullable<System.DateTime> PostDateTime { get; set; }
        public string PostBy { get; set; }
        public string GenerateInd { get; set; }
        public Nullable<bool> IsReturn { get; set; }
        public string SlipNo { get; set; }
    }
}
