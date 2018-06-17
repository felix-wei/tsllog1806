<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartyList.aspx.cs" Inherits="WareHouse_SelectPage_PartyList" %>

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
                    <dxe:ASPxTextBox ID ="txt_Name" Width="200" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve" 
                        onclick="btn_Sch_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"
             KeyFieldName="PartyId" 
             AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutParty("<%# Eval("PartyId") %>","<%# Eval("PartyName") %>","<%# Eval("Contact1") %>","<%# Eval("Tel1") %>","<%# Eval("Country") %>","<%# Eval("City") %>","<%# Eval("ZipCode") %>","<%# Eval("Address") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="PartyName" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
       </Columns>
        </dxwgv:ASPxGridView>            
    </div>
    </form>
</body>
</html>
