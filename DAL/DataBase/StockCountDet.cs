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
    
    public partial class StockCountDet
    {
        public int Id { get; set; }
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public string PartyAdd { get; set; }
        public string DoNo { get; set; }
        public Nullable<System.DateTime> DoDate { get; set; }
        public string RefNo { get; set; }
        public string WareHouseId { get; set; }
        public string Product { get; set; }
        public string LotNo { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string PalletNo { get; set; }
        public Nullable<decimal> Qty1 { get; set; }
        public Nullable<decimal> Qty2 { get; set; }
        public Nullable<decimal> Qty3 { get; set; }
        public Nullable<decimal> NewQty { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> NewPrice { get; set; }
        public string Packing { get; set; }
        public string Location { get; set; }
        public string Uom { get; set; }
        public string Att1 { get; set; }
        public string Att2 { get; set; }
        public string Att3 { get; set; }
        public string Att4 { get; set; }
        public string Att6 { get; set; }
        public string Att5 { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<decimal> GrossWeight { get; set; }
        public Nullable<decimal> NettWeight { get; set; }
    }
}
