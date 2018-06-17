<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalListing.aspx.cs" Inherits="WareHouse_Report_BalListing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table>
                <tr>
                     <td style="display:none">
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="End Date">
                        </dxe:ASPxLabel>
                         <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                    </td>
                    <td><dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Product">
                        </dxe:ASPxLabel></td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_Product" ClientInstanceName="btn_Product" runat="server" Text="" Width="95">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupAllProduct(btn_Product,null);
                        }" />
                                    </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Customer">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_CustId" ClientInstanceName="btn_CustId" runat="server" Text="" Width="95">
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
                    <td>Type</td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_DoType" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Text="All" Value="All" />
                                <dxe:ListEditItem Text="SO" Value="SO"/>
                                <dxe:ListEditItem Text="PO" Value="PO"/>
                            </Items>
                        </dxe:ASPxComboBox>
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
                AutoGenerateColumns="False" ClientInstanceName="grid_Import" Styles-Cell-HorizontalAlign="Left">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                     <dxwgv:GridViewDataTextColumn Caption="DoDate" FieldName="DoDate" VisibleIndex="1" Width="80">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="DoNo" VisibleIndex="2" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="DoType" VisibleIndex="3" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="ProductCode" VisibleIndex="4" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="5" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="PartyName" VisibleIndex="6" Width="300">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="BalQty" VisibleIndex="7" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="7" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="8" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    
                </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Import">
            </dxwgv:ASPxGridViewExporter>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="800" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
