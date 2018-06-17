using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using C2;
using System.Data;
using Wilson.ORMapper;
using DevExpress.Web.ASPxGridView;

public partial class MastData_AccountClose : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        BindData();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "ClosePeriod")
        {
            e.Result=AccClose.ClosePeriod(true);
        }
        else if (s == "OpenPeriod")
        {
            e.Result = AccClose.OpenPeriod(true);
        }
    }
    private void BindData()
    {
        string sql = "SELECT SequenceId, Year, Period FROM XXAccPeriod WHERE (CloseInd = 'N') ORDER BY Year, Period";
        DataTable tab = Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
}
