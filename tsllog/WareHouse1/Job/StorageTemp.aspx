<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageTemp.aspx.cs" Inherits="WareHouse_Job_StorageTemp" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=B88D1754D700E49A" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dxe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

                window.location = 'OutboundTemp.aspx?no=' + v;
            }
        }

        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function AfterPopubMultiInv1(v) {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            window.location = 'OutboundTemp.aspx?no=' + v;
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
                                <tr style="display:none;">
                                     
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
                                                           <tr style="display:none;">

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
                                 <tr style="display:none;">
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
								 <tr  >
                                    <td colspan="8">
                                        <hr>
                                        <table style="width:1000px;display:none;">
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
                                                    <dxe:ASPxMemo ID="memo_Description" Rows="4" Width="150" ClientInstanceName="memo_Description" Height="100%" Text='<%# Eval("ItemDes") %>'
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
                                                <td >
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="160" Value='<%# Eval("Charges") %>'
                                                        DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges" ClientInstanceName="spin_Charges" Increment="0">
                                                    </dxe:ASPxSpinEdit>
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
