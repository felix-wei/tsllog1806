<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vehicle.aspx.cs" Inherits="PagesContTrucking_Vehicle_Vehicle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
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
            parent.navTab.openTab(res+" Service Log","/PagesContTrucking/Vehicle/Activity.aspx?no="+res,{title:res+" Service Log", fresh:false, external:true});
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefVehicle" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td><dxe:ASPxLabel ID="lbl_Code" runat="server" Text="Code"></dxe:ASPxLabel></td>
                    <td><dxe:ASPxTextBox ID="txt_Code" runat="server" Width="100"></dxe:ASPxTextBox></td>
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
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" DataSourceID="dsTransport" Width="1500" 
                KeyFieldName="Id" OnInit="grid_Transport_Init" OnRowInserting="grid_Transport_RowInserting" OnRowUpdating="grid_Transport_RowUpdating" 
                OnInitNewRow="grid_Transport_InitNewRow" OnRowDeleting="grid_Transport_RowDeleting"  OnCustomDataCallback="grid_Transport_CustomDataCallback"
                OnHtmlDataCellPrepared="grid_Transport_HtmlDataCellPrepared">
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
                            <input type="button" class="button" value="View Service Log" onclick="rowOpen(<%# Container.VisibleIndex %>);" />
                            <div style="display:none">
                                <dxe:ASPxLabel ID="lbl_VehicleCode" runat="server" Text='<%# Eval("VehicleCode") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="VehicleCode" Caption="Code" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="VehicleType" Caption="VehicleType" Width="100">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Value="Crane" Text="Crane" />
                                <dxe:ListEditItem Value="Towhead" Text="PrimeMover" />
                                <dxe:ListEditItem Value="Van" Text="Van" />
                                <dxe:ListEditItem Value="Lorry" Text="Lorry" />
                                <dxe:ListEditItem Value="Motorbike" Text="Motorbike" />
                                <dxe:ListEditItem Value="Bicycle" Text="Bicycle" />
                                <dxe:ListEditItem Value="Trailer" Text="Trailer" />
                                <dxe:ListEditItem Value="Other" Text="Other" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="VehicleStatus" Caption="VehicleStatus" Width="100">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Value="Inactive" Text="Inactive" />
                                <dxe:ListEditItem Value="InService" Text="InService" />
                                <dxe:ListEditItem Value="Active" Text="Active" />
                                <dxe:ListEditItem Value="Sold" Text="Sold" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="VeicleSize" Caption="Veicle Size" Width="100">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Value="Blank" Text="Blank" />
                                <dxe:ListEditItem Value="20" Text="20" />
                                <dxe:ListEditItem Value="40" Text="40" />
                                <dxe:ListEditItem Value="45" Text="45" />
                                <dxe:ListEditItem Value="70Ton" Text="70Ton" />
                                <dxe:ListEditItem Value="120Ton" Text="120Ton" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="VehicleUse" Caption="Veicle Use" Width="100">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Value="FCL" Text="FCL" />
                                <dxe:ListEditItem Value="LCL" Text="LCL" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContractNo" Caption="Model" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContractType" Caption="Owner" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_ContractType" runat="server" Value='<%# Bind("ContractType") %>' Width="100%" DropDownStyle="DropDownList">
                                <Items>
                                    <dxe:ListEditItem Value="Lease" Text="Lease" />
                                    <dxe:ListEditItem Value="Own" Text="Own" />
                                    <dxe:ListEditItem Value="Rent" Text="Rent" />
                                    <dxe:ListEditItem Value="Subcon" Text="Subcon" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContractDate" Caption="ContractDate" Width="100">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ContractDate")) %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_ContractDate" runat="server" EditFormatString="dd/MM/yyyy" Value='<%# Bind("ContractDate") %>' Width="100%"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContractExpiryDate" Caption="ContractExpiryDate" Width="100">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ContractExpiryDate")) %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_ContractExpiryDate" runat="server" EditFormatString="dd/MM/yyyy" Value='<%# Bind("ContractExpiryDate") %>' Width="100%"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="LicenseNo" Caption="LicenseNo" Width="120" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="InsuranceExpiryDate" Caption="Insurance Expiry Date" Width="150">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="RoadTaxExpiryDate" Caption="Road Tax Expiry Date" Width="150">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="LicenseExpiryDate" Visible="false" Caption="LicenseExpiryDate" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="CraneLGCertExpiryDate" Visible="false" Caption="CraneLGCertExpiryDate" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="CraneLHCertExpiryDate" Visible="false" Caption="CraneLHCertExpiryDate" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="VpcExpiryDate" Caption="VpcExpiryDate" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="LastInternalInspectionDate"  Visible="false" Caption="LastInternalInspectionDate" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="SupplierCode" Caption="SupplierCode" Visible="false" Width="80">
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
                    <dxwgv:GridViewDataDateColumn FieldName="Date1" Caption="Next Maintenance" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="Date2" Caption="Next Inspection" Width="100">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>

                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Width="150"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="true" ShowFilterRow="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="VehicleCode" ShowInColumn="VehicleCode"
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
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
