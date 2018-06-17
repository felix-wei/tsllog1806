<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingList.aspx.cs" Inherits="Modules_Freight_Job_BookingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script src="../script/jquery.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
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
        function upload_cargo_inline(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('UploadCargoline_'+rowIndex, upload_cargo_inline_callback);
            }, config.timeout);

        }
        function upload_cargo_inline_callback(res){
            popubCtr.SetHeaderText('Import Cargo');
            popubCtr.SetContentUrl('/Modules/Freight/Job/ImportOrder.aspx?sn='+res);
            popubCtr.Show();
        }
        function booking_batch_add() {
            Popup_BookingBatchAdd(booking_batch_add_cb);
        }
        function booking_import() {
            Popup_BookingImport(booking_batch_add_cb);
        }
        var Popup_BookingBatchAdd_cb = null;
        function Popup_BookingBatchAdd(cb) {
            Popup_BookingBatchAdd_cb = cb;
            popubCtr.SetContentUrl('/Modules/Freight/SelectPages/Booking_BatchAdd.aspx');
            popubCtr.SetHeaderText('Batch Add');
            popubCtr.Show();
        }
        function Popup_BookingImport(cb) {
            Popup_BookingBatchAdd_cb = cb;
            popubCtr.SetContentUrl('/Modules/Freight/Job/ImportOrder.aspx');
            popubCtr.SetHeaderText('Import Booking');
            popubCtr.Show();
        }
        function booking_batch_add_cb(v) {
            console.log(v);
            if (v == "success") {
                setTimeout(function () {
                    btn_search.OnClick(null,null);
                }, 500);
            }
        }
        function Popup_BookingBatchAdd_callback(v) {
            ClosePopupCtr();
            if (Popup_BookingBatchAdd_cb) {
                Popup_BookingBatchAdd_cb(v);
                Popup_BookingBatchAdd_cb = null;
            }
        }
        function BatchReceive() {
            if (confirm("Confirm Receive these Cargo?")) {
                var refnos = "";
                jQuery("input.batch").each(function () {
                    if(this.checked)
                        refnos += this.id + ',';
                });
                var pos = "BR" +refnos;
			 
                grid.GetValuesOnCustomCallback(pos, OnCallbackBatch);
            }
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input.batch").each(function () {
                this.click();
            });
        }
        function cargo_list_inline(rowIndex){
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('CargoListline_'+rowIndex, cargo_list_inline_callback);
            }, config.timeout);
        }
        function cargo_list_inline_callback(res){
            popubCtr.SetHeaderText('Cargo List ');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/CargoList.aspx?id=' + res);
            popubCtr.Show();
        }

        function confirmed_inline(rowIndex){
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Confirmedline_'+rowIndex, OnCallbackBatch);
            }, config.timeout);
        }
        function OnCallbackBatch(v) {
            if(v.indexOf('Action Error')>=0){
                alert(v);
            }else{
                alert(v);
                btn_search.OnClick(null,null);
            }
        }

    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsWh" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobHouse"
            KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Job No" Width="50px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="140"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Lot No" Width="70px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_BookingNo" runat="server" Width="100"></dxe:ASPxTextBox>
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
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Hbl No" Width="60px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HblNo" runat="server" Width="140"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Vessel"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Vessel" Width="100" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="Status"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_Type" ClientInstanceName="cbb_Type" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="P" Selected="true" />
                                <dxe:ListEditItem Text="Completed" Value="C" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False" ReadOnly="true">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="6">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="230px" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Product"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Product" runat="server" Width="140"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="Pol"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Pol" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="Pod"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Pod" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click" Width="140"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_ContBatchAdd" runat="server" Text="Add Booking" AutoPostBack="false" Width="100">
                            <ClientSideEvents Click="function(s,e){booking_batch_add();}" />
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Import Booking" AutoPostBack="false" Width="100">
                            <ClientSideEvents Click="function(s,e){booking_import();}" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="80" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Receive All" AutoPostBack="False" UseSubmitBehavior="false" Width="100px">
                            <ClientSideEvents Click="function(s,e){
                                 BatchReceive();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" OnPageIndexChanged="grid_PageIndexChanged" OnInit="grid_Init" OnCustomDataCallback="grid_CustomDataCallback"
                KeyFieldName="Id" Width="1000px" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowPager" PageSize="20"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" SortOrder="Ascending" SortIndex="0" Width="20" VisibleIndex="0">
                        <DataItemTemplate>
                            <div style="min-width: 20px;">
                                <input type="checkbox" class="batch" id='<%# Eval("Id")%>' />
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#" Width="140px" VisibleIndex="0">
                        <DataItemTemplate>
                            <div style="min-width: 140px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Job Status" FieldName="JobStatus" VisibleIndex="0" Width="180" SortIndex="1">
                         <DataItemTemplate>
                             <div style="display: <%# SafeValue.SafeString(Eval("JobStatus"))=="Booked"?"block":"none"%>">
                                 <dxe:ASPxButton ID="ASPxButton24" runat="server" Text="Confirmed" Width="40" AutoPostBack="false"
                                     ClientSideEvents-Click='<%# "function(s) { confirmed_inline("+Container.VisibleIndex+") }"  %>'>
                                 </dxe:ASPxButton>
                             </div>
                             <div style="display: <%# SafeValue.SafeString(Eval("JobStatus"))=="Booked"?"none":"block"%>">
                                 <%# Eval("JobStatus") %>
                             </div>
                             <div style="display:none">
                                  <dxe:ASPxLabel ID="lbl_JobId" runat="server" Text='<%# Eval("JobId") %>'></dxe:ASPxLabel>
                             </div>
                         </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Cargo Status" VisibleIndex="0" Width="180" SortIndex="1">
                        <DataItemTemplate>
                            <%# Status(Eval("CargoStatus")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Send Mode" FieldName="SendMode" VisibleIndex="0" Width="180" SortIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="BookingNo" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Hbl No" FieldName="HblNo" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="2" Width="160">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="2" Width="160">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="2" Width="160">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="SkuCode" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Consignee" FieldName="ConsigneeInfo" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="ConsigneeContact" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="ConsigneeTel" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="ConsigneeEmail" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PostCode" FieldName="ConsigneeZip" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Delivery Address" FieldName="ConsigneeAddress" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Verify Status" FieldName="ConsigneedVerifyStatus" VisibleIndex="3" Width="100">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Verify Time" FieldName="ConsigneedVerifyTime" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ConsigneedVerifyTime")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Verify Remark" FieldName="ConsigneedVerifyRemark" VisibleIndex="3" Width="180">
                        <DataItemTemplate>
                            <%# Eval("ConsigneedVerifyRemark") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="5"
                        Width="80">
                        <DataItemTemplate>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lbl_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                            </div>
                            <div style="float: left">
                                <dxe:ASPxButton ID="ASPxButton24" runat="server" Text="Cargo List" Width="40" AutoPostBack="false"
                                    ClientSideEvents-Click='<%# "function(s) { cargo_list_inline("+Container.VisibleIndex+") }"  %>'>
                                </dxe:ASPxButton>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Attachment" VisibleIndex="6" Width="100">
                        <DataItemTemplate>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Text='<%# Eval("ContNo") %>'></dxe:ASPxTextBox>
                            </div>
                            <input type="button" class="button" value="Upload" onclick="upload_inline(<%# Container.VisibleIndex %>);" />
                            <br />
                            <br />
                            <div class='<%# FilePath(SafeValue.SafeInt(Eval("Id"),0))!=""?"show":"hide" %>' style="min-width: 70px;">
                                <a href='<%# "/Photos/"+FilePath(SafeValue.SafeInt(Eval("Id"),0)) %>' target="_blank">View</a>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
                Width="1050" EnableViewState="False">
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
