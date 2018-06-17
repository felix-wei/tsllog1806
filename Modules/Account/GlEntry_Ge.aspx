<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GlEntry_Ge.aspx.cs" Inherits="Account_GlEntry_Ge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GL Journal Entry</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ShowGlEntry(invN, docType) {
            window.location = 'EditPage/GlEntryEdit_Ge.aspx?no=' + invN+'&type=' + docType;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsGlEntry" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAJournalEntry" KeyMember="SequenceId" FilterExpression="1=0" />
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
          <td>Remark
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txtRemark" ClientInstanceName="txtRemark" Width="110" runat="server"
                            Text="">
                        </dxe:ASPxTextBox>
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
                <td>
                    <dxe:ASPxDateEdit ID="txt_end"  Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Add" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                                    Text="Add">
                                    <ClientSideEvents Click="function(s, e) {
    ShowGlEntry('0');}" />
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
                <dxwgv:GridViewDataTextColumn Caption="JN No" FieldName="DocNo" Width="120" VisibleIndex="1" SortIndex="1" SortOrder="Ascending">
                </dxwgv:GridViewDataTextColumn>                <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="DocType" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="4" >
                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Module" FieldName="ArApInd" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DB Amt" FieldName="CurrencyDbAmt" VisibleIndex="5">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CR Amt" FieldName="CurrencyCrAmt" VisibleIndex="7">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
				    <dxwgv:GridViewDataTextColumn Caption="Post" FieldName="PostInd" VisibleIndex="9"
                    Width="8%">
                    <DataItemTemplate>
                        <%# Eval("PostInd","{0}") == "Y" ? "" : "N" %>
                    </DataItemTemplate>

                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
