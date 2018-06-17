<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false"  CodeFile="ArCashInvoiceEdit.aspx.cs" Inherits="Account_ArCashInvoiceEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AR Invoice</title>
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
        //function PutValue(s, name, type1) {
        //    if (clientId != null) {
        //        clientId.SetText(s);
        //        if (clientName != null) {
        //            clientName.SetText(name);
        //        }
        //        if (clientType != null) {
        //            clientType.SetText(type1);
        //        }
        //        clientId = null;
        //        clientName = null;
        //        clientAcCode = null;
        //        clientType = null;
        //        popubCtr.Hide();
        //        popubCtr.SetContentUrl('about:blank');
        //    }
        //}
        //function PutValue(s, name, type1, acCode) {
        //    if (clientId != null) {
        //        clientId.SetText(s);
        //        if (clientName != null) {
        //            clientName.SetText(name);
        //        }
        //        if (clientType != null) {
        //            clientType.SetText(type1);
        //        }
        //        if (acCode != null) {
        //            clientAcCode.SetText(acCode);
        //        }
        //        clientId = null;
        //        clientName = null;
        //        clientAcCode = null;
        //        clientType = null;
        //        popubCtr.Hide();
        //        popubCtr.SetContentUrl('about:blank');
        //    }
        //}
        var unit = null;
        var gstType = null;
        var gstA = null;
        var gstP = null;
        function PopupChgCode(codeId, desId, unitId, gstTypeId, gstPId, acCode) {
            clientId = codeId;
            clientName = desId;
            unit = unitId;
            gstType = gstTypeId;
            gstP = gstPId;
            clientAcCode = acCode;
            popubCtr.SetHeaderText('Charge Code');
            popubCtr.SetContentUrl('../SelectPage/ChgCodeList_Ar.aspx');
            popubCtr.Show();
        }
        function PutChgCode(codeV, desV, unitV, gstTypeV, gstPV, acCode, acSource) {
            if (clientId != null) {
                clientId.SetText(codeV);
                clientName.SetText(desV);
                unit.SetText(unitV);
                gstType.SetText(gstTypeV);
                //gstA.SetNumber(gstAV);
                gstP.SetNumber(gstPV);

                clientAcCode.SetText(acCode);
                //clientType.SetText(acSource);
                clientId = null;
                clientName = null;
                unit = null;
                gstType = null;
                gstA = null;
                gstP = null;
                clientAcCode = null;
                clientType = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
                PutAmt();
            }
        }

        function PopupCurrency(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetHeaderText('Currency');
            popubCtr.SetContentUrl('../SelectPage/CurrencyList.aspx');
            popubCtr.Show();
        }
        function PutCurrency(s, v) {
            if (clientId != null) {
                clientId.SetText(s);
                clientName.SetNumber(v);
                clientId = null;
                clientName = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }

        function PopupBank(codeId, desId) {
            clientId = codeId;
            clientName = desId;
            popubCtr.SetHeaderText('Chart Of Account-Bank');
            popubCtr.SetContentUrl('../SelectPage/ChartOfAccount_bank.aspx');
            popubCtr.Show();
        }
        function PutAmt() {
            //var exRate = parseFloat(spin_det_ExRate.GetText());
            //var qty = parseFloat(spin_det_Qty.GetText());
            //var price = parseFloat(spin_det_Price.GetText());
            //var gst = parseFloat(spin_det_GstP.GetText());

            var amt = Calculate(spin_det_Qty.GetText(), spin_det_Price.GetText(), 1, 2);
            var gstAmt = Calculate(amt, spin_det_GstP.GetText(), spin_det_ExRate.GetText(), 2);
            var docAmt = Calculate(amt, spin_det_ExRate.GetText(), 1, 2);
            var locAmt = parseFloat(gstAmt) + parseFloat(docAmt);
            
            spin_det_GstAmt.SetNumber(gstAmt);
            spin_det_DocAmt.SetNumber(docAmt);
            spin_det_LocAmt.SetNumber(locAmt);
        }

        function OnPostCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function ShowReceipt(repNo, repType) {
            if (repType == "RE")
                window.location = 'ArReceiptEdit.aspx?no=' + repNo;
            else if (repType == "CN")
                window.location = 'ArCnEdit.aspx?no=' + repNo;
            else if (repType == "PS")
                window.location = 'ApPaymentEdit.aspx?no=' + repNo;
        }
        function AddInvoiceDet() {
            popubCtr.SetHeaderText('AR/AP Invoice');
            //popubCtr.SetContentUrl('ArInvoiceList.aspx?id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.SetContentUrl('BillList.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
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
        <wilson:DataSource ID="dsArInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsArInvoiceDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArInvoiceDet" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXTerm" KeyMember="SequeceId" />
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
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
                <td style="display:none">
                    <dxe:ASPxComboBox runat="server" ID="txt_DocType" ClientInstanceName="txt_DocType"
                        Width="100">
                        <Items>
                            <dxe:ListEditItem Value="CI" Text="CI" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" Width="110" runat="server" Text="Retrieve" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='ArCashInvoiceEdit.aspx?no='+txtSchNo.GetText()+'&type='+txt_DocType.GetText()
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton9" Width="110" runat="server" Text="Add New" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='ArCashInvoiceEdit.aspx?no=0&type='+txt_DocType.GetText();
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_back" Width="110" runat="server" Text="Go Search" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='../ArCashInvoice.aspx';
                        }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsArInvoice" Width="100%" KeyFieldName="SequenceId" OnInit="ASPxGridView1_Init"
            OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating"
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
                                <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                    Width="100" Text='<%# Eval("SequenceId")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                Doc No
                            </td>
                            <td width="160">
                                <dxe:ASPxTextBox runat="server" ID="txt_DocNo" ClientInstanceName="txt_DocNo" ReadOnly="true"
                                    BackColor="Control" Width="100" Text='<%# Eval("DocNo")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="100">
                                Doc Type
                            </td>
                            <td width="160">
                                <dxe:ASPxComboBox runat="server" ID="cbo_DocType" ReadOnly='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                    BackColor="Control" Width="100" Text='<%# Eval("DocType")%>'>
                                    <Items>
                                        <dxe:ListEditItem Value="IV" Text="IV" />
                                        <dxe:ListEditItem Value="DN" Text="DN" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td width="100">
                                Doc Date
                            </td>
                            <td width="160">
                                <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="100" Value='<%# Eval("DocDate")%>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Party To
                            </td>
                            <td colspan="3">
                                <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                                    Value='<%# Eval("PartyTo") %>' Width="370" DropDownWidth="380" DropDownStyle="DropDownList"
                                    DataSourceID="dsCustomerMast" ValueField="PartyId" ValueType="System.String" TextFormatString="{1}"
                                    EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                    CallbackPageSize="100">
                                    <Columns>
                                        <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                                        <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                    </Columns>
                                </dxe:ASPxComboBox>
                            </td>
                            <td width="100px">
                                Term
                            </td>
                            <td width="150px">
                                <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_TermId"
                                    DataSourceID="dsTerm" TextField="Code" ValueField="Code" Width="100" Value='<%# Eval("Term")%>'>
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
                                            <dxe:ASPxTextBox ID="txt_Currency" ClientInstanceName="txt_Currency" Width="60" runat="server"
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
                            <td>
                                Due Date
                            </td>
                            <td>
                                <dxe:ASPxDateEdit ID="txt_DueDt" runat="server" Enabled="false" BackColor="Control"
                                    Width="100" Value='<%# Eval("DocDueDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                    DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="background-color: Gray; color: White;">
                                <b>A/C Information</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                A/C Code
                            </td>
                            <td>
                               <table><tr><td>
                                <dxe:ASPxTextBox runat="server" ID="txt_AcCode" ClientInstanceName="txt_AcCode" Width="60" Text='<%# Eval("AcCode")%>'>
                                </dxe:ASPxTextBox></td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton2" Width="40" runat="server" Text="Pick" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
                                                        PopupBank(txt_AcCode,null);
                                                            }" />
                                    </dxe:ASPxButton>
                                </td></tr></table>
                            </td>
                            <td>
                                A/C Source
                            </td>
                            <td>
                                <dxe:ASPxComboBox runat="server" ID="txt_AcSource" Width="100" ReadOnly="true" BackColor="Control"
                                    Text='<%# Eval("AcSource")%>'>
                                    <Items>
                                        <dxe:ListEditItem Value="CR" Text="CR" />
                                        <dxe:ListEditItem Value="DB" Text="DB" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                                A/C Period
                            </td>
                            <td>
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
                        </tr>
                        <tr>
                            <td>
                                Remarks
                            </td>
                            <td colspan="5">
                                <dxe:ASPxMemo runat="server" ID="txt_Remarks1" Rows="3" Width="660" Text='<%# Eval("Description")%>'>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; padding: 2px 2px 2px 2px">
                                <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false"
                                    Enabled='<%# SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'>
                                    <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="text-align: right; padding: 2px 2px 2px 2px">
                                <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Det" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                grid_det.AddNewRow();
                            }" />
                                </dxe:ASPxButton>
                            </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd1" runat="server" Text="Add Line From AR/AP" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false" Width="150">
                                        <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            <td style="text-align: right; padding: 2px 2px 2px 2px">
                                <dxe:ASPxButton ID="btn_Post" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"
                                    Enabled='<%# SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>' Text="Post">
                                    <ClientSideEvents Click="function(s,e) {
                                    if(confirm('Confirm post it?')) {
                                                    ASPxGridView1.GetValuesOnCustomCallback('P,' + txt_DocNo.GetText(),OnPostCallback);
                                                    }
                                                 }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <table width="860">
                        <tr>
                            <td colspan="6">
                                                    <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server"
                                                        DataSourceID="dsArInvoiceDet" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid_InvDet_BeforePerformDataSelect"
                                                        OnRowUpdating="grid_InvDet_RowUpdating" OnRowInserting="grid_InvDet_RowInserting"
                                                        OnInitNewRow="grid_InvDet_InitNewRow" OnInit="grid_InvDet_Init" OnRowDeleting="grid_InvDet_RowDeleting"
                                                        OnRowInserted="grid_InvDet_RowInserted" OnRowUpdated="grid_InvDet_RowUpdated"
                                                        OnRowDeleted="grid_InvDet_RowDeleted" Width="100%" AutoGenerateColumns="False">
                                                        <SettingsEditing Mode="EditForm" />
                                                        <SettingsPager Mode="ShowAllRecords">
                                                        </SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataColumn Caption="#">
                                                                <DataItemTemplate>
                                                                    <div style='display: <%# Eval("Display")%>'>
                                                                        <a href="#" onclick='<%# "grid_det.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                                        <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_det.DeleteRow("+Container.VisibleIndex+");"  %>}'>
                                                                            Del</a>
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
                                                                <DataItemTemplate><%# Eval("AcCode") %>(<%# Eval("AcSource") %>)</DataItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgDes1" VisibleIndex="3"
                                                                Width="330">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Qty/Price" FieldName="Qty" VisibleIndex="5"
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
                                                            <dxwgv:ASPxSummaryItem FieldName="LocAmt1" ShowInColumn="LocAmt" SummaryType="Sum"
                                                                DisplayFormat="{0:#,##0.00}" />
                                                        </TotalSummary>
                                                        <Templates>
                                                            <EditForm>
                                                                <table style="border-bottom: solid 1px black;">
                                                                    <tr>
                                                                        <td width="40">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_LineN" runat="server" ReadOnly="true" BackColor="Control"
                                                                                Text='<%# Eval("DocLineNo") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="80">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode" ClientInstanceName="txt_det_ChgCode"
                                                                                BackColor="Control" ReadOnly="true" runat="server" Text='<%# Bind("ChgCode") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="80">
                                                                            <dxe:ASPxComboBox ID="cmb_acCode" ClientInstanceName="txt_det_AcCode" runat="server"
                                                                                 Value='<%# Bind("AcCode") %>' Width="100px" DropDownWidth="300" DropDownStyle="DropDownList"
                                                                                DataSourceID="dsGlAccChart" ValueField="Code" ValueType="System.String" TextFormatString="{0}"
                                                                                EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                                                                CallbackPageSize="100">
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="35px" />
                                                                                    <dxe:ListBoxColumn FieldName="AcDesc" Width="100%" />
                                                                                </Columns>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td width="270">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1" ClientInstanceName="txt_det_Des1"
                                                                                runat="server" Text='<%# Bind("ChgDes1") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="60">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty" ClientInstanceName="spin_det_Qty"
                                                                                runat="server" Value='<%# Bind("Qty") %>' DisplayFormatString="0.000" DecimalPlaces="3" >
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="60">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency"
                                                                                runat="server" Text='<%# Bind("Currency") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="60">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP" ClientInstanceName="spin_det_GstP"
                                                                                ReadOnly="true" BackColor="Control" runat="server" Value='<%# Bind("Gst") %>'
                                                                                DisplayFormatString="0.0000" >
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="100">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt" ClientInstanceName="spin_det_GstAmt"
                                                                                DisplayFormatString="0.00" runat="server" Text='<%# Bind("GstAmt") %>' ReadOnly="true"
                                                                                BackColor="Control">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_ChgCode_Pick" runat="server" Text="Pick" AutoPostBack="False">
                                                                                <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode,txt_det_Des1,txt_det_Unit,txt_det_GstType,spin_det_GstP,txt_det_AcCode);
                                                            }" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="txt_AcSource1" ClientInstanceName="txt_AcSource1" runat="server"
                                                                                 Value='<%# Bind("AcSource") %>' Width="100%" DropDownStyle="DropDownList">
                                                                                <Items>
                                                                                <dxe:ListEditItem Text="DB" Value="DB" />
                                                                                <dxe:ListEditItem Text="CR" Value="CR" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2" ClientInstanceName="txt_det_Remarks2"
                                                                                runat="server" Enabled="true" Text='<%# Bind("ChgDes2") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price" ClientInstanceName="spin_det_Price"
                                                                                DisplayFormatString="0.00" DecimalPlaces="2" runat="server" Text='<%# Bind("Price") %>'>
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="80">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                                                runat="server" Value='<%# Bind("ExRate") %>' DisplayFormatString="0.000000" DecimalPlaces="6" >
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType" ClientInstanceName="txt_det_GstType"
                                                                                runat="server" Text='<%# Bind("GstType") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="100">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt" ClientInstanceName="spin_det_DocAmt"
                                                                                DisplayFormatString="0.00"  ReadOnly="true" BackColor="Control" runat ="server"
                                                                                Text='<%# Eval("DocAmt") %>'>
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
                                                                PopupCurrency(txt_det_Currency,null);
                                                                    }" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt" ClientInstanceName="spin_det_LocAmt"
                                                                                DisplayFormatString="0.00" ReadOnly="true" BackColor="Control" runat="server"
                                                                                Text='<%# Eval("LocAmt") %>'>
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
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
                                                </dxw:ContentControl>
                                            </ContentCollection>
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
