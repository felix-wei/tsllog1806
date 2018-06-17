using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// EzshipLog 的摘要说明
/// </summary>
public class EzshipLog
{
	public EzshipLog()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static void Log(string refNo, string jobNo, string refType, string action)
    {
        string sql = string.Format("insert into LogEvent(RefNo,JobNo,RefType,Action,CreateBy,CreateDateTime) values('{0}','{1}','{2}','{3}','{4}',getdate())",
                refNo, jobNo, refType, action, EzshipHelper.GetUserName());
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    public static void Activity(string refNo, string jobNo, string refType, string action)
    {
        string sql = string.Format("insert into LcActivity(RefNo,JobNo,RefType,LcEvent,CreateBy,CreateDateTime) values('{0}','{1}','{2}','{3}','{4}',getdate())",
                refNo, jobNo, refType, action, EzshipHelper.GetUserName());
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
}