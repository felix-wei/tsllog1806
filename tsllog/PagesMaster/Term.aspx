<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Term.aspx.cs" Inherits="MastData_Term" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Term</title>
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
        <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXTerm" KeyMember="SequenceId" />
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsTerm"
            KeyFieldName="SequenceId" AutoGenerateColumns="False" Width="800px" OnInitNewRow="ASPxGridView1_InitNewRow" OnRowInserting="ASPxGridView1_RowInserting"
            oninit="ASPxGridView1_Init" OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating" >
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline"/>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="90">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1" Width="200">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2" Width="200">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Credit Day" FieldName="CreditDay" VisibleIndex="3" Width="100">
                <PropertiesSpinEdit AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" ></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
        
    </div>
    </form>
</body>
</html>

