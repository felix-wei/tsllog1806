<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDetFromPO.aspx.cs" Inherits="WareHouse_Account_AddDetFromPO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
       <title>Product List</title>
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
                    <td><dxe:ASPxLabel ID="lbl_No" runat="server" Text=""></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_No" runat="server" Text='' Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="display:none">Type:
                    </td>
                    <td style="display:none">
                        <dxe:ASPxLabel ID="cmb_PoSo" runat="server" Width="80">
                        </dxe:ASPxLabel>
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
                </tr>
            </table>
            <table style="border:thin solid #C0C0C0; color: #000000; background-color: #CCCCCC; width:700px">
                <tr>
                    <td style="width: 120px">Product
                    </td>
                    <td style="width: 60px">BalQty
                    </td>
                    <td style="width: 100px">PackQty
                    </td>
                    <td style="width: 100px">UnitQty
                    </td>
                    <td style="width: 100px">Price
                    </td>
                    <td>Currency
                    </td>
                    <td>Gst Type
                    </td>
                    <td></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid_Sch" runat="server"
                KeyFieldName="Id" Width="600"
                OnCustomDataCallback="grid_CustomDataCallback" >
                <SettingsPager Mode="ShowAllRecords" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                   <DataRow>
                        <div style="padding: 5px">
                            <table style="border-bottom: solid 1px black; width:700px">
                                <tr style="font-weight: bold; font-size: 11px">
                                    <td style="width: 120px">
                                        <dxe:ASPxLabel ID="lblProduct" runat="server" Text='<%# Eval("Product")%>'></dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 60px">
                                        <dxe:ASPxLabel ID="ASPxLabel1" Width="60" runat="server" Text='<%# Eval("BalQty")%>'></dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 100px">
                                        <dxe:ASPxSpinEdit Increment="0" Width="100" ID="spin_det_Qty1"
                                            ClientInstanceName="spin_det_Qty1" runat="server" >
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>

                                    </td>
                                     <td style="width: 100px">
                                        <dxe:ASPxSpinEdit Increment="0" Width="100" ID="spin_det_Qt2"
                                            ClientInstanceName="spin_det_Qt2" runat="server" >
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>

                                    </td>
                                    <td style="width: 100px">
                                        <dxe:ASPxLabel ID="lblPrice" Width="100"  runat="server" Text='<%# Eval("Price")%>'></dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 100px; text-align: center">
                                        <%# Eval("Currency")%>
                                    </td>
                                    <td style="width: 100px; text-align: center">
                                        <%# Eval("GstType")%>
                                    </td>
                                    <td width="50" valign="top">
                                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                        </dxe:ASPxCheckBox>
                                        <div style="display: none">

                                            <dxe:ASPxTextBox ID="txt_docId" runat="server"
                                                Text='<%# Eval("Id") %>'>
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
