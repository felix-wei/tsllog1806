﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillList.aspx.cs" Inherits="ReportFreightSea_Account_BillList" %>

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
        function ShowTab(tabInd) {
            var tabDate = document.getElementById("tab_Date");
            var tabNo = document.getElementById("tab_No");
            if (tabInd == "0") {
                tabDate.style.display = "block";
                tabNo.style.display = "none";
            } else if (tabInd == "1") {
                tabDate.style.display = "none";
                tabNo.style.display = "block";
            }
        }
        function ShowPartyTab(tabInd) {
            var tabCust = document.getElementById("divCust");
            var tabVendor = document.getElementById("divVendor");
            if (tabInd == "0") {
                tabCust.style.display = "block";
                tabVendor.style.display = "none";
            } else if (tabInd == "1") {
            tabCust.style.display = "none";
            tabVendor.style.display = "block";
            }
        }

        function PrintByDate() {
            var docType = cmb_DocType.GetValue();
            var partyTab = rbt_Party.GetValue();
            var party = cmb_Cust.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            }
        }
        function PrintByNo() {
            var docType = cmb_DocType.GetValue();
            var partyTab = rbt_Party.GetValue();
            var party = cmb_Cust.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            }
        }
        function ExportToExcelByDate() {
            var docType = cmb_DocType.GetValue();
            var partyTab = rbt_Party.GetValue();
            var party = cmb_Cust.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            }
        }
        function ExportToExcelByNo() {
            var docType = cmb_DocType.GetValue();
            var partyTab = rbt_Party.GetValue();
            var party = cmb_Cust.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType)
            }
        }
   </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
         <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true'" />
        <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
       <table>
            <tr>
                <td>
                    Doc Type
                </td>
                <td>
                    <dx:ASPxComboBox ID="cmb_DocType" ClientInstanceName="cmb_DocType" runat="server" Width="150">
                        <Items>
                            <dx:ListEditItem Text="Invoice" Value="IV" />
                            <dx:ListEditItem Text="CreidtNote" Value="CN" />
                            <dx:ListEditItem Text="DebitNote" Value="DN" />
                            <dx:ListEditItem Text="Vocher" Value="VO" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dx:ASPxRadioButtonList ID="rbt_Party" ClientInstanceName="rbt_Party" runat="server"
                        Width="200" RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ShowPartyTab(s.GetValue());
}" />
                        <Items>
                            <dx:ListEditItem Text="By Customer" Value="0" />
                            <dx:ListEditItem Text="By Vendor" Value="1" />
                        </Items>
                        
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
            <tr><td>Party</td><td>
                    <dx:ASPxButton ID="btn_Reset" runat="server" Text="Reset" Width="120"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                       cmb_Cust.SetValue('');
                       cmb_Vendor.SetValue('');
                                    }" />
                    </dx:ASPxButton></td></tr>
            <tr>
<td colspan="2">
<div id="divCust">
                    
                    <dx:ASPxComboBox ID="cmb_Cust" ClientInstanceName="cmb_Cust" runat="server"
                        Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dx:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                            <dx:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dx:ASPxComboBox>
                    </div>
                    <div id="divVendor" style="display:none">
                    <dx:ASPxComboBox ID="cmb_Vendor" ClientInstanceName="cmb_Vendor" runat="server"
                        Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsVendorMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dx:ListBoxColumn FieldName="PartyId" Caption="ID" Width="38px" />
                            <dx:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dx:ASPxComboBox></div>
                    </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dx:ASPxRadioButtonList ID="rbt" ClientInstanceName="rbt" runat="server" Width="200"
                        RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ShowTab(s.GetValue());
}" />
                        <Items>
                            <dx:ListEditItem Text="By Date" Value="0" />
                            <dx:ListEditItem Text="By No" Value="1" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="tab_Date">
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
                    </div>
                    <div id="tab_No" style="display: none">
                        <table>
                            <tr>
                                <td>
                                    From No
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="no_From" ClientInstanceName="frmNo" runat="server" Width="140">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    To No
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="no_End" ClientInstanceName="toNo" runat="server" Width="140">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                <table><tr><td>
                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Print By Date" Width="120"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        PrintByDate();
                                    }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton5" runat="server" Text="Export To Excel" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcelByDate();
                                    }" />
                    </dx:ASPxButton>
                </td></tr></table></td>
            </tr>
            <tr>
                <td colspan="2">
                <table><tr><td>
                    <dx:ASPxButton ID="btn_Post" runat="server" Text="Print By Number" Width="120" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        PrintByNo();
                                    }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Export To Excel" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcelByNo();
                                    }" />
                    </dx:ASPxButton>
                </td></tr></table></td>
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