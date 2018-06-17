<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartyAcc.aspx.cs" Inherits="PagesMaster_PartyAcc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsCurrency" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCurrency" KeyMember="CurrencyId" />
        <wilson:DataSource ID="dsPartyAcc" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXPartyAcc" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsParty" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="PartyId" />
        <table width="450">
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div>
            <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsPartyAcc"
                KeyFieldName="SequenceId" AutoGenerateColumns="False"
                Width="1000px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" Theme="DevEx" OnRowDeleting="grid_RowDeleting" OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting">
                <SettingsPager Mode="ShowAllRecords" >
            </SettingsPager>
            <SettingsEditing Mode="Inline" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="SequenceId" VisibleIndex="1" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="2" Width="150">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsCurrency" TextField="CurrencyName" EnableIncrementalFiltering="true"
                            ValueField="CurrencyId" />
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Party" FieldName="PartyId" VisibleIndex="3">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsParty" TextField="Name" EnableIncrementalFiltering="true"
                            ValueField="PartyId" />
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ArCode" FieldName="ArCode" VisibleIndex="4" Width="100" />
                    <dxwgv:GridViewDataTextColumn Caption="ApCode" FieldName="ApCode" VisibleIndex="5"  Width="100"/>
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
