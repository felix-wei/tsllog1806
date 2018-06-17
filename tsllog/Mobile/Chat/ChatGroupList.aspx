<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatGroupList.aspx.cs" Inherits="Mobile_Chat_ChatGroupList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function openChat(v) {
            window.location.href = "ChatGroupChannel.aspx?No=" + v;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Name:</td>
                    <td><dxe:ASPxTextBox ID="search_name" runat="server"  Width="120"></dxe:ASPxTextBox></td>
                    <td><dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click" ></dxe:ASPxButton></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gv" runat="server" ClientInstanceName="gv" AutoGenerateColumns="false"  KeyFieldName="Id" Width="850" OnBeforePerformDataSelect="gv_BeforePerformDataSelect">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="Id" Caption="#" Width="10%" CellStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <a href="#" onclick='openChat("<%# Eval("Id") %>");'>Open</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="group_name" Caption="GroupName" Width="90%"></dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
