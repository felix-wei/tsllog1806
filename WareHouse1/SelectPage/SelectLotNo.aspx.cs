using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_SelectLotNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            string no = SafeValue.SafeString(Request.QueryString["no"]);
            string sql = "";
            if (no != null && no.Length > 0)
                sql = "select * from Wh_LotNo where Code in(select isnull(LotNo,'') from Wh_DoDet where DoType='IN')";
            else
                sql = "select * from Wh_LotNo where Code not in(select isnull(LotNo,'') from Wh_DoDet where DoType='IN')";
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.ASPxGridView1.DataSource = tab;
            this.ASPxGridView1.DataBind();
        }
    }
}