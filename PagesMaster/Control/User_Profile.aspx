<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableViewState="false"  CodeFile="User_Profile.aspx.cs" Inherits="User_Profile" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
<br />
        <wilson:DataSource ID="dsUser" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.User" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsRole" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Role" KeyMember="SequenceId" />
    
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsUser"
            KeyFieldName="SequenceId" AutoGenerateColumns="False" Width="700" OnInitNewRow="ASPxGridView1_InitNewRow"
            OnInit="ASPxGridView1_Init" OnRowInserting="ASPxGridView1_RowInserting" OnRowInserted="ASPxGridView1_RowInserted"
            OnRowUpdating="ASPxGridView1_RowUpdating" OnRowDeleting="ASPxGridView1_RowDeleting">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="EditForm" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="false" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Email" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataCheckColumn FieldName="IsActive" VisibleIndex="2">
                </dxwgv:GridViewDataCheckColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <table>
                        <tr>
                            <td>
                                Name
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="txtCode" runat="server" Width="160" Text='<%# Bind("Name") %>'
                                    Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)==0 %>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Password
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="txtPwd" Password="true" runat="server" Width="160" Text='<%# Bind("Pwd") %>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: right; padding: 2px 2px 2px 2px">
                                <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormUpdateButton"
                                    runat="server">
                                </dxwgv:ASPxGridViewTemplateReplacement>
                                <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                                    runat="server">
                                </dxwgv:ASPxGridViewTemplateReplacement>
                            </td>
                        </tr>
                    </table>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
