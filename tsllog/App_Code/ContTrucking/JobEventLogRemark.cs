using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// JobEventLogRemark 的摘要说明
/// </summary>
public class CtmJobEventLogRemark
{
    public CtmJobEventLogRemark()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    static Dictionary<string, string> list = new Dictionary<string, string>();
    static string empty_string = "Empty command";

    public enum Level
    {
        Job = 1,
        Container = 2,
        Cargo = 3,
        Trip = 4,
        Quotation = 5,
        Attachment = 6,
        Invoice = 7,
        Performance = 8,
        Attendance = 9,
        VehcileReport = 10,
    }
    public enum Command
    {
        Search = 0,
        AddNew = 1,
        Delete = 2,
        Update = 3,
    }

    public static string getDes(Level l, Command c)
    {
        int temp = (int)c;
        return getDes(l, temp);
    }
    public static string getDes(Level l, int c)
    {
        int temp = (int)l;
        return getDes(temp, c);
    }
    public static string getDes(int l, int c)
    {
        if (c < 0)
        {
            return "";
        }
        int temp = l * 100 + c;
        return getDes("" + temp);
    }

    public static string getDes(string code)
    {
        string res = empty_string;
        if (list.ContainsKey(code))
        {
            res = list[code];
        }
        else
        {
            res += ":" + code;
        }
        return res;
    }

    static CtmJobEventLogRemark()
    {
        #region Job
        list.Add("100", "Job search");
        list.Add("101", "Job add new");
        list.Add("102", "Job delete");
        list.Add("103", "Job update");
        list.Add("104", "Job close");
        list.Add("105", "Job void");
        list.Add("106", "Job booked");
        list.Add("107", "Job billing");
        list.Add("108", "Job unVoid");
        #endregion

        #region Container
        list.Add("200", "Container search");
        list.Add("201", "Container add new");
        list.Add("202", "Container delete");
        list.Add("203", "Container update");
        list.Add("204", "Container trucking status");
        list.Add("205", "Container warehouse status");

        #endregion

        #region Cargo
        list.Add("300", "Cargo search");
        list.Add("301", "Cargo add new");
        list.Add("302", "Cargo delete");
        list.Add("303", "Cargo update");
        list.Add("304", "Cargo status");
        list.Add("305", "Completed Warehouse");
        #endregion

        #region Trip
        list.Add("400", "Trip search");
        list.Add("401", "Trip add new");
        list.Add("402", "Trip delete");
        list.Add("403", "Trip update");
        list.Add("404", "Trip status");
        list.Add("405", "Submit e-POD");
        list.Add("406", "Send e-POD");

        #endregion

        #region Quotation
        list.Add("500", "Quotation search");
        list.Add("501", "Quotation add new");
        list.Add("502", "Quotation delete");
        list.Add("503", "Quotation update");
        list.Add("504", "Confirmed Quotation");
        list.Add("505", "Completed Quotation");
        list.Add("506", "Restart Quotation");
        list.Add("507", "Void Quotation");
        #endregion

        #region Attachment
        list.Add("600", "Attachment search");
        list.Add("601", "Attachment upload");
        list.Add("602", "Attachment delete");
        list.Add("603", "Attachment update");

        #endregion

        #region Invoice
        list.Add("700", "Invoice search");
        list.Add("701", "Invoice add");
        list.Add("702", "Invoice delete");
        list.Add("703", "Invoice update");
        list.Add("704", "Cost search");
        list.Add("705", "Cost add");
        list.Add("706", "Cost delete");
        list.Add("707", "Cost update");

        #endregion


        list.Add("800", "Performance search");
        list.Add("801", "Performance add");
        list.Add("802", "Performance delete");
        list.Add("803", "Performance update");
        list.Add("900", "Attendance search");
        list.Add("901", "Attendance add");
        list.Add("902", "Attendance delete");
        list.Add("903", "Attendance update");
        list.Add("1000", "Vehcile FuelLog search");
        list.Add("1001", "Vehcile FuelLog add");
        list.Add("1002", "Vehcile FuelLog delete");
        list.Add("1003", "Vehcile FuelLog update");
        list.Add("1004", "Vehcile Mileage search");
        list.Add("1005", "Vehcile Mileage add");
        list.Add("1006", "Vehcile Mileage delete");
        list.Add("1007", "Vehcile Mileage update");
        list.Add("1008", "Vehcile IssueReport search");
        list.Add("1009", "Vehcile IssueReport add");
        list.Add("1010", "Vehcile IssueReport delete");
        list.Add("1011", "Vehcile IssueReport update");

    }
}