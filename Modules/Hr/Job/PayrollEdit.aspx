<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="PayrollEdit.aspx.cs" Inherits="Modules_Hr_Job_PayrollEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
            window.open('/Modules/Hr/PrintPayslip.aspx?id=' + invN);// + '&document=PayrollSlip' + '&from=' + fromDate + '&to=' + toDate+'&person='+person);
            //window.open('/Modules/Hr/Report/PrintView.aspx?master=' + invN + '&document=PayrollSlip' + '&from=' + fromDate + '&to=' + toDate+'&person='+person);
            //window.open('/Modules/Hr/Report/PrintPayrollSlip.aspx?no=' + invN + '&from=' + fromDate + '&to=' + toDate);
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
                    if (this.checked)
                        refnos += this.id + ',';
                });
                var pos = encodeURIComponent(refnos);
                if (pos.length > 0) {
                    var url = '/Modules/Hr/BatchPrint.ashx?no=' + pos;
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
            <table style="display: none">
                <tr >
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
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                DataSourceID="dsPayroll" Width="100%" KeyFieldName="Id" OnInit="ASPxGridView1_Init"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
                OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
                AutoGenerateColumns="False">
                <SettingsEditing Mode="EditForm" />
                 <Settings ShowColumnHeaders="false" />
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowFilterRow="false" />
                <SettingsBehavior ConfirmDelete="True" />
                <Settings ShowFooter="true" />
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
                                <td>Employee
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Person" ClientInstanceName="cmb_Person"
                                        EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList" TextFormatString="{1}"
                                        DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="150" ValueType="System.Int32" Value='<%# Eval("Person")%>'>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="100">Date From
                                </td>
                                <td width="160">
                                    <dxe:ASPxDateEdit ID="txt_FromDate" ClientInstanceName="txt_FromDate" runat="server" Width="150" Value='<%# Eval("FromDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>To
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_ToDate" ClientInstanceName="txt_ToDate" runat="server" Width="150" Value='<%# Eval("ToDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>Term
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="150" ID="txt_Term" ClientInstanceName="txt_Term"
                                        Text='<%# Eval("Term")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>PIC
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="150" ID="txt_Pic" ClientInstanceName="txt_Pic"
                                        Text='<%# Eval("Pic")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Amt
                                </td>
                                <td>
                                    <dxe:ASPxSpinEdit Increment="0" Width="150" ID="spin_Amt" ClientInstanceName="spin_Amt" ReadOnly="true" BackColor="Control"
                                        runat="server" Value='<%# Eval("Amt") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 80px;">Creation</td>
                                <td style="width: 160px; text-align: center"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                <td style="width: 90px;">Last Updated</td>
                                <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                <td style="width: 60px;">Status</td>
                                <td style="width: 160px">
                                    <dxe:ASPxComboBox runat="server" ID="cmb_StatusCode" OnCustomJSProperties="cmb_StatusCode_CustomJSProperties" ClientInstanceName="cmb_StatusCode" Text='<%#Eval("StatusCode") %>' Width="150" ClientEnabled='<%#SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                        <Items>
                                            <dxe:ListEditItem Text="Draft" Value="Draft" />
                                            <dxe:ListEditItem Text="Confirm" Value="Confirm" />
                                            <dxe:ListEditItem Text="Cancel" Value="Cancel"/>
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 60px;">Auto CPF Calc</td>
                                <td style="width: 160px">
                                    <dxe:ASPxComboBox runat="server" ID="cmb_AutoInd"  ClientInstanceName="cmb_AutoInd" Text='<%#Eval("AutoInd") %>' Width="150" ClientEnabled='<%#SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                                        <Items>
                                            <dxe:ListEditItem Text="Yes" Value="Yes" />
                                            <dxe:ListEditItem Text="No" Value="No" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
							
                                <td style="width: 80px;">CPF Employer (%)</td>
                                <td style="width: 160px; text-align: center">
								    <dxe:ASPxSpinEdit Increment="0" Width="150" ID="spin_cpf1" ClientInstanceName="spin_cpf1"  
                                        runat="server" Value='<%# Eval("Cpf1") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
								</td>
                                <td style="width: 80px;">CPF Employer ($)</td>
                                <td style="width: 160px; text-align: center">
								    <dxe:ASPxSpinEdit Increment="0" Width="150" ID="spin_cpf1amt" ClientInstanceName="spin_cpf1amt"  
                                        runat="server" Value='<%# Eval("Cpf1Amt") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
								</td>
                                
                            </tr>
                            <tr>
							<td style="width: 60px;">DOB </td>
                                <td style="width: 160px">
								<%# GetDob(Eval("Person","{0}")) %>		
                                </td>
                                <td style="width: 80px;">CPF Employee (%)</td>
                                <td style="width: 160px; text-align: center">
								    <dxe:ASPxSpinEdit Increment="0" Width="150" ID="spin_cpf2" ClientInstanceName="spin_cpf2"  
                                        runat="server" Value='<%# Eval("Cpf2") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
								</td>
                                <td style="width: 80px;">CPF Employer ($)</td>
                                <td style="width: 160px; text-align: center">
								    <dxe:ASPxSpinEdit Increment="0" Width="150" ID="spin_cpf2amt" ClientInstanceName="spin_cpf2amt"  
                                        runat="server" Value='<%# Eval("Cpf2Amt") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
								</td>
                                
                            </tr>
							
                            <tr>
                                <td>Remarks
                                </td>
                                <td colspan="5">
                                    <dxe:ASPxMemo runat="server" ID="txt_Remark" Rows="3" Width="645" Text='<%# Eval("Remark")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd" runat="server" Width="180" Text="Add Payroll Item" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"))=="Draft" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                                    grid_det.AddNewRow();
                                                }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td colspan="2">
                                    <dxe:ASPxButton Visible="false" ID="ASPxButton1" runat="server" Width="180" Text="Add Line From Quote" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("StatusCode"))=="Draft" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                                   ImportDet();
                                                }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                  <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                                        PrintPayRoll(txt_Oid.GetText(),txt_FromDate.GetValue(),txt_ToDate.GetValue(),cmb_Person.GetValue());
                                                }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td width="50"></td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false" ClientEnabled='<%#SafeValue.SafeString(Eval("StatusCode"))=="Draft" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                                    ASPxGridView1.PerformCallback('');
                                                }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton5" Width="100" runat="server" Text="UnConfirm" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    ASPxGridView1.GetValuesOnCustomCallback('UnConfirm',OnComfirCallback);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton4" Width="100" runat="server" Text="UnCancel" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'>
                                        <ClientSideEvents Click="function(s,e) {
                                                    ASPxGridView1.GetValuesOnCustomCallback('UnCancel',OnComfirCallback);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Back" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                window.location='PayrollList.aspx';
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>

                            </tr>
                        </table>
                        <table width="800">
                            <tr>
                                <td colspan="6">
                                    <dxwgv:ASPxGridView ID="grid_PayrollDet" ClientInstanceName="grid_det" runat="server" DataSourceID="dsPayrollDet" KeyFieldName="Id"
                                        OnInit="grid_PayrollDet_Init" OnBeforePerformDataSelect="grid_PayrollDet_BeforePerformDataSelect" OnInitNewRow="grid_PayrollDet_InitNewRow"
                                        OnRowInserting="grid_PayrollDet_RowInserting" OnRowUpdating="grid_PayrollDet_RowUpdating" OnRowDeleting="grid_PayrollDet_RowDeleting"
                                        OnRowInserted="grid_PayrollDet_RowInserted" OnRowUpdated="grid_PayrollDet_RowUpdated" OnRowDeleted="grid_PayrollDet_RowDeleted" 
                                        AutoGenerateColumns="False" Width="900px">
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <Columns>
                                            <dxwgv:GridViewDataColumn Caption="#" Width="120" VisibleIndex="0">
                                                <DataItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_Det_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                    ClientSideEvents-Click='<%# "function(s) { grid_det.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_Det_del" runat="server" Enabled='<%#SafeValue.SafeString(Eval("StatusCode"))!="Confirm" %>'
                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_det.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DataItemTemplate>
                                                <EditItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_Det_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%#SafeValue.SafeString(Eval("StatusCode"))!="Confirm" %>'
                                                                    ClientSideEvents-Click='<%# "function(s) { grid_det.UpdateEdit() }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton ID="btn_Det_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                    ClientSideEvents-Click='<%# "function(s) { grid_det.CancelEdit() }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataComboBoxColumn Caption="Payroll Item" FieldName="ChgCode" VisibleIndex="1" Width="80">
                                                <PropertiesComboBox ValueType="System.String" DataSourceID="dsPayItem" Width="150" TextFormatString="{1}" DropDownWidth="105"
                                                    TextField="Description" EnableIncrementalFiltering="true" ValueField="Code" DataMember="{1}">
                                                    <Columns>
                                                        <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="35px" />
                                                        <dxe:ListBoxColumn FieldName="Description" />
                                                    </Columns>
                                                </PropertiesComboBox>
                                            </dxwgv:GridViewDataComboBoxColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2">
                                                <EditItemTemplate>
                                                    <dxe:ASPxTextBox runat="server" ID="txt_Des" ClientInstanceName="txt_Des" Text='<%# Bind("Description") %>'>
                                                    </dxe:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="PIC" FieldName="Pic" VisibleIndex="3" Width="100" Visible="false">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataSpinEditColumn Caption="Amt" FieldName="Amt" UnboundType="String" VisibleIndex="4" Width="80">
                                                <EditItemTemplate>
                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" DecimalPlaces="2" runat="server" Width="120" ID="spin_Amt" Value='<%# Bind("Amt")%>' >
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit>

                                                </EditItemTemplate>
                                            </dxwgv:GridViewDataSpinEditColumn>
                                             <dxwgv:GridViewDataSpinEditColumn Caption="Before" FieldName="Before" UnboundType="String" VisibleIndex="4" Width="80" ReadOnly="true">
                                                 <PropertiesSpinEdit>
                                                     <SpinButtons ShowIncrementButtons="false" />
                                                 </PropertiesSpinEdit>
                                            </dxwgv:GridViewDataSpinEditColumn>
                                        </Columns>
                                        <Settings ShowFooter="true" />
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                            <dxwgv:ASPxSummaryItem FieldName="Amt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                        </TotalSummary>
                                    </dxwgv:ASPxGridView>
                                </td>
                            </tr>
                        </table>
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
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
