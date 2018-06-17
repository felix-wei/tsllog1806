using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxDataView;
using C2;
using Wilson.ORMapper;
public partial class WareHouse_AutoCreateInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            //Timer time = new Timer();
            //System.Timers.Timer t = new System.Timers.Timer(86400000);
            //t.Elapsed += new System.Timers.ElapsedEventHandler(btn_CreateIV_Click); //到达时间的时候执行事件；   
            //t.AutoReset = true; //设置是执行一次（false）还是一直执行(true)；   
            //t.Enabled = true; //是否执行System.Timers.Timer.Elapsed事件；  
            //btn_CreateIV_Click(null,null);
        }
    }
    /// <summary>
    /// if dotype=in, then calc lastschqty and preqty
    /// if dotype=out , calc preqty
    /// don't calc the transfer data(which dono='').
    /// for do in
    /// if isSch= N , only get lastSchQty
    /// if isSch=Y , need LastSchQty and qty-preQty
    /// for do out 
    /// only get the qty-preqty
    /// 
    /// update create invoice, 
    /// update preqty=qty,lastschqty=inqty-outqty where dotype=in and dono!=''
    /// update preqty=qty,lastschqty=0 where dotype=out and dono!=''
    /// </summary>

    protected void btn_CreateIV_Click1(object sender, EventArgs e)
    {
        //1: issch=false , the qty=qty1   n=schdate-dodate
        //2:issch=true, qty=lastschqty    n=currentschdate-lastschdate
        //3:issch=true, qty=qty-preqty    n=schdate-dodate

        string sql = @"select * from (select mast.DoNo,mast.DoDate,mast.DoType,mast.PartyId,mast.Quotation as ContractNo,det.Product as Sku,det.LotNo
,Case when isnull(det.isSch,0)=0 then isnull(det.Qty1,0)  else isnull(det.Qty1,0)-isnull(det.PreQty1,0) end  as Qty1
,Case when isnull(det.isSch,0)=0 then isnull(det.Qty2,0)  else isnull(det.Qty2,0)-isnull(det.PreQty2,0) end  as Qty2
,Case when isnull(det.isSch,0)=0 then isnull(det.Qty3,0)  else isnull(det.Qty3,0)-isnull(det.PreQty3,0) end  as Qty3

,Case when isnull(det.isSch,0)=1 and det.DoType='IN' then isnull(det.LastSchQty1,0)  else 0 end  as LastSchQty1
,Case when isnull(det.isSch,0)=1 and det.DoType='IN' then isnull(det.LastSchQty2,0)  else 0 end  as LastSchQty2
,Case when isnull(det.isSch,0)=1 and det.DoType='IN' then isnull(det.LastSchQty3,0)  else 0 end  as LastSchQty3
,det.IsSch,det.LastSchDate,ref.ArCode,ref.Description
 from wh_dodet2 det 
inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.Dotype
inner join ref_product ref on ref.Code=det.Product
where mast.StatusCode!='CNL' and len(isnull(det.DoNo,''))>0
) as tab where (Qty1+Qty2+Qty3>0 or LastSchQty1+LastSchQty2+LastSchQty3>0)
order by PartyId,DoDate,DoNo";
        //one customer only have one invoice
        DataTable tab = ConnectSql.GetTab(sql);
        string lastPartyId = "";
        int lastInvId = 0;
        string lastInvNo = "";
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            string sku = SafeValue.SafeString(tab.Rows[i]["Sku"]);
            string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
            string contractNo = SafeValue.SafeString(tab.Rows[i]["ContractNo"]);
            DateTime doDate = SafeValue.SafeDate(tab.Rows[i]["DoDate"], DateTime.Today);
            string partyId = SafeValue.SafeString(tab.Rows[i]["PartyId"]);
            int qty1 = SafeValue.SafeInt(tab.Rows[i]["Qty1"], 0);
            int qty2 = SafeValue.SafeInt(tab.Rows[i]["Qty2"], 0);
            int qty3 = SafeValue.SafeInt(tab.Rows[i]["Qty3"], 0);
            bool isSch = SafeValue.SafeBool(tab.Rows[i]["IsSch"], false);
            DateTime lastSchDate = SafeValue.SafeDate(tab.Rows[i]["LastSchDate"], DateTime.Today);
            int LastSchQty1 = SafeValue.SafeInt(tab.Rows[i]["LastSchQty1"], 0);
            int LastSchQty2 = SafeValue.SafeInt(tab.Rows[i]["LastSchQty2"], 0);
            int LastSchQty3 = SafeValue.SafeInt(tab.Rows[i]["LastSchQty3"], 0);
            string doType = SafeValue.SafeString(tab.Rows[i]["DoType"]).ToUpper();
            string arCode = SafeValue.SafeString(tab.Rows[i]["ArCode"]);
            string des = SafeValue.SafeString(tab.Rows[i]["Description"]);
            //DateTime peroidDate = SafeValue.SafeDate(tab.Rows[i]["PeroidDate"], DateTime.Today);
            //string refNo = SafeValue.SafeString(tab.Rows[i]["DoNo"]);
            //int detId = SafeValue.SafeInt(tab.Rows[i]["detId"], 0);
            //int preQty = SafeValue.SafeInt(tab.Rows[i]["PreQty"], 0);
            //int lastSchQty = SafeValue.SafeInt(tab.Rows[i]["LastSchQty"], 0);
            //string IsSch = SafeValue.SafeString(tab.Rows[i]["IsSch"]);
            decimal price1 = 0;
            decimal price2 = 0;
            decimal price3 = 0;
            int dailyNo = 0;
            bool isFixed = false;
            bool isYearly = false;
            bool isMonthly = false;
            bool isWeekly = false;
            bool isDaily = false;
            DateTime dt = DateTime.Today;
            DataTable tab1=new DataTable();
            if (doType == "IN")
                tab1 = GetContractByNo(contractNo, sku);
            else//out get contractno, by sku,lotno>>dotype=in, doNo>>PO no>> get ContractNo
            {
                string conNo = string.Format(@"select Quotation from wh_do mast 
inner join Wh_DoDet2 det on det.DoNo=mast.DoNo and det.DoType=mast.DoType 
 where det.DoType='in' and det.Product='{0}' and det.LotNo='{1}' and len(det.DoNo)>0", sku, lotNo);

                tab1 = GetContractByNo(SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(conNo)), sku);
            }
            int n = 0;
            int n_sch = 0;
            for (int j = 0; j < tab1.Rows.Count; j++)
            {
                isFixed = SafeValue.SafeBool(tab1.Rows[j]["IsFixed"], true);
                isYearly = SafeValue.SafeBool(tab1.Rows[j]["IsYearly"], true);
                isMonthly = SafeValue.SafeBool(tab1.Rows[j]["IsMonthly"], true);
                isWeekly = SafeValue.SafeBool(tab1.Rows[j]["IsWeekly"], true);
                isDaily = SafeValue.SafeBool(tab1.Rows[j]["IsDaily"], true);
                #region get sch and price
                if (isYearly)
                {
                    TimeSpan span = dt - doDate;
                    if (dt.ToString("MM-dd") == "01-01")
                    {
                        n = SafeValue.SafeInt(dt.Year - doDate.Year, 0);
                        if(isSch)
                            n_sch = SafeValue.SafeInt(dt.Year - lastSchDate.Year, 0);
                        price1 = SafeValue.SafeDecimal(tab1.Rows[j]["Price1"]);
                        price2 = SafeValue.SafeDecimal(tab1.Rows[j]["Price2"]);
                        price3 = SafeValue.SafeDecimal(tab1.Rows[j]["Price3"]);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (isMonthly)
                {
                    if (dt.ToString("dd") == "01")
                    {
                        price1 = SafeValue.SafeDecimal(tab1.Rows[j]["Price1"]);
                        price2 = SafeValue.SafeDecimal(tab1.Rows[j]["Price2"]);
                        price3 = SafeValue.SafeDecimal(tab1.Rows[j]["Price3"]);
                        int year = dt.Year;
                        n = (dt.Year - doDate.Year) * 12 + dt.Month - doDate.Month;
                        if (isSch)
                            n_sch = (dt.Year - lastSchDate.Year) * 12 + dt.Month - lastSchDate.Month; 
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (isWeekly)
                {
                    if (dt.DayOfWeek == DayOfWeek.Monday)
                    {
                        price1 = SafeValue.SafeDecimal(tab1.Rows[j]["Price1"]);
                        price2 = SafeValue.SafeDecimal(tab1.Rows[j]["Price2"]);
                        price3 = SafeValue.SafeDecimal(tab1.Rows[j]["Price3"]);

                        TimeSpan day = GetMondayByDate(dt) - GetMondayByDate(doDate);
                        n = SafeValue.SafeInt(day.TotalDays / 7, 0);
                        if (isSch)
                        {
                            day = GetMondayByDate(dt) - GetMondayByDate(lastSchDate);
                            n_sch = SafeValue.SafeInt(day.TotalDays / 7, 0);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (isDaily)
                {

                    dailyNo = SafeValue.SafeInt(tab1.Rows[j]["DailyNo"], 1);
                    if (dailyNo == 0)
                        dailyNo = 1;

                    TimeSpan day = dt.Date - doDate.Date;
                    if (day.Days % dailyNo == 0)
                    {
                        price1 = SafeValue.SafeDecimal(tab1.Rows[j]["Price1"]);
                        price2 = SafeValue.SafeDecimal(tab1.Rows[j]["Price2"]);
                        price3 = SafeValue.SafeDecimal(tab1.Rows[j]["Price3"]);
                        n = SafeValue.SafeInt(day.TotalDays / dailyNo, 0);
                        if (isSch)
                        {
                            day = dt.Date - lastSchDate.Date;
                            n_sch = SafeValue.SafeInt(day.TotalDays / dailyNo, 0);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
 
                #endregion
               
                XAArInvoice iv = null;
                string mastType = "WH";
                if (lastInvId == 0 || lastPartyId != partyId)//is the frist invoice or is next party then generate new invoice
                {
                    if (lastInvId > 0)//update last invoice ,set linelocamat, and mast.docamt/locamt/balanceamt
                        UpdateMaster(lastInvId);
                    #region generate invoice
                    iv = new XAArInvoice();
                    string counterType = "AR-IV";

                    iv.DocType = "IV";
                    iv.DocDate = dt;
                    lastInvNo = C2Setup.GetNextNo(iv.DocType, counterType, iv.DocDate);
                    iv.DocNo = lastInvNo;
                    iv.PartyTo = partyId;
                    iv.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                    iv.ExRate = 1;
                    iv.AcCode = EzshipHelper.GetAccApCode(iv.PartyTo, iv.CurrencyId);
                    iv.AcSource = "DB";
                    iv.Description = "";
                    iv.Term = "CASH";

                    string[] currentPeriod = EzshipHelper.GetAccPeriod(iv.DocDate);
                    iv.AcYear = SafeValue.SafeInt(currentPeriod[1], iv.DocDate.Year);
                    iv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], iv.DocDate.Month);

                    iv.MastRefNo = "";
                    iv.JobRefNo = "";
                    iv.MastType = mastType;
                    iv.DocAmt = 0;
                    iv.LocAmt = 0;
                    iv.BalanceAmt = 0;
                    iv.CancelDate = new DateTime(1900, 1, 1);
                    iv.CancelInd = "N";
                    iv.DocDueDate = dt;
                    iv.ExportInd = "N";
                    iv.SpecialNote = "";
                    iv.UserId = EzshipHelper.GetUserName();
                    iv.EntryDate = DateTime.Now;

                    C2.Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(iv);
                    C2Setup.SetNextNo(iv.DocType, counterType, lastInvNo, iv.DocDate);
                    lastInvId = iv.SequenceId;
                    #endregion
                }
                if (doType == "OUT") // form modify the qty
                {
                    qty1 = -qty1;
                    qty2 = -qty2;
                    qty3 = -qty3;
                    LastSchQty1 = 0;
                    LastSchQty2 = 0;
                    LastSchQty3 = 0;
                }
                lastPartyId = partyId;
                #region create det
                if (isFixed)// only do one time
                {
                    int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select count(*) from XAArInvoiceDet where DocId='{0}' and ChgDes1 like 'SKU {1}%,'", lastInvId, sku)), 0);
                    if (cnt == 0)//sku balQty>0
                    {
                        int count = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("select sum(case when DoType='in' then Qty1 else -qty1 end) as cnt from wh_dodet2 where Product='{0}'", sku)), 0);
                        InsertInv_det(lastInvId, lastInvNo, mastType, 1, price1 + price2 + price3, sku,arCode,des,lotNo);
                    }
                }
                else
                {
                    if (qty1 * n + LastSchQty1 * n_sch != 0)
                    {
                        InsertInv_det(lastInvId, lastInvNo, mastType, qty1 * n + LastSchQty1 * n_sch, price1, sku,arCode,des,lotNo);
                    }
                    if (qty2 * n + LastSchQty2 * n_sch != 0)
                    {
                        InsertInv_det(lastInvId, lastInvNo, mastType, qty2 * n + LastSchQty2 * n_sch, price2, sku,arCode,des,lotNo);
                    }
                    if (qty3 * n + LastSchQty3 * n_sch != 0)
                    {
                        InsertInv_det(lastInvId, lastInvNo, mastType, qty3 * n + LastSchQty3 * n_sch, price3, sku,arCode,des,lotNo);
                    }
                }
                #endregion
                UpdateDoDet(sku, lotNo, doType);
            }
        }
    }
    private void InsertInv_det(int invId,string invNo,string mastType,int qty,decimal price,string sku,string arCode,string des,string lotNo)
    {
        XAArInvoiceDet det = new XAArInvoiceDet();
        det.ChgCode = sku;
        det.ChgDes1 = des;
        det.AcCode = arCode;
        det.Qty = qty;
        det.Price = price;
        det.DocId = invId;
        det.DocNo = invNo;
        det.DocLineNo = SafeValue.SafeInt(ConnectSql.ExecuteScalar("select count(*) from XAArInvoiceDet where DocId='" + invId + "'"), 0)+1;

        det.DocType = "IV";
        det.MastRefNo = "";
        det.JobRefNo = lotNo;
        det.MastType = mastType;
        det.AcSource = "CR";
        det.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        det.ExRate = 1;
        det.Gst = 0;
        det.GstType = "E";
        det.GstAmt = 0;
        det.DocAmt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
        det.LocAmt = det.DocAmt;

        C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(det);
    }
    private DateTime GetMondayByDate(DateTime d)
    {
        if (d.DayOfWeek == DayOfWeek.Monday)
            return d;
        else if (d.DayOfWeek == DayOfWeek.Tuesday)
            return d.AddDays(-1);

        else if (d.DayOfWeek == DayOfWeek.Wednesday)
            return d.AddDays(-2);

        else if (d.DayOfWeek == DayOfWeek.Thursday)
            return d.AddDays(-3);

        else if (d.DayOfWeek == DayOfWeek.Friday)
            return d.AddDays(-4);

        else if (d.DayOfWeek == DayOfWeek.Saturday)
            return d.AddDays(-5);

        else if (d.DayOfWeek == DayOfWeek.Sunday)
            return d.AddDays(-6);
        return d;
    }
    private DataTable GetContractByNo(string no, string sku)
    {
        string sql = string.Format(@"select top(1) * from wh_ContractDet where ContractNo='{0}' and ProductCode='{1}' order by Id desc", no, sku);
        return ConnectSql.GetTab(sql);
    }
    private void UpdateMaster(int docId)
    {
        string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        sql = string.Format("select sum(LocAmt) as DocAmt,sum(lineLocAmt) as LocAmt from XaArInvoiceDet where DocId='{0}'",docId);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count == 1)
        {
            sql = string.Format("Update XAArInvoice set DocAmt='{0}',BalanceAmt='{0}',LocAmt='{1}' where SequenceId={2}", SafeValue.SafeDecimal(tab.Rows[0]["DocAmt"]),SafeValue.SafeDecimal(tab.Rows[0]["LocAmt"]), docId);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
    }

    private void UpdateDoDet(string sku, string lotNo,string doType)
    {
        string sql = "";
        if (doType == "IN")
        {
            sql = string.Format(@"update wh_dodet2 set IsSch='true',PreQty1=Qty1,PreQty2=Qty2,PreQty3=Qty3
,LastSchQty1=qty1-(select Isnull(sum(qty1),0) from wh_doDet2 where DoType='Out' and Product='{0}' and LotNo='{1}' and len(DoNo)>0)
,LastSchQty2=qty2-(select Isnull(sum(qty2),0) from wh_doDet2 where DoType='Out' and Product='{0}' and LotNo='{1}' and len(DoNo)>0)
,LastSchQty3=qty3-(select Isnull(sum(qty3),0) from wh_doDet2 where DoType='Out' and Product='{0}' and LotNo='{1}' and len(DoNo)>0)
,LastSchDate=getdate() where Product='{0}' and LotNo='{1}' and DoType='In' and len(isnull(DoNo,''))>0", sku, lotNo);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
        else
        {
            sql = string.Format(@"update wh_dodet2 set IsSch='true',PreQty1=Qty1,PreQty2=Qty2,PreQty3=Qty3
,LastSchQty1=0
,LastSchQty2=0
,LastSchQty3=0
,LastSchDate=getdate() where Product='{0}' and LotNo='{1}' and DoType='Out' ", sku, lotNo);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
      
    }

}