<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignDriver_BaseContainer.aspx.cs" Inherits="PagesContTrucking_Job_AssignDriver_BaseContainer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/ConTrucking_planner.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
    <script src="../script/jquery.js"></script>
    <script type="text/javascript">
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        var config = {
            timeout: 0,
            gridview: 'grid_Transport',
        }

        $(function () {
            loading.hide();
        })

        var newTrip_sender = null;
        function NewTrip(contId, ind, s) {
            //console.log('====== new trip ', contId, ind, val);
            newTrip_sender = s;
            if (s.value == 'New') { return; }
            loading.show();
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('NewTripInLine_' + ind + '_' + s.value, NewTrip_callback);
                //NewTrip_callback(contId + ',123,' + s.value);
            }, config.timeout);
        }
        function NewTrip_callback(v) {
            if (newTrip_sender) {
                newTrip_sender.value = 'New';
                newTrip_sender = null;
            }
            loading.hide();
            if (!v) {
                //loading.hide();
                return;
            }
            if (v.indexOf('success:') < 0) {
                //alert(v);
                parent.notice('Save Error', v, 'error');
                return;
            }
            var v_temp = v.replace('success:', '');
            var ar = v_temp.split(',');
            var contId = ar[0];
            var tripId = ar[1];
            var tripType = ar[2];
            var trips = document.getElementById("trips_" + ar[0]);
            //console.log(trips.childNodes);
            if (trips.childNodes.length > 0) {
                var lastNode = trips.lastChild;
                var oldNode = trips.childNodes[trips.childNodes.length - 2];
                var newNode = document.createElement("span");
                newNode.setAttribute('class', 'P');
                newNode.onclick = function () { OpenDetail(this); };
                newNode.id = "trip_" + ar[1];
                newNode.innerHTML = ar[2];
                //var oldNode = trips.replaceChild(newNode, last);
                //trips.appendChild(last);
                trips.removeChild(lastNode);
                trips.removeChild(oldNode);
                trips.appendChild(newNode);
                trips.appendChild(oldNode);
                trips.appendChild(lastNode);

                parent.notice('New Trip successful', null, 'success');
            }
            //loading.hide();
        }

        var openDetail_rowCol = null;
        function OpenDetail(s) {
            var col = s.parentNode.parentNode;

            var rowCol = col.id.substring(col.id.lastIndexOf('cell'));
            var ind = rowCol.replace('cell', '').split('_')[0];

            var nextColId = rowCol.substring(0, rowCol.lastIndexOf('_') + 1);
            var nextCol_No = rowCol.substring(rowCol.lastIndexOf('_') + 1);
            nextColId += (parseInt(nextCol_No, 10) + 1);
            openDetail_rowCol = nextColId;
            clearDetail(nextColId);
            s.setAttribute('style', 'color:white;background-color:red');
            //console.log(nextColId, txt_tripId,s.id.replace('trip_'));
            var txt_tripId = document.getElementById(config.gridview + '_' + nextColId + "_txt_tripId");
            txt_tripId.value = s.id.replace('trip_', '');

            loading.show();
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('OpenDetail_' + ind, OpenDetail_callback);
                //NewTrip_callback(contId + ',123,' + s.value);
            }, config.timeout);
        }
        function OpenDetail_callback(v) {
            var rowCol = -1;
            if (openDetail_rowCol) {
                rowCol = openDetail_rowCol;
                openDetail_rowCol = null;
            }
            //console.log(v);
            loading.hide();
            if (v.indexOf('success:') < 0) {
                //alert(v);
                parent.notice('Save Error', v, 'error');
                return;
            }
            var v_temp = v.replace('success:', '');
            var ar = v_temp.split('&,&');
            var driver = ar[0];
            var trailer = ar[1];
            var parkinglot = ar[2];
            var toAddress = ar[3];
            var btn_Driver = document.getElementById(config.gridview + '_' + rowCol + "_" + "btn_Driver" + '_I');
            var txt_trailer = document.getElementById(config.gridview + '_' + rowCol + "_" + "txt_trailer" + '_I');
            var txt_parkingLot = document.getElementById(config.gridview + '_' + rowCol + "_" + "txt_parkingLot" + '_I');
            var txt_toAddress = document.getElementById(config.gridview + '_' + rowCol + "_" + "txt_toAddress" + '_I');
            btn_Driver.value = driver;
            txt_trailer.value = trailer;
            txt_parkingLot.value = parkinglot;
            txt_toAddress.value = toAddress;
        }

        function clearDetail(rowCol) {
            var txt_tripId = document.getElementById(config.gridview + '_' + rowCol + "_txt_tripId");
            var last_tripId = txt_tripId.value;
            if (last_tripId && last_tripId.length > 0) {
                var last_trip = document.getElementById('trip_' + last_tripId);
                if (last_trip) {
                    last_trip.setAttribute('style', '');
                }
            }
            var btn_Driver = document.getElementById(config.gridview + '_' + rowCol + "_" + "btn_Driver" + '_I');
            var txt_trailer = document.getElementById(config.gridview + '_' + rowCol + "_" + "txt_trailer" + '_I');
            var txt_parkingLot = document.getElementById(config.gridview + '_' + rowCol + "_" + "txt_parkingLot" + '_I');
            var txt_toAddress = document.getElementById(config.gridview + '_' + rowCol + "_" + "txt_toAddress" + '_I');
            txt_tripId.value = '';
            btn_Driver.value = '';
            txt_trailer.value = '';
            txt_parkingLot.value = '';
            txt_toAddress.value = '';

        }

        function trip_update_inline(s) {
            var ar_id = s.uniqueID.split('$');
            var txt_tripId = document.getElementById(s.name.replace('btn_inline_save', 'txt_tripId'));
            if (txt_tripId && txt_tripId.value.length > 0) {
                if (ar_id.length == 3) {
                    openDetail_rowCol = ar_id[1];
                    var ar_temp = ar_id[1].replace('cell', '').split('_');

                    loading.show();
                    setTimeout(function () {
                        detailGrid.GetValuesOnCustomCallback('UpdateInline_' + ar_temp[0], trip_update_inline_callback);
                        //NewTrip_callback(contId + ',123,' + s.value);
                    }, config.timeout);
                } else {
                    //alert('Save Error');
                    parent.notice('Save Error', '', 'error');
                }
            } else {
                //alert('Please select trip first!');
                parent.notice('Please select trip first!', '', 'error');
            }
            //console.log(s.name);
        }

        function trip_update_inline_callback(v) {
            console.log(v);
            var rowCol = -1;
            if (openDetail_rowCol) {
                rowCol = openDetail_rowCol;
                openDetail_rowCol = null;
            }
            loading.hide();
            if (v.indexOf('success:') < 0) {
                //alert(v);
                parent.notice('Save Error', v, 'error');
                return;
            }
            clearDetail(rowCol);
            //alert('Save Successful');
            parent.notice('Save Successful','', 'success');

            var v_temp = v.replace('success:', '');
            var ar = v_temp.split(',');

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

        function container_update_status(s) {
            var col = s.parentNode;
            var rowCol = col.id.substring(col.id.lastIndexOf('cell'));
            var ind = rowCol.replace('cell', '').split('_')[0];

            //var nextColId = rowCol.substring(0, rowCol.lastIndexOf('_') + 1);
            //var nextCol_No = rowCol.substring(rowCol.lastIndexOf('_') + 1);
            //nextColId += (parseInt(nextCol_No, 10) + 2);
            openDetail_rowCol = col.id;

            loading.show();
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('ContChangeStatus_' + ind, container_update_status_callback);
                //NewTrip_callback(contId + ',123,' + s.value);
            }, config.timeout);
            //console.log(ind, rowCol,openDetail_rowCol);
        }

        function container_update_status_callback(v) {
            var rowCol = -1;
            if (openDetail_rowCol) {
                rowCol = openDetail_rowCol;
                openDetail_rowCol = null;
            }
            loading.hide();
            if (v.indexOf('success:') < 0) {
                //alert(v);
                parent.notice('Save Error', v, 'error');
                return;
            }
            var v_temp = v.replace('success:', '');
            var ar = v_temp.split('&,&');
            var ar_div = document.getElementById(rowCol).getElementsByTagName('div');
            if (ar_div.length > 0) {
                //console.log(ar_div);
                ar_div[0].setAttribute('style', 'background-color:' + ar[1]);
                ar_div[0].innerText= ar[0];
            }
            parent.notice('Change Successful', ar[0], 'success');
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

        .a_ltrip {
            min-width: 150px;
            width: 150px;
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
                position: relative;
                /*margin-top:2px;
            margin-left:2px;
            margin-bottom:2px;
            margin-right:4px;*/
            }

                .a_ltrip span:hover {
                    cursor: pointer;
                }

                .a_ltrip span select {
                    position: absolute;
                    top: 0px;
                    left: 0px;
                    right: 0px;
                    bottom: 0px;
                    max-width: 53px;
                    font-size: 12px;
                    border: 0px;
                    background-color: gray;
                    color: white;
                }

                    .a_ltrip span select:hover {
                        cursor: pointer;
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
            width: 48px;
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

        <div id="div_tc" class="tc_layout">
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Cont No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cont Status"></dxe:ASPxLabel>
                    </td>
                    <td>
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
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <%--<dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" Settings-AllowSort="False">
                        <DataItemTemplate>
                            <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?jobNo=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo" Settings-AllowSort="False">
                        <DataItemTemplate>
                            <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?jobNo=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});' title="<%# Eval("JobNo") %>"><%# Eval("ContainerNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Cont Type" FieldName="ContainerType" Settings-AllowSort="False"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ScheduleDate" Caption="Schedule Date" Width="80" Settings-AllowSort="False">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="StatusCode" Caption="Cont Status" Settings-AllowSort="False">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("StatusCode"))) %>" class="div_contStatus" onclick="container_update_status(this);">
                                <%# Eval("StatusCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Link Trips" FieldName="trips" Settings-AllowSort="False">
                        <DataItemTemplate>
                            <div class="a_ltrip" id='<%# "trips_"+Eval("contId") %>'>
                                <%# xmlChangeToHtml(Eval("trips"),Eval("ContId"),Container.VisibleIndex) %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Driver/Trailer//ToParkingLot/ToAddress" FieldName="tripDetail" Settings-AllowAutoFilter="False" Width="300">
                        <DataItemTemplate>
                            <%--<dxe:ASPxTextBox ID="txt_contId" runat="server" Value='<%# Eval("contId") %>'></dxe:ASPxTextBox>--%>
                            <div style="display: none">
                                <asp:TextBox ID="txt_contId" runat="server" Value='<%# Eval("contId") %>'></asp:TextBox>
                                <asp:TextBox ID="txt_tripId" runat="server"></asp:TextBox>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="btn_Driver" ClientInstanceName="btn_Driver" runat="server" AutoPostBack="False" Width="120">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupCTM_Driver,btn_Driver,null,null);
                                                //PopupCTM_Driver(btn_Driver,null,txt_towhead);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>

                                        <dxe:ASPxButtonEdit ID="txt_trailer" ClientInstanceName="txt_trailer" runat="server" AutoPostBack="False" Width="120">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupCTM_MasterData,txt_trailer,null,'Chessis');
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_inline_save" runat="server" Text="Save" Height="20" Width="55" AutoPostBack="false">
                                            <ClientSideEvents Click='function(s,e){trip_update_inline(s,e);}' />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_parkingLot" ClientInstanceName="txt_parkingLot" runat="server" AutoPostBack="False" Width="120">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                trip_update_poppup_exchange(s,PopupParkingLot,txt_parkingLot,txt_toAddress,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxMemo ID="txt_toAddress" ClientInstanceName="txt_toAddress" runat="server" Width="180" Height="16">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="900" EnableViewState="False">
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
