<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewJobList.aspx.cs" Inherits="PagesContTrucking_Job_NewJobList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript">
        function goJob(jobno) {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
            var type = cbb_new_jobtype.GetText();
            if (type == "IMP" || type == "EXP" || type == "LOC")
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            else if (type == "WGR" || type == "WDO" || type == "TPT")
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            else if (type == "CRA")
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            else if (type == "FRT")
                parent.navTab.openTab(jobno, "/Modules/Tpt/TransferEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function go_page(jobno, type) {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
            if (type == "IMP" || type == "EXP" || type == "LOC")
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            else if (type == "WGR" || type == "WDO" || type == "TPT")
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            else if (type == "CRA")
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            else if (type == "FRT")
                parent.navTab.openTab(jobno, "/Modules/Tpt/TransferEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function PopupTripsList(jobno, contId, canGO) {
            if (canGO != "GO") {
                return;
            }
            popubCtr1.SetHeaderText('Trips List');
            popubCtr1.SetContentUrl('../SelectPage/TripListForJobList.aspx?JobNo=' + jobno + "&contId=" + contId);
            popubCtr1.Show();
        }
        function AfterAddTrip() {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
        }
        function NewAdd_Visible(doShow, par) {
            if (doShow) {
                var t = new Date();
                txt_new_JobDate.SetText(t);
                cbb_new_jobtype.SetValue("");
                btn_new_ClientId.SetText('');
                txt_new_ClientName.SetText('');
                txt_FromAddress.SetText('');
                txt_WarehouseAddress.SetText('');
                txt_ToAddress.SetText('');
                txt_DepotAddress.SetText('');
                txt_new_remark.SetText('');
                cmb_IsTrucking.SetText('Yes');
                cmb_IsWarehouse.SetText('No');
                cbb_Contractor.SetText('No');
                if (par == 'Q') {
                    lbl_Header.SetText('New Quotation');
                    cbb_new_jobstatus.SetText("Quoted");
                }
                if (par == 'J') {
                    lbl_Header.SetText('New Job');
                    cbb_new_jobstatus.SetText("Confirmed");
                }
                ASPxPopupClientControl.Show();
            }
            else {
                ASPxPopupClientControl.Hide();
            }
        }

        function NewAdd_Save() {
            var jobType = cbb_new_jobtype.GetValue();
            //console.log(jobType);
            if (jobType && jobType.length > 0) {
                detailGrid.GetValuesOnCustomCallback('OK', OnSaveCallBack);
            } else {
                alert('Require JobType!');
            }
        }
        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else {
                if (v != null) {

                    goJob(v);

                    ASPxPopupClientControl.Hide();
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td style="display: none">
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Cont No"></dxe:ASPxLabel>
                    </td>
                    <td style="display: none">
                        <dxe:ASPxTextBox ID="txt_search_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td style="display: none">
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cont Status"></dxe:ASPxLabel>
                    </td>
                    <td style="display: none">
                        <dxe:ASPxComboBox ID="cbb_StatusCode" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Value="All" Text="All" />
                                <dxe:ListEditItem Value="New" Text="New" />
                                <%--<dxe:ListEditItem Value="Scheduled" Text="Scheduled" />--%>
                                <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                <%--<dxe:ListEditItem Value="Canceled" Text="Canceled" />--%>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Job Type"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="All" Value="All" Selected="true"/>
                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                                <dxe:ListEditItem Text="WGR" Value="WGR" />
                                <dxe:ListEditItem Text="WDO" Value="WDO" />
                                <dxe:ListEditItem Text="TPT" Value="TPT" />
                                <dxe:ListEditItem Text="CRA" Value="CRA" />
                                <dxe:ListEditItem Text="FRT" Value="FRT" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>

                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Vessel"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Vessel" Width="100" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="From"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>

                </tr>
                <tr>
                    <td>ContNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="search_contNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False" ReadOnly="true">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Width="100%" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td width="2px"></td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="New&nbsp;Job" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
                                    NewAdd_Visible(true,'J');
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td style="display:none">
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="New&nbsp; Quotation" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
                                    NewAdd_Visible(true,'Q');
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <%--<td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add&nbsp;New" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
                                    NewAdd_Visible(true);
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="ASPxPopupClientControl" SkinID="None" Width="240px"
                ShowOnPageLoad="false" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                AllowDragging="True" CloseAction="None" PopupElementID="popupArea"
                EnableViewState="False" runat="server" PopupHorizontalOffset="0"
                PopupVerticalOffset="33" EnableHierarchyRecreation="True">
                <HeaderTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">
                                <dxe:ASPxLabel ID="lbl_Header" ClientInstanceName="lbl_Header" runat="server" Text="New Job"></dxe:ASPxLabel>
                            </td>
                            <td>
                                <a id="a_X" onclick="NewAdd_Visible(false)" onmousedown="event.cancelBubble = true;" style="width: 15px; height: 14px; cursor: pointer;">X</a>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <div style="padding: 2px 2px 2px 2px; width: 690px">
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 680px">
                                <tr>
                                    <td>Client</td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="90">
                                                    <dxe:ASPxButtonEdit ID="btn_new_ClientId" ClientInstanceName="btn_new_ClientId" runat="server" Width="90" AutoPostBack="False">
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_new_ClientId,txt_new_ClientName);
                                                                        }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="250" ID="txt_new_ClientName" ClientInstanceName="txt_new_ClientName">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                    <td>Creation&nbsp;Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="txt_new_JobDate" ClientInstanceName="txt_new_JobDate" runat="server" Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>

                                </tr>
                                <tr>
                                    <td><a href="#" onclick="PopupAddress(txt_FromAddress,null);">From Address</a>
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_FromAddress" Rows="3" Width="340" ClientInstanceName="txt_FromAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Job&nbsp;Type</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_new_jobtype" ClientInstanceName="cbb_new_jobtype" runat="server" Width="100%" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                            <Items>
                                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                                                <dxe:ListEditItem Text="WGR" Value="WGR" />
                                                <dxe:ListEditItem Text="WDO" Value="WDO" />
                                                <dxe:ListEditItem Text="TPT" Value="TPT" />
                                                <dxe:ListEditItem Text="CRA" Value="CRA" />
                                                <dxe:ListEditItem Text="FRT" Value="FRT" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Shipper
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_WarehouseAddress" ClientInstanceName="txt_WarehouseAddress" runat="server" Width="100%"></dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top"><a href="#" onclick="PopupAddress(txt_ToAddress,null);">To Address</a>
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_ToAddress" Rows="3" Width="340" ClientInstanceName="txt_ToAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Trucking</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_IsTrucking" ClientInstanceName="cmb_IsTrucking" runat="server" Value='<%# Eval("IsTrucking") %>' Width="100%">
                                            <Items>
                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                <dxe:ListEditItem Text="No" Value="No" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Warehouse</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_IsWarehouse" ClientInstanceName="cmb_IsWarehouse" runat="server" Value='<%# Eval("IsWarehouse") %>' Width="100%">
                                            <Items>
                                                <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                <dxe:ListEditItem Text="No" Value="No" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        Depot&nbsp;Address
                                    </td>
                                    <td>
                                         <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="90">
                                                    <dxe:ASPxButtonEdit ID="btn_DepotCode" ClientInstanceName="btn_DepotCode" runat="server" Width="120" AutoPostBack="False">
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupAddress(btn_DepotCode,txt_DepotAddress);
                                                                        }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>
                                                     <dxe:ASPxMemo ID="txt_DepotAddress" Rows="1" Width="220" ClientInstanceName="txt_DepotAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>Sub-Contract ?</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_Contractor" ClientInstanceName="cbb_Contractor" runat="server" Value='<%# Eval("Contractor") %>' Width="100%" DropDownStyle="DropDownList">
                                            <Items>
                                                <dxe:ListEditItem Value="YES" Text="Yes" />
                                                <dxe:ListEditItem Value="NO" Text="No" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                   
                                </tr>
                                
                                <tr>
                                    <td>Remark
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_new_remark" Rows="3" Width="340" ClientInstanceName="txt_new_remark"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                     <td>WareHouse</td>
                                    <td>
                                        <div style="display: none">
                                            <dxe:ASPxLabel ID="lb_new_WareHouseId" runat="server" ClientInstanceName="lb_new_WareHouseId"></dxe:ASPxLabel>
                                        </div>
                                        <dxe:ASPxButtonEdit ID="txt_new_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_new_WareHouseId" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="100%" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupWh(txt_new_WareHouseId,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Job&nbsp;Status</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="cbb_new_jobstatus" ReadOnly="true" BackColor="Control" ClientInstanceName="cbb_new_jobstatus" runat="server" Width="100%">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 660px">
                                <tr>
                                    <td colspan="4" style="width: 90%"></td>
                                    <td>

                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save">
                                            <ClientSideEvents Click="function(s,e) {
                                                NewAdd_Save();
                                                 }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="1500" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback" OnBeforePerformDataSelect="grid_Transport_BeforePerformDataSelect">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo">
                        <DataItemTemplate>
                            <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                            <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                <a href='javascript:go_page("<%# Eval("JobNo") %>","<%# Eval("JobType") %>")'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="billed" Caption="Billing"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Job Status" Visible="false">
                        <DataItemTemplate>
                            <%# VilaStatus(SafeValue.SafeString(Eval("JobStatus"),""))%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ScheduleDate" Caption="Schedule Date" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo">
                        <DataItemTemplate>
                            <%# Eval("ContainerNo") %><br />
                            <%--<a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>")' style="display: <%# SafeValue.SafeString(Eval("ContId")).Length>0?"":"none" %>">Link Trips</a>--%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Link Trips" FieldName="trips" Visible="false">
                        <DataItemTemplate>
                            <a class="a_ltrip" href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>","<%# SafeValue.SafeString(Eval("ContId")).Length>0?"GO":"" %>")'>
                                <div class="div_FixWith"><%# xmlChangeToHtml(Eval("trips"),Eval("ContId")) %></div>
                            </a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Cont Type" FieldName="ContainerType" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="StatusCode" Caption="Cont Status" Visible="false">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("StatusCode"))) %>" class="div_contStatus">
                                <%# Eval("StatusCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Depot" Caption="Depot"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PermitNo" Caption="PermitNo"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PortnetStatus" Caption="PortnetStatus"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientRefNo" Caption="Client Ref No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Visible="false"></dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="530"
                Width="980" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      document.getElementById('btn_search').click();
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
