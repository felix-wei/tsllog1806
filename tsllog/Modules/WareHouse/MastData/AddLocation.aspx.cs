using C2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_WareHouse_MastData_AddLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmb_WareHouse.Value != null)
            {
                int column = SafeValue.SafeInt(Column.Value, 0);
                int level = SafeValue.SafeInt(Level.Value, 0);
                string rack = Rack.Value;

                if (column > 0)
                {
                    string zone = rack + "00";
                    #region Zone
                    RefLocation obj = new RefLocation();
                    obj.Name = "RACK:" + rack;
                    obj.Code = zone;
                    obj.WarehouseCode = SafeValue.SafeString(cmb_WareHouse.Value);
                    obj.ZoneCode = "";
                    obj.PartyId = "";
                    obj.StoreCode = "";
                    obj.Length = 0;
                    obj.Width = 0;
                    obj.Height = 0;
                    obj.SpaceM3 = 0;
                    obj.Remark = "";
                    obj.Loclevel = "Zone";

                    obj.CreateBy = HttpContext.Current.User.Identity.Name;
                    obj.CreateDateTime = DateTime.Now;
                    obj.UpdateBy = HttpContext.Current.User.Identity.Name;
                    obj.UpdateDateTime = DateTime.Now;
                    Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(obj);
                    #endregion
                    for (int i = 1; i <= column; i++)
                    {
                        #region Location
                        string loc = "";
                        if (i < 10)
                        {
                            zone = rack + "0" + i;
                        }
                        else
                        {
                            zone = rack + i;
                        }
                        if (level > 0)
                        {
                            for (int j = 1; j <= level; j++)
                            {
                                if (j > 0 && j < 10)
                                {
                                    loc = zone + "-" + "0" + j;
                                }
                                else
                                {
                                    loc = zone + "-" + j;
                                }
                                if (loc.Length > 0)
                                {
                                    #region Loction
                                    RefLocation obj1 = new RefLocation();
                                    obj1.Name = loc;
                                    obj1.Code = loc;
                                    obj1.WarehouseCode = SafeValue.SafeString(cmb_WareHouse.Value);
                                    obj1.ZoneCode = rack + "00";
                                    obj1.PartyId = "";
                                    obj1.StoreCode = "";
                                    obj1.Length = 0;
                                    obj1.Width = 0;
                                    obj1.Height = 0;
                                    obj1.SpaceM3 = 0;
                                    obj1.Remark = "";
                                    obj1.Loclevel = "Unit";

                                    obj1.CreateBy = HttpContext.Current.User.Identity.Name;
                                    obj1.CreateDateTime = DateTime.Now;
                                    obj1.UpdateBy = HttpContext.Current.User.Identity.Name;
                                    obj1.UpdateDateTime = DateTime.Now;
                                    Manager.ORManager.StartTracking(obj1, Wilson.ORMapper.InitialState.Inserted);
                                    Manager.ORManager.PersistChanges(obj1);
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    this.lab.Text = "Success!";
                }
            }
            else {
                this.lab.Text = "Warehouse is not null";
            }
        }
        catch (Exception ex) { this.lab.Text = "Warehouse is not null"; }
    }
}