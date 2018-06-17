<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectLcNo.aspx.cs" Inherits="SelectPage_SelectLcNo" %>

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
                  <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="NO" />
                </td>
                <td>
                    <dxe:ASPxTextBox ID ="txt_Code" Width="100" runat="server">
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
                        <a onclick='parent.PutValue("<%# Eval("LcNo") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="NO" FieldName="LcNo" VisibleIndex="1" SortIndex="0" Width="150" SortOrder="Ascending">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Bank" FieldName="BankName" VisibleIndex="1" SortIndex="0" Width="150" SortOrder="Ascending">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Bene Name" FieldName="LcBeneName" VisibleIndex="4" Width="40">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="LcAmount" VisibleIndex="4" Width="40">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="App Date" FieldName="LcAppDate" VisibleIndex="2" Width="50">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Expirt Date" FieldName="LcExpirtDate" VisibleIndex="2">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
       </Columns>
        </dxwgv:ASPxGridView>      
    </div>
    </form>
</body>
</html>
