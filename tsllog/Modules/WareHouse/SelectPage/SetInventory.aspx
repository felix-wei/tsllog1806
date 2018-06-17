<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetInventory.aspx.cs" Inherits="WareHouse_SelectPage_SetInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel runat="server" ID="lbl_Name" Text="Set:"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxLabel runat="server" ID="lbl_Code" Text=""></dxe:ASPxLabel>
                </td>
            </tr>
        </table>
    <div>
    <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" AutoGenerateColumns="False" Width="80%">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Product Code" FieldName="Product" Width="120" VisibleIndex="1" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" Width="100" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty1" VisibleIndex="2" Width="40" CellStyle-HorizontalAlign="Left">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="2" Visible="false" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="GRN No" FieldName="DoNo" VisibleIndex="2" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="GRN Date" FieldName="DoDate" VisibleIndex="2" Width="80">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="300">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="On Hand" FieldName="HandQty" VisibleIndex="2" Width="40" CellStyle-HorizontalAlign="Left">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Diameter" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Length" FieldName="Att2" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>

                </Columns>
        <Settings ShowFooter="true"/>
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0}"/>
        </TotalSummary>
            </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
