using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_Container_BatchAdd : System.Web.UI.Page
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
            }
        }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"insert into CTM_JobDet1 (JobNo,ContainerNo,ContainerType,SealNo,Weight,Volume,QTY,PackageType,RequestDate,ScheduleDate,CfsInDate,CfsOutDate,YardPickupDate,YardReturnDate,F5Ind,UrgentInd,StatusCode,CdtDate,YardExpiryDate,YardAddress,Remark)
values");
        string values = "";
        if (btn_ContNo.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo.Text, txt_ContType.Text, txt_SealNo.Text, dt, txt_YardAddress.Text, txt_Remark.Text);
        }
        if (btn_ContNo1.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry1.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo1.Text, txt_ContType1.Text, txt_SealNo1.Text, dt, txt_YardAddress1.Text, txt_Remark1.Text);
        }
        if (btn_ContNo2.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry2.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo2.Text, txt_ContType2.Text, txt_SealNo2.Text, dt, txt_YardAddress2.Text, txt_Remark2.Text);
        }
        if (btn_ContNo3.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry3.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo3.Text, txt_ContType3.Text, txt_SealNo3.Text, dt, txt_YardAddress3.Text, txt_Remark3.Text);
        }
        if (btn_ContNo4.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry4.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo4.Text, txt_ContType4.Text, txt_SealNo4.Text, dt, txt_YardAddress4.Text, txt_Remark4.Text);
        }
        if (btn_ContNo5.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry5.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo5.Text, txt_ContType5.Text, txt_SealNo5.Text, dt, txt_YardAddress5.Text, txt_Remark5.Text);
        }
        if (btn_ContNo6.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry6.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo6.Text, txt_ContType6.Text, txt_SealNo6.Text, dt, txt_YardAddress6.Text, txt_Remark6.Text);
        }
        if (btn_ContNo7.Text.Trim().Length > 0)
        {
            DateTime dt = SafeValue.SafeDate(date_YardExpiry7.Date, new DateTime(1990, 1, 1));
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}','{2}','{3}','0','0','0','',getdate(),getdate(),getdate(),getdate(),getdate(),getdate(),'N','N','New',getdate(),'{4}','{5}','{6}')", txt_JobNo.Text, btn_ContNo7.Text, txt_ContType7.Text, txt_SealNo7.Text, dt, txt_YardAddress7.Text, txt_Remark7.Text);
        }


        string re = "false";
        if (values.Length > 0)
        {
            sql = sql + values;
            int i = ConnectSql.ExecuteSql(sql);
            if (i > 0)
            {
                re = "success";
                Trip_new_auto(txt_JobNo.Text);
            }
        }


        Response.Write("<script>parent.Popup_ContainerBatchAdd_callback('" + re + "');</script>");
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
}