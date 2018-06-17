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
                alert("Successfully Assign Driver.")
                detailGrid.Refresh();
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
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table>
                <tr>
                    <td>Date From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Search" ClientInstanceName="btn_Search" runat="server" Text="Retrieve" OnClick="btn_Search_Click"></dxe:ASPxButton>
                    </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_assign1" runat="server" Text="OK" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                        detailGrid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="1100"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback" OnHtmlRowPrepared="grid_Transport_HtmlRowPrepared">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="10" VisibleIndex="0">
                        <DataItemTemplate>
                                    <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                    </dxe:ASPxCheckBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Descending" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="JobType" VisibleIndex="3" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Bkg Date" FieldName="BkgDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("BkgDate"))+" " +SafeValue.SafeString(Eval("BkgTime")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TPT Date" FieldName="TptDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("TptDate"))+" " +SafeValue.SafeString(Eval("TptTime")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobProgress" VisibleIndex="9" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TptType" FieldName="TptType" VisibleIndex="9" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Driver" FieldName="Driver" VisibleIndex="9" Width="100">
                        <DataItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("Driver") %>' AutoPostBack="False" Width="80">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                            </dxe:ASPxButtonEdit>
                            <div style="display:none">
                                <dxe:ASPxTextBox ID="lb_Id" runat="server" Text='<%# Bind("Id") %>'>

                                </dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Vehicle" FieldName="VehicleNo" VisibleIndex="9" Width="100">
                        <DataItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_vehicle" ClientInstanceName="btn_vehicle" runat="server" Text='<%# Bind("VehicleNo") %>' AutoPostBack="False" Width="80">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                            </dxe:ASPxButtonEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TripCode" FieldName="TripCode" VisibleIndex="9" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="From" FieldName="PickFrm1" VisibleIndex="9" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="To" FieldName="DeliveryTo1" VisibleIndex="10" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Wt" VisibleIndex="10" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="M3" FieldName="M3" VisibleIndex="10" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="10" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="VehicleNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <table>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Assign Driver" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
