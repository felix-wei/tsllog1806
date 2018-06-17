<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ExpFclQuoteEdit.aspx.cs" Inherits="PagesFreight_ExpFclQuoteEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js" ></script>
    <script type="text/javascript" src="/Script/Pages.js" ></script>

    <script type="text/javascript">
         function ImportStdRate() {
            popubCtr.SetHeaderText('Export Standard List');
            popubCtr.SetContentUrl('/SelectPage/StdRateFcl_multi.aspx?no=' + txt_Oid.GetText());
            popubCtr.Show();
        }
       function ImportDet() {
            popubCtr.SetHeaderText('Export Standard List');
            popubCtr.SetContentUrl('/SelectPage/ExpFclQuoteList_Sea.aspx?id=' + txt_Oid.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_det.Refresh();
        }
        function PrintQuote(invN) {
            window.open('/ReportFreightSea/printview.aspx?document=100&house=0&master=' + invN);
        }  
        function PopupTitle(txtId, txtName) {
            clientId = txtId;
            clientName = null;
            popubCtr.SetContentUrl('/SelectPage/QuoteTitle_Sea.aspx');
            popubCtr.SetHeaderText('Title');
            popubCtr.Show();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsQuotation" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaQuote1" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsQuotationDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaQuoteDet1" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XFCustomer" KeyMember="SequenceID" FilterExpression="CustType='C'" />
        <table>
            <tr>
                <td>
                    Quote No
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
                     window.location='ExpFclQuoteEdit.aspx?no='+txtSchNo.GetText()
                        }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) {
                     window.location='ExpFclQuoteEdit.aspx?no=0'
                        }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsQuotation" Width="100%" KeyFieldName="SequenceId" OnInit="ASPxGridView1_Init"
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
                                <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                    Width="100" Text='<%# Eval("SequenceId")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                Quote No
                            </td>
                            <td width="160">
                                <dxe:ASPxTextBox runat="server" ID="txt_DocNo" ClientInstanceName="txt_DocNo" ReadOnly="true"
                                    BackColor="Control" Width="100" Text='<%# Eval("QuoteNo")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="100">
                                ExpireDate
                            </td>
                            <td width="160">
                                <dxe:ASPxDateEdit ID="txt_ToDt" runat="server" Width="100" Value='<%# Eval("ExpireDate")%>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td>
                                Quote Date
                            </td>
                            <td>
                                <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="100" Value='<%# Eval("QuoteDate")%>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                Quote Title
                            </td>
                            <td colspan="5">
                                <dxe:ASPxTextBox runat="server" ID="txt_Title" ClientInstanceName="txt_Title" Width="650" Text='<%# Eval("Title")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Party To
                            </td>
                            <td colspan="3">
                                <table>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="95"
                                            HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PartyTo") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(txt_CustId,txt_CustName,'CA');
                                }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true"
                                            BackColor="Control" Width="290" runat="server" Text='<%# Eval("PartyName") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </table>
                            </td>
                            <td>Attention
                            </td>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="100" ID="txt_Attention" ClientInstanceName="txt_Attention"
                                    Text='<%# Eval("Attention")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Pol
                            </td>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_Pol" ClientInstanceName="txt_Pol" runat="server" Width="100" MaxLength="5"
                                    HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Pol")%>'>
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pol);
                                                                    }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            <td>Pod
                            </td>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" Width="100"
                                    HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Pod")%>' MaxLength="5">
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_Pod);
                                                                    }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            <td>Via Port
                            </td>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_ViaPort" ClientInstanceName="txt_ViaPort" runat="server" Width="100"
                                    HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ViaPort")%>' MaxLength="5">
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(txt_ViaPort);
                                                                    }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            </tr>
                            <tr>
                            <td>Cont Type
                            </td>
                                <td>
                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="100" ID="cbx_ContType"
                                        Value='<%# Eval("ContType")%>' DropDownStyle="DropDown">
                                        <Items>
                                            <dxe:ListEditItem Text="20GP" Value="20GP" />
                                            <dxe:ListEditItem Text="40GP" Value="40GP" />
                                            <dxe:ListEditItem Text="40HC" Value="40HC" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            <td>
                                Price
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit Increment="0" Width="100" ID="spin_contPrice" ClientInstanceName="spin_contPrice"
                                    runat="server" Value='<%# Eval("ContPrice") %>' DisplayFormatString="0.00" DecimalPlaces="2">
                                    <SpinButtons ShowIncrementButtons="false" />
                                </dxe:ASPxSpinEdit>
                            </td>
                            <td>
                                Transit Days
                            </td>
                            <td>
                                <dxe:ASPxTextBox Width="100" ID="spin_TsDay" ClientInstanceName="spin_TsDay"
                                    runat="server" Text='<%# Eval("TransmitDay") %>' >
                                </dxe:ASPxTextBox>
                            </td>
                            </tr>
                        <tr>
                            <td>Currency
                            </td>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" runat="server" Width="100"
                                    HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CurrencyId") %>' MaxLength="3">
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_exRate);
                                                                    }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            <td>ExRate
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit Increment="0" Width="100" ID="spin_exRate" ClientInstanceName="spin_exRate"
                                    runat="server" Value='<%# Eval("ExRate") %>' DisplayFormatString="0.000000" DecimalPlaces="6">
                                    <SpinButtons ShowIncrementButtons="false" />
                                </dxe:ASPxSpinEdit>
                            </td>
                            <td>Frequency
                            </td>
                            <td>
                                <dxe:ASPxTextBox runat="server" EnableIncrementalFiltering="true" ID="txt_Frequency" Width="100" Value='<%# Eval("Frequency")%>'>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Create User/Date
                            </td>
                            <td>
                                <%# Eval("CreateUser")%>-<%# Eval("CreateDate","{0:dd/MM/yyyy}")%>
                            </td>
                            <td>
                                Update User/Date
                            </td>
                            <td>
                                <%# Eval("UpdateUser")%>-<%# Eval("UpdateDate","{0:dd/MM/yyyy}")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Note
                            </td>
                            <td colspan="5">
                                <dxe:ASPxMemo runat="server" ID="txt_Note" Rows="3" Width="660" Text='<%# Eval("Note")%>'>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remarks
                            </td>
                            <td colspan="5">
                                <dxe:ASPxMemo runat="server" ID="txt_Remarks1" Rows="3" Width="660" Text='<%# Eval("Rmk")%>'>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="6"><table><tr>
                            <td>
                                <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Item" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                grid_det.AddNewRow();
                            }" />
                                </dxe:ASPxButton>
                            </td>
                            <td colspan="2">
                                            <dxe:ASPxButton ID="ASPxButton5" runat="server" Width="120" Text="Standard Rate" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e){
                               ImportStdRate();
                            }" />
                                            </dxe:ASPxButton>
                                            </td>
                            <td colspan="2">
                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" Width="180" Text="Add Item From Quote" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e){
                               ImportDet();
                            }" />
                                            </dxe:ASPxButton>
                                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0" %>'
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s,e){
                                PrintQuote(txt_DocNo.GetText());
                            }" />
                                </dxe:ASPxButton>
                            </td></tr></table></td>
                        </tr>
                    </table>
                    <table width="800">
                        <tr>
                            <td colspan="6">
                                <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server"
                                    DataSourceID="dsQuotationDet" KeyFieldName="SequenceId" OnBeforePerformDataSelect="grid_InvDet_BeforePerformDataSelect"
                                    OnRowUpdating="grid_InvDet_RowUpdating" OnRowInserting="grid_InvDet_RowInserting"
                                    OnInitNewRow="grid_InvDet_InitNewRow" OnInit="grid_InvDet_Init" Width="100%" OnRowDeleting="grid_InvDet_RowDeleting"
                                    AutoGenerateColumns="False">
                                    <SettingsEditing Mode="EditForm" />
                                    <SettingsPager Mode="ShowAllRecords" />
                        <Columns>
                            <dxwgv:GridViewDataColumn Caption="#" Width="80">
                                <DataItemTemplate>
                                    <div style='display: block'>
                                        <a href="#" onclick='<%# "grid_det.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                        <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_det.DeleteRow("+Container.VisibleIndex+");"  %>}'>
                                            Del</a>
                                    </div>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="No" FieldName="QuoteLineNo" VisibleIndex="3"
                                            Width="20" SortIndex="0" SortOrder="Ascending">
                                        </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgDes" VisibleIndex="3"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="5"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="5"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="GstType" FieldName="GstType" VisibleIndex="5" Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="5" Width="80">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="Amt" VisibleIndex="5"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFooter="true" />
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                        </TotalSummary>
                        <Templates>
                            <EditForm>
                                <table style="border: solid 0px black;">
                                    <tr>
                                        <td>Line No
                                        </td>
                                        <td width="40">
                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_LineN" runat="server" ReadOnly="true" BackColor="Control"
                                                Text='<%# Eval("QuoteLineNo") %>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>Group By
                                        </td>
                                        <td colspan="5">
                                            <dxe:ASPxButtonEdit ID="txt_det_GroupTitle" ClientInstanceName="txt_det_GroupTitle" runat="server" Width="460"
                                                HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("GroupTitle") %>' BackColor="Control" ReadOnly="true">
                                                <Buttons>
                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                        PopupTitle(txt_det_GroupTitle,null);
                                                            }" />
                                            </dxe:ASPxButtonEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="90">Charge Code
                                        </td>
                                        <td colspan="2">
                                            <dxe:ASPxButtonEdit ID="txt_det_ChgCode" ClientInstanceName="txt_det_ChgCode" runat="server" Width="100"
                                                HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("ChgCode") %>' BackColor="Control" ReadOnly="true">
                                                <Buttons>
                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                        PopupChgCode_Ar(txt_det_ChgCode,txt_det_ChgCodeDes,txt_det_Unit,txt_det_GstType,null,null,null);
                                                            }" />
                                            </dxe:ASPxButtonEdit>
                                        </td>
                                        <td>
                                            Description
                                        </td>
                                        <td colspan="6">
                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_ChgCodeDes" ClientInstanceName="txt_det_ChgCodeDes"
                                                runat="server" Text='<%# Bind("ChgDes") %>'>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td>
                                            Currency
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox Width="100%" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency" MaxLength="3"
                                                runat="server" Text='<%# Bind("Currency") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="SGD" Value="SGD" />
                                                    <dxe:ListEditItem Text="USD" Value="USD" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            ExRate
                                        </td>
                                        <td>
                                            <dxe:ASPxSpinEdit Increment="0" Width="80" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                DisplayFormatString="0.000000" DecimalPlaces="6" ReadOnly="false" runat="server" Value='<%# Bind("ExRate") %>'>
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                        <td>
                                            Price
                                        </td>
                                        <td>
                                            <dxe:ASPxSpinEdit Increment="0" Width="80" ID="spin_det_Price" ClientInstanceName="spin_det_Price"
                                                DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="false" runat="server" Value='<%# Bind("Price") %>'>
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </td>
                                        <td>
                                            Unit
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox Width="80" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                DropDownStyle="DropDown" runat="server" Value='<%# Bind("Unit") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="20GP" Value="20GP" />
                                                    <dxe:ListEditItem Text="40GP" Value="40GP" />
                                                    <dxe:ListEditItem Text="40HC" Value="40HC" />
                                                    <dxe:ListEditItem Text="45GP" Value="45GP" />
                                                    <dxe:ListEditItem Text="45HC" Value="45HC" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            GstType
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox Width="80" ID="txt_det_GstType" ClientInstanceName="txt_det_GstType"
                                                DropDownStyle="DropDown" runat="server" Value='<%# Bind("GstType") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="S" Value="S" />
                                                    <dxe:ListEditItem Text="Z" Value="Z" />
                                                    <dxe:ListEditItem Text="E" Value="E" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Remark
                                        </td>
                                        <td colspan="11">
                                            <dxe:ASPxMemo Width="100%" ID="txt_det_Remakrs3" Rows="3" runat="server" Text='<%# Bind("Rmk") %>'>
                                            </dxe:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="11" style="text-align: right; padding: 2px 2px 2px 2px">
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
