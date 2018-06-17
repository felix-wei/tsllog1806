<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArDocListing.aspx.cs" Inherits="ReportAccount_Other_ArDocListing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AR Receipt Report</title>

    <script type="text/javascript">
        function Print() {
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=64&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&p=' + cmb_PartyTo.GetValue() + '&cury=' + cmb_DocType.GetValue());
        }
        function PrintExcel() {
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=64&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&p=' + cmb_PartyTo.GetValue() + '&cury=' + cmb_DocType.GetValue());
            }
        
        function PrintDet() {
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=65&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&p=' + cmb_PartyTo.GetValue() + '&cury=' + cmb_DocType.GetValue());
        }
        function PrintDetExcel() {
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=65&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&p=' + cmb_PartyTo.GetValue() + '&cury=' + cmb_DocType.GetValue());
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true'" />
    <div>
        <table>
            <tr>
                <td>
                    From Date
                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frm" Width="100" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To
                    <dxe:ASPxDateEdit ID="date_To" ClientInstanceName="to" Width="100" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                   Party To
                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
            <td>Doc Type
            <dxe:ASPxComboBox ID="cmb_DocType" ClientInstanceName="cmb_DocType" runat="server">
            <Items>
            <dxe:ListEditItem Text="Invoice" Value="IV" />
            <dxe:ListEditItem Text="Credit Note" Value="CN" />
            <dxe:ListEditItem Text="Debit Note" Value="DN" />
            <dxe:ListEditItem Text="Receipt" Value="RE" />
            <dxe:ListEditItem Text="Payment CN" Value="PC" />
            </Items>
            </dxe:ASPxComboBox>
            </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Print Summary" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   Print();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Export To Excel" Width="110"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                    PrintExcel();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                <br />
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print Detail" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   PrintDet();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Export To Excel" Width="110"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                    PrintDetExcel();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
