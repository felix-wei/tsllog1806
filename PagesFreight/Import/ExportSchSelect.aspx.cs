using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class Pages_Import_ExportSchSelect : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["port"] != null)
            {
                string portId = Request.QueryString["port"].ToString();
                this.txt_Pod.Text = portId;
                dsExportRef.FilterExpression = "StatusCode='USE' and Pod='" + portId + "' and Etd >= '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                this.txt_Pod.Text = portId;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
    }
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.Length != 0)
        {
            string portId = this.txt_Pod.Text;
            dsExportRef.FilterExpression = "StatusCode='USE'  and Pod='" + portId + "' and Etd >= '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
            this.grid_Sch.DataBind();
        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["impNo"] != null)
        {
//            string refNo = e.Parameters;
//            string impNo = Request.QueryString["impNo"].ToString();
//            string pod = Request.QueryString["port"].ToString();
//            string pol = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["LocalPort"], "SGSIN");
//            string polName = EzshipHelper.GetPortName(pol, "SINGAPORE");
//            string podName = EzshipHelper.GetPortName(pod, "SINGAPORE");
//            #region create booking
//            try
//            {
//                ASPxGridView grd = sender as ASPxGridView;
//                string user = HttpContext.Current.User.Identity.Name;
//                SeaExport exp = new SeaExport();
//                string expN = C2Setup.GetSubNo(refNo,"SE");

//                exp.AsAgent = "N";

//                string bkgNo = C2Setup.GetNextNo("ExportBooking");
//                string bkgN = "SIN";
//                if (pod.Length == 5)
//                    bkgN += pod.Substring(2);
//                exp.BkgRefNo = bkgN + expN;
//                exp.CollectFrom = "";
//                exp.CreateBy = EzshipHelper.GetUserName();
//                exp.CreateDateTime = DateTime.Now;
//                exp.CustomerId = "";
//                //exp.CustomerName = "";
//                exp.ExpressBl = "N";

//                exp.FrtTerm = "FP";
//                exp.HaulierAttention = "";
//                exp.HaulierCollect = "";
//                exp.HaulierCrNo = "";
//                exp.HaulierName = "";
//                exp.HaulierRemark = "";
//                exp.HaulierTruck = "";
//                exp.HblNo = "";
//                exp.ImpCharge = 0;
//                exp.JobNo = C2Setup.GetSubNo(refNo, "SE");
//                exp.Marking = "";
//                exp.PermitRmk = "";
//                exp.PlaceDeliveryId = pod;
//                exp.PlaceDeliveryName = podName;
//                exp.PlaceDeliveryTerm = "CFS";
//                exp.PlaceDischargeName = podName;
//                exp.PlaceLoadingName = polName;
//                exp.PlaceReceiveId = pol;
//                exp.PlaceReceiveName = polName;
//                exp.PlaceReceiveTerm = "CFS";
//                exp.Pod = pod;
//                exp.Pol = pol;
//                exp.PreCarriage = "";
//                exp.RefNo = refNo;
//                exp.Remark = "";
//                exp.SAgentRemark = "";
//                exp.SConsigneeRemark = "";
//                exp.ShipLoadInd = "N";
//                exp.ShipOnBoardDate = DateTime.Today;
//                exp.ShipOnBoardInd = "N";
//                exp.ShipperContact = "";
//                exp.ShipperEmail = "";
//                exp.ShipperFax = "";
//                exp.ShipperId = "";
//                exp.ShipperName = "";
//                exp.ShipperTel = "";
//                exp.SNotifyPartyRemark = "";
//                exp.SShipperRemark = "";
//                exp.StatusCode = "USE";
//                exp.SurrenderBl = "N";
//                exp.TsInd = "Y";
//                exp.TsJobNo = impNo;
//                exp.UpdateBy = EzshipHelper.GetUserName();
//                exp.UpdateDateTime = DateTime.Now;
                
//                //exp.CancelInd = "N";
//                exp.TsInd = "Y";
//                exp.TsJobNo = impNo;



//                string finDest = pod;
//                decimal wt = 0;
//                decimal m3 = 0;
//                int qty = 0;
//                string pkgType = "";

//                string sql = string.Format(@"SELECT TsPod, TsPortFinName, TsVessel, TsVoyage, TsColoader, TsEtd, TsEta, TsAgentId, TsRemark, TsAgtRate, TsTotAgtRate, TsImpRate, 
//                      TsTotImpRate,Weight,Volume,Qty,PackageType FROM SeaImport where JobNo='{0}'", impNo);
//                DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
//                if (tab.Rows.Count > 0)
//                {
//                    DataRow row = tab.Rows[0];
//                    pod = row["TsPod"].ToString();
//                    finDest = row["TsPortFinName"].ToString();
//                    wt = SafeValue.SafeDecimal(row["Weight"], 0);
//                    m3 = SafeValue.SafeDecimal(row["Volume"], 0);
//                    qty = SafeValue.SafeInt(row["Qty"], 0);
//                    pkgType = row["PackageType"].ToString();
//                }
//                exp.FinDest = finDest;



//                exp.Weight = wt;
//                exp.Volume = m3;
//                exp.Qty = qty;
//                exp.PackageType = pkgType;

//                C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Updated);
//                C2.Manager.ORManager.PersistChanges(exp);

//                //e.Result = expSchId+";"+bkgN;
//            }
//            catch { e.Result = "Fail"; }
//            #endregion
        }
    }

}
