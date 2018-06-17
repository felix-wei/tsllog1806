<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Activity.aspx.cs" Inherits="PagesContTrucking_Vehicle_Activity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="/PagesContTrucking/script/firebase.js"></script>
    <script src="/PagesContTrucking/script/js_company.js"></script>
    <script src="/PagesContTrucking/script/js_firebase.js"></script>
    <script src="/PagesContTrucking/script/jquery.js"></script>
    <script type="text/javascript">
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        var config = {
            timeout: 0,
            gridview: 'detailGrid',
        }

        $(function () {
            loading.hide();
        })
        function rowOpen(rowIndex) {
         
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('OpenInline_'+rowIndex, rowOpen_callback);
            }, config.timeout);
        }
        function rowOpen_callback(res) {
            parent.navTab.openTab(res,"/PagesContTrucking/Vehicle/Vehicle.aspx?no="+res,{title:res, fresh:false, external:true});
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefVehicleLog" KeyMember="Id" FilterExpression="1=0" />
            <div style="display:none">
                <dxe:ASPxTextBox ID="txt_Code" runat="server" Width="100"></dxe:ASPxTextBox>
            </div>
            <table>
                <tr>
                    <td>Date From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_searchFrom" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_searchTo" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                            <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" OnCustomDataCallback="grid_Transport_CustomDataCallback" Width="1300" DataSourceID="dsTransport" KeyFieldName="Id" OnInit="grid_Transport_Init" OnRowInserting="grid_Transport_RowInserting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnRowDeleting="grid_Transport_RowDeleting">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="True">
                        </DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Width="100">
                        <DataItemTemplate>
                            <input type="button" class="button" value="Open Vehicle" onclick="rowOpen(<%# Container.VisibleIndex %>);" />
                            <div style="display:none">
                                <dxe:ASPxLabel ID="lbl_VehicleCode" runat="server" Text='<%# Eval("VehicleCode") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="DriverCode" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="100%">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,null);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="VehicleCode" Caption="VehicleCode" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_VehicleCode" ClientInstanceName="btn_VehicleCode" runat="server" Text='<%# Bind("VehicleCode") %>' AutoPostBack="False" Width="100%">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_VehicleList(btn_VehicleCode,null,'Towhead');
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="EventDate" Caption="Date" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="Description" Caption="Description" Width="150"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="EventStatus" Caption="Event Status" Width="100">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                <dxe:ListEditItem Value="Progress" Text="Progress" />
                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                <dxe:ListEditItem Value="Exit" Text="Exit" />
                                <dxe:ListEditItem Value="Cancelled" Text="Cancelled" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="EventType" Caption="EventType" Width="100">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Value="Accident" Text="Accident" />
                                <dxe:ListEditItem Value="Service" Text="Service" />
                                <dxe:ListEditItem Value="Maintenance" Text="Maintenance" />
                                <dxe:ListEditItem Value="Repair" Text="Repair" />
                                <dxe:ListEditItem Value="Inspection" Text="Inspection" />
                                <dxe:ListEditItem Value="Others" Text="Others" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataSpinEditColumn FieldName="TotalPrice" Caption="TotalPrice" Width="70">
                        <PropertiesSpinEdit DisplayFormatString="0.00" Increment="0" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataColumn FieldName="DocNo" Caption="DocNo" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SupplierCode" Caption="SupplierCode" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_SupplierCode" ClientInstanceName="btn_SupplierCode" runat="server" Text='<%# Bind("SupplierCode") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_SupplierCode,null);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="InsuranceExpiryDate" Caption="Insurance Expiry Date" Width="150">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="RoadTaxExpiryDate" Caption="Road Tax Expiry Date" Width="150">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Width="150"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="true" ShowFilterRow="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DriverCode" ShowInColumn="DriverCode"
                        SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>           
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
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
