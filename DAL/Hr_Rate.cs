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
    
    public partial class Hr_Rate
    {
        public int Id { get; set; }
        public string PayItem { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> EmployeeRate { get; set; }
        public Nullable<decimal> EmployerRate { get; set; }
        public Nullable<decimal> EmployeeRate55 { get; set; }
        public Nullable<decimal> EmployerRate55 { get; set; }
        public Nullable<decimal> EmployeeRate60 { get; set; }
        public Nullable<decimal> EmployerRate60 { get; set; }
        public Nullable<decimal> EmployeeRate65 { get; set; }
        public Nullable<decimal> EmployerRate65 { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public Nullable<int> Age { get; set; }
        public string RateType { get; set; }
        public Nullable<int> Age1 { get; set; }
    }
}
