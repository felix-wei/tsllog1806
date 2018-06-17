<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TptJobList.aspx.cs" Inherits="PagesContTrucking_Job_TptJobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">

        function goJob(jobno) {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
            parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function PopupTripsList(jobno, tripId,tripIndex, canGO) {
            if (canGO != "GO") {
                return;
            }
            popubCtr1.SetHeaderText('Trip Edit');
            popubCtr1.SetContentUrl('/Modules/Tpt/SelectPage/TripEditForJobList.aspx?JobNo=' + jobno + "&tripId=" + tripId + "&tripIndex=" + tripIndex);
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
                txt_new_WareHouseId.SetText(lb_new_WareHouseId.GetText());

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
                if (cb_cs_Uncomplete.GetValue()) {
                    cb_cs_New.SetValue(false);
                    cb_cs_Completed.SetValue(false);
                    //cb_cs_Uncomplete.SetValue(false);
                    cb_cs_Import.SetValue(false);
                    cb_cs_Return.SetValue(true);
                    cb_cs_Collection.SetValue(false);
                    cb_cs_Export.SetValue(true);
                    cb_cs_WHSMT.SetValue(false);
                    cb_cs_WHSLD.SetValue(false);
                    cb_cs_CustomerMT.SetValue(false);
                    cb_cs_CustomerLD.SetValue(false);
                } else {
                    cb_cs_New.SetValue(true);
                    cb_cs_Completed.SetValue(false);
                    //cb_cs_Uncomplete.SetValue(false);
                    cb_cs_Import.SetValue(true);
                    cb_cs_Return.SetValue(true);
                    cb_cs_Collection.SetValue(true);
                    cb_cs_Export.SetValue(true);
                    cb_cs_WHSMT.SetValue(false);
                    cb_cs_WHSLD.SetValue(false);
                    cb_cs_CustomerMT.SetValue(false);
                    cb_cs_CustomerLD.SetValue(false);
                }
            } else {
                //if (cb_ContStatus1.GetValue() || cb_ContStatus2.GetValue() || cb_ContStatus3.GetValue() || cb_ContStatus4.GetValue()
                //    || cb_ContStatus5.GetValue() || cb_ContStatus6.GetValue()) {
                //    cb_ContStatus7.SetValue(false);
                //}
                if (name == 'Completed') {
                    if (cb_cs_Completed.GetValue()) {
                        cb_cs_New.SetValue(false);
                        //cb_cs_Completed.SetValue(false);
                        //cb_cs_Uncomplete.SetValue(false);
                        cb_cs_Import.SetValue(false);
                        cb_cs_Return.SetValue(false);
                        cb_cs_Collection.SetValue(false);
                        cb_cs_Export.SetValue(false);
                        cb_cs_WHSMT.SetValue(false);
                        cb_cs_WHSLD.SetValue(false);
                        cb_cs_CustomerMT.SetValue(false);
                        cb_cs_CustomerLD.SetValue(false);
                    } else {
                        cb_cs_New.SetValue(true);
                        //cb_cs_Completed.SetValue(false);
                        //cb_cs_Uncomplete.SetValue(false);
                        cb_cs_Import.SetValue(true);
                        cb_cs_Return.SetValue(true);
                        cb_cs_Collection.SetValue(true);
                        cb_cs_Export.SetValue(true);
                        cb_cs_WHSMT.SetValue(true);
                        cb_cs_WHSLD.SetValue(true);
                        cb_cs_CustomerMT.SetValue(true);
                        cb_cs_CustomerLD.SetValue(true);
                    }
                } else {
                    cb_cs_Completed.SetValue(false);
                    cb_cs_Uncomplete.SetValue(false);
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
            min-width: 60px;
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

        .tb_split {
            border-left: 1px solid red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
<td>Job No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Job Type" Width="50px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="80" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dxe:ListEditItem Text="WGR" Value="WGR" />
                                <dxe:ListEditItem Text="WDO" Value="WDO" />
                                <dxe:ListEditItem Text="TPT" Value="TPT" />
                                 
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Subcontract
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_subContract" runat="server" DropDownStyle="DropDownList" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="ALL" Text="ALL" Selected="true" />
                                <dxe:ListEditItem Value="YES" Text="YES" />
                                <dxe:ListEditItem Value="NO" Text="NO" />
                            </Items>
                        </dxe:ASPxComboBox>
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
                                                                        PopupParty(btn_ClientId,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
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
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                     </td>
                    <td>
                        <dxe:ASPxButton ID="btn_save2Excel" runat="server" Text="Save To Excel" OnClick="btn_save2Excel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
                <tr>
					<td>Search By
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Keyword" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="6">
						<i>(Vessel, Container No, Permit No, Driver, Do No, SEF No, Client Ref)</i>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <table>
                            <tr>
                                <td>Trip Status</td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Pending" runat="server" Text="Pending">
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Started" runat="server" Text="Started">
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Completed" runat="server" Text="Completed">
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Cancel" runat="server" Text="Cancel">
                                    </dxe:ASPxCheckBox>
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
                                        <dxe:ASPxDateEdit ID="txt_new_JobDate" ClientInstanceName="txt_new_JobDate" runat="server" Width="160" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>

                                </tr>
                                <tr>
                                    <td>From Address
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_FromAddress" Rows="3" Width="340" ClientInstanceName="txt_FromAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>Job&nbsp;Type</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_new_jobtype" ClientInstanceName="cbb_new_jobtype" runat="server" Width="160" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                            <Items>
                                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                                                <%--<dxe:ListEditItem Text="COL" Value="COL" />
                                                <dxe:ListEditItem Text="RET" Value="RET" />
                                                <dxe:ListEditItem Text="ADHOC" Value="ADHOC" />
                                                <dxe:ListEditItem Text="OTHER" Value="OTHER" />--%>
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Shipper
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_WarehouseAddress" ClientInstanceName="txt_WarehouseAddress" runat="server" Width="160"></dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">To Address
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_ToAddress" Rows="3" Width="340" ClientInstanceName="txt_ToAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>WareHouse</td>
                                    <td>
                                        <div style="display: none">
                                            <dxe:ASPxLabel ID="lb_new_WareHouseId" runat="server" ClientInstanceName="lb_new_WareHouseId"></dxe:ASPxLabel>
                                        </div>
                                        <dxe:ASPxButtonEdit ID="txt_new_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_new_WareHouseId" runat="server" Text='<%# Eval("WareHouseCode") %>' Width="160" AutoPostBack="False">
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
                                        <dxe:ASPxTextBox ID="cbb_new_jobstatus" ReadOnly="true" BackColor="Control" ClientInstanceName="cbb_new_jobstatus" runat="server" Width="160">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <%--<a href="#" onclick="PopupCustAdr(null,txt_WarehouseAddress);"></a>--%>Depot&nbsp;Address
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_DepotAddress" Rows="3" Width="340" ClientInstanceName="txt_DepotAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Remark
                                    </td>
                                    <td rowspan="2">
                                        <dxe:ASPxMemo ID="txt_new_remark" Rows="3" Width="340" ClientInstanceName="txt_new_remark"
                                            runat="server">
                                        </dxe:ASPxMemo>
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
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" OnHtmlDataCellPrepared="grid_Transport_HtmlDataCellPrepared"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                 <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" VerticalScrollableHeight="400"/>
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" FixedStyle="Left">
                        <DataItemTemplate>
                            <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                            <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType" FixedStyle="Left"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TripIndex" Caption="TripNo"></dxwgv:GridViewDataColumn>
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
                    <dxwgv:GridViewDataTextColumn FieldName="ScheduleDate" Caption="Schedule Date" Width="100">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="BookingTime" Caption="Time"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="PermitRemark" Caption="SEF No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SubCon_Ind" Caption="SubCont">
                        <DataItemTemplate>
                        <%# Eval("SubCon_Ind","{0}")=="Yes" ? "<span style='color:white;background:red;padding:2px;'>SUB</span>" : ""%>
						</DataItemTemplate>
					
                    </dxwgv:GridViewDataTextColumn>

                   
                    <dxwgv:GridViewDataColumn FieldName="Direct_Inf" Caption="Direct?" ></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Trips" FieldName="trips">
                        <DataItemTemplate>
                            <div style='display: <%# (SafeValue.SafeString(Eval("Contractor"),"")=="YES"&&SafeValue.SafeString(Eval("ClientRefNo")).Length==0)?"block": "none" %>'>
                                <a class="a_ltrip">
                                    <div class="div_FixWith"><%# xmlChangeToHtml1(Eval("ContainerNo"),Eval("JobNo")) %></div>
                                </a>
                            </div>
                             <div style='display: <%# (SafeValue.SafeString(Eval("Contractor"),"")=="YES"&&SafeValue.SafeString(Eval("ClientRefNo")).Length>0)?"block": "none" %>'>
                                <a class="a_ltrip" href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("TripId") %>","<%# Eval("TripIndex") %>","<%# SafeValue.SafeString(Eval("TripId")).Length>0?"GO":"" %>")'>
                                    <div class="div_FixWith"><%# xmlChangeToHtml(Eval("trips"),0) %></div>
                                </a>
                            </div>
                            <div style='display: <%# (SafeValue.SafeString(Eval("Contractor"),"")=="NO"||SafeValue.SafeString(Eval("Contractor"),"").Length==0)?"block": "none" %>'>
                                <a class="a_ltrip" href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("TripId") %>","<%# Eval("TripIndex") %>","<%# SafeValue.SafeString(Eval("TripId")).Length>0?"GO":"" %>")'>
                                    <div class="div_FixWith"><%# xmlChangeToHtml(Eval("trips"),0) %></div>
                                </a>
                            </div>
 
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="WarehouseStatus" Caption="Status">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("WarehouseStatus"))) %>" class="div_contStatus">
                                <%# warehouseStatus_exchange(Eval("WarehouseStatus").ToString(),Eval("JobType").ToString()) %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DriverCode" Caption="Driver">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DriverCode2" Caption="Attendant">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DoubleMount" Caption="DoubleMounting">
                        <DataItemTemplate>
                        <%# Eval("DoubleMounting","{0}")=="Yes" ? "<span style='color:white;background:red;padding:2px;'>DM</span>" : ""%>
						</DataItemTemplate>
					
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="TowheadCode" Caption="Vehicle">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Job Status" Visible="false">
                        <DataItemTemplate>
                            <%# VilaStatus(SafeValue.SafeString(Eval("JobStatus"),""))%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Escort_Ind" Caption="Escort Ind"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Escort_Remark" Caption="Escort Remark"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SpecialInstruction" Caption="Instruction"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Trailer No" FieldName="ContainerNo">
                        <DataItemTemplate>
                            <%# Eval("ContainerNo") %><br />
                            <%--<a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>")' style="display: <%# SafeValue.SafeString(Eval("ContId")).Length>0?"":"none" %>">Link Trips</a>--%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Request Chassis Type " FieldName="RequestTrailerType" Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataColumn Caption="MT" FieldName="WarehouseStatus" Width="80"></dxwgv:GridViewDataColumn>--%>
                    
                    <%--<dxwgv:GridViewDataTextColumn FieldName="hr" Caption="Hours">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeInt(Eval("hr"),0)>72?"background-color:red;color:white": (SafeValue.SafeInt(Eval("hr"),0)>48?"background-color:orange;color:white":"")%>'>
                                <%# Eval("hr") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <%--<dxwgv:GridViewDataTextColumn FieldName="CfsStatus" Caption="WH Status"></dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="REFNo" FieldName="CarrierBkgNo" Width="200px" CellStyle-HorizontalAlign="Left" CellStyle-Wrap="True"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientContact" Caption="Client Contact"></dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataTextColumn FieldName="Depot" Caption="Depot">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("Depot") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <%--<dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor"></dxwgv:GridViewDataColumn>--%>
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
