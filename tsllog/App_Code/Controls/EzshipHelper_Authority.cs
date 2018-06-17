using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// EzshipHelper_Authority 的摘要说明
/// </summary>
public class EzshipHelper_Authority
{
    public EzshipHelper_Authority()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region Bind_Authority for Page

    /// <summary>
    /// page的直接子控件的显示情况
    /// </summary>
    /// <param name="page"></param>
    /// <param name="Status"></param>
    /// <param name="where"></param>
    public static void Bind_Authority(Page page, string Status, string where)
    {
        string Role = EzshipHelper.GetUseRole();
        string Path = GetFrame();
        string sql_part = "";
        if (Status.Length > 0)
        {
            sql_part = " and [Status]='" + Status + "'";
        }
        sql_part += where;
        string sql = string.Format(@"select Control,ControlType,IsHid from Helper_Authority where Frame='{0}' and Role='{1}' {2}", Path, Role, sql_part);
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            string name = SafeValue.SafeString(dr["Control"]);
            string type = SafeValue.SafeString(dr["ControlType"]);
            string IsHid = SafeValue.SafeString(dr["IsHid"].ToString());
            Control c = page.FindControl(name);
            if (c != null)
            {
                Bind_Control(c, type, IsHid);
            }
        }
    }
    /// <summary>
    /// page的直接子控件的显示情况
    /// </summary>
    /// <param name="page"></param>
    /// <param name="Status"></param>
    public static void Bind_Authority(Page page, string Status)
    {
        Bind_Authority(page, Status, "");
    }
    /// <summary>
    /// page的直接子控件的显示情况
    /// </summary>
    /// <param name="page"></param>
    public static void Bind_Authority(Page page)
    {
        Bind_Authority(page, "", "");
    }
    #endregion

    #region Bind_Authority for GridView

    /// <summary>
    /// GridView的直接子控件的显示情况(GridView.Column可填Caption/FieldName)
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="Status"></param>
    /// <param name="where"></param>
    public static void Bind_Authority(ASPxGridView gv, string Status, string where)
    {
        string Role = EzshipHelper.GetUseRole();
        string Path = GetFrame();
        string sql_part = "";
        if (Status.Length > 0)
        {
            sql_part = " and [Status]='" + Status + "'";
        }
        sql_part += where;
        string sql = string.Format(@"select Control,ControlType,IsHid from Helper_Authority where Frame='{0}' and Role='{1}' {2}", Path, Role, sql_part);
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            string name = SafeValue.SafeString(dr["Control"]);
            string type = SafeValue.SafeString(dr["ControlType"]);
            string IsHid = SafeValue.SafeString(dr["IsHid"].ToString());
            if (type.Equals("Column"))
            {
                Bind_Column(gv, name, IsHid);
            }
            else
            {
                Control c = gv.FindEditFormTemplateControl(name);
                if (c != null)
                {
                    Bind_Control(c, type, IsHid);
                }
            }
        }
    }
    /// <summary>
    /// GridView的直接子控件的显示情况(GridView.Column可填Caption/FieldName)
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="Status"></param>
    public static void Bind_Authority(ASPxGridView gv, string Status)
    {
        Bind_Authority(gv, Status, "");
    }
    /// <summary>
    /// GridView的直接子控件的显示情况(GridView.Column可填Caption/FieldName)
    /// </summary>
    /// <param name="gv"></param>
    public static void Bind_Authority(ASPxGridView gv)
    {
        Bind_Authority(gv, "", "");
    }

    #endregion

    #region Bind_Authority for PageControl

    /// <summary>
    /// TabControl的直接子控件的显示情况(包含每个TabPage 和TabPage里面的控件)
    ///  注意格式：1)TabPage->ContentCollection->ContentControl->asp:Panel->具体内容 2)TabPage.Name==asp:Panel.ID (建议设置成 TabPage.Text.Repalce(" ","") )
    /// </summary>
    /// <param name="tab"></param>
    /// <param name="Status"></param>
    /// <param name="where"></param>
    public static void Bind_Authority(ASPxPageControl tab, string Status, string where)
    {
        string Role = EzshipHelper.GetUseRole();
        string Path = GetFrame();
        string sql_part = "";
        if (Status.Length > 0)
        {
            sql_part = " and [Status]='" + Status + "'";
        }
        sql_part += where;
        string sql = string.Format(@"select Control,ControlType,IsHid from Helper_Authority where Frame='{0}' and Role='{1}' {2}", Path, Role, sql_part);
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            string name = SafeValue.SafeString(dr["Control"]);
            string type = SafeValue.SafeString(dr["ControlType"]);
            string IsHid = SafeValue.SafeString(dr["IsHid"].ToString());

            if (type.Equals("TabPage"))
            {
                Bind_Control(tab, name, type, IsHid);
                //int index = tab.TabPages.IndexOfName(name);
                //if (index >= 0)
                //{
                //    TabPage c = tab.TabPages[index];
                //    if (c != null)
                //    {
                //        Bind_Control(tab, IsHid);
                //    }
                //}
            }
            else
            {
                Control c = tab.FindControl(name);
                if (c != null)
                {
                    Bind_Control(c, type, IsHid);
                }
            }
        }
        //throw new Exception(temp);
    }
    /// <summary>
    /// TabControl的直接子控件的显示情况(包含每个TabPage 和TabPage里面的控件)
    ///  注意格式：1)TabPage->ContentCollection->ContentControl->asp:Panel->具体内容 2)TabPage.Name==asp:Panel.ID (建议设置成 TabPage.Text.Repalce(" ","") )
    /// </summary>
    /// <param name="tab"></param>
    /// <param name="Status">ss</param>
    public static void Bind_Authority(ASPxPageControl tab, string Status)
    {
        Bind_Authority(tab, Status, "");
    }
    /// <summary>
    /// TabControl的直接子控件的显示情况(包含每个TabPage 和TabPage里面的控件)
    ///  注意格式：1)TabPage->ContentCollection->ContentControl->asp:Panel->具体内容 2)TabPage.Name==asp:Panel.ID (建议设置成 TabPage.Text.Repalce(" ","") )
    /// </summary>
    /// <param name="tab"></param>
    public static void Bind_Authority(ASPxPageControl tab)
    {
        Bind_Authority(tab, "", "");
    }

    #endregion

    public static string GetFrame()
    {
        string Frame = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString();
        if (Frame == null || Frame.Length == 0)
        {
            return "";
        }
        string[] ar = Frame.Split(new string[] { ".aspx" }, StringSplitOptions.RemoveEmptyEntries);
        return ar[0];
    }


    #region Bind_Control
    static void Bind_Control(Control control, string type, string IsHid)
    {
        if (IsHid.Equals("0"))
        {
            control.Visible = true;
        }
        else
        {
            if (IsHid.Equals("1"))
            {
                control.Visible = false;
            }
            else
            {
                switch (type)
                {
                    case "ASPxButton":
                        DevExpress.Web.ASPxEditors.ASPxButton ASPxButton_c = control as DevExpress.Web.ASPxEditors.ASPxButton;
                        if (IsHid.Equals("2") && ASPxButton_c != null)
                        {
                            ASPxButton_c.Enabled = false;
                        }
                        break;
                    case "ASPxPageControl":
                        DevExpress.Web.ASPxTabControl.ASPxPageControl ASPxPageControl_c = control as DevExpress.Web.ASPxTabControl.ASPxPageControl;
                        if (IsHid.Equals("2") && ASPxPageControl_c != null)
                        {
                            ASPxPageControl_c.Enabled = false;
                        }
                        break;
                    case "TabPage":
                        System.Web.UI.WebControls.Panel TabPage_c = control as System.Web.UI.WebControls.Panel;
                        if (IsHid.Equals("2") && TabPage_c != null)
                        {
                            TabPage_c.Enabled = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 控件是TabPage 引用
    /// </summary>
    /// <param name="control"></param>
    /// <param name="IsHid"></param>
    static void Bind_Control(ASPxPageControl tab, string name, string type, string IsHid)
    {
        if (IsHid.Equals("0") || IsHid.Equals("1"))
        {
            int index = tab.TabPages.IndexOfName(name);
            TabPage control = null;
            if (index >= 0)
            {
                control = tab.TabPages[index];
            }
            if (control == null) { return; }
            switch (IsHid)
            {
                case "0":
                    control.Visible = true;
                    break;
                case "1":
                    control.Visible = false;
                    break;
                default: break;
            }
        }
        else
        {

            Bind_Control(tab.FindControl(name), type, IsHid);

        }

    }

    static void Bind_Column(ASPxGridView gv, string name, string IsHid)
    {
        GridViewColumnCollection cols = gv.Columns;
        if (cols == null) { return; }
        GridViewColumn col = cols[name];
        if (col == null) { return; }
        if (IsHid.Equals("0"))
        {
            col.Visible = true;
        }
        else
        {
            col.Visible = false;
        }
    }

    #endregion
}