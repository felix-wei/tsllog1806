<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillList.aspx.cs" Inherits="ReportAir_Report_Account_BillList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
             var refType = cmb_RefType.GetValue();
             var docType = cmb_DocType.GetValue();
             var party = cmb_Cust.GetValue();
             if (party == "1")
                 party = cmb_Vendor.GetValue();
             var tabInd = rbt.GetValue();
             if (tabInd == "0") {
                 var frm = frmDate.GetText();
                 var to = toDate.GetText();

                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=1&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
             } else {
                 var frm = frmNo.GetText();
                 var to = toNo.GetText();
                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=1&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
             }
         }
         function PrintByNo() {
             var refType = cmb_RefType.GetValue();
             var docType = cmb_DocType.GetValue();
             var party = cmb_Cust.GetValue();
             if (party == "1")
                 party = cmb_Vendor.GetValue();
             var tabInd = rbt.GetValue();
             if (tabInd == "0") {
                 var frm = frmDate.GetText();
                 var to = toDate.GetText();
                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=2&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
             } else {
                 var frm = frmNo.GetText();
                 var to = toNo.GetText();
                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=2&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
             }
         }
         function ExportToExcelByDate() {
             var refType = cmb_RefType.GetValue();
             var docType = cmb_DocType.GetValue();
             var party = cmb_Cust.GetValue();
             if (party == "1")
                 party = cmb_Vendor.GetValue();
             var tabInd = rbt.GetValue();
             if (tabInd == "0") {
                 var frm = frmDate.GetText();
                 var to = toDate.GetText();
                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=1&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
             } else {
                 var frm = frmNo.GetText();
                 var to = toNo.GetText();
                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=1&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
             }
         }
         function ExportToExcelByNo() {
             var refType = cmb_RefType.GetValue();
             var docType = cmb_DocType.GetValue();
             var party = cmb_Cust.GetValue();
             if (party == "1")
                 party = cmb_Vendor.GetValue();
             var tabInd = rbt.GetValue();
             if (tabInd == "0") {
                 var frm = frmDate.GetText();
                 var to = toDate.GetText();
                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=2&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
             } else {
                 var frm = frmNo.GetText();
                 var to = toNo.GetText();
                 parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=2&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + '&refType=' + refType)
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
                    Ref Type
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_RefType" ClientInstanceName="cmb_RefType" runat="server"
                        Width="150">
                        <Items>
                            <dxe:ListEditItem Text="Air Import" Value="AI"  Selected="true"/>
                            <dxe:ListEditItem Text="Air Export" Value="AE" />
                            <dxe:ListEditItem Text="Air CorssTrade" Value="ACT" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>Doc Type
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_DocType" ClientInstanceName="cmb_DocType" runat="server" Width="150">
                        <Items>
                            <dxe:ListEditItem Text="Invoice" Value="IV" />
                            <dxe:ListEditItem Text="CreidtNote" Value="CN" />
                            <dxe:ListEditItem Text="DebitNote" Value="DN" />
                            <dxe:ListEditItem Text="Voucher" Value="VO" />
                            <dxe:ListEditItem Text="Payable" Value="PL" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>Party</td>
                <td>
                    <dxe:ASPxButton ID="btn_Reset" runat="server" Text="Reset" Width="120"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                       cmb_Cust.SetValue('');
                       cmb_Vendor.SetValue('');
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="divCust">

                        <dxe:ASPxComboBox ID="cmb_Cust" ClientInstanceName="cmb_Cust" runat="server"
                            Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                            ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </div>
                    <div id="divVendor" style="display: none">
                        <dxe:ASPxComboBox ID="cmb_Vendor" ClientInstanceName="cmb_Vendor" runat="server"
                            Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsVendorMast"
                            ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="38px" />
                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxe:ASPxRadioButtonList ID="rbt" ClientInstanceName="rbt" runat="server" Width="200"
                        RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ShowTab(s.GetValue());
}" />
                        <Items>
                            <dxe:ListEditItem Text="By Date" Value="0" />
                            <dxe:ListEditItem Text="By No" Value="1" />
                        </Items>
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="tab_Date">
                        <table>
                            <tr>
                                <td>From Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                                        EditFormatString="dd/MM/yyyy" Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>To Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_End" ClientInstanceName="toDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                        Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="tab_No" style="display: none">
                        <table>
                            <tr>
                                <td>From No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="no_From" ClientInstanceName="frmNo" runat="server" Width="140">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>To No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="no_End" ClientInstanceName="toNo" runat="server" Width="140">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print By Date" Width="120"
                                    AutoPostBack="False" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                        PrintByDate();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Export To Excel" Width="110" AutoPostBack="False"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                        ExportToExcelByDate();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_Post" runat="server" Text="Print By Number" Width="120" AutoPostBack="False"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                        PrintByNo();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Export To Excel" Width="110" AutoPostBack="False"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                        ExportToExcelByNo();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
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
