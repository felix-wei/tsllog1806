<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CountryList.aspx.cs" Inherits="SelectPage_CountryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                  <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Code" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID ="txt_Code" Width="100" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                  <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Name" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID ="txt_Name" Width="160" runat="server">
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
             KeyFieldName="Code" 
             AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutValue("<%# Eval("Code") %>","<%# Eval("Name") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" SortIndex="0" Width="150" SortOrder="Ascending">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
       </Columns>
        </dxwgv:ASPxGridView>            
    </div>
    </form>
</body>
</html>
