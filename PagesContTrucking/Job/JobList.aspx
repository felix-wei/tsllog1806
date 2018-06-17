<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobList.aspx.cs" Inherits="PagesContTrucking_Job_JobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        .a_ltrip{
            width:130px;
            min-width:130px;
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
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Cont No" Width="50px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Vessel"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Vessel" Width="100" runat="server"></dxe:ASPxTextBox>
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
                        <%--<table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>

                                </td>
                                <td width="90%"></td>
                                <td style="display:none">
                                    <dxe:ASPxButton ID="btn_Add" runat="server" Text="New&nbsp;Job" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
                                    NewAdd_Visible(true,'J');
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td  style="display:none">
                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="New&nbsp; Quotation" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
                                    NewAdd_Visible(true,'Q');
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                    <td style="width: 400px;"></td>
                </tr>
                <tr>
                    <td>Job&nbsp;No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Job Type" Width="50px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                                <%--<dxe:ListEditItem Text="ADHOC" Value="ADHOC" />
                                <dxe:ListEditItem Text="OTHER" Value="OTHER" />--%>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="SubContract"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_subContract" runat="server" DropDownStyle="DropDownList" Width="100">
                            <Items>
                                <dxe:ListEditItem Value="ALL" Text="ALL" Selected="true" />
                                <dxe:ListEditItem Value="YES" Text="YES" />
                                <dxe:ListEditItem Value="NO" Text="NO" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>NextTrip</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_NextTrip" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Value="ALL" Text="ALL" Selected />
                                <dxe:ListEditItem Value="IMP" Text="Import" />
                                <dxe:ListEditItem Value="RET" Text="Empty Return" />
                                <dxe:ListEditItem Value="COL" Text="Empty Collection" />
                                <dxe:ListEditItem Value="EXP" Text="Export" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <%--<td>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="WHS Status" Width="76px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_Whs_Status" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Value="" Text="" Selected />
                                <dxe:ListEditItem Value="Customer-MT" Text="Customer-MT" />
                                <dxe:ListEditItem Value="Customer-LD" Text="Customer-LD" />
                                <dxe:ListEditItem Value="WHS-MT" Text="WHS-MT" />
                                <dxe:ListEditItem Value="WHS-LD" Text="WHS-LD" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="12">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cont Status:" Width="80px"></dxe:ASPxLabel>
                                </td>
                                <%--<td>
                                    <dxe:ASPxCheckBox ID="cb_ContStatus0" ClientInstanceName="cb_ContStatus0" runat="server" Text="ALL">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type('ALL');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>--%>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_New" ClientInstanceName="cb_cs_New" runat="server" Text="New">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('New');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <%--<td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Uncomplete" ClientInstanceName="cb_cs_Uncomplete" runat="server" Text="Uncomplete">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Uncomplete');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>--%>
                                <td>&nbsp;&nbsp;</td>
                                <td class="tb_split">&nbsp;</td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Import" ClientInstanceName="cb_cs_Import" runat="server" Text="Import">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Import');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Return" ClientInstanceName="cb_cs_Return" runat="server" Text="Return">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Return');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td class="tb_split">&nbsp;</td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Collection" ClientInstanceName="cb_cs_Collection" runat="server" Text="Collection">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Collection');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Export" ClientInstanceName="cb_cs_Export" runat="server" Text="Export">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Export');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td class="tb_split">&nbsp;</td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_WHSMT" ClientInstanceName="cb_cs_WHSMT" runat="server" Text="WHS-MT">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('WHS-MT');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_WHSLD" ClientInstanceName="cb_cs_WHSLD" runat="server" Text="WHS-LD">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('WHS-LD');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_CustomerMT" ClientInstanceName="cb_cs_CustomerMT" runat="server" Text="Customer-MT">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Customer-MT');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_CustomerLD" ClientInstanceName="cb_cs_CustomerLD" runat="server" Text="Customer-LD">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Customer-LD');
                                            }" />
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td class="tb_split">&nbsp;</td>
                                <td>
                                    <dxe:ASPxCheckBox ID="cb_cs_Completed" ClientInstanceName="cb_cs_Completed" runat="server" Text="Completed">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            cbb_checkbox_Type1('Completed');
                                            }" />
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
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="90">
                                                    <dxe:ASPxButtonEdit ID="btn_DepotCode" ClientInstanceName="btn_DepotCode" runat="server" Width="90" AutoPostBack="False">
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_DepotCode,txt_DepotAddress);
                                                                        }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>
                                                     <dxe:ASPxMemo ID="txt_DepotAddress" Rows="1" Width="250" ClientInstanceName="txt_DepotAddress"
                                            runat="server">
                                        </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                        </table>
                                       
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
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType" Width="60" FixedStyle="Left"></dxwgv:GridViewDataColumn>
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
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Urgent" FieldName="UrgentInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"background-color:red;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"Y":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 60px;max-width:200px;width:auto"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 60px;max-width:200px;width:auto"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Contractor" Caption="Subcon?" ></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerCategory" Caption="Direct?" ></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Escort_Ind" Caption="Escort Ind" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Escort_Remark" Caption="Escort Remark" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo">
                        <DataItemTemplate>
                            <%# Eval("ContainerNo") %><br />
                            <%--<a href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>")' style="display: <%# SafeValue.SafeString(Eval("ContId")).Length>0?"":"none" %>">Link Trips</a>--%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="DischargeCell" FieldName="DischargeCell" Width="40"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="OP" FieldName="OperatorCode" Width="40"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Email" FieldName="EmailInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("EmailInd"))=="Y"?"background-color:green;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("EmailInd"))=="Y"?"@":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    
                    <dxwgv:GridViewDataColumn Caption="SealNo" FieldName="SealNo" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="J5" FieldName="F5Ind" Width="80" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="OOG" FieldName="oogInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("oogInd"))=="Y"?"background-color:red;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("oogInd"))=="Y"?"Y":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn Caption="MT" FieldName="WarehouseStatus" Width="80"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn Caption="Next" FieldName="NextTrip" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Link Trips" FieldName="trips">
                        <DataItemTemplate>
                            <div style='display: <%# (SafeValue.SafeString(Eval("Contractor"),"")=="YES"&&SafeValue.SafeString(Eval("ClientRefNo")).Length==0)?"block": "none" %>'>
                                <a class="a_ltrip">
                                    <%# xmlChangeToHtml1(Eval("ContainerNo"),Eval("JobNo")) %>
                                </a>
                            </div>
                             <div style='display: <%# (SafeValue.SafeString(Eval("Contractor"),"")=="YES"&&SafeValue.SafeString(Eval("ClientRefNo")).Length>0)?"block": "none" %>'>
                                <a class="a_ltrip" href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>","<%# SafeValue.SafeString(Eval("ContId")).Length>0?"GO":"" %>","<%# Eval("IsWarehouse") %>")'>
                                    <%# xmlChangeToHtml(Eval("trips"),Eval("ContId")) %>
                                </a>
                            </div>
                            <div style='display: <%# (SafeValue.SafeString(Eval("Contractor"),"")=="NO"||SafeValue.SafeString(Eval("Contractor"),"").Length==0)?"block": "none" %>'>
                                <a class="a_ltrip" href='javascript: PopupTripsList("<%# Eval("JobNo") %>","<%# Eval("ContId") %>","<%# SafeValue.SafeString(Eval("ContId")).Length>0?"GO":"" %>","<%# Eval("IsWarehouse") %>")'>
                                    <%# xmlChangeToHtml(Eval("trips"),Eval("ContId")) %>
                                </a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Warehouse Status" FieldName="WhsReadyInd">
                        <DataItemTemplate>
                            <div style='color:green;display:<%# SafeValue.SafeString(Eval("Contractor"),"")=="Y"?"block": "none" %>'><%# Eval("WhsReadyTime") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Cont Type" FieldName="ContainerType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="StatusCode" Caption="Cont Status">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("StatusCode"))) %>" class="div_contStatus">
                                <%# Eval("StatusCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="hr" Caption="Hours" Visible="false">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeInt(Eval("hr"),0)>72?"background-color:red;color:white": (SafeValue.SafeInt(Eval("hr"),0)>48?"background-color:orange;color:white":"")%>'>
                                <%# Eval("hr") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ReturnLastDate" Caption="Last Day">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ReturnLastDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="CfsStatus" Caption="WH Status"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PortnetStatus" Caption="Portnet"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="REFNo" FieldName="CarrierBkgNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TT" Width="60">
                        <DataItemTemplate>
                            <%# SafeValue.SafeString(Eval("EtaTime"),"0000") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientContact" Caption="Client Contact"></dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From" Width="200">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To" Width="200">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn FieldName="Depot" Caption="Depot">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("Depot") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PermitNo" Caption="PermitNo"></dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientRefNo" Caption="Client Ref No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SpecialInstruction" Caption="Instruction"></dxwgv:GridViewDataColumn>
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
