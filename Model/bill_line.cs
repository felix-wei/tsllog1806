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
    
    public partial class bill_line
    {
        public int Id { get; set; }
        public Nullable<int> DocId { get; set; }
        public string DocNo { get; set; }
        public string DocType { get; set; }
        public Nullable<int> DocLineNo { get; set; }
        public string CostingId { get; set; }
        public string AcCode { get; set; }
        public string AcSource { get; set; }
        public string ChgCode { get; set; }
        public string ChgDes1 { get; set; }
        public string ChgDes2 { get; set; }
        public string ChgDes3 { get; set; }
        public string ChgDes4 { get; set; }
        public string GstType { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Unit { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public Nullable<decimal> Gst { get; set; }
        public Nullable<decimal> GstAmt { get; set; }
        public Nullable<decimal> DocAmt { get; set; }
        public Nullable<decimal> LocAmt { get; set; }
        public Nullable<decimal> LineLocAmt { get; set; }
        public string MastRefNo { get; set; }
        public string JobRefNo { get; set; }
        public string MastType { get; set; }
        public string SplitType { get; set; }
        public Nullable<decimal> OtherAmt { get; set; }
    }
}
