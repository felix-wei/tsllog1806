<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="ScheduleEdit.aspx.cs" Inherits="WareHouse_Job_ScheduleEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr1(); }
        }
        document.onkeydown = keydown;

    </script>
    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        var clientType = null;
        var clientAcCode = null;
        function PutValue(s, name) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                clientId = null;
                clientName = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
                PutDetAmt();
            }
        }
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else if (v == "") {
                detailGrid.Refresh();
            }
            else if (v == "No Price") {
                alert("No Price,Can not Confirm");
            }
            else if (v != null) {
                detailGrid.Refresh();
            }
        }
        function OnItemListCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else {
                alert("Success");
                Grid_Packing.Refresh();
            }
        }
        function PutValue(s, name, type1) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                if (clientType != null) {
                    clientType.SetText(type1);
                }
                clientId = null;
                clientName = null;
                clientAcCode = null;
                clientType = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PutValue(s, name, type1, acCode) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                if (clientType != null) {
                    clientType.SetText(type1);
                }
                if (acCode != null) {
                    clientAcCode.SetText(acCode);
                }
                clientId = null;
                clientName = null;
                clientAcCode = null;
                clientType = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function OnCallback(v) {
            btn_CustRate.SetEnabled(false);
            grid_det.Refresh();
        }

        function OnbookCallback(v) {
            alert(v);
            detailGrid.Refresh();
        }

        function OnPostCallback(v) {
            alert(v);
            detailGrid.Refresh();
        }
        function AddInvoiceDet_quote() {
            //popubCtr.SetHeaderText('Quotation');
            //popubCtr.SetContentUrl('QuoteList.aspx?id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.SetHeaderText('STD Rate');
            popubCtr.SetContentUrl('/SelectPage/Account/StdRateList.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AddInvoiceDet() {
            popubCtr.SetHeaderText('AR/AP Invoice');
            //popubCtr.SetContentUrl('BillList.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.SetContentUrl('/selectpage/account/BillList.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_det.Refresh();
        }
    </script>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsSchedule" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobSchedule"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsJobItemList" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobItemList" KeyMember="Id" />
             <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXSalesman" KeyMember="Code" />
            <table>
                <tr>
                    <td>Job No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txtSchNo" ClientInstanceName="txtSchNo" Width="150" runat="server"
                            Text="">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" Width="110" runat="server" Text="Retrieve" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                     window.location='ScheduleEdit.aspx?no='+txtSchNo.GetText()
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Issue" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="false" DataSourceID="dsSchedule"
                OnInitNewRow="grid_Issue_InitNewRow"
                OnInit="grid_Issue_Init" OnCustomDataCallback="grid_Issue_CustomDataCallback" OnCustomCallback="grid_Issue_CustomCallback"
                OnHtmlEditFormCreated="grid_Issue_HtmlEditFormCreated">
                <SettingsPager PageSize="50">
                </SettingsPager>
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <SettingsCustomizationWindow Enabled="True" />
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" BackColor="Control" ID="txt_Id" Text='<%# Eval("Id") %>' ClientInstanceName="txt_Id">
                            </dxe:ASPxTextBox>
                        </div>
                        <table style="width:960px;">
                                <tr style="display:none">
                                    <td>Job No</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" BackColor="Control" ID="txt_JobNo" Text='<%# Eval("JobNo") %>' ClientInstanceName="txt_DoNo">
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
                                                <dxe:ListEditItem Text="Schedule" Value="Schedule" />
                                                <dxe:ListEditItem Text="Billing" Value="Billing" />
                                                <dxe:ListEditItem Text="Close" Value="Close" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                     <td>JobType</td>
                                    <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="cmb_JobType" runat="server" Width="100%"  ClientInstanceName="cmb_JobType"  Text='<%# Eval("JobType")%>' OnCustomJSProperties="cmb_JobType_CustomJSProperties">
                                            <Items>
                                                                            <dxe:ListEditItem Text="Haulier" Value="Haulier" />
                            <dxe:ListEditItem Text="Transport" Value="Transport" />
                            <dxe:ListEditItem Text="Freight" Value="Freight" />
                            <dxe:ListEditItem Text="Warehouse" Value="Warehouse" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
									  <td>

                                    </td>
                                </tr>
                                 <tr>
                                    <td>Customer</td>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0">
                                            <td width="94">
                                                <dxe:ASPxButtonEdit ID="txt_CustomerId" ClientInstanceName="txt_CustomerId" runat="server"
                                                    Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CustomerId") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_CustomerId,txt_CustomerName,txt_Contact,txt_Tel,txt_Fax,txt_Email,txt_PostalCode,memo_Address,null,null,'C');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="280" ReadOnly="true" Text='<%# Eval("CustomerName") %>' BackColor="Control" ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </table>
                                    </td>
                                      <td>Origin Port</td>
                                      <td>
                                          <dxe:ASPxButtonEdit ID="btn_OriginPort" ClientInstanceName="btn_OriginPort" runat="server"
                                                    Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("OriginPort") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                           PopupPort(btn_OriginPort,null);
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                      </td>
                                     <td>Destination  Port</td>
                                     <td>
                                         <dxe:ASPxButtonEdit ID="btn_DestinationPort" ClientInstanceName="btn_DestinationPort" runat="server"
                                                    Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("DestinationPort") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                           PopupPort(btn_DestinationPort,null);
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                     </td>
                                </tr>
                                  <tr>
                                    <td rowspan="2">Address</td>
                                    <td rowspan="2" colspan="3">
                                        <dxe:ASPxMemo ID="memo_Address" Rows="4" Width="375" ClientInstanceName="memo_Address"
                                            runat="server" Text='<%# Eval("CustomerAdd") %>' >
                                        </dxe:ASPxMemo>
                                    </td>
                                      <td rowspan="2">Origin Address</td>
                                      <td rowspan="2">
                                          <dxe:ASPxMemo ID="memo_Address1" Rows="4" Width="120" runat="server" Text='<%# Eval("OriginAdd") %>'></dxe:ASPxMemo>
                                      </td>
                                      <td rowspan="2">Destination Address</td>
                                      <td rowspan="2">
                                          <dxe:ASPxMemo ID="memo_Address2" Rows="4" Width="120" runat="server" Text='<%# Eval("DestinationAdd") %>'></dxe:ASPxMemo>
                                      </td>
                                </tr
                                 <tr>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                     <td></td>
                                 </tr>
                                <tr>
                                    <td>Contact</td>
                                    <td  style="width:120px;">
                                        <dxe:ASPxTextBox ID="txt_Contact" runat="server" Width="120" ClientInstanceName="txt_Contact" Text='<%# Eval("Contact") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Tel
                                    </td>

                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Tel" runat="server" Width="150" ClientInstanceName="txt_Tel" Text='<%# Eval("Tel") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Origin City</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_OriginCity" ClientInstanceName="btn_OriginCity" runat="server"
                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("OriginCity") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(btn_OriginCity,null)
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>Destination City</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_DestCity" ClientInstanceName="btn_DestCity" runat="server"
                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("DestCity") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(btn_DestCity,null)
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Email" Width="120px" runat="server" ClientInstanceName="txt_Email" Text='<%# Eval("Email") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                     <td>Fax</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Fax" runat="server" Width="150" ClientInstanceName="txt_Fax" Text='<%# Eval("Fax") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Origin  Postal</td>
                                    <td>

                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_OriginPostal" Text='<%# Eval("OriginPostal") %>' ClientInstanceName="txt_OriginPostal">
                                        </dxe:ASPxTextBox>
                                    </td>
                                     <td>Destination Postal</td>
                                    <td>

                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_DestPostal" Text='<%# Eval("DestPostal") %>' ClientInstanceName="txt_DestPostal">
                                        </dxe:ASPxTextBox>
                                    </td>


                                </tr>

                                <tr>
                                      <td>PostalCode</td>
                                    <td>

                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_PostalCode" Text='<%# Eval("Postalcode") %>' ClientInstanceName="txt_PostalCode">
                                        </dxe:ASPxTextBox>
                                    </td>
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
                                    <td>WorkStatus</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_WorkStatus" runat="server" Width="120" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" OnCustomJSProperties="cmb_WorkStatus_CustomJSProperties" Value='<%# Eval("WorkStatus")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                <dxe:ListEditItem Text="Working" Value="Working" />
                                                <dxe:ListEditItem Text="Complete" Value="Complete" />
                                                <dxe:ListEditItem  Text="Cancel" Value="Cancel"/>
                                                <dxe:ListEditItem Text="Unsuccess" Value="Unsuccess" />
                                            </Items>
                                        </dxe:ASPxComboBox>
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
                                                  <%--style="display: <%# SafeValue.SafeString(Eval("JobType"),"")=="Storage"?"table-row": "none" %>"--%>
                                                  <td>Storage Days</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("StorageTotalDays") %>' DecimalPlaces="0" ID="spin_STotalDays" ClientInstanceName="spin_STotalDays" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td style="display:none">Via Warehouse</td>
                                                <td style="display:none">
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
                                                 <td>Charges</td>
                                                <td >
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("Charges") %>'
                                                        DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges" ClientInstanceName="spin_Charges" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                  <td >Free Storage Days</td>
                                                <td >
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("StorageFreeDays") %>' DecimalPlaces="0" ID="spin_StorageFreeDays" ClientInstanceName="spin_StorageFreeDays" Increment="0">
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
                                                 
                                            </tr>
                                            <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">

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
                                                        </td>
                                    </tr>
                                </table>
                        
                            <tr>
                                 <table>
                                                    <tr>
                                                        <td>
                                    <dxe:ASPxButton ID="btn_Save" runat="server" Text="Save" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton15" Width="150" runat="server" Text="Auto Add Items" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                      detailGrid.GetValuesOnCustomCallback('ITEM',OnItemListCallBack);
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddItem" Width="150" runat="server" Text="Add One Item" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                      Grid_Packing.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                            
                            <tr>
                                <td colspan="6">
                                    <dxwgv:ASPxGridView ID="Grid_Packing" ClientInstanceName="Grid_Packing" runat="server"
                                                    KeyFieldName="Id" DataSourceID="dsJobItemList" Width="960px" OnBeforePerformDataSelect="Grid_Packing_BeforePerformDataSelect" OnCustomCallback="Grid_Packing_CustomCallback"
                                                    OnRowUpdating="Grid_Packing_RowUpdating" OnRowInserting="Grid_Packing_RowInserting"
                                                    OnRowDeleting="Grid_Packing_RowDeleting" OnInitNewRow="Grid_Packing_InitNewRow"
                                                    OnInit="Grid_Packing_Init">
                                                    <SettingsEditing  Mode="Inline"/>
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { Grid_Packing.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0%>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){Grid_Packing.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                            <EditItemTemplate >
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false" 
                                                                                    ClientSideEvents-Click='<%# "function(s) { Grid_Packing.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { Grid_Packing.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                       <dxwgv:GridViewDataTextColumn Caption="Item Name" FieldName="ItemName" VisibleIndex="4" Width="180">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="ItemQty" VisibleIndex="4" Width="100">
                                                        <PropertiesSpinEdit DecimalPlaces="0" NumberType="Integer" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                        
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Volume" FieldName="ItemValue" VisibleIndex="5" Width="100">
                                                        <PropertiesSpinEdit DecimalPlaces="2" NumberType="Float" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="ItemMark" VisibleIndex="6" Width="200">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Note" FieldName="ItemNote" VisibleIndex="7" Width="200">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Create By" FieldName="CreateBy" VisibleIndex="7" Visible="false">
                                                        <EditItemTemplate>
                                                            <%# Eval("CreateBy") %>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Create Date" FieldName="CreateDateTime" VisibleIndex="8" Visible="false">
                                                        <DataItemTemplate>
                                                            <%# SafeValue.SafeDateTimeStr(Eval("CreateDateTime")) %>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <%# SafeValue.SafeDateTimeStr(Eval("CreateDateTime")) %>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Update By" FieldName="UpdateBy" VisibleIndex="8">
                                                            <EditItemTemplate>
                                                            <%# Eval("UpdateBy") %>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Update Date" FieldName="UpdateDateTime" VisibleIndex="8">
                                                        <DataItemTemplate>
                                                            <%# SafeValue.SafeDateTimeStr(Eval("UpdateDateTime")) %>
                                                        </DataItemTemplate>
                                                       <EditItemTemplate>
                                                            <%# SafeValue.SafeDateTimeStr(Eval("UpdateDateTime")) %>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="TYPE" FieldName="ItemType" VisibleIndex="8">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                    <Settings ShowGroupPanel="True" />
                                                    <Templates>
                                                        <EditForm>
                                                            <table style="width:660px;margin:0px ">
                                                                <tr>
                                                                    <td style="width:160px;">Item Name</td>
                                                                    <td>
                                                                        <dxe:ASPxLabel Width="160px" ID="txt_ItemName" runat="server" Text='<%# Bind("ItemName") %>'></dxe:ASPxLabel></td>
                                                                    <td>Qty</td>
                                                                    <td><dxe:ASPxSpinEdit runat="server" ID="spin_Qty" Value='<%# Bind("ItemQty") %>' NumberType="Integer" DecimalPlaces="0">
                                                                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                        </dxe:ASPxSpinEdit></td>
                                                                    <td>Value</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit runat="server" ID="spin_Value" Value='<%# Bind("ItemValue") %>' NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00">
                                                                        <SpinButtons ShowIncrementButtons="false"></SpinButtons>
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Remark</td>
                                                                    <td colspan="5">
                                                                        <dxe:ASPxMemo runat="server" Rows="2" ID="memo_Remark" Text='<%# Bind("ItemMark") %>' Width="100%"></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Note</td>
                                                                    <td colspan="5">
                                                                        <dxe:ASPxMemo runat="server" Rows="2" ID="memo_Note" Text='<%# Bind("ItemNote") %>' Width="100%"></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                             <div style="text-align: right; padding: 2px 2px 2px 2px;width:800px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                    <ClientSideEvents Click="function(s,e){Grid_Packing.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                runat="server">
                                                            </dxwgv:ASPxGridViewTemplateReplacement>
                                                        </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="800" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
