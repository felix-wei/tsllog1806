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
    
    public partial class CTM_JobCharge
    {
        public int Id { get; set; }
        public string JobNo { get; set; }
        public string JobType { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }
    }
}
