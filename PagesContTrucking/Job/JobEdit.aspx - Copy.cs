using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_JobEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["CTM_Job"] = null;
            if (Request.QueryString["jobNo"] != null && Request.QueryString["jobNo"].ToString() != "0")
            {
                txt_search_JobNo.Text = Request.QueryString["jobNo"].ToString();
                Session["CTM_Job"] = " jobNo='" + txt_search_JobNo.Text + "'";
                this.dsJob.FilterExpression = " jobNo='" + txt_search_JobNo.Text + "'";
                if (this.grid_job.GetRow(0) != null)
                    this.grid_job.StartEdit(0);
            }
            else
            {
                this.grid_job.AddNewRow();
            }
        }

        if (Session["CTM_Job"] != null)
        {
            this.dsJob.FilterExpression = Session["CTM_Job"].ToString();
            if (this.grid_job.GetRow(0) != null)
                this.grid_job.StartEdit(0);
        }
    }

    #region Job
    protected void grid_job_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJob));
        }
    }

    protected void grid_job_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["JobDate"] = DateTime.Now;
        e.NewValues["EtaDate"] = DateTime.Now;
        e.NewValues["EtdDate"] = DateTime.Now;
        e.NewValues["CodDate"] = DateTime.Now;
        e.NewValues["JobType"] = "";
        e.NewValues["IsTrucking"] = "Yes";
        e.NewValues["Pod"] = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
    }

    protected void grid_job_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Void")
        {
            e.Result = job_void();
        }

        if (s == "Close")
        {
            e.Result = job_close();
        }

        if (s == "AutoBilling")
        {
            job_auto_billing();
        }
        if(s=="AddGR"){
            string doNo = add_wh("DOIN", "IMPORT", "IN");
            container_add(doNo, "IN");
            e.Result = doNo;
        }
        if (s == "AddDO")
        {
            string doNo = add_wh("DOOUT", "EXPORT", "OUT");
            container_add(doNo, "OUT");
            e.Result = doNo;

        }
    }

    private void job_auto_billing()
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox txt_JobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string user = HttpContext.Current.User.Identity.Name; ;
        string acCode = EzshipHelper.GetAccArCode("", "SGD");
        string[] ChgCode_List = { "TRUCKING S", "WASHING Z", "CHASSIS DEMURRAGE S"
                                    , "W/HANDLING Z", "REPAIR Z", "PARKING S"
                                    , "FUEL S", "DETENTION Z", "SHIFTING S"
                                    , "DHC Z", "DEMURRAGE Z", "STAND-BY"
                                    , "PORTNET Z", "STORAGE Z", ""
                                    , "CMS S", "EXTRA ONE WAY S", ""
                                    , "OUTSIDE JURONG S", "ELECTRICITY Z", ""
                                    , "AIRPORT SURCHARGE S", "PERMIT S", "MISC"
                                    , "MCE", "EXCHANGE DO S", "REMARK" };
        string sql = string.Format(@"select * from CTM_JobDet1 where JobNo='{0}'", txt_JobNo.Text);
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DateTime dtime = DateTime.Now;
            string invN = C2Setup.GetNextNo("", "AR-IV", dtime);
            sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo)
values('IV',getdate(),'','{0}',Year(getdate()),Month(getdate()),'CASH',getdate(),'',
'SGD','CTM',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}')
select @@IDENTITY", invN, user, acCode, txt_JobNo.Text);
            string docId = ConnectSql_mb.ExecuteScalar(sql);
            C2Setup.SetNextNo("", "AR-IV", invN, dtime);
            string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo)
values");
            sql = "";
            for (int j = 0, j1 = 0; j < ChgCode_List.Length; j++)
            {
                if (!ChgCode_List[j].Equals("") && !ChgCode_List[j].Equals("MISC") && !ChgCode_List[j].Equals("REMARK"))
                {
                    j1++;
                    string temp_fee = dt.Rows[i]["Fee" + (j + 1)].ToString();
                    string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','','Z',1,'{4}','','SGD',1,0,0,'{4}','{4}','{4}','{5}')", docId, txt_JobNo.Text, j1, ChgCode_List[j], temp_fee, invN);
                    sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
                }
            }

            if (sql.Length > 0)
            {
                sql = sql_part1 + sql;
                int re = ConnectSql.ExecuteSql(sql);
            }
        }
    }

    private string job_close()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxMemo memo_CLSRMK = pageControl.FindControl("memo_CLSRMK") as ASPxMemo;
        if (memo_CLSRMK.Text.Trim().Length == 0)
        {
            return "Request Remark!";
        }

        string sql = "update CTM_Job set StatusCode=case when StatusCode='CLS' then 'USE' else 'CLS' end where Id=" + Id.Text;
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            sql = string.Format(@"select StatusCode from CTM_Job where Id={0}", Id.Text);
            string status = ConnectSql.ExecuteScalar(sql).ToString();
            CtmJobEventLog log = new CtmJobEventLog();
            log.CreateDateTime = DateTime.Now;
            log.Controller = HttpContext.Current.User.Identity.Name;
            log.JobNo = txt_JobNo.Text;
            log.JobStatus = status;
            log.Remark = memo_CLSRMK.Text;

            sql = string.Format(@"SELECT 
(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='IV') Rev1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsAgtRate),2)),0) FROM SeaImport  WHERE (RefNo = mast.RefNo) AND (TsAgtRate > 0))*mast.ExRate as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='DN') Rev3
,(SELECT isnull(sum(CollectAmount),0) FROM SeaImport  WHERE (RefNo = mast.RefNo))*mast.ExRate Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SI' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SI' and DocType='SC')  as Cost1
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='CN') Cost2
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsImpRate),2)),0) FROM SeaImport WHERE (RefNo = mast.RefNo) AND (TsImpRate > 0))*mast.ExRate as Cost3
,( SELECT sum(SaleLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobType ='SI') Cost4
FROM SeaImportRef mast
where mast.RefNo='{0}'", txt_JobNo.Text);
            DataTable dt_PL = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            if (dt_PL.Rows.Count == 1)
            {
                DataRow dr = dt_PL.Rows[0];
                decimal IV = SafeValue.SafeDecimal(dr["Rev1"], 0);
                decimal TsAgent = SafeValue.SafeDecimal(dr["Rev2"], 0);
                decimal DN = SafeValue.SafeDecimal(dr["Rev3"], 0);
                decimal FrCollect = SafeValue.SafeDecimal(dr["Rev4"], 0);
                decimal PLVO = SafeValue.SafeDecimal(dr["Cost1"], 0);
                decimal CN = SafeValue.SafeDecimal(dr["Cost2"], 0);
                decimal TsImp = SafeValue.SafeDecimal(dr["Cost3"], 0);
                decimal Other = SafeValue.SafeDecimal(dr["Cost4"], 0);

                log.Remark = memo_CLSRMK.Text;
                log.Value1 = IV;
                log.Value2 = TsAgent;
                log.Value3 = DN;
                log.Value4 = FrCollect;
                log.Value5 = PLVO;
                log.Value6 = CN;
                log.Value7 = TsImp;
                log.Value8 = Other;
            }

            C2.Manager.ORManager.StartTracking(log, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(log);

            return "";
        }

        return "error";
    }

    private string job_void()
    {
        ASPxLabel Id = this.grid_job.FindEditFormTemplateControl("lb_Id") as ASPxLabel;
        string sql = "update CTM_Job set StatusCode=case when StatusCode='CNL' then 'USE' else 'CNL' end where Id=" + Id.Text;
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            return "";
        }

        return "error";
    }

    private void job_save()
    {
        try
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            string jobNo = SafeValue.SafeString(txt_JobNo.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + jobNo + "'");
            C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;

            ASPxDateEdit jobDate = this.grid_job.FindEditFormTemplateControl("txt_JobDate") as ASPxDateEdit;
            bool isNew = false;
            if (ctmJob == null)
            {
                isNew = true;
                ctmJob = new C2.CtmJob();
                ctmJob.JobNo = C2Setup.GetNextNo("", "CTM_Job", jobDate.Date);
            }

            ASPxDateEdit jobEta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
            //ASPxDateEdit jobEtd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            //ASPxDateEdit jobCod = pageControl.FindControl("date_Cod") as ASPxDateEdit;
            //ASPxButtonEdit partyId = pageControl.FindControl("btn_PartyId") as ASPxButtonEdit;
            ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
            ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
            ASPxButtonEdit carrier = pageControl.FindControl("btn_CarrierId") as ASPxButtonEdit;
            //ASPxTextBox CarrierBlNo = pageControl.FindControl("txt_CarrierBlNo") as ASPxTextBox;
            ASPxTextBox CarrierBkgNo = pageControl.FindControl("txt_CarrierBkgNo") as ASPxTextBox;
            ASPxComboBox Terminal = pageControl.FindControl("cbb_Terminal") as ASPxComboBox;
            ASPxMemo PickupFrom = pageControl.FindControl("txt_PickupFrom") as ASPxMemo;
            ASPxMemo DeliveryTo = pageControl.FindControl("txt_DeliveryTo") as ASPxMemo;
            ASPxTextBox txt_WarehouseAddress = pageControl.FindControl("txt_WarehouseAddress") as ASPxTextBox;
            //ASPxMemo txt_PortnetRef = pageControl.FindControl("txt_PortnetRef") as ASPxMemo;
            ASPxMemo txt_YardRef = pageControl.FindControl("txt_YardRef") as ASPxMemo;
            ASPxMemo Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
            ASPxMemo txt_PermitNo = pageControl.FindControl("txt_PermitNo") as ASPxMemo;
            ASPxMemo SpecialInstruction = pageControl.FindControl("txt_SpecialInstruction") as ASPxMemo;
            //ASPxButtonEdit txt_Pol = pageControl.FindControl("txt_Pol") as ASPxButtonEdit;
            //ASPxButtonEdit txt_Pod = pageControl.FindControl("txt_Pod") as ASPxButtonEdit;
            ASPxTextBox txt_EtaTime = pageControl.FindControl("txt_EtaTime") as ASPxTextBox;
            //ASPxTextBox txt_EtdTime = pageControl.FindControl("txt_EtdTime") as ASPxTextBox;
            //ASPxTextBox txt_CodTime = pageControl.FindControl("txt_CodTime") as ASPxTextBox;
            ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
            ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
            ASPxTextBox txt_ClientRefNo = this.grid_job.FindEditFormTemplateControl("txt_ClientRefNo") as ASPxTextBox;
            //ASPxButtonEdit btn_HaulierId = pageControl.FindControl("btn_HaulierId") as ASPxButtonEdit;
            ASPxTextBox txt_OperatorCode = pageControl.FindControl("txt_OperatorCode") as ASPxTextBox;

            ASPxComboBox cmb_IsTrucking = this.grid_job.FindEditFormTemplateControl("cmb_IsTrucking") as ASPxComboBox;
            ASPxComboBox cmb_IsWarehouse = this.grid_job.FindEditFormTemplateControl("cmb_IsWarehouse") as ASPxComboBox;
            ASPxComboBox cmb_IsFreight = this.grid_job.FindEditFormTemplateControl("cmb_IsFreight") as ASPxComboBox;
            ASPxComboBox cmb_IsLocal = this.grid_job.FindEditFormTemplateControl("cmb_IsLocal") as ASPxComboBox;
            ASPxComboBox cmb_IsAdhoc = this.grid_job.FindEditFormTemplateControl("cmb_IsAdhoc") as ASPxComboBox;
            ASPxComboBox cmb_IsOthers = this.grid_job.FindEditFormTemplateControl("cmb_IsOthers") as ASPxComboBox;

            ctmJob.JobDate = SafeValue.SafeDate(jobDate.Date, new DateTime(1753, 1, 1));
            ctmJob.EtaDate = SafeValue.SafeDate(jobEta.Date, new DateTime(1753, 1, 1));
            //ctmJob.EtdDate = SafeValue.SafeDate(jobEtd.Date, new DateTime(1753, 1, 1));
            //ctmJob.CodDate = SafeValue.SafeDate(jobCod.Date, new DateTime(1753, 1, 1));
            //ctmJob.PartyId = partyId.Text;
            ctmJob.Vessel = ves.Text;
            ctmJob.Voyage = voy.Text;
            ctmJob.CarrierId = carrier.Text;
            //ctmJob.CarrierBlNo = CarrierBlNo.Text;
            ctmJob.CarrierBkgNo = CarrierBkgNo.Text;
            ctmJob.Terminalcode = SafeValue.SafeString(Terminal.Value);
            ctmJob.PickupFrom = PickupFrom.Text;
            ctmJob.DeliveryTo = DeliveryTo.Text;
            ctmJob.WarehouseAddress = txt_WarehouseAddress.Text;
            //ctmJob.PortnetRef = txt_PortnetRef.Text;
            ctmJob.YardRef = txt_YardRef.Text;
            ctmJob.Remark = Remark.Text;
            ctmJob.PermitNo = txt_PermitNo.Text;
            ctmJob.SpecialInstruction = SpecialInstruction.Text;
            //ctmJob.Pol = txt_Pol.Text;
            //ctmJob.Pod = txt_Pod.Text;
            ctmJob.EtaTime = txt_EtaTime.Text;
            //ctmJob.EtdTime = txt_EtdTime.Text;
            //ctmJob.CodTime = txt_CodTime.Text;
            ctmJob.JobType = cbb_JobType.Text;
            ctmJob.ClientId = btn_ClientId.Text;
            ctmJob.ClientRefNo = txt_ClientRefNo.Text;
            //ctmJob.HaulierId = btn_HaulierId.Text;
            ctmJob.OperatorCode = txt_OperatorCode.Text;

            ctmJob.IsTrucking = cmb_IsTrucking.Text;
            ctmJob.IsWarehouse = cmb_IsWarehouse.Text;
            ctmJob.IsFreight = cmb_IsFreight.Text;
            ctmJob.IsLocal = cmb_IsLocal.Text;
            ctmJob.IsAdhoc = cmb_IsAdhoc.Text;
            ctmJob.IsOthers = cmb_IsOthers.Text;

            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                ctmJob.StatusCode = "USE";

                ctmJob.CreateBy = userId;
                ctmJob.CreateDateTime = DateTime.Now;
                ctmJob.UpdateBy = userId;
                ctmJob.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(ctmJob, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(ctmJob);
            }
            else
            {
                ctmJob.UpdateBy = userId;
                ctmJob.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(ctmJob, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(ctmJob);
            }

            if (isNew)
            {
                txt_JobNo.Text = ctmJob.JobNo;
                //txt_search_JobNo.Text = txt_JobNo.Text;
                C2Setup.SetNextNo("", "CTM_Job", ctmJob.JobNo, jobDate.Date);
            }

            Session["CTM_Job"] = "JobNo='" + ctmJob.JobNo + "'";
            this.dsJob.FilterExpression = Session["CTM_Job"].ToString();
            if (this.grid_job.GetRow(0) != null)
                this.grid_job.StartEdit(0);
        }
        catch { }
    }

    private string add_wh(string refType,string type,string doType) {
        string doNo = "";

        try
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxDateEdit jobDate = this.grid_job.FindEditFormTemplateControl("txt_JobDate") as ASPxDateEdit;
            WhDo whDo  = new WhDo();
            doNo = C2Setup.GetNextNo("", refType, jobDate.Date);


            ASPxDateEdit jobEta = pageControl.FindControl("date_Eta") as ASPxDateEdit;
            //ASPxDateEdit jobEtd = pageControl.FindControl("date_Etd") as ASPxDateEdit;
            //ASPxDateEdit jobCod = pageControl.FindControl("date_Cod") as ASPxDateEdit;
            //ASPxButtonEdit partyId = pageControl.FindControl("btn_PartyId") as ASPxButtonEdit;
            ASPxTextBox ves = pageControl.FindControl("txt_Ves") as ASPxTextBox;
            ASPxTextBox voy = pageControl.FindControl("txt_Voy") as ASPxTextBox;
            ASPxButtonEdit carrier = pageControl.FindControl("btn_CarrierId") as ASPxButtonEdit;
            ASPxTextBox CarrierBkgNo = pageControl.FindControl("txt_CarrierBkgNo") as ASPxTextBox;
            ASPxComboBox Terminal = pageControl.FindControl("cbb_Terminal") as ASPxComboBox;
            ASPxMemo PickupFrom = pageControl.FindControl("txt_PickupFrom") as ASPxMemo;
            ASPxMemo DeliveryTo = pageControl.FindControl("txt_DeliveryTo") as ASPxMemo;
            ASPxTextBox txt_WarehouseAddress = pageControl.FindControl("txt_WarehouseAddress") as ASPxTextBox;
            ASPxMemo txt_YardRef = pageControl.FindControl("txt_YardRef") as ASPxMemo;
            ASPxMemo Remark = pageControl.FindControl("txt_Remark") as ASPxMemo;
            ASPxMemo txt_PermitNo = pageControl.FindControl("txt_PermitNo") as ASPxMemo;
            ASPxMemo SpecialInstruction = pageControl.FindControl("txt_SpecialInstruction") as ASPxMemo;

            ASPxTextBox txt_EtaTime = pageControl.FindControl("txt_EtaTime") as ASPxTextBox;
            ASPxButtonEdit btn_ClientId = this.grid_job.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
            ASPxTextBox txt_ClientName = this.grid_job.FindEditFormTemplateControl("txt_ClientName") as ASPxTextBox;
            ASPxTextBox txt_ClientRefNo = this.grid_job.FindEditFormTemplateControl("txt_ClientRefNo") as ASPxTextBox;
            ASPxTextBox txt_OperatorCode = pageControl.FindControl("txt_OperatorCode") as ASPxTextBox;

            whDo.DoDate = SafeValue.SafeDate(jobDate.Date, new DateTime(1753, 1, 1));
            whDo.Eta = SafeValue.SafeDate(jobEta.Date, new DateTime(1753, 1, 1));

            whDo.Vessel = ves.Text;
            whDo.Voyage = voy.Text;

            whDo.CollectFrom = PickupFrom.Text;
            whDo.DeliveryTo = DeliveryTo.Text;
            whDo.WareHouseId = txt_WarehouseAddress.Text;

            whDo.Remark = Remark.Text;
            whDo.PermitNo = txt_PermitNo.Text;

            whDo.Priority = type;
            whDo.PartyId = btn_ClientId.Text;
            whDo.PartyName = txt_ClientName.Text;
            whDo.CustomerReference = txt_ClientRefNo.Text;
            whDo.ContainerYard = txt_YardRef.Text;

            whDo.DoType = doType;
            whDo.StatusCode = "USE";
            whDo.CreateBy = EzshipHelper.GetUserName();
            whDo.CreateDateTime = DateTime.Now;
            whDo.DoNo = doNo;

            C2Setup.SetNextNo("", refType, doNo, jobDate.Date);
            Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(whDo);



        }
        catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
        return doNo;
    }

    private void container_add(string doNo,string doType) {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxDateEdit jobDate = this.grid_job.FindEditFormTemplateControl("txt_JobDate") as ASPxDateEdit;
        string sql = string.Format(@"select count(*) from Ctm_JobDet1 where JobNo='{0}'", txt_JobNo.Text);
        int n1 =SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);
        sql = string.Format(@"select count(*) from Wh_DoDet3 where DoNo='{0}'", doNo);
        int n2 = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (n1>n2)
        {
            sql = string.Format(@"select * from Ctm_JobDet1 where JobNo='{0}'", txt_JobNo.Text);
            DataTable tab = ConnectSql.GetTab(sql);
            string userId = HttpContext.Current.User.Identity.Name;
            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    
                    decimal weight = SafeValue.SafeDecimal(tab.Rows[i]["Weight"], 0);
                    decimal m3 = SafeValue.SafeDecimal(tab.Rows[i]["Volume"], 0); ;
                    int qty = SafeValue.SafeInt(tab.Rows[i]["QTY"], 0);
                    string containerNo = SafeValue.SafeString(tab.Rows[i]["ContainerNo"]);
                    string sealNo = SafeValue.SafeString(tab.Rows[i]["SealNo"]);
                    string pkgType = SafeValue.SafeString(tab.Rows[i]["PackageType"]);
                    string containerType = SafeValue.SafeString(tab.Rows[i]["ContainerType"]);

                    sql = string.Format(@"select count(*) from Wh_DoDet3 where DoNo='{0}' and ContainerNo='{1}'",doNo, containerNo);
                    int result = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                    if (result == 0)
                    {
                        sql = string.Format(@"insert into Wh_DoDet3(DoNo,DoType,Weight,M3,Qty,ContainerNo,SealNo,PkgType,ContainerType,CreateBy,CreateDateTime,UpdateBy,UpdateDateTime,JobStart) 
Values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}',getdate(),'{9}',getdate(),'{10}')", doNo, doType, weight, m3, qty, containerNo, sealNo, pkgType, containerType, userId, jobDate.Date);
                        ConnectSql.ExecuteSql(sql);
                    }
                }
            }
        }
    }

    protected void grid_job_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "save")
        {
            job_save();
        }
    }

    protected void grid_job_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid_job.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            //ASPxTextBox partyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox txt_CarrierName = pageControl.FindControl("txt_CarrierName") as ASPxTextBox;
            //ASPxTextBox txt_ClientName = pageControl.FindControl("txt_ClientName") as ASPxTextBox;
            ASPxTextBox txt_ClientName = this.grid_job.FindEditFormTemplateControl("txt_ClientName") as ASPxTextBox;
            ASPxTextBox txt_HaulierName = pageControl.FindControl("txt_HaulierName") as ASPxTextBox;

            //partyName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "PartyId" }));
            txt_CarrierName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "CarrierId" }));
            txt_ClientName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "ClientId" }));
            //txt_HaulierName.Text = EzshipHelper.GetPartyName(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "HaulierId" }));

            string StatusCode = SafeValue.SafeString(this.grid_job.GetRowValues(this.grid_job.EditingRowVisibleIndex, new string[] { "StatusCode" }), "USE");
            switch (StatusCode)
            {
                case "CLS":
                    //ASPxButton btnCLS = this.grid_job.FindEditFormTemplateControl("btn_JobClose") as ASPxButton;
                    ASPxPageControl tabs = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
                    ASPxButton btnCLS = tabs.FindControl("btn_JobClose") as ASPxButton;
                    btnCLS.Text = "Open";
                    break;
                case "CNL":
                    ASPxButton btnCNL = this.grid_job.FindEditFormTemplateControl("btn_JobVoid") as ASPxButton;
                    btnCNL.Text = "UnVoid";
                    break;
                default:
                    break;
            }

            EzshipHelper_Authority.Bind_Authority(this.grid_job);
            EzshipHelper_Authority.Bind_Authority(pageControl);
        }
    }

    #endregion

    #region Cargo
    protected void grid_Cont_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet1));
        }
    }

    protected void grid_Cont_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsCont.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }

    protected void grid_Cont_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    protected void grid_Cont_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxMemo txt_YardRef = pageControl.FindControl("txt_YardRef") as ASPxMemo;
        e.NewValues["RequestDate"] = DateTime.Now;
        e.NewValues["ScheduleDate"] = DateTime.Now;
        e.NewValues["CfsInDate"] = DateTime.Now;
        e.NewValues["CfsOutDate"] = DateTime.Now;
        e.NewValues["YardPickupDate"] = DateTime.Now;
        e.NewValues["YardReturnDate"] = DateTime.Now;
        e.NewValues["CdtDate"] = DateTime.Now;
        e.NewValues["YardExpiryDate"] = DateTime.Now;
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["UrgentInd"] = "N";
        e.NewValues["F5Ind"] = "N";
        e.NewValues["StatusCode"] = "New";
        e.NewValues["Permit"] = "N";
        e.NewValues["YardAddress"] = txt_YardRef.Text;
    }

    protected void grid_Cont_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["F5Ind"] = SafeValue.SafeString(e.NewValues["F5Ind"], "");
        e.NewValues["UrgentInd"] = SafeValue.SafeString(e.NewValues["UrgentInd"], "");
        e.NewValues["PortnetStatus"] = SafeValue.SafeString(e.NewValues["PortnetStatus"], "");
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 1, 1));
        e.NewValues["RequestDate"] = SafeValue.SafeDate(e.NewValues["RequestDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsInDate"] = SafeValue.SafeDate(e.NewValues["CfsInDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsOutDate"] = SafeValue.SafeDate(e.NewValues["CfsOutDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardPickupDate"] = SafeValue.SafeDate(e.NewValues["YardPickupDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardReturnDate"] = SafeValue.SafeDate(e.NewValues["YardReturnDate"], new DateTime(1753, 1, 1));
        e.NewValues["CdtDate"] = SafeValue.SafeDate(e.NewValues["CdtDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardExpiryDate"] = SafeValue.SafeDate(e.NewValues["YardExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"], "");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "");
    }

    protected void grid_Cont_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["F5Ind"] = SafeValue.SafeString(e.NewValues["F5Ind"], "");
        e.NewValues["UrgentInd"] = SafeValue.SafeString(e.NewValues["UrgentInd"], "");
        e.NewValues["PortnetStatus"] = SafeValue.SafeString(e.NewValues["PortnetStatus"], "");
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 1, 1));
        e.NewValues["RequestDate"] = SafeValue.SafeDate(e.NewValues["RequestDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsInDate"] = SafeValue.SafeDate(e.NewValues["CfsInDate"], new DateTime(1753, 1, 1));
        e.NewValues["CfsOutDate"] = SafeValue.SafeDate(e.NewValues["CfsOutDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardPickupDate"] = SafeValue.SafeDate(e.NewValues["YardPickupDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardReturnDate"] = SafeValue.SafeDate(e.NewValues["YardReturnDate"], new DateTime(1753, 1, 1));
        e.NewValues["CdtDate"] = SafeValue.SafeDate(e.NewValues["CdtDate"], new DateTime(1753, 1, 1));
        e.NewValues["YardExpiryDate"] = SafeValue.SafeDate(e.NewValues["YardExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"], "");
        e.NewValues["StatusCode"] = SafeValue.SafeString(e.NewValues["StatusCode"], "");
    }

    protected void grid_Cont_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }

    protected void grid_Cont_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        //Trip_new_auto(jobNo.Text);
    }

    protected void grid_Cont_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
        if (SafeValue.SafeString(e.NewValues["ContainerNo"]) != SafeValue.SafeString(e.OldValues["ContainerNo"]))
        {
            string sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'", e.Keys["Id"], SafeValue.SafeString(e.NewValues["ContainerNo"]));
            ConnectSql.ExecuteSql(sql);
        }
    }

    private void Trip_new_auto(string JobNo)
    {
        string sql = string.Format(@"select * From ctm_job where JobNo='{0}'", JobNo);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        DataRow dr_job = dt.Rows[0];
        sql = string.Format(@"with tb1 as (
select * from ctm_jobdet1 where jobno='{0}'
),
tb2 as (
select * from ctm_jobdet2 where jobno='{0}'
)
select Id,ContainerNo from (
select *,(select count(*) from tb2 where tb1.Id=tb2.Det1Id) as tripCount from tb1 
) as tb where tripCount=0", JobNo);
        dt = ConnectSql_mb.GetDataTable(sql);
        sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance) values");
        string values = "";
        //string tripcode1 = "EMPTY", tripcode2 = "LADEN";
        //string stage_from1 = "Yard", stage_from2 = "Warehouse";
        //if (dr_job["JobType"].ToString().IndexOf("IMP") > -1)
        //{
        //    tripcode1 = "LADEN";
        //    tripcode2 = "EMPTY";
        //    stage_from1 = "Port";
        //}
        string JobType = "IMP";
        switch (dr_job["JobType"].ToString())
        {
            case "IMP":
                break;
            case "EXP":
                JobType = "EXP";
                break;
            case "LOC":
                JobType = "LOC";
                break;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //values += (values.Length > 0 ? "," : "") + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','B1','N','{7}','Pending','{5}','Normal','N'),('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','B1','N','{8}','Pending','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], dr_job["PickupFrom"], dr_job["DeliveryTo"], dt.Rows[i]["Id"], tripcode1, tripcode2, stage_from1, stage_from2);
            //values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','','N','','','','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"]);
            //values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','','N','','','','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"]);
            values += (values.Length > 0 ? "," : "") + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"], "Port", JobType);
            values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"], "Warehouse", JobType);
            values += "," + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{6}','Normal','N')", JobNo, dt.Rows[i]["ContainerNo"], "", "", dt.Rows[i]["Id"], "Yard", JobType);
        }

        if (values.Length > 0)
        {
            sql = sql + values;
            try
            {
                int i = ConnectSql.ExecuteSql(sql);
            }
            catch { }
        }
    }

    protected void btn_cont_auto_invoice_Click(object sender, EventArgs e)
    {
    }

    protected void grid_Cont_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string s = e.Parameters;
        if (s == "save")
        {
            ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
            ASPxTextBox txt_Id = grd.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            ASPxButtonEdit btn_ContNo = grd.FindEditFormTemplateControl("btn_ContNo") as ASPxButtonEdit;
            ASPxTextBox txt_SealNo = grd.FindEditFormTemplateControl("txt_SealNo") as ASPxTextBox;
            ASPxComboBox cbbContType = grd.FindEditFormTemplateControl("cbbContType") as ASPxComboBox;

            ASPxSpinEdit spin_Wt = grd.FindEditFormTemplateControl("spin_Wt") as ASPxSpinEdit;
            ASPxSpinEdit spin_M3 = grd.FindEditFormTemplateControl("spin_M3") as ASPxSpinEdit;
            ASPxSpinEdit spin_Pkgs = grd.FindEditFormTemplateControl("spin_Pkgs") as ASPxSpinEdit;
            ASPxButtonEdit txt_PkgsType = grd.FindEditFormTemplateControl("txt_PkgsType") as ASPxButtonEdit;

            //ASPxDateEdit date_Cont_Request = grd.FindEditFormTemplateControl("date_Cont_Request") as ASPxDateEdit;
            ASPxDateEdit date_Cont_Schedule = grd.FindEditFormTemplateControl("date_Cont_Schedule") as ASPxDateEdit;
            ASPxTextBox txt_DgClass = grd.FindEditFormTemplateControl("txt_DgClass") as ASPxTextBox;

            //ASPxDateEdit date_Cont_CfsIn = grd.FindEditFormTemplateControl("date_Cont_CfsIn") as ASPxDateEdit;
            //ASPxDateEdit date_Cont_CfsOut = grd.FindEditFormTemplateControl("date_Cont_CfsOut") as ASPxDateEdit;
            ASPxComboBox ASPxComboBox1 = grd.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

            //ASPxDateEdit date_Cont_YardPickup = grd.FindEditFormTemplateControl("date_Cont_YardPickup") as ASPxDateEdit;
            //ASPxDateEdit date_Cont_YardReturn = grd.FindEditFormTemplateControl("date_Cont_YardReturn") as ASPxDateEdit;
            ASPxComboBox cbb_StatusCode = grd.FindEditFormTemplateControl("cbb_StatusCode") as ASPxComboBox;

            ASPxComboBox cbb_F5Ind = grd.FindEditFormTemplateControl("cbb_F5Ind") as ASPxComboBox;
            ASPxComboBox cbb_UrgentInd = grd.FindEditFormTemplateControl("cbb_UrgentInd") as ASPxComboBox;

            //ASPxDateEdit date_Cdt = grd.FindEditFormTemplateControl("date_Cdt") as ASPxDateEdit;
            //ASPxTextBox txt_CdtTime = grd.FindEditFormTemplateControl("txt_CdtTime") as ASPxTextBox;
            //ASPxDateEdit date_YardExpiry = grd.FindEditFormTemplateControl("date_YardExpiry") as ASPxDateEdit;
            //ASPxTextBox txt_YardExpiryTime = grd.FindEditFormTemplateControl("txt_YardExpiryTime") as ASPxTextBox;

            ASPxTextBox txt_TerminalLocation = grd.FindEditFormTemplateControl("txt_TerminalLocation") as ASPxTextBox;
            ASPxMemo txt_YardAddress = grd.FindEditFormTemplateControl("txt_YardAddress") as ASPxMemo;
            ASPxMemo txt_ContRemark = grd.FindEditFormTemplateControl("txt_ContRemark") as ASPxMemo;
            ASPxTextBox txt_Remark1 = grd.FindEditFormTemplateControl("txt_Remark1") as ASPxTextBox;
            ASPxComboBox cbb_Permit = grd.FindEditFormTemplateControl("cbb_Permit") as ASPxComboBox;

            ASPxComboBox cbb_warehouse_status = grd.FindEditFormTemplateControl("cbb_warehouse_status") as ASPxComboBox;
            ASPxTextBox txt_TTTime = grd.FindEditFormTemplateControl("txt_TTTime") as ASPxTextBox;
            ASPxTextBox txt_BR = grd.FindEditFormTemplateControl("txt_BR") as ASPxTextBox;

            ASPxSpinEdit spin_fee1 = grd.FindEditFormTemplateControl("spin_fee1") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee2 = grd.FindEditFormTemplateControl("spin_fee2") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee3 = grd.FindEditFormTemplateControl("spin_fee3") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee4 = grd.FindEditFormTemplateControl("spin_fee4") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee5 = grd.FindEditFormTemplateControl("spin_fee5") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee6 = grd.FindEditFormTemplateControl("spin_fee6") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee7 = grd.FindEditFormTemplateControl("spin_fee7") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee8 = grd.FindEditFormTemplateControl("spin_fee8") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee9 = grd.FindEditFormTemplateControl("spin_fee9") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee10 = grd.FindEditFormTemplateControl("spin_fee10") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee11 = grd.FindEditFormTemplateControl("spin_fee11") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee12 = grd.FindEditFormTemplateControl("spin_fee12") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee13 = grd.FindEditFormTemplateControl("spin_fee13") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee14 = grd.FindEditFormTemplateControl("spin_fee14") as ASPxSpinEdit;
            //ASPxSpinEdit spin_fee15 = grd.FindEditFormTemplateControl("spin_fee15") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee16 = grd.FindEditFormTemplateControl("spin_fee16") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee17 = grd.FindEditFormTemplateControl("spin_fee17") as ASPxSpinEdit;
            //ASPxSpinEdit spin_fee18 = grd.FindEditFormTemplateControl("spin_fee18") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee19 = grd.FindEditFormTemplateControl("spin_fee19") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee20 = grd.FindEditFormTemplateControl("spin_fee20") as ASPxSpinEdit;
            //ASPxSpinEdit spin_fee21 = grd.FindEditFormTemplateControl("spin_fee21") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee22 = grd.FindEditFormTemplateControl("spin_fee22") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee23 = grd.FindEditFormTemplateControl("spin_fee23") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee24 = grd.FindEditFormTemplateControl("spin_fee24") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee25 = grd.FindEditFormTemplateControl("spin_fee25") as ASPxSpinEdit;
            ASPxSpinEdit spin_fee26 = grd.FindEditFormTemplateControl("spin_fee26") as ASPxSpinEdit;
            //ASPxSpinEdit spin_fee27 = grd.FindEditFormTemplateControl("spin_fee27") as ASPxSpinEdit;

            int Id = SafeValue.SafeInt(txt_Id.Text, 0);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet1), "Id='" + Id + "'");
            C2.CtmJobDet1 cont = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet1;
            bool isNew = false;
            if (cont == null)
            {
                isNew = true;
                cont = new C2.CtmJobDet1();
            }

            string old_containerno = cont.ContainerNo;
            cont.JobNo = jobNo.Text;
            cont.ContainerNo = SafeValue.SafeString(btn_ContNo.Text);
            cont.SealNo = SafeValue.SafeString(txt_SealNo.Text);
            cont.ContainerType = SafeValue.SafeString(cbbContType.Value);
            cont.Weight = SafeValue.SafeDecimal(spin_Wt.Text);
            cont.Volume = SafeValue.SafeDecimal(spin_M3.Text);
            cont.Qty = SafeValue.SafeInt(spin_Pkgs.Text, 0);
            cont.PackageType = SafeValue.SafeString(txt_PkgsType.Text);

            //cont.RequestDate = SafeValue.SafeDate(date_Cont_Request.Date, new DateTime(1990, 1, 1));
            cont.ScheduleDate = SafeValue.SafeDate(date_Cont_Schedule.Date, new DateTime(1990, 1, 1));
            cont.DgClass = SafeValue.SafeString(txt_DgClass.Text);
            //cont.CfsInDate = SafeValue.SafeDate(date_Cont_CfsIn.Date, new DateTime(1990, 1, 1));
            //cont.CfsOutDate = SafeValue.SafeDate(date_Cont_CfsOut.Date, new DateTime(1990, 1, 1));
            cont.PortnetStatus = SafeValue.SafeString(ASPxComboBox1.Value);

            //cont.YardPickupDate = SafeValue.SafeDate(date_Cont_YardPickup.Date, new DateTime(1990, 1, 1));
            //cont.YardReturnDate = SafeValue.SafeDate(date_Cont_YardReturn.Date, new DateTime(1990, 1, 1));
            cont.StatusCode = SafeValue.SafeString(cbb_StatusCode.Value);
            //cont.F5Ind = SafeValue.SafeString(cbb_F5Ind.Value);
            cont.UrgentInd = SafeValue.SafeString(cbb_UrgentInd.Value);

            //cont.CdtDate = SafeValue.SafeDate(date_Cdt.Date, new DateTime(1990, 1, 1));
            //cont.YardExpiryDate = SafeValue.SafeDate(date_YardExpiry.Date, new DateTime(1990, 1, 1));
            //cont.CdtTime = SafeValue.SafeString(txt_CdtTime.Text);
            //cont.YardExpiryTime = SafeValue.SafeString(txt_YardExpiryTime.Text);
            cont.TerminalLocation = SafeValue.SafeString(txt_TerminalLocation.Text);
            cont.YardAddress = SafeValue.SafeString(txt_YardAddress.Text);
            cont.Remark = SafeValue.SafeString(txt_ContRemark.Text);
            cont.Remark1 = SafeValue.SafeString(txt_Remark1.Text);
            cont.Permit = SafeValue.SafeString(cbb_Permit.Value);

            cont.WarehouseStatus = SafeValue.SafeString(cbb_warehouse_status.Value);
            cont.TTTime = SafeValue.SafeString(txt_TTTime.Text);
            cont.Br = SafeValue.SafeString(txt_BR.Text);

            cont.Fee1 = SafeValue.SafeDecimal(spin_fee1.Text, 0);
            cont.Fee2 = SafeValue.SafeDecimal(spin_fee2.Text, 0);
            cont.Fee3 = SafeValue.SafeDecimal(spin_fee3.Text, 0);
            cont.Fee4 = SafeValue.SafeDecimal(spin_fee4.Text, 0);
            cont.Fee5 = SafeValue.SafeDecimal(spin_fee5.Text, 0);
            cont.Fee6 = SafeValue.SafeDecimal(spin_fee6.Text, 0);
            cont.Fee7 = SafeValue.SafeDecimal(spin_fee7.Text, 0);
            cont.Fee8 = SafeValue.SafeDecimal(spin_fee8.Text, 0);
            cont.Fee9 = SafeValue.SafeDecimal(spin_fee9.Text, 0);
            cont.Fee10 = SafeValue.SafeDecimal(spin_fee10.Text, 0);
            cont.Fee11 = SafeValue.SafeDecimal(spin_fee11.Text, 0);
            cont.Fee12 = SafeValue.SafeDecimal(spin_fee12.Text, 0);
            cont.Fee13 = SafeValue.SafeDecimal(spin_fee13.Text, 0);
            cont.Fee14 = SafeValue.SafeDecimal(spin_fee14.Text, 0);
            //cont.Fee15 = SafeValue.SafeDecimal(spin_fee15.Text, 0);
            cont.Fee16 = SafeValue.SafeDecimal(spin_fee16.Text, 0);
            cont.Fee17 = SafeValue.SafeDecimal(spin_fee17.Text, 0);
            //cont.Fee18 = SafeValue.SafeDecimal(spin_fee18.Text, 0);
            cont.Fee19 = SafeValue.SafeDecimal(spin_fee19.Text, 0);
            cont.Fee20 = SafeValue.SafeDecimal(spin_fee20.Text, 0);
            //cont.Fee21 = SafeValue.SafeDecimal(spin_fee21.Text, 0);
            cont.Fee22 = SafeValue.SafeDecimal(spin_fee22.Text, 0);
            cont.Fee23 = SafeValue.SafeDecimal(spin_fee23.Text, 0);
            cont.Fee24 = SafeValue.SafeDecimal(spin_fee24.Text, 0);
            cont.Fee25 = SafeValue.SafeDecimal(spin_fee25.Text, 0);
            cont.Fee26 = SafeValue.SafeDecimal(spin_fee26.Text, 0);
            cont.Fee27 = 0; //SafeValue.SafeDecimal(spin_fee27.Text, 0);

            if (isNew)
            {
                C2.Manager.ORManager.StartTracking(cont, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(cont);
                //Trip_new_auto(jobNo.Text);
                e.Result = "success";
            }
            else
            {
                C2.Manager.ORManager.StartTracking(cont, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(cont);

                if (old_containerno != cont.ContainerNo)
                {
                    string sql = string.Format("Update ctm_JobDet2 set ContainerNo='{1}' where Det1Id='{0}'", Id, cont.ContainerNo);
                    ConnectSql.ExecuteSql(sql);
                }

                e.Result = "success";
            }

            //e.Result = btn_ContNo.Text;
        }
    }

    protected void gv_cont_trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        int contId = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        string sql = "select JobNo,Id,ContainerNo from CTM_JobDet1 where Id='" + contId + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            string contNo = dr["ContainerNo"].ToString();
            string JobNo = dr["JobNo"].ToString();
            this.dsContTrip.FilterExpression = string.Format(@" JobNo='{0}' and Det1Id='{1}' and ContainerNo='{2}'", JobNo, contId, contNo);
        }
        else
        {
            this.dsContTrip.FilterExpression = "1=0";
        }
    }

    protected void gv_cont_trip_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ToCode"] = SafeValue.SafeString(e.NewValues["ToCode"]);
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"]);
        e.NewValues["TowheadCode"] = SafeValue.SafeString(e.NewValues["TowheadCode"]);
        e.NewValues["ChessisCode"] = SafeValue.SafeString(e.NewValues["ChessisCode"]);
        e.NewValues["StageStatus"] = SafeValue.SafeString(e.NewValues["StageStatus"]);
        e.NewValues["ChessisCode"] = SafeValue.SafeString(e.NewValues["ChessisCode"]);
        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1900, 1, 1));
    }

    protected void gv_cont_trip_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxLabel gv_cont_trip_Id = grd.FindEditRowCellTemplateControl(null, "gv_cont_trip_Id") as ASPxLabel;

        string sql = string.Format(@"select * from ctm_jobdet2 where id={0}", gv_cont_trip_Id.Text);
        DataTable dt1 = ConnectSql.GetTab(sql);
        DataRow dr = dt1.Rows[0];

        sql = string.Format(@"with tb1 as (
select det2.Id,
ROW_NUMBER()over(order by 
case when charindex('IMP',j.JobType)>0 then 
(case det2.StageCode when 'Port' then 1 when 'Park1' then 2 when 'Warehouse' then 3 when 'Park2' then 4 when 'Yard' then 5 when 'Park3' then 6 when 'Completed' then 7 else 0 end)
else 
(case det2.StageCode when 'Yard' then 1 when 'Park1' then 2 when 'Warehouse' then 3 when 'Park2' then 4 when 'Port' then 5 when 'Park3' then 6 when 'Completed' then 7 else 0 end)
end 
) as rowId
from ctm_jobdet2 as det2
left outer join ctm_jobdet1 as det1 on det1.Id=det2.Det1Id
left outer join ctm_job as j on j.jobno=det2.JobNo
where det1.Id='{1}'
)
select Id from tb1 where rowId>isnull((select rowId from tb1 where Id='{0}'),0)", dr["Id"], dr["Det1Id"]);
        DataTable dt = ConnectSql.GetTab(sql);
        string Ids = "0";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Ids += (Ids.Length > 0 ? "," : "") + dt.Rows[i][0];
        }

        sql = string.Format(@"update ctm_jobdet2 set ChessisCode='{1}' where Id in({0})", Ids, dr["ChessisCode"]);
        ConnectSql.ExecuteSql(sql);
    }

    protected void gv_cont_trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string action = e.Parameters;
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView gv = pageControl.FindControl("grid_Cont") as ASPxGridView;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox txt_Id = gv.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxButtonEdit btn_ContNo = gv.FindEditFormTemplateControl("btn_ContNo") as ASPxButtonEdit;
        ASPxComboBox cbb_JobType = this.grid_job.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;

        if (action.Equals("AddNew"))
        {
            string sql = string.Format(@"select top 1 * from CTM_JobDet2 where Det1Id={0} order by Id desc", txt_Id.Text);
            DataTable dt = ConnectSql.GetTab(sql);
            string FromCode = "";
            DateTime FromDate = DateTime.Now;
            string FromTime = DateTime.Now.ToString("HH:mm");
            string FromPL = "";
            string trailer = "";
            string JobType = SafeValue.SafeString(cbb_JobType.Value);
            string TripCode = "";
            if (dt.Rows.Count > 0)
            {
                FromCode = SafeValue.SafeString(dt.Rows[0]["ToCode"]);
                FromPL = SafeValue.SafeString(dt.Rows[0]["FromParkingLot"]);
                FromDate = SafeValue.SafeDate(dt.Rows[0]["ToDate"], DateTime.Now);
                FromTime = SafeValue.SafeString(dt.Rows[0]["ToTime"], "00:00");
                trailer = SafeValue.SafeString(dt.Rows[0]["ChessisCode"]);
            }
            else
            {
                switch (JobType)
                {
                    case "IMP":
                        TripCode = "IMP";
                        break;
                    case "EXP":
                        TripCode = "COL";
                        break;
                    case "LOC":
                        TripCode = "LOC";
                        break;
                }
            }

            sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot) values ('{0}','{1}','','','{6}','{2}','{7}','{8}','{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','P','','N','{5}','','{10}','Normal','N','{9}')", txt_JobNo.Text, btn_ContNo.Text, FromCode, "", txt_Id.Text, "", trailer, FromDate, FromTime, FromPL,TripCode);
            ConnectSql.ExecuteSql(sql);
            sql = string.Format(@"select count(*) from ctm_jobdet2 where det1Id={0}", txt_Id.Text);
            int rowSum = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (rowSum == 1)
            {
                sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", "InTransit", txt_Id.Text);
                ConnectSql.ExecuteSql(sql);
            }
            e.Result = "success";
        }
        if (action.IndexOf("Delete_") >= 0)
        {
            Trip_Delete(sender, e, action.Replace("Delete_", ""));
        }
        if (action.Equals("Update"))
        {
            Trip_Update(sender, e);
        }
    }

    protected void gv_cont_trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = @"select det1.Id,det1.ContainerNo,det1.ContainerType from CTM_JobDet1 as det1 where JobNo=(select JobNo from CTM_JobDet1 where Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + ")";
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView grid_Cont = pageControl.FindControl("grid_Cont") as ASPxGridView;
        ASPxGridView gv_cont_trip = grid_Cont.FindEditFormTemplateControl("gv_cont_trip") as ASPxGridView;
        ASPxDropDownEdit dde_contNo = gv_cont_trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxGridView gvlist = dde_contNo.FindControl("gridPopCont") as ASPxGridView;
        gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        gvlist.DataBind();

    }
    #endregion

    #region Trip
    public string change_StatusShortCode_ToCode(object par)
    {
        string res = SafeValue.SafeString(par);
        switch (res)
        {
            case "P":
                res = "Pending";
                break;
            case "S":
                res = "Start";
                break;
            case "C":
                res = "Completed";
                break;
            case "X":
                res = "Cancel";
                break;
        }
        return res;
    }
    protected void grid_Trip_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet2));
        }
    }

    protected void grid_Trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsTrip.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "'";
    }

    protected void grid_Trip_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxMemo PickupFrom = pageControl.FindControl("txt_PickupFrom") as ASPxMemo;
        ASPxMemo DeliveryTo = pageControl.FindControl("txt_DeliveryTo") as ASPxMemo;
        string P_From = PickupFrom.Text;
        string P_From_Pl = "";
        string P_To = "";// DeliveryTo.Text;
        ASPxTextBox txt_JobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' order by Id desc", txt_JobNo.Text);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            P_From = dt.Rows[0]["ToCode"].ToString();
            P_From_Pl = dt.Rows[0]["ToParkingLot"].ToString();
        }

        e.NewValues["Statuscode"] = "P";
        e.NewValues["FromDate"] = DateTime.Now;
        e.NewValues["ToDate"] = DateTime.Now;
        e.NewValues["SubletFlag"] = "N";
        e.NewValues["BayCode"] = "B1";
        e.NewValues["StageCode"] = "Pending";
        e.NewValues["StageStatus"] = "";
        e.NewValues["FromParkingLot"] = P_From_Pl;
        e.NewValues["FromCode"] = P_From;
        e.NewValues["ToCode"] = P_To;
        e.NewValues["Overtime"] = "Normal";
        e.NewValues["OverDistance"] = "Y";
    }

    protected void grid_Trip_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        check_Trip_Status("0", e.NewValues["DriverCode"].ToString(), e.NewValues["Statuscode"].ToString());
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["TripCode"] = SafeValue.SafeString(e.NewValues["TripCode"], "");
        e.NewValues["Det1Id"] = SafeValue.SafeInt(e.NewValues["Det1Id"], 0);
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"], "");
        e.NewValues["BayCode"] = SafeValue.SafeString(e.NewValues["BayCode"], "");

        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1753, 1, 1));
        e.NewValues["ToDate"] = SafeValue.SafeDate(e.NewValues["ToDate"], new DateTime(1753, 1, 1));
        e.NewValues["StageCode"] = SafeValue.SafeString(e.NewValues["StageCode"], "Pending");
        e.NewValues["StageStatus"] = SafeValue.SafeString(e.NewValues["StageStatus"], "Pending");
        e.NewValues["Overtime"] = SafeValue.SafeString(e.NewValues["Overtime"], "");
        e.NewValues["OverDistance"] = SafeValue.SafeString(e.NewValues["OverDistance"], "");
    }

    protected void grid_Trip_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxLabel lb_tripId = grd.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
        check_Trip_Status(lb_tripId.Text.ToString(), e.NewValues["DriverCode"].ToString(), e.NewValues["Statuscode"].ToString());
        e.NewValues["TripCode"] = SafeValue.SafeString(e.NewValues["TripCode"], "");
        e.NewValues["Det1Id"] = SafeValue.SafeInt(e.NewValues["Det1Id"], 0);
        e.NewValues["ContainerNo"] = SafeValue.SafeString(e.NewValues["ContainerNo"], "");
        e.NewValues["BayCode"] = SafeValue.SafeString(e.NewValues["BayCode"], "");

        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1753, 1, 1));
        e.NewValues["ToDate"] = SafeValue.SafeDate(e.NewValues["ToDate"], new DateTime(1753, 1, 1));
        e.NewValues["StageCode"] = SafeValue.SafeString(e.NewValues["StageCode"], "Pending");
        e.NewValues["StageStatus"] = SafeValue.SafeString(e.NewValues["StageStatus"], "Pending");
        e.NewValues["Overtime"] = SafeValue.SafeString(e.NewValues["Overtime"], "");
        e.NewValues["OverDistance"] = SafeValue.SafeString(e.NewValues["OverDistance"], "");
    }

    protected void grid_Trip_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    protected void grid_Trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = @"select det1.Id,det1.ContainerNo,det1.ContainerType from CTM_JobDet1 as det1 left outer join CTM_Job as job on det1.JobNo=job.JobNo where job.Id=" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxGridView grid_Trip = pageControl.FindControl("grid_Trip") as ASPxGridView;
        ASPxDropDownEdit dde_contNo = grid_Trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxGridView gvlist = dde_contNo.FindControl("gridPopCont") as ASPxGridView;
        gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        gvlist.DataBind();
    }

    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] contIds = new object[grid.VisibleRowCount];
        object[] contNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            contIds[i] = grid.GetRowValues(i, "Id");
            contNs[i] = grid.GetRowValues(i, "ContainerNo");
            contTypes[i] = grid.GetRowValues(i, "ContainerType");
        }

        e.Properties["cpContId"] = contIds;
        e.Properties["cpContN"] = contNs;
        e.Properties["cpContType"] = contTypes;
    }

    private void check_Trip_Status(string id, string driverCode, string status)
    {
        if (driverCode.Trim().Length == 0)
        {
            return;
        }

        if (status == "S" || status == "P")
        {
            string sql = string.Format(@"select COUNT(*) from CTM_JobDet2 where DriverCode='{0}' and Statuscode='{2}' and Id<>'{1}'", driverCode, id, status);
            int result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
            if (result > 0)
            {
                throw new Exception("Status:'" + status + "' is existing for " + driverCode);
            }
        }
    }

    protected void grid_Trip_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }

    protected void grid_Trip_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }

    protected void grid_Trip_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }

    protected void grid_Trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "Update")
        {
            Trip_Update(sender, e);
        }
        else
        {
            string temp = e.Parameters;
            string[] ar = temp.Split('_');
            if (ar.Length == 2)
            {
                if (ar[0] == "Delete")
                {
                    Trip_Delete(sender, e, ar[1]);
                }
            }
        }
    }

    private void Trip_Delete(object sender, ASPxGridViewCustomDataCallbackEventArgs e, string tripId)
    {
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        C2.Manager.ORManager.ExecuteDelete(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        //C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
        //C2.Manager.ORManager.PersistChanges(trip);

        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;
        e.Result = re;
    }

    private void Trip_Update(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        ASPxLabel lb_tripId = grd.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
        string tripId = SafeValue.SafeString(lb_tripId.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        string Driver_old = "";

        bool isNew = false;
        if (trip == null)
        {
            isNew = true;
            trip = new C2.CtmJobDet2();
        }
        else
        {
            Driver_old = trip.DriverCode;
        }

        ASPxDropDownEdit dde_Trip_ContNo = grd.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxTextBox dde_Trip_ContId = grd.FindEditFormTemplateControl("dde_Trip_ContId") as ASPxTextBox;
        //ASPxButtonEdit btn_CfsCode = grd.FindEditFormTemplateControl("btn_CfsCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_BayCode = grd.FindEditFormTemplateControl("cbb_Trip_BayCode") as ASPxComboBox;
        //ASPxComboBox cbb_Carpark = grd.FindEditFormTemplateControl("cbb_Carpark") as ASPxComboBox;
        ASPxComboBox cbb_Trip_TripCode = grd.FindEditFormTemplateControl("cbb_Trip_TripCode") as ASPxComboBox;

        ASPxButtonEdit btn_DriverCode = grd.FindEditFormTemplateControl("btn_DriverCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_TowheadCode = grd.FindEditFormTemplateControl("btn_TowheadCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_ChessisCode = grd.FindEditFormTemplateControl("btn_ChessisCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_SubletFlag = grd.FindEditFormTemplateControl("cbb_Trip_SubletFlag") as ASPxComboBox;
        //ASPxTextBox txt_SubletHauliername = grd.FindEditFormTemplateControl("txt_SubletHauliername") as ASPxTextBox;
        //ASPxComboBox cbb_StageCode = grd.FindEditFormTemplateControl("cbb_StageCode") as ASPxComboBox;
        //ASPxComboBox cbb_StageStatus = grd.FindEditFormTemplateControl("cbb_StageStatus") as ASPxComboBox;
        ASPxComboBox cbb_Trip_StatusCode = grd.FindEditFormTemplateControl("cbb_Trip_StatusCode") as ASPxComboBox;
        ASPxDateEdit txt_FromDate = grd.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_fromTime = grd.FindEditFormTemplateControl("txt_Trip_fromTime") as ASPxTextBox;
        ASPxDateEdit date_Trip_toDate = grd.FindEditFormTemplateControl("date_Trip_toDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_toTime = grd.FindEditFormTemplateControl("txt_Trip_toTime") as ASPxTextBox;
        ASPxMemo txt_Trip_Remark = grd.FindEditFormTemplateControl("txt_Trip_Remark") as ASPxMemo;
        ASPxMemo txt_Trip_FromCode = grd.FindEditFormTemplateControl("txt_Trip_FromCode") as ASPxMemo;
        ASPxMemo txt_Trip_ToCode = grd.FindEditFormTemplateControl("txt_Trip_ToCode") as ASPxMemo;
        //ASPxSpinEdit spin_Price = grd.FindEditFormTemplateControl("spin_Price") as ASPxSpinEdit;
        ASPxComboBox cbb_zone = grd.FindEditFormTemplateControl("cbb_zone") as ASPxComboBox;
        ASPxSpinEdit spin_Incentive1 = grd.FindEditFormTemplateControl("spin_Incentive1") as ASPxSpinEdit;
        ASPxSpinEdit spin_Incentive2 = grd.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;
        ASPxSpinEdit spin_Incentive3 = grd.FindEditFormTemplateControl("spin_Incentive3") as ASPxSpinEdit;
        ASPxComboBox cbb_Incentive4 = grd.FindEditFormTemplateControl("cbb_Incentive4") as ASPxComboBox;

        //ASPxComboBox cbb_Overtime = grd.FindEditFormTemplateControl("cbb_Overtime") as ASPxComboBox;
        ASPxMemo txt_driver_remark = grd.FindEditFormTemplateControl("txt_driver_remark") as ASPxMemo;
        //ASPxComboBox cbb_OverDistance = grd.FindEditFormTemplateControl("cbb_OverDistance") as ASPxComboBox;

        ASPxTextBox fromPackingLot = grd.FindEditFormTemplateControl("txt_FromPL") as ASPxTextBox;
        ASPxTextBox toPackingLot = grd.FindEditFormTemplateControl("txt_ToPL") as ASPxTextBox;
        //check_Trip_Status("0", trip.DriverCode,trip.Statuscode);

        ASPxSpinEdit spin_Charge1 = grd.FindEditFormTemplateControl("spin_Charge1") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge2 = grd.FindEditFormTemplateControl("spin_Charge2") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge3 = grd.FindEditFormTemplateControl("spin_Charge3") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge4 = grd.FindEditFormTemplateControl("spin_Charge4") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge5 = grd.FindEditFormTemplateControl("spin_Charge5") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge6 = grd.FindEditFormTemplateControl("spin_Charge6") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge7 = grd.FindEditFormTemplateControl("spin_Charge7") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge8 = grd.FindEditFormTemplateControl("spin_Charge8") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge9 = grd.FindEditFormTemplateControl("spin_Charge9") as ASPxSpinEdit;


        trip.ContainerNo = SafeValue.SafeString(dde_Trip_ContNo.Value);
        trip.Det1Id = SafeValue.SafeInt(dde_Trip_ContId.Text, 0);
        //trip.CfsCode = SafeValue.SafeString(btn_CfsCode.Value);
        //trip.BayCode = SafeValue.SafeString(cbb_Trip_BayCode.Value);

        trip.DriverCode = SafeValue.SafeString(btn_DriverCode.Value);
        trip.TowheadCode = SafeValue.SafeString(btn_TowheadCode.Value);
        trip.ChessisCode = SafeValue.SafeString(btn_ChessisCode.Value);
        //trip.SubletFlag = SafeValue.SafeString(cbb_Trip_SubletFlag.Value);
        //trip.SubletHauliername = SafeValue.SafeString(txt_SubletHauliername.Text);
        trip.TripCode = SafeValue.SafeString(cbb_Trip_TripCode.Value);
        //trip.StageCode = SafeValue.SafeString(cbb_StageCode.Value);
        //trip.Carpark = SafeValue.SafeString(cbb_Carpark.Value);
        //trip.StageStatus = SafeValue.SafeString(cbb_StageStatus.Value);
        trip.Statuscode = SafeValue.SafeString(cbb_Trip_StatusCode.Value);
        trip.FromDate = SafeValue.SafeDate(txt_FromDate.Date, new DateTime(1990, 1, 1));
        trip.FromTime = SafeValue.SafeString(txt_Trip_fromTime.Text);
        trip.ToDate = SafeValue.SafeDate(date_Trip_toDate.Date, new DateTime(1990, 1, 1));
        trip.ToTime = SafeValue.SafeString(txt_Trip_toTime.Text);
        trip.Remark = SafeValue.SafeString(txt_Trip_Remark.Text);
        trip.FromCode = SafeValue.SafeString(txt_Trip_FromCode.Text);
        trip.ToCode = SafeValue.SafeString(txt_Trip_ToCode.Text);
        //trip.Incentive1 = SafeValue.SafeDecimal(spin_Price.Text);
        trip.ParkingZone = SafeValue.SafeString(cbb_zone.Value);
        trip.Incentive1 = SafeValue.SafeDecimal(spin_Incentive1.Text);
        trip.Incentive2 = SafeValue.SafeDecimal(spin_Incentive2.Text);
        trip.Incentive3 = SafeValue.SafeDecimal(spin_Incentive3.Text);
        trip.Incentive4 = SafeValue.SafeDecimal(cbb_Incentive4.Value);

        //trip.Overtime = SafeValue.SafeSqlString(cbb_Overtime.Value);
        //trip.OverDistance = SafeValue.SafeSqlString(cbb_OverDistance.Value);
        trip.Remark1 = SafeValue.SafeString(txt_driver_remark.Text);

        trip.FromParkingLot = SafeValue.SafeString(fromPackingLot.Text);
        trip.ToParkingLot = SafeValue.SafeString(toPackingLot.Text);

        trip.Charge1 = SafeValue.SafeDecimal(spin_Charge1.Text);
        trip.Charge2 = SafeValue.SafeDecimal(spin_Charge2.Text);
        trip.Charge3 = SafeValue.SafeDecimal(spin_Charge3.Text);
        trip.Charge4 = SafeValue.SafeDecimal(spin_Charge4.Text);
        trip.Charge5 = SafeValue.SafeDecimal(spin_Charge5.Text);
        trip.Charge6 = SafeValue.SafeDecimal(spin_Charge6.Text);
        trip.Charge7 = SafeValue.SafeDecimal(spin_Charge7.Text);
        trip.Charge8 = SafeValue.SafeDecimal(spin_Charge8.Text);
        trip.Charge9 = SafeValue.SafeDecimal(spin_Charge9.Text);

        if (isNew)
        {
            trip.JobNo = jobNo.Text;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(trip);
        }
        else
        {
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);
        }
        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;

        if (!trip.DriverCode.Equals(Driver_old))
        {
            re += "," + Driver_old;
        }
        e.Result = re;

    }


    #endregion


    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        this.dsJobPhoto.FilterExpression = "RefNo='" + SafeValue.SafeString(jobNo.Text, "") + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    #endregion


    #region bill
    protected void Grid_Invoice_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsInvoice.FilterExpression = "MastType='CTM' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    protected void Grid_Payable_Import_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsVoucher.FilterExpression = "MastType='CTM' and MastRefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "-1") + "'";
    }
    #endregion

    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsCosting.FilterExpression = "JobType='CTM' and RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmCosting));
        }
    }
    protected void grid_Cost_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SaleQty"] = 1;
        e.NewValues["CostQty"] = 1;
        e.NewValues["SalePrice"] = 0;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["SaleLocAmt"] = 0;
        e.NewValues["CostLocAmt"] = 0;
        e.NewValues["VendorId"] = " ";
        e.NewValues["ChgCode"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["SaleCurrency"] = "SGD";
        e.NewValues["SaleExRate"] = 1;
        e.NewValues["CostCurrency"] = "SGD";
        e.NewValues["CostExRate"] = 1;
        e.NewValues["JobNo"] = "0";
        e.NewValues["SplitType"] = "Set";
        e.NewValues["PayInd"] = "N";
        e.NewValues["VerifryInd"] = "N";
        e.NewValues["DocNo"] = " ";
        e.NewValues["Salesman"] = "NA";

    }
    protected void grid_Cost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox refNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobType"] = "CTM";
        e.NewValues["SplitType"] = "Set";
        e.NewValues["RefNo"] = refNo.Text;
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox refNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;

    }
    protected void grid_Cost_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;

    }
    protected void grid_Cost_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox vendorName = grd.FindEditFormTemplateControl("txt_CostVendorName") as ASPxTextBox;
        vendorName.Text = EzshipHelper.GetPartyName(grd.GetRowValues(grd.EditingRowVisibleIndex, new string[] { "VendorId" }));
    }
    #endregion

    private void updateJob_By_Date(string Id)
    {
        string sql = string.Format(@"update CTM_Job set UpdateBy='{0}',UpdateDateTime=getdate() where Id='{1}'", HttpContext.Current.User.Identity.Name, Id);
        ConnectSql.ExecuteSql(sql);
    }

    #region Trip Log
    protected void grid_TripLog_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmTripLog));
        }
    }
    protected void grid_TripLog_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        //this.dsTripLog.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and JobType='HS'";
    }
    protected void grid_TripLog_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["LogDate"] = DateTime.Now;
        e.NewValues["LogTime"] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        e.NewValues["Status"] = "U";
    }
    protected void grid_TripLog_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"], "U");
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["JobType"] = "HS";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_TripLog_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"], "U");
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_TripLog_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }


    #endregion
    protected void btn_charge_add_Click(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox txt_jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        string JobNo = txt_jobNo.Text;
        string sql = string.Format(@"select count(*) from CTM_JobCharge where JobNo='{0}'", JobNo);
        int cc = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
        if (cc == 0)
        {
            sql = string.Format(@"insert into CTM_JobCharge (JobNo,ItemName,ItemType,Cost,CreateDateTime) values('{0}','charge1','','0',getdate()),('{0}','charge2','','0',getdate()),('{0}','charge3','','0',getdate())", JobNo);
            int r = ConnectSql_mb.ExecuteNonQuery(sql);
        }
    }
    protected void gv_charge_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobCharge));
        }
    }
    protected void gv_charge_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsCharge.FilterExpression = "JobNo='" + JobNo + "'";
    }
    protected void gv_charge_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Cost"] = SafeValue.SafeDecimal(e.NewValues["Cost"], 0);
        e.NewValues["ItemName"] = SafeValue.SafeString(e.NewValues["ItemName"]);
    }
    protected void gv_charge_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gv_charge_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Add")
        {
            ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
            ASPxTextBox txt_jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            string JobNo = txt_jobNo.Text;
            string sql = string.Format(@"select count(*) from CTM_JobCharge where JobNo='{0}'", JobNo);
            int cc = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
            if (cc == 0)
            {
                sql = string.Format(@"insert into CTM_JobCharge (JobNo,ItemName,ItemType,Cost,CreateDateTime) values('{0}','charge1','','0',getdate()),('{0}','charge2','','0',getdate()),('{0}','charge3','','0',getdate())", JobNo);
                int r = ConnectSql_mb.ExecuteNonQuery(sql);
                if (r > 0)
                {
                    e.Result = "refresh";
                }
            }
        }
    }

    #region  Stock

    protected void gv_stock_Init(object sender, EventArgs e)
    {

        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobStock));
        }
    }
    protected void gv_stock_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsStock.FilterExpression = "JobNo='" + JobNo + "'";

    }
    protected void gv_stock_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        e.NewValues["StockStatus"] = "IN";
        e.NewValues["Weight"] = 0;
        e.NewValues["Volume"] = 0;
        e.NewValues["StockQty"] = 0;
        e.NewValues["PackingQty"] = 0;
    }
    protected void gv_stock_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_job.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;

    }
    protected void gv_stock_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gv_stock_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["StockDescription"] = SafeValue.SafeString(e.NewValues["StockDescription"]);
        e.NewValues["StockMarking"] = SafeValue.SafeString(e.NewValues["StockMarking"]);
        e.NewValues["StockUnit"] = SafeValue.SafeString(e.NewValues["StockUnit"]);
        e.NewValues["PackingUnit"] = SafeValue.SafeString(e.NewValues["PackingUnit"]);
        e.NewValues["PackingDimention"] = SafeValue.SafeString(e.NewValues["PackingDimention"]);
    }

    #endregion

    #region Activity
    protected void gv_activity_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";

        string JobNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        dsActivity.FilterExpression = "JobNo='" + JobNo + "'";
    }
    #endregion

    #region close tab
    protected void ASPxGridView1_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select JobNo from CTM_Job where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        this.dsLogEvent.FilterExpression = " JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and (JobStatus='USE' or JobStatus='CLS')";
    }
    #endregion

}