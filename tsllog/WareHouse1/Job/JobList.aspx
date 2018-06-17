<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobList.aspx.cs" Inherits="WareHouse_Job_JobList" %>

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
        function ShowHouse(masterId) {
            parent.navTab.openTab(masterId, "/Warehouse/Job/JobEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function ShowInvoice(masterId) {
            parent.navTab.openTab(masterId, "/OpsAccount/ArInvoiceEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function AfterPopubMultiInv1(v) {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            
            parent.navTab.openTab(v, "/Warehouse/Job/JobEdit.aspx?no=" + v, { title: v, fresh: false, external: true });

        }
        function SetPCVisible(doShow) {
            if (doShow) {
                var t = new Date();
                date_IssueDate.SetText(t);
                ASPxPopupClientControl.Show();

            }
            else {
                ASPxPopupClientControl.Hide();
            }
        }
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else if (v != null) {
                txt_CustomerId.SetText();
                txt_CustomerName.SetText();
                cmb_JobType.SetText();
                txt_NewContact.SetText();
                txt_NewFax.SetText();
                txt_NewTel.SetText();
                txt_NewEmail.SetText();
                txt_NewPostalCode.SetText();
                memo_NewAddress.SetText();
                txt_NewRemark.SetText();
                date_IssueDate.SetText();
                ASPxPopupClientControl.Hide();
                parent.navTab.openTab(v, "/Warehouse/Job/JobEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lab_PoNo" runat="server" Text="Quote/Job No">
                        </dxe:ASPxLabel>

                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PoNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="From">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td><dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Type">
                        </dxe:ASPxLabel></td>
                    <td> <dxe:ASPxComboBox ID="cmb_Type" runat="server" Width="100px" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                        <Items>
                            <dxe:ListEditItem Text="Haulier" Value="Haulier" />
                            <dxe:ListEditItem Text="Transport" Value="Transport" />
                            <dxe:ListEditItem Text="Freight" Value="Freight" />
                            <dxe:ListEditItem Text="Warehouse" Value="Warehouse" />
                        </Items>
                        </dxe:ASPxComboBox>
                    </td>
               
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Customer">
                        </dxe:ASPxLabel>
                    </td>
                                        <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustName" runat="server" Width="120px" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                             PopupParty(null,txt_CustName,null,null,null,null,null,null,null,null,'C');
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4" style="display:none;">
                        <%--<dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="100%" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>--%>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <table style="width: 100px; height: 20px;" >
                            <tr>
                                <td id="addnew" style="text-align: center; vertical-align: middle;">
                                    <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="New Job" AutoPostBack="false">
                                        <ClientSideEvents  Click="function(s,e){
                                            SetPCVisible(true);
                                            }"/>
                                    </dxe:ASPxButton>
                                    <%--id="popupArea"--%>
                                </td>
                            </tr>
                        </table>
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
                                                    <dxe:ASPxButtonEdit ID="txt_CustomerId" ClientInstanceName="txt_CustomerId" runat="server"
                                                        Width="90" HorizontalAlign="Left" AutoPostBack="False">
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_CustomerId,txt_CustomerName,txt_NewContact,txt_NewTel,txt_NewFax,txt_NewEmail,txt_NewPostalCode,memo_NewAddress,null,null,'C');
                                                                    }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td >
                                                    <dxe:ASPxTextBox runat="server" Width="250"  ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                  
                                      <td>Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="160px" runat="server" ClientInstanceName="date_IssueDate"
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>

                                </tr>
                               <tr>
                                   <td rowspan="3">Origin</td>
                                   <td rowspan="3" colspan="3">
                                       <dxe:ASPxMemo ID="memo_NewAddress" Rows="5" Width="340" ClientInstanceName="memo_NewAddress"
                                           runat="server">
                                       </dxe:ASPxMemo>
                                   </td>
                                   <td>JobType</td>
                                   <td>
                                       <dxe:ASPxComboBox ID="cmb_JobType" ClientInstanceName="cmb_JobType" runat="server" Width="160" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                           <Items>
                                               <dxe:ListEditItem Text="Haulier" Value="Haulier" />
                                               <dxe:ListEditItem Text="Transport" Value="Transport" />
                                               <dxe:ListEditItem Text="Freight" Value="Freight" />
                                               <dxe:ListEditItem Text="Warehouse" Value="Warehouse" />
                                           </Items>
                                       </dxe:ASPxComboBox>
                                   </td>
                               </tr>
                               <tr>
                                     <td> </td>
                                    <td>
									<div style="display:none">
                                        <dxe:ASPxTextBox    ID="txt_NewContact" Width="160px" runat="server" ClientInstanceName="txt_NewContact" >
                                        </dxe:ASPxTextBox>
									</div>
                                    </td>
                                   </tr>
                               <tr>
                                   <td> </td>
                                   <td>
									<div style="display:none">
                                       <dxe:ASPxTextBox   ID="txt_NewTel" Width="160px" runat="server" ClientInstanceName="txt_NewTel">
                                       </dxe:ASPxTextBox>
									  </div>
                                   </td>
                               </tr>
                               <tr style="text-align: left;">
                                  
                               </tr>
                                <tr>
                                    <td rowspan="3">Destination</td>
                                    <td colspan="3" rowspan="3">
                                        <dxe:ASPxMemo runat="server" Width="340" Rows="4" ID="txt_NewRemark"  ClientInstanceName="txt_NewRemark">
                                        </dxe:ASPxMemo>
                                    </td>
                                     <td></td>
                                   <td>
								   									<div style="display:none">

                                       <dxe:ASPxTextBox  ID="txt_NewEmail" Width="160px" runat="server" ClientInstanceName="txt_NewEmail">
                                       </dxe:ASPxTextBox>
									   </div>
                                   </td>
                                </tr>
                               <tr>
                                   <td> </td>
                                   <td>
								   									<div style="display:none">

                                       <dxe:ASPxTextBox ID="txt_NewFax" Width="160px" runat="server" ClientInstanceName="txt_NewFax">
                                       </dxe:ASPxTextBox>
									   </div>
                                   </td>
                               </tr>
                               <tr>
                                   <td> </td>
                                   <td>
									<div style="display:none">

                                       <dxe:ASPxTextBox   runat="server" Width="160px" ID="txt_NewPostalCode" ClientInstanceName="txt_NewPostalCode">
                                       </dxe:ASPxTextBox>
									   </div>
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
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared"
                KeyFieldName="Id" Width="1600px" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="20">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("QuotationNo") %>');">Open</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Quote No" FieldName="QuotationNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Descending" Width="100">
                    </dxwgv:GridViewDataTextColumn>
					<dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" VisibleIndex="1"
                        SortIndex="2" SortOrder="Descending" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Client Name" FieldName="CustomerName" VisibleIndex="1" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Contact" VisibleIndex="1" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Origin" FieldName="OriginAdd" VisibleIndex="1" Width="40">
		
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Destination" FieldName="DestinationAdd" VisibleIndex="1" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Volume/Loads" FieldName="VolumneRmk" VisibleIndex="1" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="JobType" VisibleIndex="1"
                       Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Invoice" FieldName="WorkStatus" VisibleIndex="1"
                       Width="80">
					   <DataItemTemplate><%# InvoiceNo(Eval("QuotationNo","{0}"),Eval("JobNo","{0}"),"")%></DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Insurance" FieldName="WorkStatus" VisibleIndex="1"
                       Width="80">
					   <DataItemTemplate><%# InvoiceNo(Eval("QuotationNo","{0}"),Eval("JobNo","{0}"),"INS")%></DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="WorkStatus" VisibleIndex="1"
                       Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Process" FieldName="JobStage" VisibleIndex="1"
                        Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Mode" FieldName="Mode" VisibleIndex="1"
                        Width="100">
			<DataItemTemplate>
				<%# Eval("Mode","{0}") == "Local" ? "" : Eval("Mode") %>
			</DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pack Date" FieldName="PackRmk" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Move Date" FieldName="MoveRmk" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="C/S" FieldName="CreateBy" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Surveyor" FieldName="Value2" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Sales" FieldName="Value4" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Supervisor" FieldName="Value6" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="ViaWh" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Created By" FieldName="CreateBy" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Creation" FieldName="CreateDateTime" VisibleIndex="4" Width="40">
					<DataItemTemplate><%# Eval("CreateDateTime","{0:dd/MMM HH:mm}")%></DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="970" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
