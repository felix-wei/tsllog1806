using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTabControl;

public partial class Z_job_edit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        string id=Request.QueryString["id"] ?? "";
        if (id != "" && id != "0")
        {
            txtSid.Text = id;
            this.ds1.FilterExpression = "SequenceId=" + id;
            if (this.grid1.GetRow(0) != null)
                this.grid1.StartEdit(0);
        }
        else
        {
            this.ds1.FilterExpression = "1=0";
        }


    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region g1
    protected void grid1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.JobOrder));
    }
    protected void grid1_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }
    protected void grid1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name.ToUpper();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["BkgRefNo"] = SafeValue.SafeString(e.NewValues["BkgRefNo"]);
        e.NewValues["HblNo"] = SafeValue.SafeString(e.NewValues["HblNo"]);
        e.NewValues["CustomerId"] = SafeValue.SafeString(e.NewValues["CustomerId"]);
        e.NewValues["ShipperId"] = SafeValue.SafeString(e.NewValues["ShipperId"]);
        e.NewValues["ShipperName"] = SafeValue.SafeString(e.NewValues["ShipperName"]);
        e.NewValues["ShipperContact"] = SafeValue.SafeString(e.NewValues["ShipperContact"]);
        e.NewValues["ShipperFax"] = SafeValue.SafeString(e.NewValues["ShipperFax"]);
        e.NewValues["ShipperTel"] = SafeValue.SafeString(e.NewValues["ShipperTel"]);
        e.NewValues["ShipperEmail"] = SafeValue.SafeString(e.NewValues["ShipperEmail"]);
        e.NewValues["Pol"] = SafeValue.SafeString(e.NewValues["Pol"]);
        e.NewValues["PlaceLoadingName"] = SafeValue.SafeString(e.NewValues["PlaceLoadingName"]);
        e.NewValues["FrtTerm"] = SafeValue.SafeString(e.NewValues["FrtTerm"]);
        e.NewValues["Pod"] = SafeValue.SafeString(e.NewValues["Pod"]);
        e.NewValues["PlaceDischargeName"] = SafeValue.SafeString(e.NewValues["PlaceDischargeName"]);
        e.NewValues["PreCarriage"] = SafeValue.SafeString(e.NewValues["PreCarriage"]);
        e.NewValues["PlaceDeliveryId"] = SafeValue.SafeString(e.NewValues["PlaceDeliveryId"]);
        e.NewValues["PlaceDeliveryname"] = SafeValue.SafeString(e.NewValues["PlaceDeliveryname"]);
        e.NewValues["PlaceDeliveryTerm"] = SafeValue.SafeString(e.NewValues["PlaceDeliveryTerm"]);
        e.NewValues["PlaceReceiveId"] = SafeValue.SafeString(e.NewValues["PlaceReceiveId"]);
        e.NewValues["PlaceReceiveName"] = SafeValue.SafeString(e.NewValues["PlaceReceiveName"]);
        e.NewValues["PlaceReceiveTerm"] = SafeValue.SafeString(e.NewValues["PlaceReceiveTerm"]);
        e.NewValues["ShipOnBoardInd"] = SafeValue.SafeString(e.NewValues["ShipOnBoardInd"]);
        e.NewValues["ShipOnBoardDate"] = SafeValue.SafeDate(e.NewValues["ShipOnBoardDate"], new DateTime(1753, 01, 01));
        e.NewValues["ExpressBl"] = SafeValue.SafeString(e.NewValues["ExpressBl"]);
        e.NewValues["CustRefNo"] = SafeValue.SafeString(e.NewValues["CustRefNo"]);
        e.NewValues["ServiceType"] = SafeValue.SafeString(e.NewValues["ServiceType"]);
        e.NewValues["SurrenderBl"] = SafeValue.SafeString(e.NewValues["SurrenderBl"]);
        e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
        EzshipLog.Log(SafeValue.SafeString(e.NewValues["JobNo"]), SafeValue.SafeString(e.NewValues["JobNo"]), "Direct", "Update");

    }

    #endregion



    #region g11
    protected void grid11_Init(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        if (g != null)
            g.ForceDataRowType(typeof(C2.JobCost));
        // ASPxGridCommandColumn g.Columns[0]
    }
    protected void grid11_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        string sql = "select JobNo from JobOrder where SequenceId='" + SafeValue.SafeString(g.GetMasterRowKeyValue(), "0") + "'";
        this.dsCosting.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) + "'";
    }
    protected void grid11_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void grid11_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox refN = grid1.FindEditFormTemplateControl("TXT0") as ASPxTextBox;
        e.NewValues["RefNo"] = refN.Text;
        base_RowInserting(sender, e);
    }

    protected void grid11_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        base_RowUpdating(sender, e);
    }

    protected void grid11_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }

    #endregion


    #region g12
    protected void grid12_Init(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        if (g != null)
            g.ForceDataRowType(typeof(C2.JobCargo));
        // ASPxGridCommandColumn g.Columns[0]
    }
    protected void grid12_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        string sql = "select JobNo from JobOrder where SequenceId='" + SafeValue.SafeString(g.GetMasterRowKeyValue(), "0") + "'";
        this.ds12.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) + "'";
    }
    protected void grid12_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void grid12_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        e.NewValues["ParentId"] = SafeValue.SafeInt(g.GetMasterRowKeyValue(), 0);
        e.NewValues["RowType"] = "MEETING12";
        base_RowInserting(sender, e);
    }

    protected void grid12_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ScheduleDate"] = SafeValue.SafeDate(e.NewValues["ScheduleDate"], new DateTime(1753, 01, 01));
        base_RowUpdating(sender, e);
    }

    protected void grid12_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }

    #endregion

    #region g13
    protected void grid13_Init(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        if (g != null)
            g.ForceDataRowType(typeof(C2.JobWorkOrder));
        // ASPxGridCommandColumn g.Columns[0]
    }
    protected void grid13_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        string sql = "select JobNo from JobOrder where SequenceId='" + SafeValue.SafeString(g.GetMasterRowKeyValue(), "0") + "'";
        this.ds13.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) + "'";
        //this.dsAssignTo.FilterExpression = "RowStatus='RECORD' AND RowType='MEETING11' AND ParentId=" + Helper.Safe.SafeString(g.GetMasterRowKeyValue(), "0");
    }
    protected void grid13_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void grid13_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        ASPxTextBox refN = grid1.FindEditFormTemplateControl("TXT0") as ASPxTextBox;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
        e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }

    protected void grid13_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
        e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }

    protected void grid13_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }

    #endregion

    #region g14
    protected void grid14_Init(object sender, EventArgs e)
    {
        //ASPxGridView g = sender as ASPxGridView;
        //if (g != null)
        //    g.ForceDataRowType(typeof(C2.LogEvent));
        // ASPxGridCommandColumn g.Columns[0]
    }
    protected void grid14_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        string sql = "select JobNo from JobOrder where SequenceId='" + SafeValue.SafeString(g.GetMasterRowKeyValue(), "0") + "'";
        this.ds14.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) + "'";
    }
    protected void grid14_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }

    #endregion




    #region base
    protected void base_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        try
        {
            //  if (e.DataColumn.FieldName != "ExpiryDate") return;
            //  if (SafeValue.SafeInt(e.CellValue, 0) <= 30)
            //      e.Cell.BackColor = System.Drawing.Color.LightGreen;
            //  if (SafeValue.SafeInt(e.CellValue, 0) <= 7)
            //      e.Cell.BackColor = System.Drawing.Color.Orange;
        }
        catch { }
    }

    protected void base_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["RowStatus"] = "RECORD";
        e.NewValues["RowCreateBy"] = HttpContext.Current.User.Identity.Name.ToUpper();
        e.NewValues["RowUpdateBy"] = HttpContext.Current.User.Identity.Name.ToUpper();
        e.NewValues["RowCreateTime"] = DateTime.Now;
        e.NewValues["RowUpdateTime"] = DateTime.Now;

    }



    protected void base_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name.ToUpper();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }




    protected void base_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;

        if (e.CallbackName == "CUSTOMCALLBACK" && e.Args.Length == 1 && e.Args[0] == "UPDATEEDIT")
        {

            int rowIndex = g.EditingRowVisibleIndex;
            g.UpdateEdit();
            ASPxWebControl.RedirectOnCallback("job_edit.aspx?id="+txtSid.Text);
            //g.StartEdit(rowIndex);

        }
    }

    #endregion


    #region marking
    protected void grid_Mkgs_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        string sql = "select JobNo from JobOrder where SequenceId='" + SafeValue.SafeString(g.GetMasterRowKeyValue(), "0") + "'";
        this.dsMarking.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) + "'";
        //ASPxGridView grd = sender as ASPxGridView;
        //string sql = "select JobNo from SeaExport where sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        //string jobNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        //string sql_jobType = "select mast.jobtype from seaexport job inner join SeaExportRef mast on job.RefNo=mast.Refno  where job.sequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        //string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_jobType), "");
        //if (jobType == "FCL")
        //    this.dsMarking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and MkgType='Cont'";
        //else
        //    this.dsMarking.FilterExpression = "JobNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "") + "' and MkgType='Bl'";

    }
    protected void grid_Mkgs_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobCargo));
        }
    }
    protected void grid_Mkgs_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Volume"] = 0;
        e.NewValues["Weight"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["GrossWt"] = 0;
        e.NewValues["NetWt"] = 0;
        e.NewValues["PackageType"] = " ";
        e.NewValues["ContainerType"] = " ";
        e.NewValues["Volume1"] = 0;
        e.NewValues["Weight1"] = 0;
        e.NewValues["Qty1"] = 0;
        e.NewValues["L"] = 0;
        e.NewValues["W"] = 0;
        e.NewValues["H"] = 0;
        e.NewValues["L1"] = 0;
        e.NewValues["W1"] = 0;
        e.NewValues["H1"] = 0;
        ASPxGridView g = sender as ASPxGridView;
        ASPxTextBox refN = grid1.FindEditFormTemplateControl("TXT0") as ASPxTextBox;
        string sql = string.Format("select mast.JobType,det.ContainerNo,det.SealNo,det.ContainerType from SeaExportMkg det inner join SeaExportRef mast on mast.RefNo=det.RefNo where det.MkgType='CONT' and det.RefNo='{0}'", refN.Text);
        DataTable tab = ConnectSql.GetTab(sql);
        if (tab.Rows.Count > 0)
        {
            if (SafeValue.SafeString(tab.Rows[0]["JobType"]) == "FCL")
            {
            }
            else
            {
                e.NewValues["ContainerNo"] = SafeValue.SafeString(tab.Rows[0]["ContainerNo"]);
                e.NewValues["SealNo"] = SafeValue.SafeString(tab.Rows[0]["SealNo"]);
                e.NewValues["ContainerType"] = SafeValue.SafeString(tab.Rows[0]["ContainerType"]);
            }
        }

    }
    protected void grid_Mkgs_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        ASPxTextBox refN = grid1.FindEditFormTemplateControl("TXT0") as ASPxTextBox;
        e.NewValues["RefNo"] = refN.Text;

        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        string jobType = EzshipHelper.GetJobType("SE", refN.Text);
        if (jobType == "FCL")
            e.NewValues["MkgType"] = "CONT";
        else
            e.NewValues["MkgType"] = "BL";

        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["PackageType"] = SafeValue.SafeString(e.NewValues["PackageType"]);
        e.NewValues["PackageType1"] = SafeValue.SafeString(e.NewValues["PackageType"]);

        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"], 0);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"], 0);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["GrossWt"] = SafeValue.SafeDecimal(e.NewValues["GrossWt"], 0);
        e.NewValues["NetWt"] = SafeValue.SafeDecimal(e.NewValues["NetWt"], 0);
        e.NewValues["Volume1"] = SafeValue.SafeDecimal(e.NewValues["Volume1"], 0);
        e.NewValues["Weight1"] = SafeValue.SafeDecimal(e.NewValues["Weight1"], 0);
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["L"] = SafeValue.SafeDecimal(e.NewValues["L"], 0);
        e.NewValues["W"] = SafeValue.SafeDecimal(e.NewValues["W"], 0);
        e.NewValues["H"] = SafeValue.SafeDecimal(e.NewValues["H"], 0);
        e.NewValues["L1"] = SafeValue.SafeDecimal(e.NewValues["L1"], 0);
        e.NewValues["W1"] = SafeValue.SafeDecimal(e.NewValues["W1"], 0);
        e.NewValues["H1"] = SafeValue.SafeDecimal(e.NewValues["H1"], 0);
    }
    protected void grid_Mkgs_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;


        e.NewValues["ContainerType"] = SafeValue.SafeString(e.NewValues["ContainerType"]);
        e.NewValues["PackageType"] = SafeValue.SafeString(e.NewValues["PackageType"]);
        e.NewValues["PackageType1"] = SafeValue.SafeString(e.NewValues["PackageType1"]);

        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"], 0);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"], 0);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["GrossWt"] = SafeValue.SafeDecimal(e.NewValues["GrossWt"], 0);
        e.NewValues["NetWt"] = SafeValue.SafeDecimal(e.NewValues["NetWt"], 0);
        e.NewValues["Volume1"] = SafeValue.SafeDecimal(e.NewValues["Volume1"], 0);
        e.NewValues["Weight1"] = SafeValue.SafeDecimal(e.NewValues["Weight1"], 0);
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["L"] = SafeValue.SafeDecimal(e.NewValues["L"], 0);
        e.NewValues["W"] = SafeValue.SafeDecimal(e.NewValues["W"], 0);
        e.NewValues["H"] = SafeValue.SafeDecimal(e.NewValues["H"], 0);
        e.NewValues["L1"] = SafeValue.SafeDecimal(e.NewValues["L1"], 0);
        e.NewValues["W1"] = SafeValue.SafeDecimal(e.NewValues["W1"], 0);
        e.NewValues["H1"] = SafeValue.SafeDecimal(e.NewValues["H1"], 0);
    }
    protected void grid_Mkgs_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] keyValues = new object[grid.VisibleRowCount];
        object[] contNs = new object[grid.VisibleRowCount];
        object[] sealNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            keyValues[i] = grid.GetRowValues(i, "SequenceId");
            contNs[i] = grid.GetRowValues(i, "ContainerNo");
            sealNs[i] = grid.GetRowValues(i, "SealNo");
            contTypes[i] = grid.GetRowValues(i, "ContainerType");
        }
        e.Properties["cpSealN"] = sealNs;
        e.Properties["cpContN"] = contNs;
        e.Properties["cpContType"] = contTypes;
        e.Properties["cpKeyValues"] = keyValues;
    }
    protected void grid_Mkgs_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //UpdateMast(e.NewValues["JobNo"].ToString());
    }
    protected void grid_Mkgs_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {

    }
    protected void grid_Mkgs_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        //UpdateMast(e.Values["JobNo"].ToString());
    }

    private void UpdateMast(string expN)
    {
        string sql = "select mast.jobtype,mast.RefNo from seaexport job inner join SeaExportRef mast on job.RefNo=mast.Refno  where job.JobNo='" + expN + "'";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 1)
        {

            string jobType = SafeValue.SafeString(tab.Rows[0][0]);
            if (jobType == "FCL")
                jobType = "CONT";
            else
                jobType = "BL";
            string refNo = SafeValue.SafeString(tab.Rows[0][1]);

            sql = string.Format(@"update SeaExport set 
Volume=(select SUM(Volume) from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') 
,Weight=(select SUM(Weight) from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') 
,Qty=(select SUM(Qty) from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') 
,PackageType=(select MAX(PackageType) from SeaExportMkg  where JobNo='{0}' and MkgType='{1}') 
where JobNo='{0}'", expN, jobType);
            //update house
            C2.Manager.ORManager.ExecuteCommand(sql);
            //update master
            sql = string.Format(@"update SeaExportRef set 
Volume=(select SUM(SeaExportMkg.Volume) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}' )
,Weight=(select SUM(SeaExportMkg.Weight) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}' )
,Qty=(select SUM(SeaExportMkg.Qty) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}' )
,PackageType=(select MAX(SeaExportMkg.PackageType) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='{1}' )

,BkgVolume=(select SUM(SeaExportMkg.Volume) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' )
,BkgWeight=(select SUM(SeaExportMkg.Weight) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' )
,BkgQty=(select SUM(SeaExportMkg.Qty) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' )
,BkgPackageType=(select MAX(SeaExportMkg.PackageType) from SeaExportMkg inner join SeaExport on SeaExport.RefNo=SeaExportMkg.RefNo and SeaExport.JobNo=SeaExportMkg.JobNo where SeaExportMkg.RefNo='{0}' and SeaExportMkg.MkgType='BKG' )
 where RefNo='{0}'", refNo, jobType);
            C2.Manager.ORManager.ExecuteCommand(sql);

            //string sql_sum = "SELECT SUM(Weight) AS wt,SUM(Volume) AS m3, SUM(Qty) AS PKG, MAX(PackageType) AS pkgtype, MAX(RefNo) as RefNo FROM SeaExportMkg where JobNo='" + expN + "'";
            //if(jobType=="FCL")
            //    sql_sum+=" and MkgType='Cont'";
            //else
            //    sql_sum+=" and MkgType='Bl'";


            //DataTable tab = Manager.ORManager.GetDataSet(sql_sum).Tables[0];
            //if (tab.Rows.Count > 0)
            //{
            //    DataRow row = tab.Rows[0];
            //    string sql_Update = string.Format("update SeaExport set Volume='{0}',Weight='{1}',Qty='{2}',PackageType ='{3}' where JobNo='{4}'"
            //        , SafeValue.SafeDecimal(row["M3"], 0), SafeValue.SafeDecimal(row["WT"], 0), SafeValue.SafeDecimal(row["PKG"], 0), SafeValue.SafeString(row["PKGTYPE"], " "), expN);
            //    Manager.ORManager.ExecuteCommand(sql_Update);

            //    string masterNo = SafeValue.SafeString(row["RefNo"], "");
            //    sql_sum = "SELECT SUM(Weight) AS wt,SUM(Volume) AS m3, SUM(Qty) AS PKG, MAX(PackageType) AS pkgtype FROM SeaExport where RefNo='" + masterNo + "'";
            //    tab = Manager.ORManager.GetDataSet(sql_sum).Tables[0];
            //    if (tab.Rows.Count > 0)
            //    {
            //        DataRow row1 = tab.Rows[0];
            //        sql_Update = string.Format("update SeaExportRef set Volume='{0}',Weight='{1}',Qty='{2}',PackageType ='{3}' where RefNo='{4}'"
            //            , SafeValue.SafeDecimal(row1["M3"], 0), SafeValue.SafeDecimal(row1["WT"], 0), SafeValue.SafeDecimal(row1["PKG"], 0), SafeValue.SafeString(row1["PKGTYPE"], " "), masterNo);
            //        Manager.ORManager.ExecuteCommand(sql_Update);
            //    }
            //}
        }
    }
    #endregion

    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaAttachment));
        }
    }
    //protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    //{
    //    ASPxGridView g = sender as ASPxGridView;
    //    string sql = "select JobNo from JobOrder where SequenceId='" + SafeValue.SafeString(g.GetMasterRowKeyValue(), "0") + "'";
    //    this.dsJobPhoto.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) + "'";
    //}
    //protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    //{
    //    e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    //}
    //protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    //{

    //}
    //protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    //{
    //    e.NewValues["FileNote"] = " ";
    //}

    #endregion

    #region Costing
    protected void Grid_Costing_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.JobCost));
        }
    }
    protected void Grid_Costing_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        string sql = "select JobNo from JobOrder where SequenceId='" + SafeValue.SafeString(g.GetMasterRowKeyValue(), "0") + "'";
        this.dsCosting.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql)) + "'";
    }
    protected void Grid_Costing_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SaleQty"] = 0;
        e.NewValues["SalePrice"] = 0;
        e.NewValues["SaleDocAmt"] = 0;
    }
    protected void Grid_Costing_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void Grid_Costing_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["SplitType"] = SafeValue.SafeString(e.NewValues["SplitType"]);
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"]);
        e.NewValues["ChgCodeDes"] = SafeValue.SafeString(e.NewValues["ChgCodeDes"]);
        e.NewValues["SaleQty"] = SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0);
        e.NewValues["SalePrice"] = SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0);
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleCurrency"] = SafeValue.SafeString(e.NewValues["SaleCurrency"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["Salesman"] = SafeValue.SafeString(e.NewValues["Salesman"], "");
        e.NewValues["DocNo"] = SafeValue.SafeString(e.NewValues["DocNo"], "");
    }
    protected void Grid_Costing_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView g = sender as ASPxGridView;
        ASPxTextBox refN = grid1.FindEditFormTemplateControl("TXT0") as ASPxTextBox;
        e.NewValues["RefNo"] = refN.Text;
        e.NewValues["JobNo"] = refN.Text;
        e.NewValues["JobType"] = "SE";
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
    }

    #endregion

    protected void grid1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid1.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox custName = grid1.FindEditFormTemplateControl("txtCustomer") as ASPxTextBox;
            custName.Text = EzshipHelper.GetPartyName(this.grid1.GetRowValues(this.grid1.EditingRowVisibleIndex, new string[] { "CustomerId" }));
        }
    }
}
