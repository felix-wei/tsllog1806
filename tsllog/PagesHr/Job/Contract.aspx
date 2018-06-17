<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Contract.aspx.cs" Inherits="PagesHr_Job_Contract" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Contracts</title>
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
            <wilson:DataSource ID="dsContract" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrContract" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
            <wilson:DataSource ID="dsPIC" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Status='Employee'" />
            <table>
                <tr>
                    <td>No</td>
                    <td>
                        <dxe:ASPxTextBox runat="server" ID="txtSchNo" Width="100%"></dxe:ASPxTextBox>
                    </td>
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
                                    <dxe:ASPxButton ID="btn_ADD" Width="100" runat="server" Text="Add New" AutoPostBack="false">
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
                DataSourceID="dsContract" Width="900px" KeyFieldName="Id" OnInit="ASPxGridView1_Init" OnRowUpdating="ASPxGridView1_RowUpdating"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
                OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback1"
                AutoGenerateColumns="False">
                <SettingsEditing Mode="EditForm" />
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
                                        <dxe:ASPxButton ID="btn_contract_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { ASPxGridView1.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_contract_del" runat="server"
                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){ASPxGridView1.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="No" VisibleIndex="1" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="Date" VisibleIndex="2" Width="95">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
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
                    <dxwgv:GridViewDataComboBoxColumn Caption="PIC" FieldName="Pic" VisibleIndex="4" Width="100">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsPIC" Width="100" TextFormatString="{1}" DropDownWidth="105"
                            TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="10" Width="70">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
                <Templates>
                    <EditForm>
                        <table border="0">
                            <tr>
                                <td colspan="6" style="display: none">
                                    <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                        Width="100" Text='<%# Eval("Id")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_No" ClientInstanceName="txt_No" Width="330" runat="server"
                                        Text='<%# Bind("No")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_ContractDay" Width="110" runat="server" Value='<%# Bind("Date")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td width="60">Status
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_Status" ClientInstanceName="cmb_Status" Width="150" runat="server"
                                        Text='<%# Bind("StatusCode")%>'>
                                        <Items>
                                            <dxe:ListEditItem Text="CLOSED" Value="CLOSED" />
                                            <dxe:ListEditItem Text="CANCELED" Value="CANCELED" />
                                            <dxe:ListEditItem Text="USE" Value="USE" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Name
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Person" TextFormatString="{1}"
                                        EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                        DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="330" ValueType="System.Int32" Value='<%# Eval("Person")%>'>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>PIC
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_ContractPic" TextFormatString="{1}"
                                        EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                        DataSourceID="dsPIC" ValueField="Id" TextField="Name" Width="330" ValueType="System.Int32" Value='<%# Bind("Pic")%>'>
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
                            </tr>
                            <tr>
                                <td>Remark：
                                </td>
                                <td>
                                    <dxe:ASPxMemo ID="memo_ContractRemark" Rows="5" runat="server" Width="330" Value='<%# Bind("Remark") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                                <td>Term Remark：
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="memo_ContractTerm" Rows="5" runat="server" Width="330" Value='<%# Bind("Remark1") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Work Remark：
                                </td>
                                <td>
                                    <dxe:ASPxMemo ID="memo_ContractWork" Rows="5" runat="server" Width="330" Value='<%# Bind("Remark2") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                                <td>Salary Remark：
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="memo_ContractSalary" Rows="5" runat="server" Width="330" Value='<%# Bind("Remark3") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <hr>
                                    <table>
                                        <tr>
                                            <td style="width: 80px;">Creation</td>
                                            <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                            <td style="width: 90px;">Last Updated</td>
                                            <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                        </tr>
                                    </table>
                                    <hr>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right; padding: 2px 2px 2px 2px">

                            <dxe:ASPxHyperLink ID="btn_UpdateContract" runat="server" NavigateUrl="#" Text="Update">
                                <ClientSideEvents Click="function(s,e){ASPxGridView1.UpdateEdit();}" />
                            </dxe:ASPxHyperLink>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
                </Templates>
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
