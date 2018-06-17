<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Transhipment.aspx.cs" Inherits="Modules_Hr_Job_Transhipment" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transhipment</title>
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
            <wilson:DataSource ID="dsTrans" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonTran" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
            <div style="display:none">
                <dxe:ASPxTextBox runat="server" ID="txt_Type" ClientInstanceName="txt_Type" ReadOnly="true" BackColor="Control"
                    Width="100">
                </dxe:ASPxTextBox>
            </div>
            <table>
                <tr>
                    <td>Person
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txtSchName" ClientInstanceName="txtSchName" TextFormatString="{1}"
                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                            DataSourceID="dsPerson" ValueField="Id" Width="150" ValueType="System.Int32">
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
                DataSourceID="dsTrans" Width="800" KeyFieldName="Id" OnInit="ASPxGridView1_Init" OnRowUpdating="ASPxGridView1_RowUpdating"
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
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="80">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="1" Width="100">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_PersonData" TextFormatString="{1}"
                                EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDown" 
                                DropDownButton-Visible="false" Border-BorderWidth="0" ReadOnly="true"
                                DataSourceID="dsPerson" ValueField="Id" Width="100" ValueType="System.Int32" Value='<%# Eval("Person")%>'>
                                <Columns>
                                    <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                    <dxe:ListBoxColumn FieldName="Name" />
                                </Columns>
                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Hours" FieldName="Hrs" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pic" FieldName="Pic" VisibleIndex="4" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="10">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="Amt" VisibleIndex="10" Width="60">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
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
                        <div style="display: none">
                            <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                Width="100" Text='<%# Eval("Id")%>'>
                            </dxe:ASPxTextBox>
                            <dxe:ASPxTextBox runat="server" ID="txt_Type" ClientInstanceName="txt_Type" ReadOnly="true" BackColor="Control"
                                Width="100" Text='<%# Bind("Type")%>'>
                            </dxe:ASPxTextBox>
                        </div>
                        <table border="0">
                            <tr>
                                <td>Name
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Person" TextFormatString="{1}"
                                        EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                        DataSourceID="dsPerson" ValueField="Id" Width="190" ValueType="System.Int32" Value='<%# Eval("Person")%>'>
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Pic
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Trans_Pic" ClientInstanceName="txt_Trans_Pic" Width="190" runat="server"
                                        Text='<%# Bind("Pic")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Status
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_Status" ClientInstanceName="cmb_Status" Width="240" runat="server"
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
                                <td>From
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxDateEdit ID="date_Eta" Width="120" runat="server" Value='<%# Bind("Date1")%>'
                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" Text='<%# Bind("Time1") %>' Width="67">
                                                    <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                    <ValidationSettings ErrorDisplayMode="None" />
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>To
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxDateEdit ID="ASPxDateEdit1" Width="120" runat="server" Value='<%# Bind("Date2")%>'
                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="ASPxTextBox1" runat="server" Text='<%# Bind("Time2") %>' Width="67">
                                                    <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                    <ValidationSettings ErrorDisplayMode="None" />
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>Hrs
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="100">
                                                <dxe:ASPxTextBox ID="txt_Hrs" ClientInstanceName="txt_Trans_Hrs" Width="95" runat="server"
                                                    Text='<%# Bind("Hrs")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td width="40">Amt</td>
                                            <td>
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="100" ID="spin_Amt" Value='<%# Bind("Amt")%>' DecimalPlaces="2">
                                                    <SpinButtons ShowIncrementButtons="false" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Remark：
                                </td>
                                <td colspan="6">
                                    <dxe:ASPxMemo ID="memo_Trans_Remark" Rows="5" runat="server" Width="100%" Value='<%# Bind("Remark") %>'>
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

                            <dxe:ASPxHyperLink ID="btn_UpdateTrans_" runat="server" NavigateUrl="#" Text="Update">
                                <ClientSideEvents Click="function(s,e){ASPxGridView1.UpdateEdit();}" />
                            </dxe:ASPxHyperLink>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
                </Templates>
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
