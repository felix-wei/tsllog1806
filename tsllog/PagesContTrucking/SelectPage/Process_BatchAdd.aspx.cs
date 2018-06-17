using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_Process_BatchAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString().Length > 0)
            {
                txt_Id.Text = Request.QueryString["no"].ToString();
            }
        }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"insert into job_process (ProcessType,DateProcess,Qty,Remark1,HouseId) values");
        string values = "";
        if (cbb_ProcessType.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType.Text, dt, spin_Qty.Value,txt_Remark.Text,txt_Id.Text);
        }
        if (cbb_ProcessType1.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess1.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType1.Text, dt, spin_Qty1.Value, txt_Remark1.Text, txt_Id.Text);
        }
        if (cbb_ProcessType2.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess2.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType2.Text, dt, spin_Qty2.Value, txt_Remark2.Text, txt_Id.Text);
        }
        if (cbb_ProcessType3.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess3.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType3.Text, dt, spin_Qty3.Value, txt_Remark3.Text, txt_Id.Text);
        }
        if (cbb_ProcessType4.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess4.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType4.Text, dt, spin_Qty4.Value, txt_Remark4.Text, txt_Id.Text);
        }
        if (cbb_ProcessType5.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess5.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType5.Text, dt, spin_Qty5.Value, txt_Remark5.Text, txt_Id.Text);
        }
        if (cbb_ProcessType6.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess6.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType6.Text, dt, spin_Qty6.Value, txt_Remark6.Text, txt_Id.Text);
        }
        if (cbb_ProcessType7.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess7.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType7.Text, dt, spin_Qty7.Value, txt_Remark7.Text, txt_Id.Text);
        }
        if (cbb_ProcessType8.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess8.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType8.Text, dt, spin_Qty8.Value, txt_Remark8.Text, txt_Id.Text);
        }
        if (cbb_ProcessType9.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_DateProcess9.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}',{4})", cbb_ProcessType9.Text, dt, spin_Qty9.Value, txt_Remark9.Text, txt_Id.Text);
        }
        string re = "false";
        if (values.Length > 0)
        {
            sql = sql + values;
            int i = ConnectSql.ExecuteSql(sql);
            if (i > 0)
            {
                re = "success";
                //Trip_new_auto(txt_JobNo.Text);
            }
        }


        Response.Write("<script>parent.AfterPopub();</script>");
    }
}