<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillList.aspx.cs" Inherits="PagesFreight_Account_BillList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice List</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) {
                parent.ClosePopupCtr();
            }
        }
        document.onkeydown = keydown;
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

    <script type="text/javascript" src="../../Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Bill No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_No" runat="server" Text='' Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>AR/AP
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ArAp" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="AR Invoice" Value="AR Invoice" />
                                <dxe:ListEditItem Text="AP Invoice" Value="AP Invoice" />
                                <dxe:ListEditItem Text="AR Quote" Value="AR Quote" />
                                <dxe:ListEditItem Text="AP Quote" Value="AP Quote" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Retrieve" Width="110" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Select All" ClientInstanceName="btnSelect" Width="110" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid_Sch.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td style="display: none">
                        <asp:Label ID="txt_Qty" runat="server" Text=""></asp:Label>
                        <asp:Label ID="txt_ImpExpInd" runat="server" Text=""></asp:Label>
                        <asp:Label ID="txt_FclLclInd" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                <td width="150">
                    Charge Code
                </td>
                <td width="60">
                    Qty
                </td>
                <td width="80">
                    Price
                </td>
                <td width="100">
                    Currency
                </td>
                <td width="50">
                    Ex Rate
                </td>
                <td width="50">
                    Gst Type
                </td>
                <td width="50">
                </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid_Sch" runat="server"
                KeyFieldName="SequenceId" Width="600"
                OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <DataRow>
                        <div style="padding: 5px">
                            <table width="600" style="border-bottom: solid 1px black;">
                                <tr style="font-weight: bold; font-size: 11px">
                                    <td style="width: 100px">
                                        <%# Eval("ChgCode")%>
                                    </td>
                                    <td style="width: 100px">
                                       
                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="60" ID="spin_Qty"
                                    runat="server" DisplayFormatString="0.000" DecimalPlaces="3"
                                    Value='<%# Eval("Qty")%>' Increment="0">
                                </dxe:ASPxSpinEdit>
                                    </td>
                                    <td style="width: 100px">
                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="60" ID="spin_Price"
                                    runat="server" DisplayFormatString="0.00" DecimalPlaces="2"
                                    Value='<%# Eval("Price")%>' Increment="0">
                                </dxe:ASPxSpinEdit>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("Currency")%>
                                    </td>
                                <td style="width: 50px">
                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="60" ID="spin_ExRate"
                                    runat="server" DisplayFormatString="0.000000" DecimalPlaces="6"
                                    Value='<%# Eval("ExRate")%>' Increment="0">
                                </dxe:ASPxSpinEdit>
                                </td>
                                    <td style="width: 50px">
                                        <%# Eval("GstType")%>
                                    </td>
                                    <td width="50" valign="top">
                                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                        </dxe:ASPxCheckBox>
                                        <div style="display: none">

                                            <dxe:ASPxTextBox ID="txt_docId" runat="server"
                                                Text='<%# Eval("SequenceId") %>'>
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </DataRow>
                </Templates>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
