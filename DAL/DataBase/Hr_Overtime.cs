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
    
    public partial class Hr_Overtime
    {
        public int Id { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public Nullable<decimal> HoursRate { get; set; }
        public Nullable<int> Person { get; set; }
        public Nullable<decimal> Times { get; set; }
        public Nullable<decimal> TotalAmt { get; set; }
        public string TypeId { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    }
}