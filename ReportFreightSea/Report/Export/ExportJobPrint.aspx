<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportJobPrint.aspx.cs" Inherits="ReportFreightSea_Report_Export_ExportJobPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table style="width: 100%; background-color: #FFF;">
                            <tr>
                                <td>Type
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_RefType" ClientInstanceName="cmb_RefType" runat="server" Width="140">
                                        <Items>
                                            <dxe:ListEditItem Text="ALL" Value="" />
                                            <dxe:ListEditItem Text="Export" Value="SE" />
                                            <dxe:ListEditItem Text="CrossTrade" Value="SC" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Job Type
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_JobType" ClientInstanceName="cmb_DocType" runat="server"
                                        Width="150">
                                        <Items>
                                            <dxe:ListEditItem Text="ALL" Value="" />
                                            <dxe:ListEditItem Text="FCL" Value="FCL" />
                                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                                            <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>From Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                                        EditFormatString="dd/MM/yyyy" Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>To Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_End" ClientInstanceName="toDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                        Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btnRetrieve" runat="server" Text="Retrieve" Width="120" AutoPostBack="False" OnClick="btnRetrieve_Click">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <dxwgv:ASPxGridView ID="grid_Export" runat="server" Width="100%"
                    KeyFieldName="SequenceId"
                    AutoGenerateColumns="False" ClientInstanceName="grid_Export">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>

                         <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="JobNo" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                         <dxwgv:GridViewDataTextColumn Caption="Job Type" FieldName="JobType" VisibleIndex="2" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="CustomerName" VisibleIndex="2" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Vessel" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Voyage" FieldName="Voyage" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Eta" FieldName="Eta" VisibleIndex="5" Width="80">
                           <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Etd" FieldName="Etd" VisibleIndex="6" Width="80">
                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pod" FieldName="Pod" VisibleIndex="7" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="8" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="9" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Export">
                </dxwgv:ASPxGridViewExporter>
            </table>
        </div>
    </form>
</body>
</html>
