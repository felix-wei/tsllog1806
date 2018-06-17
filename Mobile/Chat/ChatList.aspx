<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatList.aspx.cs" Inherits="Mobile_Chat_ChatList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function openChat(v) {
            window.location.href = "ChatChannel.aspx?No=" + v;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsUser" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.User" KeyMember="SequenceId" FilterExpression="" />

            <table>
                <tr>
                    <td>Name:</td>
                    <td><dxe:ASPxTextBox ID="search_name" runat="server"  Width="120"></dxe:ASPxTextBox></td>
                    <td><dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click" ></dxe:ASPxButton></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gv" runat="server" ClientInstanceName="gv" AutoGenerateColumns="false" DataSourceID="dsUser" KeyFieldName="SequenceId" Width="850" OnBeforePerformDataSelect="gv_BeforePerformDataSelect">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="SequenceId" Caption="#" Width="10%" CellStyle-HorizontalAlign="Center">
                        <DataItemTemplate>
                            <a href="#" onclick='openChat("<%# Eval("SequenceId") %>");'>Open</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Name" Caption="Name" Width="45%"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Role" Caption="Role" Width="45%"></dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
