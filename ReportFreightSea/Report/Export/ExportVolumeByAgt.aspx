<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportVolumeByAgt.aspx.cs" Inherits="ReportFreightSea_ExportVolumeByAgt" %>

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
    <title>List</title>

    <script type="text/javascript">
        function Print() {
            var refType = cmb_RefType.GetValue();
            var jobType = cmb_JobType.GetValue();
            var frm = frmDate.GetText();
            var party = cmb_PartyTo.GetValue();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=34&docType=0&d1=' + frm + '&d2=' + to + '&refType=' + refType + '&type=' + jobType + '&p=' + party)
        }
        function ExportToExcel() {
            var refType = cmb_RefType.GetValue();
            var jobType = cmb_JobType.GetValue();
            var party = cmb_PartyTo.GetValue();
            var frm = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=34&docType=1&d1=' + frm + '&d2=' + to + '&refType=' + refType + '&type=' + jobType + '&p=' + party)
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true'" />
            <table>
                <tr>
                    <td>Type
                    </td>
                    <td>
                        <dxe:aspxcombobox id="cmb_RefType" clientinstancename="cmb_RefType" runat="server" width="150">
                        <Items>
                            <dxe:ListEditItem Text="ALL" Value="" />
                            <dxe:ListEditItem Text="Export" Value="SE" />
                            <dxe:ListEditItem Text="CrossTrade" Value="SC" />
                        </Items>
                    </dxe:aspxcombobox>
                    </td>
                </tr>
                <tr>
                    <td>Job Type
                    </td>
                    <td>
                        <dxe:aspxcombobox id="cmb_JobType" clientinstancename="cmb_JobType" runat="server" width="150">
                        <Items>
                            <dxe:ListEditItem Text="ALL" Value="" />
                            <dxe:ListEditItem Text="FCL" Value="FCL" />
                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                            <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
                        </Items>
                    </dxe:aspxcombobox>
                    </td>
                </tr>
                <tr>
                    <td>From Date
                    </td>
                    <td>
                        <dxe:aspxdateedit id="date_From" clientinstancename="frmDate" editformat="Custom"
                            editformatstring="dd/MM/yyyy" width="150" displayformatstring="dd/MM/yyyy" runat="server">
                    </dxe:aspxdateedit>
                    </td>
                </tr>
                <tr>
                    <td>To Date
                    </td>
                    <td>
                        <dxe:aspxdateedit id="date_End" clientinstancename="toDate" editformat="Custom" editformatstring="dd/MM/yyyy"
                            width="150" displayformatstring="dd/MM/yyyy" runat="server">
                    </dxe:aspxdateedit>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>Agent
                    </td>
                    <td>
                        <dxe:aspxcombobox id="cmb_PartyTo" clientinstancename="cmb_PartyTo" runat="server"
                            width="180" dropdownwidth="180" dropdownstyle="DropDownList" datasourceid="dsCustomerMast"
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
                                s.SetText('NA');
                         }
                        }" 
                        />
                    </dxe:aspxcombobox>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dxe:aspxbutton id="ASPxButton2" runat="server" text="Print" width="120" autopostback="False"
                            usesubmitbehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                    </dxe:aspxbutton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dxe:aspxbutton id="ASPxButton5" runat="server" text="Export To Excel" width="120"
                            autopostback="False" usesubmitbehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcel();
                                    }" />
                    </dxe:aspxbutton>
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
