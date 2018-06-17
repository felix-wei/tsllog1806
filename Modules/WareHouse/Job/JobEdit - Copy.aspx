<%@ Page Language="C#"  EnableViewState="false" AutoEventWireup="true" CodeFile="JobEdit.aspx.cs" Inherits="WareHouse_Job_JobEdit" %>

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
            popubCtr.SetContentUrl('../Upload.aspx?Type=JO&Sn=' + txt_DoNo.GetText()+'&AttType='+type);
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
                txt_CustId.SetText();
                txt_CustName.SetText();
                txt_NewContact.SetText();
                txt_NewFax.SetText();
                txt_NewTel.SetText();
                txt_NewEmail.SetText();
                txt_NewPostalCode.SetText();
                memo_NewAddress.SetText();
                txt_NewRemark.SetText();
                cmb_NewJobType.SetText();
                date_NewIssueDate.SetText();
                ASPxPopupClientControl.Hide();
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
        function OnCostingCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else{
                alert("Success");
                grid_Material.Refresh();
            }
        }
        function OnTransferCallBack(v) {
            if (v == "Fail")
                alert("Create DO out Fail");
            else if (v == "No Balance Qty or No Lot No!")
                alert(v);
            else if (v != null)
                alert("Do Out No is " + v);
            //else if (v = "Success") {
            //    alert("Create Do Out  Success!");
            //}
        }
        function MultipleAdd() {
            popubCtr.SetHeaderText('Product List');
            popubCtr.SetContentUrl('../selectpage/SelectPoductFromStock.aspx?Type=SO&Sn=' + txt_DoNo.GetText() + '&Wh=' + txt_WareHouseId.GetText() + "&partyId=" + txt_ConsigneeCode.GetText());
            popubCtr.Show();
        }
         function MultiplePickCrews() {
            popubCtr.SetHeaderText('Multiple Crews');
            popubCtr.SetContentUrl('../SelectPage/MultipleCrews.aspx?Sn=' + txt_DoNo.GetText());
            popubCtr.Show();
        }
        function CreateDeliveryOrder() {
            popubCtr.SetHeaderText('Create Delivery Order');
            popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/AddDoOut.aspx?Sn=' + txt_DoNo.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_Crews.Refresh();
            grid_DoIn.Refresh();
        }
        function AfterPopubMultiInv1(v) {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            window.location = 'JobEdit.aspx?no=' + v;
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
            <asp:SqlDataSource ID="dsCosting" runat="server" ConnectionString="<%$ ConnectionStrings:local %>" SelectCommand="select * from Cost where SequenceId in (select max(SequenceId) from Cost group by CostIndex) ORDER BY CostIndex" />
            <wilson:DataSource ID="dsIssuePhoto" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhAttachment"
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
                TypeName="C2.WhDo" KeyMember="Id" FilterExpression="1=0" />
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
			                 <wilson:DataSource ID="dsWhPacking" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhPacking" KeyMember="Id" FilterExpression="1=0" />
				               <wilson:DataSource ID="dsCrews" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobCrews" KeyMember="Id" />
				             <wilson:DataSource ID="dsPersonInfo" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefPersonInfo" KeyMember="Id" />
				            <wilson:DataSource ID="dsMaterial" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Material" KeyMember="Id" />
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
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="ASPxPopupClientControl" SkinID="None" Width="240px"
                ShowOnPageLoad="false" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                AllowDragging="True" CloseAction="None" PopupElementID="popupArea"
                EnableViewState="False" runat="server" PopupHorizontalOffset="0"
                PopupVerticalOffset="0" EnableHierarchyRecreation="True">
                <HeaderTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">New Job
                            </td>
                            <td>
                                <a id="a_X" onclick="SetPCVisible(false)" onmousedown="event.cancelBubble = true;" style="width: 15px; height: 14px; cursor: pointer;">X</a>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                         <div style="padding: 2px 2px 2px 2px;width:660px">
                            <div style="display: none">
                              
                            </div>
                           <table style="text-align: left; padding: 2px 2px 2px 2px; width: 650px">
                                <tr>
                                     <td>Customer</td>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="90">
                                                    <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server"
                                                        Width="90" HorizontalAlign="Left" AutoPostBack="False">
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_CustId,txt_CustName,txt_NewContact,txt_NewTel,txt_NewFax,txt_NewEmail,txt_NewPostalCode,memo_NewAddress,null,null,'C');
                                                                    }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="250" ReadOnly="true" BackColor="Control" ID="txt_CustName" ClientInstanceName="txt_CustName">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                  
                                      <td>Job Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="160px" runat="server" ClientInstanceName="date_NewIssueDate"
                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                        </dxe:ASPxDateEdit>
                                    </td>

                                </tr>
                               <tr>
                                   <td rowspan="3">Address</td>
                                   <td rowspan="3" colspan="3">
                                       <dxe:ASPxMemo ID="memo_NewAddress" Rows="5" Width="340" ClientInstanceName="memo_NewAddress"
                                           runat="server">
                                       </dxe:ASPxMemo>
                                   </td>
                                   <td>JobType</td>
                                   <td>
                                       <dxe:ASPxComboBox ID="cmb_JobType" ClientInstanceName="cmb_NewJobType" runat="server" Width="160" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                           <Items>
                                               <dxe:ListEditItem Text="Local Move" Value="Local Move" />
                                               <dxe:ListEditItem Text="Office Move" Value="Office Move" />
                                               <dxe:ListEditItem Text="International Move" Value="International Move" />
                                               <dxe:ListEditItem Text="Storage" Value="Storage" />
                                               <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                               <dxe:ListEditItem Text="Project" Value="Project" />
                                           </Items>
                                       </dxe:ASPxComboBox>
                                   </td>
                               </tr>
                               <tr>
                                     <td>Contact</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_NewContact" Width="160px" runat="server" ClientInstanceName="txt_NewContact" >
                                        </dxe:ASPxTextBox>
                                    </td>
                                   </tr>
                               <tr>
                                   <td>Tel</td>
                                   <td>
                                       <dxe:ASPxTextBox ID="txt_NewTel" Width="160px" runat="server" ClientInstanceName="txt_NewTel">
                                       </dxe:ASPxTextBox>
                                   </td>
                               </tr>
                               <tr style="text-align: left;">
                                  
                               </tr>
                                <tr>
                                    <td rowspan="3">Remark</td>
                                    <td colspan="3" rowspan="3">
                                        <dxe:ASPxMemo runat="server" Width="340" Rows="4" ID="txt_NewRemark"  ClientInstanceName="txt_NewRemark">
                                        </dxe:ASPxMemo>
                                    </td>
                                     <td>Email</td>
                                   <td>
                                       <dxe:ASPxTextBox ID="txt_NewEmail" Width="160px" runat="server" ClientInstanceName="txt_NewEmail">
                                       </dxe:ASPxTextBox>
                                   </td>
                                </tr>
                               <tr>
                                   <td>Fax</td>
                                   <td>
                                       <dxe:ASPxTextBox ID="txt_NewFax" Width="160px" runat="server" ClientInstanceName="txt_NewFax">
                                       </dxe:ASPxTextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td>PostalCode</td>
                                   <td>

                                       <dxe:ASPxTextBox runat="server" Width="160px" ID="txt_NewPostalCode" ClientInstanceName="txt_NewPostalCode">
                                       </dxe:ASPxTextBox>
                                   </td>
                               </tr>
                            </table>
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 660px">
                                 <tr>
                                   <td colspan="4" style="width:90%"></td>
                                    <td >

                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save">
                                            <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('OK',OnSaveCallBack);
                                                 }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
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
					
					
                        <table  bordercolor="#000000" style="text-align: center; border:solid 1px #000000; border-spacing: 0px; width:960px">
                            <tr>
                                <td  style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000;">
                                    <dxe:ASPxLabel ID="lbl_Inquir" Width="120px" runat="server" Text="Customer Inquiry"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000">
                                    <dxe:ASPxLabel ID="lbl_Survey" Width="150px" runat="server" Text="Site Survey"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000">
                                    <dxe:ASPxLabel ID="lbl_Costing" Width="120px" runat="server" Text="Costing"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000">
                                    <dxe:ASPxLabel ID="lbl_Quotation" Width="120px" runat="server" Text="Quotation"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000">
                                    <dxe:ASPxLabel ID="lbl_Confirmation" Width="120px" runat="server" Text="Job Confirmation"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000">
                                    <dxe:ASPxLabel ID="lbl_Billing" Width="120px" runat="server" Text="Billing"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000">
                                    <dxe:ASPxLabel ID="lbl_Completion" Width="150px" runat="server" Text="Job Completion"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000;">
                                    <dxe:ASPxLabel ID="lbl_Close" Width="140px" runat="server" Text="Job Close"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr style="text-align: left;">
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000; width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date" Text='<%# SafeValue.SafeDateStr(Eval("DateTime1") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time" Text='<%# SafeValue.SafeDate(Eval("DateTime1"),DateTime.Now).ToLongTimeString() %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>C/S</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Cs" Text='<%# Eval("CreateBy") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000;width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date1" Text='<%# SafeValue.SafeDateStr(Eval("DateTime2") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time1" Text='<%# SafeValue.SafeDate(Eval("DateTime2"),DateTime.Now).ToLongTimeString()%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Surveyer</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Surveyer" Text='<%# Eval("Value2") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000;width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date2" Text='<%# SafeValue.SafeDateStr(Eval("DateTime3") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time2" Text='<%# SafeValue.SafeDate(Eval("DateTime3"),DateTime.Now).ToLongTimeString()%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ops</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Ops" Text='<%# Eval("Value3") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000;width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date3" Text='<%# SafeValue.SafeDateStr(Eval("DateTime4") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time3" Text='<%# SafeValue.SafeDate(Eval("DateTime4"),DateTime.Now).ToLongTimeString()%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sales</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Sales" Text='<%# Eval("Value4") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000;width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date4" Text='<%# SafeValue.SafeDateStr(Eval("DateTime5") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time4" Text='<%# SafeValue.SafeDate(Eval("DateTime5"),DateTime.Now).ToLongTimeString()%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ops</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Ops1" Text='<%# Eval("Value5") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000;width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date5" Text='<%# SafeValue.SafeDateStr(Eval("DateTime6") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time5" Text='<%# SafeValue.SafeDate(Eval("DateTime6"),DateTime.Now).ToLongTimeString()%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Supervisor</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Supervisor" Text='<%# Eval("Value6") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000;width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date6" Text='<%# SafeValue.SafeDateStr(Eval("DateTime7") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time6" Text='<%# SafeValue.SafeDate(Eval("DateTime7"),DateTime.Now).ToLongTimeString()%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ops</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Ops2" Text='<%# Eval("Value7") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-bottom:solid 1px #000000;width:120px;">
                                    <table>
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Date7" Text='<%# SafeValue.SafeDateStr(Eval("DateTime8") )%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Time7" Text='<%# SafeValue.SafeDate(Eval("DateTime8"),DateTime.Now).ToLongTimeString()%>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Approval</td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" ID="lbl_Approval" Text='<%# Eval("Value8") %>'></dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="text-align:center">
                               
                                <td style=" border-right:solid 1px #000000;"></td>
                                <td style=" border-right:solid 1px #000000;"> <a href='/ReportJob/PrintSiteSurvey.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Survey Form</a></td>
                                <td style=" border-right:solid 1px #000000;"><a href="#" onclick="">Costing</a></td>
                                <td style=" border-right:solid 1px #000000;">
				<a href='/ReportJob/PrintQuotation.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Quotation</a>
				<br> <a href='/ReportJob/PrintAccept.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Acceptance Sheet</a>
				</td>
                                <td style=" border-right:solid 1px #000000;">
					<a href='/ReportJob/PrintInstruction.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Job Instruction</a>
				<br>	<a href="#" onclick="">Draft B/L</a>
				</td>
                                <td style=" border-right:solid 1px #000000;"><a href="#" onclick="">Open Schedule</a></td>
                                <td style=" border-right:solid 1px #000000;"> <a href="#" onclick="">Invoice</a></td>
                                <td > <a href="#" onClick="">Job Summary</a></td>
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
                                        <dxe:ASPxButton ID="btn_CloseJob" ClientInstanceName="btn_CloseJob" runat="server" Width="80" Text="Close" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                        detailGrid.GetValuesOnCustomCallback('Close',OnCloseCallBack);                 

                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                  

                                </tr>
                            </table>

                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                            <table>
                                <tr>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                    <td></td>
                                    <td style="width: 120px;"></td>
                                </tr>
                                <tr>
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
                                            runat="server" ClientInstanceName="cmb_Status" OnCustomJSProperties="cmb_Status_CustomJSProperties" Text='<%# Eval("JobStage") %>'>
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
                                                <dxe:ListEditItem Text="International Move" Value="International Move" />
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
                                                <dxe:ASPxTextBox runat="server" Width="270" ReadOnly="true" Text='<%# Eval("CustomerName") %>' BackColor="Control" ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </table>
                                    </td>
                                      <td>Origin Port</td>
                                      <td>
                                          <dxe:ASPxButtonEdit ID="btn_OriginPort" ClientInstanceName="btn_OriginPort" runat="server"
                                                    Width="100%" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("OriginPort") %>'>
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
                                    <td rowspan="2">Address</td>
                                    <td rowspan="2" colspan="3">
                                        <dxe:ASPxMemo ID="memo_Address" Rows="4" Width="100%" ClientInstanceName="memo_Address"
                                            runat="server" Text='<%# Eval("CustomerAdd") %>' >
                                        </dxe:ASPxMemo>
                                    </td>
                                      <td rowspan="2">Origin Address</td>
                                      <td rowspan="2">
                                          <dxe:ASPxMemo ID="memo_Address1" Rows="4" Width="100%" runat="server" Text='<%# Eval("OriginAdd") %>'></dxe:ASPxMemo>
                                      </td>
                                      <td rowspan="2">Destination Address</td>
                                      <td rowspan="2">
                                          <dxe:ASPxMemo ID="memo_Address2" Rows="4" Width="100%" runat="server" Text='<%# Eval("DestinationAdd") %>'></dxe:ASPxMemo>
                                      </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Contact</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Contact" runat="server" ClientInstanceName="txt_Contact" Text='<%# Eval("Contact") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Tel</td>
                                    <td> <dxe:ASPxTextBox ID="txt_Tel" runat="server" ClientInstanceName="txt_Tel" Text='<%# Eval("Tel") %>'>
                                        </dxe:ASPxTextBox></td>
                                   
                                     <td>PostalCode</td>
                                    <td>

                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_PostalCode" Text='<%# Eval("Postalcode") %>' ClientInstanceName="txt_PostalCode">
                                        </dxe:ASPxTextBox>
                                    </td>
                                      <td>Currency
                                    </td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Eval("Currency") %>' runat="server" Width="100%" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>Email</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Email" Width="100%" runat="server" ClientInstanceName="txt_Email" Text='<%# Eval("Email") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                     <td>Fax</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Fax" runat="server" ClientInstanceName="txt_Fax" Text='<%# Eval("Fax") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                     <td>Payment Term</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_PayTerm" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("PayTerm")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="CASH" Value="CASH" />
                                                <dxe:ListEditItem Text="30DAYS" Value="30DAYS" />
                                                <dxe:ListEditItem Text="60DAYS" Value="60DAYS" />
                                                <dxe:ListEditItem Text="90DAYS" Value="90DAYS" />
                                                <dxe:ListEditItem Text="120DAYS" Value="120DAYS" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                   
                                    <td>ExRate
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_ExRate" ClientInstanceName="spin_ExRate" DisplayFormatString="0.000000"
                                            runat="server" Width="100%" Value='<%# Eval("ExRate")%>' DecimalPlaces="6" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>


                                </tr>

                                <tr>
                                    <td rowspan="2">Remark</td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo runat="server" Width="100%" Rows="4" ID="txt_Remark" Text='<%# Eval("Remark") %>' ClientInstanceName="txt_Remark3">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Quotation ExpiryDate</td>
                                     <td>
                                        <dxe:ASPxDateEdit ID="date_ExpiryDate" Width="100%" runat="server" Value='<%# Eval("ExpiryDate") %>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                     <td>WareHouse</td>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_WareHouseId" ClientInstanceName="txt_WareHouseId" runat="server" Text='<%# Eval("WareHouseId")%>' Width="100%" HorizontalAlign="Left" AutoPostBack="False">
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
                                        <dxe:ASPxComboBox ID="cmb_WorkStatus" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" OnCustomJSProperties="cmb_WorkStatus_CustomJSProperties" Value='<%# Eval("WorkStatus")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                <dxe:ListEditItem Text="Working" Value="Working" />
                                                <dxe:ListEditItem Text="Complete" Value="Complete" />
                                                <dxe:ListEditItem Text="Unsuccess" Value="Unsuccess" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Sales</td>
                                    <td>
                                                                               <dxe:ASPxComboBox ID="cmb_SalesId" ClientInstanceName="cmb_SalesId" runat="server" DataSourceID="dsSalesman" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Value='<%# Eval("Value2") %>' TextField="Code" ValueField="Code" Width="100%">
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
                                    <dxtc:TabPage Name="BookingDetails" Text="Booking Details" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl5" runat="server">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td colspan="10">
                                                            <table>
                                                                <tr>

                                                                    <td colspan="4" style="background-color: Gray; color: White;">
                                                                        <b>Moving Details</b>
                                                                    </td>
                                                                    <td colspan="2" style="background-color: Gray; color: White;">
                                                                        <b>Item Description</b>
                                                                    </td>
                                                                    <td colspan="4" style="background-color: Gray; color: White; width:625px">
                                                                        <b>Moving Schedule</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Volumne (Cuft)</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_Volumne" ClientInstanceName="txt_Volumne" Text='<%# Eval("Volumne") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td colspan="2" rowspan="4">
                                                                        <dxe:ASPxMemo ID="memo_Description" Rows="4" Width="150" ClientInstanceName="memo_Description" Height="110" Text='<%# Eval("ItemDes") %>'
                                                                            runat="server">
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Pack Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Pack" Width="180" runat="server" Value='<%# Eval("PackDate") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Via Warehouse</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Via" Text='<%# Eval("ViaWh") %>'
                                                                            runat="server">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                                <dxe:ListEditItem Text="No" Value="No" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>No of Trip</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_TripNo" Text='<%# Eval("TripNo") %>' ClientInstanceName="txt_TripNo">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>

                                                                    <td>Move Date</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_MoveDate" Width="180" runat="server" Value='<%# Eval("MoveDate") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
																	  <td>Storage Start</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_StorageStartDate" Width="180" runat="server" Value='<%# Eval("StorageStartDate") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Charges</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100%" Value='<%# Eval("Charges") %>'
                                                                            DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_Charges" ClientInstanceName="spin_Charges" Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>

                                                                    <td>Port of Entry</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_PortOfEntry" ClientInstanceName="btn_PortOfEntry" runat="server" MaxLength="5"
                                                                            Width="180" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("EntryPort") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPort(btn_PortOfEntry,null);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
																	 <td>Free Storage Days</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_StorageFreeDays" Text='<%# Eval("StorageFreeDays") %>' ClientInstanceName="txt_StorageFreeDays">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Mode</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxComboBox ID="cmb_Mode" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("Mode")%>'>
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

                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>                                                  
                                                    <tr>
                                                        <td colspan="10" style="background-color: Gray; color: White;">
                                                            <b>Additional Information</b>
                                                        </td>
                                                    </tr>
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
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Items List" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl1" runat="server">
                                                 <dxe:ASPxButton ID="ASPxButton15" Width="150" runat="server" Text="Add Item" AutoPostBack="false" UseSubmitBehavior="false"
                                                  Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                                    <ClientSideEvents Click="function(s,e) {
                                                       Grid_Packing.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="Grid_Packing" ClientInstanceName="Grid_Packing" runat="server"
                                                    KeyFieldName="Id" DataSourceID="dsWhPacking" Width="100%" OnBeforePerformDataSelect="Grid_Packing_BeforePerformDataSelect"
                                                    OnRowUpdating="Grid_Packing_RowUpdating" OnRowInserting="Grid_Packing_RowInserting"
                                                    OnRowDeleting="Grid_Packing_RowDeleting" OnInitNewRow="Grid_Packing_InitNewRow"
                                                    OnInit="Grid_Packing_Init">
                                                    <SettingsBehavior ConfirmDelete="True" />
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
                                                                                <dxe:ASPxButton ID="btn_cont_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){Grid_Packing.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                       <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="PACK TYPE" FieldName="PackageType" VisibleIndex="7">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="JobNo" VisibleIndex="7" Visible="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MkgType" VisibleIndex="8" Visible="false">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_Mkg_HouseNo" runat="server" Text='<%# Bind("JobNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                            <dxe:ASPxTextBox ID="txt_Mkg_RefNo" runat="server" Text='<%# Bind("RefNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    Cont No
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txt_ContainerNo" runat="server" Text='<%# Bind("ContainerNo") %>' Width="120px">
                                                            </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    Seal No
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="120" ID="txt_sealN"
                                                                        Text='<%# Bind("SealNo") %>' ClientInstanceName="txt_sealN">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td width="90">Cont Type
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_contType" ClientInstanceName="txt_contType" Text='<%# Bind("ContainerType") %>' 
                                                                        runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_contType,1);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Weight
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                        runat="server" Width="120" ID="spin_wt" Value='<%# Bind("Weight")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                    Volume
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                        runat="server" Width="120" ID="spin_m3" Value='<%# Bind("Volume")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>Qty
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120"
                                                                        ID="spin_pkg" Text='<%# Bind("Qty")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>Pack Type
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType2" Text='<%# Bind("PackageType")%>'
                                                                        runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType2,2);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td width="45">
                                                                    Marking
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo runat="server" Rows="6" Width="298" ID="txt_mkg1" MaxLength="500" Text='<%# Bind("Marking")%>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                                <td>
                                                                    Description
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo runat="server" Rows="6" Width="304" ID="txt_des1" MaxLength="500" Text='<%# Bind("Description")%>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' >
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
                                    </dxtc:TabPage><dxtc:TabPage Text="Job Costing" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl_Cost" runat="server">
										<table cellspacing=2><tr>
											<td>
										<dxe:ASPxButton ID="ASPxButton2b" Width="150" runat="server" Text="Add Charges" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                                AutoPostBack="false" UseSubmitBehavior="false" ClientVisible="false">
                                                <ClientSideEvents Click="function(s,e) {
													PickBill();
                                                        }" />
                                            </dxe:ASPxButton>
											</td>
											<td>
										<dxe:ASPxButton ID="ASPxButton2d" Width="150" runat="server" Text="Add Costing" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                    grid_Cost.GetValuesOnCustomCallback('AddNew',AfterCostAddClick);
                                                        }" />
                                            </dxe:ASPxButton>
											</td>
											</tr></table>
											<hr>
                                              <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server" OnCustomDataCallback="grid_Cost_CustomDataCallback" OnCustomCallback="grid_Cost_CustomCallback"
                                                DataSourceID="dsCosting" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect" OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
                                                OnRowUpdating="grid_Cost_RowUpdating" OnRowInserted="grid_Cost_RowInserted" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated" OnRowDeleting="grid_Cost_RowDeleting">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <SettingsEditing Mode="PopupEditForm" />
                                                  <Columns>
                                                      <dxwgv:GridViewDataTextColumn Visible="true" VisibleIndex="0" Width="5%">
                                                          <DataItemTemplate>
                                                              <table>
                                                                  <tr>
                                                                      <td>
                                                                          <dxe:ASPxButton ID="btn_CostEdit" runat="server" Text='<%#SafeValue.SafeString(Eval("Status")).ToUpper()=="VOID"?"View":"Edit" %>' Width="50" AutoPostBack="false" OnInit="btn_CostEdit_Init">
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
                                                      </dxwgv:GridViewDataTextColumn>
                                                      <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="#" FieldName="CostIndex" Width="20" VisibleIndex="1">
                                                      </dxwgv:GridViewDataTextColumn>
                                                      <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Version" FieldName="Version" Width="20" VisibleIndex="1">
                                                      </dxwgv:GridViewDataTextColumn>
                                                      <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" Width="150" VisibleIndex="2" CellStyle-Paddings-Padding="0">
                                                          <DataItemTemplate>
                                                              <dxe:ASPxTextBox ID="txt_Description" runat="server" Text='<%#Bind("Description") %>' OnInit="txt_Description_Init" Border-BorderWidth="0" FocusedStyle-Border-BorderWidth="1"></dxe:ASPxTextBox>
                                                          </DataItemTemplate>
                                                      </dxwgv:GridViewDataTextColumn>
                                                      <dxwgv:GridViewDataColumn Caption="Remark" FieldName="Marking" Width="150" VisibleIndex="2" CellStyle-Paddings-Padding="0">
                                                          <DataItemTemplate>
                                                              <dxe:ASPxTextBox ID="txt_Remark" runat="server" Text='<%#Bind("Marking") %>' OnInit="txt_Remark_Init" Border-BorderWidth="0" FocusedStyle-Border-BorderWidth="1"></dxe:ASPxTextBox>
                                                          </DataItemTemplate>
                                                      </dxwgv:GridViewDataColumn>
                                                      <dxwgv:GridViewDataTextColumn Caption="Profitmargin" FieldName="Profitmargin" Width="150" VisibleIndex="3" CellStyle-Paddings-Padding="0">
                                                          <DataItemTemplate>
                                                              <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" Width="90" ID="spin_Profitmargin" runat="server" Text='<%#Bind("Profitmargin") %>' Border-BorderWidth="0" FocusedStyle-Border-BorderWidth="1"></dxe:ASPxSpinEdit>
                                                          </DataItemTemplate>
                                                      </dxwgv:GridViewDataTextColumn>
                                                      <dxwgv:GridViewDataColumn Caption="Total Charges" FieldName="Amount" VisibleIndex="3" Width="50">
                                                          <DataItemTemplate><%#Eval("Amount") %></DataItemTemplate>
                                                          <EditItemTemplate><%#Eval("Amount") %></EditItemTemplate>
                                                      </dxwgv:GridViewDataColumn>
                                                      <dxwgv:GridViewDataColumn Caption="Total Cost" FieldName="Amount2" VisibleIndex="3" Width="50">
                                                          <DataItemTemplate><%#Eval("Amount2") %></DataItemTemplate>
                                                          <EditItemTemplate><%#Eval("Amount2") %></EditItemTemplate>
                                                      </dxwgv:GridViewDataColumn>
                                                      <dxwgv:GridViewDataColumn Caption="Gross Profit" FieldName="Amount" VisibleIndex="3" Width="50">
                                                          <DataItemTemplate><%#SafeValue.SafeDecimal(Eval("Amount"),0)-SafeValue.SafeDecimal(Eval("Amount2"),0) %></DataItemTemplate>
                                                          <EditItemTemplate><%#SafeValue.SafeDecimal(Eval("Amount"),0)-SafeValue.SafeDecimal(Eval("Amount2"),0) %></EditItemTemplate>
                                                      </dxwgv:GridViewDataColumn>
                                                      <dxwgv:GridViewDataTextColumn FieldName="Status" Caption="Status" Width="80" VisibleIndex="5">
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
                                    <dxtc:TabPage Text="Quotation" Visible="true">
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
 <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ID="txt_Attention1" MaxLength="500" Text='<%# Eval("Attention1")%>'>
                                                                    </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>FOR THE ABOVE RATE COLLIN'S SERVICES INCLUDE:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
 <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ID="txt_Attention2" MaxLength="500" Text='<%# Eval("Attention2")%>'>
                                                                    </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  style="background-color: Gray; color: White;">
                                                            <b>OUR SERVICES EXCLUDE:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
 <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ID="txt_Attention3" MaxLength="500" Text='<%# Eval("Attention3")%>'>
                                                                    </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">
                                                            <b>ACCEPANCE OF OUOTATION:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ID="txt_Attention4" MaxLength="500" Text='<%# Eval("Attention4")%>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: Gray; color: White;">
                                                            <b>PAYMENT TERMS:</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo runat="server" Rows="10" Width="100%" ID="txt_Attention5" MaxLength="500" Text='<%# Eval("Attention5")%>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Crews" Visible="true">
                                        <ContentCollection>
                                          <dxw:ContentControl ID="ContentControl4" runat="server">
                                                 <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton10" runat="server" Text="Multiple Pick Crews" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"%>'>
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
                                                                                <dxe:ASPxButton ID="btn_del" runat="server"
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
                                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Crews.UpdateEdit() }"  %>'>
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
                                                                Caption="Name" FieldName="Name" VisibleIndex="1" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="Tel" VisibleIndex="3" Width="80px">
                                                            </dxwgv:GridViewDataTextColumn>
                                                             <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="3" Width="80px" >
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="8" Width="200">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Settings ShowFooter="true" />
                                                        <TotalSummary>
                                                            <dxwgv:ASPxSummaryItem FieldName="Name" SummaryType="Count" DisplayFormat="{0:0}" />
                                                        </TotalSummary>
                                                    </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="MCST" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl7" runat="server">
                                                <table>
                                                    <tr>
                                                        <td colspan="8" style="background-color: Gray; color: White;">
                                                            <b>ONE</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>McstNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="120" ID="txt_McstNo1"
                                                                        Text='<%# Bind("McstNo1") %>' ClientInstanceName="txt_McstNo1">
                                                                    </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Date</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_McstDate1" Width="180" runat="server" Value='<%# Eval("McstDate1") %>'
                                                                EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>States</td>
                                                        <td><dxe:ASPxTextBox runat="server" Width="120" ID="txt_States1"
                                                                        Text='<%# Eval("States1") %>' ClientInstanceName="txt_States1">
                                                                    </dxe:ASPxTextBox></td>
                                                        <td>Amount</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                runat="server" Width="120" ID="spin_Amount1" Value='<%# Eval("Amount1")%>' Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td >Remark</td>
                                                         <td colspan="7">
                                                             <dxe:ASPxMemo runat="server" Rows="4" Width="100%" ID="txt_McstRemark1" MaxLength="500" Text='<%# Bind("McstRemark1")%>'>
                                                             </dxe:ASPxMemo>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" style="background-color: Gray; color: White;">
                                                            <b>TWO</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>McstNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="120" ID="txt_McstNo2"
                                                                        Text='<%# Bind("McstNo2") %>' ClientInstanceName="txt_McstNo2">
                                                                    </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Date</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_McstDate2" Width="180" runat="server" Value='<%# Eval("McstDate2") %>'
                                                                EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>States</td>
                                                        <td><dxe:ASPxTextBox runat="server" Width="120" ID="txt_States2"
                                                                        Text='<%# Eval("States2") %>' ClientInstanceName="txt_States2">
                                                                    </dxe:ASPxTextBox></td>
                                                        <td>Amount</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                runat="server" Width="120" ID="spin_Amount2" Value='<%# Eval("Amount2")%>' Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td >Remark</td>
                                                         <td colspan="7">
                                                             <dxe:ASPxMemo runat="server" Rows="4" Width="100%" ID="txt_McstRemark2" MaxLength="500" Text='<%# Bind("McstRemark2")%>'>
                                                             </dxe:ASPxMemo>
                                                         </td>
                                                    </tr>
                                                </table>
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
                                     <dxtc:TabPage Text="Material" Visible="true">
                                        <ContentCollection>
                                           <dxw:ContentControl ID="ContentControl9" runat="server">
                                                  <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Add Material" AutoPostBack="false"
                                                                UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"%>'>
                                                                <ClientSideEvents Click="function(s,e) {
                                                        detailGrid.GetValuesOnCustomCallback('Material',OnCostingCallBack);
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_Material" ClientInstanceName="grid_Material" runat="server"  OnRowInserting="grid_Material_RowInserting" Width="100%"
                                                        OnRowDeleting="grid_Material_RowDeleting" OnRowUpdating="grid_Material_RowUpdating" OnBeforePerformDataSelect="grid_Material_BeforePerformDataSelect1" DataSourceID="dsMaterial"
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
                                                              <dxwgv:GridViewDataTextColumn Caption="" VisibleIndex="3" Width="400px">
                                                                   <HeaderTemplate>
                                                                       <table style="width:100%; text-align:center;border-spacing: 0px;width:100%;
    height:auto;font-family:Arial;font-size:12px;">
                                                                           <tr >
                                                                               <td colspan="10" style="border-bottom: solid 1px #9B9A96;">QUANTITY</td>
                                                                           </tr>
                                                                           <tr>
                                                                               <td colspan="2" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   REQUISITIONED
                                                                               </td>
                                                                               <td colspan="6" style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">
                                                                                   ADDITIONAL

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
    border-right: solid 1px #9B9A96;">New(2a)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(2a)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">New(2a)</td>
                                                                               <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(2b)</td>
                                                                                <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">Used(2b)</td>
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
                                                                       <table style="width:100%; margin:0px auto; text-align:center;border-spacing: 0px;width:100%;
    height:auto;font-family:Arial;font-size:12px;">
                                                                           <tr>
                                                                               <td><%# Eval("RequisitionNew") %></td>
                                                                               <td><%# Eval("RequisitionUsed") %></td>
                                                                               <td><%# Eval("AdditionalNew") %></td>
                                                                               <td><%# Eval("AdditionalNew1") %></td>
                                                                               <td><%# Eval("AdditionalNew2") %></td>
                                                                               <td><%# Eval("AdditionalUsed") %></td>
                                                                               <td><%# Eval("AdditionalUsed1") %></td>
                                                                               <td><%# Eval("AdditionalUsed2") %></td>
                                                                               <td><%# Eval("ReturnedNew") %></td>
                                                                               <td>
                                                                                   <%# Eval("ReturnedUsed") %></td>
                                                                               <td></td>
                                                                               <td></td>
                                                                           </tr>
                                                                       </table>
                                                                   </DataItemTemplate>
                                                                   <EditItemTemplate>
                                                                       <table style="width: 100%; margin: 0px auto; text-align: center; border-spacing: 0px; width: 100%; height: auto; font-family: Arial; font-size: 12px;">
                                                                           <tr>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionNew" runat="server" Width="40" ClientInstanceName="spin_RequisitionNew" Value='<%# Bind("RequisitionNew") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_RequisitionUsed" runat="server" Width="40" ClientInstanceName="spin_RequisitionUsed" Value='<%# Bind("RequisitionUsed") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalNew" runat="server" Width="40" ClientInstanceName="spin_AdditionalNew" Value='<%# Bind("AdditionalNew") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                                 <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalNew1" runat="server" Width="40" ClientInstanceName="spin_AdditionalNew" Value='<%# Bind("AdditionalNew1") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                                 <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalNew2" runat="server" Width="40" ClientInstanceName="spin_AdditionalNew" Value='<%# Bind("AdditionalNew2") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalUsed" runat="server" Width="40" ClientInstanceName="spin_AdditionalUsed" Value='<%# Bind("AdditionalUsed") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalUsed1" runat="server" Width="40" ClientInstanceName="spin_AdditionalUsed" Value='<%# Bind("AdditionalUsed1") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_AdditionalUsed2" runat="server" Width="40" ClientInstanceName="spin_AdditionalUsed" Value='<%# Bind("AdditionalUsed2") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_ReturnedNew" runat="server" Width="40" ClientInstanceName="spin_ReturnedNew" Value='<%# Bind("ReturnedNew") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>
                                                                               <td>
                                                                                   <dxe:ASPxSpinEdit ID="spin_ReturnedUsed" runat="server" Width="40" ClientInstanceName="spin_ReturnedUsed" Value='<%# Bind("ReturnedUsed") %>' Increment="0" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                   </dxe:ASPxSpinEdit>
                                                                               </td>

                                                                           </tr>
                                                                       </table>
                                                                   </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                             <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="180px">
                                                                 <HeaderTemplate>
                                                                     <table style="width: 100%; text-align: center; border-spacing: 0px; width: 100%; height: auto; font-family: Arial; font-size: 12px;">
                                                                         <tr>
                                                                             <td colspan="2" style="border-bottom: solid 1px #9B9A96;">TOTAL USED</td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td style="border-bottom: solid 1px #9B9A96;
    border-right: solid 1px #9B9A96;">1a+2a+3a</td>
                                                                             <td style="border-bottom: solid 1px #9B9A96;">1b+2b+3b</td>
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
                                                                             <td><%# Eval("TotalNew") %></td>
                                                                             <td><%# Eval("TotalUsed") %></td>
                                                                         </tr>
                                                                     </table>
                                                                 </DataItemTemplate>
                                                                 <EditItemTemplate>
                                                                     <table style="width: 100%; text-align: center; border-spacing: 0px; width: 100%; height: auto; font-family: Arial; font-size: 12px;">
                                                                         <tr>
                                                                             <td><%# Eval("TotalNew") %></td>
                                                                             <td><%# Eval("TotalUsed") %></td>
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
                                    <dxtc:TabPage Text="Warehouse Link">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <dxwgv:ASPxGridView ID="grid_DoIn" ClientInstanceName="grid_DoIn"
                                                    runat="server" KeyFieldName="Id" DataSourceID="dsDoIn" Width="100%"
                                                    OnBeforePerformDataSelect="grid_DoIn_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order No" FieldName="DoNo" VisibleIndex="1" Width="100">
                                                            <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("DoNo") %>","/Modules/WareHouse/Job/DoOutEdit.aspx?no=<%# Eval("DoNo") %>",{title:"<%# Eval("DoNo") %>", fresh:false, external:true});'><%# Eval("DoNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Order Date" FieldName="DoDate" VisibleIndex="2" Width="70">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Customer Ref" FieldName="CustomerReference" VisibleIndex="6" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="RefDate" FieldName="CustomerDate" VisibleIndex="6" Width="80">
                                                            <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("CustomerDate")) %> </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobStage" VisibleIndex="6" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Billing" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Bill" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add Credit Note" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed" %>'
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
                                    <dxtc:TabPage Text="Payment" Visible="false">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                                <dxwgv:ASPxGridView ID="grid_Payment" ClientInstanceName="grid_Payment" runat="server"
                                                    KeyFieldName="Id" Width="900px" AutoGenerateColumns="False" DataSourceID="dsReceipt" OnBeforePerformDataSelect="grid_Payment_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                                                            Width="8%">
                                                            <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("DocNo") %>","/PagesAccount/EditPage/ArReceiptEdit.aspx?no=<%# Eval("DocNo") %>",{title:"<%# Eval("DocNo") %>", fresh:false, external:true});'><%# Eval("DocNo") %></a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Catetory" FieldName="DocType1" VisibleIndex="3"
                                                            Width="5%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="DocCurrency" VisibleIndex="6"
                                                            Width="8%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="DocExRate" VisibleIndex="7"
                                                            Width="8%">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="8"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="9"
                                                            Width="8%">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PostInd" FieldName="ExportInd" VisibleIndex="90"
                                                            Width="8%">
                                                        </dxwgv:GridViewDataTextColumn>

                                                    </Columns>
                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="DocNo" SummaryType="Count" DisplayFormat="{0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="DocAmt" SummaryType="Sum" DisplayFormat="{0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}" />
                                                    </TotalSummary>
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
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed"  %>'>
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

                                                <dxwgv:ASPxGridView ID="grd_Attach" ClientInstanceName="grd_Attach" runat="server" DataSourceID="dsIssuePhoto"
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
                                    <dxtc:TabPage Text="Photos" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl6" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton13" Width="150" runat="server" Text="Upload Photo" AutoPostBack="false" UseSubmitBehavior="false"
                                                                Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed"  %>'>
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

                                                <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsIssuePhoto"
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
                }" />
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="970" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(grid!=null)
	                    grid.Refresh();
	                    grid=null;
                        detailGrid.Refresh();
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
