<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ArInvoice.aspx.cs"
    Inherits="Account_ArInvoice" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ar Invoice</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        var clientType = null;
        var clientAcCode = null;
        function PutValue(s, name, custType, acCode) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                clientId = null;
                clientName = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PopupCust(txtId, txtName, txtAcCode) {
            clientId = txtId;
            clientName = txtName;
            clientAcCode = txtAcCode;
            popubCtr.SetContentUrl('SelectPage/CustomerList.aspx');
            popubCtr.SetHeaderText('Customer');
            popubCtr.Show();
        }
        function ShowArInvoice(invN, docType) {
            window.location = 'EditPage/ArInvoiceEdit.aspx?no=' + invN+'&type='+docType;
        }
    </script>

</head>
<body width="800">
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsArInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
         <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
       <table border="0">
            <tr>
                <td>
                    Doc No
                </td>
                <td>
                    <asp:TextBox ID="txt_refNo" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>
                    Party To
                </td>
                <td colspan="3">
             
                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="100%" DropDownWidth="380" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true" 
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    Type
                </td>
                <td>
                                <dxe:ASPxComboBox runat="server" ID="cbo_DocType" width="50" >
                                    <Items>
                                        <dxe:ListEditItem Value="All" Text="All"/>
                                        <dxe:ListEditItem Value="IV" Text="IV" />
                                        <dxe:ListEditItem Value="DN" Text="DN" />
                                     
                                    </Items>
                                </dxe:ASPxComboBox>
                </td>
                <td>
                    PostInd
                </td>
                <td>
                                <dxe:ASPxComboBox runat="server" ID="cbo_PostInd" width="50" >
                                    <Items>
                                        <dxe:ListEditItem Value="All" Text="All"/>
                                        <dxe:ListEditItem Value="Y" Text="Y" />
                                        <dxe:ListEditItem Value="N" Text="N" />
                                    </Items>
                                </dxe:ASPxComboBox>
                </td>
				  <td>
                    Fully Paid
                </td>
                <td>
                                <dxe:ASPxComboBox runat="server" ID="cbo_PaidInd" width="50" >
                                    <Items>
                                        <dxe:ListEditItem Value="All" Text="All"/>
                                        <dxe:ListEditItem Value="Y" Text="Y" />
                                        <dxe:ListEditItem Value="N" Text="N" />
                                    </Items>
                                </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    Date From
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server"  EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    To
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                                    Text="Add IV">
                                    <ClientSideEvents Click="function(s, e) {
                                        ShowArInvoice('0','IV');}" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                                    Text="Add DN">
                                    <ClientSideEvents Click="function(s, e) {
                                        ShowArInvoice('0','DN');}" />
                                </dxe:ASPxButton>
                            </td>
                            <%--<td>
                                <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                </dxe:ASPxButton>
                            </td>--%>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" Width="100" runat="server" Text="Save Excel CSV" OnClick="ASPxButton2_Click">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsArInvoice"
            ClientInstanceName="grid" Width="100%" KeyFieldName="SequenceId">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='ShowArInvoice("<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="3"
                    Width="5%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                    Width="8%">
                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="6"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="6"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="7"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="8"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Bal Amt" FieldName="BalanceAmt" VisibleIndex="8"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PostInd" FieldName="ExportInd" VisibleIndex="9"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CancelInd" FieldName="CancelInd" VisibleIndex="10"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Description" VisibleIndex="99">
                </dxwgv:GridViewDataTextColumn>
				
            </Columns>
			 <Settings ShowFooter="true" />
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem FieldName="DocNo" SummaryType="Count" DisplayFormat="{0:0}" />
                                            <dxwgv:ASPxSummaryItem FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                        </TotalSummary>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            AllowResize="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="popubCtr" HeaderText="Customer" AllowDragging="True" EnableAnimation="False"
            Height="600" Width="900" EnableViewState="False">
            <clientsideevents closeup="function(s, e) {
}" />
            <contentcollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </contentcollection>
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
