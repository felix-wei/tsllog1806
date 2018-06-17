<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCPFContribution.aspx.cs" Inherits="Modules_Hr_Report_PrintCPFContribution" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    <script type="text/javascript">
        function Print() {
            var frm = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/Modules/Hr/Report/PrintView.aspx?doc=0&document=CPF&from=' + frm + '&to=' + to)
        }
        function ExportToExcel() {
            var frm = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/Modules/Hr/Report/PrintView.aspx?doc=1&document=CPF&from=' + frm + '&to=' + to)

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <table>
            <tr>
                <td>
                    From Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" Width="120" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_End" ClientInstanceName="toDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                        Width="120" DisplayFormatString="dd/MM/yyyy" runat="server">
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
