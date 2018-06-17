<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CraneTripReport_Monthly.aspx.cs" Inherits="PagesContTrucking_Report_TripReport_Month" %>

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
            window.open('RptPrintView.aspx?doc=3&p=' + driver + '&d1=' + d1 + '&d2=' + d2);
            //if (driver && driver.length > 0) {
            //    window.open('RptPrintView.aspx?doc=2&p=' + driver + '&d1=' + d1 + '&d2=' + d2);
            //} else {
            //    alert('Require Driver!');
            //}
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
                        <dxe:ASPxButtonEdit ID="search_Driver" ClientInstanceName="search_Driver" runat="server" AutoPostBack="False" Width="120">
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
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" AutoPostBack="false" Width="80">
                            <ClientSideEvents Click="function(s,e){print();}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"  KeyFieldName="Id" AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="Date" Caption="Date" Width="200">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <%# Eval("Date") %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    
                    <dxwgv:GridViewDataColumn FieldName="DriverCode" Caption="Driver" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TowheadCode" Caption="Prime Mover" Width="100"></dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="FromCode" Caption="From">
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
                    </dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn FieldName="SalaryBasic" Caption="Basic" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Allowance" Caption="Allowance" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive1" Caption="Trip" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive2" Caption="OT" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive3" Caption="Standby" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Incentive4" Caption="PSA" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Deduction" Caption="Deduction" Width="100"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Total" Caption="Total" Width="100"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="true" />
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="Incentive1" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="Incentive2" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="Incentive3" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="Incentive4" SummaryType="Sum" DisplayFormat="{0:0.00}" />
                    <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" DisplayFormat="{0:0.00}" />
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
