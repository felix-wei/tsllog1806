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
    
    public partial class CTM_DriverLog
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Driver { get; set; }
        public string Towhead { get; set; }
        public string IsActive { get; set; }
        public string TeamNo { get; set; }
    }
}
