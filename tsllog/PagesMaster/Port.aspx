<%@ Page Title="Port" Language="C#"  AutoEventWireup="true"
    CodeFile="Port.aspx.cs" Inherits="XX_Port" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Port</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <wilson:DataSource ID="dsCountry" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXCountry" KeyMember="Code" />
    <div>
        <table>
            <tr>
                <td>
                    Port Name
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
                <td>
                    <dxe:ASPxButton ID="btn_Save" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="dsThePort" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXPort" KeyMember="Id" />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsThePort"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init"
            OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating"
            OnCustomCallback="grid_CustomCallback" OnRowDeleting="grid_RowDeleting"
            onrowinserted="grid_RowInserted">
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
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="150" PropertiesTextEdit-MaxLength="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Country" FieldName="CountryCode" VisibleIndex="3" Width="180">
                    <PropertiesComboBox ValueType="System.String" TextFormatString="{0}" DataSourceID="dsCountry" TextField="Code" EnableIncrementalFiltering="true"
                        ValueField="Code">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Width="70px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
