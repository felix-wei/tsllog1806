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
    
    public partial class SeaQuotationDet1
    {
        public int Id { get; set; }
        public string QuoteNo { get; set; }
        public string ChgCode { get; set; }
        public string ChgCodeDes { get; set; }
        public Nullable<decimal> SaleQty { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string Unit { get; set; }
        public string SaleCurrency { get; set; }
        public Nullable<decimal> SaleExRate { get; set; }
        public Nullable<decimal> SaleDocAmt { get; set; }
        public Nullable<decimal> SaleLocAmt { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> CostDocAmt { get; set; }
        public Nullable<decimal> CostLocAmt { get; set; }
        public string SplitType { get; set; }
        public string CostCurrency { get; set; }
        public Nullable<decimal> costExRate { get; set; }
        public Nullable<decimal> SaleGst { get; set; }
        public Nullable<decimal> CostGst { get; set; }
        public Nullable<decimal> CostQty { get; set; }
        public string Remark { get; set; }
    }
}
