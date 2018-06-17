<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Carpark.aspx.cs" Inherits="PagesContTrucking_MasterData_Carpark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='carpark'" />
        <table>
            <tr>
                <td>Carpark Code</td>
                <td>
                    <dxe:ASPxTextBox ID="txt_search_Code" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>Carpark Name</td>
                <td>
                    <dxe:ASPxTextBox ID="txt_search_Name" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                        <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="850px" DataSourceID="dsTransport" KeyFieldName="Id" OnRowInserting="grid_Transport_RowInserting" OnRowDeleting="grid_Transport_RowDeleting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init">
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="Inline" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Code" Caption="Carpark Code" VisibleIndex="1" Width="100">
                    <EditItemTemplate>
                        <dxe:ASPxTextBox ID="txt" ReadOnly='<%# SafeValue.SafeString(Eval("Code")).Length>0 %>' Width="80%" runat="server" Text='<%# Bind("Code")%>'>
                        </dxe:ASPxTextBox>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                        </div>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataColumn FieldName="Name" Caption="Carpark Name"  >
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Remark"></dxwgv:GridViewDataColumn>
            </Columns>

        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
