<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryResult.aspx.cs" Inherits="PagesContTrucking_MasterData_DeliveryResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="1=0"  />
        <table>
            <tr>
                <td>
                    <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                        <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"  DataSourceID="dsTransport" KeyFieldName="Id" OnRowInserting="grid_Transport_RowInserting" OnRowDeleting="grid_Transport_RowDeleting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnBeforePerformDataSelect="grid_Transport_BeforePerformDataSelect" >
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="Inline" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="80">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Detail" FieldName="Remark" Width="500"></dxwgv:GridViewDataTextColumn>
            </Columns>

        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
