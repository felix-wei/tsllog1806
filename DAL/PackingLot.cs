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
    
    public partial class PackingLot
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public Nullable<int> Size20 { get; set; }
        public Nullable<int> Size40 { get; set; }
        public string Note { get; set; }
        public string Note1 { get; set; }
    }
}
