using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using Wilson.ORMapper;
using System.IO;
using System.Net;
using DevExpress.Web.ASPxEditors;

public partial class MastData_CurrencyRate : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Request.Params["currencyId"] != null)
        {
            Session["CurrencyRate"] = "FromCurrencyId='" + Request.Params["currencyId"] + "'";
        }
        if (Session["CurrencyRate"] != null)
            this.dsCurrencyRate.FilterExpression = Session["CurrencyRate"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CurrencyRate));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ToCurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate1"] = 0;
        e.NewValues["ExRate2"] = 0;
        e.NewValues["ExRate3"] = 0;
        e.NewValues["ExRateDate"] = DateTime.Now;
        if (Request.Params["currencyId"] != null)
        {
                Stream objStream;
                StreamReader objSR;
                System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

                string str = "http://download.finance.yahoo.com/d/quotes.csv?s=" + Request.Params["currencyId"] + System.Configuration.ConfigurationManager.AppSettings["Currency"] + "=X&f=l1";
                HttpWebRequest wrquest = (HttpWebRequest)WebRequest.Create(str);
                HttpWebResponse getresponse = null;
                getresponse = (HttpWebResponse)wrquest.GetResponse();

                objStream = getresponse.GetResponseStream();
                objSR = new StreamReader(objStream, encode, true);
                string strResponse = objSR.ReadToEnd();
                e.NewValues["FromCurrencyId"] = Request.Params["currencyId"];
                e.NewValues["ExRate1"] = SafeValue.SafeDecimal(strResponse, 0);
                e.NewValues["ExRate2"] = SafeValue.SafeDecimal(strResponse, 0);
                e.NewValues["ExRate3"] = SafeValue.SafeDecimal(strResponse, 0);
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("CurrencyRate", true);
    }


    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["FromCurrencyId"]).Length < 1)
            throw new Exception("Pls select FromCurrency !!!");
        if (SafeValue.SafeString(e.NewValues["ToCurrencyId"]).Length < 1)
            throw new Exception("Pls select ToCurrency !!!");
        e.NewValues["ExRate1"] = SafeValue.SafeDecimal(e.NewValues["ExRate1"], 0);
        e.NewValues["ExRate2"] = SafeValue.SafeDecimal(e.NewValues["ExRate2"], 0);
        e.NewValues["ExRate3"] = SafeValue.SafeDecimal(e.NewValues["ExRate3"], 0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["ExRateDate"] = SafeValue.SafeDate(e.NewValues["ExRateDate"], new DateTime(1753, 01, 01));
        e.NewValues["ExRateDate2"] = SafeValue.SafeDate(e.NewValues["ExRateDate2"], new DateTime(1753, 01, 01));
    }
    protected void cmb_FromCurrency_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
            ASPxComboBox cmb = sender as ASPxComboBox;

        object[] yahooRate = new object[cmb.Items.Count];
        string sql = "";
        //ASPxComboBox _fromCurrency = ASPxGridView1.FindEditRowCellTemplateControl(null, "cmb_FromCurrency") as ASPxComboBox;
        //ASPxComboBox _toCurrency = ASPxGridView1.FindEditRowCellTemplateControl(null, "cmb_ToCurrency") as ASPxComboBox;
        ASPxDateEdit _exRateDate = ASPxGridView1.FindEditRowCellTemplateControl(null, "Date_ExRateDate") as ASPxDateEdit;
        //if (_exRateDate != null && SafeValue.SafeDate(_exRateDate.Date, new DateTime(1753, 01, 01)).ToString("yyyy-MM-dd") == DateTime.Today.ToString("yyyy-MM-dd") && _fromCurrency != null && _toCurrency != null && SafeValue.SafeString(_fromCurrency.Text).Length == 3 && SafeValue.SafeString(_toCurrency.Text).Length == 3)
        //{
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                Stream objStream;
                StreamReader objSR;
                System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

                string str = "http://download.finance.yahoo.com/d/quotes.csv?s=" + cmb.Items[i].Text + System.Configuration.ConfigurationManager.AppSettings["Currency"] + "=X&f=l1";
                HttpWebRequest wrquest = (HttpWebRequest)WebRequest.Create(str);
                HttpWebResponse getresponse = null;
                getresponse = (HttpWebResponse)wrquest.GetResponse();

                objStream = getresponse.GetResponseStream();
                objSR = new StreamReader(objStream, encode, true);
                string strResponse = objSR.ReadToEnd();
                yahooRate[i] = SafeValue.SafeDecimal(strResponse, 0);

            }
        //}
        //else
        //{

        //    for (int i = 0; i < cmb.Items.Count; i++)
        //    {
        //        yahooRate[i] = 0;
        //    }
        //}
            e.Properties["cpYahooRate"] = yahooRate;
    }
}
