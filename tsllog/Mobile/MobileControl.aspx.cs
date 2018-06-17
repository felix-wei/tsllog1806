using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_MobileControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string role = EzshipHelper.GetUseRole();
            this.ASPxComboBox1.Value = role;
            this.DataSource2.FilterExpression = "RoleName='" + role + "'";
        }
    }
    protected void ASPxComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DataSource2.FilterExpression = "RoleName='" + this.ASPxComboBox1.Value.ToString() + "'";
    }
    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"with tb1 as (
select * from Mobile_Control where RoleName='{0}'
)
insert into Mobile_Control(Code,Type,IsActive,RoleName) 
select Code,Type,IsActive,'{0}' from Mobile_Control_default as df
where not exists(select * from tb1 where tb1.code=df.Code and tb1.Type=df.Type)", this.ASPxComboBox1.Value.ToString());
        ConnectSql.ExecuteScalar(sql);
        sql = string.Format(@"delete from Mobile_Control where Id in(
select mc.Id from Mobile_Control as mc
left outer join Mobile_Control_default df on mc.Code=df.Code and df.Type=mc.Type where RoleName='{0}' and df.Id is null
)", this.ASPxComboBox1.Value.ToString());
        ConnectSql.ExecuteScalar(sql);
        this.DataSource2.FilterExpression = "RoleName='" + this.ASPxComboBox1.Value.ToString() + "' ";
    }
    protected void grid_BeforePerformDataSelect(object sender, EventArgs e)
    {
        this.DataSource2.FilterExpression = " RoleName='" + this.ASPxComboBox1.Value.ToString() + "'";
    }
}