﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContainerList.aspx.cs" Inherits="SelectPage_ContainerList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Name" />
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="170" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
                KeyFieldName="Id"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutValue("<%# Eval("ContainerNo") %>","<%# Eval("ContainerType") %>");'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerNo" FieldName="ContainerNo" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerType" FieldName="ContainerType" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
