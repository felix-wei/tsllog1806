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
    
    public partial class AirQuoteDet1
    {
        public int SequenceId { get; set; }
        public string QuoteId { get; set; }
        public Nullable<int> QuoteLineNo { get; set; }
        public string PartyTo { get; set; }
        public string PartyType { get; set; }
        public string ImpExpInd { get; set; }
        public string FclLclInd { get; set; }
        public string ChgCode { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> MinAmt { get; set; }
        public string Rmk { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Amt { get; set; }
        public string GstType { get; set; }
        public Nullable<decimal> Gst { get; set; }
        public string GroupTitle { get; set; }
        public string ChgDes { get; set; }
        public Nullable<decimal> ExRate { get; set; }
    }
}
