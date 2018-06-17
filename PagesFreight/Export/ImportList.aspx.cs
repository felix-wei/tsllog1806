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
using DevExpress.Web.ASPxDataView;
using Wilson.ORMapper;

public partial class Pages_Export_ImportList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }

        OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (this.txt_HouseNo.Text.Length > 0)
        {
            where = "JobNo='" + this.txt_HouseNo.Text.Trim() + "'";
        }
        else if (this.txt_HblN.Text.Length > 0)
        {
            where = "HblNo='" + this.txt_HblN.Text.Trim() + "'";
        }
 
        if (where.Length > 0)
        {
            string port = SafeValue.SafeString(Request.QueryString["port"], "");
            where += " and TsInd='Y' and isnull(TsJobNo,'') = '' and TsPod='" + port + "'";
            this.dsExport.FilterExpression = where;
        }
        else
        {
            string port = SafeValue.SafeString(Request.QueryString["port"], "");
            where = " TsInd='Y' and isnull(TsJobNo,'') = '' and TsPod='" + port + "'";
            this.dsExport.FilterExpression = where;
        }

    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string impNo = e.Parameters;
        e.Result=Tranship(impNo);
    }

    #region TRANSHIPMENT
    private string Tranship(string impNo)
    {
        string user = HttpContext.Current.User.Identity.Name;
        //string pod = tPod.Text;
        string refN = SafeValue.SafeString(Request.QueryString["refNo"]);
        string sql = string.Format("select Pol,Pod,Vessel, Voyage, Eta, Etd, EtaDest from SeaExportRef where RefNo='{0}'", refN);
        DataTable tab_expRef = ConnectSql.GetTab(sql);
        if (tab_expRef.Rows.Count == 0) return "Error";
        string pod = SafeValue.SafeString(tab_expRef.Rows[0]["Pod"]);
        string pol = SafeValue.SafeString(tab_expRef.Rows[0]["Pol"]);
        string polName = EzshipHelper.GetPortName(pol, "SINGAPORE");
        string podName = EzshipHelper.GetPortName(pod, "SINGAPORE");
        SeaExport exp = new SeaExport();
        #region export info

        exp.AsAgent = "N";

        string bkgNo = C2Setup.GetNextNo("ExportBooking");
        string bkgNPrefix = "SIN";
        if (pod.Length == 5)
            bkgNPrefix += pod.Substring(2);
        exp.BkgRefNo = bkgNPrefix + bkgNo;
        exp.CollectFrom = "";
        exp.CreateBy = EzshipHelper.GetUserName();
        exp.CreateDateTime = DateTime.Now;
        exp.CustomerId = "";
        exp.ExpressBl = "N";

        exp.FrtTerm = "FP";
        exp.HaulierAttention = "";
        exp.HaulierCollect = "";
        exp.HaulierCrNo = "";
        exp.HaulierName = "";
        exp.HaulierRemark = "";
        exp.HaulierTruck = "";
        exp.HblNo = "";
        exp.ImpCharge = 0;
        exp.JobNo = C2Setup.GetSubNo(refN, "SE");
        exp.Marking = "";
        exp.PermitRmk = "";
        exp.PlaceDeliveryId = pod;
        exp.PlaceDeliveryName = podName;
        exp.PlaceDeliveryTerm = "CFS";
        exp.PlaceDischargeName = podName;
        exp.PlaceLoadingName = polName;
        exp.PlaceReceiveId = pol;
        exp.PlaceReceiveName = polName;
        exp.PlaceReceiveTerm = "CFS";
        exp.Pod = pod;
        exp.Pol = pol;
        exp.PreCarriage = "";
        exp.RefNo = refN;
        exp.Remark = "";
        exp.SAgentRemark = "";
        exp.SConsigneeRemark = "";
        exp.ShipLoadInd = "N";
        exp.ShipOnBoardDate = DateTime.Today;
        exp.ShipOnBoardInd = "N";
        exp.ShipperContact = "";
        exp.ShipperEmail = "";
        exp.ShipperFax = "";
        exp.ShipperId = "";
        exp.ShipperName = "";
        exp.ShipperTel = "";
        exp.SNotifyPartyRemark = "";
        exp.SShipperRemark = "";
        exp.StatusCode = "USE";
        exp.SurrenderBl = "N";
        exp.TsInd = "Y";
        exp.TsJobNo = impNo;
        exp.UpdateBy = EzshipHelper.GetUserName();
        exp.UpdateDateTime = DateTime.Now;




        string finDest = pod;
        decimal wt = 0;
        decimal m3 = 0;
        int qty = 0;
        string pkgType = "";

        sql = string.Format(@"SELECT TsPod, TsPortFinName, TsVessel, TsVoyage, TsColoader, TsEtd, TsEta, TsAgentId, TsRemark, TsAgtRate, TsTotAgtRate, TsImpRate, 
                      TsTotImpRate,Weight,Volume,Qty,PackageType FROM SeaImport where JobNo='{0}'", impNo);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count > 0)
        {
            DataRow row = tab.Rows[0];
            pod = row["TsPod"].ToString();
            finDest = row["TsPortFinName"].ToString();
            wt = SafeValue.SafeDecimal(row["Weight"], 0);
            m3 = SafeValue.SafeDecimal(row["Volume"], 0);
            qty = SafeValue.SafeInt(row["Qty"], 0);
            pkgType = row["PackageType"].ToString();
        }
        exp.FinDest = finDest;



        exp.Weight = wt;
        exp.Volume = m3;
        exp.Qty = qty;
        exp.PackageType = pkgType;
        #endregion

        C2.Manager.ORManager.StartTracking(exp, Wilson.ORMapper.InitialState.Inserted);
        C2.Manager.ORManager.PersistChanges(exp);
        C2Setup.SetNextNo("ExportBooking", bkgNo);
        //create bkg
        #region booking and marking
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(SeaImportMkg), "JobNo='" + exp.TsJobNo + "'");
        ObjectSet set_impMkgs = C2.Manager.ORManager.GetObjectSet(query);
        for (int m = 0; m < set_impMkgs.Count; m++)
        {
            C2.SeaImportMkg impMkg = set_impMkgs[m] as C2.SeaImportMkg;
            C2.SeaExportMkg mkg_bkg = new SeaExportMkg();
            mkg_bkg.ContainerNo = "";
            mkg_bkg.ContainerType = "";
            mkg_bkg.Description = impMkg.Description;
            mkg_bkg.CreateDateTime = DateTime.Now;
            mkg_bkg.JobNo = exp.JobNo;
            mkg_bkg.Marking = impMkg.Marking;
            mkg_bkg.MkgType = "BKG";
            mkg_bkg.PackageType = "";
            mkg_bkg.Qty = impMkg.Qty;
            mkg_bkg.RefNo = exp.RefNo;
            mkg_bkg.Remark = impMkg.Remark;
            mkg_bkg.SealNo = "";
            mkg_bkg.CreateBy = EzshipHelper.GetUserName();
            mkg_bkg.Volume = impMkg.Volume;
            mkg_bkg.Weight = impMkg.Weight;
            C2.Manager.ORManager.StartTracking(mkg_bkg, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(mkg_bkg);

            C2.SeaExportMkg mkg = new SeaExportMkg();
            mkg.ContainerNo = "";
            mkg.ContainerType = "";
            mkg.Description = impMkg.Description;
            mkg.CreateDateTime = DateTime.Now;
            mkg.JobNo = exp.JobNo;
            mkg.Marking = impMkg.Marking;
            mkg.MkgType = "BL";
            mkg.PackageType = "";
            mkg.Qty = impMkg.Qty;
            mkg.RefNo = exp.RefNo;
            mkg.Remark = impMkg.Remark;
            mkg.SealNo = "";
            mkg.CreateBy = EzshipHelper.GetUserName();
            mkg.Volume = impMkg.Volume;
            mkg.Weight = impMkg.Weight;
            C2.Manager.ORManager.StartTracking(mkg, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(mkg);
        }

        #endregion

        //update import info

        DataRow row1 = tab_expRef.Rows[0];
        string ves = row1["Vessel"].ToString();
        string voy = row1["Voyage"].ToString();
        DateTime eta = SafeValue.SafeDate(row1["Eta"], new DateTime(1900, 1, 1));
        DateTime etd = SafeValue.SafeDate(row1["Etd"], new DateTime(1900, 1, 1));
        // DateTime etaDest = SafeValue.SafeDate(row["EtaDest"], new DateTime(1900, 1, 1));
        sql = string.Format("Update SeaImport Set TsVessel='{0}', TsVoyage='{1}', TsEtd='{2}', TsEta='{3}',TsSchNo='{4}', TsBkgNo='{5}', TsPortFinName='{6}',tsBkgId='{7}',TsJobNo='{9}',TsRefNo='{10}' where JobNo='{8}'",
            ves, voy, etd, eta, refN, exp.BkgRefNo, finDest, exp.SequenceId, impNo, exp.JobNo, exp.RefNo);
        int res = C2.Manager.ORManager.ExecuteCommand(sql);

        return "Success";
    }
    #endregion
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string jobId = "";
        public Record(string _jobId)
        {
            jobId = _jobId;
        }

    }
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox jobId = this.grid_Export.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxCheckBox isClt = this.grid_Export.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (jobId != null && isClt != null && isClt.Checked)
            {
                list.Add(new Record(jobId.Text));
            }
        }
    }

}
