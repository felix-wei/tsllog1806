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
    
    public partial class Mobile_Chat
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> item_date { get; set; }
        public string item_type { get; set; }
        public string item_status { get; set; }
        public string item_chat { get; set; }
        public string speaker { get; set; }
        public string listener { get; set; }
        public string msg { get; set; }
        public string note1 { get; set; }
        public string note2 { get; set; }
    }
}
