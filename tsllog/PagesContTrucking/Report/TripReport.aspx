<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripReport.aspx.cs" Inherits="PagesContTrucking_Report_TripReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
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
                        <dxe:ASPxButtonEdit ID="search_Driver" ClientInstanceName="search_Driver" runat="server" AutoPostBack="False" Width="100%">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(search_Driver,null,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>Date From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateFrom" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateTo" Width="100px" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        Type
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_Trip_TripCode" runat="server" Width="165" DropDownStyle="DropDown">
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
                        <dxe:ASPxComboBox ID="cbb_zone" runat="server"  Width="165" DataSourceID="dsZone" TextField="Code" ValueField="Code">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="850px" KeyFieldName="Id" AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="JobNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="ContainerNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SealNo" Caption="SealNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="ContainerType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ParkingZone" Caption="Zone"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="ScheduleDate" Caption="ScheduleDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="FromTime" Caption="FromTime"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToTime" Caption="ToTime"></dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="Price" Caption="Amount"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="TripCodePrice" Visible="false" Caption="Trip Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="OverTimePrice" Visible="false" Caption="OT Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="QJPrice" Visible="false" Caption="OJ Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Total" Visible="false" Caption="Total Amt"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TripCode" Visible="false" Caption="TripCode"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Overtime"  Caption="Overtime" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="OverDistance"  Caption="Outside Jurong" Visible="false"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TowheadCode" Caption="Prime Mover"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="Trailer"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="To"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="TotalIncentive">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal(Eval("Incentive1"))+SafeValue.SafeDecimal(Eval("Incentive2"))+SafeValue.SafeDecimal(Eval("Incentive3"))+SafeValue.SafeDecimal(Eval("Incentive4")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ToCode" Caption="TotalClaims">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDecimal(Eval("Charge1"))+SafeValue.SafeDecimal(Eval("Charge2"))+SafeValue.SafeDecimal(Eval("Charge3"))+SafeValue.SafeDecimal(Eval("Charge4"))+SafeValue.SafeDecimal(Eval("Charge5"))+SafeValue.SafeDecimal(Eval("Charge6"))+SafeValue.SafeDecimal(Eval("Charge7"))+SafeValue.SafeDecimal(Eval("Charge8"))+SafeValue.SafeDecimal(Eval("Charge9")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive1" Caption="Incentive"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive2" Caption="Overtime"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive3" Caption="Standby"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive4" Caption="PSA"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge1" Caption="DHC"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge2" Caption="WEIGHING"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge3" Caption="WASHING"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge4" Caption="REPAIR"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge5" Caption="DETENTION"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge6" Caption="DEMURRAGE"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge7" Caption="LIFT ON/OFF"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge8" Caption="C/SHIPMENT"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Charge9" Caption="OTHER"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings  ShowFooter="true"/>
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="Incentive1" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Incentive2" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Incentive3" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Incentive4" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge1" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge2" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge3" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge4" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge5" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge6" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge7" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge8" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                    <dx:ASPxSummaryItem FieldName="Charge9" SummaryType="Sum" DisplayFormat="{0:0.00}"/>
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
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
