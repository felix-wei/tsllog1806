<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner_AddTrip.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner_AddTrip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <title></title>
    <script type="text/javascript">
        function onCallBack(v) {
            if (v != null && v.length > 0) {
                alert(v);
            }
            else {
                document.getElementById('div_threeRows').style.display = 'none';

                var tempDate = new Date();
                var temp = tempDate.getHours() + ":" + tempDate.getMinutes();
                document.getElementById('cb_Row1').checked = false;
                document.getElementById('cb_Row2').checked = false;
                document.getElementById('cb_Row3').checked = false;
                cbb_Driver1.SetSelectedIndex(-1);
                cbb_Driver2.SetSelectedIndex(-1);
                cbb_Driver3.SetSelectedIndex(-1);
                txt_fromTime1.SetText(temp);
                txt_fromTime2.SetText(temp);
                txt_fromTime3.SetText(temp);
                cbb_TripCode1.SetSelectedIndex(-1);
                cbb_TripCode2.SetSelectedIndex(-1);
                cbb_TripCode3.SetSelectedIndex(-1);
                txt_FromCode1.SetText("");
                txt_FromCode2.SetText("");
                txt_FromCode3.SetText("");
                txt_ToCode1.SetText("");
                txt_ToCode2.SetText("");
                txt_ToCode3.SetText("");
            }
            grid_Trip.Refresh();

        }
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopup(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none">
            <dxe:ASPxLabel ID="lb_det1Id" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lb_JobNo" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lb_ContNo" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="lb_ContDate" ClientInstanceName="lb_ContDate" runat="server" ></dxe:ASPxLabel>
        </div>
        <div>
            <wilson:DataSource ID="dsTrip" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCont" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJobDet1" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsJob" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmJob" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsTripCode" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='tripcode'" />
            <wilson:DataSource ID="dsCtmDriverLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CtmDriverLog" KeyMember="Id" FilterExpression="1=0" />

            <div style="display: none">
                <dxe:ASPxButton ID="btn_TripAdd" runat="server" Text="Assign Trip" AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e){
                                grid_Trip.AddNewRow();
                                }" />
                </dxe:ASPxButton>
            </div>
            <dxe:ASPxButton ID="btn_showThreeRows" runat="server" Text="Add Trip" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){
                        document.getElementById('div_threeRows').style.display='';
                    }" />
            </dxe:ASPxButton>
            <dxwgv:ASPxGridView ID="grid_Trip" ClientInstanceName="grid_Trip" runat="server" Width="850px" DataSourceID="dsTrip" KeyFieldName="Id" OnInit="grid_Trip_Init" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Trip_BeforePerformDataSelect" OnHtmlEditFormCreated="grid_Trip_HtmlEditFormCreated" OnRowInserting="grid_Trip_RowInserting" OnInitNewRow="grid_Trip_InitNewRow" OnRowDeleting="grid_Trip_RowDeleting" OnRowUpdating="grid_Trip_RowUpdating" OnRowDeleted="grid_Trip_RowDeleted" OnRowInserted="grid_Trip_RowInserted" OnRowUpdated="grid_Trip_RowUpdated" OnCustomDataCallback="grid_Trip_CustomDataCallback">
                <SettingsPager PageSize="100" />
                <SettingsEditing Mode="EditForm" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="10%">
                        <DataItemTemplate>
                            <a href="#" onclick='<%# "grid_Trip.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                            <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_Trip.DeleteRow("+Container.VisibleIndex+");"  %>}'>Delete</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="DriverCode" Caption="Driver Code"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="4" FieldName="TowheadCode" Caption="PrimeMover"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="5" FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="FromTime" Caption="Time">
                        <PropertiesTextEdit MaskSettings-Mask="<00..23>:<00..59>" MaskSettings-ErrorText=""></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <table>
                            <tr>
                                <td>Container No
                                    <div style="display: none">
                                        <dxe:ASPxLabel ID="lb_tripId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>

                                    </div>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_ContNo" ReadOnly="true" runat="server" Text='<%# Bind("ContainerNo") %>' Width="165"></dxe:ASPxTextBox>
                                </td>
                                <td>Cfs</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_CfsCode" ClientInstanceName="btn_CfsCode" runat="server" Text='<%# Bind("CfsCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_MasterData(btn_CfsCode,null,'Location');
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>Bay</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_BayCode" runat="server" Value='<%# Bind("BayCode") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="B1" Text="B1" />
                                            <dxe:ListEditItem Value="B2" Text="B2" />
                                            <dxe:ListEditItem Value="B3" Text="B3" />
                                            <dxe:ListEditItem Value="B4" Text="B4" />
                                            <dxe:ListEditItem Value="B5" Text="B5" />
                                            <dxe:ListEditItem Value="B6" Text="B6" />
                                            <dxe:ListEditItem Value="B7" Text="B7" />
                                            <dxe:ListEditItem Value="B8" Text="B8" />
                                            <dxe:ListEditItem Value="B9" Text="B9" />
                                            <dxe:ListEditItem Value="B10" Text="B10" />
                                            <dxe:ListEditItem Value="B11" Text="B11" />
                                            <dxe:ListEditItem Value="B12" Text="B12" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Driver</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="165">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_DriverLog(btn_DriverCode,null,btn_TowheadCode,lb_ContDate.GetText());
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
                                <td>Trail</td>
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
                            </tr>
                            <tr>
                                <td>Sublet</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_SubletFlag" runat="server" Value='<%# Bind("SubletFlag") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Sublet Haulier</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_SubletHauliername" runat="server" Width="165" Text='<%# Bind("SubletHauliername") %>'></dxe:ASPxTextBox>
                                </td>
                                <td>Trip Code</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Value='<%# Bind("TripCode") %>' Width="165" DropDownStyle="DropDown" DataSourceID="dsTripCode" ValueField="Code" TextField="Code">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <%--<td>ToDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Trip_toDate" runat="server" Value='<%# Bind("ToDate") %>' Width="165"></dxe:ASPxDateEdit>
                                                                    </td><td>FromDate</td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_Trip_fromDate" runat="server" Value='<%# Bind("FromDate") %>' Width="165"></dxe:ASPxDateEdit>
                                                                    </td>--%>
                            </tr>
                            <tr>
                                <td>Remark</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_Remark" ClientInstanceName="txt_Trip_Remark" runat="server" Text='<%# Bind("Remark") %>' Width="400">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>Status</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="165">
                                        <Items>
                                            <dxe:ListEditItem Value="U" Text="Use" />
                                            <dxe:ListEditItem Value="S" Text="Start" />
                                            <dxe:ListEditItem Value="D" Text="Doing" />
                                            <dxe:ListEditItem Value="W" Text="Waiting" />
                                            <dxe:ListEditItem Value="P" Text="Pending" />
                                            <dxe:ListEditItem Value="C" Text="Completed" />
                                            <dxe:ListEditItem Value="X" Text="Cancel" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>From</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_FromCode" runat="server" Text='<%# Bind("FromCode") %>' Width="400"></dxe:ASPxMemo>
                                </td>
                                <td>Time</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Trip_fromTime" runat="server" Text='<%# Bind("FromTime") %>' Width="165">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>To</td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Trip_ToCode" runat="server" Text='<%# Bind("ToCode") %>' Width="400"></dxe:ASPxMemo>
                                </td>
                                <td>Time</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Trip_toTime" runat="server" Text='<%# Bind("ToTime") %>' Width="165">
                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                        <ValidationSettings ErrorDisplayMode="None" />
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <span style="float: right">&nbsp
                                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        </span>
                                        <span style='float: right; display: <%# Eval("canChange")%>'>
                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        </span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <div id="div_threeRows" style="display: none; background-color: rgb(240, 240, 240)">
                <table style="width: 100%; border: 1px solid rgb(159, 159, 159);">
                    <tr>
                        <td style="border-bottom: 1px solid rgb(159, 159, 159);">
                            <asp:CheckBox ID="cb_Row1" runat="server" Text="Trip 1" />
                            <table style="width: 600px">
                                <tr>
                                    <td>Driver</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_Driver1" ClientInstanceName="cbb_Driver1" runat="server" DataSourceID="dsCtmDriverLog" ValueField="Driver" TextField="Driver" Width="120"></dxe:ASPxComboBox>
                                    </td>
                                    <td>StartTime</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_fromTime1" ClientInstanceName="txt_fromTime1" runat="server" Width="120">
                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                            <ValidationSettings ErrorDisplayMode="None" />
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>TripCode</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_TripCode1" ClientInstanceName="cbb_TripCode1" runat="server" Width="120" DropDownStyle="DropDown" DataSourceID="dsTripCode" ValueField="Code" TextField="Code">
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Status</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_StatusCode1" ClientInstanceName="cbb_StatusCode1" runat="server" Width="120">
                                            <Items>
                                                <dxe:ListEditItem Value="U" Text="Use" />
                                                <dxe:ListEditItem Value="S" Text="Start" />
                                                <dxe:ListEditItem Value="D" Text="Doing" />
                                                <dxe:ListEditItem Value="W" Text="Waiting" />
                                                <dxe:ListEditItem Value="P" Text="Pending" />
                                                <dxe:ListEditItem Value="C" Text="Completed" />
                                                <dxe:ListEditItem Value="X" Text="Cancel" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>From</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_FromCode1" ClientInstanceName="txt_FromCode1" runat="server" Width="320" Height="30"></dxe:ASPxMemo>
                                    </td>
                                    <td>To</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_ToCode1" ClientInstanceName="txt_ToCode1" runat="server" Width="300" Height="30"></dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px solid rgb(159, 159, 159);">
                            <asp:CheckBox ID="cb_Row2" runat="server" Text="Trip 2" />
                            <table style="width: 600px">
                                <tr>
                                    <td>Driver</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_Driver2" runat="server" DataSourceID="dsCtmDriverLog" ValueField="Driver" TextField="Driver" Width="120"></dxe:ASPxComboBox>
                                    </td>
                                    <td>StartTime</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_fromTime2" runat="server" Width="120">
                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                            <ValidationSettings ErrorDisplayMode="None" />
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>TripCode</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_TripCode2" runat="server" Width="120" DropDownStyle="DropDown" DataSourceID="dsTripCode" ValueField="Code" TextField="Code">
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Status</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_StatusCode2" runat="server" Width="120">
                                            <Items>
                                                <dxe:ListEditItem Value="U" Text="Use" />
                                                <dxe:ListEditItem Value="S" Text="Start" />
                                                <dxe:ListEditItem Value="D" Text="Doing" />
                                                <dxe:ListEditItem Value="W" Text="Waiting" />
                                                <dxe:ListEditItem Value="P" Text="Pending" />
                                                <dxe:ListEditItem Value="C" Text="Completed" />
                                                <dxe:ListEditItem Value="X" Text="Cancel" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>From</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_FromCode2" runat="server" Width="320" Height="30"></dxe:ASPxMemo>
                                    </td>
                                    <td>To</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_ToCode2" runat="server" Width="300" Height="30"></dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom: 1px solid rgb(159, 159, 159);">
                            <asp:CheckBox ID="cb_Row3" runat="server" Text="Trip 3" />
                            <table style="width: 600px">
                                <tr>
                                    <td>Driver</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_Driver3" runat="server" DataSourceID="dsCtmDriverLog" ValueField="Driver" TextField="Driver" Width="120"></dxe:ASPxComboBox>
                                    </td>
                                    <td>StartTime</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_fromTime3" runat="server" Width="120">
                                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                            <ValidationSettings ErrorDisplayMode="None" />
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>TripCode</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_TripCode3" runat="server" Width="120" DropDownStyle="DropDown" DataSourceID="dsTripCode" ValueField="Code" TextField="Code">
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Status</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cbb_StatusCode3" runat="server" Width="120">
                                            <Items>
                                                <dxe:ListEditItem Value="U" Text="Use" />
                                                <dxe:ListEditItem Value="S" Text="Start" />
                                                <dxe:ListEditItem Value="D" Text="Doing" />
                                                <dxe:ListEditItem Value="W" Text="Waiting" />
                                                <dxe:ListEditItem Value="P" Text="Pending" />
                                                <dxe:ListEditItem Value="C" Text="Completed" />
                                                <dxe:ListEditItem Value="X" Text="Cancel" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>From</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_FromCode3" runat="server" Width="320" Height="30"></dxe:ASPxMemo>
                                    </td>
                                    <td>To</td>
                                    <td colspan="3">
                                        <dxe:ASPxMemo ID="txt_ToCode3" runat="server" Width="300" Height="30"></dxe:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; padding-right: 70px">
                            <a href="#" onclick="document.getElementById('btn_AddThree').click();">Update</a>
                            <div style="display: none">
                                <dxe:ASPxButton ID="btn_AddThree" runat="server" AutoPostBack="false" Text="Update">
                                    <ClientSideEvents Click="function(s,e){grid_Trip.GetValuesOnCustomCallback('Three',onCallBack);}" />
                                </dxe:ASPxButton>
                            </div>
                            <a href="#" onclick="document.getElementById('div_threeRows').style.display='none';">Cancel</a>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div style="background-color: #dcd4d4; padding: 5px;">
                <span>Container Details</span>
                <dxwgv:ASPxGridView ID="grid_Container" runat="server" AutoGenerateColumns="false" KeyFieldName="Id" Width="850px" DataSourceID="dsCont">
                    <Columns>
                        <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Container Type"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="Seal No"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="ScheduleDate" Caption="ScheduleDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataColumn FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="Volume" Caption="Volume"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="Qty" Caption="Qty"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="PackageType" Caption="PackageType"></dxwgv:GridViewDataColumn>
                    </Columns>
                </dxwgv:ASPxGridView>
                <br />
                <span>Job Information</span>
                <dxwgv:ASPxGridView ID="grid_Job" runat="server" AutoGenerateColumns="false" KeyFieldName="Id" Width="850px" DataSourceID="dsJob">
                    <Columns>
                        <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="JobNo"></dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="JobDate" Caption="JobDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="EtaDate" Caption="Eta" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="EtdDate" Caption="Etd" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Pol" Caption="POL"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Pod" Caption="POD"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    </Columns>
                </dxwgv:ASPxGridView>
            </div>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="700" EnableViewState="False">
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
