﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CandidateList.aspx.cs" Inherits="PagesSelectpge_CandidateList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Candidate List</title>
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
        <table>
            <tr style="font-weight: bold; font-size: 11px">
                <td width="8"></td>
                <td width="125">
                    Name
                </td>
                <td width="75">
                    Gender
                </td>
                <td width="110">
                    IcNo
                </td>
                <td width="118">
                    Telephone
                </td>
                <td width="108">
                    Email
                </td>
                <td width="50">
                    BirthDay
                </td>
                <td width="50">
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid_Sch" runat="server" 
            KeyFieldName="SequenceId" Width="750"
            OnCustomDataCallback="grid_CustomDataCallback">
            <SettingsPager Mode="ShowAllRecords" />
            <Settings ShowColumnHeaders="false" />
              <templates>
                <DataRow>
                    <div style="padding: 5px">
                        <table width="750" style="border-bottom: solid 1px black;">
                            <tr style="font-weight: bold; font-size: 11px">
                                <td style="width: 100px">
                                <dxe:ASPxTextBox Width="110" ID="ASPxTextBox2" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                    Value='<%# Eval("Name")%>'>
                                </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 60px">
                                    
                                <dxe:ASPxTextBox runat="server" Width="60" ID="txt_Gender" ReadOnly="true" Border-BorderWidth="0"
                                    Value='<%# Eval("Gender")%>'>
                                </dxe:ASPxTextBox>
                                    
                                </td>
                                <td style="width: 90px">
                                <dxe:ASPxTextBox Width="90" ID="txt_IcNo" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                    Value='<%# Eval("IcNo")%>'>
                                </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 100px">
                                <dxe:ASPxTextBox Width="90" ID="ASPxTextBox3" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                    Value='<%# Eval("Telephone")%>'>
                                </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 90px">
                                <dxe:ASPxTextBox Width="90" ID="txt_Email" runat="server" ReadOnly="true" Border-BorderWidth="0"
                                    Value='<%# Eval("Email")%>'>
                                </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 100px">
                                <dxe:ASPxTextBox Width="100" ID="ASPxTextBox1" runat="server" ReadOnly="true"  Border-BorderWidth="0"
                                    Value='<%# SafeValue.SafeDateStr(Eval("BirthDay")) %>'>
                                </dxe:ASPxTextBox>
                                </td>
                        <td width="50" valign="top">
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="txt_detId" runat="server"
                                    Text='<%# Eval("Id") %>'>
                                </dxe:ASPxTextBox>
                            </div>
                        </td>
                            </tr>
                        </table>
                    </div>
                </DataRow>
            </templates>
</dxwgv:ASPxGridView> 
</div> </form> </body> </html> 