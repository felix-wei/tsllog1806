<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillingList.aspx.cs" Inherits="ReportTpt_Report_BillingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script type="text/javascript" src="/Script/Basepages.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table>
                <tr>
                    <td>
                    Doc Type
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_DocType" ClientInstanceName="cmb_DocType" runat="server" Width="150">
                        <Items>
                            <dxe:ListEditItem  Text="" Value="ALL"/>
                            <dxe:ListEditItem Text="Invoice" Value="IV" />
                            <dxe:ListEditItem Text="CreidtNote" Value="CN" />
                            <dxe:ListEditItem Text="DebitNote" Value="DN" />
                            <dxe:ListEditItem Text="Vocher" Value="VO" />
                            <dxe:ListEditItem Text="Payable" Value="PL" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                     <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Doc Date">
                        </dxe:ASPxLabel>
                    </td>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Customer">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_CustId" ClientInstanceName="btn_CustId" runat="server" Text="" Width="85">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(btn_CustId,txt_CustName,'C');
                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="170" runat="server">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnRetrieve" runat="server" Text="Retrieve" Width="120" AutoPostBack="False" OnClick="btnRetrieve_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Import" runat="server" Width="100%"
                KeyFieldName="SequenceId"
                AutoGenerateColumns="False" ClientInstanceName="grid_Import">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>

                    <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="2" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5" Width="80">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PartyName" FieldName="PartyName" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="MastRefNo" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="6" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="7" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="8" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="9" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pol" FieldName="Pol" VisibleIndex="9" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pod" FieldName="Pod" VisibleIndex="9" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Import">
            </dxwgv:ASPxGridViewExporter>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                AllowResize="True" Width="700" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
