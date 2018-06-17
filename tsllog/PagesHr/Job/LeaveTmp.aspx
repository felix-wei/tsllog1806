<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="LeaveTmp.aspx.cs" Inherits="PagesHr_Job_LeaveTmp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Leave Template</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>

    <script type="text/javascript">
        function OnPostCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsLeaveTmp" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrLeaveTmp" KeyMember="Id" FilterExpression="1=0" />
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
                    <td>Year
                    </td>
                    <td>
                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="txtSchYear" runat="server">
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
                                    <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){
                                            ASPxGridView1.AddNewRow();
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" OnRowInserting="ASPxGridView1_RowInserting"
                DataSourceID="dsLeaveTmp" Width="900px" KeyFieldName="Id" OnInit="ASPxGridView1_Init" OnRowUpdating="ASPxGridView1_RowUpdating"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnRowDeleting="ASPxGridView1_RowDeleting"
                AutoGenerateColumns="False">
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowFilterRow="false" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="5%" VisibleIndex="0">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_leaveTmp_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { ASPxGridView1.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_leaveTmp_del" runat="server"
                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){ASPxGridView1.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_leaveTmp_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { ASPxGridView1.UpdateEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_leaveTmp_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { ASPxGridView1.CancelEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Year" FieldName="Year" VisibleIndex="1" Width="70">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txtYear" runat="server" Text='<%#Bind("Year") %>' Width="70"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Person" FieldName="Person" VisibleIndex="2" Width="150">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}"
                            TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Days" FieldName="Days" UnboundType="String" VisibleIndex="3" Width="75">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit NumberType="Integer" runat="server" Width="75" ID="spin_Days" Value='<%# Bind("Days")%>'>
                                <SpinButtons ShowIncrementButtons="false" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="LeaveType" VisibleIndex="4" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
            </dxwgv:ASPxGridView>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
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
