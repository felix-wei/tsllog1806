﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArList.aspx.cs" Inherits="SelectPage_ArList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ApQuotation</title>

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
            <wilson:DataSource ID="dsArDet" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArInvoiceDet" KeyMember="SequenceId" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>Doc No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_No" runat="server" Text='' Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Retrieve" Width="110" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
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
                        grid_Sch.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td width="100">Charge Code
                    </td>
                    <td width="200">Charge Code Des
                    </td>
                    <td width="100">Price
                    </td>
                    <td width="100">Currency
                    </td>
                    <td width="50">ExRate
                    </td>
                    <td width="50"></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid_Sch" runat="server" DataSourceID="dsArDet"
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
                                    <td style="width: 200px">
                                        <%# Eval("ChgDes1")%>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("Price")%>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("Currency")%>
                                    </td>
                                    <td style="width: 50px">
                                        <%# Eval("ExRate")%>
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
