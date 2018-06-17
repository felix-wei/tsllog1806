<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignDriverEdit.aspx.cs" Inherits="PagesContTrucking_Job_AssignDriverEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript">
        $.noConflict();
    </script>
    <script type="text/javascript">
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
            else {
                parent.AfterPopupAD();
            }
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
            alert(jQuery("input[id*='ack_IsPay']").length);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--<wilson:DataSource ID="dsTowhead" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='towhead'" />--%>
            <wilson:DataSource ID="dsLocation" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='location' and type1='LOCAL'" />
            <wilson:DataSource ID="dsChessis" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='chessis'" />

            <table>
                <tr>
                    <td>Job No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SearchJobNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>Container No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SearchContainer" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Search" runat="server" Text="Retrieve" OnClick="btn_Search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="1500" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback" OnHtmlRowPrepared="grid_Transport_HtmlRowPrepared">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="10" VisibleIndex="0">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContNo" FieldName="ContainerNo" VisibleIndex="2" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DriverCode" FieldName="DriverCode" VisibleIndex="3" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CfsCode" FieldName="CfsCode" VisibleIndex="4" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ChessisCode" FieldName="ChessisCode" VisibleIndex="5" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="StatusCode" FieldName="StatusCode" VisibleIndex="5" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SubletFlag" FieldName="SubletFlag" VisibleIndex="6" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SubletHaulierName" FieldName="SubletHaulierName" VisibleIndex="7" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="BayCode" FieldName="BayCode" VisibleIndex="8" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="From" FieldName="FromCode" VisibleIndex="9" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="To" FieldName="ToCode" VisibleIndex="10" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="VehicleNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
                <Templates>
                    <DataRow>

                        <table border="1" bordercolor="#b1cfef" style="border-collapse: collapse; font-family: Tahoma; font-size: 8pt;">
                            <tr>
                                <td width="25">
                                    <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                    </dxe:ASPxCheckBox>
                                    <div style="display: none">
                                        <dxe:ASPxLabel ID="lb_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                    </div>
                                </td>
                                <td width="170">
                                    <dxe:ASPxLabel ID="txt_JobNo" ReadOnly="true" runat="server"
                                        Text='<%# Eval("JobNo") %>' Width="150">
                                    </dxe:ASPxLabel>
                                </td>
                                <td width="135">
                                    <dxe:ASPxLabel ID="txt_ContNo" runat="server" Text='<%# Eval("ContainerNo") %>' Width="100">
                                    </dxe:ASPxLabel>
                                    <div style="display: none">
                                        <dxe:ASPxLabel ID="txt_jobId" BackColor="Control" ReadOnly="true" runat="server"
                                            Text='<%# Eval("Id") %>' Width="80">
                                        </dxe:ASPxLabel>
                                    </div>
                                </td>
                                <td width="160">
                                    <dxe:ASPxButtonEdit ID="btn_DriveCode" ClientInstanceName="btn_DriveCode" Text='<%# Bind("DriverCode") %>' runat="server" HorizontalAlign="Left" AutoPostBack="False" Width="100%">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                    </dxe:ASPxButtonEdit>
                                    <div style="display:none" >
                                        <dxe:ASPxTextBox ID="txt_TowheadCode" runat="server" ClientInstanceName="txt_TowheadCode" Text='<%# Bind("TowheadCode") %>'></dxe:ASPxTextBox>
                                        <dxe:ASPxTextBox ID="txt_ScheduleDate" runat="server" Text='<%# Eval("ScheduleDate") %>'></dxe:ASPxTextBox>
                                    </div>
                                </td>
                                <td width="155">
                                    <dxe:ASPxComboBox ID="cbb_CfsCode" runat="server" Value='<%# Bind("CfsCode") %>' DropDownStyle="DropDownList" TextField="Code" ValueField="Code" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" DataSourceID="dsLocation" Width="100%">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="150">
                                    <dxe:ASPxComboBox ID="cbb_Chessis" runat="server" Value='<%# Bind("ChessisCode") %>' DropDownStyle="DropDownList" TextField="Code" ValueField="Code" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" DataSourceID="dsChessis" Width="100%">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="160">
                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Value='<%# Bind("Statuscode") %>' Width="100%">
                                        <Items>
                                            <dxe:ListEditItem Value="Use" Text="Use" />
                                            <dxe:ListEditItem Value="Start" Text="Start" />
                                            <dxe:ListEditItem Value="Waiting" Text="Waiting" />
                                            <dxe:ListEditItem Value="Pending" Text="Pending" />
                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                            <dxe:ListEditItem Value="Cancel" Text="Cancel" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_SubletFlag" runat="server" Value='<%# Bind("SubletFlag") %>' Width="68">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="170">
                                    <dxe:ASPxTextBox ID="txt_SubletHauliername" runat="server" Width="100%" Text='<%# Bind("SubletHauliername") %>'></dxe:ASPxTextBox>
                                </td>

                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_BayCode" runat="server" Value='<%# Bind("BayCode") %>' Width="60">
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
                                <td>
                                    <dxe:ASPxLabel ID="lb_FromCode" runat="server" Text='<%# Eval("FromCode") %>' Width="155"></dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="lb_ToCode" runat="server" Text='<%# Eval("ToCode") %>' Width="155"></dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </DataRow>
                </Templates>
            </dxwgv:ASPxGridView>
            <table>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_assign1" runat="server" Text="Assign" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                        detailGrid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Assign Driver" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
