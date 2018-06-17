using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_TripsUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dtime = DateTime.Now;
            search_dateFrom.Date = DateTime.Now.AddDays(-7);
            //search_dateFrom.Date = DateTime.Now.AddDays(-30);
            search_dateTo.Date = dtime;
            search_tripStatus1.Checked = true;
            search_tripStatus2.Checked = true;
            btn_search_Click(null, null);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        DateTime dateFrom = SafeValue.SafeDate(search_dateFrom.Date, DateTime.Now);
        DateTime dateTo = SafeValue.SafeDate(search_dateTo.Date, DateTime.Now);
        string where = string.Format(@" datediff(day,FromDate,'{0}')<=0 and datediff(day,FromDate,'{1}')>=0", dateFrom, dateTo);

        if (search_jobNo.Text.Trim() != "")
        {
            string p0 = search_jobNo.Text.Trim();
            string p1 = "";
            string p2 = "";
            foreach (char c in p0)
            {
                if (Char.IsLetter(c))
                {
                    p1 += c;
                }
                if (Char.IsNumber(c))
                {
                    p2 += c;
                }
            }
            where += string.Format(@" and JobNo like '{0}%{1}'", p1, p2);
        }
        if (search_containerNo.Text.Trim() != "")
        {
            where += string.Format(@" and ContainerNo like '{0}%'", search_containerNo.Text.Trim());
        }
        string where_Status = "";
        if (search_tripStatus1.Checked)
        {
            where_Status += (where_Status.Equals("") ? " " : " or ") + "Statuscode='P'";
        }
        if (search_tripStatus2.Checked)
        {
            where_Status += (where_Status.Equals("") ? " " : " or ") + "Statuscode='S'";
        }
        if (search_tripStatus3.Checked)
        {
            where_Status += (where_Status.Equals("") ? " " : " or ") + "Statuscode='C'";
        }
        if (search_tripStatus4.Checked)
        {
            where_Status += (where_Status.Equals("") ? " " : " or ") + "Statuscode='X'";
        }
        if (where_Status != "")
        {
            where_Status = " and (" + where_Status + ")";
            where += where_Status;
        }
        string sql = string.Format(@"select * from ctm_jobdet2 where {0}", where);
        DataTable dt = ConnectSql.GetTab(sql);
        grid_Transport.DataSource = dt;
        grid_Transport.DataBind();
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("UpdateInline"))
            {
                Update_Inline(e, SafeValue.SafeInt(ar[1], -1));
            }

        }
    }

    private void Update_Inline(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }
        ASPxComboBox cbb_statuscode = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "cbb_statuscode") as ASPxComboBox;
        TextBox txt_tripId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_tripId") as TextBox;
        ASPxButtonEdit btn_DriverCode = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "btn_DriverCode") as ASPxButtonEdit;
        ASPxDateEdit txt_FromDate = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_FromDate") as ASPxDateEdit;
        ASPxTextBox txt_FromTime = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_FromTime") as ASPxTextBox;
        ASPxButtonEdit btn_ChessisCode = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "btn_ChessisCode") as ASPxButtonEdit;
        ASPxButtonEdit txt_parkingLot = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_parkingLot") as ASPxButtonEdit;
        ASPxMemo txt_toAddress = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_toAddress") as ASPxMemo;

        ASPxSpinEdit txt_Incentive1 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_Incentive1") as ASPxSpinEdit;
        ASPxSpinEdit txt_Incentive2 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_Incentive2") as ASPxSpinEdit;
        ASPxSpinEdit txt_Incentive3 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_Incentive3") as ASPxSpinEdit;
        ASPxSpinEdit txt_Incentive4 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_Incentive4") as ASPxSpinEdit;

        string sql = string.Format(@"select DriverCode from CTM_JobDet2 where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", txt_tripId.Text, SqlDbType.Int));
        string Driver_old = ConnectSql_mb.ExecuteScalar(sql, list).context;

        sql = string.Format(@"update CTM_JobDet2 set Statuscode=@Statuscode,FromDate=@FromDate,FromTime=@FromTime,DriverCode=@DriverCode,ChessisCode=@ChessisCode,ToParkingLot=@ToParkingLot,ToCode=@ToCode,
Incentive1=@Incentive1,Incentive2=@Incentive2,Incentive3=@Incentive3,Incentive4=@Incentive4
where Id=@Id");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", txt_tripId.Text, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@Statuscode", SafeValue.SafeString(cbb_statuscode.Value, "P"), SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", btn_DriverCode.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", txt_FromDate.Date, SqlDbType.Date));
        list.Add(new ConnectSql_mb.cmdParameters("@FromTime", txt_FromTime.Text, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", btn_ChessisCode.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToParkingLot", txt_parkingLot.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", txt_toAddress.Text, SqlDbType.NVarChar, 300));

        list.Add(new ConnectSql_mb.cmdParameters("@Incentive1",SafeValue.SafeDecimal( txt_Incentive1.Text), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Incentive2", SafeValue.SafeDecimal(txt_Incentive2.Text), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Incentive3", SafeValue.SafeDecimal(txt_Incentive3.Text), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Incentive4", SafeValue.SafeDecimal(txt_Incentive4.Text), SqlDbType.Decimal));

        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {
            string re = HttpContext.Current.User.Identity.Name + "," + txt_tripId.Text + "," + btn_DriverCode.Text;
            if (!btn_DriverCode.Text.Equals(Driver_old))
            {
                re += "," + Driver_old;
            }
            e.Result = re;
        }
        else
        {
            e.Result = "Save Error";
        }
    }
}