<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefVessel.aspx.cs" Inherits="PagesMaster_RefVessel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lblName" runat="server" Text="Name"></dxe:ASPxLabel>
                     
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
        <wilson:DataSource ID="dsRefVessel" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefVessel" KeyMember="Id" FilterExpression="1=0" />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsRefVessel"
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
                <dxwgv:GridViewCommandColumn  Width="10%" VisibleIndex="0">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1">
                                <EditItemTemplate>
                                    <dxe:ASPxTextBox ID="txt" Width="80%" runat="server" Text='<%# Bind("Name")%>'>
                                    </dxe:ASPxTextBox>
                                </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
