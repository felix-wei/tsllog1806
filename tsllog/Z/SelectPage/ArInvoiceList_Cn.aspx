<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeFile="ArInvoiceList_Cn.aspx.cs" Inherits="Account_ArInvoice_Cn" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Crdit Note</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <wilson:DataSource ID="dsArInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
        <table border="0">
            <tr>
                <td>
                   CN No
                </td>
                <td >
                    <asp:TextBox ID="txt_refNo" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>
                   Date From
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    To
                </td>
                <td
                    <dxe:ASPxDateEdit ID="txt_end"  Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
                            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsArInvoice"
                                ClientInstanceName="grid" Width="100%" KeyFieldName="SequenceId" >
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior AllowFocusedRow="True" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                        <DataItemTemplate>
                        <a onclick='parent.PutCnValue("<%# Eval("SequenceId") %>","<%# Eval("DocNo") %>","<%# Eval("PartyTo") %>","<%# Eval("PartyName") %>","<%# Eval("BalanceAmt") %>","<%# Eval("AcCode") %>");'>Select</a>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Balance Amt" FieldName="BalanceAmt" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
