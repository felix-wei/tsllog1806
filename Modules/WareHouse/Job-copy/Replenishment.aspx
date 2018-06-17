<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Replenishment.aspx.cs" Inherits="WareHouse_Job_Replenishment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
       <script type="text/javascript">
         
           function SelectAll() {
               if (btnSelect.GetText() == "Select All")
                   btnSelect.SetText("UnSelect All");
               else
                   btnSelect.SetText("Select All");
               jQuery("input[id*='ack_IsPay']").each(function () {
                   this.click();
               });
           }
           function OnCallback(v) {
               if (v == "Success") {
                   alert("Action Success!!!");
                   grid.Refresh();
               }
               else if (v != null && v.length > 0)
                   alert(v)
           }
           function PopupInventory(code) {
               popubCtr.SetHeaderText('Set Inventory');
               popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SetInventory.aspx?Loc=' + code);
               popubCtr.Show();
           }

    </script>
      <script type="text/javascript">
         var lastWarehouse = null;
         var lastToWarehouse = null;
         function OnWarehouseChanged(cmbWarehouse) {
             if (cmb_Location.InCallback()) {
                lastWarehouse = cmbWarehouse.GetValue().toString();
             } else {
                 cmb_Location.PerformCallback(cmbWarehouse.GetValue().toString());
            }
        }
        function OnEndCallback(s, e) {
            if (lastWarehouse) {
                cmb_Location.PerformCallback(lastWarehouse);
                lastWarehouse = null;
            }
        }
        function OnSelectedIndexChanged(s, e, cmb_Location) {
            cmb_Location.PerformCallback(s.GetValue().toString());
        }
  
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefWarehouse" KeyMember="Id" />
    <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefLocation" KeyMember="Id"  FilterExpression="Loclevel='Unit'"/>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        Product
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        Lot No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Warehouse</td>
                    <td>
                         <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server" 
                            Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse" 
                            ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                            </Columns>
                             <ClientSideEvents SelectedIndexChanged="function(s, e) { OnWarehouseChanged(cmb_WareHouse); }" />
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Location</td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_Location" ClientInstanceName="cmb_Location" runat="server" OnCallback="cmbFromLoc_OnCallback"
                            Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" TextField="Code" DataSourceID="dsRefLocation"
                            ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                            </Columns>
                            <ClientSideEvents EndCallback="OnEndCallback"/>
                        </dxe:ASPxComboBox>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                       <td>
                          <dxe:ASPxButton ID="btnSelect" runat="server" Text="Invert Select" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                    </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Save All" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
            
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="100%"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Product" Width="100" VisibleIndex="1" SortIndex="0" SortOrder="Ascending">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_Product" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Product") %>' Width="100">
                                </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="2">
                        <DataItemTemplate>
                            <%# Eval("LotNo") %>
                            <div style="display:none">
                            <dxe:ASPxTextBox ID="txt_LotNo" Border-BorderWidth="0" ReadOnly="true" runat="server"
                                    Text='<%# Eval("LotNo") %>' >
                                </dxe:ASPxTextBox>
                                </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2">
                        <DataItemTemplate>
                             <dxe:ASPxLabel ID="txt_Description" Border-BorderWidth="0" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Description") %>' >
                                </dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DoNo" FieldName="DoNo" VisibleIndex="2" Width="50">
                         <DataItemTemplate>
                             <dxe:ASPxLabel ID="txt_DoNo" Border-BorderWidth="0" runat="server"
                                    Text='<%# Eval("DoNo") %>' >
                                </dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="On Hand" FieldName="OnHand" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="From WareHouse" FieldName="WarehouseId" VisibleIndex="2" Width="50">
                         <DataItemTemplate>
                             <dxe:ASPxLabel ID="txt_WareHouse" Border-BorderWidth="0" runat="server"
                                    Text='<%# Eval("WarehouseId") %>' >
                                </dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="From Location" FieldName="Location" VisibleIndex="2" Width="50">
                        <DataItemTemplate>
                              <dxe:ASPxLabel ID="lbl_Location" runat="server" Value='<%# Eval("Location") %>' Width="40" Increment="0"></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="To WareHouse" FieldName="ToWarehouseId" VisibleIndex="2" Width="50">
                         <DataItemTemplate>
                             <dxe:ASPxComboBox ID="cmb_WareHouse" runat="server" OnInit="cmb_WareHouse_Init"
                                 Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                                 ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                                 EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                                 <Columns>
                                     <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                                 </Columns>

                             </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="To Location" FieldName="ToLocId" VisibleIndex="2" Width="50">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox ID="cmb_Location" OnInit="cmb_Location_Init" runat="server"
                                Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefLocation" OnCallback="cmbToFromLoc_OnCallback"
                                ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                                EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                                <Columns>
                                    <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                                </Columns>

                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty1" VisibleIndex="2" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit id="spin_Qty1" runat="server" Value='<%# Eval("Qty1") %>' Width="40" Increment="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false" AutoPostBack="false">
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="1000" EnableViewState="False">
                </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
