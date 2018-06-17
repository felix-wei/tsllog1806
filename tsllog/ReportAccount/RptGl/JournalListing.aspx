<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JournalListing.aspx.cs" Inherits="ReportAccount_RptGl_JournalListing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GL Jouranl Listing</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    From Date
                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frm" Width="100" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To
                    <dxe:ASPxDateEdit ID="date_To" ClientInstanceName="to" Width="100" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
        <dxe:ASPxRadioButtonList ID="rbt" ClientInstanceName="rbt" runat="server" RepeatDirection="Horizontal">
            <Items>
                <dxe:ListEditItem Text="GE" Value="3" />
            </Items>
        </dxe:ASPxRadioButtonList>
                <%--<dxe:ListEditItem Text="RE" Value="0" />
                <dxe:ListEditItem Text="GL" Value="1" />
                <dxe:ListEditItem Text="EX" Value="2" />--%>
                </td>
            </tr>
            <tr>
        <td>
        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" Width="110" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                             parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=46&d1='+frm.GetText()+'&d2='+to.GetText()+'&p='+rbt.GetValue())
                        }" />
                        </dxe:ASPxButton></td>
            </tr>
            <tr>
        <td>
        <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Export To Excel" Width="110" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                              parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=46&d1='+frm.GetText()+'&d2='+to.GetText()+'&p='+rbt.GetValue())
                        }" />
                        </dxe:ASPxButton></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
