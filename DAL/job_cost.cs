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
    
    public partial class job_cost
    {
        public int Id { get; set; }
        public Nullable<int> LineId { get; set; }
        public string LineType { get; set; }
        public string LineStatus { get; set; }
        public string JobNo { get; set; }
        public string JobType { get; set; }
        public string RefNo { get; set; }
        public string ContNo { get; set; }
        public string ContType { get; set; }
        public string TripNo { get; set; }
        public string VendorId { get; set; }
        public string ChgCode { get; set; }
        public string ChgCodeDes { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Unit { get; set; }
        public string CurrencyId { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public Nullable<decimal> DocAmt { get; set; }
        public Nullable<decimal> LocAmt { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string RowStatus { get; set; }
        public string RowCreateUser { get; set; }
        public Nullable<System.DateTime> RowCreateTime { get; set; }
        public string RowUpdateUser { get; set; }
        public Nullable<System.DateTime> RowUpdateTime { get; set; }
        public string LineSource { get; set; }
        public string BillScope { get; set; }
        public string BillType { get; set; }
        public string BillClass { get; set; }
        public Nullable<decimal> Gst { get; set; }
        public Nullable<decimal> GstAmt { get; set; }
        public string GstType { get; set; }
        public Nullable<int> LineIndex { get; set; }
        public string SubJobNo { get; set; }
        public string ReceiptNo { get; set; }
        public string Pay_Ind { get; set; }
        public string ReceiptRemark { get; set; }
        public string VehicleNo { get; set; }
        public Nullable<decimal> First30Days { get; set; }
        public Nullable<decimal> After30Days { get; set; }
        public string RelaId { get; set; }
        public Nullable<System.DateTime> BillStartDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string EventType { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public string TripType { get; set; }
        public string DriverCode { get; set; }
        public string GroupBy { get; set; }
        public Nullable<int> JobId { get; set; }
        public string NotBuildCustomer { get; set; }
    }
}
