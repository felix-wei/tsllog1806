<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ChartOfAccount_bank.aspx.cs" Inherits="SelectPage_ChartOfAccount_bank" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Chart Of Account</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsGlAccChart" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartAcc" KeyMember="SequenceId" FilterExpression="AcBank='Y'"/>
            <wilson:DataSource ID="dsCurrency" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCurrency" KeyMember="CurrencyId"/>
            <wilson:DataSource ID="dsAcGroup" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartGroup" KeyMember="SequenceId" />
            
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" DataSourceID="dsGlAccChart"
            Width="100%" KeyFieldName="SequenceId" AutoGenerateColumns="False" OnInit="ASPxGridView1_Init"
            OnInitNewRow="ASPxGridView1_InitNewRow" OnRowInserting="ASPxGridView1_RowInserting"
            OnRowInserted="ASPxGridView1_RowInserted"  OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="EditForm" />
            <Settings ShowFilterRow="true" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsBehavior AllowFocusedRow="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutValue("<%# Eval("Code") %>","<%# Eval("AcDesc") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="AcDesc" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="AcType" FieldName="AcType" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="AcDorc" FieldName="AcDorc" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Bank" FieldName="AcBank" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="AcCurrency" VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="SubType" FieldName="AcSubType" VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>