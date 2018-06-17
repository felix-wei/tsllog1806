<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Incentive.aspx.cs" Inherits="PagesContTrucking_MasterData_Incentive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="1=0"  />
        <table>
            <tr>
                <td>
                    Type:
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cbb_type" runat="server" >
                        <Items>
                            <dxe:ListEditItem Value="ALL" Text="ALL" Selected="true" />
                            <dxe:ListEditItem Value="Trip" Text="Trip" />
                            <dxe:ListEditItem Value="OverTime" Text="OverTime" />
                            <dxe:ListEditItem Value="Standby" Text="Standby" />
                            <dxe:ListEditItem Value="PSA" Text="PSA" />
                        </Items>
                    </dxe:ASPxComboBox>
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
                <dxwgv:GridViewDataSpinEditColumn Caption="Value" FieldName="Price1" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false" PropertiesSpinEdit-DecimalPlaces="2" PropertiesSpinEdit-Increment="0" Width="200" SortOrder="Ascending"></dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="Type1" Width="200" SortOrder="Ascending">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="Trip" Text="Trip" />
                            <dxe:ListEditItem Value="OverTime" Text="OverTime" />
                            <dxe:ListEditItem Value="Standby" Text="Standby" />
                            <dxe:ListEditItem Value="PSA" Text="PSA" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="IsDefault" FieldName="Name" Width="200">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="Y" Text="Y" />
                            <dxe:ListEditItem Value="N" Text="N" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
            </Columns>

        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
