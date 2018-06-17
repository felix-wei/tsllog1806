﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobList.aspx.cs" Inherits="WareHouse_Job_JobList" %>

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
            window.location = "JobEdit.aspx?no=" + masterId;
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function AfterPopubMultiInv1(v) {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            parent.navTab.openTab(v, "/Modules/WareHouse/Job/JobEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
        }
        function SetPCVisible(doShow) {
            if (doShow) {

                ASPxPopupClientControl.Show();
            }
            else {
                ASPxPopupClientControl.Hide();
            }
        }
        function OnSaveCallBack(v) {
            if (v != null) {
                parent.navTab.openTab(v, "/Modules/WareHouse/Job/JobEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
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
                        <dxe:ASPxLabel ID="lab_PoNo" runat="server" Text="No">
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
                                <dxe:ListEditItem Text="Draft" Value="Draft" />
                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                <dxe:ListEditItem Text="Closed" Value="Closed" />
                                <dxe:ListEditItem Text="Canceled" Value="Canceled" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Customer">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120px" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                             PopupParty(txt_CustId,txt_CustName,null,null,null,null,null,null,'C');
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="100%" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
<%--					                                    <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false">
									<ClientSideEvents Click="function(s, e) {
                                    ShowHouse('0');	
                                    }" />
                                    </dxe:ASPxButton>--%>
                        <table style="width: 100px; height: 20px;" id="popupArea">
                            <tr>
                                <td id="addnew" style="text-align: center; vertical-align: middle;">
                                    <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                    </dxe:ASPxButton>
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
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="250" ReadOnly="true" BackColor="Control" ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
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
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="20">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("JobNo") %>');">Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="JobNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Descending" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobStage" VisibleIndex="1" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Date Time" FieldName="JobDate" VisibleIndex="2" Width="50">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="4" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Pay Term" FieldName="PayTerm" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="INCO Term" FieldName="IncoTerm" VisibleIndex="4" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" VisibleIndex="4" Width="40">
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
