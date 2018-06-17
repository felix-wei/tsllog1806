<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ArReceipt_ApPayalbe_multi.aspx.cs"
    Inherits="PagesAccount_SelectPage_ArReceipt_ApPayalbe_multi" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ap Payment</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>
    <script type="text/javascript">
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
            else
                parent.AfterPopubMultiInv();
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                    this.click();
            });
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
            <td>SupplierBill No
            </td>
            <td>
                            <dxe:ASPxTextBox ID="txt_No" runat="server"
                                Text='' Width="100">
                            </dxe:ASPxTextBox></td>
            <td>Date From</td>
            <td>
                                <dxe:ASPxDateEdit ID="txt_FromDt" runat="server" Width="100"
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit></td>
            <td>To</td>
            <td>
                                <dxe:ASPxDateEdit ID="txt_toDt" runat="server" Width="100"
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit></td>
            <td>
                    <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Retrieve" Width="110" OnClick="btn_Sch_Click">
                    </dxe:ASPxButton></td>
            
                <td>
                    <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
		<hr>
        <table>
            <tr>
                <td width="200">
                    Supplier Bill No
                </td>
                <td width="80">
                    Doc No
                </td>
                <td width="80">
                    Doc Type
                </td>
                <td width="70">
                    Currency
                </td>
                <td width="50">
                    Amount
                </td>
                <td width="90">
                    Balance Amount
                </td>
                <td width="80">
                    Pay Amount
                </td>
                <td width="50">
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server"
            ClientInstanceName="grid" Width="740" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
            OnCustomCallback="ASPxGridView1_CustomCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior AllowFocusedRow="True" />
            <Columns>
            </Columns>
            <Settings ShowFooter="true" ShowColumnHeaders="false" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="BalanceAmt" SummaryType="Sum" />
            </TotalSummary>
            <Columns>
            
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="SupplierBillNo" VisibleIndex="1" SortIndex="0" SortOrder="Ascending"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="BalanceAmt" FieldName="BalanceAmt" VisibleIndex="9"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DataRow>
                    <table border="0" style="border-bottom: solid 1px BLACK; width: 100%">
                        <td width="130" valign="top">
                            <dxe:ASPxTextBox ID="txt_billNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("SupplierBillNo") %>' Width="60%">
                            </dxe:ASPxTextBox>
                            <%# Eval("SupplierBillDate","{0:dd/MM/yyyy}") %>
                        </td>
                        <td width="50" valign="top">
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_docId" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("SequenceId") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            </div>
                            <dxe:ASPxTextBox ID="txt_docNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocNo") %>' Width="60%">
                            </dxe:ASPxTextBox>
                            <%# Eval("DocDate","{0:dd/MM/yyyy}") %>
                        </td>
                        <td width="50" valign="top">
                            <dxe:ASPxTextBox ID="txt_DocType" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocType") %>' Width="100%">
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="50" valign="top">
                            <%# Eval("CurrencyId") %>
                            <br />
                            <%# Eval("ExRate","{0:0.000000}") %>
                        </td>
                        <td width="50" valign="top">
                            <%# Eval("DocAmt", "{0:#,##0.00}") %>
                            <br />
                            <%# Eval("LocAmt", "{0:#,##0.00}")%>
                        </td>
                        <td width="50" valign="top">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_BalanceAmt" DisplayFormatString="0.00" ReadOnly="true"
                                runat="server" Value='<%# Eval("BalanceAmt")%>'>
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="50" valign="top">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_Amt" DisplayFormatString="0.00" runat="server"
                                Value='<%# Eval("BalanceAmt")%>'>
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="50" valign="top">
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                        </td>
                        </tr>
                    </table>
                </DataRow>
            </Templates>
        </dxwgv:ASPxGridView>
        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
        </dxe:ASPxButton>
    </div>
    </form>
</body>
</html>
