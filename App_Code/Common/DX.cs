using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

/// <summary>
/// Summary description for DX
/// </summary>
public class DX
{
    public DX()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DateTime GetDate(ASPxGridView from,string keyId)
    {
        DateTime res = new DateTime(1900, 1, 1);
        if (from != null)
        {
            ASPxDateEdit cc = null;
            try
            {
                cc = (ASPxDateEdit)from.FindControl(keyId);
            }
            catch { }
            if (cc != null)
            {
                res = cc.Date;
            }else
            {
                throw new Exception(string.Format("It isnot ASPxDateEdit.[{0}]", keyId));
            }
        }
        return res;
    }

    public static string GetText(ASPxGridView from,string keyId)
    {
        string res = string.Empty;
        if (from != null)
        {
            ASPxTextBoxBase cc = null;
            try
            {
                cc = (ASPxTextBoxBase)from.FindControl(keyId);
            }
            catch { }
            if (cc != null)
            {
                res = cc.Text;
            }
            else
            {
                throw new Exception(string.Format("It isnot ASPxTextBoxBase.[{0}]", keyId));
            }
        }
        return res;
    }
    public static string GetCombo(ASPxGridView from, string keyId)
    {
        return GetDropDown(from,keyId);
    }
    public static string GetDropDown(ASPxGridView from, string keyId)
    {
        string res = string.Empty;
        if (from != null)
        {
            ASPxDropDownEditBase cc = null;
            try
            {
                cc = (ASPxDropDownEditBase)from.FindControl(keyId);
            }
            catch { }
            if (cc != null)
            {
                res = cc.Value.ToString();
            }
            else
            {
                throw new Exception(string.Format("It isnot ASPxTextBoxBase.[{0}]", keyId));
            }
        }
        return res;
    }
}