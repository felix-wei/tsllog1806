using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Aspose.Cells;
using System.Drawing;

public partial class Modules_Tpt_Report_StockInspection : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = DateTime.Today.AddDays(-1);
            this.cmb_PartyTo.Value = "Mar-02";
        }
    }

}