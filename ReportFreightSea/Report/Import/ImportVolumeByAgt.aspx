<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportVolumeByAgt.aspx.cs" Inherits="ReportFreightSea_ImportVolumeByAgt" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.2" Namespace="DevExpress.Web.ASPxPopupControl"
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
                    <dxe:ASPxComboBox ID="cmb_DocType" ClientInstanceName="cmb_DocType" runat="server"
                        Width="150">
                        <Items>
                            <dxe:ListEditItem Text="" Value="" />
                            <dxe:ListEditItem Text="FCL" Value="FCL" />
                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                            <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
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
                        EditFormatString="dd/MM/yyyy" Width="150" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_End" ClientInstanceName="toDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                        Width="150" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
        </table>
            <table>
                <tr>
                    <td>
                        Agent
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
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="600" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
