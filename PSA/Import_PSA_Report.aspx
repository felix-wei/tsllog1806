<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import_PSA_Report.aspx.cs" Inherits="PSA_Import_PSA_Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
                <tr>
                    <td>Date </td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateFrom" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
<%--                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateTo" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>--%>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" Width="850px" KeyFieldName="Id"
            AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords" />
            <Columns>
                <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="AccountNo" Caption="Account No"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="BillNo" Caption="Bill No"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataDateColumn FieldName="BillDate" Caption="Bill Date" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataColumn FieldName="Amount" Caption="Amount"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Customer" Caption="Customer"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="BillItem" Caption="Bill Item Number"></dxwgv:GridViewDataColumn>
            </Columns>
            <Settings ShowFooter="true" />
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="ContainerNo" SummaryType="Count" DisplayFormat="{0:0.00}" />
            </TotalSummary>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
