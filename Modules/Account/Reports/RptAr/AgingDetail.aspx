﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgingDetail.aspx.cs" Inherits="ReportAccount_RptAr_AgingDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AR Aging Report</title>
    <script type="text/javascript">
        function Print() {
            if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 0) {
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=8&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_Salesman.GetValue())
            } else if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 1) {
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=10&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_Salesman.GetValue())
            } else {
            parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=4&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_Salesman.GetValue())
        }
        }
        function ExportToExcel() {
            if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 0)
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=8&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_Salesman.GetValue())
            else if (cury.GetSelectedIndex() == 1 && rbt1.GetSelectedIndex() == 1)
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=10&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_Salesman.GetValue())
            else
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=4&d1=' + frm.GetText() + '&d2=' + to.GetText() + '&cury=' + cury.GetSelectedIndex() + '&p=' + cmb_Salesman.GetValue())
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="Code" />
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
                    To Date
                    <dxe:ASPxDateEdit ID="date_To" ClientInstanceName="to" Width="100" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    Salesman
                    <dxe:ASPxComboBox ID="cmb_Salesman" ClientInstanceName="cmb_Salesman" runat="server"
                        Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsSalesman"
                        ValueField="Code" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Width="80px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    Account Type
                    <dxe:ASPxRadioButtonList ID="rbt" runat="server" ClientInstanceName="cury" RepeatDirection="Horizontal"
                       >
                        <Items>
                            <dxe:ListEditItem Text="Local" Value="Local" />
                            <dxe:ListEditItem Text="Foreign" Value="Foreign" />
                        </Items>
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
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
                    <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Print" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Export To Excel" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
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
