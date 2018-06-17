<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApQuoteList.aspx.cs" Inherits="PagesFreight_SelectPage_ApQuoteList" %>

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
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Quote No" />
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_QuoteNo"  Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve"
                            OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server"
            ClientInstanceName="grid" Width="100%" KeyFieldName="SequenceId">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                         <a onclick='parent.PutQuote("<%# Eval("QuoteNo") %>");'>Select</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Quote No" FieldName="QuoteNo" VisibleIndex="1"
                    Width="30%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="Name" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Expire Date" FieldName="ExpireDate" VisibleIndex="3"
                    Width="8%">
                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
