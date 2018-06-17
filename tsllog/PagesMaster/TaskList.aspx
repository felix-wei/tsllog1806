<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskList.aspx.cs" Inherits="PagesMaster_TaskList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <wilson:DataSource ID="dsTask" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.Tasks"
        KeyMember="Id" />
            <wilson:DataSource ID="dsUser" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.User" KeyMember="Name" />
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                     <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New Task" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           detailGrid.AddNewRow();
                        }" />
                        </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    <div>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server" DataSourceID="dsTask" OnInitNewRow="grid_InitNewRow"  OnInit="grid_Init"
            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" Styles-Cell-HorizontalAlign="Left" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                    <EditButton Visible="true"></EditButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Task Title" FieldName="TaskName" VisibleIndex="1" Width="200">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Due Date" FieldName="DueDate" VisibleIndex="2" Width="100">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="TaskType" FieldName="TaskType" VisibleIndex="1" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Assign To" FieldName="PartyId" VisibleIndex="1" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="1" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="280">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Create Date" FieldName="CreateDateTime" VisibleIndex="2" Width="100">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Update Date" FieldName="UpdateDateTime" VisibleIndex="2" Width="100">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsDetail ShowDetailRow="true" />
            <Templates>
                <EditForm>
                    <table>
                        <tr>
                            <td>Task Title</td>
                            <td><dxe:ASPxTextBox ID="txt_TaskName" runat="server" Text='<%# Bind("TaskName") %>' Width="300px"></dxe:ASPxTextBox></td>
                            <td>task To</td>
                            <td>
                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_PartyId"
                                    runat="server" DataSourceID="dsUser" Value='<%# Bind("PartyId") %>' TextField="Name" ValueField="Name">
                                   <Columns >
                                       <dxe:ListBoxColumn  Caption="Name" FieldName="Name"/>
                                   </Columns>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>Due Date</td>
                            <td>
                                <dxe:ASPxDateEdit ID="txt_DueDate" runat="server" Value='<%# Bind("DueDate") %>' Width="150" EditFormat="DateTime"  EditFormatString="dd/MM/yyyy HH:mm:ss">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td>Task Type</td>
                            <td>
                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="80" ID="cmb_TaskType"
                                    runat="server" Text='<%# Bind("TaskType") %>'>
                                    <Items>
                                        <dxe:ListEditItem Text="Accounts" Value="Accounts" />
                                        <dxe:ListEditItem Text="Meeting" Value="Meeting" />
                                        <dxe:ListEditItem Text="Operations" Value="Operations" />
                                        <dxe:ListEditItem Text="Reminder" Value="Reminder" />
                                        <dxe:ListEditItem Text="Survey" Value="Survey" />
                                        <dxe:ListEditItem Text="Others" Value="Others" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>Status</td>
                            <td>
                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="80" ID="ASPxComboBox1"
                                    runat="server" Text='<%# Bind("StatusCode") %>'>
                                    <Items>
                                        <dxe:ListEditItem Text="Pending" Value="Pending" />
                                        <dxe:ListEditItem Text="In Progress" Value="In Progress" />
                                        <dxe:ListEditItem Text="Completed" Value="Completed" />
                                        <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Task Content</td>
                            <td colspan="9"><dxe:ASPxMemo ID="memo_Content" runat="server"  Text='<%# Bind("Description") %>' Rows="4" Width="100%"></dxe:ASPxMemo></td>
                        </tr>
                        <tr>
                            <td colspan="10">
                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"Pending")!="Cancel" %>'>
                                        <ClientSideEvents Click="function(s,e){detailGrid.UpdateEdit();}" />
                                    </dxe:ASPxHyperLink>
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                </div>
                            </td>
                        </tr>
                    </table>
                </EditForm>
                <DetailRow>
                    <dxwgv:ASPxGridView ID="grid_Detail" ClientInstanceName="grid_Detail" runat="server"
                        KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Detail_BeforePerformDataSelect">
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Contant" FieldName="Description" VisibleIndex="1" Width="100"> 
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                    </dxwgv:ASPxGridView>
                </DetailRow>
            </Templates>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
