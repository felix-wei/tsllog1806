<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Quotation.aspx.cs" Inherits="PagesHr_Job_Quote" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>

    <script type="text/javascript">
        function OnPostCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        // PayItem
        function PopupItem(codeId, desId) {
            clientId = codeId;
            clientName = desId;
            popubCtr.SetHeaderText('Pay Item');
            popubCtr.SetContentUrl('../SelectPage/PayItemList.aspx');
            popubCtr.Show();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsQuote" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrQuote" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Status='Employee' or Status='Resignation'" />
            <table>
                <tr>
                    <td>Person
                    </td>
                    <td style="display: none">
                        <dxe:ASPxTextBox ID="txtSchId" ClientInstanceName="txtSchId" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txtSchName" ClientInstanceName="txtSchName" TextFormatString="{1}"
                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                            DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="100%" ValueType="System.Int32">
                            <Buttons>
                                <dxe:EditButton Text="Clear"></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            if(e.buttonIndex == 0){
                            s.SetText('');
                                }
                            }" />
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Export" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){
                                            grid_Quote.AddNewRow();
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Quote" runat="server" ClientInstanceName="grid_Quote" DataSourceID="dsQuote"
                KeyFieldName="Id" AutoGenerateColumns="False" OnInitNewRow="grid_Quote_InitNewRow" OnRowInserting="grid_Quote_RowInserting"
                OnRowDeleting="grid_Quote_RowDeleting" OnRowUpdating="grid_Quote_RowUpdating"
                Width="1000" OnInit="grid_Quote_Init">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="10%">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Quote_edit" runat="server" Text="Edit" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { grid_Quote.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Quote_del" runat="server"
                                            Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Quote.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Quote_update" runat="server" Text="Update" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { grid_Quote.UpdateEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Quote_cancel" runat="server" Text="Cancel" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { grid_Quote.CancelEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Person" FieldName="Person" VisibleIndex="0" Width="150">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}"
                            TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PayItem" FieldName="PayItem" VisibleIndex="1" Width="150">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_PayItem" ClientInstanceName="txt_PayItem" runat="server" Width="150" HorizontalAlign="Left"
                                Text='<%# Bind("PayItem")%>' AutoPostBack="False" ReadOnly="true" BackColor="Control">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                    PopupItem(txt_PayItem,null);
                                    }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Amt" FieldName="Amt" UnboundType="String" VisibleIndex="2" Width="75">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="75" ID="spin_Amt" Value='<%# Bind("Amt")%>' DecimalPlaces="2">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsPager Mode="ShowPager"></SettingsPager>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
            </dxwgv:ASPxGridView>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Quote">
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="800" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
