﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class PagesContTrucking_SelectPage_CreateJobOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string no = SafeValue.SafeString(Request.QueryString["no"]);
            string client = SafeValue.SafeString(Request.QueryString["client"]);
            string jobType = SafeValue.SafeString(Request.QueryString["jobType"]);
            string type = "";
            if (jobType == "EXP")
            {
                type = "IMP";
            }
            if (jobType == "DO")
            {
                type = "GR";
            }
            if (no.Length > 0)
            {
                dsTrip.FilterExpression = "JobType='WDO' and JobNo='"+no+"'";
            }
            btn_Retrieve_Click(null, null);
        }
        OnLoad();
    }
    #region Delivery Stock
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
        public string toLoc = "";
        public Record(int _id, decimal _qty, decimal _sku_qty, decimal _weight, decimal _volume, decimal _l, decimal _w, decimal _h, string _refNo,string _toLoc)
        {
            id = _id;
            qty = _qty;
            sku_qty = _sku_qty;
            weight = _weight;
            volume = _volume;
            l = _l;
            w = _w;
            h = _h;
            refNo = _refNo;
            toLoc = _toLoc;
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
            ASPxSpinEdit spin_Qty = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit spin_WholeQty = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "spin_WholeQty") as ASPxSpinEdit;
            ASPxSpinEdit spin_Weight = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "spin_Weight") as ASPxSpinEdit;
            ASPxSpinEdit spin_Volume = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "spin_Volume") as ASPxSpinEdit;
            ASPxSpinEdit spin_LengthPack = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "spin_LengthPack") as ASPxSpinEdit;
            ASPxSpinEdit spin_WidthPack = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "spin_WidthPack") as ASPxSpinEdit;
            ASPxSpinEdit spin_HeightPack = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "spin_HeightPack") as ASPxSpinEdit;
            ASPxLabel lbl_JobNo = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["JobNo"], "lbl_JobNo") as ASPxLabel;
            ASPxComboBox txt_ToLoc = this.grid_wh.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_wh.Columns["Qty"], "txt_ToLoc") as ASPxComboBox;
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), SafeValue.SafeDecimal(spin_Qty.Value), SafeValue.SafeDecimal(spin_WholeQty.Value),
                    SafeValue.SafeDecimal(spin_Weight.Value), SafeValue.SafeDecimal(spin_Volume.Value), SafeValue.SafeDecimal(spin_LengthPack.Value),
                    SafeValue.SafeDecimal(spin_WidthPack.Value), SafeValue.SafeDecimal(spin_HeightPack.Value),"",SafeValue.SafeString(txt_ToLoc.Value)));
            }
            else if (id == null)
                break;
        }
    }
    #endregion
    protected void grid_wh_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string para = e.Parameters;
        if (Request.QueryString["no"] != null)
        {
            try
            {

                #region list
                if (list.Count > 0)
                {
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    string jobType = SafeValue.SafeString(Request.QueryString["type"]);
                    string client = SafeValue.SafeString(Request.QueryString["clientId"]);

                    #region OK
                    if (para == "OK")
                    {

                        string jobNo = "";
                        string refType = "";
                        bool action = false;
                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + no + "'");
                        C2.CtmJob job = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
                        C2.CtmJobDet2 trip = null;
                        if (job != null)
                        {
                            refType = SafeValue.SafeString(Request.QueryString["action"]);
                            jobNo = C2Setup.GetNextNo("", "CTM_Job_" + refType, DateTime.Today);
                            job.JobNo = jobNo;
                            job.QuoteNo = jobNo;
                            job.JobType = refType;
                            job.JobDate = DateTime.Now;
                            job.JobStatus = "Confirmed";
                            job.QuoteStatus = "None";
                            C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(job);
                            C2Setup.SetNextNo("", "CTM_Job_" + refType, jobNo, DateTime.Today);


                            #region Trip
                            trip = new C2.CtmJobDet2();
                            string sql = string.Format(@"select max(TripIndex) from CTM_JobDet2 where JobType=@JobType and JobNo=@JobNo");
                            List<ConnectSql_mb.cmdParameters> list_cmd = new List<ConnectSql_mb.cmdParameters>();
                            list_cmd.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar, 100));
                            list_cmd.Add(new ConnectSql_mb.cmdParameters("@JobType", refType, SqlDbType.NVarChar, 100));
                            string maxIdex = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql, list_cmd).context, "//00");
                            int n = SafeValue.SafeInt(maxIdex.Substring(maxIdex.LastIndexOf("/") + 1), 0) + 1;
                            string str = (100 + n).ToString().Substring(1);
                            trip.BookingDate = DateTime.Today;
                            trip.TripIndex = jobNo + "/" + refType + "/" + str;
                            trip.JobNo = jobNo;
                            trip.FromDate = DateTime.Today;
                            trip.ToDate = DateTime.Today;
                            trip.CreateUser = HttpContext.Current.User.Identity.Name;
                            trip.CreateTime = DateTime.Now;
                            trip.UpdateUser = HttpContext.Current.User.Identity.Name;
                            trip.UpdateTime = DateTime.Now;
                            trip.JobType = refType;
                            trip.TripCode = refType;
                            trip.Remark = job.Remark;
                            trip.ToCode = job.DeliveryTo;
                            trip.FromCode = job.PickupFrom;
                            trip.Statuscode = "P";
                            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(trip);


                            string userId = HttpContext.Current.User.Identity.Name;
                            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                            elog.Platform_isWeb();
                            elog.Controller = userId;
                            elog.ActionLevel_isCONT(trip.Id);
                            elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Container, 1, "");
                            elog.log();
                            #endregion
                        }

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
                            string toLoc = list[i].toLoc;
                            if (qty > 0)
                            {
                                Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + id + "");
                                C2.JobHouse house = C2.Manager.ORManager.GetObject(query1) as C2.JobHouse;
                                house.LineId = id;
                                house.CargoType = "OUT";
                                house.JobNo = jobNo;
                                house.ContNo = "";
                                house.JobType = refType;
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
                                house.RefNo = no;
                                house.Qty = qty;
                                house.OpsType = "Delivery";
                                house.ClientId = client;
                                house.CargoStatus = "P";
                                house.TripId = trip.Id;
                                house.TripIndex =trip.TripIndex;
                                C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
                                C2.Manager.ORManager.PersistChanges(house);
                                int lineId = house.Id;

                                if (refType == "TR")
                                {
                                    house.LineId = lineId;
                                    house.Location = toLoc;
                                    house.JobNo = jobNo;
                                    house.TransferNo = jobNo;
                                    house.RefNo = refNo;
                                    house.CargoType = "IN";
                                    house.RefNo = no;
                                    house.CargoStatus = "P";
                                    C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
                                    C2.Manager.ORManager.PersistChanges(house);


                                }
                                action = true;
                            }
                            else
                            {
                                action = false;
                            }

                        }
                        if (action)
                            e.Result = "Action Success! No is " + jobNo;
                        else
                            e.Result = "Action Error!";
                    }
                    #endregion

                    #region New WDO Trip
                    if (para == "NewTrip")
                    {
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
                            string toLoc = list[i].toLoc;
                            if (qty > 0)
                            {
                                Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + id + "");
                                C2.JobHouse house = C2.Manager.ORManager.GetObject(query1) as C2.JobHouse;
                                house.LineId = id;
                                house.CargoType = "OUT";
                                house.JobNo = no;
                                house.ContNo = "";
                                house.JobType = jobType;
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
                                house.RefNo = no;
                                house.Qty = qty;
                                house.OpsType = "Delivery";
                                house.ClientId = client;
                                house.TripIndex = SafeValue.SafeString(cbb_TripNo.Text);
                                house.TripId = SafeValue.SafeInt(cbb_TripNo.Value,0);
                                house.CargoStatus = "P";
                                C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
                                C2.Manager.ORManager.PersistChanges(house);
                               
                            }
                            action = true;
                        }
                        if (action)
                            e.Result = "Success" ;
                        else
                            e.Result = "Action Error!";
                    }
                    #endregion
                }
                else
                {
                    e.Result = "Pls Select at least one Cargo";
                }
                    #endregion
            }
            catch { }
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
        string client = SafeValue.SafeString(Request.QueryString["client"]);
        string jobType = SafeValue.SafeString(Request.QueryString["jobType"]);
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string action = SafeValue.SafeString(Request.QueryString["action"]);
        string type = "";
        if (jobType == "EXP")
        {
            type = "IMP";
        }
        if (jobType == "WDO")
        {
            type = "WGR";
        }

        string sql = string.Format(@"select mast.*,tab_bal.BalQty,mast.JobNo,'{1}' as Action,Location as ToLocation  from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo
left join (select  sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.JobNo='{0}' and BalQty>0 and mast.CargoType='IN'", no,action);
        if (txt_ContNo.Text.Length > 0)
        {
            sql += " and ContNo like '%" + txt_ContNo.Text.Trim() + "%'";
        }
        if (txt_JobNo.Text.Length > 0)
        {
            sql += " and mast.JobNo like '%" + txt_JobNo.Text.Trim() + "%'";
        }
        if (txt_Marking.Text.Length > 0)
        {
            sql += " and Marking1 like '%" + txt_Marking.Text.Trim() + "%'";
        }
        if (txt_HblNo.Text.Length > 0)
        {
            sql += " and HblNo like '%" + txt_HblNo.Text.Trim() + "%'";
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        this.grid_wh.DataSource = dt;
        this.grid_wh.DataBind();
    }
}