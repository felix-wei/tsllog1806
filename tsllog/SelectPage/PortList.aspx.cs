﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class SelectPage_PortList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
            string type = SafeValue.SafeString(Request.QueryString["type"]).ToUpper();
            if (type.Length>0)
                this.lab_Type.Text = type;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string code = this.txt_Code.Text.Trim().ToUpper();
        string sql = "";
        string type = this.lab_Type.Text.Trim();
        if(type=="S"){
            sql ="select Code,Name from XXPort where Code<>''";
        }
        if(type=="A")
        {
            sql = "select AirCode as Code,Name from XXPort where AirCode<>''";
        }
        if (code.Length > 0)
        {
            sql += string.Format(" and Code Like '{0}%'", code);
        }
        else
        {
            sql += string.Format(" and Name Like '{0}%'", name);
        }

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}
