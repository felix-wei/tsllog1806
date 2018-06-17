<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveType.aspx.cs" Inherits="Modules_Hr_Master_LeaveType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <table>
            <tr>
                
                <td style="display:none">
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
                <wilson:DataSource ID="dsHrMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrMastData" KeyMember="Id"  FilterExpression="1=1"/>
    <div>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsHrMastData"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init" OnCellEditorInitialize="grid_CellEditorInitialize"
            OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting"
            OnRowInserted="grid_RowInserted">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn Width="10%" VisibleIndex="0">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="300">
                    <EditItemTemplate>
                        <dxe:ASPxTextBox ID="txt" Width="100%" runat="server" Text='<%# Bind("Code")%>'>
                        </dxe:ASPxTextBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
