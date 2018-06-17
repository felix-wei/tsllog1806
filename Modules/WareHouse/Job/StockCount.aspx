<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockCount.aspx.cs" Inherits="Modules_WareHouse_Job_StockCount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Stock Come</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>

    <script type="text/javascript" src="/Script/pages.js"></script>
      <script type="text/javascript">
          var isUpload = false;
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
               if (v != null && v.length > 0)
                  alert(v);
          }
          function PopupUploadExcel() {
              popubCtr.SetHeaderText('Upload Excel');
              popubCtr.SetContentUrl('../UploadExcel.aspx?Date=' + date.GetText());
              popubCtr.Show();
          }
          function AfterPopubMultiInv() {
              popubCtr.Hide();
              popubCtr.SetContentUrl('about:blank');
          }
          function ShowHouse(masterId) {
              parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/StockCountEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
              //window.location = "DoInEdit.aspx?no=" + masterId;
          }
    </script>

    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
<form id="form1" runat="server">
         <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <div>
            <table>
                <tr>
                    <td>Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_Date" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                        <div style="display: none">
                            <dxe:ASPxDateEdit ID="date" ClientInstanceName="date" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </div>
                    </td>
                    <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                             PopupParty(txt_CustId,txt_CustName,null,null,null,null,null,memo_Address,'CV');
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="230" runat="server">
                        </dxe:ASPxTextBox>
                        <div style="display:none">
                            <dxe:ASPxMemo ID="memo_Address" Rows="5" Width="100%" ClientInstanceName="memo_Address"
                                            runat="server" Text='<%# Eval("PartyAdd") %>'>
                                        </dxe:ASPxMemo>
                        </div>
                    </td>
                    <td>WareHouse</td>
                    <td>
                         <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                            Width="100px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                            ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                            </Columns>
                             <ClientSideEvents  SelectedIndexChanged="function(s, e) {
                                 cmb_loc.PerformCallback();
                                 }"/>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Location</td>
                    <td>
                         <dxe:ASPxComboBox ID="cmb_loc" ClientInstanceName="cmb_loc" runat="server" OnCallback="cmb_loc_Callback"
                            Width="100px" DropDownWidth="200" DropDownStyle="DropDownList">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    
                    <td>
                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="120" AutoPostBack="false"
                            Text="Save All">
                            <ClientSideEvents Click="function(s,e) {
                                                    grid.GetValuesOnCustomCallback('Save',OnCallback);
                                                 }" />
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Import" Width="100" runat="server" Text="Import" AutoPostBack="false" >
                            <ClientSideEvents  Click="function(s,e){
                                isUpload=true;
                                  PopupUploadExcel();
                                }"/>
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Export" OnClick="btn_Export_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" Styles-Cell-HorizontalAlign="left"
                    KeyFieldName="Id" AutoGenerateColumns="False" Width="100%" OnCustomDataCallback="grid_CustomDataCallback">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="No" FieldName="RefNo" VisibleIndex="1" Width="80">
                           <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("RefNo") %>');"><%# Eval("RefNo") %></a>
                        </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                         <dxwgv:GridViewDataTextColumn Caption="Client" FieldName="PartyName" VisibleIndex="2" Width="60">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Product" VisibleIndex="1" Width="80">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="txt_Product" runat="server" Text='<%# Eval("Product") %>'></dxe:ASPxLabel>

                                <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10" ClientVisible="false">
                                </dxe:ASPxCheckBox>
                                <div style="display: none">
                                    <dxe:ASPxTextBox runat="server" ID="txt_DoNo" Text='<%# Eval("DoNo") %>'></dxe:ASPxTextBox>
                                    <dxe:ASPxTextBox runat="server" ID="txt_Id" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="2" Width="60">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="txt_LotNo" runat="server" Text='<%# Eval("LotNo") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" VisibleIndex="4" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="4" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="In Date" FieldName="DoDate" VisibleIndex="5" Width="80">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption=" Date" FieldName="StockDate" VisibleIndex="5" Width="80">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Uom" FieldName="Uom" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Grade" FieldName="Att1" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Color" FieldName="Att2" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Size" FieldName="Att3" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="Att4" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Clot1" FieldName="Att5" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ATT" FieldName="Att6" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty1" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="NewQty" FieldName="NewQty" VisibleIndex="6" Width="50">
                            <DataItemTemplate>
                                <dxe:ASPxSpinEdit ID="spin_NewQty" runat="server" Width="80" Value='<%# Eval("NewQty") %>' ClientInstanceName="spin_NewQty" Increment="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" NumberType="Float">
                                </dxe:ASPxSpinEdit>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFooter="true" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0}" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid" >
                </dxwgv:ASPxGridViewExporter>
                 <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="300"
                AllowResize="True" Width="600" EnableViewState="False">
              
            </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
