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
    
    public partial class JobMCST
    {
        public int Id { get; set; }
        public string RefNo { get; set; }
        public string McstNo { get; set; }
        public Nullable<System.DateTime> McstDate1 { get; set; }
        public Nullable<System.DateTime> McstDate2 { get; set; }
        public string States { get; set; }
        public string McstRemark1 { get; set; }
        public string McstRemark2 { get; set; }
        public Nullable<decimal> Amount1 { get; set; }
        public Nullable<decimal> Amount2 { get; set; }
        public string CondoTel { get; set; }
        public string McstRemark3 { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
    }
}
