<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="TripListForJobList.aspx.cs" Inherits="PagesContTrucking_SelectPage_TripListForJobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
    <script type="text/javascript">
        //setTimeout(function () {
        //    grid_Trip.Refresh();
        //}, 300);
        function RowClickHandler(s, e) {
            dde_Trip_ContNo.SetText(gridPopCont.cpContN[e.visibleIndex]);
            dde_Trip_ContId.SetText(gridPopCont.cpContId[e.visibleIndex]);
            dde_Trip_ContNo.HideDropDown();
        }
        function trip_callback(v) {
            grid_Trip.CancelEdit();
            //setTimeout(function () {
            //    grid_Trip.Refresh();
            //}, 300);
            //alert(v);
            //console.log(v);
            //grid_Trip.Refresh();
            var ar = v.split(',');
            var detail = {
                controller: ar[0],
                driver: ar[1],
                no: ar[2]
            }
            console.log('=========');
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
        }
        function trip_callback1() {
            var detail = {
            }
            console.log('=========1111');
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
        }
        function Cont_callback(v) {

            if (v == "Completed") {
                alert(' Container Completed ');
                parent.AfterAddTrip();
            } else {
                alert('Opened Container');
                btn_ContClose.SetText("Close Cont");
            }

        }
        function Whs_callback(v) {

            if (v == "Completed") {
                alert('Warehouse Completed!');
                parent.AfterAddTrip();
            }
            else if(v=="Return"){
                alert('Container Return!');
                parent.AfterAddTrip();
            }
            else if (v == "Export") {
                alert('Container Export!');
                parent.AfterAddTrip();
            }
        }
        function trip_delete_callback(v) {
            console.log(v);
        }
        function trip_addnew(type) {
            lb_newTrip_Type.SetText(type);
            //grid_Trip.AddNewRow();
            grid_Trip.GetValuesOnCustomCallback('AddNew', trip_addnew_callback);
        }
        function trip_addnew_callback(v) {
            if (v == "success") {
                grid_Trip.Refresh();
                //setTimeout(function () {
                //    grid_Trip.Refresh();
                //}, 300);
                //setTimeout(function () {
                //    grid_Trip.StartEditRow(grid_Trip.pageRowCount - 1);
                //}, 500);
                //cbb_StatusCode.SetValue('InTransit');
                //console.log(grid_Trip);

                var detail = {
                }
                console.log('=========', detail);
                SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
            }
        }
        var var_par0 = null;
        var var_par1 = null;
        var var_par2 = null;
        function trip_update_poppup_exchange(s, func, par0, par1, par2) {
            //console.log(s.uniqueID, par0);
            var ar_id = s.uniqueID.split('$');
            var inputId0 = ar_id[0] + '_' + ar_id[1] + '_';
            var inputId2 = '_I';
            var_par0 = par0;
            var_par1 = par1;
            var_par2 = par2;
            if (par0 && par0.SetText) {
                var_par0 = {};
                var_par0.SetText = function (t) {
                    //console.log(t);
                    document.getElementById(inputId0 + par0.uniqueID.split('$')[2] + inputId2).value = t;
                }
            }
            if (par1 && par1.SetText) {
                var_par1 = {};
                var_par1.SetText = function (t) {
                    document.getElementById(inputId0 + par1.uniqueID.split('$')[2] + inputId2).value = t;
                }
            }
            if (par2 && par2.SetText) {
                var_par2 = {};
                var_par2.SetText = function (t) {
                    document.getElementById(inputId0 + par2.uniqueID.split('$')[2] + inputId2).value = t;
                }
            }
            func(var_par0, var_par1, var_par2);
        }
        function trip_update_inline(s) {
            //console.log('=====', s);
            var ar_id = s.uniqueID.split('$');
            if (ar_id.length == 3) {
                var ar_temp = ar_id[1].replace('cell', '').split('_');
                grid_Trip.GetValuesOnCustomCallback('UpdateInline_' + ar_temp[0], trip_update_inline_callback);
            } else {
                alert('Save Error');
            }
        }
        function trip_update_inline_callback(v) {
            //console.log('===========res', v);
            if (v.indexOf('Save Error') >= 0) {
                console.log('===========', v);
                alert('Save Error');
            } else {
                //trip_update_callback(v);
                grid_Trip.Refresh();
                var ar = v.split(',');
                var driver = ",";
                for (var i = 2; i < ar.length; i++) {
                    driver = driver + ar[i] + ',';
                }
                var detail = {
                    controller: ar[0],
                    no: ar[1],
                    driver: driver,
                }
                console.log('=========', detail);
                SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));

                alert('Save Successful');
            }
            //console.log('=====', s);
        }
        function trip_update_callback(v) {
            grid_Trip.CancelEdit();
            //setTimeout(function () {
            //    grid_Trip.Refresh();
            //}, 300);
            //alert(v);
            //console.log(v);
            //grid_Trip.Refresh();
            var ar = v.split(',');
            var driver = ",";
            for (var i = 2; i < ar.length; i++) {
                driver = driver + ar[i] + ',';
            }
            var detail = {
                controller: ar[0],
                no: ar[1],
                driver: driver,
            }
            console.log('=========', detail);
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
        }
        function trip_delete_callback(v) {
            //grid_Trip.CancelEdit();
            //setTimeout(function () {
            //    grid_Trip.Refresh();
            //}, 300);
            //alert(v);
            //console.log(v);
            grid_Trip.Refresh();
            var ar = v.split(',');
            var driver = ",";
            for (var i = 2; i < ar.length; i++) {
                driver = driver + ar[i] + ',';
            }
            var detail = {
                controller: ar[0],
                no: ar[1],
                driver: driver,
            }
            console.log('=========', detail);
            SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
        }


        function save_job() {
            grid_Trip.GetValuesOnCustomCallback('SaveJob', save_job_callback);
        }
        function save_job_callback(v) {
            if (v == "success") {
                alert('Save successfully!');
            } else {
                alert('Save false!');
            }
        }

        function goJob() {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
            var jobno = lbl_JobNo.GetText();
            parent.parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterAddTrip(); }
        }
        document.onkeydown = keydown;

        function fee_showHide() {
            $('.fee_showhide').toggle(1000);
            $('#b_feeShowHide').html($('#b_feeShowHide').html() == '+' ? '-' : '+');
        }



        function PopupEmail() {
            var jobno = lbl_JobNo.GetText();
            var contId = lb_ContId.GetText();// document.getElementById("lb_ContId").innerText;
            //console.log('========', jobno, contId);
            popubCtr.SetHeaderText('Email EXP');
            popubCtr.SetContentUrl('EmailExp_byContainer.aspx?JobNo=' + jobno + "&contId=" + contId);
            popubCtr.Show();
        }
    </script>
    <script src="../script/jquery.js"></script>
    <style type="text/css">
        .fee_showhide {
            display: none;
        }

        .add_carpark {
            float: right;
            margin-right: 10px;
            width: 100px;
        }

        .gv_editform {
            width: 800px;
        }

        .gv {
            width: 950px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCarpark" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='carpark'" />
        <wilson:DataSource ID="dsCont" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet1" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />
        <wilson:DataSource ID="dsZone" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmParkingZone" KeyMember="id" FilterExpression="1=1" />
        <div>
            <table style="width: 100%">
                <tr>
                    <td>JobNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="lbl_JobNo" ClientInstanceName="lbl_JobNo" ReadOnly="true" BackColor="Control" runat="server" Width="200"></dxe:ASPxTextBox>
                    </td>
                    <%--<td>ContNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="lb_ContNo" ReadOnly="true" BackColor="Control" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>--%>
                    <%--<td>
                        <dxe:ASPxButton ID="btn_Save" Width="140" runat="server" Text="New Trip" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e) {
                                                    grid_Trip.AddNewRow();
                                                 }" />
                        </dxe:ASPxButton>
                    </td>--%>
                    <td>Add&nbsp;Trip:</td>
                    <td colspan="8">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_addTrip_COL" Width="50" runat="server" Text="COL" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('COL');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_addTrip_EXP" Width="50" runat="server" Text="EXP" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('EXP');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" Width="50" runat="server" Text="IMP" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('IMP');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton2" Width="50" runat="server" Text="RET" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('RET');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="50" runat="server" Text="SHF" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('SHF');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton4" Width="50" runat="server" Text="LOC" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                trip_addnew('LOC');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_WhsUpdate" ClientInstanceName="btn_WhsUpdate" Width="100" runat="server" Text="Warehouse Completed" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){grid_Trip.GetValuesOnCustomCallback('WhsStatus',Whs_callback);}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_ContClose" ClientInstanceName="btn_ContClose" Width="120" runat="server" Text="Container Completed" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){grid_Trip.GetValuesOnCustomCallback('ContClose',Cont_callback);}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton5" Width="50" runat="server" Text="Refresh" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                window.location.reload();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton7" Width="50" runat="server" Text="Close" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                parent.AfterAddTrip();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px solid #808080" colspan="13"></td>
                </tr>
                <tr>
                    <td>ContNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ContNo" runat="server" Width="200"></dxe:ASPxTextBox>
                    </td>
                    <td>SealNo:</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_sealno" runat="server" Width="200"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="1"></td>
                    <td colspan="2">
                        <table style="border-spacing: 0px;">
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_job_save" runat="server" Text="Save" Width="50" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            save_job();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="padding-left: 3px;">
                                    <dxe:ASPxButton ID="ASPxButton6" runat="server" Text="Open Job" Width="50" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            goJob();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="padding-left: 3px;">
                                    <dxe:ASPxButton ID="ASPxButton8" runat="server" Text="Send Email" Width="50" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            PopupEmail();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:60px;"></td>
                </tr>
                <tr>
                    <td>From:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_from" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_from" runat="server"></asp:Label>--%>
                    </td>
                    <td>To:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_to" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_to" runat="server"></asp:Label>--%>
                    </td>
                    <td>Depot:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_depot" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                </tr>

                <tr>
                    <td>Job Instruction:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_instruction" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                    <td>Remark:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_Remark" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                    <td>Permit No:</td>
                    <td>
                        <dxe:ASPxMemo ID="txt_PermitNo" runat="server" Width="200"></dxe:ASPxMemo>
                        <%--<asp:Label ID="txt_depot" runat="server"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td>DG/J5:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_F5Ind" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Portnet:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_PortnetStatus" runat="server" DropDownStyle="DropDown" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="" Text="" />
                                            <dxe:ListEditItem Value="Created" Text="Created" />
                                            <dxe:ListEditItem Value="Released" Text="Released" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>MT/LDN:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_warehouse_status" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Empty" Text="Empty" />
                                            <dxe:ListEditItem Value="Laden" Text="Laden" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Email Sent:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_EmailInd" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Urgent&nbsp;Job:</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_UrgentInd" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px solid #808080" colspan="13"></td>
                </tr>
            </table>
            <div style="display: none">
                <%--<asp:Label ID="lb_ContId" runat="server"></asp:Label>--%>
                <dxe:ASPxTextBox ID="lb_ContId" runat="server"></dxe:ASPxTextBox>
                <dxe:ASPxTextBox ID="lb_newTrip_Type" ClientInstanceName="lb_newTrip_Type" runat="server"></dxe:ASPxTextBox>
            </div>
            <dxwgv:ASPxGridView ID="grid_Trip" ClientInstanceName="grid_Trip" runat="server" Width="930"
                OnCustomCallback="grid_Trip_CustomCallback"
                DataSourceID="dsTrip" KeyFieldName="Id" OnInit="grid_Trip_Init" AutoGenerateColumns="False"
                OnHtmlEditFormCreated="grid_Trip_HtmlEditFormCreated" OnRowInserting="grid_Trip_RowInserting"
                OnInitNewRow="grid_Trip_InitNewRow" OnRowDeleting="grid_Trip_RowDeleting" OnRowUpdating="grid_Trip_RowUpdating"
                OnRowDeleted="grid_Trip_RowDeleted" OnRowInserted="grid_Trip_RowInserted" OnRowUpdated="grid_Trip_RowUpdated"
                OnCustomDataCallback="grid_Trip_CustomDataCallback">
                <SettingsPager PageSize="100" />
                <SettingsEditing Mode="EditForm" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0">
                        <DataItemTemplate>
                            <a href="#" onclick='<%# "grid_Trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="12">
                        <DataItemTemplate>
                            <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Trip.GetValuesOnCustomCallback(\"Delete_"+Container.KeyValue+"\",trip_delete_callback);"  %> }' style='display: <%# Eval("canChange")%>'>Delete</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="Statuscode" Caption="Status">
                        <DataItemTemplate>
                            <%# change_StatusShortCode_ToCode(Eval("Statuscode")) %>
                            <%--<%# Eval("Statuscode") %>--%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="TripCode" Caption="Type"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="ToCode" Caption="Destination"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="DoubleMounting" Caption="DB-MT" Width="60">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="ToParkingLot" Caption="Driver/Trailer//ParkingLot/Instr">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_Driver" ClientInstanceName="btn_Driver" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="120">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupCTM_Driver,btn_Driver,null,txt_towhead);
                                                //PopupCTM_Driver(btn_Driver,null,txt_towhead);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>

                                        <dxe:ASPxButtonEdit ID="txt_trailer" ClientInstanceName="txt_trailer" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="120">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupCTM_MasterData,txt_trailer,null,'Chessis');
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_inline_save" runat="server" Text="Send ->" Height="20" Width="55" AutoPostBack="false">
                                            <ClientSideEvents Click='function(s,e){trip_update_inline(s,e);}' />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_parkingLot" ClientInstanceName="txt_parkingLot" runat="server" Text='<%# Bind("ToParkingLot") %>' AutoPostBack="False" Width="120">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupParkingLot,txt_parkingLot,txt_tocode,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td colspan="2">
                                        <%--<dxe:ASPxTextBox ID="txt_instr" runat="server" Value='<%# Eval("Remark") %>' Width="200"></dxe:ASPxTextBox>--%>
                                        <dxe:ASPxMemo ID="txt_instr" runat="server" Text='<%# Bind("Remark") %>' Width="180" Height="16">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_towhead" ClientInstanceName="txt_towhead" runat="server" Value='<%# Bind("TowheadCode") %>' Width="100"></dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_tocode" ClientInstanceName="txt_tocode" runat="server" Value='<%# Bind("ToCode") %>' Width="100"></dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Value='<%# Bind("Id") %>' Width="100"></dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="ToParkingLot" Caption="ParkingLot"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="DriverCode" Caption="Driver"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataDateColumn VisibleIndex="9" Width="100" FieldName="FromDate" Caption="Start - End">

                        <%--<PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>--%>
                        <DataItemTemplate><%# SafeValue.SafeDate( Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM")+"&nbsp;"+Eval("FromTime")+"&nbsp;-&nbsp;"+Eval("ToTime") %></DataItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="10" Caption="Incentive$">
                        <DataItemTemplate>
                            <%# Eval("Incentive") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="10" Caption="Claims$">
                        <DataItemTemplate>
                            <%# Eval("Claims") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn VisibleIndex="10" Caption="Incentive$">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal(Eval("Incentive1"))+SafeValue.SafeDecimal(Eval("Incentive2"))+SafeValue.SafeDecimal(Eval("Incentive3"))+SafeValue.SafeDecimal(Eval("Incentive4")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataColumn VisibleIndex="10" FieldName="Incentive3" Caption="Standby">
                        <DataItemTemplate><%# SafeValue.SafeDecimal(Eval("Incentive3")) %></DataItemTemplate>
                    </dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataColumn VisibleIndex="10" Caption="Claims$">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal(Eval("Charge1"))+SafeValue.SafeDecimal(Eval("Charge2"))+SafeValue.SafeDecimal(Eval("Charge3"))+SafeValue.SafeDecimal(Eval("Charge4"))+SafeValue.SafeDecimal(Eval("Charge5"))+SafeValue.SafeDecimal(Eval("Charge6"))+SafeValue.SafeDecimal(Eval("Charge7"))+SafeValue.SafeDecimal(Eval("Charge8"))+SafeValue.SafeDecimal(Eval("Charge9"))+SafeValue.SafeDecimal(Eval("Charge10")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>--%>
                </Columns>
                <Templates>
                    <EditForm>

                        <%--<div style="width:880px;min-height:200px;">
					
						<div style="width:440px;">

						</div>
						
						<div style="width:210px;">

							<div style="width:200px; height:100px">
								<div></div>
							</div>
						
						
						
						</div>

						<div style="width:210px;">

						</div>
					
					
					</div>--%>

                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                            <dxe:ASPxTextBox ID="dde_Trip_ContId" ClientInstanceName="dde_Trip_ContId" runat="server" Text='<%# Bind("Det1Id") %>'></dxe:ASPxTextBox>
                        </div>
                        <table>
                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Trip Detail</td>
                            </tr>
                            <tr>
                                <td>Trip Type</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown">
                                        <Items>
                                            <dxe:ListEditItem Text="IMP" Value="IMP" />
                                            <dxe:ListEditItem Text="EXP" Value="EXP" />
                                            <dxe:ListEditItem Text="COL" Value="COL" />
                                            <dxe:ListEditItem Text="RET" Value="RET" />
                                            <dxe:ListEditItem Text="LOC" Value="LOC" />
                                            <dxe:ListEditItem Text="SHF" Value="SHF" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Driver</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,btn_TowheadCode);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>PrimeMover</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheadCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(btn_TowheadCode,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>Status</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                        <Items>
                                            <%--<dxe:ListEditItem Value="U" Text="Use" />--%>
                                            <dxe:ListEditItem Value="P" Text="Pending" />
                                            <dxe:ListEditItem Value="S" Text="Start" />
                                            <%--<dxe:ListEditItem Value="D" Text="Doing" />
                                            <dxe:ListEditItem Value="W" Text="Waiting" />--%>
                                            <dxe:ListEditItem Value="C" Text="Completed" />
                                            <dxe:ListEditItem Value="X" Text="Cancel" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Double Mounting</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_DoubleMounting" runat="server" Value='<%# Bind("DoubleMounting") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                            <dxe:ListEditItem Value="No" Text="No" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>T/P Schedule</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_BookingDate" runat="server" Value='<%# Bind("BookingDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                </td>
                                <td>Time</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_BookingTime" runat="server" Text='<%# Bind("BookingTime") %>' Width="162">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>From</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_FromCode" ClientInstanceName="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="407">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_FromPL" ClientInstanceName="txt_FromPL" runat="server" Text='<%# Bind("FromParkingLot") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>From Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_FromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                </td>
                                <td>Time</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="161">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>To</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" ClientInstanceName="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="407">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <%--<a href="#" onclick="PopupParkingLot(txt_FromPL,txt_Trip_FromCode);">From Parking Lot</a>--%>
                                    <a href="#" onclick="PopupParkingLot(txt_ToPL,txt_Trip_ToCode);">To Parking Lot</a>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_ToPL" ClientInstanceName="txt_ToPL" runat="server" Text='<%# Bind("ToParkingLot") %>' Width="165">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>ToDate</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                                </td>
                                <td>Time</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="161">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                                <td >Escort Ind</td>
                                <td >
                                    <dxe:ASPxComboBox ID="cmb_Escort_Ind" runat="server" Width="165" Value='<%# Bind("Escort_Ind") %>'>
                                        <Items>
										    <dxe:ListEditItem Value="" Text="" />
                                            <dxe:ListEditItem Value="Yes" Text="Yes" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Trip Instruction</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="407">
                                    </dxe:ASPxMemo>
                                </td>
                                <td >Escort Remark  </td>
                                <td colspan="3" class="ctl">
                                    <dxe:ASPxMemo ID="txt_Trip_Escort_Remark" ClientInstanceName="txt_Trip_Escort_Remark" runat="server" Text='<%# Bind("Escort_Remark") %>' Width="165">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Contractor
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_SubCon_Code" ClientInstanceName="btn_SubCon_Code" runat="server" Text='<%# Bind("SubCon_Code") %>' Width="165" AutoPostBack="False" ReadOnly="true">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_SubCon_Code,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>Sub-Contract</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_SubCon_Code" runat="server" Value='<%# Bind("SubCon_Ind") %>' Width="100%" DropDownStyle="DropDownList">
                                        <Items>
                                            <dxe:ListEditItem Value="YES" Text="YES" />
                                            <dxe:ListEditItem Value="NO" Text="NO" />
                                        </Items>
                                    </dxe:ASPxComboBox>

                                </td>
                                <td>Self Ind</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Self_Ind" runat="server" Value='<%# Bind("Self_Ind") %>' Width="100%" DropDownStyle="DropDownList">
                                        <Items>
                                            <dxe:ListEditItem Value="YES" Text="YES" />
                                            <dxe:ListEditItem Value="NO" Text="NO" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Driver Input</td>
                            </tr>
                            <tr>
                                <td>Container
                                </td>
                                <td>
                                    <dxe:ASPxDropDownEdit ID="dde_Trip_ContNo" runat="server" ClientInstanceName="dde_Trip_ContNo"
                                        Text='<%# Bind("ContainerNo") %>' Width="165" AllowUserInput="false">
                                        <DropDownWindowTemplate>
                                            <dxwgv:ASPxGridView ID="gridPopCont" runat="server" DataSourceID="dsCont" AutoGenerateColumns="False" ClientInstanceName="gridPopCont"
                                                Width="300px" KeyFieldName="Id" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents RowClick="RowClickHandler" />
                                            </dxwgv:ASPxGridView>
                                        </DropDownWindowTemplate>
                                    </dxe:ASPxDropDownEdit>
                                </td>
                                <td>Trailer</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_MasterData(btn_ChessisCode,null,'Chessis');
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>Zone</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_zone" runat="server" Value='<%# Bind("ParkingZone") %>' Width="165" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>



                            <%--<tr>
                                <td>Stage</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StageCode" runat="server" Value='<%# Bind("StageCode") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Pending" Text="Pending" />
                                            <dxe:ListEditItem Value="Port" Text="Port" />
                                            <dxe:ListEditItem Value="Yard" Text="Yard" />
                                            <dxe:ListEditItem Value="Park1" Text="Park1" />
                                            <dxe:ListEditItem Value="Warehouse" Text="Warehouse" />
                                            <dxe:ListEditItem Value="Park2" Text="Park2" />
                                            <dxe:ListEditItem Value="Park3" Text="Park3" />
                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Stage Status</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StageStatus" runat="server" Value='<%# Bind("StageStatus") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="" Text="" />
                                            <dxe:ListEditItem Value="DriveTo" Text="DriveTo" />
                                            <dxe:ListEditItem Value="Reach" Text="Reach" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>DriverRemark</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_driver_remark" ClientInstanceName="txt_driver_remark" runat="server" Text='<%# Bind("Remark1") %>' Width="407">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>

                                <td>Incentive</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive1" Height="21px" Value='<%# Bind("Incentive1")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>Overtime</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive2" Height="21px" Value='<%# Bind("Incentive2")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>Standby</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Incentive3" Height="21px" Value='<%# Bind("Incentive3")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>PSA ALLOWANCE</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Incentive4" runat="server" Value='<%# Bind("Incentive4") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="0" Text="0" />
                                            <dxe:ListEditItem Value="5" Text="5" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="border-bottom: 1px solid #808080" colspan="6"></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: lightgreen; padding: 4px; padding-left: 10px;">Surcharge</td>
                            </tr>
                            <tr>
                                <td>DHC</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge1" Height="21px" Value='<%# Bind("Charge1")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>WEIGHING</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge2" Height="21px" Value='<%# Bind("Charge2")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>WASHING</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge3" Height="21px" Value='<%# Bind("Charge3")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>REPAIR</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge4" Height="21px" Value='<%# Bind("Charge4")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>DETENTION</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge5" Height="21px" Value='<%# Bind("Charge5")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>DEMURRAGE</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge6" Height="21px" Value='<%# Bind("Charge6")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>LIFT ON/OFF</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge7" Height="21px" Value='<%# Bind("Charge7")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>C/SHIPMENT</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge8" Height="21px" Value='<%# Bind("Charge8")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>EMF</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge10" Height="21px" Value='<%# Bind("Charge9")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>ERP</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ERP" Height="21px" Value='<%# Bind("Charge_ERP")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>ParkingFee</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge_ParkingFee" Height="21px" Value='<%# Bind("Charge_ParkingFee")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>OTHER</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="165" ID="spin_Charge9" Height="21px" Value='<%# Bind("Charge10")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <span style="float: right">&nbsp
                                           <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        </span>
                                        <span style='float: right; display: <%# Eval("canChange")%>'>
                                            <%--<dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>--%>
                                            <a onclick="grid_Trip.GetValuesOnCustomCallback('Update',trip_update_callback);"><u>Update</u></a>
                                            <%--                                            <a onclick="grid_Trip.PerformCallback('Update');"><u>Update</u></a>--%>
                                        </span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
                <Settings ShowFooter="false" />
                <TotalSummary>
                    <%--<dx:ASPxSummaryItem FieldName="Incentive1" DisplayFormat="{0:0.00}" SummaryType="Sum" />
                    <dx:ASPxSummaryItem FieldName="Charge1" DisplayFormat="{0:0.00}" SummaryType="Sum" />--%>
                </TotalSummary>
                <%--<ClientSideEvents EndCallback="trip_callback1" />--%>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="370"
            Width="600" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
