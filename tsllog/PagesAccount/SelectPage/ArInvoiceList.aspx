<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeFile="ArInvoiceList.aspx.cs" Inherits="Account_ArInvoice" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ar Invoice</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
    <script type="text/javascript">
        function ShowArInvoice(invN) {
            window.location='EditPage/ArInvoiceEdit.aspx?no=' + invN;
        }
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
                    <asp:Label ID="Label3" runat="server" Text="Doc No"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txt_refNo" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Date From"></asp:Label>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
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
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                        <DataItemTemplate>
                        <a onclick='parent.PutInvValue("<%# Eval("DocNo") %>","<%# Eval("DocType") %>","<%# Eval("DocDate","{0:dd/MM/yyyy}") %>","<%# Eval("AcCode") %>","<%# Eval("CurrencyId") %>","<%# Eval("ExRate") %>","<%# Eval("AcSource") %>","<%# Eval("DocAmt") %>","<%# Eval("LocAmt") %>");'>Select</a>
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
                                    <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
