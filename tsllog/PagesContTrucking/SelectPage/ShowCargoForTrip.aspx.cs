using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wilson.ORMapper;
using DevExpress.Web;
using System.Collections;
using System.Collections.Specialized;

public partial class PagesContTrucking_SelectPage_ShowCargoForTrip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Stock In
    protected void grid_wh_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string tripId = SafeValue.SafeString(Request.QueryString["no"]);
        dsWh.FilterExpression = "TripId=" + tripId + " ";
    }
    protected void grid_wh_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.JobHouse));
        }
    }
    protected void grid_wh_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox txt_JobNo = this.grid_wh.FindEditFormTemplateControl("txt_JobNo") as ASPxTextBox;
        ASPxButtonEdit btn_ClientId = this.grid_wh.FindEditFormTemplateControl("btn_ClientId") as ASPxButtonEdit;
        ASPxComboBox cbb_JobType = this.grid_wh.FindEditFormTemplateControl("cbb_JobType") as ASPxComboBox;
        if (cbb_JobType.Text == "IMP" || cbb_JobType.Text == "WGR")
        {
            e.NewValues["CargoType"] = "IN";
            e.NewValues["OpsType"] = "Storage";
        }
        e.NewValues["RefNo"] = " ";
        e.NewValues["Qty"] = 0;
        e.NewValues["ContNo"] = "";
        e.NewValues["ClientId"] = btn_ClientId.Text;
        e.NewValues["JobNo"] = txt_JobNo.Text;
        e.NewValues["JobType"] = SafeValue.SafeString(cbb_JobType.Value);
        e.NewValues["UomCode"] = " ";
        e.NewValues["PackTypeOrig"] = " ";
        e.NewValues["LandStatus"] = "Normal";
        e.NewValues["DgClass"] = "Normal";
        e.NewValues["CargoStatus"] = "P";
        e.NewValues["DamagedStatus"] = "Normal";
        e.NewValues["PackUom"] = " ";
        e.NewValues["SkuCode"] = "GENERAL";
        e.NewValues["Location"] = "HOLDING";
    }
    protected void grid_wh_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        if (SafeValue.SafeString(e.NewValues["BookingNo"]) == "")
        {
            throw new Exception("Pls enter the Lot No");
        }
        e.NewValues["RefNo"] = SafeValue.SafeString(e.NewValues["RefNo"]);
        if (SafeValue.SafeDecimal(e.NewValues["LengthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["WidthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["HeightPack"]) == 0)
        {
            e.NewValues["VolumeOrig"] = SafeValue.SafeDecimal(e.NewValues["VolumeOrig"]);
        }
        else
        {
            e.NewValues["VolumeOrig"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["LengthPack"]) * SafeValue.SafeDecimal(e.NewValues["WidthPack"]) * SafeValue.SafeDecimal(e.NewValues["HeightPack"]), 3);
        }
        e.NewValues["CargoStatus"] = SafeValue.SafeString(e.NewValues["CargoStatus"]);
        e.NewValues["LandStatus"] = SafeValue.SafeString(e.NewValues["LandStatus"]);
        e.NewValues["DgClass"] = SafeValue.SafeString(e.NewValues["DgClass"]);
        e.NewValues["DamagedStatus"] = SafeValue.SafeString(e.NewValues["DamagedStatus"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["HblNo"] = SafeValue.SafeString(e.NewValues["HblNo"]);
        e.NewValues["BookingNo"] = SafeValue.SafeString(e.NewValues["BookingNo"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        e.NewValues["OpsType"] = SafeValue.SafeString(e.NewValues["OpsType"]);
        e.NewValues["UomCode"] = SafeValue.SafeString(e.NewValues["UomCode"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["SkuCode"] = SafeValue.SafeString(e.NewValues["SkuCode"]);
        e.NewValues["QtyOrig"] = SafeValue.SafeDecimal(e.NewValues["QtyOrig"]);
        e.NewValues["PackTypeOrig"] = SafeValue.SafeString(e.NewValues["PackTypeOrig"]);
        e.NewValues["WeightOrig"] = SafeValue.SafeDecimal(e.NewValues["WeightOrig"]);
        e.NewValues["PackQty"] = SafeValue.SafeDecimal(e.NewValues["PackQty"]);
        e.NewValues["TripId"] = SafeValue.SafeInt(e.NewValues["TripId"], 0);
        e.NewValues["TripIndex"] = SafeValue.SafeString(e.NewValues["TripIndex"]);

    }
    protected void grid_wh_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeDecimal(e.NewValues["LengthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["WidthPack"]) == 0 && SafeValue.SafeDecimal(e.NewValues["HeightPack"]) == 0)
        {
            e.NewValues["VolumeOrig"] = SafeValue.SafeDecimal(e.NewValues["VolumeOrig"]);
        }
        else
        {
            e.NewValues["VolumeOrig"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["LengthPack"]) * SafeValue.SafeDecimal(e.NewValues["WidthPack"]) * SafeValue.SafeDecimal(e.NewValues["HeightPack"]), 3);
        }
        if (SafeValue.SafeString(e.NewValues["BookingNo"]) == "")
        {
            throw new Exception("Pls enter the Lot No");
        }
        e.NewValues["LandStatus"] = SafeValue.SafeString(e.NewValues["LandStatus"]);
        e.NewValues["DgClass"] = SafeValue.SafeString(e.NewValues["DgClass"]);
        e.NewValues["DamagedStatus"] = SafeValue.SafeString(e.NewValues["DamagedStatus"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["HblNo"] = SafeValue.SafeString(e.NewValues["HblNo"]);
        e.NewValues["BookingNo"] = SafeValue.SafeString(e.NewValues["BookingNo"]);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(e.NewValues["Qty"], 0);
        e.NewValues["OpsType"] = SafeValue.SafeString(e.NewValues["OpsType"]);
        e.NewValues["UomCode"] = SafeValue.SafeString(e.NewValues["UomCode"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"], 0);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"], 0);
        e.NewValues["SkuCode"] = SafeValue.SafeString(e.NewValues["SkuCode"], "");
        e.NewValues["QtyOrig"] = SafeValue.SafeDecimal(e.NewValues["QtyOrig"], SafeValue.SafeDecimal(0));
        e.NewValues["PackTypeOrig"] = SafeValue.SafeString(e.NewValues["PackTypeOrig"], "");
        e.NewValues["WeightOrig"] = SafeValue.SafeDecimal(e.NewValues["WeightOrig"], 0);
        e.NewValues["PackQty"] = SafeValue.SafeDecimal(e.NewValues["PackQty"], 0);
        e.NewValues["BkgSkuQty"] = SafeValue.SafeDecimal(e.NewValues["BkgSkuQty"], 0);
        e.NewValues["TripId"] = SafeValue.SafeInt(e.NewValues["TripId"], 0);
        e.NewValues["TripIndex"] = SafeValue.SafeString(e.NewValues["TripIndex"]);
    }
    protected void grid_wh_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    public string FilePath(int id)
    {
        string sql = string.Format("select top 1 FilePath from CTM_Attachment where JobNo='{0}'", id);
        return SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
    }
    protected void grid_wh_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("Uploadline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                ASPxTextBox txt_ContNo = grid.FindRowCellTemplateControl(rowIndex, null, "txt_ContNo") as ASPxTextBox;
                e.Result = txt_Id.Text + "_" + txt_ContNo.Text;
            }
            if (ar[0].Equals("Dimensionline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                e.Result = txt_Id.Text;
            }
            if (ar[0].Equals("Copyonline"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + txt_Id.Text + "'");
                C2.JobHouse job = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
                if (job != null)
                {
                    job.SkuCode = job.BkgSKuCode;
                    job.PackQty = job.BkgSkuQty;
                    job.PackUom = job.BkgSkuUnit;
                    job.WeightOrig = job.Weight;
                    job.VolumeOrig = job.Volume;
                    C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(job);

                    e.Result = "Success";
                }
            }
            if (ar[0].Equals("CopyonCargoline"))
            {

                ASPxGridView grid = sender as ASPxGridView;
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                ASPxComboBox cbb_CargoCount = grid.FindRowCellTemplateControl(rowIndex, null, "cbb_CargoCount") as ASPxComboBox;
                Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + txt_Id.Text + "'");
                C2.JobHouse job = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
                if (job != null)
                {
                    int count = SafeValue.SafeInt(cbb_CargoCount.Value, 0);
                    for (int i = 0; i < count; i++)
                    {
                        C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(job);

                        job.LineId = job.Id;
                        C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                        C2.Manager.ORManager.PersistChanges(job);

                        Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(C2.Dimension), "HouseId='" + txt_Id.Text + "'");
                        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query1);
                        for (int j = 0; j < objSet.Count; j++)
                        {
                            C2.Dimension d = objSet[i] as C2.Dimension;
                            d.HouseId = job.Id;

                            C2.Manager.ORManager.StartTracking(d, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(d);

                        }
                    }
                    e.Result = "Success";
                }
            }
        }
    }
    protected void grid_wh_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        string sql = string.Format(@"select top 1 Id from job_house order by Id desc");
        int id = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);

        sql = string.Format(@"update job_house set LineId={0} where Id={0}", id);
        ConnectSql.ExecuteSql(sql);
    }
    #endregion
}