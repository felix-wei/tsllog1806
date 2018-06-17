﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_LocationList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            btn_Sch_Click(null,null);
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT Id,Code,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as LocationName,WarehouseCode FROM ref_location ";
        string loclevel = SafeValue.SafeString(Request.QueryString["loclevel"]).ToUpper();
        string wh = SafeValue.SafeString(Request.QueryString["wh"]);
        string zone = SafeValue.SafeString(Request.QueryString["zone"]);
        string where = "";
        if (loclevel.Length == 0)
        {
            sql += "1=1";
        }
        else
        {

            if (wh.Length > 0)
            {
                where = GetWhere(where, "WarehouseCode='" + wh + "'");
            }
            if (zone.Length > 1)
            {
                where = GetWhere(where, "ZoneCode='" + zone + "'");
            }
            if (loclevel.IndexOf("Z") != -1)
            {
                where = GetWhere(where, " Loclevel='Zone'");
            }
            if (loclevel.IndexOf("S") != -1)
            {
                where = GetWhere(where, " Loclevel='Store'");
            }
            if (loclevel.IndexOf("U") != -1)
            {
                where = GetWhere(where,  "Loclevel='Unit'");
            }
            if (where.Length > 1)
                sql +="where "+ where;
        }
        if (name.Length > 0)
        {
            sql += string.Format(" and WarehouseCode=(select Code from ref_warehouse where Name like '{0}%')", name.Replace("'", "''"));
        }
        sql += " ORDER BY WarehouseCode ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}