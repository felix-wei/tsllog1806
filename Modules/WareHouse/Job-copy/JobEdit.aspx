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
        function PopupNewJob() {
            if (txt_DoNo.GetText() == "NEW" && txt_SchRefNo.GetText()=="") {
                popubCtr.SetHeaderText('New Job');
                popubCtr.SetContentUrl('/Modules/WareHouse/Job/JobBook.aspx?no=0');
                popubCtr.Show();
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
                window.location = 'JobEdit.aspx?no=' + v;
                //txt_SchRefNo.SetText(v);
                //txt_DoNo.SetText(v);
            }
        }
        function SetPCVisible(doShow) {
            if (doShow && txt_DoNo.GetText() == "NEW" && txt_SchRefNo.GetText() == "") {

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
            else if (v == "Success") {
                alert(v);
                detailGrid.Refresh();
                Grid_Costing.Refresh();
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
        function MultiplePickProduct() {
            popubCtr.SetHeaderText('Multiple Product');
            popubCtr.SetContentUrl('../SelectPage/MultipleProduct.aspx?Type=SO&Sn=' + txt_DoNo.GetText() + '&Wh=' + txt_WareHouseId.GetText());
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
            grid_SKULine.Refresh();
            grid_PoRequest.Refresh();
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
    </script>
</head>
<body onload="SetPCVisible(true)">
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
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobCosting"
                KeyMember="Id"  />
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
            <table>
                <tr>
                    <td>Jo No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SchRefNo" Width="150" ClientInstanceName="txt_SchRefNo" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='JobEdit.aspx?no='+txt_SchRefNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>

                    <td>
                         <dxe:ASPxButton ID="btn_add" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                    </dxe:ASPxButton>
                        <table style="width: 100px; height: 20px; display:none" id="popupArea">
                            <tr>
                                <td id="addnew" style="text-align: center; vertical-align: middle;">
                                    <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
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
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="160px" runat="server"
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
                                       <dxe:ASPxComboBox ID="cmb_JobType" ClientInstanceName="cmb_JobType" runat="server" Width="160" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                           <Items>
                                               <dxe:ListEditItem Text="Local Move" Value="Local Move" />
                                               <dxe:ListEditItem Text="Office Move" Value="Office Move" />
                                               <dxe:ListEditItem Text="International Move" Value="International Move" />
                                               <dxe:ListEditItem Text="Store & Move" Value="Store & Move" />
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
                        
                        <table  bordercolor="#000000" style="text-align: center; border:solid 1px #000000; border-spacing: 0px; width:960px">
                            <tr>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000;"></td>
                                <td  style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000;">
                                    <dxe:ASPxLabel ID="lbl_Inquir" Width="120px" runat="server" Text="Customer Inquir"></dxe:ASPxLabel>
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
                                    <dxe:ASPxLabel ID="lbl_Completion" Width="150px" runat="server" Text="Job Completion"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000; border-right:solid 1px #000000">
                                    <dxe:ASPxLabel ID="lbl_Billing" Width="120px" runat="server" Text="Billing"></dxe:ASPxLabel>
                                </td>
                                <td style="text-align: center ; border-bottom:solid 1px #000000;">
                                    <dxe:ASPxLabel ID="lbl_Close" Width="140px" runat="server" Text="Job Close"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr style="text-align: left;">
                                <td style="border-bottom:solid 1px #000000; border-right:solid 1px #000000;"></td>
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
                                                <dxe:ASPxLabel runat="server" ID="lbl_Cs" Text='<%# Eval("Value1") %>'></dxe:ASPxLabel>
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
                                <td style=" border-right:solid 1px #000000; width:60px;">Print</td>
                                <td style=" border-right:solid 1px #000000;"> <a href="#">Job Sheet</a></td>
                                <td style=" border-right:solid 1px #000000;"> <a href='/ReportJob/PrintSiteSurvey.aspx?no=<%# Eval("JobNo") %>&type=<%# Eval("JobType") %>' target="_blank">Survey Form</a></td>
                                <td style=" border-right:solid 1px #000000;"><a href="#" onclick="">Costing</a></td>
                                <td style=" border-right:solid 1px #000000;"><a href="#" onclick="">Quotation</a></td>
                                <td style=" border-right:solid 1px #000000;"><a href="#" onclick="">Job Confirmation</a></td>
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
                                    <td>
                                        <dxe:ASPxButton ID="btn_ConfirmPI" Width="90" runat="server" Text="Confirm" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")=="Quotation" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                               detailGrid.GetValuesOnCustomCallback('Confirm',OnCloseCallBack);
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_AddDoOut" ClientInstanceName="btn_AddDoOut" runat="server" Width="90" Text="Create DO" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")=="Job Confirmation" %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                               CreateDeliveryOrder();               
                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CreateBill" Width="120" runat="server" Text="Create Invoice" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")=="Job Completion"  %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                 AddInvoice1(Grid_Invoice, 'WH', txt_DoNo.GetText(), 'JO','IV');
                                                                
                                                                    }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CloseJob" ClientInstanceName="btn_CloseJob" runat="server" Width="80" Text="Close" AutoPostBack="False"
                                            UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                            <ClientSideEvents Click="function(s, e) {
                                                                        detailGrid.GetValuesOnCustomCallback('Close',OnCloseCallBack);                 

                                        }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>

                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save" Enabled='<%# SafeValue.SafeString(Eval("JobStage"),"")!="Job Close" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
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
                                                <dxe:ListEditItem Text="Job Completion" Value="Job Completion" />
                                                <dxe:ListEditItem Text="Billing" Value="Billing" />
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
                                                <dxe:ListEditItem Text="Store & Move" Value="Store & Move" />
                                                <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                                <dxe:ListEditItem Text="Project" Value="Project" />
                                            </Items>
                                        </dxe:ASPxComboBox>
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
                                    
                                    <td>INCO Term</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_IncoTerm" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("IncoTerm")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="CFR" Value="CFR" />
                                                <dxe:ListEditItem Text="CIF" Value="CIF" />
                                                <dxe:ListEditItem Text="CIP" Value="CIP" />
                                                <dxe:ListEditItem Text="CPT" Value="CPT" />
                                                <dxe:ListEditItem Text="DAF" Value="DAF" />
                                                <dxe:ListEditItem Text="DAP" Value="DAP" />
                                                <dxe:ListEditItem Text="DAT" Value="DAT" />
                                                <dxe:ListEditItem Text="DDP" Value="DDP" />
                                                <dxe:ListEditItem Text="DDU" Value="DDU" />
                                                <dxe:ListEditItem Text="DES" Value="DES" />
                                                <dxe:ListEditItem Text="DEQ" Value="DEQ" />
                                                <dxe:ListEditItem Text="EXW" Value="EXW" />
                                                <dxe:ListEditItem Text="FAS" Value="FAS" />
                                                <dxe:ListEditItem Text="FOB" Value="FOB" />
                                            </Items>
                                        </dxe:ASPxComboBox>
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
                                                <dxe:ListEditItem Text="PENDING" Value="PENDING" />
                                                <dxe:ListEditItem Text="WORKING" Value="WORKING" />
                                                <dxe:ListEditItem Text="COMPLETE" Value="COMPLETE" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Surveyer</td>
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
                                                <table style="width: 100%">
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
                                                                    <td colspan="4" style="background-color: Gray; color: White; width: 625px">
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
                                                                    <td>Pack DateTime</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Pack" Width="180" runat="server" Value='<%# Eval("PackDate") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
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

                                                                    <td>Move DateTime</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_MoveDate" Width="180" runat="server" Value='<%# Eval("MoveDate") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
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
                                                                </tr>
                                                                <tr>
                                                                    <td>Mode</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" Rows="4" ID="txt_Mode" Text='<%# Eval("Mode") %>' ClientInstanceName="txt_Mode">
                                                                        </dxe:ASPxTextBox>
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
                                                    <tr style="text-align: left;">
                                                        <td colspan="2">Full Packing</td>
                                                        <td>
                                                            <dxe:ASPxComboBox Width="100" ID="cmb_FullPacking" Text='<%# Eval("Item1") %>'
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
                                                        <td colspan="2">Piano Apply</td>
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
                                                        <td colspan="2">Complimentory</td>
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
                                                        <td colspan="2">Complimentory</td>
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
                                                        <td colspan="2">Shifting Services</td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Shifting"
                                                                runat="server" Text='<%# Eval("Item9") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td colspan="2">Complimentory</td>
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
                                                        <td colspan="2">Complimentory</td>
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
                                                        <td colspan="2">Complimentory</td>
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
                                                    <tr style="text-align: left; vertical-align: top">
                                                        <td colspan="2">Notes</td>
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
                                    <dxtc:TabPage Text="Billing Instruction" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Cost" runat="server">
                                                <table cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2b" Width="150" runat="server" Text="Add Charges" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s,e) {
													PickBill();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2d" Width="150" runat="server" Text="Add Cost" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click="function(s,e) {
													PickCost();
                                                        }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr>
                                                <dxwgv:ASPxGridView ID="grid_Cost" ClientInstanceName="grid_Cost" runat="server"
                                                    DataSourceID="dsCosting" KeyFieldName="Id" Width="100%" OnBeforePerformDataSelect="grid_Cost_DataSelect" OnInit="grid_Cost_Init" OnInitNewRow="grid_Cost_InitNewRow" OnRowInserting="grid_Cost_RowInserting"
                                                    OnRowUpdating="grid_Cost_RowUpdating" OnRowInserted="grid_Cost_RowInserted" OnRowUpdated="grid_Cost_RowUpdated" OnHtmlEditFormCreated="grid_Cost_HtmlEditFormCreated" OnRowDeleting="grid_Cost_RowDeleting">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="Inline" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                                                            <EditButton Visible="True" />
                                                            <DeleteButton Visible="true" />
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Type" FieldName="Status1" Width="50" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>

                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="ChgCode" Caption="Charge" Width="90" VisibleIndex="1">
                                                            <PropertiesComboBox DataSourceID="dsRate" ValueType="System.String" TextField="ChgCodeId" ValueField="ChgCodeId">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgCodeDes" Width="150" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="Unit" Caption="Unit" Width="80" VisibleIndex="5">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="20FT" Value="20FT"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="40FT" Value="40FT"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="40HQ" Value="40HQ"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="45FT" Value="45FT"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="4M3" Value="4M3"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="SET" Value="SET"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="SHPT" Value="SHPT"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="WT/M3" Value="WT/M3"></dxe:ListEditItem>
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Qty" FieldName="SaleQty" VisibleIndex="3" Width="50">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Price" FieldName="SalePrice" VisibleIndex="3" Width="50">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Total" FieldName="SaleDocAmt" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="CostDate" Caption="Billing Date" VisibleIndex="9"
                                                            Width="100">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" Width="90" EditFormatString="dd/MM/yyyy" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Bill No" FieldName="DocNo" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Cont No" FieldName="Status2" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="HBL No" FieldName="Status3" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Party" FieldName="Status4" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Remark" FieldName="Remark" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataColumn>

                                                        <dxwgv:GridViewDataColumn FieldName="RowUpdateUser" Caption="Update By" VisibleIndex="9"
                                                            Width="100">
                                                            <DataItemTemplate><%# Eval("RowUpdateUser") %></DataItemTemplate>
                                                            <EditItemTemplate><%# Eval("RowUpdateUser") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn FieldName="RowUpdateTime" Caption="Time" VisibleIndex="9" Width="100">
                                                            <DataItemTemplate><%# Eval("RowUpdateTime","{0:dd/MM HH:mm}") %></DataItemTemplate>
                                                            <EditItemTemplate><%# Eval("RowUpdateTime","{0:dd/MM HH:mm}") %></EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>

                                                    </Columns>

                                                    <Settings ShowFooter="True" />
                                                    <TotalSummary>
                                                        <dxwgv:ASPxSummaryItem FieldName="ChgCode" ShowInGroupFooterColumn="ChgCode" SummaryType="Count"
                                                            DisplayFormat="{0:0}" />
                                                        <dxwgv:ASPxSummaryItem FieldName="SaleDocAmt" ShowInColumn="SaleDocAmt" SummaryType="Sum"
                                                            DisplayFormat="{0:0.00}" />
                                                    </TotalSummary>

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
                                                                        <dxe:ASPxTextBox Width="265" ID="txt_CostDes" ClientInstanceName="txt_CostDes"
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
                                                                    PopupVendor(txt_CostVendorId,txt_CostVendorName);
                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <dxe:ASPxTextBox runat="server" Width="200" ID="txt_CostVendorName"
                                                                            ClientInstanceName="txt_CostVendorName" ReadOnly="true" Text=''>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Job No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox Width="100" ID="txt_CostJobNo" ClientInstanceName="txt_CostJobNo"
                                                                            runat="server" Text='<%# Bind("JobNo") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Split Type
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbo_SplitType" runat="server" Text='<%# Bind("SplitType")%>' Width="205">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Set" Value="Set" />
                                                                                <dxe:ListEditItem Text="WtM3" Value="WtM3" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Remark
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Doc No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100" ID="txt_DocNo" ClientInstanceName="txt_DocNo" Text='<%# Bind("DocNo") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <table>
                                                                            <td>Pay on Behalf
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_PayInd" Width="60" runat="server"
                                                                                    Text='<%# Bind("PayInd") %>'>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Y" Value="Y" />
                                                                                        <dxe:ListEditItem Text="N" Value="N" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td>Verify Ind
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_VerifryInd" Width="60" runat="server"
                                                                                    Text='<%# Bind("VerifryInd") %>'>
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="Y" Value="Y" />
                                                                                        <dxe:ListEditItem Text="N" Value="N" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </table>
                                                                    </td>
                                                                    <td>Salesman
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Salesman"
                                                                            DataSourceID="dsSalesman" TextField="Code" ValueField="Code" Width="100%" Value='<%# Bind("Salesman")%>'>
                                                                        </dxe:ASPxComboBox>
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
                                                                                    <dxe:ASPxButtonEdit ID="cmb_CostSaleCurrency" ClientInstanceName="cmb_CostSaleCurrency" runat="server" Width="80" Text='<%# Bind("SaleCurrency") %>' MaxLength="3">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostSaleCurrency,null);
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
                                                                                        ReadOnly="true" runat="server" Width="120" ID="spin_CostSaleAmt"
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
                                                                                    <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" runat="server" Width="80" Text='<%# Bind("CostCurrency") %>' MaxLength="3">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostCostCurrency,null);
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
                                                                                        ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                                                        Value='<%# Eval("CostLocAmt")%>'>
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'>
                                                                    <ClientSideEvents Click="function(s,e){grid_Cost.UpdateEdit();}" />
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
                                    <dxtc:TabPage Text="Job Schedule" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl4" runat="server">
                                                <table style="width: 900px;">
                                                    <tr>
                                                        <td colspan="9" style="background-color: Gray; color: White;">
                                                            <b>Supervisor Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Code</td>
                                                        <td colspan="4">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_AgentCode" ClientInstanceName="txt_AgentCode" runat="server"
                                                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("AgentId") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPersonInfo(txt_AgentCode,null,txt_AgentName,txt_AgentContact,txt_AgentTelexFax,txt_AgentCountry,txt_AgentCity,txt_AgentPostalCode,memo_AgentAddress,'Supervisor');
                                                                        }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ReadOnly="true" BackColor="Control" runat="server" Width="260" ID="txt_AgentName" Text='<%# Eval("AgentName") %>' ClientInstanceName="txt_AgentName">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>Tel/Fax</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_AgentTelexFax" Text='<%# Eval("AgentTel") %>' ClientInstanceName="txt_AgentTelexFax">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Contact</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_AgentContact" Text='<%# Eval("AgentContact") %>' ClientInstanceName="txt_AgentContact">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3">Address</td>
                                                        <td colspan="4" rowspan="3">
                                                            <dxe:ASPxMemo ID="memo_AgentAddress" Rows="4" Width="351" ClientInstanceName="memo_AgentAddress"
                                                                runat="server" Text='<%# Eval("AgentAdd") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Postal Code</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_AgentPostalCode" Text='<%# Eval("AgentZip") %>' ClientInstanceName="txt_AgentPostalCode">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>City</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_AgentCity" ClientInstanceName="txt_AgentCity" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("AgentCity") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_AgentCity)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Country</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_AgentCountry" ClientInstanceName="txt_AgentCountry" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("AgentCountry") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCountry(null,txt_AgentCountry)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="9" style="background-color: Gray; color: White;">
                                                            <b>Crew Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Code</td>
                                                        <td colspan="4">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_NotifyCode" ClientInstanceName="txt_NotifyCode" runat="server"
                                                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("NotifyId") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupPersonInfo(txt_NotifyCode,null,txt_NotifyName,txt_NotifyContact,txt_NotifyTelexFax,txt_NotifyCountry,txt_NotifyCity,txt_NotifyPostalCode,memo_NotifyAddress,'Crew');
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ReadOnly="true" BackColor="Control" runat="server" Width="260" ID="txt_NotifyName" Text='<%# Eval("NotifyName") %>' ClientInstanceName="txt_NotifyName">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="auto-style1">Tel/Fax</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_NotifyTelexFax" runat="server" ClientInstanceName="txt_NotifyTelexFax" Text='<%# Eval("NotifyTel") %>' Width="150px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Contact</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_NotifyContact" runat="server" ClientInstanceName="txt_NotifyContact" Text='<%# Eval("NotifyContact") %>' Width="150px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3">Address</td>
                                                        <td colspan="4" rowspan="3">
                                                            <dxe:ASPxMemo ID="memo_NotifyAddress" runat="server" ClientInstanceName="memo_NotifyAddress" Rows="4" Text='<%# Eval("NotifyAdd") %>' Width="350px">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Postal Code</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_NotifyPostalCode" runat="server" ClientInstanceName="txt_NotifyPostalCode" Text='<%# Eval("NotifyZip") %>' Width="150px">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>City</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_NotifyCity" runat="server" ClientInstanceName="txt_NotifyCity" HorizontalAlign="Left" Text='<%# Eval("NotifyCity") %>' Width="150px">
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCity(null,txt_NotifyCity)
                                                                    }" />
                                                                <Buttons>
                                                                    <dxe:EditButton Text="..">
                                                                    </dxe:EditButton>
                                                                </Buttons>
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Country</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_NotifyCountry" runat="server" ClientInstanceName="txt_NotifyCountry" HorizontalAlign="Left" Text='<%# Eval("NotifyCountry") %>' Width="150px">
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupCountry(null,txt_NotifyCountry)
                                                                    }" />
                                                                <Buttons>
                                                                    <dxe:EditButton Text="..">
                                                                    </dxe:EditButton>
                                                                </Buttons>
                                                            </dxe:ASPxButtonEdit>
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
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed"  %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Auto SO Inv" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice, "WH", txt_DoNo.GetText(), "JO","IV");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Auto DO Inv" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice, "WH", txt_DoNo.GetText(), "DO","IV");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton11" Width="125" runat="server" Text="Auto CN" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice1(Grid_Invoice, "WH", txt_DoNo.GetText(), "JO","CN");
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
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed"  %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("JobStage"),"")!="Job Closed" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable, "WH", txt_DoNo.GetText(), "0");
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Payable" ClientInstanceName="Grid_Payable"
                                                    runat="server" KeyFieldName="SequenceId" DataSourceID="dsVoucher" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Payable_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowPayable(Grid_Payable,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintPayable("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
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
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Attach_RowDeleting" OnInit="grd_Attach_Init" OnRowUpdating="grd_Attach_RowUpdating">
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
