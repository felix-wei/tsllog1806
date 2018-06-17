<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Uom.aspx.cs" Inherits="PagesMaster_Uom" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;text-align:left">
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="160" runat="server" Enabled='True' Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
                <td>&nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <wilson:DataSource ID="dsXXUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                        TypeName="C2.XXUom" KeyMember="Id" />
                    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsXXUom" OnPageIndexChanged="grid_PageIndexChanged" OnPageSizeChanged="grid_PageSizeChanged"
                        KeyFieldName="Id" AutoGenerateColumns="False" OnRowUpdating="grid_RowUpdating" OnCellEditorInitialize="grid_CellEditorInitialize"
                        Width="100%" OnRowInserting="grid_RowInserting" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting">
                        <SettingsEditing  Mode="Inline" />
                        <SettingsBehavior  ConfirmDelete="true" />
                        <SettingsPager Mode="ShowPager" PageSize="20" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                <EditButton Visible="True" />
                                <DeleteButton Visible="True">
                                </DeleteButton>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="100" SortIndex="1" SortOrder="Ascending">
                                <EditItemTemplate>
                                    <dxe:ASPxTextBox ID="txt" Width="80%" runat="server" Text='<%# Bind("Code")%>'>
                                    </dxe:ASPxTextBox>
                                </EditItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="4"  Visible="true" >
                            </dxwgv:GridViewDataColumn>
                        </Columns>
                        <Settings ShowPreview="True" />
                    </dxwgv:ASPxGridView>

                </td>
            </tr>
        </table>
        <div>
        </div>
    </form>
</body>
</html>
