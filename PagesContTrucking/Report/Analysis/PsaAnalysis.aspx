<%@ Page Title="" Language="C#" MasterPageFile="~/Mis/Drilldown.master" %>

<script runat="server">

    void Page_Init(object o, EventArgs e)
    {
        mis_drilldown_master master = Page.Master as mis_drilldown_master;
        master.report_name = "PSA Bill Analysis";
        master.drilldown_list = new string[] {"JobNo","DocNo","Tariff","Amount","ContNo","ContSize","ContType","Remark","ServiceStart","ServiceEnd" };
        master.pivot_row = new string[] { "ContNo","JobNo", "Charge","JobType" };
        master.pivot_col = new string[] { "DocMonth" };
        master.pivot_data = new string[] { "Amount" };
        master.pivot_filter = new string[] { "JobNo","DocNo","ContType", "ContSize", "POL", "POD", "Carrier","DocDate" };
        master.query_sql = @"
select 
[JOB_NO] as JOB,
[JOB_NO] as JobNo,
[Bill Number] as DocNo,
CONVERT(nvarchar(10), [BILL DATE], 111) as DocDate,
CONVERT(nvarchar(4),[BILL DATE],111) as DocYear,
CONVERT(nvarchar(7),[BILL DATE],111) as DocMonth,
[Container Number] as ContNo,
[Tariff Description] as Tariff,
[Rate] as Amount,
[FULL VESSEL NAME] as Vessel,
[FULL OUT VOY NUMBER] as Voyage,
[CNTR TYPE] as ContType,
[CNTR SIZE] as ContSize,
[TARIFF DESCRIPTION] as Charge,
[DESCRIPTION LINE 1] as Remark,
[CNTR OPERATOR] as OP,
[GST INDICATOR] as Gst,
[LOCATION FROM] as POL,
[LOCATION TO] as POD,
[LOAD/DISC INDICATOR] as JobType,
LEFT([SERVICE START DATE],16) as ServiceStart,
Left([SERVICE END DATE],16) as ServiceEnd,
[LINE CODE] as Carrier
from Psa_Bill where LTrim(RTrim([Tariff Code])) <> '799999' and (CONVERT(DATETIME, [BILL DATE], 111) BETWEEN '{0}' AND '{1}')";
        master.filename_template = "PsaBillAnalysis_{0:yyyy}_{1:MM}_{2:dd}.xls";
   }

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

