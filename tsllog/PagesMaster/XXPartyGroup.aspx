<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XXPartyGroup.aspx.cs" Inherits="PagesMaster_XXPartyGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <wilson:DataSource ID="dsPartyGroup" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XXPartyGroup" KeyMember="Code" />
    <form id="form1" runat="server">
        <div>
            <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
            </dxe:ASPxButton>
            <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsPartyGroup"
                KeyFieldName="Code" AutoGenerateColumns="False"
                Width="1000px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting" OnHtmlEditFormCreated="grid_HtmlEditFormCreated" OnCellEditorInitialize="grid_CellEditorInitialize">
                <SettingsEditing Mode="Inline" PopupEditFormWidth="750" />
                <SettingsPager PageSize="10" Mode="ShowPager">
                </SettingsPager>
                <Settings ShowFilterRow="false" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="200">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Description" FieldName="Description" VisibleIndex="2" />
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
