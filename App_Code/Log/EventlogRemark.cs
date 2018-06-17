using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2
{
    /// <summary>
    /// EventlogRemark 的摘要说明
    /// </summary>
    public class EventlogRemark
    {
        public static string GetRemark(string type, int i)
        {
            string res = "";
            #region Create
            if (type == "C")
            {
                switch (i)
                {
                    case 0:
                        res = "New Quotation";
                        break;
                    case 1:
                        res = "New Job";
                        break;
                    case 2:
                        res = "New Container";
                        break;
                    case 3:
                        res = "New Trip";
                        break;
                    case 4:
                        res = "New Cargo";
                        break;
                    case 5:
                        res = "Add Invoice";
                        break;
                }
            }
            #endregion
            #region Update
            if (type == "U")
            {
                switch (i)
                {
                    case 0:
                        res = " Close Job";
                        break;
                    case 1:
                        res = "Trip Update";
                        break;
                    case 2:
                        res = "	Container change warehouse status:";
                        break;
                    case 3:
                        res = "Confirmed Quotation";
                        break;
                    case 4:
                        res = "Restart Quotation";
                        break;
                    case 5:
                        res = "Completed Quotation";
                        break;
                    case 6:
                        res = "Un Voided Quotation";
                        break;
                    case 7:
                        res = "Voided Quotation";
                        break;
                    case 8:
                        res = "Trip change container status:";
                        break;
                    case 9:
                        res = "Completed Container";
                        break;
                    case 10:
                        res = "Warehouse Completed";
                        break;
                }
            }
            #endregion
            #region Delete
            if (type == "D")
            {
                switch (i)
                {
                    case 0:
                        res = "Delete File";
                        break;
                    case 1:
                        res = "Delete Trip";
                        break;
                    case 2:
                        res = "";
                        break;
                    case 3:
                        res = "";
                        break;
                    case 4:
                        res = "";
                        break;
                }
            }
            #endregion
            return res;
        }
    }
}