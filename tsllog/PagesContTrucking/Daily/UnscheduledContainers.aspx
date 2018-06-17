<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnscheduledContainers.aspx.cs" Inherits="PagesContTrucking_Daily_UnscheduledContainers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script>
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
            else
                window.location = "UnscheduledContainers.aspx"
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

    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Eta From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_EtaFrom" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_EtaTo" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="850" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="10" VisibleIndex="0">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="JobNo" SortIndex="1" SortOrder="Ascending" Width="170">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContNo" FieldName="ContainerNo" Width="120">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="AssignDate" FieldName="ScheduleDate" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContType" FieldName="ContainerType" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="SealNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="EtaDate" Caption="Eta" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Width="100"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="Weight" Caption="Weight"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Volume" Caption="Volume"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="QTY" Caption="QTY"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="PackageType" Caption="PackageType"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
                <Templates>
                    <DataRow>
                        <table border="1" bordercolor="#b1cfef" style="border-collapse: collapse; font-family: Tahoma; font-size: 8pt;">
                            <tr>
                                <td style="width: 20px">
                                    <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                    </dxe:ASPxCheckBox>
                                </td>
                                <td style="width: 200px"><%# Eval("JobNo") %>
                                </td>
                                <td style="width: 150px"><%# Eval("ContainerNo") %>
                                    <div style="display: none">
                                        <dxe:ASPxLabel ID="txt_det1Id" BackColor="Control" ReadOnly="true" runat="server"
                                            Text='<%# Eval("Id") %>' Width="80">
                                        </dxe:ASPxLabel>
                                    </div>
                                </td>
                                <td style="width:200px">
                                    <dxe:ASPxDateEdit ID="date_SchDate" runat="server" EditFormatString="dd/MM/yyyy"  Width="100%"></dxe:ASPxDateEdit>
                                </td>
                                <td style="width: 100px"><%# Eval("ContainerType") %>
                                </td>
                                <td style="width: 100px"><%# Eval("SealNo") %></td>
                                <td style="width: 100px"><%# SafeValue.SafeDateStr(Eval("EtaDate")) %></td>
                                <td style="width: 80px"><%# Eval("Weight") %></td>
                                <td style="width: 80px"><%# Eval("Volume") %></td>
                                <td style="width: 70px"><%# Eval("QTY") %></td>
                                <td style="width: 150px"><%# Eval("PackageType") %></td>
                            </tr>
                        </table>
                    </DataRow>
                </Templates>
            </dxwgv:ASPxGridView>
            <br />
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
                        <dxe:ASPxButton ID="btn_assign1" runat="server" Text="Assign" AutoPostBack="false" Width="110"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        detailGrid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="DepotIn" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
