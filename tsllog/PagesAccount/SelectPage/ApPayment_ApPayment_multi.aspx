<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ApPayment_ApPayment_multi.aspx.cs"
    Inherits="PagesAccount_SelectPage_ApPayment_ApPayment_multi" %>

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
            TypeName="C2.XAApPayment" KeyMember="SequenceId" FilterExpression="1=0"  />
        <table>
            <tr>
            <td>PC No
            </td>
            <td>
                            <dxe:ASPxTextBox ID="txt_No" runat="server"
                                Text='' Width="100">
                            </dxe:ASPxTextBox></td>
            <td>Date</td>
            <td>
                                <dxe:ASPxDateEdit ID="txt_FromDt" runat="server" Width="90"
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit></td>
            <td>To</td>
            <td>
                                <dxe:ASPxDateEdit ID="txt_toDt" runat="server" Width="90"
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
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Save" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td width="150">
                    PC No
                </td>
                <td width="100">
                    Date
                </td>
                <td width="350">
                    Remark
                </td>
                <td width="100">
                    Pay Amount
                </td>
                <td width="100">
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server"
            ClientInstanceName="grid" Width="740" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
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
            
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1" SortIndex="0" SortOrder="Ascending"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="9"
                    Width="50">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DataRow>
                    <table border="0" style="border-bottom: solid 1px BLACK; width: 100%">
                        <td width="150" valign="top">
                            <dxe:ASPxTextBox ID="txt_billNo" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("DocNo") %>' Width="90%">
                            </dxe:ASPxTextBox>
                           
                        </td>
                        <td width="100" valign="top">
                            
                            <%# Eval("SupplierBillDate","{0:dd/MM/yyyy}") %>
                        </td>
                        <td width="350" style="width:350px;" valign="top">
                           
                            <%# Eval("Remark","{0}") %>
                        </td>

                        <td width="100" valign="top">
                            <dxe:ASPxSpinEdit Width="100%" ID="spin_Amt" DisplayFormatString="0.00" runat="server" ReadOnly="true"
                                Value='<%# Eval("LocAmt")%>'>
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td width="100" valign="top">
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
							                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_docId" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("SequenceId") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            </div>

                        </td>
                        </tr>
                    </table>
                </DataRow>
            </Templates>
        </dxwgv:ASPxGridView>
        <%--ClientSideEvents-CheckedChanged='<%# "function(s) {grid.PerformCallback("+Container.VisibleIndex+") }"  %>'--%>
        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false" Visible="false">
            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
        </dxe:ASPxButton>
    </div>
    </form>
</body>
</html>
