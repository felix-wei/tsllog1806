﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ApPayment_ApPayable_multi.aspx.cs"
    Inherits="PagesAccount_SelectPage_ApPayment_ApPayable_multi" %>

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
            <tr>
            <td>Supp.Bill No
            </td>
            <td>
                            <dxe:ASPxTextBox ID="txt_No" runat="server"
                                Text='' Width="100">
                            </dxe:ASPxTextBox></td>
            <td>Date</td>
            <td>
                                <dxe:ASPxDateEdit ID="txt_FromDt" runat="server" Width="100"
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit></td>
            <td>-</td>
            <td>
                                <dxe:ASPxDateEdit ID="txt_toDt" runat="server" Width="100"
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit></td>
            <td>
                    <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Retrieve" Width="110" OnClick="btn_Sch_Click">
                    </dxe:ASPxButton></td>
<td width=30></td>
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
                <td width="160">
                    Supplier Bill No
                </td>
                <td width="70">
                    Doc Date
                </td>
                <td width="40">
                    Type
                </td>
                <td width="40">
                    Curr
                </td>
                <td width="60">
                    Ex.Rate
                </td>
                <td width="70" align=center>
                    Amount&nbsp;&nbsp;
                </td>
                <td width="90">
                    Balance Amt
                </td>
                <td width="80">
                    To Pay
                </td>
                <td width="80">
				    To Utilise
                </td>
                <td width="30">
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server"
            ClientInstanceName="grid" Width="840" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
            OnCustomCallback="ASPxGridView1_CustomCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Settings ShowFooter="true" ShowColumnHeaders="false" />
    
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="BalanceAmt1" SummaryType="Sum" />
            </TotalSummary>
            <Columns>
            
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="SupplierBillNo" VisibleIndex="1" 
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="SupplierBillNo" VisibleIndex="1" 
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="SupplierBillNo" VisibleIndex="1" 
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Balance" FieldName="BalanceAmt1" VisibleIndex="9"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Balance" FieldName="BalanceAmt" VisibleIndex="9"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Balance" FieldName="BalanceAmt" VisibleIndex="9"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Balance" FieldName="BalanceAmt" VisibleIndex="9"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DataRow>
                    <table border="0" style="border-bottom: solid 1px BLACK; ">
                        <td width="160" valign="top">
                            <dxe:ASPxTextBox ID="txt_billNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("SupplierBillNo") %>' Width="160">
                            </dxe:ASPxTextBox>
			</td>
			<td width="60">	
                            <%# Eval("SupplierBillDate","{0:dd/MM/yy}") %>
                         
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_docId" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("SequenceId") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            <dxe:ASPxTextBox ID="txt_docNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocNo") %>' Width="100%">
                            </dxe:ASPxTextBox>
                            </div>
			</td>
	
                        <td width="40" valign="top">
                            <dxe:ASPxTextBox ID="txt_DocType" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocType") %>' Width="100%">
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="40" valign="top" Align=center>
                            <%# Eval("CurrencyId") %>
			</td>
			<td width="60" align="center">	
                            <%# Eval("ExRate","{0:0.0000}") %>
                        </td>
                        <td width="80" valign="top" align=center>
                            <%# Eval("DocAmt1", "{0:#,##0.00}") %>
			</td>
                 
                        <td width="80" valign="top">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_BalanceAmt" DisplayFormatString="0.00" ReadOnly="true"
                                runat="server" Value='<%# Eval("BalanceAmt1")%>'>
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="80" valign="top">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_Amt" OnInit="bal_amt_init" DisplayFormatString="0.00" runat="server"
                                Value='<%# Eval("BalanceAmt")%>'>
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="80">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_Amt2" OnInit="bal_copy_init" DisplayFormatString="0.00" Value='<%# Eval("BalanceAmt1") %>'
                                runat="server">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="40" valign="top">
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                        </td>
                        </tr>
                    </table>
                </DataRow>
            </Templates>
        </dxwgv:ASPxGridView>
        <%--ClientSideEvents-CheckedChanged='<%# "function(s) {grid.PerformCallback("+Container.VisibleIndex+") }"  %>'--%>
        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
        </dxe:ASPxButton>
    </div>
    </form>
</body>
</html>
