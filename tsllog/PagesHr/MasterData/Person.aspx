<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="Person.aspx.cs" Inherits="PagesMaster_Person" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script>
        function OnCloseCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                grid.Refresh();
            }
            else if (v == "Fail")
                alert("Action Fail,please try again!");
        }
        // PayItem
        function PopupItem(codeId, desId) {
            clientId = codeId;
            clientName = desId;
            popubCtr.SetHeaderText('Pay Item');
            popubCtr.SetContentUrl('../SelectPage/PayItemList.aspx');
            popubCtr.Show();
        }
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('../Upload.aspx?Sn=' + txt_Id.GetText());
            popubCtr.Show();
        }
        function AfterUploadPhoto() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        function ConfirmStatusAndSave() {
            if (txt_Status.GetText() != cmb_Type.GetText()) {
                if (confirm('Confirm change this ' + txt_Status.GetText() + ' to ' + cmb_Type.GetText() + ' ?')) {
                    grid.PerformCallback('Save');
                    grid.Refresh();
                }
                else { cmb_Type.SetText(txt_Status.GetText()); }
            }
            else { grid.PerformCallback('Save'); }
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
            }
        }
        function PrintEmploymentProfile(id) {
            window.open('/ReportFreightSea/printview.aspx?document=Hr_EmploymentProfile&master=&oid=' + id);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" />
            <wilson:DataSource ID="dsSchPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" />
            <wilson:DataSource ID="dsPIC" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Status='EMPLOYEE'" />
            <wilson:DataSource ID="dsPersonDet" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonDet1" KeyMember="Id" />
            <wilson:DataSource ID="dsPersonDet2" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonDet2" KeyMember="Id" />
            <wilson:DataSource ID="dsPersonDet3" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonDet3" KeyMember="Id" />
            <wilson:DataSource ID="dsPersonDet4" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonDet4" KeyMember="Id" />
            <wilson:DataSource ID="dsPersonDet5" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonDet5" KeyMember="Id" />
            <wilson:DataSource ID="dsPersonDet6" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonDet6" KeyMember="Id" />
            <wilson:DataSource ID="dsContract" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrContract" KeyMember="Id" />
            <wilson:DataSource ID="dsComment" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonComment" KeyMember="Id" />
            <wilson:DataSource ID="dsTrans" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonTran" KeyMember="Id" />
            <wilson:DataSource ID="dsLeave" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrLeave" KeyMember="Id" />
            <wilson:DataSource ID="dsLeaveTmp" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrLeaveTmp" KeyMember="Id" />
            <wilson:DataSource ID="dsPersonLog" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonLog" KeyMember="Id" />
            <wilson:DataSource ID="dsQuote" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrQuote" KeyMember="Id" />
            <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrAttachment" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCountry" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXCountry" KeyMember="Code" />
            <wilson:DataSource ID="dsDepartment" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='Department'" />
            <wilson:DataSource ID="dsRole" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='Role'" />
            <wilson:DataSource ID="dsHrGroup" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='HrGroup'" />
            <wilson:DataSource ID="dsCPF" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrCpf" KeyMember="Id" />
            <table width="450">
                <tr>
                    <td>Name
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_SchName" ClientInstanceName="txt_SchName" runat="server"
                            Width="200" DropDownStyle="DropDownList"
                            DataSourceID="dsSchPerson" ValueField="Id" TextField="Name" ValueType="System.String" TextFormatString="{1}"
                            EnableCallbackMode="false" EnableIncrementalFiltering="true" IncrementalFilteringMode="StartsWith"
                            CallbackPageSize="100">
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
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='Person.aspx?name='+txt_SchName.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Export" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div style="display: none">
                <dxe:ASPxTextBox ID="txt_Status" ClientInstanceName="txt_Status" runat="server" ReadOnly="true"
                    BackColor="Control" Text="" Width="150">
                </dxe:ASPxTextBox>
            </div>
            <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsPerson"
                KeyFieldName="Id" AutoGenerateColumns="False" OnLoad="grid_Load"
                Width="1000px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnCustomDataCallback="grid_CustomDataCallback" OnCustomCallback="grid_CustomCallback" OnHtmlEditFormCreated="grid_HtmlEditFormCreated">
                <SettingsEditing Mode="EditForm" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowFilterRow="false" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Gender" FieldName="Gender" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Department" FieldName="Department" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Role" FieldName="HrRole" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Telephone" FieldName="Telephone" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="BirthDay" Caption="BirthDay" VisibleIndex="5" Width="100">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxTextBox runat="server" ID="text_BirthDay" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                Value='<%# Eval("BirthDay").ToString() =="1/1/0001 12:00:00 AM" ? "" : DataBinder.Eval(Container.DataItem,"BirthDay","{0:dd/MM/yyyy}") %>' Border-BorderWidth="0">
                            </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
                <Templates>
                    <EditForm>
                        <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                            <tr>
                                <td width="90%"></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                        Enabled='<%# (SafeValue.SafeString(Eval("Status"),"USE")!="InActive")%>'>
                                        <ClientSideEvents Click="function(s,e) {
                                            ConfirmStatusAndSave();
                                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Cancel" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid.PerformCallback('Cancle');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                BackColor="Control" Text='<%# Eval("Id") %>' Width="150">
                            </dxe:ASPxTextBox>
                            <dxe:ASPxTextBox ID="txt_InterviewId" ClientInstanceName="txt_InterviewId" runat="server" ReadOnly="true"
                                BackColor="Control" Text='<%# Bind("InterviewId") %>' Width="150">
                            </dxe:ASPxTextBox>
                            <dxe:ASPxTextBox ID="txt_RecruitId" ClientInstanceName="txt_RecruitId" runat="server" ReadOnly="true"
                                BackColor="Control" Text='<%# Bind("RecruitId") %>' Width="150">
                            </dxe:ASPxTextBox>
                        </div>
                        <table style="border: solid 1px black; padding: 2px 2px 2px 2px;" width="100%">
                            <tr>
                                <td style="background-color: Gray; color: White;width:120px;">
                                    <b>Print Documents:</b>
                                </td>
                                <td>
                                    <a href="#" onclick='PrintEmploymentProfile("<%# Eval("Id")%>")'>EmploymentProfile</a>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>Name：
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Name" ClientInstanceName="txt_Name" runat="server" Width="150" Value='<%# Eval("Name") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Type</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_Type" ClientInstanceName="cmb_Type" runat="server" ClientEnabled='<%#SafeValue.SafeInt(Eval("Id"),0)>0 %>' Value='<%# Eval("Status")%>' Width="120">
                                        <Items>
                                            <dxe:ListEditItem Text="EMPLOYEE" Value="EMPLOYEE" />
                                            <dxe:ListEditItem Text="CANDIDATE" Value="CANDIDATE" />
                                            <dxe:ListEditItem Text="RESIGNATION" Value="RESIGNATION" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                        <dxtc:ASPxPageControl ID="pageControl" runat="server" Width="1024" Height="500px" EnableCallBacks="true" BackColor="#F0F0F0" ActiveTabIndex="0">
                            <TabPages>
                                <dxtc:TabPage Text="Basic Info">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <table border="0">
                                                    <tr>
                                                        <td>Gender</td>
                                                        <td width="184">
                                                            <dxe:ASPxComboBox ID="cbo_Gender" runat="server" Value='<%# Eval("Gender")%>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Male" Value="Male" />
                                                                    <dxe:ListEditItem Text="Female " Value="Female" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Ic No</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtIcNo" runat="server" EnableIncrementalFiltering="True" Value='<%# Eval("IcNo") %>' Width="150">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Birthday</td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Birthday" Width="150" runat="server" Value='<%# Eval("BirthDay")%>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Married</td>
                                                        <td>
                                                            <dxe:ASPxCheckBox ID="ckb_Married" Checked='<%# SafeValue.SafeString(Eval("Married"),"N")=="Y" %>' ClientInstanceName="ckb_Married" runat="server" TextAlign="Left">
                                                            </dxe:ASPxCheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td>Country</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Country" ClientInstanceName="txt_Country" runat="server"
                                                                Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Country") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                      PopupCountry(null,txt_Country)
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Race
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Race" runat="server" Width="150" Value='<%# Eval("Race") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Religion
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Religion" runat="server" Width="150" Value='<%# Eval("Religion") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Telephone
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Telephone" runat="server" Width="150" Value='<%# Eval("Telephone") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Email
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Email" runat="server" Width="150" Value='<%# Eval("Email") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Address
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Address" Rows="3" runat="server" Width="375" Value='<%# Eval("Address") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Remark" Rows="3" runat="server" Width="370" Value='<%# Eval("Remark") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Profile Remark
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Profile" Rows="3" runat="server" Width="375" Value='<%# Eval("Remark1") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Work Remark
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Work" Rows="3" runat="server" Width="370" Value='<%# Eval("Remark2") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="125">Education Remark
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Education" Rows="3" runat="server" Width="375" Value='<%# Eval("Remark3") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td width="105">Family Remark
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Family" Rows="3" runat="server" Width="370" Value='<%# Eval("Remark4") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Emergency Contact Remark
                                                        </td>
                                                        <td colspan="7">
                                                            <dxe:ASPxMemo ID="memo_Emergency" Rows="2" runat="server" Width="858" Value='<%# Eval("Remark5") %>'>
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
                                                                    <td style="width: 80px;">Status</td>
                                                                    <td style="width: 160px">
                                                                        <dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text='<%#Eval("StatusCode") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel runat="server" ID="divPersonDet">
                                                    <dxe:ASPxButton ID="ASPxButton2" Width="175" runat="server" Text="Add Line"
                                                        Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s,e) {
                                                        gridPersonDet.AddNewRow();
                                                        }" />
                                                    </dxe:ASPxButton>
                                                    <dxwgv:ASPxGridView ID="gridPersonDet" ClientInstanceName="gridPersonDet" runat="server"
                                                        KeyFieldName="Id" DataSourceID="dsPersonDet" Width="100%" OnBeforePerformDataSelect="gridPersonDet_BeforePerformDataSelect"
                                                        OnRowUpdating="gridPersonDet_RowUpdating" OnRowInserting="gridPersonDet_RowInserting"
                                                        OnRowDeleting="gridPersonDet_RowDeleting" OnInitNewRow="gridPersonDet_InitNewRow"
                                                        OnInit="gridPersonDet_Init">
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PersonDet_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { gridPersonDet.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PersonDet_del" runat="server"
                                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gridPersonDet.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PersonDet_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { gridPersonDet.UpdateEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_PersonDet_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { gridPersonDet.CancelEdit() }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="PersonId" FieldName="Person" VisibleIndex="1" Visible="false">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="BeginDate" FieldName="BeginDate" VisibleIndex="2" Width="80">
                                                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dxe:ASPxTextBox runat="server" ID="text_BeginDate" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                                                        Value='<%# SafeValue.SafeDateStr(Eval("BeginDate")) %>' Border-BorderWidth="0">
                                                                    </dxe:ASPxTextBox>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit runat="server" ID="date_BeginDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100" Value='<%# Bind("BeginDate") %>'>
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="ResignDate" FieldName="ResignDate" VisibleIndex="3" Width="80">
                                                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dxe:ASPxTextBox runat="server" ID="text_ResignDate" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                                                        Value='<%# SafeValue.SafeDateStr(Eval("ResignDate")) %>' Border-BorderWidth="0">
                                                                    </dxe:ASPxTextBox>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxDateEdit runat="server" ID="date_ResignDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100" Value='<%# Bind("ResignDate") %>'>
                                                                    </dxe:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="4">
                                                                <EditItemTemplate>
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" Text='<%#Bind("Remark") %>' ID="Remark"></dxe:ASPxTextBox>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="InLine" />
                                                    </dxwgv:ASPxGridView>
                                                </asp:Panel>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Other Info">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <table border="0">
                                                    <tr>
                                                        <td>ProbationFromDate
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_ProbationFromDate" Width="150" runat="server" Value='<%# Eval("ProbationFromDate")%>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>To
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_ProbationToDate" Width="150" runat="server" Value='<%# Eval("ProbationToDate")%>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>HoursPerDay</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_HoursPerDay" runat="server" Value='<%# Eval("HoursPerDay")%>' Width="150" SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>HoursPerWeek</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_HoursPerWeek" runat="server" Value='<%# Eval("HoursPerWeek")%>' Width="120" SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>HoursPerMonth</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_HoursPerMonth" runat="server" Value='<%# Eval("HoursPerMonth")%>' Width="150" SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>DaysPerWeek</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_DaysPerWeek" runat="server" Value='<%# Eval("DaysPerWeek")%>' Width="150" SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>DaysPerMonth</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_DaysPerMonth" runat="server" Value='<%# Eval("DaysPerMonth")%>' Width="150" SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>HrGroup
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_HrGroup"
                                                                DataSourceID="dsHrGroup" TextField="Code" ValueField="Code" Width="120" Value='<%# Eval("HrGroup")%>'>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                            <tr>
                                                        <td>EmploymentType
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_EmploymentType" runat="server" Text='<%#Eval("EmploymentType") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="FullTime" Value="FullTime" />
                                                                    <dxe:ListEditItem Text="PartTime" Value="PartTime" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>SalaryPaymode
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_SalaryPaymode" runat="server" Text='<%#Eval("SalaryPaymode") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Monthly" Value="Monthly" />
                                                                    <dxe:ListEditItem Text="Weekly" Value="Weekly" />
                                                                    <dxe:ListEditItem Text="Daily" Value="Daily" />
                                                                    <dxe:ListEditItem Text="Hourly" Value="Hourly" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                            <td>Department
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Department"
                                                                    DataSourceID="dsDepartment" TextField="Code" ValueField="Code" Width="150" Value='<%# Eval("Department")%>'>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td>Role
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Role"
                                                                    DataSourceID="dsRole" TextField="Code" ValueField="Code" Width="120" Value='<%# Eval("HrRole")%>'>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td>PassType</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_PassType" runat="server" Text='<%#Eval("PassType") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="EP" Value="EP" />
                                                                    <dxe:ListEditItem Text="WP" Value="WP" />
                                                                    <dxe:ListEditItem Text="SP" Value="SP" />
                                                                    <dxe:ListEditItem Text="DP" Value="DP" />
                                                                    <dxe:ListEditItem Text="VISA" Value="VISA" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>PassNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_PassNo" runat="server" Width="150" Value='<%# Eval("PassNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Identity</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_PRInd" runat="server" Value='<%#Eval("PRInd") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="SINGAPORE" Value="SINGAPORE" />
                                                                    <dxe:ListEditItem Text="PR" Value="PR" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>PR Year</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit NumberType="Integer" runat="server" Width="120" ID="PR_Year" ClientInstanceName="PR_Year" Value='<%# Bind("PRYear")%>' Increment="0">
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Dialect
                                                        </td>
                                                        <td colspan="7">
                                                            <dxe:ASPxTextBox ID="txt_Dialect" runat="server" Width="875" Value='<%# Eval("Dialect") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>LanguageWritten
                                                        </td>
                                                        <td colspan="7">
                                                            <dxe:ASPxTextBox ID="txt_LanguageWrittenRemark" runat="server" Width="875" Value='<%# Eval("LanguageWrittenRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>LanguageSpoken
                                                        </td>
                                                        <td colspan="7">
                                                            <dxe:ASPxTextBox ID="txt_LanguageSpokenRemark" runat="server" Width="875" Value='<%# Eval("LanguageSpokenRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>NationalService
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_NationalServiceStatus" runat="server" Text='<%#Eval("NationalServiceStatus") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                                    <dxe:ListEditItem Text="Exempted" Value="Exempted" />
                                                                    <dxe:ListEditItem Text="NotAffected" Value="NotAffected" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="txt_NationalServiceRemark" runat="server" Width="620" Value='<%# Eval("NationalServiceRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>EmploySource
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_EmploySource" runat="server" Text='<%#Eval("EmploySource") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Advertisement" Value="Advertisement" />
                                                                    <dxe:ListEditItem Text="Friend" Value="Friend" />
                                                                    <dxe:ListEditItem Text="NotAffected" Value="NotAffected" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="cmb_EmploySourceRemark" runat="server" Width="620" Value='<%# Eval("EmploySourceRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>DrivingType
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_DrivingType" runat="server" Text='<%#Eval("DrivingType") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                    <dxe:ListEditItem Text="Class3" Value="Class3" />
                                                                    <dxe:ListEditItem Text="Class4" Value="Class4" />
                                                                    <dxe:ListEditItem Text="Class5" Value="Class5" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="txt_DrivingRemark" runat="server" Width="620" Value='<%# Eval("DrivingRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>HealthStatus
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_HealthStatus" runat="server" Text='<%#Eval("HealthStatus") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Normal" Value="Normal" />
                                                                    <dxe:ListEditItem Text="Others" Value="Others" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="txt_HealthRemark" runat="server" Width="620" Value='<%# Eval("HealthRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>OverTimeStatus
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_OverTimeStatus" runat="server" Text='<%#Eval("OverTimeStatus") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="txt_OverTimeRemark" runat="server" Width="620" Value='<%# Eval("OverTimeRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>RelativeWorking
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_RelativeWorkingStatus" runat="server" Text='<%#Eval("RelativeWorkingStatus") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="txt_RelativeWorkingRemark" runat="server" Width="620" Value='<%# Eval("RelativeWorkingRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>TerminateStatus
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_TerminateStatus" runat="server" Text='<%#Eval("TerminateStatus") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="txt_TerminateRemark" runat="server" Width="620" Value='<%# Eval("TerminateRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>CheckReference
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cmb_CheckReferenceStatus" runat="server" Text='<%#Eval("CheckReferenceStatus") %>' Width="150">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Yes" Value="Yes" />
                                                                    <dxe:ListEditItem Text="No" Value="No" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Remark
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxTextBox ID="txt_CheckReferenceRemark" runat="server" Width="620" Value='<%# Eval("CheckReferenceRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                    <dxe:ASPxButton ID="ASPxButton11" Width="175" runat="server" Text="Add Bank Account"
                                                        Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s,e) {
                                                        gridPersonDet3.AddNewRow();
                                                        }" />
                                                    </dxe:ASPxButton>
                                                    <dxwgv:ASPxGridView ID="gridPersonDet3" runat="server" ClientInstanceName="gridPersonDet3" DataSourceID="dsPersonDet3"
                                                        KeyFieldName="Id" AutoGenerateColumns="False" OnInit="gridPersonDet3_Init" OnBeforePerformDataSelect="gridPersonDet3_BeforePerformDataSelect"
                                                        OnInitNewRow="gridPersonDet3_InitNewRow" OnRowInserting="gridPersonDet3_RowInserting" OnRowUpdating="gridPersonDet3_RowUpdating"
                                                        OnRowDeleting="gridPersonDet3_RowDeleting" OnHtmlEditFormCreated="gridPersonDet3_HtmlEditFormCreated" Width="1000px">
                                                        <SettingsPager Mode="ShowAllRecords">
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="EditForm" />
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="10%">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_BankAcc_edit" runat="server" Text="Edit" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { gridPersonDet3.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_BankAcc_del" runat="server"
                                                                                    Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gridPersonDet3.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="BankCode" FieldName="BankCode" VisibleIndex="1" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="BankName" FieldName="BankName" VisibleIndex="2" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="BranchCode" FieldName="BranchCode" VisibleIndex="3" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Acc No" FieldName="AccNo" VisibleIndex="4" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="IsPayroll" FieldName="IsPayroll" VisibleIndex="5" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Templates>
                                                            <EditForm>
                                                                <div style="display: none">
                                                                </div>
                                                                <table>
                                                                    <tr>
                                                                        <td>Bank Code
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_BankCode" ClientInstanceName="txt_BankCode" Width="150" runat="server"
                                                                                Text='<%# Bind("BankCode")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Bank Name
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_BankName" ClientInstanceName="txt_BankName" Width="430" runat="server"
                                                                                Text='<%# Bind("BankName")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>IsPayroll
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxCheckBox ID="ckb_IsPayroll" Checked='<%# SafeValue.SafeBool(Eval("IsPayroll"),false)==true %>' ClientInstanceName="ckb_IsPayroll" runat="server" TextAlign="Left">
                                                                            </dxe:ASPxCheckBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Branch Code
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_BranchCode" ClientInstanceName="txt_BranchCode" Width="150" runat="server"
                                                                                Text='<%# Bind("BranchCode")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Account No 
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_AccNo" ClientInstanceName="txt_AccNo" Width="430" runat="server"
                                                                                Text='<%# Bind("AccNo")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Swift Code
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_SwiftCode" ClientInstanceName="txt_SwiftCode" Width="150" runat="server"
                                                                                Text='<%# Bind("SwiftCode")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Remark
                                                                        </td>
                                                                        <td colspan="7">
                                                                            <dxe:ASPxMemo ID="memo_ContactRemark" Rows="4" runat="server" Width="885" Value='<%# Bind("Remark") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                                    <dxe:ASPxHyperLink ID="btn_UpdateBankAcc" runat="server" NavigateUrl="#" Text="Update">
                                                                        <ClientSideEvents Click="function(s,e){gridPersonDet3.UpdateEdit();}" />
                                                                    </dxe:ASPxHyperLink>
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelBankAcc" ReplacementType="EditFormCancelButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                </div>
                                                            </EditForm>
                                                        </Templates>
                                                        <SettingsPager Mode="ShowPager"></SettingsPager>
                                                        <Styles Header-HorizontalAlign="Center">
                                                            <Header HorizontalAlign="Center"></Header>
                                                            <Cell HorizontalAlign="Center"></Cell>
                                                        </Styles>
                                                    </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Education">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <dxe:ASPxButton ID="ASPxButton13" Width="175" runat="server" Text="Add Education History"
                                                    Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        gridPersonDet4.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="gridPersonDet4" runat="server" ClientInstanceName="gridPersonDet4" DataSourceID="dsPersonDet4"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInit="gridPersonDet4_Init" OnBeforePerformDataSelect="gridPersonDet4_BeforePerformDataSelect"
                                                    OnInitNewRow="gridPersonDet4_InitNewRow" OnRowInserting="gridPersonDet4_RowInserting" OnRowUpdating="gridPersonDet4_RowUpdating"
                                                    OnRowDeleting="gridPersonDet4_RowDeleting" Width="1000px">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_EduHistory_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gridPersonDet4.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_EduHistory_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gridPersonDet4.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_EduHistory_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gridPersonDet4.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_EduHistory_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gridPersonDet4.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="SchoolName" FieldName="SchoolName" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="DateFrom" FieldName="DateFrom" VisibleIndex="2" Width="100">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                            <DataItemTemplate>
                                                                <dxe:ASPxTextBox runat="server" ID="EduHistory_DateFrom" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                                                    Value='<%# SafeValue.SafeDateStr(Eval("DateFrom")) %>' Border-BorderWidth="0">
                                                                </dxe:ASPxTextBox>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxDateEdit runat="server" ID="EduHistory_DateFrom" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100" Value='<%# Bind("DateFrom") %>'>
                                                                </dxe:ASPxDateEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="DateTo" FieldName="DateTo" VisibleIndex="3" Width="100">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                            <DataItemTemplate>
                                                                <dxe:ASPxTextBox runat="server" ID="text_DateTo" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                                                    Value='<%# SafeValue.SafeDateStr(Eval("DateTo")) %>' Border-BorderWidth="0">
                                                                </dxe:ASPxTextBox>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxDateEdit runat="server" ID="EduHistory_DateTo" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100" Value='<%# Bind("DateTo") %>'>
                                                                </dxe:ASPxDateEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="SchoolYear" FieldName="SchoolYear" VisibleIndex="4" Width="40">
                                                            <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="HighestLevel" FieldName="HighestLevel" VisibleIndex="5" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="Status" VisibleIndex="6" Width="105">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Passed" Value="Passed" />
                                                                    <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                                    <dxe:ListEditItem Text="Terminate" Value="Terminate" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Employment">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <dxe:ASPxButton ID="ASPxButton14" Width="175" runat="server" Text="Add Employment History"
                                                    Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        gridPersonDet5.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="gridPersonDet5" runat="server" ClientInstanceName="gridPersonDet5" DataSourceID="dsPersonDet5"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInit="gridPersonDet5_Init" OnBeforePerformDataSelect="gridPersonDet5_BeforePerformDataSelect"
                                                    OnInitNewRow="gridPersonDet5_InitNewRow" OnRowInserting="gridPersonDet5_RowInserting" OnRowUpdating="gridPersonDet5_RowUpdating"
                                                    OnRowDeleting="gridPersonDet5_RowDeleting" Width="1000px">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Employment_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gridPersonDet5.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Employment_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gridPersonDet5.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="EmployerName" FieldName="EmployerName" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PositionHold" FieldName="PositionHold" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="DateFrom" FieldName="DateFrom" VisibleIndex="3" Width="100">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                            <DataItemTemplate>
                                                                <dxe:ASPxTextBox runat="server" ID="Employment_DateFrom" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                                                    Value='<%# SafeValue.SafeDateStr(Eval("DateFrom")) %>' Border-BorderWidth="0">
                                                                </dxe:ASPxTextBox>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxDateEdit runat="server" ID="Employment_DateFrom" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100" Value='<%# Bind("DateFrom") %>'>
                                                                </dxe:ASPxDateEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="DateTo" FieldName="DateTo" VisibleIndex="4" Width="100">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                            <DataItemTemplate>
                                                                <dxe:ASPxTextBox runat="server" ID="txtEmployment_DateTo" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                                                    Value='<%# SafeValue.SafeDateStr(Eval("DateTo")) %>' Border-BorderWidth="0">
                                                                </dxe:ASPxTextBox>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <dxe:ASPxDateEdit runat="server" ID="Employment_DateTo" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100" Value='<%# Bind("DateTo") %>'>
                                                                </dxe:ASPxDateEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Salary" FieldName="Salary" VisibleIndex="5" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="LeaveStatus" FieldName="LeaveStatus" VisibleIndex="6" Width="90">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Passed" Value="Passed" />
                                                                    <dxe:ListEditItem Text="Completed" Value="Completed" />
                                                                    <dxe:ListEditItem Text="Terminate" Value="Terminate" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                        <Templates>
                                                            <EditForm>
                                                                <table>
                                                                    <tr>
                                                                        <td>EmployerName</td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_EmployerName" ClientInstanceName="txt_EmployerName" Width="170" runat="server"
                                                                                Text='<%# Bind("EmployerName")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>PositionHold</td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_PositionHold" ClientInstanceName="txt_PositionHold" Width="170" runat="server"
                                                                                Text='<%# Bind("PositionHold")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>DateFrom</td>
                                                                        <td>
                                                                            <dxe:ASPxDateEdit ID="Employment_DateFrom" Width="150" runat="server" Value='<%# Bind("DateFrom")%>'
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td>DateTo</td>
                                                                        <td>
                                                                            <dxe:ASPxDateEdit ID="Employment_DateTo" Width="150" runat="server" Value='<%# Bind("DateTo")%>'
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Salary</td>
                                                                        <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="170" ID="spin_Salary" Value='<%# Bind("Salary")%>' DecimalPlaces="2">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>Allowance</td>
                                                                        <td colspan="5">
                                                                            <dxe:ASPxTextBox ID="txt_Allowance" ClientInstanceName="txt_Allowance" Width="100%" runat="server"
                                                                                Text='<%# Bind("Allowance")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>LeaveStatus</td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="cmb_LeaveStatus" runat="server" Width="170" Text='<%#Bind("LeaveStatus") %>'>
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Resign" Value="Resign" />
                                                                                    <dxe:ListEditItem Text="Close" Value="Close" />
                                                                                    <dxe:ListEditItem Text="Terminate" Value="Terminate" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td>ReasonForLeaving
                                                                        </td>
                                                                        <td colspan="5">
                                                                            <dxe:ASPxTextBox ID="txt_ReasonForLeaving" ClientInstanceName="txt_ReasonForLeaving" Width="100%" runat="server"
                                                                                Text='<%# Bind("ReasonForLeaving")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Remark：
                                                                        </td>
                                                                        <td colspan="10">
                                                                            <dxe:ASPxMemo ID="memo_Trans_Remark" Rows="4" runat="server" Width="100%" Value='<%# Bind("Remark") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                    <dxe:ASPxHyperLink ID="btn_UpdateContact" runat="server" NavigateUrl="#" Text="Update">
                                                                        <ClientSideEvents Click="function(s,e){gridPersonDet5.UpdateEdit();}" />
                                                                    </dxe:ASPxHyperLink>
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelContact" ReplacementType="EditFormCancelButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                </div>
                                                            </EditForm>
                                                        </Templates>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Family & Contact">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <dxe:ASPxButton ID="ASPxButton15" Width="175" runat="server" Text="Add Family Member"
                                                    Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        gridPersonDet6.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="gridPersonDet6" runat="server" ClientInstanceName="gridPersonDet6" DataSourceID="dsPersonDet6"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInit="gridPersonDet6_Init" OnBeforePerformDataSelect="gridPersonDet6_BeforePerformDataSelect"
                                                    OnInitNewRow="gridPersonDet6_InitNewRow" OnRowInserting="gridPersonDet6_RowInserting" OnRowUpdating="gridPersonDet6_RowUpdating"
                                                    OnRowDeleting="gridPersonDet6_RowDeleting" Width="1000px">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_FamilyMember_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gridPersonDet6.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_FamilyMember_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gridPersonDet6.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_FamilyMember_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gridPersonDet6.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_FamilyMember_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gridPersonDet6.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1" Width="120">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Relationship" FieldName="Relationship" VisibleIndex="2" Width="90">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Age" FieldName="Age" VisibleIndex="3" Width="40">
                                                            <PropertiesSpinEdit NumberType="Integer" Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Occupation" FieldName="Occupation" VisibleIndex="4" Width="90">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                                    <dxe:ASPxButton ID="ASPxButton9" Width="175" runat="server" Text="Add Contact"
                                                        Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s,e) {
                                                        gridPersonDet2.AddNewRow();
                                                        }" />
                                                    </dxe:ASPxButton>
                                                    <dxwgv:ASPxGridView ID="gridPersonDet2" runat="server" ClientInstanceName="gridPersonDet2" DataSourceID="dsPersonDet2"
                                                        KeyFieldName="Id" AutoGenerateColumns="False" OnInit="gridPersonDet2_Init" OnBeforePerformDataSelect="gridPersonDet2_BeforePerformDataSelect"
                                                        OnInitNewRow="gridPersonDet2_InitNewRow" OnRowInserting="gridPersonDet2_RowInserting" OnRowUpdating="gridPersonDet2_RowUpdating"
                                                        OnRowDeleting="gridPersonDet2_RowDeleting" Width="1000px">
                                                        <SettingsPager Mode="ShowAllRecords">
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="EditForm" />
                                                        <SettingsCustomizationWindow Enabled="True" />
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="10%">
                                                                <DataItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_Contact_edit" runat="server" Text="Edit" AutoPostBack="false"
                                                                                    ClientSideEvents-Click='<%# "function(s) { gridPersonDet2.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxButton ID="btn_Contact_del" runat="server"
                                                                                    Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gridPersonDet2.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                </dxe:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="ContactName" VisibleIndex="1" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Gender" FieldName="Gender" VisibleIndex="2" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="DOB" FieldName="Dob" VisibleIndex="3" Width="80">
                                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dxe:ASPxTextBox runat="server" ID="text_BirthDay" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="100" ReadOnly="true"
                                                                        Value='<%# Eval("Dob").ToString() =="1/1/0001 12:00:00 AM" ? "" : DataBinder.Eval(Container.DataItem,"Dob","{0:dd/MM/yyyy}") %>' Border-BorderWidth="0">
                                                                    </dxe:ASPxTextBox>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Telephone" FieldName="Phone" VisibleIndex="4" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Relationship" FieldName="Relationship" VisibleIndex="6" Width="80">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Templates>
                                                            <EditForm>
                                                                <div style="display: none">
                                                                </div>
                                                                <table>
                                                                    <tr>
                                                                        <td>Name
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_ContactName" ClientInstanceName="txt_ContactName" Width="150" runat="server"
                                                                                Text='<%# Bind("ContactName")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Gender:</td>
                                                                        <td width="157">
                                                                            <dxe:ASPxComboBox ID="cbo_ContactGender" runat="server" Value='<%# Bind("Gender")%>' Width="150">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Male" Value="Male" />
                                                                                    <dxe:ListEditItem Text="Female " Value="Female" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td>DOB
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxDateEdit ID="txt_Dob" Width="150" runat="server" Value='<%# Bind("Dob")%>'
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td>Relationship 
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_Relationship" ClientInstanceName="txt_Relationship" Width="150" runat="server"
                                                                                Text='<%# Bind("Relationship")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Telephone
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_ContactPhone" ClientInstanceName="txt_ContactPhone" Width="150" runat="server"
                                                                                Text='<%# Bind("Phone")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Mobile
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_ContactMobile" ClientInstanceName="txt_ContactMobile" Width="150" runat="server"
                                                                                Text='<%# Bind("Mobile")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>Email
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txt_ContactEmail" ClientInstanceName="txt_ContactEmail" Width="150" runat="server"
                                                                                Text='<%# Bind("Email")%>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Remark
                                                                        </td>
                                                                        <td colspan="7">
                                                                            <dxe:ASPxMemo ID="memo_ContactRemark" Rows="4" runat="server" Width="900" Value='<%# Bind("Remark") %>'>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                                    <dxe:ASPxHyperLink ID="btn_UpdateContact" runat="server" NavigateUrl="#" Text="Update">
                                                                        <ClientSideEvents Click="function(s,e){gridPersonDet2.UpdateEdit();}" />
                                                                    </dxe:ASPxHyperLink>
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelContact" ReplacementType="EditFormCancelButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                </div>
                                                            </EditForm>
                                                        </Templates>
                                                        <SettingsPager Mode="ShowPager"></SettingsPager>
                                                        <Styles Header-HorizontalAlign="Center">
                                                            <Header HorizontalAlign="Center"></Header>
                                                            <Cell HorizontalAlign="Center"></Cell>
                                                        </Styles>
                                                    </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl8" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton12" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%#  SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                isUpload=true;
                                                            PopupUploadPhoto();
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                            UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                                grd_Photo.Refresh();
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                                                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init">
                                                <Settings />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_photo_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_photo_del" runat="server"
                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <div style="height: 100px;">
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                        </div>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>Remark
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="800" Text='<%# Bind("FileNote") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                            <dxe:ASPxHyperLink ID="btn_UpdatePhoto" runat="server" NavigateUrl="#" Text="Update">
                                                                <ClientSideEvents Click="function(s,e){grd_Photo.UpdateEdit();}" />
                                                            </dxe:ASPxHyperLink>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                        </div>
                                                    </EditForm>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                            <TabStyle BackColor="#F0F0F0">
                            </TabStyle>
                            <ContentStyle BackColor="#F0F0F0">
                            </ContentStyle>
                        </dxtc:ASPxPageControl>
                        
                        <dxtc:ASPxPageControl ID="PageControl1" runat="server" Width="1024" Height="500px" EnableCallBacks="true" BackColor="#F0F0F0" ActiveTabIndex="0">
                            <TabPages>
                                <dxtc:TabPage Text="Recruitment/InterView">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_RecItv" runat="server" ClientInstanceName="grid_RecItv"
                                                    KeyFieldName="Id" AutoGenerateColumns="False"
                                                    Width="1000px" OnInit="grid_RecItv_Init" OnBeforePerformDataSelect="grid_RecItv_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" FieldName="BelongTo" VisibleIndex="1" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="Date" VisibleIndex="2" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="PIC" FieldName="Pic" VisibleIndex="3" Width="150">
                                                            <PropertiesComboBox ValueType="System.String" DataSourceID="dsPIC" Width="150" TextFormatString="{1}" DropDownWidth="105"
                                                                TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                                                    <dxe:ListBoxColumn FieldName="Name" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Department" FieldName="Department" VisibleIndex="4" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark1" FieldName="Remark" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark2" FieldName="Remark1" VisibleIndex="6">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Contract">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Add New"
                                                Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Contract.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Contract" runat="server" ClientInstanceName="grid_Contract" DataSourceID="dsContract"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInitNewRow="grid_Contract_InitNewRow" OnRowInserting="grid_Contract_RowInserting"
                                                    OnRowDeleting="grid_Contract_RowDeleting" OnRowUpdating="grid_Contract_RowUpdating"
                                                    Width="1000px" OnInit="grid_Contract_Init" OnBeforePerformDataSelect="grid_Contract_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Contract_edit" runat="server" Text="Edit" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Contract.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Contract_del" runat="server"
                                                                                Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Contract.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PersonId" FieldName="Person" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="No" FieldName="No" VisibleIndex="1" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="PIC" FieldName="Pic" VisibleIndex="2" Width="150">
                                                            <PropertiesComboBox ValueType="System.String" DataSourceID="dsPIC" Width="150" TextFormatString="{1}" DropDownWidth="105"
                                                                TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                                                    <dxe:ListBoxColumn FieldName="Name" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="Date" VisibleIndex="3" Width="100">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td>No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_No" ClientInstanceName="txt_No" Width="210" runat="server"
                                                                            Text='<%# Bind("No")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Date
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_ContractDay" Width="130" runat="server" Value='<%# Bind("Date")%>'
                                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>PIC
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_ContractPic" TextFormatString="{1}"
                                                                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                                                            DataSourceID="dsPIC" ValueField="Id" TextField="Name" Width="380" ValueType="System.Int32" Value='<%# Bind("Pic")%>'>
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
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_ContractRemark" Rows="5" runat="server" Width="380" Value='<%# Bind("Remark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Term Remark：
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_ContractTerm" Rows="5" runat="server" Width="380" Value='<%# Bind("Remark1") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Work Remark：
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_ContractWork" Rows="5" runat="server" Width="380" Value='<%# Bind("Remark2") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Salary Remark：
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_ContractSalary" Rows="5" runat="server" Width="380" Value='<%# Bind("Remark3") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                                <dxe:ASPxHyperLink ID="btn_UpdateContract" runat="server" NavigateUrl="#" Text="Update">
                                                                    <ClientSideEvents Click="function(s,e){grid_Contract.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="OT/Expense">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton7" Width="150" runat="server" Text="Add Overtime" AutoPostBack="false" UseSubmitBehavior="false"
                                                            Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>'>
                                                            <ClientSideEvents Click="function(s,e) {
                                                                txtTransType.SetText('OT');
                                                                grid_Trans.AddNewRow();
                                                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add Expense" AutoPostBack="false" UseSubmitBehavior="false"
                                                            Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>'>
                                                            <ClientSideEvents Click="function(s,e) {
                                                                txtTransType.SetText('EXPENSE');
                                                                grid_Trans.AddNewRow();
                                                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txtTransType" ClientVisible="false" ClientInstanceName="txtTransType" runat="server"></dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Trans" runat="server" ClientInstanceName="grid_Trans" DataSourceID="dsTrans" OnRowInserting="grid_Trans_RowInserting"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInitNewRow="grid_Trans_InitNewRow" OnRowUpdating="grid_Trans_RowUpdating" OnRowDeleting="grid_Trans_RowDeleting"
                                                    Width="1000px" OnInit="grid_Trans_Init" OnBeforePerformDataSelect="grid_Trans_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Contract_edit" runat="server" Text="Edit" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Trans.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Contract_del" runat="server"
                                                                                Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Trans.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Type" VisibleIndex="1" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Period" Caption="Period" VisibleIndex="2" Width="260">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                            <DataItemTemplate>
                                                                <dxe:ASPxTextBox runat="server" ID="text_ResignDate" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy" Width="260" ReadOnly="true"
                                                                    Value='<%# SafeValue.SafeDateStr(Eval("Date1"))+" "+Eval("Time1").ToString().Substring(0,2)+":"+Eval("Time1").ToString().Substring(2,2)+" - "+SafeValue.SafeDateStr(Eval("Date2"))+" "+Eval("Time2").ToString().Substring(0,2)+":"+Eval("Time2").ToString().Substring(2,2) %>' Border-BorderWidth="0">
                                                                </dxe:ASPxTextBox>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Hours" FieldName="Hrs" VisibleIndex="4" Width="50">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="PIC" FieldName="Pic" VisibleIndex="4" Width="100">
                                                            <PropertiesComboBox ValueType="System.String" DataSourceID="dsPIC" Width="100" TextFormatString="{1}" DropDownWidth="105"
                                                                TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                                                    <dxe:ListBoxColumn FieldName="Name" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="10">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="Amt" VisibleIndex="10" Width="60">
                                                            <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox runat="server" ID="txt_Type" ClientInstanceName="txt_Type" ReadOnly="true" BackColor="Control"
                                                                    Width="100" Text='<%# Bind("Type")%>'>
                                                                </dxe:ASPxTextBox>
                                                            </div>
                                                            <table border="0">
                                                                <tr>
                                                                    <td>From
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="date_Eta" Width="100" runat="server" Value='<%# Bind("Date1")%>'
                                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" Text='<%# Bind("Time1") %>' Width="60">
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
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="ASPxDateEdit1" Width="100" runat="server" Value='<%# Bind("Date2")%>'
                                                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="ASPxTextBox1" runat="server" Text='<%# Bind("Time2") %>' Width="60">
                                                                                        <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                                        <ValidationSettings ErrorDisplayMode="None" />
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>Hrs
                                                                    </td>
                                                                    <td width="110">
                                                                        <dxe:ASPxTextBox ID="txt_Hrs" ClientInstanceName="txt_Trans_Hrs" Width="90" runat="server"
                                                                            Text='<%# Bind("Hrs")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Amt</td>
                                                                    <td width="110">
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="90" ID="spin_Amt" Value='<%# Bind("Amt")%>' DecimalPlaces="2">
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>PIC
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_Trans_Pic" TextFormatString="{1}"
                                                                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                                                            DataSourceID="dsPIC" ValueField="Id" TextField="Name" Width="250" ValueType="System.Int32" Value='<%# Bind("Pic")%>'>
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
                                                                    <td colspan="10">
                                                                        <dxe:ASPxMemo ID="memo_Trans_Remark" Rows="5" runat="server" Width="100%" Value='<%# Bind("Remark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                <td>Create User/Date
                                </td>
                                <td>
                                    <%# Eval("CreateUser")%>-<%# Eval("CreateDate","{0:dd/MM/yyyy}")%>
                                </td>
                                <td>Update User/Date
                                </td>
                                <td>
                                    <%# Eval("UpdateUser")%>-<%# Eval("UpdateDate","{0:dd/MM/yyyy}")%>
                                </td>
                            </tr>--%>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                                <dxe:ASPxHyperLink ID="btn_UpdateTrans_" runat="server" NavigateUrl="#" Text="Update">
                                                                    <ClientSideEvents Click="function(s,e){grid_Trans.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Leave">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Record"
                                                    Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                                grid_Leave.AddNewRow();;
                                                                }" />
                                                </dxe:ASPxButton>
                                            </div>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Leave" runat="server" ClientInstanceName="grid_Leave" DataSourceID="dsLeave" OnRowInserting="grid_Leave_RowInserting"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInitNewRow="grid_Leave_InitNewRow" OnRowUpdating="grid_Leave_RowUpdating" OnRowDeleting="grid_Leave_RowDeleting"
                                                    Width="1000px" OnInit="grid_Leave_Init" OnBeforePerformDataSelect="grid_Leave_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="80">
                                                            <EditButton Visible="true"></EditButton>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="80">
                                                            <DataItemTemplate>
                                                                <a href='/PagesHr/Report/PrintApplicationForLeave.aspx?no=<%# Eval("Id") %>' target="_blank">Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Person" FieldName="Person" VisibleIndex="1" Width="150" Visible="false">
                                                            <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                                                                TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                                                    <dxe:ListBoxColumn FieldName="Name" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ApplyDateTime" FieldName="ApplyDateTime" VisibleIndex="2" Width="150">
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
                                                        <dxwgv:GridViewDataTextColumn Caption="Days" FieldName="Days" VisibleIndex="4" Width="60">
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
                                                                    <td>ApplyDateTime
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_ApplyDateTime" Width="170" runat="server" Value='<%# Bind("ApplyDateTime") %>'
                                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                                                                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
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
                                                                                    <dxe:ASPxComboBox runat="server" ID="cmb_Time1" ClientInstanceName="cmb_Time1" Width="50" Text='<%#Bind("Time1") %>'>
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
                                                                        <dxe:ASPxSpinEdit runat="server" Width="90" ID="spin_Days" ClientInstanceName="spin_Days" Value='<%# Bind("Days")%>'>
                                                                            <SpinButtons ShowIncrementButtons="false" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>ApproveBy</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_ApproveBy" TextFormatString="{1}"
                                                                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                                                                            DataSourceID="dsSchPerson" ValueField="Id" TextField="Name" Width="170" ValueType="System.Int32" Value='<%# Bind("ApproveBy")%>'>
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
                                                                    <td>ApproveStatus
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cmb_ApproveStatus" ClientInstanceName="cmb_ApproveStatus" Text='<%#Bind("ApproveStatus") %>' runat="server" Width="170">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Draft" Value="Draft" />
                                                                                <dxe:ListEditItem Text="Approve" Value="Approve" />
                                                                                <dxe:ListEditItem Text="Reject" Value="Reject" />
                                                                                <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>
                                                                        Type
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" ID="txt_LeaveType" ClientInstanceName="txt_LeaveType"
                                                                            Width="90" Text='<%# Bind("LeaveType")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Applicant Remark
                                                                    </td>
                                                                    <td colspan="7">
                                                                        <dxe:ASPxMemo ID="memo_ApplicantRemark" Rows="3" runat="server" Width="860" Value='<%# Bind("Remark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Approve Remark
                                                                    </td>
                                                                    <td colspan="7">
                                                                        <dxe:ASPxMemo ID="memo_ApproveRemark" Rows="3" runat="server" Width="860" Value='<%# Bind("ApproveRemark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateLeave" runat="server" NavigateUrl="#" Text="Update">
                                                                    <ClientSideEvents Click="function(s,e){grid_Leave.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelLeave" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                                <div>
                                                    <dxe:ASPxButton ID="ASPxButton10" Width="150" runat="server" Text="Add Template"
                                                        Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s,e) {
                                                                grid_LeaveTmp.AddNewRow();;
                                                                }" />
                                                    </dxe:ASPxButton>
                                                </div>
                                                <dxwgv:ASPxGridView ID="grid_LeaveTmp" ClientInstanceName="grid_LeaveTmp" runat="server" OnRowInserting="grid_LeaveTmp_RowInserting"
                                                    DataSourceID="dsLeaveTmp" Width="1000px" KeyFieldName="Id" OnInit="grid_LeaveTmp_Init" OnRowUpdating="grid_LeaveTmp_RowUpdating"
                                                    OnInitNewRow="grid_LeaveTmp_InitNewRow" OnRowDeleting="grid_LeaveTmp_RowDeleting" OnBeforePerformDataSelect="grid_LeaveTmp_BeforePerformDataSelect"
                                                    AutoGenerateColumns="False">
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsPager PageSize="100" Mode="ShowPager">
                                                    </SettingsPager>
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <Settings ShowFilterRow="false" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_LeaveTmp_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_LeaveTmp.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_LeaveTmp_del" runat="server"
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_LeaveTmp.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_LeaveTmp_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_LeaveTmp.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_LeaveTmp_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_LeaveTmp.CancelEdit() }"  %>'>
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
                                                        <dxwgv:GridViewDataComboBoxColumn Caption="Person" FieldName="Person" VisibleIndex="0" Width="150" Visible="false">
                                                            <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                                                                TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                                                    <dxe:ListBoxColumn FieldName="Name" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Days" FieldName="Days" UnboundType="String" VisibleIndex="3" Width="75">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit NumberType="Integer" runat="server" Width="75" ID="spin_Days" Value='<%# Bind("Days")%>'>
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataSpinEditColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="LeaveType" VisibleIndex="4" Width="120">
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
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Log">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Log" runat="server" ClientInstanceName="grid_Log" DataSourceID="dsPersonLog"
                                                    KeyFieldName="Id" AutoGenerateColumns="False"
                                                    Width="1000px" OnInit="grid_Log_Init" OnBeforePerformDataSelect="grid_Log_DataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="1" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="LogDate" FieldName="LogDate" VisibleIndex="2" Width="100">
                                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="LogTime" FieldName="LogTime" VisibleIndex="3" Width="100">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxTextBox ID="txt_EtaTime" runat="server" Text='<%# Eval("LogTime") %>' ReadOnly="true" Border-BorderWidth="0" Width="60">
                                                                    <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" IncludeLiterals="None" />
                                                                    <ValidationSettings ErrorDisplayMode="None" />
                                                                </dxe:ASPxTextBox>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="4" Width="150">
                                                        </dxwgv:GridViewDataTextColumn>
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
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Appraisal">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add New"
                                                Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Comment.AddNewRow();;
                                                        }" />
                                            </dxe:ASPxButton>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Comment" runat="server" ClientInstanceName="grid_Comment" DataSourceID="dsComment"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInitNewRow="grid_Comment_InitNewRow" OnRowInserting="grid_Comment_RowInserting"
                                                    OnRowDeleting="grid_Comment_RowDeleting" OnRowUpdating="grid_Comment_RowUpdating"
                                                    Width="1000px" OnInit="grid_Comment_Init" OnBeforePerformDataSelect="grid_Comment_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Comment_edit" runat="server" Text="Edit" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Comment.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Comment_del" runat="server"
                                                                                Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Comment.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Manager" FieldName="Manager" VisibleIndex="1" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Rating" FieldName="Rating" VisibleIndex="3" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="Date" VisibleIndex="4" Width="100">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="6" Width="80">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td>Manager
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_CommentManager" ClientInstanceName="txt_CommentManager" Width="180" runat="server"
                                                                            Text='<%# Bind("Manager")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Date
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_CommentDate" Width="150" runat="server" Value='<%# Bind("Date")%>'
                                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td rowspan="2">Remark：
                                                                    </td>
                                                                    <td rowspan="2">
                                                                        <dxe:ASPxMemo ID="memo_CommentRemark" Rows="3" runat="server" Width="380" Value='<%# Bind("Remark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Rating
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_CommentRating" ClientInstanceName="txt_CommentRating" Width="180" runat="server"
                                                                            Text='<%# Bind("Rating")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Status</td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_CommentStatus" ClientInstanceName="txt_CommentStatus" Width="150" runat="server"
                                                                            Text='<%# Bind("Status")%>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Work Remark：
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_CommentWork" Rows="5" runat="server" Width="380" Value='<%# Bind("Remark1") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Other Remark：
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="memo_CommentPary" Rows="5" runat="server" Width="380" Value='<%# Bind("Remark2") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">

                                                                <dxe:ASPxHyperLink ID="btn_UpdateComment" runat="server" NavigateUrl="#" Text="Update">
                                                                    <ClientSideEvents Click="function(s,e){grid_Comment.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Payroll Setup">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Add New"
                                                Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_Quote.AddNewRow();;
                                                        }" />
                                            </dxe:ASPxButton>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Quote" runat="server" ClientInstanceName="grid_Quote" DataSourceID="dsQuote"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInitNewRow="grid_Quote_InitNewRow" OnRowInserting="grid_Quote_RowInserting"
                                                    OnRowDeleting="grid_Quote_RowDeleting" OnRowUpdating="grid_Quote_RowUpdating"
                                                    Width="1000px" OnInit="grid_Quote_Init" OnBeforePerformDataSelect="grid_Quote_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
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
                                                                            <dxe:ASPxButton ID="btn_Quote_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Quote.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Quote_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Quote.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PayItem" FieldName="PayItem" VisibleIndex="1" Width="150">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxButtonEdit ID="txt_PayItem" ClientInstanceName="txt_PayItem" runat="server" ReadOnly="true" BackColor="Control" Width="150" HorizontalAlign="Left"
                                                                    Text='<%# Bind("PayItem")%>' AutoPostBack="False">
                                                                    <Buttons>
                                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupItem(txt_PayItem,null);
                                                                        }" />
                                                                </dxe:ASPxButtonEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataSpinEditColumn Caption="Amt" FieldName="Amt" UnboundType="String" VisibleIndex="2" Width="120">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="120" ID="spin_Amt" Value='<%# Bind("Amt")%>' DecimalPlaces="2">
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
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                
                                <dxtc:TabPage Text="CPF">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add New"
                                                Visible='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("Status")).ToUpper()!="RESIGNATION" %>' AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_CPF.AddNewRow();;
                                                        }" />
                                            </dxe:ASPxButton>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_CPF" runat="server" ClientInstanceName="grid_CPF" DataSourceID="dsCPF"
                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnInitNewRow="grid_CPF_InitNewRow" OnRowInserting="grid_CPF_RowInserting"
                                                    OnRowDeleting="grid_CPF_RowDeleting" OnRowUpdating="grid_CPF_RowUpdating"
                                                    Width="1000px" OnInit="grid_CPF_Init" OnBeforePerformDataSelect="grid_CPF_BeforePerformDataSelect">
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsCustomizationWindow Enabled="True" />
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CPF_edit" runat="server" Text="Edit" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_CPF.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CPF_del" runat="server"
                                                                                Text="Delete" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_CPF.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CPF_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_CPF.UpdateEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_CPF_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_CPF.CancelEdit() }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Person" FieldName="Person" VisibleIndex="0" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="FromDate" FieldName="FromDate" VisibleIndex="1" Width="100">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataDateColumn Caption="ToDate" FieldName="ToDate" VisibleIndex="1" Width="100">
                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="EmployerCPF" FieldName="Cpf1" VisibleIndex="3">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="130" ID="spin_EmployerCPF" Value='<%# Bind("Cpf1")%>' DecimalPlaces="2">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="SelfCPF" FieldName="Cpf2" VisibleIndex="4">
                                                            <EditItemTemplate>
                                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="130" ID="spin_SelfCPF" Value='<%# Bind("Cpf2")%>' DecimalPlaces="2">
                                                                    <SpinButtons ShowIncrementButtons="false" />
                                                                </dxe:ASPxSpinEdit>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowPager"></SettingsPager>
                                                    <Styles Header-HorizontalAlign="Center">
                                                        <Header HorizontalAlign="Center"></Header>
                                                        <Cell HorizontalAlign="Center"></Cell>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                            <TabStyle BackColor="#F0F0F0">
                            </TabStyle>
                            <ContentStyle BackColor="#F0F0F0">
                            </ContentStyle>
                        </dxtc:ASPxPageControl>

                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                          if(isUpload)
	                        grd_Photo.Refresh();
                    }" />
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                          if(grid!=null)
	                        grid.Refresh();
	                        grid=null;
                    }" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
