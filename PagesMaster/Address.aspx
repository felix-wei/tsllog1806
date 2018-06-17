<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Address.aspx.cs" Inherits="PagesMaster_AddressList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
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
    <form runat="server">
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Code" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_Code" Width="180" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td><dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Type" /></td>
                <td>
                    <dxe:ASPxComboBox ID="cbb_TypeId" runat="server" Width="80">
                           <Items>
                               <dxe:ListEditItem  Text="Location" Value="Location"/>
                               <dxe:ListEditItem  Text="Owner" Value="Owner"/>
                               <dxe:ListEditItem  Text="Yard" Value="Yard"/>
                           </Items>
                        </dxe:ASPxComboBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                        OnClick="btn_Sch_Click">
                    </dxe:ASPxButton>
                </td>
                 <td>
                     <dxe:ASPxButton ID="ASPxButton1" Width="160" runat="server" Enabled='True' Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="Id" FilterExpression="CodeType='3'" />
        <wilson:DataSource ID="dsAddress" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefAddress" KeyMember="Id" />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" Width="100%" DataSourceID="dsAddress"
            KeyFieldName="Id" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting"
            AutoGenerateColumns="False">
            <SettingsPager Mode="ShowPager" PageSize="20">
            </SettingsPager>
            <SettingsBehavior ConfirmDelete="true" />
            <SettingsEditing Mode="Inline"></SettingsEditing>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                 <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Address" Width="120" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataMemoColumn Caption="Address" PropertiesMemoEdit-Rows="3" FieldName="Address1" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="350px">
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataTextColumn Caption="Client" FieldName="PartyId" VisibleIndex="2" Width="30%">
                    <DataItemTemplate>
                        <%# Eval("PartyName") %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Text='<%# Bind("PartyId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" Text='<%# Bind("PartyName") %>'></dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>

                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Postcode" FieldName="Postcode" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="TypeId" VisibleIndex="1" Width="80">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cbb_TypeId" runat="server" Text='<%# Bind("TypeId") %>' Width="80">
                           <Items>
                               <dxe:ListEditItem  Text="Location" Value="Location"/>
                               <dxe:ListEditItem  Text="Owner" Value="Owner"/>
                               <dxe:ListEditItem  Text="Yard" Value="Yard"/>
                           </Items>
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="1" Width="80">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cbb_Location" runat="server" Text='<%# Bind("Location") %>' Width="80" DataSourceID="dsUom">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                
            </Columns>
        </dxwgv:ASPxGridView>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
                Width="1050" EnableViewState="False">
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
