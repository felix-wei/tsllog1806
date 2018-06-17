﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesProfit.aspx.cs"
    Inherits="ReportFreightSea_SalesProfit" %>

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
<head id="Head1" runat="server">
    <title>Bill List</title>

    <script type="text/javascript">

        function Print() {
            var frm = frmDate.GetText();
            var to = toDate.GetText();
                var party = cmb_Sales.GetValue();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=8&type=I&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party)
            
        }
        function ExportToExcel() {
            var frm = frmDate.GetText();
            var to = toDate.GetText();
                var party = cmb_Sales.GetValue();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=8&type=I&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party)
            
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="Code" />
        <table>
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
        </table>
        <div id="divSales" >
            <table>
                <tr>
                    <td>
                        Salesman
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmb_Sales" ClientInstanceName="cmb_Sales" runat="server" Width="150px"
                            DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsSalesman" ValueField="Code"
                            ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true" EnableIncrementalFiltering="True"
                            IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dx:ListBoxColumn FieldName="Code" />
                                <dx:ListBoxColumn FieldName="Name" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Reset" Width="100" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        cmb_Sales.SetValue('NA');
                        cmb_Cust.SetValue('NA');
                                    }" />
                    </dx:ASPxButton>
        </br>
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