using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class PagesContTrucking_SelectPage_SelectCargoForReturn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_Retrieve_Click(null,null);
        }
        OnLoad();
    }
    #region Return
    protected void grid_wh_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (Request.QueryString["no"] != null)
        {
            try
            {
                if (ar.Length >= 2)
                {
                    if (ar[0].Equals("Dimensionline"))
                    {
                        ASPxGridView grid = sender as ASPxGridView;

                        int rowIndex = SafeValue.SafeInt(ar[1], -1);
                        ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                        e.Result = txt_Id.Text;
                    }
                }
                else
                {

                    #region 
                    if (list.Count > 0)
                    {
                        string no = SafeValue.SafeString(Request.QueryString["no"]);
                        string type = SafeValue.SafeString(Request.QueryString["type"]);
                        string client = SafeValue.SafeString(Request.QueryString["client"]);
                        bool action = false;
                        for (int i = 0; i < list.Count; i++)
                        {
                            int id = list[i].id;
                            decimal qty = list[i].qty;
                            decimal weight = list[i].weight;
                            decimal volume = list[i].volume;
                            decimal sku_qty = list[i].sku_qty;
                            decimal l = list[i].l;
                            decimal h = list[i].h;
                            decimal w = list[i].w;
                            string refNo = list[i].refNo;
                            if (qty > 0)
                            {
                                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + id + "");
                                C2.JobHouse house = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;

                                house.CargoStatus = "C";
                                house.JobNo = no;
                                house.ContNo = "";
                                house.QtyOrig = qty;
                                house.PackQty = sku_qty;
                                house.Weight = weight;
                                house.Volume = volume;
                                house.WeightOrig = weight;
                                house.VolumeOrig = volume;
                                house.LandStatus = "Normal";
                                house.DgClass = "Normal";
                                house.DamagedStatus = "Normal";
                                house.LengthPack = l;
                                house.WidthPack = w;
                                house.HeightPack = h;
                                house.RefNo = refNo;
                                house.ClientId = client;
                                house.LineId = id;
                                house.CargoType = type;
                                house.StockDate = DateTime.Now;
                                string jobType = "";
                                if (type == "IN")
                                {
                                    jobType = "WGR";
                                }
                                else if (type == "OUT")
                                {
                                    jobType = "WDO";
                                }
                                string res = Trip_New(jobType, house.TripId);
                                string[] str = res.Split('_');
                                house.TripId = SafeValue.SafeInt(str[0], 0);
                                house.TripIndex = str[1];

                                house.Qty = qty;
                                house.OpsType = "Delivery";
                                C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
                                C2.Manager.ORManager.PersistChanges(house);
                                action = true;
                            }
                            else
                            {
                                action = false;
                                break;
                            }
                        }
                        if (action)
                            e.Result = "Success";
                        else
                            e.Result = "Pls keyin the Qty";
                    }
                    else
                    {
                        e.Result = "Pls Select at least one Cargo";
                    }
                    #endregion
                }
            }
            catch { }
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public decimal qty = 0;
        public decimal sku_qty = 0;
        public decimal weight = 0;
        public decimal volume = 0;
        public decimal l = 0;
        public decimal w = 0;
        public decimal h = 0;
        public string refNo = "";
        public Record(int _id, decimal _qty, decimal _sku_qty, decimal _weight, decimal _volume)
        {
            id = _id;
            qty = _qty;
            sku_qty = _sku_qty;
            weight = _weight;
            volume = _volume;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = 1000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxSpinEdit spin_Qty = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["BalQty"], "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit spin_WholeQty = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["BalQty"], "spin_WholeQty") as ASPxSpinEdit;
            ASPxSpinEdit spin_Weight = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["BalQty"], "spin_Weight") as ASPxSpinEdit;
            ASPxSpinEdit spin_Volume = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["BalQty"], "spin_Volume") as ASPxSpinEdit;
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), SafeValue.SafeDecimal(spin_Qty.Value), SafeValue.SafeDecimal(spin_WholeQty.Value),
                    SafeValue.SafeDecimal(spin_Weight.Value), SafeValue.SafeDecimal(spin_Volume.Value)));
            }
            else if (id == null)
                break;
        }
    }
    public string FilePath(int id)
    {
        string sql = string.Format("select top 1 FilePath from CTM_Attachment where RefNo='{0}'", id);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceQty(object lineId)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,LineId from job_house  group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceSkuQty(object lineId)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then PackQty else -PackQty end) as BalQty,LineId from job_house  group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceWeight(object lineId)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceVolume(object lineId)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalQty,LineId from job_house  group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    protected void btn_Retrieve_Click(object sender, EventArgs e)
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        string sql = string.Format(@"select mast.*,tab_bal.BalQty,mast.JobNo  from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo
left join (select  sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,LineId from job_house  group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where job.JobNo='{0}' and tab_bal.BalQty>0", no);
        if (type == "IN")
            type = "OUT";
        else if (type == "OUT")
            type = "IN";
        sql += " and CargoType='"+type+"'";
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        this.grid_wh.DataSource = dt;
        this.grid_wh.DataBind();
    }
    private string Trip_New(string jobType,int tripId)
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;

        string sql = string.Format(@"select max(TripIndex) from CTM_JobDet2 where JobType=@JobType and JobNo=@JobNo");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", no, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", jobType, SqlDbType.NVarChar, 100));
        string maxIdex = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql, list).context, "//00");
        int n = SafeValue.SafeInt(maxIdex.Substring(maxIdex.LastIndexOf("/") + 1), 0) + 1;
        string str = (100 + n).ToString().Substring(1);
        string fromCode = trip.ToCode;
        string toCode = trip.FromCode;
        trip.BookingDate = DateTime.Today;
        trip.TripIndex = no + "/" + jobType + "/" + str;
        trip.Self_Ind = "No";
        trip.JobNo = no;
        //trip.FromDate = DateTime.Today;
        //trip.ToDate = DateTime.Today;
        trip.CreateUser = HttpContext.Current.User.Identity.Name;
        trip.CreateTime = DateTime.Now;
        trip.UpdateUser = HttpContext.Current.User.Identity.Name;
        trip.UpdateTime = DateTime.Now;
        trip.JobType = jobType;
        trip.TripCode = jobType;
        trip.ReturnType = "Y";
        trip.ToCode = toCode;
        trip.FromCode = fromCode;
        trip.Statuscode = "P";
        C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(trip);

        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isJOB(trip.Id);
        elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Trip, 1, "");

        return trip.Id + "_" + trip.TripIndex;

    }

    #endregion
}