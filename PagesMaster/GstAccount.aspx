<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="GstAccount.aspx.cs" Inherits="MastData_GstAccount" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gst Account</title>
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
        <wilson:DataSource ID="dsGstAccount" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXGstAccount" KeyMember="SequenceId" />
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsGstAccount"
            KeyFieldName="SequenceId" AutoGenerateColumns="False" Width="880" OnInitNewRow="ASPxGridView1_InitNewRow"
            oninit="ASPxGridView1_Init" OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating" OnRowDeleting="ASPxGridView1_RowDeleting">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline"/>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
                
                <dxwgv:GridViewDataComboBoxColumn Caption="Source" FieldName="GstSrc" VisibleIndex="1">
                <PropertiesComboBox>
                <Items>
                <dxe:ListEditItem Text="AR" Value="AR" />
                <dxe:ListEditItem Text="AP" Value="AP" />
                </Items>
                </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                
                <dxwgv:GridViewDataTextColumn Caption="AcCode" FieldName="AcCode" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="GstType" FieldName="GstType" VisibleIndex="3">
                <PropertiesComboBox>
                <Items>
                <dxe:ListEditItem Text="S" Value="S"/>
                <dxe:ListEditItem Text="Z" Value="Z" />
                <dxe:ListEditItem Text="E" Value="E"/>
                </Items>
                </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Gst" FieldName="GstValue" VisibleIndex="4">
                <PropertiesSpinEdit AllowMouseWheel="false" DisplayFormatString="0.00" ></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
        
    </div>
    </form>
</body>
</html>

