<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statement.aspx.cs" Inherits="ReportAccount_RptAr_Statement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AR Statement</title>

    <script type="text/javascript">
        function Print() {
            if (frm.GetText() == "") {
                alert("Please entry the begin date");
            } else if (to.GetText() == "") {
                alert("Please entry the end date");
            }
            else {
                if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 0) {
                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=6&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_PartyTo.GetValue() + '&' + new Date())
                } else if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 1) {
                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=11&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_PartyTo.GetValue() + '&' + new Date())
                } else {
                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=2&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_PartyTo.GetValue() + '&' + new Date())
                }
            }
        }
        function ExportToExcel() {
            if (frm.GetText() == "") {
                alert("Please entry the begin date");
            } else if (to.GetText() == "") {
                alert("Please entry the end date");
            }
            else {
                if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 0)
                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=6&d1='  + frm.GetText() + '&d2='+ to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_PartyTo.GetValue() + '&' + new Date())
                else if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 1)
                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=11&d1='  + frm.GetText() + '&d2='+ to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_PartyTo.GetValue() + '&' + new Date())
                else
                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=2&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_PartyTo.GetValue() + '&' + new Date())
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true'"/>
        <table>
            <tr>
                <td>
                    From
                    <dxe:ASPxDateEdit ID="date_Frm" ClientInstanceName="frm" Width="110" EditFormat="Custom"
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
                        Width="180px" DropDownWidth="280px" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="30px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="180px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    Account Type
                    <dxe:ASPxRadioButtonList ID="rbt" runat="server" RepeatDirection="Horizontal" SelectedIndex="0"
                        ClientInstanceName="cury">
                        <Items>
                            <dxe:ListEditItem Text="Local" Value="Local" />
                            <dxe:ListEditItem Text="Foreign" Value="Foreign" />
                        </Items>
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>
            <tr style="display:none;">
                <td>
                    <dxe:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        SelectedIndex="0" ClientInstanceName="rbt1">
                        <Items>
                            <dxe:ListEditItem Text="Local" Value="0" />
                            <dxe:ListEditItem Text="Base On Currency" Value="1" />
                        </Items>
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Print" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Export To Excel" Width="110"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcel();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
