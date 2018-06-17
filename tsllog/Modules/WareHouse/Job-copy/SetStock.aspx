<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetStock.aspx.cs" Inherits="WareHouse_Job_SetStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Set Stock</title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
       <script type="text/javascript">

           function SelectAll() {
               if (btnSelect.GetText() == "Select All")
                   btnSelect.SetText("UnSelect All");
               else
                   btnSelect.SetText("Select All");
               jQuery("input[id*='ack_IsPay']").each(function () {
                   this.click();
               });
           }
           function OnCallback(v) {
               if (v == "Success") {
                   alert("Action Success!!!");
                   grid.Refresh();
               }
               else if (v != null && v.length > 0)
                   alert(v)
           }
           function PopupInventory(code) {
               popubCtr.SetHeaderText('Set Inventory');
               popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SetInventory.aspx?Loc=' + code);
               popubCtr.Show();
           }
    </script>

    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefLocation" KeyMember="Id"  FilterExpression="Loclevel='Unit'"/>
    <div>
        <table>
            <tr>
                <td>Set</td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_Location" ClientInstanceName="cmb_Location" runat="server"
                        Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefLocation"
                        ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                        OnClick="btn_Sch_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid" runat="server" Width="60%"
            KeyFieldName="Id" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Set" FieldName="Code" VisibleIndex="1" Width="50">
                    <DataItemTemplate>
                        <a href="#" onclick="PopupInventory('<%# Eval("Code") %>');"><%# Eval("Code") %></a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WarehouseCode" VisibleIndex="2" Width="50">
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewDataTextColumn Caption="MaxCount" FieldName="MaxCount" VisibleIndex="2" Width="40" CellStyle-HorizontalAlign="Left">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="AvaibleQty" FieldName="AvaibleQty" VisibleIndex="2" Width="40" CellStyle-HorizontalAlign="Left">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="1000" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
