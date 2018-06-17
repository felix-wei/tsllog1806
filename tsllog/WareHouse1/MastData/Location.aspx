<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Location.aspx.cs" Inherits="WareHouse_MastData_Location" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Location</title>
            <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefLocation" KeyMember="Id"/>
            <table width="500">
                <tr>
                    <td>WareHouse Name
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="160" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='Location.aspx?name='+txt_Name.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsRefLocation"
                KeyFieldName="Id" AutoGenerateColumns="False"
                Width="1000px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnCustomCallback="grid_CustomCallback" OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting" OnHtmlEditFormCreated="grid_HtmlEditFormCreated">
                <SettingsEditing Mode="EditForm" PopupEditFormWidth="750" NewItemRowPosition="Bottom" />
                <SettingsPager PageSize="10" Mode="ShowPager">
                </SettingsPager>
                <Settings ShowFilterRow="false" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" Visible="false" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" />
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
                    <dxwgv:GridViewDataTextColumn Caption="Warehouse Code" FieldName="WarehouseCode" VisibleIndex="4" />
                    <dxwgv:GridViewDataTextColumn Caption="Zone Code" FieldName="ZoneCode" VisibleIndex="5" />
                    <dxwgv:GridViewDataTextColumn Caption="Store Code" FieldName="StoreCode" VisibleIndex="6" />
                    <dxwgv:GridViewDataTextColumn Caption="Length" FieldName="Length" VisibleIndex="7" />
                    <dxwgv:GridViewDataTextColumn Caption="Width" FieldName="Width" VisibleIndex="8" />
                    <dxwgv:GridViewDataTextColumn Caption="Height" FieldName="Height" VisibleIndex="9" />
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
                <Templates>
                    <EditForm>
                        <dxtc:ASPxPageControl ID="pageControl" runat="server" Width="100%" EnableCallBacks="true" BackColor="#F0F0F0">
                            <TabPages>
                                <dxtc:TabPage Text="Info" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="display:none">
                                                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                    BackColor="Control" Text='<%# Eval("Id") %>' Width="170">
                                                </dxe:ASPxTextBox>
                                            </div>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <table width="70%">
                                                    <tr>
                                                        <td>Code：
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Code" runat="server" Width="100%" Text='<%# Bind("Code") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Name:</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Name" runat="server" EnableIncrementalFiltering="True" Text='<%# Bind("Name") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td>Party
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server" Text='<%# Bind("PartyId") %>' Width="100%"
                                                                HorizontalAlign="Left" AutoPostBack="False">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupCust(txt_PartyId,txt_PartyName);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td colspan="2">
                                                            <dxe:ASPxTextBox ID="txt_PartyName" ClientInstanceName="txt_PartyName" runat="server" BackColor="Control" ReadOnly="True" Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>Warehouse Code
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_WarehouseCode" ClientInstanceName="txt_WarehouseCode" runat="server" Text='<%# Bind("WarehouseCode") %>' Width="100%"
                                                                HorizontalAlign="Left" AutoPostBack="False">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupWh(txt_WarehouseCode,txt_WarehouseName);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td colspan="2"><dxe:ASPxTextBox ID="txt_WarehouseName" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_WarehouseName" runat="server" Width="100%">
                                                            </dxe:ASPxTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>ZoneCode:</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="btn_ZoneCode" ClientInstanceName="btn_ZoneCode" runat="server" Width="100%" Text='<%# Bind("ZoneCode") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                           PopupZoneCodeFromWh(btn_ZoneCode,null);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>StoreCode</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="btn_StoreCode" ClientInstanceName="btn_StoreCode" Width="100%" runat="server" Text='<%# Bind("StoreCode") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupStoreCodeFromWh(btn_StoreCode,null);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Length： </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="txt_Length" runat="server" Text='<%# Bind("Length") %>' Width="100%" Increment="0"  DisplayFormatString="0.000" DecimalPlaces="3">
                                                              <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Width： </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="txt_Width" runat="server" Text='<%# Bind("Width") %>' Width="100%" Increment="0"  DisplayFormatString="0.000" DecimalPlaces="3">
                                                              <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Height： </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="txt_Height" runat="server" Text='<%# Bind("Height") %>' Width="100%" Increment="0"  DisplayFormatString="0.000" DecimalPlaces="3">
                                                              <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>SpaceM3： </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="txt_SpaceM3" runat="server" Text='<%# Bind("SpaceM3") %>' Width="100%" Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remarks： </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Remark" runat="server" Rows="3" Text='<%# Bind("Remark") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>LocLevel：</td>
                                                        <td colspan="3">
                                                           <dxe:ASPxTextBox ID="txt_Loclevel" runat="server" Text='<%# Bind("LocLevel") %>' Width="100%">
                                                            </dxe:ASPxTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                                <hr>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 80px;">Creation</td>
                                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                    <td style="width: 100px;">Last Updated</td>
                                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    <tr>
                                                        <td colspan="4">
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>

                                                            </div>
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
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
