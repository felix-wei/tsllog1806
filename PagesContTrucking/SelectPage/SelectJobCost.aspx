<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  CodeFile="SelectJobCost.aspx.cs" Inherits="PagesContTrucking_SelectPage_SelectJobCost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
    <script src="../script/jquery.js"></script>
      <script type="text/javascript">
          function $(s) {
              return document.getElementById(s) ? document.getElementById(s) : s;
          }
          function keydown(e) {
              if (e.keyCode == 27) { parent.AfterPopub(); }
          }
          document.onkeydown = keydown;
          var loading = {
              show: function () {
                  $("#div_tc").css("display", "block");
              },
              hide: function () {
                  $("#div_tc").css("display", "none");
              }
          }
          function goJob() {
              //window.location = "JobEdit.aspx?jobNo=" + jobno;
              var jobno = lbl_JobNo.GetText();
              parent.parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
          }
          var config = {
              timeout: 0,
              gridview: 'grid',
          }

          $(function () {
              loading.hide();
          })
          function SelectAll() {
              if (btnSelect.GetText() == "Select All")
                  btnSelect.SetText("UnSelect All");
              else
                  btnSelect.SetText("Select All");
              jQuery("input[id*='ack_IsPay']").each(function () {
                  this.click();
              });
          }
          function SelectWHAll() {
              jQuery("input[id*='ack_IsOk']").each(function () {
                  this.click();
              });
          }
          function OnCallback(v) {
              if (v != null) {
                  alert(v);
                  grid.Refresh();
              }
          }
          function OnCreateInvCallback(v) {
              if (v != null && v.indexOf('Action Error')>=0)
              {
                  alert(v);
              }
              else{
                  alert("Create Invoice Success");
                  grid.Refresh();
                  document.location.reload();
                  parent.parent.navTab.openTab(v, "/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
              }
              
          }
          function OnUpdateInvCallback(v) {
              if (v != null && v.indexOf('Action Error') >= 0) {
                  alert(v);
              }
              else {
                  alert("Update Invoice Success");
                  document.location.reload();
                  parent.parent.navTab.openTab(v, "/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
              }

          }
          function OnSaveCallback(v) {
              if (v != null) {
                  alert(v);
                  grid.Refresh();
              }
          }
          function AfterPopub() {
              popubCtr.Hide();
              popubCtr.SetContentUrl('about:blank');
              grid.Refresh();
          }
          function PopupBillRate() {
              var client = encodeURIComponent(lbl_Client.GetText());
              popubCtr.SetHeaderText('Bill Rate');
              popubCtr.SetContentUrl('../SelectPage/SelectBillRate.aspx?no=' + lbl_JobNo.GetText() + '&type=' + lbl_Type.GetText() + '&client=' +client+ '&status=Rate');
              popubCtr.Show();
          }
          function PopupRate() {
              popubCtr.SetHeaderText('Chg Code');
              popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/SelectChargesForCost.aspx?no=' + lbl_JobNo.GetText() + '&quoteNo=' + lbl_QuotedNo.GetText());
              popubCtr.Show();
          }
          function PopupQuotation() {
              var client = encodeURIComponent(lbl_Client.GetText());
              popubCtr.SetHeaderText('Quotation Rate');
              popubCtr.SetContentUrl('../SelectPage/SelectBillRate.aspx?no=' + lbl_QuotedNo.GetText() + '&type=' + lbl_Type.GetText() + '&client=' + client + '&jobNo=' + lbl_JobNo.GetText() + '&status=Quoted');
              popubCtr.Show();
          }
          //Update Trucking Charge
          var btnId = null;
          function charge_update_inline(spin, rowIndex) {
              console.log(rowIndex);
              loading.show();
              setTimeout(function () {
                  grid.GetValuesOnCustomCallback('ChargeUpdateline_' + rowIndex, charge_update_inline_callback);
              }, config.timeout);
              btnId = spin;
          }
          function charge_update_inline_callback(res) {
              grid.Refresh();
          }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Job_Cost" KeyMember="Id" FilterExpression="1=1" />
        <wilson:DataSource ID="dsWhCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhCosting" KeyMember="Id"  />
        <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="id" FilterExpression="1=1" />
        <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RateType" KeyMember="Id" />
        <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId"  />
        <div style="display: none">
            <dxe:ASPxLabel ID="lbl_JobNo" ClientInstanceName="lbl_JobNo" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lbl_QuotedNo" ClientInstanceName="lbl_QuotedNo" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lbl_Client" ClientInstanceName="lbl_Client"  runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lbl_Type" ClientInstanceName="lbl_Type"  runat="server"></dxe:ASPxLabel>
        </div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="40" runat="server" Text="Add New" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s,e){
                              PopupRate();
                            }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="100" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton3" Width="60" runat="server" Text="Save"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('Save',OnSaveCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton2" Width="80" runat="server" Text="Select Bill Rate"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                   PopupBillRate();             
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton5" Width="80" runat="server" Text="Select Quotation"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                   PopupQuotation();             
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                
                <td>

                    <dxe:ASPxButton ID="btn_CreateInv" Width="60" runat="server" Text="Create Inv" 
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                                              if(confirm('Confirm Create Invoice for '+txt_DocDt.GetText()+' ?')){
                                    grid.GetValuesOnCustomCallback('OK',OnCreateInvCallback);   
                            }              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <div style="display:none">
                        <dxe:ASPxLabel ID="lbl_DocNo" ClientInstanceName="lbl_DocNo" runat="server"></dxe:ASPxLabel>
                    </div>
                    <dxe:ASPxButton ID="btn_UpdateInv" Width="60" runat="server" Text="Update Inv" 
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                                              if(confirm('Confirm Update These Cost for '+lbl_DocNo.GetText()+' ?')){
                                    grid.GetValuesOnCustomCallback('Update',OnUpdateInvCallback);   
                            }              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                 <td>
                    <dxe:ASPxButton ID="ASPxButton6" Width="60" runat="server" Text="View Job" 
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    goJob();
                                         
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td style="color:red;width:80px">
                    Invoice_Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_DocDt" ClientInstanceName="txt_DocDt" runat="server" Width="100"
                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td style="width:60px">
                    Curr.
                </td>
                <td>
                    <dxe:ASPxButtonEdit ID="cmb_CurrencyId" ClientInstanceName="cmb_CurrencyId" runat="server" Width="60" MaxLength="3">
                        <Buttons>
                            <dxe:EditButton Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CurrencyId,spin_ExRate);
                        }" />
                    </dxe:ASPxButtonEdit>
                </td>
                <td style="width:60px">ExRate</td>
                <td>
                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="60"
                        DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_ExRate" ClientInstanceName="spin_ExRate" Increment="0">
                    </dxe:ASPxSpinEdit>
                </td>
                <td style="width:80px">BillType
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cbb_BillType" runat="server" Width="80" DataSourceID="dsRateType"
                        ValueField="Code" TextField="Code"  CallbackPageSize="15"
                        EnableCallbackMode="True" EnableViewState="false" IncrementalFilteringMode="StartsWith" ClientSideEvents-SelectedIndexChanged="charge_update_inline">
                    </dxe:ASPxComboBox>
                </td>
            </tr>
        </table>
    <div>
        <table style="width: 100%;">
           <%-- <tr>
                <td style="padding: 4px; text-align: center; background: #e9e9e9">
                    <b>TRUCKING CHARGES</b>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" OnCustomDataCallback="grid_CustomDataCallback"
                        DataSourceID="dsCosting" KeyFieldName="Id" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting"
                        OnRowInserting="grid_RowInserting" OnInitNewRow="grid_InitNewRow"
                        OnInit="grid_Init" Width="100%" AutoGenerateColumns="False">
                        <SettingsEditing Mode="EditForm" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                                <EditButton Visible="True" />
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="#" Visible="true" VisibleIndex="999" Width="5%">
                                <DataItemTemplate>
                                    <dxe:ASPxHyperLink ID="link_Delete" runat="server" NavigateUrl="#" Text="Delete" Visible='<%# SafeValue.SafeString(Eval("LineSource"))=="M" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                    </dxe:ASPxHyperLink>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="1" SortOrder="Descending" SortIndex="0"  Settings-SortMode="Value" Settings-AllowSort="True"
                                Width="40">
                                <DataItemTemplate>
                                    <dxe:ASPxCheckBox ID="ack_IsPay" Visible='<%# IsCostCreated(Eval("Id")) %>' runat="server" Width="10">
                                    </dxe:ASPxCheckBox>
                                    <div style="display: none">
                                        <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                            Text='<%# Eval("Id") %>' Width="150">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </DataItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Index" FieldName="LineIndex" VisibleIndex="1" SortOrder="Ascending" SortIndex="0"
                                Width="50">
                                <DataItemTemplate>
                                     <dxe:ASPxSpinEdit DisplayFormatString="0" runat="server" Width="40" NumberType="Integer"
                                        ID="spin_LineIndex" Height="21px" Value='<%# Bind("LineIndex")%>' DecimalPlaces="0" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="LineType" Caption="Line Type" Width="150" VisibleIndex="1" Visible="false">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="LineStatus" Caption="Optional" Width="50" VisibleIndex="98" Visible="false">
                                <PropertiesComboBox>
                                    <Items>
                                        <dxe:ListEditItem Text="Y" Value="Y" />
                                        <dxe:ListEditItem Text="N" Value="N" />
                                    </Items>
                                </PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1">
                                <DataItemTemplate>
                                    <dxe:ASPxComboBox ID="cbb_ChgCode" ClientInstanceName="cbb_ChgCode" runat="server"
                                    Value='<%# Eval("ChgCode") %>' Width="100" DropDownWidth="100" DropDownStyle="DropDownList" TextField="ChgCodeDe"
                                    DataSourceID="dsRate" ValueField="ChgCodeId" ValueType="System.String" 
                                    EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                    CallbackPageSize="100">
                                </dxe:ASPxComboBox>
                                    <%--<dxe:ASPxLabel ID="lbl_ChgCode" runat="server" Text='<%# Eval("ChgCode") %>'></dxe:ASPxLabel>--%>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                                Width="120">
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox runat="server" Width="120" ID="txt_ChgCodeDe" Text='<%# Bind("ChgCodeDe") %>'>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                Width="100" >
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_Remark" Text='<%# Bind("Remark") %>'>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ContNo" Caption="Cont No" Width="80" VisibleIndex="1">
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox runat="server" Width="80" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="ContType" Caption="Cont Type" Width="150" VisibleIndex="1">
                                <DataItemTemplate>
                                    <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="cbbContType" runat="server" Width="80" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContType") %>'>
                                        <Items>
                                            <dxe:ListEditItem Text="20HC" Value="20HC" />
                                            <dxe:ListEditItem Text="20OT" Value="20OT" />
                                            <dxe:ListEditItem Text="20RF" Value="20RF" />
                                            <dxe:ListEditItem Text="20GP" Value="20GP" />
                                            <dxe:ListEditItem Text="20FT" Value="20FT" />
                                            <dxe:ListEditItem Text="20HD" Value="20HD" />
                                            <dxe:ListEditItem Text="40FR" Value="40FR" />
                                            <dxe:ListEditItem Text="40GP" Value="40GP" />
                                            <dxe:ListEditItem Text="40GPD" Value="40GPD" />
                                            <dxe:ListEditItem Text="40HC" Value="40HC" />
                                            <dxe:ListEditItem Text="40HCD" Value="40HCD" />
                                            <dxe:ListEditItem Text="40HCN" Value="40HCN" />
                                            <dxe:ListEditItem Text="40OT" Value="40OT" />
                                            <dxe:ListEditItem Text="40RF" Value="40RF" />
                                            <dxe:ListEditItem Text="40HD" Value="40HD" />
                                            <dxe:ListEditItem Text="45GP" Value="45GP" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </DataItemTemplate>
                                <PropertiesComboBox DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Gst" FieldName="GstType" VisibleIndex="5"
                                Width="50" >
                                 
                            </dxwgv:GridViewDataTextColumn>
							
                            <dxwgv:GridViewDataTextColumn Caption="Rate" FieldName="Price" VisibleIndex="5"
                                Width="50">
                                <DataItemTemplate>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="80"
                                        ID="spin_Price" Height="21px" Value='<%# Bind("Price")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="5"
                                Width="50">
                                <DataItemTemplate>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.0" runat="server" Width="50"
                                        ID="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="1" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Unit" VisibleIndex="5"
                                Width="50" >
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox Width="80px" ID="txt_Unit" ClientInstanceName="txt_Unit"
                                        runat="server" Text='<%# Bind("Unit") %>'>
                                    </dxe:ASPxTextBox>
                                    </td>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Group By" FieldName="GroupBy" VisibleIndex="1"
                                Width="60" >
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox runat="server" Width="60" ID="txt_GroupBy" Text='<%# Bind("GroupBy") %>'>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="LocAmt" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Sub JobNo" FieldName="SubJobNo" VisibleIndex="5"
                                Width="50">
                                <DataItemTemplate>
                                    <a href='javascript: parent.parent.navTab.openTab("<%# Eval("SubJobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("SubJobNo") %>",{title:"<%# Eval("SubJobNo") %>", fresh:false, external:true});'><%# Eval("SubJobNo") %></a>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Inv No" VisibleIndex="5" SortIndex="0"
                                Width="120" >
                                <DataItemTemplate>
                                    <div class='<%# IsCostCreated(Eval("Id"))==true?"show":"hide" %>' style="min-width: 70px;">
                                        <a href='javascript: parent.parent.navTab.openTab("<%# DocNo(Eval("Id")) %>","/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=<%# DocNo(Eval("Id")) %>",{title:"<%# DocNo(Eval("Id")) %>", fresh:false, external:true});'><%# DocNo(Eval("Id")) %></a>
                                    </div>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Templates>
                            <EditForm>
                                <div style="display: none">
                                </div>
                                <table>
                                    <tr>
                                        <td>Charge Code
                                        </td>
                                        <td>
                                            <dxe:ASPxButtonEdit ID="txt_CostChgCode" ClientInstanceName="txt_CostChgCode" Width="100" runat="server" Text='<%# Bind("ChgCode")%>' ReadOnly='True'>
                                                <Buttons>
                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_CostChgCode,txt_CostDes);
                        }" />
                                            </dxe:ASPxButtonEdit>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxTextBox Width="265" ID="txt_CostDes" ClientInstanceName="txt_CostDes" 
                                                 runat="server" Text='<%# Bind("ChgCodeDe") %>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>Vendor
                                        </td>
                                        <td>
                                            <dxe:ASPxButtonEdit ID="txt_CostVendorId" ClientInstanceName="txt_CostVendorId" Width="80" runat="server" Text='<%# Bind("VendorId")%>'>
                                                <Buttons>
                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupVendor(txt_CostVendorId,txt_CostVendorName);
                        }" />
                                            </dxe:ASPxButtonEdit>
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxTextBox runat="server" BackColor="Control" Width="350" ID="txt_CostVendorName"
                                                ClientInstanceName="txt_CostVendorName" ReadOnly="true" Text=''>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Remark
                                        </td>
                                        <td colspan="3">
                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td colspan="4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>Cont No</td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>Cont Type</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="80" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContType") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="20HC" Value="20HC" />
                                                                <dxe:ListEditItem Text="20OT" Value="20OT" />
                                                                <dxe:ListEditItem Text="20RF" Value="20RF" />
                                                                <dxe:ListEditItem Text="20GP" Value="20GP" />
                                                                <dxe:ListEditItem Text="20FT" Value="20FT" />
                                                                <dxe:ListEditItem Text="20HD" Value="20HD" />
                                                                <dxe:ListEditItem Text="40FR" Value="40FR" />
                                                                <dxe:ListEditItem Text="40GP" Value="40GP" />
                                                                <dxe:ListEditItem Text="40GPD" Value="40GPD" />
                                                                <dxe:ListEditItem Text="40HC" Value="40HC" />
                                                                <dxe:ListEditItem Text="40HCD" Value="40HCD" />
                                                                <dxe:ListEditItem Text="40HCN" Value="40HCN" />
                                                                <dxe:ListEditItem Text="40OT" Value="40OT" />
                                                                <dxe:ListEditItem Text="40RF" Value="40RF" />
                                                                <dxe:ListEditItem Text="40HD" Value="40HD" />
                                                                <dxe:ListEditItem Text="45GP" Value="45GP" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td>Line Type</td>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="ASPxComboBox1" ClientInstanceName="txt_ContType" runat="server" Width="60" Value='<%# Bind("LineType") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="Job" Value="JOB" />
                                                                <dxe:ListEditItem Text="Cont" Value="CONT" />
                                                                <dxe:ListEditItem Text="Claim" Value="CL" />
                                                                <dxe:ListEditItem Text="Transport" Value="TP" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <table width="100%">
                                                <tr>
                                                    <td></td>
                                                    <td>Qty
                                                    </td>
                                                    <td>UOM
                                                    </td>
                                                    <td>Price
                                                    </td>
                                                    <td>Currency
                                                    </td>
                                                    <td>ExRate
                                                    </td>
                                                    <td>Amount
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                            ID="spin_CostCostQty" Text='<%# Bind("Qty")%>' Increment="0" DisplayFormatString="0.0" DecimalPlaces="1">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                         <dxe:ASPxTextBox Width="80px" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                                                runat="server" Text='<%# Bind("Unit") %>'>
                                                                            </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                            runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("Price")%>' Increment="0" DecimalPlaces="2">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" runat="server" Width="80" Text='<%# Bind("CurrencyId") %>' MaxLength="3">
                                                            <Buttons>
                                                                <dxe:EditButton Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostCostCurrency,spin_CostCostExRate);
                        }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                            DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("ExRate")%>' Increment="0">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                            BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                            Value='<%# Bind("LocAmt")%>'>
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <div style="text-align: right; padding: 2px 2px 2px 2px">

                                    <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                        <ClientSideEvents Click="function(s,e){grid.UpdateEdit();}" />
                                    </dxe:ASPxHyperLink>
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                </div>
                            </EditForm>
                        </Templates>
                        <Settings ShowFooter="true" />
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                        </TotalSummary>

                    </dxwgv:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td>

                    <dxe:ASPxButton ID="ASPxButton4" Width="80" runat="server" Text="Delete"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                            if(confirm('Confirm Delete All?')){
                                   grid.GetValuesOnCustomCallback('Delete',OnSaveCallback);   
                            }          
                                                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <table style="width: 100%; border:double">
            <tr>
                <td>
                    <div style="float:left;width:32%">
                        <table style="border-top:solid 1px #000000;border-left:solid 1px #000000;line-height:30px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="5" class="td_bottom_right" style="background:#e9e9e9">Container</td>
                            </tr>
                            <tr style="font-weight:bold;padding:2px;border-bottom:solid 1px #000000;">
                                <td class="td_bottom_right" width="20%">Cont No</td>
                                <td class="td_bottom_right" width="20%">Cont Type</td>
                                <td class="td_bottom_right" width="30%">Schedule Date</td>
                                <td class="td_bottom_right" width="20%">VGM Wt</td>
                                <td class="td_bottom_right" width="10%">Cont Status</td>
                            </tr>
                            <% DataTable tab = GetCont();
                               for(int i=0;i<tab.Rows.Count;i++){
                            %>
                            <tr style="padding:2px;">
                                <td class="td_bottom_right"><%=tab.Rows[i]["ContainerNo"] %></td>
                                <td class="td_bottom_right"><%=tab.Rows[i]["ContainerType"] %></td>
                                <td class="td_bottom_right"><%=SafeValue.SafeDateStr(tab.Rows[i]["ScheduleDate"]) %></td>
                                <td class="td_bottom_right"><%= tab.Rows[i]["Weight"] %></td>
                                <td class="td_bottom_right"><%=tab.Rows[i]["StatusCode"] %></td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                     <div style="float:right;width:67%;">
                         <table style="border-top:solid 1px #000000;border-left:solid 1px #000000;line-height:30px;" cellspacing="0" cellpadding="0">
                             <tr>
                                 <td colspan="7" class="td_bottom_right" style="background:#e9e9e9">Trips</td>
                             </tr>
                             <tr style="font-weight: bold;">
                                 <td class="td_bottom_right" width="20%">Trip No (Billing Remark)</td>
                                 <td class="td_bottom_right" width="5%">Trip Code</td>
                                 <td class="td_bottom_right" width="10%">Cont/Trailer No</td>
                                 <td class="td_bottom_right" width="10%">Status</td>
                                 <td class="td_bottom_right" width="10%">VehicleType</td>
                                 <td class="td_bottom_right" width="10%">From</td>
                                 <td class="td_bottom_right" width="10%">To</td>
                             </tr>
                              <% DataTable tab1 = GetTrip();
                                 for (int i = 0; i < tab1.Rows.Count; i++)
                                 {
                            %>
                            <tr style="padding:2px;">
                                <td class="td_bottom_right">
								<%=tab1.Rows[i]["TripIndex"] %>
								<span style="font-weight:bold;color:red;"><br><%= tab1.Rows[i]["BillingRemark"]%></span>
								</td>
                                <td class="td_bottom_right"><%=tab1.Rows[i]["TripCode"] %></td>
                                <td class="td_bottom_right"><%=tab1.Rows[i]["ContainerNo"] %></td>
                                <td class="td_bottom_right"><%=VilaStatus(tab1.Rows[i]["Statuscode"].ToString()) %></td>
                                <td class="td_bottom_right"><%=tab1.Rows[i]["RequestVehicleType"] %></td>
                                <td class="td_bottom_right"><%=tab1.Rows[i]["FromCode"] %></td>
                                <td class="td_bottom_right"><%=tab1.Rows[i]["ToCode"] %></td>
                            </tr>
                            <%} %>
                         </table>
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%;display:none">
            <tr>
                <td style="padding: 4px; text-align: center; background: #e9e9e9">
                    <b>WAREHOUSE CHARGES</b>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display:none">
                         <dxe:ASPxButton ID="btn_WhSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectWHAll();
                                    }" />
                    </dxe:ASPxButton>
                    </div>
                    <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server"
                        KeyFieldName="Id" Width="100%"
                       >
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsEditing Mode="EditFormAndDisplayRow" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="1"
                                Width="40">
                                <DataItemTemplate>
                                    <dxe:ASPxCheckBox ID="ack_IsOk" runat="server" Width="10" Checked="true">
                                    </dxe:ASPxCheckBox>
                                    <div style="display: none">
                                        <dxe:ASPxTextBox ID="txt_wh_Id" BackColor="Control" ReadOnly="true" runat="server"
                                            Text='<%# Eval("Id") %>' Width="150">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </DataItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                             <dxwgv:GridViewDataTextColumn Caption="Ref No" FieldName="RefNo" VisibleIndex="2" Width="120">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgCodeDes" VisibleIndex="2">
                                <DataItemTemplate>
                                    <dxe:ASPxLabel ID="lbl_ChgCode" runat="server" Text='<%# Eval("ChgCodeDes") %>'></dxe:ASPxLabel>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="CostQty" VisibleIndex="3" Width="50">
                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Uom" FieldName="Unit" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Price" FieldName="CostPrice" VisibleIndex="4" Width="50">
                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="GST" FieldName="CostGst" VisibleIndex="5" Width="50">
                                <PropertiesSpinEdit DisplayFormatString="{0:0.00}"></PropertiesSpinEdit>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CostCurrency" VisibleIndex="5" Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="ExRate" FieldName="costExRate" VisibleIndex="5" Width="50">
                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000000}"></PropertiesSpinEdit>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Amt" FieldName="CostLocAmt" VisibleIndex="5" Width="50">
                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                            </dxwgv:GridViewDataSpinEditColumn>
                        </Columns>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="920" EnableViewState="False">
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
