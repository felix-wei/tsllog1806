<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatGroup.aspx.cs" Inherits="Mobile_ChatGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Script/pages.js"></script>
    <style type="text/css">
        body {
            font-size: 14px;
        }

        .edit_table {
            border-spacing: 5px;
            width: 100%;
        }

            .edit_table .td80 {
                min-width: 80px;
                text-align: left;
            }

        .css_det {
            margin-left: 50px;
        }
    </style>
    <script type="text/javascript">

        function PopupMobileUsers(name) {
            console.log('/Mobile/SelectPage_Users.aspx?p=' + hide_Id.GetText());
            clientId = name;
            //clientName = txtName;
            popubCtr.SetHeaderText('User List');
            popubCtr.SetContentUrl('/Mobile/SelectPage_Users.aspx?p=' + hide_Id.GetText());
            popubCtr.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsGroup" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.MobileChatGroup" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsGroupDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.MobileChatGroupDet" KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table>
                <tr>
                    <td>User:</td>
                    <td>
                        <dxe:ASPxTextBox runat="server" ID="txt_search"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btn_search" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btn_AddNew" Text="New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                gv.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <dxwgv:ASPxGridView runat="server" ID="gv" ClientInstanceName="gv" AutoGenerateColumns="false" Width="100%" KeyFieldName="Id" DataSourceID="dsGroup" OnInit="gv_Init" OnRowDeleting="gv_RowDeleting" OnBeforePerformDataSelect="gv_BeforePerformDataSelect" OnRowInserting="gv_RowInserting" OnRowUpdating="gv_RowUpdating" OnHtmlEditFormCreated="gv_HtmlEditFormCreated">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Settings ShowFilterRow="false" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewCommandColumn Width="10%">
                    <EditButton Visible="true"></EditButton>
                    <DeleteButton Visible="true"></DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="GroupName" Width="30%"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Note" FieldName="Note1" Width="30%"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn Caption="Create Date" FieldName="CreateDate" Width="30%" PropertiesDateEdit-DisplayFormatString="yyyy/MM/dd"></dxwgv:GridViewDataDateColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div>
                        <div style="display: none">
                            <dxe:ASPxTextBox runat="server" ID="hide_Id" ClientInstanceName="hide_Id" Value='<%#Bind("Id") %>'></dxe:ASPxTextBox>
                        </div>
                        <table>
                            <tr>
                                <td colspan="2">
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                </td>
                            </tr>
                            <tr>
                                <td>Group Name:</td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="txt_GroupName" Value='<%#Bind("GroupName") %>' Width="200"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Note:</td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="txt_Note" Value='<%#Bind("Note1") %>' Width="200"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <dxe:ASPxButton runat="server" ID="btn_AddDet" AutoPostBack="false" Text="Add Person">
                                        <ClientSideEvents Click="function(s,e){gv_Det.AddNewRow();}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>

                        <dxwgv:ASPxGridView runat="server" ID="gv_Det" ClientInstanceName="gv_Det" Width="100%" AutoGenerateColumns="false" KeyFieldName="Id" DataSourceID="dsGroupDet" OnInit="gv_Det_Init" OnRowDeleting="gv_Det_RowDeleting" OnBeforePerformDataSelect="gv_Det_BeforePerformDataSelect"
                            OnRowInserting="gv_Det_RowInserting" OnRowUpdating="gv_Det_RowUpdating">
                            <SettingsCustomizationWindow Enabled="True" />
                            <SettingsEditing Mode="Inline" />
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Columns>
                                <dxwgv:GridViewCommandColumn Width="10%" CellStyle-BackColor="#ffffcc">
                                    <EditButton Visible="true"></EditButton>
                                    <DeleteButton Visible="true"></DeleteButton>
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataColumn Caption="UserName" FieldName="Username" CellStyle-BackColor="#ffffcc" EditCellStyle-BackColor="#ffffcc">
                                    <EditItemTemplate>
                                        <dxe:ASPxButtonEdit runat="server" ID="btne_username" ClientInstanceName="btne_username" AutoPostBack="false" Value='<%#Bind("Username") %>' Width="200">
                                            <Buttons>
                                                <dxe:EditButton Text="..."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){PopupMobileUsers(btne_username);}" />
                                        </dxe:ASPxButtonEdit>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </div>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>

        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="800" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
