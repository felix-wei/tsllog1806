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
    
    public partial class SeaExportMkg
    {
        public int SequenceId { get; set; }
        public string RefNo { get; set; }
        public string JobNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string Marking { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public Nullable<int> Qty { get; set; }
        public string PackageType { get; set; }
        public string Remark { get; set; }
        public string ContainerType { get; set; }
        public string MkgType { get; set; }
        public Nullable<decimal> GrossWt { get; set; }
        public Nullable<decimal> NetWt { get; set; }
        public Nullable<System.DateTime> PodEta { get; set; }
        public Nullable<System.DateTime> PodClearDate { get; set; }
        public Nullable<System.DateTime> PodReturnDate { get; set; }
        public string PodRemark { get; set; }
        public Nullable<System.DateTime> PolEta { get; set; }
        public Nullable<System.DateTime> PolClearDate { get; set; }
        public Nullable<System.DateTime> PolReturnDate { get; set; }
        public string PolRemark { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public string StatusCode { get; set; }
    }
}