<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillList.aspx.cs" Inherits="ReportFreightSea_Report_Account_BillList" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.2" Namespace="DevExpress.Web.ASPxPopupControl"
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
            var party = cmb_Cust.GetValue();
            var refType = cmb_RefType.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();

                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
            }
        }
        function PrintByNo() {
            var docType = cmb_DocType.GetValue();
            var party = cmb_Cust.GetValue();
            var refType = cmb_RefType.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=0&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
            }
        }
        function ExportToExcelByDate() {
            var docType = cmb_DocType.GetValue();
            var party = cmb_Cust.GetValue();
            var refType = cmb_RefType.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=1&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
            }
        }
        function ExportToExcelByNo() {
            var docType = cmb_DocType.GetValue();
            var party = cmb_Cust.GetValue();
            var refType = cmb_RefType.GetValue();
            if (party == "1")
                party = cmb_Vendor.GetValue();
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
            } else {
                var frm = frmNo.GetText();
                var to = toNo.GetText();
                parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=2&docType=1&d1=' + frm + '&d2=' + to + '&p=' + party + '&type=' + docType + "&refType=" + refType)
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
                    <td>Type
                    </td>
                    <td>
                        <dxe:aspxcombobox id="cmb_RefType" clientinstancename="cmb_RefType" runat="server"
                            width="150">
                        <Items>
                            <dxe:ListEditItem Text="Import" Value="SI" />
                            <dxe:ListEditItem Text="Export" Value="SE" />
                            <dxe:ListEditItem Text="CrossTrade" Value="SC" />
                        </Items>
                    </dxe:aspxcombobox>
                    </td>
                </tr>
                <tr>
                    <td>Doc Type
                    </td>
                    <td>
                        <dxe:aspxcombobox id="cmb_DocType" clientinstancename="cmb_DocType" runat="server" width="150">
                        <Items>
                            <dxe:ListEditItem Text="Invoice" Value="IV" />
                            <dxe:ListEditItem Text="CreidtNote" Value="CN" />
                            <dxe:ListEditItem Text="DebitNote" Value="DN" />
                            <dxe:ListEditItem Text="Voucher" Value="VO" />
                            <dxe:ListEditItem Text="Payable" Value="PL" />
                        </Items>
                    </dxe:aspxcombobox>
                    </td>
                    <td style="display: none">
                        <dxe:aspxlabel id="lbl_RefType" clientinstancename="lbl_RefType" runat="server" text="">
                        </dxe:aspxlabel>
                    </td>
                </tr>
                <%--            <tr>
                <td colspan="2">
                    <dxe:ASPxRadioButtonList ID="rbt_Party" ClientInstanceName="rbt_Party" runat="server"
                        Width="200" RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ShowPartyTab(s.GetValue());
}" />
                        <Items>
                            <dxe:ListEditItem Text="By Customer" Value="0" />
                            <dxe:ListEditItem Text="By Vendor" Value="1" />
                        </Items>
                        
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>--%>
                <tr>
                    <td>Party</td>
                    <td>
                        <div id="divCust">

                            <dxe:aspxcombobox id="cmb_Cust" clientinstancename="cmb_Cust" runat="server"
                                width="150" dropdownwidth="220" dropdownstyle="DropDownList" datasourceid="dsCustomerMast"
                                valuefield="PartyId" valuetype="System.String" textformatstring="{1}" enablecallbackmode="true"
                                enableincrementalfiltering="True" incrementalfilteringmode="StartsWith" callbackpagesize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                        <Buttons>
                            <dxe:EditButton Text="Reset"></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents buttonclick="function(s, e) {
                            if(e.buttonIndex == 0){
                                s.SetText('');
                         }
                        }" 
                        />
                    </dxe:aspxcombobox>
                        </div>
                        <div id="divVendor" style="display: none">
                            <dxe:aspxcombobox id="cmb_Vendor" clientinstancename="cmb_Vendor" runat="server"
                                width="170" dropdownwidth="180" dropdownstyle="DropDownList" datasourceid="dsVendorMast"
                                valuefield="PartyId" valuetype="System.String" textformatstring="{1}" enablecallbackmode="true"
                                enableincrementalfiltering="True" incrementalfilteringmode="StartsWith" callbackpagesize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="38px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                        <Buttons>
                            <dxe:EditButton Text="Reset"></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents buttonclick="function(s, e) {
                            if(e.buttonIndex == 0){
                                s.SetText('');
                         }
                        }" 
                        />
                    </dxe:aspxcombobox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dxe:aspxradiobuttonlist id="rbt" clientinstancename="rbt" runat="server" width="220"
                            repeatdirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ShowTab(s.GetValue());
}" />
                        <Items>
                            <dxe:ListEditItem Text="By Date" Value="0" />
                            <dxe:ListEditItem Text="By No" Value="1" />
                        </Items>
                    </dxe:aspxradiobuttonlist>
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
                                        <dxe:aspxdateedit id="date_From" clientinstancename="frmDate" editformat="Custom"
                                            editformatstring="dd/MM/yyyy" width="140" displayformatstring="dd/MM/yyyy" runat="server">
                                    </dxe:aspxdateedit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>To Date
                                    </td>
                                    <td>
                                        <dxe:aspxdateedit id="date_End" clientinstancename="toDate" editformat="Custom" editformatstring="dd/MM/yyyy"
                                            width="140" displayformatstring="dd/MM/yyyy" runat="server">
                                    </dxe:aspxdateedit>
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
                                        <dxe:aspxtextbox id="no_From" clientinstancename="frmNo" runat="server" width="140">
                                    </dxe:aspxtextbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>To No
                                    </td>
                                    <td>
                                        <dxe:aspxtextbox id="no_End" clientinstancename="toNo" runat="server" width="140">
                                    </dxe:aspxtextbox>
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
                                    <dxe:aspxbutton id="ASPxButton2" runat="server" text="Print By Date" width="120"
                                        autopostback="False" usesubmitbehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        PrintByDate();
                                    }" />
                    </dxe:aspxbutton>
                                </td>
                                <td>
                                    <dxe:aspxbutton id="ASPxButton5" runat="server" text="Export To Excel" width="110" autopostback="False"
                                        usesubmitbehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcelByDate();
                                    }" />
                    </dxe:aspxbutton>
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
                                    <dxe:aspxbutton id="btn_Post" runat="server" text="Print By Number" width="120" autopostback="False"
                                        usesubmitbehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        PrintByNo();
                                    }" />
                    </dxe:aspxbutton>
                                </td>
                                <td>
                                    <dxe:aspxbutton id="ASPxButton3" runat="server" text="Export To Excel" width="110" autopostback="False"
                                        usesubmitbehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcelByNo();
                                    }" />
                    </dxe:aspxbutton>
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
