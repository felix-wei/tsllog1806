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
    
    public partial class psa_bill
    {
        public int Id { get; set; }
        public string JOB_NO { get; set; }
        public Nullable<System.DateTime> F1 { get; set; }
        public string BILL_TYPE { get; set; }
        public string BILLING_COMPANY { get; set; }
        public string BILL_NUMBER { get; set; }
        public Nullable<double> BILL_ITEM_NUMBER { get; set; }
        public Nullable<double> ACCOUNT_NUMBER { get; set; }
        public Nullable<System.DateTime> BILL_DATE { get; set; }
        public string REF_NUMBER { get; set; }
        public string CONTAINER_NUMBER { get; set; }
        public Nullable<double> TARIFF_CODE { get; set; }
        public string TARIFF_DESCRIPTION { get; set; }
        public Nullable<double> RATE { get; set; }
        public string UNIT_DESCRIPTION { get; set; }
        public Nullable<double> BILLABLE_UNIT { get; set; }
        public Nullable<double> AMOUNT { get; set; }
        public string FULL_VESSEL_NAME { get; set; }
        public string FULL_OUT_VOY_NUMBER { get; set; }
        public string FULL_IN_VOY_NUMBER { get; set; }
        public string ABBR_VESSEL_NAME { get; set; }
        public string ABBR_OUT_VOY_NUMBER { get; set; }
        public string ABBR_IN_VOY_NUMBER { get; set; }
        public string LINE_CODE { get; set; }
        public string GROSS_TONNAGE { get; set; }
        public Nullable<double> LOA { get; set; }
        public string SERVICE_ROUTE { get; set; }
        public string IN_SERVICE_ROUTE { get; set; }
        public Nullable<System.DateTime> LAST_BTR_DATE { get; set; }
        public Nullable<System.DateTime> ATB_DATE { get; set; }
        public Nullable<System.DateTime> ATU_DATE { get; set; }
        public string FIRST_ACTIVITY_DATE { get; set; }
        public string LAST_ACTIVITY_DATE { get; set; }
        public string CONNECTING_FULL_VSL_NAME { get; set; }
        public string CONNECTING_FULL_OUT_VOY_NUMBER { get; set; }
        public string CONNECTING_ABBR_VSL_NAME { get; set; }
        public string CONNECTING_ABBR_VOY_NUMBER { get; set; }
        public string CONNECTING_SERVICE_ROUTE { get; set; }
        public string CONNECTING_IN_SERVICE_ROUTE { get; set; }
        public string CONNECTING_VESSEL_COD_DATE { get; set; }
        public string CONNECTING_VESSEL_ATB_DATE { get; set; }
        public Nullable<System.DateTime> SERVICE_START_DATE { get; set; }
        public Nullable<System.DateTime> SERVICE_END_DATE { get; set; }
        public string LOCATION_FROM { get; set; }
        public string LOCATION_TO { get; set; }
        public string BERTH_NUMBER { get; set; }
        public string SLOT_OPERATOR { get; set; }
        public string LOAD_DISC_INDICATOR { get; set; }
        public string FROM { get; set; }
        public string TO { get; set; }
        public string CNTR_TYPE { get; set; }
        public Nullable<double> CNTR_SIZE { get; set; }
        public string ISO_SIZE_TYPE { get; set; }
        public string DG_IMO_CLASS { get; set; }
        public string TRANSHIP_INDICATOR { get; set; }
        public string DEPOT_INDICATOR { get; set; }
        public string REASON_CODE { get; set; }
        public string LADEN_STATUS { get; set; }
        public string CNTR_OPERATOR { get; set; }
        public string GST_INDICATOR { get; set; }
        public Nullable<double> GST_PERCENTAGE { get; set; }
        public string CURRENCY_CODE { get; set; }
        public Nullable<double> EXCHANGE_RATE { get; set; }
        public string ORG_CODE { get; set; }
        public string CHARGE_CATEGORY { get; set; }
        public string CHARGE_TYPE { get; set; }
        public string CHARGE_CLASSIFICATION_1 { get; set; }
        public string CHARGE_CLASSIFICATION_2 { get; set; }
        public string CHARGE_DESCRIPTION { get; set; }
        public string DESCRIPTION_LINE_1 { get; set; }
        public string DESCRIPTION_LINE_2 { get; set; }
        public string DISCOUNT_TARIFF_CODE { get; set; }
        public Nullable<double> DISCOUNT_PERCENT { get; set; }
        public string CUSTOMER_REF_1 { get; set; }
        public string CUSTOMER_REF_2 { get; set; }
        public string CUSTOMER_REF_3 { get; set; }
        public string CUSTOMER_REF_4 { get; set; }
        public string CUSTOMER_REF_5 { get; set; }
        public string CUSTOMER_REF_6 { get; set; }
    }
}
