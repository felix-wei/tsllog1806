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

public partial class PagesContTrucking_SelectPage_TripListForJobList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string contId = SafeValue.SafeString(Request.QueryString["contId"]);
        this.dsCont.FilterExpression = "JobNo='" + jobNo + "'";
        this.dsTrip.FilterExpression = "Det1Id='" + contId + "'";
        lbl_JobNo.Text = jobNo;
        lb_ContId.Text = contId;
        if (!IsPostBack)
        {
            if (jobNo.Length > 0)
            {
                //string ContNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select ContainerNo from Ctm_JobDet1 where Id={0}", contId)));
                DataTable dt = ConnectSql.GetTab(string.Format(@"select ContainerNo,StatusCode from Ctm_JobDet1 where Id={0}", contId));
                if (dt.Rows.Count > 0)
                {
                    //lb_ContNo.Text = dt.Rows[0]["ContainerNo"].ToString();
                    btn_ContClose.Text = SafeValue.SafeString(dt.Rows[0]["StatusCode"]).Equals("Completed") ? "Open Container" : "Complete Container";
                }
                //lb_ContNo.Text = ContNo;
                dt = ConnectSql.GetTab(string.Format(@"select job.Id,PickupFrom,DeliveryTo,YardRef,job.SpecialInstruction,job.Remark,job.PermitNo,
det1.SealNo,det1.ContainerNo,det1.F5Ind,det1.EmailInd,det1.WarehouseStatus,det1.PortnetStatus,det1.UrgentInd
from ctm_jobdet1 as det1
left outer join ctm_job as job on job.JobNo=det1.JobNo where det1.Id={0}", contId));
                if (dt.Rows.Count > 0)
                {
                    txt_sealno.Text = SafeValue.SafeString(dt.Rows[0]["SealNo"]);
                    txt_ContNo.Text = SafeValue.SafeString(dt.Rows[0]["ContainerNo"]);

                    txt_from.Text = SafeValue.SafeString(dt.Rows[0]["PickupFrom"]);
                    txt_to.Text = SafeValue.SafeString(dt.Rows[0]["DeliveryTo"]);
                    txt_depot.Text = SafeValue.SafeString(dt.Rows[0]["YardRef"]);
                    txt_instruction.Text = SafeValue.SafeString(dt.Rows[0]["SpecialInstruction"]);
                    txt_Remark.Text = SafeValue.SafeString(dt.Rows[0]["Remark"]);
                    txt_PermitNo.Text = SafeValue.SafeString(dt.Rows[0]["PermitNo"]);

                    cbb_EmailInd.Value = SafeValue.SafeString(dt.Rows[0]["EmailInd"]);
                    cbb_F5Ind.Value = SafeValue.SafeString(dt.Rows[0]["F5Ind"]);
                    cbb_PortnetStatus.Value = SafeValue.SafeString(dt.Rows[0]["PortnetStatus"]);
                    cbb_UrgentInd.Value = SafeValue.SafeString(dt.Rows[0]["UrgentInd"]);
                    cbb_warehouse_status.Value = SafeValue.SafeString(dt.Rows[0]["WarehouseStatus"]);

                }
            }
        }
        //        if (lb_ContId.Text.Length > 0)
        //        {
        //            string sql = string.Format(@"select det2.*,(case job.StatusCode when 'USE' then '' else 'none' end ) as canChange,
        //(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP') as incentive,
        //(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL') as claims
        //from ctm_jobdet2 as det2 
        //left outer join ctm_job as job on det2.jobno=job.jobno
        //where det2.det1Id=@ContId", lb_ContId.Text);
        //            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        //            list.Add(new ConnectSql_mb.cmdParameters("@ContId", SafeValue.SafeInt(lb_ContId.Text, 0), SqlDbType.Int));
        //            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        //            grid_Trip.DataSource = dt;
        //            grid_Trip.DataBind();
        //        }
    }
    #region Trip
    protected void grid_Trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string contId = SafeValue.SafeString(Request.QueryString["contId"]);
        dsTrip.FilterExpression = "Det1Id="+contId;
    }
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
    protected void grid_Trip_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string jobNo = lbl_JobNo.Text;
        string sql = string.Format(@"select PickupFrom,DeliveryTo,YardRef,JobType from CTM_Job where JobNo='{0}'", SafeValue.SafeString(lbl_JobNo.Text, ""));
        DataTable tab = ConnectSql.GetTab(sql);
        sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} order by Id desc", SafeValue.SafeString(lbl_JobNo.Text, ""), SafeValue.SafeInt(lb_ContId.Text, 0));
        DataTable dt = ConnectSql.GetTab(sql);
        string job_from = SafeValue.SafeString(tab.Rows[0]["PickupFrom"]);
        string job_to = SafeValue.SafeString(tab.Rows[0]["DeliveryTo"]);
        string job_Depot = SafeValue.SafeString(tab.Rows[0]["YardRef"]);
        string P_From = "";
        string P_From_Pl = "";
        string P_To = "";// DeliveryTo.Text;
        string trailer = "";
        string JobType = SafeValue.SafeString(tab.Rows[0]["JobType"]);
        string TripCode = "";
        //if (dt.Rows.Count > 0)
        //{
        //    P_From = dt.Rows[0]["ToCode"].ToString();
        //    P_From_Pl = dt.Rows[0]["ToParkingLot"].ToString();
        //    trailer = dt.Rows[0]["ChessisCode"].ToString();
        //}
        //else
        //{
        //    switch (JobType)
        //    {
        //        case "IMP":
        //            TripCode = "IMP";
        //            break;
        //        case "EXP":
        //            TripCode = "COL";
        //            break;
        //        case "LOC":
        //            TripCode = "LOC";
        //            break;
        //    }
        //}
        string newType = SafeValue.SafeString(lb_newTrip_Type.Text, "SHF");
        TripCode = newType;
        switch (newType)
        {
            case "COL":
                add_newTrip_CheckMultiple(newType);
                P_From = job_Depot;
                P_To = job_from;
                break;
            case "EXP":
                add_newTrip_CheckMultiple(newType);
                P_From = job_from;
                P_To = job_to;
                break;
            case "IMP":
                add_newTrip_CheckMultiple(newType);
                P_From = job_from;
                P_To = job_to;
                break;
            case "RET":
                add_newTrip_CheckMultiple(newType);
                P_To = job_Depot;
                break;
            case "SHF":
                P_To = job_from;
                break;
            case "LOC":
                P_From = job_from;
                P_To = job_to;
                break;
        }
        if (dt.Rows.Count > 0)
        {
            P_From = dt.Rows[0]["ToCode"].ToString();
            P_From_Pl = dt.Rows[0]["ToParkingLot"].ToString();
            trailer = dt.Rows[0]["ChessisCode"].ToString();
        }

        e.NewValues["Statuscode"] = "P";
        //e.NewValues["FromDate"] = DateTime.Now;
        //e.NewValues["ToDate"] = DateTime.Now;
        e.NewValues["SubletFlag"] = "N";
        e.NewValues["BayCode"] = "B1";
        e.NewValues["StageCode"] = "Pending";
        e.NewValues["StageStatus"] = "";
        e.NewValues["FromParkingLot"] = P_From_Pl;
        e.NewValues["FromCode"] = P_From;
        e.NewValues["ToCode"] = P_To;
        e.NewValues["Overtime"] = "Normal";
        e.NewValues["OverDistance"] = "Y";
        //e.NewValues["Incentive1"] = 0;
        //e.NewValues["Incentive2"] = 0;
        //e.NewValues["Incentive3"] = 0;
        //e.NewValues["Incentive4"] = 0;

        //e.NewValues["Charge1"] = 0;
        //e.NewValues["Charge2"] = 0;
        //e.NewValues["Charge3"] = 0;
        //e.NewValues["Charge4"] = 0;
        //e.NewValues["Charge5"] = 0;
        //e.NewValues["Charge6"] = 0;
        //e.NewValues["Charge7"] = 0;
        //e.NewValues["Charge8"] = 0;
        //e.NewValues["Charge9"] = 0;

        e.NewValues["ContainerNo"] = txt_ContNo.Text;
        e.NewValues["Det1Id"] = lb_ContId.Text;
        e.NewValues["ChessisCode"] = trailer;
        e.NewValues["TripCode"] = TripCode;
    }
    private void add_newTrip_CheckMultiple(string Type)
    {
        string sql = string.Format(@"select Id,TripCode from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} and TripCode='{2}' order by Id desc", SafeValue.SafeString(lbl_JobNo.Text, ""), SafeValue.SafeInt(lb_ContId.Text, 0), Type);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            throw new Exception("Exist trip:" + Type);
        }
    }
    protected void grid_Trip_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        check_Trip_Status("0", e.NewValues["DriverCode"].ToString(), e.NewValues["Statuscode"].ToString());

        e.NewValues["JobNo"] = lbl_JobNo.Text;
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


        //e.NewValues["Incentive1"] = SafeValue.SafeDecimal(e.NewValues["Incentive1"]);
        //e.NewValues["Incentive2"] = SafeValue.SafeDecimal(e.NewValues["Incentive2"]);
        //e.NewValues["Incentive3"] = SafeValue.SafeDecimal(e.NewValues["Incentive3"]);
        //e.NewValues["Incentive4"] = SafeValue.SafeDecimal(e.NewValues["Incentive4"]);

        //e.NewValues["Charge1"] = SafeValue.SafeDecimal(e.NewValues["Charge1"]);
        //e.NewValues["Charge2"] = SafeValue.SafeDecimal(e.NewValues["Charge2"]);
        //e.NewValues["Charge3"] = SafeValue.SafeDecimal(e.NewValues["Charge3"]);
        //e.NewValues["Charge4"] = SafeValue.SafeDecimal(e.NewValues["Charge4"]);
        //e.NewValues["Charge5"] = SafeValue.SafeDecimal(e.NewValues["Charge5"]);
        //e.NewValues["Charge6"] = SafeValue.SafeDecimal(e.NewValues["Charge6"]);
        //e.NewValues["Charge7"] = SafeValue.SafeDecimal(e.NewValues["Charge7"]);
        //e.NewValues["Charge8"] = SafeValue.SafeDecimal(e.NewValues["Charge8"]);
        //e.NewValues["Charge9"] = SafeValue.SafeDecimal(e.NewValues["Charge9"]);

        e.NewValues["FromParkingLot"] = SafeValue.SafeString(e.NewValues["FromParkingLot"], "");
        e.NewValues["ToParkingLot"] = SafeValue.SafeString(e.NewValues["ToParkingLot"], "");
        e.NewValues["ParkingZone"] = SafeValue.SafeString(e.NewValues["ParkingZone"], "");
        e.NewValues["DoubleMounting"] = SafeValue.SafeString(e.NewValues["DoubleMounting"], "No");

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
        //e.NewValues["Overtime"] = SafeValue.SafeString(e.NewValues["Overtime"], "");
        e.NewValues["OverDistance"] = SafeValue.SafeString(e.NewValues["OverDistance"], "");


        //e.NewValues["Incentive1"] = SafeValue.SafeDecimal(e.NewValues["Incentive1"]);
        //e.NewValues["Incentive2"] = SafeValue.SafeDecimal(e.NewValues["Incentive2"]);
        //e.NewValues["Incentive3"] = SafeValue.SafeDecimal(e.NewValues["Incentive3"]);
        //e.NewValues["Incentive4"] = SafeValue.SafeDecimal(e.NewValues["Incentive4"]);

        //e.NewValues["Charge1"] = SafeValue.SafeDecimal(e.NewValues["Charge1"]);
        //e.NewValues["Charge2"] = SafeValue.SafeDecimal(e.NewValues["Charge2"]);
        //e.NewValues["Charge3"] = SafeValue.SafeDecimal(e.NewValues["Charge3"]);
        //e.NewValues["Charge4"] = SafeValue.SafeDecimal(e.NewValues["Charge4"]);
        //e.NewValues["Charge5"] = SafeValue.SafeDecimal(e.NewValues["Charge5"]);
        //e.NewValues["Charge6"] = SafeValue.SafeDecimal(e.NewValues["Charge6"]);
        //e.NewValues["Charge7"] = SafeValue.SafeDecimal(e.NewValues["Charge7"]);
        //e.NewValues["Charge8"] = SafeValue.SafeDecimal(e.NewValues["Charge8"]);
        //e.NewValues["Charge9"] = SafeValue.SafeDecimal(e.NewValues["Charge9"]);

        e.NewValues["FromParkingLot"] = SafeValue.SafeString(e.NewValues["FromParkingLot"], "");
        e.NewValues["ToParkingLot"] = SafeValue.SafeString(e.NewValues["ToParkingLot"], "");
        e.NewValues["ParkingZone"] = SafeValue.SafeString(e.NewValues["ParkingZone"], "");
        e.NewValues["DoubleMounting"] = SafeValue.SafeString(e.NewValues["DoubleMounting"]);

    }
    protected void grid_Trip_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ////ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        //ASPxLabel lb_tripId=this.grid_Trip.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
        //int id = SafeValue.SafeInt(lb_tripId.Text,0);
        //string sql = @"select det1.Id,det1.ContainerNo,det1.ContainerType from CTM_JobDet1 as det1 left outer join CTM_Job as job on det1.JobNo=job.JobNo where job.Id=" + id;
        //ASPxDropDownEdit dde_contNo =this.grid_Trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        //ASPxGridView gvlist = dde_contNo.FindControl("gridPopCont") as ASPxGridView;
        //gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        //gvlist.DataBind();
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

        if (status == "S")
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
        string JobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string status = "";
        if (JobNo.Substring(0, 1) == "I")
            status = "Import";
        if (JobNo.Substring(0, 1) == "E")
            status = "Collection";

        string sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", status, lb_ContId.Text);
        ConnectSql.ExecuteSql(sql);
    }
    protected void grid_Trip_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    private void updateJob_By_Date(string Id)
    {
        string sql = string.Format(@"update CTM_Job set UpdateBy='{0}',UpdateDateTime=getdate() where Id='{1}'", HttpContext.Current.User.Identity.Name, Id);
        ConnectSql.ExecuteSql(sql);
    }
    protected void grid_Trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string temp = e.Parameters;
        string[] ar = temp.Split('_');
        if (temp == "Update")
        {
            Trip_Update(sender, e);
        }
        if (temp == "AddNew")
        {
            Trip_AddNew(sender, e);
        }
        if (temp == "SaveJob")
        {
            Job_Save(sender, e);
        }
        if (ar.Length == 2)
        {
            if (ar[0] == "Delete")
            {
                Trip_Delete(sender, e, ar[1]);
            }
            if (ar[0] == "UpdateInline")
            {
                Trip_UpdateInline(sender, e, SafeValue.SafeInt(ar[1], -1));
            }
        }
        if (e.Parameters == "ContClose")
        {
            Cont_Close(e);
            C2.CtmJobDet1.contTruckingStatusChanged(SafeValue.SafeInt(lb_ContId.Text, 0));
        }
        if (e.Parameters == "WhsStatus")
        {
            WHS_Close(e);
            //C2.CtmJobDet1.contTruckingStatusChanged(SafeValue.SafeInt(lb_ContId.Text, 0));
        }
    }
    private void Job_Save(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string sql = string.Format(@"update ctm_jobdet1 set SealNo=@SealNo,ContainerNo=@ContainerNo,
EmailInd=@EmailInd,F5Ind=@F5Ind,PortnetStatus=@PortnetStatus,UrgentInd=@UrgentInd,WarehouseStatus=@WarehouseStatus
where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", lb_ContId.Text, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@SealNo", txt_sealno.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", txt_ContNo.Text, SqlDbType.NVarChar, 100));

        list.Add(new ConnectSql_mb.cmdParameters("@EmailInd", SafeValue.SafeString(cbb_EmailInd.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@F5Ind", SafeValue.SafeString(cbb_F5Ind.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@PortnetStatus", SafeValue.SafeString(cbb_PortnetStatus.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@UrgentInd", SafeValue.SafeString(cbb_UrgentInd.Value), SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@WarehouseStatus", SafeValue.SafeString(cbb_warehouse_status.Value), SqlDbType.NVarChar, 100));

        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status)
        {
            sql = string.Format(@"update ctm_job set PickupFrom=@PickupFrom,DeliveryTo=@DeliveryTo,YardRef=@YardRef,SpecialInstruction=@SpecialInstruction,Remark=@Remark,PermitNo=@PermitNo where JobNo=@JobNo");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", lbl_JobNo.Text, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@PickupFrom", txt_from.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@DeliveryTo", txt_to.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@YardRef", txt_depot.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@SpecialInstruction", txt_instruction.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@Remark", txt_Remark.Text, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@PermitNo", txt_PermitNo.Text, SqlDbType.NVarChar, 100));
            res = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (res.status)
            {
                sql = string.Format(@"update ctm_jobdet2 set ContainerNo=@ContainerNo where Det1Id=@Det1Id");
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", lb_ContId.Text, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", txt_ContNo.Text, SqlDbType.NVarChar, 100));
                res = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (res.status)
                {
                    update_cargo(lb_ContId.Text, txt_ContNo.Text);
                    e.Result = "success";
                }
            }
        }
    }
    private void Trip_modified(string Id, string type, string JobType)
    {
        int contId = 0;
        string sql = "";
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        if (type == "trip")
        {
            sql = string.Format(@"select det1Id from ctm_jobdet2 where Id=@tripId");
            list.Add(new ConnectSql_mb.cmdParameters("@tripId", SafeValue.SafeInt(Id, 0), SqlDbType.Int));
            contId = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);

        }
        if (type == "container" || type == null || type == "")
        {
            contId = SafeValue.SafeInt(Id, 0);
        }

        sql = string.Format(@"select TripCode from ctm_jobdet2 where det1Id=@contId order by Id desc");
        list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
        //string temp = ConnectSql_mb.ExecuteScalar(sql, list).context;
        DataTable dt_trips = ConnectSql_mb.GetDataTable(sql, list);
        sql = string.Format(@"update ctm_jobdet1 set StatusCode=@StatusCode where Id=@contId");
        //if (temp.Equals("0"))
        //{
        //    list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", "New", SqlDbType.NVarChar, 30));
        //}
        //else
        //{
        //    string StatusCode = "";
        //    if(JobType == "IMP"){
        //        StatusCode = "Import"; 
        //    }
        //    if (JobType == "EXP") {
        //        StatusCode = "Collection";
        //    }
        //    list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", StatusCode, SqlDbType.NVarChar, 30));
        //}
        if (dt_trips.Rows.Count==0)
        {
            list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", "New", SqlDbType.NVarChar, 30));
        }
        else
        {
            string StatusCode = "";
            string tripType = dt_trips.Rows[0]["TripCode"].ToString();
            switch (tripType)
            {
                case "IMP":
                    StatusCode = "Import";
                    break;
                case "RET":
                    StatusCode = "Return";
                    break;
                case "COL":
                    StatusCode = "Collection";
                    break;
                case "EXP":
                    StatusCode = "Export";
                    break;
            }
            list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", StatusCode, SqlDbType.NVarChar, 30));
        }
        ConnectSql_mb.ExecuteNonQuery(sql, list);
    }
    private void Trip_AddNew(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

        string jobNo = lbl_JobNo.Text;
        string sql = string.Format(@"select PickupFrom,DeliveryTo,YardRef,JobType,IsWarehouse,WareHouseCode from CTM_Job where JobNo='{0}'", SafeValue.SafeString(lbl_JobNo.Text, ""));
        DataTable tab = ConnectSql.GetTab(sql);
        sql = string.Format(@"select top 1 * from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} order by Id desc", SafeValue.SafeString(lbl_JobNo.Text, ""), SafeValue.SafeInt(lb_ContId.Text, 0));
        DataTable dt = ConnectSql.GetTab(sql);
        string job_from = SafeValue.SafeString(tab.Rows[0]["PickupFrom"]);
        string job_to = SafeValue.SafeString(tab.Rows[0]["DeliveryTo"]);
        string job_Depot = SafeValue.SafeString(tab.Rows[0]["YardRef"]);
        string isWarehouse = SafeValue.SafeString(tab.Rows[0]["IsWarehouse"]);
        string warehouseId = SafeValue.SafeString(tab.Rows[0]["WareHouseCode"]);
        string P_From = "";
        string P_From_Pl = "";
        string P_To = "";// DeliveryTo.Text;
        string trailer = "";
        string JobType = SafeValue.SafeString(tab.Rows[0]["JobType"]);
        string TripCode = "";
        DateTime FromDate = DateTime.Now;
        string FromTime = "00:00";// DateTime.Now.ToString("HH:mm");

        string newType = SafeValue.SafeString(lb_newTrip_Type.Text, "SHF");
        TripCode = newType;
        switch (newType)
        {
            case "COL":
                add_newTrip_CheckMultiple(newType);
                P_From = job_Depot;
                P_To = job_from;
                if (isWarehouse == "Yes")
                    P_To = warehouseId;
                break;
            case "EXP":
                add_newTrip_CheckMultiple(newType);
                if (isWarehouse == "Yes")
                    P_From = warehouseId;
                P_From = job_from;
                P_To = job_to;
                break;
            case "IMP":
                add_newTrip_CheckMultiple(newType);
                P_From = job_from;
                P_To = job_to;
                if (isWarehouse == "Yes")
                    P_To = warehouseId;
                break;
            case "RET":
                add_newTrip_CheckMultiple(newType);
                if (isWarehouse == "Yes")
                    P_From = warehouseId;
                else
                    P_From = job_to;
                P_To = job_Depot;
                break;
            case "SHF":
                P_From = job_from;
                if (isWarehouse == "Yes")
                    P_To = warehouseId;
                else
                {
                    P_From = job_Depot;
                    P_To = job_from;
                }
                break;
            case "LOC":
                P_From = job_from;
                P_To = job_to;
                break;
        }
        if (dt.Rows.Count > 0)
        {
            P_From = dt.Rows[0]["ToCode"].ToString();
            P_From_Pl = dt.Rows[0]["ToParkingLot"].ToString();
            trailer = dt.Rows[0]["ChessisCode"].ToString();
        }

        // sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
        // BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot,DoubleMounting) values ('{0}','{1}','','','{2}','{3}','{4}','{5}','{6}','{4}','{5}','{7}','P',
        // '','N','','','{8}','Normal','N','{9}','No')", jobNo, txt_ContNo.Text, trailer, P_From, FromDate, FromTime, P_To, lb_ContId.Text,
        // TripCode, P_From_Pl);
        // ConnectSql.ExecuteSql(sql);

        sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,BookingDate,BookingTime,ToCode,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot) values (@JobNo,@ContainerNo,'','',@ChessisCode,@FromCode,@BookingDate,@BookingTime,@ToCode,@Det1Id,'P',
'','N','','',@TripCode,'Normal','N',@FromParkingLot) select @@identity", jobNo, txt_ContNo.Text, trailer, P_From, FromDate, FromTime, P_To, lb_ContId.Text,
                                            TripCode, P_From_Pl);
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", txt_ContNo.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", trailer, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromCode", P_From, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@BookingDate", FromDate, SqlDbType.DateTime));
        list.Add(new ConnectSql_mb.cmdParameters("@BookingTime", FromTime, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", P_To, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", lb_ContId.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@TripCode", TripCode, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@FromParkingLot", P_From_Pl, SqlDbType.NVarChar, 100));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
        int tripId = SafeValue.SafeInt(res.context, 0);

        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isTRIP(tripId);
        elog.setActionLevel(tripId, CtmJobEventLogRemark.Level.Trip, 1);
        elog.log();

        Trip_modified(lb_ContId.Text, "container", JobType);

        e.Result = "success";
    }
    private void Trip_Delete(object sender, ASPxGridViewCustomDataCallbackEventArgs e, string tripId)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isTRIP(SafeValue.SafeInt(tripId, 0));
        elog.setActionLevel(SafeValue.SafeInt(tripId, 0), CtmJobEventLogRemark.Level.Trip, 2);
        elog.log();

        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        C2.CtmJobDet2 trip = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        C2.Manager.ORManager.ExecuteDelete(typeof(C2.CtmJobDet2), "Id='" + tripId + "'");
        //C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
        //C2.Manager.ORManager.PersistChanges(trip);
        string sql = string.Format(@"select PickupFrom,DeliveryTo,YardRef,JobType from CTM_Job where JobNo='{0}'", SafeValue.SafeString(lbl_JobNo.Text, ""));
        DataTable tab = ConnectSql.GetTab(sql);
        string JobType = SafeValue.SafeString(tab.Rows[0]["JobType"]);
        //Trip_modified(lb_ContId.Text, "container", JobType);

        sql = string.Format(@"delete from job_cost where TripNo='{0}'", tripId);
        C2.Manager.ORManager.ExecuteScalar(sql);



        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;
        e.Result = re;
    }
    private void Trip_Update(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxLabel lb_tripId = this.grid_Trip.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
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

        ASPxDropDownEdit dde_Trip_ContNo = this.grid_Trip.FindEditFormTemplateControl("dde_Trip_ContNo") as ASPxDropDownEdit;
        ASPxTextBox dde_Trip_ContId = this.grid_Trip.FindEditFormTemplateControl("dde_Trip_ContId") as ASPxTextBox;
        //ASPxButtonEdit btn_CfsCode = this.grid_Trip.FindEditFormTemplateControl("btn_CfsCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_BayCode = this.grid_Trip.FindEditFormTemplateControl("cbb_Trip_BayCode") as ASPxComboBox;
        //ASPxComboBox cbb_Carpark = this.grid_Trip.FindEditFormTemplateControl("cbb_Carpark") as ASPxComboBox;
        ASPxComboBox cbb_Trip_TripCode = this.grid_Trip.FindEditFormTemplateControl("cbb_Trip_TripCode") as ASPxComboBox;

        ASPxButtonEdit btn_DriverCode = this.grid_Trip.FindEditFormTemplateControl("btn_DriverCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_TowheadCode = this.grid_Trip.FindEditFormTemplateControl("btn_TowheadCode") as ASPxButtonEdit;
        ASPxButtonEdit btn_ChessisCode = this.grid_Trip.FindEditFormTemplateControl("btn_ChessisCode") as ASPxButtonEdit;
        //ASPxComboBox cbb_Trip_SubletFlag = this.grid_Trip.FindEditFormTemplateControl("cbb_Trip_SubletFlag") as ASPxComboBox;
        //ASPxTextBox txt_SubletHauliername = this.grid_Trip.FindEditFormTemplateControl("txt_SubletHauliername") as ASPxTextBox;
        //ASPxComboBox cbb_StageCode = this.grid_Trip.FindEditFormTemplateControl("cbb_StageCode") as ASPxComboBox;
        //ASPxComboBox cbb_StageStatus = this.grid_Trip.FindEditFormTemplateControl("cbb_StageStatus") as ASPxComboBox;
        ASPxComboBox cbb_Trip_StatusCode = this.grid_Trip.FindEditFormTemplateControl("cbb_Trip_StatusCode") as ASPxComboBox;
        ASPxDateEdit txt_FromDate = this.grid_Trip.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_fromTime = this.grid_Trip.FindEditFormTemplateControl("txt_Trip_fromTime") as ASPxTextBox;
        ASPxDateEdit date_Trip_toDate = this.grid_Trip.FindEditFormTemplateControl("date_Trip_toDate") as ASPxDateEdit;
        ASPxTextBox txt_Trip_toTime = this.grid_Trip.FindEditFormTemplateControl("txt_Trip_toTime") as ASPxTextBox;
        ASPxMemo txt_Trip_Remark = this.grid_Trip.FindEditFormTemplateControl("txt_Trip_Remark") as ASPxMemo;
        ASPxMemo txt_Trip_FromCode = this.grid_Trip.FindEditFormTemplateControl("txt_Trip_FromCode") as ASPxMemo;
        ASPxMemo txt_Trip_ToCode = this.grid_Trip.FindEditFormTemplateControl("txt_Trip_ToCode") as ASPxMemo;
        //ASPxSpinEdit spin_Price = this.grid_Trip.FindEditFormTemplateControl("spin_Price") as ASPxSpinEdit;
        ASPxDateEdit date_BookingDate = this.grid_Trip.FindEditFormTemplateControl("date_BookingDate") as ASPxDateEdit;
        ASPxTextBox txt_BookingTime = this.grid_Trip.FindEditFormTemplateControl("txt_BookingTime") as ASPxTextBox;

        ASPxComboBox cbb_zone = grd.FindEditFormTemplateControl("cbb_zone") as ASPxComboBox;
        ASPxSpinEdit spin_Incentive1 = grd.FindEditFormTemplateControl("spin_Incentive1") as ASPxSpinEdit;
        ASPxSpinEdit spin_Incentive2 = grd.FindEditFormTemplateControl("spin_Incentive2") as ASPxSpinEdit;
        ASPxSpinEdit spin_Incentive3 = grd.FindEditFormTemplateControl("spin_Incentive3") as ASPxSpinEdit;
        ASPxComboBox cbb_Incentive4 = grd.FindEditFormTemplateControl("cbb_Incentive4") as ASPxComboBox;

        //ASPxComboBox cbb_Overtime = this.grid_Trip.FindEditFormTemplateControl("cbb_Overtime") as ASPxComboBox;
        ASPxMemo txt_driver_remark = this.grid_Trip.FindEditFormTemplateControl("txt_driver_remark") as ASPxMemo;
        //ASPxComboBox cbb_OverDistance = this.grid_Trip.FindEditFormTemplateControl("cbb_OverDistance") as ASPxComboBox;

        ASPxTextBox fromPackingLot = this.grid_Trip.FindEditFormTemplateControl("txt_FromPL") as ASPxTextBox;
        ASPxTextBox toPackingLot = this.grid_Trip.FindEditFormTemplateControl("txt_ToPL") as ASPxTextBox;
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
        ASPxSpinEdit spin_Charge10 = grd.FindEditFormTemplateControl("spin_Charge10") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge_ERP = grd.FindEditFormTemplateControl("spin_Charge_ERP") as ASPxSpinEdit;
        ASPxSpinEdit spin_Charge_ParkingFee = grd.FindEditFormTemplateControl("spin_Charge_ParkingFee") as ASPxSpinEdit;

        ASPxComboBox cmb_DoubleMounting = grd.FindEditFormTemplateControl("cmb_DoubleMounting") as ASPxComboBox;
        trip.DoubleMounting = SafeValue.SafeString(cmb_DoubleMounting.Value, "No");

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
        //trip.Incentive1 = SafeValue.SafeDecimal(spin_Incentive1.Text);
        //trip.Incentive2 = SafeValue.SafeDecimal(spin_Incentive2.Text);
        //trip.Incentive3 = SafeValue.SafeDecimal(spin_Incentive3.Text);
        //trip.Incentive4 = SafeValue.SafeDecimal(cbb_Incentive4.Value);

        //trip.Overtime = SafeValue.SafeSqlString(cbb_Overtime.Value);
        //trip.OverDistance = SafeValue.SafeSqlString(cbb_OverDistance.Value);
        trip.Remark1 = SafeValue.SafeString(txt_driver_remark.Text);

        trip.FromParkingLot = SafeValue.SafeString(fromPackingLot.Text);
        trip.ToParkingLot = SafeValue.SafeString(toPackingLot.Text);

        //trip.Charge1 = SafeValue.SafeDecimal(spin_Charge1.Text);
        //trip.Charge2 = SafeValue.SafeDecimal(spin_Charge2.Text);
        //trip.Charge3 = SafeValue.SafeDecimal(spin_Charge3.Text);
        //trip.Charge4 = SafeValue.SafeDecimal(spin_Charge4.Text);
        //trip.Charge5 = SafeValue.SafeDecimal(spin_Charge5.Text);
        //trip.Charge6 = SafeValue.SafeDecimal(spin_Charge6.Text);
        //trip.Charge7 = SafeValue.SafeDecimal(spin_Charge7.Text);
        //trip.Charge8 = SafeValue.SafeDecimal(spin_Charge8.Text);
        //trip.Charge9 = SafeValue.SafeDecimal(spin_Charge9.Text);
        //trip.Charge10 = SafeValue.SafeDecimal(spin_Charge10.Text);
        string userId = HttpContext.Current.User.Identity.Name;

        ASPxComboBox cmb_Escort_Ind = grd.FindEditFormTemplateControl("cmb_Escort_Ind") as ASPxComboBox;
        if (cmb_Escort_Ind != null)
            trip.Escort_Ind = SafeValue.SafeString(cmb_Escort_Ind.Value);
        ASPxMemo txt_Trip_Escort_Remark = grd.FindEditFormTemplateControl("txt_Trip_Escort_Remark") as ASPxMemo;
        if (txt_Trip_Escort_Remark != null)
            trip.Escort_Remark = txt_Trip_Escort_Remark.Text;
        ASPxButtonEdit btn_SubCon_Code = grd.FindEditFormTemplateControl("btn_SubCon_Code") as ASPxButtonEdit;
        if (btn_SubCon_Code != null)
            trip.SubCon_Code = SafeValue.SafeString(btn_SubCon_Code.Text);
        ASPxComboBox cbb_SubCon_Code = grd.FindEditFormTemplateControl("cbb_SubCon_Code") as ASPxComboBox;
        if (cbb_SubCon_Code != null)
            trip.SubCon_Ind = SafeValue.SafeString(cbb_SubCon_Code.Value);
        ASPxComboBox cbb_Self_Ind = grd.FindEditFormTemplateControl("cbb_Self_Ind") as ASPxComboBox;
        if (cbb_Self_Ind != null)
            trip.Self_Ind = SafeValue.SafeString(cbb_Self_Ind.Value);

        if (date_BookingDate != null)
            trip.BookingDate = date_BookingDate.Date;
        if (txt_BookingTime != null)
            trip.BookingTime = txt_BookingTime.Text;
        if (isNew)
        {
            trip.JobNo = lbl_JobNo.Text;
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(trip);

            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = userId;
            elog.ActionLevel_isTRIP(trip.Id);
            elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Trip, 1);
            elog.log();
        }
        else
        {
            C2.Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(trip);

            //C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            //elog.Platform_isWeb();
            //elog.Controller = userId;
            //elog.ActionLevel_isTRIP(trip.Id);
            //elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Trip, 3);
            //elog.log();

            C2.CtmJobDet2.tripStatusChanged(trip.Id);

            Dictionary<string, decimal> d = new Dictionary<string, decimal>();
            d.Add("Trip", SafeValue.SafeDecimal(spin_Incentive1.Text));
            d.Add("OverTime", SafeValue.SafeDecimal(spin_Incentive2.Text));
            d.Add("Standby", SafeValue.SafeDecimal(spin_Incentive3.Text));
            d.Add("PSA", SafeValue.SafeDecimal(cbb_Incentive4.Text));
            C2.CtmJobDet2.Incentive_Save(trip.Id, d);
            d = new Dictionary<string, decimal>();
            d.Add("DHC", SafeValue.SafeDecimal(spin_Charge1.Text));
            d.Add("WEIGHING", SafeValue.SafeDecimal(spin_Charge2.Text));
            d.Add("WASHING", SafeValue.SafeDecimal(spin_Charge3.Text));
            d.Add("REPAIR", SafeValue.SafeDecimal(spin_Charge4.Text));
            d.Add("DETENTION", SafeValue.SafeDecimal(spin_Charge5.Text));
            d.Add("DEMURRAGE", SafeValue.SafeDecimal(spin_Charge6.Text));
            d.Add("LIFT_ON_OFF", SafeValue.SafeDecimal(spin_Charge7.Text));
            d.Add("C_SHIPMENT", SafeValue.SafeDecimal(spin_Charge8.Text));
            d.Add("EMF", SafeValue.SafeDecimal(spin_Charge9.Text));
            d.Add("OTHER", SafeValue.SafeDecimal(spin_Charge10.Text));
            d.Add("ERP",SafeValue.SafeDecimal(spin_Charge_ERP.Value));
            d.Add("ParkingFee", SafeValue.SafeDecimal(spin_Charge_ParkingFee.Value));
            C2.CtmJobDet2.Claims_Save(trip.Id, d);
        }
        string re = HttpContext.Current.User.Identity.Name + "," + trip.Id + "," + trip.DriverCode;

        if (!trip.DriverCode.Equals(Driver_old))
        {
            re += "," + Driver_old;
        }
        e.Result = re;

    }
    private void Trip_UpdateInline(object sender, ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }
        ASPxTextBox txt_Id = this.grid_Trip.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Trip.Columns["ToParkingLot"], "txt_Id") as ASPxTextBox;
        ASPxButtonEdit txt_parkingLot = this.grid_Trip.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Trip.Columns["ToParkingLot"], "txt_parkingLot") as ASPxButtonEdit;
        ASPxTextBox txt_tocode = this.grid_Trip.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Trip.Columns["ToParkingLot"], "txt_tocode") as ASPxTextBox;
        ASPxButtonEdit btn_Driver = this.grid_Trip.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Trip.Columns["ToParkingLot"], "btn_Driver") as ASPxButtonEdit;
        ASPxTextBox txt_towhead = this.grid_Trip.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Trip.Columns["ToParkingLot"], "txt_towhead") as ASPxTextBox;
        ASPxButtonEdit txt_trailer = this.grid_Trip.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Trip.Columns["ToParkingLot"], "txt_trailer") as ASPxButtonEdit;
        ASPxMemo txt_instr = this.grid_Trip.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Trip.Columns["ToParkingLot"], "txt_instr") as ASPxMemo;


        string sql = string.Format(@"select DriverCode from ctm_jobdet2 where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", txt_Id.Text, SqlDbType.Int));
        string Driver_old = ConnectSql_mb.ExecuteScalar(sql, list).context;
        string re = HttpContext.Current.User.Identity.Name + "," + txt_Id.Text + "," + btn_Driver.Text;
        if (!btn_Driver.Text.Equals(Driver_old))
        {
            re += "," + Driver_old;
        }

        sql = string.Format(@"update ctm_jobdet2 set 
ToParkingLot=@ToParkingLot,ToCode=@ToCode,DriverCode=@DriverCode,TowheadCode=@TowheadCode,ChessisCode=@ChessisCode,Remark=@Remark 
where Id=@Id");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", txt_Id.Text, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@ToParkingLot", txt_parkingLot.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", txt_tocode.Text, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", btn_Driver.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@TowheadCode", txt_towhead.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", txt_trailer.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Remark", txt_instr.Text, SqlDbType.NVarChar, 300));

        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status)
        {
            e.Result = re;
        }
        else
        {
            e.Result = "Save Error:" + res.context;
        }


        //e.Result = "Save Error:" + txt_parkingLot.Text;
    }
    protected void grid_Trip_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Update")
        {
            //Trip_Update(sender, e);

            this.grid_Trip.CancelEdit();

        }
    }
    public void Cont_Close(ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string ContId = lb_ContId.Text;
        string JobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string sql = string.Format(@"select StatusCode from ctm_jobdet1 where Id={0}", ContId);
        string status = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (status.Equals("Completed"))
        {
            if (JobNo.Contains("CI"))
                status = "Return";
            if (JobNo.Contains("CE"))
                status = "Export";
        }
        else
        {
            status = "Completed";
        }
        sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", ContId, status);
        ConnectSql.ExecuteSql(sql);

        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isCONT(SafeValue.SafeInt(ContId, 0));
        elog.setActionLevel(SafeValue.SafeInt(ContId, 0), CtmJobEventLogRemark.Level.Container, 6);
        elog.log();

        e.Result = status;
    }
    public void WHS_Close(ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string isWarehouse = SafeValue.SafeString(Request.QueryString["isWarehouse"]);
        string JobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string ContId = lb_ContId.Text;
        string sql = string.Format(@"select StatusCode from ctm_jobdet1 where Id={0}", ContId);
        string status = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (JobNo.Substring(0, 1) == "I")
            status = "Return";
        else if (JobNo.Substring(0, 1) == "E")
            status = "Export";
        else
        {
            status = "Completed";
        }
        sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", ContId, status);
        ConnectSql.ExecuteSql(sql);

        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isCONT(SafeValue.SafeInt(ContId, 0));
        elog.setActionLevel(SafeValue.SafeInt(ContId, 0), CtmJobEventLogRemark.Level.Cargo, 5);
        elog.log();


        e.Result = status;
    }
    private void update_ContStatus_trip(int contId, string type, string statucCode)
    {
        string JobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
        string isWarehouse = SafeValue.SafeString(Request.QueryString["isWarehouse"]);
        string sql = "";
        string status = "";
        if (isWarehouse == "Yes")
        {
            if (type == "IMP")
                status = "WHS-LD";
            if (type == "EXP")
                status = "WHS-MT";
            if (type == "RET")
                status = "Return";
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        if (isWarehouse == "No")
        {
            if (type == "IMP")
                status = "Customer-LD";
            if (type == "EXP")
                status = "Customer-MT";
            if (type == "RET")
                status = "Return";
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        string userId = HttpContext.Current.User.Identity.Name;
        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
        elog.Platform_isWeb();
        elog.Controller = userId;
        elog.ActionLevel_isCONT(contId);
        elog.setActionLevel(contId, CtmJobEventLogRemark.Level.Container, 4, status);
        elog.log();
    }
    private void update_cargo(string contId, string contNo)
    {
        string sql = string.Format(@"update job_house set ContNo=@ContNo where ContId=@ContId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", contNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@ContId",SafeValue.SafeInt(contId,0), SqlDbType.Int));
        ConnectSql_mb.ExecuteNonQuery(sql, list);
    }
    #endregion

}