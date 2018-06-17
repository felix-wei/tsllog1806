<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPI_Trip.aspx.cs" Inherits="ReportAccount_OperationReport_KPI_Trip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript">
        function driver_view(driver) {
            var from = getDateString(search_from.GetValue());
            var to = getDateString(search_to.GetValue());
            //console.log(driver, from, to);
            window.location.href = 'KPI_Trip_Driver.aspx?driver=' + driver + '&from=' + from + '&to=' + to;
        }
        function getDateString(dd) {
            var year = dd.getFullYear();
            var month = dd.getMonth() + 1;
            var day = dd.getDate();
            var res = '' + year;
            res += (100 + month).toString().substr(1);
            res += (100 + day).toString().substr(1);
            return res;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="ds_driver" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmDriver" KeyMember="Id" FilterExpression="StatusCode='Active'" />
        <div>
            <table>
                <tr>
                    <td>DateFrom:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_from" ClientInstanceName="search_from" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To:</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_to" ClientInstanceName="search_to" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>Driver:</td>
                    <td>
                        <dxe:ASPxComboBox ID="search_driver" runat="server" DataSourceID="ds_driver" ValueField="Code" TextField="Code"></dxe:ASPxComboBox>
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
            <asp:Repeater ID="rp" runat="server"  OnItemDataBound="rp_ItemDataBound">
                <HeaderTemplate>
                    <table class="kpi_report">
                        <tr>
                            <th colspan="6">
                                <asp:Label ID="lb_datefrom" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="lb_dateto" runat="server"></asp:Label></th>
                        </tr>
                        <tr>
                            <th class="th_small">#</th>
                            <th class="th_large">Driver</th>
                            <th>Trips</th>
                            <th>SHF</th>
                            <th>Incentive</th>
                            <th>Claim</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr onclick="driver_view('<%# Eval("DriverCode") %>');">
                        <td><%# Container.ItemIndex+1 %></td>
                        <td><%# Eval("DriverCode") %></td>
                        <td><%# Eval("Trips") %></td>
                        <td><%# Eval("shf") %></td>
                        <td><%# Eval("Incentive") %></td>
                        <td><%# Eval("Claim") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr class="tr_summary">
                        <td colspan="2">&nbsp;</td>
                        <td>
                            <asp:Label ID="lb_Trips" runat="server"></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="lb_shf" runat="server"></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="lb_Incentive" runat="server"></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="lb_Claim" runat="server"></asp:Label>

                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </form>
</body>
</html>
