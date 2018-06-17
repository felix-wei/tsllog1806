﻿using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wilson.ORMapper;

public partial class Modules_Freight_Job_AssignExportList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-2);
            txt_search_dateTo.Date = DateTime.Now.AddDays(7);
            date_Assign.Date = DateTime.Today.AddDays(1);
        }
        
        OnLoad();
        
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = "";
        if (SafeValue.SafeString(cbb_Type.Value) == "N")
        {
            where = " BalQty>0 and JobType='WGR' and CargoType='IN'";
            sql = string.Format(@"select * from(select h.JobNo,h.Id,h.QtyOrig,h.WeightOrig,h.VolumeOrig,(case when h.ShipDate<'1900-01-01' or h.ShipDate is null then getdate()+1 else h.ShipDate end) as ShipDate,h.ShipIndex,
(case when h.ShipPortCode is null then 'SGSIN' else h.ShipPortCode end ) as ShipPortCode,(case when h.ContIndex is null then '1' else h.ContIndex end) as ContIndex,h.ConsigneeInfo,job.JobType,h.CargoType,h.CargoStatus,
h.BookingNo,h.HblNo,h.Marking1,h.Marking2,h.SkuCode,h.ConsigneeContact,h.ConsigneeTel,h.ConsigneeEmail,h.ConsigneeZip,h.ConsigneeAddress,h.Remark1,tab_hand.BalQty,tab_hand.BalWeight,tab_hand.BalVolume,
job.Vessel,job.Voyage,job.JobDate from job_house h inner join ctm_job job on h.JobNo=job.JobNo 
left join(select sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,sum(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalWeight,sum(case when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalVolume,LineId from job_house group by LineId) as tab_hand on tab_hand.LineId=h.Id
 ) as tab ");
        }
        else if (SafeValue.SafeString(cbb_Type.Value) == "Y")
        {
            where = " BalQty=0 and CargoType='OUT' and JobType='EXP'";
            sql = string.Format(@"select * from(select h.JobNo,h.Id,h.QtyOrig,h.WeightOrig,h.VolumeOrig,(case when h.ShipDate<'1900-01-01' or h.ShipDate is null then getdate()+1 else h.ShipDate end) as ShipDate,h.ShipIndex,
(case when h.ShipPortCode is null then 'SGSIN' else h.ShipPortCode end ) as ShipPortCode,(case when h.ContIndex is null then '1' else h.ContIndex end) as ContIndex,h.ConsigneeInfo,job.JobType,h.CargoType,h.CargoStatus,
h.BookingNo,h.HblNo,h.Marking1,h.Marking2,h.SkuCode,h.ConsigneeContact,h.ConsigneeTel,h.ConsigneeEmail,h.ConsigneeZip,h.ConsigneeAddress,h.Remark1,tab_hand.BalQty,tab_hand.BalWeight,tab_hand.BalVolume,
job.Vessel,job.Voyage,job.JobDate from job_house h inner join ctm_job job on h.JobNo=job.JobNo 
left join(select  sum(QtyOrig) QtyOrig,LineId from job_house where CargoType='IN' group by LineId) as tab_in on tab_in.LineId=h.LineId
left join(select  sum(case when CargoType='IN' then QtyOrig else -QtyOrig end) as BalQty,sum(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalWeight,sum(case when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalVolume,LineId from job_house group by LineId) as tab_hand on tab_hand.LineId=tab_in.LineId
 ) as tab  ");
        }


        string from = "";
        string to = "";
        if (txt_search_jobNo.Text != "")
        {
            where = GetWhere(where, string.Format(@" JobNo='{0}'", txt_search_jobNo.Text));
        }
        else if (txt_search_BookingNo.Text != "")
        {
            where = GetWhere(where, string.Format(@" BookingNo='{0}'", txt_search_BookingNo.Text));
        }
        else if (txt_HblNo.Text != "")
        {
            where = GetWhere(where, string.Format(@" HblNo='{0}'", txt_HblNo.Text));
        }
        else
        {
            if (txt_search_dateFrom.Date != null && txt_search_dateTo.Date != null)
            {
                from = txt_search_dateFrom.Date.ToString("yyyyMMdd");
                to = txt_search_dateTo.Date.ToString("yyyyMMdd");
            }
            if (txt_Product.Text != "")
            {
                where = GetWhere(where, string.Format(@" SkuCode='{0}'", txt_Product.Text));
            }
            if (txt_Vessel.Text != "")
            {
                where = GetWhere(where, string.Format(@" Vessel='{0}'", txt_Vessel.Text));
            }

            if (from.Length > 0 && to.Length > 0)
            {
                if (SafeValue.SafeString(cbb_Date.Value) == "Job")
                {
                    where = GetWhere(where, string.Format(@" '{0}'<=JobDate and JobDate<='{1}'", from, to));
                }
                if (SafeValue.SafeString(cbb_Date.Value) == "Ship") {
                    where = GetWhere(where, string.Format(@" '{0}'<=ShipDate and ShipDate<='{1}'", from, to));
                }
            }
            
        }
        if (where.Length > 0)
        {
            sql += " where " + where;
        }
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        grid.DataSource = dt;
        grid.DataBind();
    }
    public string FilePath(int id)
    {
        string sql = string.Format("select top 1 FilePath from CTM_Attachment where JobNo='{0}'", id);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    public string Status(object s)
    {
        string status = SafeValue.SafeString(s);
        if (status == "P")
            status = "Pending";
        if (status == "C")
            status = "Completed";
        return status;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
        {
            where += " and" + s;
        }
        else
        {
            where = s;
        }
        return where;
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string paras = e.Parameters;
        string[] ar = paras.Split('_');

        if (paras == "OK")
        {
            #region
            string jobNo = GetJobNo();
            if (jobNo.Length>0)
            {
                if (list.Count > 0)
                {
                    e.Result = create_house(list.Count, -1,jobNo);
                }
                else
                {
                    e.Result = "Action Error";
                }
            }
            else
            {
                e.Result = "NO Export";
            }           
            #endregion
        }
        if (paras == "New")
        {

            #region
            DateTime now = date_Assign.Date;
            string jobNo = GetJobNo();
            if (now != null && jobNo.Length==0)
            {
                string userId = HttpContext.Current.User.Identity.Name;
                #region New Export Job
                C2.CtmJob job = new C2.CtmJob();
                jobNo = C2Setup.GetNextNo("", "CTM_Job_EXP", now);
                job.JobNo = jobNo;
                job.JobDate = now;
                string client = System.Configuration.ConfigurationManager.AppSettings["EdiClient"];
                job.ClientId = EzshipHelper.GetPartyId(client);
                job.DeliveryTo = "";
                job.JobType = "EXP";
                job.StatusCode = "USE";
                job.Contractor = "YES";
                job.HaulierId = job.ClientId;
                job.QuoteNo = jobNo;
                job.JobStatus = "Confirmed";
                job.QuoteStatus = "None";
                job.QuoteDate = DateTime.Today;
                job.Pol = txt_Pol.Text;
                job.Pod = "SGSIN";
                C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(job);
                C2Setup.SetNextNo("", "CTM_Job_EXP", jobNo, now);

                e.Result = " Job No is " + jobNo;
                #endregion
            }
            else
            {
                e.Result = "Create Error!Today had a export job";
            }
            #endregion
        }
        if (paras == "Assign")
        {
            #region
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count;i++ )
                {
                    int id = list[i].id;
                    int shipIndex = list[i].shipIndex;
                    DateTime shipDate = date_Assign.Date;
                    string postCode = list[i].postCode;
                    string contIndex = list[i].contIndex;
                    decimal qty = list[i].qty;
                    decimal weight = list[i].weight;
                    decimal volume = list[i].volume;
                    update_house(id, shipDate, shipIndex.ToString(), postCode, contIndex, qty, weight, volume);
                    e.Result = "";
                }
            }
            else {
                e.Result = "Action Error";
            }
            #endregion
        }
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("Saveline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_Id = this.grid.FindRowCellTemplateControl(rowIndex, null, "lbl_Id") as ASPxLabel;
                ASPxDateEdit date_ShipDate = this.grid.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipDate"], "date_ShipDate") as ASPxDateEdit;
                ASPxTextBox txt_ShipIndex = this.grid.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipIndex"], "txt_ShipIndex") as ASPxTextBox;
                ASPxTextBox txt_ShipPortCode = this.grid.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipPortCode"], "txt_ShipPortCode") as ASPxTextBox;
                ASPxComboBox cbb_ContIndex = this.grid.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContIndex"], "cbb_ContIndex") as ASPxComboBox;
                ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["QtyOrig"], "spin_Qty") as ASPxSpinEdit;
                ASPxSpinEdit spin_Weight = this.grid.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["WeightOrig"], "spin_Weight") as ASPxSpinEdit;
                ASPxSpinEdit spin_Volume = this.grid.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["VolumeOrig"], "spin_Volume") as ASPxSpinEdit;
                e.Result = update_house(SafeValue.SafeInt(lbl_Id.Text, 0), date_ShipDate.Date, txt_ShipIndex.Text, txt_ShipPortCode.Text, cbb_ContIndex.Text, spin_Qty.Value, spin_Weight.Value, spin_Volume.Value);
                #endregion
            }
            if (ar[0].Equals("CargoListline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                e.Result = txt_Id.Text;
                #endregion
            }

            if (ar[0].Equals("Pickline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;
                
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                string jobNo = GetJobNo();
                if (jobNo.Length>0)
                {
                    e.Result = create_house(0, rowIndex, jobNo);
                }
                else {
                    e.Result = "NO Export";
                }
                #endregion
            }
        }
    }
    private string GetJobNo() {
        string jobDate = date_Assign.Date.ToString("yyyyMMdd");
        string pol = txt_Pol.Text;
        string userId = HttpContext.Current.User.Identity.Name;
        string sql = string.Format(@"select JobNo from ctm_job where CONVERT(varchar(100), JobDate, 112)='{0}' and JobType='EXP' and Pol='{1}'", jobDate,pol);
        return SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
    }
    private string update_house(int id,  DateTime shipDate, string shipIndex
        , string portCode, string contIndex, object qty, object weight, object volume)
    {
        string result = "";
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + id + "");
        C2.JobHouse house = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
        if (house != null)
        {
            #region

            house.ShipDate = shipDate;
            house.ShipIndex =SafeValue.SafeInt(shipIndex,0);
            house.ShipPortCode = portCode;
            house.ContIndex = contIndex;
            house.QtyOrig = SafeValue.SafeDecimal(qty);
            house.WeightOrig = SafeValue.SafeDecimal(weight);
            house.VolumeOrig = SafeValue.SafeDecimal(volume);
            C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(house);
            result = "Save Success!";
            #endregion
        }

        return result;
    }
    private string create_house(int count,int index,string jobNo) {

        string result = "";
        DateTime now = DateTime.Now;
        
        string refNo = "";
        bool action = false;
        string userId = HttpContext.Current.User.Identity.Name;
        if (count > 0 || index > -1)
        {
            #region 处理逻辑

        
            for (int i = 0; i < count; i++)
            {
                int id = list[i].id;
                int shipIndex = list[i].shipIndex;
                DateTime shipDate = list[i].shipDate;
                string postCode = list[i].postCode;
                string contIndex = list[i].contIndex;
                decimal qty = list[i].qty;
                decimal weight = list[i].weight;
                decimal volume = list[i].volume;
                insert_data(jobNo,refNo,id,shipIndex,shipDate,postCode,contIndex,qty,weight,volume);
            }

            if (index > -1)
            {
                #region

                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(index, null, "txt_Id") as ASPxTextBox;
                ASPxDateEdit date_ShipDate = this.grid.FindRowCellTemplateControl(index, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipDate"], "date_ShipDate") as ASPxDateEdit;
                ASPxTextBox txt_ShipIndex = this.grid.FindRowCellTemplateControl(index, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipIndex"], "txt_ShipIndex") as ASPxTextBox;
                ASPxTextBox txt_ShipPortCode = this.grid.FindRowCellTemplateControl(index, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipPortCode"], "txt_ShipPortCode") as ASPxTextBox;
                ASPxComboBox cbb_ContIndex = this.grid.FindRowCellTemplateControl(index, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContIndex"], "cbb_ContIndex") as ASPxComboBox;
                ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(index, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["QtyOrig"], "spin_Qty") as ASPxSpinEdit;
                ASPxSpinEdit spin_Weight = this.grid.FindRowCellTemplateControl(index, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["WeightOrig"], "spin_Weight") as ASPxSpinEdit;
                ASPxSpinEdit spin_Volume = this.grid.FindRowCellTemplateControl(index, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["VolumeOrig"], "spin_Volume") as ASPxSpinEdit;
                int id = SafeValue.SafeInt(txt_Id.Text, 0);
                int shipIndex = SafeValue.SafeInt(txt_ShipIndex.Text, 0);
                DateTime shipDate = date_ShipDate.Date;
                string postCode = txt_ShipPortCode.Text;
                string contIndex = SafeValue.SafeString(cbb_ContIndex.Value);
                decimal qty = SafeValue.SafeDecimal(spin_Qty.Value);
                decimal weight = SafeValue.SafeDecimal(spin_Weight.Value);
                decimal volume = SafeValue.SafeDecimal(spin_Volume.Value);
                insert_data(jobNo, refNo, id, shipIndex, shipDate, postCode, contIndex, qty, weight, volume);

                #endregion
            }
            #endregion
        }
        else
        {
            result = "Pls Select at least one";
        }
        return result;
    }
    private void insert_data(string jobNo, string refNo, int id, int shipIndex, DateTime shipDate,string postCode,string contIndex,decimal qty,decimal weight,decimal volume)
    {
        string contNo = "C_" + contIndex;
        string sql = string.Format(@"select count(*) from ctm_jobdet1 where JobNo='{0}' and ContainerNo='{1}'", jobNo, contNo);
        int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
        if (n == 0)
        {
            create_cont(jobNo, contNo, shipDate);
        }
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + id + "");
        C2.JobHouse house = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
        if (house != null)
        {
            #region
            refNo = house.JobNo;
            house.OpsType = "Export";
            house.LineId = id;
            house.JobType = "EXP";
            house.JobNo = jobNo;
            house.RefNo = refNo;
            house.CargoType = "OUT";
            house.CargoStatus = "P";
            house.QtyOrig = qty;
            house.WeightOrig = weight;
            house.VolumeOrig = volume;
            house.ShipDate = shipDate;
            house.ShipIndex = shipIndex;
            house.ShipPortCode = postCode;
            house.ContIndex = contNo;
            house.ContNo = house.ContIndex;
            C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(house);

            #endregion
        }
        ObjectQuery query1 = new ObjectQuery(typeof(C2.JobStock), "OrderId=" + id + "", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query1);
        if (objSet.Count > 0)
        {
            for (int j = 0; j < objSet.Count; j++)
            {
                C2.JobStock s = objSet[j] as C2.JobStock;
                s.OrderId = house.Id;
                s.LineId = id;
                C2.Manager.ORManager.StartTracking(s, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(s);
            }
        }
    }
    private void create_cont(string jobNo, string contNo, DateTime date)
    {
        C2.CtmJobDet1 det1 = new C2.CtmJobDet1();
        det1.JobNo = jobNo;
        det1.ContainerNo = contNo;
        det1.StatusCode = "New";
        det1.ScheduleDate = date;

        C2.Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(det1);


        C2.CtmJobDet2 det2 = new C2.CtmJobDet2();
        det2.JobNo = jobNo;
        det2.ContainerNo = contNo;
        det2.Det1Id = det1.Id;
        det2.TripCode = "EXP";
        det2.Statuscode = "S";
        C2.Manager.ORManager.StartTracking(det2, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(det2);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id { get; set; }
        public int shipIndex { get; set; }
        public DateTime shipDate { get; set; }
        public string postCode { get; set; }
        public string contIndex { get; set; }
        public decimal qty { get; set; }
        public decimal weight { get; set; }
        public decimal volume { get; set; }
        public string contNo { get; set; }
        public Record(int _id, int _shipIndex, DateTime _shipDate, string _postCode, string _contIndex, decimal _qty, decimal _weight, decimal _volume)
        {
            id = _id;
            shipIndex = _shipIndex;
            shipDate = _shipDate;
            postCode = _postCode;
            contIndex = _contIndex;
            qty = _qty;
            weight = _weight;
            volume = _volume;
        }
        public Record(int _id, string _contNo)
        {
            id = _id;
            contNo = _contNo;
        }
    }
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 1000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxLabel Id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "lbl_Id") as ASPxLabel;
            ASPxDateEdit date_ShipDate = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipDate"], "date_ShipDate") as ASPxDateEdit;
            ASPxTextBox txt_ShipIndex = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipIndex"], "txt_ShipIndex") as ASPxTextBox;
            ASPxTextBox txt_ShipPortCode = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ShipPortCode"], "txt_ShipPortCode") as ASPxTextBox;
            ASPxComboBox cbb_ContIndex = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContIndex"], "cbb_ContIndex") as ASPxComboBox;
            ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["QtyOrig"], "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit spin_Weight = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["WeightOrig"], "spin_Weight") as ASPxSpinEdit;
            ASPxSpinEdit spin_Volume = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["VolumeOrig"], "spin_Volume") as ASPxSpinEdit;

            if (Id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(Id.Text, 0), SafeValue.SafeInt(txt_ShipIndex.Value, 0), date_ShipDate.Date, txt_ShipPortCode.Text, SafeValue.SafeString(cbb_ContIndex.Value), SafeValue.SafeDecimal(spin_Qty.Value), SafeValue.SafeDecimal(spin_Weight.Value), SafeValue.SafeDecimal(spin_Volume.Value)));
            }
            else
            {
                if (Id == null)
                {
                    break;
                }
            }
        }
    }   
    protected void txt_ShipPortCode_Init(object sender, EventArgs e)
    {
        ASPxTextBox txt = sender as ASPxTextBox;
        ASPxGridView grid = sender as ASPxGridView;
        GridViewDataItemTemplateContainer container = txt.NamingContainer as GridViewDataItemTemplateContainer;

        txt.Text = "SGSIN";

    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
}