<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="OfficeMoveTemp.aspx.cs" Inherits="WareHouse_Job_OfficeMoveTemp" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=B88D1754D700E49A" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
         <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript">
        var isUpload = false;
    </script>
    <script type="text/javascript">
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else if (v == "") {
                detailGrid.Refresh();
            }

            else if (v != null) {

                window.location = 'OfficeMoveTemp.aspx?no=' + v;
            }
        }

        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function AfterPopubMultiInv1(v) {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            window.location = 'OfficeMoveTemp.aspx?no=' + v;
        }

    </script>
</head>
<body>
<form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsIssue" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobInfo"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefWarehouse" KeyMember="Id" />
            <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXSalesman" KeyMember="Code" />
            <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefLocation" KeyMember="Id" FilterExpression="Loclevel='Unit'" />
            <dxwgv:ASPxGridView ID="grid_Issue" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="false" DataSourceID="dsIssue"
                OnBeforePerformDataSelect="grid_Issue_DataSelect" OnInitNewRow="grid_Issue_InitNewRow"
                OnInit="grid_Issue_Init" OnCustomDataCallback="grid_Issue_CustomDataCallback" OnCustomCallback="grid_Issue_CustomCallback"
                OnHtmlEditFormCreated="grid_Issue_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>

					<table style="width:960px;">
					<tr>
					<td style="font-size:20px; width=480" nowrap>
					<%# string.Format("[<b>{1} : {0}</b>] [{2}]",Eval("JobNo"),Eval("JobType"),Eval("WorkStatus")) %>
					</td>
					<td>
					</td>
					<td width=240>
 <dxe:ASPxButton ID="btn_search" Width="180" runat="server" Text="Reload Job Order" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        detailGrid.Refresh();
                    }" />
                        </dxe:ASPxButton>
					</td>
					<td align=right width=240>
                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="180" AutoPostBack="false"
                                            Text="Save" Enabled='<%# SafeValue.SafeString(Eval("JobStage"),"")!="Job Close" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                                 }" />
                                        </dxe:ASPxButton>

					</td>
					</tr>
                       </table>
                        <div style="padding: 2px 2px 2px 2px">
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                            <table style="width:960px;">

                                <tr style="display:none">
                                    <td>Job No</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" BackColor="Control" ID="txt_DoNo" Text='<%# Eval("JobNo") %>' ClientInstanceName="txt_DoNo">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Job Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="100%" runat="server" Value='<%# Eval("JobDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                   <td>Job Stage</td>
                                    <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100%" ID="cmb_Status"
                                            runat="server" ReadOnly="True" ClientInstanceName="cmb_Status" OnCustomJSProperties="cmb_Status_CustomJSProperties" Text='<%# Eval("JobStage") %>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Customer Inquiry" Value="Customer Inquiry" />
                                                <dxe:ListEditItem Text="Site Survey" Value="Site Survey" />
                                                <dxe:ListEditItem Text="Costing" Value="Costing" />
                                                <dxe:ListEditItem Text="Quotation" Value="Quotation" />
                                                <dxe:ListEditItem Text="Job Confirmation" Value="Job Confirmation" />
                                                <dxe:ListEditItem Text="Billing" Value="Billing" />
                                                <dxe:ListEditItem Text="Job Completion" Value="Job Completion" />
                                                <dxe:ListEditItem Text="Job Close" Value="Job Close" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                     <td>JobType</td>
                                    <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="cmb_JobType" runat="server" Width="100%"  ClientInstanceName="cmb_JobType"  Text='<%# Eval("JobType")%>' OnCustomJSProperties="cmb_JobType_CustomJSProperties">
                                            <Items>
                                                <dxe:ListEditItem Text="Local Move" Value="Local Move" />
                                                <dxe:ListEditItem Text="Office Move" Value="Office Move" />
                                                <dxe:ListEditItem Text="Outbound" Value="Outbound" />
                                                <dxe:ListEditItem Text="Storage" Value="Storage" />
                                                <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                                <dxe:ListEditItem Text="Project" Value="Project" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
									  <td>

                                    </td>
                                </tr>
                                <tr>
                                     
                                    <td>Payment Term</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_PayTerm" runat="server" Width="150" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("PayTerm")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="CASH" Value="CASH" />
                                                <dxe:ListEditItem Text="30DAYS" Value="30DAYS" />
                                                <dxe:ListEditItem Text="60DAYS" Value="60DAYS" />
                                                <dxe:ListEditItem Text="90DAYS" Value="90DAYS" />
                                                <dxe:ListEditItem Text="120DAYS" Value="120DAYS" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Currency
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Eval("Currency") %>' runat="server" Width="120" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>ExRate
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_ExRate" ClientInstanceName="spin_ExRate" DisplayFormatString="0.000000"
                                            runat="server" Width="120" Value='<%# Eval("ExRate")%>' DecimalPlaces="6" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>WareHouse</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_WareHouseId" ClientInstanceName="txt_WareHouseId" runat="server" Text='<%# Eval("WareHouseId")%>' Width="120" HorizontalAlign="Left" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                              PopupWh(txt_WareHouseId,null);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2">Remark</td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo runat="server" Width="375" Rows="4" ID="txt_Remark" Text='<%# Eval("Remark") %>' ClientInstanceName="txt_Remark3">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Quotation Validity</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_ExpiryDate" Width="120" runat="server" Value='<%# Eval("ExpiryDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                     <td>Sales</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_SalesId" ClientInstanceName="cmb_SalesId" runat="server" DataSourceID="dsSalesman" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Value='<%# Eval("Value2") %>' TextField="Code" ValueField="Code" Width="120">
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                            </Columns>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>WorkStatus</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_WorkStatus" runat="server" Width="120" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" OnCustomJSProperties="cmb_WorkStatus_CustomJSProperties" Value='<%# Eval("WorkStatus")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                <dxe:ListEditItem Text="Working" Value="Working" />
                                                <dxe:ListEditItem Text="Complete" Value="Complete" />
                                                <dxe:ListEditItem Text="Unsuccess" Value="Unsuccess" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                   
                                </tr>
								<tr>
                                    <td colspan="8">
                                        <hr>
                                        <table style="width:1000px;">
                                            <tr>

                                                <td colspan="4" style="background-color: Gray; color: White;">
                                                    <b>Moving Details</b>
                                                </td>
                                                <td colspan="2" style="background-color: Gray; color: White;">
                                                    <b>Item Description</b>
                                                </td>
                                                <td colspan="6" style="background-color: Gray; color: White; width: 680px">
                                                    <b>Moving Schedule</b>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>Volumn</td>
                                                <td colspan="3">
                                                    <dxe:ASPxTextBox ID="memo_Volumn"  Width="100" ClientInstanceName="memo_Volumn" Text='<%# Eval("VolumneRmk") %>'
                                                        runat="server">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td colspan="2" rowspan="4">
                                                    <dxe:ASPxMemo ID="memo_Description" Rows="4" Width="150" ClientInstanceName="memo_Description" Height="110" Text='<%# Eval("ItemDes") %>'
                                                        runat="server">
                                                    </dxe:ASPxMemo>
                                                </td>
                                                <td>Pack Date</td>
                                                <td>
                                                    <dxe:ASPxMemo ID="txt_PackRemark" Rows="1" runat="server" Width="160" Text='<%# Eval("PackRmk") %>'></dxe:ASPxMemo>
                                                </td>
                                                <td>Date</td>
                                                <td>
                                                    <dxe:ASPxDateEdit ID="date_Pack" Width="100" runat="server" Value='<%# Eval("PackDate") %>' ClientInstanceName="date_Pack"
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
                                                </td>

                                                <td>Via Warehouse</td>
                                                <td>
                                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Via" Text='<%# Eval("ViaWh") %>'
                                                        runat="server">
                                                        <Items>
  
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                            <dxe:ListEditItem Text="Normal" Value="Normal" />
                                                            <dxe:ListEditItem Text="Aircon" Value="Aircon" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>Net Cuft</td>
                                                <td colspan="3">
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("Volumne") %>'
                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Volumne" ClientInstanceName="spin_Volumne" Increment="0">
                                                    </dxe:ASPxSpinEdit>

                                                </td>
                                                
                                                 <td>Move Date</td>
                                                <td>
                                                    <dxe:ASPxMemo ID="txt_MoveRemark" Rows="1" runat="server" Width="160" Text='<%# Eval("MoveRmk") %>'></dxe:ASPxMemo>
                                                </td>
                                                <td>Date</td>
                                                <td>
                                                    <dxe:ASPxDateEdit ID="date_MoveDate" Width="100" runat="server" Value='<%# Eval("MoveDate") %>'
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
                                                </td>

                                                <td>Storage Start</td>
                                                <td>
                                                    <dxe:ASPxDateEdit ID="date_StorageStartDate" Width="100" runat="server" Value='<%# Eval("StorageStartDate") %>'
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>No of Trip</td>
                                                <td colspan="3">
                                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_TripNo" Text='<%# Eval("TripNo") %>' ClientInstanceName="txt_TripNo">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td >TruckNo</td>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="160px" ID="txt_TruckNo" ClientInstanceName="txt_TruckNo" Text='<%# Eval("TruckNo") %>'>
                                                    </dxe:ASPxTextBox>
                                                </td>

                                                <td>Free Storage Days</td>
                                                <td >
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("StorageFreeDays") %>' DecimalPlaces="0" ID="spin_StorageFreeDays" ClientInstanceName="spin_StorageFreeDays" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                 <td>Storage Days</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("StorageTotalDays") %>' DecimalPlaces="0" ID="spin_STotalDays" ClientInstanceName="spin_STotalDays" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>HeadCount</td>
                                                <td colspan="3">
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("HeadCount") %>'
                                                         DecimalPlaces="0" ID="spin_HeadCount" ClientInstanceName="spin_HeadCount" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                  <td>Charges</td>
                                                <td colspan="3">
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="160" Value='<%# Eval("Charges") %>'
                                                        DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges" ClientInstanceName="spin_Charges" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                            <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound")?"table-row": "none" %>">

                                                <td>Mode </td>


                                                <td colspan="3">

                                                    <dxe:ASPxComboBox ID="cmb_Mode" runat="server" Width="100" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("Mode")%>'>
                                                        <Items>
                                                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                                                            <dxe:ListEditItem Text="20''" Value="20''" />
                                                            <dxe:ListEditItem Text="40''HC" Value="40''HC" />
                                                            <dxe:ListEditItem Text="20 Con" Value="20 Con" />
                                                            <dxe:ListEditItem Text="40 Con" Value="40 Con" />
                                                            <dxe:ListEditItem Text="Sea" Value="Sea" />
                                                            <dxe:ListEditItem Text="Air" Value="Air" />
                                                            <dxe:ListEditItem Text="Road" Value="Road" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td width="150px" colspan="2"></td>
                                                <td>ServiceType </td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_ServiceType" runat="server" Width="160" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("ServiceType")%>'>
                                                        <Items>
                                                            <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                                            <dxe:ListEditItem Text="Door to Door" Value="Door to Door" />
                                                            <dxe:ListEditItem Text="Door to Port" Value="Door to Port" />
                                                            <dxe:ListEditItem Text="Origin Services" Value="Origin Services" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td>Port of Entry </td>
                                                <td>
                                                    <dxe:ASPxButtonEdit ID="btn_PortOfEntry" ClientInstanceName="btn_PortOfEntry" runat="server" MaxLength="5"
                                                        Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("EntryPort") %>'>
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPort(btn_PortOfEntry,null);
                                                                    }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>Eta </td>
                                                <td>
                                                    <dxe:ASPxDateEdit ID="date_Eta" Width="100" runat="server" Value='<%# Eval("Eta") %>'
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                            </tr>
                                <tr>
                                    <td colspan="12">
                                        <hr>
                                        <table>
                                            <tr>
                                                <td style="width: 80px;">Creation</td>
                                                <td style="width: 200px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateTimeStr( Eval("CreateDateTime"))%></td>
                                                <td style="width: 100px;">Last Updated</td>
                                                <td style="width: 200px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateTimeStr(Eval("UpdateDateTime"))%></td>
                                                <td style="width: 80px;"></td>
                                                <td style="width: 160px; display: none;">Job Status
                                                                        <dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text='<%#Eval("JobStage") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                        <hr>
                                    </td>
                                </tr>
                            </table>
                            
                            <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="100%" Height="440px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Name="BookingDetails" Text="Survery" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl5" runat="server">
                                                <table style="width:960px">
                                                    
                                                    <tr style="text-align:left;">
                                                        <td colspan="2">Full Packing</td>
                                                        <td>
                                                            <dxe:ASPxComboBox  Width="100" ID="cmb_FullPacking" Text='<%# Eval("Item1") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Full Packing Details</td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_Details" ClientInstanceName="txt_Details" Text='<%# Eval("ItemDetail1") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Full Un Packing</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_UnFull" Text='<%# Eval("Item2") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Unpack Details</td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_UnpackDetails" ClientInstanceName="txt_UnpackDetails" Text='<%# Eval("ItemDetail2") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Insurance</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Insurance" Text='<%# Eval("Item3") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Insurance Percentage</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_Percentage" ClientInstanceName="txt_Percentage" Text='<%# Eval("ItemValue3") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Insurance Value</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_Value" ClientInstanceName="txt_Value" Text='<%# Eval("ItemData3") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Insurance Premium</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%" Value='<%# Eval("ItemPrice3") %>'
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="txt_Premium" ClientInstanceName="txt_Premium" Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"> Piano Apply</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_PianoApply" Text='<%# Eval("Item4") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Paino Details</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_PainoDetails" ClientInstanceName="txt_PainoDetails" Text='<%# Eval("ItemDetail4") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%" Value='<%# Eval("ItemPrice4") %>'
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges1" ClientInstanceName="spin_Charges1" Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Safe</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Safe"
                                                                runat="server" Text='<%# Eval("Item5") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Brand</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_Brand" ClientInstanceName="txt_Brand" Text='<%# Eval("ItemValue5") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Weight in KG:</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%"
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Weight" ClientInstanceName="spin_Weight" Increment="0" Value='<%# Eval("ItemPrice5") %>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Crating</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Crating"
                                                                runat="server" Text='<%# Eval("Item6") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Details</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Details1" ClientInstanceName="txt_Details1" Text='<%# Eval("ItemDetail6") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%"
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges2" ClientInstanceName="spin_Charges2" Increment="0" Value='<%# Eval("ItemPrice6") %>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Handyman Services</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Handyman"
                                                                runat="server" Text='<%# Eval("Item7") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Complimentary</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Complimentory"
                                                                runat="server" Text='<%# Eval("ItemValue7") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Details</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Details2" ClientInstanceName="txt_Details2" Text='<%# Eval("ItemDetail7") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%"
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges3" ClientInstanceName="spin_Charges3" Increment="0" Value='<%# Eval("ItemPrice7") %>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Maid Services</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Maid"
                                                                runat="server" Text='<%# Eval("Item8") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Complimentary</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Complimentory1"
                                                                runat="server" Text='<%# Eval("ItemValue8") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Details</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Details3" ClientInstanceName="txt_Details3" Text='<%# Eval("ItemDetail8") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%"
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges4" ClientInstanceName="spin_Charges4" Increment="0" Value='<%# Eval("ItemPrice8") %>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Shuttling Services</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Shifting"
                                                                runat="server" Text='<%# Eval("Item9") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Complimentary</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Complimentory2" Text='<%# Eval("ItemValue9") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Details</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Details4" ClientInstanceName="txt_Details4" Text='<%# Eval("ItemDetail9") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%"
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges5" ClientInstanceName="spin_Charges5" Increment="0" Value='<%# Eval("ItemPrice9") %>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Disposal Services</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Disposal"
                                                                runat="server" Text='<%# Eval("Item10") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Complimentary</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Complimentory3" Text='<%# Eval("ItemValue10") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Details</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Details5" ClientInstanceName="txt_Details5" Text='<%# Eval("ItemDetail10") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%"
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges6" ClientInstanceName="spin_Charges6" Increment="0" Value='<%# Eval("ItemPrice10") %>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Additional Pick-Up</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_PickUp"
                                                                runat="server" Text='<%# Eval("Item11") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Details</td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_Details6" ClientInstanceName="txt_Details6" Text='<%# Eval("ItemDetail11") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Additional Delivery</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Additional" Text='<%# Eval("Item12") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Details</td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_Details7" ClientInstanceName="txt_Details7" Text='<%# Eval("ItemDetail12") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Bad Access</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_BadAccess" Text='<%# Eval("Item13") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Origin </td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Origin" Text='<%# Eval("ItemValue13") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Destination</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Destination" Text='<%# Eval("ItemData13") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">Storage</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Storage" Text='<%# Eval("Item14") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Complimentary</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Complimentory4" Text='<%# Eval("ItemValue14") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes/No" Value="Yes/No" />
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Details</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Details8" ClientInstanceName="txt_Details8" Text='<%# Eval("ItemDetail14") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%" Value='<%# Eval("ItemPrice14") %>'
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges7" ClientInstanceName="spin_Charges7" Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr style="text-align:left; vertical-align:top">
                                                        <td  colspan="2">Notes</td>
                                                        <td colspan="8">
                                                             <dxe:ASPxMemo ID="memo_Notes" Rows="4" Width="100%" ClientInstanceName="memo_Notes" Height="110" Text='<%# Eval("Notes") %>'
                                                                            runat="server">
                                                                        </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">How did you come to know about our Company</td>
                                                        <td colspan="6">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_How" ClientInstanceName="txt_How" Text='<%# Eval("Answer1") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Other</td>
                                                        <td colspan="6">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Other" ClientInstanceName="txt_Other" Text='<%# Eval("Answer2") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Move Competitors</td>
                                                        <td colspan="6">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Move" ClientInstanceName="txt_Move" Text='<%# Eval("Answer3") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">Cancel/Unsuccess Remark</td>
                                                        <td colspan="6">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_UnsuccessRemark" ClientInstanceName="txt_UnsuccessRemark" Text='<%# Eval("Answer4") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>								
                                    <dxtc:TabPage Text="Quotation" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl10" runat="server">
                                               <table style="width:960px;">
                                                  <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>COVER LETTER:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <dxe:ASPxHtmlEditor ID="txt_Attention1" ToolbarMode="None" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention1") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                 <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>TERMS & CONDITIONS:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <dxe:ASPxHtmlEditor ID="txt_Attention2" ToolbarMode="None" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention2") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>SPECIAL REMARKS:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention3" ToolbarMode="None" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention3") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">
                                                            <b>INTERNAL REMARKS:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention4" ToolbarMode="None" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention4") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>
                            </dxtc:ASPxPageControl>
                            <hr />
                        </div>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="970" EnableViewState="False">
                
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="970" EnableViewState="False">

                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
