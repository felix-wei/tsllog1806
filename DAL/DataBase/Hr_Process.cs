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
    
    public partial class Hr_Process
    {
        public int Id { get; set; }
        public string DocType { get; set; }
        public string DocNo { get; set; }
        public string Status { get; set; }
        public string ByWho { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string Remark { get; set; }
    }
}
