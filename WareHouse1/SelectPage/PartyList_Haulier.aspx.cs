﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class PagesFreight_SelectPage_PartyList_Haulier : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT PartyId, Code, Name,CrNo,Contact1,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as PartyName FROM  XXParty where IsVendor='True'";
        if (name.Length > 0)
        {
            sql += string.Format(" and  Name Like '{0}%'", name);
        }
        sql += " ORDER BY Code ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}