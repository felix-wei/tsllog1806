<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollList.aspx.cs" Inherits="Modules_Hr_Job_PayrollList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payroll Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>
        <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript">
        var config = {
            timeout: 0,
            gridview: 'grid_Transport',
        }
        $(function () {
            loading.hide();
        })
        var clientId = null;
        var clientName = null;
        // PayItem
        function PopupItem(codeId, desId) {
            clientId = codeId;
            clientName = desId;
            popubCtr.SetHeaderText('Pay Item');
            popubCtr.SetContentUrl('../SelectPage/PayItemList.aspx');
            popubCtr.Show();
        }
        function ImportDet() {
            popubCtr.SetHeaderText('Pick From Quote');
            popubCtr.SetContentUrl('../SelectPage/QuoteList.aspx?mId=' + txt_Oid.GetText() + "&pId=" + cmb_Person.GetValue());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            ASPxGridView1.Refresh();
            grid_det.Refresh();
        }
        function OnComfirCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
            btn_Sch.OnClick(null, null);
        }
        function PrintPayRoll(invN, from, to,person) {
            var fromM = from.getMonth() + 1;
            var fromD = from.getDate();
            var fromY = from.getFullYear();

            var toM = to.getMonth() + 1;
            var toD = to.getDate();
            var toY = to.getFullYear();

            var fromDate = fromY + '-' + fromM + '-' + fromD;
            var toDate = toY + '-' + toM + '-' + toD;
            window.open('/Modules/Hr/Report/PrintView.aspx?master=' + invN + '&document=PayrollSlip' + '&from=' + fromDate + '&to=' + toDate+'&person='+person);
            //window.open('/Modules/Hr/Report/PrintPayrollSlip.aspx?no=' + invN + '&from=' + fromDate + '&to=' + toDate);
        }

        function printPayroll(payId) {
			 window.open('/Modules/Hr/PrintPayslip.aspx?id=' + payId);
			 
        }

        function print(rowIndex) {
            console.log(rowIndex);

            setTimeout(function () {
                ASPxGridView1.GetValuesOnCustomCallback('Printline_' + rowIndex, print_inline_callback);
            }, config.timeout);
        }
        function print_inline_callback(res) {
            console.log(res);
            var str = new Array();
            str = res.split(',');

            var fromM =new Date(str[0]).getMonth() + 1;
            var fromD = new Date(str[0]).getDate();
            var fromY = new Date(str[0]).getFullYear();

            var toM = new Date(str[1]).getMonth() + 1;
            var toD = new Date(str[1]).getDate();
            var toY = new Date(str[1]).getFullYear();

            var fromDate = fromY + '-' + fromM + '-' + fromD;
            var toDate = toY + '-' + toM + '-' + toD;
            var invN = str[2];
            var person = str[3];

            window.open('/Modules/Hr/Report/PrintView.aspx?master=' + invN + '&document=PayrollSlip' + '&from=' + fromDate + '&to=' + toDate + '&person=' + person);
        }
        function edit(rowIndex) {
            console.log(rowIndex);

            setTimeout(function () {
                ASPxGridView1.GetValuesOnCustomCallback('Editline_' + rowIndex, edit_inline_callback);
            }, config.timeout);
        }
        function edit_inline_callback(res) {
            parent.navTab.openTab('Payroll Edit_' + res, "/Modules/Hr/Job/PayrollEdit.aspx?no=" + res, { title: 'Payroll Edit_' + res, fresh: false, external: true });
            //window.location = 'PayrollEdit.aspx?no=' + res;
        }
        function del(rowIndex) {
            console.log(rowIndex);

            setTimeout(function () {
                ASPxGridView1.GetValuesOnCustomCallback('Deleteline_' + rowIndex, del_inline_callback);
            }, config.timeout);
        }
        function del_inline_callback(res) {
            if (res == 'Success') {
                btn_Sch.OnClick(null, null);
            }
        }
        function addNew() {
            parent.navTab.openTab('Payroll New', "/Modules/Hr/Job/PayrollEdit.aspx?no=NEW", { title: 'Payroll New', fresh: false, external: true });
            //window.location = 'PayrollEdit.aspx?no=' + res;
        }
        function SelectAll() {
            if (btn_select.GetText() == "Select All")
                btn_select.SetText("UnSelect All");
            else
                btn_select.SetText("Select All");
            jQuery("input.batch").each(function () {
                this.click();
            });
        }
        function UnSelectAll() {
            jQuery("INPUT[type='checkbox']").each(function () {
                if (this.checked)
                    this.click();
            });
        }
        function PrintAll() {
            if (confirm("Confirm Print of Payroll ?")) {
                var refnos = "";
                jQuery("input.batch").each(function () {
                    if (this.checked) {
						var _ids = this.id.split('_');
					 refnos += _ids[0] + ',';
					}
                });
                var pos = encodeURIComponent(refnos);
                if (pos.length > 0) {
                    var url = '/Modules/Hr/PrintPayslip.aspx?id=' + refnos;
                    //var url = '/Modules/Hr/BatchPrint.ashx?no=' + pos;
                    window.open(url);
                }
                else {
                    alert("Pls select at least one");
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsPayroll" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPayroll" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPayrollDet" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPayrollDet" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
            <wilson:DataSource ID="dsPayItem" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPayItem" KeyMember="Id" />
            <wilson:DataSource ID="dsDepartment" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='Department'" />
            <table>
                <tr>
                    <td>Name
                    </td>
                    <td style="display: none">
                        <dxe:ASPxTextBox ID="txtSchId" ClientInstanceName="txtSchId" runat="server"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txtSchName" ClientInstanceName="txtSchName" TextFormatString="{1}"
                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                            DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="200px" ValueType="System.Int32">
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
                        Department
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Department"
                            DataSourceID="dsDepartment" TextField="Code" ValueField="Code" Width="150">
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
                            </tr>
                        </table>
                    </td>
                    <td colspan="6">
                        <table>
                            <tr>
                                 <td>
                                    <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                                                       addNew();
                                                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Export" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Auto" Width="100" runat="server" Text="Generate Payroll" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    ASPxGridView1.GetValuesOnCustomCallback('Payroll',OnComfirCallback);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td style="display:none">
                                    <dxe:ASPxButton ID="btn_Confirm" Width="110" runat="server" Text="Confirm Payroll" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    ASPxGridView1.GetValuesOnCustomCallback('Confirm',OnComfirCallback);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton6" Width="110" runat="server" Text="Save CPF" OnClick="ASPxButton6_Click" Visible="false">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_print" runat="server" Text="Batch Print" AutoPostBack="false" UseSubmitBehavior="false" Width="100">
                                        <ClientSideEvents Click="function(s,e) {
                        PrintAll();
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_select" ClientInstanceName="btn_select" Height="25px" runat="server" Text="Select All" Visible="true" Width="120" AutoPostBack="False"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                Width="100%" KeyFieldName="Id" OnInit="ASPxGridView1_Init"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback"
                OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
                AutoGenerateColumns="False">
                <SettingsEditing Mode="EditForm" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowFilterRow="false" />
                <SettingsBehavior ConfirmDelete="True" />
                <Settings ShowFooter="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Person" SummaryType="Count" DisplayFormat="{0:0}" />
                    <dxwgv:ASPxSummaryItem FieldName="Amt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Cpf1Amt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Cpf2Amt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Total1" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Total2" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="NetPay" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                </TotalSummary>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="5%" VisibleIndex="0">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton7" runat="server" Text="Print" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { printPayroll("+Eval("Id","{0}")+") }"  %>'>
                                        </dxe:ASPxButton>
                                        <div style="display:none">
                                            <dxe:ASPxLabel ID="lbl_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                        </div>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_payroll_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { edit("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_payroll_del" runat="server"
                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){del("+Container.VisibleIndex+") }}"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Employee" FieldName="Person" VisibleIndex="0" Width="100">
                        <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                            TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                <dxe:ListBoxColumn FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="2" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="FromDate" Caption="FromDate" VisibleIndex="2" Width="80" >
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ToDate" Caption="ToDate" VisibleIndex="3" Width="80">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Gross" FieldName="Total1" VisibleIndex="6" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Deduction" FieldName="Total2" VisibleIndex="6" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Net Pay" FieldName="NetPay" VisibleIndex="6" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CPF Employer" FieldName="Cpf1Amt" VisibleIndex="6" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CPF Employee" FieldName="Cpf2Amt" VisibleIndex="6" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="10">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="10"
                    Width="8%">
                    <DataItemTemplate>
                        <input type="checkbox" class="batch" value="<%# Eval("Id") %>" id='<%# Eval("Id")%>_<%# Eval("Person")%>_<%# SafeValue.SafeDate(Eval("FromDate"),DateTime.Today).ToString("dd/MM/yyyy")%>_<%#  SafeValue.SafeDate(Eval("ToDate"),DateTime.Today).ToString("dd/MM/yyyy")%>' name="<%# Eval("Id")%>" />
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="800" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>