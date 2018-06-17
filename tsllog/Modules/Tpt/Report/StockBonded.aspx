<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockBonded.aspx.cs" Inherits="Modules_Tpt_Report_StockBonded" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function Print() {
            var no = cmb_PartyTo.GetValue();
            var from = frmDate.GetText();
            parent.PrintReport('/Modules/Tpt/Report/RptPrintView.aspx?docType=0&doc=stock_bonded&no=' + no + '&d1=' + from + '&d2=0')
        }
        function ExportToExcel() {
            var no = cmb_PartyTo.GetValue();
            var from = frmDate.GetText();
            parent.PrintReport('/Modules/Tpt/Report/RptPrintView.aspx?docType=1&doc=stock_bonded&no=' + no + '&d1=' + from + '&d2=0')
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" />
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Client"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="180" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                        <Buttons>
                            <dxe:EditButton Text="Reset"></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                            if(e.buttonIndex == 0){
                                s.SetText('NA');
                         }
                        }" />
                    </dxe:ASPxComboBox>
                </td>

            </tr>
             <tr style="display:none">
                <td>
                    Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" Width="140" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
        </table>
         <table>
            <tr>
                <td colspan="2">
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Width="120" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Export To Excel" Width="120"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcel();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
