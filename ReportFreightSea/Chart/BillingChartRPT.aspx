<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillingChartRPT.aspx.cs" Inherits="ReportFreightSea_Chart_BillingChartRPT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
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
<body onloadeddata="VisibleMonth();">
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
            <table>
                <tr>
                    <td>Party&nbsp;To
                    </td>
                    <td colspan="5">
                        <dxe:ASPxComboBox ID="search_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                            Width="241" DropDownWidth="300" DropDownStyle="DropDownList" DataSourceID="dsVendorMast"
                            ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="40px" />
                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Billing&nbsp;Type</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_Type" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Ar" Text="Ar" Selected="true" />
                                <dxe:ListEditItem Value="Ap" Text="Ap" />
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
                        <dxe:ASPxComboBox ID="search_DateType" ClientInstanceName="search_DateType" runat="server" Width="80" AutoPostBack="false">
                            <ClientSideEvents SelectedIndexChanged="function(s,e){VisibleMonth();}" />
                            <Items>
                                <dxe:ListEditItem Value="Year" Text="Year" />
                                <dxe:ListEditItem Value="Month" Text="Month" Selected="true" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" Text="Retrieve" runat="server" OnClick="btn_search_Click"></dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_save" Text="Save" runat="server" OnClick="btn_save_Click"></dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>


            <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" Height="470px"
                Width="900px" ClientInstanceName="chart" SeriesDataMember="Date"
                CrosshairEnabled="False" ToolTipEnabled="false">

                <seriestemplate LabelsVisibility="True" argumentdatamember="Date" valuedatamembersserializable="Amt" tooltiphintdatamember="" tooltippointpattern="Date:{A}<br/>Value:{V}">
            <ViewSerializable>
                <dxcharts:SideBySideBarSeriesView ></dxcharts:SideBySideBarSeriesView>
            </ViewSerializable>

            <LabelSerializable>
                <dxcharts:SideBySideBarSeriesLabel LineVisible="True" ResolveOverlappingMode="Default" ></dxcharts:SideBySideBarSeriesLabel>
            </LabelSerializable>
        </seriestemplate>
                <legend visible="false"></legend>
                <borderoptions visible="false" />
                <titles>
            <dxcharts:ChartTitle Text="Ar/Ap Billing Chart"></dxcharts:ChartTitle>
        </titles>
                <diagramserializable>
            <dxcharts:XYDiagram>
                <AxisX VisibleInPanesSerializable="-1">
<Label Angle="-30" ></Label>
<Range SideMarginsEnabled="True"></Range>
</AxisX>
                <AxisY  VisibleInPanesSerializable="-1">

                </AxisY>
            </dxcharts:XYDiagram>
        </diagramserializable>
            </dxchartsui:WebChartControl>

        </div>
    </form>
</body>
</html>
