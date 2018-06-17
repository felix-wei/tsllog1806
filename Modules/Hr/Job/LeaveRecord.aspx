<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="LeaveRecord.aspx.cs" Inherits="Modules_Hr_Job_LeaveRecord" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Record</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>

    <script type="text/javascript">
        function OnPostCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function PutDays(txtDate1, txtTime1, txtDate2, txtTime2, txtdays) {
            if (txtDate1 != null && txtTime1 != null && txtDate2 != null && txtTime2 != null) {
                var date1 = new Date();
                var date2 = new Date();
                date1 = txtDate1.GetValue().getTime();
                date2 = txtDate2.GetValue().getTime();
                var diff_millisecond = date2 - date1;
                var days = Math.floor(diff_millisecond / (24 * 3600 * 1000));
                if (txtTime1.GetText() == txtTime2.GetText())
                    days = days + 0.5;
                else if (txtTime1.GetText() == 'AM' && txtTime2.GetText() == 'PM')
                    days = days + 1;
                txtdays.SetNumber(days);

                var total = Number.parseFloat(lbl_TotalDays.GetText());
                var bal = total - days;
                lbl_BalDays.SetText(bal);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsLeaveRecord" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrLeave" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
            <wilson:DataSource ID="dsLeaveType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='LeaveType'" />
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
                    <td>From Date
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
                                    <dxe:ASPxButton ID="btn_Export" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
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
                DataSourceID="dsLeaveRecord" Width="900px" KeyFieldName="Id" OnInit="ASPxGridView1_Init" OnRowUpdating="ASPxGridView1_RowUpdating"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
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
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="1" Visible="false">
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
                    <dxwgv:GridViewDataTextColumn Caption="ApplyDateTime" FieldName="ApplyDateTime" VisibleIndex="2" Width="110">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="From" FieldName="Date1" VisibleIndex="3" Width="110">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxTextBox runat="server" ID="text_Date1" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="110" ReadOnly="true"
                                Value='<%# SafeValue.SafeDateStr(Eval("Date1"))+" "+Eval("Time1") %>' Border-BorderWidth="0">
                            </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="To" FieldName="Date2" VisibleIndex="3" Width="110">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxTextBox runat="server" ID="text_Date2" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="110" ReadOnly="true"
                                Value='<%# SafeValue.SafeDateStr(Eval("Date2"))+" "+Eval("Time2") %>' Border-BorderWidth="0">
                            </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Days" FieldName="Days" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="ApproveStatus" FieldName="ApproveStatus" UnboundType="String" VisibleIndex="5" Width="75">
                    </dxwgv:GridViewDataSpinEditColumn>
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
                <Templates>
                    <EditForm>
                        <table>
                            <tr>
                                <td colspan="6" style="display: none">
                                    <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                        Width="100" Text='<%# Eval("Id")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Name
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Person" TextFormatString="{1}"
                                        EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                        DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="170" ValueType="System.Int32" Value='<%# Bind("Person")%>'>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>ApplyDateTime
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_ApplyDateTime" Width="168" runat="server" Value='<%# Bind("ApplyDateTime") %>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                                        <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td width="110">Type
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cbb_Type" ClientInstanceName="cmb_ApproveStatus"
                                         OnCustomJSProperties="cbb_Type_CustomJSProperties"
                                        DataSourceID="dsLeaveType" TextField="Description" ValueField="Code" Width="100" Value='<%# Bind("LeaveType")%>'>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Total Days
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="lbl_TotalDays" ClientInstanceName="lbl_TotalDays" runat="server"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>From</td>
                                <td>
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="118">
                                                <dxe:ASPxDateEdit runat="server" ID="date_Date1" ClientInstanceName="date_Date1" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="115" Value='<%# Bind("Date1") %>'>
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                PutDays(date_Date1,cmb_Time1,date_Date2,cmb_Time2,spin_Days);
                                             }" />
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="cmb_Time1" ClientInstanceName="cmb_Time1" Width="51" Text='<%#Bind("Time1") %>'>
                                                    <Items>
                                                        <dxe:ListEditItem Text="AM" Value="AM" />
                                                        <dxe:ListEditItem Text="PM" Value="PM" />
                                                    </Items>
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                PutDays(date_Date1,cmb_Time1,date_Date2,cmb_Time2,spin_Days);
                                             }" />
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>To</td>
                                <td>
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="118">
                                                <dxe:ASPxDateEdit runat="server" ID="date_Date2" ClientInstanceName="date_Date2" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="115" Value='<%# Bind("Date2") %>'>
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                PutDays(date_Date1,cmb_Time1,date_Date2,cmb_Time2,spin_Days);
                                             }" />
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="cmb_Time2" ClientInstanceName="cmb_Time2" Width="50" Text='<%#Bind("Time2") %>'>
                                                    <Items>
                                                        <dxe:ListEditItem Text="AM" Value="AM" />
                                                        <dxe:ListEditItem Text="PM" Value="PM" />
                                                    </Items>
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                PutDays(date_Date1,cmb_Time1,date_Date2,cmb_Time2,spin_Days);
                                             }" />
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>Days</td>
                                <td>
                                    <dxe:ASPxSpinEdit runat="server" Width="100" ID="spin_Days" ClientInstanceName="spin_Days" Value='<%# Bind("Days")%>'>
                                        <SpinButtons ShowIncrementButtons="false" />

                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>Bal Days
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="lbl_BalDays" ClientInstanceName="lbl_BalDays" runat="server"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>ApproveBy</td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_ApproveBy" TextFormatString="{1}"
                                        EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                        DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="100" ValueType="System.Int32" Value='<%# Bind("ApproveBy")%>'>
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
                                <td>ApproveDateTime
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxDateEdit ID="date_ApproveDate" Width="115" runat="server" Value='<%# Bind("ApproveDate")%>'
                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_ApproveTime" runat="server" Text='<%# Bind("ApproveTime") %>' Width="50">
                                                    <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                    <ValidationSettings ErrorDisplayMode="None" />
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="110">ApproveStatus
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_ApproveStatus" ClientInstanceName="cmb_ApproveStatus" Text='<%#Bind("ApproveStatus") %>' runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Text="Draft" Value="Draft" />
                                            <dxe:ListEditItem Text="Approve" Value="Approve" />
                                            <dxe:ListEditItem Text="Reject" Value="Reject" />
                                            <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Applicant Remark
                                </td>
                                <td colspan="8">
                                    <dxe:ASPxMemo ID="memo_ApplicantRemark" Rows="3" runat="server" Width="760" Value='<%# Bind("Remark") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Approve Remark
                                </td>
                                <td colspan="8">
                                    <dxe:ASPxMemo ID="memo_ApproveRemark" Rows="3" runat="server" Width="760" Value='<%# Bind("ApproveRemark") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                            <dxe:ASPxHyperLink ID="btn_UpdateLeave" runat="server" NavigateUrl="#" Text="Update">
                                <ClientSideEvents Click="function(s,e){ASPxGridView1.UpdateEdit();}" />
                            </dxe:ASPxHyperLink>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelLeave" ReplacementType="EditFormCancelButton"
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
