<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="TptJobEdit.aspx.cs" Inherits="PagesTpt_Job_WhJobEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transport Job</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../../PagesContTrucking/script/StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/PagesContTrucking/script/jquery.js"></script>
    <script type="text/javascript" src="/PagesContTrucking/script/firebase.js"></script>
    <script type="text/javascript" src="/PagesContTrucking/script/js_company.js"></script>
    <script type="text/javascript" src="/PagesContTrucking/script/js_firebase.js"></script>
    <script type="text/javascript">
        var isUpload = false;
    </script>
    <script type="text/javascript">
        //close job
        function OnCloseCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                detailGrid.Refresh();
            }
            else if (v == "Billing")
                alert("Have Billing, Can't void!");
            else if (v == "Fail")
                alert("Action Fail,please try again!");
        }
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('../../Upload.aspx?Type=Tpt&Sn=' + txt_JobNo.GetText());
            popubCtr.Show();
        }
        function AfterUploadPhoto() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }

        function firebase_callback(v) {
            //grid_Trip.CancelEdit();
            //alert(v);
            //console.log(v);
            var ar = v.split(',');
            var detail = {
                controller: ar[0],
                driver: ar[1],
                no: ar[2]
            }
            SV_Firebase.publish_system_msg_send('refresh', 'localjoblist', JSON.stringify(detail));
            detailGrid.Refresh();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.TptJob" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.TptAttachment" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.TptCosting" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />

            <wilson:DataSource ID="dsTripCode" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='tripcode'" />
            <wilson:DataSource ID="dsTripLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmTripLog" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsStock" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobStock" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsActivity" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobEventLog" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_TptNo" ClientInstanceName="txt_TptNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="display: none">
                        <dxe:ASPxTextBox ID="txt_type" ClientInstanceName="txt_type" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                           window.location='TptJobEdit.aspx?no='+txt_TptNo.GetText() + '&typ=' + txt_type.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='TptJobEdit.aspx?no=0&typ=' + txt_type.GetText();
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='TptJobList.aspx?typ=' + txt_type.GetText();
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="950px" AutoGenerateColumns="False" DataSourceID="dsTransport"
                OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnCustomCallback="grid_Transport_CustomCallback"
                OnCustomDataCallback="grid_Transport_CustomDataCallback" OnHtmlEditFormCreated="grid_Transport_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                                <tr>
                                    <td width="50%" style="">
                                        <table style="padding: 2px 2px 2px 2px; display: normal">
                                            <tr>
                                                <td>Print Document:
                                                </td>
                                                <td>
                                                    <a href='/ReportFreightSea/printview.aspx?document=TPT_DN&master=<%# Eval("JobNo")%>'
                                                        target="_blank">DN</a>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <a href='/ReportFreightSea/printview.aspx?document=TPT_JobOrder&master=<%# Eval("JobNo")%>'
                                                        target="_blank">Job Order</a>
                                                </td>
                                                <td>
                                                    <a href='/ReportFreightSea/printview.aspx?document=TPT_PL&master=<%# Eval("JobNo")%>'
                                                        target="_blank">P&L</a>
                                                </td>
                                                <td>
                                                    <a href='/ReportFreightSea/printview.aspx?document=TPT_Lable&master=<%# Eval("JobNo")%>'
                                                        target="_blank">Lable</a>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="30%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton21" Width="110" runat="server" Text="Save" AutoPostBack="false"
                                            Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e) {
                                    detailGrid.GetValuesOnCustomCallback('Save',firebase_callback);
                                    }" />
                                        </dxe:ASPxButton>

                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CloseJob" Width="100" runat="server" Text="Close Job" AutoPostBack="False"
                                            Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>' UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
                                                                detailGrid.GetValuesOnCustomCallback('CloseJob',OnCloseCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_VoidMaster" ClientInstanceName="btn_VoidMaster" runat="server" Width="100" Text="Void" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CLS" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_VoidMaster.GetText()+' Job?'))
                                                                    {
                                                                        detailGrid.GetValuesOnCustomCallback('VoidJob',OnCloseCallBack);                 
                                                                    }
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" Height="450px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Text="Job Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Job" runat="server">
                                                <div style="display: none">
                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                        BackColor="Control" Text='<%# Eval("Id") %>'>
                                                    </dxe:ASPxTextBox>
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td>No
                                                        </td>
                                                        <td width="150">
                                                            <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" BackColor="Control"
                                                                ReadOnly="true" Width="100%" runat="server" Text='<%# Eval("JobNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="80">Date
                                                        </td>
                                                        <td width="150">
                                                            <dxe:ASPxDateEdit ID="date_JobDate" runat="server" Width="100%" Value='<%# Eval("JobDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Job Type</td>
                                                        <td width="150">
                                                            <dxe:ASPxComboBox ID="cmb_JobType" Width="100%" runat="server" Text='<%# Eval("JobType") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="IMP" Value="IMP" />
                                                                    <dxe:ListEditItem Text="EXP" Value="EXP" />
                                                                    <dxe:ListEditItem Text="TSP" Value="TSP" />
                                                                    <dxe:ListEditItem Text="LOC" Value="LOC" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Customer
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="btn_Cust" ClientInstanceName="btn_Cust" runat="server" Text='<%# Eval("Cust") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupShipper(btn_Cust,txt_Cust,txt_CustPic,null,null,txt_CustEmail,null);
                        }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td colspan="4">
                                                            <dxe:ASPxTextBox Width="100%" ReadOnly="true" BackColor="Control" ID="txt_Cust" ClientInstanceName="txt_Cust"
                                                                runat="server" Text='<%# Eval("CustName") %>' />
                                                        </td>
                                                        <td>Cust. Job No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_BkgRef" Width="100%" runat="server" Text='<%# Eval("BkgRef") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>PIC
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_CustPic" ClientInstanceName="txt_CustPic" Width="100%" runat="server" Text='<%# Eval("CustPic") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Email
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_CustEmail" ClientInstanceName="txt_CustEmail" Width="100%" runat="server" Text='<%# Eval("CustEmail") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Doc No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_CustDocNo" Width="100%" runat="server" Text='<%# Eval("CustDocNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Doc Type
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="txt_CustDocType" Width="100%" DropDownStyle="DropDown" runat="server" Text='<%# Eval("CustDocType") %>'>
                                                            </dxe:ASPxComboBox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>Vessel
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="txt_Ves" Width="100%" runat="server" Text='<%# Eval("Vessel") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Voyage
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Voy" Width="100%" runat="server" Text='<%# Eval("Voyage") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>HBL No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_BlRef" Width="100%" runat="server" Text='<%# Eval("BlRef") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pol
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Text='<%# Eval("Pol") %>' Width="100%" MaxLength="5">
                                                                <Buttons>
                                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pol,null);
                        }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Pod
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Text='<%# Eval("Pod") %>' Width="100%" MaxLength="5">
                                                                <Buttons>
                                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_Pod,null);
                        }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Eta
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Eta" runat="server" Width="100%" Value='<%# Eval("Eta") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Etd
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Etd" runat="server" Width="100%" Value='<%# Eval("Etd") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <hr />
                                                            Booking Info
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Date
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_BkgDate" runat="server" Width="100%" Value='<%# Eval("BkgDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Time
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_BkgTime" Width="100%" runat="server" Text='<%# Eval("BkgTime") %>'>
                                                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="txt_JobRmk" Width="100%" runat="server" Text='<%# Eval("JobRmk") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Weight
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_BkgWt" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                Value='<%# Eval("BkgWt") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Volume
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_BkgM3" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                Value='<%# Eval("BkgM3") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Qty
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_BkgQty" runat="server" Width="100%" Increment="0" Number="0"
                                                                NumberType="Integer" Value='<%# Eval("BkgQty") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>PackType
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_BkgPackType" ClientInstanceName="txt_BkgPackType" runat="server" Text='<%# Eval("BkgPkgType") %>' Width="100%">
                                                                <Buttons>
                                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupUom(txt_BkgPackType,2);
                        }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Marking
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="txt_cargoMkg" Rows="4" Width="100%" runat="server" Text='<%# Eval("CargoMkg") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Description
                                                        </td>
                                                        <td colspan="6">
                                                            <dxe:ASPxMemo ID="txt_cargoDes" Rows="4" Width="99%" runat="server" Text='<%# Eval("CargoDesc") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a href="#" onclick="PopupPartyAdr(null,txt_PickupFrm1);">Pick From</a>
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="txt_PickupFrm1" ClientInstanceName="txt_PickupFrm1" Rows="4" Width="100%" runat="server" Text='<%# Eval("PickFrm1") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>
                                                            <a href="#" onclick="PopupPartyAdr(null,txt_DeliveryTo1);">Delivery To</a>
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="txt_DeliveryTo1" ClientInstanceName="txt_DeliveryTo1" Rows="4" Width="100%" runat="server" Text='<%# Eval("DeliveryTo1") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <hr />
                                                            Transport Info</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Date
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_TptDate" runat="server" Width="100%" Value='<%# Eval("TptDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Time
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_TptTime" Width="100%" runat="server" Text='<%# Eval("TptTime") %>'>
                                                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>TPT&nbsp;Status</td>
                                                        <td width="150">
                                                            <dxe:ASPxComboBox ID="cmb_JobStatus" Width="100%" runat="server" Text='<%# Eval("JobProgress") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="" Value="" />
                                                                    <dxe:ListEditItem Text="Assigned" Value="Assigned" />
                                                                    <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                                                    <dxe:ListEditItem Text="Picked" Value="Picked" />
                                                                    <dxe:ListEditItem Text="Delivered" Value="Delivered" />
                                                                    <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                                    <dxe:ListEditItem Text="Canceled" Value="Canceled" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>TPT Type</td>
                                                        <td width="150">
                                                            <dxe:ASPxComboBox ID="cmb_TptType" Width="100%" runat="server" Text='<%# Eval("TptType") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="PUP" />
                                                                    <dxe:ListEditItem Text="DEL" />
                                                                    <dxe:ListEditItem Text="LOC" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Trip Code</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Eval("TripCode") %>' Width="100%" DropDownStyle="DropDown" DataSourceID="dsTripCode" ValueField="Code" TextField="Code">
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Driver</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Eval("Driver") %>' AutoPostBack="False" Width="100%">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,btn_vehicle);
                                                                        }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Vehicle</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="btn_vehicle" ClientInstanceName="btn_vehicle" runat="server" Text='<%# Eval("VehicleNo") %>' AutoPostBack="False" Width="100%">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_VehicleList(btn_vehicle,null);
                                                                        }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Weight
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_Wt" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                Value='<%# Eval("Wt") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Volume
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_M3" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                Value='<%# Eval("M3") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Qty
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_Qty" runat="server" Width="100%" Increment="0" Number="0"
                                                                NumberType="Integer" Value='<%# Eval("Qty") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>PackType
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_PackType" ClientInstanceName="txt_PackType" runat="server" Text='<%# Eval("PkgType") %>' Width="100%">
                                                                <Buttons>
                                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                            PopupUom(txt_PackType,2);
                        }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td colspan="8">
                                                            <hr />
                                                            Fee</td>
                                                    </tr>

                                                    <tr>
                                                        <td>TPT
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_FeeTpt" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                Value='<%# Eval("FeeTpt") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Labour
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_FeeLabour" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                Value='<%# Eval("FeeLabour") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>OT
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_FeeOt" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                Value='<%# Eval("FeeOt") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Admin
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_FeeAdmin" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                Value='<%# Eval("FeeAdmin") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Reimberse
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_FeeReimberse" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                Value='<%# Eval("FeeReimberse") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>MISC
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_FeeMisc" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                Value='<%# Eval("FeeMisc") %>'>
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="txt_FeeRemark" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                Value='<%# Eval("FeeRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <hr>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 80px;">Creation</td>
                                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                    <td style="width: 100px;">Last Updated</td>
                                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                    <td style="width: 80px;">Job Status</td>
                                                                    <td style="width: 160px">
                                                                        <dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text="" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>

                                    <dxtc:TabPage Text="TripLog">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <div>
                                                    <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Add Trip Log" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e){
                                grid_TripLog.AddNewRow();
                                }" />
                                                    </dxe:ASPxButton>
                                                    <dxwgv:ASPxGridView ID="grid_TripLog" ClientInstanceName="grid_TripLog" runat="server" Width="850" DataSourceID="dsTripLog" KeyFieldName="Id" AutoGenerateColumns="false" OnInit="grid_TripLog_Init" OnBeforePerformDataSelect="grid_TripLog_BeforePerformDataSelect" OnInitNewRow="grid_TripLog_InitNewRow" OnRowInserting="grid_TripLog_RowInserting" OnRowUpdating="grid_TripLog_RowUpdating" OnRowDeleting="grid_TripLog_RowDeleting">
                                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsBehavior ConfirmDelete="true" />
                                                        <Columns>
                                                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                                                <EditButton Visible="True" />
                                                                <DeleteButton Visible="True">
                                                                </DeleteButton>
                                                            </dxwgv:GridViewCommandColumn>

                                                            <dxwgv:GridViewDataColumn FieldName="Driver" Caption="Driver" Width="130">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxButtonEdit ID="btn_TripLog_DriverCode" ClientInstanceName="btn_TripLog_DriverCode" runat="server" Text='<%# Bind("Driver") %>' AutoPostBack="False" Width="100%">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_TripLog_DriverCode,null,null);
                                                                        }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataComboBoxColumn FieldName="Status" Caption="Status" Width="100">
                                                                <PropertiesComboBox>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Assigned" Value="Assigned" />
                                                                        <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                                                        <dxe:ListEditItem Text="Picked" Value="Picked" />
                                                                        <dxe:ListEditItem Text="Delivered" Value="Delivered" />
                                                                        <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                                        <dxe:ListEditItem Text="Canceled" Value="Canceled" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                            </dxwgv:GridViewDataComboBoxColumn>
                                                            <dxwgv:GridViewDataDateColumn FieldName="LogDate" Caption="Date" PropertiesDateEdit-EditFormatString="dd/MM/yyyy" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Width="120"></dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataTextColumn FieldName="LogTime" Caption="Time" Width="80">
                                                                <PropertiesTextEdit>
                                                                    <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                    <ValidationSettings ErrorDisplayMode="None" />
                                                                </PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn FieldName="Remark" Caption="Remark" MinWidth="150"></dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dxwgv:ASPxGridView>

                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Billing" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Bill" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import, "TPT", txt_JobNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import, "TPT", txt_JobNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import, "TPT", txt_JobNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Invoice_Import" ClientInstanceName="Grid_Invoice_Import"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsInvoice" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Invoice_Import_DataSelect">
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowInvoice(Grid_Invoice_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintInvoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1" Width="30">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="4" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="5" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="6" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import, "TPT", txt_JobNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import, "TPT", txt_JobNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Payable_Import" ClientInstanceName="Grid_Payable_Import"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsVoucher" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Payable_Import_DataSelect">
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowPayable(Grid_Payable_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintPayable("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1" Width="30">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="4" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="5" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="6" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                                <div style="display: none">
                                                    <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 &&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s,e) {
                                                        grid_Cost.AddNewRow();
                                                        }" />
                                                    </dxe:ASPxButton>
                                                    <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server"
                                                        DataSourceID="dsCosting" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect"
                                                        OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
                                                        OnRowUpdating="grid_Cost_RowUpdating" OnRowInserted="grid_Cost_RowInserted" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated" OnRowDeleting="grid_Cost_RowDeleting">
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <SettingsEditing Mode="EditFormAndDisplayRow" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" Width="70" VisibleIndex="0">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Cost.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Cost.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgCodeDes" VisibleIndex="2">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Cost Qty" FieldName="CostQty" VisibleIndex="3" Width="50">
                                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Cost Price" FieldName="CostPrice" VisibleIndex="4" Width="50">
                                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Cost" FieldName="CostLocAmt" VisibleIndex="5" Width="50">
                                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Sale Qty" FieldName="SaleQty" VisibleIndex="13" Width="50">
                                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.000}"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Sale Price" FieldName="SalePrice" VisibleIndex="14" Width="50">
                                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Sale" FieldName="SaleLocAmt" VisibleIndex="15" Width="50">
                                                                <PropertiesSpinEdit DisplayFormatString="{0:#,##0.00}"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn FieldName="JobNo" VisibleIndex="10" Visible="true" Width="120">
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
                                                                            <dxe:ASPxTextBox Width="200" ID="txt_CostDes" ClientInstanceName="txt_CostDes" BackColor="Control"
                                                                                ReadOnly="true" runat="server" Text='<%# Bind("ChgCodeDes") %>'>
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
                                                                    PopupParty(txt_CostVendorId,txt_CostVendorName);
                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxTextBox runat="server" BackColor="Control" Width="200" ID="txt_CostVendorName"
                                                                                ClientInstanceName="txt_CostVendorName" ReadOnly="true" Text=''>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Remark
                                                                        </td>
                                                                        <td colspan="7">
                                                                            <dxe:ASPxMemo ID="memo_Remark" runat="server" Width="100%" Text='<%# Bind("Remark") %>' Rows="3"></dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="8">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td>Qty
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
                                                                                    <td>Sale
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostSaleQty"
                                                                                            ID="spin_CostSaleQty" Text='<%# Bind("SaleQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSalePrice"
                                                                                            runat="server" Width="100" ID="spin_CostRevPrice" Value='<%# Bind("SalePrice")%>' Increment="0" DecimalPlaces="2">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxButtonEdit ID="cmb_CostSaleCurrency" ClientInstanceName="cmb_CostSaleCurrency" runat="server" Width="80" MaxLength="3" Text='<%# Bind("SaleCurrency") %>'>
                                                                                            <Buttons>
                                                                                                <dxe:EditButton Text=".."></dxe:EditButton>
                                                                                            </Buttons>
                                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostSaleCurrency,spin_CostSaleExRate);
                        }" />
                                                                                        </dxe:ASPxButtonEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                                                            DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostSaleExRate" ClientInstanceName="spin_CostSaleExRate" Text='<%# Bind("SaleExRate")%>' Increment="0">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSaleAmt"
                                                                                            BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostSaleAmt"
                                                                                            Value='<%# Eval("SaleLocAmt")%>'>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Cost
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                                                            ID="spin_CostCostQty" Text='<%# Bind("CostQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                                                            runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("CostPrice")%>' Increment="0" DecimalPlaces="2">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" MaxLength="3" runat="server" Width="80" Text='<%# Bind("CostCurrency") %>'>
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
                                                                                            DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("CostExRate")%>' Increment="0">
                                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                                                            BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                                                            Value='<%# Eval("CostLocAmt")%>'>
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                    <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                                        <ClientSideEvents Click="function(s,e){grid_Cost.UpdateEdit();}" />
                                                                    </dxe:ASPxHyperLink>
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                </div>
                                                            </EditForm>
                                                        </Templates>
                                                    </dxwgv:ASPxGridView>
                                                </div>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>

                                    <dxtc:TabPage Text="Stock" Name="Stock" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddStock" runat="server" Text="Add Stock" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s,e){
                                gv_stock.AddNewRow();
                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="gv_stock" ClientInstanceName="gv_stock" runat="server" AutoGenerateColumns="false" Width="850" DataSourceID="dsStock" KeyFieldName="Id" OnInit="gv_stock_Init" OnBeforePerformDataSelect="gv_stock_BeforePerformDataSelect"
                                                    OnInitNewRow="gv_stock_InitNewRow" OnRowDeleting="gv_stock_RowDeleting" OnRowInserting="gv_stock_RowInserting" OnRowUpdating="gv_stock_RowUpdating">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn VisibleIndex="0">
                                                            <EditButton Visible="True" />
                                                            <DeleteButton Visible="True">
                                                            </DeleteButton>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="StockStatus" Caption="Status"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Volume" Caption="Volume"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="StockQty" Caption="Stock Qty"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="StockUnit" Caption="Stock Unit"></dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>JobNo:</td>
                                                                    <td><%# Eval("JobNo") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Status:</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" ID="cbb_stock_status" Value='<%# Bind("StockStatus") %>' AutoPostBack="false" Width="100%">
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="IN" Text="IN" />
                                                                                <dxe:ListEditItem Value="OUT" Text="OUT" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Weight:</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100%"
                                                                            ID="sp_stock_weight" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Volume:</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100%"
                                                                            ID="sp_stock_volume" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="table_tb_top">Description:</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_stock_des" runat="server" Width="100%" Value='<%# Bind("StockDescription") %>'></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="table_tb_top">Marking:</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="txt_stock_marking" runat="server" Width="100%" Value='<%# Bind("StockMarking") %>'></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th colspan="6" class="table_th">Stock:</th>
                                                                </tr>
                                                                <tr>
                                                                    <td>Qty:</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100%"
                                                                            ID="sp_stock_qty" Height="21px" Value='<%# Bind("StockQty")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Unit:</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_stock_unit" runat="server" Width="100%" Value='<%# Bind("StockUnit") %>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th colspan="6" class="table_th">Packing:</th>
                                                                </tr>
                                                                <tr>
                                                                    <td>Qty:</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100%"
                                                                            ID="sp_stock_pk_qty" Height="21px" Value='<%# Bind("PackingQty")%>' DecimalPlaces="2" Increment="0">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Unit:</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_stock_pk_unit" runat="server" Width="100%" Value='<%# Bind("PackingUnit") %>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Dimention:</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_stock_pk_dimention" runat="server" Width="100%" Value='<%# Bind("PackingDimention") %>'></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5"></td>
                                                                    <td class="table_td_gv_editform_command">
                                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="Update" ReplacementType="EditFormUpdateButton"
                                                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="Cancel" ReplacementType="EditFormCancelButton"
                                                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>

                                    <dxtc:TabPage Text="Activity" Name="Activity" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <dxwgv:ASPxGridView ID="gv_activity" runat="server" ClientInstanceName="gv_activity" AutoGenerateColumns="false" DataSourceID="dsActivity" KeyFieldName="Id" OnBeforePerformDataSelect="gv_activity_BeforePerformDataSelect" Width="850">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" Caption="DateTime" PropertiesDateEdit-DisplayFormatString="yyyy/MM/dd hh:mm" SortOrder="Descending" Width="150"></dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Controller" Caption="Controller" Width="100"></dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Note"></dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>


                                    <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl8" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton12" Width="150" runat="server" Text="Upload Attachments"
                                                                Enabled='<%#  SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s,e) {
                                             isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                                UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                                                    KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                    <Settings />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                    ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="height: 100px;">
                                                                                <a href='<%# Eval("Path")%>' target="_blank">
                                                                                    <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                    </dxe:ASPxImage>
                                                                                </a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>Remark
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="800" Text='<%# Bind("FileNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'>
                                                                    <ClientSideEvents Click="function(s,e){grd_Photo.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>
                                <TabStyle BackColor="#F0F0F0">
                                </TabStyle>
                                <ContentStyle BackColor="#F0F0F0">
                                </ContentStyle>
                            </dxtc:ASPxPageControl>
                        </div>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload)
	    grd_Photo.Refresh();
}" />
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
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
