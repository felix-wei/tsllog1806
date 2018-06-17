using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2
{
    /// <summary>
    /// Summary description for BizResult
    /// </summary>
    public class BizResult
    {
        public BizResult() : this(false)
        {
        }
        public BizResult(bool s) : this(s, "")
        {
        }
        public BizResult(bool s, string c)
        {
            status = s;
            context = c;
        }
        public bool status { get; set; }
        public string context { get; set; }
    }
}