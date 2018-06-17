<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPutAway.aspx.cs" Inherits="WareHouse_SelectPage_AddPutAway" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsPutAway" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhPutAway" KeyMember="Code" />
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                            <ClientSideEvents Click="function(s,e) {
                                                        grid_PutAway.AddNewRow();
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_PutAway" ClientInstanceName="grid_PutAway"
                runat="server" DataSourceID="dsPutAway" KeyFieldName="Code" OnCellEditorInitialize="grid_PutAway_CellEditorInitialize"
                OnRowUpdating="grid_PutAway_RowUpdating" OnRowInserting="grid_PutAway_RowInserting"  OnInitNewRow="grid_PutAway_InitNewRow" OnInit="grid_PutAway_Init" OnRowDeleting="grid_PutAway_RowDeleting"
                Width="800" >
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
                    <dxwgv:GridViewDataTextColumn
                        Caption="Code" FieldName="Code"  VisibleIndex="1" Width="60">
                          <EditItemTemplate>
                                <dxe:ASPxTextBox ID="txt_Code" Width="100%" runat="server" Text='<%# Bind("Code")%>' ></dxe:ASPxTextBox>
                            </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Name" FieldName="Name" VisibleIndex="2" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Qty" FieldName="Qty"  CellStyle-HorizontalAlign="Left"  VisibleIndex="3" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                 
                </Columns>
                <Settings ShowFooter="true" />
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
