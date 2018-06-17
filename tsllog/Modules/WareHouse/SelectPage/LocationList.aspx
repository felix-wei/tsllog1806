<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationList.aspx.cs" Inherits="WareHouse_SelectPage_LocationList" %>

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
                 <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <div>
            <table>
                <tr>
                     <td><dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="WareHouse" /></td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                        Width="100px" DropDownWidth="100" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                        ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Name" />
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
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
                KeyFieldName="Id"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutValue("<%# Eval("Code") %>","<%# Eval("LocationName") %>");'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WarehouseCode" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing"  VisibleIndex="2">
                        <DataItemTemplate>
                            <%# GetPacking(SafeValue.SafeString(Eval("Code"))) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
