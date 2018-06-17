using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_Container_BatchAdd1 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString().Length > 0)
            {
                txt_JobNo.Text = Request.QueryString["no"].ToString();
                DateTime dt = DateTime.Now;
                date_YardExpiry.Value = dt;
                date_YardExpiry1.Value = dt;
                date_YardExpiry2.Value = dt;
                date_YardExpiry3.Value = dt;
                date_YardExpiry4.Value = dt;
                date_YardExpiry5.Value = dt;
                date_YardExpiry6.Value = dt;
                date_YardExpiry7.Value = dt;
                date_YardExpiry8.Value = dt;
                date_YardExpiry9.Value = dt;
                string sql = string.Format(@"select YardRef from ctm_job where JobNo='{0}'", txt_JobNo.Text);
                string DeportAdd = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
                btn_YardAddress.Text = DeportAdd;
                btn_YardAddress1.Text = DeportAdd;
                btn_YardAddress2.Text = DeportAdd;
                btn_YardAddress3.Text = DeportAdd;
                btn_YardAddress4.Text = DeportAdd;
                btn_YardAddress5.Text = DeportAdd;
                btn_YardAddress6.Text = DeportAdd;
                btn_YardAddress7.Text = DeportAdd;
                btn_YardAddress8.Text = DeportAdd;
                btn_YardAddress9.Text = DeportAdd;

                rows_init();
            }
        }
        string sql1 = string.Format(@"select top 1 substring(ContainerNo,2,case when len(ContainerNo)>1 then len(ContainerNo)-1 else 0 end) as CurNO
from ctm_jobdet1  
where len(ContainerNo)<4 and CHARINDEX(ContainerNo,'c',0)=0 and JobNo='{0}'
order by len(ContainerNo) desc,ContainerNo desc", txt_JobNo.Text);
        DataTable dt_cont = ConnectSql.GetTab(sql1);
        string res = "0";
        if (dt_cont.Rows.Count > 0)
        {
            res = dt_cont.Rows[0]["CurNO"].ToString();
        }
        txt_CurNO.Text = res;
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {

        container_new();
        return;
        #region old create container 
        //        string jobType = Request.QueryString["type"].ToString();
        //        string sql = string.Format(@"insert into CTM_JobDet1 (JobNo,ContainerNo,ContainerType,SealNo,Weight,Volume,QTY,PackageType,RequestDate,ScheduleDate,CfsInDate,CfsOutDate,YardPickupDate,YardReturnDate,F5Ind,UrgentInd,StatusCode,CdtDate,YardExpiryDate,YardAddress,Remark,Permit,JobType,CfsStatus)
        //values");
        //        string values = "";
        //        if (btn_ContNo.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo.Text, txt_ContType.Text, txt_SealNo.Text, dt, btn_YardAddress.Text, txt_Remark.Text, txt_Weight.Text, txt_Urgent.Text, txt_Permit.Text, txt_dg.Text, jobType);
        //        }
        //        if (btn_ContNo1.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry1.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo1.Text, txt_ContType1.Text, txt_SealNo1.Text, dt, btn_YardAddress1.Text, txt_Remark1.Text, txt_Weight1.Text, txt_Urgent1.Text, txt_Permit1.Text, txt_dg1.Text, jobType);
        //        }
        //        if (btn_ContNo2.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry2.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo2.Text, txt_ContType2.Text, txt_SealNo2.Text, dt, btn_YardAddress2.Text, txt_Remark2.Text, txt_Weight2.Text, txt_Urgent2.Text, txt_Permit2.Text, txt_dg2.Text, jobType);
        //        }
        //        if (btn_ContNo3.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry3.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo3.Text, txt_ContType3.Text, txt_SealNo3.Text, dt, btn_YardAddress3.Text, txt_Remark3.Text, txt_Weight3.Text, txt_Urgent3.Text, txt_Permit3.Text, txt_dg3.Text, jobType);
        //        }
        //        if (btn_ContNo4.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry4.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo4.Text, txt_ContType4.Text, txt_SealNo4.Text, dt, btn_YardAddress4.Text, txt_Remark4.Text, txt_Weight4.Text, txt_Urgent4.Text, txt_Permit4.Text, txt_dg4.Text, jobType);
        //        }
        //        if (btn_ContNo5.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry5.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo5.Text, txt_ContType5.Text, txt_SealNo5.Text, dt, btn_YardAddress5.Text, txt_Remark5.Text, txt_Weight5.Text, txt_Urgent5.Text, txt_Permit5.Text, txt_dg5.Text, jobType);
        //        }

        //        if (btn_ContNo6.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry6.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo6.Text, txt_ContType6.Text, txt_SealNo6.Text, dt, btn_YardAddress6.Text, txt_Remark6.Text, txt_Weight6.Text, txt_Urgent6.Text, txt_Permit6.Text, txt_dg6.Text, jobType);
        //        }
        //        if (btn_ContNo7.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry7.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo7.Text, txt_ContType7.Text, txt_SealNo7.Text, dt, btn_YardAddress7.Text, txt_Remark7.Text, txt_Weight7.Text, txt_Urgent7.Text, txt_Permit7.Text, txt_dg7.Text, jobType);
        //        }
        //        if (btn_ContNo8.Text.Trim().Length > 0)
        //        {
        //            DateTime dt = SafeValue.SafeDate(date_YardExpiry8.Date, new DateTime(1990, 1, 1));
        //            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo8.Text, txt_ContType8.Text, txt_SealNo8.Text, dt, btn_YardAddress8.Text, txt_Remark8.Text, txt_Weight8.Text, txt_Urgent8.Text, txt_Permit8.Text, txt_dg8.Text, jobType);
        //        }
        //if (btn_ContNo9.Text.Trim().Length > 0)
        //{
        //    DateTime dt = SafeValue.SafeDate(date_YardExpiry9.Date, new DateTime(1990, 1, 1));
        //    values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','{7}','0','0','',getdate(),'{4}',getdate(),getdate(),getdate(),getdate(),'{10}','{8}','New',getdate(),'{4}','{5}','{6}','{9}','{11}','Pending')", txt_JobNo.Text, btn_ContNo9.Text, txt_ContType9.Text, txt_SealNo9.Text, dt, btn_YardAddress9.Text, txt_Remark9.Text, txt_Weight9.Text, txt_Urgent9.Text, txt_Permit9.Text, txt_dg9.Text, jobType);
        //}

        //        string re = "false";
        //        if (values.Length > 0)
        //        {
        //            sql = sql + values;
        //            int i = ConnectSql.ExecuteSql(sql);
        //            if (i > 0)
        //            {
        //                re = "success";
        //                //Trip_new_auto(txt_JobNo.Text);
        //            }
        //        }


        //        Response.Write("<script>parent.Popup_ContainerBatchAdd_callback('" + re + "');</script>");

        #endregion
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
BayCode,SubletFlag,StageCode,StageStatus) values");
        string values = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            values += (values.Length > 0 ? "," : "") + string.Format("('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','B1','N','Pending','Pending'),('{0}','{1}','','','','{2}',getdate(),left(convert(nvarchar,getdate(),108),5),'{3}',getdate(),left(convert(nvarchar,getdate(),108),5),'{4}','U','B1','N','Pending','Pending')", JobNo, dt.Rows[i]["ContainerNo"], dr_job["PickupFrom"], dr_job["DeliveryTo"], dt.Rows[i]["Id"]);
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


    private void rows_init()
    {
        txt_Weight.Value = "0";
        txt_Urgent.Value = "N";
        txt_Permit.Value = "N";
        txt_dg.Value = "N";

        txt_Weight1.Value = "0";
        txt_Urgent1.Value = "N";
        txt_Permit1.Value = "N";
        txt_dg1.Value = "N";
        txt_Weight2.Value = "0";
        txt_Urgent2.Value = "N";
        txt_Permit2.Value = "N";
        txt_dg2.Value = "N";
        txt_Weight3.Value = "0";
        txt_Urgent3.Value = "N";
        txt_Permit3.Value = "N";
        txt_dg3.Value = "N";
        txt_Weight4.Value = "0";
        txt_Urgent4.Value = "N";
        txt_Permit4.Value = "N";
        txt_dg4.Value = "N";
        txt_Weight5.Value = "0";
        txt_Urgent5.Value = "N";
        txt_Permit5.Value = "N";
        txt_dg5.Value = "N";
        txt_Weight6.Value = "0";
        txt_Urgent6.Value = "N";
        txt_Permit6.Value = "N";
        txt_dg6.Value = "N";
        txt_Weight7.Value = "0";
        txt_Urgent7.Value = "N";
        txt_Permit7.Value = "N";
        txt_dg7.Value = "N";
        txt_Weight8.Value = "0";
        txt_Urgent8.Value = "N";
        txt_Permit8.Value = "N";
        txt_dg8.Value = "N";
        txt_Weight9.Value = "0";
        txt_Urgent9.Value = "N";
        txt_Permit9.Value = "N";
        txt_dg9.Value = "N";
    }


    private void container_new()
    {
        C2.CtmJob job = C2.Manager.ORManager.GetObject(new Wilson.ORMapper.OPathQuery<C2.CtmJob>("JobNo='" + txt_JobNo.Text + "'"));
        if (job != null)
        {
            string user = HttpContext.Current.User.Identity.Name;
            C2.CtmJobDet1Biz det1Bz = new C2.CtmJobDet1Biz(0);
            C2.CtmJobDet1 det1 = new C2.CtmJobDet1();
            det1.JobNo = job.JobNo;
            det1.RequestDate = DateTime.Now;
            det1.CfsInDate = DateTime.Now;
            det1.CfsOutDate = DateTime.Now;
            det1.YardPickupDate = DateTime.Now;
            det1.YardReturnDate = DateTime.Now;
            det1.CdtDate = DateTime.Now;
            det1.YardExpiryDate = DateTime.Now;
            det1.F5Ind = "N";
            det1.UrgentInd = "N";
            det1.StatusCode = "New";
            //det1.YardCode = job.YardCode;
            //det1.YardAddress = job.YardRef;

            #region container sss
            string values = "";
            if (btn_ContNo.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo.Text;
                det1.ContainerType = txt_ContType.Text;
                det1.SealNo = txt_SealNo.Text;
                det1.YardAddress = btn_YardAddress.Text;
                det1.Remark = txt_Remark.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType.Value);
                det1.UrgentInd = txt_Urgent.Text;
                det1.Permit = txt_Permit.Text;
                det1.DgClass = txt_dg.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo1.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo1.Text;
                det1.ContainerType = txt_ContType1.Text;
                det1.SealNo = txt_SealNo1.Text;
                det1.YardAddress = btn_YardAddress1.Text;
                det1.Remark = txt_Remark1.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry1.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time1.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight1.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType1.Value);
                det1.UrgentInd = txt_Urgent1.Text;
                det1.Permit = txt_Permit1.Text;
                det1.DgClass = txt_dg1.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo2.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo2.Text;
                det1.ContainerType = txt_ContType2.Text;
                det1.SealNo = txt_SealNo2.Text;
                det1.YardAddress = btn_YardAddress2.Text;
                det1.Remark = txt_Remark2.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry2.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time2.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight2.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType2.Value);
                det1.UrgentInd = txt_Urgent2.Text;
                det1.Permit = txt_Permit2.Text;
                det1.DgClass = txt_dg2.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo3.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo3.Text;
                det1.ContainerType = txt_ContType3.Text;
                det1.SealNo = txt_SealNo3.Text;
                det1.YardAddress = btn_YardAddress3.Text;
                det1.Remark = txt_Remark3.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry3.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time3.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight3.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType3.Value);
                det1.UrgentInd = txt_Urgent3.Text;
                det1.Permit = txt_Permit3.Text;
                det1.DgClass = txt_dg3.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo4.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo4.Text;
                det1.ContainerType = txt_ContType4.Text;
                det1.SealNo = txt_SealNo4.Text;
                det1.YardAddress = btn_YardAddress4.Text;
                det1.Remark = txt_Remark4.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry4.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time4.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight4.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType4.Value);
                det1.UrgentInd = txt_Urgent4.Text;
                det1.Permit = txt_Permit4.Text;
                det1.DgClass = txt_dg4.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo5.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo5.Text;
                det1.ContainerType = txt_ContType5.Text;
                det1.SealNo = txt_SealNo5.Text;
                det1.YardAddress = btn_YardAddress5.Text;
                det1.Remark = txt_Remark5.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry5.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time5.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight5.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType5.Value);
                det1.UrgentInd = txt_Urgent5.Text;
                det1.Permit = txt_Permit5.Text;
                det1.DgClass = txt_dg5.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo6.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo6.Text;
                det1.ContainerType = txt_ContType6.Text;
                det1.SealNo = txt_SealNo6.Text;
                det1.YardAddress = btn_YardAddress6.Text;
                det1.Remark = txt_Remark6.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry6.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time6.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight6.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType6.Value);
                det1.UrgentInd = txt_Urgent6.Text;
                det1.Permit = txt_Permit6.Text;
                det1.DgClass = txt_dg6.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo7.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo7.Text;
                det1.ContainerType = txt_ContType7.Text;
                det1.SealNo = txt_SealNo7.Text;
                det1.YardAddress = btn_YardAddress7.Text;
                det1.Remark = txt_Remark7.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry7.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time7.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight7.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType7.Value);
                det1.UrgentInd = txt_Urgent7.Text;
                det1.Permit = txt_Permit7.Text;
                det1.DgClass = txt_dg7.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo8.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo8.Text;
                det1.ContainerType = txt_ContType8.Text;
                det1.SealNo = txt_SealNo8.Text;
                det1.YardAddress = btn_YardAddress8.Text;
                det1.Remark = txt_Remark8.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry8.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time8.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight8.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType8.Value);
                det1.UrgentInd = txt_Urgent8.Text;
                det1.Permit = txt_Permit8.Text;
                det1.DgClass = txt_dg8.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }
            if (btn_ContNo9.Text.Trim().Length > 0)
            {
                det1.ContainerNo = btn_ContNo9.Text;
                det1.ContainerType = txt_ContType9.Text;
                det1.SealNo = txt_SealNo9.Text;
                det1.YardAddress = btn_YardAddress9.Text;
                det1.Remark = txt_Remark9.Text;
                det1.ScheduleDate = SafeValue.SafeDate(date_YardExpiry9.Date, new DateTime(1990, 1, 1));
                det1.ScheduleTime = txt_time9.Text;
                det1.Weight = SafeValue.SafeDecimal(txt_Weight9.Text);
                det1.ServiceType = SafeValue.SafeString(cmb_ServiceType9.Value);
                det1.UrgentInd = txt_Urgent9.Text;
                det1.Permit = txt_Permit9.Text;
                det1.DgClass = txt_dg9.Text;
                det1.JobType = job.JobType;
                det1.Br = job.CarrierBkgNo;
                C2.BizResult bRes = det1Bz.insert(user, det1);
                if (!bRes.status)
                {
                    values = bRes.context;
                }
            }

            if (values.Length == 0)
            {
                values = "Success";
            }
            Response.Write("<script>parent.Popup_ContainerBatchAdd_callback('" + values + "');</script>");
            #endregion

        }
    }

}