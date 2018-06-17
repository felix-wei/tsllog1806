using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class ReportFreightSea_Report_Account_BillList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string refType = Session["rType"].ToString();
            if (refType.Length > 0)
            {
                this.lbl_RefType.Text = refType;
                if (refType == "SI")
                    this.cmb_RefType.Text = "Import";
                if (refType == "SE")
                    this.cmb_RefType.Text = "Export";
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.rbt.SelectedIndex = 0;
            //this.rbt_Party.SelectedIndex = 0;
            this.date_From.Date = new DateTime(2011, 1, 1);
            this.date_End.Date = DateTime.Today;
            this.cmb_DocType.Value = "IV";
        }
    }
    protected void btn_Post_click(object sender, EventArgs e)
    {
               
    }

}
