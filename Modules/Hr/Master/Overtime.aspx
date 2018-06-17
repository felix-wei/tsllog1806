<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Overtime.aspx.cs" Inherits="Modules_Hr_Master_Overtime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsHrOvertime" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrOvertime" KeyMember="Id" />
        <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
        <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Save" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsHrOvertime"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow"
            OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Employee" FieldName="Person" VisibleIndex="0" Width="150">
                    <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                        TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Hours" FieldName="Hour" VisibleIndex="3" Width="100">
                    <PropertiesSpinEdit NumberType="Float" Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Hours Rate" FieldName="HoursRate" VisibleIndex="3" Width="140">
                    <PropertiesSpinEdit NumberType="Float" Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Times" FieldName="Time" VisibleIndex="3" Width="140">
                    <PropertiesSpinEdit NumberType="Float" Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="TotalAmt" VisibleIndex="3" Width="140" ReadOnly="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="TypeId" VisibleIndex="3" Width="80">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cbb_Type" ClientInstanceName="cmb_ApproveStatus" Value='<%# Bind("TypeId") %>' runat="server" Width="170">
                            <Items>
                                <dxe:ListEditItem Text="Work" Value="Work" Selected="true" />
                                <dxe:ListEditItem Text="Overtime" Value="Overtime" />
                                <dxe:ListEditItem Text="NoPay" Value="NoPay" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="From" FieldName="FromDate" Width="120px" VisibleIndex="3">
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_ApplyDateTime" Width="168" runat="server" Value='<%# Bind("FromDate") %>'
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="To" FieldName="ToDate" Width="120px" VisibleIndex="3">
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_ApplyDateTime" Width="168" runat="server" Value='<%# Bind("ToDate") %>'
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
