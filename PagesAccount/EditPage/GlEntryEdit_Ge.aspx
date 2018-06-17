<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="GlEntryEdit_Ge.aspx.cs" Inherits="Account_GlEntryEdit_Ge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GL</title>
    <script type="text/javascript" src="/Script/BasePages.js"></script>

    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        var clientType = null;
        var clientAcCode = null;
        function PutValue(s, name) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                clientId = null;
                clientName = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PutValue(s, name, type1) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                if (clientType != null) {
                    clientType.SetText(type1);
                }
                clientId = null;
                clientName = null;
                clientAcCode = null;
                clientType = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PutValue(s, name, type1, acCode) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                if (clientType != null) {
                    clientType.SetText(type1);
                }
                if (clientAcCode != null) {
                    clientAcCode.SetText(acCode);
                }
                clientId = null;
                clientName = null;
                clientAcCode = null;
                clientType = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PopupCurrency(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetHeaderText('Currency');
            popubCtr.SetContentUrl('../SelectPage/CurrencyList.aspx');
            popubCtr.Show();
        }
        function PutAmt() {
            var exRate = parseFloat(spin_det_ExRate.GetText());
            var crAmt = parseFloat(spin_det_CrAmt.GetText());
            var dbAmt = parseFloat(spin_det_DbAmt.GetText());

            spin_det_CuryCrAmt.SetNumber(crAmt * exRate.toFixed(2));
            spin_det_CuryDbAmt.SetNumber(dbAmt * exRate.toFixed(2));
        }
        function OnbookCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        //select chart of account
        function RowClickHandler(s, e) {
            DropDownEdit.SetText(GridView.cpCode[e.visibleIndex]);
            txt_AcSource.SetText(GridView.cpSource[e.visibleIndex]);
            //txt_det_Des1.SetText(GridView.cpDes[e.visibleIndex]);
            DropDownEdit.HideDropDown();
        }
        //delete call back
        function OnDeleteCallback(v) {
            if (v == "CS") {
                alert("Delete Success!");
                ASPxGridView1.Refresh();
            }
            else if (v == "CF")
                alert("Delete Fail,please try again!");
        }
        function PrintBill() {
            var invN = txt_Oid.GetText();
            window.open("/ReportAccount/printview.aspx?doc=7&docId=" + invN);
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsGlEntry" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAGlEntry" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsGlEntryDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAGlEntryDet" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsGlAccChart" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartAcc" KeyMember="SequenceId" />
        <table>
            <tr>
                <td>
                    Doc No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtSchNo" ClientInstanceName="txtSchNo" Width="110" runat="server"
                        Text="">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" Width="110" runat="server" Text="Retrieve" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='GlEntryEdit_Ge.aspx?no='+txtSchNo.GetText()+'&type=GE'
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton9" Width="110" runat="server" Text="Add New" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='GlEntryEdit_Ge.aspx?no=0'
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_back" Width="110" runat="server" Text="Go Search" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='../GlEntry_GE.aspx';
                        }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsGlEntry" Width="800" KeyFieldName="SequenceId" OnInit="ASPxGridView1_Init"
            OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback"
            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback1"
            AutoGenerateColumns="False">
            <SettingsPager PageSize="50">
            </SettingsPager>
            <Settings ShowColumnHeaders="false" />
            <SettingsEditing Mode="EditForm" />
            <SettingsCustomizationWindow Enabled="True" />
            <Templates>
                <EditForm>
                    <table border="0">
                        <tr>
                            <td colspan="6" style="display: none">
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                Trans No
                            </td>
                            <td width="160">
                                <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                    Width="100" Text='<%# Eval("SequenceId")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="100">
                                A/C Period
                            </td>
                            <td width="160">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox runat="server" Width="40" ReadOnly="true" BackColor="Control" ID="txt_AcYear"
                                                Text='<%# Eval("AcYear")%>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox runat="server" Width="40" ReadOnly="true" BackColor="Control" ID="txt_AcPeriod"
                                                Text='<%# Eval("AcPeriod")%>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="100">
                                Module
                            </td>
                            <td width="160">
                                <dxe:ASPxComboBox runat="server" ID="cbo_ArAPInd" Width="100" Text='<%# Eval("ArApInd")%>'
                                    ReadOnly="true" BackColor="Control">
                                    <Items>
                                        <dxe:ListEditItem Value="AR" Text="AR" />
                                        <dxe:ListEditItem Value="AP" Text="AP" />
                                        <dxe:ListEditItem Value="GL" Text="GL" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Doc No
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_DocNo" ClientInstanceName="txt_DocNo" ReadOnly="true" BackColor="Control"
                                    Width="100" runat="server" Text='<%# Eval("DocNo") %>'>
                                </dxe:ASPxTextBox>
                            </td>
                            <td>
                                Doc Date
                            </td>
                            <td>
                                <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="100" Value='<%# Eval("DocDate")%>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td width="100">
                                Doc Type
                            </td>
                            <td width="160">
                                <dxe:ASPxComboBox runat="server" ID="cbo_DocType" Width="100" Text='<%# Eval("DocType")%>'
                                    ReadOnly="true" BackColor="Control">
                                    <Items>
                                        <dxe:ListEditItem Value="IV" Text="IV" />
                                        <dxe:ListEditItem Value="DN" Text="DN" />
                                        <dxe:ListEditItem Value="CN" Text="CN" />
                                        <dxe:ListEditItem Value="RE" Text="RE" />
                                        <dxe:ListEditItem Value="PC" Text="PC" />
                                        <dxe:ListEditItem Value="PL" Text="PL" />
                                        <dxe:ListEditItem Value="SC" Text="SC" />
                                        <dxe:ListEditItem Value="SD" Text="SD" />
                                        <dxe:ListEditItem Value="PS" Text="PS" />
                                        <dxe:ListEditItem Value="GE" Text="GE" />
                                        <dxe:ListEditItem Value="VO" Text="VO" />
                                        <dxe:ListEditItem Value="EX" Text="EX" />
                                        <dxe:ListEditItem Value="SR" Text="SR" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txt_Currency" ClientInstanceName="txt_Currency" Width="40" runat="server"
                                                Text='<%# Eval("CurrencyId") %>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_Currency_Pick" Width="40" runat="server" Text="Pick" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_Currency,txt_DocExRate);
                                                                    }" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                Ex Rate
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit Increment="0" ID="txt_DocExRate" Width="100" ClientInstanceName="txt_DocExRate"
                                    DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Eval("ExRate") %>'>
                                    <SpinButtons ShowIncrementButtons="false" />
                                </dxe:ASPxSpinEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Doc Debit
                            </td>
                            <td>
                                <strong>
                                    <%# Eval("DbAmt","{0:#,##0.00}") %></strong>
                            </td>
                            <td>
                                Doc Credit
                            </td>
                            <td>
                                <strong>
                                    <%# Eval("CrAmt","{0:#,##0.00}") %></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Debit Total
                            </td>
                            <td>
                                <strong>
                                    <%# Eval("CurrencyDbAmt","{0:#,##0.00}") %></strong>
                            </td>
                            <td>
                                Credit Total
                            </td>
                            <td>
                                <strong>
                                    <%# Eval("CurrencyCrAmt","{0:#,##0.00}") %></strong>
                            </td>
                            <td>
                                User
                            </td>
                            <td>
                                <%# Eval("UserId")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remarks
                            </td>
                            <td colspan="5">
                                <dxe:ASPxMemo runat="server" Rows="3" ID="txt_Remark" Width="660" Text='<%# Eval("Remark")%>'>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false"
                                 Enabled='<%# SafeValue.SafeString(Eval("PostInd"),"N")!="Y" %>'>
                                    <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Det" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("PostInd"),"N")!="Y" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                grid_det.AddNewRow();
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Delete" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("PostInd"),"N")!="Y" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                    ASPxGridView1.GetValuesOnCustomCallback('Delete',OnDeleteCallback);
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                PrintBill();
                            }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <table width="800">
                        <tr>
                            <td colspan="6">
                                <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server"
                                    DataSourceID="dsGlEntryDet" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid_InvDet_BeforePerformDataSelect"
                                    OnRowUpdating="grid_InvDet_RowUpdating" OnRowInserting="grid_InvDet_RowInserting"
                                    OnInitNewRow="grid_InvDet_InitNewRow" OnInit="grid_InvDet_Init" OnRowDeleting="grid_InvDet_RowDeleting"
                                    OnRowInserted="grid_InvDet_RowInserted" OnRowUpdated="grid_InvDet_RowUpdated"
                                    OnRowDeleted="grid_InvDet_RowDeleted" Width="100%" AutoGenerateColumns="False">
                                    <SettingsEditing Mode="EditForm" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <Columns>
                                        <dxwgv:GridViewDataColumn Caption="#">
                                            <DataItemTemplate>
                                                <div style='display: <%# Eval("Display")%>;'>
                                                    <a href="#" onclick='<%# "grid_det.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                    <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_det.DeleteRow("+Container.VisibleIndex+");"  %>}'>
                                                        Del</a>
                                                </div>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="No" FieldName="GlLineNo" VisibleIndex="3"
                                            Width="20" SortIndex="0" SortOrder="Ascending">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Ac Code" FieldName="AcCode" VisibleIndex="3"
                                            Width="80">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Remark" VisibleIndex="3"
                                            Width="300">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="5"
                                            Width="80">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="DB Amt" FieldName="CurrencyDbAmt" VisibleIndex="6"
                                            Width="80">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="CR Amt" FieldName="CurrencyCrAmt" VisibleIndex="6"
                                            Width="80">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFooter="true" />
                                    <TotalSummary>
                                        <dxwgv:ASPxSummaryItem FieldName="CurrencyDbAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                        <dxwgv:ASPxSummaryItem FieldName="CurrencyCrAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                    </TotalSummary>
                                    <Templates>
                                        <EditForm>
                                            <table style="border-bottom: solid 1px black;">
                                                <tr>
                                                    <td width="40">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_LineN" runat="server" ReadOnly="true" BackColor="Control"
                                                            Text='<%# Eval("GlLineNo") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxDropDownEdit ID="DropDownEdit" runat="server" ClientInstanceName="DropDownEdit"
                                                            Width="100%" AllowUserInput="False" Text='<%# Bind("AcCode") %>'>
                                                            <DropDownWindowTemplate>
                                                                <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                                                    Width="300px" DataSourceID="dsGlAccChart" KeyFieldName="SequenceId" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                    <SettingsPager PageSize="10">
                                                                    </SettingsPager>
                                                                    <Settings ShowFilterRow="true" />
                                                                    <Columns>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="SequenceId" VisibleIndex="0" Visible="false">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Code" VisibleIndex="0" Width="60">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="AcDorc" VisibleIndex="0" Width="60">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="AcDesc" Caption="Description" VisibleIndex="1">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <ClientSideEvents RowClick="RowClickHandler" />
                                                                </dxwgv:ASPxGridView>
                                                            </DropDownWindowTemplate>
                                                        </dxe:ASPxDropDownEdit>
                                                    </td>
                                                    <td width="300">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1" ClientInstanceName="txt_det_Des1"
                                                            runat="server" Text='<%# Bind("Remark") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency"
                                                            runat="server" Text='<%# Bind("CurrencyId") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="ASPxSpinEdit1" ClientInstanceName="spin_det_DbAmt"
                                                            DisplayFormatString="0.00" DecimalPlaces="2" runat="server" Text='<%# Bind("DbAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                            PutAmt();
	                                                   }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="ASPxSpinEdit2" ClientInstanceName="spin_det_CrAmt"
                                                            DisplayFormatString="0.00" DecimalPlaces="2" runat="server" Text='<%# Bind("CrAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                            PutAmt();
	                                                   }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                <dxe:ASPxComboBox runat="server" ID="txt_AcSource" ClientInstanceName="txt_AcSource" Width="100%" Text='<%# Bind("AcSource")%>'>
                                    <Items>
                                        <dxe:ListEditItem Value="CR" Text="CR" />
                                        <dxe:ListEditItem Value="DB" Text="DB" />
                                    </Items>
                                </dxe:ASPxComboBox>
                                                    </td>
                                                    <td></td>
                                                        <td width="80">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                                runat="server" Value='<%# Bind("ExRate") %>' DisplayFormatString="0.000000" DecimalPlaces="6">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                            PutAmt();
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td width="100">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_CuryDbAmt" ClientInstanceName="spin_det_CuryDbAmt"
                                                                DisplayFormatString="0.00" BackColor="Control" runat="server"
                                                                Text='<%# Eval("CurrencyDbAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_CuryCrAmt" ClientInstanceName="spin_det_CuryCrAmt"
                                                                DisplayFormatString="0.00" BackColor="Control" runat="server"
                                                                Text='<%# Eval("CurrencyCrAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Currency_Pick" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency,spin_det_ExRate);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="9" style="text-align: right; padding: 2px 2px 2px 2px">
                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton1" ReplacementType="EditFormUpdateButton"
                                                            runat="server">
                                                        </dxwgv:ASPxGridViewTemplateReplacement>
                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton1" ReplacementType="EditFormCancelButton"
                                                            runat="server">
                                                        </dxwgv:ASPxGridViewTemplateReplacement>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditForm>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                    </table>
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
