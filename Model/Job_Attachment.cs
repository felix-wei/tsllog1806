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
    
    public partial class Job_Attachment
    {
        public int Id { get; set; }
        public string JobType { get; set; }
        public string RefNo { get; set; }
        public string JobNo { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileNote { get; set; }
        public Nullable<System.DateTime> FileDate { get; set; }
        public string FileSize { get; set; }
        public string FileStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
    }
}
