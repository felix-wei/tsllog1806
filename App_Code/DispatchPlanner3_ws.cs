using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// DispatchPlanner3_ws 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class DispatchPlanner3_ws : System.Web.Services.WebService {

    public DispatchPlanner3_ws () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World planner3";
    }

    [WebMethod]
    public List<DriverTrip> GetDriverTrip_ByDate(string date)
    {
        List<DriverTrip> list = new List<DriverTrip>();
        string sql = string.Format(@"with tab1 as (
select Driver,Towhead from CTM_DriverLog where DATEDIFF(d,Date,'{0}')=0
),
tab2 as (
select DriverCode,JobNo,ContainerNo,FromDate,FromTime,ToDate,ToTime,Id from CTM_JobDet2 where DATEDIFF(d,FromDate,'{0}')=0 or DATEDIFF(d,ToDate,'{0}')=0
)
select tab1.Driver,tab1.Towhead,tab2.* from tab1 left outer join tab2 on tab1.Driver=tab2.DriverCode order by Driver,FromDate,FromTime,ToDate,ToTime", date);
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            DriverTrip row = new DriverTrip();
            row.Driver = dr["Driver"].ToString();
            row.Towhead = dr["Towhead"].ToString();
            row.ContainerNo = dr["ContainerNo"].ToString();
            row.JobNo = dr["JobNo"].ToString();
            row.FromDate = dr["FromDate"].ToString();
            row.FromTime = dr["FromTime"].ToString();
            row.ToDate = dr["ToDate"].ToString();
            row.ToTime = dr["ToTime"].ToString();
            row.Id = dr["Id"].ToString();
            list.Add(row);
        }
        return list;
    }
    
    [Serializable]
    public class DriverTrip
    {
        public string Id { get; set; }
        public string Driver { get; set; }
        public string Towhead { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerSize { get; set; }
        public string JobNo { get; set; }
        public string FromTime { get; set; }
        public string FromDate { get; set; }
        public string ToTime { get; set; }
        public string ToDate { get; set; }
        public string JobType { get; set; }
        public string LoadCode { get; set; }
        public string StageCode { get; set; }
        public string StageStatus { get; set; }

    }

    [WebMethod]
    public DriverTrip GetDriverTripToast_ById(string Id)
    {
        string sql = string.Format(@"select det1.JobNo,det1.ContainerNo,det1.ContainerType,DriverCode,TowheadCode,convert(nvarchar,FromDate,111) as FromDate,FromTime,convert(nvarchar,ToDate,111) as ToDate,ToTime
,(case job.JobType when 'KD-IMP' then 'I' when 'KD-EXP' then 'E' when 'FCL-IMP' then 'I' when 'FCL-EXP' then 'E' else 'L' end) as JobType,LoadCode,StageCode,StageStatus
from CTM_JobDet2 as det2 
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where det2.Id={0}", Id);
        DataTable dt = ConnectSql.GetTab(sql);
        DriverTrip toast = new DriverTrip();
        if (dt.Rows.Count > 0)
        {
            DataRow dr=dt.Rows[0];
            toast.Id = Id;
            toast.JobNo = dr["JobNo"].ToString();
            toast.ContainerNo = dr["ContainerNo"].ToString();
            toast.ContainerSize = dr["ContainerType"].ToString();
            toast.Driver = dr["DriverCode"].ToString();
            toast.Towhead = dr["TowheadCode"].ToString();
            toast.FromDate = dr["FromDate"].ToString();
            toast.FromTime = dr["FromTime"].ToString();
            toast.ToDate = dr["ToDate"].ToString();
            toast.ToTime = dr["ToTime"].ToString();
            toast.JobType = dr["JobType"].ToString();
            toast.LoadCode = dr["LoadCode"].ToString();
            toast.StageCode = dr["StageCode"].ToString();
            toast.StageStatus = dr["StageStatus"].ToString();
        }
        return toast;
    }

}
