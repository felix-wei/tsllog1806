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
    
    public partial class JobCost
    {
        public int SequenceId { get; set; }
        public string RefNo { get; set; }
        public Nullable<System.DateTime> RefDate { get; set; }
        public string JobNo { get; set; }
        public string JobType { get; set; }
        public string VendorId { get; set; }
        public string ChgCode { get; set; }
        public string ChgCodeDes { get; set; }
        public string Remark { get; set; }
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
        public Nullable<decimal> CostQty { get; set; }
        public Nullable<decimal> SaleGst { get; set; }
        public Nullable<decimal> CostGst { get; set; }
        public string PayInd { get; set; }
        public string VerifryInd { get; set; }
        public string DocNo { get; set; }
        public string Salesman { get; set; }
    }
}
