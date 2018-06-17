using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_NewJobListBill : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e) {
        
    }
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
			txt_search_dateTo.Date = DateTime.Now;
			if(SafeValue.SafeString(Request.QueryString["n"]) == "0")
				cbb_BillingStatusCode.Text = "Unbilled";
			else
				cbb_BillingStatusCode.Text = "Billed";

			//btn_search_Click(null, null);
		}
	}
    public string xmlChangeToHtml(object par, object contId)
    {
        string res = par.ToString();
        res = res.Replace("&lt;", "<");
        res = res.Replace("&gt;", ">");
        if (res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0)
        {
            res = "<span class='X'>Trips</span>";
        }
        return res;
    }
	protected void btn_search_Click(object sender, EventArgs e)
	{
		string where = "";
        string sql = string.Format(@"select * from(select job.StatusCode,job.Id,job.JobNo,job.QuoteNo,job.JobStatus,job.JobDate,job.YardRef as Depot,job.ClientRefNo,job.ClientId,
job.PermitNo,job.Remark,job.SpecialInstruction,job.EtaDate,job.EtaTime,job.EtdDate,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,job.PartyId,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,job.ShowInvoice_Ind,
(select top 1 Name from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,
job.Terminalcode,job.JobType,
(select count(*) from XAArInvoice where MastRefNo=job.JobNo) as billed,inv.DocNo,
(select count(*) as Cnt from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where mast.MastRefNo=job.JobNo and mast.DocType='IV' ) as Cnt
,tab_h.Weight as CargoWeight,tab_h.Volume,tab_h.ContNo,tab_h.SkuCode,tab_h.QtyOrig,tab_h.BookingNo,tab_h.HblNo,tab_h.Marking1,tab_h.Marking2
,(select count(*) from ctm_jobdet1 where JobNo=job.JobNo) as contsCnt,(select count(*) from ctm_jobdet2 where JobNo=job.JobNo) as tripsCnt,
(select count(*) from ctm_job where BillingRefNo=job.JobNo) as jobsCnt,(select count(*) from ctm_jobdet1 det1 inner join CTM_Job mast on det1.JobNo=mast.JobNo where BillingRefNo=job.JobNo) as subContsCnt,
(select count(*) from ctm_jobdet2 det2 inner join CTM_Job mast on det2.JobNo=mast.JobNo where BillingRefNo=job.JobNo) as subTripsCnt
from CTM_Job as job 
left join(select SkuCode,max(QtyOrig) QtyOrig,max(ContNo) ContNo,BookingNo,h.JobNo,HblNo,max(DoNo) DoNo,max(Marking1) Marking1,max(Marking2) Marking2,max(Weight) Weight,max(Volume) Volume from job_house h inner join  CTM_Job mast on mast.JobNo=h.JobNo group by h.JobNo,HblNo,BookingNo,SkuCode) as tab_h on tab_h.JobNo=job.JobNo
left join(select DocNo,MastRefNo,MastType,LocAmt from XAArInvoice where DocType='IV') as inv on inv.MastRefNo=job.JobNo
) as tab");//
        if (txt_search_jobNo.Text.Trim() != "")
		{
			where = GetWhere(where, string.Format(@"JobNo like '%{0}%'", txt_search_jobNo.Text.Replace("'", "").Trim()));
            //if (txt_search_ContNo.Text.Trim() != "")
            //{
            //    where = GetWhere(where, string.Format(@" isnull(ContainerNo,'') like '%{0}%'", txt_search_ContNo.Text.Replace("'", "").Trim()));
            //}
            if (search_JobType.Text.Length > 0&& search_JobType.Text!="All")
            {
                where = GetWhere(where, " JobType='" +SafeValue.SafeString(search_JobType.Value) + "'");
            }
		}
		else
		{

			if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
			{
				where = GetWhere(where, " datediff(d,'" + txt_search_dateFrom.Date + "',EtaDate)>=0");
			}

			if (txt_Vessel.Text != "")
			{
				where = GetWhere(where, "Vessel='" + txt_Vessel.Text.Replace("'", "").Trim() + "'");
			}

			if (txt_search_dateTo.Date > new DateTime(1900, 1, 1))
			{
				where = GetWhere(where, " datediff(d,'" + txt_search_dateTo.Date + "',EtaDate)<=0");
			}

            //if (cbb_StatusCode.Text != "All")
            //{
            //    where = GetWhere(where, " StatusCode='" + cbb_StatusCode.Text.Trim() + "'");
            //}

			if (btn_ClientId.Text.Length > 0)
			{
				where = GetWhere(where, " PartyId='" + btn_ClientId.Text.Replace("'", "") + "'");
			}

			if (search_JobType.Text!="All")
			{
				where = GetWhere(where, " JobType='" + search_JobType.Text + "'");
			}
		}

		if (where.Length > 0)
		{
			string bst = cbb_BillingStatusCode.Text;
            //if (txt_search_jobNo.Text.Trim() == "")
            {
                if (Request.QueryString["n"] != null && bst == "Unbilled")
                {
                    where = GetWhere(where, " Cnt=0 ");
                }
                else if (Request.QueryString["n"] != null && bst == "Billed")
                {
                    where = GetWhere(where, " Cnt>0 ");
                }
                else if (Request.QueryString["n"] != null && bst == "UnClosed")
                {
                    where = GetWhere(where, " StatusCode='USE' ");
                }
                else
                {
                    where = GetWhere(where, " (Cnt>=0) ");
                }
            }
            sql += " where " + where + " and JobStatus in ('Billing','Closed','Completed') order by EtaDate,JobNo,Id desc, JobDate asc";
        }
        //and  JobType in ('IMP','EXP','LOC','WGR','WDO','TPT','CRA','FRT','ROS','LI','LE','CT','LT','RE')
        //throw new Exception(sql);
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();


    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    public string VilaStatus(string status)
    {
        string strStatus = "";
        if (status == "USE")
        {
            strStatus = "NEW";
        }
        if (status == "CLS")
        {
            strStatus = "COMPLATED";
        }
        if (status == "CNL")
        {
            strStatus = "CANCEL";
        }
        return strStatus;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "InTransit")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }
        return color;
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("UpdateInline"))
            {
                Update_Inline(e, SafeValue.SafeInt(ar[1], -1));
            }
            if (ar[0].Equals("CreateInvline"))
            {
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_JobNo = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_JobNo") as ASPxLabel;
                ASPxLabel lbl_ClientId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_ClientId") as ASPxLabel;
                TextBox txt_cntId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_cntId") as TextBox;
                ASPxLabel lbl_JobType = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_JobType") as ASPxLabel;
                ASPxLabel lbl_QuoteNo = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_QuoteNo") as ASPxLabel;
                e.Result = lbl_JobNo.Text + "," + lbl_JobType.Text + "," + lbl_ClientId.Text+","+lbl_QuoteNo.Text;
            }
        }
    }
    private void Update_Inline(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }

        #region 
        ASPxTextBox txt_cntId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_cntId") as ASPxTextBox;
        ASPxSpinEdit spin_fee1 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "spin_fee1") as ASPxSpinEdit;
        ASPxTextBox txt_feeNote1 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_feeNote1") as ASPxTextBox;
        ASPxSpinEdit spin_fee3 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "spin_fee3") as ASPxSpinEdit;
        ASPxTextBox txt_feeNote3 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_feeNote3") as ASPxTextBox;
        ASPxSpinEdit spin_fee5 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "spin_fee5") as ASPxSpinEdit;
        ASPxTextBox txt_feeNote5 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_feeNote5") as ASPxTextBox;
        ASPxSpinEdit spin_fee6 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "spin_fee6") as ASPxSpinEdit;
        ASPxTextBox txt_feeNote6 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_feeNote6") as ASPxTextBox;
        ASPxSpinEdit spin_fee12 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "spin_fee12") as ASPxSpinEdit;
        ASPxTextBox txt_feeNote12 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_feeNote12") as ASPxTextBox;
        ASPxSpinEdit spin_fee13 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "spin_fee13") as ASPxSpinEdit;
        ASPxTextBox txt_feeNote13 = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_feeNote13") as ASPxTextBox;

        #endregion

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

        string sql = string.Format(@"update CTM_JobDet1 set Fee1=@Fee1,FeeNote1=@FeeNote1,Fee3=@Fee3,FeeNote3=@FeeNote3,Fee5=@Fee5,FeeNote5=@FeeNote5,Fee6=@Fee6,FeeNote6=@FeeNote6,Fee12=@Fee12,FeeNote12=@FeeNote12,Fee13=@Fee13,FeeNote13=@FeeNote13
where Id=@Id");
        #region list
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", txt_cntId.Text, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@Fee1", SafeValue.SafeDecimal(spin_fee1.Value,0), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Fee3", SafeValue.SafeDecimal(spin_fee3.Value), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Fee5", SafeValue.SafeDecimal(spin_fee5.Value), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Fee6", SafeValue.SafeDecimal(spin_fee6.Value), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Fee12", SafeValue.SafeDecimal(spin_fee12.Value), SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Fee13", SafeValue.SafeDecimal(spin_fee13.Value), SqlDbType.Decimal));
       
        list.Add(new ConnectSql_mb.cmdParameters("@FeeNote1",SafeValue.SafeString(txt_feeNote1.Text), SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@FeeNote3", SafeValue.SafeString(txt_feeNote3.Text), SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@FeeNote5", SafeValue.SafeString(txt_feeNote5.Text), SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@FeeNote6", SafeValue.SafeString(txt_feeNote6.Text), SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@FeeNote12", SafeValue.SafeString(txt_feeNote12.Text), SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@FeeNote13", SafeValue.SafeString(txt_feeNote13.Text), SqlDbType.NVarChar));
        #endregion
        if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
        {
            e.Result = "Save Success";
        }
        else
        {
            e.Result = "Save Error";
        }
    }
    private void Create_Invoice(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        ASPxLabel lbl_ClientId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_ClientId") as ASPxLabel;
        ASPxLabel lbl_JobNo = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_JobNo") as ASPxLabel;
        TextBox txt_cntId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_cntId") as TextBox;
        ASPxDateEdit txt_DocDt = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_DocDt") as ASPxDateEdit;
        string user = HttpContext.Current.User.Identity.Name;
        string acCode = EzshipHelper.GetAccArCode("", "SGD");
        string sql = string.Format(@"select Vessel,Voyage,Pol,Pod,EtaDate from CTM_Job where JobNo='{0}'", lbl_JobNo.Text);
        DataTable dt_job = ConnectSql.GetTab(sql);
        DateTime eta = DateTime.Today;
        if(dt_job.Rows.Count>0){
            eta = SafeValue.SafeDate(dt_job.Rows[0]["EtaDate"],DateTime.Today);
        }
        string[] ChgCode_List = { "TRUCKING","FUEL","DHC","PORTENT","CMS","PSA LOLO","","","",""
                                    , "WEIGNING", "WASHING", "REPAIR"
                                    , "DETENTION", "DEMURRAGE", "C/S LOLO","CNL/SHIPMENT"
                                    , "EMF", "OTHER","","PSA STORAGE","EX ONE-WAY","WRONG WEIGHT",
                                    "ELECTRICITY","PERMIT","EXCHANGE DO","SEAL","DOCUMENTATION",
                                    "ERP CHARGES","HEAVYWEIGHT 23/24T","PSA FLEXIBOOK","PSA NO SHOW",
                                    "CHASSIS DEMURRAGE","PARKING","SHIFTING","STAND-BY","MISC 1","MISC 2","MISC 3"};

        sql = string.Format(@"select * from CTM_JobDet1 where JobNo='{0}'", lbl_JobNo.Text);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            DateTime dtime = txt_DocDt.Date;

            string invN = C2Setup.GetNextNo("", "AR-IV", dtime);
            string sql_cnt = string.Format(@"select count(*) from XAArInvoice where PartyTo='{0}' and MastRefNo='{1}'", lbl_ClientId.Text,lbl_JobNo.Text);
            int cnt = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql_cnt), 0);
            string des = "";
            if (cnt == 0)
            {
                #region Inv Mast
                
                sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo)
values('IV','{5:yyyy-MM-dd}','{4}','{0}','{6}','{7}','CASH','{5:yyyy-MM-dd}','',
'SGD','CTM',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}')
select @@IDENTITY", invN, user, acCode, lbl_JobNo.Text, lbl_ClientId.Text,dtime,dtime.Year, dtime.Month);
                string docId = ConnectSql_mb.ExecuteScalar(sql);
                C2Setup.SetNextNo("", "AR-IV", invN, dtime);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string cntNo = SafeValue.SafeString(dt.Rows[i]["ContainerNo"]);
                    string cntType = SafeValue.SafeString(dt.Rows[i]["ContainerType"]);
                    //if()
                    string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType) 
values");
                    sql = "";
                    des += cntNo + " / " + cntType + "   ";
                    for (int j = 0, j1 = 0; j < ChgCode_List.Length; j++)
                    {
                        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j]);
                        if (ChgCode_List[j].Equals("TRUCKING"))
                        {
                            sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j]+" "+cntType.Substring(0, 2));
                        }
                        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                        string chgCodeId = "";
                        string note = "";
                        if (dt_chgCode.Rows.Count > 0)
                        {
                           
                            chgCodeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeId"]);
                            note = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeDes"]);
                        }
                        else {
                            chgCodeId = ChgCode_List[j];
                        }
                        if (!ChgCode_List[j].Equals("") && !ChgCode_List[j].Equals("MISC") && !ChgCode_List[j].Equals("REMARK"))
                        {
                            j1++;

                            decimal temp_fee = SafeValue.SafeDecimal(dt.Rows[i]["Fee" + (j + 1)]);
                            if (temp_fee != 0)
                            {
                                note += dt.Rows[i]["FeeNote" + (j + 1)].ToString();
                                string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','31','CR','{3}','{4}','Z',1,{5},'','SGD',1,0,0,{5},{5},{5},'{6}','{7}','{8}')", docId, invN, j1, chgCodeId, note, temp_fee, lbl_JobNo.Text, cntNo, "CTM");
                                sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
                            }

                        }
                    }
                    if (sql.Length > 0)
                    {
                        sql = sql_part1 + sql;
                        int re = ConnectSql.ExecuteSql(sql);
                        e.Result = invN;
                        des = "Vessel/Voy:" + dt_job.Rows[0]["Vessel"] + " / " + dt_job.Rows[0]["Voyage"] + "\n" + "Pol/Pod:" + dt_job.Rows[0]["Pol"] + " / " + dt_job.Rows[0]["Pod"] + "\n" + "Eta:" + eta.ToString("dd.MM.yy") + "\n" + "Container No: " + des;
                        UpdateMaster(SafeValue.SafeInt(docId, 0),des);


                    }
                }
                #endregion
            }
            else
            {
                string sql_id = string.Format(@"select SequenceId,DocNo from XAArInvoice where PartyTo='{0}'", lbl_ClientId.Text);
                DataTable dt_inv = ConnectSql.GetTab(sql_id);
                int sequenceId = 0;
                if (dt_inv.Rows.Count > 0)
                {
                    sequenceId = SafeValue.SafeInt(dt_inv.Rows[0]["SequenceId"], 0);
                    invN = SafeValue.SafeString(dt_inv.Rows[0]["DocNo"]);
                }
                #region Inv Det
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string cntNo = SafeValue.SafeString(dt.Rows[i]["ContainerNo"]);
                    string cntType = SafeValue.SafeString(dt.Rows[i]["ContainerType"]);

                    //if()
                    string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType)
values");
                    sql = "";
                    des += cntNo + " / " + cntType + "     ";
                    for (int j = 0, j1 = 0; j < ChgCode_List.Length; j++)
                    {
                        sql_cnt = string.Format(@"select count(*) from XAArInvoiceDet where ChgCode='{0}' and JobRefNo='{1}'", ChgCode_List[j], cntNo);
                        cnt = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql_cnt), 0);
                        if (cnt == 0)
                        {
                            string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j]);
                            if (ChgCode_List[j].Equals("TRUCKING"))
                            {
                                sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes from XXChgCode where ChgcodeDes like '{0}%'", ChgCode_List[j] + " " + cntType.Substring(0, 2));
                            }
                            DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                            string chgCodeId = "";
                            string note = "";
                            if (dt_chgCode.Rows.Count > 0)
                            {

                                chgCodeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeId"]);
                                note = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeDes"]);
                            }
                            else
                            {
                                chgCodeId = ChgCode_List[j];
                            }
                            if (!ChgCode_List[j].Equals("") && !ChgCode_List[j].Equals("MISC") && !ChgCode_List[j].Equals("REMARK"))
                            {
                                j1++;

                                decimal temp_fee = SafeValue.SafeDecimal(dt.Rows[i]["Fee" + (j + 1)]);
                                if (temp_fee != 0)
                                {
                                    note += dt.Rows[i]["FeeNote" + (j + 1)].ToString();
                                    string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','{4}','Z',1,{5},'','SGD',1,0,0,{5},{5},{5},'{6}','{7}','{8}')", sequenceId, invN, j1, chgCodeId, note, temp_fee, lbl_JobNo.Text, cntNo, "CTM");
                                    sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
                                }
                            }
                        }
                    }
                    if (sql.Length > 0)
                    {
                        sql = sql_part1 + sql;
                        int re = ConnectSql.ExecuteSql(sql);
                        e.Result = invN;
                        des = "Vessel/Voy:" + dt_job.Rows[0]["Vessel"] + " / " + dt_job.Rows[0]["Voyage"] + "\n" + "Pol/Pod:" + dt_job.Rows[0]["Pol"] + " / " + dt_job.Rows[0]["Pod"] + "\n" + "Eta:" + eta.ToString("dd.MM.yy") + "\n" + "Container No: " + des;
                        UpdateMaster(SafeValue.SafeInt(sequenceId, 0),des);
                    }
                }
                #endregion
            }
        }

    }
    private void UpdateMaster(int docId,string des)
    {
        string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (tab.Rows[i]["AcSource"].ToString() == "CR")
            {
                docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
            else
            {
                docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
        }


        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        balAmt += SafeValue.SafeDecimal(Manager.ORManager.GetDataSet(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}',Description='{4}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId,des);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    protected void btn_CreateInv_Init(object sender, EventArgs e)
    {
        ASPxButton btn=sender as ASPxButton;
        GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;

        string jobNo =SafeValue.SafeString(this.grid_Transport.GetRowValues(container.VisibleIndex, "JobNo"));
        string sql="";
        string invNo="";
        sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}' and MastType='CTM'", jobNo);
        invNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (invNo != "")
        {
            btn.Text = invNo;
            btn.ClientSideEvents.Click = String.Format("function (s, e) {{ open_inv_page('{0}'); }}", invNo);
        }
        else
        {
            btn.Text = "Create Inv";
            btn.ClientInstanceName = String.Format("btn_CreateInv{0}", container.VisibleIndex);
            btn.ClientSideEvents.Click = String.Format("function (s, e) {{ inv_create_inline(btn_CreateInv{0},{0}); }}", container.VisibleIndex);
        }
    }
    protected void grid_Transport_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {

    }
    protected void grid_Transport_PageSizeChanged(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void grid_Transport_PageIndexChanged(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
}