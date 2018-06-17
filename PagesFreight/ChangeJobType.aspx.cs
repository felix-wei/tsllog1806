using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web.ASPxDataView;
using Wilson.ORMapper;

public partial class PagesFreight_ChangeJobType : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_Type.Text = "Sea";
            this.cmb_refType.Text = "Import";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_change_Click(object sender, EventArgs e)
    {
        string seaAirInd = this.cmb_Type.Text;
        string impExpCtInd = this.cmb_refType.Text;
        string refNo = this.txt_RefNo.Text;
        string newJobType = this.ASPxComboBox1.Text;
        string newRefType = "";
        string resps = "";
        if (newJobType == "")
            return;
        if (seaAirInd == "Sea" && impExpCtInd == "Import")
        {
            string sql = string.Format("select JobType from SeaImportRef where RefNo='{0}'", refNo);
            string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            if (jobType.Length == 0)
            {
                resps = string.Format("Can not find this ref {0}", refNo);

            }
            else if (jobType == newJobType)
            {
                resps = string.Format("Success, {0}  From {1} to {2}", refNo, jobType, newJobType);

            }
            else
            {
                if (newJobType == "FCL")
                    newRefType = "SIF";
                else if (newJobType == "LCL")
                    newRefType = "SIL";
                else if (newJobType == "CONSOL")
                    newRefType = "SIC";
                sql = string.Format("Update SeaImportRef set JobType='{1}',RefType='{2}' where RefNO='{0}'", refNo, newJobType, newRefType);
                int flag = C2.Manager.ORManager.ExecuteCommand(sql);
                if (flag > -1)
                    resps = string.Format("Success, {0}  From {1} to {2}", refNo, jobType, newJobType);
            }
        }
        else if (seaAirInd == "Sea" && impExpCtInd == "Export")
        {
            string sql = string.Format("select JobType from SeaExportRef where RefNo='{0}' and RefType like 'SE%'", refNo);
            string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            if (jobType.Length == 0)
            {
                resps = string.Format("Can not find this ref {0}", refNo);
            }
            else if (jobType == newJobType)
            {
                resps = string.Format("Success, {0}  From {1} to {2}", refNo, jobType, newJobType);
            }
            else
            {
                if (newJobType == "FCL")
                    newRefType = "SEF";
                else if (newJobType == "LCL")
                    newRefType = "SEL";
                else if (newJobType == "CONSOL")
                    newRefType = "SEC";
                sql = string.Format("Update SeaExportRef set JobType='{1}',RefType='{2}' where RefNO='{0}'", refNo, newJobType, newRefType);
                int flag = C2.Manager.ORManager.ExecuteCommand(sql);
                if (flag > -1)
                    resps = string.Format("Success, {0}  From {1} to {2}", refNo, jobType, newJobType);
            }
        }
        else if (seaAirInd == "Sea" && impExpCtInd == "CrossTrade")
        {
            string sql = string.Format("select JobType from SeaExportRef where RefNo='{0}' and RefType like 'SC%'", refNo);
            string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            if (jobType.Length == 0)
            {
                resps = string.Format("Can not find this ref {0}", refNo);
            }
            else if (jobType == newJobType)
            {
                resps = string.Format("Success, {0}  From {1} to {2}", refNo, jobType, newJobType);
            }
            else
            {
                if (newJobType == "FCL")
                    newRefType = "SCF";
                else if (newJobType == "LCL")
                    newRefType = "SCL";
                else if (newJobType == "CONSOL")
                    newRefType = "SCC";
                sql = string.Format("Update SeaeXportRef set JobType='{1}',RefType='{2}' where RefNO='{0}'", refNo, newJobType, newRefType);
                int flag = C2.Manager.ORManager.ExecuteCommand(sql);
                if (flag > -1)
                    resps = string.Format("Success, {0}  From {1} to {2}", refNo, jobType, newJobType);
            }
        }

        if (resps.Length > 0)
        {
            string script = string.Format("<script type='text/javascript' >alert('{0}');</script>", resps);
            Response.Clear();
            Response.Write(script);
        }

    }
}