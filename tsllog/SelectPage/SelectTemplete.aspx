<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectTemplete.aspx.cs" Inherits="SelectPage_SelectTemplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title> Select Templete</title>
          <script type="text/javascript">
              function $(s) {
                  return document.getElementById(s) ? document.getElementById(s) : s;
              }
              function keydown(e) {
                  if (e.keyCode == 27) { parent.ClosePopupCtr(); }
              }
              document.onkeydown = keydown;

    </script>

    <script type="text/javascript" src="/Script/jquery.js" />
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
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="RefNo" />
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="200" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Content" />
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Content" Width="300" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" Width="50" VisibleIndex="1">
                        <DataItemTemplate>
                            <a onclick='parent.PutContract("<%# Eval("CenterContent") %>");'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn> 
                    <dxwgv:GridViewDataTextColumn Caption="RefNo" FieldName="RefNo" Width="100" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CenterContent" FieldName="CenterContent" VisibleIndex="2" Width="200" SortIndex="0" SortOrder="Ascending">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
