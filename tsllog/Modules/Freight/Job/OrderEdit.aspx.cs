using Aspose.Cells;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Freight_Job_OrderEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Session["CargoOrderDJ_" + txt_No.Text] = null;
            string no = Request.QueryString["no"] ?? "0";

            if (no != "0" && no != "New")
            {
                Session["CargoOrderDJ_" + txt_No.Text] = " Id=" + no + "";
                this.txt_No.Text = Request.QueryString["no"];
                this.dsJobDet.FilterExpression = "Id="+no;
                if (this.grd_Det.GetRow(0) != null)
                    this.grd_Det.StartEdit(0);
            }
            else if (Request.QueryString["no"] != null)
			{
                if (Session["CargoOrderDJ_" + txt_No.Text] == null)
				{
                    this.grd_Det.AddNewRow();
				}
			}
            else
                this.dsJobDet.FilterExpression = "1=0";
        }
        if (Session["CargoOrderDJ_" + txt_No.Text] != null)
        {

            this.dsJobDet.FilterExpression = Session["CargoOrderDJ_" + txt_No.Text].ToString();
            if (this.grd_Det.GetRow(0) != null)
                this.grd_Det.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region job detail
    protected void grd_Det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobHouse));
        }
    }
    protected void grd_Det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        DateTime dt = DateTime.Now;
        string role = EzshipHelper.GetUseRole();
        if (role.ToUpper() == "DONGJI"||role=="DONGJISTAFF")
        {
            e.NewValues["BookingNo"] = "";
        }
        else {
            string refN = C2Setup.GetNextNo("", "BkgRefNo", dt.Date);
            e.NewValues["BookingNo"] = refN;
        }
        e.NewValues["Carrier"] = "EAST INTERNATIONAL";
        e.NewValues["CargoStatus"] = "USE";
        e.NewValues["Responsible"] = "PERSON";
        e.NewValues["JobNo"] = "0";
        e.NewValues["ExJobOrder"] = "0";
        e.NewValues["JobType"] = "I";
        e.NewValues["Fee1CurrId"] = "SGD";
        e.NewValues["LineN"] = 0;
        e.NewValues["M3"] = 0;
        e.NewValues["Pod"] = "SGSIN";
        e.NewValues["Weight"] = 0;
        e.NewValues["TotQty"] = 0;
        e.NewValues["PackType"] = " ";
        e.NewValues["Marking1"] = " ";
        e.NewValues["Marking2"] = " ";

        e.NewValues["Remark1"] = " ";
        e.NewValues["Remark2"] = " ";

        e.NewValues["Desc1"] = " ";
        e.NewValues["Desc2"] = " ";

        e.NewValues["IsOverLength"] = false;
        e.NewValues["IsHold"] = false;
        e.NewValues["IsBill"] = false;
        e.NewValues["IsNormal"] = true;
        e.NewValues["IsDg"] = false;
        e.NewValues["DgClass"] = " ";


    }
    protected void grd_Det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        e.NewValues["JobOrder"] = "0";
        e.NewValues["ExJobOrder"] = "0";
        e.NewValues["JobType"] = "O";
        e.NewValues["UserId"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["EntryDate"] = DateTime.Now;

        e.NewValues["QtyOrig"] = SafeValue.SafeInt(e.NewValues["QtyOrig"], 0);
        e.NewValues["WeightOrig"] = e.NewValues["WeightOrig"];
        e.NewValues["M3Orig"] = e.NewValues["M3Orig"];

        e.NewValues["CargoStatus"] = e.NewValues["CargoStatus"];
        e.NewValues["ClientId"] = e.NewValues["ClientId"];
        e.NewValues["ClientContact"] = e.NewValues["ClientContact"];
        e.NewValues["ClientEmail"] = e.NewValues["ClientEmail"];
        e.NewValues["ShipperInfo"] = e.NewValues["ShipperInfo"];
        e.NewValues["ShipperRemark"] = e.NewValues["ShipperRemark"];
        e.NewValues["ShipperEmail"] = e.NewValues["ShipperEmail"];
        e.NewValues["Desc1"] = e.NewValues["Desc1"];
        DateTime dt = DateTime.Now;
       
        e.Cancel = false;
    }
    protected void grd_Det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["JobOrder"] = "0";
        e.NewValues["JobType"] = "O";
        e.NewValues["QtyOrig"] = SafeValue.SafeInt(e.NewValues["QtyOrig"], 0);
        e.NewValues["WeightOrig"] = e.NewValues["WeightOrig"];
        e.NewValues["M3Orig"] = e.NewValues["M3Orig"];

        e.NewValues["CargoStatus"] = e.NewValues["CargoStatus"];
        e.NewValues["ClientId"] = e.NewValues["ClientId"];
        e.NewValues["ClientContact"] = e.NewValues["ClientContact"];
        e.NewValues["ClientEmail"] = e.NewValues["ClientEmail"];
        e.NewValues["ShipperInfo"] = e.NewValues["ShipperInfo"];
        e.NewValues["ShipperRemark"] = e.NewValues["ShipperRemark"];
        e.NewValues["ShipperEmail"] = e.NewValues["ShipperEmail"];
        e.NewValues["Desc1"] = e.NewValues["Desc1"];

        e.Cancel = false;
    }
    private string save()
    {
        try
        {

            ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
            string oId = txt_cargo_id.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + oId + "'");
            C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
            bool isNew = false;
            if (jo == null)
            {

                jo = new C2.JobHouse();
                isNew = true;
                jo.JobType = "I";
                jo.UserId = HttpContext.Current.User.Identity.Name;
                jo.EntryDate = DateTime.Now;


            }
            ASPxPageControl pageControl = this.grd_Det.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_OrderNo = pageControl.FindControl("txt_OrderNo") as ASPxTextBox;
            if (txt_OrderNo != null)
                jo.JobNo = txt_OrderNo.Text;
            ASPxTextBox txt_ExpBkgN = grd_Det.FindEditFormTemplateControl("txt_ExpBkgN") as ASPxTextBox;
            if (txt_ExpBkgN != null)
                jo.BookingNo = txt_ExpBkgN.Text;
            ASPxTextBox txt_JobOrder = grd_Det.FindEditFormTemplateControl("txt_JobOrder") as ASPxTextBox;
            ASPxTextBox txt_ExJobOrder = grd_Det.FindEditFormTemplateControl("txt_ExJobOrder") as ASPxTextBox;
            if (txt_ExJobOrder != null)
                jo.HblNo = txt_ExJobOrder.Text;
            ASPxTextBox txt_ContNo = grd_Det.FindEditFormTemplateControl("txt_ContNo") as ASPxTextBox;
            if (txt_ContNo != null)
                jo.ContNo = txt_ContNo.Text;


            ASPxTextBox txt_Carrier = grd_Det.FindEditFormTemplateControl("txt_Carrier") as ASPxTextBox;
            if (txt_Carrier != null)
                jo.ShipperInfo = txt_Carrier.Text;
            ASPxDropDownEdit DropDownEdit_Party = grd_Det.FindEditFormTemplateControl("DropDownEdit_Party") as ASPxDropDownEdit;
            ASPxTextBox txt_Party = grd_Det.FindEditFormTemplateControl("txt_Party") as ASPxTextBox;
            if (DropDownEdit_Party != null)
            {
                jo.ConsigneeInfo = SafeValue.SafeString(DropDownEdit_Party.Text);
                //if (SafeValue.SafeString(DropDownEdit_Party.Text) == "")
                //{
                //    throw new Exception("请输入货主信息！");
                //}
            }
            if (txt_Party != null)
            {
                if (txt_Party.Text != "")
                {
                    VilaParty(jo.ConsigneeInfo, jo.ConsigneeRemark, jo.ConsigneeEmail, jo.Email1, jo.Email2, jo.Tel1, jo.Tel2, jo.Mobile1, jo.Mobile2, jo.Desc1);
                    jo.ConsigneeInfo = txt_Party.Text;
                }
                else
                {
                    throw new Exception("请输入货主信息！");
                }
            }


            ASPxComboBox cmb_Responsible = grd_Det.FindEditFormTemplateControl("cmb_Responsible") as ASPxComboBox;
            if (cmb_Responsible != null)
            {
                jo.Responsible = SafeValue.SafeString(cmb_Responsible.Value);
                if (SafeValue.SafeString(cmb_Responsible.Value) == "")
                {
                    throw new Exception("请选择个人还是公司！");
                }
            }
            ASPxTextBox txt_ConsigneeRemark = grd_Det.FindEditFormTemplateControl("txt_ConsigneeRemark") as ASPxTextBox;
            if (txt_ConsigneeRemark != null)
            {
                jo.ConsigneeRemark = txt_ConsigneeRemark.Text;
                if (SafeValue.SafeString(cmb_Responsible.Value) == "PERSON" && txt_ConsigneeRemark.Text == "")
                {
                    throw new Exception("请输入身份证号码/IC");
                }
            }
            ASPxTextBox txt_ConsigneeEmail = grd_Det.FindEditFormTemplateControl("txt_ConsigneeEmail") as ASPxTextBox;
            if (txt_ConsigneeEmail != null)
                jo.ConsigneeEmail = txt_ConsigneeEmail.Text;
            ASPxTextBox txt_Email1 = grd_Det.FindEditFormTemplateControl("txt_Email1") as ASPxTextBox;
            if (txt_Email1 != null)
                jo.Email1 = txt_Email1.Text;
            ASPxTextBox txt_Email2 = grd_Det.FindEditFormTemplateControl("txt_Email2") as ASPxTextBox;
            if (txt_Email2 != null)
                jo.Email2 = txt_Email2.Text;
            ASPxTextBox txt_Tel1 = grd_Det.FindEditFormTemplateControl("txt_Tel1") as ASPxTextBox;
            if (txt_Tel1 != null)
                jo.Tel1 = txt_Tel1.Text;
            ASPxTextBox txt_Tel2 = grd_Det.FindEditFormTemplateControl("txt_Tel2") as ASPxTextBox;
            if (txt_Tel2 != null)
                jo.Tel2 = txt_Tel2.Text;
            ASPxTextBox txt_Mobile1 = grd_Det.FindEditFormTemplateControl("txt_Mobile1") as ASPxTextBox;
            if (txt_Mobile1 != null)
                jo.Mobile1 = txt_Mobile1.Text;
            ASPxTextBox txt_Mobile2 = grd_Det.FindEditFormTemplateControl("txt_Mobile2") as ASPxTextBox;
            if (txt_Mobile2 != null)
                jo.Mobile2 = txt_Mobile2.Text;
            ASPxMemo memo_Desc1 = grd_Det.FindEditFormTemplateControl("memo_Desc1") as ASPxMemo;
            if (memo_Desc1 != null)
            {
                jo.Desc1 = memo_Desc1.Text;

            }
            if (txt_Tel1.Text.Trim() == "" && txt_Tel2.Text.Trim() == "" && txt_Mobile1.Text.Trim() == "" && txt_Mobile2.Text.Trim() == "")
            {
                throw new Exception("请输入一个联系方式！");
            }
            ASPxTextBox txt_ClientId = grd_Det.FindEditFormTemplateControl("txt_ClientId") as ASPxTextBox;
            ASPxTextBox txt_ClientEmail = grd_Det.FindEditFormTemplateControl("txt_ClientEmail") as ASPxTextBox;
            ASPxMemo memo_Remark1 = grd_Det.FindEditFormTemplateControl("memo_Remark1") as ASPxMemo;
            ASPxCheckBox ckb_IsHold = grd_Det.FindEditFormTemplateControl("ckb_IsHold") as ASPxCheckBox;
            #region
            if (ckb_IsHold != null)
            {
                if (ckb_IsHold.Checked)
                {
                    jo.IsHold = SafeValue.SafeBool(ckb_IsHold.Checked, false);
                    if (txt_ClientId != null)
                        jo.ClientId = jo.ConsigneeInfo;

                    if (txt_ClientEmail != null)
                        jo.ClientEmail = jo.Tel1 + " " + jo.Tel2 + " " + jo.Mobile1 + " " + jo.Mobile2;

                    if (memo_Remark1 != null)
                        jo.Remark1 = jo.Desc1;
                }
                else
                {
                    if (txt_ClientId != null)
                        jo.ClientId = txt_ClientId.Text;

                    if (txt_ClientEmail != null)
                        jo.ClientEmail = txt_ClientEmail.Text;

                    if (memo_Remark1 != null)
                        jo.Remark1 = memo_Remark1.Text;
                }
            }
            #endregion
            ASPxCheckBox ckb_Prepaid_Ind = grd_Det.FindEditFormTemplateControl("ckb_Prepaid_Ind") as ASPxCheckBox;
            if (ckb_Prepaid_Ind != null)
            {
                if (ckb_Prepaid_Ind.Checked)
                {
                    jo.PrepaidInd = "YES";
                }
                else
                {
                    jo.PrepaidInd = "NO";
                }
            }
            ASPxSpinEdit spin_Collect_Amount1 = grd_Det.FindEditFormTemplateControl("spin_Collect_Amount1") as ASPxSpinEdit;
            if (spin_Collect_Amount1 != null)
                jo.CollectAmount1 = SafeValue.SafeDecimal(spin_Collect_Amount1.Value);
            ASPxSpinEdit spin_Collect_Amount2 = grd_Det.FindEditFormTemplateControl("spin_Collect_Amount2") as ASPxSpinEdit;
            if (spin_Collect_Amount2 != null)
                jo.CollectAmount2 = SafeValue.SafeDecimal(spin_Collect_Amount2.Value);
            ASPxComboBox cmb_Duty_Payment = grd_Det.FindEditFormTemplateControl("cmb_Duty_Payment") as ASPxComboBox;
            if (cmb_Duty_Payment != null)
                jo.DutyPayment = SafeValue.SafeString(cmb_Duty_Payment.Value);
            ASPxComboBox cmb_Incoterm = grd_Det.FindEditFormTemplateControl("cmb_Incoterm") as ASPxComboBox;
            if (cmb_Incoterm != null)
                jo.Incoterm = cmb_Incoterm.Text;
            ASPxMemo memo_Remark2 = grd_Det.FindEditFormTemplateControl("memo_Remark2") as ASPxMemo;
            if (memo_Remark2 != null)
                jo.Remark2 = memo_Remark2.Text;
            ASPxSpinEdit spin_MiscFee = grd_Det.FindEditFormTemplateControl("spin_MiscFee") as ASPxSpinEdit;
            if (spin_MiscFee != null)
                jo.Ocean_Freight = SafeValue.SafeDecimal(spin_MiscFee.Value);

            jo.UserId = HttpContext.Current.User.Identity.Name;


            ASPxSpinEdit spin_OtherFee1 = grd_Det.FindEditFormTemplateControl("spin_OtherFee1") as ASPxSpinEdit;
            if (spin_OtherFee1 != null)
                jo.GstFee = SafeValue.SafeDecimal(spin_OtherFee1.Value);
            ASPxSpinEdit spin_OtherFee2 = grd_Det.FindEditFormTemplateControl("spin_OtherFee2") as ASPxSpinEdit;
            if (spin_OtherFee2 != null)
                jo.PermitFee = SafeValue.SafeDecimal(spin_OtherFee2.Value);
            ASPxSpinEdit spin_OtherFee3 = grd_Det.FindEditFormTemplateControl("spin_OtherFee3") as ASPxSpinEdit;
            if (spin_OtherFee3 != null)
                jo.HandlingFee = SafeValue.SafeDecimal(spin_OtherFee3.Value);
            ASPxSpinEdit spin_OtherFee4 = grd_Det.FindEditFormTemplateControl("spin_OtherFee4") as ASPxSpinEdit;
            if (spin_OtherFee4 != null)
                jo.OtherFee = SafeValue.SafeDecimal(spin_OtherFee4.Value);
            ASPxComboBox cmb_IsBill = grd_Det.FindEditFormTemplateControl("cmb_IsBill") as ASPxComboBox;
            if (cmb_IsBill != null)
            {
                if (cmb_IsBill.SelectedItem.Value != null)
                {
                    if (SafeValue.SafeString(cmb_IsBill.SelectedItem.Value) == "YES")
                    {
                        jo.IsBill = true;
                    }
                    else
                    {
                        jo.IsBill = false;
                    }
                }

            }
            ASPxComboBox cmb_CheckIn = grd_Det.FindEditFormTemplateControl("cmb_CheckIn") as ASPxComboBox;
            if (cmb_CheckIn != null)
            {
                if (SafeValue.SafeString(cmb_CheckIn.SelectedItem.Value) == "YES")
                {
                    jo.IsLong = true;
                }
                else
                {
                    jo.IsLong = false;
                }
            }
            jo.UserId = HttpContext.Current.User.Identity.Name;

            if (isNew)
            {
                jo.CargoStatus = "ORDER";
                C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(jo);

            }
            else
            {
                C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(jo);

            }
            UpdateParty(jo.ConsigneeInfo, jo.ConsigneeRemark, jo.ConsigneeEmail, jo.Email1, jo.Email2, jo.Tel1, jo.Tel2, jo.Mobile1, jo.Mobile2, jo.Desc1);
            this.dsJobDet.FilterExpression = "Id='" + jo.Id + "'";
            if (grd_Det.GetRow(0) != null)
                grd_Det.StartEdit(0);

            return jo.Id.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private string update()
    {
        try
        {
            ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
            string oId = txt_cargo_id.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + oId + "'");
            C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;

            ASPxSpinEdit spin_TotQty = grd_Det.FindEditFormTemplateControl("spin_TotQty") as ASPxSpinEdit;
            jo.Qty = SafeValue.SafeInt(spin_TotQty.Value, 0);
            ASPxComboBox txt_PackType = grd_Det.FindEditFormTemplateControl("txt_PackType") as ASPxComboBox;
            jo.PackTypeOrig = SafeValue.SafeString(txt_PackType.Text, "");
            ASPxSpinEdit spin_Weight = grd_Det.FindEditFormTemplateControl("spin_Weight") as ASPxSpinEdit;
            jo.Weight = SafeValue.SafeDecimal(spin_Weight.Value);
            ASPxSpinEdit spin_M3 = grd_Det.FindEditFormTemplateControl("spin_M3") as ASPxSpinEdit;
            jo.Volume = SafeValue.SafeDecimal(spin_M3.Value);
            ASPxComboBox cmb_Fee1CurrId = grd_Det.FindEditFormTemplateControl("cmb_Fee1CurrId") as ASPxComboBox;
            jo.Currency = SafeValue.SafeString(cmb_Fee1CurrId.Value);

            C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(jo);

            Session["CargoOrderDJ"] = "Id=" + jo.Id + "";
            this.dsJobDet.FilterExpression = Session["CargoOrderDJ"].ToString();

            if (this.grd_Det.GetRow(0) != null)
                this.grd_Det.StartEdit(0);
            return jo.Id.ToString();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + ex.StackTrace); }
    }
    private string updateStatus()
    {
        try
        {
            ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
            string oId = txt_cargo_id.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + oId + "'");
            C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
            if (jo.CargoStatus == "ORDER")
            {
                jo.CargoStatus = "IN";
            }
            else if (jo.CargoStatus == "IN")
            {
                jo.CargoStatus = "PICKED";
            }
            else if (jo.CargoStatus == "PICKED")
            {
                jo.CargoStatus = "OUT";
            }
            else if (jo.CargoStatus == "OUT")
            {
                jo.CargoStatus = "SHIPMENT";
            }
            else if (jo.CargoStatus == "SHIPMENT")
            {
                jo.CargoStatus = "DEPARTURE";
            }
            C2.Manager.ORManager.StartTracking(jo, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(jo);
            L.Audit(jo.CargoStatus, "DJ", jo.Id.ToString(), "", "", "");
            Session["CargoOrderDJ"] = "Id=" + jo.Id + "";
            this.dsJobDet.FilterExpression = Session["CargoOrderDJ"].ToString();

            if (this.grd_Det.GetRow(0) != null)
                this.grd_Det.StartEdit(0);
            return jo.Id.ToString();
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + ex.StackTrace); }
    }
    private void insert_cargo(int id ) {

        string sql = string.Format(@"insert into job_stock(OrderId,SortIndex,Marks1,Marks2,Qty2,Price1) values(@OrderId,100,@Marks1,@Marks2,1,380) ");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@OrderId", id, SqlDbType.Int, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Marks1", "OCEAN FREIGHT", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Marks2", "海运费", SqlDbType.NVarChar, 100));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql,list);
        if(res.status){
           
        }
    }
    protected void grd_Det_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxComboBox cmb_Responsible = grd_Det.FindEditFormTemplateControl("cmb_Responsible") as ASPxComboBox;
        ASPxTextBox txt_Party = grd_Det.FindEditFormTemplateControl("txt_Party") as ASPxTextBox;
        ASPxTextBox txt_ConsigneeRemark = grd_Det.FindEditFormTemplateControl("txt_ConsigneeRemark") as ASPxTextBox;
        ASPxTextBox txt_ConsigneeEmail = grd_Det.FindEditFormTemplateControl("txt_ConsigneeEmail") as ASPxTextBox;
        ASPxTextBox txt_Tel1 = grd_Det.FindEditFormTemplateControl("txt_Tel1") as ASPxTextBox;
        ASPxTextBox txt_Tel1_1 = grd_Det.FindEditFormTemplateControl("txt_Tel1_1") as ASPxTextBox;
        if (e.Parameters == "Save")
        {
            e.Result = save();
        }
        if (e.Parameters == "Update")
        {
            e.Result = update();
        }
        if (e.Parameters == "UpdateStatus")
        {
            e.Result = updateStatus();
        }
        if(e.Parameters=="Export"){
            btn_Export_Click(null,null);
        }
        if (e.Parameters == "UpdateParty")
        {
           e.Result = save();
        }
        if(e.Parameters=="Vali"){
            if (SafeValue.SafeString(cmb_Responsible.Value) == "PERSON")
            {
                string sql = string.Format(@"select count(*) from xxparty where  CrNo='{0}' and Name like N'%{1}%' and Tel1='{2}'", txt_ConsigneeRemark.Text, txt_Party.Text, txt_Tel1_1.Text);
                int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    e.Result = "OK";
                }
            }
            if (SafeValue.SafeString(cmb_Responsible.Value) == "COMPANY")
            {
                string sql = string.Format(@"select count(*) from xxparty where CrNo='{0}' and Name like N'%{1}%' and Tel1='{2}'", txt_ConsigneeEmail.Text, txt_Party.Text, txt_Tel1_1.Text);
                int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    e.Result = "OK";
                }
            }

            
        }
        if (e.Parameters == "ValiUen")
        {

            string sql = string.Format(@"select count(*) from xxparty where CrNo='{0}' and Name like '{1}%'", txt_ConsigneeRemark.Text, txt_Party.Text);
            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (n==0)
            {
                e.Result = "OK";
            }
        }
        if (e.Parameters == "Delete")
        {

            ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
            string sql = string.Format(@"delete from job_house where OrderId={0}", txt_cargo_id.Text);
            int n = ConnectSql.ExecuteSql(sql);
            if (n>0)
            {
                e.Result = "OK";
            }
        }
    }
    protected void grd_Det_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grd_Det.EditingRowVisibleIndex > -1)
        {
            string oid = SafeValue.SafeString(this.grd_Det.GetRowValues(this.grd_Det.EditingRowVisibleIndex, new string[] { "Id" }));
            ASPxCheckBox ckb_Prepaid_Ind = grd_Det.FindEditFormTemplateControl("ckb_Prepaid_Ind") as ASPxCheckBox;
            ASPxButton btn_Status = grd_Det.FindEditFormTemplateControl("btn_Status") as ASPxButton;
            ASPxLabel lbl_Status = grd_Det.FindEditFormTemplateControl("lbl_Status") as ASPxLabel;
            string sql = string.Format(@"select CargoStatus from job_house where Id={0}", oid);
            string status = SafeValue.SafeString(Helper.Sql.One(sql));
            #region Status
            if (status == "USE")
            {
                //btn_Status.Visible = false;
                lbl_Status.Text = "待确认";
                btn_Status.Text = "下单";
            }
            else if (status == "ORDER")
            {
                lbl_Status.Text = "已下单";
                btn_Status.Text = "入库";
            }
            else if (status == "IN")
            {
                lbl_Status.Text = "已入库";
                btn_Status.Text = "排库";
            }
            else if (status == "PICKED")
            {
                lbl_Status.Text = "已排库";
                btn_Status.Text = "出库";
            }
            else if (status == "OUT")
            {
                lbl_Status.Text = "已出库";
                btn_Status.Text = "装船";
            }
            else if (status == "SHIPMENT")
            {
                lbl_Status.Text = "已装船";
                btn_Status.Text = "出港";
            }

            string role = EzshipHelper.GetUseRole();
            if(role=="Admin"){
                if (status == "DEPARTURE")
                {
                    lbl_Status.Text = "已出港";
                    btn_Status.Text = "抵港";
                }
                if (status == "ARRIVED")
                {
                    lbl_Status.Text = "已抵港";
                    btn_Status.Text = "入库";
                }
                else if (status == "GR")
                {
                    lbl_Status.Text = "已入库";
                    btn_Status.Text = "出库";
                }
                else if (status == "DO")
                {
                    lbl_Status.Text = "已出库";
                    btn_Status.Text = "派送中";
                }
                else if (status == "SEND")
                {
                    lbl_Status.Text = "派送中";
                    btn_Status.Text = "派送完成";
                }
                else if (status == "COMPLETED")
                {
                    lbl_Status.Text = "派送完成";
                    btn_Status.Visible = false;
                }
            }
            else if (status == "DEPARTURE")
            {
                lbl_Status.Text = "已出港";
                btn_Status.Visible = false;
            }
            #endregion

            #region IsBill
            ASPxComboBox cmb_IsBill = grd_Det.FindEditFormTemplateControl("cmb_IsBill") as ASPxComboBox;
            sql = string.Format(@"select IsBill from job_house where Id={0}", oid);
            bool isBill = SafeValue.SafeBool(Helper.Sql.One(sql),true);
            if (isBill)
            {
                cmb_IsBill.SelectedIndex = 0;
            }
            else {
                cmb_IsBill.SelectedIndex = 1;
            }
            #endregion

            #region Total Amt
            ASPxLabel lbl_stockamt = grd_Det.FindEditFormTemplateControl("lbl_stockamt") as ASPxLabel;
            ASPxLabel lbl_total = grd_Det.FindEditFormTemplateControl("lbl_total") as ASPxLabel;

            sql = string.Format(@"select sum(Price2) from job_stock where OrderId={0}", oid);
            decimal totalAmt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
            lbl_stockamt.Text =SafeValue.SafeString(totalAmt);

            sql = string.Format(@"select Ocean_Freight from job_house where Id={0}", oid);
            decimal miscFee = SafeValue.SafeDecimal(Helper.Sql.One(sql));

            sql = string.Format(@"select sum(Price2) from job_stock where OrderId={0}", oid);
            totalAmt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
            lbl_total.Text = SafeValue.SafeString(totalAmt + miscFee);
            #endregion

        }
    }
    protected bool VilaParty(string code,string uen,string ic,string email1,string email2,string tel1,string tel2,string mobile1,string mobile2,string address) {

        bool action = false;
        ASPxComboBox cmb_Responsible = grd_Det.FindEditFormTemplateControl("cmb_Responsible") as ASPxComboBox;
        string sql = "";
        sql = string.Format(@"select top 1 SequenceId from XXParty order by SequenceId desc");
        int sequenceId = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
         if (cmb_Responsible.Value == "PERSON")
             sql = string.Format(@"select count(*) from XXParty where Name like N'%{0}%' and CrNo='{1}' and Tel1='{2}'", code, ic, tel1);
        else
             sql = string.Format(@"select count(*) from XXParty where Name like N'%{0}%' and CrNo='{1}' and Tel1='{2}'", code, uen, tel1);
        int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);
        if (n > 0)
        {
            action = true;
        }
        else {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = null;
            sequenceId = sequenceId + 1;
            #region list add
            if (code.Length > 6)
            {
                if (cmb_Responsible.Value == "PERSON")
                {
                    cpar = new ConnectSql_mb.cmdParameters("@PartyId", code + "_" + sequenceId, SqlDbType.NVarChar, 100);
                    list.Add(cpar);
                }
                else {
                    cpar = new ConnectSql_mb.cmdParameters("@PartyId", code.Substring(0, 6) + "_" + sequenceId, SqlDbType.NVarChar, 100);
                    list.Add(cpar);
                }
            }
            else {
                cpar = new ConnectSql_mb.cmdParameters("@PartyId", code + "_" + sequenceId, SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }

            cpar = new ConnectSql_mb.cmdParameters("@Code", code, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Name", code, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Status", "USE", SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Address", address, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Fax1", mobile1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Fax2", mobile2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            if (ic.Length > 0)
            {
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", ic, SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }
            if (uen.Length > 0)
            {
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", uen, SqlDbType.NVarChar, 100);
                list.Add(cpar);
            }
            sql = string.Format(@"insert into XXParty(PartyId,Code,Name,IsCustomer,Status,Address,Tel1,Tel2,Fax1,Fax2,Email1,Email2,CrNo) values(@PartyId,@Code,@Name,1,@Status,@Address,@Tel1,@Tel2,@Fax1,@Fax2,@Email1,@Email2,@CrNo)");
            ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
            if (re.status)
            {
                action = true;
            }
            else
            {
            }
            #endregion
        }
        return action;
    }
    protected bool UpdateParty(string name, string uen, string ic, string email1, string email2, string tel1, string tel2, string mobile1, string mobile2, string address)
    {
        ASPxComboBox cmb_Responsible = grd_Det.FindEditFormTemplateControl("cmb_Responsible") as ASPxComboBox;
        ASPxTextBox txt_ConsigneeRemark1 = grd_Det.FindEditFormTemplateControl("txt_ConsigneeRemark1") as ASPxTextBox;
        string old_ic = txt_ConsigneeRemark1.Text;
        ASPxTextBox txt_ConsigneeEmail1 = grd_Det.FindEditFormTemplateControl("txt_ConsigneeEmail1") as ASPxTextBox;
        string old_uen = txt_ConsigneeEmail1.Text;
        ASPxTextBox txt_Tel1_1 = grd_Det.FindEditFormTemplateControl("txt_Tel1_1") as ASPxTextBox;
        string old_tel1 = txt_Tel1_1.Text;
        string sql = string.Format(@"select count(*) from XXParty where Name like N'%{0}%' and (CrNo='{1}' or CrNo='{2}') and Tel1='{3}'", name, old_uen, old_ic,old_tel1);
        int count = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        bool action = false;
        if (count > 0)
        {
            #region  Update Party
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = null;
            string code="";
            if (name.Length > 4)
            {
                if (cmb_Responsible.Value != "PERSON")
                {
                    code = name.Substring(0, 4) + DateTime.Today.Second;
                }
                else {
                    code = name;
                }
            }
            else
            {
                code = name;
            }
            if (ic.Length > 0)
            {
                #region
                cpar = new ConnectSql_mb.cmdParameters("@Code", code, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Name", name, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Address", address, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax1", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax2", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", ic, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@IC", old_ic, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel", old_tel1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Value",name, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                sql = string.Format(@"update XXParty set Code=@Code,Name=@Name,Address=@Address,Tel1=@Tel1,Tel2=@Tel2,Fax1=@Fax1,Fax2=@Fax2,Email1=@Email1,Email2=@Email2,CrNo=@CrNo where Name=@Value and CrNo=@IC and Tel1=@Tel");
                ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (re.status)
                {
                    action = true;
                }
                #endregion
            }
            if (uen.Length > 0)
            {
                #region
                cpar = new ConnectSql_mb.cmdParameters("@Code", code, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Name", name, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Address", address, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel1", tel1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel2", tel2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax1", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Fax2", "", SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email1", email1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Email2", email2, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@CrNo", uen, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@IC", old_uen, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Tel", old_tel1, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                cpar = new ConnectSql_mb.cmdParameters("@Value", name, SqlDbType.NVarChar, 100);
                list.Add(cpar);
                sql = string.Format(@"update XXParty set Code=@Code,Name=@Name,Address=@Address,Tel1=@Tel1,Tel2=@Tel2,Fax1=@Fax1,Fax2=@Fax2,Email1=@Email1,Email2=@Email2,CrNo=@CrNo where Name=@Value and CrNo=@IC and Tel1=@Tel");
                ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);
                if (re.status)
                {
                    action = true;
                }
                #endregion
            }
            #endregion
        }
        return action;
    }
    #endregion
    #region Stock
    protected void grd_Stock_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobStock));
        }
    }
    protected void grd_Stock_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
        e.NewValues["OrderId"] = SafeValue.SafeInt(txt_cargo_id.Text, 0);
        e.NewValues["JobNo"] = "0";
        string sql = string.Format(@"select count(*) from job_house where OrderId={0}", SafeValue.SafeInt(txt_cargo_id.Text, 0));
        int n = SafeValue.SafeInt(Helper.Sql.One(sql),0);
        if (n == 0)
        {
            e.NewValues["SortIndex"] = 1;
        }
        else {
            n++;
            e.NewValues["SortIndex"] = n;
        }
    }
    protected void grd_Stock_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
        this.dsJobStock.FilterExpression = "OrderId=" +SafeValue.SafeInt(txt_cargo_id.Text,0) + "";// 
        
    }
    protected void grd_Stock_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
        e.NewValues["OrderId"] = SafeValue.SafeInt(txt_cargo_id.Text, 0);
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"],0);
        e.NewValues["Marks1"] = SafeValue.SafeString(e.NewValues["Marks1"]);
        e.NewValues["Marks2"] = SafeValue.SafeString(e.NewValues["Marks2"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]));
        ASPxSpinEdit spin_M3 = grd_Det.FindEditFormTemplateControl("spin_M3") as ASPxSpinEdit;
        if (SafeValue.SafeString(e.NewValues["Marks2"])=="海运费")
        {
            e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]) * SafeValue.SafeDecimal(spin_M3.Value));
        }

    }
    protected void grd_Stock_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
        e.NewValues["OrderId"] = SafeValue.SafeInt(txt_cargo_id.Text, 0);
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"], 0);
        e.NewValues["Marks1"] = SafeValue.SafeString(e.NewValues["Marks1"]);
        e.NewValues["Marks2"] = SafeValue.SafeString(e.NewValues["Marks2"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(e.NewValues["Price2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]));
        ASPxSpinEdit spin_M3 = grd_Det.FindEditFormTemplateControl("spin_M3") as ASPxSpinEdit;
        if (SafeValue.SafeString(e.NewValues["Marks2"]) == "海运费")
        {
            e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]) * SafeValue.SafeDecimal(spin_M3.Value));
        }
    }
    protected void grd_Stock_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion
    public void SaveExcel()
    {
        ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
        string oId = txt_cargo_id.Text;
        DateTime now =DateTime.Now;

        string pathTemp = HttpContext.Current.Server.MapPath("~/excel/packinglistsample.xlsx").ToLower();

        string file = string.Format(@"PackingListSample for {0}.xls", oId).ToLower();
        string pathUrl = string.Format("~/files/{0}", file);
        string pathOpen = HttpContext.Current.Server.MapPath(pathUrl).ToLower();
        using (FileStream input = File.OpenRead(pathTemp), output = File.OpenWrite(pathOpen))
        {
            int read = -1;
            byte[] buffer = new byte[4096];
            while (read != 0)
            {
                read = input.Read(buffer, 0, buffer.Length);
                output.Write(buffer, 0, read);
            }
        }


        License lic = new License();
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\bin\License.lic"));
        Workbook workbook = new Workbook();
        workbook.Open(pathOpen);
        Worksheet sheet = null;
        Cells c1 = null;
        int wsCount=workbook.Worksheets.Count;
        string name="";
        for (int i = 0; i < wsCount; i++)
        {

            sheet = workbook.Worksheets[i];
            if (sheet.Name == "T196")
            {
                c1 = sheet.Cells;
            }
        }
        string _inf = string.Format(@"PackingListSample for {0}.xls", oId);
        Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
        style1.Font.IsBold = true;
        style1.Font.Size = 12;
        style1.HorizontalAlignment = TextAlignmentType.Center;
        style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
        style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
        style1.BackgroundColor = System.Drawing.Color.FromArgb(255, 204, 153);

        //Select Data
        #region Booking Order
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + oId + "'");
        C2.JobHouse jo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse; 
        DateTime today = DateTime.Now;
        //decimal basic = 0;
        //int days = 0;
        string sql = "";
        if (jo!=null)
        {
            c1[1, 2].PutValue(jo.BookingNo);
            c1[1, 5].PutValue(jo.JobNo);
            c1[1, 8].PutValue(jo.ContNo);
            c1[2, 2].PutValue(jo.ShipperInfo);
            c1[3, 0].SetStyle(style1);
            c1[3, 2].SetStyle(style1);
            c1[3, 2].PutValue(jo.ConsigneeInfo);
            c1[3, 9].PutValue(jo.Responsible);
            c1[4, 3].PutValue(jo.ConsigneeRemark);
            c1[4, 7].PutValue(jo.ConsigneeEmail);
            c1[5, 3].PutValue(jo.Email1);
            c1[5, 7].PutValue(jo.Email2);
            c1[6, 3].PutValue(jo.Tel1);
            c1[6, 5].PutValue(jo.Tel2);
            c1[6, 7].PutValue(jo.Mobile1);
            c1[6, 9].PutValue(jo.Mobile2);
            c1[7, 2].PutValue(jo.Desc1);
            c1[9, 2].PutValue(jo.ClientId);
            c1[10, 2].PutValue(jo.ClientEmail);
            c1[11, 2].PutValue(jo.Remark1);
            if (jo.PrepaidInd=="YES")
              c1[12, 3].PutValue("√");
            c1[12, 7].PutValue(jo.CollectAmount1);
            c1[12, 9].PutValue(jo.CollectAmount1);
            if (jo.DutyPayment == "DUTY PAID")
            {
                c1[13, 2].PutValue("税中国付" + "/" + jo.DutyPayment);
            }
            else {
                c1[13, 2].PutValue("税新加坡付" + "/" + jo.DutyPayment);
            }
            c1[14, 2].PutValue(jo.Incoterm);
            c1[15, 2].PutValue(jo.Remark2);
        }


        #endregion

        workbook.Save(pathOpen, FileFormatType.Excel2003);

        MemoryStream ms = new MemoryStream();
        workbook.Save(ms, FileFormatType.Excel2003);

        byte[] bt = ms.GetBuffer();

        try
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Length", bt.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
            Response.BinaryWrite(bt);
        }
        catch (Exception exc)
        {
            throw new Exception(exc.Message + "/" + exc.StackTrace);
        }
        ms.Dispose();

    }
    public string ShowUom(object s) {
        string uom = SafeValue.SafeString(s);
        string str = "";
        if (uom == "CTN")
        {
            str = "CTN/箱";
        }
        if (uom == "PKG")
        {
            str = "PKG/件";
        }
        if (uom == "BAG")
        {
            str = "BAG/包";
        }
        if (uom == "PAL")
        {
            str = "PAL/卡板";
        }
        if (uom == "TON")
        {
            str = "TON/吨";
        }
        return str;
    }
    public void SetColor(Cells c1, int r, bool res)
    {

        for (int t = 14; t <= 18; t++)
        {
            c1[r, t].Style.ForegroundColor = System.Drawing.Color.FromArgb(255, 153, 0);
            c1[r, t].Style.Pattern = BackgroundType.Solid;
            if (true)//Title
            {
                c1[r + 1, t].Style.ForegroundColor = System.Drawing.Color.FromArgb(255, 153, 0);
                c1[r + 1, t].Style.Pattern = BackgroundType.Solid;
            }
        }
        for (int t = 19; t <= 22; t++)
        {
            c1[r, t].Style.ForegroundColor = System.Drawing.Color.FromArgb(204, 255, 255);
            c1[r, t].Style.Pattern = BackgroundType.Solid;
            if (true)//Title
            {
                c1[r + 1, t].Style.ForegroundColor = System.Drawing.Color.FromArgb(204, 255, 255);
                c1[r + 1, t].Style.Pattern = BackgroundType.Solid;
            }
        }
        c1[r, 23].Style.ForegroundColor = System.Drawing.Color.FromArgb(204, 255, 204);
        c1[r, 23].Style.Pattern = BackgroundType.Solid;
        c1[r, 24].Style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 255);
        c1[r, 24].Style.Pattern = BackgroundType.Solid;
        c1[r, 28].Style.ForegroundColor = System.Drawing.Color.FromArgb(255, 153, 204);
        c1[r, 28].Style.Pattern = BackgroundType.Solid;

        c1[r, 35].Style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 255);
        c1[r, 35].Style.Pattern = BackgroundType.Solid;
        c1[r, 36].Style.ForegroundColor = System.Drawing.Color.FromArgb(255, 255, 0);
        c1[r, 36].Style.Pattern = BackgroundType.Solid;

        c1[r, 38].Style.ForegroundColor = System.Drawing.Color.FromArgb(51, 204, 204);
        c1[r, 38].Style.Pattern = BackgroundType.Solid;
        c1[r, 39].Style.ForegroundColor = System.Drawing.Color.FromArgb(192, 192, 192);
        c1[r, 39].Style.Pattern = BackgroundType.Solid;
        c1[r, 40].Style.ForegroundColor = System.Drawing.Color.FromArgb(255, 204, 153);
        c1[r, 40].Style.Pattern = BackgroundType.Solid;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        SaveExcel();
    }
    protected void gridParty_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] PartyId = new object[grid.VisibleRowCount];
        object[] Name = new object[grid.VisibleRowCount];
        object[] CrNo = new object[grid.VisibleRowCount];
        object[] Address = new object[grid.VisibleRowCount];
        object[] Tel1 = new object[grid.VisibleRowCount];
        object[] Tel2 = new object[grid.VisibleRowCount];
        object[] Fax1 = new object[grid.VisibleRowCount];
        object[] Fax2 = new object[grid.VisibleRowCount];
        object[] Email1 = new object[grid.VisibleRowCount];
        object[] Email2 = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            PartyId[i] = grid.GetRowValues(i, "PartyId");
            Name[i] = grid.GetRowValues(i, "Name");
            CrNo[i] = grid.GetRowValues(i, "CrNo");
            Address[i] = grid.GetRowValues(i, "Address");
            Tel1[i] = grid.GetRowValues(i, "Tel1");
            Tel2[i] = grid.GetRowValues(i, "Tel2");
            Fax1[i] = grid.GetRowValues(i, "Fax1");
            Fax2[i] = grid.GetRowValues(i, "Fax2");
            Email1[i] = grid.GetRowValues(i, "Email1");
            Email2[i] = grid.GetRowValues(i, "Email2");
        }
        e.Properties["cpPartyId"] = PartyId;
        e.Properties["cpName"] = Name;
        e.Properties["cpCrNo"] = CrNo;
        e.Properties["cpAddress"] = Address;
        e.Properties["cpTel1"] = Tel1;
        e.Properties["cpTel2"] = Tel2;
        e.Properties["cpFax1"] = Fax1;
        e.Properties["cpFax2"] = Fax2;
        e.Properties["cpEmail1"] = Email1;
        e.Properties["cpEmail2"] = Email2;
    }
    protected void grd_Stock_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("UpdateInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                grid.UpdateEdit();
                

                ASPxTextBox txt_cargo_id = grd_Det.FindEditFormTemplateControl("txt_cargo_id") as ASPxTextBox;
                string sql = string.Format(@"select sum(Price2) from job_house where OrderId={0}", txt_cargo_id.Text);
                decimal totalAmt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                e.Result = totalAmt;
            }
        }
    }

}