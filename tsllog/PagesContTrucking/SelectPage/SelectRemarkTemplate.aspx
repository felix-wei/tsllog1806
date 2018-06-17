<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRemarkTemplate.aspx.cs" Inherits="PagesContTrucking_SelectPage_SelectRemarkTemplate" %>

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
                        <dxe:ASPxTextBox ID="txt_Name" Width="200" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Search"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Save" Width="80" runat="server" Text="New Remark" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e) {
                                                    grid.AddNewRow();
                                                 }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <wilson:DataSource ID="dsJobText" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.JobText" KeyMember="Id" FilterExpression="" />
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" Width="100%" DataSourceID="dsJobText" 
             KeyFieldName="Id"  OnInit="ASPxGridView1_Init" OnRowInserting="ASPxGridView1_RowInserting" 
            OnInitNewRow="ASPxGridView1_InitNewRow" OnRowUpdating="ASPxGridView1_RowUpdating" StylesEditors-Memo-Wrap="True"
             AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing  Mode="Inline"/>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='parent.PutValue("<%# R.Text(Eval("Content")) %>")'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate></EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn VisibleIndex="1" Width="5%">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true"></DeleteButton>
                    </dx:GridViewCommandColumn>
                    <dxwgv:GridViewDataMemoColumn Caption="Content" FieldName="Content" Width="90%"  CellStyle-Wrap="True" VisibleIndex="2" PropertiesMemoEdit-Rows="6">
                        <DataItemTemplate>
                            <div style="word-wrap:break-word; width:730px">
                                <%# Eval("Content") %>
                            </div>
                            
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Content" runat="server" Text='<%# Bind("Content") %>' Rows="6" Width="100%"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataMemoColumn>

                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
