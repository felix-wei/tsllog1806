<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ApPayment_Prepayment_multi.aspx.cs"
    Inherits="PagesAccount_SelectPage_ApPayment_Prepayment_multi" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ap Payment</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

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

    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsApPayable" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0"  />
        <table>
            <tr style="display:none;">
            <td>Doc No
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
			</tr>
			<tr>
				<td colspan=4>&nbsp;</td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" Width="100px" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td width="100">
                    Prepayment No
                </td>
                <td width="100">
                    Doc Date
                </td>
                <td width="80">
                    Doc Type
                </td>
                <td width="100">
                    Currency
                </td>
                <td width="100">
                    Amount
                </td>
                <td width="100">
                    Balance Amount
                </td>
                <td width="100">
                    Applied Amount
                </td>
                <td width="100">
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server"
            ClientInstanceName="grid" Width="820" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
            OnCustomCallback="ASPxGridView1_CustomCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Settings ShowFooter="true" ShowColumnHeaders="false" />
            <SettingsBehavior AllowFocusedRow="True" />
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
                    <table border="0" style="border-bottom: solid 1px BLACK; width: 800px">
                        <td width="100" valign="top">
                            <dxe:ASPxTextBox ID="txt_billNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("SupplierBillNo") %>' Width="80">
                            </dxe:ASPxTextBox>
						</td>
						<td width=100>
                            <%# Eval("SupplierBillDate","{0:dd/MM/yyyy}") %>
                        </td>
                        <td width="100" valign="top" style="display:none;">
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_docId" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("SequenceId") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            <dxe:ASPxTextBox ID="txt_docNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocNo") %>' Width="100%">
                            </dxe:ASPxTextBox>
                            <%# Eval("DocDate","{0:dd/MM/yyyy}") %>
                            </div>
                        </td>
                        <td width="100" valign="top">
                            <dxe:ASPxTextBox ID="txt_DocType" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocType") %>' Width="80">
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="100" valign="top">
                            <%# Eval("CurrencyId") %>
                            @
                            <%# Eval("ExRate","{0:0.00000}") %>
                        </td>
                        <td width="100" valign="top">
                            <%# Eval("BalanceAmt", "{0:#,##0.00}")%>
                        </td>
                        <td width="100" valign="top"  >
                            <dxe:ASPxSpinEdit   ID="spin_BalanceAmt" DisplayFormatString="0.00" ReadOnly="true"
                                runat="server" Value='<%# Eval("BalanceAmt")%>' Width="90">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="100" valign="top">
                            <dxe:ASPxSpinEdit  ID="spin_Amt" DisplayFormatString="0.00" runat="server"
                                Value='<%# Eval("BalanceAmt")%>'  Width="90">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="100" valign="top">
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                        </td>
                        </tr>
                    </table>
                </DataRow>
            </Templates>
        </dxwgv:ASPxGridView>
      
    </div>
    </form>
</body>
</html>
