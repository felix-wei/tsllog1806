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
    
    public partial class wh_PODet
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public string PoNo { get; set; }
        public Nullable<int> LinePNo { get; set; }
        public string Product { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string GstType { get; set; }
        public Nullable<decimal> Gst { get; set; }
        public Nullable<decimal> DocAmt { get; set; }
        public Nullable<decimal> GstAmt { get; set; }
        public Nullable<decimal> LocAmt { get; set; }
        public Nullable<decimal> LineLocAmt { get; set; }
        public Nullable<int> BalQty { get; set; }
        public Nullable<int> Qty1 { get; set; }
        public Nullable<int> Qty2 { get; set; }
        public string BatchNo { get; set; }
        public string StatusCode { get; set; }
        public string Remark { get; set; }
    }
}
