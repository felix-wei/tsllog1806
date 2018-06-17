<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuotationList.aspx.cs" Inherits="PagesContTrucking_Job_QuotationList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
 <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
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
        function PopupTripsList(jobno, contId, canGO, isWarehouse) {
            if (canGO != "GO") {
                return;
            }
            popubCtr1.SetHeaderText('Trips List');
            popubCtr1.SetContentUrl('../SelectPage/TripListForJobList.aspx?JobNo=' + jobno + "&contId=" + contId + "&isWarehouse=" + isWarehouse);
            popubCtr1.Show();
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
                    //parent.navTab.openTab(v, "/Warehouse/Job/JobEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
                    goJob(v);
                    ASPxPopupClientControl.Hide();
                }
            }
        }
        function AfterAddTrip() {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
        }

        function cbb_checkbox_Type(name) {
            //console.log('========= checkbox', name);
            if (name == 'ALL') {
                cb_ContStatus1.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus2.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus3.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus4.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus5.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus6.SetValue(cb_ContStatus0.GetValue());
                cb_ContStatus7.SetValue(cb_ContStatus0.GetValue());
            } else {
                if (cb_ContStatus1.GetValue() && cb_ContStatus2.GetValue() && cb_ContStatus3.GetValue() && cb_ContStatus4.GetValue()
                    && cb_ContStatus5.GetValue() && cb_ContStatus6.GetValue()) {
                    cb_ContStatus0.SetValue(true);
                } else {
                    cb_ContStatus0.SetValue(false);
                }
            }
        }

        function cbb_checkbox_Type1(name) {
            //console.log('========= checkbox', name);
            if (name == 'Uncomplete') {
                if (cb_ContStatus5.GetValue()) {
                    cb_ContStatus1.SetValue(false);
                    cb_ContStatus2.SetValue(false);
                    cb_ContStatus3.SetValue(false);
                    cb_ContStatus4.SetValue(false);
                    cb_ContStatus5.SetValue(false);
                    cb_ContStatus6.SetValue(false);
                }
            } else {
                if (cb_ContStatus1.GetValue() || cb_ContStatus2.GetValue() || cb_ContStatus3.GetValue() || cb_ContStatus4.GetValue()
                    || cb_ContStatus5.GetValue() || cb_ContStatus6.GetValue()) {
                    cb_ContStatus7.SetValue(false);
                }
            }
        }
    </script>
    <style type="text/css">
        a:hover {
            color: black;
        }

        .link a:link {
            color: red;
        }

        .link a:hover {
            color: red;
        }

        .none a:link {
        }


        .a_ltrip span {
            display: inline-block;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding: 2px;
            width: 33px;
            height: 21px;
            overflow: hidden;
            white-space: nowrap;
            text-align: center;
            margin: 2px;
            /*margin-top:2px;
            margin-left:2px;
            margin-bottom:2px;
            margin-right:4px;*/
        }

        .a_ltrip .S {
            background-color: green;
        }

        .a_ltrip .X {
            background-color: gray;
        }

        .a_ltrip .C {
            background-color: blue;
        }

        .a_ltrip .P {
            background-color: orange;
        }


        .a_ltrip .div_FixWith {
            min-width: 112px;
        }

        .div_contStatus {
            width: 80px;
            height: 21px;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding-top: 2px;
        }

        .div_hr {
            width: 30px;
            height: 21px;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            padding-top: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Quotation No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_QuoNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Status"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_QuoteStatus" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="All" Value="All"  Selected="true"/>
                                <dxe:ListEditItem Text="Pending" Value="Pending"/>
                                 <dxe:ListEditItem Text="Quoted" Value="Quoted" />
                                <dxe:ListEditItem Text="Closed" Value="Closed" />
                                <dxe:ListEditItem Text="Failed" Value="Failed" />
                                <dxe:ListEditItem Text="Voided" Value="Voided" />
                            </Items>
                        </dxe:ASPxComboBox>
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

                                    <td>Job&nbsp;Date</td>
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
                    <dxwgv:GridViewDataTextColumn Caption="Quotation No" FieldName="JobNo">
                        <DataItemTemplate>
                            <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                            <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                <a href='javascript:go_page("<%# Eval("QuoteNo") %>","<%# Eval("JobType") %>")'><%# Eval("QuoteNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="QuoteStatus" Caption="Quote Status"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="Job No" Width="120px">
                        <DataItemTemplate>
                            <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Text='<%# Eval("JobNo") %>' Width="80px"></dxe:ASPxLabel>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="billed" Caption="Billing"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="StatusCode" Caption="Job Status" Visible="false">
                        <DataItemTemplate>
                            <%# VilaStatus(SafeValue.SafeString(Eval("StatusCode"),""))%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
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
