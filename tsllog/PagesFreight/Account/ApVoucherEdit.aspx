<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="ApVoucherEdit.aspx.cs" Inherits="Account_ApVoucherEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AP Voucher</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr1(); }
        }
        document.onkeydown = keydown;
    </script>
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
                PutDetAmt();
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
                if (acCode != null) {
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
        function OnCallback(v) {
            btn_CustRate.SetEnabled(false);
            grid_det.Refresh();
        }

        function OnbookCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function AddInvoiceDet_quote() {
            popubCtr.SetHeaderText('STD Rate');
            popubCtr.SetContentUrl('/SelectPage/Account/StdRateList.aspx?typ=AP&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AddInvoiceDet() {
            popubCtr.SetHeaderText('AR/AP Invoice/Quote');
            //popubCtr.SetContentUrl('BillList.aspx?typ=AP&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.SetContentUrl('/selectpage/account/BillList.aspx?typ=AP&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_det.Refresh();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsApPayable" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsApPayableDet" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayableDet" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXTerm" KeyMember="SequeceId" />
            <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />

            <table>
                <tr>
                    <td>Doc No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txtSchNo" ClientInstanceName="txtSchNo" Width="150" runat="server"
                            Text="">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" Width="110" runat="server" Text="Retrieve" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                     window.location='ApVoucherEdit.aspx?no='+txtSchNo.GetText()
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                  <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                           window.location='ApVoucherList.aspx';
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                DataSourceID="dsApPayable" Width="100%" KeyFieldName="SequenceId" OnInit="ASPxGridView1_Init"
                OnInitNewRow="ASPxGridView1_InitNewRow"
                OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback1"
                AutoGenerateColumns="False" OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating">
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
                                    <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                        Width="100" Text='<%# Eval("SequenceId")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">Doc No
                                </td>
                                <td width="160">
                                    <dxe:ASPxTextBox runat="server" ID="txt_DocNo" ClientInstanceName="txt_DocNo" ReadOnly="true"
                                        BackColor="Control" Width="130" Text='<%# Eval("DocNo")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td width="100">Doc Type
                                </td>
                                <td width="160">
                                    <dxe:ASPxComboBox runat="server" ID="cbo_DocType" ReadOnly="true" BackColor="Control"
                                        Width="155" Text='<%# Eval("DocType")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="VO" Text="VO" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="100">Doc Date
                                </td>
                                <td width="160">
                                    <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="100" Value='<%# Eval("DocDate")%>'
                                         EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>Party To
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server" Value='<%# Eval("PartyTo") %>'
                                        Width="423" DropDownWidth="380" DropDownStyle="DropDownList" DataSourceID="dsVendorMast"
                                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="40px" />
                                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="100px">Term
                                </td>
                                <td width="150px">
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_TermId"
                                        DataSourceID="dsTerm" TextField="Code" ValueField="Code" Width="100" Value='<%# Eval("Term")%>'>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Cheque No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_ChqNo" ClientInstanceName="txt_ChqNo"
                                        Width="130" runat="server" Text='<%# Eval("ChqNo") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Cheque Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_SupplierBillDate" runat="server" Width="155" Value='<%# Eval("ChqDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>PC No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_SupplierBillNo" ClientInstanceName="txt_SupplierBillNo"
                                        Width="100" runat="server" Text='<%# Eval("SupplierBillNo") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Currency
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_Currency" ClientInstanceName="txt_Currency" Width="80" runat="server" MaxLength="3"
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
                                <td>Ex Rate
                                </td>
                                <td>
                                    <dxe:ASPxSpinEdit Increment="0" ID="txt_DocExRate" Width="155" ClientInstanceName="txt_DocExRate"
                                        DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Eval("ExRate") %>'>
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: Gray; color: White;">
                                    <b>A/C Information</b>
                                </td>
                            </tr>
                            <tr>
                                <td>A/C Code
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" ID="txt_AcCode" ClientInstanceName="txt_AcCode" Width="80" Text='<%# Eval("AcCode")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="ASPxButton2" Width="40" runat="server" Text="Pick" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
                                                        PopupBank(txt_AcCode,null);
                                                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>A/C Source
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="txt_AcSource" Width="155" ReadOnly="true" BackColor="Control"
                                        Text='<%# Eval("AcSource")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="CR" Text="CR" />
                                            <dxe:ListEditItem Value="DB" Text="DB" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>A/C Period
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="46" ReadOnly="true" BackColor="Control" ID="txt_AcYear"
                                                    Text='<%# Eval("AcYear")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="46" ReadOnly="true" BackColor="Control" ID="txt_AcPeriod"
                                                    Text='<%# Eval("AcPeriod")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-color: Gray; color: White;">
                                    <b>Job Information</b>
                                </td>
                            </tr>
                            <tr>
                                <td>Ref No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_MastRefNo" runat="server" Text='<%# Eval("MastRefNo") %>' Width="130">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Job No </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_JobRefNo" runat="server" Text='<%# Eval("JobRefNo") %>' Width="155">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Job Type </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbo_DocCate" runat="server" BackColor="Control" ClientInstanceName="cbo_DocCate" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="100">
                                        <Items>
                                            <dxe:ListEditItem Text="SI" Value="SI" />
                                            <dxe:ListEditItem Text="SE" Value="SE" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Remarks
                                </td>
                                <td colspan="5">
                                    <dxe:ASPxMemo runat="server" ID="txt_Remarks1" Rows="3" Width="660" Text='<%# Eval("Description")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.UpdateEdit();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td colspan="2"><table>
                                    <td>
                                        <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Line" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                            AutoPostBack="false" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e){
                                grid_det.AddNewRow();
                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_DetAdd_quote" runat="server" Text="Add Line From Quote" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                            AutoPostBack="false" UseSubmitBehavior="false" Width="160">
                                            <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet_quote();
                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </table>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd1" runat="server" Text="Add Line From AR/AP" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false" Width="150">
                                        <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                PrintPayable(txt_DocNo.GetText(),'VO','S');
                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                            </tr>
                        </table>
                        <table width="800">
                            <tr>
                                <td colspan="6">
                                    <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server"
                                        DataSourceID="dsApPayableDet" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid_InvDet_BeforePerformDataSelect"
                                        OnRowUpdating="grid_InvDet_RowUpdating" OnRowInserting="grid_InvDet_RowInserting"
                                        OnInitNewRow="grid_InvDet_InitNewRow" OnInit="grid_InvDet_Init" OnRowDeleting="grid_InvDet_RowDeleting"
                                        OnRowInserted="grid_InvDet_RowInserted" OnRowUpdated="grid_InvDet_RowUpdated" OnRowDeleted="grid_InvDet_RowDeleted" Width="100%"
                                        AutoGenerateColumns="False">
                                        <SettingsEditing Mode="EditForm" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <Columns>
                                            <dxwgv:GridViewDataColumn Caption="#">
                                                <DataItemTemplate>
                                                    <div style='display: <%# Eval("Display")%>'>
                                                        <a href="#" onclick='<%# "grid_det.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                        <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_det.DeleteRow("+Container.VisibleIndex+");"  %>}'>Del</a>
                                                    </div>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="No" FieldName="DocLineNo" VisibleIndex="3"
                                                Width="20" SortIndex="0" SortOrder="Ascending">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="ChgCode" FieldName="ChgCode" VisibleIndex="3"
                                                Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Ac Code" FieldName="AcCode" VisibleIndex="3"
                                                Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgDes1" VisibleIndex="3"
                                                Width="330">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="5"
                                                Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="5"
                                                Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Gst Type" FieldName="GstType" VisibleIndex="5"
                                                Width="80">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Line Amt" FieldName="LocAmt" VisibleIndex="6"
                                                Width="80">
                                                <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFooter="true" />
                                        <TotalSummary>
                                            <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                            <dxwgv:ASPxSummaryItem FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                        </TotalSummary>
                                        <Templates>
                                            <EditForm>
                                                <table style="border-bottom: solid 1px black;">
                                                    <tr>
                                                        <td width="40">
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_LineN" runat="server" ReadOnly="true" BackColor="Control" Text='<%# Eval("DocLineNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="80">
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode" ClientInstanceName="txt_det_ChgCode" BackColor="Control" ReadOnly="true"
                                                                runat="server" Text='<%# Bind("ChgCode") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="80">
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode"
                                                                runat="server" Text='<%# Bind("AcCode") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="230">
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1" ClientInstanceName="txt_det_Des1"
                                                                runat="server" Text='<%# Bind("ChgDes1") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="60">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty" ClientInstanceName="spin_det_Qty"
                                                                runat="server" Value='<%# Bind("Qty") %>' DisplayFormatString="0.000">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutBillingAmt();
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td width="60">
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency"  MaxLength="3"
                                                                runat="server" Text='<%# Bind("Currency") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="60">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP" ClientInstanceName="spin_det_GstP"
                                                                ReadOnly="true" BackColor="Control" runat="server" Value='<%# Bind("Gst") %>' DisplayFormatString="0.00">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutBillingAmt();
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td width="100">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt" ClientInstanceName="spin_det_GstAmt" DisplayFormatString="0.00"
                                                                runat="server" Text='<%# Bind("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_ChgCode_Pick" runat="server" Text="Pick" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode_Ap(txt_det_ChgCode,txt_det_Des1,txt_det_Unit,txt_det_GstType,spin_det_GstP,txt_det_AcCode,txt_DetMastType.GetText());
                                                            }" />
                                                            </dxe:ASPxButton>
                                                        </td>

                                                        <td>
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_AcSource" ClientInstanceName="txt_AcSource" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("AcSource") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2" ClientInstanceName="txt_det_Remarks2"
                                                                runat="server" Enabled="true" Text='<%# Bind("ChgDes2") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price" ClientInstanceName="spin_det_Price" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                ReadOnly="false" runat="server" Text='<%# Bind("Price") %>'>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutBillingAmt();
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td width="80">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                                runat="server" Value='<%# Bind("ExRate") %>' DisplayFormatString="0.000000" DecimalPlaces="6">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutBillingAmt();
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType" ClientInstanceName="txt_det_GstType" runat="server" Text='<%# Bind("GstType") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="100">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt" ClientInstanceName="spin_det_DocAmt" DisplayFormatString="0.00"
                                                                ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3" runat="server" Text='<%# Bind("ChgDes3") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                                runat="server" Text='<%# Bind("Unit") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_Currency_Pick" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency,spin_det_ExRate);
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt" ClientInstanceName="spin_det_LocAmt" DisplayFormatString="0.00"
                                                                ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_MastRefNo" ReadOnly="true" BackColor="Control"  ClientInstanceName="btn_MastRefNo" runat="server" Text='<%# Bind("MastRefNo") %>' Width="150">
                                                                            <Buttons>
                                                                                <dxe:EditButton Text="..."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents  ButtonClick="function(s,e){
                                                                                 PopupRefNo(btn_MastRefNo,txt_DetMastType);
                                                                                }"/>
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="btn_JobRefNo" ReadOnly="true" BackColor="Control"  ClientInstanceName="btn_JobRefNo" runat="server" Text='<%# Bind("JobRefNo") %>' Width="150">
                                                                            <Buttons>
                                                                                <dxe:EditButton Text="..."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents  ButtonClick="function(s,e){
                                                                               PopupJobNo(btn_MastRefNo.GetText(),txt_DetMastType.GetText(),btn_JobRefNo);
                                                                                }"/>
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>Split Type
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox ID="cbo_SplitType" runat="server" Text='<%# Bind("SplitType")%>' Width="100">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Set" Value="Set" />
                                                                                <dxe:ListEditItem Text="WtM3" Value="WtM3" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                         <dxe:ASPxTextBox ID="txt_DetMastType" runat="server" BackColor="Control" ClientInstanceName="txt_DetMastType" ReadOnly="true" Text='<%# Bind("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80"></td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="9" style="text-align: right; padding: 2px 2px 2px 2px">
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton1" ReplacementType="EditFormUpdateButton"
                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton1" ReplacementType="EditFormCancelButton"
                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
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
