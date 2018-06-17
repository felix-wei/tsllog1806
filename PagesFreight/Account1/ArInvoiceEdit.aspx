<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="ArInvoiceEdit.aspx.cs" Inherits="Account_ArInvoiceEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invoice</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">
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
        function AddInvoiceDet() {
            popubCtr.SetHeaderText('Ar Invoice');
            popubCtr.SetContentUrl('ArInvoiceList.aspx?id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_det.Refresh();
        }

        //detail

        function OnCallback(v) {
            if (v == "UPDATE") {
                ASPxGridView1.Refresh();
            } else if (v == "INSERT") {
                txt_det_ChgCode.SetText("");
                txt_det_AcCode.SetText("");
                txt_det_Des1.SetText("");
                spin_det_Qty.SetText("0");
                txt_det_Currency.SetText("SGD");
                spin_det_GstP.SetText("0.000");
                spin_det_GstAmt.SetText("0.00");
                txt_AcSource.SetText("CR");
                txt_det_Remarks2.SetText("");
                spin_det_Price.SetText("0.00");
                spin_det_ExRate.SetText("0.000");
                txt_det_GstType.SetText("Z");
                spin_det_DocAmt.SetText("0.00");
                txt_det_Remakrs3.SetText("");
                txt_det_Unit.SetText("");
                spin_det_LocAmt.SetText("0.00");


                txt_det_ChgCode_2.SetText("");
                txt_det_AcCode_2.SetText("");
                txt_det_Des1_2.SetText("");
                spin_det_Qty_2.SetText("0");
                txt_det_Currency_2.SetText("SGD");
                spin_det_GstP_2.SetText("0.000");
                spin_det_GstAmt_2.SetText("0.00");
                txt_AcSource_2.SetText("CR");
                txt_det_Remarks2_2.SetText("");
                spin_det_Price_2.SetText("0.00");
                spin_det_ExRate_2.SetText("0.000");
                txt_det_GstType_2.SetText("Z");
                spin_det_DocAmt_2.SetText("0.00");
                txt_det_Remakrs3_2.SetText("");
                txt_det_Unit_2.SetText("");
                spin_det_LocAmt_2.SetText("0.00");

                txt_det_ChgCode_3.SetText("");
                txt_det_AcCode_3.SetText("");
                txt_det_Des1_3.SetText("");
                spin_det_Qty_3.SetText("0");
                txt_det_Currency_3.SetText("SGD");
                spin_det_GstP_3.SetText("0.000");
                spin_det_GstAmt_3.SetText("0.00");
                txt_AcSource_3.SetText("CR");
                txt_det_Remarks2_3.SetText("");
                spin_det_Price_3.SetText("0.00");
                spin_det_ExRate_3.SetText("0.000");
                txt_det_GstType_3.SetText("Z");
                spin_det_DocAmt_3.SetText("0.00");
                txt_det_Remakrs3_3.SetText("");
                txt_det_Unit_3.SetText("");
                spin_det_LocAmt_3.SetText("0.00");

                txt_det_ChgCode_4.SetText("");
                txt_det_AcCode_4.SetText("");
                txt_det_Des1_4.SetText("");
                spin_det_Qty_4.SetText("0");
                txt_det_Currency_4.SetText("SGD");
                spin_det_GstP_4.SetText("0.000");
                spin_det_GstAmt_4.SetText("0.00");
                txt_AcSource_4.SetText("CR");
                txt_det_Remarks2_4.SetText("");
                spin_det_Price_4.SetText("0.00");
                spin_det_ExRate_4.SetText("0.000");
                txt_det_GstType_4.SetText("Z");
                spin_det_DocAmt_4.SetText("0.00");
                txt_det_Remakrs3_4.SetText("");
                txt_det_Unit_4.SetText("");
                spin_det_LocAmt_4.SetText("0.00");

                txt_det_ChgCode_5.SetText("");
                txt_det_AcCode_5.SetText("");
                txt_det_Des1_5.SetText("");
                spin_det_Qty_5.SetText("0");
                txt_det_Currency_5.SetText("SGD");
                spin_det_GstP_5.SetText("0.000");
                spin_det_GstAmt_5.SetText("0.00");
                txt_AcSource_5.SetText("CR");
                txt_det_Remarks2_5.SetText("");
                spin_det_Price_5.SetText("0.00");
                spin_det_ExRate_5.SetText("0.000");
                txt_det_GstType_5.SetText("Z");
                spin_det_DocAmt_5.SetText("0.00");
                txt_det_Remakrs3_5.SetText("");
                txt_det_Unit_5.SetText("");
                spin_det_LocAmt_5.SetText("0.00");

                txt_det_ChgCode_6.SetText("");
                txt_det_AcCode_6.SetText("");
                txt_det_Des1_6.SetText("");
                spin_det_Qty_6.SetText("0");
                txt_det_Currency_6.SetText("SGD");
                spin_det_GstP_6.SetText("0.000");
                spin_det_GstAmt_6.SetText("0.00");
                txt_AcSource_6.SetText("CR");
                txt_det_Remarks2_6.SetText("");
                spin_det_Price_6.SetText("0.00");
                spin_det_ExRate_6.SetText("0.000");
                txt_det_GstType_6.SetText("Z");
                spin_det_DocAmt_6.SetText("0.00");
                txt_det_Remakrs3_6.SetText("");
                txt_det_Unit_6.SetText("");
                spin_det_LocAmt_6.SetText("0.00");

                txt_det_ChgCode_7.SetText("");
                txt_det_AcCode_7.SetText("");
                txt_det_Des1_7.SetText("");
                spin_det_Qty_7.SetText("0");
                txt_det_Currency_7.SetText("SGD");
                spin_det_GstP_7.SetText("0.000");
                spin_det_GstAmt_7.SetText("0.00");
                txt_AcSource_7.SetText("CR");
                txt_det_Remarks2_7.SetText("");
                spin_det_Price_7.SetText("0.00");
                spin_det_ExRate_7.SetText("0.000");
                txt_det_GstType_7.SetText("Z");
                spin_det_DocAmt_7.SetText("0.00");
                txt_det_Remakrs3_7.SetText("");
                txt_det_Unit_7.SetText("");
                spin_det_LocAmt_7.SetText("0.00");

                txt_det_ChgCode_8.SetText("");
                txt_det_AcCode_8.SetText("");
                txt_det_Des1_8.SetText("");
                spin_det_Qty_8.SetText("0");
                txt_det_Currency_8.SetText("SGD");
                spin_det_GstP_8.SetText("0.000");
                spin_det_GstAmt_8.SetText("0.00");
                txt_AcSource_8.SetText("CR");
                txt_det_Remarks2_8.SetText("");
                spin_det_Price_8.SetText("0.00");
                spin_det_ExRate_8.SetText("0.000");
                txt_det_GstType_8.SetText("Z");
                spin_det_DocAmt_8.SetText("0.00");
                txt_det_Remakrs3_8.SetText("");
                txt_det_Unit_8.SetText("");
                spin_det_LocAmt_8.SetText("0.00");

                txt_det_ChgCode_9.SetText("");
                txt_det_AcCode_9.SetText("");
                txt_det_Des1_9.SetText("");
                spin_det_Qty_9.SetText("0");
                txt_det_Currency_9.SetText("SGD");
                spin_det_GstP_9.SetText("0.000");
                spin_det_GstAmt_9.SetText("0.00");
                txt_AcSource_9.SetText("CR");
                txt_det_Remarks2_9.SetText("");
                spin_det_Price_9.SetText("0.00");
                spin_det_ExRate_9.SetText("0.000");
                txt_det_GstType_9.SetText("Z");
                spin_det_DocAmt_9.SetText("0.00");
                txt_det_Remakrs3_9.SetText("");
                txt_det_Unit_9.SetText("");
                spin_det_LocAmt_9.SetText("0.00");

                txt_det_ChgCode_10.SetText("");
                txt_det_AcCode_10.SetText("");
                txt_det_Des1_10.SetText("");
                spin_det_Qty_10.SetText("0");
                txt_det_Currency_10.SetText("SGD");
                spin_det_GstP_10.SetText("0.000");
                spin_det_GstAmt_10.SetText("0.00");
                txt_AcSource_10.SetText("CR");
                txt_det_Remarks2_10.SetText("");
                spin_det_Price_10.SetText("0.00");
                spin_det_ExRate_10.SetText("0.000");
                txt_det_GstType_10.SetText("Z");
                spin_det_DocAmt_10.SetText("0.00");
                txt_det_Remakrs3_10.SetText("");
                txt_det_Unit_10.SetText("");
                spin_det_LocAmt_10.SetText("0.00");

                txt_det_ChgCode_11.SetText("");
                txt_det_AcCode_11.SetText("");
                txt_det_Des1_11.SetText("");
                spin_det_Qty_11.SetText("0");
                txt_det_Currency_11.SetText("SGD");
                spin_det_GstP_11.SetText("0.000");
                spin_det_GstAmt_11.SetText("0.00");
                txt_AcSource_11.SetText("CR");
                txt_det_Remarks2_11.SetText("");
                spin_det_Price_11.SetText("0.00");
                spin_det_ExRate_11.SetText("0.000");
                txt_det_GstType_11.SetText("Z");
                spin_det_DocAmt_11.SetText("0.00");
                txt_det_Remakrs3_11.SetText("");
                txt_det_Unit_11.SetText("");
                spin_det_LocAmt_11.SetText("0.00");

                txt_det_ChgCode_12.SetText("");
                txt_det_AcCode_12.SetText("");
                txt_det_Des1_12.SetText("");
                spin_det_Qty_12.SetText("0");
                txt_det_Currency_12.SetText("SGD");
                spin_det_GstP_12.SetText("0.000");
                spin_det_GstAmt_12.SetText("0.00");
                txt_AcSource_12.SetText("CR");
                txt_det_Remarks2_12.SetText("");
                spin_det_Price_12.SetText("0.00");
                spin_det_ExRate_12.SetText("0.000");
                txt_det_GstType_12.SetText("Z");
                spin_det_DocAmt_12.SetText("0.00");
                txt_det_Remakrs3_12.SetText("");
                txt_det_Unit_12.SetText("");
                spin_det_LocAmt_12.SetText("0.00");

                ASPxGridView1.Refresh();

            }
        }

        function PopupChgCode(codeId, desId, unitId, gstTypeId, gstPId, acCode) {
            clientId = codeId;
            clientName = desId;
            unit = unitId;
            gstType = gstTypeId;
            gstP = gstPId;
            clientAcCode = acCode;
            popubCtr.SetHeaderText('Charge Code');
            var mastType = cbo_DocCate.GetText();
            popubCtr.SetContentUrl('/SelectPage/ChgCodeList_Ar.aspx?jobType=' + mastType);
            popubCtr.Show();
        }
        function PutChgCode(codeV, desV, unitV, gstTypeV, gstPV, acCode, acSource) {
            if (clientId != null) {
                clientId.SetText(codeV);
                clientName.SetText(desV);
                unit.SetText(unitV);
                gstType.SetText(gstTypeV);
                gstP.SetValue(gstPV);
                clientAcCode.SetText(acCode);

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
            }
        }
        function PutAmt1(exRateCtr, qtyCtr, priceCtr, gstPCtr, gstAmtCtr, docAmtCtr, locAmtCtr) {
            var exRate = parseFloat(exRateCtr.GetText());
            var qty = parseFloat(qtyCtr.GetText());
            var price = parseFloat(priceCtr.GetText());
            var gst = parseFloat(gstPCtr.GetText());

            var gstAmt = qty * price * gst * exRate;
            var docAmt = qty * price * exRate;
            var locAmt = gstAmt + docAmt;
            gstAmtCtr.SetNumber(gstAmt.toFixed(2));
            docAmtCtr.SetNumber(docAmt.toFixed(2));
            locAmtCtr.SetNumber(locAmt.toFixed(2));
        }
        function deleteRow(i) {
            $("#pr_" + i).css("background-color", "#FF8888");
            try {
                eval("txtCheck_" + i).SetText('Y');
            }
            catch (e) {
            }
            return false;
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
                     window.location='ArInvoiceEdit.aspx?no='+txtSchNo.GetText()
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                 <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                           window.location='ArInvoices.aspx';
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
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback"
                OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback1"
                AutoGenerateColumns="False" >
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
                                    <dxe:ASPxComboBox runat="server" ID="cbo_DocType" ReadOnly='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                        BackColor="Control" Width="155" Text='<%# Eval("DocType")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="IV" Text="IV" />
                                            <dxe:ListEditItem Value="DN" Text="DN" />
                                            <dxe:ListEditItem Value="CN" Text="CN" />
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
                                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                                        Value='<%# Eval("PartyTo") %>' Width="425" DropDownWidth="380" DropDownStyle="DropDownList"
                                        DataSourceID="dsCustomerMast" ValueField="PartyId" ValueType="System.String" TextFormatString="{1}"
                                        EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                        CallbackPageSize="100">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
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
                                <td>Currency
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_Currency" ClientInstanceName="txt_Currency" Width="80" runat="server"  MaxLength="3"
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
                                <td>Due Date
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
                                <td>A/C Code
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="txt_AcCode" ClientInstanceName="txt_AcCode" BackColor="Control"
                                        ReadOnly="true" Width="130" Text='<%# Eval("AcCode")%>'>
                                    </dxe:ASPxTextBox>
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
                                <td>
                                    Job Type </td>
                                <td>
                                    <dxe:ASPxTextBox ID="cbo_DocCate" runat="server" BackColor="Control" ClientInstanceName="cbo_DocCate" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="100"></dxe:ASPxTextBox>
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
                            <tr><td colspan="6"><table><tr>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                               <dxe:ASPxButton ID="btn_Save1_1" runat="server" Text="Update Detail" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y"&&SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'>
                                    <ClientSideEvents Click="function(s, e) {
                                    ASPxGridView1.GetValuesOnCustomCallback('Save',OnCallback);
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Item" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                document.getElementById('div_newRow').style.display='block';
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd1" runat="server" Text="Add Item From IV" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false" Width="150">
                                        <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                PrintInvoice(txt_DocNo.GetText(),'IV');
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print A4" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                PrintInvoiceA4(txt_DocNo.GetText(),'IV');
                            }" />
                                    </dxe:ASPxButton>
                                </td></tr></table></td>
                            </tr>
                        </table>
                        <table width="800">
                            <tr>
                                <td colspan="6">
                                <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server"
                                    DataSourceID="dsArInvoiceDet" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid_InvDet_BeforePerformDataSelect"
                                     OnHtmlRowPrepared="grid_InvDet_HtmlRowPrepared" OnInitNewRow="grid_InvDet_InitNewRow" OnInit="grid_InvDet_Init"
                                    Width="100%" AutoGenerateColumns="False">
                                    <SettingsEditing Mode="EditForm" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <SettingsBehavior AllowSort="false" />
                                    <Columns>
                                        <dxwgv:GridViewDataColumn Caption="#" >
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
                                                        <DataRow>
                                                        <div id='pr_<%# Eval("SequenceId") %>' style="background-color:white;">
                                                                <table style="border-bottom: solid 1px black;">
                                                                    <tr>
                                                                        <td width="40">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_LineN" runat="server" ReadOnly="true" BackColor="Control"
                                                                                Text='<%# Eval("DocLineNo") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                            <div style="display:none" >
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Oid" runat="server" ReadOnly="true" BackColor="Control"
                                                                                Text='<%# Eval("SequenceId") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                            <dxe:ASPxTextBox ID="txtCheck" Width="100%" runat="server" AutoPostBack="false" EnableClientSideAPI="true" Text="N"></dxe:ASPxTextBox> 
                                                                                </div>
                                                                        </td>
                                                                        <td width="80">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode" 
                                                                                BackColor="Control" ReadOnly="true" runat="server" Text='<%# Eval("ChgCode") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="80">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode" ReadOnly="true" BackColor="Control"
                                                                                 runat="server" Text='<%# Eval("AcCode") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="270">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1" runat="server" Text='<%# Eval("ChgDes1") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="60">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty" 
                                                                                runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="60">
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency"  runat="server" MaxLength="3" Text='<%# Eval("Currency") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="60">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP" ReadOnly="false" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>'
                                                                                DisplayFormatString="0.00">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="100">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt" 
                                                                                DisplayFormatString="0.00" runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true"
                                                                                BackColor="Control">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                           <button onclick='deleteRow(<%# Eval("SequenceId") %>);return false;'>Delete</button>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_ChgCode_Pick" runat="server" Text="Pick" AutoPostBack="False">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="txt_AcSource1" runat="server"
                                                                                 Value='<%# Eval("AcSource") %>' Width="100%" DropDownStyle="DropDownList">
                                                                                <Items>
                                                                                <dxe:ListEditItem Text="DB" Value="DB" />
                                                                                <dxe:ListEditItem Text="CR" Value="CR" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2" 
                                                                                runat="server" Enabled="true" Text='<%# Eval("ChgDes2") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price"
                                                                                DisplayFormatString="0.00" ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td width="80">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate" 
                                                                                runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType" runat="server" Text='<%# Eval("GstType") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td width="100">
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt" 
                                                                                DisplayFormatString="0.00" ReadOnly="false" BackColor="Control" runat="server"
                                                                                Text='<%# Eval("DocAmt") %>'>
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                <strong style="color:white; font-size:larger; font-weight:bolder">
                Press Save To Confirm Deletion.
                </strong>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3" runat="server" Text='<%# Eval("ChgDes3") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit" runat="server" Text='<%# Eval("Unit") %>'>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_Currency_Pick" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt" 
                                                                                DisplayFormatString="0.00" ReadOnly="false" BackColor="Control" runat="server"
                                                                                Text='<%# Eval("LocAmt") %>'>
                                                                                <SpinButtons ShowIncrementButtons="false" />
                                                                            </dxe:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                </div>
                                                        </DataRow>
                                        <EditForm>
<div id="div_newRow" style="display:none">
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row1</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode" ClientInstanceName="txt_det_ChgCode" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1" ClientInstanceName="txt_det_Des1"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty" ClientInstanceName="spin_det_Qty"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt1(spin_det_ExRate,spin_det_Qty,spin_det_Price,spin_det_GstP,spin_det_GstAmt,spin_det_DocAmt,spin_det_LocAmt);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP" ClientInstanceName="spin_det_GstP"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt1(spin_det_ExRate,spin_det_Qty,spin_det_Price,spin_det_GstP,spin_det_GstAmt,spin_det_DocAmt,spin_det_LocAmt);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt" ClientInstanceName="spin_det_GstAmt" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_ChgCode_Pick" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode,txt_det_Des1,txt_det_Unit,txt_det_GstType,spin_det_GstP,txt_det_AcCode);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource" ClientInstanceName="txt_AcSource" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2" ClientInstanceName="txt_det_Remarks2"
                                                            runat="server" Enabled="true" Text='<%# Eval("ChgDes2") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price" ClientInstanceName="spin_det_Price" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt1(spin_det_ExRate,spin_det_Qty,spin_det_Price,spin_det_GstP,spin_det_GstAmt,spin_det_DocAmt,spin_det_LocAmt);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutAmt1(spin_det_ExRate,spin_det_Qty,spin_det_Price,spin_det_GstP,spin_det_GstAmt,spin_det_DocAmt,spin_det_LocAmt);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType" ClientInstanceName="txt_det_GstType" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt" ClientInstanceName="spin_det_DocAmt" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3" ClientInstanceName="txt_det_Remakrs3" runat="server" Text='<%# Eval("ChgDes3") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
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
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt" ClientInstanceName="spin_det_LocAmt" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row2</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_2" ClientInstanceName="txt_det_ChgCode_2" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_2" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_2"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_2" ClientInstanceName="txt_det_Des1_2"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_2" ClientInstanceName="spin_det_Qty_2"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_2, spin_det_Qty_2, spin_det_Price_2, spin_det_GstP_2, spin_det_GstAmt_2, spin_det_DocAmt_2, spin_det_LocAmt_2);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_2" ClientInstanceName="txt_det_Currency_2" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_2" ClientInstanceName="spin_det_GstP_2"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_2, spin_det_Qty_2, spin_det_Price_2, spin_det_GstP_2, spin_det_GstAmt_2, spin_det_DocAmt_2, spin_det_LocAmt_2);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_2" ClientInstanceName="spin_det_GstAmt_2" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_2,txt_det_Des1_2,txt_det_Unit_2,txt_det_GstType_2,spin_det_GstP_2,txt_det_AcCode_2);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_2" ClientInstanceName="txt_AcSource_2" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_2" ClientInstanceName="txt_det_Remarks2_2"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_2" ClientInstanceName="spin_det_Price_2" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_2, spin_det_Qty_2, spin_det_Price_2, spin_det_GstP_2, spin_det_GstAmt_2, spin_det_DocAmt_2, spin_det_LocAmt_2);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_2" ClientInstanceName="spin_det_ExRate_2"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_2, spin_det_Qty_2, spin_det_Price_2, spin_det_GstP_2, spin_det_GstAmt_2, spin_det_DocAmt_2, spin_det_LocAmt_2);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_2" ClientInstanceName="txt_det_GstType_2" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_2" ClientInstanceName="spin_det_DocAmt_2" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_2" ClientInstanceName="txt_det_Remakrs3_2" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_2" ClientInstanceName="txt_det_Unit_2"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton4" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_2,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_2" ClientInstanceName="spin_det_LocAmt_2" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_2" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_2" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_2" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row3</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_3" ClientInstanceName="txt_det_ChgCode_3" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_3" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_3"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_3" ClientInstanceName="txt_det_Des1_3"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_3" ClientInstanceName="spin_det_Qty_3"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_3, spin_det_Qty_3, spin_det_Price_3, spin_det_GstP_3, spin_det_GstAmt_3, spin_det_DocAmt_3, spin_det_LocAmt_3);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_3" ClientInstanceName="txt_det_Currency_3" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_3" ClientInstanceName="spin_det_GstP_3"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_3, spin_det_Qty_3, spin_det_Price_3, spin_det_GstP_3, spin_det_GstAmt_3, spin_det_DocAmt_3, spin_det_LocAmt_3);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_3" ClientInstanceName="spin_det_GstAmt_3" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_3" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_3,txt_det_Des1_3,txt_det_Unit_3,txt_det_GstType_3,spin_det_GstP_3,txt_det_AcCode_3);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_3" ClientInstanceName="txt_AcSource_3" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_3" ClientInstanceName="txt_det_Remarks2_3"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_3" ClientInstanceName="spin_det_Price_3" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_3, spin_det_Qty_3, spin_det_Price_3, spin_det_GstP_3, spin_det_GstAmt_3, spin_det_DocAmt_3, spin_det_LocAmt_3);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_3" ClientInstanceName="spin_det_ExRate_3"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_3, spin_det_Qty_3, spin_det_Price_3, spin_det_GstP_3, spin_det_GstAmt_3, spin_det_DocAmt_3, spin_det_LocAmt_3);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_3" ClientInstanceName="txt_det_GstType_3" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_3" ClientInstanceName="spin_det_DocAmt_3" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_3" ClientInstanceName="txt_det_Remakrs3_3" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_3" ClientInstanceName="txt_det_Unit_3"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_3" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_3,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_3" ClientInstanceName="spin_det_LocAmt_3" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_3" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_3" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_3" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row4</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_4" ClientInstanceName="txt_det_ChgCode_4" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_4" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_4"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_4" ClientInstanceName="txt_det_Des1_4"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_4" ClientInstanceName="spin_det_Qty_4"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_4, spin_det_Qty_4, spin_det_Price_4, spin_det_GstP_4, spin_det_GstAmt_4, spin_det_DocAmt_4, spin_det_LocAmt_4);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_4" ClientInstanceName="txt_det_Currency_4" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_4" ClientInstanceName="spin_det_GstP_4"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_4, spin_det_Qty_4, spin_det_Price_4, spin_det_GstP_4, spin_det_GstAmt_4, spin_det_DocAmt_4, spin_det_LocAmt_4);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_4" ClientInstanceName="spin_det_GstAmt_4" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_4" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_4,txt_det_Des1_4,txt_det_Unit_4,txt_det_GstType_4,spin_det_GstP_4,txt_det_AcCode_4);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_4" ClientInstanceName="txt_AcSource_4" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_4" ClientInstanceName="txt_det_Remarks2_4"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_4" ClientInstanceName="spin_det_Price_4" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_4, spin_det_Qty_4, spin_det_Price_4, spin_det_GstP_4, spin_det_GstAmt_4, spin_det_DocAmt_4, spin_det_LocAmt_4);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_4" ClientInstanceName="spin_det_ExRate_4"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_4, spin_det_Qty_4, spin_det_Price_4, spin_det_GstP_4, spin_det_GstAmt_4, spin_det_DocAmt_4, spin_det_LocAmt_4);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_4" ClientInstanceName="txt_det_GstType_4" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_4" ClientInstanceName="spin_det_DocAmt_4" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_4" ClientInstanceName="txt_det_Remakrs3_4" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_4" ClientInstanceName="txt_det_Unit_4"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_4" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_4,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_4" ClientInstanceName="spin_det_LocAmt_4" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_4" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_4" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_4" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row5</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_5" ClientInstanceName="txt_det_ChgCode_5" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_5" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_5"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_5" ClientInstanceName="txt_det_Des1_5"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_5" ClientInstanceName="spin_det_Qty_5"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_5, spin_det_Qty_5, spin_det_Price_5, spin_det_GstP_5, spin_det_GstAmt_5, spin_det_DocAmt_5, spin_det_LocAmt_5);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_5" ClientInstanceName="txt_det_Currency_5" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_5" ClientInstanceName="spin_det_GstP_5"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_5, spin_det_Qty_5, spin_det_Price_5, spin_det_GstP_5, spin_det_GstAmt_5, spin_det_DocAmt_5, spin_det_LocAmt_5);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_5" ClientInstanceName="spin_det_GstAmt_5" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_5" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_5,txt_det_Des1_5,txt_det_Unit_5,txt_det_GstType_5,spin_det_GstP_5,txt_det_AcCode_5);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_5" ClientInstanceName="txt_AcSource_5" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_5" ClientInstanceName="txt_det_Remarks2_5"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_5" ClientInstanceName="spin_det_Price_5" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_5, spin_det_Qty_5, spin_det_Price_5, spin_det_GstP_5, spin_det_GstAmt_5, spin_det_DocAmt_5, spin_det_LocAmt_5);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_5" ClientInstanceName="spin_det_ExRate_5"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_5, spin_det_Qty_5, spin_det_Price_5, spin_det_GstP_5, spin_det_GstAmt_5, spin_det_DocAmt_5, spin_det_LocAmt_5);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_5" ClientInstanceName="txt_det_GstType_5" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_5" ClientInstanceName="spin_det_DocAmt_5" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_5" ClientInstanceName="txt_det_Remakrs3_5" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_5" ClientInstanceName="txt_det_Unit_5"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_5" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_5,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_5" ClientInstanceName="spin_det_LocAmt_5" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_5" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_5" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_5" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row6</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_6" ClientInstanceName="txt_det_ChgCode_6" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_6" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_6"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_6" ClientInstanceName="txt_det_Des1_6"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_6" ClientInstanceName="spin_det_Qty_6"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_6, spin_det_Qty_6, spin_det_Price_6, spin_det_GstP_6, spin_det_GstAmt_6, spin_det_DocAmt_6, spin_det_LocAmt_6);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_6" ClientInstanceName="txt_det_Currency_6" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="60">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_6" ClientInstanceName="spin_det_GstP_6"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_6, spin_det_Qty_6, spin_det_Price_6, spin_det_GstP_6, spin_det_GstAmt_6, spin_det_DocAmt_6, spin_det_LocAmt_6);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_6" ClientInstanceName="spin_det_GstAmt_6" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_6" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_6,txt_det_Des1_6,txt_det_Unit_6,txt_det_GstType_6,spin_det_GstP_6,txt_det_AcCode_6);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_6" ClientInstanceName="txt_AcSource_6" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_6" ClientInstanceName="txt_det_Remarks2_6"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_6" ClientInstanceName="spin_det_Price_6" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_6, spin_det_Qty_6, spin_det_Price_6, spin_det_GstP_6, spin_det_GstAmt_6, spin_det_DocAmt_6, spin_det_LocAmt_6);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_6" ClientInstanceName="spin_det_ExRate_6"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_6, spin_det_Qty_6, spin_det_Price_6, spin_det_GstP_6, spin_det_GstAmt_6, spin_det_DocAmt_6, spin_det_LocAmt_6);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_6" ClientInstanceName="txt_det_GstType_6" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_6" ClientInstanceName="spin_det_DocAmt_6" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_6" ClientInstanceName="txt_det_Remakrs3_6" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_6" ClientInstanceName="txt_det_Unit_6"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_6" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_6,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_6" ClientInstanceName="spin_det_LocAmt_6" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_6" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_6" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_6" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row7</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_7" ClientInstanceName="txt_det_ChgCode_7" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_7" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_7"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_7" ClientInstanceName="txt_det_Des1_7"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_7" ClientInstanceName="spin_det_Qty_7"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_7, spin_det_Qty_7, spin_det_Price_7, spin_det_GstP_7, spin_det_GstAmt_7, spin_det_DocAmt_7, spin_det_LocAmt_7);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_7" ClientInstanceName="txt_det_Currency_7" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_7" ClientInstanceName="spin_det_GstP_7"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_7, spin_det_Qty_7, spin_det_Price_7, spin_det_GstP_7, spin_det_GstAmt_7, spin_det_DocAmt_7, spin_det_LocAmt_7);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_7" ClientInstanceName="spin_det_GstAmt_7" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_7" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_7,txt_det_Des1_7,txt_det_Unit_7,txt_det_GstType_7,spin_det_GstP_7,txt_det_AcCode_7);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_7" ClientInstanceName="txt_AcSource_7" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_7" ClientInstanceName="txt_det_Remarks2_7"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_7" ClientInstanceName="spin_det_Price_7" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_7, spin_det_Qty_7, spin_det_Price_7, spin_det_GstP_7, spin_det_GstAmt_7, spin_det_DocAmt_7, spin_det_LocAmt_7);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_7" ClientInstanceName="spin_det_ExRate_7"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_7, spin_det_Qty_7, spin_det_Price_7, spin_det_GstP_7, spin_det_GstAmt_7, spin_det_DocAmt_7, spin_det_LocAmt_7);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_7" ClientInstanceName="txt_det_GstType_7" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_7" ClientInstanceName="spin_det_DocAmt_7" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_7" ClientInstanceName="txt_det_Remakrs3_7" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_7" ClientInstanceName="txt_det_Unit_7"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_7" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_7,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_7" ClientInstanceName="spin_det_LocAmt_7" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_7" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_7" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_7" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row8</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_8" ClientInstanceName="txt_det_ChgCode_8" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_8" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_8"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_8" ClientInstanceName="txt_det_Des1_8"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_8" ClientInstanceName="spin_det_Qty_8"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_8, spin_det_Qty_8, spin_det_Price_8, spin_det_GstP_8, spin_det_GstAmt_8, spin_det_DocAmt_8, spin_det_LocAmt_8);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_8" ClientInstanceName="txt_det_Currency_8" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_8" ClientInstanceName="spin_det_GstP_8"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_8, spin_det_Qty_8, spin_det_Price_8, spin_det_GstP_8, spin_det_GstAmt_8, spin_det_DocAmt_8, spin_det_LocAmt_8);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_8" ClientInstanceName="spin_det_GstAmt_8" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_8" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_8,txt_det_Des1_8,txt_det_Unit_8,txt_det_GstType_8,spin_det_GstP_8,txt_det_AcCode_8);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_8" ClientInstanceName="txt_AcSource_8" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_8" ClientInstanceName="txt_det_Remarks2_8"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_8" ClientInstanceName="spin_det_Price_8" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_8, spin_det_Qty_8, spin_det_Price_8, spin_det_GstP_8, spin_det_GstAmt_8, spin_det_DocAmt_8, spin_det_LocAmt_8);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_8" ClientInstanceName="spin_det_ExRate_8"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_8, spin_det_Qty_8, spin_det_Price_8, spin_det_GstP_8, spin_det_GstAmt_8, spin_det_DocAmt_8, spin_det_LocAmt_8);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_8" ClientInstanceName="txt_det_GstType_8" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_8" ClientInstanceName="spin_det_DocAmt_8" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_8" ClientInstanceName="txt_det_Remakrs3_8" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_8" ClientInstanceName="txt_det_Unit_8"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_8" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_8,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_8" ClientInstanceName="spin_det_LocAmt_8" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_8" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_8" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_8" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row9</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_9" ClientInstanceName="txt_det_ChgCode_9" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_9" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_9"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_9" ClientInstanceName="txt_det_Des1_9"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_9" ClientInstanceName="spin_det_Qty_9"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_9, spin_det_Qty_9, spin_det_Price_9, spin_det_GstP_9, spin_det_GstAmt_9, spin_det_DocAmt_9, spin_det_LocAmt_9);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_9" ClientInstanceName="txt_det_Currency_9" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_9" ClientInstanceName="spin_det_GstP_9"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_9, spin_det_Qty_9, spin_det_Price_9, spin_det_GstP_9, spin_det_GstAmt_9, spin_det_DocAmt_9, spin_det_LocAmt_9);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_9" ClientInstanceName="spin_det_GstAmt_9" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_9" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_9,txt_det_Des1_9,txt_det_Unit_9,txt_det_GstType_9,spin_det_GstP_9,txt_det_AcCode_9);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_9" ClientInstanceName="txt_AcSource_9" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_9" ClientInstanceName="txt_det_Remarks2_9"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_9" ClientInstanceName="spin_det_Price_9" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_9, spin_det_Qty_9, spin_det_Price_9, spin_det_GstP_9, spin_det_GstAmt_9, spin_det_DocAmt_9, spin_det_LocAmt_9);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_9" ClientInstanceName="spin_det_ExRate_9"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_9, spin_det_Qty_9, spin_det_Price_9, spin_det_GstP_9, spin_det_GstAmt_9, spin_det_DocAmt_9, spin_det_LocAmt_9);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_9" ClientInstanceName="txt_det_GstType_9" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_9" ClientInstanceName="spin_det_DocAmt_9" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_9" ClientInstanceName="txt_det_Remakrs3_9" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_9" ClientInstanceName="txt_det_Unit_9"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_9" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_9,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_9" ClientInstanceName="spin_det_LocAmt_9" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_9" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_9" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_9" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row10</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_10" ClientInstanceName="txt_det_ChgCode_10" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_10" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_10"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_10" ClientInstanceName="txt_det_Des1_10"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_10" ClientInstanceName="spin_det_Qty_10"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_10, spin_det_Qty_10, spin_det_Price_10, spin_det_GstP_10, spin_det_GstAmt_10, spin_det_DocAmt_10, spin_det_LocAmt_10);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_10" ClientInstanceName="txt_det_Currency_10" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_10" ClientInstanceName="spin_det_GstP_10"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_10, spin_det_Qty_10, spin_det_Price_10, spin_det_GstP_10, spin_det_GstAmt_10, spin_det_DocAmt_10, spin_det_LocAmt_10);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_10" ClientInstanceName="spin_det_GstAmt_10" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_10" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_10,txt_det_Des1_10,txt_det_Unit_10,txt_det_GstType_10,spin_det_GstP_10,txt_det_AcCode_10);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_10" ClientInstanceName="txt_AcSource_10" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_10" ClientInstanceName="txt_det_Remarks2_10"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_10" ClientInstanceName="spin_det_Price_10" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_10, spin_det_Qty_10, spin_det_Price_10, spin_det_GstP_10, spin_det_GstAmt_10, spin_det_DocAmt_10, spin_det_LocAmt_10);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_10" ClientInstanceName="spin_det_ExRate_10"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_10, spin_det_Qty_10, spin_det_Price_10, spin_det_GstP_10, spin_det_GstAmt_10, spin_det_DocAmt_10, spin_det_LocAmt_10);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_10" ClientInstanceName="txt_det_GstType_10" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_10" ClientInstanceName="spin_det_DocAmt_10" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_10" ClientInstanceName="txt_det_Remakrs3_10" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_10" ClientInstanceName="txt_det_Unit_10"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_10" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_10,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_10" ClientInstanceName="spin_det_LocAmt_10" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_10" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_10" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_10" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row11</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_11" ClientInstanceName="txt_det_ChgCode_11" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_11" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_11"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_11" ClientInstanceName="txt_det_Des1_11"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_11" ClientInstanceName="spin_det_Qty_11"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_11, spin_det_Qty_11, spin_det_Price_11, spin_det_GstP_11, spin_det_GstAmt_11, spin_det_DocAmt_11, spin_det_LocAmt_11);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_11" ClientInstanceName="txt_det_Currency_11" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_11" ClientInstanceName="spin_det_GstP_11"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_11, spin_det_Qty_11, spin_det_Price_11, spin_det_GstP_11, spin_det_GstAmt_11, spin_det_DocAmt_11, spin_det_LocAmt_11);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_11" ClientInstanceName="spin_det_GstAmt_11" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_11" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_11,txt_det_Des1_11,txt_det_Unit_11,txt_det_GstType_11,spin_det_GstP_11,txt_det_AcCode_11);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_11" ClientInstanceName="txt_AcSource_11" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_11" ClientInstanceName="txt_det_Remarks2_11"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_11" ClientInstanceName="spin_det_Price_11" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_11, spin_det_Qty_11, spin_det_Price_11, spin_det_GstP_11, spin_det_GstAmt_11, spin_det_DocAmt_11, spin_det_LocAmt_11);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_11" ClientInstanceName="spin_det_ExRate_11"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_11, spin_det_Qty_11, spin_det_Price_11, spin_det_GstP_11, spin_det_GstAmt_11, spin_det_DocAmt_11, spin_det_LocAmt_11);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_11" ClientInstanceName="txt_det_GstType_11" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_11" ClientInstanceName="spin_det_DocAmt_11" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_11" ClientInstanceName="txt_det_Remakrs3_11" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_11" ClientInstanceName="txt_det_Unit_11"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_11" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_11,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_11" ClientInstanceName="spin_det_LocAmt_11" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_11" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_11" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_11" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            
                                            <table style="border-bottom: solid 1px red;">
                                                <tr>
                                                    <td width="40">
                                                        <strong style="color:Red">New Row12</strong>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCode_12" ClientInstanceName="txt_det_ChgCode_12" BackColor="Control" ReadOnly="true"
                                                            runat="server" Text='<%# Eval("ChgCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode_12" ReadOnly="true" BackColor="Control" ClientInstanceName="txt_det_AcCode_12"
                                                            runat="server" Text='<%# Eval("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="230">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1_12" ClientInstanceName="txt_det_Des1_12"
                                                            runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty_12" ClientInstanceName="spin_det_Qty_12"
                                                            runat="server" Value='<%# Eval("Qty") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_12, spin_det_Qty_12, spin_det_Price_12, spin_det_GstP_12, spin_det_GstAmt_12, spin_det_DocAmt_12, spin_det_LocAmt_12);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Currency_12" ClientInstanceName="txt_det_Currency_12" MaxLength="3"
                                                            runat="server" Text='<%# Eval("Currency") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="70">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP_12" ClientInstanceName="spin_det_GstP_12"
                                                             ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Gst") %>' DisplayFormatString="0.00">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_12, spin_det_Qty_12, spin_det_Price_12, spin_det_GstP_12, spin_det_GstAmt_12, spin_det_DocAmt_12, spin_det_LocAmt_12);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt_12" ClientInstanceName="spin_det_GstAmt_12" DisplayFormatString="0.00"
                                                            runat="server" Text='<%# Eval("GstAmt") %>' ReadOnly="true" BackColor="Control">
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td><dxe:ASPxButton ID="btn_chgCode_12" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                        PopupChgCode(txt_det_ChgCode_12,txt_det_Des1_12,txt_det_Unit_12,txt_det_GstType_12,spin_det_GstP_12,txt_det_AcCode_12);
                                                            }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource_12" ClientInstanceName="txt_AcSource_12" ReadOnly="true" BackColor="Control" runat="server" Text='<%# Eval("AcSource") %>'>
                                                         </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2_12" ClientInstanceName="txt_det_Remarks2_12"
                                                            runat="server" Enabled="true" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price_12" ClientInstanceName="spin_det_Price_12" DisplayFormatString="0.00"
                                                            ReadOnly="false" runat="server" Text='<%# Eval("Price") %>'>
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_12, spin_det_Qty_12, spin_det_Price_12, spin_det_GstP_12, spin_det_GstAmt_12, spin_det_DocAmt_12, spin_det_LocAmt_12);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td width="80">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate_12" ClientInstanceName="spin_det_ExRate_12"
                                                            runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                          PutAmt1(spin_det_ExRate_12, spin_det_Qty_12, spin_det_Price_12, spin_det_GstP_12, spin_det_GstAmt_12, spin_det_DocAmt_12, spin_det_LocAmt_12);
	                                                   }" />
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_GstType_12" ClientInstanceName="txt_det_GstType_12" runat="server" Text='<%# Eval("GstType") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt_12" ClientInstanceName="spin_det_DocAmt_12" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("DocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Remakrs3_12" ClientInstanceName="txt_det_Remakrs3_12" runat="server" Text=''>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_Unit_12" ClientInstanceName="txt_det_Unit_12"
                                                            runat="server" Text='<%# Eval("Unit") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_currency_12" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupCurrency(txt_det_Currency_12,null);
                                                                    }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt_12" ClientInstanceName="spin_det_LocAmt_12" DisplayFormatString="0.00"
                                                            ReadOnly="false" BackColor="Control" runat="server" Text='<%# Eval("LocAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                     <tr>
                                                        <td colspan="8">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td width="80">
                                                                    </td>
                                                                    <td width="80">Ref No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_MastRefNo_12" runat="server" Text='<%# Eval("MastRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job No </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_JobRefNo_12" runat="server" Text='<%# Eval("JobRefNo") %>' Width="150">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td width="80">Job Type </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txt_mastType_12" runat="server" BackColor="Control" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="80">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                                    </tr>
                                            </table>
                                            </div>
                                        </EditForm>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                        <tr><td colspan="6"><table><tr>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                               <dxe:ASPxButton ID="btn_Save1" runat="server" Text="Update Detail" AutoPostBack="false" UseSubmitBehavior="false" Enabled='<%# SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y"&&SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'>
                                    <ClientSideEvents Click="function(s, e) {
                                    ASPxGridView1.GetValuesOnCustomCallback('Save',OnCallback);
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_DetAdd4" runat="server" Text="Add Item" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                document.getElementById('div_newRow').style.display='block';
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_DetAdd2" runat="server" Text="Add Item From IV" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false" Width="150">
                                    <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet();
                            }" />
                                </dxe:ASPxButton>
                            </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton1_2" runat="server" Text="Print" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                PrintInvoice(txt_DocNo.GetText(),'IV');
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton2_2" runat="server" Text="Print A4" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                PrintInvoiceA4(txt_DocNo.GetText(),'IV');
                            }" />
                                    </dxe:ASPxButton>
                                </td></tr></table></td>
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
