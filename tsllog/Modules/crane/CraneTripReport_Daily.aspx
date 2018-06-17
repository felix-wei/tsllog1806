<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CraneTripReport_Daily.aspx.cs" Inherits="PagesContTrucking_Report_TripReport_Incentive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript">
        function print() {
            var driver = search_Driver.GetText();
            var d1 = search_DateFrom.GetText();
            var d2 = search_DateTo.GetText();
            var ar_d1 = d1.split('/');
            if (ar_d1.length == 3) {
                d1 = ar_d1[2] + ar_d1[1] + ar_d1[0];
            } else {
                alert('Date From is Error');
                return;
            }
            var ar_d2 = d2.split('/');
            if (ar_d2.length == 3) {
                d2 = ar_d2[2] + ar_d2[1] + ar_d2[0];
            } else {
                alert('Date To is Error');
                return;
            }
            console.log('==========print:', driver, d1, d2);
            if (driver && driver.length > 0) {
                window.open('RptPrintView.aspx?doc=1&p=' + driver + '&d1=' + d1 + '&d2=' + d2);
            } else {
                alert('Require Driver!');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsZone" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmParkingZone" KeyMember="id" FilterExpression="" />
        <div>
            <table>
                <tr>
                    <td>Driver
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="search_Driver" ClientInstanceName="search_Driver" runat="server" AutoPostBack="False" Width="100">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(search_Driver,null,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>PrimeMover</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="search_TowheadCode" ClientInstanceName="search_TowheadCode" runat="server" AutoPostBack="False" Width="100">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(search_TowheadCode,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>Type
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Width="100" DropDownStyle="DropDown">
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
                    <td>Zone</td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_zone" runat="server" Width="100" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Date&nbsp;From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateFrom" ClientInstanceName="search_DateFrom" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateTo" ClientInstanceName="search_DateTo" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click" Width="80"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click" Width="80"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" AutoPostBack="false" Width="80">
                            <ClientSideEvents Click="function(s,e){print();}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="950px" KeyFieldName="Id" AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="ToDate" Caption="Date">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <%# SafeValue.SafeDate(Eval("ToDate"),new DateTime(1900,1,1)).ToString("dd-MM-yyyy") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="Time">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <%# Eval("FromTime") %>-<%# Eval("ToTime") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="ContainerNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="Type"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="TowheadCode" Caption="Prime Mover"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="JobNo">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <%# Eval("JobNo") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="SealNo"></dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataDateColumn FieldName="ScheduleDate" Caption="ScheduleDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>--%>
                    <%--<dxwgv:GridViewDataColumn FieldName="ToTime" Caption="ToTime"></dxwgv:GridViewDataColumn>--%>
                    <%--<dxwgv:GridViewDataColumn FieldName="Price" Caption="Amount"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 200px;">
                                <%# Eval("FromCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 200px;">
                                <%# Eval("ToCode") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="TripCodePrice" Visible="false" Caption="Trip Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="OverTimePrice" Visible="false" Caption="OT Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="QJPrice" Visible="false" Caption="OJ Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Total" Visible="false" Caption="Total Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TripCode" Visible="false" Caption="TripCode"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Overtime" Caption="Overtime" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="OverDistance" Caption="Outside Jurong" Visible="false"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="Incentive1" Caption="Trip">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Incentive1")).ToString("n") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive2" Caption="OT">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Incentive2")).ToString("n") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive3" Caption="Standby">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Incentive3")).ToString("n") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive4" Caption="PSA">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("Incentive4")).ToString("n") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TotalIncentive" Caption="Total">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal( Eval("TotalIncentive")).ToString("n") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="true" />
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="Incentive1" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="Incentive2" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="Incentive3" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="Incentive4" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="TotalIncentive" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="470"
                Width="800" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
