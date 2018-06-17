<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MastData.aspx.cs" Inherits="PagesMaster_MastData" %>

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
                    <wilson:DataSource ID="dsXXMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
                        TypeName="C2.XXMastData" KeyMember="Id"  FilterExpression="CodeType='JobCate'"/>
                    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsXXMastData"
                        KeyFieldName="Id" AutoGenerateColumns="False" OnRowUpdating="grid_RowUpdating"
                        Width="100%" OnRowInserting="grid_RowInserting" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting">
                        <SettingsEditing  Mode="Inline" />
                        <SettingsBehavior  ConfirmDelete="true" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                <EditButton Visible="True" />
                                <DeleteButton Visible="True">
                                </DeleteButton>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="250" SortIndex="1" SortOrder="Ascending">
                                <EditItemTemplate>
                                    <dxe:ASPxTextBox ID="txt"  Width="250" runat="server" Text='<%# Bind("Code")%>'>
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
