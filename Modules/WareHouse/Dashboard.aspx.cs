using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
public partial class Modules_WareHouse_Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Container
        string sql = string.Format(@"select d.*,dbo.fun_GetPartyName(mast.PartyId) as PartyName,mast.PartyId,mast.CustomerReference,mast.CustomerDate 
        ,mast.DoDate,mast.DoType from Wh_DoDet3 d inner join Wh_do mast on d.DoNo=mast.DoNo");
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();


        //Transport
        sql = "select *,dbo.fun_GetPartyName(PartyId) as PartyName from Wh_DO d where UseTransport='Yes'";
        tab = ConnectSql.GetTab(sql);
        this.grid1.DataSource = tab;
        this.grid1.DataBind();


        //Warehouse
        sql = "select * from ref_warehouse ";
        tab = ConnectSql.GetTab(sql);
        this.grid2.DataSource = tab;
        this.grid2.DataBind();
    }

    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "InTransit")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }
        return color;
    }
    protected void grid_Location_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox txt_Code = this.grid2.FindDetailRowTemplateControl(0,"txt_Code") as ASPxTextBox;
        this.dsRefLocation.FilterExpression = "LocLevel='Unit' and WarehouseCode='" + txt_Code.Text + "'";
    }
    public string GetPacking(string loc)
    {
        string str = "";
        string sql = string.Format(@"select Packing,Product,LotNo,PalletNo,det.Remark from wh_dodet2 det inner join wh_do mast on det.DoNo=mast.DoNo and det.DoType='IN' and mast.StatusCode='CLS' where Location='{0}'", loc);
        DataTable tab = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (i < tab.Rows.Count - 1)
            {
                str +="【"+ SafeValue.SafeString(tab.Rows[i]["Product"])+":"+SafeValue.SafeString(tab.Rows[i]["Packing"]) + "】";
            }
            else
            {
                str += "【" + SafeValue.SafeString(tab.Rows[i]["Product"]) + ":" + SafeValue.SafeString(tab.Rows[i]["Packing"]) + "】";
            }

        }
        return str;
    }
    public string Percentage(string loc)
    {
        string str = "";
        string sql = string.Format(@"select LengthPacking,WidthPacking from wh_dodet2 det inner join wh_do mast on det.DoNo=mast.DoNo and det.DoType='IN' and mast.StatusCode='CLS'
left join ref_product p on p.Code=det.Product
where Location='{0}'", loc);
        decimal length = 0;
        decimal width = 0;
        decimal locationArea = 0;
        decimal area = 0;
        DataTable tab=ConnectSql.GetTab(sql);

        sql=string.Format(@"select sum(length*width) as Area from ref_location where Code='{0}' and  Loclevel='Unit'",loc);
        locationArea = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
        if (tab.Rows.Count > 0)
        {
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                length = SafeValue.SafeDecimal(tab.Rows[i]["LengthPacking"]);
                width = SafeValue.SafeDecimal(tab.Rows[i]["WidthPacking"]);

                area += (length * width);
            }
            double per = (double)(area / locationArea) * 100;
            str = SafeValue.SafeString(per);
        }
        else {
            str = SafeValue.SafeString(100);
        }


        return str;
    }
}