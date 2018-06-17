<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditTrail.aspx.cs" Inherits="ReportAccount_RptGl_AuditTrail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GL Audit Trail</title>

    <script type="text/javascript">
        function ShowTab(tabInd) {
            var divAcc = document.getElementById("divAcc");
            var divParty = document.getElementById("divParty");
            if (tabInd == "0") {
                divAcc.style.display = "block";
                divParty.style.display = "none";
            } else if (tabInd == "1") {
                divAcc.style.display = "none";
                divParty.style.display = "block";
            }
        }
        function PopupParty() {
            var partyType = txt_PartyType.GetText();
            if (partyType == "Customer")
                PopupCust(txt_PartyTo, null, null);
            else
                PopupVendor(txt_PartyTo, null, null);
        }
        function Print() {
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {//by chart of account
                parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=45&d1=' + year.GetText() + '&d2=' + period.GetText() + '&d3=' + period1.GetValue() + '&p=' + cmb_acCode.GetValue())

            } else if (tabInd == "1") {//by party to
                if (txt_PartyType.GetText() == "Customer") {
                    if (null == cmb_PartyTo.GetValue())
                        alert("Please select the Customer");
                    else
                        parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=44&d1=' + year.GetText() + '&d2=' + period.GetText() + '&d3=' + period1.GetValue() + '&p=' + cmb_PartyTo.GetValue() + '&cury=C')
                } else {
                    if (null == cmb_PartyTo1.GetValue())
                        alert("Please select the Vendor");
                    else
                        parent.PrintReport('/ReportAccount/Rptprintview.aspx?doc=44&d1=' + year.GetText() + '&d2=' + period.GetText() + '&d3=' + period1.GetValue() + '&p=' + cmb_PartyTo1.GetValue() + '&cury=V')
                }
            }
        } 
        function PrintExcel() {
            var tabInd = rbt.GetValue();
            if (tabInd == "0") {//by chart of account
                    parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=45&d1=' + year.GetText() + '&d2=' + period.GetText() + '&d3=' + period1.GetValue() + '&p=' + cmb_acCode.GetValue())

            } else if (tabInd == "1") {//by party to
                if (txt_PartyType.GetText() == "Customer") {
                    if (null == cmb_PartyTo.GetValue())
                        alert("Please select the Customer");
                    else
                        parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=44&d1=' + year.GetText() + '&d2=' + period.GetText() + '&d3=' + period1.GetValue() + '&p=' + cmb_PartyTo.GetValue() + '&cury=C')
                } else {
                    if (null == cmb_PartyTo1.GetValue())
                        alert("Please select the Vendor");
                    else
                        parent.PrintReport('/ReportAccount/Rptprintview.aspx?docType=1&doc=44&d1=' + year.GetText() + '&d2=' + period.GetText() + '&d3=' + period1.GetValue() + '&p=' + cmb_PartyTo1.GetValue() + '&cury=V')
                }
            }
        }
        function ShowParty(tabParty) {
            var divCust = document.getElementById("divCust");
            var divVendor = document.getElementById("divVendor");
            if (tabParty == "Customer") {
                divCust.style.display = "block";
                divVendor.style.display = "none";
            } else {
                divCust.style.display = "none";
                divVendor.style.display = "block";
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
          <wilson:DataSource ID="dsAcCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartAcc" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
         <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true'" />
  <div>
        <table>
            <tr>
                <td>
                    Year/Period 
                    <dxe:ASPxComboBox ID="cmb_Year" ClientInstanceName="year" runat="server" Width="60">
                        <Items>
                            <dxe:ListEditItem Text="2008" Value="2008" />
                            <dxe:ListEditItem Text="2009" Value="2009" />
                            <dxe:ListEditItem Text="2010" Value="2010" />
                            <dxe:ListEditItem Text="2011" Value="2011" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                &nbsp;
                    <dxe:ASPxComboBox ID="cmb_From" runat="server" Width="40" ClientInstanceName="period">
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
            </tr>
                <td>
                &nbsp;
                    <dxe:ASPxComboBox ID="cmb_To" runat="server" Width="40" ClientInstanceName="period1">
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
            </tr>
            <tr>
                <td colspan="2">
                    <dxe:ASPxRadioButtonList ID="rbt" ClientInstanceName="rbt" runat="server" RepeatDirection="Horizontal">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
                            ShowTab(s.GetValue());
                        }" />
                        <Items>
                            <dxe:ListEditItem Text="Acc Code" Value="0" />
                            
                        </Items>
                    </dxe:ASPxRadioButtonList><%--<dxe:ListEditItem Text="Party" Value="1" />--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="divAcc">
                     Chart of Account
                    <dxe:ASPxComboBox ID="cmb_acCode" ClientInstanceName="cmb_acCode" runat="server"
                        Width="180px" DropDownWidth="250" DropDownStyle="DropDownList" DataSourceID="dsAcCode"
                        ValueField="Code" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="35px" />
                            <dxe:ListBoxColumn FieldName="AcDesc" Width="100%" />
                        </Columns>
                        <Buttons>
                            <dxe:EditButton Text="Clear"></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents buttonclick="function(s, e) {
                            if(e.buttonIndex == 0){
                                s.SetText('');
                         }
                        }" 
                        />
                    </dxe:ASPxComboBox>
                    </div>
                </td>
            </tr>
            </table>
            <table>
            <tr>
                <td>
                    <div id="divParty" style="display: none">
                        <table>
                            <tr>
                                <td>
                                    Party
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="txt_PartyType" ClientInstanceName="txt_PartyType" runat="server"
                                        Width="100">
                                        <Items>
                                            <dxe:ListEditItem Text="Customer" Value="Customer" />
                                            <dxe:ListEditItem Text="Vendor" Value="Vendor" />
                                        </Items>
                                        <ClientSideEvents TextChanged="function(s,e){
                                        ShowParty(s.GetText());
                                        }" />
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                <div id="divCust">
                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="50px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
                        </div>
                        <div id="divVendor" style="display:none">
                                <dxe:ASPxComboBox ID="cmb_PartyTo1" ClientInstanceName="cmb_PartyTo1" runat="server"
                        Width="180px" DropDownWidth="180" DropDownStyle="DropDownList" DataSourceID="dsVendorMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="50px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" Width="110" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                        Print();
                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Export To Excel" Width="110"
                        AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                        PrintExcel();
                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="800" EnableViewState="False">
            <contentcollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </contentcollection>
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
