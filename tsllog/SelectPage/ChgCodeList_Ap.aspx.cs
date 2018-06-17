using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class SelectPage_ChgCodeList_Ap : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = string.Format(@"select  SequenceId,REPLACE(REPLACE(ChgcodeId,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeId,REPLACE(REPLACE(ChgcodeDes,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeDes,ChgUnit,GstTypeId,GstP,ArCode,ApCode,ImpExpInd from XXChgCode");
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
}
