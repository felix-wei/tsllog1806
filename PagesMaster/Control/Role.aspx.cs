using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class Role : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Role));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Description"] = " ";
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        //e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        //e.NewValues["UpdateDateTime"] = DateTime.Now;
        //e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        //e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        //e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }

    protected void ASPxGridView1_Inserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        //if (e.Exception == null)
        //{
        //    string role = e.NewValues["Code"].ToString();
        //    InsertMenu(role);
        //}
    }
    protected void ASPxGridView1_Deleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        //if (e.Exception == null)
        //{
        //    string role = e.Values["Code"].ToString();
        //    DeleteMenu(role);
        //}
    }
    private void InsertMenu(string roleName)
    {
        string sql = "SELECT MasterId, Name, Note, Color, IsActive, SortIndex, Img FROM MenuBasicMast order by SortIndex";
        DataTable mast = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < mast.Rows.Count; i++)
        {
            string masterId = SafeValue.SafeString(mast.Rows[i]["MasterId"]);
            string name = SafeValue.SafeString(mast.Rows[i]["Name"]);
            string note = SafeValue.SafeString(mast.Rows[i]["Note"]);
            string color = SafeValue.SafeString(mast.Rows[i]["Color"]);
            string isActive = SafeValue.SafeString(mast.Rows[i]["IsActive"]);
            string img = SafeValue.SafeString(mast.Rows[i]["Img"]);
            string sortIndex = SafeValue.SafeString(mast.Rows[i]["SortIndex"]);
            string masterStr = roleName + masterId;
            bool flag = InsertMast(masterStr, name, note, color, isActive, sortIndex, img, roleName);
            if (flag)
            {
                string sql1 = string.Format("SELECT MasterId, Name, Note, Color, IsActive, Link, LinkType, SortIndex FROM MenuBasicSub where MasterId='{0}' order by SortIndex", masterId);
                DataTable sub = C2.Manager.ORManager.GetDataSet(sql1).Tables[0];
                for (int j = 0; j < sub.Rows.Count; j++)
                {
                    string sub_name = SafeValue.SafeString(sub.Rows[j]["Name"]);
                    string sub_note = SafeValue.SafeString(sub.Rows[j]["Note"]);
                    string sub_color = SafeValue.SafeString(sub.Rows[j]["Color"]);
                    string sub_isActive = SafeValue.SafeString(sub.Rows[j]["IsActive"]);
                    string sub_link = SafeValue.SafeString(sub.Rows[j]["Link"]);
                    string sub_linkType = SafeValue.SafeString(sub.Rows[j]["LinkType"]);
                    string sub_sortIndex = SafeValue.SafeString(sub.Rows[j]["SortIndex"]);
                    InsertSub(masterStr, sub_name, sub_note, sub_color, sub_isActive, sub_link, sub_linkType, sub_sortIndex);
                }
            }
        }

    }
    private bool InsertMast(string maserId, string name, string note, string color, string isActive, string sortIndex, string img, string roleName)
    {
        string sql = string.Format("Insert Into MenuMast (MasterId, Name, Note, Color, IsActive, SortIndex, Img,RoleName) Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
            maserId, name, note, color, isActive, sortIndex, img, roleName);
        int flag = C2.Manager.ORManager.ExecuteCommand(sql);
        if (flag > 0)
            return true;
        else return false;
    }
    private bool InsertSub(string maserId, string name, string note, string color, string isActive, string link, string linkType, string sortIndex)
    {
        string sql = string.Format("Insert Into MenuSub (MasterId, Name, Note, Color, IsActive, Link, LinkType, SortIndex) Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
            maserId, name, note, color, isActive, link, linkType, sortIndex);
        int flag = C2.Manager.ORManager.ExecuteCommand(sql);
        if (flag > 0)
            return true;
        else return false;
    }
    private void DeleteMenu(string roleName)
    {
        string sql = string.Format("SELECT MasterId FROM  MenuMast where RoleName='{0}' order by SortIndex", roleName);
        DataTable mast = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < mast.Rows.Count; i++)
        {
            string masterId = SafeValue.SafeString(mast.Rows[i]["MasterId"]);
            string sql1 = string.Format("delete from  MenuMast where MasterId='{0}' ", masterId);
            string sql2 = string.Format("delete from  MenuSub where MasterId='{0}' ", masterId);
            C2.Manager.ORManager.ExecuteCommand(sql1);
            C2.Manager.ORManager.ExecuteCommand(sql2);
        }
    }
}
