using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Job_ScheduleEdit : System.Web.UI.Page
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            //this.txtSchNo.Focus();
            this.form1.Focus();
            Session["SchEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                Session["SchEditWhere"] = "JobNo='" + Request.QueryString["no"] + "'";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["SchEditWhere"] == null)
                {
                    this.grid_Issue.AddNewRow();
                }
            }
            else
                this.dsSchedule.FilterExpression = "1=0";
        }
        if (Session["SchEditWhere"] != null)
        {
            this.dsSchedule.FilterExpression = Session["SchEditWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region Schedule
    protected void grid_Issue_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.JobSchedule));
    }
    protected void grid_Issue_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string[] currentPeriod = EzshipHelper.GetAccPeriod(DateTime.Today);
        string acYear = currentPeriod[1];
        string acPeriod = currentPeriod[0];

        e.NewValues["RefNo"] = acYear;
        e.NewValues["AcPeriod"] = acPeriod;
        e.NewValues["DocDate"] = DateTime.Today;
        e.NewValues["DocDueDate"] = DateTime.Today;
        e.NewValues["DocType"] = "IV";
        e.NewValues["AcSource"] = "DB";
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = new decimal(1.0);
        e.NewValues["Term"] = "CASH";


        if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
        {
            string refNo = Request.QueryString["RefN"].ToString();
            string houseNo = Request.QueryString["JobN"].ToString();
            string jobType = Request.QueryString["JobType"].ToString();
            e.NewValues["MastRefNo"] = refNo;
            e.NewValues["JobRefNo"] = houseNo;
            e.NewValues["MastType"] = jobType;
            string sql = "SELECT Cust FROM  TPT_Job WHERE (JobNo= '" + refNo + "')";
            if (jobType == "TPT")
            {
                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                e.NewValues["Term"] = EzshipHelper.GetTerm(e.NewValues["PartyTo"].ToString());
            }
            if (houseNo.Length > 1)
            {
                if (jobType == "SI")
                    sql = "SELECT CustomerID FROM  SeaImport WHERE (JobNo= '" + houseNo + "')";
                if (jobType == "SE")
                    sql = "SELECT CustomerID FROM  SeaExport WHERE (JobNo= '" + houseNo + "')";
                if (jobType == "AI" || jobType == "AE" || jobType == "ACT")
                    sql = "SELECT CustomerID FROM  air_job WHERE (JobNo= '" + houseNo + "')";

                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                e.NewValues["Term"] = EzshipHelper.GetTerm(e.NewValues["PartyTo"].ToString());

                if (jobType == "SI")
                {
                    sql = "SELECT CollectCurrency, CollectExRate FROM  SeaImport WHERE (JobNo= '" + houseNo + "')";
                    DataTable tab_cur = Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab_cur.Rows.Count == 1)
                    {
                        e.NewValues["CurrencyId"] = tab_cur.Rows[0][0].ToString();
                        e.NewValues["ExRate"] = SafeValue.SafeDecimal(tab_cur.Rows[0][1], 0);
                    }
                }

            }
        }
    }
    protected void grid_Issue_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox txt_JobNo = grid_Issue.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
        string s = e.Parameters;
        //string[] _filter = filter.Split(',');
        //string p = _filter[0];
        //string s = _filter[1];
        ASPxTextBox docId = this.grid_Issue.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        if (s == "Save")
        {
            #region Save
            //if (txt_CustomerId.Text.Trim() == "")
            //{
            //    e.Result = "Fail! Please enter the Customer";
            //    return;
            //}
            //ASPxComboBox doStatus = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            //if (doStatus.Text == "Job Confirmation")
            //{
            //    //check purchase price and sell price
            //    string sql = string.Format(@"select count(SequenceId) from Cost where RefNo='{0}'", txt_JobNo.Text);
            //    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
            //    {
            //        e.Result = "Fail!No Costing";
            //        return;
            //    }
            //}
            if (txt_JobNo.Text.Length > 4)
            {
               SaveJob();
                e.Result = "";//update old one
            }
            else{
                 SaveJob();
                 e.Result = "";
            };// new one}
            #endregion
        }
        else if (s == "ITEM")
        {
            #region ITEM
            ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
            string sql = "";

            //if (cmb_JobType.Text !="")
            //{
                #region Local Move / International Move
                sql = string.Format(@"SELECT Id, JobNo, ItemType, ItemName, ItemQty, ItemValue, ItemMark, ItemNote, CreateBy, CreateDateTime, UpdateBy, UpdateDateTime
FROM JobItem");
                DataTable dt = ConnectSql.GetTab(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JobItemList list = new JobItemList();

                    list.RefNo = txt_JobNo.Text;
                    list.ItemName = SafeValue.SafeString(dt.Rows[i]["ItemName"]);

                    list.ItemNote = SafeValue.SafeString(dt.Rows[i]["ItemNote"]);
                    list.ItemMark = SafeValue.SafeString(dt.Rows[i]["ItemMark"]);
                    list.ItemQty = 0;
                    list.ItemValue = 0;
                    list.ItemType = SafeValue.SafeString(dt.Rows[i]["ItemType"]);
                    list.CreateBy = EzshipHelper.GetUserName();
                    list.CreateDateTime = DateTime.Now;
                    list.JobNo = SafeValue.SafeString(dt.Rows[i]["JobNo"]);
                    if (ViaItemName(list.ItemName, list.ItemType, SafeValue.SafeString(txt_JobNo.Text)) > 0)
                    {

                    }
                    else
                    {
                        Manager.ORManager.StartTracking(list, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(list);
                    }
               }
                 #endregion
          // }
            //else
           // {
          //      e.Result = "Fail!No data";
          //  }
            #endregion
        }
    }
    protected void grid_Issue_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
    }
    protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Issue.EditingRowVisibleIndex > -1)
        {

        }
    }
    protected void grid_Issue_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
    }
    protected void grid_Issue_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //SaveAndUpdate();
        e.Cancel = true;
    }
    protected string SaveJob()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox DoNo = grid_Issue.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            ASPxTextBox txt_Id = grid_Issue.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string pId = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(JobSchedule), "Id='" + pId + "'");
            JobSchedule job = C2.Manager.ORManager.GetObject(query) as JobSchedule;
            ASPxDateEdit issueDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            bool isNew = false;
            //const string runType = "DOOUT";
            string issueN = "";
            if (job == null)
            {
                job = new JobSchedule();
                isNew = true;
                issueN = C2Setup.GetNextNo("", "JobOrder", issueDate.Date);
                job.JobDate = issueDate.Date;
            }

            ASPxDateEdit doDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            if (doDate != null)
                job.JobDate = doDate.Date;

            ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            if (cmb_Status != null)
                job.JobStage = SafeValue.SafeString(cmb_Status.Value);
            ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
            if (cmb_JobType != null)
                job.JobType = SafeValue.SafeString(cmb_JobType.Value);
            //Main Info
            ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
            if (txt_CustomerId != null)
                job.CustomerId = txt_CustomerId.Text;
            ASPxTextBox txt_CustomerName = grid_Issue.FindEditFormTemplateControl("txt_CustomerName") as ASPxTextBox;
            if (txt_CustomerName != null)
                job.CustomerName = txt_CustomerName.Text;
            ASPxMemo memo_Address = grid_Issue.FindEditFormTemplateControl("memo_Address") as ASPxMemo;
            if (memo_Address != null)
                job.CustomerAdd = memo_Address.Text;

            ASPxTextBox txt_PostalCode = grid_Issue.FindEditFormTemplateControl("txt_PostalCode") as ASPxTextBox;
            if (txt_PostalCode != null)
                job.Postalcode = txt_PostalCode.Text;
            ASPxTextBox txt_Contact = grid_Issue.FindEditFormTemplateControl("txt_Contact") as ASPxTextBox;
            if (txt_Contact != null)
                job.Contact = txt_Contact.Text;
            ASPxTextBox txt_Tel = grid_Issue.FindEditFormTemplateControl("txt_Tel") as ASPxTextBox;
            if (txt_Tel != null)
                job.Tel = txt_Tel.Text;
            ASPxTextBox txt_Email = grid_Issue.FindEditFormTemplateControl("txt_Email") as ASPxTextBox;
            if (txt_Email != null)
                job.Email = txt_Email.Text;
            ASPxTextBox txt_Fax = grid_Issue.FindEditFormTemplateControl("txt_Fax") as ASPxTextBox;
            if (txt_Fax != null)
                job.Fax = txt_Fax.Text;
            ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            if (remark != null)
                job.Remark = remark.Text;
            ASPxButtonEdit currency = grid_Issue.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
            if (currency != null)
                job.Currency = currency.Text;
            ASPxSpinEdit exRate = grid_Issue.FindEditFormTemplateControl("spin_ExRate") as ASPxSpinEdit;
            if (exRate != null)
                job.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            ASPxComboBox payTerm = grid_Issue.FindEditFormTemplateControl("cmb_PayTerm") as ASPxComboBox;
            if (payTerm != null)
                job.PayTerm = payTerm.Text;
            ASPxDateEdit date_ExpiryDate = grid_Issue.FindEditFormTemplateControl("date_ExpiryDate") as ASPxDateEdit;
            if (date_ExpiryDate != null)
                job.ExpiryDate = date_ExpiryDate.Date;


            ASPxMemo txt_PackRemark = grid_Issue.FindEditFormTemplateControl("txt_PackRemark") as ASPxMemo;
            if (txt_PackRemark != null)
                job.PackRmk = txt_PackRemark.Text;
            ASPxMemo txt_MoveRemark = grid_Issue.FindEditFormTemplateControl("txt_MoveRemark") as ASPxMemo;
            if (txt_MoveRemark != null)
                job.MoveRmk = txt_MoveRemark.Text;

            ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            if (txt_WareHouseId != null)
                job.WareHouseId = txt_WareHouseId.Text;
            ASPxButtonEdit btn_OriginPort = grid_Issue.FindEditFormTemplateControl("btn_OriginPort") as ASPxButtonEdit;
            if (btn_OriginPort != null)
                job.OriginPort = btn_OriginPort.Text;
            ASPxButtonEdit btn_DestinationPort = grid_Issue.FindEditFormTemplateControl("btn_DestinationPort") as ASPxButtonEdit;
            if (btn_DestinationPort != null)
                job.DestinationPort = btn_DestinationPort.Text;
            ASPxMemo memo_Address1 = grid_Issue.FindEditFormTemplateControl("memo_Address1") as ASPxMemo;
            if (memo_Address1 != null)
                job.OriginAdd = memo_Address1.Text;
            ASPxMemo memo_Address2 = grid_Issue.FindEditFormTemplateControl("memo_Address2") as ASPxMemo;
            if (memo_Address2 != null)
                job.DestinationAdd = memo_Address2.Text;
            ASPxSpinEdit spin_Volumne = grid_Issue.FindEditFormTemplateControl("spin_Volumne") as ASPxSpinEdit;
            if (spin_Volumne != null)
                job.Volumne = SafeValue.SafeDecimal(spin_Volumne.Value);
            ASPxMemo memo_Description = grid_Issue.FindEditFormTemplateControl("memo_Description") as ASPxMemo;
            if (memo_Description != null)
                job.ItemDes = memo_Description.Text;
            ASPxTextBox memo_Volumn = grid_Issue.FindEditFormTemplateControl("memo_Volumn") as ASPxTextBox;
            if (memo_Volumn != null)
                job.VolumneRmk = memo_Description.Text;
            ASPxSpinEdit spin_HeadCount = grid_Issue.FindEditFormTemplateControl("spin_HeadCount") as ASPxSpinEdit;
            if (spin_HeadCount != null)
                job.HeadCount = SafeValue.SafeInt(spin_HeadCount.Value, 0);

            ASPxDateEdit date_Pack = grid_Issue.FindEditFormTemplateControl("date_Pack") as ASPxDateEdit;
            if (date_Pack != null)
                job.PackDate = date_Pack.Date;
            ASPxComboBox cmb_Via = grid_Issue.FindEditFormTemplateControl("cmb_Via") as ASPxComboBox;
            if (cmb_Via != null)
                job.ViaWh = cmb_Via.Text;
            ASPxDateEdit date_StorageStartDate = grid_Issue.FindEditFormTemplateControl("date_StorageStartDate") as ASPxDateEdit;
            if (date_StorageStartDate != null)
                job.StorageStartDate = date_StorageStartDate.Date;
            ASPxSpinEdit spin_StorageFreeDays = grid_Issue.FindEditFormTemplateControl("spin_StorageFreeDays") as ASPxSpinEdit;
            if (spin_StorageFreeDays != null)
                job.StorageFreeDays = SafeValue.SafeInt(spin_StorageFreeDays.Value, 0);
            ASPxSpinEdit spin_STotalDays = grid_Issue.FindEditFormTemplateControl("spin_STotalDays") as ASPxSpinEdit;
            if (spin_STotalDays != null)
                job.StorageTotalDays = Helper.Safe.SafeInt(spin_STotalDays.Value);
            ASPxTextBox txt_TripNo = grid_Issue.FindEditFormTemplateControl("txt_TripNo") as ASPxTextBox;
            if (txt_TripNo != null)
                job.TripNo = txt_TripNo.Text;
            ASPxDateEdit date_MoveDate = grid_Issue.FindEditFormTemplateControl("date_MoveDate") as ASPxDateEdit;
            if (date_MoveDate != null)
                job.MoveDate = date_MoveDate.Date;
            ASPxSpinEdit spin_Charges = grid_Issue.FindEditFormTemplateControl("spin_Charges") as ASPxSpinEdit;
            if (spin_Charges != null)
                job.Charges = SafeValue.SafeDecimal(spin_Charges.Value);
            ASPxButtonEdit btn_PortOfEntry = grid_Issue.FindEditFormTemplateControl("btn_PortOfEntry") as ASPxButtonEdit;
            if (btn_PortOfEntry != null)
                job.EntryPort = btn_PortOfEntry.Text;
            ASPxComboBox txt_Mode = grid_Issue.FindEditFormTemplateControl("cmb_Mode") as ASPxComboBox;
            if (txt_Mode != null)
                job.Mode = txt_Mode.Text;
            ASPxDateEdit date_Eta = grid_Issue.FindEditFormTemplateControl("date_Eta") as ASPxDateEdit;
            if (date_Eta != null)
                job.Eta = date_Eta.Date;
            ASPxTextBox txt_TruckNo = grid_Issue.FindEditFormTemplateControl("txt_TruckNo") as ASPxTextBox;
            if (txt_TruckNo != null)
                job.TruckNo = txt_TruckNo.Text;
            ASPxButtonEdit btn_OriginCity = grid_Issue.FindEditFormTemplateControl("btn_OriginCity") as ASPxButtonEdit;
            if (btn_OriginCity != null)
                job.OriginCity = btn_OriginCity.Text;
            ASPxButtonEdit btn_DestCity = grid_Issue.FindEditFormTemplateControl("btn_DestCity") as ASPxButtonEdit;
            if (btn_DestCity != null)
                job.DestCity = btn_DestCity.Text;
            ASPxTextBox txt_OriginPostal = grid_Issue.FindEditFormTemplateControl("txt_OriginPostal") as ASPxTextBox;
            if (txt_OriginPostal != null)
                job.OriginPostal = txt_OriginPostal.Text;
            ASPxTextBox txt_DestPostal = grid_Issue.FindEditFormTemplateControl("txt_DestPostal") as ASPxTextBox;
            if (txt_DestPostal != null)
                job.DestPostal = txt_DestPostal.Text;
            ASPxComboBox cmb_ServiceType = grid_Issue.FindEditFormTemplateControl("cmb_ServiceType") as ASPxComboBox;
            if (cmb_ServiceType != null)
                job.ServiceType = cmb_ServiceType.Text;
            //Additional

            //ASPxComboBox cmb_FullPacking = pageControl.FindControl("cmb_FullPacking") as ASPxComboBox;
            //if (cmb_FullPacking != null)
            //    job.Item1 = SafeValue.SafeString(cmb_FullPacking.Value);
            //ASPxComboBox cmb_UnFull = pageControl.FindControl("cmb_UnFull") as ASPxComboBox;
            //if (cmb_UnFull != null)
            //    job.Item2 = SafeValue.SafeString(cmb_UnFull.Value);
            //ASPxTextBox txt_Details = pageControl.FindControl("txt_Details") as ASPxTextBox;
            //if (txt_Details != null)
            //    job.ItemDetail1 = txt_Details.Text;
            //ASPxTextBox txt_UnpackDetails = pageControl.FindControl("txt_UnpackDetails") as ASPxTextBox;
            //if (txt_UnpackDetails != null)
            //    job.ItemDetail2 = txt_UnpackDetails.Text;

            //ASPxComboBox cmb_Insurance = pageControl.FindControl("cmb_Insurance") as ASPxComboBox;
            //if (cmb_Insurance != null)
            //    job.Item3 = SafeValue.SafeString(cmb_Insurance.Value);
            //ASPxTextBox txt_Percentage = pageControl.FindControl("txt_Percentage") as ASPxTextBox;
            //if (txt_Percentage != null)
            //    job.ItemValue3 = txt_Percentage.Text;
            //ASPxTextBox txt_Value = pageControl.FindControl("txt_Value") as ASPxTextBox;
            //if (txt_Value != null)
            //    job.ItemData3 = txt_Value.Text;
            //ASPxSpinEdit txt_Premium = pageControl.FindControl("txt_Premium") as ASPxSpinEdit;
            //if (txt_Premium != null)
            //    job.ItemPrice3 = SafeValue.SafeDecimal(txt_Premium.Text);

            //ASPxComboBox cmb_PianoApply = pageControl.FindControl("cmb_PianoApply") as ASPxComboBox;
            //if (cmb_PianoApply != null)
            //    job.Item4 = SafeValue.SafeString(cmb_PianoApply.Value);
            //ASPxTextBox txt_PainoDetails = pageControl.FindControl("txt_PainoDetails") as ASPxTextBox;
            //if (txt_PainoDetails != null)
            //    job.ItemDetail4 = txt_PainoDetails.Text;
            //ASPxSpinEdit spin_Charges1 = pageControl.FindControl("spin_Charges1") as ASPxSpinEdit;
            //if (spin_Charges1 != null)
            //    job.ItemPrice4 = SafeValue.SafeDecimal(spin_Charges1.Value);

            //ASPxComboBox cmb_Safe = pageControl.FindControl("cmb_Safe") as ASPxComboBox;
            //if (cmb_Safe != null)
            //    job.Item5 = cmb_Safe.Text;
            //ASPxTextBox txt_Brand = pageControl.FindControl("txt_Brand") as ASPxTextBox;
            //if (txt_Brand != null)
            //    job.ItemValue5 = SafeValue.SafeString(txt_Brand.Value);
            //ASPxSpinEdit spin_Weight = pageControl.FindControl("spin_Weight") as ASPxSpinEdit;
            //if (spin_Weight != null)
            //    job.ItemPrice5 = SafeValue.SafeDecimal(spin_Weight.Value);

            //ASPxComboBox cmb_Crating = pageControl.FindControl("cmb_Crating") as ASPxComboBox;
            //if (cmb_Crating != null)
            //    job.Item6 = SafeValue.SafeString(cmb_Crating.Value);
            //ASPxTextBox txt_Details1 = pageControl.FindControl("txt_Details1") as ASPxTextBox;
            //if (txt_Details1 != null)
            //    job.ItemDetail6 = txt_Details1.Text;
            //ASPxSpinEdit spin_Charges2 = pageControl.FindControl("spin_Charges2") as ASPxSpinEdit;
            //if (spin_Charges2 != null)
            //    job.ItemPrice6 = SafeValue.SafeDecimal(spin_Charges2.Value);

            //ASPxComboBox cmb_Handyman = pageControl.FindControl("cmb_Handyman") as ASPxComboBox;
            //if (cmb_Handyman != null)
            //    job.Item7 = SafeValue.SafeString(cmb_Handyman.Value);
            //ASPxComboBox cmb_Complimentory = pageControl.FindControl("cmb_Complimentory") as ASPxComboBox;
            //if (cmb_Complimentory != null)
            //    job.ItemValue7 = SafeValue.SafeString(cmb_Complimentory.Value);
            //ASPxTextBox txt_Details2 = pageControl.FindControl("txt_Details2") as ASPxTextBox;
            //if (txt_Details2 != null)
            //    job.ItemDetail7 = txt_Details2.Text;
            //ASPxSpinEdit spin_Charges3 = pageControl.FindControl("spin_Charges3") as ASPxSpinEdit;
            //if (spin_Charges3 != null)
            //    job.ItemPrice7 = SafeValue.SafeDecimal(spin_Charges3.Value);

            //ASPxComboBox cmb_Maid = pageControl.FindControl("cmb_Maid") as ASPxComboBox;
            //if (cmb_Maid != null)
            //    job.Item8 = SafeValue.SafeString(cmb_Maid.Value);
            //ASPxComboBox cmb_Complimentory1 = pageControl.FindControl("cmb_Complimentory1") as ASPxComboBox;
            //if (cmb_Complimentory1 != null)
            //    job.ItemValue8 = SafeValue.SafeString(cmb_Complimentory1.Value);
            //ASPxTextBox txt_Details3 = pageControl.FindControl("txt_Details3") as ASPxTextBox;
            //if (txt_Details3 != null)
            //    job.ItemDetail8 = txt_Details3.Text;
            //ASPxSpinEdit spin_Charges4 = pageControl.FindControl("spin_Charges4") as ASPxSpinEdit;
            //if (spin_Charges4 != null)
            //    job.ItemPrice8 = SafeValue.SafeDecimal(spin_Charges4.Value);

            //ASPxComboBox cmb_Shifting = pageControl.FindControl("cmb_Shifting") as ASPxComboBox;
            //if (cmb_Shifting != null)
            //    job.Item9 = SafeValue.SafeString(cmb_Shifting.Value);
            //ASPxComboBox cmb_Complimentory2 = pageControl.FindControl("cmb_Complimentory2") as ASPxComboBox;
            //if (cmb_Complimentory2 != null)
            //    job.ItemValue9 = SafeValue.SafeString(cmb_Complimentory2.Value);
            //ASPxTextBox txt_Details4 = pageControl.FindControl("txt_Details4") as ASPxTextBox;
            //if (txt_Details4 != null)
            //    job.ItemDetail9 = txt_Details4.Text;
            //ASPxSpinEdit spin_Charges5 = pageControl.FindControl("spin_Charges5") as ASPxSpinEdit;
            //if (spin_Charges5 != null)
            //    job.ItemPrice9 = SafeValue.SafeDecimal(spin_Charges5.Value);

            //ASPxComboBox cmb_Disposal = pageControl.FindControl("cmb_Disposal") as ASPxComboBox;
            //if (cmb_Disposal != null)
            //    job.Item10 = cmb_Disposal.Text;
            //ASPxComboBox cmb_Complimentory3 = pageControl.FindControl("cmb_Complimentory3") as ASPxComboBox;
            //if (cmb_Complimentory3 != null)
            //    job.ItemValue10 = cmb_Complimentory3.Text;
            //ASPxTextBox txt_Details5 = pageControl.FindControl("txt_Details5") as ASPxTextBox;
            //if (txt_Details5 != null)
            //    job.ItemDetail10 = txt_Details5.Text;
            //ASPxSpinEdit spin_Charges6 = pageControl.FindControl("spin_Charges6") as ASPxSpinEdit;
            //if (spin_Charges6 != null)
            //    job.ItemPrice10 = SafeValue.SafeDecimal(spin_Charges6.Value);

            //ASPxComboBox cmb_PickUp = pageControl.FindControl("cmb_PickUp") as ASPxComboBox;
            //if (cmb_PickUp != null)
            //    job.Item11 = SafeValue.SafeString(cmb_PickUp.Value);
            //ASPxTextBox txt_Details6 = pageControl.FindControl("txt_Details6") as ASPxTextBox;
            //if (txt_Details6 != null)
            //    job.ItemDetail11 = txt_Details6.Text;

            //ASPxComboBox cmb_Additional = pageControl.FindControl("cmb_Additional") as ASPxComboBox;
            //if (cmb_Additional != null)
            //    job.Item12 = cmb_Additional.Text;
            //ASPxTextBox txt_Details7 = pageControl.FindControl("txt_Details7") as ASPxTextBox;
            //if (txt_Details7 != null)
            //    job.ItemDetail12 = txt_Details7.Text;

            //ASPxComboBox cmb_BadAccess = pageControl.FindControl("cmb_BadAccess") as ASPxComboBox;
            //if (cmb_BadAccess != null)
            //    job.Item13 = SafeValue.SafeString(cmb_BadAccess.Value);
            //ASPxComboBox cmb_Origin = pageControl.FindControl("cmb_Origin") as ASPxComboBox;
            //if (cmb_Origin != null)
            //    job.ItemValue13 = cmb_Origin.Text;
            //ASPxComboBox cmb_Destination = pageControl.FindControl("cmb_Destination") as ASPxComboBox;
            //if (cmb_Destination != null)
            //    job.ItemData13 = SafeValue.SafeString(cmb_Destination.Value);

            //ASPxComboBox cmb_Storage = pageControl.FindControl("cmb_Storage") as ASPxComboBox;
            //if (cmb_Storage != null)
            //    job.Item14 = SafeValue.SafeString(cmb_Storage.Value);
            //ASPxComboBox cmb_Complimentory4 = pageControl.FindControl("cmb_Complimentory4") as ASPxComboBox;
            //if (cmb_Complimentory4 != null)
            //    job.ItemValue14 = SafeValue.SafeString(cmb_Complimentory4.Value);
            //ASPxTextBox txt_Details8 = pageControl.FindControl("txt_Details8") as ASPxTextBox;
            //if (txt_Details8 != null)
            //    job.ItemDetail14 = txt_Details8.Text;
            //ASPxSpinEdit spin_Charges7 = pageControl.FindControl("spin_Charges7") as ASPxSpinEdit;
            //if (spin_Charges7 != null)
            //    job.ItemPrice14 = SafeValue.SafeDecimal(spin_Charges7.Value);

            //ASPxTextBox txt_How = pageControl.FindControl("txt_How") as ASPxTextBox;
            //if (txt_How != null)
            //    job.Answer1 = txt_How.Text;
            //ASPxTextBox txt_Other = pageControl.FindControl("txt_Other") as ASPxTextBox;
            //if (txt_Other != null)
            //    job.Answer2 = txt_Other.Text;
            //ASPxTextBox txt_Move = pageControl.FindControl("txt_Move") as ASPxTextBox;
            //if (txt_Move != null)
            //    job.Answer3 = txt_Move.Text;
            //ASPxTextBox txt_UnsuccessRemark = pageControl.FindControl("txt_UnsuccessRemark") as ASPxTextBox;
            //if (txt_UnsuccessRemark != null)
            //    job.Answer4 = txt_UnsuccessRemark.Text;
            //ASPxComboBox cmb_WorkStatus = grid_Issue.FindEditFormTemplateControl("cmb_WorkStatus") as ASPxComboBox;
            //if (cmb_WorkStatus != null)
            //    job.WorkStatus = SafeValue.SafeString(cmb_WorkStatus.Value);



            //Quotation
            //ASPxHtmlEditor txt_Attention1 = pageControl.FindControl("txt_Attention1") as ASPxHtmlEditor;
            //if (txt_Attention1 != null)
            //    job.Attention1 = txt_Attention1.Html;
            //ASPxHtmlEditor txt_Attention2 = pageControl.FindControl("txt_Attention2") as ASPxHtmlEditor;
            //if (txt_Attention2 != null)
            //    job.Attention2 = txt_Attention2.Html;
            //ASPxHtmlEditor txt_Attention3 = pageControl.FindControl("txt_Attention3") as ASPxHtmlEditor;
            //if (txt_Attention3 != null)
            //    job.Attention3 = txt_Attention3.Html;
            //ASPxHtmlEditor txt_Attention4 = pageControl.FindControl("txt_Attention4") as ASPxHtmlEditor;
            //if (txt_Attention4 != null)
            //    job.Attention4 = txt_Attention4.Html;
            //ASPxHtmlEditor txt_Attention5 = pageControl.FindControl("txt_Attention5") as ASPxHtmlEditor;
            //if (txt_Attention5 != null)
            //    job.Attention5 = txt_Attention5.Html;

            //if (cmb_Status.Text == "Job Confirmation")
            //{
            //    job.WorkStatus = "Pending";
            //}
            //ASPxComboBox cmb_SalesId = grid_Issue.FindEditFormTemplateControl("cmb_SalesId") as ASPxComboBox;
            //if (cmb_SalesId != null)
            //    job.Value4 = cmb_SalesId.Text;


            //if (cmb_Status.Text == "Job Confirmation")
            //{
            //    job.WorkStatus = "Pending";
            //}
            //if (job.JobStage == "Customer Inquiry")
            //{
            //    job.DateTime1 = DateTime.Now;

            //}



            //if (job.WorkStatus == "Costing")
            //{
            //    job.DateTime3 = DateTime.Now;

            //}
            //if (job.WorkStatus == "Quotation")
            //{
            //    job.DateTime4 = DateTime.Now;

            //}
            //if (job.WorkStatus == "Job Confirmation")
            //{
            //    job.DateTime5 = DateTime.Now;

            //}
            //if (job.WorkStatus == "Billing")
            //{
            //    job.DateTime6 = DateTime.Now;

            //}
            //if (job.WorkStatus == "Job Completion")
            //{
            //    job.DateTime7 = DateTime.Now;

            //}
            //if (job.WorkStatus == "Job Close")
            //{
            //    job.DateTime8 = DateTime.Now;

            //}
            //ASPxDateEdit date_DateTime2 = grid_Issue.FindEditFormTemplateControl("date_DateTime2") as ASPxDateEdit;
            //if (date_DateTime2 != null)
            //{
            //    if (!date_DateTime2.Date.IsDaylightSavingTime())
            //        job.DateTime2 = date_DateTime2.Date;
            //    else
            //        job.DateTime2 = DateTime.Now;
            //}
            //ASPxMemo memo_Notes = pageControl.FindControl("memo_Notes") as ASPxMemo;
            //if (memo_Notes != null)
            //    job.Notes = memo_Notes.Text;
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                job.CreateBy = userId;
                job.CreateDateTime = DateTime.Now;
                job.UpdateBy = userId;
                job.UpdateDateTime = DateTime.Now;
                job.JobNo = issueN;
                Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(job);
                C2Setup.SetNextNo("", "JobOrder", issueN, issueDate.Date);
            }
            else
            {
                job.UpdateBy = userId;
                job.UpdateDateTime = DateTime.Now;
                bool isAddLog = false;
                if (job.JobStatus == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select JobStatus from JobSchedule where JobNo='" + job.JobNo + "'")))
                {
                }
                else
                {
                    isAddLog = true;
                }
                Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(job);
                if (isAddLog)
                    EzshipLog.Log(job.JobNo, "", "JO", job.JobStatus);
            }
            Session["SoWhere"] = "JobNo='" + job.JobNo + "'";
            this.dsSchedule.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);

            return job.JobNo;
        }
        catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
        return "";
    }
    #endregion
    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
        ASPxTextBox txt_JobNo = grid_Issue.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string doNo = SafeValue.SafeString(txt_JobNo.Text);
        string sql = string.Format(@"select JobStage from JobSchedule where JobNo='{0}'", doNo);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        if (status == "Haulier Import")
        {
            cmb_Status.Text = "Haulier Import";
        }
        if (status == "Haulier Export")
        {
            cmb_Status.Text = "Haulier Export";
        }
        if (status == "Freight Import")
        {
            cmb_Status.Text = "Freight Import";
        }
        if (status == "Freight Export")
        {
            cmb_Status.Text = "Freight Export";
        }
        if (status == "Transport")
        {
            cmb_Status.Text = "Transport";
        }
        if (status == "Warehouse Receive")
        {
            cmb_Status.Text = "Warehouse Receive";
        }
        if (status == "Warehouse Release")
        {
            cmb_Status.Text = "Warehouse Release";
        }
        if (status == "Misc Job")
        {
            cmb_Status.Text = "Misc Job";
        }

    }
    protected void cmb_JobType_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
    }
    protected void cmb_WorkStatus_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {

        ASPxComboBox cmb_WorkStatus = grid_Issue.FindEditFormTemplateControl("cmb_WorkStatus") as ASPxComboBox;
        ASPxTextBox txt_JobNo = grid_Issue.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string doNo = SafeValue.SafeString(txt_JobNo.Text);
        string sql = string.Format(@"select JobStage from JobSchedule where JobNo='{0}'", doNo);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        if (status == "Job Completion")
        {
            cmb_WorkStatus.Text = "COMPLETE";
        }

    }

    protected int ViaItemName(string name, string type, string doNo)
    {
        string sql = string.Format(@"select Count(*) from  JobItemList where RefNo='{0}' and ItemName='{1}' and ItemType='{2}'", doNo, name, type);
        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        return cnt;
    }
    #region Item List
    protected void Grid_Packing_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {

            string sql = "select JobNo from JobSchedule where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsJobItemList.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void Grid_Packing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ItemName"] = SafeValue.SafeString(e.NewValues["ItemName"]);
        e.NewValues["ItemQty"] = SafeValue.SafeInt(e.NewValues["ItemQty"], 0);
        e.NewValues["ItemValue"] = SafeValue.SafeDecimal(e.NewValues["ItemValue"], 0);
        e.NewValues["ItemMark"] = SafeValue.SafeString(e.NewValues["ItemMark"]);
        e.NewValues["ItemNote"] = SafeValue.SafeString(e.NewValues["ItemNote"]);

        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void Grid_Packing_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox refN = this.grid_Issue.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = "";
        e.NewValues["RefNo"] = refN.Text;
    }
    protected void Grid_Packing_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void Grid_Packing_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty"] = 0;

        ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxButtonEdit refN = this.grid_Issue.FindEditFormTemplateControl("txt_JobNo") as ASPxButtonEdit;

    }
    protected void Grid_Packing_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobItemList));
            if (!IsPostBack)
            {
                ApplyLayout(0, grd);
            }
        }
    }
    protected void Grid_Packing_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ApplyLayout(Int32.Parse(e.Parameters), grd);
    }
    protected void ApplyLayout(int layoutIndex, ASPxGridView grid)
    {
        if (grid != null)
        {
            grid.BeginUpdate();
            try
            {
                grid.ClearSort();
                switch (layoutIndex)
                {
                    case 0:
                        grid.GroupBy(grid.Columns["ItemType"]);
                        break;
                    case 1:
                        grid.GroupBy(grid.Columns["ItemType"]);
                        grid.GroupBy(grid.Columns["ItemName"]);
                        break;
                    case 2:
                        grid.GroupBy(grid.Columns["ItemQty"]);
                        break;
                }
            }
            finally
            {
                grid.EndUpdate();
            }
            grid.ExpandAll();
        }
    }
    #endregion
}