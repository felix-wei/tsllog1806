<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApPayable.aspx.cs" Inherits="Account_ApPayable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payable</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ShowArInvoice(invN, docType) {
            window.location = 'EditPage/ApPayableEdit.aspx?type=' + docType + '&no=' + invN;
        }

	function open_payment(no,typ)
	{
		var url = '/pagesAccount/editpage/appaymentedit.aspx?no=' + no +'&type=' + typ;
		var title = no; //'GL:/'+v1+'/'+y1+'/' + m1;
		parent.navTab.openTab(title,url,{title:title, fresh:true, external:true});
	}
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsApPayable" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
         <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
        <table border="0" width="880">
            <tr>
                <td>
                    Doc No
                </td>
                <td>
                    <asp:TextBox ID="txt_refNo" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>
                    Type
                </td>
                <td>
                    <dxe:ASPxComboBox runat="server" ID="cbo_DocType" Width="100">
                        <Items>
                            <dxe:ListEditItem Value="All" Text="All" />
                            <dxe:ListEditItem Value="PIN" Text="PIN" />
                            <dxe:ListEditItem Value="PDN" Text="PDN" />
                            <dxe:ListEditItem Value="PCN" Text="PCN" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    Party To
                </td>
                <td colspan="3">
                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="100%" DropDownWidth="380" DropDownStyle="DropDownList" DataSourceID="dsVendorMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="40px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
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
            </tr>
            <tr>
                <td>
                    Supplier Bill No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_supplyBillNo" Width="100" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    Date From
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server"  EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    to
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td colspan="2">
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
    ShowArInvoice('0',txt_doctype.GetText());}" />
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
            DataSourceID="dsApPayable" Width="880px" KeyFieldName="SequenceId" 
            AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='ShowArInvoice("<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="SupplierBillNo" FieldName="SupplierBillNo" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="MastRefNo" VisibleIndex="1"
                    Width="8%"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="1"
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
                <dxwgv:GridViewDataTextColumn Caption="Balance" FieldName="BalanceAmt" VisibleIndex="8"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Payment" FieldName="DocNo" VisibleIndex="8"
                    Width="8%">
						<DataItemTemplate>
						<%# get_pay_list(Eval("SequenceId")) %>
						</DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Post" FieldName="ExportInd" VisibleIndex="9"
                    Width="8%">
                    <DataItemTemplate>
                        <%# Eval("ExportInd","{0}") == "Y" ? "" : "N" %>
                    </DataItemTemplate>

                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Cancel" FieldName="CancelInd" VisibleIndex="10"
                    Width="8%">
                    <DataItemTemplate>
                        <%# Eval("CancelInd","{0}") == "N" ? "" : "Y" %>
                    </DataItemTemplate>

                </dxwgv:GridViewDataTextColumn>
            </Columns>
			
						<Settings ShowFooter="true" />
                                    <TotalSummary>
                                        <dxwgv:ASPxSummaryItem FieldName="LocAmt" ShowInColumn="LocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                    </TotalSummary>

				</dxwgv:ASPxGridView>

			
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
        HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
        Width="800" EnableViewState="False">
        <contentcollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </contentcollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
