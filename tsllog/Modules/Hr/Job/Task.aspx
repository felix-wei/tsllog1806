<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Task.aspx.cs" Inherits="Modules_Hr_Job_Task" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Task</title>
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
            <wilson:DataSource ID="dsTask" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrTask" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
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
                    <td>
                        From Date
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
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
                DataSourceID="dsTask" Width="900px" KeyFieldName="Id" OnInit="ASPxGridView1_Init" OnRowUpdating="ASPxGridView1_RowUpdating"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
                AutoGenerateColumns="False">
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowFilterRow="false" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="80">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Person" FieldName="Person" VisibleIndex="0" Width="150">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                            TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="Date" VisibleIndex="2" Width="100">
                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesDateEdit>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_Date" runat="server" Value='<%#Bind("Date") %>' Width="100"></dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Time" FieldName="Time" VisibleIndex="3" Width="70">
                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesDateEdit>
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" ReadOnly="true" Border-BorderWidth="0" Text='<%# Bind("Time") %>' Width="60">
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" Text='<%# Bind("Time") %>' Width="60">
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="RefNo" FieldName="RefNo" VisibleIndex="3" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" Width="100" ID="txt_RefNo" Value='<%# Bind("RefNo")%>'>
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="4" Width="60">
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
