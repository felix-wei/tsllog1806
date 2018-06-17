using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Freight_SelectPage_Booking_BatchAdd : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
                DateTime dt = DateTime.Now;

                rows_init();
            
        }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"insert into job_house (BookingNo,HblNo,Weight,Volume,Qty,DgClass,ConsigneeInfo,ConsigneeContact,ConsigneeEmail,ConsigneeTel,ConsigneeZip,ConsigneeAddress,Remark1,JobNo,RefNo,CargoStatus,CargoType,JobType,LineId,SendMode,ClientId,ClientContact,ClientEmail,ClientTel,ClientZip)
values");
        string values = "";
        string jobNo = "";
        if (btn_ClientId.Text.Trim().Length > 0)
        {
            jobNo = add_job(btn_ClientId.Text);
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{13}','P','IN','WGR',0,'{14}','{15}','{16}','{17}','{18}','{19}')", txt_BookingNo.Text, txt_HblNo.Text, spin_Weight.Value, spin_Volume.Value, spin_Qty.Value, cbb_DgClass.Value, 
                btn_ConsigneeId.Text, txt_ConsigneeContact.Text, txt_ConsigneeEmail.Text, 
                txt_ConsigneeTel.Text, txt_ConsigneeZip.Text, memo_ConsigneeAddress.Text,
                txt_Remark.Text, jobNo, cbb_SendMode.Text, btn_ClientId.Text, txt_ClientContact.Text, txt_ClientEmail.Text,txt_ClientTel.Text,txt_ClientZip.Text);
        }
        if (btn_ClientId1.Text.Trim().Length > 0)
        {
            jobNo = add_job(btn_ClientId1.Text);
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{13}','P','IN','WGR',0,'{14}','{15}','{16}','{17}','{18}','{19}')", txt_BookingNo1.Text, txt_HblNo1.Text, spin_Weight1.Value, spin_Volume1.Value, spin_Qty1.Value, cbb_DgClass1.Value, btn_ConsigneeId1.Text, txt_ConsigneeContact1.Text, txt_ConsigneeEmail1.Text, txt_ConsigneeTel1.Text, txt_ConsigneeZip1.Text, memo_ConsigneeAddress1.Text, txt_Remark1.Text, jobNo, cbb_SendMode1.Text, btn_ClientId1.Text, txt_ClientContact1.Text, txt_ClientEmail1.Text, txt_ClientTel1.Text, txt_ClientZip1.Text);
        }
        if (btn_ClientId2.Text.Trim().Length > 0)
        {
            jobNo = add_job(btn_ClientId2.Text);
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{13}','P','IN','WGR',0,'{14}','{15}','{16}','{17}','{18}','{19}')", txt_BookingNo2.Text, txt_HblNo2.Text, spin_Weight2.Value, spin_Volume2.Value, spin_Qty2.Value, cbb_DgClass2.Value, btn_ConsigneeId2.Text, txt_ConsigneeContact2.Text, txt_ConsigneeEmail2.Text, txt_ConsigneeTel2.Text, txt_ConsigneeZip2.Text, memo_ConsigneeAddress2.Text, txt_Remark2.Text, jobNo, cbb_SendMode2.Text, btn_ClientId2.Text, txt_ClientContact2.Text, txt_ClientEmail2.Text, txt_ClientTel2.Text, txt_ClientZip2.Text);
        }
        if (btn_ClientId3.Text.Trim().Length > 0)
        {
            jobNo = add_job(btn_ClientId3.Text);
            values += (values.Length > 0 ? "," : "") + string.Format(@"('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{13}','P','IN','WGR',0,'{14}','{15}','{16}','{17}','{18}','{19}')", txt_BookingNo3.Text, txt_HblNo3.Text, spin_Weight3.Value, spin_Volume3.Value, spin_Qty3.Value, cbb_DgClass3.Value, btn_ConsigneeId3.Text, txt_ConsigneeContact3.Text, txt_ConsigneeEmail3.Text, txt_ConsigneeTel3.Text, txt_ConsigneeZip3.Text, memo_ConsigneeAddress3.Text, txt_Remark3.Text, jobNo, cbb_SendMode3.Text, btn_ClientId3.Text, txt_ClientContact3.Text, txt_ClientEmail3.Text, txt_ClientTel3.Text, txt_ClientZip2.Text);
        }
        string re = "false";
        if (values.Length > 0)
        {
            sql = sql + values;
            int i = ConnectSql.ExecuteSql(sql);
            if (i > 0)
            {
                re = "success";
                update_Line();
                //Trip_new_auto("");
            }
        }
        Response.Write("<script>parent.Popup_BookingBatchAdd_callback('" + re + "');</script>");
    }
    private void update_Line() {
        string sql = string.Format(@"update job_house set LineId=Id where CargoType='IN' and LineId=0");
        ConnectSql.ExecuteSql(sql);
    }
    private string add_job(string client) {
        DateTime now = DateTime.Now;
        string jobNo = "";
        C2.CtmJob job=new C2.CtmJob();
        jobNo=C2Setup.GetNextNo("","CTM_Job_WGR",DateTime.Now);
        job.JobNo = jobNo;
        job.JobDate = DateTime.Now;
        job.ClientId = client;
        job.DeliveryTo = "";
        job.StatusCode = "USE";
        job.QuoteNo = jobNo;
        job.QuoteStatus = "None";
        job.JobStatus = "Confirmed";
        job.JobType = "WGR";
        C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(job);
        C2Setup.SetNextNo("", "CTM_Job_WGR", jobNo, now);
        return jobNo;
    }
    private void rows_init()
    {
        spin_Weight.Value = "0";
        spin_Volume.Value = "0";
        spin_Qty.Value = "1";

        spin_Weight1.Value = "0";
        spin_Volume1.Value = "0";
        spin_Qty1.Value = "1";

        spin_Weight2.Value = "0";
        spin_Volume2.Value = "0";
        spin_Qty2.Value = "1";

        spin_Weight3.Value = "0";
        spin_Volume3.Value = "0";
        spin_Qty3.Value = "1";
    }
}