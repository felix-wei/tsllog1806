﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectAllRefNo.aspx.cs" Inherits="SelectPage_SelectAllRefNo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ref No</title>
         <script type="text/javascript">
             function $(s) {
                 return document.getElementById(s) ? document.getElementById(s) : s;
             }
             function keydown(e) {
                 if (e.keyCode == 27) { parent.ClosePopupCtr(); }
             }
             document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="MastRefNo" />
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo"  Width="170" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                     <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="MastType" />
                    </td>
                    <td>
                     <dxe:ASPxComboBox ID="cb_Type" runat="server">
                         <Items>
                             <dxe:ListEditItem Text="" Value=""/>
                             <dxe:ListEditItem  Text="Sea Import" Value="SI" />
                             <dxe:ListEditItem  Text="Sea Export" Value="SE"/>
                             <dxe:ListEditItem  Text="Sea CrossTrade" Value="SE"/>
                             <dxe:ListEditItem  Text="Air Import " Value="AI"/>
                             <dxe:ListEditItem  Text="Air Export" Value="AE"/>
                             <dxe:ListEditItem  Text="Air Cross Trade" Value="ACT"/>
                         </Items>
                     </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
                AutoGenerateColumns="False" KeyFieldName="Id">
                <SettingsPager Mode="ShowAllRecords" >
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutValue("<%# Eval("MastRefNo") %>","<%# Eval("MastType") %>");'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="MastRefNo" FieldName="MastRefNo" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Eta" FieldName="Eta" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Etd" FieldName="Etd" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Agent" FieldName="Agent" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pol" FieldName="Pol" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pod" FieldName="Pod" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
    </form>
</body>
</html>