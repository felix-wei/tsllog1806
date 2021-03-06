﻿<%@ Page Language="C#"  EnableViewState="false" AutoEventWireup="true" CodeFile="JobEdit.aspx.cs" Inherits="WareHouse_Job_JobEdit" %>
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
		function PrintInvoiceHtml(dn,dt) {
			window.open("/reportjob/printinvoicenew.aspx?no=" + dn);
        }

        function PopupUploadPhoto(type) {
            if (type == "IMG") {
                popubCtr.SetHeaderText('Upload Photo');
            } else {
                popubCtr.SetHeaderText('Upload Attachment');
            }
            popubCtr.SetContentUrl('../Upload.aspx?Type=JO&Sn=' + txt_Quotation.GetText() + '&AttType=' + type);
            popubCtr.Show();
        }
        function PopubCrews(jobNo, date, type) {
            popubCtr.SetHeaderText('Crews List');
            popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectCrewsList.aspx?no=' + jobNo + '&date=' + date + "&type=" + type);
            popubCtr.Show();
        }
        function PopubMaterials(jobNo) {
            popubCtr.SetHeaderText('Materials List');
            popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SelectMaterialsList.aspx?no=' + jobNo);
            popubCtr.Show();
        }
	 
        function ShowSchedule(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/JobScheduleEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function gotoSchedule(ds) {
            parent.navTab.openTab(ds, "/Modules/WareHouse/Job/JobScheduleEdit.aspx?d=" + ds, { title: "Sch:"+ds, fresh: false, external: true });
        }
        function PopupUploadItemPhoto(type, item,itemName) {
            if (type == "IMG") {
                popubCtr1.SetHeaderText('Upload Photo');
            } else {
                popubCtr1.SetHeaderText('Upload Attachment');
            }
            popubCtr1.SetContentUrl('../UploadItem.aspx?Type=JO&Sn=' + txt_DoNo.GetText() + '&AttType=' + type + '&item=' + item + '&itemName=' + itemName);
            popubCtr1.Show();
        }
	    function PopupPerson(id,name,type) {
            clientId = id;
            clientName = name;
            popubCtr.SetHeaderText('' + type + '');

            popubCtr.SetContentUrl('/PagesHr/SelectPage/PersonList.aspx?Type='+type);
            popubCtr.Show();
	    }
	    function AfterUploadItemPhoto() {
	        popubCtr1.Hide();
	        popubCtr1.SetContentUrl('about:blank');
	    }
	    function OnCloseCallBack(v) {
	        if (v != null) {
	            window.location = 'JobEdit.aspx?refN=' + v;
	        }
	       else if (v == "Success") {
	            alert("Action Success!");
	            detailGrid.Refresh();
	        }
	       else if (v == "Save") {
	            detailGrid.Refresh();
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
                //txt_CustId.SetText();
                //txt_CustName.SetText();
                //txt_NewContact.SetText();
                //txt_NewFax.SetText();
                //txt_NewTel.SetText();
                //txt_NewEmail.SetText();
                //txt_NewPostalCode.SetText();
                //memo_NewAddress.SetText();
                //txt_NewRemark.SetText();
                //cmb_NewJobType.SetText();
                //date_NewIssueDate.SetText();
                //ASPxPopupClientControl.Hide();
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
	                //alert("Success");
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
	        function OnSchCallBack(v) {
	            if (v != null && v.indexOf("Fail") > -1) {
	                alert(v);
	            }
	            else {
	                alert("Success!");
	                grid_sch.Refresh();
	                //parent.navTab.openTab(v, "/Modules/WareHouse/Job/JobScheduleEdit.aspx?no=" + v, { title: v, fresh: false, external: true });

	            }
	        }
	        function OnFreightCallBack(v) {
	            if (v != null && v.indexOf("Fail") > -1) {
	                alert(v);
	            }
	            else {
	                alert("Success!");
	                grid_freight.Refresh();
	                //parent.navTab.openTab(v, "/Modules/WareHouse/Job/JobScheduleEdit.aspx?no=" + v, { title: v, fresh: false, external: true });

	            }
	        }
	        function NewFreightImport(refType) {
	            parent.navTab.openTab(txt_DoNo.GetText() + '/' + refType, "/PagesFreight/Import/ImportRefEdit.aspx?no=0&refType=" + refType + "&MasterNo=" + txt_DoNo.GetText(), { title: txt_DoNo.GetText() + '/' + refType, fresh: false, external: true });
	        }

	        function NewFreightExport(refType) {
	            parent.navTab.openTab(txt_DoNo.GetText() + '/' + refType, "/PagesFreight/Export/ExportRefEdit.aspx?no=0&refType=" + refType + "&MasterNo=" + txt_DoNo.GetText(), { title: txt_DoNo.GetText() + '/' + refType, fresh: false, external: true });
	        }
	        function ShowImportJob(masterId, refType) {
	            parent.navTab.openTab(masterId, "/PagesFreight/Import/ImportRefEdit.aspx?no=" + masterId + "&refType=" + refType, { title: masterId, fresh: false, external: true });
	        }
	        function ShowExportJob(masterId, refType) {
	            parent.navTab.openTab(masterId, "/PagesFreight/Export/ExportRefEdit.aspx?no=" + masterId + "&refType=" + refType, { title: masterId, fresh: false, external: true });
	        }
	        function OnCostingCallBack(v) {
	            if (v != null && v.indexOf("Fail") > -1) {
	                alert(v);
	            }
	            else{
	                //alert("Success");
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
	            parent.navTab.openTab(v, "/Modules/WareHouse/Job/JobScheduleEdit.aspx?no=" + v+"&JobType=IN", { title: v, fresh: false, external: true });
	        }
	        function OnDoOutCallBack(v) {
	            if (v == "Fail")
	                alert("Create DO Job out Fail");
	            else if (v == "No Balance Qty or No Lot No!")
	                alert(v);
	            else if (v != null)
	                alert("Success!");
	            grid_DoIn.Refresh();
	            parent.navTab.openTab(v, "/Modules/WareHouse/Job/JobScheduleEdit.aspx?no=" + v+"&JobType=OUT", { title: v, fresh: false, external: true });
	        }

	        function MultiplePickCrews() {
	            popubCtr.SetHeaderText('Multiple Crews');
	            popubCtr.SetContentUrl('../SelectPage/MultipleCrews.aspx?Sn=' + txt_Quotation.GetText());
	            popubCtr.Show();
	        }
	        function MultiplePickItem() {
	            popubCtr1.SetHeaderText('Delivery Item');
	            popubCtr1.SetContentUrl('../SelectPage/MultipleAddItem.aspx?typ=OUT&Sn=' + txt_DoNo.GetText());
	            popubCtr1.Show();
	        }
	        function AfterPopubMultiInv() {
	            popubCtr.Hide();
	            popubCtr.SetContentUrl('about:blank');
	            grid_sch.Refresh();
	            grid_Crews.Refresh();
	            grid_DoIn.Refresh();
	        }
	        function AfterPopubMultiInv1(v) {
	            popubCtr1.Hide();
	            popubCtr1.SetContentUrl('about:blank');
	            if (v != null) {
	                window.location = 'JobEdit.aspx?no=' + v;
	            }
	            if (grid_Inventory != null) {
	                grid_Inventory.Refresh();
	            }
	            if (grid_Released != null) {
	                grid_Released.Refresh();
	            }
	            else {
	                grid_sch.Refresh();
	            }
	        }
	        function PrintDo(doNo, doType) {
	            if (doType == "SO")
	                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_So&master=' + doNo + "&house=0");
	            else if (doType == "Inv")
	                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_Inv&master=' + doNo + "&house=0");
	            else if (doType == "DoOut")
	                window.open('/Modules/WareHouse/PrintView.aspx?document=wh_SoDoOut&master=' + doNo + "&house=0");
	        }

	        function AddInvoice1(gridId, JobType, refNo, jobNo, docType) {
	            grid = gridId;
	            popubCtr1.SetHeaderText('Invoice');
	            popubCtr1.SetContentUrl('../Account/ArInvoice.aspx?no=0&JobType=' + JobType + '&RefN=' + refNo + '&JobN=' + jobNo + '&DocType=' + docType);
	            popubCtr1.Show();
	        }
	        function PickBill() {
	            var sn = txt_Quotation.GetText();
	            var jt = cmb_JobType.GetText();
	            var cc = txt_CustomerName.GetText();
	            var ac = "";//txt_AgentCode.GetText();
	            ac = btn_OriginPort.GetText();
	            popubCtr1.SetHeaderText('Select Charges');
	            popubCtr1.SetContentUrl('/SelectPage/PickRate.aspx?g=BILL&o=' + sn + '&t=' + jt + '&c=' + cc + '&a=' + ac);
	            popubCtr1.Show();
	        }

	        function PickCost() {
	            var sn = txt_Quotation.GetText();
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
			function OnQuoteEditClick(no, rev) {
	            popubCtr1.SetHeaderText("Revised Quotate Edit");
	            popubCtr1.SetContentUrl('QuoteEdit.aspx?no=' + no + '&rev=' + rev);
	            popubCtr1.Show();
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
	        function OnCostVoidClick(btn,id) {
	            grid_Cost.GetValuesOnCustomCallback(btn.GetText() + id, OnRateLocalCallback);
	        }
	        function PrintCost(id) {
	            window.open('/ReportJob/CostingPrint.aspx?id=' + id);
	        }
	        function ShowCostHistory(id,refN) {
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
	        function OnScheduleCallback() {
	            grid_sch.Refresh();
	        }
	        function OnCopyClick(id) {
	            grid_sch.GetValuesOnCustomCallback('Copy' + id, OnScheduleCallback);
	        }
    </script>
</head>
<body >
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.LogEvent"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssue" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobInfo"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsIssueDet" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhTransDet"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XAArInvoice"
                KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XAApPayable"
                KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsJobCost" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobCost"
                KeyMember="SequenceId"  />
            <wilson:DataSource ID="dsJobCostRev" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobCost"
                KeyMember="SequenceId"   />
            <wilson:DataSource ID="dsChgCode" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode"
                KeyMember="SequenceId"  FilterExpression="ChgTypeId='Billing'" />
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.Cost"
                KeyMember="SequenceId"  />
            <wilson:DataSource ID="dsPhoto" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPartyGroup" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXPartyGroup"
                KeyMember="Code" />
            <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
                KeyMember="Id" FilterExpression="CodeType='1'" />
            <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
                KeyMember="Id" FilterExpression="CodeType='2'" />
            <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhMastData"
                KeyMember="Id" FilterExpression="Type='Attribute'" />
             <wilson:DataSource ID="dsDoIn" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobSchedule" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsDoOut" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobSchedule" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPoRequest" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhPORequest" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsReceipt" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArReceipt" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefWarehouse" KeyMember="Id" />
            <wilson:DataSource ID="dsSalesman" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXSalesman" KeyMember="Code" />
            <wilson:DataSource ID="dsRefLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefLocation" KeyMember="Id" FilterExpression="Loclevel='Unit'" />
            <wilson:DataSource ID="dsJobItemList" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobItemList" KeyMember="Id" />
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
			<wilson:DataSource ID="dsHaulierCont" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobSchedule"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobSchedule"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsFreight" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobSchedule"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsItemImg" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsItemRImg" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
           <wilson:DataSource ID="dsContainer" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container" KeyMember="Id"/>
        <wilson:DataSource ID="dsContainerType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="Id"  />
        <wilson:DataSource ID="dsContainerCategory" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="Code" FilterExpression="CodeType='TankCate'" />
            <wilson:DataSource ID="dsRefCont" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaImportMkg" KeyMember="SequenceId" />
            <wilson:DataSource ID="dsTripCode" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='tripcode'" />
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

					<table border=0 style="width:960px;">
                        <tr>
                            <td style="font-size: 20px; width=480" nowrap colspan="4">
                                <%# string.Format("<b>{0}</b> ",Eval("QuotationNo")) %><%# string.Format("[<b style='color:red;'>Job No : {0}</b>] [{2}]",Eval("JobNo"),Eval("JobType"),Eval("WorkStatus")) %>
                            </td>
                            <td width="100">
                                <dxe:ASPxButton ID="btn_search" Width="80" runat="server" Text="Refresh" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){
                        detailGrid.Refresh();
                    }" />
                                </dxe:ASPxButton>
                            </td>
                            <td  width="100">
                                <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                    Text="Save" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                    <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                                 }" />
                                </dxe:ASPxButton>

                            </td>

                            <td align="right" width="140">
                                <dxe:ASPxButton ID="btn_CreateJob" ClientInstanceName="btn_CreateJob" runat="server" Width="120" Text="Create JobNo" AutoPostBack="False"
                                    UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                    <ClientSideEvents Click="function(s, e) {
											if(cmb_Via.GetText() == '' )
											{
												alert('Please indicate storage option before creating the job no');
												return;
											}
											if(cmb_note15.GetText() == '' && (cmb_JobType.GetText()=='Inbound' || cmb_JobType.GetText()=='Outbound'))
											{
												alert('Please indicate FIDI option before creating the job no');
												return;
											}
                                                                        detailGrid.GetValuesOnCustomCallback('CreateJobNo',OnCloseCallBack);                 

                                        }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                       </table>
					
                        <table border=0 style="text-align: left; border-spacing: 0px; width:auto;width:960px;" cellpadding="0"  cellspacing="0">
                            
                            <tr style="height:5px">
                                <td colspan="8">
                                    <hr />
                                </td>
                            </tr>
                            <tr style="height:5px">
                                <td colspan="8">
								<table cellspacing=8 width=100% >
								<tr>
								<td valign=top><b>Print</b>:</td>
                                    <td  valign=top style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Local Move"||SafeValue.SafeString(Eval("JobType"),"")=="Office Move"||SafeValue.SafeString(Eval("JobType"),"")=="Outbound")?"":"none" %>">
                                    <a href='/ReportJob/PrintSiteSurveyNew.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">New Survey</a>
                                </td>
                                <td valign=top style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Local Move"||SafeValue.SafeString(Eval("JobType"),"")=="Office Move"||SafeValue.SafeString(Eval("JobType"),"")=="Outbound")?"":"none" %>">
                                    <a href='/ReportJob/PrintSiteSurvey.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Survey</a>
                                </td>
                                <td valign=top style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Local Move"||SafeValue.SafeString(Eval("JobType"),"")=="Office Move") ?"":"none" %>">
                                    <a href='/Custom/SurveyKit-lm.pdf?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Survey Kit</a>
                                </td>
                                <td valign=top style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound")?"":"none" %>">
                                    <a href='/Custom/SurveyKit-im.pdf?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Survey Kit</a>
                                </td>
                                <td valign=top>
                                    <a href='/ReportJob/PrintQuotation.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Quotation</a>
									<br>
                                    <a href='/ReportJob/PrintQuotationLong.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Quotation Long</a>
                                   
                                </td>

                                <td valign=top>
								       <a href='/ReportJob/PrintAccept.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Acceptance</a>
                             
								</td>
                                <td valign=top style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Local Move")?"block":"none" %>">
                                    <a href='/ReportJob/JobInstruction-LM.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Instr. To Crew</a>
                                </td>
                                <td style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Storage"||SafeValue.SafeString(Eval("JobType"),"")=="Office Move")?"":"none" %>">
                                    <a href='/ReportJob/JobInstruction-OM.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Job Instruction</a>
                                </td>
                                <td valign=top style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound")?"":"none" %>">
                                    <a href='/ReportJob/JobInstruction-OB.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Job Instruction</a>
                                </td>
                                <td valign=top>
                                    <a href='/ReportJob/SatisfactionForm.aspx?no=<%# Eval("QuotationNo") %>&type=<%# Eval("JobType") %>' target="_blank">Satisfaction Form</a>
                        
                                </td>
								<td align=right>
								<b>Created</b>: <%# Eval("CreateBy")%> @ <%# Eval("CreateDateTime","{0:dd/MMM HHmm}")%>
								<b>Updated</b>: <%# Eval("UpdateBy")%> @ <%# Eval("UpdateDateTime","{0:dd/MMM HHmm}")%>
								</td>
								
								</tr>
								</table>
                                </td>
                            </tr>
                            <tr style="height:5px">
                                <td colspan="8">
                                    <hr />
                                </td>
                            </tr>
							
                            <tr style="text-align: left;">
                                
                                <td style="width:160px;">
                                       <table style="width:100%;padding:0px;">
                                        <tr>
                                            <td>Survey</td>
                                            <td>
                                               <dxe:ASPxDateEdit ID="date_DateTime2" Width="130" runat="server" Value='<%# Eval("DateTime2") %>'
                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy#HH:mm">
                                        </dxe:ASPxDateEdit>
                                            </td>
                                      
                                            <td>
	                                        <dxe:ASPxComboBox ID="cmb_SurveyId" ClientInstanceName="cmb_SurveyId" runat="server" DataSourceID="dsSalesman" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Value='<%# Eval("Value2") %>' TextField="Code" ValueField="Code" Width="70">
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                            </Columns>
                                        </dxe:ASPxComboBox>

                                         
                                            </td>
											
                                      
                                            <td>Sales</td>
                                            <td>
	                                        <dxe:ASPxComboBox ID="cmb_SalesId" ClientInstanceName="cmb_SalesId" runat="server" DataSourceID="dsSalesman" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Value='<%# Eval("Value4") %>' TextField="Code" ValueField="Code" Width="70">
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                            </Columns>
                                        </dxe:ASPxComboBox>
										 
                                            </td>
                                     
                                            <td>C/S</td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="btn_BillingBy" ReadOnly="true" ClientInstanceName="btn_BillingBy" runat="server"
                                                    Width="80" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CreateBy") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            //PopupPerson(null,btn_BillingBy,'Accountant');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td style="display:none">Supervisor</td>
                                            <td style="display:none">
                                                <dxe:ASPxButtonEdit ID="btn_Supervisor" ClientInstanceName="btn_Supervisor" runat="server"
                                                    Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Value7") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPerson(null,btn_Supervisor,'Supervisor');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
											<td>Status</td>
											<td>
											                                       <dxe:ASPxComboBox ID="cmb_WorkStatus" runat="server" Width="100" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" OnCustomJSProperties="cmb_WorkStatus_CustomJSProperties" Value='<%# Eval("WorkStatus")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                <dxe:ListEditItem Text="Working" Value="Working" />
                                                <dxe:ListEditItem Text="Complete" Value="Complete" />
                                                <dxe:ListEditItem  Text="FOC" Value="FOC"/>
                                                <dxe:ListEditItem  Text="Cancel" Value="Cancel"/>
                                                <dxe:ListEditItem Text="Unsuccess" Value="Unsuccess" />
                                            </Items>
                                        </dxe:ASPxComboBox>
										
											</td>
											<td>
											 
											</td>
											<td>
											<div style="display:none;">
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Status"
                                            runat="server" ClientInstanceName="cmb_Status" OnCustomJSProperties="cmb_Status_CustomJSProperties" Text='<%# Eval("JobStage") %>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Survey" Value="Survey" />
                                                <dxe:ListEditItem Text="Costing" Value="Costing" />
                                                <dxe:ListEditItem Text="Quotation" Value="Quotation" />
                                                <dxe:ListEditItem Text="Schedule" Value="Schedule" />
                                                <dxe:ListEditItem Text="Billing" Value="Billing" />
                                                <dxe:ListEditItem Text="Close" Value="Close" />
                                            </Items>
                                        </dxe:ASPxComboBox>
											</div>
											</td>
 
                                        </tr>
                                    </table>
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
								                                <dxe:ASPxTextBox ID="txt_Quotation" ClientInstanceName="txt_Quotation" runat="server" Text='<%# Eval("QuotationNo") %>'></dxe:ASPxTextBox>
                            </div>
                            <table style="width:960px;" border=1 cellpadding=2 >

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
                                    </td>
                                     <td>JobType</td>
                                    <td>
                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="cmb_JobType" runat="server" Width="100%"  ClientInstanceName="cmb_JobType"  Text='<%# Eval("JobType")%>' OnCustomJSProperties="cmb_JobType_CustomJSProperties">
                                            <Items>
                                                <dxe:ListEditItem Text="Local Move" Value="Local Move" />
                                                <dxe:ListEditItem Text="Office Move" Value="Office Move" />
                                                <dxe:ListEditItem Text="OutBound" Value="OutBound" />
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
                                    <td>Customer</td>
                                    <td colspan="7">
                                        <table cellpadding="0" cellspacing="0">
										<tr>
                                            <td width="94">
                                                <dxe:ASPxButtonEdit ID="txt_CustomerId" ClientInstanceName="txt_CustomerId" runat="server"
                                                    Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CustomerId") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_CustomerId,txt_CustomerName,txt_Contact,txt_Tel,txt_Fax,txt_Email,txt_PostalCode,memo_Address,null,null,'');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="280"  Text='<%# Eval("CustomerName") %>' ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
                                                </dxe:ASPxTextBox>
                                            </td>
											</tr>
                                        </table>

									                                            <dxe:ASPxButtonEdit Visible="false" ID="btn_OriginPort" ClientInstanceName="btn_OriginPort" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("OriginPort") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                           PopupPort(null,btn_OriginPort);
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                     <dxe:ASPxButtonEdit Visible="false" ID="btn_DestinationPort" ClientInstanceName="btn_DestinationPort" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("DestinationPort") %>'>
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
                                    <td >Billing<br>Address</td>
                                    <td  colspan="3">
                                        <dxe:ASPxMemo ID="memo_Address" Rows="4" Width="100%" ClientInstanceName="memo_Address"
                                            runat="server" Text='<%# Eval("CustomerAdd") %>' >
                                        </dxe:ASPxMemo>
                                    </td>
                                      <td  >Origin<br>Address</td>
                                      <td colspan="3" >
                                          <dxe:ASPxMemo ID="memo_Address1" BackColor="LightGreen" Rows="4" Width="100%" runat="server" Text='<%# Eval("OriginAdd") %>'></dxe:ASPxMemo>
                                      </td>
                                    
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
                                        <dxe:ASPxTextBox ID="txt_Tel" runat="server" Width="100%" ClientInstanceName="txt_Tel" Text='<%# Eval("Tel") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
									  <td  rowspan=3>Destination<br>Address</td>
                                      <td colspan=3 rowspan=3>
                                          <dxe:ASPxMemo ID="memo_Address2" BackColor="LightGreen" Rows="4" Width="100%" runat="server" Text='<%# Eval("DestinationAdd") %>'></dxe:ASPxMemo>

										  <div style="display:none"> 
                                        <dxe:ASPxButtonEdit Visible="false" ID="btn_OriginCity" ClientInstanceName="btn_OriginCity" runat="server"
                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("OriginCity") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(btn_OriginCity,null)
                                                                    }" />
                                        </dxe:ASPxButtonEdit>

                                        <dxe:ASPxButtonEdit Visible="false" ID="btn_DestCity" ClientInstanceName="btn_DestCity" runat="server"
                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("DestCity") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(btn_DestCity,null)
                                                                    }" />
                                        </dxe:ASPxButtonEdit>

										</div>

										</td>
										</tr>
										
                                </tr>
                                <tr>
                                    <td>Email</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Email" Width="100%" runat="server" ClientInstanceName="txt_Email" Text='<%# Eval("Email") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                     <td>Fax</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Fax" runat="server" Width="100%" ClientInstanceName="txt_Fax" Text='<%# Eval("Fax") %>'>
                                        </dxe:ASPxTextBox>
                                        <dxe:ASPxTextBox Visible="false" runat="server" Width="120" ID="txt_OriginPostal" Text='<%# Eval("OriginPostal") %>' ClientInstanceName="txt_OriginPostal">
                                        </dxe:ASPxTextBox>
                                        <dxe:ASPxTextBox Visible="false" runat="server" Width="120" ID="txt_DestPostal" Text='<%# Eval("DestPostal") %>' ClientInstanceName="txt_DestPostal">
                                        </dxe:ASPxTextBox>
                                    </td>


                                </tr>

                                <tr>
                                      <td>Postal </td>
                                    <td>

                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_PostalCode" Text='<%# Eval("Postalcode") %>' ClientInstanceName="txt_PostalCode">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>PoNo</td>
                                    <td>
										<dxe:ASPxTextBox runat="server" Width="100%" ID="txt_PoNo" Text='<%# Eval("Note21") %>' ClientInstanceName="txt_PoNo">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
								 
                                            <tr>

                                                <td colspan="2" style="background-color: Gray; color: White; width: 240px">
                                                    <b>Moving Details</b>
                                                </td>
                                                <td colspan="2" style="background-color: Gray; color: White; width: 240px">
                                                    <b>Item Description</b>
                                                </td>
                                                <td colspan="2" style="background-color: Gray; color: White; width: 240px">
                                                    <b>Moving Schedule</b>
                                                </td>
                                                <td colspan="2" style="background-color: Gray; color: White; width: 240px">
                                                    <b>Storage Details</b>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>Volume</td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="memo_Volumn" BackColor="LightGreen" Width="100%" ClientInstanceName="memo_Volumn" Text='<%# Eval("VolumneRmk") %>'
                                                        runat="server">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td colspan="2" rowspan="3">
                                                    <dxe:ASPxMemo ID="memo_Description" Rows="3" Width="100%" ClientInstanceName="memo_Description" Height="70" Text='<%# Eval("ItemDes") %>'
                                                        runat="server">
                                                    </dxe:ASPxMemo>
                                                </td>
                                                <td style="width:90px;">Pack Date</td>
                                                <td>
                                                    <dxe:ASPxMemo BackColor="LightGreen" ID="txt_PackRemark" Rows="1" runat="server" Width="100%" Text='<%# Eval("PackRmk") %>'></dxe:ASPxMemo>
                                                </td>
                                                
                                                <td>Storage Requirement</td>
                                                <td>
                                                    <dxe:ASPxComboBox BackColor="LightBlue" EnableIncrementalFiltering="True" Width="100" ID="cmb_Via" ClientInstanceName="cmb_Via" Text='<%# Eval("ViaWh") %>'  runat="server">
                                                        <Items>
                                                            <dxe:ListEditItem Text="N/A" Value="N/A" />
                                                            <dxe:ListEditItem Text="Normal" Value="Normal" />
                                                            <dxe:ListEditItem Text="Aircon" Value="Aircon" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>

                                            </tr>
                                            <tr>

                                                <td>Nett Vol (Cuft)</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%" Value='<%# Eval("Volumne") %>'
                                                        DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Volumne" ClientInstanceName="spin_Volumne" Increment="0">
                                                    </dxe:ASPxSpinEdit>

                                                </td>
                                                
                                                 <td>Move Date</td>
                                                <td>
                                                    <dxe:ASPxMemo ID="txt_MoveRemark" Rows="1" runat="server" Width="100%" Text='<%# Eval("MoveRmk") %>'></dxe:ASPxMemo>
                                                </td>
                                                
                                                 <td>FOC Storage Days</td>
                                                <td >
                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" Value='<%# Eval("StorageFreeDays") %>' DecimalPlaces="0" ID="spin_StorageFreeDays" ClientInstanceName="spin_StorageFreeDays" Increment="0">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                              
                                            </tr>
                                            <tr>
                                                <td>No of Trip</td>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_TripNo" Text='<%# Eval("TripNo") %>' ClientInstanceName="txt_TripNo">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                 <td>Move Completion Date</td>
                                                <td>
                                               <dxe:ASPxDateEdit ID="date_MoveDate" Width="160" runat="server" Value='<%# Eval("MoveDate") %>'
                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                                        </dxe:ASPxDateEdit>
												
                                                </td>
                                                 
												
                                                <td>Storage Begin</td>
                                                <td>
                                                    <dxe:ASPxDateEdit ID="date_StorageStartDate" Width="100" runat="server" Value='<%# Eval("StorageStartDate") %>'
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
                                                </td>
 
                                            </tr>

                                            <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                              <td colspan="8" style="background-color: Gray; color: White;">
                                                    <b>International Move</b>
                                                </td>
                                            </tr>


                                            <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">

                                                <td>Mode </td>


                                                <td>

                                                    <dxe:ASPxComboBox ID="cmb_Mode" BackColor="LightGreen" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("Mode")%>'>
                                                        <Items>
                                                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                                                            <dxe:ListEditItem Text="20FT" Value="20FT" />
                                                            <dxe:ListEditItem Text="40FT" Value="40FT" />
                                                            <dxe:ListEditItem Text="40HC" Value="40HC" />
                                                            <dxe:ListEditItem Text="20CON" Value="20CON" />
                                                            <dxe:ListEditItem Text="40CON" Value="40CON" />
                                                            <dxe:ListEditItem  Text="AIR" Value="AIR"/>
                                                            <dxe:ListEditItem Text="ROAD" Value="ROAD" />
                                                            <dxe:ListEditItem  Text="SEA" Value="SEA"/>
                                                            <dxe:ListEditItem  Text="SEA/AIR" Value="SEA/AIR"/>
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
												<td>Delivery Date</td>
												<td>
										<dxe:ASPxDateEdit ID="date_EtaDest" Width="100" runat="server" Value='<%# Eval("EtaDest") %>'
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
												</td>		
												<td>Vessel/Voy</td>
												<td>
                                                    <dxe:ASPxTextBox BackColor="Yellow" runat="server" Width="120" ID="txt_note17" Text='<%# Eval("Note17") %>'>
                                                    </dxe:ASPxTextBox>
												</td>		
												<td>Carrier</td>
												<td>
                                                    <dxe:ASPxTextBox runat="server" Width="120" ID="txt_note18" Text='<%# Eval("Note18") %>'>
                                                    </dxe:ASPxTextBox>
												</td>		
                                              </tr>
                                            <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                                <td>Service </td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cmb_ServiceType" runat="server" Width="120" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("ServiceType")%>'>
                                                        <Items>
                                                            <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                                            <dxe:ListEditItem Text="Door to Door" Value="Door to Door" />
                                                            <dxe:ListEditItem Text="Door to Port" Value="Door to Port" />
                                                            <dxe:ListEditItem Text="Port to Door" Value="Port to Door" />
                                                            <dxe:ListEditItem Text="Origin Services" Value="Origin Services" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td>Port of Entry </td>
                                                <td>
                                                    <dxe:ASPxButtonEdit BackColor="LightGreen" ID="btn_PortOfEntry" ClientInstanceName="btn_PortOfEntry" runat="server" MaxLength="5"
                                                        Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("EntryPort") %>'>
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPort(btn_PortOfEntry,null);
                                                                    }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>Eta Singapore</td>
                                                <td>
                                                    <dxe:ASPxDateEdit BackColor="Yellow" ID="date_Etd" Width="120" runat="server" Value='<%# Eval("Etd") %>'
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                                <td>Eta Dest.</td>
                                                <td>
                                                    <dxe:ASPxDateEdit ID="date_Eta" Width="120" runat="server" Value='<%# Eval("Eta") %>'
                                                        EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
                                                </td>
												
												 	
												</tr>
                                <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                    <td>D.Agent</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_note14" Text='<%# Eval("Note14") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>FIDI</td>
                                    <td>
                                        <dxe:ASPxComboBox BackColor="LightBlue"  EnableIncrementalFiltering="True" Width="100" ID="cmb_note15" ClientInstanceName="cmb_note15" Text='<%# Eval("Note15") %>' runat="server">
                                            <Items>
                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                <dxe:ListEditItem Text="No" Value="No" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>

                                    <td>Container No</td>
                                    <td>
                                        <dxe:ASPxTextBox BackColor="Yellow" runat="server" Width="120" ID="txt_note19" Text='<%# Eval("Note19") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Seal No</td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_note20" Text='<%# Eval("Note20") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>

                                <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                     
                                    <td colspan="2">All Risk Transit Insurance</td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox BackColor="LightGreen" runat="server" Width="100%" ID="txt_Note27" Text='<%# Eval("Note27") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td colspan="2">Estimated Pack Days</td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note22" Text='<%# Eval("Note22") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>

                                </tr>
                                  <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                    <td colspan="2">Pairs & Sets Coverage</td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note28" Text='<%# Eval("Note28") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                      <td colspan="2">Shipment Mode / Type</td>
                                      <td colspan="2">
                                          <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note23" Text='<%# Eval("Note23") %>'>
                                          </dxe:ASPxTextBox>
                                      </td>

                                </tr>
                                <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                    <td colspan="2">Electrical/Mechanical Derangement Coverage</td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note29" Text='<%# Eval("Note29") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td colspan="2">Vessel Frequency</td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note24" Text='<%# Eval("Note24") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                     
                                      <td colspan="2">Insurance Storage Extension</td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note30" Text='<%# Eval("Note30") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    
                                     <td colspan="2">Transit Time</td>
                                     <td colspan="2">
                                         <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note25" Text='<%# Eval("Note25") %>'>
                                         </dxe:ASPxTextBox>
                                     </td>
                                </tr>
                                <tr style="display: <%# (SafeValue.SafeString(Eval("JobType"),"")=="Outbound"||SafeValue.SafeString(Eval("JobType"),"")=="Inbound"||SafeValue.SafeString(Eval("JobType"),"")=="Air")?"table-row": "none" %>">
                                    <td colspan="2"></td>
                                     <td colspan="2"></td>
                                    <td colspan="2">Estimated Duration of Custom Clearance</td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox runat="server" BackColor="LightGreen" Width="100%" ID="txt_Note26" Text='<%# Eval("Note26") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
								<div style="height:8px;"></div>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="960px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Name="Survery" Text="Survey" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl5" runat="server">
                                                <table style="width:930px" cellpadding="2">
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
                                                        <td colspan="2">Piano Details</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_PainoDetails" ClientInstanceName="txt_PainoDetails" Text='<%# Eval("ItemDetail4") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Charges S$</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%" Value='<%# Eval("ItemPrice4") %>'
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Charges1" ClientInstanceName="spin_Charges1" Increment="0">
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
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Charges2" ClientInstanceName="spin_Charges2" Increment="0" Value='<%# Eval("ItemPrice6") %>'>
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
                                                                    <dxe:ListEditItem Text="N/A" Value="N/A" />
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
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Charges3" ClientInstanceName="spin_Charges3" Increment="0" Value='<%# Eval("ItemPrice7") %>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>

                                                    </tr>
                                                    <tr style="display: <%# SafeValue.SafeString(Eval("JobType"),"")=="Office Move"?"none": "table-row" %>">
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

                                                                    <dxe:ListEditItem Text="N/A" Value="N/A" />
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
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Charges4" ClientInstanceName="spin_Charges4" Increment="0" Value='<%# Eval("ItemPrice8") %>'>
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
                                                                    <dxe:ListEditItem Text="N/A" Value="N/A" />
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
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Charges5" ClientInstanceName="spin_Charges5" Increment="0" Value='<%# Eval("ItemPrice9") %>'>
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
                                                                    <dxe:ListEditItem Text="N/A" Value="N/A" />
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
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Charges6" ClientInstanceName="spin_Charges6" Increment="0" Value='<%# Eval("ItemPrice10") %>'>
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
                                                                    <dxe:ListEditItem Text="N/A" Value="N/A" />
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
                                                                DisplayFormatString="0.00" DecimalPlaces="2" ID="spin_Charges7" ClientInstanceName="spin_Charges7" Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr style="text-align:left; vertical-align:top">
                                                        <td  colspan="2">Notes</td>
                                                        <td colspan="8">
                                                             <dxe:ASPxMemo ID="memo_Notes" Rows="4" Width="100%" ClientInstanceName="memo_Notes" Height="70" Text='<%# Eval("Notes") %>'
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
                                                                                <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false"  Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                                    <dxwgv:GridViewDataTextColumn Caption="Note" FieldName="ItemNote" VisibleIndex="6" Width="200">
                                                    </dxwgv:GridViewDataTextColumn>
													<dxwgv:GridViewDataTextColumn Caption="Type" FieldName="ItemType" VisibleIndex="6" Width="80">
                                                    </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="ItemType" VisibleIndex="6" Width="80">
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
                                                            <dxe:ASPxLabel ID="lb_CostMode" runat="server" Text="Costing Mode" ClientVisible='<%# SafeValue.SafeString(Eval("JobType")).ToUpper().Contains("OUTBOUND") %>'></dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_CostMode" runat="server" ClientInstanceName="cmb_CostMode" ClientVisible='<%# SafeValue.SafeString(Eval("JobType")).ToUpper().Contains("OUTBOUND") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="LCL" Value="LCL" Selected="true" />
                                                                    <dxe:ListEditItem Text="20FT" Value="20FT" />
                                                                    <dxe:ListEditItem Text="40FT" Value="40FT" />
                                                                    <dxe:ListEditItem Text="40HC" Value="40HC" />
                                                                    <dxe:ListEditItem Text="20CON" Value="20CON" />
                                                                    <dxe:ListEditItem Text="40CON" Value="40CON" />
                                                                    <dxe:ListEditItem Text="AIR" Value="AIR" />
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
                                                    DataSourceID="dsCosting" KeyFieldName="SequenceId" Width="900" OnBeforePerformDataSelect="grid_Cost_DataSelect" OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
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
                                                                            <dxe:ASPxButton ID="btn_CostDetEdit" runat="server" Text="Open" Width="50" AutoPostBack="false" OnInit="btn_CostEdit_Init">
                                                                            </dxe:ASPxButton>
                                                                            <dxe:ASPxTextBox runat="server" ID="txt_Id" OnInit="txt_Id_Init" ClientVisible="false" Text='<%# Eval("SequenceId") %>'></dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CostUpdate" runat="server" Text="Update" Width="50" AutoPostBack="false"
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
                                                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Mode" FieldName="JobNo" Width="20" VisibleIndex="1" SortIndex="0" SortOrder="Descending">
                                                            <DataItemTemplate><%# Eval("RefType","{0}") == "Inbound" ? "Inbound" : Eval("JobNo") %></DataItemTemplate>
															<EditItemTemplate><%# Eval("RefType","{0}") == "Inbound" ? "Inbound" : Eval("JobNo") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="#" FieldName="CostIndex" Width="20" VisibleIndex="1" SortIndex="0" SortOrder="Descending">
                                                            <EditItemTemplate><%#Eval("CostIndex") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Rev" FieldName="Version" Width="20" VisibleIndex="1" SortIndex="1" SortOrder="Descending">
                                                            <EditItemTemplate><%#Eval("Version") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" Width="200" VisibleIndex="2" EditCellStyle-Paddings-Padding="0">
                                                        </dxwgv:GridViewDataTextColumn>

                                                        <dxwgv:GridViewDataColumn Caption="OS" FieldName="Amt1" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amt1") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amt1") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="FRT" FieldName="Amt2" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amt2") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amt2") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="DS" FieldName="Amt3" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amt3") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amt3") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="MIS" FieldName="Amt4" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amt4") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amt4") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Profit" FieldName="Profitmargin" Width="150" VisibleIndex="3">
                                                            <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="2" DisplayFormatString="0.00"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Other" FieldName="Amount2" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amount2") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amount2") %><dxe:ASPxTextBox runat="server" Text='<%#Bind("Amount2") %>' ClientVisible="false" ID="txt_Amount2"></dxe:ASPxTextBox>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Total" FieldName="Amount" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#Eval("Amount") %></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Amount") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Gross Profit" FieldName="Amount" VisibleIndex="3" Width="50">
                                                            <DataItemTemplate><%#SafeValue.SafeDecimal(Eval("Amount"),0)-SafeValue.SafeDecimal(Eval("Amount2"),0) %></DataItemTemplate>
                                                            <EditItemTemplate><%#SafeValue.SafeDecimal(Eval("Amount"),0)-SafeValue.SafeDecimal(Eval("Amount2"),0) %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Create User/Time" FieldName="CreateBy" Width="90" VisibleIndex="98">
                                                            <DataItemTemplate>
                                                                <%#SafeValue.SafeString(Eval("CreateBy"))+" - "+SafeValue.SafeString(Eval("CreateDateTime","{0:dd/MM HH:mm}")) %>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Status" Caption="Status" Width="80" VisibleIndex="5">
															<DataItemTemplate><div style="<%# Eval("Status","{0}")!="VOID" ? "" : "background:red" %>"><%#Eval("Status") %></div></DataItemTemplate>
                                                            <EditItemTemplate><%#Eval("Status") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>

                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Quotation" Visible="true" Name="Quotation">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl10" runat="server">

											<table width=900>
											<tr>
											<td>Quotation Date</td>
											<td>
                                              <dxe:ASPxDateEdit ID="date_DateTime1" Width="100" runat="server" Value='<%# Eval("DateTime1") %>'
                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
 
											</td>
											<td>Currency/ExRate</td>
	                                        <td>
										<dxe:ASPxComboBox visible="false" ID="cmb_PayTerm" runat="server" Width="150" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("PayTerm")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="CASH" Value="CASH" />
                                                <dxe:ListEditItem Text="30DAYS" Value="30DAYS" />
                                                <dxe:ListEditItem Text="60DAYS" Value="60DAYS" />
                                                <dxe:ListEditItem Text="90DAYS" Value="90DAYS" />
                                                <dxe:ListEditItem Text="120DAYS" Value="120DAYS" />
                                            </Items>
                                        </dxe:ASPxComboBox>
										</td>
												<td>
											<dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Eval("Currency") %>' runat="server" Width="60" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
												</td>

										<td>
                                        <dxe:ASPxSpinEdit ID="spin_ExRate" ClientInstanceName="spin_ExRate" DisplayFormatString="0.00000000"  
                                            runat="server" Width="90" Value='<%# Eval("ExRate")%>' DecimalPlaces="6" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
										</td>
										                                    <td>Quote Validity</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_ExpiryDate" Width="100" runat="server" Value='<%# Eval("ExpiryDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
									  <td>Print Total</td>
                                                <td>
                                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="70" ID="cmb_PrintTotal" Text='<%# Eval("Note16") %>'  runat="server">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                            <dxe:ListEditItem Text="No" Value="No" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>

											<td width="80">&nbsp;
											</td>

											<td align=right>
                                           <dxe:ASPxButton ID="ASPxButton13x" Width="120" runat="server" Text="Add New Item"                                                 
                                                AutoPostBack="false" UseSubmitBehavior="false" >
                                                <ClientSideEvents Click="function(s,e) {
                                                       grid_ref_Cont.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>
											</td>
											</tr>
											</table>
                                            <dxwgv:ASPxGridView ID="Grid_RefCont" ClientInstanceName="grid_ref_Cont" runat="server"
                                                KeyFieldName="SequenceId" DataSourceID="dsJobCost" Width="900" OnBeforePerformDataSelect="Grid_RefCont_DataSelect"
                                                OnRowUpdating="Grid_RefCont_RowUpdating" OnRowInserting="Grid_RefCont_RowInserting"
                                                OnRowDeleting="Grid_RefCont_RowDeleting" OnInitNewRow="Grid_RefCont_InitNewRow"
                                                OnInit="Grid_RefCont_Init">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_del" runat="server" 
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_ref_Cont.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.UpdateEdit()() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.CancelEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataMemoColumn Caption="Description" Width="300" FieldName="ChgCodeDe" VisibleIndex="2" PropertiesMemoEdit-Rows="8">
                                                    </dxwgv:GridViewDataMemoColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Amount" Width="100" FieldName="SalePrice" VisibleIndex="2" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataMemoColumn Caption="Charge Remark" Width="300" PropertiesMemoEdit-Rows="8" FieldName="Remark" VisibleIndex="2" >
                                                    </dxwgv:GridViewDataMemoColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn
                                                        Caption="Charge Code" FieldName="ChgCode" VisibleIndex="1">
                                                        <PropertiesComboBox
                                                            ValueType="System.String" TextFormatString="{0}" DataSourceID="dsChgCode"
                                                            TextField="ChgCodeDe" EnableIncrementalFiltering="true"
                                                            ValueField="ChgCodeId" DropDownWidth="100">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="ChgCodeId" Width="70px" />
                                                                <dxe:ListBoxColumn FieldName="ChgCodeDe" Width="100%" />
                                                            </Columns>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                </Columns>
                                                <SettingsEditing Mode="InLine" />
                                            </dxwgv:ASPxGridView>
 
<br><br>

<dxe:ASPxButton ID="ASPxButton13xx" Width="120" runat="server" Text="Add Revise Quote"                                                 
                                                AutoPostBack="false" UseSubmitBehavior="false" >
                                                <ClientSideEvents Click="function(s,e) {
                                                       grid_ref_Cont_rev.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>                                            
<dxwgv:ASPxGridView ID="Grid_RefContRev" ClientInstanceName="grid_ref_Cont_rev" runat="server"
                                                KeyFieldName="SequenceId" DataSourceID="dsJobCostRev" Width="900" OnBeforePerformDataSelect="Grid_RefContRev_DataSelect"
                                                OnRowUpdating="Grid_RefContRev_RowUpdating" OnRowInserting="Grid_RefContRev_RowInserting"
                                                OnRowDeleting="Grid_RefContRev_RowDeleting" OnInitNewRow="Grid_RefContRev_InitNewRow"
                                                OnInit="Grid_RefContRev_Init">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                <dxe:ASPxButton ID="btn_search" Width="80" runat="server" Text="Open Quote" AutoPostBack="false"
								 ClientSideEvents-Click='<%# "function(s) { OnQuoteEditClick(\""+Eval("RefNo","{0}")+"\",\""+Eval("JobNo","{0}")+"\") }"  %>'
								/>
                                																</td>
																	<td>
                                                                        <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont_rev.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_del" runat="server" 
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_ref_Cont_rev.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
																	
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont_rev.UpdateEdit()() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont_rev.CancelEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
						     <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="RefDate" VisibleIndex="2" Width="100px">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>                                                    
                                                    <dxwgv:GridViewDataTextColumn Caption="Description" Width="300" FieldName="Remark" VisibleIndex="2" >
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Amount" Width="100" FieldName="SalePrice" VisibleIndex="2" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true">
							<DataItemTemplate>
								<%#  R.Amount(D.One("select sum(saleprice) from jobcost where refno='"+Eval("RefNo","{0}")+"' and jobno='"+Eval("JobNo","{0}")+"' ")) %>
							</DataItemTemplate>
							<EditItemTemplate>
							</EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Rev" Width="90" FieldName="JobNo" VisibleIndex="2" >
							<EditItemTemplate>
							</EditItemTemplate>
													
                                                    </dxwgv:GridViewDataTextColumn>
                                                 <dxwgv:GridViewDataTextColumn Caption="Print" Width="90" FieldName="JobNo" VisibleIndex="2" >

<DataItemTemplate>
                             <a href='/ReportJob/PrintQuotationLong.aspx?no=<%# Eval("RefNo") %>&rev=<%# Eval("JobNo") %>' target="_blank">Print</a>
							</DataItemTemplate>

												 <EditItemTemplate>
							</EditItemTemplate>
													
                                                    </dxwgv:GridViewDataTextColumn>
 
     
                                                </Columns>
                                                <SettingsEditing Mode="InLine" />
                                            </dxwgv:ASPxGridView>
 
<br><br>

											<table style="width:900px;">
                                                   <tr>
                                                        <td  style="background-color: Gray; color: White; ">
                                                            <b>COVER LETTER:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention1"  runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention1") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="true" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
 
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>ORIGIN SERVICES:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <dxe:ASPxHtmlEditor ID="txt_Attention7"  runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention7") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="true" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>OTHER SERVICES:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <dxe:ASPxHtmlEditor ID="txt_Attention2" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention2") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="true" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
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
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention3" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention3") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="true" AllowHtmlView="false" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">
                                                            <b>INSURANCE REMARKS:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxHtmlEditor ID="txt_Attention4" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention4") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="true" AllowHtmlView="true" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>

                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>PAYMENT TERM REMARKS:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <dxe:ASPxHtmlEditor ID="txt_Attention5" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention5") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="true" AllowHtmlView="true" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>IMPORTANT NOTE:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <dxe:ASPxHtmlEditor ID="txt_Attention6" runat="server" SettingsHtmlEditing-AllowScripts="false" Settings-AllowContextMenu="False"  Height="320" Width="100%" Html='<%# Eval("Attention6") %>'>
                                                               <Settings  AllowDesignView="true" AllowContextMenu="true" AllowHtmlView="true" AllowPreview="false" AllowInsertDirectImageUrls="false"/>       
                                                                                      
                                                            </dxe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                                                <dxe:ASPxButton ID="btn_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false"  Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                                                      <td>  </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit visible="false" ID="date_McstDate1" Width="150" runat="server" Value='<%# Bind("McstDate1") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td> </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit visible="false" ID="date_McstDate2" Width="150" runat="server" Value='<%# Bind("McstDate2") %>'
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
                                                                     
                                                                     <td></td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox visible="false" runat="server" Width="100%" ID="txt_States1"
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
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
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
                                    <dxtc:TabPage Text="Haulier Container" Visible="true" >
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl12" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddSch" Width="180" runat="server" Text="New Haulier Container" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed"&&SafeValue.SafeString(Eval("JobNo"),"").Length>0 %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                grid_sch.AddNewRow();
                                                                    }" />
                                        </dxe:ASPxButton>
                                                        </td>
                                                         <td>
                                                            <dxe:ASPxButton ID="ASPxButton11" runat="server" Text="Refresh" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                   grid_sch.Refresh();
                                                                 }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_sch" ClientInstanceName="grid_sch" runat="server" OnInit="grid_sch_Init" OnInitNewRow="grid_sch_InitNewRow" 
                                                    OnRowInserting="grid_sch_RowInserting" OnRowUpdating="grid_sch_RowUpdating"
                                                     DataSourceID="dsHaulierCont" OnBeforePerformDataSelect="grid_sch_BeforePerformDataSelect" OnRowDeleting="grid_sch_RowDeleting"
                                                    KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_sch_CustomDataCallback">
                                                    <SettingsBehavior  ConfirmDelete="true"/>
                                                    <SettingsEditing Mode="EditForm"/>
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_sch.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%#SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_sch.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Note1" VisibleIndex="4" Visible="true">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Note2" Caption="ContainerType" VisibleIndex="5" Visible="true">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Note3" Caption="Lessor" VisibleIndex="6" Visible="true">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="DateTime1" Caption="OnHireDateTime" VisibleIndex="7" Visible="true">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="DateTime2" Caption="CommissionDate" VisibleIndex="8" Visible="true">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="Note4" Caption="Category" VisibleIndex="9" Visible="true">
                                                        </dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                  <Templates>
                                                      <EditForm>
                                                          <div style="padding: 2px 2px 2px 2px">
                                                              <div style="display: none">
                                                                  <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>'>
                                                                  </dxe:ASPxTextBox>
                                                              </div>
                                                              <div>
                                                                  <table>
                                                                      <tr>
                                                                          <td>Container No
                                                                          </td>
                                                                          <td colspan="2">
                                                                              <dxe:ASPxTextBox ID="txt_ContainerNo" runat="server" Width="165" Text='<%# Bind("Note1") %>'>
                                                                              </dxe:ASPxTextBox>
                                                                          </td>
                                                                          <td>Commission Date
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxDateEdit ID="date_CommissionDate" runat="server" Value='<%# Bind("DateTime2") %>' Width="165" DisplayFormatString="dd/MM/yyyy">
                                                                              </dxe:ASPxDateEdit>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Container Category
                                                                          </td>
                                                                          <td colspan="2">
                                                                              <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_ContainerCategory"
                                                                                  DataSourceID="dsContainerCategory" TextField="Code" ValueField="Code" Width="165" Value='<%# Eval("Note4")%>'>
                                                                              </dxe:ASPxComboBox>
                                                                          </td>
                                                                          <td>Lessor
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxTextBox ID="txt_Lessor" runat="server" Width="165" Text='<%# Bind("Note3") %>'>
                                                                              </dxe:ASPxTextBox>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>On Hire Date
                                                                          </td>
                                                                          <td colspan="2">
                                                                              <dxe:ASPxDateEdit ID="date_OnHireDate" Width="165" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm" Value='<%# Bind("DateTime1") %>'>
                                                                                  <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                                                                              </dxe:ASPxDateEdit>
                                                                          </td>
                                                                          <td>Off Hire Date
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxDateEdit ID="date_OffHireDate" Width="165" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm" Value='<%# Bind("DateTime3") %>'>
                                                                                  <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                                                                              </dxe:ASPxDateEdit>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Date of Manufacture
                                                                          </td>
                                                                          <td colspan="2">
                                                                              <dxe:ASPxDateEdit ID="date_Manufacture" runat="server" Value='<%# Bind("DateTime4") %>' Width="165">
                                                                              </dxe:ASPxDateEdit>
                                                                          </td>
                                                                          <td>Manufacturer
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxTextBox ID="txt_Manufacturer" runat="server" Width="165" Text='<%# Bind("Value4") %>'>
                                                                              </dxe:ASPxTextBox>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>CSC Plate No.
                                                                          </td>
                                                                          <td colspan="2">
                                                                              <dxe:ASPxTextBox ID="txt_PlateNo" runat="server" Width="165" Text='<%# Bind("Value4") %>'>
                                                                              </dxe:ASPxTextBox>
                                                                          </td>
                                                                          <td>Container Type
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_ContainerType"
                                                                                  DataSourceID="dsContainerType" TextField="ContainerType" ValueField="ContainerType" Width="165" Value='<%# Eval("Note2")%>'>
                                                                              </dxe:ASPxComboBox>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>External Length
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_ExternalLength" Increment="0" runat="server" Value='<%# Bind("ItemPrice1") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>mm</td>
                                                                          <td>External Width
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_ExternalWidth" Increment="0" runat="server" Value='<%# Bind("ItemPrice2") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>mm</td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>External Height
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_ExternalHeight" Increment="0" runat="server" Value='<%# Bind("ItemPrice3") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>mm</td>
                                                                          <td>Internal Length
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_InternalLength" Increment="0" runat="server" Value='<%# Bind("ItemPrice4") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>mm</td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Internal Width
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_InternalBreadth" Increment="0" runat="server" Value='<%# Bind("ItemPrice5") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>mm</td>
                                                                          <td>Internal Height
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_InternalHeight" Increment="0" runat="server" Value='<%# Bind("ItemPrice6") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>mm</td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Material
                                                                          </td>
                                                                          <td colspan="2">
                                                                              <dxe:ASPxTextBox ID="txt_Material" runat="server" Width="165" Text='<%# Bind("Note5") %>'>
                                                                              </dxe:ASPxTextBox>
                                                                          </td>
                                                                          <td>External coat
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxTextBox ID="txt_ExternalCoat" runat="server" Width="165" Text='<%# Bind("Note6") %>'>
                                                                              </dxe:ASPxTextBox>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Capacity
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_Capacity" Increment="0" runat="server" Value='<%# Bind("Note7") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>cum</td>
                                                                          <td>Maximum gross weight
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_MaxGrossWeight" Increment="0" runat="server" Value='<%# Bind("ItemPrice7") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>kg</td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Tare Weight(±2%)
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_TareWeight" Increment="0" runat="server" Value='<%# Bind("ItemPrice8") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>kg</td>
                                                                          <td>Max PayLoad
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_MaxPayload" Increment="0" runat="server" Value='<%# Bind("ItemPrice9") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>kg</td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Testing Pressure
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_TestPress" Increment="0" runat="server" Value='<%# Bind("ItemPrice10") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.00" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>Bars</td>
                                                                          <td>Shell Thickness
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxSpinEdit ID="spin_Thickness" Increment="0" runat="server" Value='<%# Bind("ItemPrice11") %>'
                                                                                  SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.00" Width="165">
                                                                              </dxe:ASPxSpinEdit>
                                                                          </td>
                                                                          <td>mm</td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>InActive</td>
                                                                          <td>
                                                                              <dxe:ASPxComboBox ID="cbb_InActive" runat="server" Width="165" TextField="Code" ValueField="Code" Value='<%# Bind("JobStatus") %>'>
                                                                                  <Items>
                                                                                      <dxe:ListEditItem Value="Use" Text="Use" />
                                                                                      <dxe:ListEditItem Value="InActive" Text="InActive" />
                                                                                  </Items>
                                                                              </dxe:ASPxComboBox>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td>Approvals
                                                                          </td>
                                                                          <td>
                                                                              <dxe:ASPxMemo ID="txt_Approvals" Rows="3" Width="165" ClientInstanceName="txt_Approvals"
                                                                                  runat="server" Text='<%# Bind("Note8") %>'>
                                                                              </dxe:ASPxMemo>
                                                                          </td>
                                                                      </tr>
                                                                      <tr>
                                                                          <td colspan="4">
                                                                              <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                                  <div style="display: none">
                                                                                      <a href="#" onclick="javascript:detailGrid.GetValuesOnCustomCallback('Save',showCallBack);">Update</a>
                                                                                  </div>
                                                                                  <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                      runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                                  <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                      runat="server"></dxwgv:ASPxGridViewTemplateReplacement>

                                                                              </div>
                                                                          </td>
                                                                      </tr>
                                                                  </table>
                                                              </div>
                                                          </div>
                                                      </EditForm>
                                                </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Tpt Job">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table style="padding: 2px 2px 2px 2px">
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_CloseJob" Width="100" runat="server" Text="Add Tpt Job" AutoPostBack="False"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"USE")!="Closed" %>' UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                    grid_Transport.AddNewRow();
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_VoidMaster" ClientInstanceName="btn_VoidMaster" runat="server" Width="100" Text="Refresh" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"USE")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                                                    grid_Transport.Refresh();
                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="grid_Transport" runat="server"
                                                    KeyFieldName="Id" Width="950px" AutoGenerateColumns="False" DataSourceID="dsTransport"
                                                    OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnBeforePerformDataSelect="grid_Transport_BeforePerformDataSelect"
                                                    OnRowInserting="grid_Transport_RowInserting" OnRowUpdating="grid_Transport_RowUpdating">
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="40">
                                                          <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Transport.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%#SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Transport.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="No" FieldName="JobNo" VisibleIndex="1" SortIndex="0" SortOrder="Descending" Width="70">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Cust" FieldName="CustomerName" UnboundType="String" VisibleIndex="3" Width="200">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Eta" FieldName="Eta" VisibleIndex="5" Width="60">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yy}" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Etd" FieldName="Etd" VisibleIndex="5" Width="60">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yy}" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Vessel" VisibleIndex="6" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Voyage" FieldName="Voyage" VisibleIndex="6" Width="60">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Pol" FieldName="Pol" VisibleIndex="6" Width="60">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Pod" FieldName="Pod" VisibleIndex="7" Width="60">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Charges" VisibleIndex="8" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volumne" VisibleIndex="9" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="StorageFreeDays" VisibleIndex="10" Width="55" PropertiesTextEdit-DisplayFormatString="0.000">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobStatus" Width="55" VisibleIndex="11">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="padding: 2px 2px 2px 2px">
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                                        BackColor="Control" Text='<%# Eval("Id") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <table style="padding: 2px 2px 2px 2px; display: normal;width:100%">
                                                                    <tr>
                                                                        <td style="width:70%"></td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="ASPxButton21" Width="110" runat="server" Text="Save" AutoPostBack="false"
                                                                                Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"USE")!="Closed" %>' UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s,e) {
                                    grid_Transport.UpdateEdit();
                                    }" />
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                         <td>
                                                                            <dxe:ASPxButton ID="ASPxButton23" Width="110" runat="server" Text="Cancel" AutoPostBack="false"
                                                                                Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"USE")!="Closed" %>' UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s,e) {
                                    grid_Transport.CancelEdit();
                                    }" />
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                                            <dxe:ASPxComboBox ID="cmb_JobType" Width="100%" runat="server" Text='<%# Eval("JobStage") %>'>
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
                                                                            <dxe:ASPxButtonEdit ID="btn_Cust" ClientInstanceName="btn_Cust" runat="server" Text='<%# Eval("CustomerId") %>'>
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
                                                                                runat="server" Text='<%# Eval("CustomerName") %>' />
                                                                        </td>
                                                                        <td>Cust. Job No
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_BkgRef" Width="100%" runat="server" Text='<%# Eval("RefNo") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>PIC
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_CustPic" ClientInstanceName="txt_CustPic" Width="100%" runat="server" Text='<%# Eval("Contact") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Email
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_CustEmail" ClientInstanceName="txt_CustEmail" Width="100%" runat="server" Text='<%# Eval("Email") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Doc No
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_CustDocNo" Width="100%" runat="server" Text='<%# Eval("Remark1") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Doc Type
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="txt_CustDocType" Width="100%" DropDownStyle="DropDown" runat="server" Text='<%# Eval("Remark2") %>'>
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
                                                                            <dxe:ASPxTextBox ID="txt_BlRef" Width="100%" runat="server" Text='<%# Eval("Hbl") %>'>
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
                                                                            <dxe:ASPxDateEdit ID="date_BkgDate" runat="server" Width="100%" Value='<%# Eval("DateTime1") %>'
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td>Time
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_BkgTime" Width="100%" runat="server" Text='<%# Eval("DateTime2") %>'>
                                                                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Remark
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <dxe:ASPxTextBox ID="txt_JobRmk" Width="100%" runat="server" Text='<%# Eval("Remark") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Weight
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_BkgWt" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                                Value='<%# Eval("Charges") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Volume
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_BkgM3" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                                Value='<%# Eval("Volumne") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Qty
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_BkgQty" runat="server" Width="100%" Increment="0" Number="0"
                                                                                NumberType="Integer" Value='<%# Eval("StorageFreeDays") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>PackType
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="txt_BkgPackType" ClientInstanceName="txt_BkgPackType" runat="server" Text='<%# Eval("ItemValue1") %>' Width="100%">
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
                                                                            <dxe:ASPxMemo ID="txt_cargoMkg" Rows="4" Width="100%" runat="server" Text='<%# Eval("Note1") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                        <td>Description
                                                                        </td>
                                                                        <td colspan="6">
                                                                            <dxe:ASPxMemo ID="txt_cargoDes" Rows="4" Width="99%" runat="server" Text='<%# Eval("Note2") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <a href="#" onclick="PopupPartyAdr(null,txt_PickupFrm1);">Pick From</a>
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <dxe:ASPxMemo ID="txt_PickupFrm1" ClientInstanceName="txt_PickupFrm1" Rows="4" Width="100%" runat="server" Text='<%# Eval("CollectFrom") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                        <td>
                                                                            <a href="#" onclick="PopupPartyAdr(null,txt_DeliveryTo1);">Delivery To</a>
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <dxe:ASPxMemo ID="txt_DeliveryTo1" ClientInstanceName="txt_DeliveryTo1" Rows="4" Width="100%" runat="server" Text='<%# Eval("DeliveryTo") %>'>
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
                                                                            <dxe:ASPxDateEdit ID="date_TptDate" runat="server" Width="100%" Value='<%# Eval("DateTime3") %>'
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td>Time
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_TptTime" Width="100%" runat="server" Text='<%# Eval("DateTime4") %>'>
                                                                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>TPT&nbsp;Status</td>
                                                                        <td width="150">
                                                                            <dxe:ASPxComboBox ID="cmb_JobStatus" Width="100%" runat="server" Text='<%# Eval("JobStatus") %>'>
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
                                                                            <dxe:ASPxComboBox ID="cmb_TptType" Width="100%" runat="server" Text='<%# Eval("WorkStatus") %>'>
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
                                                                            <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Eval("Value1") %>' Width="100%" DropDownStyle="DropDown" DataSourceID="dsTripCode" ValueField="Code" TextField="Code">
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td>Driver</td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Eval("Value2") %>' AutoPostBack="False" Width="100%">
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
                                                                            <dxe:ASPxButtonEdit ID="btn_vehicle" ClientInstanceName="btn_vehicle" runat="server" Text='<%# Eval("Value3") %>' AutoPostBack="False" Width="100%">
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
                                                                                Value='<%# Eval("ItemPrice1") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Volume
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_M3" runat="server" Increment="0" Width="100%" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                                Value='<%# Eval("ItemPrice2") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Qty
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_Qty" runat="server" Width="100%" Increment="0" Number="0"
                                                                                NumberType="Integer" Value='<%# Eval("StorageTotalDays") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>PackType
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButtonEdit ID="txt_PackType" ClientInstanceName="txt_PackType" runat="server" Text='<%# Eval("Value4") %>' Width="100%">
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
                                                                                Value='<%# Eval("ItemPrice3") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Labour
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_FeeLabour" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                                Value='<%# Eval("ItemPrice4") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>OT
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_FeeOt" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                                Value='<%# Eval("ItemPrice5") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Admin
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_FeeAdmin" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                                Value='<%# Eval("ItemPrice6") %>'>
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
                                                                                Value='<%# Eval("ItemPrice7") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>MISC
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit ID="spin_FeeMisc" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                                Value='<%# Eval("ItemPrice8") %>'>
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Remark
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <dxe:ASPxTextBox ID="txt_FeeRemark" runat="server" Increment="0" Width="100%" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                                Value='<%# Eval("Note4") %>'>
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
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Freight Shipment">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton3" Width="80" runat="server" Text="Add New Freight Job" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed"&&SafeValue.SafeString(Eval("JobNo"),"").Length>0 %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                                                        grid_freight.AddNewRow();
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                         <td>
                                                            <dxe:ASPxButton ID="ASPxButton12" ClientInstanceName="btn_VoidMaster" runat="server" Width="100" Text="Refresh" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"USE")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                                                    grid_freight.Refresh();
                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                                <dxwgv:ASPxGridView ID="grid_freight" ClientInstanceName="grid_freight" runat="server"
                                                    KeyFieldName="Id" Width="950px" AutoGenerateColumns="False" DataSourceID="dsFreight"
                                                    OnInitNewRow="grid_freight_InitNewRow" OnInit="grid_freight_Init" OnBeforePerformDataSelect="grid_freight_BeforePerformDataSelect"
                                                    OnRowInserting="grid_freight_RowInserting" OnRowUpdating="grid_freight_RowUpdating">
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="40">
                                                          <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_freight.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%#SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_freight.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Note1"  Width="50%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Note2"  Width="50%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="padding: 2px 2px 2px 2px">
                                                                <div style="display: none">
                                                                    <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                                        BackColor="Control" Text='<%# Eval("Id") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <table style="padding: 2px 2px 2px 2px; display: normal;width:100%">
                                                                    <tr>
                                                                        <td style="width:70%"></td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="ASPxButton21" Width="110" runat="server" Text="Save" AutoPostBack="false"
                                                                                Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"USE")!="Closed" %>' UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s,e) {
                                    grid_freight.UpdateEdit();
                                    }" />
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                         <td>
                                                                            <dxe:ASPxButton ID="ASPxButton23" Width="110" runat="server" Text="Cancel" AutoPostBack="false"
                                                                                Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"USE")!="Closed" %>' UseSubmitBehavior="false">
                                                                                <ClientSideEvents Click="function(s,e) {
                                    grid_freight.CancelEdit();
                                    }" />
                                                                            </dxe:ASPxButton>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table style="padding: 2px 2px 2px 2px; display: normal;width:100%">
                                                                    <tr>
                                                                        <td>Marking
                                                                        </td>
                                                                        <td colspan="4">
                                                                            <dxe:ASPxMemo ID="txt_cargoMkg" Rows="4" Width="100%" runat="server" Text='<%# Bind("Note1") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                        <td>Description
                                                                        </td>
                                                                        <td colspan="4">
                                                                            <dxe:ASPxMemo ID="txt_cargoDes" Rows="4" Width="100%" runat="server" Text='<%# Bind("Note2") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="10">
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
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
								    <dxtc:TabPage Text="Job Instruction" Name="Job Instruction" Visible="false">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table width=800>
                                                    <tr>
                                                        <td width=150>Moving/Delivery Date</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="560px" ID="txt_Note1" Text='<%# Eval("Note1") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Moving/Delivery Time</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="560px" ID="txt_Note2" Text='<%# Eval("Note2") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Job Description</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="560px" ID="txt_Note3" Text='<%# Eval("Note3") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="560px" ID="txt_Note4" Text='<%# Eval("Note4") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Contact Person</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="560px" ID="txt_Note5" Text='<%# Eval("Note5") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Issued By (Collins)</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="560px" ID="txt_Note6" Text='<%# Eval("Note6") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td>Note7:</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="560px" ID="txt_Note7" Text='<%# Eval("Note7") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td>Note8:</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="960px" ID="txt_Note8" Text='<%# Eval("Note8") %>'></dxe:ASPxMemo></td>
													</tr>
													<tr style="display:none">
                                                        <td>Note9:</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="960px" ID="txt_Note9" Text='<%# Eval("Note9") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td>Note10:</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="960px" ID="txt_Note10" Text='<%# Eval("Note10") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td>Note11:</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="960px" ID="txt_Note11" Text='<%# Eval("Note11") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td>Note12:</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="960px" ID="txt_Note12" Text='<%# Eval("Note12") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td>Note13:</td>
                                                        <td><dxe:ASPxMemo runat="server" Rows="2" Width="960px" ID="txt_Note13" Text='<%# Eval("Note13") %>'></dxe:ASPxMemo></td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
									<dxtc:TabPage Text="Material" Visible="false" Name="Material">
                                        <ContentCollection>
                                           <dxw:ContentControl ID="ContentControl9" runat="server">
                                                  <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Add Empty Material" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        detailGrid.GetValuesOnCustomCallback('Material',OnCostingCallBack);
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
														<td>
                                                            <dxe:ASPxButton ID="ASPxButton1a" runat="server" Text="Add 20FT Material" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        detailGrid.GetValuesOnCustomCallback('Material20',OnCostingCallBack);
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td><td>
                                                            <dxe:ASPxButton ID="ASPxButton1b" runat="server" Text="Add 40FT Material" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        detailGrid.GetValuesOnCustomCallback('Material40',OnCostingCallBack);
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
                                                                                <dxe:ASPxButton ID="btn_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false"  Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
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
                                    <dxtc:TabPage Text="Warehouse Job" Name="Warehouse">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_AddDoIn" ClientInstanceName="btn_AddDoIn" runat="server" Width="180" Text="Create Storing Order" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed"&SafeValue.SafeString(Eval("ViaWh"),"No")!="No"&&SafeValue.SafeString(Eval("JobNo"),"").Length>0 %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                            detailGrid.GetValuesOnCustomCallback('AddDoIn',OnDoInCallBack);           
                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton6" ClientInstanceName="btn_AddDoOut" runat="server" Width="180" Text="Create Release Order" AutoPostBack="False"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed"&&SafeValue.SafeString(Eval("ViaWh"),"No")!="No"&&SafeValue.SafeString(Eval("JobNo"),"").Length>0 %>'>
                                                                <ClientSideEvents Click="function(s, e) {
                                                detailGrid.GetValuesOnCustomCallback('AddDoOut',OnDoOutCallBack);                 
                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                 <table style="width:100%">
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">Storing Order List</td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_DoIn" ClientInstanceName="grid_DoIn"
                                                    runat="server" KeyFieldName="Id" DataSourceID="dsDoIn" Width="100%"
                                                    OnBeforePerformDataSelect="grid_DoIn_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order No" FieldName="JobNo" VisibleIndex="1" Width="100">
                                                            <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/Modules/WareHouse/Job/JobScheduleEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
														<dxwgv:GridViewDataTextColumn Caption="Type" FieldName="JobType" VisibleIndex="6" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order Date" FieldName="JobDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Collect From" FieldName="CollectFrom" VisibleIndex="6" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobStatus" VisibleIndex="6" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">Release Order List</td>
                                                    </tr>
                                                </table>
                                                  <dxwgv:ASPxGridView ID="grid_DoOut" ClientInstanceName="grid_DoOut"
                                                    runat="server" KeyFieldName="Id" DataSourceID="dsDoOut" Width="100%"
                                                    OnBeforePerformDataSelect="grid_DoOut_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order No" FieldName="JobNo" VisibleIndex="1" Width="100">
                                                            <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/Modules/WareHouse/Job/JobScheduleEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
														<dxwgv:GridViewDataTextColumn Caption="Type" FieldName="JobType" VisibleIndex="6" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order Date" FieldName="JobDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Delivery To" FieldName="DeliveryTo" VisibleIndex="6" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobStatus" VisibleIndex="6" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Billing" Visible="true" Name="Billing">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Bill" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add Job Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice, "Job", txt_Quotation.GetText(), "0",txt_CustomerId.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton16s" Width="150" runat="server" Text="Add Storage Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice, "Storage", txt_Quotation.GetText(), "0",txt_CustomerId.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td style="display:none">
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add Credit Note" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice, "Job", txt_Quotation.GetText(), "0",txt_CustomerId.GetText());
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
                                                                <a onclick='ShowInvoice(Grid_Invoice,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintInvoiceHtml("<%# Eval("DocNo")%>","<%# Eval("DocType") %>")'>Print</a>
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
									<dxtc:TabPage Text="Inventory" Name="Inventory" Visible="false">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl200" runat="server">
                                                 <table class="noprint" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <h2>Cargo Received</h2>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Add New Received" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                   grid_Inventory.AddNewRow();
                                                                 }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                         <td>
                                                            <dxe:ASPxButton ID="ASPxButton10" runat="server" Text="Refresh" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                   grid_Inventory.Refresh();
                                                                 }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                               
                                                <dxwgv:ASPxGridView ID="grid_Inventory" ClientInstanceName="grid_Inventory" runat="server"
                                                    KeyFieldName="Id" Width="1400px" AutoGenerateColumns="False" DataSourceID="dsInventory"
                                                    OnBeforePerformDataSelect="grid_Inventory_BeforePerformDataSelect" OnInit="grid_Inventory_Init" OnInitNewRow="grid_Inventory_InitNewRow"
                                                    OnRowInserting="grid_Inventory_RowInserting" OnRowDeleting="grid_Inventory_RowDeleting" OnRowUpdating="grid_Inventory_RowUpdating">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsText Title="Cargo Received" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Inventory.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Inventory.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Inventory.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Inventory.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Item No" FieldName="ItemNo" VisibleIndex="1"
                                                            Width="120">
                                                            <DataItemTemplate>
                                                                <%# Eval("ItemNo") %>
                                                                 <div style="display: none">
                                                                    <dxe:ASPxLabel runat="server" ID="lbl_RItemId" ClientInstanceName="lbl_RItemId" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                </div>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="1"
                                                            Width="200">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="QTY" FieldName="Qty" VisibleIndex="2" Width="60">
                                                            <PropertiesSpinEdit NumberType="Integer" SpinButtons-ShowIncrementButtons="false" Width="60">
                                                            </PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Balance Qty" FieldName="BalQty" VisibleIndex="2" Width="60">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="2" Width="60">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2" Width="60">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Weight" FieldName="Weight" VisibleIndex="2" Width="60">
                                                            <PropertiesSpinEdit NumberType="Float" Increment="2" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                            </PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Volume" FieldName="Volume" VisibleIndex="2" Width="60">
                                                            <PropertiesSpinEdit NumberType="Float" Increment="2" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                            </PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="DoType" VisibleIndex="2" ReadOnly="true"
                                                            Width="60">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="6"
                                                            Width="260">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="Stock Date" FieldName="DoDate" VisibleIndex="10" Width="140" SortIndex="0" SortOrder="Descending">
                                                            <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm" Width="140"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="11">
                                                            <DataItemTemplate>
                                                                <a onclick='javascript:PopupUploadItemPhoto("<%# Eval("JobNo") %>","<%# Eval("Id") %>","<%# Eval("ItemNo") %>");' href="#">Upload Photo</a>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <a onclick='javascript:PopupUploadItemPhoto("<%# Eval("JobNo") %>","<%# Eval("Id") %>","<%# Eval("ItemNo") %>");' href="#">Upload Photo</a>
                                                            </EditItemTemplate>

                                                        </dxwgv:GridViewDataTextColumn>
                                                       <%-- <dxwgv:GridViewDataColumn Caption="Photo" Width="100px" VisibleIndex="12">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# SafeValue.SafeString("/Photos/" +SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format(@"select FilePath from Job_Attachment where JobNo='{0}' and RefNo='{1}'", Eval("Id"), Eval("JobNo")))).Replace("\\", "/"))%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# GetImgUrl(SafeValue.SafeString(Eval("Id")),SafeValue.SafeString(Eval("JobNo"))) %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="display: none">
                                                                    <dxe:ASPxLabel runat="server" ID="lbl_RItemId" OnInit="lbl_RItemId_Init" ClientInstanceName="lbl_RItemId" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                </div>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>--%>
                                                    </Columns>
                                                   <SettingsDetail  ShowDetailRow="true" ShowDetailButtons="true"/>
                                                    <Templates>
                                                        <DetailRow>
                                                            <dxwgv:ASPxGridView ID="grid_img_R" ClientInstanceName="grid_img_R" runat="server" DataSourceID="dsItemRImg" 
                                                                KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grid_img_R_BeforePerformDataSelect"
                                                                AutoGenerateColumns="false" OnRowDeleting="grid_img_R_RowDeleting" OnInit="grid_img_R_Init" OnInitNewRow="grid_img_R_InitNewRow" OnRowUpdating="grid_img_R_RowUpdating">
                                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                                <Columns>
                                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                                        <DataItemTemplate>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_img_R.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                            </dxwgv:ASPxGridView>
                                                        </DetailRow>
                                                    </Templates>
                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="ItemNo" SummaryType="Count" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                </dxwgv:ASPxGridView>


                                                <table class="noprint" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <h2>Cargo Released</h2>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton9" runat="server" Text="Multiple Pick Item" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                                   MultiplePickItem();
                                                                 }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Released" ClientInstanceName="grid_Released" runat="server" OnRowInserted="grid_Released_RowInserted"
                                                    KeyFieldName="Id" Width="1400px" AutoGenerateColumns="False" DataSourceID="dsReleased"  OnRowUpdated="grid_Released_RowUpdated"
                                                    OnBeforePerformDataSelect="grid_Released_BeforePerformDataSelect" OnInit="grid_Released_Init" OnInitNewRow="grid_Released_InitNewRow"
                                                      OnRowInserting="grid_Released_RowInserting" OnRowDeleting="grid_Released_RowDeleting" OnRowUpdating="grid_Released_RowUpdating"  OnRowDeleted="grid_Released_RowDeleted">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <SettingsEditing  Mode="Inline"/>
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Released.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Released.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Released.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Released.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Item No" FieldName="ItemNo" VisibleIndex="1"
                                                            Width="120">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="1"
                                                            Width="200">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="QTY" FieldName="Qty" VisibleIndex="2" Width="60">
                                                                <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="2" Width="60">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2" Width="60">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Weight" FieldName="Weight" VisibleIndex="2" Width="60">
                                                            <PropertiesSpinEdit NumberType="Float" Increment="2" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                            </PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Volume" FieldName="Volume" VisibleIndex="2" Width="60">
                                                            <PropertiesSpinEdit NumberType="Float" Increment="2" SpinButtons-ShowIncrementButtons="false" Width="40">
                                                            </PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="DoType" VisibleIndex="2" ReadOnly="true"
                                                            Width="60" >
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="6"
                                                            Width="260">
                                                        </dxwgv:GridViewDataTextColumn>
                                                       <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="Stock Date" FieldName="DoDate" VisibleIndex="10" Width="140" SortIndex="0" SortOrder="Descending">
                                                            <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm" Width="140"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>
                                                         <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="11">
                                                            <DataItemTemplate>
                                                               <a onclick='javascript:PopupUploadItemPhoto("<%# Eval("JobNo") %>","<%# Eval("Id") %>","<%# Eval("ItemNo") %>");' href="#">Upload Photo</a>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <a onclick='javascript:PopupUploadItemPhoto("<%# Eval("JobNo") %>","<%# Eval("Id") %>","<%# Eval("ItemNo") %>");' href="#">Upload Photo</a>
                                                            </EditItemTemplate>

                                                        </dxwgv:GridViewDataTextColumn>
                                                         <dxwgv:GridViewDataColumn Caption="Photo" Width="100px" VisibleIndex="12" Visible="false">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# SafeValue.SafeString("/Photos/" +SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format(@"select FilePath from Job_Attachment where JobNo='{0}' and RefNo='{1}'", Eval("Id"), Eval("JobNo")))).Replace("\\", "/"))%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# GetImgUrl(SafeValue.SafeString(Eval("Id")),SafeValue.SafeString(Eval("JobNo"))) %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="display:none">
                                                                        <dxe:ASPxLabel runat="server" ID="lbl_ItemId" ClientInstanceName="lbl_ItemId" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                    </div>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                    </Columns>
                                                    <SettingsDetail  ShowDetailRow="true" ShowDetailButtons="true"/>
                                                    <Templates>
                                                        <DetailRow>
                                                            <dxwgv:ASPxGridView ID="grid_img" ClientInstanceName="grid_img" runat="server" DataSourceID="dsItemImg" 
                                                                KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grid_img_BeforePerformDataSelect"
                                                                AutoGenerateColumns="false" OnRowDeleting="grid_img_RowDeleting" OnInit="grid_img_Init" OnInitNewRow="grid_img_InitNewRow" OnRowUpdating="grid_img_RowUpdating">
                                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                                <Columns>
                                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                                        <DataItemTemplate>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_img.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
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
                                                            </dxwgv:ASPxGridView>
                                                        </DetailRow>
                                                    </Templates>
                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="ItemNo" SummaryType="Count" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Attach" Name="Attachments" Visible="true">
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
																		<dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                    <ClientSideEvents Click="function(s,e){grd_Attach.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
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
																		<dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'>
                                                                    <ClientSideEvents Click="function(s,e){grd_Photo.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
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
                                     <dxtc:TabPage Text="Message" Visible="false" Name="Message">
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
                                    <dxtc:TabPage Text="Job Event" Visible="true" Name="Job Event">
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
                        grid_sch.Refresh();

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
