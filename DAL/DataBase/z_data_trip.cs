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
    
    public partial class z_data_trip
    {
        public string info_jobno { get; set; }
        public string jobno { get; set; }
        public string tr { get; set; }
        public string wh { get; set; }
        public string tp { get; set; }
        public string CR { get; set; }
        public string job_from { get; set; }
        public string job_to { get; set; }
        public string depot { get; set; }
        public string info_contno { get; set; }
        public string sys_triptype { get; set; }
        public Nullable<double> scheduledate { get; set; }
        public string info_from { get; set; }
        public string info_to { get; set; }
        public Nullable<System.DateTime> trip_schdule_date_sc { get; set; }
        public Nullable<System.DateTime> info_pickupdate { get; set; }
        public Nullable<System.DateTime> info_pickuptime { get; set; }
        public string contracter { get; set; }
        public string vehicle { get; set; }
        public string trailer { get; set; }
        public string INCENTIVE { get; set; }
        public string CHARGES { get; set; }
        public Nullable<double> cw { get; set; }
        public Nullable<double> cd { get; set; }
        public string cdet { get; set; }
        public string crepair { get; set; }
        public string complete { get; set; }
        public int Id { get; set; }
        public Nullable<int> tripId { get; set; }
    }
}