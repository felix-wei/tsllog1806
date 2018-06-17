<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerTrans.aspx.cs" Inherits="ReportAccount_RptAr_CustomerTrans" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AR Customer Trans</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" />
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
                    To Date
                    <dxe:ASPxDateEdit ID="date_To" ClientInstanceName="to" Width="100" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    Account Type
                    <dxe:ASPxRadioButtonList ID="rbt" runat="server" RepeatDirection="Horizontal" ClientInstanceName="cury">
                        <Items>
                            <dxe:ListEditItem Text="Local" Value="Local" />
                            <dxe:ListEditItem Text="Foreign" Value="Foreign" />
                        </Items>
                    </dxe:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Party To
                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="180px" DropDownWidth="280px" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="15px" />
                            <dxe:ListBoxColumn FieldName="Name" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Print" Width="110" AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                if(frm.GetText()=='')
                                alert('Please select the from date');
                                else if(to.GetText()=='')
                                alert('Please select the end date');
                                else
                                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=1&d1='+frm.GetText()+'&d2='+to.GetText()+'&cury='+cury.GetSelectedIndex()+'&p='+cmb_PartyTo.GetValue())
                                    }" />
                                </dxe:ASPxButton>
                            </td>
            </tr>
            <tr>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Export To Excel" Width="110" AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                if(null==cmb_PartyTo.GetValue())
                                {
                                alert('Please select the party ');
                                }else if(frm.GetText()=='')
                                alert('Please select the from date');
                                else if(to.GetText()=='')
                                alert('Please select the end date');
                                else
                                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=1&d1='+frm.GetText()+'&d2='+to.GetText()+'&cury='+cury.GetSelectedIndex()+'&p='+cmb_PartyTo.GetValue())
                                    }" />
                                </dxe:ASPxButton>
                            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
