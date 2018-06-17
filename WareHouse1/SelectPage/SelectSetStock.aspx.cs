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

public partial class WareHouse_SelectPage_SelectSetStock : System.Web.UI.Page
{
    protected void Page_Init()
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string salesman = SafeValue.SafeString(Request.QueryString["Salesman"].ToString());
            string schId = SafeValue.SafeString(Request.QueryString["SchId"].ToString());
            lbl_Salesmen.Text = salesman;
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

       // string code = SafeValue.SafeString(Request.QueryString["Loc"].ToString());
        string userId = HttpContext.Current.User.Identity.Name;
        string sql = string.Format(@"select * from (select 0 as Id, tab_hand.Product,0 as Qty,tab_hand.LotNo,p.Description as Des1,tab_hand.DoNo,tab_hand.DoDate,p.Att4 as Att1,p.Att5 as Att2,tab_hand.Qty1-isnull(tab_out.Qty1,0) as HandQty,tab_hand.Location,isnull(tab_Price.Price,0) as Price,'{0}' as Salesman 
from (select product ,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3,Location,max(mast.DoNo) DoNo,max(mast.DoDate) DoDate from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product,LotNo,Location) as tab_hand 
left join (select top(1) price,ProductCode from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='SO' and mast.DoStatus='Closed' order by mast.DoDate,det.Price Desc) as tab_Price on tab_Price.ProductCode=tab_hand.Product
left join (select Product,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3,Location from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='OUT' and len(det.DoNo)>0 group by Product,LotNo,Location) 
as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo and tab_out.Location=tab_hand.Location
left join ref_product p on p.Code=tab_hand.Product) as tab where HandQty>0 ", userId);
        string location = SafeValue.SafeString(cmb_Location.Text.Trim());
        if (location.Length > 0)
        {
            sql += string.Format(@" and Location='{0}'", location);
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        string schId = SafeValue.SafeString(Request.QueryString["SchId"].ToString());
        //string code = SafeValue.SafeString(Request.QueryString["Loc"].ToString());
        string salesman = SafeValue.SafeString(Request.QueryString["Salesman"].ToString());
        string location = SafeValue.SafeString(cmb_Location.Text.Trim());
        string userId = HttpContext.Current.User.Identity.Name;
        if (s == "Save")
        {
            bool result = false;
            string soNo = "";
            string issueN = "";
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].qty==0){
                    e.Result = "Fail,Please keyin Qty ";
                    return;
                }
            }
            C2.XAArInvoice inv=null;
            C2.XAArInvoice inv_Do=null;
            if (list.Count > 0)
            {

                //Create SO
                #region Create So
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhSchedule), "Id='" + schId + "'");
                WhSchedule sch = C2.Manager.ORManager.GetObject(query) as WhSchedule;
                bool isNew = false;
                Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "DoNo='" + 0 + "'");
                WhTrans so = C2.Manager.ORManager.GetObject(query1) as WhTrans;
                if (so == null)
                {
                    isNew = true;
                    so = new WhTrans();
                    soNo = C2Setup.GetNextNo("", "SaleOrders", DateTime.Now);
                }


                so.DoNo = soNo;
                so.DoDate = DateTime.Now;
                so.PartyId = sch.PartyId;
                so.PartyName = sch.PartyName;
                so.Pic = sch.PartyContact;
                so.PartyAdd = sch.PartyAdd;
                so.AgentId = sch.DoctorId;
                so.AgentName = EzshipHelper.GetPartyName(sch.DoctorId);
                so.NotifyId = sch.Patient;
                so.NotifyName = GetPatientName(sch.Patient);
                so.Currency = "SGD";
                so.DoType = "SO";
                so.DoStatus = "Draft";
                so.ExRate = 1;
                so.SalesId = salesman;
                so.CreateBy = userId;
                so.CreateDateTime = DateTime.Now;
                so.UpdateBy = userId;
                so.UpdateDateTime = DateTime.Now;
                if (isNew)
                {
                    Manager.ORManager.StartTracking(so, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(so);
                    C2Setup.SetNextNo("", "SaleOrders", soNo, DateTime.Now);
                }
                else
                {
                    Manager.ORManager.StartTracking(so, Wilson.ORMapper.InitialState.Updated);
                    Manager.ORManager.PersistChanges(so);
                }
                sch.DoNo = soNo;
                string update = string.Format(@"update Wh_Schedule set DoNo='{0}',StatusCode='Finished' where Id='{1}'", soNo, schId);
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
                    invN = C2Setup.GetNextNo("", counterType,so.DoDate);
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
                    if (so.PayTerm!=null)
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
                    inv.AcSource ="DB";
                    inv.SpecialNote = "";

                    inv.MastType = "WH";
                    inv.MastRefNo =so.DoNo;
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
                whDo.WareHouseId = location;
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
            for (int i = 0; i < list.Count; i++)
            {
                string id = list[i].id;
                string product = list[i].product;
                string lotNo = list[i].lotNo;
                int qty = list[i].qty;
                decimal price = list[i].price;
                //string loction = list[i].location;
                string des1 = list[i].des1;
                int hanQty = list[i].hanQty;
                if(qty>hanQty){
                    e.Result = "Fail,Please keyin the correct qty ";
                    return;
                }
                //string salesman = list[i].salesman;
                string sql = @"Insert Into Wh_TransDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,LocationCode,DocAmt)";
                sql += string.Format(@"select '{0}'as DoNo, 'SO' as DoType,'{1}' as ProductCode
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,'{3}' as Price
,'{4}' as LotNo
,ref.UomPacking as Uom1,ref.UomWhole as Uom2,ref.UomLoose as Uom3,ref.UomBase as Uom4
,ref.QtyPackingWhole as QtyPackWhole,ref.QtyWholeLoose as  QtyWholeLoose,ref.QtyLooseBase as QtyLooseBase
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
,ref.Att4 as Att1,ref.Att5 as Att2,ref.att6 as Att3,ref.att7 as Att4,ref.att8 as Att5,ref.att9 as Att6,ref.Description as Des1,'{6}' as LocationCode,{2}*{3} as DocAmt
from (select '{1}' as Sku) as tab inner join ref_product ref on ref.Code=tab.Sku", soNo, product, qty, price, lotNo, EzshipHelper.GetUserName(), location);
                C2.Manager.ORManager.ExecuteCommand(sql);

                sql = @"Insert Into wh_DoDet(JobStatus,DoNo, DoType,ProductCode,ExpiredDate,Price,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
                sql += string.Format(@" select * from (select 'Picked' as JobStatus,'{1}'as DoNo, 'Out' as DoType,ProductCode,ExpiredDate,Price
,Qty1 as Qty1
,0 as Qty2
,0 as Qty3
,Qty1 as Qty4
,0 as Qty5
, LotNo,BatchNo,CustomsLot,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,Packing,'{2}' as CreateBy,getdate() as CreateDateTime,'{2}' as UpdateBy,getdate() as UpdateDateTime,Id as DoInId from Wh_TransDet where DoNo='{0}' and ProductCode='{4}' and LotNo='{5}' and DoType='SO' and LocationCode='{3}'
 ) as tab_aa where qty4>0  ", soNo, issueN, userId, location,product,lotNo);

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
                InsertInv_Det(inv.SequenceId,inv.DocNo,i+1,product,lotNo,des1,qty,price,inv.CurrencyId,inv.ExRate,0,inv.MastRefNo,"",inv.MastType,"IV");
                UpdateMaster(inv.SequenceId);

                InsertInv_Det(inv_Do.SequenceId, inv_Do.DocNo, i + 1, product, lotNo, des1, qty, price, inv_Do.CurrencyId, inv_Do.ExRate, 0, inv_Do.MastRefNo, "", inv_Do.MastType, "IV");
                UpdateMaster(inv_Do.SequenceId);
                if(soNo.Length>0){
                    result = true;
                }
            }
            #endregion
            if (result)
            {
                e.Result = soNo;
            }
            else
            {
                e.Result = "Fail,Please keyin select product ";
            }

        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string id = "";
        public string product = "";
        public string lotNo="";
        public int qty=0;
        public decimal price = 0;
        public string location="";
        public string des1="";
        public string salesman = "";
        public int hanQty = 0;
        public Record(string _id, string _product,string _lotNo,int _qty,decimal _price,string _location,string _des1,string _salesmen,int _hanQty)
        {
            id = _id;
            product = _product;
            lotNo = _lotNo;
            qty = _qty;
            price = _price;
            location = _location;
            des1 = _des1;
            salesman = _salesmen;
            hanQty = _hanQty;
        }

    }
    private void OnLoad()
    {
        string salesman = SafeValue.SafeString(Request.QueryString["Salesman"].ToString());
        int start = 0;
        int end = 5000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxLabel product = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Product"], "lbl_ProductCode") as ASPxLabel;
            ASPxLabel lotNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["LotNo"], "lbl_LotNo") as ASPxLabel;
            ASPxSpinEdit qty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit price = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            //ASPxComboBox location = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Location"], "cmb_Location") as ASPxComboBox;
            ASPxLabel des1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Des1"], "lbl_Description") as ASPxLabel;
            ASPxComboBox cmb_SalesId = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Salesman"], "cmb_SalesId") as ASPxComboBox;
            ASPxSpinEdit hanQty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["HanQty"], "spin_HandQty") as ASPxSpinEdit;
            if (id != null && isPay != null && isPay.Checked && product != null)
            {
                list.Add(new Record("", product.Text, lotNo.Text, SafeValue.SafeInt(qty.Text, 0), SafeValue.SafeDecimal(price.Text, 0), "", des1.Text,"",SafeValue.SafeInt(hanQty,0)));
            }
            else if (id == null)
                break; ;
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
}