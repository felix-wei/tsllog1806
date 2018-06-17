using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using C2;

public static class WarehouseRptPrint
{
    #region Summary
    public static DataTable dsSummary(string billType, DateTime date1, DateTime date2, string userName)
    {
        string type = "Billing summary to Rejo-Export Shipment-";
        if(billType.ToLower()=="in"){
            type = "Billing summary to Rejo-Import Shipment-";
        }
        string strsql = string.Format(@"select row_number() over(order by Expecteddate,id) as No,'{3}'+'-'+(CONVERT(nvarchar(10),'{0}',103)+'-'+CONVERT(nvarchar(10),'{1}',103)) as Date
,CONVERT(nvarchar(10),ExpectedDate,103) as ExpectedDate,PoNo,PermitNo as Permit,PartyName as Supplier,'' as Origin
,(select isnull(LotNo,'')+',' from (select isnull(LotNo,'') as LotNo from wh_dodet where doNo=wh_do.DoNO group by isnull(LotNo,'')) as tb for xml path('')) as LotNo
,(select isnull(ProductClass,'')+',' from (select isnull(p.ProductClass,'') as ProductClass from wh_dodet det left join ref_Product p on det.ProductCode=p.Code where doNo=wh_do.DoNO group by isnull(p.ProductClass,'')) as tb1 for xml path('')) as Des
,(select isnull(ContainerNo,'')+char(10) from (select isnull(ContainerNo,'') as ContainerNo from wh_dodet3 where doNo=wh_do.DoNO group by isnull(ContainerNo,'')) as tb2 for xml path('')) as Container
,(select sum(isnull(CostLocAmt,0)) from Wh_costing where RefNo=wh_do.DoNo and JobType=wh_do.Dotype) as Amt
,(select sum(isnull(Qty1,0)) from Wh_DoDet where DoNo=wh_do.DoNo and Dotype=wh_do.Dotype) as Qty
 from wh_do where ExpectedDate between '{0}' and '{1}' and DoType='{2}'", date1, date2, billType,type);
        return ConnectSql.GetTab(strsql);
    }  
    #endregion
    #region FCL
    public static DataTable dsFCL(string billType, DateTime date1, DateTime date2, string userName)
    {
        string title = "EXPORT CONTAINER UNSTUFFING RECORDS";
        if (billType.ToLower() == "in")
        {
            title = "IMPORT CONTAINER STUFFING RECORDS";
        }
        string strsql = string.Format(@"select row_number() over(order by Expecteddate,id) as No,'{3}'+'-'+(CONVERT(nvarchar(10),'{0}',103)+'-'+CONVERT(nvarchar(10),'{1}',103)) as Title
,convert(nvarchar(10),ExpectedDate,103) as ExpectedDate
,wh_do.DoNo,PoNo
,(select isnull(LotNo,'')+',' from (select isnull(LotNo,'') as LotNo from wh_dodet where doNo=wh_do.DoNO group by isnull(LotNo,'')) as tb for xml path('')) as LotNo
,(select isnull(ProductClass,'')+',' from (select isnull(p.ProductClass,'') as ProductClass from wh_dodet det left join ref_Product p on det.ProductCode=p.Code where doNo=wh_do.DoNO group by isnull(p.ProductClass,'')) as tb1 for xml path('')) as Des
,PartyName as Supplier
,(select isnull(ContainerNo,'')+char(10) from (select isnull(ContainerNo,'')+'/'+isnull(SealNo,'') as ContainerNo from wh_dodet3 where doNo=wh_do.DoNO group by isnull(ContainerNo,''),isnull(SealNo,'')) as tb2 for xml path('')) as Container
,Carrier as Shipping
,isnull(Cont40,0) as Cont40
,isnull(Cont20,0) as cont20,isnull(Cont40,0)*2+isnull(Cont20,0) as TotalCont
,PermitNo as Permit
,(select sum(isnull(Qty1,0)) from Wh_DoDet where DoNo=wh_do.DoNo and Dotype=wh_do.Dotype) as Qty
,Contractor
,'MONTHLY PAYMENT' AS PaidBy
,convert(decimal(10,2),isnull(Cont40,0)*150+isnull(Cont20,0)*100)  as Amt
 from wh_do
 left join (
 select DoNo,DoType
 ,sum(Case when charindex('40',containertype)>0 then 1 else 0 end) as Cont40
 ,sum(Case when charindex('20',containertype)>0 then 1 else 0 end) as Cont20
  from wh_dodet3
 group by DoNo,DoType
 ) as tab_cont on wh_do.DoNo=tab_cont.DoNo and wh_do.DoType=tab_cont.DoType where ExpectedDate between '{0}' and '{1}' and wh_do.DoType='{2}' and ModelType='FCL'", date1, date2, billType,title);
        return ConnectSql.GetTab(strsql);
    }

    #endregion
    #region Detail
    public static DataTable dsDoDet(string type, DateTime d1, DateTime d2, string userName)
    {

        string sql = "";
        string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"].ToUpper();
        string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
        sql = string.Format(@"SELECT *,dense_rank() over( order BY t1.DoNo) as No FROM(SELECT m.DoNO,PermitNo,ModelType,'Billing to Rejo - '+case when m.DoType='IN' then 'In Coming' else 'Out Going' end + ' - 01/01/2013 to 25/01/2014' AS Title
,CONVERT(nvarchar(10), ExpectedDate, 103)+' - '+ Priority+' FROM '+m.PartyName AS Title2,LotNo,d.des1 AS des,CONVERT(NVARCHAR(10),Qty1) AS Qty
,Qty1 AS Qty1,'' AS Price,'' AS Amt
    FROM  wh_DoDet d inner JOIN Wh_Do m ON m.DoNo=d.DoNo AND m.DoType=d.DoType 
	where m.DoType='IN' and m.ExpectedDate>= '2013-01-01' and m.ExpectedDate< '2014-01-26'	UNION ALL SELECT DoNO,PermitNo,ModelType
,'Billing to Rejo - '+case when m.DoType='IN' then 'In Coming' else 'Out Going' end + ' - 01/01/2013 to 25/01/2014' AS Title,CONVERT(nvarchar(10), ExpectedDate, 103)+' - '+ Priority+' FROM '+m.PartyName AS Title2
,'' AS LotNo,d.chgCodedes AS Des,CONVERT(NVARCHAR(10),CostQty) AS Qty,0 AS Qty1,CONVERT(NVARCHAR(10),CostPrice) AS Price,CONVERT(NVARCHAR(10),d.costlocAmt) AS Amt
    FROM  wh_Costing d inner JOIN Wh_Do m ON m.DoNo=d.RefNo AND m.DoType=d.JobType
	where m.DoType='{0}' and m.ExpectedDate>= '{1}' and m.ExpectedDate< '{2}'
) AS t1", type, d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
        
        //sql += "  order by job.JobNo";
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Det";
        return dt;

    }

    #endregion
}
