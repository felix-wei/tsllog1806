﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignExportList.aspx.cs" Inherits="Modules_Freight_Job_AssignExportList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        function assign_one(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Assignline_' + rowIndex, assign_one_inline_callback);
            }, config.timeout);
        }
        function assign_one_inline_callback(res) {
            popubCtr.SetHeaderText('Import Cargo');
            popubCtr.SetContentUrl('/Modules/Freight/SelectPages/SelectAssignedCargo.aspx?id=' + res);
            popubCtr.Show();
        }
        function OnCallback(v) {
            if (v.indexOf('Action Error') >= 0) {
                //alert('Action Error,pls select at least one');
                parent.notice('Action Error', 'pls select at least one', 'warn');
            }
            else if (v.indexOf('Action Success') >= 0) {
                //alert('Action Error,pls select at least one');
                parent.notice('Action Success', v, 'success');
                btn_search.OnClick(null, null);
            }
            else if (v == 'NO Export') {
                parent.notice('Action Error', 'pls create a export job firstly', 'warn');
                //alert('Action Error,pls create a export job firstly');
            }
            else if (v != null && v.length > 0) {
                parent.notice(v, '', 'warn');
                //alert(v);
                btn_search.OnClick(null, null);
            }
            else {
                //alert("Successfully Assign Driver.")
                //
                parent.notice('Save Successful', '', 'success');
                btn_search.OnClick(null, null);
            }
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function AfterPopub() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            btn_search.OnClick(null, null);
        }
        function cargo_list_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('CargoListline_' + rowIndex, cargo_list_inline_callback);
            }, config.timeout);
        }
        function cargo_list_inline_callback(res) {
            popubCtr.SetHeaderText('Cargo List ');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/CargoList.aspx?id=' + res);
            popubCtr.Show();
        }
        function OnCallbackBatch(v) {
            alert(v);
            btn_search.OnClick(null, null);
        }
        function pick_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Pickline_' + rowIndex, pick_list_inline_callback);
            }, config.timeout);
        }
        function pick_list_inline_callback(res) {
            if (res.indexOf('Save Error') >= 0) {
                console.log('===========', res);
                parent.notice(res, '', 'success');
                //alert('Save Error');
            }
            else if (res == 'NO Export') {
                parent.notice('Action Error,pls create a export job firstly', '', 'warn');
                //alert('Action Error,pls create a export job firstly');
            }
            else {
                parent.notice('Save Successful', '', 'success');
                btn_search.OnClick(null, null);
                //grid.Refresh();
            }
        }
        function save_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('Saveline_' + rowIndex, save_list_inline_callback);
            }, config.timeout);
        }
        function save_list_inline_callback(res) {
            if (res.indexOf('Save Error') >= 0) {
                console.log('===========', res);
                //alert('Save Error');
                parent.notice('Save Error', '', 'warn');
            } else {
                parent.notice('Save Successful', '', 'success');
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
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Job No" Width="50px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="120"></dxe:ASPxTextBox>
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
                    <td>By</td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_Date" ClientInstanceName="cbb_Date" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Job Date" Value="Job" Selected="true" />
                                <dxe:ListEditItem Text="Ship Date" Value="Ship" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Hbl No" Width="60px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HblNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Vessel"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Vessel" Width="100" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="Export"></dxe:ASPxLabel>
                    </td>
                     <td>
                        <dxe:ASPxComboBox ID="cbb_Type" ClientInstanceName="cbb_Type" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="YES" Value="Y" />
                                <dxe:ListEditItem Text="NO" Value="N"  Selected="true"/>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                     <td>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Product"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Product" runat="server" Width="100"></dxe:ASPxTextBox>
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
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="120" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btnSelect" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="100" AutoPostBack="False"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Assign" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Assign Date" AutoPostBack="false" Width="100"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('Assign',OnCallback);
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="Pol"></dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Width="100" HorizontalAlign="Left" AutoPostBack="False"
                                        MaxLength="5">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pol);
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Create Export" AutoPostBack="false" Width="100"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('New',OnCallback);
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_assign1" runat="server" Text="Assgin To Export" AutoPostBack="false" Width="100"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td width="80%"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" Width="1000px" AutoGenerateColumns="False" OnPageIndexChanged="grid_PageIndexChanged" OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowPager" PageSize="20"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" FieldName="Id" SortIndex="0" SortOrder="Ascending" Width="10" VisibleIndex="0">
                        <DataItemTemplate>
                            <table style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Save" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { save_inline("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton24" runat="server" Text="Assign" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { pick_inline("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                </tr>
                            </table>
                            
                             <div style="display: none">
                                <dxe:ASPxLabel ID="lbl_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="#" Width="20" VisibleIndex="0" Visible="false">
                        <DataItemTemplate>
                            <div style="min-width: 20px;">
                                <input type="button" value="Assign Delivery"  onclick="assign_one(<%# Container.VisibleIndex %>);"/>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="#" Width="140px" VisibleIndex="0">
                        <DataItemTemplate>
                            <div style="min-width: 140px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Ship Date" FieldName="ShipDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%--<%# SafeValue.SafeDateStr(Eval("TptDate")) %>--%>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                                <dxe:ASPxDateEdit ID="date_ShipDate" runat="server" Width="120px" Value='<%# Eval("ShipDate") %>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none"%>">
                                <%# SafeValue.SafeDateStr(Eval("ShipDate")) %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ship Index" FieldName="ShipIndex" VisibleIndex="3" Width="80">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                                <dxe:ASPxTextBox ID="txt_ShipIndex" ClientInstanceName="txt_ShipIndex" Value='<%# Eval("ShipIndex") %>' runat="server" Width="80">
                                </dxe:ASPxTextBox>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none"%>">
                                <%# Eval("ShipIndex") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Port Code" FieldName="ShipPortCode" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                            <dxe:ASPxTextBox ID="txt_ShipPortCode" OnInit="txt_ShipPortCode_Init" Width="120" runat="server" Text='<%# Eval("ShipPortCode") %>'>
                            </dxe:ASPxTextBox>
                                </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none"%>">
                                <%# Eval("ShipPortCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Cont Index" FieldName="ContIndex" VisibleIndex="3" Width="80">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                                <dxe:ASPxComboBox ID="cbb_ContIndex" ClientInstanceName="cbb_ContIndex" Value='<%# Eval("ContIndex") %>' runat="server" Width="80">
                                    <Items>
                                        <dxe:ListEditItem Text="1" Value="1" Selected="true" />
                                        <dxe:ListEditItem Text="2" Value="2" />
                                        <dxe:ListEditItem Text="3" Value="3" />
                                        <dxe:ListEditItem Text="4" Value="4" />
                                        <dxe:ListEditItem Text="5" Value="5" />
                                        <dxe:ListEditItem Text="6" Value="6" />
                                        <dxe:ListEditItem Text="7" Value="7" />
                                        <dxe:ListEditItem Text="8" Value="8" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none"%>">
                                <%# Eval("ContIndex") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="QtyOrig" VisibleIndex="3" Width="60">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                                <dxe:ASPxSpinEdit ID="spin_Qty" Value='<%# Eval("BalQty") %>' runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none"%>">
                                <%# Eval("BalQty") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="WeightOrig" VisibleIndex="3" Width="60">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                                <dxe:ASPxSpinEdit ID="spin_Weight" Value='<%# Eval("BalWeight") %>' runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none"%>">
                                <%# Eval("BalWeight") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="VolumeOrig" VisibleIndex="3" Width="60">
                        <DataItemTemplate>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="WGR"?"block":"none"%>">
                                <dxe:ASPxSpinEdit ID="spin_Volume" Value='<%# Eval("BalVolume") %>' runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                            </div>
                            <div style="display: <%# SafeValue.SafeString(Eval("JobType"))=="EXP"?"block":"none"%>">
                                <%# Eval("BalVolume") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Vessel" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="BookingNo" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Hbl No" FieldName="HblNo" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
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
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="5"
                        Width="80">
                        <DataItemTemplate>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                            <div style="float: left">
                                <dxe:ASPxButton ID="ASPxButton24" runat="server" Text="Cargo List" Width="40" AutoPostBack="false"
                                    ClientSideEvents-Click='<%# "function(s) { cargo_list_inline("+Container.VisibleIndex+") }"  %>'>
                                </dxe:ASPxButton>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
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
