<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintStockTallySheet.aspx.cs" Inherits="PagesContTrucking_Report_PrintStockTallySheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Print() {
            var no = txt_No.GetText();
            var from = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/PagesContTrucking/Report/RptPrintView.aspx?docType=0&doc=stock_tallysheet&no=' + no + '&d1=' + from + '&d2=' + to)

        }
        function ExportToExcel() {
            var no = txt_No.GetText();
            var from = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/PagesContTrucking/Report/RptPrintView.aspx?docType=1&doc=stock_tallysheet&no=' + no + '&d1=' + from + '&d2=' + to)
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Job No"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_No" ClientInstanceName="txt_No" runat="server" Width="140px"></dxe:ASPxTextBox>
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
        </table>
         <table>
            <tr>
                <td colspan="2">
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Width="120" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Export To Excel" Width="120"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcel();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
