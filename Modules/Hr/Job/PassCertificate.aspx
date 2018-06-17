<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PassCertificate.aspx.cs" Inherits="Modules_Hr_Job_PassCertificate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Status='Employee' and Id>0" />
        <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lbl_Employee" runat="server" Text="Employee"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txtSchName" ClientInstanceName="txtSchName" TextFormatString="{1}"
                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                            DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="100%" ValueType="System.Int32">
                            <Buttons>
                                <dxe:EditButton Text="Clear"></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                if(e.buttonIndex == 0){
                                s.SetText('');
                             }
                            }" />
                        </dxe:ASPxComboBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
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
        <wilson:DataSource ID="dsPassCertificate" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.PassCertificate" KeyMember="Id" />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsPassCertificate"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init" OnCellEditorInitialize="grid_CellEditorInitialize"
            OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowDeleting="grid_RowDeleting" OnRowUpdating="grid_RowUpdating"
            OnCustomCallback="grid_CustomCallback" 
            onrowinserted="grid_RowInserted">
            <SettingsPager Mode="ShowPager" PageSize="20">
            </SettingsPager>
            <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn Width="10%" VisibleIndex="0">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Employee" FieldName="Employee" VisibleIndex="0" Width="100">
                    <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                        TextField="Name" EnableIncrementalFiltering="true" EnableCallbackMode="true" ValueField="Id" DataMember="{1}">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn FieldName="ExpiryDate" Caption="Expiry Date" VisibleIndex="2" Width="120">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="TypeCode" VisibleIndex="1" Width="300">
                    <PropertiesComboBox ValueType="System.String"  Width="150" TextFormatString="{1}" DropDownWidth="105"
                        TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                        <Items>
                            <dxe:ListEditItem  Text="Other" Value="Other"/>
                            <dxe:ListEditItem  Text="Pass" Value="Pass"/>
                            <dxe:ListEditItem  Text="Certificate" Value="Certificate"/>
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
