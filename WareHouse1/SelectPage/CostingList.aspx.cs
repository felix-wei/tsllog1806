using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

public partial class WareHouse_SelectPage_CostingList : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            string doNo = SafeValue.SafeString(Request.QueryString["no"].ToString());
            lbl_DoNo.Text = doNo;

        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    { 
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Costing
    protected void ASPxGridView1_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender  as ASPxGridView;
        string doNo = SafeValue.SafeString(Request.QueryString["no"].ToString());
       this.dsCosting.FilterExpression = "RefNo='" + doNo + "'";
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhCosting));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CostQty"] = 1;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["CostLocAmt"] = 0;
        e.NewValues["CostGst"] = 0;
        e.NewValues["ChgCode"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["CostCurrency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["costExRate"] = 1;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxButtonEdit doNo = ASPxGridView1.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobType"] = "OUT";
        e.NewValues["RefNo"] = doNo.Text;
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void ASPxGridView1_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void ASPxGridView1_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "Import")
        {
            ASPxButtonEdit doNo = ASPxGridView1.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
            ASPxComboBox impExp = this.ASPxGridView1.FindEditFormTemplateControl("cmb_Priority") as ASPxComboBox;
            ASPxComboBox fclLcl = this.ASPxGridView1.FindEditFormTemplateControl("cmb_ModelType") as ASPxComboBox;
            string where = "";
            if (impExp.Text.ToLower() == "import" && fclLcl.Text.ToLower() == "fcl")
            {
                where = "ImpExpInd='WHI' and FclLclInd='Fcl'";
            }
            else if (impExp.Text.ToLower() == "import" && fclLcl.Text.ToLower() == "lcl")
            {
                where = "ImpExpInd='WHI' and FclLclInd='Lcl'";
            }
            else if (impExp.Text.ToLower() == "export" && fclLcl.Text.ToLower() == "fcl")
            {
                where = "ImpExpInd='WHE' and FclLclInd='Fcl'";
            }
            else if (impExp.Text.ToLower() == "export" && fclLcl.Text.ToLower() == "lcl")
            {
                where = "ImpExpInd='WHE' and FclLclInd='Lcl'";
            }
            if (where.Length > 0)
            {
                string sql = @"insert into wh_costing(RefNo,JobType,ChgCode,ChgCodeDes,remark,CostQty,CostPrice,CostCurrency,CostExRate,Unit,CostGst)";
                sql += string.Format(@"select '{0}','OUT',ChgCode,ChgDes,'SKU:'+productcode+';LotNo:'+LotNo
,Case when Qty1*price>=Amt then Qty1 else 1 end as Qty
,Case when Qty1*price>=Amt then Price else Amt end as Price
,Currency,ExRate,Unit,Gst from 
(select productcode,lotno,qty1 from wh_dodet where dono='{0}' and DoType='OUT' ) as tab1
cross  join( select ChgCode,chgdes,Currency,exrate,Price,qty,amt,Unit,gsttype,gst from SeaQuoteDet1 where {1}) as tab2
", doNo.Text, where);
                ConnectSql.ExecuteSql(sql);
                sql = string.Format("Update wh_costing set CostDocAmt=convert(decimal(10,2),CostQty*CostPrice)+convert(decimal(10,2),CostQty*CostPrice)*Costgst where RefNo='{0}' and JobType='OUT'", doNo.Text);
                ConnectSql.ExecuteSql(sql);
                sql = string.Format("Update wh_costing set CostLocAmt=CostDocAmt*CostExRate where RefNo='{0}' and JobType='OUT'", doNo.Text);
                ConnectSql.ExecuteSql(sql);
                e.Result = "Success";
            }
        }
    }
    #endregion
}