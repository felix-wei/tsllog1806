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

public partial class Modules_Tpt_SelectPage_StockListForJobReceipt : System.Web.UI.Page
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
        public Record(int _id, decimal _qty, decimal _sku_qty, decimal _weight, decimal _volume, decimal _l, decimal _w, decimal _h)
        {
            id = _id;
            qty = _qty;
            sku_qty = _sku_qty;
            weight = _weight;
            volume = _volume;
            l = _l;
            w = _w;
            h = _h;
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
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), SafeValue.SafeDecimal(spin_Qty.Value), SafeValue.SafeDecimal(spin_WholeQty.Value),
                    SafeValue.SafeDecimal(spin_Weight.Value), SafeValue.SafeDecimal(spin_Volume.Value), SafeValue.SafeDecimal(spin_LengthPack.Value),
                    SafeValue.SafeDecimal(spin_WidthPack.Value), SafeValue.SafeDecimal(spin_HeightPack.Value)));
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
                    string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
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
                        if (qty > 0)
                        {
                            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + id + "");
                            C2.JobHouse house = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;

                            if (house != null)
                            {
                                C2.JobReceipt rec = new JobReceipt();
                                rec.Qty = qty;
                                rec.Weight = weight;
                                rec.Volume = volume;
                                rec.JobNo = jobNo;
                                rec.LineId = id;
                                rec.HblNo = house.HblNo;
                                rec.BookingNo = house.BookingNo;
                                rec.CargoId = id;
                                rec.PackQty = sku_qty;
                                rec.Remark1 = house.Remark1;
                                rec.Remark2 = house.Remark2;
                                rec.Marking1 = house.Marking1;
                                rec.Marking2 = house.Marking2;
                                rec.Location = house.Location;
                                rec.CargoType = "IN";
                                rec.TrailerId = SafeValue.SafeInt(no, 0);
                                rec.TrailerNo = house.ContNo;

                                C2.Manager.ORManager.StartTracking(rec, Wilson.ORMapper.InitialState.Inserted);
                                C2.Manager.ORManager.PersistChanges(rec);
                            }

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
        string sql = string.Format(@"select (Qty-isnull(tab_bal.TotalQty,0)) as BalQty  from job_house mast
left join (select  sum(PackQty) as TotalQty,LineId from job_receipt  group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceWeight(object lineId)
    {
        string sql = string.Format(@"select (Qty-isnull(tab_bal.TotalQty,0)) as BalQty   from job_house mast
left join (select  sum(Weight) as TotalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    public decimal BalanceVolume(object lineId)
    {
        string sql = string.Format(@"select (Qty-isnull(tab_bal.TotalQty,0)) as BalQty  from job_house mast
left join (select  sum(Volume) as TotalQty,LineId from job_house  group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
where mast.LineId={0}", lineId);
        return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
    }
    protected void btn_Retrieve_Click(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);

        string sql = string.Format(@"select * from (select mast.*,(Qty-isnull(tab_bal.TotalQty,0)) as BalQty,(Weight-isnull(tab_bal.TotalWeight,0)) as BalWeight,(Volume-isnull(tab_bal.TotalVolume,0)) as BalVolume,(PackQty-isnull(tab_bal.TotalPack,0)) as BalPack  from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo
left join (select  sum(Qty) as TotalQty,sum(Weight) as TotalWeight,sum(Volume) as TotalVolume,sum(PackQty) as TotalPack,LineId from job_receipt  group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
) as tab where  JobNo='{0}' and  BalQty>0", jobNo);
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
        if (cbb_JobType.Text != "All")
        {
            sql += " and OpsType= '" + cbb_JobType.Text.Trim() + "'";
        }
        else
        {
            sql += " and OpsType in ('Delivery','Export','Storage')";
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        this.grid_wh.DataSource = dt;
        this.grid_wh.DataBind();
    }
}