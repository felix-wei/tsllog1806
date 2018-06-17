<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnMathcedRef.aspx.cs" Inherits="ReportFreightSea_Report_Import_UnMathcedRef" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Departure Port">
                        </dxe:ASPxLabel>
                       

                    </td>
                    <td>
                                                <dxe:ASPxButtonEdit ID="btn_DeparturePort" ClientInstanceName="txt_DeparturePort" runat="server" Text="" Width="95">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPort(txt_DeparturePort,null);
                        }" />
                                    </dxe:ASPxButtonEdit>
                       
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Depature Date">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                         <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Arrival Port">
                        </dxe:ASPxLabel>

                    </td>
                    <td>
                         <dxe:ASPxButtonEdit ID="btn_ArrivalPort" ClientInstanceName="txt_ArrivalPort" runat="server" Text="" Width="95">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                             PopupPort(txt_ArrivalPort,null);
                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnRetrieve" runat="server" Text="Retrieve" Width="120" AutoPostBack="False" OnClick="btnRetrieve_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
                <dxwgv:ASPxGridView ID="grid_Import" runat="server" Width="100%"
                    KeyFieldName="Id"
                    AutoGenerateColumns="False" ClientInstanceName="grid_Import">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="RefNo" FieldName="RefNo" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="AirportCode" FieldName="AirportCode0" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                         <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Flight Date" FieldName="FlightDate0" VisibleIndex="5" Width="80">
                           <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="7" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="GrossWeight" VisibleIndex="8" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="RateClass" FieldName="RateClass" VisibleIndex="9" Width="80">
                        </dxwgv:GridViewDataTextColumn>
  
                    </Columns>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Import">
                </dxwgv:ASPxGridViewExporter>
                        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                AllowResize="True" Width="500" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
