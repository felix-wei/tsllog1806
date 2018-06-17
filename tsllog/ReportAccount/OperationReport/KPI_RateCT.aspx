<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPI_RateCT.aspx.cs" Inherits="ReportAccount_OperationReport_KPI_RateCT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>ChgCode:</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_chgcode" runat="server" ValueField="c" TextField="c"></dxe:ASPxComboBox>
                    </td>
                    <td>Client:</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_client" runat="server" ValueField="c" TextField="c"></dxe:ASPxComboBox>
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
            <asp:Repeater ID="rp" runat="server" OnItemDataBound="rp_ItemDataBound">
                <HeaderTemplate>
                    <table class="kpi_report">
                        <tr>
                            <th class="th_small">#</th>
                            <th>ChgCode</th>
                            <th class="th_large">Des</th>
                            <th>Unit</th>
                            <th>Price</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# Eval("status5") %></td>
                        <td><%# Eval("des") %></td>
                        <td><%# Eval("unit") %></td>
                        <td><%# Eval("price") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr class="tr_summary">
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
