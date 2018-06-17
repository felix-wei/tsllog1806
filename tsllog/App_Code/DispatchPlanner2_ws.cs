using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// DispatchPlanner2_ws 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 [System.Web.Script.Services.ScriptService]
public class DispatchPlanner2_ws : System.Web.Services.WebService {

    public DispatchPlanner2_ws () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld(String date)
    {
        return "Hello World"+date;
    }

    [WebMethod]
    public List<det1_row>  GetContainerByDate(string date)
    {
        //string[] temp = date.Split('/');
        //if (temp.Length != 3)
        //{
        //    return null;
        //}
        //string temp_d = temp[2] + "-" + temp[1] + "-" + temp[0];
        string sql = string.Format(@"with tab1 as (
select ROW_NUMBER() over(order by Det1Id,ContainerNo,case StageCode when 'Pending' then 1 when 'Port' then 2 when 'Park1' then 3 when 'Warehouse' then 4 when 'Park2' then 5 when 'Yard' then 6 when 'Completed' then 7 else 0 end,convert(nvarchar,FromDate,111),FromTime) as RowNum
,JobNo,ContainerNo,StageCode,StageStatus,DriverCode,FromDate,FromTime,ChessisCode,Remark,Det1Id,Id as Det2Id,LoadCode,Carpark
from CTM_JobDet2 
where datediff(d,FromDate,'{0}')<=0 
),
tab2 as (
select distinct Det1Id,ContainerNo from CTM_JobDet2 where datediff(d,FromDate,'{0}')=0 
),
tab3 as (
select max(tab1.RowNum) as RowNum from tab2 left outer join tab1 on tab2.Det1Id=tab1.Det1Id and tab2.ContainerNo=tab1.ContainerNo group by tab2.Det1Id,tab2.ContainerNo
),
tab4 as (
select tab1.*,det1.ContainerType,(case job.JobType when 'KD-IMP' then 'I' when 'KD-EXP' then 'E' when 'FCL-IMP' then 'I' when 'FCL-EXP' then 'E' else 'L' end) as JobType
from tab3 left outer join tab1 on tab3.RowNum=tab1.RowNum 
left outer join CTM_JobDet1 as det1 on tab1.Det1Id=det1.Id
left outer join CTM_Job as job on tab1.JobNo=job.JobNo
where DATEDIFF(d,FromDate,'{0}')=0
)
select * from tab4 order by FromTime", date);
        DataTable  dt= ConnectSql.GetTab(sql);
        List<det1_row> list = new List<det1_row>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            det1_row newRow = new det1_row();
            newRow.Det2Id = dr["Det2Id"].ToString();
            newRow.JobNo = dr["JobNo"].ToString();
            newRow.JobType = dr["JobType"].ToString();
            newRow.ContainerNo = dr["ContainerNo"].ToString();
            newRow.ContainerType = dr["ContainerType"].ToString();
            newRow.StageCode = dr["StageCode"].ToString();
            newRow.StageStatus = dr["StageStatus"].ToString();
            newRow.Time = dr["FromTime"].ToString();
            newRow.Remark = dr["Remark"].ToString();
            newRow.Driver = dr["DriverCode"].ToString();
            newRow.Trailer = dr["ChessisCode"].ToString();
            newRow.Load = dr["LoadCode"].ToString();
            newRow.Carpark = dr["Carpark"].ToString();
            list.Add(newRow);
        }
        return list;
    }

    [WebMethod]
    public List<row_stage> GetStage()
    {
        //===========为生成界面，必须order by ParId,SortIndex
        string sql = string.Format(@"select stage1.Id,stage1.Stage,stage1.ParId
From CTM_DispatchPlanner_Stage as stage1 
order by stage1.ParId,stage1.SortIndex");
        DataTable dt = ConnectSql.GetTab(sql);
        List<row_stage> list = new List<row_stage>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            row_stage row = new row_stage();
            row.Id = dr["Id"].ToString();
            row.Stage = dr["Stage"].ToString();
            row.ParId = dr["ParId"].ToString();
            list.Add(row);
        }
        return list;
    }


    [Serializable]
    public class det1_row
    {
        public string Det2Id { get; set; }
        public string JobNo { get; set; }
        public string JobType { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerType { get; set; }
        public string StageCode { get; set; }
        public string StageStatus { get; set; }
        public string Time { get; set; }
        public string Remark { get; set; }
        public string Driver { get; set; }
        public string FromDate { get; set; }
        public string Trailer { get; set; }
        public string Load { get; set; }

        public string Carpark { get; set; }
        
    }

    [Serializable]
    public class row_stage
    {
        public string Id { get; set; }
        public string Stage { get; set; }
        public string ParId { get; set; }
    }
}
