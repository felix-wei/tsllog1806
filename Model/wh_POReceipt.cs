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
    
    public partial class wh_POReceipt
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
        public string ReceiptNo { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public string ReceiptType { get; set; }
        public string PartyId { get; set; }
        public string PartyRefNo { get; set; }
        public string WarehouseId { get; set; }
        public string SalesmanId { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public string Remark { get; set; }
        public string StatusCode { get; set; }
        public string PoNo { get; set; }
    }
}
