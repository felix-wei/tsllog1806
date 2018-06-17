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


public partial class WareHouse_Job_JobList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = new DateTime(2010,1,1); // DateTime.Today.AddDays(-90);
            this.txt_end.Date = DateTime.Today.AddDays(30);
            //this.date_IssueDate.Date = DateTime.Today;
            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       // if (!IsPostBack)
        //    btn_search_Click(null, null);
    }
	
	public string InvoiceNo(string jobno, string jobno2, string chgcode)
	{
		if(S.Text(jobno2).Trim().Length == 0)
			return "";
		DataTable dt = GetInvoice(jobno, chgcode);
		string ret = "";
		for(int i=0; i<dt.Rows.Count; i++)
		{
			DataRow dr = dt.Rows[i];
			ret += i==0 ? "" : "<br>";
			ret += "<a href='javascript:ShowInvoice(\""+S.Text(dr["DocNo"])+"\")'>"+S.Text(dr["DocNo"])+"</a>";
		}
		if(ret=="" & chgcode=="")
			ret = "<div style='background:red;height:22px;width:60px;'>&nbsp;</div>";
		if(ret=="" & chgcode!="")
			ret = "<div style='background:orange;height:22px;width:60px;'>&nbsp;</div>";
		
		return ret;
	}
	
	public DataTable GetInvoice(string jobno, string chgcode)
	{
		DataTable dt = new DataTable();
		if( jobno == "")
			return dt;
		if( chgcode == "")
		{
			dt = D.List("select docno from xaarinvoice where doctype='IV'  and mastrefno='"+jobno+"'");
		}
		else
		{
			dt = D.List("select d.docno from xaarinvoice d, xaarinvoicedet l where d.sequenceid=l.docid and d.doctype='IV' and d.mastrefno='"+jobno+"' and l.chgcode='"+chgcode+"' group by d.docno");
		}
		
		return dt;
	}
	
    protected void btn_search_Click(object sender, EventArgs e)
    {
         string type = "";
        string status = "";
        if (Request.QueryString["type"] != null )
        {
            type = SafeValue.SafeString(Request.QueryString["type"]);
        }
        if (Request.QueryString["status"] != null)
        {
            status = SafeValue.SafeString(Request.QueryString["status"]);
        }
        string where = "";
        string sql = string.Format(@"select * from JobInfo");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (txt_PoNo.Text.Trim() != "")
            where = GetWhere(where, "(JobNo like '" + txt_PoNo.Text.Trim() + "%' or QuotationNo like '" + txt_PoNo.Text.Trim() + "%')");
        else if (this.txt_CustId.Text.Length > 0)
        {
            where = GetWhere(where, "CustomerName like '%" + this.txt_CustId.Text.Trim() + "%' or Contact like '%" + this.txt_CustId.Text.Trim() + "%'");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " JobDate >= '" + dateFrom + "' and JobDate < '" + dateTo + "'");
        }
        if (type.Length > 0 && cmb_Type.Text.Trim() == "")
        {
            where = GetWhere(where, " JobStage = '" + type + "'");
        }
		if (cmb_Type.Text.Trim()!="")
        {
            if (cmb_Type.Text == "All")
            {
                
            }
            else
            {
                where = GetWhere(where, " JobType = '" + cmb_Type.Text.Trim() + "'");
            }
        }
        if (status.Length > 0)
        {
            where = GetWhere(where," WorkStatus='"+status+"'");
        }
        if (where.Length > 0)
        {
            sql += " where " + where + " and Attribute = 'Job' order by QuotationNo";
        }
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected string SaveNewJob()
    {
        try
        {
            ASPxDateEdit issueDate = ASPxPopupControl1.FindControl("date_IssueDate") as ASPxDateEdit;
            string issueN = "";
            JobInfo job = new JobInfo();
            DateTime dt = DateTime.Now;
            if (issueDate.Date.IsDaylightSavingTime())
            {
                dt = issueDate.Date;
            }
            issueN = C2Setup.GetNextNo("", "Quotation", dt);
            job.JobDate = dt;
			job.DateTime1 = dt;
            job.Attribute = "Job";
            
            ASPxComboBox cmb_JobType = ASPxPopupControl1.FindControl("cmb_JobType") as ASPxComboBox;
            job.JobType = SafeValue.SafeString(cmb_JobType.Value);
            job.JobStage = SafeValue.SafeString(cmb_JobType.Value);
			job.Mode = "LOCAL";
			job.ViaWh = "N/A";
			job.Item1 = "Yes";
			job.Item2 = "Yes";
            job.Currency = "SGD";
            job.ExRate = 1;
			job.StorageStartDate = new DateTime(1900,1,1);
			job.StorageFreeDays = 0; //new DateTime(1900,1,1);
			string sql = string.Format(@"select Id from JobInfo where Attribute='Temp'");
            string id = "";
            string jobType = "";
            if (job.JobType == "Haulier")
            {
                issueN = "Q-H-" + issueN;
                job.ViaWh = "Normal";
                jobType = "'HTemp'";
                sql += " and JobType=" + jobType;
                id = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
            }
            if (job.JobType == "Transport")
            {
                issueN = "Q-T-" + issueN;
                job.Mode = "LCL";
                job.Note27 = "3% of declared value";
                job.Note28 = "+0.25% additional";
                job.Note29 = "+0.25% additional";
                job.Note30 = "+0.5 % additional per month";
                job.Note23 = "By Sea";
                job.Note24 = "Weekly";

                jobType="'TTemp'";
                sql += " and JobType=" + jobType;
                id = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
            }
            if (job.JobType == "Freight")
            {
                issueN = "Q-F-" + issueN;
                jobType = "'FTemp'";
                sql += " and JobType=" + jobType;
                id = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
            }
            if (job.JobType == "Warehouse")
            {
                issueN = "Q-W-" + issueN;
                jobType = "WTemp'";
                sql += " and JobType="+jobType;
                id = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
            }
			//Main Info
			Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(JobInfo), "Id='" + id + "'");
            JobInfo jobTemp = C2.Manager.ORManager.GetObject(query) as JobInfo;
            ASPxButtonEdit txt_CustomerId = ASPxPopupControl1.FindControl("txt_CustomerId") as ASPxButtonEdit;
            job.CustomerId = txt_CustomerId.Text;
            ASPxTextBox txt_CustomerName = ASPxPopupControl1.FindControl("txt_CustomerName") as ASPxTextBox;
            job.CustomerName = txt_CustomerName.Text;
            ASPxMemo memo_Address = ASPxPopupControl1.FindControl("memo_NewAddress") as ASPxMemo;
            job.CustomerAdd = memo_Address.Text;
            job.OriginAdd = memo_Address.Text;

            ASPxTextBox txt_PostalCode = ASPxPopupControl1.FindControl("txt_NewPostalCode") as ASPxTextBox;
            job.Postalcode = txt_PostalCode.Text;
            ASPxTextBox txt_Contact = ASPxPopupControl1.FindControl("txt_NewContact") as ASPxTextBox;
            job.Contact = txt_Contact.Text;
            ASPxTextBox txt_Tel = ASPxPopupControl1.FindControl("txt_NewTel") as ASPxTextBox;
            job.Tel = txt_Tel.Text;
            ASPxTextBox txt_Email = ASPxPopupControl1.FindControl("txt_NewEmail") as ASPxTextBox;
            job.Email = txt_Email.Text;
            ASPxTextBox txt_Fax = ASPxPopupControl1.FindControl("txt_NewFax") as ASPxTextBox;
            job.Fax = txt_Fax.Text;
            ASPxMemo remark = ASPxPopupControl1.FindControl("txt_NewRemark") as ASPxMemo;
            job.DestinationAdd = remark.Text;
            job.WorkStatus = "Pending";
            if (jobTemp != null)
            {
                job.PayTerm = jobTemp.PayTerm;
                job.Currency = jobTemp.Currency;
                job.ExRate = jobTemp.ExRate;
                job.ExpiryDate = jobTemp.ExpiryDate;
                job.WareHouseId = jobTemp.WareHouseId;
                job.Volumne = jobTemp.Volumne;
                job.VolumneRmk = jobTemp.VolumneRmk;
                job.Remark = jobTemp.Remark;
                job.TripNo = jobTemp.TripNo;
                job.HeadCount = jobTemp.HeadCount;
                job.TruckNo = jobTemp.TruckNo;
                job.PackRmk = jobTemp.PackRmk;
                job.MoveRmk = jobTemp.MoveRmk;
                job.ViaWh = jobTemp.ViaWh;
                job.ItemDes = jobTemp.ItemDes;
                job.ServiceType = jobTemp.ServiceType;
                job.StorageFreeDays = jobTemp.StorageFreeDays;
                job.StorageTotalDays = jobTemp.StorageTotalDays;
                job.Item1 = jobTemp.Item1;
                job.Item2 = jobTemp.Item2;
                job.ItemDetail1 = jobTemp.ItemDetail1;
                job.ItemDetail2 = jobTemp.ItemDetail2;

                job.Item3 = jobTemp.Item3;
                job.ItemValue3 = jobTemp.ItemValue3;
                job.ItemData3 = jobTemp.ItemData3;
                job.ItemPrice3 = jobTemp.ItemPrice3;

                job.Item4 = jobTemp.Item4;
                job.ItemDetail4 = jobTemp.ItemDetail4;
                job.ItemPrice4 = jobTemp.ItemPrice4;

                job.Item5 = jobTemp.Item5;
                job.ItemValue5 = jobTemp.ItemValue5;
                job.ItemPrice5 = jobTemp.ItemPrice5;

                job.Item6 = jobTemp.Item6;
                job.ItemDetail6 = jobTemp.ItemDetail6;
                job.ItemPrice6 = jobTemp.ItemPrice6; ;

                job.Item7 = jobTemp.Item7;
                job.ItemValue7 = jobTemp.ItemValue7;
                job.ItemDetail7 = jobTemp.ItemDetail7;
                job.ItemPrice7 = jobTemp.ItemPrice7;

                job.Item8 = jobTemp.Item8;
                job.ItemValue8 = jobTemp.ItemValue8;
                job.ItemDetail8 = jobTemp.ItemDetail8;
                job.ItemPrice8 = jobTemp.ItemPrice8;

                job.Item9 = jobTemp.Item9;
                job.ItemValue9 = jobTemp.ItemValue9;
                job.ItemDetail9 = jobTemp.ItemDetail9;
                job.ItemPrice9 = jobTemp.ItemPrice9;

                job.Item10 = jobTemp.Item10;
                job.ItemValue10 = jobTemp.ItemValue10;
                job.ItemDetail10 = jobTemp.ItemDetail10;
                job.ItemPrice10 = jobTemp.ItemPrice10;

                job.Item11 = jobTemp.Item11;
                job.ItemDetail11 = jobTemp.ItemDetail11;

                job.Item12 = jobTemp.Item12;
                job.ItemDetail12 = jobTemp.ItemDetail12;

                job.Item13 = jobTemp.Item13;
                job.ItemValue13 = jobTemp.ItemValue13;
                job.ItemData13 = jobTemp.ItemData13;

                job.Item14 = jobTemp.Item14;
                job.ItemValue14 = jobTemp.ItemValue14;
                job.ItemDetail14 = jobTemp.ItemDetail14;
                job.ItemPrice14 = jobTemp.ItemPrice14;

                job.Answer1 = jobTemp.Answer1;
                job.Answer2 = jobTemp.Answer2;
                job.Answer3 = jobTemp.Answer3;
                job.Answer4 = jobTemp.Answer4;
                job.WorkStatus = jobTemp.WorkStatus;

                job.DateTime2 = jobTemp.DateTime2;
                //job.DateTime1 = jobTemp.DateTime1;
                job.DateTime3 = jobTemp.DateTime3;
                job.DateTime4 = jobTemp.DateTime4;

                job.Value1 = jobTemp.Value1;
                job.Value2 = jobTemp.Value2;
                job.Value3 = jobTemp.Value3;
                job.Value4 = jobTemp.Value4;

                job.Notes = jobTemp.Notes;
                job.Attention1 = jobTemp.Attention1;
                job.Attention2 = jobTemp.Attention2;
                job.Attention3 = jobTemp.Attention3;
                job.Attention4 = jobTemp.Attention4;
                job.Attention5 = jobTemp.Attention5;
                job.Attention6 = jobTemp.Attention6;
                job.Attention7 = jobTemp.Attention7;
            }
            else
            {
                #region
                job.Attention1 = @"<p>You can insure separately at a premium of 1.50% of total value.(Min. Premium is $100.00)
                Please be advised that completed insurance application must be submitted to our office prior to pack and move dates. There is no liability on Collin’s Movers upon the decline of insurance. All items in the shipment must be insured. Any policy that covers only a limited number of items in the shipment may be subject to rejection by insurance company.</p>
                <p>This Quotation is based on us providing a normal weekday(Monday to Saturday)services unless otherwise stated. Any substantial variation in the volume will affect our charges. The validity of this quotation is 30 days from the date of issue.</p>
                ";
                job.Attention2 = @"<p>*Preparation : Delivery of cartons and tapes for self packing.</p>
                <p>*Packing Materials : Provide required quantity of packing materials such as various sizes of cartons boxes, Bubble wraps, Tapes etc. Our service includes one time collection of empty cartons after the completion of the move.</p>
                <p>*Pack and Unpack: Wrapping and packing of all fragile to include china, glassware, paintings, ornaments, books files, kitchenware and miscellaneous item and unpacking on destination if not specified.</p>
                <p>*Moving: Provide skilled and experienced movers to pack and move your house hold items.</p>
                <p>*Supervisor : Provide highly experienced and skilled team supervisor during the entire move.</p>
                <p>*Transportation: Provide Collins Movers 22ft covered truck equipped with hydraulic tail gate system. All trucks are equipped with GPS tracking system to monitor the truck movements.</p>
                <p>*MCST Process: Removal permit application and refundable cheque deposit if applicable for your condominium move.</p>
                ";
                job.Attention3 = @"<p>*Insurance coverage</p>
                <p>*Non refundable Condominium administration fee, Lift padding fee or any such fees.</p>
                <p>*Handyman Services.</p>
                <p>*Maid Services.</p>
                <p>*Piano and Safe handling.</p>
                <p>*Additional pickup and delivery unless specified.</p>
                ";
                job.Attention4 = @"<p>This quotation is based on providing a normal weekday(Monday to Saturday) services unless otherwise</p><p> stated. To confirm acceptance of our services please complete the attached Acceptance Sheet and return to</p><p> us bay fax ,mail or email as soon as possible.</p>
                
                ";
                job.Attention5 = @"<p>FULL Payment is required on the FIRST day of the move. We require company’s stamp and authorized</p><p> signature on the acceptance sheet in case payment is made by Company. Please make cheque payable to</p><p> Collins Movers Pte Ltd. For cash payment, Please contact our office beforehand and speak to sales person</p><p> on this agreement.</p>
                <p>&nbsp;&nbsp;</p><p>&nbsp;&nbsp;</p>
                <p>Once a booking has been made any cancellation of job would result in a penalty of 30% upon the contract</p><p> agreed. However within 24 hours prior to the commencement of the move, any cancellation would be</p><p> penalized at 70% of the contract agreed.</p>
                <p>&nbsp;&nbsp;</p><p>&nbsp;&nbsp;</p>
                <p>We thank you once again for the opportunity to be of service to you. We hope that the above rate meets</p><p> your approval and should you require any further information regarding our services, please do not hesitate</p><p> to contact us.</p>
                ";
            
            #endregion
            }


            string userId = HttpContext.Current.User.Identity.Name;

            job.CreateBy = userId;
            job.CreateDateTime = DateTime.Now;
            job.UpdateBy = userId;
            job.UpdateDateTime = DateTime.Now;
            job.QuotationNo = issueN;
            job.DateTime1=DateTime.Now;
            job.DateTime2=DateTime.Now;
            job.Value2=userId;
            Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(job);
            C2Setup.SetNextNo("", "Quotation", issueN, issueDate.Date);

            return job.QuotationNo;
        }
        catch { }
        return "";
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "OK")
        {
            ASPxComboBox cmb_JobType = ASPxPopupControl1.FindControl("cmb_JobType") as ASPxComboBox;
            ASPxButtonEdit txt_CustomerId = ASPxPopupControl1.FindControl("txt_CustomerId") as ASPxButtonEdit;
            ASPxDateEdit issueDate = ASPxPopupControl1.FindControl("date_IssueDate") as ASPxDateEdit;
            if (txt_CustomerId.Text.Trim() == "" &&  txt_CustomerName.Text.Trim() == "")
            {
                e.Result = "Fail! Please enter the Customer";
                return;
            }
            if (cmb_JobType.Text.Trim() == "")
            {
                e.Result = "Fail! Please enter the JobType";
                return;
            }
            else
            {  
                e.Result = SaveNewJob();
            }
        }
    }
    protected void grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "Whs Location")
        {
            string closeInd = SafeValue.SafeString(this.grid.GetRowValues(e.VisibleIndex, "ViaWh"));
            if (closeInd == "Yes")
            {
                e.Cell.BackColor = System.Drawing.Color.Orange;
            }

        }
        if (e.DataColumn.Caption == "Type")
        {
            string closeInd = SafeValue.SafeString(this.grid.GetRowValues(e.VisibleIndex, "JobType"));
            if (closeInd == "OutBound")
            {
                e.Cell.BackColor = System.Drawing.Color.LightBlue;
            }

        }
        if (e.DataColumn.Caption == "Status")
        {
            string closeInd = SafeValue.SafeString(this.grid.GetRowValues(e.VisibleIndex, "WorkStatus"));
            if (closeInd == "Working")
            {
                e.Cell.BackColor = System.Drawing.Color.LightGreen;
            }
            if (closeInd == "Unsuccess")
            {
                e.Cell.BackColor = System.Drawing.Color.Red;
            }

        }
    }
}