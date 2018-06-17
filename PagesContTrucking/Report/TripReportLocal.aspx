<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripReportLocal.aspx.cs" Inherits="PagesContTrucking_Report_TripReportLocal" %>

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
                        <dxe:ASPxDateEdit ID="search_DateFrom" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_DateTo" runat="server" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="1050px" KeyFieldName="Id" AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="JobNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Customer" Caption="Customer"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="BkgDate" Caption="BkgDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="BkgTime" Caption="BkgTime"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TripCode" Caption="TripCode"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Driver" Caption="Driver"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="VehicleNo" Caption="VehicleNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="PickFrm1" Caption="PickFrom"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DeliveryTo1" Caption="DeliveryTo"></dxwgv:GridViewDataColumn>
                </Columns>
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
