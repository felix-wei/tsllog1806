<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArReceiptCn.aspx.cs" Inherits="Account_ArReceiptCn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Receipt</title>
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
        function ShowArReceipt(invN, typ) {
			if(typ=="RVC")
				window.location = 'EditPage/ArReceiptEdit.aspx?no=' + invN + '&t=' + typ;
			else
				window.location = 'EditPage/ArReceiptEditGl.aspx?no=' + invN + '&t=' + typ;
        }
    </script>

</head>
<body width="800">
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsArReceipt" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArReceipt" KeyMember="SequenceId" FilterExpression="1=0" />
          <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true'" />
       <table border="0" width="880">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Receipt No"></asp:Label>
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
                <td>Cheque No</td>
                <td>
                    <asp:TextBox ID="txt_ChqNo" Width="100" runat="server"></asp:TextBox></td>
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
            </tr>
            <tr>
                <td>Invoice No</td>
                <td>
                    <asp:TextBox ID="txt_InvNo" Width="100" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Date From"></asp:Label>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td colspan="4">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Add" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                                    Text="Create New" Width="110">
                                    <ClientSideEvents Click="function(s, e) {
    ShowArReceipt('0',txt_doctype.GetText());}" />
                                </dxe:ASPxButton>
                    <div style="display:none">
						<dxe:ASPxTextBox ID="txt_doctype" ClientInstanceName="txt_doctype" Width="100" runat="server"></dxe:ASPxTextBox>
					</div>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsArReceipt" Width="880" KeyFieldName="SequenceId" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick="ShowArReceipt('<%# Eval("DocNo") %>',txt_doctype.GetText());">Edit</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Voucher No" FieldName="DocNo" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="DocType" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Rec From" FieldName="PartyName" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                    Width="8%">
                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="AcCode" FieldName="AcCode" VisibleIndex="6"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="6" Width=120>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="DocCurrency" VisibleIndex="6"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="DocExRate" VisibleIndex="7"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.0000}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="8"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="9"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Posted" FieldName="ExportInd" VisibleIndex="90"
                    Width="5%">
                    <DataItemTemplate>
                        <%# Eval("ExportInd","{0}") == "Y" ? "" : "N" %>
                    </DataItemTemplate>

					</dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Catetory" FieldName="DocType1" VisibleIndex="90"
                    Width="5%">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
			
			<Settings ShowFooter="true" />
                                    <TotalSummary>
                                        <dxwgv:ASPxSummaryItem FieldName="LocAmt" ShowInColumn="LocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                    </TotalSummary>

        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
        HeaderText="Ar Receipt Edit" AllowDragging="True" EnableAnimation="False" Height="400"
        Width="600" EnableViewState="False">
        <contentcollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </contentcollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
