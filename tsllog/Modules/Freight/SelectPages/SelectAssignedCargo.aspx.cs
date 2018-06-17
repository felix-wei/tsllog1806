using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2;

public partial class Modules_Freight_SelectPages_SelectAssignedCargo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (Request.QueryString["id"] != null) {
                lbl_Id.Text = SafeValue.SafeString(Request.QueryString["id"]);
                dsWh.FilterExpression = "Id=" + lbl_Id.Text;
            }
        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string paras = e.Parameters;
        if (paras == "Assign")
        {
            ASPxSpinEdit spin_Qty = grid.FindRowCellTemplateControl(0, null, "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit spin_Weight = grid.FindRowCellTemplateControl(0, null, "spin_Weight") as ASPxSpinEdit;
            ASPxSpinEdit spin_Volume = grid.FindRowCellTemplateControl(0, null, "spin_Volume") as ASPxSpinEdit;
            ASPxDateEdit date_ShipDate = grid.FindRowCellTemplateControl(0, null, "date_ShipDate") as ASPxDateEdit;
            ASPxComboBox cbb_ShipIndex = grid.FindRowCellTemplateControl(0, null, "cbb_ShipIndex") as ASPxComboBox;
            ASPxTextBox txt_ShipPortCode = grid.FindRowCellTemplateControl(0, null, "txt_ShipPortCode") as ASPxTextBox;
            ASPxComboBox cbb_ContIndex = grid.FindRowCellTemplateControl(0, null, "cbb_ContIndex") as ASPxComboBox;

            DateTime now = DateTime.Now;
            string jobNo = "";
            string refNo = "";
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id=" + lbl_Id.Text + "");
            C2.JobHouse house = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
            if (house != null)
            {
                #region 
                refNo = house.JobNo;
                C2.CtmJob job = new CtmJob();
                jobNo = C2Setup.GetNextNo("", "CTM_Job_WDO", DateTime.Now);
                job.JobNo = jobNo;
                job.JobDate = DateTime.Now;
                job.ClientId = house.ConsigneeInfo;
                job.DeliveryTo = "";
                C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(job);
                C2Setup.SetNextNo("", "CTM_Job_WDO", jobNo, now);

                house.LineId =SafeValue.SafeInt(lbl_Id.Text,0);
                job.JobType = "WDO";
                house.JobNo = jobNo;
                house.RefNo = refNo;
                house.CargoType = "OUT";
                house.CargoStatus = "P";
                house.QtyOrig = SafeValue.SafeDecimal(spin_Qty.Value);
                house.WeightOrig = SafeValue.SafeDecimal(spin_Weight.Value);
                house.VolumeOrig = SafeValue.SafeDecimal(spin_Volume.Value);
                house.ShipDate = date_ShipDate.Date;
                house.ShipIndex =SafeValue.SafeInt(cbb_ShipIndex.Value,0);
                house.ShipPortCode = txt_ShipPortCode.Text;
                house.ContIndex = SafeValue.SafeString(cbb_ContIndex.Value);
                C2.Manager.ORManager.StartTracking(house, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(house);

                #endregion
            }
            e.Result = "Action Success! No is " + jobNo;
        }
    }
}