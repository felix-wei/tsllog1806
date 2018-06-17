<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockMove.aspx.cs" Inherits="WareHouse_Report_StockMove" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Stock Move</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
</head>
<body>
 <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                     <td>SKU
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Width="120">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                PopupProduct(txt_SKULine_Product,null);
                            }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>LotNo</td>
                    <td>
                        <dxe:ASPxTextBox runat="server" Width="100px" ID="txt_LotNo"
                            ClientInstanceName="txt_LotNo" >
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                      <td>WareHouse</td>
                    <td><dxe:ASPxTextBox ID="txt_WareHouse" runat="server" Width="80"></dxe:ASPxTextBox></td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td></td>
                    <td>
                        <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid"
                    KeyFieldName="Id" AutoGenerateColumns="False" Width="1400">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="LotNo"  Width="80" />
						
						<dxwgv:GridViewDataTextColumn Caption="Location Code" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Permit No" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Inv. No" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Case/Pallet" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Size" FieldName="Packing"  Width="80" />

						<dxwgv:GridViewBandColumn Caption="Weight">
						<Columns>
						<dxwgv:GridViewDataTextColumn Caption="Gross" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Nett" FieldName="LotNo"  Width="80" />
						</Columns>
						</dxwgv:GridViewBandColumn>
						<dxwgv:GridViewBandColumn Caption="Incoming">
						<Columns>
						<dxwgv:GridViewDataTextColumn Caption="Date" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Pallet" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Drum/Bag/Pc" FieldName="LotNo"  Width="80" />
						</Columns>
						</dxwgv:GridViewBandColumn>
						<dxwgv:GridViewBandColumn Caption="Outgoing">
						<Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="DoDate"  Width="80">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
						<dxwgv:GridViewDataTextColumn Caption="Pallet" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Drum/Bag/Pc" FieldName="Qty1"  Width="80" />
						</Columns>
						</dxwgv:GridViewBandColumn>
						
						<dxwgv:GridViewDataTextColumn Caption="Supplier DO" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Bal Qty" FieldName="LotNo"  Width="80" />
						<dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="LotNo"  Width="80" />
                    </Columns>
					<Templates>
					<TitlePanel>
            Stock Movement Report
        </TitlePanel>
		</Templates>
                    <Settings ShowFooter="true" ShowTitlePanel="true" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0}" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                </dxwgv:ASPxGridViewExporter>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="600" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
