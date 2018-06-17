<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlStatement.aspx.cs" Inherits="ReportAccount_RptGl_PlStatement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GL P&&L Statement</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Year
                    <dxe:ASPxComboBox ID="cmb_Year" ClientInstanceName="year" runat="server" Width="80">
                        <Items>
                            <dxe:ListEditItem Text="2008" Value="2008" />
                            <dxe:ListEditItem Text="2009" Value="2009" />
                            <dxe:ListEditItem Text="2010" Value="2010" />
                            <dxe:ListEditItem Text="2011" Value="2011" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                </tr><tr>
                <td>
                    Period
                    <dxe:ASPxComboBox ID="cmb_Period" ClientInstanceName="period" runat="server" Width="80">
                        <Items>
                            <dxe:ListEditItem Text="1" Value="1" />
                            <dxe:ListEditItem Text="2" Value="2" />
                            <dxe:ListEditItem Text="3" Value="3" />
                            <dxe:ListEditItem Text="4" Value="4" />
                            <dxe:ListEditItem Text="5" Value="5" />
                            <dxe:ListEditItem Text="6" Value="6" />
                            <dxe:ListEditItem Text="7" Value="7" />
                            <dxe:ListEditItem Text="8" Value="8" />
                            <dxe:ListEditItem Text="9" Value="9" />
                            <dxe:ListEditItem Text="10" Value="10" />
                            <dxe:ListEditItem Text="11" Value="11" />
                            <dxe:ListEditItem Text="12" Value="12" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                 </tr><tr>
               <td>
                    <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Print" AutoPostBack="False"  Width="110"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                            parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=43&d1='+year.GetText()+'&d2='+period.GetText());
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr><td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Export to Excel" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                            parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=43&d1='+year.GetText()+'&d2='+period.GetText());
                                    }" />
                    </dxe:ASPxButton></td></tr>
        </table>
           
     </div>
    </form>
</body>
</html>
