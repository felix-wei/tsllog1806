<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MastType.aspx.cs" Inherits="WareHouse_MastData_MastType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhMastData" KeyMember="Id" />
        <div>
            <table width="450">
                <tr>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsWhMastData"
                    KeyFieldName="Id" AutoGenerateColumns="False" OnCellEditorInitialize="grid_CellEditorInitialize"
                    Width="1000px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" Theme="DevEx" OnRowDeleting="grid_RowDeleting" OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsEditing Mode="Inline" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior ConfirmDelete="True" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                            <EditButton Visible="true"></EditButton>
                            <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="1" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" Width="150">
                            <EditItemTemplate>
                                <dxe:ASPxTextBox ID="txt_Code" Width="100%" runat="server" Text='<%# Bind("Code")%>' ></dxe:ASPxTextBox>
                            </EditItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager Mode="ShowPager"></SettingsPager>
                    <Styles Header-HorizontalAlign="Center">
                        <Header HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center"></Cell>
                    </Styles>
                </dxwgv:ASPxGridView>
            </div>
        </div>
    </form>
</body>
</html>
