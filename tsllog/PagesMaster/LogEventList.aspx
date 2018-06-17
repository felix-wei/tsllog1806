<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogEventList.aspx.cs" Inherits="ReportFreightSea_Report_Import_UnMathcedRef" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>RefType
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="cmb_DocType" Width="155">
                            <Items>
                                <dxe:ListEditItem Value="" Text="" />
                                <dxe:ListEditItem Value="SI" Text="Sea Import" />
                                <dxe:ListEditItem Value="SE" Text="Sea Export" />
                                <dxe:ListEditItem Value="SCT" Text="Sea Cross Trade" />
                                <dxe:ListEditItem Value="AI" Text="Air Import" />
                                <dxe:ListEditItem Value="AE" Text="Air Export" />
                                <dxe:ListEditItem Value="ACT" Text="Air Cross Trade" />
                                <dxe:ListEditItem Value="TPT" Text="Local Handing" />
                                <dxe:ListEditItem Value="IV" Text="Invoice" />
                                <dxe:ListEditItem Value="CI" Text="Cash Invoice" />
                                <dxe:ListEditItem Value="CN" Text="Credit Note" />
                                <dxe:ListEditItem Value="DN" Text="Debit Note" />
                                <dxe:ListEditItem Value="RE" Text="Receipt" />
                                <dxe:ListEditItem Value="PC" Text="AR-Refund" />
                                <dxe:ListEditItem Value="PL" Text="Supplier Invoice" />
                                <dxe:ListEditItem Value="SC" Text="Supplier Credit Note" />
                                <dxe:ListEditItem Value="SD" Text="Supplier Debit Note" />
                                <dxe:ListEditItem Value="VO" Text="Payment Voucher" />
                                <dxe:ListEditItem Value="PS" Text="Payment" />
                                <dxe:ListEditItem Value="SR" Text="AP-Refund" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Ref No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Job No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_JobNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <hr />
            <dxwgv:ASPxGridView ID="grid_LogEvent" runat="server" Width="100%" KeyFieldName="Id"
                AutoGenerateColumns="False" ClientInstanceName="grid_LogEvent">
                <SettingsPager Mode="ShowAllRecords" PageSize="100" >
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="RefType" FieldName="RefType" VisibleIndex="1" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="RefNo" FieldName="RefNo" VisibleIndex="2" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="JobNo" VisibleIndex="3" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Action" FieldName="Action" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="User" FieldName="CreateBy" VisibleIndex="5" Width="80">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DateTime" FieldName="CreateDateTime" VisibleIndex="6" Width="150">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
