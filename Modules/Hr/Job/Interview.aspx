<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="Interview.aspx.cs" Inherits="Modules_Hr_Job_Interview" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Interview</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>

    <script type="text/javascript">
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            gridInterviewDet.Refresh();
        }
        function OnPostCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function AddInterviewDet_candidate() {
            popubCtr.SetHeaderText('Candidate');
            popubCtr.SetContentUrl('/PagesHr/SelectPage/CandidateList.aspx?typ=Interview&id=' + txt_Oid.GetText());
            popubCtr.Show();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsInterview" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrInterview" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Status='Employee'" />
            <wilson:DataSource ID="dsDepartment" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='Department'" />
            <table>
                <tr>
                    <td>Department</td>
                    <td>
                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" DataSourceID="dsDepartment" Width="150" ID="txtSchDpm"
                            runat="server" TextField="Code" ValueField="Code" ValueType="System.String">
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
                    <td>Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="Date_From" Width="140" runat="server" ClientInstanceName="FromDate"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                            DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="Date_To" Width="140" runat="server" ClientInstanceName="ToDate"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                            DisplayFormatString="dd/MM/yyyy">
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
                DataSourceID="dsInterview" Width="800px" KeyFieldName="Id" OnInit="ASPxGridView1_Init" OnRowUpdating="ASPxGridView1_RowUpdating"
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
                    <dxwgv:GridViewDataTextColumn FieldName="Date" Caption="Date" VisibleIndex="1" Width="80">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Department" FieldName="Department" VisibleIndex="2" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pic" FieldName="Pic" VisibleIndex="3" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="4">
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
                                <td>Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_InterviewDay" Width="110" runat="server" Value='<%# Bind("Date")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>Department
                                </td>
                                <td>
                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" DataSourceID="dsDepartment" Width="207" ID="cmb_Department"
                                        runat="server" Text='<%# Bind("Department") %>' TextField="Code" ValueField="Code" ValueType="System.String">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Pic
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_InterviewPic" ClientInstanceName="txt_InterviewPic" Width="240" runat="server"
                                        Text='<%# Bind("Pic")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Status
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_Status" ClientInstanceName="cmb_Status" Width="110" runat="server"
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
                                <td>Remark：
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="memo_InterviewRemark" Rows="5" runat="server" Width="400" Value='<%# Bind("Remark") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                                <td>Joiner Remark：
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="memo_InterviewJoiner" Rows="5" runat="server" Width="400" Value='<%# Bind("Remark1") %>'>
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
                                            <td style="width: 80px;">Job Status</td>
                                            <td style="width: 160px">
                                                <dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text='<%#Eval("StatusCode") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                    <hr>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_Interview_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                        ClientSideEvents-Click='<%# "function(s) { ASPxGridView1.UpdateEdit() }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton2" Width="80" runat="server" Text="Add Line"
                                        Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                        gridInterviewDet.AddNewRow();;
                                                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd_quote" runat="server" Text="Pick From Candidate" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false" Width="160">
                                        <ClientSideEvents Click="function(s,e){
                                            AddInterviewDet_candidate();
                                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Interview_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                        ClientSideEvents-Click='<%# "function(s) { ASPxGridView1.CancelEdit() }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>

                        <dxwgv:ASPxGridView ID="gridInterviewDet" ClientInstanceName="gridInterviewDet" runat="server"
                            KeyFieldName="Id" DataSourceID="dsPerson" Width="1000" OnBeforePerformDataSelect="gridInterviewDet_BeforePerformDataSelect"
                            OnInit="gridInterviewDet_Init" OnInitNewRow="gridInterviewDet_InitNewRow"
                            OnRowDeleting="gridInterviewDet_RowDeleting" OnRowInserting="gridInterviewDet_RowInserting" OnRowUpdating="gridInterviewDet_RowUpdating">
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
                                <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="1" Width="80">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Gender" FieldName="Gender" VisibleIndex="2" Width="50">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="3" Width="150">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="4">
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
                                    <div style="display:none">
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
                                    <div style="padding: 4px 4px 3px 4px;">
                                        <table border="0">
                                            <tr>
                                                <td>Name：
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txt_Name" runat="server" Width="150" Value='<%# Bind("Name") %>'>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>Gender:</td>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cbo_Gender" runat="server" Text='<%# Bind("Gender")%>' Width="150">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Male" Value="Male" />
                                                            <dxe:ListEditItem Text="Female " Value="Female" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td>Ic No:</td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtIcNo" runat="server" EnableIncrementalFiltering="True" Value='<%# Bind("IcNo") %>' Width="150">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>Birthday:</td>
                                                <td>
                                                    <dxe:ASPxDateEdit ID="date_Birthday" Width="150" runat="server" Value='<%# Bind("BirthDay")%>'
                                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                    </dxe:ASPxDateEdit>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>Country:</td>
                                                <td>
                                                    <dxe:ASPxButtonEdit ID="txt_Country" ClientInstanceName="txt_Country" runat="server"
                                                        Width="150" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("Country") %>'>
                                                        <Buttons>
                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                        </Buttons>
                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                      PopupCountry(null,txt_Country)
                                                                    }" />
                                                    </dxe:ASPxButtonEdit>
                                                </td>
                                                <td>Race：
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txt_Race" runat="server" Width="150" Value='<%# Bind("Race") %>'>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>Religion：
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txt_Religion" runat="server" Width="150" Value='<%# Bind("Religion") %>'>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>Married:</td>
                                                <td>
                                                    <dxe:ASPxCheckBox ID="ckb_Married" Checked='<%# SafeValue.SafeString(Eval("Married"),"N")=="Y" %>' ClientInstanceName="ckb_Married" runat="server" TextAlign="Left">
                                                    </dxe:ASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Telephone：
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txt_Telephone" runat="server" Width="150" Value='<%# Bind("Telephone") %>'>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>Email：
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txt_Email" runat="server" Width="150" Value='<%# Bind("Email") %>'>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Address：
                                                </td>
                                                <td colspan="3">
                                                    <dxe:ASPxMemo ID="memo_Address" Rows="4" runat="server" Width="360" Value='<%# Bind("Address") %>'>
                                                    </dxe:ASPxMemo>
                                                </td>
                                                <td>Remark：
                                                </td>
                                                <td colspan="3">
                                                    <dxe:ASPxMemo ID="memo_Remark" Rows="4" runat="server" Width="360" Value='<%# Bind("Remark") %>'>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Profile Remark：
                                                </td>
                                                <td colspan="3">
                                                    <dxe:ASPxMemo ID="memo_Profile" Rows="4" runat="server" Width="360" Value='<%# Bind("Remark1") %>'>
                                                    </dxe:ASPxMemo>
                                                </td>
                                                <td>Work Remark：
                                                </td>
                                                <td colspan="3">
                                                    <dxe:ASPxMemo ID="memo_Work" Rows="4" runat="server" Width="360" Value='<%# Bind("Remark2") %>'>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Education Remark：
                                                </td>
                                                <td colspan="3">
                                                    <dxe:ASPxMemo ID="memo_Education" Rows="4" runat="server" Width="360" Value='<%# Bind("Remark3") %>'>
                                                    </dxe:ASPxMemo>
                                                </td>
                                                <td>Family Remark：
                                                </td>
                                                <td colspan="3">
                                                    <dxe:ASPxMemo ID="memo_Family" Rows="4" runat="server" Width="360" Value='<%# Bind("Remark4") %>'>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align: right; padding: 2px 2px 2px 2px">

                                            <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" NavigateUrl="#" Text="Update">
                                                <ClientSideEvents Click="function(s,e){gridInterviewDet.UpdateEdit();}" />
                                            </dxe:ASPxHyperLink>
                                            <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormCancelButton"
                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        </div>
                                </EditForm>
                            </Templates>
                        </dxwgv:ASPxGridView>

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
