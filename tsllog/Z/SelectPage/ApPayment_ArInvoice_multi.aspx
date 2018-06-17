<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ApPayment_ArInvoice_multi.aspx.cs"
    Inherits="PagesAccount_SelectPage_ApPayment_ArInvoice_multi" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ar Invoice</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
            else {
                parent.AfterPopubMultiInv();
            }
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
        <%--<wilson:DataSource ID="dsArInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />--%>
        <table>
            <tr>
            <td>Bill No
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
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" 
            ClientInstanceName="grid" Width="660" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
            OnCustomCallback="ASPxGridView1_CustomCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                    SortIndex="1" SortOrder="Ascending" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="2"
                    Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="3"
                    SortIndex="0" SortOrder="Descending" Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="5"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="6"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="7"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="8"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="BalanceAmt" FieldName="BalanceAmt" VisibleIndex="9"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Payment Amt" VisibleIndex="10" Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="11">
                    <DataItemTemplate>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFooter="true" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="BalanceAmt" SummaryType="Sum" />
            </TotalSummary>
            <Templates>
                <DataRow>
                    <table border="0" style="border-bottom: solid 1px BLACK; width: 100%">
                        <td width="100">
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_docId" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("SequenceId") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            </div>
                            <dxe:ASPxTextBox ID="txt_docNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocNo") %>' Width="100%">
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="80">
                            <%# Eval("DocDate","{0:dd/MM/yyyy}") %>
                        </td>
                        <td width="50">
                            <dxe:ASPxTextBox ID="txt_DocType" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocType") %>' Width="60%">
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="50">
                            <%# Eval("MastType") %>
                        </td>
                        <td width="50">
                            <%# Eval("CurrencyId") %>
                        </td>
                        <td width="50">
                            <%# Eval("ExRate","{0:0.000000}") %>
                        </td>
                        <td width="50">
                            <%# Eval("DocAmt", "{0:#,##0.00}") %>
                        </td>
                        <td width="50">
                            <%# Eval("LocAmt", "{0:#,##0.00}")%>
                        </td>
                        <td width="50">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_BalanceAmt" DisplayFormatString="0.00" BackColor="Control"
                                ReadOnly="true" runat="server" Value='<%# Eval("BalanceAmt")%>'>
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="50">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_Amt" DisplayFormatString="0.00" Value='<%# Eval("BalanceAmt")%>'
                                runat="server">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="50">
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
