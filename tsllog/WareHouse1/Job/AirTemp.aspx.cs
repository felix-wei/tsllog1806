jusing System;
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
using System.Xml;
using System.IO;
using DevExpress.Web.ASPxHtmlEditor;

public partial class WareHouse_Job_AirTemp : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            Session["SoWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"] != "0")
            {
                Session["SoWhere"] = "JobNo='" + Request.QueryString["no"] + "'";
                this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            }
            else if (Request.QueryString["no"] == null || Request.QueryString["no"] == "0")
            {
                string sql = string.Format(@"select count(*) from JobInfo where JobType='ARTemp'");
                int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                if (cnt == 0)
                {
                    this.grid_Issue.AddNewRow();
                }
                else
                {
                    Session["SoWhere"] = "JobType='ARTemp'";
                    dsIssue.FilterExpression = Session["SoWhere"].ToString();
                }

            }
            else
                this.dsIssue.FilterExpression = "1=0";

        }
        if (Session["SoWhere"] != null)
        {
            this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region JO
    protected void grid_Issue_DataSelect(object sender, EventArgs e)
    {
    }
    protected void grid_Issue_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobInfo));
        }
    }
    protected void grid_Issue_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        e.NewValues["JobNo"] = "NEW";
        e.NewValues["JobDate"] = DateTime.Now;
        e.NewValues["JobType"] = "ARTemp";
        e.NewValues["JobStage"] = "Site Survey";
        e.NewValues["CreateBy"] = userId;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["DateTime2"] = DateTime.Now;
        e.NewValues["ExpectedDate"] = DateTime.Today;
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.000000;
        e.NewValues["PayTerm"] = "30DAYS";
        e.NewValues["IncoTerm"] = "EXW";
        e.NewValues["WareHouseId"] = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];

        e.NewValues["Attention1"] = @"<p>You can insure separately at a premium of 1.50% of total value.(Min. Premium is $100.00)
Please be advised that completed insurance application must be submitted to our office prior to pack and move dates. There is no liability on Collin’s Movers upon the decline of insurance. All items in the shipment must be insured. Any policy that covers only a limited number of items in the shipment may be subject to rejection by insurance company.</p>
<p>This Quotation is based on us providing a normal weekday(Monday to Saturday)services unless otherwise stated. Any substantial variation in the volume will affect our charges. The validity of this quotation is 30 days from the date of issue.</p>
";
        e.NewValues["Attention2"] = @"<p>*Preparation : Delivery of cartons and tapes for self packing.</p>
<p>*Packing Materials : Provide required quantity of packing materials such as various sizes of cartons boxes, Bubble wraps, Tapes etc. Our service includes one time collection of empty cartons after the completion of the move.</p>
<p>*Pack and Unpack: Wrapping and packing of all fragile to include china, glassware, paintings, ornaments, books files, kitchenware and miscellaneous item and unpacking on destination if not specified.</p>
<p>*Moving: Provide skilled and experienced movers to pack and move your house hold items.</p>
<p>*Supervisor : Provide highly experienced and skilled team supervisor during the entire move.</p>
<p>*Transportation: Provide Collins Movers 22ft covered truck equipped with hydraulic tail gate system. All trucks are equipped with GPS tracking system to monitor the truck movements.</p>
<p>*MCST Process: Removal permit application and refundable cheque deposit if applicable for your condominium move.</p>
";
        e.NewValues["Attention3"] = @"<p>*Insurance coverage</p>
<p>*Non refundable Condominium administration fee, Lift padding fee or any such fees.</p>
<p>*Handyman Services.</p>
<p>*Maid Services.</p>
<p>*Piano and Safe handling.</p>
<p>*Additional pickup and delivery unless specified.</p>
";
        e.NewValues["Attention4"] = @"<p>This quotation is based on providing a normal weekday(Monday to Saturday) services unless otherwise</p><p> stated. To confirm acceptance of our services please complete the attached Acceptance Sheet and return to</p><p> us bay fax ,mail or email as soon as possible.</p>

";
        e.NewValues["Attention5"] = @"<p>FULL Payment is required on the FIRST day of the move. We require company’s stamp and authorized</p><p> signature on the acceptance sheet in case payment is made by Company. Please make cheque payable to</p><p> Collins Movers Pte Ltd. For cash payment, Please contact our office beforehand and speak to sales person</p><p> on this agreement.</p>
<p>&nbsp;&nbsp;</p><p>&nbsp;&nbsp;</p>
<p>Once a booking has been made any cancellation of job would result in a penalty of 30% upon the contract</p><p> agreed. However within 24 hours prior to the commencement of the move, any cancellation would be</p><p> penalized at 70% of the contract agreed.</p>
<p>&nbsp;&nbsp;</p><p>&nbsp;&nbsp;</p>
<p>We thank you once again for the opportunity to be of service to you. We hope that the above rate meets</p><p> your approval and should you require any further information regarding our services, please do not hesitate</p><p> to contact us.</p>
";
    }
    protected string SaveJob()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxTextBox DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            ASPxTextBox txt_Id = grid_Issue.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string pId = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(JobInfo), "Id='" + pId + "'");
            JobInfo job = C2.Manager.ORManager.GetObject(query) as JobInfo;
            ASPxDateEdit issueDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            bool isNew = false;
            //const string runType = "DOOUT";
            string issueN = "";
            if (job == null)
            {
                job = new JobInfo();
                isNew = true;
                job.JobType = "ARTemp";
                //issueN = C2Setup.GetNextNo("", "JobOrder", issueDate.Date);
                issueN = "ARTemp-01";
                job.JobDate = issueDate.Date;
            }
            job.Attribute = "Temp";

            ASPxDateEdit doDate = grid_Issue.FindEditFormTemplateControl("date_IssueDate") as ASPxDateEdit;
            job.JobDate = doDate.Date;

            ASPxComboBox cmb_Status = grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            job.JobStage = SafeValue.SafeString(cmb_Status.Value);
            ASPxComboBox cmb_JobType = grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
            job.JobType = SafeValue.SafeString(cmb_JobType.Value);
            //Main Info

            ASPxMemo remark = grid_Issue.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            job.Remark = remark.Text;
            ASPxButtonEdit currency = grid_Issue.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
            job.Currency = currency.Text;
            ASPxSpinEdit exRate = grid_Issue.FindEditFormTemplateControl("spin_ExRate") as ASPxSpinEdit;
            job.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            ASPxComboBox payTerm = grid_Issue.FindEditFormTemplateControl("cmb_PayTerm") as ASPxComboBox;
            job.PayTerm = payTerm.Text;
            ASPxDateEdit date_ExpiryDate = grid_Issue.FindEditFormTemplateControl("date_ExpiryDate") as ASPxDateEdit;
            job.ExpiryDate = date_ExpiryDate.Date;


            ASPxMemo txt_PackRemark = grid_Issue.FindEditFormTemplateControl("txt_PackRemark") as ASPxMemo;
            job.PackRmk = txt_PackRemark.Text;
            ASPxMemo txt_MoveRemark = grid_Issue.FindEditFormTemplateControl("txt_MoveRemark") as ASPxMemo;
            job.MoveRmk = txt_MoveRemark.Text;

            ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
            job.WareHouseId = txt_WareHouseId.Text;

            ASPxSpinEdit spin_Volumne = grid_Issue.FindEditFormTemplateControl("spin_Volumne") as ASPxSpinEdit;
            job.Volumne = SafeValue.SafeDecimal(spin_Volumne.Value);
            ASPxMemo memo_Description = grid_Issue.FindEditFormTemplateControl("memo_Description") as ASPxMemo;
            job.ItemDes = memo_Description.Text;
            ASPxTextBox memo_Volumn = grid_Issue.FindEditFormTemplateControl("memo_Volumn") as ASPxTextBox;
            job.VolumneRmk = memo_Description.Text;
            ASPxSpinEdit spin_HeadCount = grid_Issue.FindEditFormTemplateControl("spin_HeadCount") as ASPxSpinEdit;
            job.HeadCount = SafeValue.SafeInt(spin_HeadCount.Value, 0);

            ASPxDateEdit date_Pack = grid_Issue.FindEditFormTemplateControl("date_Pack") as ASPxDateEdit;
            job.PackDate = date_Pack.Date;
            ASPxComboBox cmb_Via = grid_Issue.FindEditFormTemplateControl("cmb_Via") as ASPxComboBox;
            job.ViaWh = cmb_Via.Text;
            ASPxDateEdit date_StorageStartDate = grid_Issue.FindEditFormTemplateControl("date_StorageStartDate") as ASPxDateEdit;
            job.StorageStartDate = date_StorageStartDate.Date;
            ASPxSpinEdit spin_StorageFreeDays = grid_Issue.FindEditFormTemplateControl("spin_StorageFreeDays") as ASPxSpinEdit;
            job.StorageFreeDays = SafeValue.SafeInt(spin_StorageFreeDays.Value, 0);
            ASPxSpinEdit spin_STotalDays = grid_Issue.FindEditFormTemplateControl("spin_STotalDays") as ASPxSpinEdit;
            job.StorageTotalDays = Helper.Safe.SafeInt(spin_STotalDays.Value);
            ASPxTextBox txt_TripNo = grid_Issue.FindEditFormTemplateControl("txt_TripNo") as ASPxTextBox;
            job.TripNo = txt_TripNo.Text;
            ASPxDateEdit date_MoveDate = grid_Issue.FindEditFormTemplateControl("date_MoveDate") as ASPxDateEdit;
            job.MoveDate = date_MoveDate.Date;
            ASPxSpinEdit spin_Charges = grid_Issue.FindEditFormTemplateControl("spin_Charges") as ASPxSpinEdit;
            job.Charges = SafeValue.SafeDecimal(spin_Charges.Value);
            ASPxButtonEdit btn_PortOfEntry = grid_Issue.FindEditFormTemplateControl("btn_PortOfEntry") as ASPxButtonEdit;
            job.EntryPort = btn_PortOfEntry.Text;
            ASPxComboBox txt_Mode = grid_Issue.FindEditFormTemplateControl("cmb_Mode") as ASPxComboBox;
            job.Mode = txt_Mode.Text;
            ASPxDateEdit date_Eta = grid_Issue.FindEditFormTemplateControl("date_Eta") as ASPxDateEdit;
            job.Eta = date_Eta.Date;
            ASPxTextBox txt_TruckNo = grid_Issue.FindEditFormTemplateControl("txt_TruckNo") as ASPxTextBox;
            job.TruckNo = txt_TruckNo.Text;

            ASPxComboBox cmb_ServiceType = grid_Issue.FindEditFormTemplateControl("cmb_ServiceType") as ASPxComboBox;
            job.ServiceType = cmb_ServiceType.Text;
            //Additional

            ASPxComboBox cmb_FullPacking = pageControl.FindControl("cmb_FullPacking") as ASPxComboBox;
            job.Item1 = SafeValue.SafeString(cmb_FullPacking.Value);
            ASPxComboBox cmb_UnFull = pageControl.FindControl("cmb_UnFull") as ASPxComboBox;
            job.Item2 = SafeValue.SafeString(cmb_UnFull.Value);
            ASPxTextBox txt_Details = pageControl.FindControl("txt_Details") as ASPxTextBox;
            job.ItemDetail1 = txt_Details.Text;
            ASPxTextBox txt_UnpackDetails = pageControl.FindControl("txt_UnpackDetails") as ASPxTextBox;
            job.ItemDetail2 = txt_UnpackDetails.Text;

            ASPxComboBox cmb_Insurance = pageControl.FindControl("cmb_Insurance") as ASPxComboBox;
            job.Item3 = SafeValue.SafeString(cmb_Insurance.Value);
            ASPxTextBox txt_Percentage = pageControl.FindControl("txt_Percentage") as ASPxTextBox;
            job.ItemValue3 = txt_Percentage.Text;
            ASPxTextBox txt_Value = pageControl.FindControl("txt_Value") as ASPxTextBox;
            job.ItemData3 = txt_Value.Text;
            ASPxSpinEdit txt_Premium = pageControl.FindControl("txt_Premium") as ASPxSpinEdit;
            job.ItemPrice3 = SafeValue.SafeDecimal(txt_Premium.Text);

            ASPxComboBox cmb_PianoApply = pageControl.FindControl("cmb_PianoApply") as ASPxComboBox;
            job.Item4 = SafeValue.SafeString(cmb_PianoApply.Value);
            ASPxTextBox txt_PainoDetails = pageControl.FindControl("txt_PainoDetails") as ASPxTextBox;
            job.ItemDetail4 = txt_PainoDetails.Text;
            ASPxSpinEdit spin_Charges1 = pageControl.FindControl("spin_Charges1") as ASPxSpinEdit;
            job.ItemPrice4 = SafeValue.SafeDecimal(spin_Charges1.Value);

            ASPxComboBox cmb_Safe = pageControl.FindControl("cmb_Safe") as ASPxComboBox;
            job.Item5 = cmb_Safe.Text;
            ASPxTextBox txt_Brand = pageControl.FindControl("txt_Brand") as ASPxTextBox;
            job.ItemValue5 = SafeValue.SafeString(txt_Brand.Value);
            ASPxSpinEdit spin_Weight = pageControl.FindControl("spin_Weight") as ASPxSpinEdit;
            job.ItemPrice5 = SafeValue.SafeDecimal(spin_Weight.Value);

            ASPxComboBox cmb_Crating = pageControl.FindControl("cmb_Crating") as ASPxComboBox;
            job.Item6 = SafeValue.SafeString(cmb_Crating.Value);
            ASPxTextBox txt_Details1 = pageControl.FindControl("txt_Details1") as ASPxTextBox;
            job.ItemDetail6 = txt_Details1.Text;
            ASPxSpinEdit spin_Charges2 = pageControl.FindControl("spin_Charges2") as ASPxSpinEdit;
            job.ItemPrice6 = SafeValue.SafeDecimal(spin_Charges2.Value);

            ASPxComboBox cmb_Handyman = pageControl.FindControl("cmb_Handyman") as ASPxComboBox;
            job.Item7 = SafeValue.SafeString(cmb_Handyman.Value);
            ASPxComboBox cmb_Complimentory = pageControl.FindControl("cmb_Complimentory") as ASPxComboBox;
            job.ItemValue7 = SafeValue.SafeString(cmb_Complimentory.Value);
            ASPxTextBox txt_Details2 = pageControl.FindControl("txt_Details2") as ASPxTextBox;
            job.ItemDetail7 = txt_Details2.Text;
            ASPxSpinEdit spin_Charges3 = pageControl.FindControl("spin_Charges3") as ASPxSpinEdit;
            job.ItemPrice7 = SafeValue.SafeDecimal(spin_Charges3.Value);

            ASPxComboBox cmb_Maid = pageControl.FindControl("cmb_Maid") as ASPxComboBox;
            job.Item8 = SafeValue.SafeString(cmb_Maid.Value);
            ASPxComboBox cmb_Complimentory1 = pageControl.FindControl("cmb_Complimentory1") as ASPxComboBox;
            job.ItemValue8 = SafeValue.SafeString(cmb_Complimentory1.Value);
            ASPxTextBox txt_Details3 = pageControl.FindControl("txt_Details3") as ASPxTextBox;
            job.ItemDetail8 = txt_Details3.Text;
            ASPxSpinEdit spin_Charges4 = pageControl.FindControl("spin_Charges4") as ASPxSpinEdit;
            job.ItemPrice8 = SafeValue.SafeDecimal(spin_Charges4.Value);

            ASPxComboBox cmb_Shifting = pageControl.FindControl("cmb_Shifting") as ASPxComboBox;
            job.Item9 = SafeValue.SafeString(cmb_Shifting.Value);
            ASPxComboBox cmb_Complimentory2 = pageControl.FindControl("cmb_Complimentory2") as ASPxComboBox;
            job.ItemValue9 = SafeValue.SafeString(cmb_Complimentory2.Value);
            ASPxTextBox txt_Details4 = pageControl.FindControl("txt_Details4") as ASPxTextBox;
            job.ItemDetail9 = txt_Details4.Text;
            ASPxSpinEdit spin_Charges5 = pageControl.FindControl("spin_Charges5") as ASPxSpinEdit;
            job.ItemPrice9 = SafeValue.SafeDecimal(spin_Charges5.Value);

            ASPxComboBox cmb_Disposal = pageControl.FindControl("cmb_Disposal") as ASPxComboBox;
            job.Item10 = cmb_Disposal.Text;
            ASPxComboBox cmb_Complimentory3 = pageControl.FindControl("cmb_Complimentory3") as ASPxComboBox;
            job.ItemValue10 = cmb_Complimentory3.Text;
            ASPxTextBox txt_Details5 = pageControl.FindControl("txt_Details5") as ASPxTextBox;
            job.ItemDetail10 = txt_Details5.Text;
            ASPxSpinEdit spin_Charges6 = pageControl.FindControl("spin_Charges6") as ASPxSpinEdit;
            job.ItemPrice10 = SafeValue.SafeDecimal(spin_Charges6.Value);

            ASPxComboBox cmb_PickUp = pageControl.FindControl("cmb_PickUp") as ASPxComboBox;
            job.Item11 = SafeValue.SafeString(cmb_PickUp.Value);
            ASPxTextBox txt_Details6 = pageControl.FindControl("txt_Details6") as ASPxTextBox;
            job.ItemDetail11 = txt_Details6.Text;

            ASPxComboBox cmb_Additional = pageControl.FindControl("cmb_Additional") as ASPxComboBox;
            job.Item12 = cmb_Additional.Text;
            ASPxTextBox txt_Details7 = pageControl.FindControl("txt_Details7") as ASPxTextBox;
            job.ItemDetail12 = txt_Details7.Text;

            ASPxComboBox cmb_BadAccess = pageControl.FindControl("cmb_BadAccess") as ASPxComboBox;
            job.Item13 = SafeValue.SafeString(cmb_BadAccess.Value);
            ASPxComboBox cmb_Origin = pageControl.FindControl("cmb_Origin") as ASPxComboBox;
            job.ItemValue13 = cmb_Origin.Text;
            ASPxComboBox cmb_Destination = pageControl.FindControl("cmb_Destination") as ASPxComboBox;
            job.ItemData13 = SafeValue.SafeString(cmb_Destination.Value);

            ASPxComboBox cmb_Storage = pageControl.FindControl("cmb_Storage") as ASPxComboBox;
            job.Item14 = SafeValue.SafeString(cmb_Storage.Value);
            ASPxComboBox cmb_Complimentory4 = pageControl.FindControl("cmb_Complimentory4") as ASPxComboBox;
            job.ItemValue14 = SafeValue.SafeString(cmb_Complimentory4.Value);
            ASPxTextBox txt_Details8 = pageControl.FindControl("txt_Details8") as ASPxTextBox;
            job.ItemDetail14 = txt_Details8.Text;
            ASPxSpinEdit spin_Charges7 = pageControl.FindControl("spin_Charges7") as ASPxSpinEdit;
            job.ItemPrice14 = SafeValue.SafeDecimal(spin_Charges7.Value);

            ASPxTextBox txt_How = pageControl.FindControl("txt_How") as ASPxTextBox;
            job.Answer1 = txt_How.Text;
            ASPxTextBox txt_Other = pageControl.FindControl("txt_Other") as ASPxTextBox;
            job.Answer2 = txt_Other.Text;
            ASPxTextBox txt_Move = pageControl.FindControl("txt_Move") as ASPxTextBox;
            job.Answer3 = txt_Move.Text;
            ASPxTextBox txt_UnsuccessRemark = pageControl.FindControl("txt_UnsuccessRemark") as ASPxTextBox;
            job.Answer4 = txt_UnsuccessRemark.Text;
            ASPxComboBox cmb_WorkStatus = grid_Issue.FindEditFormTemplateControl("cmb_WorkStatus") as ASPxComboBox;
            job.WorkStatus = SafeValue.SafeString(cmb_WorkStatus.Value);



            //Quotation
            ASPxHtmlEditor txt_Attention1 = pageControl.FindControl("txt_Attention1") as ASPxHtmlEditor;
            job.Attention1 = txt_Attention1.Html;
            ASPxHtmlEditor txt_Attention2 = pageControl.FindControl("txt_Attention2") as ASPxHtmlEditor;
            job.Attention2 = txt_Attention2.Html;
            ASPxHtmlEditor txt_Attention3 = pageControl.FindControl("txt_Attention3") as ASPxHtmlEditor;
            job.Attention3 = txt_Attention3.Html;
            ASPxHtmlEditor txt_Attention4 = pageControl.FindControl("txt_Attention4") as ASPxHtmlEditor;
            job.Attention4 = txt_Attention4.Html;
            ASPxHtmlEditor txt_Attention5 = pageControl.FindControl("txt_Attention5") as ASPxHtmlEditor;
            job.Attention5 = txt_Attention5.Html;
                job.WorkStatus = "Pending";
            
            ASPxComboBox cmb_SalesId = grid_Issue.FindEditFormTemplateControl("cmb_SalesId") as ASPxComboBox;
            job.Value4 = cmb_SalesId.Text;


            

            ASPxMemo memo_Notes = pageControl.FindControl("memo_Notes") as ASPxMemo;
            job.Notes = memo_Notes.Text;
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
                // C2Setup.SetNextNo("", "JobOrder", issueN, issueDate.Date);
            }
            else
            {
                job.UpdateBy = userId;
                job.UpdateDateTime = DateTime.Now;
                bool isAddLog = false;
                if (job.JobStatus == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select JobStatus from JobInfo where JobNo='" + job.JobNo + "'")))
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
            this.dsIssue.FilterExpression = Session["SoWhere"].ToString();
            if (this.grid_Issue.GetRow(0) != null)
                this.grid_Issue.StartEdit(0);

            return job.JobNo;
        }
        catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
        return "";
    }

    protected void grid_Issue_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_Issue.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
            ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
            ASPxComboBox cmb_Status = this.grid_Issue.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            ASPxComboBox cmb_JobType = this.grid_Issue.FindEditFormTemplateControl("cmb_JobType") as ASPxComboBox;
        }
    }
    protected void grid_Issue_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
    }
    protected void grid_Issue_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Issue.FindEditFormTemplateControl("pageControl_Hbl") as ASPxPageControl;
        ASPxTextBox txt_DoNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        ASPxButtonEdit txt_WareHouseId = grid_Issue.FindEditFormTemplateControl("txt_WareHouseId") as ASPxButtonEdit;
        ASPxButtonEdit txt_CustomerId = grid_Issue.FindEditFormTemplateControl("txt_CustomerId") as ASPxButtonEdit;
        string s = e.Parameters;
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
            //    string sql = string.Format(@"select count(SequenceId) from Cost where RefNo='{0}'", txt_DoNo.Text);
            //    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
            //    {
            //        e.Result = "Fail!No Costing";
            //        return;
            //    }
            //}
            if (txt_DoNo.Text.Length > 4)
            {
                SaveJob();
                e.Result = "";//update old one
            }
            else
                e.Result = SaveJob();// new one
            #endregion
        }

    }
    protected void cmb_JobType_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
    }
    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
    }
    protected void cmb_WorkStatus_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {

    }
    #endregion
}