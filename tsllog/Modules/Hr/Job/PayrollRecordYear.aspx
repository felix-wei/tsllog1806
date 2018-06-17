<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="PayrollRecordYear.aspx.cs" Inherits="Modules_Hr_Job_PayrollRecordYear" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript">
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
            grid_det.Refresh();
        }
        function OnComfirCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function PrintPayRoll(invN, from, to, person) {
            var fromM = from.getMonth() + 1;
            var fromD = from.getDate();
            var fromY = from.getFullYear();

            var toM = to.getMonth() + 1;
            var toD = to.getDate();
            var toY = to.getFullYear();

            var fromDate = fromY + '-' + fromM + '-' + fromD;
            var toDate = toY + '-' + toM + '-' + toD;
            window.open('/Modules/Hr/Report/PrintView.aspx?master=' + invN + '&document=PayrollSlip' + '&from=' + fromDate + '&to=' + toDate + '&person=' + person);
            //window.open('/Modules/Hr/Report/PrintPayrollSlip.aspx?no=' + invN + '&from=' + fromDate + '&to=' + toDate);
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input.batch_IV").each(function () {
                this.click();
            });
        }
        function save_cpf() {
            grid.GetValuesOnCustomCallback('C_' + pos, onCpfCallBack);
            //var refnos = "";
            //jQuery("input.batch_IV").each(function () {//input[id*='ack_IsGst']
            //    if (this.checked)
            //        refnos += this.id + ',';
            //});
            //grid.GetValuesOnCustomCallback('C_' + pos, onCallBack);
            //var pos = refnos;
            ////alert(pos);
            //if (pos.length > 0) {
            //    grid.GetValuesOnCustomCallback('C_' + pos, onCallBack);
            //}
            //else {
            //    //alert("Pls select at least one");
            //    //btn_search.OnClick(null,null);
            //}
        }
        function onCpfCallBack() {

        }
        function save_iras() {
            grid.GetValuesOnCustomCallback('I_' + pos, onCallBack);
            //var refnos = "";
            //jQuery("input.batch_IV").each(function () {//input[id*='ack_IsGst']
            //    if (this.checked)
            //        refnos += this.id + ',';
            //});
            //var pos = refnos;
            ////alert(pos);
            //if (pos.length > 0) {
            //    grid.GetValuesOnCustomCallback('I_' + pos, onCallBack);
            //}
            //else {
            //    //alert("Pls select at least one");
            //    //btn_search.OnClick(null,null);
            //}
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
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Id>0" />
            <wilson:DataSource ID="dsPayItem" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPayItem" KeyMember="Id" />
            <table>
                <tr>
                    <td><dxe:ASPxLabel ID="lbl" runat="server" Text="Name"></dxe:ASPxLabel>
                        
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
                    <td><dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="From Date"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td><dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="To"></dxe:ASPxLabel></td>
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
<%--                                <td>
                                    <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Height="100%" Width="100" AutoPostBack="False"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                                    </dxe:ASPxButton>
                                </td>--%>
                                <td>
                                    
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Download IRAS e-File" OnClick="ASPxButton6_Click">
                                    </dxe:ASPxButton>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                Width="100%" KeyFieldName="Id" OnInit="ASPxGridView1_Init"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
                OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
                AutoGenerateColumns="False">
				<SettingsPager PageSize="1000" Mode="ShowPager" />
                <Settings ShowFooter="true" />
                <TotalSummary>
 		
				
                    <dxwgv:ASPxSummaryItem FieldName="Name" SummaryType="Count" DisplayFormat="{0:0}" />
                    <dxwgv:ASPxSummaryItem FieldName="Fee1" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Fee2" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Fee3" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Fee4" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Fee5" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Fee6" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    <dxwgv:ASPxSummaryItem FieldName="Fee7" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                </TotalSummary>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="PassType" FieldName="PassType" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="PassNo" FieldName="PassNo" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="Nationality" FieldName="Country" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="Gender" FieldName="Gender" VisibleIndex="0"  />
                     <dxwgv:GridViewDataTextColumn FieldName="BirthDay" Caption="DOB" VisibleIndex="2" Width="80" >
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Position" FieldName="Position" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="AddressType" FieldName="AddressType" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Address" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="Postal" FieldName="Postal" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="Country" FieldName="Country2" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn FieldName="DateStart" Caption="Date Commerce" VisibleIndex="2" Width="80" >
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn FieldName="DateEnd" Caption="Date End" VisibleIndex="2" Width="80" >
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Bank" FieldName="BankName" VisibleIndex="0"  />
                    <dxwgv:GridViewDataTextColumn Caption="AccNo" FieldName="BankAccNo" VisibleIndex="0"  />

                    <dxwgv:GridViewDataTextColumn Caption="GrossSalary" FieldName="Fee1" VisibleIndex="6" Width="100">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Bonus" FieldName="Fee2" VisibleIndex="6" Width="100">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DirectorFee" FieldName="Fee3" VisibleIndex="6" Width="100">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CDAC/SINDA" FieldName="Fee4" VisibleIndex="6" Width="100">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="MBMF" FieldName="Fee5" VisibleIndex="6" Width="100">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="EmployeeCPF" FieldName="Fee6" VisibleIndex="6" Width="100">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="EmployerCPF" FieldName="Fee7" VisibleIndex="6" Width="100">
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
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
