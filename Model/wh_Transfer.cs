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
    
    public partial class wh_Transfer
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public string TransferNo { get; set; }
        public string RequestPerson { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public string Pic { get; set; }
        public string StatusCode { get; set; }
        public string ConfirmPerson { get; set; }
        public Nullable<System.DateTime> ConfirmDate { get; set; }
        public string PartyId { get; set; }
        public string PartyName { get; set; }
    }
}
