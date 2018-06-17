﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GstRptReturn.aspx.cs" Inherits="ReportAccount_RptGl_GstRptReturn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GST Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    From Date
                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frm" Width="100" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To Date
                    <dxe:ASPxDateEdit ID="date_To" ClientInstanceName="to" Width="100" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
   
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Print" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                if(frm.GetText()=='')
                                alert('Please select the from date');
                                else if(to.GetText()=='')
                                alert('Please select the end date');
                                else
                                    parent.PrintReport('/ReportAccount/gst_f5.aspx?start_year='+frm.GetText().substring(6,10)+'&start_month='+frm.GetText().substring(3,5)+'&end_year='+to.GetText().substring(6,10)+'&end_month='+to.GetText().substring(3,5))
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton Visible="false" ID="ASPxButton1" runat="server" Text="Export To Excel" Width="110"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                if(null==cmb_PartyTo.GetValue())
                                {
                                alert('Please select the party ');
                                }else if(frm.GetText()=='')
                                alert('Please select the from date');
                                else if(to.GetText()=='')
                                alert('Please select the end date');
                                else
                                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=47&type='+type.GetText()+'&d1='+frm.GetText()+'&d2='+to.GetText())
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>