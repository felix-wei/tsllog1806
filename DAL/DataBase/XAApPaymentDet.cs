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
    
    public partial class XAApPaymentDet
    {
        public int SequenceId { get; set; }
        public string PayNo { get; set; }
        public string PayType { get; set; }
        public Nullable<int> PayLineNo { get; set; }
        public string AcCode { get; set; }
        public string AcSource { get; set; }
        public string DocNo { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public string DocType { get; set; }
        public Nullable<decimal> DocAmt { get; set; }
        public Nullable<decimal> LocAmt { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string PartyTo { get; set; }
        public string PartyType { get; set; }
        public Nullable<int> PayId { get; set; }
        public Nullable<int> DocId { get; set; }
        public string MastRefNo { get; set; }
        public string JobRefNo { get; set; }
        public string MastType { get; set; }
        public string Dim1 { get; set; }
        public string Dim2 { get; set; }
        public string Dim3 { get; set; }
        public string Dim4 { get; set; }
    }
}
