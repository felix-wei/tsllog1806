<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportList.aspx.cs" Inherits="Pages_Export_ImportList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import Shipment</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function OnCallback(v) {
            if (v != null && v == "Error")
                alert(v)
            else
                parent.AfterBooking(v);
        }
    </script>

    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsExport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaImport" KeyMember="SequenceId" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>Import No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HouseNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>HBL No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HblN" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Export" ClientInstanceName="detailGrid" runat="server" Width="800"
                KeyFieldName="SequenceId" AutoGenerateColumns="False" DataSourceID="dsExport"
                OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Import No" VisibleIndex="1" Width="150" SortIndex="1"
                        SortOrder="Ascending">
                        <DataItemTemplate>
                                        <a onclick='detailGrid.GetValuesOnCustomCallback("<%# Eval("JobNo") %>",OnCallback);'><%# Eval("JobNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Hbl No" VisibleIndex="3" Width="150">
                        <DataItemTemplate>
                            <%# Eval("HblNo") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" VisibleIndex="6" Width="80">
                        <DataItemTemplate>
                                    <%# Eval("Qty") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" VisibleIndex="4" Width="100">
                        <DataItemTemplate>
                            <%# Eval("Weight", "{0:0.000}") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="M3" VisibleIndex="5" Width="80">
                        <DataItemTemplate>
                            <%# Eval("Volume", "{0:0.000}")%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
