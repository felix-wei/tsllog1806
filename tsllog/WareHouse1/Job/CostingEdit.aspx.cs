using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_CostingEdit : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (Request.Params["id"]!=null)
        {
		Response.Redirect(String.Format("/WareHouse/Job/CostTest.aspx?id={0}",Request.Params["id"]));
            txt_Id.Text = Request.Params["id"];
            string sql = "select [Status] from Cost where SequenceId='" + SafeValue.SafeString(Request.Params["id"]) + "'";
            if (SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) != "USE")
            {
                ASPxButton2b.Enabled = false;
                ASPxButton2d.Enabled = false;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_CostDet_DataSelect(object sender, EventArgs e)
    {
        this.dsCosting.FilterExpression = "ParentId='" + SafeValue.SafeString(Request.Params["id"], "0") + "'";
    }
    protected void grid_CostDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CostDet));
        }
    }
    protected void grid_CostDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SaleQty"] = 1;
        e.NewValues["CostQty"] = 1;
        e.NewValues["SalePrice"] = 0;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["SaleLocAmt"] = 0;
        e.NewValues["SaleDocAmt"] = 0;
        e.NewValues["CostLocAmt"] = 0;
        e.NewValues["CostGst"] = 0;
        e.NewValues["ChgCode"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["CostCurrency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["SaleExRate"] = 1;
        e.NewValues["CostExRate"] = 1;
    }
    protected void grid_CostDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //ASPxButtonEdit doNo = grid_Issue.FindEditFormTemplateControl("txt_DoNo") as ASPxButtonEdit;
        e.NewValues["JobType"] = "JO";
        e.NewValues["ParentId"] = SafeValue.SafeInt(Request.Params["id"],0);
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        decimal gstAmt = 0;//SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 1), 2);
        e.NewValues["SaleDocAmt"] = locAmt;
        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_CostDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        UpdateMast(SafeValue.SafeInt(txt_Id.Text, 0));
    }
    protected void grid_CostDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["Unit"]);
        e.NewValues["SaleQty"] = SafeValue.SafeDecimal(e.NewValues["SaleQty"], 1);
        e.NewValues["SalePrice"] = SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0);
        e.NewValues["SaleCurrency"] = SafeValue.SafeString(e.NewValues["SaleCurrency"]);
        e.NewValues["SaleExRate"] = SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 1);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["DocNo"] = SafeValue.SafeString(e.NewValues["DocNo"]);
        e.NewValues["Status2"] = SafeValue.SafeString(e.NewValues["Status2"]);
        e.NewValues["Status3"] = SafeValue.SafeString(e.NewValues["Status3"]);
        e.NewValues["Status4"] = SafeValue.SafeString(e.NewValues["Status4"]);
        e.NewValues["CostDate"] = SafeValue.SafeDate(e.NewValues["CostDate"],new DateTime(1753,01,01));

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        decimal gstAmt = 0;//SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 1), 2);
        e.NewValues["SaleDocAmt"] = locAmt;
        e.NewValues["RowUpdateUser"] = EzshipHelper.GetUserName();
        e.NewValues["RowUpdateTime"] = DateTime.Now;
    }
    protected void grid_CostDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_CostDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        UpdateMast(SafeValue.SafeInt(txt_Id.Text, 0));
    }
    protected void grid_CostDet_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
    }
    protected void grid_CostDet_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }
    protected void grid_CostDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        UpdateMast(SafeValue.SafeInt(txt_Id.Text, 0));
    }
    private void UpdateMast(int mastId)
    {
        if (mastId > 0)
        {
            string sql = string.Format("update Cost set Amount=(select sum(SaleDocAmt) from CostDet where ParentId='{0}')*(1+profitmargin),Amount2=(select sum(SaleDocAmt) from CostDet where ParentId='{0}') WHERE SequenceId='{0}'", mastId);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
    }
    protected void cmb_ChgCode_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb = sender as ASPxComboBox;

        object[] keyValues = new object[cmb.Items.Count];
        object[] chgCode = new object[cmb.Items.Count];
        object[] des1 = new object[cmb.Items.Count];
        object[] unit = new object[cmb.Items.Count];
        object[] acCode = new object[cmb.Items.Count];
        object[] qty = new object[cmb.Items.Count];
        object[] price = new object[cmb.Items.Count];
        object[] gst = new object[cmb.Items.Count];
        object[] gstType = new object[cmb.Items.Count];


        string pol = "";
        string pod = "";

        string sql_ref = string.Format("select Volumne,JobType,Currency,CustomerId from JobInfo where JobNo=(Select RefNo from Cost where SequenceId='{0}')", SafeValue.SafeInt(txt_Id.Text, 0));
        DataTable mast = ConnectSql.GetTab(sql_ref);
        string curr = SafeValue.SafeString(mast.Rows[0]["Currency"]);
        decimal oQty = SafeValue.SafeDecimal(mast.Rows[0]["Volumne"], 0);
        string filter_FclLclInd = "Local";
        string filter_ImpExpInd = "";
        string partyTo = SafeValue.SafeString(mast.Rows[0]["CustomerId"]);
        if (oQty == 0)
            oQty = 1;
        if(1==1)//if (curr.ToUpper() == "SGD" || curr == "USD")
        {
//            if (sql_ref.Length > 0)
//            {
////                string sql = string.Format(@"
////WITH home AS(select SequenceId,ChgCode
////,Case when unit='set' or unit='SHPT' then qty
////     when Amt>0 and {4}*price>=amt then {4}
////     when Amt>0 and {4}*price<amt then 1
////     when Amt=0 and {4}>=Qty then {4}
////     when Amt=0 and {4}<Qty then qty
////     else {4} end as Qty
////,Case when Amt>0 and {4}*price>=amt then price
////     when Amt>0 and {4}*price<amt then amt
////     else price end as Price
////from seaquotedet1 where QuoteId='-1' and FclLclInd='{3}' and (isnull(PartyTo,'')='{0}' or isnull(PartyTo,'')='')
//// and Currency='{6}')
////SELECT Qty,Price,ChgCode FROM home WHERE SequenceId IN (SELECT MAX(SequenceId) FROM home GROUP BY ChgCode)",
////partyTo, pol, pod, filter_FclLclInd, oQty, filter_ImpExpInd, curr);//and ImpExpInd='{5}' and (isnull(Pol,'')='{1}' or isnull(Pol,'')='') and (isnull(Pod,'')='{2}' or isnull(Pod,'')='')

//                string sql = string.Format(@"SELECT * FROM seaquotedet1 where QuoteId='-1' and FclLclInd='{3}' and (isnull(PartyTo,'')='{0}' or isnull(PartyTo,'')='')",
//partyTo, pol, pod, filter_FclLclInd, oQty, filter_ImpExpInd, curr);
//                DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
//                for (int i = 0; i < cmb.Items.Count; i++)
//                {
//                    keyValues[i] = cmb.Items[i].GetValue("SequenceId");
//                    chgCode[i] = cmb.Items[i].GetValue("ChgcodeId");
//                    des1[i] = cmb.Items[i].GetValue("ChgcodeDe");
//                    unit[i] = cmb.Items[i].GetValue("ChgUnit");
//                    acCode[i] = cmb.Items[i].GetValue("ArCode");
//                    gst[i] = cmb.Items[i].GetValue("GstP");
//                    gstType[i] = cmb.Items[i].GetValue("GstTypeId");


//                    for (int r = 0; r < dt.Rows.Count; r++)
//                    {
//                        if (dt.Rows[r]["ChgCode"].ToString() == chgCode[i].ToString())
//                        {
//                            qty[i] = dt.Rows[r]["Qty"];
//                            price[i] = dt.Rows[r]["Price"];
//                        }
//                    }
//                }
//            }
//            else
//            {
//                for (int i = 0; i < cmb.Items.Count; i++)
//                {
//                    keyValues[i] = cmb.Items[i].GetValue("SequenceId");
//                    des1[i] = cmb.Items[i].GetValue("ChgcodeDe");
//                    unit[i] = cmb.Items[i].GetValue("ChgUnit");
//                    acCode[i] = cmb.Items[i].GetValue("ArCode");
//                    gst[i] = cmb.Items[i].GetValue("GstP");
//                    gstType[i] = cmb.Items[i].GetValue("GstTypeId");
//                }
//            }
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                keyValues[i] = cmb.Items[i].GetValue("SequenceId");
                des1[i] = cmb.Items[i].GetValue("ChgcodeDe");
                unit[i] = cmb.Items[i].GetValue("ChgUnit");
                acCode[i] = cmb.Items[i].GetValue("ArCode");
                gst[i] = cmb.Items[i].GetValue("GstP");
                gstType[i] = cmb.Items[i].GetValue("GstTypeId");
                if (SafeValue.SafeString(cmb.Items[i].GetValue("ChgUnit")).ToUpper() == "VOL")
                    qty[i] = oQty;
            }
        }
        else
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                keyValues[i] = cmb.Items[i].GetValue("SequenceId");
                des1[i] = cmb.Items[i].GetValue("ChgcodeDe");
                unit[i] = cmb.Items[i].GetValue("ChgUnit");
                acCode[i] = cmb.Items[i].GetValue("ArCode");
                gst[i] = cmb.Items[i].GetValue("GstP");
                gstType[i] = cmb.Items[i].GetValue("GstTypeId");
            }
        }
        e.Properties["cpDes1"] = des1;
        e.Properties["cpUnit"] = unit;
        e.Properties["cpAcCode"] = acCode;
        e.Properties["cpQty"] = qty;
        e.Properties["cpPrice"] = price;
        e.Properties["cpGst"] = gst;
        e.Properties["cpGstType"] = gstType;
        e.Properties["cpKeyValues"] = keyValues;
    }
}