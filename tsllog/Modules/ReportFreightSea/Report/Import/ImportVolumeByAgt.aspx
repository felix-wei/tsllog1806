﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportVolumeByAgt.aspx.cs" Inherits="ReportFreightSea_ImportVolumeByAgt" %>

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
        var clientId = null;
        var clientName = null;
        function PutValue(s, name) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PopupAgent(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetContentUrl('/PagesFreight/SelectPage/AgentList.aspx');
            popubCtr.SetHeaderText('Agent');
            popubCtr.Show();
        }
        function Print() {
            var docType = cmb_DocType.GetText();
            var frm = frmDate.GetText();
            var party = cmb_PartyTo.GetValue();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=4&docType=0&d1=' + frm + '&d2=' + to + '&type=' + docType + '&p=' + party)
        }
        function ExportToExcel() {
            var docType = cmb_DocType.GetText();
            var party = cmb_PartyTo.GetValue();
            var frm = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=4&docType=1&d1=' + frm + '&d2=' + to + '&type=' + docType + '&p=' + party)
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsAgent='true'" />
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
                            <dx:ListEditItem Text="FCL" Value="F" />
                            <dx:ListEditItem Text="LCL" Value="L" />
                            <dx:ListEditItem Text="CONSOLE" Value="C" />
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
        </table>
            <table>
                <tr>
                    <td>
                        Agent
                    </td>
                    <td>                  
                        <dx:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="150px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dx:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                            <dx:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dx:ASPxComboBox>

                    </td>
                    <td>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Reset" Width="40" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        cmb_PartyTo.SetValue('NA');
                                    }" />
                    </dx:ASPxButton></td>
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