<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPI_CUS.aspx.cs" Inherits="ReportAccount_OperationReport_KPI_CUS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>DateFrom:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_from" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_to" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Repeater ID="rp" runat="server" OnItemDataBound="rp_ItemDataBound" >
                <HeaderTemplate>
                    <table class="kpi_report">
                        <tr>
                            <th rowspan="2" class="th_small">#</th>
                            <th rowspan="2" class="th_large1">Client</th>
                            <th colspan="2">Import</th>
                            <th colspan="2">Export</th>
                            <th colspan="3">Others</th>
                            <th rowspan="2">TEU</th>
                            <th rowspan="2">Trips</th>
                            <th rowspan="2">Incentive</th>
                            <th rowspan="2">Claim</th>
                            <th rowspan="2">Invoice</th>
                            <th rowspan="2">PSA&nbsp;RB</th>
                        </tr>
                        <tr>
                            <th class="th_small">20</th>
                            <th class="th_small">40</th>
                            <th class="th_small">20</th>
                            <th class="th_small">40</th>
                            <th class="th_small">20</th>
                            <th class="th_small">40</th>
                            <th class="th_small">Empty</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# Eval("ClientId") %></td>
                        <td><%# Eval("i20") %></td>
                        <td><%# Eval("i40") %></td>
                        <td><%# Eval("e20") %></td>
                        <td><%# Eval("e40") %></td>
                        <td><%# Eval("o20") %></td>
                        <td><%# Eval("o40") %></td>
                        <td><%# Eval("oe") %></td>
                        <td><%# Eval("teu") %></td>
                        <td><%# Eval("Trips") %></td>
                        <td><%# Eval("Incentive") %></td>
                        <td><%# Eval("Claim") %></td>
                        <td><%# Eval("inv") %></td>
                        <td><%# Eval("psaRB") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr class="tr_summary">
                        <td colspan="2">&nbsp;</td>
                        <td><asp:Label ID="lb_i20" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_i40" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_e20" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_e40" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_o20" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_o40" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_oe" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_teu" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_Trips" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_Incentive" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_Claim" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_inv" runat="server" ></asp:Label></td>
                        <td><asp:Label ID="lb_psaRB" runat="server" ></asp:Label></td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
