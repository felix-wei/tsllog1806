using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService_Attachment_List
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService_Attachment_List : System.Web.Services.WebService
{

    public WebService_Attachment_List()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void List_GetData_ByPage()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        int curPage = SafeValue.SafeInt(info["curPage"], 0);
        string str_pageSize = info["pageSize"].ToString();
        curPage = curPage <= 0 ? 1 : curPage;
        int pageSize = 0;
        if (!str_pageSize.Equals("ALL"))
        {
            pageSize = SafeValue.SafeInt(str_pageSize, 0);
        }
        string From = info["From"].ToString();
        string To = info["To"].ToString();
        string no = SafeValue.SafeString(info["no"]);
        string Category = SafeValue.SafeString(info["category"], "ALL");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@FromDate", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@ToDate", To, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@Category", Category, SqlDbType.NVarChar, 30));
        list.Add(new ConnectSql_mb.cmdParameters("@no", "%" + no + "%", SqlDbType.NVarChar, 100));

        string sql = "";
        string sql_inner = string.Format(@"select * from CTM_Attachment as att
where ");


        string sql_where = "1=1";

        if (no.Length == 0)
        {
            if (From.Length > 0)
            {
                sql_where += " and att.CreateDateTime>=@FromDate";
            }
            if (To.Length > 0)
            {
                sql_where += " and att.CreateDateTime<@ToDate";
            }
            if (Category.Length > 0 && !Category.Equals("ALL"))
            {
                sql_where += " and att.FileType=@Category";
            }
        }
        else
        {
            sql_where += " and (att.RefNo like @No or att.ContainerNo like @No)";
        }

        sql_inner = sql_inner + sql_where;

        sql = string.Format(@"select count(*) from ({0}) as temp", sql_inner);
        int totalItems = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        int totalPages = pageSize > 0 ? (totalItems / pageSize + (totalItems % pageSize > 0 ? 1 : 0)) : 1;
        curPage = curPage > totalPages ? totalPages : curPage;
        list.Add(new ConnectSql_mb.cmdParameters("@currenPage", pageSize * (curPage - 1), SqlDbType.Int));



        sql = string.Format(@"
select top {0} * from (
select top {0} * from (

select ROW_NUMBER()over(order by CreateDateTime) as rowId,* 
from (
{1}
) as temp

) as t
where rowId>@currenPage
order by rowId
) as t
order by rowId desc", pageSize, sql_inner);
        //throw new Exception(sql);
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"list\":{2},\"curPage\":{3},\"totalPages\":{4},\"totalItems\":{5}{1}", "{", "}", DataList, curPage, totalPages, totalItems);
        Common.WriteJson(true, context);
    }

}
