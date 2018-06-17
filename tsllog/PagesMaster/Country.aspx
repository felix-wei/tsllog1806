<%@ Page Title="Country" Language="C#"  AutoEventWireup="true"
    CodeFile="Country.aspx.cs" Inherits="XX_Country" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Country</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Country Name
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtName" Width="100" runat="server" Text="">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                                <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="dsCountry" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCountry" KeyMember="Code" FilterExpression="1=0" />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsCountry"
            Width="100%" KeyFieldName="Code" AutoGenerateColumns="False" OnInit="grid_Init" OnCellEditorInitialize="grid_CellEditorInitialize"
            OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating"
            OnCustomCallback="grid_CustomCallback" 
            onrowinserted="grid_RowInserted">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn  Width="10%" VisibleIndex="0">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1">
                                <EditItemTemplate>
                                    <dxe:ASPxTextBox ID="txt" Width="100%" runat="server" Text='<%# Bind("Code")%>'>
                                    </dxe:ASPxTextBox>
                                </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
