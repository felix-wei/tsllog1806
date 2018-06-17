<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Quotation.aspx.cs" Inherits="Modules_Hr_Job_Quote" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                TypeName="C2.HrQuote" KeyMember="Id" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Status='Employee' or Status='Resignation'" />
                        <wilson:DataSource ID="dsPayItem" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPayItem" KeyMember="Id" />
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl_Employee" runat="server" Text="Employee"></dxe:ASPxLabel>
                        
                    </td>
                    <td style="display: none">
                        <dxe:ASPxTextBox ID="txtSchId" ClientInstanceName="txtSchId" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txtSchName" ClientInstanceName="txtSchName" TextFormatString="{1}"
                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                            DataSourceID="dsPerson" ValueField="Id" Width="100%" ValueType="System.Int32">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                            </Columns>
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
                    <td><dxe:ASPxLabel ID="lbl_Item" runat="server" Text="Payroll Item"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txtPayItem" ClientInstanceName="txtPayItem" TextFormatString="{1}"
                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                            DataSourceID="dsPayItem" ValueField="Code" Width="100%" ValueType="System.String">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Id" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Description" Width="100%" />
                            </Columns>
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
                Width="100%" OnInit="grid_Quote_Init">
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
                    <dxwgv:GridViewDataComboBoxColumn Caption="Employee" FieldName="Person" VisibleIndex="0" Width="150">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                            TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Payroll Item" FieldName="PayItem" VisibleIndex="1" Width="150">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsPayItem" Width="150" TextFormatString="{1}" DropDownWidth="105"
                            TextField="Description" EnableIncrementalFiltering="true" ValueField="Code" DataMember="{1}">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Description" />
                            </Columns>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Amount" FieldName="Amt" UnboundType="String" VisibleIndex="2" Width="75">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="75" ID="spin_Amt" Value='<%# Bind("Amt")%>' DecimalPlaces="2">
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Auto Compute" FieldName="IsCal" VisibleIndex="2" Width="60">
                        <PropertiesComboBox ValueType="System.String"  Width="60" DropDownWidth="105"
                           EnableIncrementalFiltering="true">
                            <Items>
                                <dxe:ListEditItem  Value="YES" Text="YES"/>
                                <dxe:ListEditItem  Value="NO" Text="NO"/>
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataMemoColumn Caption="Remark" FieldName="Remark" VisibleIndex="5" PropertiesMemoEdit-Rows="3">
                    </dxwgv:GridViewDataMemoColumn>
                </Columns>
                <SettingsPager Mode="ShowPager"></SettingsPager>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
            </dxwgv:ASPxGridView>
        </div>
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
