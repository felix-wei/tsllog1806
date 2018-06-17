using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;
using System.Data;

public partial class WareHouse_SelectPage_SelectSalesOrderProduct : System.Web.UI.Page
{
    protected void Page_Init()
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string soNo = SafeValue.SafeString(Request.QueryString["no"].ToString());
            lbl_SoNo.Text = soNo;
        }
    }
    static public string GetPatientName(object icNo)
    {
        if (SafeValue.SafeString(icNo, "").Length > 0)
        {
            string sql = "select Name from Ref_PersonInfo where ICNo='" + icNo + "'";
            return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        return "";
    }
    private void InsertInv_Det(int docId, string docNo, int index, string sku, string lotNo, string des, int qty, decimal price, string cur, decimal exRate, decimal gst, string soNo, string doNo, string mastType, string docType)
    {
        try
        {
            C2.XAArInvoiceDet det = new C2.XAArInvoiceDet();
            det.DocId = docId;
            det.DocLineNo = index;
            det.DocNo = docNo;
            det.DocType = docType;
            det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ArCode from ref_product where Code='{0}'", sku)), System.Configuration.ConfigurationManager.AppSettings["LocalArCode"]);
            det.AcSource = "CR";
            det.MastRefNo = soNo;
            det.JobRefNo = lotNo;
            if (doNo.Length > 0)
            {
                det.MastRefNo = doNo;
                det.JobRefNo = "";
            }
            det.MastType = mastType;
            det.SplitType = "";



            det.ChgCode = sku + "(Product)";
            det.ChgDes1 = des;
            det.ChgDes2 = "";
            det.ChgDes3 = "";

            det.Price = price;
            det.Qty = qty;
            det.Unit = "";
            det.Currency = cur;
            det.ExRate = 1;
            det.Gst = gst;
            if (det.ExRate == 0)
                det.ExRate = 1;
            if (det.Gst > 0)
                det.GstType = "S";
            else if (det.Currency == System.Configuration.ConfigurationManager.AppSettings["Currency"])
                det.GstType = "E";
            else
                det.GstType = "Z";
            decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
            decimal gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);
            decimal docAmt = amt + gstAmt;
            decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
            det.GstAmt = gstAmt;
            det.DocAmt = docAmt;
            det.LocAmt = locAmt;
            det.OtherAmt = 0;
            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det);
        }
        catch
        {
        }
    }
    private void UpdateMaster(int docId)
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

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #region SKULine
    protected void grid_SKULine_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhTransDet));
        }
    }
    protected void grid_SKULine_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string soNo = SafeValue.SafeString(Request.QueryString["no"].ToString());
        this.dsIssueDet.FilterExpression = "DoType= 'SO' and DoNo='" + soNo + "'";
    }
    protected void grid_SKULine_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty1"] = 1;
        e.NewValues["Qty2"] = 0;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Qty4"] = 0;
        e.NewValues["Qty5"] = 0;
        e.NewValues["QtyPackWhole"] = 1;
        e.NewValues["QtyWholeLoose"] = 1;
        e.NewValues["QtyLooseBase"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["Gst"] = 0;
        e.NewValues["ExRate"] = 1.000000;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["GstType"] = "Z";
    }
    protected void grid_SKULine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("Product not be null !!!");
            return;
        }
        string soNo = SafeValue.SafeString(Request.QueryString["no"].ToString());
        e.NewValues["DoNo"] = soNo;
        e.NewValues["DoType"] = "SO";

        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);

        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);

        ASPxComboBox cb_Att1 = grid_SKULine.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid_SKULine.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;

        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["Currency"] = SafeValue.SafeString(e.NewValues["Currency"]);
        e.NewValues["ExRate"] = SafeValue.SafeDecimal(e.NewValues["ExRate"], 0);
        e.NewValues["Gst"] = SafeValue.SafeDecimal(e.NewValues["Gst"], 0);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Today;
        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty1"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;


    }
    protected void grid_SKULine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.NewValues["ProductCode"] == null || e.NewValues["ProductCode"].ToString().Trim() == "")
        {
            throw new Exception("Product not be null !!!");
            return;
        }
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);

        ASPxComboBox cb_Att1 = grid_SKULine.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid_SKULine.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;

        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);


        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["Currency"] = SafeValue.SafeString(e.NewValues["Currency"]);
        e.NewValues["ExRate"] = SafeValue.SafeDecimal(e.NewValues["ExRate"], 0);
        e.NewValues["Gst"] = SafeValue.SafeDecimal(e.NewValues["Gst"], 0);
        e.NewValues["GstType"] = SafeValue.SafeString(e.NewValues["GstType"]);
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Today;
        e.NewValues["ExpiredDate"] = SafeValue.SafeDate(e.NewValues["ExpiredDate"], DateTime.Today);

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty1"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
    }
    protected void grid_SKULine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_SKULine_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_SKULine_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {

    }
    private void UpdatePoDetBalQty(int transId)
    {
        string sql = string.Format(@"update Wh_TransDet set BalQty=Qty-isnull((select sum(Qty) from Wh_DoDet where DoInId={0}),0) where Id='{0}'", transId);
        C2.Manager.ORManager.ExecuteScalar(sql);
    }
    private int AddNewSkuLine(int qty, string doNo, string product, string lotNo, string des, string uom1, int pkg, int unit, string att1
        , string att2, string att3, string att4, string att5, string att6, int stk, string uom2, string uom3, decimal price, string packing, string wh)
    {
        string sql = string.Format(@"select tab_hand.HandQty-isnull(tab_Reserved.ReservedQty,0) as BalQty
from (select product,LotNo,Packing ,sum(isnull(Case when det.DoType='In' then Qty1 else -Qty1 end,0)) as HandQty from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode!='CNL' group by product,LotNo,Packing) as tab_hand
left join (select productCode as Product,LotNo,sum(Qty5) as ReservedQty from wh_doDet det inner join  wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' group by productCode,LotNo) as tab_Reserved on tab_Reserved.product=tab_hand.product and tab_Reserved.LotNo=tab_hand.LotNo 
where tab_hand.Product='{0}'", product, lotNo);
        int balQty = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (qty > balQty && balQty != 0)
        {
            sql = @"Insert Into wh_TransDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Qty4,Qty5,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,DocAmt,LocationCode)";
            sql += string.Format(@"select '{0}'as DoNo, 'SO' as DoType,'{1}' as Sku,'{2}' as Qty1,0 as Qty2,0 as Qty3,0 as Qty4,0 as Qty5,'{3}' as Price,'{4}' as LotNo
,'{5}' as Uom1,'{6}' as Uom2,'{7}' as Uom3,'' as Uom4
,'{8}' as QtyPackWhole,'{9}' as QtyWholeLoose,'{10}' as QtyLooseBase
,'{11}' as CreateBy,getdate() as CreateDateTime,'{11}' as UpdateBy,getdate() as UpdateDateTime
,'{12}' as att1,'{13}' as att2,'{14}' as att3,'{15}' as att4,'{16}' as att5,'{17}' as att6,'{18}' as Des1,'{19}' as Packing,({2}*{3}) as DocAmt,'{20}' as LocationCode
from (select '{1}' as Sku) as tab", doNo, product, (qty - balQty), price, lotNo, uom1, uom2, uom3, pkg, unit, stk, EzshipHelper.GetUserName(), att1, att2, att3, att4, att5, att6, des, packing, wh);
            ConnectSql.ExecuteSql(sql);
        }
        else
        {
            balQty = qty;
        }
        return balQty;
    }

    protected void grid_SKULine_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        string soNo = SafeValue.SafeString(Request.QueryString["no"].ToString());
        string schId = SafeValue.SafeString(Request.QueryString["SchId"].ToString());
        string userId = HttpContext.Current.User.Identity.Name;
        if (s == "Save")
        {
            bool result = false;
            string issueN = "";
            string sql =string.Format(@"select * from Wh_TransDet where DoNo='{0}'",soNo);
            DataTable tab = ConnectSql.GetTab(sql);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (SafeValue.SafeInt(tab.Rows[i]["Qty1"],0) == 0)
                {
                    e.Result = "Fail,Please keyin Qty ";
                    return;
                }
            }
            C2.XAArInvoice inv = null;
            C2.XAArInvoice inv_Do = null;
            if (tab.Rows.Count > 0)
            {

                //Get SO
                #region Get So
                bool isNew = false;
                Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "DoNo='" + soNo + "'");
                WhTrans so = C2.Manager.ORManager.GetObject(query1) as WhTrans;
                string sql1 = string.Format("select doNo from wh_do where DoType='Out' and PoNo='{0}'", soNo);
                DataTable tab_do = ConnectSql.GetTab(sql1);
                string where = "(1=0";
                for (int i = 0; i < tab_do.Rows.Count; i++)
                {
                    where += string.Format(" or DoNo='{0}' ", tab_do.Rows[i][0]);

                }
                where += ")";

                int cnt = 0;
                sql = string.Format("select count(*) from wh_transDet where DoNo='{0}' and DoType='SO' and isnull(LotNo,'')='' ", soNo);
                cnt = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
                if (cnt > 0)
                {
                    e.Result = "No Balance Qty or No Lot No!";
                    return;
                }
                string update = string.Format(@"update Wh_Schedule set StatusCode='Closed' where Id='{1}'", soNo, schId);
                C2.Manager.ORManager.ExecuteCommand(update);
                #endregion
                #region Create SO Invoice
                int invId = 0;
                string invN = "";
                inv = Manager.ORManager.GetObject(typeof(XAArInvoice), SafeValue.SafeInt(invId, 0)) as XAArInvoice;
                if (inv == null)// first insert invoice
                {
                    string counterType = "AR-IV";
                    inv = new XAArInvoice();
                    invN = C2Setup.GetNextNo("", counterType, so.DoDate);
                    inv.PartyTo = SafeValue.SafeString(so.PartyId, "");
                    inv.DocType = "IV";
                    inv.DocNo = invN.ToString();
                    inv.DocDate = so.DoDate;
                    string[] currentPeriod = EzshipHelper.GetAccPeriod(so.DoDate);

                    inv.AcYear = SafeValue.SafeInt(currentPeriod[1], so.DoDate.Year);
                    inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], so.DoDate.Month);
                    inv.Term = so.PayTerm;

                    //
                    int dueDay = 0;
                    if (so.PayTerm != null)
                    {
                        dueDay = SafeValue.SafeInt(so.PayTerm.ToUpper().Replace("DAYS", ""), 0);
                    }

                    inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
                    inv.Description = so.Remark;
                    inv.CurrencyId = so.Currency;
                    inv.ExRate = SafeValue.SafeDecimal(so.ExRate, 1);
                    if (inv.ExRate <= 0)
                        inv.ExRate = 1;
                    inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
                    inv.AcSource = "DB";
                    inv.SpecialNote = "";

                    inv.MastType = "WH";
                    inv.MastRefNo = so.DoNo;
                    inv.JobRefNo = "";

                    inv.ExportInd = "N";
                    inv.UserId = HttpContext.Current.User.Identity.Name;
                    inv.EntryDate = DateTime.Now;
                    inv.CancelDate = new DateTime(1900, 1, 1);
                    inv.CancelInd = "N";
                    try
                    {
                        C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(inv);
                        C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
                    }
                    catch
                    {
                    }
                }
                #endregion
                #region Create DO
                //Create DoNo
                string doId = "";
                Wilson.ORMapper.OPathQuery query2 = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + doId + "'");
                WhDo whDo = C2.Manager.ORManager.GetObject(query2) as WhDo;

                if (whDo == null)
                {
                    whDo = new WhDo();
                    isNew = true;
                    issueN = C2Setup.GetNextNo("", "DOOUT", so.DoDate);
                    whDo.DoType = "OUT";
                }
                whDo.DoNo = issueN;
                whDo.PoNo = so.DoNo;
                whDo.DoDate = so.DoDate;
                whDo.PoDate = so.DoDate;
                whDo.StatusCode = "CLS";
                whDo.PartyId = so.PartyId;
                whDo.PartyName = so.PartyName;
                whDo.AgentId = so.AgentId;
                whDo.AgentName = so.AgentName;
                whDo.AgentTel = so.AgentTel;
                whDo.AgentZip = so.AgentZip;
                whDo.AgentCountry = so.AgentCountry;
                whDo.AgentCity = so.AgentCity;
                whDo.NotifyId = so.NotifyId;
                whDo.NotifyName = so.NotifyName;
                whDo.WareHouseId = so.WareHouseId;
                whDo.CreateBy = so.CreateBy;
                whDo.CreateDateTime = so.CreateDateTime;
                whDo.UpdateBy = so.UpdateBy;
                whDo.UpdateDateTime = so.UpdateDateTime;
                if (isNew)
                {
                    Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(whDo);
                    C2Setup.SetNextNo("", "DOOUT", issueN, DateTime.Now);
                }
                #endregion
                #region Create DO Invoice
                inv_Do = Manager.ORManager.GetObject(typeof(XAArInvoice), SafeValue.SafeInt(invId, 0)) as XAArInvoice;
                if (inv_Do == null)// first insert invoice
                {
                    string counterType = "AR-IV";
                    inv_Do = new XAArInvoice();
                    invN = C2Setup.GetNextNo("", counterType, so.DoDate);
                    inv_Do.PartyTo = SafeValue.SafeString(so.PartyId, "");
                    inv_Do.DocType = "IV";
                    inv_Do.DocNo = invN.ToString();
                    inv_Do.DocDate = so.DoDate;
                    string[] currentPeriod = EzshipHelper.GetAccPeriod(so.DoDate);

                    inv_Do.AcYear = SafeValue.SafeInt(currentPeriod[1], so.DoDate.Year);
                    inv_Do.AcPeriod = SafeValue.SafeInt(currentPeriod[0], so.DoDate.Month);
                    inv_Do.Term = so.PayTerm;

                    //
                    int dueDay = 0;
                    if (so.PayTerm != null)
                    {
                        dueDay = SafeValue.SafeInt(so.PayTerm.ToUpper().Replace("DAYS", ""), 0);
                    }

                    inv_Do.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
                    inv_Do.Description = so.Remark;
                    inv_Do.CurrencyId = so.Currency;
                    inv_Do.ExRate = SafeValue.SafeDecimal(so.ExRate, 1);
                    if (inv_Do.ExRate <= 0)
                        inv_Do.ExRate = 1;
                    inv_Do.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
                    inv_Do.AcSource = "DB";
                    inv_Do.SpecialNote = "";

                    inv_Do.MastType = "WH";
                    inv_Do.MastRefNo = whDo.DoNo;
                    inv_Do.JobRefNo = "";

                    inv_Do.ExportInd = "N";
                    inv_Do.UserId = HttpContext.Current.User.Identity.Name;
                    inv_Do.EntryDate = DateTime.Now;
                    inv_Do.CancelDate = new DateTime(1900, 1, 1);
                    inv_Do.CancelInd = "N";
                    try
                    {
                        C2.Manager.ORManager.StartTracking(inv_Do, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(inv_Do);
                        C2Setup.SetNextNo("", counterType, invN, inv_Do.DocDate);
                    }
                    catch
                    {
                    }
                }
                #endregion
            }
            #region Create Transfer
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                string product =SafeValue.SafeString(tab.Rows[i]["ProductCode"]);
                string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
                int qty =SafeValue.SafeInt(tab.Rows[i]["Qty1"],0);
                decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"]);
                string des1 = SafeValue.SafeString(tab.Rows[i]["Des1"]);
                string location = SafeValue.SafeString(tab.Rows[i]["LocationCode"]);

                sql = @"Insert Into wh_DoDet(JobStatus,DoNo, DoType,ProductCode,ExpiredDate,Price,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
                sql += string.Format(@" select * from (select 'Picked' as JobStatus,'{1}'as DoNo, 'Out' as DoType,ProductCode,ExpiredDate,Price
,Qty1 as Qty1
,0 as Qty2
,0 as Qty3
,Qty1 as Qty4
,0 as Qty5
, LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,'{2}' as CreateBy,getdate() as CreateDateTime,'{2}' as UpdateBy,getdate() as UpdateDateTime,Id as DoInId from Wh_TransDet where DoNo='{0}' and ProductCode='{4}' and LotNo='{5}' and DoType='SO' and LocationCode='{3}'
 ) as tab_aa where qty4>0  ", soNo, issueN, userId, location, product, lotNo);

                C2.Manager.ORManager.ExecuteCommand(sql);

                sql = @"Insert Into Wh_DoDet2(DoNo,DoType,Product,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,Location,ProcessStatus)";
                sql += string.Format(@"select '{0}'as DoNo, 'OUT' as DoType,'{1}' as Sku
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,'{3}' as Price
,'{4}' as LotNo
,ref.UomPacking as Uom1,ref.UomWhole as Uom2,ref.UomLoose as Uom3,ref.UomBase as Uom4
,ref.QtyPackingWhole as QtyPackWhole,ref.QtyWholeLoose as  QtyWholeLoose,ref.QtyLooseBase as QtyLooseBase
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
,ref.Att4 as Att1,ref.Att5 as Att2,ref.att6 as Att3,ref.att7 as Att4,ref.att8 as Att5,ref.att9 as Att6,ref.Description as Des1,'' as Packing,'{6}' as Location,'Delivered' as ProcessStatus
from (select '{1}' as Sku) as tab inner join ref_product ref on ref.Code=tab.Sku", issueN, product, qty, price, lotNo, userId, location);
                C2.Manager.ORManager.ExecuteCommand(sql);
                InsertInv_Det(inv.SequenceId, inv.DocNo, i + 1, product, lotNo, des1, qty, price, inv.CurrencyId, inv.ExRate, 0, inv.MastRefNo, "", inv.MastType, "IV");
                UpdateMaster(inv.SequenceId);

                InsertInv_Det(inv_Do.SequenceId, inv_Do.DocNo, i + 1, product, lotNo, des1, qty, price, inv_Do.CurrencyId, inv_Do.ExRate, 0, inv_Do.MastRefNo, "", inv_Do.MastType, "IV");
                UpdateMaster(inv_Do.SequenceId);
                if (soNo.Length > 0)
                {
                    result = true;
                }
            }
            #endregion
            if (result)
            {
                e.Result = issueN;
            }
            else
            {
                e.Result = "Fail,Please keyin select product ";
            }

        }
    }
    #endregion

}