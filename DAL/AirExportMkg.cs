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
    
    public partial class AirExportMkg
    {
        public int SequenceId { get; set; }
        public string RefNo { get; set; }
        public string JobNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Marking { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public Nullable<int> Qty { get; set; }
        public string PackageType { get; set; }
        public string Remark { get; set; }
    }
}
