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
using DevExpress.Web.ASPxDataView;

public partial class PagesTpt_Job_ShipperView : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Viewer"] = null;
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
            string typ = SafeValue.SafeString(Request.QueryString["typ"], "");
            this.txt_type.Text = typ;
            btn_search_Click(null,null);


            this.spin_BkgM3.Value = 0;
            this.spin_BkgM32.Value = 0;
            this.spin_BkgM33.Value = 0;
            this.spin_BkgQty.Value = 0;
            this.spin_BkgQty2.Value = 0;
            this.spin_BkgQty3.Value = 0;
            this.spin_BkgWt.Value = 0;
            this.spin_BkgWt2.Value = 0;
            this.spin_BkgWt3.Value = 0;

            this.date_BkgDate.Date = DateTime.Today;
            this.date_BkgDate2.Date = DateTime.Today;
            this.date_BkgDate3.Date = DateTime.Today;
            this.date_Eta.Date = DateTime.Today;
            this.date_Eta2.Date = DateTime.Today;
            this.date_Eta3.Date = DateTime.Today;
            this.date_Etd.Date = DateTime.Today;
            this.date_Etd2.Date = DateTime.Today;
            this.date_Etd3.Date = DateTime.Today;

        }
        if (Session["Viewer"] != null)
            this.dsTransport.FilterExpression = Session["Viewer"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (txt_TptNo.Text.Trim() != "")
            where = "JobNo='" + txt_TptNo.Text.Trim() + "'";

        else
        {
            if (dateFrom.Length > 0 && dateTo.Length > 0)
                where = GetWhere(where, string.Format(" JobDate >= '{0}' and JobDate < '{1}'", dateFrom, dateTo));
        }
        string sql = "select Role,CustId from [User] where Name='" + EzshipHelper.GetUserName() + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Equals("Client"))
        {
            where = GetWhere(where, " Cust='" + dt.Rows[0][1] + "'");
            sql = "select Contact1 from xxparty where PartyId='" + dt.Rows[0][1] + "'";
            string pic = "";
            DataTable dtPic = ConnectSql.GetTab(sql);
            if (dtPic != null && dtPic.Rows.Count > 0)
            {
                pic = dtPic.Rows[0][0].ToString();
            }
            if (pic.Length > 0)
            {
                txt_CustPic.Text = pic;
                txt_CustPic2.Text = pic;
                txt_CustPic3.Text = pic;
            }
        }
        else
        {
            where = GetWhere(where, " jobProgress<>'Completed'");
        }

        if (where.Length > 0)
        {
            Session["Viewer"] = where+ string.Format(" and {0}={0}",DateTime.Now.ToString("HHmmss"));
            this.dsTransport.FilterExpression = where;// +" and JobType='" + this.txt_type.Text + "'";
        }
        else
        {
            this.dsTransport.FilterExpression = "1=1";
        }
    }
    public string IsClient()
    {
        string sql = "select Role from [User] where Name='" + EzshipHelper.GetUserName() + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            return SafeValue.SafeString(dt.Rows[0][0]);
        }
        return "Client";
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }

    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            if (e.NewValues[i] == null)
            {
                if (e.OldValues[i].GetType() == typeof(string))
                {
                    e.NewValues[i] = "";
                }else if(e.OldValues[i].GetType() == typeof(DateTime))
                    e.NewValues[i]=new DateTime(1753,1,1);
                else if (e.OldValues[i].GetType() == typeof(decimal) || e.OldValues[i].GetType() == typeof(int))
                    e.NewValues[i]=0;
            }
        }
    }
    protected void grid_Transport_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            string closeInd = SafeValue.SafeString(this.grid_Transport.GetRowValues(e.VisibleIndex, "StatusCode"));
            if (closeInd == "CLS")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            }
            else if (closeInd == "CNL")
            {
                e.Row.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                DateTime eta = SafeValue.SafeDate(this.grid_Transport.GetRowValues(0, "Eta"), new DateTime(1900, 1, 1));
                if ((DateTime.Today.Subtract(eta)).Days > 30)
                    e.Row.BackColor = System.Drawing.Color.LightPink;

            }
        }
    }
    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        try
        {

            string sql = "select Contact1,(select top 1 Role from [User] where Name='" + EzshipHelper.GetUserName() + "') from xxparty where PartyId=(select top 1 custId from [User] where Name='" + EzshipHelper.GetUserName() + "')";
            string pic = "";
            DataTable dtPic = ConnectSql.GetTab(sql);
            if (dtPic != null && dtPic.Rows.Count > 0&&dtPic.Rows[0][1].ToString().Equals("Client"))
            {
                pic = dtPic.Rows[0][0].ToString();
            }

            if (this.txt_BkgRef.Text.Trim().Length > 1)
            {
                InsertData(this.cmb_JobType.Text, txt_BkgRef.Text, txt_CustPic.Text, txt_Ves.Text, txt_Voy.Text, txt_Pol.Text, txt_Pod.Text, SafeValue.SafeDate(date_Eta.Date, new DateTime(1753, 1, 1)), SafeValue.SafeDate(date_Etd.Date, new DateTime(1753, 1, 1)), SafeValue.SafeDate(date_BkgDate.Date, new DateTime(1753, 1, 1)), txt_BkgTime.Text, SafeValue.SafeDecimal(spin_BkgWt.Value, 0), SafeValue.SafeDecimal(spin_BkgM3.Value, 0), SafeValue.SafeInt(spin_BkgQty.Value, 0), txt_BkgPackType.Text, txt_JobRmk.Text, txt_cargoDes.Text, txt_PickupFrm1.Text, txt_DeliveryTo1.Text);
                this.cmb_JobType.Text = "";
                txt_BkgRef.Text = "";
                txt_CustPic.Text = pic;
                txt_Ves.Text = "";
                txt_Voy.Text = "";
                txt_Pol.Text = "";
                txt_Pod.Text = "";
                date_Eta.Date = DateTime.Today;
                date_Etd.Date = DateTime.Today;
                date_BkgDate.Date = DateTime.Today;
                txt_BkgTime.Text = "";
                spin_BkgWt.Value = 0;
                spin_BkgM3.Value = 0;
                spin_BkgQty.Value = 0;
                txt_BkgPackType.Text = "";
                txt_JobRmk.Text = "";
                txt_cargoDes.Text = "";
                txt_PickupFrm1.Text = "";
                txt_DeliveryTo1.Text = "";
            }
            if (this.txt_BkgRef2.Text.Trim().Length > 1)
            {
                InsertData(this.cmb_JobType2.Text, txt_BkgRef2.Text, txt_CustPic2.Text, txt_Ves2.Text, txt_Voy2.Text, txt_Pol2.Text, txt_Pod2.Text, SafeValue.SafeDate(date_Eta2.Date, new DateTime(1753, 1, 1)), SafeValue.SafeDate(date_Etd2.Date, new DateTime(1753, 1, 1)), SafeValue.SafeDate(date_BkgDate2.Date, new DateTime(1753, 1, 1)), txt_BkgTime2.Text, SafeValue.SafeDecimal(spin_BkgWt2.Value, 0), SafeValue.SafeDecimal(spin_BkgM32.Value, 0), SafeValue.SafeInt(spin_BkgQty2.Value, 0), txt_BkgPackType2.Text, txt_JobRmk2.Text, txt_cargoDes2.Text, txt_PickupFrm12.Text, txt_DeliveryTo12.Text);
                this.cmb_JobType2.Text = "";
                txt_BkgRef2.Text = "";
                txt_CustPic2.Text = pic;
                txt_Ves2.Text = "";
                txt_Voy2.Text = "";
                txt_Pol2.Text = "";
                txt_Pod2.Text = "";
                date_Eta2.Date = DateTime.Today;
                date_Etd2.Date = DateTime.Today;
                date_BkgDate2.Date = DateTime.Today;
                txt_BkgTime2.Text = "";
                spin_BkgWt2.Value = 0;
                spin_BkgM32.Value = 0;
                spin_BkgQty2.Value = 0;
                txt_BkgPackType2.Text = "";
                txt_JobRmk2.Text = "";
                txt_cargoDes2.Text = "";
                txt_PickupFrm12.Text = "";
                txt_DeliveryTo12.Text = "";
            }
            if (this.txt_BkgRef3.Text.Trim().Length > 1)
            {
                InsertData(this.cmb_JobType3.Text, txt_BkgRef3.Text, txt_CustPic3.Text, txt_Ves3.Text, txt_Voy3.Text, txt_Pol3.Text, txt_Pod3.Text, SafeValue.SafeDate(date_Eta3.Date, new DateTime(1753, 1, 1)), SafeValue.SafeDate(date_Etd3.Date, new DateTime(1753, 1, 1)), SafeValue.SafeDate(date_BkgDate3.Date, new DateTime(1753, 1, 1)), txt_BkgTime3.Text, SafeValue.SafeDecimal(spin_BkgWt3.Value, 0), SafeValue.SafeDecimal(spin_BkgM33.Value, 0), SafeValue.SafeInt(spin_BkgQty3.Value, 0), txt_BkgPackType3.Text, txt_JobRmk3.Text, txt_cargoDes3.Text, txt_PickupFrm13.Text, txt_DeliveryTo13.Text);
                this.cmb_JobType3.Text = "";
                txt_BkgRef3.Text = "";
                txt_CustPic3.Text = pic;
                txt_Ves3.Text = "";
                txt_Voy3.Text = "";
                txt_Pol3.Text = "";
                txt_Pod3.Text = "";
                date_Eta3.Date = DateTime.Today;
                date_Etd3.Date = DateTime.Today;
                date_BkgDate3.Date = DateTime.Today;
                txt_BkgTime3.Text = "";
                spin_BkgWt3.Value = 0;
                spin_BkgM33.Value = 0;
                spin_BkgQty3.Value = 0;
                txt_BkgPackType3.Text = "";
                txt_JobRmk3.Text = "";
                txt_cargoDes3.Text = "";
                txt_PickupFrm13.Text = "";
                txt_DeliveryTo13.Text = "";
            }
            
        }
        catch
        {
        }
        btn_search_Click(null, null);
    }
    private void InsertData(string jobType, string custJobNo, string custPic, string ves, string voy, string pol, string pod, DateTime eta, DateTime etd, DateTime bkgDate, string bkgTime, decimal wt, decimal m3, int qty, string pkgType, string rmk, string des, string frm, string to)
    {
        string runType = "LocalTpt";
        C2.TptJob tj = new C2.TptJob();
        tj.JobDate = DateTime.Today;
        tj.JobType = this.txt_type.Text;
        string jobNo = C2Setup.GetNextNo(tj.JobType, runType, tj.JobDate);
        tj.JobNo = jobNo;
        tj.JobType = jobType;

        string sql = "select CustId,Role from [User] where name='" + EzshipHelper.GetUserName() + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        string cust = "";
        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][1].ToString().Equals("Client"))
            cust = dt.Rows[0][0].ToString();
        tj.Cust = cust;
        tj.BkgRef = custJobNo;
        tj.CustPic = custPic;
        tj.CustEmail = "";
        tj.CustDocNo = "";
        tj.CustDocType = "";

        tj.Vessel = ves;
        tj.Voyage = voy;
        tj.Pol = pol;
        tj.Pod = pod;
        tj.Eta = eta;
        tj.Etd = etd;
        tj.BlRef = "";



        tj.BkgDate = bkgDate;
        tj.BkgTime = bkgTime;
        tj.JobRmk = rmk;

        tj.BkgQty = qty;
        tj.BkgPkgType = pkgType;
        tj.BkgWt = wt;
        tj.BkgM3 = m3;

        tj.PickFrm1 = frm;
        tj.DeliveryTo1 = to;
        tj.CargoMkg = "";
        tj.CargoDesc = des;

        tj.JobProgress = "Booked";
        tj.TptType = "";
        tj.TripCode = "";

        tj.Driver = "";
        tj.VehicleNo = "";

        tj.Qty = qty;
        tj.PkgType = pkgType;
        tj.Wt = wt;
        tj.M3 = m3;


        tj.FeeTpt = 0;
        tj.FeeLabour = 0;
        tj.FeeOt = 0;
        tj.FeeAdmin = 0;
        tj.FeeReimberse = 0;
        tj.FeeMisc = 0;

        tj.FeeTotal = tj.FeeTpt + tj.FeeLabour + tj.FeeOt + tj.FeeAdmin + tj.FeeReimberse + tj.FeeMisc;

        tj.FeeRemark = "";

        tj.CreateBy = EzshipHelper.GetUserName();
        tj.CreateDateTime = DateTime.Now;
        tj.StatusCode = "USE";
        C2.Manager.ORManager.StartTracking(tj, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(tj);
        C2Setup.SetNextNo(tj.JobType, runType, jobNo, tj.JobDate);

    }
}