<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrencyList.aspx.cs" Inherits="SelectPage_CurrencyList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                    <dxe:ASPxTextBox ID ="txt_Name" Width="100" runat="server">
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
             KeyFieldName="CurrencyId" 
             AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsBehavior AllowFocusedRow="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutValue("<%# Eval("CurrencyId") %>","<%# Eval("CurrencyExRate") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="1" SortIndex="0" SortOrder="Ascending" Width="30%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="CurrencyExRate" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
       </Columns>
        </dxwgv:ASPxGridView>            
    </div>
    </form>
</body>
</html>
