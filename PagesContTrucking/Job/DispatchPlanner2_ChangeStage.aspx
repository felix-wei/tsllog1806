<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner2_ChangeStage.aspx.cs" Inherits="PagesContTrucking_Job_DispatchPlanner2_ChangeStage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../Style/StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
        function addNewStageRow() {
            var tempdate = new Date();
            txt_FromDate.SetDate(tempdate);
            txt_FromTime.SetText(tempdate.getHours() + ":" + tempdate.getMinutes());
            txt_ToDate.SetDate(tempdate);
            txt_ToTime.SetText(tempdate.getHours() + ":" + tempdate.getMinutes());
            document.getElementById("AddStage").style.display = "block";
        }
        function cancelAddNewStageRow() {
            document.getElementById("AddStage").style.display = "none";
        }
        function addNewStageRow_result(temp) {
            var alert_str = "";
            if (temp == 1) {
                document.getElementById("AddStage").style.display = "none";
            } else {
                if (temp == -1) {
                    alert_str = "Request Driver";
                } else {
                    alert_str = "Add Error";
                }
                document.getElementById("AddStage").style.display = "block";
                alert(alert_str);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxe:ASPxButton ID="btn_changeStage" runat="server" Text="Change Stage" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){addNewStageRow();}" />
            </dxe:ASPxButton>
            <div id="AddStage" style="display: none;">
                <div style="display: none">
                    <dxe:ASPxTextBox ID="txt_TowheadCode" ClientInstanceName="txt_TowheadCode" runat="server"></dxe:ASPxTextBox>
                    <dxe:ASPxTextBox ID="txt_Det2Id" runat="server"></dxe:ASPxTextBox>
                </div>
                <table style="background-color: #F2F2F2; border: 1px solid gray; padding: 5px; margin-bottom: 5px; margin-top: 5px;">
                    <tr>
                        <td>JobNo</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_JobNo" runat="server" ReadOnly="true" Width="150"></dxe:ASPxTextBox>
                        </td>
                        <td>ContainerNo</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_ContNo" runat="server" ReadOnly="true" Width="150"></dxe:ASPxTextBox>
                        </td>
                        <td>Driver</td>
                        <td>
                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("DriverCode") %>' AutoPostBack="False" Width="150">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null,txt_TowheadCode);
                                                                        }" />
                            </dxe:ASPxButtonEdit>
                        </td>
                    </tr>
                    <tr>
                        <td>Stage</td>
                        <td>
                            <dxe:ASPxComboBox ID="cbb_StageCode" runat="server" Width="150">
                                <Items>
                                    <dxe:ListEditItem Value="Pending" Text="Pending" Selected />
                                    <dxe:ListEditItem Value="Port" Text="Port" />
                                    <dxe:ListEditItem Value="Park1" Text="Park1" />
                                    <dxe:ListEditItem Value="Warehouse" Text="Warehouse" />
                                    <dxe:ListEditItem Value="Park2" Text="Park2" />
                                    <dxe:ListEditItem Value="Yard" Text="Yard" />
                                    <dxe:ListEditItem Value="Completed" Text="Completed" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>Stage&nbsp;Status</td>
                        <td>
                            <dxe:ASPxComboBox ID="cbb_StageStatus" runat="server" Width="150">
                                <Items>
                                    <dxe:ListEditItem Value="" Text="" Selected="true" />
                                    <dxe:ListEditItem Value="DriveTo" Text="DriveTo" />
                                    <dxe:ListEditItem Value="Reach" Text="Reach" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>Trailer</td>
                        <td>
                            <dxe:ASPxButtonEdit ID="btn_ChessisCode" ClientInstanceName="btn_ChessisCode" runat="server" Text='<%# Bind("ChessisCode") %>' AutoPostBack="False" Width="150">
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
                        <td>From&nbsp;Date</td>
                        <td>
                            <dxe:ASPxDateEdit ID="txt_FromDate" ClientInstanceName="txt_FromDate" runat="server" Width="150" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                        </td>
                        <td>From&nbsp;Time</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_FromTime" ClientInstanceName="txt_FromTime" runat="server" Width="150">
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </td>
                        <td>Load</td>
                        <td>
                            <dxe:ASPxComboBox ID="cbb_LoadCode" runat="server" Width="150" >
                                <Items>
                                    <dxe:ListEditItem Value="E" Text="EMPTY" />
                                    <dxe:ListEditItem Value="L" Text="LADEN" />
                                    <dxe:ListEditItem Value=" " Text=" " Selected="true" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>To&nbsp;Date</td>
                        <td>
                            <dxe:ASPxDateEdit ID="txt_ToDate" ClientInstanceName="txt_ToDate" runat="server" Width="150" EditFormatString="dd/MM/yyyy" CalendarProperties-ShowClearButton="false"></dxe:ASPxDateEdit>
                        </td>
                        <td>To&nbsp;Time</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_ToTime" ClientInstanceName="txt_ToTime" runat="server" Width="150">
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>From</td>
                        <td colspan="4">
                            <dxe:ASPxMemo ID="txt_Trip_FromCode" runat="server" Width="392"></dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td>To</td>
                        <td colspan="4">
                            <dxe:ASPxMemo ID="txt_Trip_ToCode" runat="server" Width="392"></dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td>Remark</td>
                        <td colspan="4">
                            <dxe:ASPxMemo ID="txt_Trip_Remark" runat="server" Width="392">
                            </dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5"></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_submit_AddNewStageRow" runat="server" Text="Submit" OnClick="btn_submit_AddNewStageRow_Click"></dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cancel_AddNewStageRow" runat="server" AutoPostBack="false" Text="Cancel">
                                            <ClientSideEvents Click="function(s,e){cancelAddNewStageRow();}" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <dxwgv:ASPxGridView ID="gv" ClientInstanceName="gv" runat="server" KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnRowDeleting="gv_RowDeleting" OnRowUpdating="gv_RowUpdating">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="8%">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td><a href="#" onclick='<%# "gv.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a></td>
                                    <td><a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "gv.DeleteRow("+Container.VisibleIndex+");"  %>}'>Delete</a></td>
                                </tr>
                            </table>

                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td><a href="#" onclick='<%# "gv.UpdateEdit();" %>'>Update</a></td>
                                    <td><a href="#" onclick='gv.CancelEdit();'>Cancel</a></td>
                                </tr>
                            </table>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" ReadOnly="true"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo" ReadOnly="true"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Driver" FieldName="DriverCode" ReadOnly="true" Width="60"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Trailer" FieldName="ChessisCode" ReadOnly="true" Width="60"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Stage" FieldName="StageCode" Width="80">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                <dxe:ListEditItem Text="Port" Value="Port" />
                                <dxe:ListEditItem Text="Park1" Value="Park1" />
                                <dxe:ListEditItem Text="Warehouse" Value="Warehouse" />
                                <dxe:ListEditItem Text="Park2" Value="Park2" />
                                <dxe:ListEditItem Text="Yard" Value="Yard" />
                                <dxe:ListEditItem Text="Completed" Value="Completed" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="StageStatus" Width="80">
                        <PropertiesComboBox>
                            <Items>
                                    <dxe:ListEditItem Value="" Text="" Selected="true" />
                                    <dxe:ListEditItem Value="DriveTo" Text="DriveTo" />
                                    <dxe:ListEditItem Value="Reach" Text="Reach" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Load" FieldName="LoadCode" Width="60">
                        <PropertiesComboBox>
                            <Items>
                                    <dxe:ListEditItem Value="E" Text="EMPTY" />
                                    <dxe:ListEditItem Value="L" Text="LADEN" />
                                    <dxe:ListEditItem Value=" " Text=" " />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <%--<dxwgv:GridViewDataDateColumn Caption="FromDate" FieldName="FromDate" PropertiesDateEdit-EditFormatString="yyyy/MM/dd" PropertiesDateEdit-CalendarProperties-ShowClearButton="false" Width="90"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="FromTime" FieldName="FromTime" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" PropertiesTextEdit-ValidationSettings-ErrorDisplayMode="None" Width="60">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark"></dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="600" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
