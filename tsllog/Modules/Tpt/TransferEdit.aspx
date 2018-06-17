<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferEdit.aspx.cs" Inherits="Modules_Tpt_TransferEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../script/StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="/Script/Tpt/page.js"></script>
    <script src="/PagesContTrucking/script/jquery.js"></script>
    <script src="/PagesContTrucking/script/firebase.js"></script>
    <script src="/PagesContTrucking/script/js_company.js"></script>
    <script src="/PagesContTrucking/script/js_firebase.js"></script>
    <style type="text/css">
        .show {
            display: block;
        }

        .hide {
            display: none;
        }
    </style>
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
            gridview: 'grid_Transport',
        }

        $(function () {
            loading.hide();
        })
        var ContainerTripIndex = 0;

        function AfterPopub(){
            popubCtrPic.Hide();
            popubCtrPic.SetContentUrl('about:blank');
            grid_wh.Refresh();
        }
    </script>
    <style type="text/css">
        .fee_showhide {
            display: none;
        }

        .add_carpark {
            float: right;
            margin-right: 10px;
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsJob" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJob" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCont" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet1" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsContTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <%--<wilson:DataSource ID="dsTripLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmTripLog" KeyMember="Id" FilterExpression="1=0" />--%>
        <wilson:DataSource ID="dsCharge" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobCharge" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsStock" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobStock" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsActivity" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobEventLog" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmAttachment" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Job_Cost" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXSalesman" KeyMember="Code" FilterExpression="" />
        <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />
        <wilson:DataSource ID="dsTripCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='tripcode'" />
        <wilson:DataSource ID="dsTerminal" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='location' and type1='TERMINAL'" />
        <wilson:DataSource ID="dsCarpark" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='carpark'" />
        <wilson:DataSource ID="dsLogEvent" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmJobEventLog" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsZone" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmParkingZone" KeyMember="id" FilterExpression="" />
        <wilson:DataSource ID="dsWh" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobHouse"
            KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
            KeyMember="Id" FilterExpression="CodeType='2'" />
        <wilson:DataSource ID="dsIncoTerms" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='IncoTerms'" />
        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobRate" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RateType" KeyMember="Id" />
        <div>
            <table>
                <tr>
                    <td>Job No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_JobNo" ClientInstanceName="txt_search_JobNo" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Retrieve" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='TransferEdit.aspx?no='+txt_search_JobNo.GetText();
                    }" />
                        </dxe:ASPxButton>

                    </td>
                    <td style="display: none">
                        <dxe:ASPxButton ID="btn_GoSearch" runat="server" Text="Go Search" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.location='TransferList.aspx';}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_job" runat="server" ClientInstanceName="grid_job" KeyFieldName="Id" DataSourceID="dsJob" Width="1000px" AutoGenerateColumns="False" OnInit="grid_job_Init" OnInitNewRow="grid_job_InitNewRow" OnCustomDataCallback="grid_job_CustomDataCallback" OnCustomCallback="grid_job_CustomCallback" OnHtmlEditFormCreated="grid_job_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                        </div>
                        <%--<div style="float: right; padding-right: 50px">

                        </div>--%>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="padding: 0px">
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="Large" Text="Type:"></dxe:ASPxLabel>
                                            </td>
                                            <td>

                                                <dxe:ASPxLabel ID="lbl_JobType" runat="server" Font-Size="Large" Text='<%# Eval("JobType") %>'></dxe:ASPxLabel>
                                                <div style="display: none">
                                                    <dxe:ASPxComboBox ID="cbb_JobType" ClientInstanceName="cbb_JobType" runat="server" Value='<%# Eval("JobType") %>' Width="80">
                                                        <Items>
                                                            <dxe:ListEditItem Text="GR" Value="GR" />
                                                            <dxe:ListEditItem Text="DO" Value="DO" />
                                                            <dxe:ListEditItem Text="TP" Value="TP" />
                                                            <dxe:ListEditItem Text="TR" Value="TR" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </div>
                                            </td>
                                            <td width="60%"></td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_JobClose" ClientInstanceName="btn_JobClose" runat="server" Text="Close Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("StatusCode"),"USE")!="CNL" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Close Job ?')) {
                                                        grid_job.GetValuesOnCustomCallback('Close',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_JobVoid" ClientInstanceName="btn_JobVoid" runat="server" Text="Void Job" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    if(confirm('Void Job?')) {
                                                        grid_job.GetValuesOnCustomCallback('Void',onCallBack);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                    <dxe:ASPxButton ID="ASPxButton16" runat="server" Text="Print Transfer Instruction" AutoPostBack="false" Width="100">
                                                        <ClientSideEvents Click="function(s,e){
                                                                        PrintTR();
                                                                        }" />
                                                    </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_JobSave" ClientInstanceName="btn_JobSave" runat="server" Text="Save" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    grid_job.PerformCallback('save');
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>

                                            <%--<td>
                                                <dxe:ASPxButton ID="ASPxButton9" ClientInstanceName="btn_JobAutoBilling" runat="server" Text="Create Inv" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' Width="100">
                                                    <ClientSideEvents Click="function(s,e) {
                                                    grid_job.GetValuesOnCustomCallback('CreateInv',onCallBack);
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div style="padding: 6px; background-color: #FFFFFF; border: 1px solid #A8A8A8; white-space: nowrap">
                            <table>
                                <tr>
                                    <td class="lbl">Job No</td>
                                    <td class="ctl2">
                                        <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" runat="server" ReadOnly="true" BackColor="Control" Text='<%# Eval("JobNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>

                                    <td class="ctl2">
                                        <dxe:ASPxDateEdit ID="txt_JobDate" runat="server" Value='<%# Eval("JobDate") %>' Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>
                                    <td class="lbl2">Status
                                    </td>
                                    <td class="ctl2">
                                        <dxe:ASPxComboBox ID="cmb_JobStatus" ClientInstanceName="cmb_JobStatus" OnCustomJSProperties="cmb_JobStatus_CustomJSProperties" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("JobStatus") %>' Width="100%">
                                            <Items>
                                                <dxe:ListEditItem Text="Quoted" Value="Quoted" />
                                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                                <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                <dxe:ListEditItem Text="Voided" Value="Voided" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-top: 6px; padding: 10px; background-color: #FFFFFF; border: 1px solid #A8A8A8; white-space: nowrap">
                            <table>
                                <tr>
                                    <td class="lbl">Client</td>
                                    <td class="ctl">
                                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Text='<%# Eval("ClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td class="lbl">Sub Client
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxButtonEdit ID="btn_SubClientId" ClientInstanceName="btn_SubClientId" runat="server" Text='<%# Eval("SubClientId") %>' Width="100%" AutoPostBack="False" ReadOnly="true">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParentParty(btn_SubClientId,null,btn_ClientId.GetText());
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td class="lbl">Contact</td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_ClientContact" runat="server" Text='<%# Eval("ClientContact") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl">Client Ref No
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_ClientRefNo" runat="server" Text='<%# Eval("ClientRefNo") %>' Width="100%"></dxe:ASPxTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl">Email
                                    </td>
                                    <td class="ctl">
                                        <dxe:ASPxTextBox ID="txt_notifiEmail" runat="server" Width="100%" Value='<%# Eval("EmailAddress") %>'></dxe:ASPxTextBox>
                                    </td>
                                    <td class="lbl">Warehouse</t>
                                                    <td class="ctl">
                                                        <dxe:ASPxButtonEdit ID="txt_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_WareHouseId" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="170" AutoPostBack="False">
                                                            <Buttons>
                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupWh(txt_WareHouseId,null);
                                                                        }" />
                                                        </dxe:ASPxButtonEdit>
                                                    </td>
                                </tr>
                            </table>
                        </div>
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="980px">
                            <TabPages>
                                <dxtc:TabPage Text="Transfer Line" Visible="true" Name="Cargo">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Add Transfer" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e){
                       PopupStockForTransfer();
                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="overflow-y: auto; width: 1000px">
                                                <dxwgv:ASPxGridView ID="grid_wh" ClientInstanceName="grid_wh" runat="server" DataSourceID="dsWh" OnInit="grid_wh_Init" OnInitNewRow="grid_wh_InitNewRow"
                                                    OnRowInserting="grid_wh_RowInserting" OnRowUpdating="grid_wh_RowUpdating" OnRowDeleting="grid_wh_RowDeleting" OnRowDeleted="grid_wh_RowDeleted"
                                                    OnBeforePerformDataSelect="grid_wh_BeforePerformDataSelect" OnCustomDataCallback="grid_wh_CustomDataCallback"
                                                    KeyFieldName="Id" Width="1300px" AutoGenerateColumns="False">
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="true" />
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px">
                                                            <EditButton Visible="false"></EditButton>
                                                            <DeleteButton Visible="true"></DeleteButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="0" Width="180" SortIndex="1">
                                                            <DataItemTemplate>
                                                                <%# Eval("CargoStatus") %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Bind("CargoStatus") %>' Width="90">
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Pending" Value="P" />
                                                                        <dxe:ListEditItem Text="Completed" Value="C" />
                                                                    </Items>
                                                                </dxe:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Type" FieldName="CargoType" ReadOnly="true" VisibleIndex="0" Visible="false">
                                                            
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Lot No/BL/Container" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                                                            <DataItemTemplate>
                                                                <table style="width: 260px;">
                                                                    <tr>
                                                                        <td class="lbl">Lot No</td>

                                                                        <td><%# Eval("BookingNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">BL/Bkg No</td>

                                                                        <td><%# Eval("HblNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Cont No</td>

                                                                        <td><%# Eval("ContNo") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Type</td>

                                                                        <td><%# Eval("OpsType") %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 260px">
                                                                    <tr>
                                                                        <td class="lbl">Lot No</td>

                                                                        <td class="ctl">
                                                                            <dxe:ASPxTextBox ID="txt_BookingNo" Width="120" runat="server" Text='<%# Bind("BookingNo") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl1">BL/Bkg No</td>

                                                                        <td class="ctl">
                                                                            <dxe:ASPxTextBox ID="txt_HblNo" runat="server" Width="120" Text='<%# Bind("HblNo") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Cont No</td>

                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Text='<%# Bind("ContNo") %>' AutoPostBack="False" Width="120">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_ContNo,null);
                                                                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Type</td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_JobType" runat="server" Value='<%# Bind("OpsType") %>' Width="120">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Delivery" Value="Delivery" />
                                                                                    <dxe:ListEditItem Text="Pickup" Value="Pickup" />
                                                                                    <dxe:ListEditItem Text="Receipt" Value="Receipt" />
                                                                                    <dxe:ListEditItem Text="Storage" Value="Storage" />
                                                                                    <dxe:ListEditItem Text="Tranship" Value="Tranship" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Booking Info" VisibleIndex="2" Width="160">
                                                            <DataItemTemplate>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>

                                                                        <td><%# Eval("Qty") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Unit</td>

                                                                        <td><%# Eval("UomCode") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>

                                                                        <td><%# Eval("Weight") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>

                                                                        <td><%# Eval("Volume") %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 120px">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td class="ctl">
                                                                            <dxe:ASPxSpinEdit runat="server" Width="100%"
                                                                                ID="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Unit</td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit runat="server" ID="txt_UomCode" Width="70" ClientInstanceName="txt_UomCode" Text='<%# Bind("UomCode") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_UomCode,2);
                                                                            }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>

                                                                        <td>
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Weight" Height="21px" Value='<%# Bind("Weight")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>

                                                                        <td>
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Volume" Height="21px" Value='<%# Bind("Volume")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Actual Info" VisibleIndex="2" Width="400">
                                                            <DataItemTemplate>
                                                                <table style="width: 280px;">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# Eval("QtyOrig") %></td>
                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                        <td class="lbl1">SKU Code</td>
                                                                        <td colspan="2"><%# Eval("SkuCode") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>
                                                                        <td colspan="2"><%# Eval("WeightOrig") %></td>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# Eval("PackQty") %></td>
                                                                        <td><%# Eval("PackUom") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>
                                                                        <td colspan="2"><%# Eval("VolumeOrig") %></td>
                                                                        <td class=""></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Remark</td>
                                                                        <td colspan="5">
                                                                            <%# Eval("Desc1") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td colspan="4">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>L</td>
                                                                                    <td><%# Eval("LengthPack") %></td>
                                                                                    <td>×</td>
                                                                                    <td>W</td>
                                                                                    <td><%# Eval("WidthPack") %></td>
                                                                                    <td>×</td>
                                                                                    <td>H</td>
                                                                                    <td><%# Eval("HeightPack") %></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 420px">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit runat="server" Width="60"
                                                                                ID="spin_Qty" Height="21px" Value='<%# Bind("QtyOrig")%>' Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit runat="server" ID="txt_PackTypeOrig" Width="70" ClientInstanceName="txt_PackTypeOrig" Text='<%# Bind("PackTypeOrig") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_PackTypeOrig,2);
                                                                            }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                        <td class="lbl1">SKU Code</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxButtonEdit ID="btn_SkuCode" ClientInstanceName="btn_SkuCode" runat="server" Text='<%# Bind("SkuCode") %>' AutoPostBack="False" Width="100%">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupSku(btn_SkuCode);
                                                                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Weight" Height="21px" Value='<%# Bind("WeightOrig")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td class="lbl">Qty</td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit runat="server" Width="60"
                                                                                ID="spin_PackQty" Height="100%" Value='<%# Bind("PackQty")%>' Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit runat="server" ID="txt_PackUom" Width="70" ClientInstanceName="txt_PackUom" Text='<%# Bind("PackUom") %>'>
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupUom(txt_PackUom,2);
                                                                            }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                ID="spin_Volume" Height="21px" Value='<%# Bind("VolumeOrig")%>' DecimalPlaces="3" Increment="0">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td class="lbl">Location</td>
                                                                        <td colspan="2">
                                                                            <dxe:ASPxButtonEdit ID="txt_Location" ClientInstanceName="txt_Location" runat="server" Text='<%# Bind("Location") %>' AutoPostBack="False" Width="100%">
                                                                                <Buttons>
                                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                </Buttons>
                                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupLoc(txt_Location);
                                                                        }" />
                                                                            </dxe:ASPxButtonEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Remark</td>
                                                                        <td colspan="5">
                                                                            <dxe:ASPxMemo ID="memo_Desc1" ClientInstanceName="memo_Desc1" Text='<%# Bind("Desc1") %>' Rows="2" runat="server" Width="100%"></dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none">
                                                                        <td colspan="6">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td>L</td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                            ID="spin_LengthPack" Height="21px" Value='<%# Bind("LengthPack")%>' DecimalPlaces="3" Increment="0">
                                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>×</td>
                                                                                    <td>W</td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                            ID="spin_WidthPack" Height="21px" Value='<%# Bind("WidthPack")%>' DecimalPlaces="3" Increment="0">
                                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                    <td>×</td>
                                                                                    <td>H</td>
                                                                                    <td>
                                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100%"
                                                                                            ID="ASPxSpinEdit1" Height="21px" Value='<%# Bind("HeightPack")%>' DecimalPlaces="3" Increment="0">
                                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                                        </dxe:ASPxSpinEdit>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Balance Info" VisibleIndex="2" Width="200">
                                                            <DataItemTemplate>
                                                                <table style="width: 160px">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# BalanceQty(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>
                                                                        <td colspan="2"><%# BalanceWeight(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>
                                                                        <td colspan="2"><%# BalanceVolume(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl1">SKU Qty</td>
                                                                        <td colspan="2"><%# BalanceSkuQty(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 160px; line-height: 20px;">
                                                                    <tr>
                                                                        <td class="lbl">Qty</td>
                                                                        <td><%# BalanceQty(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                        <td><%# Eval("PackTypeOrig") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Weight</td>

                                                                        <td colspan="2"><%# BalanceWeight(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Volume</td>

                                                                        <td colspan="2"><%# BalanceVolume(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl1">SKU Qty</td>

                                                                        <td colspan="2"><%# BalanceSkuQty(Eval("ClientId"),Eval("SkuCode"),Eval("BookingNo"),Eval("RefNo"),Eval("Location")) %></td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                         <dxwgv:GridViewDataTextColumn Caption="From Loction"  VisibleIndex="2" Width="120" Visible="true">
                                                            <DataItemTemplate>
                                                                <%# from_loc(SafeValue.SafeInt(Eval("LineId"),0)) %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                         <dxwgv:GridViewDataTextColumn Caption="To Location" VisibleIndex="2" Width="120" Visible="true">
                                                            <DataItemTemplate>
                                                               <%# Eval("Location") %>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Marking1" ClientInstanceName="memo_Marking1" Text='<%# Bind("Marking1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Marking2" ClientInstanceName="memo_Marking2" Text='<%# Bind("Marking2") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Bind("Remark1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="3" Width="200">
                                                            <DataItemTemplate>
                                                                <table style="width: 200px">
                                                                    <tr>
                                                                        <td class="lbl">Landing</td>

                                                                        <td>
                                                                            <%# Eval("LandStatus") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DG Cargo</td>

                                                                        <td>
                                                                            <%# Eval("DgClass") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Damage</td>
                                                                        <td>
                                                                            <%# Eval("DamagedStatus") %>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DMG Remark</td>
                                                                        <td>
                                                                            <%# Eval("Remark2") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table style="width: 200px">
                                                                    <tr>
                                                                        <td class="lbl">Landing</td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_LandStatus" runat="server" Value='<%# Bind("LandStatus") %>' Width="100">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                                                                    <dxe:ListEditItem Text="Shortland" Value="Shortland" />
                                                                                    <dxe:ListEditItem Text="Overland" Value="Overland" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DG Cargo</td>

                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_DgClass" runat="server" Value='<%# Bind("DgClass") %>' Width="100">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                                                                    <dxe:ListEditItem Text="Class 2" Value="Class 2" />
                                                                                    <dxe:ListEditItem Text="Class 3" Value="Class 3" />
                                                                                    <dxe:ListEditItem Text="Other Class" Value="Other Class" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">Damage</td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cbb_Damage" runat="server" Value='<%# Bind("DamagedStatus") %>' Width="100">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                                                                    <dxe:ListEditItem Text="Damaged" Value="Damaged" />

                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl">DMG Remark</td>
                                                                        <td>
                                                                            <dxe:ASPxMemo ID="meno_Remark2" Rows="2" runat="server" Text='<%# Bind("Remark2") %>' Width="100%">
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
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
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </EditForm>
                </Templates>

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
            <dxpc:ASPxPopupControl ID="popubCtrPic" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrPic"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                    if(grid!=null)
	                     grid.Refresh();
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1050" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
