<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DimensionForCargo.aspx.cs" Inherits="PagesContTrucking_SelectPage_DimensionForCargo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopubDimension(); }
        }
        document.onkeydown = keydown;

        function onCallBack(v) {
            if (v == "Success") {
                grid.Refresh();
            }
            else {
                txt_Count.SetFocus();
                alert(v);
                
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none">
            <dxe:ASPxLabel runat="server" ID="lbl_Id"></dxe:ASPxLabel>
        </div>
        <table>
            <tr>
                <td>Number</td>
                <td>
                    <dxe:ASPxTextBox ID="txt_Count" ClientInstanceName="txt_Count" runat="server" Width="80">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e){
                      grid.GetValuesOnCustomCallback('AddLines',onCallBack);
                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        

        <wilson:DataSource ID="dsDimension" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Dimension" KeyMember="Id" />
        <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
            KeyMember="Id" FilterExpression="CodeType='2'" />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
            DataSourceID="dsDimension" KeyFieldName="Id" Width="100%" 
            OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnBeforePerformDataSelect="grid_BeforePerformDataSelect" Styles-CommandColumn-HorizontalAlign="Left" Styles-CommandColumn-Spacing="10"
            OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting" OnRowDeleted="grid_RowDeleted" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" OnCustomDataCallback="grid_CustomDataCallback">
            <SettingsBehavior ConfirmDelete="true" />
            <SettingsEditing Mode="Batch" BatchEditSettings-StartEditAction="Click" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <SettingsCommandButton CancelButton-Text="Cancel" UpdateButton-ButtonType="Button" CancelButton-ButtonType="Button" UpdateButton-Text="Save"></SettingsCommandButton>
            <Settings ShowFooter="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60px">
                    <EditButton Visible="true"></EditButton>
                    <DeleteButton Visible="true"></DeleteButton>
                </dxwgv:GridViewCommandColumn>
              <dxwgv:GridViewDataTextColumn Caption="Des" FieldName="Description" Width="200px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="Uom" Caption="Unit" Width="50" VisibleIndex="1">
                    <PropertiesComboBox DataSourceID="dsPackageType">
                        <Columns>
                            <dxe:ListBoxColumn  FieldName="Code"/>
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Length" FieldName="Length" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                 <dxwgv:GridViewDataSpinEditColumn Caption="Width" FieldName="Width" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Height" FieldName="Height" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Unit Vol (M3)" FieldName="Volume" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
               <dxwgv:GridViewDataSpinEditColumn Caption="Total Vol" FieldName="TotalVol" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Unit Qty" FieldName="PackQty" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Total Qty" FieldName="SkuQty" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Total Wt" FieldName="TotalWt" VisibleIndex="1"
                    Width="60">
                    <PropertiesSpinEdit Increment="0" DecimalPlaces="3" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="Unit Wt (KGS)" FieldName="SingleWeight" VisibleIndex="2">
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>

                  <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" Width="200px">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <TotalSummary>
                <dxwgv:ASPxSummaryItem  FieldName="TotalVol" SummaryType="Sum" DisplayFormat="0.000"/>
                <dxwgv:ASPxSummaryItem  FieldName="TotalWt" SummaryType="Sum" DisplayFormat="0.000"/>
                <dxwgv:ASPxSummaryItem  FieldName="SkuQty" SummaryType="Sum" DisplayFormat="0.000"/>
            </TotalSummary>
        </dxwgv:ASPxGridView>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="500" EnableViewState="False">
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
