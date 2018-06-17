<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrencyList.aspx.cs" Inherits="SelectPage_CurrencyList" %>

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
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Name" />
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
                    <wilson:DataSource ID="dsCurrencyMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCurrency" KeyMember="CurrencyId"/>
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" ClientInstanceName="grid"
                KeyFieldName="CurrencyId" DataSourceID="dsCurrencyMast"
            OnInit="ASPxGridView1_Init" OnInitNewRow="ASPxGridView1_InitNewRow"
            OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating" AutoGenerateColumns="False"
               >
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td><a onclick='parent.PutValue("<%# Eval("CurrencyId") %>","<%# Eval("CurrencyExRate") %>");'>Select</a> </td>
                                    <td>
                                        <a  onclick='<%# "grid.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a></td>
                                    <td>
                                        <a  onclick='if(confirm("Confirm Delete"))  {<%# "grid.DeleteRow("+Container.VisibleIndex+");"  %>}'>Delete</a> </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <a onclick='parent.PutValue("<%# Eval("CurrencyId") %>","<%# Eval("CurrencyExRate") %>");'>Select</a>
                                    </td>
                                    <td>
                                        <a  onclick='<%# "grid.UpdateEdit(); " %>'>Update</a></td>
                                    <td>
                                        <a  onclick='<%# "grid.CancelEdit();"  %>'>Cancle</a> </td>
                                </tr>
                            </table>     
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="1"  Width="30%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="CurrencyName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="CurrencyExRate" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
