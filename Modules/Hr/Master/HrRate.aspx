<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HrRate.aspx.cs" Inherits="Modules_Hr_Master_HrRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HrGroup</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsHrRate" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrRate" KeyMember="Id" />
                    <wilson:DataSource ID="dsPayItem" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPayItem" KeyMember="Id" />
        <div>
            <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
            </dxe:ASPxButton>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsHrRate"
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
                
                <dxwgv:GridViewDataTextColumn Caption="From" FieldName="FromDate" VisibleIndex="2" Width="120" >
                    <DataItemTemplate><%# Eval("FromDate","{0:dd/MM/yyyy}")%></DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_From" Width="120" runat="server" Value='<%# Bind("FromDate")%>'
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="To" FieldName="ToDate" VisibleIndex="2" Width="120">
                    <DataItemTemplate><%# Eval("ToDate","{0:dd/MM/yyyy }")%></DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_To" Width="120" runat="server" Value='<%# Bind("ToDate")%>'
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
				<dxwgv:GridViewBandColumn Name="Age1" Caption="Age Group: Below 55" VisibleIndex="9">
                    <Columns>
                <dxwgv:GridViewDataSpinEditColumn Caption="Employee(below 55)" FieldName="EmployeeRate" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"    Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Employer(<=55)" FieldName="EmployerRate" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"   Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                    </Columns>
                </dxwgv:GridViewBandColumn>
				<dxwgv:GridViewBandColumn Name="Age2" Caption="Age Group: 55 - 60"  VisibleIndex="9"><Columns>
				<dxwgv:GridViewDataSpinEditColumn Caption="Employee(55-60)" FieldName="EmployeeRate55" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"   Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Employer(55-60)" FieldName="EmployerRate55" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"    Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn> 
              </Columns>  </dxwgv:GridViewBandColumn>
				<dxwgv:GridViewBandColumn Name="Age3" Caption="Age Group: 60 - 65"  VisibleIndex="9"><Columns>
				<dxwgv:GridViewDataSpinEditColumn Caption="Employee(60-65)" FieldName="EmployeeRate60" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"    Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Employer(60-65)" FieldName="EmployerRate60" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"     Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
               </Columns> </dxwgv:GridViewBandColumn>
				<dxwgv:GridViewBandColumn Name="Age4" Caption="Age Group: Above 65"  VisibleIndex="9"><Columns>
				 <dxwgv:GridViewDataSpinEditColumn Caption="Employee(>65)" FieldName="EmployeeRate65" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"    Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Employer(>65)" FieldName="EmployerRate65" UnboundType="String" VisibleIndex="2" Width="75">
                    <PropertiesSpinEdit DecimalPlaces="3" DisplayFormatString="0.000"    Width="75">
                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>  
               </Columns> </dxwgv:GridViewBandColumn>
               
                
                 <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="RateType" VisibleIndex="2" Width="150">
                        <PropertiesComboBox ValueType="System.String" Width="150" DropDownWidth="105"
                            EnableIncrementalFiltering="true">
                            <Items>
                                <dxe:ListEditItem  Text="SC" Value="SC"/>
                                <dxe:ListEditItem  Text="SPR1" Value="SPR1"/>
                                <dxe:ListEditItem  Text="SPR2" Value="SPR2"/>
                                <dxe:ListEditItem  Text="SPR3" Value="SPR3"/>
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="3" >
                </dxwgv:GridViewDataTextColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
