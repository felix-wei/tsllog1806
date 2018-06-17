using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;


public partial class WareHouse_Job_JobSchedule : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            this.txt_end.Date = DateTime.Today;
			string d = Request.QueryString["d"] ?? "";
			if(d != "")
			{
            this.txt_from.Date = DateTime.Parse(d);
            this.txt_end.Date = DateTime.Parse(d);
			}
            btn_search_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
        else
        {
            string where = "";
            where += "CONVERT(varchar(100), JobDate, 23) between '" + txt_date.Date.ToString("yyyy-MM-dd")
               + "' and '" + txt_date2.Date.ToString("yyyy-MM-dd") + "'";
            this.dsJobSchedule.FilterExpression = where + "and (WorkStatus<>'Cancel' and WorkStatus<>'Unsuccess')";
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string where = "";
        //string sql = string.Format(@"select * from  JobSchedule ");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0)
        {
            where = GetWhere(where, " JobDate>= '" + dateFrom + "' and JobDate<= '" + dateTo + "'");
        }
        if (where.Length > 0)
        {
            where += " Order by JobNo";
        }
        //throw new Exception(sql);
        dsJobSchedule.FilterExpression = where;
        //btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("JobSchedule", true);
    }	
    protected void btn_search_Click(object sender, EventArgs e)
    {
        
        string where = "";
        //string sql = string.Format(@"select * from  JobSchedule ");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(0).ToString("yyyy-MM-dd");
            txt_date.Date = txt_from.Date;
            txt_date2.Date = txt_end.Date;
        }
        if (dateFrom.Length > 0)
        {
            where = GetWhere(where, " JobDate>= '" + dateFrom + " 00:00' and JobDate<= '" + dateTo + " 23:59' ");
        }
        if (where.Length > 0)
        {
            where += " and (WorkStatus<>'Cancel' and WorkStatus<>'Unsuccess') and len(RefNo)>0 ";
        }
        //throw new Exception(sql);
        dsJobSchedule.FilterExpression = where;
        //DataTable tab = ConnectSql.GetTab(sql);
        //this.grid.DataSource = tab;
        //this.grid.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string id = "";
        string sql = "";
        string s = e.Parameters;
        if (s.Contains("Copy"))
        {
            id = e.Parameters.Replace("Copy", "");
            SaveNewSch(id);
            e.Result = "Success!";

        }
    }
    protected string SaveNewSch(string id)
    {
        try
        {
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(JobSchedule), "Id='" + id + "'");
            JobSchedule jobSch = C2.Manager.ORManager.GetObject(query) as JobSchedule;
            string issueN = "";
            JobSchedule job = new JobSchedule();
            DateTime dt = DateTime.Now;
            issueN = C2Setup.GetNextSchNo("", "Schedule", dt);
            job.JobDate = dt;

            job.JobStage = jobSch.JobStage;
            job.JobType = jobSch.JobType;
            job.Mode = jobSch.Mode;
            job.ViaWh = jobSch.ViaWh;
            job.StorageStartDate = new DateTime(1900, 1, 1);
            job.StorageFreeDays = jobSch.StorageFreeDays; //new DateTime(1900,1,1);
            if (job.JobType == "Storage")
            {
                issueN = "SR-" + issueN;
                job.ViaWh = "Normal";
            }
            if (job.JobType == "Outbound")
            {
                issueN = "OB-" + issueN;
                job.Mode = "Lcl";

            }
            if (job.JobType == "Local Move")
            {
                issueN = "LM-" + issueN;
            }
            if (job.JobType == "Office Move")
            {
                issueN = "OM-" + issueN;
            }
            if (job.JobType == "Inbound")
            {
                issueN = "IB-" + issueN;
            }
            if (job.JobType == "Air")
            {
                issueN = "AR-" + issueN;
            }

            //Main Info
            job.CustomerId = jobSch.CustomerId;
            job.CustomerName = jobSch.CustomerName;
            job.CustomerAdd = jobSch.CustomerAdd;
            job.Postalcode = jobSch.Postalcode;
            job.Contact = jobSch.Contact;
            job.Tel = jobSch.Tel;
            job.Email = jobSch.Email;
            job.Fax = jobSch.Fax;
            job.Remark = jobSch.Remark;
            job.WorkStatus = jobSch.WorkStatus;
            job.Currency = jobSch.Currency;
            job.ExRate = jobSch.ExRate;
            job.PayTerm = jobSch.PayTerm;
            job.ExpiryDate = jobSch.ExpiryDate;
            job.PackRmk = jobSch.PackRmk;
            job.MoveRmk = jobSch.MoveRmk;

            job.WareHouseId = jobSch.WareHouseId;
            job.OriginPort = jobSch.OriginPort;
            job.DestinationPort = jobSch.DestinationPort;
            job.OriginAdd = jobSch.OriginAdd;
            job.DestinationAdd = jobSch.DestinationAdd;
            job.Volumne = jobSch.Volumne;
            job.ItemDes = jobSch.ItemDes;
            job.VolumneRmk = jobSch.VolumneRmk;
            job.HeadCount = jobSch.HeadCount;

            job.PackDate = jobSch.PackDate;
            job.StorageStartDate = jobSch.StorageStartDate;
            job.StorageFreeDays = jobSch.StorageFreeDays;
            job.StorageTotalDays = jobSch.StorageTotalDays;
            job.TripNo = jobSch.TripNo;
            job.MoveDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd") + " 09:00");
            job.Charges = jobSch.Charges;
            job.EntryPort = jobSch.EntryPort;
            job.Mode = jobSch.Mode;
            job.Eta = jobSch.Eta;
            job.TruckNo = jobSch.TruckNo;
            job.OriginCity = jobSch.OriginCity;
            job.DestCity = jobSch.DestCity;
            job.OriginPostal = jobSch.OriginPostal;
            job.DestPostal = jobSch.DestPostal;
            job.ServiceType = jobSch.ServiceType;
            //Additional

            job.Item1 = jobSch.Item1;
            job.Item2 = jobSch.Item2;
            job.ItemDetail1 = jobSch.ItemDetail1;
            job.ItemDetail2 = jobSch.ItemDetail2;

            job.Item3 = jobSch.Item3;
            job.ItemValue3 = jobSch.ItemValue3;
            job.ItemData3 = jobSch.ItemData3;
            job.ItemPrice3 = jobSch.ItemPrice3;

            job.Item4 = jobSch.Item4;
            job.ItemDetail4 = jobSch.ItemDetail4;
            job.ItemPrice4 = jobSch.ItemPrice4;

            job.Item5 = jobSch.Item5;
            job.ItemValue5 = jobSch.ItemValue5;
            job.ItemPrice5 = jobSch.ItemPrice5;

            job.Item6 = jobSch.Item6;
            job.ItemDetail6 = jobSch.ItemDetail6;
            job.ItemPrice6 = jobSch.ItemPrice6; ;

            job.Item7 = jobSch.Item7;
            job.ItemValue7 = jobSch.ItemValue7;
            job.ItemDetail7 = jobSch.ItemDetail7;
            job.ItemPrice7 = jobSch.ItemPrice7;

            job.Item8 = jobSch.Item8;
            job.ItemValue8 = jobSch.ItemValue8;
            job.ItemDetail8 = jobSch.ItemDetail8;
            job.ItemPrice8 = jobSch.ItemPrice8;

            job.Item9 = jobSch.Item9;
            job.ItemValue9 = jobSch.ItemValue9;
            job.ItemDetail9 = jobSch.ItemDetail9;
            job.ItemPrice9 = jobSch.ItemPrice9;

            job.Item10 = jobSch.Item10;
            job.ItemValue10 = jobSch.ItemValue10;
            job.ItemDetail10 = jobSch.ItemDetail10;
            job.ItemPrice10 = jobSch.ItemPrice10;

            job.Item11 = jobSch.Item11;
            job.ItemDetail11 = jobSch.ItemDetail11;

            job.Item12 = jobSch.Item12;
            job.ItemDetail12 = jobSch.ItemDetail12;

            job.Item13 = jobSch.Item13;
            job.ItemValue13 = jobSch.ItemValue13;
            job.ItemData13 = jobSch.ItemData13;

            job.Item14 = jobSch.Item14;
            job.ItemValue14 = jobSch.ItemValue14;
            job.ItemDetail14 = jobSch.ItemDetail14;
            job.ItemPrice14 = jobSch.ItemPrice14;

            job.Answer1 = jobSch.Answer1;
            job.Answer2 = jobSch.Answer2;
            job.Answer3 = jobSch.Answer3;
            job.Answer4 = jobSch.Answer4;
            job.WorkStatus = jobSch.WorkStatus;



            //Quotation
            job.Attention1 = jobSch.Attention1;
            job.Attention2 = jobSch.Attention2;
            job.Attention3 = jobSch.Attention3;
            job.Attention4 = jobSch.Attention4;
            job.Attention5 = jobSch.Attention5;
            job.DateTime2 = jobSch.DateTime2;
            job.DateTime1 = jobSch.DateTime1;
            job.DateTime3 = jobSch.DateTime3;
            job.DateTime4 = jobSch.DateTime4;
            //job.DateTime5 = jobSch.DateTime5;
            //job.DateTime6 = jobSch.DateTime6;
            //job.DateTime7 = jobSch.DateTime7;
            //job.DateTime8 = jobSch.DateTime8;
            job.Value1 = jobSch.Value1;
            job.Value2 = jobSch.Value2;
            job.Value3 = jobSch.Value3;
            job.Value4 = jobSch.Value4;
            //job.Value5 = jobSch.Value5;
            //job.Value6 = jobSch.Value6;
            //job.Value7 = jobSch.Value7;
            //job.Value8 = jobSch.Value8;
            job.Notes = jobSch.Notes;


            string userId = HttpContext.Current.User.Identity.Name;
            job.RefNo = jobSch.RefNo;
            job.CreateBy = userId;
            job.CreateDateTime = DateTime.Now;
            job.UpdateBy = userId;
            job.UpdateDateTime = DateTime.Now;
            job.JobNo = issueN;
            job.DateTime1 = DateTime.Now;
            job.CustomerName = jobSch.CustomerName;
            job.OriginAdd = jobSch.OriginAdd;
            job.DestinationAdd = jobSch.DestinationAdd;
            job.PackRmk = jobSch.PackRmk;
            job.VolumneRmk = jobSch.VolumneRmk;
            job.Contact = jobSch.Contact;
            Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(job);
            C2Setup.SetNextNo("", "Schedule", issueN, DateTime.Now);

            #region Item List
            string sql = string.Format(@"SELECT  Id, RefNo, JobNo, ItemType, ItemName, ItemQty, ItemValue, ItemMark, ItemNote, CreateBy, CreateDateTime, 
                UpdateBy, UpdateDateTime FROM JobItemList where RefNo='{0}'", jobSch.JobNo);
            DataTable tab = ConnectSql.GetTab(sql);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                JobItemList list = new JobItemList();

                list.RefNo = job.JobNo;
                list.ItemName = SafeValue.SafeString(tab.Rows[i]["ItemName"]);

                list.ItemNote = SafeValue.SafeString(tab.Rows[i]["ItemNote"]);
                list.ItemMark = SafeValue.SafeString(tab.Rows[i]["ItemMark"]);
                list.ItemQty = SafeValue.SafeInt(tab.Rows[i]["ItemQty"], 0);
                list.ItemValue = SafeValue.SafeDecimal(tab.Rows[i]["ItemValue"]);
                list.ItemType = SafeValue.SafeString(tab.Rows[i]["ItemType"]);
                list.CreateBy = EzshipHelper.GetUserName();
                list.CreateDateTime = DateTime.Now;
                list.JobNo = SafeValue.SafeString(tab.Rows[i]["JobNo"]);
                Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(list);

            }
            #endregion
            
            #region JobMCST
            sql = string.Format(@"SELECT Id, RefNo, McstNo, McstDate1, McstDate2, States, McstRemark1, McstRemark2, Amount1, Amount2, CondoTel, 
                McstRemark3, CreateBy, CreateDateTime, UpdateBy, UpdateDateTime FROM JobMCST where RefNo='{0}'", jobSch.JobNo);
            DataTable tab_mcst = ConnectSql.GetTab(sql);
            for (int i = 0; i < tab_mcst.Rows.Count; i++)
            {
                JobMCST list = new JobMCST();
                list.RefNo = job.JobNo;
                list.McstNo = SafeValue.SafeString(tab_mcst.Rows[i]["McstNo"]);
                list.McstDate1 = SafeValue.SafeDate(tab_mcst.Rows[i]["McstDate1"], DateTime.Now);
                list.McstDate2 = SafeValue.SafeDate(tab_mcst.Rows[i]["McstDate2"], DateTime.Now);
                list.States = SafeValue.SafeString(tab_mcst.Rows[i]["States"]);
                list.McstRemark1 = SafeValue.SafeString(tab_mcst.Rows[i]["McstRemark1"]);
                list.McstRemark2 = SafeValue.SafeString(tab_mcst.Rows[i]["McstRemark2"]);
                list.CreateBy = SafeValue.SafeString(tab_mcst.Rows[i]["CreateBy"]);
                list.CreateDateTime = SafeValue.SafeDate(tab_mcst.Rows[i]["CreateDateTime"], DateTime.Now);
                list.UpdateBy = SafeValue.SafeString(tab_mcst.Rows[i]["UpdateBy"]);
                list.UpdateDateTime = SafeValue.SafeDate(tab_mcst.Rows[i]["UpdateDateTime"], DateTime.Now);
                list.CondoTel = SafeValue.SafeString(tab_mcst.Rows[i]["CondoTel"]);
                list.McstRemark3 = SafeValue.SafeString(tab_mcst.Rows[i]["McstRemark3"]);
                list.Amount1 = SafeValue.SafeDecimal(tab_mcst.Rows[i]["Amount1"]);
                list.Amount2 = SafeValue.SafeDecimal(tab_mcst.Rows[i]["Amount2"]);
                Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(list);

            }
            #endregion
            #region Materials
            sql = string.Format(@"SELECT Id, Description, Unit, RequisitionNew, RequisitionUsed, AdditionalNew, AdditionalUsed, AdditionalNew1, 
                AdditionalUsed1, AdditionalNew2, AdditionalUsed2, ReturnedNew, ReturnedUsed, TotalNew, TotalUsed, RefNo, 
                RequisitionNew1, RequisitionUsed1, RequisitionNew2, RequisitionUsed2 FROM Materials where RefNo='{0}'", jobSch.JobNo);
            DataTable tab_material = ConnectSql.GetTab(sql);
            for (int i = 0; i < tab_material.Rows.Count; i++)
            {
                Material list = new Material();
                list.RefNo = job.JobNo;
                list.Description = SafeValue.SafeString(tab_material.Rows[i]["Description"]);
                list.Unit = SafeValue.SafeString(tab_material.Rows[i]["Unit"]);
                list.RequisitionNew = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew"], 0);
                list.RequisitionUsed = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed"], 0);
                list.AdditionalNew = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew"], 0);
                list.AdditionalUsed = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed"], 0);
                list.AdditionalNew1 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew1"], 0);
                list.AdditionalUsed1 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed1"], 0);
                list.AdditionalNew2 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalNew2"], 0);
                list.AdditionalUsed2 = SafeValue.SafeInt(tab_material.Rows[i]["AdditionalUsed2"], 0);
                list.ReturnedNew = SafeValue.SafeInt(tab_material.Rows[i]["ReturnedNew"], 0);
                list.ReturnedUsed = SafeValue.SafeInt(tab_material.Rows[i]["ReturnedUsed"], 0);
                list.RequisitionNew1 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew1"], 0);
                list.RequisitionUsed1 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed1"], 0);
                list.RequisitionNew2 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionNew2"], 0);
                list.RequisitionUsed2 = SafeValue.SafeInt(tab_material.Rows[i]["RequisitionUsed2"], 0);
                list.TotalNew = SafeValue.SafeInt(tab_material.Rows[i]["TotalNew"], 0);
                list.TotalUsed = SafeValue.SafeInt(tab_material.Rows[i]["TotalUsed"], 0);
                Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(list);

            }
            #endregion
            return job.JobNo;
        }
        catch { }
        return "";
    }
    protected void btn_CopySch_Init(object sender, EventArgs e)
    {
        ASPxButton btn_CopySch = sender as ASPxButton;
        GridViewDataItemTemplateContainer container = btn_CopySch.NamingContainer as GridViewDataItemTemplateContainer;

        btn_CopySch.ClientInstanceName = String.Format("btn_CopySch{0}", container.VisibleIndex);
        btn_CopySch.ClientSideEvents.Click = String.Format("function (s, e) {{ OnCopyClick(txtSId{0}.GetText()); }}", container.VisibleIndex);
    }
    protected void txt_Id_Init(object sender, EventArgs e)
    {
        ASPxTextBox txtId = sender as ASPxTextBox;
        GridViewDataItemTemplateContainer container = txtId.NamingContainer as GridViewDataItemTemplateContainer;

        txtId.ClientInstanceName = String.Format("txtSId{0}", container.VisibleIndex);
    }
    public string GetMaterials(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}'", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "NO MAT";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int totalNew = SafeValue.SafeInt(dt.Rows[i]["TotalNew"], 0);
            int totalUsed = SafeValue.SafeInt(dt.Rows[i]["TotalUsed"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);

            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            //string code = SafeValue.SafeString(dt.Rows[i]["Code"]);
            string code = D.Text("select top 1 code from materials where description='"+des+"' and refno='JO0001010001'");

            if (totalNew > 0)
            {
                result += totalNew + code + "(N)" + "<br />";
            }
            if (totalUsed > 0)
            {
                result += totalUsed + code + "(U)" + "<br />";
            }
        }
        return result;
    }
    public string GetCrews(string jobNo)
    {
        string sql = string.Format(@"select * from JobCrews where RefNo='{0}' order by Remark", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
		if(dt.Rows.Count == 0)
		{
			return D.Text(string.Format(@"select Remark2 from JobSchedule where RefNo='{0}'", jobNo));
		}
        string ValueStr = "";
        string result = "";
        string super = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string status = SafeValue.SafeString(dt.Rows[i]["Status"]);
            string name = SafeValue.SafeString(dt.Rows[i]["Remark"]);

            if (status == "Supervisor")
            {
                super = name;
            }
            if (status != "Supervisor")
            {
                result += name + ",";
            }
            if (super.Length > 0)
            {
                ValueStr = super + "/" + result;
            }
            else
            {
                ValueStr = result;
            }
        }
        return ValueStr;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["RefNo"] = SafeValue.SafeString(e.NewValues["RefNo"]);
        e.NewValues["OriginAdd"] = SafeValue.SafeString(e.NewValues["OriginAdd"]);
        e.NewValues["DestinationAdd"] = SafeValue.SafeString(e.NewValues["DestinationAdd"]);
        e.NewValues["CustomerName"] = SafeValue.SafeString(e.NewValues["CustomerName"]);
        e.NewValues["Contact"] = SafeValue.SafeString(e.NewValues["Contact"]);
        e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
        e.NewValues["Value1"] = SafeValue.SafeString(e.NewValues["Value1"]);
        e.NewValues["Value2"] = SafeValue.SafeString(e.NewValues["Value2"]);
        e.NewValues["Value3"] = SafeValue.SafeString(e.NewValues["Value3"]);
        e.NewValues["WorkStatus"] = SafeValue.SafeString(e.NewValues["WorkStatus"]);
        e.NewValues["JobDate"] = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        e.NewValues["MoveDate"] = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        e.NewValues["PackRmk"] = SafeValue.SafeString(e.NewValues["PackRmk"]);
        e.NewValues["MoveRmk"] = SafeValue.SafeString(e.NewValues["MoveRmk"]);
        e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
        e.NewValues["VolumneRmk"] = SafeValue.SafeString(e.NewValues["VolumneRmk"]);
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
        e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
        DateTime jobDate = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        ASPxTimeEdit date_Time = grid.FindEditRowCellTemplateControl(null, "date_Time") as ASPxTimeEdit;
        string jobTime = SafeValue.SafeDateStr(e.NewValues["JobDate"]);
        DateTime moveDate = DateTime.Parse(jobDate.ToString("yyyy-MM-dd") + " " + date_Time.Text);
        e.NewValues["MoveDate"] = SafeValue.SafeDate(moveDate, DateTime.Now);
        //e.NewValues["MoveDate"] = SafeValue.SafeDate(date_Time.Value, DateTime.Now);
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobSchedule));
        }
    }
}