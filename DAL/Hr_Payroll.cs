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
    
    public partial class Hr_Payroll
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Person { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Amt { get; set; }
        public string Remark { get; set; }
        public string Term { get; set; }
        public string Pic { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public string StatusCode { get; set; }
        public Nullable<decimal> Cpf1 { get; set; }
        public Nullable<decimal> Cpf1Amt { get; set; }
        public Nullable<decimal> Cpf2 { get; set; }
        public Nullable<decimal> Cpf2Amt { get; set; }
        public Nullable<decimal> Total1 { get; set; }
        public Nullable<decimal> Total2 { get; set; }
        public Nullable<decimal> Cpf0 { get; set; }
        public string AutoInd { get; set; }
    }
}
