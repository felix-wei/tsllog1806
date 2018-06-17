<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankReceiptRpt.aspx.cs" Inherits="ReportAccount_Other_BankReceiptRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Receipt Report</title>

    <script type="text/javascript">
        function Print() {
            if (null == cmb_acCode.GetValue())
                alert("Please select the chart of account");
            else
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=61&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&p=' + cmb_acCode.GetValue());
        }
        function PrintExcel() {
            if (null == cmb_acCode.GetValue())
                alert("Please select the chart of account");
            else
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=61&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&p=' + cmb_acCode.GetValue());
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <wilson:DataSource ID="dsAcCode" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XXChartAcc" KeyMember="SequenceId" FilterExpression="AcBank='Y'" />
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
                    Chart of Account
                    <dxe:ASPxComboBox ID="cmb_acCode" ClientInstanceName="cmb_acCode" runat="server"
                        Width="180px" DropDownWidth="250" DropDownStyle="DropDownList" DataSourceID="dsAcCode"
                        ValueField="Code" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="35px" />
                            <dxe:ListBoxColumn FieldName="AcDesc" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
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
                    <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Export To Excel" Width="110"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                    PrintExcel();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
