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
    
    public partial class Vehicle_Mileage
    {
        public int Id { get; set; }
        public string VehicleNo { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public Nullable<decimal> Value { get; set; }
        public string Note { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
    }
}
