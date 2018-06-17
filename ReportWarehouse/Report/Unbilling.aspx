<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unbilling.aspx.cs" Inherits="ReportWarehouse_Report_Unbilling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; background-color: #FFF;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td><asp:Label ID="lblDate" Text="From Date" runat="server"></asp:Label></td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                                        EditFormatString="dd/MM/yyyy" Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td><asp:Label ID="Label1" Text="To Date" runat="server"></asp:Label></td>
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
                <dxwgv:ASPxGridView ID="grid" runat="server" Width="100%"
                    KeyFieldName="Id"
                    AutoGenerateColumns="False" ClientInstanceName="grid">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="DoNo" FieldName="DoNo" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Job Type" FieldName="DoType" VisibleIndex="2" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="PartyName" VisibleIndex="2" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Ref" FieldName="CustomerReference" VisibleIndex="2" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ref Date" FieldName="CustomerDate" VisibleIndex="2" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DoStatus" VisibleIndex="2" >
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
                </dxwgv:ASPxGridViewExporter>
            </table>
        </div>
    </form>
</body>
</html>
