<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPI_Trip_Driver.aspx.cs" Inherits="ReportAccount_OperationReport_KPI_Trip_Driver" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="display:none">
            <table>
                <tr>
                    <td>DateFrom:</td>
                    <td>
                        <%--<dxe:ASPxDateEdit ID="search_from" ClientInstanceName="search_from" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>--%>
                        <dxe:ASPxTextBox ID="search_from" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>To:</td>
                    <td>
                        <%--<dxe:ASPxDateEdit ID="search_to" ClientInstanceName="search_to" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>--%>
                        <dxe:ASPxTextBox ID="search_to" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>Driver:</td>
                    <td>
                        <%--<dxe:ASPxComboBox ID="search_driver" runat="server" DataSourceID="ds_driver" ValueField="Code" TextField="Code"></dxe:ASPxComboBox>--%>
                        <dxe:ASPxTextBox ID="search_driver" runat="server"></dxe:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Repeater ID="rp" runat="server" OnItemDataBound="rp_ItemDataBound">
                <HeaderTemplate>
                    <table class="kpi_report">
                        <tr>
                            <th colspan="8">
                                <asp:Label ID="lb_datefrom" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="lb_dateto" runat="server"></asp:Label>&nbsp;&nbsp;[<asp:Label ID="lb_driver" runat="server"></asp:Label>]
                            </th>
                        </tr>
                        <tr>
                            <th class="th_small">#</th>
                            <th>DeliverDate</th>
                            <th class="th_large">JobNo</th>
                            <th class="th_large">Container</th>
                            <th>Incentive</th>
                            <th>Claim</th>
                            <th class="th_large">FromAddress</th>
                            <th class="th_large">ToAddress</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# SafeValue.SafeDate( Eval("ToDate"),DateTime.Now).ToString("dd/MM/yyyy") %>&nbsp;<%# Eval("ToTime") %></td>
                        <td><%# Eval("JobNo") %></td>
                        <td><%# Eval("ContainerNo") %></td>
                        <td><%# Eval("Incentive") %></td>
                        <td><%# Eval("Claim") %></td>
                        <td><%# Eval("FromCode") %></td>
                        <td><%# Eval("ToCode") %></td>


                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr class="tr_summary">
                        <td colspan="4">&nbsp;</td>
                        <td>
                            <asp:Label ID="lb_Incentive" runat="server"></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="lb_Claim" runat="server"></asp:Label>

                        </td>
                        <td colspan="2"></td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
