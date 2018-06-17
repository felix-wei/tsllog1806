<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Modules_Hr_Master_Category" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsRole" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrMastData" KeyMember="Id" />
        <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Save" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsRole"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow"
            OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="Type" VisibleIndex="3" Visible="false">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
