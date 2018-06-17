<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventLog.aspx.cs" Inherits="PagesContTrucking_SelectPage_DriverList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
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
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="JobNo" />
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
                    <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" Caption="DateTime" PropertiesDateEdit-DisplayFormatString="yyyy/MM/dd HH:mm" SortOrder="Descending" Width="120"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataColumn FieldName="Platform" Caption="Platform" Width="70"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Controller" Caption="User" Width="70"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ContainerNo" Caption="Container" Width="90"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Trip" Caption="Trip" Width="170"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Note"></dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
