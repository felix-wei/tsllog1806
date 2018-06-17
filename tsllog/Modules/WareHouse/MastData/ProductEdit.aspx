<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="ProductEdit.aspx.cs" Inherits="WareHouse_MastData_ProductEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Product</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
            <script type="text/javascript">
                var isUpload = false;
    </script>
    <script type="text/javascript" >
        function OnCloseCallBack(v) {
            if (v == "Success") {
                alert("Successful");
                detailGrid.Refresh();
            } else if (v == "Fail") {
                alert("Fail");
            } else if (v == "Save") {
                detailGrid.Refresh();
            }
            else if (v != null) {
                window.location = 'ProductEdit.aspx?no=' + v;
            }
        }
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('../Upload.aspx?Type=Product&Sn=' + txt_ProductCode.GetText());
            popubCtr.Show();
        }
    </script>
</head>
<body>
     <wilson:DataSource ID="dsProduct" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProduct" KeyMember="Id" FilterExpression="1=0"/>
    <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProductClass" KeyMember="Id" />
    <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='Attribute'" />
        <wilson:DataSource ID="dsPhoto" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhAttachment"
        KeyMember="Id" FilterExpression="1=0" />
    <form id="form1" runat="server">
        <div>
            <table width="450">
                <tr>
                    <td>Code
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Code" Width="120" runat="server" ClientInstanceName="txt_Code">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='ProductEdit.aspx?no='+txt_Code.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='Product.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                window.location='ProductEdit.aspx?no=0';
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="detailGrid" DataSourceID="dsProduct"
                    KeyFieldName="Id" AutoGenerateColumns="False"
                    Width="1500px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" 
                    OnCustomCallback="grid_CustomCallback" OnCustomDataCallback="grid_CustomDataCallback" 
                    OnHtmlEditFormCreated="grid_HtmlEditFormCreated" OnRowDeleting="grid_RowDeleting"
                     OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting">
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsEditing Mode="EditForm" />
                    <Settings ShowColumnHeaders="false"  />
                    <Templates>
                        <EditForm>
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 980px">
                                <tr>
                                    <td width="95%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                            detailGrid.GetValuesOnCustomCallback('Save',OnCloseCallBack);
                                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                       <%-- <dxe:ASPxButton ID="btn_Block" ClientInstanceName="btn_Block" runat="server" Width="100" Text="Block" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                if(confirm('Confirm '+btn_Block.GetText()+' this Product?'))
                                                {
                                                    detailGrid.GetValuesOnCustomCallback('Block',OnCloseCallBack);                 
                                                }
                                            }" />
                                        </dxe:ASPxButton>--%>
                                    </td>
                                    <td>
                                       <%-- <dxe:ASPxButton ID="btn_Cancle" ClientInstanceName="btn_Cancle" runat="server" Width="100" Text="Cancel" AutoPostBack="False"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
                                                  detailGrid.CancelEdit();   
                                        }" />
                                        </dxe:ASPxButton>--%>
                                    </td>
                                </tr>
                            </table>
                                                    <div style="display: none"> 
                                                        <dxe:ASPxTextBox runat="server" Width="60" ID="txt_Id" ClientInstanceName="txt_Id"
                                                            Text='<%# Eval("Id") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </div>
                                                    <table border="0" width="900">
                                                        <tr>
                                                            <td width="100">Code </td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="155" ID="txt_ProductCode" ClientInstanceName="txt_ProductCode" Text='<%# Eval("Code")%>'></dxe:ASPxTextBox>
                                                            </td>
                                                            <td width="100">Name</td>
                                                            <td>
                                                                <dxe:ASPxTextBox ID="txt_Name" runat="server" ClientsInstanceName="txt_Name" Width="180px" Text='<%# Eval("Name")%>'></dxe:ASPxTextBox>
                                                            </td>
                                                            <td>Product Class</td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="cbProductClass" Width="155px" TextFormatString="{0}" IncrementalFilteringMode="StartsWith"
                                                                    CallbackPageSize="30" runat="server" DataSourceID="dsProductClass" EnableCallbackMode="True" EnableIncrementalFiltering="true" TextField="Code" Value='<%# Bind("ProductClass") %>' ValueField="Code" ValueType="System.String">
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                                        <dxe:ListBoxColumn FieldName="Description" Width="100%" />
                                                                    </Columns>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Supplier</td>
                                                            <td colspan="3">
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="btnParty" ClientInstanceName="txt_PartyId" runat="server" Text='<%# Eval("PartyId")%>' Width="155">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupParty(txt_PartyId,txt_PartyName,null,null,null,null,null,null,'V');
                                                                   }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" BackColor="Control" ReadOnly="true" Width="300px" ID="txt_PartyName" ClientInstanceName="txt_PartyName"></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>Brand Name</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="155" ID="txt_BrandName" Text='<%# Eval("BrandName")%>'></dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td rowspan="2">Description</td>
                                                            <td colspan="3" rowspan="2">
                                                                <dxe:ASPxMemo ID="txt_Description" runat="server" Rows="1" Text='<%# Eval("Description") %>' Height="20" Width="475"></dxe:ASPxMemo>
                                                            </td> </tr>
                                                        <tr>
                                                            <td>Type</td>
                                                            <td>
                                                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="160" ID="cmb_OptionType" runat="server" Text='<%# Eval("OptionType") %>' DropDownStyle="DropDown">
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="All" Value="All" />
                                                                        <dxe:ListEditItem Text="Normal" Value="Normal" />
                                                                        <dxe:ListEditItem Text="Bonded" Value="Bonded" />
                                                                        <dxe:ListEditItem Text="Licenced" Value="Licenced" />
                                                                    </Items>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td colspan="3" rowspan="5" align="left">
                                                                <table width="100%" border="0" height="auto">
                                                                    <tr>
                                                                        <td>Packing</td>
                                                                        <td>Whole</td>
                                                                        <td>Loose</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_LengthPacking" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("LengthPacking") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_LengthWhole" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("LengthWhole") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_LengthLoose" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("LengthLoose") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_WidthPacking" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("WidthPacking") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_WidthWhole" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("WidthWhole") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_WidthLoose" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("WidthLoose") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_HeightPacking" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("HeightPacking") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_HeightWhole" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("HeightWhole") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_HeightLoose" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("HeightLoose") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_VolumePacking" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("VolumePacking") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_VolumeWhole" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("VolumeWhole") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_VolumeLoose" runat="server" DisplayFormatString="0.000" Increment="0" DecimalPlaces="3" Text='<%# Eval("VolumeLoose") %>' Width="100px">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Length</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Width</td>
                                                            <td>Cost Price</td>
                                                            <td>
                                                                <dxe:ASPxSpinEdit Increment="0" Width="155" ID="spin_CostPrice" ClientInstanceName="spin_CostPrice"
                                                                    DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="false" runat="server" Text='<%# Eval("CostPrice") %>'>
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Height</td>
                                                            <td>Sale Price</td>
                                                            <td>
                                                                <dxe:ASPxSpinEdit Increment="0" Width="155" ID="spin_Saleprice" ClientInstanceName="spin_Saleprice"
                                                                    DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="false" runat="server" Text='<%# Eval("Saleprice") %>'>
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Volume</td>
                                                            <td>HS Code</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="155" ID="txt_HsCode" ClientInstanceName="txt_HsCode" Text='<%# Eval("HsCode")%>'></dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>UOM</td>
                                                            <td colspan="3" >
                                                                
                                                                <table width="100%" border="0" height="auto">
                                                                    <tr>

                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="spin_UomPacking" ClientInstanceName="spin_UomPacking" runat="server"
                                                                                Width="100px" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("UomPacking") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupUom(spin_UomPacking,2);
                                                                                    }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="spin_UomWhole" ClientInstanceName="spin_UomWhole" runat="server"
                                                                                Width="100px" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("UomWhole") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupUom(spin_UomWhole,2);
                                                                                    }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="spin_UomLoose" ClientInstanceName="spin_UomLoose" runat="server"
                                                                                Width="100px" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("UomLoose") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupUom(spin_UomLoose,2);
                                                                                    }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    </table>
                                                            </td>
                                                           <td width="100">Base UOM</td>
                                                            <td>
                                                                <dxe:ASPxButtonEdit ID="txt_UomBase" ClientInstanceName="txt_UomBase" runat="server"
                                                                    Width="155px" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("UomBase") %>'>
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                                PopupUom(txt_UomBase,2);
                                                                                    }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>1 Packing=</td>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="155">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="txt_QtyPackingWhole" runat="server" Text='<%# Eval("QtyPackingWhole") %>'
                                                                                Width="90px" Increment="0" IDecimalPlaces="0" DisplayFormatString="0">
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="65px">Whole</td>
                                                                    </tr>
                                                                </table>
                                                                
                                                                
                                                            </td>
                                                            <td>1 Whole=
                                                            </td>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="155">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit Increment="0" IDecimalPlaces="0" DisplayFormatString="0" runat="server" Width="90px" ID="txt_QtyWholeLoose"
                                                                                Text='<%# Eval("QtyWholeLoose")%>'>
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="65px">Loose</td>
                                                                    </tr>
                                                                </table>
                                                                
                                                                
                                                            </td>
                                                            <td>1 Loose=</td>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="155">
                                                                    <tr>
                                                                        <td>
                                                                             <dxe:ASPxSpinEdit Increment="0" IDecimalPlaces="0" DisplayFormatString="0" runat="server" Width="120px" ID="txt_QtyLooseBase" NumberType="Integer"
                                                                                Text='<%# Eval("QtyLooseBase")%>'>
                                                                                <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="35px">Base</td>
                                                                    </tr>
                                                                </table>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            
                                                            <td>
                                                              Att1
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="cb_Att1" Width="155" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Eval("Att4") %>' DropDownStyle="DropDown">
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td>
                                                              Att2
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="cb_Att2" Width="155" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Eval("Att5") %>' DropDownStyle="DropDown">
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                             <td>
                                                              Att3
                                                            </td>
                                                            <td>
                                                               <dxe:ASPxComboBox ID="cb_Att3" Width="150" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Eval("Att6") %>' DropDownStyle="DropDown">
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                              Att4
                                                            </td>
                                                            <td>
                                                              <dxe:ASPxComboBox ID="cb_Att4" Width="155" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Eval("Att7") %>' DropDownStyle="DropDown">
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td>Att5
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="cb_Att5" Width="155" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Eval("Att8") %>' DropDownStyle="DropDown">
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td>Att6
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="cb_Att6" Width="150" DataSourceID="dsWhMastData" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Eval("Att9") %>' DropDownStyle="DropDown">
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>AR Code</td>
                                                            <td><dxe:ASPxTextBox runat="server" Width="155" ID="txt_ArCode" Text='<%# Eval("ArCode")%>'></dxe:ASPxTextBox></td>
                                                       
                                                           <td>AP Code</td>
                                                            <td><dxe:ASPxTextBox runat="server" Width="155" ID="txt_ApCode" Text='<%# Eval("ApCode")%>'></dxe:ASPxTextBox></td>
                                                             <td>Part No</td>
                                                            <td>
                                                                <dxe:ASPxTextBox runat="server" Width="155" ID="txt_PartNo" Text='<%# Eval("PartNo")%>'></dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Reorder Qty</td>
                                                            <td>
                                                                <dxe:ASPxSpinEdit Increment="0" IDecimalPlaces="0" DisplayFormatString="0" runat="server" Width="155px" ID="spin_MinOrderQty" NumberType="Integer"
                                                                    Text='<%# Eval("MinOrderQty")%>'>
                                                                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                </dxe:ASPxSpinEdit>
                                                            </td>
                                                            <td>NewOrderQty</td>
                                                            <td>
                                                                <dxe:ASPxSpinEdit Increment="0" IDecimalPlaces="0" DisplayFormatString="0" runat="server" Width="155px" ID="spin_NewOrderQty" NumberType="Integer"
                                                                    Text='<%# Eval("NewOrderQty")%>'>
                                                                    <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                </dxe:ASPxSpinEdit>
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
                                                        <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Upload Attachments" AutoPostBack="false" UseSubmitBehavior="false"
                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                                    isUpload=true;
                                                                    PopupUploadPhoto();
                                                                }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                                    grd_Photo.Refresh();
                                                                }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>

                            <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsPhoto"
                                KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
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
                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                    </EditForm>
                                </Templates>
                            </dxwgv:ASPxGridView>
                        </EditForm>
                    </Templates>
                </dxwgv:ASPxGridView>
                 <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grd_Photo.Refresh();
                }" />
            </dxpc:ASPxPopupControl>
            </div>
        </div>

    </form>
</body>
</html>
