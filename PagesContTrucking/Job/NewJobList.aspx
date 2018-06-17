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
        function go_page(jobno) {
            var type = cbb_new_jobtype.GetText();
            if (type == "FRT") {
                parent.navTab.openTab(jobno, "/Modules/Tpt/TransferEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
            else {
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
        }
        function open_job(jobno, type) {
            if (type == "FRT") {
                parent.navTab.openTab(jobno, "/Modules/Tpt/TransferEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
            else {
                parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
            }
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
                cb_Trucking.SetChecked(false);
                cb_Warehouse.SetChecked(false);
                cb_Transport.SetChecked(false);
                cb_Crane.SetChecked(false);
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
            btn_Ref_Save.SetEnabled(false);
            btn_AddJob.SetEnabled(false);
            var jobType = cbb_new_jobtype.GetValue();
            //console.log(jobType);
            if (jobType && jobType.length > 0) {
                detailGrid.GetValuesOnCustomCallback('OK', OnSaveCallBack);
            } else {
                alert('Require JobType!');
            }
        }
        function OnSaveCallBack(v) {
            btn_Ref_Save.SetEnabled(true);
            btn_AddJob.SetEnabled(true);
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else {
                if (v != null) {

                    go_page(v);

                    ASPxPopupClientControl.Hide();
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="table table-responsive">
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
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Job Type" Width="60"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="All" Value="All" Selected="true"/>
                                <dxe:ListEditItem Text="CI - Container Import" Value="IMP" />
                                <dxe:ListEditItem Text="CE - Container Export" Value="EXP" />
                                <dxe:ListEditItem Text="CL - Container Local " Value="LOC" />
                                <dxe:ListEditItem Text="LR - Warehouse Receipt" Value="WGR" />
                                <dxe:ListEditItem Text="LD - Warehouse Delivery" Value="WDO" />
                                <dxe:ListEditItem Text="LL - Local Delivery" Value="TPT" />
                                <dxe:ListEditItem Text="LI - Local Import" Value="LI" />
                                <dxe:ListEditItem Text="LE - Local Export" Value="LE" />
                                <dxe:ListEditItem Text="CR - Crane" Value="CRA" />
                                <dxe:ListEditItem Text="CT - Container Tranship" Value="CT" />
                                <dxe:ListEditItem Text="LT - Loose Tranship" Value="LT" />
                                <dxe:ListEditItem Text="RS - Rental of Space" Value="ROS" />
                                <dxe:ListEditItem Text="RE - Rental of Equipment" Value="RE" />
                                <dxe:ListEditItem Text="MS - Miscellaneous" Value="MS" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="Job Status" Width="70"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_Status" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="All" Value="All" Selected="true"/>
                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed"/>
                                <dxe:ListEditItem Text="Billing" Value="Billing" />
                                <dxe:ListEditItem Text="Completed" Value="Completed" />
                                <dxe:ListEditItem Text="Voided" Value="Voided" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                     <td><dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Master JobNo" Width="80"></dxe:ASPxLabel></td>
                    <td> <dxe:ASPxTextBox ID="txt_MasterJobNo" Width="100" runat="server"></dxe:ASPxTextBox></td>
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
                    <td >
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td colspan="2">
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
                    <td colspan="6">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="3">
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Width="100%" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td width="2px"></td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_Add" ClientInstanceName="btn_AddJob" runat="server" Text="New&nbsp;Job" Width="100%" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
                                    NewAdd_Visible(true,'J');
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td width="2px"></td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
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
                        <div style="padding: 2px 2px 2px 2px; width: 360px">
                            <table style="text-align: left; padding: 2px 2px 2px 2px; width: 350px">
                                <tr>
                                    <td>Creation&nbsp;Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="txt_new_JobDate" ClientInstanceName="txt_new_JobDate" runat="server" Width="180" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>

                                </tr>
                                <tr>
                                    <td>Job&nbsp;Type</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_new_jobtype" ClientInstanceName="cbb_new_jobtype" runat="server" Width="180" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                            <Items>
                                                <dxe:ListEditItem Text="All" Value="All" Selected="true" />
                                                <dxe:ListEditItem Text="CI - Container Import" Value="IMP" />
                                                <dxe:ListEditItem Text="CE - Container Export" Value="EXP" />
                                                <dxe:ListEditItem Text="CL - Container Local " Value="LOC" />
                                                <dxe:ListEditItem Text="LR - Warehouse Receipt" Value="WGR" />
                                                <dxe:ListEditItem Text="LD - Warehouse Delivery" Value="WDO" />
                                                <dxe:ListEditItem Text="LL - Local Delivery" Value="TPT" />
                                                <dxe:ListEditItem Text="LI - Local Import" Value="LI" />
                                                <dxe:ListEditItem Text="LE - Local Export" Value="LE" />
                                                <dxe:ListEditItem Text="CR - Crane" Value="CRA" />
                                                <dxe:ListEditItem Text="CT - Container Tranship" Value="CT" />
                                                <dxe:ListEditItem Text="LT - Loose Tranship" Value="LT" />
                                                <dxe:ListEditItem Text="RS - Rental of Space" Value="ROS" />
                                                <dxe:ListEditItem Text="RE - Rental of Equipment" Value="RE" />
                                                <dxe:ListEditItem Text="MS - Miscellaneous" Value="MS" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>WareHouse</td>
                                    <td>
                                        <div style="display: none">
                                            <dxe:ASPxLabel ID="lb_new_WareHouseId" runat="server" ClientInstanceName="lb_new_WareHouseId"></dxe:ASPxLabel>
                                        </div>
                                        <dxe:ASPxButtonEdit ID="txt_new_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_new_WareHouseId" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="180px" AutoPostBack="False">
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
                                    <td>Trucking</td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="cb_Trucking" ClientInstanceName="cb_Trucking" runat="server">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Warehouse</td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="cb_Warehouse" ClientInstanceName="cb_Warehouse" runat="server">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Transport</td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="cb_Transport" ClientInstanceName="cb_Transport" runat="server">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Crane</td>
                                    <td>
                                        <dxe:ASPxCheckBox ID="cb_Crane" ClientInstanceName="cb_Crane" runat="server">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                </tr>

                                <tr style="display:none">
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
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 360px">
                                <tr>
                                    <td colspan="4" style="width: 90%"></td>
                                    <td>

                                        <dxe:ASPxButton ID="btn_Ref_Save" ClientInstanceName="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
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
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" OnPageIndexChanged="grid_Transport_PageIndexChanged"
 KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback" OnBeforePerformDataSelect="grid_Transport_BeforePerformDataSelect">
                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="400"/>
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" Width="160" FixedStyle="Left">
                        <DataItemTemplate>
                            <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                            <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                <a href='javascript:open_job("<%# Eval("JobNo") %>","<%# Eval("JobType") %>")'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Width="60" Caption="JobType" FixedStyle="Left"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsTrucking" Caption="CT" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsTrucking","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsWarehouse" Caption="WH" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsWarehouse","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsLocal" Caption="TP" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsLocal","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsAdhoc" Caption="CR" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsAdhoc","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Urgent" FieldName="UrgentInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"background-color:red;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"Y":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Status" Visible="false">
                        <DataItemTemplate>
                            <%# SafeValue.SafeString(Eval("JobStatus")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From" CellStyle-Wrap="True" >
                        <DataItemTemplate>
                            <div style="word-break:break-all;word-spacing:normal;word-wrap:break-word;width:auto"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To" CellStyle-Wrap="True">
                        <DataItemTemplate>
                            <div style="width:auto;word-break:break-all;word-spacing:normal;word-wrap:break-word"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="billed" Caption="Billing" Width="60"></dxwgv:GridViewDataColumn>
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
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
