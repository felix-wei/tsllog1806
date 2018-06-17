<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GlEntryEdit.aspx.cs" Inherits="Account_GlEntryEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GL</title>

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
        function PopupCust(txtId, txtName, txtType, txtAcCode) {
            clientId = txtId;
            clientName = txtName;
            clientType = txtType;
            clientAcCode = txtAcCode;
            popubCtr.SetContentUrl('../SelectPage/CustomerList.aspx');
            popubCtr.SetHeaderText('Party To');
            popubCtr.Show();
        }
        function PopupVendor(txtId, txtName, txtType, txtAcCode) {
            clientId = txtId;
            clientName = txtName;
            clientType = txtType;
            clientAcCode = txtAcCode;
            popubCtr.SetContentUrl('../SelectPage/VendorList.aspx');
            popubCtr.SetHeaderText('Party To');
            popubCtr.Show();
        }
        function PopupCurrency(txtId, txtName) {
            clientId = txtId;
            clientName = txtName;
            popubCtr.SetHeaderText('Currency');
            popubCtr.SetContentUrl('../SelectPage/CurrencyList.aspx');
            popubCtr.Show();
        }
        function PopupChart(txtId, txtName, txtSource) {
            clientId = txtId;
            clientName = txtName;
            clientType = txtSource;
            popubCtr.SetContentUrl('../SelectPage/ChartOfAccount.aspx');
            popubCtr.SetHeaderText('CharList');
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsGlEntry" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAGlEntry" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsGlEntryDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAGlEntryDet" KeyMember="SequenceId" FilterExpression="1=0" />
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
                    Supplier Bill No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtBillNo" ClientInstanceName="txtBillNo" Width="110" runat="server"
                        Text="">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    Doc Type
                </td>
                <td>
                    <dxe:ASPxComboBox runat="server" ID="txt_DocType" ClientInstanceName="txt_DocType"
                        Width="100">
                        <Items>
                            <dxe:ListEditItem Value="IV" Text="IV" />
                            <dxe:ListEditItem Value="CN" Text="CN" />
                            <dxe:ListEditItem Value="DN" Text="DN" />
                            <dxe:ListEditItem Value="RE" Text="RE" />
                            <dxe:ListEditItem Value="PC" Text="PC" />
                            <dxe:ListEditItem Value="VO" Text="VO" />
                            <dxe:ListEditItem Value="PL" Text="PL" />
                            <dxe:ListEditItem Value="SC" Text="SC" />
                            <dxe:ListEditItem Value="SD" Text="SD" />
                            <dxe:ListEditItem Value="PS" Text="PS" />
                            <dxe:ListEditItem Value="SR" Text="SR" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" Width="110" runat="server" Text="Retrieve" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='GlEntryEdit.aspx?no='+txtSchNo.GetText()+'&type='+txt_DocType.GetText()+'&billNo='+txtBillNo.GetText()
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton9" Width="110" runat="server" Text="Add New" AutoPostBack="False"
                                    Visible="false">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='GlEntryEdit.aspx?no=0'
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_back" Width="110" runat="server" Text="Go Search" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='../GlEntry.aspx';
                        }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsGlEntry" Width="800" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback1"
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
                                <dxe:ASPxTextBox runat="server" ID="txt_Oid" ReadOnly="true" BackColor="Control"
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
                            <td width="160"><%# Eval("ArApInd")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Party To
                            </td>
                            <td colspan="3"><%# Eval("PartyName")%></td>
                                        <td width="100">
                                            Doc Type
                                        </td>
                                        <td width="160">
                                <strong>
                                        <%# Eval("DocNo") %>(<%# Eval("DocType") %>)
                                </strong>
                                        </td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency
                            </td>
                            <td><%# Eval("CurrencyId") %>
                            </td>
                            <td>
                                Ex Rate
                            </td>
                            <td><%# Eval("ExRate","{0:0.000000}") %>
                                </dxe:ASPxSpinEdit>
                            </td>
                                        <td>
                                            Doc Date
                                        </td>
                                        <td>
                                            <%# Eval("DocDate","{0:dd/MM/yyyy}") %>
                                        </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier Bill No
                            </td>
                            <td>
                                <%# Eval("SupplierBillNo") %>
                            </td>
                            <td>
                                Supplier Bill Date
                            </td>
                            <td>
                                <%# SafeValue.SafeDateStr(Eval("SupplierBillDate")) %>
                            </td>
                            <td>
                                Cheque No
                            </td>
                            <td>
                                <%# Eval("ChqNo") %>
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
                            <td>Cheque Date</td>
                            <td>
                                <%# SafeValue.SafeDateStr(Eval("ChqDate")) %>
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
                                Post User/Date
                            </td>
                            <td>
                                <%# Eval("UserId")%>-<%# Eval("PostDate","{0:dd/MM/yyyy}") %>
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
                            <td style="text-align: right; padding: 2px 2px 2px 2px">
                                <dxe:ASPxButton ID="btn_Post" runat="server" Width="80" AutoPostBack="false" UseSubmitBehavior="false"
                                    Visible="true" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("PostInd"),"N")!="Y" %>'
                                    Text="UnPost">
                                    <ClientSideEvents Click="function(s,e) {
                                        ASPxGridView1.GetValuesOnCustomCallback('P',OnbookCallback);
                                                 }" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="text-align: right; padding: 2px 2px 2px 2px">
                                <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false"
                                    Visible="false" Enabled='<%# SafeValue.SafeString(Eval("PostInd"),"N")!="Y" %>'>
                                    <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="text-align: right; padding: 2px 2px 2px 2px">
                                <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Det" Visible="false" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&SafeValue.SafeString(Eval("PostInd"),"N")!="Y" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                grid_det.AddNewRow();
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
                                    Width="100%" AutoGenerateColumns="False">
                                    <SettingsEditing Mode="EditForm" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <Columns>
                                        <dxwgv:GridViewDataColumn Caption="#">
                                            <DataItemTemplate>
                                                <div style='display: <%# Eval("Display")%>; display: none'>
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
                                        <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="3"
                                            Width="80">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Remark" VisibleIndex="3"
                                            Width="300">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="5"
                                            Width="80">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Doc DBAmt" FieldName="DbAmt" VisibleIndex="6"
                                            Width="80">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Loc DBAmt" FieldName="CurrencyDbAmt" VisibleIndex="6"
                                            Width="80">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Doc CRAmt" FieldName="CrAmt" VisibleIndex="6"
                                            Width="80">
                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Loc CRAmt" FieldName="CurrencyCrAmt" VisibleIndex="6"
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
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode" ReadOnly="true" BackColor="Control"
                                                            ClientInstanceName="txt_det_AcCode" runat="server" Text='<%# Bind("AcCode") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="ASPxTextBox1" runat="server" ReadOnly="true" BackColor="Control"
                                                            Text='<%# Eval("DocNo") %>'>
                                                        </dxe:ASPxTextBox>
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
                                                            DisplayFormatString="0.00" runat="server" Text='<%# Bind("DbAmt") %>'>
                                                            <SpinButtons ShowIncrementButtons="false" />
                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                            PutAmt();
	                                                   }" />
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td width="100">
                                                        <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="ASPxSpinEdit2" ClientInstanceName="spin_det_CrAmt"
                                                            DisplayFormatString="0.00" runat="server" Text='<%# Bind("CrAmt") %>'>
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
                                                        <dxe:ASPxTextBox Width="100%" ID="txt_AcSource" ClientInstanceName="txt_AcSource"
                                                            ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("AcSource") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    </td>
                                                    <td width="80">
                                                        <dxe:ASPxTextBox Width="100%" ID="ASPxTextBox2" runat="server" ReadOnly="true" BackColor="Control"
                                                            Text='<%# Eval("DocType") %>'>
                                                        </dxe:ASPxTextBox>
                                                        <td>
                                                        </td>
                                                        <td width="80">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                                runat="server" Value='<%# Bind("ExRate") %>' DisplayFormatString="0.000000">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                            PutAmt();
	                                                   }" />
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td width="100">
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_CuryDbAmt" ClientInstanceName="spin_det_CuryDbAmt"
                                                                DisplayFormatString="0.00" ReadOnly="false" BackColor="Control" runat="server"
                                                                Text='<%# Eval("CurrencyDbAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_CuryCrAmt" ClientInstanceName="spin_det_CuryCrAmt"
                                                                DisplayFormatString="0.00" ReadOnly="false" BackColor="Control" runat="server"
                                                                Text='<%# Eval("CurrencyCrAmt") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Chart_Pick" Width="50" runat="server" Text="Pick" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
                                                                PopupChart(txt_det_AcCode,null,txt_AcSource);
                                                                    }" />
                                                        </dxe:ASPxButton>
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
