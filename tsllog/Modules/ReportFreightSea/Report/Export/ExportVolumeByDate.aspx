<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportVolumeByDate.aspx.cs"
    Inherits="ReportFreightSea_ExportVolumeByDate" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v14.2" Namespace="DevExpress.Web"
    TagPrefix="dxpc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bill List</title>

    <script type="text/javascript">
        function Print() {
            var docType = cmb_DocType.GetText();
            var frm = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=33&docType=0&d1=' + frm + '&d2=' + to + '&type=' + docType)
        }
        function ExportToExcel() {
            var docType = cmb_DocType.GetText();
            var frm = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=33&docType=1&d1=' + frm + '&d2=' + to + '&type=' + docType)
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Doc Type
                </td>
                <td>
                    <dx:ASPxComboBox ID="cmb_DocType" ClientInstanceName="cmb_DocType" runat="server"
                        Width="150">
                        <Items>
                            <dx:ListEditItem Text="" Value="" />
                            <dx:ListEditItem Text="FCL" Value="FCL" />
                            <dx:ListEditItem Text="LCL" Value="LCL" />
                            <dx:ListEditItem Text="CONSOLE" Value="CONSOLE" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    From Date
                </td>
                <td>
                    <dx:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To Date
                </td>
                <td>
                    <dx:ASPxDateEdit ID="date_End" ClientInstanceName="toDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                        Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Width="120" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dx:ASPxButton ID="ASPxButton5" runat="server" Text="Export To Excel" Width="120"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcel();
                                    }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="600" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
