<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="WareHouse_Inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inventory</title>
        <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <wilson:DataSource ID="dsWhInventory" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhInventory" KeyMember="Id" />
    <wilson:DataSource ID="dsWhInventoryDet" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhInventoryDet" KeyMember="Id" />
    <form id="form1" runat="server">
        <div>
            <table width="450">
                <tr>
                    <td>Inventory No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='Transfer.aspx?name='+txt_Name.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
                    KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" OnInit="grid_Init" Theme="DevEx" DataSourceID="dsWhInventory" OnHtmlEditFormCreated="grid_HtmlEditFormCreated" OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsEditing Mode="EditForm" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior ConfirmDelete="True" />
                    <Columns>
                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                            <EditButton Visible="true"></EditButton>
                            <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="1" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="InventoryNo" FieldName="InventoryNo" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="InventoryDate" FieldName="InventoryDate" VisibleIndex="2" Width="150">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="InventoryUser" FieldName="InventoryUser" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CreateBy" FieldName="CreateBy" VisibleIndex="4" Width="100">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CreateDateTime" FieldName="CreateDateTime" VisibleIndex="5" Width="100">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <EditForm>
                            <div style="padding: 2px 2px 2px 2px">
                                <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" Height="140px">
                                    <TabPages>
                                        <dxtc:TabPage Text=" " Visible="true">
                                            <ContentCollection>
                                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                                    <div style="display: none">
                                                        <dxe:ASPxTextBox runat="server" Width="60" ID="txt_Id" ClientInstanceName="txt_Id"
                                                            Text='<%# Eval("Id") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </div>
                                                    <table border="0" width="900">
                                                        <tr>
                                                            <td width="100"></td>
                                                            <td width="180"></td>
                                                            <td width="100"></td>
                                                            <td width="180"></td>
                                                            <td width="100"></td>
                                                            <td width="180"></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100">Inventory No
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_InventoryNo" ClientInstanceName="txt_InventoryNo" Text='<%# Bind("InventoryNo")%>'>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td width="100">InventoryDate</td>
                                                            <td>
                                                                <dxe:ASPxDateEdit ID="date_InventoryDate" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"  Value='<%# Bind("InventoryDate") %>' Width="165px">
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                            <td>InventoryUser</td>
                                                            <td> <dxe:ASPxTextBox ID="txt_InventoryUser" runat="server" ClientInstanceName="txt_PartyRefNo" Width="165px" Text='<%# Bind("InventoryUser")%>'>
                                                                </dxe:ASPxTextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 100px;">CreateBy</td>
                                                                        <td style="width: 70px"><%# Eval("CreateBy") %></td>
                                                                        <td style="width: 100px;">CreateDateTime</td>
                                                                        <td style="width: 120px; text-align: center"><%# SafeValue.SafeDateStr( Eval("CreateDateTime")) %></td>
                                                                        <td style="width: 60px;">UpdateBy</td>
                                                                        <td style="width: 120px; text-align: center"><%# Eval("UpdateBy") %></td>
                                                                        <td style="width: 100px;">UpdateDateTime</td>
                                                                        <td style="text-align: center"><%# SafeValue.SafeDateStr(Eval("UpdateDateTime")) %></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">
                                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxw:ContentControl>
                                            </ContentCollection>
                                        </dxtc:TabPage>
                                    </TabPages>
                                </dxtc:ASPxPageControl>
                            </div>
                            <table>
                                <tr>
                                    <td style="text-align: right; padding: 2px 2px 2px 2px"></td>
                                    <td style="text-align: right; padding: 2px 2px 2px 2px">
                                        <dxe:ASPxButton ID="btn_DetAdd" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' Text="Add Det" AutoPostBack="false" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e){
                                         grid_det.AddNewRow();
                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="text-align: right; padding: 2px 2px 2px 2px"></td>
                                </tr>
                            </table>
                            <table width="800">
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="grid_det" ClientInstanceName="grid_det"
                                            runat="server" DataSourceID="dsWhInventoryDet" KeyFieldName="Id"
                                            OnBeforePerformDataSelect="grid_det_BeforePerformDataSelect" OnRowUpdating="grid_det_RowUpdating"
                                            OnRowInserting="grid_det_RowInserting" OnInitNewRow="grid_det_InitNewRow" OnInit="grid_det_Init" OnRowDeleting="grid_det_RowDeleting"
                                            Width="100%"
                                            AutoGenerateColumns="False">
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <SettingsEditing Mode="EditForm" />
                                            <SettingsCustomizationWindow Enabled="True" />
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn
                                                    VisibleIndex="0" Width="50">
                                                    <EditButton Visible="true"></EditButton>
                                                    <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn
                                                    Caption="Product" FieldName="Product" VisibleIndex="3" Width="80">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn
                                                    Caption="Qty" FieldName="Qty" VisibleIndex="5" Width="80">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn
                                                    Caption="Unit" FieldName="Unit" VisibleIndex="5" Width="80">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn
                                                    Caption="AdjustQty" FieldName="AdjustQty" VisibleIndex="5" Width="80">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" />
                                            <Templates>
                                                <EditForm>
                                                    <div style="display:none">
                                                        <dxe:ASPxTextBox Width="150px" ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'>
                                                                </dxe:ASPxTextBox>
                                                    </div>
                                                    <table style="border-bottom: solid 1px black;">
                                                        <tr>
                                                            <td colspan="4">
                                                                <table>
                                                                    <tr>
                                                                        <td width="70">
                                                                         Product
                                                                        </td>
                                                                        <td>
                                                                              <dxe:ASPxButtonEdit ID="btn_Product" ClientInstanceName="btn_Product" runat="server" Text='<%# Bind("Product")%>' Width="155">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupProduct(btn_Product,null);
                                                                   }" />
                                                                </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                        <td>Qty</td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit Width="60" ID="spin_det_Qty"
                                                                                ClientInstanceName="spin_det_Qty" runat="server"   Increment="0" Value='<%# Bind("Qty") %>'>
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td></td>
                                                                        <td>
                
                                                                        </td>
                                                                        <td>Unit</td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox Width="60" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                                                runat="server" Text='<%# Bind("Unit") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                              <td>AdjustQty</td>
                                                            <td>
                                                                <dxe:ASPxSpinEdit Width="60" ID="spin_AdjustQty" Increment="0" ClientInstanceName="spin_AdjustQty"
                                                                                 runat="server" Text='<%# Bind("AdjustQty") %>'>
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Warehouse
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButtonEdit ID="btn_WareHouse" ClientInstanceName="btnWareHouse" runat="server" Text='<%# Bind("WarehouseId")%>' Width="155">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupWhLo(btnWareHouse,null);
                                                                   }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </td>
                                                            <td width="120px">
                                                               Location
                                                            </td>                                                            
                                                            <td>
                                                          <dxe:ASPxButtonEdit ID="btn_LocId" ClientInstanceName="btn_LocId" runat="server" Text='<%# Bind("LocId")%>' Width="155">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupWhLo(btn_LocId,null);
                                                                   }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4"
                                                                style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton1"
                                                                    ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton1"
                                                                    ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditForm>
                                            </Templates>
                                        </dxwgv:ASPxGridView>
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
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
