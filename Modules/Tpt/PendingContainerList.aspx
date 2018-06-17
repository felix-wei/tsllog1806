<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingContainerList.aspx.cs" Inherits="Modules_Tpt_PendingContainerList" %>

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
            parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?jobNo=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function PopupTripsList(jobno, contId, canGO) {
            if (canGO != "GO") {
                return;
            }
            popubCtr1.SetHeaderText('Trips List');
            popubCtr1.SetContentUrl('../SelectPage/TripListForJobList.aspx?JobNo=' + jobno + "&contId=" + contId);
            popubCtr1.Show();
        }
        function NewAdd_Visible(doShow) {
            if (doShow) {
                var t = new Date();
                txt_new_JobDate.SetText(t);
                cbb_new_jobtype.SetValue("");
                btn_new_ClientId.SetText('ELOG');
                txt_new_ClientName.SetText('');
                txt_FromAddress.SetText('');
                txt_WarehouseAddress.SetText('');
                txt_ToAddress.SetText('');
                txt_DepotAddress.SetText('');
                txt_new_remark.SetText('');

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
            } else {
                if (cb_ContStatus1.GetValue() && cb_ContStatus2.GetValue() && cb_ContStatus3.GetValue()) {
                    cb_ContStatus0.SetValue(true);
                } else {
                    cb_ContStatus0.SetValue(false);
                }
            }
        }
        function cbb_checkbox_Type1(name) {
            //console.log('========= checkbox', name);
            if (name == 'Uncomplete') {
                if (cb_ContStatus4.GetValue()) {
                    cb_ContStatus1.SetValue(false);
                    cb_ContStatus2.SetValue(false);
                    cb_ContStatus3.SetValue(false);
                }
            } else {
                if (cb_ContStatus1.GetValue() || cb_ContStatus2.GetValue() || cb_ContStatus3.GetValue()) {
                    cb_ContStatus4.SetValue(false);
                }
            }
        }
        function rowStatusChange(s, e, status) {
            var ar_id = s.uniqueID.split('$');
            if (ar_id.length == 3) {
                var ar_temp = ar_id[1].replace('cell', '').split('_');
                detailGrid.GetValuesOnCustomCallback('UpdateInlineStatus_' + ar_temp[0] + '_' + status, rowStatusChange_callback);
            } else {
                alert('Save Error');
            }
        }
        function rowStatusChange_callback(res) {
            //console.log(res, btn_search);
            if (res.indexOf('successful') >= 0) {
                btn_search.OnClick();
            } else {
                alert(res);
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
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Trailer No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>Warehouse</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_WareHouseId" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_WareHouseId" runat="server" Width="100" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupWh(txt_WareHouseId,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Job Type"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                <dxe:ListEditItem Text="WGR" Value="WGR" />
                                <dxe:ListEditItem Text="WDO" Value="WDO" />
                                <dxe:ListEditItem Text="TPT" Value="TPT" />
                                <%--<dxe:ListEditItem Text="LOC" Value="LOC" />--%>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <%--<td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Schedule Date From"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="WareHouse Status"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_warehouseStatus" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="Pending" Selected="true"/>
                                <dxe:ListEditItem Text="Scheduled" Value="Scheduled"/>
<%--                                 <dxe:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dxe:ListEditItem Text="Scheduled" Value="Scheduled" />
                                <dxe:ListEditItem Text="Started" Value="Started" />                               
                                <dxe:ListEditItem Text="Completed" Value="Completed" />--%>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    
                    <%--<td>NextTrip</td>
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
                    </td>--%>
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
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>

                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_save2excel" runat="server" Text="Save to excel" OnClick="btn_save2excel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo">
                        <DataItemTemplate>
                            <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                            <div style="min-width: 70px; display: <%# (SafeValue.SafeString(Eval("JobType"))=="WGR"||SafeValue.SafeString(Eval("JobType"))=="WDO"||SafeValue.SafeString(Eval("JobType"))=="TPT")?"block":"none"%>">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                            <div style="min-width: 70px; display: <%# (SafeValue.SafeString(Eval("JobType"))=="IMP"||SafeValue.SafeString(Eval("JobType"))=="EXP"||SafeValue.SafeString(Eval("JobType"))=="LOC")?"block":"none"%>">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Value='<%# Bind("JobNo") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Job Type" FieldName="JobType"></dxwgv:GridViewDataColumn>
                    
                    <dxwgv:GridViewDataColumn FieldName="ClientId" Caption="Customer"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="[Cont/Trailer]"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="SealNo" FieldName="SealNo" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Trucking Date" FieldName="ScheduleDate"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="StatusCode" Caption="Trucking Status">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("StatusCode"))) %>" class="div_contStatus">
                                <%# Eval("StatusCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="CfsStatus" Caption="Whs Status"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="DateIn" Caption="Date In" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("DateIn")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="DateOut" Caption="Date Out" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("DateOut")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <%--<dxwgv:GridViewDataDateColumn FieldName="ScheduleStartDate" Caption="WH Schedule/Start Date" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="CompletionDate" Caption="Completion Date" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate( Eval("CompletionDate"),new DateTime(1900,1,1)).Year<2000?"":SafeValue.SafeDate( Eval("CompletionDate"),new DateTime(1900,1,1)).ToString("dd/MM/yyyy") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataDateColumn>--%>
                    <dxwgv:GridViewDataColumn Caption="Schedule Date" FieldName="ScheduleStartDate">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" Value='<%# Bind("ScheduleStartDate") %>' runat="server" Width="150" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="date_ScheduleStartTime" runat="server" Text='<%# Bind("ScheduleStartTime") %>' Width="80">
                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                            <ValidationSettings ErrorDisplayMode="None" />
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Completion Date" FieldName="CompletionDate" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#">
                        <DataItemTemplate>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lb_Id" runat="server" Value='<%# Bind("Id") %>'></dxe:ASPxLabel>
                            </div>
                            <dxe:ASPxButton ID="ASPxButton1" Width="60" Text="Schedule" runat="server" Visible='<%# (Eval("CfsStatus").Equals("Pending")||Eval("CfsStatus").Equals("Scheduled")) %>' AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){rowStatusChange(s,e,'Scheduled');}" />
                            </dxe:ASPxButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    
                    <%--<dxwgv:GridViewDataColumn Caption="#">
                        <DataItemTemplate>
                            <dxe:ASPxButton ID="btn_contEnd" Text="End" runat="server" ></dxe:ASPxButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>--%>
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
