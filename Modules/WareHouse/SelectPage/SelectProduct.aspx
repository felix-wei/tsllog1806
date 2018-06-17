<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectProduct.aspx.cs" Inherits="Modules_Warehouse_SelectPage_SelectProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
          <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Product</title>
            <script type="text/javascript">
                function $(s) {
                    return document.getElementById(s) ? document.getElementById(s) : s;
                }
                function keydown(e) {
                    if (e.keyCode == 27) { parent.ClosePopupCtr(); }
                }
                document.onkeydown = keydown;
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefProductClass" KeyMember="Id" />
        <wilson:DataSource ID="dsProduct" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefProduct" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='Attribute'" />
        <wilson:DataSource ID="dsPhoto" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhAttachment"
            KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table style="width:400px">
                <tr>
                     <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(txt_CustId,txt_CustName,null,null,null,null,null,null,'C');
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                     <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="230" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    
                   
                    <td>
                        <dxe:ASPxLabel ID="lbl_SKU" Width="50" runat="server" Text="SKU">
                        </dxe:ASPxLabel>
                        
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lbl_Type" Width="80" runat="server" Text="ProductClass">
                        </dxe:ASPxLabel>
                        
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbProductClass" Width="120px" TextFormatString="{0}" IncrementalFilteringMode="StartsWith"
                            CallbackPageSize="30" runat="server" DataSourceID="dsProductClass" EnableCallbackMode="True" EnableIncrementalFiltering="true" TextField="Code" ValueField="Code" ValueType="System.String">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                <dxe:ListBoxColumn FieldName="Description" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve" UseSubmitBehavior="false"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" OnInit="grid_Init" DataSourceID="dsProduct" ClientInstanceName="grid" OnInitNewRow="grid_InitNewRow" 
                KeyFieldName="Id"  OnCustomCallback="ASPxGridView1_CustomCallback"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing  Mode="EditForm"/>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutSku(new Array("<%# Eval("Code") %>","<%# Eval("Description") %>","<%# Eval("UomPacking") %>","<%# Eval("UomWhole") %>","<%# Eval("UomLoose") %>","<%# Eval("UomBase") %>","<%# Eval("QtyPackingWhole") %>","<%# Eval("QtyWholeLoose") %>","<%# Eval("QtyLooseBase")%>","<%# Eval("Att4")%>","<%# Eval("Att5") %>","<%# Eval("Att6") %>","<%# Eval("Att7") %>","<%# Eval("Att8") %>","<%# Eval("Att9") %>","<%# Eval("Att10") %>","<%# Eval("Att11") %>","<%# Eval("Att12") %>","<%# Eval("Att13") %>"));'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                      <dxwgv:GridViewDataComboBoxColumn Caption="Att1" FieldName="Att4"  ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att2" FieldName="Att5" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Att3" FieldName="Att6" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Att1" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Att13" VisibleIndex="10">
                    </dxwgv:GridViewDataTextColumn>

                </Columns>
                <Templates>
                        <EditForm>
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 980px">
                                <tr>
                                    <td width="95%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                            grid.PerformCallback('Save');
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
                        </EditForm>
                    </Templates>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridView ID="ASPxGridView2" runat="server" Width="100%" OnInit="grid_Init" DataSourceID="dsProduct" ClientInstanceName="detailGrid" OnInitNewRow="grid_InitNewRow" 
                KeyFieldName="Id" Visible="false" OnCustomCallback="ASPxGridView1_CustomCallback"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                 <SettingsEditing  Mode="EditForm"/>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutSku(new Array("<%# Eval("Code") %>","<%# Eval("Description") %>","<%# Eval("UomPacking") %>","<%# Eval("UomWhole") %>","<%# Eval("UomLoose") %>","<%# Eval("UomBase") %>","<%# Eval("QtyPackingWhole") %>","<%# Eval("QtyWholeLoose") %>","<%# Eval("QtyLooseBase")%>","<%# Eval("Att4")%>","<%# Eval("Att5") %>","<%# Eval("Att6") %>","<%# Eval("Att7") %>","<%# Eval("Att8") %>","<%# Eval("Att9") %>","<%# Eval("Att10") %>","<%# Eval("Att11") %>","<%# Eval("Att12") %>","<%# Eval("Att13") %>"));'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description1" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                      <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Att1" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PKG" FieldName="QtyPackingWhole" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PKG UOM" FieldName="Uom2" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UNIT" FieldName="QtyWholeLoose" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="UNIT UOM" FieldName="Uom3" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="STK" FieldName="QtyLooseBase" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="STK UOM" FieldName="Uom4" ReadOnly="true" VisibleIndex="6" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Att13" VisibleIndex="10">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
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
                        </EditForm>
                    </Templates>
            </dxwgv:ASPxGridView>
                        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
