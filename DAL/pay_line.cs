//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class pay_line
    {
        public int Id { get; set; }
        public string RepNo { get; set; }
        public string RepType { get; set; }
        public Nullable<int> RepLineNo { get; set; }
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
        public Nullable<int> RepId { get; set; }
        public Nullable<int> DocId { get; set; }
    }
}
