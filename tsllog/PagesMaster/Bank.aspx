<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Bank.aspx.cs" Inherits="MastData_Bank" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bank</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table><tr><td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton></td>
                    <td>
                    <dxe:ASPxButton ID="btn_Export" runat="server" Text="Save Excel" onclick="btn_Export_Click">
                    </dxe:ASPxButton>
                    </td></tr></table>
        <wilson:DataSource ID="dsBank" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXBank" KeyMember="SequenceId" />
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsBank"
            KeyFieldName="SequenceId" AutoGenerateColumns="False" Width="400" OnInitNewRow="ASPxGridView1_InitNewRow"
            oninit="ASPxGridView1_Init" OnRowDeleting="ASPxGridView1_RowDeleting" >
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline"/>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="BankCode" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="BankName" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
        
    </div>
    </form>
</body>
</html>

