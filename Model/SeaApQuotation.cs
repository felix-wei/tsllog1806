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
    
    public partial class SeaApQuotation
    {
        public int Id { get; set; }
        public string QuoteNo { get; set; }
        public Nullable<System.DateTime> QuoteDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public string QuoteType { get; set; }
        public string PartyTo { get; set; }
        public string PartyName { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Contact { get; set; }
        public string Pol { get; set; }
        public string Pod { get; set; }
        public string FinDest { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public Nullable<System.DateTime> Eta { get; set; }
        public Nullable<System.DateTime> Etd { get; set; }
        public Nullable<System.DateTime> EtaDest { get; set; }
        public string CurrencyId { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public string SalesmanId { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string Rmk { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }
    }
}