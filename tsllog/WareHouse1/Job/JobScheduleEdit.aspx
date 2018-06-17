<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="JobScheduleEdit.aspx.cs" Inherits="WareHouse_Job_JobScheduleEdit" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v13.2, Version=13.2.8.0, Culture=neutral, PublicKeyToken=B88D1754D700E49A" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dxe" %>

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
        function PopupUploadPhoto(type) {
            if (type == "IMG") {
                popubCtr.SetHeaderText('Upload Photo');
            } else {
                popubCtr.SetHeaderText('Upload Attachment');
            }
            popubCtr.SetContentUrl('../Upload.aspx?Type=JO&Sn=' + txt_DoNo.GetText() + '&AttType=' + type);
            popubCtr.Show();
        }
        function PopupUploadItemPhoto(type, item, itemName) {
            if (type == "IMG") {
                popubCtr.SetHeaderText('Upload Photo');
            } else {
                popubCtr.SetHeaderText('Upload Attachment');
            }
            popubCtr.SetContentUrl('../UploadItem.aspx?Type=JO&Sn=' + txt_DoNo.GetText() + '&AttType=' + type + '&item=' + item + '&itemName=' + itemName);
            popubCtr.Show();
        }
        function PopupPerson(id, name, type) {
            clientId = id;
            clientName = name;
            popubCtr.SetHeaderText('Crews ');

            popubCtr.SetContentUrl('/PagesHr/SelectPage/PersonList.aspx?Type=' + type);
            popubCtr.Show();
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
                window.location = 'JobEdit.aspx?no=' + v;
                //txt_SchRefNo.SetText(v);
                //txt_DoNo.SetText(v);
            }
        }
        function SetPCVisible(doShow) {
            if (doShow) {

                ASPxPopupClientControl.Show();
            }
            else {
                ASPxPopupClientControl.Hide();
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
        function OnChangesCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else {
                alert(v);
                detailGrid.Refresh();
            }
        }
        function OnCostingCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else {
                alert("Success");
                grid_Material.Refresh();
            }
        }
        function OnDoInCallBack(v) {
            if (v == "Fail")
                alert("Create DO Job out Fail");
            else if (v == "No Balance Qty or No Lot No!")
                alert(v);
            else if (v != null)
                alert("Success!");
            grid_DoIn.Refresh();
            parent.navTab.openTab(v, "/Warehouse/Job/DoInEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
        }
        function OnDoOutCallBack(v) {
            if (v == "Fail")
                alert("Create DO Job out Fail");
            else if (v == "No Balance Qty or No Lot No!")
                alert(v);
            else if (v != null)
                alert("Success!");
            grid_DoIn.Refresh();
            parent.navTab.openTab(v, "/Warehouse/Job/DoOutEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
        }
        function MultipleAdd() {
            popubCtr.SetHeaderText('Product List');
            popubCtr.SetContentUrl('../selectpage/SelectPoductFromStock.aspx?Type=SO&Sn=' + txt_DoNo.GetText() + '&Wh=' + txt_WareHouseId.GetText() + "&partyId=" + txt_ConsigneeCode.GetText());
            popubCtr.Show();
        }
        function MultiplePickCrews() {
            popubCtr1.SetHeaderText('Multiple Crews');
            popubCtr1.SetContentUrl('../SelectPage/MultipleCrews.aspx?Sn=' + txt_DoNo.GetText());
            popubCtr1.Show();
        }
        function CreateDeliveryOrder() {
            popubCtr.SetHeaderText('Create Delivery Order');
            popubCtr.SetContentUrl('/warehouse/SelectPage/AddDoOut.aspx?Sn=' + txt_DoNo.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_DoIn.Refresh();
        }
        function AfterPopubMultiInv1() {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
            grid_Crews.Refresh();
        }
        function AfterUploadPhoto() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grd_Attach.Refresh();
            grd_Photo.Refresh();
        }
        function AfterUploadItemPhoto() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_Inventory.Refresh();
        }
        function PrintDo(doNo, doType) {
            if (doType == "SO")
                window.open('/ReportWarehouse/PrintView.aspx?document=wh_So&master=' + doNo + "&house=0");
            else if (doType == "Inv")
                window.open('/ReportWarehouse/PrintView.aspx?document=wh_Inv&master=' + doNo + "&house=0");
            else if (doType == "DoOut")
                window.open('/ReportWarehouse/PrintView.aspx?document=wh_SoDoOut&master=' + doNo + "&house=0");
        }

        function AddInvoice1(gridId, JobType, refNo, jobNo, docType) {
            grid = gridId;
            popubCtr1.SetHeaderText('Invoice');
            popubCtr1.SetContentUrl('../Account/ArInvoice.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo + '&DocType=' + docType);
            popubCtr1.Show();
        }
        function PickBill() {
            var sn = txt_DoNo.GetText();
            var jt = cmb_JobType.GetText();
            var cc = txt_CustomerName.GetText();
            var ac = "";//txt_AgentCode.GetText();
            ac = btn_OriginPort.GetText();
            popubCtr1.SetHeaderText('Select Charges');
            popubCtr1.SetContentUrl('/SelectPage/PickRate.aspx?g=BILL&o=' + sn + '&t=' + jt + '&c=' + cc + '&a=' + ac);
            popubCtr1.Show();
        }

        function PickCost() {
            var sn = txt_DoNo.GetText();
            var jt = cmb_JobType.GetText();
            var cc = txt_CustomerName.GetText();
            var ac = "";
            ac = btn_OriginPort.GetText();
            popubCtr1.SetHeaderText('Select Cost');
            popubCtr1.SetContentUrl('/SelectPage/PickRate.aspx?g=COST&o=' + sn + '&t=' + jt + '&c=' + cc + '&a=' + ac);
            popubCtr1.Show();
        }

        function OnRateCallBack(v) {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
            grid_Cost.GetValuesOnCustomCallback('Refresh', OnRateLocalCallback);
        }

        function OnRateLocalCallback() {
            grid_Cost.Refresh();
        }
        function OnCostEditClick(id) {
            popubCtr1.SetHeaderText("Costing Edit");
            popubCtr1.SetContentUrl('CostingEdit.aspx?id=' + id);
            popubCtr1.Show();
        }
        function OnCostCopyClick(id) {
            grid_Cost.GetValuesOnCustomCallback('Copy' + id, OnRateLocalCallback);
        }
        function OnCostReviseClick(id) {
            grid_Cost.GetValuesOnCustomCallback('Revise' + id, OnRateLocalCallback);
        }
        function OnCostVoidClick(btn, id) {
            grid_Cost.GetValuesOnCustomCallback(btn.GetText() + id, OnRateLocalCallback);
        }
        function PrintCost(id) {
            window.open('/ReportJob/CostingPrint.aspx?id=' + id);
        }
        function ShowCostHistory(id, refN) {
            popubCtr1.SetHeaderText("Costing History");
            popubCtr1.SetContentUrl('CostingHistory.aspx?id=' + id + '&refN=' + refN);
            popubCtr1.Show();
        }
        function UpdateCost(rowIndex) {
            grid_Cost.PerformCallback('UpdateCost' + rowIndex);
        }
        function AfterCostAddClick(v) {
            grid_Cost.Refresh();
            grid_Cost.GetVisibleRowsOnPage()
        }
        function PutAmt() {
            var amount1 = parseFloat(spin_Amount1.GetText());
            var amount2 = parseFloat(spin_Amount2.GetText());
            var amount3 = parseFloat(spin_Amount3.GetText());
            var amount5 = parseFloat(spin_Amount5.GetText());
            var othour = parseFloat(spin_OtHour.GetText());
            var otValue = parseFloat(spin_OtValue.GetText());
            var amount4 = FormatNumber(othour * otValue, 0);
            var sum = parseFloat(amount1) + parseFloat(amount2) + (amount3) + parseFloat(amount4) - parseFloat(amount5);
            var total = FormatNumber(sum, 0);
            spin_Amount4.SetText(amount4);
            spin_TotalPay.SetText(total);
        }
    </script>
</head>
<body >
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.LogEvent"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssue" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobSchedule"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XAArInvoice"
                KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XAApPayable"
                KeyMember="SequenceId" FilterExpression="1=0" />
            <asp:SqlDataSource ID="dsCosting" runat="server" ConnectionString="<%$ ConnectionStrings:local %>" SelectCommand="select * from Cost where SequenceId in (select max(SequenceId) from Cost group by CostIndex) ORDER BY CostIndex" />
            <wilson:DataSource ID="dsPhoto" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPartyGroup" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXPartyGroup"
                KeyMember="Code" />
            <wilson:DataSource ID="dsDoIn" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhDo" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsDoOut" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhDo" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefWarehouse" KeyMember="Id" />
            <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXSalesman" KeyMember="Code" />
            <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefLocation" KeyMember="Id" FilterExpression="Loclevel='Unit'" />
            <wilson:DataSource ID="dsJobItemList" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobItemList" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCrews" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobCrews" KeyMember="Id" />
            <wilson:DataSource ID="dsPersonInfo" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefPersonInfo" KeyMember="Id" />
            <wilson:DataSource ID="dsMaterial" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Material" KeyMember="Id" />
            <wilson:DataSource ID="dsJobMCST" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobMCST" KeyMember="Id" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" />
            <wilson:DataSource ID="dsMessage" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobMessage" KeyMember="Id" />
            <wilson:DataSource ID="dsInventory" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobInventory" KeyMember="Id" />
            <wilson:DataSource ID="dsReleased" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobInventory" KeyMember="Id" />
            <wilson:DataSource ID="dsItemImg" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsItemRImg" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td style="display:none">
                        <dxe:ASPxTextBox ID="txt_SchRefNo" Width="150" ClientInstanceName="txt_SchRefNo" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="display:none">
                       
                    </td>

                    <td style="display:none">
                        <table style="width: 100px; height: 20px;" id="popupArea">
                            <tr>
                                <td id="addnew" style="text-align: center; vertical-align: middle;">
                                    <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="display:none">
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='JobList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
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
                                            Text="Save" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                                 }" />
                                        </dxe:ASPxButton>

					</td>
                        <td align=right width=240>
                          <dxe:ASPxButton ID="btn_CloseJob" ClientInstanceName="btn_CloseJob" runat="server" Width="180" Text="Close" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                        detailGrid.GetValuesOnCustomCallback('Close',OnCloseCallBack);                 

                                        }" />
                                        </dxe:ASPxButton>
                        </td>
					</tr>
                       </table>
					
                        <table  style="text-align: left; border-spacing: 0px; width:auto;display:none" cellpadding="0"  cellspacing="0">
                             <tr style="height:5px">
                                <td colspan="8">
                                    <hr />
                                </td>
                            </tr>
                              <tr>
                                <td style="text-align: center; width:150px;display:none">
                                    <dxe:ASPxButton ID="btn_Inquir" Border-BorderWidth="0"  Border-BorderStyle="None" AutoPostBack="false" BackgroundImage-Repeat="NoRepeat" Width="160px" runat="server"  Height="30px"
                                        Text="Customer Inquiry" Enabled="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Customer Inquiry',OnChangesCallBack);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="text-align: left;width:150px">

                                    <dxe:ASPxButton ID="btn_Survey"  Border-BorderStyle="None" AutoPostBack="false" Height="30px" BackgroundImage-Repeat="NoRepeat"  Width="185px" runat="server" 
                                        Text="Inquiry / Survey" Enabled='<%# SafeValue.SafeString(Eval("Id"),"").Length>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Site Survey',OnChangesCallBack);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="text-align: left;width:150px">
                                    <dxe:ASPxButton ID="btn_Costing" Border-BorderStyle="None" AutoPostBack="false" Height="30px" BackgroundImage-Repeat="NoRepeat"  Width="160px" runat="server" 
                                        Text="Costing" Enabled='<%# SafeValue.SafeString(Eval("Id"),"").Length>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed"%>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Costing',OnChangesCallBack);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="text-align: left;width:150px">
                                    <dxe:ASPxButton ID="btn_Quotation" Border-BorderStyle="None" AutoPostBack="false" Height="30px" BackgroundImage-Repeat="NoRepeat"  Width="150px" runat="server" 
                                        Text="Quotation" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Quotation',OnChangesCallBack);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                               
                                <td style="text-align: left;width:150px">
                                    <dxe:ASPxButton ID="btn_Billing" Border-BorderStyle="None" AutoPostBack="false" BackgroundImage-Repeat="NoRepeat" Height="30px" Width="150px" runat="server" 
                                        Text="Billing" Enabled='<%# SafeValue.SafeString(Eval("Id"),"").Length>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed"%>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Billing',OnChangesCallBack);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                   <td style="text-align: left;width:150px">                         
                                        <dxe:ASPxButton ID="btn_Confirmation" Border-BorderStyle="None" AutoPostBack="false" BackgroundImage-Repeat="NoRepeat" Height="30px"  Width="150px" runat="server" 
                                            Text="Schedule" Enabled='<%# SafeValue.SafeString(Eval("Id"),"").Length>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Schedule',OnChangesCallBack);
                                                 }" />
                                        </dxe:ASPxButton>
                                </td>
                                <td style="text-align: left;width:150px">
                                    <dxe:ASPxButton ID="btn_Completion" Border-BorderStyle="None" AutoPostBack="false" BackgroundImage-Repeat="NoRepeat" Height="30px" Width="180px" runat="server" 
                                        Text="Close" Enabled='<%# SafeValue.SafeString(Eval("Id"),"").Length>0&&(SafeValue.SafeString(Eval("JobStage"),"")=="Close"||SafeValue.SafeString(Eval("JobStage"),"")=="Schedule")  %>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Close',OnChangesCallBack);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="text-align: left;width:150px;display:none">
                                    <dxe:ASPxButton ID="btn_Close" Border-BorderStyle="None" AutoPostBack="false" BackgroundImage-Repeat="NoRepeat" Width="150px" runat="server" 
                                        Text="Job Close" Height="30px" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed"&&SafeValue.SafeString(Eval("JobStage"),"")=="Job Completion" %>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Job Close',OnChangesCallBack);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                            <tr style="height:5px">
                                <td colspan="8">
                                    <hr />
                                </td>
                            </tr>
                            <tr style="text-align: left;">
                                <td style=" width:150px;display:none">
                                    <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                              <dxe:ASPxLabel runat="server" ID="lbl_Date" Text='<%# SafeValue.SafeDate(Eval("DateTime1"),DateTime.Now).ToString("dd/MM/yyyy#HH:mm")%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Staff</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_Staff" ClientInstanceName="btn_Staff" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value1") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_Staff,'Staff');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:160px;">
                                       <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                               <dxe:ASPxDateEdit ID="date_DateTime2" Width="100%" runat="server" Value='<%# Eval("DateTime2") %>'
                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy#HH:mm">
                                        </dxe:ASPxDateEdit>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Surveyor</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_Surveyer" ClientInstanceName="btn_Surveyer" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value2") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_Surveyer,'Surveyer');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:150px;">
                                         <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                              <dxe:ASPxLabel runat="server" ID="ASPxLabel2" Text='<%# SafeValue.SafeDate(Eval("DateTime3"),DateTime.Now).ToString("dd/MM/yy#HH:mm")%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Costing</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_CostingBy" ClientInstanceName="btn_CostingBy" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value3") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_CostingBy,'Costing');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:150px;">
                                       <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                              <dxe:ASPxLabel runat="server" ID="ASPxLabel3" Text='<%# SafeValue.SafeDate(Eval("DateTime4"),DateTime.Now).ToString("dd/MM/yy HH:mm")%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sales</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_Sales" ClientInstanceName="btn_Sales" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value4") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_Sales,'Sales');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:150px;">
                                      <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                              <dxe:ASPxLabel runat="server" ID="ASPxLabel4" Text='<%# SafeValue.SafeDate(Eval("DateTime5"),DateTime.Now).ToString("dd/MM/yy#HH:mm")%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ops</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_OpsAdmin" ClientInstanceName="btn_OpsAdmin" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value5") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_OpsAdmin,'Operations');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:150px;">
                                      <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                              <dxe:ASPxLabel runat="server" ID="ASPxLabel5" Text='<%# SafeValue.SafeDate(Eval("DateTime6"),DateTime.Now).ToString("dd/MM/yy#HH:mm")%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Billing</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_BillingBy" ClientInstanceName="btn_BillingBy" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value6") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_BillingBy,'Accountant');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:150px;">
                                        <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                              <dxe:ASPxLabel runat="server" ID="ASPxLabel6" Text='<%# SafeValue.SafeDate(Eval("DateTime7"),DateTime.Now).ToString("dd/MM/yy#HH:mm")%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Supervisor</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_Supervisor" ClientInstanceName="btn_Supervisor" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value7") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_Supervisor,'Supervisor');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width:150px;display:none">
                                       <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                              <dxe:ASPxLabel runat="server" ID="ASPxLabel7" Text='<%# SafeValue.SafeDate(Eval("DateTime8"),DateTime.Now).ToString("dd/MM/yy#HH:mm")%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Closed</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_ClosedBy" ClientInstanceName="btn_ClosedBy" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value8") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_ClosedBy,'');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <hr />
                                </td>
                            </tr>
                            <tr style="text-align: center">
                                <td style="display: none"></td>
                                <td>
                                    <a href='/ReportJob/PrintSiteSurvey.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Survey Form</a>
                                </td>
                                <td></td>
                                <td>
                                    <a href='/ReportJob/PrintQuotation.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Quotation</a>
                                    <br>
                                    <a href='/ReportJob/PrintAccept.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Acceptance Sheet</a>
                                </td>

                                <td></td>
                                <td>
                                    <a href='/ReportJob/PrintInstruction.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Job Instruction</a>
                                    <br>
                                    <a href='/ReportJob/PrintDraftBL.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Draft B/L</a>
                                </td>
                                <td>
                                    <a href="#" onclick="">Job Summary</a>
                                </td>

                                <td></td>
                            </tr>
                            <tr style="height:5px">
                                <td colspan="8">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 970px">
                                <tr>
                                    <td width="90%">

                                    </td>
                                    <td style="display:none">
                                        <dxe:ASPxButton ID="btn_ConfirmPI" Width="90" runat="server" Text="Confirm" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")=="Quotation" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                               detailGrid.GetValuesOnCustomCallback('Confirm',OnCloseCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="display:none">
                                        <dxe:ASPxButton ID="btn_AddDoOut" ClientInstanceName="btn_AddDoOut" runat="server" Width="90" Text="Create DO" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")=="Job Confirmation" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                               CreateDeliveryOrder();               
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="display:none">
                                        <dxe:ASPxButton ID="btn_CreateBill" Width="120" runat="server" Text="Create Invoice" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")=="Job Completion"  %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                 AddInvoice1(Grid_Invoice, 'WH', txt_DoNo.GetText(), 'JO','IV');
                                                                
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="display:none">
                                        
                                    </td>
                                  

                                </tr>
                            </table>

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
                            <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="100%" Height="440px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Name="Survery" Text="Survery" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl5" runat="server">
                                                <table style="width:960px">
                                                    
                                                    <tr style="text-align:left;">
                                                        <td colspan="2">Full Packing</td>
                                                        <td>
                                                            <dxe:ASPxComboBox  Width="100" ID="cmb_FullPacking" Text='<%# Eval("Item1") %>'
                                                                runat="server">
                                                                <Items>
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
                                    <dxtc:TabPage Text="Item List" Visible="true" Name="Item List">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                                  <table>
                                                    <tr>
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
                                                                                <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false"  Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Costing" Visible="true" Name="Costing">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Cost" runat="server">
                                                <table cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2b" Width="150" runat="server" Text="Add Charges" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false" ClientVisible="false">
                                                                <ClientSideEvents Click="function(s,e) {
													PickBill();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxLabel ID="lb_CostMode" runat="server" Text="Costing Mode" ClientVisible='<%# SafeValue.SafeString(Eval("JobType")).ToUpper().Contains("INTERNATIONAL") %>'></dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_CostMode" runat="server" ClientInstanceName="cmb_CostMode" ClientVisible='<%# SafeValue.SafeString(Eval("JobType")).ToUpper().Contains("INTERNATIONAL") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Lcl" Value="Lcl" Selected="true" />
                                                                    <dxe:ListEditItem Text="20''" Value="20''" />
                                                                    <dxe:ListEditItem Text="40''HC" Value="40''HC" />
                                                                    <dxe:ListEditItem Text="20 Con" Value="20 Con" />
                                                                    <dxe:ListEditItem Text="40 Con" Value="40 Con" />
                                                                    <dxe:ListEditItem Text="Sea" Value="Sea" />
                                                                    <dxe:ListEditItem Text="Air" Value="Air" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2d" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s,e) {
                                                    grid_Cost.GetValuesOnCustomCallback('AddNew',AfterCostAddClick);
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr>
                                                <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server" OnCustomDataCallback="grid_Cost_CustomDataCallback" OnCustomCallback="grid_Cost_CustomCallback"
                                                    DataSourceID="dsCosting" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect" OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
                                                    OnRowUpdating="grid_Cost_RowUpdating" OnRowInserted="grid_Cost_RowInserted" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated" OnRowDeleting="grid_Cost_RowDeleting">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="Inline" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Visible="true" VisibleIndex="0" Width="5%">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostEdit" runat="server" Text="Edit" Width="50" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Cost.StartEditRow("+Container.VisibleIndex+") }" %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostDetEdit" runat="server" Text="Charge" Width="50" AutoPostBack="false" OnInit="btn_CostEdit_Init">
                                                                            </dxe:ASPxButton>
                                                                            <dxe:ASPxTextBox runat="server" ID="txt_Id" OnInit="txt_Id_Init" ClientVisible="false" Text='<%# Eval("SequenceId") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostPrint" runat="server" Text="Print" Width="50" AutoPostBack="false" OnInit="btn_CostPrint_Init">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostUpdate" runat="server" Text="Update" Width="50" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Cost.UpdateEdit() }" %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostCancel" runat="server" Text="Cancel" Width="50" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Cost.CancelEdit() }" %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Visible="true" VisibleIndex="100" Width="5%">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostCopy" runat="server" Text="Copy" Width="50" AutoPostBack="false" OnInit="btn_CostCopy_Init">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostRevise" runat="server" Text="Revise" Width="50" AutoPostBack="false" OnInit="btn_CostRevise_Init">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostVoid" runat="server" Text='<%#SafeValue.SafeString(Eval("Status")).ToUpper()=="VOID"?"Unvoid":"Void" %>' Width="70" AutoPostBack="false" OnInit="btn_CostVoid_Init">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostHistory" runat="server" Text="History" Width="50" AutoPostBack="false" OnInit="btn_CostHistory_Init" ClientVisible='<%#SafeValue.SafeInt(Eval("Version"),0)>1 %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="#" FieldName="CostIndex" Width="20" VisibleIndex="1" SortIndex="0" SortOrder="Descending">
                                                            <EditItemTemplate><%#Eval("CostIndex") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Version" FieldName="Version" Width="20" VisibleIndex="1" SortIndex="1" SortOrder="Descending">
                                                            <EditItemTemplate><%#Eval("Version") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" Width="200" VisibleIndex="2" EditCellStyle-Paddings-Padding="0">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Remark" FieldName="Marking" Width="200" VisibleIndex="2" EditCellStyle-Paddings-Padding="0">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Profitmargin" FieldName="Profitmargin" Width="150" VisibleIndex="3">
                                                            <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Total Charges" FieldName="Amount" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amount") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amount") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Total Cost" FieldName="Amount2" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amount2") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amount2") %><dxe:ASPxTextBox runat="server" Text='<%#Bind("Amount2") %>' ClientVisible="false" ID="txt_Amount2"></dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Gross Profit" FieldName="Amount" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#SafeValue.SafeDecimal(Eval("Amount"),0)-SafeValue.SafeDecimal(Eval("Amount2"),0) %></DataItemTemplate>
                                                            <EditItemTemplate><%#SafeValue.SafeDecimal(Eval("Amount"),0)-SafeValue.SafeDecimal(Eval("Amount2"),0) %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Status" Caption="Status" Width="80" VisibleIndex="5">
                                                            <EditItemTemplate><%#Eval("Status") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Create User/Time" FieldName="CreateBy" Width="90" VisibleIndex="98">
                                                            <DataItemTemplate>
                                                                <%#SafeValue.SafeString(Eval("CreateBy"))+" - "+SafeValue.SafeString(Eval("CreateDateTime","{0:dd/MM HH:mm}")) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Update User/Time" FieldName="UpdateBy" Width="90" VisibleIndex="99">
                                                            <DataItemTemplate>
                                                                <%#SafeValue.SafeString(Eval("UpdateBy"))+" - "+SafeValue.SafeString(Eval("UpdateDateTime","{0:dd/MM HH:mm}")) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>

                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Quotation" Visible="true" Name="Quotation">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl10" runat="server">
                                               <table style="width:960px;">
                                                    <tr>
                                                        <td  style="background-color: Gray; color: White; ">
                                                            <b>INSURANCE COVERAGE:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention1"  runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention1") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
<%-- <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ID="txt_Attention1" MaxLength="500" Text='<%# Eval("Attention1")%>'>
                                                                    </dxe:ASPxMemo>--%>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>FOR THE ABOVE RATE COLLIN'S SERVICES INCLUDE:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <dxe:ASPxHtmlEditor ID="txt_Attention2"  runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention2") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>OUR SERVICES EXCLUDE:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention3"  runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention3") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">
                                                            <b>ACCEPANCE OF OUOTATION:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention4"  runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention4") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">
                                                            <b>PAYMENT TERMS:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention5"  runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="180" Width="100%" Html='<%# Eval("Attention5") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="False" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Billing" Visible="true" Name="Billing">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Bill" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add Credit Note" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Invoice" ClientInstanceName="Grid_Invoice"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsInvoice" Width="900px"
                                                    OnBeforePerformDataSelect="Grid_Invoice_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowInvoice(Grid_Invoice,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintWh_invoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>")'>Print</a>
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
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Crews" Visible="true" Name="Crews">
                                        <ContentCollection>
                                          <dxw:ContentControl ID="ContentControl4" runat="server">
                                                 <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton10" runat="server" Text="Assign Crews" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        MultiplePickCrews();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Crews" ClientInstanceName="grid_Crews" DataSourceID="dsCrews" runat="server"  OnRowInserting="grid_Crews_RowInserting" Width="100%"
                                                        OnRowDeleting="grid_Crews_RowDeleting" OnRowUpdating="grid_Crews_RowUpdating" OnBeforePerformDataSelect="grid_Crews_BeforePerformDataSelect"
                                                        KeyFieldName="Id" OnInit="grid_Crews_Init" OnInitNewRow="grid_Crews_InitNewRow" >
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Crews.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_del" runat="server" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Crews.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false" 
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Crews.UpdateEdit() }"  %>' Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Crews.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                                Caption="Name" FieldName="Name" VisibleIndex="1" Width="160">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="NRIC" FieldName="Code" VisibleIndex="3" Width="160px">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Standard Wages" Width="70" FieldName="Amount1" VisibleIndex="3" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false">
                                                                <PropertiesSpinEdit NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Refreshment" Width="70" FieldName="Amount2" VisibleIndex="3" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false">
                                                                <PropertiesSpinEdit NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Others" Width="70" FieldName="Amount3" VisibleIndex="3" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false">
                                                                <PropertiesSpinEdit NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00"></PropertiesSpinEdit>
                                                                
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="OT Hours" Width="70" FieldName="Amount4" VisibleIndex="3" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false">
                                                                <PropertiesSpinEdit NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Reduction" Width="70" FieldName="Amount5" VisibleIndex="3" PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false">
                                                                <PropertiesSpinEdit NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00"></PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                             <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="3" Width="80px" Visible="false" >
                                                            </dxwgv:GridViewDataTextColumn>

                                                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="8" Width="200">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="PayTotal" FieldName="PayTotal" VisibleIndex="8" Width="100">
                                                            </dxwgv:GridViewDataTextColumn>
															<dxwgv:GridViewDataDateColumn Caption="Payment Date" FieldName="PayDate" VisibleIndex="9" Width="180">
                                                                <PropertiesDateEdit EditFormatString="dd/MM/yyyy HH:mm"  DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                                                            </dxwgv:GridViewDataDateColumn>
                                                        </Columns>
<Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>Name</td>
                                                                    <td>
                                                                         <dxe:ASPxTextBox runat="server" Width="120" ID="txt_Name" ClientInstanceName="txt_Name" Text='<%# Bind("Name") %>'>
                                                            </dxe:ASPxTextBox>
                                                                       </td>
                                                                    <td>Tel</td>
                                                                    <td>
                                                                         <dxe:ASPxTextBox runat="server" Width="100" ID="txt_Tel" ClientInstanceName="txt_Tel" Text='<%# Bind("Tel") %>'>
                                                            </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        Work Date
                                                                    </td>
                                                                    <td>
                                                                          <dxe:ASPxDateEdit ID="date_PayDate" Width="160" runat="server" Value='<%# Eval("PayDate") %>'
                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td>WorkHour</td>
                                                                    <td>
                                                                         <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120" Value='<%# Eval("WorkHour") %>'
                                                         DecimalPlaces="1" ID="spin_WorkHour" ClientInstanceName="spin_WorkHour" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                     <td>OverTime</td>
                                                                    <td>
                                                                         <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("OtHour"                                                        ) %>'
 DecimalPlaces="1" ID="spin_OtHour" ClientInstanceName="spin_OtHour" Increment="0">
                                                                             
                                                                               <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                    </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td>Total</td>
                                                                    <td colspan="5"> 
                                                                        <table cellpadding="0" cellspacing="0" >
                                                                            
                                                                            <tr>
                                                                                
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120" Value='<%# Bind("PayTotal") %>'
                                                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_TotalPay" ClientInstanceName="spin_TotalPay" Increment="0">
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>=</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="78" Value='<%# Bind("Amount1") %>'
                                                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Amount1" ClientInstanceName="spin_Amount1" Increment="0">

                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>+</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="78" Value='<%# Bind("Amount2") %>'
                                                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Amount2" ClientInstanceName="spin_Amount2" Increment="0">

                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>+</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="75" Value='<%# Bind("Amount3") %>'
                                                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Amount3" ClientInstanceName="spin_Amount3" Increment="0">

                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>+</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="75" Value='<%# Bind("Amount4") %>'
                                                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Amount4" ClientInstanceName="spin_Amount4" Increment="0">
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                <td>-</td>
                                                                                <td>
                                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="75" Value='<%# Bind("Amount5") %>'
                                                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Amount5" ClientInstanceName="spin_Amount5" Increment="0">

                                                                                        <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                                
                                                                            </tr>

                                                                        </table>
                                                                        <div style="display:none">
                                                                             <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("OverTimeValue") %>' DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_OtValue" ClientInstanceName="spin_OtValue" Increment="0">
                                                                                        
                                                                               <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                    </dxe:ASPxSpinEdit>
                                                                        </div>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Remark</td>
                                                                    <td colspan="3">
                                                                                                                                                <dxe:ASPxMemo ID="memo_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="100%" Rows="4"></dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>PayNote</td>
                                                                    <td >
                                                                        <dxe:ASPxMemo ID="memo_PayNote" Rows="4" Width="100%" runat="server" Text='<%# Bind("PayNote") %>'></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                             <div style="text-align: right; padding: 2px 2px 2px 2px;width:800px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                    <ClientSideEvents Click="function(s,e){grid_Crews.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                runat="server">
                                                            </dxwgv:ASPxGridViewTemplateReplacement>
                                                        </div>
                                                        </EditForm>
                                                    </Templates>
                                                        <Settings ShowFooter="true" />
                                                        <TotalSummary>
                                                            <dxwgv:ASPxSummaryItem FieldName="Name" SummaryType="Count" DisplayFormat="{0:0}" />
                                                        </TotalSummary>
                                                    </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="MCST" Visible="true" Name="MCST">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl7" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Add New MCST" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Mcst.AddNewRow();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Mcst" ClientInstanceName="grid_Mcst" DataSourceID="dsJobMCST" runat="server"  OnRowInserting="grid_Mcst_RowInserting" Width="100%"
                                                        OnRowDeleting="grid_Mcst_RowDeleting" OnRowUpdating="grid_Mcst_RowUpdating" OnBeforePerformDataSelect="grid_Mcst_BeforePerformDataSelect"
                                                        KeyFieldName="Id" OnInit="grid_Mcst_Init" OnInitNewRow="grid_Mcst_InitNewRow" >
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsEditing Mode="EditForm" />
                                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Mcst.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_del" runat="server"
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Mcst.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false" 
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Mcst.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Mcst.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                            Caption="MCST No" FieldName="McstNo" VisibleIndex="1" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="Date Out" FieldName="McstDate1" VisibleIndex="2" Width="100px">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>                                                                                                                                            <dxwgv:GridViewDataDateColumn Caption="Date In" FieldName="McstDate2" VisibleIndex="3" Width="100px">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Deposit" FieldName="Amount1" VisibleIndex="3" Width="60px">
                                                        </dxwgv:GridViewDataTextColumn>


                                                        <dxwgv:GridViewDataTextColumn Caption="Other Fee" FieldName="Amount2" VisibleIndex="6" Width="60px">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ChqNo" FieldName="States" VisibleIndex="8" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Condo Name " FieldName="McstRemark1" VisibleIndex="10" Width="180px">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                           <table>
                                                                <tr>
                                                                    <td>MCST No</td>
                                                                    <td  colspan="3">
                                                                        <dxe:ASPxTextBox runat="server" Width="260" ID="txt_McstNo1"
                                                                            Text='<%# Bind("McstNo") %>' ClientInstanceName="txt_McstNo1">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    
                                                                    
                                                                    <td>Deposit</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="150" ID="spin_Amount1" Value='<%# Bind("Amount1")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                     <td> Other Fee</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="150" ID="spin_Amount2" Value='<%# Bind("Amount2")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                   
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    <td>Condo Name</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox runat="server"  Width="100%" ID="txt_McstRemark1" MaxLength="500" Text='<%# Bind("McstRemark1")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                      <td> Date Out</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_McstDate1" Width="150" runat="server" Value='<%# Bind("McstDate1") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Date In</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_McstDate2" Width="150" runat="server" Value='<%# Bind("McstDate2") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                  
                                                                </tr>
                                                                <tr>
                                                                     <td> Condo Address</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo runat="server" Rows="3" Width="100%" ID="txt_McstRemark2" MaxLength="500" Text='<%# Bind("McstRemark2")%>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                     <td>Condo Tel</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server"  Width="100%" ID="txt_CondoTel" MaxLength="11" Text='<%# Bind("CondoTel")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                     
                                                                     <td>Chq No</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_States1"
                                                                            Text='<%# Bind("States") %>' ClientInstanceName="txt_States1">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>MCST Remark</td>
                                                                    <td  colspan="7">
                                                                        <dxe:ASPxMemo runat="server" Rows="4" Width="100%" ID="memo_McstRemark3" MaxLength="500" Text='<%# Bind("McstRemark3")%>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="8">
                                                                        <hr>
                                                                        <table>
                                                                            <tr>
                                                                                <td style="width: 80px;">Creation</td>
                                                                                <td style="width: 200px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateTimeStr( Eval("CreateDateTime"))%></td>
                                                                                <td style="width: 100px;">Last Updated</td>
                                                                                <td style="width: 200px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateTimeStr(Eval("UpdateDateTime"))%></td>
                                                                                <td style="width: 80px;"></td>
                                                                               
                                                                            </tr>
                                                                        </table>
                                                                        <hr>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                    <ClientSideEvents Click="function(s,e){grid_Mcst.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                runat="server">
                                                            </dxwgv:ASPxGridViewTemplateReplacement>
                                                        </div>
                                                        </EditForm>
                                                    </Templates>
                                                        <Settings ShowFooter="true" />
                                                        <TotalSummary>
                                                            <dxwgv:ASPxSummaryItem FieldName="Name" SummaryType="Count" DisplayFormat="{0:0}" />
                                                        </TotalSummary>
                                                    </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>                                   
                                     <dxtc:TabPage Text="Material" Visible="true" Name="Material">
                                        <ContentCollection>
                                           <dxw:ContentControl ID="ContentControl9" runat="server">
                                                  <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Add Material" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        detailGrid.GetValuesOnCustomCallback('Material',OnCostingCallBack);
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
  <td>
                                                           <%--<dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Print Materials" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                    window.open('/ReportJob/PrintMaterials.aspx?no=' + txt_DoNo.GetText()+'&party='+txt_CustomerName.GetText()+'&date='+date_Pack.GetText()+'&address='+memo_Address.GetText()+'&jobType='+cmb_JobType.GetText());
                                                        }" />
                                                            </dxe:ASPxButton>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Material" ClientInstanceName="grid_Material" runat="server"  OnRowInserting="grid_Material_RowInserting" Width="100%"
                                                        OnRowDeleting="grid_Material_RowDeleting" OnRowUpdating="grid_Material_RowUpdating" OnBeforePerformDataSelect="grid_Material_BeforePerformDataSelect" DataSourceID="dsMaterial"
                                                        KeyFieldName="Id" OnInit="grid_Material_Init" OnInitNewRow="grid_Material_InitNewRow" >
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Material.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_del" runat="server"
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Material.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false" 
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Material.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Material.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn
                                                                Caption="Description" FieldName="Description" VisibleIndex="1" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="3" Width="40px">
                                                                <EditItemTemplate>
<dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="60" ID="cmb_Unit" Text='<%# Bind("Unit") %>'
                                                                runat="server">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Ctn" Value="Ctn" />
                                                                    <dxe:ListEditItem Text="Kgs" Value="Kgs" />
                                                                     <dxe:ListEditItem Text="Pcs" Value="Pcs" />
                                                                    <dxe:ListEditItem Text="Rm" Value="Rm" />
                                                                    <dxe:ListEditItem Text="Roll" Value="Roll" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>

                                                               <dxwgv:GridViewDataTextColumn Caption="" VisibleIndex="3" Width="600px"
                                                                    CellStyle-Paddings-Padding="0" EditCellStyle-Paddings-Padding="0"
                                                                    PropertiesTextEdit-Style-Spacing="0">
                                                                   <HeaderTemplate>
                                                                       <table style="width:100%; text-align:center;border-spacing: 0px;width:100%;
    height:auto;font-family:Arial;font-size:12px; padding:0px;spacing:0px">
                                                                           <tr >
                                                                               <td colspan="14" style="border-bottom: solid 1px #9B9A96;">QUANTITY</td>
                                                                           </tr>
                                                                           <tr>
                                                                               <td colspan="2" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   REQUISITIONED
                                                                               </td>
                                                                               <td colspan="2" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   REQUISITIONED1
                                                                               </td>
                                                                               <td colspan="2" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   REQUISITIONED2
                                                                               </td>
                                                                               <td colspan="2" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   ADDITIONAL

                                                                               </td>
                                                                                <td colspan="2" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   ADDITIONAL1

                                                                               </td>
                                                                                <td colspan="2" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   ADDITIONAL2

                                                                               </td>
                                                                               <td colspan="2" style="border-bottom: solid 1px #9B9A96;">
                                                                                   RETURNED
                                                                               </td>

                                                                           </tr>
                                                                           <tr>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(1a)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(1b)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(1a)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(1b)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(1a)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(1b)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(2a)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(2b)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(2a)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(2b)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(2a)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(2b)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(3a)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;">
                                                                                   Used(3b)</td>

                                                                           </tr>
                                                                       </table>
                                                                   </HeaderTemplate>
                                                                   <DataItemTemplate>
                                                                        <table style="width: 100%; margin: 0px auto; text-align: center; border-spacing: 0px; width: 100%; height: 100%; font-family: Arial; font-size: 12px;">
                                                                           <tr>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:65px;"><%# Eval("RequisitionNew") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("RequisitionUsed") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px;"><%# Eval("RequisitionNew1") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("RequisitionUsed1") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px;"><%# Eval("RequisitionNew2") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("RequisitionUsed2") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px;"><%# Eval("AdditionalNew") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("AdditionalUsed") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px;"><%# Eval("AdditionalNew1") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("AdditionalUsed1") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("AdditionalNew2") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("AdditionalUsed2") %></td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:40px;width:60px; "><%# Eval("ReturnedNew") %></td>
                                                                               <td width="7.1%">
                                                                                   <%# Eval("ReturnedUsed") %></td>

                                                                           </tr>
                                                                       </table>
                                                                   </DataItemTemplate>
                                                                   <EditItemTemplate>
                                                                       <table style="width: 100%; margin: 0px auto; text-align: center; border-spacing: 0px; height: 100%; font-family: Arial; font-size: 12px;">
                                                                           <tr>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:48px">
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionNew" runat="server" Width="63px" ClientInstanceName="spin_RequisitionNew" Value='<%# Bind("RequisitionNew") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionUsed" runat="server" Width="60" ClientInstanceName="spin_RequisitionUsed" Value='<%# Bind("RequisitionUsed") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:48px">
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionNew1" runat="server" Width="60" ClientInstanceName="spin_RequisitionNew1" Value='<%# Bind("RequisitionNew1") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionUsed1" runat="server" Width="60" ClientInstanceName="spin_RequisitionUsed1" Value='<%# Bind("RequisitionUsed1") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE; height:48px">
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionNew2" runat="server" Width="60" ClientInstanceName="spin_RequisitionNew2" Value='<%# Bind("RequisitionNew2") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionUsed2" runat="server" Width="60" ClientInstanceName="spin_RequisitionUsed2" Value='<%# Bind("RequisitionUsed2") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalNew" runat="server"  Width="60" ClientInstanceName="spin_AdditionalNew" Value='<%# Bind("AdditionalNew") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalUsed" runat="server" Width="60" ClientInstanceName="spin_AdditionalUsed" Value='<%# Bind("AdditionalUsed") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalNew1" runat="server" Width="60" ClientInstanceName="spin_AdditionalNew" Value='<%# Bind("AdditionalNew1") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalUsed1" runat="server" Width="60" ClientInstanceName="spin_AdditionalUsed" Value='<%# Bind("AdditionalUsed1") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalNew2" runat="server" Width="60" ClientInstanceName="spin_AdditionalNew" Value='<%# Bind("AdditionalNew2") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalUsed2" runat="server" Width="60" ClientInstanceName="spin_AdditionalUsed" Value='<%# Bind("AdditionalUsed2") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%" style="border-right: solid 1px #CECECE;">
                                                                                   <dxe:ASPxSpinEdit ID="spin_ReturnedNew" runat="server" Width="60" ClientInstanceName="spin_ReturnedNew" Value='<%# Bind("ReturnedNew") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td width="7.1%">
                                                                                   <dxe:ASPxSpinEdit ID="spin_ReturnedUsed" runat="server" Width="60" ClientInstanceName="spin_ReturnedUsed" Value='<%# Bind("ReturnedUsed") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>

                                                                           </tr>
                                                                       </table>
                                                                   </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                             <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="180px" CellStyle-Paddings-Padding="0" EditCellStyle-Paddings-Padding="0"
                                                                    PropertiesTextEdit-Style-Spacing="0">
                                                                 <HeaderTemplate>
                                                                    <table style="width: 100%; text-align: center; border-spacing: 0px; width: 100%; height: auto; font-family: Arial; font-size: 12px;">
                                                                         <tr>
                                                                             <td colspan="2" style="border-bottom: solid 1px #9B9A96;">TOTAL USED</td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">1a+2a-3a</td>
                                                                             <td style="border-bottom: solid 1px #9B9A96;">1b+2b-3b</td>
                                                                         </tr>
                                                                          <tr>
                                                                             <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New</td>
                                                                             <td style="border-bottom: solid 1px #9B9A96;">Used</td>
                                                                         </tr>
                                                                     </table>
                                                                 </HeaderTemplate>
                                                                 <DataItemTemplate>
                                                                      <table style="width: 100%; text-align: center; border-spacing: 0px; width: 100%; height: auto; font-family: Arial; font-size: 12px;">
                                                                         <tr>
                                                                             <td width="50%" style="border-right: solid 1px #9B9A96;height:40px"><%# Eval("TotalNew") %></td>
                                                                             <td width="50%"><%# Eval("TotalUsed") %></td>
                                                                         </tr>
                                                                     </table>
                                                                 </DataItemTemplate>
                                                                 <EditItemTemplate>
                                                                     <table style="width: 100%; text-align: center; border-spacing: 0px; width: 100%; height: auto; font-family: Arial; font-size: 12px;">
                                                                         <tr>
                                                                             <td width="50%" style="border-right: solid 1px #9B9A96;height:48px"><%# Eval("TotalNew") %></td>
                                                                             <td width="50%"><%# Eval("TotalUsed") %></td>
                                                                         </tr>
                                                                     </table>
                                                                 </EditItemTemplate>
                                                                 </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Settings ShowFooter="true" />
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
                                                            <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Upload Attachments" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                    isUpload=true;
                                                                    PopupUploadPhoto('Att');
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

                                                <dxwgv:ASPxGridView ID="grd_Attach" ClientInstanceName="grd_Attach" runat="server" DataSourceID="dsAttachment"
                                                    KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Attach_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Attach_RowDeleting" OnInit="grd_Attach_Init"  OnRowUpdating="grd_Attach_RowUpdating">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
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
                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
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
                                                                        <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>

                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Photos" Visible="true" Name="Photos">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl6" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton13" Width="150" runat="server" Text="Upload Photo" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                    isUpload=true;
                                                                    PopupUploadPhoto('IMG');
                                                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton14" runat="server" Text="Refresh" AutoPostBack="false"
                                                                UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s,e) {
                                                                    grd_Photo.Refresh();
                                                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsPhoto"
                                                    KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
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
                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
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
                                                                        <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Message" Visible="true" Name="Message">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl20" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Add New Message" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                    grid_Message.AddNewRow();
                                                                }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Message" ClientInstanceName="grid_Message" runat="server" OnRowDeleting="grid_Message_RowDeleting" OnInit="grid_Message_Init"
                                                     OnRowInserting="grid_Message_RowInserting"
 OnInitNewRow="grid_Message_InitNewRow"  KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" DataSourceID="dsMessage" OnBeforePerformDataSelect="grid_Message_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <SettingsEditing  Mode="Inline"/>
                                                    <SettingsBehavior  ConfirmDelete="true"/>
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn  VisibleIndex="0" Width="40">
                                                            
                                                            <DeleteButton Text="Delete" Visible="true"></DeleteButton>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Title" FieldName="MTitle" VisibleIndex="2" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Content" FieldName="MBody" VisibleIndex="3" Width="300">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Create By" FieldName="CreatedBy" VisibleIndex="4" Width="70" ReadOnly="true">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Create Time" FieldName="CreateDateTime" VisibleIndex="4" Width="70" ReadOnly="true">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                     <dxtc:TabPage Text="Job Event" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl3" runat="server">
                                                 <dxwgv:ASPxGridView ID="grid_Log" ClientInstanceName="grid_Log" runat="server"
                                                    KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" DataSourceID="dsLog" OnBeforePerformDataSelect="grid_Log_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Action" VisibleIndex="2" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="User" FieldName="CreateBy" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Time" FieldName="CreateDateTime" VisibleIndex="4" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>
                            </dxtc:ASPxPageControl>
                            <hr />
                                        </td>
                                    </tr>
                                </table>
                        </div>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="970" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grd_Photo.Refresh();
                        grd_Attach.Refresh();
                }" />
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="970" EnableViewState="False">
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
