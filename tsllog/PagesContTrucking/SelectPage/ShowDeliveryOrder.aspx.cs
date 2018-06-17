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

public partial class PagesContTrucking_SelectPage_ShowDeliveryOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            btn_Retrieve_Click(null, null);
        }
        //OnLoad();
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
        public Record(int _id, decimal _qty, decimal _sku_qty, decimal _weight, decimal _volume, decimal _l, decimal _w, decimal _h, string _refNo)
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
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), SafeValue.SafeDecimal(spin_Qty.Value), SafeValue.SafeDecimal(spin_WholeQty.Value),
                    SafeValue.SafeDecimal(spin_Weight.Value), SafeValue.SafeDecimal(spin_Volume.Value), SafeValue.SafeDecimal(spin_LengthPack.Value),
                    SafeValue.SafeDecimal(spin_WidthPack.Value), SafeValue.SafeDecimal(spin_HeightPack.Value), ""));
            }
            else if (id == null)
                break;
        }
    }
    #endregion
    protected void grid_wh_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["no"] != null)
        {
            try
            {
                if (list.Count > 0)
                {
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    string jobType = SafeValue.SafeString(Request.QueryString["jobType"]);
                    string jobNo = "";
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + no + "'");
                    C2.CtmJob job = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
                    if (job != null)
                    {

                        jobNo = C2Setup.GetNextNo("", "CTM_Job_DO", DateTime.Today);
                        job.JobNo = jobNo;
                        job.QuoteNo = "";
                        job.JobType = "DO";
                        job.JobDate = DateTime.Now;
                        job.JobStatus = "Confirmed";
                        C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(job);
                        C2Setup.SetNextNo("", "CTM_Job_DO", jobNo, DateTime.Today);

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
                        if (qty > 0)
                        {
                            Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + id + "");
                            C2.JobHouse house = C2.Manager.ORManager.GetObject(query1) as C2.JobHouse;

                            house.CargoType = "OUT";
                            house.JobNo = jobNo;
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
                            C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(house);

                        }
                        else
                        {

                        }
                    }
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Pls Select at least one Cargo";
                }
            }
            catch { }
        }
    }
    public string FilePath(int id)
    {
        string sql = string.Format("select top 1 FilePath from CTM_Attachment where RefNo='{0}'", id);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceQty(object client, object code, object bookingNo, object lineId, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,ClientId,SkuCode,BookingNo,Location,LineId from job_house  group by ClientId,SkuCode,BookingNo,Location,LineId) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location and tab_bal.LineId=mast.LineId
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.LineId={3} and mast.Location='{4}'", client, code, bookingNo, lineId, loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceSkuQty(object client, object code, object bookingNo, object lineId, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then PackQty else -PackQty end) as BalQty,ClientId,SkuCode,BookingNo,Location,LineId from job_house group by ClientId,SkuCode,BookingNo,Location,LineId) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location and tab_bal.LineId=mast.LineId
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.LineId={3} and mast.Location='{4}'", client, code, bookingNo, lineId, loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceWeight(object client, object code, object bookingNo, object lineId, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalQty,ClientId,SkuCode,BookingNo,Location,LineId from job_house  group by ClientId,SkuCode,BookingNo,Location,LineId) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location and tab_bal.LineId=mast.LineId
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.LineId={3} and mast.Location='{4}'", client, code, bookingNo, lineId, loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceVolume(object client, object code, object bookingNo, object lineId, object loc)
    {
        string sql = string.Format(@"select tab_bal.BalQty  from job_house mast
left join (select  sum(case when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalQty,ClientId,SkuCode,BookingNo,Location,LineId from job_house group by ClientId,SkuCode,BookingNo,Location,LineId) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo and tab_bal.Location=mast.Location and tab_bal.LineId=mast.LineId
where mast.ClientId='{0}' and mast.SkuCode='{1}' and mast.BookingNo='{2}' and mast.LineId={3} and mast.Location='{4}'", client, code, bookingNo, lineId, loc);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    protected void btn_Retrieve_Click(object sender, EventArgs e)
    {
        string client = SafeValue.SafeString(Request.QueryString["client"]);
        string jobType = SafeValue.SafeString(Request.QueryString["jobType"]);
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select *,tab_bal.BalQty,mast.JobNo  from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo
left join (select  sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,ClientId,SkuCode,BookingNo from job_house where CargoStatus='C' group by ClientId,BookingNo,SkuCode) as tab_bal on tab_bal.ClientId=mast.ClientId and tab_bal.SkuCode=mast.SkuCode and tab_bal.BookingNo=mast.BookingNo
where mast.RefNo='{0}' and job.JobType='WDO'", no);
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