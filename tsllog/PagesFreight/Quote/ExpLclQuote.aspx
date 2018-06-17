<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ExpLclQuote.aspx.cs"
    Inherits="PagesFreight_ExpLclQuote" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js" ></script>

    <script type="text/javascript">
        function ShowQuo(invN) {
            window.location = 'ExpLclQuoteEdit.aspx?no=' + invN;
        }
    </script>

</head>
<body width="800">
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsQuotation" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaQuote1" KeyMember="SequenceId" />
        <table border="0">
            <tr>
                <td>
                    Quotation No
                </td>
                <td>
                    <asp:TextBox ID="txt_refNo" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>Party To
                </td>
                <td>
                    <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="95"
                        HorizontalAlign="Left" AutoPostBack="False">
                        <Buttons>
                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(txt_CustId,txt_CustName,'CA');
                                }" />
                    </dxe:ASPxButtonEdit>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true"
                        BackColor="Control" Width="250" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Add" Width="140" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                        Text="Add Quotation">
                        <ClientSideEvents Click="function(s, e) {
                                        ShowQuo('0');}" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsQuotation"
            ClientInstanceName="grid" Width="850" KeyFieldName="SequenceId">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='ShowQuo("<%# Eval("QuoteNo") %>");'>Edit</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Quote No" FieldName="QuoteNo" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Title" FieldName="Title" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Pol" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Pod" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ViaPort" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Expire Date" FieldName="ExpireDate" VisibleIndex="4"
                    Width="8%">
                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="LCL Charge" FieldName="LclChg" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        
        
    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
        HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
        Width="800" EnableViewState="False">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
