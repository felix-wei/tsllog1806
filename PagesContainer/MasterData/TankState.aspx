<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TankState.aspx.cs" Inherits="PagesMaster_Category" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>&nbsp;</td>
                <td>
                </td>
                <td>
                 <dxe:ASPxButton ID="ASPxButton1" Width="160" runat="server" Enabled='True' Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>  
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <wilson:DataSource ID="dsXXUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                        TypeName="C2.XXUom" KeyMember="Id"  FilterExpression="CodeType='TankState'"/>
                    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsXXUom"
                        KeyFieldName="Id" AutoGenerateColumns="False" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting"
                        Width="100%" OnRowInserting="grid_RowInserting" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow">
                        <SettingsEditing  Mode="Inline" />
                        <SettingsBehavior  ConfirmDelete="true" />
                        <SettingsPager Mode="ShowAllRecords" PageSize="100" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                <EditButton Visible="True" />
                                <DeleteButton Visible="True">
                                </DeleteButton>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="100">
                                <EditItemTemplate>
                                    <dxe:ASPxTextBox ID="txt" ReadOnly='<%# SafeValue.SafeString(Eval("Code")).Length>0 %>' Width="80%" runat="server" Text='<%# Bind("Code")%>'>
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
