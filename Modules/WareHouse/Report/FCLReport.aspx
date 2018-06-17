<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FCLReport.aspx.cs" Inherits="ReportWarehouse_Report_FCLReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            function Print() {
                parent.PrintReport('/Modules/WareHouse/RptPrintView.aspx?doc=2&docType=0&type=' + cmb_DocType.GetValue() + '&d1=' + frmDate.GetText() + '&d2=' + toDate.GetText());
            }
            function ExportToExcel() {
                parent.PrintReport('/Modules/WareHouse/Rptprintview.aspx?doc=1&docType=1&d1=' + frmDate.GetText() + '&d2=' + toDate.GetText() + '&type=' + cmb_DocType.GetValue())
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>Type</td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_DocType" ClientInstanceName="cmb_DocType" runat="server" SelectedIndex="1"
                        Width="140">
                        <Items>
                            <dxe:ListEditItem Text="DO IN" Value="In"/>
                            <dxe:ListEditItem Text="DO OUT" Value="Out" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    From Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_End" ClientInstanceName="toDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                        Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
             <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Width="100" AutoPostBack="False"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Export To Excel" Width="120"
                                    AutoPostBack="False" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                        ExportToExcel();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
