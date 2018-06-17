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
    
    public partial class job_rate
    {
        public int Id { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> LineId { get; set; }
        public string LineType { get; set; }
        public string LineStatus { get; set; }
        public string JobNo { get; set; }
        public string JobType { get; set; }
        public string RefNo { get; set; }
        public string ClientId { get; set; }
        public string SubClientId { get; set; }
        public string VendorId { get; set; }
        public string BillScope { get; set; }
        public string BillType { get; set; }
        public string BillClass { get; set; }
        public string ContSize { get; set; }
        public string ContType { get; set; }
        public string SkuCode { get; set; }
        public string ChgCode { get; set; }
        public string ChgCodeDes { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> MinPrice { get; set; }
        public Nullable<decimal> MinQty { get; set; }
        public Nullable<decimal> MinAmt { get; set; }
        public string CurrencyId { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public string GstType { get; set; }
        public Nullable<decimal> DocAmt { get; set; }
        public Nullable<decimal> LocAmt { get; set; }
        public string Status1 { get; set; }
        public string Status2 { get; set; }
        public string Status3 { get; set; }
        public string Status4 { get; set; }
        public Nullable<System.DateTime> Date1 { get; set; }
        public Nullable<System.DateTime> Date2 { get; set; }
        public Nullable<System.DateTime> Date3 { get; set; }
        public Nullable<System.DateTime> Date4 { get; set; }
        public string LineRemark { get; set; }
        public Nullable<System.DateTime> DateEffective { get; set; }
        public Nullable<System.DateTime> DateExpiry { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string RowStatus { get; set; }
        public string RowCreateUser { get; set; }
        public Nullable<System.DateTime> RowCreateTime { get; set; }
        public string RowUpdateUser { get; set; }
        public Nullable<System.DateTime> RowUpdateTime { get; set; }
        public string SkuClass { get; set; }
        public string SkuUnit { get; set; }
        public string StorageType { get; set; }
        public string VehicleType { get; set; }
        public string LumsumInd { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> LineIndex { get; set; }
        public Nullable<int> DailyNo { get; set; }
        public Nullable<decimal> First30Days { get; set; }
        public Nullable<decimal> After30Days { get; set; }
    }
}
