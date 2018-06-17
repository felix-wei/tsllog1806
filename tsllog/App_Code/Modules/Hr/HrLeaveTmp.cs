using System;
using System.Data;

namespace C2
{
	public class HrLeaveTmp
	{
		private int id;
		private int person;
		private string year;
		private string leaveType;
		private int days;
        private int balDays;
		private string remark;

		public int Id
		{
			get { return this.id; }
		}

		public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

		public string Year
		{
			get { return this.year; }
			set { this.year = value; }
		}

		public string LeaveType
		{
			get { return this.leaveType; }
			set { this.leaveType = value; }
		}

		public int Days
		{
			get { return this.days; }
			set { this.days = value; }
		}
        public int BalDays
        {
            get { return this.balDays; }
            set { this.balDays = value; }
        }

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}
        public decimal BalanceDays
        {
            get
            {
                decimal balDays = 0;
                decimal leaveDays = 0;
                decimal year_n = 0;
                string sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", this.person, this.leaveType, SafeValue.SafeInt(this.year, 0) - 1);
                DataTable dt_day = ConnectSql_mb.GetDataTable(sql);
                for (int i = 0; i < dt_day.Rows.Count; i++)
                {
                    year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                    string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                    if (approveStatus == "Approve")
                        leaveDays += SafeValue.SafeDecimal(dt_day.Rows[i]["Days"], 0);
                }
                sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", this.person, this.leaveType, SafeValue.SafeInt(this.year, 0) - 1);
                decimal totalDays = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql), 0);
                balDays = totalDays - leaveDays;

                leaveDays = 0;
                sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", this.person, this.leaveType, this.year);
                dt_day = ConnectSql_mb.GetDataTable(sql);
                for (int i = 0; i < dt_day.Rows.Count; i++)
                {
                    year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                    string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                    if (approveStatus == "Approve")
                        leaveDays += SafeValue.SafeDecimal(dt_day.Rows[i]["Days"], 0);
                }
                sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", this.person, this.leaveType,this.year);
                totalDays = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0)+balDays;

                balDays = totalDays - leaveDays;

                return balDays;
            } 
        }

        public static decimal getDays(int person,string leaveType,int year) {
            decimal balDays = 0;
            decimal leaveDays = 0;
            int year_n = 0;
            string sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", person, leaveType, year - 1);
            DataTable dt_day = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt_day.Rows.Count; i++)
            {
                year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                if (approveStatus == "Approve")
                    leaveDays += SafeValue.SafeDecimal(dt_day.Rows[i]["Days"], 0);
            }
            sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", person, leaveType, year - 1);
            decimal totalDays = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql), 0);
            balDays = totalDays - leaveDays;

            leaveDays = 0;
            sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", person, leaveType, year);
            dt_day = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt_day.Rows.Count; i++)
            {
                year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                if (approveStatus == "Approve")
                    leaveDays += SafeValue.SafeDecimal(dt_day.Rows[i]["Days"], 0);
            }
            sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", person, leaveType, year);
            totalDays = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql), 0) + balDays;
            return totalDays;
        }

        public static decimal getBalDays(int person, string leaveType,int year){
            decimal balDays = 0;
            decimal leaveDays = 0;
            int year_n = 0;
            string sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", person,leaveType, year - 1);
            DataTable dt_day = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt_day.Rows.Count; i++)
            {
                year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                if (approveStatus == "Approve")
                    leaveDays += SafeValue.SafeInt(dt_day.Rows[i]["Days"], 0);
            }
            sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", person,leaveType,year- 1);
            decimal totalDays = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql), 0);
            balDays = totalDays - leaveDays;

            leaveDays = 0;
            sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", person, leaveType, year);
            dt_day = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt_day.Rows.Count; i++)
            {
                year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                if (approveStatus == "Approve")
                    leaveDays += SafeValue.SafeDecimal(dt_day.Rows[i]["Days"]);
            }
            sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", person, leaveType, year);
            totalDays = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql), 0) + balDays;

            balDays = totalDays - leaveDays;

            return balDays;
        }
        public static int getBalanceDays(int person, string leaveType, int year)
        {
            int balDays = 0;
            int leaveDays = 0;
            int year_n = 0;
            string sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", person, leaveType, year - 1);
            DataTable dt_day = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt_day.Rows.Count; i++)
            {
                year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                if (approveStatus == "Approve")
                    leaveDays += SafeValue.SafeInt(dt_day.Rows[i]["Days"], 0);
            }
            sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", person, leaveType, year - 1);
            int totalDays = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
            balDays = totalDays - leaveDays;

            leaveDays = 0;
            sql = string.Format(@"select Days,ApproveStatus,LeaveType,ApplyDateTime from Hr_Leave where Person={0} and LeaveType='{1}' and year(Date1)={2}", person, leaveType, year);
            dt_day = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt_day.Rows.Count; i++)
            {
                year_n = SafeValue.SafeDate(dt_day.Rows[i]["ApplyDateTime"], DateTime.Today).Year;
                string approveStatus = SafeValue.SafeString(dt_day.Rows[i]["ApproveStatus"]);
                if (approveStatus == "Approve")
                    leaveDays += SafeValue.SafeInt(dt_day.Rows[i]["Days"],0);
            }
            sql = string.Format(@"select Days from Hr_LeaveTmp where Person={0} and LeaveType='{1}' and Year='{2}'", person, leaveType, year);
            totalDays = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0) + balDays;

            balDays = totalDays - leaveDays;

            return balDays;
        }
    }
}
