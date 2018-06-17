﻿<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false"  CodeFile="ApPaymentEdit.aspx.cs" Inherits="Account_ApPaymentEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AP Payment</title>
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
        var unit = null;
        var gstType = null;
        var gstA = null;
        var gstP = null;
        function PutChgCode(codeV, desV, unitV, gstTypeV, gstAV, gstPV) {
            if (clientId != null) {
                clientId.SetText(codeV);
                clientName.SetText(desV);
                unit.SetText(unitV);
                gstType.SetText(gstTypeV);
                gstA.SetNumber(gstAV);
                gstP.SetNumber(gstPV);
                clientId = null;
                clientName = null;
                unit = null;
                gstType = null;
                gstA = null;
                gstP = null;
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
        /////////////////////////////////////////////////////////////////////////////////begin for select invoice
        function PopupAp() {
                popubCtr.SetHeaderText('Ap Payable List');
                popubCtr.SetContentUrl('../SelectPage/ApPayment_ApPayable_multi.aspx?partyTo=' + cmb_PartyTo.GetValue() + "&no=" + txt_Oid.GetText());
                popubCtr.Show();
        }
        function PopupPc() {
            popubCtr.SetHeaderText('Payment Cash List');
            popubCtr.SetContentUrl('../SelectPage/ApPayment_ApPayment_multi.aspx?partyTo=' + cmb_PartyTo.GetValue() + "&no=" + txt_Oid.GetText());
            popubCtr.Show();
        }
        function PopupAr() {
            popubCtr.SetHeaderText('Ar Invoice List');
            popubCtr.SetContentUrl('../SelectPage/ApPayment_ArInvoice_multi.aspx?partyTo=' + cmb_PartyTo.GetValue() + "&no=" + txt_Oid.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');

            if (grid_det != null) {
                grid_det.Refresh();
            }
        }
        var DocDt = null;
        var AcCode = null;
        var DocCurr = null;
        var DocExRate = null;
        var AcTrSrc = null;
        //        var PartyCode = null;
        var DocAmt = null;
        var LocAmt = null;
        /////////////////////////////////////////////////////////////////////////////////end for select invoice


        function PutAmt() {
            spin_LocAmt.SetNumber(Calculate(spin_DocAmt.GetText(), txt_DocExRate.GetText(), 1));
        }
        function PutDetAmt() {
            spin_det_LocAmt.SetNumber(Calculate(spin_det_Amt.GetText(), spin_det_LineExRate.GetText(), 1));
        }
        
        function OnCallback(v) {
            btn_CustRate.SetEnabled(false);
            grid_det.Refresh();
        }
        function OnbookCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function PrintBill() {
            var invN = txt_Oid.GetText();
            window.open("/ReportFreightSea/printview.aspx?document=ApPayment&master=" + invN + "&" + Date());
        }
        function PrintClaimsPay() {
            var invN = txt_Oid.GetText();
            window.open("/ReportAccount/printview.aspx?doc=8&docId=" + invN);
        }
        function PopupBank(codeId, desId) {
            clientId = codeId;
            clientName = desId;
            popubCtr.SetHeaderText('Chart Of Account-Bank');
            popubCtr.SetContentUrl('../SelectPage/ChartOfAccount_bank.aspx');
            popubCtr.Show();
        }

        //select chart of account
        function RowClickHandler(s, e) {
            DropDownEdit.SetText(GridView.cpCode[e.visibleIndex]);
            txt_det_Des1.SetText(GridView.cpDes[e.visibleIndex]);
            DropDownEdit.HideDropDown();
        }
        function OnDeleteCallback(v) {
            ASPxGridView1.Refresh();
        }
        function OnDeleteDetailCallback(v) {
            grid_det.Refresh();
        }

        function onGenerateCallback(v) {
            if (v.length > 0) {
                txt_DocNo.SetText(v);
                txtSchNo.SetText(v);
                btn_GenerateNo.SetEnabled(false);
                ASPxGridView1.Refresh();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsApPayment" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayment" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsApPaymentDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPaymentDet" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsGlAccChart" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartAcc" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
        <table>
            <tr>
                <td>
                    Doc No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtSchNo" ClientInstanceName="txtSchNo" Width="120" runat="server"
                        Text="">
                    </dxe:ASPxTextBox>
                    <div style="display:none">
                        <dxe:ASPxTextBox ID="txt_DocType1a" ClientInstanceName="txt_DocType1a" Width="120" runat="server"
                        Text="">
                    </dxe:ASPxTextBox>
                    </div>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" Width="110" runat="server" Text="Retrieve" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='ApPaymentEdit.aspx?no='+txtSchNo.GetText() + '&type=' + cbo_DocType1.GetText();
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton9" Width="110" runat="server" Text="Add New" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='ApPaymentEdit.aspx?no=0&type=' + cbo_DocType1.GetText();
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_back" Width="110" runat="server" Text="Go Search" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='../ApPayment.aspx?type=' + cbo_DocType1.GetText();
                        }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsApPayment" Width="100%" KeyFieldName="SequenceId" OnInit="ASPxGridView1_Init"
            OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback"
            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
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
                                <dxe:ASPxTextBox runat="server" Width="100" ID="txt_Oid" ClientInstanceName="txt_Oid"
                                    BackColor="Control" ReadOnly="true" Text='<%# Eval("SequenceId")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                Doc No
                            </td>
                            <td width="160">
                                <table><tr><td>
                                <dxe:ASPxTextBox runat="server" ID="txt_DocNo" BackColor="Control" ReadOnly="true"
                                    ClientInstanceName="txt_DocNo" Width="120" Text='<%# Eval("DocNo")%>'>
                                </dxe:ASPxTextBox>
                                           </td>
                                    <td>
                                            <dxe:ASPxButton ID="btn_GenerateNo" ClientInstanceName="btn_GenerateNo" runat="server" Visible="false" Text="Generate No" Width="100" AutoPostBack="false" UseSubmitBehavior="false"
                                                Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("GenerateInd"),"N")=="N" %>'>
                                                <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.GetValuesOnCustomCallback('Generate',onGenerateCallback);
                            }" />
                                            </dxe:ASPxButton>
                                    </td>
                                       </tr></table>
                            </td>
                            <td width="100">
                                Date
                            </td>
                            <td width="160">
                                <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="100" Value='<%# Eval("DocDate")%>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    <ClientSideEvents DateChanged="function(s,e){
                                   txt_ChqDt.SetDate(s.GetDate());
                                   }" />
                                </dxe:ASPxDateEdit>
                            </td>
                            <td width="100">Category
                                <div style="display:none">
                                Doc Type
                                <dxe:ASPxComboBox runat="server" ID="cbo_DocType" ClientInstanceName="cbo_DocType" Width="100" Text='<%# Eval("DocType")%>'>
                                    <Items>
                                        <dxe:ListEditItem Value="PS" Text="PS" />
                                    </Items>
                                </dxe:ASPxComboBox></div>
                            </td>
                            <td width="160">
                                <dxe:ASPxComboBox runat="server" ID="cbo_DocType1" ClientInstanceName="cbo_DocType1" Width="100" Text='<%# Eval("DocType1")%>'>
                                    <Items>
                                        <dxe:ListEditItem Value="Job" Text="Job" />
                                        <dxe:ListEditItem Value="General" Text="General" />
                                        <dxe:ListEditItem Value="Cash" Text="Cash" />
                                        <dxe:ListEditItem Value="PSA" Text="PSA" />
                                    </Items>
                                    <ClientSideEvents ValueChanged='function(s,e){
                                        if(s.GetValue()=="Refund")
                                        {
                                        txt_AcDorc.SetValue("DB");
                                        cbo_DocType.SetValue("SR");
                                        }
                                        else{
                                        txt_AcDorc.SetValue("CR");
                                        cbo_DocType.SetValue("PS");
                                        }
                                        }' />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Party To
                            </td>
                            <td colspan="3">
                                <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                                    Value='<%# Eval("PartyTo") %>' Width="440" DropDownWidth="380" DropDownStyle="DropDownList"
                                    DataSourceID="dsVendorMast" ValueField="PartyId" ValueType="System.String" TextFormatString="{1}"
                                    EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                    CallbackPageSize="100">
                                    <Columns>
                                        <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="40px" />
                                        <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                    </Columns>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                                Currency
                            </td>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" runat="server" MaxLength="3"
                                    Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CurrencyId") %>'>
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,txt_DocExRate);
                                                                    }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Other PartyName
                            </td>
                            <td colspan="3">
                                <dxe:ASPxTextBox runat="server" ID="txt_OtherPartyName" Width="440" Text='<%# Eval("OtherPartyName")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                            <td>
                                Ex Rate
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit Increment="0" ID="txt_DocExRate" Width="100" ClientInstanceName="txt_DocExRate"
                                    DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Eval("ExRate") %>'>
                                    <SpinButtons ShowIncrementButtons="false" />
                                    <ClientSideEvents ValueChanged="function(s, e) {
                                      PutAmt();
	                                }" />
                                </dxe:ASPxSpinEdit>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="3">
                                Remarks
                            </td>
                            <td colspan="3" rowspan="3">
                                <dxe:ASPxMemo runat="server" ID="txt_Remarks1" Rows="5" Width="440" Text='<%# Eval("Remark")%>'>
                                </dxe:ASPxMemo>
                            </td>
                            <td>
                                CASH
                            </td>
                            <td>
                                <dxe:ASPxTextBox runat="server" ID="txt_ChequeNo" Width="100" Text='<%# Eval("ChqNo")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Chq. Date
                            </td>
                            <td style="width: 20%">
                                <dxe:ASPxDateEdit ID="txt_ChqDt" ClientInstanceName="txt_ChqDt" runat="server" Width="100"
                                    Value='<%# Eval("ChqDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                    DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PIC
                            </td>
                            <td>
                                <dxe:ASPxTextBox runat="server" ID="txt_Pic" Width="100" Text='<%# Eval("Pic")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Doc Amt
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit Increment="0" ID="spin_DocAmt" Width="120" ClientInstanceName="spin_DocAmt"
                                    DisplayFormatString="0.00" DecimalPlaces="2" runat="server" Value='<%# Eval("DocAmt") %>'>
                                    <SpinButtons ShowIncrementButtons="false" />
                                    <ClientSideEvents ValueChanged="function(s, e) {
                                      PutAmt();
	                                }" />
                                </dxe:ASPxSpinEdit>
                            </td>
                            <td>
                                Loc Amt
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit Increment="0" ID="spin_LocAmt" Width="100" ClientInstanceName="spin_LocAmt"
                                    DisplayFormatString="0.00" ReadOnly="true" BackColor="Control" runat="server"
                                    Value='<%# Eval("LocAmt") %>'>
                                    <SpinButtons ShowIncrementButtons="false" />
                                </dxe:ASPxSpinEdit>
                            </td>
                             <td style="display:none">
                                PoNo
                            </td>
                            <td style="display:none">
                                <dxe:ASPxTextBox runat="server" ID="txt_PoNo" Width="100" Text='<%# Eval("PoNo")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="background-color: Gray; color: White;">
                                <b>A/C Information</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ac Code
                            </td>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_AcCode" ClientInstanceName="txt_AcCode" runat="server"
                                    Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("AcCode") %>'>
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                        PopupBank(txt_AcCode,null);
                                                            }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            <td>
                                Ac Source
                            </td>
                            <td>
                                <dxe:ASPxTextBox runat="server" ID="txt_AcDorc" ClientInstanceName="txt_AcDorc" Width="100" BackColor="Control" ReadOnly="true"
                                    Text='<%# Eval("AcSource")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                            <td>
                                A/C Period
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox runat="server" Width="55" ReadOnly="true" BackColor="Control" ID="txt_AcYear"
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
                            <td>Create </td>
                            <td><%# Eval("CreateBy") %>-<%# SafeValue.SafeDateStr(Eval("CreateDateTime")) %></td>
                            <td>Update </td>
                            <td><%# Eval("UpdateBy") %>-<%# SafeValue.SafeDateStr(Eval("UpdateDateTime")) %></td>
                            <td>Post </td>
                            <td><%# Eval("PostBy") %>-<%# SafeValue.SafeDateStr(Eval("PostDateTime")) %></td>
                        </tr>
                        <table width="840">
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('S');
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd" runat="server" Width="90" Text="Add AP Det" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                               PopupAp();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Width="90" Text="Add AR Det" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                               PopupAr();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton4" runat="server" Width="90" Text="Add Det" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                              grid_det.AddNewRow();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1p" runat="server" Width="90" Text="Add PC" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                               PopupPc();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'
                                        Text="Gain&Loss">
                                        <ClientSideEvents Click="function(s,e) {
                                       ASPxGridView1.GetValuesOnCustomCallback('GainLoss',OnDeleteDetailCallback);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Print" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>' Text="Print">
                                        <ClientSideEvents Click="function(s,e) {
                                        PrintBill();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                 <td>
                                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>' Text="Print Claims">
                                        <ClientSideEvents Click="function(s,e) {
                                        PrintClaimsPay();
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Post" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>' Text="Post">
                                        <ClientSideEvents Click="function(s,e) {
                                    if(confirm('Confirm post it?')) {
                                        ASPxGridView1.GetValuesOnCustomCallback('P,' + txt_DocNo.GetText(),OnbookCallback);
                                        }
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton5" runat="server" Width="90" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y" %>'
                                        Text="Delete Detail">
                                        <ClientSideEvents Click="function(s,e) {
                                    if(confirm('Confirm delete all details?'))
                                       ASPxGridView1.GetValuesOnCustomCallback('DD',OnDeleteDetailCallback);
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    <table width="820">
                        <tr>
                            <td colspan="6">
                                <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server"
                                    DataSourceID="dsApPaymentDet" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid_InvDet_BeforePerformDataSelect"
                                    OnRowUpdating="grid_InvDet_RowUpdating" OnRowInserting="grid_InvDet_RowInserting"
                                    OnInitNewRow="grid_InvDet_InitNewRow" OnInit="grid_InvDet_Init" OnRowDeleting="grid_InvDet_RowDeleting"
                                    OnRowInserted="grid_InvDet_RowInserted" OnRowUpdated="grid_InvDet_RowUpdated"
                                    OnRowDeleted="grid_InvDet_RowDeleted" AutoGenerateColumns="False">
                                    <SettingsEditing Mode="EditForm" />
                                    <Settings ShowFooter="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <Columns>
                                        <dxwgv:GridViewDataColumn Caption="#" Width="80">
                                            <DataItemTemplate>
                                                <div style='display: <%# Eval("Display")%>'>
                                                    <a href="#" onclick='<%# "grid_det.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                    <a href="#" onclick='if(confirm("Confirm Delete it?"))  {<%# "grid_det.DeleteRow("+Container.VisibleIndex+");"  %>}'>
                                                        Del</a>
                                                </div>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Line No" FieldName="PayLineNo" VisibleIndex="3"
                                            Width="40px" SortIndex="0" SortOrder="Ascending">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Ac Code" FieldName="AcCode" VisibleIndex="3"
                                            Width="130px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3"
                                            Width="250px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Ac Source" FieldName="AcSource" VisibleIndex="3"
                                            Width="40px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="5"
                                            Width="80px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="Remark3" VisibleIndex="5"
                                            Width="80px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="MastRefNo" VisibleIndex="5"
                                            Width="80px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Trip" FieldName="JobRefNo" VisibleIndex="5"
                                            Width="80px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Dept" FieldName="Dim1" Width="80px" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" Width="80px" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="5"
                                            Visible="false" Width="80px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="7"
                                            Width="60px">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="7"
                                            Width="60px">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <TotalSummary>
                                        <dxwgv:ASPxSummaryItem FieldName="LocAmt1" ShowInColumn="LocAmt" SummaryType="Sum"
                                            DisplayFormat="{0:#,##0.00}" />
                                    </TotalSummary>
                                    <Templates>
                                        <EditForm>
                                            <table style="border-bottom: solid 1px black;">
                                                <tr>
                                                    <td width="40px">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_LineN" runat="server" ReadOnly="true" Text='<%# Eval("PayLineNo") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="130px">
                                                        <dxe:ASPxComboBox ID="cmb_acCode" ClientInstanceName="cmb_acCode" runat="server"
                                                            Value='<%# Bind("AcCode") %>' Width="100%" DropDownWidth="400" DropDownStyle="DropDownList"
                                                            DataSourceID="dsGlAccChart" ValueField="Code" ValueType="System.String" TextFormatString="{1} - {0}"
                                                            EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains"
                                                            CallbackPageSize="100">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="35px" />
                                                                <dxe:ListBoxColumn FieldName="AcDesc" Width="100%" />
                                                            </Columns>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td width="250px">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks1" ClientInstanceName="txt_det_Rmk1"
                                                            runat="server" Text='<%# Bind("Remark1") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60px">
                                                        <dxe:ASPxComboBox ID="cbo_AcDorc" runat="server" Width="100%" Value='<%# Bind("AcSource") %>'>
                                                            <Items>
                                                                <dxe:ListEditItem Text="CR" Value="CR" />
                                                                <dxe:ListEditItem Text="DB" Value="DB" />
                                                            </Items>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td width="80px">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency"
                                                            runat="server" Text='<%# Bind("Currency") %>'>
                                                            <ClientSideEvents TextChanged="function(s, e) {
                                                           PutAmt();
	                                                   }" />
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="85px">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Invoice" ClientInstanceName="txt_det_Invoice"
                                                            ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("DocNo") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="85px">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Amt" ClientInstanceName="spin_det_Amt"
                                                            DisplayFormatString="0.00" DecimalPlaces="2" runat="server" Text='<%# Bind("DocAmt") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutDetAmt();
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_Det_DocId" ClientInstanceName="txt_Det_DocId"
                                                                ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("DocId") %>'>
                                                            </dxe:ASPxTextBox>
                                                            <dxe:ASPxTextBox Width="100%" ID="txt_Det_DocType" ClientInstanceName="txt_Det_DocType"
                                                                ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("DocType") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <%--<dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode2" ClientInstanceName="txt_det_Des1" BackColor="Control" ReadOnly="true" Text='<%# Eval("AcCodeStr") %>'
                                                            runat="server">
                                                        </dxe:ASPxTextBox>--%>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2" ClientInstanceName="txt_det_Remarks2"
                                                            runat="server" Enabled="true" Text='<%# Bind("Remark2") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LineExRate" ClientInstanceName="spin_det_LineExRate"
                                                             DecimalPlaces="6" DisplayFormatString="0.000000" runat="server" Value='<%# Bind("ExRate") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutDetAmt();
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxDateEdit ID="txt_invDt" ClientInstanceName="txt_invDt" runat="server" Width="100"
                                                            Value='<%# Bind("DocDate")%>' EditFormat="Custom"
                                                            EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                    <td width="70px">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt" ClientInstanceName="spin_det_LocAmt"
                                                            ReadOnly="true" BackColor="Control" DisplayFormatString="0.00" runat="server"
                                                            Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Job No
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_MastRefNo" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("MastRefNo") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>

                                                                <td>Cont No</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox Width="110" ID="txt_det_Remakrs3" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("Remark3") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Trip</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox Width="60" ID="txt_JobRefNo" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("JobRefNo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Currency_Pick" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency,spin_det_LineExRate);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
													Department
													</td>
													<td>
													   <dxe:ASPxComboBox ID="cmb_dim1" runat="server" Value='<%# Bind("Dim1") %>' Width="100%" >
                                                        <Items>
                                                            <dxe:ListEditItem Text="" Value="" />
                                                            <dxe:ListEditItem Text="PRIME MOVER" Value="PRIME MOVER" />
                                                            <dxe:ListEditItem Text="ESSILOR" Value="ESSILOR" />
                                                            <dxe:ListEditItem Text="UPS" Value="UPS" />
                                                            <dxe:ListEditItem Text="OFFICE" Value="OFFICE" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
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
        HeaderText="Ar Receipt Edit" AllowDragging="True" EnableAnimation="False" Height="500"
        Width="800" EnableViewState="False">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
