using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;
using DevExpress.Web.ASPxTabControl;
using System.Data;

public partial class PagesContainer_Job_StoringOrderEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["TptWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["TptWhere"] = "DocNo='" + Request.QueryString["no"].ToString() + "'";
                this.dsTransport.FilterExpression = Session["TptWhere"].ToString();
                this.txt_ReNo.Text = Request.QueryString["no"].ToString();
            }
            else if (Request.QueryString["no"].ToString() == "0")
            {
                this.grid_Transport.AddNewRow();
            }
            else
                this.dsTransport.FilterExpression = "1=0";
        }
        if (Session["TptWhere"] != null)
        {
            this.dsTransport.FilterExpression = Session["TptWhere"].ToString();
            if (this.grid_Transport.GetRow(0) != null)
                this.grid_Transport.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //ASPxButtonEdit btnPort = this.grid_Transport.FindEditFormTemplateControl("btn_Port") as ASPxButtonEdit;
        if (EzshipHelper.GetUserName().ToUpper() == "ADMIN")
        {
            btn_Port.Enabled = true;
            btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        }
        else
        {
            btn_Port.Text = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        }
    }
    #region Job Info
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.ContAsset));
        }
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string jobN = Request.QueryString["no"].ToString();
        e.NewValues["DocNo"] = jobN;
        e.NewValues["ShipEta"] = DateTime.Now;
        e.NewValues["ShipEtd"] = DateTime.Now;
    }
    protected void SaveJob()
    {
        try
        {
            ASPxGridView g = this.grid_Transport;


            ASPxTextBox houseId = g.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            int Id = SafeValue.SafeInt(houseId.Text, 0);
            ASPxTextBox docNo = g.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(ContAsset), "Id='" + Id + "'");
            ContAsset contA = C2.Manager.ORManager.GetObject(query) as ContAsset;
            bool isNew = false;
            if (Id == 0)
            {
                isNew = true;
                contA = new ContAsset();
                contA.DocNo = C2Setup.GetNextNo("StoringOrder");
                contA.DocType = "SO";
            }
            ASPxTextBox jobNo = g.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
            contA.JobNo = jobNo.Text;
            ASPxComboBox depotCode = g.FindEditFormTemplateControl("cmb_DepotCode") as ASPxComboBox;
            contA.DepotCode = depotCode.Text;
            ASPxTextBox shipVessel = g.FindEditFormTemplateControl("txt_ShipVessel") as ASPxTextBox;
            contA.ShipVessel = shipVessel.Text;
            ASPxTextBox shipVoyage = g.FindEditFormTemplateControl("txt_ShipVoyage") as ASPxTextBox;
            contA.ShipVoyage = shipVoyage.Text;
            ASPxDateEdit shipEta = g.FindEditFormTemplateControl("date_ShipEta") as ASPxDateEdit;
            contA.ShipEta = shipEta.Date;
            ASPxDateEdit shipEtd = g.FindEditFormTemplateControl("date_ShipEtd") as ASPxDateEdit;
            contA.ShipEtd = shipEtd.Date;
            ASPxButtonEdit shipPol = g.FindEditFormTemplateControl("btn_ShipPol") as ASPxButtonEdit;
            contA.ShipPol = shipPol.Text;
            ASPxButtonEdit shipPod = g.FindEditFormTemplateControl("btn_ShipPod") as ASPxButtonEdit;
            contA.ShipPod = shipPod.Text;
            ASPxMemo shipNote = g.FindEditFormTemplateControl("txt_ShipNote") as ASPxMemo;
            contA.ShipNote = shipNote.Text;
            ASPxButtonEdit shipCarrierCode = g.FindEditFormTemplateControl("btn_ShipCarrierCode") as ASPxButtonEdit;
            contA.ShipCarrierCode = shipCarrierCode.Text;
            ASPxTextBox shipCarrierRef = g.FindEditFormTemplateControl("txt_ShipCarrierRef") as ASPxTextBox;
            contA.ShipCarrierRef = shipCarrierRef.Text;
            ASPxTextBox demurrageRef = g.FindEditFormTemplateControl("txt_DemurrageRef") as ASPxTextBox;
            contA.DemurrageRef = demurrageRef.Text;
            ASPxSpinEdit demurrageFreeDay = g.FindEditFormTemplateControl("spin_DemurrageFreeDay") as ASPxSpinEdit;
            contA.DemurrageFreeDay = SafeValue.SafeInt(demurrageFreeDay.Text, 0);
            ASPxDateEdit demurrageStartDate = g.FindEditFormTemplateControl("date_DemurrageStartDate") as ASPxDateEdit;
            contA.DemurrageStartDate = demurrageStartDate.Date;
            ASPxTextBox detentionRef = g.FindEditFormTemplateControl("txt_DetentionRef") as ASPxTextBox;
            contA.DetentionRef = detentionRef.Text;
            ASPxSpinEdit detentionFreeDay = g.FindEditFormTemplateControl("spin_DetentionFreeDay") as ASPxSpinEdit;
            contA.DetentionFreeDay = SafeValue.SafeInt(detentionFreeDay.Text, 0);
            ASPxDateEdit detentionStartDate = g.FindEditFormTemplateControl("date_DetentionStartDate") as ASPxDateEdit;
            contA.DetentionStartDate = detentionStartDate.Date;
            ASPxComboBox returnType = g.FindEditFormTemplateControl("cmb_ReturnType") as ASPxComboBox;
            contA.ReturnType = returnType.Text;
            ASPxButtonEdit shipperCode = g.FindEditFormTemplateControl("btn_ShipperCode") as ASPxButtonEdit;
            contA.ShipperCode = shipperCode.Text;
            ASPxTextBox shipperName = g.FindEditFormTemplateControl("txt_ShipperName") as ASPxTextBox;
            contA.ShipperName = shipperName.Text;
            ASPxButtonEdit consigneeCode = g.FindEditFormTemplateControl("btn_ConsigneeCode") as ASPxButtonEdit;
            contA.ConsigneeCode = consigneeCode.Text;
            ASPxTextBox consigneeName = g.FindEditFormTemplateControl("txt_ConsigneeName") as ASPxTextBox;
            contA.ConsigneeName = consigneeName.Text;
            ASPxButtonEdit haulierCode = g.FindEditFormTemplateControl("btn_HaulierCode") as ASPxButtonEdit;
            contA.HaulierCode = haulierCode.Text;
            ASPxDateEdit haulierCompleteDate = g.FindEditFormTemplateControl("date_HaulierCompleteDate") as ASPxDateEdit;
            contA.HaulierCompleteDate = haulierCompleteDate.Date;
            ASPxTextBox instruction = g.FindEditFormTemplateControl("txt_Instruction") as ASPxTextBox;
            contA.Instruction = instruction.Text;


            if (isNew)
            {
                contA.CreateBy = EzshipHelper.GetUserName();
                contA.CreateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(contA, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(contA);

                C2Setup.SetNextNo("StoringOrder", contA.DocNo);
                houseId.Text = contA.Id.ToString();
                //this.txt_JobNo.Text = tpt1.JobNo;
                docNo.Text = contA.DocNo;
                Session["ExpWhere"] = "Id='" + contA.Id + "'";
                this.dsTransport.FilterExpression = Session["ExpWhere"].ToString();
                if (this.grid_Transport.GetRow(0) != null)
                    this.grid_Transport.StartEdit(0);
            }
            else
            {

                contA.UpdateBy = EzshipHelper.GetUserName();
                contA.UpdateDateTime = DateTime.Now;
                C2.Manager.ORManager.StartTracking(contA, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(contA);
                string sql = "update cont_assetEvent set pod='" + shipPod.Text + "',pol='" + shipPol.Text + "' where docNo='" + docNo.Text + "' ";
                C2.Manager.ORManager.ExecuteCommand(sql);
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message + ex.StackTrace); }
    }


    protected void grid_Transport_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {

        //throw new Exception("AA");
        try
        {
            if (this.grid_Transport.EditingRowVisibleIndex > -1)
            {
                ASPxGridView g = this.grid_Transport;
                //ASPxTextBox custName = g.FindEditFormTemplateControl("txt_Tpt_CustomerName") as ASPxTextBox;
                //ASPxTextBox haulierName = g.FindEditFormTemplateControl("txt_Tpt_HaulierName") as ASPxTextBox;
                //custName.Text = EzshipHelper.GetPartyName(this.grid_Transport.GetRowValues(this.grid_Transport.EditingRowVisibleIndex, new string[] { "CustomerId" }));
                //haulierName.Text = EzshipHelper.GetPartyName(this.grid_Transport.GetRowValues(this.grid_Transport.EditingRowVisibleIndex, new string[] { "HaulierId" }));


            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ex.StackTrace);
        }
        //if (Request.QueryString["JobNo"] != null)
        //{
        //    string jobType = EzshipHelper.GetJobType("SE", Request.QueryString["JobNo"].ToString());
        //}
        //else
        //{
        //    string refNo = SafeValue.SafeString(this.grid_Transport.GetRowValues(this.grid_Transport.EditingRowVisibleIndex, new string[] { "RefNo" }));
        //}
    }
    protected void grid_Transport_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        //throw new Exception("AA");
        try
        {
            if (s == "Save")
                SaveJob();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ex.StackTrace);
        }
    }


    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxGridView g = sender as ASPxGridView;
        ASPxTextBox masterId = g.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        string userId = HttpContext.Current.User.Identity.Name;
    }

    #endregion

    #region Event
    protected void grid_Event_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView g = this.grid_Transport;
        ASPxTextBox docNo = g.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxGridView grd = sender as ASPxGridView;
        this.dsEvent.FilterExpression = "DocNo='" + SafeValue.SafeString(docNo.Text, "-1") + "'";

    }
    protected void grid_Event_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.ContAssetEvent));
        }
    }
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] contNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            contNs[i] = grid.GetRowValues(i, "ContainerNo");
            contTypes[i] = grid.GetRowValues(i, "ContainerType");
        }
        e.Properties["cpContType"] = contTypes;
        e.Properties["cpContN"] = contNs;
    }
    protected void grid_Event_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["EventDateTime"] = DateTime.Now;

        e.NewValues["JobNo"] = " ";
        e.NewValues["EventCode"] = " ";
        e.NewValues["EventDepot"] = " ";
        e.NewValues["Pol"] = " ";
        e.NewValues["Pod"] = " ";
        e.NewValues["VehicleNo"] = " ";
        e.NewValues["ReturnDate"] = DateTime.Today;
        e.NewValues["ReceiveDate"] = DateTime.Today;
        e.NewValues["ReleaseDate"] = DateTime.Today;
        e.NewValues["State"] = " ";
        e.NewValues["Insturction"] = " ";
    }
    protected void grid_Event_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView g = this.grid_Transport;
        ASPxGridView gEvent = g.FindEditFormTemplateControl("grid_Event") as ASPxGridView;
        ASPxDropDownEdit contNo = gEvent.FindEditFormTemplateControl("DropDownEdit") as ASPxDropDownEdit;
        ASPxTextBox contType = gEvent.FindEditFormTemplateControl("txt_ContainerType") as ASPxTextBox;
        if (checkCont_NO_Type(contNo.Text, contType.Text))
        {
            throw new Exception("Container No and Type are not match");
        }

        e.NewValues["EventDateTime"] = DateTime.Now;
        ASPxTextBox docNo = g.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        e.NewValues["DocNo"] = docNo.Text;
        ASPxTextBox docType = g.FindEditFormTemplateControl("txt_DocType") as ASPxTextBox;
        e.NewValues["DocType"] = docType.Text;
        ASPxTextBox jobNo = g.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        e.NewValues["JobNo"] = jobNo.Text;
        ASPxButtonEdit shipPol = g.FindEditFormTemplateControl("btn_ShipPol") as ASPxButtonEdit;
        e.NewValues["Pol"] = shipPol.Text;
        ASPxButtonEdit shipPod = g.FindEditFormTemplateControl("btn_ShipPod") as ASPxButtonEdit;
        e.NewValues["Pod"] = shipPod.Text;
        //string Name = EzshipHelper.GetUserName();
        //string Port = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        e.NewValues["EventPort"] = btn_Port.Text;

    }
    protected void grid_Event_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView g = this.grid_Transport;
        ASPxGridView gEvent = g.FindEditFormTemplateControl("grid_Event") as ASPxGridView;
        ASPxDropDownEdit contNo = gEvent.FindEditFormTemplateControl("DropDownEdit") as ASPxDropDownEdit;
        ASPxTextBox contType = gEvent.FindEditFormTemplateControl("txt_ContainerType") as ASPxTextBox;
        if (checkCont_NO_Type(contNo.Text, contType.Text))
        {
            throw new Exception("Container No and Type are not match");
        }

    }
    protected void grid_Event_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_Event_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {

    }
    protected void grid_Event_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Event_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
    }
    protected void grid_Event_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        //string Port = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        //string where = string.Format("(isnull(EventCode,'')='' or EventCode='gateout') AND EventPort='{0}' AND EventDateTime in(select MAX(EventDateTime) from Cont_AssetEvent where isnull(EventCode,'')='' or EventCode='gateout' group by ContainerNo)",Port); 
        string Port = btn_Port.Text;
        //string where = string.Format(" Id in(select id from Cont_AssetEvent as aEvent left outer join (select containerno,MAX(EventDateTime) as EventDateTime from Cont_AssetEvent group by containerno) as ass on ass.ContainerNo=aEvent.ContainerNo and ass.EventDateTime=aEvent.EventDateTime where (isnull(EventCode,'')='' or EventCode='gateout') and aEvent.EventPort='{0}' and ass.EventDateTime is not null and len(replace(isnull(aEvent.DocNo,''),' ',''))=0 and len(isnull(DocType,''))=0 )", Port);
        //dsRefCont.FilterExpression = where;
        string sql = string.Format(@"select aEvent.ContainerType,aEvent.ContainerNo from Cont_AssetEvent as aEvent left outer join (select containerno,MAX(EventDateTime) as EventDateTime from Cont_AssetEvent group by containerno) as ass on ass.ContainerNo=aEvent.ContainerNo and ass.EventDateTime=aEvent.EventDateTime left outer join Ref_Container as Cont on aEvent.ContainerNo=Cont.ContainerNo where (isnull(EventCode,'')='' or EventCode='gateout') and aEvent.EventPort='{0}' and ass.EventDateTime is not null and len(replace(isnull(aEvent.DocNo,''),' ',''))=0 and isnull(DocType,'')<>'SO' and Cont.StatusCode='Use' union all select ContainerType,ContainerNo  from Ref_Container where ContainerNo not in( select distinct isnull(ContainerNo,'') from Cont_AssetEvent) and StatusCode='Use' ", Port);
        ASPxGridView grid = sender as ASPxGridView;
        ASPxDropDownEdit d = grid.FindEditFormTemplateControl("DropDownEdit") as ASPxDropDownEdit;
        ASPxGridView gvlist = d.FindControl("gridPopCont") as ASPxGridView;
        
        gvlist.DataSource=C2.Manager.ORManager.GetDataSet(sql);
        gvlist.DataBind();
    }
    #endregion

    private bool checkCont_NO_Type(string NO, string Type)
    {
        if (Type.Length == 0 || NO.Trim().Length == 0)
        {
            return true;
        }
        string sql = "select COUNT(*) from Ref_Container where ContainerNo='" + NO + "' and ContainerType='" + Type + "' ";
        int result = Convert.ToInt32(ConnectSql.GetTab(sql).Rows[0][0]);
        if (result != 1)
        {
            return true;
        }
        return false;
    }

}