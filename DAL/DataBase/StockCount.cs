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
    
    public partial class StockCount
    {
        public int Id { get; set; }
        public string StockNo { get; set; }
        public Nullable<System.DateTime> StockDate { get; set; }
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public string PartyAdd { get; set; }
        public string WareHouseId { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
    }
}
