<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnbilledStorageByItem.aspx.cs" Inherits="Modules_Tpt_Report_UnbilledStorageByItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
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
              gridview: 'grid',
          }
          function $(s) {
              return document.getElementById(s) ? document.getElementById(s) : s;
          }
          function keydown(e) {
              if (e.keyCode == 27) { parent.AfterPopub(); }
          }
          document.onkeydown = keydown;
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
              if (v != null && v.indexOf('Action Error') >= 0) {
                  alert(v);
              }
              else {
                  alert("Save Success");
                  
              }
          }
          function OnCreateInvCallback(v) {
              if (v != null && v.indexOf('Action Error') >= 0) {
                  alert(v);
              }
              else {
                  alert("Create Invoice Success");
                  grid.Refresh();
                  document.location.reload();
                  parent.parent.navTab.openTab(v, "/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
              }

          }
          function AfterPopub() {
              popubCtr.Hide();
              popubCtr.SetContentUrl('about:blank');
              grid.Refresh();
          }
          function PopupBillRate() {

              popubCtr.SetHeaderText('Bill Rate');
              popubCtr.SetContentUrl('../SelectPage/SelectBillRate.aspx?no=' + lbl_JobNo.GetText() + '&type=' + lbl_JobNo.GetText() + '&client=' + lbl_Client.GetText());
              popubCtr.Show();
          }
          function tallysheet(rowIndex){
              console.log(rowIndex);

              loading.show();
              setTimeout(function () {
                  grid.GetValuesOnCustomCallback('Tallysheet_'+rowIndex, OpenTallySheet);
              }, config.timeout);
          }
          function OpenTallySheet(res){
              var ar = res.split('_');
              parent.navTab.openTab(ar[0]+"_"+"TallySheet", "/Modules/Tpt/SelectPage/TallySheet.aspx?no=" + ar[0], { title: ar[0]+"_"+"TallySheet", fresh: false, external: true });
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
        <div style="display:none">
            <dxe:ASPxLabel ID="lbl_JobNo" ClientInstanceName="lbl_JobNo" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lbl_Client" ClientInstanceName="lbl_Client"  runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lbl_Type" ClientInstanceName="lbl_Type"  runat="server"></dxe:ASPxLabel>
        </div>
        <table style="width: 90%;margin-left:10px">
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="JobNo"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_JobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                </td>

                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="From"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                </td>
                
            </tr>
            <tr>
                <td>Client</td>
                <td>
                    <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False">
                        <Buttons>
                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName,'C',null,null);
                                                                        }" />
                    </dxe:ASPxButtonEdit>
                </td>
                <td colspan="4">
                    <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Reload" Width="90%" AutoPostBack="False" OnClick="btn_search_Click"
                        UseSubmitBehavior="False">
                    </dxe:ASPxButton>
                </td>

                <td>
                    <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="90%" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton3" Width="90%" runat="server" Text="Save All"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('Save',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="btn_CreateInv" Width="90%" runat="server" Text="Create Inv"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                                              if(confirm('Confirm Create Invoice for '+txt_DocDt.GetText()+' ?')){
                                    grid.GetValuesOnCustomCallback('OK',OnCreateInvCallback);   
                            }              
                                                        }" />
                    </dxe:ASPxButton>
                    <div style="display: none">
                        <dxe:ASPxLabel ID="lbl_DocNo" ClientInstanceName="lbl_DocNo" runat="server"></dxe:ASPxLabel>
                    </div>
                </td>
                <td>Invoice Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_DocDt" ClientInstanceName="txt_DocDt" runat="server" Width="100"
                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
        </table>
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" OnCustomDataCallback="grid_CustomDataCallback"
                         KeyFieldName="Id" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting"
                        OnRowInserting="grid_RowInserting" OnInitNewRow="grid_InitNewRow"
                        OnInit="grid_Init" Width="100%" AutoGenerateColumns="False">
                        <SettingsEditing Mode="EditForm" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                                <EditButton Visible="True" />
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewCommandColumn Visible="false" VisibleIndex="999" Width="5%" >
                                <DeleteButton Visible="true" />
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="1"
                                Width="40">
                                <DataItemTemplate>
                                    <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
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
                            <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1" >
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDes" VisibleIndex="1"
                                Width="150" SortOrder="Descending">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                Width="150">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="JobNo" Caption="Job No" Width="150" VisibleIndex="1">
                                <DataItemTemplate>
                                    <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                                    <div style="display:none">
                                        <dxe:ASPxLabel runat="server" ID="lbl_JobNo" Text='<%# Bind("JobNo") %>'></dxe:ASPxLabel>
                                    </div>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Name" Caption="Client" Width="150" VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ContNo" Caption="Cont No" Width="150" VisibleIndex="1" Visible="false">
                                <DataItemTemplate>
                                    <dxe:ASPxLabel runat="server" ID="lbl_ContNo" Text='<%# Bind("ContNo") %>'></dxe:ASPxLabel>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="ContType" Caption="Cont Type" Width="150" VisibleIndex="1" Visible="false">
                                <DataItemTemplate>
                                    <dxe:ASPxLabel runat="server" ID="lbl_ContType" Text='<%# Bind("ContType") %>'></dxe:ASPxLabel>
                                </DataItemTemplate>
                                <PropertiesComboBox DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Rate" FieldName="Price" VisibleIndex="5"
                                Width="50">
                                <DataItemTemplate>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="80"
                                        ID="spin_Price" Height="21px" Value='<%# Bind("Price")%>' DecimalPlaces="3" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="5"
                                Width="50">
                                <DataItemTemplate>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="50"
                                        ID="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="3" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Group By" FieldName="GroupBy" VisibleIndex="5"
                                Width="150">
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_GroupBy" Text='<%# Bind("GroupBy") %>'>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Unit" VisibleIndex="5"
                                    Width="50">
                                    <DataItemTemplate>
                                        <dxe:ASPxTextBox Width="80px" ID="txt_Unit" ClientInstanceName="txt_Unit"
                                            runat="server" Text='<%# Bind("Unit") %>'>
                                        </dxe:ASPxTextBox>
                                        </td>
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
                        </Columns>
                        <Settings ShowFooter="true" />
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                        </TotalSummary>

                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="900" EnableViewState="False">
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
