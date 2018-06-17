using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wilson.ORMapper;

public partial class Modules_Freight_Job_AssignContainerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_search_dateFrom.Date = DateTime.Now;
            txt_search_dateTo.Date = DateTime.Now.AddDays(7);
            //date_ClearDate.Date = DateTime.Now;
            if (Request.QueryString["type"] != null)
            {
                lbl_JobType.Text = SafeValue.SafeString(Request.QueryString["type"]);
                if (lbl_JobType.Text == "IMP")
                {
                    lbl_CargoType.Text = "IN";
                    btn_Export.Visible = false;
                    btn_assign1.Visible = false;
                }
                else if (lbl_JobType.Text == "EXP")
                {
                    lbl_CargoType.Text = "OUT";
                    btn_Import.Visible = false;
                    btn_assign2.Visible = false;
                }
                else
                {
                    btn_Export.Visible = false;
                    btn_Import.Visible = false;
                }
            }
            else
            {
                btn_Export.Visible = false;
                btn_Import.Visible = false;
            }
        }
        //OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (lbl_JobType.Text != "")
        {
            where += string.Format(@" job.JobType='{0}'", lbl_JobType.Text);
        }
        string from = "";
        string to = "";
        string clearDateFrom = "";
        string clearDateTo = "";
        string sql = string.Format(@"select Job.Id as JobId,job.JobNo,det1.Id,det1.ContainerNo,(case when det1.ScheduleDate<getdate()  then getdate()+1 else det1.ScheduleDate end) as ScheduleDate,det1.SealNo,det1.ContainerType,job.JobType
,(case when det1.CustomsClearDate<getdate() then getdate() else det1.CustomsClearDate end) as CustomsClearDate,CustomsVerifyInd,(case when det1.CustomsVerifyDate<getdate() then getdate() else det1.CustomsVerifyDate end) as CustomsVerifyDate,CustomsRemark,CustomsClearStatus
,job.Vessel,job.Voyage,job.JobDate from CTM_JobDet1 det1 inner join ctm_job job on det1.JobNo=job.JobNo  ");
        if (txt_search_jobNo.Text != "")
        {
            where = GetWhere(where, string.Format(@" det1.JobNo='{0}'", txt_search_jobNo.Text));
        }
        if (txt_ContNo.Text != "")
        {
            where = GetWhere(where, string.Format(@" det1.ContainerNo='{0}'", txt_ContNo.Text));
        }
        else
        {
            if (txt_search_dateFrom.Date != null && txt_search_dateTo.Date != null)
            {
                from = txt_search_dateFrom.Date.ToString("yyyyMMdd");
                to = txt_search_dateTo.Date.ToString("yyyyMMdd");
            }
            if ( date_ClearDate.Date>new DateTime(1900, 1, 1))
            {
                clearDateFrom = date_ClearDate.Date.AddDays(-1).ToString("yyyyMMdd");
                clearDateTo = date_ClearDate.Date.AddDays(1).ToString("yyyyMMdd");
            }
            if (cmb_CustomsClearStatus.Text != "")
            {
                where = GetWhere(where, string.Format(@" det1.CustomsClearStatus='{0}'", cmb_CustomsClearStatus.Text));
            }
            if (txt_Vessel.Text != "")
            {
                where = GetWhere(where, string.Format(@" job.Vessel='{0}'", txt_Vessel.Text));
            }
            if (from.Length > 0 && to.Length > 0)
            {
                where = GetWhere(where, string.Format(@" '{0}'<ScheduleDate and ScheduleDate<'{1}'", from, to));
            }
            if (clearDateFrom.Length > 0 && clearDateTo.Length>0)
            {
                where = GetWhere(where, string.Format(@" '{0}'<ScheduleDate and CustomsClearDate<'{1}'", clearDateFrom, clearDateTo));
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

        if (ar.Length >= 2)
        {
            if (ar[0].Equals("Saveline"))
            {
                #region
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_Id = this.grid.FindRowCellTemplateControl(rowIndex, null, "lbl_Id") as ASPxLabel;
                ASPxLabel lbl_JobNo = this.grid.FindRowCellTemplateControl(rowIndex, null, "lbl_JobNo") as ASPxLabel;
                ASPxComboBox txt_ContNo = this.grid.FindRowCellTemplateControl(rowIndex, null, "cbbContainer") as ASPxComboBox;
                ASPxLabel lbl_ContIndex = this.grid.FindRowCellTemplateControl(rowIndex, null, "lbl_ContIndex") as ASPxLabel;
                ASPxDateEdit date_ScheduleDate = this.grid.FindRowCellTemplateControl(rowIndex, null, "date_ScheduleDate") as ASPxDateEdit;
                ASPxTextBox txt_SealNo = this.grid.FindRowCellTemplateControl(rowIndex, null, "txt_SealNo") as ASPxTextBox;
                ASPxComboBox cbbContType = this.grid.FindRowCellTemplateControl(rowIndex, null, "cbbContType") as ASPxComboBox;
                ASPxDateEdit date_CustomsClearDate = this.grid.FindRowCellTemplateControl(rowIndex, null, "date_CustomsClearDate") as ASPxDateEdit;
                ASPxComboBox cmb_CustomsClearStatus = this.grid.FindRowCellTemplateControl(rowIndex, null, "cmb_CustomsClearStatus") as ASPxComboBox;
                ASPxComboBox cmb_CustomsVerifyInd = this.grid.FindRowCellTemplateControl(rowIndex, null, "cmb_CustomsVerifyInd") as ASPxComboBox;
                ASPxDateEdit date_CustomsVerifyDate = this.grid.FindRowCellTemplateControl(rowIndex, null, "date_CustomsVerifyDate") as ASPxDateEdit;
                ASPxMemo memo_CustomsRemark = this.grid.FindRowCellTemplateControl(rowIndex, null, "memo_CustomsRemark") as ASPxMemo;
                e.Result = update_cont(SafeValue.SafeInt(lbl_Id.Text,0),lbl_JobNo.Text,txt_ContNo.Text,txt_SealNo.Text,cbbContType.Text,date_ScheduleDate.Date,
                    lbl_ContIndex.Text,cmb_CustomsClearStatus.Text,date_CustomsClearDate.Date,cmb_CustomsVerifyInd.Text,date_CustomsVerifyDate.Date,memo_CustomsRemark.Text);
                #endregion
            }
            if (ar[0].Equals("Ediline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_JobId = this.grid.FindRowCellTemplateControl(rowIndex, null, "lbl_JobId") as ASPxLabel;
                ASPxComboBox cmb_CustomsClearStatus = this.grid.FindRowCellTemplateControl(rowIndex, null, "cmb_CustomsClearStatus") as ASPxComboBox;
                ASPxComboBox cmb_CustomsVerifyInd = this.grid.FindRowCellTemplateControl(rowIndex, null, "cmb_CustomsVerifyInd") as ASPxComboBox;
                if (SafeValue.SafeString(cmb_CustomsClearStatus.Value) == "Y")
                {
                    e.Result = C2.Edi_Freight.DoOne(lbl_JobId.Text);
                    DateTime now = DateTime.Now;
                    string userId = HttpContext.Current.User.Identity.Name;
                    Session["Export_" + now.ToString("yyyyMMdd") + userId] = null;
                }
                else {
                    e.Result = "Edi Error! Pls confirm the Customs Clear Status";
                }
                
            }
        }
    }
    public string IsEdi(object jobNo) {
        string res = "N";
        string sql = string.Format(@"select count(*) from ctm_job where ClientRefNo='{0}'",jobNo);
        int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
        if (n > 0)
        {
            res= "Y";
        }
        return res;
    }
    private string update_cont(int id, string jobNo, string contNo, string sealNo, string contType, DateTime schedule, string contIndex
        , string clearStatus, DateTime clearDate, string verify, DateTime verifyDate, string remark)
    {
        string result = "";
        #region list
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "JobNo='" + jobNo + "' and ContNo='" + contIndex + "'");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count > 0)
        {
            #region
            for (int j = 0; j < objSet.Count; j++)
            {
                C2.JobHouse house = objSet[j] as C2.JobHouse;

                house.ContNo = contNo;
                house.OpsType = "Export";
                house.CargoStatus = "C";

                C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(house);
            }
            #endregion
        }
        string sql = "";
        List<ConnectSql_mb.cmdParameters> cmd = new List<ConnectSql_mb.cmdParameters>();

        sql = string.Format(@"update ctm_jobdet1 set ContainerNo=@ContainerNo,SealNo=@SealNo,
ContainerType=@ContainerType,ScheduleDate=@ScheduleDate,StatusCode=@StatusCode,CustomsClearDate=@CustomsClearDate,
CustomsClearStatus=@CustomsClearStatus,CustomsVerifyInd=@CustomsVerifyInd,CustomsVerifyDate=@CustomsVerifyDate,CustomsRemark=@CustomsRemark
where JobNo=@JobNo and ContainerNo=@ContNo");
        cmd = new List<ConnectSql_mb.cmdParameters>();
        DateTime dt=new DateTime(2000,1,1);
        if (schedule < dt)
            schedule = dt;
        if (clearDate < dt)
            clearDate = dt;
        if (verifyDate < dt)
            verifyDate = dt;
        cmd.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", contNo, SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@SealNo", sealNo, SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@ContainerType", contType, SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@ContNo", contIndex, SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@StatusCode", "Export", SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@ScheduleDate", schedule, SqlDbType.DateTime));
        cmd.Add(new ConnectSql_mb.cmdParameters("@CustomsClearDate", clearDate, SqlDbType.DateTime));
        cmd.Add(new ConnectSql_mb.cmdParameters("@CustomsClearStatus", clearStatus, SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@CustomsVerifyInd", verify, SqlDbType.NVarChar));
        cmd.Add(new ConnectSql_mb.cmdParameters("@CustomsVerifyDate", verifyDate, SqlDbType.DateTime));
        cmd.Add(new ConnectSql_mb.cmdParameters("@CustomsRemark", remark, SqlDbType.NVarChar));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, cmd);
        if (res.status)
        {
            result = "Save Success!";

        }

        #endregion
        return result;
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
        public string jobNo { get; set; }
        public string customsClearStatus { get; set; }
        public DateTime customsClearDate { get; set; }
        public string customsVerifyInd { get; set; }
        public DateTime customsVerifyDate { get; set; }
        public string customsRemark { get; set; }
        public string contNo { get; set; }
        public string contType { get; set; }
        public string sealNo { get; set; }
        public DateTime schedule { get; set; }
        public string contIndex { get; set; }
        public Record(int _id,string _jobNo, string _contNo,DateTime _schedule,string _sealNo,string _contType,string _customsClearStatus,DateTime _customsClearDate,
            string _customsVerifyInd,DateTime _customsVerifyDate,string _customsRemark,string _contIndex)
        {
            id = _id;
            jobNo = _jobNo;
            contNo = _contNo;
            schedule = _schedule;
            sealNo = _sealNo;
            contType = _contType;
            customsClearStatus = _customsClearStatus;
            customsClearDate = _customsClearDate;
            customsVerifyInd = _customsVerifyInd;
            customsVerifyDate = _customsVerifyDate;
            customsRemark = _customsRemark;
            contIndex = _contIndex;
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
            ASPxLabel JobNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["JobNo"], "lbl_JobNo") as ASPxLabel;
            ASPxComboBox txt_ContNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContainerNo"], "cbbContainer") as ASPxComboBox;
            ASPxLabel lbl_ContIndex = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContainerNo"], "lbl_ContIndex") as ASPxLabel;
            ASPxDateEdit date_ScheduleDate = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ScheduleDate"], "date_ScheduleDate") as ASPxDateEdit;
            ASPxTextBox txt_SealNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["SealNo"], "txt_SealNo") as ASPxTextBox;
            ASPxComboBox cbbContType = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContainerType"], "cbbContType") as ASPxComboBox;
            ASPxDateEdit date_CustomsClearDate = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["CustomsClearDate"], "date_CustomsClearDate") as ASPxDateEdit;
            ASPxComboBox cmb_CustomsClearStatus = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["CustomsClearStatus"], "cmb_CustomsClearStatus") as ASPxComboBox;
            ASPxComboBox cmb_CustomsVerifyInd = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["CustomsVerifyInd"], "cmb_CustomsVerifyInd") as ASPxComboBox;
            ASPxDateEdit date_CustomsVerifyDate = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["CustomsVerifyDate"], "date_CustomsVerifyDate") as ASPxDateEdit;
            ASPxMemo memo_CustomsRemark = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["CustomsRemark"], "memo_CustomsRemark") as ASPxMemo;
            if (Id != null)
            {
                list.Add(new Record(SafeValue.SafeInt(Id.Text,0),JobNo.Text,txt_ContNo.Text,date_ScheduleDate.Date,txt_SealNo.Text,SafeValue.SafeString(cbbContType.Value),
                    SafeValue.SafeString(cmb_CustomsClearStatus.Value),date_CustomsClearDate.Date,SafeValue.SafeString(cmb_CustomsVerifyInd.Value),
                    date_CustomsVerifyDate.Date,memo_CustomsRemark.Text,lbl_ContIndex.Text));
                insert_container(txt_ContNo.Text, SafeValue.SafeString(cbbContType.Value));
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
    private void insert_container(string contNo,string contType) {
        string sql = string.Format(@"select count(*) from Ref_Container where ContainerNo='{0}'",contNo);
        int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
        if (n == 0) {
            sql = string.Format(@"insert into Ref_Container(ContainerNo,ContainerType) values('{0}','{1}')",contNo,contType);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
    }
    protected void txt_ShipPortCode_Init(object sender, EventArgs e)
    {
        ASPxTextBox txt = sender as ASPxTextBox;
        ASPxGridView grid = sender as ASPxGridView;
        GridViewDataItemTemplateContainer container = txt.NamingContainer as GridViewDataItemTemplateContainer;

        txt.Text = "SGSIN";

    }

}