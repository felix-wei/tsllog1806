using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_EmailExp_byContainer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
            string contId = SafeValue.SafeString(Request.QueryString["contId"]);
            if (!jobNo.Equals(""))
            {
                txt_JobNo.Text = jobNo;
                txt_ContainerNo.Text = contId;

                string sql = string.Format(@"select j.JobNo,j.ClientRefNo,j.ClientId,j.EmailAddress,j.Pod,j.ClientRefNo,j.Vessel,j.Voyage,j.EtaDate,
j.CarrierBkgNo as bookinbRefno,
det1.ContainerNo,det1.SealNo 
from CTM_JobDet1 as det1 
left outer join ctm_job as j on det1.JobNo=j.JobNo
where det1.Id={0}", txt_ContainerNo.Text);
                DataTable dt = ConnectSql.GetTab(sql);
                if (dt.Rows.Count > 0)
                {
                    string name = dt.Rows[0]["ClientId"].ToString();
                    string email = dt.Rows[0]["EmailAddress"].ToString();
                    string title = string.Format(@"PORT: {0}  JOB: {1}", dt.Rows[0]["Pod"], dt.Rows[0]["ClientRefNo"]);
                    string demo = string.Format(@"VESSEL: {0} / {1}
CONTAINER NO: {2}
SEAL NO: {3}
BOOKING REF: {4}
ETA SIN: {5}", dt.Rows[0]["Vessel"], dt.Rows[0]["Voyage"], dt.Rows[0]["ContainerNo"], dt.Rows[0]["SealNo"], dt.Rows[0]["bookinbRefno"], SafeValue.SafeDate(dt.Rows[0]["EtaDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy"));
                    txt_name.Text = name;
                    txt_address.Text = email;
                    txt_title.Text = title;
                    txt_context.Text = demo;
                }

                //sql = string.Format(@"select ContainerNo from ctm_jobdet1 where Id={0}", contId);
                //dt = ConnectSql.GetTab(sql);
                //if (dt.Rows.Count > 0)
                //{
                //    string ContainerNo = dt.Rows[0]["ContainerNo"].ToString();
                //    txt_ContainerNo.Text = ContainerNo;
                //}
            }
        }
    }
    protected void btn_Send_Click(object sender, EventArgs e)
    {
        Regex reg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        if (reg.IsMatch(txt_address.Text))
        {

            string res = Helper.Email.SendEmail(txt_address.Text, "", "", txt_title.Text, txt_context.Text, "");
            if (res.Equals(""))
            {
                string sql = string.Format(@"select ContainerNo from ctm_jobdet1 where Id={0}", txt_ContainerNo.Text);
                string containerNo = "";
                DataTable dt = ConnectSql.GetTab(sql);
                if (dt.Rows.Count > 0)
                {
                    containerNo = dt.Rows[0]["ContainerNo"].ToString();

                    sql = string.Format(@"update ctm_jobdet1 set EmailInd='Y' where Id={0}", txt_ContainerNo.Text);
                    ConnectSql.ExecuteSql(sql);
                }

                C2.CtmJobEventLog l = new C2.CtmJobEventLog();
                l.JobNo = txt_JobNo.Text;
                l.ContainerNo = containerNo;
                l.Controller = User.Identity.Name;
                l.Remark = l.Controller + " send email to " + txt_name.Text + "(" + txt_address.Text + "),title:" + txt_title.Text;
                l.log();

                Response.Write("<script>alert('Send successful');</script>");
            }
            else
            {
                Response.Write("<script>alert('Send Error,{0}');</script>" + res);
            }
        }
        else
        {
            Response.Write("<script>alert('Eamil Address Error');</script>");
        }
    }
}