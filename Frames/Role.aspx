<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Role" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Role</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsRole" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Role" KeyMember="SequenceId" />
        <table>
            <tr>
                <td valign="top">
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                    <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsRole"
                        KeyFieldName="SequenceId" AutoGenerateColumns="False" Width="500" OnInitNewRow="ASPxGridView1_InitNewRow"
                        OnInit="ASPxGridView1_Init">
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <SettingsCustomizationWindow Enabled="True" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60">
                                <EditButton Visible="True" />
                                <DeleteButton Visible="true" />
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1">
                                <EditItemTemplate>
                                    <dxe:ASPxTextBox ID="txtCode" runat="server" Width="100" Text='<%# Bind("Code") %>'
                                        Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)==0 %>'>
                                    </dxe:ASPxTextBox>
                                </EditItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                    </dxwgv:ASPxGridView>
                </td>
                <td valign="top">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
