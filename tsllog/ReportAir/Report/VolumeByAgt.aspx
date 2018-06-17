<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VolumeByAgt.aspx.cs" Inherits="ReportAir_Report_VolumeByAgt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Volume By Agent</title>
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
                var refType = cmb_RefType.GetValue();
                var frm = frmDate.GetText();
                var party = cmb_PartyTo.GetValue();
                var to = toDate.GetText();
                parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=4&docType=0&d1=' + frm + '&d2=' + to + '&type=' + refType + '&p=' + party)
            }
            function ExportToExcel() {
                var refType = cmb_RefType.GetValue();
                var party = cmb_PartyTo.GetValue();
                var frm = frmDate.GetText();
                var to = toDate.GetText();
                parent.PrintReport('/ReportAir/Rptprintview.aspx?doc=4&docType=1&d1=' + frm + '&d2=' + to + '&type=' + refType + '&p=' + party)
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
                        <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="180" DropDownWidth="190" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
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
                    </dxe:ASPxComboBox>

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
