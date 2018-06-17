﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class PagesContTrucking_SelectPage_SelectWarehouse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT Id,Code,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as WhName,IsBonded FROM ref_warehouse where ";
        if (name.Length > 0)
        {
            sql += string.Format("  Name Like '{0}%' ", name);
        }
        else
        {
            sql += "1=1";
        }
        sql += " ORDER BY Code ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    public string ShowBgColor(string status)
    {
        string color = "white";
        if (status == "Bonded")
        {
            color = "red";
        }
        return color;
    }
    public string ShowFontColor(string status)
    {
        string color = "black";
        if (status == "Bonded")
        {
            color = "white";
        }

        return color;
    }
}