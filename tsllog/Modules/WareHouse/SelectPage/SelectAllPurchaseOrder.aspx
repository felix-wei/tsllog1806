<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectAllPurchaseOrder.aspx.cs" Inherits="WareHouse_SelectPage_SelectAllPurchaseOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="PoNo" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_Name" Width="170" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                        OnClick="btn_Sch_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <div>
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
                KeyFieldName="Id"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutValue("<%# Eval("PoNo") %>")'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PoNo" FieldName="PoNo" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="PartyName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WarehouseId" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PoDate" FieldName="PoDate" VisibleIndex="2">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
