<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonInfo.aspx.cs" Inherits="PagesMaster_PersonInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>PersoInfo</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script>
        function OnCloseCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                grid.Refresh();
            }
            else if (v == "Fail")
                alert("Action Fail,please try again!");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsPersonInfo" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefPersonInfo" KeyMember="Id" />
            <wilson:DataSource ID="dsCountry" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXCountry" KeyMember="Code" />
            <wilson:DataSource ID="dsCity" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXCity" KeyMember="Code" />
            <table width="450">
                <tr>
                    <td>Name
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="200" runat="server" ClientInstanceName="txt_Name">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='PersonInfo.aspx?name='+txt_Name.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsPersonInfo"
                KeyFieldName="Id" AutoGenerateColumns="False"
                Width="1080px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnCustomDataCallback="grid_CustomDataCallback" OnCustomCallback="grid_CustomCallback" OnHtmlEditFormCreated="grid_HtmlEditFormCreated">
                <SettingsEditing Mode="EditForm" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowFilterRow="false" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="PartyId" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2" Width="100" />
                    <dxwgv:GridViewDataTextColumn Caption="ICNo" FieldName="ICNo" VisibleIndex="2" Width="100" />
                    <dxwgv:GridViewDataTextColumn Caption="AccountNo" FieldName="AccountNo" VisibleIndex="3" Width="120" />
                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="Type" VisibleIndex="4" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Address" VisibleIndex="5" Width="300" />
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
                <Templates>
                    <EditForm>
                        <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                            <tr>
                                <td width="90%"></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid.PerformCallback('Save');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Back" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid.PerformCallback('Cancle');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dxtc:ASPxPageControl ID="pageControl" runat="server" Width="100%" Height="200px" EnableCallBacks="true" BackColor="#F0F0F0" ActiveTabIndex="0">
                            <TabPages>
                                <dxtc:TabPage Text="Info" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="display: none">
                                                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                    BackColor="Control" Text='<%# Eval("Id") %>' Width="170">
                                                </dxe:ASPxTextBox>
                                            </div>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <table width="100%">
                                                    <tr>
                                                        <td>Code：
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtPartyId" runat="server" Width="150" Value='<%# Eval("PartyId") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>IC No:</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtICNo" runat="server" EnableIncrementalFiltering="True" Value='<%# Eval("ICNo") %>' Width="180">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Name：
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtName" runat="server" Width="150" Value='<%# Eval("Name") %>'>
                                                            </dxe:ASPxTextBox>
                                                            <dxe:ASPxLabel ID="lblMessage" runat="server" Text=""></dxe:ASPxLabel>
                                                        </td>
                                                        <td>Contact： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtContact" runat="server" Value='<%# Eval("Contact") %>' Width="100">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Type：
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Type"
                                                                runat="server" Text='<%# Eval("Type") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Patient" Value="Patient" />
                                                                    <dxe:ListEditItem Text="Doctor" Value="Doctor" />
                                                                    <dxe:ListEditItem Text="Room" Value="Room" />
                                                                    <dxe:ListEditItem Text="Others" Value="Others" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Address</td>
                                                        <td colspan="5">
                                                            <dxe:ASPxMemo ID="txtAddress" runat="server" Rows="3" Value='<%# Eval("Address") %>' Width="100%"></dxe:ASPxMemo>
                                                        </td>
                                                        <td>Telephone： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtTel" runat="server" Value='<%# Eval("Tel") %>' Width="100"></dxe:ASPxTextBox>
                                                        </td>
                                                        <td>ZipCode： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_ZipCode" runat="server" Value='<%# Eval("ZipCode") %>' Width="100"></dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>AccountNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtAccountNo" runat="server" Value='<%# Eval("AccountNo") %>' Width="150"></dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Hospital</td>
                                                        <td colspan="3">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <td width="94">
                                                                   <dxe:ASPxButtonEdit ID="txt_Customer" ClientInstanceName="txt_Customer" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("RelationId") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_Customer,txt_CustomerName,null,null,null,null,null,null,'CV');
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="235" ReadOnly="true"  BackColor="Control" ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </table>
                                                        </td>
                                                          <td>Country： </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbCountry" Width="100" runat="server" DataSourceID="dsCountry" EnableIncrementalFiltering="true" TextField="Name" Value='<%# Eval("Country") %>' ValueField="Code" ValueType="System.String"></dxe:ASPxComboBox>
                                                        </td>
                                                        <td>City： </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbCity" Width="100" runat="server" DataSourceID="dsCity" EnableIncrementalFiltering="true" TextField="Name" Value='<%# Eval("City") %>' ValueField="Code" ValueType="System.String"></dxe:ASPxComboBox>
                                                        </td>                                                      
                                                    </tr>
                                                </table>
                                            </div>

                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                            <TabStyle BackColor="#F0F0F0">
                            </TabStyle>
                            <ContentStyle BackColor="#F0F0F0">
                            </ContentStyle>
                        </dxtc:ASPxPageControl>

                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
                        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                     
                }" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
