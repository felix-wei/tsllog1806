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
    
    public partial class ref_permit
    {
        public int Id { get; set; }
        public string PermitNo { get; set; }
        public Nullable<System.DateTime> PermitDate { get; set; }
        public string PermitBy { get; set; }
        public string PermitRemark { get; set; }
        public string PartyInvNo { get; set; }
        public Nullable<decimal> GstAmt { get; set; }
        public string PaymentStatus { get; set; }
        public string IncoTerms { get; set; }
        public string JobNo { get; set; }
        public Nullable<int> ContId { get; set; }
        public string HblNo { get; set; }
    }
}
