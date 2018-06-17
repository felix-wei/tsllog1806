<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ContractEdit.aspx.cs" Inherits="WareHouse_ContractEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var isUpload = false;
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('Upload.aspx?Type=C&Sn=' + txt_RefNo.GetText());
            popubCtr.Show();
        }
        function CheckRaBtn(rbtBtn, txtSpin1) {
            if (rbtBtn.GetChecked()) {
                txtSpin1.SetEnabled(true);
            }
            else {
                txtSpin1.SetEnabled(false);
            }
        }
    </script>
</head>
<body>
    <wilson:DataSource ID="dsWhContract" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhContract" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsWhContractDet" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhContractDet" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhAttachment" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProductClass" KeyMember="Id" />
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>ContractNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ContractNo" Width="150" runat="server" ClientInstanceName="txt_ContractNo">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='ContractEdit.aspx?no='+txt_ContractNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='ContractList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='ContractEdit.aspx?no=0';
                                            }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="1000px" AutoGenerateColumns="False" OnInit="grid_Init" OnCustomDataCallback="grid_CustomDataCallback" DataSourceID="dsWhContract" OnCustomCallback="grid_CustomCallback" OnHtmlEditFormCreated="grid_HtmlEditFormCreated" OnInitNewRow="grid_InitNewRow">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                                <tr>
                                    <td width="70%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                            detailGrid.PerformCallback('Save');
                                            }" />
                                        </dxe:ASPxButton>
                                    </td>

                                    <td style="display: none">
                                        <dxe:ASPxButton ID="btn_CloseJob" Width="90" runat="server" Text="Close Job" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('CloseJob',OnCloseCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Void" ClientInstanceName="btn_Void" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_Void.GetText()+' Contract?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('Void',OnCloseCallBack);                 
                                                                    }
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="1000px" Height="500px">
                                <TabPages>
                                    <dxtc:TabPage Text="Contract" Visible="true">
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
                                                        <td width="100">ContractNo
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="150" ID="txt_RefNo" ClientInstanceName="txt_RefNo" Text='<%# Eval("ContractNo")%>' ReadOnly="True" BackColor="Control">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="100">Type
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="160" ID="cmb_Type"
                                                                runat="server" Text='<%# Eval("StorageType") %>' DropDownStyle="DropDown">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Weekly" Value="Weekly" />
                                                                    <dxe:ListEditItem Text="Biweekly" Value="Biweekly" />
                                                                    <dxe:ListEditItem Text="Monthly" Value="Monthly" />
                                                                    <dxe:ListEditItem Text="Fixed" Value="Fixed" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>ContractDate</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_Date" runat="server" Value='<%# Eval("ContractDate") %>' Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Customer
                                                        </td>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_PartyId" ClientInstanceName="txt_PartyId" runat="server" Text='<%# Eval("PartyId")%>' Width="150" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                 PopupParty(txt_PartyId,txt_PartyName,null,null,null,null,null,null,'V');
                                                               
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="305px" BackColor="Control" ID="txt_PartyName"
                                                                            ReadOnly="true" ClientInstanceName="txt_PartyName">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>StartDate</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_StartDate" runat="server" Value='<%# Eval("StartDate") %>' Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Warehouse
                                                        </td>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_WarehouseId" ClientInstanceName="txt_WarehouseId" runat="server" Text='<%# Eval("WhCode") %>' Width="150px" HorizontalAlign="Left" AutoPostBack="False">
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupWh(txt_WarehouseId,txt_WhName);
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_WhName" runat="server" BackColor="Control" ClientInstanceName="txt_WhName" ReadOnly="True" Width="305px">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                        <td>Expired Date</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="txt_ExpireDate" runat="server" Value='<%# Eval("ExpireDate") %>' Width="120px" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="txt_Remark" Rows="3" runat="server" Width="465" Text='<%# Eval("Remark") %>'></dxe:ASPxMemo>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="6">
                                                            <hr>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 80px;">Creation</td>
                                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                    <td style="width: 100px;">Last Updated</td>
                                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                    <td style="width: 100px;">Status</td>
                                                                    <td><%# Eval("StatusCode")%> </td>
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="width: 800px;">
                                                    <tr>
                                                        <td style="text-align: left; padding: 2px 2px 2px 2px">
                                                            <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Det" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                                <ClientSideEvents Click="function(s,e){
                                         grid_det.AddNewRow();
                            }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td width="98%">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="WIDTH: 1000px; overflow-y: scroll;">
                                                                <dxwgv:ASPxGridView ID="grid_det" ClientInstanceName="grid_det" Styles-Cell-HorizontalAlign="Left"
                                                                    runat="server" DataSourceID="dsWhContractDet" KeyFieldName="Id" OnHtmlEditFormCreated="grid_det_HtmlEditFormCreated"
                                                                    OnBeforePerformDataSelect="grid_det_BeforePerformDataSelect" OnRowUpdating="grid_det_RowUpdating" OnCellEditorInitialize="grid_det_CellEditorInitialize"
                                                                    OnRowInserting="grid_det_RowInserting" OnInitNewRow="grid_det_InitNewRow" OnInit="grid_det_Init" OnRowDeleting="grid_det_RowDeleting"
                                                                    Width="1200"
                                                                    AutoGenerateColumns="False">
                                                                    <SettingsEditing Mode="Inline" />
                                                                    <SettingsPager Mode="ShowAllRecords" />
                                                                    <SettingsBehavior ConfirmDelete="true" />
                                                                    <Columns>
                                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                                            <DataItemTemplate>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxButton ID="btn_DoDet_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                                ClientSideEvents-Click='<%# "function(s) { grid_det.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxButton ID="btn_DoDet_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_det.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </DataItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxButton ID="btn_DoDet_update" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                                Text="Update" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { grid_det.UpdateEdit(); }"  %>'>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxButton ID="btn_DoDet_cancle" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                                ClientSideEvents-Click='<%# "function(s) { grid_det.CancelEdit(); }"  %>'>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataColumn>
                                                                        <dxwgv:GridViewDataTextColumn
                                                                            Caption="Product" FieldName="ProductCode" VisibleIndex="3" Width="80">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxButtonEdit ID="txt_Product" ClientInstanceName="txt_Product" runat="server" Value='<%# Bind("ProductCode") %>' Width="120">
                                                                                    <Buttons>
                                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                    </Buttons>
                                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                              PopupProductContract(txt_Product,null);
                                                                           }" />
                                                                                </dxe:ASPxButtonEdit>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn Caption="Product Class" FieldName="ProductClass" VisibleIndex="3" Width="50">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxComboBox ID="cbProductClass" ClientInstanceName="cbProductClass" Width="155px" TextFormatString="{0}" IncrementalFilteringMode="StartsWith"
                                                                                    CallbackPageSize="30" runat="server" DataSourceID="dsProductClass" EnableCallbackMode="True" EnableIncrementalFiltering="true" TextField="Code" Value='<%# Bind("ProductClass") %>' ValueField="Code" ValueType="System.String">
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                                                        <dxe:ListBoxColumn FieldName="Description" Width="100%" />
                                                                                    </Columns>
                                                                                </dxe:ASPxComboBox>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn
                                                                            Caption="ChgCode" FieldName="ChgCode" VisibleIndex="3" Width="80">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxButtonEdit ID="txt_ChgCode" ClientInstanceName="txt_ChgCode" runat="server" Value='<%# Bind("ChgCode") %>' Width="120">
                                                                                    <Buttons>
                                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                    </Buttons>
                                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupChgCode(txt_ChgCode,null,null,null,null,null,null);
                                                                           }" />
                                                                                </dxe:ASPxButtonEdit>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <%--                                                                        <dxwgv:GridViewDataTextColumn
                                                                            Caption="Handling Fee" FieldName="HandlingFee" VisibleIndex="5" Width="80" Visible="false">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_HandlingFee"
                                                                                    ClientInstanceName="spin_det_HandlingFee" 
                                                                                    runat="server" Value='<%# Bind("HandlingFee") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn
                                                                            Caption="Storage Fee" FieldName="StorageFee" VisibleIndex="5" Width="80" Visible="false">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_StorageFee"
                                                                                    ClientInstanceName="spin_det_StorageFee" 
                                                                                    runat="server" Value='<%# Bind("StorageFee") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>--%>
                                                                        <dxwgv:GridViewDataTextColumn
                                                                            Caption="Rate" FieldName="Price1" VisibleIndex="5" Width="80">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Price1"
                                                                                    ClientInstanceName="spin_det_Price1"
                                                                                    runat="server" Value='<%# Bind("Price1") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <%--                                                                         <dxwgv:GridViewDataTextColumn
                                                                            Caption="Whole Fee" FieldName="Price2" VisibleIndex="5" Width="80" Visible="false">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Price2"
                                                                                    ClientInstanceName="spin_det_Price2" 
                                                                                    runat="server" Value='<%# Bind("Price2") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                         <dxwgv:GridViewDataTextColumn
                                                                            Caption="Loose Fee" FieldName="Price3" VisibleIndex="5" Width="80" Visible="false">
                                                                            <EditItemTemplate>
                                                                                <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_Price3"
                                                                                    ClientInstanceName="spin_det_Price3"
                                                                                    runat="server" Value='<%# Bind("Price3") %>' DisplayFormatString="0.00" DecimalPlaces="2">

                                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                                </dxe:ASPxSpinEdit>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>--%>
                                                                        <%--                                                                        <dxwgv:GridViewDataCheckColumn Caption="Fixed" FieldName="IsFixed" VisibleIndex="13" Width="50" Visible="false">
                                                                        </dxwgv:GridViewDataCheckColumn>--%>
                                                                        <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="13" Width="100">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn Caption="Fee Schedule" VisibleIndex="14" Visible="false">
                                                                            <DataItemTemplate>
                                                                                <%# Eval("SchStr") %>
                                                                            </DataItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="display: none">
                                                                                            <dxe:ASPxRadioButton ID="rbt_Yearly" GroupName="rbt" ClientInstanceName="rbt_Yearly" runat="server" Text="Yearly" Value='<%# Bind("IsYearly") %>'>
                                                                                                <ClientSideEvents CheckedChanged="function(s,e){
                                                                                            }" />
                                                                                            </dxe:ASPxRadioButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxRadioButton ID="rbt_Monthly" GroupName="rbt" ClientInstanceName="rbt_Monthly" runat="server" Text="Monthly" Value='<%# Bind("IsMonthly") %>'>
                                                                                                <ClientSideEvents CheckedChanged="function(s,e){
                                                                                            }" />
                                                                                            </dxe:ASPxRadioButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxRadioButton ID="rbt_Weekly" GroupName="rbt" ClientInstanceName="rbt_Weekly" runat="server" Text="Weekly" Value='<%# Bind("IsWeekly") %>'>
                                                                                                <ClientSideEvents CheckedChanged="function(s,e){
                                                                                            }" />
                                                                                            </dxe:ASPxRadioButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxRadioButton ID="rbt_Daily" GroupName="rbt" ClientInstanceName="rbt_Daily" runat="server" Text="Daily" Value='<%# Bind("IsDaily") %>'>
                                                                                                <ClientSideEvents CheckedChanged="function(s,e){
                                                                                            CheckRaBtn(rbt_Daily,spin_det_DailyNo);
                                                                                            }" />
                                                                                            </dxe:ASPxRadioButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_det_DailyNo" ClientEnabled='<%# SafeValue.SafeBool(Eval("IsDaily"),false) %>'
                                                                                                ClientInstanceName="spin_det_DailyNo" runat="server" Value='<%# Bind("DailyNo") %>'>
                                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                                            </dxe:ASPxSpinEdit>

                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </EditItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                    </Columns>
                                                                </dxwgv:ASPxGridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl8" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <td>
                                                                <dxe:ASPxButton ID="ASPxButton13" Width="100%" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' runat="server" Text="Upload Attachments"
                                                                    AutoPostBack="false"
                                                                    UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s,e) {
                                                                         isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                                    UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsAttachment"
                                                    KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                    <Settings />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                    ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                    Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>Remark
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'>
                                                                                <ClientSideEvents Click="function(s,e){grd_Photo.UpdateEdit();}" />
                                                                            </dxe:ASPxHyperLink>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>

                            </dxtc:ASPxPageControl>
                        </div>

                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="800" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload)
	    grd_Photo.Refresh();
}" />
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="980" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    detailGrid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>

    </form>
</body>
</html>
