<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TptJobPrint.aspx.cs" Inherits="ReportTpt_Report_TptJobPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table style="width: 100%; background-color: #FFF;">
                <tr>
                    <td>
                        <table>
                            <tr>

                                <td>From Date<as</td>
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
                <dxwgv:ASPxGridView ID="grid_Import" runat="server" Width="100%"
                    KeyFieldName="SequenceId"
                    AutoGenerateColumns="False" ClientInstanceName="grid_Import">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>

                        <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="JobNo" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="JobType" VisibleIndex="2" >
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
                        <dxwgv:GridViewDataTextColumn Caption="Pol" FieldName="Pol" VisibleIndex="7" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Wt" FieldName="Wt" VisibleIndex="8" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="M3" FieldName="M3" VisibleIndex="9" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="9" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Import">
                </dxwgv:ASPxGridViewExporter>
            </table>
        </div>
    </form>
</body>
</html>
