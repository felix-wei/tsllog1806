<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GlEntry.aspx.cs" Inherits="Account_GlEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ShowGlEntry(invN, docType) {
            window.location = 'EditPage/GlEntryEdit.aspx?no=' + invN+'&type='+docType;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsGlEntry" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAGlEntry" KeyMember="SequenceId" FilterExpression="1=0" />
        <table>
            <tr>
                <td>
                    Doc No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtDocNo" ClientInstanceName="txtDocNo" Width="110" runat="server"
                        Text="">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    Supplier Bill No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtBillNo" ClientInstanceName="txtBillNo" Width="110" runat="server"
                        Text="">
                    </dxe:ASPxTextBox>
                </td>
                <td style="display:none;">
                    Doc Type
                </td>
                <td style="display:none;">
                    <dxe:ASPxComboBox runat="server" ID="txtSchType" Width="100" >
                        <Items>
                            <dxe:ListEditItem Value="All" Text="All" />
                            <dxe:ListEditItem Value="SIN" Text="SIN" />
                            <dxe:ListEditItem Value="SCN" Text="SCN" />
                            <dxe:ListEditItem Value="SDN" Text="SDN" />
                            <dxe:ListEditItem Value="RVS" Text="RVS" />
                            <dxe:ListEditItem Value="RVG" Text="RVG" />
                            <dxe:ListEditItem Value="PIN" Text="PIN" />
                            <dxe:ListEditItem Value="PCN" Text="PCN" />
                            <dxe:ListEditItem Value="PDN" Text="PDN" />
                            <dxe:ListEditItem Value="PVS" Text="PVS" />
                            <dxe:ListEditItem Value="PVG" Text="PVG" />
                            <dxe:ListEditItem Value="JNE" Text="JNE" />
                            <dxe:ListEditItem Value="IBT" Text="IBT" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>-</td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end"  Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                </dxe:ASPxButton>
                            </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsGlEntry" Width="800" KeyFieldName="SequenceId" 
            AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='ShowGlEntry("<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="2" >
                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="SupplierBillNo" FieldName="SupplierBillNo" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PartyName" FieldName="PartyName" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="CurrencyCrAmt" VisibleIndex="7">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
