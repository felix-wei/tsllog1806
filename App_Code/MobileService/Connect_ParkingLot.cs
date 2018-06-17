using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_ParkingLot 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Connect_ParkingLot : System.Web.Services.WebService {

    public Connect_ParkingLot () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void ParkingLog_GetList()
    {
        string sql = string.Format(@"select * from PackingLot");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }
    
    [WebMethod]
    public void ParkingLog_GetCurrentStatusList()
    {
        string sql = string.Format(@"with trip1 as (
select FromParkingLot as Code,isnull(FromParkingLotType,0) as t,isnull(ChessisCode,'') as trailer,isnull(ContainerNo,'') as container,Id as TripId 
from CTM_JobDet2 where datediff(day,FromDate,getdate())>=0 and (Statuscode='S' or Statuscode='C') and isnull(FromParkingLotType,0)<>0
),
trip2 as (
select ToParkingLot as Code,isnull(ToParkingLotType,0) as t,isnull(ChessisCode,'') as trailer,isnull(ContainerNo,'') as container,Id as TripId 
from CTM_JobDet2 where datediff(day,ToDate,getdate())>=0 and Statuscode='C' and isnull(ToParkingLotType,0)<>0
),
trips as(
select * from trip1
union all
select * from trip2
),
trailer_ex as(
select Code,trailer,sum(t) as tl_ex,max(tripId) as maxId
From trips group by Code,trailer
),
parkinglot_ex as(
select *,sum(tl_ex)over(partition by Code) as pl_ex,(select ContainerNo from CTM_JobDet2 where Id=maxId) as ContainerNo
From trailer_ex
where tl_ex>0
),
tb_trailer as (
select Id,Code,cast(isnull(Remark,0) as int) as size from CTM_MastData where [Type]='chessis'
)
select *,case when pl_size=0 then '-/-' else cast((pl_size-pl_ex_size) as nvarchar)+'/'+cast((pl_size) as nvarchar) end as blx from (
select pl.Code,isnull(trailer,'') as  trailer,isnull(tl_ex,0) as tl_ex,isnull(pl_ex,0) as pl_ex,isnull(ContainerNo,'') as ContainerNo, 
isnull(tb_trailer.size,0) as tl_size,sum(isnull(tb_trailer.size,0))over(partition by pl.Code) as pl_ex_size,
pl.Size40 as pl_size
from PackingLot as pl
left outer join parkinglot_ex as pl1 on pl.Code=pl1.Code
left outer join tb_trailer on pl1.trailer=tb_trailer.Code
) as temp");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        Common.WriteJsonP(true, Common.DataTableToJson(dt));
    }
}
