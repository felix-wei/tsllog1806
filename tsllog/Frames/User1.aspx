﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User1.aspx.cs" Inherits="Frames_User1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="160" runat="server" Enabled='True' Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <wilson:DataSource ID="dsUser" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.User" KeyMember="SequenceId" FilterExpression="Role='Client'" />
            <wilson:DataSource ID="dsRole" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Role" KeyMember="SequenceId" />
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsUser"
                KeyFieldName="SequenceId" AutoGenerateColumns="False" Width="700" OnInitNewRow="ASPxGridView1_InitNewRow"
                OnInit="ASPxGridView1_Init" OnRowInserting="ASPxGridView1_RowInserting" OnRowInserted="ASPxGridView1_RowInserted"
                OnRowUpdating="ASPxGridView1_RowUpdating" OnRowDeleting="ASPxGridView1_RowDeleting" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing Mode="EditForm" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Role" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Port" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Email" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Tel" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <table>
                            <tr>
                                <td>Name
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtCode" runat="server" Width="160" Text='<%# Bind("Name") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Tel
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtTel" ClientInstanceName="txtTel" runat="server" Width="160" Text='<%# Bind("Tel") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Email
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtEmail" ClientInstanceName="txtEmail" runat="server" Width="160" Text='<%# Bind("Email") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Role
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" DataSourceID="dsRole" TextField="Code"
                                        ValueField="Code" ValueType="System.String" Value='<%# Bind("Role") %>' Width="160">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Password
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtPwd" Password="true" runat="server" Width="160">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Port
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtPort" runat="server" Width="160" Text='<%# Bind("Port") %>' MaxLength="5">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Customer</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_Cust" ClientInstanceName="btn_Cust" runat="server" Text='<%# Bind("CustId") %>' Width="160">
                                        <Buttons>
                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupShipper(btn_Cust,txt_Cust,null,txtTel,null,txtEmail,null);
                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td colspan="2">
                                    <dxe:ASPxTextBox Width="100%" ReadOnly="true" BackColor="Control" ID="txt_Cust" ClientInstanceName="txt_Cust"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormUpdateButton"
                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
}" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>