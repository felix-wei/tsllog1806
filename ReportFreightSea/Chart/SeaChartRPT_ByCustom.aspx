<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeaChartRPT_ByCustom.aspx.cs" Inherits="ReportFreightSea_Chart_SeaChartRPT_ByCustom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function VisibleMonth() {
            if (search_DateType.GetText() == "Month") {
                search_DateFrom_Month.SetVisible(true);
                search_DateTo_Month.SetVisible(true);
            }
            else {
                search_DateFrom_Month.SetVisible(false);
                search_DateTo_Month.SetVisible(false);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Custom</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="search_CustomerId" ClientInstanceName="txtCustomerId" runat="server"
                            Width="60" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupParty(txtCustomerId,txtCustomer,'C');
                                                                                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txtCustomer" ClientInstanceName="txtCustomer" ReadOnly="true"
                            BackColor="Control" runat="server" Text='' Width="177">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>XAxis</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_XAxis" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Value="Date" Text="Date" Selected="true" />
                                <dxe:ListEditItem Value="Custom" Text="Custom" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>RefType</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_RefType" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Value="Import" Text="Import" Selected="true" />
                                <dxe:ListEditItem Value="Export" Text="Export" />
                                <dxe:ListEditItem Value="CrossTrade" Text="CrossTrade" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Date&nbsp;From</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_DateFrom_Year" runat="server" Width="60" ValueField="Year" TextField="Year"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_DateFrom_Month" runat="server" Width="45" ValueField="Month" TextField="Month"></dxe:ASPxComboBox>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_DateTo_Year" runat="server" Width="60" ValueField="Year" TextField="Year"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_DateTo_Month" runat="server" Width="45" ValueField="Month" TextField="Month"></dxe:ASPxComboBox>
                    </td>
                    <td>DateType</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_DateType" ClientInstanceName="search_DateType" runat="server" Width="100" AutoPostBack="false">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){VisibleMonth();}" />
                            <Items>
                                <dxe:ListEditItem Value="Year" Text="Year" />
                                <dxe:ListEditItem Value="Month" Text="Month" Selected="true" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>

            <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" Height="470px"
                Width="900px" ClientInstanceName="chart" SeriesDataMember="Date"
                CrosshairEnabled="False" ToolTipEnabled="True">
                <seriestemplate argumentdatamember="Custom" valuedatamembersserializable="value" tooltiphintdatamember="Date" tooltippointpattern="">
            <ViewSerializable>
                <dxcharts:SideBySideBarSeriesView></dxcharts:SideBySideBarSeriesView>
            </ViewSerializable>
            <LabelSerializable>
                <dxcharts:SideBySideBarSeriesLabel LineVisible="True" ResolveOverlappingMode="Default"></dxcharts:SideBySideBarSeriesLabel>
            </LabelSerializable>
        </seriestemplate>
                <legend visible="False"></legend>
                <borderoptions visible="False" />
                <titles>
            <dxcharts:ChartTitle Text="Sea Chart By Custom"></dxcharts:ChartTitle>
        </titles>
                <diagramserializable>
            <dxcharts:XYDiagram>
                <AxisX VisibleInPanesSerializable="-1">
<Label Angle="-30"></Label>
<Range SideMarginsEnabled="True"></Range>
</AxisX>
                <AxisY  VisibleInPanesSerializable="-1">

                </AxisY>
            </dxcharts:XYDiagram>
        </diagramserializable>
            </dxchartsui:WebChartControl>


            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                AllowResize="True" Width="550" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
