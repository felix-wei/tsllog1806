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
    
    public partial class Job_Charge
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public string JobNo { get; set; }
        public string RefType { get; set; }
        public string Code { get; set; }
        public string ChgCodeDes { get; set; }
        public Nullable<decimal> SalePrice1 { get; set; }
        public Nullable<decimal> SalePrice2 { get; set; }
        public Nullable<decimal> CostPrice1 { get; set; }
        public Nullable<decimal> CostPrice2 { get; set; }
    }
}