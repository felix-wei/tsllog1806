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
    
    public partial class X6
    {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string RowVersion { get; set; }
        public string RowType { get; set; }
        public string RowStatus { get; set; }
        public string RowCreateBy { get; set; }
        public Nullable<System.DateTime> RowCreateTime { get; set; }
        public string RowUpdateBy { get; set; }
        public Nullable<System.DateTime> RowUpdateTime { get; set; }
        public Nullable<int> LineIndex { get; set; }
        public string ItemCode { get; set; }
        public string ItemGroup { get; set; }
        public string ItemStatus { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public string ItemNote { get; set; }
        public Nullable<int> ItemQty { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public Nullable<System.DateTime> ActualDate { get; set; }
    }
}
