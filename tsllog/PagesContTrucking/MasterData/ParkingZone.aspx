<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParkingZone.aspx.cs" Inherits="PagesContTrucking_MasterData_ParkingZone" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsPackingLot" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmParkingZone" KeyMember="Id"  />
        <table>
            <tr>
                <td> Code</td>
                <td>
                    <dxe:ASPxTextBox ID="txt_search_Code" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
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
        <dxwgv:ASPxGridView ID="ASPxGridView" ClientInstanceName="detailGrid" runat="server" Width="100%" DataSourceID="dsPackingLot" KeyFieldName="Id"
             OnRowInserting="grid_RowInserting" OnRowDeleting="grid_RowDeleting" OnRowUpdating="grid_RowUpdating" OnInitNewRow="grid_InitNewRow" OnInit="grid_Init">
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="Inline" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Code" Caption="Code" VisibleIndex="1" Width="30%"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Size20" FieldName="Size20" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" Width="20%">
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Size40" FieldName="Size40" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" Width="20%">
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="DoubleMT" FieldName="DoubleMT" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" Width="20%">
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>

        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
