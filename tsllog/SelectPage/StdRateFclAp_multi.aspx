<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="StdRateFclAp_multi.aspx.cs"
    Inherits="PagesFreight_SelectPage_StdRateFcl_multi" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ar Invoice</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>

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

    <script type="text/javascript" src="../../Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsSeaApQuoteDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaApQuoteDet1" KeyMember="SequenceId" FilterExpression="1=0" />
        <table>
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
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsSeaApQuoteDet"
            ClientInstanceName="grid" Width="500" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
            >
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Description" VisibleIndex="3"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Currency" VisibleIndex="5"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Price" VisibleIndex="5"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unit" VisibleIndex="5" Width="80">
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
                        <td width="120">
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_docId" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("SequenceId") %>' Width="100%">
                                </dxe:ASPxTextBox>
                            </div>
                            <%# Eval("ChgDes") %>
                        </td>
                        <td width="80">
                            <%# Eval("Currency")%>
                        </td>
                        <td width="80">
                            <%# Eval("Price","{0:#,##0.00}") %>
                        </td>
                        <td width="80">
                            <%# Eval("Unit") %>
                        </td>
                        <td width="80">
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
