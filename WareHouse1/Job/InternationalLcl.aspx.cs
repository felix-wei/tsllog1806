using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Job_InternationalLcl : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsCallback)
        {
            B28.Text = "30.00";
            C23.Text = "105.00";
            if (Request.Params["id"] != null)
            {
                string id = SafeValue.SafeString(Request.Params["id"]);
                txt_Id.Text = id;
                string sql = string.Format("select (Select SaleDocAmt from  CostDet where ParentId='{0}' and ChgCode='ORG006') as C23,(Select SaleDocAmt from  CostDet where ParentId='{0}' and ChgCode='ORG007') as C24,(select Volumne from JobInfo where JobNo=(select RefNo from Cost where SequenceId='{0}')) as Volumn,* from CostDet where ParentId='{0}'", id);
                DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                decimal m = SafeValue.SafeDecimal(dt.Rows[0]["Volumn"], 0);
                B15.Text = SafeValue.SafeString(dt.Rows[0]["Volumn"]);

                C15.Value = SafeValue.ChinaRound(m - m * SafeValue.SafeDecimal(0.2), 0);
                D15.Value = SafeValue.ChinaRound(m * SafeValue.SafeDecimal(6.5), 0);
                B16.Value = SafeValue.ChinaRound(m / SafeValue.SafeDecimal(35.312), 2);
                C16.Value = SafeValue.ChinaRound((m - m * SafeValue.SafeDecimal(0.2)) * SafeValue.SafeDecimal(6.5), 0);
                D16.Value = SafeValue.ChinaRound(m * SafeValue.SafeDecimal(6.5) / SafeValue.SafeDecimal(2.2), 0);
                D18.Value = SafeValue.ChinaRound((m - m * SafeValue.SafeDecimal(0.2)) * 1, 3);
                C20.Value = SafeValue.ChinaRound(m * SafeValue.SafeDecimal(1.2), 2);
                C23.Value = SafeValue.SafeString(dt.Rows[0]["C23"], "0");
                C24.Value = SafeValue.SafeDecimal(dt.Rows[0]["C24"], 0);
                C25.Value = SafeValue.ChinaRound(270 + m * SafeValue.SafeDecimal(1.2) + SafeValue.SafeDecimal(C23.Value) + SafeValue.SafeDecimal(C24.Value), 2);//170+m * 1.2+parseFloat(C23.GetText())+parseFloat(C24.GetText())
                //C28.Text = SafeValue.SafeString(FormatNumber(m / 35.312, 2) * B28_ * E27_);
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string id = SafeValue.SafeString(txt_Id.Text);
        string sql = string.Format("Update JobInfo set Volumne='{1}' where JobNo=(select RefNo from Cost where SequenceId='{0}')", id, B15.Value);
        C2.Manager.ORManager.ExecuteCommand(sql);
        sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' and ChgCode='ORG001' ", id, C18.Value);
        C2.Manager.ORManager.ExecuteCommand(sql);
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Materials' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' and ChgCode='ORG003' ", id, C20.Value);
        C2.Manager.ORManager.ExecuteCommand(sql);
        sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' and ChgCode='ORG006' ", id, C23.Value);
        C2.Manager.ORManager.ExecuteCommand(sql);
        sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' and ChgCode='ORG007' ", id, C24.Value);
        C2.Manager.ORManager.ExecuteCommand(sql);

        this.lab.Text = "Success";
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Trans to Port' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Fumigation' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Other' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Labor' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Labor' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Labor' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        //sql = string.Format("Update CostDet set SaleDocAmt='{1}'  where ParentId='{0}' where ChgCodeDes='Labor' ", id, C18.Value);
        //C2.Manager.ORManager.ExecuteCommand(sql);

    }
}