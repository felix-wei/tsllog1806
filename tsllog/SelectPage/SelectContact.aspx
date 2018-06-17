<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectContact.aspx.cs" Inherits="SelectPage_SelectContact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsContact" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefContact" KeyMember="Id" />
        <dxe:ASPxButton ID="ASPxButton5" Width="160" runat="server" Text="Add New" AutoPostBack="false" >
            <ClientSideEvents Click="function(s,e){
                                grid_contact.AddNewRow();
                                }" />
        </dxe:ASPxButton>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
            ClientInstanceName="grid_contact" DataSourceID="dsContact" OnBeforePerformDataSelect="grid_contact_BeforePerformDataSelect" OnRowUpdated="grid_contact_RowUpdated" OnRowInserted="grid_contact_RowInserted"
            KeyFieldName="Id" OnInit="grid_contact_Init" OnInitNewRow="grid_contact_InitNewRow" OnRowInserting="grid_contact_RowInserting" OnRowUpdating="grid_contact_RowUpdating" OnRowDeleting="grid_contact_RowDeleting"
            AutoGenerateColumns="False">
            <SettingsEditing Mode="Inline"></SettingsEditing>
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutValue("<%# SafeValue.SafeString(Eval("Name")).Replace("'","") %>","<%# SafeValue.SafeString(Eval("Email")).Replace("'","") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1" Width="150px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="Tel" VisibleIndex="1" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Fax" FieldName="Fax" VisibleIndex="1" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="1" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Mobile" FieldName="Mobile" VisibleIndex="1" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataMemoColumn Caption="Address" PropertiesMemoEdit-Rows="3" FieldName="Address" VisibleIndex="1" Width="200px">
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Department" FieldName="Department" VisibleIndex="1" Width="100px">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="" Text="" />
                            <dxe:ListEditItem Value="Sales" Text="Sales" />
                            <dxe:ListEditItem Value="Operation" Text="Operation" />
                            <dxe:ListEditItem Value="Finance" Text="Finance" />
                            <dxe:ListEditItem Value="Admin" Text="Admin" />
                            <dxe:ListEditItem Value="Others" Text="Others" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataMemoColumn Caption="Remark" FieldName="Remark" VisibleIndex="1" Width="200" EditFormSettings-RowSpan="3">
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataCheckColumn Caption="Default?" FieldName="IsDefault" VisibleIndex="1" SortIndex="0" SortOrder="Descending">
                    <DataItemTemplate>
                        <%# IsDefault(SafeValue.SafeBool(Eval("IsDefault"),true)) %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataCheckColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
