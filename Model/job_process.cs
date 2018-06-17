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
    
    public partial class job_process
    {
        public int Id { get; set; }
        public string JobNo { get; set; }
        public Nullable<int> HouseId { get; set; }
        public Nullable<System.DateTime> DateEntry { get; set; }
        public Nullable<System.DateTime> DatePlan { get; set; }
        public Nullable<System.DateTime> DateInspect { get; set; }
        public Nullable<System.DateTime> DateProcess { get; set; }
        public string LotNo { get; set; }
        public string LocationCode { get; set; }
        public string ProcessType { get; set; }
        public string ProcessStatus { get; set; }
        public string Specs1 { get; set; }
        public string Specs2 { get; set; }
        public string Specs3 { get; set; }
        public string Specs4 { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> ProcessQty1 { get; set; }
        public Nullable<decimal> ProcessQty2 { get; set; }
        public Nullable<decimal> ProcessQty3 { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public Nullable<System.DateTime> RowCreateTime { get; set; }
        public string RowCreateUser { get; set; }
        public Nullable<System.DateTime> RowUpdateTime { get; set; }
        public string RowUpdateUser { get; set; }
        public string PipeNo { get; set; }
        public string HeatNo { get; set; }
        public string InventoryId { get; set; }
    }
}
